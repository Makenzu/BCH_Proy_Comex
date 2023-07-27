using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.XGGL.Forms;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;

namespace BCH.Comex.Core.BL.XGGL
{
    public partial class XgglService
    {
        public void EmitirNotaCreditoInit(DatosGlobales globales)
        {
            using (Tracer tracer = new Tracer("EmitirNotaCreditoInit"))
            {
                Frmg_AsoNC.Form_Load(globales);
            }
        }
        public void EmitirNotaCredito_OkClick(DatosGlobales globales)
        {
            using (Tracer tracer = new Tracer("EmitirNotaCredito_OkClick"))
            {
                Frmg_AsoNC.ok_Click(globales, uow);
            }
        }
        public void EmitirNotaCredito_Cb_Producto_Click(DatosGlobales globales)
        {
            using (Tracer tracer = new Tracer("EmitirNotaCreditoa_Cb_Producto_Click"))
            {
                Frmg_AsoNC.Cb_Producto_Click(globales, uow);
            }
        }
        public void EmitirNotaCredito_Aceptar(DatosGlobales globales)
        {
            using (Tracer tracer = new Tracer("EmitirNotaCredito_AceptarClick"))
            {
                Frmg_AsoNC.Aceptar_Click(globales, uow);
            }
        }



    }
}
