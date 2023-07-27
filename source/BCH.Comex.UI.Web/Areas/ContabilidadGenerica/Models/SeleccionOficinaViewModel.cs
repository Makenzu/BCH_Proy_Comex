using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms;

namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Models
{
    public class SeleccionOficinaViewModel : ContabilidadGenericaViewModel
    {
        public UI_Combo Cb_Oficina { get; set; }

        public SeleccionOficinaViewModel()
        {
        }

        public SeleccionOficinaViewModel(SeleccionOficinaViewModel frmState, string action)
        {
            Cb_Oficina = frmState.Cb_Oficina;
        }

        public SeleccionOficinaViewModel(UI_Frm_SeleccionOficina frmState)
        {
            Cb_Oficina = frmState.Cb_Oficina;
        }

        public void Update(UI_Frm_SeleccionOficina frmState)
        {
            Update(frmState.Cb_Oficina, Cb_Oficina);
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
        }

    }

}