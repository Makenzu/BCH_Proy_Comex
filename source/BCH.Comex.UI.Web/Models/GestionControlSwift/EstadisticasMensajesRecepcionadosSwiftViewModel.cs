using BCH.Comex.Core.Entities.Swift;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.UI.Web.Models.GestionControlSwift
{
    public class EstadisticasMensajesRecepcionadosSwiftViewModel
    {

        public string Cod_Banco_Rec { get; set; }
        public string Nombre_Banco { get; set; }
        public bool Enviados { get; set; }
        public int Casilla { get; set; }

        public string NombreCasilla { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public string Filtro { get; set; }
        public int Suma { get; set; }
        public IEnumerable<IGrouping<int, proc_sw_rec_estadist_msgDTO>> RegistrosRecibidos { get; set; }
        public string Verbo
        {
            get
            {
                if (this.Enviados)
                {
                    return "recepcionados";
                }
                else
                {
                    return "recepcionados";
                }
            }
        }
    }
}