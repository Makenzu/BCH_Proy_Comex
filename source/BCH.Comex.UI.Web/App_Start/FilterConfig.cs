using BCH.Comex.UI.Web.Common;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new HandleAjaxExceptionAttribute());
            filters.Add(new ComexLogAttribute());
        }
    }
}
