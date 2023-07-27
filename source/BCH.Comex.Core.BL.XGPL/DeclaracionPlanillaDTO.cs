using System;

namespace BCH.Comex.Core.BL.XGPL
{
    public class DeclaracionPlanillaDTO
    {
        public string NumeroPresentacion { get; set; }
        
        public DateTime? FechaPresentacion { get; set; }

        public string Tipo { get; set; }

        public DateTime? FechaActual { get; set; }

        public string DeclaracionExportacion { get; set; }

        public string DeclaracionImportacion { get; set; }

        public DateTime? FechaDeclaracion { get; set; }

        public int CodigoAduana { get; set; }

        public decimal MontoDI { get; set; }

        public decimal InteresDI { get; set; }
        
        public decimal MontoUSD { get; set; }             
        
        public DateTime? FechaVencimientoRetorno { get; set; }
       
    }
}
