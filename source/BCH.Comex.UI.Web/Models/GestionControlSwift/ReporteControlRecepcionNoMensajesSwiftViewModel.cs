using BCH.Comex.Core.Entities.Swift;
using System;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Models.GestionControlSwift
{
    public class ReporteControlRecepcionNoMensajesSwiftViewModel
    {
        public bool Enviados { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        //public IList<proc_sw_rec_trae_resumen_msgDTO> Registros { get; set; }
        public IList<proc_sw_rec_controlDTO> Registros { get; set; }
        public string[][] RegistrosStr { get; set; }
        public string Verbo
        {
            get
            {
                if (this.Enviados)
                {
                    return "Reporte de Mensajes No Recibidos";
                }
                else
                {
                    return "Reporte de Mensajes No Recibidos";
                }
            }
        }
        public int SumaMensaje { get; set; }
    }
}
