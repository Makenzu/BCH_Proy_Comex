using BCH.Comex.Core.Entities.Portal;
using System.Linq;

namespace BCH.Comex.Data.DAL.Portal
{
    public class AplicacionesRepository : GenericRepository<Aplicacion, Portal.portalEntities>
    {
        public AplicacionesRepository(portalEntities context) : base(context)
        {
        }

        /// <summary>
        /// Buscar parametros en la tabla tbl_sce_tabcomex_vchar
        /// </summary>
        /// <param name="parametro">Nombre del parametro</param>
        /// <returns></returns>
        public string tbl_sce_tabcomex(string parametro)
        {
            return context.proc_sel_TBLSceTabcomex_MS(parametro).Aggregate((a,b) => a + b);
        }
    }
}
