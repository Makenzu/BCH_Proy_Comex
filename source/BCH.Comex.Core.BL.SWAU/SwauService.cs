using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01.AutorizacionSwift;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.Data.DAL.Swift;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using BCH.Comex.Data.DAL.Cext01;

namespace BCH.Comex.Core.BL.SWAU
{
    public class SwauService
    {
        private UnitOfWorkSwift unitOfWork;
        public SwauService()
        {
            this.unitOfWork = new UnitOfWorkSwift();
        }

        public IList<proc_sw_env_trae_aut_pend_MS_Result> GetPendientesAutorizacion(int idCasilla, int rut)
        {
            return unitOfWork.MensajeRepository.Proc_Sw_Env_Trae_Aut_Pend(idCasilla, rut);
        }

        public IList<proc_sw_env_trae_dev_firma_MS_Result> GetDevueltos(int idCasilla, int rut)
        {
            return unitOfWork.MensajeRepository.Proc_Sw_Env_Trae_Dev_Firma(idCasilla, rut);
        }

        public IList<proc_sw_env_trae_firmas_MS_Result> GetFirmasPendientes(int idCasilla, int rut)
        {
            return unitOfWork.MensajeRepository.Proc_Sw_Env_Trae_Firmas(idCasilla, rut);
        }

        public IList<proc_sw_env_trae_ing_rango_MS_Result> GetIngresados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return unitOfWork.MensajeRepository.Proc_Sw_Env_Trae_Ing_Rango(idCasilla, fechaInicio, fechaFin);
        }

        public IList<proc_sw_env_trae_aup_rango_MS_Result> GetEnAprobacion(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return unitOfWork.MensajeRepository.Proc_Sw_Env_Trae_Aup_Rango(idCasilla, fechaInicio, fechaFin);
        }

        public IList<proc_sw_env_trae_mod_rango_MS_Result> GetModificados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return unitOfWork.MensajeRepository.Proc_Sw_Env_Trae_Mod_Rango(idCasilla, fechaInicio, fechaFin);
        }

        public IList<proc_sw_env_trae_aut_rango_MS_Result> GetAutorizados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return unitOfWork.MensajeRepository.Proc_Sw_Env_Trae_Aut_Rango(idCasilla, fechaInicio, fechaFin);
        }

        public IList<proc_sw_env_trae_pro_rango_MS_Result> GetProcesados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return unitOfWork.MensajeRepository.Proc_Sw_Env_Trae_Pro_Rango(idCasilla, fechaInicio, fechaFin);
        }

        public IList<proc_sw_env_trae_env_rango_MS_Result> GetEnviados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return unitOfWork.MensajeRepository.Proc_Sw_Env_Trae_Env_Rango(idCasilla, fechaInicio, fechaFin);
        }

        public IList<proc_sw_env_trae_rech_rango_MS_Result> GetRechazados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return unitOfWork.MensajeRepository.Proc_Sw_Env_Trae_Rech_Rango(idCasilla, fechaInicio, fechaFin);
        }

        public IList<proc_sw_env_trae_blo_rango_MS_Result> GetBloqueados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return unitOfWork.MensajeRepository.Proc_Sw_Env_Trae_Blo_Rango(idCasilla, fechaInicio, fechaFin);
        }

        public IList<proc_sw_env_trae_nul_rango_MS_Result> GetAnulados(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return unitOfWork.MensajeRepository.Proc_Sw_Env_Trae_Nul_Rango(idCasilla, fechaInicio, fechaFin);
        }

        public int? ConfirmaSiRequiereFirma(int idMensaje, int rut, double monto)
        {
            return unitOfWork.MensajeRepository.Proc_Sw_Env_Cons_Fipe(idMensaje, rut, monto);
        }

        public IList<string> ConstataSim(int idMensaje, int rut)
        {
            return unitOfWork.MensajeRepository.Proc_Sw_Env_Cons_Sim(idMensaje, rut);
        }

        public bool ValidaInyecciones(int idMensaje, ref int cantidadOriginal, ref int cantidadRelacionada)
        {
            using (UnitOfWorkCext01 unitOfWorkCext01 = new UnitOfWorkCext01())
            {
                bool retorno = true;
                var mensaje = unitOfWork.MensajeRepository.Get(idMensaje);
                if (mensaje != null && !string.IsNullOrEmpty(mensaje.referencia) && mensaje.referencia.Length >= 15)
                {
                    var referencia = mensaje.referencia;
                    var cantidad = unitOfWorkCext01.SceRepository.get_cantidad_inyecciones_pendientes(referencia.Substring(0, 3), referencia.Substring(3, 2), referencia.Substring(5, 2), referencia.Substring(7, 3), referencia.Substring(10, 5));
                    cantidadOriginal = (int)cantidad.Cantidad_Original;
                    cantidadRelacionada = (int)cantidad.Cantidad_Relacionada;
                    //si hay cargos por inyectar debemos evitar que firme
                    if (cantidadOriginal + cantidadRelacionada > 0)
                        retorno = false;
                }
                return retorno;
            }
             
        }

        public bool GrabaAutorizacion(int idMensaje, int rut)
        {
            //Obtengo el mensaje para mandarle el monto a confirmaSiRequiereFirma
            var mensaje = unitOfWork.MensajeRepository.Get(idMensaje);
            //valida si falta firmas para el mensaje, de faltar retorna false
            if (this.ConfirmaSiRequiereFirma(idMensaje, rut, mensaje.monto.Value) == 1)
                return false;
            unitOfWork.MensajeRepository.Proc_Sw_Env_Graba_Apr(idMensaje, rut);
            ConstataSim(idMensaje, rut);
            return true;
        }

        public void GrabaDevolver(int idMensaje, int rut, int casilla)
        {
            unitOfWork.MensajeRepository.Proc_Sw_Env_Graba_Dev(idMensaje, rut, casilla, "DEV");
        }

        public void GrabaRechazar(int idMensaje, int rut, int casilla, string comentario)
        {
            unitOfWork.MensajeRepository.Proc_Sw_Env_Graba_Rec(idMensaje, rut, casilla, "Mensaje Rechazado", comentario, "REM");
        }

        #region "Exporta a Excel"
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
                        CargarHojaExcel(datos, doc);
                        doc.SaveAs(stream);
                    }

                    stream.Position = 0; //importante, dejar el stream pronto para leer;

                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, no se ha podido generar archivo excel en Autorización Swift.", ex);
                }
                return stream;
            }
        }

        private static void CargarHojaExcel(IList<ResultItem> acs, SLDocument doc)
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
            styleFechaBold.FormatCode = "dd-mm-yyyy";

            SLStyle styleFecha = doc.CreateStyle();
            styleFecha.FormatCode = "dd-MM-yyyy";

            SLStyle styleDecimalMonedaExtranjera = doc.CreateStyle();
            styleDecimalMonedaExtranjera.FormatCode = "#,##0.00";

            SLStyle styleDecimalPesosChilenos = doc.CreateStyle();
            styleDecimalPesosChilenos.FormatCode = "#,##0";

            SLStyle styleRut = doc.CreateStyle();
            styleRut.FormatCode = "########-#";

            SLStyle styleDecimal = doc.CreateStyle();
            styleDecimal.FormatCode = "0.00####";

            SLStyle styleInt = doc.CreateStyle();
            styleInt.FormatCode = "#";
            #endregion

            doc.MergeWorksheetCells("A1", "AD1");

            doc.SetCellValue("A1", "Autorización Swift");
            doc.SetCellStyle("A1", styleTitulo);

            int colIndex = 1;
            int rowIndex = 3;
            #region titulos
            doc.SetCellValue(rowIndex, colIndex++, "Nº Mensaje");
            doc.SetCellValue(rowIndex, colIndex++, "Tipo");
            doc.SetCellValue(rowIndex, colIndex++, "Unidad");
            doc.SetCellValue(rowIndex, colIndex++, "Mnda");
            doc.SetCellValue(rowIndex, colIndex++, "Monto");
            doc.SetCellValue(rowIndex, colIndex++, "Referencia");
            doc.SetCellValue(rowIndex, colIndex++, "Beneficiario");
            doc.SetCellValue(rowIndex, colIndex++, "Banco Receptor");
            doc.SetCellValue(rowIndex, colIndex++, "Fecha ingreso");
            #endregion

            doc.SetColumnStyle(9, styleFecha);
            doc.SetCellStyle("A3", "AD3", styleEncabezado);

            DataTable dt = SwauService.ConvertToDataTable(acs);

            doc.ImportDataTable(4, 1, dt, false);

            //doc.SetColumnStyle(9, 12, styleFecha);
            //doc.SetColumnStyle(17, 18, styleDecimal);
            //doc.SetColumnStyle(19, 21, styleRut);
            doc.SetColumnStyle(4, styleDecimal);
            //doc.SetColumnStyle(25, styleDecimalMonedaExtranjera);
            //doc.SetColumnStyle(26, 29, styleDecimal);

            doc.AutoFitColumn("A", "AD");
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
    }
}
