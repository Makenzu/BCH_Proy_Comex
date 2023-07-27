using BCH.Comex.Core.Entities.Swift;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.UI.Web.Models.GestionControlSwift
{
    public class EstadisticasMensajesEnviadosSwiftViewModel
    {
        public bool Enviados { get; set; }
        public int Casilla { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public string Filtro { get; set; }

        public string NombreCasilla { get; set; }
        public int Suma { get; set; }
        public string CodBancoEm { get; set; }
        public string BranchEm { get; set; }
        public string NombreBanco { get; set; }
        public IEnumerable<IGrouping<int, proc_sw_env_estadist_msgDTO>> RegistrosEnviados { get; set; }

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