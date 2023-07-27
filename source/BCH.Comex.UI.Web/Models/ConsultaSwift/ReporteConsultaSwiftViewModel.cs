using BCH.Comex.Core.Entities.Swift;
using System;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Models.ConsultaSwift
{
    public class ReporteConsultaSwiftViewModel
    {
        public bool Enviados { get; set; }
        public string Casilla { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public string Filtro { get; set; }

        public IList<ResultadoBusquedaSwift> Registros { get; set; }
        public string Verbo { get; set; }

    }
}