using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Devengo
{
    public class DevengoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Devengo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Devengo_default",
                "Devengo/{controller}/{action}/{id}",
                new {controller = "Inicio", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}