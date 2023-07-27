using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Cext01.MT300Planilla
{
    public class DetalleMensajeMT300
    {
        public decimal id_archivo { get; set; }
        public decimal id_detalle { get; set; }
        public decimal safekeeping { get; set; }
        public string reference { get; set; }
        public DateTime value_date { get; set; }
        public DateTime booked_by { get; set; }
        public decimal rate { get; set; }
        public decimal amount_me {get;set; }
        public decimal amount_mn { get; set; }
        public string codigo_moneda_me { get; set; }
        public string codigo_moneda_mn { get; set; }
        public string mensajes { get; set; }
        public string estado { get; set; }
        public string exec_time_hhmmss { get; set; }

    }
}
