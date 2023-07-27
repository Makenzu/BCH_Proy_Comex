using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BCH.Comex.UI.Web.Helpers
{
    public class ControllerFactory
    {

        public static T CreateController<T>(out ControllerContext context, RouteData routeData = null)
            where T : Controller, new()
        {
            T controller = new T();

            // Create an MVC Controller Context
            var wrapper = new HttpContextWrapper(System.Web.HttpContext.Current);

            if (routeData == null)
                routeData = new RouteData();

            if (!routeData.Values.ContainsKey("controller") && !routeData.Values.ContainsKey("Controller"))
                routeData.Values.Add("controller", controller.GetType().Name
                                                            .ToLower()
                                                            .Replace("controller", ""));

            controller.ControllerContext = context = new ControllerContext(wrapper, routeData, controller);
            return controller;
        }
    }
}