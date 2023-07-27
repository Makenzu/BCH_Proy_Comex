using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_Destino_Fondos : UI_Frm 
    {
        public bool ErrorEnOk { set; get; }
        public bool VuelveDeNemonico { set; get; }
        public bool VuelveDeOtro { set; get; }
        public short Indice_Cuenta { set; get; }  //Almecena el Itemdata de la Lista de Cuentas.
        public short Indice_Monto { set; get; }  //Almecena el Itemdata de la Lista de Montos.
        public short Indice_CtaCte { set; get; } //Almecena el Itemdata de la Lista de Cuentas de un Partys seleccionado.
        public short Indice_Destino { set; get; }  //Almecena el Itemdata de la Combox de Destinos de Fondos M/E.
        //****************************************************************************
        //Variables para determinar si algunos campos ya fueron validados para no
        //volver a validarlos de nuevo.
        //Formato    => $(indice a validar) $(resultado de la validación).
        public string SCSMN_OK { set; get; }  //Saldos c/ Sucursales M/N.
        public string SCSME_OK { set; get; }  //Saldos c/ Sucursales M/E.
        public string VAM_OK { set; get; }  //Varios Acreedores Importaciones.
        public string VAX_OK { set; get; }  //Varios Acreedores Exportaciones.
        public string VAMC_OK { set; get; }  //Varios Acreedores Mercado de Corredores.
        public string ONMN_OK { set; get; }  //Otro Nemónico M/N.
        public string ONME_OK { set; get; }  //Otro Nemónico M/E.


        public List<UI_TextBox> Text1 { set; get; }
        public List<UI_Label> Label3 { set; get; }
        public List<UI_Button> Boton { set; get; }
        public List<UI_TextBox> Tx_Datos { set; get; }
        public List<UI_Label> Tx_Cuentas { set; get; }
        public List<UI_Label> Lb_Datos { set; get; }
        public List<UI_Label> Titulo { set; get; }

        public UI_Frame frm_infoctagap { set; get; }
        public UI_TextBox txt_cuenta { set; get; }
        public UI_TextBox txt_CRN { set; get; }
        public UI_Label lbl_cuenta { set; get; }
        public UI_Label Label1 { set; get; }
        public UI_Frame frm_datos { set; get; }
        public UI_Frame Frame3D1 { set; get; }
        public UI_Combo cmb_codtran { set; get; }
        public UI_Button BNem { set; get; }
        public UI_TextBox txtNumRef { set; get; }
        public UI_Button OK { set; get; }
        public UI_Button NO { set; get; }
        public UI_CheckBox Ch_ImpChq { set; get; }
        public UI_CheckBox Ch_GenPln { set; get; }
        public UI_Button Bt_PlnTrn { set; get; }
        public UI_Combo Cb_Destino { set; get; }
        public UI_Combo L_Partys { set; get; }
        public UI_Combo L_Cuentas { set; get; }
        public UI_Button Ok_Partys { set; get; }
        public UI_TextBox MtoVia { set; get; }
        public UI_Grid l_via { set; get; }
        public UI_Combo L_Mnd { set; get; }
        public UI_Grid l_mto { set; get; }
        public UI_Label LB_Referencia { set; get; }
        public UI_Label Lb_Oficina { set; get; }
        public UI_Combo L_Cta { set; get; }

        /// <summary>
        /// Si el usuario entra a la pantalla por 2da vez, con los destinos ya definidos, hay que mostrar el popup
        /// </summary>
        public bool MostrarVolverADefinir { get; set; }
        public int CargaAutomatica { get; set; }

        public string Caption { get; set; }

        public UI_Frm_Destino_Fondos()
        {
            ErrorEnOk = false;
            VuelveDeNemonico = false;
            VuelveDeOtro = false;
            Text1 = new List<UI_TextBox>() { new UI_TextBox(), new UI_TextBox(), new UI_TextBox(), new UI_TextBox(), new UI_TextBox() { Enabled = false } }; 
            Label3 = new List<UI_Label>() { new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label()};
            Boton = new List<UI_Button> { new UI_Button(), new UI_Button() };
            Tx_Datos = new List<UI_TextBox>() { new UI_TextBox(), new UI_TextBox(), new UI_TextBox() };
            Tx_Cuentas = new List<UI_Label>() { new UI_Label(), new UI_Label()};
            Lb_Datos = new List<UI_Label>() { new UI_Label(), new UI_Label(), new UI_Label()};
            Titulo = new List<UI_Label>() { new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label()};

            frm_infoctagap = new UI_Frame();
            frm_infoctagap.Caption = "Información Cuentas GAP";

            txt_cuenta = new UI_TextBox();
            txt_CRN = new UI_TextBox();

            lbl_cuenta = new UI_Label();
            lbl_cuenta.Text= "Cuenta";

            Label1 = new UI_Label();
            Label1.Text= "C. R. Number";

            frm_datos = new UI_Frame();
            frm_datos.Visible = false;

            Frame3D1 = new UI_Frame();

            cmb_codtran = new UI_Combo();
            BNem = new UI_Button();

            txtNumRef = new UI_TextBox();
            txtNumRef.Tag= "________";

            OK = new UI_Button();
            NO = new UI_Button();

            Ch_ImpChq = new UI_CheckBox();
            Ch_GenPln = new UI_CheckBox();
            Bt_PlnTrn = new UI_Button();
            Cb_Destino = new UI_Combo();
            L_Partys = new UI_Combo();
            L_Cuentas = new UI_Combo();
            Ok_Partys = new UI_Button();

            MtoVia = new UI_TextBox();
            MtoVia.Tag = "_____________.__";

            L_Cta = new UI_Combo();
            L_Mnd = new UI_Combo();
            l_mto = new UI_Grid();
            l_via = new UI_Grid();

            LB_Referencia = new UI_Label();
            Lb_Oficina = new UI_Label();

            CargaAutomatica = 0;

        }
    }
}
