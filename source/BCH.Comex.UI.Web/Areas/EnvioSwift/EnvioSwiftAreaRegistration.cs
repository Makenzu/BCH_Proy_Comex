using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.InicioDia
{
    public class EnvioSwiftAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EnvioSwift";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EnvioSwift_default",
                "EnvioSwift/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "BCH.Comex.UI.Web.Areas.EnvioSwift.Controllers" }
            );
        }
    }
}