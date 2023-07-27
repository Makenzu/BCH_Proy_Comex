using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public static class Frm_Parti_No
    {
        private static void Command1_Click(InitializationObject initObj)
        {
            initObj.Frm_Parti_No.Tag = "ParCon.exe";

            // @estanislao ignorado
            //this.Hide();
        }

        private static void Command2_Click(InitializationObject initObj)
        {
            initObj.Frm_Parti_No.Tag = "PartyExp.exe";
            
            // @estanislao ignorado
            //this.Hide();
        }

        private static void Command3_Click(InitializationObject initObj)
        {
            initObj.Frm_Parti_No.Tag = "";

            // @estanislao ignorado
            //this.Hide();
        }

        private static void Form_Load(InitializationObject initObj)
        {
            initObj.Frm_Parti_No.Caption = T_Module1.GPrt_Caption + "[" + initObj.Frm_Participantes.Llave.Text + "]";
        }
    }
}
