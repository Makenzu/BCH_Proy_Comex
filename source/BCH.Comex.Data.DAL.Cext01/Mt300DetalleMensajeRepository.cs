using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Core.Entities.Cext01.MT300Planilla;

namespace BCH.Comex.Data.DAL.Cext01
{
    public class Mt300DetalleMensajeRepository : GenericRepository<DetalleMensajeMT300, cext01Entities>
    {
        public Mt300DetalleMensajeRepository(cext01Entities context)
            : base(context)
        {
        }

        public DetalleMensajeMT300 GetDetalleMensaje(Decimal? id_detalle)
        {
            const string SQL = "mt300F2.sce_mt300_obtener_detalle_mensaje @id";

            var idDetalleParam = new SqlParameter("id", id_detalle);

            return context.Database.SqlQuery<DetalleMensajeMT300>(SQL,
                idDetalleParam).First();
        }

        public DetalleMensajeMT300 SaveDetalleMensaje(DetalleMensajeMT300 mensaje)
        {

            DetalleMensajeMT300 resultado = new DetalleMensajeMT300();

            try
            {
                DbCommand commandExecSP = context.Database.Connection.CreateCommand();
                commandExecSP.CommandText = "exec mt300F2.sce_mt300_update_detalle @id_detalle, @safekeeping, @reference, @amount_mn, @amount_me, @value_date, @rate, @booked_by, @estado, @codigo_moneda_mn, @codigo_moneda_me ";
                commandExecSP.Parameters.Add(new SqlParameter("@id_detalle", mensaje.id_detalle));
                commandExecSP.Parameters.Add(new SqlParameter("@safekeeping", mensaje.safekeeping));
                commandExecSP.Parameters.Add(new SqlParameter("@reference", mensaje.reference));
                commandExecSP.Parameters.Add(new SqlParameter("@amount_mn", mensaje.amount_mn));
                commandExecSP.Parameters.Add(new SqlParameter("@amount_me", mensaje.amount_me));
                commandExecSP.Parameters.Add(new SqlParameter("@value_date", mensaje.value_date));
                commandExecSP.Parameters.Add(new SqlParameter("@rate", mensaje.rate));
                commandExecSP.Parameters.Add(new SqlParameter("@booked_by", mensaje.booked_by));
                commandExecSP.Parameters.Add(new SqlParameter("@estado", "PENDIENTE"));
                commandExecSP.Parameters.Add(new SqlParameter("@codigo_moneda_mn", mensaje.codigo_moneda_mn));
                commandExecSP.Parameters.Add(new SqlParameter("@codigo_moneda_me", mensaje.codigo_moneda_me));

                context.Database.Connection.Open();

                DbDataReader reader = commandExecSP.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        int col = 0;
                        resultado.id_detalle = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                        resultado.safekeeping = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                        resultado.reference = Utils.GetStringFromDataReader(reader, col++);
                        resultado.amount_mn = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                        resultado.amount_me = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                        resultado.value_date = (DateTime)Utils.GetFechaFromDataReader(reader, col++);
                        resultado.rate = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                        resultado.booked_by = (DateTime)Utils.GetFechaFromDataReader(reader, col++);
                        resultado.codigo_moneda_mn = Utils.GetStringFromDataReader(reader, col++);
                        resultado.codigo_moneda_me = Utils.GetStringFromDataReader(reader, col++);
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
