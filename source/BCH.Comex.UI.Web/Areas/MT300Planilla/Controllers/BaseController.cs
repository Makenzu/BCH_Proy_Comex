using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Common.Exceptions;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.MT300Planilla;
using BCH.Comex.Core.Entities.Cext01.MT300Planilla;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using System;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.MT300Planilla.Controllers
{
    /// <summary>
    /// Funcionalidad compartida por todos los controladores de GeneracionMT300
    /// </summary>
    public class BaseControllerMT300Planilla : BaseController
    {
        protected MT300PlanillaService service = new MT300PlanillaService();

        /// <summary>
        /// Datos globales del modulo GeneracionMT300
        /// </summary>
        protected DatosGlobales Globales
        {
            get
            {
                var dato = Session[SessionKeys.MT300Planilla.DatosGlobalesKey] as DatosGlobales;
                if (dato == null)
                {
                    Session[SessionKeys.MT300Planilla.DatosGlobalesKey] = dato = service.Iniciar(HttpContext.GetCurrentUser().GetDatosUsuario());
                }
                return dato;
            }
            set
            {
                Session[SessionKeys.MT300Planilla.DatosGlobalesKey] = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (service != null)
            {
                service.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
