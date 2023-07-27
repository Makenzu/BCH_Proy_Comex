using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Planillas.Models
{
    public class MovimientosCuentaCorrienteViewModel
    {
        public IEnumerable<SelectListItem> TiposCuenta { get; set; }
        
        [Display(Name="Tipo cuenta")]
        public int TipoCuenta { get; set; }

        public DateTime? Fecha { get; set; }

    }
}