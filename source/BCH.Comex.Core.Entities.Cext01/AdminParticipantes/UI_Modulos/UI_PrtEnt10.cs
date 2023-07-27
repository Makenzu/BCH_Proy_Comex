using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos
{
    public class UI_PrtEnt10
    {
        public UI_Combo cbo_cta { get; set; }
        public UI_Combo Combo1 { get; set; } //Moneda
        public UI_CheckBox _prtactiva_1 { get; set; }
        public UI_CheckBox _prtactiva_0 { get; set; }
        public UI_CheckBox CuentaBae { get; set; }
        public UI_CheckBox especial { get; set; }
        public UI_TextBox oficina { get; set; }
        public UI_Button aceptar { get; set; }
        public UI_Button cancelar { get; set; }
        public UI_Button Eliminar { get; set; }
        public UI_Combo listmon { get; set; }
        public UI_TextBox prtcuenta { get; set; } //UserControl

        public UI_TextBox Tag { get; set; } //UserControl
        public UI_Label txtTitulo { get; set; }

        public UI_Label Label1 { get; set; }
        public UI_Label Label3 { get; set; }
       // public string Label1 { get; set; }
        public int idEstadoMensaje { get; set; }

        public int MarcaMensaje { get; set; }

        public UI_PrtEnt10()
        {
            cbo_cta = new UI_Combo();
            Combo1 = new UI_Combo();
            _prtactiva_1 = new UI_CheckBox();
            _prtactiva_0 = new UI_CheckBox();
            CuentaBae = new UI_CheckBox();
            especial = new UI_CheckBox();
            oficina = new UI_TextBox();
            aceptar = new UI_Button();
            cancelar = new UI_Button();
            Eliminar = new UI_Button();
            listmon = new UI_Combo();
            prtcuenta = new UI_TextBox();
            Tag = new UI_TextBox();
            txtTitulo = new UI_Label();
            //Label1 = string.Empty;
            Label1 = new UI_Label();
            Label3 = new UI_Label();
            this.idEstadoMensaje = new int();

        }
    }
}
