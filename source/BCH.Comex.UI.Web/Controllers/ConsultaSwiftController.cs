using BCH.Comex.Common;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.BL.SWEM;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using BCH.Comex.UI.Web.Models.ConsultaSwift;
using BCH.Comex.Utils;
using RazorPDF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Humanizer;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.UI.Web.Helpers.Extensions;

namespace BCH.Comex.UI.Web.Controllers
{
    public class ConsultaSwiftController : BaseController
    {
        private SwiftMgr bl;
        private const string CookieName = "BCHComexSwem_Casillas";
        private const string CookieValueCasillasVisibles = "CasillasVisibles";
        private const string CookieValueCasillaDefault = "CasillaDefault";
        private const string CacheCasillasSwiftTodas = "CasillasSwiftTodas";
        private const string CacheTipoCamposConMaximo = "TipoCamposConMaximo";
        private const string CacheCampos = "Campos";
        private const string ConfigTamanioPaginaGridBusqueda = "ConsultaSwift.TamanioPaginaGridBusqueda";
        private const string ConfigCuerpoEmail = "ConsultaSwift.CuerpoEmailMensaje";
        private const string CacheCuerpoEmail = "CuerpoEmailMensaje";

        static ConsultaSwiftController() {
            new PortalService().RegisterApp("SWCO", "Consulta de mensajes Swift", "SWIFT", 
                Constantes.AppRoles.ConsultaSwiftAppRole, "COMEX_GRP_SWIFT", "ConsultaSwift");
        }

        public ConsultaSwiftController()
        {
            bl = new SwiftMgr();
        }

        #region Actions

        [Authorize]
        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.ConsultaSwiftAppRole + ", " + Constantes.AppRoles.EnvioSwiftAppRole)]
        public ActionResult Index(ConsultarParaAccion paraAccion = ConsultarParaAccion.SoloConsultarOEnviarMail)
        {
            using (var tracer = new Tracer("ConsultaSwift"))
            {
                IEnumerable<sw_casillas> casillas = GetCasillasDeCacheOBL();
                SelectList todasLasCasillas = new SelectList(casillas.OrderBy(i => i.cod_casilla).ToList(), "cod_casilla", "DataTextField");

                IndexModel model = new IndexModel();
                model.TodasLasCasillas = todasLasCasillas;
                model.PageSize = short.Parse(ConfigurationManager.AppSettings[ConfigTamanioPaginaGridBusqueda]);

                //si tiene permiso para Envio de mensajeria Swift, muestro datos adicionales 
                try
                {
                    //model.FuncionalidadesExtraPermitidas = (User.IsInRole(Constantes.AppRoles.EnvioSwiftAppRole) || User.IsInRole(Constantes.AppRoles.ConsultaSwiftAppRole));
                    // Ahora ambos perfiles permiten funcionalidades extra
                    model.FuncionalidadesExtraPermitidas = true;
                    // Se agrega campo para verificar si el usuario solo puede hacer consulta
                    model.SoloConsulta = (User.IsInRole(Constantes.AppRoles.ConsultaSwiftAppRole) && !(User.IsInRole(Constantes.AppRoles.EnvioSwiftAppRole)));
                }
                catch (Exception ex)
                {
                    model.FuncionalidadesExtraPermitidas = false;
                    model.SoloConsulta = true;
                    tracer.TraceException("Alerta al usar IsInRole, asumo que el usuario no pertenece", ex);
                }
#if DEBUG
                model.FuncionalidadesExtraPermitidas = true;
                model.SoloConsulta = false;
#endif
                model.ParaAccion = paraAccion;
                model.NombreCompletoUsuarioLogueado = HttpContext.GetCurrentUser().FullName;
                model.RutUsuarioLogueado = infoUsuario.RutEnFormatoBDSwift;

                //if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains(CookieName))
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
        }

        [HttpGet, HandleAjaxException]
        public ActionResult ValidarFirmas(int idMensaje)
        {
            var firmasSolicitadas = bl.GetFirmasDeMensajeEnviado(idMensaje, false);
            var mensaje = bl.GetDatosMensajeEnviado(idMensaje);

            if (mensaje.estado_msg != "REM")
            {
                if (firmasSolicitadas.Count > 0)
                {
                    return Json(new { success = false, responseText = "SWIFT tiene firmas solicitadas." }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = true, responseText = "SWIFT no tiene firmas solicitadas." }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get), FileDownload]
        public ActionResult DescargarMailConSwiftComoAttachment(int sesion, int secuencia, int? idMensaje)
        {
            MensajeSwiftAdjunto msg = new MensajeSwiftAdjunto { Sesion = sesion, Secuencia = secuencia, IdMensaje = idMensaje };
            return DescargarMailConSwiftsComoAttachments(new List<MensajeSwiftAdjunto>() { msg });
        }

        [AcceptVerbs(HttpVerbs.Get), FileDownload]
        public ActionResult DescargarMailConSwiftsComoAttachments(string idsAttachments)
        {
            if (!string.IsNullOrEmpty(idsAttachments))
            {
                //tengo que recomponer los ids de los attachments desde una string donde estan todos unidos
                string[] keysAttachs = idsAttachments.Split(new string[] { "!@!" }, StringSplitOptions.RemoveEmptyEntries);

                List<MensajeSwiftAdjunto> swifts = new List<MensajeSwiftAdjunto>();
                foreach (string keyAttach in keysAttachs)
                {
                    MensajeSwiftAdjunto msg = new MensajeSwiftAdjunto();
                    msg.CargarDesdeStrIdentificacion(keyAttach);

                    swifts.Add(msg);
                }

                return DescargarMailConSwiftsComoAttachments(swifts);
            }
            else
            {
                return null;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarCasillas(List<string> idsCasillasVisibles, string idCasillaDefault)
        {
            using (var tracer = new Tracer())
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

        [AcceptVerbs(HttpVerbs.Post), HandleAjaxException]
        public ActionResult BuscarSwifts(int? idCasilla, DateTime? fechaDesde, DateTime? fechaHasta, byte direccion, bool incluirSoloPropios, short? pageSize, int? rowOffset, string sortOrder, string searchText, EstadoSwiftEnviado estadoEnviados = EstadoSwiftEnviado.Enviado)
        {
            if (fechaDesde.HasValue)
            {
                fechaDesde = fechaDesde.Value.Date; //para sacar cualquier componente de hora:min que me pudiera venir de js
                fechaHasta = fechaHasta.Value.Date;
            }

            if(!String.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToUpper().Replace("%", "");
            }
            

            if (!pageSize.HasValue)
            {
                pageSize = short.Parse(ConfigurationManager.AppSettings[ConfigTamanioPaginaGridBusqueda]);
            }

            if (!rowOffset.HasValue)
            {
                rowOffset = 0;
            }
            
            int totalRows = 0;
			int? rutUsuario = null;
            
            if(direccion != 2 && incluirSoloPropios)
            {
                rutUsuario = int.Parse(infoUsuario.Identificacion_Rut);
            }

            var resultado = bl.BuscarSwiftsPorCasillaYFechas(idCasilla, fechaDesde, fechaHasta, (direccion != 2), rutUsuario, out totalRows, rowOffset, pageSize, searchText, estadoEnviados);

            object jsonData = new { total = totalRows, rows = resultado }; //'total' y 'rows' tienen que llamarse asi, y en ese orden, lo requiere la bootstrap table

            return new JsonResult()
            {
                Data = jsonData,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult ReporteConsultaSwift(int? idCasilla, DateTime fechaDesde, DateTime fechaHasta, byte direccion, string filtroCampo, string filtroDesc, bool incluirSoloPropios, EstadoSwiftEnviado estadoEnviados = EstadoSwiftEnviado.Enviado)
        {
            using (var tracer = new Tracer("ReporteConsultaSwift"))
            {
                try
                {
                    fechaDesde = fechaDesde.Date; //para sacar cualquier componente de hora:min que me pudiera venir de js
                    fechaHasta = fechaHasta.Date;

                    bool enviados = (direccion != 2);
                    int totalRows = 0;
                    int? rutUsuario = null;
                    if (incluirSoloPropios)
                    {
                        rutUsuario = int.Parse(infoUsuario.Identificacion_Rut);
                    }

                    var resultado = bl.BuscarSwiftsPorCasillaYFechas(idCasilla, fechaDesde, fechaHasta, (direccion != 2), rutUsuario, out totalRows, 0, short.MaxValue, null, estadoEnviados); //paso el maximo tamanio de pagina posible

                    if (!enviados && filtroCampo != null)
                    {
                        resultado = resultado.Where(r => r.estado_msg == filtroCampo).ToList();
                    }

                    IList<sw_casillas> casillas = GetCasillasDeCacheOBL();
                    sw_casillas casilla = casillas.Where(c => c.cod_casilla == idCasilla).FirstOrDefault();

                    ReporteConsultaSwiftViewModel model = new ReporteConsultaSwiftViewModel()
                    {
                        FechaDesde = fechaDesde,
                        Enviados = enviados,
                        Registros = resultado,
                        Verbo = (direccion == 2 ? "recepcionados" : (estadoEnviados == EstadoSwiftEnviado.EnAprobacion ? "en aprobación" : estadoEnviados.ToString().ToLower().Pluralize()))
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
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta", ex);
                    throw;
                }
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EstadisticasSwifts(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, byte direccion)
        {
            fechaDesde = fechaDesde.Date; //para sacar cualquier componente de hora:min que me pudiera venir de js
            fechaHasta = fechaHasta.Date; 

            JsonResult jsonResult;

                var result = bl.GetEstadisticasMensajesPorCasilla(idCasilla, fechaDesde, fechaHasta, (direccion != 2));

                jsonResult = new JsonResult()
                {
                    Data = result,
                    MaxJsonLength = Int32.MaxValue,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                return jsonResult;
        }

        public ActionResult PruebaPDF()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="secuencia"></param>
        /// <param name="idMensaje"></param>
        /// <param name="htmlCompleto"></param>
        /// <param name="pdf"></param>
        /// <param name="incluirFirmas"></param>
        /// <param name="visualizacion"></param>
        /// <returns></returns>
        public ActionResult DetalleSwift(int sesion, int secuencia, int? idMensaje, bool? htmlCompleto = true, bool? pdf = false, bool incluirFirmas = true, bool visualizacion = false)
        {
            using (var tracer = new Tracer("DetalleSwift"))
            {
                var datosUsuario = HttpContext.GetCurrentUser().GetDatosUsuario();
                PrintFormat imp = datosUsuario.ConfigImpres_PrintFormat;
                if (idMensaje.HasValue)
                    if (idMensaje.Value == 0)
                        idMensaje = null;

                var configuracionPagina = new TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerateConfig()
                {
                    PageSize = PdfSharp.PageSize.A4,
                    MarginBottom = 25,
                    MarginTop = 25,
                    MarginLeft = 25,
                    MarginRight = 25
                };

                if (idMensaje.HasValue)
                {
                    DetalleSwiftEnviadoViewModel model = new DetalleSwiftEnviadoViewModel();
                    model.Swift = bl.CargarMensajeSwiftEnviado(idMensaje.Value, GetTipoCamposDeCacheOBL(), GetCamposDeCacheOBL());
                    if (incluirFirmas)
                    {
                        model.Firmas = bl.GetFirmasDeMensajeEnviado(idMensaje.Value, true);
                    }
                    model.GenerarHtmlCompleto = (htmlCompleto.HasValue ? htmlCompleto.Value : true);

                    if (model.Swift != null)
                    {
                        if (pdf == true && (imp == PrintFormat.PDF || imp == PrintFormat.TIFF))
                        {
                            return this.AlternateOutput(imp, "DetalleSwiftEnviado", model, null, configuracionPagina);
                        }
                        else
                        {
                            return View("DetalleSwiftEnviado", model);
                        }
                    }
                    else
                        return null;
                }
                else
                {
                    this.ViewBag.GenerarHtmlCompleto = true;
                    ResultadoBusquedaSwift swift = bl.CargarMensajeSwiftRecibido(sesion, secuencia, GetTipoCamposDeCacheOBL(), GetCamposDeCacheOBL());

                    if (swift != null)
                    {
                        swift.visualizacion = visualizacion;
                        if (pdf == true && (imp == PrintFormat.PDF || imp == PrintFormat.TIFF))
                        {
                            bl.ActualizaContadorImpresionMensajeRecibido(sesion, secuencia);
                            return this.AlternateOutput(imp, "DetalleSwiftRecibido", swift, null, configuracionPagina);
                        }
                        else
                        {
                            return View("DetalleSwiftRecibido", swift);
                        }
                    }
                    else return null;
                } 
            }
        }

        public ActionResult DatosSwiftEnviado(int idMensaje)
        {
            DatosMensajeEnviadoViewModel model = new DatosMensajeEnviadoViewModel();
            model.Datos = bl.GetDatosMensajeEnviado(idMensaje);
            model.Firmas = bl.GetFirmasDeMensajeEnviado(idMensaje, true);
            return View(model);
        }

        public ActionResult DatosSwiftRecibido(int sesion, int secuencia)
        {
            proc_sw_rec_trae_datos_msg_MS_Result model = bl.GetDatosMensajeRecibido(sesion, secuencia);
            if (model != null)
            {
                var datosUsuario = HttpContext.GetCurrentUser().GetDatosUsuario();
                PrintFormat imp = datosUsuario.ConfigImpres_PrintFormat;
                ViewBag.PrintFormat = imp;
                return View(model);
            }
            else return null;
        }

        public ActionResult LogSwiftEnviado(int idMensaje)
        {
            LogSwiftViewModel model = new LogSwiftViewModel();
            model.Log = bl.GetLogDeMensajeEnviado(idMensaje);
            model.IdMensaje = idMensaje;
            return View("LogSwift", model);
        }

        public ActionResult LogSwiftRecibido(int sesion, int secuencia)
        {
            LogSwiftViewModel model = new LogSwiftViewModel();
            model.Log = bl.GetLogDeMensajeRecibido(sesion, secuencia);
            model.Sesion = sesion;
            model.Secuencia = secuencia;
            return View("LogSwift", model);
        }

        #endregion


        #region Private methods

        private ActionResult DescargarMailConSwiftsComoAttachments(List<MensajeSwiftAdjunto> swifts)
        {
            using (var tracer = new Tracer())
            {
                if (swifts != null)
                {
                    string from = "especialista@bancochile.cl";
                    try
                    {
                        var usuario = HttpContext.GetCurrentUser();
                        tracer.AddToContext("email", usuario.EMail);
                    }
                    catch (Exception ex)
                    {
                        tracer.TraceException("Alerta", ex);
                    }

                    MailMessage message = new MailMessage(from, "direccion@cliente.cl"); //remplazar el from con la dir del usuario del principal 
                    message.Body = GetCuerpoDeEmailDeCacheOConfig();
                    message.IsBodyHtml = false;

                    var fileEncoding = message.BodyEncoding = message.HeadersEncoding = message.SubjectEncoding = System.Text.Encoding.UTF8;

                    foreach (MensajeSwiftAdjunto swift in swifts)
                    {
                        string rawHtml = String.Empty;
                        if (swift.IdMensaje.HasValue)
                        {
                            rawHtml = RenderViewDetalleSwiftEnviadoToString(swift.Sesion, swift.Secuencia, swift.IdMensaje.Value);
                        }
                        else
                        {
                            rawHtml = RenderViewDetalleSwiftRecibidoToString(swift.Sesion, swift.Secuencia);
                        }

                        string fileName = swift.Sesion + "" + swift.Secuencia + ".html";
                        message.Attachments.Add(
                            Attachment.CreateAttachmentFromString(rawHtml, fileName)
                            );
                    }

                    /*string html = @"<html><body><h1>Titulo</h1><p>" + mail.Body + @"</p><img src=""cid:Logo""></body></html>";
                    AlternateView altView = AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html);

                    LinkedResource yourPictureRes = new LinkedResource(@"D:\ProyectosVS\PruebasComex\MvcApplication\Files\LogoBCH.jpg",
                        MediaTypeNames.Image.Jpeg);
                    yourPictureRes.ContentId = "Logo";
                    altView.LinkedResources.Add(yourPictureRes);
                    message.AlternateViews.Add(altView);*/


                    byte[] fileContent = message.SaveToStream();
                    //Se deja en blanco la direccion FROM para poder enviar desde OUTLOOK del banco
                    fileContent = fileEncoding.GetBytes(fileEncoding.GetString(fileContent).Replace(from, string.Empty));

                    FileContentResult result = new FileContentResult(fileContent, "message/rfc822");
                    result.FileDownloadName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".eml"; //genero un nombre de archivo aux con la fecha del momento
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }


        private IList<sw_casillas> GetCasillasDeCacheOBL()
        {
            this.ControllerContext.HttpContext.Cache.Remove(CacheCasillasSwiftTodas);
            if (this.ControllerContext.HttpContext.Cache[CacheCasillasSwiftTodas] == null)
            {
                IList<sw_casillas> result = bl.GetTodasLasCasillas();
                this.ControllerContext.HttpContext.Cache[CacheCasillasSwiftTodas] = result;
                return result;
            }
            else
            {
                return this.ControllerContext.HttpContext.Cache[CacheCasillasSwiftTodas] as IList<sw_casillas>;
            }
        }

        private IList<proc_trae_tipo_campos_MS_Result> GetTipoCamposDeCacheOBL()
        {
            if (this.ControllerContext.HttpContext.Cache[CacheTipoCamposConMaximo] == null)
            {
                IList<proc_trae_tipo_campos_MS_Result> result = bl.GetTipoCamposConMaximo();
                this.ControllerContext.HttpContext.Cache[CacheTipoCamposConMaximo] = result;
                return result;
            }
            else
            {
                return this.ControllerContext.HttpContext.Cache[CacheTipoCamposConMaximo] as IList<proc_trae_tipo_campos_MS_Result>;
            }
        }

        private IList<sw_campos_msg> GetCamposDeCacheOBL()
        {
            if (this.ControllerContext.HttpContext.Cache[CacheCampos] == null)
            {
                IList<sw_campos_msg> result = bl.GetCampos();
                this.ControllerContext.HttpContext.Cache[CacheCampos] = result;
                return result;
            }
            else
            {
                return this.ControllerContext.HttpContext.Cache[CacheCampos] as IList<sw_campos_msg>;
            }
        }

       private string GetCuerpoDeEmailDeCacheOConfig()
        {
            if (this.ControllerContext.HttpContext.Cache[CacheCuerpoEmail] == null)
            {
                string cuerpo = ConfigurationManager.AppSettings[ConfigCuerpoEmail];
                this.ControllerContext.HttpContext.Cache[CacheCuerpoEmail] = cuerpo;
                return cuerpo;
            }
            else
            {
                return this.ControllerContext.HttpContext.Cache[CacheCuerpoEmail] as string;
            } 
        }

        //Metodo que en lugar de retornar una view de email recibido, la devuelve como string (luego se genera un attachment con eso)
        private string RenderViewDetalleSwiftRecibidoToString(int sesion, int secuencia)
        {
            using (var sw = new StringWriter())
            {
                ViewResult result = (ViewResult)DetalleSwift(sesion, secuencia, null, true);
                result.View = ViewEngines.Engines.FindView(ControllerContext, "DetalleSwiftRecibido", String.Empty).View;

                ViewContext vc = new ViewContext(ControllerContext, result.View, result.ViewData, result.TempData, sw);

                result.View.Render(vc, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        //Metodo que en lugar de retornar una view de email enviado, la devuelve como string (luego se genera un attachment con eso)
        private string RenderViewDetalleSwiftEnviadoToString(int sesion, int secuencia, int nroMensaje)
        {
            using (var sw = new StringWriter())
            {
                ViewResult result = (ViewResult)DetalleSwift(sesion, secuencia, nroMensaje, true, false, false);
                result.View = ViewEngines.Engines.FindView(ControllerContext, "DetalleSwiftEnviado", String.Empty).View;

                ViewContext vc = new ViewContext(ControllerContext, result.View, result.ViewData, result.TempData, sw);

                result.View.Render(vc, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        #endregion
    }
}
