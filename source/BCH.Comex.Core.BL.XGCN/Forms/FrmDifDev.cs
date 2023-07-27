using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Devengo;
using BCH.Comex.Data.DAL.Cext01;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BCH.Comex.Core.BL.XGCN.Forms
{
    public static class FrmDifDev
    {
        private static string NombreWorksheet = "DIFERENC";

        public static void Form_Load(DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            int Fin = -1;
            int x = 0;
            int i = 0;
            string tipmov = "";
            bool resp = false;

            Fin = globales.MODDIFDEV.DifDev.Count();

            //((dynamic)GrillaDif).Col = 0;
            //((dynamic)GrillaDif).Row = 0;
            //GrillaDif.Text = " Operación";
            //((dynamic)GrillaDif).Col = 1;
            //GrillaDif.Text = " Mes Devengado";
            //((dynamic)GrillaDif).Col = 2;
            //GrillaDif.Text = " Moneda";
            //((dynamic)GrillaDif).Col = 3;
            //GrillaDif.Text = " Monto Debe";
            //((dynamic)GrillaDif).Col = 4;
            //GrillaDif.Text = " Monto Haber";
            //((dynamic)GrillaDif).Col = 5;
            //GrillaDif.Text = " Diferencia";
            //((dynamic)GrillaDif).Col = 6;
            //GrillaDif.Text = " Tipo Mov.";

            resp = SyGet_DifDev(globales.MODDIFDEV, uow);

            if (!resp)
            {
                globales.ListaMensajesError.Add(new UI_Message
                {
                    Text = "Se ha producido un error al tratar de leer los datos de diferencia de devengamientos.",
                    Type = TipoMensaje.Error,
                    Title = ""
                });
            }

            //Fin = -1;
            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    Fin = MODDIFDEV.DifDev.GetUpperBound(0);
            //});


            //for (i = 1; i <= Fin; i += 1)
            //{
            //    Modgri.Aumenta_Grilla(i, GrillaDif);

            //    ((dynamic)GrillaDif).Row = i;

            //    ((dynamic)GrillaDif).Col = 0;
            //    GrillaDif.Text = MODDIFDEV.DifDev[i].numope;

            //    ((dynamic)GrillaDif).Col = 1;
            //    GrillaDif.Text = MODDIFDEV.DifDev[i].mesdif.ToStr();

            //    ((dynamic)GrillaDif).Col = 2;
            //    GrillaDif.Text = MODDIFDEV.DifDev[i].codmon.ToStr();

            //    ((dynamic)GrillaDif).Col = 3;
            //    GrillaDif.Text = MODGPYF0.forma(MODDIFDEV.DifDev[i].mtodeb, "#,###,###,##0.#0");

            //    ((dynamic)GrillaDif).Col = 4;
            //    GrillaDif.Text = MODGPYF0.forma(MODDIFDEV.DifDev[i].mtohab, "#,###,###,##0.#0");

            //    ((dynamic)GrillaDif).Col = 5;
            //    GrillaDif.Text = MODGPYF0.forma(MODDIFDEV.DifDev[i].mtodif, "#,###,###,##0.#0");


            //    if (MODDIFDEV.DifDev[i].tipmov == 1)
            //    {
            //        tipmov = "Deveng. Intereses";
            //    }
            //    else if (MODDIFDEV.DifDev[i].tipmov == 2)
            //    {
            //        tipmov = "Reverso Intereses";
            //    }
            //    else if (MODDIFDEV.DifDev[i].tipmov == 3)
            //    {
            //        tipmov = "Deveng. Reajustes";
            //    }
            //    else if (MODDIFDEV.DifDev[i].tipmov == 4)
            //    {
            //        tipmov = "Reverso Reajustes";
            //    }

            //    ((dynamic)GrillaDif).Col = 6;
            //    GrillaDif.Text = tipmov;
            //}
        }

        public static bool SyGet_DifDev(T_MODDIFDEV MODDIFDEV, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer("Inicia obtencion diferencia devengo"))
            {
                try
                {
                    object SyGet_DifDev = null;

                    bool SyGetn_DifDev = false;
                    string Que = "";
                    string R = "";
                    int Fin = 0;
                    int k = 0;
                    int n = 0;
                    int i = 0;

                    var result = uow.SceRepository.scedev_difdh_dev_MS();

                    if (result == null)
                        return false;

                    for (i = 0; i <= result.Count(); i++)
                    {
                        T_DifDev add = new T_DifDev()
                        {
                            numope = result[i].numope.ToLng(),
                            mesdif = (int)result[i].mesdif,
                            codmon = (int)result[i].codmon,
                            mtodeb = (double)result[i].mtodeb,
                            mtohab = (double)result[i].mtohab,
                            mtodif = (double)result[i].mtodif,
                            tipmov = (int)result[i].tipmov
                        };

                        if (result[i].tipmov == 1)
                        {
                            add.tipmovNombre = "Deveng. Intereses";
                        }
                        else if (result[i].tipmov == 2)
                        {
                            add.tipmovNombre = "Reverso Intereses";
                        }
                        else if (result[i].tipmov == 3)
                        {
                            add.tipmovNombre = "Deveng. Reajustes";
                        }
                        else if (result[i].tipmov == 4)
                        {
                            add.tipmovNombre = "Reverso Reajustes";
                        }

                        MODDIFDEV.DifDev.Add(add);
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, problemas al obtener la informacion de diferencia de devengo", ex);
                    return false;
                }
            }
        }

        public static MemoryStream getFile(List<T_DifDev> datos)
        {
            using (Tracer tracer = new Tracer("GetFile diferencia devengo"))
            {
                MemoryStream stream = new MemoryStream();
                using (SLDocument doc = new SLDocument())
                {
                    doc.RenameWorksheet(SLDocument.DefaultFirstSheetName, NombreWorksheet);

                    doc.SelectWorksheet(NombreWorksheet);
                    CargarHojaExcelDiferencias(datos, doc);

                    doc.SaveAs(stream);
                }

                stream.Position = 0; //importante, dejar el stream pronto para leer;
                return stream;
            }
        }

        private static void CargarHojaExcelDiferencias(List<T_DifDev> acs, SLDocument doc)
        {
            SLStyle styleTitulo = doc.CreateStyle();
            styleTitulo.Font.Bold = true;
            styleTitulo.Font.FontSize = 15;
            styleTitulo.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleSubtitulo = doc.CreateStyle();
            styleSubtitulo.Font.Bold = true;
            styleSubtitulo.Font.FontSize = 13;
            styleSubtitulo.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleEncabezado = doc.CreateStyle();
            styleEncabezado.Font.Bold = true;
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

            SLStyle styleInt = doc.CreateStyle();
            styleInt.FormatCode = "0";

            doc.MergeWorksheetCells("A1", "G1");

            doc.SetCellValue("A1", "Diferencias devengamiento");
            doc.SetCellStyle("A1", styleTitulo);

            int colIndex = 1;
            int rowIndex = 3;
            doc.SetCellValue(rowIndex, colIndex++, "Operación");
            doc.SetCellValue(rowIndex, colIndex++, "Mes");
            doc.SetCellValue(rowIndex, colIndex++, "Moneda");
            doc.SetCellValue(rowIndex, colIndex++, "Monto Debe");
            doc.SetCellValue(rowIndex, colIndex++, "Monto Haber");
            doc.SetCellValue(rowIndex, colIndex++, "Monto Diferencia");
            doc.SetCellValue(rowIndex, colIndex++, "Tipo Movimiento");
            doc.SetCellStyle("A3", "G3", styleEncabezado);
            doc.SetColumnStyle(1, styleInt);



            foreach (T_DifDev ac in acs)
            {
                colIndex = 1;
                rowIndex++;

                doc.SetCellValue(rowIndex, colIndex++, ac.numope);
                doc.SetCellValueNumeric(rowIndex, colIndex++, ac.mesdif.ToString());
                doc.SetCellValue(rowIndex, colIndex++, ac.codmon);
                doc.SetCellValue(rowIndex, colIndex, ac.mtodeb);
                doc.SetCellStyle(rowIndex, colIndex++, (ac.mtodeb == 1 ? styleDecimalPesosChilenos : styleDecimalMonedaExtranjera));
                doc.SetCellValue(rowIndex, colIndex, ac.mtohab);
                doc.SetCellStyle(rowIndex, colIndex++, (ac.mtohab == 1 ? styleDecimalPesosChilenos : styleDecimalMonedaExtranjera));
                doc.SetCellValue(rowIndex, colIndex, ac.mtodif);
                doc.SetCellStyle(rowIndex, colIndex++, (ac.mtodif == 1 ? styleDecimalPesosChilenos : styleDecimalMonedaExtranjera));
                doc.SetCellValue(rowIndex, colIndex, ac.tipmovNombre);
            }

            doc.AutoFitColumn("A", "G");
        }
    }
}
