using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class PlanillaVisibleExportViewModel : FundTransferViewModel
    {
        [Display(Name = "Número")]
        public UI_TextBox TxtNumeroDeclaracion { get; set; }
        [Display(Name = "Fecha")]
        public UI_TextBox TxtFechaDeclaracion { get; set; }
        [Display(Name = "Adn")]
        public UI_TextBox TxtCodigoDeclaracion { get; set; }
        [Display(Name = "Trn. Varias")]
        public UI_TextBox TxtValTranVariasDeclaracion { get; set; }
        public UI_Button BtnOkDeclaracion { get; set; }
        [Display(Name = "V. Bruto")]
        public UI_TextBox TxtValBrutoDeclaracion { get; set; }
        [Display(Name = "Comisiones")]
        public UI_TextBox TxtValComisionDeclaracion { get; set; }
        [Display(Name = "Flete")]
        public UI_TextBox TxtValFleteDeclaracion { get; set; }
        [Display(Name = "Seguro")]
        public UI_TextBox TxtValSeguroDeclaracion { get; set; }
        [Display(Name = "G. Corresp.")]
        public UI_TextBox TxtValGastoCorredorDeclaracion { get; set; }
        [Display(Name = "Ajuste V. Ret")]
        public UI_TextBox TxtValAjusteDeclaracion { get; set; }
        [Display(Name = "V. Líquido")]
        public UI_TextBox TxtValLiquidoDeclaracion { get; set; }
        [Display(Name = "Total Compra")]
        public UI_TextBox TxtValTotalCompraDeclaracion { get; set; }
        [Display(Name = "")]
        public UI_CheckBox ChkValComisionesDeclaracion { get; set; }
        [Display(Name = "")]
        public UI_CheckBox ChkValFleteDeclaracion { get; set; }
        [Display(Name = "")]
        public UI_CheckBox ChkValSeguroDeclaracion { get; set; }
        [Display(Name = "Sec. Ec. Ben.")]
        public UI_Combo CmbSecEcBen { get; set; }
        [Display(Name = "Sec. Ec. Inv.")]
        public UI_Combo CmbSecEcInv { get; set; }
        [Display(Name = "% Participac.")]
        public UI_TextBox TxtValPorcentParticipante { get; set; }
        [Display(Name = "Nombre del Comprador")]
        public UI_TextBox TxtNombreComprador { get; set; }
        [Display(Name = "Total Plns")]
        public UI_TextBox TxtValTotalPlanilla { get; set; }
        [Display(Name = "Tipo Pln.")]
        public UI_Combo CmbTipoPlanilla { get; set; }
        [Display(Name = "V. Bruto")]
        public UI_TextBox TxtValBrutoPlanilla { get; set; }
        [Display(Name = "Comisiones")]
        public UI_TextBox TxtValComisionPlanilla { get; set; }
        [Display(Name = "Otr. Gastos")]
        public UI_TextBox TxtOtrosGastosPlanilla { get; set; }
        [Display(Name = "V. Líquido")]
        public UI_TextBox TxtValLiquidoPlanilla { get; set; }
        [Display(Name = "T/Cambio")]
        public UI_TextBox TxtTipoCambioPlanilla { get; set; }
        [Display(Name = "Vto. Retorno")]
        public UI_TextBox TxtFecVencRetornoPlanilla { get; set; }
        [Display(Name = "Observaciones")]
        public UI_TextBox TxtObservacionesPlanilla { get; set; }
        [Display(Name = "Plazo Financiamiento")]
        public UI_TextBox TxtPlazoFinanciamiento { get; set; }
        [Display(Name = "Número Presentación")]
        public UI_TextBox TxtNumPresentacion { get; set; }
        [Display(Name = "Fecha Presentación")]
        public UI_TextBox TxtFechaPresentacion { get; set; }
        [Display(Name = "Bancos Comerciales")]
        public UI_Combo CmbBanco { get; set; }
        [Display(Name = "Plaza Banco Central")]
        public UI_Combo CmbPlazaBancoCentral { get; set; }
        [Display(Name = "Tipo de Planilla")]
        public UI_Combo CmbTipoPlanillaInformar { get; set; }
        [Display(Name = "Fecha Inscripción")]
        public UI_TextBox TxtFechaInscripcion { get; set; }
        [Display(Name = "Nº Inscripción")]
        public UI_TextBox TxtNumeroInscripcion { get; set; }
        [Display(Name = "País de la Operación")]
        public UI_Combo CmbPaisOperacion { get; set; }
        [Display(Name = "Montos")]
        public UI_ListBox LstMontos { get; set; }
        [Display(Name = "Planillas/#Dec./IDE/Mnd./Monto")]
        public UI_ListBox LstPlanillas { get; set; }
        public UI_Button BtnAceptar { get; set; }
        public UI_Button BtnCancelar { get; set; }
        public UI_Button BtnVisualizar { get; set; }
        public UI_Button BtnOK { get; set; }
        public UI_Button BtnNO { get; set; }


        public PlanillaVisibleExportViewModel() { }

        public PlanillaVisibleExportViewModel(UI_FrmxPln0 frm, List<UI_Message> errores)
        {
            TxtNumeroDeclaracion = frm.Tx_NumDec;
            TxtFechaDeclaracion = frm.Tx_FecDec;
            TxtCodigoDeclaracion = frm.Tx_CodAdn;
            BtnOkDeclaracion = frm.Ok_Dec;
            TxtValBrutoDeclaracion = frm.Tx_MtoDec[0];
            TxtValComisionDeclaracion = frm.Tx_MtoDec[1];
            TxtValFleteDeclaracion = frm.Tx_MtoDec[2];
            TxtValSeguroDeclaracion = frm.Tx_MtoDec[3];
            TxtValGastoCorredorDeclaracion = frm.Tx_MtoDec[4];
            TxtValAjusteDeclaracion = frm.Tx_MtoDec[5];
            TxtValLiquidoDeclaracion = frm.Tx_MtoDec[6];
            TxtValTranVariasDeclaracion = frm.Tx_MtoDec[7];
            TxtValTotalCompraDeclaracion = frm.Tx_MtoDec[8];
            ChkValComisionesDeclaracion = frm.Ch_Deduc[0];
            ChkValFleteDeclaracion = frm.Ch_Deduc[1];
            ChkValSeguroDeclaracion = frm.Ch_Deduc[2];
            CmbSecEcBen = frm.Cb_SecEcBen;
            CmbSecEcInv = frm.Cb_SecEcIn;
            TxtValPorcentParticipante = frm.Tx_PrcPar;
            TxtNombreComprador = frm.Tx_NomCom;

            TxtValTotalPlanilla = frm.Tx_MtoPln[0];
            CmbTipoPlanilla = frm.LtTPln;
            TxtValBrutoPlanilla = frm.Tx_MtoPln[1];
            TxtValComisionPlanilla = frm.Tx_MtoPln[2];
            TxtOtrosGastosPlanilla = frm.Tx_MtoPln[3];
            TxtValLiquidoPlanilla = frm.Tx_MtoPln[4];
            TxtTipoCambioPlanilla = frm.Tx_MtoPln[5];
            TxtFecVencRetornoPlanilla = frm.Tx_FecVen;
            TxtObservacionesPlanilla = frm.Tx_Obs;

            TxtPlazoFinanciamiento = frm.Tx_PlzFin;
            TxtNumPresentacion = frm.Tx_NumPre;
            TxtFechaPresentacion = frm.Tx_FecPre;
            CmbBanco = frm.Cb_Bco;
            CmbPlazaBancoCentral = frm.Cb_Pbc;
            CmbTipoPlanillaInformar = frm.Cb_Tippln;
            TxtFechaInscripcion = frm.Tx_Fecha;
            TxtNumeroInscripcion = frm.Tx_NumIns;
            CmbPaisOperacion = frm.Cb_Pais;

            LstMontos = frm.LtMto;
            LstMontos.Items.ForEach(x => x.Value = x.Value.Replace(" ", "\xA0").Replace("\t", "\xA0\xA0\xA0"));
            LstPlanillas = frm.LtPln;
            LstPlanillas.Items.ForEach(x => x.Value = x.Value.Replace(" ", "\xA0").Replace("\t", "\xA0\xA0\xA0"));

            BtnAceptar = frm.Boton[0];
            BtnVisualizar = frm.Boton[1];
            BtnCancelar = frm.Boton[2];

            BtnOK = frm.Ok;
            BtnNO = frm.NO;

            MensajesDeError = errores;
        }

        public void Update(UI_FrmxPln0 frm)
        {
            Update(frm.Tx_NumDec,TxtNumeroDeclaracion);
            Update(frm.Tx_FecDec,TxtFechaDeclaracion);
            Update(frm.Tx_CodAdn,TxtCodigoDeclaracion);
            Update(frm.Tx_MtoDec[0], TxtValBrutoDeclaracion);

            Update(frm.Tx_MtoDec[1], TxtValComisionDeclaracion);
            Update(frm.Tx_MtoDec[2], TxtValFleteDeclaracion);
            Update(frm.Tx_MtoDec[3], TxtValSeguroDeclaracion);
            Update(frm.Tx_MtoDec[4], TxtValGastoCorredorDeclaracion);
            Update(frm.Tx_MtoDec[5], TxtValAjusteDeclaracion);
            Update(frm.Tx_MtoDec[6], TxtValLiquidoDeclaracion);
            Update(frm.Tx_MtoDec[7], TxtValTranVariasDeclaracion);
            Update(frm.Tx_MtoDec[8], TxtValTotalCompraDeclaracion);
            Update(frm.Ch_Deduc[0], ChkValComisionesDeclaracion);
            Update(frm.Ch_Deduc[1], ChkValFleteDeclaracion);
            Update(frm.Ch_Deduc[2], ChkValSeguroDeclaracion);
            Update(frm.Cb_SecEcBen, CmbSecEcBen);
            Update(frm.Cb_SecEcIn, CmbSecEcInv);
            Update(frm.Tx_PrcPar, TxtValPorcentParticipante);
            Update(frm.Tx_NomCom, TxtNombreComprador);

            Update(frm.Tx_MtoPln[0], TxtValTotalPlanilla);
            Update(frm.LtTPln, CmbTipoPlanilla);
            Update(frm.Tx_MtoPln[1],TxtValBrutoPlanilla);
            Update(frm.Tx_MtoPln[2],TxtValComisionPlanilla);
            Update(frm.Tx_MtoPln[3],TxtOtrosGastosPlanilla);
            Update(frm.Tx_MtoPln[4],TxtValLiquidoPlanilla);
            Update(frm.Tx_MtoPln[5],TxtTipoCambioPlanilla);
            Update(frm.Tx_FecVen, TxtFecVencRetornoPlanilla);
            Update(frm.Tx_Obs, TxtObservacionesPlanilla);

            Update(frm.Tx_PlzFin, TxtPlazoFinanciamiento);
            Update(frm.Tx_NumPre, TxtNumPresentacion);
            Update(frm.Tx_FecPre, TxtFechaPresentacion);
            Update(frm.Cb_Bco, CmbBanco);
            Update(frm.Cb_Pbc, CmbPlazaBancoCentral);
            Update(frm.Cb_Tippln, CmbTipoPlanillaInformar);
            Update(frm.Tx_Fecha, TxtFechaInscripcion);
            Update(frm.Tx_NumIns, TxtNumeroInscripcion);
            Update(frm.Cb_Pais, CmbPaisOperacion);

            Update(frm.LtMto, LstMontos);
            Update(frm.LtPln, LstPlanillas);
        }

    }
}
