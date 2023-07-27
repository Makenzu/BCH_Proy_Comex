using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.XGGL.Forms;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;

namespace BCH.Comex.Core.BL.XGGL
{
    public partial class XgglService
    {

        public void AnularContabilidadInit(DatosGlobales globales)
        {
            using (Tracer tracer = new Tracer("Anular_Contabilidad_Generica - AnularContabilidadInit"))
            {
                //FrmGlanu.Form_Load(globales);
            }
        }

        public void AnularContabilidad_OkClick(DatosGlobales globales)
        {
            using (Tracer tracer = new Tracer("Anular_Contabilidad_Generica - AnularContabilidad_OkClick"))
            {
                FrmGlanu.ok_Click(globales, uow);
            }
        }

        //public void AnularContabilidad_AceptarClick(DatosGlobales globales, string Identificacion_Rut)
        public void AnularContabilidad_AceptarClick(DatosGlobales globales)
        {
            using (Tracer tracer = new Tracer("Anular_Contabilidad_Generica - AnularContabilidad_AceptarClick"))
            {
                string Identificacion_Rut = globales.DatosUsuario.Identificacion_Rut;
                FrmGlanu.Aceptar_Click(Identificacion_Rut, globales, uow, uowSwift);
                globales.Controller = string.Empty;
                globales.Action = string.Empty;

                Frm_gl.inicializa(globales, uow);

                //if (globales.DocumentosAImprimir.Count > 0)
                //{
                //    globales.Action = "Imprimir";
                //}
                //else
                //{
                //    globales.Controller = "Home";
                //    globales.Action = "Index";
                //}
            }
        }

    }
}
