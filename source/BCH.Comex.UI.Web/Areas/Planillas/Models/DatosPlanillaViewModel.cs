using System;

namespace BCH.Comex.UI.Web.Areas.Planillas.Models
{
    public class DatosPlanillaViewModel
    {
        public string NumeroPresentacion { get; set; }
        public DateTime FechaPresentacion { get; set; }
        public string CentroCosto { get; set; }
        public string CodigoUsuario { get; set; }
        public string NombreCliente { get; set; }
        public string Operacion { get; set; }
        public string CodComercio { get; set; }
        public string rutexp { get; set; }
        public int? indnom { get; set; }
        public int? TipoPlanilla { get; set; }
        public int? CodigoMoneda { get; set; }
        public string SiglaMoneda { get; set; }
        public decimal? MontoLiquidado { get; set; }
        public decimal? MontoLiquidadoIngr { get; set; }
        public decimal? MontoLiquidadoEgr { get; set; }
        public string tipo { get; set; }
        public string ingegr { get; set; }
        public string OpeRelacionada { get; set; }
    }
}
