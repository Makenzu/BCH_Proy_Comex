using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using System;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class PlanillaVisibleImportEditarViewModel
    {
        public string Lb_NumPre { get; set; }
        public string Lb_NomPla { get; set; }
        public string Lb_CodPla { get; set; }
        public UI_TextBox Tx_FecVen { get; set; }
        public string Lb_NomImp { get; set; }
        public string Lb_RutImp { get; set; }
        public string Lb_NumIdi { get; set; }
        public string Lb_FecIdi { get; set; }

        public string Lb_PagIdi { get; set; }
        public string Lb_NumCon { get; set; }
        public string Lb_FecCon { get; set; }

        public string Lb_NumCua { get; set; }
        public string Lb_NumCuo { get; set; }
        public string Lb_NumAcu { get; set; }
        public string Lb_Acuer1 { get; set; }
        public string Lb_Acuer2 { get; set; }
        public string Lb_NomPai { get; set; }
        public string Lb_CodPai { get; set; }
        public string Lb_NomMon { get; set; }
        public string Lb_CodMon { get; set; }

        public string Lb_MtoFob { get; set; }
        public string Lb_MtoFle { get; set; }
        public string Lb_MtoSeg { get; set; }
        public string Lb_MtoCif { get; set; }
        public string Lb_ValTot { get; set; }
        public string Lb_CifDol { get; set; }
        public string Lb_TotDol { get; set; }
        public string Lb_TipCam { get; set; }
        public string Lb_ParPag { get; set; }

        //Planilla Reemplazada.-
        public string Lb_NumPlnR { get; set; }
        public string Lb_FecPlnR { get; set; }
        public string Lb_CodPlzR { get; set; }
        public string Lb_CodEntR { get; set; }
        public string Lb_NumConR { get; set; }
        public string Lb_FecConR { get; set; }

        //Convenio credito reciproco.-
        public string Lb_FecDeb { get; set; }
        public string Lb_DocChi { get; set; }
        public string Lb_DocExt { get; set; }
        public string Tx_Observ { get; set; }
        public bool Ch_ZonFra { get; set; }

        public UI_Button Bot_Sig { get; set; }
        public UI_Button Bot_Ant { get; set; }

        public PlanillaVisibleImportEditarViewModel() { }

        public PlanillaVisibleImportEditarViewModel(UI_Frm_Pln_cob frm)
        { 
            Lb_NumPre = frm.Lb_NumPre.Text == null?"":frm.Lb_NumPre.Text;
            Lb_NomPla = frm.Lb_NomPla.Text == null?"":frm.Lb_NomPla.Text;
            Lb_CodPla = frm.Lb_CodPla.Text == null?"":frm.Lb_CodPla.Text;
            Tx_FecVen = frm.Tx_FecVen == null?new UI_TextBox():frm.Tx_FecVen;
            Lb_NomImp = frm.Lb_NomImp.Text == null?"":frm.Lb_NomImp.Text;
            Lb_RutImp = frm.Lb_RutImp.Text == null?"":frm.Lb_RutImp.Text;
            Lb_NumIdi = frm.Lb_NumIdi.Text == null?"":frm.Lb_NumIdi.Text;
            Lb_FecIdi = frm.Lb_FecIdi.Text == null?"":frm.Lb_FecIdi.Text;

            Lb_PagIdi = frm.Lb_PagIdi.Text == null?"":frm.Lb_PagIdi.Text;
            Lb_NumCon = frm.Lb_NumCon.Text == null?"":frm.Lb_NumCon.Text;
            Lb_FecCon = frm.Lb_FecCon.Text == null?"":frm.Lb_FecCon.Text;

            Lb_NumCua = frm.Lb_NumCua.Text == null?"":frm.Lb_NumCua.Text;
            Lb_NumCuo = frm.Lb_NumCuo.Text == null?"":frm.Lb_NumCuo.Text;
            Lb_NumAcu = frm.Lb_NumAcu.Text == null?"":frm.Lb_NumAcu.Text;
            Lb_Acuer1 = frm.Lb_Acuer1.Text == null?"":frm.Lb_Acuer1.Text;
            Lb_Acuer2 = frm.Lb_Acuer2.Text == null?"":frm.Lb_Acuer2.Text;
            Lb_NomPai = frm.Lb_NomPai.Text == null?"":frm.Lb_NomPai.Text;
            Lb_CodPai = frm.Lb_CodPai.Text == null?"":frm.Lb_CodPai.Text;
            Lb_NomMon = frm.Lb_NomMon.Text == null?"":frm.Lb_NomMon.Text;
            Lb_CodMon = frm.Lb_CodMon.Text == null?"":frm.Lb_CodMon.Text;

            Lb_MtoFob = frm.Lb_MtoFob.Text == null?"":frm.Lb_MtoFob.Text;
            Lb_MtoFle = frm.Lb_MtoFle.Text == null?"":frm.Lb_MtoFle.Text;
            Lb_MtoSeg = frm.Lb_MtoSeg.Text == null?"":frm.Lb_MtoSeg.Text;
            Lb_MtoCif = frm.Lb_MtoCif.Text == null?"":frm.Lb_MtoCif.Text;
            Lb_ValTot = frm.Lb_ValTot.Text == null?"":frm.Lb_ValTot.Text;
            Lb_CifDol = frm.Lb_CifDol.Text == null?"":frm.Lb_CifDol.Text;
            Lb_TotDol = frm.Lb_TotDol.Text == null?"":frm.Lb_TotDol.Text;
            Lb_TipCam = frm.Lb_TipCam.Text == null?"":frm.Lb_TipCam.Text;
            Lb_ParPag = frm.Lb_ParPag.Text == null?"":frm.Lb_ParPag.Text; 

            //Planilla Reemplazada.-
            Lb_NumPlnR = frm.Lb_NumPlnR.Text == null?"":frm.Lb_NumPlnR.Text;
            Lb_FecPlnR = frm.Lb_FecPlnR.Text == null?"":frm.Lb_FecPlnR.Text;
            Lb_CodPlzR = frm.Lb_CodPlzR.Text == null?"":frm.Lb_CodPlzR.Text;
            Lb_CodEntR = frm.Lb_CodEntR.Text == null?"":frm.Lb_CodEntR.Text;
            Lb_NumConR = frm.Lb_NumConR.Text == null?"":frm.Lb_NumConR.Text;
            Lb_FecConR = frm.Lb_FecConR.Text == null?"":frm.Lb_FecConR.Text; 

            //Convenio credito reciproco.-
            Lb_FecDeb = frm.Lb_FecDeb.Text == null?"":frm.Lb_FecDeb.Text;
            Lb_DocChi = frm.Lb_DocChi.Text == null?"":frm.Lb_DocChi.Text;
            Lb_DocExt = frm.Lb_DocExt.Text == null?"":frm.Lb_DocExt.Text;
            Tx_Observ = frm.Tx_Observ == null ? "" : frm.Tx_Observ.Text;
            Ch_ZonFra = Convert.ToBoolean(frm.Ch_ZonFra.Value); 

            Bot_Sig = frm.Bot_Sig;
            Bot_Ant = frm.Bot_Ant;
        }

        public void Update(UI_Frm_Pln_cob frm) {
            frm.Ch_ZonFra.Value = (short)( this.Ch_ZonFra ? 1 : 0);
            frm.Tx_FecVen = this.Tx_FecVen;
            frm.Tx_Observ.Text = this.Tx_Observ;
        }   
    }
}