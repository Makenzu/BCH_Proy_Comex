using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class EmitirNotaCreditoViewModel
    {

        public UI_TextBox Tx_NumOpe_000 { set; get; }
        public UI_TextBox Tx_NumOpe_001 { set; get; }
        public UI_TextBox Tx_NumOpe_002 { set; get; }
        public UI_TextBox Tx_NumOpe_003 { set; get; }
        public UI_TextBox Tx_NumOpe_004 { set; get; }
        public UI_TextBox Tx_NumOpe_005 { set; get; }
        public UI_TextBox Tx_NumOpe_006 { set; get; }
        public UI_Button Boton_000 { set; get; }
        public UI_Button Boton_001 { set; get; }
        public UI_Combo Cb_Producto { set; get; }
        public int selected_Producto_ID { set; get; }
        public UI_TextBox Tx_RutPrt { set; get; }
        public UI_TextBox Tx_NomPrt { set; get; }
        public UI_TextBox Tx_DirPrt { set; get; }
        public UI_TextBox Tx_NroFac { set; get; }
        public UI_TextBox Tx_NroRep { set; get; }
        public UI_TextBox Tx_tipo { set; get; }
        public UI_TextBox Tx_FecRep { set; get; }
        public UI_TextBox Tx_Moneda { set; get; }
        public UI_TextBox Tx_Neto { set; get; }
        public UI_TextBox Tx_Iva { set; get; }
        public UI_TextBox Tx_MtoOri { set; get; }
        

        public EmitirNotaCreditoViewModel()
        {
        }

        public EmitirNotaCreditoViewModel(UI_Frmgnota Fgn)
        {
            Tx_NumOpe_000 = Fgn.Tx_NumOpe[0];
            Tx_NumOpe_001 = Fgn.Tx_NumOpe[1];
            Tx_NumOpe_002 = Fgn.Tx_NumOpe[2];
            Tx_NumOpe_003 = Fgn.Tx_NumOpe[3];
            Tx_NumOpe_004 = Fgn.Tx_NumOpe[4];
            Tx_NumOpe_005 = Fgn.Tx_NumOpe[5];
            Tx_NumOpe_006 = Fgn.Tx_NumOpe[6];
            Boton_000 = Fgn.Boton[0];
            Boton_001 = Fgn.Boton[1];
            Cb_Producto = Fgn.Cb_Producto;
            Tx_RutPrt = Fgn.Tx_RutPrt;
            Tx_NomPrt = Fgn.Tx_NomPrt;
            Tx_DirPrt = Fgn.Tx_DirPrt;

            Tx_NroFac = Fgn.Tx_NroFac;
            Tx_NroRep = Fgn.Tx_NroRep;
            Tx_tipo   = Fgn.Tx_tipo;
            Tx_FecRep = Fgn.Tx_FecRep;
            Tx_Moneda = Fgn.Tx_Moneda;
            Tx_Neto   = Fgn.Tx_Neto;
            Tx_Iva    = Fgn.Tx_iva;
            Tx_MtoOri = Fgn.Tx_MtoOri;
        }

        public void Update(UI_Frmgnota Fgn)
        {
            Fgn.Tx_NumOpe[0] = Tx_NumOpe_000 != null ? Tx_NumOpe_000 : Fgn.Tx_NumOpe[0];
            Fgn.Tx_NumOpe[1] = Tx_NumOpe_001 != null ? Tx_NumOpe_001 : Fgn.Tx_NumOpe[1];
            Fgn.Tx_NumOpe[2] = Tx_NumOpe_002 != null ? Tx_NumOpe_002 : Fgn.Tx_NumOpe[2];
            Fgn.Tx_NumOpe[3] = Tx_NumOpe_003 != null ? Tx_NumOpe_003 : Fgn.Tx_NumOpe[3];
            Fgn.Tx_NumOpe[4] = Tx_NumOpe_004 != null ? Tx_NumOpe_004 : Fgn.Tx_NumOpe[4];
            Fgn.Tx_NumOpe[5] = Tx_NumOpe_005 != null ? Tx_NumOpe_005 : Fgn.Tx_NumOpe[5];
            Fgn.Tx_NumOpe[6] = Tx_NumOpe_006 != null ? Tx_NumOpe_006 : Fgn.Tx_NumOpe[6];
        }
    }
}