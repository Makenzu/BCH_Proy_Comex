using BCH.Comex.Core.Entities.Swift;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace BCH.Comex.Data.DAL.Swift
{
    public class GestionControlRepository : GenericRepository<proc_sw_rec_trae_iny_rangoDTO, swiftEntities>
    {
        SqlDataAdapter _sqlAdapter = new SqlDataAdapter();
        string query = "";
        string _conexion = @"Data Source=ComexSqldesa.cloudapp.net;Persist Security Info=True;Password=Banco.Des@;User ID=comex;Initial Catalog=swift";
        public GestionControlRepository(swiftEntities context) : base(context)
        {

        }
        //<add name="swiftEntities" connectionString="metadata=res://*/SwiftModel.csdl|res://*/SwiftModel.ssdl|
        //res://*/SwiftModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=ComexSqldesa.cloudapp.net;initial catalog=swift;user id=comex;password=Banco.Des@;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
        public string GetConnectionString()
        {
            return @"Data Source=ComexSqldesa.cloudapp.net;Persist Security Info=True;Password=Banco.Des@;User ID=comex;Initial Catalog=swift";
        }

        public IList<proc_sw_rec_trae_iny_rangoDTO> proc_sw_rec_trae_iny_rango(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.Database.SqlQuery<proc_sw_rec_trae_iny_rangoDTO>("exec proc_sw_rec_trae_iny_rango_MS  @p_casilla,@p_fecha1,@p_fecha2",
            new SqlParameter("p_casilla", idCasilla), new SqlParameter("p_fecha1", fechaInicio), new SqlParameter("p_fecha2", fechaFin)).ToList<proc_sw_rec_trae_iny_rangoDTO>();
        }

        public IList<proc_sw_rec_trae_iny_rangoDTO> proc_sw_rec_trae_iny_rango_por_estado_impresos(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.Database.SqlQuery<proc_sw_rec_trae_iny_rangoDTO>("exec proc_sw_rec_trae_iny_rango_por_estado_impresos  @p_casilla,@p_fecha1,@p_fecha2",
            new SqlParameter("p_casilla", idCasilla), new SqlParameter("p_fecha1", fechaInicio), new SqlParameter("p_fecha2", fechaFin)).ToList<proc_sw_rec_trae_iny_rangoDTO>();
        }

        public IList<proc_sw_rec_trae_iny_rangoDTO> proc_sw_rec_trae_iny_rango_por_estado_rechazados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.Database.SqlQuery<proc_sw_rec_trae_iny_rangoDTO>("exec proc_sw_rec_trae_iny_rango_por_estado_rechazados  @p_casilla,@p_fecha1,@p_fecha2",
            new SqlParameter("p_casilla", idCasilla), new SqlParameter("p_fecha1", fechaInicio), new SqlParameter("p_fecha2", fechaFin)).ToList<proc_sw_rec_trae_iny_rangoDTO>();
        }
        public IList<proc_sw_rec_trae_iny_rangoDTO> proc_sw_rec_trae_iny_rango_por_estado_encasillados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.Database.SqlQuery<proc_sw_rec_trae_iny_rangoDTO>("exec proc_sw_rec_trae_iny_rango_por_estado_encasillados  @p_casilla,@p_fecha1,@p_fecha2",
            new SqlParameter("p_casilla", idCasilla), new SqlParameter("p_fecha1", fechaInicio), new SqlParameter("p_fecha2", fechaFin)).ToList<proc_sw_rec_trae_iny_rangoDTO>();
        }

        public IList<proc_sw_rec_trae_iny_rangoDTO> proc_sw_rec_trae_iny_rango_por_estado_posibles_duplicados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.Database.SqlQuery<proc_sw_rec_trae_iny_rangoDTO>("exec proc_sw_rec_trae_iny_rango_por_estado_posibles_duplicados  @p_casilla,@p_fecha1,@p_fecha2",
            new SqlParameter("p_casilla", idCasilla), new SqlParameter("p_fecha1", fechaInicio), new SqlParameter("p_fecha2", fechaFin)).ToList<proc_sw_rec_trae_iny_rangoDTO>();
        }

        public IList<proc_sw_rec_trae_iny_rangoDTO> proc_sw_rec_trae_iny_rango_por_estado_confirmados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.Database.SqlQuery<proc_sw_rec_trae_iny_rangoDTO>("exec proc_sw_rec_trae_iny_rango_por_estado_confirmados  @p_casilla,@p_fecha1,@p_fecha2",
            new SqlParameter("p_casilla", idCasilla), new SqlParameter("p_fecha1", fechaInicio), new SqlParameter("p_fecha2", fechaFin)).ToList<proc_sw_rec_trae_iny_rangoDTO>();
        }

        public IList<proc_sw_rec_trae_iny_rangoDTO> proc_sw_rec_trae_iny_rango_por_estado_reenviados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.Database.SqlQuery<proc_sw_rec_trae_iny_rangoDTO>("exec proc_sw_rec_trae_iny_rango_por_estado_reenviados  @p_casilla,@p_fecha1,@p_fecha2",
            new SqlParameter("p_casilla", idCasilla), new SqlParameter("p_fecha1", fechaInicio), new SqlParameter("p_fecha2", fechaFin)).ToList<proc_sw_rec_trae_iny_rangoDTO>();
        }

        public IList<proc_sw_rec_controlDTO> proc_sw_rec_control(int idCasilla, DateTime fechaDesde)
        {
            return context.Database.SqlQuery<proc_sw_rec_controlDTO>("exec proc_sw_rec_control_MS  @p_casilla,@p_fecha1",
            new SqlParameter("p_casilla", idCasilla), new SqlParameter("p_fecha1", fechaDesde)).ToList<proc_sw_rec_controlDTO>();
        }

        public IList<proc_sw_log_trae_enc_rangoDTO> proc_sw_log_trae_enc_rango(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.Database.SqlQuery<proc_sw_log_trae_enc_rangoDTO>("exec proc_sw_log_trae_enc_rango_MS  @p_casilla,@p_fecha1,@p_fecha2",
            new SqlParameter("p_casilla", idCasilla), new SqlParameter("p_fecha1", fechaInicio), new SqlParameter("p_fecha2", fechaFin)).ToList<proc_sw_log_trae_enc_rangoDTO>();
        }


        public IList<proc_sw_env_estadist_msgDTO> proc_sw_env_estadist_msg(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.Database.SqlQuery<proc_sw_env_estadist_msgDTO>("exec proc_sw_env_estadist_msg_MS  @p_casilla,@p_fecha1,@p_fecha2",
            new SqlParameter("p_casilla", idCasilla), new SqlParameter("p_fecha1", fechaInicio), new SqlParameter("p_fecha2", fechaFin)).ToList<proc_sw_env_estadist_msgDTO>();
        }

        public IList<proc_sw_rec_estadist_msgDTO> proc_sw_rec_estadist_msg(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.Database.SqlQuery<proc_sw_rec_estadist_msgDTO>("exec proc_sw_rec_estadist_msg_MS  @p_casilla,@p_fecha1,@p_fecha2",
            new SqlParameter("p_casilla", idCasilla), new SqlParameter("p_fecha1", fechaInicio), new SqlParameter("p_fecha2", fechaFin)).ToList<proc_sw_rec_estadist_msgDTO>();
        }

        public IList<proc_sw_log_trae_aut_rangoDTO> proc_sw_log_trae_aut_rango_MS(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.Database.SqlQuery<proc_sw_log_trae_aut_rangoDTO>("exec proc_sw_log_trae_aut_rango_MS  @p_casilla,@p_fecha1,@p_fecha2",
            new SqlParameter("p_casilla", idCasilla), new SqlParameter("p_fecha1", fechaInicio), new SqlParameter("p_fecha2", fechaFin)).ToList<proc_sw_log_trae_aut_rangoDTO>();
        }

        public IList<proc_sw_env_trae_env_rangoDTO> proc_sw_env_trae_env_rango_MS(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.Database.SqlQuery<proc_sw_env_trae_env_rangoDTO>("exec proc_sw_env_trae_env_rango_MS  @p_casilla,@p_fecha1,@p_fecha2",
            new SqlParameter("p_casilla", idCasilla), new SqlParameter("p_fecha1", fechaInicio), new SqlParameter("p_fecha2", fechaFin)).ToList<proc_sw_env_trae_env_rangoDTO>();
        }

        public IList<proc_sw_env_trae_env_rangoDTO> proc_sw_env_trae_env_rango_paginado_MS(int idCasilla, DateTime fechaInicio, DateTime fechaFin, int offset, int fetchrows)
        {
            return context.Database.SqlQuery<proc_sw_env_trae_env_rangoDTO>("exec proc_sw_env_trae_env_rango_paginado_MS  @p_casilla,@p_fecha1,@p_fecha2,@offset,@fetchRows",
            new SqlParameter("p_casilla", idCasilla), new SqlParameter("p_fecha1", fechaInicio), new SqlParameter("p_fecha2", fechaFin), new SqlParameter("offset", offset), new SqlParameter("fetchRows", fetchrows)).ToList<proc_sw_env_trae_env_rangoDTO>();
        }

        public IList<proc_sw_env_trae_filesDTO> proc_sw_env_trae_files_MS(DateTime fechaInicio, DateTime fechaFin)
        {
            string estado = "R";
            return context.Database.SqlQuery<proc_sw_env_trae_filesDTO>("exec proc_sw_env_trae_files_MS @p_estado,@p_fecha1,@p_fecha2",
            new SqlParameter("p_estado", estado), new SqlParameter("p_fecha1", fechaInicio), new SqlParameter("p_fecha2", fechaFin)).ToList<proc_sw_env_trae_filesDTO>();
        }

        public IList<proc_sw_env_trae_filesDTO> proc_sw_env_trae_files02(DateTime fechaInicio, DateTime fechaFin)
        {
            string estado = "R";
            return context.Database.SqlQuery<proc_sw_env_trae_filesDTO>("exec proc_sw_env_trae_files_MS @p_estado,@p_fecha1,@p_fecha2",
            new SqlParameter("p_estado", estado), new SqlParameter("p_fecha1", fechaInicio), new SqlParameter("p_fecha2", fechaFin)).ToList<proc_sw_env_trae_filesDTO>();
        }

        public IList<proc_sw_rec_trae_resumen_msgDTO> proc_sw_rec_trae_resumen_msg(DateTime fechaInicio, DateTime fechaFin)
        {
            return context.Database.SqlQuery<proc_sw_rec_trae_resumen_msgDTO>("exec proc_sw_rec_trae_resumen_msg_MS @p_fecha1,@p_fecha2",
            new SqlParameter("p_fecha1", fechaInicio), new SqlParameter("p_fecha2", fechaFin)).ToList<proc_sw_rec_trae_resumen_msgDTO>();
        }

        //secuencia
        public string ObtSesPorSec(int sec, DateTime fecha)
        {
            string aux = "";
            //query = "select sesion from sw_mensajes where fecha_send >= dateadd(dd,0,fecha) and fecha_send < dateadd(dd,+1,fecha) and send_recv = 'R' and secuencia = " + sec + "";
            query = "select sesion from sw_mensajes where fecha_send >= dateadd(dd,0,fecha) and fecha_send < dateadd(dd,+1,fecha) and send_recv = 'R' and secuencia = @sec";
            using (SqlConnection _conexion = new SqlConnection(GetConnectionString()))
            {
                _conexion.Open();
                _sqlAdapter.SelectCommand = new SqlCommand(query, _conexion);
                _sqlAdapter.SelectCommand.Parameters.AddWithValue("@sec", sec);
                DataSet DS1 = new DataSet();
                _sqlAdapter.Fill(DS1, query);
                foreach (DataRow renglon in DS1.Tables[query].Rows)
                {
                    aux = renglon["sesion"].ToString();
                }
                _conexion.Close();
            }
            return aux;
        }

        public string ObtSecPorSec(int sec, DateTime fecha)
        {
            string aux = "";
            //query = "select secuencia from sw_mensajes where fecha_send >= dateadd(dd,0,fecha) and fecha_send < dateadd(dd,+1,fecha) and send_recv = 'R' and secuencia = " + sec + "";
            query = "select secuencia from sw_mensajes where fecha_send >= dateadd(dd,0,fecha) and fecha_send < dateadd(dd,+1,fecha) and send_recv = 'R' and secuencia = @sec";
            using (SqlConnection _conexion = new SqlConnection(GetConnectionString()))
            {
                _conexion.Open();
                _sqlAdapter.SelectCommand = new SqlCommand(query, _conexion);
                _sqlAdapter.SelectCommand.Parameters.AddWithValue("@sec", sec);
                DataSet DS1 = new DataSet();
                _sqlAdapter.Fill(DS1, query);
                foreach (DataRow renglon in DS1.Tables[query].Rows)
                {
                    aux = renglon["secuencia"].ToString();
                }
                _conexion.Close();
            }
            return aux;
        }

    }
}
