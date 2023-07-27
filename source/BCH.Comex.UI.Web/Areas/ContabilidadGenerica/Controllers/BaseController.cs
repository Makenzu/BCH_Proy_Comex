using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Common.Exceptions;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XGGL;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using System;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Controllers
{
    /// <summary>
    /// Funcionalidad compartida por todos los controladores de ContabilidadGenerica
    /// </summary>
    public class BaseControllerCG : BaseController
    {
        protected XgglService service = new XgglService();

        /// <summary>
        /// Datos globales del modulo ContabilidadGenerica
        /// </summary>
        protected DatosGlobales Globales
        {
            get
            {
                var dato = Session[SessionKeys.ContabilidadGenerica.DatosGlobalesKey] as DatosGlobales;
                if (dato == null)
                {
                    Session[SessionKeys.ContabilidadGenerica.DatosGlobalesKey] = dato = service.Iniciar(HttpContext.GetCurrentUser().GetDatosUsuario());
                }
                return dato;
            }
            set
            {
                Session[SessionKeys.ContabilidadGenerica.DatosGlobalesKey] = value;
            }
        }

        public void Dispose()
        {
            if (service != null)
            {
                service.Dispose();
            }
        }

        public ActionResult Rutear(Action<DatosGlobales> Logica, Func<DatosGlobales, ActionResult> View, bool limpiar = true)
        {
            if(this.Globales!=null && limpiar)
            {
                this.Globales.MESSAGES.Clear();
            }
            if (this.Globales == null)
            {

            }
            if (Logica != null)
            {
                try
                {
                    Logica(this.Globales);
                }catch(Exception ex)
                {
                    this.Globales.Action = String.Empty;
                    this.Globales.Controller = String.Empty;
                    if (ExceptionPolicy.HandleException(ex, "PoliticaUIFundTransfer"))
                    {
                        throw;
                    }
                    else
                    {
                        this.Globales.MESSAGES.Add(new UI_Message()
                        {
                            Text = ex.Message,
                            Title = "Excepción: ",
                            Type = TipoMensaje.Error
                        });

                        return RedirectToAction("Index", "Home", new { area = "ContabilidadGenerica" });
                    }

                }
            }

            if (this.Globales.MESSAGES.Count > 0 && string.IsNullOrWhiteSpace(this.Globales.MODGUSR.UsrEsp.OfixUser))
            {
                throw new ComexApplicationException("No se pudo encontrar la Oficina del Especialista. Comuníquese con el Administrador del Sistema.");
            }

            if (String.IsNullOrEmpty(this.Globales.Action)) // si no tengo que redireccionar
            {
                if (View == null)
                {
                    //tirar excepcion
                    this.Globales.Action = String.Empty;
                    this.Globales.Controller = String.Empty;
                    throw new ComexApplicationException("El parametro View no fué proporcionado y no redirige.");
                }
                return View(this.Globales);
            }
            else // tengo que redireccionar
            {
                var nuevaAccion = this.Globales.Action;
                var nuevoControlador = this.Globales.Controller;
                this.Globales.Action = String.Empty;
                this.Globales.Controller = String.Empty;
                if (String.IsNullOrEmpty(nuevoControlador))
                {
                    return RedirectToAction(nuevaAccion, new { area = "ContabilidadGenerica" });
                }
                else
                {
                    return RedirectToAction(nuevaAccion, nuevoControlador, new { area = "ContabilidadGenerica" });
                }
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Common.Utils.ValidarEjecucionFinDia(Session);

            var globales = Session[SessionKeys.ContabilidadGenerica.DatosGlobalesKey] as DatosGlobales;
            if (globales == null && filterContext.ActionDescriptor.ActionName != "Index")
            {
                filterContext.Result = this.RedirectToAction("Index", "Home", new { area = "ContabilidadGenerica" });
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
