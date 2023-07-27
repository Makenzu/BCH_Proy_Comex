using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_Rem_PVI
    {

        public UI_TextBox Tx_NumDec { get; set; }
        public UI_TextBox Tx_FecDec { get; set; }
        public UI_TextBox Tx_CodPag { get; set; }
        public UI_TextBox Pn_NroPre { get; set; }
        public UI_Combo Cb_Pbc { get; set; }
        public UI_TextBox Pn_Import { get; set; }
        public UI_TextBox Pn_RutImp { get; set; }
        public UI_TextBox Tx_NroCon { get; set; }
        public UI_TextBox Tx_FecCon { get; set; }
        public UI_TextBox Tx_FecVen { get; set; }
        public UI_TextBox Tx_CodPai { get; set; }
        public UI_TextBox Pn_PPago { get; set; }
        public UI_TextBox Pn_CodMon { get; set; }
        public UI_TextBox Pn_Moneda { get; set; }
        public UI_CheckBox Ch_ConvCre { get; set; }
        public UI_CheckBox Ch_Acuerdo { get; set; }
        public UI_CheckBox Ch_CPagos { get; set; }
        public UI_TextBox Tx_FecDeb { get; set; }
        public UI_TextBox Tx_DocChi { get; set; }
        public UI_TextBox Tx_DocExt { get; set; }
        public UI_TextBox Tx_CantAc { get; set; }
        public UI_TextBox Tx_NumAc1 { get; set; }
        public UI_TextBox Tx_NumAc2 { get; set; }
        public UI_TextBox Tx_NCpago { get; set; }
        public UI_TextBox Tx_NCuota { get; set; }
        public UI_TextBox Tx_NroPre { get; set; }
        public UI_TextBox Tx_FecRee { get; set; }
        public UI_TextBox Tx_Observ { get; set; }
        public UI_ListBox Lt_Final { get; set; }
        public UI_TextBox Tx_MtoFob { get; set; }
        public UI_TextBox Tx_MtoFle { get; set; }
        public UI_TextBox Tx_MtoSeg { get; set; }
        public UI_TextBox Pn_ValCif { get; set; }
        public UI_TextBox Tx_IntPla { get; set; }
        public UI_TextBox Tx_GasBan { get; set; }
        public UI_TextBox Pn_MtoTot { get; set; }
        public UI_TextBox Pn_CifDol { get; set; }
        public UI_TextBox Pn_TotDol { get; set; }
        public UI_TextBox Tx_TipCam { get; set; }
        public UI_TextBox Pn_TCDol { get; set; }
        public UI_TextBox Tx_Paridad { get; set; }

        public UI_Label Lb_NemTC { get; set; }
        public UI_Button Bot_OkDec { get; set; }
        public UI_Button Bot_NoDec { get; set; }
        public UI_Button Bot_OkFinal { get; set; }
        public UI_Button Bot_NoFinal { get; set; }
        public UI_Button Bot_Acepta { get; set; }
        public UI_Button Bot_Cancel { get; set; }

        public List<UI_Message> PopUps;

        public UI_Frm_Rem_PVI()
        {

            Tx_NumDec = new UI_TextBox();
            Tx_FecDec = new UI_TextBox();
            Tx_CodPag = new UI_TextBox();
            Pn_NroPre = new UI_TextBox();
            Cb_Pbc = new UI_Combo();
            Pn_Import = new UI_TextBox();
            Pn_RutImp = new UI_TextBox();
            Tx_NroCon = new UI_TextBox();
            Tx_FecCon = new UI_TextBox();
            Tx_FecVen = new UI_TextBox();
            Tx_CodPai = new UI_TextBox();
            Pn_PPago = new UI_TextBox();
            Pn_CodMon = new UI_TextBox();
            Pn_Moneda = new UI_TextBox();
            Ch_ConvCre = new UI_CheckBox();
            Ch_Acuerdo = new UI_CheckBox();
            Ch_CPagos = new UI_CheckBox();
            Tx_FecDeb = new UI_TextBox();
            Tx_DocChi = new UI_TextBox();
            Tx_DocExt = new UI_TextBox();
            Tx_CantAc = new UI_TextBox();
            Tx_NumAc1 = new UI_TextBox();
            Tx_NumAc2 = new UI_TextBox();
            Tx_NCpago = new UI_TextBox();
            Tx_NCuota = new UI_TextBox();
            Tx_NroPre = new UI_TextBox();
            Tx_FecRee = new UI_TextBox();
            Tx_Observ = new UI_TextBox();
            Lt_Final = new UI_ListBox();
            Tx_MtoFob = new UI_TextBox();
            Tx_MtoFle = new UI_TextBox();
            Tx_MtoSeg = new UI_TextBox();
            Pn_ValCif = new UI_TextBox();
            Tx_IntPla = new UI_TextBox();
            Tx_GasBan = new UI_TextBox();
            Pn_MtoTot = new UI_TextBox();
            Pn_CifDol = new UI_TextBox();
            Pn_TotDol = new UI_TextBox();
            Tx_TipCam = new UI_TextBox();
            Pn_TCDol = new UI_TextBox();
            Tx_Paridad = new UI_TextBox();
            Lb_NemTC = new UI_Label();
            Bot_OkDec  = new UI_Button();
            Bot_NoDec  = new UI_Button();
            Bot_OkFinal= new UI_Button();
            Bot_NoFinal= new UI_Button();
            Bot_Acepta = new UI_Button();
            Bot_Cancel = new UI_Button();

            PopUps = new List<UI_Message>();
        }

    }
}
