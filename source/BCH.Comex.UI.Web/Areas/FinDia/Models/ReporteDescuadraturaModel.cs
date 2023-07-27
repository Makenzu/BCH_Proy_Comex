using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.FinDia.Models
{
    public class ReporteDescuadraturaModel
    {
        public string Titulo { get; set; }
        public string Total { get; set; }

        public IEnumerable<DetalleReporteDescuadraturaModel> DetalleInyeccion { get; set; }
        public IEnumerable<DetalleReporteDescuadraturaSceModel> DetalleSce { get; set; }
    }
}