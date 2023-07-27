using BCH.Comex.Common.UI_Modulos;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms
{
    public class UI_PrtEnt09:UI_Frm
    {
        public UI_Button Cerrar;
        public UI_Button OK;
        public UI_TextBox caja;
        public UI_Grid lista;
        public UI_Label Label2;
        public UI_Label Label1;

        public UI_PrtEnt09()
        {
            Cerrar = new UI_Button() { Text="Seleccionar" };
            OK = new UI_Button();
            caja = new UI_TextBox();
            lista = new UI_Grid();
            Label1 = new UI_Label() { Text="Identificación" };
            Label2 = new UI_Label() { Text="Nombre o Razón Social" };
            this.Text = "Consulta de Participantes";
        }
    }
}
