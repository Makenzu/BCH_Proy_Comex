using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODXPLN0
    {
        public static T_MODXPLN0 GetMODXPLN0()
        {
            return new T_MODXPLN0();
        }

        //'Vuelve los montos de la Declaración a Dólar.-
        public static short RPut_xDec(InitializationObject initObject, UnitOfWorkCext01 unit, short CodMnd)
        {
            using (var trace = new Tracer("RPut_xDec: Vuelve los montos de la Declaración a Dólar"))
            {
                T_MODXPLN0 MODXPLN0 = initObject.MODXPLN0;
                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;

                short n = 0;
                double Mtopar = 0;
                //---------------------------------------------------------------------
                //Pasa los montos de dólares a la moneda que se está operando.-
                //---------------------------------------------------------------------
                for (n = 0; n <= (short)VB6Helpers.UBound(MODXPLN0.VxDecP); n++)
                {
                    //Paridad mensual de Declaración Export. (Sgt_Vmd).-
                    Mtopar = MODGTAB1.SyGet_Vmf(initObject, unit, CodMnd, MODXPLN0.VxDecP[n].FecDec, "P");
                    if (Mtopar == 0)
                    {
                        Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Text = "No se ha podido establecer la Paridad del último día hábil del mes anterior. Reporte este problema.",
                            Type = TipoMensaje.Error
                        });
                        trace.TraceError("No se ha podido establecer la Paridad del último día hábil del mes anterior. Reporte este problema.");
                        return 0;
                    }

                    //-----------------------------------------------------------------
                    MODXPLN0.VxDecP[n].ValRet1c = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValRet1c / Mtopar), "0.00"));
                    MODXPLN0.VxDecP[n].ValCom1c = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValCom1c / Mtopar), "0.00"));
                    MODXPLN0.VxDecP[n].ValGas1c = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValGas1c / Mtopar), "0.00"));
                    MODXPLN0.VxDecP[n].ValLiq1c = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValLiq1c / Mtopar), "0.00"));
                    MODXPLN0.VxDecP[n].ValFle1c = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValFle1c / Mtopar), "0.00"));
                    MODXPLN0.VxDecP[n].ValSeg1c = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValSeg1c / Mtopar), "0.00"));
                    //-------------------------------------------------------------
                    MODXPLN0.VxDecP[n].ValRet2c = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValRet2c / Mtopar), "0.00"));
                    MODXPLN0.VxDecP[n].ValCom2c = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValCom2c / Mtopar), "0.00"));
                    MODXPLN0.VxDecP[n].ValGas2c = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValGas2c / Mtopar), "0.00"));
                    MODXPLN0.VxDecP[n].ValLiq2c = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValLiq2c / Mtopar), "0.00"));
                    MODXPLN0.VxDecP[n].ValFle2c = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValFle2c / Mtopar), "0.00"));
                    MODXPLN0.VxDecP[n].ValSeg2c = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValSeg2c / Mtopar), "0.00"));
                    //-------------------------------------------------------------
                }

                return (short)(true ? -1 : 0);
            }
        }

        //Obtiene el disponible de una Declaración.-
        public static void GetDis_xDec(T_MODXPLN0 MODXPLN0, short Indice)
        {
            short n = Indice;
            if (Indice >= 0)
            {
                //Exportador 1.-
                MODXPLN0.VxDecP[n].ValRet1d = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValRet1 - MODXPLN0.VxDecP[n].ValRet1c), "0.00"));
                MODXPLN0.VxDecP[n].ValCom1d = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValCom1 - MODXPLN0.VxDecP[n].ValCom1c), "0.00"));
                MODXPLN0.VxDecP[n].ValGas1d = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValGas1 - MODXPLN0.VxDecP[n].ValGas1c), "0.00"));
                MODXPLN0.VxDecP[n].ValLiq1d = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValLiq1 - MODXPLN0.VxDecP[n].ValLiq1c), "0.00"));
                MODXPLN0.VxDecP[n].ValFle1d = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValFle1 - MODXPLN0.VxDecP[n].ValFle1c), "0.00"));
                MODXPLN0.VxDecP[n].ValSeg1d = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValSeg1 - MODXPLN0.VxDecP[n].ValSeg1c), "0.00"));

                //Exportador 2.-
                MODXPLN0.VxDecP[n].ValRet2d = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValRet2 - MODXPLN0.VxDecP[n].ValRet2c), "0.00"));
                MODXPLN0.VxDecP[n].ValCom2d = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValCom2 - MODXPLN0.VxDecP[n].ValCom2c), "0.00"));
                MODXPLN0.VxDecP[n].ValGas2d = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValGas2 - MODXPLN0.VxDecP[n].ValGas2c), "0.00"));
                MODXPLN0.VxDecP[n].ValLiq2d = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValLiq2 - MODXPLN0.VxDecP[n].ValLiq2c), "0.00"));
                MODXPLN0.VxDecP[n].ValFle2d = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValFle2 - MODXPLN0.VxDecP[n].ValFle2c), "0.00"));
                MODXPLN0.VxDecP[n].ValSeg2d = Format.StringToDouble(Format.FormatCurrency((MODXPLN0.VxDecP[n].ValSeg2 - MODXPLN0.VxDecP[n].ValSeg2c), "0.00"));
            }
        }

        /// <summary>
        /// Pasa la Declaración de disco a memoria.-
        /// Retorna el índice en memoria del arreglo VxDecP().-
        /// En memoria sólo se carga lo disponible de la Declaración.-
        /// </summary>
        /// <param name="initObject"></param>
        /// <param name="unit"></param>
        /// <param name="CodMnd"></param>
        /// <param name="Indice"></param>
        /// <returns></returns>
        public static short Put_xDec(InitializationObject initObject, UnitOfWorkCext01 unit, short CodMnd, short Indice)
        {
            using (var trace = new Tracer("Put_xDec: Pasa la Declaración de disco a memoria. Retorna el índice en memoria del arreglo VxDecP(). En memoria sólo se carga lo disponible de la Declaración"))
            {
                T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;
                T_MODXPLN0 MODXPLN0 = initObject.MODXPLN0;
                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;

                double Mtopar = MODGTAB1.SyGet_Vmf(initObject, unit, CodMnd, VB6Helpers.Format(Mdl_Funciones_Varias.VxDec.FecDec, "dd/MM/yyyy"), "P");
                short n = 0;
                //Paridad mensual de Declaración Export. (Sgt_Vmd).-
                if (Mtopar == 0)
                {
                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Text = "No se ha podido establecer la Paridad del último día hábil del mes anterior. Reporte este problema.",
                        Title = "Paridades Mensuales",
                        Type = TipoMensaje.Error
                    });
                    trace.TraceError("No se ha podido establecer la Paridad del último día hábil del mes anterior. Reporte este problema.");
                    return 0;
                }

                if (Indice < 0)
                {
                    n = (short)(VB6Helpers.UBound(MODXPLN0.VxDecP));
                    VB6Helpers.RedimPreserve(ref MODXPLN0.VxDecP, 0, n);
                    MODXPLN0.VxDecP[VB6Helpers.UBound(MODXPLN0.VxDecP)] = new T_xDecP();
                }
                else
                {
                    n = Indice;
                }


                //-------------------------------------------------------------
                //Pasa los montos disponibles de US$ a la moneda en cuestión.-
                //-------------------------------------------------------------
                MODXPLN0.VxDecP[n].ValRet1 = Format.StringToDouble(Format.FormatCurrency(((Mdl_Funciones_Varias.VxDec.ValRet1 - Mdl_Funciones_Varias.VxDec.ValRet1c) * Mtopar), "0.00"));
                MODXPLN0.VxDecP[n].ValCom1 = Format.StringToDouble(Format.FormatCurrency(((Mdl_Funciones_Varias.VxDec.ValCom1 - Mdl_Funciones_Varias.VxDec.ValCom1c) * Mtopar), "0.00"));
                MODXPLN0.VxDecP[n].ValGas1 = Format.StringToDouble(Format.FormatCurrency(((Mdl_Funciones_Varias.VxDec.ValGas1 - Mdl_Funciones_Varias.VxDec.ValGas1c) * Mtopar), "0.00"));
                MODXPLN0.VxDecP[n].ValLiq1 = Format.StringToDouble(Format.FormatCurrency(((Mdl_Funciones_Varias.VxDec.ValLiq1 - Mdl_Funciones_Varias.VxDec.ValLiq1c) * Mtopar), "0.00"));
                MODXPLN0.VxDecP[n].ValFle1 = Format.StringToDouble(Format.FormatCurrency(((Mdl_Funciones_Varias.VxDec.ValFle1 - Mdl_Funciones_Varias.VxDec.ValFle1c) * Mtopar), "0.00"));
                MODXPLN0.VxDecP[n].ValSeg1 = Format.StringToDouble(Format.FormatCurrency(((Mdl_Funciones_Varias.VxDec.ValSeg1 - Mdl_Funciones_Varias.VxDec.ValSeg1c) * Mtopar), "0.00"));
                //-------------------------------------------------------------
                MODXPLN0.VxDecP[n].ValRet1c = Format.StringToDouble(Format.FormatCurrency(0, "0.00"));
                MODXPLN0.VxDecP[n].ValCom1c = Format.StringToDouble(Format.FormatCurrency(0, "0.00"));
                MODXPLN0.VxDecP[n].ValGas1c = Format.StringToDouble(Format.FormatCurrency(0, "0.00"));
                MODXPLN0.VxDecP[n].ValLiq1c = Format.StringToDouble(Format.FormatCurrency(0, "0.00"));
                MODXPLN0.VxDecP[n].ValFle1c = Format.StringToDouble(Format.FormatCurrency(0, "0.00"));
                MODXPLN0.VxDecP[n].ValSeg1c = Format.StringToDouble(Format.FormatCurrency(0, "0.00"));
                //-------------------------------------------------------------
                MODXPLN0.VxDecP[n].ValRet2 = Format.StringToDouble(Format.FormatCurrency(((Mdl_Funciones_Varias.VxDec.ValRet2 - Mdl_Funciones_Varias.VxDec.ValRet2c) * Mtopar), "0.00"));
                MODXPLN0.VxDecP[n].ValCom2 = Format.StringToDouble(Format.FormatCurrency(((Mdl_Funciones_Varias.VxDec.ValCom2 - Mdl_Funciones_Varias.VxDec.ValCom2c) * Mtopar), "0.00"));
                MODXPLN0.VxDecP[n].ValGas2 = Format.StringToDouble(Format.FormatCurrency(((Mdl_Funciones_Varias.VxDec.ValGas2 - Mdl_Funciones_Varias.VxDec.ValGas2c) * Mtopar), "0.00"));
                MODXPLN0.VxDecP[n].ValLiq2 = Format.StringToDouble(Format.FormatCurrency(((Mdl_Funciones_Varias.VxDec.ValLiq2 - Mdl_Funciones_Varias.VxDec.ValLiq2c) * Mtopar), "0.00"));
                MODXPLN0.VxDecP[n].ValFle2 = Format.StringToDouble(Format.FormatCurrency(((Mdl_Funciones_Varias.VxDec.ValFle2 - Mdl_Funciones_Varias.VxDec.ValFle2c) * Mtopar), "0.00"));
                MODXPLN0.VxDecP[n].ValSeg2 = Format.StringToDouble(Format.FormatCurrency(((Mdl_Funciones_Varias.VxDec.ValSeg2 - Mdl_Funciones_Varias.VxDec.ValSeg2c) * Mtopar), "0.00"));
                //-------------------------------------------------------------
                MODXPLN0.VxDecP[n].ValRet2c = Format.StringToDouble(Format.FormatCurrency(0, "0.00"));
                MODXPLN0.VxDecP[n].ValCom2c = Format.StringToDouble(Format.FormatCurrency(0, "0.00"));
                MODXPLN0.VxDecP[n].ValGas2c = Format.StringToDouble(Format.FormatCurrency(0, "0.00"));
                MODXPLN0.VxDecP[n].ValLiq2c = Format.StringToDouble(Format.FormatCurrency(0, "0.00"));
                MODXPLN0.VxDecP[n].ValFle2c = Format.StringToDouble(Format.FormatCurrency(0, "0.00"));
                MODXPLN0.VxDecP[n].ValSeg2c = Format.StringToDouble(Format.FormatCurrency(0, "0.00"));

                MODXPLN0.VxDecP[n].numdec = Mdl_Funciones_Varias.VxDec.numdec;
                MODXPLN0.VxDecP[n].FecDec = Mdl_Funciones_Varias.VxDec.FecDec;
                MODXPLN0.VxDecP[n].CodAdn = Mdl_Funciones_Varias.VxDec.CodAdn;
                MODXPLN0.VxDecP[n].Estado = Mdl_Funciones_Varias.VxDec.estado;
                MODXPLN0.VxDecP[n].TipDec = Mdl_Funciones_Varias.VxDec.TipDec;
                MODXPLN0.VxDecP[n].CodCCv = Mdl_Funciones_Varias.VxDec.CodCCv;

                MODXPLN0.VxDecP[n].PrtExp1 = MODGPYF0.Componer(Mdl_Funciones_Varias.VxDec.PrtExp1, "~", "");
                MODXPLN0.VxDecP[n].IndNom1 = Mdl_Funciones_Varias.VxDec.IndNom1;
                MODXPLN0.VxDecP[n].IndDir1 = Mdl_Funciones_Varias.VxDec.IndDir1;

                MODXPLN0.VxDecP[n].PrtExp2 = MODGPYF0.Componer(Mdl_Funciones_Varias.VxDec.PrtExp2, "~", "");
                MODXPLN0.VxDecP[n].IndNom2 = Mdl_Funciones_Varias.VxDec.IndNom2;
                MODXPLN0.VxDecP[n].IndDir2 = Mdl_Funciones_Varias.VxDec.IndDir2;

                MODXPLN0.VxDecP[n].FecRet = Mdl_Funciones_Varias.VxDec.FecRet;
                MODXPLN0.VxDecP[n].CodPbc = Mdl_Funciones_Varias.VxDec.CodPbc;
                MODXPLN0.VxDecP[n].NumInf = Mdl_Funciones_Varias.VxDec.NumInf;
                MODXPLN0.VxDecP[n].FecInf = Mdl_Funciones_Varias.VxDec.FecInf;

                return n;
            }
        }

        //Lee una Declaración de Exportación en Memoria.-
        //Retorno    >= 0 : Indice de la Dec. en arreglo VxDecP().-
        //           = -1 : Dec. NO existe en VxDecP().-
        public static short Get_xDec(InitializationObject initObject, string numdec, string FecDec, short CodAdn)
        {
            using (var trace = new Tracer("Get_xDec: Lee una Declaración de Exportación en Memoria"))
            {
                T_MODXPLN0 MODXPLN0 = initObject.MODXPLN0;
                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;

                short _retValue = 0;
                short i = 0;
                short n = 0;

                try
                {
                    //Se busca la Declaración en memoria.-
                    for (i = 0; i <= (short)VB6Helpers.UBound(MODXPLN0.VxDecP); i++)
                    {
                        if (MODXPLN0.VxDecP[i].numdec == numdec &&
                            VB6Helpers.Format(MODXPLN0.VxDecP[i].FecDec, "dd/MM/yyyy") == VB6Helpers.Format(FecDec, "dd/MM/yyyy") &&
                            MODXPLN0.VxDecP[i].CodAdn == CodAdn)
                        {
                            n = i;
                            break;
                        }

                    }

                    _retValue = n;
                }
                catch (Exception _ex)
                {
                    trace.TraceException("Alerta", _ex);
                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                        Type = TipoMensaje.Error
                    });
                    _retValue = -1;
                }
                return _retValue;
            }
        }

        //****************************************************************************
        //   1.  Graba una Declaración de Planilla.
        //   2.  Retorno    = True  : Grabación Exitosa.
        //                  = False : Error o Grabación no Exitosa.
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        // UPGRADE_INFO (#0561): The 'CodNeg' symbol was defined without an explicit "As" clause.
        // UPGRADE_INFO (#0561): The 'CodSec' symbol was defined without an explicit "As" clause.
        public static short SyPutn_xDep(InitializationObject initObject, UnitOfWorkCext01 unit, string NumOpe, dynamic CodNeg, dynamic CodSec, short nrocan)
        {
            using (var tracer = new Tracer("Graba Declaración de Planilla - SyPutn_xDep"))
            {
                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;
                T_MODXPLN0 MODXPLN0 = initObject.MODXPLN0;

                short _retValue = 0;
                short i = 0;
                short n = 0;
                short HayErr = 0;

                n = (short)VB6Helpers.UBound(MODXPLN0.VxDecP);

                for (i = 0; i <= (short)n; i++)
                {
                    try
                    {
                        List<string> parameters = new List<string>();


                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)));
                        parameters.Add(MODGSYB.dbnumesy(CodNeg));
                        parameters.Add(MODGSYB.dbnumesy(CodSec));
                        parameters.Add(MODGSYB.dbnumesy(nrocan));
                        parameters.Add(MODGSYB.dbcharSy(MODXPLN0.VxDecP[i].numdec));
                        parameters.Add(MODGSYB.dbdatesy(MODXPLN0.VxDecP[i].FecDec));
                        parameters.Add(MODGSYB.dbnumesy(MODXPLN0.VxDecP[i].CodAdn));
                        parameters.Add(MODGSYB.dbcharSy(MODXPLN0.VxDecP[i].PrtExp1));
                        parameters.Add(MODGSYB.dbnumesy(MODXPLN0.VxDecP[i].IndNom1));
                        parameters.Add(MODGSYB.dbnumesy(MODXPLN0.VxDecP[i].IndDir1));
                        parameters.Add(MODGSYB.dbnumesy(MODXPLN0.VxDecP[i].ValRet1c));
                        parameters.Add(MODGSYB.dbnumesy(MODXPLN0.VxDecP[i].ValCom1c));
                        parameters.Add(MODGSYB.dbnumesy(MODXPLN0.VxDecP[i].ValGas1c));
                        parameters.Add(MODGSYB.dbnumesy(MODXPLN0.VxDecP[i].ValLiq1c));
                        parameters.Add(MODGSYB.dbnumesy(MODXPLN0.VxDecP[i].ValFle1c));
                        parameters.Add(MODGSYB.dbnumesy(MODXPLN0.VxDecP[i].ValSeg1c));
                        parameters.Add(MODGSYB.dbcharSy(MODXPLN0.VxDecP[i].PrtExp2));
                        parameters.Add(MODGSYB.dbnumesy(MODXPLN0.VxDecP[i].IndNom2));
                        parameters.Add(MODGSYB.dbnumesy(MODXPLN0.VxDecP[i].IndDir2));
                        parameters.Add(MODGSYB.dbnumesy(MODXPLN0.VxDecP[i].ValRet2c));
                        parameters.Add(MODGSYB.dbnumesy(MODXPLN0.VxDecP[i].ValCom2c));
                        parameters.Add(MODGSYB.dbnumesy(MODXPLN0.VxDecP[i].ValGas2c));
                        parameters.Add(MODGSYB.dbnumesy(MODXPLN0.VxDecP[i].ValLiq2c));
                        parameters.Add(MODGSYB.dbnumesy(MODXPLN0.VxDecP[i].ValFle2c));
                        parameters.Add(MODGSYB.dbnumesy(MODXPLN0.VxDecP[i].ValSeg2c));

                        int res = unit.SceRepository.EjecutarSP<int>("sce_xdep_i01", parameters.ToArray()).First();
                        if (res == 9)
                        {
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Text = "Se ha producido un error al tratar de grabar los Rebajes de las Declaraciones de Exportación (Sce_xDep).",
                                Type = TipoMensaje.Error
                            });
                            HayErr = (short)(true ? -1 : 0);
                        }
                    }
                    catch (Exception e)
                    {
                        tracer.TraceException("Alerta", e);

                        Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Text = "Se ha producido un error al tratar de grabar los Rebajes de las Declaraciones de Exportación (Sce_xDep).",
                            Type = TipoMensaje.Error
                        });
                        HayErr = (short)(true ? -1 : 0);
                    }
                }

                if (~HayErr != 0)
                {
                    _retValue = (short)(true ? -1 : 0);
                }
                return _retValue;
            }
        }

        //Rebaja el Saldo de una Declaración de Exportación.-
        //Retorno    = True  : Grabación Exitosa.-
        //           = False : Error o Grabación no Exitosa.-
        // UPGRADE_INFO (#0561): The 'SyPutReb_xDec' symbol was defined without an explicit "As" clause.
        // UPGRADE_INFO (#0561): The 'CodNeg' symbol was defined without an explicit "As" clause.
        // UPGRADE_INFO (#0561): The 'CodSec' symbol was defined without an explicit "As" clause.
        public static dynamic SyPutReb_xDec(InitializationObject initObject, UnitOfWorkCext01 unit, string OpeSin, dynamic CodNeg, dynamic CodSec, short nrocan)
        {
            using (var tracer = new Tracer("Rebaja Saldo Declaración de Exportación - SyPutReb_xDec"))
            {
                T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;

                dynamic _retValue = null;
                string Que = "";
                try
                {
                    _retValue = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Cmd_Put_New(Mdl_Funciones_Varias, () =>
                    {
                        return (short)unit.SceRepository.sce_xdec_u01_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 1, 3)),
                            MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 4, 2)),
                            MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 6, 2)),
                            MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 8, 3)),
                            MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 11, 5)),
                            CodNeg,
                            CodSec,
                            nrocan);
                    });
                }
                catch (Exception _ex)
                {
                    tracer.TraceException("Alerta", _ex);
                    _retValue = 0;
                }
                return _retValue;
            }
        }


        /// <summary>
        /// Entrega el Indice de una Declaración de Exportación que esté en memoria
        /// pero que su saldo NO ha sido utilizado.-        
        /// </summary>
        /// <param name="numdec"></param>
        /// <param name="FecDec"></param>
        /// <param name="CodAdn"></param>
        /// <returns> > 0 : Indice de la Dec. en arreglo VxDecP().-
        ///            =  -1 : Dec. NO existe en VxDecP().- </returns>
        public static short Releer_xDec(T_MODXPLN0 modxpln0, string numdec, string FecDec, short CodAdn)
        {
            short _retValue = -1;
            short i = 0;
            double Suma1 = 0;
            double Suma2 = 0;
            short n = 0;
            try
            {
                //Se busca la Declaración en memoria.-
                for (i = 0; i <= (short)VB6Helpers.UBound(modxpln0.VxDecP); i++)
                {
                    if (modxpln0.VxDecP[i].numdec == numdec && VB6Helpers.Format(modxpln0.VxDecP[i].FecDec, "dd/MM/yyyy") ==
                        VB6Helpers.Format(FecDec, "dd/MM/yyyy") && modxpln0.VxDecP[i].CodAdn == CodAdn)
                    {
                        //Exportador 1.-
                        Suma1 = modxpln0.VxDecP[i].ValRet1c + modxpln0.VxDecP[i].ValCom1c + modxpln0.VxDecP[i].ValGas1c +
                            modxpln0.VxDecP[i].ValLiq1c + modxpln0.VxDecP[i].ValFle1c + modxpln0.VxDecP[i].ValSeg1c;
                        //Exportador 2.-
                        Suma2 = modxpln0.VxDecP[i].ValRet2c + modxpln0.VxDecP[i].ValCom2c + modxpln0.VxDecP[i].ValGas2c +
                            modxpln0.VxDecP[i].ValLiq2c + modxpln0.VxDecP[i].ValFle2c + modxpln0.VxDecP[i].ValSeg2c;
                        if (Format.StringToDouble(VB6Helpers.Format(VB6Helpers.CStr(Suma1), "0.00")) == 0 &&
                            Format.StringToDouble(VB6Helpers.Format(VB6Helpers.CStr(Suma2), "0.00")) == 0)
                        {
                            n = i;
                            break;
                        }

                    }

                }

                _retValue = n;

                return _retValue;
            }
            catch (Exception _ex)
            {
                return _retValue;
            }
        }

    }
}
