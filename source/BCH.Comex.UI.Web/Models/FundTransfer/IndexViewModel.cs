using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class IndexViewModel
    {
        private UI_Frm_Principal uI_Frm_Principal;

        public string Caption { set; get; }
        public UI_TextBox Referencia { get; set; }
        public UI_TextBox NombreParticipante { get; set; }
        public UI_TextBox NumeroFactura { get; set; }
        public UI_TextBox NumeroOperacion { get; set; }
        public UI_TextBox Neto { get; set; }
        public UI_TextBox TipoFactura { get; set; }
        public UI_TextBox Moneda { get; set; }
        public UI_TextBox IVA { get; set; }
        public UI_TextBox Total { get; set; }
        public IList<UI_ElementoLista_Frm_Principal> ComercioVisible { get; set; }
        public IList<UI_ElementoLista_Frm_Principal> ComercioVisibleExport { get; set; }
        public IList<UI_ElementoLista_Frm_Principal> ComercioVisibleImport { get; set; }
        public UI_CheckBox ChkImpresionCartas { get; set; }
        public UI_CheckBox ChkImpresionContabilidad { get; set; }
        public UI_CheckBox ChkImpresionPlanillas { get; set; }

        public IndexViewModel(UI_Frm_Principal frm, UI_Mdi_Principal frmMdi)
        {
            Caption = frm.Caption;
            Referencia = frm.Tx_RefCli;
            NombreParticipante = frm.Tx_NomPrt;
            NumeroFactura = frm.Tx_NroFac;
            NumeroOperacion = frm.Num_Op;
            Neto = frm.Tx_neto;
            TipoFactura = frm.Tx_tipo;
            Moneda = frm.Tx_moneda;
            IVA = frm.Tx_iva;
            Total = frm.Tx_MtoOri;

            ComercioVisible = frm.Lt_CI;
            ComercioVisibleExport = frm.Lt_CVE;
            ComercioVisibleImport = frm.Lt_CVI;

            ChkImpresionCartas = frmMdi.mnu_cartas;
            ChkImpresionContabilidad = frmMdi.mnu_conta;
            ChkImpresionPlanillas = frmMdi.mnu_planillas;
        }
    }
}
