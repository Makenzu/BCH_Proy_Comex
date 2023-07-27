using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class ParticipantesIdentificarViewModel
    {
        public List<SelectListItem> RazonSocialList { get; set; }
        public List<SelectListItem> DirList { get; set; }

        public ParticipantesIdentificarViewModel(UI_Frm_Iden_Participantes form)
        {
            RazonSocialList = form.Nome.Items.Select(x => new SelectListItem
            {
                Text = x.Value,
                Value = x.Data.ToString()
            }).ToList();

            DirList = form.Dire.Items.Select(x => new SelectListItem
            {
                Text = x.Value,
                Value = x.Data.ToString()
            }).ToList();
        }


    }
}
