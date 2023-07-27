using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.UI.Web.Areas.Supervisor.Models;
using BCH.Comex.UI.Web.Common;
using System;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Supervisor.Controllers
{
    public class ProductividadController : BaseController
    {
        //
        // GET: /Supervisor/Productividad/
        public ActionResult Index()
        {
            using (Tracer tracer = new Tracer())
            {
                this.Globales.ListaMensajesError.Clear();

                if (this.Globales.FrmAyM == null)
                    this.Globales.FrmAyM = new FrmAyMDTO();

                service.ProductividadInit(this.Globales);
                ProductividadViewModel evm = new ProductividadViewModel(this.Globales);
                return View(evm);
            }
        }


        public ActionResult DetalleProductividad(int anio, int mes, bool? generarHtmlCompleto, bool? imprimir)
        {
            using (Tracer tracer = new Tracer())
            {
                try
                {
                    this.Globales.ListaMensajesError.Clear();

                    service.Productividad_ImprimirClick(anio, mes + 1, this.Globales);
                    ViewBag.Detalle = this.Globales.FrmAyM.stringHtml;
                    ViewBag.GenerarHtmlCompleto = (generarHtmlCompleto.HasValue && generarHtmlCompleto.Value);
                    ViewBag.Imprimir = (imprimir.HasValue && imprimir.Value);
                    return View();
                }
                catch (Exception ex)
                {
                    return new RedirectResult("~/Supervisor/Productividad");
                }
            }
            
        }


        [HttpPost]
        public ActionResult Index(ProductividadViewModel pvm, string command)
        {
            using (Tracer tracer = new Tracer())
            {
                this.Globales.ListaMensajesError.Clear();

                if (command == "Cancelar")
                    return new RedirectResult("~/Supervisor");

                return View();
            }
        }

	}
}