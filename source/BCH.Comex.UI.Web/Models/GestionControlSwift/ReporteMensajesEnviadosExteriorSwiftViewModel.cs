using BCH.Comex.Core.Entities.Swift;
using System;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Models.GestionControlSwift
{
    public class ReporteMensajesEnviadosExteriorSwiftViewModel
    {
        public bool Enviados { get; set; }
        public int Casilla { get; set; }
        public string NombreCasilla { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public string Filtro { get; set; }

        public IList<proc_sw_env_trae_env_rangoDTO> Registros { get; set; }


        public string Verbo
        {
            get
            {
                if (this.Enviados)
                {
                    return "enviados";
                }
                else
                {
                    return "enviados";//"recepcionados";
                }
            }
        }
    }
}