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
using System.Linq;

namespace BCH.Comex.Core.BL.XGCN.Forms
{
    public static class FrmCtaCte
    {
        public const string NombreWorksheet = "CTACTE";

        public static bool CmdBuscar_Click(T_CTACTE ctacte, IList<UI_Message> listaMensajes, UnitOfWorkCext01 uow)
        {
            bool CmdBuscar = false;

            if (ctacte.fechaDesde.Trim() == string.Empty || ctacte.fechaHasta.Trim() == string.Empty)
            {
                listaMensajes.Add(new UI_Message() { Text = "Debe ingresar rango de fechas para hacer las consultas", Type = TipoMensaje.Informacion, ControlName = "txtFechaDesde_TexttxtFechaDesde_Text" });
                return CmdBuscar;
            }

            if (ctacte.optSeleccionada == tipoFiltro.NemCta && (ctacte.txtNemCta.Trim() == string.Empty))
            {
                listaMensajes.Add(new UI_Message() { Text = "Debe ingresar Nemonico de cuenta", Type = TipoMensaje.Informacion, ControlName = "txtNemcta_Text" });
                return CmdBuscar;
            }

            if (ctacte.optSeleccionada == tipoFiltro.NumCta && (ctacte.txtNumCta.Trim() == string.Empty))
            {
                listaMensajes.Add(new UI_Message() { Text = "Debe ingresar Numero de cuenta", Type = TipoMensaje.Informacion, ControlName = "txtNumcta_Text" });
                return CmdBuscar;
            }

           
            if (ctacte.txtCuentaCorriente.Trim() != string.Empty && ( ctacte.txtNemCta.Trim() != "CC$" && ctacte.txtNemCta.Trim() != "CCE" ))
            {
                listaMensajes.Add(new UI_Message() { Text = "Nemónico no corresponde a Cuenta corriente", Type = TipoMensaje.Informacion, ControlName = "txtNemcta_Text" });
                return CmdBuscar;
            }

            if (Fn_Buscar_cta(ctacte,listaMensajes,uow))
            {
                CmdBuscar = true;
            }

            return CmdBuscar;
        }

        private static bool Fn_Buscar_cta(T_CTACTE ctacte, IList<UI_Message> listaMensajes, UnitOfWorkCext01 uow)
        {
            bool Fn_Buscar_cta = false;

            using (Tracer tracer = new Tracer("Consulta cuentas contables"))
            {
                string todos = "S";

                try
                {
                    if (ctacte.optSeleccionada != tipoFiltro.Todos)
                    {
                        todos = "N";
                    }

                    var resultados = uow.SceRepository.sce_mcdh_s01(ctacte.txtNemCta, ctacte.txtNumCta, ctacte.txtCuentaCorriente, ctacte.txtCentroCosto, DateTime.Parse(ctacte.fechaDesde), DateTime.Parse(ctacte.fechaHasta), todos);

                    //remuevo posibles caracteres de escape \0
                    resultados.ForEach(r => r.numcta = r.numcta.Replace("\0", ""));
                    ctacte.DvgCta = new List<T_DvgCta>();
                    foreach (var resultado in resultados)
                    {
                        T_DvgCta nuevo = new T_DvgCta();
                        nuevo.Operacion = resultado.operacion.ToLng();
                        nuevo.Codneg = (int)resultado.codneg;
                        nuevo.NroRpt = (int)resultado.nrorpt;
                        nuevo.FecMov = resultado.fecmov;
                        nuevo.Cencos = (string.IsNullOrWhiteSpace(resultado.cencos) ? 0 : int.Parse(resultado.cencos));
                        nuevo.codusr = resultado.codusr;
                        nuevo.NroImp = (int)resultado.nroimp;
                        nuevo.nemcta = resultado.nemcta;
                        nuevo.numcta = resultado.numcta;
                        nuevo.nemmon = resultado.nemmon;
                        nuevo.mtomcd = (double)resultado.mtomcd;
                        nuevo.cod_dh = resultado.cod_dh;
                        nuevo.rutcli = resultado.rutcli.Trim().Length > 0 && Microsoft.VisualBasic.Information.IsNumeric(resultado.rutcli.Left(resultado.rutcli.Length - 1)) ? (String.Format("{0:0##,###,###-}", int.Parse(resultado.rutcli.Left(resultado.rutcli.Length - 1))) + resultado.rutcli.Right(1)) : string.Empty;
                        nuevo.numcct = (string.IsNullOrWhiteSpace(resultado.numcct) ? 0 : resultado.numcct.Trim().ToLng());
                        nuevo.Mtotas = (double)resultado.mtotas;
                        nuevo.OfiDes = (int)resultado.ofides;
                        nuevo.NumPar = (double)resultado.numpar;
                        nuevo.tipmov = (int)resultado.tipmov;
                        ctacte.DvgCta.Add(nuevo);
                    }
                    Fn_Buscar_cta = true;

                    return Fn_Buscar_cta;
                }
                catch (Exception exc)
                {
                    tracer.TraceException("Alerta, problemas al obtener la información de la consulta de cuentas contables", exc);
                    listaMensajes.Add(new UI_Message() { Text = "problemas al obtener la información de la consulta de cuentas contables", Type = TipoMensaje.Error });
                }
            }
            return Fn_Buscar_cta;
        }

        public static MemoryStream getFrmCtaCteFile(IList<T_DvgCta> datos, IList<UI_Message> listaMensajes)
        {
            int countRows = datos.Count;
            using (Tracer tracer = new Tracer("getFrmCtaCteFile"))
            {
                MemoryStream stream = new MemoryStream();
                    try
                    {
                        using (SLDocument doc = new SLDocument())
                        {
                            //SLDocument doc = new SLDocument();
                            doc.RenameWorksheet(SLDocument.DefaultFirstSheetName, NombreWorksheet);

                            doc.SelectWorksheet(NombreWorksheet);
                            CargarHojaExcel(datos, doc);
                            doc.SaveAs(stream);
                        }

                        stream.Position = 0; //importante, dejar el stream pronto para leer;
                    }
                    catch (Exception ex)
                    {
                        var mensajeError = string.Format("No se ha podido generar archivo excel de cuentas corrientes, la consulta devuelve {0} registros", countRows);
                        tracer.TraceException( "Alerta: " + mensajeError, ex);
                        listaMensajes.Add(new UI_Message() { Text = mensajeError, Type = TipoMensaje.Error });
                    }
                    return stream;
            }
        }

        private static void CargarHojaExcel(IList<T_DvgCta> acs, SLDocument doc)
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

            doc.MergeWorksheetCells("A1", "R1");
            doc.SetCellValue("A1", "Cuentas Contables");
            doc.SetCellStyle("A1", styleTitulo);

            int colIndex = 1;
            int rowIndex = 3;
            #region titulos
            doc.SetCellValue(rowIndex, colIndex++, "Operación");
            doc.SetCellValue(rowIndex, colIndex++, "Negociación");
            doc.SetCellValue(rowIndex, colIndex++, "Nro.reporte");
            doc.SetCellValue(rowIndex, colIndex++, "Fecha");
            doc.SetCellValue(rowIndex, colIndex++, "Centro costo");
            doc.SetCellValue(rowIndex, colIndex++, "Usuario");
            doc.SetCellValue(rowIndex, colIndex++, "Nroimp");
            doc.SetCellValue(rowIndex, colIndex++, "Nemonico Cta");
            doc.SetCellValue(rowIndex, colIndex++, "Nro.Cuenta");
            doc.SetCellValue(rowIndex, colIndex++, "Moneda");
            doc.SetCellValue(rowIndex, colIndex++, "Monto");
            doc.SetCellValue(rowIndex, colIndex++, "D-H");
            doc.SetCellValue(rowIndex, colIndex++, "Rut Cliente");
            doc.SetCellValue(rowIndex, colIndex++, "Cta.Cte");
            doc.SetCellValue(rowIndex, colIndex++, "Tasa");
            doc.SetCellValue(rowIndex, colIndex++, "Destino");
            doc.SetCellValue(rowIndex, colIndex++, "Numpar");
            doc.SetCellValue(rowIndex, colIndex++, "Tipo Movimiento");
            #endregion

            doc.SetCellStyle("A3", "R3", styleEncabezado);

            doc.SetColumnStyle(1, styleInt);
            doc.SetColumnStyle(4, styleFecha);
            doc.SetColumnStyle(9, styleInt);
            doc.SetColumnStyle(11, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(13, styleRut);
            doc.SetColumnStyle(14, styleInt);
            doc.SetColumnStyle(15, styleTasaInt);
            
            DataTable dt = Modulos.Modulo1.ConvertToDataTable(acs);
            doc.ImportDataTable(4, 1, dt, false);
            //doc.AutoFitColumn("A", "R");
            doc.AutoFitColumn(1, 18);
        }
    }
}
