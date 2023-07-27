using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica
{
    public class ContabilidadGenericaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ContabilidadGenerica";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ContabilidadGenerica_default",
                "ContabilidadGenerica/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = "" }
            );
        }
    }
}