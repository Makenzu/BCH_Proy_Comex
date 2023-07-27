using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_VisE
    {
        public UI_Button[] Co_Boton;
        public UI_TextBox[] Tx_MtoVisE;
        public UI_Label[] Lb_Titulo;
        public UI_Frame Frame1;
        public UI_Combo Cb_Mnd;
        public string Caption;
        public List<UI_Message> Messages;

        public UI_Frm_VisE()
        {
            Co_Boton = new UI_Button[2] { new UI_Button(), new UI_Button() };
            Tx_MtoVisE = new UI_TextBox[4] { new UI_TextBox(), new UI_TextBox(), new UI_TextBox(), new UI_TextBox() };
            Cb_Mnd = new UI_Combo();
            Messages = new List<UI_Message>();
            Caption = "Comercio Visible Exportaciones";
        }
    }
}
