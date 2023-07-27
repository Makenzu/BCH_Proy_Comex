using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.Data.DAL.Swift.DTO;


namespace BCH.Comex.Data.DAL.Swift
{
    public class MensajeRepository : GenericRepository<sw_msgsend, swiftEntities>
    {
        public enum Direccion : byte
        {
            Enviado = 0,
            Recibido = 1
        }

        public MensajeRepository(swiftEntities context) : base(context)
        {
        }

        /// <summary>
        /// Extrae el estado de un Mensaje Swift
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="secuencia"></param>
        /// <returns></returns>
        public String Proc_Sw_Valida_Estado_Msg_MS(int sesion, int secuencia)
        {
            try
            {
                using (DbCommand commandExecSP = context.Database.Connection.CreateCommand())
                {
                    //DbCommand commandExecSP = context.Database.Connection.CreateCommand();
                    commandExecSP.CommandText = "exec proc_sw_valida_estado_msg_MS @sesion, @secuencia";

                    DbParameter param = commandExecSP.CreateParameter();
                    param.DbType = System.Data.DbType.Int32;
                    param.ParameterName = "sesion";
                    param.Value = sesion;
                    commandExecSP.Parameters.Add(param);

                    param = commandExecSP.CreateParameter();
                    param.DbType = System.Data.DbType.Int32;
                    param.ParameterName = "secuencia";
                    param.Value = secuencia;
                    commandExecSP.Parameters.Add(param);

                    context.Database.Connection.Open();
                    DbDataReader reader = commandExecSP.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            return Utils.GetStringFromDataReader(reader, 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (var tracer = new Tracer())
                {
                    tracer.TraceException("Alerta: ", ex);
                }
                if (!ExceptionPolicy.HandleException(ex, "PoliticaDALFundTransfer")) throw;
            }
            finally
            {
                context.Database.Connection.Close();
            }
            return null;
        }

    /// <summary>
    /// Graba Cambio de Casilla para un mensaje Swift
    /// </summary>
    /// <param name="sesion"></param>
    /// <param name="secuencia"></param>
    /// <param name="rut"></param>
    /// <param name="observacion"></param>
    /// <returns>Devuelve:
    ///     0       :   Si no hay errores
    ///     6      :   Errores no controlados
    /// </returns>
    public int Proc_Sw_Rec_Graba_Enc(int casilla, int sesion, int secuencia, int rut, string observacion)
    {
        int o = 0;
        Exception exe = null;

        using (Tracer tracer = new Tracer("MensajeRepository Proc_Sw_Rec_Graba_Enc"))
        {
            try
            {
                using (DbCommand commandExecSP = context.Database.Connection.CreateCommand())
                {
                    commandExecSP.CommandText = "exec proc_sw_rec_graba_enc_MS @p_casilla, @p_sesion, @p_secuencia, @p_rut_log, @p_coment";

                    DbParameter param = commandExecSP.CreateParameter();
                    param.DbType = System.Data.DbType.Int32;
                    param.ParameterName = "p_casilla";
                    param.Value = casilla;
                    commandExecSP.Parameters.Add(param);

                    param = commandExecSP.CreateParameter();
                    param.DbType = System.Data.DbType.Int32;
                    param.ParameterName = "p_sesion";
                    param.Value = sesion;
                    commandExecSP.Parameters.Add(param);

                    param = commandExecSP.CreateParameter();
                    param.DbType = System.Data.DbType.Int32;
                    param.ParameterName = "p_secuencia";
                    param.Value = secuencia;
                    commandExecSP.Parameters.Add(param);

                    param = commandExecSP.CreateParameter();
                    param.DbType = System.Data.DbType.Int32;
                    param.ParameterName = "p_rut_log";
                    param.Value = rut;
                    commandExecSP.Parameters.Add(param);

                    param = commandExecSP.CreateParameter();
                    param.DbType = System.Data.DbType.String;
                    param.ParameterName = "p_coment";
                    param.Value = observacion;
                    commandExecSP.Parameters.Add(param);

                    context.Database.Connection.Open();
                    o = Convert.ToInt32(commandExecSP.ExecuteScalar()); // get the error description
                }
            }
            catch (Exception ex)
            {
                using (var tracers = new Tracer())
                {
                    tracers.TraceException("Alerta: ", ex);
                }
                o = 6;
                exe = ex;
            }
            finally
            {
                context.Database.Connection.Close();

            }
            return o;
        }
    }

        public IList<proc_sw_rec_trae_enc_rango_MS_Result> Proc_Sw_Rec_Trae_Enc_Rango(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.proc_sw_rec_trae_enc_rango_MS(idCasilla, fechaInicio, fechaFin).ToList();
        }

        public IList<proc_sw_rec_trae_otr_rango_MS_Result> Proc_Sw_Rec_Trae_Otr_Rango(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.proc_sw_rec_trae_otr_rango_MS(idCasilla, fechaInicio, fechaFin).ToList();
        }

        public IList<proc_sw_env_trae_aut_pend_MS_Result> Proc_Sw_Env_Trae_Aut_Pend(int idCasilla, int rut)
        {
            return context.proc_sw_env_trae_aut_pend_MS(idCasilla, rut).ToList();
        }

        public IList<proc_sw_env_trae_dev_firma_MS_Result> Proc_Sw_Env_Trae_Dev_Firma(int idCasilla, int rut)
        {
            return context.proc_sw_env_trae_dev_firma_MS(idCasilla, rut).ToList();
        }

        public IList<proc_sw_env_trae_firmas_MS_Result> Proc_Sw_Env_Trae_Firmas(int idCasilla, int rut)
        {
            return context.proc_sw_env_trae_firmas_MS(idCasilla, rut).ToList();
        }

        public IList<proc_sw_env_trae_mod_rango_MS_Result> Proc_Sw_Env_Trae_Mod_Rango(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.proc_sw_env_trae_mod_rango_MS(idCasilla, fechaInicio, fechaFin).ToList();
        }

        public IList<proc_sw_env_trae_ing_rango_MS_Result> Proc_Sw_Env_Trae_Ing_Rango(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.proc_sw_env_trae_ing_rango_MS(idCasilla, fechaInicio, fechaFin).ToList();
        }

        public IList<proc_sw_env_trae_aup_rango_MS_Result> Proc_Sw_Env_Trae_Aup_Rango(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.proc_sw_env_trae_aup_rango_MS(idCasilla, fechaInicio, fechaFin).ToList();
        }

        public IList<proc_sw_env_trae_aut_rango_MS_Result> Proc_Sw_Env_Trae_Aut_Rango(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.proc_sw_env_trae_aut_rango_MS(idCasilla, fechaInicio, fechaFin).ToList();
        }

        public IList<proc_sw_env_trae_pro_rango_MS_Result> Proc_Sw_Env_Trae_Pro_Rango(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.proc_sw_env_trae_pro_rango_MS(idCasilla, fechaInicio, fechaFin).ToList();
        }

        public IList<proc_sw_env_trae_rech_rango_MS_Result> Proc_Sw_Env_Trae_Rech_Rango(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.proc_sw_env_trae_rech_rango_MS(idCasilla, fechaInicio, fechaFin).ToList();
        }

        public IList<proc_sw_env_trae_env_rango_MS_Result> Proc_Sw_Env_Trae_Env_Rango(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.proc_sw_env_trae_env_rango_MS(idCasilla, fechaInicio, fechaFin).ToList();
        }

        public IList<proc_sw_env_trae_blo_rango_MS_Result> Proc_Sw_Env_Trae_Blo_Rango(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.proc_sw_env_trae_blo_rango_MS(idCasilla, fechaInicio, fechaFin).ToList();
        }

        public IList<proc_sw_env_trae_nul_rango_MS_Result> Proc_Sw_Env_Trae_Nul_Rango(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.proc_sw_env_trae_nul_rango_MS(idCasilla, fechaInicio, fechaFin).ToList();
        }

        public int? Proc_Sw_Env_Cons_Fipe(int idMensaje, int rut, double monto)
        {
            return context.proc_sw_env_cons_fipe_MS(idMensaje, rut, monto, null).FirstOrDefault();
        }

        public IList<string> Proc_Sw_Env_Cons_Sim(int idMensaje, int rut)
        {
            return context.proc_sw_env_cons_sim_MS(idMensaje, rut).ToList();
        }

        public void Proc_Sw_Env_Graba_Apr(int idMensaje, int rut)
        {
            context.proc_sw_env_graba_apr_MS(idMensaje, rut);
        }

        public void Proc_Sw_Env_Graba_Dev(int idMensaje, int rut, int casilla, string estado)
        {
            context.proc_sw_env_graba_dev_MS(idMensaje, rut, casilla, estado);
        }

        public void Proc_Sw_Env_Graba_Rec(int idMensaje, int rut, int casilla, string comentario1, string comentario2, string estado)
        {
            context.proc_sw_env_graba_rec_MS(idMensaje, rut, casilla, comentario1, comentario2, estado);
        }

        public bool IngresaMensaje(MensajeSwiftSwi200 msg)
        {
            context.EncriptaMensajeS_MS(msg.IdMensaje, msg.Sesion, msg.Secuencia, msg.Casilla, msg.Unidad, msg.TipoMensaje, msg.Prioridad, msg.EstadoMensaje, msg.TipoIngreso, msg.RutDigita, msg.BancoRe, msg.BranchRe, msg.BancoEm, msg.BranchEm, msg.Moneda, msg.Monto, msg.Referencia, msg.Beneficiario, msg.Texto, msg.Comentario);
            return true;
        }

        public bool ModificaMensaje(MensajeSwiftSwi200 msg)
        {
            context.UpdateMensajeS_MS(msg.Unidad, msg.Prioridad, msg.EstadoMensaje, msg.BancoRe, msg.BranchRe, msg.Moneda, msg.Monto, msg.Referencia, msg.Beneficiario, msg.Comentario, msg.Texto, msg.IdMensaje);
            return true;
        }

        public sw_msgsend Get(int id_mensaje)
        {
            string query = "select * from sw_msgsend where id_mensaje=" + id_mensaje;
            sw_msgsend sw_msgsend = context.sw_msgsend.SqlQuery(query.ToString()).FirstOrDefault();
            return sw_msgsend;
        }

        public List<sw_msgsend> sw_msgsendList()
        {
            return new List<sw_msgsend>();
        }

        public override void Delete(sw_msgsend msg)
        {
            string query = "DELETE FROM sw_msgsend WHERE id_mensaje = " + msg.id_mensaje;
            ((IObjectContextAdapter)context).ObjectContext.ExecuteStoreCommand(query.ToString());
        }

        public ResultadoBusquedaSwift GetSwiftEnviado(int idMensaje)
        {
            return context.proc_sw_env_trae_por_idmensaje_MS(idMensaje).FirstOrDefault();
        }

        public ResultadoBusquedaSwift GetSwiftRecibido(int sesion, int secuencia)
        {
            try
            {
                using (DbCommand commandExecSP = context.Database.Connection.CreateCommand())
                {
                    //DbCommand commandExecSP = context.Database.Connection.CreateCommand();
                    commandExecSP.CommandText = "exec proc_sw_rec_trae_por_id_MS @sesion, @secuencia";

                    DbParameter param = commandExecSP.CreateParameter();
                    param.DbType = System.Data.DbType.Int32;
                    param.ParameterName = "sesion";
                    param.Value = sesion;
                    commandExecSP.Parameters.Add(param);

                    param = commandExecSP.CreateParameter();
                    param.DbType = System.Data.DbType.Int32;
                    param.ParameterName = "secuencia";
                    param.Value = secuencia;
                    commandExecSP.Parameters.Add(param);

                    context.Database.Connection.Open();
                    DbDataReader reader = commandExecSP.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            return GetSwiftFromDataReader(reader, Direccion.Recibido);
                        }
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

            return null;
        }

        public IList<ResultadoBusquedaSwift> BuscarSwiftsRecibidosPorCasillaYFechas(int idCasilla, DateTime? fechaDesde, DateTime? fechaHasta, int? rutUsuario, out int totalCount, int? offset, short? fetchRows, string searchText)
        {
            return BuscarSwiftsPorCasillaYFechas(Direccion.Recibido, idCasilla, fechaDesde, fechaHasta, rutUsuario, out totalCount, offset, fetchRows, searchText);
        }

        public IList<ResultadoBusquedaSwift> BuscarSwiftsEnviadosPorCasillaYFechas(int? idCasilla, DateTime? fechaDesde,
            DateTime? fechaHasta, int? rutUsuario, out int totalCount, int? offset, short? fetchRows, string searchText,
            EstadoSwiftEnviado estadoEnviados = EstadoSwiftEnviado.Enviado)
        {
            return BuscarSwiftsPorCasillaYFechas(Direccion.Enviado, idCasilla, fechaDesde, fechaHasta, rutUsuario, out totalCount, offset, fetchRows, searchText, estadoEnviados);
        }

        public List<proc_sw_trae_fmt_largo_MS_Result> LlenaMatrizMensaje(string tipoMensaje)
        {
            return context.proc_sw_trae_fmt_largo_MS(tipoMensaje).ToList();
        }

        public IList<EstadisticaCasillla> GetEstadisticaMsgPorCasilla(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, Direccion direccion)
        {
            List<EstadisticaCasillla> resultado = new List<EstadisticaCasillla>();

            try
            {
                using (DbCommand commandExecSP = context.Database.Connection.CreateCommand())
                {
                    string command = (direccion == Direccion.Enviado) ? "proc_sw_env_estadist_msg_MS" : "proc_sw_rec_estadist_msg_MS";
                    commandExecSP.CommandText = "exec " + command + " @IdCasilla, @FechaDesde, @FechaHasta";

                    //param id casilla
                    DbParameter param = commandExecSP.CreateParameter();
                    param.DbType = System.Data.DbType.Int32;
                    param.ParameterName = "IdCasilla";
                    param.Value = idCasilla;
                    commandExecSP.Parameters.Add(param);

                    //param fecha desde
                    param = commandExecSP.CreateParameter();
                    param.DbType = System.Data.DbType.DateTime;
                    param.ParameterName = "FechaDesde";
                    param.Value = fechaDesde;
                    commandExecSP.Parameters.Add(param);

                    //param fecha hasta
                    param = commandExecSP.CreateParameter();
                    param.DbType = System.Data.DbType.DateTime;
                    param.ParameterName = "FechaHasta";
                    param.Value = fechaHasta;
                    commandExecSP.Parameters.Add(param);

                    commandExecSP.Connection.Open();

                    DbDataReader reader = commandExecSP.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            EstadisticaCasillla ec = GetEstadisticaCasillaFromDataReader(reader, direccion);
                            resultado.Add(ec);
                        }
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

        public proc_sw_env_trae_datos_msg_MS_Result GetDatosMensajeEnviado(int idMensaje)
        {
            return context.proc_sw_env_trae_datos_msg_MS(idMensaje).FirstOrDefault();
        }

        public proc_sw_rec_trae_datos_msg_MS_Result GetDatosMensajeRecibido(int sesion, int secuencia)
        {
            return context.proc_sw_rec_trae_datos_msg_MS(sesion, secuencia, "R").FirstOrDefault();
        }

        public IList<proc_sw_log_trae_msg_MS_Result> GetLogDeMensajeRecibido(int sesion, int secuencia)
        {
            return context.proc_sw_log_trae_msg_MS(sesion, secuencia, "R").ToList();
        }

        public IList<proc_sw_log_trae_msg_MS_Result> GetLogDeMensajeEnviado(int idMensaje)
        {
            return context.proc_sw_log_trae_msgsend_MS(idMensaje).ToList();
        }

        private IList<ResultadoBusquedaSwift> BuscarSwiftsPorCasillaYFechas(Direccion direccion, int? idCasilla, DateTime? fechaDesde, DateTime? fechaHasta, int? rutUsuario, out int totalCount, int? offset, short? fetchRows, string searchText, EstadoSwiftEnviado estadoEnviados = EstadoSwiftEnviado.Enviado)
        {
            List<ResultadoBusquedaSwift> resultado = new List<ResultadoBusquedaSwift>();

            string nombreSP = String.Empty;
            string commandTextExcepcional = String.Empty;
            bool spSoportaPaginado = false;
            bool spRequiereFechas = true;
            if (direccion == Direccion.Recibido)
            {
                nombreSP = "proc_sw_rec_trae_iny_rango_paginado_MS";
                spSoportaPaginado = true;
            }
            else
            {
                if (estadoEnviados == EstadoSwiftEnviado.Enviado)
                {
                    nombreSP = "proc_sw_env_trae_env_rango_paginado_MS";
                    spSoportaPaginado = true;
                }
                else if (estadoEnviados == EstadoSwiftEnviado.Modificado)
                {
                    nombreSP = "proc_sw_env_trae_mod_rango_MS";
                }
                else if (estadoEnviados == EstadoSwiftEnviado.Autorizado)
                {
                    nombreSP = "proc_sw_env_trae_aut_rango_MS";
                }
                else if (estadoEnviados == EstadoSwiftEnviado.Ingresado)
                {
                    nombreSP = "proc_sw_env_trae_ing_rango_MS";
                }
                else if (estadoEnviados == EstadoSwiftEnviado.EnAprobacion)
                {
                    nombreSP = "proc_sw_env_trae_aup_rango_MS";
                }
                else if (estadoEnviados == EstadoSwiftEnviado.Procesado)
                {
                    nombreSP = "proc_sw_env_trae_pro_rango_MS";
                }
                else if (estadoEnviados == EstadoSwiftEnviado.Rechazado)
                {
                    nombreSP = "proc_sw_env_trae_rech_rango_MS";
                }
                else if (estadoEnviados == EstadoSwiftEnviado.Anulado)
                {
                    nombreSP = "proc_sw_env_trae_nul_rango_MS";
                }
                else if (estadoEnviados == EstadoSwiftEnviado.Bloqueado)
                {
                    nombreSP = "proc_sw_env_trae_blo_rango_MS";
                }
                else if (estadoEnviados == EstadoSwiftEnviado.Devuelto)
                {
                    commandTextExcepcional = "exec proc_sw_env_trae_dev_firma_MS @IdCasilla, @Rut";
                    spRequiereFechas = false;
                }
                else if (estadoEnviados == EstadoSwiftEnviado.SinSolicitudFirmas)
                {
                    commandTextExcepcional = "exec proc_sw_env_test_firma_MS @Rut, @FechaDesde";
                    spRequiereFechas = true;
                    fechaHasta = null;

                }
                else throw new ArgumentException("El tipo de estado por el que se desea buscar no es válido");
            }

            totalCount = 0;

            try
            {
                using (DbCommand commandExecSP = context.Database.Connection.CreateCommand())
                {
                    commandExecSP.CommandText = (String.IsNullOrEmpty(commandTextExcepcional) ? "exec " + nombreSP + " @IdCasilla, @FechaDesde, @FechaHasta" : commandTextExcepcional);

                    if (spSoportaPaginado)
                    {
                        commandExecSP.CommandText += ", @Offset, @FetchRows";

                        //si lleva valor searchText o tiene datos el rut, debe enviar el parametro, ya que si no se incluye 
                        //y el rut tiene datos, los procedures toman el rut como searchText 
                        if (!String.IsNullOrEmpty(searchText) || rutUsuario.HasValue)
                            commandExecSP.CommandText += ", @searchText";
                        
                        if (rutUsuario.HasValue)
                            commandExecSP.CommandText += ", @Rut";
                    }

                    AgregarParametrosParaBusquedaSwiftsPorRango(direccion, commandExecSP, idCasilla, fechaDesde, fechaHasta, rutUsuario, spRequiereFechas, spSoportaPaginado, offset, fetchRows, searchText);

                    context.Database.Connection.Open();
                    DbDataReader reader = commandExecSP.ExecuteReader();

                    bool esPrimero = true;

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ResultadoBusquedaSwift rbs = GetSwiftFromDataReader(reader, direccion, estadoEnviados);
                            resultado.Add(rbs);
                            if (esPrimero && spSoportaPaginado)
                            {
                                esPrimero = false;
                                totalCount = reader.GetInt32(reader.FieldCount - 1); //la ultima columna es el count total
                            }
                        }

                        if (!spSoportaPaginado)
                            totalCount = resultado.Count;
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

        private void AgregarParametrosParaBusquedaSwiftsPorRango(Direccion direccion, DbCommand commandExecSP, int? idCasilla, DateTime? fechaDesde, DateTime? fechaHasta, int? rutUsuario, bool spRequiereFechas, bool spSoportaPaginado, int? offset, short? fetchRows, string searchText)
        {
            //param id casilla
            DbParameter param = null;
            if (idCasilla.HasValue)
            {
                param = commandExecSP.CreateParameter();
                commandExecSP.CreateParameter();
                param.DbType = System.Data.DbType.Int32;
                param.ParameterName = "IdCasilla";
                param.Value = idCasilla.Value;
                commandExecSP.Parameters.Add(param);
            }

            if (spRequiereFechas && fechaDesde.HasValue)
            {
                //param fecha desde
                param = commandExecSP.CreateParameter();
                param.DbType = System.Data.DbType.DateTime;
                param.ParameterName = "FechaDesde";
                param.Value = fechaDesde.Value;
                commandExecSP.Parameters.Add(param);
            }

            if (spRequiereFechas && fechaHasta.HasValue)
            {
                //param fecha hasta
                param = commandExecSP.CreateParameter();
                param.DbType = System.Data.DbType.DateTime;
                param.ParameterName = "FechaHasta";
                param.Value = fechaHasta.Value;
                commandExecSP.Parameters.Add(param);
            }

            if (spSoportaPaginado && offset.HasValue)
            {
                //param offset, cuantos registros me salteo
                param = commandExecSP.CreateParameter();
                param.DbType = System.Data.DbType.Int32;
                param.ParameterName = "Offset";
                param.Value = offset;
                commandExecSP.Parameters.Add(param);
            }

            if (spSoportaPaginado && fetchRows.HasValue)
            {
                //param fetchRows, cuantas rows traigo
                param = commandExecSP.CreateParameter();
                param.DbType = System.Data.DbType.Int16;
                param.ParameterName = "FetchRows";
                param.Value = fetchRows;
                commandExecSP.Parameters.Add(param);
            }

            //si lleva valor searchText o tiene datos el rut, debe enviar el parametro, ya que si no se incluye 
            //y el rut tiene datos, los procedures toman el rut como searchText
            if (spSoportaPaginado && ( !String.IsNullOrEmpty(searchText) || rutUsuario.HasValue))
            {
                param = commandExecSP.CreateParameter();
                param.DbType = System.Data.DbType.AnsiString;
                param.ParameterName = "searchText";
                param.Value = searchText ?? string.Empty;
                commandExecSP.Parameters.Add(param);
            }

            if (rutUsuario.HasValue)
            {
                //param fecha hasta
                param = commandExecSP.CreateParameter();
                param.DbType = System.Data.DbType.Int32;
                param.ParameterName = "Rut";
                param.Value = rutUsuario.Value;
                commandExecSP.Parameters.Add(param);
            }
        }

        private ResultadoBusquedaSwift GetSwiftFromDataReader(DbDataReader reader, Direccion direccion, EstadoSwiftEnviado estadoEnviados = EstadoSwiftEnviado.Enviado)
        {
            int col = 0;
            ResultadoBusquedaSwift rbs = null;
            if (direccion == Direccion.Recibido)
            {
                rbs = new ResultadoBusquedaSwift()
                {
                    sesion = reader.GetInt32(col++),
                    secuencia = reader.GetInt32(col++),
                    casilla = reader.GetInt32(col++),
                    nombre_casilla = Utils.GetStringFromDataReader(reader, col++),
                    tipo_msg = Utils.GetStringFromDataReader(reader, col++),
                    nombre_tipo = Utils.GetStringFromDataReader(reader, col++),
                    prioridad = Utils.GetStringFromDataReader(reader, col++),
                    estado_msg = Utils.GetStringFromDataReader(reader, col++),
                    fecha_rec = Utils.GetStringFromDataReader(reader, col++),
                    hora_rec = Utils.GetStringFromDataReader(reader, col++),
                    cod_banco_rec = Utils.GetStringFromDataReader(reader, col++),
                    branch_rec = Utils.GetStringFromDataReader(reader, col++),
                    cod_banco_em = Utils.GetStringFromDataReader(reader, col++),
                    branch_em = Utils.GetStringFromDataReader(reader, col++),
                    nombre_banco = Utils.GetStringFromDataReader(reader, col++),
                    ciudad_banco = Utils.GetStringFromDataReader(reader, col++),
                    pais_banco = Utils.GetStringFromDataReader(reader, col++),
                    oficina_banco = Utils.GetStringFromDataReader(reader, col++),
                    cod_moneda = Utils.GetStringFromDataReader(reader, col++),
                    monto = Utils.GetDoubleFromDataReader(reader, col++),
                    referencia = Utils.GetStringFromDataReader(reader, col++),
                    beneficiario = Utils.GetStringFromDataReader(reader, col++),
                    total_imp = Utils.GetIntFromDataReader(reader, col++),
                    comentario = Utils.GetStringFromDataReader(reader, col++)
                };
            }
            else
            {
                rbs = new ResultadoBusquedaSwift()
                {
                    id_mensaje = reader.GetInt32(col++),
                    sesion = reader.GetInt32(col++),
                    secuencia = reader.GetInt32(col++),
                    casilla = reader.GetInt32(col++),
                    nombre_casilla = Utils.GetStringFromDataReader(reader, col++),
                    tipo_msg = Utils.GetStringFromDataReader(reader, col++),
                    nombre_tipo = Utils.GetStringFromDataReader(reader, col++),
                    prioridad = Utils.GetStringFromDataReader(reader, col++),
                    estado_msg = Utils.GetStringFromDataReader(reader, col++),
                    fecha_ingr = Utils.GetStringFromDataReader(reader, col++),
                    hora_ingr = Utils.GetStringFromDataReader(reader, col++)
                };

                if (estadoEnviados != EstadoSwiftEnviado.Ingresado && estadoEnviados != EstadoSwiftEnviado.Devuelto && estadoEnviados != EstadoSwiftEnviado.EnAprobacion && estadoEnviados != EstadoSwiftEnviado.SinSolicitudFirmas)
                {
                    rbs.fecha_env = Utils.GetStringFromDataReader(reader, col++);
                    rbs.hora_env = Utils.GetStringFromDataReader(reader, col++);
                }

                rbs.cod_banco_em = Utils.GetStringFromDataReader(reader, col++);
                rbs.branch_em = Utils.GetStringFromDataReader(reader, col++);
                rbs.cod_banco_rec = Utils.GetStringFromDataReader(reader, col++);
                rbs.branch_rec = Utils.GetStringFromDataReader(reader, col++);
                rbs.nombre_banco = Utils.GetStringFromDataReader(reader, col++);
                rbs.ciudad_banco = Utils.GetStringFromDataReader(reader, col++);
                rbs.pais_banco = Utils.GetStringFromDataReader(reader, col++);
                rbs.oficina_banco = Utils.GetStringFromDataReader(reader, col++);
                rbs.cod_moneda = Utils.GetStringFromDataReader(reader, col++);
                rbs.nombre_moneda = Utils.GetStringFromDataReader(reader, col++);
                rbs.cod_moneda_banco = Utils.GetStringFromDataReader(reader, col++);
                rbs.monto = Utils.GetDoubleFromDataReader(reader, col++);
                rbs.referencia = Utils.GetStringFromDataReader(reader, col++);
                rbs.beneficiario = Utils.GetStringFromDataReader(reader, col++);
                rbs.rut_ingreso = reader.GetInt32(col++);
            }

            return rbs;
        }

        private EstadisticaCasillla GetEstadisticaCasillaFromDataReader(DbDataReader reader, Direccion direccion)
        {
            int col = 0;
            EstadisticaCasillla ec = new EstadisticaCasillla()
            {
                IdCasilla = reader.GetInt32(col++),
                NombreCasilla = Utils.GetStringFromDataReader(reader, col++),
                TipoMsg = Utils.GetStringFromDataReader(reader, col++),
                NombreTipo = Utils.GetStringFromDataReader(reader, col++)
            };

            //avanzo el indice ya que hay algunas columnas de informacion de banco que no me interesan
            col = (direccion == Direccion.Recibido) ? 6 : 7;

            ec.Cantidad = Utils.GetIntFromDataReader(reader, col++).Value;
            return ec;
        }

        public string Proc_Sw_Rec_Anula_Enc(int idCasilla, int Sesion, int Secuencia, int Rut)
        {
            try
            {
                context.proc_sw_rec_anula_enc_MS(idCasilla, Sesion, Secuencia, Rut);
                return "OK";
            }
            catch
            {
                throw;
            }
        }

        public void Proc_Sw_Rec_Graba_Conf(int casilla, int sesion, int secuencia, int rutLog, string comentario)
        {
            context.proc_sw_rec_graba_conf_MS(casilla, sesion, secuencia, rutLog, comentario);
        }

        public void Proc_Sw_Rec_Graba_Rech(int casilla, int sesion, int secuencia, int rutLog, string estado, string texto)
        {
            context.proc_sw_rec_graba_rech_MS(casilla, sesion, secuencia, rutLog, estado, texto);
        }

        public IList<proc_sw_env_test_firma_MS_Result> proc_sw_env_test_firma(int rut, DateTime fecha)
        {
            return context.proc_sw_env_test_firma_MS(rut, fecha).ToList();
        }

        public int proc_sw_env_test_firma_count(int rut, DateTime fecha)
        {
            int? result = context.proc_sw_env_test_firma_count_MS(rut, fecha).First();
            
            if (result != null) return result.Value;
            else return 0;
        }

        public bool proc_sw_env_graba_mod(int idMensaje, int casilla, int rut)
        {
            int? res = context.proc_sw_env_graba_mod_MS(idMensaje, casilla, rut);
            return true;
        }

        public bool proc_sw_msgsend_log_i01(int idMensaje, DateTime fechaLog, int rut, string idProg, string opcion, int casillaDestino, string estadoDestino, int unidad, string resultado, string comentario ) 
        {
            context.proc_sw_msgsend_log_i01_MS(idMensaje, fechaLog, rut, idProg, opcion, casillaDestino, estadoDestino, unidad, resultado, comentario);
            return true;
        }

        public int Proc_Sw_Rec_Borra_Casilla(int idCasilla)
        {
            ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 1200;
            return context.proc_sw_rec_borra_casilla_MS(idCasilla).FirstOrDefault().GetValueOrDefault();
        }

        public proc_sw_busca_eliminar_MS_Result Proc_Sw_Busca_Eliminar(int sesion, int secuencia)
        {
            return context.proc_sw_busca_eliminar_MS(sesion, secuencia).FirstOrDefault<proc_sw_busca_eliminar_MS_Result>();
        }

        public void Proc_Sw_Elimina_Mensaje(int sesion, int secuencia)
        {
            context.proc_sw_elimina_mensaje_MS(sesion, secuencia);
        }

        public int proc_sw_env_test_apr_MS_count(int rut)
        {
            int? result = context.proc_sw_env_test_apr_MS(0,rut).First();

            if (result != null) return result.Value;
            else return 0;
        }

        public int proc_sw_env_test_file_MS()
        {
            int? result = context.proc_sw_env_test_file_MS().FirstOrDefault();
            if (result != null) return result.Value;
            else return 0;
        }

        public int Proc_sw_rec_trae_nro_imp(int sesion, int secuencia) 
        {
            var result = context.proc_sw_rec_trae_nro_imp_MS(sesion, secuencia).FirstOrDefault();
            return result.Value;
        }

        public void Proc_sw_rec_graba_imp(int casilla, int sesion, int secuencia, int rutLog, string estado, string comentario) 
        {
            context.proc_sw_rec_graba_imp_MS(casilla, sesion, secuencia, rutLog, estado, comentario);
        }
    }
}