using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Data.DAL.Cext01;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public static class Frm_Con_Participantes
    {
        private static void Form_Load(InitializationObject initObj)
        {
            Titulos_Grilla(initObj.Frm_Con_Participantes.msg_datos);

            //ignorado
            //Tabs = new short[2];
            //Tabs[0] = 315;
            //Tabs[1] = 450;
            //b = MODGPYF0.seteatabulador(lista, Tabs);

            initObj.Module1.KeyPrt = "";
        }

        public static void ok_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            initObj.Frm_Con_Participantes.lista.Items.Clear();

            initObj.Frm_Con_Participantes.img_Pare.Tag = "";
            initObj.Frm_Con_Participantes.ok.Visible = false;
            initObj.Frm_Con_Participantes.img_Pare.Visible = true;
            
            Mdl_Funciones_Varias.Limpia_Grilla(initObj.Frm_Con_Participantes.msg_datos);

            if (!string.IsNullOrEmpty(initObj.Frm_Con_Participantes.caja.Text))
            {
                var queryResult = uow.SceRepository.pro_sce_prty_s01_MS(initObj.Frm_Con_Participantes.caja.Text);

                if (queryResult != null)
                {
                    foreach (var item in queryResult)
                    {
                        var gridItem = new UI_GridItem();

                        gridItem.AddColumn("Nombre o Razón Social",
                            string.Format("{0} {1}, {2}, {3}", item.razon_soci, item.direccion, item.ciudad, item.pais));
                        gridItem.AddColumn("Identificador", item.id_party.Replace("|", ""));

                        initObj.Frm_Con_Participantes.msg_datos.Items.Add(gridItem);
                    }
                }
            }

            //if (VB6Helpers.Err.Number != 0)
            //{
            //    // UPGRADE_INFO (#0181): Reference to default form instance 'Frm_Con_Participantes' was converted to Me/this keyword.
            //    d = n - this.lista.ListCount;
            //    VB6Helpers.MsgBox("Quedaron " + VB6Helpers.Format(VB6Helpers.CStr(d)) + " ítemes por cargar a la lista.", MsgBoxStyle.Information, "Validación de Datos");
            //    return;
            //}

            //msg_datos.Rows = Format.StringToDouble(msg_datos.Rows) - 1;

            initObj.Frm_Con_Participantes.ok.Visible = true;
            initObj.Frm_Con_Participantes.img_Pare.Visible = false;
        }

        private static void msg_datos_DblClick(InitializationObject initObj)
        {
            int selectdRow = initObj.Frm_Con_Participantes.msg_datos.ListIndex;
            initObj.Module1.KeyPrt = initObj.Frm_Con_Participantes.msg_datos.Items[selectdRow].GetColumn("Identificador");
        }

        private static void Titulos_Grilla(UI_Grid grilla)
        {
            grilla.Header.Add("Nombre o Razón Social");
            grilla.Header.Add("Identificador");
        }

    }
}
