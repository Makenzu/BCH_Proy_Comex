using BCH.Comex.Common;
using BCH.Comex.Core.BL.XGCN;
using BCH.Comex.Core.Entities.Cext01.Devengo;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using System.Web;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Devengo.Controllers
{
    [AuthorizeOrForbidden(Roles = Constantes.AppRoles.DevengoAppRole)]
    public class BaseController : Controller
    {
        protected XgcnService service = new XgcnService();

        /// <summary>
        /// Datos globales del modulo Supervisor
        /// </summary>
        protected DatosGlobales Globales
        {
            get
            {
                var dato = Session[SessionKeys.Devengo.DatosGlobalesKey] as DatosGlobales;
                if (dato == null)
                    Session[SessionKeys.Devengo.DatosGlobalesKey] = dato = service.Iniciar(HttpContext.GetCurrentUser().GetDatosUsuario()); 

                return dato;
            }
            set
            {
                Session[SessionKeys.Devengo.DatosGlobalesKey] = value;
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