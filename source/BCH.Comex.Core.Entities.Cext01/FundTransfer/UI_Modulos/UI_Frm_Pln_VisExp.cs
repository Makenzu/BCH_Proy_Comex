using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_Pln_VisExp
    {
        //public UI_Label Lb_Plnv_000;
        //public UI_Label Lb_Plnv_002;
        //public UI_Label Lb_Plnv_003;
        //public UI_Label Lb_Plnv_004;
        //public UI_Label Lb_Plnv_005;
        //public UI_Label Lb_Plnv_006;
        //public UI_Label Lb_Plnv_007;
        //public UI_Label Lb_Plnv_008;
        //public UI_Label Lb_Plnv_009;
        //public UI_Label Lb_Plnv_010;
        //public UI_Label Lb_Plnv_011;
        ////public Pic_Marco_005 PictureBox
        //public UI_Label Lb_Plnv_012;
        //public UI_Label Lb_Plnv_013;
        //public UI_Label Lb_Plnv_014;
        //public UI_Label Lb_Plnv_015;
        //public UI_Label Lb_Plnv_016;
        //public UI_Label Lb_Plnv_017;
        //public UI_Label Lb_Plnv_018;
        //public UI_Label Lb_Plnv_019;
        //public UI_Label Lb_Plnv_020;
        //public UI_Label Lb_Plnv_021;
        //public UI_Label Lb_Plnv_022;
        //public UI_Label Lb_Plnv_023;
        //public UI_Label Lb_Plnv_024;
        //public UI_Label Lb_Plnv_025;
        //public UI_Label Lb_Plnv_026;
        //public UI_Label Lb_Plnv_027;
        //public UI_Label Lb_Plnv_028;
        //public UI_Label Lb_Plnv_029;
        //public UI_Label Lb_Plnv_030;
        //public UI_Label Lb_Plnv_031;
        //public UI_Label Lb_Plnv_032;
        //public UI_Label Lb_Plnv_033;
        //public UI_Label Lb_Plnv_034;
        //public UI_Label Lb_Plnv_035;
        //public UI_Label Lb_Plnv_036;
        //public UI_Label Lb_Plnv_037;
        //public UI_Label Lb_Plnv_038;
        //public UI_Label Lb_Plnv_039;
        //public UI_Label Lb_Plnv_040;
        //public UI_Label Lb_Plnv_041;
        //public UI_Label Lb_Plnv_042;
        //public UI_Label Lb_Plnv_043;
        //public UI_Label Lb_Plnv_044;
        //public UI_Label Lb_Plnv_045;
        //public UI_Label Lb_Plnv_046;
        //public UI_Label Lb_Plnv_047;
        //public UI_Label Lb_Plnv_048;
        //public UI_Label Lb_Plnv_049;
        //public UI_Label Lb_Plnv_050;
        //public UI_Label Lb_Plnv_052; //Codigo 1, parecido a textbox        
        //public UI_Label Lb_Plnv_053;
        //public UI_Label Lb_Plnv_054;
        public List<UI_Label> Lb_Plnv;
        public List<UI_TextBox> Tx_Plnv;
        //public UI_TextBox Tx_Plnv_000;
        public UI_TextBox Tx_Fecha;

        public List<UI_Label> L_PaD;
        public List<UI_Label> L_PaI;


        //public UI_TextBox Tx_Plnv_040;
        //public UI_TextBox Tx_Plnv_041;
        //public UI_TextBox Tx_Plnv_002;
        //public UI_TextBox Tx_Plnv_003;
        //public UI_TextBox Tx_Plnv_004;
        //public UI_TextBox Tx_Plnv_005;
        //public UI_TextBox Tx_Plnv_006;
        //public UI_TextBox Tx_Plnv_007;
        //public UI_TextBox Tx_Plnv_008;
        //public UI_TextBox Tx_Plnv_009;
        //public UI_TextBox Tx_Plnv_010;
        //public UI_TextBox Tx_Plnv_011;
        //public UI_TextBox Tx_Plnv_012;
        //public UI_TextBox Tx_Plnv_013;
        //public UI_TextBox Tx_Plnv_014;
        //public UI_TextBox Tx_Plnv_015;
        //public UI_TextBox Tx_Plnv_016;
        //public UI_TextBox Tx_Plnv_017;
        //public UI_TextBox Tx_Plnv_018;
        //public UI_TextBox Tx_Plnv_019;
        //public UI_TextBox Tx_Plnv_020;
        //public UI_TextBox Tx_Plnv_021;
        //public UI_TextBox Tx_Plnv_022;
        //public UI_TextBox Tx_Plnv_023;
        //public UI_TextBox Tx_Plnv_024;
        //public UI_TextBox Tx_Plnv_025;
        //public UI_TextBox Tx_Plnv_026;
        //public UI_TextBox Tx_Plnv_027;
        //public UI_TextBox Tx_Plnv_028;
        //public UI_TextBox Tx_Plnv_029;
        //public UI_TextBox Tx_Plnv_030;
        //public UI_TextBox Tx_Plnv_031;
        //public UI_TextBox Tx_Plnv_032;
        //public UI_TextBox Tx_Plnv_033;
        //public UI_TextBox Tx_Plnv_034;
        //public UI_TextBox Tx_Plnv_035;
        //public UI_TextBox Tx_Plnv_036;
        //public UI_TextBox Tx_Plnv_037;
        //public UI_TextBox Tx_Plnv_038;
        //public UI_TextBox Tx_Plnv_039;
        //public UI_TextBox Tx_Plnv_042;
        public List<UI_Button> Boton;
        //public UI_Button Boton_000;
        //public UI_Button Boton_001;
        //public UI_Button Boton_002;
        //public UI_Button Boton_003;
        //public UI_Button Boton_004;

        public UI_Frm_Pln_VisExp()
        {
            this.Tx_Plnv = new List<UI_TextBox>();
            for (int i = 0; i <= 43; i++) // 42 + 1 codigo que en el original esta como Label pero se transformo a textbox
            {
                this.Tx_Plnv.Add(new UI_TextBox());
            }

            Boton = new List<UI_Button>();
            for (int i = 0; i <= 4; i++)
            {
                Boton.Add(new UI_Button());
            }

            Tx_Fecha = new UI_TextBox();
            L_PaI = new List<UI_Label>() { new UI_Label(), new UI_Label() };
            L_PaD = new List<UI_Label>() { new UI_Label(), new UI_Label() };
            //this.Lb_Plnv = new List<UI_Label>();
            //for (int i = 0; i <= 54; i++)
            //{
            //    this.Lb_Plnv.Add(new UI_Label());
            //}

            
        }
    }
}
