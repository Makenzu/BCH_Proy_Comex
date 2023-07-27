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
using BCH.Comex.UI.Web.Areas.MT300Gestion.Models;
using BCH.Comex.UI.Web.Common;

namespace BCH.Comex.UI.Web.Areas.MT300Gestion.Controllers
{
    public class HomeController : BaseControllerMT300Gestion
    {
        static HomeController()
        {
            new PortalService().RegisterApp("MT300GESTION", "Gestión MT300", "SWIFT",
                "COMEX_MT300_ADMIN", "COMEX_GRP_SWIFT", "MT300Gestion");
        }

        // GET: GeneracionMT300/Home
        [AuthorizeOrForbidden(Roles = "COMEX_MT300_ADMIN")]
        [HttpGet]
        public ActionResult Index()
        {
            using (Tracer tracer = new Tracer("Index de Gestión MT300"))
            {
                tracer.TraceVerbose("Entrando a Index de Gestión MT300...");
                IndexViewModel viewModel = new IndexViewModel();
                viewModel.ListaMensajes = CargarMensajesGlobales();

                viewModel.pageSize = Globales.DatosIndex.PageSize;
                viewModel.rowOffset = Globales.DatosIndex.RowOffset;
                viewModel.referencia = Globales.DatosIndex.Referencia;
                viewModel.cuenta = Globales.DatosIndex.Cuenta;
                viewModel.destino = Globales.DatosIndex.Destino;
                viewModel.fecha = Globales.DatosIndex.Fecha;
                viewModel.usarFiltros = Globales.DatosIndex.UsarFiltros;

                return View(viewModel);
            }
        }

        [AcceptVerbs(HttpVerbs.Post), HandleAjaxException]
        public ActionResult BuscarSwifts(bool? usarFiltros, string referencia, string destino, string cuenta, DateTime? fecha, int? pageSize, int? rowOffset, string sortOrder, string searchText)
        {
            if (!usarFiltros.HasValue)
            {
                usarFiltros = false;
            }

            if (String.IsNullOrEmpty(referencia) && String.IsNullOrEmpty(cuenta) && String.IsNullOrEmpty(destino) && !fecha.HasValue)
            {
                usarFiltros = false;
            }

            if (fecha.HasValue)
            {
                fecha = fecha.Value.Date; //para sacar cualquier componente de hora:min que me pudiera venir de js
            }

            if (!String.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToUpper().Replace("%", "");
            }


            if (!pageSize.HasValue)
            {
                pageSize = 25;
            }

            if (!rowOffset.HasValue)
            {
                rowOffset = 0;
            }

            Globales.DatosIndex.PageSize = pageSize.Value;
            Globales.DatosIndex.RowOffset = rowOffset.Value;
            Globales.DatosIndex.Referencia = referencia;
            Globales.DatosIndex.Cuenta = cuenta;
            Globales.DatosIndex.Destino = destino;
            Globales.DatosIndex.Fecha = fecha;
            Globales.DatosIndex.UsarFiltros = usarFiltros.Value;

            int totalRows = 0;

            var resultado = service.BuscarSwifts(usarFiltros.Value, referencia, destino, cuenta, fecha, rowOffset.Value, pageSize.Value, sortOrder, searchText, out totalRows);

            object jsonData = new { total = totalRows, rows = resultado }; //'total' y 'rows' tienen que llamarse asi, y en ese orden, lo requiere la bootstrap table

            return new JsonResult()
            {
                Data = jsonData,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
