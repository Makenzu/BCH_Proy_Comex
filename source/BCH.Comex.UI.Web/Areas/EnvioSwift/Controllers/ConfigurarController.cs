using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.BL.SWSE;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.UI.Web.Areas.EnvioSwift.Models;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using BCH.Comex.UI.Web.Models;
using System.Web;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.EnvioSwift.Controllers
{
    public class ConfigurarController : BCH.Comex.UI.Web.Common.BaseController
    {
        protected EnvioSwiftService service = new EnvioSwiftService();
        protected PortalService serviceP = new PortalService();

        // GET: EnvioSwift/Configurar
        public ActionResult Alertas()
        {
            var model = new ConfiguracionAlertasModel(infoUsuario.MinsAlertaEnvioSwift??0);
            return View(model);
        }

        public ActionResult EditarConfiguracionAlertas(short? id)
        {
            id = id ?? 0;
            IDatosUsuario usuario = HttpContext.GetCurrentUser().GetDatosUsuario(); //leo devuelta DatosUsuario ya que no puedo usar el de session porque fue leído en un context diferente
            usuario.MinsAlertaEnvioSwift = id;
            serviceP.CambiarMinsAlertaEnvioSwift(usuario);
            ActualizarDatosUsuarioEnCache(usuario);
            this.ControllerContext.HttpContext.Session.Remove(SessionKeys.Common.ConfigAlertas); //invalido el cache de config de alertas
            this.ControllerContext.HttpContext.Session.Remove(SessionKeys.Common.Alertas);
            return RedirectToAction("Index", "Home", new { area = "EnvioSwift" });
        }
    }
}