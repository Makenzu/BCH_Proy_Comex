using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.BusquedaPlanilla
{
    public class BusquedaPlanillaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BusquedaPlanilla";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "BusquedaPlanilla_default",
                "BusquedaPlanilla/{controller}/{action}/{id}",
                new { controller = "Inicio", action = "Index", id = "" }
            );
        }
    }
}