using BCH.Comex.Common;
using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.BL.XGID;
using BCH.Comex.UI.Web.Helpers;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Supervisor.Controllers
{
    [AuthorizeOrForbidden(Roles = Constantes.AppRoles.SupervisorAppRole)]
    public class InicioController : BaseController
    {

        static InicioController()
        {
            new PortalService().RegisterApp("XGSV", "Supervisor", "FX TRANSFER", Constantes.AppRoles.SupervisorAppRole,
                "COMEX_GRP_CAMBIOS", "Supervisor");
        }

        // GET: Supervisor/Inicio
        public ActionResult Index()
        {
            InicioDiaService service = new InicioDiaService();
            bool? diaIniciado = service.DiaIniciado(this.Globales.DatosUsuario);
            ViewBag.DiaFinalizado = (!diaIniciado.HasValue || !diaIniciado.Value);
            return View();
        }
    }
}