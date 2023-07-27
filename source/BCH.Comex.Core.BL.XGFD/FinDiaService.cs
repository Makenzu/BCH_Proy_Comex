using BCH.Comex.Common;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XGFD.Forms;
using BCH.Comex.Core.BL.XGFD.Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.FinDia;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Swift;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XGFD
{
    public partial class FinDiaService : IDisposable
    {
        private UnitOfWorkCext01 uow; 
        private UnitOfWorkSwift uows;
        public FinDiaService()
        {
            uow = new UnitOfWorkCext01();
            uows = new UnitOfWorkSwift();
        }

        public void Dispose()
        {
            if (uow != null)
            {
                uow.Dispose();
            }
        }

        public DatosGlobales Iniciar(IDatosUsuario Usuario, IList<UI_Message> ListaMensajes)
        {
            DatosGlobales globales = new DatosGlobales() { Usuario = Usuario };


            if (!MODGUSR.VerRegistroUsuario(1, globales, ListaMensajes, uow))
            {
                return globales;
            }

            modcuacc.SyGet_CCtx(globales.MODCUACC, ListaMensajes, uow);

            return globales;
        }

        
        public void FinDiaIniciar(T_MODCUACC MODCUACC, T_CCIRLLVR CCIRLLVR, IList<UI_Message> ListaMensajesError, IDatosUsuario usuario, ref string FomularioQueAbrir)
        {
            using (var tracer = new Tracer())
            {
                tracer.AddToContext("Especialista", usuario.Identificacion_IdEspecialistaImpersonado);
                tracer.AddToContext("EspOrig", usuario.Identificacion_IdEspecialistaOriginal);
                FinDia.Form_Load(MODCUACC, CCIRLLVR, ListaMensajesError, uow, usuario, ref FomularioQueAbrir);
            }            
        }

        public void GetListaAceptacionesVencidas(string centroCosto, string especialista, T_CCIRLLVR CC, IList<UI_Message> listaErrores)
        {
            CCIRLLVR.SyGet_jAcp2(centroCosto, especialista, CC, listaErrores, uow);
        }

        public string GetArchivoAceptacionesVencidas(T_CCIRLLVR CC)
        {
            return FRMACPV.Imp_jAcp(CC);
        }

        public void btnBajarDatos_Click(T_MODGUSR modgusr, IList<UI_Message> listaMensajes, T_MODFDIA modfdia)
        {
            FinDia.BajarDatos_Click(modgusr, listaMensajes, modfdia, uow);
        }

        public void btnBajarDatos_Click_2(T_MODGUSR modgusr, IList<UI_Message> listaMensajes, T_MODFDIA modfdia, T_MODCUACC modcuacc)
        {
            FinDia.BajarDatos_Click_2(modgusr, listaMensajes, modfdia, modcuacc, uow);
        }

        public void btnBajarDatos_Click_3(T_MODGUSR modgusr, IList<UI_Message> listaMensajes, T_MODFDIA modfdia, T_MODCUACC modcuacc)
        {
            FinDia.BajarDatos_Click_3(modgusr, listaMensajes, modfdia, modcuacc, uow);
        }

        public void btnBajarDatos_Click_4(T_MODGUSR modgusr, IList<UI_Message> listaMensajes, T_MODFDIA modfdia, T_MODCUACC modcuacc)
        {
            FinDia.BajarDatos_Click_4(modgusr, listaMensajes, modfdia, modcuacc, uow);
        }

        public void BajarDatos_Click_Supervisor_1(T_MODGUSR modgusr, IList<UI_Message> listaMensajes, T_MODFDIA modfdia, T_MODCUACC modcuacc)
        {
            FinDia.BajarDatos_Click_Supervisor_1(modgusr, listaMensajes, modfdia, modcuacc, uow);
        }

        public void BajarDatos_Click_Supervisor_2(T_MODGUSR modgusr, IList<UI_Message> listaMensajes, T_MODFDIA modfdia, T_MODCUACC modcuacc)
        {
            FinDia.BajarDatos_Click_Supervisor_2(modgusr, listaMensajes, modfdia, modcuacc, uow);
        }

        public void BajarDatos_Click_Especialista_1(T_MODGUSR modgusr, IList<UI_Message> listaMensajes, T_MODFDIA modfdia, T_MODCUACC modcuacc)
        {
            FinDia.BajarDatos_Click_Especialista_1(modgusr, listaMensajes, modfdia, modcuacc, uow);
        }

        public void btnBajarDatos_Click_final(T_MODGUSR modgusr, IList<UI_Message> listaMensajes, T_MODFDIA modfdia, T_MODCUACC modcuacc, bool cierreForzado)
        {
            FinDia.BajarDatos_Click_final(modgusr, listaMensajes, modfdia, modcuacc, uow, cierreForzado);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="opcion"></param>
        /// <param name="globales"></param>
        /// <param name="listaMensajes"></param>
        /// <returns></returns>
        public string TotalesContables(int opcion, DatosGlobales globales, List<UI_Message> listaMensajes)
        {
            ReporteMonedasDTO datosReporte;
            Printer printer = null;

            switch (opcion)
            {
                case 0:
                    // Cuadra Cuentas Contables por moneda.- TODO:estanislao: revisar
                    datosReporte = MODFDIA.SyGet_CtaMnd(0, globales.MODGUSR.UsrEsp.CentroCosto,
                         globales.MODGUSR.UsrEsp.Especialista, this.uow, listaMensajes);

                    printer = MODFDIA.Lst_CtaMnd(true, false, globales.MODGUSR, datosReporte.SumaCuentas, datosReporte.TipoCuentas);
                    if (datosReporte == null)
                    {
                        listaMensajes.Add(new UI_Message
                        {
                            Text = "Existe una descuadratura entre las cuentas contables utilizadas el dia de hoy.",
                            Type = TipoMensaje.Error,
                            Title = "Fin de día"
                        });
                    }
                    break;
                case 1:
                    // Cuadra Cuentas Contables por moneda.-
                    datosReporte = MODFDIA.SyGet_CtaMnd(1, globales.MODGUSR.UsrEsp.CentroCosto,
                         globales.MODGUSR.UsrEsp.Especialista, this.uow, listaMensajes);
                    if (datosReporte != null)
        {
                        printer = MODFDIA.Lst_CtaMnd(true, true, globales.MODGUSR, datosReporte.SumaCuentas, datosReporte.TipoCuentas);
                    }
                    break;
            }

            return printer.ToString();
        }

        public IList<sce_mch_s14_MS_DTO> GetContabilidadVigente(string CentroCosto, string Especialidad)
        {
            return uow.GetContabilidadVigente(CentroCosto, Especialidad);           
        }

        public bool ValidacionCuadraturaSwift(DatosGlobales Globales)
        {
            string rutOri = Globales.MODGUSR.UsrEsp.Rut;
            DateTime fecha = DateTime.Today;//DateTime.Parse(DateTime.Now.ToString("dd-mm-yyyy"));
            decimal rut = decimal.Parse(rutOri.Substring(0, rutOri.Length-1));
            int rutSwift = int.Parse(rutOri.Substring(0, rutOri.Length - 1));
            DateTime fechaSwift = DateTime.Today; //DateTime.Parse(DateTime.Now.ToString("dd-mm-yyyy"));

            return MODCUAD.ValidacionCuadraturaSwift(rutSwift, fechaSwift, rut, fecha, uow, uows, Globales);
          
        }

        public IList<No_Ele> GetElementosCuadratura(DatosGlobales Globales)
        {
            return Globales.MODCUAD.Elementos;
        }
        public int EliminaMT(string codcct, string codesp, string codofi, string codope, string codpro, string glosa_estado, string mensaje_error, decimal? ncorr, string referencia, string tipo_mt, string tipo_mt_decimal)
        {
            return uow.EliminaMT( codcct,  codesp,  codofi,  codope,  codpro,  glosa_estado,  mensaje_error,  ncorr,  referencia,  tipo_mt, tipo_mt_decimal);

        }
        public IList<sce_usr_s16_MS_DTO> GetPassword()
        {
            return uow.GetPassword();
        }
        public bool Set_MODCUACC_true(DatosGlobales Globales)
        {

            Globales.MODCUACC.ABONO_VB = true;
            return true;

        }
        public bool Get_MODCUACC_ABONO_VB(DatosGlobales Globales)
        {

            return Globales.MODCUACC.ABONO_VB;

        }
        public bool Set_MODCUACC_false(DatosGlobales Globales)
        {

            Globales.MODCUACC.ABONO_VB = false;
            return true;

        }

        public IList<T_ConCCLin> GetElementosSce_cuadra_inyecciones_ctacte_MS(DatosGlobales Globales)
        {
            return Globales.MODCUACC.VConCCLin;
        }

        public IList<T_ConCCLin> Sce_mcd_s71(DatosGlobales Globales)
        {
            return Globales.MODCUACC.VConCCLin2;
        }
        #region FRMCLAVE
        public string GetPassword(string cencos, string codusr)
        {
            return uow.SceRepository.sce_usr_s16_MS(cencos, codusr);
        }

        #endregion

        #region frmRut
        public void btnAceptar_Confirmacion(string dato, FormMostrar tipo, T_MODCUACC modcuacc, T_MODFDIA modfdia, IList<UI_Message> ListaMensajes, T_MODGUSR modgusr)
        {
            if (tipo == FormMostrar.frmRut)
            {//RUT
                FrmRut.BAceptar_Click(dato, modcuacc, modfdia, ListaMensajes);
            }
            else if (tipo == FormMostrar.frmClave)
            {
                //clave
                //Revisar si debe manenerce esto aqui
                string claSup = "";
                string id_Sup = modgusr.UsrEsp.Id_Super;
                if(id_Sup == "00")
                {
                    claSup = GetPassword(modgusr.UsrEsp.CentroCosto, modgusr.UsrEsp.Especialista);
                }
                else
                {
                    claSup = GetPassword(modgusr.UsrEsp.CentroCosto, id_Sup);
                }


                if (!dato.Equals(claSup))
                {
                    ListaMensajes.Add(new UI_Message() { Text = "Clave No Corresponde a la del Supervisor"});
                    return;
                }
                else
                {
                    modcuacc.ABONO_VB = true;
                }
                
            }
        }
        #endregion

        public string imprimirReporteDescuadratura(T_MODCUACC modcuacc)
        {
            string impresion = "";
            Printer printer = new Printer();
            imprime(modcuacc, printer);
            impresion = printer.ToString();
            return impresion;
        }

        #region FrmResDesc
        public void imprime(T_MODCUACC modcuacc, Printer printer)
        {
            string s = "";
            string paso5 = "";
            string paso4 = "";
            string paso3 = "";
            string paso2 = "";
            string paso1 = "";
            int i = 0;
            bool esta = false;
            int pasa = 0;
            int nrolin = 0;
            int nropag = 0;
            nropag = 1;
            nrolin = 0;
            Imprime_Header(printer);
            //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
            //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeSize(MigrationSupport.Printer.DefInstance.Font, 10);
            //MigrationSupport.Printer.DefInstance.Font = new Font("Courier New", 9F);
            pasa = 1;


            esta = false;

            for (i = 0; i < modcuacc.VConCCLin.Count; i++)
            {

                if (!bool.Parse(modcuacc.VConCCLin[i].cuadra.ToString()))
                {
                    //  Titulo Contabilidad
                    if (modcuacc.VConCCLin.Count > 0 && !esta)
                    {
                        printer.Print(" Contabilidad Descuadrada ");
                        esta = true;
                    }

                    paso1 = modcuacc.VConCCLin[i].codcct.Trim() + "-" + modcuacc.VConCCLin[i].codpro.Trim() + "-" + modcuacc.VConCCLin[i].codesp.Trim() + "-" + modcuacc.VConCCLin[i].codofi.Trim() + "-" + modcuacc.VConCCLin[i].codope.Trim();
                    paso2 = modcuacc.VConCCLin[i].numcct.Trim();
                    if (paso2.Length > 32)
                    {
                        paso2 = paso2.Substring(0, 32);//paso2.Mid(1, 32);
                    }

                    if (modcuacc.VConCCLin[i].cod_dh.Trim() == "D")
                    {
                        paso3 = "Cargo ";
                    }
                    else
                    {
                        paso3 = "Abono ";
                    }
                    paso4 = modcuacc.VConCCLin[i].nemmon.Trim();
                    paso5 = modcuacc.VConCCLin[i].mtomcd.ToString().Trim();
                    if (paso4 == "$")
                    {
                        s = paso1 + Printer.TAB() + paso2 + Printer.TAB() + Printer.TAB() + Printer.TAB() + paso3 + Printer.TAB() + paso4.PadLeft(3, ' ') + Printer.TAB() + string.Format("#,###0", paso5.Trim()).PadLeft(18, ' ');//MODGPYF1.PoneChar(MigrationSupport.Utils.Format(paso5.TrimB(), "#,###0"), " ", "H", 18);
                    }
                    else if (paso4 == "US$")
                    {
                        s = paso1 + Printer.TAB() + paso2 + Printer.TAB() + Printer.TAB() + Printer.TAB() + paso3 + Printer.TAB() + paso4.PadLeft(3, ' ') + Printer.TAB() + string.Format("#,###0.00", paso5.Trim()).PadLeft(18, ' ');//MODGPYF1.PoneChar(MigrationSupport.Utils.Format(paso5.TrimB(), "#,###0.00"), " ", "H", 18);
                    }
                    else
                    {
                        s = paso1 + Printer.TAB() + paso2 + Printer.TAB() + Printer.TAB() + Printer.TAB() + paso3 + Printer.TAB() + string.Empty.PadLeft(3, ' ') + Printer.TAB() + string.Format("#,###0.00", paso5.Trim()).PadLeft(18, ' '); //MODGPYF1.PoneChar(MigrationSupport.Utils.Format(paso5.TrimB(), "#,###0.00"), " ", "H", 18);
                    }
                    printer.Print(s);
                }
            }


            esta = false;

            for (i = 0; i < modcuacc.TTraSTB.Count; i++)
            {

                if (!bool.Parse(modcuacc.TTraSTB[i].cuadra.ToString()))
                {

                    //  Titulo STB
                    if (modcuacc.TTraSTB.Count > 0 && !esta)
                    {
                        printer.Print(" ");
                        printer.Print(" Cuenta Corriente en Linea Descuadrada");
                        esta = true;
                    }

                    paso1 = "Líder: " + modcuacc.TTraSTB[i].codcct.Trim() + "-" + modcuacc.TTraSTB[i].codesp.Trim() + "         ";
                    paso2 = modcuacc.TTraSTB[i].rutesp.Trim();
                    if (paso2.Length > 32)
                    {
                        paso2 = paso2.Substring(0, 32);//paso2.Mid(1, 32);
                    }

                    if (modcuacc.TTraSTB[i].cod_dh.Trim() == "D")
                    {
                        paso3 = "Cargo ";
                    }
                    else
                    {
                        paso3 = "Abono ";
                    }
                    if (modcuacc.TTraSTB[i].codtra == "0766" || modcuacc.TTraSTB[i].codtra == "0666")
                    {
                        paso4 = "$";
                    }
                    else if (modcuacc.TTraSTB[i].codtra == "7766" || modcuacc.TTraSTB[i].codtra == "7666")
                    {
                        paso4 = "US$";
                    }
                    else
                    {
                        paso4 = "";
                    }
                    //         paso4$ = Trim$(VCCResSTB(i%).nemmon)
                    paso5 = modcuacc.TTraSTB[i].mtotra.Trim();
                    if (paso4 == "$")
                    {
                        s = paso1 + Printer.TAB() + paso2 + Printer.TAB() + Printer.TAB() + Printer.TAB() + paso3 + Printer.TAB() + paso4.PadLeft(3, ' ') + Printer.TAB() + string.Format("#,###0", int.Parse(paso5.Trim())/100).PadLeft(18, ' ');//MODGPYF1.PoneChar(MigrationSupport.Utils.Format((paso5.TrimB().ToInt() / 100).ToStr(), "#,###0"), " ", "H", 18);
                    }
                    else if (paso4 == "US$")
                    {
                        s = paso1 + Printer.TAB() + paso2 + Printer.TAB() + Printer.TAB() + Printer.TAB() + paso3 + Printer.TAB() + paso4.PadLeft(3, ' ') + Printer.TAB() + string.Format("#,###0.00", int.Parse(paso5.Trim()) / 100).PadLeft(18, ' ');// MODGPYF1.PoneChar(MigrationSupport.Utils.Format((paso5.TrimB().ToInt() / 100).ToStr(), "#,###0.00"), " ", "H", 18);
                    }
                    else
                    {
                        s = paso1 + Printer.TAB() + paso2 + Printer.TAB() + Printer.TAB() + Printer.TAB() + paso3 + Printer.TAB() + string.Empty.PadLeft(3, ' ') + Printer.TAB() + string.Format("#,###0.00", int.Parse(paso5.Trim()) / 100).PadLeft(18, ' ');// MODGPYF1.PoneChar(MigrationSupport.Utils.Format((paso5.TrimB().ToInt() / 100).ToStr(), "#,###0.00"), " ", "H", 18);
                    }
                    printer.Print(s);

                }

            }

            printer.Print();
            printer.Print();
            //printer.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, true);
            //printer.Font = MigrationSupport.Utils.FontChangeSize(MigrationSupport.Printer.DefInstance.Font, 11);
            printer.Print();
            //    Printer.Print Tab(23); "Total de Movimientos por Inyectar : " + Str$(j%)
            //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
            printer.EndDoc();

        }
        public void Imprime_Header(Printer printer)
        {
            int nropag = 0;
            //MigrationSupport.Printer.DefInstance.Font = new Font("Arial", 9F);
            //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(MigrationSupport.Printer.DefInstance.Font, false);
            //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
            //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(MigrationSupport.Printer.DefInstance.Font, false);
            printer.Print();
            SaltoPagina(printer);
            printer.Print();
            SaltoPagina(printer);
            //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeSize(MigrationSupport.Printer.DefInstance.Font, 17);
            //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, true);
            printer.PrintList(Printer.TAB(1), "Banco de Chile");
            printer.PrintWithoutCrlf(Printer.TAB(1), "Comercio Exterior");
            //MigrationSupport.Printer.DefInstance.Font = new Font("Courier New", 9F);
            //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeSize(MigrationSupport.Printer.DefInstance.Font, 11);
            printer.PrintList(new object[] { Printer.TAB(70), "Fecha : " + DateTime.Now.ToShortDateString() });
            SaltoPagina(printer);
            printer.PrintList(new object[] { Printer.TAB(70), "Pag   : " + nropag.ToString().PadRight(3,' ') });
            SaltoPagina(printer);

            printer.Print();
            SaltoPagina(printer);
            printer.Print();
            SaltoPagina(printer);
            printer.PrintList(new object[] { Printer.TAB(35), "Descuadratura" });
            SaltoPagina(printer);
            printer.PrintList(new object[] { Printer.TAB(28), "Contabilidad/Cta. Cte. en Línea" });
            SaltoPagina(printer);
            printer.Print();
            SaltoPagina(printer);
            printer.Print();
            SaltoPagina(printer);
            printer.Print();
            SaltoPagina(printer);
            printer.Print();
            SaltoPagina(printer);
            printer.PrintList(new object[] { Printer.TAB(1), "---------------------------------------------------------------------------------------------------------------------------------------------------" });
            SaltoPagina(printer);
            printer.PrintWithoutCrlf("# Operación", Printer.TAB(25), "Nº Cta.Cte/Rut", Printer.TAB(59), "C/A", Printer.TAB(65), "Moneda", Printer.TAB(85), "Monto");
            printer.Print();
            SaltoPagina(printer);
            printer.PrintList(new object[] { Printer.TAB(1), "---------------------------------------------------------------------------------------------------------------------------------------------------" });
            SaltoPagina(printer);
            printer.Print();
            SaltoPagina(printer);

        }

        public void SaltoPagina(Printer printer)
        {
            int nropag = 0;
            int nrolin = 0;

            if (nrolin >= 55)
            {
                nropag = nropag + 1;
                nrolin = 0;
                printer.NewPage();
                Imprime_Header(printer);
            }
            else
            {
                nrolin = nrolin + 1;
            }
        }
        #endregion
    }
}
