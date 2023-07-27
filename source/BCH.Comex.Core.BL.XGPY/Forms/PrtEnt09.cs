using BCH.Comex.Core.BL.XGPY.Modulos;
//using BCH.Comex.Common.XGPY.Datatypes;
//using BCH.Comex.Common.XGPY.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;
using CodeArchitects.VB6Library;
using System;


namespace BCH.Comex.Core.BL.XGPY.Forms
{
    public class PrtEnt09
    {

        private void Cerrar_Click(object sender, EventArgs e)
        {

            //if (lista.SelectedIndex >= 0)
            //{
            //    lista_dblclick(null, new EventArgs());
            //}
            //else
            //{
            //    this.Close();
            //}

        }
        private static void Form_Load(InitializationObject initObj, object sender, EventArgs e)
        {
            int b = 0;
            int[] tabs = null;        
            tabs = new int[3];
            tabs[0] = 315;
            tabs[1] = 450;
            b =  UTILES.seteaTabulador(initObj.PrtEnt09.lista, tabs);
            T_PRTYENT2.KeyPrt = "";

        }
        private void lista_dblclick(InitializationObject initObj, object sender, EventArgs e)
        {
            string l = "";        
            if (initObj.PrtEnt09.lista.ListIndex == -1)
            {
                return;
            }
            if (initObj.PrtEnt09.lista.Items.Count > 0)
                l = initObj.PrtEnt09.lista.Items[initObj.PrtEnt09.lista.ListIndex].Value;
           
            T_PRTGLOB.llave = UTILES.copiardestring(l, VB6Helpers.Chr(9), 2);
            T_PRTYENT2.KeyPrt = T_PRTGLOB.llave;
            // PrtEnt09.DefInstance.Close();

        }

    }
}
