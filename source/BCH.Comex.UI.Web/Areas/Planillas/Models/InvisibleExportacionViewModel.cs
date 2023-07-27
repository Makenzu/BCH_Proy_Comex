using BCH.Comex.Core.BL.XGPL;
using BCH.Comex.Core.BL.XGPL.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace BCH.Comex.UI.Web.Areas.Planillas.Models
{
    public class InvisibleExportacionViewModel
    {
        public int? CodigoOperacion { get; set; }
        public string NumeroPlanilla { get; set; }
        [CodigoPaisValido]
        [Display(Name="Código de país")]
        public int CodigoPaisOrigen { get; set; }
        public string PaisOrigen { get; set; }
        public int TipoPlanila { get; set; }
        [Required]
        [DiaHabil]
        [Rango20Anos]
        [Display(Name="Fecha de Planilla")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaPlanilla { get; set; }
        public DateTime FechaAnterior { get; set; }
        public int CodigoPlazaBancoCentral { get; set; }
        public string PlazaBancoCentral { get; set; }
        [CodigoMonedaBancoCentralValido]
        [Display(Name="Código de Moneda")]
        public int CodigoMoneda { get; set; }
        public string NombreMoneda { get; set; }
        public string RUTCliente { get; set; }
        public string NombreParticipante { get; set; }
        public string DireccionParticipante { get; set; }
        public int TipoPlanilla { get; set; }
        public string CodigoComercio { get; set; }
        public string NombreCodigoComercio { get; set; }
        public string Concepto { get; set; }
        public string NumeroPlanillaAnulada { get; set; }

        [Rango20Anos]
        [Display(Name = "Fecha de Anulación")]
        public DateTime? FechaAnulacion { get; set; }
        public int PlazaBancoCentralAnulacion { get; set; }
        // TODO: Averiguar qué significa "Apc"
        public string ApcTipo { get; set; }
        public string ApcNumero { get; set; }

        [Rango20Anos]
        public DateTime? ApcFecha { get; set; }
        public int ApcPlazaBancoCentral { get; set; }
        public CommaSeparatedCompactableArray Acuerdos { get; set; }
        //[DisplayFormat(DataFormatString = "{0:N}")]
        public string MontoOperacion { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:#,##0.0000000000}", ApplyFormatInEditMode = true)]
        public decimal? MontoParidadBancoCentral { get; set; }
        
        public string MontoEnDolares { get; set; }

        [Display(Name = "Tipo de Cambio")]
        [DisplayFormat(DataFormatString = "{0:#,##0.0000}", ApplyFormatInEditMode = true)]
        public decimal? TipoDeCambio { get; set; }
        //[DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public string MontoNacional { get; set; }
        public bool ZonaFranca { get; set; }
        public string DieNumero { get; set; }

        [Rango20Anos]
        public DateTime? DieFecha { get; set; }
        public int? DiePlazaBancoCentral { get; set; }
        public string NumeroDeclaracion { get; set; }
        [Rango20Anos]
        public DateTime? FechaDeclaracion { get; set; }
        public int? CodigoAduana { get; set; }
        public string CodigoEOR { get; set; }
        public int? NumeroCredito { get; set; }
        [Rango20Anos]
        public DateTime? FechaCredito { get; set; }
        public int? MonedaCredito { get; set; }
        public decimal? MontoCredito { get; set; }
        public decimal? SecBen { get; set; }
        public decimal? SecInv { get; set; }
        public decimal? PorcentajeParticipacion { get; set; }
        public string CodigoAcuerdo { get; set; }
        public string NumeroRegistroAcuerdo { get; set; }
        public string RutAcuerdo { get; set; }
        [Rango20Anos]
        public DateTime? ConvenioCreditoFechaAutorizacionDebito { get; set; }
        public string ConvenioCreditoDocumentoNacional { get; set; }
        public string ConvenioCreditoDocumentoExtranjero { get; set; }
        public int? ConvenioCreditoBancoExtranjero { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(255)]
        public string Observaciones { get; set; }

        public bool PlanillaGuardada { get; set; }
    }
}