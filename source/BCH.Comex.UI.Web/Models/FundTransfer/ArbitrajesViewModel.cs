using System.Collections.Generic;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class ArbitrajesViewModel
    {
        public bool irAIngresoValores { set; get; }
        public string Tx_Mtoarb_005 {set;get;}
        public string Tx_Mtoarb_000 { set; get; }
        public string Tx_Mtoarb_004 { set; get; }
        public string Tx_Mtoarb_002 { set; get; }
        public string Tx_Mtoarb_003 { set; get; }
        public string Tx_Mtoarb_001 { set; get; }
        public string nroDeal { get; set; }
        public int indexPais { set; get; }
        public int idPais { set; get; }
        public List<SelectListItem> Cb_Pais { set; get; }
        public bool Cb_Pais_Habilitado { set; get; }

        public int indexMonedaCompra { set; get; }
        public List<SelectListItem> Cb_Moneda_Compra { set; get; }

        public int indexMonedaVenta { set; get; }
        public List<SelectListItem> Cb_Moneda_Venta { set; get; }

        public List<string> Headers { set; get; }
        public List<Lt_Operacion_Arbitraje_Model> Lt_Operacion { set; get; }

        public List<string> MensajesDeError { set; get; }
        public List<string> MensajesDeConfirmacion { set; get; }

        public bool Ch_Futuro { set; get; }
    }
}