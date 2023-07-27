
using BCH.Comex.Core.BL.XGGL.Forms;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;

namespace BCH.Comex.Core.BL.XGGL
{
    public partial class XgglService
    {
		public void CHQ_FormLoad(DatosGlobales Globales)
        {
            FrmgChq_Logic.Form_Load(Globales, this.uow);
        }

        public void CHQ_L_Benef_Click(DatosGlobales Globales)
        {
            FrmgChq_Logic.l_benef_Click(Globales);
        }

        public void CHQ_L_Plaza_Click(DatosGlobales Globales)
        {
            FrmgChq_Logic.l_plaza_Click(Globales);
            FrmgChq_Logic.l_plaza_LostFocus(Globales);
        }

        public void CHQ_Generar(DatosGlobales Globales)
        {
            FrmgChq_Logic.Co_Generar_Click(Globales, this.uow);
        }

        public void CHQ_Aceptar(DatosGlobales Globales)
        {
            FrmgChq_Logic.Co_Aceptar_Click(Globales);
            Globales.Action = "Despues_De_Cheques";
            Globales.Controller = "Grabar";
        }

        public void CHQ_Cancelar(DatosGlobales Globales)
        {
            FrmgChq_Logic.Co_Cancelar_Click(Globales);
        }

    }
}
