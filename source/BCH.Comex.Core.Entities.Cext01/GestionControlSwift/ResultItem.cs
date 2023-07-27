using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Cext01.GestionControlSwift
{
    public class ResultItem
    {
        public int Secuencia { get; set; }
        public int Sesion { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public string Referencia { get; set; }
        public string Beneficiario { get; set; }
        public string Moneda { get; set; }
        public double Monto { get; set; }
    }
}
