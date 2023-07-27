using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.GeneracionMT300
{
    public class GeneracionMT300AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GeneracionMT300";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GeneracionMT300_default",
                "GeneracionMT300/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = "" }
            );
        }
    }
}