using BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Controllers
{
    public class GrabarController : BaseControllerCG
    {
        public ActionResult Inicicar()
        {
            return Rutear(this.service.Grabar_Inicial, null);
        }
        public ActionResult Ticket_Post()
        {
            return Rutear(this.service.Grabar_Ticket, null);
        }

        public ActionResult Swift()
        {
            return Rutear(this.service.Grabar_Generar_Swift_1, null);
        }

        public ActionResult MT()
        {
            return Rutear(this.service.Grabar_Generar_Swift_2, null);
        }

        public ActionResult Despues_De_MT()
        {
            return Rutear(this.service.Grabar_Generar_Swift_3, null);
        }

        public ActionResult Cheques()
        {
            return Rutear(this.service.Grabar_Generar_Cheques_1_2, null);
        }

        public ActionResult Despues_De_Cheques()
        {
            return Rutear(this.service.Grabar_Generar_Cheques_2_2, null);
        }

        public ActionResult Tickets()
        {
            return Rutear(this.service.Grabar_Ticket, null);
        }

        public ActionResult Final()
        {
            return Rutear(this.service.Grabar_Final, null);
        }

        public ActionResult Imprimir()
        {
            var imprimirModel = new ImprimirModel();
            imprimirModel.Documentos = new List<string>(this.Globales.DocumentosAImprimir);
            this.Globales.DocumentosAImprimir.Clear();
            imprimirModel.FileName = (string.IsNullOrEmpty(Globales.FileName)) ? Globales.MODGCON1.V_IMch.CodCct + Globales.MODGCON1.V_IMch.CodPro + Globales.MODGCON1.V_IMch.CodEsp + Globales.MODGCON1.V_IMch.CodOfi + Globales.MODGCON1.V_IMch.CodOpe: Globales.FileName;
            this.Globales.FileName = string.Empty;
            return Rutear(null, (g) => View(imprimirModel), false);
        }
    }
}