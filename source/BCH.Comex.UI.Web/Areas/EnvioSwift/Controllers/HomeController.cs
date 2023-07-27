using BCH.Comex.Common;
using BCH.Comex.Core.BL.Portal;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.EnvioSwift.Controllers
{
    public class HomeController : BaseController
    {
        /// <summary>
        /// Type initializer. Registra la app en el portal
        /// </summary>
        static HomeController()
        {
            new PortalService().RegisterApp("SWSE", "Envío de mensajería Swift", "SWIFT",
                Constantes.AppRoles.EnvioSwiftAppRole, "COMEX_GRP_SWIFT", "EnvioSwift");
        }


        //
        // GET: /InicioDia/Home/
        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.EnvioSwiftAppRole)]
        public ActionResult Index()
        {   
            return View();
        }
    }
}