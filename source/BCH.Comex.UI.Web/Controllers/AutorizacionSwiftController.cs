using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.BL.SWAU;
using BCH.Comex.Core.BL.SWCE;
using BCH.Comex.Core.BL.SWEM;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using BCH.Comex.UI.Web.Models.AutorizacionSwift;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FormatUtils = BCH.Comex.UI.Web.Common.FormatUtils;
using BCH.Comex.Common.Tracing;
using System.IO;
using BCH.Comex.Core.Entities.Cext01.AutorizacionSwift;
using BCH.Comex.UI.Web.Models;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.Common;

namespace BCH.Comex.UI.Web.Controllers
{

    public class AutorizacionSwiftController : BCH.Comex.UI.Web.Common.BaseController
    {
        private SwauService swauService;
        private SwceService swceService;
        protected PortalService serviceP = new PortalService();
        private SwiftMgr bl;
        private const int CasillaDefault = 0;
        private const string CookieName = "BCHComexSwau_Casillas";
        private const string CookieValueCasillasVisibles = "CasillasVisibles";
        private const string CookieValueCasillaDefault = "CasillaDefault";
        private const string CacheCasillasSwiftTodas = "CasillasSwiftTodas";

        static AutorizacionSwiftController()
        {
            new PortalService().RegisterApp("SWAU", "Autorización de mensajes Swift", "SWIFT",
                Constantes.AppRoles.AutorizacionSwiftAppRole, "COMEX_GRP_SWIFT", "AutorizacionSwift");
        }

        public AutorizacionSwiftController()
        {
            this.bl = new SwiftMgr();
            this.swauService = new SwauService();
            this.swceService = new SwceService();
        }

        // GET: AutorizacionSwift
        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public ActionResult Index()
        {
            return View();
        }

        [FileDownload]
        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public FileResult ExportarDevueltos()
        {
            var result = this.swauService.GetDevueltos(CasillaDefault, infoUsuario.RutEnFormatoBDSwift);

            if (result.Count == 0)
            {
                return null;
            }

            MemoryStream msFile = SwauService.GetExportedFile(result.Select(i =>
            {
                DateTime fecha = new DateTime(1900, 1, 1); ;
                DateTime.TryParse(i.fecha_ingr + " " + i.hora_ingr, out fecha);
                return new ResultItem
                {
                    NroMensaje = i.id_mensaje,
                    Tipo = i.tipo_msg,
                    Moneda = i.cod_moneda,
                    Unidad = i.casilla,
                    Monto = i.monto.Value,
                    Referencia = i.referencia,
                    Beneficiario = i.beneficiario,
                    BancoReceptor = i.nombre_banco,
                    FechaIngreso = fecha
                };
            }).ToList(), "AutorizacionSwift");

            return File(msFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AutorizacionSwift.xlsx");
        }

        [FileDownload]
        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public FileResult ExportarFirmasPendientes()
        {
            var result = this.swauService.GetFirmasPendientes(CasillaDefault, infoUsuario.RutEnFormatoBDSwift);

            if (result.Count == 0)
            {
                return null;
            }

            MemoryStream msFile = SwauService.GetExportedFile(result.Select(i =>
            {
                DateTime fecha = new DateTime(1900, 1, 1); ;
                DateTime.TryParse(i.fec_ing + " " + i.hor_ing, out fecha);
                return new ResultItem
                {
                    NroMensaje = i.id_mensaje,
                    Tipo = i.tipo_msg,
                    Moneda = i.cod_moneda,
                    Unidad = i.casilla,
                    Monto = i.monto.Value,
                    Referencia = i.referencia,
                    Beneficiario = i.beneficiario,
                    BancoReceptor = i.nombre_banco,
                    FechaIngreso = fecha
                };
            }).ToList(), "AutorizacionSwift");

            return File(msFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AutorizacionSwift.xlsx");
        }

        [FileDownload]
        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public FileResult ExportarPendientes()
        {
            var result = this.swauService.GetPendientesAutorizacion(CasillaDefault, infoUsuario.RutEnFormatoBDSwift);

            if (result.Count == 0)
            {
                return null;
            }

            MemoryStream msFile = SwauService.GetExportedFile(result.Select(i =>
            {
                DateTime fecha = new DateTime(1900, 1, 1); ;
                DateTime.TryParse(i.fec_ing + " " + i.hor_ing, out fecha);
                return new ResultItem
                {
                    NroMensaje = i.id_mensaje,
                    Tipo = i.tipo_msg,
                    Moneda = i.cod_moneda,
                    Unidad = i.casilla,
                    Monto = i.monto.Value,
                    Referencia = i.referencia,
                    Beneficiario = i.beneficiario,
                    BancoReceptor = i.nombre_banco,
                    FechaIngreso = fecha
                };
            }).ToList(), "AutorizacionSwift");

            return File(msFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AutorizacionSwift.xlsx");
        }

        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public ActionResult SeguimientoMensaje()
        {
            return View();
        }

        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public ActionResult Rechazar(int idMensaje)
        {
            ViewBag.IdMensaje = idMensaje;
            return View();
        }

        [HttpPost]
        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public JsonResult Rechazar(int idMensaje, string comentario)
        {
            ///todo: RUT y Casilla
            swauService.GrabaRechazar(idMensaje, infoUsuario.RutEnFormatoBDSwift, 749, comentario);
            return Json("OK");
        }

        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public ActionResult Devolver(int idMensaje)
        {
            ViewBag.IdMensaje = idMensaje;
            return View();
        }

        [HttpPost]
        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public JsonResult GrabaDevolver(int idMensaje)
        {
            ///todo: RUT y Casilla
            swauService.GrabaDevolver(idMensaje, infoUsuario.RutEnFormatoBDSwift, Convert.ToInt32(infoUsuario.Identificacion_CentroDeCostosOriginal));
            return Json("OK");
        }

        [HttpPost]
        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public JsonResult Autorizar(int? idMensaje)
        {
            if (!idMensaje.HasValue)
            {
                throw new ArgumentException("El valor del parámetro idMensaje no es válido.");
            }

            var mensaje = "";
            int cantOri = 0, cantRela = 0;
            if(!swauService.ValidaInyecciones(idMensaje.Value, ref cantOri, ref cantRela)){
                mensaje = "No es posible firmar el mensaje "+ idMensaje.Value;
                if(cantOri > 0)
                {
                    mensaje += " ya que falta inyectar " + cantOri +" cargo"+ (cantOri > 1 ? "s":"") + " de la operación";
                }
                if(cantOri > 0 && cantRela > 0)
                {
                    mensaje += " y además tiene una o más operaciones relacionadas con " + cantRela + " cargo" + (cantRela > 1 ? "s" : "") + " por inyectar";
                }
                else if(cantRela > 0)
                {
                    mensaje += " ya que tiene una o más operaciones relacionadas con " + cantRela + " cargo" + (cantRela > 1 ? "s" : "") + " por inyectar";
                }
                
                return  Json(new { resultado = false , mensaje = mensaje });
            }
            ///todo: RUT
            ///
            var resultado = swauService.GrabaAutorizacion(idMensaje.Value, infoUsuario.RutEnFormatoBDSwift);
            if (resultado)
            {
                mensaje = "Se autorizó con éxito el mensaje Nº" + idMensaje.Value;
            }
            else
            {
                mensaje = "Antes de firmar el mensaje Nº" + idMensaje.Value + " debe solicitar más firmas para él";
            }
            return Json(new { resultado = resultado, mensaje = mensaje });
        }

        [HttpPost]
        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public JsonResult SetFirmasEliminadas(int idMensaje, int rut, DateTime fecha)
        {
            //bl.SetFirmasEliminadas(idMensaje, rut, fecha);
            bl.EliminarFirmas(idMensaje, rut);
            return Json("OK");
        }

        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public ActionResult GetFirmasSwift(int idMensaje)
        {

            var result = bl.GetFirmasDeMensajeEnviado(idMensaje, true);
            return Json(result.Select(i => new
            {
                NombrePersonaFirma = i.NombrePersonaFirma,
                revisa_firma = i.revisa_firma,
                tipo_firma = i.tipo_firma,
                estado_firma = i.estado_firma,
                rut_firma = i.rut_firma

            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public JsonResult SaveFirma(int idmensaje, string rut, string p_tipo_firma, string p_estado, string p_revfir, string p_avisado)
        {
            bl.SaveFirma(idmensaje, FormatUtils.RutEnFormatoBDSwift(rut), p_tipo_firma, p_estado, p_revfir, infoUsuario.RutEnFormatoBDSwift, DateTime.Now, p_avisado);
            return Json("OK");
        }

        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public JsonResult GetPendientesAutorizacion()
        {
            var result = swauService.GetPendientesAutorizacion(CasillaDefault, infoUsuario.RutEnFormatoBDSwift);
            return Json(result.Select(i => new { 
                NroMensaje = i.id_mensaje, 
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg.Trim(), 
                Unidad = i.casilla, 
                Moneda = i.cod_moneda, 
                Monto = i.monto,
                Referencia = i.referencia, 
                Beneficiario = i.beneficiario, 
                BancoReceptor = i.nombre_banco, 
                FechaIngreso = i.fec_ing + " " + i.hor_ing }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public JsonResult GetDevueltos()
        {
            var result = swauService.GetDevueltos(CasillaDefault, infoUsuario.RutEnFormatoBDSwift);
            return Json(result.Select(i => new
            {
                NroMensaje = i.id_mensaje,
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg.Trim(),
                Unidad = i.casilla,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                BancoReceptor = i.nombre_banco,
                FechaIngreso = i.fecha_ingr + " " + i.hora_ingr
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public JsonResult GetFirmasPendientes()
        {
            var result = swauService.GetFirmasPendientes(CasillaDefault, infoUsuario.RutEnFormatoBDSwift);
            return Json(result.Select(i => new
            {
                NroMensaje = i.id_mensaje,
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg.Trim(),
                Unidad = i.casilla,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                BancoReceptor = i.nombre_banco,
                FechaIngreso = i.fec_ing + " " + i.hor_ing
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public JsonResult GetIngresados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var result = swauService.GetIngresados(idCasilla, fechaInicio, fechaFin);
            return Json(result.Select(i => new
            {
                NroMensaje = i.id_mensaje,
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg.Trim(),
                Casilla = i.casilla,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                BancoReceptor = i.nombre_banco,
                FechaHora = i.fecha_ingr + " " + i.hora_ingr
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public JsonResult GetModificados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var result = swauService.GetModificados(idCasilla, fechaInicio, fechaFin);
            return Json(result.Select(i => new
            {
                NroMensaje = i.id_mensaje,
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg.Trim(),
                Casilla = i.casilla,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                BancoReceptor = i.nombre_banco,
                FechaHora = i.fecha_ingr + " " + i.hora_ingr
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public JsonResult GetEnAprobacion(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var result = swauService.GetEnAprobacion(idCasilla, fechaInicio, fechaFin);
            return Json(result.Select(i => new
            {
                NroMensaje = i.id_mensaje,
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg.Trim(),
                Casilla = i.casilla,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                BancoReceptor = i.nombre_banco,
                FechaHora = i.fecha_ingr + " " + i.hora_ingr
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public JsonResult GetAutorizados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var result = swauService.GetAutorizados(idCasilla, fechaInicio, fechaFin);
            return Json(result.Select(i => new
            {
                NroMensaje = i.id_mensaje,
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg.Trim(),
                Casilla = i.casilla,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                BancoReceptor = i.nombre_banco,
                FechaHora = i.fecha_ingr + " " + i.hora_ingr
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public JsonResult GetProcesados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var result = swauService.GetProcesados(idCasilla, fechaInicio, fechaFin);
            return Json(result.Select(i => new
            {
                NroMensaje = i.id_mensaje,
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg.Trim(),
                Casilla = i.casilla,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                BancoReceptor = i.nombre_banco,
                FechaHora = i.fecha_ingr + " " + i.hora_ingr
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public JsonResult GetEnviados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var result = swauService.GetEnviados(idCasilla, fechaInicio, fechaFin);
            return Json(result.Select(i => new
            {
                NroMensaje = i.id_mensaje,
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg.Trim(),
                Casilla = i.casilla,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                BancoReceptor = i.nombre_banco,
                FechaHora = i.fecha_env + " " + i.hora_env
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public JsonResult GetRechazados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var result = swauService.GetRechazados(idCasilla, fechaInicio, fechaFin);
            return Json(result.Select(i => new
            {
                NroMensaje = i.id_mensaje,
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg.Trim(),
                Casilla = i.casilla,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                BancoReceptor = i.nombre_banco,
                FechaHora = i.fecha_ingr + " " + i.hora_ingr
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public JsonResult GetBloqueados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var result = swauService.GetBloqueados(idCasilla, fechaInicio, fechaFin);
            return Json(result.Select(i => new
            {
                NroMensaje = i.id_mensaje,
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg.Trim(),
                Casilla = i.casilla,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                BancoReceptor = i.nombre_banco,
                FechaHora = i.fecha_ingr + " " + i.hora_ingr
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public JsonResult GetAnulados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var result = swauService.GetAnulados(idCasilla, fechaInicio, fechaFin);
            return Json(result.Select(i => new
            {
                NroMensaje = i.id_mensaje,
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Tipo = i.tipo_msg.Trim(),
                Casilla = i.casilla,
                Moneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                BancoReceptor = i.nombre_banco,
                FechaHora = i.fecha_ingr + " " + i.hora_ingr
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public ActionResult ConsultaMensaje()
        {
            IEnumerable<sw_casillas> casillas = GetCasillasDeCacheOBL().OrderBy(c => c.cod_casilla);
            SelectList todasLasCasillas = new SelectList(casillas, "cod_casilla", "DataTextField");

            var model = new ConsultaModel();
            model.TodasLasCasillas = todasLasCasillas;
            //model.PageSize = short.Parse(ConfigurationManager.AppSettings[ConfigTamanioPaginaGridBusqueda]);

            var datosUsuario = HttpContext.GetCurrentUser().GetDatosUsuario();
            if (!String.IsNullOrEmpty(datosUsuario.BCHComexSwem_Casillas))
            {
                string[] cookieValues = datosUsuario.BCHComexSwem_Casillas.Split('|');
                //el primero es values y el segundo es default
                model.IdsCasillasVisibles = cookieValues[0].Split(',').ToList();
                IEnumerable<sw_casillas> casillasVisibles = casillas.Where(c => model.IdsCasillasVisibles.Contains(c.cod_casilla.ToString())).ToList();
                model.IdCasillaDefault = cookieValues[1];
                int valorDefault = 0;
                int.TryParse(cookieValues[1], out valorDefault);

                model.CasillasVisibles = new SelectList(casillasVisibles, "cod_casilla", "DataTextField", valorDefault);
            }

            return View(model);
        }

        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
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
        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public JsonResult GuardarCasillas(List<string> idsCasillasVisibles, string idCasillaDefault)
        {
            using (var tracer = new Tracer("GuardarCasillas"))
            {
                try
                {
                    string cookie = "{0}|{1}";
                    string casillasVisibles = (idsCasillasVisibles == null ? string.Empty : String.Join(",", idsCasillasVisibles.ToArray()));
                    string casillaDefault = (idCasillaDefault == null ? string.Empty : idCasillaDefault);
                    cookie = String.Format(cookie, casillasVisibles, casillaDefault);
                    var datosUsuario = HttpContext.GetCurrentUser().GetDatosUsuario();
                    datosUsuario.BCHComexSwem_Casillas = cookie;
                    var service = new PortalService();
                    service.CambiarBCHComexSwem_Casillas(datosUsuario);
                    return Json(String.Empty);
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta", ex);
                    throw;
                }
            }
        }

        //
        // GET: AutorizacionSwift/Configurar/
        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public ActionResult Alertas()
        {
            var model = new ConfiguracionAlertasModel(HttpContext.GetCurrentUser().GetDatosUsuario().MinsAlertaEnvioSwift ?? 0);
            return View(model);
        }

        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.AutorizacionSwiftAppRole)]
        public ActionResult EditarConfiguracionAlertas(short? id)
        {
            var mins = id ?? 0;

            var usuario = HttpContext.GetCurrentUser().GetDatosUsuario(); //voy a modificarlo, no lo puedo obtener de cache
            usuario.MinsAlertaEnvioSwift = mins;
            serviceP.CambiarMinsAlertaEnvioSwift(usuario);

            this.ActualizarDatosUsuarioEnCache(usuario); //sobreescribo el cache de usuario
            this.ControllerContext.HttpContext.Session.Remove(SessionKeys.Common.ConfigAlertas); //invalido el cache de config de alertas
            this.ControllerContext.HttpContext.Session.Remove(SessionKeys.Common.Alertas);
            return RedirectToAction("Index", "AutorizacionSwift");
        }

        #region SolFirmas

        [HttpPost, HandleAjaxException]
        public ActionResult SolicitarFirmas(int? idMensaje, decimal monto, int? casilla)
        {
            using (var tracer = new Tracer("SolicitarFirmas"))
            {
                try
                {
                    if (!idMensaje.HasValue)
                    {
                        throw new ArgumentException("El valor del parámetro idMensaje no es válido.");
                    }

                    SolicitarFirmasViewModel model = new SolicitarFirmasViewModel();
                    model.IdMensaje = idMensaje.Value;
                    model.CasillaMensaje = casilla ?? 0;
                    int? retorno = swauService.ConfirmaSiRequiereFirma(idMensaje.Value, infoUsuario.RutEnFormatoBDSwift, Convert.ToDouble(monto));
                    model.NecesitaConfirmacion = (retorno == 0);
                    model.FirmasSolicitadas = bl.GetFirmasDeMensajeEnviado(idMensaje.Value, false);
                    model.FirmasLocales = swceService.GetFirmasLocales(infoUsuario.FirmasLocales);
                    model.RutAis = infoUsuario.RutEnFormatoBDSwift;
                    model.ruta = 123;

                    return View("SolFirmas", model);
                }
                catch (Exception _e)
                {
                    tracer.TraceException("Alerta SolicitarFirmas", _e);
                    throw;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idMensaje"></param>
        /// <param name="monto"></param>
        /// <param name="casilla"></param>
        /// <returns></returns>
        [HttpPost, HandleAjaxException]
        public ActionResult SolicitarMultiplesFirmas(List<int> idMensaje, List<decimal> monto, int casilla)
        {
            using (var tracer = new Tracer("SolicitarMultiplesFirmas"))
            {
                try
                {
                    SolicitarFirmasViewModel model = new SolicitarFirmasViewModel();

                    model.FirmasLocales = swceService.GetFirmasLocales(infoUsuario.FirmasLocales);

                    for (int i = 0; i < idMensaje.Count; i++)
                    {
                        int? _casilla = casilla;
                        int? retorno = swauService.ConfirmaSiRequiereFirma(idMensaje[i], infoUsuario.RutEnFormatoBDSwift, Convert.ToDouble(monto[i]));

                        model.Multiple.Add(new SolicitarFirmasViewModel()
                        {
                            IdMensaje = idMensaje[i],
                            CasillaMensaje = _casilla ?? 0,
                            NecesitaConfirmacion = (retorno == 0),
                            FirmasSolicitadas = bl.GetFirmasDeMensajeEnviado(idMensaje[i], false),
                            FirmasLocales = swceService.GetFirmasLocales(infoUsuario.FirmasLocales),
                            RutAis = infoUsuario.RutEnFormatoBDSwift,
                            ruta = 123
                        });
                    }

                    return View("SolFirmas", model);
                }
                catch (Exception _e)
                {
                    tracer.TraceException("Alerta, SolicitarMultiplesFirmas", _e);
                    throw;
                } 
            }
        }

        public ActionResult GetUsuariosPorNombre(string nombre)
        {

            var result = bl.GetUsuariosPorNombre(nombre);
            return Json(result.Select(i => new
            {
                Nombre = i.nombre,
                Rut = i.rut
            }).ToList(), JsonRequestBehavior.AllowGet);

        }

        [HttpPost, HandleAjaxException]
        public JsonResult ObtieneImagenFirma(string rut)
        {
            using (var tracer = new Tracer("ObtieneImagenFirma"))
            {
                try
                {
                    var result = swceService.GetFirmaImage(rut);
                    return Json(result.ImagenString, JsonRequestBehavior.AllowGet);
                }
                catch (Exception _e)
                {
                    tracer.TraceException("Alerta ObtieneImagenFirma", _e);
                    throw;
                } 
            }
        }

        [HttpGet, HandleAjaxException]
        public ActionResult GetPoderes(string rut)
        {
            if (!String.IsNullOrEmpty(rut) && rut.Length >= 8)
            {
                int intRut = FormatUtils.RutEnFormatoBDSwift(rut);
                var result = swceService.GetPoderes(intRut);
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else throw new ArgumentException("El usuario enviado no fue encontrado en la tabla de poderes");
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
                try
                {
                    string mensaje = string.Empty;
                    bool ok = false;

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

                    tracer.TraceVerbose("Comienza el Firma anuladas anteriores");

                    if (model.Multiple.Count > 0)
                    {
                        foreach (var item in model.Multiple)
                        {
                            foreach (sw_msgsend_firma firma in item.FirmasSolicitadas)
                                firma.rut_firma = FormatUtils.RutEnFormatoBDSwift(firma.RutFirmaConDigitoVerificador);

                            ProcesarMensajes(item, ref ok, ref mensaje);
                            if (!ok)
                                throw new Exception("No se pudieron eliminar las firmas nulas anteriores");
                            
                        }
                        mensaje = "Las firmas se solicitaron satisfactoriamente";
                        return Json(new { OK = ok, Message = mensaje });
                    }
                    else
                    {
                        foreach (sw_msgsend_firma firma in model.FirmasSolicitadas)
                            firma.rut_firma = FormatUtils.RutEnFormatoBDSwift(firma.RutFirmaConDigitoVerificador);

                        ProcesarMensajes(model, ref ok, ref mensaje);
                        if (!ok)
                            throw new Exception("No se pudieron eliminar las firmas nulas anteriores");
                        
                        return Json(new { OK = ok, Message = mensaje });
                    }
                }
                catch (Exception _e)
                {
                    tracer.TraceException("Alerta SaveFirmas", _e);
                    throw;
                }
            }
        }

        /// <summary>
        /// Elimina las firmas de los mensajes Swift
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal void ProcesarMensajes(SolicitarFirmasViewModel model, ref bool estado, ref string mensaje)
        {
            using (var tracer = new Tracer("Procesar Mensajes"))
            {
                estado = bl.SetFirmasAnuladasAnteriores(model.IdMensaje, infoUsuario.RutEnFormatoBDSwift, DateTime.Now);
                if (estado)
                {
                    tracer.TraceVerbose("Comienza el borrado de firmas");
                    foreach (sw_msgsend_firma firma in model.FirmasEliminadas)
                    {
                        estado = bl.EliminarFirmas(model.IdMensaje, firma.rut_firma);
                        tracer.TraceInformation("Eliminar Firma: ", model.IdMensaje, firma.rut_firma);
                    }

                    if (estado)
                    {
                        tracer.TraceVerbose("Comienza el grabado de las firmas");
                        estado = bl.SaveFirmas(model.IdMensaje, model.FirmasSolicitadas, infoUsuario.RutEnFormatoBDSwift);

                        tracer.TraceVerbose("Comienza Cambio de estado firmas SAP");
                        string comentario = "Mensaje en espera de Autorización";

                        estado = bl.CambiaEstadoFirmasSAP(model.CasillaMensaje, model.IdMensaje, infoUsuario.RutEnFormatoBDSwift, DateTime.Now, comentario);
                        if (estado)
                            mensaje = "Las firmas se solicitaron satisfactoriamente";
                        else
                            mensaje = "No se ha podido actualizar el mensaje";
                        
                    }
                    else
                        mensaje = "No se pudieron eliminar las firmas removidas";
                }
            }
        }
        #endregion SolFirmas
    }
}
