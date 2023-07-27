using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BCH.Comex.Core.BL.XGPL.Validators;


namespace BCH.Comex.UI.Web.Areas.Planillas.Models
{
    public class DeclaracionPlanillaViewModel
    {
       
        [Display(Name = "Num. Pre.")]
        public string NumeroPresentacion { get; set; }

        [DiaHabil]
        [Display(Name="Fec. Pre.")]
        //[DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaPresentacion { get; set; }

        public string FechaPresentacionString { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.00}", ApplyFormatInEditMode = true)]
        public decimal MontoUSD { get; set; }

        [DiaHabil]
        [Required(ErrorMessage = "Debe ingresar la fecha de Inicio.")]
        public DateTime FechaInicio { get; set; }

        [DiaHabil]
        [Required(ErrorMessage = "Debe ingresar la fecha de Término.")]
        public DateTime FechaTermino { get; set; }

        [Required(ErrorMessage = "Debe ingresar el número de declaración")]
        [Display(Name = "Num. Dec.")]
        public string NumeroDeclaracion { get; set; }

        [DiaHabil]
        [Required(ErrorMessage = "Debe ingresar la fecha de declaración.")]
        //[DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fec. Dec.")]
        public DateTime FechaDeclaracion { get; set; }

        public string FechaDeclaracionString { get; set; }

        [DiaHabil]
        [Display(Name = "Vto. Retorno")]
        public DateTime? FechaVencimientoRetorno { get; set; }

        public String FechaVencimientoRetornoString { get; set; }

        [Required(ErrorMessage = "Debe ingresar el monto a aplicar a la declaración.")]
        [Display(Name = "Mto. USD$ D.I.")]
        [DisplayFormat(DataFormatString = "{0:#,##0.00}", ApplyFormatInEditMode = true)]
        public decimal? MontoDI { get; set; }

        [Display(Name = "Mto.Interés D.I.")]
        [DisplayFormat(DataFormatString = "{0:#,##0.00}", ApplyFormatInEditMode = true)]
        public decimal? InteresDI { get; set; }

        [Required(ErrorMessage = "Debe ingresar el código de aduana.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:00}")]
        [Display(Name = "Cód. Aduana")]
        public int CodigoAduana { get; set; }

        public IEnumerable<SelectListItem> CodigosAduana { get; set; }

        public int Estado { get; set; }

        public decimal MontoUSDPlanilla { get; set; }
                
        public char TipoPlanilla { get; set; }

        public List<BCH.Comex.Core.BL.XGPL.DeclaracionPlanillaDTO> listado { get; set; }
    }
}