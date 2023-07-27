using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Mcambio
{
    public class ClassResultConsultDealsDispon
    {
        public string status { get; set; }
        public string MsgSP { get; set; }
        public List<Mcambio_Consulta_Deals_Disponible> data { get; set; }
        public int lg { get; set; }
        public Nullable<int> dps { get; set; }
        public Nullable<int> das { get; set; }
    }
}
