using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms;
using BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Models;
using System.Web.Mvc;
using System.Collections.Generic;
namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Controllers
{
    public class AnularContabilidadController : BaseControllerCG
    {
        //
        // GET: /ContabilidadGenerica/AnularContabilidad/
        public ActionResult Index()
        {
            using (Tracer tracer = new Tracer())
            {
                if (this.Globales.FrmGlanu == null)
                    this.Globales.FrmGlanu = new UI_FrmGlanu();
                return View();
            }
        }

        public JsonResult FrmGlanu_Init()
        {
            using (Tracer tracer = new Tracer("Anular_Contabilidad_Generica - FrmGlanu_Init"))
            {
                if (this.Globales.FrmGlanu == null)
                    this.Globales.FrmGlanu = new UI_FrmGlanu();
                AnularContabilidadViewModel vm = new AnularContabilidadViewModel(this.Globales);
                return Json(vm, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult FrmGlanu_AceptarClick(AnularContabilidadViewModel jsonModel)
        {
            using (Tracer tracer = new Tracer("Anular_Contabilidad_Generica - FrmGlanu_AceptarClick"))
            {
                if (this.Globales.FrmGlanu == null)
                    this.Globales.FrmGlanu = new UI_FrmGlanu();
                this.Globales.MESSAGES.Clear();
                AnularContabilidadViewModel vm = new AnularContabilidadViewModel();
                vm.Update(jsonModel, this.Globales);
                service.AnularContabilidad_AceptarClick(this.Globales);
                this.Globales.FrmGlanu.RptContable = new List<string>(this.Globales.DocumentosAImprimir);                
                return Json(new AnularContabilidadViewModel(this.Globales), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult FrmGlanu_OkClick(AnularContabilidadViewModel jsonModel)
        {
            using (Tracer tracer = new Tracer("Anular_Contabilidad_Generica - FrmGlanu_OkClick"))
            {
                if (this.Globales.FrmGlanu == null)
                    this.Globales.FrmGlanu = new UI_FrmGlanu();
                this.Globales.FrmGlanu.ListaErrores.Clear();
                AnularContabilidadViewModel vm = new AnularContabilidadViewModel();
                vm.Update(jsonModel, this.Globales);
                service.AnularContabilidad_OkClick(this.Globales);                
                return Json(new AnularContabilidadViewModel(this.Globales), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Imprimir()
        {
            var imprimirModel = new ImprimirModel();
            imprimirModel.Documentos = new List<string>(this.Globales.DocumentosAImprimir);
            imprimirModel.FileName = Globales.MODGCON1.V_IMch.CodCct + Globales.MODGCON1.V_IMch.CodPro + Globales.MODGCON1.V_IMch.CodEsp + Globales.MODGCON1.V_IMch.CodOfi + Globales.MODGCON1.V_IMch.CodOpe;
            this.Globales.DocumentosAImprimir.Clear();
            Globales.FileName = string.Empty;
            return Rutear(null, (g) => View(imprimirModel));

        }

        [HttpGet, HandleError]
        public void FrmGlanu_CancelarClick()
        {
            this.Globales.FrmGlanu = null;
        }
    }
}