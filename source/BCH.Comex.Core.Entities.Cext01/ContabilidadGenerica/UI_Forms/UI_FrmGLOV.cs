using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms
{
    public class UI_FrmGLOV:UI_Frm
    {
        public List<UI_Button> Boton { get; set; }
        public List<UI_Label> Label1 { get; set; }
        public List<UI_TextBox> Tx_Datos { get; set; }
        public List<UI_Label> Tx_Cuentas { get; set; }
        public List<UI_Label> Lb_Datos { get; set; }

        public UI_Button Boton_001 { get; set; }
        public UI_Button Boton_000 { get; set; }
        public UI_Frame Frame3D2 { get; set; }

        public UI_Combo Cb_Mnd { get; set; }
        public UI_TextBox Tx_NomCta { get; set; }
        public UI_TextBox Tx_Mto { get; set; }
        public UI_Label Label1_000 { get; set; }
        public UI_Label Label1_001 { get; set; }
        public UI_Label Label1_002 { get; set; }
        public UI_Frame Fr_DatAdic { get; set; }
        //public UI_TextBox Tx_Datos_001 { get; set; }
        //public UI_TextBox Tx_Datos_002 { get; set; }
        //public UI_TextBox Tx_Datos_000 { get; set; }
        public UI_Combo L_Cuentas { get; set; }
        public UI_Label Tx_Cuentas_005 { get; set; }
        public UI_Label Lb_Datos_000 { get; set; }
        public UI_Label Lb_Datos_001 { get; set; }
        public UI_Label Lb_Datos_002 { get; set; }
        public UI_Label Lb_Datos_003 { get; set; }
        public UI_Label Lb_Oficina { get; set; }

        public UI_Label Operacion { get; set; }
        public UI_Label Lb_Info { get; set; }

        public UI_FrmGLOV()
        {
            Boton = new List<UI_Button>();
            Label1 = new List<UI_Label>();
            Tx_Datos = new List<UI_TextBox>();
            Tx_Cuentas = new List<UI_Label>();
            Lb_Datos = new List<UI_Label>();

            Boton_001 = new UI_Button() { Text="Cancelar" };
            Boton_000 = new UI_Button() { Text="Aceptar" };
            Frame3D2 = new UI_Frame() { Caption="Datos Adicionales" };

            Cb_Mnd = new UI_Combo();
            Tx_NomCta = new UI_TextBox();
            Tx_Mto = new UI_TextBox() { Tag= "_____________.__" };
            Label1_000 = new UI_Label() { Text="Cuenta Contable" };
            Label1_001 = new UI_Label() { Text="Moneda" };
            Label1_002 = new UI_Label() { Text="Monto"};
            Fr_DatAdic = new UI_Frame();
            //Tx_Datos_001 = new UI_TextBox();
            //Tx_Datos_002 = new UI_TextBox();
            //Tx_Datos_000 = new UI_TextBox();
            L_Cuentas = new UI_Combo();
            Tx_Cuentas_005 = new UI_Label() { Text="Cuentas Corrientes" };
            Lb_Datos_000 = new UI_Label() { Text="Datos" };
            Lb_Datos_001 = new UI_Label() { Text="Datos"};
            Lb_Datos_002 = new UI_Label() { Text="Datos"};
            Lb_Datos_003 = new UI_Label() { Text = "Datos" };
            Lb_Oficina = new UI_Label();
            Lb_Info = new UI_Label();

            Operacion = new UI_Label() { Text = "" };

            Boton.Add(Boton_000);
            Boton.Add(Boton_001);

            Label1.Add(Label1_000);
            Label1.Add(Label1_001);
            Label1.Add(Label1_002);

            Tx_Datos.Add(new UI_TextBox());
            Tx_Datos.Add(new UI_TextBox());
            Tx_Datos.Add(new UI_TextBox());
            Tx_Datos.Add(new UI_TextBox());

            Tx_Cuentas.Add(Tx_Cuentas_005);

            Lb_Datos.Add(Lb_Datos_000);
            Lb_Datos.Add(Lb_Datos_001);
            Lb_Datos.Add(Lb_Datos_002);
            Lb_Datos.Add(Lb_Datos_003);

            Frame3D2.Controles.Add(Cb_Mnd);
            Frame3D2.Controles.Add(Tx_NomCta);
            Frame3D2.Controles.Add(Tx_Mto);
            Frame3D2.Controles.Add(Label1_000);
            Frame3D2.Controles.Add(Label1_001);
            Frame3D2.Controles.Add(Label1_002);
            Frame3D2.Enabled = false;

        }
    }
}
