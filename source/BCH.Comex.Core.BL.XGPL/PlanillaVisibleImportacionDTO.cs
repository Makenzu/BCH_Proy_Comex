using System;

namespace BCH.Comex.Core.BL.XGPL
{
    public class PlanillaVisibleImportacionDTO
    {
        public string CodigoCentroCosto { get; set; }
        public string CodigoProducto { get; set; }
        public string CodigoEspecialista { get; set; }
        public string CodigoEmpresa { get; set; }
        public string CodigoCobranza { get; set; }
        public decimal NumeroPlanilla { get; set; }
        public DateTime FechaVentaAntigua { get; set; }
        public DateTime FechaVenta { get; set; }
        public string NumeroConocimientoEmbarque { get; set; }
        public DateTime FechaConocimientoEmbarque { get; set; }
        public decimal FormaPago { get; set; }
        public decimal CodigoPais { get; set; }
        public string NombrePais { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool HayCuadroCuotas { get; set; }
        public decimal NumeroCuotas { get; set; }
        public decimal Cuota { get; set; }
        public bool HayAcuerdos { get; set; }
        public decimal NumeroAcuerdos { get; set; }
        public string AcuerdoDesde { get; set; }        
        public string AcuerdoHasta { get; set; }
        public DateTime FechaAutorizacionDebito { get; set; }
        public string NumeroDocumentoChile { get; set; }
        public string NumeroDocumentoExtranjero { get; set; }
        public string Observaciones { get; set; }
        public bool isZonaFranca { get; set; }
    }
}
