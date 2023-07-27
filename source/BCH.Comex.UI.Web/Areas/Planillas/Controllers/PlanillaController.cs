using BCH.Comex.Core.BL.XGPL;
using BCH.Comex.UI.Web.Areas.Planillas.Models;
using BCH.Comex.UI.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BCH.Comex.Utils;

namespace BCH.Comex.UI.Web.Areas.Planillas.Controllers
{
    public class PlanillaController : PlanillasBaseController
    {
        private static readonly DateTime SqlSmallDateTimeMinValue = new DateTime(1900, 01, 01, 00, 00, 00);
        private XgplService bl
        {
            get
            {
                return new XgplService(HttpContext.GetCurrentUser().GetDatosUsuario());
            }
        }

        public PlanillaController()
        {
            //this.bl = new XgplService(HttpContext.GetCurrentUser().GetDatosUsuario());
        }

        IngresoVisibleExportacionViewModel ingresoVisibleExportacionViewModel;
        private IQueryable<T_xPlv> GetPlanillaIngresoVisibleExportacion(string numeroPresentacion, DateTime? fechaPresentacion)
        {
            return bl.Sce_Xplv_S11(numeroPresentacion, fechaPresentacion.Value).Select(
                                    p => new T_xPlv()
                                    {
                                        NumPre = p.numpre,
                                        estado = p.estado,
                                        fecpre = p.fecpre,
                                        PlzBcc = p.plzbcc,
                                        TipPln = p.tippln,
                                        Cencos = p.cencos,
                                        IndDir = p.inddir,
                                        IndNom = p.indnom,
                                        PrtExp = p.prtexp,
                                        RutExp = p.rutexp,
                                        CodMnd = p.codmnd,
                                        MtoBru = p.mtobru,
                                        DedCom = p.dedcom,
                                        MtoCom = p.mtocom,
                                        MtoOtg = p.mtootg,
                                        MtoLiq = p.mtoliq,
                                        Mtopar = p.mtopar,
                                        MtoDol = p.mtodol,
                                        codadn = p.codadn,
                                        NumDec = p.numdec,
                                        FecDec = p.fecdec,
                                        FecVen = p.fecven,
                                        DfoCea = p.dfocea,
                                        DfoCtf = p.dfoctf,
                                        DfoCbc = p.dfocbc,
                                        DfoNpr = p.dfonpr,
                                        DfoFpr = p.dfofpr,
                                        AfiMnd = p.afimnd,
                                        AfiPar = p.afipar,
                                        AfiMto = p.afimto,
                                        AfiMtoD = p.afimtod,
                                        AfiVen = p.afiven,
                                        DiePbc = p.diepbc,
                                        DieNum = p.dienum,
                                        DieFec = p.diefec,
                                        ObsPln = p.obspln,
                                        PlnEst = p.plnest,
                                        TipCam = p.tipcam,
                                    });
        }

        public ActionResult VerIngresoVisibleExportacion(string numeroPresentacion, DateTime fechaPresentacion)
        {
            ModelState.Clear();

            T_xPlv planilla = GetPlanillaIngresoVisibleExportacion(numeroPresentacion, fechaPresentacion).FirstOrDefault();

            if (planilla != null)
            {
                ingresoVisibleExportacionViewModel = new IngresoVisibleExportacionViewModel();
                ingresoVisibleExportacionViewModel.NumeroPresentacion = planilla.NumPre.FormatoNroPresentacion();
                ingresoVisibleExportacionViewModel.FechaPresentacion = planilla.fecpre;
                ingresoVisibleExportacionViewModel.FechaAnterior = planilla.fecpre;
                ingresoVisibleExportacionViewModel.CodigoTipoPlanilla = planilla.TipPln;
                ingresoVisibleExportacionViewModel.CentroCosto = planilla.Cencos;
                ingresoVisibleExportacionViewModel.TipoOperacion = MODXPLN1.GetNombrePlanilla((int)planilla.TipPln);
                ingresoVisibleExportacionViewModel.RutExportador = planilla.RutExp.FormatoRut();
                ingresoVisibleExportacionViewModel.NombreParty = MODGTAB0.GetNombreParty(planilla.PrtExp, (int)planilla.IndNom, bl);
                ingresoVisibleExportacionViewModel.DireccionParty = MODGTAB0.GetDireccionParty(planilla.PrtExp, (byte)planilla.IndDir, bl);
                ingresoVisibleExportacionViewModel.CodigoPlazaBancoCentralContabilidad = (int)planilla.PlzBcc;
                ingresoVisibleExportacionViewModel.PlazaBancoCentralContabilidad = MODGTAB0.GetNombrePlazaBancoCentral((int)planilla.PlzBcc, bl);

                ingresoVisibleExportacionViewModel.HabilitarGrupoMonto = PreparaGrupoMontoRetornado(planilla);

                if (!MODXPLN1.PLNTRN.Contains(planilla.TipPln)
                    && !MODXPLN1.PLNINF.Contains(planilla.TipPln)
                    && planilla.TipPln != 402)
                {
                    ingresoVisibleExportacionViewModel.TipoCambio = planilla.TipCam;
                }

                ingresoVisibleExportacionViewModel.Aduana = MODGTAB0.GetNombreAduana(planilla.codadn, bl);
                if (planilla.codadn > 0)
                {
                    ingresoVisibleExportacionViewModel.CodigoAduana = planilla.codadn;
                }
                if (planilla.NumDec.Trim() != "")
                {
                    ingresoVisibleExportacionViewModel.NumeroDeclaracion = planilla.NumDec.PadLeft(7, '0').FormatoRut();
                }
                ingresoVisibleExportacionViewModel.FechaDeclaracion = planilla.FecDec;
                ingresoVisibleExportacionViewModel.FechaVencimiento = planilla.FecVen;

                if (planilla.DfoCea != 0)
                {
                    ingresoVisibleExportacionViewModel.DFOCodigoEntidadAutorizada = planilla.DfoCea;
                    ingresoVisibleExportacionViewModel.DFOEntidadAutorizada = MODGTAB0.GetNombreBanco((int)planilla.DfoCea, bl);
                }

                if (planilla.DfoCtf != 0)
                {
                    ingresoVisibleExportacionViewModel.DFOCodigoTipoFinanciamiento = planilla.DfoCtf;
                    ingresoVisibleExportacionViewModel.DFOTipoFinanciamiento = MODXPLN1.GetNombrePlanilla((int)planilla.DfoCtf);
                }

                if (planilla.DfoCbc != 0)
                {
                    ingresoVisibleExportacionViewModel.DFOCodigoBancoCentral = planilla.DfoCbc;
                    ingresoVisibleExportacionViewModel.DFOBancoCentral = MODGTAB0.GetNombrePlazaBancoCentral((int)planilla.DfoCbc, bl);
                }

                if (!string.IsNullOrWhiteSpace(planilla.DfoNpr) && Convert.ToDouble(planilla.DfoNpr) != 0)
                {
                    ingresoVisibleExportacionViewModel.DFONumeroPresentacion = planilla.DfoNpr;
                }

                ingresoVisibleExportacionViewModel.DFOFechaPresentacion = planilla.DfoFpr;
                if (planilla.AfiMnd != 0)
                {
                    ingresoVisibleExportacionViewModel.AFCodigoMoneda = planilla.AfiMnd;
                    ingresoVisibleExportacionViewModel.AFMoneda = MODGTAB0.GetNombreMoneda((int)planilla.AfiMnd, bl);
                }

                if (planilla.AfiPar != 0)
                {
                    ingresoVisibleExportacionViewModel.AFParidadUSD = planilla.AfiPar;
                }

                if (planilla.AfiMto != 0)
                {
                    ingresoVisibleExportacionViewModel.AFMonto = planilla.AfiMto;
                }

                if (planilla.AfiMtoD != 0)
                {
                    ingresoVisibleExportacionViewModel.AFMontoDolar = planilla.AfiMtoD;
                }

                if (planilla.AfiVen != 0)
                {
                    ingresoVisibleExportacionViewModel.AFPlazoVencimiento = planilla.AfiVen;
                }

                if (planilla.DiePbc != 0)
                {
                    ingresoVisibleExportacionViewModel.DIECodigoBancoCentral = planilla.DiePbc;
                    ingresoVisibleExportacionViewModel.DIEBancoCentral = MODGTAB0.GetNombrePlazaBancoCentral((int)planilla.DiePbc, bl);
                }

                if (!string.IsNullOrWhiteSpace(planilla.DieNum))
                {
                    ingresoVisibleExportacionViewModel.DIENumeroEmision = planilla.DieNum.PadLeft(7, '0').FormatoRut();
                }

                ingresoVisibleExportacionViewModel.DIEFechaEmision = planilla.DieFec;
                ingresoVisibleExportacionViewModel.Observaciones = planilla.ObsPln;

                planilla.CodPai = MODXPLN1.GetPlanillaPais(planilla.NumPre.Replace("-",""), planilla.fecpre, bl);

                if (planilla.CodPai != null)
                {
                    ingresoVisibleExportacionViewModel.Pais = MODGTAB0.GetNombrePais((int)planilla.CodPai, bl);
                }

                ingresoVisibleExportacionViewModel.PlnEstado = planilla.PlnEst;

                ingresoVisibleExportacionViewModel.PlanillaGuardada = false;

                return View(ingresoVisibleExportacionViewModel);
            }
            throw new Exception("La planilla que está intentando buscar no existe o fue actualizada.");
        }

        private bool PreparaGrupoMontoRetornado(T_xPlv planilla)
        {
            if (MODXPLN1.PLN400.IndexOf(planilla.TipPln.ToString(), 0, StringComparison.CurrentCulture) == -1)
            {
                ingresoVisibleExportacionViewModel.Moneda = MODGTAB0.GetNombreMoneda((int)planilla.CodMnd, bl);
                ingresoVisibleExportacionViewModel.CodigoMoneda =  MODGTAB0.GetCodigoMonedaBancoCentral((int)planilla.CodMnd, bl);
                ingresoVisibleExportacionViewModel.MontoBruto = (decimal)planilla.MtoBru;

                // Comisiones.
                if (planilla.DedCom == false)
                {
                    if (planilla.MtoCom > 0)
                    {
                        ingresoVisibleExportacionViewModel.ParentesisComisiones = false;
                    }
                }
                else
                {
                    if (planilla.MtoCom > 0)
                    {
                        ingresoVisibleExportacionViewModel.ParentesisComisiones = true;
                    }
                }
                ingresoVisibleExportacionViewModel.Comisiones = planilla.MtoCom;

                if (planilla.MtoOtg > 0)
                {
                    ingresoVisibleExportacionViewModel.OtrosGastos = (decimal)planilla.MtoOtg;
                }

                if (planilla.MtoLiq > 0)
                {
                    ingresoVisibleExportacionViewModel.MontoLiquido = (decimal)planilla.MtoLiq;
                }

                if (planilla.Mtopar > 0)
                {
                    ingresoVisibleExportacionViewModel.ParidadUSD = (decimal)planilla.Mtopar;
                }

                if (planilla.MtoDol > 0)
                {
                    ingresoVisibleExportacionViewModel.MontoDolar = (decimal)planilla.MtoDol;
                }

                return true;
            }
            return false;
        }

        public ActionResult VerVisibleExportacionAnulada(string NumeroPresentacion, DateTime FechaPresentacion)
        {
            ModelState.Clear();

            var planilla = bl.Sce_Xanu_S02(NumeroPresentacion, FechaPresentacion).FirstOrDefault();
            if (planilla != null)
            {
                var model = new VisibleExportacionAnuladaViewModel()
                {
                    FechaPresentacion = planilla.fecpre,
                    FechaPresentacionNueva = planilla.fecpre,
                    NumeroPresentacion = planilla.numpre.FormatoNroPresentacion(),
                    TipoAnulacion = planilla.tipanu.ToString("00"),
                    CodigoPlazaBancoCentral = (int)planilla.codpbc,
                    NombrePlazaBancoCentral = MODGTAB0.GetNombrePlazaBancoCentral((int)planilla.codpbc, bl),
                    NombreParty = MODGTAB0.GetNombreParty(planilla.prtexp, (int)planilla.indnom, bl),
                    DireccionParty = MODGTAB0.GetDireccionParty(planilla.prtexp, (byte)planilla.inddir, bl),
                    RutExportador = planilla.prtexp.Replace("|", "").PadLeft(9, '0').FormatoRut(),
                    // En código original el campo EntAut era siempre el código del banco
                    CodigoEntidadAutorizadaOriginal = 15,
                    NombreEntidadAutorizadaOriginal = MODGTAB0.GetNombreBanco(15, bl),

                    NumeroPresentacionOriginal = planilla.numpreo,
                    FechaPresentacionOriginal = planilla.fecpreo,
                    CodigoTipoPlanillaOriginal = (int)planilla.tippln,
                    TipoPlanillaOriginal = MODXPLN1.GetNombrePlanilla((int)planilla.tippln),
                    CodigoPlazaBancoCentralOriginal = (int)planilla.plzbcc,
                    NombrePlazaBancoCentralOriginal = MODGTAB0.GetNombrePlazaBancoCentral((int)planilla.plzbcc, bl),
                    MontoDolaresOriginal = planilla.mtodol,
                    ParidadOriginal = planilla.mtopar,

                    // Datos de Declaración
                    CodigoAduana = (int)planilla.codadn,
                    NombreAduana = MODGTAB0.GetNombreAduana(planilla.codadn, bl),
                    NumeroDeclaracion = planilla.numdec,
                    FechaAceptacion = planilla.fecdec != SqlSmallDateTimeMinValue ? planilla.fecdec : (DateTime?)null,
                    FechaVencimientoRetorno = planilla.fecven != SqlSmallDateTimeMinValue ? planilla.fecven : (DateTime?)null,
                    Observaciones = planilla.obspln,

                    // Montos anulación
                    CodigoMonedaAnulacion = MODGTAB0.GetCodigoMonedaBancoCentral((int)planilla.codmnd, bl),
                    MonedaAnulacion = MODGTAB0.GetNombreMoneda((int)planilla.codmnd, bl),
                    MontoAnulado = planilla.mtoanu,
                    MontoAnuladoUS = planilla.mtodola,
                    MontoAnuladoUSParidadOriginal = planilla.mtodolpo,
                    ParidadAnulado = planilla.mtopara,

                    PlanillaGuardada = false
                };
                return View(model);
            }
            throw new Exception("La planilla que está intentando buscar no existe o fue actualizada.");
        }

        public ActionResult ImprimirPlanillaInvisibleExportacion(string numeroPresentacion, DateTime fechaPresentacion)
        {
            var model = bl.ImprimirPlanillaInvisibleExportacion(numeroPresentacion, fechaPresentacion);
            model.Vplis_AnuFec = model.Vplis_AnuFec != "01/01/1900" ? model.Vplis_AnuFec : "";  
            return View(model);
        }

        //Esta es la planilla visible importacion
        public ActionResult ImprimirPlanillaReemplazos(string codigoCentroCosto, string codigoProducto, string codigoEspecialista, string codigoEmpresa, string codigoCobranza, int numeroPlanilla, DateTime fechaVenta)
        {
            var model = bl.ImprimirPlanillaReemplazo(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla, fechaVenta);
            return View(model);
        }

        public ActionResult ImprimirPlanillaAnulada(string numeroPresentacion, DateTime fechaPresentacion)
        {
            var model = bl.ImprimirPlanillaAnulada(numeroPresentacion, fechaPresentacion);
            return View(model);
        }

        public ActionResult ImprimirPlanillaVisibleExportacion(string numeroPresentacion, DateTime fechaPresentacion)
        {
            T_xPlv planilla = GetPlanillaIngresoVisibleExportacion(numeroPresentacion, fechaPresentacion).FirstOrDefault();
            if (planilla.TipPln >= 500)
            {
                var model = bl.ImprimirPlanilla500(planilla);
                return View("ImprimirPlanilla500", model);
            }
            else
            {
                var model = bl.ImpimirPlanilla401(planilla);
                return View("ImprimirPlanilla401", model);
            }
        }        

        public ActionResult VerInvisibleExportacion(string NumeroPresentacion, DateTime FechaPresentacion)
        {
            ModelState.Clear();
            var planilla = bl.Sce_Xpli_S06(NumeroPresentacion, FechaPresentacion);
            if (planilla != null)
            {
                var model = new InvisibleExportacionViewModel()
                {
                    CodigoOperacion = (int?)planilla.codoci,
                    NumeroPlanilla =  planilla.numpli,
                    CodigoPaisOrigen = (int)planilla.codpai,
                    PaisOrigen = MODGTAB0.GetNombrePais((int)planilla.codpai, bl),
                    TipoPlanila = (int)planilla.tippln,
                    FechaPlanilla = planilla.fecpli.Value,
                    FechaAnterior = planilla.fecpli.Value,
                    CodigoPlazaBancoCentral = (int)planilla.plzbcc,
                    PlazaBancoCentral = MODGTAB0.GetNombrePlazaBancoCentral((int)planilla.plzbcc, bl),
                    CodigoMoneda = Convert.ToInt32(planilla.codmndbc),
                    NombreMoneda = MODGTAB0.GetNombreMonedaBancoCentral((int)planilla.codmndbc, bl),
                    RUTCliente = planilla.rutcli.FormatoRut(),
                    NombreParticipante = MODGTAB0.GetNombreParty(planilla.prtcli, (int)planilla.indnom, bl),
                    DireccionParticipante = MODGTAB0.GetDireccionParty(planilla.prtcli, (byte)planilla.inddir, bl),
                    TipoPlanilla = (int)planilla.tippln,
                    CodigoComercio = planilla.codcom,
                    Concepto = planilla.concep,
                    NombreCodigoComercio = MODGTAB0.GetNombreCodigoComercio(planilla.codcom, planilla.concep, bl),
                    NumeroPlanillaAnulada = planilla.anunum,
                    FechaAnulacion = planilla.anufec,
                    PlazaBancoCentralAnulacion = (int)planilla.anupbc,
                    ApcTipo = planilla.apctip,
                    ApcNumero = planilla.apcnum,
                    ApcFecha = planilla.apcfec,
                    ApcPlazaBancoCentral = (int)planilla.apcpbc,
                    Acuerdos = new CommaSeparatedCompactableArray(planilla.desacu),
                    MontoOperacion = Format.FormatCurrency(Convert.ToDouble(planilla.mtoope), "###,##0.00"),
                    MontoParidadBancoCentral = planilla.mtopar,
                    MontoEnDolares = Format.FormatCurrency(Convert.ToDouble(planilla.mtodol), "###,##0.00"),
                    TipoDeCambio = planilla.tipcam,
                    MontoNacional = Format.FormatCurrency(Convert.ToDouble(planilla.mtonac), "###,##0.00"),
                    ZonaFranca = planilla.zonfra.HasValue ? planilla.zonfra.Value : false,
                    DieNumero = planilla.dienum,
                    DieFecha = planilla.diefec,
                    DiePlazaBancoCentral = (int)planilla.diepbc,
                    NumeroDeclaracion = planilla.numdec,
                    FechaDeclaracion = planilla.fecdec,
                    CodigoAduana = (int?)planilla.codadn,
                    CodigoEOR = planilla.codeor,
                    NumeroCredito = (int?)planilla.numcre,
                    FechaCredito = planilla.feccre,
                    MonedaCredito = (int?)planilla.mndcre,
                    MontoCredito = planilla.mtocre,
                    SecBen = planilla.secben,
                    SecInv = planilla.secfin,
                    PorcentajeParticipacion = planilla.prcpar,
                    CodigoAcuerdo = planilla.codacu,
                    NumeroRegistroAcuerdo = planilla.regacu,
                    RutAcuerdo = planilla.rutacu,
                    ConvenioCreditoFechaAutorizacionDebito = planilla.fecdeb,
                    ConvenioCreditoDocumentoNacional = planilla.docnac,
                    ConvenioCreditoDocumentoExtranjero = planilla.docext,
                    ConvenioCreditoBancoExtranjero = (int?)planilla.bcoext,
                    Observaciones = planilla.obspli,
                    PlanillaGuardada = false
                };
                return View(model);
            }
            throw new Exception("La planilla que está intentando buscar no existe o fue actualizada.");
        }

        [HttpPost]
        
        public ActionResult VerInvisibleExportacion(InvisibleExportacionViewModel model)
        {
            if (ModelState.IsValid)
            {
                int? res = GuardarPlanilla(model);

                if(res != null && res != 9)
                {
                    model.PlanillaGuardada = true;
                }
            }
            return View(model);
        }

        [HttpPost]
        
        public ActionResult VerVisibleExportacionAnulada(VisibleExportacionAnuladaViewModel model)
        {
            if (ModelState.IsValid)
            {
                int? res = GuardarPlanilla(model);

                if (res != null && res != -1)
                {
                    model.PlanillaGuardada = true;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult VerIngresoVisibleExportacion(IngresoVisibleExportacionViewModel planilla)
        {
            if (ModelState.IsValid)
            {
                if (planilla.TipoCambio == null)
                {
                    planilla.TipoCambio = 0;
                }
                if (planilla.OtrosGastos == null)
                {
                    planilla.OtrosGastos = 0;
                }

                GuardarPlanilla(planilla);
                planilla.PlanillaGuardada = true;
            }
            return View(planilla);
        }

        private void GuardarPlanilla(IngresoVisibleExportacionViewModel planilla)
        {
            bl.Sce_Xplv_W03(planilla.NumeroPresentacion, planilla.FechaAnterior,
                planilla.FechaPresentacion, planilla.MontoBruto, planilla.Comisiones, (decimal)planilla.OtrosGastos,
                (decimal)planilla.TipoCambio, planilla.Observaciones);
        }

        private int? GuardarPlanilla(InvisibleExportacionViewModel planilla)
        {
            return bl.Sce_Pli_W06(planilla.NumeroPlanilla, planilla.FechaAnterior, planilla.FechaPlanilla, planilla.CodigoPaisOrigen, planilla.Observaciones, planilla.ZonaFranca);
        }

        private int GuardarPlanilla(VisibleExportacionAnuladaViewModel planilla)
        {
            return bl.Sce_Xanu_U03(planilla.NumeroPresentacion.Replace("-",""), planilla.FechaPresentacion.Value, planilla.FechaPresentacionNueva.Value, planilla.Observaciones);
        }

        private void GuardarPlanilla(VisibleImportacionViewModel planilla)
        {
            //Guardar Planilla de Importación.
            PlanillaVisibleImportacionDTO planillaDTO = new PlanillaVisibleImportacionDTO()
            {
                CodigoCentroCosto = planilla.CodigoCentroCosto!= null ? planilla.CodigoCentroCosto : "",
                CodigoProducto = planilla.CodigoProducto!= null ? planilla.CodigoProducto :"",
                CodigoEspecialista = planilla.CodigoEspecialista != null ? planilla.CodigoEspecialista : "",
                CodigoEmpresa = planilla.CodigoEmpresa != null ? planilla.CodigoEmpresa :"",
                CodigoCobranza = planilla.CodigoCobranza!= null ? planilla.CodigoCobranza : "",
                NumeroPlanilla = planilla.NumeroPresentacion,
                FechaVentaAntigua = planilla.FechaVentaAntigua.GetValueOrDefault(SqlSmallDateTimeMinValue),
                FechaVenta = planilla.FechaPlanilla,
                NumeroConocimientoEmbarque = planilla.NumeroConocimientoEmbarque!= null ? planilla.NumeroConocimientoEmbarque:"",
                FechaConocimientoEmbarque = planilla.FechaConocimientoEmbarque.GetValueOrDefault(SqlSmallDateTimeMinValue),
                FormaPago = planilla.FormaPago,
                CodigoPais = planilla.CodigoPaisPago,
                NombrePais = planilla.PaisPago,
                FechaVencimiento = planilla.FechaVencimiento.GetValueOrDefault(SqlSmallDateTimeMinValue),
                HayCuadroCuotas = planilla.HayCuadroCuotas.GetValueOrDefault(false),
                NumeroCuotas = planilla.NumeroCuotas.GetValueOrDefault(0),
                Cuota = planilla.Cuota.GetValueOrDefault(0),
                HayAcuerdos = planilla.HayAcuerdos,
                NumeroAcuerdos = planilla.NumeroAcuerdos.GetValueOrDefault(0),
                AcuerdoDesde = planilla.AcuerdoDesde == null ? "" : planilla.AcuerdoDesde,
                AcuerdoHasta = planilla.AcuerdoHasta == null ? "" : planilla.AcuerdoHasta,
                FechaAutorizacionDebito = planilla.FechaAutorizacionDebito.GetValueOrDefault(SqlSmallDateTimeMinValue),
                NumeroDocumentoChile = planilla.NumeroDocumentoChile == null ? "" : planilla.NumeroDocumentoChile,
                NumeroDocumentoExtranjero = planilla.NumeroDocumentoExtranjero == null ? "" : planilla.NumeroDocumentoExtranjero,
                Observaciones = planilla.Observaciones == null ? "" : planilla.Observaciones,
                isZonaFranca = planilla.isZonaFranca
              
            };


            bl.Sce_Plan_U12(planillaDTO);
        }

        [Obsolete]
        public ActionResult VerEstadisticaImportacion(string codigoCentroCosto, string codigoProducto, string codigoEspecialista, string codigoEmpresa, string codigoCobranza, int numeroPlanilla, DateTime fechaVenta)
        {
            var planilla = bl.GetPlanillaImportacion(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla, fechaVenta).FirstOrDefault();

            var planilla2 = bl.GetPlanillaEstadisticaImportacion(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla).FirstOrDefault();

            var intereses = bl.GetIntereses(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla);
            if (planilla != null)
            {
                var model = new EstadisticaImportacionViewModel()
                {
                    NumeroPresentacion = planilla.num_presen,
                    CodigoPlanilla = planilla.codigo,
                    FechaVenta = planilla.fechaventa,
                    CodigoPlazaBancoCentral = planilla.cod_plaza,
                    NombreImportador = planilla.nomimport,
                    RutImportador = planilla.rut.PadLeft(9, '0').FormatoRut(),
                    NumeroInforme = planilla.num_idi,
                    FechaInforme = planilla.fec_idi,
                    FormaPago = planilla.forma_pag,
                    FormasPago = GetFormasPago(),
                    CodigoPaisPago = planilla.codpais,
                    PaisPago = MODGTAB0.GetNombrePais((int)planilla.codpais, bl),
                    PlazaEmision = planilla.cod_plaza,
                    PlazasEmision = GetPlazasEmision(),
                    CodigoMoneda = planilla.codmone,
                    NumeroConocimientoEmbarque = planilla.num_con,
                    FechaConocimientoEmbarque = planilla.fec_con,
                    FechaVencimiento = planilla.fechavenc,
                    Moneda = MODGTAB0.GetNombreMoneda((int)planilla.codmone, bl),
                    ValorMercaderia = planilla.mercaderia,
                    GastosHastaFOB = planilla.hasta_fob,
                    FOB = planilla.mtofob,
                    Flete = planilla.mtoflete,
                    Seguro = planilla.mtoseguro,
                    CIF = planilla.mtocif,
                    Intereses = planilla.mtointer,
                    GastosBancarios = planilla.mtogastos,
                    ValorTotal = planilla.mtototal,
                    CIFEnDolares = planilla.cifdolar,
                    TotalEnDolares = planilla.totaldolar,
                    TipoCambio = planilla.tipo_camb,
                    ParidadUSD = planilla.paridad,

                    HayCuadroCuotas = planilla2.haycuadro,
                    NumeroCuotas = planilla2.numcuadro,
                    Cuota = planilla2.numcuota,
                    NumeroAcuerdos = planilla2.numacuerdo,
                    AcuerdoDesde = planilla2.acuerdo1,
                    AcuerdoHasta = planilla2.acuerdo2,
                    UsaConvenio = planilla2.hayacuerdo,
                    FechaAutorizacionDebito = planilla2.fecdebito,
                    NumeroDocumentoChile = planilla2.ndoc1,
                    NumeroDocumentoExtranjero = planilla2.ndoc2,
                    isPlanillaReemplazo = planilla2.hayrpl > 0 ? true : false,

                };

                if (model.isPlanillaReemplazo)
                {
                    model.ReemplazoNumeroPresentacion = planilla2.numpln_r;
                    model.ReemplazoFecha = planilla2.fecpln_r;
                    model.ReemplazoCodigoPlazaBancoCentral = planilla2.codpln_r;
                    model.ReemplazoCodigoEntidad = planilla2.codent_r;
                    model.ReemplazoNumeroInforme = planilla2.numinf_r;
                    model.ReemplazoFechaInforme = planilla2.fecinf_r;
                    model.ReemplazoPlazaEmision = planilla2.plzinf_r;
                    model.ReemplazoNumeroConocimientoEmbarque = planilla2.numcon_r;
                    model.ReemplazoFechaConocimientoEmbarque = planilla2.feccon_r;

                }

                ViewBag.FormaPago = GetFormasPago();
                return View(model);
            }
            throw new Exception("La planilla que está intentando buscar no existe o fue actualizada.");
        }

        private IEnumerable<SelectListItem> GetPlazasEmision()
        {
            return new SelectList(MOD_PLAV.GetPlazasBancoCentral(bl), "Key", "Value");
        }

        private IEnumerable<SelectListItem> GetFormasPago()
        {
            return new SelectList(MOD_PLAV.GetFormasPago(bl), "fdpcod", "fdpnom");
        }

        public ActionResult VerVisibleImportacion(string codigoCentroCosto, string codigoProducto, string codigoEspecialista, string codigoEmpresa, string codigoCobranza, int numeroPlanilla, DateTime fechaVenta)
        {
            var planilla = bl.GetPlanillaImportacion(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla, fechaVenta).FirstOrDefault();

            var planilla2 = bl.GetPlanillaVisibleImportacion(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla, fechaVenta).FirstOrDefault();

            if (planilla != null)
            {
                var model = new VisibleImportacionViewModel()
                {
                    CodigoCentroCosto = codigoCentroCosto,
                    CodigoProducto = codigoProducto,
                    CodigoEspecialista = codigoEspecialista,
                    CodigoEmpresa = codigoEmpresa,
                    CodigoCobranza = codigoCobranza,
                    NumeroPresentacion = planilla.num_presen,
                    CodigoPlanilla = planilla.codigo,
                    FechaPlanilla = planilla.fechaventa,
                    FechaVentaAntigua = planilla.fechaventa,
                    CodigoPlazaBancoCentral = planilla.cod_plaza,
                    NombreImportador = planilla.nomimport,
                    RutImportador = planilla.rut.PadLeft(9, '0').FormatoRut(),
                    NumeroInforme = planilla.num_idi,
                    FechaInforme = planilla.fec_idi,
                    FormaPago = planilla.forma_pag,
                    FormasPago = GetFormasPago(),
                    CodigoPaisPago = planilla.codpais,
                    PaisPago = MODGTAB0.GetNombrePais((int)planilla.codpais, bl),
                    PlazaEmision = planilla.cod_plaza,
                    PlazasEmision = GetPlazasEmision(),
                    CodigoMoneda = planilla.codmone,
                    NumeroConocimientoEmbarque = planilla.num_con.Trim(),
                    FechaConocimientoEmbarque = planilla.fec_con,
                    FechaVencimiento = planilla.fechavenc,
                    Moneda = MODGTAB0.GetNombreMoneda((int)planilla.codmone, bl),
                    ValorMercaderia = planilla.mercaderia,
                    GastosHastaFOB = planilla.hasta_fob,
                    FOB = Format.FormatCurrency(Convert.ToDouble(planilla.mtofob), "###,##0.00"),
                    Flete = Format.FormatCurrency(Convert.ToDouble(planilla.mtoflete), "###,##0.00"),
                    Seguro = Format.FormatCurrency(Convert.ToDouble(planilla.mtoseguro), "###,##0.00"),
                    CIF = Format.FormatCurrency(Convert.ToDouble(planilla.mtocif), "###,##0.00"),
                    Intereses = planilla.mtointer,
                    GastosBancarios = planilla.mtogastos,
                    ValorTotal = Format.FormatCurrency(Convert.ToDouble(planilla.mtototal), "###,##0.00"),
                    CIFEnDolares = Format.FormatCurrency(Convert.ToDouble(planilla.cifdolar), "###,##0.00"),
                    TotalEnDolares = Format.FormatCurrency(Convert.ToDouble(planilla.totaldolar), "###,##0.00"),
                    TipoCambio = planilla.tipo_camb,
                    ParidadUSD = planilla.paridad,
                    ObservacionesDeclaracion = planilla2.obsdecl,

                    isZonaFranca = planilla2.zonfra.HasValue ? (bool)planilla2.zonfra : false,
                    HayCuadroCuotas = planilla2.haycuadro,
                    NumeroCuotas = planilla2.numcuadro,
                    Cuota = planilla2.numcuota,
                    NumeroAcuerdos = planilla2.numacuerdo,
                    AcuerdoDesde = planilla2.acuerdo1,
                    AcuerdoHasta = planilla2.acuerdo2,
                    UsaConvenio = planilla2.hayacuerdo.HasValue ? (bool)planilla2.hayacuerdo : false,
                    FechaAutorizacionDebito = planilla2.fecdebito,
                    NumeroDocumentoChile = planilla2.ndoc1,
                    NumeroDocumentoExtranjero = planilla2.ndoc2,
                    isPlanillaReemplazo = planilla2.hayrpl > 0 ? true : false,
                    isAnulada = planilla2.hayanula.HasValue ? (bool)planilla2.hayanula : false,

                    PlanillaGuardada = false
                };

                if (model.isPlanillaReemplazo)
                {
                    model.ReemplazoNumeroPresentacion = planilla2.numpln_r;
                    model.ReemplazoFecha = planilla2.fecpln_r;
                    model.ReemplazoCodigoPlazaBancoCentral = planilla2.codpln_r;
                    model.ReemplazoCodigoEntidad = planilla2.codent_r;
                    model.ReemplazoNumeroInforme = planilla2.numinf_r;
                    model.ReemplazoFechaInforme = planilla2.fecinf_r;
                    model.ReemplazoPlazaEmision = planilla2.plzinf_r;
                    model.ReemplazoNumeroConocimientoEmbarque = planilla2.numcon_r;
                    model.ReemplazoFechaConocimientoEmbarque = planilla2.feccon_r;
                }

                if (model.isAnulada)
                {
                    model.FechaAnulacion = planilla2.fechaanula;
                    model.ParidadUSDAnulacion = planilla2.paranula;
                    model.TotalAnulacion = planilla2.totalanula;
                }

                model.DetalleIntereses = GetDetalleIntereses(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla);

                model.Paises = GetPaises().OrderBy(i => i.Text).ToList();
                model.Observaciones = planilla2.observ;

                return View(model);
            }
            throw new Exception("La planilla que está intentando buscar no existe o fue actualizada.");
        }

        [HttpPost]
        public ActionResult VerVisibleImportacion(VisibleImportacionViewModel planilla)
        {
            planilla.HayCuadroCuotas = (planilla.NumeroCuotas != null && planilla.Cuota != null);
            planilla.HayAcuerdos = planilla.NumeroAcuerdos != 0;
            planilla.FormasPago = GetFormasPago();
            planilla.PlazasEmision = GetPlazasEmision();
            planilla.Paises = GetPaises();
            planilla.PaisPago = MODGTAB0.GetNombrePais((int)planilla.CodigoPaisPago, bl);

            if (ModelState.IsValid)
            {
                GuardarPlanilla(planilla);
                planilla.PlanillaGuardada = true;
            }
            return View(planilla);
        }

        private void ActualizaIntereses(VisibleImportacionViewModel planilla)
        {
            bl.Sce_Inpl_D01(planilla.CodigoCentroCosto, planilla.CodigoProducto,
                planilla.CodigoEspecialista, planilla.CodigoEmpresa, planilla.CodigoCobranza, planilla.NumeroPresentacion);

            bl.Sce_Inpl_W01(planilla.CodigoCentroCosto, planilla.CodigoProducto, planilla.CodigoEspecialista,
                planilla.CodigoEmpresa, planilla.CodigoCobranza, planilla.NumeroPresentacion, planilla.FechaPlanilla,
                planilla.DetalleIntereses.NumeroLineaInteres, planilla.DetalleIntereses.Concepto,
                planilla.DetalleIntereses.Tipo, planilla.DetalleIntereses.Monto, planilla.DetalleIntereses.CapitalBase,
                planilla.DetalleIntereses.CodigoBaseAno, planilla.DetalleIntereses.TasaInteres,
                planilla.DetalleIntereses.FechaInicial, planilla.DetalleIntereses.FechaFinal,
                planilla.DetalleIntereses.NumeroDias);
        }

        private IEnumerable<SelectListItem> GetPaises()
        {
            return new SelectList(MODGTAB0.GetPaises(bl), "pai_paicod", "pai_painom");
        }

        private ActionResult RedirectToListado()
        {
            return RedirectToAction("Volver", "Planillas");
        }
        private InteresesPlanilla GetDetalleIntereses(string codigoCentroCosto, string codigoProducto, string codigoEspecialista, string codigoEmpresa, string codigoCobranza, int numeroPlanilla)
        {
            return bl.GetIntereses(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla).Select(
                                    i => new InteresesPlanilla()
                                    {
                                        NumeroPlanilla = i.numplan,
                                        FechaPlanilla = i.fecha,
                                        NumeroLineaInteres = i.nro,
                                        Concepto = i.concepto,
                                        Tipo = i.tipo,
                                        CapitalBase = i.capbas,
                                        CodigoBaseAno = i.codbas,
                                        TasaInteres = i.tasa,
                                        FechaInicial = i.fini,
                                        FechaFinal = i.ffin,
                                        NumeroDias = i.ndias,
                                        Monto = i.monto
                                    }).FirstOrDefault();

        }

    }
}
