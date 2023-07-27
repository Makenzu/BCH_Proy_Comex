using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Cext01.AutorizacionSwift
{
    public class ResultItem
    {
        public int NroMensaje { get; set; }
        public string Tipo { get; set; }
        public int Unidad { get; set; }
        public string Moneda { get; set; }
        public double Monto { get; set; }
        public string Referencia { get; set; }
        public string Beneficiario { get; set; }
        public string BancoReceptor { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}
