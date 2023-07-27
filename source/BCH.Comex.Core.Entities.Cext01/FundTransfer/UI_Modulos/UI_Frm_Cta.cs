using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_Cta : UI_Frm
    {
        public const short PorNum = 1;
        // UPGRADE_INFO (#0561): The 'PorNem' symbol was defined without an explicit "As" clause.
        public const short PorNem = 2;
        // UPGRADE_INFO (#0561): The 'PorNom' symbol was defined without an explicit "As" clause.
        public const short PorNom = 3;

        public string VieneDe { set; get; }

        public UI_ListBox LCtaSort { set; get; }
        public UI_Combo tipoord_cta { set; get; }
        public UI_Button bot_acep { set; get; }
        public UI_Button bot_canc { set; get; }
        public UI_Grid Lista { set; get; }
        public List<UI_Label> Titulo { set; get; }

        public UI_Frm_Cta()
        {
            LCtaSort = new UI_ListBox();
            tipoord_cta = new UI_Combo();
            bot_acep = new UI_Button();
            bot_canc = new UI_Button();
            Lista = new UI_Grid();
            Titulo = new List<UI_Label>() { new UI_Label(), new UI_Label() };
        }
    }
}
