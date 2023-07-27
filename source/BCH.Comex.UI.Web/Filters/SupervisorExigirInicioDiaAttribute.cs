using BCH.Comex.Core.BL.XGID;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BCH.Comex.UI.Web.Helpers;
using BCH.Comex.Core.Entities.Portal;
using System;

namespace BCH.Comex.UI.Web.Common
{
    /// <summary>
    /// Este filtro es para los controllers de Supervisor, se fija si el usuario inicio el dia y si no lo hizo lo redirije al index de Supervisor 
    /// (que ya esta mostrando un warning de que el usuario no inicio el dia y que debe iniciar el dia para operar)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SupervisorExigirInicioDiaAttribute :  ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            InicioDiaService service = new InicioDiaService();
            IDatosUsuario usuario = filterContext.HttpContext.GetCurrentUser().GetDatosUsuario();

            bool? diaIniciado = service.DiaIniciado(usuario);

            if(diaIniciado.HasValue && diaIniciado.Value)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Supervisor/");
            }
        }
    }
}