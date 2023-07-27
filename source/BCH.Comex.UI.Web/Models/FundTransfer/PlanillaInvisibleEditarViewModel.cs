using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using System;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class PlanillaInvisibleEditarViewModel
    {

        public string codComercio { get; set; }
        public string nroPlanilla { get; set; }
        public string codigo1 { get; set; }
        public string plazaBanco { get; set; }
        public string codPais { get; set; }
        public string paisOrigen { get; set; }
        public string nombreParticipante { get; set; }
        public string tipoPlanilla { get; set; }
        public string fechaPlanilla { get; set; }
        public string codBcoPlzCentral { get; set; }
        public string rutParticipante { get; set; }
        public string direccionParticipante { get; set; }
        public string nombreMoneda { get; set; }
        public string codMoneda { get; set; }
        public string nombreComercio { get; set; }
        public string codOperacion { get; set; }
        public string conceptoOperacion { get; set; }
        public string montoOperacion { get; set; }
        public string paridadBcoC { get; set; }
        public string montoUSD { get; set; }
        public string montoPesosTC { get; set; }
        public string montoTotal { get; set; }
        public string numPlanilla { get; set; }
        public string fechaAnul { get; set; }
        public string sucBCCHAnuPbc { get; set; }
        public string tipoPlanillaAnul { get; set; }
        public string numeroPlanillaAnul { get; set; }
        public string fechaApc { get; set; }
        public string sucBCCHApcPbc { get; set; }
        public string cantidad1 { get; set; }
        public string cantidad2 { get; set; }
        public string cantidad3 { get; set; }
        public string cantidad4 { get; set; }
        public string cantidad5 { get; set; }
        public string cantidad6 { get; set; }
        public string FecechaDebConvenio { get; set; }
        public string nroDocumentoCh { get; set; }
        public string nroDocumentoExt { get; set; }
        public string codBcoExt { get; set; }
        public string nroExportacion { get; set; }
        public string fechaExportacion { get; set; }
        public string plazaBCCHExportacion { get; set; }
        public string nroAceptacion { get; set; }
        public string fechaAceptacion { get; set; }
        public string codAduana { get; set; }
        public string CodEOR { get; set; }
        public string numCredito { get; set; }
        public string fechaDesembolso { get; set; }
        public string codMonedaDesembolso { get; set; }
        public string montoEquivalente { get; set; }
        public string secEcBen { get; set; }
        public string secEc { get; set; }
        public string porcentajePart { get; set; }
        public string acuerdoCorrespA { get; set; }
        public string nroRegistro { get; set; }
        public string rutDeudorOriginal { get; set; }
        public string observaciones { get; set; }
        public bool zonaFranca { get; set; }
        public UI_Button adelante { get; set; }
        public UI_Button atras { get; set; }
        public UI_Button aceptar { get; set; }
        public UI_Button cancelar { get; set; }
        public bool fechaPlanillaEnabled { get; set; }
        public bool observacionesEnabled { get; set; }
        public bool zonaFrancaEnabled { get; set; }

        public PlanillaInvisibleEditarViewModel()
        {
        }

        public PlanillaInvisibleEditarViewModel(InitializationObject iniObj)
        {
            using (var trace = new Tracer("PlanillaInvisibleEditarViewModel"))
            {
                try
                {
                    codComercio = iniObj.Frm_Pln_Invisible.Tx_Planilla[0].Text;
                    nroPlanilla = iniObj.Frm_Pln_Invisible.Tx_Planilla[1].Text;

                    codigo1 = "15";
                    plazaBanco = iniObj.Frm_Pln_Invisible.Tx_Planilla[7].Text;
                    codPais = iniObj.Frm_Pln_Invisible.Tx_Planilla[4].Text;
                    paisOrigen = iniObj.Frm_Pln_Invisible.Tx_Planilla[3].Text;

                    tipoPlanilla = iniObj.Frm_Pln_Invisible.Tx_Planilla[5].Text;
                    fechaPlanilla = iniObj.Frm_Pln_Invisible.Tx_Planilla[6].Text;
                    codBcoPlzCentral = iniObj.Frm_Pln_Invisible.Tx_Planilla[8].Text;

                    rutParticipante = iniObj.Frm_Pln_Invisible.Tx_Planilla[11].Text;
                    nombreParticipante = iniObj.Frm_Pln_Invisible.Tx_Planilla[12].Text;
                    direccionParticipante = iniObj.Frm_Pln_Invisible.Tx_Planilla[14].Text;
                    nombreMoneda = iniObj.Frm_Pln_Invisible.Tx_Planilla[9].Text;
                    codMoneda = iniObj.Frm_Pln_Invisible.Tx_Planilla[10].Text;
                    nombreComercio = iniObj.Frm_Pln_Invisible.Tx_Planilla[16].Text;
                    codOperacion = iniObj.Frm_Pln_Invisible.Tx_Planilla[17].Text;
                    conceptoOperacion = iniObj.Frm_Pln_Invisible.Tx_Planilla[18].Text;

                    montoOperacion = iniObj.Frm_Pln_Invisible.Tx_Planilla[13].Text;
                    paridadBcoC = iniObj.Frm_Pln_Invisible.Tx_Planilla[15].Text;
                    montoUSD = iniObj.Frm_Pln_Invisible.Tx_Planilla[19].Text;
                    montoPesosTC = iniObj.Frm_Pln_Invisible.Tx_Planilla[23].Text;
                    montoTotal = iniObj.Frm_Pln_Invisible.Tx_Planilla[28].Text;

                    numPlanilla = iniObj.Frm_Pln_Invisible.Tx_Planilla[20].Text;
                    fechaAnul = iniObj.Frm_Pln_Invisible.Tx_Planilla[21].Text;
                    sucBCCHAnuPbc = iniObj.Frm_Pln_Invisible.Tx_Planilla[22].Text;
                    tipoPlanillaAnul = iniObj.Frm_Pln_Invisible.Tx_Planilla[24].Text;
                    numeroPlanillaAnul = iniObj.Frm_Pln_Invisible.Tx_Planilla[25].Text;
                    fechaApc = iniObj.Frm_Pln_Invisible.Tx_Planilla[26].Text;
                    sucBCCHApcPbc = iniObj.Frm_Pln_Invisible.Tx_Planilla[27].Text;

                    cantidad1 = iniObj.Frm_Pln_Invisible.Tx_Planilla[48].Text;
                    cantidad2 = iniObj.Frm_Pln_Invisible.Tx_Planilla[49].Text;
                    cantidad3 = iniObj.Frm_Pln_Invisible.Tx_Planilla[47].Text;
                    cantidad4 = iniObj.Frm_Pln_Invisible.Tx_Planilla[50].Text;
                    cantidad5 = iniObj.Frm_Pln_Invisible.Tx_Planilla[51].Text;
                    cantidad6 = iniObj.Frm_Pln_Invisible.Tx_Planilla[52].Text;

                    FecechaDebConvenio = iniObj.Frm_Pln_Invisible.Tx_Planilla[32].Text;
                    nroDocumentoCh = iniObj.Frm_Pln_Invisible.Tx_Planilla[33].Text;
                    nroDocumentoExt = iniObj.Frm_Pln_Invisible.Tx_Planilla[37].Text;
                    codBcoExt = iniObj.Frm_Pln_Invisible.Tx_Planilla[38].Text;

                    nroExportacion = iniObj.Frm_Pln_Invisible.Tx_Planilla[29].Text;
                    fechaExportacion = iniObj.Frm_Pln_Invisible.Tx_Planilla[30].Text;
                    plazaBCCHExportacion = iniObj.Frm_Pln_Invisible.Tx_Planilla[31].Text;

                    nroAceptacion = iniObj.Frm_Pln_Invisible.Tx_Planilla[34].Text;
                    fechaAceptacion = iniObj.Frm_Pln_Invisible.Tx_Planilla[35].Text;
                    codAduana = iniObj.Frm_Pln_Invisible.Tx_Planilla[36].Text;
                    CodEOR = iniObj.Frm_Pln_Invisible.Tx_Planilla[53].Text;

                    numCredito = iniObj.Frm_Pln_Invisible.Tx_Planilla[39].Text;
                    fechaDesembolso = iniObj.Frm_Pln_Invisible.Tx_Planilla[40].Text;
                    codMonedaDesembolso = iniObj.Frm_Pln_Invisible.Tx_Planilla[41].Text;
                    montoEquivalente = iniObj.Frm_Pln_Invisible.Tx_Planilla[42].Text;
                    secEcBen = iniObj.Frm_Pln_Invisible.Tx_SecBen.Text;
                    secEc = iniObj.Frm_Pln_Invisible.Tx_SecInv.Text;
                    porcentajePart = iniObj.Frm_Pln_Invisible.Tx_PrcPar.Text;

                    acuerdoCorrespA = iniObj.Frm_Pln_Invisible.Tx_Planilla[44].Text;
                    nroRegistro = iniObj.Frm_Pln_Invisible.Tx_Planilla[45].Text;
                    rutDeudorOriginal = iniObj.Frm_Pln_Invisible.Tx_Planilla[46].Text;
                    observaciones = iniObj.Frm_Pln_Invisible.Tx_Planilla[43].Text;
                    zonaFranca = Convert.ToBoolean(iniObj.Frm_Pln_Invisible.Ch_ZonFra.Value);

                    adelante = iniObj.Frm_Pln_Invisible.Adelante;
                    atras = iniObj.Frm_Pln_Invisible.Atras;
                    aceptar = iniObj.Frm_Pln_Invisible.Boton[0];
                    cancelar = iniObj.Frm_Pln_Invisible.Boton[1];
                    fechaPlanillaEnabled = iniObj.Frm_Pln_Invisible.Tx_Planilla[6].Enabled;
                    observacionesEnabled = iniObj.Frm_Pln_Invisible.Tx_Planilla[43].Enabled;
                    zonaFrancaEnabled = iniObj.Frm_Pln_Invisible.Ch_ZonFra.Enabled;
                }
                catch (Exception ex)
                {
                    trace.TraceException("Alerta",ex);
                    throw;
                }
            }
        }

        internal void Update(InitializationObject iniObj, UI_Frm_Pln_Invisible uI_Frm_Pln_Invisible)
        {
            using (var trace = new Tracer("PlanillaInvisibleEditarViewModel - Update"))
            {
                try
                {
                    iniObj.Frm_Pln_Invisible.Tx_Planilla[4].Text = codPais;
                    iniObj.Frm_Pln_Invisible.Tx_Planilla[43].Text = observaciones;
                    iniObj.Frm_Pln_Invisible.Tx_Planilla[6].Text = fechaPlanilla;
                    iniObj.Frm_Pln_Invisible.Ch_ZonFra.Value = Convert.ToInt16(zonaFranca);
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