using BCH.Comex.Core.BL.XGSV;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Supervisor.Controllers
{
    /// <summary>
    /// Funcionalidad compartida por todos los controladores de Supervisor
    /// </summary>
    public class BaseController : Controller
    {
        protected XgsvService service = new XgsvService();

        /// <summary>
        /// Datos globales del modulo Supervisor
        /// </summary>
        protected DatosGlobales Globales
        {
            get
            {
                var dato = Session[SessionKeys.Supervisor.DatosGlobalesKey] as DatosGlobales;
                if (dato == null)
                {
                    Session[SessionKeys.Supervisor.DatosGlobalesKey] = dato = service.Iniciar(HttpContext.GetCurrentUser().GetDatosUsuario());
                }
                else if (dato.DatosUsuario.Identificacion_IdEspecialistaImpersonado != dato.UsrEsp.id_especia)
                {
                    Session[SessionKeys.Supervisor.DatosGlobalesKey] = dato = service.Iniciar(HttpContext.GetCurrentUser().GetDatosUsuario());
                }

                return dato;
            }
            set
            {
                Session[SessionKeys.Supervisor.DatosGlobalesKey] = value;
            }
        }

        public void Dispose()
        {
            if (service != null)
            {
                service.Dispose();
            }
        }
    }
}
