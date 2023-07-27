using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Planillas.Controllers
{
    public class PlanillasBaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Common.Utils.ValidarEjecucionFinDia(Session);

            base.OnActionExecuting(filterContext);
        }
    }
}
