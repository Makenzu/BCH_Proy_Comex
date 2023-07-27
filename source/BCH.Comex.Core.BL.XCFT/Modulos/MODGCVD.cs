using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;
using BCH.Comex.Common.Tracing;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public static class MODGCVD
    {
        //Retorna el monto en Pesos de todas las Planillas.
        public static double GetMtoNac_xPlv(T_MODXPLN0 MODXPLN0)
        {
            return Format.StringToDouble(Format.FormatCurrency(MODXPLN0.VxDatP.MtoLiq * MODXPLN0.VxDatP.TipCam, "0"));
        }
        //Carga los valores a justificar en las Vías.
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short Llena_VxMtoVia(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            T_MODPREEM MODPREEM = initObject.MODPREEM;
            T_MODANUVI MODANUVI = initObject.MODANUVI;
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_MODGARB MODGARB = initObject.MODGARB;
            T_MODXPLN0 MODXPLN0 = initObject.MODXPLN0;
            T_MODGANU MODGANU = initObject.MODGANU;
            T_MODXANU MODXANU = initObject.MODXANU;
            T_MODGFYS MODGFYS = initObject.MODGFYS;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;

            short ree = 0;
            short anu = 0;
            short i = 0;
            short x = 0;
            double MT = 0;
            short n = 0;
            double AVia = 0;
            short mnd = 0;


            ree = (short)VB6Helpers.UBound(MODPREEM.Vx_PReem);
            anu = (short)VB6Helpers.UBound(MODANUVI.V_PlAnu);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            short _switchVar1 = MODGCVD.VgCvd.TipCVD;
            //Compra-Venta.
            if (_switchVar1 == T_MODGCVD.TCvd_CVD)
            {
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGCVD.VgPli); i++)
                {
                    if (MODGCVD.VgPli[i].Status != T_MODGCVD.EstadoEli)
                    {
                        string _switchVar2 = MODGCVD.VgPli[i].TipCVD;
                        if (_switchVar2 == "C")
                        {
                            //Compra => Se entrega $.
                            x = VB6Helpers.CShort(BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Put_xVia(initObject, unit, MODGCVD.CodMonedaNacional, MODGCVD.VgPli[i].MtoPes, 0));
                        }
                        else if (_switchVar2 == "V" || _switchVar2 == "W")
                        {
                            //Venta  => Se entrega M/E.
                            x = VB6Helpers.CShort(BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Put_xVia(initObject, unit, MODGCVD.VgPli[i].CodMnd, MODGCVD.VgPli[i].MtoCVD, 0));
                        }
                        else if (_switchVar2 == "TI" || _switchVar2 == "TE" || _switchVar2 == "TIN")
                        {
                            x = VB6Helpers.CShort(BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Put_xVia(initObject, unit, MODGCVD.VgPli[i].CodMnd, MODGCVD.VgPli[i].MtoCVD, 0));
                        }

                        if (~x != 0)
                        {
                            return 0;
                        }
                    }

                }

                //Arbitrajes.
            }
            else if (_switchVar1 == T_MODGCVD.TCvd_Arb)
            {
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGARB.VArb); i++)
                {
                    if (MODGARB.VArb[i].Status != T_MODGCVD.EstadoEli)
                    {
                        x = VB6Helpers.CShort(BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Put_xVia(initObject, unit, MODGARB.VArb[i].MndVta, MODGARB.VArb[i].MtoVta, 0));
                        if (~x != 0)
                        {
                            return 0;
                        }
                    }

                }

                //Venta Visible : Liquidar, se entrga $.
            }
            else if (_switchVar1 == T_MODGCVD.TCvd_VisExp)
            {
                if (MODXPLN0.VxDatP.MtoLiq > 0)
                {
                    MT = GetMtoNac_xPlv(MODXPLN0);
                    if (MT > 0)
                    {
                        x = VB6Helpers.CShort(BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Put_xVia(initObject, unit, MODGCVD.CodMonedaNac, MT, 0));
                    }
                }

                //Reverso.
            }
            else if (_switchVar1 == T_MODGCVD.TCvd_Rev)
            {

                for (i = 0; i <= (short)VB6Helpers.UBound(MODGANU.VAnuPl); i++)
                {
                    short _switchVar3 = MODGANU.VAnuPl[i].TipDoc;
                    if (_switchVar3 == T_Mdl_Funciones.TPli_Ingreso || _switchVar3 == T_Mdl_Funciones.TPli_AnuEgr)
                    {
                        //Compra + Anul. Egr.-
                        x = VB6Helpers.CShort(BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Put_xVia(initObject, unit, MODGANU.VAnuPl[i].CodMnd, MODGANU.VAnuPl[i].MtoAnu, 0));
                        if (~x != 0)
                        {
                            return 0;
                        }
                    }
                    else if (_switchVar3 == T_Mdl_Funciones.TPli_Egreso || _switchVar3 == T_Mdl_Funciones.TPli_AnuIng)
                    {
                        //Venta  + Anul. Ing.-
                        x = VB6Helpers.CShort(BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Put_xVia(initObject, unit, MODGCVD.CodMonedaNac, Format.StringToDouble(Format.FormatCurrency(MODGANU.VAnuPl[i].MtoAnu * MODGANU.VAnuPl[i].TipCamo, "0")), 0));
                        if (~x != 0)
                        {
                            return 0;
                        }
                    }

                }

                //Reverso y Reemplazo.-
            }
            else if (_switchVar1 == T_MODGCVD.TCvd_RyR)
            {
                //Reverso.-
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGANU.VAnuPl); i++)
                {
                    if (!string.IsNullOrEmpty(MODGANU.VAnuPl[i].Motivo))
                    {
                        short _switchVar4 = MODGANU.VAnuPl[i].TipDoc;
                        if (_switchVar4 == T_Mdl_Funciones.TPli_Ingreso)
                        {
                            //Compra.-
                            x = VB6Helpers.CShort(BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Put_xVia(initObject, unit, MODGANU.VAnuPl[i].CodMnd, MODGANU.VAnuPl[i].MtoAnu, 0));
                            if (~x != 0)
                            {
                                return 0;
                            }
                        }
                        else if (_switchVar4 == T_Mdl_Funciones.TPli_Egreso)
                        {
                            //Venta.-
                            x = VB6Helpers.CShort(BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Put_xVia(initObject, unit, MODGCVD.CodMonedaNac, Format.StringToDouble(Format.FormatCurrency(MODGANU.VAnuPl[i].MtoAnu * MODGANU.VAnuPl[i].TipCam, "0")), 0));
                            if (~x != 0)
                            {
                                return 0;
                            }
                        }

                    }
                    else
                    {
                        if (MODGANU.VAnuPl[i].VisInv == "VIS")
                        {
                            x = VB6Helpers.CShort(BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Put_xVia(initObject, unit, MODGANU.VAnuPl[i].CodMnd, MODGANU.VAnuPl[i].MtoAnu, 0));
                            if (~x != 0)
                            {
                                return 0;
                            }
                        }

                    }

                }

                //Reemplazo.-
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGCVD.VgPli); i++)
                {
                    if (MODGCVD.VgPli[i].Status != T_MODGCVD.EstadoEli)
                    {
                        string _switchVar5 = MODGCVD.VgPli[i].TipCVD;
                        if (_switchVar5 == "C")
                        {
                            //Compra => Se entrega $.
                            x = VB6Helpers.CShort(BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Put_xVia(initObject, unit, MODGCVD.CodMonedaNac, MODGCVD.VgPli[i].MtoPes, 0));
                        }
                        else if (_switchVar5 == "V" || _switchVar5 == "W")
                        {
                            //Venta  => Se entrega M/E.
                            x = VB6Helpers.CShort(BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Put_xVia(initObject, unit, MODGCVD.VgPli[i].CodMnd, MODGCVD.VgPli[i].MtoCVD, 0));
                        }

                        if (~x != 0)
                        {
                            return 0;
                        }
                    }

                }

                if (MODXPLN0.VxDatP.MtoLiq > 0)
                {
                    MT = GetMtoNac_xPlv(MODXPLN0);
                    if (MT > 0)
                    {
                        x = VB6Helpers.CShort(BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Put_xVia(initObject, unit, MODGCVD.CodMonedaNac, MT, 0));
                    }
                }

                //Anulación de Planillas s/Operación.-
            }
            else if (_switchVar1 == T_MODGCVD.TCvd_PlnoBco)
            {
                for (i = 0; i <= (short)VB6Helpers.UBound(MODXANU.VxAnus); i++)
                {
                    if (((MODXANU.VxAnus[i].Estado != T_MODGCVD.EstadoEli ? -1 : 0) & VB6Helpers.Instr(T_MODXPLN1.PLNLIQ, VB6Helpers.CStr(MODXANU.VxAnus[i].TipPln))) != 0)
                    {
                        n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMndBC(MODGTAB0, unit, MODXANU.VxAnus[i].CodMnd);
                        x = VB6Helpers.CShort(BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Put_xVia(initObject, unit, MODXANU.VxAnus[i].CodMnd, MODXANU.VxAnus[i].MtoAnu, 0));
                    }

                }

            }
            else if (_switchVar1 == T_MODGCVD.TCvd_VisImp)
            {
                //Ventas Visibles Import
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGFYS.VgFyS); i++)
                {
                    if (~MODGFYS.VgFyS[i].Borrado != 0)
                    {

                        if (MODGFYS.VgFyS[i].monto[1] != 0 || MODGFYS.VgFyS[i].monto[2] != 0)
                        {

                            short _switchVar6 = MODGFYS.CVD.Operacion;
                            if (_switchVar6 == T_MODGFYS.FLT)
                            {
                                AVia = MODGFYS.VgFyS[i].monto[1];
                                mnd = MODGFYS.VgFyS[i].CodMon[1];
                            }
                            else if (_switchVar6 == T_MODGFYS.SEG)
                            {
                                AVia = MODGFYS.VgFyS[i].monto[2];
                                mnd = MODGFYS.VgFyS[i].CodMon[1];
                            }
                            else if (_switchVar6 == T_MODGFYS.FLTSEG)
                            {
                                AVia = MODGFYS.VgFyS[i].monto[1] + MODGFYS.VgFyS[i].monto[2];
                                mnd = MODGFYS.VgFyS[i].CodMon[1];
                            }
                            else if (_switchVar6 == T_MODGFYS.ENDREC)
                            {
                                AVia = MODGFYS.VgFyS[i].monto[1];
                                mnd = MODGFYS.VgFyS[i].CodMon[1];
                            }

                            if (AVia > 0)
                            {
                                x = VB6Helpers.CShort(BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Put_xVia(initObject, unit, mnd, MODGPYF1.Dec_EnMto(initObject, unit, AVia, mnd), 0));
                                if (~x != 0)
                                {
                                    return 0;
                                }
                            }

                        }

                    }

                }

            }
            else if (_switchVar1 == T_MODGCVD.TCvd_AnuVisI)
            {
                //Anulacion Planilla Vis. de Importac.-

                if (MODANUVI.Vx_AnuReem.ConvRee - MODANUVI.Vx_AnuReem.ConvAnu > 0)
                {
                    x = VB6Helpers.CShort(BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Put_xVia(initObject, unit, MODANUVI.Vx_AnuReem.CodMon, MODANUVI.Vx_AnuReem.ConvRee - MODANUVI.Vx_AnuReem.ConvAnu, 0));
                }

                if (MODANUVI.Vx_AnuReem.CambAnu - MODANUVI.Vx_AnuReem.CambRee > 0)
                {
                    x = VB6Helpers.CShort(BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Put_xVia(initObject, unit, T_MODGTAB0.MndNac, MODANUVI.Vx_AnuReem.CambAnu - MODANUVI.Vx_AnuReem.CambRee, 0));
                }

            }
            else if (_switchVar1 == T_MODGCVD.TCvd_PlanSO)
            {
                //Planillas Visibles de Imp. Sin Operacion
                for (i = 0; i <= (short)ree; i++)
                {
                    if (MODPREEM.Vx_PReem[i].Estado != 9)
                    {
                        x = VB6Helpers.CShort(BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Put_xVia(initObject, unit, MODPREEM.Vx_PReem[i].CodMon, MODPREEM.Vx_PReem[i].TotOri, 0));
                        if (~x != 0)
                        {
                            return 0;
                        }
                    }

                }

            }

            return (short)(true ? -1 : 0);
        }

        public static short HayDiferencia(T_MODGCVD MODGCVD)
        {
            short res = 0;
            if (MODGCVD.VgCVDNul.codcct != MODGCVD.VgCvd.codcct)
            {
                res = -1;
            }

            if (MODGCVD.VgCVDNul.codpro != MODGCVD.VgCvd.codpro)
            {
                res = -1;
            }

            if (MODGCVD.VgCVDNul.codesp != MODGCVD.VgCvd.codesp)
            {
                res = -1;
            }

            if (MODGCVD.VgCVDNul.codofi != MODGCVD.VgCvd.codofi)
            {
                res = -1;
            }

            if (MODGCVD.VgCVDNul.codope != MODGCVD.VgCvd.codope)
            {
                res = -1;
            }

            if (MODGCVD.VgCVDNul.PrtCli != MODGCVD.VgCvd.PrtCli)
            {
                res = -1;
            }

            if (MODGCVD.VgCVDNul.PrtOtr != MODGCVD.VgCvd.PrtOtr)
            {
                res = -1;
            }

            return res;
        }

        public static T_MODGCVD GetMODGCVD()
        {
            T_MODGCVD mod = new T_MODGCVD()
            {
                CodPlazaCentral = short.Parse(Mdl_Acceso.GetConfigValue("FundTransfer.SceIdi.PlazaCentral")),
                CodMonDolar = short.Parse(Mdl_Acceso.GetConfigValue("FundTransfer.Monedas.CodMonedaDolar")),
                CodMonedaNacional = short.Parse(Mdl_Acceso.GetConfigValue("FundTransfer.Monedas.CodMonedaNacional")),
                VgCVDVacia = new T_gCVD()
                {
                    TcpConDec = Mdl_Acceso.GetConfigValue("FundTransfer.Exportaciones.TcpConDec"),
                    TcpSinPai = Mdl_Acceso.GetConfigValue("FundTransfer.Exportaciones.TcpSinPai"),
                    TcpConvenio = Mdl_Acceso.GetConfigValue("FundTransfer.Exportaciones.TcpConvenio"),
                    TcpAutBcoCen = Mdl_Acceso.GetConfigValue("FundTransfer.Importaciones.TcpAutBcoCen")
                }
            };
            return mod;
        }

        //Retorna el número de Compras-Ventas del arreglo VgPli.
        public static short Count_CVD(T_MODGCVD MODGCVD, T_MODGARB MODGARB)
        {
            short i = 0;
            short n = 0;
            for (i = 0; i <= (short)VB6Helpers.UBound(MODGCVD.VgPli); i++)
            {
                //Compra-Venta.
                if (MODGCVD.VgPli[i].Status != T_MODGCVD.EstadoEli)
                {
                    n = (short)(n + 1);
                }
            }

            for (i = 0; i <= (short)VB6Helpers.UBound(MODGARB.VArb); i++)
            {
                //Arbitrajes.
                if (MODGARB.VArb[i].Status != T_MODGCVD.EstadoEli)
                {
                    n = (short)(n + 1);
                }
            }

            return n;
        }

        public static string GeneraTRXID(string opeSin, UnitOfWorkCext01 uow, List<UI_Message> mensajes)
        {
            using (Tracer tracer = new Tracer())
            {
                string correlativo;
                string fecjul = Fecha_Juliana();

                correlativo = SyGetCorre(uow, mensajes);

                if (string.IsNullOrEmpty(fecjul) || correlativo == "0")
                {
                    mensajes.Add(new UI_Message
                    {
                        Text = "Problemas en la generacion del TransactionId.",
                        Type = TipoMensaje.Critical,
                        Title = T_MODGCVD.MsgCVD
                    });

                    tracer.TraceInformation("Problemas para obtener TransactionId Operacion: ", opeSin);

                    return String.Empty;
                }
                else if (string.IsNullOrEmpty(opeSin) || opeSin.Length < 3)
                {
                    mensajes.Add(new UI_Message
                    {
                        Text = "Problemas en la generacion del TransactionId. Número de operación en blanco.",
                        Type = TipoMensaje.Error,
                        Title = T_MODGCVD.MsgCVD
                    });
                    tracer.TraceInformation("Problemas para obtener TransactionId Nro operacion en blanco", opeSin);
                    return String.Empty;
                }
                else
                {
                    correlativo = correlativo.PadLeft(5, '0');
                    return "CBSCVD" + DateTime.Now.ToString("yyMMdd") + opeSin.Substring(0, 3) + fecjul + correlativo;
                }
            }
        }

        public static string Fecha_Juliana()
        {

            string YYYY = VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Today), "yy");
            short AñoJ = 0;
            string Years = "";
            DateTime MyDate = VB6Helpers.CDate(VB6Helpers.Today);

            if (Format.StringToDouble(YYYY) <= 1999)
            {
                Years = VB6Helpers.Right(VB6Helpers.CStr(YYYY), 2);
            }
            else
            {
                AñoJ = (short)(VB6Helpers.CShort(VB6Helpers.Right(VB6Helpers.CStr(YYYY), 2)) + 100);
                Years = VB6Helpers.CStr(AñoJ);
            }

            return Years + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.DateToDouble(MyDate) - VB6Helpers.DateToDouble(VB6Helpers.DateSerial(VB6Helpers.Year(MyDate) - 1, 12, 31))), "000");
        }

        /// <summary>
        /// Se consulta correlativo trxid
        /// </summary>
        /// <returns></returns>
        public static string SyGetCorre(UnitOfWorkCext01 uow, List<UI_Message> mensajes)
        {
            string _retValue = "";
            string mensaje = "";
            string corre = "";
            int? intCorre;
            string Retorno = "";
            int queryResultCount;

            try
            {
                queryResultCount = uow.SceRepository.pro_sce_trxcor_ft_MS(out Retorno, out mensaje, out intCorre);
                corre = intCorre.HasValue ? intCorre.ToString() : string.Empty;

                _retValue = corre;
            }
            catch (Exception)
            {
                //TODO:@estanislao manejo de excepciones
                mensajes.Add(new UI_Message
                {
                    Text = "Se ha producido un error al tratar de rescatar correlativo de TrxId",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODGCVD.MsgCVD
                });
            }

            return _retValue;
        }

        //****************************************************************************
        //   Deja el arreglo de Conceptos de Planillas VTcp() en una lista.
        //   1:Compra;2:Venta;3:Arbitraje
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static void CargaEnListaTcp(T_ModChVrf ModChVrf, UI_Combo Lista, short Tipo)
        {
            short n = 0;
            short i = 0;
            short Cargar = 0;
            string s = "";
            n = (short)VB6Helpers.UBound(ModChVrf.VCcpl);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            Lista.Items.Clear();
            for (i = 0; i <= n; i++)
            {
                if (VB6Helpers.Val(ModChVrf.VCcpl[i].tipope) >= 110 && VB6Helpers.Val(ModChVrf.VCcpl[i].tipope) <= 240)
                {
                    Cargar = 0;
                    switch (Tipo)
                    {
                        case 1:
                        case 3:  //Compra o Transferencia ingreso
                            if (ModChVrf.VCcpl[i].flging != 0)
                            {
                                Cargar = -1;
                            }
                            break;
                        case 2:
                        case 4:  //Venta o Transferencia egreso
                            if (~ModChVrf.VCcpl[i].flging != 0)
                            {
                                Cargar = -1;
                            }
                            break;
                        case 5:
                            if (ModChVrf.VCcpl[i].CodCom == "100080")
                            {
                                Cargar = -1;
                            }
                            break;
                    }

                    if (Cargar != 0)
                    {
                        s = "";
                        // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 's' variable as a StringBuilder6 object.
                        s = s + VB6Helpers.Trim(ModChVrf.VCcpl[i].CodCom + "-" + ModChVrf.VCcpl[i].CptCom) + VB6Helpers.Space(2);
                        s += VB6Helpers.Trim(VB6Helpers.Mid(MODGPYF1.Minuscula(ModChVrf.VCcpl[i].DesCom), 1, 55));
                        Lista.Items.Add(new UI_ComboItem()
                        {
                            ID = i.ToString(),
                            Value = s,
                            Data = i
                        });
                    }

                }

            }

        }

        //True   : Exitoso.
        //False  : Errores.
        public static short CargaPln_CVD(T_MODGRNG MODGRNG, T_MODGCVD MODGCVD, T_MODGPLI1 MODGPLI1, T_MODGUSR MODGUSR, T_Module1 Module1, T_MODGSCE MODGSCE, UI_Mdi_Principal Mdi_Principal, UnitOfWorkCext01 unit)
        {
            short i = 0;
            // UPGRADE_INFO (#0501): The 'n' member isn't used anywhere in current application.
            short n = 0;
            //********************************************************************************
            //   Autor                      : Accenture - Continuidad Comex
            //   Incidente                  : IR46107
            //   Descripcion                : Problema en correlativo de planillas CUI 248
            //   Fecha                      : Mayo de 2012
            //   Identificador de Inicio  : ACC-001-I
            //   Identificador de Termino : ACC-001-F
            //   Codigo Anterior          : ACC-001-ANT
            //   Codigo Nuevo             : ACC-001-N
            //********************************************************************************
            //Redimensiona el arreglo de Planillas Invisibles.
            //ReDim Vplis(0) 'ACC-001-ANT


            VB6Helpers.RedimPreserve(ref MODGPLI1.Vplis, 0, VB6Helpers.UBound(MODGCVD.VgPli));
            for (i = 0; i <= (short)VB6Helpers.UBound(MODGCVD.VgPli); i++)
            {
                if (MODGCVD.VgPli[i].Status != T_MODGCVD.EstadoEli)
                {
                    //------------------------------------------------
                    //n% = UBound(Vplis) + 1 'ACC-001-ANT
                    //ReDim Preserve Vplis(i%) 'ACC-001-ANT
                    //VB6Helpers.RedimPreserve(ref MODGPLI1.Vplis, 0, VB6Helpers.UBound(MODGCVD.VgPli));  //ACC-001-N

                    //------------------------------------------------
                    //Carga los datos de la Planilla Invisible.
                    //------------------------------------------------
                    // Vplis(n%).NumPli = Trim$(Str$(LeeSceRng("PLI"))) 'ACC-001-ANT
                    // If Vplis(n%).NumPli = "" Then Exit Function 'ACC-001-ANT

                    if (MODGCVD.VgPli[i].TipCVD != "TIN")
                    {
                        //ACC-001-N se reemplaza puntero n% por i%
                        if (String.IsNullOrEmpty(MODGPLI1.Vplis[i].NumPli))
                        {
                            //ACC-001-N
                            MODGPLI1.Vplis[i].NumPli = VB6Helpers.Trim(VB6Helpers.Str(BCH.Comex.Core.BL.XCFT.Modulos.MODGRNG.LeeSceRng(MODGRNG, MODGUSR, Mdi_Principal, unit, "PLI")));
                            //si retorna -1 no pudo obtener un numero valido
                            if (string.IsNullOrEmpty(MODGPLI1.Vplis[i].NumPli) || MODGPLI1.Vplis[i].NumPli == "-1")
                            {
                                return 0;
                            }
                        }

                    }
                    else
                    {
                        MODGPLI1.Vplis[i].NumPli = "000000";
                    }

                    MODGPLI1.Vplis[i].FecPli = DateTime.Now.ToString("yyyy-MM-dd");


                    DateTime dt = DateTime.Now;
                    MODGPLI1.Vplis[i].FecPli = String.Format("{0:dd/MM/yyyy}", dt);

                    //------------------------------------------------
                    MODGPLI1.Vplis[i].cencos = MODGUSR.UsrEsp.CentroCosto;
                    MODGPLI1.Vplis[i].codusr = MODGUSR.UsrEsp.Especialista;
                    MODGPLI1.Vplis[i].Fecing = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    MODGPLI1.Vplis[i].FecAct = DateTime.Now.ToString("yyyy-MM-dd");
                    //------------------------------------------------
                    MODGPLI1.Vplis[i].codcct = MODGCVD.VgCvd.codcct;
                    MODGPLI1.Vplis[i].codpro = MODGCVD.VgCvd.codpro;
                    MODGPLI1.Vplis[i].codesp = MODGCVD.VgCvd.codesp;
                    MODGPLI1.Vplis[i].codofi = MODGCVD.VgCvd.codofi;
                    MODGPLI1.Vplis[i].codope = MODGCVD.VgCvd.codope;
                    //------------------------------------------------
                    MODGPLI1.Vplis[i].Estado = 1;
                    MODGPLI1.Vplis[i].CodOper = "";
                    MODGPLI1.Vplis[i].PlzBcc = MODGSCE.VGen.CodPbc;
                    //------------------------------------------------
                    MODGPLI1.Vplis[i].rutcli = Module1.PartysOpe[MODGCVD.VgCvd.IndPrt].rut;
                    MODGPLI1.Vplis[i].PrtCli = Module1.PartysOpe[MODGCVD.VgCvd.IndPrt].LlaveArchivo;
                    MODGPLI1.Vplis[i].IndNom = Module1.PartysOpe[MODGCVD.VgCvd.IndPrt].IndNombre;
                    MODGPLI1.Vplis[i].IndDir = Module1.PartysOpe[MODGCVD.VgCvd.IndPrt].IndDireccion;
                    //------------------------------------------------
                    string _switchVar1 = MODGCVD.VgPli[i].IngEgr;
                    if (_switchVar1 == "I")
                    {
                        MODGPLI1.Vplis[i].TipPln = T_Mdl_Funciones.TPli_Ingreso;
                    }
                    else if (_switchVar1 == "E")
                    {
                        MODGPLI1.Vplis[i].TipPln = T_Mdl_Funciones.TPli_Egreso;
                    }
                    else if (_switchVar1 == "TI")
                    {
                        MODGPLI1.Vplis[i].TipPln = T_Mdl_Funciones.TPli_TranIng;
                    }
                    else if (_switchVar1 == "TE")
                    {
                        MODGPLI1.Vplis[i].TipPln = T_Mdl_Funciones.TPli_TranEg;
                    }
                    else
                    {
                        MODGPLI1.Vplis[i].TipPln = VB6Helpers.CShort(VB6Helpers.Format(VB6Helpers.Left(MODGCVD.VgPli[i].CodTcp, 1)));
                    }

                    MODGPLI1.Vplis[i].CodOci = MODGCVD.VgPli[i].CodOci;
                    MODGPLI1.Vplis[i].codcom = VB6Helpers.Left(MODGCVD.VgPli[i].CodTcp, 6);
                    MODGPLI1.Vplis[i].Concep = VB6Helpers.Right(MODGCVD.VgPli[i].CodTcp, 3);
                    if (MODGCVD.VgPli[i].AnuNum != 0)
                    {
                        //ACC-001-N
                        MODGPLI1.Vplis[i].AnuNum = VB6Helpers.CStr(MODGCVD.VgPli[i].AnuNum);
                    }
                    else
                    {
                        MODGPLI1.Vplis[i].AnuNum = "";
                    }
                    //ACC-001-N
                    MODGPLI1.Vplis[i].AnuFec = MODGCVD.VgPli[i].AnuFec;
                    MODGPLI1.Vplis[i].AnuPbc = MODGCVD.VgPli[i].AnuPbc;
                    MODGPLI1.Vplis[i].codpai = MODGCVD.VgPli[i].codpai;
                    MODGPLI1.Vplis[i].CodMnd = MODGCVD.VgPli[i].CodMnd;
                    MODGPLI1.Vplis[i].CodMndBC = MODGCVD.VgPli[i].MndCBC;
                    MODGPLI1.Vplis[i].MtoOpe = MODGCVD.VgPli[i].MtoCVD;
                    MODGPLI1.Vplis[i].Mtopar = MODGCVD.VgPli[i].Mtopar;
                    if (MODGCVD.VgPli[i].Mtopar == 0)
                    {
                        MODGCVD.VgPli[i].Mtopar = 1;
                    }
                    MODGPLI1.Vplis[i].MtoDol = MODGCVD.VgPli[i].MtoCVD / MODGCVD.VgPli[i].Mtopar;
                    MODGPLI1.Vplis[i].TipCam = MODGCVD.VgPli[i].MtoPes / MODGPLI1.Vplis[i].MtoDol;
                    MODGPLI1.Vplis[i].MtoNac = MODGCVD.VgPli[i].MtoPes;
                    //------------------------------------------------
                    MODGPLI1.Vplis[i].DieNum = VB6Helpers.Trim(MODGCVD.VgPli[i].DieNum);
                    MODGPLI1.Vplis[i].DieFec = VB6Helpers.Trim(MODGCVD.VgPli[i].DieFec);
                    MODGPLI1.Vplis[i].DiePbc = MODGCVD.VgPli[i].DiePbc;
                    //------------------------------------------------
                    MODGPLI1.Vplis[i].numdec = VB6Helpers.Trim(MODGCVD.VgPli[i].numdec);
                    MODGPLI1.Vplis[i].FecDec = VB6Helpers.Trim(MODGCVD.VgPli[i].FecDec);
                    MODGPLI1.Vplis[i].CodAdn = MODGCVD.VgPli[i].CodAdn;
                    MODGPLI1.Vplis[i].CodEOR = MODGCVD.VgPli[i].CodEOR;

                    //------------------------------------------------
                    MODGPLI1.Vplis[i].FecDeb = VB6Helpers.Trim(MODGCVD.VgPli[i].FecDeb);
                    MODGPLI1.Vplis[i].DocNac = VB6Helpers.Trim(MODGCVD.VgPli[i].DocNac);
                    MODGPLI1.Vplis[i].DocExt = VB6Helpers.Trim(MODGCVD.VgPli[i].DocExt);
                    MODGPLI1.Vplis[i].BcoExt = 0;
                    //------------------------------------------------
                    MODGPLI1.Vplis[i].NumCre = MODGCVD.VgPli[i].NumCre;
                    MODGPLI1.Vplis[i].FecCre = MODGCVD.VgPli[i].FecCre;
                    MODGPLI1.Vplis[i].MndCre = MODGCVD.VgPli[i].MndCre;
                    MODGPLI1.Vplis[i].MtoCre = MODGCVD.VgPli[i].MtoCre;
                    MODGPLI1.Vplis[i].DatImp = MODGCVD.VgPli[i].DatImp;
                    MODGPLI1.Vplis[i].fecins = MODGCVD.VgPli[i].fecins;
                    MODGPLI1.Vplis[i].NomFin = MODGCVD.VgPli[i].NomFin;
                    MODGPLI1.Vplis[i].VenOfi = MODGCVD.VgPli[i].VenOfi;
                    //------------------------------------------------
                    MODGPLI1.Vplis[i].ApcNum = MODGCVD.VgPli[i].ApcNum;
                    MODGPLI1.Vplis[i].ApcFec = MODGCVD.VgPli[i].ApcFec;
                    MODGPLI1.Vplis[i].ApcPbc = MODGCVD.VgPli[i].ApcPbc;
                    MODGPLI1.Vplis[i].ApcTip = MODGCVD.VgPli[i].ApcTip;
                    //------------------------------------------------
                    MODGPLI1.Vplis[i].CodAcu = "";
                    MODGPLI1.Vplis[i].RegAcu = "";
                    MODGPLI1.Vplis[i].RutAcu = "";
                    MODGPLI1.Vplis[i].ObsPli = MODGCVD.VgCvd.OpeCon;
                    //------------------------------------------------
                    MODGPLI1.Vplis[i].NumAcu = MODGCVD.VgPli[i].NumAcu;
                    MODGPLI1.Vplis[i].Desacu = MODGCVD.VgPli[i].Desacu;

                    MODGPLI1.Vplis[i].NumCon = MODGCVD.VgPli[i].NumCon;
                    MODGPLI1.Vplis[i].fecsus = MODGCVD.VgPli[i].fecsus;
                    MODGPLI1.Vplis[i].VenOd = MODGCVD.VgPli[i].VenOd;
                    MODGPLI1.Vplis[i].insuti = MODGCVD.VgPli[i].insuti;
                    MODGPLI1.Vplis[i].partip = MODGCVD.VgPli[i].partip;
                    MODGPLI1.Vplis[i].arecon = MODGCVD.VgPli[i].arecon;
                    MODGPLI1.Vplis[i].canacu = MODGCVD.VgPli[i].canacu;
                    MODGPLI1.Vplis[i].afeder = MODGCVD.VgPli[i].afeder;
                    MODGPLI1.Vplis[i].ZonFra = MODGCVD.VgPli[i].ZonFra;
                    MODGPLI1.Vplis[i].SecBen = MODGCVD.VgPli[i].SecBen;
                    MODGPLI1.Vplis[i].SecInv = MODGCVD.VgPli[i].SecInv;
                    MODGPLI1.Vplis[i].PrcPar = MODGCVD.VgPli[i].PrcPar;
                }
                //SE AGREGA EL ELSE PARA MARCAR EL ELEMENTO COMO ELIMINADO PARA QUE NO GENERE UNA IMPRESION CON VALORES POR DEFECTO
                else
                {
                    MODGPLI1.Vplis[i].Status = T_MODGCVD.EstadoEli;
                }

            }

            return (short)(true ? -1 : 0);
        }

        //Carga los valores a justificar en los Orígenes.
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short Llena_VxMtoOri(InitializationObject Modulos, UnitOfWorkCext01 unit)
        {
            T_MODGCVD MODGCVD = Modulos.MODGCVD;
            T_MODPREEM MODPREEM = Modulos.MODPREEM;
            T_MODGARB MODGARB = Modulos.MODGARB;
            T_MODGANU MODGANU = Modulos.MODGANU;
            T_MODXPLN0 MODXPLN0 = Modulos.MODXPLN0;
            T_MODXANU MODXANU = Modulos.MODXANU;
            T_Mdl_Funciones Mdl_Funciones = Modulos.Mdl_Funciones;
            T_MODGFYS MODGFYS = Modulos.MODGFYS;
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = Modulos.Mdl_Funciones_Varias;
            T_MODANUVI MODANUVI = Modulos.MODANUVI;

            short i = 0;
            short x = 0;
            short largo_r = 0;
            short largo_a = 0;
            double ACob = 0;
            double APag = 0;
            short mnd = 0;
            // UPGRADE_INFO (#0501): The 'MT' member isn't used anywhere in current application.
            double MT = 0;

            largo_r = (short)VB6Helpers.UBound(MODPREEM.Vx_PReem);
            largo_a = (short)VB6Helpers.UBound(MODANUVI.V_PlAnu);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            short _switchVar1 = MODGCVD.VgCvd.TipCVD;

            //Compra-Venta.
            if (_switchVar1 == T_MODGCVD.TCvd_CVD)
            {
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGCVD.VgPli); i++)
                {
                    if (MODGCVD.VgPli[i].Status != T_MODGCVD.EstadoEli)
                    {
                        string _switchVar2 = MODGCVD.VgPli[i].TipCVD;
                        if (_switchVar2 == "C")
                        {
                            //Compra => Se recibe M/E.
                            x = VB6Helpers.CShort(MODXORI.Put_xOri(Modulos, unit, MODGCVD.VgPli[i].CodMnd, MODGCVD.VgPli[i].MtoCVD));
                        }
                        else if (_switchVar2 == "V" || _switchVar2 == "W")
                        {
                            //Venta  => Se recibe $.
                            x = VB6Helpers.CShort(MODXORI.Put_xOri(Modulos, unit, MODGCVD.CodMonedaNac, MODGCVD.VgPli[i].MtoPes));
                        }
                        else if (_switchVar2 == "TI" || _switchVar2 == "TE" || _switchVar2 == "TIN")
                        {
                            x = VB6Helpers.CShort(MODXORI.Put_xOri(Modulos, unit, MODGCVD.VgPli[i].CodMnd, MODGCVD.VgPli[i].MtoCVD));
                        }

                        if (~x != 0)
                        {
                            return 0;
                        }
                    }

                }

                //Arbitrajes.
            }
            else if (_switchVar1 == T_MODGCVD.TCvd_Arb)
            {
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGARB.VArb); i++)
                {
                    if (MODGARB.VArb[i].Status != T_MODGCVD.EstadoEli)
                    {
                        x = VB6Helpers.CShort(MODXORI.Put_xOri(Modulos, unit, MODGARB.VArb[i].MndCom, MODGARB.VArb[i].MtoCom));
                        if (~x != 0)
                        {
                            return 0;
                        }
                    }

                }

                //Venta Visible : Liquidar, se entrga $.
            }
            else if (_switchVar1 == T_MODGCVD.TCvd_VisExp)
            {
                if (MODXPLN0.VxDatP.MtoLiq > 0)
                {
                    x = VB6Helpers.CShort(MODXORI.Put_xOri(Modulos, unit, MODXPLN0.VxDatP.CodMnd, MODXPLN0.VxDatP.MtoLiq));
                    if (~x != 0)
                    {
                        return 0;
                    }
                }

                //Reverso.
            }
            else if (_switchVar1 == T_MODGCVD.TCvd_Rev)
            {
                //n% = SyGetOV_Cvd(VgCVD.OpeRel) : Borrar.-
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGANU.VAnuPl); i++)
                {
                    short _switchVar3 = MODGANU.VAnuPl[i].TipDoc;
                    if (_switchVar3 == T_Mdl_Funciones.TPli_Ingreso || _switchVar3 == T_Mdl_Funciones.TPli_AnuEgr)
                    {
                        //Compra + Anul. Egr.-
                        x = VB6Helpers.CShort(MODXORI.Put_xOri(Modulos, unit, MODGCVD.CodMonedaNac, Format.StringToDouble(Format.FormatCurrency(MODGANU.VAnuPl[i].MtoAnu * MODGANU.VAnuPl[i].TipCamo, "0"))));
                        if (~x != 0)
                        {
                            return 0;
                        }
                    }
                    else if (_switchVar3 == T_Mdl_Funciones.TPli_Egreso || _switchVar3 == T_Mdl_Funciones.TPli_AnuIng)
                    {
                        //Venta  + Anul. Ing.-
                        x = VB6Helpers.CShort(MODXORI.Put_xOri(Modulos, unit, MODGANU.VAnuPl[i].CodMnd, MODGANU.VAnuPl[i].MtoAnu));
                        if (~x != 0)
                        {
                            return 0;
                        }
                    }

                }

                //Reverso y Reemplazo.
            }
            else if (_switchVar1 == T_MODGCVD.TCvd_RyR)
            {
                //Reverso.-
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGANU.VAnuPl); i++)
                {
                    if (!string.IsNullOrEmpty(MODGANU.VAnuPl[i].Motivo))
                    {
                        short _switchVar4 = MODGANU.VAnuPl[i].TipDoc;
                        if (_switchVar4 == T_Mdl_Funciones.TPli_Ingreso)
                        {
                            //Compra.-
                            x = VB6Helpers.CShort(MODXORI.Put_xOri(Modulos, unit, MODGCVD.CodMonedaNac, Format.StringToDouble(Format.FormatCurrency(MODGANU.VAnuPl[i].MtoAnu * MODGANU.VAnuPl[i].TipCam, "0"))));
                            if (~x != 0)
                            {
                                return 0;
                            }
                        }
                        else if (_switchVar4 == T_Mdl_Funciones.TPli_Egreso)
                        {
                            //Venta.-
                            x = VB6Helpers.CShort(MODXORI.Put_xOri(Modulos, unit, MODGANU.VAnuPl[i].CodMnd, MODGANU.VAnuPl[i].MtoAnu));
                            if (~x != 0)
                            {
                                return 0;
                            }
                        }

                    }
                    else
                    {
                        if (MODGANU.VAnuPl[i].VisInv == "VIS")
                        {
                            x = VB6Helpers.CShort(MODXORI.Put_xOri(Modulos, unit, MODGCVD.CodMonedaNac, Format.StringToDouble(Format.FormatCurrency(MODGANU.VAnuPl[i].MtoAnu * MODGANU.VAnuPl[i].TipCam, "0"))));
                            if (~x != 0)
                            {
                                return 0;
                            }
                        }

                    }

                }

                //Reemplazo.-
                for (i = 0; i < (short)VB6Helpers.UBound(MODGCVD.VgPli); i++)
                {
                    if (MODGCVD.VgPli[i].Status != T_MODGCVD.EstadoEli)
                    {
                        string _switchVar5 = MODGCVD.VgPli[i].TipCVD;
                        if (_switchVar5 == "C")
                        {
                            //Compra => Se recibe M/E.
                            x = VB6Helpers.CShort(MODXORI.Put_xOri(Modulos, unit, MODGCVD.VgPli[i].CodMnd, MODGCVD.VgPli[i].MtoCVD));
                        }
                        else if (_switchVar5 == "V" || _switchVar5 == "W")
                        {
                            //Venta  => Se recibe $.
                            x = VB6Helpers.CShort(MODXORI.Put_xOri(Modulos, unit, MODGCVD.CodMonedaNac, MODGCVD.VgPli[i].MtoPes));
                        }

                        if (~x != 0)
                        {
                            return 0;
                        }
                    }

                }

                if (MODXPLN0.VxDatP.MtoLiq > 0)
                {
                    x = VB6Helpers.CShort(MODXORI.Put_xOri(Modulos, unit, MODXPLN0.VxDatP.CodMnd, MODXPLN0.VxDatP.MtoLiq));
                    if (~x != 0)
                    {
                        return 0;
                    }
                }

                //Anulación de Planillas s/Operación.-
            }
            else if (_switchVar1 == T_MODGCVD.TCvd_PlnoBco)
            {
                for (i = 0; i <= (short)VB6Helpers.UBound(MODXANU.VxAnus); i++)
                {
                    if (((MODXANU.VxAnus[i].Estado != T_MODGCVD.EstadoEli ? -1 : 0) & VB6Helpers.Instr(T_MODXPLN1.PLNLIQ, VB6Helpers.CStr(MODXANU.VxAnus[i].TipPln))) != 0)
                    {
                        x = VB6Helpers.CShort(MODXORI.Put_xOri(Modulos, unit, T_MODGTAB0.MndNac, Format.StringToDouble(Format.FormatCurrency(MODXANU.VxAnus[i].MtoAnu * MODGCVD.VxPlaAnu.TipCam, "0"))));
                    }

                }

            }
            else if (_switchVar1 == T_MODGCVD.TCvd_VisImp)
            {
                //Ventas Visibles Import
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGFYS.VgFyS); i++)
                {
                    if (~MODGFYS.VgFyS[i].Borrado != 0)
                    {

                        if (MODGFYS.VgFyS[i].monto[1] != 0 || MODGFYS.VgFyS[i].monto[2] != 0)
                        {

                            short _switchVar6 = MODGFYS.CVD.Operacion;
                            if (_switchVar6 == T_MODGFYS.FLT)
                            {
                                ACob = Format.StringToDouble(Format.FormatCurrency(MODGFYS.VgFyS[i].MtoCob * MODGFYS.VgFyS[i].TipCam[1], "0"));
                                APag = MODGFYS.VgFyS[i].monto[1] - MODGFYS.VgFyS[i].MtoCob;
                                mnd = MODGFYS.VgFyS[i].CodMon[1];
                            }
                            else if (_switchVar6 == T_MODGFYS.SEG)
                            {
                                ACob = Format.StringToDouble(Format.FormatCurrency(MODGFYS.VgFyS[i].MtoCob * MODGFYS.VgFyS[i].TipCam[1], "0"));
                                APag = MODGFYS.VgFyS[i].monto[2] - MODGFYS.VgFyS[i].MtoCob;
                                mnd = MODGFYS.VgFyS[i].CodMon[1];
                            }
                            else if (_switchVar6 == T_MODGFYS.FLTSEG)
                            {
                                ACob = Format.StringToDouble(Format.FormatCurrency((MODGFYS.VgFyS[i].MtoCob + MODGFYS.VgFyS[i].MtoCob2) * MODGFYS.VgFyS[i].TipCam[1], "0"));
                                APag = (MODGFYS.VgFyS[i].monto[1] + MODGFYS.VgFyS[i].monto[2]) - (MODGFYS.VgFyS[i].MtoCob + MODGFYS.VgFyS[i].MtoCob2);
                                mnd = MODGFYS.VgFyS[i].CodMon[1];
                            }
                            else if (_switchVar6 == T_MODGFYS.ENDREC)
                            {
                                ACob = Format.StringToDouble(Format.FormatCurrency(MODGFYS.VgFyS[i].monto[1] * MODGFYS.VgFyS[i].TipCam[1], "0"));
                                APag = 0;
                                mnd = MODGFYS.VgFyS[i].CodMon[1];
                            }

                            if (APag > 0)
                            {
                                x = VB6Helpers.CShort(MODXORI.Put_xOri(Modulos, unit, mnd, MODGPYF1.Dec_EnMto(Modulos.MODGTAB0, Modulos.MODGPYF0, unit, APag, mnd)));
                                if (~x != 0)
                                {
                                    return 0;
                                }
                            }

                            if (ACob > 0)
                            {
                                x = VB6Helpers.CShort(MODXORI.Put_xOri(Modulos, unit, MODGCVD.CodMonedaNac, MODGPYF1.Dec_EnMto(Modulos.MODGTAB0, Modulos.MODGPYF0, unit, ACob, 1)));
                                if (~x != 0)
                                {
                                    return 0;
                                }
                            }

                        }

                    }

                }

            }
            else if (_switchVar1 == T_MODGCVD.TCvd_AnuVisI)
            {
                //Anulacion Planillas Vis. Importacion.-

                if (MODANUVI.Vx_AnuReem.ConvAnu - MODANUVI.Vx_AnuReem.ConvRee > 0)
                {
                    x = VB6Helpers.CShort(MODXORI.Put_xOri(Modulos, unit, MODANUVI.Vx_AnuReem.CodMon, MODANUVI.Vx_AnuReem.ConvAnu - MODANUVI.Vx_AnuReem.ConvRee));
                }

                if (MODANUVI.Vx_AnuReem.CambRee - MODANUVI.Vx_AnuReem.CambAnu > 0)
                {
                    x = VB6Helpers.CShort(MODXORI.Put_xOri(Modulos, unit, T_MODGTAB0.MndNac, MODANUVI.Vx_AnuReem.CambRee - MODANUVI.Vx_AnuReem.CambAnu));
                }

            }
            else if (_switchVar1 == T_MODGCVD.TCvd_PlanSO)
            {
                //Planillas Vis. de Importacion Sin Operacion.-
                for (i = 0; i <= (short)largo_r; i++)
                {
                    if (MODPREEM.Vx_PReem[i].Estado != 9 && MODPREEM.Vx_PReem[i].IndAnu != 3)
                    {
                        x = VB6Helpers.CShort(MODXORI.Put_xOri(Modulos, unit, T_MODGTAB0.MndNac, MODGPYF1.Dec_EnMto(Modulos.MODGTAB0, Modulos.MODGPYF0, unit, Math.Round(MODPREEM.Vx_PReem[i].TotOri * MODPREEM.Vx_PReem[i].TipCamo, MidpointRounding.AwayFromZero), T_MODGTAB0.MndNac)));
                    }

                    if (MODPREEM.Vx_PReem[i].Estado != 9 && MODPREEM.Vx_PReem[i].IndAnu == 3)
                    {
                        x = VB6Helpers.CShort(MODXORI.Put_xOri(Modulos, unit, MODPREEM.Vx_PReem[i].CodMon, MODPREEM.Vx_PReem[i].TotOri));
                    }

                }

            }

            if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 0)
            {
                //Ingresa Todas las Comisiones a Cobrar en Moneda Nacional.-
                for (i = 0; i <= (short)VB6Helpers.UBound(Mdl_Funciones_Varias.V_gCom); i++)
                {
                    if (Mdl_Funciones_Varias.V_gCom[i].estado != 3 && Mdl_Funciones_Varias.V_gCom[i].MtoTotp > 0)
                    {
                        x = VB6Helpers.CShort(MODXORI.Put_xOri(Modulos, unit, MODGCVD.CodMonedaNac, Mdl_Funciones_Varias.V_gCom[i].MtoTotp));
                    }

                }

            }

            return (short)(true ? -1 : 0);
        }

        //True   : Exitoso.
        //False  : Errores.
        public static bool CargaPln_Arb(InitializationObject Modulos, UnitOfWorkCext01 unit)
        {
            T_MODGPLI1 MODGPLI1 = Modulos.MODGPLI1;
            T_MODGARB MODGARB = Modulos.MODGARB;
            T_MODGCVD MODGCVD = Modulos.MODGCVD;
            T_MODGUSR MODGUSR = Modulos.MODGUSR;
            T_Module1 Module1 = Modulos.Module1;
            T_MODGTAB0 MODGTAB0 = Modulos.MODGTAB0;
            T_MODGTAB1 MODGTAB1 = Modulos.MODGTAB1;

            short i = 0;
            short n = 0;
            short Elemen = 0;
            string s = "";
            short m = 0;

            VB6Helpers.RedimPreserve(ref MODGPLI1.Vplis, 0, (MODGARB.VArb.Count() * 2) - 1);  //ACC-001-N
            
            for (i = 0; i <= (short)VB6Helpers.UBound(MODGARB.VArb); i++)
            {
                if (MODGARB.VArb[i].Status != T_MODGCVD.EstadoEli)
                {
                    //------------------------------------------------
                    //------------------------------------------------
                    //Carga los datos de la Planilla Invisible.
                    //------------------------------------------------
                    if (string.IsNullOrEmpty(MODGPLI1.Vplis[n].NumPli))
                    {
                        //ACC-001-N
                        MODGPLI1.Vplis[n].NumPli = MODGRNG.LeeSceRng(Modulos.MODGRNG, Modulos.MODGUSR, Modulos.Mdi_Principal, unit, "PLI").ToString();
                        //Si el numero de planilla (NumPli) es igual a -1 ocurrio un error
                        if (string.IsNullOrEmpty(MODGPLI1.Vplis[n].NumPli) || MODGPLI1.Vplis[n].NumPli == "-1")
                        {
                            return false;
                        }
                    }
                    //ACC-001-N
                    MODGPLI1.Vplis[n].FecPli = DateTime.Now.ToString("dd/MM/yyyy");

                    //Para almacenar la planilla relacionada con el arbitraje
                    Elemen = n;
                    //------------------------------------------------
                    MODGPLI1.Vplis[n].cencos = MODGUSR.UsrEsp.CentroCosto;
                    MODGPLI1.Vplis[n].codusr = MODGUSR.UsrEsp.Especialista;
                    MODGPLI1.Vplis[n].Fecing = DateTime.Now.ToString("dd/MM/yyyy");
                    MODGPLI1.Vplis[n].FecAct = DateTime.Now.ToString("dd/MM/yyyy");
                    //------------------------------------------------
                    MODGPLI1.Vplis[n].codcct = MODGCVD.VgCvd.codcct;
                    MODGPLI1.Vplis[n].codpro = MODGCVD.VgCvd.codpro;
                    MODGPLI1.Vplis[n].codesp = MODGCVD.VgCvd.codesp;
                    MODGPLI1.Vplis[n].codofi = MODGCVD.VgCvd.codofi;
                    MODGPLI1.Vplis[n].codope = MODGCVD.VgCvd.codope;
                    //------------------------------------------------
                    MODGPLI1.Vplis[n].Estado = 1;
                    MODGPLI1.Vplis[n].CodOper = "";
                    MODGPLI1.Vplis[n].PlzBcc = 25;
                    //------------------------------------------------
                    MODGPLI1.Vplis[n].rutcli = Module1.PartysOpe[MODGCVD.VgCvd.IndPrt].rut;
                    MODGPLI1.Vplis[n].PrtCli = Module1.PartysOpe[MODGCVD.VgCvd.IndPrt].LlaveArchivo;
                    MODGPLI1.Vplis[n].IndNom = Module1.PartysOpe[MODGCVD.VgCvd.IndPrt].IndNombre;
                    MODGPLI1.Vplis[n].IndDir = Module1.PartysOpe[MODGCVD.VgCvd.IndPrt].IndDireccion;
                    //------------------------------------------------
                    MODGPLI1.Vplis[n].TipPln = T_Mdl_Funciones.TPli_Ingreso;
                    MODGPLI1.Vplis[n].codcom = "100550";
                    MODGPLI1.Vplis[n].Concep = "000";
                    s = MODGPLI1.Vplis[n].codcom + MODGPLI1.Vplis[n].Concep;
                    m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB1.Get_VTcp(Modulos.MODGTAB1, unit, s);
                    MODGPLI1.Vplis[n].CodOci = MODGTAB1.VTcp[m].CodOci;
                    MODGPLI1.Vplis[n].codpai = MODGARB.VArb[i].codpai;
                    MODGPLI1.Vplis[n].CodMnd = MODGARB.VArb[i].MndCom;
                    m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, MODGPLI1.Vplis[n].CodMnd);
                    MODGPLI1.Vplis[n].CodMndBC = MODGTAB0.VMnd[m].Mnd_MndCbc;
                    MODGPLI1.Vplis[n].MtoOpe = MODGARB.VArb[i].MtoCom;
                    MODGPLI1.Vplis[n].Mtopar = MODGARB.VArb[i].PrdCom;
                    MODGPLI1.Vplis[n].MtoDol = MODGARB.VArb[i].DolCom;
                    MODGPLI1.Vplis[n].TipCam = MODGARB.VArb[i].CamCom;
                    MODGPLI1.Vplis[n].MtoNac = MODGARB.VArb[i].MtoPes;
                    MODGPLI1.Vplis[n].ObsPli = "Arbitraje Contado por Caja. Paridad Utilizada. " + MODGARB.VArb[i].NemMndV + " " + Format.FormatCurrency(MODGARB.VArb[i].PrdArb, T_MODGPYF2.Formato_Par) + " por " + MODGARB.VArb[i].NemMndC + "." + " Número de Operación " + MODGCVD.VgCvd.OpeSin;
                    //------------------------------------------------
                    n = (short)(n + 1);  // ACC-001-N
                    //------------------------------------------------
                    //Carga los datos de la Planilla Invisible.
                    //------------------------------------------------
                    MODGPLI1.Vplis[n].CodOci = VB6Helpers.Val(MODGCVD.VgCvd.codope);
                    if (string.IsNullOrEmpty(MODGPLI1.Vplis[n].NumPli))
                    {
                        //ACC-001-N
                        MODGPLI1.Vplis[n].NumPli = MODGRNG.LeeSceRng(Modulos.MODGRNG, Modulos.MODGUSR, Modulos.Mdi_Principal, unit, "PLI").ToString();
                        //Si el numero de planilla (NumPli) es igual a -1 ocurrio un error
                        if (string.IsNullOrEmpty(MODGPLI1.Vplis[n].NumPli) || MODGPLI1.Vplis[n].NumPli == "-1")
                        {
                            return false;
                        }
                    }
                    //ACC-001-N
                    MODGPLI1.Vplis[n].FecPli = DateTime.Now.ToString("dd/MM/yyyy");
                    //------------------------------------------------
                    MODGPLI1.Vplis[n].cencos = MODGUSR.UsrEsp.CentroCosto;
                    MODGPLI1.Vplis[n].codusr = MODGUSR.UsrEsp.Especialista;
                    MODGPLI1.Vplis[n].Fecing = DateTime.Now.ToString("dd/MM/yyyy");
                    MODGPLI1.Vplis[n].FecAct = DateTime.Now.ToString("dd/MM/yyyy");
                    //------------------------------------------------
                    MODGPLI1.Vplis[n].codcct = MODGCVD.VgCvd.codcct;
                    MODGPLI1.Vplis[n].codpro = MODGCVD.VgCvd.codpro;
                    MODGPLI1.Vplis[n].codesp = MODGCVD.VgCvd.codesp;
                    MODGPLI1.Vplis[n].codofi = MODGCVD.VgCvd.codofi;
                    MODGPLI1.Vplis[n].codope = MODGCVD.VgCvd.codope;
                    //------------------------------------------------
                    MODGPLI1.Vplis[n].Estado = 1;
                    MODGPLI1.Vplis[n].CodOper = "";
                    MODGPLI1.Vplis[n].PlzBcc = 25;
                    //------------------------------------------------
                    MODGPLI1.Vplis[n].rutcli = Module1.PartysOpe[MODGCVD.VgCvd.IndPrt].rut;
                    MODGPLI1.Vplis[n].PrtCli = Module1.PartysOpe[MODGCVD.VgCvd.IndPrt].LlaveArchivo;
                    MODGPLI1.Vplis[n].IndNom = Module1.PartysOpe[MODGCVD.VgCvd.IndPrt].IndNombre;
                    MODGPLI1.Vplis[n].IndDir = Module1.PartysOpe[MODGCVD.VgCvd.IndPrt].IndDireccion;
                    //------------------------------------------------
                    MODGPLI1.Vplis[n].TipPln = T_Mdl_Funciones.TPli_Egreso;
                    MODGPLI1.Vplis[n].codcom = "200550";
                    MODGPLI1.Vplis[n].Concep = "000";

                    s = MODGPLI1.Vplis[n].codcom + MODGPLI1.Vplis[n].Concep;
                    m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB1.Get_VTcp(Modulos.MODGTAB1, unit, s);
                    MODGPLI1.Vplis[n].CodOci = MODGTAB1.VTcp[m].CodOci;
                    MODGPLI1.Vplis[n].codpai = MODGARB.VArb[i].codpai;
                    MODGPLI1.Vplis[n].CodMnd = MODGARB.VArb[i].MndVta;
                    m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, MODGPLI1.Vplis[n].CodMnd);
                    MODGPLI1.Vplis[n].CodMndBC = MODGTAB0.VMnd[m].Mnd_MndCbc;
                    MODGPLI1.Vplis[n].MtoOpe = MODGARB.VArb[i].MtoVta;
                    MODGPLI1.Vplis[n].Mtopar = MODGARB.VArb[i].PrdVta;
                    MODGPLI1.Vplis[n].MtoDol = MODGARB.VArb[i].DolVta;
                    MODGPLI1.Vplis[n].TipCam = MODGARB.VArb[i].CamVta;
                    MODGPLI1.Vplis[n].MtoNac = MODGARB.VArb[i].MtoPes;
                    MODGPLI1.Vplis[n].ObsPli = "Arbitraje Contado por Caja. Paridad Utilizada. " + MODGARB.VArb[i].NemMndV + " " + Format.FormatCurrency(MODGARB.VArb[i].PrdArb, T_MODGPYF2.Formato_Par) + " por " + MODGARB.VArb[i].NemMndC + "." + " Número de Operación " + MODGCVD.VgCvd.OpeSin;

                    //Almacenamos la planilla relacionada para el elemento actual.-
                    //y el anterior.-
                    //---------------------------------------
                    MODGPLI1.Vplis[n].AnuNum = MODGPLI1.Vplis[Elemen].NumPli;
                    MODGPLI1.Vplis[n].AnuFec = MODGPLI1.Vplis[Elemen].FecPli;
                    MODGPLI1.Vplis[Elemen].AnuNum = MODGPLI1.Vplis[n].NumPli;
                    MODGPLI1.Vplis[Elemen].AnuFec = MODGPLI1.Vplis[n].FecPli;
                    //ACC-001-F -----------------------------------------------------------------------------
                    n++;
                }
            }
            return true;
        }

        public static short SyPut_CVDAtom(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            using (var trace = new Tracer("SyPut_CVDAtom"))
            {
                decimal _retValue = 0;
                List<string> parameters = new List<string>();
                try
                {
                    //-------------------------------------------
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCVD.VgCvd.codcct));
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCVD.VgCvd.codpro));
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCVD.VgCvd.codesp));
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCVD.VgCvd.codofi));
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCVD.VgCvd.codope));
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGUSR.UsrEsp.CentroCosto));
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGUSR.UsrEsp.Especialista));
                    parameters.Add(DateTime.Now.ToString("yyyy-MM-dd"));
                    parameters.Add(DateTime.Now.ToString("yyyy-MM-dd"));
                    parameters.Add(MODGSYB.dbnumesy(initObj.MODGCVD.VgCvd.TipCVD));
                    parameters.Add(MODGSYB.dbnumesy(initObj.MODGCVD.VgCvd.estado));
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCVD.VgCvd.operel));
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCVD.VgCvd.PrtCli));//102425~~~~~~
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCVD.VgCvd.rutcli));
                    parameters.Add(MODGSYB.dbnumesy(initObj.MODGCVD.VgCvd.IndNomC));
                    parameters.Add(MODGSYB.dbnumesy(initObj.MODGCVD.VgCvd.IndDirC));//0
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCVD.VgCvd.IndPopC));//vacio
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCVD.VgCvd.PrtOtr));//vacio
                    parameters.Add(MODGSYB.dbnumesy(initObj.MODGCVD.VgCvd.IndNomO));//0
                    parameters.Add(MODGSYB.dbnumesy(initObj.MODGCVD.VgCvd.IndDirO));//vacio
                    parameters.Add(MODGSYB.dbcharSy(initObj.MODGCVD.VgCvd.IndPopO));//vacio

                    //-------------------------------------------
                    //Se ejecuta el Procedimiento Almacenado.
                    //-------------------------------------------
                    Mdl_Funciones_Varias.Cmd_Put_New(initObj.Mdl_Funciones_Varias, () =>
                    {
                        return unit.SceRepository.sce_cvd_w01_MS(parameters.ToArray());
                    });
                    _retValue = -1;
                }
                catch (Exception _ex)
                {
                    trace.TraceException("Alerta", _ex);
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                        Title = T_MODGCVD.MsgCVD
                    });
                    _retValue = 0;
                }
                return (short)_retValue;
            }
        }

        //Valida que la Contabilidad esté OK con el Producto.-
        public static short ValidaVigente(InitializationObject initObj, UnitOfWorkCext01 unit, string OpeSin, short tipope)
        {
            using (var trace = new Tracer("ValidaVigente: Valida que la Contabilidad esté OK con el Producto"))
            {
                short _retValue = 0;
                List<string> parameters = new List<string>();
                try
                {
                    trace.TraceInformation("Datos para sce_mcd_s14_MS: NroOP: {0}, tipope: {1}", OpeSin, tipope);
                    //Genera Comando.-                
                    _retValue = Mdl_Funciones_Varias.Cmd_Put_New(initObj.Mdl_Funciones_Varias, () =>
                    {                        
                        return (short)unit.SceRepository.sce_mcd_s14_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 1, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 4, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 6, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 8, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 11, 5)), tipope);
                    });
                }
                catch (Exception _ex)
                {
                    trace.TraceException("Alerta", _ex);
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                        Title = "Validación Vigente-Contabilidad"
                    });
                }
                return _retValue;
            }
        }

        //Valida que la Conversión y el Producto estén OK.-
        public static short ConvPlnOK(InitializationObject initObject, UnitOfWorkCext01 unit, dynamic OpeSin, string CodAnu)
        {
            using (var trace = new Tracer("ConvPlnOK: Valida que la Conversión y el Producto estén OK"))
            {
                short _retValue;
                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;
                try
                {

                    _retValue = Mdl_Funciones_Varias.Cmd_Put_New(initObject.Mdl_Funciones_Varias, () =>
                    {
                        return (short)unit.SceRepository.sce_mcd_s15_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 1, 3)),
                                                                    MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 4, 2)),
                                                                    MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 6, 2)),
                                                                    MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 8, 3)),
                                                                    MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 11, 5)),
                                                                    MODGSYB.dbcharSy(CodAnu),
                                                                    (initObject.MODGCON0.VMch.NroRpt),
                                                                    MODGSYB.dbdatesy(initObject.MODGCON0.VMch.fecmov));
                    });
                    return _retValue;
                }
                catch (Exception _ex)
                {
                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                        Title = "Validación Conversión-Planilla"
                    });
                    trace.TraceException("Alerta", _ex);
                    throw;
                }
            }
        }

        //****************************************************************************
        //   1.  Graba una Compra o una Venta en la tabla Sce_Cov.
        //   2.  Retorno    <> 0 : Grabación Exitosa.
        //                  =  0 : Error o Grabación no Exitosa.
        //****************************************************************************
        public static short SyPutn_Cov(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;

            short _retValue = 0;
            short n = 0;
            short i = 0;
            short HayError = 0;

            n = (short)VB6Helpers.UBound(MODGCVD.VgPli);

            HayError = (short)(false ? -1 : 0);
            //Recorre estructura de Compra - Venta.
            for (i = 0; i <= (short)n; i++)
            {
                if (MODGCVD.VgPli[i].Status != T_MODGCVD.EstadoEli)
                {
                    if (MODGCVD.VgPli[i].TipCVD == "W")
                    {
                        MODGCVD.VgPli[i].TipCVD = "V";
                    }

                    //-------------------------------------------
                    List<string> parameters = new List<string>();
                    parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codcct));
                    parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codpro));
                    parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codesp));
                    parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codofi));
                    parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codope));
                    //-------------------------------------------
                    parameters.Add(MODGCVD.VgPli[i].numcor.ToString());
                    parameters.Add(T_MODGCON0.ECC_ANU.ToString());
                    parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgPli[i].TipCVD));
                    parameters.Add(MODGCVD.VgPli[i].codpai.ToString());
                    //-------------------------------------------
                    parameters.Add(MODGCVD.VgPli[i].CodMnd.ToString());
                    parameters.Add(MODGSYB.dbmontoSy(MODGCVD.VgPli[i].MtoCVD));
                    parameters.Add(MODGSYB.dbTCamSy(MODGCVD.VgPli[i].TipCam));
                    parameters.Add(MODGSYB.dbmontoSy(MODGCVD.VgPli[i].MtoPes));
                    parameters.Add(MODGSYB.dbPardSy(MODGCVD.VgPli[i].Mtopar));
                    //-------------------------------------------
                    parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgPli[i].CodTcp));
                    parameters.Add(MODGCVD.VgPli[i].CodOci.ToString());
                    parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgPli[i].IngEgr));
                    parameters.Add(MODGSYB.dblogisy(MODGCVD.VgPli[i].Conven));
                    //-------------------------------------------
                    parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgPli[i].numdec));
                    parameters.Add(MODGSYB.dbdatesy(MODGCVD.VgPli[i].FecDec));
                    parameters.Add(MODGCVD.VgPli[i].CodAdn.ToString());
                    parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgPli[i].DieNum));
                    parameters.Add(MODGSYB.dbdatesy(MODGCVD.VgPli[i].DieFec));
                    parameters.Add(MODGCVD.VgPli[i].DiePbc.ToString());
                    //-------------------------------------------
                    parameters.Add(MODGSYB.dblogisy(MODGCVD.VgPli[i].IndDec));
                    parameters.Add(MODGSYB.dbdatesy(MODGCVD.VgPli[i].FecDeb));
                    parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgPli[i].DocNac));
                    parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgPli[i].DocExt));

                    //-------------------------------------------
                    //Se ejecuta el Procedimiento Almacenado.
                    //-------------------------------------------
                    decimal res = unit.SceRepository.EjecutarSP<int>("sce_cov_i01_MS", parameters.ToArray()).First();
                    if (res == 9)
                    {
                        HayError = -1;
                    }
                }

            }

            if (HayError == -1)
            {
                Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Se ha producido un error al tratar de grabar una Operación de Compra Venta (Sce_Cov). Reporte este problema."
                });
                _retValue = (short)(false ? -1 : 0);
            }

            _retValue = (short)(true ? -1 : 0);
            return _retValue;
        }

        //Graba un reg. de Venta Exportaciones-
        //Retorno    = True  : Grabación Exitosa.-
        //           = False : Error o Grabación no Exitosa.-
        public static short SyPut_Vex(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_MODXPLN0 MODXPLN0 = initObject.MODXPLN0;
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;

            short _retValue = 0;
            try
            {
                _retValue = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Cmd_Put_New(Mdl_Funciones_Varias, () =>
                {
                    return (short)unit.SceRepository.sce_vex_i01_MS(MODGSYB.dbcharSy(MODGCVD.VgCvd.codcct),
                       MODGSYB.dbcharSy(MODGCVD.VgCvd.codpro),
                       MODGSYB.dbcharSy(MODGCVD.VgCvd.codesp),
                       MODGSYB.dbcharSy(MODGCVD.VgCvd.codofi),
                       MODGSYB.dbcharSy(MODGCVD.VgCvd.codope),
                       MODGSYB.dbnumesy(T_MODGCON0.ECC_ING),
                       MODGSYB.dbnumesy(MODXPLN0.VxDatP.CodMnd),
                       MODGSYB.dbTCamSy(MODXPLN0.VxDatP.TipCam),
                       MODGSYB.dbmontoSy(MODXPLN0.VxDatP.MtoLiq),
                       MODGSYB.dbmontoSy(MODXPLN0.VxDatP.MtoInf),
                       MODGSYB.dbmontoSy(MODXPLN0.VxDatP.mtotran));
                });

                //-------------------------------------------
                //Se ejecuta el Procedimiento Almacenado.
                //-------------------------------------------

                if (~_retValue != 0)
                {
                    return _retValue;
                }
                _retValue = (short)(true ? -1 : 0);
            }
            catch (Exception _ex)
            {
                _retValue = 0;
            }
            return _retValue;
        }

        //Graba los Participantes Opcionales de CVD
        //Retorno    = True  : Grabación Exitosa.-
        //           = False : Error o Grabación no Exitosa.-
        public static short SyPutn_Prt(InitializationObject initObject, UnitOfWorkCext01 unit, string NumOpe)
        {
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;
            T_Module1 Module1 = initObject.Module1;
            T_MODGCVD MODGCVD = initObject.MODGCVD;

            short _retValue = 0;
            short X1 = 0;
            short i = 0;
            short X2 = 0;

            X1 = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Cmd_Put_New(Mdl_Funciones_Varias, () =>
            {
                return (short)unit.SceRepository.sce_xprt_d01_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)),
                    MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)),
                    MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)),
                    MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)),
                    MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)));
            });

            //Hace un Put en Sce_xPrt.-
            X2 = (short)(true ? -1 : 0);
            for (i = 0; i <= (short)VB6Helpers.UBound(Module1.PartysOpe); i++)
            {
                if (VB6Helpers.Instr(T_MODGCVD.PrtOpc, VB6Helpers.Format(VB6Helpers.CStr(i), "00")) != 0 && !String.IsNullOrEmpty(Module1.PartysOpe[i].LlaveArchivo))
                {
                    int indice = i;
                    var ret = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Cmd_Put_New(Mdl_Funciones_Varias, () =>
                    {
                        return (short)unit.SceRepository.sce_xprt_i01_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)),
                           MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)),
                           MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)),
                           MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)),
                           MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)),
                           MODGSYB.dbnumesy(i),
                           Module1.PartysOpe[indice].LlaveArchivo,
                           MODGSYB.dbnumesy(Module1.PartysOpe[indice].IndNombre),
                           MODGSYB.dbnumesy(Module1.PartysOpe[indice].IndDireccion),
                           MODGSYB.dblogisy(Module1.PartysOpe[indice].Ubicacion));
                    });

                    if (~ret != 0)
                    {
                        X2 = (short)(false ? -1 : 0);
                    }
                }
            }

            if ((X1 & X2) != 0)
            {
                _retValue = (short)(true ? -1 : 0);
            }
            return _retValue;
        }

        public static void ImprimeCartas(InitializationObject initObject)
        {
            short n = 0;
            string Copias = "";
            short i = 0;
            string s = "";
            string Correlativo = "";

            //cartas
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_Mdl_Funciones Mdl_Funciones = initObject.Mdl_Funciones;
            T_MODXVIA MODXVIA = initObject.MODXVIA;

            //Carta de Avisio Débito/Crédito.-
            if (!string.IsNullOrWhiteSpace(MODGCVD.VgCvd.AvisoDC))
            {
                n = MODGPYF0.cuentadestring(MODGCVD.VgCvd.AvisoDC, ";");
                for (i = 1; i <= (short)n; i++)
                {
                    s = VB6Helpers.Trim(MODGPYF0.copiardestring(MODGCVD.VgCvd.AvisoDC, ";", i));
                    Correlativo = MODGPYF0.copiardestring(s, " ", 1);
                    if (MODGPYF0.copiardestring(s, " ", 2) == "N")
                    {
                        Copias = "1";
                    }
                    else
                    {
                        Copias = "1";//Antes se imprimian 2 copias, pero no debería ser necesario, por lo cual se cambia
                    }
                    var esDeb = MODGPYF0.copiardestring(s, " ", 3) == "D";

                    if (i == 1 && Correlativo != "0")
                    {
                        break;
                    }
                    if (i != 1)
                    {
                        for (var aux = 0; aux < int.Parse(Copias); aux++)
                        {
                            initObject.DocumentosAImprimir.Add(new DataImpresion()
                            {
                                URL = "Impresion/Imprimir?numeroOperacion=" + MODGCVD.VgCvd.OpeSin + "&codDocumento=" + (esDeb ? T_MODXVIA.DocGAdeb : T_MODXVIA.DocGAcre) + "&nroCorrelativo=" + (Correlativo)
                                ,
                                NumeroOperacion = MODGCVD.VgCvd.OpeSin
                                ,
                                CodigoDocumento = (esDeb ? T_MODXVIA.DocGAdeb : T_MODXVIA.DocGAcre)
                                ,
                                NumeroCorrelativo = decimal.Parse(Correlativo)
                                ,
                                tipoDoc = 1 //carta
                                ,
                                fileName = MODGCVD.VgCvd.OpeSin //fileName = MODGCVD.VgCvd.OpeCon
                            });
                        }
                    }
                }
            }

            //Compra - Venta.
            if (MODGCVD.VgCvd.DocCVD != 0)
            {
                initObject.DocumentosAImprimir.Add(new DataImpresion()
                {
                    URL = "Impresion/Imprimir?numeroOperacion=" + MODGCVD.VgCvd.OpeSin + "&codDocumento=" + T_Mdl_Funciones_Varias.DocCVD + "&nroCorrelativo=" + MODGCVD.VgCvd.DocCVD,
                    NumeroOperacion = MODGCVD.VgCvd.OpeSin,
                    CodigoDocumento = T_Mdl_Funciones_Varias.DocCVD,
                    NumeroCorrelativo = (decimal)MODGCVD.VgCvd.DocCVD,
                    tipoDoc = 1, //carta
                    fileName = MODGCVD.VgCvd.OpeSin
                });
            }

            //Planillas.
            if (MODGCVD.VgCvd.DocPln != 0)
            {
                initObject.DocumentosAImprimir.Add(new DataImpresion()
                {
                    URL = "Impresion/Imprimir?numeroOperacion=" + MODGCVD.VgCvd.OpeSin + "&codDocumento=" + T_Mdl_Funciones_Varias.DocxRegPln + "&nroCorrelativo=" + MODGCVD.VgCvd.DocPln,
                    NumeroOperacion = MODGCVD.VgCvd.OpeSin,
                    CodigoDocumento = T_Mdl_Funciones_Varias.DocxRegPln,
                    NumeroCorrelativo = MODGCVD.VgCvd.DocPln,
                    tipoDoc = 1, //carta
                    fileName = MODGCVD.VgCvd.OpeSin
                });
            }

            //Arbitraje.
            if (MODGCVD.VgCvd.DocArb != 0)
            {
                initObject.DocumentosAImprimir.Add(new DataImpresion()
                {
                    URL = "Impresion/Imprimir?numeroOperacion=" + MODGCVD.VgCvd.OpeSin + "&codDocumento=" + T_Mdl_Funciones_Varias.DocArb + "&nroCorrelativo=" + MODGCVD.VgCvd.DocArb,
                    NumeroOperacion = MODGCVD.VgCvd.OpeSin,
                    CodigoDocumento = T_Mdl_Funciones_Varias.DocArb,
                    NumeroCorrelativo = MODGCVD.VgCvd.DocArb,
                    tipoDoc = 1,
                    fileName = MODGCVD.VgCvd.OpeSin
                });
            }

            //Ventas Visibles Import.
            if (MODGCVD.VgCvd.DocCvdI != 0)
            {
                initObject.DocumentosAImprimir.Add(new DataImpresion()
                {
                    URL = "Impresion/Imprimir?numeroOperacion=" + MODGCVD.VgCvd.OpeSin + "&codDocumento=" + T_Mdl_Funciones_Varias.DocCvdI + "&nroCorrelativo=" + MODGCVD.VgCvd.DocCvdI,
                    NumeroOperacion = MODGCVD.VgCvd.OpeSin,
                    CodigoDocumento = T_Mdl_Funciones_Varias.DocCvdI,
                    NumeroCorrelativo = MODGCVD.VgCvd.DocCvdI,
                    tipoDoc = 1, //carta
                    fileName = MODGCVD.VgCvd.OpeSin
                });
                //Se imprimian 2 copias, pero no debería ser necesario, por lo cual se elimina
                //initObject.DocumentosAImprimir.Add(new DataImpresion()
                //{
                //    URL = "Impresion/Imprimir?numeroOperacion=" + MODGCVD.VgCvd.OpeSin + "&codDocumento=" + T_Mdl_Funciones_Varias.DocCvdI + "&nroCorrelativo=" + MODGCVD.VgCvd.DocCvdI
                //});
            }
        }

        //Habilita o Deshabilita los botones y las Opciones.
        public static void Pr_HabilitaBotonMenu(short Boton, short Valor, InitializationObject initObj)
        {
            switch (Boton)
            {
                case 1:  //"tbr_nuevo".
                    initObj.Mdi_Principal.BUTTONS.ToList()[Boton].Value.Enabled = Valor != 0;
                    break;
                case 3:  //"tbr_nuevo".
                    initObj.Mdi_Principal.BUTTONS.ToList()[Boton].Value.Enabled = Valor != 0;
                    break;
                case 5:  //--Comercio Invisible
                    initObj.Mdi_Principal.BUTTONS.ToList()[Boton].Value.Enabled = Valor != 0;
                    break;
                case 7:  //--Arbitrajes
                    initObj.Mdi_Principal.BUTTONS.ToList()[Boton].Value.Enabled = Valor != 0;
                    break;
                case 9:  //--Ventas Vis. Export
                    initObj.Mdi_Principal.BUTTONS.ToList()[Boton].Value.Enabled = Valor != 0;
                    break;
                case 11:  //--Participantes
                    initObj.Mdi_Principal.BUTTONS.ToList()[Boton].Value.Enabled = Valor != 0;
                    break;
                case 13:  //--Ventas Vis. Import
                    initObj.Mdi_Principal.BUTTONS.ToList()[Boton].Value.Enabled = Valor != 0;
                    break;
                case 15:  //--Graba Operaciones
                    initObj.Mdi_Principal.BUTTONS.ToList()[Boton].Value.Enabled = Valor != 0;
                    break;
                case 17:  //tbr_impresion
                    initObj.Mdi_Principal.BUTTONS.ToList()[Boton].Value.Enabled = Valor != 0;
                    break;
                case 19:  //'--Comisiones
                    initObj.Mdi_Principal.BUTTONS.ToList()[Boton].Value.Enabled = Valor != 0;
                    break;
                case 21:  //--Destino de Fondos
                    initObj.Mdi_Principal.BUTTONS.ToList()[Boton].Value.Enabled = Valor != 0;
                    break;
                case 23:  //--Origenes de Fondos
                    //initObj.Mdi_Principal.BUTTONS.ToList()[Boton].Value.Enabled = Valor != 0;
                    break;
                case 25:  //--Swift
                    initObj.Mdi_Principal.BUTTONS.ToList()[Boton].Value.Enabled = Valor != 0;
                    break;
                case 27:  //--Generación de Cheques
                    initObj.Mdi_Principal.BUTTONS.ToList()[Boton].Value.Enabled = Valor != 0;
                    break;
                case 29:  //--Carga Automatica de Operaciones
                    initObj.Mdi_Principal.BUTTONS.ToList()[Boton].Value.Enabled = Valor != 0;
                    break;
                case 31:  //--Muestra la Planilla Invisible.
                    initObj.Mdi_Principal.BUTTONS.ToList()[Boton].Value.Enabled = Valor != 0;
                    break;
                case 33:  //--Muestra la Planilla Visible.
                    initObj.Mdi_Principal.BUTTONS.ToList()[Boton].Value.Enabled = Valor != 0;
                    break;
                case 35:  //--Muestra la Planilla Visible Anulada.
                    initObj.Mdi_Principal.BUTTONS.ToList()[Boton].Value.Enabled = Valor != 0;
                    break;
                case 37:  //--Muestra la Planilla Cobertura Visible de Importacion.
                    initObj.Mdi_Principal.BUTTONS.ToList()[Boton].Value.Enabled = Valor != 0;
                    break;
                case 39:  //--Muestra la Planilla para realizar el cargo o abono a los servicios
                    initObj.Mdi_Principal.BUTTONS.ToList()[Boton].Value.Enabled = Valor != 0;
                    break;
                case 41:  //--Salida de la Aplicacion
                    initObj.Mdi_Principal.BUTTONS.ToList()[Boton].Value.Enabled = Valor != 0;

                    break;
            }
        }

        public static short HayOri(InitializationObject initObject)
        {
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_MODGARB MODGARB = initObject.MODGARB;
            T_MODXPLN0 MODXPLN0 = initObject.MODXPLN0;
            T_MODGANU MODGANU = initObject.MODGANU;
            T_MODANUVI MODANUVI = initObject.MODANUVI;
            T_MODPREEM MODPREEM = initObject.MODPREEM;
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;

            short _retValue = 0;
            short i = 0;
            short x = 0;
            _retValue = (short)(true ? -1 : 0);
            short _switchVar1 = MODGCVD.VgCvd.TipCVD;

            //Compra-Venta.
            if (_switchVar1 == T_MODGCVD.TCvd_CVD)
            {
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGCVD.VgPli); i++)
                {
                    if (MODGCVD.VgPli[i].Status != T_MODGCVD.EstadoEli)
                    {
                        string _switchVar2 = MODGCVD.VgPli[i].TipCVD;
                        if (_switchVar2 == "C" || _switchVar2 == "V" || _switchVar2 == "W")
                        {
                            //Venta  => Se recibe $.
                            return _retValue;
                        }

                    }

                }

                //Arbitrajes.
            }
            else if (_switchVar1 == T_MODGCVD.TCvd_Arb)
            {
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGARB.VArb); i++)
                {
                    if (MODGARB.VArb[i].Status != T_MODGCVD.EstadoEli)
                    {
                        return _retValue;
                    }

                }

                //Venta Visible : Liquidar, se entrga $.
            }
            else if (_switchVar1 == T_MODGCVD.TCvd_VisExp)
            {
                if (MODXPLN0.VxDatP.MtoLiq > 0)
                {
                    return _retValue;
                }

                //Reverso.
            }
            else if (_switchVar1 == T_MODGCVD.TCvd_Rev)
            {

                for (i = 0; i <= (short)VB6Helpers.UBound(MODGANU.VAnuPl); i++)
                {
                    if (MODGANU.VAnuPl[i].Motivo != "")
                    {
                        short _switchVar3 = MODGANU.VAnuPl[i].TipPln;
                        if (_switchVar3 == T_Mdl_Funciones.TPli_Ingreso)
                        {
                            //Compra.-
                            return _retValue;
                        }
                        else if (_switchVar3 == T_Mdl_Funciones.TPli_Egreso)
                        {
                            //Venta.-
                            return _retValue;
                        }

                    }
                    else
                    {
                        if (MODGANU.VAnuPl[i].VisInv == "VIS" && MODGANU.VAnuPl[i].MtoAnu > 0 && MODGANU.VAnuPl[i].estado != T_MODGCVD.EstadoEli && VB6Helpers.Instr(T_MODXPLN1.PLNLIQ, VB6Helpers.CStr(MODGANU.VAnuPl[i].TipPln)) != 0)
                        {
                            return _retValue;
                        }

                    }

                }

                //Reverso y Reemplazo.
            }
            else if (_switchVar1 == T_MODGCVD.TCvd_RyR)
            {
                //Reverso.-
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGANU.VAnuPl); i++)
                {
                    if (MODGANU.VAnuPl[i].Motivo != "")
                    {
                        short _switchVar4 = MODGANU.VAnuPl[i].TipPln;
                        if (_switchVar4 == T_Mdl_Funciones.TPli_Ingreso)
                        {
                            //Compra.-
                            return _retValue;
                        }
                        else if (_switchVar4 == T_Mdl_Funciones.TPli_Egreso)
                        {
                            //Venta.-
                            return _retValue;
                        }

                    }
                    else
                    {
                        if (MODGANU.VAnuPl[i].VisInv == "VIS")
                        {
                            return _retValue;
                        }

                    }

                }

                //Reemplazo.-
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGCVD.VgPli); i++)
                {
                    if (MODGCVD.VgPli[i].Status != T_MODGCVD.EstadoEli)
                    {
                        string _switchVar5 = MODGCVD.VgPli[i].TipCVD;
                        if (_switchVar5 == "C" || _switchVar5 == "V" || _switchVar5 == "W")
                        {
                            //Venta  => Se recibe $.
                            return _retValue;
                        }

                    }

                }

                if (MODXPLN0.VxDatP.MtoLiq > 0)
                {
                    return _retValue;
                }

                //Anulacion - Reemplazo Planillas Invisibles.-
            }
            else if (_switchVar1 == T_MODGCVD.TCvd_AnuVisI)
            {
                if (MODANUVI.Vx_AnuReem.ConvAnu - MODANUVI.Vx_AnuReem.ConvRee > 0)
                {
                    return _retValue;
                }

                if (MODANUVI.Vx_AnuReem.CambRee - MODANUVI.Vx_AnuReem.CambAnu > 0)
                {
                    return _retValue;
                }

            }
            else if (_switchVar1 == T_MODGCVD.TCvd_PlanSO)
            {
                VB6Helpers.ClearError();
                x = (short)VB6Helpers.UBound(MODPREEM.Vx_PReem);
                // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
                // IGNORED: On Error GoTo 0

                //Si hay planillas transferencia no se contabiliza.-
                for (i = 0; i <= (short)x; i++)
                {
                    if (MODPREEM.Vx_PReem[i].Estado != 9)
                    {
                        return _retValue;
                    }

                }

            }

            //Ingresa Todas las Comisiones a Cobrar en Moneda Nacional.-
            for (i = 0; i <= (short)VB6Helpers.UBound(Mdl_Funciones_Varias.V_gCom); i++)
            {
                if (Mdl_Funciones_Varias.V_gCom[i].estado != 3 && Mdl_Funciones_Varias.V_gCom[i].MtoTotp > 0)
                {
                    return _retValue;
                }

            }

            return (short)(false ? -1 : 0);
        }
    }
}
