using BCH.Comex.Core.BL.XGPL.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace BCH.Comex.UI.Web.Areas.Planillas.Models
{
    public class VisibleExportacionAnuladaViewModel
    {
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:000000-0}")]
        public string NumeroPresentacion { get; set; }   //# Presentación.

        [Required]
        [DiaHabil]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name="Fecha de Presentación")]
        public DateTime? FechaPresentacion { get; set; }   //Fecha Presentación.


        [Required]
        [DiaHabil]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Presentación")]
        public DateTime? FechaPresentacionNueva { get; set; }   //Fecha Presentación.


        [Required]
        public string TipoAnulacion { get; set; }

        [Required]
        public int CodigoPlazaBancoCentral { get; set; }

        public string NombrePlazaBancoCentral { get; set; }

        [Required]
        public string RutExportador { get; set; }
        public string NombreParty { get; set; }
        public string DireccionParty { get; set; }


        // Datos planilla original
        [Required]
        public int CodigoEntidadAutorizadaOriginal { get; set; }
        public string NombreEntidadAutorizadaOriginal { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0000000}")]
        // Datos Presentación original
        public string NumeroPresentacionOriginal { get; set; }

        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaPresentacionOriginal { get; set; }

        [Required]
        public int CodigoTipoPlanillaOriginal { get; set; }

        [Required]
        public string TipoPlanillaOriginal { get; set; }

        [Required]
        public int CodigoPlazaBancoCentralOriginal { get; set; }
        public string NombrePlazaBancoCentralOriginal { get; set; }

        [Required]
        public decimal MontoDolaresOriginal { get; set; }

        [Required, DisplayFormat(DataFormatString="{0:0.0000000000}", ApplyFormatInEditMode=true)]
        public decimal ParidadOriginal { get; set; }


        // Datos declaracion exportación
        //[Required]
        public int CodigoAduana { get; set; }
        public string NombreAduana { get; set; }

        //[Required]
        public string NumeroDeclaracion { get; set; }

        //[Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaAceptacion { get; set; }

        //[Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaVencimientoRetorno { get; set; }
        
        [DataType(DataType.MultilineText)]
        [StringLength(255)]
        public string Observaciones { get; set; }

        // Monto anulado
        [Required]
        public int CodigoMonedaAnulacion { get; set; }

        [Required]
        public string MonedaAnulacion { get; set; }

        [Required]
        public decimal MontoAnulado { get; set; }

        [Required, DisplayFormat(DataFormatString = "{0:0.0000000000}", ApplyFormatInEditMode = true)]
        public decimal ParidadAnulado { get; set; }

        [Required]
        //[AssertThat("MontoAnuladoUS <= MontoDolaresOriginal")]
        public decimal MontoAnuladoUS { get; set; }

        [Required]
        public decimal MontoAnuladoUSParidadOriginal { get; set; }

        public bool PlanillaGuardada { get; set; }
    }
}