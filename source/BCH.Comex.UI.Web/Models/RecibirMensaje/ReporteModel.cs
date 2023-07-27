using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCH.Comex.UI.Web.Models.RecibirMensaje
{
    public class ReporteModel
    {
        public string Casilla { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public IList<ReporteItem> Result { get; set; }
        public string TipoMensaje { get; set; }
        public string Estado { get; set; }
    }

    public class ReporteItem
    {
        public int Sesion { get; set; }
        public int Secuencia { get; set; }
        public string TipoMt { get; set; }
        public string Referencia { get; set; }
        public string Beneficiario { get; set; }
        public string BancoEmisor { get; set; }
        public string Moneda { get; set; }
        public double Monto { get; set; }
        public string Prioridad { get; set; }
        public string Fecha { get; set; }
    }
    public class MensajeItem
    {
        public int Casilla { get; set; }
        public int Sesion { get; set; }
        public int Secuencia { get; set; }
        public int RutaLog { get; set; }
        public string Estado { get; set; }
        public string Comentario { get; set; }
    }
}
