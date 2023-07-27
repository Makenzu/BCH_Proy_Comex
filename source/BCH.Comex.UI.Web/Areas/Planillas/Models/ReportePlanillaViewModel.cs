using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.UI.Web.Areas.Planillas.Models
{
    public class ReportePlanillaViewModel
    {
        public DateTime Fecha { get; set; }
        public string Titulo { get; set; }
        public List<DatosPlanillaViewModel> Planillas { get; set; }
        public string Especialista { get; set; }
        public string CentroCosto { get; set; }
        public int NumPagina { get; set; }
    }
}
