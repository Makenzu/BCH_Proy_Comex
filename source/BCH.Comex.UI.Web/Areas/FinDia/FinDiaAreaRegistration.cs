using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.FinDia
{
    public class FinDiaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "FinDia";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "FinDia_default",
                "FinDia/{controller}/{action}/{id}",
                new { controller = "Inicio", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}