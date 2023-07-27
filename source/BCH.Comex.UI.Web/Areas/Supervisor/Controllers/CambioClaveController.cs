using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.UI.Web.Areas.Supervisor.Models;
using BCH.Comex.UI.Web.Common;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Supervisor.Controllers
{

    public class CambioClaveController : BaseController
    {
        //
        // GET: /Supervisor/CambioClave/
        public ActionResult Index()
        {
            using (Tracer tracer = new Tracer())
            {
                if (this.Globales.FrmCamCl == null)
                    this.Globales.FrmCamCl = new FrmCamClDTO();

                service.CambioClaveInit(this.Globales);
                CambioClaveViewModel ccvm = new CambioClaveViewModel(this.Globales);
                return View(ccvm);
            }
        }

        [HttpPost]
        public ActionResult Index(CambioClaveViewModel ccvm, string Command)
        {
            using (Tracer tracer = new Tracer())
            {
                this.Globales.ListaMensajesError.Clear();

                ModelState.Clear();
                ccvm.Update(ccvm, this.Globales);

                switch (Command)
                {
                    case "Cancelar":
                        return new RedirectResult("~/Supervisor");
                    case "Aceptar":
                        service.CambioClave_AceptarClick(this.Globales);
                        ccvm = new CambioClaveViewModel(this.Globales);
                        break;
                }
                return View(ccvm);
            }
        }

    }
}