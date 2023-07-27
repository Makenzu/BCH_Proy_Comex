using BCH.Comex.Core.Entities.Swift;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.UI.Web.Models.GestionControlSwift
{
    public class ReporteControlRecepcionMensajesSwiftViewModel
    {
        public bool Enviados { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public IEnumerable<IGrouping<int, proc_sw_rec_trae_resumen_msgDTO>> Registros { get; set; }
        public string Verbo
        {
            get
            {
                if (this.Enviados)
                {
                    return "Archivo de Mensajes Terminados";
                }
                else
                {
                    return "Archivo de Mensajes Terminados";
                }
            }
        }
    }
}