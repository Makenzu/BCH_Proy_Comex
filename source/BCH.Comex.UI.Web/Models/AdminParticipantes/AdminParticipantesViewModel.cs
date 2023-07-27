using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Models.AdminParticipantes
{
    public class AdminParticipantesViewModel
    {

        public List<BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos.UI_Message> MensajesDeError { set; get; }

        protected void Update(UI_ListBox listDestino, UI_ListBox listOrigen)
        {
            if (listOrigen != null)
            {
                Update(listDestino, listOrigen.SelectedValue);
            }
        }

        protected void Update(UI_ListBox listDestino, int? selectedValue)
        {
            int index = -1;

            index = selectedValue.HasValue ? listDestino.Items.FindIndex(x => x.Data == selectedValue) : -1;

            listDestino.ListIndex = index;
            listDestino.SelectedValue = index;
        }

        protected void Update(UI_Combo cmbDestino, UI_Combo cmbOrigen)
        {
            if (cmbOrigen != null)
            {
                Update(cmbDestino, cmbOrigen.SelectedValue);
            }
        }

        protected void Update(UI_Combo cmbDestino, int? selectedValue)
        {
            int index = -1;

            index = selectedValue.HasValue ? cmbDestino.Items.FindIndex(x => x.Data == selectedValue) : -1;

            cmbDestino.ListIndex = index;
            cmbDestino.SelectedValue = index;
        }

        protected void Update(UI_CheckBox chkDestino, UI_CheckBox chkOrigen)
        {
            if (chkOrigen != null)
                chkDestino.Checked = chkOrigen.Checked;
        }

        protected void Update(UI_OptionItem chkDestino, UI_OptionItem chkOrigen)
        {
            if (chkOrigen != null)
                chkDestino.Selected = chkOrigen.Selected;
        }

        protected void Update(UI_TextBox txtDestino, UI_TextBox txtOrigen)
        {
            if (txtOrigen != null)
                txtDestino.Text = txtOrigen.Text;
        }
    }
}