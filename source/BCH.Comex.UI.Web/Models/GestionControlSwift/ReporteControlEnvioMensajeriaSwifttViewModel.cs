using BCH.Comex.Core.Entities.Swift;
using System;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Models.GestionControlSwift
{
    public class ReporteControlEnvioMensajeriaSwifttViewModel
    {
        public bool Enviados { get; set; }
        public int Direccion { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        //public string[][] Registros { get; set; }
        //public  Registros { get; set; }
        public IList<proc_sw_env_trae_filesDTO> Registros { get; set; }
        public string Verbo
        {
            get
            {
                if (this.Enviados)
                {
                    return "Control Envío Mensajería";
                }
                else
                {
                    return "Control Envío Mensajería";//"recepcionados";
                }
            }
        }
    }
}