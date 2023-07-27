using BCH.Comex.Core.BL.Portal.Users;
using System;
using System.Web;

namespace BCH.Comex.UI.Web.Helpers
{
    public static class HttpContextHelpers
    {
        public static ComexUser GetCurrentUser(this HttpContext current)
        {
            if (current == null)
                return null;

            ComexUser value = current.Session["ComexUser"] as ComexUser;
            if (value == null || String.IsNullOrEmpty(value.Name))
                current.Session["ComexUser"] = value = ComexUser.Create(current.User);

            return value;
        }

        public static ComexUser GetCurrentUser(this HttpContextBase current)
        {
            return GetCurrentUser(current.ApplicationInstance.Context);
        }
    }
}