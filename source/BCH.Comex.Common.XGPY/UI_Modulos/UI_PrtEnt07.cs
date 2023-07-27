using System.Collections.Generic;

namespace BCH.Comex.Common.XGPY.UI_Modulos
{
    public class UI_PrtEnt07
    {
        //public List<UI_CheckBox> prtcliente { set; get; }
        //public List<UI_CheckBox> cliente { set; get; }


        //public UI_CheckBox prtcliente { set; get; }
        //public UI_CheckBox cliente { set; get; }
        public UI_TextBox_ prtrut { set; get; }
        public UI_TextBox_ oficina { set; get; }
        public UI_Combo_ ejecutivo { set; get; }
        public UI_Combo_ Combo2 { set; get; }
        public UI_Combo_ Combo4 { set; get; }
        public UI_Combo_ Combo1 { set; get; }

        public UI_Button_ aceptar { set; get; }
        public UI_Button_ cancelar { set; get; }

        //Comision
        public UI_Combo_ Cbo_CenCosImp { set; get; }
        public UI_Combo_ Cbo_EspecImp { set; get; }
        public UI_Button_ Bot_IngImp { set; get; }
        public UI_Button_ Bot_EliImp { set; get; }
        public UI_TextBox_ Txt_Imp { set; get; }

        //Exportacion 
        public UI_Combo_ Cbo_CenCosExp { set; get; }
        public UI_Combo_ Cbo_EspecExp { set; get; }
        public UI_Button_ Bot_IngExp { set; get; }
        public UI_Button_ Bot_EliExp { set; get; }
        public UI_TextBox_ Txt_Exp { set; get; }

        //Negocios 
        public UI_Combo_ Cbo_CenCosNeg { set; get; }
        public UI_Combo_ Cbo_EspecNeg { set; get; }
        public UI_Button_ Bot_IngNeg { set; get; }
        public UI_Button_ Bot_EliNeg { set; get; }
        public UI_TextBox_ Txt_Negocios { set; get; }
        public List<UI_OptionItem_> prtcliente { get; set; }
        public UI_TextBox_ actividad { get; set; } //Hidden
        public UI_PrtEnt07()
        {
            //this.prtcliente = new List<UI_CheckBox>();
            //this.cliente = new List<UI_CheckBox>();
            //this.prtcliente = new UI_CheckBox();
            //this.cliente = new UI_CheckBox();
            this.prtrut = new UI_TextBox_();
            this.oficina = new UI_TextBox_();
            this.ejecutivo = new UI_Combo_();
            this.Combo2 = new UI_Combo_();
            this.Combo4 = new UI_Combo_();
            this.Combo1 = new UI_Combo_();
            this.aceptar = new UI_Button_();
            this.cancelar = new UI_Button_();
            //Comision
            this.Cbo_CenCosImp = new UI_Combo_();
            this.Cbo_EspecImp = new UI_Combo_();
            this.Bot_IngImp = new UI_Button_();
            this.Bot_EliImp = new UI_Button_();
            this.Txt_Imp = new UI_TextBox_();
            //Exportacion
            this.Cbo_CenCosExp = new UI_Combo_();
            this.Cbo_EspecExp = new UI_Combo_();
            this.Bot_IngExp = new UI_Button_();
            this.Bot_EliExp = new UI_Button_();
            this.Txt_Exp = new UI_TextBox_();
            //Negocios
            this.Cbo_CenCosNeg = new UI_Combo_();
            this.Cbo_EspecNeg = new UI_Combo_();
            this.Bot_IngNeg = new UI_Button_();
            this.Bot_EliNeg = new UI_Button_();
            this.Txt_Negocios = new UI_TextBox_();
            prtcliente = new List<UI_OptionItem_>() {
                new UI_OptionItem_ { ID="0", Value="Cliente Comex", Selected=true },
                new UI_OptionItem_ { ID="1", Value="Cliente Banco" },
            };
            actividad = new UI_TextBox_();
        }



    }
}
