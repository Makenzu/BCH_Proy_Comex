using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Models
{
    public class AnularContabilidadViewModel : ContabilidadGenericaViewModel
    {

        public UI_TextBox Tx_NroRep { get; set; }
        public UI_TextBox Tx_FecRep { get; set; }
        public UI_TextBox Tx_Cliente { get; set; }
        public List<FrmGlanuMovimientoDTO> ListaDatos { get; set; }
        public List<string> RptContable { get; set; }

        public AnularContabilidadViewModel() { }

        public AnularContabilidadViewModel(DatosGlobales Globales)
        {
            Tx_NroRep = Globales.FrmGlanu.Tx_NroRep;
            Tx_FecRep = Globales.FrmGlanu.Tx_FecRep;
            Tx_Cliente = Globales.FrmGlanu.Tx_Cliente;
            ListaDatos = Globales.FrmGlanu.ListaDatos;
            ListaErrores = Globales.FrmGlanu.ListaErrores;
            RptContable = Globales.FrmGlanu.RptContable;   

        }

        public void Update(AnularContabilidadViewModel vm, DatosGlobales Globales)
        {
            Globales.FrmGlanu.Tx_NroRep = vm.Tx_NroRep;
            Globales.FrmGlanu.Tx_FecRep = vm.Tx_FecRep;
            Globales.FrmGlanu.Tx_Cliente = vm.Tx_Cliente;
            Globales.FrmGlanu.ListaDatos = vm.ListaDatos;
            Globales.FrmGlanu.RptContable = vm.RptContable;  
        }
    }
}