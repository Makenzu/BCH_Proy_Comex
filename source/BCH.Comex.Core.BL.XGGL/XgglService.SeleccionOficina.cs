using BCH.Comex.Core.BL.XGGL.Forms;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.BL.XGGL
{
    public partial class XgglService
    {
        public void SeleccionBancoInit(DatosGlobales Globales)
        {
            Globales.Frm_SeleccionOficina = new UI_Frm_SeleccionOficina();
            FrmOfi.Form_Load(Globales, uow);
        }
        public void Seleccion_Banco_AceptarBanco(DatosGlobales Globales)
        {
            FrmOfi.Aceptar_Click(Globales);
        }

        public void Seleccion_Banco_CancelarBanco(DatosGlobales Globales)
        {
            FrmOfi.Cancelar_Click(Globales);
        }

    }
}
