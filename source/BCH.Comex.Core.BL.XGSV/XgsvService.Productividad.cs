using BCH.Comex.Core.BL.XGSV.Forms;
using BCH.Comex.Core.Entities.Cext01.Supervisor;

namespace BCH.Comex.Core.BL.XGSV
{
    partial class XgsvService
    {

        public void ProductividadInit(DatosGlobales globales)
        {
            FrmAyM.Form_Load(globales);
        }
        public void Productividad_ImprimirClick(int anio, int mes, DatosGlobales globales)
        {
            FrmAyM.Boton_Click(anio, mes, globales, uow);
        }

    }
}
