using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.Planillas.Models
{

    public class ReferenciasBAEBCHViewModel
    {
        public IEnumerable<ItemReferenciasBAEBCHViewModel> Detalle { get; set; }   
    }

    public class ItemReferenciasBAEBCHViewModel
    {
        public string OperacionBancoChile { get; set; }
        public string ReferenciaNueva { get; set; }
    }
}