
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes
{
    public class ImprimirPlanilla:Planilla
    {
        public string NumPre { set; get; }//413960
        public string V_PlAnu_Codigo { set; get; }//205000
        public string FecVen { set; get; }//20/08/2015
        public string NomPla { set; get; }
        public string V_PlAnu_CodBch { set; get; }//25
        public string V_PlAnu_NomImp { set; get; }//MICROSOFT CHILE S.A.
        public string V_PlAnu_RutImp { set; get; }//96.633.760-5
        public string V_PlAnu_NomPai { set; get; }//ESTADOS UNIDOS
        public string V_PlAnu_CodPPa { set; get; }//225
        public  string V_PlAnu_NomMon { set; get; }//Dolar Usa
        public string V_PlAnu_CodMPa { set; get; }//013
        //if V_PlAnu_NumIdi != '000000'
        public string V_PlAnu_NumIdi { set; get; }
        public string V_PlAnu_FecIdi { set; get; }
        public string V_PlAnu_CodPla { set; get; }
        //endif
        public string V_PlAnu_CodPag { set; get; }//32
        //************ no aparecen en el reporte
        public string V_PlAnu_NumCon { set; get; }
        public string V_PlAnu_FecCon { set; get; }
        public string V_PlAnu_FecVto { set; get; }
        //***********

        public string V_PlAnu_MtoFob { set; get; }//154.000,00
        public bool V_PlAnu_HayAcu { set; get; }
        public string V_PlAnu_NumAcu { set; get; }
        public string V_PlAnu_Acuer1 { set; get; }
        public string V_PlAnu_FecDeb { set; get; }
        public string V_PlAnu_DocChi { set; get; }
        public string V_PlAnu_DocExt { set; get; }
        public string V_PlAnu_MtoFle { set; get; }
        public string V_PlAnu_MtoSeg { set; get; }
        public string V_PlAnu_MtoCif { set; get; }
        public string V_PlAnu_NumCua { set; get; }
        public string V_PlAnu_numcuo { set; get; }
        public string V_PlAnu_MtoInt { set; get; }
        public string V_PlAnu_MtoGas { set; get; }
        public string V_PlAnu_FecAnu { set; get; }
        public string V_PlAnu_ParAnu { set; get; }
        public string V_PlAnu_MtoTot { set; get; }
        public string V_PlAnu_CifDol { set; get; }
        public string V_PlAnu_TotAnu { set; get; }
        public string V_PlAnu_TotDol { set; get; }
        public string V_PlAnu_TipCamo { set; get; }
        public string V_PlAnu_ParPag { set; get; }
        public string V_PlAnu_ObsDec { set; get; }
        public string V_PlAnu_ObsPar { set; get; }
        public  string V_PlAnu_ObsMer { set; get; }
        public string V_PlAnu_observ { set; get; }
        public string V_PlAnu_ObsCob { set; get; }
        
    }
}
