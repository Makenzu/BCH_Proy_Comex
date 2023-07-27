using BCH.Comex.Common;
using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.Entities.Cext01.BusquedaPlanilla;
using BCH.Comex.UI.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using BCH.Comex.UI.Web.Common;

namespace BCH.Comex.UI.Web.Areas.BusquedaPlanilla.Controllers
{
    public class InicioController : BaseController
    {

        static InicioController()
        {
            new PortalService().RegisterApp("XEVA", "Búsqueda de planillas asociadas a operaciones Comex", "Generales",
                Constantes.AppRoles.BusquedaPlanillaAppRole, "COMEX_GRP_GENERAL", "BusquedaPlanilla");
        }

        //
        // GET: /BusquedaPlanilla/Inicio/
        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.BusquedaPlanillaAppRole)]
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post), HandleAjaxException]
        public JsonResult BuscarOperacion(string numpre, string cui, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            try
            {
                var data = service.BuscarOperacion(numpre, cui, fechaDesde, fechaHasta);
                return new JsonResult()
                {
                    Data = data,
                    MaxJsonLength = int.MaxValue
                };
            }
            catch (KeyNotFoundException ex) { throw ex; }
            catch (Exception ex)
            {
                throw new ArgumentException("Se ha producido un error al tratar de leer los Datos de las Planillas. El Servidor reporta :" + ex.Message);
            }
        }
	}
}