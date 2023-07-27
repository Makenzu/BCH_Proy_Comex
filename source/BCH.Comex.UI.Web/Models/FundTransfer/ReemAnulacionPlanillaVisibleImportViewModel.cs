using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using System;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class ReemAnulacionPlanillaVisibleImportViewModel : FundTransferViewModel
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

        public ReemAnulacionPlanillaVisibleImportViewModel()
        {
        }

        public ReemAnulacionPlanillaVisibleImportViewModel(UI_Frm_Rem_PVI frm)
        {

            Tx_NumDec = frm.Tx_NumDec;
            Tx_FecDec = frm.Tx_FecDec;
            Tx_CodPag = frm.Tx_CodPag;
            Pn_NroPre = frm.Pn_NroPre;
            Cb_Pbc    = frm.Cb_Pbc;
            Pn_Import = frm.Pn_Import;
            Pn_RutImp = frm.Pn_RutImp;
            Tx_NroCon = frm.Tx_NroCon;
            Tx_FecCon = frm.Tx_FecCon;
            Tx_FecVen = frm.Tx_FecVen;
            Tx_CodPai = frm.Tx_CodPai;
            Pn_PPago  = frm.Pn_PPago;
            Pn_CodMon = frm.Pn_CodMon;
            Pn_Moneda = frm.Pn_Moneda;
            Ch_ConvCre = frm.Ch_ConvCre;
            Ch_Acuerdo = frm.Ch_Acuerdo;
            Ch_CPagos = frm.Ch_CPagos;
            Tx_FecDeb = frm.Tx_FecDeb;
            Tx_DocChi = frm.Tx_DocChi;
            Tx_DocExt = frm.Tx_DocExt;
            Tx_CantAc = frm.Tx_CantAc;
            Tx_NumAc1 = frm.Tx_NumAc1;
            Tx_NumAc2 = frm.Tx_NumAc2;
            Tx_NCpago = frm.Tx_NCpago;
            Tx_NCuota = frm.Tx_NCuota;
            Tx_NroPre = frm.Tx_NroPre;
            Tx_FecRee = frm.Tx_FecRee;
            Tx_Observ = frm.Tx_Observ;
            Lt_Final  = frm.Lt_Final;
            Tx_MtoFle = frm.Tx_MtoFle;
            Tx_MtoSeg = frm.Tx_MtoSeg;
            Pn_ValCif = frm.Pn_ValCif;
            Tx_IntPla = frm.Tx_IntPla;
            Tx_GasBan = frm.Tx_GasBan;
            Pn_MtoTot = frm.Pn_MtoTot;
            Pn_CifDol = frm.Pn_CifDol;
            Pn_TotDol = frm.Pn_TotDol;
            Tx_TipCam = frm.Tx_TipCam;
            Lb_NemTC  = frm.Lb_NemTC;
            Pn_TCDol  = frm.Pn_TCDol;
            Tx_Paridad = frm.Tx_Paridad;

            Bot_OkDec = frm.Bot_OkDec;
            Bot_NoDec = frm.Bot_NoDec;
            Bot_OkFinal = frm.Bot_OkFinal;
            Bot_NoFinal = frm.Bot_NoFinal;
            Bot_Acepta = frm.Bot_Acepta;
            Bot_Cancel = frm.Bot_Cancel;


        }

        internal void Update(UI_Frm_Rem_PVI frm)
        {
            using (var trace = new Tracer("ReemAnulacionViewModel - Update"))
            {
                try
                {
                    Update(frm.Tx_NumDec, Tx_NumDec);
                    Update(frm.Tx_FecDec, Tx_FecDec);
                    Update(frm.Tx_CodPag, Tx_CodPag);
                    Update(frm.Pn_NroPre, Pn_NroPre);
                    Update(frm.Cb_Pbc, Cb_Pbc);
                    Update(frm.Pn_Import, Pn_Import);
                    Update(frm.Pn_RutImp, Pn_RutImp);
                    Update(frm.Tx_NroCon, Tx_NroCon);
                    Update(frm.Tx_FecCon, Tx_FecCon);
                    Update(frm.Tx_FecVen, Tx_FecVen);
                    Update(frm.Tx_CodPai, Tx_CodPai);
                    Update(frm.Pn_PPago, Pn_PPago);
                    Update(frm.Pn_CodMon, Pn_CodMon);
                    Update(frm.Pn_Moneda, Pn_Moneda);
                    Update(frm.Ch_ConvCre, Ch_ConvCre);
                    Update(frm.Ch_Acuerdo, Ch_Acuerdo);
                    Update(frm.Ch_CPagos, Ch_CPagos);
                    Update(frm.Tx_FecDeb, Tx_FecDeb);
                    Update(frm.Tx_DocChi, Tx_DocChi);
                    Update(frm.Tx_DocExt, Tx_DocExt);
                    Update(frm.Tx_CantAc, Tx_CantAc);
                    Update(frm.Tx_NumAc1, Tx_NumAc1);
                    Update(frm.Tx_NumAc2, Tx_NumAc2);
                    Update(frm.Tx_NCpago, Tx_NCpago);
                    Update(frm.Tx_NCuota, Tx_NCuota);
                    Update(frm.Tx_NroPre, Tx_NroPre);
                    Update(frm.Tx_FecRee, Tx_FecRee);
                    Update(frm.Tx_Observ, Tx_Observ);
                    Update(frm.Lt_Final, Lt_Final);
                    Update(frm.Tx_MtoFle, Tx_MtoFle);
                    Update(frm.Tx_MtoSeg, Tx_MtoSeg);
                    Update(frm.Pn_ValCif, Pn_ValCif);
                    Update(frm.Tx_IntPla, Tx_IntPla);
                    Update(frm.Tx_GasBan, Tx_GasBan);
                    Update(frm.Pn_MtoTot, Pn_MtoTot);
                    Update(frm.Pn_CifDol, Pn_CifDol);
                    Update(frm.Pn_TotDol, Pn_TotDol);
                    Update(frm.Tx_TipCam, Tx_TipCam);
                    Update(frm.Pn_TCDol, Pn_TCDol);
                    Update(frm.Tx_Paridad, Tx_Paridad);

                }
                catch (Exception ex)
                {
                    trace.TraceException("Alerta", ex);
                    throw;
                }
            }
        }
    }
}