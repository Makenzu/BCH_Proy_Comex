using BCH.Comex.Core.Entities.Cext01.MT300Common;
using System.Linq;

namespace BCH.Comex.Data.DAL.Cext01
{
    public class ArchivosRepository : GenericRepository<Archivo, cext01Entities>
    {
        public ArchivosRepository(cext01Entities context)
            : base(context)
        {
        }

        public Archivo AddArchivoMT300(Archivo archivo)
        {
            return EjecutarSP<Archivo>("mt300F2.sce_mt300_insertar_archivos",archivo.nombre, archivo.total_registros.ToString(), archivo.total_mt300_nuevos.ToString(), archivo.total_mt300_existentes.ToString(), archivo.total_registros_error.ToString(), archivo.estado, archivo.origen, archivo.resultado, archivo.tipo_archivo).First();
        }

        public Archivo UpdateArchivoMT300(Archivo archivo)
        {
            return EjecutarSP<Archivo>("mt300F2.sce_mt300_update_archivo", archivo.id_archivo.ToString(),archivo.total_registros.ToString(),archivo.total_mt300_nuevos.ToString(),archivo.total_mt300_existentes.ToString(),archivo.total_registros_error.ToString()).First();
        }

        public int UpdateArchivoEstadosFinProceso(decimal id_archivo)
        {
            return EjecutarSPConRetorno("mt300F2.sce_mt300_update_estados_fin_proceso", "", id_archivo.ToString());
        }
    }
}
