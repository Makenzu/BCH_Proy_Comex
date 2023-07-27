using System;
using System.Linq;

namespace BCH.Comex.UI.Web.Areas.Planillas.Models
{
    public class ReportePlanillasVsConversionItemViewModel
    {
        public string Moneda { get; set; }
        public decimal Egresos { get; set; }
        public decimal Ingresos { get; set; }
        public decimal ConversionDebe { get; set; }
        public decimal ConversionHaber { get; set; }
        public decimal DiferenciaDebe { get; set; }
        public decimal DiferenciaHaber { get; set; }
    }

    public class ResumenTipoPlanilla
    {
        public int TipoPlanilla  { get; set; }
        public string Moneda {get; set;}
        public string Tipo {get;set;}
        public decimal Ingreso {get;set;}
        public decimal Egreso { get; set; }
        public int Cantidad { get; set; }
    }

    public class ReportePlanillasVsConversionViewModel
    {
        public DateTime Fecha { get; set; }
        public string Titulo { get; set; }
        public IQueryable<ReportePlanillasVsConversionItemViewModel> Planillas { get; set; }
        public IQueryable<ResumenTipoPlanilla> ResumenPorTipo { get; set; }
        public string Especialista { get; set; }
    }

    public class ReporteInformePosicionCambioItemViewModel
    {
        public string NumeroPlanilla { get; set; }
        public DateTime Fecha { get; set; }
        public int CodigoOperacion { get; set; }
        public string Operacion { get; set; }
        public string CodigoEspecialista { get; set; }
        public string RutCliente { get; set; }
        public int CodigoMoneda { get; set; }
        public decimal MontoOrigen { get; set; }
        public decimal Ingreso { get; set; }
        public decimal Egreso { get; set; }
    }

    public class ReporteInformePosicionCambioResumenItemViewModel
    {
        public int CodigoOperacion { get; set; }
        public decimal Ingreso { get; set; }
        public decimal Egreso { get; set; }
        public int Cantidad { get; set; }
    }

    public class ReporteInformePosicionCambioViewModel
    {
        public DateTime Fecha { get; set; }
        public string Titulo { get; set; }
        public string Especialista { get; set; }
        public string CentroCosto { get; set; }
        public IQueryable<ReporteInformePosicionCambioItemViewModel> Planillas { get; set; }
    }
}
