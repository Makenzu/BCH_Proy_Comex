using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.InicioDia
{
    public class InicioDiaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "InicioDia";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "InicioDia_default",
                "InicioDia/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "BCH.Comex.UI.Web.Areas.InicioDia.Controllers" }
            );
        }
    }
}