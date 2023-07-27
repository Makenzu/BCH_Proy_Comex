using BCH.Comex.Core.Entities.Swift;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Data.DAL.Swift
{
    public class MonedaRepository : GenericRepository<sw_monedas, swiftEntities>
    {
        public MonedaRepository(swiftEntities context)
            : base(context)
        {
        }

        public IList<sw_monedas> GetMonedasUsoBanco()
        {
            return context.sw_monedas.Where(m => m.uso_moneda_banco == "S").ToList();
        }
    }
}
