using BCH.Comex.Core.Entities.Swift;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Data.DAL.Swift
{
    public class TipoCampoRepository : GenericRepository<sw_tipos_campos, swiftEntities>
    {
        public TipoCampoRepository(swiftEntities context)
            : base(context)
        {

        }
        
        public IList<proc_trae_tipo_campos_MS_Result> GetTipoCamposConMaximo()
        {
            return context.proc_trae_tipo_campos_MS().ToList();
        }

        public IList<sw_tipos_campos> GetTipoCampos(string codMT)
        {
            return context.sw_tipos_campos.Where(tc => tc.tipo_msg_tipcam == codMT).ToList();
        }
    }
}