using BCH.Comex.Common;
using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.FinDia;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace BCH.Comex.Core.BL.XGFD.Modulos
{
    internal static class MODFDIA
    {
        public struct Desc_Sw_Sup
        {
            public string referencia;
            public string id_esp;
            public string cctesp;
            public string id_sup;
            public string cctsup;
            public string tipmt;
            public int ncorr;
            public string glosa_estado;
            public string mensaje;
        }
        public static Desc_Sw_Sup[] Err_Sw_Sup = null;
        public static void main(DatosGlobales Globales, List<UI_Message> ListaMensajesError, UnitOfWorkCext01 uow)
        {
            bool bien = false;
            int SceLock = 0;
            
            // Verificar la unicidad del ejecutable en memoria
            //if (MigrationSupport.Utils.AppPrevInstance())
            //{
            //    MigrationSupport.Utils.MsgBox("La aplicación ya se encuentra en ejecución y no puede ejecutar más copias de ella.", MODGPYF0.pito(16).Cast<MigrationSupport.MsgBoxStyle>(), "Aplicación En Ejecución");
            //    Environment.Exit(0);
            //}

            //// Copyright del Banco de Chile en la Inicialización
            //System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            //System.Windows.Forms.Application.DoEvents();
            //System.Windows.Forms.Application.Run(Portada.DefInstance);
            //System.Windows.Forms.Application.DoEvents();

            //// Inscribe su nombre en task list
            //System.Reflection.Assembly.GetExecutingAssembly().GetName().Name = MODLOCK.Master_Titulo;
            //// asignamos su archivo de help (este mientras se construye"
            //MigrationSupport.Utils.HelpFile = MODGPYF0.GetHelpFile(MigrationSupport.Utils.GetEXEName());

            //// Bloquea el Fin de Día en Presencia de Otras aplicaciones
            //// de Comercio Exterior Activas.
            //// 

            // Registro de Usuario.-
            // If VerRegistroUsuario(False) Then End

            /******************************************************************
             ES NECESARIO REVISAR SI EL USUARIO TIENE INICIADO EL DIA Y SI TIENE PERMISOS
             * ****************************************************************/
            //if (MODGUSR.VerRegistroUsuario(true.ToInt()) != 0)
            //{
            //    Environment.Exit(0);
            //}


            /******************************************************************
             REVISA QUE NO HAYAN OPERACIONES DE MONEDA EXTRAJERA OPERATIVAS 
             * ****************************************************************/
            //string argTemp1 = MODLOCK.Master_Titulo;
            //SceLock = MODLOCK.BloquearSce(((int)FinDia.DefInstance.Handle), ref argTemp1, 2);
            //if (SceLock != 2)
            //{
            //    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            //    Environment.Exit(0);
            //}

            // Call Desarrollo
            // 
            //  D.S.B.
            // Hab_CtaCteLinea = True
            // 
            // If Hab_CtaCteLinea Then
            // 
            // Comentado Akzio Abril 2014
            // descomentado 20140820
            //bien = modcuacc.SyGet_NodoDst();
            //if (bien == 0)
            //{
            //    Environment.Exit(0);
            //}
            //// Fin Akzio Abril 2014

            bien = modcuacc.SyGet_CCtx(Globales.MODCUACC, ListaMensajesError, uow);
            if (bien)
            {
                //Environment.Exit(0);
                return;
            }
            // End If



            //PicIcoEnabled(FinDia.DefInstance.BajarDatos, true.ToInt());

            // Call Desarrollo
            //System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;

        }

        /// <summary>
        /// Cuadra Cuentas Contables por moneda
        /// </summary>
        /// <param name="jerarquia"></param>
        /// <returns></returns>
        public static ReporteMonedasDTO SyGet_CtaMnd(int jerarquia, string centroCosto, string especialista, UnitOfWorkCext01 uow,
            IList<UI_Message> listaMensajes)
        {
            ReporteMonedasDTO datosReporte = new ReporteMonedasDTO();
            datosReporte.TipoCuentas = new List<Tipo_CtaMon>();
            datosReporte.SumaCuentas = new List<Sum_Cta>();

            try
            {
                var resultado = uow.SceRepository.Sce_mcd_s05_MS(centroCosto, especialista, DateTime.Now.Date, jerarquia);

                if (resultado == null || resultado.Count == 0)
                {
                    // mensajes de error
                    return datosReporte;
                }

                foreach (var item in resultado)
                {
                    Tipo_CtaMon cta = new Tipo_CtaMon();
                    cta.fecha = DateTime.Now.ToString("dd/MM/yyyy");
                    cta.CCosto = centroCosto;
                    cta.UsrEsp = especialista;
                    cta.nemcta = item.nemcta;
                    cta.numcta = item.numcta;
                    cta.nemmon = item.nemmon;
                    cta.MtoMcd_d = item.mtomcd_d.HasValue ? (double)item.mtomcd_d : 0;
                    cta.MtoMcd_h = item.mtomcd_h.HasValue ? (double)item.mtomcd_h : 0;
                    cta.MtoMcd_dt = cta.MtoMcd_d.ToString("#,###,###,###,##0.00");
                    cta.MtoMcd_ht = cta.MtoMcd_h.ToString("#,###,###,###,##0.00");

                    datosReporte.TipoCuentas.Add(cta);
                }

                // agrupo por cuentas
                datosReporte.SumaCuentas = datosReporte.TipoCuentas.Select(item => item.nemmon).Distinct()
                    .Select(nemonico => new Sum_Cta { nemmon = nemonico }).ToList();
                foreach (var suma in datosReporte.SumaCuentas)
                {
                    suma.MtoDeb = datosReporte.TipoCuentas.Where(c => c.nemmon == suma.nemmon)
                        .Sum(c => c.MtoMcd_d);
                    suma.MtoHab = datosReporte.TipoCuentas.Where(x => x.nemmon == suma.nemmon)
                        .Sum(c => c.MtoMcd_h);
                }

                return datosReporte;
            }
            catch (Exception ex)
            {
                listaMensajes.Add(new UI_Message
                {
                    Text = "Se ha producido un error al efectuar la cuadratura por cuentas contables y moneda. Reporte este problema.",
                    Title = "Fin de día",
                    Type = TipoMensaje.Error
                });

                if (!ExceptionPolicy.HandleException(ex, "PoliticaBLFundTransfer")) throw;

                return null;
            }

        }

        /// <summary>
        /// Resumen Movimientos Contables agrupados por Nemónico
        /// </summary>
        /// <param name="imprime"></param>
        /// <param name="Seccion"></param>
        public static Printer Lst_CtaMnd(bool imprime, bool Seccion, T_MODGUSR modgusr, List<Sum_Cta> sumCtaList,
            List<Tipo_CtaMon> ctaMonList)
        {
            int n = 0;
            string s = "";
            double MontoHaber = 0.0;
            double MontoDebe = 0.0;
            int i = 0;
            int T = 3;

            string MtoDebe = "";
            string MtoHaber = "";
            string Cuenta_t = "";
            string Moneda_t = "";
            double TotalDebe = 0.0;
            string TotalDebe_t = "";
            double TotalHaber = 0.0;
            string TotalHaber_t = "";


            Printer printer = new Printer();
            //MODGPLN.Pagina = 0;

            // Abre archivo de texto para escribir datos.-
            ////FName = "C:\\Data\\Sce\\Doc\\CtaMnd.Txt";
            ////FNum = MigrationSupport.FileSystem.FreeFile();
            ////MigrationSupport.FileSystem.FileOpen(FNum, FName, MigrationSupport.FileSystem.OpenMode.Output, MigrationSupport.FileSystem.OpenAccess.Default, MigrationSupport.FileSystem.OpenShare.Default, -1);

            // Título del resumen contable.-
            if (Seccion)
            {
                MODFDIA.TituloCodUsr0(printer, imprime, modgusr.UsrEsp.CentroCosto, modgusr.UsrEsp.Especialista, modgusr.UsrEsp.nombre);
            }
            else
            {
                TituloPorCtaMnd(printer, imprime, modgusr.UsrEsp.CentroCosto, modgusr.UsrEsp.Especialista, modgusr.UsrEsp.nombre);
            }

            foreach (var item in ctaMonList)
            {
                //MontoDebe = !string.IsNullOrWhiteSpace(item.MtoMcd_dt) ? double.Parse(item.MtoMcd_dt) : 0;
                //MontoHaber = !string.IsNullOrWhiteSpace(item.MtoMcd_ht) ? double.Parse(item.MtoMcd_ht) : 0;
                TotalDebe += item.MtoMcd_d;
                TotalHaber += item.MtoMcd_h;
                s = item.numcta;
                Cuenta_t = s.Substring(0, 3) + "." + s.Substring(3, 2) + "." + s.Substring(5, 2) + "-" + s.Substring(7, 1);
                Moneda_t = item.nemmon;
                MtoDebe = item.MtoMcd_dt.PadLeft(20);  //new string(' ', 20 - MontoDebe.Len).ToDbl() + MontoDebe;
                MtoHaber = item.MtoMcd_ht.PadLeft(20); // new string(' ', 20 - MontoHaber.Len()).ToDbl() + MontoHaber;
                //printer.PrintList(Printer.TAB((short)(T)), Cuenta_t, Printer.TAB((short)(14 + T)), item.nemcta,
                //    Printer.TAB((short)(31 + T)), Moneda_t, Printer.TAB((short)(37 + T)), MtoDebe,
                //    Printer.TAB((short)(57 + T)), MtoHaber);
                if (imprime)
                {
                    printer.PrintList(Printer.TAB((short)(T)), Cuenta_t, Printer.TAB((short)(14 + T)), item.nemcta,
                        Printer.TAB((short)(31 + T)), Moneda_t,
                        Printer.TAB((short)(37 + T)), MtoDebe, Printer.TAB((short)(57 + T)), MtoHaber);
                }
                n += 1;
                if (n > 40)
                {
                    //MigrationSupport.FileSystem.PrintLine(FNum, 12.Char());
                    printer.Print();
                    // Título del resumen contable.-
                    TituloPorCtaMnd(printer, imprime, modgusr.UsrEsp.CentroCosto, modgusr.UsrEsp.Especialista, modgusr.UsrEsp.nombre);
                    n = 0;
                }
            }

            //TotalDebe_t = TotalDebe.ToString("#,###,###,###,##0.00");
            //TotalHaber_t = TotalHaber.ToString("#,###,###,###,##0.00");
            //TotalDebe_t = TotalDebe_t.PadLeft(20);
            //TotalHaber_t = TotalHaber_t.PadLeft(20);
            //printer.PrintList(Printer.TAB((short)(T)), "----------------------------------------------------------------------------");
            //printer.PrintList(Printer.TAB((short)(T)), "Totales : ", Printer.TAB((short)(22 + T)), "-" + TotalDebe_t, 
            //    Printer.TAB((short)(43 + T)), "-" + TotalHaber_t);
            //MigrationSupport.FileSystem.PrintLine(FNum, 12.Char());

            //// Cierra archivo de texto y se muestra.-
            //MigrationSupport.FileSystem.FileClose(1);

            if (imprime)
            {
                printer.PrintList(Printer.TAB((short)(T)),
                    "----------------------------------------------------------------------------");

                foreach (var item in sumCtaList)
                {
                    if (sumCtaList.IndexOf(item) == 0)
                    {
                        printer.PrintList(Printer.TAB((short)(T)), "Totales : " + new string(' ', 21),
                            item.nemmon.PadLeft(3), Printer.TAB((short)(37 + T)),
                            item.MtoDeb.ToString("#,###,###,###,###.00").PadLeft(20),
                            item.MtoHab.ToString("#,###,###,###,###.00").PadLeft(20));
                    }
                    else
                    {
                        printer.PrintList(Printer.TAB((short)(T)), new string(' ', 31), (item.nemmon + "   ").PadLeft(3),
                            Printer.TAB((short)(37 + T)),
                            item.MtoDeb.ToString("#,###,###,###,###.00").PadLeft(20) +
                            item.MtoHab.ToString("#,###,###,###,###.00").PadLeft(20));
                    }
                }

                //MigrationSupport.Printer.DefInstance.EndDoc();
            }

            return printer;
        }

        /// <summary>
        /// Título para resumen general contable por nemónico
        /// </summary>
        /// <param name="FNum"></param>
        /// <param name="imprime"></param>
        public static void TituloCodUsr0(Printer printer, bool imprime, string centroCosto, string especialista, string nombreUsuario)
        {
            int T = 3;

            int pagina = 1;
            //MODGPLN.Pagina = MODGPLN.Pagina + 1;
            //printer.Print();
            //printer.Print();
            //printer.PrintList(Printer.TAB(3), "Comercio Exterior", Printer.TAB(28), "MOVIMIENTOS CONTABLES", Printer.TAB(62), "Fecha  : " 
            //    + DateTime.Now.ToString("dd/mm/yyyy"));
            //printer.PrintList(Printer.TAB(62), "Pagina : " + pagina.ToString("00"));
            //printer.Print("");
            //printer.Print("");
            //// Implementar encabezado para más de un usuario.-
            //printer.PrintList(Printer.TAB((short)(T)), centroCosto + "-" + especialista + " : " + nombreUsuario);
            //printer.PrintList(Printer.TAB((short)(T)), "Agrupado por Cuentas Contables y Moneda");
            //printer.PrintList(Printer.TAB((short)(T)), "----------------------------------------------------------------------------");
            //printer.PrintList(Printer.TAB((short)(T)), "Cuenta", Printer.TAB(17), "Nemonico", Printer.TAB(32), "Moneda", Printer.TAB(55),
            //    "Debe", Printer.TAB(74), "Haber");
            //printer.PrintList(Printer.TAB((short)(T)), "----------------------------------------------------------------------------");
            if (imprime)
            {
                printer.Print();
                printer.Print();
                printer.PrintList(Printer.TAB(3), "COMERCIO EXTERIOR", Printer.TAB(28), "Contabilidad Diaria",
                    Printer.TAB(62), "Fecha  : " + DateTime.Now.ToString("dd/MM/yyyy"));
                printer.PrintList(Printer.TAB(28), "===================", Printer.TAB(62), "Pagina : " +
                    pagina.ToString("00"));
                printer.Print("");
                printer.Print("");
                printer.PrintList(Printer.TAB((short)(T)), "Centro de Costo : ", Printer.TAB((short)(T + 18)), centroCosto);
                printer.PrintList(Printer.TAB((short)(T)), "Agrupado por Cuentas Contables y Moneda");
                printer.PrintList(Printer.TAB((short)(T)), "----------------------------------------------------------------------------");
                printer.PrintList(Printer.TAB((short)(T)), "Cuenta", Printer.TAB(17), "Nemonico", Printer.TAB(32), "Moneda", Printer.TAB(55),
                    "Debe", Printer.TAB(74), "Haber");
                printer.PrintList(Printer.TAB((short)(T)), "----------------------------------------------------------------------------");
            }
        }

        /// <summary>
        /// Título para resumen contable por nemónico
        /// </summary>
        public static void TituloPorCtaMnd(Printer printer, bool imprime, string centroCosto, string especialista, string userNombre)
        {
            object RutUsr = null;
            int T = 3;
            //MODGPLN.Pagina = MODGPLN.Pagina + 1;
            int pagina = 1;

            //printer.Print("");
            //printer.Print("");
            //printer.PrintList(Printer.TAB(3), "Comercio Exterior", Printer.TAB(28), "MOVIMIENTOS CONTABLES", Printer.TAB(62), 
            //    "Fecha  : " + DateTime.Now.ToString("dd/mm/yyyy"));
            //printer.PrintList(Printer.TAB(62), "Pagina : " + pagina.ToString("00"));
            //printer.Print("");
            //printer.Print("");
            //// Implementar encabezado para más de un usuario.-
            //printer.PrintList(Printer.TAB((short)(T)), centroCosto + "-" + especialista + " : " + userNombre);
            //printer.PrintList(Printer.TAB((short)(T)), "Agrupado por Cuentas Contables y Moneda");
            //printer.PrintList(Printer.TAB((short)(T)), "----------------------------------------------------------------------------");
            //printer.PrintList(Printer.TAB((short)(T)), "Cuenta", Printer.TAB(17), "Nemonico", Printer.TAB(32), "Moneda", Printer.TAB(55), 
            //    "Debe", Printer.TAB(74), "Haber");
            //printer.PrintList(Printer.TAB((short)(T)), "----------------------------------------------------------------------------");
            if (imprime)
            {

                //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeName(MigrationSupport.Printer.DefInstance.Font, "Courier New");
                printer.Print("");
                printer.Print("");
                printer.PrintList(Printer.TAB(3), "COMERCIO EXTERIOR", Printer.TAB(28), "Contabilidad Diaria",
                    Printer.TAB(62), "Fecha  : " + DateTime.Now.ToString("dd/MM/yyyy"));
                printer.PrintList(Printer.TAB(28), "===================", Printer.TAB(62), "Pagina : " +
                    pagina.ToString("00"));
                printer.Print("");
                printer.Print("");
                printer.PrintList(Printer.TAB((short)(T)), centroCosto + "-" +
                    especialista + " (" + RutUsr + ") : " + userNombre);
                printer.PrintList(Printer.TAB((short)(T)), "Agrupado por Cuentas Contables y Moneda");
                printer.PrintList(Printer.TAB((short)(T)), "----------------------------------------------------------------------------");
                printer.PrintList(Printer.TAB((short)(T)), "Cuenta", Printer.TAB(17), "Nemonico", Printer.TAB(32), "Moneda", Printer.TAB(55), "Debe",
                    Printer.TAB(74), "Haber");
                printer.PrintList(Printer.TAB((short)(T)), "----------------------------------------------------------------------------");
            }
        }

        /// <summary>
        /// Lee el NroRpt, FecMov de Sce_Mcd verificando el formato de la Cta. Cte.
        /// </summary>
        /// <param name="cencos"></param>
        /// <param name="codusr"></param>
        /// <param name="FecMcd"></param>
        public static bool SyGet_CtaCteMN(string cencos, string codusr, DateTime FecMcd, T_MODFDIA MODFDIA, IList<UI_Message> ListaErrores, UnitOfWorkCext01 uow)
        {
            bool SyGet_CtaCteMN = false;
            int n = 0;

            using (Tracer tracer = new Tracer())
            {

                try
                {
                    MODFDIA.VBcoFin = new List<string>();

                    List<sce_mcd_s28_MS_Result> R = uow.SceRepository.sce_mcd_s28_MS(cencos, codusr, FecMcd).ToList();

                    // Error de Comunicación.
                    if (R == null)
                    {
                        ListaErrores.Add(new UI_Message() { Text = "Se ha producido un error al tratar de leer los formatos de Cta. Cte./MN. Reporte este problema.", Type = TipoMensaje.Error });
                        return SyGet_CtaCteMN;
                    }

                    n = R.Count;
                    if (n == 0)//MODGPYF0.copiardestring(R, "~", 1) == "0"
                    {
                        SyGet_CtaCteMN = true;
                    }
                    else
                    {
                        foreach (sce_mcd_s28_MS_Result aux in R)
                        {
                            MODFDIA.VBcoFin.Add(aux.nrorpt.ToString() + " - " + aux.fecmov.ToShortDateString());
                        }
                        SyGet_CtaCteMN = true;
                    }
                    return SyGet_CtaCteMN;

                }
                catch (Exception exc)
                {
                    tracer.TraceException("Alerta en SyGet_CtaCteMN", exc);
                    ListaErrores.Add(new UI_Message() 
                        {   Text = "Se ha producido un error al tratar de leer los formatos de Cta. Cte./MN. Reporte este problema.", 
                            Type = TipoMensaje.Error 
                        });
                        
                }
            }
            return SyGet_CtaCteMN;
        }


        /// <summary>
        /// Lee el NroRpt, FecMov de Sce_Mcd donde el NemCta:
        /// ACE, OPE, OPEPEND, COE, BOE, BANCENE, además FecMov = Now y CodBco =0
        /// Retorna el string que se obtuvo.
        /// </summary>
        /// <param name="cencos"></param>
        /// <param name="codusr"></param>
        /// <param name="FecMcd"></param>
        public static bool SyGet_CtaBco(string cencos, string codusr, DateTime FecMcd, T_MODFDIA MODFDIA, IList<UI_Message> ListaErrores, UnitOfWorkCext01 uow)
        {
            bool SyGet_CtaBco = false;

            using (Tracer tracer = new Tracer())
            {
                try
                {
                    // Se ejecuta el Procedimiento Almacenado.
                    List<sce_mcd_s06_MS_Result> R = uow.SceRepository.sce_mcd_s06_MS(cencos, codusr, FecMcd).ToList();

                    // Error de Comunicación.
                    if (R == null)
                    {
                        ListaErrores.Add(new UI_Message() { Text = "Se ha producido un error al tratar de leer Nro.Correlativo + Fecha de la Tabla de Contabilidades. Reporte este problema.", Type = TipoMensaje.Error });
                        return SyGet_CtaBco;
                    }

                    int n = R.Count;
                    MODFDIA.VBcoFin = new List<string>();
                    foreach (sce_mcd_s06_MS_Result r in R)
                    {
                        MODFDIA.VBcoFin.Add(r.nrorpt + " - " + r.fecmov.ToShortDateString());
                    }
                    SyGet_CtaBco = true;

                    return SyGet_CtaBco;

                }
                catch (Exception exc)
                {
                    tracer.TraceException("Alerta en SyGet_CtaBco", exc);
                    ListaErrores.Add(new UI_Message() 
                            { 
                                Text = "Se ha producido un error al tratar de leer Nro.Correlativo + Fecha de la Tabla de Contabilidades. Reporte este problema.", 
                                Type = TipoMensaje.Error 
                            });
                        
                }
            }
            return SyGet_CtaBco;
        }

        public static bool SyGet_CtaSuc(string cencos, string codusr, DateTime FecMcd, T_MODFDIA MODFDIA, IList<UI_Message> ListaErrores, UnitOfWorkCext01 uow)
        {
            bool SyGet_CtaSuc = false;

            using (Tracer tracer = new Tracer())
                try
                {
                    // Se ejecuta el Procedimiento Almacenado.
                    List<sce_mcd_s61_MS_Result> R = uow.SceRepository.sce_mcd_s61_MS(cencos, codusr, FecMcd).ToList();

                    // Error de Comunicación.
                    if (R == null)
                    {
                        ListaErrores.Add(new UI_Message() { Text = "Se ha producido un error al tratar de leer Nro.Correlativo + Fecha de la Tabla de Contabilidades. Reporte este problema.", Type = TipoMensaje.Error });
                        return SyGet_CtaSuc;
                    }
                    MODFDIA.VBcoFin = new List<string>();

                    foreach (sce_mcd_s61_MS_Result r in R)
                    {
                        MODFDIA.VBcoFin.Add(r.nrorpt + " - " + r.fecmov.ToShortDateString());
                    }
                    SyGet_CtaSuc = true;

                    return SyGet_CtaSuc;

                }
                catch (Exception exc)
                {
                   tracer.TraceException("Alerta en SyGet_CtaSuc", exc);
                   ListaErrores.Add(new UI_Message() 
                        { 
                            Text = "Se ha producido un error al tratar de leer Nro.Correlativo + Fecha de la Tabla de Contabilidades. Reporte este problema.",
                            Type = TipoMensaje.Error
                        });
                        
                }
            return SyGet_CtaSuc;
        }

        /// <summary>
        /// Lee el NroRpt, FecMov de Sce_Mcd donde el NemCta:
        /// ACE, OPE, OPEPEND, COE, BOE, BANCENE, además FecMov = Now y CodBco =0
        /// Retorna el string que se obtuvo.
        /// </summary>
        /// <param name="cencos"></param>
        /// <param name="codusr"></param>
        /// <param name="FecMcd"></param>
        public static bool SyGet_ContabOK(string cencos, string codusr, DateTime FecMcd, T_MODFDIA MODFDIA, IList<UI_Message> ListaErrores, UnitOfWorkCext01 uow)
        {
            bool SyGet_ContabOK = false;
            using (Tracer tracer = new Tracer())
            {
                try
                {
                    // Se ejecuta el Procedimiento Almacenado.
                    List<sce_mcd_s40_MS_Result> R = uow.SceRepository.sce_mcd_s40_MS(cencos, codusr, FecMcd).ToList();

                    // Error de Comunicación.
                    if (R == null)
                    {
                        ListaErrores.Add(new UI_Message() { Text = "Se ha producido un error al tratar de leer Contabilidad Diaria Reporte este problema.", Type = TipoMensaje.Error });
                        return SyGet_ContabOK;
                    }
                    
                    MODFDIA.VCtb = new List<string>();
                    foreach (sce_mcd_s40_MS_Result r in R)
                    {
                        MODFDIA.VCtb.Add(r.codcct + "-" + r.codpro + "-" + r.codesp + "-" + r.codofi + "-" + r.codope + " - " + r.nrorpt.ToString().Trim() + " - " + r.fecmov.ToShortDateString());
                    }
                    SyGet_ContabOK = true;

                    return SyGet_ContabOK;

                }
                catch (Exception exc)
                {
                    tracer.TraceException("Alerta en SyGet_ContabOK", exc);
                    ListaErrores.Add(new UI_Message() 
                        {
                            Text = "Se ha producido un error al tratar de leer Contabilidad Diaria Reporte este problema.",
                            Type = TipoMensaje.Error 
                        });
                        
                }
            }
            return SyGet_ContabOK;
        }

        public static bool SyGet_ContabVig(string cencos, string codusr, T_MODFDIA MODFDIA, IList<UI_Message> ListaErrores, UnitOfWorkCext01 uow)
        {
            bool SyGet_ContabVig = false;
            using (Tracer tracer = new Tracer())
            {
                try
                {

                    // Se ejecuta el Procedimiento Almacenado.
                    List<sce_mch_s12_MS_Result> R = uow.SceRepository.sce_mch_s12_MS(cencos, codusr).ToList();

                    // Error de Comunicación.
                    if (R == null)
                    {
                        ListaErrores.Add(new UI_Message() { Text = "Se ha producido un error al tratar de leer Contabilidad Diaria Reporte este problema.", Type = TipoMensaje.Error });
                        return SyGet_ContabVig;
                    }

                    MODFDIA.VCtb = new List<string>();
                    foreach (sce_mch_s12_MS_Result r in R)
                    {
                        MODFDIA.DetallesReportes.Add(new DetalleProblemas() { NroOperacion = r.codcct + "-" + r.codpro + "-" + r.codesp + "-" + r.codofi + "-" + r.codope, NroReporte = r.nrorpt.ToString().Trim(), FechaContable = r.fecmov.ToShortDateString() });
                    }
                    SyGet_ContabVig = true;

                    return SyGet_ContabVig;

                }
                catch (Exception exc)
                {
                   tracer.TraceException("Alerta en SyGet_ContabVig", exc);
                   ListaErrores.Add(new UI_Message() 
                        {   Text = "Se ha producido un error al tratar de leer Contabilidad Diaria Reporte este problema.",
                            Type = TipoMensaje.Error 
                        });
                        
                }
            }
            return SyGet_ContabVig;
        }


        public static bool SyGet_ContabNoDiaCBS(string cencos, string codusr, T_MODFDIA modfdia, IList<UI_Message> listaMensajes, UnitOfWorkCext01 uow)
        {
            bool SyGet_ContabNoDiaCBS = false;

            using (Tracer tracer = new Tracer())
            {
                try
                {
                    // Se ejecuta el Procedimiento Almacenado.
                    IList<sce_mch_s14_MS_Result> result = uow.SceRepository.sce_mch_s14_MS(cencos, codusr);

                    // Error de Comunicación.
                    if (result == null)
                    {
                        listaMensajes.Add(new UI_Message() { Text = "Se ha producido un error al tratar de leer Contabilidad Diaria Reporte este problema.", Type = TipoMensaje.Error });
                       return SyGet_ContabNoDiaCBS;
                    }


                    foreach (sce_mch_s14_MS_Result r in result)
                    {
                        modfdia.DetallesReportes.Add(new DetalleProblemas() { 
                            NroOperacion = r.codcct + "-" + r.codpro + "-" + r.codesp + "-" + 
                            r.codofi + "-" + r.codope, 
                            NroReporte = r.nrorpt.ToString().Trim(), 
                            FechaContable = r.fecmov.ToShortDateString() 
                        });
                    }
                    SyGet_ContabNoDiaCBS = true;

                    return SyGet_ContabNoDiaCBS;

                }
                catch (Exception exc)
                {
                    tracer.TraceException("Alerta en SyGet_ContabNoDiaCBS", exc);
                    listaMensajes.Add(new UI_Message() { 
                        Text = "Se ha producido un error al tratar de leer Contabilidad Diaria Reporte este problema.", 
                        Type = TipoMensaje.Error 
                    });

                }
            }
            return SyGet_ContabNoDiaCBS;
        }

        public static bool Fn_ValidaPlan(string cencos, string codusr, DateTime FecIng, T_MODFDIA modfdia, IList<UI_Message> ListaErrores, UnitOfWorkCext01 uow)
        {
            bool Fn_ValidaPlan = false;

            int i = 0;
            int n = 0;
            //string R = "";
            string Que = "";
            using (Tracer tracer = new Tracer())
            {
                try
                {

                    //Que = "";
                    //Que = "Exec " + MODGSRM.ParamSrm8k.Base + "." + MODGSRM.ParamSrm8k.usuario + "." + "sce_gpln_s15 ";
                    //Que = Que.LCase();
                    //Que = Que + MODGSYB.dbcharSy(cencos) + ", ";
                    //Que = Que + MODGSYB.dbcharSy(codusr) + ", ";
                    //Que = Que + MODGSYB.dbdatesy(FecIng);

                    //// Se ejecuta el Procedimiento Almacenado.
                    //R = MODGSRM.RespuestaQuery(ref Que);
                    List<sce_gpln_s15_MS_Result> R = uow.SceRepository.sce_gpln_s15_MS(cencos, codusr, FecIng).ToList();


                    //// Error de Comunicación.
                    if (R == null)
                    {
                        //MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de leer las planillas. Reporte este problema.", MODGPYF0.pito(48).Cast<MigrationSupport.MsgBoxStyle>(), MODFDIA.MsgFDia);
                        ListaErrores.Add(new UI_Message() { Text = "Se ha producido un error al tratar de leer las planillas. Reporte este problema.", Type = TipoMensaje.Error });
                        return Fn_ValidaPlan;
                    }

                    //n = MODGSRM.RowCount;
                    //Est_Pla = new planillas[n + 1];
                    //for (i = 1; i <= n; i += 1)
                    //{
                    //    Est_Pla[i].numpla = MODGSYB.GetPosSy(MODGSYB.NumIni(), "N", R).ToInt();
                    //    Est_Pla[i].fecpla = MODGSYB.GetPosSy(MODGSYB.NumSig(), "F", R).ToStr();
                    //    R = MODGSRM.NuevaRespuesta(2, R);
                    //}
                    foreach (sce_gpln_s15_MS_Result r in R)
                    {
                        modfdia.Est_Pla.Add(new planillas() { numpla = (int)( r.numpre ?? 0 ) , fecpla = ((DateTime)r.Column1).ToShortDateString() });
                        modfdia.DetallesReportes.Add(new DetalleProblemas() { NroReporte = (r.numpre ?? 0).ToString(), FechaContable = ((DateTime)r.Column1).ToShortDateString() });
                    }
                    Fn_ValidaPlan = true;

                    return Fn_ValidaPlan;

                }
                catch (Exception exc)
                {
                    //MigrationSupport.GlobalException.Initialize(exc);
                    //MigrationSupport.Utils.MsgBox("[0" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] " + MigrationSupport.Utils.GetErrorDescrption(MigrationSupport.GlobalException.Instance.Number), MODGPYF0.pito(48).Cast<
                    //   MigrationSupport.MsgBoxStyle>(), MODFDIA.MsgFDia);

                    tracer.TraceException("Alerta en Fn_ValidaPlan", exc);
                }
            }
            return Fn_ValidaPlan;
        }

        public static bool Chq_ContaSTB(T_MODGUSR modgusr, T_MODCUACC modCuaCC, UnitOfWorkCext01 uow, IList<UI_Message> listaMensajes)
        {
            using (var tracer = new Tracer())
            {
                bool Chq_ContaSTB = false;

                //  D.S.B.
                string rutesp = "";
                string codofi = "";     //  As String
                bool bien = false;

                try
                {
                    Chq_ContaSTB = true;

                    codofi = modgusr.UsrEsp.Oficina.ToString("00000");
                    //  Obtiene Rut Oficina
                    bien = modcuacc.SyGet_RutOficina(modgusr.UsrEsp.CentroCosto, modgusr.UsrEsp.Especialista, ref rutesp,
                        uow, listaMensajes);
                    if (!bien)
                    {
                        return false;
                    }

                    //  Obtiene Contabilidad del Día realizada por el Especialista Sce_Mcd.
                    bien = modcuacc.SyGet_ContaEsp(modgusr.UsrEsp.CentroCosto, modgusr.UsrEsp.Especialista, rutesp,
                        DateTime.Today, modCuaCC, uow, listaMensajes);

                    if (!bien)
                    {
                        return false;
                    }

                    //  Obtiene Trx del STB
                    modCuaCC.TTraSTB = new List<T_TraSTB>();

                    // Cuadra inyecciones CCOL
                    bien = MODCUAD2.SyGet_ContaEsp2(modgusr.UsrEsp.CentroCosto, modgusr.UsrEsp.Especialista,
                        DateTime.Today, modCuaCC, uow, listaMensajes);

                    Chq_ContaSTB = bien;

                    return Chq_ContaSTB;
                }
                catch (Exception exc)
                {
                    tracer.TraceException("Alerta en Chq_ContaSTB: ", exc);

                    Chq_ContaSTB = false;
                }

                return Chq_ContaSTB;
            }
        }

        // ****************************************************************************
        //    1.  Cuenta si existen Cuentas Críticas o Cuentas No Críticas.
        // ****************************************************************************
        public static void Fn_CtaCrit(IList<Tipo_CtaMon> CtaMon, IList<UI_Message> listaMensaje, out int criticasEnError, out int noCriticasEnError)
        {
            criticasEnError = 0;
            noCriticasEnError = 0;
            
            string Paso = "";
            string nemcta = "";
            
            string CtasCrit = ConfigurationManager.AppSettings["FinDia.CuentasPuentesCriticas"]; //MODGPYF0.GetSceIni("Cuentas Puentes", "CtasCriticas").UCase();
            string CtasNoCrit = ConfigurationManager.AppSettings["FinDia.CuentasPuentesNoCriticas"];//MODGPYF0.GetSceIni("Cuentas Puentes", "CtasNOCriticas").UCase();
            

            List<string> listaCuentasCriticas = CtasCrit.Replace(" ", "").ToUpper().Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> listaCuentasNoCriticas = CtasNoCrit.Replace(" ", "").ToUpper().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
           
            
            foreach (Tipo_CtaMon tCta in CtaMon)
            {
                nemcta = tCta.nemcta.Trim().ToUpper();
                if (listaCuentasCriticas.Contains(nemcta))
                {
                    // Contabiliza si existe alguna Cuenta Crítica.
                    if (tCta.MtoMcd_d != tCta.MtoMcd_h)
                    {
                        criticasEnError++;
                        Paso = nemcta + " - " + tCta.nemmon.Trim() + " - Debe " + tCta.MtoMcd_dt.Trim() + " - Haber " + tCta.MtoMcd_ht.Trim();
                        listaMensaje.Add(new UI_Message() { Text = "Se ha encontrado la siguiente Cuenta Puente descuadrada: " + Paso.Trim() , Type = TipoMensaje.Error });
                    }
                }
                // Contabiliza si existe alguna Cuenta No Crítica.
                if (listaCuentasNoCriticas.Contains(nemcta))
                {
                    if (tCta.MtoMcd_d != tCta.MtoMcd_h)
                    {
                        noCriticasEnError++;
                        Paso = nemcta + " - " + tCta.nemmon.Trim() + " - Debe " + tCta.MtoMcd_dt.Trim() + " - Haber " + tCta.MtoMcd_ht.Trim();
                        listaMensaje.Add(new UI_Message() { Text = "Se ha encontrado la siguiente Cuenta Puente descuadrada: " + Paso.Trim(), Type= TipoMensaje.Warning });
                    }
                }
            }
        }
      
    }
}
