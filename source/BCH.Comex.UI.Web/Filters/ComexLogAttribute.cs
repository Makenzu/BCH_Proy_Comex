using BCH.Comex.Common.Tracing;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Common
{
    public class ComexLogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                using (var tracer = new Tracer())
                {
                    tracer.TraceException("Alerta: ", filterContext.Exception);
                    filterContext.Exception.HelpLink = tracer.ActividadID;
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
