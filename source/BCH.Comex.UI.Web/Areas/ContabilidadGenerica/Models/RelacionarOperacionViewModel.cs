using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;

namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Models
{
    public class RelacionarOperacionViewModel : ContabilidadGenericaViewModel
    {

        public UI_Combo Cb_Producto { get; set; }
        public UI_TextBox Tx_NumOpe_000 { get; set; }
        public UI_TextBox Tx_NumOpe_001 { get; set; }
        public UI_TextBox Tx_NumOpe_002 { get; set; }
        public UI_TextBox Tx_NumOpe_003 { get; set; }
        public UI_TextBox Tx_NumOpe_004 { get; set; }
        public UI_TextBox Tx_NumOpe_005 { get; set; }
        public UI_TextBox Tx_NumOpe_006 { get; set; }
        public UI_TextBox Tx_DirPrt { get; set; }
        public UI_TextBox Tx_NomPrt { get; set; }
        public UI_TextBox Tx_RutPrt { get; set; }

        public RelacionarOperacionViewModel() { }

        public RelacionarOperacionViewModel(DatosGlobales Globales)
        {
            ListaErrores = Globales.MESSAGES;
            Cb_Producto = Globales.FrmgAso.Cb_Producto;
            Tx_NumOpe_000 = Globales.FrmgAso.Tx_NumOpe_000;
            Tx_NumOpe_001 = Globales.FrmgAso.Tx_NumOpe_001;
            Tx_NumOpe_002 = Globales.FrmgAso.Tx_NumOpe_002;
            Tx_NumOpe_003 = Globales.FrmgAso.Tx_NumOpe_003;
            Tx_NumOpe_004 = Globales.FrmgAso.Tx_NumOpe_004;
            Tx_NumOpe_005 = Globales.FrmgAso.Tx_NumOpe_005;
            Tx_NumOpe_006 = Globales.FrmgAso.Tx_NumOpe_006;
            Tx_DirPrt = Globales.FrmgAso.Tx_DirPrt;
            Tx_NomPrt = Globales.FrmgAso.Tx_NomPrt;
            Tx_RutPrt = Globales.FrmgAso.Tx_RutPrt;
        }

        public void Update(RelacionarOperacionViewModel vm, DatosGlobales Globales)
        {
            Globales.FrmgAso.Cb_Producto = vm.Cb_Producto;
            Globales.FrmgAso.Tx_NumOpe_000 = vm.Tx_NumOpe_000;
            Globales.FrmgAso.Tx_NumOpe_001 = vm.Tx_NumOpe_001;
            Globales.FrmgAso.Tx_NumOpe_002 = vm.Tx_NumOpe_002;
            Globales.FrmgAso.Tx_NumOpe_003 = vm.Tx_NumOpe_003;
            Globales.FrmgAso.Tx_NumOpe_004 = vm.Tx_NumOpe_004;
            Globales.FrmgAso.Tx_NumOpe_005 = vm.Tx_NumOpe_005;
            Globales.FrmgAso.Tx_NumOpe_006 = vm.Tx_NumOpe_006;
            Globales.FrmgAso.Tx_DirPrt = vm.Tx_DirPrt;
            Globales.FrmgAso.Tx_NomPrt = vm.Tx_NomPrt;
            Globales.FrmgAso.Tx_RutPrt = vm.Tx_RutPrt;
        }

    }
}