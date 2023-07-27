using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class ComercioVisibleExportViewModel
    {
        public string Caption { set; get; }
        [StringLength(8)]
        public string Tx_MtoVisE_000 { set; get; }
        [StringLength(15)]
        public string Tx_MtoVisE_001 { set; get; }
        [StringLength(15)]
        public string Tx_MtoVisE_002 { set; get; }
        [StringLength(15)]
        public string Tx_MtoVisE_003 { set; get; }
        public List<SelectListItem> Cb_Mnd { set; get; }
        public int indexCbMnd { set; get; }
        public int idCbMnd { set; get; }
        public List<UI_Message> MensajesDeError { set; get; }
    }
}
