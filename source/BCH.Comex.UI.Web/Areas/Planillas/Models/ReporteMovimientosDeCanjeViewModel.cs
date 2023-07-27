
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.UI.Web.Areas.Planillas.Models
{
    public class ReporteMovimientosDeCanjeViewModel
    {
        public string Titulo { get; set; }
        public DateTime FechaReporte { get; set; }

        public IList<ReporteMovimientosDeCanjeItemViewModel> Items { get; set; }
    }

    public class ReporteMovimientosDeCanjeItemViewModel
    {
        public string NumeroOperacion { get; set; }
        public string NombreCliente { get; set; }
        public string NumeroCuenta { get; set; }
        public string NemotecnicoCuenta { get; set; }
        public string CentroCosto { get; set; }
        public string CodigoUsuario { get; set; }
        public string SimboloMoneda { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
    }
}