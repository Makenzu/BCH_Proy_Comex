using BCH.Comex.Common.UI_Modulos;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_Pln_cob
    {
        // UPGRADE_INFO (#0501): The 'IndicePla' member isn't used anywhere in current application.
        public short IndicePla;
        public string Nemonico = "";

        public UI_Label Lb_NumPre;
		public UI_Label Lb_NomPla;
		public UI_Label Lb_CodPla;
		public UI_TextBox Tx_FecVen;
		public UI_Label Lb_NomImp;
		public UI_Label Lb_RutImp;
		public UI_Label Lb_NumIdi;
		public UI_Label Lb_FecIdi;

		public UI_Label Lb_PagIdi;
		public UI_Label Lb_NumCon;
		public UI_Label Lb_FecCon;
	
		public UI_Label Lb_NumCua;
		public UI_Label Lb_NumCuo;
		public UI_Label Lb_NumAcu;
		public UI_Label Lb_Acuer1;
		public UI_Label Lb_Acuer2;
		public UI_Label Lb_NomPai;
		public UI_Label Lb_CodPai;
		public UI_Label Lb_NomMon;
		public UI_Label Lb_CodMon;
	
		public UI_Label Lb_MtoFob;
        public UI_Label Lb_MtoFle;
		public UI_Label Lb_MtoSeg;
		public UI_Label Lb_MtoCif;
		public UI_Label Lb_ValTot;
		public UI_Label Lb_CifDol;
        public UI_Label Lb_TotDol;
		public UI_Label Lb_TipCam;
		public UI_Label Lb_ParPag;
	
		//Planilla Reemplazada.-
		public UI_Label Lb_NumPlnR;
		public UI_Label Lb_FecPlnR;
		public UI_Label Lb_CodPlzR;
		public UI_Label Lb_CodEntR;
		public UI_Label Lb_NumConR;
		public UI_Label Lb_FecConR;

		//Convenio credito reciproco.-
		public UI_Label Lb_FecDeb;
		public UI_Label Lb_DocChi;
		public UI_Label Lb_DocExt;
		public UI_TextBox Tx_Observ;
		public UI_CheckBox Ch_ZonFra;

        public UI_Button Bot_Sig;
        public UI_Button Bot_Ant;

        public UI_Frm_Pln_cob() {
            Lb_NumPre = new UI_Label();
            Lb_NomPla = new UI_Label();
            Lb_CodPla = new UI_Label();
            Tx_FecVen = new UI_TextBox();
            Lb_NomImp = new UI_Label();
            Lb_RutImp = new UI_Label();
            Lb_NumIdi = new UI_Label();
            Lb_FecIdi = new UI_Label();

            Lb_PagIdi = new UI_Label();
            Lb_NumCon = new UI_Label();
            Lb_FecCon = new UI_Label();

            Lb_NumCua = new UI_Label();
            Lb_NumCuo = new UI_Label();
            Lb_NumAcu = new UI_Label();
            Lb_Acuer1 = new UI_Label();
            Lb_Acuer2 = new UI_Label();
            Lb_NomPai = new UI_Label();
            Lb_CodPai = new UI_Label();
            Lb_NomMon = new UI_Label();
            Lb_CodMon = new UI_Label();

            Lb_MtoFob = new UI_Label();
            Lb_MtoFle = new UI_Label();
            Lb_MtoSeg = new UI_Label();
            Lb_MtoCif = new UI_Label();
            Lb_ValTot = new UI_Label();
            Lb_CifDol = new UI_Label();
            Lb_TotDol = new UI_Label();
            Lb_TipCam = new UI_Label();
            Lb_ParPag = new UI_Label();

            //Planilla Reemplazada.-
            Lb_NumPlnR = new UI_Label();
            Lb_FecPlnR = new UI_Label();
            Lb_CodPlzR = new UI_Label();
            Lb_CodEntR = new UI_Label();
            Lb_NumConR = new UI_Label();
            Lb_FecConR = new UI_Label();

            //Convenio credito reciproco.-
            Lb_FecDeb = new UI_Label();
            Lb_DocChi = new UI_Label();
            Lb_DocExt = new UI_Label();
            Tx_Observ = new UI_TextBox();
            Ch_ZonFra = new UI_CheckBox();

            Bot_Sig = new UI_Button();
            Bot_Ant = new UI_Button();
        }
    }
}
