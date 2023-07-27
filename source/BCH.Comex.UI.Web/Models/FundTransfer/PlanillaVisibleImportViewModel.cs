using System.Collections.Generic;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class PlanillaVisibleImportViewModel
    {
        public string FormularioQueAbrir { get; set; }
        public string Tx_Observ { set; get; }
        public string Tx_NroCon { set; get; }
        public string Tx_FecVen { set; get; }
        public string Tx_FecCond { set; get; }
        public string Tx_Paridad { set; get; }
        public bool Tx_Paridad_Enabled { get; set; }
        public string Tx_TipCam { set; get; }
        public string Tx_MtoSeg { set; get; }
        public string Tx_MtoFle { set; get; }
        public string Tx_MtoFob { set; get; }
        public string Tx_FecRee { set; get; }
        public string Tx_NroPre { set; get; }
        public string Tx_NCuota { set; get; }
        public string Tx_NCpago { set; get; }
        public string Tx_NumAc2 { set; get; }
        public string Tx_CantAc { set; get; }
        public string Tx_NumAc1 { set; get; }
        public string Tx_DocExt { set; get; }
        public string Tx_DocChi { set; get; }
        public string Tx_FecDeb { set; get; }
        public string Tx_FecDec { set; get; }
        public string Tx_CodPag { set; get; }
        public string Tx_NumDec { set; get; }

        //Panels
        public string Pn_TCDol { set; get; }
        public string Pn_ValCif { set; get; }
        public string Pn_CifDol { set; get; }
        public string Pn_TotDol { set; get; }
        public string Pn_MtoTot { set; get; }
        public string Pn_RutImp { set; get; }
        public string Pn_Import { set; get; }
        public string Pn_NroPre { set; get; }
        public string Pn_CodMon { set; get; }
                      
        public List<SelectListItem> Lt_Final { set; get; }
        public List<SelectListItem> Cb_Pbc { set; get; }
        public List<SelectListItem> Cb_PPago { set; get; }
        public List<SelectListItem> Cb_Moneda { set; get; }

        public bool Bot_OkFinal { set; get; }
        public bool Bot_NoFinal { set; get; }
        public bool Bot_OkDec { set; get; }
        public bool Bot_NoDec { set; get; }
        public bool Bot_Clientes { set; get; }
        public bool Bot_Acepta { set; get; }
        public bool Bot_Cancel { set; get; }

        public IEnumerable<string> selectedLtFinal { get; set; }
        public int idCbPbc { set; get; }
        public int idCbPPago { set; get; }
        public int idCbMoneda { set; get; }

        public int indexFinal { set; get; }
        public int indexCbPbc { set; get; }
        public int indexCbPPago { set; get; }
        public int indexCbMoneda { set; get; }

        public bool Ch_PlanRee { set; get; }
        public bool Ch_ZonFra { set; get; }
        public bool Ch_ClauRo { set; get; }
        public bool Ch_Endoso { set; get; }
        public bool Ch_CPagos { set; get; }
        public bool Ch_Acuerdo { set; get; }
        public bool Ch_ConvCre { set; get; }
        public bool Ch_Transf { set; get; }       
        
        public List<string> MensajesDeError { set; get; }

        public PlanillaVisibleImportViewModel()
        {
            this.indexFinal = -1;
        }
    }
}
