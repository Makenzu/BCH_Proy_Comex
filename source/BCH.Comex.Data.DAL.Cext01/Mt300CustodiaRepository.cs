using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Core.Entities.Cext01.MT300Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace BCH.Comex.Data.DAL.Cext01
{
    public class Mt300CustodiaRepository : GenericRepository<Mt300Custodia, cext01Entities>
    {
        public Mt300CustodiaRepository(cext01Entities context)
            : base(context)
        {
        }

        public IList<Mt300Custodia> GetCustodia(string safekeeping)
        {
            return EjecutarSP<Mt300Custodia>("mt300F2.sce_mt300_obtener_custodia", safekeeping).ToList();
        }

    }
}
