
namespace BCH.Comex.Common.XGPY.UI_Modulos
{
    public class UI_PrtEnt10
    {
        public UI_Combo_ cbo_cta { get; set; }
        public UI_Combo_ Combo1 { get; set; } //Moneda
        public UI_CheckBox_ _prtactiva_1 { get; set; }
        public UI_CheckBox_ _prtactiva_0 { get; set; }
        public UI_CheckBox_ CuentaBae { get; set; }
        public UI_CheckBox_ especial { get; set; }
        public UI_TextBox_ oficina { get; set; }
        public UI_Button_ aceptar { get; set; }
        public UI_Button_ cancelar { get; set; }
        public UI_Button_ Eliminar { get; set; }
        public UI_Combo_ listmon { get; set; }
        public UI_TextBox_ prtcuenta { get; set; } //UserControl

        public UI_TextBox_ Tag { get; set; } //UserControl
        public UI_Label txtTitulo { get; set; }

        public UI_Label Label1 { get; set; }

        public UI_PrtEnt10()
        {
            cbo_cta = new UI_Combo_();
            Combo1 = new UI_Combo_();
            _prtactiva_1 = new UI_CheckBox_();
            _prtactiva_0 = new UI_CheckBox_();
            CuentaBae = new UI_CheckBox_();
            especial = new UI_CheckBox_();
            oficina = new UI_TextBox_();
            aceptar = new UI_Button_();
            cancelar = new UI_Button_();
            Eliminar = new UI_Button_();
            listmon = new UI_Combo_();
            prtcuenta = new UI_TextBox_();
            Tag = new UI_TextBox_();
            txtTitulo = new UI_Label();
            Label1 = new UI_Label();
        }
    }
}
