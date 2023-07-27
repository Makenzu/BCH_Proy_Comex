using BCH.Comex.Core.Entities.Cext01;

namespace BCH.Comex.Data.DAL.Cext01
{
    class PrtyEjcMSRepository : GenericRepository<tbl_prty_ejc_MS, cext01Entities>
    {
        public PrtyEjcMSRepository(cext01Entities context) : base(context)
        {
        }
    }
}
