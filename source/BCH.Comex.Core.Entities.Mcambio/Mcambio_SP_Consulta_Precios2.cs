using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Mcambio
{
    public class Mcambio_SP_Consulta_Precios2
    {
        public int retorno_decimales { get; set; }
        public decimal precio_final { get; set; }
        public decimal monto_segunda_moneda { get; set; }
        public string identificador_consulta { get; set; }
        public int tiempo_consulta { get; set; }
        public System.DateTime fec_valuta { get; set; }
    }
}
