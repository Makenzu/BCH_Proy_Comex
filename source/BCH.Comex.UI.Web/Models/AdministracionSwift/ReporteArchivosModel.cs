using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Models.AdministracionSwift
{
    public class ReporteArchivosModel
    {
        public string Titulo { get; set; }
        public string Total { get; set; }
        public string Archivos { get; set; }
        public IEnumerable<DetalleReporteArchivosModel> Detalle { get; set; }

    }
}