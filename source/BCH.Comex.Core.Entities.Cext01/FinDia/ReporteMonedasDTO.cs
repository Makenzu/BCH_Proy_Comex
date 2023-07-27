using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FinDia
{
    public class ReporteMonedasDTO
    {
        public List<Tipo_CtaMon> TipoCuentas { get; set; }
        public List<Sum_Cta> SumaCuentas { get; set; }
    }
}
