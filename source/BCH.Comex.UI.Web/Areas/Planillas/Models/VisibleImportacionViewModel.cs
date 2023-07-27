using BCH.Comex.Core.BL.XGPL.Validators;
using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Planillas.Models
{
    public class VisibleImportacionViewModel
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:000000}")]
        public decimal NumeroPresentacion { get; set; }

        public string CodigoPlanilla { get; set; }   //Código de Planilla

        public InteresesPlanilla DetalleIntereses { get; set; }

        [Required]
        [Display(Name = "Fecha venta")]
        [DiaHabil]
        public DateTime FechaPlanilla { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0}")]
        public decimal CodigoPlazaBancoCentral { get; set; }

        public string NombreImportador { get; set; }

        public string RutImportador { get; set; }

        public string NumeroInforme { get; set; }

        public DateTime? FechaInforme { get; set; }

        public decimal FormaPago { get; set; }

        public IEnumerable<SelectListItem> FormasPago { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:000}")]
        public decimal CodigoPaisPago { get; set; }

        public string PaisPago { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0}")]
        public decimal PlazaEmision { get; set; }

        public IEnumerable<SelectListItem> PlazasEmision { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:000}")]
        public decimal CodigoMoneda { get; set; }

        public string NumeroConocimientoEmbarque { get; set; }

        public DateTime? FechaConocimientoEmbarque { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        [RequiredIf("Cuota != null", ErrorMessage = "Debe Registrar el Nº de Cuadro de Pagos")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0}")]
        public decimal? NumeroCuotas { get; set; }

        [RequiredIf("NumeroCuotas != null", ErrorMessage = "Debe Registrar el Nº de la Cuota para el Cuadro de Pagos")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0}")]
        public decimal? Cuota { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0}")]
        public decimal? NumeroAcuerdos { get; set; }

        [StringLength(5)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0}")]
        public string AcuerdoDesde { get; set; }

        [StringLength(5)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0}")]
        public string AcuerdoHasta { get; set; }

        public bool UsaConvenio { get; set; }

        //[RequiredIf("UsaConvenio == true", ErrorMessage = "Para Convenio Aladi Fecha Debito en Chile es Dato Requerido")]
        public DateTime? FechaAutorizacionDebito { get; set; }

        [RequiredIf("UsaConvenio == true", ErrorMessage = "Para Convenio Aladi Numero Documento en Chile es Dato Requerido")]
        public string NumeroDocumentoChile { get; set; }

        [RequiredIf("UsaConvenio == true", ErrorMessage = "Para Convenio Aladi Numero Documento en el Extranjero es Dato Requerido")]
        public string NumeroDocumentoExtranjero { get; set; }

        public string Moneda { get; set; }

        public decimal ValorMercaderia { get; set; }

        public decimal GastosHastaFOB { get; set; }

        public string FOB { get; set; }

        public string Flete { get; set; }

        public string Seguro { get; set; }

        [AssertThat("ParsearADecimal(CIF) == ParsearADecimal(FOB) + ParsearADecimal(Flete) + ParsearADecimal(Seguro)", 
            ErrorMessage = "Monto CIF {CIF} no cuadra con detalle de Planilla {FOB} + {Flete} + {Seguro}. Debe Corregir detalle de Planilla.")]
        public string CIF { get; set; }

        public decimal Intereses { get; set; }

        public decimal GastosBancarios { get; set; }

        public string ValorTotal { get; set; }

        public string CIFEnDolares { get; set; }

        public string TotalEnDolares { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.0000}", ApplyFormatInEditMode = true)]
        public decimal ParidadUSD { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.0000}", ApplyFormatInEditMode = true)]
        public decimal TipoCambio { get; set; }

        public bool isPlanillaReemplazo { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:000000}")]
        public decimal? ReemplazoNumeroPresentacion { get; set; }

        public DateTime? ReemplazoFecha { get; set; }

        public decimal? ReemplazoCodigoPlazaBancoCentral { get; set; }

        public decimal? ReemplazoCodigoEntidad { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:000000}")]
        public string ReemplazoNumeroInforme { get; set; }

        public DateTime? ReemplazoFechaInforme { get; set; }

        public decimal? ReemplazoPlazaEmision { get; set; }

        public string ReemplazoNumeroConocimientoEmbarque { get; set; }

        public DateTime? ReemplazoFechaConocimientoEmbarque { get; set; }

        public IEnumerable<SelectListItem> Paises { get; set; }

        public DateTime? FechaAnulacion { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.0000}", ApplyFormatInEditMode = true)]
        public decimal? ParidadUSDAnulacion { get; set; }

        public decimal? TotalAnulacion { get; set; }

        public bool isAnulada { get; set; }

        public bool? HayCuadroCuotas { get; set; }

        public bool HayAcuerdos { get; set; }

        public bool isZonaFranca { get; set; }

        public DateTime? FechaVentaAntigua { get; set; }

        public string CodigoCentroCosto { get; set; }

        public string CodigoProducto { get; set; }

        public string CodigoEspecialista { get; set; }

        public string CodigoEmpresa { get; set; }

        public string CodigoCobranza { get; set; }

        public string Observaciones { get; set; }

        public string ObservacionesDeclaracion { get; set; }

        public decimal ParsearADecimal(string valor)
        {
            decimal aux;
            if (decimal.TryParse(valor, out aux))
            {
                return aux;
            }
            else
                return 0;
        }

        public bool PlanillaGuardada { get; set; }
    }
}