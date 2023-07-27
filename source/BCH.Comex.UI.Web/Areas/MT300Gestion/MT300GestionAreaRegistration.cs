using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.MT300Gestion
{
    public class MT300GestionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MT300Gestion";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MT300Gestion_default",
                "MT300Gestion/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = "" }

            );
        }
    }
}