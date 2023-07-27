using BCH.Comex.Core.BL.XGSV.Forms;
using BCH.Comex.Core.Entities.Cext01.Supervisor;

namespace BCH.Comex.Core.BL.XGSV
{
    partial class XgsvService
    {
        public void VisualizacionUsuarioInit(DatosGlobales globales)
        {
            FrmgUsr.Form_Load(globales, uow);
        }

        public void TraspasoUsuarioInit(DatosGlobales globales)
        {
            FrmTrasp.Form_Load(globales, uow);
        }

        public void CederCarteraInit(DatosGlobales globales)
        {
            FrmCeder.Form_Load(globales, uow);
        }

        public void CederCarteraObtenerClientes(DatosGlobales globales, string usuarioActual, string producto)
        {
            FrmCeder.Form_ObtenerClientes(globales, uow, usuarioActual, producto);
        }

        public void CederCarteraObtenerOperaciones(DatosGlobales globales, string usuarioActual, string producto, string clienteID)
        {
            FrmCeder.Form_ObtenerOperaciones(globales, uow, usuarioActual, producto, clienteID);
        }

        public void CederCarteraSave(DatosGlobales globales, string usuarioActual, string usuarioNuevo, string cliente, string producto)
        {
            FrmCeder.Form_Save(globales, uow, usuarioActual, usuarioNuevo, cliente, producto);
        }

        public void VisualizacionUsuario_CierreClick(DatosGlobales globales)
        {
            FrmgUsr.Cierre_Click(globales, uow);
        }
        public void VisualizacionUsuario_FinDiaClick(string CenCos, string CodEsp, DatosGlobales globales)
        {
            FrmgUsr.FinDia_Click(CenCos, CodEsp, globales, uow);
        }
        public bool VisualizacionUsuario_UnCierreClick(string CenCos, string CodEsp, DatosGlobales globales)
        {
            return FrmgUsr.UnCierre_Click(CenCos, CodEsp, globales, uow);
        }

        public void CederCarteraObtenerProductos(DatosGlobales globales)
        {
            FrmCeder.ObtenerProductos(globales, uow);
        }
    }
}
