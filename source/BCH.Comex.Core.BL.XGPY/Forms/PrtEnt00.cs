using BCH.Comex.Core.BL.XGPY.Modulos;
//using BCH.Comex.Common.XGPY.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using System;

namespace BCH.Comex.Core.BL.XGPY.Forms
{
    public class PrtEnt00
    {

        public static void Form_Load(InitializationObject InitObj)
        {    
            InitObj.PrtEnt00._prttipob_2.Checked = System.Windows.Forms.CheckState.Unchecked.ToBool();
            InitObj.PrtEnt00._prttipob_0.Enabled = false;
            InitObj.PrtEnt00._prttipob_1.Enabled = false;
            InitObj.PrtEnt00._prttipob_2.Enabled = false;

            if (MODGUSR.UsrEsp.Tipeje == "N")
            {
                // initObj.Frm_Participantes.Donde[0].Selected = true;
                //_PrtTipo_1.Checked = true;               
                //_PrtTipo_2.Enabled = false;
                //_PrtTipo_2.Visible = false;


                //_PrtFormato_1.Checked = true;
                //_PrtFormato_1.Enabled = false;
                //_PrtFormato_2.Enabled = false;
                //_PrtFormato_2.Visible = false;
                //_PrtFormato_2.Checked = false;
                InitObj.PrtEnt00._PrtTipo[0].Selected = true;
                InitObj.PrtEnt00._PrtTipo[1].Enabled = false;
                InitObj.PrtEnt00._PrtTipo[1].Visible = false;

                InitObj.PrtEnt00._PrtFormato[0].Selected = true;
                InitObj.PrtEnt00._PrtFormato[0].Enabled = false;
                InitObj.PrtEnt00._PrtFormato[1].Enabled = false;
                InitObj.PrtEnt00._PrtFormato[1].Visible = false;
                InitObj.PrtEnt00._PrtFormato[1].Selected = false;

            }
            else if (MODGUSR.UsrEsp.Tipeje == "O")
            {               
                InitObj.PrtEnt00._PrtTipo[0].Enabled = true;
                InitObj.PrtEnt00._PrtTipo[1].Enabled = true;
                InitObj.PrtEnt00._PrtTipo[0].Visible = true;
                InitObj.PrtEnt00._PrtTipo[1].Visible = true;
                InitObj.PrtEnt00._PrtTipo[0].Selected = false;
                InitObj.PrtEnt00._PrtTipo[1].Selected = true;
                InitObj.PrtEnt00._PrtFormato[1].Enabled = true;
                InitObj.PrtEnt00._PrtFormato[1].Selected = false;
            }

            if (MODGUSR.UsrEsp.Jerarquia != 0)
            {            

                InitObj.PrtEnt00._PrtTipo[0].Enabled = true;
                InitObj.PrtEnt00._PrtTipo[1].Enabled = true;
                InitObj.PrtEnt00._PrtTipo[1].Visible = true;
                InitObj.PrtEnt00._PrtTipo[0].Selected = true;
                InitObj.PrtEnt00._PrtTipo[1].Selected = false;

                InitObj.PrtEnt00._PrtFormato[0].Value = "Rut";
                InitObj.PrtEnt00._PrtFormato[0].Enabled = true;
                InitObj.PrtEnt00._PrtFormato[0].Visible = true;
                InitObj.PrtEnt00._PrtFormato[0].Selected = true;
                InitObj.PrtEnt00._PrtFormato[1].Visible = true;
                InitObj.PrtEnt00._PrtFormato[1].Enabled = true;
                InitObj.PrtEnt00._PrtFormato[1].Selected = false;
            }           
        }
    }
}
