using BCH.Comex.Core.BL.XGSV.Forms;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XGSV
{
    partial class XgsvService
    {
        public void DocumentosValoradosInit(DatosGlobales globales)
        {
            FrmgChq.Form_Load(globales, this.uow);
        }

        public List<T_Chq> DocumentosValorados_ObtenerCheques(int opPlazaPago, string fechaEmision, bool todos, DatosGlobales globales)
        {
            return FrmgChq.ObtenerCheques(opPlazaPago, fechaEmision, todos,  globales, uow);
        }

        public List<T_Vvi> DocumentosValorados_ObtenerValeVista(int opPlazaPago, DatosGlobales globales)
        {
            return FrmgChq.ObtenerValeVista(opPlazaPago, globales, uow);
        }

        public void PrepararChequeParaImprimir(T_Chq cheque, DatosGlobales globales)
        {
            FrmgChq.Co_Imprimir_Click(cheque, globales, this.uow);
        }

    }
}
