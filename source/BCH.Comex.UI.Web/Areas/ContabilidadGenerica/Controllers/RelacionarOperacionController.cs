using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Models;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Controllers
{
    public class RelacionarOperacionController : BaseControllerCG
    {
        //
        // GET: /ContabilidadGenerica/RelacionarOperacion/
        public ActionResult Index()
        {
            using (Tracer tracer = new Tracer())
            {
                if (this.Globales.FrmgAso == null)
                    this.Globales.FrmgAso = new FrmgAsoDTO();

                //service.RelacionarOperacionInit(this.Globales);
               
                return View();
            }
        }
        
        public JsonResult FrmgAso_LoadFrm()
        {
            using (Tracer tracer = new Tracer("RelacionarOperacion - FrmgAso_LoadFrm"))
            {
                if (this.Globales.FrmgAso == null)
                    this.Globales.FrmgAso = new FrmgAsoDTO();

                service.RelacionarOperacionInit(this.Globales);
                RelacionarOperacionViewModel vm = new RelacionarOperacionViewModel(this.Globales);
                return Json(vm, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, HandleError]
        public JsonResult FrmgAso_Cb_Producto_Click(RelacionarOperacionViewModel jsonModel)
        {
            using (Tracer tracer = new Tracer("RelacionarOperacion - FrmgAso_Cb_Producto_Click"))
            {
                Globales.MESSAGES.Clear();
                if (this.Globales.FrmgAso == null)
                    this.Globales.FrmgAso = new FrmgAsoDTO();

                RelacionarOperacionViewModel vm = new RelacionarOperacionViewModel(this.Globales);
                vm.Update(jsonModel, this.Globales);
                service.RelacionarOperacion_Cb_Producto_Click(this.Globales);
                this.Globales.FrmgAso.Errores = this.Globales.MESSAGES;
                vm = new RelacionarOperacionViewModel(this.Globales);                
                return Json(vm, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, HandleError]
        public JsonResult FrmgAso_ok_Click(RelacionarOperacionViewModel jsonModel)
        {
            using (Tracer tracer = new Tracer("RelacionarOperacion - FrmgAso_ok_Click"))
            {
                Globales.MESSAGES.Clear();
                RelacionarOperacionViewModel vm = new RelacionarOperacionViewModel(this.Globales);
                vm.Update(jsonModel, this.Globales);
                service.RelacionarOperacion_OkClick(this.Globales);
                this.Globales.FrmgAso.Errores = this.Globales.MESSAGES;        
                return Json(this.Globales.FrmgAso, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public JsonResult FrmgAso_Aceptar_Click(RelacionarOperacionViewModel jsonModel)
        {
            using (Tracer tracer = new Tracer("RelacionarOperacion - FrmgAso_Aceptar_Click"))
            {
                Globales.MESSAGES.Clear();
                RelacionarOperacionViewModel vm = new RelacionarOperacionViewModel(this.Globales);
                vm.Update(jsonModel, this.Globales);
                service.RelacionarOperacion_AceptarClick(this.Globales);
                return Json(this.Globales.FrmgAso, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet, HandleError]
        public void FrmgAso_Cancelar_Click()
        {
            this.Globales.FrmgAso = null;
        }

	}
}