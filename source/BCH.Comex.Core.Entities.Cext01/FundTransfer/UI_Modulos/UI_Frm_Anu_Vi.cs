using BCH.Comex.Common.UI_Modulos;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_Anu_Vi
    {
        public UI_TextBox[] Tx_NroOpe;
        public UI_TextBox Tx_NroOpe_000 { get; set; }
        public UI_TextBox Tx_NroOpe_001 { get; set; }
        public UI_TextBox Tx_NroOpe_002 { get; set; }
        public UI_TextBox Tx_NroOpe_003 { get; set; }
        public UI_TextBox Tx_NroOpe_004 { get; set; }
        public UI_TextBox Tx_FecPre { get; set; }
        public UI_ListBox Lt_PlAnul { get; set; }
        public UI_TextBox Tx_MtoAnu { get; set; }
        public UI_TextBox Tx_TipCam { get; set; }
        public UI_TextBox Tx_ObsAnu { get; set; }
        public UI_Combo Cb_TipAut { get; set; }
        public UI_TextBox Tx_NumAut { get; set; }
        public UI_TextBox Tx_FecAut { get; set; }
        public UI_CheckBox Ch_Reemp { get; set; }
        public UI_Button Bot_Aceptar { get; set; }
        public UI_Button Bot_Cancel { get; set; }
        public UI_Button Bot_Ok { get; set; }


        public UI_Frm_Anu_Vi()
        {

            Tx_NroOpe_000 = new UI_TextBox();
            Tx_NroOpe_001 = new UI_TextBox();
            Tx_NroOpe_002 = new UI_TextBox();
            Tx_NroOpe_003 = new UI_TextBox();
            Tx_NroOpe_004 = new UI_TextBox();
            Tx_NroOpe = new UI_TextBox[] { Tx_NroOpe_000, Tx_NroOpe_001, Tx_NroOpe_002, Tx_NroOpe_003, Tx_NroOpe_004 };
            Tx_FecPre = new UI_TextBox();
            Lt_PlAnul = new UI_ListBox() { Enabled = false };
            Tx_MtoAnu= new UI_TextBox ();
            Tx_TipCam= new UI_TextBox ();
            Tx_ObsAnu = new UI_TextBox() { EsTextArea = true, Rows = 10 };
            Cb_TipAut = new UI_Combo() { SelectedValue = -1 };
            Tx_NumAut = new UI_TextBox();
            Tx_FecAut = new UI_TextBox();
            Ch_Reemp = new UI_CheckBox();
            Bot_Aceptar = new UI_Button();
            Bot_Cancel = new UI_Button();
            Bot_Ok = new UI_Button();
        }
    }
}
