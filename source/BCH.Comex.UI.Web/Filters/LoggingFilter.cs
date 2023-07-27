using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BCH.Comex.UI.Web.Helpers;
using BCH.Comex.Common.Tracing;

namespace BCH.Comex.UI.Web.Filters
{
    public class LoggingFilter : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            string originArea = String.Empty;
            if (filterContext.RouteData.DataTokens.ContainsKey("area"))
            {
                originArea = filterContext.RouteData.DataTokens["area"].ToString() + "-";
            }

            using (var tracer = new Tracer(originArea + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "-" + filterContext.ActionDescriptor.ActionName))
            {
                base.OnActionExecuting(filterContext);               
            }                       
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
            base.OnActionExecuted(filterContext);
        }
    }
}