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
    public static class FrmCDR
    {

        private const string NombreWorksheet = "OperCDR";

        public static void CmdBuscar_Click(T_CDR cdr, IList<UI_Message> listaMensajes, UnitOfWorkCext01 uow)
        {
            string tipoOperacion = "";
            string indcdr = "";
            string Num_me = "";
            string dig_me = "";
            string rut = "";
            string digcli = "";
            string operacion = "";
            string saldo = "";

            Fn_Buscar_cred(tipoOperacion, indcdr, Num_me, dig_me, rut, digcli, operacion, saldo, cdr, listaMensajes, uow);
        }

        private static bool Fn_Buscar_cred(string Tipope, string indcdr, string Num_me, string dig_me, string rut, string digcli, string operacion, string saldo, T_CDR cdr, IList<UI_Message> listaMensajes, UnitOfWorkCext01 uow)
        {
            bool Fn_Buscar_cred = false;
            using (Tracer tracer = new Tracer("Obteniendo datos de operaciones CDR"))
            {
                try
                {
                    var resultados = uow.SceRepository.sce_vgt_s02_s21(Tipope, indcdr, Num_me, dig_me, rut, digcli, saldo, operacion.Mid(0, 3), operacion.Mid(3, 2), operacion.Mid(5, 2), operacion.Mid(7, 3), operacion.Mid(10, 5));

                    //if (resultados.Count == 0)
                    //{
                    //    listaMensajes.Add(new UI_Message() { Text = "No existen datos para los parametros ingresados.", Type = TipoMensaje.Informacion });
                    //    return true;
                    //}

                    //se limpia la lista de resultados
                    cdr.DvgCredE = new List<T_DvgCred_Excel>();

                    foreach (var result in resultados)
                    {
                        try
                        {
                            T_DvgCred_Excel nuevo = new T_DvgCred_Excel();

                            nuevo.Operacion = result.Operacion.ToLng();
                            nuevo.Numcor = (int)result.numcor;
                            nuevo.Numcuo = (int)result.numcuo;
                            nuevo.moneda = result.moneda;
                            nuevo.Numacc = (int)FormateaNumero(result.numacc);
                            nuevo.Num_me_dig_me = result.num_me + result.dig_me;
                            nuevo.Tipope = (int)FormateaNumero(result.tipope);
                            nuevo.indcdr = (int)FormateaNumero(result.indcdr);
                            nuevo.Feccon = int.Parse(result.feccon) == 0 ? new DateTime() : new DateTime(int.Parse(result.feccon.Substring(0, 4)), int.Parse(result.feccon.Substring(4, 2)), int.Parse(result.feccon.Substring(6, 2)));
                            nuevo.FecVen = int.Parse(result.fecven) == 0 ? new DateTime() : new DateTime(int.Parse(result.fecven.Substring(0, 4)), int.Parse(result.fecven.Substring(4, 2)), int.Parse(result.fecven.Substring(6, 2)));
                            nuevo.FecInt = int.Parse(result.fecint) == 0 ? new DateTime() : new DateTime(int.Parse(result.fecint.Substring(0, 4)), int.Parse(result.fecint.Substring(4, 2)), int.Parse(result.fecint.Substring(6, 2)));
                            nuevo.FecOri = int.Parse(result.fecori) == 0 ? new DateTime() : new DateTime(int.Parse(result.fecori.Substring(0, 4)), int.Parse(result.fecori.Substring(4, 2)), int.Parse(result.fecori.Substring(6, 2)));
                            nuevo.Val_mo = FormateaNumero(result.val_mo, 100);
                            nuevo.NomCli = result.nomcli;
                            nuevo.Tiptas = result.tiptas;
                            nuevo.Tasbas = FormateaNumero(result.tasbas, 1000000);
                            nuevo.Spread = FormateaNumero(result.spread, 1000000);
                            nuevo.Tastot = FormateaNumero(result.tastot, 1000000);
                            nuevo.numcli_digcli = String.Format("{0:0##,###,###-}", int.Parse(result.numcli)) + result.digcli;
                            nuevo.Numava1_Digava1 = String.Format("{0:0##,###,###-}", int.Parse(result.numava1)) + result.digava1;
                            nuevo.Numava2_Digava2 = String.Format("{0:0##,###,###-}", int.Parse(result.numava2)) + result.digava2;
                            nuevo.TipCam = FormateaNumero(result.tipcam, 10000);
                            nuevo.Diadev = int.Parse(result.diadev);
                            nuevo.Moneda_int = result.moneda_int;
                            nuevo.Valori_cre_mo = FormateaNumero(result.valori_cre_mo, 100);
                            //if (result.int_al_ven_mo.InStr("-", 1, StringComparison.CurrentCulture) == 0)
                            //{
                            //    nuevo.Int_al_ven_mo = result.int_al_ven_mo.ToDbl();
                            //}
                            //else
                            //{
                            //    nuevo.Int_al_ven_mo = Utils.Format.StringToDouble((result.int_al_ven_mo.Mid(result.int_al_ven_mo.InStr("-", 1, StringComparison.CurrentCulture), result.int_al_ven_mo.Len())), 100);
                            //}
                            nuevo.Int_al_ven_mo = FormateaNumero(result.int_al_ven_mo,100);
                            nuevo.Dev_normal_mo = FormateaNumero(result.dev_normal_mo, 100);
                            nuevo.Real_normal = FormateaNumero(result.rea_normal, 100);
                            nuevo.Tc_origen = FormateaNumero(result.tc_origen, 10000);
                            nuevo.Fogape = result.instfom;

                            cdr.DvgCredE.Add(nuevo);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
               
                    }

                    Fn_Buscar_cred = true;

                    return Fn_Buscar_cred;

                }
                catch (Exception exc)
                {
                    tracer.TraceException("Alerta, problemas al obtener los datos de las operaciones CDR.", exc);
                    listaMensajes.Add(new UI_Message() { Title = "Problemas al obtener los datos de las operaciones CDR.", Type = TipoMensaje.Error });
                }
            }
            return Fn_Buscar_cred;
        }

        public static MemoryStream getFrmCDRFile(IList<T_DvgCred_Excel> datos, IList<UI_Message> listaMensajes)
        {
            using (Tracer tracer = new Tracer("getFrmCDRFile"))
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
                    tracer.TraceException("Alera, no se ha podido generar archivo excel de operaciones CDR", ex);
                    listaMensajes.Add(new UI_Message() { Text = "No se ha podido generar archivo excel de operaciones CDR", Type = TipoMensaje.Error });
                }
                return stream;
            }
        }

        private static void CargarHojaExcel(IList<T_DvgCred_Excel> acs, SLDocument doc)
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

            SLStyle styleTasaInt = doc.CreateStyle();
            styleTasaInt.FormatCode = "#0.000000";

            #endregion



            doc.MergeWorksheetCells("A1", "AD1");

            doc.SetCellValue("A1", "Operaciones CDR");
            doc.SetCellStyle("A1", styleTitulo);

            int colIndex = 1;
            int rowIndex = 3;
            #region titulos
            doc.SetCellValue(rowIndex, colIndex++, "Operación");
            doc.SetCellValue(rowIndex, colIndex++, "C.Pago");
            doc.SetCellValue(rowIndex, colIndex++, "Cuota");
            doc.SetCellValue(rowIndex, colIndex++, "Moneda");
            doc.SetCellValue(rowIndex, colIndex++, "Correlativo");
            doc.SetCellValue(rowIndex, colIndex++, "Cuenta");
            doc.SetCellValue(rowIndex, colIndex++, "TO");
            doc.SetCellValue(rowIndex, colIndex++, "Plazo");
            doc.SetCellValue(rowIndex, colIndex++, "Fec Contable");
            doc.SetCellValue(rowIndex, colIndex++, "Vencimiento");
            doc.SetCellValue(rowIndex, colIndex++, "Inicio Interes");
            doc.SetCellValue(rowIndex, colIndex++, "Fec.Origen");
            doc.SetCellValue(rowIndex, colIndex++, "Saldo");
            doc.SetCellValue(rowIndex, colIndex++, "Nombre Cliente");
            doc.SetCellValue(rowIndex, colIndex++, "Tipo Tasa");
            doc.SetCellValue(rowIndex, colIndex++, "Base");
            doc.SetCellValue(rowIndex, colIndex++, "Spread");
            doc.SetCellValue(rowIndex, colIndex++, "Tasa Total");
            doc.SetCellValue(rowIndex, colIndex++, "Rut Cliente");
            doc.SetCellValue(rowIndex, colIndex++, "Rut Aval1");
            doc.SetCellValue(rowIndex, colIndex++, "Rut Aval2");
            doc.SetCellValue(rowIndex, colIndex++, "Tipo Cambio");
            doc.SetCellValue(rowIndex, colIndex++, "Dias Devengados");
            doc.SetCellValue(rowIndex, colIndex++, "Moneda Interes");
            doc.SetCellValue(rowIndex, colIndex++, "Monto Original");
            doc.SetCellValue(rowIndex, colIndex++, "Interes al Vcto");
            doc.SetCellValue(rowIndex, colIndex++, "Interes Devengado");
            doc.SetCellValue(rowIndex, colIndex++, "Reajuste Devengado");
            doc.SetCellValue(rowIndex, colIndex++, "Tipo Cambio Orig");
            doc.SetCellValue(rowIndex, colIndex++, "Instr. de Fomento");
            #endregion

            doc.SetCellStyle("A3", "AD3", styleEncabezado);
            doc.SetColumnStyle(1, styleInt);
            doc.SetColumnStyle(6, styleInt);
            doc.SetColumnStyle(9, 12, styleFecha);
            doc.SetColumnStyle(13, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(16, 18, styleTasaInt);
            doc.SetColumnStyle(19, 21, styleRut);
            doc.SetColumnStyle(22, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(25, 26, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(28, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(29, styleDecimal);
            doc.SetColumnStyle(25, 29, styleDecimalMonedaExtranjera);

            DataTable dt = Modulos.Modulo1.ConvertToDataTable(acs);

            doc.ImportDataTable(4, 1, dt, false);
            doc.AutoFitColumn("A", "AD");
        }

        private static double FormateaNumero(string numero, double divisor = 1)
        {
            //if (numero.IndexOf('-') == -1)
            //{
            //    return double.Parse(numero) / divisor;
            //}
            //else
            //{
            //    return double.Parse();
            //}
            double numeric = 0.0;
            if (double.TryParse(numero.TrimStart('0'), out numeric))
            {
                return numeric / divisor;
            }
            else
            {
                return numeric;
            }

            
        }
    }
}
