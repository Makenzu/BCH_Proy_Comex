using BCH.Comex.Core.Entities.Cext01.MT300Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Data.DAL.Cext01
{
    public class Mt300ArchivosProcesadosRepository : GenericRepository<Mt300ArchivoProcesado, cext01Entities>
    {
        public Mt300ArchivosProcesadosRepository(cext01Entities context)
            : base(context)
        {
        }

        public Mt300ArchivoProcesado GetArchivoProcesado(decimal id)
        {
            return EjecutarSP<Mt300ArchivoProcesado>("mt300F2.sce_mt300_obtener_archivo_procesado", id.ToString()).SingleOrDefault();
        }

        public Boolean ExistsArchivoProcesado(string reference)
        {
            return EjecutarSPConRetorno("mt300F2.sce_mt300_existe_archivo_procesado", String.Empty, reference) > 0;
        }

    }
}
