using BCH.Comex.Core.Entities.Cext01;
using System;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.InicioDia.Views.Models
{
    public class ReporteNovedadesViewModel
    {
        public string Usuario { get; set; }
        public DateTime FechaReporte { get; set; }
        public IList<sce_nov_s01_MS_Result> Novedades { get; set; }

        public ReporteNovedadesViewModel()
        {
            this.Novedades = new List<sce_nov_s01_MS_Result>();
        }
    }
}