using BCH.Comex.Common.UI_Modulos;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms
{
    public class UI_Tickets:UI_Frm
    {
        public UI_Button cancelar { set; get; }
        public UI_Button aceptar { set; get; }
        public UI_Frame Frame3D2 { set; get; }
        public UI_TextBox CAM_Concepto { set; get; }
        public UI_CheckBox otro { set; get; }
        public UI_Combo Cb_ticket { set; get; }
        public UI_Label Label1 { set; get; }
        public UI_Frame Frame3D1 { set; get; }
        public UI_TextBox CAM_Cuenta { set; get; }
        public UI_TextBox CAM_Monto { set; get; }
        public UI_TextBox CAM_Nemonico { set; get; }
        public UI_TextBox CAM_Nombre { set; get; }
        public UI_Combo CBO_DeHa { set; get; }
        public UI_Label Label7 { set; get; }
        public UI_Label Label5 { set; get; }
        public UI_Label Label2 { set; get; }
        public UI_Label Label3 { set; get; }
        public int IMPUESTO { get; set; }
        public string TIP { get; set; }
        public string S { get; set; }
        public double[] MONTO { get; set; }
        public int A { get; set; }
        public string ST { get; set; }

        public UI_Tickets()
        {
            cancelar = new UI_Button();
            aceptar = new UI_Button();
            Frame3D2 = new UI_Frame();
            CAM_Concepto = new UI_TextBox();
            otro = new UI_CheckBox();
            Cb_ticket = new UI_Combo();
            Label1 = new UI_Label();
            Frame3D1 = new UI_Frame();
            CAM_Cuenta = new UI_TextBox();
            CAM_Monto = new UI_TextBox();
            CAM_Nemonico = new UI_TextBox();
            CAM_Nombre = new UI_TextBox();
            CBO_DeHa = new UI_Combo();
            Label7 = new UI_Label();
            Label5 = new UI_Label();
            Label2 = new UI_Label();
            Label3 = new UI_Label();
        }
    }
}
