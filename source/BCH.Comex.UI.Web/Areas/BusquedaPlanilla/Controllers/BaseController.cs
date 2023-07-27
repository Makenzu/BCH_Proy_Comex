using BCH.Comex.Core.BL.XEVA;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.BusquedaPlanilla.Controllers
{
    public class BaseController : Controller
    {
        protected XevaService service = new XevaService();

        public void Dispose()
        {
            if (service != null)
            {
                service.Dispose();
            }
        }
	}
}