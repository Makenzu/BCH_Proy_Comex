using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.FinDia.Models
{
    public class ReporteCuadraturaModel
    {
        public string Titulo { get; set; }
        public string Total { get; set; }

        public IEnumerable<DetalleReporteCuadraturaModel> Detalle { get; set; }
    }
}