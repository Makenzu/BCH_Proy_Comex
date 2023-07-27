using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Planillas.Models
{
    public class EstadisticaImportacionViewModel
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0}")]
        public decimal NumeroPresentacion { get; set; }
       
        public string CodigoPlanilla { get; set; }   //Código de Planilla
        
        public InteresesPlanilla DetalleIntereses { get; set; }

        public DateTime FechaVenta { get; set; }

        public decimal CodigoPlazaBancoCentral { get; set; }

        public string NombreImportador { get; set; }

        public string RutImportador { get; set; }

        public string NumeroInforme { get; set; }

        public DateTime FechaInforme { get; set; }

        public decimal FormaPago { get; set; }

        public IEnumerable<SelectListItem> FormasPago { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0}")]
        public decimal CodigoPaisPago { get; set; }

        public string PaisPago { get; set; }

        public decimal PlazaEmision { get; set; }

        public IEnumerable<SelectListItem> PlazasEmision { get; set; }

        public decimal CodigoMoneda { get; set; }

        public string NumeroConocimientoEmbarque { get; set; }

        public DateTime FechaConocimientoEmbarque { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public decimal NumeroCuotas { get; set; }

        public decimal Cuota { get; set; }

        public decimal NumeroAcuerdos { get; set; }

        public string AcuerdoDesde { get; set; }

        public string AcuerdoHasta { get; set; }

        public bool UsaConvenio { get; set; }

        public DateTime FechaAutorizacionDebito { get; set; }

        public string NumeroDocumentoChile { get; set; }

        public string NumeroDocumentoExtranjero { get; set; }

        public string Moneda { get; set; }

        public decimal ValorMercaderia { get; set; }

        public decimal GastosHastaFOB { get; set; }

        public decimal FOB { get; set; }

        public decimal Flete { get; set; }

        public decimal Seguro { get; set; }

        public decimal CIF { get; set; }

        public decimal Intereses { get; set; }

        public decimal GastosBancarios { get; set; }

        public decimal ValorTotal { get; set; }

        public decimal CIFEnDolares { get; set; }

        public decimal TotalEnDolares { get; set; }

        public decimal ParidadUSD { get; set; }

        public decimal TipoCambio { get; set; }

        public bool isPlanillaReemplazo { get; set; }

        public decimal? ReemplazoNumeroPresentacion { get; set; }

        public DateTime? ReemplazoFecha { get; set; }

        public decimal? ReemplazoCodigoPlazaBancoCentral { get; set; }

        public decimal? ReemplazoCodigoEntidad { get; set; }

        public string ReemplazoNumeroInforme { get; set; }

        public DateTime? ReemplazoFechaInforme { get; set; }

        public decimal? ReemplazoPlazaEmision { get; set; }

        public string ReemplazoNumeroConocimientoEmbarque { get; set; }

        public DateTime? ReemplazoFechaConocimientoEmbarque { get; set; }

        public bool HayCuadroCuotas { get; set; }
    }

    public class InteresesPlanilla
    {
        public decimal NumeroPlanilla  { get; set; }   //Número de la Planilla
        public DateTime FechaPlanilla { get; set; }  //Fecha de venta
        public decimal NumeroLineaInteres { get; set; }   //Número línea interes
        public decimal Concepto { get; set; }   //Concepto interes
        public string Tipo { get; set; }   //Tipo Interes
        public decimal CapitalBase { get; set; }   //Capital base
        public decimal CodigoBaseAno { get; set; }   //Código base año
        public decimal TasaInteres { get; set; }   //Tasa interes
        public DateTime FechaInicial { get; set; }   //Fecha inicial
        public DateTime FechaFinal { get; set; }   //Fecha final
        public decimal NumeroDias { get; set; }   //Número de días
        public decimal Monto { get; set; }   //Monto interes
        public bool isEliminado { get; set; }   //True si fué eliminado
    }
}