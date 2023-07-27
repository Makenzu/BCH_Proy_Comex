using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using System;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class AnulacionPlanillaVisibleImportViewModel : FundTransferViewModel
    {
        public UI_TextBox Tx_NroOpe_000 { get; set; }
        public UI_TextBox Tx_NroOpe_001 { get; set; }
        public UI_TextBox Tx_NroOpe_002 { get; set; }
        public UI_TextBox Tx_NroOpe_003 { get; set; }
        public UI_TextBox Tx_NroOpe_004 { get; set; }
        public UI_TextBox Tx_FecPre { get; set; }
        public string FecPre { get; set; }
        public UI_ListBox Lt_PlAnul { get; set; }
        public UI_TextBox Tx_MtoAnu { get; set; }
        public UI_TextBox Tx_TipCam { get; set; }
        public UI_TextBox Tx_ObsAnu { get; set; }
        public UI_Combo Cb_TipAut { get; set; }
        public UI_TextBox Tx_NumAut { get; set; }
        public UI_TextBox Tx_FecAut { get; set; }
        public UI_CheckBox Ch_Reemp { get; set; }
        public UI_Button Bot_Aceptar { get; set; }
        public UI_Button Bot_Cancel { get; set; }
        public UI_Button Bot_Ok { get; set; }


        public AnulacionPlanillaVisibleImportViewModel()
        {
        }
            

        public AnulacionPlanillaVisibleImportViewModel(UI_Frm_Anu_Vi frm)
        {
            Tx_NroOpe_000 = frm.Tx_NroOpe[0];
            Tx_NroOpe_001 = frm.Tx_NroOpe[1];
            Tx_NroOpe_002 = frm.Tx_NroOpe[2];
            Tx_NroOpe_003 = frm.Tx_NroOpe[3];
            Tx_NroOpe_004 = frm.Tx_NroOpe[4];
            Tx_FecPre = frm.Tx_FecPre;
            Lt_PlAnul = frm.Lt_PlAnul;
            Lt_PlAnul.Items.ForEach(x => x.Value = x.Value.Replace(" ", "\xA0").Replace("\t", "\xA0\xA0\xA0"));
            Tx_MtoAnu = frm.Tx_MtoAnu;
            Tx_TipCam = frm.Tx_TipCam;
            Tx_ObsAnu = frm.Tx_ObsAnu;
            Cb_TipAut = frm.Cb_TipAut;
            Tx_NumAut = frm.Tx_NumAut;
            Tx_FecAut = frm.Tx_FecAut;
            Ch_Reemp = frm.Ch_Reemp;
            Bot_Aceptar = frm.Bot_Aceptar;
            Bot_Cancel = frm.Bot_Cancel;
            Bot_Ok = frm.Bot_Ok;

        }


        internal void Update(UI_Frm_Anu_Vi frm)
        {
            using (var trace = new Tracer("AnulaPlanillaVisibleImport - Update"))
            {
                try
                {
                    Update(frm.Tx_NroOpe_000, Tx_NroOpe_000);
                    Update(frm.Tx_NroOpe_001, Tx_NroOpe_001);
                    Update(frm.Tx_NroOpe_002, Tx_NroOpe_002);
                    Update(frm.Tx_NroOpe_003, Tx_NroOpe_003);
                    Update(frm.Tx_NroOpe_004, Tx_NroOpe_004);
                    Update(frm.Tx_FecPre, Tx_FecPre);
                    Update(frm.Lt_PlAnul, Lt_PlAnul);
                    Update(frm.Tx_MtoAnu, Tx_MtoAnu);
                    Update(frm.Tx_TipCam, Tx_TipCam);
                    Update(frm.Tx_ObsAnu, Tx_ObsAnu);
                    Update(frm.Cb_TipAut, Cb_TipAut);
                    Update(frm.Tx_NumAut, Tx_NumAut);
                    Update(frm.Tx_FecAut, Tx_FecAut);
                    Update(frm.Ch_Reemp, Ch_Reemp);

                }
                catch (Exception ex)
                {
                    trace.TraceException("Alerta",ex);
                    throw;
                }
            }
        }
    }
}