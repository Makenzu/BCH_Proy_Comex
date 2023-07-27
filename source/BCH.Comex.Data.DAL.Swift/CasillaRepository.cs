using BCH.Comex.Core.Entities.Swift;

namespace BCH.Comex.Data.DAL.Swift
{
    public class CasillaRepository : GenericRepository<sw_casillas, swiftEntities>
    {
        public CasillaRepository(swiftEntities context)
            : base(context)
        {

        }

    }
}