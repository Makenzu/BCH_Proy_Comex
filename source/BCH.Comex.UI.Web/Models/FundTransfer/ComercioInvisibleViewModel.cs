using BCH.Comex.Common.UI_Modulos;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class ComercioInvisibleViewModel
    {
        public int indexPais { set; get; }
        public int indexMoneda { set; get; }
        public int indexSecEcBen { set; get; }
        public int indexSecEcIn { set; get; }
        public int indexMonDes { set; get; }
        public int indexTipAut { set; get; }
        public int indexInsUt { set; get; }
        public int indexArCon { set; get; }
        public int indexDivisa { set; get; }
        public int indexLt_Tcp { set; get; }
        public int indexOperacion { set; get; }

        public List<SelectListItem> CB_Pais { set; get; }
        //public List<SelectListItem> CB_Moneda { set; get; }
        public List<SelectListItem> Cb_SecEcBen { set; get; }
        public List<SelectListItem> Cb_SecEcIn { set; get; }
        public List<SelectListItem> Cb_MonDes { set; get; }
        public List<SelectListItem> Cb_TipAut { set; get; }
        public List<SelectListItem> Cb_InsUt { set; get; }
        public List<SelectListItem> Cb_ArCon { set; get; }
        //public List<SelectListItem> Cb_Divisa { set; get; }
        public List<SelectListItem> Lt_Tcp { set; get; }
        public List<Lt_Operacion_Model> Lt_Operacion { set; get; }

        public UI_Combo CB_Moneda { set; get; }
        public UI_Combo Cb_Divisa { set; get; }

        public string[] Tx_MtoCV { set; get; }
        public bool[] BTx_MtoCV { set; get; }
        public string Tx_CanAc { set; get; }

        public string Tx_NumCon { set; get; }
        public string Tx_FecSus { set; get; }
        public string Tx_FecVen { set; get; }
        public string Tx_ParTip { set; get; }
        public string Tx_NumIns { set; get; }
        public string Tx_FecIns { set; get; }
        public string Tx_NomFin { set; get; }
        public string Tx_FecVC { set; get; }
        public string Tx_Fecha { set; get; }
        public string Tx_Mto { set; get; }
        public string Tx_PrcPar { set; get; }
        public string Tx_FecDeb { set; get; }
        public string Tx_DocNac { set; get; }
        public string Tx_DocExt { set; get; }

        public string Tx_NroAut { set; get; }
        public string Tx_FecAut { set; get; }
        public string Tx_SucBcch { set; get; }

        public string Tx_NroDec { set; get; }
        public string Tx_FecDec { set; get; }
        public string Tx_CodAdn { set; get; }
        public string Tx_ER { set; get; }
        
           
        public bool Ch_Convenio { set; get; }
        public bool ch_AfDer { set; get; }
        public bool ch_ZoFra { set; get; }


        public string Tx_FecPre {set;get;}
        public string Tx_NumPre {set;get;}
        public string Tx_CodIns { set; get; }

        public bool Fr_Ope_V { set; get; }
        public bool Fr_Ope_D_V { set; get; }
        public bool Fr_Ofi_V { set; get; }
        public bool Fr_Sec_V { set; get; }
        public bool Fr_Convenio_V { set; get; }
        public bool Fr_Autori_V { set; get; }
        public bool Fr_Declaracion_V { set; get; }
        public bool Fr_OpRe_V { set; get; }

        public bool Fr_Ope_E { set; get; }
        public bool Fr_Ope_D_E { set; get; }
        public bool Fr_Ofi_E { set; get; }
        public bool Fr_Sec_E { set; get; }
        public bool Fr_Convenio_E { set; get; }
        public bool Fr_Autori_E { set; get; }
        public bool Fr_Declaracion_E { set; get; }
        public bool Fr_OpRe_E { set; get; }


        public List<string> Errores { set; get; }



        public ComercioInvisibleViewModel()
        {
            Errores = new List<string>();
            Tx_MtoCV = new string[4] { String.Empty, String.Empty, String.Empty, String.Empty};
            Tx_ER = String.Empty;
            BTx_MtoCV = new bool[4] { true,true,true,true};
            CB_Pais = new List<SelectListItem>();
            CB_Moneda = new UI_Combo();
            Cb_SecEcBen = new List<SelectListItem>();
            Cb_SecEcIn = new List<SelectListItem>();
            Cb_MonDes = new List<SelectListItem>();
            Cb_TipAut = new List<SelectListItem>();
            Cb_InsUt = new List<SelectListItem>();
            Cb_ArCon = new List<SelectListItem>();
            Cb_Divisa = new UI_Combo();
            Lt_Tcp = new List<SelectListItem>();
            Lt_Operacion = new List<Lt_Operacion_Model>();
            Ch_Convenio = false;
            ch_AfDer = false;
            ch_ZoFra = false;
        }
    }
}