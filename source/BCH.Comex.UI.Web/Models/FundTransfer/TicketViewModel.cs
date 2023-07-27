using System.Collections.Generic;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class TicketViewModel
    {
        public string CAM_Nombre { set; get; }
        public string CAM_Nemonico { set; get; }
        public string CAM_Monto { set; get; }
        public string CAM_Cuenta { set; get; }
        public List<SelectListItem> CBO_DeHa { set; get; }
        public int selected_cbo_deha_id { set; get; }
        public List<SelectListItem> Cb_ticket { set; get; }
        public int selected_cb_ticket_id { set; get; }
        public bool otro { set; get; }
        public string CAM_Concepto { set; get; }
    }
}