using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_FrmxAnu
    {
        public dynamic[] Focus_RENAMED = new dynamic[24];
        public UI_Combo Cb_Autor;
        public List<UI_Button> Boton;
        public UI_TextBox Tx_Fecha;
        public List<UI_TextBox> Tx_PlAnu;
        public UI_FrmxAnu()
        {
            Boton = new List<UI_Button>();
            for (int i = 0; i <= 4; i++)
            {
                Boton.Add(new UI_Button());
            }
            Tx_Fecha = new UI_TextBox();

            Tx_PlAnu = new List<UI_TextBox>();
            for (int i = 0; i <= 32; i++)
            {
                Tx_PlAnu.Add(new UI_TextBox());
            }
            Cb_Autor = new UI_Combo();
          //  mensaje = new UI_Message();
        }
    }


}
