using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.UI.Web.Areas.FinDia.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.FinDia.Controllers
{
    public class TotalesContablesController : BaseController
    {
        // GET: FinDia/TotalesContables
        public ActionResult Index(int? opcion)
        {
            List<UI_Message> listaMensajes = new List<UI_Message>();
            string reporte = string.Empty;

            if (opcion.HasValue && (opcion == 0 || opcion == 1))
            {
                reporte = this.fdService.TotalesContables(opcion.Value, this.Globales, listaMensajes);
            }

            TotalesContablesViewModel tcvm = new TotalesContablesViewModel(reporte, listaMensajes);
            return View(tcvm);
        }

    }
}