using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.BL.SWBO;
using BCH.Comex.Core.BL.SWEM;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using BCH.Comex.UI.Web.Models.EliminacionMasiva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Controllers
{
    public class EliminacionMasivaController : BaseController
    {
        private SwiftMgr bl;
        private SwboService swboService;
        private const string EliminacionMasivaAppRole = "COMEX_SWIFT_SWBO";

        static EliminacionMasivaController()
        {
            new PortalService().RegisterApp("SWBO", "Eliminación Masiva de mensajes Swift", "SWIFT", EliminacionMasivaAppRole, "COMEX_GRP_SWIFT", "EliminacionMasiva");
        }

        public EliminacionMasivaController()
        {
            this.bl = new SwiftMgr();
            this.swboService = new SwboService();
        }

        // GET: EliminacionMasiva
        [AuthorizeOrForbidden(Roles = EliminacionMasivaAppRole)]
        public ActionResult Index()
        {
            //var result = bl.GetTodasLasCasillas();
            //SelectList todasLasCasillas = new SelectList(result.OrderBy(i => i.cod_casilla).ToList(), "cod_casilla", "DataTextField");
            //var model = new IndexModel();
            //model.TodasLasCasillas = todasLasCasillas;
            //return View(model);
            return View();
        }

        public JsonResult LimpiarCasilla(int idCasilla)
        {
            swboService.EliminarCasilla(idCasilla);
            return Json(string.Empty);
        }

        public ActionResult EliminarMensaje()
        {
            return View();
        }

        public JsonResult BuscaMensajeEliminar(int sesion, int secuencia)
        {
            var result = swboService.BuscaEliminar(sesion, secuencia);
            return Json(new { Casilla = result.casilla, Estado = result.estado_msg }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminaMensaje(int sesion, int secuencia)
        {
            swboService.EliminarMensaje(sesion, secuencia);
            return Json(string.Empty);
        }

    }
}