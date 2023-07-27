using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BCH.Comex.Common;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.Core.BL.XGPL;
using BCH.Comex.UI.Web.Helpers;
using BCH.Comex.UI.Web.Areas.Planillas.Models;
using BCH.Comex.Utils;
using BCH.Comex.UI.Web.Helpers.Extensions;
using BCH.Comex.Core.Entities.Portal;


namespace BCH.Comex.UI.Web.Areas.Impresion.Controllers
{
    public class PlanillasController : BaseController
    {
        private static readonly DateTime SqlSmallDateTimeMinValue = new DateTime(1900, 01, 01, 00, 00, 00);
        private XgplService bl
        {
            get
            {
                return new XgplService(HttpContext.GetCurrentUser().GetDatosUsuario());
            }
        }

        //
        // GET: /Impresion/ImpresionPlanillas/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        #region IMPRESION DE DOCUMENTOS
        public ActionResult ImprimirPlanillaInvisibleExportacion(string numeroPresentacion, DateTime fechaPresentacion, PrintFormat? format = null)
        {
            var model = bl.ImprimirPlanillaInvisibleExportacion(numeroPresentacion, fechaPresentacion);
            model.Vplis_AnuFec = model.Vplis_AnuFec != "01/01/1900" ? model.Vplis_AnuFec : "";
            //return View(model);
            return this.AlternateOutput(format, "ImprimirPlanillaInvisibleExportacion", model)
                ?? View(model);
        }

        //Esta es la planilla visible importacion
        public ActionResult ImprimirPlanillaReemplazos(string codigoCentroCosto, string codigoProducto, string codigoEspecialista, string codigoEmpresa, string codigoCobranza, int numeroPlanilla, DateTime fechaVenta, PrintFormat? format = null)
        {
            var model = bl.ImprimirPlanillaReemplazo(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla, fechaVenta);
            //return View(model);
            return this.AlternateOutput(format, "ImprimirPlanillaReemplazos", model)
                ?? View(model);
        }

        public ActionResult ImprimirPlanillaAnulada(string numeroPresentacion, DateTime fechaPresentacion, PrintFormat? format = null)
        {
            var model = bl.ImprimirPlanillaAnulada(numeroPresentacion, fechaPresentacion);
            //return View(model);
            return this.AlternateOutput(format, "ImprimirPlanillaAnulada", model)
                ?? View(model);
        }

        public ActionResult ImprimirPlanillaVisibleExportacion(string numeroPresentacion, DateTime fechaPresentacion, PrintFormat? format = null)
        {
            T_xPlv planilla = GetPlanillaIngresoVisibleExportacion(numeroPresentacion, fechaPresentacion).FirstOrDefault();
            if (planilla.TipPln >= 500)
            {
                var model = bl.ImprimirPlanilla500(planilla);
                //return View("ImprimirPlanilla500", model);
                return this.AlternateOutput(format, "ImprimirPlanilla500", model)
                ?? View("ImprimirPlanilla500", model);
            }
            else
            {
                var model = bl.ImpimirPlanilla401(planilla);
                //return View("ImprimirPlanilla401", model);
                return this.AlternateOutput(format, "ImprimirPlanilla401", model)
                ?? View("ImprimirPlanilla401", model);
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
                    NumeroPlanilla = planilla.numpli,
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
                    Observaciones = planilla.obspli
                };
                return View(model);
            }
            return HttpNotFound();
        }
        
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

        #endregion


    }
}