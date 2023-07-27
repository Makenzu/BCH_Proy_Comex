using BCH.Comex.Common.UI_Modulos;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_Declaracion
    {

        public string Caption { get; set; }
        public string Tag { get; set; }
        public UI_TextBox CAM_Clausula { get; set; }
        public UI_TextBox CAM_Comisiones{ get; set; }
        public UI_TextBox CAM_Otros { get; set; }
        public UI_TextBox CAM_Liquido { get; set; }
        public UI_Button Aceptar { get; set; }
        public UI_Button Cancelar { get; set; }

        public UI_Frm_Declaracion() {
            this.CAM_Clausula = new UI_TextBox();
            this.CAM_Comisiones = new UI_TextBox();
            this.CAM_Otros = new UI_TextBox();
            this.CAM_Liquido = new UI_TextBox();
            this.Aceptar = new UI_Button();
            this.Cancelar = new UI_Button();
            this.CAM_Liquido.Tag = "_____________.__";
            this.CAM_Otros.Tag = "_____________.__";
            this.CAM_Comisiones.Tag = "_____________.__";
            this.CAM_Clausula.Tag = "_____________.__";

        }

    }
}
