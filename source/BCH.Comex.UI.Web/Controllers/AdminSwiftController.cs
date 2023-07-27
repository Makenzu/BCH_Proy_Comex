using BCH.Comex.Common;
using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.BL.SWAU;
using BCH.Comex.Core.BL.SWCE;
using BCH.Comex.Core.BL.SWEM;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.UI.Web.Areas.EnvioSwift.Models;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using BCH.Comex.UI.Web.Models;
using BCH.Comex.UI.Web.Models.AdministracionSwift;
using BCH.Comex.UI.Web.Models.AutorizacionSwift;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FormatUtils = BCH.Comex.UI.Web.Common.FormatUtils;

namespace BCH.Comex.UI.Web.Controllers
{
    public class AdminSwiftController : BCH.Comex.UI.Web.Common.BaseController
    {
        private SwiftMgr bl;
        private SwceService swceService;
        private SwauService swauService;
        private const string CookieName = "BCHComexSwem_Casillas";
        private const string CookieValueCasillasVisibles = "CasillasVisibles";
        private const string CookieValueCasillaDefault = "CasillaDefault";
        private const string CacheCasillasSwiftTodas = "CasillasSwiftTodas";
        private const string CacheTipoCamposConMaximo = "TipoCamposConMaximo";
        private const string CacheCampos = "Campos";
        private const string ConfigTamanioPaginaGridBusqueda = "ConsultaSwift.TamanioPaginaGridBusqueda";
        private const string ConfigCuerpoEmail = "ConsultaSwift.CuerpoEmailMensaje";
        private const string CacheCuerpoEmail = "CuerpoEmailMensaje";

        static AdminSwiftController()
        {
            new PortalService().RegisterApp("SWCE", "Administración de Envío Automático Swift", "SWIFT",
                Constantes.AppRoles.AdminSwiftAppRole, "COMEX_GRP_SWIFT", "AdminSwift");
        }

        public AdminSwiftController()
        {
            this.bl = new SwiftMgr();
            this.swceService = new SwceService();
            this.swauService = new SwauService();
        }

        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AdminSwiftAppRole)]
        public ActionResult Index()
        {
            IEnumerable<sw_casillas> casillas = GetCasillasDeCacheOBL();
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
        public ActionResult GestionArchivos()
        {
         
            return View();
        }
        public ActionResult EliminaMensaje()
        {

            return View();
        }
        public ActionResult ConsultaPoderes()
        {

            return View();
        }

        public ActionResult ReporteAdminSwiftNulos(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, string estado)
        {
            ReporteAdminSwiftNulosViewModel model = new ReporteAdminSwiftNulosViewModel();
            fechaDesde = fechaDesde.Date;
            fechaHasta = fechaHasta.Date;
            string _nomCasilla = "";

            if (estado == "nulos")
            {
                var result = swceService.GetNulos(idCasilla, fechaDesde, fechaHasta);

                IList<sw_casillas> casillas = GetCasillasDeCacheOBL().OrderBy(i => i.cod_casilla).ToList();

                sw_casillas casilla = idCasilla != 0 ? casillas.Where(c => c.cod_casilla == idCasilla).FirstOrDefault() : casillas.FirstOrDefault();
                _nomCasilla = idCasilla != 0 ? casilla.nombre_casilla : "Todas";

                model = new ReporteAdminSwiftNulosViewModel()
                {
                    Casilla = idCasilla,
                    NombreCasilla = _nomCasilla,
                    FechaDesde = fechaDesde,
                    FechaHasta = fechaHasta,
                    Registros = result
                };
                return View(model);

            }
            else
            {
                return null;
            }
        }

        public ActionResult ReporteAdminSwiftBloqueados(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, string estado)
        {
            ReporteAdminSwiftBloqueadosViewModel model = new ReporteAdminSwiftBloqueadosViewModel();
            fechaDesde = fechaDesde.Date;
            fechaHasta = fechaHasta.Date;
            string _nomCasilla = "";

            if (estado == "bloqueados")
            {
                var result = swceService.GetBloqueados(idCasilla, fechaDesde, fechaHasta);

                IList<sw_casillas> casillas = GetCasillasDeCacheOBL().OrderBy(i => i.cod_casilla).ToList();

                sw_casillas casilla = idCasilla != 0 ? casillas.Where(c => c.cod_casilla == idCasilla).FirstOrDefault() : casillas.FirstOrDefault();
                _nomCasilla = idCasilla != 0 ? casilla.nombre_casilla : "Todas";

                model = new ReporteAdminSwiftBloqueadosViewModel()
                {
                    Casilla = idCasilla,
                    NombreCasilla = _nomCasilla,
                    FechaDesde = fechaDesde,
                    FechaHasta = fechaHasta,
                    Registros = result
                };
                return View(model);

            }
            else
            {
                return null;
            }
        }

        public ActionResult ReporteAdminSwiftProcesados(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, string estado)
        {
            ReporteAdminSwiftProcesadosViewModel model = new ReporteAdminSwiftProcesadosViewModel();
            fechaDesde = fechaDesde.Date;
            fechaHasta = fechaHasta.Date;
            string _nomCasilla = "";

            if (estado == "procesados")
            {
                var result = swceService.GetProcesados(idCasilla, fechaDesde, fechaHasta);

                IList<sw_casillas> casillas = GetCasillasDeCacheOBL().OrderBy(i => i.cod_casilla).ToList();

                sw_casillas casilla = idCasilla != 0 ? casillas.Where(c => c.cod_casilla == idCasilla).FirstOrDefault() : casillas.FirstOrDefault();
                _nomCasilla = idCasilla != 0 ? casilla.nombre_casilla : "Todas";

                model = new ReporteAdminSwiftProcesadosViewModel()
                {
                    Casilla = idCasilla,
                    NombreCasilla = _nomCasilla,
                    FechaDesde = fechaDesde,
                    FechaHasta = fechaHasta,
                    Registros = result
                };
                return View(model);

            }
            else
            {
                return null;
            }
        }

        public ActionResult ReporteAdminSwiftRechazados(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, string estado)
        {
            ReporteAdminSwiftRechazadosViewModel model = new ReporteAdminSwiftRechazadosViewModel();
            fechaDesde = fechaDesde.Date;
            fechaHasta = fechaHasta.Date;
            string _nomCasilla = "";

            if (estado == "rechazados")
            {
                var result = swceService.GetRechazados(idCasilla, fechaDesde, fechaHasta);

                IList<sw_casillas> casillas = GetCasillasDeCacheOBL().OrderBy(i => i.cod_casilla).ToList();

                sw_casillas casilla = idCasilla != 0 ? casillas.Where(c => c.cod_casilla == idCasilla).FirstOrDefault() : casillas.FirstOrDefault();
                _nomCasilla = idCasilla != 0 ? casilla.nombre_casilla : "Todas";

                model = new ReporteAdminSwiftRechazadosViewModel()
                {
                    Casilla = idCasilla,
                    NombreCasilla = _nomCasilla,
                    FechaDesde = fechaDesde,
                    FechaHasta = fechaHasta,
                    Registros = result
                };
                return View(model);

            }
            else
            {
                return null;
            }
        }

        public ActionResult ReporteAdminSwiftAutorizados(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, string estado)
        {
            ReporteAdminSwiftAutorizadosViewModel model = new ReporteAdminSwiftAutorizadosViewModel();
            fechaDesde = fechaDesde.Date;
            fechaHasta = fechaHasta.Date;
            string _nomCasilla = "";

            if (estado == "autorizados")
            {
                var result = swceService.GetAutorizados(idCasilla, fechaDesde, fechaHasta);

                IList<sw_casillas> casillas = GetCasillasDeCacheOBL().OrderBy(i => i.cod_casilla).ToList();

                sw_casillas casilla = idCasilla != 0 ? casillas.Where(c => c.cod_casilla == idCasilla).FirstOrDefault() : casillas.FirstOrDefault();
                _nomCasilla = idCasilla != 0 ? casilla.nombre_casilla : "Todas";

                model = new ReporteAdminSwiftAutorizadosViewModel()
                {
                    Casilla = idCasilla,
                    NombreCasilla = _nomCasilla,
                    FechaDesde = fechaDesde,
                    FechaHasta = fechaHasta,
                    Registros = result
                };
                return View(model);

            }
            else
            {
                return null;
            }
        }

        public ActionResult ReporteAdminSwiftSinAutorizar(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, string estado)
        {
            ReporteAdminSwiftSinAutorizarViewModel model = new ReporteAdminSwiftSinAutorizarViewModel();
            fechaDesde = fechaDesde.Date;
            fechaHasta = fechaHasta.Date;
            string _nomCasilla = "";

            if (estado == "sinAutorizar")
            {
                var result = swceService.Get(idCasilla, fechaDesde, fechaHasta);

                IList<sw_casillas> casillas = GetCasillasDeCacheOBL().OrderBy(i => i.cod_casilla).ToList();

                sw_casillas casilla = idCasilla != 0 ? casillas.Where(c => c.cod_casilla == idCasilla).FirstOrDefault() : casillas.FirstOrDefault();
                _nomCasilla = idCasilla != 0 ? casilla.nombre_casilla : "Todas";

                model = new ReporteAdminSwiftSinAutorizarViewModel()
                {
                    Casilla = idCasilla,
                    NombreCasilla = _nomCasilla,
                    FechaDesde = fechaDesde,
                    FechaHasta = fechaHasta,
                    Registros = result
                };
                return View(model);

            }
            else
            {
                return null;
            }
        }
        
        public ActionResult ReporteGestionArchivos(string estado, DateTime fechaInicio, DateTime fechaFin)
        {

            if (estado == "T")
            {
                var result = swceService.GetNoTransmitidos(fechaInicio, fechaFin);
                var listado = from p in result
                              select new DetalleReporteArchivosModel()
                               {
                                   fd_archivo = p.fd_archivo,
                                   estado_archivo = p.estado_archivo,
                                   fecha_confirma = p.fecha_confirma,
                                   fecha_creacion = p.fecha_creacion,
                                   nombre_archivo = p.nombre_archivo,
                                   total_envios = p.total_envios,
                                   total_mensajes = p.total_mensajes,
                                   total_rechazos = p.total_rechazos,
                               };
              int countlistado=listado.Count();
                var reporte = new ReporteArchivosModel()
                {
                    Total="Total de archivos: "+ countlistado,
                    Titulo = "Listado de archivos automáticos.",
                    Detalle = listado,
                    Archivos = "No Transmitidos"
                };

                return View(reporte);

            }

            if (estado == "R")
            {
                var result = swceService.GetRecepcionados(fechaInicio, fechaFin);
                var listado = from p in result
                              select new DetalleReporteArchivosModel()
                              {
                                  fd_archivo = p.fd_archivo,
                                  estado_archivo = p.estado_archivo,
                                  fecha_confirma = p.fecha_confirma,
                                  fecha_creacion = p.fecha_creacion,
                                  nombre_archivo = p.nombre_archivo,
                                  total_envios = p.total_envios,
                                  total_mensajes = p.total_mensajes,
                                  total_rechazos = p.total_rechazos
                              };
                int countlistado = listado.Count();
                var reporte = new ReporteArchivosModel()
                {
                    Total = "Total de archivos: " + countlistado,
                    Titulo = "Listado de archivos automáticos.",
                    Detalle = listado,
                    Archivos = "Recepcionados"
                };

                return View(reporte);
            }

            else
            {
                var resultPendientes = swceService.GetPendientes();
                var listadoPendientes = from p in resultPendientes
                                        select new DetalleReporteArchivosModel()
                                        {
                                            fd_archivo = p.fd_archivo,
                                            estado_archivo = p.estado_archivo,
                                            fecha_confirma = p.fecha_confirma,
                                            fecha_creacion = p.fecha_creacion,
                                            nombre_archivo = p.nombre_archivo,
                                            total_envios = p.total_envios,
                                            total_mensajes = p.total_mensajes,
                                            total_rechazos = p.total_rechazos
                                        };
                int countlistadoPendientes = listadoPendientes.Count();
                var reporte = new ReporteArchivosModel()
                {
                    Total = "Total de archivos: " + countlistadoPendientes,
                    Titulo = "Listado de archivos automáticos.",
                    Detalle = listadoPendientes,
                    Archivos = "Pendientes"
                };

                return View(reporte);
            }
        }
   
           
        public ActionResult Parear(int idArchivo)
        {
            ParearViewModel model = new ParearViewModel()
            {
                IdArchivo = idArchivo,
                DetalleArchivo = swceService.GetDetalleArchivo(idArchivo)
            };

            if(model.DetalleArchivo != null)
            {
                proc_sw_env_trae_detfile_MS_Result primeroProcesado = model.DetalleArchivo.ToList().OrderBy(x => x.fecha_creacion).Where(x => x.estado_msg == "PRO").FirstOrDefault();
                if (primeroProcesado != null)
                {
                    model.MensajesNop = swceService.GetNop(primeroProcesado.fecha_creacion.Date, DateTime.Now);
                }
            }
            
            return View(model);
        }
        public ActionResult Rechazar(int idMensaje)
        {
            ViewBag.IdMensaje = idMensaje;
            return View();
        }
        public ActionResult Bloquear(int idMensaje)
        {
            ViewBag.IdMensaje = idMensaje;
            return View();
        }

        public ActionResult Anular(int idMensaje)
        {
            ViewBag.IdMensaje = idMensaje;
            return View();
        }

        [HttpPost]
        public JsonResult BloquearSwift(int idMensaje, string comentario)
        {
            swceService.SetBloqueados(idMensaje, int.Parse(infoUsuario.Identificacion_CentroDeCostosOriginal), infoUsuario.RutEnFormatoBDSwift, comentario);
            return Json("OK");
        }

        
        [HttpPost, HandleAjaxException]
        public JsonResult AnularSwift(int idMensaje, string comentario)
        {
            var result = swceService.SetNulos(idMensaje, infoUsuario.RutEnFormatoBDSwift, int.Parse(infoUsuario.Identificacion_CentroDeCostosOriginal), comentario);
            return Json(result.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idMensaje"></param>
        /// <returns></returns>
        [HttpPost, HandleAjaxException]
        public JsonResult ValidarAnularSwift(int idMensaje)
        {
            var result = swceService.proc_sw_validaFirmaAnular_MS(idMensaje);
            return Json(result.ToString());
        }

        [HttpPost]
        public JsonResult RechazarSwift(int idMensaje, string comentario)
        {
            var result = swceService.GrabaRechazar(idMensaje, infoUsuario.RutEnFormatoBDSwift, int.Parse(infoUsuario.Identificacion_CentroDeCostosOriginal), comentario);
            return Json(result.ToString());
        }

        [HttpPost]
        public JsonResult Desbloquear(int idMensaje)
        {
            swceService.SetDesbloqueados(idMensaje, int.Parse(infoUsuario.Identificacion_CentroDeCostosOriginal), infoUsuario.RutEnFormatoBDSwift);
            return Json("OK");
        }

        public JsonResult Get(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var result = swceService.Get(idCasilla, fechaInicio, fechaFin);
            GuardarGrillaEnSession(idCasilla, fechaInicio, fechaFin, SwceService.TipoGrillaAdminSwift.SinAutorizar);
            
            return Json(result.Select(i => new
            {

                id_mensaje = i.id_mensaje,
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg.Trim(),
                Unidad = i.casilla,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                BancoReceptor = i.nombre_banco,
                BancoSwift = (i.cod_banco_rec ?? string.Empty).Trim() + (i.branch_rec ?? string.Empty).Trim(),
                FechaIngreso = i.fecha_ingr + " " + i.hora_ingr
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProcesados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {

            var result = swceService.GetProcesados(idCasilla, fechaInicio, fechaFin);
            GuardarGrillaEnSession(idCasilla, fechaInicio, fechaFin, SwceService.TipoGrillaAdminSwift.Procesados);

            return Json(result.Select(i => new
            {
                id_mensaje = i.id_mensaje,
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg.Trim(),
                Unidad = i.casilla,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                BancoReceptor = i.nombre_banco,
                BancoSwift = (i.cod_banco_rec ?? string.Empty).Trim() + (i.branch_rec ?? string.Empty).Trim(),
                FechaIngreso = i.fecha_ingr + " " + i.hora_ingr,
                RutIngreso = i.rut_ingreso,
                FechaProcesado = i.fecha_pro + " " + i.hora_pro,
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAutorizados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {

            var result = swceService.GetAutorizados(idCasilla, fechaInicio, fechaFin);
            GuardarGrillaEnSession(idCasilla, fechaInicio, fechaFin, SwceService.TipoGrillaAdminSwift.Autorizados);

            return Json(result.Select(i => new
            {
                id_mensaje = i.id_mensaje,
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg.Trim(),
                Unidad = i.casilla,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                BancoReceptor = i.nombre_banco,
                BancoSwift = (i.cod_banco_rec ?? string.Empty).Trim() + (i.branch_rec ?? string.Empty).Trim(),
                FechaIngreso = i.fecha_ingr + " " + i.hora_ingr,
                RutIngreso = i.rut_ingreso,
                FechaAutorizado = i.fecha_apr + " " + i.hora_apr
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRechazados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {

            var result = swceService.GetRechazados(idCasilla, fechaInicio, fechaFin);
            GuardarGrillaEnSession(idCasilla, fechaInicio, fechaFin, SwceService.TipoGrillaAdminSwift.Rechazados);

            return Json(result.Select(i => new
            {
                id_mensaje = i.id_mensaje,
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg.Trim(),
                Unidad = i.casilla,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                BancoReceptor = i.nombre_banco,
                BancoSwift = (i.cod_banco_rec ?? string.Empty).Trim() + (i.branch_rec ?? string.Empty).Trim(),
                CodBancoReceptor = i.cod_banco_rec,
                FechaIngreso = i.fecha_ingr + " " + i.hora_ingr,
                FechaRechazo = i.fecha_rechazo + " " + i.hora_rechazo
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBloqueados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {

            var result = swceService.GetBloqueados(idCasilla, fechaInicio, fechaFin);
            GuardarGrillaEnSession(idCasilla, fechaInicio, fechaFin, SwceService.TipoGrillaAdminSwift.Bloqueados);
            return Json(result.Select(i => new
            {
                id_mensaje = i.id_mensaje,
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg.Trim(),
                Unidad = i.casilla,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                BancoReceptor = i.nombre_banco,
                BancoSwift = (i.cod_banco_rec ?? string.Empty).Trim() + (i.branch_rec ?? string.Empty).Trim(),
                FechaIngreso = i.fecha_ingr + " " + i.hora_ingr,
                RutIngreso = i.rut_ingreso,
                FechaBloqueo = i.fecha_bloq + " " + i.hora_bloq
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNulos(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {

            var result = swceService.GetNulos(idCasilla, fechaInicio, fechaFin);
            GuardarGrillaEnSession(idCasilla, fechaInicio, fechaFin, SwceService.TipoGrillaAdminSwift.Anulados);
            
            return Json(result.Select(i => new
            {
                id_mensaje = i.id_mensaje,
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg.Trim(),
                Unidad = i.casilla,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                BancoReceptor = i.nombre_banco,
                BancoSwift = (i.cod_banco_rec ?? string.Empty).Trim() + ( i.branch_rec ?? string.Empty).Trim(),
                FechaIngreso = i.fecha_ingr + " " + i.hora_ingr,
                RutIngreso = i.rut_ingreso,
                FechaAnulado = i.fecha_anula + " " + i.hora_anula
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetPendientes()
        {

            var result = swceService.GetPendientes();
            return Json(result.Select(i => new
            {
                nombre_archivo = i.nombre_archivo,
                fecha_creacion = i.fecha_creacion.ToString(),
                fecha_confirma = i.fecha_confirma.ToString(),
                total_mensajes = i.total_mensajes,
                total_envios = i.total_envios,
                total_rechazos = i.total_rechazos,
                fd_archivo=i.fd_archivo
              
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRecepcionados(string buscar,DateTime fechaInicio, DateTime fechaFin)
        {
            object data = swceService.GetRecepcionadosCount(buscar,fechaInicio, fechaFin);
            return new JsonResult()
            {
                Data = data,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        private void GuardarGrillaEnSession(int idCasilla, DateTime fechaInicio, DateTime fechaFin, SwceService.TipoGrillaAdminSwift tipo)
        {
            this.ControllerContext.HttpContext.Session[SessionKeys.AdminSwift.IDCasillaKey] = idCasilla;
            this.ControllerContext.HttpContext.Session[SessionKeys.AdminSwift.FecIniKey] = fechaInicio;
            this.ControllerContext.HttpContext.Session[SessionKeys.AdminSwift.FecFinKey] = fechaFin;
            this.ControllerContext.HttpContext.Session[SessionKeys.AdminSwift.TipoGrillaKey] = tipo;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [FileDownload]
        public ActionResult ExportarAExcelUltimaGrillaGenerada()
        {
            var IDCasillaKey = (int)this.ControllerContext.HttpContext.Session[SessionKeys.AdminSwift.IDCasillaKey];
            var FecIniKey = (DateTime)this.ControllerContext.HttpContext.Session[SessionKeys.AdminSwift.FecIniKey];
            var FecFinKey = (DateTime)this.ControllerContext.HttpContext.Session[SessionKeys.AdminSwift.FecFinKey];

            if (IDCasillaKey != null && FecIniKey != null && FecFinKey != null)
            {
                SwceService.TipoGrillaAdminSwift tipo = (SwceService.TipoGrillaAdminSwift)this.ControllerContext.HttpContext.Session[SessionKeys.AdminSwift.TipoGrillaKey];

                MemoryStream ms = this.swceService.GetExcelGrilla(tipo, IDCasillaKey, FecIniKey, FecFinKey);
                return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", tipo.ToString() + ".xlsx");
            }
            else
            {
                throw new ApplicationException("No se puede descargar el Excel, primero debe realizar la consulta para obtener los resultados");
            }
            
        }
        

        [HttpPost, HandleAjaxException]
        public ActionResult SolicitarFirmas(int idMensaje, decimal monto, int? casilla)
        {
            var mensaje = "";
            int cantOri = 0, cantRela = 0;
            if (!bl.ValidaInyecciones(idMensaje, ref cantOri, ref cantRela))
            {
                mensaje = "No es posible firmar el mensaje " + idMensaje;
                if (cantOri > 0)
                {
                    mensaje += " ya que falta inyectar " + cantOri + " cargo" + (cantOri > 1 ? "s" : "") + " de la operación";
                }
                if (cantOri > 0 && cantRela > 0)
                {
                    mensaje += " y además tiene una o más operaciones relacionadas con " + cantRela + " cargo" + (cantRela > 1 ? "s" : "") + " por inyectar";
                }
                else if (cantRela > 0)
                {
                    mensaje += " ya que tiene una o más operaciones relacionadas con " + cantRela + " cargo" + (cantRela > 1 ? "s" : "") + " por inyectar";
                }

                return Json(new { resultado = false, mensaje = mensaje });
            }
            else
            {
                SolicitarFirmasViewModel model = new SolicitarFirmasViewModel();
                model.IdMensaje = idMensaje;
                model.CasillaMensaje = casilla ?? 0;
                model.NecesitaConfirmacion = false; //al parecer en AdminSwift nunca necesita confirmacion
                model.FirmasSolicitadas = bl.GetFirmasDeMensajeEnviado(idMensaje, false);
                model.FirmasLocales = swceService.GetFirmasLocales(infoUsuario.FirmasLocales);
                model.RutAis = infoUsuario.RutEnFormatoBDSwift;
                model.ruta = 321;

                return View("SolFirmas", model);
            }
        }


        public ActionResult GetFirmasSwift(int idMensaje)
        {
           
            var result = bl.GetFirmasDeMensajeEnviado(idMensaje, false);
            return Json(result.Select(i => new
            {
                NombrePersonaFirma = i.NombrePersonaFirma,
                revisa_firma = i.revisa_firma,
                tipo_firma = i.tipo_firma,
                estado_firma=i.estado_firma,
                rut_firma = i.rut_firma

            }).ToList(), JsonRequestBehavior.AllowGet);

            
           
        }
        public ActionResult GetAtributosPersona(int rut)
        {

            var result = bl.GetAtributosPersona(rut);
            return Json(result.Select(i => new
            {
                NombrePersonaFirma = i.NombrePersonaFirma,
                revisa_firma = i.revisa_firma,
                tipo_firma = i.tipo_firma,
                estado_firma = i.estado_firma,
                rut_firma = i.rut_firma

            }).ToList(), JsonRequestBehavior.AllowGet);



        }
        public ActionResult GetUsuariosPorNombre(string nombre)
        {

            var result = bl.GetUsuariosPorNombre(nombre);

            return Json(result.Select(i => new
            {
                Nombre = i.nombre,
                Rut = formatRut(i.rut)
            }).ToList(), JsonRequestBehavior.AllowGet);

        }

        public static string formatRut(string rut)
        {
            int cont = 0;
            string format;
            if (rut.Length == 0)
            {
                return "";
            }
            else
            {
                rut = rut.TrimStart('0');
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                format = "-" + rut.Substring(rut.Length - 1);
                for (int i = rut.Length - 2; i >= 0; i--)
                {
                    format = rut.Substring(i, 1) + format;
                    cont++;
                    if (cont == 3 && i != 0)
                    {
                        format = "." + format;
                        cont = 0;
                    }
                }
                return format;
            }
        }
        
        [HttpGet, HandleAjaxException]
        public ActionResult GetPoderes(string rut)
        {
            rut = rut.Replace(".","").Replace("-", "");
            rut = rut.Substring(0, rut.Length - 1);

            if (!String.IsNullOrEmpty(rut) && rut.Length >= 7)
            {
                int intRut =  FormatUtils.RutEnFormatoBDSwift(rut);
                var result = swceService.GetPoderes(intRut);
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new
                    {
                        Estado = false,
                        JsonRequestBehavior.AllowGet
                    });
                }
            }
            else
            {
                throw new ArgumentException("El rut enviado no tiene la longitud mínima esperada");
            }
        }

        [HandleAjaxException]
        public JsonResult SaveFirmas(SolicitarFirmasViewModel model)
        {
            using (Tracer tracer = new Tracer("SaveFirmas"))
            {
                tracer.TraceVerbose("Comienza serializacion firmas locales");
                string firmasLocalesSerializadas = swceService.SerializarFirmasLocales(model.FirmasLocales);
                if (firmasLocalesSerializadas != infoUsuario.FirmasLocales)
                {
                    IDatosUsuario usuario = HttpContext.GetCurrentUser().GetDatosUsuario(); //leo devuelta DatosUsuario ya que no puedo usar el de session porque fue leído en un context diferente
                    usuario.FirmasLocales = firmasLocalesSerializadas;
                    PortalService portalService = new PortalService();
                    portalService.CambiarFirmasLocales(usuario);
                    ActualizarDatosUsuarioEnCache(usuario);
                }

                List<sw_msgsend_firma> firmasNuevas =  model.FirmasSolicitadas.Where(f => f.estado_firma == "N").ToList();
                foreach (sw_msgsend_firma firma in firmasNuevas)
                {
                    firma.rut_firma = FormatUtils.RutEnFormatoBDSwift(firma.RutFirmaConDigitoVerificador);
                }

                tracer.TraceVerbose("Comienza el Firma anuladas anteriores");
                bool ok = bl.SetFirmasAnuladasAnteriores(model.IdMensaje, infoUsuario.RutEnFormatoBDSwift, DateTime.Now);

                if (ok)
                {
                    tracer.TraceVerbose("Comienza el borrado de firmas");
                    foreach (sw_msgsend_firma firma in model.FirmasEliminadas)
                    {
                        ok = bl.EliminarFirmas(model.IdMensaje, firma.rut_firma);
                    }

                    if (ok)
                    {
                        tracer.TraceVerbose("Comienza el grabado de las firmas");
                        ok = bl.SaveFirmas(model.IdMensaje, model.FirmasSolicitadas, infoUsuario.RutEnFormatoBDSwift);

                        tracer.TraceVerbose("Comienza la actualizacion del mensaje y el estado de las firmas");
                        string comentario = "Mensaje Autorizado por U.swift";

                        bool tieneFirmasNecesarias = false;
                        ok = bl.SaveFirmaEnvAutm(model.IdMensaje, model.CasillaMensaje, infoUsuario.RutEnFormatoBDSwift, DateTime.Now, comentario, out tieneFirmasNecesarias);

                        if (ok)
                        {
                            string mensaje = String.Empty;
                            if (tieneFirmasNecesarias)
                            {
                                mensaje = "El mensaje se autorizó satisfactoriamente.";
                            }
                            return Json(new { TieneFirmasNecesarias= tieneFirmasNecesarias, OK= (ok && tieneFirmasNecesarias), Message = mensaje });
                        }
                        else
                        {
                            throw new Exception("No se ha podido actualizar el mensaje");
                        }
                    }
                    else throw new Exception("No se pudieron eliminar las firmas removidas");
                }
                else throw new Exception("No se pudieron eliminar las firmas nulas anteriores");
            }
        }


        
        public JsonResult GetNoTransmitidos(DateTime fechaInicio, DateTime fechaFin)
        {

            var result = swceService.GetNoTransmitidos(fechaInicio, fechaFin);
            return Json(result.Select(i => new
            {
                nombre_archivo = i.nombre_archivo,
                fecha_creacion = i.fecha_creacion.ToString(),
                fecha_confirma = i.fecha_confirma.ToString(),
                total_mensajes = i.total_mensajes,
                total_envios = i.total_envios,
                total_rechazos = i.total_rechazos,
                fd_archivo=i.fd_archivo

            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetalleArchivo(int idArchivo)
        {

            var result = swceService.GetDetalleArchivo(idArchivo);      
      //i.estado_msg
    
            return Json(result.Select(i => new
            {
                id_mensaje = i.id_mensaje,
                tipo_msg = i.tipo_msg,
                casilla = i.casilla,
                estado_msg = i.estado_msg,
                branch_rec = i.branch_rec,
                banco_rec = (i.cod_banco_rec ?? "").Trim() + (i.branch_rec ?? "").Trim(),
                referencia = i.referencia,
                cod_moneda=i.cod_moneda,                 
                monto = i.monto,
                fecha_creacion = i.fecha_creacion.ToString("dd/MM/yyyy"),
                sesion = i.sesion,
                secuencia = i.secuencia,
                fecha_envio = i.fecha_envio + " "+ i.hora_envio

            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Recepcion(int idArchivo, string nombreArchivo)
        {
            using (var tracer = new Tracer("AdminSwiftController - Recepcion"))
            {
                Index model = new Index();
                try
                {
                    swceService.Recepcion(idArchivo);
                    model.Mensajes.Add(new UI_Message() { Text = "Recepción Exitosa! La recepcion del archivo " + nombreArchivo + " se ha realizado correctamente", Type = TipoMensaje.Correcto, AutoClose = true });
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta al hacer Recepcion", e);
                    model.Mensajes.Add(new UI_Message() { Text = "Ocurrio un problema al realizar la recepción del archivo " + nombreArchivo + "," + e.Message, Type = TipoMensaje.Error, AutoClose = false });
                }
                return Json(new { Model = model });
            }
        }
        [HttpPost]
        public JsonResult Reenvio(int idArchivo)
        {

            swceService.reenvio(idArchivo);
            return Json("OK");
        }

        public JsonResult GetNop(DateTime FechaInicio)
        {
            DateTime FechaFin = DateTime.Now;
            var result = swceService.GetNop(FechaInicio, FechaFin);

            return Json(result.Select(i => new
            {
               sesion= i.sesion,
               tipo_msg= i.tipo_msg,
               banco_rec = (i.cod_banco_rec ?? "").Trim() + (i.branch_rec ?? "").Trim(),
               branch_rec= i.branch_rec,
               referencia= i.referencia,
               cod_moneda=i.cod_moneda,
               monto=i.monto,
               estado_msg=i.estado_msg,
               fecha_pro = i.fecha_pro+" "+ i.hora_pro,
               secuencia= i.secuencia

            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GrabaPareoAceptado(int IdMensaje,int Sesion, int Secuencia, DateTime Fecha)
        {
            swceService.GrabaPareoAceptado(IdMensaje, Sesion, Secuencia, Fecha, infoUsuario.RutEnFormatoBDSwift, int.Parse(infoUsuario.Identificacion_CentroDeCostosOriginal));
            swceService.EliminaDetallePareo(Sesion, Secuencia);
            return Json("OK");
        }

        [HttpPost]
        public JsonResult GrabaPareoRechazado(int IdMensaje, DateTime Fecha,string Texto)
        {
            swceService.GrabaPareoRechazado(IdMensaje, Fecha, infoUsuario.RutEnFormatoBDSwift, int.Parse(infoUsuario.Identificacion_CentroDeCostosOriginal), Texto);
            return Json("OK");
        }

        [HttpPost, HandleAjaxException]
        public JsonResult ObtieneImagenFirma(string rut)
        {
            var result = swceService.GetFirmaImage(rut);
            return Json(result.ImagenString,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMensajeEliminar(int? codigo)
        {
            var result = swceService.GetMensajeEliminar(codigo);
            int indice = 0;

            return Json(result.Select(i => new
            {
                id = indice++,
                casilla = i.casilla,
                descripcion = i.descripcion,
                estado_msg = i.estado_msg,
                fecha_ingreso = i.fecha_ingreso.ToString("dd/MM/yyyy"),
                id_mensaje = i.id_mensaje,
                nombre_casilla = i.nombre_casilla,
                tipo_msg = i.tipo_msg

            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteMensajeSwift(int? codigo)
        {
            bool? result = swceService.EliminaMensaje(codigo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<sw_casillas> GetCasillasDeCacheOBL()
        {

            if (this.ControllerContext.HttpContext.Cache[CacheCasillasSwiftTodas] == null)
            {
                IEnumerable<sw_casillas> result = bl.GetTodasLasCasillas();
                this.ControllerContext.HttpContext.Cache[CacheCasillasSwiftTodas] = result;
                return result;
            }
            else
            {
                return this.ControllerContext.HttpContext.Cache[CacheCasillasSwiftTodas] as IEnumerable<sw_casillas>;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarCasillas(List<string> idsCasillasVisibles, string idCasillaDefault)
        {
            if (idsCasillasVisibles == null)
            {
                return null;
            }

            HttpCookie cookie = new HttpCookie(CookieName);
            cookie.Expires.AddDays(365);
            cookie.Values.Add(CookieValueCasillasVisibles, String.Join(",", idsCasillasVisibles.ToArray()));
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


        // GET: EnvioSwift/Configurar
        public ActionResult Alertas()
        {
            var model = new ConfiguracionAlertasModel(infoUsuario.MinsAlertaAdminEnvioSwift ?? 0);
            return View(model);
        }

        public ActionResult EditarConfiguracionAlertas(short? id)
        {
            id = id ?? 0;

            IDatosUsuario usuario = HttpContext.GetCurrentUser().GetDatosUsuario(); //leo devuelta DatosUsuario ya que no puedo usar el de session porque fue leído en un context diferente
            usuario.MinsAlertaAdminEnvioSwift = id;
            PortalService serviceP = new PortalService();
            serviceP.CambiarMinsAlertaAdminEnvioSwift(usuario);
            ActualizarDatosUsuarioEnCache(usuario);
            this.ControllerContext.HttpContext.Session.Remove(SessionKeys.Common.ConfigAlertas); //invalido el cache de config de alertas
            this.ControllerContext.HttpContext.Session.Remove(SessionKeys.Common.Alertas);
            return RedirectToAction("Index");
        }
    }
}