using BCH.Comex.Common.UI_Modulos;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_Comercio_Invisibles : UI_Frm
    {
        public string OPESIN { set; get; }
        public List<UI_Button> Co_Boton{set;get;}
        public List<UI_Label> Label1{set;get;}
        public List<UI_TextBox> Tx_MtoCV{set;get;}
        public UI_Frame Titulo{set;get;}
        public UI_Grid Lt_Operacion{set;get;}
        public UI_Label Lb_Titulo_Operacion{set;get;}
        public UI_Frame Fr_Convenio{set;get;}
        public UI_TextBox Tx_FecDeb{set;get;}
        public UI_TextBox Tx_DocNac{set;get;}
        public UI_TextBox Tx_DocExt{set;get;}
        public UI_Frame Fr_Declaracion{set;get;}
        public UI_TextBox Tx_ER{set;get;}
        public UI_TextBox Tx_CodAdn{set;get;}
        public UI_TextBox Tx_FecDec{set;get;}
        public UI_TextBox Tx_NroDec{set;get;}
        public UI_Frame Fr_OpeD{set;get;}
        public UI_TextBox Tx_ParTip{set;get;}
        public UI_Combo Cb_InsUt{set;get;}
        public UI_TextBox Tx_FecSus{set;get;}
        public UI_TextBox Tx_NumCon{set;get;}
        public UI_Combo Cb_ArCon{set;get;}
        public UI_TextBox Tx_FecVen{set;get;}
        public UI_Frame Fr_Sec{set;get;}
        public UI_Combo Cb_SecEcIn{set;get;}
        public UI_TextBox Tx_PrcPar{set;get;}
        public UI_Combo Cb_SecEcBen{set;get;}
        public UI_Label Lb_PrcPar{set;get;}
        public UI_Label Lb_SecEcBen{set;get;}
        public UI_Frame Fr_OpRe{set;get;}
        public UI_TextBox Tx_FecPre{set;get;}
        public UI_TextBox Tx_NumPre{set;get;}
        public UI_TextBox Tx_CodIns{set;get;}
        public UI_Frame Fr_OFI{set;get;}
        public UI_Combo Cb_MonDes{set;get;}
        public UI_TextBox Tx_Mto{set;get;}
        public UI_TextBox Tx_Fecha{set;get;}
        public UI_TextBox Tx_NumIns{set;get;}
        public UI_TextBox Tx_FecIns{set;get;}
        public UI_TextBox Tx_NomFin{set;get;}
        public UI_TextBox Tx_FecVC{set;get;}
        public UI_Frame Fr_Autori{set;get;}
        public UI_TextBox Tx_NroAut{set;get;}
        public UI_TextBox Tx_FecAut{set;get;}
        public UI_TextBox Tx_SucBcch{set;get;}
        public UI_Combo Cb_TipAut{set;get;}
        public UI_Frame Fr_Ope{set;get;}
        public UI_Button OK{set;get;}
        public UI_Button NO{set;get;}
        public UI_CheckBox ch_ZoFra{set;get;}
        public UI_CheckBox ch_AfDer{set;get;}
        public UI_CheckBox Ch_Convenio{set;get;}
        public UI_TextBox Tx_CanAc{set;get;}
        public UI_Combo Lt_Tcp{set;get;}
        public UI_Combo Cb_Moneda{set;get;}
        public UI_Combo Cb_Divisa{set;get;}
        public UI_Combo Cb_Pais{set;get;}
        public UI_Label Label8{set;get;}
        public UI_Label Label4{set;get;}
        public UI_Label Label3{set;get;}
        public UI_Label Label2{set;get;}

        public bool LV_PasePorIngresoValores { set; get; }

        public int CargaAutomatica { get; set; }

        //*************************
        //*************************


        public short IndDec
        {
            set; get;
        }  //Registra si es Declaración.
        public short TipDiv { set; get; }  //Registra el tipo de operacion de divisas.-
        public short Switch { set; get; }

        // UPGRADE_INFO (#0561): The 'Concepai' symbol was defined without an explicit "As" clause.
        public const string Concepai = "151114;152412;10055;176109;25231K;252727;26220K;20055;275107";
        public short OPER_AUTOMATICA { set; get; }

        public UI_Frm_Comercio_Invisibles(){
            OPESIN = String.Empty;
            LV_PasePorIngresoValores = false;
            Co_Boton = new List<UI_Button>() { new UI_Button(), new UI_Button() };
            Titulo = new UI_Frame();
            Titulo.Caption = "Titulo";
            Lt_Operacion = new UI_Grid();
            Lb_Titulo_Operacion = new UI_Label();
            Lb_Titulo_Operacion.Text = "Titulo";
            Fr_Convenio = new UI_Frame();
            Fr_Convenio.Caption = "Conv.Crédito Recíproco";

            Tx_FecDeb = new UI_TextBox();
            Tx_DocNac = new UI_TextBox();
            Tx_DocExt = new UI_TextBox();

            Fr_Convenio.Controles.Add(Tx_FecDeb);
            Fr_Convenio.Controles.Add(Tx_DocNac);
            Fr_Convenio.Controles.Add(Tx_DocExt);

            Label1 = new List<UI_Label>() { new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label(), new UI_Label() };
            
            Fr_Declaracion = new UI_Frame();
            Fr_Declaracion.Caption = "Datos Declaración";

            Tx_ER = new UI_TextBox();
            Tx_ER.Tag = "___";

            Tx_CodAdn = new UI_TextBox();
            Tx_CodAdn.Tag = "___";


            Tx_FecDec = new UI_TextBox();
            Tx_NroDec = new UI_TextBox();

            Fr_Convenio.Controles.Add(Tx_ER);
            Fr_Convenio.Controles.Add(Tx_CodAdn);
            Fr_Convenio.Controles.Add(Tx_FecDec);
            Fr_Convenio.Controles.Add(Tx_NroDec);

            Fr_OpeD = new UI_Frame();
            Fr_OpeD.Caption= "Operaciones de Derivados";

            Tx_ParTip = new UI_TextBox();
            Tx_ParTip.Tag = "______.____";


            Cb_InsUt = new UI_Combo();
            Tx_FecSus = new UI_TextBox();

            Tx_NumCon = new UI_TextBox();
            Tx_NumCon.Tag = "________";

            Cb_ArCon = new UI_Combo();
            Tx_FecVen = new UI_TextBox();

            Fr_OpeD.Controles.Add(Tx_ParTip);
            Fr_OpeD.Controles.Add(Cb_InsUt);
            Fr_OpeD.Controles.Add(Tx_FecSus);
            Fr_OpeD.Controles.Add(Tx_NumCon);
            Fr_OpeD.Controles.Add(Cb_ArCon);
            Fr_OpeD.Controles.Add(Tx_FecVen);

            Fr_Sec = new UI_Frame();
            Fr_Sec.Caption = "Sectores";

            Cb_SecEcIn = new UI_Combo();

            Tx_PrcPar = new UI_TextBox();
            Tx_PrcPar.Tag = "____._";

            Cb_SecEcBen = new UI_Combo();

            Lb_PrcPar = new UI_Label();
            Lb_SecEcBen = new UI_Label();

            Fr_Sec.Controles.Add(Cb_SecEcIn);
            Fr_Sec.Controles.Add(Cb_SecEcBen);
            
            Fr_Sec.Controles.Add(Tx_PrcPar);


            Fr_OpRe = new UI_Frame();
            Fr_OpRe.Caption = "Operación relacionada";


            Tx_FecPre = new UI_TextBox();

            Tx_NumPre = new UI_TextBox();
            Tx_NumPre.Tag = "______";

            Tx_CodIns = new UI_TextBox();
            Tx_CodIns.Tag = "___";

            Fr_OpRe.Controles.Add(Tx_FecPre);
            Fr_OpRe.Controles.Add(Tx_NumPre);
            Fr_OpRe.Controles.Add(Tx_CodIns);

            Fr_OFI = new UI_Frame();
            Fr_OFI.Caption = "Operaciones Financieras Internacionales";

            Cb_MonDes = new UI_Combo();

            Tx_Mto = new UI_TextBox();
            Tx_Mto.Tag = "_____________.__";

            Tx_Fecha = new UI_TextBox();

            Tx_NumIns = new UI_TextBox();
            Tx_NumIns.Tag = "______";

            Tx_FecIns = new UI_TextBox();
            Tx_NomFin = new UI_TextBox();
            Tx_FecVC = new UI_TextBox();

            Fr_OFI.Controles.Add(Cb_MonDes);
            Fr_OFI.Controles.Add(Tx_Mto);
            Fr_OFI.Controles.Add(Tx_Fecha);
            Fr_OFI.Controles.Add(Tx_NumIns);
            Fr_OFI.Controles.Add(Tx_FecIns);
            Fr_OFI.Controles.Add(Tx_NomFin);
            Fr_OFI.Controles.Add(Tx_FecVC);


            Fr_Autori = new UI_Frame();
            Fr_Autori.Caption = "Datos Aut. Bco. Central";

            Tx_NroAut = new UI_TextBox();
            Tx_NroAut.Tag = "______";

            Tx_FecAut = new UI_TextBox();

            Tx_SucBcch = new UI_TextBox();
            Tx_SucBcch.Tag = "__";

            Cb_TipAut = new UI_Combo();

            this.Fr_Autori.Controles.Add(this.Tx_NroAut);
            this.Fr_Autori.Controles.Add(this.Tx_FecAut);
            this.Fr_Autori.Controles.Add(this.Tx_SucBcch);
            this.Fr_Autori.Controles.Add(this.Cb_TipAut);

            Fr_Ope = new UI_Frame();


            OK = new UI_Button();
            NO=new UI_Button();

            ch_ZoFra = new UI_CheckBox();
            ch_AfDer = new UI_CheckBox();
            Ch_Convenio = new UI_CheckBox();

            var Tx_MtoCV_000 = new UI_TextBox();
            Tx_MtoCV_000.Tag = "_____________.__";
            Tx_MtoCV_000.Mask = "_____________.__";
            var Tx_MtoCV_001 = new UI_TextBox();
            Tx_MtoCV_001.Tag = "________.____";
            Tx_MtoCV_001.Mask = "________.____";
            var Tx_MtoCV_003 = new UI_TextBox();
            Tx_MtoCV_003.Tag = "_______.__________";
            Tx_MtoCV_003.Mask = "_______.__________";
            Tx_MtoCV_003.Enabled = false;
            var Tx_MtoCV_002 = new UI_TextBox();
            Tx_MtoCV_002.Tag = "_____________";
            Tx_MtoCV_002.Mask = "_____________";
            Tx_MtoCV_002.Enabled = false;

            Tx_MtoCV = new List<UI_TextBox>() { Tx_MtoCV_000, Tx_MtoCV_001,  Tx_MtoCV_002, Tx_MtoCV_003 };

            Tx_CanAc = new UI_TextBox();
            Tx_CanAc.Tag = "_";


            Lt_Tcp = new UI_Combo();
            Cb_Moneda = new UI_Combo();
            Cb_Divisa = new UI_Combo();
            Cb_Pais = new UI_Combo();


            this.Fr_Ope.Controles.Add(this.OK);
            this.Fr_Ope.Controles.Add(this.NO);
            this.Fr_Ope.Controles.Add(this.ch_ZoFra);
            this.Fr_Ope.Controles.Add(this.ch_AfDer);
            this.Fr_Ope.Controles.Add(this.Ch_Convenio);
            this.Fr_Ope.Controles.Add(Tx_MtoCV_000);
            this.Fr_Ope.Controles.Add(Tx_MtoCV_001);
            this.Fr_Ope.Controles.Add(Tx_MtoCV_003);
            this.Fr_Ope.Controles.Add(Tx_MtoCV_002);
            this.Fr_Ope.Controles.Add(this.Tx_CanAc);
            this.Fr_Ope.Controles.Add(this.Lt_Tcp);
            this.Fr_Ope.Controles.Add(this.Cb_Moneda);
            this.Fr_Ope.Controles.Add(this.Cb_Divisa);
            this.Fr_Ope.Controles.Add(this.Cb_Pais);

            LV_PasePorIngresoValores = false;
            CargaAutomatica = 0;
        }


    }
}
