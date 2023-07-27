using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCH.Comex.Core.Entities.Cext01.MT300Common;
using BCH.Comex.Core.Entities.Cext01.MT300Planilla;

namespace BCH.Comex.Data.DAL.Cext01
{
    public class Mt300PlanillaRepository : GenericRepository<ResultadoArchivosMT300, cext01Entities>
    {

        public Mt300PlanillaRepository(cext01Entities context)
            : base(context)
        {
        }

        public IList<ResultadoArchivosMT300> GetArchivosPag(DateTime? fecha , int rowOffset, int pageSize)
        {
            const string SQL = "mt300F2.sce_mt300_obtener_archivos_pag @filtro_fecha, @offset, @fetchRows";

            var fechaParam = new SqlParameter("filtro_fecha", SqlDbType.Date);
            fechaParam.Value = fecha.HasValue ? (object)fecha : DBNull.Value;
            var offsetParam = new SqlParameter("offset", rowOffset);
            var fetchRowsParam = new SqlParameter("fetchRows", pageSize);

            return context.Database.SqlQuery<ResultadoArchivosMT300>(SQL,
                fechaParam, offsetParam, fetchRowsParam).ToList();
            //return EjecutarSP<ResultadoBusquedaMT300>("mt300F2.sce_mt300_obtener_registros_enviados_pag", 
            //    usarFiltrosParam, referenciaParam, destinoParam, cuentaParam, fechaParam, rowOffset.ToString(), pageSize.ToString()).ToList();
        }

        public int GetArchivosTot(DateTime? fecha)
        {
            const string SQL = "mt300F2.sce_mt300_obtener_archivos_tot @filtro_fecha";

            var fechaParam = new SqlParameter("filtro_fecha", SqlDbType.Date);
            fechaParam.Value = fecha.HasValue ? (object)fecha : DBNull.Value;

            var result = context.Database.SqlQuery<int>(SQL,
                fechaParam).SingleOrDefault();

            return result;
            //return EjecutarSP<ResultadoBusquedaMT300>("mt300F2.sce_mt300_obtener_registros_enviados_pag", 
            //    usarFiltrosParam, referenciaParam, destinoParam, cuentaParam, fechaParam, rowOffset.ToString(), pageSize.ToString()).ToList();
        }

        public ResumenArchivosMT300 GetResumen(DateTime? fecha)
        {
            const string SQL = "mt300F2.sce_mt300_obtener_resumen @filtro_fecha";

            var fechaParam = new SqlParameter("filtro_fecha", SqlDbType.Date);
            fechaParam.Value = fecha.HasValue ? (object)fecha : DBNull.Value;

            return context.Database.SqlQuery<ResumenArchivosMT300>(SQL,
                fechaParam).First();
            //return EjecutarSP<ResultadoBusquedaMT300>("mt300F2.sce_mt300_obtener_registros_enviados_pag", 
            //    usarFiltrosParam, referenciaParam, destinoParam, cuentaParam, fechaParam, rowOffset.ToString(), pageSize.ToString()).ToList();
        }

        public IList<ResultadoDetalleArchivosMT300> GetDetalleArchivosPag(Decimal? id_archivo, int rowOffset, int pageSize)
        {
            const string SQL = "mt300F2.sce_mt300_obtener_detalle_archivos_pag @id_archivo, @offset, @fetchRows";

            var idArchivoParam = new SqlParameter("id_archivo", id_archivo);
            var offsetParam = new SqlParameter("offset", rowOffset);
            var fetchRowsParam = new SqlParameter("fetchRows", pageSize);

            return context.Database.SqlQuery<ResultadoDetalleArchivosMT300>(SQL,
                idArchivoParam, offsetParam, fetchRowsParam).ToList();
            //return EjecutarSP<ResultadoBusquedaMT300>("mt300F2.sce_mt300_obtener_registros_enviados_pag", 
            //    usarFiltrosParam, referenciaParam, destinoParam, cuentaParam, fechaParam, rowOffset.ToString(), pageSize.ToString()).ToList();
        }

        public int GetDetalleArchivosTot(decimal? id_archivo)
        {
            const string SQL = "mt300F2.sce_mt300_obtener_detalle_archivos_tot @id_archivo";

            var id_archivoParam = new SqlParameter("id_archivo", id_archivo);

            var result = context.Database.SqlQuery<int>(SQL,
                id_archivoParam).SingleOrDefault();

            return result;
            //return EjecutarSP<ResultadoBusquedaMT300>("mt300F2.sce_mt300_obtener_registros_enviados_pag", 
            //    usarFiltrosParam, referenciaParam, destinoParam, cuentaParam, fechaParam, rowOffset.ToString(), pageSize.ToString()).ToList();
        }

        public ResumenDetalleArchivosMT300 GetResumenDetalle(decimal? id_archivo)
        {
            const string SQL = "mt300F2.sce_mt300_obtener_resumen_detalle @id_archivo";

            var id_archivoParam = new SqlParameter("id_archivo", id_archivo);


            return context.Database.SqlQuery<ResumenDetalleArchivosMT300>(SQL,
                id_archivoParam).First();
            //return EjecutarSP<ResultadoBusquedaMT300>("mt300F2.sce_mt300_obtener_registros_enviados_pag", 
            //    usarFiltrosParam, referenciaParam, destinoParam, cuentaParam, fechaParam, rowOffset.ToString(), pageSize.ToString()).ToList();
        }

        public DetalleMensajeMT300 GetDetalleMensaje(Decimal? id_detalle)
        {
            const string SQL = "mt300F2.sce_mt300_obtener_detalle_mensaje @id_detalle";

            var idDetalleParam = new SqlParameter("id_detalle", id_detalle);

            return context.Database.SqlQuery<DetalleMensajeMT300>(SQL,
                idDetalleParam).First();
        }

        public List<ArchivoDetalle> getArchivoDetalle(Decimal? id_detalle)
        {
            const string SQL = "mt300F2.sce_mt300_obtener_archivo_detalle @id";

            var idDetalleParam = new SqlParameter("id", id_detalle);

            return context.Database.SqlQuery<ArchivoDetalle>(SQL,
                idDetalleParam).ToList();
        }



    }
}
