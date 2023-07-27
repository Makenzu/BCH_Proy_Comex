using System;
using System.Linq;

namespace BCH.Comex.UI.Web.Areas.Planillas.Models
{
    public class ReporteMovimientosCorresponsalesItemViewModel
    {
        public string Operacion { get; set; }
        public int NumeroReporte { get; set; }
        public string Usuario { get; set; }
        public string Cuenta { get; set; }
        public string Moneda { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
    }

    public class ReporteMovimientosCorresponsalesViewModel
    {
        public string Titulo{ get; set; }
        public DateTime Fecha { get; set; }
        public IQueryable<ReporteMovimientosCorresponsalesItemViewModel> Items { get; set; }        
    }
}