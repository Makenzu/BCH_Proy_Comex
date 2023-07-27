using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Planillas
{
    public class PlanillasAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Planillas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Planillas_default",
                "Planillas/{controller}/{action}/{id}",
                new { controller = "Planillas", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}