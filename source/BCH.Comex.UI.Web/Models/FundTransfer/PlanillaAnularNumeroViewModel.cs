using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class PlanillaAnularNumeroViewModel : FundTransferViewModel
    {
        public UI_Combo Cb_Moneda { get; set; }
        public UI_TextBox CAM_TipCam { get; set; }
        public UI_TextBox Cam_NroPln { get; set; }

        public PlanillaAnularNumeroViewModel()
        {
            MensajesDeError = new List<UI_Message>();
        }

        public PlanillaAnularNumeroViewModel(UI_frmnroa frmState)
        {
            this.Cb_Moneda = frmState.Cb_Moneda;
            this.CAM_TipCam = frmState.CAM_TipCam;
            this.Cam_NroPln = frmState.Cam_NroPln;
        }

        public void Update(UI_frmnroa frmState)
        {
            Update(frmState.Cam_NroPln, Cam_NroPln);
            Update(frmState.CAM_TipCam, CAM_TipCam);
            Update(frmState.Cb_Moneda, Cb_Moneda);
        }
    }
}