
namespace BCH.Comex.Common.XGPY.UI_Modulos
{
    public class UI_PrtEnt13
    {
        public UI_CheckBox_ manual { get; set; }//UserControl
        public UI_TextBox_ fecha { get; set; }//UserControl
        public UI_CheckBox_ fijo { get; set; }//UserControl
        public UI_TextBox_ tasa { get; set; }
        public UI_TextBox_ monto { get; set; }
        public UI_TextBox_ minimo { get; set; }
        public UI_TextBox_ maximo { get; set; }
        public UI_Button_ aceptar { get; set; }
        public UI_Button_ cancelar { get; set; }

        public UI_Label Lb_fecing { get; set; } //Hidden

        public UI_Frame Frame1 { get; set; }

        public UI_PrtEnt13()
        {
            manual = new UI_CheckBox_();
            fecha = new UI_TextBox_();
            fijo = new UI_CheckBox_();
            tasa = new UI_TextBox_();
            monto = new UI_TextBox_();
            minimo = new UI_TextBox_();
            maximo = new UI_TextBox_();
            aceptar = new UI_Button_();
            cancelar = new UI_Button_();
            Lb_fecing = new UI_Label();
            Frame1 = new UI_Frame();
        }
    }
}
