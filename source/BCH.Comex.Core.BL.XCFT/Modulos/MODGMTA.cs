using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Caching;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODGMTA
    {

        public static T_MODGMTA GetMODGMTA() {
            return new T_MODGMTA();
        }

        public static short SyGetn_Imp(T_MODGMTA MODGMTA,UnitOfWorkCext01 unit) {
            short _retValue = -1;
            const string cacheKey = "VImpCache";
            var cache = MemoryCache.Default;
            try
            {
                if (!cache.Contains(cacheKey))
                {
                    var result = unit.SceRepository.EjecutarSP<sce_mta3_s01_MS_Result>("sce_mta3_s01_MS").Select(x => new T_Imp()
                    {
                        CodImp = x.codimp,
                        cta_me = x.cta_me,
                        cta_mn = x.cta_mn,
                        MtoFij = (short)(x.mtofij ? -1 : 0),
                        MtoMax = (double)x.mtomax,
                        MtoMin = (double)x.mtomin,
                        NomImp = x.nomimp,
                        tasmax = (double)x.tasmax,
                        TasMin = (double)x.tasmin
                    }).ToArray();
                    cache.Set(cacheKey, result, DateTimeOffset.Now.AddDays(1));
                    T_MODGMTA.VImpDict = new Dictionary<string, short>();
                    for (short i = 0; i < result.Length; i++)
                    {
                        T_MODGMTA.VImpDict[result[i].CodImp] = i;
                    }
                }
                MODGMTA.VImp = cache[cacheKey] as T_Imp[];
            }
            catch (Exception ex)
            {
                _retValue = 0;
            }
            return _retValue;
        }

        //***************************************************
        // Objetivo  : Lee la Tabla Comisiones (Sce_Mta1).- *
        //***************************************************
        //
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short SyGetn_Com(T_MODGMTA MODGMTA, UnitOfWorkCext01 unit)
        {
            short _retValue = 0;
            try
            {
                MODGMTA.VCom = unit.SceRepository.EjecutarSP<sce_mta1_s06_MS_Result>("sce_mta1_s06_MS").Select(x => new T_Com(false)
                {
                    codsis = x.codsis,
                    codpro = x.codpro,
                    CodEta = x.codeta,
                    MtoFij = (short)(x.mtofij ? -1 : 0),
                    TasMin = (double)x.tasmin,
                    tasmax = (double)x.tasmax,
                    MtoMin = (double)x.mtomin,
                    MtoMax = (double)x.mtomax,
                    FecIni = x.fecini,
                    CodMon = (short)x.codmon,
                    hayiva = (short)(x.hayiva ? -1 : 0),
                    cta_mn = x.cta_mn,
                    cta_me = x.cta_me,
                    RanMin = (double)x.ranmin,
                    RanMax = (double)x.ranmax,
                    CorMta = (double)x.cormat1
                }).ToArray();
                _retValue = -1;
            }
            catch (Exception ex)
            {
                _retValue = 0;
            }
            
            return _retValue;
        }

        //Encuentra una Comisión.-
        // UPGRADE_INFO (#0561): The 'Sis' symbol was defined without an explicit "As" clause.
        // UPGRADE_INFO (#0561): The 'Pro' symbol was defined without an explicit "As" clause.
        public static short Find_VCom(T_MODGMTA MODGMTA, dynamic Sis, dynamic Pro, string Eta)
        {
            short _retValue = 0;
            short i = 0;
            for (i = 0; i <= (short)VB6Helpers.UBound(MODGMTA.VCom); i++)
            {
                // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Sis'. Consider using the GetDefaultMember6 helper method.
                // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Pro'. Consider using the GetDefaultMember6 helper method.
                if (MODGMTA.VCom[i].codsis.Equals(Sis) && MODGMTA.VCom[i].codpro.Equals(Pro) && MODGMTA.VCom[i].CodEta.Equals(Eta))
                {
                    _retValue = i;
                    break;
                }

            }

            return _retValue;
        }

        public static void LlenaDatCob(T_MODGMTA MODGMTA, string LlaCli, string codsis, string codpro, string CodEta, string FecRef, short MonCob, short monnac, short MonMto, double MtoCom, double MtoInt, short numchq)
        {

            MODGMTA.VDatCob.LlaCli = LlaCli;
            MODGMTA.VDatCob.codsis = codsis;
            MODGMTA.VDatCob.codpro = codpro;
            MODGMTA.VDatCob.CodEta = CodEta;
            MODGMTA.VDatCob.FecRef = FecRef;
            MODGMTA.VDatCob.MonCob = MonCob;
            MODGMTA.VDatCob.monnac = monnac;
            MODGMTA.VDatCob.MonMto = MonMto;
            MODGMTA.VDatCob.MtoCom = MtoCom;
            MODGMTA.VDatCob.MtoInt = MtoInt;
            MODGMTA.VDatCob.numchq = numchq;
        }

        /// <summary>
        /// ****************************************************************
        ///                                                                *
        ///  Función   : Cobrar                                            *
        ///                                                                *
        ///  Objetivo  : Realiza proceso de cobro de Comisiones, Gastos,   *
        ///              Intereses e Impuestos                             *
        ///                                                                *
        ///  Retornos  : True, se realiza algún cobro                      *
        ///              false, no hay cobro                               *
        ///                                                                *
        /// ****************************************************************
        /// </summary>
        /// <param name="MODGMTA"></param>
        /// <param name="MODGPYF0"></param>
        /// <param name="MODGTAB0"></param>
        /// <param name="MODGCHQ"></param>
        /// <param name="Mdi_Principal"></param>
        /// <param name="Frm_Ingreso_Valores"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static short Cobrar(T_MODGMTA MODGMTA, T_MODGPYF0 MODGPYF0, T_MODGTAB0 MODGTAB0, T_MODGCHQ MODGCHQ, 
            UI_Mdi_Principal Mdi_Principal,UI_Frm_Ingreso_Valores Frm_Ingreso_Valores, UnitOfWorkCext01 unit)
        {
            short Fin = -1;
            short a = 0;
            short b = 0;
            double ret = 0;
            short j = 0;
            short hay_vdi = 0;
            short i = 0;
            double suma = 0;
            
            Fin = (short)VB6Helpers.UBound(MODGMTA.VCon);

            if (Fin < 0)
            {
                //MODGMTA.VCon = new T_Con[1];
                VB6Helpers.RedimPreserve(ref MODGMTA.VCon, 0, 0);   
            }

            //Ver Conceptos a cobrar
            if (SyGet_Cob(MODGMTA, unit) != 0)
            {
                if (MODGMTA.VgCob.EsComi != 0)
                {
                    if (~CalculaCom(MODGMTA, MODGTAB0, MODGPYF0, MODGCHQ, Mdi_Principal, Frm_Ingreso_Valores, unit) != 0)
                    {
                        return 0;
                    }
                }

                if (MODGMTA.VgCob.EsGast != 0)
                {
                    if (MODGMTA.VgCob.EsGast != 0)
                    {
                        if (~CalculaGas(MODGMTA,MODGTAB0,MODGPYF0, Mdi_Principal, Frm_Ingreso_Valores,unit) != 0)
                        {
                            return 0;
                        }
                    }

                }

                if (MODGMTA.VgCob.EsImpu != 0)
                {
                    if (SyGet_Imp(MODGMTA, MODGMTA.VgCob.DetImp) != 0)
                    {

                        //IVA.-
                        if (VB6Helpers.Instr(MODGMTA.VgCob.DetImp, "IVA") != 0)
                        {
                            for (a = 1; a <= (short)VB6Helpers.UBound(MODGMTA.VCon); a++)
                            {
                                if (MODGMTA.VCon[a].hayiva != 0)
                                {
                                    b = CalculaIva(MODGMTA,MODGPYF0,unit, MODGMTA.VCon[a].MtoCon, MODGMTA.VCon[a].MonCon, a);
                                }

                            }

                        }

                        //REI.-
                        if (VB6Helpers.Instr(MODGMTA.VgCob.DetImp, "REI") != 0)
                        {
                            ret = CalculaRei(MODGMTA, MODGTAB0, MODGPYF0, Frm_Ingreso_Valores, unit, MODGMTA.VDatCob.MtoInt, "REI");
                        }

                        //REC.-
                        if (VB6Helpers.Instr(MODGMTA.VgCob.DetImp, "REC") != 0)
                        {
                            ret = CalculaRei(MODGMTA, MODGTAB0, MODGPYF0, Frm_Ingreso_Valores, unit, MODGMTA.VDatCob.MtoInt, "REC");
                        }

                        //RIC
                        if (VB6Helpers.Instr(MODGMTA.VgCob.DetImp, "RIC") != 0)
                        {
                            ret = CalculaRic(MODGMTA, MODGTAB0, MODGPYF0, Frm_Ingreso_Valores, unit, MODGMTA.VDatCob.MtoInt);
                        }

                        //SCH.-
                        if ((VB6Helpers.Instr(MODGMTA.VgCob.DetImp, "SCH") & (MODGMTA.VDatCob.numchq != 0 ? -1 : 0)) != 0)
                        {
                            ret = CalculaSch(MODGMTA, MODGTAB0, MODGPYF0, Frm_Ingreso_Valores, unit, MODGMTA.VDatCob.numchq);
                        }

                        //VDI.-
                        if (VB6Helpers.Instr(MODGMTA.VgCob.DetImp, "VDI") != 0)
                        {

                            hay_vdi = (short)VB6Helpers.UBound(MODGMTA.VVdi);

                            if (hay_vdi != 0)
                            {
                                a = FindImp(MODGMTA,unit, "VDI");
                                for (i = 0; i <= (short)VB6Helpers.UBound(MODGMTA.VVdi); i++)
                                {
                                    MODGMTA.VVdi[i].MtoVdi = CalculaVdi(MODGMTA, MODGTAB0, Frm_Ingreso_Valores, unit, i, a);
                                    suma += MODGMTA.VVdi[i].MtoVdi;
                                }

                                //crear un nuevo VCon, guardando el valor de suma
                                //Se llena la estructura final
                                var aux = (new List<T_Con>(MODGMTA.VCon));
                                aux.Add(new T_Con());
                                MODGMTA.VCon = aux.ToArray();
                                MODGMTA.VCon[j].MtoCon = VB6Helpers.Val(BCH.Comex.Core.BL.XCFT.Modulos.MODGPYF0.unformat(MODGPYF0,VB6Helpers.Format(VB6Helpers.CStr(suma), "0.00")));
                                MODGMTA.VCon[j].glscon = MODGPYF1.Minuscula(MODGMTA.VImp[a].NomImp);
                                if (MODGMTA.VDatCob.MonCob != MODGMTA.VDatCob.monnac)
                                {
                                    MODGMTA.VCon[j].NemCta = MODGMTA.VImp[a].cta_me;
                                }
                                else
                                {
                                    MODGMTA.VCon[j].NemCta = MODGMTA.VImp[a].cta_mn;
                                }

                                MODGMTA.VCon[j].MonCon = MODGMTA.VDatCob.MonCob;
                                MODGMTA.VCon[j].tipcon = T_MODGMTA.EsVdi;
                                MODGMTA.VCon[j].MtoCob = MODGMTA.VCon[j].MtoCon;
                                MODGMTA.VCon[j].FecCon = DateTime.Now.ToString("dd/MM/yyyy");
                                MODGMTA.VCon[j].gdacon = (short)(false ? -1 : 0);
                                MODGMTA.VCon[j].Estado = MODGMTA.RegNva;
                                MODGMTA.VCon[j].corcon = j;
                            }

                        }

                        //PGI.- Impuesto al pagaré con tasa variable
                        if (VB6Helpers.Instr(MODGMTA.VgCob.DetImp, "PGI") != 0)
                        {
                            if (VB6Helpers.StrComp(VB6Helpers.Format(VB6Helpers.CStr(MODGMTA.VPgi.MtoPag), T_MODGPYF1.Fto_Comparar), VB6Helpers.Format("0", T_MODGPYF1.Fto_Comparar)) > 0)
                            {
                                a = FindImp(MODGMTA,unit,"PGI");
                                suma = CalculaPgi(MODGMTA, MODGTAB0, Frm_Ingreso_Valores, unit, a);
                                //crear un nuevo VCon, guardando el valor de suma
                                //Se llena la estructura final
                                var aux = (new List<T_Con>(MODGMTA.VCon));
                                aux.Add(new T_Con());
                                MODGMTA.VCon = aux.ToArray();
                                MODGMTA.VCon[j].MtoCon = VB6Helpers.Val(BCH.Comex.Core.BL.XCFT.Modulos.MODGPYF0.unformat(MODGPYF0,VB6Helpers.Format(VB6Helpers.CStr(suma), "0.00")));
                                MODGMTA.VCon[j].glscon = MODGPYF1.Minuscula(MODGMTA.VImp[a].NomImp);
                                if (MODGMTA.VDatCob.MonCob != MODGMTA.VDatCob.monnac)
                                {
                                    MODGMTA.VCon[j].NemCta = MODGMTA.VImp[a].cta_me;
                                }
                                else
                                {
                                    MODGMTA.VCon[j].NemCta = MODGMTA.VImp[a].cta_mn;
                                }

                                MODGMTA.VCon[j].MonCon = MODGMTA.VDatCob.MonCob;
                                MODGMTA.VCon[j].tipcon = T_MODGMTA.EsPgi;
                                MODGMTA.VCon[j].MtoCob = MODGMTA.VCon[j].MtoCon;
                                MODGMTA.VCon[j].FecCon = DateTime.Now.ToString("dd/MM/yyyy");
                                MODGMTA.VCon[j].gdacon = (short)(false ? -1 : 0);
                                MODGMTA.VCon[j].Estado = MODGMTA.RegNva;
                                MODGMTA.VCon[j].corcon = j;
                            }

                        }

                        //PGF.- Impuesto al pagaré con tasa fija
                        if (VB6Helpers.Instr(MODGMTA.VgCob.DetImp, "PGF") != 0)
                        {
                            if (VB6Helpers.StrComp(VB6Helpers.Format(VB6Helpers.CStr(MODGMTA.VPgf.MtoPag), T_MODGPYF1.Fto_Comparar), VB6Helpers.Format("0", T_MODGPYF1.Fto_Comparar)) > 0)
                            {
                                a = FindImp(MODGMTA,unit, "PGF");
                                suma = CalculaPgf(MODGMTA, MODGTAB0, Frm_Ingreso_Valores, unit, a);
                                //crear un nuevo VCon, guardando el valor de suma
                                //Se llena la estructura final
                                var aux = (new List<T_Con>(MODGMTA.VCon));
                                aux.Add(new T_Con());
                                MODGMTA.VCon = aux.ToArray();
                                MODGMTA.VCon[j].MtoCon = VB6Helpers.Val(BCH.Comex.Core.BL.XCFT.Modulos.MODGPYF0.unformat(MODGPYF0,VB6Helpers.Format(VB6Helpers.CStr(suma), "0.00")));
                                MODGMTA.VCon[j].glscon = MODGPYF1.Minuscula(MODGMTA.VImp[a].NomImp);
                                if (MODGMTA.VDatCob.MonCob != MODGMTA.VDatCob.monnac)
                                {
                                    MODGMTA.VCon[j].NemCta = MODGMTA.VImp[a].cta_me;
                                }
                                else
                                {
                                    MODGMTA.VCon[j].NemCta = MODGMTA.VImp[a].cta_mn;
                                }

                                MODGMTA.VCon[j].MonCon = MODGMTA.VDatCob.MonCob;
                                MODGMTA.VCon[j].tipcon = T_MODGMTA.EsPgf;
                                MODGMTA.VCon[j].MtoCob = MODGMTA.VCon[j].MtoCon;
                                MODGMTA.VCon[j].FecCon = DateTime.Now.ToString("dd/MM/yyyy");
                                MODGMTA.VCon[j].gdacon = (short)(false ? -1 : 0);
                                MODGMTA.VCon[j].Estado = MODGMTA.RegNva;
                                MODGMTA.VCon[j].corcon = j;
                            }

                        }

                    }

                }

                if (VB6Helpers.UBound(MODGMTA.VCon) > 0)
                {
                    return (short)(true ? -1 : 0);
                }
            }

            return 0;
        }

        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method. *1
        public static short FindImp(T_MODGMTA MODGMTA, UnitOfWorkCext01 unit, string Cod)
        {
            short _retValue = 0;
            short k = 0;
            short a = 0;

            k = (short)VB6Helpers.UBound(MODGMTA.VImp);

            if (k == -1)
            {
                k = SyGetn_Imp(MODGMTA,unit);
            }
            if (T_MODGMTA.VImpDict.ContainsKey(Cod))
            {
                _retValue = T_MODGMTA.VImpDict[Cod];
            }
            //for (a = 1; a <= (short)VB6Helpers.UBound(MODGMTA.VImp); a++)
            //{
            //    if (MODGMTA.VImp[a].CodImp.Equals(Cod))
            //    {
            //        _retValue = a;
            //        break;
            //    }

            //}

            return _retValue;
        }

        //***************************************************
        // Objetivo : Lee la tabla Sce_Mta0 para determinar *
        //            que concepos cobrar para un sistema,  *
        //            producto, etapa.-                     *
        //***************************************************
        public static short SyGet_Cob(T_MODGMTA MODGMTA,UnitOfWorkCext01 unit)
        {
            short _retValue = 0;
            try
            {
                var result = unit.SceRepository.sce_mta0_s01_MS(MODGMTA.VDatCob.codsis, MODGMTA.VDatCob.codpro, MODGMTA.VDatCob.CodEta);
                if (result != null)
                {
                    MODGMTA.VgCob = new T_Cob()
                        {
                            codsis = result.codsis,
                            codpro = result.codpro,
                            CodEta = result.codeta,
                            EsComi = (short)(result.escomi ? -1 : 0),
                            EsGast = (short)(result.esgast ? -1 : 0),
                            EsInte = (short)(result.esinte ? -1 : 0),
                            EsImpu = (short)(result.esimpu ? -1 : 0),
                            EsOtro = (short)(result.esotro ? -1 : 0),
                            DetImp = result.detimp,
                            DetOtr = result.detotr
                        };
                }
                
                _retValue = -1;
            }
            catch (Exception ex)
            {
                _retValue = 0;
            }
            return _retValue;
        }


        //************************************************
        // Objetivo  : Calcula Comisiones                *
        //************************************************
        public static short CalculaCom(T_MODGMTA MODGMTA, T_MODGTAB0 MODGTAB0, T_MODGPYF0 MODGPYF0,T_MODGCHQ MODGCHQ, UI_Mdi_Principal Mdi_Principal, UI_Frm_Ingreso_Valores Frm_Ingreso_Valores,UnitOfWorkCext01 unit)
        {
            string hoy = DateTime.Now.ToString("dd/MM/yyyy");
            double mtomt = 0;
            short a = 0;
            double mtofin = 0;
            string Glosa = "";
            short j = 0;
            double a_cob = 0;
            string cuenta_com = "";

            //Se convierte el monto sobre el cual se hará el cálculo
            //a la moneda del manual de tarifas

            //A DOLAR
            //MODGMTA.Vgcom.CodMon = 11;//Por mientras
            if (MODGMTA.Vgcom.CodMon == T_MODGTAB0.MndDol)
            {
                if (MODGMTA.VDatCob.MonMto != T_MODGTAB0.MndDol)
                {
                    if (MODGMTA.VDatCob.MonMto != MODGMTA.VDatCob.monnac)
                    {
                        if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores,unit, hoy, MODGMTA.VDatCob.MonMto) != 0)
                        {
                            return 0;
                        }
                        mtomt = MODGMTA.VDatCob.MtoCom / MODGTAB0.VVmd.VmdPrd;
                    }
                    else
                    {
                        if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores, unit, hoy, T_MODGTAB0.MndDol) != 0)
                        {
                            return 0;
                        }
                        mtomt = MODGMTA.VDatCob.MtoCom / MODGTAB0.VVmd.VmdObs;
                    }

                }
                else
                {
                    mtomt = MODGMTA.VDatCob.MtoCom;
                }

            }
            else
            {
                //A OTRA MONEDA DE DOLAR
                if (MODGMTA.VDatCob.MonMto != MODGMTA.Vgcom.CodMon)
                {
                    if (MODGMTA.VDatCob.MonMto != MODGMTA.VDatCob.monnac)
                    {
                        if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores, unit, hoy, MODGMTA.VDatCob.MonMto) != 0)
                        {
                            return 0;
                        }
                        mtomt = MODGMTA.VDatCob.MtoCom / MODGTAB0.VVmd.VmdPrd;
                    }
                    else
                    {
                        if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores, unit, hoy, MODGMTA.Vgcom.CodMon) != 0)
                        {
                            return 0;
                        }
                        mtomt = MODGMTA.VDatCob.MtoCom / MODGTAB0.VVmd.VmdObs;
                    }

                }
                else
                {
                    mtomt = MODGMTA.VDatCob.MtoCom;
                }

            }

            //Se leen los datos desde el manual de tarifas
            if (~SyGet_Com(MODGMTA,MODGCHQ,Mdi_Principal, mtomt) != 0)
            {
                return 0;
            }

            //Se consulta si el cliente tiene tasas especiales
            a = VB6Helpers.CShort(SyGet_TCom(MODGMTA, Mdi_Principal, unit, mtomt));

            //Se efectúa el cálculo de las comisiones
            if (MODGMTA.Vgcom.MtoFij == 1)
            {
                a_cob = MODGMTA.Vgcom.MtoMin;
                Glosa = "Nuestra Comisión Fija";
            }
            else
            {
                a_cob = mtomt * (MODGMTA.Vgcom.tasmax / 100);
                if (VB6Helpers.StrComp(VB6Helpers.Format(VB6Helpers.CStr(a_cob), T_MODGPYF1.Fto_Comparar), VB6Helpers.Format(VB6Helpers.CStr(MODGMTA.Vgcom.MtoMin), T_MODGPYF1.Fto_Comparar)) < 0)
                {
                    a_cob = MODGMTA.Vgcom.MtoMin;
                    Glosa = "Nuestra Comisión Mínima";
                }
                else
                {
                    if (VB6Helpers.StrComp(VB6Helpers.Format(VB6Helpers.CStr(a_cob), T_MODGPYF1.Fto_Comparar), VB6Helpers.Format(VB6Helpers.CStr(MODGMTA.Vgcom.MtoMax), T_MODGPYF1.Fto_Comparar)) > 0)
                    {
                        a_cob = MODGMTA.Vgcom.MtoMax;
                        Glosa = "Nuestra Comisión Máxima";
                    }
                    else
                    {
                        Glosa = "Nuestra Comisión del " + VB6Helpers.Trim(BCH.Comex.Core.BL.XCFT.Modulos.MODGPYF0.forma(MODGMTA.Vgcom.tasmax, "##0.###0")) + "%";
                    }

                }

            }

            //Se convierte el monto a cobrar que está  en moneda del MT a la moneda pedida */
            if (MODGMTA.VDatCob.MonCob != MODGMTA.Vgcom.CodMon)
            {
                if (MODGMTA.VDatCob.MonCob != MODGMTA.VDatCob.monnac)
                {
                    cuenta_com = MODGMTA.Vgcom.cta_me;
                    if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores, unit, hoy, MODGMTA.VDatCob.MonCob) != 0)
                    {
                        return 0;
                    }
                    mtofin = a_cob * MODGTAB0.VVmd.VmdPrd;
                }
                else
                {
                    if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores, unit, hoy, MODGMTA.Vgcom.CodMon) != 0)
                    {
                        return 0;
                    }
                    mtofin = a_cob * MODGTAB0.VVmd.VmdObs;
                    cuenta_com = MODGMTA.Vgcom.cta_mn;
                }

            }
            else
            {
                mtofin = a_cob;
                if (MODGMTA.VDatCob.MonCob == MODGMTA.VDatCob.monnac)
                {
                    cuenta_com = MODGMTA.Vgcom.cta_mn;
                }
                else
                {
                    cuenta_com = MODGMTA.Vgcom.cta_me;
                }

            }

            //Se llena la estructura final
            var aux = (new List<T_Con>(MODGMTA.VCon));
            aux.Add(new T_Con());
            MODGMTA.VCon = aux.ToArray();
            j = (short)VB6Helpers.UBound(MODGMTA.VCon);

            MODGMTA.VCon[j].MonCon = MODGMTA.VDatCob.MonCob;
            MODGMTA.VCon[j].glscon = Glosa;
            MODGMTA.VCon[j].MtoCon = Math.Round(mtofin, 2);//VB6Helpers.Val(BCH.Comex.Core.BL.XCFT.Modulos.MODGPYF0.unformat(MODGPYF0, VB6Helpers.Format(VB6Helpers.CStr(mtofin), "0.00")));
            MODGMTA.VCon[j].NemCta = cuenta_com;
            MODGMTA.VCon[j].hayiva = MODGMTA.Vgcom.hayiva;
            MODGMTA.VCon[j].tipcon = T_MODGMTA.EsCom;
            MODGMTA.VCon[j].MtoCob = MODGMTA.VCon[j].MtoCon;
            MODGMTA.VCon[j].FecCon = DateTime.Now.ToString("dd/MM/yyyy");
            MODGMTA.VCon[j].gdacon = (short)(false ? -1 : 0);
            MODGMTA.VCon[j].Estado = MODGMTA.RegNva;
            MODGMTA.VCon[j].corcon = j;

            return (short)(true ? -1 : 0);
        }

        //***********************************************************
        //                                                          *
        // Función  : Get_VPar()                                    *
        //                                                          *
        // Objetivo : Busca si ya se ha leído o pedido manualmente  *
        //            el valor de una moneda.                       *
        //                                                          *
        // Retornos : Si encuentra datos retorna TRUE.              *
        //            Sino, los pide manualmente y si se ingresan   *
        //            retorna True.                                 *
        //            Sino retorna FALSE                            *
        //                                                          *
        //***********************************************************
        //
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short Get_VPar(T_MODGMTA MODGMTA, T_MODGTAB0 MODGTAB0, UI_Frm_Ingreso_Valores Frm_Ingreso_Valores,UnitOfWorkCext01 unit, string Fecha, short CodMon)
        {
            short hay_dat = (short)(false ? -1 : 0);
            short Fin = 0;
            short j = 0;
            if (~Find_VPar(MODGMTA, MODGTAB0, Fecha, CodMon) != 0)
            {
                if (BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.SyGet_Vmd(MODGTAB0,unit, Fecha, CodMon) != 0)
                {
                    //MODGTAB0.VVmd.VmdObs = Format.StringToDouble(VB6Helpers.Format(VB6Helpers.CStr(MODGTAB0.VVmd.VmdObs), "0.0000"));
                    hay_dat = (short)(true ? -1 : 0);
                }

            }
            else
            {
                return (short)(true ? -1 : 0);
            }

            //Pedir datos manuales
            if (~hay_dat != 0)
            {
                hay_dat = Put_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores,unit,Fecha, CodMon);
            }

            if (hay_dat != 0)
            {
                Fin = -1;
                
                Fin = (short)VB6Helpers.UBound(MODGMTA.VPar);
                if (Fin < 0)
                {
                    MODGMTA.VPar = new T_Par[1] {new T_Par()};
                }

                j = (short)(VB6Helpers.UBound(MODGMTA.VPar) + 1);
                VB6Helpers.RedimPreserve(ref MODGMTA.VPar, 0, j);

                //@estanislao: comento esto, no esta en el codigo original
                //var aux = (new List<T_Con>(MODGMTA.VCon));
                //aux.Add(new T_Con());
                //MODGMTA.VCon = aux.ToArray();

                MODGMTA.VPar[j] = new T_Par();
                MODGMTA.VPar[j].CodMon = CodMon;
                MODGMTA.VPar[j].FecVmd = Fecha;
                MODGMTA.VPar[j].VmdPar = MODGTAB0.VVmd.VmdPrd;
                MODGMTA.VPar[j].VmdObs = MODGTAB0.VVmd.VmdObs;
            }

            return hay_dat;
        }

        //*******************************************************************
        // Objetivo  : Pide el ingreso manual de los valores para la moneda *
        //             Si éstos son correctamente ingresados retorna TRUE   *
        //             sino retorna False                                   *
        //*******************************************************************
        public static short Put_VPar(T_MODGMTA MODGMTA, T_MODGTAB0 MODGTAB0, UI_Frm_Ingreso_Valores Frm_Ingreso_Valores,UnitOfWorkCext01 unit, string Fecha, short CodMon)
        {
            if(Frm_Ingreso_Valores != null)
            {
                short ind = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, CodMon);
                Frm_Ingreso_Valores.Moneda.Text = VB6Helpers.LCase(MODGTAB0.VMnd[ind].Mnd_MndNom);
                Frm_Ingreso_Valores.Fecha.Text = Fecha;
                if(MODGTAB0.VVmd.VmdPrd != 0 && MODGTAB0.VVmd.VmdObs != 0)
                {
                    return (short)(true ? -1 : 0);
                }
            }
            else
            {
                //el forulario es null, pero si estos valores ya estan cargados, entonces estamos OK porque ya se mostro el form de paridad y tengo los valores que me interesan
                if (MODGTAB0.VVmd.VmdPrd != 0 && MODGTAB0.VVmd.VmdObs != 0)
                {
                    return (short)(true ? -1 : 0);
                }
            }
            return 0;
        }

        //************************************************
        // Objetivo : Busca paridad                      *
        //************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short Find_VPar(T_MODGMTA MODGMTA, T_MODGTAB0 MODGTAB0, string Fecha, short CodMon)
        {
            short Fin = -1;
            short i = 0;            
            Fin = (short)VB6Helpers.UBound(MODGMTA.VPar);

            for (i = 0; i <= (short)Fin; i++)
            {
                if(!string.IsNullOrEmpty(MODGMTA.VPar[i].FecVmd) && !string.IsNullOrEmpty(Fecha))
                {
                    if (VB6Helpers.Format(MODGMTA.VPar[i].FecVmd, "yyyymmdd") == VB6Helpers.Format(Fecha, "yyyymmdd") && MODGMTA.VPar[i].CodMon == CodMon)
                    {
                        MODGTAB0.VVmd.VmdPrd = MODGMTA.VPar[i].VmdPar;
                        MODGTAB0.VVmd.VmdObs = MODGMTA.VPar[i].VmdObs;
                        return (short)(true ? -1 : 0);
                    }

                }

            }

            return 0;
        }


        //**************************************************
        // Objetivo  : Lee la tabla Sce_Mta1 para obtener  *
        //             datos de la cmisión a cobrar        *
        //**************************************************
        public static short SyGet_Com(T_MODGMTA MODGMTA, T_MODGCHQ MODGCHQ, UI_Mdi_Principal Mdi_Principal, double monto)
        {
            short _retValue;
            try
            {
                using (var unit = new UnitOfWorkCext01())
                {
                    var aux = unit.SceRepository.EjecutarSP<sce_mta1_s07_MS_Result>("sce_mta1_s07_MS", 
                        MODGMTA.VDatCob.codsis, 
                        MODGMTA.VDatCob.codpro, 
                        MODGMTA.VDatCob.CodEta, 
                        MODGSYB.dbdatesy(VB6Helpers.Format(VB6Helpers.CStr(MODGMTA.VDatCob.FecRef), "dd/MM/yyyy")));//2015-09.15 --> Cambio por error de conversión.
                    
                    //Buscar el monto entre los rangos mínimos y máximo.-
                    MODGCHQ.Indice = (short)aux.FindIndex(x => VB6Helpers.StrComp(VB6Helpers.Format(VB6Helpers.CStr(monto), T_MODGPYF1.Fto_Comparar), VB6Helpers.Format(VB6Helpers.CStr(x.ranmin), T_MODGPYF1.Fto_Comparar)) >= 0 && VB6Helpers.StrComp(VB6Helpers.Format(VB6Helpers.CStr(monto), T_MODGPYF1.Fto_Comparar), VB6Helpers.Format(VB6Helpers.CStr(x.ranmax), T_MODGPYF1.Fto_Comparar)) <= 0);
                    var VCom_aux = aux.ToArray();
                    MODGMTA.Vgcom.MtoFij = (short)(VCom_aux[MODGCHQ.Indice].mtofij ? -1 : 0);
                    MODGMTA.Vgcom.TasMin = (double)Math.Round(VCom_aux[MODGCHQ.Indice].tasmin,2);
                    MODGMTA.Vgcom.tasmax = (double)Math.Round(VCom_aux[MODGCHQ.Indice].tasmax,2);
                    MODGMTA.Vgcom.MtoMin = (double)Math.Round(VCom_aux[MODGCHQ.Indice].mtomin,2);
                    MODGMTA.Vgcom.MtoMax = (double)Math.Round(VCom_aux[MODGCHQ.Indice].mtomax,2);
                    MODGMTA.Vgcom.CodMon = (short)VCom_aux[MODGCHQ.Indice].codmon;
                    MODGMTA.Vgcom.hayiva = (short)(VCom_aux[MODGCHQ.Indice].hayiva ? -1 : 0);
                    MODGMTA.Vgcom.cta_mn = VCom_aux[MODGCHQ.Indice].cta_mn;
                    MODGMTA.Vgcom.cta_me = VCom_aux[MODGCHQ.Indice].cta_me;
                    MODGMTA.Vgcom.RanMin = (double)VCom_aux[MODGCHQ.Indice].mtomin;
                    MODGMTA.Vgcom.RanMax = (double)VCom_aux[MODGCHQ.Indice].ranmax;
                }
                _retValue = -1;
            }
            catch (Exception ex)
            {
                Mdi_Principal.MESSAGES.Add(new UI_Message() { Title = "Error", Text = "Se ha producido un error al tratar de calcular las comisiones.", Type = TipoMensaje.Error });
                _retValue = 0;
            }
            return _retValue;
        }


        //*************************************************
        // Objetivo : Lee la tabla Sce_TCom para obtener  *
        //            datos de la tasa especial           *
        //*************************************************
        // UPGRADE_INFO (#0561): The 'SyGet_TCom' symbol was defined without an explicit "As" clause.
        public static dynamic SyGet_TCom(T_MODGMTA MODGMTA, UI_Mdi_Principal Mdi_Principal,UnitOfWorkCext01 unit, double monto)
        {
            dynamic _retValue = null;
            try
            {
                
                var resp = unit.SceRepository.Sce_Tcom_S03_MS(MODGMTA.VDatCob.LlaCli, MODGMTA.VDatCob.codsis, MODGMTA.VDatCob.codpro, MODGMTA.VDatCob.CodEta, DateTime.Parse(MODGMTA.VDatCob.FecRef), monto);
                if (resp != null) {
                    if (resp.tasaesp.HasValue && resp.tasaesp.Value)
                    {
                        if (resp.manual.HasValue && !resp.manual.Value)
                        {
                            MODGMTA.Vgcom.MtoFij =  Convert.ToInt16(resp.MtoFij);
                            MODGMTA.Vgcom.tasmax = resp.tasmax.Value;
                            //Esto se replica igual que en legacy
                            MODGMTA.Vgcom.TasMin = MODGMTA.Vgcom.tasmax;
                            //esto se replica igual que en legacy
                            MODGMTA.Vgcom.MtoMin = resp.TasMin.Value;
                            MODGMTA.Vgcom.MtoMax = resp.MtoMax.Value;
                        }
                    }
                }
                _retValue = true;
                
            }
            catch (Exception ex)
            {
                Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "Se ha producido un error al tratar de leer las tasas especiales del cliente." });
            }
            return _retValue;
        }


        //**********************************************
        // Objetivo : Calcula Gastos                   *
        //**********************************************
        public static short CalculaGas(T_MODGMTA MODGMTA, T_MODGTAB0 MODGTAB0, T_MODGPYF0 MODGPYF0, UI_Mdi_Principal Mdi_Principal, UI_Frm_Ingreso_Valores Frm_Ingreso_Valores,UnitOfWorkCext01 unit)
        {
            string hoy = "";
            short a = 0;
            double a_cob = 0;
            string Glosa = "";
            string cuenta_com = "";
            double mtofin = 0;
            short j = 0;
            //Se leen los datos desde el manual de tarifas
            if (~SyGet_Gas(MODGMTA,unit) != 0)
            {
                return 0;
            }

            hoy = DateTime.Now.ToString("dd/MM/yyyy");

            //Se consulta si el cliente tiene tasas especiales
            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'SyGet_TGas()'. Consider using the GetDefaultMember6 helper method.
            a = VB6Helpers.CShort(SyGet_TGas(MODGMTA,Mdi_Principal,unit));

            //Se efectúa el cálculo de las comisiones
            a_cob = MODGMTA.VgGas.MtoMax;
            Glosa = "Comisión de Gastos";

            //Se convierte el monto a cobrar que está  en moneda del MT a la moneda pedida */
            if (MODGMTA.VDatCob.MonCob != MODGMTA.VgGas.CodMon)
            {
                if (~Get_VPar(MODGMTA,MODGTAB0,Frm_Ingreso_Valores, unit, hoy, MODGMTA.VgGas.CodMon) != 0)
                {
                    return 0;
                }
                mtofin = a_cob * MODGTAB0.VVmd.VmdObs;
                if (MODGMTA.VDatCob.MonCob != MODGMTA.VDatCob.monnac)
                {
                    cuenta_com = MODGMTA.VgGas.cta_me;
                    if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores,unit, hoy, MODGMTA.VDatCob.MonCob) != 0)
                    {
                        return 0;
                    }
                    mtofin /= MODGTAB0.VVmd.VmdObs;
                }
                else
                {
                    cuenta_com = MODGMTA.VgGas.cta_mn;
                }

            }
            else
            {
                mtofin = a_cob;
                if (MODGMTA.VDatCob.MonCob == MODGMTA.VDatCob.monnac)
                {
                    cuenta_com = MODGMTA.VgGas.cta_mn;
                }
                else
                {
                    cuenta_com = MODGMTA.VgGas.cta_me;
                }

            }

            //Se llena la estructura final
            j = (short)(VB6Helpers.UBound(MODGMTA.VCon) + 1);
            var aux = new List<T_Con>(MODGMTA.VCon);
            aux.Add(new T_Con());
            MODGMTA.VCon = aux.ToArray();

            MODGMTA.VCon[j].MonCon = MODGMTA.VDatCob.MonCob;
            MODGMTA.VCon[j].glscon = Glosa;
            MODGMTA.VCon[j].MtoCon = Math.Round(mtofin, 2);//VB6Helpers.Val(BCH.Comex.Core.BL.XCFT.Modulos.MODGPYF0.unformat(MODGPYF0, VB6Helpers.Format(VB6Helpers.CStr(mtofin), "0.00")));
            MODGMTA.VCon[j].NemCta = cuenta_com;
            MODGMTA.VCon[j].hayiva = MODGMTA.VgGas.hayiva;
            MODGMTA.VCon[j].tipcon = T_MODGMTA.EsGas;
            MODGMTA.VCon[j].MtoCob = MODGMTA.VCon[j].MtoCon;
            MODGMTA.VCon[j].FecCon = DateTime.Now.ToString("dd/MM/yyyy");
            MODGMTA.VCon[j].gdacon = (short)(false ? -1 : 0);
            MODGMTA.VCon[j].Estado = MODGMTA.RegNva;
            MODGMTA.VCon[j].corcon = j;

            return (short)(true ? -1 : 0);
        }


        //*********************************************************************
        // Objetivo : Leer monto de la tabla Sce_Mta2, llenar la estructura   *
        //            VgGas con el resultado del Select                       *
        //*********************************************************************
        public static short SyGet_Gas(T_MODGMTA MODGMTA, UnitOfWorkCext01 unit)
        {
            short _retValue = 0;
            try
            {
                
                MODGMTA.VgGas = unit.SceRepository.EjecutarSP<sce_mta2_s03_MS_Result>("sce_mta2_s03_MS", MODGMTA.VDatCob.codsis, MODGMTA.VDatCob.codpro, MODGMTA.VDatCob.CodEta).Select(x => new T_Gas()
                {
                    codsis = x.codsis,
                    codpro = x.codpro,
                    CodEta = x.codeta,
                    MtoMin = (double)Math.Round(x.mtomin,2),
                    MtoMax = (double)Math.Round(x.mtomax,2),
                    CodMon = (short)x.codmon,
                    hayiva = (short)(x.hayiva ? -1 : 0),
                    cta_mn = x.cta_mn,
                    cta_me = x.cta_me
                }).First();
                _retValue = -1;
            }
            catch (Exception ex)
            {
                _retValue = 0;
            }
            return _retValue;
        }

        
        //*********************************************************************
        // Objetivo : Ver si el cliente tiene tasa especial, si es así se     *
        //            reemplazan los valos de la estructura VGgas             *
        //*********************************************************************
        // UPGRADE_INFO (#0561): The 'SyGet_TGas' symbol was defined without an explicit "As" clause.
        public static dynamic SyGet_TGas(T_MODGMTA MODGMTA, UI_Mdi_Principal Mdi_Principal,UnitOfWorkCext01 unit)
        {
            dynamic _retValue = null;
            try
            {
                //TODO: EMILIANO
                unit.SceRepository.ReadQuerySP((DbDataReader x) =>
                {
                    if (x.Read())
                    {
                        if (x.GetBoolean(0))
                        {
                            MODGMTA.VgGas.MtoMin = (double)x.GetFloat(1);
                            MODGMTA.VgGas.MtoMax = Math.Round(MODGMTA.VgGas.MtoMin,2);
                        }
                    }
                }, "sce_tgas_s03_MS", MODGMTA.VDatCob.LlaCli, MODGMTA.VDatCob.codsis, MODGMTA.VDatCob.codpro, MODGMTA.VDatCob.CodEta);                
                _retValue = true;
            }
            catch (Exception ex)
            {
                Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Text = "Se ha producido un error al tratar de leer las tasas especiales del cliente.",
                    Type = TipoMensaje.Error
                });
            }
            return _retValue;
        }


        //*********************************************************************
        // Objetivo : Traer los impuestos corespondientes                     *
        //*********************************************************************
        public static short SyGet_Imp(T_MODGMTA MODGMTA, string DetImp)
        {
            short _retValue = 0;
            short Con = 1;
            int sw = 0;
            string s = "";
            List<T_Imp> Aux = new List<T_Imp>();
            try
            {
                using (var unit = new UnitOfWorkCext01())
                {
                    while ((sw) == 0)
                    {
                        s = MODGPYF0.copiardestring(DetImp, ";", Con);
                        if(!string.IsNullOrWhiteSpace(s))
                        {
                            T_Imp AuxVImp = unit.SceRepository.EjecutarSP<sce_mta3_s03_MS_Result>("sce_mta3_s03_MS", s).Select(
                            x => new T_Imp()
                            {
                                CodImp = x.codimp,
                                NomImp = x.nomimp,
                                MtoFij = (short)(x.mtofij ? -1 : 0),
                                TasMin = (double)Math.Round(x.tasmin,2),
                                tasmax = (double)Math.Round( x.tasmax,2),
                                MtoMin = (double)Math.Round(x.mtomin,2),
                                MtoMax = (double)Math.Round(x.mtomax,2),
                                cta_mn = x.cta_mn,
                                cta_me = x.cta_me
                            }).First();
                            Aux.Add(AuxVImp);
                            Con++;
                        }
                        else
                        {
                            sw = 1;
                        }
                    }
                    MODGMTA.VImp = Aux.ToArray();
                }
                _retValue = -1;
            }
            catch (Exception ex)
            {
                _retValue = 0;
            }
            return _retValue;
        }


        //**********************************************************
        // Objetivo : Calcular impuesto al Pagaré con tasa fija.   *
        //**********************************************************
        public static double CalculaPgf(T_MODGMTA MODGMTA,T_MODGTAB0 MODGTAB0, UI_Frm_Ingreso_Valores Frm_Ingreso_Valores,UnitOfWorkCext01 unit, short a)
        {
            double Tasa = MODGMTA.VImp[a].tasmax;
            string hoy = DateTime.Now.ToString("dd/MM/yyyy");
            double monto = MODGMTA.VPgf.MtoPag;
            double M2 = 0;
            double tipo_cambio = 0;

            if (VB6Helpers.StrComp(VB6Helpers.Format(VB6Helpers.CStr(monto), T_MODGPYF1.Fto_Comparar), VB6Helpers.Format("0", T_MODGPYF1.Fto_Comparar)) > 0)
            {
                monto = (monto * (Tasa / 100));

                //convertir a la moneda de cobro
                if (MODGMTA.VPgf.MonPag != MODGMTA.VDatCob.MonCob)
                {
                    if (MODGMTA.VDatCob.MonCob != MODGMTA.VDatCob.monnac)
                    {
                        if (MODGMTA.VPgf.MonPag != MODGMTA.VDatCob.monnac)
                        {
                            if (~Get_VPar(MODGMTA,MODGTAB0,Frm_Ingreso_Valores,unit,hoy, MODGMTA.VPgf.MonPag) != 0)
                            {
                                return 0;
                            }
                            M2 = monto * MODGTAB0.VVmd.VmdObs;
                            if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores,unit, hoy, MODGMTA.VDatCob.MonCob) != 0)
                            {
                                return 0;
                            }
                            monto = M2 / MODGTAB0.VVmd.VmdObs;
                        }
                        else
                        {
                            if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores,unit, hoy, MODGMTA.VDatCob.MonCob) != 0)
                            {
                                return 0;
                            }
                            monto /= MODGTAB0.VVmd.VmdObs;
                        }

                    }
                    else
                    {
                        if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores,unit, hoy, MODGMTA.VPgf.MonPag) != 0)
                        {
                            return 0;
                        }
                        tipo_cambio = MODGTAB0.VVmd.VmdObs;
                        if (VB6Helpers.StrComp(VB6Helpers.Format(VB6Helpers.CStr(MODGMTA.VPgf.TipCam), T_MODGPYF1.Fto_Comparar), VB6Helpers.Format(VB6Helpers.CStr(tipo_cambio), T_MODGPYF1.Fto_Comparar)) > 0)
                        {
                            tipo_cambio = MODGMTA.VPgf.TipCam;
                        }

                        monto *= tipo_cambio;
                    }

                }

            }

            // se envian datos de retorno en el siguiente orden : monto final
            return monto;
        }

        //**********************************************************
        // Objetivo : Calcular el cobro de impuesto al Pagaré.     *
        //**********************************************************
        public static double CalculaPgi(T_MODGMTA MODGMTA, T_MODGTAB0 MODGTAB0, UI_Frm_Ingreso_Valores Frm_Ingreso_Valores,UnitOfWorkCext01 unit, short a)
        {
            double tasmax;
            double TasMin;
            double monto = MODGMTA.VPgi.MtoPag;
            string hoy = DateTime.Now.ToString("dd/MM/yyyy");
            double Tasa = 0;
            double M2 = 0;
            short periodos = 0;
            double tipo_cambio = 0;

            tasmax = MODGMTA.VImp[a].tasmax;
            TasMin = MODGMTA.VImp[a].TasMin;

            if (VB6Helpers.StrComp(VB6Helpers.Format(VB6Helpers.CStr(monto), T_MODGPYF1.Fto_Comparar), VB6Helpers.Format("0", T_MODGPYF1.Fto_Comparar)) > 0)
            {
                periodos = Calcula_Periodos(DateTime.Now.ToString("dd/MM/yyyy"), MODGMTA.VPgi.FecOtr);
                Tasa = periodos * TasMin;
                if (VB6Helpers.StrComp(VB6Helpers.Format(VB6Helpers.CStr(Tasa), T_MODGPYF1.Fto_Comparar), VB6Helpers.Format(VB6Helpers.CStr(tasmax), T_MODGPYF1.Fto_Comparar)) > 0)
                {
                    Tasa = tasmax;
                }

                monto = (monto * (Tasa / 100));  //monto en MonPag

                //convertir a la moneda de cobro
                if (MODGMTA.VPgi.MonPag != MODGMTA.VDatCob.MonCob)
                {
                    if (MODGMTA.VDatCob.MonCob != MODGMTA.VDatCob.monnac)
                    {
                        if (MODGMTA.VPgi.MonPag != MODGMTA.VDatCob.monnac)
                        {
                            if (~Get_VPar(MODGMTA,MODGTAB0,Frm_Ingreso_Valores,unit, hoy, MODGMTA.VPgi.MonPag) != 0)
                            {
                                return 0;
                            }
                            M2 = monto * MODGTAB0.VVmd.VmdObs;
                            if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores,unit, hoy, MODGMTA.VDatCob.MonCob) != 0)
                            {
                                return 0;
                            }
                            monto = M2 / MODGTAB0.VVmd.VmdObs;
                        }
                        else
                        {
                            if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores,unit, hoy, MODGMTA.VDatCob.MonCob) != 0)
                            {
                                return 0;
                            }
                            monto /= MODGTAB0.VVmd.VmdObs;
                        }

                    }
                    else
                    {
                        if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores,unit, hoy, MODGMTA.VPgi.MonPag) != 0)
                        {
                            return 0;
                        }
                        tipo_cambio = MODGTAB0.VVmd.VmdObs;
                        if (VB6Helpers.StrComp(VB6Helpers.Format(VB6Helpers.CStr(MODGMTA.VPgi.TipCam), T_MODGPYF1.Fto_Comparar), VB6Helpers.Format(VB6Helpers.CStr(tipo_cambio), T_MODGPYF1.Fto_Comparar)) > 0)
                        {
                            tipo_cambio = MODGMTA.VPgi.TipCam;
                        }

                        monto *= tipo_cambio;
                    }

                }

            }

            // se envian datos de retorno en el siguiente orden : monto final
            return monto;
        }

        public static short Calcula_Periodos(string Actual, string Inter)
        {
            string hoy = VB6Helpers.Format(Actual, "yyyymmdd");
            string dec = VB6Helpers.Format(Inter, "yyyymmdd");
            short p = 0;
            short dia_act = 0;
            string s = "";
            double act = 0;
            string f = "";
            double fech = 0;
            short dia = 0;
            short mes = 0;
            short año = 0;
            short Inicio = 0;
            string ff = "";

            if (VB6Helpers.StrComp(hoy, dec) > 0)
            {

                dia_act = (short)VB6Helpers.Val(VB6Helpers.Right(hoy, 2));

                s = VB6Helpers.Left(hoy, 6);
                act = VB6Helpers.Val(s);

                f = VB6Helpers.Left(dec, 6);
                fech = VB6Helpers.Val(f);

                dia = (short)VB6Helpers.Val(VB6Helpers.Right(dec, 2));
                mes = (short)VB6Helpers.Val(VB6Helpers.Mid(dec, 5, 2));
                año = (short)VB6Helpers.Val(VB6Helpers.Left(dec, 4));

                Inicio = (short)(true ? -1 : 0);
                while (fech <= act)
                {

                    if (mes < 12)
                    {
                        mes = (short)(mes + 1);
                    }
                    else
                    {
                        mes = 1;
                        año = (short)(año + 1);
                    }

                    ff = VB6Helpers.Format(VB6Helpers.CStr(año), "0000") + VB6Helpers.Format(VB6Helpers.CStr(mes), "00");
                    fech = VB6Helpers.Val(ff);
                    if (Inicio != 0)
                    {
                        Inicio = (short)(false ? -1 : 0);
                    }
                    else
                    {
                        p = (short)(p + 1);
                    }

                }

                if (dia < dia_act)
                {
                    p = (short)(p + 1);
                }

            }

            return p;
        }

        //**********************************************************
        // Objetivo : Calcular el cobro de impuesto a la venta de  *
        //            Divisas                                      *
        //**********************************************************
        //
        public static double CalculaVdi(T_MODGMTA MODGMTA, T_MODGTAB0 MODGTAB0, UI_Frm_Ingreso_Valores Frm_Ingreso_Valores,UnitOfWorkCext01 unit, short ind, short a)
        {
            double tasmax;
            double TasMin;
            string hoy = DateTime.Now.ToString("dd/MM/yyyy");
            double monto = MODGMTA.VVdi[ind].MtoDec;
            short periodos = 0;
            double Tasa = 0;
            double M2 = 0;
            double tipo_cambio = 0;

            tasmax = MODGMTA.VImp[a].tasmax;
            TasMin = MODGMTA.VImp[a].TasMin;

            if (VB6Helpers.StrComp(VB6Helpers.Format(VB6Helpers.CStr(monto), T_MODGPYF1.Fto_Comparar), VB6Helpers.Format("0", T_MODGPYF1.Fto_Comparar)) > 0)
            {
                periodos = Calcula_Periodos(DateTime.Now.ToString("dd/MM/yyyy"), MODGMTA.VVdi[ind].FecDec);
                Tasa = periodos * TasMin;
                if (VB6Helpers.StrComp(VB6Helpers.Format(VB6Helpers.CStr(Tasa), T_MODGPYF1.Fto_Comparar), VB6Helpers.Format(VB6Helpers.CStr(tasmax), T_MODGPYF1.Fto_Comparar)) > 0)
                {
                    Tasa = tasmax;
                }

                monto = (monto * (Tasa / 100));  //monto en mondec

                //convertir a la moneda de cobro
                if (MODGMTA.VVdi[ind].MonDec != MODGMTA.VDatCob.MonCob)
                {
                    if (MODGMTA.VDatCob.MonCob != MODGMTA.VDatCob.monnac)
                    {
                        if (MODGMTA.VVdi[ind].MonDec != MODGMTA.VDatCob.monnac)
                        {
                            if (~Get_VPar(MODGMTA,MODGTAB0,Frm_Ingreso_Valores,unit, hoy, MODGMTA.VVdi[ind].MonDec) != 0)
                            {
                                return 0;
                            }
                            M2 = monto * MODGTAB0.VVmd.VmdObs;
                            if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores,unit, hoy, MODGMTA.VDatCob.MonCob) != 0)
                            {
                                return 0;
                            }
                            monto = M2 / MODGTAB0.VVmd.VmdObs;
                        }
                        else
                        {
                            if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores,unit, hoy, MODGMTA.VDatCob.MonCob) != 0)
                            {
                                return 0;
                            }
                            monto /= MODGTAB0.VVmd.VmdObs;
                        }

                    }
                    else
                    {
                        if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores,unit, hoy, MODGMTA.VVdi[ind].MonDec) != 0)
                        {
                            return 0;
                        }
                        tipo_cambio = MODGTAB0.VVmd.VmdObs;
                        if (VB6Helpers.StrComp(VB6Helpers.Format(VB6Helpers.CStr(MODGMTA.VVdi[ind].TipCam), T_MODGPYF1.Fto_Comparar), VB6Helpers.Format(VB6Helpers.CStr(tipo_cambio), T_MODGPYF1.Fto_Comparar)) > 0)
                        {
                            tipo_cambio = MODGMTA.VVdi[ind].TipCam;
                        }

                        monto *= tipo_cambio;
                    }

                }

            }

            // se envian datos de retorno en el siguiente orden : monto final
            return monto;
        }

        //***********************************************************************
        // Objetivo   : Calcular el impuesto iva sobre un monto determinado     *
        //***********************************************************************
        public static short CalculaIva(T_MODGMTA MODGMTA, T_MODGPYF0 MODGPYF0,UnitOfWorkCext01 unit, double monto, short Moneda, short ind)
        {
            string hoy = DateTime.Now.ToString("dd/MM/yyyy");
            short a = FindImp(MODGMTA,unit, "IVA");
            string Glosa = MODGMTA.VImp[a].NomImp;
            double tasmax;
            string cta_mn;
            string cta_me;
            double iva;
            string cuenta_com = "";

            tasmax = MODGMTA.VImp[a].tasmax;
            cta_mn = MODGMTA.VImp[a].cta_mn;
            cta_me = MODGMTA.VImp[a].cta_me;
            iva = ((tasmax / 100) * monto);

            if (Moneda == MODGMTA.VDatCob.monnac)
            {
                cuenta_com = cta_mn;
            }
            else
            {
                cuenta_com = cta_me;
            }

            //Se llena la estructura final
            MODGMTA.VCon[ind].ivacon = VB6Helpers.Val(BCH.Comex.Core.BL.XCFT.Modulos.MODGPYF0.unformat(MODGPYF0, iva.ToString("0.00")));
            MODGMTA.VCon[ind].ctaiva = cuenta_com;

            return (short)(true ? -1 : 0);
        }


        //*******************************************************
        // Objetivo : Calcular Remesa de impuestos              *
        //*******************************************************
        public static double CalculaRei(T_MODGMTA MODGMTA, T_MODGTAB0 MODGTAB0, T_MODGPYF0 MODGPYF0, UI_Frm_Ingreso_Valores Frm_Ingreso_Valores,UnitOfWorkCext01 unit, double monto, string Impu)
        {
            string hoy = DateTime.Now.ToString("dd/MM/yyyy");
            short a = FindImp(MODGMTA,unit, Impu);
            string Glosa = MODGMTA.VImp[a].NomImp;
            double tasmax;
            string cta_mn;
            string cta_me;
            double Total;
            double M2 = 0;
            double tot = 0;
            short j = 0;
            string cta = "";

            tasmax = MODGMTA.VImp[a].tasmax;
            cta_mn = MODGMTA.VImp[a].cta_mn;
            cta_me = MODGMTA.VImp[a].cta_me;
            Total = monto * (tasmax / 100) * 1.041667;

            //Se convierte el monto a cobrar que está en moneda del MT a la moneda pedida */
            if (MODGMTA.VDatCob.MonMto != MODGMTA.VDatCob.MonCob)
            {
                if (MODGMTA.VDatCob.MonCob != MODGMTA.VDatCob.monnac)
                {
                    if (MODGMTA.VDatCob.MonMto != MODGMTA.VDatCob.monnac)
                    {
                        if (~Get_VPar(MODGMTA,MODGTAB0,Frm_Ingreso_Valores,unit, hoy, MODGMTA.VDatCob.MonMto) != 0)
                        {
                            return 0;
                        }
                        M2 = Total * MODGTAB0.VVmd.VmdObs;
                        if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores, unit, hoy, MODGMTA.VDatCob.MonCob) != 0)
                        {
                            return 0;
                        }
                        tot = M2 / MODGTAB0.VVmd.VmdObs;
                    }
                    else
                    {
                        if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores, unit, hoy, MODGMTA.VDatCob.MonCob) != 0)
                        {
                            return 0;
                        }
                        tot = Total * MODGTAB0.VVmd.VmdObs;
                    }

                    cta = cta_me;
                }
                else
                {
                    if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores, unit, hoy, MODGMTA.VDatCob.MonMto) != 0)
                    {
                        return 0;
                    }
                    tot = Total * MODGTAB0.VVmd.VmdObs;
                    cta = cta_mn;
                }

            }
            else
            {
                tot = Total;
                if (MODGMTA.VDatCob.MonCob != MODGMTA.VDatCob.monnac)
                {
                    cta = cta_me;
                }
                else
                {
                    cta = cta_mn;
                }

            }

            var VConAux = new T_Con();
            j = (short)(MODGMTA.VCon.Length + 1);
            VConAux.MonCon = MODGMTA.VDatCob.MonCob;
            VConAux.MtoCon = VB6Helpers.Val(BCH.Comex.Core.BL.XCFT.Modulos.MODGPYF0.unformat(MODGPYF0,VB6Helpers.Format(VB6Helpers.CStr(tot), "0.00")));
            VConAux.NemCta = cta;
            VConAux.glscon = MODGPYF1.Minuscula(Glosa);
            VConAux.tipcon = T_MODGMTA.EsRei;
            VConAux.MtoCob = VConAux.MtoCon;
            VConAux.FecCon = DateTime.Now.ToString("dd/MM/yyyy");
            VConAux.gdacon = (short)(false ? -1 : 0);
            VConAux.Estado = MODGMTA.RegNva;
            VConAux.corcon = j;

            var aux = new List<T_Con>(MODGMTA.VCon);
            aux.Add(VConAux);
            MODGMTA.VCon = aux.ToArray();

            return (true ? -1 : 0);
        }


        public static double CalculaRic(T_MODGMTA MODGMTA, T_MODGTAB0 MODGTAB0,T_MODGPYF0 MODGPYF0, UI_Frm_Ingreso_Valores Frm_Ingreso_Valores, UnitOfWorkCext01 unit, double monto)
        {
            string hoy = DateTime.Now.ToString("dd/MM/yyyy");
            short a = FindImp(MODGMTA,unit, "RIC");
            string Glosa = MODGMTA.VImp[a].NomImp;
            double tasmax;
            string cta_mn;
            string cta_me;
            double Total;
            double M2 = 0;
            short j = 0;
            double tot = 0;
            string cta = "";

            tasmax = MODGMTA.VImp[a].tasmax;
            cta_mn = MODGMTA.VImp[a].cta_mn;
            cta_me = MODGMTA.VImp[a].cta_me;
            Total = monto * (tasmax / 100) * 1.041667;

            //Se convierte el monto a cobrar que está  en moneda del MT a la moneda pedida */
            if (MODGMTA.VDatCob.MonMto != MODGMTA.VDatCob.MonCob)
            {
                if (MODGMTA.VDatCob.MonCob != MODGMTA.VDatCob.monnac)
                {
                    if (MODGMTA.VDatCob.MonMto != MODGMTA.VDatCob.monnac)
                    {
                        if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores, unit, hoy, MODGMTA.VDatCob.MonMto) != 0)
                        {
                            return 0;
                        }
                        M2 = Total * MODGTAB0.VVmd.VmdObs;
                        if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores, unit, hoy, MODGMTA.VDatCob.MonCob) != 0)
                        {
                            return 0;
                        }
                        tot = M2 / MODGTAB0.VVmd.VmdObs;
                    }
                    else
                    {
                        if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores, unit, hoy, MODGMTA.VDatCob.MonCob) != 0)
                        {
                            return 0;
                        }
                        tot = Total * MODGTAB0.VVmd.VmdObs;
                    }

                    cta = cta_me;
                }
                else
                {
                    if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores, unit, hoy, MODGMTA.VDatCob.MonMto) != 0)
                    {
                        return 0;
                    }
                    tot = Total * MODGTAB0.VVmd.VmdObs;
                    cta = cta_mn;
                }

            }
            else
            {
                tot = Total;
                if (MODGMTA.VDatCob.MonCob != MODGMTA.VDatCob.monnac)
                {
                    cta = cta_me;
                }
                else
                {
                    cta = cta_mn;
                }

            }

            //Se llena la estructura final           
            var AuxVCon = new T_Con();
            j = (short)(MODGMTA.VCon.Length + 1);
            AuxVCon.MonCon = MODGMTA.VDatCob.MonCob;
            AuxVCon.MtoCon = VB6Helpers.Val(BCH.Comex.Core.BL.XCFT.Modulos.MODGPYF0.unformat(MODGPYF0,VB6Helpers.Format(VB6Helpers.CStr(tot), "0.00")));
            AuxVCon.NemCta = cta;
            AuxVCon.glscon = MODGPYF1.Minuscula(Glosa);
            AuxVCon.tipcon = T_MODGMTA.EsRei;
            AuxVCon.MtoCob = AuxVCon.MtoCon;
            AuxVCon.FecCon = DateTime.Now.ToString("dd/MM/yyyy");
            AuxVCon.gdacon = (short)(false ? -1 : 0);
            AuxVCon.Estado = MODGMTA.RegNva;
            AuxVCon.corcon = j;

            var aux = new List<T_Con>(MODGMTA.VCon);
            aux.Add(AuxVCon);
            MODGMTA.VCon = aux.ToArray();
            return (true ? -1 : 0);
        }


        //**************************************************
        // Objetivo  : Calculo de Impueto sobre Cheque     *
        //**************************************************
        public static double CalculaSch(T_MODGMTA MODGMTA, T_MODGTAB0 MODGTAB0, T_MODGPYF0 MODGPYF0, UI_Frm_Ingreso_Valores Frm_Ingreso_Valores,UnitOfWorkCext01 unit, short NroChe)
        {
            string hoy = DateTime.Now.ToString("dd/MM/yyyy");
            short a = FindImp(MODGMTA,unit,"SCH");
            string Glosa = MODGMTA.VImp[a].NomImp;
            short impue = (short)MODGMTA.VImp[a].MtoMax;
            string cta_mn;
            string cta_me;
            double Toimp = NroChe * impue;
            string cta = "";
            short j = 0;

            cta_mn = MODGMTA.VImp[a].cta_mn;
            cta_me = MODGMTA.VImp[a].cta_me;

            if (MODGMTA.VDatCob.MonCob != MODGMTA.VDatCob.monnac)
            {
                if (~Get_VPar(MODGMTA, MODGTAB0, Frm_Ingreso_Valores,unit, hoy, MODGMTA.VDatCob.MonCob) != 0)
                {
                    return 0;
                }
                Toimp /= MODGTAB0.VVmd.VmdObs;
            }

            if (MODGMTA.VDatCob.MonCob != MODGMTA.VDatCob.monnac)
            {
                cta = cta_me;
            }
            else
            {
                cta = cta_mn;
            }

            //Se llena la estructura final
            j =(short)(MODGMTA.VCon.Length + 1);

            T_Con AuxVCon = new T_Con();

            AuxVCon.MonCon = MODGMTA.VDatCob.MonCob;
            AuxVCon.MtoCon = VB6Helpers.Val(BCH.Comex.Core.BL.XCFT.Modulos.MODGPYF0.unformat(MODGPYF0, VB6Helpers.Format(VB6Helpers.CStr(Toimp), "0.00")));
            AuxVCon.NemCta = cta;
            AuxVCon.glscon = MODGPYF1.Minuscula(Glosa);
            AuxVCon.tipcon = T_MODGMTA.EsSch;
            AuxVCon.MtoCob = AuxVCon.MtoCon;
            AuxVCon.FecCon = DateTime.Now.ToString("dd/MM/yyyy");
            AuxVCon.gdacon = (short)(false ? -1 : 0);
            AuxVCon.Estado = MODGMTA.RegNva;
            AuxVCon.corcon = j;

            var aux = new List<T_Con>(MODGMTA.VCon);
            aux.Add(AuxVCon);

            MODGMTA.VCon = aux.ToArray();
            return (true ? -1 : 0);
        }


        public static void Llena_Vdi(InitializationObject initObject, ref T_Vdi Aux_Vdi)
        {
            T_MODGMTA MODGMTA = initObject.MODGMTA;

            short i = -1;
            i = (short)VB6Helpers.UBound(MODGMTA.VVdi);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            i = (short)(i + 1);
            
            VB6Helpers.RedimPreserve(ref MODGMTA.VVdi, 0, i);
            MODGMTA.VVdi[i] = Aux_Vdi.Copy();
        }
    }
}
