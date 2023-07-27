using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Custodia.ControlIntegral.UI_Modulos;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.UI.Web.Models.ControlIntegral
{
    public class ChkListViewModel
    {

        //public List<string> opCtrl { get; set; }


        public string lblControl1 { get; set; }
        //public string opCtrl1 { get; set; }
        //public string opCtrl2 { get; set; }
        public string lblControl2 { get; set; }

        public List<UI_OptionItem> opCtrl1 { get; set; }
        public int SelectedCtrl1 { get; set; }

        public List<UI_OptionItem> opCtrl2 { get; set; }
        public int SelectedCtrl2 { get; set; }

        public List<UI_OptionItem> opCtrl3 { get; set; }
        public int SelectedCtrl3 { get; set; }

        public List<UI_OptionItem> opCtrl4 { get; set; }
        public int SelectedCtrl4 { get; set; }

        public List<UI_OptionItem> opCtrl5 { get; set; }
        public int SelectedCtrl5 { get; set; }

        public List<UI_OptionItem> opCtrl6 { get; set; }
        public int SelectedCtrl6 { get; set; }

        public List<UI_OptionItem> opCtrl7 { get; set; }
        public int SelectedCtrl7 { get; set; }

        public List<UI_OptionItem> opCtrl8 { get; set; }
        public int SelectedCtrl8 { get; set; }


        #region "variables Internas"
        public int indexAuxCtrl1 { get; set; }
        public int indexAuxCtrl2 { get; set; }
        public int indexAuxCtrl3 { get; set; }
        public int indexAuxCtrl4 { get; set; }
        public int indexAuxCtrl5 { get; set; }
        public int indexAuxCtrl6 { get; set; }
        public int indexAuxCtrl7 { get; set; }
        public int indexAuxCtrl8 { get; set; }

        #endregion

        public ChkListViewModel(UI_frmChkList frmState)
        {

            this.opCtrl1 = frmState._opCtrl1;
            indexAuxCtrl1 = -1;
            for (int i = 0; i < frmState._opCtrl1.Count; i++)
            {
                if (frmState._opCtrl1[i].Selected)
                    indexAuxCtrl1 = 1;
            }
            if (indexAuxCtrl1 != -1)
                this.SelectedCtrl1 = int.Parse(frmState._opCtrl1.First(x => x.Selected).ID);

            this.opCtrl2 = frmState._opCtrl2;
            indexAuxCtrl2 = -1;
            for (int i = 0; i < frmState._opCtrl2.Count; i++)
            {
                if (frmState._opCtrl2[i].Selected)
                    indexAuxCtrl2 = 1;
            }
            if (indexAuxCtrl2 != -1)
                this.SelectedCtrl2 = int.Parse(frmState._opCtrl2.First(x => x.Selected).ID);


            this.opCtrl3 = frmState._opCtrl3;
            indexAuxCtrl3 = -1;
            for (int i = 0; i < frmState._opCtrl3.Count; i++)
            {
                if (frmState._opCtrl3[i].Selected)
                    indexAuxCtrl3 = 1;
            }
            if (indexAuxCtrl3 != -1)
                this.SelectedCtrl3 = int.Parse(frmState._opCtrl3.First(x => x.Selected).ID);

            this.opCtrl4 = frmState._opCtrl4;
            indexAuxCtrl4 = -1;
            for (int i = 0; i < frmState._opCtrl4.Count; i++)
            {
                if (frmState._opCtrl4[i].Selected)
                    indexAuxCtrl4 = 1;
            }
            if (indexAuxCtrl4 != -1)
                this.SelectedCtrl4 = int.Parse(frmState._opCtrl4.First(x => x.Selected).ID);

            this.opCtrl5 = frmState._opCtrl5;
            indexAuxCtrl5 = -1;
            for (int i = 0; i < frmState._opCtrl5.Count; i++)
            {
                if (frmState._opCtrl5[i].Selected)
                    indexAuxCtrl5 = 1;
            }
            if (indexAuxCtrl5 != -1)
                this.SelectedCtrl5 = int.Parse(frmState._opCtrl5.First(x => x.Selected).ID);


            this.opCtrl6 = frmState._opCtrl6;
            indexAuxCtrl6 = -1;
            for (int i = 0; i < frmState._opCtrl6.Count; i++)
            {
                if (frmState._opCtrl6[i].Selected)
                    indexAuxCtrl6 = 1;
            }
            if (indexAuxCtrl6 != -1)
                this.SelectedCtrl6 = int.Parse(frmState._opCtrl6.First(x => x.Selected).ID);

            this.opCtrl7 = frmState._opCtrl7;
            indexAuxCtrl7 = -1;
            for (int i = 0; i < frmState._opCtrl7.Count; i++)
            {
                if (frmState._opCtrl7[i].Selected)
                    indexAuxCtrl7 = 1;
            }
            if (indexAuxCtrl7 != -1)
                this.SelectedCtrl7 = int.Parse(frmState._opCtrl7.First(x => x.Selected).ID);

            this.opCtrl8 = frmState._opCtrl8;
            indexAuxCtrl8 = -1;
            for (int i = 0; i < frmState._opCtrl8.Count; i++)
            {
                if (frmState._opCtrl8[i].Selected)
                    indexAuxCtrl8 = 1;
            }
            if (indexAuxCtrl8 != -1)
                this.SelectedCtrl8 = int.Parse(frmState._opCtrl8.First(x => x.Selected).ID);

        }

    }




}