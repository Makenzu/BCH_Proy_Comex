using BCH.Comex.Core.Entities.Cext01;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Data.DAL.Cext01
{
    public class BancoRepository : GenericRepository<sce_bic, cext01Entities>
    {
        public BancoRepository(cext01Entities context)
            : base(context)
        {
        }

        public IList<sce_bic> sce_bic_s02_MS(string swiftBanco, string secuencia)
        {
            return context.sce_bic_s02_MS(swiftBanco, secuencia).ToList();
        }

        public int? sce_cou_s03_MS(string codPais)
        {
            return (int?)context.sce_cou_s03_MS(codPais).FirstOrDefault();
        }
    }
}
