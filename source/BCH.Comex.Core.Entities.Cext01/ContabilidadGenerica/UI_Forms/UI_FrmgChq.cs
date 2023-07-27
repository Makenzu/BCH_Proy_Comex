using BCH.Comex.Common.UI_Modulos;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms
{
    public class UI_FrmgChq:UI_Frm
    {
        public UI_ListBox l_cor{set;get;}
        public UI_Button Co_Cancelar{set;get;}
        public UI_ListBox l_montos{set;get;}
        public UI_Combo l_benef{set;get;}
        public UI_Button Co_Aceptar{set;get;}
        public UI_Combo l_plaza{set;get;}
        public UI_Button Co_Generar{set;get;}
        public UI_Frame Frame3D1{set;get;}
        public UI_TextBox Tx_Rut{set;get;}
        public UI_TextBox Tx_Nombre{set;get;}
        public UI_Label Lb_Nombre{set;get;}
        public UI_Label Lb_rut{set;get;}
        public UI_Label Label1{set;get;}
        public UI_Label Lb_Corresponsal{set;get;}
        public UI_Label Label2{set;get;}
        public UI_Label Label3{set;get;}

        public int En_Load = 0;
        public int I_Ben = 0;     // Indice del Party Beneficiario.-
        public int Pais_Ben = 0;     // Código de País del Beneficiario.-
        public string Nombre_Ben = "";     // Nombre del Beneficiario.-
        public string Nombre_Cli = "";     // Nombre del Cliente.-
        public string Rut_Ben = "";     // Rur del Beneficiario.-
        public int Plaza_Pago = 0;     // Código de País donde se pagará al Beneficiario.-
        public string Swift_Corresponsal = "";
        public int EsBcx = 0;

        public UI_FrmgChq()
        {
            l_cor = new UI_ListBox() { };
            Co_Cancelar = new UI_Button() { Text="Cancelar" };
            l_montos = new UI_ListBox() { };
            l_benef = new UI_Combo() { };
            Co_Aceptar = new UI_Button() { Text = "Aceptar" };
            l_plaza = new UI_Combo() { };
            Co_Generar = new UI_Button() { Text="Generar Doc." };
            Frame3D1 = new UI_Frame() { };
            Tx_Rut = new UI_TextBox() { };
            Tx_Nombre = new UI_TextBox() { };
            Lb_Nombre = new UI_Label() { Text="Nombre" };
            Lb_rut = new UI_Label() { Text="Rut" };
            Label1 = new UI_Label() { };
            Lb_Corresponsal = new UI_Label() { Text="BancosCorresponsales" };
            Label2 = new UI_Label() { Text="Beneficiario" };
            Label3 = new UI_Label() { Text= "Plaza de Pago"};

            Frame3D1.Controles.Add(Tx_Rut);
            Frame3D1.Controles.Add(Tx_Nombre);
            Frame3D1.Controles.Add(Lb_Nombre);
            Frame3D1.Controles.Add(Lb_rut);

        }
    }
}
