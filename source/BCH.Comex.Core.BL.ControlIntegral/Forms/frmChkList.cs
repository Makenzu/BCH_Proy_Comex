using BCH.Comex.Core.Entities.Custodia.ControlIntegral.DataTypes;

namespace BCH.Comex.Core.BL.CONTROLINTEGRAL.Forms
{
    public class frmChkList
    {
        public static void main(InitializationObject initObj)
        {


            //if (initObj.MODFUNC.sCHK[1] != null || initObj.MODFUNC.sCHK[2] != null || initObj.MODFUNC.sCHK[3] != null || initObj.MODFUNC.sCHK[4] != null)
            if (initObj.ModFunc.sCHK != null)
            {
                initObj.frmChkList._opCtrl8[0].Enabled = initObj.ModFunc.sCHK[0] == 1 ? false : true;
                initObj.frmChkList._opCtrl8[1].Enabled = initObj.ModFunc.sCHK[0] == 1 ? false : true;
                initObj.frmChkList._opCtrl8[2].Enabled = initObj.ModFunc.sCHK[0] == 1 ? false : true;
                initObj.frmChkList._opCtrl8[1].Selected = initObj.ModFunc.sCHK[0] == 1 ? true : false;

                initObj.frmChkList._opCtrl7[0].Enabled = initObj.ModFunc.sCHK[1] == 1 ? false : true;
                initObj.frmChkList._opCtrl7[1].Enabled = initObj.ModFunc.sCHK[1] == 1 ? false : true;
                initObj.frmChkList._opCtrl7[2].Enabled = initObj.ModFunc.sCHK[1] == 1 ? false : true;
                initObj.frmChkList._opCtrl7[1].Selected = initObj.ModFunc.sCHK[1] == 1 ? true : false;

                initObj.frmChkList._opCtrl1[0].Enabled = initObj.ModFunc.sCHK[2] == 1 ? false : true;
                initObj.frmChkList._opCtrl1[1].Enabled = initObj.ModFunc.sCHK[2] == 1 ? false : true;
                initObj.frmChkList._opCtrl1[1].Selected = initObj.ModFunc.sCHK[2] == 1 ? true : false;

                initObj.frmChkList._opCtrl3[0].Enabled = initObj.ModFunc.sCHK[3] == 1 ? false : true;
                initObj.frmChkList._opCtrl3[1].Enabled = initObj.ModFunc.sCHK[3] == 1 ? false : true;
                initObj.frmChkList._opCtrl3[2].Enabled = initObj.ModFunc.sCHK[3] == 1 ? false : true;
                initObj.frmChkList._opCtrl3[1].Selected = initObj.ModFunc.sCHK[3] == 1 ? true : false;

                initObj.frmChkList._opCtrl6[0].Enabled = initObj.ModFunc.sCHK[4] == 1 ? false : true;
                initObj.frmChkList._opCtrl6[1].Enabled = initObj.ModFunc.sCHK[4] == 1 ? false : true;
                initObj.frmChkList._opCtrl6[2].Enabled = initObj.ModFunc.sCHK[4] == 1 ? false : true;
                initObj.frmChkList._opCtrl6[1].Selected = initObj.ModFunc.sCHK[4] == 1 ? true : false;
            }
            //Show vbModal
        }


        public static void Limpiar(InitializationObject initObj)
        {
            initObj.frmChkList._opCtrl8[0].Enabled = true;
            initObj.frmChkList._opCtrl8[1].Enabled = true;
            initObj.frmChkList._opCtrl8[2].Enabled = true;
            initObj.frmChkList._opCtrl8[1].Selected = false;

            initObj.frmChkList._opCtrl7[0].Enabled = true;
            initObj.frmChkList._opCtrl7[1].Enabled = true;
            initObj.frmChkList._opCtrl7[2].Enabled = true;
            initObj.frmChkList._opCtrl7[1].Selected = false;

            initObj.frmChkList._opCtrl1[0].Enabled = true;
            initObj.frmChkList._opCtrl1[1].Enabled = true;
            initObj.frmChkList._opCtrl1[1].Selected = false;

            initObj.frmChkList._opCtrl3[0].Enabled = true;
            initObj.frmChkList._opCtrl3[1].Enabled = true;
            initObj.frmChkList._opCtrl3[2].Enabled = true;
            initObj.frmChkList._opCtrl3[1].Selected = false;

            initObj.frmChkList._opCtrl6[0].Enabled = true;
            initObj.frmChkList._opCtrl6[1].Enabled = true;
            initObj.frmChkList._opCtrl6[2].Enabled = true;
            initObj.frmChkList._opCtrl6[1].Selected = false;


        }

    }
}
