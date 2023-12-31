﻿using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Supervisor
{
    public class SupervisorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Supervisor";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Supervisor_default",
                "Supervisor/{controller}/{action}/{id}",
                new { controller = "Inicio", action = "Index", id = "" }
            );
        }
    }
}