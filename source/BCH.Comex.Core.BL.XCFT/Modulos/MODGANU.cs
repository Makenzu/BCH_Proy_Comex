using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;
using System.Linq;


namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODGANU
    {
        public static T_MODGANU GetMODGANU()
        {
            return new T_MODGANU();
        }

        public static short Rebaja_xAnu(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            T_MODXPLN0 MODXPLN0 = initObject.MODXPLN0;
            T_MODGANU MODGANU = initObject.MODGANU;
            T_Module1 Module1 = initObject.Module1;
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;

            dynamic TcpSeg = null;
            short i = 0;
            short mnd = 0;
            short m = 0;
            short n = 0;
            short x = 0;
            string p = "";

            const string TcpFle = "25111901K";
            TcpSeg = "251305014";

            MODXPLN0.VxDecP = new T_xDecP[0];
            for (i = 0; i <= (short)VB6Helpers.UBound(MODGANU.VAnuPl); i++)
            {
                if (!string.IsNullOrEmpty(MODGANU.VAnuPl[i].numdec) && MODGANU.VAnuPl[i].MtoAnu > 0)
                {
                    mnd = MODGANU.VAnuPl[i].CodMnd;
                    m = (short)(m + 1);
                    n = BCH.Comex.Core.BL.XCFT.Modulos.MODXPLN0.Get_xDec(initObject, MODGANU.VAnuPl[i].numdec, MODGANU.VAnuPl[i].FecDec,
                        MODGANU.VAnuPl[i].CodAdn);
                    if (n < 0)
                    {
                        x = Mdl_Funciones.SyGet_xDec(Mdl_Funciones_Varias, unit, MODGANU.VAnuPl[i].numdec, MODGANU.VAnuPl[i].FecDec,
                            MODGANU.VAnuPl[i].CodAdn);
                        if (x != 0)
                        {
                            n = BCH.Comex.Core.BL.XCFT.Modulos.MODXPLN0.Put_xDec(initObject, unit, MODGANU.VAnuPl[i].CodMnd, -1);
                        }
                        else
                        {
                            initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Hay Problemas para leer la Declaración de Exportación",
                                Title = "FundTransfer"
                            });

                            return 0;
                        }

                    }

                    if (n > 0)
                    {
                        p = MODGPYF0.Componer(Module1.PartysOpe[0].LlaveArchivo, "~", "");
                        string _switchVar1 = MODGANU.VAnuPl[i].VisInv;
                        if (_switchVar1 == "INV")
                        {
                            //Rebaja Saldo Dec. Exportador 1.
                            if (p == MODXPLN0.VxDecP[n].PrtExp1 && Module1.PartysOpe[0].IndNombre == MODXPLN0.VxDecP[n].IndNom1)
                            {
                                string _switchVar2 = MODGANU.VAnuPl[i].codcom;
                                if (_switchVar2 == TcpFle)
                                {
                                    MODXPLN0.VxDecP[n].ValFle1c = MODXPLN0.VxDecP[n].ValFle1c + -1 * Format.StringToDouble(VB6Helpers.Format(VB6Helpers.CStr(MODXPLN0.VxDecP[n].ValFle1c) + VB6Helpers.CStr(MODGANU.VAnuPl[i].MtoAnu), "0.00"));
                                }
                                else if (_switchVar2 == TcpSeg)
                                {
                                    MODXPLN0.VxDecP[n].ValSeg1c = MODXPLN0.VxDecP[n].ValSeg1c + -1 * Format.StringToDouble(VB6Helpers.Format(VB6Helpers.CStr(MODXPLN0.VxDecP[n].ValSeg1c) + VB6Helpers.CStr(MODGANU.VAnuPl[i].MtoAnu), "0.00"));
                                }

                            }

                            //Rebaja Saldo Dec. Exportador 2.
                            if (p == MODXPLN0.VxDecP[n].PrtExp2 && Module1.PartysOpe[MODXPLN0.VxDatP.IndPrt].IndNombre == MODXPLN0.VxDecP[n].IndNom2)
                            {
                                string _switchVar3 = MODGANU.VAnuPl[i].codcom;
                                if (_switchVar3 == TcpFle)
                                {
                                    MODXPLN0.VxDecP[n].ValFle2c = MODXPLN0.VxDecP[n].ValFle2c + -1 * Format.StringToDouble(VB6Helpers.Format(VB6Helpers.CStr(MODXPLN0.VxDecP[n].ValFle2c) + VB6Helpers.CStr(MODGANU.VAnuPl[i].MtoAnu), "0.00"));
                                }
                                else if (_switchVar3 == TcpSeg)
                                {
                                    MODXPLN0.VxDecP[n].ValSeg2c = MODXPLN0.VxDecP[n].ValSeg2c + -1 * Format.StringToDouble(VB6Helpers.Format(VB6Helpers.CStr(MODXPLN0.VxDecP[n].ValSeg2c) + VB6Helpers.CStr(MODGANU.VAnuPl[i].MtoAnu), "0.00"));
                                }

                            }

                        }
                        else if (_switchVar1 == "VIS")
                        {
                            if (p == MODXPLN0.VxDecP[n].PrtExp1 && Module1.PartysOpe[0].IndNombre == MODXPLN0.VxDecP[n].IndNom1)
                            {
                                MODXPLN0.VxDecP[n].ValRet1c = MODXPLN0.VxDecP[n].ValRet1c + Format.StringToDouble(VB6Helpers.Format(VB6Helpers.CStr(-1 * MODGANU.VAnuPl[i].ValCla), "0.00"));
                                MODXPLN0.VxDecP[n].ValCom1c = MODXPLN0.VxDecP[n].ValCom1c + Format.StringToDouble(VB6Helpers.Format(VB6Helpers.CStr(-1 * MODGANU.VAnuPl[i].ValCom), "0.00"));
                                MODXPLN0.VxDecP[n].ValGas1c = MODXPLN0.VxDecP[n].ValGas1c + Format.StringToDouble(VB6Helpers.Format(VB6Helpers.CStr(-1 * MODGANU.VAnuPl[i].OtrGas), "0.00"));
                                MODXPLN0.VxDecP[n].ValLiq1c = MODXPLN0.VxDecP[n].ValLiq1c + Format.StringToDouble(VB6Helpers.Format(VB6Helpers.CStr(-1 * MODGANU.VAnuPl[i].ValLiq), "0.00"));
                            }

                            if (p == MODXPLN0.VxDecP[n].PrtExp2 && Module1.PartysOpe[0].IndNombre == MODXPLN0.VxDecP[n].IndNom2)
                            {
                                MODXPLN0.VxDecP[n].ValRet2c = MODXPLN0.VxDecP[n].ValRet2c + Format.StringToDouble(VB6Helpers.Format(VB6Helpers.CStr(-1 * MODGANU.VAnuPl[i].ValCla), "0.00"));
                                MODXPLN0.VxDecP[n].ValCom2c = MODXPLN0.VxDecP[n].ValCom2c + Format.StringToDouble(VB6Helpers.Format(VB6Helpers.CStr(-1 * MODGANU.VAnuPl[i].ValCom), "0.00"));
                                MODXPLN0.VxDecP[n].ValGas2c = MODXPLN0.VxDecP[n].ValGas2c + Format.StringToDouble(VB6Helpers.Format(VB6Helpers.CStr(-1 * MODGANU.VAnuPl[i].OtrGas), "0.00"));
                                MODXPLN0.VxDecP[n].ValLiq2c = MODXPLN0.VxDecP[n].ValLiq2c + Format.StringToDouble(VB6Helpers.Format(VB6Helpers.CStr(-1 * MODGANU.VAnuPl[i].ValLiq), "0.00"));
                            }

                        }

                    }

                }

            }

            //No hay devolucion de Saldos de la Declaración.-
            if (m == 0)
            {
                return (short)(true ? -1 : 0);
            }

            //Se convierten los montos a la moneda original.-
            if (~BCH.Comex.Core.BL.XCFT.Modulos.MODXPLN0.RPut_xDec(initObject, unit, mnd) != 0)
            {
                return 0;
            }

            return (short)(true ? -1 : 0);
        }

        //Lee una Operación de Compra-Venta.
        //Retorno =  True    => Exitoso.
        //           False   => Erróneo.
        public static short SyGet_CVD(string NumOpe, InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            short _retValue = 0;

            try
            {
                initObj.MODGCVD.VgCVDo = new T_gCVD();// initObj.MODGCVD.VgCVDVacia;

                var Result = unit.SceRepository.sce_cvd_s06_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)),
                    MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)),
                    MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)),
                    MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)),
                    MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)));


                if (Result != null)
                {
                    string cod_pro = MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2));
                    switch (Convert.ToInt16(cod_pro))
                    {
                        case 3:
                            initObj.MODGCVD.VgCVDo.codcct = Result.codpro_03.codcct;
                            initObj.MODGCVD.VgCVDo.codpro = Result.codpro_03.codpro;
                            initObj.MODGCVD.VgCVDo.codesp = Result.codpro_03.codesp;
                            initObj.MODGCVD.VgCVDo.codofi = Result.codpro_03.codofi;
                            initObj.MODGCVD.VgCVDo.codope = Result.codpro_03.codope;
                            initObj.MODGCVD.VgCVDo.Fecing = Result.codpro_03.fecing.ToString();
                            initObj.MODGCVD.VgCVDo.TipCVD = (short)Result.codpro_03.TipCVD;
                            initObj.MODGCVD.VgCVDo.PrtCli = Result.codpro_03.prtcli;
                            initObj.MODGCVD.VgCVDo.IndNomC = (short)(Result.codpro_03.indnomc.HasValue ? Result.codpro_03.indnomc : 0);
                            initObj.MODGCVD.VgCVDo.IndDirC = (short)(Result.codpro_03.inddirc.HasValue ? Result.codpro_03.inddirc : 0);
                            break;

                        case 5:
                            initObj.MODGCVD.VgCVDo.codcct = Result.codpro_05.codcct;
                            initObj.MODGCVD.VgCVDo.codpro = Result.codpro_05.codpro;
                            initObj.MODGCVD.VgCVDo.codesp = Result.codpro_05.codesp;
                            initObj.MODGCVD.VgCVDo.codofi = Result.codpro_05.codofi;
                            initObj.MODGCVD.VgCVDo.codope = Result.codpro_05.codope;
                            initObj.MODGCVD.VgCVDo.Fecing = Result.codpro_05.fecing.ToString();
                            initObj.MODGCVD.VgCVDo.TipCVD = (short)Result.codpro_05.TipCVD;
                            initObj.MODGCVD.VgCVDo.PrtCli = Result.codpro_05.prtcli;
                            initObj.MODGCVD.VgCVDo.IndNomC = (short)(Result.codpro_05.indnomc.HasValue ? Result.codpro_05.indnomc : 0);
                            initObj.MODGCVD.VgCVDo.IndDirC = (short)(Result.codpro_05.inddirc.HasValue ? Result.codpro_05.inddirc : 0);
                            break;

                        case 6:
                            initObj.MODGCVD.VgCVDo.codcct = Result.codpro_06.codcct;
                            initObj.MODGCVD.VgCVDo.codpro = Result.codpro_06.codpro;
                            initObj.MODGCVD.VgCVDo.codesp = Result.codpro_06.codesp;
                            initObj.MODGCVD.VgCVDo.codofi = Result.codpro_06.codofi;
                            initObj.MODGCVD.VgCVDo.codope = Result.codpro_06.codope;
                            initObj.MODGCVD.VgCVDo.Fecing = Result.codpro_06.fecing.ToString();
                            initObj.MODGCVD.VgCVDo.TipCVD = (short)Result.codpro_06.TipCVD;
                            initObj.MODGCVD.VgCVDo.PrtCli = Result.codpro_06.prtcli;
                            initObj.MODGCVD.VgCVDo.IndNomC = (short)(Result.codpro_06.indnomc.HasValue ? Result.codpro_06.indnomc : 0);
                            initObj.MODGCVD.VgCVDo.IndDirC = (short)(Result.codpro_06.inddirc.HasValue ? Result.codpro_06.inddirc : 0);
                            break;

                        case 7:
                            initObj.MODGCVD.VgCVDo.codcct = Result.codpro_07.codcct;
                            initObj.MODGCVD.VgCVDo.codpro = Result.codpro_07.codpro;
                            initObj.MODGCVD.VgCVDo.codesp = Result.codpro_07.codesp;
                            initObj.MODGCVD.VgCVDo.codofi = Result.codpro_07.codofi;
                            initObj.MODGCVD.VgCVDo.codope = Result.codpro_07.codope;
                            initObj.MODGCVD.VgCVDo.Fecing = Result.codpro_07.fecing.ToString();
                            initObj.MODGCVD.VgCVDo.TipCVD = (short)Result.codpro_07.TipCVD;
                            initObj.MODGCVD.VgCVDo.PrtCli = Result.codpro_07.prtcli;
                            initObj.MODGCVD.VgCVDo.IndNomC = (short)(Result.codpro_07.indnomc.HasValue ? Result.codpro_07.indnomc : 0);
                            initObj.MODGCVD.VgCVDo.IndDirC = (short)(Result.codpro_07.inddirc.HasValue ? Result.codpro_07.inddirc : 0);
                            break;

                        case 8:
                            initObj.MODGCVD.VgCVDo.codcct = Result.codpro_08.codcct;
                            initObj.MODGCVD.VgCVDo.codpro = Result.codpro_08.codpro;
                            initObj.MODGCVD.VgCVDo.codesp = Result.codpro_08.codesp;
                            initObj.MODGCVD.VgCVDo.codofi = Result.codpro_08.codofi;
                            initObj.MODGCVD.VgCVDo.codope = Result.codpro_08.codope;
                            initObj.MODGCVD.VgCVDo.Fecing = Result.codpro_08.fecing.ToString();
                            initObj.MODGCVD.VgCVDo.TipCVD = (short)Result.codpro_08.TipCVD;
                            initObj.MODGCVD.VgCVDo.PrtCli = Result.codpro_08.prtcli;
                            initObj.MODGCVD.VgCVDo.IndNomC = (short)(Result.codpro_08.indnomc.HasValue ? Result.codpro_08.indnomc : 0);
                            initObj.MODGCVD.VgCVDo.IndDirC = (short)(Result.codpro_08.inddirc.HasValue ? Result.codpro_08.inddirc : 0);
                            break;

                        case 9:
                            initObj.MODGCVD.VgCVDo.codcct = Result.codpro_09.codcct;
                            initObj.MODGCVD.VgCVDo.codpro = Result.codpro_09.codpro;
                            initObj.MODGCVD.VgCVDo.codesp = Result.codpro_09.codesp;
                            initObj.MODGCVD.VgCVDo.codofi = Result.codpro_09.codofi;
                            initObj.MODGCVD.VgCVDo.codope = Result.codpro_09.codope;
                            initObj.MODGCVD.VgCVDo.Fecing = Result.codpro_09.fecing.ToString();
                            initObj.MODGCVD.VgCVDo.TipCVD = (short)Result.codpro_09.campo6;
                            initObj.MODGCVD.VgCVDo.PrtCli = Result.codpro_09.prtexp;
                            initObj.MODGCVD.VgCVDo.IndNomC = (short)Result.codpro_09.indexpn;
                            initObj.MODGCVD.VgCVDo.IndDirC = (short)Result.codpro_09.indexpd;
                            break;

                        case 17:
                            initObj.MODGCVD.VgCVDo.codcct = Result.codpro_17.codcct;
                            initObj.MODGCVD.VgCVDo.codpro = Result.codpro_17.codpro;
                            initObj.MODGCVD.VgCVDo.codesp = Result.codpro_17.codesp;
                            initObj.MODGCVD.VgCVDo.codofi = Result.codpro_17.codofi;
                            initObj.MODGCVD.VgCVDo.codope = Result.codpro_17.codope;
                            initObj.MODGCVD.VgCVDo.Fecing = Result.codpro_17.fecing.ToString();
                            initObj.MODGCVD.VgCVDo.TipCVD = (short)Result.codpro_17.campo6;
                            initObj.MODGCVD.VgCVDo.PrtCli = Result.codpro_17.prtexp;
                            initObj.MODGCVD.VgCVDo.IndNomC = (short)Result.codpro_17.indnome;
                            initObj.MODGCVD.VgCVDo.IndDirC = (short)Result.codpro_17.inddire;
                            break;

                        case 18:
                            initObj.MODGCVD.VgCVDo.codcct = Result.codpro_18.codcct;
                            initObj.MODGCVD.VgCVDo.codpro = Result.codpro_18.codpro;
                            initObj.MODGCVD.VgCVDo.codesp = Result.codpro_18.codesp;
                            initObj.MODGCVD.VgCVDo.codofi = Result.codpro_18.codofi;
                            initObj.MODGCVD.VgCVDo.codope = Result.codpro_18.codope;
                            initObj.MODGCVD.VgCVDo.Fecing = Result.codpro_18.fecing.ToString();
                            initObj.MODGCVD.VgCVDo.TipCVD = (short)Result.codpro_18.campo6;
                            initObj.MODGCVD.VgCVDo.PrtCli = Result.codpro_18.prtexp;
                            initObj.MODGCVD.VgCVDo.IndNomC = (short)Result.codpro_18.indexpn;
                            initObj.MODGCVD.VgCVDo.IndDirC = (short)Result.codpro_18.indexpd;

                            break;

                        case 20:
                            initObj.MODGCVD.VgCVDo.codcct = Result.codpro_20.codcct;
                            initObj.MODGCVD.VgCVDo.codpro = Result.codpro_20.codpro;
                            initObj.MODGCVD.VgCVDo.codesp = Result.codpro_20.codesp;
                            initObj.MODGCVD.VgCVDo.codofi = Result.codpro_20.codofi;
                            initObj.MODGCVD.VgCVDo.codope = Result.codpro_20.codope;
                            initObj.MODGCVD.VgCVDo.Fecing = Result.codpro_20.fecing.ToString();
                            initObj.MODGCVD.VgCVDo.TipCVD = (short)Result.codpro_20.tipcvd;
                            initObj.MODGCVD.VgCVDo.PrtCli = Result.codpro_20.prtcli;
                            initObj.MODGCVD.VgCVDo.IndNomC = (short)Result.codpro_20.indnomc;
                            initObj.MODGCVD.VgCVDo.IndDirC = (short)Result.codpro_20.inddirc;
                            break;

                        case 30:
                            initObj.MODGCVD.VgCVDo.codcct = Result.codpro_30.codcct;
                            initObj.MODGCVD.VgCVDo.codpro = Result.codpro_30.codpro;
                            initObj.MODGCVD.VgCVDo.codesp = Result.codpro_30.codesp;
                            initObj.MODGCVD.VgCVDo.codofi = Result.codpro_30.codofi;
                            initObj.MODGCVD.VgCVDo.codope = Result.codpro_30.codope;
                            initObj.MODGCVD.VgCVDo.Fecing = Result.codpro_30.fecing.ToString();
                            initObj.MODGCVD.VgCVDo.TipCVD = (short)Result.codpro_30.tipcvd;
                            initObj.MODGCVD.VgCVDo.PrtCli = Result.codpro_30.prtcli;
                            initObj.MODGCVD.VgCVDo.IndNomC = (short)Result.codpro_30.indnomc;
                            initObj.MODGCVD.VgCVDo.IndDirC = (short)Result.codpro_30.inddirc;
                            break;

                        default:
                            break;
                    }
                }
                _retValue = (short)(true ? -1 : 0);
            }
            catch (Exception _ex)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                    Title = T_MODGCVD.MsgCVD
                });
                _retValue = 0;
            }
            return _retValue;
        }

        //Lee las planillas de la Compra-Venta que se va a Anular.
        //Retorno =  True    => Exitoso.
        //           False   => Erróneo.
        public static int SyGetpl_CVD(string NumOpe, InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            using (var tracer = new Tracer())
            {
                short _retValue = 0;
                short n = 0;
                short i = 0;
                short m = 0;

                try
                {
                    var result = uow.SceRepository.sce_cvd1_s03_MS(
                                MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)),
                                MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)),
                                MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)),
                                MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)),
                                MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5))).ToArray();


                    n = (short)result.Count();
                    VB6Helpers.RedimPreserve(ref initObj.MODGANU.VAnuPl, 0, n - 1);
                    for (i = 0; i < (short)n; i++)
                    {
                        initObj.MODGANU.VAnuPl[i].NumPln = result[i].numpln.Trim();
                        initObj.MODGANU.VAnuPl[i].FecPln = result[i].fecpln.ToString();
                        initObj.MODGANU.VAnuPl[i].VisInv = result[i].visinv.Trim();
                        initObj.MODGANU.VAnuPl[i].TipPln = (short)result[i].tippln;
                        initObj.MODGANU.VAnuPl[i].CodMnd = (short)result[i].codmnd;
                        initObj.MODGANU.VAnuPl[i].MtoPln = (double)result[i].mtopln;
                        initObj.MODGANU.VAnuPl[i].TipCam = (double)result[i].tipcam;
                        initObj.MODGANU.VAnuPl[i].TipCamo = (double)result[i].tipcamo;
                        initObj.MODGANU.VAnuPl[i].PlzBcc = (short)result[i].plzbcc;
                        initObj.MODGANU.VAnuPl[i].estado = (short)result[i].estado;
                        initObj.MODGANU.VAnuPl[i].numdec = result[i].numdec.ToString();
                        initObj.MODGANU.VAnuPl[i].FecDec = result[i].fecdec.ToString();
                        initObj.MODGANU.VAnuPl[i].CodAdn = (short)result[i].codadn;
                        initObj.MODGANU.VAnuPl[i].PrtExp = result[i].prtexp.ToString();
                        initObj.MODGANU.VAnuPl[i].codcom = result[i].codcom.ToString();
                        m = MODGTAB0.Get_VMnd(initObj.MODGTAB0, uow, initObj.MODGANU.VAnuPl[i].CodMnd);
                        initObj.MODGANU.VAnuPl[i].NemMnd = initObj.MODGTAB0.VMnd[m].Mnd_MndNmc;
                        initObj.MODGANU.VAnuPl[i].CodMndBC = initObj.MODGTAB0.VMnd[m].Mnd_MndCbc;
                        initObj.MODGANU.VAnuPl[i].MtoPln_t = Utils.Format.FormatCurrency(initObj.MODGANU.VAnuPl[i].MtoPln, "#,###,###,###,##0.00");
                    }

                    _retValue = (short)(true ? -1 : 0);

                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta", ex);

                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                        Title = T_MODGCVD.MsgCVD + ex
                    });

                }
                return _retValue;
            }
        }

        public static short SyGet_ANU(string NumOpe, DateTime? Fecha, InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            using (var trace = new Tracer())
            {
                #region Inicializacion Variables
                short _retValue = 0;
                #endregion

                try
                {
                    _retValue = short.Parse(unit.SceRepository.sce_xanu_s03_MS(NumOpe, Fecha));
                    if (_retValue == 1)
                    {
                        _retValue = (short)(false ? -1 : 0);
                        return 0;
                    }
                    _retValue = (short)(true ? -1 : 0);
                }
                catch (Exception ex)
                {
                    trace.TraceException("Alerta", ex);
                    throw;
                }
                return _retValue;
            }
        }

        //Funcion que verifica si una planilla invisible ha sido anulada
        //
        public static short SyGetA_Pli(string NumOpe, DateTime Fecha, InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            #region Inicializacion Variables
            short _retValue = 0;
            #endregion
            try
            {
                _retValue = short.Parse(unit.SceRepository.sce_pli_s04_MS(NumOpe, Fecha));

                if (VB6Helpers.Left((_retValue.ToString()), 1) == "1")
                {
                    _retValue = (short)(false ? -1 : 0);
                    return 0;
                }

            }
            catch (Exception ex)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                    Title = T_MODGPLI1.MsgPli
                });
                return _retValue;
            }

            _retValue = (short)(true ? -1 : 0);

            return _retValue;
        }

        //   1.  Lee una Planilla Invisible de Exportación para su posterior Anulación.
        //   2.  Retorno    <> 0    : Retorna los datos de la Planilla.
        //                  =  0    : No existen datos de esa Planilla.
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short SyGet_PliAnu(short ind, InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            using (var tracer = new Tracer())
            {
                short _retValue = 0;
                short largo = 0;
                short n = 0;
                short i = 0;

                try
                {
                    T_Pli result = new T_Pli();
                    result = uow.SceRepository.sce_pli_s07_MS(
                                   MODGSYB.dbcharSy(initObj.MODGANU.VAnuPl[ind].NumPln),
                                   Convert.ToDateTime(MODGSYB.dbdatesy(initObj.MODGANU.VAnuPl[ind].FecPln))
                                 );

                    largo = (short)VB6Helpers.UBound(initObj.MODGPLI1.Vplis);

                    n = 1;
                    VB6Helpers.RedimPreserve(ref initObj.MODGPLI1.Vplis, 0, largo + n);
                    i = (short)(largo + n);

                    //-----------------------------------------
                    //Se genera una nueva planilla.
                    //-----------------------------------------
                    initObj.MODGPLI1.Vplis[i].NumPli = VB6Helpers.Trim(VB6Helpers.Str(MODGRNG.LeeSceRng(initObj.MODGRNG, initObj.MODGUSR, initObj.Mdi_Principal, uow, "PLI")));
                    if (string.IsNullOrEmpty(initObj.MODGPLI1.Vplis[i].NumPli))
                    {
                        return _retValue;
                    }

                    initObj.MODGPLI1.Vplis[i].FecPli = DateTime.Now.ToString("dd/MM/yyyy");
                    initObj.MODGPLI1.Vplis[i].Fecing = DateTime.Now.ToString("dd/MM/yyyy");

                    initObj.MODGPLI1.Vplis[i].AnuNum = result.NumPli;
                    initObj.MODGPLI1.Vplis[i].AnuFec = result.FecPli;
                    initObj.MODGPLI1.Vplis[i].AnuPbc = result.PlzBcc;

                    initObj.MODGPLI1.Vplis[i].cencos = result.cencos;
                    initObj.MODGPLI1.Vplis[i].codusr = result.codusr;
                    initObj.MODGPLI1.Vplis[i].codcct = result.codcct;
                    initObj.MODGPLI1.Vplis[i].codpro = result.codpro;
                    initObj.MODGPLI1.Vplis[i].codesp = result.codesp;
                    initObj.MODGPLI1.Vplis[i].codofi = result.codofi;
                    initObj.MODGPLI1.Vplis[i].codope = result.codope;
                    initObj.MODGPLI1.Vplis[i].CodAnu = "000000";
                    initObj.MODGPLI1.Vplis[i].Estado = T_MODGPLI1.EPli_Emi;
                    initObj.MODGPLI1.Vplis[i].CodOper = result.CodOper;
                    initObj.MODGPLI1.Vplis[i].PlzBcc = 25;
                    initObj.MODGPLI1.Vplis[i].rutcli = result.rutcli;
                    initObj.MODGPLI1.Vplis[i].PrtCli = result.PrtCli;
                    initObj.MODGPLI1.Vplis[i].IndNom = result.IndNom;
                    initObj.MODGPLI1.Vplis[i].IndDir = result.IndDir;
                    initObj.MODGPLI1.Vplis[i].CodOci = result.CodOci;
                    initObj.MODGPLI1.Vplis[i].TipPln = result.TipPln;
                    short _switchVar1 = initObj.MODGPLI1.Vplis[i].TipPln;
                    if (_switchVar1 == T_Mdl_Funciones.TPli_Ingreso)
                    {
                        initObj.MODGPLI1.Vplis[i].TipPln = T_Mdl_Funciones.TPli_AnuIng;
                        if (initObj.MODGPLI1.Vplis[i].CodOci == 110)
                        {
                            initObj.MODGPLI1.Vplis[i].CodOci = 115;
                        }
                        if (initObj.MODGPLI1.Vplis[i].CodOci == 140)
                        {
                            initObj.MODGPLI1.Vplis[i].CodOci = 145;
                        }
                    }
                    else if (_switchVar1 == T_Mdl_Funciones.TPli_Egreso)
                    {
                        initObj.MODGPLI1.Vplis[i].TipPln = T_Mdl_Funciones.TPli_AnuEgr;
                        if (initObj.MODGPLI1.Vplis[i].CodOci == 210)
                        {
                            initObj.MODGPLI1.Vplis[i].CodOci = 215;
                        }
                        if (initObj.MODGPLI1.Vplis[i].CodOci == 240)
                        {
                            initObj.MODGPLI1.Vplis[i].CodOci = 245;
                        }
                    }
                    else if (_switchVar1 == T_Mdl_Funciones.TPli_TranIng)
                    {
                        initObj.MODGPLI1.Vplis[i].TipPln = T_Mdl_Funciones.TPli_AnuTranIng;
                        if (initObj.MODGPLI1.Vplis[i].CodOci == 110)
                        {
                            initObj.MODGPLI1.Vplis[i].CodOci = 115;
                        }
                        if (initObj.MODGPLI1.Vplis[i].CodOci == 140)
                        {
                            initObj.MODGPLI1.Vplis[i].CodOci = 145;
                        }
                    }
                    else if (_switchVar1 == T_Mdl_Funciones.TPli_TranEg)
                    {
                        initObj.MODGPLI1.Vplis[i].TipPln = T_Mdl_Funciones.TPli_AnuTranEg;
                        if (initObj.MODGPLI1.Vplis[i].CodOci == 210)
                        {
                            initObj.MODGPLI1.Vplis[i].CodOci = 215;
                        }
                        if (initObj.MODGPLI1.Vplis[i].CodOci == 240)
                        {
                            initObj.MODGPLI1.Vplis[i].CodOci = 245;
                        }
                    }

                    initObj.MODGPLI1.Vplis[i].codcom = result.codcom;
                    initObj.MODGPLI1.Vplis[i].Concep = result.Concep;
                    initObj.MODGPLI1.Vplis[i].ApcTip = initObj.MODGANU.VAnuPl[ind].ApcTip;
                    initObj.MODGPLI1.Vplis[i].ApcNum = initObj.MODGANU.VAnuPl[ind].ApcNum;
                    initObj.MODGPLI1.Vplis[i].ApcFec = initObj.MODGANU.VAnuPl[ind].ApcFec;
                    initObj.MODGPLI1.Vplis[i].ApcPbc = initObj.MODGANU.VAnuPl[ind].PlzBcc;
                    initObj.MODGPLI1.Vplis[i].Motivo = initObj.MODGANU.VAnuPl[ind].Motivo;

                    initObj.MODGPLI1.Vplis[i].NumAcu = result.NumAcu;
                    initObj.MODGPLI1.Vplis[i].Desacu = result.Desacu;
                    initObj.MODGPLI1.Vplis[i].codpai = result.codpai;
                    initObj.MODGPLI1.Vplis[i].CodMnd = result.CodMnd;
                    initObj.MODGPLI1.Vplis[i].CodMndBC = result.CodMndBC;
                    initObj.MODGPLI1.Vplis[i].MtoOpe = result.MtoOpe;
                    initObj.MODGPLI1.Vplis[i].Mtopar = result.Mtopar;
                    initObj.MODGPLI1.Vplis[i].MtoDol = result.MtoDol;
                    initObj.MODGPLI1.Vplis[i].TipCam = initObj.MODGANU.VAnuPl[ind].TipCam;
                    initObj.MODGPLI1.Vplis[i].MtoNac = initObj.MODGPLI1.Vplis[i].TipCam * initObj.MODGPLI1.Vplis[i].MtoDol;
                    initObj.MODGPLI1.Vplis[i].DieNum = result.DieNum;
                    initObj.MODGPLI1.Vplis[i].DieFec = result.DieFec;
                    initObj.MODGPLI1.Vplis[i].DiePbc = result.DiePbc;
                    initObj.MODGPLI1.Vplis[i].numdec = result.numdec;
                    initObj.MODGPLI1.Vplis[i].FecDec = result.FecDec;
                    initObj.MODGPLI1.Vplis[i].CodAdn = result.CodAdn;
                    initObj.MODGPLI1.Vplis[i].FecDeb = result.FecDeb;
                    initObj.MODGPLI1.Vplis[i].DocNac = result.DocNac;
                    initObj.MODGPLI1.Vplis[i].DocExt = result.DocExt;
                    initObj.MODGPLI1.Vplis[i].BcoExt = result.BcoExt;
                    initObj.MODGPLI1.Vplis[i].NumCre = result.NumCre;
                    initObj.MODGPLI1.Vplis[i].FecCre = result.FecCre;
                    initObj.MODGPLI1.Vplis[i].MndCre = result.MndCre;
                    initObj.MODGPLI1.Vplis[i].MtoCre = result.MtoCre;
                    initObj.MODGPLI1.Vplis[i].CodAcu = result.CodAcu;
                    initObj.MODGPLI1.Vplis[i].RegAcu = result.RegAcu;
                    initObj.MODGPLI1.Vplis[i].RutAcu = result.RutAcu;
                    initObj.MODGPLI1.Vplis[i].ZonFra = result.ZonFra;
                    initObj.MODGPLI1.Vplis[i].SecBen = result.SecBen;
                    initObj.MODGPLI1.Vplis[i].SecInv = result.SecInv;
                    initObj.MODGPLI1.Vplis[i].PrcPar = result.PrcPar;
                    initObj.MODGPLI1.Vplis[i].ObsPli = initObj.MODGANU.VAnuPl[ind].ObsPln;
                    _retValue = (short)(true ? -1 : 0);
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta", e);

                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                        Title = T_MODGPLI1.MsgPli
                    });

                    return _retValue;
                }
                return _retValue;
            }
        }

        public static short SyGetPlanCVD_Anu(string NumOpe, UnitOfWorkCext01 unit, InitializationObject initObj)
        {
            short _retValue = 0;

            try
            {
                var result = unit.SceRepository.sce_cvd1_s04_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)),
                    MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)),
                    MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)));
                VB6Helpers.Redim(ref initObj.MODGANU.VAnuPl, 0, result.Count - 1);
                //initObj.MODGANU.VAnuPl = new T_AnuPl[result.Count];
                for (int i = 0; i < result.Count; i++)
                {
                    var item = result[i];
                    //var m = MODGTAB0.Get_VMnd(initObj.MODGTAB0, unit, initObj.MODGANU.VAnuPl[i].CodMnd);
                    var m = MODGTAB0.Get_VMnd(initObj.MODGTAB0, unit, (short)result[i].codmnd);
                    var anuPl = new T_AnuPl
                    {
                        NumPln = item.numpln,
                        FecPln = item.fecpln.ToString(),
                        VisInv = item.visinv,
                        TipPln = (short)item.tippln,
                        CodMnd = (short)item.codmnd,
                        MtoPln = (double)item.mtopln,
                        TipCam = (double)item.tipcam,
                        TipCamo = (double)item.tipcamo,
                        PlzBcc = (short)item.plzbcc,
                        estado = (short)item.estado,
                        numdec = item.numdec,
                        FecDec = item.fecdec.ToString(),
                        CodAdn = (short)item.codadn,
                        PrtExp = item.prtexp,
                        codcom = item.codcom,
                        NemMnd = initObj.MODGTAB0.VMnd[m].Mnd_MndNmc,
                        CodMndBC = initObj.MODGTAB0.VMnd[m].Mnd_MndCbc,
                        MtoPln_t = item.mtopln.ToString()
                    };
                    initObj.MODGANU.VAnuPl[i] = anuPl;
                }
                _retValue = (short)(true ? -1 : 0);
            }
            catch
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Se ha producido un error al leer una Operación de Compra-Venta ."
                });
                _retValue = -1;
            }
            return _retValue;
        }

        //Anula una Operación de Compra-Venta.
        //Retorno =  True    => Exitoso.
        //           False   => Erróneo.
        public static short SyPutr_Cvd(string NumOpe, UnitOfWorkCext01 unit, InitializationObject initObj)
        {
            using (var trace = new Tracer("SyPutr_Cvd"))
            {
                //short _retValue = 0;
                //string Que = "";
                string R = "";
                short Codigo = 0;
                string Msg = "";
                //dynamic MsgxCob = null;

                //try
                //{
                var RESULTADO = unit.SceRepository.sce_anu_u03_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)));
                R = RESULTADO.Column1.ToString();
                Msg = RESULTADO.Column2;
                if (R == "-1")
                {
                    trace.TraceError("Se ha producido un error al anular la Operación de Compra-Venta (Sce_Cvd). El Servidor reporta : [" + Msg + "]. Reporte este problema.");
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error al anular la Operación de Compra-Venta (Sce_Cvd). El Servidor reporta : [" + Msg + "]. Reporte este problema."
                    });
                    return 0;
                }
                Codigo = (short)VB6Helpers.Val(MODGPYF0.copiardestring(R, "~", 1));
                if (Codigo == -1)
                {
                    trace.TraceError("Se ha producido un error al tratar de Anular la Operación de Compra-Venta. El Servidor reporta : [" + Msg + "]. Reporte este problema.");
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error al tratar de Anular la Operación de Compra-Venta. El Servidor reporta : [" + Msg + "]. Reporte este problema."
                    });
                }

                //Resultado nulo de la Consulta.
                if (string.IsNullOrEmpty(R))
                {
                    return 0;
                }

                return -1; //Devuelve -1 cuando es Exitoso
            }
        }

        /// <summary>
        /// envia monto de operacion para transf. internas o comisiones.
        /// </summary>
        /// <param name="NumOpe"> numero de operacion </param>
        /// <param name="unit"> conexion a la base de datos </param>
        /// <returns></returns>
        public static string SyGet_montoOperacion(string NumOpe, UnitOfWorkCext01 unit)
        {
            using (var tracer = new Tracer("Get_MontoTotalOp"))
            {
                string codcct = NumOpe.Substring(0, 3);
                string codpro = NumOpe.Substring(3, 2);
                string codesp = NumOpe.Substring(5, 2);
                string codofi = NumOpe.Substring(7, 3);
                string codope = NumOpe.Substring(10, 5);
                tracer.TraceInformation("Número de operación: " + codcct + codpro + codesp + codofi + codope);
                return unit.SceRepository.proc_sel_montoOperacionAnulacionDia_MS(codcct, codpro, codesp, codofi, codope);
            }
        }

    }
}
