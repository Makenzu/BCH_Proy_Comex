using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BCH.Comex.UI.Web.Areas.Planillas.Models
{
    public class ListadoActualizacionPlanillasViewModel
    {
        public IList<ItemActualizacionPlanillasViewModel> Detalle { get; set; }
    }

    public class ItemActualizacionPlanillasViewModel
    {
        public string NumeroPresentacion { get; set; }

        [Display(Name = "Fecha presentación")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaPresentacion { get; set; }

        [Display(Name = "Monto total USD")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,##0.00}")]
        public decimal TotalDolar { get; set; }

        [Display(Name = "Número días")]
        public int? AntiguedadDias { get; set; }
        public string Rut { get; set; }
        public string RazonSocial { get; set; }

        public string FechaPresentacionString { get; set; }
    }
}
