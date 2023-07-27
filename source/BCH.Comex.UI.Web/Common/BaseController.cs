using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.UI.Web.Helpers;
using System;
using System.Security;
using System.Web;
using System.Web.Mvc;


namespace BCH.Comex.UI.Web.Common
{
    public abstract class BaseController: Controller
    {

        protected IDatosUsuario infoUsuario;

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            //if (!String.IsNullOrEmpty(filterContext.HttpContext.User.Identity.Name))
            //{
                CargarInfoUsuarioDeCacheOBL();
            //}
        }

        private void CargarInfoUsuarioDeCacheOBL()
        {
            if (this.ControllerContext.HttpContext.Session[SessionKeys.Common.DatosUsuario] != null)
            {
                infoUsuario = this.ControllerContext.HttpContext.Session[SessionKeys.Common.DatosUsuario] as IDatosUsuario;
            }
            else
            {
                infoUsuario = HttpContext.GetCurrentUser().GetDatosUsuario();
                if (infoUsuario == null)
                {
                    throw new SecurityException("El usuario no tiene permisos para acceder a la aplicación");
                }
                else
                {
                    this.ControllerContext.HttpContext.Session[SessionKeys.Common.DatosUsuario] = infoUsuario;
                }
            }
        }

        protected void ActualizarDatosUsuarioEnCache(IDatosUsuario datos)
        {
            this.ControllerContext.HttpContext.Session[SessionKeys.Common.DatosUsuario] = datos;
        }

    }
}