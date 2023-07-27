using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frmgrev : UI_Frm
    {
        public UI_Frame Frame3D1;
        public UI_Label Label5;
        public UI_Label Label2;
        public UI_Label Label1;
        public UI_Label Label3;

        public UI_TextBox[] Tx_NroOpe;
        public UI_Button Ok_Operacion;
        public UI_Button[] Co_Boton;

        public UI_Label Lbl_Cliente;
        public UI_TextBox Tx_Prty;

        public UI_Frame Frame3D2;
        public UI_Combo Lt_Pln;

        public UI_Label lbl_tipAnu;
        public UI_Combo CB_Tipanu;

        public UI_Label lbl_tipCam;
        public UI_TextBox CAM_Tipcam;

        public UI_Button Bot_Dec;
        public UI_Button BOT_Obs;
        public UI_Button Ok;

        public UI_Frame Fr_Invisible;

        public UI_Label Label6;
        public UI_TextBox Tx_NroPln;

        public UI_Label Label10;
        public UI_TextBox Tx_Fecha;

        public UI_Label Label4;
        public UI_Combo Cb_Pbc;

        public UI_Label Label7;
        public UI_TextBox Tx_Motivo;

        public UI_Label Label9;
        public UI_Combo Cb_Tipo;

        public UI_Label Label8;
        public UI_TextBox Tx_TipCam;
        
        public UI_TextBox Tx_ObsPln;
        public UI_Button Co_Volver;
        public UI_Frame Fr_Observaciones;

        //*** PARA PODER MODELAR LOS POP UPS SE AGREGAN LAS SIGUIENTES VARIABLES
        public List<UI_Message> PopUps;

        public bool AbrirReversarOperacionDeclaracion { get; set; }

        public List<UI_Message> Errores { set; get; }
        
        public UI_Frmgrev()
        {
            Frame3D1 = new UI_Frame();
            Frame3D1.Caption = "Identificación Operación"; 
            var Tx_NroOpe_000 = new UI_TextBox();
            Label5 = new UI_Label();
            Label5.Tag = "-";  
            var Tx_NroOpe_001 = new UI_TextBox();
            Label2 = new UI_Label();
            Label2.Tag = "-";
            var Tx_NroOpe_002 = new UI_TextBox();
            Label1 = new UI_Label();
            Label1.Tag = "-";
            var Tx_NroOpe_003 = new UI_TextBox();
            Label3 = new UI_Label();
            Label3.Tag = "-";
            var Tx_NroOpe_004 = new UI_TextBox();
            Tx_NroOpe = new UI_TextBox[] { Tx_NroOpe_000, Tx_NroOpe_001, Tx_NroOpe_002, Tx_NroOpe_003, Tx_NroOpe_004 };
            Ok_Operacion = new UI_Button(); 
            Co_Boton = new UI_Button[2] { new UI_Button(), new UI_Button() };
            Lbl_Cliente = new UI_Label();
            Tx_Prty = new UI_TextBox();

            Frame3D2 = new UI_Frame();
            Frame3D2.Caption = "Planillas";
            Lt_Pln = new UI_Combo();

            lbl_tipAnu = new UI_Label();
            CB_Tipanu = new UI_Combo();

            lbl_tipCam = new UI_Label(); 
            CAM_Tipcam = new UI_TextBox();

            Bot_Dec = new UI_Button();
            BOT_Obs = new UI_Button();
            Ok = new UI_Button();
            
            Fr_Invisible = new UI_Frame();
            Fr_Invisible.Caption = "Autorización previa del Banco Central";

            Label6 = new UI_Label(); 
            Tx_NroPln = new UI_TextBox();

            Label10 = new UI_Label(); 
            Tx_Fecha = new UI_TextBox();

            Label4 = new UI_Label(); 
            Cb_Pbc = new UI_Combo();

            Label7 = new UI_Label(); 
            Tx_Motivo = new UI_TextBox();

            Label9 = new UI_Label(); 
            Cb_Tipo = new UI_Combo();

            Label8 = new UI_Label(); 
            Tx_TipCam = new UI_TextBox();

            Tx_ObsPln = new UI_TextBox();
            Co_Volver = new UI_Button(); 
            Errores = new List<UI_Message>();

            Fr_Observaciones = new UI_Frame(); 
            PopUps = new List<UI_Message>();
            Errores = new List<UI_Message>(); 

        }
   }
}

