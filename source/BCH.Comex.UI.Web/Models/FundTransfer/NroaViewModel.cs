using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class NroaViewModel
    {
        public int Moneda { get; set; }
        public List<SelectListItem> Cb_Moneda { get; set; }
        public string Cam_TipCam { get; set; }
        public string Cam_NroPln { get; set; }

        public UI_Button Boton_Aceptar;
        public UI_Button Boton_Cancelar; 

    }
}