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
using BCH.Comex.Core.Entities.Cext01.MT300Common;
using BCH.Comex.Common.UI_Modulos;

namespace BCH.Comex.UI.Web.Areas.MT300Consulta.Controllers
{
    public class MensajesController : BaseControllerMT300Consulta
    {

        // GET: GeneracionMT300/Mensajes
        [AuthorizeOrForbidden(Roles = "COMEX_MT300_CONSULTA")]
        [HttpGet]
        public ActionResult Detalle(string id)
        {
            using (Tracer tracer = new Tracer("Visualizar MT300"))
            {
                tracer.TraceVerbose("Entrando a detalle de Mensaje MT300 " + id);

                var archivoProcesado = service.GetArchivoProcesado(Decimal.Parse(id));

                string rate = archivoProcesado.rate.ToString("0.#####");
                if (archivoProcesado.rate % 1 == 0)
                {
                    rate = rate + ",";
                }

                DetalleViewModel viewModel = new DetalleViewModel();
                viewModel.Mensaje = new MensajeViewModel()
                {
                    idProcesados = archivoProcesado.id_procesados,
                    reference = archivoProcesado.reference,
                    campo22A = archivoProcesado.campo22A,
                    campo22C = archivoProcesado.campo22C,
                    campo82A = archivoProcesado.campo82A,
                    campo87A = archivoProcesado.campo87A,
                    bookedBy = archivoProcesado.booked_by.ToString("yyyyMMdd"),
                    valueDate = archivoProcesado.value_date.ToString("yyyyMMdd"),
                    rate = rate,
                    campo32B = archivoProcesado.codigo_moneda_mn + archivoProcesado.amount_mn.ToString(),
                    campo53A = archivoProcesado.campo53A,
                    campo57A = archivoProcesado.campo57A,
                    campo33B = archivoProcesado.codigo_moneda_me + archivoProcesado.amount_me.ToString(),
                    campo98D = archivoProcesado.campo98D,
                };

                return View("Detalle", viewModel);
            }
        }
    }
}
