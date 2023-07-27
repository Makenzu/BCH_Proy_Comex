using BCH.Comex.Core.Entities.Cext01;

namespace BCH.Comex.UI.Web.Areas.Impresion.Models
{
    public class ReporteContableViewModel
    {
        public ReporteContable Reporte { get; set; }
        public bool GenerarHtmlCompleto { get; set; }
        public bool Imprimir { get; set; }
    }
}