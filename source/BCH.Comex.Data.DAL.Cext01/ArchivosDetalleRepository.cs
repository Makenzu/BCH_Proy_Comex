using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01.MT300Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace BCH.Comex.Data.DAL.Cext01
{
    public class ArchivosDetalleRepository : GenericRepository<ArchivoDetalle, cext01Entities>
    {
        public ArchivosDetalleRepository(cext01Entities context)
            : base(context)
        {
        }

        public ArchivoDetalle AddArchivoDetalleMT300(ArchivoDetalle archivoDetalle)
        {
            ArchivoDetalle resultado = new ArchivoDetalle();

            try
            {
                DbCommand commandExecSP = context.Database.Connection.CreateCommand();
                commandExecSP.CommandText = "exec mt300F2.sce_mt300_insertar_archivos_detalles @id_archivo, @reference, @amount_mn, @amount_me, @beneficiary, @safekeeping, @value_date, @rate, @booked_by, @flag_nack, @flag_validaciones, @flag_existente, @flag_formato, @flag_citi, @estado, @resultado, @codigo_moneda_mn, @codigo_moneda_me, @campo22C, @campo82A, @campo87A, @campo53A, @campo57A, @tipo_operacion, @exec_time_hhmmss  ";
                commandExecSP.Parameters.Add(new SqlParameter("@id_archivo", archivoDetalle.id_archivo));
                commandExecSP.Parameters.Add(new SqlParameter("@reference", archivoDetalle.reference));
                commandExecSP.Parameters.Add(new SqlParameter("@amount_mn", archivoDetalle.amount_mn));
                commandExecSP.Parameters.Add(new SqlParameter("@amount_me", archivoDetalle.amount_me));
                commandExecSP.Parameters.Add(new SqlParameter("@beneficiary", archivoDetalle.beneficiary));
                commandExecSP.Parameters.Add(new SqlParameter("@safekeeping", archivoDetalle.safekeeping));
                commandExecSP.Parameters.Add(new SqlParameter("@value_date", archivoDetalle.value_date));
                commandExecSP.Parameters.Add(new SqlParameter("@rate", archivoDetalle.rate));
                commandExecSP.Parameters.Add(new SqlParameter("@booked_by", archivoDetalle.booked_by));
                commandExecSP.Parameters.Add(new SqlParameter("@flag_nack", archivoDetalle.flag_nack));
                commandExecSP.Parameters.Add(new SqlParameter("@flag_validaciones", archivoDetalle.flag_validaciones));
                commandExecSP.Parameters.Add(new SqlParameter("@flag_existente", archivoDetalle.flag_existente));
                commandExecSP.Parameters.Add(new SqlParameter("@flag_formato", archivoDetalle.flag_formato));
                commandExecSP.Parameters.Add(new SqlParameter("@flag_citi", archivoDetalle.flag_citi));
                commandExecSP.Parameters.Add(new SqlParameter("@estado", archivoDetalle.estado));
                commandExecSP.Parameters.Add(new SqlParameter("@resultado", archivoDetalle.resultado));
                commandExecSP.Parameters.Add(new SqlParameter("@codigo_moneda_mn", archivoDetalle.codigo_moneda_mn));
                commandExecSP.Parameters.Add(new SqlParameter("@codigo_moneda_me", archivoDetalle.codigo_moneda_me));
                commandExecSP.Parameters.Add(new SqlParameter("@campo22C", archivoDetalle.campo22C));
                commandExecSP.Parameters.Add(new SqlParameter("@campo82A", archivoDetalle.campo82A));
                commandExecSP.Parameters.Add(new SqlParameter("@campo87A", archivoDetalle.campo87A));
                commandExecSP.Parameters.Add(new SqlParameter("@campo53A", archivoDetalle.campo53A));
                commandExecSP.Parameters.Add(new SqlParameter("@campo57A", archivoDetalle.campo57A));
                commandExecSP.Parameters.Add(new SqlParameter("@tipo_operacion", archivoDetalle.tipo_operacion));
                commandExecSP.Parameters.Add(new SqlParameter("@exec_time_hhmmss", archivoDetalle.executionTimehhmmss));

                context.Database.Connection.Open();

                DbDataReader reader = commandExecSP.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        int col = 0;
                        resultado.id_archivo_detalle = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                        resultado.id_archivo = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                        resultado.reference = Utils.GetStringFromDataReader(reader, col++);
                        resultado.amount_mn = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                        resultado.amount_me = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                        resultado.beneficiary = Utils.GetStringFromDataReader(reader, col++);
                        resultado.safekeeping = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                        resultado.value_date = (DateTime)Utils.GetFechaFromDataReader(reader, col++);
                        resultado.rate = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                        resultado.booked_by = (DateTime)Utils.GetFechaFromDataReader(reader, col++);
                        resultado.flag_nack = (byte)Utils.GetIntByteFromDataReader(reader, col++);
                        resultado.flag_validaciones = (byte)Utils.GetIntByteFromDataReader(reader, col++);
                        resultado.flag_existente = (byte)Utils.GetIntByteFromDataReader(reader, col++);
                        resultado.flag_formato = (byte)Utils.GetIntByteFromDataReader(reader, col++);
                        resultado.flag_citi = (byte)Utils.GetIntByteFromDataReader(reader, col++);
                        resultado.estado = Utils.GetStringFromDataReader(reader, col++);
                        resultado.resultado = Utils.GetStringFromDataReader(reader, col++);
                        resultado.fecha_carga = (DateTime)Utils.GetFechaFromDataReader(reader, col++);
                        resultado.fecha_actualizacion = Utils.GetFechaFromDataReader(reader, col++);
                        resultado.codigo_moneda_mn = Utils.GetStringFromDataReader(reader, col++);
                        resultado.codigo_moneda_me = Utils.GetStringFromDataReader(reader, col++);
                        resultado.campo22C = Utils.GetStringFromDataReader(reader, col++);
                        resultado.campo82A = Utils.GetStringFromDataReader(reader, col++);
                        resultado.campo87A = Utils.GetStringFromDataReader(reader, col++);
                        resultado.campo53A = Utils.GetStringFromDataReader(reader, col++);
                        resultado.campo57A = Utils.GetStringFromDataReader(reader, col++);
                        resultado.tipo_operacion = Utils.GetStringFromDataReader(reader, col++);
                        resultado.executionTimehhmmss = Utils.GetStringFromDataReader(reader, col++);
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


        public List<ArchivoDetalle> getArchivoDetalleDesdeMesaMT300()
        {
            ArchivoDetalle resultado = new ArchivoDetalle();
            List<ArchivoDetalle> listaResultado = new List<ArchivoDetalle>();

            using (var trace = new Tracer())
            {
                try
                {
                    DbCommand commandExecSP = context.Database.Connection.CreateCommand();
                    commandExecSP.CommandText = "exec mt300F2.sce_mt300_obtener_detalle_mesacambio";

                    context.Database.Connection.Open();

                    DbDataReader reader = commandExecSP.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            int col = 0;
                            resultado = new ArchivoDetalle();
                            resultado.id_archivo_detalle = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                            resultado.id_archivo = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                            resultado.reference = Utils.GetStringFromDataReader(reader, col++);
                            resultado.amount_mn = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                            resultado.amount_me = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                            resultado.beneficiary = Utils.GetStringFromDataReader(reader, col++);
                            resultado.safekeeping = Utils.GetDecimalFromDataReader(reader, col++) == null ? 0 : (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                            resultado.value_date = (DateTime)Utils.GetFechaFromDataReader(reader, col++);
                            resultado.rate = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                            resultado.booked_by = (DateTime)Utils.GetFechaFromDataReader(reader, col++);
                            resultado.flag_nack = (byte)Utils.GetIntByteFromDataReader(reader, col++);
                            resultado.flag_validaciones = (byte)Utils.GetIntByteFromDataReader(reader, col++);
                            resultado.flag_existente = (byte)Utils.GetIntByteFromDataReader(reader, col++);
                            resultado.flag_formato = (byte)Utils.GetIntByteFromDataReader(reader, col++);
                            resultado.flag_citi = (byte)Utils.GetIntByteFromDataReader(reader, col++);
                            resultado.estado = Utils.GetStringFromDataReader(reader, col++);
                            resultado.resultado = Utils.GetStringFromDataReader(reader, col++);
                            resultado.fecha_carga = (DateTime)Utils.GetFechaFromDataReader(reader, col++);
                            resultado.fecha_actualizacion = Utils.GetFechaFromDataReader(reader, col++);
                            resultado.codigo_moneda_mn = Utils.GetStringFromDataReader(reader, col++);
                            resultado.codigo_moneda_me = Utils.GetStringFromDataReader(reader, col++);
                            resultado.campo22C = Utils.GetStringFromDataReader(reader, col++);
                            resultado.campo82A = Utils.GetStringFromDataReader(reader, col++);
                            resultado.campo87A = Utils.GetStringFromDataReader(reader, col++);
                            resultado.campo53A = Utils.GetStringFromDataReader(reader, col++);
                            resultado.campo57A = Utils.GetStringFromDataReader(reader, col++);
                            resultado.tipo_operacion = Utils.GetStringFromDataReader(reader, col++);
                            listaResultado.Add(resultado);
                        }
                    }
                }
                catch (Exception ex)
                {
                    trace.TraceException("Error ", ex);
                    throw;
                }
                finally
                {
                    context.Database.Connection.Close();
                }

            }
            return listaResultado;
        }
    }
}
