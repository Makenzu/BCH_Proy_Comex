using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms;
using BCH.Comex.Data.DAL.Cext01;

namespace BCH.Comex.Core.BL.XGGL.Forms
{
    public static class PrtEnt09_Logic
    {
        public static void OK_Click(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            UI_PrtEnt09 PrtEnt09 = Globales.PrtEnt09;
            string texto = PrtEnt09.caja.Text;
            

            PrtEnt09.lista.Items.Clear();
            if (!string.IsNullOrEmpty(texto))
            {
                try
                {
                    var queryResult = unit.SceRepository.pro_sce_prty_s01_MS(texto);
                    foreach(var item in queryResult)
                    {
                        var gridItem = new UI_GridItem();

                        gridItem.AddColumn("Nombre o Razón Social",
                            string.Format("{0} {1}, {2}, {3}", item.razon_soci, item.direccion, item.ciudad, item.pais));
                        gridItem.AddColumn("Identificador", item.id_party.Replace("|", ""));

                        PrtEnt09.lista.Items.Add(gridItem);

                    }
                }
                catch
                {
                    
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Error leer claves de ordenamiento de los participantes de la cartera.",
                        Type = TipoMensaje.Error
                    });
                    return;
                }
            }
        }
    }
}
