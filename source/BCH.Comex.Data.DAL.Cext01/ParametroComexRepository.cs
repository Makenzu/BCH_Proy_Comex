using BCH.Comex.Core.Entities.Cext01;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Data.DAL.Cext01
{
    public class ParametroComexRepository : GenericRepository<ParametroComex, cext01Entities>
    {
        public ParametroComexRepository(cext01Entities context)
            : base(context)
        {
        }

        public IList<ParametroComex> GetParametrosComex(string agrupacion2, string agrupacion3, string agrupacion4)
        {
            return EjecutarSP<ParametroComex>("sce_c50f_obtener_parametros", agrupacion2, agrupacion3, agrupacion4).ToList();
        }

	public IList<ParametroComex> GetParametrosMT300(string agrupacion2, string agrupacion3, string agrupacion4)
        {
            return EjecutarSP<ParametroComex>("mt300F2.sce_mt300_obtener_parametros", agrupacion2, agrupacion3, agrupacion4).ToList();
        }
    }
}
