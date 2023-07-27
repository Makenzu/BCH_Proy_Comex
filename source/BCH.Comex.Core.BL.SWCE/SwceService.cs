using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Services;
using BCH.Comex.Data.DAL.Swift;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BCH.Comex.Core.BL.SWCE
{
    public class SwceService
    {
        public const string AVOID_SEARCH = "AVOID_SEARCH";
        public const string LIMIT_EXCEEDED = "LIMIT_EXCEEDED";
        public const string MSG_ERROR = "Ha ocurrido un error, contacte al administrador: ";
        public const string buscar = "";

        private UnitOfWorkSwift unitOfWork;
        private UnitOfWorkCext01 unitOfWorkCext01;


        public SwceService()
        {
            this.unitOfWork = new UnitOfWorkSwift();
            this.unitOfWorkCext01 = new UnitOfWorkCext01();

        }
        public IList<proc_sw_env_trae_noaut_rangoDTO> Get(int casilla, DateTime fecha1, DateTime fecha2)
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_trae_noaut_rango_MS(casilla, fecha1, fecha2);
        }

        public IList<proc_sw_env_trae_pro_rango_MS_Result> GetProcesados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_trae_pro_rango(idCasilla, fechaInicio, fechaFin);
        }

        public IList<proc_sw_env_trae_aut_rango_MS_Result> GetAutorizados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_trae_aut_rango(idCasilla, fechaInicio, fechaFin);
        }

        public IList<proc_sw_env_trae_rech_swiDTO> GetRechazados(int casilla, DateTime fecha1, DateTime fecha2)
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_trae_rech_swi(casilla, fecha1, fecha2);
        }

        public IList<proc_sw_env_trae_blo_rango_MS_Result> GetBloqueados(int casilla, DateTime fecha1, DateTime fecha2)
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_trae_blo_rango(casilla, fecha1, fecha2);
        }

        public IList<proc_sw_env_graba_bloDTO> SetBloqueados(int idMensaje, int casilla, int rut, string comentario)
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_graba_blo_MS(idMensaje, casilla, rut, comentario);
        }

        public IList<proc_sw_env_graba_desbloDTO> SetDesbloqueados(int idMensaje, int casilla, int rut)
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_graba_desblo_MS(idMensaje, casilla, rut);
        }

        public IList<proc_sw_env_trae_nul_rango_MS_Result> GetNulos(int casilla, DateTime fecha1, DateTime fecha2)
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_trae_nul_rango(casilla, fecha1, fecha2);
        }

        public int SetNulos(int idMensaje, int rut, int casilla, string comentario)
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_graba_nul(idMensaje, rut, casilla, comentario);
        }

        public IList<proc_sw_env_trae_files_MS_Result> GetPendientes()
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_trae_files_MS("P", new DateTime(1900, 1, 1), DateTime.Now.Date); //quiero los pendientes desde siempre, mando 1/1/1900, lo bastante antiguo. :)
        }

        public enum TipoGrillaAdminSwift
        {
            SinAutorizar,
            Autorizados,
            Rechazados,
            Procesados,
            Bloqueados,
            Anulados
        }

        public MemoryStream GetExcelGrilla(TipoGrillaAdminSwift tipo, int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            MemoryStream stream = new MemoryStream();
            try
            {
                using (SLDocument doc = new SLDocument())
                {
                    string nombreWorksheet = "Resultados";
                    doc.RenameWorksheet(SLDocument.DefaultFirstSheetName, nombreWorksheet);

                    doc.SelectWorksheet(nombreWorksheet);

                    object datos = null;

                    string tituloColumnaFecha = String.Empty;
                    switch (tipo)
                    {
                        case TipoGrillaAdminSwift.SinAutorizar:
                            tituloColumnaFecha = "Fecha/Hora Ingreso";
                            datos = Get(idCasilla, fechaInicio, fechaFin);
                            break;
                        case TipoGrillaAdminSwift.Autorizados:
                            tituloColumnaFecha = "Fecha/Hora Autorizado";
                            datos = GetAutorizados(idCasilla, fechaInicio, fechaFin);
                            break;
                        case TipoGrillaAdminSwift.Rechazados:
                            tituloColumnaFecha = "Fecha/Hora Rechazo";
                            datos = GetRechazados(idCasilla, fechaInicio, fechaFin);
                            break;
                        case TipoGrillaAdminSwift.Procesados:
                            tituloColumnaFecha = "Fecha/Hora Procesado";
                            datos = GetProcesados(idCasilla, fechaInicio, fechaFin);
                            break;
                        case TipoGrillaAdminSwift.Bloqueados:
                            tituloColumnaFecha = "Fecha/Hora Bloqueo";
                            datos = GetBloqueados(idCasilla, fechaInicio, fechaFin);
                            break;
                        case TipoGrillaAdminSwift.Anulados:
                            tituloColumnaFecha = "Fecha/Hora Anulación";
                            datos = GetNulos(idCasilla, fechaInicio, fechaFin);
                            break;
                    }

                    GenerarTituloyEstilosExcel(doc, tituloColumnaFecha);
                    GenerarCuerpoExcel(doc, tipo, datos);        
            
                    doc.SaveAs(stream);
                }

                stream.Position = 0; //importante, dejar el stream pronto para leer;
            }
            catch (Exception ex)
            {
                
            }
            return stream;
        }

        private void GenerarTituloyEstilosExcel(SLDocument doc, string tituloColumnaFecha)
        {
            SLStyle styleTitulo = doc.CreateStyle();
            styleTitulo.Font.Bold = true;
            styleTitulo.Font.FontSize = 12;
            styleTitulo.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleDecimalMonedaExtranjera = doc.CreateStyle();
            styleDecimalMonedaExtranjera.FormatCode = "#,###,###,##0.00";
            styleDecimalMonedaExtranjera.SetHorizontalAlignment(HorizontalAlignmentValues.Right);

            doc.SetColumnStyle(8, styleDecimalMonedaExtranjera);

            SLStyle styleFecha = doc.CreateStyle();
            styleFecha.FormatCode = "dd-MM-yyyy hh:mm:ss";
            doc.SetColumnStyle(9, styleFecha);

            doc.SetCellValue("A1", "N° Mensaje");
            doc.SetCellValue("B1", "Tipo MT");
            doc.SetCellValue("C1", "Casilla");
            doc.SetCellValue("D1", "Nombre Banco Receptor");
            doc.SetCellValue("E1", "Código Banco Receptor");
            doc.SetCellValue("F1", "Referencia");
            doc.SetCellValue("G1", "Mnda");
            doc.SetCellValue("H1", "Monto");
            doc.SetCellValue("I1", tituloColumnaFecha);

            doc.SetCellStyle("A1", "I1", styleTitulo);
        }

        private void GenerarCuerpoExcel(SLDocument doc, TipoGrillaAdminSwift tipo, object datos)
        {
            IList<proc_sw_env_trae_noaut_rangoDTO> sinAutorizar = null;
            IList<proc_sw_env_trae_pro_rango_MS_Result> procesados = null;
            IList<proc_sw_env_trae_aut_rango_MS_Result> autorizados = null;
            IList<proc_sw_env_trae_blo_rango_MS_Result> bloqueados = null;
            IList<proc_sw_env_trae_nul_rango_MS_Result> anulados = null;
            IList<proc_sw_env_trae_rech_swiDTO> rechazados = null;
            
            int rowIndex = 2;
                       
            switch (tipo)
            {
                case TipoGrillaAdminSwift.SinAutorizar:
                    sinAutorizar = datos as IList<proc_sw_env_trae_noaut_rangoDTO>;

                    foreach (proc_sw_env_trae_noaut_rangoDTO row in sinAutorizar)
                    {
                        doc.SetCellValue(rowIndex, 1, row.id_mensaje);
                        doc.SetCellValue(rowIndex, 2, row.tipo_msg);
                        doc.SetCellValue(rowIndex, 3, row.casilla);
                        doc.SetCellValue(rowIndex, 4, row.nombre_banco);
                        doc.SetCellValue(rowIndex, 5, (row.cod_banco_rec ?? string.Empty).Trim() + (row.branch_rec ?? string.Empty).Trim());
                        doc.SetCellValue(rowIndex, 6, row.referencia);
                        doc.SetCellValue(rowIndex, 7, row.cod_moneda);
                        doc.SetCellValue(rowIndex, 8, row.monto);
                        doc.SetCellValue(rowIndex, 9, row.fecha_hora_ingreso);
                        rowIndex++;
                    }
                    break;

                case TipoGrillaAdminSwift.Autorizados:
                    autorizados = datos as IList<proc_sw_env_trae_aut_rango_MS_Result>;
                    foreach (proc_sw_env_trae_aut_rango_MS_Result row in autorizados)
                    {
                        doc.SetCellValue(rowIndex, 1, row.id_mensaje);
                        doc.SetCellValue(rowIndex, 2, row.tipo_msg);
                        doc.SetCellValue(rowIndex, 3, row.casilla);
                        doc.SetCellValue(rowIndex, 4, row.nombre_banco);
                        doc.SetCellValue(rowIndex, 5, (row.cod_banco_rec ?? string.Empty).Trim() + (row.branch_rec ?? string.Empty).Trim());
                        doc.SetCellValue(rowIndex, 6, row.referencia);
                        doc.SetCellValue(rowIndex, 7, row.cod_moneda);
                        if(row.monto.HasValue)
                        {
                            doc.SetCellValue(rowIndex, 8, row.monto.Value);
                        }
                        doc.SetCellValue(rowIndex, 9, row.fecha_apr + " " + row.hora_apr);
                        rowIndex++;
                    }
                    break;

                case TipoGrillaAdminSwift.Rechazados:
                    rechazados = datos as IList<proc_sw_env_trae_rech_swiDTO>;
 
                    foreach (proc_sw_env_trae_rech_swiDTO row in rechazados)
                    {
                        doc.SetCellValue(rowIndex, 1, row.id_mensaje);
                        doc.SetCellValue(rowIndex, 2, row.tipo_msg);
                        doc.SetCellValue(rowIndex, 3, row.casilla);
                        doc.SetCellValue(rowIndex, 4, row.nombre_banco);
                        doc.SetCellValue(rowIndex, 5, (row.cod_banco_rec ?? string.Empty).Trim() + (row.branch_rec ?? string.Empty).Trim());
                        doc.SetCellValue(rowIndex, 6, row.referencia);
                        doc.SetCellValue(rowIndex, 7, row.cod_moneda);
                        doc.SetCellValue(rowIndex, 8, row.monto);
                        doc.SetCellValue(rowIndex, 9, row.fecha_rechazo + " " + row.hora_rechazo);
                        rowIndex++;
                    }
                    break;

                case TipoGrillaAdminSwift.Procesados:
                    procesados = datos as IList<proc_sw_env_trae_pro_rango_MS_Result>;

                    foreach (proc_sw_env_trae_pro_rango_MS_Result row in procesados)
                    {
                        doc.SetCellValue(rowIndex, 1, row.id_mensaje);
                        doc.SetCellValue(rowIndex, 2, row.tipo_msg);
                        doc.SetCellValue(rowIndex, 3, row.casilla);
                        doc.SetCellValue(rowIndex, 4, row.nombre_banco);
                        doc.SetCellValue(rowIndex, 5, (row.cod_banco_rec ?? string.Empty).Trim() + (row.branch_rec ?? string.Empty).Trim());
                        doc.SetCellValue(rowIndex, 6, row.referencia);
                        doc.SetCellValue(rowIndex, 7, row.cod_moneda);
                        if (row.monto.HasValue)
                        {
                            doc.SetCellValue(rowIndex, 8, row.monto.Value);
                        }
                        doc.SetCellValue(rowIndex, 9, row.fecha_pro + " " + row.hora_pro);
                        rowIndex++;
                    }
                    break;

                case TipoGrillaAdminSwift.Bloqueados:
                    bloqueados = datos as IList<proc_sw_env_trae_blo_rango_MS_Result>;

                    foreach (proc_sw_env_trae_blo_rango_MS_Result row in bloqueados)
                    {
                        doc.SetCellValue(rowIndex, 1, row.id_mensaje);
                        doc.SetCellValue(rowIndex, 2, row.tipo_msg);
                        doc.SetCellValue(rowIndex, 3, row.casilla);
                        doc.SetCellValue(rowIndex, 4, row.nombre_banco);
                        doc.SetCellValue(rowIndex, 5, (row.cod_banco_rec ?? string.Empty).Trim() + (row.branch_rec ?? string.Empty).Trim());
                        doc.SetCellValue(rowIndex, 6, row.referencia);
                        doc.SetCellValue(rowIndex, 7, row.cod_moneda);
                        if (row.monto.HasValue)
                        {
                            doc.SetCellValue(rowIndex, 8, row.monto.Value);
                        }
                        doc.SetCellValue(rowIndex, 9, row.fecha_bloq + " " + row.hora_bloq);
                        rowIndex++;
                    }

                    break;

                case TipoGrillaAdminSwift.Anulados:
                    anulados = datos as IList<proc_sw_env_trae_nul_rango_MS_Result>;

                    foreach (proc_sw_env_trae_nul_rango_MS_Result row in anulados)
                    {
                        doc.SetCellValue(rowIndex, 1, row.id_mensaje);
                        doc.SetCellValue(rowIndex, 2, row.tipo_msg);
                        doc.SetCellValue(rowIndex, 3, row.casilla);
                        doc.SetCellValue(rowIndex, 4, row.nombre_banco);
                        doc.SetCellValue(rowIndex, 5, (row.cod_banco_rec ?? string.Empty).Trim() + (row.branch_rec ?? string.Empty).Trim());
                        doc.SetCellValue(rowIndex, 6, row.referencia);
                        doc.SetCellValue(rowIndex, 7, row.cod_moneda);
                        if (row.monto.HasValue)
                        {
                            doc.SetCellValue(rowIndex, 8, row.monto.Value);
                        }
                        doc.SetCellValue(rowIndex, 9, row.fecha_anula + " " + row.hora_anula);
                        rowIndex++;
                    }
                    break;
            }

            doc.AutoFitColumn(1, 9);
        }



        //public IList<proc_sw_env_trae_files_MS_Result> GetRecepcionados(DateTime fecha1, DateTime fecha2)
        //{
        //    return unitOfWork.AdministracionRepository.proc_sw_env_trae_files("R", fecha1, fecha2);
        //}

        public IList<proc_sw_env_del_firnulDTO> SetFirmasEliminadas(int id_mensaje, int rut, DateTime fecha)
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_del_firnul_MS(id_mensaje, rut, fecha);
        }

        public proc_rh_swi_001DTO GetPoderes(int rut)
        {
            proc_rh_swi_001DTO poder = unitOfWork.AdministracionRepository.proc_rh_swi_001_MS(rut);
            if (poder != null)
            {
                poder.PoderTipoFirma = GetTipoPoder(poder.fun_atributo);
                if (string.IsNullOrEmpty(poder.PoderTipoFirma))
                    poder.PoderTipoFirma = poder.GetPoderTipoFirma;
            }
            return poder;
        }

        public string GetTipoPoder(string codig_rh)
        {
            return unitOfWork.AdministracionRepository.GetTipoPoder(codig_rh);
        }

        public bool UpdateFirma(int idmensaje, int rut, string p_tipo_firma, string p_estado, string p_revfir, int p_rut_solic, string p_fecha_solic, string p_avisado)
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_ing_firma(idmensaje, rut, p_tipo_firma, p_estado, p_revfir, p_rut_solic, p_fecha_solic, p_avisado);
        }

        public int GrabaRechazar(int idMensaje, int rut, int casilla, string comentario)
        {
            return unitOfWork.AdministracionRepository.Proc_Sw_Env_Graba_Rec(idMensaje, rut, casilla, "Mensaje Rechazado", comentario, "REM");
        }

        public IList<proc_sw_env_trae_ftp_err_MS_Result> GetNoTransmitidos(DateTime fecha1, DateTime fecha2)
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_trae_ftp_err_MS(fecha1, fecha2);
        }

        public IList<proc_sw_env_trae_detfile_MS_Result> GetDetalleArchivo(int idArchivo)
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_trae_detfile_MS(idArchivo);
        }

        public IList<proc_sw_env_graba_fileDTO> Recepcion(int idArchivo)
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_graba_file_MS(idArchivo);
        }

        public IList<proc_sw_env_graba_ftp_reeDTO> reenvio(int idArchivo)
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_graba_ftp_ree_MS(idArchivo);
        }

        public IList<proc_sw_env_trae_nop_MS_Result> GetNop(DateTime FechaInicio, DateTime FechaFin)
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_trae_nop_MS(FechaInicio, FechaFin);
        }

        public IList<proc_sw_trae_firma_MS_Result> GetFirmas(int idCasilla)
        {
            return unitOfWork.AdministracionRepository.proc_sw_trae_firma_MS(idCasilla);
        }

        public IList<proc_sw_env_graba_envDTO> GrabaPareoAceptado(int p_id_mensaje, int p_sesion, int p_secuencia, DateTime p_fecha_env, int p_rut_log, int p_unidad)
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_graba_env_MS(p_id_mensaje, p_sesion, p_secuencia, p_fecha_env, p_rut_log, p_unidad);
        }

        public IList<proc_sw_env_graba_resDTO> GrabaPareoRechazado(int p_id_mensaje, DateTime p_fecha_res, int p_rut_log, int p_unidad, string p_texto)
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_graba_res_MS(p_id_mensaje, p_fecha_res, p_rut_log, p_unidad, p_texto);
        }

        public ImageStringEnvioAutomatico GetFirmaImage(string rut)
        {
            return SWCEServices.RetornaFirma(rut);
        }

        public IList<proc_sw_env_del_nopDTO> EliminaDetallePareo(int p_sesion, int p_secuencia)
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_del_nop_MS(p_sesion, p_secuencia);
        }

        public IList<proc_sw_trae_MensajesEliminar_MS_Result> GetMensajeEliminar(int? codigo)
        {
            return unitOfWork.AdministracionRepository.proc_sw_trae_MensajesEliminar(codigo);
        }
        public bool? EliminaMensaje(int? codigo)
        {
            return unitOfWork.AdministracionRepository.proc_sw_elimina_MensajeSwift_MS(codigo);
        }


        public IList<PoderUsuario> GetFirmasLocales(string strFirmasSerializadas)
        {
            List<PoderUsuario> list = new List<PoderUsuario>();
            if (!String.IsNullOrEmpty(strFirmasSerializadas))
            {
                string[] usuarios = strFirmasSerializadas.Split(new string[] { "#$#" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string strUsuario in usuarios)
                {
                    string[] partesUsuario = strUsuario.Split(new string[] { "!@!" }, StringSplitOptions.RemoveEmptyEntries);
                    PoderUsuario poderUsuario = new PoderUsuario()
                    {
                        Rut = partesUsuario[0],
                        Nombre = partesUsuario[1]
                    };

                    int intRut = RutEnFormatoBDSwift(poderUsuario.Rut);
                    proc_rh_swi_001DTO poder = this.GetPoderes(intRut);
                    if (poder != null)
                    {
                        poderUsuario.Poder = poder.Poder;
                        poderUsuario.FunAtributo = poder.fun_atributo;
                        poderUsuario.PoderTipoFirma = poder.PoderTipoFirma;
                    }
                    list.Add(poderUsuario);
                }
            }
            return list;
        }

        public string SerializarFirmasLocales(IList<PoderUsuario> firmasLocales)
        {
            if (firmasLocales != null && firmasLocales.Count > 0)
            {
                List<string> strUsuarios = new List<string>();

                firmasLocales = firmasLocales.OrderBy(m => m.Nombre).ToList();
                foreach (PoderUsuario poder in firmasLocales)
                {
                    strUsuarios.Add(poder.Rut + "!@!" + poder.Nombre);
                }

                return String.Join("#$#", strUsuarios.ToArray());
            }
            else return string.Empty;
        }

        private static int RutEnFormatoBDSwift(string rut)
        {
            if (rut.Contains("-"))
            {
                rut = rut.Replace("-", "");
                rut = rut.Replace(".", "");
            }

            if (!String.IsNullOrEmpty(rut))
            {
                if (rut.Length > 7)
                {
                    return int.Parse(rut.Substring(0, rut.Length - 1)); //dejo afuera el digito verificador
                }
                else
                {
                    return int.Parse(rut);
                }
            }
            else return 0;
        }

        public void updateAutorizada(int idMensaje, int Casilla)
        {

            unitOfWork.AdministracionRepository.proc_sw_env_update_aut_MS(idMensaje, Casilla);

        }

        /// <summary>
        /// Anular mensaje Swift de una Operación
        /// </summary>
        /// <param name="idMensaje"></param>
        /// <param name="rut"></param>
        /// <param name="casilla"></param>
        /// <param name="comentario"></param>
        /// <returns></returns>
        public string proc_sw_env_graba_nul_MS(int idMensaje, int rut, int casilla, string comentario)
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_graba_nul_MS(idMensaje, rut, casilla, comentario);
        }

        /// <summary>
        /// Validar si el Swift anular, si contiene firmas autorizadas.
        /// </summary>
        /// <param name="idMensaje"></param>
        /// <returns></returns>
        public int proc_sw_validaFirmaAnular_MS(int idMensaje)
        {
            return unitOfWork.AdministracionRepository.proc_sw_validaFirmaAnular_MS(idMensaje);
        }

        #region Procedimientos Migracion

        public IList<proc_sw_env_trae_files_MS_Result> GetRecepcionados(DateTime fecha1, DateTime fecha2)
        {
            return unitOfWork.AdministracionRepository.proc_sw_env_trae_files_MS("R", fecha1, fecha2);
        }

        public int CantidadeRecepcionados(DateTime fecha1, DateTime fecha2)
        {

            return unitOfWork.AdministracionRepository.proc_sw_env_trae_files_MS("R", fecha1, fecha2).Count();
        }


        public object GetRecepcionadosCount(string buscar, DateTime fecha1, DateTime fecha2)
        {
            object data;
            try
            {
                if (buscar == "")
                {
                    data = new List<proc_sw_env_trae_files_MS_Result>();
                }

                int cantidad = CantidadeRecepcionados(fecha1, fecha2);

                if (cantidad > 100000)
                {
                    data = LIMIT_EXCEEDED;
                }

                else if (cantidad == 0)
                {
                    data = new List<proc_sw_env_trae_files_MS_Result>();
                }

                else
                {

                    data = GetRecepcionados(fecha1, fecha2).
                        Select(i => new proc_sw_env_trae_files_fecha_string_MS_Result
                        {
                         estado_archivo = i.estado_archivo,
                         fd_archivo = i.fd_archivo,
                         fecha_confirma = i.fecha_confirma.ToString(),
                         fecha_creacion = i.fecha_creacion.ToString(),
                         nombre_archivo = i.nombre_archivo,
                         total_envios = i.total_envios,
                         total_mensajes = i.total_mensajes,
                         total_rechazos = i.total_rechazos
                        }).ToList();


                }
                //}
            }
            catch (Exception e)
            {
                //data = MSG_ERROR + e.Message;
                return null;
            }
            return data;
        }

        #endregion

    }
}
