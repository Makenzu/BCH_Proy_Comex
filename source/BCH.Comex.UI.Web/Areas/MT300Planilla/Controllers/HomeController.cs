using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.Portal;
using BCH.Comex.UI.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Globalization;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Areas.MT300Planilla.Models;

namespace BCH.Comex.UI.Web.Areas.MT300Planilla.Controllers
{
    public class HomeController : BaseControllerMT300Planilla
    {
        static HomeController()
        {
            new PortalService().RegisterApp("MT300PLANILLA", "Planillas MT300", "SWIFT",
                "COMEX_MT300_ADMIN", "COMEX_GRP_SWIFT", "MT300Planilla");
        }

        // GET: GeneracionMT300/Home
        [AuthorizeOrForbidden(Roles = "COMEX_MT300_ADMIN")]
        [HttpGet]
        public ActionResult Index()
        {
            using (Tracer tracer = new Tracer("Index de Planillas cargadas MT300"))
            {
                tracer.TraceVerbose("Entrando a Index de planillas cargadas MT300...");
                IndexViewModel viewModel = new IndexViewModel();
                return View(viewModel);
            }
        }

        // GET: GeneracionMT300/Home/detallePlanilla
        [AuthorizeOrForbidden(Roles = "COMEX_MT300_ADMIN")]
        [HttpGet]
        public ActionResult detallePlanilla()
        {
            using (Tracer tracer = new Tracer("Index de Planillas cargadas MT300"))
            {
                tracer.TraceVerbose("Entrando a Index de planillas cargadas MT300...");
                DetallePlanillaViewModel viewModel = new DetallePlanillaViewModel();
                return View(viewModel);
            }
        }


        [AcceptVerbs(HttpVerbs.Post), HandleAjaxException]
        public ActionResult BuscarArchivos(DateTime? fecha, int? pageSize, int? rowOffset, string sortOrder)
        {
            if (fecha.HasValue)
            {
                fecha = fecha.Value.Date; //para sacar cualquier componente de hora:min que me pudiera venir de js
            }

            if (!pageSize.HasValue)
            {
                pageSize = 25;
            }

            if (!rowOffset.HasValue)
            {
                rowOffset = 0;
            }

            int totalRows = 0;

            var resultado = service.BuscarArchivos(fecha, rowOffset.Value, pageSize.Value, sortOrder, out totalRows);

            object jsonData = new { total = totalRows, rows = resultado }; //'total' y 'rows' tienen que llamarse asi, y en ese orden, lo requiere la bootstrap table

            return new JsonResult()
            {
                Data = jsonData,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [AcceptVerbs(HttpVerbs.Post), HandleAjaxException]
        public ActionResult TraeResumen(DateTime? fechafiltro)
        {
            if (fechafiltro.HasValue)
            {
                fechafiltro  = fechafiltro.Value.Date; //para sacar cualquier componente de hora:min que me pudiera venir de js
            }


            var resultado = service.TraeResumen(fechafiltro);

            object jsonData = new { resumen = resultado }; 

            return new JsonResult()
            {
                Data = jsonData,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [AcceptVerbs(HttpVerbs.Post), HandleAjaxException]
        public ActionResult BuscarDetalleArchivos(decimal? id_archivo, int? pageSize, int? rowOffset, string sortOrder)
        {

            if (!pageSize.HasValue)
            {
                pageSize = 25;
            }

            if (!rowOffset.HasValue)
            {
                rowOffset = 0;
            }

            int totalRows = 0;

            var resultado = service.BuscarDetalleArchivo(id_archivo.Value, rowOffset.Value, pageSize.Value, sortOrder, out totalRows);

            object jsonData = new { total = totalRows, rows = resultado }; //'total' y 'rows' tienen que llamarse asi, y en ese orden, lo requiere la bootstrap table

            return new JsonResult()
            {
                Data = jsonData,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [AcceptVerbs(HttpVerbs.Post), HandleAjaxException]
        public ActionResult TraeResumenDetalle(decimal? id_archivo)
        {


            var resultado = service.TraeDetalleResumen(id_archivo);

            object jsonData = new { resumen = resultado };

            return new JsonResult()
            {
                Data = jsonData,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }





    }
}
