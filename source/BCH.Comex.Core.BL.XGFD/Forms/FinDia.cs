using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XGFD.Modulos;
using BCH.Comex.Core.Entities.Cext01.FinDia;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XGFD.Forms
{
    public static class FinDia
    {
        public static void Form_Load(T_MODCUACC MODCUACC, T_CCIRLLVR TCCIRLLVR, IList<UI_Message> ListaMensajesError, UnitOfWorkCext01 uow, IDatosUsuario usuario, ref string FomularioQueAbrir)
        {
            int res = 0;
            string s = "";
            int retais = 0;
            int a = 0;

            //retais = MODFDIA.AISGetRutUsr(modcuacc.RutwAis);
            MODCUACC.RutwAis = usuario.Identificacion_Rut;

            // Titulo.-
            //Titulo.Text = "Usuario         Nombre                                 Lider            Sección                           Ciudad";

            // Carga el Usuario en la lista.-
            //s = MODGUSR.UsrEsp.CentroCosto + "-" + MODGUSR.UsrEsp.Especialista + 9.Char() + MODGUSR.UsrEsp.nombre + 9.Char() + MODGUSR.UsrEsp.CostoSuper + "-" + MODGUSR.UsrEsp.Id_Super + 9.Char() + MODGUSR.UsrEsp.Seccion + 9.Char() + MODGUSR.UsrEsp.Ciudad;
            //Lista.Items.Add(s);
            //Lista.SelectedIndex = 0;

            //// Verifica Usuario activo.-
            //if (Lista.Items.Count == 0)
            //{
            //    MigrationSupport.Utils.MsgBox("No se puede realizar el Proceso de Fin de Día debido a que no se ha encontrado el Usuario activo. Reporte este problema.", MODGPYF0.pito(48).Cast<MigrationSupport.MsgBoxStyle>(), MODFDIA.MsgFDia);
            //    Environment.Exit(0);
            //}

            // *******************************
            //   C.C.I.  José Luis
            // *******************************

            if (CCIRLLVR.SyGet_jAcp(usuario.Identificacion_CentroDeCostosOriginal, usuario.Identificacion_IdEspecialistaOriginal, ListaMensajesError, uow) > 0)
            {
                ListaMensajesError.Add(new UI_Message() { Text = "No se puede realizar el Proceso de Fin de Día debido a que se han encontrado aceptaciones vencidas que no se han resuelto.  Para ello utilice Módulo de Colocaciones botón Rollover.", Type = TipoMensaje.Informacion });

                //SE MUEVE AL INICIO DE LA SIGUIENTE PANTALLA
                //res = CCIRLLVR.SyGet_jAcp2(usuario.Identificacion_CentroDeCostosOriginal, usuario.Identificacion_IdEspecialistaOriginal, TCCIRLLVR, ListaMensajesError, uow);

                FomularioQueAbrir = "Aceptacion";
            }

            // *******************************
        }

        public static void BajarDatos_Click(T_MODGUSR modGUsr, IList<UI_Message> ListaMensajes, T_MODFDIA modfdia, UnitOfWorkCext01 uow)
        {
            int i = 0;
            int n = 0;
            bool X = false;

            // desactivar todos aquello que pueda generar procesos

            // Valida que ciertas cuentas ctes. mn estén bien formateados.-
            X = MODFDIA.SyGet_CtaCteMN(modGUsr.UsrEsp.CentroCosto, modGUsr.UsrEsp.Especialista, DateTime.Now.Date, modfdia, ListaMensajes, uow);
            //SI HUBO ERROR EN LA LLAMADA A LA FUNCION SE TERMINA EL PROCESO PARA MOSTRAR EL ERROR
            if (!X)
            {
                return;
            }
            // Despliega los Reportes con problemas.-
            n = modfdia.VBcoFin.Count;
            if (n > 0)
            {
                ListaMensajes.Add(new UI_Message()
                {
                    Text = "Existe(n) " + n.ToString("00") + " Reporte(s) Contable(s) cuyo(s) dato(s) acerca de la Cuenta Corriente está erróneo. Presione Enter para listar el o los Reportes con Problemas.",
                    Type = TipoMensaje.Warning
                });
                for (i = 0; i < n; i++)
                {
                    ListaMensajes.Add(new UI_Message() { Text = "Reporte " + modfdia.VBcoFin[i], Type = TipoMensaje.Warning });
                }

                ListaMensajes.Add(new UI_Message() { Text = "El Proceso de Fin de Día ha sido cancelado. Reporte este problema.", Type = TipoMensaje.Error });

                return;
            }

            //// Valida que ciertas cuentas tengan información de los Bancos.-
            X = MODFDIA.SyGet_CtaBco(modGUsr.UsrEsp.CentroCosto, modGUsr.UsrEsp.Especialista, DateTime.Now.Date, modfdia, ListaMensajes, uow);
            if (!X)
            {
                return;
            }
            // Despliega los Reportes con problemas.-
            n = modfdia.VBcoFin.Count;
            if (n > 0)
            {
                ListaMensajes.Add(new UI_Message() 
                                    { 
                                        Text = "Existe(n) " + n.ToString("00") + " Reporte(s) Contable(s) cuyo(s) dato(s) acerca del Código de Banco está nulo o erróneo. Presione Enter para listar el o los Reportes con Problemas.",
                                        Type = TipoMensaje.Warning 
                                    });
                for (i = 0; i < n; i++)
                {
                    ListaMensajes.Add(new UI_Message() { Text = "Reporte " + modfdia.VBcoFin[i], Type = TipoMensaje.Warning });
                }
                ListaMensajes.Add(new UI_Message() { Text = "El Proceso de Fin de Día ha sido cancelado. Reporte este problema.", Type = TipoMensaje.Error });
                return;
            }


            X = MODFDIA.SyGet_CtaSuc(modGUsr.UsrEsp.CentroCosto, modGUsr.UsrEsp.Especialista, DateTime.Now.Date, modfdia, ListaMensajes, uow);
            if (!X)
            {
                return;
            }
            // Despliega los Reportes con problemas.-
            n = modfdia.VBcoFin.Count;
            if (n > 0)
            {
                ListaMensajes.Add(new UI_Message() { Text = "Existe(n) " + n.ToString("00") + " Reporte(s) Contable(s) cuya(s) oficina de origen y destino son iguales en la cuenta saldo con sucursal. Presione Enter para listar el o los Reportes con Problemas.",
                    Type = TipoMensaje.Warning
                });
                
                for (i = 0; i < n; i++)
                {
                    ListaMensajes.Add(new UI_Message() { Text = "Reporte " + modfdia.VBcoFin[i], Type = TipoMensaje.Warning });
                }
                ListaMensajes.Add(new UI_Message() { Text = "El Proceso de Fin de Día ha sido cancelado. Reporte este problema.", Type = TipoMensaje.Error });
                return;
            }

        }

        public static void BajarDatos_Click_2(T_MODGUSR modGUsr, IList<UI_Message> ListaMensajes, T_MODFDIA modfdia, T_MODCUACC modCuaCC, UnitOfWorkCext01 uow)
        {
            int i = 0;
            int n = 0;
            bool X = false;

            X = MODFDIA.SyGet_ContabOK(modGUsr.UsrEsp.CentroCosto, modGUsr.UsrEsp.Especialista, DateTime.Now.Date, modfdia, ListaMensajes, uow);
            if (!X)
            {
                return;
            }
            // Despliega los Reportes con problemas.-
            n = modfdia.VCtb.Count;
            if (n > 0)
            {
                ListaMensajes.Add(new UI_Message() { Text = "Existe(n) " + n.ToString("00") + " Movimiento(s) Contable(s) cuyo(s) dato(s) contables está(n) con error(es). Presione Enter para listar el o los Reportes con Problemas.", Type = TipoMensaje.Warning });
                for (i = 0; i < n; i += 1)
                {
                    ListaMensajes.Add(new UI_Message() { Text = modfdia.VCtb[i], Type = TipoMensaje.Warning });
                }
                ListaMensajes.Add(new UI_Message() { Text = "El Proceso de Fin de Día ha sido cancelado. Reporte este problema.", Type = TipoMensaje.Error });
                return;

            }

            //// Valida que no exista contabilidad vigente con fecha distinta a hoy.-
            X = MODFDIA.SyGet_ContabVig(modGUsr.UsrEsp.CentroCosto, modGUsr.UsrEsp.Especialista, modfdia, ListaMensajes, uow);
            if (!X)
            {
                return;
            }
            // Despliega los Reportes con problemas.-
            n = modfdia.DetallesReportes.Count;
            if (n > 0)
            {
                ReporteProblemas rp = new ReporteProblemas()
                {
                    Titulo = "REPORTES CONTABLES",
                    Mensaje = "ATENCION !!!. Existe(n) " + n.ToString("00") + " Reporte(s) Contable(s) vigentes cuya(s) fecha(s) contable(s) es(son) distinta(s) a la de hoy. Verifique si la Contabilidad de estas operaciones deben tener la fecha indicada. Si hay alguna que deba tener otra fecha, avisar a la Unidad de Control.",
                    DetallesProblemas = modfdia.DetallesReportes,
                    listaConfirmacion = new UI_Message() { Text = "Para aceptar las operaciones anteriormente listadas, Ud. deberá ingresar su rut como responsable ante cualquier problema posterior. Desea continuar el Proceso de Fin de Día ?", Type = TipoMensaje.YesNo },
                    formMostrarConfirmacion = FormMostrar.frmRut
                };
                modfdia.ListaReportes.Add(rp);
                return;
            }

        }
        public static void BajarDatos_Click_3(T_MODGUSR modGUsr, IList<UI_Message> ListaMensajes, T_MODFDIA modfdia, T_MODCUACC modCuaCC, UnitOfWorkCext01 uow)
        {
            int n = 0;
            bool p = false;
            bool X = false;

            p = modfdia.ChkImpresionListado;
            //// Valida que no exista contabilidad vigente con fecha de hoy
            //// para las operaciones de CBS.-
            

            //// 
            //// Valida que no exista contabilidad vigente con fecha distinta a la de hoy
            //// para las operaciones que no son del CBS.-
            X = MODFDIA.SyGet_ContabNoDiaCBS(modGUsr.UsrEsp.CentroCosto, modGUsr.UsrEsp.Especialista, modfdia, ListaMensajes, uow);
            if (!X)
            {
                return;
            }

            // Despliega los Reportes con problemas.-
            n = modfdia.DetallesReportes.Count;
            if (n > 0)
            {
                ReporteProblemas rp = new ReporteProblemas()
                {
                    Titulo = "REPORTES CONTABLES ERRONEOS",
                    Mensaje = "ERROR !!!. Existe(n) " + n.ToString("00") + " Reporte(s) Contable(s) erróneos. Esta(s) operación(es) debiera(n) tener la fecha de hoy. " + "Avise a la Brevedad a la Unidad de Control.",
                    DetallesProblemas = modfdia.DetallesReportes,
                    formMostrarConfirmacion = FormMostrar.sinForm
                };
                modfdia.ListaReportes.Add(rp);

                return;
            }

            // Cuadra Cuentas Contables por moneda.-
            var reporteMonedas = MODFDIA.SyGet_CtaMnd(modGUsr.UsrEsp.Jerarquia, modGUsr.UsrEsp.CentroCosto, modGUsr.UsrEsp.Especialista, uow,
                ListaMensajes);

            if (p)
            {
                //TODO: Mostrar el reporte (en un formularion con printer.ToString()
                BCH.Comex.Common.Printer printer = MODFDIA.Lst_CtaMnd(true, false, modGUsr, reporteMonedas.SumaCuentas,
                    reporteMonedas.TipoCuentas);
                modfdia.impresionListado = printer.ToString();
            }

            T_MODGPLN modgpln = new T_MODGPLN();
            X = MODGPLN.SyGetn_PlnConv(modGUsr.UsrEsp.CentroCosto, modGUsr.UsrEsp.Especialista, DateTime.Today, modgpln, ListaMensajes,uow);
            for (int i = 0; i < modgpln.VPlnCon.Count; i++)
            {
                if (modgpln.VPlnCon[i].DifDeb != 0 || modgpln.VPlnCon[i].DifHab != 0)
                {
                    ListaMensajes.Add(
                        new UI_Message() 
                        {
                            Text = "Se ha detectado una descuadratura entre Conversion v/s Planilla. Revise sus reportes e informe a Sistemas debido a que el Proceso de Fin de Día será cancelado.",
                            Type = TipoMensaje.Error
                        });
                    return;
                }
            }

            //// Cuadra Producto v/s Contabilidad de lo Modificado Hoy día.-

            //// Verifica cuentas puentes pegadas.-
            int cuentasCriticasEnError = 0;
            int cuentasNoCriticasEnError = 0;

            MODFDIA.Fn_CtaCrit(reporteMonedas.TipoCuentas, ListaMensajes, out cuentasCriticasEnError, out cuentasNoCriticasEnError);
            
            if (cuentasCriticasEnError > 0)
            {
                ListaMensajes.Add(new UI_Message() { Text = "El Proceso de Fin de Día ha sido cancelado debido a que existen cuentas Puentes importantes que NO ha sido canceladas.", Type = TipoMensaje.Error });
            }
            else if (cuentasNoCriticasEnError > 0)
            {
                //solo si no hay cuentas criticas descuadradas, es que le doy la opcion de continuar si hay cuentas no criticas descuadradas
                modfdia.ListaConfirmaciones.Add(
                    new Confirmaciones() 
                    { 
                        formMostrarConfirmacion = FormMostrar.sinForm, 
                        Confirmacion = new UI_Message() 
                                        { 
                                            Text = "El Proceso de Fin de Día ha encontrado cuentas puentes que no han sido canceladas. ¿ Desea seguir realizando el Proceso de Fin de Día ?" 
                                        } 
                    });
            }
        }


        public static void BajarDatos_Click_4(T_MODGUSR modGUsr, IList<UI_Message> ListaMensajes, T_MODFDIA modfdia, T_MODCUACC modCuaCC, UnitOfWorkCext01 uow)
        {
            bool H = false;

            //// 
            ////  D.S.B.
            //// 
            //// PACP
            H = MODFDIA.Fn_ValidaPlan(modGUsr.UsrEsp.CentroCosto, modGUsr.UsrEsp.Especialista, DateTime.Now.Date, modfdia, ListaMensajes, uow);
            if (modfdia.Est_Pla.Count > 0)
            {
                 modfdia.ListaReportes.Add(
                    new ReporteProblemas() 
                    { 
                        Titulo = "Informe de Planillas", 
                        DetallesProblemas = modfdia.DetallesReportes, 
                        tipoReporte = TipoReporte.ReportePlanillas 
                    });
                return;
            }
        }

        public static void BajarDatos_Click_Supervisor_1(T_MODGUSR modGUsr, IList<UI_Message> ListaMensajes, T_MODFDIA modfdia, T_MODCUACC modCuaCC, UnitOfWorkCext01 uow)
        {
            bool abre = false;
            bool bien = false;

            bien = MODFDIA.Chq_ContaSTB(modGUsr, modCuaCC, uow, ListaMensajes);

            if (!bien) //(!bien.ToBool() && error)
            {
                ListaMensajes.Add(new UI_Message
                {
                    Text = "El Proceso de Fin de Día ha sido cancelado. Reporte este problema.",
                    Title = "Fin de dia",
                    Type = TipoMensaje.Error
                });

                return;
            }

            // Imprime descuadraturas
            abre = false;

            if (modCuaCC.VConCCLin2.Count > 0)
            {
                abre = true;
            }

            if (abre)
            {
                //TODO
                //FrmResDesc.DefInstance.ShowDialog();
            }
        }

        public static void BajarDatos_Click_Supervisor_2(T_MODGUSR modGUsr, IList<UI_Message> ListaMensajes, T_MODFDIA modfdia, T_MODCUACC modCuaCC, UnitOfWorkCext01 uow)
        {
            bool error = false;
            bool bien = false;
            // Valida la moneda y sólo cancela Fin de día si la moneda es dolar
            error = false;

            if (modCuaCC.VConCCLin.Count(c => c.error != 0) > 0)
            {
                error = true;
            }

            // ACCENTURE 20120503
            if (modCuaCC.VConCCLin2.Count > 0)//(modcuacc.VConCCLin2.GetUpperBound(0) > 0)
            {
                error = true;
            }

            if (!error)
            {
                if (modCuaCC.TTraSTB.Count(c => c.error != 0) > 0)
                {
                    error = true;
                }
            }

            if (error) //(!bien.ToBool() && error)
            {
                ListaMensajes.Add(new UI_Message
                {
                    Text = "El Proceso de Fin de Día ha sido cancelado. Reporte este problema.",
                    Title = "Fin de dia",
                    Type = TipoMensaje.Error
                });

                return;
            }

            // Lee Todos los Especialistas del Lider
            if (modGUsr.UsrEsp.Id_Super == "00")
            {
                MODGUSR.SyGetn2_Usr(modGUsr.UsrEsp.CentroCosto, modGUsr.UsrEsp.Especialista, modGUsr, uow, ListaMensajes);

                modCuaCC.V_CYA = new List<CargoAbono>();
                foreach (var item in modGUsr.UsrLidEsp)
                {
                    modcuacc.SyGet_CargoAbono(item.CentroCosto, item.Especialista, uow, modCuaCC, ListaMensajes);
                }

                //bool tieneQueInyectar = modcuacc.Tiene_TrxMEInyectar(modCuaCC.V_CYA, modGUsr.UsrEsp.nombre);
                string tieneQueInyectar = modcuacc.Tiene_TrxMEInyectar(modCuaCC.V_CYA, modGUsr.UsrEsp.nombre);

                if (tieneQueInyectar.Length > 0) //(tiene != 0)
                {
                    modfdia.ListaConfirmaciones.Add(
                        new Confirmaciones()
                        {
                            formMostrarConfirmacion = FormMostrar.sinForm,
                            Confirmacion = new UI_Message()
                            {
                                Text = "Revise impresión, tiene movimientos a Ctas. Ctes. M/E, por inyectar en línea. Desea Forzar el Cierre."
                            }
                        });
                    modfdia.impresionPorInyectar = tieneQueInyectar;
                    return;
                }

                //if (tieneQueInyectar.Length > 0) //(tiene != 0)
                //{
                //    ListaMensajes.Add(new UI_Message
                //    {
                //        Text = "El Proceso de Fin de Día ha sido cancelado. Reporte este problema.",
                //        Title = "Fin de dia",
                //        Type = TipoMensaje.Error
                //    });

                //    return;
                //}

            }
        }

        public static void BajarDatos_Click_Especialista_1(T_MODGUSR modGUsr, IList<UI_Message> ListaMensajes, T_MODFDIA modfdia, T_MODCUACC modCuaCC, UnitOfWorkCext01 uow)
        {

            //  Es especialista
            modCuaCC.V_CYA = new List<CargoAbono>();
            modcuacc.SyGet_CargoAbono(modGUsr.UsrEsp.CentroCosto, modGUsr.UsrEsp.Especialista, uow, modCuaCC, ListaMensajes);


            //bool tieneQueInyectar = modcuacc.Tiene_TrxMEInyectar(modCuaCC.V_CYA, modGUsr.UsrEsp.nombre);
            string tieneQueInyectar = modcuacc.Tiene_TrxMEInyectar(modCuaCC.V_CYA, modGUsr.UsrEsp.nombre);

            if (tieneQueInyectar.Length > 0) //(tiene != 0)
            {
                modfdia.ListaConfirmaciones.Add(
                    new Confirmaciones() 
                    { 
                        formMostrarConfirmacion = FormMostrar.frmClave, 
                        Confirmacion = new UI_Message() 
                                        { 
                                            Text = "Tiene Movimientos a Ctas. Ctes. M/E por inyectar en línea, revise el listado impreso. Desea Forzar Cierre." 
                                        } 
                    });
                modfdia.impresionPorInyectar = tieneQueInyectar;
                return;
            }
        }

        public static void BajarDatos_Click_final(T_MODGUSR modGUsr, IList<UI_Message> ListaMensajes, T_MODFDIA modfdia, T_MODCUACC modCuaCC, UnitOfWorkCext01 uow, bool cierreForzado)
        {
            // Habilitamos el Log
            using (Tracer tracer = new Tracer())
            {
                string forzado = cierreForzado ? " Forzado" : string.Empty;
                tracer.TraceInformation(string.Format("Inicio Proceso Cierre Diario{0}.", forzado));
                // indicamos cual es el centro de costo y especialista que se esta cerrando
                tracer.AddToContext("codcct", modGUsr.UsrEsp.CentroCosto);
                tracer.AddToContext("codesp", modGUsr.UsrEsp.Especialista);
                // indicamos el centro de costo y especialista que cerró realmente el dia
                tracer.AddToContext("codcctori", modGUsr.UsrEsp.CCtOrig);
                tracer.AddToContext("codespori", modGUsr.UsrEsp.EspOrig);

                // Se marca el Proceso de Fin de Dia.-
                bool hayError = MODGUSR1.SyUpd_Usr(modGUsr.UsrEsp.CentroCosto, modGUsr.UsrEsp.Especialista, "F", uow, ListaMensajes);

                if (!hayError)
                {
                    tracer.TraceInformation(string.Format("El Proceso de Fin de Día{0} ha terminado exitosamente.", forzado));
                    ListaMensajes.Add(new UI_Message() { Text = "El Proceso de Fin de Día ha terminado exitosamente.", Type = TipoMensaje.Correcto });
                }
                else
                {
                    tracer.TraceInformation(string.Format("El Proceso de Fin de Día{0} NO ha terminado exitosamente. Reporte este problema.", forzado));
                    ListaMensajes.Add(new UI_Message() { Text = "El Proceso de Fin de Día NO ha terminado exitosamente. Reporte este problema", Type = TipoMensaje.Error });
                }
            }
        }

    }
}
