using BCH.Comex.Core.Entities.Swift;
using System;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Models.GestionControlSwift
{
    public class ReporteEstadisticaSwift
    {

        public bool Enviados { get; set; }
        public string Casilla { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public string Filtro { get; set; }

        public IList<proc_sw_env_estadist_msgDTO> RegistrosEnviados { get; set; }
        public IList<proc_sw_rec_estadist_msgDTO> RegistrosRecibidos { get; set; }
        public string Verbo
        {
            get
            {
                if (this.Enviados)//if (this.Enviados)
                {
                    return "enviados";
                }
                else
                {
                    return "enviados";//return "recepcionados";
                }
            }
        }

    }
}