using BCH.Comex.Common.UI_Modulos;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_PlvSO
    {
        public short Flag_TipCam;
        public short IndiceInt;
        public short IndiceFin;
        public short FlgYaMostro;
        public double MtoInteres;
        public short IngPlan;
        public short Bandera;
        public string guardaobs = "";
        public int NumIdi;

        public string numdec = "";
        public string FecDec = "";
        public short CodPag;


        public UI_CheckBox Ch_PlanRee { set; get; }
        public UI_CheckBox Ch_ZonFra { set; get; }
        public UI_CheckBox Ch_ClauRo { set; get; }
        public UI_CheckBox Ch_Endoso { set; get; }
        public UI_CheckBox Ch_CPagos { set; get; }
        public UI_CheckBox Ch_Acuerdo { set; get; }
        public UI_CheckBox Ch_ConvCre { set; get; }
        public UI_CheckBox Ch_Transf { set; get; }

        public UI_Button Bot_OkFinal { set; get; }
        public UI_Button Bot_NoFinal { set; get; }
        public UI_Button Bot_OkDec { set; get; }
        public UI_Button Bot_NoDec { set; get; }
        public UI_Button Bot_Clientes { set; get; }
        public UI_Button Bot_Acepta { set; get; }
        public UI_Button Bot_Cancel { set; get; }

        //Panels
        public UI_TextBox Pn_TCDol { set; get; }
        public UI_TextBox Pn_ValCif { set; get; }
        public UI_TextBox Pn_CifDol { set; get; }
        public UI_TextBox Pn_TotDol { set; get; }
        public UI_TextBox Pn_MtoTot { set; get; }
        public UI_TextBox Pn_RutImp { set; get; }
        public UI_TextBox Pn_Import { set; get; }
        public UI_TextBox Pn_NroPre { set; get; }
        public UI_TextBox Pn_CodMon { set; get; }

        public UI_Label Lb_Transf{ set; get; }
        public UI_Label Label7{ set; get; }
        public UI_Label Lb_CodPag{ set; get; }
        public UI_Label Lb_FDec{ set; get; }
        public UI_Label Lb_Moneda{ set; get; }
        public UI_Label Lb_DImp{ set; get; }
        public UI_Label Lb_NumCon{ set; get; }
        public UI_Label Lb_FecCon{ set; get; }
        public UI_Label Lb_FecVto{ set; get; }
        public UI_Label Lb_Zonfra{ set; get; }
        public UI_Label Lb_Endoso{ set; get; }
        public UI_Label Lb_ClauRo{ set; get; }
        public UI_Label Lb_Observ{ set; get; }
        public UI_Label Lb_NemTC{ set; get; }
        public UI_Label Label29{ set; get; }
        public UI_Label Label32{ set; get; }
        public UI_Label Label33{ set; get; }
        public UI_Label Label34{ set; get; }
        public UI_Label Lb_TipCam{ set; get; }
        public UI_Label Lb_Paridad{ set; get; }
        public UI_Label Label12{ set; get; }
        public UI_Label Lb_MtoFob{ set; get; }
        public UI_Label Lb_MtoFle{ set; get; }
        public UI_Label Lb_MtoSeg{ set; get; }
        public UI_Label Label4{ set; get; }
        public UI_Label Label3{ set; get; }
        public UI_Label Lb_PlzBco{ set; get; }
        public UI_Label Lb_NPre{ set; get; }
        public UI_Label Lb_CapBase_000{ set; get; }
        public UI_Label Label5{ set; get; }
        public UI_Label Lb_NroPre{ set; get; }
        public UI_Label Lb_FecRee{ set; get; }
        public UI_Label Lb_NCPago{ set; get; }
        public UI_Label Lb_CanAc{ set; get; }
        public UI_Label Lb_NumAc{ set; get; }
        public UI_Label Lb_FecAut{ set; get; }
        public UI_Label Lb_DocChi{ set; get; }
        public UI_Label Lb_DocExt{ set; get; }
        public UI_Label Lb_PPago{ set; get; }
        public UI_Label Lb_NCuota{ set; get; }

        public UI_TextBox Tx_Observ{ set; get; }
        public UI_TextBox Tx_NroCon{ set; get; }
        public UI_TextBox Tx_FecVen{ set; get; }
        public UI_TextBox Tx_FecCon{ set; get; }
        public UI_TextBox Tx_Paridad{ set; get; }
        public UI_TextBox Tx_TipCam{ set; get; }
        public UI_TextBox Tx_MtoSeg{ set; get; }
        public UI_TextBox Tx_MtoFle{ set; get; }
        public UI_TextBox Tx_MtoFob{ set; get; }
        public UI_TextBox Tx_FecRee{ set; get; }
        public UI_TextBox Tx_NroPre{ set; get; }
        public UI_TextBox Tx_NCuota{ set; get; }
        public UI_TextBox Tx_NCpago{ set; get; }
        public UI_TextBox Tx_NumAc2{ set; get; }
        public UI_TextBox Tx_CantAc{ set; get; }
        public UI_TextBox Tx_NumAc1{ set; get; }
        public UI_TextBox Tx_DocExt{ set; get; }
        public UI_TextBox Tx_DocChi{ set; get; }
        public UI_TextBox Tx_FecDeb{ set; get; }
        public UI_TextBox Tx_FecDec{ set; get; }
        public UI_TextBox Tx_CodPag{ set; get; }
        public UI_TextBox Tx_NumDec{ set; get; }//VB6MaskEdBox

        public UI_Frame Fr_Montos{ set; get; }
        public UI_Frame Fr_Final{ set; get; }
        public UI_Frame Fr_PlanRee{ set; get; }
        public UI_Frame Fr_Presen{ set; get; }
        public UI_Frame Fr_Acuerdos{ set; get; }
        public UI_Frame Fr_Cpagos{ set; get; }
        public UI_Frame Fr_ConvCre{ set; get; }
        public UI_Frame Fr_Conoc{ set; get; }
        public UI_Frame Fr_DecImp{ set; get; }

        public UI_Combo Lt_Final{ set; get; }
        public UI_Combo Cb_Pbc{ set; get; }
        public UI_Combo Cb_PPago{ set; get; }
        public UI_Combo Cb_Moneda{ set; get; }        

        public UI_Frm_PlvSO()
        {
            Ch_PlanRee = new UI_CheckBox();
            Ch_ZonFra = new UI_CheckBox();
            Ch_ClauRo = new UI_CheckBox();
            Ch_Endoso = new UI_CheckBox();
            Ch_CPagos = new UI_CheckBox();
            Ch_Acuerdo = new UI_CheckBox();
            Ch_ConvCre = new UI_CheckBox();
            Ch_Transf = new UI_CheckBox();

            Lt_Final = new UI_Combo();
            Cb_Pbc = new UI_Combo();
            Cb_PPago = new UI_Combo();
            Cb_Moneda = new UI_Combo();

            Tx_Observ = new UI_TextBox();
            Tx_NroCon = new UI_TextBox();
            Tx_FecVen = new UI_TextBox();
            Tx_FecCon = new UI_TextBox();
            Tx_Paridad = new UI_TextBox() { Enabled = false };
            Tx_TipCam = new UI_TextBox();
            Tx_MtoSeg = new UI_TextBox();
            Tx_MtoFle = new UI_TextBox();
            Tx_MtoFob = new UI_TextBox();
            Tx_FecRee = new UI_TextBox();
            Tx_NroPre = new UI_TextBox();
            Tx_NCuota = new UI_TextBox();
            Tx_NCpago = new UI_TextBox();
            Tx_NumAc2 = new UI_TextBox();
            Tx_CantAc = new UI_TextBox();
            Tx_NumAc1 = new UI_TextBox();
            Tx_DocExt = new UI_TextBox();
            Tx_DocChi = new UI_TextBox();
            Tx_FecDeb = new UI_TextBox();
            Tx_FecDec = new UI_TextBox();
            Tx_CodPag = new UI_TextBox();
            Tx_NumDec = new UI_TextBox();

            //Panels
            Pn_TCDol = new UI_TextBox();
            Pn_ValCif = new UI_TextBox();
            Pn_CifDol = new UI_TextBox();
            Pn_TotDol = new UI_TextBox();
            Pn_MtoTot = new UI_TextBox();
            Pn_RutImp = new UI_TextBox();
            Pn_Import = new UI_TextBox();
            Pn_NroPre = new UI_TextBox();
            Pn_CodMon = new UI_TextBox();


            Bot_OkFinal = new UI_Button();
            Bot_NoFinal = new UI_Button();
            Bot_OkDec = new UI_Button();
            Bot_NoDec = new UI_Button();
            Bot_Clientes = new UI_Button();
            Bot_Acepta = new UI_Button();
            Bot_Cancel = new UI_Button();

            Fr_Montos = new UI_Frame();
            Fr_Final = new UI_Frame();
            Fr_PlanRee = new UI_Frame();
            Fr_Presen = new UI_Frame();
            Fr_Acuerdos = new UI_Frame();
            Fr_Cpagos = new UI_Frame();
            Fr_ConvCre = new UI_Frame();
            Fr_Conoc = new UI_Frame();
            Fr_DecImp = new UI_Frame();

            Lb_Transf = new UI_Label();
            Label7 = new UI_Label();
            Lb_CodPag = new UI_Label();
            Lb_FDec = new UI_Label();
            Lb_Moneda = new UI_Label();
            Lb_DImp = new UI_Label();
            Lb_NumCon = new UI_Label();
            Lb_FecCon = new UI_Label();
            Lb_FecVto = new UI_Label();
            Lb_Zonfra = new UI_Label();
            Lb_Endoso = new UI_Label();
            Lb_ClauRo = new UI_Label();
            Lb_Observ = new UI_Label();
            Lb_NemTC = new UI_Label();
            Label29 = new UI_Label();
            Label32 = new UI_Label();
            Label33 = new UI_Label();
            Label34 = new UI_Label();
            Lb_TipCam = new UI_Label();
            Lb_Paridad = new UI_Label();
            Label12 = new UI_Label();
            Lb_MtoFob = new UI_Label();
            Lb_MtoFle = new UI_Label();
            Lb_MtoSeg = new UI_Label();
            Label4 = new UI_Label();
            Label3 = new UI_Label();
            Lb_PlzBco = new UI_Label();
            Lb_NPre = new UI_Label();
            Lb_CapBase_000 = new UI_Label();
            Label5 = new UI_Label();
            Lb_NroPre = new UI_Label();
            Lb_FecRee = new UI_Label();
            Lb_NCPago = new UI_Label();
            Lb_CanAc = new UI_Label();
            Lb_NumAc = new UI_Label();
            Lb_FecAut = new UI_Label();
            Lb_DocChi = new UI_Label();
            Lb_DocExt = new UI_Label();
            Lb_PPago = new UI_Label();
            Lb_NCuota = new UI_Label();
        }
    }
}
