using BCH.Comex.Core.BL.XGFD;
using BCH.Comex.Core.Entities.Cext01.FinDia;
using BCH.Comex.UI.Web.Common;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.FinDia.Controllers
{
    public class BaseController : Controller
    {

        protected FinDiaService fdService = new FinDiaService();

        /// <summary>
        /// Datos globales del modulo Fin Dia
        /// </summary>
        protected DatosGlobales Globales
        {
            get
            {
                var dato = Session[SessionKeys.FinDia.DatosGlobalesKey] as DatosGlobales;
                //if (dato == null)
                //    Session[SessionKeys.Devengo.DatosGlobalesKey] = dato = fdService.Iniciar(HttpContext.GetCurrentUser().GetDatosUsuario());
                return dato;
            }
            set
            {
                Session[SessionKeys.FinDia.DatosGlobalesKey] = value;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Se agrega validacion para que se salte la verificacion de fin de dia cuando sea el listado de operaciones
            if (!(filterContext.ActionDescriptor.ActionName == "ImpresionListado"))
                Common.Utils.ValidarEjecucionFinDia(Session);

            base.OnActionExecuting(filterContext);
        }
        
	}
}