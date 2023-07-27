using BCH.Comex.Common.UI_Modulos;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_FrmChq : UI_Frm
    {

        public short En_Load;
        // UPGRADE_INFO (#0501): The 'I_Ben' member isn't used anywhere in current application.
        public short I_Ben;  //Indice del Party Beneficiario.-
        // UPGRADE_INFO (#0501): The 'Pais_Ben' member isn't used anywhere in current application.
        public short Pais_Ben;  //Código de País del Beneficiario.-
        public string Nombre_Ben = "";  //Nombre del Beneficiario.-
        public string Nombre_Cli = "";  //Nombre del Cliente.-
        public string Rut_Ben = "";  //Rur del Beneficiario.-
        public short Plaza_Pago;  //Código de País donde se pagará al Beneficiario.-
        public string Swift_Corresponsal = "";
        public short EsBcx;
        public bool respuesta;
        public UI_ListBox l_cor { get; set; }
        public UI_Grid l_montos { get; set; }

        public UI_Combo l_benef { get; set; }
        public UI_Combo l_plaza { get; set; }

        public UI_TextBox Tx_Nombre { get; set; }
        public UI_TextBox Tx_Rut { get; set; }

        public UI_Frame Frame1 { get; set; }

        public UI_Button Co_Cancelar { get; set; }
        public UI_Button Co_Aceptar { get; set; }
        public UI_Button Co_Generar { get; set; }

        public UI_Label Label1 { get; set; }
        public UI_Label Lb_rut { get; set; }
        public UI_Label Lb_Nombre { get; set; }
        public UI_Label Lb_Corresponsal { get; set; }
        public UI_Label Label2 { get; set; }
        public UI_Label Label3 { get; set; }

        public UI_FrmChq()
        {
            Label1 = new UI_Label();
            l_plaza = new UI_Combo();
            Label3 = new UI_Label();
            Label2 = new UI_Label();
            Lb_Corresponsal = new UI_Label();
            Co_Generar = new UI_Button();
            Co_Aceptar = new UI_Button();
            l_benef = new UI_Combo();
            l_montos = new UI_Grid();
            Co_Cancelar = new UI_Button();
            l_cor = new UI_ListBox();
            Lb_Nombre = new UI_Label();
            Lb_rut = new UI_Label();
            Tx_Rut = new UI_TextBox();
            Tx_Nombre = new UI_TextBox();
            Frame1 = new UI_Frame();
        }
    }
}
