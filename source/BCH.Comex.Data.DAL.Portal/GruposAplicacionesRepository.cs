using BCH.Comex.Core.Entities.Portal;

namespace BCH.Comex.Data.DAL.Portal
{
    public class GruposAplicacionesRepository : GenericRepository<GrupoAplicacion, Portal.portalEntities>
    {
        public GruposAplicacionesRepository(portalEntities context) : base(context)
        {
        }
        
    }
}
