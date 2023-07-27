using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODGCON0
    {
        public static T_MODGCON0 GetMODGCON0()
        {
            return new T_MODGCON0();
        }

        //Retorna el nemónico de la Cuenta Contable según WorkSheet.-
        public static string GetWorkSheet(string Codigo)
        {

            switch (Codigo)
            {
                case "1":  //Cobranza Exterior.-
                    return "CEXE";
                case "2":  //Depositantes.-
                    return "DEXE";
                case "3":  //Corresponsales.-
                    return "ACE";
                case "4":  //Retorno.-
                    return "RETEXE";
                case "5":  //Conversión.-
                    return "CONV";
                case "6":  //Cambio.-
                    return "CAMBIO";
                case "7":  //IVA M/E.-
                    return "IVAE";
                case "8":  //IVA $.-
                    return "IVA$";
                case "9":  //BANCO CENTRAL.-
                    return "BANCENE";
                case "10":
                    return "COMIS$";
            }

            return "";
        }

        public static short GetIndMcd(InitializationObject initiObject, UnitOfWorkCext01 unit)
        {
            T_MODGTAB0 MODGTAB0 = initiObject.MODGTAB0;
            T_MODGNCTA MODGNCTA = initiObject.MODGNCTA;

            short n;
            short m;
            initiObject.MODGCON0.VMcd.NemMon = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_NemMnd(MODGTAB0, unit, initiObject.MODGCON0.VMcd.CodMon);
            n = (short)(VB6Helpers.UBound(initiObject.MODGCON0.VMcds));
            m = (short)BCH.Comex.Core.BL.XCFT.Modulos.MODGNCTA.Get_Cta(initiObject.MODGCON0.VMcd.NemCta, initiObject, unit);
            initiObject.MODGCON0.VMcd.NumCta = MODGNCTA.VCta[m].Cta_Num.Value;
            initiObject.MODGCON0.VMcd.IntCIT = MODGNCTA.VCta[m].Cta_CIT;
            initiObject.MODGCON0.VMcd.IntCVT = MODGNCTA.VCta[m].Cta_CVT;
            initiObject.MODGCON0.VMcd.intcap = MODGNCTA.VCta[m].Cta_CAP;
            initiObject.MODGCON0.VMcd.IntCTD = MODGNCTA.VCta[m].Cta_CTD;
            initiObject.MODGCON0.VMcd.IntPOS = MODGNCTA.VCta[m].Cta_POS;
            initiObject.MODGCON0.VMcd.IntCDR = MODGNCTA.VCta[m].Cta_CDR;
            initiObject.MODGCON0.VMcd.NroTOp = MODGNCTA.VCta[m].Cta_NroTO;
            initiObject.MODGCON0.VMcd.IndTOp = MODGNCTA.VCta[m].Cta_IndTO;
            //initiObject.MODGCON0.VMcd.nroimp = n;   
            initiObject.MODGCON0.VMcd.nroimp = (short)(n == -1 ? 1 : initiObject.MODGCON0.VMcd.nroimp);
            VB6Helpers.RedimPreserve(ref initiObject.MODGCON0.VMcds, 0, ++n);
            initiObject.MODGCON0.VMcds[n] = initiObject.MODGCON0.VMcd.Copy();
            return n;
        }

        //Valida que la Contabilidad esté OK.-
        public static short ValidaContab(InitializationObject initObject, UnitOfWorkCext01 unit, int NroRpt, string fecmov)
        {
            short _retValue = 0;
            string mensaje = "";
            string Que = "";
            string R = "";
            //Si viene un Reporte Nulo => Sali como exitoso.-
            if (NroRpt == 0)
            {
                return (short)(true ? -1 : 0);
            }
            using (var tracer = new Tracer("ValidaContab"))
            {
                try
                {
                    unit.SceRepository.ReadQuerySP((reader) =>
                    {
                        if (reader.Read())
                        {
                            if (reader.GetDecimal(0) == 0)
                            {
                                _retValue = -1;
                            }
                            else
                            {
                                _retValue = 0;
                                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                                {
                                    Text = reader.GetString(1),
                                    Type = TipoMensaje.Error
                                });
                            }
                        }
                        else
                        {
                            _retValue = 0;
                        }
                    }, "sce_mcd_s11", NroRpt.ToString(), fecmov);
                    tracer.AddToContext("sce_mcd_s11", "El resultado de la validacion es: " + (_retValue == 0 ? "ERROR" : "CORRECTO"));
                    ////TODO: GRABAR -> SE PONE EN -1 PARA SEGUIR PROBANDO
                    //_retValue = -1;
                }
                catch (Exception _ex)
                {
                    tracer.TraceException("sce_mcd_s11", _ex);
                    _retValue = 0;

                }
                return _retValue;
            }
        }

        public static int GetNroPar(InitializationObject initObj)
        {
            return (int)VB6Helpers.Val(initObj.MODGUSR.UsrEsp.Especialista + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.TimeOfDay), "hhmmss"));
        }

        //Escribe los Movimientos Contables en Sybase.-
        public static short SyPutCon(InitializationObject initObj, UnitOfWorkCext01 unit, string Usuario, short ImpDeb)
        {
            short Ok = (short)(true ? -1 : 0);
            short Y = 0;
            short x = 0;
            short Hab_CtaCteLinea = 0;
            if (~Refunde(initObj, unit, ImpDeb) != 0)
            {
                return 0;
            }
            if (VB6Helpers.UBound(initObj.MODGCON0.VMcds) >= 0)
            {
                x = SyPut_Mch(initObj, unit, Usuario);
                if (~x != 0)
                {
                    Ok = (short)(false ? -1 : 0);
                }

                if (Hab_CtaCteLinea != 0)
                {
                    Y = SyPutn_Mcd_CCLin(initObj, unit, Usuario);
                }
                else
                {
                    Y = SyPutn_Mcd(initObj, unit, Usuario);
                }

                if (~Y != 0)
                {
                    Ok = (short)(false ? -1 : 0);
                }
            }
            else
            {
                initObj.MODGCON0.VMch = initObj.MODGCON0.VmchNul;
            }

            return Ok;
        }

        //Graba el Detalle del Reporte Contable (Sce_Mcd).-
        //Retorno    = True  : Grabación Exitosa.-
        //           = False : Error o Grabación no Exitosa.-
        public static short SyPutn_Mcd(InitializationObject initObj, UnitOfWorkCext01 unit, string Usuario)
        {
            using (var tracer = new Tracer())
            {
                short _retValue = 0;
                short HayError = 0;
                short i = 0;
                string Que = "";
                int R = 0;
                UI_Mdi_Principal Mdi_Principal = initObj.Mdi_Principal;
                try
                {
                    HayError = (short)(false ? -1 : 0);

                    //Graba los movimientos de un Reporte Contable.-
                    for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODGCON0.VMcds); i++)
                    {
                        //Se incluyen estas validaciones anexas.-
                        if (initObj.MODGCON0.VMcds[i].CodMon == 0)
                        {
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Informacion,
                                Text = "Existe un movimiento contable que no tiene Nemónico de Moneda.",
                                Title = T_MODGCON0.MsgCon
                            });

                            return _retValue;
                        }

                        if (string.IsNullOrEmpty(initObj.MODGCON0.VMcds[i].NemMon))
                        {
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Informacion,
                                Text = "Existe un movimiento contable que no tiene Código de Moneda.",
                                Title = T_MODGCON0.MsgCon
                            });

                            return _retValue;
                        }

                        if (initObj.MODGCON0.VMcds[i].mtomcd == 0)
                        {
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Informacion,
                                Text = "Existe un movimiento contable que no tiene un Monto válido.",
                                Title = T_MODGCON0.MsgCon
                            });

                            return _retValue;
                        }

                        if (string.IsNullOrEmpty(initObj.MODGCON0.VMcds[i].NemCta))
                        {
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Informacion,
                                Text = "Existe un movimiento contable que no tiene Nemónico de Cuenta Contable.",
                                Title = T_MODGCON0.MsgCon
                            });

                            return _retValue;
                        }

                        //Genera consulta.-
                        string sp = string.Empty;//probando
                        if (initObj.MODCVDIM.Gvar_NotaCredito == 1 && initObj.MODGCVD.TIN == true)
                        {
                            sp = "sce_mcd_i01_gl01_MS ";
                        }
                        else
                        {
                            sp = "sce_mcd_i01_MS ";
                        }

                        List<string> parameters = new List<string>();

                        Que = VB6Helpers.LCase(Que);
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.codcct));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.codpro));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.codesp));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.codofi));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.codope));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMch.CodNeg));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMch.CodSec));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].NroRpt));
                        parameters.Add(MODGSYB.dbdatesy(initObj.MODGCON0.VMcds[i].fecmov));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Left(Usuario, 3)));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Right(Usuario, 2)));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].nroimp));
                        parameters.Add(MODGSYB.dbnumesy(T_MODGCON0.ECC_ANU));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].TipMcd));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].IdnCta));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].NemCta));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].NumCta));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].CodMon));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].NemMon));
                        //aca
                        parameters.Add(MODGSYB.dbmontoSy(initObj.MODGCON0.VMcds[i].mtomcd));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].cod_dh));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].NumEmb));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].PrtCli));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].IndCli));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].rutcli));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].PrtBco));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].IndBco));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].RutBco));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].SwiBco));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].CodBco));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].numcct));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].numlin));
                        parameters.Add(MODGSYB.dbdatesy(initObj.MODGCON0.VMcds[i].FecOri));
                        parameters.Add(MODGSYB.dbdatesy(initObj.MODGCON0.VMcds[i].FecVen));
                        parameters.Add(MODGSYB.dbdatesy(initObj.MODGCON0.VMcds[i].FecInt));
                        parameters.Add(MODGSYB.dblogisy2(initObj.MODGCON0.VMcds[i].TasFij));
                        parameters.Add(MODGSYB.dbmontoSy(initObj.MODGCON0.VMcds[i].MtoTas));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].OfiDes));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].NumPar));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].TipMov));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].NroRef));
                        parameters.Add(MODGSYB.dbTCamSy(initObj.MODGCON0.VMcds[i].TipCam));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].NroTOp));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].IndTOp));
                        parameters.Add(MODGSYB.dblogisy2(initObj.MODGCON0.VMcds[i].IntCIT));
                        parameters.Add(MODGSYB.dblogisy2(initObj.MODGCON0.VMcds[i].IntCVT));
                        parameters.Add(MODGSYB.dblogisy2(initObj.MODGCON0.VMcds[i].intcap));
                        parameters.Add(MODGSYB.dblogisy2(initObj.MODGCON0.VMcds[i].IntCTD));
                        parameters.Add(MODGSYB.dblogisy2(initObj.MODGCON0.VMcds[i].IntPOS));
                        parameters.Add(MODGSYB.dblogisy2(initObj.MODGCON0.VMcds[i].IntCDR));
                        parameters.Add(MODGSYB.dblogisy2(initObj.MODGCON0.VMcds[i].McdVig));
                        if (initObj.MODCVDIM.Gvar_NotaCredito == 1 && initObj.MODGCVD.TIN == true)
                        {
                            parameters.Add(initObj.MODCVDIM.VNotaCreGl.NumFac);
                        }

                        try
                        {
                            //aca deberia entrar dos veces
                            R = unit.SceRepository.EjecutarSP<int>(sp, parameters.ToArray()).First();
                            if (R != 0)
                            {
                                HayError = -1;
                            }
                        }
                        catch (Exception e)
                        {
                            tracer.TraceException("Alerta", e);

                            HayError = -1;
                        }

                        //R = unit.SceRepository.EjecutarSP<int>(sp, parameters.ToArray()).First();

                        if (initObj.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
                        {
                            SyGet_Actlinea(initObj, unit, initObj.MODGCON0.VMcds[i].NroRpt, initObj.MODGCON0.VMcds[i].fecmov, initObj.MODGCON0.VMcds[i].nroimp, 0, initObj.MODGCON0.VMcds[i].rutcli);
                        }
                    }

                    if (~HayError != 0)
                    {
                        _retValue = (short)(true ? -1 : 0);
                    }
                    return _retValue;
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta", e);

                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                        Title = T_MODGCON0.MsgCon
                    });

                    throw;
                }
            }
        }

        public static dynamic SyGet_Actlinea(InitializationObject initObj, UnitOfWorkCext01 unit, int NroRpt, string fecmov, short nroimp, short enlinea, string rut)
        {
            dynamic _retValue = null;
            string Que = "";
            string R = "";
            string ResultadoQuery = "";
            List<string> parameters = new List<string>();
            try
            {
                _retValue = false;
                parameters.Add(MODGSYB.dbnumesy(NroRpt));
                parameters.Add(MODGSYB.dbdatesy(fecmov));
                parameters.Add(MODGSYB.dbnumesy(nroimp));
                parameters.Add(MODGSYB.dbnumesy(enlinea));
                parameters.Add(MODGSYB.dbcharSy(rut));

                //Ejecuta Query.-
                int resOp = 0;
                unit.SceRepository.ReadQuerySP((reader) =>
                {
                    if (reader.Read())
                    {
                        resOp = reader.GetInt32(0);
                    }
                    else
                    {
                        resOp = 0;
                    }
                }, "sce_mcd_u70", parameters.ToArray());
                //int rowsAffected = unit.SceRepository.ExecuteNonQuerySP("sce_xdoc_i01_MS", VB6Helpers.Mid(NumOpe, 1, 3), VB6Helpers.Mid(NumOpe, 4, 2), VB6Helpers.Mid(NumOpe, 6, 2), VB6Helpers.Mid(NumOpe, 8, 3), VB6Helpers.Mid(NumOpe, 11, 5), c.ToString(), VB6Helpers.Left(Usuario, 3), VB6Helpers.Right(Usuario, 2), CodDoc.ToString(), DateTime.Now.ToString("yyyy-MM-dd"), m.ToString());
                //Hace un Put en Sce_xDoc.
                if (resOp != 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "No pudo actualizar la tabla de Contabilidad reporte este problema a sistema.",
                        Title = "Asignación de Rangos"
                    });
                    _retValue = false;
                }
                else
                {
                    _retValue = true;
                }
            }
            catch (Exception _ex)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Informacion,
                    Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                    Title = "Asignación de Rangos"
                });
                return _retValue;
            }
            return _retValue;
        }

        public static short SyPutn_Mcd_CCLin(InitializationObject initObj, UnitOfWorkCext01 unit, string Usuario)
        {
            short _retValue = 0;
            short HayError = 0;
            short i = 0;
            string Que = "";
            string R = "";

            string rut = "";
            short res = 0;
            using (var tracer = new Tracer())
            {

                try
                {
                    HayError = (short)(false ? -1 : 0);

                    //Graba los movimientos de un Reporte Contable.-
                    for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODGCON0.VMcds); i++)
                    {
                        //Se incluyen estas validaciones anexas.-
                        if (initObj.MODGCON0.VMcds[i].CodMon == 0)
                        {
                            tracer.TraceInformation("Existe un movimiento contable que no tiene Nemónico de Moneda.");

                            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Existe un movimiento contable que no tiene Nemónico de Moneda.",
                                Title = "Validación de Datos"
                            });

                            return _retValue;
                        }

                        if (string.IsNullOrEmpty(initObj.MODGCON0.VMcds[i].NemMon))
                        {
                            tracer.TraceInformation("Existe un movimiento contable que no tiene Código de Moneda.");
                            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Existe un movimiento contable que no tiene Código de Moneda.",
                                Title = "Validación de Datos"
                            });

                            return _retValue;
                        }

                        if (initObj.MODGCON0.VMcds[i].mtomcd == 0)
                        {
                            tracer.TraceInformation("Existe un movimiento contable que no tiene Nemónico de Cuenta Contable.");
                            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Existe un movimiento contable que no tiene un Monto válido.",
                                Title = "Validación de Datos"
                            });

                            return _retValue;
                        }

                        if (string.IsNullOrEmpty(initObj.MODGCON0.VMcds[i].NemCta))
                        {
                            tracer.TraceInformation("Existe un movimiento contable que no tiene Nemónico de Cuenta Contable.");
                            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Existe un movimiento contable que no tiene Nemónico de Cuenta Contable.",
                                Title = "Validación de Datos"
                            });

                            return _retValue;
                        }

                        //res = Mdl_SRM.AISGetUsr(initObj.MODGCVD.RutwAis.Value);  // Rut del Especialista
                        rut = VB6Helpers.Mid(initObj.MODGCVD.RutwAis, 1, 8);

                        //Genera consulta.-
                        List<string> parameters = new List<string>();

                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.codcct));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.codpro));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.codesp));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.codofi));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.codope));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMch.CodNeg));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMch.CodSec));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].NroRpt));
                        parameters.Add(MODGSYB.dbdatesy(initObj.MODGCON0.VMcds[i].fecmov));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Left(Usuario, 3)));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Right(Usuario, 2)));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].nroimp + 1));
                        parameters.Add(MODGSYB.dbnumesy(T_MODGCON0.ECC_ANU));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].TipMcd));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].IdnCta));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].NemCta));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].NumCta));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].CodMon));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].NemMon));
                        parameters.Add(MODGSYB.dbmontoSyForRead(initObj.MODGCON0.VMcds[i].mtomcd));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].cod_dh));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].NumEmb));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].PrtCli));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].IndCli));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].rutcli));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].PrtBco));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].IndBco));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].RutBco));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].SwiBco));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].CodBco));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].numcct));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].numlin));
                        parameters.Add(MODGSYB.dbdatesy(initObj.MODGCON0.VMcds[i].FecOri));
                        parameters.Add(MODGSYB.dbdatesy(initObj.MODGCON0.VMcds[i].FecVen));
                        parameters.Add(MODGSYB.dbdatesy(initObj.MODGCON0.VMcds[i].FecInt));
                        parameters.Add(MODGSYB.dblogisy(initObj.MODGCON0.VMcds[i].TasFij));
                        parameters.Add(MODGSYB.dbtasaSyForRead(initObj.MODGCON0.VMcds[i].MtoTas));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].OfiDes));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].NumPar));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].TipMov));
                        parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMcds[i].NroRef));
                        parameters.Add(MODGSYB.dbTCamSyForRead(initObj.MODGCON0.VMcds[i].TipCam));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].NroTOp));
                        parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMcds[i].IndTOp));
                        parameters.Add(MODGSYB.dblogisy(initObj.MODGCON0.VMcds[i].IntCIT));
                        parameters.Add(MODGSYB.dblogisy(initObj.MODGCON0.VMcds[i].IntCVT));
                        parameters.Add(MODGSYB.dblogisy(initObj.MODGCON0.VMcds[i].intcap));
                        parameters.Add(MODGSYB.dblogisy(initObj.MODGCON0.VMcds[i].IntCTD));
                        parameters.Add(MODGSYB.dblogisy(initObj.MODGCON0.VMcds[i].IntPOS));
                        parameters.Add(MODGSYB.dblogisy(initObj.MODGCON0.VMcds[i].IntCDR));
                        parameters.Add(MODGSYB.dblogisy(initObj.MODGCON0.VMcds[i].McdVig));
                        parameters.Add(MODGSYB.dbcharSy(rut));

                        //Se ejecuta el Procedimiento Almacenado.-
                        int resOp = 0;
                        unit.SceRepository.ReadQuerySP((reader) =>
                        {
                            if (reader.Read())
                            {
                                resOp = reader.GetInt32(0);
                            }
                            else
                            {
                                resOp = -1;
                            }
                        }, "sce_mcd_u70", parameters.ToArray());
                        if (resOp != 0)
                        {
                            //Hubo Error.-
                            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Informacion,
                                Text = "No pudo actualizar la tabla de Contabilidad reporte este problema a sistema.",
                                Title = "Asignación de Rangos"
                            });
                            _retValue = 0;
                        }
                        else
                        {
                            _retValue = -1;
                        }
                    }
                    if (~HayError != 0)
                    {
                        _retValue = (short)(true ? -1 : 0);
                    }
                    return _retValue;
                }
                catch (Exception _ex)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                        Title = T_MODGCON0.MsgCon
                    });

                    if (!ExceptionPolicy.HandleException(_ex, "PoliticaBLFundTransfer")) throw;
                }

                return _retValue;
            }
        }

        //Graba el Encabezado del Reporte Contable (Sce_Mch).-
        //Retorno    = True  : Grabación Exitosa.-
        //           = False : Error o Grabación no Exitosa.-
        public static short SyPut_Mch(InitializationObject initObj, UnitOfWorkCext01 unit, string Usuario)
        {
            using (var trace = new Tracer())
            {
                short _retValue = 0;
                string Que = "";
                string R = "";
                short HayError = 0;
                List<string> parameters = new List<string>();
                try
                {
                    //Genera consulta.-
                    Que = "";
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.codcct));
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.codpro));
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.codesp));
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.codofi));
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.codope));
                    parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMch.CodNeg));
                    parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMch.CodSec));
                    parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMch.NroRpt));
                    parameters.Add(MODGSYB.dbdatesy(initObj.MODGCON0.VMch.fecmov));
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Left(Usuario, 3)));
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Right(Usuario, 2)));
                    parameters.Add(MODGSYB.dbnumesy(T_MODGCON0.ECC_ANU));
                    parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMch.OfiCon));
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.PrtCli));
                    parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMch.IndCli));
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.rutcli));
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.NomCli));
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.DirCli));
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.ComCli));
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.CiuCli));
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.PaiCli));
                    parameters.Add(MODGSYB.dbnumesy(initObj.MODGCON0.VMch.codfun));
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.operel));
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCON0.VMch.DesGen));

                    //Se ejecuta el Procedimiento Almacenado.-  
                    decimal resOpe = -1;
                    unit.SceRepository.ReadQuerySP((reader) =>
                    {
                        if (reader.Read())
                        {

                            resOpe = reader.GetInt32(0);

                        }
                        else
                        {
                            resOpe = -1;
                        }
                    }, "sce_mch_i01", parameters.ToArray());

                    if (resOpe == 0)
                    {
                        _retValue = (short)(true ? -1 : 0);
                    }

                    return _retValue;
                }
                catch (Exception _ex)
                {
                    trace.TraceException("Alerta", _ex);
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                        Title = T_MODGCON0.MsgCon
                    });
                    throw;
                }
            }
        }

        //Refunde los Movimientos de Cuenta Corriente en Moneda Nacional.-
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short Refunde(InitializationObject initObj, UnitOfWorkCext01 uow, short ImpDeb)
        {
            short n = 0;//probar aca
            short i = 0;
            short x = 0;
            short m = 0;
            short HayCVT = 0;
            bool cuenta40001018 = false;

            initObj.MODGCON0.VMcdz = new T_Mcd[0];

            n = (short)VB6Helpers.UBound(initObj.MODGCON0.VMcds);

            for (i = 0; i <= (short)n; i++)
            {
                if (initObj.MODGCON0.VMcds[i].NumCta == "40001018")
                {
                    if (i > 0 && (initObj.MODGCON0.VMcds[i].numcct == initObj.MODGCON0.VMcds[i - 1].numcct) && (initObj.MODGCON0.VMcds[i].NumCta == initObj.MODGCON0.VMcds[i - 1].NumCta) && (initObj.MODGCON0.VMcds[i].cod_dh == initObj.MODGCON0.VMcds[i - 1].cod_dh))
                    {
                        initObj.MODGCON0.VMcdz[x].mtomcd += initObj.MODGCON0.VMcds[i].mtomcd;
                    }
                    else
                    {

                        //La estructura no se encuantra llenada: x = 0
                        x = (short)VB6Helpers.UBound(initObj.MODGCON0.VMcdz);
                        x = (short)(x + 1);
                        VB6Helpers.RedimPreserve(ref initObj.MODGCON0.VMcdz, 0, x);
                        initObj.MODGCON0.VMcdz[x] = initObj.MODGCON0.VMcds[i].Copy();
                        initObj.MODGCON0.VMcdz[x].nroimp = (short)(x == 0 ? 1 : initObj.MODGCON0.VMcdz[x].nroimp);
                        //initObj.MODGCON0.VMcdz[x].nroimp = x;
                    }

                }
                else if (!cuenta40001018)
                {
                    //entra acacccc   
                    x = (short)VB6Helpers.UBound(initObj.MODGCON0.VMcdz);
                    x = (short)(x + 1);
                    VB6Helpers.RedimPreserve(ref initObj.MODGCON0.VMcdz, 0, x);
                    initObj.MODGCON0.VMcdz[x] = initObj.MODGCON0.VMcds[i].Copy();
                    initObj.MODGCON0.VMcdz[x].nroimp = (short)(x == 0 ? 1 : initObj.MODGCON0.VMcdz[x].nroimp);
                    //initObj.MODGCON0.VMcdz[x].nroimp = x;
                }

                //Si viene Cuenta Corriente => Sumar el Impuesto y el Timbre.-
                if (ImpDeb != 0)
                {
                    //Verifica que esté seteado Impuesto al débito.-
                    if (initObj.MODGCON0.VMcdz[x].NumCta == "40001018" && initObj.MODGCON0.VMcdz[x].cod_dh == "D" && T_MODGMTA.impflag == 1)
                    {
                        // si el flag viene en 1 se cobra el impuesto
                        initObj.MODGCON0.VMcdz[x].mtomcd += initObj.MODGSCE.VGen.MtoDeb;
                        n = (short)(VB6Helpers.UBound(initObj.MODGCON0.VMcdz) + 1);
                        VB6Helpers.RedimPreserve(ref initObj.MODGCON0.VMcdz, 0, n);
                        initObj.MODGCON0.VMcdz[n].codcct = initObj.MODGCON0.VMch.codcct;
                        initObj.MODGCON0.VMcdz[n].codpro = initObj.MODGCON0.VMch.codpro;
                        initObj.MODGCON0.VMcdz[n].codesp = initObj.MODGCON0.VMch.codesp;
                        initObj.MODGCON0.VMcdz[n].codofi = initObj.MODGCON0.VMch.codofi;
                        initObj.MODGCON0.VMcdz[n].codope = initObj.MODGCON0.VMch.codope;
                        initObj.MODGCON0.VMcdz[n].CodNeg = initObj.MODGCON0.VMch.CodNeg;
                        initObj.MODGCON0.VMcdz[n].CodSec = initObj.MODGCON0.VMch.CodSec;
                        initObj.MODGCON0.VMcdz[n].NroRpt = initObj.MODGCON0.VMch.NroRpt;
                        initObj.MODGCON0.VMcdz[n].fecmov = initObj.MODGCON0.VMch.fecmov;
                        initObj.MODGCON0.VMcdz[n].cencos = initObj.MODGCON0.VMch.cencos;
                        initObj.MODGCON0.VMcdz[n].codusr = initObj.MODGCON0.VMch.codusr;
                        initObj.MODGCON0.VMcdz[n].nroimp = n;
                        initObj.MODGCON0.VMcdz[n].Estado = T_MODGCON0.ECC_ING;
                        initObj.MODGCON0.VMcdz[n].TipMcd = T_MODGCON0.CONTAB_ING;

                        m = (short)BCH.Comex.Core.BL.XCFT.Modulos.MODGNCTA.Get_Cta("FIJO$", initObj, uow);
                        if (m == 0)
                        {
                            return 0;
                        }

                        if (T_MODGMTA.impflag == 1)
                        {
                            // si el flag viene en 1 se cobra el impuesto

                            initObj.MODGCON0.VMcdz[n].IdnCta = 0;
                            initObj.MODGCON0.VMcdz[n].NemCta = initObj.MODGNCTA.VCta[m].Cta_Nem.Value;
                            initObj.MODGCON0.VMcdz[n].NumCta = initObj.MODGNCTA.VCta[m].Cta_Num.Value;
                            initObj.MODGCON0.VMcdz[n].CodMon = T_MODGTAB0.MndNac;
                            initObj.MODGCON0.VMcdz[n].NemMon = "$";
                            initObj.MODGCON0.VMcdz[n].mtomcd = initObj.MODGSCE.VGen.MtoDeb;
                            initObj.MODGCON0.VMcdz[n].cod_dh = "H";
                            initObj.MODGCON0.VMcdz[n].NumEmb = 0;
                            initObj.MODGCON0.VMcdz[n].PrtCli = "";
                            initObj.MODGCON0.VMcdz[n].IndCli = 0;
                            initObj.MODGCON0.VMcdz[n].rutcli = "";
                            initObj.MODGCON0.VMcdz[n].PrtBco = "";
                            initObj.MODGCON0.VMcdz[n].IndBco = 0;
                            initObj.MODGCON0.VMcdz[n].RutBco = "";
                            initObj.MODGCON0.VMcdz[n].SwiBco = "";
                            initObj.MODGCON0.VMcdz[n].CodBco = 0;
                            initObj.MODGCON0.VMcdz[n].numcct = "";
                            initObj.MODGCON0.VMcdz[n].numlin = "";
                            initObj.MODGCON0.VMcdz[n].FecOri = "";
                            initObj.MODGCON0.VMcdz[n].FecVen = "";
                            initObj.MODGCON0.VMcdz[n].FecInt = "";
                            initObj.MODGCON0.VMcdz[n].TasFij = 0;
                            initObj.MODGCON0.VMcdz[n].MtoTas = 0;
                            initObj.MODGCON0.VMcdz[n].OfiDes = 0;
                            initObj.MODGCON0.VMcdz[n].NumPar = 0;
                            initObj.MODGCON0.VMcdz[n].TipMov = 0;
                            initObj.MODGCON0.VMcdz[n].NroRef = "";
                            initObj.MODGCON0.VMcdz[n].TipCam = 0;
                            initObj.MODGCON0.VMcdz[n].NroTOp = initObj.MODGNCTA.VCta[m].Cta_NroTO;
                            initObj.MODGCON0.VMcdz[n].IndTOp = initObj.MODGNCTA.VCta[m].Cta_IndTO;
                            initObj.MODGCON0.VMcdz[n].IntCIT = initObj.MODGNCTA.VCta[m].Cta_CIT;
                            initObj.MODGCON0.VMcdz[n].IntCVT = initObj.MODGNCTA.VCta[m].Cta_CVT;
                            initObj.MODGCON0.VMcdz[n].intcap = initObj.MODGNCTA.VCta[m].Cta_CAP;
                            initObj.MODGCON0.VMcdz[n].IntCTD = initObj.MODGNCTA.VCta[m].Cta_CTD;
                            initObj.MODGCON0.VMcdz[n].IntPOS = initObj.MODGNCTA.VCta[m].Cta_POS;
                            initObj.MODGCON0.VMcdz[n].IntCDR = initObj.MODGNCTA.VCta[m].Cta_CDR;
                            initObj.MODGCON0.VMcdz[n].McdVig = (short)(false ? -1 : 0);
                        }

                    }

                }

            }

            //Se generan los movimientos detalle.-
            n = (short)VB6Helpers.UBound(initObj.MODGCON0.VMcdz);
            initObj.MODGCON0.VMcds = new T_Mcd[n + 1];
            for (i = 0; i <= (short)n; i++)
            {
                initObj.MODGCON0.VMcds[i] = initObj.MODGCON0.VMcdz[i].Copy();
            }

            //Verifica que para cuentas de IVA y Comisión, el Party tenga Rut.-
            for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODGCON0.VMcds); i++)
            {
                //If Left$(VMcds(i%).NemCta, 5) = "COMIS" Or Left$(VMcds(i%).NemCta, 5) = "IVA" Then  ACC-001-I Antes
                if ((VB6Helpers.Left(initObj.MODGCON0.VMcds[i].NemCta, 3) == "COM" && VB6Helpers.Left(initObj.MODGCON0.VMcds[i].NumCta, 8) == "22112015") || VB6Helpers.Left(initObj.MODGCON0.VMcds[i].NemCta, 3) == "IVA")
                {
                    //ACC-001-F Nuevo
                    HayCVT = (short)(true ? -1 : 0);
                    break;
                }

            }

            if ((HayCVT & (string.IsNullOrEmpty(initObj.MODGCON0.VMch.rutcli) ? -1 : 0)) != 0)
            {
                VB6Helpers.MsgBox("No se puede grabar el Reporte Contable debido a que generaría una Factura para una persona que NO tiene Rut.", MsgBoxStyle.Information, "Validación Reporte Contable");
                return 0;
            }

            return (short)(true ? -1 : 0);
        }
    }
}
