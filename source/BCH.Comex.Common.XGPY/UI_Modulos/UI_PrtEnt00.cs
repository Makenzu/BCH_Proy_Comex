using System.Collections.Generic;

namespace BCH.Comex.Common.XGPY.UI_Modulos
{
    public class UI_PrtEnt00
    {

        //Participante
        public List<UI_OptionItem_> _PrtTipo { get; set; }

        //Formato
        public List<UI_OptionItem_> _PrtFormato { get; set; }


        public UI_Button_ prtaceptar { get; set; }
        public UI_Button_ prtcancelar { get; set; }

        //Llave de Identificacion
        public UI_TextBox_ txt_BNumber { get; set; }

        //Situacion
        public UI_CheckBox_ prtsituacion { get; set; }
        public UI_CheckBox_ _prttipob_0 { get; set; }
        public UI_CheckBox_ _prttipob_1 { get; set; }
        public UI_CheckBox_ _prttipob_2 { get; set; }

        public UI_TextBox_ prtllave { get; set; }

        public UI_PrtEnt00()
        {
            _PrtTipo = new List<UI_OptionItem_>() {
                new UI_OptionItem_ { ID="1", Value="Individuo", Selected=true },
                new UI_OptionItem_ { ID="2", Value="Banco" },
                new UI_OptionItem_ { ID="0", Value="Cuenta Global" }
            };
            _PrtFormato = new List<UI_OptionItem_>() {
                new UI_OptionItem_{ ID="1", Value="Rut", Selected=true },
                new UI_OptionItem_ { ID="2", Value="Alfa Numerico" }              
            };

            prtaceptar = new UI_Button_();
            prtcancelar = new UI_Button_();
            txt_BNumber = new UI_TextBox_();
            prtsituacion = new UI_CheckBox_();
            _prttipob_0 = new UI_CheckBox_();
            _prttipob_1 = new UI_CheckBox_(); ;
            _prttipob_2 = new UI_CheckBox_(); ;

            prtllave = new UI_TextBox_();
        }

    }
}
