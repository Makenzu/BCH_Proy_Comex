using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.XGGL.Forms;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;

namespace BCH.Comex.Core.BL.XGGL
{
    public partial class XgglService
    {
        public void RelacionarOperacionInit(DatosGlobales globales)
        {
            using (Tracer tracer = new Tracer("RelacionarOperacionInit"))
            {
                Frmg_Aso.Form_Load(globales, uow);
            }
        }
        public void RelacionarOperacion_OkClick(DatosGlobales globales)
        {
            using (Tracer tracer = new Tracer("RelacionarOperacion_OkClick"))
            {
                Frmg_Aso.ok_Click(globales, uow);
            }
        }
        public void RelacionarOperacion_Cb_Producto_Click(DatosGlobales globales)
        {
            using (Tracer tracer = new Tracer("RelacionarOperaciona_Cb_Producto_Click"))
            {
                Frmg_Aso.Cb_Producto_Click(globales, uow);
            }
        }
        public void RelacionarOperacion_AceptarClick(DatosGlobales globales)
        {
            using (Tracer tracer = new Tracer("RelacionarOperacion_AceptarClick"))
            {
                Frmg_Aso.Aceptar_Click(globales, uow);

                globales.gl.Cliente.Text = globales.SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].NombreUsado;
                globales.gl.Num_Op.Text = globales.MODGASO.VgAso.OpeSin;
                globales.gl.datos.Enabled = true;
                globales.gl.frame_ext.Enabled = true;
                globales.gl.frame_nac.Enabled = true;
            }
        }
    
    }
}
