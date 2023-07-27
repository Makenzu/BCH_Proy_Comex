using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
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
    public static class FrmDevengIntReaj
    {
        private static string NombreWorksheetIntereces = "INTERESES";
        private static string NombreWorksheetReajustes = "REAJUSTES";

        public static void Form_Load(DevengIntReaj datos, List<UI_Message> listaErrores, UnitOfWorkCext01 uow)
        {
            LlenaCombos(datos, listaErrores, uow);
        }

        private static void LlenaCombos(DevengIntReaj datos, List<UI_Message> listaErrores, UnitOfWorkCext01 uow)
        {
            string Que = "";
            string R = "";
            int nro_reg = 0;
            int I = 0;
            object MsgGst = null;
            int cont = 0;

            ObtienePeriodosDev(datos, listaErrores, uow);
        }

        public static void ObtienePeriodosDev(DevengIntReaj datos, List<UI_Message> listaErrores, UnitOfWorkCext01 uow)
        {
            int iNewIndex = 0;
            string Que = "";
            string R = "";
            int nro_reg = 0;
            int I = 0;
            string MsgGst = "";
            int cont = 0;
            List<decimal> listaPeriodos = new List<decimal>();

            listaPeriodos = uow.SceRepository.pro_sce_datos_cuadratura_s01_MS(datos.TipoConsultaDev).ToList();

            //Se limpia la variable de periodos, ya que ambas consultas usan la misma.
            datos.Cmbperiodo = new List<string>();

            if (listaPeriodos.Count() == 0)  //R == "")
            {
                //MigrationSupport.Utils.MsgBox("No existen PERIODOS .", MODGPYF0.pito(48).Cast<MigrationSupport.MsgBoxStyle>(), MsgGst);
                listaErrores.Add(new UI_Message() { Text = "No existen PERIODOS ." });
                return;
            }

            //nro_reg = MODGSRM.RowCount;
            if (listaPeriodos.Count() > 0)
            {
                datos.Cmbperiodo = listaPeriodos.Select(x => x.ToString()).ToList();
            }
        }
        //public void ObtieneMonedasDev(System.Windows.Forms.ComboBox cmb, int TipoConsultaDev)
        //{
        //    int iNewIndex = 0;
        //    string Que = "";
        //    string R = "";
        //    int nro_reg = 0;
        //    string MsgGst = "";
        //    int I = 0;
        //    int cont = 0;

        //    if (TipoConsultaDev == 1)
        //    {
        //        Que = "SELECT DISTINCT t_monpro FROM sce_datos_cuadratura ORDER BY 1 ";
        //    }
        //    else
        //    {
        //        Que = "SELECT DISTINCT t_monpro FROM sce_datos_cuadratura_r ORDER BY 1 ";
        //    }

        //    MODGSRM.ParamSrm8k.APartirDe = 0;
        //    R = Módulo1.respuesta(Que);

        //    if (R == "-1")
        //    {
        //        System.Windows.Forms.MessageBox.Show("Error al leer Monedas. El servidor reporta : [" + MODGSRM.ParamSrm8k.mensaje.Left(80).TrimB() + "]. Reporte este problema.", MsgGst, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    if (R == "")
        //    {
        //        MigrationSupport.Utils.MsgBox("No existen Monedas.", MODGPYF0.pito(48).Cast<MigrationSupport.MsgBoxStyle>(), MsgGst);
        //        return;
        //    }

        //    nro_reg = MODGSRM.RowCount;

        //    iNewIndex = cmb.Items.Add("Todas");
        //    cmb.SetItemData(iNewIndex, 0);

        //    if (nro_reg > 0)
        //    {
        //        for (I = 0; I <= nro_reg - 1; I += 1)
        //        {
        //            iNewIndex = cmb.Items.Add(MigrationSupport.Utils.Format(MODGSYB.GetPosSy(MODGSYB.NumIni(), "N", R), "000"));
        //            cmb.SetItemData(iNewIndex, I);

        //            R = MODGSRM.NuevaRespuesta(1, R);
        //        }
        //    }

        //    cmb.SelectedIndex = 0;
        //}

        public static bool CmdBuscar_Click(DevengIntReaj datos, List<UI_Message> listaErrores, UnitOfWorkCext01 uow)
        {
            double periodo = 0.0;
            System.Windows.Forms.DialogResult respuesta = 0;
            //float moneda = 0.0F;
            double Pausa = 0.0;
            double Start = 0.0;


            if (datos.CmbperiodoSelected == 0)
            {
                listaErrores.Add(new UI_Message() { Text = "Para consultar debe ingresar periodo.", Type = TipoMensaje.Informacion, ControlName = "listaPeriodos_SelectedValue", AutoClose = true });
                //respuesta = MigrationSupport.Utils.MsgBox("Para consultar debe ingresar periodo.-", MigrationSupport.MsgBoxStyle.Information, "Información ");
                //Cmbperiodo.Focus();
                return false;
            }

            //if (txtCantidadRegistros.Text.Len() < 1)
            //{
            //    System.Windows.Forms.MessageBox.Show("Ingrese número de registros a bajar.-", "Información ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtCantidadRegistros.Focus();
            //    return;
            //}

            //if (txtCantidadRegistros.Text == "0")
            //{
            //    System.Windows.Forms.MessageBox.Show("Valor no permitido.- Ingrese número de registros a bajar.", "Información ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtCantidadRegistros.Focus();
            //    return;
            //}

            periodo = double.Parse(datos.CmbperiodoSelected.ToString());

            //if (Optrut.Checked)
            //{
            //    if (((object)((dynamic)Rut).ClipText()).ToStr() == "")
            //    {
            //        respuesta = MigrationSupport.Utils.MsgBox(" Debe ingresar rut para consultar ", MigrationSupport.MsgBoxStyle.Information, "Información ");
            //        SSCmdparar.Visible = false;
            //        return;
            //    }
            //}
            //else
            //{
            //    if (Optoperacion.Checked)
            //    {
            //        if (TxtOperacion.Text == "")
            //        {
            //            respuesta = MigrationSupport.Utils.MsgBox(" Debe ingresar operacion para consultar ", MigrationSupport.MsgBoxStyle.Information, "Información");
            //            SSCmdparar.Visible = false;
            //            return;
            //        }
            //    }
            //}

            //Módulo1.parar = false;
            //Lbltitulo.Text = "";
            //SSCmdparar.Visible = true;
            //Pausa = 0.5;
            //Start = MigrationSupport.Utils.Timer;

            //while (MigrationSupport.Utils.Timer < Start + Pausa)
            //{
            //    System.Windows.Forms.Application.DoEvents();
            //}

            //MODGSRM.ParamSrm8k.APartirDe = 1;

            //Modgri.Limpia_Grilla(Grilla);



            //if (!Optrut.Checked)
            //{
            //    strRUT = "";
            //}
            //else
            //{
            //    strRUT = (((object)((dynamic)Rut).ClipText()).ToStr().Mid(1, 9)).ToStr() + "-" + ((object)((dynamic)Rut).ClipText()).ToStr().Mid(10, 1);
            //}

            //if (cmbMonedas.Text == "Todas")
            //{
            //    moneda = 9;
            //}
            //else
            //{
            //    moneda = cmbMonedas.Text.ToSng();
            //}
            string TxtOperacion = "";
            string strRUT = "";
            float moneda = 9.0F; //para traer todas las monedas
            int QRegistros = 0;
            return BuscarDeveng(datos, listaErrores, uow, periodo, strRUT, TxtOperacion, moneda, datos.TipoConsultaDev, QRegistros);

            //SSCmdparar.Visible = false;
        }

        private static bool BuscarDeveng(DevengIntReaj datos, List<UI_Message> listaErrores, UnitOfWorkCext01 uow, double periodo, string rut, string Operacion, float moneda, int TipoConsulta, int CantidadRegistros)
        {
            bool BuscarDeveng = false;

            int nContador = 0;
            int cont = 0;
            int nro_reg = 0;
            int I = 0;
            string RutaSyb = "";
            System.Windows.Forms.DialogResult resp = 0;
            string MsgGst = "";
            string Que = "";
            string RespuestaSRM = "";
            bool Retorno = false;
            int nNroRegistros = 0;
            object Contador = null;
            object nNroArr = null;

            using (Tracer tracer = new Tracer("Inicia busqueda devengo intereses y reajustes"))
            {
                try
                {

                    Retorno = false;
                    cont = 0;

                    nContador = 1;
                    nNroRegistros = 0;

                    datos.DevengInt = new List<T_DevengInt>();
                    datos.DevengReaj = new List<T_DevengReaj>();

                    Que = Que.TrimB();

                    if (TipoConsulta == 1)
                    {
                        var resultado = uow.SceRepository.Sce_Dev_Cons_MS((decimal)periodo, rut, Operacion, (decimal)moneda, datos.todos, (decimal)TipoConsulta, CantidadRegistros);

                        if (resultado.Count == 0)
                        {
                            listaErrores.Add(new UI_Message() { Text = "No existe Información para los datos ingresados .", Type = TipoMensaje.Informacion });
                            return false;
                        }

                        foreach (sce_dev_cons_MS_Result reg in resultado)
                        {
                            datos.DevengInt.Add(new T_DevengInt()
                            {
                                t_num_ope = double.Parse(reg.t_num_ope),
                                t_cui = int.Parse(reg.t_cui),
                                t_numneg = (int)(reg.t_numneg ?? 0),
                                t_monpro = (double)(reg.t_monpro ?? 0),
                                t_tastot = (double)(reg.t_tastot ?? 0),
                                t_fecini = DateTime.Parse(reg.t_fecini),
                                t_fecfin = DateTime.Parse(reg.t_fecfin),
                                t_dias = (int)(reg.t_dias ?? 0),
                                t_tipcam = (double)(reg.t_tipcam ?? 0),
                                t_rut = reg.t_rut,
                                t_ano_mes = (int)(reg.t_ano_mes ?? 0),
                                t_estado = (int)(reg.t_estado ?? 0),
                                t_mtovig = (double)(reg.t_mtovig ?? 0),
                                t_cuenta_k = reg.t_cuenta_k,
                                t_nemonico_k = reg.t_nemonico_k,
                                t_mtome_c = (double)(reg.t_mtome_c ?? 0),
                                t_mtomn_c = (double)(reg.t_mtomn_c ?? 0),
                                t_cuenta_c = reg.t_cuenta_c,
                                t_nemonico_c = reg.t_nemonico_c,
                                t_mtome_gn = (double)(reg.t_mtome_gn ?? 0),
                                t_mtomn_gn = (double)(reg.t_mtomn_gn ?? 0),
                                t_cuenta_gn = reg.t_cuenta_gn,
                                t_nemonico_gn = reg.t_nemonico_gn,
                                t_mtome_gd = (double)(reg.t_mtome_gd ?? 0),
                                t_mtomn_gd = (double)(reg.t_mtomn_gd ?? 0),
                                t_cuenta_gd = reg.t_cuenta_gd,
                                t_nemonico_gd = reg.t_nemonico_gd,
                                t_mtome_gdd = (double)(reg.t_mtome_gdd ?? 0),
                                t_mtomn_gdd = (double)(reg.t_mtomn_gdd ?? 0),
                                t_cuenta_gdd = reg.t_cuenta_gdd,
                                t_nemonico_gdd = reg.t_nemonico_gdd,
                                t_mtome_cp = (double)(reg.t_mtome_cp ?? 0),
                                t_mtomn_cp = (double)(reg.t_mtomn_cp ?? 0),
                                t_cuenta_cp = reg.t_cuenta_cp,
                                t_nemonico_cp = reg.t_nemonico_cp,
                                t_mtome_gpn = (double)(reg.t_mtome_gpn ?? 0),
                                t_mtomn_gpn = (double)(reg.t_mtomn_gpn ?? 0),
                                t_cuenta_gpn = reg.t_cuenta_gpn,
                                t_nemonico_gpn = reg.t_nemonico_gpn,
                                t_mtome_gpd = (double)(reg.t_mtome_gpd ?? 0),
                                t_mtomn_gpd = (double)(reg.t_mtomn_gpd ?? 0),
                                t_cuenta_gpd = reg.t_cuenta_gpd,
                                t_nemonico_gpd = reg.t_nemonico_gpd,
                                t_mtome_gpdd = (double)(reg.t_mtome_gpdd ?? 0),
                                t_mtomn_gpdd = (double)(reg.t_mtomn_gpdd ?? 0),
                                t_cuenta_gpdd = reg.t_cuenta_gpdd,
                                t_nemonico_gpdd = reg.t_nemonico_gpdd,
                                t_tippro = (int)(reg.t_tippro ?? 0),
                                t_numpro = (int)(reg.t_numpro ?? 0),
                                t_numcuo = (int)(reg.t_numcuo ?? 0),
                                t_fec_deterioro = int.Parse(reg.t_fec_deterioro),
                                t_tasa_penal = (double)(reg.t_tasa_penal ?? 0),
                                t_to = (int)(reg.t_to ?? 0),
                                t_to_plazo = (int)(reg.t_to_plazo ?? 0)
                            });
                        }
                    }
                    else if (TipoConsulta == 2)
                    {
                        var resultado = uow.SceRepository.Sce_Dev_Cons_MS_r((decimal)periodo, rut, Operacion, (decimal)moneda, datos.todos, (decimal)TipoConsulta, CantidadRegistros);
                        if (resultado.Count == 0)
                        {
                            listaErrores.Add(new UI_Message() { Text = "No existe Información para los datos ingresados .", Type = TipoMensaje.Informacion });
                            return false;
                        }

                        foreach (sce_dev_cons_Result_r reaj in resultado)
                        {
                            datos.DevengReaj.Add(new T_DevengReaj()
                                                {
                                                    t_num_ope = double.Parse(reaj.t_num_ope),
                                                    t_cui = int.Parse(reaj.t_cui),
                                                    t_numneg = (int)(reaj.t_numneg ?? 0),
                                                    t_monpro = (int)(reaj.t_monpro ?? 0),
                                                    t_tastot = (double)(reaj.t_tastot ?? 0),
                                                    t_fecini = DateTime.Parse(reaj.t_fecini),
                                                    t_fecfin = DateTime.Parse(reaj.t_fecfin),
                                                    t_dias = (int)(reaj.t_dias ?? 0),
                                                    t_tipcam = (double)(reaj.t_tipcam ?? 0),
                                                    t_rut = reaj.t_rut,
                                                    t_ano_mes = (int)(reaj.t_ano_mes ?? 0),
                                                    t_estado = (int)(reaj.t_estado ?? 0),
                                                    t_mtovig = (double)(reaj.t_mtovig ?? 0),
                                                    t_cuenta_k = reaj.t_cuenta_k,
                                                    t_nemonico_k = reaj.t_nemonico_k,
                                                    t_rmtome_c = (double)(reaj.t_rmtome_c ?? 0),
                                                    t_rmtomn_c = (double)(reaj.t_rmtomn_c ?? 0),
                                                    t_rcuenta_c = reaj.t_rcuenta_c != null ? reaj.t_rcuenta_c.ToString() : string.Empty,
                                                    t_rnemonico_c = reaj.t_rnemonico_c != null ? reaj.t_rnemonico_c.ToString() : string.Empty,
                                                    t_rmtome_gn = (double)(reaj.t_rmtome_gn ?? 0),
                                                    t_rmtomn_gn = (double)(reaj.t_rmtomn_gn ?? 0),
                                                    t_rcuenta_gn = reaj.t_rcuenta_gn != null ? reaj.t_rcuenta_gn.ToString() : string.Empty,
                                                    t_rnemonico_gn = reaj.t_rnemonico_gn != null ? reaj.t_rnemonico_gn.ToString() : string.Empty,
                                                    t_rmtome_gd = (double)(reaj.t_rmtome_gd ?? 0),
                                                    t_rmtomn_gd = (double)(reaj.t_rmtomn_gd ?? 0),
                                                    t_rcuenta_gd = reaj.t_rcuenta_gd != null ? int.Parse(reaj.t_rcuenta_gd.ToString()): 0,
                                                    t_rnemonico_gd = reaj.t_rnemonico_gd != null ? reaj.t_rnemonico_gd.ToString() : string.Empty,
                                                    t_rmtome_gdd = (double)(reaj.t_rmtome_gdd ?? 0),
                                                    t_rmtomn_gdd = (double)(reaj.t_rmtomn_gdd ?? 0),
                                                    t_rcuenta_gdd = reaj.t_rcuenta_gdd != null ? reaj.t_rcuenta_gdd.ToString() : string.Empty,
                                                    t_rnemonico_gdd = reaj.t_rnemonico_gdd != null ? reaj.t_rnemonico_gdd.ToString() : string.Empty,
                                                    t_tippro = (int)(reaj.t_tippro ?? 0),
                                                    t_numpro = (int)(reaj.t_numpro ?? 0),
                                                    t_sprwsh = reaj.t_sprwsh != null ? int.Parse(reaj.t_sprwsh.ToString()) : 0,
                                                    t_to = (int)(reaj.t_to ?? 0),
                                                    t_to_plazo = (int)(reaj.t_to_plazo ?? 0)
                                                });
                        }
                    }


                    Retorno = true;
                    BuscarDeveng = Retorno;
                    return BuscarDeveng;

                }
                catch (Exception exc)
                {
                    tracer.TraceException("Alerta, problemas al obtener los datos de la consulta de devengo intereces/reajustes", exc);
                    listaErrores.Add(new UI_Message() { Text = "Error al obtener la información.", Type = TipoMensaje.Error });
                }
            }
            return BuscarDeveng;
        }

        public static MemoryStream getFileConsultaIntereses(List<T_DevengInt> datos, string titulo)
        {
            using (Tracer tracer = new Tracer("GetFileIntereces"))
            {
                MemoryStream stream = new MemoryStream();
                try
                {
                    using (SLDocument doc = new SLDocument())
                    {
                        doc.RenameWorksheet(SLDocument.DefaultFirstSheetName, NombreWorksheetIntereces);

                        doc.SelectWorksheet(NombreWorksheetIntereces);
                        CargarHojaExcelIntereces(datos, doc, titulo);
                        doc.SaveAs(stream);
                    }

                    stream.Position = 0; //importante, dejar el stream pronto para leer;
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, no se ha podido generar archivo excel de consulta devengamiento reajuste", ex);
                }
                return stream;
            }
        }

        public static MemoryStream getFileConsultaReajustes(List<T_DevengReaj> datos, string titulo)
        {
            using (Tracer tracer = new Tracer("GetFileReajustes"))
            {
                MemoryStream stream = new MemoryStream();
                try
                {

                    using (SLDocument doc = new SLDocument())
                    {
                        doc.RenameWorksheet(SLDocument.DefaultFirstSheetName, NombreWorksheetIntereces);

                        doc.SelectWorksheet(NombreWorksheetIntereces);
                        CargarHojaExcelReajustes(datos, doc, titulo);
                        doc.SaveAs(stream);
                    }

                    stream.Position = 0; //importante, dejar el stream pronto para leer;

                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alera, no se ha podido generar archivo excel de consulta devengamiento reajuste", ex);
                }
                return stream;
            }
        }


        private static void CargarHojaExcelIntereces(List<T_DevengInt> acs, SLDocument doc, string titulo)
        {
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


            doc.MergeWorksheetCells("A1", "BB1");

            doc.SetCellValue("A1", "Consulta Devengamiento " + titulo);
            doc.SetCellStyle("A1", styleTitulo);

            int colIndex = 1;
            int rowIndex = 3;
            #region titulos
            doc.SetCellValue(rowIndex, colIndex++, "Operación");
            doc.SetCellValue(rowIndex, colIndex++, "CUI");
            doc.SetCellValue(rowIndex, colIndex++, "N° Negoc");
            doc.SetCellValue(rowIndex, colIndex++, "Moneda");
            doc.SetCellValue(rowIndex, colIndex++, "Tasa");
            doc.SetCellValue(rowIndex, colIndex++, "F Inicio");
            doc.SetCellValue(rowIndex, colIndex++, "F Deveng");
            doc.SetCellValue(rowIndex, colIndex++, "Días");
            doc.SetCellValue(rowIndex, colIndex++, "Tipo Cambio");
            doc.SetCellValue(rowIndex, colIndex++, "Rut");
            doc.SetCellValue(rowIndex, colIndex++, "Periodo");
            doc.SetCellValue(rowIndex, colIndex++, "Estado");
            doc.SetCellValue(rowIndex, colIndex++, "Capital");
            doc.SetCellValue(rowIndex, colIndex++, "Cta Capital");
            doc.SetCellValue(rowIndex, colIndex++, "Nem Capital");
            doc.SetCellValue(rowIndex, colIndex++, "Int xCob ME");
            doc.SetCellValue(rowIndex, colIndex++, "Int xCob MN");
            doc.SetCellValue(rowIndex, colIndex++, "Cuenta IXCob");
            doc.SetCellValue(rowIndex, colIndex++, "Nem IXCob");
            doc.SetCellValue(rowIndex, colIndex++, "Int Gan ME");
            doc.SetCellValue(rowIndex, colIndex++, "Int Gan MN");
            doc.SetCellValue(rowIndex, colIndex++, "Cta Int Gan");
            doc.SetCellValue(rowIndex, colIndex++, "Nem Int Gan");
            doc.SetCellValue(rowIndex, colIndex++, "Int Gan D+ME");
            doc.SetCellValue(rowIndex, colIndex++, "Int Gan D+MN");
            doc.SetCellValue(rowIndex, colIndex++, "Cta Int Gan D+");
            doc.SetCellValue(rowIndex, colIndex++, "Nem Int GanD+");
            doc.SetCellValue(rowIndex, colIndex++, "Acint D-ME");
            doc.SetCellValue(rowIndex, colIndex++, "Acint D- MN");
            doc.SetCellValue(rowIndex, colIndex++, "Cta Acint D-");
            doc.SetCellValue(rowIndex, colIndex++, "Nem Acint D-");
            doc.SetCellValue(rowIndex, colIndex++, "IXCPenal ME");
            doc.SetCellValue(rowIndex, colIndex++, "IXCPenal MN");
            doc.SetCellValue(rowIndex, colIndex++, "Cta IXCPenal");
            doc.SetCellValue(rowIndex, colIndex++, "Nem IXCPenal");
            doc.SetCellValue(rowIndex, colIndex++, "IGanPenal ME");
            doc.SetCellValue(rowIndex, colIndex++, "IGanPenal MN");
            doc.SetCellValue(rowIndex, colIndex++, "Cta IGanPenal");
            doc.SetCellValue(rowIndex, colIndex++, "Nem IGanPenal");
            doc.SetCellValue(rowIndex, colIndex++, "IGanPenalD+ME");
            doc.SetCellValue(rowIndex, colIndex++, "IGanPenalD+MN");
            doc.SetCellValue(rowIndex, colIndex++, "Cta IGanPenalD+");
            doc.SetCellValue(rowIndex, colIndex++, "Nem IGanPenalD+");
            doc.SetCellValue(rowIndex, colIndex++, "ACINTPenalD-ME");
            doc.SetCellValue(rowIndex, colIndex++, "ACINTPenalD-MN");
            doc.SetCellValue(rowIndex, colIndex++, "CtaAcintPenalD-");
            doc.SetCellValue(rowIndex, colIndex++, "Nem AcintpenalD-");
            doc.SetCellValue(rowIndex, colIndex++, "Tipo Prod");
            doc.SetCellValue(rowIndex, colIndex++, "Numprod");
            doc.SetCellValue(rowIndex, colIndex++, "N° Cuota");
            doc.SetCellValue(rowIndex, colIndex++, "Fecha Deterioro");
            doc.SetCellValue(rowIndex, colIndex++, "Tasa Penal");
            doc.SetCellValue(rowIndex, colIndex++, "TO");
            doc.SetCellValue(rowIndex, colIndex++, "Plazo");
            #endregion

            doc.SetCellStyle("A3", "BB3", styleEncabezado);

            doc.SetColumnStyle(1, styleInt);
            doc.SetColumnStyle(5, styleTasaInt);
            doc.SetColumnStyle(6, 7, styleFecha);
            doc.SetColumnStyle(9, styleDecimal);
            doc.SetColumnStyle(13, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(16, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(17, styleDecimalPesosChilenos);
            doc.SetColumnStyle(20, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(21, styleDecimalPesosChilenos);

            doc.SetColumnStyle(24, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(25, styleDecimalPesosChilenos);

            doc.SetColumnStyle(28, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(29, styleDecimalPesosChilenos);

            doc.SetColumnStyle(32, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(33, styleDecimalPesosChilenos);
            doc.SetColumnStyle(36, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(37, styleDecimalPesosChilenos);
            doc.SetColumnStyle(40, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(41, styleDecimalPesosChilenos);
            doc.SetColumnStyle(44, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(45, styleDecimalPesosChilenos);
            doc.SetColumnStyle(51, styleInt);
            doc.SetColumnStyle(52, styleTasaInt);

            doc.ImportDataTable(4, 1, Modulos.Modulo1.ConvertToDataTable(acs), false);

            doc.AutoFitColumn("A", "BB");
        }

        private static void CargarHojaExcelReajustes(List<T_DevengReaj> acs, SLDocument doc, string titulo)
        {
            #region Styles
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

            doc.MergeWorksheetCells("A1", "AJ1");

            doc.SetCellValue("A1", "Consulta Devengamiento " + titulo);
            doc.SetCellStyle("A1", styleTitulo);

            int colIndex = 1;
            int rowIndex = 3;
            #region titulos
            doc.SetCellValue(rowIndex, colIndex++, "Operación");
            doc.SetCellValue(rowIndex, colIndex++, "CUI");
            doc.SetCellValue(rowIndex, colIndex++, "N° Negoc");
            doc.SetCellValue(rowIndex, colIndex++, "Moneda");
            doc.SetCellValue(rowIndex, colIndex++, "Tasa");
            doc.SetCellValue(rowIndex, colIndex++, "F Inicio");
            doc.SetCellValue(rowIndex, colIndex++, "F Vcto");
            doc.SetCellValue(rowIndex, colIndex++, "Días");
            doc.SetCellValue(rowIndex, colIndex++, "Tipo Cambio");
            doc.SetCellValue(rowIndex, colIndex++, "Rut");
            doc.SetCellValue(rowIndex, colIndex++, "Periodo");
            doc.SetCellValue(rowIndex, colIndex++, "Estado");
            doc.SetCellValue(rowIndex, colIndex++, "Capital Vig");
            doc.SetCellValue(rowIndex, colIndex++, "Cta Capital");
            doc.SetCellValue(rowIndex, colIndex++, "Nem Capital");
            doc.SetCellValue(rowIndex, colIndex++, "Reaj X Cobrar MO");
            doc.SetCellValue(rowIndex, colIndex++, "Reaj X Cobrar MN");
            doc.SetCellValue(rowIndex, colIndex++, "Cta Reaj XCob");
            doc.SetCellValue(rowIndex, colIndex++, "Nem Reajxcob");
            doc.SetCellValue(rowIndex, colIndex++, "Reaj Ganado MO");
            doc.SetCellValue(rowIndex, colIndex++, "Reaj Ganado MN");
            doc.SetCellValue(rowIndex, colIndex++, "Cta Reaj Ganado");
            doc.SetCellValue(rowIndex, colIndex++, "Nem ReajGan");
            doc.SetCellValue(rowIndex, colIndex++, "Reaj Gan MO D+");
            doc.SetCellValue(rowIndex, colIndex++, "Reaj Gan MN D+");
            doc.SetCellValue(rowIndex, colIndex++, "Cta ReajGan D+");
            doc.SetCellValue(rowIndex, colIndex++, "Nem ReajGan D+");
            doc.SetCellValue(rowIndex, colIndex++, "ACReajuste MO D-");
            doc.SetCellValue(rowIndex, colIndex++, "ACReajuste MN D-");
            doc.SetCellValue(rowIndex, colIndex++, "Cta ACReaj D-");
            doc.SetCellValue(rowIndex, colIndex++, "NemACReaj D-");
            doc.SetCellValue(rowIndex, colIndex++, "Tip Prod");
            doc.SetCellValue(rowIndex, colIndex++, "N°Prod");
            doc.SetCellValue(rowIndex, colIndex++, "Cód Wsh");
            doc.SetCellValue(rowIndex, colIndex++, "TO");
            doc.SetCellValue(rowIndex, colIndex++, "Plazo");

            #endregion

            doc.SetCellStyle("A3", "AJ3", styleEncabezado);
            
            doc.SetColumnStyle(1, 2, styleInt);
            doc.SetColumnStyle(5, styleTasaInt);
            doc.SetColumnStyle(6, 7, styleFecha);
            doc.SetColumnStyle(9, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(13, styleDecimalMonedaExtranjera);
            doc.SetColumnStyle(16, 17, styleDecimalPesosChilenos);
            doc.SetColumnStyle(20, 21, styleDecimalPesosChilenos);
            doc.SetColumnStyle(24, 25, styleDecimalPesosChilenos);
            doc.SetColumnStyle(28, 29, styleDecimalPesosChilenos);
            doc.SetColumnStyle(32, 36, styleInt);

            doc.ImportDataTable(4, 1, Modulos.Modulo1.ConvertToDataTable(acs), false);
            doc.AutoFitColumn("A", "BB");
        }


    }
}
