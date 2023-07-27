using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.SWI200;
using BCH.Comex.Core.BL.SWI300;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Swift;
using CodeArchitects.VB6Library;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODSWENN
    {
        public static short RowCount;
        public static T_MODSWENN GetMODSWENN()
        {
            return new T_MODSWENN();
        }

        public static short Fn_BorraSwiCo(UnitOfWorkCext01 unit, InitializationObject initObj, string OpeSin, dynamic numneg, dynamic tippro, dynamic NuCorr, dynamic numcuo, short NumCob, int Correlativo)
        {
            short _retValue = 0;
            #region Comentado
            //string Que = "";
            //string R = "";
            //try
            //{

            //var result = unit.SceRepository.sce_mts_d01_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 1, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 4, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 6, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 8, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 11, 5)));

            //    Que = "";
            //    Que = Que + "Exec " + Mdl_SRM.ParamSrm8k.Base + "." + Mdl_SRM.ParamSrm8k.Usuario + "." + "sce_mts_d01_MS ";
            //    Que = VB6Helpers.LCase(Que);
            //    Que = Que + MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 1, 3)) + " , ";
            //    Que = Que + MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 4, 2)) + " , ";
            //    Que = Que + MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 6, 2)) + " , ";
            //    Que = Que + MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 8, 3)) + " , ";
            //    Que = Que + MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 11, 5)) + " , ";
            //    Que = Que + MODGSYB.dbnumesy(numneg) + " , ";
            //    Que = Que + MODGSYB.dbnumesy(tippro) + " , ";
            //    Que = Que + MODGSYB.dbnumesy(NuCorr) + " , ";
            //    Que = Que + MODGSYB.dbnumesy(numcuo) + " , ";
            //    Que = Que + MODGSYB.dbnumesy(NumCob) + " , ";
            //    Que += MODGSYB.dbnumesy(Correlativo);

            //    //Se ejecuta el Procedimiento Almacenado.
            //    R = Mdl_SRM.RespuestaQuery(ref Que);
            //    if (Mdl_SRM.HayErr_Com(R) != 0)
            //    {
            //        VB6Helpers.MsgBox("Se ha producido un error de Comunicación al tratar de Borrar Información del Swift en la Base Cext01. El Servidor reporta : [" + VB6Helpers.Left(VB6Helpers.Trim(Mdl_SRM.ParamSrm8k.mensaje.Value), 100) + "]. Reporte este problema.", MsgBoxStyle.Information, "SWIFT");
            //        goto Fn_BorraSwiCoEnd;
            //    }

            //    _retValue = (short)(true ? -1 : 0);

            //Fn_BorraSwiCoEnd:
            //return _retValue;
            //}
            //catch (Exception _ex)
            //{
            //    // IGNORED: Fn_BorraSwiCoErr:
            //    VB6Helpers.SetError(_ex);
            //    VB6Helpers.MsgBox("[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number), MsgBoxStyle.Information, "Activación de Registros");
            //    // UPGRADE_ISSUE (#04B8): The Resume keyword has been converted to a goto statement
            //    goto Fn_BorraSwiCoEnd;
            //}
            #endregion
            using (Tracer tracer = new Tracer("Fn_BorraSwiCo"))
            {
                try
                {
                    tracer.TraceInformation("Datos para el SP sce_mts_d01_MS: NroOP: {0}, numneg: {1}, tippro: {2}, NuCorr: {3} Numcuo: {4}, NumCob: {5}, MensajeID: {6}", OpeSin, numneg, tippro, NuCorr, numcuo, NumCob, Correlativo);
                    int Result = unit.SceRepository.sce_mts_d01_MS(
                        OpeSin.Substring(0, 3),
                        OpeSin.Substring(3, 2),
                        OpeSin.Substring(5, 2),
                        OpeSin.Substring(7, 3),
                        OpeSin.Substring(10, 5),
                        numneg,
                        tippro,
                        NuCorr,
                        numcuo,
                        NumCob,
                        Correlativo
                        );

                    if (Result != -1)
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Se ha producido un error de Comunicación al tratar de Borrar Información del Swift en la Base Cext01. El Swift Nº " + Correlativo.ToString() + " reportar.",
                            Title = "SWIFT"
                        });
                        tracer.TraceError("Se ha producido un error de Comunicación al tratar de Borrar Información del Swift en la Base Cext01. El Swift Nº " + Correlativo.ToString() + " reportar.");
                        _retValue = 1;
                    }
                }
                catch (Exception exc)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error de Comunicación al tratar de Borrar Información del Swift en la Base Cext01. El Swift Nº " + Correlativo.ToString() + " reportar.",
                        Title = "SWIFT"
                    });
                    tracer.TraceException("Alerta, problemas al intentar anular swift Fn_AnulaSwift", exc);
                    _retValue = 1;
                }
            }
            return _retValue;
        }


        public static short Fn_AnulaSwift(UnitOfWorkSwift uow, InitializationObject initObj, string casilla, int Correl, string comentario)
        {
            short _retValue = 0;
            using (Tracer tracer = new Tracer("Anular Swift - Fn_AnulaSwift"))
            {
                try
                {
                    tracer.TraceInformation("Fn_AnulaSwift | proc_sw_env_graba_nul: {0} | {1} | {2} | {3}", Correl, int.Parse(initObj.MODSWENN.RutAis), int.Parse(casilla), comentario);
                    var result = uow.AdministracionRepository.proc_sw_env_graba_nul_MS(Correl, int.Parse(initObj.MODSWENN.RutAis), int.Parse(casilla), comentario);
                    if (result != "NUL")
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "No es posible Anular el Swift Nº " + Correl.ToString() + " desde la base SWIFT.",
                            Title = "SWIFT"
                        });
                        tracer.TraceError("No es posible Anular el Swift Nº " + Correl.ToString() + " desde la base SWIFT.");
                        _retValue = 1;
                    }
                }
                catch (Exception exc)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error al tratar de Anular el Swift Nº " + Correl.ToString() + " desde la base SWIFT. ",
                        Title = "SWIFT"
                    });
                    tracer.TraceException("Alerta, problemas al intentar anular swift Fn_AnulaSwift", exc);
                    _retValue = 1;
                }
            }

            return _retValue;
        }


        public static short Fn_GetMts(InitializationObject initObj, UnitOfWorkCext01 unit, short estado, string TipGra, string numneg, string tippro, string NumCpa, string numcuo, string NumCob)
        {

            decimal _numneg = Convert.ToDecimal(numneg);
            decimal _tippro = Convert.ToDecimal(tippro);
            decimal _NumCpa = Convert.ToDecimal(NumCpa);
            decimal _numcuo = Convert.ToDecimal(numcuo);
            decimal _NumCob = Convert.ToDecimal(NumCob);

            short _retValue = 0;

            string R = "";
            short n = 0;
            short i = 0;
            using (Tracer tracer = new Tracer("Inicia Fn_GetMts"))
            {
                try
                {
                    tracer.TraceInformation("Datos para sce_mts_s01_MS: NroOP: {0}, numneg: {1}, tippro: {2}, NumCpa: {3}, numcuo: {4}, NumCob: {5}, estado: {6}, TipGra: {7}", initObj.MODGANU.VAnu.CodOpe_t, _numneg, _tippro, _NumCpa, _numcuo, _NumCob, estado, TipGra);
                    var resultado = unit.SceRepository.sce_mts_s01_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(initObj.MODGANU.VAnu.CodOpe_t, 1, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(initObj.MODGANU.VAnu.CodOpe_t, 4, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(initObj.MODGANU.VAnu.CodOpe_t, 6, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(initObj.MODGANU.VAnu.CodOpe_t, 8, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(initObj.MODGANU.VAnu.CodOpe_t, 11, 5)), (decimal)_numneg, (decimal)_tippro, (decimal)_NumCpa, (decimal)_numcuo, (decimal)_NumCob, (decimal)estado, TipGra);

                    if (HayErr_Com(R) != 0)
                    {
                        tracer.TraceError("Se ha producido un error de Comunicación al tratar Desactivar los Swift en la Base Cext01.");
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Se ha producido un error de Comunicación al tratar Desactivar los Swift en la Base Cext01."
                        });
                        return _retValue;
                    }
                    n = (short)resultado.Count();
                    initObj.MODSWENN.VMts = new T_Mtes[n];
                    for (i = 0; i < (short)n; i++)
                    {
                        tracer.TraceInformation("{0} {1} {2}", resultado[i].fecmsg.ToShortDateString(), resultado[i].id_mensaje, resultado[i].nrorpt);
                        initObj.MODSWENN.VMts[i] = new T_Mtes()
                        {
                            fecmsg = resultado[i].fecmsg.ToShortDateString(),
                            id_mensaje = (int)resultado[i].id_mensaje,
                            NroRpt = (int)resultado[i].nrorpt
                        };
                    }
                    _retValue = (short)(true ? -1 : 0);
                    return _retValue;
                }
                catch (Exception _ex)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Alerta, problemas al obtener los swift",// "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                        Title = T_MODGCVD.MsgCVD
                    });
                    tracer.TraceException("Alerta, problemas en Fn_GetMts", _ex);
                    _retValue = 0;
                }
            }
            return _retValue;
        }

        public static short HayErr_Com(string Respuesta)
        {
            if (Respuesta == "-1")
            {
                return (short)(true ? -1 : 0);
            }
            else
            {
                return (short)(false ? -1 : 0);
            }

        }

        private static bool ObtenerHabilSwift(UnitOfWorkCext01 uow, string nombreGrupo, string elemento)
        {
            using (var tracer = new Tracer("ObtenerHabilSwift"))
            {
                try
                {
                    sce_ini_s01_MS_Result result = uow.SceRepository.sce_ini_s01_MS(nombreGrupo, elemento).FirstOrDefault();
                    if (result != null)
                    {
                        return (int.Parse(result.valor) != 0);
                    }
                    else return false;
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta", ex);

                    return false;
                }
            }
        }

        public static bool Habil_SWIFT(UnitOfWorkCext01 uow, T_MODSWENN MODSWENN, T_MODGUSR MODGUSR, string Producto1, string Producto2)
        {
            MODSWENN.gb_RutUsuario = MODGUSR.UsrEsp.Rut.Substring(0, MODGUSR.UsrEsp.Rut.Length - 1);
            MODSWENN.gb_DvUsuario = MODGUSR.UsrEsp.Rut.Substring(MODGUSR.UsrEsp.Rut.Length - 1, 1);

            return ObtenerHabilSwift(uow, "swifttodo", "0") || ObtenerHabilSwift(uow, "swiftprod", Producto1) || ObtenerHabilSwift(uow, "swiftprod", Producto2) || ObtenerHabilSwift(uow, "swiftesp", MODSWENN.gb_RutUsuario);
        }

        //Cambia el estado de un swift.-
        public static short ActivaBD_Swi(InitializationObject initObj, UnitOfWorkCext01 unit, string OpeSin, decimal numneg, decimal tippro, decimal NuCorr, decimal numcuo, short NumCob, int Correlativo)
        {
            using (var trace = new Tracer("ActivaBD_Swi: Cambia el estado de un swift"))
            {
                short _retValue = 0;
                short estado = 0;
                try
                {
                    //Genera Comando.-

                    estado = 1;
                    trace.TraceInformation("Datos para Sce_mts_u01_MS: NroOP: {0}, numneg: {1}, tippro: {2}, NuCorr: {3}, numcuo: {4}, NumCob: {5}, Correlativo: {6}, estado: {7}", OpeSin, numneg, tippro, NuCorr, numcuo, NumCob, Correlativo, estado);
                    _retValue = Mdl_Funciones_Varias.Cmd_Put_New(initObj.Mdl_Funciones_Varias, () =>
                    {
                        return (short)unit.SceRepository.Sce_mts_u01_MS(VB6Helpers.Mid(OpeSin, 1, 3),
                            VB6Helpers.Mid(OpeSin, 4, 2),
                            VB6Helpers.Mid(OpeSin, 6, 2),
                            VB6Helpers.Mid(OpeSin, 8, 3),
                            VB6Helpers.Mid(OpeSin, 11, 5),
                            numneg,
                            tippro,
                            NuCorr,
                            numcuo,
                            NumCob,
                            Correlativo,
                            estado);
                    });
                }
                catch (Exception _ex)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                        Title = "Activación de Registros"
                    });
                    trace.TraceException("Alerta", _ex);
                }
                return _retValue;
            }
        }

        public static short Fn_Save_BaseSwft(InitializationObject initObj, UnitOfWorkCext01 uow, string numneg, string tippro, string NumCpa, string numcuo, string NumCob)
        {
            short _retValue = 0;
            short l = 0;
            string TipG = "";
            string s = "";
            short TipMT = 0;

            string cuerpoSwift = "";
            string EncaSw = "";
            int? correlativo = 0;

            using (var tracer = new Tracer("Fn_Save_BaseSwft"))
            {
                try
                {
                    //Guarda el Texto del Swift
                    //Guarda el Encabezado del Swift
                    initObj.MODSWENN.RutAis = initObj.Usuario.Identificacion_Rut; //Rut del especialista
                    if (initObj.MODSWENN.Hab_Swift != 0)
                    {
                        if (initObj.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 0)
                        {
                            for (short i = 0; i < initObj.MODGSWF.VSwf.Length; i++)
                            {
                                initObj.MODGSWF.VGSwf.ProblemasAlGenerar = false;
                                cuerpoSwift = Fn_GenSwiftEnvio(initObj, uow, i);
                                correlativo = Fn_Sw_GetCorr(initObj);
                                if (!correlativo.HasValue)
                                {
                                    tracer.TraceInformation("No hay comunicación con la base de datos de Swift. El swift generado quedará grabado localmente.");

                                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Informacion,
                                        Text = "No hay comunicación con la base de datos de Swift. El swift generado quedará grabado localmente.",
                                        Title = "Error de comunicación"
                                    });
                                    //Se agrega para que adentante lo pueda usar
                                    correlativo = 0;

                                    TipG = "L";  //Grabo Local
                                    EncaSw = Fn_EncSwiLoc(initObj, i);
                                    //s = EncaSw + cuerpoSwift;
                                    //if (!Fn_SwiftLocal(initObj, uow, s))
                                    if (!Fn_SwiftPendientes(initObj, uow, cuerpoSwift, i))
                                    {
                                        tracer.TraceWarning("Se ha producido un error al grabar el Swift Localmente. El swift deberá ser digitado en la aplicación Envio de Swift.");

                                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                                        {
                                            Type = TipoMensaje.Error,
                                            Text = "Se ha producido un error al grabar el Swift Localmente. El swift deberá ser digitado en la aplicación Envio de Swift.",
                                            Title = "Error al guardar localmente."
                                        });
                                    }
                                }
                                else if (initObj.MODGSWF.VGSwf.ProblemasAlGenerar)
                                {
                                    TipG = "L";  //Grabo Local

                                    tracer.TraceInformation("Problemas al genear el swift para el envío automático. El swift generado quedará grabado localmente.");
                                    initObj.Mdi_Principal.MantenerMensajes = true;

                                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Informacion,
                                        Text = "El swift generado quedará grabado localmente.",
                                        Title = "Aviso: "
                                    });

                                    EncaSw = Fn_EncSwiLoc(initObj, i);
                                    s = EncaSw + cuerpoSwift;
                                    //if (!Fn_SwiftLocal(initObj, uow, s))
                                    if (!Fn_SwiftPendientes(initObj, uow, cuerpoSwift, i))
                                    {
                                        tracer.TraceWarning("Se ha producido un error al grabar el Swift Localmente. El swift deberá ser digitado en la aplicación Envio de Swift.");

                                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                                        {
                                            Type = TipoMensaje.Error,
                                            Text = "Se ha producido un error al grabar el Swift Localmente. El swift deberá ser digitado en la aplicación Envio de Swift.",
                                            Title = "Error al guardar localmente."
                                        });
                                    }
                                }
                                else
                                {
                                    TipG = "R";  //GraboRemoto
                                    bool result = Fn_Sw_PutSw(initObj, uow, initObj.MODGSWF.VSwf[i], cuerpoSwift, correlativo.Value);
                                    if (!result)
                                    {
                                        TipG = "L";  //Grabo Local
                                        initObj.Mdi_Principal.MantenerMensajes = true;

                                        tracer.TraceInformation("No hay comunicación con la base de datos de Swift. El swift generado quedará grabado localmente.");

                                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                                        {
                                            Type = TipoMensaje.Informacion,
                                            Text = "No hay comunicación con la base de datos de Swift. El swift generado quedará grabado localmente.",
                                            Title = "Error de comunicación"
                                        });

                                        EncaSw = Fn_EncSwiLoc(initObj, i);
                                        s = EncaSw + cuerpoSwift;
                                        //if (!Fn_SwiftLocal(initObj, uow, s))
                                        if (!Fn_SwiftPendientes(initObj, uow, cuerpoSwift, i))
                                        {
                                            tracer.TraceWarning("Se ha producido un error al grabar el Swift Localmente. El swift deberá ser digitado en la aplicación Envio de Swift.");

                                            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                                            {
                                                Type = TipoMensaje.Error,
                                                Text = "Se ha producido un error al grabar el Swift Localmente. El swift deberá ser digitado en la aplicación Envio de Swift.",
                                                Title = "Error al guardar localmente."
                                            });
                                        }

                                    }
                                }

                                if (TipG == "R")
                                {
                                    Pr_ActSwift(initObj, i, 1, correlativo.Value);
                                }
                                else
                                {
                                    Pr_ActSwift(initObj, i, 0, correlativo.HasValue ? correlativo.Value : 0);
                                }

                                if (initObj.MODGSWF.VSwf[i].NroSwf == "MT-103")
                                {
                                    TipMT = 103;
                                }
                                if (initObj.MODGSWF.VSwf[i].NroSwf == "MT-202")
                                {
                                    TipMT = 202;
                                }

                                if (Fn_PutSwfSCE(initObj, uow, initObj.MODSWENN.RutAis, correlativo.ToString(), 0, TipG, numneg, tippro, NumCpa, numcuo, NumCob, TipMT) == 0)
                                {
                                    return _retValue;
                                }
                            }

                        }
                        else
                        {
                            if (initObj.MODGSWF.VSwf[0].NroSwf == "MT-103")
                            {
                                TipMT = 103;
                            }
                            if (initObj.MODGSWF.VSwf[0].NroSwf == "MT-202")
                            {
                                TipMT = 202;
                            }

                            if (Fn_PutSwfSCE(initObj, uow, initObj.MODSWENN.RutAis, "1", 0, "L", numneg, tippro, NumCpa, numcuo, NumCob, TipMT) == 0)
                            {
                                return _retValue;
                            }
                        }

                    }

                    _retValue = (short)(true ? -1 : 0);
                    return _retValue;


                }
                catch (Exception _ex)
                {
                    tracer.TraceException("Alerta al grabar swift", _ex);

                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "[Fn_Save_BaseSwt]" + _ex.Message,
                        Title = "SWIFT"
                    });

                    throw;
                }
            }
        }

        public static void EliminarSwiftDeBDSwift(int idMensaje)
        {
            Swi200Service service = new SWI200.Swi200Service();
            service.DeleteMensaje(idMensaje);
        }

        public static bool Fn_Sw_PutSw(InitializationObject initObj, UnitOfWorkCext01 uow, T_Swf swift, string cuerpoSwift, int correlativo)
        {
            using (var tracer = new Tracer("Fn_Sw_PutSw"))
            {
                using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
                {
                    int? idExistente = uowSwift.SwRepository.Proc_sw_msg_s01_MS(correlativo);
                    if (!idExistente.HasValue || idExistente.Value == 0)
                    {
                        try
                        {
                            //todo ok, el id no existe
                            Swi200Service service = new SWI200.Swi200Service();

                            bool result = service.IngresaModificaMensajeSwift(1234, correlativo, int.Parse(initObj.MODSWENN.RutAis), int.Parse(initObj.MODGUSR.UsrEsp.CentroCosto), swift.SwfMon, swift.mtoswf, 'A', cuerpoSwift);
                            return result;
                        }
                        catch (Exception ex)
                        {
                            tracer.TraceException("Alerta", ex);

                            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Detalles: " + ex.Message,
                                Title = "Error al guardar swift en base SWIFT"
                            });

                            return false;
                        }
                    }
                    else
                    {
                        tracer.TraceWarning("El correlativo que se desea utilizar ya existe en la base de datos SWIFT");

                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "El correlativo que se desea utilizar ya existe en la base de datos SWIFT",
                            Title = "SWIFT"
                        });

                        return false;
                    }
                }
            }
        }

        //Retorna la respuesta de una Consulta.-
        //RespuestaEnvioSwift="" => Hubo error.-
        public static int? GetCorrelativoSwift()
        {
            return new Swi300Service().GetCorrelativo();
        }

        public static bool Fn_SwiftLocal(InitializationObject initObj, UnitOfWorkCext01 uow, string Texto)
        {
            using (var tracer = new Tracer("Fn_SwiftLocal"))
            {
                double x = 0;
                string Nombre = "";
                try
                {
                    // IGNORED: On Error GoTo Fn_SwiftLocal_Err

                    //Captura Nº correlativo para el nombre del archivo de mensaje               
                    x = MODGRNG.LeeSceRng(initObj.MODGRNG, initObj.MODGUSR, initObj.Mdi_Principal, uow, "CSW");
                    if (x == 0)
                    {
                        tracer.TraceInformation("No se puede capturar nombre de archivo local del mensaje Swift.");
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = "No se puede capturar nombre de archivo local del mensaje Swift.",
                            Title = "Swift"
                        });

                        return false;
                    }

                    Nombre = VB6Helpers.Right("00000000" + VB6Helpers.Trim(VB6Helpers.Str(x)), 8) + ".msg";

                    tracer.TraceInformation("El nombre del archivo generado es " + Nombre + ". Podrá accesar a el mediante la aplicación Envío de SWIFT, opción Ingresar.");
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "El nombre del archivo generado es " + Nombre + ". Podrá accesar a el mediante la aplicación Envío de SWIFT, opción Ingresar.",
                        Title = "Archivo generado"
                    });

                    //Abre archivo de texto
                    initObj.MODGSWF.FName = @"c:\data\swift\" + Nombre;
                    initObj.MODGSWF.FNum = VB6Helpers.FreeFile();
                    VB6Helpers.FileOpen(initObj.MODGSWF.FNum, initObj.MODGSWF.FName, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);

                    VB6Helpers.FilePrintLine(initObj.MODGSWF.FNum, Texto);

                    VB6Helpers.FileClose(initObj.MODGSWF.FNum);

                }
                catch (Exception _ex)
                {
                    tracer.TraceException("Alerta", _ex);

                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                        Title = T_MODGSWF.MsgSwf
                    });
                }

                return true;
            }
        }

        public static bool Fn_SwiftPendientes(InitializationObject initObj, UnitOfWorkCext01 uow, string cuerpo, short ind)
        {
            using (Tracer tracer = new Tracer("Guardando swift pendientes"))
            {
                try
                {
                    double archivo = MODGRNG.LeeSceRng(initObj.MODGRNG, initObj.MODGUSR, initObj.Mdi_Principal, uow, "CSW");
                    string sistema = "SCE";
                    DateTime fecha = DateTime.Now;
                    string moneda = initObj.MODGSWF.VSwf[ind].SwfMon;
                    decimal monto = (decimal)initObj.MODGSWF.VSwf[ind].mtoswf;
                    string referencia = initObj.MODGSWF.VGSwf.NumOpe;
                    string tipo = "MT" + initObj.MODGSWF.VSwf[ind].NroSwf.Substring(3, 3);

                    uow.SceRepository.pro_sce_swf_pendientes_i01_MS(initObj.MODGUSR.UsrEsp.CentroCosto, initObj.MODGUSR.UsrEsp.Especialista, archivo.ToString().PadLeft(6, '0'), initObj.MODGUSR.UsrEsp.Rut.ToString(), sistema, fecha, tipo, moneda, monto, referencia, cuerpo, false);

                    return true;
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, problemas al guardar los swift pendientes", ex);
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "No se puede capturar nombre de archivo local del mensaje Swift.",
                        Title = "Swift"
                    });
                }

            }
            return false;
        }

        public static string Fn_EncSwiLoc(InitializationObject initObj, short ind)
        {
            string s = "";
            s += "SCE";
            s = s + DateTime.Now.ToString("dd/MM/yyyy");
            s += VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.TimeOfDay), "hh:mm:ss");
            s = s + "MT" + VB6Helpers.Mid(initObj.MODGSWF.VSwf[ind].NroSwf, 4, 3);
            s += "0000000000";
            s = s + VB6Helpers.Right("00000000000000000" + VB6Helpers.Trim(initObj.MODGSWF.VGSwf.NumOpe), 16);
            s += initObj.MODGSWF.VSwf[ind].SwfMon;
            s = s + VB6Helpers.Right("0000000000000000000000000000000" + VB6Helpers.Trim(MODGPYF0.forma(initObj.MODGSWF.VSwf[ind].mtoswf, "############0.00")), 30) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);

            return s;
        }

        public static string Fn_EncSwi(InitializationObject initObj, short ind, string rut, string Corr)
        {
            string monto = "0000000000000000000000000000000";
            string sw = "";
            monto = VB6Helpers.Right(monto + VB6Helpers.CStr(initObj.MODGSWF.VSwf[ind].mtoswf), 30);

            sw += rut;
            sw += initObj.MODGUSR.UsrEsp.CentroCosto;
            sw += initObj.MODGSWF.VSwf[ind].SwfMon;
            sw += monto;
            sw += Corr;
            sw += "A";
            return sw;
        }

        public static int? Fn_Sw_GetCorr(InitializationObject initObj)
        {
            using (Tracer trace = new Tracer("Fn_Sw_GetCorr"))
            {
                using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
                {
                    while (true)
                    {
                        int? correlativo = GetCorrelativoSwift();
                        if (correlativo == null)
                        {
                            trace.TraceWrite("No se pudo obtener el correlativo");

                            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Informacion,
                                Text = "Se ha producido un error al rescatar correlativo de Mensaje Swift.",
                                Title = T_MODGCHQ.MsgDocVal
                            });

                            return null;
                        }
                        else
                        {
                            int? idExistente = uowSwift.SwRepository.Proc_sw_msg_s01_MS(correlativo.Value);
                            if (!idExistente.HasValue || idExistente.Value == 0)
                            {
                                //todo OK, el id no existe
                                trace.TraceWrite("Se obtuvo el correlativo válido " + correlativo.Value.ToString());
                                return correlativo;
                            }
                            else
                            {
                                //esto no deberia suceder, el sw_folios deberia retornar un id que no este utilizado en sw_msgsend
                                //pero si sucede, simplemente sigo pidiendo un correlativo mayor hasta que ya no exista
                                trace.TraceWrite("Se obtuvo el correlativo " + correlativo.Value.ToString() + " pero este ya estaba en uso");
                            }
                        }
                    }
                }
            }
        }

        //Función que llena un string con el MT
        public static string Fn_GenSwiftEnvio(InitializationObject initObj, UnitOfWorkCext01 unit, short ind)
        {
            string sw = String.Empty;

            if (initObj.MODGSWF.VSwf[ind].EstaGen != 0)
            {
                string _switchVar1 = initObj.MODGSWF.VSwf[ind].NroSwf;
                if (_switchVar1 == "MT-103")
                {
                    sw = Fn_Gen103Sw(initObj, unit, ind);
                }
                else if (_switchVar1 == "MT-202")
                {
                    sw = Fn_Gen202Sw(initObj, unit, ind);
                }

            }
            return sw;
        }

        public static string Fn_Gen202Sw(InitializationObject initObj, UnitOfWorkCext01 unit, short ind)
        {
            string _retValue = "";

            string s = "";
            string sw = "";
            string Bco = "";
            short i = 0;
            short LC = 0;
            string Fecha_Paso1 = "";
            var MODGSWF = initObj.MODGSWF;

            if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoAla.SwfBco))
            {
                Bco = MODGSWF.VSwf[ind].BcoAla.SwfBco;
            }
            else
            {
                Bco = MODGSWF.VSwf[ind].DatSwf.SwfCor;
            }

            if (!BancoIntercambiaClave(initObj, Bco))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Informacion,
                    Text = "No se pudo emitir el " + initObj.MODGSWF.VSwf[ind].NroSwf + " automáticamente, ya que la casilla SWIFT del Banco Receptor (" + Bco + "), no está habilitada. Deberá emitir un Télex, o modificarlo en la aplicacion ENVIO DE SWIFT.",
                    Title = "Validación de Datos"
                });
                return _retValue;
            }

            _retValue = "";

            sw = "";
            sw = sw + VB6Helpers.Chr(1) + "{1:F01";
            sw += "BCHICLRMAXXX";
            sw += "          }";
            sw += "{2:I";
            sw += VB6Helpers.Mid(MODGSWF.VSwf[ind].NroSwf, 4, 3);
            if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoAla.SwfBco))
            {
                sw = sw + VB6Helpers.Mid(VB6Helpers.LTrim(MODGSWF.VSwf[ind].BcoAla.SwfBco), 1, 8) + "A" + VB6Helpers.Mid(VB6Helpers.LTrim(MODGSWF.VSwf[ind].BcoAla.SwfBco), 9, 3);
            }
            else
            {
                sw = sw + VB6Helpers.Mid(VB6Helpers.LTrim(MODGSWF.VSwf[ind].DatSwf.SwfCor), 1, 8) + "A" + VB6Helpers.Mid(VB6Helpers.LTrim(MODGSWF.VSwf[ind].DatSwf.SwfCor), 9, 3);
            }

            sw += "N";
            sw = sw + "}{4:" + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);

            if (~Fn_Trae_Fmt_Campos(initObj, "MT202") != 0)
            {
                return _retValue;
            }
            for (i = 1; i <= (short)VB6Helpers.UBound(initObj.MODSWENN.VFmt_Swf); i++)
            {

                LC = initObj.MODSWENN.VFmt_Swf[i].Largo_Campo;
                string _switchVar1 = initObj.MODSWENN.VFmt_Swf[i].Id_Campo;
                if (_switchVar1 == "20")
                {
                    // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 'sw' variable as a StringBuilder6 object.
                    sw = sw + ":20:" + VB6Helpers.LTrim(MODGSWF.VGSwf.NumOpe) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                }
                else if (_switchVar1 == "21")
                {
                    if (string.IsNullOrEmpty(MODGSWF.VSwf[ind].DatSwf.RefOpe))
                    {
                        sw = sw + ":21:" + VB6Helpers.LTrim(MODGSWF.VGSwf.NumOpe) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                    }
                    else
                    {
                        sw = sw + ":21:" + VB6Helpers.LTrim(MODGPYF0.Componer(MODGSWF.VSwf[ind].DatSwf.RefOpe, "/RFB/", "")) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                    }

                }
                else if (_switchVar1 == "32A")
                {
                    //Campo32A
                    Fecha_Paso1 = VB6Helpers.Format(MODGSWF.VSwf[ind].DatSwf.FecPag, "yymmdd");
                    sw = sw + ":32A:" + VB6Helpers.LTrim(Fecha_Paso1) + VB6Helpers.LTrim(MODGSWF.VSwf[ind].SwfMon) + VB6Helpers.LTrim(MODGPYF0.forma(MODGSWF.VSwf[ind].mtoswf, "############0.00")) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                }
                else if (_switchVar1 == "56A")
                {
                    //Es A si el Swift es <>BIC
                    //Primero: en el caso de existir Bco Intermediario.
                    if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                    {
                        if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoInt.NomBco))
                        {
                            if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoInt.SwfBco))
                            {
                                if (VB6Helpers.Mid(VB6Helpers.LTrim(MODGSWF.VSwf[ind].BcoInt.SwfBco), 8, 3) != "1  " && VB6Helpers.Mid(VB6Helpers.LTrim(MODGSWF.VSwf[ind].BcoInt.SwfBco), 8, 3) != "BIC" && (MODGSWF.VSwf[ind].BcoInt.IngMan == 0))
                                {
                                    sw = sw + ":56A:" + VB6Helpers.LTrim(MODGSWF.VSwf[ind].BcoInt.SwfBco) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }

                            }

                        }

                    }

                }
                else if (_switchVar1 == "56D")
                {
                    //Para ser D :   - Ingreso manual de datos.-
                    //               -   Swift terminado en BIC.-
                    //               -   Swift terminado en 1BlancoBlanco.-
                    //En el caso del Swift = BIC.
                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoInt.NomBco))
                    {
                        if (VB6Helpers.LTrim(MODGSWF.VSwf[ind].BcoInt.SwfBco) == "" || VB6Helpers.Mid(VB6Helpers.LTrim(MODGSWF.VSwf[ind].BcoInt.SwfBco), 8, 3) == "1  " || VB6Helpers.Mid(VB6Helpers.LTrim(MODGSWF.VSwf[ind].BcoInt.SwfBco), 8, 3) == "BIC" || (MODGSWF.VSwf[ind].BcoInt.IngMan == -1))
                        {
                            if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoInt.NomBco))
                                {
                                    string _tempVar1 = VB6Helpers.LTrim(MODGSWF.VSwf[ind].BcoInt.NomBco);
                                    sw = sw + ":56D:" + Fn_FormateaString(ref _tempVar1, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoInt.DirBco1))
                                {
                                    string _tempVar2 = VB6Helpers.LTrim(MODGSWF.VSwf[ind].BcoInt.DirBco1);
                                    sw = sw + Fn_FormateaString(ref _tempVar2, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 3)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoInt.DirBco2))
                                {
                                    string _tempVar3 = VB6Helpers.LTrim(MODGSWF.VSwf[ind].BcoInt.DirBco2);
                                    sw = sw + Fn_FormateaString(ref _tempVar3, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 4)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoInt.PaiBco))
                                {
                                    string _tempVar4 = VB6Helpers.LTrim(MODGSWF.VSwf[ind].BcoInt.PaiBco);
                                    sw = sw + Fn_FormateaString(ref _tempVar4, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }

                        }

                    }

                }
                else if (_switchVar1 == "57A")
                {
                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.NomBco))
                    {
                        if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.SwfBco) && (VB6Helpers.Mid(VB6Helpers.LTrim(MODGSWF.VSwf[ind].BcoPag.SwfBco), 8, 3) != "1  ") && (VB6Helpers.Mid(VB6Helpers.LTrim(MODGSWF.VSwf[ind].BcoPag.SwfBco), 8, 3) != "BIC") && (MODGSWF.VSwf[ind].BcoPag.IngMan == 0))
                        {
                            if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                            {
                                sw = sw + ":57A:" + VB6Helpers.LTrim(MODGSWF.VSwf[ind].BcoPag.SwfBco) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }
                        }

                    }

                }
                else if (_switchVar1 == "57D")
                {
                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.NomBco))
                    {
                        if (string.IsNullOrEmpty(MODGSWF.VSwf[ind].BcoPag.SwfBco) || (VB6Helpers.Mid(VB6Helpers.LTrim(MODGSWF.VSwf[ind].BcoPag.SwfBco), 8, 3) == "1  ") || (MODGSWF.VSwf[ind].BcoPag.IngMan == -1))
                        {
                            if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.NomBco))
                                {
                                    sw = sw + ":57D:" + VB6Helpers.LTrim(MODGSWF.VSwf[ind].BcoPag.NomBco) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.DirBco1))
                                {
                                    string _tempVar5 = VB6Helpers.LTrim(MODGSWF.VSwf[ind].BcoPag.DirBco1);
                                    sw = sw + Fn_FormateaString(ref _tempVar5, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 3)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.DirBco2))
                                {
                                    string _tempVar6 = VB6Helpers.LTrim(MODGSWF.VSwf[ind].BcoPag.DirBco2);
                                    sw = sw + Fn_FormateaString(ref _tempVar6, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 4)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.PaiBco))
                                {
                                    string _tempVar7 = VB6Helpers.LTrim(MODGSWF.VSwf[ind].BcoPag.PaiBco);
                                    sw = sw + Fn_FormateaString(ref _tempVar7, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }

                        }

                    }

                }
                else if (_switchVar1 == "58A")
                {
                    if (VB6Helpers.Mid(VB6Helpers.LTrim(MODGSWF.VSwf[ind].BenSwf.SwfBen), 8) != "1")
                    {
                        if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                        {
                            if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].DatSwf.ctacte))
                            {
                                sw = sw + ":58A:" + "/" + VB6Helpers.LTrim(MODGSWF.VSwf[ind].DatSwf.ctacte) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }
                            else
                            {
                                sw = sw + ":58A:" + VB6Helpers.LTrim(MODGSWF.VSwf[ind].BenSwf.SwfBen) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }

                        }
                        else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                        {
                            if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].DatSwf.ctacte))
                            {
                                sw = sw + VB6Helpers.LTrim(MODGSWF.VSwf[ind].BenSwf.SwfBen) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }

                        }

                    }

                }
                else if (_switchVar1 == "58D")
                {
                    if (VB6Helpers.LTrim(MODGSWF.VSwf[ind].BenSwf.SwfBen) == "" || VB6Helpers.Mid(VB6Helpers.LTrim(MODGSWF.VSwf[ind].BenSwf.SwfBen), 8) == "1")
                    {
                        if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].DatSwf.ctacte))
                        {
                            if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                            {
                                sw = sw + ":58D:" + "/" + VB6Helpers.LTrim(MODGSWF.VSwf[ind].DatSwf.ctacte) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                            {
                                string _tempVar8 = VB6Helpers.LTrim(MODGSWF.VSwf[ind].BenSwf.NomBen);
                                sw = sw + Fn_FormateaString(ref _tempVar8, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 3)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.DirBen1))
                                {
                                    string _tempVar9 = VB6Helpers.LTrim(MODGSWF.VSwf[ind].BenSwf.DirBen1);
                                    sw = sw + Fn_FormateaString(ref _tempVar9, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 4)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.DirBen2))
                                {
                                    string _tempVar10 = VB6Helpers.LTrim(MODGSWF.VSwf[ind].BenSwf.DirBen2);
                                    sw = sw + Fn_FormateaString(ref _tempVar10, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 5)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.PaiBen_t))
                                {
                                    string _tempVar11 = VB6Helpers.LTrim(MODGSWF.VSwf[ind].BenSwf.PaiBen_t);
                                    sw = sw + Fn_FormateaString(ref _tempVar11, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }

                        }
                        else
                        {
                            if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                            {
                                string _tempVar12 = VB6Helpers.LTrim(MODGSWF.VSwf[ind].BenSwf.NomBen);
                                sw = sw + Fn_FormateaString(ref _tempVar12, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.DirBen1))
                                {
                                    string _tempVar13 = VB6Helpers.LTrim(MODGSWF.VSwf[ind].BenSwf.DirBen1);
                                    sw = sw + Fn_FormateaString(ref _tempVar13, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 3)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.DirBen2))
                                {
                                    string _tempVar14 = VB6Helpers.LTrim(MODGSWF.VSwf[ind].BenSwf.DirBen2);
                                    sw = sw + Fn_FormateaString(ref _tempVar14, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 4)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.PaiBen_t))
                                {
                                    string _tempVar15 = VB6Helpers.LTrim(MODGSWF.VSwf[ind].BenSwf.PaiBen_t);
                                    sw = sw + Fn_FormateaString(ref _tempVar15, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }

                        }

                    }

                }
                else if (_switchVar1 == "71A")
                {
                    if (MODGSWF.VSwf[ind].DatSwf.TipGas == 1)
                    {
                        sw = sw + ":71A:" + "BEN" + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                    }
                    else if (MODGSWF.VSwf[ind].DatSwf.TipGas == 2)
                    {
                        sw = sw + ":71A:" + "OUR" + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                    }

                }
                else if (_switchVar1 == "72")
                {
                    if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                    {
                        s = Fn_CampoMT(initObj, "72", MODGSWF.VSwf[ind].DocSwf, "72", "", "", 43, 6);
                        sw += s;
                    }

                }

            }

            sw = sw + "-}" + VB6Helpers.Chr(3);
            return sw;
        }


        public static string Fn_Gen103Sw(InitializationObject initObj, UnitOfWorkCext01 unit, short ind)
        {
            string _retValue = "";
            string Bco = "";
            string s = "";
            string sw = "";
            short LC = 0;
            short i = 0;
            short x = 0;
            string Fecha_Paso1 = "";
            short m = 0;
            string SwfMon = "";
            short linea = 0;
            string[] Arreglo = null;
            var MODGSWF = initObj.MODGSWF;

            if (!String.IsNullOrEmpty(VB6Helpers.Trim(initObj.MODGSWF.VSwf[ind].BcoAla.SwfBco)))
            {
                Bco = initObj.MODGSWF.VSwf[ind].BcoAla.SwfBco;
            }
            else
            {
                Bco = initObj.MODGSWF.VSwf[ind].DatSwf.SwfCor;
            }

            if (!BancoIntercambiaClave(initObj, Bco))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Informacion,
                    Text = "No se pudo emitir el " + initObj.MODGSWF.VSwf[ind].NroSwf + " automáticamente, ya que la casilla SWIFT del Banco Receptor (" + Bco + "), no está habilitada. Deberá emitir un Télex, o modificarlo en la aplicacion ENVIO DE SWIFT.",
                    Title = "Validación de Datos"
                });
                initObj.MODGSWF.VGSwf.ProblemasAlGenerar = true;
            }

            _retValue = "";

            sw = "";
            sw = sw + VB6Helpers.Chr(1) + "{1:F01";
            sw += "BCHICLRMAXXX";
            sw += "          }";
            sw += "{2:I";
            sw += VB6Helpers.Mid(initObj.MODGSWF.VSwf[ind].NroSwf, 4, 3);
            if (!string.IsNullOrWhiteSpace(initObj.MODGSWF.VSwf[ind].BcoAla.SwfBco))
            {
                sw = sw + VB6Helpers.Mid(VB6Helpers.Trim(initObj.MODGSWF.VSwf[ind].BcoAla.SwfBco), 1, 8) + "A" + VB6Helpers.Mid(VB6Helpers.Trim(initObj.MODGSWF.VSwf[ind].BcoAla.SwfBco), 9, 3);
            }
            else
            {
                sw = sw + VB6Helpers.Mid(VB6Helpers.Trim(initObj.MODGSWF.VSwf[ind].DatSwf.SwfCor), 1, 8) + "A" + VB6Helpers.Mid(VB6Helpers.Trim(initObj.MODGSWF.VSwf[ind].DatSwf.SwfCor), 9, 3);
            }

            sw += "N";
            sw = sw + "}{4:" + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);

            if (initObj.MODGSWF.VSwf[ind].NroSwf == "MT-103")
            {
                if (~Fn_Trae_Fmt_Campos(initObj, "MT103") != 0)
                {
                    return _retValue;
                }
            }
            for (i = 1; i <= (short)VB6Helpers.UBound(initObj.MODSWENN.VFmt_Swf); i++)
            {

                LC = initObj.MODSWENN.VFmt_Swf[i].Largo_Campo;
                string _switchVar1 = initObj.MODSWENN.VFmt_Swf[i].Id_Campo.Trim();
                if (_switchVar1 == "20")
                {
                    // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 'sw' variable as a StringBuilder6 object.
                    sw = sw + ":20:" + VB6Helpers.Trim(initObj.MODGSWF.VGSwf.NumOpe) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                }
                else if (_switchVar1 == "21")
                {
                    if (initObj.MODGSWF.VSwf[ind].NroSwf == "MT-202")
                    {
                        if (string.IsNullOrEmpty(initObj.MODGSWF.VSwf[ind].DatSwf.RefOpe))
                        {
                            sw = sw + ":21:" + VB6Helpers.Trim(initObj.MODGSWF.VGSwf.NumOpe) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                        }
                        else
                        {
                            sw = sw + ":21:" + VB6Helpers.Trim(MODGPYF0.Componer(initObj.MODGSWF.VSwf[ind].DatSwf.RefOpe, "/RFB/", "")) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                        }

                    }

                }
                else if (_switchVar1 == "23B")
                {
                    if (initObj.MODGSWF.VSwf[ind].NroSwf == "MT-103")
                    {
                        sw = sw + ":23B:" + "CRED" + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                    }

                }
                else if (_switchVar1 == "23E")
                {
                    if (initObj.MODGSWF.VSwf[ind].NroSwf == "MT-103")
                    {
                        if (initObj.MODGSWF.VSwf[ind].NroSwf == "MT-103")
                        {
                            if (VB6Helpers.UBound(initObj.MODGSWF.VCod) > 0)
                            {
                                for (x = 0; x <= (short)VB6Helpers.UBound(initObj.MODGSWF.VCod); x++)
                                {
                                    if (initObj.MODGSWF.VCod[x].Estado != 9)
                                    {
                                        if (initObj.MODGSWF.VCod[x].numswi == ind)
                                        {
                                            sw = sw + ":23E:" + initObj.MODGSWF.VCod[x].Codigo + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                        }

                                    }

                                }

                            }

                        }

                    }

                }
                else if (_switchVar1 == "32A")
                {
                    Fecha_Paso1 = VB6Helpers.Format(initObj.MODGSWF.VSwf[ind].DatSwf.FecPag, "yymmdd");
                    sw = sw + ":32A:" + VB6Helpers.Trim(Fecha_Paso1) + VB6Helpers.Trim(initObj.MODGSWF.VSwf[ind].SwfMon) + VB6Helpers.Trim(MODGPYF0.forma(initObj.MODGSWF.VSwf[ind].mtoswf, "############0.00")) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                }
                else if (_switchVar1 == "33B")
                {
                    if (initObj.MODGSWF.VSwf[ind].NroSwf == "MT-103")
                    {
                        if (!string.IsNullOrWhiteSpace(VB6Helpers.CStr(initObj.MODGSWF.VMT103[ind].MndOri)) && VB6Helpers.Trim(VB6Helpers.CStr(initObj.MODGSWF.VMT103[ind].MtoOri)) != "0")
                        {
                            m = MODGTAB0.Get_VMnd(initObj.MODGTAB0, unit, initObj.MODGSWF.VMT103[ind].MndOri);
                            SwfMon = initObj.MODGTAB0.VMnd[m].Mnd_MndSwf;
                            sw = sw + ":33B:" + VB6Helpers.Trim(SwfMon) + VB6Helpers.Trim(MODGPYF0.Componer(MODGPYF0.forma(initObj.MODGSWF.VMT103[ind].MtoOri, T_MODGSWF.FormatoSwf), ".", "")) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                        }

                    }

                }
                else if (_switchVar1 == "36")
                {
                    if (initObj.MODGSWF.VSwf[ind].NroSwf == "MT-103")
                    {
                        if (initObj.MODGSWF.VMT103[ind].TipCam != 0)
                        {
                            sw = sw + ":36:" + VB6Helpers.Trim(MODGPYF0.Componer(MODGPYF0.forma(initObj.MODGSWF.VMT103[ind].TipCam, T_MODGSWF.FormatoSwf), ",", ".")) + VB6Helpers.Chr(10);
                        }

                    }

                    //Flag 50F
                    //-------------------------------------------

                    //realiza generacion de mensaje 50F o 50K
                }
                else if (_switchVar1 == "50K")
                {
                    if (initObj.MOD_50F.VG_50F[ind, 1] == "0")
                    {
                        if (initObj.MODGSWF.VSwf[ind].NroSwf == "MT-103")
                        {
                            if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                            {
                                string _tempVar1 = VB6Helpers.Left(VB6Helpers.Trim(initObj.MODGSWF.VCliSwf.NomCli), LC);
                                sw = sw + ":50K:" + Fn_FormateaString(ref _tempVar1, 35) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                            {
                                if (!string.IsNullOrWhiteSpace(initObj.MODGSWF.VCliSwf.rutcli))
                                {
                                    string _tempVar2 = "Rut:" + VB6Helpers.Trim(initObj.MODGSWF.VCliSwf.rutcli);
                                    sw = sw + Fn_FormateaString(ref _tempVar2, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 3)
                            {
                                if (!string.IsNullOrWhiteSpace(initObj.MODGSWF.VCliSwf.DirCli1))
                                {
                                    string _tempVar3 = VB6Helpers.Trim(initObj.MODGSWF.VCliSwf.DirCli1);
                                    sw = sw + Fn_FormateaString(ref _tempVar3, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 4)
                            {
                                if (!string.IsNullOrWhiteSpace(initObj.MOD_50F.VG_50F[ind, 2]))
                                {
                                    string _tempVar4 = VB6Helpers.Trim(initObj.MODGSWF.VCliSwf.DirCli2) + " " + VB6Helpers.Trim(initObj.MOD_50F.VG_50F[ind, 2]);
                                    sw = sw + Fn_FormateaString(ref _tempVar4, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(initObj.MODGSWF.VCliSwf.DirCli2))
                                    {
                                        string _tempVar5 = VB6Helpers.Trim(initObj.MODGSWF.VCliSwf.DirCli2);
                                        sw = sw + Fn_FormateaString(ref _tempVar5, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }
                                }

                            }

                        }

                    }

                }
                else if (_switchVar1 == "50F")
                {
                    if (initObj.MOD_50F.VG_50F[ind, 1] == "1")
                    {
                        if (initObj.MODGSWF.VSwf[ind].NroSwf == "MT-103")
                        {
                            if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                            {
                                string _tempVar6 = VB6Helpers.Trim(initObj.MODGSWF.VCliSwf.CtaCli);
                                sw = sw + ":50F:/" + Fn_FormateaString(ref _tempVar6, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }

                            if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                            {
                                if (!string.IsNullOrWhiteSpace(initObj.MODGSWF.VCliSwf.NomCli))
                                {
                                    string _tempVar7 = VB6Helpers.Left(VB6Helpers.Trim(initObj.MODGSWF.VCliSwf.NomCli), LC);
                                    sw = sw + VB6Helpers.Mid("1/" + Fn_FormateaString(ref _tempVar7, 35), 1, 35) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }

                            }

                            if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 3)
                            {
                                bool printLinea1b = !string.IsNullOrWhiteSpace(initObj.MODGSWF.VCliSwf.NomCli) && initObj.MODGSWF.VCliSwf.NomCli.Length > 33;
                                if (printLinea1b)
                                {
                                    string _tempVar7 = VB6Helpers.Mid(VB6Helpers.Trim(initObj.MODGSWF.VCliSwf.NomCli), 34, 33);
                                    sw = sw + VB6Helpers.Mid("1/" + Fn_FormateaString(ref _tempVar7, 33), 1, 35) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                                else if (!string.IsNullOrWhiteSpace(initObj.MODGSWF.VCliSwf.DirCli1))
                                {
                                    string _tempVar8 = VB6Helpers.Trim(initObj.MODGSWF.VCliSwf.DirCli1);
                                    sw = sw + VB6Helpers.Mid("2/" + Fn_FormateaString(ref _tempVar8, LC), 1, 35) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }

                            if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 4)
                            {
                                if (!string.IsNullOrWhiteSpace(initObj.MOD_50F.VG_50F[ind, 2]))
                                {
                                    string _tempVar9 = VB6Helpers.Trim(initObj.MODGSWF.VCliSwf.CiuCli);
                                    sw = sw + VB6Helpers.Mid("3/" + VB6Helpers.Trim(initObj.MOD_50F.VG_50F[ind, 2]) + "/" + Fn_FormateaString(ref _tempVar9, LC), 1, 35) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }

                            }

                            if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 5)
                            {
                                if (!string.IsNullOrWhiteSpace(initObj.MODGSWF.VCliSwf.rutcli))
                                {
                                    string _tempVar10 = VB6Helpers.Trim(initObj.MODGSWF.VCliSwf.rutcli);
                                    sw = sw + VB6Helpers.Mid("6/" + VB6Helpers.Trim(initObj.MOD_50F.VG_50F[ind, 2]) + "/BCHICLRM/" + _tempVar10, 1, 35) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }

                            }

                        }

                    }

                }
                else if (_switchVar1 == "53A")
                {
                    if (VB6Helpers.Instr(sw, "53A") == 0)
                    {
                        if (!string.IsNullOrWhiteSpace(initObj.MODGSWF.VSwf[ind].BcoCoE.SwfBco))
                        {
                            if (VB6Helpers.Mid(VB6Helpers.Trim(initObj.MODGSWF.VSwf[ind].BcoCoE.SwfBco), 8, 3) != "1  " && VB6Helpers.Mid(VB6Helpers.Trim(initObj.MODGSWF.VSwf[ind].BcoCoE.SwfBco), 8, 3) != "BIC" && (initObj.MODGSWF.VSwf[ind].BcoCoE.IngMan == 0))
                            {
                                sw = sw + ":53A:" + VB6Helpers.Trim(initObj.MODGSWF.VSwf[ind].BcoCoE.SwfBco) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }

                        }

                    }

                }
                else if (_switchVar1 == "53D")
                {
                    //Para ser D :   - Ingreso manual de datos.-
                    //               -   Swift terminado en BIC.-
                    //               -   Swift terminado en 1BlancoBlanco.-
                    //En el caso del Swift = BIC.
                    if (!string.IsNullOrWhiteSpace(initObj.MODGSWF.VSwf[ind].BcoCoE.NomBco))
                    {
                        if (VB6Helpers.Trim(initObj.MODGSWF.VSwf[ind].BcoCoE.SwfBco) == "" || VB6Helpers.Mid(VB6Helpers.Trim(initObj.MODGSWF.VSwf[ind].BcoCoE.SwfBco), 8, 3) == "1  " || VB6Helpers.Mid(VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoCoE.SwfBco), 8, 3) == "BIC" || (MODGSWF.VSwf[ind].BcoCoE.IngMan == -1))
                        {
                            if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                            {
                                if (!string.IsNullOrWhiteSpace(initObj.MODGSWF.VSwf[ind].BcoCoE.NomBco))
                                {
                                    string _tempVar9 = VB6Helpers.Trim(initObj.MODGSWF.VSwf[ind].BcoCoE.NomBco);
                                    sw = sw + ":53D:" + Fn_FormateaString(ref _tempVar9, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                            {
                                if (!string.IsNullOrWhiteSpace(initObj.MODGSWF.VSwf[ind].BcoCoE.DirBco1))
                                {
                                    string _tempVar10 = VB6Helpers.Trim(initObj.MODGSWF.VSwf[ind].BcoCoE.DirBco1);
                                    sw = sw + Fn_FormateaString(ref _tempVar10, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 3)
                            {
                                if (!string.IsNullOrWhiteSpace(initObj.MODGSWF.VSwf[ind].BcoCoE.DirBco2))
                                {
                                    string _tempVar11 = VB6Helpers.Trim(initObj.MODGSWF.VSwf[ind].BcoCoE.DirBco2);
                                    sw = sw + Fn_FormateaString(ref _tempVar11, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 4)
                            {
                                if (!string.IsNullOrWhiteSpace(initObj.MODGSWF.VSwf[ind].BcoCoE.PaiBco))
                                {
                                    string _tempVar12 = VB6Helpers.Trim(initObj.MODGSWF.VSwf[ind].BcoCoE.PaiBco);
                                    sw = sw + Fn_FormateaString(ref _tempVar12, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }

                        }

                    }

                }
                else if (_switchVar1 == "54A")
                {
                    if (VB6Helpers.Instr(sw, "54A") == 0)
                    {
                        if (!string.IsNullOrWhiteSpace(initObj.MODGSWF.VSwf[ind].BcoCoD.SwfBco))
                        {
                            if (VB6Helpers.Mid(VB6Helpers.Trim(initObj.MODGSWF.VSwf[ind].BcoCoD.SwfBco), 8, 3) != "1  " && VB6Helpers.Mid(VB6Helpers.Trim(initObj.MODGSWF.VSwf[ind].BcoCoD.SwfBco), 8, 3) != "BIC" && (initObj.MODGSWF.VSwf[ind].BcoCoD.IngMan == 0))
                            {
                                sw = sw + ":54A:" + VB6Helpers.Trim(initObj.MODGSWF.VSwf[ind].BcoCoD.SwfBco) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }

                        }

                    }

                }
                else if (_switchVar1 == "54D")
                {
                    //Para ser D :   - Ingreso manual de datos.-
                    //               -   Swift terminado en BIC.-
                    //               -   Swift terminado en 1BlancoBlanco.-
                    //En el caso del Swift = BIC.
                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoCoD.NomBco))
                    {
                        if (VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoCoD.SwfBco) == "" || VB6Helpers.Mid(VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoCoD.SwfBco), 8, 3) == "1  " || VB6Helpers.Mid(VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoCoD.SwfBco), 8, 3) == "BIC" || (MODGSWF.VSwf[ind].BcoCoE.IngMan == -1))
                        {
                            if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoCoD.NomBco))
                                {
                                    string _tempVar13 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoCoD.NomBco);
                                    sw = sw + ":54D:" + Fn_FormateaString(ref _tempVar13, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoCoD.DirBco1))
                                {
                                    string _tempVar14 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoCoD.DirBco1);
                                    sw = sw + Fn_FormateaString(ref _tempVar14, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 3)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoCoD.DirBco2))
                                {
                                    string _tempVar15 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoCoD.DirBco2);
                                    sw = sw + Fn_FormateaString(ref _tempVar15, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 4)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoCoD.PaiBco))
                                {
                                    string _tempVar16 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoCoD.PaiBco);
                                    sw = sw + Fn_FormateaString(ref _tempVar16, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }

                        }

                    }

                }
                else if (_switchVar1 == "55A")
                {
                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoTer.SwfBco))
                    {
                        if (VB6Helpers.Mid(VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoTer.SwfBco), 8, 3) != "1  " && VB6Helpers.Mid(VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoTer.SwfBco), 8, 3) != "BIC" && (MODGSWF.VSwf[ind].BcoTer.IngMan == 0))
                        {
                            sw = sw + ":55A:" + VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoTer.SwfBco) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                        }

                    }

                }
                else if (_switchVar1 == "55D")
                {
                    //Para ser D :   - Ingreso manual de datos.-
                    //               -   Swift terminado en BIC.-
                    //               -   Swift terminado en 1BlancoBlanco.-
                    //En el caso del Swift = BIC.
                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoTer.NomBco))
                    {
                        if (VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoTer.SwfBco) == "" || VB6Helpers.Mid(VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoCoD.SwfBco), 8, 3) == "1  " || VB6Helpers.Mid(VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoTer.SwfBco), 8, 3) == "BIC" || (MODGSWF.VSwf[ind].BcoTer.IngMan == -1))
                        {
                            if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoTer.NomBco))
                                {
                                    string _tempVar17 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoTer.NomBco);
                                    sw = sw + ":55D:" + Fn_FormateaString(ref _tempVar17, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoTer.DirBco1))
                                {
                                    string _tempVar18 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoTer.DirBco1);
                                    sw = sw + Fn_FormateaString(ref _tempVar18, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 3)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoTer.DirBco2))
                                {
                                    string _tempVar19 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoTer.DirBco2);
                                    sw = sw + Fn_FormateaString(ref _tempVar19, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 4)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoTer.PaiBco))
                                {
                                    string _tempVar20 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoTer.PaiBco);
                                    sw = sw + Fn_FormateaString(ref _tempVar20, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }

                        }

                    }

                }
                else if (_switchVar1 == "56A")
                {
                    //Es A si el Swift es <>BIC
                    if (MODGSWF.VSwf[ind].NroSwf == "MT-202")
                    {
                        //Primero: en el caso de existir Bco Intermediario.
                        if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoInt.NomBco))
                        {
                            if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoInt.SwfBco))
                            {
                                if (VB6Helpers.Mid(VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.SwfBco), 8, 3) != "1  " && VB6Helpers.Mid(VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.SwfBco), 8, 3) != "BIC" && (MODGSWF.VSwf[ind].BcoInt.IngMan == 0))
                                {
                                    sw = sw + ":56A:" + VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.SwfBco) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }

                            }

                        }

                    }
                    else
                    {
                        if (VB6Helpers.Instr(sw, "56A") == 0)
                        {
                            if (VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.CodCom) == "")
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoInt.SwfBco))
                                {
                                    if (VB6Helpers.Mid(VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.SwfBco), 8, 3) != "1  " && VB6Helpers.Mid(VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.SwfBco), 8, 3) != "BIC" && (MODGSWF.VSwf[ind].BcoInt.IngMan == 0))
                                    {
                                        sw = sw + ":56A:" + VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.SwfBco) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }

                                }

                            }

                        }

                    }

                }
                else if (_switchVar1 == "56C")
                {
                    if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                    {
                        if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoInt.CodCom) && VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.SwfBco) == "" && VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.NomBco) == "")
                        {
                            sw = sw + ":56C:" + "//" + VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.CodCom) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                        }

                    }

                }
                else if (_switchVar1 == "56D")
                {
                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoInt.CodCom) && VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.SwfBco) == "" && !string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoInt.NomBco))
                    {
                        if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                        {
                            sw = sw + ":56D:" + "//" + VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.CodCom) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                        }
                        else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                        {
                            string _tempVar21 = VB6Helpers.Trim(VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.NomBco));
                            sw = sw + Fn_FormateaString(ref _tempVar21, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                        }
                        else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 3)
                        {
                            if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoInt.DirBco1))
                            {
                                string _tempVar22 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.DirBco1);
                                sw = sw + Fn_FormateaString(ref _tempVar22, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }
                        }
                        else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 4)
                        {
                            if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoInt.DirBco2))
                            {
                                string _tempVar23 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.DirBco2);
                                sw = sw + Fn_FormateaString(ref _tempVar23, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }
                        }
                        else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 5)
                        {
                            if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoInt.PaiBco))
                            {
                                string _tempVar24 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.PaiBco);
                                sw = sw + Fn_FormateaString(ref _tempVar24, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }
                        }

                    }

                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoInt.NomBco) && VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.CodCom) == "" && VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.SwfBco) == "")
                    {
                        if (VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.SwfBco) == "" || VB6Helpers.Mid(VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.SwfBco), 8, 3) == "1  " || VB6Helpers.Mid(VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.SwfBco), 8, 3) == "BIC" || (MODGSWF.VSwf[ind].BcoInt.IngMan == -1))
                        {
                            if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoInt.NomBco))
                                {
                                    string _tempVar25 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.NomBco);
                                    sw = sw + ":56D:" + Fn_FormateaString(ref _tempVar25, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoInt.DirBco1))
                                {
                                    string _tempVar26 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.DirBco1);
                                    sw = sw + Fn_FormateaString(ref _tempVar26, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 3)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoInt.DirBco2))
                                {
                                    string _tempVar27 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.DirBco2);
                                    sw = sw + Fn_FormateaString(ref _tempVar27, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 4)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoInt.PaiBco))
                                {
                                    string _tempVar28 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoInt.PaiBco);
                                    sw = sw + Fn_FormateaString(ref _tempVar28, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }

                        }

                    }

                }
                else if (_switchVar1 == "57A")
                {
                    if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                    {
                        if (VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.CodCom) == "")
                        {
                            if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.SwfBco) && (VB6Helpers.Mid(VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.SwfBco), 8, 3) != "1  ") && (VB6Helpers.Mid(VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.SwfBco), 8, 3) != "BIC") && (MODGSWF.VSwf[ind].BcoPag.IngMan == 0))
                            {
                                if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                                {
                                    sw = sw + ":57A:" + VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.SwfBco) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }

                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.SwfBco))
                            {
                                if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                                {
                                    sw = sw + ":57A:" + VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.CodCom) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                                else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                                {
                                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.SwfBco))
                                    {
                                        string _tempVar29 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.SwfBco);
                                        sw = sw + Fn_FormateaString(ref _tempVar29, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }
                                }

                            }

                        }

                    }

                }
                else if (_switchVar1 == "57B")
                {
                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.CodCom) && VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.SwfBco) == "" && VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.NomBco) == "" && !string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.PaiBco))
                    {
                        if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                        {
                            sw = sw + ":57B:" + "//" + VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.CodCom) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                        }
                        else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                        {
                            string _tempVar30 = VB6Helpers.Trim(VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.PaiBco));
                            sw = sw + Fn_FormateaString(ref _tempVar30, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                        }

                    }

                }
                else if (_switchVar1 == "57C")
                {
                    if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                    {
                        if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.CodCom) && VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.SwfBco) == "" && VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.NomBco) == "" && VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.PaiBco) == "")
                        {
                            sw = sw + ":57C:" + "//" + VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.CodCom) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                        }

                    }

                }
                else if (_switchVar1 == "57D")
                {
                    //Para ser D :   - Ingreso manual de datos.-
                    //               -   Swift terminado en BIC.-
                    //               -   Swift terminado en 1BlancoBlanco.-
                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.CodCom) && VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.SwfBco) == "" && !string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.NomBco))
                    {
                        if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                        {
                            sw = sw + ":57D:" + "//" + VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.CodCom) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                        }
                        else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                        {
                            string _tempVar31 = VB6Helpers.Trim(VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.NomBco));
                            sw = sw + Fn_FormateaString(ref _tempVar31, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                        }
                        else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 3)
                        {
                            if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.DirBco1))
                            {
                                string _tempVar32 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.DirBco1);
                                sw = sw + Fn_FormateaString(ref _tempVar32, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }
                        }
                        else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 4)
                        {
                            if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.DirBco2))
                            {
                                string _tempVar33 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.DirBco2);
                                sw = sw + Fn_FormateaString(ref _tempVar33, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }
                        }
                        else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 5)
                        {
                            if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.PaiBco))
                            {
                                string _tempVar34 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.PaiBco);
                                sw = sw + Fn_FormateaString(ref _tempVar34, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }
                        }

                    }

                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.NomBco) && VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.CodCom) == "" && (MODGSWF.VSwf[ind].BcoPag.SwfBco ?? string.Empty).Trim() == "")
                    {
                        if ((MODGSWF.VSwf[ind].BcoPag.SwfBco ?? string.Empty).Trim() == "" || (VB6Helpers.Mid(VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.SwfBco), 8, 3) == "1  ") || (MODGSWF.VSwf[ind].BcoPag.IngMan == -1))
                        {
                            if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.NomBco))
                                {
                                    sw = sw + ":57D:" + VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.NomBco) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.DirBco1))
                                {
                                    string _tempVar35 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.DirBco1);
                                    sw = sw + Fn_FormateaString(ref _tempVar35, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 3)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.DirBco2))
                                {
                                    string _tempVar36 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.DirBco2);
                                    sw = sw + Fn_FormateaString(ref _tempVar36, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 4)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BcoPag.PaiBco))
                                {
                                    string _tempVar37 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BcoPag.PaiBco);
                                    sw = sw + Fn_FormateaString(ref _tempVar37, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }

                        }

                    }

                }
                else if (_switchVar1 == "59")
                {
                    if (!MODGSWF.VSwf[ind].BenSwf.Es59F)
                    {
                        if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                        {
                            if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].DatSwf.ctacte))
                            {
                                if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                                {
                                    string _tempVar38 = VB6Helpers.Trim(MODGSWF.VSwf[ind].DatSwf.ctacte);
                                    sw = sw + ":59:" + "/" + Fn_FormateaString(ref _tempVar38, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                                else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                                {
                                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.NomBen))
                                    {
                                        string _tempVar39 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.NomBen);
                                        sw = sw + Fn_FormateaString(ref _tempVar39, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }
                                }
                                else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 3)
                                {
                                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.DirBen1))
                                    {
                                        string _tempVar40 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.DirBen1);
                                        sw = sw + Fn_FormateaString(ref _tempVar40, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }
                                }
                                else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 4)
                                {
                                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.PaiBen_t))
                                    {
                                        if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.DirBen2))
                                        {
                                            string _tempVar41 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.DirBen2); string _tempVar42 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.PaiBen_t);
                                            sw = sw + Fn_FormateaString(ref _tempVar41, LC) + " " + Fn_FormateaString(ref _tempVar42, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.DirBen2))
                                        {
                                            string _tempVar43 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.DirBen2);
                                            sw = sw + Fn_FormateaString(ref _tempVar43, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                        }
                                    }

                                }

                            }
                            else
                            {
                                if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                                {
                                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.NomBen))
                                    {
                                        string _tempVar44 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.NomBen);
                                        sw = sw + ":59:" + Fn_FormateaString(ref _tempVar44, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }
                                }
                                else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                                {
                                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.DirBen1))
                                    {
                                        string _tempVar45 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.DirBen1);
                                        sw = sw + Fn_FormateaString(ref _tempVar45, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }
                                }
                                else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 3)
                                {
                                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.PaiBen_t))
                                    {
                                        if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.DirBen2))
                                        {
                                            string _tempVar46 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.DirBen2); string _tempVar47 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.PaiBen_t);
                                            sw = sw + Fn_FormateaString(ref _tempVar46, LC) + " " + Fn_FormateaString(ref _tempVar47, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.DirBen2))
                                        {
                                            string _tempVar48 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.DirBen2);
                                            sw = sw + Fn_FormateaString(ref _tempVar48, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                        }
                                    }

                                }

                            }

                        }
                    }
                }
                else if (_switchVar1 == "59F")
                {
                    if (MODGSWF.VSwf[ind].BenSwf.Es59F)
                    {
                        if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                        {
                            if (!String.IsNullOrEmpty(MODGSWF.VSwf[ind].DatSwf.ctacte.Trim()))
                            {
                                string _tempVar59f = String.Empty;
                                if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                                {
                                    _tempVar59f = MODGSWF.VSwf[ind].DatSwf.ctacte.Trim();
                                    sw += ":59F:" + "/" + Fn_FormateaString(ref _tempVar59f, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }

                                else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                                {
                                    if (!String.IsNullOrEmpty(MODGSWF.VSwf[ind].BenSwf.NomBen))
                                    {
                                        _tempVar59f = VB6Helpers.Left(MODGSWF.VSwf[ind].BenSwf.NomBen.Trim(), LC);
                                        sw += "1/" + Fn_FormateaString(ref _tempVar59f, 33) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }
                                }
                                else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 3)
                                {
                                    if (!String.IsNullOrEmpty(MODGSWF.VSwf[ind].BenSwf.NomBen) && MODGSWF.VSwf[ind].BenSwf.NomBen.Trim().Length > 33)
                                    {
                                        _tempVar59f = MODGSWF.VSwf[ind].BenSwf.NomBen.Trim().Substring(33);
                                        sw += "1/" + Fn_FormateaString(ref _tempVar59f, 33) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }
                                    else if (!String.IsNullOrEmpty(MODGSWF.VSwf[ind].BenSwf.DirBen1))
                                    {
                                        _tempVar59f = VB6Helpers.Left(MODGSWF.VSwf[ind].BenSwf.DirBen1.Trim(), LC);
                                        sw += "2/" + Fn_FormateaString(ref _tempVar59f, 33) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }
                                }
                                else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 4)
                                {
                                    if (!String.IsNullOrEmpty(MODGSWF.VSwf[ind].BenSwf.NomBen) && MODGSWF.VSwf[ind].BenSwf.NomBen.Trim().Length > 33 && !String.IsNullOrEmpty(MODGSWF.VSwf[ind].BenSwf.DirBen1))
                                    {
                                        _tempVar59f = VB6Helpers.Left(MODGSWF.VSwf[ind].BenSwf.DirBen1.Trim(), LC);
                                        sw += "2/" + Fn_FormateaString(ref _tempVar59f, 33) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }
                                    else if (!String.IsNullOrEmpty(MODGSWF.VSwf[ind].BenSwf.DirBen1) && MODGSWF.VSwf[ind].BenSwf.DirBen1.Trim().Length > 33)
                                    {
                                        _tempVar59f = MODGSWF.VSwf[ind].BenSwf.DirBen1.Trim().Substring(33);
                                        sw += "2/" + Fn_FormateaString(ref _tempVar59f, 33) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }
                                }
                                else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 5)
                                {
                                    if (!String.IsNullOrEmpty(MODGSWF.VSwf[ind].BenSwf.PaiBen59F))
                                    {
                                        _tempVar59f = VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.DirBen2);
                                        sw += VB6Helpers.Left(("3/" + MODGSWF.VSwf[ind].BenSwf.PaiBen59F.Trim() + (String.IsNullOrEmpty(_tempVar59f) ? String.Empty : "/" + _tempVar59f)), 35) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (_switchVar1 == "58A")
                {
                    if (MODGSWF.VSwf[ind].NroSwf == "MT-202")
                    {
                        if (VB6Helpers.Mid(VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.SwfBen), 8) != "1")
                        {
                            if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].DatSwf.ctacte))
                                {
                                    sw = sw + ":58A:" + "/" + VB6Helpers.Trim(MODGSWF.VSwf[ind].DatSwf.ctacte) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                                else
                                {
                                    sw = sw + ":58A:" + VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.SwfBen) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }

                            }
                            else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                            {
                                if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].DatSwf.ctacte))
                                {
                                    sw = sw + VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.SwfBen) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }

                            }

                        }

                    }

                }
                else if (_switchVar1 == "58D")
                {
                    if (MODGSWF.VSwf[ind].NroSwf == "MT-202")
                    {
                        if (VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.SwfBen) == "" || VB6Helpers.Mid(VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.SwfBen), 8) == "1")
                        {
                            if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].DatSwf.ctacte))
                            {
                                if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                                {
                                    sw = sw + ":58D:" + "/" + VB6Helpers.Trim(MODGSWF.VSwf[ind].DatSwf.ctacte) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                                else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                                {
                                    string _tempVar49 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.NomBen);
                                    sw = sw + Fn_FormateaString(ref _tempVar49, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                                else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 3)
                                {
                                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.DirBen1))
                                    {
                                        string _tempVar50 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.DirBen1);
                                        sw = sw + Fn_FormateaString(ref _tempVar50, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }
                                }
                                else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 4)
                                {
                                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.DirBen2))
                                    {
                                        string _tempVar51 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.DirBen2);
                                        sw = sw + Fn_FormateaString(ref _tempVar51, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }
                                }
                                else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 5)
                                {
                                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.PaiBen_t))
                                    {
                                        string _tempVar52 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.PaiBen_t);
                                        sw = sw + Fn_FormateaString(ref _tempVar52, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }
                                }

                            }
                            else
                            {
                                if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 1)
                                {
                                    string _tempVar53 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.NomBen);
                                    sw = sw + Fn_FormateaString(ref _tempVar53, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                                else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 2)
                                {
                                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.DirBen1))
                                    {
                                        string _tempVar54 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.DirBen1);
                                        sw = sw + Fn_FormateaString(ref _tempVar54, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }
                                }
                                else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 3)
                                {
                                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.DirBen2))
                                    {
                                        string _tempVar55 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.DirBen2);
                                        sw = sw + Fn_FormateaString(ref _tempVar55, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }
                                }
                                else if (initObj.MODSWENN.VFmt_Swf[i].Linea_Campo == 4)
                                {
                                    if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].BenSwf.PaiBen_t))
                                    {
                                        string _tempVar56 = VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.PaiBen_t);
                                        sw = sw + Fn_FormateaString(ref _tempVar56, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }
                                }

                            }

                        }

                    }

                }
                else if (_switchVar1 == "70")
                {
                    if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                    {
                        linea = initObj.MODSWENN.VFmt_Swf[i].Linea_Campo;
                        if (linea == 1)
                        {
                            Arreglo = new string[1];
                            if (!string.IsNullOrWhiteSpace(MODGSWF.VSwf[ind].DatSwf.RefOpe))
                            {
                                if (VB6Helpers.Instr(MODGSWF.VSwf[ind].DatSwf.RefOpe, "/RFB/") == 0)
                                {
                                    MODGSWF.VSwf[ind].DatSwf.RefOpe = "/RFB/" + MODGSWF.VSwf[ind].DatSwf.RefOpe;
                                }

                                int nLineas = (MODGSWF.VSwf[ind].DatSwf.RefOpe.Length - 1) / 35 + 1;
                                List<string> arregloAux = Arreglo.ToList();
                                for (int nLinea = 0; nLinea < nLineas; nLinea++)
                                {
                                    arregloAux.Add(VB6Helpers.Mid(MODGSWF.VSwf[ind].DatSwf.RefOpe, nLinea * LC + 1, LC));
                                }
                                Arreglo = arregloAux.ToArray();
                            }

                        }

                        if (linea <= Math.Min(VB6Helpers.UBound(Arreglo), 4))
                        {
                            if (linea == 1)
                            {
                                if (!string.IsNullOrEmpty(Arreglo[linea]))
                                {
                                    sw = sw + ":70:" + Fn_FormateaString(ref Arreglo[linea], LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }

                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Arreglo[linea]))
                                {
                                    sw = sw + Fn_FormateaString(ref Arreglo[linea], LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }

                            }

                        }

                    }

                }
                else if (_switchVar1 == "71A")
                {
                    if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                    {
                        if (MODGSWF.VSwf[ind].DatSwf.TipGas == 1)
                        {
                            sw = sw + ":71A:" + "BEN" + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                        }
                        else if (MODGSWF.VSwf[ind].DatSwf.TipGas == 2)
                        {
                            sw = sw + ":71A:" + "OUR" + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                        }
                        else if (MODGSWF.VSwf[ind].DatSwf.TipGas == 3)
                        {
                            sw = sw + ":71A:" + "SHA" + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                        }

                    }

                }
                else if (_switchVar1 == "71F")
                {
                    if (MODGSWF.VSwf[ind].DatSwf.TipGas == 1)
                    {
                        sw = sw + ":71F:" + VB6Helpers.Trim(MODGSWF.VSwf[ind].SwfMon) + VB6Helpers.Trim(MODGPYF0.forma(MODGSWF.VMT103[MODGSWF.Indice_Monto].GasEmi, T_MODGSWF.FormatoSwf)) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                    }
                    else if (MODGSWF.VMT103[MODGSWF.Indice_Monto].GasEmi > 0)
                    {
                        sw = sw + ":71F:" + VB6Helpers.Trim(MODGSWF.VSwf[ind].SwfMon) + VB6Helpers.Trim(MODGPYF0.forma(MODGSWF.VMT103[MODGSWF.Indice_Monto].GasEmi, T_MODGSWF.FormatoSwf)) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                    }

                }
                else if (_switchVar1 == "71G")
                {
                    if (MODGSWF.VMT103[MODGSWF.Indice_Monto].GasRec > 0)
                    {
                        sw = sw + ":71G:" + VB6Helpers.Trim(MODGSWF.VSwf[ind].SwfMon) + VB6Helpers.Trim(MODGPYF0.forma(MODGSWF.VMT103[MODGSWF.Indice_Monto].GasRec, T_MODGSWF.FormatoSwf)) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                    }

                }
                else if (_switchVar1 == "72")
                {
                    linea = initObj.MODSWENN.VFmt_Swf[i].Linea_Campo;
                    if (linea == 1)
                    {
                        Arreglo = new string[1];
                        string camp = Fn_CampoMTManual(MODGSWF.VSwf[ind].DocSwf, _switchVar1 + " ", "INFO DE REMITENTE A DESTINATARIO").Trim();
                        Arreglo = camp.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToArray();
                    }

                    if (linea <= Arreglo.Length)
                    {
                        if (linea == 1)
                        {
                            if (!string.IsNullOrEmpty(Arreglo[linea - 1]))
                            {
                                sw = sw + ":" + _switchVar1 + ":" + Fn_FormateaString(ref Arreglo[linea - 1], LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Arreglo[linea - 1]))
                            {
                                if (VB6Helpers.Instr(Arreglo[linea - 1], "//") == 0)
                                {
                                    Arreglo[linea - 1] = "//" + Arreglo[linea - 1];
                                }
                                sw = sw + Fn_FormateaString(ref Arreglo[linea - 1], LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }
                        }
                    }


                }
                else if (_switchVar1 == "77B")
                {
                    if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                    {
                        linea = initObj.MODSWENN.VFmt_Swf[i].Linea_Campo;
                        if (linea == 1)
                        {
                            Arreglo = new string[1];
                            string camp = Fn_CampoMTManual(MODGSWF.VSwf[ind].DocSwf, "77B", "INFO EXIGIDA POR REGLAMENTOS").Trim();
                            Arreglo = camp.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToArray();
                        }

                        if (linea <= Arreglo.Length)
                        {
                            if (linea == 1)
                            {
                                if (!string.IsNullOrEmpty(Arreglo[linea - 1]))
                                {
                                    sw = sw + ":77B:" + Fn_FormateaString(ref Arreglo[linea - 1], LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }

                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Arreglo[linea - 1]))
                                {
                                    sw = sw + Fn_FormateaString(ref Arreglo[linea - 1], LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }

                            }

                        }

                    }

                }

            }

            sw = sw + "-}" + VB6Helpers.Chr(3);
            return sw;
        }

        public static string Fn_CampoMT(InitializationObject initObj, string Codigo, string MT, string campo, string sig, string subsig, short Espacio, short Lineas)
        {
            // UPGRADE_INFO (#0561): The 'sw2' symbol was defined without an explicit "As" clause.
            dynamic sw2 = "";
            string Tipo = "";
            string Com = "";
            short k = 0;
            string LinSinAc = "";

            if (VB6Helpers.Instr(MT, ": " + campo + " :") != 0)
            {

                switch (Codigo)
                {
                    case "72":
                        if (VB6Helpers.Instr(MT, ": " + sig + " :") != 0)
                        {
                            Com = VB6Helpers.Mid(MT, VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio, VB6Helpers.Instr(MT, ": " + sig + " :") - VB6Helpers.Instr(MT, ": " + campo + " :") - Espacio);
                        }
                        else
                        {
                            if (VB6Helpers.Instr(MT, ": " + subsig + " :") != 0)
                            {
                                Com = VB6Helpers.Mid(MT, VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio, VB6Helpers.Instr(MT, ": " + subsig + " :") - VB6Helpers.Instr(MT, ": " + campo + " :") - Espacio);
                            }
                            else
                            {
                                Com = VB6Helpers.Mid(MT, VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio);
                            }

                        }

                        break;
                    case "74":
                        if (VB6Helpers.Instr(MT, ": " + sig + " :") != 0)
                        {
                            Com = VB6Helpers.Mid(MT, VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio, VB6Helpers.Instr(MT, ": " + sig + " :") - VB6Helpers.Instr(MT, ": " + campo + " :") - Espacio);
                        }
                        else
                        {
                            if (VB6Helpers.Instr(MT, ": " + subsig + " :") != 0)
                            {
                                Com = VB6Helpers.Mid(MT, VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio, VB6Helpers.Instr(MT, ": " + subsig + " :") - VB6Helpers.Instr(MT, ": " + campo + " :") - Espacio);
                            }
                            else
                            {
                                Com = VB6Helpers.Mid(MT, VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio);
                            }

                        }

                        break;
                    case "75":
                        if (VB6Helpers.Instr(MT, ": " + sig + " :") != 0)
                        {
                            Com = VB6Helpers.Mid(MT, VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio, VB6Helpers.Instr(MT, ": " + sig + " :") - VB6Helpers.Instr(MT, ": " + campo + " :") - Espacio);
                        }
                        else
                        {
                            Com = VB6Helpers.Mid(MT, VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio, VB6Helpers.Len(MT) - VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio);
                        }

                        break;
                    case "76":
                        Com = VB6Helpers.Mid(MT, VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio);

                        break;
                    case "73":
                        Com = VB6Helpers.Mid(MT, VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio);

                        break;
                }

                Com = MODGPYF0.Componer(Com, VB6Helpers.Space(Espacio), " ");
                Com = MODGPYF0.Componer(Com, ": 72 : SENDER TO RECEIVER INFORMATION", " ");
                Com = MODGPYF0.Componer(Com, ": 75 : QUERIES", " ");
                Com = MODGPYF0.Componer(Com, "//", "");
                Com = MODGPYF0.Componer(Com, "/", "");
                Com = MODGPYF0.Componer(Com, VB6Helpers.Chr(13) + VB6Helpers.Chr(10), " ");
                VB6Helpers.Erase(ref initObj.MODSWENN.Lin_Mts);

                if (Codigo == "72")
                {

                    //Dependiendo si tiene campo 57? pone "ACC" sino "REC"
                    //Esto es válido sólo para MT-100 y MT-202.-
                    Tipo = "";
                    if (VB6Helpers.Instr(MT, "MESSAGE TYPE   : 100") != 0 || VB6Helpers.Instr(MT, "MESSAGE TYPE   : 202") != 0)
                    {
                        Tipo = "/REC/";
                        if (VB6Helpers.Instr(MT, ": 57") != 0)
                        {
                            Tipo = "/ACC/";
                        }

                        Com = Tipo + Com;
                    }

                    //Si es MT-100 o MT-202 v/s los otros MT's.-
                    if (!string.IsNullOrEmpty(Tipo))
                    {
                        MODGPYF1.SeparaL(initObj.Mdi_Principal.MESSAGES, Com, ref initObj.MODSWENN.Lin_Mts, 33, 210); //Len(Com$))
                    }
                    else
                    {
                        MODGPYF1.SeparaL(initObj.Mdi_Principal.MESSAGES, Com, ref initObj.MODSWENN.Lin_Mts, 35, 210); //Len(Com$))
                    }

                    for (k = 1; k <= (short)VB6Helpers.UBound(initObj.MODSWENN.Lin_Mts); k++)
                    {
                        if (k > Lineas)
                        {
                            break;
                        }

                        LinSinAc = Fn_FormateaString(ref initObj.MODSWENN.Lin_Mts[k], (short)VB6Helpers.Len(initObj.MODSWENN.Lin_Mts[k]));
                        initObj.MODSWENN.Lin_Mts[k] = LinSinAc;
                        if (k == 1)
                        {

                            sw2 = sw2 + ":" + campo + ":" + VB6Helpers.LTrim(initObj.MODSWENN.Lin_Mts[k]) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Tipo))
                            {
                                sw2 = sw2 + "//" + VB6Helpers.LTrim(initObj.MODSWENN.Lin_Mts[k]) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }
                            else
                            {
                                sw2 = sw2 + VB6Helpers.LTrim(initObj.MODSWENN.Lin_Mts[k]) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }

                            //Se ponen dos lineas en comentarios del MT-100
                        }

                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(Com))
                    {
                        MODGPYF1.SeparaL(initObj.Mdi_Principal.MESSAGES, Com, ref initObj.MODSWENN.Lin_Mts, 35, 210); //Len(Com$))
                        for (k = 1; k <= (short)VB6Helpers.UBound(initObj.MODSWENN.Lin_Mts); k++)
                        {
                            if (k > Lineas)
                            {
                                break;
                            }

                            LinSinAc = Fn_FormateaString(ref initObj.MODSWENN.Lin_Mts[k], (short)VB6Helpers.Len(initObj.MODSWENN.Lin_Mts[k]));
                            initObj.MODSWENN.Lin_Mts[k] = LinSinAc;
                            if (k == 1)
                            {

                                sw2 = sw2 + ":" + campo + ":" + VB6Helpers.LTrim(initObj.MODSWENN.Lin_Mts[k]) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }
                            else
                            {
                                sw2 = sw2 + VB6Helpers.LTrim(initObj.MODSWENN.Lin_Mts[k]) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }

                        }

                    }

                }

            }

            return VB6Helpers.CStr(sw2);
        }

        //Esta función va a buscar los datos a la base Swift
        public static short Fn_Trae_Fmt_Campos(InitializationObject initObj, string MT)
        {
            List<ProcSwTraeFmtCampos> listaFormato = null;

            using (UnitOfWorkSwift uow = new UnitOfWorkSwift())
            {
                listaFormato = uow.SwRepository.Proc_sw_trae_fmt_campos(MT);

                //Resultado nulo de la Consulta.-
                if (listaFormato.Count == 0)
                {
                    return -1;
                }

                initObj.MODSWENN.VFmt_Swf = new Fmt_Swf[listaFormato.Count + 1];

                int i = 1;
                foreach (ProcSwTraeFmtCampos formatoCampo in listaFormato)
                {
                    Fmt_Swf formato = new Fmt_Swf()
                    {
                        Status_Campo = formatoCampo.Status,
                        Id_Campo = formatoCampo.Tag,
                        Nombre_Campo = formatoCampo.Nombre,
                        Orden_Campo = (short)(formatoCampo.Orden.HasValue ? formatoCampo.Orden.Value : 0),
                        Repeticion = (short)(formatoCampo.Repeticion.HasValue ? formatoCampo.Repeticion.Value : 0),
                        Formato_Campo = formatoCampo.Formato,
                        Largo_Campo = (short)(formatoCampo.Largo.HasValue ? formatoCampo.Largo.Value : 0),
                        Linea_Campo = (short)(formatoCampo.Linea.HasValue ? formatoCampo.Linea.Value : 0)
                    };

                    initObj.MODSWENN.VFmt_Swf[i++] = formato;
                }
            }

            return -1;
        }

        //Esta función va a buscar los datos de los Bancos a la Base SWIFT
        //Remplaza a la funcion Fn_Trae_Bancos del legacy. Va a la base swift y busca un banco por código swift y retorna si intercambia clave o no
        public static bool BancoIntercambiaClave(InitializationObject initObj, string codBco)
        {
            if (codBco != null && codBco.Length == 11)
            {
                using (UnitOfWorkSwift uow = new UnitOfWorkSwift())
                {
                    sw_bancos banco = uow.BancoRepository.GetBancosByCodAndBranch(codBco.Substring(0, 8), codBco.Substring(8, 3)).FirstOrDefault();
                    if (banco != null)
                    {
                        return banco.intercambio_clave == "S";
                    }
                    else
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = "El banco '" + codBco + "' no existe en la base SWIFT.",
                            Title = "Banco intercambia clave"
                        });

                        return false;
                    }
                }
            }
            else throw new ArgumentException("El código de banco enviado no tiene la longitud esperada (11)");
        }

        public static void Pr_ActSwift(InitializationObject initObj, short ind, short GrabSW, int CorSwi)
        {
            initObj.MODGSWF.VSwf[ind].GrabSW = GrabSW;
            initObj.MODGSWF.VSwf[ind].CorSwi = CorSwi;
        }

        //Graba el Correlativo del mensaje
        public static short Fn_PutSwfSCE(InitializationObject initObj, UnitOfWorkCext01 unit, string RutAis, string Corr, short estado, string TipGra, string numneg, string tippro, string NumCpa, string numcuo, string NumCob, short TipMT)
        {
            using (var tracer = new Tracer())
            {

                short _retValue = 0;
                try
                {
                    unit.SceRepository.Sce_mts_i01(
                        initObj.Module1.Codop.Cent_Costo,
                        initObj.Module1.Codop.Id_Product,
                        initObj.Module1.Codop.Id_Especia,
                        initObj.Module1.Codop.Id_Empresa,
                        initObj.Module1.Codop.Id_Operacion,
                        MODGSYB.dbDecimal(numneg),
                        MODGSYB.dbDecimal(tippro),
                        MODGSYB.dbDecimal(NumCpa),
                        MODGSYB.dbDecimal(numcuo),
                        MODGSYB.dbDecimal(NumCob),
                        MODGSYB.dbDecimal(RutAis),
                        DateTime.Now.Date,
                        MODGSYB.dbDecimal(Corr),
                        estado,
                        TipGra,
                        initObj.MODGCON0.VMch.NroRpt,
                        TipMT);
                    ;

                    _retValue = (short)(true ? -1 : 0);
                }
                catch (Exception _ex)
                {
                    tracer.TraceException("Alerta:", _ex);

                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "No se pudo guardar el nro correlativo",
                        Title = "Error al guardar"
                    });
                }
                return _retValue;
            }
        }

        public static string Fn_FormateaString(ref string cadena, short largo)
        {

            string dbcom = @"""";

            cadena = MODGPYF0.Componer(cadena, "=", " ");
            cadena = MODGPYF0.Componer(cadena, "&", " ");
            cadena = MODGPYF0.Componer(cadena, "$", "");
            cadena = MODGPYF0.Componer(cadena, ";", " ");
            cadena = MODGPYF0.Componer(cadena, "@", " ");
            cadena = MODGPYF0.Componer(cadena, "#", "N");
            cadena = MODGPYF0.Componer(cadena, "%", " ");
            cadena = MODGPYF0.Componer(cadena, "!", " ");
            cadena = MODGPYF0.Componer(cadena, "'", " ");
            cadena = MODGPYF0.Componer(cadena, "*", " ");
            cadena = MODGPYF0.Componer(cadena, ">", " ");
            cadena = MODGPYF0.Componer(cadena, "<", " ");
            cadena = MODGPYF0.Componer(cadena, "º", "");
            cadena = MODGPYF0.Componer(cadena, "ª", "");
            cadena = MODGPYF0.Componer(cadena, "Ñ", "N");
            cadena = MODGPYF0.Componer(cadena, "ñ", "n");
            cadena = MODGPYF0.Componer(cadena, "á", "a");
            cadena = MODGPYF0.Componer(cadena, "é", "e");
            cadena = MODGPYF0.Componer(cadena, "í", "i");
            cadena = MODGPYF0.Componer(cadena, "ó", "o");
            cadena = MODGPYF0.Componer(cadena, "ú", "u");
            cadena = MODGPYF0.Componer(cadena, "ü", "u");
            cadena = MODGPYF0.Componer(cadena, "Á", "A");
            cadena = MODGPYF0.Componer(cadena, "É", "E");
            cadena = MODGPYF0.Componer(cadena, "Í", "I");
            cadena = MODGPYF0.Componer(cadena, "Ó", "O");
            cadena = MODGPYF0.Componer(cadena, "Ú", "U");
            cadena = MODGPYF0.Componer(cadena, "Ü", "U");
            cadena = MODGPYF0.Componer(cadena, "´", "");
            cadena = MODGPYF0.Componer(cadena, "`", "");
            dbcom = VB6Helpers.Mid(dbcom, 1, 1);
            cadena = MODGPYF0.Componer(cadena, dbcom, "");

            if (VB6Helpers.Len(cadena) > largo)
            {
                cadena = VB6Helpers.Mid(cadena, 1, largo);
            }

            return cadena;
        }

        /// <summary>
        /// Funcion utilizada para extraer los campos manuales desde el mensaje
        /// </summary>
        /// <param name="MT">Mensaje Swift codificado</param>
        /// <param name="campo">Campo a extraer</param>
        /// <param name="titulo">Titulo del campo a eliminar</param>
        /// <returns></returns>
        public static string Fn_CampoMTManual(string MT, string campo, string titulo)
        {
            string texto = "";
            var pos = MT.IndexOf(": " + campo + ":");
            if (pos > -1)
            {
                texto = MT.Substring(pos).Replace(": " + campo + ":", "").Replace(titulo, "");
                pos = texto.IndexOf(": ");
                if (pos > -1)
                {
                    texto = texto.Substring(0, pos);
                }
            }
            return texto;
        }
    }
}
