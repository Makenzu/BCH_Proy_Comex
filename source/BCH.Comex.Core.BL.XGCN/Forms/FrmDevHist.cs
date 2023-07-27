using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Devengo;
using BCH.Comex.Data.DAL.Cext01;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace BCH.Comex.Core.BL.XGCN.Forms
{
    public static class FrmDevHist
    {
        private const string NombreWorksheet = "Devengamiento";

        public static void Form_Load(T_DevHist devHist, IList<UI_Message> listaMensajes, UnitOfWorkCext01 uow)
        {
            Llenacombo(devHist, listaMensajes, uow);
        }

        private static void Llenacombo(T_DevHist devHist, IList<UI_Message> listaMensajes, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer("Obtencion listado periodos dev historico"))
            {
                try
                {
                    var Periodos = uow.SceRepository.pro_sce_cdev_s02();
                    devHist.periodos = new List<decimal>();

                    if (Periodos.Count == 0)
                    {
                        listaMensajes.Add(new UI_Message() { Text = "No existen PERIODOS", Type = TipoMensaje.Error });
                    }

                    foreach (var periodo in Periodos)
                    {
                        devHist.periodos.Add(periodo ?? 0);
                    }
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, problemas al obtener los periodos en consulta devengo historica", ex);
                    listaMensajes.Add(new UI_Message() { Text = "Alerta, problemas al obtener los periodos en consulta devengo historicos", Type = TipoMensaje.Error });
                }
            }

        }

        public static bool CmdBuscar_Click(T_DevHist devHist, IList<UI_Message> listaMensajes, UnitOfWorkCext01 uow)
        {
            if (devHist.periodoSeleccionado == 0)
            {
                listaMensajes.Add(new UI_Message() { Title = "Para consultar debe ingresar periodo.", Type = TipoMensaje.Informacion, ControlName = "listaPeriodos_SelectedValue", AutoClose = true });
                return false;
            }
            string rut = "";
            string operacion = "";
            int moneda = 9; //  significa que trae los registros con moneda nacional e internacional

            return Fn_Buscar_Deveng(devHist.periodoSeleccionado, rut, operacion, moneda, devHist, listaMensajes, uow);

        }

        private static bool Fn_Buscar_Deveng(int periodo, string rut, string operacion, int moneda, T_DevHist devHist, IList<UI_Message> listaMensajes, UnitOfWorkCext01 uow)
        {
            bool Fn_Buscar_Deveng = false;

            using (Tracer tracer = new Tracer("consulta devengo historica"))
            {

                tracer.AddToContext("periodo", periodo);

                string todos = "S";

                try
                {
                    devHist.Deveng.Clear();
                    var resultados = uow.SceRepository.sce_cdev_s01(periodo, rut, operacion, moneda, todos);

                    if (resultados.Count == 0)
                    {
                        listaMensajes.Add(new UI_Message() { Text = "No existe Información para los datos ingresados .", Type = TipoMensaje.Informacion });
                    }

                    foreach (var resultado in resultados)
                    {
                        T_Deveng nuevo = new T_Deveng()
                                                {
                                                    Operacion = resultado.operacion.ToLng(),
                                                    NumNeg = (int)resultado.numneg,
                                                    moneda = (int)resultado.moneda,
                                                    Mtovigente = (double)resultado.mtovig,
                                                    Tasa = (double)resultado.tasbas,
                                                    Inicio = resultado.fecini,
                                                    Fin = resultado.fecfin,
                                                    Dias = resultado.numdia,
                                                    TipCambio = (double)resultado.tipcam,
                                                    MtoInter = (double)resultado.mtointer,
                                                    rut = resultado.rut,
                                                    CtaIng = resultado.cta_intganado.Trim().ToLng(),
                                                    CtaIngXCob = resultado.cta_intxcob.Trim().ToLng(),
                                                    MtoNac = (double)resultado.mnd,
                                                    periodo = (double)resultado.periodo
                                                };
                        devHist.Deveng.Add(nuevo);
                    }

                    Fn_Buscar_Deveng = true;
                    return Fn_Buscar_Deveng;

                }
                catch (Exception exc)
                {
                    tracer.TraceException("Alerta, problemas al obtener la informacion de devengos historicos", exc);
                    listaMensajes.Add(new UI_Message() { Text = "Alerta, problemas al obtener la información de devengos históricos", Type = TipoMensaje.Error });
                }
            }
            return Fn_Buscar_Deveng;
        }

        public static MemoryStream getFileDevengoHistoricos(IList<T_Deveng> datos)
        {
            using (Tracer tracer = new Tracer("GetFileDevengos Historicos"))
            {
                MemoryStream stream = new MemoryStream();
                try
                {

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
                    tracer.TraceException("Alera, no se ha podido generar archivo excel de consulta devengamiento historico", ex);
                }
                return stream;
            }
        }

        private static void CargarHojaExcel(IList<T_Deveng> acs, SLDocument doc)
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
            styleDecimalMonedaExtranjera.FormatCode = "#,###,###,##0.00";

            SLStyle styleDecimalPesosChilenos = doc.CreateStyle();
            styleDecimalPesosChilenos.FormatCode = "#,###,###,##0";

            SLStyle styleRut = doc.CreateStyle();
            styleRut.FormatCode = "########-#";

            SLStyle styleDecimal = doc.CreateStyle();
            styleDecimal.FormatCode = "#0.00";

            SLStyle styleInt = doc.CreateStyle();
            styleInt.FormatCode = "0";

            SLStyle styleTasaInt = doc.CreateStyle();
            styleTasaInt.FormatCode = "#0.000000";
            #endregion

            doc.MergeWorksheetCells("A1", "O1");

            doc.SetCellValue("A1", "Devengamientos");
            doc.SetCellStyle("A1", styleTitulo);

            int colIndex = 1;
            int rowIndex = 3;
            #region titulos
            doc.SetCellValue(rowIndex, colIndex++, "Operación");
            doc.SetCellValue(rowIndex, colIndex++, "Numneg");
            doc.SetCellValue(rowIndex, colIndex++, "Moneda");
            doc.SetCellValue(rowIndex, colIndex++, "Monto");
            doc.SetCellValue(rowIndex, colIndex++, "Tasa");
            doc.SetCellValue(rowIndex, colIndex++, "Inicio");
            doc.SetCellValue(rowIndex, colIndex++, "Fin");
            doc.SetCellValue(rowIndex, colIndex++, "Dias");
            doc.SetCellValue(rowIndex, colIndex++, "Tipcambio");
            doc.SetCellValue(rowIndex, colIndex++, "Mto.Inter");
            doc.SetCellValue(rowIndex, colIndex++, "Rut");
            doc.SetCellValue(rowIndex, colIndex++, "Cta.Ing");
            doc.SetCellValue(rowIndex, colIndex++, "Cta IngxCOB");
            doc.SetCellValue(rowIndex, colIndex++, "Mto.Nac");
            doc.SetCellValue(rowIndex, colIndex++, "Periodo");
            #endregion

            doc.SetCellStyle("A3", "O3", styleEncabezado);

            doc.SetColumnStyle(1, styleInt);
            doc.SetColumnStyle(4, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(5, styleTasaInt);
            doc.SetColumnStyle(6, 7, styleFecha);
            doc.SetColumnStyle(9, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(10, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(11, styleRut);
            doc.SetColumnStyle(14, styleDecimalPesosChilenos);
            

            DataTable dt = Modulos.Modulo1.ConvertToDataTable(acs);
            doc.ImportDataTable(4, 1, dt, false);
            doc.AutoFitColumn("A", "O");
        }

    }
}
