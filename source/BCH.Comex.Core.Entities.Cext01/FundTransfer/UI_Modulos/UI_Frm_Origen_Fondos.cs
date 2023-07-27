using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_Origen_Fondos: UI_Frm
    {
        public bool ErrorEnOk;
        public bool VuelveDeNemonico { set; get; }
        public bool VuelveDeOtro { set; get; }
        public List<UI_Button> Boton { set; get; }
        public List<UI_TextBox> Text1 { set; get; }
        public List<UI_Label> Label3 { set; get; }
        public List<UI_TextBox> Tx_Datos { set; get; }
        public List<UI_Label> Tx_Cuentas { set; get; }
        public List<UI_Label> Lb_Datos { set; get; }
        public List<UI_Label> Titulo { set; get; }
        public UI_Frame frm_infoctagap { set; get; }
        public UI_TextBox txt_cuenta { set; get; }
        public UI_TextBox txt_CRN { set; get; }
        public UI_Label lbl_cuenta { set; get; }
        public UI_Label Label1 { set; get; }
        public UI_Frame Frame3D1 { set; get; }
        public UI_Combo cmb_codtran { set; get; }
        public UI_Frame frm_datos { set; get; }
        public UI_Button BNem { set; get; }
        public UI_TextBox txtNumRef { set; get; }
        public UI_Button OK { set; get; }
        public UI_Button NO { set; get; }
        public UI_CheckBox Ch_ImpDeb { set; get; }
        public UI_TextBox MtoOri { set; get; }
        public UI_Button Bt_PlnTrn { set; get; }
        public UI_Grid l_ori { set; get; }
        public UI_Combo L_Cuentas { set; get; }
        public UI_Combo L_Partys { set; get; }
        public UI_Combo l_mnd { set; get; }
        public UI_Grid l_mto { set; get; }
        public UI_Label LB_Referencia { set; get; }
        public UI_Label Lb_Oficina { set; get; }

        public short indiceVxOri { get; set; }
        public short Indice_Cuenta { set; get; }  //Almecena el Itemdata de la Lista de Cuentas.
        public short Indice_Monto { set; get; }  //Almecena el Itemdata de la Lista de Montos.
        public short Indice_CtaCte { set; get; }  //Almecena el Itemdata de la Lista de Cuentas de un Partys seleccionado.
                                      //****************************************************************************
                                      //Variables para determinar si algunos campos ya fueron validados para no
                                      //volver a validarlos de nuevo.

        //Formato    => $(indice a validar) $(resultado de la validación).

        public string SCSMN_OK { set; get; }  //Saldos c/ Sucursales M/N.
        public string SCSME_OK { set; get; }  //Saldos c/ Sucursales M/E.
        public string VVBCH_OK { set; get; }  //Vale Vista Banco de Chile.
        public string VAM_OK { set; get; }  //Varios Acreedores Importaciones.
        public string VAX_OK { set; get; }  //Varios Acreedores Exportaciones.
        public string VAMC_OK { set; get; }  //Varios Acreedores Mercado de Corredores.
        public string ONMN_OK { set; get; }  //Otro Nemónico M/N.
        public string ONME_OK { set; get; }  //Otro Nemónico M/E.
                                     //****************************************************************************
        public int CargaAutomatica { get; set; }
        //Debe venir al final, ya que genera problema por KO en la pagina
        public UI_Combo L_Cta { set; get; }

        public UI_Frm_Origen_Fondos()
        {
            VuelveDeNemonico = false;
            VuelveDeOtro = false;
            ErrorEnOk = false;

            SCSMN_OK = "";  //Saldos c/ Sucursales M/N.
            SCSME_OK = "";  //Saldos c/ Sucursales M/E.
            VVBCH_OK = "";  //Vale Vista Banco de Chile.
            VAM_OK = "";  //Varios Acreedores Importaciones.
            VAX_OK = "";  //Varios Acreedores Exportaciones.
            VAMC_OK = "";  //Varios Acreedores Mercado de Corredores.
            ONMN_OK = "";  //Otro Nemónico M/N.
            ONME_OK = "";  //Otro Nemónico M/E.


            Boton = new List<UI_Button> { new UI_Button(), new UI_Button() };

            frm_infoctagap = new UI_Frame();
            txt_cuenta = new UI_TextBox();
            txt_CRN = new UI_TextBox();
            lbl_cuenta = new UI_Label();
            Label1 = new UI_Label();
            Frame3D1 = new UI_Frame();
            cmb_codtran = new UI_Combo();
            frm_datos = new UI_Frame();
            BNem = new UI_Button();
            txtNumRef = new UI_TextBox();
            OK = new UI_Button();
            NO = new UI_Button();
            Ch_ImpDeb = new UI_CheckBox();
            MtoOri = new UI_TextBox();
            L_Cta = new UI_Combo();
            Bt_PlnTrn = new UI_Button();
            l_ori = new UI_Grid();
            L_Cuentas = new UI_Combo();
            L_Partys = new UI_Combo();
            l_mnd = new UI_Combo();
            l_mto = new UI_Grid();
            LB_Referencia = new UI_Label();
            Lb_Oficina = new UI_Label();

            Boton = new List<UI_Button> { new UI_Button(), new UI_Button() };
            Text1 = new List<UI_TextBox> { new UI_TextBox(), new UI_TextBox(), new UI_TextBox(), new UI_TextBox(), new UI_TextBox() { Enabled = false } };
            Label3 = new List<UI_Label> { new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label() };
            Tx_Datos = new List<UI_TextBox> { new UI_TextBox(), new UI_TextBox(), new UI_TextBox()};
            Tx_Cuentas = new List<UI_Label> { new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label() };
            Lb_Datos = new List<UI_Label> { new UI_Label(), new UI_Label(), new UI_Label() };

            CargaAutomatica = 0;
        }
    }
}
