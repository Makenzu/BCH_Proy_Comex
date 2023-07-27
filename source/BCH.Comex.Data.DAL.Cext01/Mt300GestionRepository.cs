using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Core.Entities.Cext01.MT300Gestion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace BCH.Comex.Data.DAL.Cext01
{
    public class Mt300GestionRepository : GenericRepository<ResultadoBusquedaMT300, cext01Entities>
    {
        public Mt300GestionRepository(cext01Entities context)
            : base(context)
        {
        }

        public IList<ResultadoBusquedaMT300> GetRegistrosEnviadosPag(bool usarFiltros, string referencia, string destino, string cuenta, DateTime? fecha, int rowOffset, int pageSize)
        {
            const string SQL = "mt300F2.sce_mt300_obtener_registros_enviados_pag @usar_filtros, @filtro_referencia, @filtro_destino, @filtro_cuenta, @filtro_fecha, @offset, @fetchRows";

            var usarFiltrosParam = new SqlParameter("usar_filtros", usarFiltros);
            var referenciaParam = new SqlParameter("filtro_referencia", SqlDbType.VarChar);
            referenciaParam.Value = !String.IsNullOrWhiteSpace(referencia) ? (object)referencia : DBNull.Value;
            var destinoParam = new SqlParameter("filtro_destino", SqlDbType.VarChar);
            destinoParam.Value = !String.IsNullOrWhiteSpace(destino) ? (object)destino : DBNull.Value;
            var cuentaParam = new SqlParameter("filtro_cuenta", SqlDbType.VarChar);
            cuentaParam.Value = !String.IsNullOrWhiteSpace(cuenta) ? (object)cuenta : DBNull.Value;
            var fechaParam = new SqlParameter("filtro_fecha", SqlDbType.Date);
            fechaParam.Value = fecha.HasValue ? (object)fecha : DBNull.Value;
            var offsetParam = new SqlParameter("offset", rowOffset);
            var fetchRowsParam = new SqlParameter("fetchRows", pageSize);

            return context.Database.SqlQuery<ResultadoBusquedaMT300>(SQL,
                usarFiltrosParam, referenciaParam, destinoParam, cuentaParam, fechaParam, offsetParam, fetchRowsParam).ToList();
            //return EjecutarSP<ResultadoBusquedaMT300>("mt300F2.sce_mt300_obtener_registros_enviados_pag", 
            //    usarFiltrosParam, referenciaParam, destinoParam, cuentaParam, fechaParam, rowOffset.ToString(), pageSize.ToString()).ToList();
        }

        public int GetRegistrosEnviadosTot(bool usarFiltros, string referencia, string destino, string cuenta, DateTime? fecha)
        {
            const string SQL = "mt300F2.sce_mt300_obtener_registros_enviados_tot @usar_filtros, @filtro_referencia, @filtro_destino, @filtro_cuenta, @filtro_fecha";

            var usarFiltrosParam = new SqlParameter("usar_filtros", usarFiltros);
            var referenciaParam = new SqlParameter("filtro_referencia", SqlDbType.VarChar);
            referenciaParam.Value = !String.IsNullOrWhiteSpace(referencia) ? (object)referencia : DBNull.Value;
            var destinoParam = new SqlParameter("filtro_destino", SqlDbType.VarChar);
            destinoParam.Value = !String.IsNullOrWhiteSpace(destino) ? (object)destino : DBNull.Value;
            var cuentaParam = new SqlParameter("filtro_cuenta", SqlDbType.VarChar);
            cuentaParam.Value = !String.IsNullOrWhiteSpace(cuenta) ? (object)cuenta : DBNull.Value;
            var fechaParam = new SqlParameter("filtro_fecha", SqlDbType.Date);
            fechaParam.Value = fecha.HasValue ? (object)fecha : DBNull.Value;

            var result = context.Database.SqlQuery<int>(SQL,
                usarFiltrosParam, referenciaParam, destinoParam, cuentaParam, fechaParam).SingleOrDefault();

            return result;
            //return EjecutarSP<ResultadoBusquedaMT300>("mt300F2.sce_mt300_obtener_registros_enviados_pag", 
            //    usarFiltrosParam, referenciaParam, destinoParam, cuentaParam, fechaParam, rowOffset.ToString(), pageSize.ToString()).ToList();
        }

    }
}
