using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_Arbitrajes : UI_Frm
    {
        public bool IrAIngresoValores { set; get; }
        public short En_Load { set; get; }
        public bool VieneDeIngresoValores { set; get; }
        public string nroDeal{get;set;}
        public UI_Frame Frame3D3{set;get;}
        public UI_Grid Lt_Operacion{set;get;}
        public List<UI_Button> Co_Boton{set;get;}
        public UI_CheckBox Ch_Futuro{set;get;}
        public UI_Label Lb_Titulo{set;get;}
        public UI_Frame Frame3D1{set;get;}
        public UI_Button NO{set;get;}
        public UI_Button OK{set;get;}
        public UI_Combo Cb_Moneda_Compra{set;get;}       
        public List<UI_TextBox> Tx_Mtoarb{set;get;}
        public UI_Combo Cb_Moneda_Venta{set;get;}
        public UI_Combo Cb_Pais{set;get;}
        public List<UI_Label> Label1 { set; get; }

        
        public UI_Frm_Arbitrajes()
        {
            VieneDeIngresoValores = false;
            Label1 = new List<UI_Label>() { new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label() };
            Frame3D3 = new UI_Frame();
            Lt_Operacion = new UI_Grid();
            Co_Boton = new List<UI_Button>() { new UI_Button(), new UI_Button()};
            Ch_Futuro = new UI_CheckBox();
            Lb_Titulo = new UI_Label();
            Frame3D1 = new UI_Frame();
            NO = new UI_Button();
            OK = new UI_Button();
            Cb_Moneda_Compra = new UI_Combo();
            Tx_Mtoarb = new List<UI_TextBox>() { new UI_TextBox() { Tag= "_______.____" } , new UI_TextBox() { Tag= "_______.__________" }, new UI_TextBox() {Tag= "_____________.__" }, new UI_TextBox(), new UI_TextBox() { Enabled=false,Tag= "_______.__________" }, new UI_TextBox() { Enabled=false,Tag= "_______.__________" } };
            Cb_Moneda_Venta = new UI_Combo();
            Cb_Pais = new UI_Combo();
        }
    }
}
