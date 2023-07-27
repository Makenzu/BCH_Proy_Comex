using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.BL.SWEM;
using BCH.Comex.Core.BL.SWGC;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using BCH.Comex.UI.Web.Models.GestionControlSwift;
using BCH.Comex.Core.Entities.Cext01.GestionControlSwift;
using BCH.Comex.Common.Tracing;

namespace BCH.Comex.UI.Web.Controllers
{
    public class GestionControlSwiftController : BCH.Comex.UI.Web.Common.BaseController
    {
        private SwiftMgr bl;
        private SwgcService swgcService;
        private const string CookieName = "BCHComexSwem_Casillas";
        private const string CookieValueCasillasVisibles = "CasillasVisibles";
        private const string CookieValueCasillaDefault = "CasillaDefault";
        private const string CacheCasillasSwiftTodas = "CasillasSwiftTodas";
        private const string CacheTipoCamposConMaximo = "TipoCamposConMaximo";
        private const string CacheCampos = "Campos";
        private const string ConfigTamanioPaginaGridBusqueda = "ConsultaSwift.TamanioPaginaGridBusqueda";
        private const string ConfigCuerpoEmail = "ConsultaSwift.CuerpoEmailMensaje";
        private const string CacheCuerpoEmail = "CuerpoEmailMensaje";
        string _cod_banco_rec;
        string _nombre_banco;
        string _cod_banco_em;
        string _branch_em;
        string _nombre_banco1;
        int sum = 0;
        string _resultado;

        private const string GestionControlSwiftAppRole = "COMEX_SWIFT_SWGC";

        static GestionControlSwiftController()
        {
            new PortalService().RegisterApp("SWGC", "Gestión Control Swift", "SWIFT", GestionControlSwiftAppRole, "COMEX_GRP_SWIFT", "GestionControlSwift");
        }

        public GestionControlSwiftController()
        {
            this.bl = new SwiftMgr();
            this.swgcService = new SwgcService();
        }

        [AuthorizeOrForbidden(Roles = GestionControlSwiftAppRole)]
        public ActionResult Index()
        {
            this.Session[SessionKeys.GestionControlSwift.MensajesNoRecepcionadosKey] = null;

            IEnumerable<sw_casillas> casillas = GetCasillasDeCacheOBL().OrderBy(i => i.cod_casilla);
            SelectList todasLasCasillas = new SelectList(casillas.OrderBy(i => i.cod_casilla).ToList(), "cod_casilla", "DataTextField");

            Index model = new Index();
            model.TodasLasCasillas = todasLasCasillas;
            model.PageSize = short.Parse(ConfigurationManager.AppSettings[ConfigTamanioPaginaGridBusqueda]);

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains(CookieName))
            {
                //leo las casillas visibles y la casilla default de la cookie
                NameValueCollection collection = this.ControllerContext.HttpContext.Request.Cookies[CookieName].Values;
                model.IdsCasillasVisibles = collection[CookieValueCasillasVisibles].Split(new char[] { ',' }).ToList();

                IEnumerable<sw_casillas> casillasVisibles = casillas.Where(c => model.IdsCasillasVisibles.Contains(c.cod_casilla.ToString())).ToList();
                model.IdCasillaDefault = collection[CookieValueCasillaDefault];
                int valorDefault = 0;
                int.TryParse(model.IdCasillaDefault, out valorDefault);

                model.CasillasVisibles = new SelectList(casillasVisibles, "cod_casilla", "DataTextField", valorDefault);
            }

            return View(model);
        }

        [AuthorizeOrForbidden(Roles = GestionControlSwiftAppRole)]
        public ActionResult GestionEnviados()
        {
            this.Session[SessionKeys.GestionControlSwift.MensajesNoRecepcionadosKey] = null;

            IEnumerable<sw_casillas> casillas = GetCasillasDeCacheOBL().OrderBy(i => i.cod_casilla);
            SelectList todasLasCasillas = new SelectList(casillas.OrderBy(i => i.cod_casilla).ToList(), "cod_casilla", "DataTextField");

            GestionEnviados model = new GestionEnviados();
            model.TodasLasCasillas = todasLasCasillas;
            model.PageSize = short.Parse(ConfigurationManager.AppSettings[ConfigTamanioPaginaGridBusqueda]);

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains(CookieName))
            {
                //leo las casillas visibles y la casilla default de la cookie
                NameValueCollection collection = this.ControllerContext.HttpContext.Request.Cookies[CookieName].Values;
                model.IdsCasillasVisibles = collection[CookieValueCasillasVisibles].Split(new char[] { ',' }).ToList();

                IEnumerable<sw_casillas> casillasVisibles = casillas.Where(c => model.IdsCasillasVisibles.Contains(c.cod_casilla.ToString())).ToList();
                model.IdCasillaDefault = collection[CookieValueCasillaDefault];
                int valorDefault = 0;
                int.TryParse(model.IdCasillaDefault, out valorDefault);

                model.CasillasVisibles = new SelectList(casillasVisibles, "cod_casilla", "DataTextField", valorDefault);
            }

            return View(model);
        }

        public ActionResult ControlSwift()
        {
            return View();
        }

        public ActionResult EncasillamientoManualSwift()
        {
            IEnumerable<sw_casillas> casillas = GetCasillasDeCacheOBL().OrderBy(i => i.cod_casilla);
            SelectList todasLasCasillas = new SelectList(casillas, "cod_casilla", "DataTextField");

            Index model = new Index();
            model.TodasLasCasillas = todasLasCasillas;
            model.PageSize = short.Parse(ConfigurationManager.AppSettings[ConfigTamanioPaginaGridBusqueda]);

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains(CookieName))
            {
                //leo las casillas visibles y la casilla default de la cookie
                NameValueCollection collection = this.ControllerContext.HttpContext.Request.Cookies[CookieName].Values;
                model.IdsCasillasVisibles = collection[CookieValueCasillasVisibles].Split(new char[] { ',' }).ToList();

                IEnumerable<sw_casillas> casillasVisibles = casillas.Where(c => model.IdsCasillasVisibles.Contains(c.cod_casilla.ToString())).ToList();
                model.IdCasillaDefault = collection[CookieValueCasillaDefault];
                int valorDefault = 0;
                int.TryParse(model.IdCasillaDefault, out valorDefault);

                model.CasillasVisibles = new SelectList(casillasVisibles, "cod_casilla", "DataTextField", valorDefault);
            }

            return View(model);
        }

        private IEnumerable<sw_casillas> GetCasillasDeCacheOBL()
        {
            //swgcService
            if (this.ControllerContext.HttpContext.Cache[CacheCasillasSwiftTodas] == null)
            {
                //IEnumerable<sw_casillas> result = bl.GetTodasLasCasillas();
                IEnumerable<sw_casillas> result = swgcService.GetTodasLasCasillas();
                this.ControllerContext.HttpContext.Cache[CacheCasillasSwiftTodas] = result;
                return result;
            }
            else
            {
                return this.ControllerContext.HttpContext.Cache[CacheCasillasSwiftTodas] as IEnumerable<sw_casillas>;
            }
        }

        public JsonResult GetSinEncasillar(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var result = swgcService.GetSinEncasillar(idCasilla, fechaInicio, fechaFin);
            return Json(result.Select(i => new
            {
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg,
                Prioridad = i.prioridad,
                Estado = i.estado_msg,
                Casilla = i.casilla,
                FechaRecepcion = i.fecha1 + " " + i.fecha2,
                BancoEmisor = i.cod_banco_em + i.branch_em,
                Beneficiario = i.beneficiario,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia
            }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetImpresos(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var result = swgcService.GetImpresos(idCasilla, fechaInicio, fechaFin);
            return Json(result.Select(i => new
            {
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg,
                Prioridad = i.prioridad,
                Estado = i.estado_msg,
                Casilla = i.casilla,
                FechaRecepcion = i.fecha1 + " " + i.fecha2,
                BancoEmisor = i.cod_banco_em + i.branch_em,
                Beneficiario = i.beneficiario,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia
            }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRechazados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var result = swgcService.GetRechazados(idCasilla, fechaInicio, fechaFin);
            return Json(result.Select(i => new
            {
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg,
                Prioridad = i.prioridad,
                Estado = i.estado_msg,
                Casilla = i.casilla,
                FechaRecepcion = i.fecha1 + " " + i.fecha2,
                BancoEmisor = i.cod_banco_em + i.branch_em,
                Beneficiario = i.beneficiario,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia
            }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPosiblesDuplicados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var result = swgcService.GetPosiblesDuplicados(idCasilla, fechaInicio, fechaFin);
            return Json(result.Select(i => new
            {
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg,
                Prioridad = i.prioridad,
                Estado = i.estado_msg,
                Casilla = i.casilla,
                FechaRecepcion = i.fecha1 + " " + i.fecha2,
                BancoEmisor = i.cod_banco_em + i.branch_em,
                Beneficiario = i.beneficiario,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia
            }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEncasillados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var result = swgcService.GetEncasillados(idCasilla, fechaInicio, fechaFin);
            return Json(result.Select(i => new
            {
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg,
                Prioridad = i.prioridad,
                Estado = i.estado_msg,
                Casilla = i.casilla,
                FechaRecepcion = i.fecha1 + " " + i.fecha2,
                BancoEmisor = i.cod_banco_em + i.branch_em,
                Beneficiario = i.beneficiario,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia
            }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetConfirmados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var result = swgcService.GetConfirmados(idCasilla, fechaInicio, fechaFin);
            return Json(result.Select(i => new
            {
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg,
                Prioridad = i.prioridad,
                Estado = i.estado_msg,
                Casilla = i.casilla,
                FechaRecepcion = i.fecha1 + " " + i.fecha2,
                BancoEmisor = i.cod_banco_em + i.branch_em,
                Beneficiario = i.beneficiario,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia
            }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReenviados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var result = swgcService.GetReenviados(idCasilla, fechaInicio, fechaFin);
            return Json(result.Select(i => new
            {
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg,
                Prioridad = i.prioridad,
                Estado = i.estado_msg,
                Casilla = i.casilla,
                FechaRecepcion = i.fecha1 + " " + i.fecha2,
                BancoEmisor = i.cod_banco_em + i.branch_em,
                Beneficiario = i.beneficiario,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia
            }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTodos(int idCasilla, DateTime fechaDesde, DateTime fechaHasta)
        {
            var result = swgcService.GetTodos(idCasilla, fechaDesde, fechaHasta, "0");
            var jsonResult = Json(result.Select(i => new
            {
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Casilla = i.casilla,
                NombreCasilla = i.nombre_casilla,
                Tipo = i.tipo_msg,
                TipoNombre = i.nombre_tipo,
                Prioridad = i.prioridad,
                Estado = i.estado_msg,
                FechaRecepcion = i.fecha1 + " " + i.fecha2,
                i.fecha1,
                i.fecha2,
                i.cod_banco_rec,
                i.branch_rec,
                BancoEmisor = i.cod_banco_em + i.branch_em,
                i.cod_banco_em,
                i.branch_em,
                NombreBanco = i.nombre_banco,
                CiudadBanco = i.ciudad_banco,
                PaisBanco = i.pais_banco,
                OficinaBanco = i.oficina_banco,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia.Trim(),
                Beneficiario = i.beneficiario,
                NumeroImpresion = i.total_imp,
                Comentario = i.comentario
            }), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetTodosEnviados(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, int offset, int fetchrows)
        {
            var result = swgcService.GetTodosMensajesEnviadosExteriorPaginado(idCasilla, fechaDesde, fechaHasta, offset, fetchrows);
            var jsonResult = Json(result.Select(i => new
            {
                Id = i.id_mensaje,
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Casilla = i.casilla,
                RutIngreso = i.rut_ingreso,
                Monto = i.monto,
                NombreCasilla = i.nombre_casilla,
                Tipo = i.tipo_msg,
                TipoNombre = i.nombre_tipo,
                Prioridad = i.prioridad,
                Estado = i.estado_msg,
                FechaIngreso = i.fecha_ingr + " " + i.hora_ingr,
                FechaEnvio = i.fecha_env + " " + i.hora_env,
                BancoReceptor = i.cod_banco_rec + i.branch_rec,
                i.cod_banco_rec,
                i.branch_rec,
                BancoEmisor = i.cod_banco_em + i.branch_em,
                i.cod_banco_em,
                i.branch_em,
                NombreBanco = i.nombre_banco,
                CiudadBanco = i.ciudad_banco,
                PaisBanco = i.pais_banco,
                OficinaBanco = i.oficina_banco,
                Moneda = i.cod_moneda,
                NombreMoneda = i.nombre_moneda,
                CodigoMoneda = i.cod_moneda_banco,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario
            }), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [FileDownload]
        public ActionResult Exportar(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, string _codEstado, string _tipoConsulta = "")
        {
            MemoryStream ms = new MemoryStream();
            switch (_tipoConsulta)
            {
                case "env":
                    ms = swgcService.getExcel(swgcService.GetTodosMensajesEnviadosExteriorPaginado(idCasilla, fechaDesde, fechaHasta, 0, 500));
                    break;
                default:
                    ms = swgcService.getExcel(swgcService.GetTodos(idCasilla, fechaDesde, fechaHasta, _codEstado));
                    break;
            }
            return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "GESTION_" + _tipoConsulta + DateTime.Now.ToString("yyyymmdd_hhmmss") + ".xlsx");
        }

        public ActionResult LogSwiftRecibido(int sesion, int secuencia)
        {
            LogSwiftViewModel model = new LogSwiftViewModel();
            model.Log = bl.GetLogDeMensajeRecibido(sesion, secuencia);
            model.Sesion = sesion;
            model.Secuencia = secuencia;
            return View("LogSwift", model);
        }

        public ActionResult ReporteConsultaSwift(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, byte direccion, string filtroCampo, string filtroDesc)
        {
            fechaDesde = fechaDesde.Date; //para sacar cualquier componente de hora:min que me pudiera venir de js
            fechaHasta = fechaHasta.Date;

            bool enviados = (direccion != 2);
            int totalRows = 0;
            var resultado = swgcService.BuscarSwiftsPorCasillaYFechas(idCasilla, fechaDesde, fechaHasta, infoUsuario.RutEnFormatoBDSwift, (direccion != 2), out totalRows, 0, short.MaxValue, null); //pago el maximo tamanio de pagina posible

            if (!enviados && filtroCampo != null)
            {
                resultado = resultado.Where(r => r.estado_msg == filtroCampo).ToList();
            }

            IList<sw_casillas> casillas = GetCasillasDeCacheOBL1().OrderBy(i => i.cod_casilla).ToList();
            sw_casillas casilla = casillas.Where(c => c.cod_casilla == idCasilla).FirstOrDefault();

            ReporteConsultaSwiftViewModel model = new ReporteConsultaSwiftViewModel()
            {
                FechaDesde = fechaDesde,
                Enviados = enviados,
                Registros = resultado
            };

            if (fechaHasta != fechaDesde)
            {
                model.FechaHasta = fechaHasta;
            }

            if (casilla != null)
            {
                model.Casilla = casilla.nombre_casilla;
            }
            else
            {
                model.Casilla = idCasilla.ToString();
            }

            model.Filtro = (String.IsNullOrEmpty(filtroCampo) ? "Todos" : filtroDesc);
            return View(model);
        }

        private IList<sw_casillas> GetCasillasDeCacheOBL1()
        {

            if (this.ControllerContext.HttpContext.Cache[CacheCasillasSwiftTodas] == null)
            {
                IList<sw_casillas> result = swgcService.GetTodasLasCasillas1();
                this.ControllerContext.HttpContext.Cache[CacheCasillasSwiftTodas] = result;
                return result;
            }
            else
            {
                return this.ControllerContext.HttpContext.Cache[CacheCasillasSwiftTodas] as IList<sw_casillas>;
            }
        }


        public ActionResult ReporteConsultaSwift01(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, byte direccion, string filtroCampo, string filtroDesc)
        {
            fechaDesde = fechaDesde.Date; //para sacar cualquier componente de hora:min que me pudiera venir de js
            fechaHasta = fechaHasta.Date;

            bool enviados = (direccion != 2);
            int totalRows = 0;
            var resultado = swgcService.BuscarSwiftsPorCasillaYFechas(idCasilla, fechaDesde, fechaHasta, infoUsuario.RutEnFormatoBDSwift, (direccion != 2), out totalRows, 0, short.MaxValue, null); //paso el maximo tamanio de pagina posible

            if (!enviados && filtroCampo != null)
            {
                resultado = resultado.Where(r => r.estado_msg == filtroCampo).ToList();
            }

            IList<sw_casillas> casillas = GetCasillasDeCacheOBL1().OrderBy(i => i.cod_casilla).ToList();
            sw_casillas casilla = casillas.Where(c => c.cod_casilla == idCasilla).FirstOrDefault();

            ReporteConsultaSwiftViewModel model = new ReporteConsultaSwiftViewModel()
            {
                FechaDesde = fechaDesde,
                Enviados = enviados,
                Registros = resultado
            };

            if (fechaHasta != fechaDesde)
            {
                model.FechaHasta = fechaHasta;
            }

            if (casilla != null)
            {
                model.Casilla = casilla.nombre_casilla;
            }
            else
            {
                model.Casilla = idCasilla.ToString();
            }

            model.Filtro = (String.IsNullOrEmpty(filtroCampo) ? "Todos" : filtroDesc);
            return View(model);
        }

        public ActionResult GetControlRecepcionMensajes(int idCasilla, DateTime fechaDesde)
        {
            var result = swgcService.GetControlRecepcionMensajes(idCasilla, fechaDesde).OrderBy(x => x.sesion).ThenBy(c => c.secuencia);

            return Json(result.Select(i => new
            {
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Estado = i.estado_msg,
                cod_banco_rec = i.cod_banco_rec,
            }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTodosEncasillamientoManual(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, string _codEstado)
        {
            var result = swgcService.GetTodosEncasillamientoManual(idCasilla, fechaDesde, fechaHasta);
            return Json(result.Select(i => new
            {
                Casilla = i.casilla,
                Secuencia = i.secuencia,
                Sesion = i.sesion,
                Tipo = i.tipo_msg,
                Estado = i.estado_msg,
                FechaRecepcion = i.fecha1 + " " + i.hora1,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                Moneda = i.cod_moneda,
                Monto = i.monto
            }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [FileDownload]
        public FileResult ExportarEncasillamientoAutomatico(int idCasilla, DateTime fechaDesde, DateTime fechaHasta)
        {
            using (var tracer = new Tracer("Exportar Encasillamiento Automatico"))
            {
                var result = this.swgcService.GetTodosEncasillamientoAutomatico(idCasilla, fechaDesde, fechaHasta);

                if (result.Count == 0)
                {
                    tracer.TraceInformation("No es posible generar excel");
                    return null;
                }

                MemoryStream msFile = SwgcService.GetExportedFile(result.Select(i =>
                {
                    DateTime fecha = new DateTime(1900, 1, 1);
                    DateTime.TryParse(i.fecha_send + " " + i.hora_ack, out fecha);
                    return new ResultItem
                    {
                        Secuencia = i.secuencia,
                        Sesion = i.sesion,
                        Tipo = i.tipo_msg,
                        Estado = i.estado_msg,
                        FechaRecepcion = fecha,
                        Referencia = i.referencia,
                        Beneficiario = i.beneficiario,
                        Moneda = i.cod_moneda,
                        Monto = i.monto
                    };
                }).ToList(), "EncasillamientoAutomatico");

                return File(msFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EncasillamientoAutomatico.xlsx");
            }
        }

        [HttpPost]
        [FileDownload]
        public FileResult ExportarEncasillamientoManual(int idCasilla, DateTime fechaDesde, DateTime fechaHasta)
        {
            using (var tracer = new Tracer("Exportar Encasillamiento Manual"))
            {
                var result = this.swgcService.GetTodosEncasillamientoManual(idCasilla, fechaDesde, fechaHasta);

                if (result.Count == 0)
                {
                    tracer.TraceInformation("No es posible generar excel");
                    return null;
                }

                MemoryStream msFile = SwgcService.GetExportedFile(result.Select(i => 
                {
                    DateTime fecha = new DateTime(1900, 1, 1);
                    DateTime.TryParse(i.fecha1 + " " + i.hora1, out fecha);
                    return new ResultItem
                    {
                        Secuencia = i.secuencia,
                        Sesion = i.sesion,
                        Tipo = i.tipo_msg,
                        Estado = i.estado_msg,
                        FechaRecepcion = fecha,
                        Referencia = i.referencia,
                        Beneficiario = i.beneficiario,
                        Moneda = i.cod_moneda,
                        Monto = i.monto
                    };
                }).ToList(), "EncasillamientoManual");

                return File(msFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EncasillamientoManual.xlsx"); 
            }
        }

        public ActionResult EstadisticasMensajesRecepcionadosSwift()
        {
            IEnumerable<sw_casillas> casillas = GetCasillasDeCacheOBL().OrderBy(i => i.cod_casilla);
            SelectList todasLasCasillas = new SelectList(casillas, "cod_casilla", "DataTextField");

            Index model = new Index();
            model.TodasLasCasillas = todasLasCasillas;
            model.PageSize = short.Parse(ConfigurationManager.AppSettings[ConfigTamanioPaginaGridBusqueda]);

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains(CookieName))
            {
                //leo las casillas visibles y la casilla default de la cookie
                NameValueCollection collection = this.ControllerContext.HttpContext.Request.Cookies[CookieName].Values;
                model.IdsCasillasVisibles = collection[CookieValueCasillasVisibles].Split(new char[] { ',' }).ToList();

                IEnumerable<sw_casillas> casillasVisibles = casillas.Where(c => model.IdsCasillasVisibles.Contains(c.cod_casilla.ToString())).ToList();
                model.IdCasillaDefault = collection[CookieValueCasillaDefault];
                int valorDefault = 0;
                int.TryParse(model.IdCasillaDefault, out valorDefault);

                model.CasillasVisibles = new SelectList(casillasVisibles, "cod_casilla", "DataTextField", valorDefault);
            }

            return View(model);
        }

        public ActionResult EstadisticaEnviarRecibir()
        {

            IEnumerable<sw_casillas> casillas = GetCasillasDeCacheOBL().OrderBy(i => i.cod_casilla);
            SelectList todasLasCasillas = new SelectList(casillas.OrderBy(i => i.cod_casilla).ToList(), "cod_casilla", "DataTextField");

            Index model = new Index();
            model.TodasLasCasillas = todasLasCasillas;
            model.PageSize = short.Parse(ConfigurationManager.AppSettings[ConfigTamanioPaginaGridBusqueda]);

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains(CookieName))
            {
                //leo las casillas visibles y la casilla default de la cookie
                NameValueCollection collection = this.ControllerContext.HttpContext.Request.Cookies[CookieName].Values;
                model.IdsCasillasVisibles = collection[CookieValueCasillasVisibles].Split(new char[] { ',' }).ToList();

                IEnumerable<sw_casillas> casillasVisibles = casillas.Where(c => model.IdsCasillasVisibles.Contains(c.cod_casilla.ToString())).ToList();
                model.IdCasillaDefault = collection[CookieValueCasillaDefault];
                int valorDefault = 0;
                int.TryParse(model.IdCasillaDefault, out valorDefault);

                model.CasillasVisibles = new SelectList(casillasVisibles, "cod_casilla", "DataTextField", valorDefault);
            }

            return View(model);
        }

        public ActionResult EstadisticasSwifts(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, byte direccion)
        {
            fechaDesde = fechaDesde.Date; //para sacar cualquier componente de hora:min que me pudiera venir de js
            fechaHasta = fechaHasta.Date;

            JsonResult jsonResult;

            try
            {
                var result = bl.GetEstadisticasMensajesPorCasilla(idCasilla, fechaDesde, fechaHasta, (direccion != 2));

                jsonResult = new JsonResult()
                {
                    Data = result,
                    MaxJsonLength = Int32.MaxValue,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception)
            {
                jsonResult = new JsonResult()
                {
                    Data = string.Empty,
                    MaxJsonLength = Int32.MaxValue,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            return jsonResult;
        }

        public ActionResult ReporteEstadistica(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, byte direccion)
        {
            if (direccion == 1) // enviados
            {
                return RedirectToAction("ReporteEstadisticasMensajesEnviadosSwift", new { idCasilla = idCasilla, fechaDesde = fechaDesde, fechaHasta = fechaHasta, direccion = direccion });
            }
            else //recibidos
            {
                return RedirectToAction("ReporteEstadisticasMensajesRecepcionadosSwift", new { idCasilla = idCasilla, fechaDesde = fechaDesde, fechaHasta = fechaHasta, direccion = direccion });
            }
        }

        public ActionResult EstadisticasMensajesEnviadosSwift()
        {
            IEnumerable<sw_casillas> casillas = GetCasillasDeCacheOBL().OrderBy(i => i.cod_casilla);
            SelectList todasLasCasillas = new SelectList(casillas, "cod_casilla", "DataTextField");

            Index model = new Index();
            model.TodasLasCasillas = todasLasCasillas;
            model.PageSize = short.Parse(ConfigurationManager.AppSettings[ConfigTamanioPaginaGridBusqueda]);

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains(CookieName))
            {
                //leo las casillas visibles y la casilla default de la cookie
                NameValueCollection collection = this.ControllerContext.HttpContext.Request.Cookies[CookieName].Values;
                model.IdsCasillasVisibles = collection[CookieValueCasillasVisibles].Split(new char[] { ',' }).ToList();

                IEnumerable<sw_casillas> casillasVisibles = casillas.Where(c => model.IdsCasillasVisibles.Contains(c.cod_casilla.ToString())).ToList();
                model.IdCasillaDefault = collection[CookieValueCasillaDefault];
                int valorDefault = 0;
                int.TryParse(model.IdCasillaDefault, out valorDefault);

                model.CasillasVisibles = new SelectList(casillasVisibles, "cod_casilla", "DataTextField", valorDefault);
            }

            return View(model);
        }

        public ActionResult ReporteEstadisticasMensajesEnviadosSwift(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, byte direccion)
        {
            fechaDesde = fechaDesde.Date;
            fechaHasta = fechaHasta.Date;
            string _nomCasilla;

            var result = swgcService.GetEstadisticasEnviados(idCasilla, fechaDesde, fechaHasta);

            var group = from p in result
                        group p by p.casilla into grupo
                        select grupo;

            IList<sw_casillas> casillas = GetCasillasDeCacheOBL1().OrderBy(i => i.cod_casilla).ToList();
            sw_casillas casilla = idCasilla != 0 ? casillas.Where(c => c.cod_casilla == idCasilla).FirstOrDefault() : casillas.FirstOrDefault();
            _nomCasilla = idCasilla != 0 ? casilla.nombre_casilla : "Todas";

            int num = 0;
            foreach (var item in result)
            {
                _cod_banco_em = item.cod_banco_em;
                _branch_em = item.branch_em;
                _nombre_banco1 = item.nombre_banco;
                int sum = item.cantidad + num;
                break;
            }

            foreach (var item1 in result)
            {
                sum = item1.cantidad + num;
                num = sum;
            }

            EstadisticasMensajesEnviadosSwiftViewModel model1 = new EstadisticasMensajesEnviadosSwiftViewModel()
            {
                Casilla = idCasilla,
                NombreCasilla = _nomCasilla,
                FechaDesde = fechaDesde,
                FechaHasta = fechaHasta,
                CodBancoEm = _cod_banco_em,
                BranchEm = _branch_em,
                NombreBanco = _nombre_banco1,
                Suma = sum,
                RegistrosEnviados = group
            };
            return View(model1);
        }
        public ActionResult ReporteEstadisticasMensajesRecepcionadosSwift(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, byte direccion)
        {
            fechaDesde = fechaDesde.Date;
            fechaHasta = fechaHasta.Date;
            string _nomCasilla;

            var result = swgcService.GetEstadisticasRecibidos(idCasilla, fechaDesde, fechaHasta);

            var group = from p in result
                        group p by p.casilla into grupo
                        select grupo;

            IList<sw_casillas> casillas = GetCasillasDeCacheOBL1().OrderBy(i => i.cod_casilla).ToList();
            sw_casillas casilla = idCasilla != 0 ? casillas.Where(c => c.cod_casilla == idCasilla).FirstOrDefault() : casillas.FirstOrDefault();
            _nomCasilla = idCasilla != 0 ? casilla.nombre_casilla : "Todas";

            int num1 = 0;
            foreach (var item in result)
            {
                _cod_banco_rec = item.cod_banco_rec;
                _nombre_banco = item.nombre_banco;
                break;
            }
            foreach (var item1 in result)
            {
                sum = item1.cantidad + num1;
                num1 = sum;
            }

            EstadisticasMensajesRecepcionadosSwiftViewModel model1 = new EstadisticasMensajesRecepcionadosSwiftViewModel()
            {
                Casilla = idCasilla,
                NombreCasilla = _nomCasilla,
                FechaDesde = fechaDesde,
                FechaHasta = fechaHasta,
                Cod_Banco_Rec = _cod_banco_rec,
                Nombre_Banco = _nombre_banco,
                Suma = sum,
                RegistrosRecibidos = group
            };
            return View(model1);
        }

        public ActionResult EstadisticasSwifts02(int idCasilla, DateTime fechaDesde, DateTime fechaHasta)
        {
            fechaDesde = fechaDesde.Date; //para sacar cualquier componente de hora:min que me pudiera venir de js
            fechaHasta = fechaHasta.Date;
            byte direccion = 2;
            JsonResult jsonResult;

            try
            {
                var result = bl.GetEstadisticasMensajesPorCasilla(idCasilla, fechaDesde, fechaHasta, (direccion != 2));

                jsonResult = new JsonResult()
                {
                    Data = result,
                    MaxJsonLength = Int32.MaxValue,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception)
            {
                jsonResult = new JsonResult()
                {
                    Data = string.Empty,
                    MaxJsonLength = Int32.MaxValue,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            return jsonResult;
        }

        public ActionResult EncasillamientoAutomaticoSwift()
        {
            IEnumerable<sw_casillas> casillas = GetCasillasDeCacheOBL().OrderBy(i => i.cod_casilla);
            SelectList todasLasCasillas = new SelectList(casillas, "cod_casilla", "DataTextField");

            Index model = new Index();
            model.TodasLasCasillas = todasLasCasillas;
            model.PageSize = short.Parse(ConfigurationManager.AppSettings[ConfigTamanioPaginaGridBusqueda]);

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains(CookieName))
            {
                //leo las casillas visibles y la casilla default de la cookie
                NameValueCollection collection = this.ControllerContext.HttpContext.Request.Cookies[CookieName].Values;
                model.IdsCasillasVisibles = collection[CookieValueCasillasVisibles].Split(new char[] { ',' }).ToList();

                IEnumerable<sw_casillas> casillasVisibles = casillas.Where(c => model.IdsCasillasVisibles.Contains(c.cod_casilla.ToString())).ToList();
                model.IdCasillaDefault = collection[CookieValueCasillaDefault];
                int valorDefault = 0;
                int.TryParse(model.IdCasillaDefault, out valorDefault);

                model.CasillasVisibles = new SelectList(casillasVisibles, "cod_casilla", "DataTextField", valorDefault);
            }
            return View(model);
        }

        public JsonResult GetTodosEncasillamientoAutomatico(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, string _codEstado)
        {
            var result = swgcService.GetTodosEncasillamientoAutomatico(idCasilla, fechaDesde, fechaHasta);
            return Json(result.Select(i => new
            {
                Casilla = i.casilla,
                Secuencia = i.secuencia,
                Sesion = i.sesion,
                Tipo = i.tipo_msg,
                Estado = i.estado_msg,
                FechaRecepcion = i.fecha_send + " " + i.hora_send,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                Moneda = i.cod_moneda,
                Monto = i.monto
            }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTodosMensajesEnviadosExterior(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var result = swgcService.GetTodosMensajesEnviadosExterior(idCasilla, fechaInicio, fechaFin);
            return Json(result.Select(i => new
            {
                Casilla = i.casilla,
                Secuencia = i.secuencia,
                Sesion = i.sesion,
                Tipo = i.tipo_msg,
                Estado = i.estado_msg,
                FechaRecepcion = i.fecha_env + " " + i.hora_env,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                Moneda = i.cod_moneda,
                Monto = i.monto
            }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult MensajesEnviadosExteriorSwift()
        {
            IEnumerable<sw_casillas> casillas = GetCasillasDeCacheOBL().OrderBy(i => i.cod_casilla);
            SelectList todasLasCasillas = new SelectList(casillas, "cod_casilla", "DataTextField");

            Index model = new Index();
            model.TodasLasCasillas = todasLasCasillas;
            model.PageSize = short.Parse(ConfigurationManager.AppSettings[ConfigTamanioPaginaGridBusqueda]);

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains(CookieName))
            {
                //leo las casillas visibles y la casilla default de la cookie
                NameValueCollection collection = this.ControllerContext.HttpContext.Request.Cookies[CookieName].Values;
                model.IdsCasillasVisibles = collection[CookieValueCasillasVisibles].Split(new char[] { ',' }).ToList();

                IEnumerable<sw_casillas> casillasVisibles = casillas.Where(c => model.IdsCasillasVisibles.Contains(c.cod_casilla.ToString())).ToList();
                model.IdCasillaDefault = collection[CookieValueCasillaDefault];
                int valorDefault = 0;
                int.TryParse(model.IdCasillaDefault, out valorDefault);

                model.CasillasVisibles = new SelectList(casillasVisibles, "cod_casilla", "DataTextField", valorDefault);
            }
            return View(model);
        }


        public ActionResult ReporteMensajesEnviadosExteriorSwift(int idCasilla, DateTime fechaDesde, DateTime fechaHasta)
        {
            fechaDesde = fechaDesde.Date;
            fechaHasta = fechaHasta.Date;
            string _nomCasilla = string.Empty;

            var result = swgcService.GetTodosMensajesEnviadosExterior(idCasilla, fechaDesde, fechaHasta);

            IList<sw_casillas> casillas = GetCasillasDeCacheOBL1().OrderBy(i => i.cod_casilla).ToList();
            sw_casillas casilla = casillas.Where(c => c.cod_casilla == idCasilla).FirstOrDefault();
            _nomCasilla = casilla.nombre_casilla;

            ReporteMensajesEnviadosExteriorSwiftViewModel model = new ReporteMensajesEnviadosExteriorSwiftViewModel()
            {
                Casilla = idCasilla,
                NombreCasilla = _nomCasilla,
                FechaDesde = fechaDesde,
                FechaHasta = fechaHasta,
                Registros = result
            };
            return View(model);
        }


        public ActionResult GetTodosMensajesEnviadosExterior02(int idCasilla, DateTime fechaDesde, DateTime fechaHasta)
        {
            using (var tracer = new Tracer("Get Todos Mensajes Enviados Exterior 02"))
            {
                fechaDesde = fechaDesde.Date; //para sacar cualquier componente de hora:min que me pudiera venir de js
                fechaHasta = fechaHasta.Date;
                JsonResult jsonResult;

                try
                {
                    var result = swgcService.GetTodosMensajesEnviadosExterior(idCasilla, fechaDesde, fechaHasta);

                    jsonResult = new JsonResult()
                    {
                        Data = result,
                        MaxJsonLength = Int32.MaxValue,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                catch (Exception _e)
                {
                    tracer.TraceException("Alerta, ", _e);
                    jsonResult = new JsonResult()
                    {
                        Data = string.Empty,
                        MaxJsonLength = Int32.MaxValue,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }

                return jsonResult; 
            }
        }
        public ActionResult ReporteEncasillamientoAutomaticoSwift(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, string _codEstado)
        {
            fechaDesde = fechaDesde.Date;
            fechaHasta = fechaHasta.Date;
            string _nomCasilla;

            if (_codEstado == "0")
            { _resultado = "Todos"; }
            if (_codEstado == "ENC")
            { _resultado = "Encasillados"; }
            if (_codEstado == "CNF")
            { _resultado = "Confirmados"; }
            if (_codEstado == "IMP")
            { _resultado = "Impresos"; }

            IEnumerable<proc_sw_log_trae_aut_rangoDTO> result = swgcService.GetTodosEncasillamientoAutomatico(idCasilla, fechaDesde, fechaHasta);

            if (_codEstado != "0")
            {
                result = result.Where(c => c.estado_msg == _codEstado);
            }

            IList<sw_casillas> casillas = GetCasillasDeCacheOBL1().OrderBy(i => i.cod_casilla).ToList();
            sw_casillas casilla = idCasilla != 0 ? casillas.Where(c => c.cod_casilla == idCasilla).FirstOrDefault() : casillas.FirstOrDefault();
            _nomCasilla = idCasilla != 0 ? casilla.nombre_casilla : "Todas";

            ReporteEncasillamientoAutomaticoSwiftViewModel model = new ReporteEncasillamientoAutomaticoSwiftViewModel()
            {
                Casilla = idCasilla,
                NombreCasilla = _nomCasilla,
                FechaDesde = fechaDesde,
                FechaHasta = fechaHasta,
                Estado = _resultado,
                Registros = result.ToList()
            };
            return View(model);
        }
        public ActionResult ReporteEncasillamientoManualSwift(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, string _codEstado)
        {
            fechaDesde = fechaDesde.Date;
            fechaHasta = fechaHasta.Date;
            string _nomCasilla;

            if (_codEstado == "0")
            { _resultado = "Todos"; }
            if (_codEstado == "ENC")
            { _resultado = "Encasillados"; }
            if (_codEstado == "CNF")
            { _resultado = "Confirmados"; }
            if (_codEstado == "IMP")
            { _resultado = "Impresos"; }
            if (_codEstado == "REE")
            { _resultado = "Reenviados"; }

            IEnumerable<proc_sw_log_trae_enc_rangoDTO> result = swgcService.GetTodosEncasillamientoManual(idCasilla, fechaDesde, fechaHasta);

            if (_codEstado != "0")
            {
                result = result.Where(c => c.estado_msg == _codEstado);
            }

            IList<sw_casillas> casillas = GetCasillasDeCacheOBL1().OrderBy(i => i.cod_casilla).ToList();
            sw_casillas casilla = idCasilla != 0 ? casillas.Where(c => c.cod_casilla == idCasilla).FirstOrDefault() : casillas.FirstOrDefault();
            _nomCasilla = idCasilla != 0 ? casilla.nombre_casilla : "Todas";

            ReporteEncasillamientoManualSwiftViewModel model = new ReporteEncasillamientoManualSwiftViewModel()
            {
                Casilla = idCasilla,
                NombreCasilla = _nomCasilla,
                FechaDesde = fechaDesde,
                FechaHasta = fechaHasta,
                Estado = _resultado,
                Registros = result.ToList()
            };
            return View(model);
        }

        public ActionResult ReporteGestionControlSwift(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, string _codEstado)
        {
            ReporteGestionControlSwiftViewModel model = new ReporteGestionControlSwiftViewModel();
            fechaDesde = fechaDesde.Date;
            fechaHasta = fechaHasta.Date;
            string _nomCasilla = "";

            if (_codEstado == "0")
            { _resultado = "Todos"; }
            if (_codEstado == "ENC")
            { _resultado = "Encasillados"; }
            if (_codEstado == "CNF")
            { _resultado = "Confirmados"; }
            if (_codEstado == "IMP")
            { _resultado = "Impresos"; }
            if (_codEstado == "REE")
            { _resultado = "Reenviados"; }
            if (_codEstado == "SEN")
            { _resultado = "Sin Encasilla"; }
            if (_codEstado == "REC")
            { _resultado = "Rechazados"; }
            if (_codEstado == "PDU")
            { _resultado = "Posibles Duplicados"; }

            //Busca por todas las casillas.
            var result = swgcService.GetTodos(idCasilla, fechaDesde, fechaHasta, _codEstado);

            //Ordena el listado por Casillas.
            IList<sw_casillas> casillas = GetCasillasDeCacheOBL1().OrderBy(i => i.cod_casilla).ToList();
            //Buscar casillas. 
            sw_casillas casilla = idCasilla != 0 ? casillas.Where(c => c.cod_casilla == idCasilla).FirstOrDefault() : casillas.FirstOrDefault();
            _nomCasilla = idCasilla != 0 ? casilla.nombre_casilla : "Todas";
            model = new ReporteGestionControlSwiftViewModel()
            {
                Casilla = idCasilla,
                NombreCasilla = _nomCasilla,
                FechaDesde = fechaDesde,
                FechaHasta = fechaHasta,
                Estado = _resultado,
                Registros = result
            };
            return View(model);

        }

        public ActionResult VentanaTodosPorRango(int _globalSesion, int _globalSecuencia, int _globalSecuenciaFinal)
        {

            ViewBag._globalSesion = _globalSesion;
            ViewBag._globalSecuencia = _globalSecuencia;
            ViewBag._globalSecuenciaFinal = _globalSecuenciaFinal;
            return View();
        }


        public JsonResult Get_proc_sw_env_trae_files(DateTime fechaDesde, DateTime fechaHasta)
        {
            var result = swgcService.Getproc_sw_env_trae_files(fechaDesde, fechaHasta);
            return Json(result.Select(i => new
            {
                fd_archivo = i.fd_archivo,
                nombre_archivo = i.nombre_archivo,
                fecha_creacion = i.fecha_creacion,
                fecha_confirma = i.fecha_confirma,
                estado_archivo = i.estado_archivo,
                total_mensajes = i.total_mensajes,
                total_envios = i.total_envios,
                total_rechazos = i.total_rechazos
            }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get_proc_sw_env_trae_files02(DateTime fechaDesde, DateTime fechaHasta)
        {
            var result = swgcService.Getproc_sw_env_trae_files02(fechaDesde, fechaHasta);
            return Json(result.Select(i => new
            {
                fd_archivo = i.fd_archivo,
                nombre_archivo = i.nombre_archivo,
                fecha_creacion = i.fecha_creacion,
                fecha_confirma = i.fecha_confirma,
                estado_archivo = i.estado_archivo,
                total_mensajes = i.total_mensajes,
                total_envios = i.total_envios,
                total_rechazos = i.total_rechazos,
                campo1 = i.campo1
            }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ControlEnvioMensajeriaSwift()
        {
            return View();
        }

        public ActionResult ReporteControlEnvioMensajeriaSwift(DateTime fechaDesde, DateTime fechaHasta)
        {
            fechaDesde = fechaDesde.Date;
            fechaHasta = fechaHasta.Date;
            var result = swgcService.Getproc_sw_env_trae_files02(fechaDesde, fechaHasta);

            ReporteControlEnvioMensajeriaSwiftViewModel model = new ReporteControlEnvioMensajeriaSwiftViewModel()
            {
                FechaDesde = fechaDesde,
                FechaHasta = fechaHasta,
                Registros = result
            };
            return View(model);
        }

        public ActionResult ReporteControlEnvioMensajeriaSwiftt(DateTime fechaDesde, DateTime fechaHasta)
        {
            fechaDesde = fechaDesde.Date;
            fechaHasta = fechaHasta.Date;
            var result = swgcService.Getproc_sw_env_trae_files02(fechaDesde, fechaHasta);

            ReporteControlEnvioMensajeriaSwifttViewModel model = new ReporteControlEnvioMensajeriaSwifttViewModel()
            {
                FechaDesde = fechaDesde,
                FechaHasta = fechaHasta,
                Registros = result
            };
            return View(model);
        }
        public ActionResult ReporteControlRecepcionMensajesSwift(int idCasilla, DateTime fechaDesde)
        {
            fechaDesde = fechaDesde.Date;
            DateTime fechaHasta = fechaDesde.Date;
            var result = swgcService.proc_sw_rec_trae_resumen_msg(fechaDesde, fechaHasta);
            var group = from p in result
                        group p by p.casilla into grupo
                        select grupo;

            ReporteControlRecepcionMensajesSwiftViewModel model = new ReporteControlRecepcionMensajesSwiftViewModel()
            {
                FechaDesde = fechaDesde,
                FechaHasta = fechaHasta,
                Registros = group
            };
            return View(model);
        }

        [HttpPost, HandleAjaxException]
        public ActionResult GuardarTemporalmenteMensajesNoRecepcionados(List<proc_sw_rec_controlDTO> mensajesNoRecepcionados)
        {
            using (var tracer = new Tracer("Guardar Temporalmente Mensajes No Recepcionados"))
            {
                try
                {
                    if (mensajesNoRecepcionados == null)
                    {
                        return Json(new
                        {
                            idCodEstado = 0,
                            Estado = false
                        });
                    }

                    this.ControllerContext.HttpContext.Session[SessionKeys.GestionControlSwift.MensajesNoRecepcionadosKey] = mensajesNoRecepcionados;
                    return Json(new
                    {
                        idCodEstado = 1,
                        Estado = true
                    });
                }
                catch (Exception _e)
                {
                    tracer.TraceException("Alerta", _e);
                    throw;
                } 
            }
        }

        public ActionResult ReporteControlRecepcionNoMensajesSwift(DateTime fechaDesde)
        {
            if (this.HttpContext.Session[SessionKeys.GestionControlSwift.MensajesNoRecepcionadosKey] != null)
            {
                List<proc_sw_rec_controlDTO> msgsNoRecepcionados = this.HttpContext.Session[SessionKeys.GestionControlSwift.MensajesNoRecepcionadosKey] as List<proc_sw_rec_controlDTO>;

                int _rowNum = 0;
                int _sumaMensaje = 0;
                ReporteControlRecepcionNoMensajesSwiftViewModel model = new ReporteControlRecepcionNoMensajesSwiftViewModel()
                {
                    FechaDesde = fechaDesde,
                    FechaHasta = fechaDesde,
                    Registros = msgsNoRecepcionados
                };

                foreach (var item in model.Registros)
                {
                    _rowNum = _rowNum + 1;
                    _sumaMensaje = +_rowNum;
                    item.num = _rowNum;
                }

                model.SumaMensaje = _sumaMensaje;
                return View(model);
            }
            else
            {
                return null;
            }
        }

        public string ObtSecPorSec(int sec, DateTime fecha)
        {
            string retorno = swgcService.ObtSecPorSec(sec, fecha);
            return retorno;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarCasillas(List<string> idsCasillasVisibles, string idCasillaDefault)
        {
            HttpCookie cookie = new HttpCookie(CookieName);
            cookie.Expires.AddDays(365);
            if (idsCasillasVisibles != null)
            {
                cookie.Values.Add(CookieValueCasillasVisibles, String.Join(",", idsCasillasVisibles.ToArray()));
            }
            else
            {
                cookie.Values.Add(CookieValueCasillasVisibles, "");
            }

            cookie.Values.Add(CookieValueCasillaDefault, idCasillaDefault);

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains(CookieName))
            {
                this.ControllerContext.HttpContext.Response.SetCookie(cookie);
            }
            else
            {
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }

            return Json(String.Empty);
        }
    }
}
