using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm
    {
        public List<UI_Message> Errors { set; get; }
        public List<UI_Message> Confirms { set; get; }

        public UI_Frm()
        {
            Errors = new List<UI_Message>();
            Confirms = new List<UI_Message>();
        }
    }
}
