using BCH.Comex.Core.Entities.Swift;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Data.DAL.Swift
{
    public class ValorCamposRepository : GenericRepository<sw_tipos_msg, swiftEntities>
    {
        public ValorCamposRepository(swiftEntities context)
            : base(context){}

        public List<sw_valor_campos> LlenaMatrizValores(string tipo_msg)
        {
            return context.sw_valor_campos.Where(i => i.tipo_msg == tipo_msg || i.tipo_msg == "MT000").OrderByDescending(i => i.tipo_msg).OrderBy(i => i.tag_campo).ToList();
        }
    }
}
