using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos
{
    public class UI_PrtEnt00
    {

        //Participante
        public List<UI_OptionItem> _PrtTipo { get; set; }

        //Formato
        public List<UI_OptionItem> _PrtFormato { get; set; }


        public UI_Button prtaceptar { get; set; }
        public UI_Button prtcancelar { get; set; }

        //Llave de Identificacion
        public UI_TextBox txt_BNumber { get; set; }

        //Situacion
        public UI_CheckBox prtsituacion { get; set; }
        public UI_CheckBox _prttipob_0 { get; set; }
        public UI_CheckBox _prttipob_1 { get; set; }
        public UI_CheckBox _prttipob_2 { get; set; }

        public UI_TextBox prtllave { get; set; }

        public UI_PrtEnt00()
        {
            _PrtTipo = new List<UI_OptionItem>() {
                new UI_OptionItem { ID="1", Value="Individuo", Selected=true },
                new UI_OptionItem { ID="2", Value="Banco" },
                new UI_OptionItem { ID="0", Value="Cuenta Global" }
            };
            _PrtFormato = new List<UI_OptionItem>() {
                new UI_OptionItem{ ID="1", Value="Rut", Selected=true },
                new UI_OptionItem { ID="2", Value="Alfa Numerico" }              
            };

            prtaceptar = new UI_Button();
            prtcancelar = new UI_Button();
            txt_BNumber = new UI_TextBox();
            prtsituacion = new UI_CheckBox();
            _prttipob_0 = new UI_CheckBox();
            _prttipob_1 = new UI_CheckBox(); ;
            _prttipob_2 = new UI_CheckBox(); ;

            prtllave = new UI_TextBox();
        }

    }
}
