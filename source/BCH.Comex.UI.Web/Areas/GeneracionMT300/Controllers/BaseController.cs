using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Common.Exceptions;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.SWG3;
using BCH.Comex.Core.Entities.Cext01.GeneracionMT300;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using System;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.GeneracionMT300.Controllers
{
    /// <summary>
    /// Funcionalidad compartida por todos los controladores de GeneracionMT300
    /// </summary>
    public class BaseControllerMT300 : BaseController
    {
        protected Swg3Service service = new Swg3Service();

        /// <summary>
        /// Datos globales del modulo GeneracionMT300
        /// </summary>
        protected DatosGlobales Globales
        {
            get
            {
                var dato = Session[SessionKeys.GeneracionMT300.DatosGlobalesKey] as DatosGlobales;
                if (dato == null)
                {
                    Session[SessionKeys.GeneracionMT300.DatosGlobalesKey] = dato = service.Iniciar(HttpContext.GetCurrentUser().GetDatosUsuario());
                }
                return dato;
            }
            set
            {
                Session[SessionKeys.GeneracionMT300.DatosGlobalesKey] = value;
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
