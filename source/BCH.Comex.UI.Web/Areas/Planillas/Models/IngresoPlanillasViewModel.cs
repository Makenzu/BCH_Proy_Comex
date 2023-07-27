using System;
using System.ComponentModel.DataAnnotations;

namespace BCH.Comex.UI.Web.Areas.Planillas.Models
{
    public class IngresoPlanillasViewModel
    {
        [Display(Name="Número de presentación")]
        [Required]
        public String NumeroPresentacion { get; set; }

        [Display(Name = "Fecha de presentación")]
        [Required]
        public DateTime FechaPresentacion { get; set; }

        public String FechaPresentacionString { get; set; }

        [Required]
        [StringLength(1)]
        [Display(Name = "Tipo planilla")]
        public String TipoPlanilla { get; set; }

        [Display(Name = "Número planilla")]
        public String NumeroPlanilla { get; set; }

        [Display(Name = "Mto. USD")]
        public decimal? MontoUSD { get; set; }

        public String Rut { get; set; }

        public String Nombre { get; set; }

        public int? AntiguedadDias { get; set; }
    }
}