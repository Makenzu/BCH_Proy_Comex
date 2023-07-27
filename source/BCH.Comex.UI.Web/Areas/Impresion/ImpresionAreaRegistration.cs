using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Impresion
{
    public class ImpresionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Impresion";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Impresion_default",
                "Impresion/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}