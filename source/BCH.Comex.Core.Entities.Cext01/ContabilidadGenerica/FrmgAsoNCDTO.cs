using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica
{
    public class FrmgAsoNCDTO : FrmgAsoDTO
    {
        public List<FrmgNCFacturasDTO> Facturas { get; set; }
        
        public FrmgAsoNCDTO()
        {
            Cb_Producto = new UI_Combo();
            Tx_NumOpe_000 = new UI_TextBox();
            Tx_NumOpe_001 = new UI_TextBox();
            Tx_NumOpe_002 = new UI_TextBox();
            Tx_NumOpe_003 = new UI_TextBox();
            Tx_NumOpe_004 = new UI_TextBox();
            Tx_NumOpe_005 = new UI_TextBox();
            Tx_NumOpe_006 = new UI_TextBox();
            Tx_DirPrt = new UI_TextBox();
            Tx_NomPrt = new UI_TextBox();
            Tx_RutPrt = new UI_TextBox();

            Facturas = new List<FrmgNCFacturasDTO>();
        }
    }

    public class FrmgNCFacturasDTO
    {
        public string NroFactura { get; set; }
        public string FechaFactura { get; set; }
        public string NroReporte { get; set; }
        public string Moneda { get; set; }
        public string Neto { get; set; }
        public string Iva { get; set; }
        public string Total { get; set; }
        public string Tipo { get; set; }
    }
}
