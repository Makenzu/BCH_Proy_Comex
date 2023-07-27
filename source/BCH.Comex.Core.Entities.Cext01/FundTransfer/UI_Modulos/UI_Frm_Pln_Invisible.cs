using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_Pln_Invisible
    {

        public List<UI_TextBox> Tx_Planilla;
        public string Tx_Modificable { get; set; }
        public List<UI_Button> Boton;
        public UI_Button Atras;
        public UI_Button Adelante;
        public UI_CheckBox Ch_ZonFra;
        public UI_TextBox Tx_SecBen;
        public UI_TextBox Tx_SecInv;
        public UI_TextBox Tx_PrcPar;
        
        public UI_Frm_Pln_Invisible()
        {
            Tx_Planilla = new List<UI_TextBox>();
            for(int i = 0; i < 60; i++)
            {
                this.Tx_Planilla.Add(new UI_TextBox());
            }

            Boton = new List<UI_Button>();
            for (int i = 0; i <= 1; i++)
            {
                this.Boton.Add(new UI_Button());
            }
            Atras = new UI_Button();
            Adelante = new UI_Button();
            Ch_ZonFra = new UI_CheckBox();
            Tx_SecBen = new UI_TextBox();
            Tx_SecInv = new UI_TextBox();
            Tx_PrcPar = new UI_TextBox();

        }
    }
}
