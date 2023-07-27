using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODGCON0
    {
        // Retorna el Número de Partida de la Operación Contable.-
        public static int GetNroPar(DatosGlobales Globales)
        {
            T_MODGUSR MODGUSR = Globales.MODGUSR;
            int GetNroPar = 0;


            GetNroPar = (MODGUSR.UsrEsp.Especialista + DateTime.Now.ToString("HHmmss")).ToInt();

            return GetNroPar;
        }

        // Retorna el índice en VMcds() para incluir los Movs. Contables.-
        public static int GetIndMcd(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            T_MODGCON0 MODGCON0 = Globales.MODGCON0;
            T_MODGNCTA MODGNCTA = Globales.MODGNCTA;
            int GetIndMcd = 0;

            int m = 0;
            int n = 0;

            MODGCON0.VMcd.NemMon = MODGTAB0.Get_NemMnd(Globales,unit, MODGCON0.VMcd.CodMon);
            n = MODGCON0.VMcds.GetUpperBound(0) + 1;
            m = BCH.Comex.Core.BL.XGGL.Modulos.MODGNCTA.Get_Cta(MODGCON0.VMcd.NemCta,Globales,unit);
            MODGCON0.VMcd.numcta = MODGNCTA.VCta[m].Cta_Num;
            MODGCON0.VMcd.IntCIT = MODGNCTA.VCta[m].Cta_CIT;
            MODGCON0.VMcd.IntCVT = MODGNCTA.VCta[m].Cta_CVT;
            MODGCON0.VMcd.intcap = MODGNCTA.VCta[m].Cta_CAP;
            MODGCON0.VMcd.IntCTD = MODGNCTA.VCta[m].Cta_CTD;
            MODGCON0.VMcd.IntPOS = MODGNCTA.VCta[m].Cta_POS;
            MODGCON0.VMcd.IntCDR = MODGNCTA.VCta[m].Cta_CDR;
            MODGCON0.VMcd.NroTOp = MODGNCTA.VCta[m].Cta_NroTO;
            MODGCON0.VMcd.IndTOp = MODGNCTA.VCta[m].Cta_IndTO;
            MODGCON0.VMcd.NroImp = n;
            Array.Resize(ref MODGCON0.VMcds, n + 1);
            MODGCON0.VMcds[n] = new T_Mcd();
            MODGCON0.VMcds[n] = MODGCON0.VMcd.Copy();
            GetIndMcd = n;

            return GetIndMcd;
        }

        //Escribe los Movimientos Contables en Sybase.-
        public static short SyPutCon(DatosGlobales initObj, UnitOfWorkCext01 unit, string Usuario, int ImpDeb)
        {
            using(var tracer = new Tracer("SyPutCon"))
            {
                short Ok = (short)(true ? -1 : 0);
                short Y = 0;
                short x = 0;
                short Hab_CtaCteLinea = 0;
                if (Refunde(initObj, unit, ImpDeb) == 0)
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
                    initObj.MODGCON0.VMch = initObj.MODGCON0.VmchNul.Copy();
                }

                return Ok;
            }
        }

        //Refunde los Movimientos de Cuenta Corriente en Moneda Nacional.-
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static int Refunde(DatosGlobales Globales, UnitOfWorkCext01 unit, int ImpDeb)
        {
            T_MODGCON0 MODGCON0 = Globales.MODGCON0;
            T_MODGMTA MODGMTA = Globales.MODGMTA;
            T_MODGSCE MODGSCE = Globales.MODGSCE;
            T_MODGNCTA MODGNCTA = Globales.MODGNCTA;

            int Refunde = 0;

            bool HayCVT = false;
            int m = 0;
            int X = 0;
            int i = 0;
            int n = 0;

            // *************************************************************************************************************************************
            //    Autor                      : Accenture - Continuidad Comex
            //    Incidente                  : IR55780
            //    Descripcion                : Problemas Interfaz CVT con rut en Blanco - PBI700000256198
            //    Fecha                      : noviembre del 2012
            //    Identificador de Inicio    : ACC-001-I
            //    Identificador de Termino   : ACC-001-F
            // *************************************************************************************************************************************

            
            MODGCON0.VMcdz = new T_Mcd[1] { new T_Mcd() };
            n = MODGCON0.VMcds.GetUpperBound(0);
            
            for (i = 1; i <= n; i += 1)
            {
                if (MODGCON0.VMcds[i].numcta == "40001018")
                {
                    if (MODGCON0.VMcds[i].numcct == MODGCON0.VMcds[i - 1].numcct && MODGCON0.VMcds[i].numcta == MODGCON0.VMcds[i - 1].numcta && MODGCON0.VMcds[i].cod_dh == MODGCON0.VMcds[i - 1].cod_dh)
                    {
                        MODGCON0.VMcdz[X].mtomcd = MODGCON0.VMcdz[X].mtomcd + MODGCON0.VMcds[i].mtomcd;
                    }
                    else
                    {
                        
                        X = MODGCON0.VMcdz.GetUpperBound(0);
                        X = X + 1;
                        Array.Resize(ref MODGCON0.VMcdz, X + 1);
                        MODGCON0.VMcdz[X] = new T_Mcd();
                        MODGCON0.VMcdz[X] = MODGCON0.VMcds[i];
                        MODGCON0.VMcdz[X].NroImp = X;
                        
                    }
                }
                else
                {
                    
                    X = MODGCON0.VMcdz.GetUpperBound(0);
                    X = X + 1;
                    Array.Resize(ref MODGCON0.VMcdz, X + 1);
                    MODGCON0.VMcdz[X] = new T_Mcd();
                    MODGCON0.VMcdz[X] = MODGCON0.VMcds[i];
                    MODGCON0.VMcdz[X].NroImp = X;
                    
                }

                // Si viene Cuenta Corriente => Sumar el Impuesto y el Timbre.-
                if (ImpDeb != 0)
                {
                    // Verifica que esté seteado Impuesto al débito.-
                    if (MODGCON0.VMcdz[X].numcta == "40001018" && MODGCON0.VMcdz[X].cod_dh == "D" && MODGMTA.impflag == 1)
                    {
                        //  JFO Modificación al Impuesto 07/02/2007 si el flag viene en 1 se cobra el impuesto
                        MODGCON0.VMcdz[X].mtomcd = MODGCON0.VMcdz[X].mtomcd + MODGSCE.VGen.MtoDeb;
                        n = MODGCON0.VMcdz.GetUpperBound(0) + 1;
                        Array.Resize(ref MODGCON0.VMcdz, n + 1);
                        MODGCON0.VMcdz[n].CodCct = MODGCON0.VMch.CodCct;
                        MODGCON0.VMcdz[n].CodPro = MODGCON0.VMch.CodPro;
                        MODGCON0.VMcdz[n].CodEsp = MODGCON0.VMch.CodEsp;
                        MODGCON0.VMcdz[n].CodOfi = MODGCON0.VMch.CodOfi;
                        MODGCON0.VMcdz[n].CodOpe = MODGCON0.VMch.CodOpe;
                        MODGCON0.VMcdz[n].CodNeg = MODGCON0.VMch.CodNeg;
                        MODGCON0.VMcdz[n].CodSec = MODGCON0.VMch.CodSec;
                        MODGCON0.VMcdz[n].NroRpt = MODGCON0.VMch.NroRpt;
                        MODGCON0.VMcdz[n].FecMov = MODGCON0.VMch.FecMov;
                        MODGCON0.VMcdz[n].cencos = MODGCON0.VMch.cencos;
                        MODGCON0.VMcdz[n].codusr = MODGCON0.VMch.codusr;
                        MODGCON0.VMcdz[n].NroImp = n;
                        MODGCON0.VMcdz[n].estado = T_MODGCON0.ECC_ING;
                        MODGCON0.VMcdz[n].TipMcd = T_MODGCON0.CONTAB_ING;

                        m = BCH.Comex.Core.BL.XGGL.Modulos.MODGNCTA.Get_Cta("FIJO$",Globales,unit);
                        if (m == 0)
                        {
                            return Refunde;
                        }

                        if (MODGMTA.impflag == 1)
                        {
                            //  JFO Modificacion del Impuesto 07/02/2007 si el flag viene en 1 se cobra el impuesto

                            MODGCON0.VMcdz[n].IdnCta = 0;
                            MODGCON0.VMcdz[n].NemCta = MODGNCTA.VCta[m].Cta_Nem;
                            MODGCON0.VMcdz[n].numcta = MODGNCTA.VCta[m].Cta_Num;
                            MODGCON0.VMcdz[n].CodMon = T_MODGTAB0.MndNac;
                            MODGCON0.VMcdz[n].NemMon = "$";
                            MODGCON0.VMcdz[n].mtomcd = MODGSCE.VGen.MtoDeb;
                            MODGCON0.VMcdz[n].cod_dh = "H";
                            MODGCON0.VMcdz[n].NumEmb = 0;
                            MODGCON0.VMcdz[n].PrtCli = "";
                            MODGCON0.VMcdz[n].IndCli = 0;
                            MODGCON0.VMcdz[n].rutcli = "";
                            MODGCON0.VMcdz[n].PrtBco = "";
                            MODGCON0.VMcdz[n].IndBco = 0;
                            MODGCON0.VMcdz[n].RutBco = "";
                            MODGCON0.VMcdz[n].SwiBco = "";
                            MODGCON0.VMcdz[n].CodBco = 0;
                            MODGCON0.VMcdz[n].numcct = "";
                            MODGCON0.VMcdz[n].numlin = "";
                            MODGCON0.VMcdz[n].FecOri = "";
                            MODGCON0.VMcdz[n].FecVen = "";
                            MODGCON0.VMcdz[n].FecInt = "";
                            MODGCON0.VMcdz[n].TasFij = 0;
                            MODGCON0.VMcdz[n].MtoTas = 0;
                            MODGCON0.VMcdz[n].OfiDes = 0;
                            MODGCON0.VMcdz[n].NumPar = 0;
                            MODGCON0.VMcdz[n].TipMov = 0;
                            MODGCON0.VMcdz[n].NroRef = "";
                            MODGCON0.VMcdz[n].TipCam = 0;
                            MODGCON0.VMcdz[n].NroTOp = MODGNCTA.VCta[m].Cta_NroTO;
                            MODGCON0.VMcdz[n].IndTOp = MODGNCTA.VCta[m].Cta_IndTO;
                            MODGCON0.VMcdz[n].IntCIT = MODGNCTA.VCta[m].Cta_CIT;
                            MODGCON0.VMcdz[n].IntCVT = MODGNCTA.VCta[m].Cta_CVT;
                            MODGCON0.VMcdz[n].intcap = MODGNCTA.VCta[m].Cta_CAP;
                            MODGCON0.VMcdz[n].IntCTD = MODGNCTA.VCta[m].Cta_CTD;
                            MODGCON0.VMcdz[n].IntPOS = MODGNCTA.VCta[m].Cta_POS;
                            MODGCON0.VMcdz[n].IntCDR = MODGNCTA.VCta[m].Cta_CDR;
                            MODGCON0.VMcdz[n].McdVig = false.ToInt();

                        }
                    }
                }

            }

            // Se generan los movimientos detalle.-
            n = MODGCON0.VMcdz.GetUpperBound(0);
            MODGCON0.VMcds = new T_Mcd[n + 1];
            for (i = 1; i <= n; i += 1)
            {
                MODGCON0.VMcds[i] = MODGCON0.VMcdz[i].Copy();
            }

            // Verifica que para cuentas de IVA y Comisión, el Party tenga Rut.-
            for (i = 1; i <= MODGCON0.VMcds.GetUpperBound(0); i += 1)
            {
                //     If Left$(VMcds(i%).NemCta, 5) = "COMIS" Or Left$(VMcds(i%).NemCta, 5) = "IVA" Then 'ACC-001-I Antes
                if (MODGCON0.VMcds[i].NemCta.Left(3) == "COM" && MODGCON0.VMcds[i].numcta.Left(8) == "22112015" || MODGCON0.VMcds[i].NemCta.Left(3) == "IVA")
                {
                    //  ACC-001-F Nuevo
                    HayCVT = true;
                    break;
                }
            }
            if (HayCVT && MODGCON0.VMch.rutcli == "")
            {
                Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                {
                    Type=Common.UI_Modulos.TipoMensaje.Error,
                    Text= "No se puede grabar el Reporte Contable debido a que generaría una Factura para una persona que NO tiene Rut.",
                    Title= "Validación Reporte Contable"
                });
                return Refunde;
            }

            Refunde = true.ToInt();

            return Refunde;
        }


        //Graba el Encabezado del Reporte Contable (Sce_Mch).-
        //Retorno    = True  : Grabación Exitosa.-
        //           = False : Error o Grabación no Exitosa.-
        public static short SyPut_Mch(DatosGlobales Globales, UnitOfWorkCext01 unit, string Usuario)
        {
            short _retValue = 0;
            List<string> parameters = new List<string>();
            try
            {
                //Genera consulta.-
                parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.CodCct));
                parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.CodPro));
                parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.CodEsp));
                parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.CodOfi));
                parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.CodOpe));
                parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMch.CodNeg));
                parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMch.CodSec));
                parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMch.NroRpt));
                parameters.Add(MODGSYB.dbdatesy(Globales.MODGCON0.VMch.FecMov));
                parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Left(Usuario, 3)));
                parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Right(Usuario, 2)));
                parameters.Add(MODGSYB.dbnumesy(T_MODGCON0.ECC_ANU));
                parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMch.OfiCon));
                parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.PrtCli));
                parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMch.IndCli));
                parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.rutcli));
                parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.NomCli));
                parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.DirCli));
                parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.ComCli));
                parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.CiuCli));
                parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.PaiCli));
                parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMch.codfun));
                parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.operel));
                parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.DesGen));

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
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Informacion,
                    Text = "Se ha producido un error al tratar de grabar el Encabezado del Reporte Contable (Sce_Mch)." ,
                    Title = T_MODGCON0.MsgCon
                });
                throw _ex;
            }
        }

        public static short SyPutn_Mcd_CCLin(DatosGlobales Globales, UnitOfWorkCext01 unit, string Usuario)
        {
            short _retValue = 0;
            short HayError = 0;
            short i = 0;
            string Que = "";
            string R = "";

            string rut = "";
            short res = 0;

            try
            {
                HayError = (short)(false ? -1 : 0);

                //Graba los movimientos de un Reporte Contable.-
                for (i = 1; i <= (short)VB6Helpers.UBound(Globales.MODGCON0.VMcds); i++)
                {
                    //Se incluyen estas validaciones anexas.-
                    if (Globales.MODGCON0.VMcds[i].CodMon == 0)
                    {
                        Globales.MESSAGES.Add(new UI_Message()
                        {
                            Type=TipoMensaje.Error,
                            Title= "Validación de Datos",
                            Text= "Existe un movimiento contable que no tiene Nemónico de Moneda."
                        });
                        return _retValue;
                    }

                    if (string.IsNullOrEmpty(Globales.MODGCON0.VMcds[i].NemMon))
                    {
                        Globales.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Title = "Validación de Datos",
                            Text = "Existe un movimiento contable que no tiene Código de Moneda."
                        });
                        return _retValue;
                    }

                    if (Globales.MODGCON0.VMcds[i].mtomcd == 0)
                    {
                        Globales.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Title = "Validación de Datos",
                            Text = "Existe un movimiento contable que no tiene un Monto válido."
                        });
                        return _retValue;
                    }

                    if (string.IsNullOrEmpty(Globales.MODGCON0.VMcds[i].NemCta))
                    {
                        Globales.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Title = "Validación de Datos",
                            Text = "Existe un movimiento contable que no tiene Nemónico de Cuenta Contable."
                        });
                        return _retValue;
                    }

                    //res = Mdl_SRM.AISGetUsr(initObj.MODGCVD.RutwAis.Value);  // Rut del Especialista
                    rut = VB6Helpers.Mid(Globales.MODGCVD.RutwAis, 1, 8);

                    //Genera consulta.-
                    List<string> parameters = new List<string>();

                    parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.CodCct));
                    parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.CodPro));
                    parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.CodEsp));
                    parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.CodOfi));
                    parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.CodOpe));
                    parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMch.CodNeg));
                    parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMch.CodSec));
                    parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].NroRpt));
                    parameters.Add(MODGSYB.dbdatesy(Globales.MODGCON0.VMcds[i].FecMov));
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Left(Usuario, 3)));
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Right(Usuario, 2)));
                    parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].NroImp + 1));
                    parameters.Add(MODGSYB.dbnumesy(T_MODGCON0.ECC_ANU));
                    parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].TipMcd));
                    parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].IdnCta));
                    parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].NemCta));
                    parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].numcta));
                    parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].CodMon));
                    parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].NemMon));
                    parameters.Add(MODGSYB.dbmontoSyForRead(Globales.MODGCON0.VMcds[i].mtomcd));
                    parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].cod_dh));
                    parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].NumEmb));
                    parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].PrtCli));
                    parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].IndCli));
                    parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].rutcli));
                    parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].PrtBco));
                    parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].IndBco));
                    parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].RutBco));
                    parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].SwiBco));
                    parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].CodBco));
                    parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].numcct));
                    parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].numlin));
                    parameters.Add(MODGSYB.dbdatesy(Globales.MODGCON0.VMcds[i].FecOri));
                    parameters.Add(MODGSYB.dbdatesy(Globales.MODGCON0.VMcds[i].FecVen));
                    parameters.Add(MODGSYB.dbdatesy(Globales.MODGCON0.VMcds[i].FecInt));
                    parameters.Add(MODGSYB.dblogisy(Globales.MODGCON0.VMcds[i].TasFij.ToShort()));
                    parameters.Add(MODGSYB.dbtasaSyForRead(Globales.MODGCON0.VMcds[i].MtoTas));
                    parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].OfiDes));
                    parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].NumPar));
                    parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].TipMov));
                    parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].NroRef));
                    parameters.Add(MODGSYB.dbTCamSyForRead(Globales.MODGCON0.VMcds[i].TipCam));
                    parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].NroTOp));
                    parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].IndTOp));
                    parameters.Add(MODGSYB.dblogisy(Globales.MODGCON0.VMcds[i].IntCIT.ToShort()));
                    parameters.Add(MODGSYB.dblogisy(Globales.MODGCON0.VMcds[i].IntCVT.ToShort()));
                    parameters.Add(MODGSYB.dblogisy(Globales.MODGCON0.VMcds[i].intcap.ToShort()));
                    parameters.Add(MODGSYB.dblogisy(Globales.MODGCON0.VMcds[i].IntCTD.ToShort()));
                    parameters.Add(MODGSYB.dblogisy(Globales.MODGCON0.VMcds[i].IntPOS.ToShort()));
                    parameters.Add(MODGSYB.dblogisy(Globales.MODGCON0.VMcds[i].IntCDR.ToShort()));
                    parameters.Add(MODGSYB.dblogisy(Globales.MODGCON0.VMcds[i].McdVig.ToShort()));
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
                        Globales.MESSAGES.Add(new UI_Message()
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
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Informacion,
                    Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                    Title = T_MODGCON0.MsgCon
                });
                throw _ex;
            }
        }

        //Graba el Detalle del Reporte Contable (Sce_Mcd).-
        //Retorno    = True  : Grabación Exitosa.-
        //           = False : Error o Grabación no Exitosa.-
        public static short SyPutn_Mcd(DatosGlobales Globales, UnitOfWorkCext01 unit, string Usuario)
        {
            using (var tracer = new Tracer())
            {
                short _retValue = 0;
                short HayError = 0;
                short i = 0;
                string Que = "";
                int R = 0;

                try
                {
                    HayError = (short)(false ? -1 : 0);

                    //Graba los movimientos de un Reporte Contable.-
                    for (i = 1; i <= (short)VB6Helpers.UBound(Globales.MODGCON0.VMcds); i++)
                    {
                        //Se incluyen estas validaciones anexas.-
                        if (Globales.MODGCON0.VMcds[i].CodMon == 0)
                        {
                            Globales.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Informacion,
                                Text = "Existe un movimiento contable que no tiene Código de Moneda.",
                                Title = T_MODGCON0.MsgCon
                            });

                            return _retValue;
                        }

                        if (string.IsNullOrEmpty(Globales.MODGCON0.VMcds[i].NemMon))
                        {
                            Globales.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Informacion,
                                Text = "Existe un movimiento contable que no tiene Nemónico de Moneda.",
                                Title = T_MODGCON0.MsgCon
                            });

                            return _retValue;
                        }

                        if (Globales.MODGCON0.VMcds[i].mtomcd == 0)
                        {
                            Globales.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Informacion,
                                Text = "Existe un movimiento contable que no tiene un Monto válido.",
                                Title = T_MODGCON0.MsgCon
                            });

                            return _retValue;
                        }

                        if (string.IsNullOrEmpty(Globales.MODGCON0.VMcds[i].NemCta))
                        {
                            Globales.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Informacion,
                                Text = "Existe un movimiento contable que no tiene Nemónico de Cuenta Contable.",
                                Title = T_MODGCON0.MsgCon
                            });

                            return _retValue;
                        }

                        //Genera consulta.-
                        string sp = string.Empty;//probando
                        sp = "sce_mcd_i01_gl01_MS ";



                        List<string> parameters = new List<string>();

                        Que = VB6Helpers.LCase(Que);
                        parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.CodCct));//string
                        parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.CodPro));//string
                        parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.CodEsp));//string
                        parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.CodOfi));//string
                        parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMch.CodOpe));//string
                        parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMch.CodNeg));//int
                        parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMch.CodSec));//int
                        parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].NroRpt));//int
                        parameters.Add(MODGSYB.dbdatesy(Globales.MODGCON0.VMcds[i].FecMov));//string
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Left(Usuario, 3)));//string
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Right(Usuario, 2)));//string
                        parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].NroImp));//int
                        parameters.Add(MODGSYB.dbnumesy(T_MODGCON0.ECC_ANU));//int
                        parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].TipMcd));//string
                        parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].IdnCta));//int
                        parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].NemCta));//string
                        parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].numcta));//string
                        parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].CodMon));//int
                        parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].NemMon));//string
                        //aca
                        parameters.Add(MODGSYB.dbmontoSy(Globales.MODGCON0.VMcds[i].mtomcd));//double

                        parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].cod_dh));//string
                        parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].NumEmb));//int
                        parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].PrtCli));//string
                        parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].IndCli));//int
                        parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].rutcli));//string
                        parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].PrtBco));//string
                        parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].IndBco));//int
                        parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].RutBco));//string
                        parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].SwiBco));//string
                        parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].CodBco));//int
                        parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].numcct));//string
                        parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].numlin));//string
                        parameters.Add(MODGSYB.dbdatesy(Globales.MODGCON0.VMcds[i].FecOri));//string
                        parameters.Add(MODGSYB.dbdatesy(Globales.MODGCON0.VMcds[i].FecVen));//string
                        parameters.Add(MODGSYB.dbdatesy(Globales.MODGCON0.VMcds[i].FecInt));//string
                        parameters.Add(MODGSYB.dblogisy(Globales.MODGCON0.VMcds[i].TasFij.ToShort()));//int

                        parameters.Add(MODGSYB.dbtasaSy(Globales.MODGCON0.VMcds[i].MtoTas));//double

                        parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].OfiDes));//int
                        parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].NumPar));//int
                        parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].TipMov));//int
                        parameters.Add(MODGSYB.dbcharSy(Globales.MODGCON0.VMcds[i].NroRef));//string

                        parameters.Add(MODGSYB.dbTCamSy(Globales.MODGCON0.VMcds[i].TipCam));//double

                        parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].NroTOp));//int
                        parameters.Add(MODGSYB.dbnumesy(Globales.MODGCON0.VMcds[i].IndTOp));//int
                        parameters.Add(MODGSYB.dblogisy(Globales.MODGCON0.VMcds[i].IntCIT.ToShort()));//int
                        parameters.Add(MODGSYB.dblogisy(Globales.MODGCON0.VMcds[i].IntCVT.ToShort()));//int
                        parameters.Add(MODGSYB.dblogisy(Globales.MODGCON0.VMcds[i].intcap.ToShort()));//int
                        parameters.Add(MODGSYB.dblogisy(Globales.MODGCON0.VMcds[i].IntCTD.ToShort()));//int
                        parameters.Add(MODGSYB.dblogisy(Globales.MODGCON0.VMcds[i].IntPOS.ToShort()));//int
                        parameters.Add(MODGSYB.dblogisy(Globales.MODGCON0.VMcds[i].IntCDR.ToShort()));//int
                        parameters.Add(MODGSYB.dblogisy(Globales.MODGCON0.VMcds[i].McdVig.ToShort()));//int
                        parameters.Add(MODGSYB.dbnumesy((Globales.MODCTA.VNotaCreGl.NumFac ?? string.Empty).Replace(".", "")));

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
                            tracer.TraceException("Alerta al grabar XGGL", e);

                            HayError = -1;
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
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error en el sybase al tratar de grabar el detalle del Reporte Contable (Sce_Mcd).",
                        Title = T_MODGCON0.MsgCon
                    });

                    throw _ex;
                }
            }
        }

        // Verifica que la Info. de Bancos del Reporte Contable estén OK.-
        public static int BancosOK(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            T_MODGCON0 MODGCON0 = Globales.MODGCON0;
            int BancosOK = 0;

            int i = 0;

            for (i = 1; i <= MODGCON0.VMcds.GetUpperBound(0); i += 1)
            {
                switch (MODGCON0.VMcds[i].NemCta)
                {
                    case "ACE":
                    case "ACECANJE":
                    case "OPE":
                    case "OPEPEND":
                    case "COE":
                    case "BOE":
                    case "BANCENE":
                        if (MODGCON0.VMcds[i].CodBco == 0)
                        {
                            return BancosOK;
                        }
                        break;
                }

                // Obligaciones
                if(unit.SceRepository.EsObligacion(MODGCON0.VMcds[i].IdnCta))
                {
                    if (MODGCON0.VMcds[i].CodBco == 0)
                    {
                        return BancosOK;
                    }
                    break;
                }

                // Valida Bancos Corresponsales <> 24.-
                if (MODGCON0.VMcds[i].NemCta == "ACE" && MODGCON0.VMcds[i].CodBco == 24)
                {
                    return BancosOK;
                }
                // Valida Bancos Central = 24.-
                if ((MODGCON0.VMcds[i].NemCta == "BOE" || MODGCON0.VMcds[i].NemCta == "BANCENE") && MODGCON0.VMcds[i].CodBco != 24)
                {
                    return BancosOK;
                }
            }
            BancosOK = true.ToInt();


            return BancosOK;
        }

        //Valida que la Contabilidad esté OK.-
        public static short ValidaContab(DatosGlobales Globales, UnitOfWorkCext01 unit, int NroRpt, string fecmov)
        {
            short _retValue = 0;
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
                                string res = reader.GetString(1);
                                tracer.AddToContext("sce_mcd_s11", res);
                                Globales.MESSAGES.Add(new UI_Message()
                                {
                                    Text = res,
                                    Type = TipoMensaje.Error
                                });
                            }
                        }
                        else
                        {
                            _retValue = 0;
                        }
                    }, "sce_mcd_s11", NroRpt.ToString(), fecmov);
                }
                catch (Exception _ex)
                {
                    tracer.AddToContext("sce_mcd_s11", _ex);
                    _retValue = 0;

                }
                return _retValue;
            }
        }
    }
}
