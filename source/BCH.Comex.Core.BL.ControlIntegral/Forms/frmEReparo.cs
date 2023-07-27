using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Custodia;
using BCH.Comex.Core.Entities.Custodia.ControlIntegral.DataTypes;
using BCH.Comex.Data.DAL.Custodia;
using System;
using System.Collections.Generic;


namespace BCH.Comex.Core.BL.CONTROLINTEGRAL.Forms
{
    public class frmEReparo
    {

        public static void BotonGenerar(InitializationObject InitObj, UnitOfWorkCustodia uow, string Datos0, string Datos1, string DocName, string MailEjecutivo, string Usuario)
        {
            string TextoBin = "";
            string cadena0 = string.Empty;
            string OTROS0, OTROS1, OTROS2, OTROS3, OTROS4;
            OTROS0 = string.Empty;
            OTROS1 = string.Empty;
            OTROS2 = string.Empty;
            OTROS3 = string.Empty;
            OTROS4 = string.Empty;

            TextoBin += InitObj.frmEReparo.chkReparo[0].Checked.ToInt().ToStr() == "1" ? "1" : "0";   //IIf(chkReparo(0).Value, "1", "0")
            TextoBin += InitObj.frmEReparo.chkReparo[1].Checked.ToInt().ToStr() == "1" ? "1" : "0";
            TextoBin += InitObj.frmEReparo.chkReparo[17].Checked.ToInt().ToStr() == "1" ? "1" : "0";
            TextoBin += InitObj.frmEReparo.chkReparo[18].Checked.ToInt().ToStr() == "1" ? "1" : "0";
            TextoBin += InitObj.frmEReparo.chkReparo[19].Checked.ToInt().ToStr() == "1" ? "1" : "0";
            TextoBin += InitObj.frmEReparo.chkReparo[2].Checked.ToInt().ToStr() == "1" ? "1" : "0";
            TextoBin += InitObj.frmEReparo.chkReparo[3].Checked.ToInt().ToStr() == "1" ? "1" : "0";
            TextoBin += InitObj.frmEReparo.chkReparo[4].Checked.ToInt().ToStr() == "1" ? "1" : "0";

            TextoBin += InitObj.frmEReparo.chkReparo[5].Checked.ToInt().ToStr() == "1" ? "1" : "0";
            TextoBin += InitObj.frmEReparo.chkReparo[6].Checked.ToInt().ToStr() == "1" ? "1" : "0";

            TextoBin += InitObj.frmEReparo.chkReparo[7].Checked.ToInt().ToStr() == "1" ? "1" : "0";
            TextoBin += InitObj.frmEReparo.chkReparo[8].Checked.ToInt().ToStr() == "1" ? "1" : "0";
            TextoBin += InitObj.frmEReparo.chkReparo[9].Checked.ToInt().ToStr() == "1" ? "1" : "0";

            TextoBin += InitObj.frmEReparo.chkReparo[10].Checked.ToInt().ToStr() == "1" ? "1" : "0";
            TextoBin += InitObj.frmEReparo.chkReparo[11].Checked.ToInt().ToStr() == "1" ? "1" : "0";
            TextoBin += InitObj.frmEReparo.chkReparo[12].Checked.ToInt().ToStr() == "1" ? "1" : "0";
            TextoBin += InitObj.frmEReparo.chkReparo[13].Checked.ToInt().ToStr() == "1" ? "1" : "0";

            TextoBin += InitObj.frmEReparo.chkReparo[14].Checked.ToInt().ToStr() == "1" ? "1" : "0";
            TextoBin += InitObj.frmEReparo.chkReparo[15].Checked.ToInt().ToStr() == "1" ? "1" : "0";
            TextoBin += InitObj.frmEReparo.chkReparo[16].Checked.ToInt().ToStr() == "1" ? "1" : "0";

            InitObj.ModFunc.sCHK = new int[5];

            if (InitObj.frmEReparo.chkReparo[0].Checked || InitObj.frmEReparo.chkReparo[1].Checked || InitObj.frmEReparo.chkReparo[2].Checked || InitObj.frmEReparo.chkReparo[3].Checked || InitObj.frmEReparo.chkReparo[4].Checked || (InitObj.frmEReparo.chkOtros[0].Checked || InitObj.frmEReparo.txtOtros[0].Text.Trim().Length > 0))
            {
                InitObj.ModFunc.sCHK[0] = 1;
                if (InitObj.frmEReparo.chkReparo[2].Checked || InitObj.frmEReparo.chkReparo[3].Checked || InitObj.frmEReparo.chkReparo[4].Checked || (InitObj.frmEReparo.chkOtros[0].Checked && (InitObj.frmEReparo.txtOtros[0].Text.Trim().Length) > 0))
                {
                    if (InitObj.frmEReparo.chkOtros[0].Checked && (InitObj.frmEReparo.txtOtros[0].Text.Trim().Length > 0))
                    {
                        OTROS0 = InitObj.frmEReparo.txtOtros[0].Text;
                    }
                }
            }
            else
                InitObj.ModFunc.sCHK[0] = 0;

            if (InitObj.frmEReparo.chkReparo[5].Checked || InitObj.frmEReparo.chkReparo[6].Checked || (InitObj.frmEReparo.chkOtros[1].Checked && (InitObj.frmEReparo.txtOtros[1].Text.Trim().Length > 0)))
            {
                InitObj.ModFunc.sCHK[1] = 1;

                if (InitObj.frmEReparo.chkOtros[1].Checked && (InitObj.frmEReparo.txtOtros[1].Text.Trim().Length > 0))
                {
                    OTROS1 = InitObj.frmEReparo.txtOtros[1].Text;
                }
            }
            else
                InitObj.ModFunc.sCHK[1] = 0;


            if (InitObj.frmEReparo.chkReparo[7].Checked || InitObj.frmEReparo.chkReparo[8].Checked || InitObj.frmEReparo.chkReparo[9].Checked || (InitObj.frmEReparo.chkOtros[2].Checked && (InitObj.frmEReparo.txtOtros[2].Text.Trim().Length > 0)))
            {
                InitObj.ModFunc.sCHK[2] = 1;

                if (InitObj.frmEReparo.chkOtros[2].Checked && (InitObj.frmEReparo.txtOtros[2].Text.Trim().Length > 1))
                {
                    OTROS2 = InitObj.frmEReparo.txtOtros[2].Text;
                }
            }
            else
                InitObj.ModFunc.sCHK[2] = 0;


            InitObj.ModFunc.sCHK[3] = InitObj.frmEReparo.chkReparo[9].Checked.ToInt() == 1 ? 1 : 0;

            if (InitObj.frmEReparo.chkReparo[10].Checked || InitObj.frmEReparo.chkReparo[11].Checked || InitObj.frmEReparo.chkReparo[12].Checked || InitObj.frmEReparo.chkReparo[13].Checked || (InitObj.frmEReparo.chkOtros[3].Checked && (InitObj.frmEReparo.txtOtros[3].Text.Trim().Length > 0)))
            {
                InitObj.ModFunc.sCHK[4] = 1;
                if (InitObj.frmEReparo.chkOtros[3].Checked && (InitObj.frmEReparo.txtOtros[3].Text.Trim().Length > 1))
                {
                    OTROS3 = InitObj.frmEReparo.txtOtros[3].Text;
                }
            }
            else
                InitObj.ModFunc.sCHK[4] = 0;


            if (InitObj.frmEReparo.chkReparo[14].Checked || InitObj.frmEReparo.chkReparo[15].Checked || InitObj.frmEReparo.chkReparo[16].Checked || (InitObj.frmEReparo.chkOtros[4].Checked && (InitObj.frmEReparo.txtOtros[4].Text.Trim().Length > 1)))
            {
                if (InitObj.frmEReparo.chkOtros[4].Checked && (InitObj.frmEReparo.txtOtros[4].Text.Trim().Length > 1))
                {
                    OTROS4 = InitObj.frmEReparo.txtOtros[4].Text;
                }
            }
            uow.CambiosRepository.cambios_mift_reparo_log_01_MS(InitObj.ModFunc.sCuenta, Datos0, Datos1, InitObj.ModFunc.sMoneda, decimal.Parse(InitObj.ModFunc.sMonto), DocName, MailEjecutivo, Usuario, TextoBin, OTROS0, OTROS1, OTROS2, OTROS3, OTROS4);

        }


        public static List<UI_ComboItem> documentoName(InitializationObject InitObj, UnitOfWorkCustodia uow)
        {
            List<UI_ComboItem> docName = new List<UI_ComboItem>();

            foreach (cambios_mift_citidoc_consulta_01_MS_Result doc in uow.CambiosRepository.cambios_mift_citidoc_consulta_01_MS(InitObj.ModFunc.sCuenta, decimal.Parse(InitObj.ModFunc.sMonto)))
            {
                docName.Add(new UI_ComboItem() { Value = doc.Document_Name, ID = doc.rut });
            }
            return (docName);
        }


        public static void LlenaComboDocName(InitializationObject InitObj, UnitOfWorkCustodia uow)
        {
            InitObj.frmEReparo.cmbDocName.Items.Clear();           
            int i = 0;
            foreach (UI_ComboItem docName in documentoName(InitObj, uow))
            {
                InitObj.ModFunc.Datos[0] = docName.ID; //Rut
                if (!string.IsNullOrEmpty(docName.ID.ToString().Trim()))
                {
                    InitObj.frmEReparo.cmbDocName.Items.Add(new UI_ComboItem { Data = i, ID = docName.ID, Value = docName.Value });
                }
                else
                {
                    InitObj.frmEReparo.cmbDocName.Items.Add(new UI_ComboItem());
                }
                i++;
            }

            if (InitObj.frmEReparo.cmbDocName.Items.Count > 0)
                InitObj.frmEReparo.cmbDocName.ListIndex = 0;

        }


        public static void Limpiar(InitializationObject InitObj)
        {

            InitObj.frmEReparo.chkReparo[0].Checked = false;
            InitObj.frmEReparo.chkReparo[1].Checked = false;
            InitObj.frmEReparo.chkReparo[17].Checked = false;
            InitObj.frmEReparo.chkReparo[18].Checked = false;
            InitObj.frmEReparo.chkReparo[19].Checked = false;
            InitObj.frmEReparo.chkReparo[2].Checked = false;
            InitObj.frmEReparo.chkReparo[3].Checked = false;
            InitObj.frmEReparo.chkReparo[4].Checked = false;

            InitObj.frmEReparo.chkReparo[5].Checked = false;
            InitObj.frmEReparo.chkReparo[6].Checked = false;

            InitObj.frmEReparo.chkReparo[7].Checked = false;
            InitObj.frmEReparo.chkReparo[8].Checked = false;
            InitObj.frmEReparo.chkReparo[9].Checked = false;

            InitObj.frmEReparo.chkReparo[10].Checked = false;
            InitObj.frmEReparo.chkReparo[11].Checked = false;
            InitObj.frmEReparo.chkReparo[12].Checked = false;
            InitObj.frmEReparo.chkReparo[13].Checked = false;

            InitObj.frmEReparo.chkReparo[14].Checked = false;
            InitObj.frmEReparo.chkReparo[15].Checked = false;
            InitObj.frmEReparo.chkReparo[16].Checked = false;

            InitObj.frmEReparo.chkOtros[0].Checked = false;
            InitObj.frmEReparo.chkOtros[1].Checked = false;
            InitObj.frmEReparo.chkOtros[2].Checked = false;
            InitObj.frmEReparo.chkOtros[3].Checked = false;
            InitObj.frmEReparo.chkOtros[4].Checked = false;
            InitObj.frmEReparo.txtOtros[0].Text = string.Empty;
            InitObj.frmEReparo.txtOtros[1].Text = string.Empty;
            InitObj.frmEReparo.txtOtros[2].Text = string.Empty;
            InitObj.frmEReparo.txtOtros[3].Text = string.Empty;
            InitObj.frmEReparo.txtOtros[4].Text = string.Empty;

        }



    }
}
