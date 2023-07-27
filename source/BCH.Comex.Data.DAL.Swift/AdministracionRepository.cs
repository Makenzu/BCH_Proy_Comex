using BCH.Comex.Core.Entities.Swift;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BCH.Comex.Data.DAL.Swift
{
    public class AdministracionRepository : GenericRepository<proc_sw_env_trae_noaut_rangoDTO, swiftEntities>
    {
        public AdministracionRepository(swiftEntities context)
            : base(context)
        {

        }

        public IList<proc_sw_env_trae_noaut_rangoDTO> proc_sw_env_trae_noaut_rango_MS(int casilla, DateTime fecha1, DateTime fecha2)
        {         
            return context.Database.SqlQuery<proc_sw_env_trae_noaut_rangoDTO>("exec proc_sw_env_trae_noaut_rango_MS  @p_casilla,@p_fecha1,@p_fecha2",
                new SqlParameter("p_casilla", casilla), new SqlParameter("p_fecha1", fecha1), new SqlParameter("p_fecha2", fecha2)).ToList<proc_sw_env_trae_noaut_rangoDTO>();
        }

        public IList<proc_sw_env_trae_pro_rango_MS_Result> proc_sw_env_trae_pro_rango(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {          
           return context.proc_sw_env_trae_pro_rango_MS(idCasilla, fechaInicio, fechaFin).ToList();
        }

        public IList<proc_sw_env_trae_aut_rango_MS_Result> proc_sw_env_trae_aut_rango(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.proc_sw_env_trae_aut_rango_MS(idCasilla, fechaInicio, fechaFin).ToList();
        }

        public IList<proc_sw_env_trae_rech_swiDTO> proc_sw_env_trae_rech_swi(int casilla, DateTime fecha1, DateTime fecha2)
        {

            return context.Database.SqlQuery<proc_sw_env_trae_rech_swiDTO>("exec proc_sw_env_trae_rech_swi_MS  @p_casilla,@p_fecha1,@p_fecha2",
                new SqlParameter("p_casilla", casilla), new SqlParameter("p_fecha1", fecha1), new SqlParameter("p_fecha2", fecha2)).ToList<proc_sw_env_trae_rech_swiDTO>();

        }

        public IList<proc_sw_env_trae_blo_rango_MS_Result> proc_sw_env_trae_blo_rango(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.proc_sw_env_trae_blo_rango_MS(idCasilla, fechaInicio, fechaFin).ToList();
        }

        public IList<proc_sw_env_graba_bloDTO> proc_sw_env_graba_blo_MS(int Idmensaje, int casilla, int rut,string comentario)
        {

            return context.Database.SqlQuery<proc_sw_env_graba_bloDTO>("exec proc_sw_env_graba_blo_MS  @p_id_mensaje,@p_casilla,@p_rut_log,@p_comentario",
                new SqlParameter("p_id_mensaje", Idmensaje), new SqlParameter("p_casilla", casilla), new SqlParameter("p_rut_log", rut), new SqlParameter("p_comentario", comentario)).ToList<proc_sw_env_graba_bloDTO>();

        }

        public IList<proc_sw_env_graba_desbloDTO> proc_sw_env_graba_desblo_MS(int Idmensaje, int casilla, int rut)
        {

            return context.Database.SqlQuery<proc_sw_env_graba_desbloDTO>("exec proc_sw_env_graba_desblo_MS  @p_id_mensaje,@p_casilla,@p_rut_log",
                new SqlParameter("p_id_mensaje", Idmensaje), new SqlParameter("p_casilla", casilla), new SqlParameter("p_rut_log", rut)).ToList<proc_sw_env_graba_desbloDTO>();

        }

        public IList<proc_sw_env_trae_nul_rango_MS_Result> proc_sw_env_trae_nul_rango(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.proc_sw_env_trae_nul_rango_MS(idCasilla, fechaInicio, fechaFin).ToList();
        }

        public int Proc_Sw_Env_Graba_Rec(int idMensaje, int rut, int casilla, string comentario1, string comentario2, string estado)
        {
            int result = EjecutarSPConRetorno("proc_sw_env_graba_rec_MS", String.Empty, idMensaje.ToString(), rut.ToString(), casilla.ToString(), comentario1.ToString(), comentario2.ToString(), estado.ToString());
            return result;

            //return context.Database.SqlQuery<proc_sw_env_graba_recDTO>("exec proc_sw_env_graba_rec_MS  @id_mensaje,@p_rut_log,@p_casilla,@p_comentario1,@p_comentario2,@p_estado",
            //     new SqlParameter("id_mensaje", idMensaje), new SqlParameter("p_rut_log", rut), new SqlParameter("p_casilla", casilla), new SqlParameter("p_comentario1", comentario1), new SqlParameter("p_comentario2", comentario2), new SqlParameter("p_estado", estado)).ToList<proc_sw_env_graba_recDTO>();
        }

        /// <summary>
        /// Anular mensaje Swift
        /// </summary>
        /// <param name="idMensaje"></param>
        /// <param name="rut"></param>
        /// <param name="casilla"></param>
        /// <param name="comentario"></param>
        /// <returns></returns>
        public string proc_sw_env_graba_nul_MS(int idMensaje, int rut, int casilla, string comentario)
        {
            return context.proc_sw_env_graba_nul_MS(casilla, idMensaje, rut, comentario).FirstOrDefault();
        }

        public int proc_sw_env_graba_nul(int idMensaje, int rut, int casilla, string comentario)
        {
            return EjecutarSPConRetorno(
                "proc_sw_env_graba_nul_MS", 
                string.Empty,
                casilla.ToString(),
                idMensaje.ToString(),
                rut.ToString(),
                comentario
                );

            //return context.Database.SqlQuery<proc_sw_env_graba_nulDTO>("exec proc_sw_env_graba_nul  @p_casilla,@p_id_mensaje,@p_rut_log,@p_comentario",
            //    new SqlParameter("p_casilla", casilla), new SqlParameter("p_id_mensaje", idMensaje), new SqlParameter("p_rut_log", rut), new SqlParameter("p_comentario", comentario)).ToList<proc_sw_env_graba_nulDTO>();

        }

        public IList<proc_sw_env_trae_files_MS_Result> proc_sw_env_trae_files_MS(string estado, DateTime fechaInicio, DateTime fechaFin)
        {
            return context.proc_sw_env_trae_files_MS(estado, fechaInicio, fechaFin).ToList();
        }

        public IList<proc_sw_env_del_firnulDTO> proc_sw_env_del_firnul_MS(int id_mensaje, int rut, DateTime fecha)
        {
            string date = fecha.ToString("dd-MM-yyyy");
            return context.Database.SqlQuery<proc_sw_env_del_firnulDTO>("exec proc_sw_env_del_firnul_MS  @p_id_mensaje,@p_rut_solic,@p_fecha_solic",
                new SqlParameter("p_id_mensaje", id_mensaje), new SqlParameter("p_rut_solic", rut), new SqlParameter("p_fecha_solic", date)).ToList<proc_sw_env_del_firnulDTO>();

        }

        public bool proc_sw_env_ing_firma(int idmensaje, int rut, string p_tipo_firma, string p_estado, string p_revfir, int p_rut_solic, string p_fecha_solic, string p_avisado)
        {
            int result = EjecutarSPConRetorno("proc_sw_env_ing_firma_MS", String.Empty, idmensaje.ToString(), rut.ToString(), p_tipo_firma, p_estado, p_revfir, p_rut_solic.ToString(), p_fecha_solic, p_avisado);
            return result == 0;
        }
       
        public IList<proc_sw_trae_firma_MS_Result> proc_sw_trae_firma_MS(int idCasilla)
        {
            return context.proc_sw_trae_firma_MS(idCasilla).ToList();
        }

        public IList<proc_sw_env_trae_ftp_err_MS_Result> proc_sw_env_trae_ftp_err_MS(DateTime fechaInicio, DateTime fechaFin)
        {
            return context.proc_sw_env_trae_ftp_err_MS(fechaInicio, fechaFin).ToList();
        }
        public IList<proc_sw_env_trae_detfile_MS_Result> proc_sw_env_trae_detfile_MS(int idArchivo)
        {
            return context.proc_sw_env_trae_detfile_MS(idArchivo).ToList();
        }

        public IList<proc_sw_env_graba_fileDTO> proc_sw_env_graba_file_MS(int fd_archivo)
        {
          
            return context.Database.SqlQuery<proc_sw_env_graba_fileDTO>("exec proc_sw_env_graba_file_MS  @fd_archivo,@p_estado",
                new SqlParameter("fd_archivo", fd_archivo), new SqlParameter("p_estado", "R")).ToList<proc_sw_env_graba_fileDTO>();

        }

        public IList<proc_sw_env_graba_ftp_reeDTO> proc_sw_env_graba_ftp_ree_MS(int fd_archivo)
        {

            return context.Database.SqlQuery<proc_sw_env_graba_ftp_reeDTO>("exec proc_sw_env_graba_ftp_ree_MS  @fd_archivo",
                new SqlParameter("fd_archivo", fd_archivo)).ToList<proc_sw_env_graba_ftp_reeDTO>();

        }

        public IList<proc_sw_env_trae_nop_MS_Result> proc_sw_env_trae_nop_MS(DateTime fechaInicio,DateTime fechaFin)
        {
            return context.proc_sw_env_trae_nop_MS(fechaInicio, fechaFin).ToList();
        }

        public IList<proc_sw_env_graba_envDTO> proc_sw_env_graba_env_MS(int p_id_mensaje, int p_sesion, int p_secuencia, DateTime p_fecha_env, int p_rut_log, int p_unidad)
        {
            string fecha = p_fecha_env.ToString("yyyy-MM-dd HH:mm:ss");
            return context.Database.SqlQuery<proc_sw_env_graba_envDTO>("exec proc_sw_env_graba_env_MS  @p_id_mensaje,@p_sesion,@p_secuencia,@p_fecha_env,@p_rut_log,@p_unidad",
                new SqlParameter("p_id_mensaje", p_id_mensaje), new SqlParameter("p_sesion", p_sesion), new SqlParameter("p_secuencia", p_secuencia), new SqlParameter("p_fecha_env", fecha), new SqlParameter("p_rut_log", p_rut_log), new SqlParameter("p_unidad", p_unidad)).ToList<proc_sw_env_graba_envDTO>();

        }

        public IList<proc_sw_env_graba_resDTO> proc_sw_env_graba_res_MS(int p_id_mensaje, DateTime p_fecha_res, int p_rut_log, int p_unidad, string p_texto)
        {
            string fecha = p_fecha_res.ToString("yyyy-MM-dd HH:mm:ss");
            return context.Database.SqlQuery<proc_sw_env_graba_resDTO>("exec proc_sw_env_graba_res_MS  @p_id_mensaje,@p_fecha_res,@p_rut_log,@p_unidad,@p_texto",
                new SqlParameter("p_id_mensaje", p_id_mensaje), new SqlParameter("p_fecha_res", p_fecha_res), new SqlParameter("p_rut_log", p_rut_log), new SqlParameter("p_unidad", p_unidad), new SqlParameter("p_texto", p_texto)).ToList<proc_sw_env_graba_resDTO>();

        }

        public IList<proc_sw_env_del_nopDTO> proc_sw_env_del_nop_MS(int p_sesion, int p_secuencia)
        {

            return context.Database.SqlQuery<proc_sw_env_del_nopDTO>("exec proc_sw_env_del_nop_MS  @p_sesion,@p_secuencia",
                new SqlParameter("p_sesion", p_sesion), new SqlParameter("p_secuencia", p_secuencia)).ToList<proc_sw_env_del_nopDTO>();

        }

        public IList<proc_sw_trae_MensajesEliminar_MS_Result> proc_sw_trae_MensajesEliminar(int? codigo)
        {
            return context.proc_sw_trae_MensajesEliminar_MS(codigo).ToList();
        }
        public bool? proc_sw_elimina_MensajeSwift_MS(int? codigo)
        {
            return context.proc_sw_elimina_MensajeSwift_MS(codigo).FirstOrDefault();
        }

        public proc_rh_swi_001DTO proc_rh_swi_001_MS(int rut)
        {
            return context.Database.SqlQuery<proc_rh_swi_001DTO>("exec proc_rh_swi_001_MS  @nurut",
                new SqlParameter("nurut", rut)).ToList<proc_rh_swi_001DTO>().FirstOrDefault();
            //return context.proc_rh_swi_001(rut).ToList();
        }

        public string GetTipoPoder(string codig_rh)
        {
            return context.sw_tipos_firmas.Where(c => c.codig_rh == codig_rh).Select(c => c.tipofirma).FirstOrDefault();
        }

        public void proc_sw_env_update_aut_MS(int idMensaje, int casilla)
        {
            context.proc_sw_env_update_aut_MS(idMensaje, casilla);
        }

        public int proc_sw_env_graba_aum_MS(int idmensaje, int casilla, int rut_log, DateTime fecha_aum, string comentario)
        {
            int result = EjecutarSPConRetornoSinTransaccion("proc_sw_env_graba_aum_MS", String.Empty, new List<string> { idmensaje.ToString(), casilla.ToString(), rut_log.ToString(), fecha_aum.ToString("yyyy-MM-dd HH:mm:ss"), comentario}, null);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idMensaje"></param>
        /// <returns></returns>
        public int proc_sw_validaFirmaAnular_MS(int idMensaje)
        {
            return (int)context.proc_sw_validaFirmaAnular_MS(idMensaje).FirstOrDefault();
        }
    }
}
