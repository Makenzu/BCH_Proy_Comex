using BCH.Comex.Core.Entities.Swift;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Data.DAL.Swift
{
    public class CamposMsgRepository : GenericRepository<sw_campos_msg, swiftEntities>
    {
        public CamposMsgRepository(swiftEntities context)
            : base(context)
        {
        }       

        public IList<sw_campos_msg> GetByFormatoCampo()
        {
            return context.sw_campos_msg.SqlQuery("select tag_campo, linea_campo, formato_campo from sw_campos_msg where formato_campo like \'06G%%\' or formato_campo like \'08G%%\'").ToList();
        }

        public IList<sw_campos_msg> GetFormatoCamposSWEM()
        {
            return context.proc_trae_formato_campos_MS().ToList();
        }
		
		public List<sw_campos_msg> LlenaMatrizMontos()
        {
            return context.sw_campos_msg.SqlQuery("select tag_campo, linea_campo, formato_campo, nombre_campo, largo_campo from sw_campos_msg where (formato_campo like \'06G 03C%%\' or formato_campo like \'03C%%\' or formato_campo like \'03A%%\' or formato_campo like \'01A 06G 03C%%\' or formato_campo like \'01A 03D 02A 03C 15N%%\' or formato_campo like \'05D 03C 15N%%\')").ToList();
        }
		
		public List<sw_campos_msg> LlenaMatrizBancos()
        {
            return context.sw_campos_msg.SqlQuery("select tag_campo, linea_campo, formato_campo, nombre_campo, largo_campo from sw_campos_msg where formato_campo like \'11T%%\'").ToList();
        }

 		public List<sw_campos_msg> LlenaMatrizFechas()
        {
            return context.sw_campos_msg.SqlQuery("select tag_campo, linea_campo, formato_campo, nombre_campo, largo_campo from sw_campos_msg where formato_campo like \'06G%%\' or formato_campo like \'08G%%\'").ToList();
        }



    }
}
