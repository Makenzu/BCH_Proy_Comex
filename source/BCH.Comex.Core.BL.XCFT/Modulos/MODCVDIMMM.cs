using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODCVDIMMM
    {
        public static T_MODCVDIMMM GetMODCVDIMMM()
        {
            return new T_MODCVDIMMM();
        }

        public static string GetCtaCbe(T_MODCVDIMMM MODCVDIMMM, T_MODGTAB0 MODGTAB0, T_MODGSCE MODGSCE, UI_Mdi_Principal Mdi_Principal, short TipCom)
        {
            string _retValue = "";
            switch (TipCom)
            {
                case T_MODGMTA.EsCom:  //Comisión
                    _retValue = Get_Com(MODCVDIMMM, Mdi_Principal);
                    MODCVDIMMM.VComI.TipCmC = MODGTAB0.VVmd.VmdObs;
                    break;
                case T_MODGMTA.EsGas:  //Gasto
                    _retValue = Get_Gas(MODCVDIMMM, Mdi_Principal);
                    MODCVDIMMM.VComI.TipCnp = "Comisión de Gastos.";
                    MODCVDIMMM.VComI.TipCmC = MODGTAB0.VVmd.VmdObs;
                    break;
                case T_MODGMTA.EsIva:  //Iva
                    _retValue = Get_Imp(MODCVDIMMM, Mdi_Principal, "IVA");
                    MODCVDIMMM.VComI.TipCnp = "Impuesto IVA " + VB6Helpers.Trim(VB6Helpers.Format(VB6Helpers.CStr(MODGSCE.VGen.MtoIva))) + "%.";
                    MODCVDIMMM.VComI.TipCmC = MODGTAB0.VVmd.VmdObs;
                    break;
                case T_MODGMTA.EsSch:  //Impto. sobre cheque
                    _retValue = Get_Imp(MODCVDIMMM, Mdi_Principal, "SCH");
                    MODCVDIMMM.VComI.TipCnp = "Impuesto Fijo sobre Cheques.";
                    MODCVDIMMM.VComI.TipCmC = MODGTAB0.VVmd.VmdObs;
                    break;
                case T_MODGMTA.EsVdi:  //Impto. Venta Divisa
                    _retValue = Get_Imp(MODCVDIMMM, Mdi_Principal, "VDI");
                    MODCVDIMMM.VComI.TipCnp = "Impto. Venta de Divisas";
                    if (MODCVDIMMM.VComI.TipCmO > MODGTAB0.VVmd.VmdObs)
                    {
                        MODCVDIMMM.VComI.TipCmC = MODCVDIMMM.VComI.TipCmO;
                    }
                    else
                    {
                        MODCVDIMMM.VComI.TipCmC = MODGTAB0.VVmd.VmdObs;
                    }

                    break;
                case T_MODGMTA.EsRei:  //Impto. Remesa
                    _retValue = Get_Imp(MODCVDIMMM, Mdi_Principal, "REI");
                    MODCVDIMMM.VComI.TipCnp = "Remesa de Intereses";
                    MODCVDIMMM.VComI.TipCmC = MODGTAB0.VVmd.VmdObs;
                    break;
                default:
                    return Get_Com(MODCVDIMMM, Mdi_Principal);
            }

            return _retValue;
        }

        public static string Get_Com(T_MODCVDIMMM MODCVDIMMM, UI_Mdi_Principal Mdi_Principal)
        {
            using (var trace = new Tracer())
            {
                try
                {
                    using (var unit = new UnitOfWorkCext01())
                    {
                        var res = unit.SceRepository.EjecutarSP<string>("sce_mta1_s04_MS ",
                            MODCVDIMMM.VComI.codsis,
                            MODCVDIMMM.VComI.codpro,
                            MODCVDIMMM.VComI.CodEta,
                            MODGSYB.dbdatesy(VB6Helpers.Format(VB6Helpers.CStr(DateTime.Now), "dd/MM/yyyy")));

                        if (res.Count == 0)
                        {
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "No encontro la Cuenta Contable (Sce_Cta)."
                            });
                            return "";
                        }
                        else
                        {
                            string com = res.First();
                            return com;
                        }

                    }
                }
                catch (Exception ex)
                {
                    trace.TraceException("Alerta", ex);
                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error al tratar de leer una Cuenta Contable (Sce_Cta)."
                    });
                    return "";
                }
            }
        }

        public static string Get_Gas(T_MODCVDIMMM MODCVDIMMM, UI_Mdi_Principal Mdi_Principal)
        {
            using (var trace = new Tracer())
            {
                try
                {
                    using (var unit = new UnitOfWorkCext01())
                    {
                        var res = unit.SceRepository.EjecutarSP<string>("sce_mta2_s04", MODCVDIMMM.VComI.codsis, MODCVDIMMM.VComI.codpro, MODCVDIMMM.VComI.CodEta);
                        if (res.Count == 0)
                        {
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Text = "No encontro la Cuenta Contable (Sce_Cta).",
                                Type = TipoMensaje.Error
                            });
                            return "";
                        }
                        else
                        {
                            return res.First();
                        }
                    }
                }
                catch (Exception ex)
                {
                    trace.TraceException("Alerta", ex);
                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error al tratar de leer una Cuenta Contable (Sce_Cta)."
                    });
                    return "";
                }
            }
        }

        public static string Get_Imp(T_MODCVDIMMM MODCVDIMMM, UI_Mdi_Principal Mdi_Principal, string Impuesto)
        {
            using (var trace = new Tracer())
            {
                try
                {
                    using (var unit = new UnitOfWorkCext01())
                    {
                        var res = unit.SceRepository.EjecutarSP<string>("sce_mta3_s05", Impuesto);
                        if (res.Count == 0)
                        {
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Text = "No encontro la Cuenta Contable (Sce_Cta).",
                                Type = TipoMensaje.Error
                            });
                            return "";
                        }
                        else
                        {
                            return res.First();
                        }
                    }
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error al tratar de leer una Cuenta Contable (Sce_Cta)."
                    });
                    return "";
                }
            }
        }

        public static string RetSoloAlfa(string NumBlw)
        {
            // UPGRADE_INFO (#05B1): The 'k' variable wasn't declared explicitly.
            short k = 0;
            // UPGRADE_INFO (#05B1): The 'a' variable wasn't declared explicitly.
            string a = "";
            string Sale = "";
            for (k = 1; k <= (short)VB6Helpers.Len(NumBlw); k++)
            {
                a = VB6Helpers.Mid(NumBlw, k, 1);
                if (VB6Helpers.IsNumeric(a))
                {
                    // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 'Sale' variable as a StringBuilder6 object.
                    Sale += a;
                }

            }

            return Sale;
        }

        public static short Val_Endo(InitializationObject initObject, short mens)
        {
            T_MODGFYS MODGFYS = initObject.MODGFYS;
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;

            short _retValue = 0;
            // UPGRADE_INFO (#05B1): The 'j' variable wasn't declared explicitly.
            short j = 0;

            _retValue = (short)(false ? -1 : 0);

            if (MODGFYS.CVD.Operacion == T_MODGFYS.ENDREC && MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_VisImp)
            {
                for (j = 0; j <= (short)VB6Helpers.UBound(MODGFYS.Planillas); j++)
                {
                    if (~MODGFYS.Planillas[j].Lista != 0)
                    {
                        if (mens != 0)
                        {
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Falta Definir Planilla Nº " + MODGFYS.Planillas[j].num_planilla + ". Debe Registrarla"
                            });
                        }
                        // UPGRADE_WARNING (#80F4): The Screen6.MousePointer property sets or returns the MousePointer property of the active form, but only if it's a VB6Form.
                        return _retValue;
                    }

                }

            }

            return (short)(true ? -1 : 0);
        }

        public static void GenPlAlad(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODGFYS MODGFYS = initObject.MODGFYS;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            T_MODGSWF MODGSWF = initObject.MODGSWF;

            // UPGRADE_INFO (#05B1): The 'nv' variable wasn't declared explicitly.
            short nv = 0;
            // UPGRADE_INFO (#05B1): The 'np' variable wasn't declared explicitly.
            short np = 0;
            // UPGRADE_INFO (#05B1): The 'Op' variable wasn't declared explicitly.
            short Op = 0;
            // UPGRADE_INFO (#05B1): The 'n' variable wasn't declared explicitly.
            short n = 0;
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;
            // UPGRADE_INFO (#05B1): The 'PaiAla' variable wasn't declared explicitly.
            short PaiAla = 0;


            nv = 0;
            np = 0;
            nv = (short)VB6Helpers.UBound(MODXVIA.VxVia);
            np = (short)VB6Helpers.UBound(MODGFYS.Planillas);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            Op = 0;
            n = 0;
            for (i = 0; i <= (short)nv; i++)
            {
                if (MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_OPC || MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CTACTEBC)
                {
                    Op = i;
                    n = (short)(n + 1);
                    break;
                }

            }

            if (Op == 0)
            {
                for (i = 0; i <= (short)np; i++)
                {
                    PaiAla = MODGTAB0.VPai[BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VPai(initObject, unit, MODGFYS.Planillas[i].Cod_Paispago)].Pai_PaiAla;
                    if ((PaiAla & (MODGFYS.Planillas[i].Cod_Mon == T_MODGTAB0.MndDol ? -1 : 0)) != 0)
                    {
                        MODGFYS.Planillas[i].HayAcuerdo = (short)(false ? -1 : 0);
                        MODGFYS.Planillas[i].num_acuerdos = 0;
                        MODGFYS.Planillas[i].acuerdo[0] = "";
                        MODGFYS.Planillas[i].fecha_debito = "";
                        MODGFYS.Planillas[i].NDoc1 = "";
                        MODGFYS.Planillas[i].NDoc2 = "";
                    }

                }

            }
            else
            {
                for (i = 0; i <= (short)np; i++)
                {
                    PaiAla = MODGTAB0.VPai[BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VPai(initObject, unit, MODGFYS.Planillas[i].Cod_Paispago)].Pai_PaiAla;
                    if ((PaiAla & (MODGFYS.Planillas[i].Cod_Mon == T_MODGTAB0.MndDol ? -1 : 0)) != 0)
                    {
                        MODGFYS.Planillas[i].HayAcuerdo = (short)(true ? -1 : 0);
                        MODGFYS.Planillas[i].num_acuerdos = 1;
                        MODGFYS.Planillas[i].acuerdo[0] = "1493";
                        if (n == 1)
                        {
                            if (MODXVIA.VxVia[Op].NumCta == T_MODGCON0.IdCta_OPC)
                            {
                                MODGFYS.Planillas[i].fecha_debito = MODGSWF.VSwf[MODXVIA.VxVia[Op].IndSwf].DatSwf.FecPag;
                                //Planillas(i%).NDoc1 = Left(VSwf(VxVia(Op%).IndSwf).DatSwf.NroAla, 15)
                                MODGFYS.Planillas[i].NDoc1 = VB6Helpers.Left(MODGSWF.VSwf[MODXVIA.VxVia[Op].IndSwf].DatSwf.NroAla, 18);
                            }
                            else
                            {
                                //Planillas(i%).NDoc1 = Left(VxVia(Op%).numdoc, 15)
                                MODGFYS.Planillas[i].NDoc1 = VB6Helpers.Left(MODXVIA.VxVia[Op].numdoc, 18);
                            }

                        }

                    }

                }

            }

        }

        public static short ValPlan(InitializationObject initObject)
        {
            T_MODGFYS MODGFYS = initObject.MODGFYS;
            UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;

            short _retValue = 0;
            // UPGRADE_INFO (#05B1): The 'np' variable wasn't declared explicitly.
            short np = 0;
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;

            np = 0;
            np = (short)VB6Helpers.UBound(MODGFYS.Planillas);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            _retValue = (short)(false ? -1 : 0);

            for (i = 0; i <= (short)np; i++)
            {

                if (MODGFYS.Planillas[i].HayAcuerdo != 0)
                {
                    if (string.IsNullOrEmpty(MODGFYS.Planillas[i].fecha_debito))
                    {
                        Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Falta ingresar la fecha de debito en las planillas aladi. Para continuar debe completar esta información."
                        });
                        return _retValue;
                    }

                    if (string.IsNullOrEmpty(MODGFYS.Planillas[i].NDoc1))
                    {
                        Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Falta ingresar el documento nacional en las planillas aladi. Para continuar debe completar esta información."
                        });
                        return _retValue;
                    }

                    if (string.IsNullOrEmpty(MODGFYS.Planillas[i].NDoc2))
                    {
                        Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Falta ingresar el documento extranjero en las planillas aladi. Para continuar debe completar esta información."
                        });
                        return _retValue;
                    }

                }

            }

            return (short)(true ? -1 : 0);
        }

        public static void ImpPlanillasTodas(InitializationObject initObject, UnitOfWorkCext01 unit, short Copias)
        {
            T_MODCVDIMMM MODCVDIMMM = initObject.MODCVDIMMM;
            T_MODGFYS MODGFYS = initObject.MODGFYS;

            // UPGRADE_INFO (#05B1): The 'n' variable wasn't declared explicitly.
            short n = 0;
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;
            // UPGRADE_INFO (#05B1): The 'j' variable wasn't declared explicitly.
            short j = 0;


            n = (short)VB6Helpers.UBound(MODGFYS.Planillas);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            VB6Helpers.DoEvents();
            for (i = 0; i <= (short)n; i++)
            {
                for (j = 1; j <= (short)Copias; j++)
                {
                    BCH.Comex.Core.BL.XCFT.Modulos.MODGFYS.Imp_Planilla(initObject, unit, ref MODGFYS.Planillas, i, MODCVDIMMM.ISCVD);
                }

            }

            MODGFYS.Planillas = new Reg_Planilla[0];
            MODGFYS.DetInt = new Detalles[0];
        }

        public static short SyPut_VisImp(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            T_MODGFYS MODGFYS = initObject.MODGFYS;
            T_MODCVDIMMM MODCVDIMMM = initObject.MODCVDIMMM;
            T_MODGRNG MODGRNG = initObject.MODGRNG;
            T_MODGUSR MODGUSR = initObject.MODGUSR;
            UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;
            T_MODCVDIM MODCVDIM = initObject.MODCVDIM;
            T_MODGASO MODGASO = initObject.MODGASO;


            // UPGRADE_INFO (#05B1): The 'TotMto' variable wasn't declared explicitly.
            double TotMto = 0;
            // UPGRADE_INFO (#05B1): The 'TotMtoP' variable wasn't declared explicitly.
            double TotMtoP = 0;
            // UPGRADE_INFO (#05B1): The 'kk' variable wasn't declared explicitly.
            short kk = 0;
            // UPGRADE_INFO (#05B1): The 'NumPlanilla' variable wasn't declared explicitly.
            double NumPlanilla = 0;

            //Faltan Validaciones

            if ((MODGFYS.CVD.EsVisible & (MODGFYS.CVD.Operacion == T_MODGFYS.FLTSEG ? -1 : 0) & ~MODCVDIMMM.EsReverso) != 0)
            {
                TotMto = MODGFYS.VgFyS[MODCVDIMMM.ServAct].monto[1] + MODGFYS.VgFyS[MODCVDIMMM.ServAct].monto[2];
                MODGFYS.VgFyS[MODCVDIMMM.ServAct].monto[1] = TotMto;
                MODGFYS.VgFyS[MODCVDIMMM.ServAct].monto[2] = 0;
                TotMtoP = MODGFYS.VgFyS[MODCVDIMMM.ServAct].mtopss[1] + MODGFYS.VgFyS[MODCVDIMMM.ServAct].mtopss[2];
                MODGFYS.VgFyS[MODCVDIMMM.ServAct].mtopss[1] = TotMtoP;
                MODGFYS.VgFyS[MODCVDIMMM.ServAct].mtopss[2] = 0;
            }

            //Asigna Números Centralizados a las Planillas
            if (MODGFYS.CVD.EsVisible != 0)
            {
                if (MODGFYS.CVD.Operacion != T_MODGFYS.ENDREC)
                {
                    for (kk = 1; kk <= (short)VB6Helpers.UBound(MODGFYS.Planillas); kk++)
                    {
                        NumPlanilla = BCH.Comex.Core.BL.XCFT.Modulos.MODGRNG.LeeSceRng(MODGRNG, MODGUSR, Mdi_Principal, unit, T_MODGRNG.Rng_PlaVis);
                        if (NumPlanilla == -1)
                        {
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Text = "No se pudo obtener el Número de Planilla Visible. Reporte este problema inmediatamente.",
                                Type = TipoMensaje.Error,
                                Title = "Compra-Venta de Divisas"
                            });
                            MODCVDIMMM.YaEntroUnaVez = (short)(false ? -1 : 0);
                            BCH.Comex.Core.BL.XCFT.Modulos.MODCVDIM.Inicializa(MODCVDIM, MODGFYS, MODGASO, MODCVDIMMM, 0);
                            return 0;
                        }

                        MODGFYS.Planillas[kk].num_planilla = VB6Helpers.Format(VB6Helpers.CStr(NumPlanilla), "0");
                        NumPlaAnuCob(initObject, ref MODGFYS.Planillas, kk);
                    }

                }

            }

            return 0;
        }

        public static void NumPlaAnuCob(InitializationObject initObject, ref Reg_Planilla[] Pl, short ind)
        {
            T_MODCVDIMMM MODCVDIMMM = initObject.MODCVDIMMM;


            // UPGRADE_INFO (#05B1): The 'MaxAC' variable wasn't declared explicitly.
            short MaxAC = 0;
            // UPGRADE_INFO (#05B1): The 'n' variable wasn't declared explicitly.
            short n = 0;
            MaxAC = -1;

            MaxAC = (short)VB6Helpers.UBound(MODCVDIMMM.AnuCob);
            for (n = 0; n <= (short)MaxAC; n++)
            {
                if (Pl[ind].num_idi == MODCVDIMMM.AnuCob[n].NumIdi && DateTime.Parse(Pl[ind].fecha_idi).Date == DateTime.Parse(MODCVDIMMM.AnuCob[n].FecIdi).Date)
                {
                    if (Pl[ind].numdec == MODCVDIMMM.AnuCob[n].numdec && DateTime.Parse(Pl[ind].FecDec).Date == DateTime.Parse(MODCVDIMMM.AnuCob[n].FecDec).Date)
                    {
                        MODCVDIMMM.AnuCob[n].NumPla = Pl[ind].num_planilla;
                        break;
                    }

                }

            }

        }

        public static short SyPut_SceRdo(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            using (var tracer = new Tracer())
            {
                T_MODCVDIMMM MODCVDIMMM = initObject.MODCVDIMMM;
                T_MODGCHQ MODGCHQ = initObject.MODGCHQ;
                T_Module1 Module1 = initObject.Module1;
                T_MODGCON0 MODGCON0 = initObject.MODGCON0;


                short _retValue = 0;
                short MaxRDI = 0;
                string RutaSyb = "";
                string Que = "";
                dynamic MsgCobImp = null;

                MaxRDI = -1;

                MaxRDI = (short)VB6Helpers.UBound(MODCVDIMMM.RDecIni);

                _retValue = (short)(false ? -1 : 0);
                try
                {
                    for (MODGCHQ.Indice = 0; MODGCHQ.Indice <= (short)MaxRDI; MODGCHQ.Indice++)
                    {
                        List<string> parameters = new List<string>();
                        int indice = MODGCHQ.Indice;
                        // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 'Que' variable as a StringBuilder6 object.
                        parameters.Add(MODGSYB.dbcharSy(Module1.Codop.Cent_Costo));
                        parameters.Add(MODGSYB.dbcharSy(Module1.Codop.Id_Product));
                        parameters.Add(MODGSYB.dbcharSy(Module1.Codop.Id_Especia));
                        parameters.Add(MODGSYB.dbcharSy(Module1.Codop.Id_Empresa));
                        parameters.Add(MODGSYB.dbcharSy(Module1.Codop.Id_Operacion));
                        parameters.Add(MODGSYB.dbnumesy(VB6Helpers.Val(MODCVDIMMM.RDecIni[indice].RDec_principal)));
                        parameters.Add(MODGSYB.dbcharSy(MODCVDIMMM.RDecIni[indice].RDec_numero));
                        parameters.Add(MODGSYB.dbdatesy((MODCVDIMMM.RDecIni[indice].RDec_fecha)));
                        parameters.Add(MODGSYB.dbnumesy(MODGCON0.VMch.NroRpt));
                        parameters.Add(MODGSYB.dbdatesy(MODGCON0.VMch.fecmov));
                        parameters.Add(MODGSYB.dbnumesy(indice));
                        parameters.Add(MODGSYB.dbnumesy(MODCVDIMMM.RDecIni[indice].Rdec_codmoneda));
                        parameters.Add(MODGSYB.dbPardSy(MODCVDIMMM.RDecIni[indice].RDec_paridad));
                        parameters.Add(MODGSYB.dbmontoSy(MODCVDIMMM.RDecIni[indice].RDec_relfob));
                        parameters.Add(MODGSYB.dbmontoSy(MODCVDIMMM.RDecIni[indice].RDec_relflete));
                        parameters.Add(MODGSYB.dbmontoSy(MODCVDIMMM.RDecIni[indice].RDec_relseguro));
                        parameters.Add(MODGSYB.dbmontoSy(MODCVDIMMM.RDecIni[indice].RDec_relmerma));
                        parameters.Add(MODGSYB.dbmontoSy(MODCVDIMMM.RDecIni[indice].RDec_relcif));
                        parameters.Add(MODGSYB.dbmontoSy(MODCVDIMMM.RDecIni[indice].RDec_cubfob));
                        parameters.Add(MODGSYB.dbmontoSy(MODCVDIMMM.RDecIni[indice].RDec_cubflete));
                        parameters.Add(MODGSYB.dbmontoSy(MODCVDIMMM.RDecIni[indice].RDec_cubseguro));
                        parameters.Add(MODGSYB.dbmontoSy(MODCVDIMMM.RDecIni[indice].RDec_cubmerma));
                        parameters.Add(MODGSYB.dbmontoSy(MODCVDIMMM.RDecIni[indice].RDec_cubcif));
                        parameters.Add(MODGSYB.dbmontoSy(MODCVDIMMM.RDecIni[indice].RDec_fobmer));
                        parameters.Add(MODGSYB.dbmontoSy(MODCVDIMMM.RDecIni[indice].RDec_flemer));
                        parameters.Add(MODGSYB.dbmontoSy(MODCVDIMMM.RDecIni[indice].RDec_segmer));
                        parameters.Add(MODGSYB.dbnumesy(T_MODGCON0.ECC_ING));

                        if (~Mdl_Funciones_Varias.Cmd_Put_New(initObject.Mdl_Funciones_Varias, () =>
                        {
                            return (short)unit.SceRepository.EjecutarSPConRetorno("sce_grdo_i04", String.Empty, parameters.ToArray());
                        }) != 0)
                        {
                            return _retValue;
                        }

                    }
                    _retValue = (short)(true ? -1 : 0);
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta", e);

                    initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Error al grabar las relaciones de declaraciones de importación",
                        Type = TipoMensaje.Error
                    });
                    _retValue = 0;
                }
                return _retValue;
            }
        }

        public static short SyPut_SceReb(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            using (var tracer = new Tracer())
            {
                T_MODCVDIMMM MODCVDIMMM = initObject.MODCVDIMMM;
                T_Module1 Module1 = initObject.Module1;
                T_MODGCON0 MODGCON0 = initObject.MODGCON0;

                short _retValue = 0;
                short Anu_Cob = 0;
                string RutaSyb = "";
                short i = 0;
                string Que = "";
                string R = "";
                dynamic MsgCvd_Imp = null;


                Anu_Cob = (short)VB6Helpers.UBound(MODCVDIMMM.AnuCob);
                // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
                // IGNORED: On Error GoTo 0

                _retValue = (short)(false ? -1 : 0);

                try
                {
                    for (i = 0; i <= (short)Anu_Cob; i++)
                    {
                        List<string> parameters = new List<string>();

                        Que = "exec " + RutaSyb + "sce_reb_w02 ";
                        // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 'Que' variable as a StringBuilder6 object.
                        parameters.Add(MODGSYB.dbcharSy(Module1.Codop.Cent_Costo));
                        parameters.Add(MODGSYB.dbcharSy(Module1.Codop.Id_Product));
                        parameters.Add(MODGSYB.dbcharSy(Module1.Codop.Id_Especia));
                        parameters.Add(MODGSYB.dbcharSy(Module1.Codop.Id_Empresa));
                        parameters.Add(MODGSYB.dbcharSy(Module1.Codop.Id_Operacion));
                        parameters.Add(MODGSYB.dbnumesy(VB6Helpers.Val(MODCVDIMMM.AnuCob[i].NumPla)));
                        parameters.Add(MODGSYB.dbcharSy(MODCVDIMMM.AnuCob[i].numdec));
                        parameters.Add(MODGSYB.dbdatesy(MODCVDIMMM.AnuCob[i].FecDec));
                        parameters.Add(MODGSYB.dbnumesy(MODCVDIMMM.AnuCob[i].NumPri));
                        parameters.Add(MODGSYB.dblogisy(MODCVDIMMM.AnuCob[i].PagChi));
                        parameters.Add(MODGSYB.dbnumesy(MODCVDIMMM.AnuCob[i].Moneda));
                        parameters.Add(MODGSYB.dbmontoSy(MODCVDIMMM.AnuCob[i].MtoFob));
                        parameters.Add(MODGSYB.dbmontoSy(MODCVDIMMM.AnuCob[i].MtoFle));
                        parameters.Add(MODGSYB.dbmontoSy(MODCVDIMMM.AnuCob[i].MtoSeg));
                        parameters.Add(MODGSYB.dbmontoSy(MODCVDIMMM.AnuCob[i].FobMer));
                        parameters.Add(MODGSYB.dbmontoSy(MODCVDIMMM.AnuCob[i].FleMer));
                        parameters.Add(MODGSYB.dbmontoSy(MODCVDIMMM.AnuCob[i].SegMer));
                        parameters.Add(MODGSYB.dbmontoSy(MODCVDIMMM.AnuCob[i].MtoCif));
                        parameters.Add(MODGSYB.dbnumesy(0));  //falta cifusd para el proc. alm.
                        parameters.Add(MODGSYB.dbPardSy(MODCVDIMMM.AnuCob[i].ParDec));
                        parameters.Add(MODGSYB.dbTCamSy(MODCVDIMMM.AnuCob[i].TCVDia));
                        parameters.Add(DateTime.Now.ToString("yyyy-MM-dd"));
                        parameters.Add(MODGSYB.dblogisy(-1));
                        parameters.Add(MODGSYB.dbnumesy(MODGCON0.VMch.NroRpt));

                        int res = unit.SceRepository.EjecutarSP<int>("sce_reb_w02", parameters.ToArray()).First();
                        if (res != 0)
                        {
                            throw new Exception();
                        }
                    }
                    _retValue = (short)(true ? -1 : 0);
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta", e);
                    initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Se ha producido un error de Comunicación al tratar de grabar los rebajes(Sce_Reb).",
                        Type = TipoMensaje.Error
                    });
                    _retValue = 0;
                }
                return _retValue;
            }
        }
    }
}
