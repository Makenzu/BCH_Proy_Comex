using BCH.Comex.Common.UI_Modulos;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_frmnroa
    {
        public UI_Combo Cb_Moneda;
        public UI_TextBox CAM_TipCam;
        public UI_TextBox Cam_NroPln;

        public UI_frmnroa()
        {
            this.Cb_Moneda = new UI_Combo();
            this.CAM_TipCam = new UI_TextBox();
            this.Cam_NroPln = new UI_TextBox();
        }
    }
}
