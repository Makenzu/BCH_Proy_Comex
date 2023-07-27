using BCH.Comex.Core.BL.XCFT;
using BCH.Comex.Core.BL.XGPL;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.UI.Web.Areas.Planillas.Models;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.UI.Web.Helpers.Extensions;

namespace BCH.Comex.UI.Web.Areas.Planillas.Controllers
{
    public class ReportesController : BaseController
    {
        private static readonly DateTime SqlSmallDateTimeMinValue = new DateTime(1900, 01, 01, 00, 00, 00);

        private XgplService bl
        {
            get
            {
                return new XgplService(HttpContext.GetCurrentUser().GetDatosUsuario());
            }
        }

        public ReportesController() {
            //bl = new XgplService(HttpContext.GetCurrentUser().GetDatosUsuario());
        }
        
        public ActionResult GeneradasUsuarios(ListadoPlanillasViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuarioStr = HttpContext.GetCurrentUser().GetDatosUsuario().Identificacion_CCtUsr;
                model.CentroCosto = usuarioStr.Substring(0, 3);
                model.CodigoUsuario = usuarioStr.Substring(3, 2);
                var resultSet = bl.ListadoPlanillasUsuarios(model.CentroCosto, model.CodigoUsuario, model.FechaIngreso);
                var listado = from p in resultSet
                              select new DatosPlanillaViewModel()
                              {
                                  CentroCosto = p.cencos,
                                  CodigoMoneda = p.codmnd,
                                  SiglaMoneda = p.codmnd.HasValue ? bl.GetSimboloMoneda(p.codmnd.Value) : string.Empty,
                                  CodigoUsuario = p.codusr,
                                  NombreCliente = p.nomcli,
                                  FechaPresentacion = p.fecpre.Value,
                                  indnom = p.indnom,
                                  ingegr = p.ingegr,
                                  CodComercio = p.codcom,
                                  MontoLiquidadoEgr = p.mtoliqegr,
                                  MontoLiquidadoIngr = p.mtoliqing,
                                  NumeroPresentacion = p.numpre,
                                  Operacion = bl.FormatearNroOperacion(p.operacion),
                                  rutexp = p.rutexp,
                                  tipo = p.tipo,
                                  TipoPlanilla = p.tippln,
                                  OpeRelacionada = bl.FormatearNroOperacion(p.operel)
                              };

                var reporte = new ReportePlanillaViewModel()
                {
                    Fecha = model.FechaIngreso,
                    Titulo = "Planillas Generadas (Usuarios)",
                    Especialista = bl.GetNombreUsuario(model.CentroCosto, model.CodigoUsuario),
                    Planillas = listado.ToList()
                };

                return View(reporte);
            }
            return VolverInicio(model);        
        }

        public ActionResult PlanillasVsConversionUsuarios(ListadoPlanillasViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Esta es una conversión dumb de la lógica existente
                // Primero vamos a replicar el funcionamiento, si tenemos tiempo esto se debe poder hacer
                // de mejor manera en un procedimiento almacenado, en una sola llamada
                var usuarioStr = HttpContext.GetCurrentUser().GetDatosUsuario().Identificacion_CCtUsr;
                model.CentroCosto = usuarioStr.Substring(0, 3);
                model.CodigoUsuario = usuarioStr.Substring(3, 2);
                var listadoConversion = bl.ListadoPlanillasVsConversionUsuarios(model.CentroCosto, model.CodigoUsuario, model.FechaIngreso);
                var listadoResumenPlanillas = bl.ListadoResumenPlanillas(model.CentroCosto, model.CodigoUsuario, model.FechaIngreso);

                var listaResumenPlanillas = listadoResumenPlanillas.ToList();
                var cantidadPorTipo = (from pl in listaResumenPlanillas
                                       group pl by pl.tippln into g
                                       select new { Tipo = g.Key, Cantidad = g.Count() }).ToDictionary(k => k.Tipo, v => v.Cantidad);

                var listadoVsConversionUsuarios = listadoConversion.ToList();
                var conversion = from l in listadoVsConversionUsuarios.AsQueryable()
                                 select new ReportePlanillasVsConversionItemViewModel()
                                 {
                                     Moneda = l.codmnd.HasValue ? bl.GetSimboloMoneda(l.codmnd.Value) : string.Empty,
                                     Ingresos = l.mtoingt.Value,
                                     Egresos = l.mtoegrt.Value,
                                     ConversionDebe = l.mtodeb.Value,
                                     ConversionHaber = l.mtohab.Value,
                                     DiferenciaDebe = l.difdeb.Value,
                                     DiferenciaHaber = l.difhab.Value
                                 };

                // MODGPLN.cs, ~ 1448
                var resumenPorTipo = from it in listaResumenPlanillas
                                     select new ResumenTipoPlanilla()
                                        {
                                            TipoPlanilla = Convert.ToInt32(it.tippln),
                                            Moneda = bl.GetNombreMoneda((int)it.codmnd.Value),
                                            Tipo = it.tipo,
                                            Ingreso = it.ingegr == "I" ? it.mtoliq.Value : 0,
                                            Egreso = it.ingegr == "E" ? it.mtoliq.Value : 0,
                                            Cantidad = cantidadPorTipo[it.tippln]
                                        };

                var reporte = new ReportePlanillasVsConversionViewModel()
                {
                    Fecha = model.FechaIngreso,
                    Especialista = bl.GetNombreUsuario(model.CentroCosto, model.CodigoUsuario),
                    Planillas = conversion,
                    ResumenPorTipo = resumenPorTipo.AsQueryable(),
                    Titulo = "Planillas vs Conversion (Usuarios)"
                };
                return View(reporte);
            }
            return VolverInicio(model);
        }

        public ActionResult GeneradasSeccion(ListadoPlanillasViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuarioStr = HttpContext.GetCurrentUser().GetDatosUsuario().Identificacion_CCtUsr;
                model.CentroCosto = usuarioStr.Substring(0, 3);
                model.CodigoUsuario = usuarioStr.Substring(3, 2);
                var resultSet = bl.ListadoPlanillasUsuarios(model.CentroCosto, "99", model.FechaIngreso);
                var listado = from p in resultSet
                              select new DatosPlanillaViewModel()
                              {
                                  CentroCosto = p.cencos,
                                  CodigoMoneda = p.codmnd,
                                  SiglaMoneda = p.codmnd.HasValue ? bl.GetSimboloMoneda(p.codmnd.Value) : string.Empty,
                                  CodigoUsuario = p.codusr,
                                  NombreCliente = p.nomcli,
                                  FechaPresentacion = p.fecpre.Value,
                                  indnom = p.indnom,
                                  ingegr = p.ingegr,
                                  MontoLiquidadoEgr = p.mtoliqegr,
                                  MontoLiquidadoIngr = p.mtoliqing,
                                  NumeroPresentacion = p.numpre,
                                  Operacion = bl.FormatearNroOperacion(p.operacion),
                                  CodComercio = p.codcom,
                                  rutexp = p.rutexp,
                                  tipo = p.tipo,
                                  TipoPlanilla = p.tippln,
                                  OpeRelacionada = bl.FormatearNroOperacion(p.operel),
                              };

                var listaProcesada = listado.ToList();
                var reporte = new ReportePlanillaViewModel()
                {
                    Fecha = model.FechaIngreso,
                    NumPagina = 1,
                    Titulo = "Planillas Generadas (Seccion)",
                    Planillas = listaProcesada,
                    CentroCosto = model.CentroCosto
                };

                return View(reporte);
            }
            return VolverInicio(model);
        }

        public ActionResult InformePosicionCambio(ListadoPlanillasViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuarioStr = HttpContext.GetCurrentUser().GetDatosUsuario().Identificacion_CCtUsr;
                model.CentroCosto = usuarioStr.Substring(0, 3);
                model.CodigoUsuario = usuarioStr.Substring(3, 2);
                var resultados = bl.InformePosicionCambio(model.CentroCosto, model.FechaIngreso);
                var planillas = from r in resultados
                                select new ReporteInformePosicionCambioItemViewModel()
                                {
                                    NumeroPlanilla = r.numpre,
                                    Fecha = r.fecpre,
                                    CodigoOperacion = (int)r.tippln,
                                    CodigoMoneda = (int)r.codmnd,
                                    Operacion = r.operacion,
                                    Ingreso = r.ingegr == "I" ? r.mtodol : 0,
                                    Egreso = r.ingegr == "E" ? r.mtodol : 0,
                                    MontoOrigen = r.mtoliq,
                                    RutCliente = r.rutexp.FormatoRut(),
                                    CodigoEspecialista = string.Format("{0:D3}-{1:D2}", r.cencos, r.codusr)
                                };
                var reporte = new ReporteInformePosicionCambioViewModel()
                {
                    CentroCosto = model.CentroCosto,
                    Fecha = model.FechaIngreso,
                    Planillas = planillas,
                    Titulo = "Informe Posicion Cambio"
                };
                return View(reporte);
            }
            return VolverInicio(model);
        }

        private ActionResult VolverInicio(ListadoPlanillasViewModel model)
        {
            // return View("Index", "Planillas", model);
            return RedirectToAction("Volver", "Planillas");
        }

        public ActionResult ReferenciasBAEBCH(string keyword)
        {
            ReferenciasBAEBCHViewModel model = new ReferenciasBAEBCHViewModel();
            keyword = keyword != null ? keyword : "";
            model.Detalle = GetReferenciasBAEBCH(keyword.ToUpper());

            return View(model);
        }

        private IQueryable<ItemReferenciasBAEBCHViewModel> GetReferenciasBAEBCH(string referenciaBAE)
        {
            return bl.BuscarReferenciaBAEBCH(referenciaBAE).Select(
                x => new ItemReferenciasBAEBCHViewModel()
                {
                    OperacionBancoChile = x.codcct + "-" + x.codpro + "-" + x.codesp + "-" + x.codofi + "-" + x.codope,
                    ReferenciaNueva = x.refnueva,

                });
        }       

        public ActionResult BuscarMovimientosCuentaCorriente()
        {
            MovimientosCuentaCorrienteViewModel model = new MovimientosCuentaCorrienteViewModel();
            IDictionary<string, string> tiposCuenta = new Dictionary<string, string>() { { "22110217", "Moneda Extranjera" }, { "40001018", "Moneda Nacional" } };
            model.TiposCuenta = new SelectList(tiposCuenta, "Key", "Value");
            //model.TipoCuenta = 0;
            return View(model);
        }

        public ActionResult BuscarMovimientosCuentaContables(CuentaContableMovimientosMode mode)
        {
            return View(mode);
        }

        public ActionResult MovimientosCuentaContables(DateTime fecha, string valor, CuentaContableMovimientosMode mode)
        {
            IEnumerable<IMovimientoContasContables> result;
            switch (mode) {
                case CuentaContableMovimientosMode.NemonicoCuenta:
                    result = bl.sce_mcd_s66_MS(fecha, valor);
                    break;
                case CuentaContableMovimientosMode.NumeroCuenta:
                    result = bl.sce_mcd_s65_MS(fecha, valor);
                    break;
                default:
                    throw new ArgumentException("invalid Mode");

            }   

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MovimientosCuentaCorriente(string tipoCuenta, DateTime fecha)
        {
            ReporteMovimientosCuentaCorrienteViewModel model = new ReporteMovimientosCuentaCorrienteViewModel();
            model.Detalle = new List<ItemMovimientosCuentaCorrienteViewModel>();
            model.Titulo = "Movimientos Cuenta Corriente";
            model.FechaMovimiento = fecha;

            var reporte = bl.Sce_Mcd_S20(fecha, tipoCuenta);
            foreach (BCH.Comex.Core.Entities.Cext01.sce_mcd_s20_MS_Result item in reporte)
            {
                model.Detalle.Add(new ItemMovimientosCuentaCorrienteViewModel()
                {

                    CodigoOperacion = (item.codope.Substring(0, 3) + "-" + item.codope.Substring(3, 2) + "-" + item.codope.Substring(5, 2) + "-" + item.codope.Substring(7, 3) + "-" + item.codope.Substring(10, 5)),
                    CodigoEspecialista = string.Format("{0:D3}-{1:D2}", item.codope.Substring(0, 3), item.codope.Substring(3, 2)),
                    CodigoDh = item.cod_dh,
                    NumeroCme = item.numcct,
                    NemMoneda = item.nemmon,
                    MontoMcd = item.mtomcd,
                    OfiCon = item.oficon,
                    RutCliente = item.rutcli.FormatoRut(),
                    Estado = item.estado
                });
            }

            foreach (var reg in model.Detalle) {
                if (reg.CodigoDh == "D")
                {
                    model.NemMoneda = reg.NemMoneda;
                    model.TotalDebe += (decimal)reg.MontoMcd;
                }

                if (reg.CodigoDh == "H")
                {
                    model.NemMoneda = reg.NemMoneda;
                    model.TotalHaber +=(decimal)reg.MontoMcd;
                }
           
            }
 
            return View(model);
        }
            
        public ActionResult MovimientosCorresponsales(ListadoPlanillasViewModel model)
        {
            if (ModelState.IsValid)
            {
                var reporte = new ReporteMovimientosCorresponsalesViewModel()
                {
                    Titulo = "Movimientos Corresponsales",
                    Fecha = model.FechaIngreso
                };

                var res = bl.ReporteMovimientosCorresponsales(model.CentroCosto, model.FechaIngreso);
                reporte.Items = from item in res
                                select new ReporteMovimientosCorresponsalesItemViewModel()
                                {
                                    Operacion = item.codcct + "-" + item.codpro + "-" + item.codesp + "-" + item.codofi + "-" + item.codope ,
                                    Usuario = item.cencos + "-" + item.codusr,
                                    NumeroReporte = (int)item.nrorpt,
                                    Moneda = item.nemmon,
                                    Cuenta = item.nemcta,
                                    Debe = (item.cod_dh == "D") ? item.mtomcd : 0,
                                    Haber = (item.cod_dh == "H") ? item.mtomcd : 0
                                };
                return View(reporte);
            }
            return VolverInicio(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>Para probar usar fecha 2015/09/11</remarks>
        public ActionResult MovimientosDeCanje()
        {
            // TODO: Validar centro de costo, código original indica que es sólo para centro de costo 826 (FrmgPlv.cs, ~1479)
            var fechaReporte = DateTime.Now.Date;
            var reporte = bl.MovimientosDeCanje(fechaReporte);
            var items = from item in reporte
                        select new ReporteMovimientosDeCanjeItemViewModel()
                        {
                            CentroCosto = item.cencos,
                            CodigoUsuario = item.codusr,
                            Debe = item.mtodeb.HasValue ? item.mtodeb.Value : 0,
                            Haber = item.mtohab.HasValue ? item.mtohab.Value : 0,
                            NemotecnicoCuenta = item.nemcta,
                            NombreCliente = item.nomcli,
                            NumeroCuenta = item.numcta,
                            NumeroOperacion = item.operacion,
                            SimboloMoneda = item.nemmon
                        };
            var usuario = HttpContext.GetCurrentUser().GetDatosUsuario();
            //model.CentroCosto = usuarioStr.Substring(0, 3);
            //ViewBag.ErrorCentroCosto = int.Parse(usuarioStr.Substring(0, 3)) != 826;
            ViewBag.Rut = usuario.Identificacion_Rut;
            return View(new ReporteMovimientosDeCanjeViewModel()
            {
                FechaReporte = fechaReporte,
                Titulo = "Reporte Movimientos de Canje",
                Items = items.ToList()
            });
        }

        public ActionResult ActualizacionPlanillas()
        {
            ActualizacionPlanillasViewModel model = new ActualizacionPlanillasViewModel();
            model.Declaracion = new DeclaracionPlanillaViewModel();
            model.Declaracion.CodigosAduana = GetAduanas();
            
            return View(model);
        }

        private IEnumerable<SelectListItem> GetAduanas()
        {
            return new SelectList(bl.ObtenerAduanas(), "Key", "Value");
        }

        [HttpGet]
        public ActionResult GetPlanillas(string tipoPlanilla, DateTime fechaInicio, DateTime fechaTermino)
        {
            var reporte = bl.Sce_Pldc_S01(tipoPlanilla, fechaInicio, fechaTermino);
            ListadoActualizacionPlanillasViewModel model = new ListadoActualizacionPlanillasViewModel();
            model.Detalle = new List<ItemActualizacionPlanillasViewModel>();

            foreach (BCH.Comex.Core.Entities.Cext01.sce_pldc_s01_MS_Result item in reporte)
            {
                model.Detalle.Add(new ItemActualizacionPlanillasViewModel()
                {
                    NumeroPresentacion = item.NumeroPresentacion,
                    FechaPresentacion = item.FechaPresentacion,
                    FechaPresentacionString = item.FechaPresentacion.FormatoFecha(),
                    AntiguedadDias = item.AntiguedadDias,
                    TotalDolar = item.TotalDolar,
                    RazonSocial = item.RazonSocial,
                    Rut = item.Rut
                });
            }

            return Json(model.Detalle, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeclaracionPlanilla(ActualizacionPlanillasViewModel model)
        {
            model.Declaracion.CodigosAduana = GetAduanas();
            if (ModelState.IsValid)
            {
                model.Declaracion.FechaPresentacionString = model.Declaracion.FechaPresentacion.ToString();
                model.Declaracion.FechaDeclaracionString = model.Declaracion.FechaDeclaracion.ToString();
                model.Declaracion.FechaVencimientoRetornoString = model.Declaracion.FechaVencimientoRetorno.ToString();
                IList<DeclaracionPlanillaViewModel> declaraciones = new List<DeclaracionPlanillaViewModel>() { model.Declaracion };
                return Json(declaraciones);
            }

            return PartialView("FormActualizacionPlanillasDeclaracion", model);
            
        }

        private List<BusquedaPlanillasInformadasItemViewModel> GetListadoPlanillasInformadas(char tipoPlanilla, DateTime fecha)
        {
            var qry = bl.BusquedaPlanillasInformadas(tipoPlanilla, fecha);
            var lista = from p in qry
                        select new BusquedaPlanillasInformadasItemViewModel()
                        {
                            Correlativo = (int)p.correl,
                            NumeroPresentacion = p.numpre.Trim(),
                            CodigoAduana = (int)p.codadn,
                            FechaDeclaracion = p.fecdec,
                            FechaPresentacion = p.fecpre,
                            MontoDolaresPlanilla = p.mtousd,
                            MontoDolaresDeclaracionIngreso = p.mtoapl,
                            MontoInteresDeclaracionIngreso = p.mtoint,
                            NumeroDeclaracion = string.IsNullOrEmpty(p.decimp.Trim()) ? p.decexp.Trim() : p.decimp.Trim(),
                            VtoRet = p.fecvto,
                            TipoPlanilla = tipoPlanilla
                        };

            return lista.ToList().OrderBy(x => x.NumeroDeclaracion).ToList();
        }

        private List<BusquedaPlanillasInformadasItemViewModel> GetListadoPlanillasInformadas(char tipoPlanilla, 
            DateTime fecha, string[] nrosPresentacion)
        {
            var result = new List<BusquedaPlanillasInformadasItemViewModel>();
            var qry = bl.BusquedaPlanillasInformadas(tipoPlanilla, fecha);
            var planillas = qry.ToList();
            
            nrosPresentacion = nrosPresentacion.Distinct().ToArray();
            foreach (var item in nrosPresentacion)
            {
                string numeroPresentacion = item.PadRight(10, ' ');
                var lista = from p in planillas
                            where p.numpre == numeroPresentacion
                            select new BusquedaPlanillasInformadasItemViewModel()
                            {
                                Correlativo = (int)p.correl,
                                NumeroPresentacion = p.numpre.Trim(),
                                CodigoAduana = (int)p.codadn,
                                FechaDeclaracion = p.fecdec,
                                FechaPresentacion = p.fecpre,
                                MontoDolaresPlanilla = p.mtousd,
                                MontoDolaresDeclaracionIngreso = p.mtoapl,
                                MontoInteresDeclaracionIngreso = p.mtoint,
                                NumeroDeclaracion = string.IsNullOrEmpty(p.decimp.Trim()) ? p.decexp.Trim() : p.decimp.Trim(),
                                VtoRet = p.fecvto,
                                TipoPlanilla = tipoPlanilla
                            };
                result.AddRange(lista.ToList());
            }
            return result.OrderBy(X=>X.NumeroDeclaracion).ToList();
        }

        public ActionResult BuscarPlanillasInformadas(char tipoPlanilla, DateTime fecha)
        {
            if (tipoPlanilla == 'I' || tipoPlanilla == 'E' && fecha > new DateTime(1900, 1, 1))
            {
                var res = GetListadoPlanillasInformadas(tipoPlanilla, fecha);
                return new JsonNetResult()
                {
                    Data = res
                };
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult BusquedaPlanillasInformadas()
        {
            return View();
        }

        public ActionResult ImprimirCartaBancoCentral(char tipoPlanilla, DateTime fecha)
        {
            int _codigoFrase;
            var model = new CartaBancoCentralViewModel();
            //Importación: 413 ; Exportación:412
            _codigoFrase = tipoPlanilla != 'E' ? 413 : 412;
            model = new CartaBancoCentralViewModel()
            {
                Frase = bl.FraseCartaBanco(_codigoFrase, ""),
                //Metodo sobrecargado acepta 2 parámetros:
                Planillas = GetListadoPlanillasInformadas(tipoPlanilla, fecha)
            };
            return View(model);
        }

        public ActionResult ImprimirCartaBancoCentralDeclaracionesBak(char tipoPlanilla, string numeroPresentacion)
        {
            int _codigoFrase;
            var nrosPresentacion = numeroPresentacion.Split(',');
            var model = new CartaBancoCentralViewModel();
                //Importación: 413 ; Exportación:412
                _codigoFrase = tipoPlanilla != 'E' ? 413 : 412;

            model = new CartaBancoCentralViewModel()
            {
                Frase = bl.FraseCartaBanco(_codigoFrase, ""),
                //Metodo sobrecargado acepta 3 parámetros:
                Planillas = GetListadoPlanillasInformadas(tipoPlanilla, DateTime.Now.Date, nrosPresentacion)
            };

            return View(model);
        }

        public ActionResult ImprimirCartaBancoCentralDeclaraciones(char tipoPlanilla, string numeroPresentacion, string idDetalle)
        {
            int _codigoFrase;
            var nrosPresentacion = numeroPresentacion.Split(',').Distinct();
            var model = new CartaBancoCentralViewModel();
            //Importación: 413 ; Exportación:412
            _codigoFrase = tipoPlanilla != 'E' ? 413 : 412;

            //obtengo la lista de cache
            List<DeclaracionPlanillaViewModel> listaDetalles = new List<DeclaracionPlanillaViewModel>();
            if (HttpContext.Session[idDetalle] != null)
            {
                listaDetalles = HttpContext.Session[idDetalle] as List<DeclaracionPlanillaViewModel>;
                HttpContext.Session.Remove(idDetalle);
            }

            var listaPlanillas = new List<BusquedaPlanillasInformadasItemViewModel>();
            foreach (var nroPres in nrosPresentacion)
            {
                var listaTmp = listaDetalles.FindAll(x => x.NumeroPresentacion.Trim() == nroPres.Trim()).ToList();
                foreach (var item in listaTmp)
                {
                    listaPlanillas.Add(new BusquedaPlanillasInformadasItemViewModel
                    {
                        Correlativo = 15,
                        NumeroPresentacion = nroPres.Trim(),
                        FechaPresentacion = Convert.ToDateTime(item.FechaPresentacionString),
                        MontoDolaresPlanilla = item.MontoUSDPlanilla,
                        NumeroDeclaracion = item.NumeroDeclaracion,
                        FechaDeclaracion = Convert.ToDateTime(item.FechaDeclaracionString),
                        CodigoAduana = item.CodigoAduana,
                        MontoDolaresDeclaracionIngreso = item.MontoDI.Value,
                        MontoInteresDeclaracionIngreso = item.InteresDI ?? 0,
                        VtoRet = Convert.ToDateTime(item.FechaVencimientoRetornoString),
                        TipoPlanilla = tipoPlanilla
                    });
                }
            }
            
            model = new CartaBancoCentralViewModel()
            {
                Frase = bl.FraseCartaBanco(_codigoFrase, ""),
                //Metodo sobrecargado acepta 3 parámetros:
                Planillas = listaPlanillas
            };

            return View(model);
        }

        public ActionResult ImpresionDeDocumentos()
        {
            var model = new ImpresionDocumentosViewModel();
            ObtenerNroDeOperacionPorDefecto(model);
            return View(model);
        }

        private void ObtenerNroDeOperacionPorDefecto(ImpresionDocumentosViewModel model)
        {
            var usuarioEstacion = bl.ObtenerUsuarioEstacion();
            model.codcct = int.Parse(usuarioEstacion.CentroCosto);
            model.codpro = int.Parse(T_MODGUSR.IdPro_ComVen);
            model.codesp = int.Parse(usuarioEstacion.Especialista);
            model.codofi = "000";
            model.codope = "00000";
        }

        [HandleAjaxException]
        public ActionResult BuscarDocumentosOperaciones(DateTime? fechaOperacion, string codcct, string codpro, string codesp, string codofi, string codope, string contactReference)
        {
            var usuarioEstacion = bl.ObtenerUsuarioEstacion();
            IList<DocumentoOperacion> documentos = null;
            if (fechaOperacion.HasValue)
            {
                int centroCosto = int.Parse(usuarioEstacion.CentroCosto);
                string idUsuario = usuarioEstacion.Especialista;

                documentos = bl.BuscarDocumentosOperacionesPorFecha(fechaOperacion.Value, centroCosto.ToString(), idUsuario);
            }
            else if (!String.IsNullOrEmpty(codcct) && !String.IsNullOrEmpty(codpro) && !String.IsNullOrEmpty(codesp) && !String.IsNullOrEmpty(codofi) && !String.IsNullOrEmpty(codope))
            {
                documentos = bl.BuscarDocumentosOperacionesPorNroOperacion(codcct, codpro, codesp, codofi, codope);
            }
            else
            {
                throw new ArgumentNullException("No se envió una combinación válida de parámetros");
            }

            //LimpiarCacheImpresionDeDocumentos(); //si busco de nuevo puede ser que se haya ido a otra pagina por ej a anular un doc, asi q limpio el cache

            if (documentos != null)
            {
                var jsonResult = new JsonResult()
                {
                    Data = documentos,
                    MaxJsonLength = Int32.MaxValue,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                return jsonResult;
            }
            else return null;
        }

        public ActionResult GetDetalleSwift(string nroMensaje, int nroReporte, DateTime fechaOp, bool? generarHtmlCompleto, bool? imprimir, PrintFormat? format = null)
        {
            //var service = new FundTransferService();
            //ViewBag.DetalleSwift = service.GetDetalleMensajeSwift(nroMensaje);
            //ViewBag.GenerarHtmlCompleto = (generarHtmlCompleto.HasValue && generarHtmlCompleto.Value);
            //ViewBag.Imprimir = (imprimir.HasValue && imprimir.Value);

            //return View("DetalleSwift");
            //return RedirectToAction("GetDetalleSwift", "ImpresionDeDocumentos", new { area = "Impresion", nroMensaje, nroReporte, fechaOp, generarHtmlCompleto, imprimir, format });
            string url = "~/Impresion/ImpresionDeDocumentos/GetDetalleSwift" + (Request.Url.Query ?? string.Empty);
            return new TransferResult(url);
        }

        public ActionResult ReporteContable(int nroReporte, DateTime fechaOp, bool? generarHtmlCompleto, bool? imprimir, PrintFormat? format = null)
        {
            //ReporteContableViewModel model = new ReporteContableViewModel();
            //var service = new FundTransferService();
            //IList<sce_dfc> descripciones = service.GetDescripcionesFuncionesContables();
            //var usuario = HttpContext.GetCurrentUser();
            //model.Reporte = bl.GetReporteContable(usuario.GetDatosUsuario(), nroReporte, fechaOp, descripciones);
            ////model.Reporte = GetReporteContableDeCacheOBL(nroReporte, fechaOp);
            //model.GenerarHtmlCompleto = (generarHtmlCompleto.HasValue && generarHtmlCompleto.Value);
            //model.Imprimir = (imprimir.HasValue && imprimir.Value);

            //return View(model);
            //return RedirectToAction("ReporteContable", "ImpresionDeDocumentos", new { area = "Impresion", nroReporte, fechaOp, generarHtmlCompleto, imprimir, format });
            string url = "~/Impresion/ImpresionDeDocumentos/ReporteContable" + (Request.Url.Query ?? string.Empty);
            return new TransferResult(url);
        }

        public ActionResult IngresoPlanillas()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IngresoPlanillas(IngresoPlanillasViewModel model)
        {
            IngresoPlanillasViewModel result = bl.Sce_Pldc_S03(model.TipoPlanilla, model.NumeroPresentacion, model.FechaPresentacion).Select(
                x => new IngresoPlanillasViewModel()
                {
                    NumeroPresentacion = x.numpre,
                    FechaPresentacion = x.fecpre,
                    FechaPresentacionString = x.fecpre.ToString(),
                    MontoUSD = x.mtodol,
                    NumeroPlanilla = x.numpre,
                    Rut = x.rut,
                    Nombre = x.nombre,
                    AntiguedadDias = x.antiguedad
                }).FirstOrDefault();
            if (result != null)
            {
                result.NumeroPlanilla = result.NumeroPlanilla;
                result.MontoUSD = result.MontoUSD;

                return new JsonNetResult() { Data = result}; 
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        [HttpPost]
        public ActionResult GuardarDeclaracion(List<DeclaracionPlanillaViewModel> model)
        {
            using (var tracer = new Tracer("Guardar Declaracion Adm. Planilla"))
            {
                string guid = Guid.NewGuid().ToString();

                foreach (var item in model)
                {
                    try
                    {
                        DeclaracionPlanillaDTO declaracion = new DeclaracionPlanillaDTO()
                        {
                            NumeroPresentacion = item.NumeroPresentacion,
                            FechaPresentacion = Convert.ToDateTime(item.FechaPresentacionString),
                            Tipo = item.TipoPlanilla.ToString(),
                            FechaActual = DateTime.Now.Date,
                            DeclaracionExportacion = item.TipoPlanilla == 'E' ? item.NumeroDeclaracion.ToString() : "",
                            DeclaracionImportacion = item.TipoPlanilla == 'I' ? item.NumeroDeclaracion.ToString() : "",
                            FechaDeclaracion = Convert.ToDateTime(item.FechaDeclaracionString),
                            CodigoAduana = item.CodigoAduana,
                            MontoDI = item.MontoDI.Value,
                            InteresDI = item.InteresDI.Value,
                            MontoUSD = item.MontoUSDPlanilla,
                            FechaVencimientoRetorno = item.FechaVencimientoRetornoString == null ? SqlSmallDateTimeMinValue : Convert.ToDateTime(item.FechaVencimientoRetornoString)
                        };

                        try
                        {
                            int? resultInsert = bl.GuardaDeclaracionPlanilla(declaracion).FirstOrDefault();
                        }
                        catch (Exception e)
                        {
                            tracer.TraceException("Alerta al Guardar Declaracion Adm. Planilla", e);
                        }

                        bl.ActualizaFechaDeclaracion(DateTime.Now.Date);
                    }
                    catch (Exception e)
                    {
                        tracer.TraceException("Alerta al Guardar Declaracion Adm. Planilla", e);
                    }
                }

                //agrego el modelo a cache
                HttpContext.Session[guid] = model;

                return Json(new { success = true, responseText = "Declaración guardada exitosamente", id = guid });
            }
        }
    }
}