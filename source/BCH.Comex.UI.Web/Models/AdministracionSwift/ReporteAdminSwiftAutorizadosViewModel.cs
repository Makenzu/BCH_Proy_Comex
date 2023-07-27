using BCH.Comex.Core.Entities.Swift;
using System;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Models.AdministracionSwift
{
    public class ReporteAdminSwiftAutorizadosViewModel
    {
        public bool Enviados { get; set; }
        public int Casilla { get; set; }
        public string NombreCasilla { get; set; }
        public string Estado { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public string Filtro { get; set; }

        public IList<proc_sw_env_trae_aut_rango_MS_Result> Registros { get; set; }
        public string Verbo
        {
            get
            {
                return "Autorizados";
            }
        }
    }
}