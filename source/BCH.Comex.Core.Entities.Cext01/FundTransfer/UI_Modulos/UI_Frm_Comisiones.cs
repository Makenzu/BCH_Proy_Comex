using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_Comisiones
    {

        public List<UI_Label> Lb_gcom_;
        public List<UI_TextBox> Tx_Com;
        public UI_ListBox Ls_com;
        public List<UI_Button> Cm_com_;
        public UI_CheckBox Ch_com;
        public UI_Button NO_com;
        public UI_Button OK_com;


        public UI_Frm_Comisiones()
        {

            //this.Lb_gcom_ = new List<UI_Label>();
            //for (int i = 0; i <= 4; i++)
            //{
            //    Lb_gcom_.Add(new UI_Label());
            //}

            this.Tx_Com = new List<UI_TextBox>();
            for (int i = 0; i <= 5; i++)
            {
                Tx_Com.Add(new UI_TextBox());
            }

          //  this.Ls_com = new UI_Grid();

            this.Cm_com_ = new List<UI_Button>();
            for (int i = 0; i <= 1; i++)
            {
                Cm_com_.Add(new UI_Button());
            }

            this.Ch_com = new UI_CheckBox();
            this.NO_com = new UI_Button();
            this.OK_com = new UI_Button();
            this.Ls_com = new UI_ListBox();

        }



    }
}
