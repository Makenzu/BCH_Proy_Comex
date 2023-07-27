using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.MT300Consulta
{
    public class MT300ConsultaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MT300Consulta";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MT300Consulta_default",
                "MT300Consulta/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = "" }

            );
        }
    }
}