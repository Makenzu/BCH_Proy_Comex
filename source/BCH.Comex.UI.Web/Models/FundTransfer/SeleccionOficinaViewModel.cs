using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class SeleccionOficinaViewModel : FundTransferViewModel
    {
        public UI_Combo Cb_Oficina { get; set; }
        public UI_OptionItem opt_CVD { set; get; }
        public UI_OptionItem opt_Comision { set; get; }
        public bool EsComision { get; set; }

        public SeleccionOficinaViewModel()
        {
        }

        public SeleccionOficinaViewModel(UI_Frm_SeleccionOficina frmState)
        {
            Cb_Oficina = frmState.Cb_Oficina;
            opt_Comision = frmState.opt_Comision;
            opt_CVD = frmState.opt_CVD;
            EsComision = opt_Comision.Selected;
        }

        public void Update(UI_Frm_SeleccionOficina frmState)
        {
            Update(frmState.Cb_Oficina, Cb_Oficina);
            frmState.opt_Comision.Selected = EsComision;
            frmState.opt_CVD.Selected = !EsComision;
        }
    }

}