
namespace BCH.Comex.Common.XGPY.UI_Modulos
{
    public class UI_PrtEnt09
    {
        public UI_TextBox_ caja { get; set; }
        public UI_Button_ OK { get; set; }
        public UI_Combo_ lista { get; set; }     
        public UI_Button_ Cerrar { get; set; }

        public UI_PrtEnt09()
        {
            caja = new UI_TextBox_();
            OK = new UI_Button_();
            lista = new UI_Combo_();
            Cerrar = new UI_Button_();           
        }


    }
}
