using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.Data.DAL.Swift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using BCH.Comex.Common.Tracing;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using BCH.Comex.Core.Entities.Cext01.GestionControlSwift;

namespace BCH.Comex.Core.BL.SWGC
{
    public class SwgcService
    {
        private UnitOfWorkSwift unitOfWork;
        //private UnitOfWorkSwift uow;
        //protected TContext context;
        
        public SwgcService()
        {
            this.unitOfWork = new UnitOfWorkSwift();
        }

        public IList<proc_sw_rec_trae_iny_rangoDTO> GetSinEncasillar(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {

            return unitOfWork.GestionControlRepository.proc_sw_rec_trae_iny_rango(idCasilla, fechaInicio, fechaFin);
        }

        public IList<proc_sw_rec_trae_iny_rangoDTO> GetImpresos(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {

            return unitOfWork.GestionControlRepository.proc_sw_rec_trae_iny_rango_por_estado_impresos(idCasilla, fechaInicio, fechaFin);
        }

        public IList<proc_sw_rec_trae_iny_rangoDTO> GetRechazados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {

            return unitOfWork.GestionControlRepository.proc_sw_rec_trae_iny_rango_por_estado_rechazados(idCasilla, fechaInicio, fechaFin);
        }


        public IList<proc_sw_rec_trae_iny_rangoDTO> GetPosiblesDuplicados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {

            return unitOfWork.GestionControlRepository.proc_sw_rec_trae_iny_rango_por_estado_posibles_duplicados(idCasilla, fechaInicio, fechaFin);
        }

        public IList<proc_sw_rec_trae_iny_rangoDTO> GetEncasillados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {

            return unitOfWork.GestionControlRepository.proc_sw_rec_trae_iny_rango_por_estado_encasillados(idCasilla, fechaInicio, fechaFin);
        }

        public IList<proc_sw_rec_trae_iny_rangoDTO> GetConfirmados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {

            return unitOfWork.GestionControlRepository.proc_sw_rec_trae_iny_rango_por_estado_confirmados(idCasilla, fechaInicio, fechaFin);
        }

        public IList<proc_sw_rec_trae_iny_rangoDTO> GetReenviados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {

            return unitOfWork.GestionControlRepository.proc_sw_rec_trae_iny_rango_por_estado_reenviados(idCasilla, fechaInicio, fechaFin);
        }

        public IList<proc_sw_rec_trae_iny_rangoDTO> GetTodos(int idCasilla, DateTime fechaInicio, DateTime fechaFin, string _codEstado)
        {
            if (_codEstado == "0")
            {
                return unitOfWork.GestionControlRepository.proc_sw_rec_trae_iny_rango(idCasilla, fechaInicio, fechaFin);

            }
            else {
                var result = unitOfWork.GestionControlRepository.proc_sw_rec_trae_iny_rango(idCasilla, fechaInicio, fechaFin).ToList();
                return result.Where(x=>x.estado_msg ==_codEstado).ToList();
            }
        }

        //IEnumerable
        public IEnumerable<sw_casillas> GetTodasLasCasillas()
        {
            return unitOfWork.CasillaRepository.Get(orderBy: (q => q.OrderBy(c => c.nombre_casilla)));
        }

        public IList<sw_casillas> GetTodasLasCasillas1()
        {
            return unitOfWork.CasillaRepository.Get1(orderBy: (q => q.OrderBy(c => c.nombre_casilla)));
        }

        public IList<ResultadoBusquedaSwift> BuscarSwiftsPorCasillaYFechas(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, int? rutUsuario, bool enviados, out int totalCount, int? rowOffset, short? fetchRows, string searchText)
        {
            if (enviados)
            {
                return unitOfWork.MensajeRepository.BuscarSwiftsEnviadosPorCasillaYFechas(idCasilla, fechaDesde, fechaHasta, rutUsuario, out totalCount, rowOffset, fetchRows, searchText);
            }
            else
            {
                //recibidos
                return unitOfWork.MensajeRepository.BuscarSwiftsRecibidosPorCasillaYFechas(idCasilla, fechaDesde, fechaHasta, rutUsuario, out totalCount, rowOffset, fetchRows, searchText);
            }
        }


        public IList<proc_sw_rec_controlDTO> GetControlRecepcionMensajes(int idCasilla, DateTime fechaDesde)
        {

            return unitOfWork.GestionControlRepository.proc_sw_rec_control(idCasilla, fechaDesde);
        }

        public IList<proc_sw_log_trae_enc_rangoDTO> GetTodosEncasillamientoManual(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {

            return unitOfWork.GestionControlRepository.proc_sw_log_trae_enc_rango(idCasilla, fechaInicio, fechaFin);
        }


        public IList<proc_sw_env_estadist_msgDTO> GetEstadisticasEnviados(int idCasilla, DateTime fechaDesde, DateTime fechaHasta)
        {

            return unitOfWork.GestionControlRepository.proc_sw_env_estadist_msg(idCasilla, fechaDesde, fechaHasta);
            
        }

        public IList<proc_sw_rec_estadist_msgDTO> GetEstadisticasRecibidos(int idCasilla, DateTime fechaDesde, DateTime fechaHasta)
        {

            return unitOfWork.GestionControlRepository.proc_sw_rec_estadist_msg(idCasilla, fechaDesde, fechaHasta);

        }

        public IList<proc_sw_log_trae_aut_rangoDTO> GetTodosEncasillamientoAutomatico(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {

            return unitOfWork.GestionControlRepository.proc_sw_log_trae_aut_rango_MS(idCasilla, fechaInicio, fechaFin);
        }

        public IList<proc_sw_env_trae_env_rangoDTO> GetTodosMensajesEnviadosExterior(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return unitOfWork.GestionControlRepository.proc_sw_env_trae_env_rango_MS(idCasilla, fechaInicio, fechaFin);
        }

        public IList<proc_sw_env_trae_env_rangoDTO> GetTodosMensajesEnviadosExteriorPaginado(int idCasilla, DateTime fechaInicio, DateTime fechaFin, int offset, int fetchrows)
        {
            return unitOfWork.GestionControlRepository.proc_sw_env_trae_env_rango_paginado_MS(idCasilla, fechaInicio, fechaFin, offset, fetchrows);
        }

        //public IList<proc_sw_log_trae_msgsendDTO> GetLogDeMensajeEnviado(int idMensaje)
        //{
        //    IList<proc_sw_log_trae_msgsendDTO> logs = unitOfWork.gestionControlRepository.GetLogDeMensajeEnviado(idMensaje);
        //    //ComplementarLogsConNombresPersonas1(logs);
        //    return logs;
        //}


        //private void ComplementarLogsConNombresPersonas1(IList<proc_sw_log_trae_msgsendDTO> logs)
        //{
        //    if (logs != null)
        //    {
        //        //busco los nombres de las personas ya que el log solo me trae los ruts

        //        IList<proc_sw_log_trae_msgsendDTO> logsConRutAis = logs.Where(l => l.rutais_log != null).ToList();
        //        int[] rutsDeInteres = logsConRutAis.Select(l => l.rutais_log.Value).ToArray();
        //        IList<sce_usr> usuariosLogs = GetUsuariosDeCextEnBaseARutsDeSwift(rutsDeInteres);

        //        foreach (proc_sw_log_trae_msgsendDTO log in logsConRutAis)
        //        {
        //            sce_usr usr = BuscarUsuarioCextComparandoConRutSwift(usuariosLogs, log.rutais_log.Value);
        //            if (usr != null)
        //            {
        //                log.NombrePersonaAis = usr.nombre;
        //            }
        //        }
        //    }
        //}

        //private IList<sce_usr> GetUsuariosDeCextEnBaseARutsDeSwift(params int[] rutsDeInteres)
        //{

        //    if (rutsDeInteres != null && rutsDeInteres.Length > 0)
        //    {
        //        using (UnitOfWorkCext01 uowCext = new UnitOfWorkCext01())
        //        {
        //            IList<string> rutsDeInteresComoFormateo = rutsDeInteres.Distinct().Select(r => r.ToString().PadLeft(9, '0')).ToList();
        //            return uowCext.UsuarioRepository.Get(u => rutsDeInteresComoFormateo.Contains(u.rut.Substring(0, u.rut.Length - 1))).ToList(); //esta consulta deberia ser un SP
        //        }
        //    }
        //    else return null;
        //}


        //private sce_usr BuscarUsuarioCextComparandoConRutSwift(IList<sce_usr> users, int rutSwift)
        //{
        //    return users.Where(u => u.rut.Substring(0, u.rut.Length - 1) == rutSwift.ToString().PadLeft(9, '0')).FirstOrDefault();
        //}

        public IList<proc_sw_env_trae_filesDTO> Getproc_sw_env_trae_files(DateTime fechaDesde, DateTime fechaHasta)
        {

            return unitOfWork.GestionControlRepository.proc_sw_env_trae_files_MS(fechaDesde, fechaHasta);

        }

        public IList<proc_sw_env_trae_filesDTO> Getproc_sw_env_trae_files02(DateTime fechaDesde, DateTime fechaHasta)
        {

            return unitOfWork.GestionControlRepository.proc_sw_env_trae_files02(fechaDesde, fechaHasta);

        }

        public IList<proc_sw_rec_trae_resumen_msgDTO> proc_sw_rec_trae_resumen_msg(DateTime fechaDesde, DateTime fechaHasta)
        {
            return unitOfWork.GestionControlRepository.proc_sw_rec_trae_resumen_msg(fechaDesde, fechaHasta);
        }

        //public sw_msgsend Get(int id_mensaje)
        //{
        //    string query = "select * from sw_msgsend where id_mensaje=" + id_mensaje;
        //    sw_msgsend sw_msgsend = context.sw_msgsend.SqlQuery(query.ToString()).FirstOrDefault();
        //    return sw_msgsend;
        //}

        //ObtSecPorSec(int sec, DateTime fecha)
        public string ObtSecPorSec(int sec, DateTime fecha)
        {
            return unitOfWork.GestionControlRepository.ObtSecPorSec(sec, fecha);
        }

        public MemoryStream getExcel(IList<proc_sw_rec_trae_iny_rangoDTO> datos)
        {
            using (Tracer tracer = new Tracer("getExcel Control y Gestion"))
            {
                MemoryStream stream = new MemoryStream();
                try
                {
                    string NombreWorksheet = "GESTION";
                    using (SLDocument doc = new SLDocument())
                    {
                        doc.RenameWorksheet(SLDocument.DefaultFirstSheetName, NombreWorksheet);

                        doc.SelectWorksheet(NombreWorksheet);
                        CargarHojaExcel(datos, doc);
                        doc.SaveAs(stream);
                    }

                    stream.Position = 0; //importante, dejar el stream pronto para leer;

                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alera, no se ha podido generar archivo excel de Control y Gestion Swift", ex);

                }
                return stream;
            }
        }

        #region Exportar a Excel Encasillamiento Manual y Automatico
        public static MemoryStream GetExportedFile(IList<ResultItem> datos, string nombreWorkSheet)
        {
            using (Tracer tracer = new Tracer("getExportedFile"))
            {
                MemoryStream stream = new MemoryStream();
                try
                {

                    using (SLDocument doc = new SLDocument())
                    {
                        doc.RenameWorksheet(SLDocument.DefaultFirstSheetName, nombreWorkSheet);

                        doc.SelectWorksheet(nombreWorkSheet);
                        CargarHojaEncasillamiento(datos, doc);
                        doc.SaveAs(stream);
                    }

                    stream.Position = 0; //importante, dejar el stream pronto para leer;

                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, no se ha podido generar archivo excel en Encasillamiento Swift.", ex);
                }
                return stream;
            }
        }

        private static void CargarHojaEncasillamiento(IList<ResultItem> acs, SLDocument doc)
        {
            #region Estilos
            SLStyle styleFont = doc.CreateStyle();
            styleFont.Font.FontSize = 10;

            SLStyle styleBold = doc.CreateStyle();
            styleBold.Font.Bold = true;
            styleBold.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleFechaBold = doc.CreateStyle();
            styleFechaBold.Font.Bold = true;
            styleFechaBold.FormatCode = "dd-mm-yyyy";

            SLStyle styleFecha = doc.CreateStyle();
            styleFecha.FormatCode = "dd-MM-yyyy";

            SLStyle styleDecimalMonto = doc.CreateStyle();
            styleDecimalMonto.FormatCode = "#,##0";

            SLStyle styleDecimalMonedaExtranjera = doc.CreateStyle();
            styleDecimalMonedaExtranjera.FormatCode = "#,##0.00";

            SLStyle styleDecimal = doc.CreateStyle();
            styleDecimal.FormatCode = "0.00####";

            SLStyle styleInt = doc.CreateStyle();
            styleInt.FormatCode = "#"; 
            #endregion

            doc.SetRowStyle(1, styleFont);
            doc.SetColumnStyle(1, acs.Count(), styleFont);
            doc.SetRowStyle(1, styleBold);
            doc.SetColumnStyle(5, styleFecha);
            doc.SetColumnStyle(9, styleDecimalMonedaExtranjera);

            int colIndex = 1;
            int rowIndex = 1;

            #region titulos
            doc.SetCellValue(rowIndex, colIndex++, "Secuencia");
            doc.SetCellValue(rowIndex, colIndex++, "Sesión");
            doc.SetCellValue(rowIndex, colIndex++, "Tipo");
            doc.SetCellValue(rowIndex, colIndex++, "Fecha Recepción");
            doc.SetCellValue(rowIndex, colIndex++, "Referencia");
            doc.SetCellValue(rowIndex, colIndex++, "Beneficiario");
            doc.SetCellValue(rowIndex, colIndex++, "Moneda");
            doc.SetCellValue(rowIndex, colIndex++, "Monto");
            #endregion

            DataTable dt =  SwgcService.ConvertToDataTable(acs);

            doc.ImportDataTable(1, 1, dt, true);

            doc.SetColumnStyle(8, styleDecimal);

            doc.AutoFitColumn("A", "I");
        }

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        #endregion

        public MemoryStream getExcel(IList<proc_sw_env_trae_env_rangoDTO> datos)
        {
            using (Tracer tracer = new Tracer("getExcel Control y Gestion"))
            {
                MemoryStream stream = new MemoryStream();
                try
                {
                    string NombreWorksheet = "GESTION";
                    using (SLDocument doc = new SLDocument())
                    {
                        doc.RenameWorksheet(SLDocument.DefaultFirstSheetName, NombreWorksheet);

                        doc.SelectWorksheet(NombreWorksheet);
                        CargarHojaExcel(datos, doc);
                        doc.SaveAs(stream);
                    }

                    stream.Position = 0; //importante, dejar el stream pronto para leer;

                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alera, no se ha podido generar archivo excel de Control y Gestion Swift", ex);

                }
                return stream;
            }
        }
        private void CargarHojaExcel(IList<proc_sw_rec_trae_iny_rangoDTO> acs, SLDocument doc)
        {
            #region style
            SLStyle styleTitulo = doc.CreateStyle();
            styleTitulo.Font.Bold = true;
            styleTitulo.Font.FontSize = 15;
            styleTitulo.Font.FontColor = System.Drawing.Color.Blue;
            styleTitulo.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleSubtitulo = doc.CreateStyle();
            styleSubtitulo.Font.Bold = true;
            styleSubtitulo.Font.FontSize = 13;
            styleSubtitulo.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleEncabezado = doc.CreateStyle();
            styleEncabezado.Font.Bold = true;
            styleEncabezado.Font.FontColor = System.Drawing.Color.Blue;
            styleEncabezado.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleBold = doc.CreateStyle();
            styleBold.Font.Bold = true;
            styleBold.SetHorizontalAlignment(HorizontalAlignmentValues.Left);

            SLStyle styleFechaBold = doc.CreateStyle();
            styleFechaBold.Font.Bold = true;
            styleFechaBold.FormatCode = "dd/MM/yyyy";

            SLStyle styleFecha = doc.CreateStyle();
            styleFecha.FormatCode = "dd/MM/yyyy";

            SLStyle styleDecimalMonedaExtranjera = doc.CreateStyle();
            styleDecimalMonedaExtranjera.FormatCode = "#,##0.00";

            SLStyle styleDecimalPesosChilenos = doc.CreateStyle();
            styleDecimalPesosChilenos.FormatCode = "#,###,###,##0";

            SLStyle styleRut = doc.CreateStyle();
            styleRut.FormatCode = "########-#";

            SLStyle styleDecimal = doc.CreateStyle();
            styleDecimal.FormatCode = "#0.00";

            SLStyle styleInt = doc.CreateStyle();
            styleInt.FormatCode = "0";
            #endregion

            int colIndex = 1;
            int rowIndex = 1;
            #region titulos
            doc.SetCellValue(rowIndex, colIndex++, "sesion");
            doc.SetCellValue(rowIndex, colIndex++, "secuencia");
            doc.SetCellValue(rowIndex, colIndex++, "casilla");
            doc.SetCellValue(rowIndex, colIndex++, "nombre_casilla");
            doc.SetCellValue(rowIndex, colIndex++, "tipo_msg");
            doc.SetCellValue(rowIndex, colIndex++, "nombre_tipo");
            doc.SetCellValue(rowIndex, colIndex++, "prioridad");
            doc.SetCellValue(rowIndex, colIndex++, "estado_msg");
            doc.SetCellValue(rowIndex, colIndex++, "compute_0006");
            doc.SetCellValue(rowIndex, colIndex++, "compute_0007");
            doc.SetCellValue(rowIndex, colIndex++, "cod_banco_rec");
            doc.SetCellValue(rowIndex, colIndex++, "branch_rec");
            doc.SetCellValue(rowIndex, colIndex++, "cod_banco_em");
            doc.SetCellValue(rowIndex, colIndex++, "branch_em");
            doc.SetCellValue(rowIndex, colIndex++, "nombre_banco");
            doc.SetCellValue(rowIndex, colIndex++, "ciudad_banco");
            doc.SetCellValue(rowIndex, colIndex++, "pais_banco");
            doc.SetCellValue(rowIndex, colIndex++, "oficina_banco");
            doc.SetCellValue(rowIndex, colIndex++, "cod_moneda");
            doc.SetCellValue(rowIndex, colIndex++, "monto");
            doc.SetCellValue(rowIndex, colIndex++, "referencia");
            doc.SetCellValue(rowIndex, colIndex++, "beneficiario");
            doc.SetCellValue(rowIndex, colIndex++, "total_imp");
            doc.SetCellValue(rowIndex, colIndex++, "comentario");
            #endregion

            doc.SetCellStyle("A1", "X1", styleEncabezado);


            //doc.SetColumnStyle(1, styleInt);
            //doc.SetColumnStyle(4, styleFecha);
            //doc.SetColumnStyle(9, styleInt);
            //doc.SetColumnStyle(11, styleDecimalMonedaExtranjera);
            //doc.SetColumnStyle(13, styleRut);
            //doc.SetColumnStyle(14, styleInt);
            //doc.SetColumnStyle(15, styleDecimal);

            DataTable dt = ConvertToDataTable(acs);
            doc.ImportDataTable(2, 1, dt, false);


            doc.AutoFitColumn("A", "X");
        }

        private void CargarHojaExcel(IList<proc_sw_env_trae_env_rangoDTO> acs, SLDocument doc)
        {
            #region style
            SLStyle styleTitulo = doc.CreateStyle();
            styleTitulo.Font.Bold = true;
            styleTitulo.Font.FontSize = 15;
            styleTitulo.Font.FontColor = System.Drawing.Color.Blue;
            styleTitulo.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleSubtitulo = doc.CreateStyle();
            styleSubtitulo.Font.Bold = true;
            styleSubtitulo.Font.FontSize = 13;
            styleSubtitulo.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleEncabezado = doc.CreateStyle();
            styleEncabezado.Font.Bold = true;
            styleEncabezado.Font.FontColor = System.Drawing.Color.Blue;
            styleEncabezado.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleBold = doc.CreateStyle();
            styleBold.Font.Bold = true;
            styleBold.SetHorizontalAlignment(HorizontalAlignmentValues.Left);

            SLStyle styleFechaBold = doc.CreateStyle();
            styleFechaBold.Font.Bold = true;
            styleFechaBold.FormatCode = "dd/MM/yyyy";

            SLStyle styleFecha = doc.CreateStyle();
            styleFecha.FormatCode = "dd/MM/yyyy";

            SLStyle styleDecimalMonedaExtranjera = doc.CreateStyle();
            styleDecimalMonedaExtranjera.FormatCode = "#,##0.00";

            SLStyle styleDecimalPesosChilenos = doc.CreateStyle();
            styleDecimalPesosChilenos.FormatCode = "#,###,###,##0";

            SLStyle styleRut = doc.CreateStyle();
            styleRut.FormatCode = "########-#";

            SLStyle styleDecimal = doc.CreateStyle();
            styleDecimal.FormatCode = "#0.00";

            SLStyle styleInt = doc.CreateStyle();
            styleInt.FormatCode = "0";
            #endregion

            int colIndex = 1;
            int rowIndex = 1;
            #region titulos
            doc.SetCellValue(rowIndex, colIndex++, "id_mensaje");
            doc.SetCellValue(rowIndex, colIndex++, "sesion");
            doc.SetCellValue(rowIndex, colIndex++, "secuencia");
            doc.SetCellValue(rowIndex, colIndex++, "casilla");
            doc.SetCellValue(rowIndex, colIndex++, "rut_ingreso");
            doc.SetCellValue(rowIndex, colIndex++, "monto");
            doc.SetCellValue(rowIndex, colIndex++, "nombre_casilla");
            doc.SetCellValue(rowIndex, colIndex++, "tipo_msg");
            doc.SetCellValue(rowIndex, colIndex++, "nombre_tipo");
            doc.SetCellValue(rowIndex, colIndex++, "prioridad");
            doc.SetCellValue(rowIndex, colIndex++, "estado_msg");
            doc.SetCellValue(rowIndex, colIndex++, "fecha_ingr"); //12
            doc.SetCellValue(rowIndex, colIndex++, "hora_ingr");
            doc.SetCellValue(rowIndex, colIndex++, "fecha_env");
            doc.SetCellValue(rowIndex, colIndex++, "hora_env");
            doc.SetCellValue(rowIndex, colIndex++, "cod_banco_rec");
            doc.SetCellValue(rowIndex, colIndex++, "branch_rec");
            doc.SetCellValue(rowIndex, colIndex++, "cod_banco_em");
            doc.SetCellValue(rowIndex, colIndex++, "branch_em");
            doc.SetCellValue(rowIndex, colIndex++, "nombre_banco");
            doc.SetCellValue(rowIndex, colIndex++, "ciudad_banco");
            doc.SetCellValue(rowIndex, colIndex++, "pais_banco");
            doc.SetCellValue(rowIndex, colIndex++, "oficina_banco");
            doc.SetCellValue(rowIndex, colIndex++, "cod_moneda");
            doc.SetCellValue(rowIndex, colIndex++, "nombre_moneda");
            doc.SetCellValue(rowIndex, colIndex++, "cod_moneda_banco");
            doc.SetCellValue(rowIndex, colIndex++, "referencia");
            doc.SetCellValue(rowIndex, colIndex++, "beneficiario");
            #endregion

            doc.SetCellStyle("A1", "AB1", styleEncabezado);

            doc.SetColumnStyle(5, styleRut);
            doc.SetColumnStyle(12, styleFecha);
            doc.SetColumnStyle(14, styleFecha);
            //doc.SetColumnStyle(1, styleInt);           
            //doc.SetColumnStyle(9, styleInt);
            //doc.SetColumnStyle(11, styleDecimalMonedaExtranjera);
            //doc.SetColumnStyle(14, styleInt);
            //doc.SetColumnStyle(15, styleDecimal);

            DataTable dt = ConvertToDataTable(acs);
            doc.ImportDataTable(2, 1, dt, false);

            doc.AutoFitColumn("A", "X");
        }
    }
}
