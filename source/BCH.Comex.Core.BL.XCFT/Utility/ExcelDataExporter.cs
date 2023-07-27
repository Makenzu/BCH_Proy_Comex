using BCH.Comex.Common.Utility;
using BCH.Comex.Core.Entities.Cext01;
using SpreadsheetLight;
using System.Collections.Generic;
using System.Data;
using System.IO;
using DocumentFormat.OpenXml.Spreadsheet;

namespace BCH.Comex.Core.BL.XCFT.Utility
{
    public class ExcelDataExporter
    {
        /// <summary>
        /// Agrega o crea el primer worksheet del excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datos">Lista de datos a agregar al worksheet</param>
        /// <param name="titulo">Titulo del worksheet</param>
        /// <param name="document">Documento excel</param>
        /// <param name="mensajeError">En caso de que haya fallado alguna consulta a datos, se envia un mensaje para mostrar</param>
        /// <param name="isFirstWorksheet">Si es primer worksheet se sobrescribe titulo por default</param>
        public static void GenerateWorksheet<T>(List<T> datos, string titulo, SLDocument document, string mensajeError, bool isFirstWorksheet = false)
        {
            SLStyle styleTitulo = document.CreateStyle();
            styleTitulo.Font.Bold = true;
            styleTitulo.Font.FontSize = 15;
            styleTitulo.Font.FontColor = System.Drawing.Color.Black;
            styleTitulo.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleEncabezado = document.CreateStyle();
            styleEncabezado.Font.Bold = true;
            styleEncabezado.Font.FontColor = System.Drawing.Color.Black;
            styleEncabezado.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            if (isFirstWorksheet)
            {
                document.RenameWorksheet(SLDocument.DefaultFirstSheetName, titulo);
            }
            else
            {
                document.AddWorksheet(titulo);
            }

            document.MergeWorksheetCells("A1", "B1");
            document.SetCellValue("A1", titulo);
            document.SetCellStyle("A1", styleTitulo);

            if(datos == null)
            {
                document.SetCellValue(3, 1, "Mensaje");
                document.SetCellValue(4, 1, mensajeError);
            }
            else
            {
                DataTable datosWorksheet = ExcelHelper.ConvertToDataTable(datos);
                GenerateColumnTitles(3, 1, datosWorksheet, document);

                document.ImportDataTable(4, 1, datosWorksheet, false);
            }

            document.SetCellStyle("A3", "BM3", styleEncabezado);
            document.AutoFitColumn("A", "BM");
            document.SetColumnWidth(1, 20); 
        }

        /// <summary>
        /// Genera las columnas del excel
        /// </summary>
        /// <param name="rowIndex">Fila donde se ubicarán los titulos dentro del worksheet</param>
        /// <param name="colIndex">Colunma donde comienzan los titulos</param>
        /// <param name="dataTable">Tabla de los datos a mapear</param>
        /// <param name="document">Documento excel</param>
        public static void GenerateColumnTitles(int rowIndex, int colIndex, DataTable dataTable, SLDocument document)
        {
            var columns = dataTable.Columns;

            for (int i = 0; i < columns.Count; i++)
            {
                document.SetCellValue(rowIndex, colIndex++, columns[i].ColumnName);
            }
        }


        public static MemoryStream GenerarArchivoReporteOperacion(
            List<sce_xdoc_s05_MS_Result> cartas,
            List<sce_mch_s16_MS_Result> cabeceraContabilidad,
            List<sce_mcd_s78_MS_Result> detalleContabilidad,
            List<sce_plan_s19_MS_Result> planillasImportacion,
            List<sce_pli_s08_MS_Result> planillasInvisibles,
            List<sce_xanu_s04_MS_Result> planillasAnulacion,
            List<sce_xplv_s12_MS_Result> planillasExportacion,
            List<sce_prty_s10_MS_Result> datosParticipante,
            List<sce_rsa_s07_MS_Result> razonSocialParticipante,
            List<sce_dad_s08_MS_Result> direccionParticipante,
            List<tbl_datos_usuario_s01_MS_Result> datosUsuario)
        {
            MemoryStream stream = new MemoryStream();
            using (SLDocument doc = new SLDocument())
            {
                GenerateWorksheet(cartas, "Cartas", doc, "Ocurrio un problema al tratar de leer las cartas", true);
                GenerateWorksheet(cabeceraContabilidad, "Cabecera Contabilidad", doc, "Ocurrio un problema al tratar de leer la cabecera de la contabilidad");
                GenerateWorksheet(detalleContabilidad, "Detalle Contabilidad", doc, "Ocurrio un problema al tratar de leer el detalle de la contabilidad");
                GenerateWorksheet(datosParticipante, "Datos Participante", doc, "Ocurrio un problema al tratar de leer los Datos del Participante");
                GenerateWorksheet(razonSocialParticipante, "Razon Social Participante", doc, "Ocurrio un problema al tratar de leer la Razón Social del Participante");
                GenerateWorksheet(direccionParticipante, "Direccion Participante", doc, "Ocurrio un problema al tratar de leer la Direccion del Participante");
                GenerateWorksheet(planillasImportacion, "Planillas Importacion", doc, "Ocurrio un problema al tratar de leer las Planillas de Importacion");
                GenerateWorksheet(planillasInvisibles, "Planillas Invisibles", doc, "Ocurrio un problema al tratar de leer las Planillas Invisibles");
                GenerateWorksheet(planillasAnulacion, "Planillas Anulacion", doc, "Ocurrio un problema al tratar de leer las Planillas de Anulacion");
                GenerateWorksheet(planillasExportacion, "Planillas Exportacion", doc, "Ocurrio un problema al tratar de leer las Planillas de Exportacion");
                GenerateWorksheet(datosUsuario, "Datos Usuario", doc, "Ocurrio un problema al tratar de leer los Datos del Usuario");

                doc.SaveAs(stream);
            }

            stream.Position = 0; //importante, dejar el stream pronto para leer;

            return stream;
        }
    }
}
