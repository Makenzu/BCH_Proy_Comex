using BCH.Comex.Common;
using BCH.Comex.Core.BL.Portal;
using BCH.Comex.UI.Web.Helpers;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Devengo.Controllers
{
    public class InicioController : BaseController
    {
        static InicioController()
        {
            new PortalService().RegisterApp("XGCN", "Información de devengo", "Generales", 
                Constantes.AppRoles.DevengoAppRole, "COMEX_GRP_GENERAL", "Devengo");
        }

        //
        // GET: /Devengo/Inicio/
        public ActionResult Index()
        {
            return View();
        }
	}
}