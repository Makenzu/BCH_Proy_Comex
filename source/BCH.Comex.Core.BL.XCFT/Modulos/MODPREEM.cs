using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODPREEM
    {

        public static T_MODPREEM GetMODPREEM()
        {
            return new T_MODPREEM();
        }

        //Tipos de Autorizacion.-
        public static void CargaEnLista_TipAut(T_Mdl_Funciones_Varias Mdl_Funciones_Varias, UI_Combo Lista)
        {
            short i = 0;
            if (Lista.Items.Count == 0)
            {

                Lista.Items = Mdl_Funciones_Varias.V_TipAut.Select(x => new UI_ComboItem()
                {
                    Value = Mdl_Funciones_Varias.V_TipAut[i].CodAut + ":" + Mdl_Funciones_Varias.V_TipAut[i].DesAut,
                    Data = (i++)
                }).ToList();

                Lista.Items.Add(new UI_ComboItem()
                {
                    Data = i,
                    Value = "Sin Autorización Previa"
                });

                Lista.Items = Lista.Items.OrderBy(x => x.Data).ToList();
                Lista.SelectedValue = i;
            }
        }

        public static void Pr_LlenaMtoRee(T_MODPREEM MODPREEM, T_MODANUVI MODANUVI)
        {
            short i = 0;
            short reg_anu = 0;

            reg_anu = (short)VB6Helpers.UBound(MODPREEM.Vx_PReem);


            for (i = 0; i <= (short)reg_anu; i++)
            {
                if (MODPREEM.Vx_PReem[i].Estado != 9)
                {
                    MODANUVI.Vx_AnuReem.CodMon = MODPREEM.Vx_PReem[i].CodMon;
                    MODANUVI.Vx_AnuReem.CambRee = MODANUVI.Vx_AnuReem.CambRee + Format.StringToDouble(VB6Helpers.Format(VB6Helpers.CStr(MODPREEM.Vx_PReem[i].TotOri * MODPREEM.Vx_PReem[i].TipCamo), "0"));
                    MODANUVI.Vx_AnuReem.ConvRee += MODPREEM.Vx_PReem[i].TotOri;
                }
            }
        }

        public static void Pr_RefundeGrDO(T_MODCVDIMMM MODCVDIMMM)
        {
            short Dec_Fin = 0;
            short Dec_Ini = 0;
            short j = 0;
            short i = 0;
            short Encontro = 0;
            short m = 0;

            Dec_Fin = (short)VB6Helpers.UBound(MODCVDIMMM.RDecFin);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            Dec_Ini = -1;

            for (i = 0; i <= (short)Dec_Fin; i++)
            {

                Dec_Ini = (short)VB6Helpers.UBound(MODCVDIMMM.RDecIni);
                //Dec_Ini = Convert.ToInt16(MODCVDIMMM.RDecFin.Length);
                //MODCVDIMMM.RDecIni = new Common.XCFT.Domain.r_dec[Dec_Ini];

                Encontro = (short)(false ? -1 : 0);

                for (j = 0; j <= (short)Dec_Ini; j++)
                {
                    //MODCVDIMMM.RDecIni[j] = new Common.XCFT.Domain.r_dec();
                    if ((MODCVDIMMM.RDecIni[j].RDec_numero == MODCVDIMMM.RDecFin[i].RDec_numero) && (MODCVDIMMM.RDecIni[j].Rdec_codmoneda == MODCVDIMMM.RDecFin[i].Rdec_codmoneda))
                    {
                        Encontro = (short)(true ? -1 : 0);
                        MODCVDIMMM.RDecIni[j].RDec_relfob += MODCVDIMMM.RDecFin[i].RDec_relfob;
                        MODCVDIMMM.RDecIni[j].RDec_relflete += MODCVDIMMM.RDecFin[i].RDec_relflete;
                        MODCVDIMMM.RDecIni[j].RDec_relseguro += MODCVDIMMM.RDecFin[i].RDec_relseguro;
                        MODCVDIMMM.RDecIni[j].RDec_relcif += MODCVDIMMM.RDecFin[i].RDec_relcif;
                        MODCVDIMMM.RDecIni[j].RDec_cubfob += MODCVDIMMM.RDecFin[i].RDec_cubfob;
                        MODCVDIMMM.RDecIni[j].RDec_cubflete += MODCVDIMMM.RDecFin[i].RDec_cubflete;
                        MODCVDIMMM.RDecIni[j].RDec_cubseguro += MODCVDIMMM.RDecFin[i].RDec_cubseguro;
                        MODCVDIMMM.RDecIni[j].RDec_cubcif += MODCVDIMMM.RDecFin[i].RDec_cubcif;
                        break;
                    }

                }

                if (Encontro == 0)
                {
                    if (Dec_Ini == -1)
                    {
                        m = 0;
                    }
                    else
                    {
                        m = (short)(Dec_Ini + 1);
                    }

                    VB6Helpers.RedimPreserve(ref MODCVDIMMM.RDecIni, 0, m);
                    MODCVDIMMM.RDecIni[m] = MODCVDIMMM.RDecFin[i];
                }
            }
        }

        public static void Pr_PlAcceso(InitializationObject initObj, UnitOfWorkCext01 uow, string FecDec, string numdec)
        {
            short max_plan = 0;
            double ParMon = 0;
            double ParRel2 = 0;
            short ind = 0;

            var MODPREEM = initObj.MODPREEM;


            max_plan = (short)VB6Helpers.UBound(MODPREEM.Vx_PReem);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            if (numdec != "________________-_" && !string.IsNullOrEmpty(numdec))
            {
                //NO es Clausula Roja.-
                //---------------------------------
                //DECLARACION.-
                //---------------------------------
                //Paridad de la declaracion.-
                //Buscamos la paridad del dia para la moneda.-

                if (MODGMTA.Get_VPar(initObj.MODGMTA, initObj.MODGTAB0, initObj.Frm_Ingreso_Valores, uow, FecDec, initObj.MODANUVI.Var_CodMon) != 0)
                {
                    ParMon = initObj.MODGTAB0.VVmd.VmdPrd;
                }
                else
                {
                    ParMon = 1;
                }

                initObj.MODPREEM.ParDec = Par_Dec(initObj, uow, FecDec, T_MODGTAB0.MndDol);

                ParRel2 = ParMon / initObj.MODPREEM.ParDec;

                initObj.MODPREEM.ParDec = ParRel2;

                //Conversion de Montos de la Declaracion a la moneda.-
                MODPREEM.VxIdiDec.OriFob_M = MODGPYF1.Dec_EnMto(initObj, uow, MODPREEM.VxIdiDec.OriFob * (ParRel2), T_MODGTAB0.MndDol);
                MODPREEM.VxIdiDec.RelFob_M = MODGPYF1.Dec_EnMto(initObj, uow, MODPREEM.VxIdiDec.RelFob * (ParRel2), T_MODGTAB0.MndDol);
                MODPREEM.VxIdiDec.CubFob_M = MODGPYF1.Dec_EnMto(initObj, uow, MODPREEM.VxIdiDec.CubFob * (ParRel2), T_MODGTAB0.MndDol);
                if (MODPREEM.VxIdiDec.flag == 1 || MODPREEM.VxIdiDec.flag == 2)
                {
                    MODPREEM.VxIdiDec.DisFob_M = 9999999999999.99;
                }
                else
                {
                    MODPREEM.VxIdiDec.DisFob_M = MODGPYF1.Dec_EnMto(initObj, uow, MODPREEM.VxIdiDec.DisFob * (ParRel2), T_MODGTAB0.MndDol);
                }

                MODPREEM.VxIdiDec.OriFle_M = MODGPYF1.Dec_EnMto(initObj, uow, MODPREEM.VxIdiDec.OriFle * (ParRel2), T_MODGTAB0.MndDol);
                MODPREEM.VxIdiDec.RelFle_M = MODGPYF1.Dec_EnMto(initObj, uow, MODPREEM.VxIdiDec.RelFle * (ParRel2), T_MODGTAB0.MndDol);
                MODPREEM.VxIdiDec.CubFle_M = MODGPYF1.Dec_EnMto(initObj, uow, MODPREEM.VxIdiDec.CubFle * (ParRel2), T_MODGTAB0.MndDol);
                if (MODPREEM.VxIdiDec.flag == 1 || MODPREEM.VxIdiDec.flag == 2)
                {
                    MODPREEM.VxIdiDec.DisFle_M = 9999999999999.99;
                }
                else
                {
                    MODPREEM.VxIdiDec.DisFle_M = MODGPYF1.Dec_EnMto(initObj, uow, MODPREEM.VxIdiDec.DisFle * (ParRel2), T_MODGTAB0.MndDol);
                }

                MODPREEM.VxIdiDec.OriSeg_M = MODGPYF1.Dec_EnMto(initObj, uow, MODPREEM.VxIdiDec.OriSeg * (ParRel2), T_MODGTAB0.MndDol);
                MODPREEM.VxIdiDec.RelSeg_M = MODGPYF1.Dec_EnMto(initObj, uow, MODPREEM.VxIdiDec.RelSeg * (ParRel2), T_MODGTAB0.MndDol);
                MODPREEM.VxIdiDec.CubSeg_M = MODGPYF1.Dec_EnMto(initObj, uow, MODPREEM.VxIdiDec.CubSeg * (ParRel2), T_MODGTAB0.MndDol);
                if (MODPREEM.VxIdiDec.flag == 1 || MODPREEM.VxIdiDec.flag == 2)
                {
                    MODPREEM.VxIdiDec.DisSeg_M = 9999999999999.99;
                }
                else
                {
                    MODPREEM.VxIdiDec.DisSeg_M = MODGPYF1.Dec_EnMto(initObj, uow, MODPREEM.VxIdiDec.DisSeg * (ParRel2), T_MODGTAB0.MndDol);
                }

                MODPREEM.VxIdiDec.OriCif_M = MODGPYF1.Dec_EnMto(initObj, uow, MODPREEM.VxIdiDec.OriCif * (ParRel2), T_MODGTAB0.MndDol);
                MODPREEM.VxIdiDec.RelCif_M = MODGPYF1.Dec_EnMto(initObj, uow, MODPREEM.VxIdiDec.RelCif * (ParRel2), T_MODGTAB0.MndDol);
                MODPREEM.VxIdiDec.CubCif_M = MODGPYF1.Dec_EnMto(initObj, uow, MODPREEM.VxIdiDec.CubCif * (ParRel2), T_MODGTAB0.MndDol);
                if (MODPREEM.VxIdiDec.flag == 1 || MODPREEM.VxIdiDec.flag == 2)
                {
                    MODPREEM.VxIdiDec.DisCif_M = 9999999999999.99;
                }
                else
                {
                    MODPREEM.VxIdiDec.DisCif_M = MODGPYF1.Dec_EnMto(initObj, uow, MODPREEM.VxIdiDec.DisCif * (ParRel2), T_MODGTAB0.MndDol);
                }

                //Recorremos la estructura de reemplazo para actualizar los montos de acceso.-
                for (ind = 1; ind <= (short)max_plan; ind++)
                {
                    if (MODPREEM.Vx_PReem[ind].numdec == numdec)
                    {
                        MODPREEM.VxIdiDec.RelFob_M += MODPREEM.Vx_PReem[ind].MtoFob;
                        MODPREEM.VxIdiDec.CubFob_M += MODPREEM.Vx_PReem[ind].MtoFob;
                        if (MODPREEM.VxIdiDec.flag == 1 || MODPREEM.VxIdiDec.flag == 2)
                        {
                            MODPREEM.VxIdiDec.DisFob_M = 9999999999999.99;
                        }
                        else
                        {
                            MODPREEM.VxIdiDec.DisFob_M -= MODPREEM.Vx_PReem[ind].MtoFob;
                        }

                        MODPREEM.VxIdiDec.RelFle_M += MODPREEM.Vx_PReem[ind].MtoFle;
                        MODPREEM.VxIdiDec.CubFle_M += MODPREEM.Vx_PReem[ind].MtoFle;
                        if (MODPREEM.VxIdiDec.flag == 1 || MODPREEM.VxIdiDec.flag == 2)
                        {
                            MODPREEM.VxIdiDec.DisFle_M = 9999999999999.99;
                        }
                        else
                        {
                            MODPREEM.VxIdiDec.DisFle_M -= MODPREEM.Vx_PReem[ind].MtoFle;
                        }

                        MODPREEM.VxIdiDec.RelSeg_M += MODPREEM.Vx_PReem[ind].MtoSeg;
                        MODPREEM.VxIdiDec.CubSeg_M += MODPREEM.Vx_PReem[ind].MtoSeg;
                        if (MODPREEM.VxIdiDec.flag == 1 || MODPREEM.VxIdiDec.flag == 2)
                        {
                            MODPREEM.VxIdiDec.DisSeg_M = 9999999999999.99;
                        }
                        else
                        {
                            MODPREEM.VxIdiDec.DisSeg_M -= MODPREEM.Vx_PReem[ind].MtoSeg;
                        }

                        MODPREEM.VxIdiDec.RelCif_M += MODPREEM.Vx_PReem[ind].MtoCif;
                        MODPREEM.VxIdiDec.CubCif_M += MODPREEM.Vx_PReem[ind].MtoCif;
                        if (MODPREEM.VxIdiDec.flag == 1 || MODPREEM.VxIdiDec.flag == 2)
                        {
                            MODPREEM.VxIdiDec.DisCif_M = 9999999999999.99;
                        }
                        else
                        {
                            MODPREEM.VxIdiDec.DisCif_M -= MODPREEM.Vx_PReem[ind].MtoCif;
                        }

                    }

                }

            }
            else if (numdec == "________________-_" || numdec == "")
            {
                //ES Clausula Roja.-
                MODPREEM.VxIdiDec.OriCif_M = 9999999999999.99;
                MODPREEM.VxIdiDec.RelCif_M = 0;
                MODPREEM.VxIdiDec.CubCif_M = 0;
                if (MODPREEM.VxIdiDec.flag == 1 || MODPREEM.VxIdiDec.flag == 2)
                {
                    MODPREEM.VxIdiDec.DisCif_M = 9999999999999.99;
                }
                else
                {
                    MODPREEM.VxIdiDec.DisCif_M = MODPREEM.VxIdiDec.DisMon;
                }

                MODPREEM.VxIdiDec.DisFob_M = 9999999999999.99;
                MODPREEM.VxIdiDec.DisFle_M = 9999999999999.99;
                MODPREEM.VxIdiDec.DisSeg_M = 9999999999999.99;
                MODPREEM.VxIdiDec.DisMon = 9999999999999.99;

                //Recorremos la estructura de reemplazo para actualizar los montos de acceso.-
                for (ind = 1; ind <= (short)max_plan; ind++)
                {
                    if (MODPREEM.Vx_PReem[ind].numdec == numdec)
                    {
                        MODPREEM.VxIdiDec.RelCif_M += MODPREEM.Vx_PReem[ind].MtoCif;
                        MODPREEM.VxIdiDec.CubCif_M += MODPREEM.Vx_PReem[ind].MtoCif;
                        if (MODPREEM.VxIdiDec.flag == 1 || MODPREEM.VxIdiDec.flag == 2)
                        {
                            MODPREEM.VxIdiDec.DisCif_M = 9999999999999.99;
                        }
                        else
                        {
                            MODPREEM.VxIdiDec.DisCif_M -= MODPREEM.Vx_PReem[ind].MtoCif;
                        }
                    }
                }
            }
        }

        public static double Par_Dec(InitializationObject initObj, UnitOfWorkCext01 uow, string Fecha, short mon)
        {
            double _retValue = 0;

            short año = 0;
            short mes = 0;
            string fecbas = "";
            string fecpen = "";
            short cont = 0;
            string RutaSyb = "";
            short Contdia = 0;
            try
            {
                // IGNORED: On Error GoTo ErrorSyParRel

                //RutaSyb = Mdl_SRM.ParamSrm8k.Base + "." + Mdl_SRM.ParamSrm8k.Usuario + ".";TODO ARKANO

                año = VB6Helpers.CShort(VB6Helpers.Format(Fecha, "yyyy"));
                mes = VB6Helpers.CShort(VB6Helpers.Format(Fecha, "M"));

                fecbas = VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.DateSerial(año, mes, 1)), "yyyy-MM-dd");
                fecpen = fecbas;
                Contdia = 0;
                cont = 0;
                while (Contdia != 2 && cont < 7)
                {

                    if (Convert.ToDateTime(fecpen) != DateTime.Parse("01/01/1900"))
                    {
                        fecpen = VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.DateAdd("d", -1, VB6Helpers.CDate(fecpen))), "yyyy-MM-dd");
                    }

                    cont = (short)(cont + 1);
                    if (MODGTAB0.Fecha_Habil(initObj, uow, ref fecpen) != 0)
                    {
                        if (MODGTAB0.SyGet_Vmd(initObj.MODGTAB0, uow, fecpen, mon) != 0)
                        {
                            Contdia = (short)(Contdia + 1);
                        }

                    }

                }

                if (MODGMTA.Get_VPar(initObj.MODGMTA, initObj.MODGTAB0, initObj.Frm_Ingreso_Valores, uow, fecpen, mon) != 0)
                {
                    _retValue = initObj.MODGTAB0.VVmd.VmdPrd;
                }
                else
                {
                    _retValue = -1;
                }

                //FinSyParRel:
            }
            catch (Exception _ex)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Error al leer los valores diarios de la moneda: [" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                    Type = TipoMensaje.Informacion,
                    Title = "Tabla de Paridades"
                });
            }
            return _retValue;
        }

        public static short SyGet_IdiDec(InitializationObject initObj, UnitOfWorkCext01 uow, string numdec, string FecDec, short CodPag, string Party)
        {
            using (var tracer = new Tracer())
            {
                List<string> parameters = new List<string>();
                parameters.Add(MODGSYB.dbcharSy(numdec));
                parameters.Add(MODGSYB.dbdatesy(FecDec));
                parameters.Add(MODGSYB.dbnumesy(CodPag));
                parameters.Add(MODGSYB.dbcharSy(Party));
                try
                {
                    uow.SceRepository.ReadQuerySP((reader) =>
                    {
                        while (reader.Read())
                        {
                            initObj.MODPREEM.VxIdiDec.flag = Convert.ToInt16(reader.GetValue(0));
                            initObj.MODPREEM.VxIdiDec.OriFob = Convert.ToInt16(reader.GetValue(1));
                            initObj.MODPREEM.VxIdiDec.RelFob = Convert.ToInt16(reader.GetValue(2));
                            initObj.MODPREEM.VxIdiDec.CubFob = Convert.ToInt16(reader.GetValue(3));
                            initObj.MODPREEM.VxIdiDec.DisFob = Convert.ToInt16(reader.GetValue(4));
                            initObj.MODPREEM.VxIdiDec.OriFle = Convert.ToInt16(reader.GetValue(5));
                            initObj.MODPREEM.VxIdiDec.RelFle = Convert.ToInt16(reader.GetValue(6));
                            initObj.MODPREEM.VxIdiDec.CubFle = Convert.ToInt16(reader.GetValue(7));
                            initObj.MODPREEM.VxIdiDec.DisFle = Convert.ToInt16(reader.GetValue(8));
                            initObj.MODPREEM.VxIdiDec.OriSeg = Convert.ToInt16(reader.GetValue(9));
                            initObj.MODPREEM.VxIdiDec.RelSeg = Convert.ToInt16(reader.GetValue(10));
                            initObj.MODPREEM.VxIdiDec.CubSeg = Convert.ToInt16(reader.GetValue(11));
                            initObj.MODPREEM.VxIdiDec.DisSeg = Convert.ToInt16(reader.GetValue(12));
                            initObj.MODPREEM.VxIdiDec.OriCif = Convert.ToInt16(reader.GetValue(13));
                            initObj.MODPREEM.VxIdiDec.RelCif = Convert.ToInt16(reader.GetValue(14));
                            initObj.MODPREEM.VxIdiDec.CubCif = Convert.ToInt16(reader.GetValue(15));
                            initObj.MODPREEM.VxIdiDec.DisCif = Convert.ToInt16(reader.GetValue(16));
                            initObj.MODPREEM.VxIdiDec.FecEmb = Convert.ToString(reader.GetValue(17));
                            initObj.MODPREEM.VxIdiDec.ForPag = reader.IsDBNull(18) ? Convert.ToInt16(0) : Convert.ToInt16(reader.GetValue(18));
                            initObj.MODPREEM.VxIdiDec.codpai = reader.IsDBNull(19) ? Convert.ToInt16(0) : Convert.ToInt16(reader.GetValue(19));
                        }
                    }, "sce_plrm_s03_MS", MODGSYB.dbcharSy(numdec), MODGSYB.dbdatesy(FecDec), MODGSYB.dbnumesy(CodPag), MODGSYB.dbcharSy(Party));
                    return -1;
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta", e);

                    return 0;
                }
                //Error en el Query.
                /*if (R == null)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error al tratar de leer la tabla Sce_idi y Sce_Dec (Sce_iDoc).",
                        Title = T_MODANUVI.MsgRemPv
                    });

                    throw new Exception();
                }*/

            }
        }

        public static short Getn_Conoc(InitializationObject initObj, UnitOfWorkCext01 uow, string numdec, string FecDec)
        {
            short _retValue = 0;
            List<string> R = new List<string>();
            int n = 0;
            short i = 0;
            _retValue = 0;

            try
            {
                uow.SceRepository.ReadQuerySP((reader) =>
                {
                    while (reader.Read())
                    {
                        R.Add(reader.GetString(0));//TODO VALIDAR QUE SEA LA COLUMNA 1!! 
                    }
                }, "sce_blw_s02", MODGSYB.dbcharSy(numdec), MODGSYB.dbdatesy(FecDec));


                if (R.Count == 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "Error al leer los Conocimientos de Embarque.",
                        Type = TipoMensaje.Informacion,
                        Title = T_MODANUVI.MsgRemPv
                    });

                    return _retValue;
                }

                n = R.Count;
                initObj.MODPREEM.Vx_ConEmb = new Tx_ConEmb[n];
                for (i = 0; i < (short)n; i++)
                {
                    initObj.MODPREEM.Vx_ConEmb[i] = new Tx_ConEmb();
                    initObj.MODPREEM.Vx_ConEmb[i].numdec = numdec;
                    initObj.MODPREEM.Vx_ConEmb[i].FecDec = FecDec;
                    initObj.MODPREEM.Vx_ConEmb[i].NumBlw = R[i];//VB6Helpers.CStr(MODGSYB.GetPosSy(1, "C", R));                    
                }
                _retValue = -1;
            }
            catch (Exception _ex)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Error al leer los conocimientos de embarque.",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODANUVI.MsgRemPv
                });
            }
            return _retValue;
        }

        //Relacion Rebajes.-
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static void Pr_RelReb(InitializationObject initObj, short IndReb, short Ing_Mod)
        {
            short Largo_Anucob = 0;
            short Ind_Reb = 0;
            // Agregar nueva relación de rebajes

            Largo_Anucob = (short)VB6Helpers.UBound(initObj.MODCVDIMMM.AnuCob);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            if (Ing_Mod == -1)
            {
                Ind_Reb = (short)(Largo_Anucob + 1);
                VB6Helpers.RedimPreserve(ref initObj.MODCVDIMMM.AnuCob, 0, Ind_Reb);
            }
            else
            {
                Ind_Reb = IndReb;
            }

            initObj.MODCVDIMMM.AnuCob[Ind_Reb].NumPla = VB6Helpers.Str(initObj.MODPREEM.Vx_PReem[IndReb].NumPla);
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].NumIdi = VB6Helpers.Str(initObj.MODPREEM.Vx_PReem[IndReb].NumIdi);
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].FecIdi = initObj.MODPREEM.Vx_PReem[IndReb].FecIdi;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].numdec = initObj.MODPREEM.Vx_PReem[IndReb].numdec;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].FecDec = initObj.MODPREEM.Vx_PReem[IndReb].FecDec;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].NumPri = 0;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].PagChi = 0;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].Moneda = initObj.MODANUVI.Var_CodMon;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].MtoFob = initObj.MODPREEM.Vx_PReem[IndReb].MtoFob;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].MtoFle = initObj.MODPREEM.Vx_PReem[IndReb].MtoFle;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].MtoSeg = initObj.MODPREEM.Vx_PReem[IndReb].MtoSeg;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].FobMer = 0;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].FleMer = 0;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].SegMer = 0;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].MtoCif = initObj.MODPREEM.Vx_PReem[IndReb].MtoCif;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].CifUsd = initObj.MODPREEM.Vx_PReem[IndReb].CifDol;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].ParDec = initObj.MODPREEM.ParDec;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].ParIdi = initObj.MODPREEM.ParIdi;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].TCVDia = initObj.MODPREEM.Vx_PReem[IndReb].TipCamo;
        }

        //Relacion de la declaracion
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static void Pr_RelDec(InitializationObject initObj, short IndDec, short Ing_Mod)
        {
            short Largo_Dec = 0;
            short Indice_Dec = 0;

            Largo_Dec = (short)VB6Helpers.UBound(initObj.MODCVDIMMM.RDecFin);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            if (Ing_Mod == -1)
            {
                Indice_Dec = (short)(Largo_Dec + 1);
                VB6Helpers.RedimPreserve(ref initObj.MODCVDIMMM.RDecFin, 0, Indice_Dec);
            }
            else
            {
                Indice_Dec = IndDec;
            }


            initObj.MODCVDIMMM.RDecFin[Indice_Dec].RDec_numero = initObj.MODPREEM.Vx_PReem[IndDec].numdec;
            initObj.MODCVDIMMM.RDecFin[Indice_Dec].RDec_fecha = initObj.MODPREEM.Vx_PReem[IndDec].FecDec;
            initObj.MODCVDIMMM.RDecFin[Indice_Dec].RDec_principal = "";
            initObj.MODCVDIMMM.RDecFin[Indice_Dec].RDec_relfob = initObj.MODPREEM.Vx_PReem[IndDec].MtoFob;
            initObj.MODCVDIMMM.RDecFin[Indice_Dec].RDec_relflete = initObj.MODPREEM.Vx_PReem[IndDec].MtoFle;
            initObj.MODCVDIMMM.RDecFin[Indice_Dec].RDec_relseguro = initObj.MODPREEM.Vx_PReem[IndDec].MtoSeg;
            initObj.MODCVDIMMM.RDecFin[Indice_Dec].RDec_relmerma = 0;
            initObj.MODCVDIMMM.RDecFin[Indice_Dec].RDec_relcif = initObj.MODPREEM.Vx_PReem[IndDec].MtoCif;
            initObj.MODCVDIMMM.RDecFin[Indice_Dec].RDec_cubfob = initObj.MODPREEM.Vx_PReem[IndDec].MtoFob;
            initObj.MODCVDIMMM.RDecFin[Indice_Dec].RDec_cubflete = initObj.MODPREEM.Vx_PReem[IndDec].MtoFle;
            initObj.MODCVDIMMM.RDecFin[Indice_Dec].RDec_cubseguro = initObj.MODPREEM.Vx_PReem[IndDec].MtoSeg;
            initObj.MODCVDIMMM.RDecFin[Indice_Dec].RDec_cubcif = initObj.MODPREEM.Vx_PReem[IndDec].MtoCif;
            initObj.MODCVDIMMM.RDecFin[Indice_Dec].RDec_cubmerma = 0;
            initObj.MODCVDIMMM.RDecFin[Indice_Dec].RDec_fobmer = 0;
            initObj.MODCVDIMMM.RDecFin[Indice_Dec].RDec_flemer = 0;
            initObj.MODCVDIMMM.RDecFin[Indice_Dec].RDec_segmer = 0;
            initObj.MODCVDIMMM.RDecFin[Indice_Dec].Rdec_codmoneda = initObj.MODANUVI.Var_CodMon;
            initObj.MODCVDIMMM.RDecFin[Indice_Dec].RDec_paridad = initObj.MODPREEM.ParDec;
            initObj.MODCVDIMMM.RDecFin[Indice_Dec].RDec_numidi = VB6Helpers.Str(initObj.MODPREEM.Vx_PReem[IndDec].NumIdi);
            initObj.MODCVDIMMM.RDecFin[Indice_Dec].RDec_fechaidi = initObj.MODPREEM.Vx_PReem[IndDec].FecIdi;
        }

        //Rescata los que faltan para llenar la estructura de planilla de reemplazo.-
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static void Pr_DatosPl_R(InitializationObject initObj, int NumPln_R, string FecPln_R, short Indice_r)
        {
            short i = 0;
            short largo_planu = 0;

            largo_planu = (short)VB6Helpers.UBound(initObj.MODANUVI.V_PlAnu);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            for (i = 0; i <= (short)largo_planu; i++)
            {
                if (VB6Helpers.Format(VB6Helpers.CStr(initObj.MODANUVI.V_PlAnu[i].NumPre), "000000") == VB6Helpers.Format(VB6Helpers.CStr(NumPln_R), "000000") && VB6Helpers.Format(initObj.MODANUVI.V_PlAnu[i].FecVen, "yyyymmdd") == VB6Helpers.Format(FecPln_R, "yyyymmdd"))
                {
                    initObj.MODPREEM.Vx_PReem[Indice_r].CodEnt_R = 15;
                    initObj.MODPREEM.Vx_PReem[Indice_r].NumInf_R = initObj.MODANUVI.V_PlAnu[i].NumIdi;
                    initObj.MODPREEM.Vx_PReem[Indice_r].FecInf_R = initObj.MODANUVI.V_PlAnu[i].FecIdi;
                    initObj.MODPREEM.Vx_PReem[Indice_r].PlzInf_R = initObj.MODANUVI.V_PlAnu[i].CodPla;
                    initObj.MODPREEM.Vx_PReem[Indice_r].NumCon_R = initObj.MODANUVI.V_PlAnu[i].NumCon;
                    initObj.MODPREEM.Vx_PReem[Indice_r].FecCon_R = initObj.MODANUVI.V_PlAnu[i].FecCon;
                }

            }
        }

        //Cambia el estado de la planilla.-        
        public static short Fn_ActivaBDPlv(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            short _retValue = 0;
            short Reem = 0;
            short i = 0;
            short j = 0;


            Reem = (short)VB6Helpers.UBound(initObj.MODPREEM.Vx_PReem);
            List<string> parameters = new List<string>();//aca estoy

            if (Reem == -1 || initObj.MODPREEM.Vx_PReem[0].FecVen == null)
                return (short)(true ? -1 : 0);

            //Genera Comando.-
            for (i = 0; i <= (short)Reem; i++)
            {
                int indice = i;

                j = Mdl_Funciones_Varias.Cmd_Put_New(initObj.Mdl_Funciones_Varias, () =>
                {
                    return (short)unit.SceRepository.sce_plan_u07_MS(MODGSYB.dbcharSy(initObj.MODGCVD.VgCvd.codcct),
                        MODGSYB.dbcharSy(initObj.MODGCVD.VgCvd.codpro),
                        MODGSYB.dbcharSy(initObj.MODGCVD.VgCvd.codesp),
                        MODGSYB.dbcharSy(initObj.MODGCVD.VgCvd.codofi),
                        MODGSYB.dbcharSy(initObj.MODGCVD.VgCvd.codope),
                        MODGSYB.dbnumesy(initObj.MODPREEM.Vx_PReem[indice].NumPla),
                        MODGSYB.dbnumesy(5));
                });
            }

            _retValue = (short)(true ? -1 : 0);
            return _retValue;
            /*initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
            {
                Type = TipoMensaje.Informacion,
                Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                Title = "Activación de Registros"
            });
            return _retValue;*/
        }


        //Imprime Planillas de Reemplazo.-
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static void Pr_ImprRee(InitializationObject initObject, UnitOfWorkCext01 unit, short copias_ree)
        {
            T_MODPREEM MODPREEM = initObject.MODPREEM;
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_MODVPLE MODVPLE = initObject.MODVPLE;
            T_MODANUVI MODANUVI = initObject.MODANUVI;
            T_MODGFYS MODGFYS = initObject.MODGFYS;


            string mensaje = "";
            short i = 0;
            short k = 0;
            short xx = 0;
            short Tot_Ree = 0;
            short j = 0;
            string NroAcs1 = "";
            string NroAcs2 = "";

            PlanillaReemplazo pr = new PlanillaReemplazo();


            Tot_Ree = (short)VB6Helpers.UBound(MODPREEM.Vx_PReem);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            if (MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_AnuVisI)
            {
                //Reemplazo.-
                mensaje = "Impresión de Planillas de Reemplazo";
            }
            else if (MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_PlanSO)
            {
                //Sin Operación.-
                mensaje = "Impresión de Planillas sin Operación";
            }
            pr.Mensaje = mensaje;



            for (i = 0; i <= (short)Tot_Ree; i++)
            {
                for (j = 1; j <= (short)copias_ree; j++)
                {
                    if (MODPREEM.Vx_PReem[i].Estado == 1)
                    {

                        //Numero de Presentacion
                        //-----------------------------------
                        pr.Vx_PReem_NumPla = VB6Helpers.Format(MODPREEM.Vx_PReem[i].NumPla, "000000");
                        //Codigo (205000)
                        //-----------------------------------
                        pr.Vx_PReem_Codigo = MODPREEM.Vx_PReem[i].Codigo;
                        //Fecha de Venta
                        //-----------------------------------
                        pr.Vx_PReem_FecVen = String.IsNullOrEmpty(MODPREEM.Vx_PReem[i].FecVen) ? String.Empty : DateTime.Parse(MODPREEM.Vx_PReem[i].FecVen).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        //Plaza Banco Central y Codigo
                        //-----------------------------------
                        //Printxy 77 + xx%, 31 + k%, "SANTIAGO"

                        pr.Vx_PReem_NomPlz = MODPREEM.Vx_PReem[i].NomPlz;
                        pr.Vx_PReem_CodBch = VB6Helpers.Format(VB6Helpers.CStr(MODPREEM.Vx_PReem[i].CodBch), "00");
                        //Importador
                        //-----------------------------------
                        pr.Vx_PReem_NomImp = MODPREEM.Vx_PReem[i].NomImp;
                        pr.Vx_PReem_RutImp = BCH.Comex.Core.BL.XCFT.Modulos.MODGFYS.Rut_Formateado(MODPREEM.Vx_PReem[i].RutImp);
                        //Pais de Pago
                        //-----------------------------------
                        if (!string.IsNullOrEmpty(MODPREEM.Vx_PReem[i].NomPai))
                        {
                            string _tempVar1 = VB6Helpers.Mid(MODPREEM.Vx_PReem[i].NomPai, 1, 17);
                            pr.Vx_PReem_NomPai = BCH.Comex.Core.BL.XCFT.Modulos.MODGFYS.Escribe_Nombre(ref _tempVar1);
                            pr.Vx_PReem_CodPPa = VB6Helpers.Format(MODPREEM.Vx_PReem[i].CodPPa, "000");
                        }

                        //Moneda
                        //-----------------------------------
                        pr.Vx_PReem_NomMon = BCH.Comex.Core.BL.XCFT.Modulos.MODGFYS.Escribe_Nombre(ref MODPREEM.Vx_PReem[i].NomMon);
                        pr.Vx_PReem_CodMPa = VB6Helpers.Format(MODPREEM.Vx_PReem[i].CodMPa, "000");
                        //Informe de Importacion
                        //-----------------------------------
                        if (VB6Helpers.Format(VB6Helpers.CStr(MODPREEM.Vx_PReem[i].NumIdi), "000000") != "000000")
                        {
                            pr.Vx_PReem_NumIdi = VB6Helpers.Format(VB6Helpers.CStr(MODPREEM.Vx_PReem[i].NumIdi), "000000");
                            pr.Vx_PReem_FecIdi = String.IsNullOrEmpty(MODPREEM.Vx_PReem[i].FecIdi) ? String.Empty : DateTime.Parse(MODPREEM.Vx_PReem[i].FecIdi).ToString("dd/MM/yyyy");
                            pr.Vx_PReem_CodPlz = VB6Helpers.Format(VB6Helpers.CStr(MODPREEM.Vx_PReem[i].CodPlz), "00");
                        }

                        //Forma de Pago
                        //-----------------------------------
                        pr.Vx_PReem_CodPag = VB6Helpers.Format(MODPREEM.Vx_PReem[i].CodPag, "00");
                        //Conocimiento de Embarque
                        //-----------------------------------
                        pr.Vx_PReem_NumCon = MODPREEM.Vx_PReem[i].NumCon;
                        pr.Vx_PReem_FecCon = String.IsNullOrEmpty(MODPREEM.Vx_PReem[i].FecCon) ? String.Empty : DateTime.Parse(MODPREEM.Vx_PReem[i].FecCon).ToString("dd/MM/yyyy");
                        //Vencimiento de la operacion
                        //-----------------------------------
                        pr.Vx_PReem_FecVto = String.IsNullOrEmpty(MODPREEM.Vx_PReem[i].FecVto) ? String.Empty : DateTime.Parse(MODPREEM.Vx_PReem[i].FecVto).ToString("dd/MM/yyyy");
                        //Impresion del Detalle de los Montos Cubiertos en la Planilla
                        //-----------------------------------


                        pr.Vx_PReem_MtoFob = BCH.Comex.Core.BL.XCFT.Modulos.MODGFYS.PrintMonto(MODPREEM.Vx_PReem[i].MtoFob);
                        pr.Vx_PReem_MtoFle = BCH.Comex.Core.BL.XCFT.Modulos.MODGFYS.PrintMonto(MODPREEM.Vx_PReem[i].MtoFle);
                        pr.Vx_PReem_MtoSeg = BCH.Comex.Core.BL.XCFT.Modulos.MODGFYS.PrintMonto(MODPREEM.Vx_PReem[i].MtoSeg);
                        pr.Vx_PReem_MtoCif = BCH.Comex.Core.BL.XCFT.Modulos.MODGFYS.PrintMonto(MODPREEM.Vx_PReem[i].MtoCif);
                        pr.Vx_PReem_MtoInt = BCH.Comex.Core.BL.XCFT.Modulos.MODGFYS.PrintMonto(MODPREEM.Vx_PReem[i].MtoInt);
                        pr.Vx_PReem_GasBan = BCH.Comex.Core.BL.XCFT.Modulos.MODGFYS.PrintMonto(MODPREEM.Vx_PReem[i].GasBan);
                        pr.Vx_PReem_TotOri = BCH.Comex.Core.BL.XCFT.Modulos.MODGFYS.PrintMonto(MODPREEM.Vx_PReem[i].TotOri);
                        pr.Vx_PReem_CifDol = BCH.Comex.Core.BL.XCFT.Modulos.MODGFYS.PrintMonto(MODPREEM.Vx_PReem[i].CifDol);
                        pr.Vx_PReem_TotDol = BCH.Comex.Core.BL.XCFT.Modulos.MODGFYS.PrintMonto(MODPREEM.Vx_PReem[i].TotDol);

                        //-----------------------------------
                        //Tipo cambio y Paridad.-
                        //-----------------------------------
                        pr.Vx_PReem_TipCam = Format.FormatCurrency(MODPREEM.Vx_PReem[i].TipCam, "0.0000");

                        pr.Vx_PReem_ParPag = Format.FormatCurrency(MODPREEM.Vx_PReem[i].ParPag, "0.0000");
                        //Cuadro de Pagos.-
                        //-----------------------------------
                        if (MODPREEM.Vx_PReem[i].HayCua != 0)
                        {
                            if (MODPREEM.Vx_PReem[i].NumCua != 0)
                            {
                                pr.Vx_PReem_NumCua = MODPREEM.Vx_PReem[i].NumCua.ToString();
                            }
                            if (MODPREEM.Vx_PReem[i].numcuo != 0)
                            {
                                pr.Vx_PReem_numcuo = MODPREEM.Vx_PReem[i].numcuo.ToString();
                            }
                        }

                        //Acuerdos.-
                        //-----------------------------------
                        if (MODPREEM.Vx_PReem[i].HayAcu != 0)
                        {
                            pr.Vx_PReem_NumAcu = MODPREEM.Vx_PReem[i].NumAcu.ToString();

                            NroAcs1 = "    " + VB6Helpers.Trim(MODPREEM.Vx_PReem[i].Acuer1) + "    ";
                            NroAcs2 = "    " + VB6Helpers.Trim(MODPREEM.Vx_PReem[i].Acuer2) + "    ";
                            pr.Vx_PReem_Acuer1 = NroAcs1;
                            pr.Vx_PReem_Acuer2 = NroAcs2;
                        }

                        //Convenio Credito Reciproco.-
                        //----------------------------
                        if (!String.IsNullOrEmpty(MODPREEM.Vx_PReem[i].FecDeb))
                        {
                            pr.Vx_PReem_FecDeb = DateTime.Parse(MODPREEM.Vx_PReem[i].FecDeb).ToString("dd/MM/yyyy");
                        }

                        if (!String.IsNullOrEmpty(MODPREEM.Vx_PReem[i].DocChi))
                        {
                            pr.Vx_PReem_DocChi = MODPREEM.Vx_PReem[i].DocChi;
                        }

                        if (!String.IsNullOrEmpty(MODPREEM.Vx_PReem[i].DocExt))
                        {
                            pr.Vx_PReem_DocExt = MODPREEM.Vx_PReem[i].DocExt;
                        }

                        //Datos planilla reemplazada.-
                        //-----------------------------------
                        if (MODPREEM.Vx_PReem[i].HayRpl == -1)
                        {
                            pr.Vx_PReem_NumPln_R = VB6Helpers.Format(VB6Helpers.CStr(MODPREEM.Vx_PReem[i].NumPln_R), "000000");
                            pr.Vx_PReem_FecPln_R = DateTime.Parse(MODPREEM.Vx_PReem[i].FecPln_R).ToString("dd/MM/yyyy");
                            pr.Vx_PReem_CodPlz_R = VB6Helpers.Format(VB6Helpers.Str(MODPREEM.Vx_PReem[i].CodPlz_R), "00");
                            pr.Vx_PReem_CodEnt_R = VB6Helpers.Format(VB6Helpers.CStr(MODPREEM.Vx_PReem[i].CodEnt_R), "00");
                            pr.Vx_PReem_NumInf_R = VB6Helpers.Format(VB6Helpers.CStr(MODPREEM.Vx_PReem[i].NumInf_R), "000000");
                            pr.Vx_PReem_FecInf_R = DateTime.Parse(MODPREEM.Vx_PReem[i].FecInf_R).ToString("dd/MM/yyyy");
                            pr.Vx_PReem_PlzInf_R = VB6Helpers.Format(VB6Helpers.CStr(MODPREEM.Vx_PReem[i].PlzInf_R), "00");
                            pr.Vx_PReem_NumCon_R = VB6Helpers.Format(MODPREEM.Vx_PReem[i].NumCon_R, "000000");
                            pr.Vx_PReem_FecCon_R = DateTime.Parse(MODPREEM.Vx_PReem[i].FecCon_R).ToString("dd/MM/yyyy");
                        }

                        pr.Vx_PReem_ObsDec = MODPREEM.Vx_PReem[i].ObsDec;
                        pr.Vx_PReem_observ = MODPREEM.Vx_PReem[i].observ;
                        pr.Vx_PReem_ObsCob = MODPREEM.Vx_PReem[i].ObsCob;

                        //Detalle de Intereses
                        //--------------------
                        BCH.Comex.Core.BL.XCFT.Modulos.MODANUVI.Pr_ImpDetInt(pr, ref MODVPLE.IntPla, MODPREEM.Vx_PReem[i].NumPla);
                        //--------------------

                        string paramStr = "Impresion/Planillas/ImprimirPlanillaReemplazos?codigoCentroCosto={0}&codigoProducto={1}&codigoEspecialista={2}&codigoEmpresa={3}&codigoCobranza={4}&numeroPlanilla={5}&fechaVenta={6}";
                        string urlStr = string.Format(paramStr, MODGCVD.VgCvd.codcct, MODGCVD.VgCvd.codpro, MODGCVD.VgCvd.codesp, MODGCVD.VgCvd.codofi, MODGCVD.VgCvd.codope, pr.Vx_PReem_NumPla, DateTime.Parse(pr.Vx_PReem_FecVen).ToString("yyyy-MM-dd"));

                        initObject.DocumentosAImprimir.Add(new DataImpresion()
                        {
                            URL = urlStr,
                            //URL="FundTransfer/PlanillaReemplazos/"+initObject.PlanillasReemplazo.Count
                            NumeroOperacion = MODGCVD.VgCvd.OpeSin,
                            nroPresentacion = pr.Vx_PReem_NumPla,
                            fechaOp = DateTime.Parse(pr.Vx_PReem_FecVen),
                            tipoDoc = 6,
                            fileName = initObject.MODGCVD.VgCvd.OpeSin
                        });
                        initObject.PlanillasReemplazo.Add(pr);

                    }

                }

            }

            MODVPLE.IntPla = new T_IntPla[0];
            MODVPLE.IntAnu = new T_IntPla[0];
            MODPREEM.Vx_PReem = new Plan_Reemp[0];
            MODANUVI.V_PlAnu = new T_AnuVi[0];

            MODGFYS.RIdiFin = new R_Idi[0];
            initObject.MODCVDIMMM.RDecFin = new r_dec[0];
            MODGFYS.RIdiIni = new R_Idi[0];
            initObject.MODCVDIMMM.RDecIni = new r_dec[0];
        }


        //Graba las Planillas Visibles de Importacion para el reemplazo.-
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short Fn_SyPutPl(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            using (var tracer = new Tracer("Fn_SyPutPl: Graba las Planillas Visibles de Importacion para el reemplazo"))
            {
                T_MODPREEM MODPREEM = initObject.MODPREEM;
                T_MODGCVD MODGCVD = initObject.MODGCVD;
                T_Module1 Module1 = initObject.Module1;
                T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
                T_MODGUSR MODGUSR = initObject.MODGUSR;

                short _retValue = 0;
                short Tot_Reem = 0;
                short i = 0;
                string Que = "";
                short hay = 0;
                short m = 0;
                short n = 0;
                string R = "";

                Tot_Reem = (short)VB6Helpers.UBound(MODPREEM.Vx_PReem);
                // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
                // IGNORED: On Error GoTo 0
                try
                {
                    if (Tot_Reem == -1)
                        _retValue = (short)(true ? -1 : 0);

                    for (i = 0; i <= (short)Tot_Reem; i++)
                    {
                        List<string> parameters = new List<string>();

                        parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codcct));
                        parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codpro));
                        parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codesp));
                        parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codofi));
                        parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codope));
                        parameters.Add(MODGSYB.dbnumesy(MODPREEM.Vx_PReem[i].NumPla));
                        parameters.Add(MODGSYB.dbcharSy(MODPREEM.Vx_PReem[i].RutImp));
                        parameters.Add(MODGSYB.dbcharSy(Module1.PartysOpe[T_MODGCVD.ICli].LlaveArchivo));
                        parameters.Add(MODGSYB.dbcharSy(MODPREEM.Vx_PReem[i].NomImp));
                        parameters.Add(MODGSYB.dbdatesy(MODPREEM.Vx_PReem[i].FecVen));
                        parameters.Add(MODGSYB.dbcharSy(MODPREEM.Vx_PReem[i].numdec));
                        parameters.Add(MODGSYB.dbdatesy(MODPREEM.Vx_PReem[i].FecDec));
                        parameters.Add(MODGSYB.dbcharSy(MODPREEM.Vx_PReem[i].NumCon));
                        parameters.Add(MODGSYB.dbdatesy(MODPREEM.Vx_PReem[i].FecCon));
                        parameters.Add(MODGSYB.dbcharSy(MODPREEM.Vx_PReem[i].Codigo));
                        parameters.Add(MODGSYB.dbnumesy(MODPREEM.Vx_PReem[i].CodBch));
                        parameters.Add(MODGSYB.dbnumesy(MODPREEM.Vx_PReem[i].CodPlz));
                        parameters.Add(MODGSYB.dbcharSy(MODPREEM.Vx_PReem[i].NomPlz));
                        parameters.Add(MODGSYB.dbnumesy(MODPREEM.Vx_PReem[i].CodPag));
                        parameters.Add(MODGSYB.dbnumesy(MODPREEM.Vx_PReem[i].CodPPa));
                        m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VPai(initObject, unit, MODPREEM.Vx_PReem[i].CodPPa);
                        parameters.Add(MODGSYB.dbcharSy(MODGTAB0.VPai[m].Pai_PaiNom));
                        parameters.Add(MODGSYB.dbnumesy(MODPREEM.Vx_PReem[i].CodMon));
                        n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMndBC(MODGTAB0, unit, MODPREEM.Vx_PReem[i].CodMPa);
                        parameters.Add(MODGSYB.dbcharSy(MODGTAB0.VMnd[n].Mnd_MndNom));
                        parameters.Add(MODGSYB.dbPardSy(MODPREEM.Vx_PReem[i].ParPag));
                        parameters.Add(MODGSYB.dbmontoSy(MODPREEM.Vx_PReem[i].TipCam));
                        parameters.Add(MODGSYB.dbmontoSy(0));
                        parameters.Add(MODGSYB.dbmontoSy(0));
                        parameters.Add(MODGSYB.dbmontoSy(MODPREEM.Vx_PReem[i].MtoFob));
                        parameters.Add(MODGSYB.dbmontoSy(MODPREEM.Vx_PReem[i].MtoFle));
                        parameters.Add(MODGSYB.dbmontoSy(MODPREEM.Vx_PReem[i].MtoSeg));
                        parameters.Add(MODGSYB.dbmontoSy(MODPREEM.Vx_PReem[i].MtoCif));
                        parameters.Add(MODGSYB.dbmontoSy(MODPREEM.Vx_PReem[i].MtoInt));
                        parameters.Add(MODGSYB.dbmontoSy(MODPREEM.Vx_PReem[i].GasBan));
                        parameters.Add(MODGSYB.dbmontoSy(MODPREEM.Vx_PReem[i].TotOri));
                        parameters.Add(MODGSYB.dbmontoSy(MODPREEM.Vx_PReem[i].CifDol));
                        parameters.Add(MODGSYB.dbmontoSy(MODPREEM.Vx_PReem[i].TotDol));
                        parameters.Add(MODGSYB.dbdatesy(MODPREEM.Vx_PReem[i].FecVto));
                        parameters.Add(MODGSYB.dbnumesy(MODPREEM.Vx_PReem[i].HayCua));
                        parameters.Add(MODGSYB.dbnumesy(MODPREEM.Vx_PReem[i].NumCua));
                        parameters.Add(MODGSYB.dbnumesy(MODPREEM.Vx_PReem[i].numcuo));
                        if (MODPREEM.Vx_PReem[i].NumAcu > 0)
                        {
                            hay = 1;
                        }
                        else
                        {
                            hay = 0;
                        }

                        parameters.Add(MODGSYB.dbnumesy(hay));
                        parameters.Add(MODGSYB.dbnumesy(MODPREEM.Vx_PReem[i].NumAcu));
                        parameters.Add(MODGSYB.dbcharSy(MODPREEM.Vx_PReem[i].Acuer1));
                        parameters.Add(MODGSYB.dbcharSy(MODPREEM.Vx_PReem[i].Acuer2));
                        parameters.Add(MODGSYB.dbcharSy(""));  //hay acuerdo 3
                        parameters.Add(MODGSYB.dbnumesy(0));  //hay anul.
                        parameters.Add(MODGSYB.dbnumesy(MODPREEM.Vx_PReem[i].IndAnu));  //2 Reemplazo /0 Si no
                        parameters.Add(MODGSYB.dbnumesy(0));  //numreg
                        parameters.Add(MODGSYB.dbdatesy("1900-01-01"));
                        parameters.Add(MODGSYB.dbdatesy("1900-01-01"));
                        parameters.Add(MODGSYB.dbnumesy(0));
                        parameters.Add(MODGSYB.dbnumesy(0));
                        parameters.Add(MODGSYB.dbdatesy(MODPREEM.Vx_PReem[i].FecDeb));
                        parameters.Add(MODGSYB.dbcharSy(MODPREEM.Vx_PReem[i].DocChi));
                        parameters.Add(MODGSYB.dbcharSy(MODPREEM.Vx_PReem[i].DocExt));
                        parameters.Add(MODGSYB.dbnumesy(9));  //Estado
                        parameters.Add(MODGSYB.dbcharSy(MODPREEM.Vx_PReem[i].observ));
                        parameters.Add(MODGSYB.dbcharSy(MODPREEM.Vx_PReem[i].ObsDec));
                        parameters.Add(MODGSYB.dbcharSy(""));
                        parameters.Add(MODGSYB.dbcharSy(MODPREEM.Vx_PReem[i].ObsCob));
                        parameters.Add(MODGSYB.dbcharSy(""));
                        parameters.Add(MODGSYB.dbcharSy(MODGUSR.UsrEsp.CentroCosto));
                        parameters.Add(MODGSYB.dbcharSy(MODGUSR.UsrEsp.Especialista));
                        parameters.Add(MODGSYB.dbnumesy(MODPREEM.Vx_PReem[i].NumPln_R));
                        parameters.Add(MODGSYB.dbdatesy(MODPREEM.Vx_PReem[i].FecPln_R));
                        parameters.Add(MODGSYB.dbnumesy(MODPREEM.Vx_PReem[i].CodPlz_R));
                        parameters.Add(MODGSYB.dbnumesy(MODPREEM.Vx_PReem[i].CodEnt_R));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Format(VB6Helpers.CStr(MODPREEM.Vx_PReem[i].NumInf_R), "000000")));
                        parameters.Add(MODGSYB.dbdatesy(MODPREEM.Vx_PReem[i].FecInf_R));
                        parameters.Add(MODGSYB.dbnumesy(MODPREEM.Vx_PReem[i].PlzInf_R));
                        parameters.Add(MODGSYB.dbcharSy(MODPREEM.Vx_PReem[i].NumCon_R));
                        parameters.Add(MODGSYB.dbdatesy(MODPREEM.Vx_PReem[i].FecCon_R));
                        parameters.Add(MODGSYB.dbnumesy(Module1.PartysOpe[T_MODGCVD.ICli].IndNombre));
                        parameters.Add(MODGSYB.dbnumesy(Module1.PartysOpe[T_MODGCVD.ICli].IndDireccion));
                        parameters.Add(MODGSYB.dblogisy(MODPREEM.Vx_PReem[i].HayRpl));  //Hay reemplazo.-
                        parameters.Add(MODGSYB.dbnumesy(20));  //Tipo Planilla
                        parameters.Add(MODGSYB.dblogisy(MODPREEM.Vx_PReem[i].ZonFra));

                        //-------------------------------------------
                        //Se ejecuta el Procedimiento Almacenado.-
                        int res = unit.SceRepository.EjecutarSP<int>("sce_plan_w07", parameters.ToArray()).First();
                        if (res != 0)
                        {
                            throw new Exception();
                        }
                        else
                        {
                            _retValue = (short)(true ? -1 : 0);
                        }
                    }
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta", e);

                    initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error al tratar de grabar la Planilla Visible de Importación (Sce_Plan)."
                    });
                    _retValue = 0;
                }
                return _retValue;
            }
        }

        //Graba los intereses de planilla visible importacion (reemplazo).-
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short Fn_SyPutIn(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            using (var tracer = new Tracer())
            {
                T_MODVPLE MODVPLE = initObject.MODVPLE;
                T_MODPREEM MODPREEM = initObject.MODPREEM;
                T_MODGCVD MODGCVD = initObject.MODGCVD;

                short _retValue = 0;
                short Fin_Int = 0;
                short Fin_Pln = 0;
                string Que = "";
                string tip = "";
                short ind_int = 0;
                short ind_pln = 0;
                string Fecha = "";
                string R = "";

                Fin_Int = (short)VB6Helpers.UBound(MODVPLE.IntPla);
                Fin_Pln = (short)VB6Helpers.UBound(MODPREEM.Vx_PReem);
                // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
                // IGNORED: On Error GoTo 0
                try
                {
                    for (ind_int = 0; ind_int <= (short)Fin_Int; ind_int++)
                    {

                        if (MODVPLE.IntPla[ind_int].FlgEli == 0)
                        {
                            for (ind_pln = 0; ind_pln <= (short)Fin_Pln; ind_pln++)
                            {
                                if (VB6Helpers.Format(VB6Helpers.CStr(MODPREEM.Vx_PReem[ind_pln].NumPla), "000000") == VB6Helpers.Format(VB6Helpers.CStr(MODVPLE.IntPla[ind_int].NroPln), "000000"))
                                {
                                    Fecha = MODPREEM.Vx_PReem[ind_pln].FecVen;
                                }

                            }

                            List<string> parameters = new List<string>();
                            parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codcct));
                            parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codpro));
                            parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codesp));
                            parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codofi));
                            parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codope));
                            parameters.Add(MODGSYB.dbnumesy(MODVPLE.IntPla[ind_int].NroPln));
                            parameters.Add(MODGSYB.dbdatesy(Fecha));
                            parameters.Add(MODGSYB.dbnumesy(ind_int));
                            parameters.Add(MODGSYB.dbnumesy(MODVPLE.IntPla[ind_int].Concep));
                            short _switchVar1 = MODVPLE.IntPla[ind_int].TipInt;
                            if (_switchVar1 == 1)
                            {
                                tip = "PR";
                            }
                            else if (_switchVar1 == 2)
                            {
                                tip = "AC";
                            }
                            else if (_switchVar1 == 3)
                            {
                                tip = "IC";
                            }
                            else if (_switchVar1 == 4)
                            {
                                tip = "BA";
                            }

                            parameters.Add(MODGSYB.dbcharSy(tip));
                            parameters.Add(MODGSYB.dbmontoSy(MODVPLE.IntPla[ind_int].MtoInt));
                            parameters.Add(MODGSYB.dbmontoSy(MODVPLE.IntPla[ind_int].CapBas));
                            parameters.Add(MODGSYB.dbnumesy(MODVPLE.IntPla[ind_int].CodBas));
                            parameters.Add(MODGSYB.dbtasaSy(MODVPLE.IntPla[ind_int].TasInt));
                            parameters.Add(MODGSYB.dbdatesy(MODVPLE.IntPla[ind_int].FecIni));
                            parameters.Add(MODGSYB.dbdatesy(MODVPLE.IntPla[ind_int].FecFin));
                            parameters.Add(MODGSYB.dbnumesy(MODVPLE.IntPla[ind_int].NumDia));
                            //----------------------------------------------------

                            //Se ejecuta el Procedimiento Almacenado.
                            int res = unit.SceRepository.EjecutarSP<int>("sce_inpl_w01", parameters.ToArray()).First();
                            if (res != 0)
                            {
                                throw new Exception();
                            }

                        }
                    }
                    _retValue = -1;
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta", e);

                    initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Se ha producido un error de Comunicación al tratar de grabar intereses Planilla Visibles de Importación (Sce_inpl).",
                        Type = TipoMensaje.Error
                    });
                    _retValue = 0;
                }
                return _retValue;
            }
        }

        public static short SyGet_Fdp(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short n = 0;
            short i = 0;
            //Verifica que ya se hayan leído las Formas de Pago.-

            n = (short)VB6Helpers.UBound(initObj.MODPREEM.Vx_Fdp);

            if (n > 0)
            {
                return -1;
            }

            List<V_Fdp> result = new List<V_Fdp>();
            uow.SceRepository.ReadQuerySP((reader) =>
            {
                while (reader.Read())
                {
                    result.Add(new V_Fdp
                    {
                        codfdp = Convert.ToInt16(reader.GetValue(0)),
                        NomFdp = (string)reader.GetValue(1),
                        ProFdp = (string)reader.GetValue(2)
                    });
                }
            }, "sce_fdp_s01");
            initObj.MODPREEM.Vx_Fdp = result.ToArray();
            return -1;
        }


        //Vemos si la Declaracion y el Idi esta en alguna de las planillas
        //seleccionadas en la Anulacion para actualizar los Montos.
        public static void Pr_VeEnPlan(InitializationObject initObj, string numdec, string FecDec, short CodPag)
        {
            short largo_planu = 0;
            short i = 0;

            largo_planu = (short)VB6Helpers.UBound(initObj.MODANUVI.V_PlAnu);

            for (i = 1; i <= (short)largo_planu; i++)
            {

                if (initObj.MODANUVI.V_PlAnu[i].numdec == numdec && DateTime.Parse(initObj.MODANUVI.V_PlAnu[i].FecDec).ToString("dd/MM/yyyy") == DateTime.Parse(FecDec).ToString("dd/MM/yyyy"))
                {
                    initObj.MODPREEM.VxIdiDec.DisFob_M += initObj.MODANUVI.V_PlAnu[i].MtoFob;
                    initObj.MODPREEM.VxIdiDec.RelFob_M -= initObj.MODANUVI.V_PlAnu[i].MtoFob;
                    initObj.MODPREEM.VxIdiDec.CubFob_M -= initObj.MODANUVI.V_PlAnu[i].MtoFob;

                    initObj.MODPREEM.VxIdiDec.DisFle_M += initObj.MODANUVI.V_PlAnu[i].MtoFle;
                    initObj.MODPREEM.VxIdiDec.RelFle_M -= initObj.MODANUVI.V_PlAnu[i].MtoFle;
                    initObj.MODPREEM.VxIdiDec.CubFle_M -= initObj.MODANUVI.V_PlAnu[i].MtoFle;

                    initObj.MODPREEM.VxIdiDec.DisSeg_M += initObj.MODANUVI.V_PlAnu[i].MtoSeg;
                    initObj.MODPREEM.VxIdiDec.RelSeg_M -= initObj.MODANUVI.V_PlAnu[i].MtoSeg;
                    initObj.MODPREEM.VxIdiDec.CubSeg_M -= initObj.MODANUVI.V_PlAnu[i].MtoSeg;

                    initObj.MODPREEM.VxIdiDec.DisCif_M += initObj.MODANUVI.V_PlAnu[i].MtoCif;
                    initObj.MODPREEM.VxIdiDec.RelCif_M -= initObj.MODANUVI.V_PlAnu[i].MtoCif;
                    initObj.MODPREEM.VxIdiDec.CubCif_M -= initObj.MODANUVI.V_PlAnu[i].MtoCif;
                }

            }

        }


        //Lee en la estructura de planillas selecionadas en la pantalla de anulacion
        //y ve si corresponden los antecedentes de planilla reemplazada
        //numero de planilla y fecha.
        public static short Fn_LeePlAnu(InitializationObject initObj, int NumPl, string FecPl)
        {
            short _retValue = 0;
            short largo_anuvi = 0;
            short i = 0;

            largo_anuvi = (short)VB6Helpers.UBound(initObj.MODANUVI.V_PlAnu);

            _retValue = (short)(false ? -1 : 0);
            for (i = 1; i <= (short)largo_anuvi; i++)
            {
                if (VB6Helpers.Format(VB6Helpers.CStr(initObj.MODANUVI.V_PlAnu[i].NumPre), "000000") == VB6Helpers.Format(VB6Helpers.CStr(NumPl), "000000") && DateTime.Parse(initObj.MODANUVI.V_PlAnu[i].FecVen).ToString("yyyyMMdd") == DateTime.Parse(FecPl).ToString("yyyyMMdd"))
                {
                    _retValue = (short)(true ? -1 : 0);
                    break;
                }

            }

            return _retValue;
        }

        //Ve si hay Vias para Planillas Sin Operacion.-
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short Fn_HayViasSO(InitializationObject initObj)
        {
            short _retValue = 0;
            short Tot_Reem = 0;
            short i = 0;
            VB6Helpers.ClearError();
            Tot_Reem = (short)VB6Helpers.UBound(initObj.MODPREEM.Vx_PReem);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            _retValue = (short)(false ? -1 : 0);

            //Verifica Existencia de Planillas Sin Operacion (c/contab.).-
            //Si hay planillas de transferencia no se contabiliza.-
            //Se cambio el valor de la variable a 0 ya que no entraba al ciclo del for porque el valor de la variable i estaba seteado en 1.
            for (i = 0; i <= (short)Tot_Reem; i++)
            {
                if (initObj.MODPREEM.Vx_PReem[i].Estado != 9 && initObj.MODPREEM.Vx_PReem[i].TotOri > 0)
                {
                    _retValue = (short)(true ? -1 : 0);
                }

            }

            return _retValue;
        }


        //Calcula el Monto Total de las planillas de reemplazo.-
        public static double Fn_TotOriAnu(InitializationObject initObj)
        {
            double Valor = 0D;

            if (initObj.MODANUVI.Vx_AnuReem.ConvAnu - initObj.MODANUVI.Vx_AnuReem.ConvRee > 0)
            {
                Valor = initObj.MODANUVI.Vx_AnuReem.ConvAnu - initObj.MODANUVI.Vx_AnuReem.ConvRee;
            }

            if (initObj.MODANUVI.Vx_AnuReem.CambRee - initObj.MODANUVI.Vx_AnuReem.CambAnu > 0)
            {
                Valor = initObj.MODANUVI.Vx_AnuReem.CambRee - initObj.MODANUVI.Vx_AnuReem.CambAnu;
            }

            return Valor;
        }



        //Total de Origenes para Planillas sin Operacion.-
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static double Fn_TotOriSO(InitializationObject initObj)
        {
            double Valor = 0D;
            short X = 0;
            short i = 0;

            VB6Helpers.ClearError();
            X = (short)VB6Helpers.UBound(initObj.MODPREEM.Vx_PReem);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            //Si hay planillas transferencia no se contabiliza.-
            for (i = 1; i <= (short)X; i++)
            {
                if (initObj.MODPREEM.Vx_PReem[i].Estado != 9)
                {
                    Valor += initObj.MODPREEM.Vx_PReem[i].TotOri;
                }

            }

            return Valor;
        }

        internal static bool Fn_HayViasAnu(InitializationObject initObj)
        {
            // Indica si hay planillas que tienen vias.-
            bool HayVia = false;
            HayVia = false;

            if (initObj.MODANUVI.Vx_AnuReem.ConvRee - initObj.MODANUVI.Vx_AnuReem.ConvAnu > 0)
            {
                HayVia = true;
            }

            if (initObj.MODANUVI.Vx_AnuReem.CambAnu - initObj.MODANUVI.Vx_AnuReem.CambRee > 0)
            {
                HayVia = true;
            }

            return HayVia;
        }
    }
}
