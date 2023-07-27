using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.MT300Planilla
{
    public class MT300PlanillaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MT300Planilla";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MT300Planilla_default",
                "MT300Planilla/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = "" }
            );
        }
    }
}