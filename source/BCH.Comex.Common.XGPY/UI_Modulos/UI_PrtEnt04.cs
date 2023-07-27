
namespace BCH.Comex.Common.XGPY.UI_Modulos
{
    public class UI_PrtEnt04
    {
        public UI_TextBox_ prtinstruc { get; set; }    
        public UI_Button_ cancelar { get; set; }//UserControl
        public UI_Button_ aceptar { get; set; }//UserControl
        public UI_Combo_ CmbMemo { get; set; }

        public UI_PrtEnt04()
        {
            prtinstruc = new UI_TextBox_();         
            cancelar = new UI_Button_();
            aceptar = new UI_Button_();
            CmbMemo = new UI_Combo_();
        }
    }
}
