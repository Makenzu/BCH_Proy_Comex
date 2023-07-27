using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.Devengo
{
    public class T_INFORME
    {
        public IList<string> periodos { get; set; }
        public IList<int> dias { get; set; }
        public bool esCartera { get; set; }
        public int tipoConsulta { get; set; }
        public int periodoSeleccionado { get; set; }
        public int diaSeleccionado { get; set; }
        public string txtRut { get; set; }


        public IList<T_CDR_Cartera> CDR_Cartera ;
        public IList<T_CDR_Deveng> CDR_Deveng ;

        public T_INFORME()
        {
            periodos = new List<string>();
            dias = new List<int>();
            esCartera = true;
            tipoConsulta = 1;
            CDR_Cartera = new List<T_CDR_Cartera>();
            CDR_Deveng = new List<T_CDR_Deveng>();
        }

        public T_INFORME(bool esCartera)
        {
            periodos = new List<string>();
            dias = new List<int>();
            this.esCartera = esCartera;
            tipoConsulta = esCartera ? 1 : 2;
            CDR_Cartera = new List<T_CDR_Cartera>();
            CDR_Deveng = new List<T_CDR_Deveng>();
        }

    }

     public class T_CDR_Cartera
      {
         //public string CrdTipoReg { get; set; }
          public Int64 CrdCpoRutDdorDir { get; set; }
          public string CrdDvRutDdorDir { get; set; }
          public int CrdOfiFicCont { get; set; }
          public int CrdTo { get; set; }
          public int CrdPppEmb { get; set; }
          public int CrdNumDoc { get; set; }
          public int CrdSituaCred { get; set; }
          public int CrdActivEconDest { get; set; }
          public int CrdTipoGarantia { get; set; }
          public int CrdClasRgoCred { get; set; }
          public int CrdOperRen { get; set; }
          public int CrdFecOtorCre { get; set; }
          public int CrdFecAprobLinea { get; set; }
          public int CrdFecExtinCre { get; set; }
          public int CrdFecExtinLinea { get; set; }
          public int CrdFecProxCambioTasa { get; set; }
          public int CrdFecUltRenov { get; set; }
          public int CrdFecPasoVenc { get; set; }
          public int CrdFecPasoEjec { get; set; }
          public int CrdFecDeterioro { get; set; }
          public int CrdFecProxVencFtro { get; set; }
          public int CrdFecUltPagCap { get; set; }
          public int CrdFecUltPagInt { get; set; }
          public int CrdFecRezagoMasAntig { get; set; }
          public int CrdFecImpagaMasAntig { get; set; }
          public int CrdFecUltTasa { get; set; }
          public int CrdFecPenultTasa { get; set; }
          public string CrdMo { get; set; }
          public string CrdMc { get; set; }
          public string CrdMcInt { get; set; }
          public float CrdMtoOrigMo { get; set; }
          public float CrdMtoRenMo { get; set; }
          public float CrdCapitProxVencMo { get; set; }
          public float CrdIntProxVencMo { get; set; }
          public float CrdVigMo { get; set; }
          public float CrdMoraH29Mo { get; set; }
          public float CrdMoraM30H59Mo { get; set; }
          public float CrdMoraM60H89Mo { get; set; }
          public float CrdMoraD90Mo { get; set; }
          public float CrdIxcVencMo { get; set; }
          public float CrdPreJudMo { get; set; }
          public float CrdEjecMo { get; set; }
          public double CrdVigMn { get; set; }
          public double CrdIxcVencMn { get; set; }
          public double CrdPreJudMn { get; set; }
          public double CrdEjecMn { get; set; }
          public double CrdVigMe { get; set; }
          public float CrdIntVencMe { get; set; }
          public float CrdPreJudMe { get; set; }
          public float CrdEjecMe { get; set; }
          public float CrdInVigMo { get; set; }
          public float CrdInMoraH29Mo { get; set; }
          public float CrdInMoraM30H59Mo { get; set; }
          public float CrdInMoraM60H89Mo { get; set; }
          public float CrdInMoraD90Mo { get; set; }
          public float CrdIpMoraH29Mo { get; set; }
          public float CrdIpMoraM30H59Mo { get; set; }
          public float CrdIpMoraM60H89Mo { get; set; }
          public float CrdIpMoraD90Mo { get; set; }
          public float CrdIpIxcVencMo { get; set; }
          public float CrdIpPreJudicMo { get; set; }
          public float CrdIpEjecMo { get; set; }
          public float CrdIsNokIxcVencMo { get; set; }
          public float CrdIsNokPreJudicMo { get; set; }
          public float CrdIsNokEjecMo { get; set; }
          public float CrdIsNokIxcVencMc { get; set; }
          public int CrdPppOrig { get; set; }
          public int CrdPppRdual { get; set; }
          public int CrdCuotCapitXVenc { get; set; }
          public int CrdCuotIntXVenc { get; set; }
          public int CrdCuotAtra { get; set; }
          public int CrdProxCuot { get; set; }
          public int CrdTipInt { get; set; }
          public int CrdMtdoCalcInt { get; set; }
          public int CrdExpreTasa { get; set; }
          public int CrdTipBase { get; set; }
          public float CrdPtoSobBase { get; set; }
          public float CrdTasaRealAplic { get; set; }
          public float CrdTasaEquiAa { get; set; }
          public int CrdCodVariTasa { get; set; }
          public int CrdTipDocSust { get; set; }
          public int CrdOfiReaCont { get; set; }
          public int CrdTipoCre { get; set; }
          public int CrdEstCre { get; set; }
          public int CrdCausaExtincionCre { get; set; }
          public int CrdPzoContbCre { get; set; }
          public int CrdIndDivProrrogados { get; set; }
          public float CrdParidCapit { get; set; }
          public float CrdMtoVencPagMm { get; set; }
          public float CrdMtoVencRenovMm { get; set; }
          public float CrdUltMtoPagIntMo { get; set; }
          public float CrdPromCapVigMo { get; set; }
          public float CrdPromCapMorMo { get; set; }
          public float CrdPromCapVenMo { get; set; }
          public float CrdPromIntnMorvenMo { get; set; }
          public float CrdIntnorRecibMesMc { get; set; }
          public float CrdIntpenRecibMesMc { get; set; }
          public int CrdReaRecibMes { get; set; }
      }
     public class T_CDR_Deveng
      {
          //public string DevFechaInt { get; set; }
          //public string DevTipoReg { get; set; }
          public int DevCpoRutDdorDir { get; set; }
          public string DevDvRutDdorDir { get; set; }
          public int DevOfiFicCont { get; set; }
          public int DevTo { get; set; }
          public int DevPppEmb { get; set; }
          public int DevNumDoc { get; set; }
          public float DevIntVigMo { get; set; }
          public float DevIntMoraH29Mo { get; set; }
          public float DevIntMoraM30H59Mo { get; set; }
          public float DevIntMoraM60H89Mo { get; set; }
          public float DevIntMoraD90Mo { get; set; }
          public float DevIntDivProrrMo { get; set; }
          public float DevIntDifPcioMo { get; set; }
          public float DevIntVigMc { get; set; }
          public float DevIntMoraH29Mc { get; set; }
          public float DevIntMoraM30H59Mc { get; set; }
          public float DevIntMoraM60H89Mc { get; set; }
          public float DevIntMoraD90Mc { get; set; }
          public float DevIntDivProrrMc { get; set; }
          public float DevIntDifPcioMc { get; set; }
          public float DevIsCrVigMo { get; set; }
          public float DevIsCrMoraH29Mo { get; set; }
          public float DevIsCrMoraM30H59Mo { get; set; }
          public float DevIsCrMoraM60H89Mo { get; set; }
          public float DevIsCrMoraD90Mo { get; set; }
          public float DevIsCrDivProrrMo { get; set; }
          public float DevIsCrVigMc { get; set; }
          public float DevIsCrMoraH29Mc { get; set; }
          public float DevIsCrMoraM30H59Mc { get; set; }
          public float DevIsCrMoraM60H89Mc { get; set; }
          public float DevIsCrMoraD90Mc { get; set; }
          public float DevIsCrDivProrrMc { get; set; }
          public float DevIsnCartDetVigMc { get; set; }
          public float DevIsnCartDetMrH29VigMc { get; set; }
          public float DevIsnCartDetMrM30H59Mc { get; set; }
          public float DevIsnCartDetMrM60H89Mc { get; set; }
          public float DevIsnCartDetMrD90Mc { get; set; }
          public float DevIsCvDivProrrMo { get; set; }
          public float DevIspCartDetVigMc { get; set; }
          public float DevIspCartDetMrH29Mc { get; set; }
          public float DevIspCartDetMrM30H59Mc { get; set; }
          public float DevIspCartDetMrM60H89Mc { get; set; }
          public float DevIspCartDetMrD90Mc { get; set; }
          public float DevIsCvDivProrrMc { get; set; }
          public float DevIpMoraH29Mc { get; set; }
          public float DevIpMoraM30H59Mc { get; set; }
          public float DevIpMoraM60H89Mc { get; set; }
          public float DevIpMoraD90Mc { get; set; }
          public float DevIpVencCobMc { get; set; }
          public float DevIpPreJudicMc { get; set; }
          public float DevIpEjecMc { get; set; }
          public int DevReajVigMn { get; set; }
          public int DevReajMoraH29Mn { get; set; }
          public int DevReajMoraM30H59Mn { get; set; }
          public int DevReajMoraM60H89Mn { get; set; }
          public int DevReajMoraD90Mn { get; set; }
          public int DevReajDivProrrMn { get; set; }
          public int DevReajDifPcioMn { get; set; }
          public int DevRsCartDetVigMn { get; set; }
          public int DevRsCartDetMoraH29Mn { get; set; }
          public int DevRsCartDetM30H59Mn { get; set; }
          public int DevRsCartDetM60H89Mn { get; set; }
          public int DevRsCartDetD90Mn { get; set; }
          public int DevRsDivProrrMo { get; set; }
          public int DevRsCmVigMo { get; set; }
          public int DevRsCmMoraH29Mo { get; set; }
          public int DevRsCmMoraM30H59Mo { get; set; }
          public int DevRsCmMoraM60H89Mo { get; set; }
          public int DevRsCmMoraD90Mo { get; set; }
          public int DevRsCmIntCobVenc { get; set; }
          public int DevRsCmPreJudicMo { get; set; }
          public int DevRsCmEnEjecMo { get; set; }
          public int DevCodMdaContabCap { get; set; }
          public int DevPppContabCre { get; set; }
      }

     //public class T_CDR_Cartera
     //{
     //    //public string CrdTipoReg { get; set; }
     //    public string CrdCpoRutDdorDir { get; set; }
     //    public string CrdDvRutDdorDir { get; set; }
     //    public string CrdOfiFicCont { get; set; }
     //    public string CrdTo { get; set; }
     //    public string CrdPppEmb { get; set; }
     //    public string CrdNumDoc { get; set; }
     //    public string CrdSituaCred { get; set; }
     //    public string CrdActivEconDest { get; set; }
     //    public string CrdTipoGarantia { get; set; }
     //    public string CrdClasRgoCred { get; set; }
     //    public string CrdOperRen { get; set; }
     //    public string CrdFecOtorCre { get; set; }
     //    public string CrdFecAprobLinea { get; set; }
     //    public string CrdFecExtinCre { get; set; }
     //    public string CrdFecExtinLinea { get; set; }
     //    public string CrdFecProxCambioTasa { get; set; }
     //    public string CrdFecUltRenov { get; set; }
     //    public string CrdFecPasoVenc { get; set; }
     //    public string CrdFecPasoEjec { get; set; }
     //    public string CrdFecDeterioro { get; set; }
     //    public string CrdFecProxVencFtro { get; set; }
     //    public string CrdFecUltPagCap { get; set; }
     //    public string CrdFecUltPagInt { get; set; }
     //    public string CrdFecRezagoMasAntig { get; set; }
     //    public string CrdFecImpagaMasAntig { get; set; }
     //    public string CrdFecUltTasa { get; set; }
     //    public string CrdFecPenultTasa { get; set; }
     //    public string CrdMo { get; set; }
     //    public string CrdMc { get; set; }
     //    public string CrdMcInt { get; set; }
     //    public string CrdMtoOrigMo { get; set; }
     //    public string CrdMtoRenMo { get; set; }
     //    public string CrdCapitProxVencMo { get; set; }
     //    public string CrdIntProxVencMo { get; set; }
     //    public string CrdVigMo { get; set; }
     //    public string CrdMoraH29Mo { get; set; }
     //    public string CrdMoraM30H59Mo { get; set; }
     //    public string CrdMoraM60H89Mo { get; set; }
     //    public string CrdMoraD90Mo { get; set; }
     //    public string CrdIxcVencMo { get; set; }
     //    public string CrdPreJudMo { get; set; }
     //    public string CrdEjecMo { get; set; }
     //    public string CrdVigMn { get; set; }
     //    public string CrdIxcVencMn { get; set; }
     //    public string CrdPreJudMn { get; set; }
     //    public string CrdEjecMn { get; set; }
     //    public string CrdVigMe { get; set; }
     //    public string CrdIntVencMe { get; set; }
     //    public string CrdPreJudMe { get; set; }
     //    public string CrdEjecMe { get; set; }
     //    public string CrdInVigMo { get; set; }
     //    public string CrdInMoraH29Mo { get; set; }
     //    public string CrdInMoraM30H59Mo { get; set; }
     //    public string CrdInMoraM60H89Mo { get; set; }
     //    public string CrdInMoraD90Mo { get; set; }
     //    public string CrdIpMoraH29Mo { get; set; }
     //    public string CrdIpMoraM30H59Mo { get; set; }
     //    public string CrdIpMoraM60H89Mo { get; set; }
     //    public string CrdIpMoraD90Mo { get; set; }
     //    public string CrdIpIxcVencMo { get; set; }
     //    public string CrdIpPreJudicMo { get; set; }
     //    public string CrdIpEjecMo { get; set; }
     //    public string CrdIsNokIxcVencMo { get; set; }
     //    public string CrdIsNokPreJudicMo { get; set; }
     //    public string CrdIsNokEjecMo { get; set; }
     //    public string CrdIsNokIxcVencMc { get; set; }
     //    public string CrdPppOrig { get; set; }
     //    public string CrdPppRdual { get; set; }
     //    public string CrdCuotCapitXVenc { get; set; }
     //    public string CrdCuotIntXVenc { get; set; }
     //    public string CrdCuotAtra { get; set; }
     //    public string CrdProxCuot { get; set; }
     //    public string CrdTipInt { get; set; }
     //    public string CrdMtdoCalcInt { get; set; }
     //    public string CrdExpreTasa { get; set; }
     //    public string CrdTipBase { get; set; }
     //    public string CrdPtoSobBase { get; set; }
     //    public string CrdTasaRealAplic { get; set; }
     //    public string CrdTasaEquiAa { get; set; }
     //    public string CrdCodVariTasa { get; set; }
     //    public string CrdTipDocSust { get; set; }
     //    public string CrdOfiReaCont { get; set; }
     //    public string CrdTipoCre { get; set; }
     //    public string CrdEstCre { get; set; }
     //    public string CrdCausaExtincionCre { get; set; }
     //    public string CrdPzoContbCre { get; set; }
     //    public string CrdIndDivProrrogados { get; set; }
     //    public string CrdParidCapit { get; set; }
     //    public string CrdMtoVencPagMm { get; set; }
     //    public string CrdMtoVencRenovMm { get; set; }
     //    public string CrdUltMtoPagIntMo { get; set; }
     //    public string CrdPromCapVigMo { get; set; }
     //    public string CrdPromCapMorMo { get; set; }
     //    public string CrdPromCapVenMo { get; set; }
     //    public string CrdPromIntnMorvenMo { get; set; }
     //    public string CrdIntnorRecibMesMc { get; set; }
     //    public string CrdIntpenRecibMesMc { get; set; }
     //    public string CrdReaRecibMes { get; set; }
     //}
     //public class T_CDR_Deveng
     //{
     //    public string DevFechaInt { get; set; }
     //    public string DevTipoReg { get; set; }
     //    public string DevCpoRutDdorDir { get; set; }
     //    public string DevDvRutDdorDir { get; set; }
     //    public string DevOfiFicCont { get; set; }
     //    public string DevTo { get; set; }
     //    public string DevPppEmb { get; set; }
     //    public string DevNumDoc { get; set; }
     //    public string DevIntVigMo { get; set; }
     //    public string DevIntMoraH29Mo { get; set; }
     //    public string DevIntMoraM30H59Mo { get; set; }
     //    public string DevIntMoraM60H89Mo { get; set; }
     //    public string DevIntMoraD90Mo { get; set; }
     //    public string DevIntDivProrrMo { get; set; }
     //    public string DevIntDifPcioMo { get; set; }
     //    public string DevIntVigMc { get; set; }
     //    public string DevIntMoraH29Mc { get; set; }
     //    public string DevIntMoraM30H59Mc { get; set; }
     //    public string DevIntMoraM60H89Mc { get; set; }
     //    public string DevIntMoraD90Mc { get; set; }
     //    public string DevIntDivProrrMc { get; set; }
     //    public string DevIntDifPcioMc { get; set; }
     //    public string DevIsCrVigMo { get; set; }
     //    public string DevIsCrMoraH29Mo { get; set; }
     //    public string DevIsCrMoraM30H59Mo { get; set; }
     //    public string DevIsCrMoraM60H89Mo { get; set; }
     //    public string DevIsCrMoraD90Mo { get; set; }
     //    public string DevIsCrDivProrrMo { get; set; }
     //    public string DevIsCrVigMc { get; set; }
     //    public string DevIsCrMoraH29Mc { get; set; }
     //    public string DevIsCrMoraM30H59Mc { get; set; }
     //    public string DevIsCrMoraM60H89Mc { get; set; }
     //    public string DevIsCrMoraD90Mc { get; set; }
     //    public string DevIsCrDivProrrMc { get; set; }
     //    public string DevIsnCartDetVigMc { get; set; }
     //    public string DevIsnCartDetMrH29VigMc { get; set; }
     //    public string DevIsnCartDetMrM30H59Mc { get; set; }
     //    public string DevIsnCartDetMrM60H89Mc { get; set; }
     //    public string DevIsnCartDetMrD90Mc { get; set; }
     //    public string DevIsCvDivProrrMo { get; set; }
     //    public string DevIspCartDetVigMc { get; set; }
     //    public string DevIspCartDetMrH29Mc { get; set; }
     //    public string DevIspCartDetMrM30H59Mc { get; set; }
     //    public string DevIspCartDetMrM60H89Mc { get; set; }
     //    public string DevIspCartDetMrD90Mc { get; set; }
     //    public string DevIsCvDivProrrMc { get; set; }
     //    public string DevIpMoraH29Mc { get; set; }
     //    public string DevIpMoraM30H59Mc { get; set; }
     //    public string DevIpMoraM60H89Mc { get; set; }
     //    public string DevIpMoraD90Mc { get; set; }
     //    public string DevIpVencCobMc { get; set; }
     //    public string DevIpPreJudicMc { get; set; }
     //    public string DevIpEjecMc { get; set; }
     //    public string DevReajVigMn { get; set; }
     //    public string DevReajMoraH29Mn { get; set; }
     //    public string DevReajMoraM30H59Mn { get; set; }
     //    public string DevReajMoraM60H89Mn { get; set; }
     //    public string DevReajMoraD90Mn { get; set; }
     //    public string DevReajDivProrrMn { get; set; }
     //    public string DevReajDifPcioMn { get; set; }
     //    public string DevRsCartDetVigMn { get; set; }
     //    public string DevRsCartDetMoraH29Mn { get; set; }
     //    public string DevRsCartDetM30H59Mn { get; set; }
     //    public string DevRsCartDetM60H89Mn { get; set; }
     //    public string DevRsCartDetD90Mn { get; set; }
     //    public string DevRsDivProrrMo { get; set; }
     //    public string DevRsCmVigMo { get; set; }
     //    public string DevRsCmMoraH29Mo { get; set; }
     //    public string DevRsCmMoraM30H59Mo { get; set; }
     //    public string DevRsCmMoraM60H89Mo { get; set; }
     //    public string DevRsCmMoraD90Mo { get; set; }
     //    public string DevRsCmIntCobVenc { get; set; }
     //    public string DevRsCmPreJudicMo { get; set; }
     //    public string DevRsCmEnEjecMo { get; set; }
     //    public string DevCodMdaContabCap { get; set; }
     //    public string DevPppContabCre { get; set; }
     //}

}
