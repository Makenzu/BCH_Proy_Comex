using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;
namespace BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos
{
    public class UI_PrtEnt09
    {
        public UI_TextBox caja { get; set; }
        public UI_Button OK { get; set; }
        public UI_Combo lista { get; set; }
        public UI_Button Cerrar { get; set; }

        public UI_PrtEnt09()
        {
            caja = new UI_TextBox();
            OK = new UI_Button();
            lista = new UI_Combo();
            Cerrar = new UI_Button();
        }


    }
}
