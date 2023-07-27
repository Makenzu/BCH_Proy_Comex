using System.Collections.Generic;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Controllers
{
    public class ChequeController : BaseControllerCG
    {
        // GET: ContabilidadGenerica/Cheque
        public ActionResult Emision()
        {
            return Rutear(this.service.CHQ_FormLoad, (g) => View(g.FrmgChq));
        }

        public JsonResult Benef(int id)
        {
            this.Globales.FrmgChq.l_benef.ListIndex = id-1;
            this.service.CHQ_L_Benef_Click(this.Globales);
            this.Globales.FrmgChq.ListaErrores = new List<Comex.Common.UI_Modulos.UI_Message>(this.Globales.MESSAGES);
            this.Globales.MESSAGES.Clear();
            return Json(this.Globales.FrmgChq, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Plaza(int id)
        {
            this.Globales.FrmgChq.l_plaza.ListIndex = id-1;
            this.service.CHQ_L_Plaza_Click(this.Globales);
            this.Globales.FrmgChq.ListaErrores = new List<Comex.Common.UI_Modulos.UI_Message>(this.Globales.MESSAGES);
            this.Globales.MESSAGES.Clear();

            this.Globales.FrmgChq.l_cor.Items.ForEach(x => x.Value = x.Value.Replace(" ", "\xA0").Replace("\t", "\xA0\xA0\xA0\xA0\xA0\xA0\xA0\xA0"));

            return Json(this.Globales.FrmgChq, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Generar(List<string> jsonModel)
        {

            string nom = jsonModel[0];
            string rut = jsonModel[1];
            int indiceCor = int.Parse(jsonModel[2]);

            this.Globales.FrmgChq.Tx_Nombre.Text = nom;
            this.Globales.FrmgChq.Tx_Rut.Text = rut;
            this.Globales.FrmgChq.l_cor.ListIndex = indiceCor;

            this.service.CHQ_Generar(this.Globales);
            this.Globales.FrmgChq.ListaErrores = new List<Comex.Common.UI_Modulos.UI_Message>(this.Globales.MESSAGES);
            this.Globales.MESSAGES.Clear();

            return Json(this.Globales.FrmgChq, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Aceptar()
        {
            return Rutear(this.service.CHQ_Aceptar, null);
        }

        public JsonResult Cancelar(bool? resMsg)
        {
            this.Globales.vieneDeMsg = resMsg.HasValue;
            this.Globales.resMsg = resMsg.HasValue && resMsg.Value;

            this.service.CHQ_Cancelar(this.Globales);
            this.Globales.FrmgChq.ListaConfirmaciones = new List<Comex.Common.UI_Modulos.UI_Message>(this.Globales.MESSAGES);
            this.Globales.MESSAGES.Clear();
            return Json(this.Globales.FrmgChq, JsonRequestBehavior.AllowGet);
        }

    }
}