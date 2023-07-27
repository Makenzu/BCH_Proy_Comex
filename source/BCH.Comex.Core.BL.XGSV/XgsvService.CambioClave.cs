using BCH.Comex.Core.BL.XGSV.Forms;
using BCH.Comex.Core.Entities.Cext01.Supervisor;

namespace BCH.Comex.Core.BL.XGSV
{
    partial class XgsvService
    {
        public void CambioClaveInit(DatosGlobales globales)
        {
            FrmCamCl.Form_Load(globales, this.uow);
        }
        public void CambioClave_AceptarClick(DatosGlobales globales)
        {
            FrmCamCl.Bot_Aceptar_Click(globales, uow);
        }

        
    }
}
