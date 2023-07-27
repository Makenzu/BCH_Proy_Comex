using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Core.Entities.Cext01.MT300Common;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace BCH.Comex.Data.DAL.Cext01
{
    public class Mt300BitacoraRepository : GenericRepository<Mt300Bitacora, cext01Entities>
    {
        public Mt300BitacoraRepository(cext01Entities context)
            : base(context)
        {
        }

        public Mt300Bitacora AddBitacoraMT300(Mt300Bitacora bitacora)
        {
            Mt300Bitacora resultado = new Mt300Bitacora();

            try
            {
                DbCommand commandExecSP = context.Database.Connection.CreateCommand();
                commandExecSP.CommandText = "exec mt300F2.sce_mt300_insertar_bitacora @id_archivo, @id_procesados, @id_archivo_detalle, @usuario, @resultado, @resultado_1, @resultado_2, @tipo_movimiento";

                SqlParameter idArchivo = new SqlParameter("@id_archivo", SqlDbType.Int);

                if (bitacora.id_archivo == 0 || bitacora.id_archivo == null)
                    idArchivo.Value = DBNull.Value;
                else
                    idArchivo.Value = bitacora.id_archivo;

                SqlParameter idArchivoProcesados = new SqlParameter("@id_procesados", SqlDbType.Int);

                if (bitacora.id_procesados == 0 || bitacora.id_procesados == null)
                    idArchivoProcesados.Value = DBNull.Value;
                else
                    idArchivoProcesados.Value = bitacora.id_procesados;

                SqlParameter idArchivoDetalle = new SqlParameter("@id_archivo_detalle", SqlDbType.Int);

                if (bitacora.id_archivo_detalle == 0 || bitacora.id_archivo_detalle == null)
                    idArchivoDetalle.Value = DBNull.Value;
                else
                    idArchivoDetalle.Value = bitacora.id_archivo_detalle;

                commandExecSP.Parameters.Add(idArchivo);
                commandExecSP.Parameters.Add(idArchivoProcesados);
                commandExecSP.Parameters.Add(idArchivoDetalle);

                commandExecSP.Parameters.Add(new SqlParameter("@usuario", bitacora.usuario));

                SqlParameter usuario = new SqlParameter("@resultado", SqlDbType.NVarChar);

                if (String.IsNullOrEmpty(bitacora.resultado))
                    usuario.Value = DBNull.Value;
                else
                    usuario.Value = bitacora.resultado;

                commandExecSP.Parameters.Add(usuario);

                SqlParameter resultado_1 = new SqlParameter("@resultado_1", SqlDbType.NVarChar);

                if (String.IsNullOrEmpty(bitacora.resultado_1))
                    resultado_1.Value = DBNull.Value;
                else
                    resultado_1.Value = bitacora.resultado_1;

                commandExecSP.Parameters.Add(resultado_1);

                SqlParameter resultado_2 = new SqlParameter("@resultado_2", SqlDbType.NVarChar);

                if (String.IsNullOrEmpty(bitacora.resultado_2))
                    resultado_2.Value = DBNull.Value;
                else
                    resultado_2.Value = bitacora.resultado_1;

                commandExecSP.Parameters.Add(resultado_2);

                commandExecSP.Parameters.Add(new SqlParameter("@tipo_movimiento", bitacora.tipo_movimiento));

                context.Database.Connection.Open();

                DbDataReader reader = commandExecSP.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        int col = 0;
                        resultado.id_bitacora = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                        resultado.id_archivo = Utils.GetDecimalFromDataReader(reader, col++);
                        resultado.id_procesados = Utils.GetDecimalFromDataReader(reader, col++);
                        resultado.id_archivo_detalle = Utils.GetDecimalFromDataReader(reader, col++);
                        resultado.usuario = Utils.GetStringFromDataReader(reader, col++);
                        resultado.resultado = Utils.GetStringFromDataReader(reader, col++);
                        resultado.resultado_1 = Utils.GetStringFromDataReader(reader, col++);
                        resultado.resultado_2= Utils.GetStringFromDataReader(reader, col++);
                        resultado.tipo_movimiento = Utils.GetStringFromDataReader(reader, col++);
                        resultado.fecha_registro = (DateTime)Utils.GetFechaFromDataReader(reader, col);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!ExceptionPolicy.HandleException(ex, "PoliticaDALFundTransfer")) throw;
            }
            finally
            {
                context.Database.Connection.Close();
            }

            return resultado;
        }
    }
}
