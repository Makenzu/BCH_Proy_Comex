using BCH.Comex.Common.UI_Modulos;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_Iden_Participantes
    {
        public string Caption { get; set; }
        public string Tag { get; set; }
        public UI_Button Aceptar { get; set; }
        public UI_Button Cancelar { get; set; }
        public UI_Combo Nome { get; set; }
        public UI_Combo Dire { get; set; }
        public UI_Combo Otro { get; set; }

        public UI_Frm_Iden_Participantes()
        {
           Aceptar = new UI_Button();
           Cancelar = new UI_Button();
           Nome = new UI_Combo();
           Dire = new UI_Combo();
           Otro = new UI_Combo();
        }
    }
}
