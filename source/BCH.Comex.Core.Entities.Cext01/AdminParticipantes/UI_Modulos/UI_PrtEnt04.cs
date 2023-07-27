using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;
namespace BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos
{
    public class UI_PrtEnt04
    {
        public UI_TextBox prtinstruc { get; set; }    
        public UI_Button cancelar { get; set; }//UserControl
        public UI_Button aceptar { get; set; }//UserControl
        public UI_Combo CmbMemo { get; set; }

        public UI_PrtEnt04()
        {
            prtinstruc = new UI_TextBox();         
            cancelar = new UI_Button();
            aceptar = new UI_Button();
            CmbMemo = new UI_Combo();
        }
    }
}
