using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using System.Linq;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class FacturasAsociadasViewModel
    {
        public UI_TextBox Tx_NumOpe_000 { set; get; }
        public UI_TextBox Tx_NumOpe_001 { set; get; }
        public UI_TextBox Tx_NumOpe_002 { set; get; }
        public UI_TextBox Tx_NumOpe_003 { set; get; }
        public UI_TextBox Tx_NumOpe_004 { set; get; }
        public UI_Button bot_acet { set; get; }
        public UI_Button bot_canc { set; get; }
        public UI_ListBox L_Print { set; get; }


        public FacturasAsociadasViewModel() { }

        public FacturasAsociadasViewModel(UI_FrmFact ffact) {
            Tx_NumOpe_000 = ffact.Tx_NumOpe[0];
            Tx_NumOpe_001 = ffact.Tx_NumOpe[1];
            Tx_NumOpe_002 = ffact.Tx_NumOpe[2];
            Tx_NumOpe_003 = ffact.Tx_NumOpe[3];
            Tx_NumOpe_004 = ffact.Tx_NumOpe[4];

            bot_acet = ffact.bot_acep;
            bot_canc = ffact.bot_canc;

            L_Print = new UI_ListBox();
            L_Print.Enabled = ffact.L_Print.Enabled;
            L_Print.ID = ffact.L_Print.ID;
            L_Print.ListIndex = ffact.L_Print.ListIndex;
            L_Print.SelectedValue = ffact.L_Print.SelectedValue;
            L_Print.Tag = ffact.L_Print.Tag;
            L_Print.Visible = ffact.L_Print.Visible;
            L_Print.Items = ffact.L_Print.Items.Select(x => new UI_ListBoxItem() { 
                Data = x.Data, 
                Value = x.Value,
                ID = x.ID,
                Visible = x.Visible,
                Enabled = x.Enabled,
            }).ToList();
            L_Print.Items.ForEach(x => x.Value = x.Value.Replace(" ", "\xA0").Replace("\t", "\xA0\xA0\xA0"));
        }


        public void Update(UI_FrmFact ffact) {
            ffact.L_Print.ListIndex = (int)L_Print.SelectedValue;

        }
    }
}