using System;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.Planillas.Models
{
    public class ItemMovimientosCuentaCorrienteViewModel
    {
        public string CodigoOperacion { get; set; }
        public string CodigoEspecialista { get; set; }
        public string NemMoneda { get; set; }
        public decimal? MontoMcd { get; set; }
        public string CodigoDh { get; set; }
        public string NumeroCme { get; set; }
        public string RutCliente { get; set; }
        public string OfiCon { get; set; }
        public decimal? Estado { get; set; }

    }

    public class ReporteMovimientosCuentaCorrienteViewModel
    {
        public IList<ItemMovimientosCuentaCorrienteViewModel> Detalle { get; set; }
        public string NemMoneda { get; set; }
        public decimal TotalDebe { get; set; }
        public decimal TotalHaber { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaMovimiento { get; set; }
    }
}