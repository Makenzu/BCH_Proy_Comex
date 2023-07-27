using System.Collections.Generic;

namespace BCH.Comex.Common.XGPY.UI_Modulos
{
    public class UI_PrtEnt11
    {
        public UI_TextBox_ prtcodigo;
        public UI_TextBox_ prtswif;

        public UI_CheckBox_ prtaladi;

        public UI_TextBox_ prtplaza;

        public UI_TextBox_ ejecorr;

        //Tipo Banco
        public List<UI_CheckBox_> _prttipob_;
        // public List<UI_OptionItem_> _prttipob_;

        //Tasa de Rifanciamiento
        public List<UI_OptionItem_> _prttasa_ { get; set; }

        public UI_TextBox_ prtspread;

        public UI_Combo_ Combo1;

        public UI_Button_ aceptar;
        public UI_Button_ cancelar;
        public UI_TextBox_ prtrut;
        public UI_PrtEnt11()
        {
            prtcodigo = new UI_TextBox_();
            prtswif = new UI_TextBox_();
            prtaladi = new UI_CheckBox_();
            //prtaladi = new UI_CheckBox_(
            //   new UI_CheckBox_ { ID = "0", Tag = "Acreedor", Value = 0, Checked = true }

            //    );


            prtplaza = new UI_TextBox_();
            ejecorr = new UI_TextBox_();

            _prttipob_ = new List<UI_CheckBox_>(){
                new UI_CheckBox_ { ID="0", Tag="Acreedor", Value= 0, Checked=true },
                new UI_CheckBox_ { ID="1", Tag="Corresponsal", Value = 1 },
                new UI_CheckBox_ { ID="2", Tag="Avisador", Value = 2 }
            };
            //_prttipob_ = new List<UI_CheckBox_>();
            //for (var i = 0; i <= 2; i++)
            //{
            //    _prttipob_.Add(new UI_CheckBox_());
            //}
            _prttasa_ = new List<UI_OptionItem_>() {
                new UI_OptionItem_ { ID="0", Value="Libor", Selected=true },
                new UI_OptionItem_ { ID="1", Value="Prime" },
                new UI_OptionItem_ { ID="2", Value="Nominal" }
            };

            prtspread = new UI_TextBox_();
            Combo1 = new UI_Combo_();
            aceptar = new UI_Button_();
            cancelar = new UI_Button_();
            prtrut = new UI_TextBox_();

        }


    }
}
