using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Cext01.MT300Gestion
{
    public class ResultadoBusquedaMT300
    {
		public decimal id_procesados { get; set; }
		public decimal id_swift { get; set; }
		public string reference { get; set; }
		public decimal amount_mn { get; set; }
		public decimal amount_me { get; set; }
		public string beneficiary { get; set; }
		public decimal safekeeping { get; set; }
		public decimal rate { get; set; }
		public string booked_by { get; set; }
		public string estado_msg { get; set; }
		public string estado_interno { get; set; }
		public string fecha_proceso { get; set; }
		public string fecha_envio { get; set; }
		public string bic_destino { get; set; }
		public string codigo_moneda_mn { get; set; }
		public string codigo_moneda_me { get; set; }
		public string tipo_operacion { get; set; }
	}
}
