using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Controllers
{
    public class TicketsController : BaseControllerCG
    {
        // GET: ContabilidadGenerica/Tickets
        public ActionResult Ingreso()
        {
            return Rutear(this.service.Ticket_Load, (g)=>View(g.Tickets));
        }

        private void Traspass(UI_Tickets old,UI_Tickets neew)
        {
            neew.IMPUESTO = old.IMPUESTO;
            neew.TIP = old.TIP;
            neew.S = old.S;
            neew.MONTO = old.MONTO;
            neew.A = old.A;
            neew.ST = old.ST;
        }

        [HttpPost]
        public JsonResult Aceptar(UI_Tickets jsonModel)
        {
            Traspass(this.Globales.Tickets, jsonModel);
            this.Globales.Tickets = jsonModel;
            this.service.Ticket_Aceptar(this.Globales);
            this.Globales.Tickets.ListaErrores = new List<Comex.Common.UI_Modulos.UI_Message>(Globales.MESSAGES);
            Globales.MESSAGES.Clear();
            return Json(this.Globales.Tickets);
        }

        public ActionResult Cancelar()
        {
            return Rutear(this.service.Ticket_Cancelar, null);
        }

        [HttpPost]
        public JsonResult otro_Change(UI_Tickets jsonModel)
        {
            Traspass(this.Globales.Tickets, jsonModel);
            this.Globales.Tickets = jsonModel;
            this.service.Ticket_Otro_Click(this.Globales);
            this.Globales.Tickets.ListaErrores = new List<Comex.Common.UI_Modulos.UI_Message>(Globales.MESSAGES);
            Globales.MESSAGES.Clear();
            return Json(this.Globales.Tickets);
        }

        public JsonResult Get_Form()
        {
            return Json(this.Globales.Tickets, JsonRequestBehavior.AllowGet);
        }
    }
}