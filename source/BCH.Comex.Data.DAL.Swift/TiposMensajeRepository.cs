using BCH.Comex.Core.Entities.Swift;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Data.DAL.Swift
{
    public class TiposMensajeRepository : GenericRepository<sw_tipos_msg, swiftEntities>
    {
        public TiposMensajeRepository(swiftEntities context)
            : base(context)
        {


        }

        public IList<sw_tipos_msg> GetTipoMensajesConFormato()
        {
            return context.proc_sw_trae_tiposMensajeConFormato_MS().ToList();
        }
    }
}
