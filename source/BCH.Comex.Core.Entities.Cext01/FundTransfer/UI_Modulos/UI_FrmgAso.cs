using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_FrmgAso : UI_Frm
    {
        public UI_Frame Frame3D2 { get; set; }
        public UI_Frame Fr_InfClie { get; set; }
        public UI_Frame Frame3D1 { get; set; }

        public UI_Panel Pn_InfPrt { get; set; }

        public UI_Button Boton_000 { get; set; }
        public UI_Button Boton_001 { get; set; }
        public UI_Button Ok { get; set; }

        public UI_Label Label1_005 { get; set; }
        public UI_Label Label1_004 { get; set; }
        public UI_Label Label1_000 { get; set; }
        public UI_Label Label4 { get; set; }
        public UI_Label Label3 { get; set; }
        public UI_Label Label1_003 { get; set; }
        public UI_Label Label1_002 { get; set; }
        public UI_Label Label1_001 { get; set; }
        public UI_Label Label2 { get; set; }

        public UI_Combo Cb_Producto { get; set; }

        public UI_TextBox Tx_NumOpe_000 { get; set; }
        public UI_TextBox Tx_NumOpe_001 { get; set; }
        public UI_TextBox Tx_NumOpe_002 { get; set; }
        public UI_TextBox Tx_NumOpe_003 { get; set; }
        public UI_TextBox Tx_NumOpe_004 { get; set; }
        public UI_TextBox Tx_NumOpe_005 { get; set; }
        public UI_TextBox Tx_NumOpe_006 { get; set; }
        public UI_TextBox[] Tx_NroOpe;

        public UI_TextBox Tx_DirPrt { get; set; }
        public UI_TextBox Tx_NomPrt { get; set; }
        public UI_TextBox Tx_RutPrt { get; set; }

        public List<UI_Message> Errores { set; get; }

        //se agrega para poder ir actualizando el numero de operacion segun el producto
        public string OPE { set; get; }

        public UI_FrmgAso()
        {

            Frame3D2 = new UI_Frame();
            Frame3D1 = new UI_Frame();
            Fr_InfClie = new UI_Frame();

            Pn_InfPrt = new UI_Panel();

            Boton_000 = new UI_Button();
            Boton_001 = new UI_Button();
            Ok = new UI_Button();

            Label1_005 = new UI_Label();
            Label1_004 = new UI_Label();
            Label1_000 = new UI_Label();
            Label4 = new UI_Label();
            Label3 = new UI_Label();
            Label1_003 = new UI_Label();
            Label1_002 = new UI_Label();
            Label1_001 = new UI_Label();
            Label2 = new UI_Label();

            Cb_Producto = new UI_Combo();

            Tx_NumOpe_000 = new UI_TextBox();
            Tx_NumOpe_001 = new UI_TextBox();
            Tx_NumOpe_002 = new UI_TextBox();
            Tx_NumOpe_003 = new UI_TextBox();
            Tx_NumOpe_004 = new UI_TextBox();
            Tx_NumOpe_005 = new UI_TextBox();
            Tx_NumOpe_006 = new UI_TextBox();

            Tx_NroOpe = new UI_TextBox[] { Tx_NumOpe_000, Tx_NumOpe_001, Tx_NumOpe_002, Tx_NumOpe_003, Tx_NumOpe_004, Tx_NumOpe_005, Tx_NumOpe_006 };
            
            Tx_DirPrt = new UI_TextBox();
            Tx_NomPrt = new UI_TextBox();
            Tx_RutPrt = new UI_TextBox();

            Errores = new List<UI_Message>();

            OPE = string.Empty;
        }
    }
}
