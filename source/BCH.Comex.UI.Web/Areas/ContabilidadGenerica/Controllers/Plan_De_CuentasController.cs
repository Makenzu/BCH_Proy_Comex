using System.Linq;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Controllers
{
    public class Plan_De_CuentasController : BaseControllerCG
    {
        // GET: ContabilidadGenerica/Plan_De_Cuentas
        public ActionResult Ver()
        {
            return Rutear(this.service.CTA_Form_Load, (g) => View(g.Frm_Cta));
        }

        public JsonResult Get_Ctas()
        {
            return Json(this.Globales.Frm_Cta.Lista.Items.Select(x => x.columns), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Aceptar(string num, string nem, string desc)
        {
            this.Globales.Frm_Cta.NUM = num;
            this.Globales.Frm_Cta.NEM = nem;
            this.Globales.Frm_Cta.DESC = desc;
            return Rutear(this.service.CTA_Aceptar, null,false);
        }

        public ActionResult Cancelar()
        {
            return Rutear(this.service.CTA_Cancelar, null);
        }
    }
}