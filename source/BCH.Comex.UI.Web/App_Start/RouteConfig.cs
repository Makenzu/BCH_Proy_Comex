using System.Web.Mvc;
using System.Web.Routing;

namespace BCH.Comex.UI.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "BCH.Comex.UI.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Error404",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Error", action = "Error404", id = UrlParameter.Optional }
            );
        }
    }
}
