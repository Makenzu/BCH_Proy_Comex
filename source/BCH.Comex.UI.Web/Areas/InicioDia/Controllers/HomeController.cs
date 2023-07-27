using BCH.Comex.Common;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.BL.XGID;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.UI.Web.Areas.InicioDia.Views.Models;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Security;
using System.Web;
using System.Web.Mvc;
using BCH.Comex.UI.Web.Filters;

namespace BCH.Comex.UI.Web.Areas.InicioDia.Controllers
{
    [LoggingFilter]
    public class HomeController : Controller
    {
        InicioDiaService service = new InicioDiaService();
        SessionDatosUsuario infoUsuario = null;

        private const string CCImportacionesCentral = "827";
        private const string CCImportacionesMetropolitana = "829";


        static HomeController()
        {
            new PortalService().RegisterApp("XGID", "Inicio de Día", "Generales",
                Constantes.AppRoles.InicioDiaAppRole, "COMEX_GRP_GENERAL", "InicioDia");
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            CargarInfoUsuarioDeCacheOBL();
        }

        protected override void Dispose(bool disposing)
        {
            if (service != null)
            {
                service.Dispose();
            }
        }


        //
        // GET: /InicioDia/Home/
        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.InicioDiaAppRole)]
        public ActionResult Index(IList<UI_Message> mensajes)
        {
            IndexViewModel model = new IndexViewModel();
            model.InfoUsuario = infoUsuario;
            model.Especialistas = service.GetEspecialistasParaUsuario(infoUsuario.UsuarioOriginal);
            model.DescCCActual = infoUsuario.Configuracion.Identificacion_CentroDeCostosImpersonado;
            model.Mensajes = mensajes;

            if (model.DescCCActual == CCImportacionesCentral)
            {
                model.DescCCActual += " - Importaciones Central";
            }
            else if (model.DescCCActual == CCImportacionesMetropolitana)
            {
                model.DescCCActual += " - Importaciones Metropolitana";
            }

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult IniciarDia()
        {
            List<UI_Message> messages;
            bool inicioDiaOK = service.IniciarDia(infoUsuario.Configuracion, out messages);

            if (inicioDiaOK) // si se ejecuto bien el inicio de dia, limpio la variable que impide entrar a las aplicaciones
            {
                Session[SessionKeys.Common.FinDiaEjecutado] = null;
                Common.Utils.InvalidarSesion(this.Session);
            }

            if (messages.Count == 0)
            {
                return RedirectToAction("Novedades", new { inicioDiaEfectuado = true });
            }
            else
            {
                return Index(messages);
            }
        }

        [HttpPost, HandleAjaxException]
        public ActionResult ReemplazarUsuario(string centroCostoYCodUsr)
        {
            if (!String.IsNullOrEmpty(centroCostoYCodUsr) && centroCostoYCodUsr.Length == 6)
            {
                /// Datos Originales
                SessionDatosUsuario infoUsuarioOriginal = new SessionDatosUsuario();
                infoUsuarioOriginal.Configuracion = HttpContext.GetCurrentUser().GetDatosUsuario();
                infoUsuarioOriginal.UsuarioImpersonado = infoUsuarioOriginal.UsuarioImpersonado;
                string mensajeAccion = string.Empty;
                bool estadoAccion = true;

                string[] parts = centroCostoYCodUsr.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                service.ReemplazarUsuario(infoUsuario.Configuracion, parts[0], parts[1]);
                infoUsuario.UsuarioImpersonado = service.GetUsuario(parts[0], parts[1]);

                if (!service.ValidarSucursalUsuario(infoUsuario.Configuracion))
                {
                    infoUsuario.Configuracion = infoUsuarioOriginal.Configuracion;
                    estadoAccion = false;
                    service.ReemplazarUsuario(infoUsuario.Configuracion,
                        infoUsuario.Configuracion.Identificacion_CentroDeCostosImpersonado,
                        infoUsuario.Configuracion.Identificacion_IdEspecialistaImpersonado);
                    mensajeAccion = string.Format("El especialista '{0}' no tiene configurado códigos de sucursales. No es posible realizar el reemplazo.", centroCostoYCodUsr);
                }else
                {
                    Common.Utils.InvalidarSesion(this.Session);
                }

                var datos = new { estado = estadoAccion, info = centroCostoYCodUsr, mensaje = mensajeAccion };

                return new JsonResult()
                {
                    Data = datos
                };
            }
            else
            {
                throw new ArgumentException("El centro de costo e ids de especialistas enviado no son validos");
            }
        }


        public ActionResult Novedades(bool? inicioDiaEfectuado = false)
        {
            using (InicioDiaService service = new InicioDiaService())
            {
                NovedadesViewModel model = new NovedadesViewModel();
                if (inicioDiaEfectuado.HasValue && inicioDiaEfectuado.Value)
                {
                    model.Mensajes.Add(new UI_Message() { Title = "Operación exitosa!", Text = "El proceso de Inicio de Día ha terminado exitosamente.", AutoClose = true, Type = TipoMensaje.Correcto });
                }

                model.Novedades = service.GetNovedadesParaUsuario(infoUsuario.UsuarioImpersonado.cent_costo, infoUsuario.UsuarioImpersonado.id_especia, infoUsuario.UsuarioImpersonado.jerarquia, DateTime.Now.Date, model.Mensajes);
                return View(model);
            }
        }

        [HttpGet, HandleAjaxException]
        public ActionResult BuscarNovedades(String fecha)
        {
            using (InicioDiaService service = new InicioDiaService())
            {
                DateTime fechaOut;
                if (DateTime.TryParse(fecha, out fechaOut))
                {
                    List<UI_Message> mensajes = new List<UI_Message>();
                    IList<sce_nov_s01_MS_Result> novedades = service.GetNovedadesParaUsuario(infoUsuario.UsuarioImpersonado.cent_costo, infoUsuario.UsuarioImpersonado.id_especia, infoUsuario.UsuarioImpersonado.jerarquia, fechaOut, mensajes);

                    var data = new { Novedades = novedades, Mensajes = mensajes };

                    return new JsonResult()
                    {
                        Data = data,
                        MaxJsonLength = Int32.MaxValue,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    throw new ApplicationException("Fecha Invalida");
                }
            }
        }

        public ActionResult ImprimirReporteNovedades(DateTime fecha)
        {
            using (InicioDiaService service = new InicioDiaService())
            {
                List<UI_Message> mensajes = new List<UI_Message>();
                ReporteNovedadesViewModel model = new ReporteNovedadesViewModel();
                model.FechaReporte = fecha;
                model.Novedades = service.GetNovedadesParaUsuario(infoUsuario.UsuarioImpersonado.cent_costo, infoUsuario.UsuarioImpersonado.id_especia, infoUsuario.UsuarioImpersonado.jerarquia, fecha, mensajes);
                model.Usuario = infoUsuario.UsuarioImpersonado.nombre.Trim();
                return View("ReporteNovedades", model);
            }
        }

        #region Metodos privados

        private void CargarInfoUsuarioDeCacheOBL()
        {
            infoUsuario = new SessionDatosUsuario();
            infoUsuario.Configuracion = HttpContext.GetCurrentUser().GetDatosUsuario();
            if (infoUsuario.Configuracion != null)
            {
                infoUsuario.UsuarioImpersonado = service.GetUsuario(infoUsuario.Configuracion.Identificacion_CentroDeCostosImpersonado, infoUsuario.Configuracion.Identificacion_IdEspecialistaImpersonado);

                if (infoUsuario.Configuracion.Identificacion_IdEspecialistaOriginal == infoUsuario.Configuracion.Identificacion_IdEspecialistaImpersonado)
                {
                    //el usuario logueado es si mismo, no esta impersonando a nadie
                    infoUsuario.UsuarioOriginal = infoUsuario.UsuarioImpersonado;
                }
                else
                {
                    infoUsuario.UsuarioOriginal = service.GetUsuario(infoUsuario.Configuracion.Identificacion_CentroDeCostosOriginal, infoUsuario.Configuracion.Identificacion_IdEspecialistaOriginal);
                }

                this.ControllerContext.HttpContext.Session[SessionKeys.InicioDia.DatosUsuario] = infoUsuario;
            }
            else throw new SecurityException("El usuario no tiene permisos para acceder a la aplicación");

        }

        #endregion
    }
}