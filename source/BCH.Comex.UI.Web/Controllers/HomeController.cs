using BCH.Comex.Core.BL.Common;
using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BCH.Comex.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        PortalService service;

        public HomeController()
        {
            service = new PortalService();
        }

        [Authorize]
        public ActionResult Index()
        {
            return View(service.GetUserApps(HttpContext.GetCurrentUser(), Request.UrlReferrer == null));
        }

        public ActionResult External(string url)
        {
            return View("External", (object)url);
        }

       public static RouteValueDictionary ParseRouteValues(Aplicacion app) {
            // shorthand
            var qs = HttpUtility.ParseQueryString(app.parameters ?? "");

            // because LINQ is the (old) new black
            return qs.AllKeys.Aggregate(new RouteValueDictionary(),
                (rvd, k) => {
                    // can't separately add multiple values `?foo=1&foo=2` to dictionary, they'll be combined as `foo=1,2`
                    //qs.GetValues(k).ForEach(v => rvd.Add(k, v));
                    rvd.Add(k, qs[k]);
                    return rvd;
                });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Version()
        {
            var versionTypes = Enum.GetValues(typeof(VersionType)).Cast<VersionType>().ToList();
            var versionService = new VersionService();
            var versions = versionTypes.ToDictionary(i => i.ToString(), i => versionService.GetVersion(i));
            versions["Web"] = HttpContext.Application[ApplicationKeys.Comex.ComexVersionKey].ToString();

            return View(versions);
        }

        public ActionResult Settings()
        {
            ViewBag.formats = Enum.GetNames(typeof(PrintFormat)).Select(i => new SelectListItem() { Text = i, Value = i }).ToList();
            return View(HttpContext.GetCurrentUser().GetDatosUsuario());
        }

        [HttpPost]
        public JsonResult SaveSettings(PrintFormat ConfigImpres_PrintFormat)
        {
            var datosUsuario = HttpContext.GetCurrentUser().GetDatosUsuario();
            datosUsuario.ConfigImpres_PrintFormat = ConfigImpres_PrintFormat;
            PortalService service = new PortalService();
            service.CambiarConfigImpres_PrintFormat(datosUsuario);
            return Json("OK");
        }
    }
}