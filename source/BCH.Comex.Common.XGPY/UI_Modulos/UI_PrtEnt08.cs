
namespace BCH.Comex.Common.XGPY.UI_Modulos
{
    public class UI_PrtEnt08
    {
        public UI_Combo_ Lista1 { get; set; }
        public UI_Combo_ Lista2 { get; set; }
        //public UI_ListBox_ Lista1 { get; set; }
        //public UI_ListBox_ Lista2 { get; set; }
        public UI_Button_ aceptar { get; set; }
        public UI_Button_ cancelar { get; set; }
        //public UI_Combo_ Combo1 { get; set; }
        public UI_TextBox_ cuenta { get; set; }//UserControl
        public UI_Label Label1 { get; set; }
        public UI_Label Label2 { get; set; }
        public UI_Label Label3 { get; set; }
        public UI_Label Label4 { get; set; }
        public UI_Label Label5 { get; set; }
        public UI_Label Label6 { get; set; }
        public UI_Label Label7 { get; set; }
        public UI_Label Titulo { get; set; }

        public UI_TextBox_ Frame1 { get; set; }
        public UI_TextBox_ Frame2 { get; set; }


        public UI_PrtEnt08()
        {
            Lista1 = new UI_Combo_();
            Lista2 = new UI_Combo_();          
            cuenta = new UI_TextBox_();
            aceptar = new UI_Button_();
            cancelar = new UI_Button_();

            Label1 = new UI_Label();
            Label2 = new UI_Label();
            Label3 = new UI_Label();
            Label4 = new UI_Label();
            Label5 = new UI_Label();
            Label6 = new UI_Label();
            Label7 = new UI_Label();
            Titulo = new UI_Label();
            Frame1 = new UI_TextBox_();
            Frame2 = new UI_TextBox_();
        }
    }
}