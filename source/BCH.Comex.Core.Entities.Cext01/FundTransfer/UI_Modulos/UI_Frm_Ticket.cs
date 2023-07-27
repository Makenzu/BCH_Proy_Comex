using BCH.Comex.Common.UI_Modulos;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_Ticket:UI_Frm
    {
        public UI_Button aceptar { set; get; }
        public UI_Button cancelar { set; get; }
        public UI_Frame Frame2 { set; get; }
        public UI_Combo Cb_ticket { set; get; }
        public UI_CheckBox otro { set; get; }
        public UI_TextBox CAM_Concepto { set; get; }
        public UI_Label Label1 { set; get; }
        public UI_Frame Frame1 { set; get; }
        public UI_Combo CBO_DeHa { set; get; }
        public UI_TextBox CAM_Nombre { set; get; }
        public UI_TextBox CAM_Nemonico { set; get; }
        public UI_TextBox CAM_Monto { set; get; }
        public UI_TextBox CAM_Cuenta { set; get; }
        public UI_Label Label3 { set; get; }
        public UI_Label Label2 { set; get; }
        public UI_Label Label5 { set; get; }
        public UI_Label Label7 { set; get; }

        public bool esOri { set; get; }
        public string NumOpe { set; get; }
        public string Referencia { set; get; }
        public string Usuario { set; get; }
        public short ImpDeb { set; get; }

        public UI_Frm_Ticket(string no, dynamic refe, string usu, short id)
        {
            esOri = false;

            NumOpe = no;
            Referencia = refe;
            Usuario = usu;
            id = ImpDeb;

            aceptar = new UI_Button();
            cancelar = new UI_Button();
            Frame2 = new UI_Frame();
            Cb_ticket = new UI_Combo();
            otro = new UI_CheckBox();
            CAM_Concepto = new UI_TextBox();
            Label1 = new UI_Label();
            Frame1 = new UI_Frame();
            CBO_DeHa = new UI_Combo();
            CAM_Nombre = new UI_TextBox();
            CAM_Nemonico = new UI_TextBox();
            CAM_Monto = new UI_TextBox();
            CAM_Cuenta = new UI_TextBox();
            Label3 = new UI_Label();
            Label2 = new UI_Label();
            Label5 = new UI_Label();
            Label7 = new UI_Label();
        }

        public UI_Frm_Ticket()
        {
            aceptar = new UI_Button();
            cancelar = new UI_Button();
            Frame2 = new UI_Frame();
            Cb_ticket = new UI_Combo();
            otro = new UI_CheckBox();
            CAM_Concepto = new UI_TextBox();
            Label1 = new UI_Label();
            Frame1 = new UI_Frame();
            CBO_DeHa = new UI_Combo();
            CAM_Nombre = new UI_TextBox();
            CAM_Nemonico = new UI_TextBox();
            CAM_Monto = new UI_TextBox();
            CAM_Cuenta = new UI_TextBox();
            Label3 = new UI_Label();
            Label2 = new UI_Label();
            Label5 = new UI_Label();
            Label7 = new UI_Label();
        }
    }
}
