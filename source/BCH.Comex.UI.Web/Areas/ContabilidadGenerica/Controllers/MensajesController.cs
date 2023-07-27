using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Controllers
{
    public class MensajesController : BaseControllerCG
    {
        // GET: ContabilidadGenerica/Mensajes
        public ActionResult GuardarCambiosOperacion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GuardarCambiosOperacion(bool ok)
        {
            this.Globales.resMsg = ok;
            return RedirectToAction("Operacion", "Nueva");
        }
    }
}