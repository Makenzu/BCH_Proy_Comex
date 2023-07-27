using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error404
        public ActionResult Error404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true; //para que IIS no muestre su 404

            return View();
        }

        public ActionResult SessionExpired()
        {
            return View();
        }
    }
}