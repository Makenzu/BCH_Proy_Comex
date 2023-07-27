using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Models;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Controllers
{
    public class EmitirNotaCreditoController : BaseControllerCG
    {
        //
        // GET: /ContabilidadGenerica/EmitirNotaCredito/
        public ActionResult Index()
        {
            EmitirNotaCreditoViewModel vm = new EmitirNotaCreditoViewModel();
            return View(vm);
        }

        public JsonResult FrmgAsoNC_LoadFrm()
        {
            using (Tracer tracer = new Tracer("EmitirNotaCredito - FrmgAsoNC_LoadFrm"))
            {
                if (this.Globales.FrmgAsoNC == null)
                    this.Globales.FrmgAsoNC = new FrmgAsoNCDTO();
                this.Globales.MESSAGES.Clear();
                service.EmitirNotaCreditoInit(this.Globales);
                EmitirNotaCreditoViewModel vm = new EmitirNotaCreditoViewModel(this.Globales);
                return Json(vm, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult FrmgAsoNC_Cb_Producto_Click()
        {
            using (Tracer tracer = new Tracer("EmitirNotaCredito - FrmgAsoNC_Cb_Producto_Click"))
            {
                if (this.Globales.FrmgAsoNC == null)
                    this.Globales.FrmgAsoNC = new FrmgAsoNCDTO();
                this.Globales.MESSAGES.Clear();
                service.EmitirNotaCredito_Cb_Producto_Click(this.Globales);
                EmitirNotaCreditoViewModel vm = new EmitirNotaCreditoViewModel(this.Globales);
                return Json(vm, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult FrmgAsoNC_ok_Click(EmitirNotaCreditoViewModel jsonModel)
        {
            using (Tracer tracer = new Tracer("EmitirNotaCredito - FrmgAsoNC_ok_Click"))
            {
                this.Globales.MESSAGES.Clear();
                EmitirNotaCreditoViewModel vm = new EmitirNotaCreditoViewModel();
                vm.Update(jsonModel, this.Globales);
                service.EmitirNotaCredito_OkClick(this.Globales);


                return Json(new EmitirNotaCreditoViewModel(this.Globales), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult FrmgAsoNC_Aceptar_Click(EmitirNotaCreditoViewModel jsonModel, FrmgNCFacturasDTO jsonFactura)
        {
            using (Tracer tracer = new Tracer("EmitirNotaCredito - FrmgAsoNC_Aceptar_Click"))
            {
                this.Globales.MESSAGES.Clear();
                EmitirNotaCreditoViewModel vm = new EmitirNotaCreditoViewModel();
                vm.Update(jsonModel, jsonFactura, this.Globales);
                service.EmitirNotaCredito_Aceptar(this.Globales);
                return Json(vm, JsonRequestBehavior.AllowGet);
            }
        }
	}
}