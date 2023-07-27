using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FinDia;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XGFD.Forms
{
    public static class FrmRut
    {
        public static void BAceptar_Click(string rut, T_MODCUACC modcuacc, T_MODFDIA modfdin, IList<UI_Message> listaMensaje)
        {

            //if (rut.Substring(rut.Length-1).ToUpper() == "K")
            //{
            //    rut = string.Format("########", rut.Substring(0, rut.Length - 2)) + rut.Substring(rut.Length - 1).ToUpper();
            //}
            //else
            //{
            //    rut = string.Format("#########", rut);
            //}

            rut = rut.PadLeft(9, '0');

            if (rut.ToUpper() != modcuacc.RutwAis.ToUpper())
            {
                //  D.S.B.
                listaMensaje.Add(new UI_Message() { Text = "El Rut ingresado no corresponde. " });
                //MigrationSupport.Utils.MsgBox("El Rut ingresado no corresponde. ", MODGPYF0.pito(48).Cast<MigrationSupport.MsgBoxStyle>(), MODFDIA.MsgFDia);
                // MsgBox "El Rut ingresado NO corresponde al Usuario de esta Estación.", Pito(48), MsgFDia
                //Tx_Rut.Focus();
            }
            else
            {
                modfdin.RutOK = true;
            }

        }
    }
}
