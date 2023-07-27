using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Devengo;
using BCH.Comex.Data.DAL.Cext01;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;


namespace BCH.Comex.Core.BL.XGCN.Forms
{
    public static class Frminforme
    {
        public static string NombreWorksheetCartera = "CartetaCDR";
        public static string NombreWorksheetDevengo = "DevengoCDR";

        public static void Form_Load(T_INFORME infor, IList<UI_Message> listaMensajes, UnitOfWorkCext01 uow)
        {
            ObtienePeriodosCDR(infor, listaMensajes, uow);
        }

        private static void ObtienePeriodosCDR(T_INFORME infor, IList<UI_Message> listaMensajes, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer("Obtencion Parametros CDR"))
            {
                try
                {
                    //llamar al procedimiento pro_sce_int_cdr_MS
                    var res = uow.SceRepository.pro_sce_int_cdr_S01_MS(infor.tipoConsulta);

                    infor.periodos = res.ToList();
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, problemas al obtener la lista de periodos CDR", ex);
                    listaMensajes.Add(new UI_Message() { Text = "Alerta, problemas al obtener la lista de periodos CDR", Type = TipoMensaje.Error });
                }

            }
        }

        public static void ObtieneDiasDisponibles(T_INFORME infor, IList<UI_Message> listaMensajes, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer("Obtencion dias posibles CDR"))
            {
                try
                {
                    int anno = infor.periodoSeleccionado / 100;
                    int mes = infor.periodoSeleccionado % 100;
                    var res = uow.SceRepository.pro_sce_int_cdr_S02_MS(infor.tipoConsulta, anno, mes);

                    infor.dias = new List<int>();
                    foreach (var r in res)
                    {
                        infor.dias.Add(r ?? 0);
                    }
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, problemas al obtener dias disponibles", ex);
                    listaMensajes.Add(new UI_Message() { Text = "Alerta, problemas al obtener dias disponibles", Type = TipoMensaje.Error });
                }

            }
        }

        public static bool CmdBuscar_Click(T_INFORME infor, IList<UI_Message> listaMensajes, UnitOfWorkCext01 uow)
        {
            string PlazoTO = "";
            string operacion = "";
            string moneda = "9";
            int QRegistros = 0;
            string todos = "S";

            if (infor.periodoSeleccionado == -1)
            {
                listaMensajes.Add(new UI_Message() { Text = "Para consultar debe seleccionar Año / Mes", Type = TipoMensaje.Informacion, ControlName = "listaPeriodos_SelectedValue", AutoClose = true });
                return false;
            }

            if (infor.diaSeleccionado == -1 && infor.tipoConsulta != 2)
            {
                listaMensajes.Add(new UI_Message() { Text = "Para consultar debe seleccionar día", Type = TipoMensaje.Informacion, ControlName = "listaDias_SelectedValue", AutoClose = true });
                return false;
            }

            ////RUT Solo para uno

            int periodo = infor.periodoSeleccionado;
            string FInicio = string.Empty;
            string FTermino = string.Empty;
            int anno = infor.periodoSeleccionado / 100;
            int mes = infor.periodoSeleccionado % 100;
            string TipoInterfazCDR = "";

            if (infor.txtRut.Trim() != string.Empty)
            {
                todos = "N";
            }

            if (infor.diaSeleccionado == 0 || infor.tipoConsulta == 2)//mensual
            {
                TipoInterfazCDR = "M";
                DateTime fec = new DateTime(anno, mes, 1);
                FInicio = fec.ToShortDateString();
                FTermino = fec.AddMonths(1).AddDays(-1).ToShortDateString();
            }
            else //diaria
            {
                TipoInterfazCDR = "D";
                DateTime fec = new DateTime(anno, mes, infor.diaSeleccionado);
                FInicio = fec.ToShortDateString();
                FTermino = fec.ToShortDateString();
            }

            return BuscarCDR(periodo, FInicio, FTermino, infor.txtRut, operacion, moneda, PlazoTO, todos,
               infor.tipoConsulta, QRegistros, TipoInterfazCDR, infor, listaMensajes, uow);

        }

        public static bool BuscarCDR(int periodo, string InicioPeriodo, string TerminoPeriodo, string rut, string Operacion, string moneda, string PlazoTO,string todos,
            int TipoConsultaCDR, int CantidadRegistros, string TipoInterfazCartera, T_INFORME infor, IList<UI_Message> listaMensajes, UnitOfWorkCext01 uow)
        {
            bool BuscarCDR = false;

            using (Tracer tracer = new Tracer("Buscar interfaz CDR"))
            {
                try
                {
                    infor.CDR_Cartera = new List<T_CDR_Cartera>();
                    infor.CDR_Deveng = new List<T_CDR_Deveng>();
                    Char[] charQuitar = new Char[] { '0' };

                    if (infor.tipoConsulta == 1)
                    {
                        var resultadosCartera = uow.SceRepository.sce_dev_cdr(periodo, InicioPeriodo, TerminoPeriodo, rut, Operacion, moneda, PlazoTO, todos, TipoConsultaCDR, CantidadRegistros, TipoInterfazCartera);
                        
                        foreach (var resultado in resultadosCartera)
                        {
                            var nuevo = new T_CDR_Cartera()
                            {
                                //CrdTipoReg = resultado.CRD_TIPO_REG,
                                CrdCpoRutDdorDir = resultado.CRD_CPO_RUT_DDOR_DIR.ToLng(),
                                CrdDvRutDdorDir = resultado.CRD_DV_RUT_DDOR_DIR,
                                CrdOfiFicCont = int.Parse(resultado.CRD_OFI_FIC_CONT),
                                CrdTo = int.Parse(resultado.CRD_TO),
                                CrdPppEmb = int.Parse(resultado.CRD_PPP_EMB),
                                CrdNumDoc = int.Parse(resultado.CRD_NUM_DOC),
                                CrdSituaCred = int.Parse(resultado.CRD_SITUA_CRED),
                                CrdActivEconDest = int.Parse(resultado.CRD_ACTIV_ECON_DEST),
                                CrdTipoGarantia = int.Parse(resultado.CRD_TIPO_GARANTIA.Trim() == "" ? "0" : resultado.CRD_TIPO_GARANTIA),
                                CrdClasRgoCred = resultado.CRD_CLAS_RGO_CRED.Trim() == "" ? 0 : int.Parse(resultado.CRD_CLAS_RGO_CRED),
                                CrdOperRen = resultado.CRD_OPER_REN.Trim() == "" ? 0 : int.Parse(resultado.CRD_OPER_REN),
                                CrdFecOtorCre = resultado.CRD_FEC_OTOR_CRE.Trim() == "" ? 0 : int.Parse(resultado.CRD_FEC_OTOR_CRE),
                                CrdFecExtinCre = resultado.CRD_FEC_EXTIN_CRE.Trim() == "" ? 0 : int.Parse(resultado.CRD_FEC_EXTIN_CRE),
                                CrdFecExtinLinea = resultado.CRD_FEC_EXTIN_LINEA.Trim() == "" ? 0 : int.Parse(resultado.CRD_FEC_EXTIN_LINEA),
                                CrdFecProxCambioTasa = resultado.CRD_FEC_PROX_CAMBIO_TASA.Trim() == "" ? 0 : int.Parse(resultado.CRD_FEC_PROX_CAMBIO_TASA),
                                CrdFecUltRenov = resultado.CRD_FEC_ULT_RENOV.Trim() == "" ? 0 : int.Parse(resultado.CRD_FEC_ULT_RENOV),
                                CrdFecPasoVenc = resultado.CRD_FEC_PASO_VENC.Trim() == "" ? 0 : int.Parse(resultado.CRD_FEC_PASO_VENC),
                                CrdFecPasoEjec = resultado.CRD_FEC_PASO_EJEC.Trim() == "" ? 0 : int.Parse(resultado.CRD_FEC_PASO_EJEC),
                                CrdFecDeterioro = resultado.CRD_FEC_DETERIORO.Trim() == "" ? 0 : int.Parse(resultado.CRD_FEC_DETERIORO),
                                CrdFecProxVencFtro = resultado.CRD_FEC_PROX_VENC_FTRO.Trim() == "" ? 0 : int.Parse(resultado.CRD_FEC_PROX_VENC_FTRO),
                                CrdFecUltPagCap = resultado.CRD_FEC_ULT_PAG_CAP.Trim() == "" ? 0 : int.Parse(resultado.CRD_FEC_ULT_PAG_CAP),
                                CrdFecUltPagInt = resultado.CRD_FEC_ULT_PAG_INT.Trim() == "" ? 0 : int.Parse(resultado.CRD_FEC_ULT_PAG_INT),
                                CrdFecRezagoMasAntig = resultado.CRD_FEC_REZAGO_MAS_ANTIG.Trim() == "" ? 0 : int.Parse(resultado.CRD_FEC_REZAGO_MAS_ANTIG),
                                CrdFecImpagaMasAntig = resultado.CRD_FEC_IMPAGA_MAS_ANTIG.Trim() == "" ? 0 : int.Parse(resultado.CRD_FEC_IMPAGA_MAS_ANTIG),
                                CrdFecUltTasa = resultado.CRD_FEC_ULT_TASA.Trim() == "" ? 0 : int.Parse(resultado.CRD_FEC_ULT_TASA),
                                CrdFecPenultTasa = resultado.CRD_FEC_PENULT_TASA.Trim() == "" ? 0 : int.Parse(resultado.CRD_FEC_PENULT_TASA),
                                CrdMo = resultado.CRD_MO,
                                CrdMc = resultado.CRD_MC,
                                CrdMcInt = resultado.CRD_MC_INT,
                                CrdMtoOrigMo = resultado.CRD_MTO_ORIG_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_MTO_ORIG_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdMtoRenMo = resultado.CRD_MTO_REN_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_MTO_REN_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdCapitProxVencMo = resultado.CRD_CAPIT_PROX_VENC_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_CAPIT_PROX_VENC_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdIntProxVencMo = resultado.CRD_INT_PROX_VENC_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_INT_PROX_VENC_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdVigMo = resultado.CRD_VIG_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_VIG_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdMoraH29Mo = resultado.CRD_MORA_H29_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_MORA_H29_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdMoraM30H59Mo = resultado.CRD_MORA_M30_H59_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_MORA_M30_H59_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdMoraM60H89Mo = resultado.CRD_MORA_M60_H89_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_MORA_M60_H89_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdMoraD90Mo = resultado.CRD_MORA_D90_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_MORA_D90_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdIxcVencMo = resultado.CRD_IXC_VENC_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_IXC_VENC_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdPreJudMo = resultado.CRD_PRE_JUD_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_PRE_JUD_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdEjecMo = resultado.CRD_EJEC_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_EJEC_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdVigMn = double.Parse(resultado.CRD_VIG_MN),
                                CrdIxcVencMn = double.Parse(resultado.CRD_IXC_VENC_MN),
                                CrdPreJudMn = double.Parse(resultado.CRD_PRE_JUD_MN),
                                CrdEjecMn = double.Parse(resultado.CRD_EJEC_MN),
                                CrdVigMe = resultado.CRD_VIG_ME.Trim() == "" ? 0 : float.Parse(resultado.CRD_VIG_ME.TrimStart(charQuitar).Replace('.', ',')),
                                CrdIntVencMe = resultado.CRD_INT_VENC_ME.Trim() == "" ? 0 : float.Parse(resultado.CRD_INT_VENC_ME.TrimStart(charQuitar).Replace('.', ',')),
                                CrdPreJudMe = resultado.CRD_PRE_JUD_ME.Trim() == "" ? 0 : float.Parse(resultado.CRD_PRE_JUD_ME.TrimStart(charQuitar).Replace('.', ',')),
                                CrdEjecMe = resultado.CRD_EJEC_ME.Trim() == "" ? 0 : float.Parse(resultado.CRD_EJEC_ME.TrimStart(charQuitar).Replace('.', ',')),
                                CrdInVigMo = resultado.CRD_IN_VIG_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_IN_VIG_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdInMoraH29Mo = resultado.CRD_IN_MORA_H29_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_IN_MORA_H29_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdInMoraM30H59Mo = resultado.CRD_IN_MORA_M30_H59_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_IN_MORA_M30_H59_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdInMoraM60H89Mo = resultado.CRD_IN_MORA_M60_H89_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_IN_MORA_M60_H89_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdInMoraD90Mo = resultado.CRD_IN_MORA_D90_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_IN_MORA_D90_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdIpMoraH29Mo = resultado.CRD_IP_MORA_H29_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_IP_MORA_H29_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdIpMoraM30H59Mo = resultado.CRD_IP_MORA_M30_H59_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_IP_MORA_M30_H59_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdIpMoraM60H89Mo = resultado.CRD_IP_MORA_M60_H89_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_IP_MORA_M60_H89_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdIpMoraD90Mo = resultado.CRD_IP_MORA_D90_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_IP_MORA_D90_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdIpIxcVencMo = resultado.CRD_IP_IXC_VENC_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_IP_IXC_VENC_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdIpPreJudicMo = resultado.CRD_IP_PRE_JUDIC_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_IP_PRE_JUDIC_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdIpEjecMo = resultado.CRD_IP_EJEC_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_IP_EJEC_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdIsNokIxcVencMo = resultado.CRD_IS_NOK_IXC_VENC_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_IS_NOK_IXC_VENC_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdIsNokPreJudicMo = resultado.CRD_IS_NOK_PRE_JUDIC_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_IS_NOK_PRE_JUDIC_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdIsNokEjecMo = resultado.CRD_IS_NOK_EJEC_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_IS_NOK_EJEC_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdIsNokIxcVencMc = resultado.CRD_IS_NOK_IXC_VENC_MC.Trim() == "" ? 0 : float.Parse(resultado.CRD_IS_NOK_IXC_VENC_MC.TrimStart(charQuitar).Replace('.', ',')),
                                CrdPppOrig = resultado.CRD_PPP_ORIG.Trim() == "" ? 0 : int.Parse(resultado.CRD_PPP_ORIG),
                                CrdPppRdual = resultado.CRD_PPP_RDUAL.Trim() == "" ? 0 : int.Parse(resultado.CRD_PPP_RDUAL),
                                CrdCuotCapitXVenc = resultado.CRD_CUOT_CAPIT_X_VENC.Trim() == "" ? 0 : int.Parse(resultado.CRD_CUOT_CAPIT_X_VENC),
                                CrdCuotIntXVenc = resultado.CRD_CUOT_INT_X_VENC.Trim() == "" ? 0 : int.Parse(resultado.CRD_CUOT_INT_X_VENC),
                                CrdCuotAtra = resultado.CRD_CUOT_ATRA.Trim() == "" ? 0 : int.Parse(resultado.CRD_CUOT_ATRA),
                                CrdProxCuot = resultado.CRD_PROX_CUOT.Trim() == "" ? 0 : int.Parse(resultado.CRD_PROX_CUOT),
                                CrdTipInt = resultado.CRD_TIP_INT.Trim() == "" ? 0 : int.Parse(resultado.CRD_TIP_INT),
                                CrdMtdoCalcInt = resultado.CRD_MTDO_CALC_INT.Trim() == "" ? 0 : int.Parse(resultado.CRD_MTDO_CALC_INT),
                                CrdExpreTasa = resultado.CRD_EXPRE_TASA.Trim() == "" ? 0 : int.Parse(resultado.CRD_EXPRE_TASA),
                                CrdTipBase = resultado.CRD_TIP_BASE.Trim() == "" ? 0 : int.Parse(resultado.CRD_TIP_BASE),
                                CrdPtoSobBase = resultado.CRD_PTO_SOB_BASE.Trim() == "" ? 0 : float.Parse(resultado.CRD_PTO_SOB_BASE.TrimStart(charQuitar).Replace('.', ',')),
                                CrdTasaRealAplic = resultado.CRD_TASA_REAL_APLIC.Trim() == "" ? 0 : float.Parse(resultado.CRD_TASA_REAL_APLIC.TrimStart(charQuitar).Replace('.', ',')),
                                CrdTasaEquiAa = resultado.CRD_TASA_EQUI_AA.Trim() == "" ? 0 : float.Parse(resultado.CRD_TASA_EQUI_AA.Replace('.', ',')),
                                CrdCodVariTasa = resultado.CRD_COD_VARI_TASA.Trim() == "" ? 0 : int.Parse(resultado.CRD_COD_VARI_TASA),
                                CrdTipDocSust = resultado.CRD_TIP_DOC_SUST.Trim() == "" ? 0 : int.Parse(resultado.CRD_TIP_DOC_SUST),
                                CrdOfiReaCont = resultado.CRD_OFI_REA_CONT.Trim() == "" ? 0 : int.Parse(resultado.CRD_OFI_REA_CONT),
                                CrdTipoCre = resultado.CRD_TIPO_CRE.Trim() == "" ? 0 : int.Parse(resultado.CRD_TIPO_CRE),
                                CrdEstCre = resultado.CRD_EST_CRE.Trim() == "" ? 0 : int.Parse(resultado.CRD_EST_CRE),
                                CrdCausaExtincionCre = resultado.CRD_CAUSA_EXTINCION_CRE.Trim() == "" ? 0 : int.Parse(resultado.CRD_CAUSA_EXTINCION_CRE),
                                CrdPzoContbCre = resultado.CRD_PZO_CONTB_CRE.Trim() == "" ? 0 : int.Parse(resultado.CRD_PZO_CONTB_CRE),
                                CrdIndDivProrrogados = resultado.CRD_IND_DIV_PRORROGADOS.Trim() == "" ? 0 : int.Parse(resultado.CRD_IND_DIV_PRORROGADOS),
                                CrdParidCapit = resultado.CRD_PARID_CAPIT.Trim() == "" ? 0 : float.Parse(resultado.CRD_PARID_CAPIT.TrimStart(charQuitar).Replace('.', ',')),
                                CrdMtoVencPagMm = resultado.CRD_MTO_VENC_PAG_MM.Trim() == "" ? 0 : float.Parse(resultado.CRD_MTO_VENC_PAG_MM.TrimStart(charQuitar).Replace('.', ',')),
                                CrdMtoVencRenovMm = resultado.CRD_ULT_MTO_PAG_CAP_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_ULT_MTO_PAG_CAP_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdUltMtoPagIntMo = resultado.CRD_ULT_MTO_PAG_INT_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_ULT_MTO_PAG_INT_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdPromCapVigMo = resultado.CRD_PROM_CAP_VIG_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_PROM_CAP_VIG_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdPromCapMorMo = resultado.CRD_PROM_CAP_MOR_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_PROM_CAP_MOR_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdPromCapVenMo = resultado.CRD_PROM_CAP_VEN_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_PROM_CAP_VEN_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdPromIntnMorvenMo = resultado.CRD_PROM_INTN_MORVEN_MO.Trim() == "" ? 0 : float.Parse(resultado.CRD_PROM_INTN_MORVEN_MO.TrimStart(charQuitar).Replace('.', ',')),
                                CrdIntnorRecibMesMc = resultado.CRD_INTNOR_RECIB_MES_MC.Trim() == "" ? 0 : float.Parse(resultado.CRD_INTNOR_RECIB_MES_MC.TrimStart(charQuitar).Replace('.', ',')),
                                CrdIntpenRecibMesMc = resultado.CRD_INTPEN_RECIB_MES_MC.Trim() == "" ? 0 : float.Parse(resultado.CRD_INTPEN_RECIB_MES_MC.TrimStart(charQuitar).Replace('.', ',')),
                                CrdReaRecibMes = int.Parse(float.Parse(resultado.CRD_REA_RECIB_MES).ToString("#############0"))
                            };
                            infor.CDR_Cartera.Add(nuevo);
                        }
                    }
                    else
                    {
                        var resultadosDevengo = uow.SceRepository.sce_dev_cdr_Devengo(periodo, InicioPeriodo, TerminoPeriodo, rut, Operacion, moneda, PlazoTO, todos, TipoConsultaCDR, CantidadRegistros, TipoInterfazCartera);

                        foreach (var resultado in resultadosDevengo)
                        {
                            T_CDR_Deveng nuevo = new T_CDR_Deveng()
                            {
                                DevCpoRutDdorDir = resultado.DEV_CPO_RUT_DDOR_DIR.ToInt(),
                                DevDvRutDdorDir = resultado.DEV_DV_RUT_DDOR_DIR,
                                DevOfiFicCont = int.Parse(resultado.DEV_OFI_FIC_CONT),
                                DevTo = resultado.DEV_TO.Trim() == "" ? 0 : int.Parse(resultado.DEV_TO),
                                DevPppEmb = resultado.DEV_PPP_EMB.Trim() == "" ? 0 : int.Parse(resultado.DEV_PPP_EMB),
                                DevNumDoc = resultado.DEV_NUM_DOC.Trim() == "" ? 0 : int.Parse(resultado.DEV_NUM_DOC),
                                DevIntVigMo = resultado.DEV_INT_VIG_MO.Trim() == "" ? 0 : float.Parse(resultado.DEV_INT_VIG_MO.TrimStart(charQuitar).Replace('.', ',')),
                                DevIntMoraH29Mo = resultado.DEV_INT_MORA_H29_MO.Trim() == "" ? 0 : float.Parse(resultado.DEV_INT_MORA_H29_MO.TrimStart(charQuitar).Replace('.', ',')),
                                DevIntMoraM30H59Mo = resultado.DEV_INT_MORA_M30_H59_MO.Trim() == "" ? 0 : float.Parse(resultado.DEV_INT_MORA_M30_H59_MO.TrimStart(charQuitar).Replace('.', ',')),
                                DevIntMoraM60H89Mo = resultado.DEV_INT_MORA_M60_H89_MO.Trim() == "" ? 0 : float.Parse(resultado.DEV_INT_MORA_M60_H89_MO.TrimStart(charQuitar).Replace('.', ',')),
                                DevIntMoraD90Mo = resultado.DEV_INT_MORA_D90_MO.Trim() == "" ? 0 : float.Parse(resultado.DEV_INT_MORA_D90_MO.TrimStart(charQuitar).Replace('.', ',')),
                                DevIntDivProrrMo = resultado.DEV_INT_DIV_PRORR_MO.Trim() == "" ? 0 : float.Parse(resultado.DEV_INT_DIV_PRORR_MO.TrimStart(charQuitar).Replace('.', ',')),
                                DevIntDifPcioMo = resultado.DEV_INT_DIF_PCIO_MO.Trim() == "" ? 0 : float.Parse(resultado.DEV_INT_DIF_PCIO_MO.TrimStart(charQuitar).Replace('.', ',')),
                                DevIntVigMc = resultado.DEV_INT_VIG_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_INT_VIG_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIntMoraH29Mc = resultado.DEV_INT_MORA_H29_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_INT_MORA_H29_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIntMoraM30H59Mc = resultado.DEV_INT_MORA_M30_H59_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_INT_MORA_M30_H59_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIntMoraM60H89Mc = resultado.DEV_INT_MORA_M60_H89_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_INT_MORA_M60_H89_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIntMoraD90Mc = resultado.DEV_INT_MORA_D90_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_INT_MORA_D90_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIntDivProrrMc = resultado.DEV_INT_DIV_PRORR_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_INT_DIV_PRORR_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIntDifPcioMc = resultado.DEV_INT_DIF_PCIO_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_INT_DIF_PCIO_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIsCrVigMo = resultado.DEV_IS_CR_VIG_MO.Trim() == "" ? 0 : float.Parse(resultado.DEV_IS_CR_VIG_MO.TrimStart(charQuitar).Replace('.', ',')),
                                DevIsCrMoraH29Mo = resultado.DEV_IS_CR_MORA_H29_MO.Trim() == "" ? 0 : float.Parse(resultado.DEV_IS_CR_MORA_H29_MO.TrimStart(charQuitar).Replace('.', ',')),
                                DevIsCrMoraM30H59Mo = resultado.DEV_IS_CR_MORA_M30_H59_MO.Trim() == "" ? 0 : float.Parse(resultado.DEV_IS_CR_MORA_M30_H59_MO.TrimStart(charQuitar).Replace('.', ',')),
                                DevIsCrMoraM60H89Mo = resultado.DEV_IS_CR_MORA_M60_H89_MO.Trim() == "" ? 0 : float.Parse(resultado.DEV_IS_CR_MORA_M60_H89_MO.TrimStart(charQuitar).Replace('.', ',')),
                                DevIsCrMoraD90Mo = resultado.DEV_IS_CR_MORA_D90_MO.Trim() == "" ? 0 : float.Parse(resultado.DEV_IS_CR_MORA_D90_MO.TrimStart(charQuitar).Replace('.', ',')),
                                DevIsCrDivProrrMo = resultado.DEV_IS_CR_DIV_PRORR_MO.Trim() == "" ? 0 : float.Parse(resultado.DEV_IS_CR_DIV_PRORR_MO.TrimStart(charQuitar).Replace('.', ',')),
                                DevIsCrVigMc = resultado.DEV_IS_CR_VIG_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_IS_CR_VIG_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIsCrMoraH29Mc = resultado.DEV_IS_CR_MORA_H29_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_IS_CR_MORA_H29_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIsCrMoraM30H59Mc = resultado.DEV_IS_CR_MORA_M30_H59_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_IS_CR_MORA_M30_H59_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIsCrMoraM60H89Mc = resultado.DEV_IS_CR_MORA_M60_H89_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_IS_CR_MORA_M60_H89_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIsCrMoraD90Mc = resultado.DEV_IS_CR_MORA_D90_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_IS_CR_MORA_D90_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIsCrDivProrrMc = resultado.DEV_IS_CR_DIV_PRORR_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_IS_CR_DIV_PRORR_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIsnCartDetVigMc = resultado.DEV_ISN_CART_DET_VIG_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_ISN_CART_DET_VIG_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIsnCartDetMrH29VigMc = resultado.DEV_ISN_CART_DET_MR_H29_VIG_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_ISN_CART_DET_MR_H29_VIG_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIsnCartDetMrM30H59Mc = resultado.DEV_ISN_CART_DET_MR_M30_H59_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_ISN_CART_DET_MR_M30_H59_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIsnCartDetMrM60H89Mc = resultado.DEV_ISN_CART_DET_MR_M60_H89_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_ISN_CART_DET_MR_M60_H89_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIsnCartDetMrD90Mc = resultado.DEV_ISN_CART_DET_MR_D90_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_ISN_CART_DET_MR_D90_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIsCvDivProrrMo = resultado.DEV_IS_CV_DIV_PRORR_MO.Trim() == "" ? 0 : float.Parse(resultado.DEV_IS_CV_DIV_PRORR_MO.TrimStart(charQuitar).Replace('.', ',')),
                                DevIspCartDetVigMc = resultado.DEV_ISP_CART_DET_VIG_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_ISP_CART_DET_VIG_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIspCartDetMrH29Mc = resultado.DEV_ISP_CART_DET_MR_H29_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_ISP_CART_DET_MR_H29_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIspCartDetMrM30H59Mc = resultado.DEV_ISP_CART_DET_MR_M30_H59_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_ISP_CART_DET_MR_M30_H59_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIspCartDetMrM60H89Mc = resultado.DEV_ISP_CART_DET_MR_M60_H89_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_ISP_CART_DET_MR_M60_H89_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIspCartDetMrD90Mc = resultado.DEV_ISP_CART_DET_MR_D90_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_ISP_CART_DET_MR_D90_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIsCvDivProrrMc = resultado.DEV_IS_CV_DIV_PRORR_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_IS_CV_DIV_PRORR_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIpMoraH29Mc = resultado.DEV_IP_MORA_H29_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_IP_MORA_H29_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIpMoraM30H59Mc = resultado.DEV_IP_MORA_M30_H59_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_IP_MORA_M30_H59_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIpMoraM60H89Mc = resultado.DEV_IP_MORA_M60_H89_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_IP_MORA_M60_H89_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIpMoraD90Mc = resultado.DEV_IP_MORA_D90_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_IP_MORA_D90_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIpVencCobMc = resultado.DEV_IP_VENC_COB_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_IP_VENC_COB_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIpPreJudicMc = resultado.DEV_IP_PRE_JUDIC_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_IP_PRE_JUDIC_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevIpEjecMc = resultado.DEV_IP_EJEC_MC.Trim() == "" ? 0 : float.Parse(resultado.DEV_IP_EJEC_MC.TrimStart(charQuitar).Replace('.', ',')),
                                DevReajVigMn = resultado.DEV_REAJ_VIG_MN.Trim() == "" ? 0 : int.Parse(resultado.DEV_REAJ_VIG_MN),
                                DevReajMoraH29Mn = resultado.DEV_REAJ_MORA_H29_MN.Trim() == "" ? 0 : int.Parse(resultado.DEV_REAJ_MORA_H29_MN),
                                DevReajMoraM30H59Mn = resultado.DEV_REAJ_MORA_M30_H59_MN.Trim() == "" ? 0 : int.Parse(resultado.DEV_REAJ_MORA_M30_H59_MN),
                                DevReajMoraM60H89Mn = resultado.DEV_REAJ_MORA_M60_H89_MN.Trim() == "" ? 0 : int.Parse(resultado.DEV_REAJ_MORA_M60_H89_MN),
                                DevReajMoraD90Mn = resultado.DEV_REAJ_MORA_D90_MN.Trim() == "" ? 0 : int.Parse(resultado.DEV_REAJ_MORA_D90_MN),
                                DevReajDivProrrMn = resultado.DEV_REAJ_DIV_PRORR_MN.Trim() == "" ? 0 : int.Parse(resultado.DEV_REAJ_DIV_PRORR_MN),
                                DevReajDifPcioMn = resultado.DEV_REAJ_DIF_PCIO_MN.Trim() == "" ? 0 : int.Parse(resultado.DEV_REAJ_DIF_PCIO_MN),
                                DevRsCartDetVigMn = resultado.DEV_RS_CART_DET__VIG_MN.Trim() == "" ? 0 : int.Parse(resultado.DEV_RS_CART_DET__VIG_MN),
                                DevRsCartDetMoraH29Mn = resultado.DEV_RS_CART_DET_MORA_H29_MN.Trim() == "" ? 0 : int.Parse(resultado.DEV_RS_CART_DET_MORA_H29_MN),
                                DevRsCartDetM30H59Mn = resultado.DEV_RS_CART_DET_M30_H59_MN.Trim() == "" ? 0 : int.Parse(resultado.DEV_RS_CART_DET_M30_H59_MN),
                                DevRsCartDetM60H89Mn = resultado.DEV_RS_CART_DET_M60_H89_MN.Trim() == "" ? 0 : int.Parse(resultado.DEV_RS_CART_DET_M60_H89_MN),
                                DevRsCartDetD90Mn = resultado.DEV_RS_CART_DET_D90_MN.Trim() == "" ? 0 : int.Parse(resultado.DEV_RS_CART_DET_D90_MN),
                                DevRsDivProrrMo = resultado.DEV_RS_DIV_PRORR_MO.Trim() == "" ? 0 : int.Parse(resultado.DEV_RS_DIV_PRORR_MO),
                                DevRsCmVigMo = resultado.DEV_RS_CM_VIG_MO.Trim() == "" ? 0 : int.Parse(resultado.DEV_RS_CM_VIG_MO),
                                DevRsCmMoraH29Mo = resultado.DEV_RS_CM_MORA_H29_MO.Trim() == "" ? 0 : int.Parse(resultado.DEV_RS_CM_MORA_H29_MO),
                                DevRsCmMoraM30H59Mo = resultado.DEV_RS_CM_MORA_M30_H59_MO.Trim() == "" ? 0 : int.Parse(resultado.DEV_RS_CM_MORA_M30_H59_MO),
                                DevRsCmMoraM60H89Mo = resultado.DEV_RS_CM_MORA_M60_H89_MO.Trim() == "" ? 0 : int.Parse(resultado.DEV_RS_CM_MORA_M60_H89_MO),
                                DevRsCmMoraD90Mo = resultado.DEV_RS_CM_MORA_D90_MO.Trim() == "" ? 0 : int.Parse(resultado.DEV_RS_CM_MORA_D90_MO),
                                DevRsCmIntCobVenc = resultado.DEV_RS_CM_INT_COB_VENC.Trim() == "" ? 0 : int.Parse(resultado.DEV_RS_CM_INT_COB_VENC),
                                DevRsCmPreJudicMo = resultado.DEV_RS_CM_PRE_JUDIC_MO.Trim() == "" ? 0 : int.Parse(resultado.DEV_RS_CM_PRE_JUDIC_MO),
                                DevRsCmEnEjecMo = resultado.DEV_RS_CM_EN_EJEC_MO.Trim() == "" ? 0 : int.Parse(resultado.DEV_RS_CM_EN_EJEC_MO),
                                DevPppContabCre = resultado.DEV_PPP_CONTAB_CRE.Trim() == "" ? 0 : int.Parse(resultado.DEV_PPP_CONTAB_CRE),
                                DevCodMdaContabCap = resultado.DEV_COD_MDA_CONTAB_CAP.Trim() == "" ? 0 : int.Parse(resultado.DEV_COD_MDA_CONTAB_CAP)
                            };
                            infor.CDR_Deveng.Add(nuevo);
                        }
                    }

                    BuscarCDR = true;

                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, problemas al obtener los datos de la interfaz CDR", ex);
                    listaMensajes.Add(new UI_Message() { Text = "Problemas al obtener los datos de la interfaz CDR", Type = TipoMensaje.Error });
                }
            }

            return BuscarCDR;
        }

        public static MemoryStream getFileCarteraCDR(IList<T_CDR_Cartera> datos)
        {
            using (Tracer tracer = new Tracer("getFileCarteraCDR"))
            {
                MemoryStream stream = new MemoryStream();
                using (SLDocument doc = new SLDocument())
                {
                    doc.RenameWorksheet(SLDocument.DefaultFirstSheetName, NombreWorksheetCartera);

                    doc.SelectWorksheet(NombreWorksheetCartera);
                    CargarHojaExcelCartera(datos, doc);

                    doc.SaveAs(stream);
                }

                stream.Position = 0; //importante, dejar el stream pronto para leer;
                return stream;
            }
        }

        private static void CargarHojaExcelCartera(IList<T_CDR_Cartera> acs, SLDocument doc)
        {
            #region style
            SLStyle styleTitulo = doc.CreateStyle();
            styleTitulo.Font.Bold = true;
            styleTitulo.Font.FontSize = 15;
            styleTitulo.Font.FontColor = System.Drawing.Color.Blue;
            styleTitulo.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleSubtitulo = doc.CreateStyle();
            styleSubtitulo.Font.Bold = true;
            styleSubtitulo.Font.FontSize = 13;
            styleSubtitulo.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleEncabezado = doc.CreateStyle();
            styleEncabezado.Font.Bold = true;
            styleEncabezado.Font.FontColor = System.Drawing.Color.Blue;
            styleEncabezado.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleBold = doc.CreateStyle();
            styleBold.Font.Bold = true;
            styleBold.SetHorizontalAlignment(HorizontalAlignmentValues.Left);

            SLStyle styleFechaBold = doc.CreateStyle();
            styleFechaBold.Font.Bold = true;
            styleFechaBold.FormatCode = "dd/MM/yyyy";

            SLStyle styleFecha = doc.CreateStyle();
            styleFecha.FormatCode = "dd/MM/yyyy";

            SLStyle styleFechayyyyMMdd = doc.CreateStyle();
            styleFechayyyyMMdd.FormatCode = "yyyymmdd";


            SLStyle styleDecimalMonedaExtranjera = doc.CreateStyle();
            styleDecimalMonedaExtranjera.FormatCode = "#,##0.00";

            SLStyle styleDecimalPesosChilenos = doc.CreateStyle();
            styleDecimalPesosChilenos.FormatCode = "#,###,###,##0";

            SLStyle styleRut = doc.CreateStyle();
            styleRut.FormatCode = "########-#";

            SLStyle styleDecimal = doc.CreateStyle();
            styleDecimal.FormatCode = "#0.00";

            SLStyle styleInt = doc.CreateStyle();
            styleInt.FormatCode = "0";
            #endregion

            doc.MergeWorksheetCells("A1", "CT1");

            doc.SetCellValue("A1", "Interfaz Cartera CDR");
            doc.SetCellStyle("A1", styleTitulo);

            int colIndex = 1;
            int rowIndex = 3;
            #region titulos
            doc.SetCellValue(rowIndex, colIndex++, "Rut");
            doc.SetCellValue(rowIndex, colIndex++, "dv");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-OFI-FIC-CONT");
            doc.SetCellValue(rowIndex, colIndex++, "TO");
            doc.SetCellValue(rowIndex, colIndex++, "Plazo Prom");
            doc.SetCellValue(rowIndex, colIndex++, "N° Operación");
            doc.SetCellValue(rowIndex, colIndex++, "Estado");
            doc.SetCellValue(rowIndex, colIndex++, "Act Económica");
            doc.SetCellValue(rowIndex, colIndex++, "Garantía");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-CLAS-RGO-CRED");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-OPER-REN");
            doc.SetCellValue(rowIndex, colIndex++, "F Otorg Original");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-FEC-APROB-LINEA");
            doc.SetCellValue(rowIndex, colIndex++, "F Vcto");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-FEC-EXTIN-LINEA");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-FEC-PROX-CAMBIO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-FEC-ULT-RENOV");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-FEC-PASO-VENC");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-FEC-PASO-EJEC");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-FEC-DETERIORO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-FEC-PROX-VENC-FTRO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-FEC-ULT-PAG-CAP");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-FEC-ULT-PAG-INT");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-FEC-REZAGO-MAS-ANTIG");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-FEC-IMPAGA-MAS-ANTIG");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-FEC-ULT-TASA");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-FEC-PENULT-TASA");
            doc.SetCellValue(rowIndex, colIndex++, "Moneda (MO)");
            doc.SetCellValue(rowIndex, colIndex++, "Mon K");
            doc.SetCellValue(rowIndex, colIndex++, "Mon Int");
            doc.SetCellValue(rowIndex, colIndex++, "Monto Original");
            doc.SetCellValue(rowIndex, colIndex++, "Monto Renovado");
            doc.SetCellValue(rowIndex, colIndex++, "Monto Cuota prox");
            doc.SetCellValue(rowIndex, colIndex++, "Intereses cuota a vencer");
            doc.SetCellValue(rowIndex, colIndex++, "saldo Capital");
            doc.SetCellValue(rowIndex, colIndex++, "saldo Capital H29d");
            doc.SetCellValue(rowIndex, colIndex++, "Saldo capital 30-59d");
            doc.SetCellValue(rowIndex, colIndex++, "Saldo capital 60-89d");
            doc.SetCellValue(rowIndex, colIndex++, "Saldo capital 90d+");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-IXC-VENC-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-PRE-JUD-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-EJEC-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-VIG-MN");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-IXC-VENC-MN");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-PRE-JUD-MN");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-EJEC-MN");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-VIG-ME");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-INT-VENC-ME");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-PRE-JUD-ME");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-EJEC-ME");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-IN-VIG-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-IN-MORA-H29-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-IN-MORA-M30-H59-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-IN-MORA-M60-H89-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-IN-MORA-D90-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-IP-MORA-H29-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-IP-MORA-M30-H59-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-IP-MORA-M60-H89-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-IP-MORA-D90-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-IP-IXC-VENC-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-IP-PRE-JUDIC-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-IP-EJEC-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-IS-NOK-IXC-VENC-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-IS-NOK-PRE-JUDIC-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-IS-NOK-EJEC-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-IS-NOK-IXC-VENC-MC");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-PPP-ORIG");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-PPP-RDUAL");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-CUOT-CAPIT-X-VENC");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-CUOT-INT-X-VENC");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-CUOT-ATRA");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-PROX-CUOT");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-TIP-INT");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-MTDO-CALC-INT");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-EXPRE-TASA");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-TIP-BASE");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-PTO-SOB-BASE");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-TASA-REAL-APLIC");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-TASA-EQUI-AA");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-COD-VARI-TASA");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-TIP-DOC-SUST");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-OFI-REA-CONT");
            doc.SetCellValue(rowIndex, colIndex++, "N° DE PARTIDA");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-EST-CRE");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-CAUSA-EXTINCION-CRE");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-PZO-CONTB-CRE");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-IND-DIV-PRORROGADOS");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-PARID-CAPIT");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-MTO-VENC-PAG-MM");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-ULT-MTO-PAG-CAP-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-ULT-MTO-PAG-INT-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-PROM-CAP-VIG-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-PROM-CAP-MOR-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-PROM-CAP-VEN-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-PROM-INTN-MORVEN-MO");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-INTNOR-RECIB-MES-MC");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-INTPEN-RECIB-MES-MC");
            doc.SetCellValue(rowIndex, colIndex++, "CRD-REA-RECIB-MES");
            #endregion

            doc.SetCellStyle("A3", "CT3", styleEncabezado);
            doc.SetColumnStyle(1, styleInt);
            doc.SetColumnStyle(31, 42, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(43, styleDecimalPesosChilenos);
            doc.SetColumnStyle(45, 66, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(77, 79, styleDecimal);
            doc.SetColumnStyle(88, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(89, styleDecimalPesosChilenos);
            doc.SetColumnStyle(90, 98, styleDecimalMonedaExtranjera);

            DataTable dt = Modulos.Modulo1.ConvertToDataTable(acs);

            doc.ImportDataTable(4, 1, dt, false);

            doc.AutoFitColumn("A", "CT");
        }

        public static MemoryStream getFileDevengoCDR(IList<T_CDR_Deveng> datos)
        {
            using (Tracer tracer = new Tracer("GetFile"))
            {
                MemoryStream stream = new MemoryStream();
                using (SLDocument doc = new SLDocument())
                {
                    doc.RenameWorksheet(SLDocument.DefaultFirstSheetName, NombreWorksheetDevengo);

                    doc.SelectWorksheet(NombreWorksheetDevengo);
                    CargarHojaExcelDevengo(datos, doc);

                    doc.SaveAs(stream);
                }

                stream.Position = 0; //importante, dejar el stream pronto para leer;
                return stream;
            }
        }

        private static void CargarHojaExcelDevengo(IList<T_CDR_Deveng> acs, SLDocument doc)
        {
            #region style
            SLStyle styleTitulo = doc.CreateStyle();
            styleTitulo.Font.Bold = true;
            styleTitulo.Font.FontSize = 15;
            styleTitulo.Font.FontColor = System.Drawing.Color.Blue;
            styleTitulo.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleSubtitulo = doc.CreateStyle();
            styleSubtitulo.Font.Bold = true;
            styleSubtitulo.Font.FontSize = 13;
            styleSubtitulo.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleEncabezado = doc.CreateStyle();
            styleEncabezado.Font.Bold = true;
            styleEncabezado.Font.FontColor = System.Drawing.Color.Blue;
            styleEncabezado.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleBold = doc.CreateStyle();
            styleBold.Font.Bold = true;
            styleBold.SetHorizontalAlignment(HorizontalAlignmentValues.Left);

            SLStyle styleAgrupador = doc.CreateStyle();
            styleAgrupador.Font.Bold = true;
            styleAgrupador.Font.FontSize = 13;
            styleAgrupador.Font.FontColor = System.Drawing.Color.Blue;
            styleAgrupador.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
            styleAgrupador.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Aqua, System.Drawing.Color.White);

            SLStyle styleFechaBold = doc.CreateStyle();
            styleFechaBold.Font.Bold = true;
            styleFechaBold.FormatCode = "dd/MM/yyyy";

            SLStyle styleFecha = doc.CreateStyle();
            styleFecha.FormatCode = "dd/MM/yyyy";

            SLStyle styleFechayyyyMMdd = doc.CreateStyle();
            styleFechayyyyMMdd.FormatCode = "yyyymmdd";


            SLStyle styleDecimalMonedaExtranjera = doc.CreateStyle();
            styleDecimalMonedaExtranjera.FormatCode = "#,##0.00";

            SLStyle styleDecimalPesosChilenos = doc.CreateStyle();
            styleDecimalPesosChilenos.FormatCode = "#,###,###,##0";

            SLStyle styleRut = doc.CreateStyle();
            styleRut.FormatCode = "########-#";

            SLStyle styleDecimal = doc.CreateStyle();
            styleDecimal.FormatCode = "#0.00";

            SLStyle styleInt = doc.CreateStyle();
            styleInt.FormatCode = "0";
            #endregion

            doc.MergeWorksheetCells("A1", "BV1");

            doc.SetCellValue("A1", "Interfaz Devengamiento CDR");
            doc.SetCellStyle("A1", styleTitulo);

            int colIndex = 1;
            int rowIndex = 3;
            #region titulos
            doc.SetCellValue(rowIndex, colIndex++, "Rut");
            doc.SetCellValue(rowIndex, colIndex++, "Dig Verif");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-OFI-FIC-CONT");
            doc.SetCellValue(rowIndex, colIndex++, "TO");
            doc.SetCellValue(rowIndex, colIndex++, "Plazo promedio de Embarque");
            doc.SetCellValue(rowIndex, colIndex++, "N° Operación");
            doc.SetCellValue(rowIndex, colIndex++, "Dev Int Vig MO");
            doc.SetCellValue(rowIndex, colIndex++, "Dev Int MH29 MO");
            doc.SetCellValue(rowIndex, colIndex++, "Dev Int M30-59 MO");
            doc.SetCellValue(rowIndex, colIndex++, "Dev Int M60-89 MO");
            doc.SetCellValue(rowIndex, colIndex++, "Dev Int M90+ MO");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-INT-FILLER");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-INT-DIF-PCIO-MO");
            doc.SetCellValue(rowIndex, colIndex++, "Dev Int Vig MC");
            doc.SetCellValue(rowIndex, colIndex++, "Dev Int MH29 MC");
            doc.SetCellValue(rowIndex, colIndex++, "Dev Int M30-59 MC");
            doc.SetCellValue(rowIndex, colIndex++, "Dev Int M60-89 MC");
            doc.SetCellValue(rowIndex, colIndex++, "Dev Int M90+ MC");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-INT-FILLER");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-INT-DIF-PCIO-MC");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-IS-CR-VIG-MO");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-IS-CR-MORA-H29-MO");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-IS-CR-MORA-M30-H59-MO");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-IS-CR-MORA-M60-H89-MO");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-IS-CR-MORA-D90-MO");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-IS-CR-DIV-PRORR-MO");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-IS-CR-VIG-MC");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-IS-CR-MORA-H29-MC");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-IS-CR-MORA-M30-H59-MC");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-IS-CR-MORA-M60-H89-MC");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-IS-CR-MORA-D90-MC");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-IS-CR-DIV-PRORR-MC");
            doc.SetCellValue(rowIndex, colIndex++, "Acint Vig MC");
            doc.SetCellValue(rowIndex, colIndex++, "Acint-MoraH29 MC");
            doc.SetCellValue(rowIndex, colIndex++, "Acint-Mora30-59 MC");
            doc.SetCellValue(rowIndex, colIndex++, "Acint-Mora60-89 MC");
            doc.SetCellValue(rowIndex, colIndex++, "Acint-Mora 90+MC");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-IS-CV-DIV-PRORR-MO");
            doc.SetCellValue(rowIndex, colIndex++, "AcintPenal Vig MC");
            doc.SetCellValue(rowIndex, colIndex++, "AcintPenal MH29 MC");
            doc.SetCellValue(rowIndex, colIndex++, "AcintPenal M30-59MC");
            doc.SetCellValue(rowIndex, colIndex++, "AcintPenal M60-89MC");
            doc.SetCellValue(rowIndex, colIndex++, "AcintPenal M90+MC");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-IS-CV-DIV-PRORR-MC");
            doc.SetCellValue(rowIndex, colIndex++, "IntPenales MH29MC");
            doc.SetCellValue(rowIndex, colIndex++, "IntPenales M30-59MC");
            doc.SetCellValue(rowIndex, colIndex++, "IntPenales M60-89MC");
            doc.SetCellValue(rowIndex, colIndex++, "IntPenales M90+MC");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-IP-VENC-COB-MC");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-IP-PRE-JUDIC-MC");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-IP-EJEC-MC");
            doc.SetCellValue(rowIndex, colIndex++, "Reaj xcob Vig MN");
            doc.SetCellValue(rowIndex, colIndex++, "Reaj xcob MH29 MN");
            doc.SetCellValue(rowIndex, colIndex++, "Reaj xcob M30-59 MN");
            doc.SetCellValue(rowIndex, colIndex++, "Reaj xcob M60-89 MN");
            doc.SetCellValue(rowIndex, colIndex++, "Reaj xcob M90+MN");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-REAJ-DIV-PRORR-MN");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-REAJ-DIF-PCIO-MN");
            doc.SetCellValue(rowIndex, colIndex++, "AComp Reaj Vig MN");
            doc.SetCellValue(rowIndex, colIndex++, "Acomp Reaj MH29 MN");
            doc.SetCellValue(rowIndex, colIndex++, "Acomp Reaj M30-59 MN");
            doc.SetCellValue(rowIndex, colIndex++, "Acomp Reaj M60-89 MN");
            doc.SetCellValue(rowIndex, colIndex++, "Acomp Reaj M90+MN");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-RS-DIV-PRORR-MO");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-RS-CM-VIG-MO");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-RS-CM-MORA-H29-MO");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-RS-CM-MORA-M30-H59-MO");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-RS-CM-MORA-M60-H89-MO");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-RS-CM-MORA-D90-MO");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-RS-CM-INT-COB-VENC");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-RS-CM-PRE-JUDIC-MO");
            doc.SetCellValue(rowIndex, colIndex++, "DEV-RS-CM-EN-EJEC-MO");
            doc.SetCellValue(rowIndex, colIndex++, "Plazo Contable");
            doc.SetCellValue(rowIndex, colIndex++, "Moneda SBIF");
            #endregion

            doc.SetCellStyle("A3", "BV3", styleEncabezado);

            doc.MergeWorksheetCells("G2", "K2");
            doc.SetCellValue("G2", "INTERESES POR COBRAR NORMALES M/ORIGEN");
            doc.SetCellStyle("G2", styleAgrupador);

            doc.MergeWorksheetCells("N2", "R2");
            doc.SetCellValue("N2", "INTERESES POR COBRAR NORMALES M/CONTABLE");
            doc.SetCellStyle("N2", styleAgrupador);

            doc.MergeWorksheetCells("AG2", "AK2");
            doc.SetCellValue("AG2", "ACTIVO COMPLEMENTARIO INTERES");
            doc.SetCellStyle("AG2", styleAgrupador);

            doc.MergeWorksheetCells("AM2", "AQ2");
            doc.SetCellValue("AM2", "ACTIVO COMPLEMENTARIO INTERES PENAL");
            doc.SetCellStyle("AM2", styleAgrupador);

            doc.MergeWorksheetCells("AS2", "AV2");
            doc.SetCellValue("AS2", "INTERESES PENALES");
            doc.SetCellStyle("AS2", styleAgrupador);

            doc.MergeWorksheetCells("AZ2", "BD2");
            doc.SetCellValue("AZ2", "REAJUSTES POR COBRAR");
            doc.SetCellStyle("AZ2", styleAgrupador);

            doc.MergeWorksheetCells("BG2", "BK2");
            doc.SetCellValue("BG2", "ACTIVOS COMPLEMENTARIOS DE REAJUSTES");
            doc.SetCellStyle("BG2", styleAgrupador);

            doc.MergeWorksheetCells("BG2", "BK2");
            doc.SetCellValue("BG2", "ACTIVOS COMPLEMENTARIOS DE REAJUSTES");
            doc.SetCellStyle("BG2", styleAgrupador);

            doc.SetColumnStyle(7, 51, styleDecimalMonedaExtranjera);
            //REAJUSTES POR COBRAR				
            doc.SetColumnStyle(52, 56, styleDecimalMonedaExtranjera);
            //ACTIVOS COMPLEMENTARIOS DE REAJUSTES				
            doc.SetColumnStyle(59, 63, styleDecimalMonedaExtranjera);
            DataTable dt = Modulos.Modulo1.ConvertToDataTable(acs);

            doc.ImportDataTable(4, 1, dt, false);
            doc.AutoFitColumn("A", "BV");
        }
    }
}
