using BCH.Comex.Core.Entities.Sbcor;

namespace BCH.Comex.Data.DAL.Sbcor
{
    public class PaisRepository : GenericRepository<sbc_cpai, sbcorEntities>
    {
        public PaisRepository(sbcorEntities context)
            : base(context)
        {

        }
    }
}
