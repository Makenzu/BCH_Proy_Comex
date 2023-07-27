using BCH.Comex.Common.UI_Modulos;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_Con_Participantes
    {
        public UI_ListBox lista { get; set; }
        public UI_Button ok { get; set; }
        public UI_Button img_Pare { get; set; }
        public UI_TextBox caja { get; set; }
        public UI_Grid msg_datos { get; set; }

        public UI_Frm_Con_Participantes()
        {
            lista = new UI_ListBox();
            ok = new UI_Button();
            img_Pare = new UI_Button();
            caja = new UI_TextBox();
            msg_datos = new UI_Grid();
        }
    }
}
