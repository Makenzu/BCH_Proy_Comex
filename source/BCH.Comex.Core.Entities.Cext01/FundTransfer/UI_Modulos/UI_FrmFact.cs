using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_FrmFact
    {
        public const string MsgPrn = "Notas de Crédito";

        public List<UI_Label> Titulo;
        public List<UI_TextBox> Tx_NumOpe;
        public UI_Combo L_Print_Sort;
        public UI_Combo L_Print;
        public UI_Button bot_acep;
        public UI_Button bot_canc;
        

        public UI_FrmFact() {
            Titulo = new List<UI_Label>() { new UI_Label()};
            Tx_NumOpe = new List<UI_TextBox>() 
                                            {   new UI_TextBox(){ Enabled = false}, 
                                                new UI_TextBox(){ Enabled = false}, 
                                                new UI_TextBox(){ Enabled = false}, 
                                                new UI_TextBox(){ Enabled = false}, 
                                                new UI_TextBox(){ Enabled = false}};
            L_Print_Sort = new UI_Combo();
            L_Print = new UI_Combo();
            bot_acep = new UI_Button();
            bot_canc = new UI_Button();
        }
    }
}
