using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frmgnota
    {
        public List<UI_TextBox> Tx_NumOpe;
        public List<UI_Button> Boton;
        public List<UI_Label> Label1;
        public UI_TextBox Tx_RutPrt;
        public UI_TextBox Tx_NomPrt;
        public UI_TextBox Tx_DirPrt;
        public UI_Combo Cb_Producto;
        public UI_Button ok;
        public int selected_Producto_ID;
        public UI_TextBox Tx_NroFac ;
        public UI_TextBox Tx_NroRep ;
        public UI_TextBox Tx_tipo  ;
        public UI_TextBox Tx_FecRep ;
        public UI_TextBox Tx_Moneda ;
        public UI_TextBox Tx_Neto  ;
        public UI_TextBox Tx_iva ;
        public UI_TextBox Tx_MtoOri ;

        public UI_Frmgnota() {
            Tx_NumOpe = new List<UI_TextBox>() 
                                            {   new UI_TextBox(), 
                                                new UI_TextBox(), 
                                                new UI_TextBox(), 
                                                new UI_TextBox(), 
                                                new UI_TextBox(), 
                                                new UI_TextBox(), 
                                                new UI_TextBox() };
            Boton = new List<UI_Button>() { new UI_Button(), new UI_Button() };
            Label1 = new List<UI_Label>();
            Tx_RutPrt = new UI_TextBox() { Enabled = false };
            Tx_NomPrt = new UI_TextBox() { Enabled = false };
            Tx_DirPrt = new UI_TextBox() { Enabled = false };
            Cb_Producto = new UI_Combo();
            ok = new UI_Button();

            Tx_NroFac = new UI_TextBox() { Enabled = false};
            Tx_NroRep = new UI_TextBox() { Enabled = false };
            Tx_tipo = new UI_TextBox() { Enabled = false };
            Tx_FecRep = new UI_TextBox() { Enabled = false };
            Tx_Moneda = new UI_TextBox() { Enabled = false };
            Tx_Neto = new UI_TextBox() { Enabled = false };
            Tx_iva = new UI_TextBox() { Enabled = false };
            Tx_MtoOri = new UI_TextBox() { Enabled = false };
        }
    }
}
