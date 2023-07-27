using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Mcambio
{
    public class Lista_Moneda_Pais
    {
        public int CodMoneda { get; set; }
        public string SiglaMoneda { get; set; }
        public Nullable<int> CodPaisEquiv { get; set; }
    }
}
