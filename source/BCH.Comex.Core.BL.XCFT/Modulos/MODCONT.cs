using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODCONT
    {
        public static T_MODCONT getMODCONT()
        {
            return new T_MODCONT();
        }        

        //Lee las Cuentas Contables de GL desde el Plan de Cuentas.-
        public static short SyGetn_CtaCtb(InitializationObject initObject,UnitOfWorkCext01 unit)
        {
            using (var tracer = new Tracer())
            {
                T_MODCONT MODCONT = initObject.MODCONT;
                short _retValue = 0;
                try
                {
                    MODCONT.CtaCtb = unit.SceRepository.EjecutarSP<sce_cta_s06_Result>("sce_cta_s06").Select(x => new T_CtaCtb()
                    {
                        Cta_Mon = (short)x.cta_mon,
                        Cta_Nem = x.cta_nem,
                        Cta_Nom = x.cta_nom,
                        Cta_Num = x.cta_num
                    }).ToArray();
                    _retValue = (short)(true ? -1 : 0);
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta", e);

                    _retValue = 0;
                }
                return _retValue;
            }
        }

        //Realiza la Contabilidad de la Cancelación.-
        public static short SyConCan(InitializationObject initObject, UnitOfWorkCext01 unit, string Usuario)
        {
            //PROBAR ACA
            T_MODGCON0 MODGCON0 = initObject.MODGCON0;
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_MODGARB MODGARB = initObject.MODGARB;
            T_MODGANU MODGANU = initObject.MODGANU;
            T_MODCVDIMMM MODCVDIMMM = initObject.MODCVDIMMM;
            T_MODGFYS MODGFYS = initObject.MODGFYS;
            T_MODANUVI MODANUVI = initObject.MODANUVI;
            T_MODPREEM MODPREEM = initObject.MODPREEM;            
            T_MODXPLN0 MODXPLN0 = initObject.MODXPLN0;

            string MndPro_O = "";
            string MndPro_V = "";
            short i = 0;
            short m = 0;
            short j = 0;
            string MndVto = "";
            short mc = 0;
            short mv = 0;
            string MonC = "";
            string Op = "";
            dynamic Rev = null;
            short k = 0;
            short Mon1 = 0;
            short Mon2 = 0;
            short v = 0;
            short o = 0;
            short Via = 0;
            short ori = 0;
            short anu = 0;
            short ree = 0;
            dynamic HAY_IVA = null;
            dynamic HAY_COM = null;

            MODGCON0.VMch = MODGCON0.VmchNul.Copy();

            //No genera contabilidad.-
            if (MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_VisExp && MODXPLN0.VxDatP.MtoLiq == 0 && initObject.MODXPLN1.VCom_xPlv.MtoTot == 0)
            {
                return (short)(true ? -1 : 0);
            }

            //Reverso de Estadística NO tiene Contabilidad.-
            if (((MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_Rev ? -1 : 0) & ~HayPlnConVia(initObject) & (Mdl_Funciones_Varias.TotalComis(initObject.Mdl_Funciones_Varias) == 0 ? -1 : 0)) != 0)
            {
                return (short)(true ? -1 : 0);
            }

            //Reverso de Estadística NO tiene Contabilidad.-
            if (((MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_PlnoBco ? -1 : 0) & ~HayPlnConVia(initObject) & (Mdl_Funciones_Varias.TotalComis(initObject.Mdl_Funciones_Varias) == 0 ? -1 : 0)) != 0)
            {
                return (short)(true ? -1 : 0);
            }

            SyConMch(initObject, T_MODGCVD.ICli, T_MODCONT.CodFun_CVD);
            short _switchVar1 = MODGCVD.VgCvd.TipCVD;
            if (_switchVar1 == T_MODGCVD.TCvd_CVD)
            {
                //Compra/Venta.-
                MndPro_O = "";  //Monedas Procesadas Origen.-
                MndPro_V = "";  //Monedas Procesadas Vías.-
                //Compras.-
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGCVD.VgPli); i++)
                {
                    if (MODGCVD.VgPli[i].Status != T_MODGCVD.EstadoEli && MODGCVD.VgPli[i].TipCVD == "C")
                    {
                        m = MODGCVD.VgPli[i].CodMnd;
                        SyConMcd(initObject, unit, 1, i); //Conversion Haber.-
                        for (j = 0; j <= (short)VB6Helpers.UBound(MODXVIA.VxVia); j++)
                        {
                            //Vueltos.-
                            if (MODXVIA.VxVia[j].Status != T_MODXVIA.ExVia_Eli)
                            {
                                if (((MODXVIA.VxVia[j].CodMon == m ? -1 : 0) & MODXVIA.VxVia[j].Vuelto) != 0)
                                {
                                    MODXVIA.VxVia[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                                    SyConMcd( initObject, unit, 4, j);
                                }
                            }
                        }

                        for (j = 0; j <= (short)VB6Helpers.UBound(MODXVIA.VxVia); j++)
                        {
                            //Vías Nacional Haber.-
                            if (MODXVIA.VxVia[j].Status != T_MODXVIA.ExVia_Eli)
                            {
                                if (MODXVIA.VxVia[j].CodMon == MODGCVD.CodMonedaNac && VB6Helpers.Instr(MndPro_V, VB6Helpers.Format(VB6Helpers.CStr(j), "000")) == 0)
                                {
                                    MODXVIA.VxVia[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                                    SyConMcd(initObject, unit, 4, j);
                                    MndPro_V = MndPro_V + VB6Helpers.Format(VB6Helpers.CStr(j), "000") + ";";
                                }
                            }
                        }

                        for (j = 0; j <= (short)VB6Helpers.UBound(MODXORI.VxOri); j++)
                        {
                            //Origen Debe.-
                            if (MODXORI.VxOri[j].Status != T_MODXORI.ExOri_Eli)
                            {
                                if (MODXORI.VxOri[j].CodMon == m && VB6Helpers.Instr(MndPro_O, VB6Helpers.Format(VB6Helpers.CStr(j), "000")) == 0)
                                {
                                    MODXORI.VxOri[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                                    SyConMcd(initObject, unit, 2, j);
                                    MndPro_O = MndPro_O + VB6Helpers.Format(VB6Helpers.CStr(j), "000") + ";";
                                }
                            }
                        }

                        SyConMcd(initObject, unit, 3, i); //Cambio Debe.-
                        for (j = 0; j <= (short)VB6Helpers.UBound(MODXORI.VxOri); j++)
                        {
                            //Origen Nacional Debe.-
                            if (MODXORI.VxOri[j].Status != T_MODXORI.ExOri_Eli)
                            {
                                if (MODXORI.VxOri[j].CodMon == MODGCVD.CodMonedaNac && VB6Helpers.Instr(MndPro_O, VB6Helpers.Format(VB6Helpers.CStr(j), "000")) == 0)
                                {
                                    MODXORI.VxOri[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                                    SyConMcd(initObject, unit, 2, j);
                                    MndPro_O = MndPro_O + VB6Helpers.Format(VB6Helpers.CStr(j), "000") + ";";
                                }
                            }
                        }
                    }
                }

                //Ventas.-
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGCVD.VgPli); i++)
                {
                    if (MODGCVD.VgPli[i].Status != T_MODGCVD.EstadoEli && (MODGCVD.VgPli[i].TipCVD == "V" || MODGCVD.VgPli[i].TipCVD == "W"))
                    {
                        m = MODGCVD.VgPli[i].CodMnd;
                        SyConMcd(initObject, unit, 5, i); //Conversion Debe.-
                        for (j = 0; j <= (short)VB6Helpers.UBound(MODXVIA.VxVia); j++)
                        {
                            //Vias Haber.-
                            if (MODXVIA.VxVia[j].Status != T_MODXVIA.ExVia_Eli)
                            {
                                if (MODXVIA.VxVia[j].CodMon == m && VB6Helpers.Instr(MndPro_V, VB6Helpers.Format(VB6Helpers.CStr(j), "000")) == 0)
                                {
                                    MODXVIA.VxVia[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                                    SyConMcd(initObject, unit, 4, j);
                                    MndPro_V = MndPro_V + VB6Helpers.Format(VB6Helpers.CStr(j), "000") + ";";
                                }
                            }
                        }

                        SyConMcd(initObject, unit, 6, i); //Cambio Haber.-
                        for (j = 0; j <= (short)VB6Helpers.UBound(MODXVIA.VxVia); j++)
                        {
                            //Vueltos.-
                            if (MODXVIA.VxVia[j].Status != T_MODXVIA.ExVia_Eli)
                            {
                                if (MODXVIA.VxVia[j].CodMon == MODGCVD.CodMonedaNac && VB6Helpers.Instr(MndPro_V, VB6Helpers.Format(VB6Helpers.CStr(j), "000")) == 0)
                                {
                                    MODXVIA.VxVia[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                                    SyConMcd(initObject, unit, 4, j);
                                    MndPro_V = MndPro_V + VB6Helpers.Format(VB6Helpers.CStr(j), "000") + ";";
                                }
                            }
                        }

                        for (j = 0; j <= (short)VB6Helpers.UBound(MODXORI.VxOri); j++)
                        {
                            //Origen Debe.-
                            if (MODXORI.VxOri[j].Status != T_MODXORI.ExOri_Eli)
                            {
                                if (MODXORI.VxOri[j].CodMon == MODGCVD.CodMonedaNac && VB6Helpers.Instr(MndPro_O, VB6Helpers.Format(VB6Helpers.CStr(j), "000")) == 0)
                                {
                                    MODXORI.VxOri[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                                    SyConMcd(initObject, unit, 2, j);
                                    MndPro_O = MndPro_O + VB6Helpers.Format(VB6Helpers.CStr(j), "000") + ";";
                                }
                            }
                        }
                    }
                }

                //Transferencias
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGCVD.VgPli); i++)
                {
                    if (MODGCVD.VgPli[i].Status != T_MODGCVD.EstadoEli && (MODGCVD.VgPli[i].TipCVD == "TI" || MODGCVD.VgPli[i].TipCVD == "TE" || MODGCVD.VgPli[i].TipCVD == "TIN"))
                    {
                        m = MODGCVD.VgPli[i].CodMnd;

                        for (j = 0; j <= (short)VB6Helpers.UBound(MODXVIA.VxVia); j++)
                        {
                            //Vias Haber.-
                            if (MODXVIA.VxVia[j].Status != T_MODXVIA.ExVia_Eli)
                            {
                                if (MODXVIA.VxVia[j].CodMon == m && VB6Helpers.Instr(MndPro_V, VB6Helpers.Format(VB6Helpers.CStr(j), "000")) == 0)
                                {
                                    MODXVIA.VxVia[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                                    SyConMcd(initObject, unit, 4, j);
                                    MndPro_V = MndPro_V + VB6Helpers.Format(VB6Helpers.CStr(j), "000") + ";";
                                }
                            }
                        }

                        for (j = 0; j <= (short)VB6Helpers.UBound(MODXVIA.VxVia); j++)
                        {
                            //Vueltos.-
                            if (MODXVIA.VxVia[j].Status != T_MODXVIA.ExVia_Eli)
                            {
                                if (MODXVIA.VxVia[j].CodMon == MODGCVD.CodMonedaNac && VB6Helpers.Instr(MndPro_V, VB6Helpers.Format(VB6Helpers.CStr(j), "000")) == 0)
                                {
                                    MODXVIA.VxVia[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                                    SyConMcd(initObject, unit, 4, j);
                                    MndPro_V = MndPro_V + VB6Helpers.Format(VB6Helpers.CStr(j), "000") + ";";
                                }
                            }
                        }

                        for (j = 0; j <= (short)VB6Helpers.UBound(MODXORI.VxOri); j++)
                        {
                            //Origen Debe.-
                            if (MODXORI.VxOri[j].Status != T_MODXORI.ExOri_Eli)
                            {
                                if (MODXORI.VxOri[j].CodMon == m && VB6Helpers.Instr(MndPro_O, VB6Helpers.Format(VB6Helpers.CStr(j), "000")) == 0)
                                {
                                    MODXORI.VxOri[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                                    SyConMcd(initObject, unit, 2, j);
                                    MndPro_O = MndPro_O + VB6Helpers.Format(VB6Helpers.CStr(j), "000") + ";";
                                }
                            }
                        }

                        for (j = 0; j <= (short)VB6Helpers.UBound(MODXORI.VxOri); j++)
                        {
                            //Origen Debe.-
                            if (MODXORI.VxOri[j].Status != T_MODXORI.ExOri_Eli)
                            {
                                if (MODXORI.VxOri[j].CodMon == MODGCVD.CodMonedaNac && VB6Helpers.Instr(MndPro_O, VB6Helpers.Format(VB6Helpers.CStr(j), "000")) == 0)
                                {
                                    MODXORI.VxOri[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                                    SyConMcd(initObject, unit, 2, j);
                                    MndPro_O = MndPro_O + VB6Helpers.Format(VB6Helpers.CStr(j), "000") + ";";
                                }
                            }
                        }
                    }
                }
            }
            else if (_switchVar1 == T_MODGCVD.TCvd_Arb)
            {
                //Arbitraje.-
                MndPro_O = "";  //Monedas Procesadas Origen.-
                MndPro_V = "";  //Monedas Procesadas Vías.-
                MndVto = "";    //Monedas Procesadas Vueltos.-
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGARB.VArb); i++)
                {
                    if (MODGARB.VArb[i].Status != T_MODGCVD.EstadoEli)
                    {
                        mc = MODGARB.VArb[i].MndCom;
                        mv = MODGARB.VArb[i].MndVta;
                        SyConMcd(initObject, unit, 7, i); //Conversion Compra Haber.-

                        MonC = VB6Helpers.Format(VB6Helpers.CStr(mc), "000");
                        for (j = 0; j <= (short)VB6Helpers.UBound(MODXVIA.VxVia); j++)
                        {
                            //Vueltos en mnd compra.-
                            if (MODXVIA.VxVia[j].Status != T_MODXVIA.ExVia_Eli)
                            {
                                if ((((MODXVIA.VxVia[j].CodMon == mc ? -1 : 0) & MODXVIA.VxVia[j].Vuelto) & (VB6Helpers.Instr(MndVto, MonC) == 0 ? -1 : 0)) != 0)
                                {
                                    MODXVIA.VxVia[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                                    SyConMcd(initObject, unit, 4, j);
                                }
                            }
                        }

                        MndVto = MndVto + VB6Helpers.Format(VB6Helpers.CStr(mc), "000") + ";";

                        SyConMcd(initObject, unit, 8, i); //Conversion Venta Debe.-
                        for (j = 0; j <= (short)VB6Helpers.UBound(MODXVIA.VxVia); j++)
                        {
                            //Vías Venta Haber.-
                            if (MODXVIA.VxVia[j].Status != T_MODXVIA.ExVia_Eli)
                            {
                                if (((MODXVIA.VxVia[j].CodMon == mv ? -1 : 0) & ~MODXVIA.VxVia[j].Vuelto & (VB6Helpers.Instr(MndPro_V, VB6Helpers.Format(VB6Helpers.CStr(j), "000")) == 0 ? -1 : 0)) != 0)
                                {
                                    MODXVIA.VxVia[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                                    SyConMcd(initObject, unit, 4, j);
                                    MndPro_V = MndPro_V + VB6Helpers.Format(VB6Helpers.CStr(j), "000") + ";";
                                }
                            }
                        }

                        SyConMcd(initObject, unit, 9, i); //Cambio Compra Debe.-
                        SyConMcd(initObject, unit, 10, i); //Cambio Venta  Haber.-

                        for (j = 0; j <= (short)VB6Helpers.UBound(MODXORI.VxOri); j++)
                        {
                            //Origen Compra Debe.-
                            if (MODXORI.VxOri[j].Status != T_MODXORI.ExOri_Eli)
                            {
                                if (MODXORI.VxOri[j].CodMon == mc && VB6Helpers.Instr(MndPro_O, VB6Helpers.Format(VB6Helpers.CStr(j), "000")) == 0)
                                {
                                    MODXORI.VxOri[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                                    SyConMcd(initObject, unit, 2, j);
                                    MndPro_O = MndPro_O + VB6Helpers.Format(VB6Helpers.CStr(j), "000") + ";";
                                }
                            }
                        }
                    }
                }

                for (j = 0; j <= (short)VB6Helpers.UBound(MODXORI.VxOri); j++)
                {
                    //Origen Nac. x Com. Debe.-
                    if (MODXORI.VxOri[j].Status != T_MODXORI.ExOri_Eli)
                    {
                        if (MODXORI.VxOri[j].CodMon == MODGCVD.CodMonedaNac)
                        {
                            MODXORI.VxOri[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                            SyConMcd(initObject, unit, 2, j);
                        }
                    }
                }

                for (j = 0; j <= (short)VB6Helpers.UBound(MODXVIA.VxVia); j++)
                {
                    //Vueltos en Moneda Nacional.-
                    if (MODXVIA.VxVia[j].Status != T_MODXVIA.ExVia_Eli)
                    {
                        if (((MODXVIA.VxVia[j].CodMon == MODGCVD.CodMonedaNac ? -1 : 0) & MODXVIA.VxVia[j].Vuelto) != 0)
                        {
                            MODXVIA.VxVia[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                            SyConMcd(initObject, unit, 4, j);
                        }
                    }
                }
            }
            else if (_switchVar1 == T_MODGCVD.TCvd_VisExp)
            {
                //Visible Exportaciones.-
                for (j = 0; j <= (short)VB6Helpers.UBound(MODXVIA.VxVia); j++)
                {
                    //Vías x Liq. al Haber.-
                    if (MODXVIA.VxVia[j].Status != T_MODXVIA.ExVia_Eli)
                    {
                        MODXVIA.VxVia[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                        SyConMcd(initObject, unit, 4, j);
                    }
                }

                for (j = 0; j <= (short)VB6Helpers.UBound(MODXORI.VxOri); j++)
                {
                    //Origen x Liq. al Debe.-
                    if (MODXORI.VxOri[j].Status != T_MODXORI.ExOri_Eli)
                    {
                        MODXORI.VxOri[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                        SyConMcd(initObject, unit, 2, j);
                    }
                }

                if (MODXPLN0.VxDatP.MtoLiq > 0)
                {
                    SyConMcd(initObject, unit, 11, i); //Conversion x Liq. al Haber.-
                }

                if (BCH.Comex.Core.BL.XCFT.Modulos.MODGCVD.GetMtoNac_xPlv(initObject.MODXPLN0) > 0)
                {
                    SyConMcd(initObject, unit, 12, i); //Cambio x Liq. al Debe.-
                }

            }
            else if (_switchVar1 == T_MODGCVD.TCvd_Rev || _switchVar1 == T_MODGCVD.TCvd_RyR)
            {
                MndPro_O = "";  //Monedas Procesadas Origen.-
                MndPro_V = "";  //Monedas Procesadas Vías.-
                //Reverso Compras.-
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGANU.VAnuPl); i++)
                {
                    if (MODGANU.VAnuPl[i].VisInv == "INV" && (MODGANU.VAnuPl[i].TipPln == T_Mdl_Funciones.TPli_Ingreso || MODGANU.VAnuPl[i].TipPln == T_Mdl_Funciones.TPli_AnuEgr) && MODGANU.VAnuPl[i].MtoAnu > 0 && MODGANU.VAnuPl[i].estado != T_MODGCVD.EstadoEli)
                    {
                        m = MODGANU.VAnuPl[i].CodMnd;
                        SyConMcd(initObject, unit, 13, i); //Conversion x Rev. al Debe.-
                        for (j = 0; j <= (short)VB6Helpers.UBound(MODXVIA.VxVia); j++)
                        {
                            //Vías Venta Haber.-
                            if (MODXVIA.VxVia[j].Status != T_MODXVIA.ExVia_Eli)
                            {
                                if (MODXVIA.VxVia[j].CodMon == m && VB6Helpers.Instr(MndPro_V, VB6Helpers.Format(VB6Helpers.CStr(j), "000")) == 0)
                                {
                                    MODXVIA.VxVia[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                                    SyConMcd(initObject, unit, 4, j);
                                    MndPro_V = MndPro_V + VB6Helpers.Format(VB6Helpers.CStr(j), "000") + ";";
                                }
                            }
                        }
                    }
                }

                for (i = 0; i <= (short)VB6Helpers.UBound(MODGANU.VAnuPl); i++)
                {
                    if (MODGANU.VAnuPl[i].VisInv == "INV" && (MODGANU.VAnuPl[i].TipPln == T_Mdl_Funciones.TPli_Ingreso || MODGANU.VAnuPl[i].TipPln == T_Mdl_Funciones.TPli_AnuEgr) && MODGANU.VAnuPl[i].MtoAnu > 0 && MODGANU.VAnuPl[i].estado != T_MODGCVD.EstadoEli)
                    {
                        SyConMcd(initObject, unit, 14, i); //Cambio x Liq. al Haber.-
                    }
                }

                //Reverso Visible Export.-
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGANU.VAnuPl); i++)
                {
                    if (MODGANU.VAnuPl[i].VisInv == "VIS" && MODGANU.VAnuPl[i].MtoAnu > 0 && MODGANU.VAnuPl[i].estado != T_MODGCVD.EstadoEli && VB6Helpers.Instr(T_MODXPLN1.PLNLIQ, VB6Helpers.CStr(MODGANU.VAnuPl[i].TipPln)) != 0)
                    {
                        m = MODGANU.VAnuPl[i].CodMnd;
                        for (j = 0; j <= (short)VB6Helpers.UBound(MODXVIA.VxVia); j++)
                        {
                            //Vías en M/E al Haber.-
                            if (MODXVIA.VxVia[j].Status != T_MODXVIA.ExVia_Eli)
                            {
                                if (MODXVIA.VxVia[j].CodMon == m && VB6Helpers.Instr(MndPro_V, VB6Helpers.Format(VB6Helpers.CStr(j), "000")) == 0)
                                {
                                    MODXVIA.VxVia[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                                    SyConMcd(initObject, unit, 4, j);
                                    MndPro_V = MndPro_V + VB6Helpers.Format(VB6Helpers.CStr(j), "000") + ";";
                                }
                            }
                        }
                        SyConMcd(initObject, unit, 13, i); //Conversion x Rev. al Debe.-
                        SyConMcd(initObject, unit, 14, i); //Cambio x Liq. al Haber.-
                    }
                }

                for (j = 0; j <= (short)VB6Helpers.UBound(MODXVIA.VxVia); j++)
                {
                    //Vías en $ al Haber.-
                    if (MODXVIA.VxVia[j].Status != T_MODXVIA.ExVia_Eli)
                    {
                        if (MODXVIA.VxVia[j].CodMon == MODGCVD.CodMonedaNac)
                        {
                            MODXVIA.VxVia[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                            SyConMcd(initObject, unit, 4, j);
                        }
                    }
                }

                for (j = 0; j <= (short)VB6Helpers.UBound(MODXORI.VxOri); j++)
                {
                    //Origen en M/E al Debe.-
                    if (MODXORI.VxOri[j].Status != T_MODXORI.ExOri_Eli)
                    {
                        if (MODXORI.VxOri[j].CodMon == m && VB6Helpers.Instr(MndPro_O, VB6Helpers.Format(VB6Helpers.CStr(j), "000")) == 0)
                        {
                            MODXORI.VxOri[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                            SyConMcd(initObject, unit, 2, j);
                            MndPro_O = MndPro_O + VB6Helpers.Format(VB6Helpers.CStr(j), "000") + ";";
                        }
                    }
                }

                for (i = 0; i <= (short)VB6Helpers.UBound(MODXORI.VxOri); i++)
                {
                    //Origen en $ al Debe.-
                    if (MODXORI.VxOri[i].Status != T_MODXORI.ExOri_Eli)
                    {
                        if (MODXORI.VxOri[i].CodMon == MODGCVD.CodMonedaNac)
                        {
                            MODXORI.VxOri[i].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                            SyConMcd(initObject, unit, 2, i);
                        }
                    }
                }

                //Reverso Ventas.-
                for (i = 0; i <= (short)VB6Helpers.UBound(MODGANU.VAnuPl); i++)
                {
                    if (MODGANU.VAnuPl[i].VisInv == "INV" && MODGANU.VAnuPl[i].TipPln == T_Mdl_Funciones.TPli_Egreso && MODGANU.VAnuPl[i].MtoAnu > 0 && MODGANU.VAnuPl[i].estado != T_MODGCVD.EstadoEli)
                    {
                        m = MODGANU.VAnuPl[i].CodMnd;
                        SyConMcd(initObject, unit, 15, i); //Conversion x Rev. al Haber.-
                        for (j = 0; j <= (short)VB6Helpers.UBound(MODXORI.VxOri); j++)
                        {
                            //Origen en M/E al Debe.-
                            if (MODXORI.VxOri[j].Status != T_MODXORI.ExOri_Eli)
                            {
                                if (MODXORI.VxOri[j].CodMon == m && VB6Helpers.Instr(MndPro_O, VB6Helpers.Format(VB6Helpers.CStr(j), "000")) == 0)
                                {
                                    MODXORI.VxOri[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                                    SyConMcd(initObject, unit, 2, j);
                                    MndPro_O = MndPro_O + VB6Helpers.Format(VB6Helpers.CStr(j), "000") + ";";
                                }
                            }
                        }
                    }
                }

                for (i = 0; i <= (short)VB6Helpers.UBound(MODGANU.VAnuPl); i++)
                {
                    if (MODGANU.VAnuPl[i].VisInv == "INV" && (MODGANU.VAnuPl[i].TipPln == T_Mdl_Funciones.TPli_Egreso || MODGANU.VAnuPl[i].TipPln == T_Mdl_Funciones.TPli_AnuIng) && MODGANU.VAnuPl[i].MtoAnu > 0 && MODGANU.VAnuPl[i].estado != T_MODGCVD.EstadoEli)
                    {
                        SyConMcd(initObject, unit, 16, i); //Cambio x Liq. al Debe.-
                    }
                }
            }
            else if (_switchVar1 == T_MODGCVD.TCvd_PlnoBco)
            {
                //Arbitraje.-
                //Vías en M/E al Haber.-
                for (j = 0; j <= (short)VB6Helpers.UBound(MODXVIA.VxVia); j++)
                {
                    //Vias Haber.-
                    if (MODXVIA.VxVia[j].Status != T_MODGCVD.EstadoEli)
                    {
                        MODXVIA.VxVia[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                        SyConMcd(initObject, unit, 4, j);
                    }
                }

                //Conversión M/E al Debe.-
                if (TotalAnu(initObject, unit) > 0)
                {
                    SyConMcd(initObject, unit, 19, i); //Conversion al Debe.-
                }

                //Cambio en $ al Haber.-
                if (TotalCambio(initObject, unit) > 0)
                {
                    SyConMcd(initObject, unit, 20, i); //Cambio x Liq. al Haber.-
                }

                //Origen en $ al Debe.-
                for (j = 0; j <= (short)VB6Helpers.UBound(MODXORI.VxOri); j++)
                {
                    //Origen Debe.-
                    if (MODXORI.VxOri[j].Status != T_MODGCVD.EstadoEli)
                    {
                        MODXORI.VxOri[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                        SyConMcd(initObject, unit, 2, j);
                    }
                }
            }
            else if (_switchVar1 == T_MODGCVD.TCvd_VisImp)
            {
                //Ventas Visibles Importaciones.-
                Op = VB6Helpers.Left(MODGCVD.VgCvd.operel, 3) + "-" + VB6Helpers.Mid(MODGCVD.VgCvd.operel, 4, 2) + "-" + VB6Helpers.Mid(MODGCVD.VgCvd.operel, 6, 2) + "-" + VB6Helpers.Mid(MODGCVD.VgCvd.operel, 8, 3) + "-" + VB6Helpers.Mid(MODGCVD.VgCvd.operel, 11, 5);

                if(!string.IsNullOrEmpty(MODGCVD.VgCvd.operel))
                {
                    MODGCON0.VMch.DesGen = "Asociado a Operación Nº : " + Op;
                }

                Rev = false;

                BCH.Comex.Core.BL.XCFT.Modulos.MODGFYS.FillArrMnd(initObject);

                for (k = 0; k <= (short)VB6Helpers.UBound(MODCVDIMMM.Monedas); k++)
                {
                    //Ordena por Moneda.Distinta de $.
                    //Recorre Operaciones de Compra/Ventas. Genera Cuentas Conversión
                    for (i = 0; i <= (short)VB6Helpers.UBound(MODGFYS.VgFyS); i++)
                    {
                        if (~MODGFYS.VgFyS[i].Borrado != 0)
                        {
                            Mon1 = MODGFYS.VgFyS[i].CodMon[1];
                            Mon2 = MODGFYS.VgFyS[i].CodMon[2];
                            if (MODGFYS.VgFyS[i].monto[1] != 0 || MODGFYS.VgFyS[i].monto[2] != 0)
                            {
                                if (MODGFYS.VgFyS[i].EleTip == 1)
                                {
                                    if (MODCVDIMMM.Monedas[k] == Mon1)
                                    {
                                        SyConMcd(initObject, unit, 21, i);
                                    }
                                }
                            }
                        }
                    }

                    //Recorre Operaciones de Compra/Ventas. Genera Cuentas Cambio

                    for (i = 0; i <= (short)VB6Helpers.UBound(MODGFYS.VgFyS); i++)
                    {
                        if (~MODGFYS.VgFyS[i].Borrado != 0)
                        {
                            if (MODGFYS.VgFyS[i].monto[1] != 0 || MODGFYS.VgFyS[i].monto[2] != 0)
                            {
                                if (MODGFYS.VgFyS[i].EleTip == 1)
                                {
                                    if (MODCVDIMMM.Monedas[k] == T_MODGTAB0.MndNac)
                                    {
                                        SyConMcd(initObject, unit, 22, i);
                                    }
                                }
                            }
                        }
                    }

                    for (j = 0; j <= (short)VB6Helpers.UBound(MODXVIA.VxVia); j++)
                    {
                        //Vueltos.-
                        if (MODXVIA.VxVia[j].Status != T_MODXVIA.ExVia_Eli)
                        {
                            if (((MODXVIA.VxVia[j].CodMon == MODCVDIMMM.Monedas[k] ? -1 : 0) & MODXVIA.VxVia[j].Vuelto) != 0)
                            {
                                MODXVIA.VxVia[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                                SyConMcd(initObject, unit, 4, j);
                            }
                        }
                    }

                    for (j = 0; j <= (short)VB6Helpers.UBound(MODXVIA.VxVia); j++)
                    {
                        //Vías Haber.-
                        if (MODXVIA.VxVia[j].Status != T_MODXVIA.ExVia_Eli)
                        {
                            if ((((MODXVIA.VxVia[j].CodMon == MODCVDIMMM.Monedas[k] && VB6Helpers.Instr(MndPro_V, VB6Helpers.Format(VB6Helpers.CStr(j), "000")) == 0) ? -1 : 0) & ~MODXVIA.VxVia[j].Vuelto) != 0)
                            {
                                SyConMcd(initObject, unit, 4, j);
                                MndPro_V = MndPro_V + VB6Helpers.Format(VB6Helpers.CStr(MODCVDIMMM.Monedas[k]), "000") + ";";
                            }
                        }
                    }

                    for (j = 0; j <= (short)VB6Helpers.UBound(MODXORI.VxOri); j++)
                    {
                        //Origen Debe.-
                        if (MODXORI.VxOri[j].Status != T_MODXORI.ExOri_Eli)
                        {
                            if (MODXORI.VxOri[j].CodMon == MODCVDIMMM.Monedas[k] && VB6Helpers.Instr(MndPro_O, VB6Helpers.Format(VB6Helpers.CStr(j), "000")) == 0)
                            {
                                MODXORI.VxOri[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                                SyConMcd(initObject, unit, 2, j);
                                MndPro_O = MndPro_O + VB6Helpers.Format(VB6Helpers.CStr(j), "000") + ";";
                            }
                        }
                    }
                }
            }
            else if (_switchVar1 ==  T_MODGCVD.TCvd_PlnVEst)
            {
                //Planillas Estadísticas Import.-
                v = 0; o = 0;
                
                v = (short)VB6Helpers.UBound(MODXVIA.VxVia);
                o = (short)VB6Helpers.UBound(MODXORI.VxOri);

                for (j = 0; j <= (short)v; j++)
                {
                    //Vías Nacional Haber.-
                    if (MODXVIA.VxVia[j].Status != T_MODXVIA.ExVia_Eli)
                    {
                        if (MODXVIA.VxVia[j].CodMon == MODGCVD.CodMonedaNac && VB6Helpers.Instr(MndPro_V, VB6Helpers.Format(VB6Helpers.CStr(j), "000")) == 0)
                        {
                            MODXVIA.VxVia[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                            SyConMcd(initObject, unit, 4, j);
                            MndPro_V = MndPro_V + VB6Helpers.Format(VB6Helpers.CStr(j), "000") + ";";
                        }

                    }

                }

                for (j = 0; j <= (short)o; j++)
                {
                    //Origen Nacional Debe.-
                    if (MODXORI.VxOri[j].Status != T_MODXORI.ExOri_Eli)
                    {
                        if (MODXORI.VxOri[j].CodMon == MODGCVD.CodMonedaNac && VB6Helpers.Instr(MndPro_O, VB6Helpers.Format(VB6Helpers.CStr(j), "000")) == 0)
                        {
                            MODXORI.VxOri[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                            SyConMcd(initObject, unit, 2, j);
                            MndPro_O = MndPro_O + VB6Helpers.Format(VB6Helpers.CStr(j), "000") + ";";
                        }

                    }

                }

            }
            else if (_switchVar1 == T_MODGCVD.TCvd_AnuVisI)
            {
                //Anulacion Visibles Importac.
                //----------------------------
                
                Via = (short)VB6Helpers.UBound(MODXVIA.VxVia);
                ori = (short)VB6Helpers.UBound(MODXORI.VxOri);
                anu = (short)VB6Helpers.UBound(MODANUVI.V_PlAnu);
                ree = (short)VB6Helpers.UBound(MODPREEM.Vx_PReem);
                //----------------------------
                //Vias al Haber.-
                //--------------------------
                //Moneda Extranjera.-
                //--------------------------
                for (j = 0; j <= (short)Via; j++)
                {
                    if (MODXVIA.VxVia[j].Status != T_MODXVIA.ExVia_Eli)
                    {
                        if (MODXVIA.VxVia[j].CodMon != MODGCVD.CodMonedaNac)
                        {
                            MODXVIA.VxVia[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                            SyConMcd(initObject, unit, 4, j);
                        }

                    }

                }

                //----------------------------
                //Moneda Nacional
                //----------------------------
                //Vias al Haber.-
                for (j = 0; j <= (short)Via; j++)
                {
                    if (MODXVIA.VxVia[j].Status != T_MODXVIA.ExVia_Eli)
                    {
                        if (MODXVIA.VxVia[j].CodMon == MODGCVD.CodMonedaNac)
                        {
                            MODXVIA.VxVia[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                            SyConMcd(initObject, unit, 4, j);
                        }

                    }

                }

                //--------------------------
                //Moneda Extranjera.-
                //--------------------------
                for (j = 0; j <= (short)ori; j++)
                {
                    if (MODXORI.VxOri[j].Status != T_MODXORI.ExOri_Eli)
                    {
                        if (MODXORI.VxOri[j].CodMon != MODGCVD.CodMonedaNac)
                        {
                            MODXORI.VxOri[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                            SyConMcd(initObject, unit, 2, j);
                        }

                    }

                }

                //Anulacion de Planilla (Conv. al Haber).-
                for (j = 0; j <= (short)anu; j++)
                {
                    if (MODANUVI.V_PlAnu[j].IndAnu != 3 && MODANUVI.V_PlAnu[j].IndAnu != 4)
                    {
                        SyConMcd(initObject, unit, 23, j);
                    }
                }

                //Reemplazo de Planilla (Conv. al Debe)
                for (j = 0; j <= (short)ree; j++)
                {
                    if (MODPREEM.Vx_PReem[j].Estado != 9 && MODPREEM.Vx_PReem[j].IndAnu != 3)
                    {
                        SyConMcd(initObject, unit, 25, j);
                    }
                }

                //----------------------------
                //Moneda Nacional
                //----------------------------

                //Origenes al Debe.-
                for (j = 0; j <= (short)ori; j++)
                {
                    if (MODXORI.VxOri[j].Status != T_MODXORI.ExOri_Eli)
                    {
                        if (MODXORI.VxOri[j].CodMon == MODGCVD.CodMonedaNac)
                        {
                            MODXORI.VxOri[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                            SyConMcd(initObject, unit, 2, j);
                        }
                    }
                }
                //Anulación de Planilla (Cambio al Debe)
                for (j = 0; j <= (short)anu; j++)
                {
                    if (MODANUVI.V_PlAnu[j].IndAnu != 3 && MODANUVI.V_PlAnu[j].IndAnu != 4)
                    {
                        SyConMcd(initObject, unit, 24, j);
                    }
                }
                //Reemplazo de Planilla (Cambio al Haber)
                for (j = 0; j <= (short)ree; j++)
                {
                    if (MODPREEM.Vx_PReem[j].Estado != 9 && MODPREEM.Vx_PReem[j].IndAnu != 3)
                    {
                        SyConMcd(initObject, unit, 26, j);
                    }
                }
            }
            else if (_switchVar1 == T_MODGCVD.TCvd_PlanSO)
            {
                //Planillas sin Operacion.-
                //----------------------------
                Via = (short)VB6Helpers.UBound(MODXVIA.VxVia);
                ori = (short)VB6Helpers.UBound(MODXORI.VxOri);
                ree = (short)VB6Helpers.UBound(MODPREEM.Vx_PReem);
                //--------------------------
                //Moneda Extranjera.-
                //--------------------------
                //Vias al Haber.-
                for (j = 0; j <= (short)Via; j++)
                {
                    if (MODXVIA.VxVia[j].Status != T_MODXVIA.ExVia_Eli)
                    {
                        if (MODXVIA.VxVia[j].CodMon != MODGCVD.CodMonedaNac)
                        {
                            MODXVIA.VxVia[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                            SyConMcd(initObject, unit, 4, j);
                        }
                    }
                }
                //Origenes al debe.-
                for (j = 0; j <= (short)ori; j++)
                {
                    if (MODXORI.VxOri[j].Status != T_MODXORI.ExOri_Eli)
                    {
                        if (MODXORI.VxOri[j].CodMon != MODGCVD.CodMonedaNac)
                        {
                            MODXORI.VxOri[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                            SyConMcd(initObject, unit, 2, j);
                        }

                    }

                }

                //Planilla (Conv. al Debe)
                for (j = 0; j <= (short)ree; j++)
                {
                    if (MODPREEM.Vx_PReem[j].Estado != 9 && MODPREEM.Vx_PReem[j].IndAnu != 3)
                    {
                        SyConMcd(initObject, unit, 25, j);
                    }

                }

                //----------------------------
                //Moneda Nacional
                //----------------------------
                //Vias al Haber.-
                for (j = 0; j <= (short)Via; j++)
                {
                    if (MODXVIA.VxVia[j].Status != T_MODXVIA.ExVia_Eli)
                    {
                        if (MODXVIA.VxVia[j].CodMon == MODGCVD.CodMonedaNac)
                        {
                            MODXVIA.VxVia[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                            SyConMcd(initObject, unit, 4, j);
                        }

                    }

                }

                //Origenes al Debe.-
                for (j = 0; j <= (short)ori; j++)
                {
                    if (MODXORI.VxOri[j].Status != T_MODXORI.ExOri_Eli)
                    {
                        if (MODXORI.VxOri[j].CodMon == MODGCVD.CodMonedaNac)
                        {
                            MODXORI.VxOri[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                            SyConMcd(initObject, unit, 2, j);
                        }
                    }
                }

                //Reemplazo de Planilla (Cambio al Haber)
                for (j = 0; j <= (short)ree; j++)
                {
                    if (MODPREEM.Vx_PReem[j].Estado != 9 && MODPREEM.Vx_PReem[j].IndAnu != 3)
                    {
                        SyConMcd(initObject, unit, 26, j);
                    }
                }
            }
            else if (_switchVar1 == 0)
            {
                if (MODGCVD.COMISION == true)
                {
                    ori = (short)VB6Helpers.UBound(MODXORI.VxOri);
                    for (j = 0; j <= (short)ori; j++)
                    {
                        if (MODXORI.VxOri[j].Status != T_MODXORI.ExOri_Eli)
                        {
                            if (MODXORI.VxOri[j].CodMon == MODGCVD.CodMonedaNac)
                            {
                                MODXORI.VxOri[j].nroimp = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);
                                SyConMcd(initObject, unit, 2, j);
                            }
                        }
                    }
                }
            }
            else
            {
                return 0;
            }

            if (initObject.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 0)
            {
                //Comisiones.-
                HAY_IVA = false;
                HAY_COM = false;

                for (i = 0; i <= (short)VB6Helpers.UBound(initObject.Mdl_Funciones_Varias.V_gCom); i++)
                {
                    if ( initObject.Mdl_Funciones_Varias.V_gCom[i].estado != 3)
                    {
                        if ( initObject.Mdl_Funciones_Varias.V_gCom[i].MtoComp > 0)
                        {
                            SyConMcd(initObject, unit, 17, i);
                            HAY_COM = true;
                        }

                        if ( initObject.Mdl_Funciones_Varias.V_gCom[i].MtoIvap > 0)
                        {
                            SyConMcd(initObject, unit, 18, i);
                            HAY_IVA = true;
                        }
                    }
                }
            }

            using(var tracer = new Tracer("SyConCan - Graba Contabilidad"))
            {
                BCH.Comex.Core.BL.XCFT.Modulos.MODANUVI.Pr_Refundir(initObject);

                if (~Val_GasCorr(initObject) != 0)
                {
                    tracer.AddToContext("SyConCan - Val_GasCorr", "No hay una operación asociada para los gastos");
                    initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "La Cuenta 300.03.03-9 (Gastos por Cobrar) necesita tener operación asociada. Su operación será cancelada.",
                        Title = T_MODGCVD.MsgCVD
                    });

                    return (short)(false ? -1 : 0);
                }
                tracer.AddToContext("SyConCan - SyPutCon", "Grabo Contabilidad");
                return BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.SyPutCon(initObject, unit, Usuario, MODXORI.VgxOri.ImpDeb);
            }
        }

        //Carga el Header del Reporte Contable.-
        public static void SyConMch(InitializationObject initObject, dynamic ICli, short codfun)
        {
            //Header.-
            initObject.MODGCON0.VMcds = new T_Mcd[0];
            initObject.MODGCON0.VMch = initObject.MODGCON0.VmchNul.Copy();
            initObject.MODGCON0.VMch.codcct = initObject.MODGCVD.VgCvd.codcct;
            initObject.MODGCON0.VMch.codpro = initObject.MODGCVD.VgCvd.codpro;
            initObject.MODGCON0.VMch.codesp = initObject.MODGCVD.VgCvd.codesp;
            initObject.MODGCON0.VMch.codofi = initObject.MODGCVD.VgCvd.codofi;
            initObject.MODGCON0.VMch.codope = initObject.MODGCVD.VgCvd.codope;
            initObject.MODGCON0.VMch.NroRpt = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetNroPar(initObject);
            initObject.MODGCON0.VMch.fecmov = DateTime.Now.ToString("yyyy-MM-dd");
            initObject.MODGCON0.VMch.Estado = T_MODGCON0.ECC_ING;
            initObject.MODGCON0.VMch.PrtCli = initObject.Module1.PartysOpe[VB6Helpers.CInt(ICli)].LlaveArchivo;
            initObject.MODGCON0.VMch.IndCli = initObject.Module1.PartysOpe[VB6Helpers.CInt(ICli)].IndNombre;
            initObject.MODGCON0.VMch.rutcli = initObject.Module1.PartysOpe[VB6Helpers.CInt(ICli)].rut;
            initObject.MODGCON0.VMch.NomCli = initObject.Module1.PartysOpe[VB6Helpers.CInt(ICli)].NombreUsado;
            initObject.MODGCON0.VMch.DirCli = initObject.Module1.PartysOpe[VB6Helpers.CInt(ICli)].DireccionUsado;
            initObject.MODGCON0.VMch.ComCli = initObject.Module1.PartysOpe[VB6Helpers.CInt(ICli)].ComunaUsado;
            initObject.MODGCON0.VMch.CiuCli = initObject.Module1.PartysOpe[VB6Helpers.CInt(ICli)].CiudadUsado;
            initObject.MODGCON0.VMch.PaiCli = initObject.Module1.PartysOpe[VB6Helpers.CInt(ICli)].PaisUsado;
            initObject.MODGCON0.VMch.codfun = codfun;

            if(!string.IsNullOrEmpty(initObject.MODGASO.VgAso.OpeCon))
            {
                initObject.MODGCON0.VMch.operel = initObject.MODGASO.VgAso.OpeSin;
                initObject.MODGCON0.VMch.DesGen = "Operación Relacionada " + initObject.MODGASO.VgAso.OpeCon;
            }
            else if (initObject.MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_Rev || initObject.MODGCVD.VgCvd.TipCVD == 0)
            {
                //TCv_RyR es null
                if (initObject.MODGCVD.COMISION == false)
                {
                    initObject.MODGCON0.VMch.DesGen = "Operación Anulada " + initObject.MODGCVD.VgCVDo.codcct + "-" + initObject.MODGCVD.VgCVDo.codpro + "-" + initObject.MODGCVD.VgCVDo.codesp + "-" + initObject.MODGCVD.VgCVDo.codofi + "-" + initObject.MODGCVD.VgCVDo.codope;
                }
            }
            else if (initObject.MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_AnuVisI || initObject.MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_RyR) 
            {
                if (initObject.MODANUVI.V_Plani != null && initObject.MODANUVI.V_Plani.Length > 0)
                {
                    initObject.MODGCON0.VMch.DesGen = "Operación Anulada " + initObject.MODANUVI.V_Plani[0].codcct + "-" + initObject.MODANUVI.V_Plani[0].codpro + "-" + initObject.MODANUVI.V_Plani[0].codesp + "-" + initObject.MODANUVI.V_Plani[0].codofi + "-" + initObject.MODANUVI.V_Plani[0].codope;
                }
            }
        }

        //Carga el Detalle del Reporte Contable.-
        public static void SyConMcd(InitializationObject initObject, UnitOfWorkCext01 unit, dynamic TipMcd, short Indice)
        {
            T_MODGCON0 MODGCON0 = initObject.MODGCON0;
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODGARB MODGARB = initObject.MODGARB;
            T_MODXPLN0 MODXPLN0 = initObject.MODXPLN0;
            T_MODGANU MODGANU = initObject.MODGANU;
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            T_MODGFYS MODGFYS = initObject.MODGFYS;
            T_MODCVDIMMM MODCVDIMMM = initObject.MODCVDIMMM;
            T_MODANUVI MODANUVI = initObject.MODANUVI;
            T_MODPREEM MODPREEM = initObject.MODPREEM;

            short i = 0;
            short n = 0;
            double MtoAPago = 0;
            dynamic ConReempl = null;
            string CodMon = "";

            //Correlativo.-
            //i = 1;
            i = (short)(VB6Helpers.UBound(MODGCON0.VMcds) + 2);//se pone +2 porque cuando el array no tiene elementos devuelve -1
         
            //Detalle.-
            MODGCON0.VMcd = MODGCON0.VMcdNul.Copy();
            MODGCON0.VMcd.codcct = MODGCON0.VMch.codcct;
            MODGCON0.VMcd.codpro = MODGCON0.VMch.codpro;
            MODGCON0.VMcd.codesp = MODGCON0.VMch.codesp;
            MODGCON0.VMcd.codofi = MODGCON0.VMch.codofi;
            MODGCON0.VMcd.codope = MODGCON0.VMch.codope;
            MODGCON0.VMcd.NroRpt = MODGCON0.VMch.NroRpt;
            MODGCON0.VMcd.fecmov = MODGCON0.VMch.fecmov;
            MODGCON0.VMcd.Estado = MODGCON0.VMch.Estado;
            MODGCON0.VMcd.TipMcd = T_MODGCON0.CONTAB_ING;
            MODGCON0.VMcd.rutcli = MODGCON0.VMch.rutcli;
 
            if (Format.StringToDouble(TipMcd) == 1)
            {
                //Conversion al Haber.-
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetWorkSheet("5");
                MODGCON0.VMcd.cod_dh = "H";
                MODGCON0.VMcd.mtomcd = MODGCVD.VgPli[Indice].MtoCVD;
                MODGCON0.VMcd.CodMon = MODGCVD.VgPli[Indice].CodMnd;
            }
            else if (Format.StringToDouble(TipMcd) == 2)
            {
                //Origen al Debe.-
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = MODXORI.VxOri[Indice].NemCta;
                MODGCON0.VMcd.cod_dh = "D";
                MODGCON0.VMcd.mtomcd = MODXORI.VxOri[Indice].MtoTot;
                MODGCON0.VMcd.CodMon = MODXORI.VxOri[Indice].CodMon;
                MODGCON0.VMcd.IdnCta = MODXORI.VxOri[Indice].NumCta;
                n = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones.SyDatosAdic(initObject, unit, "O", Indice);
            }
            else if (Format.StringToDouble(TipMcd) == 3)
            {
                //Cambio al Debe.-
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetWorkSheet("6") + VB6Helpers.Trim(VB6Helpers.Str(MODGCVD.VgPli[Indice].CodMnd));
                MODGCON0.VMcd.cod_dh = "D";
                MODGCON0.VMcd.mtomcd = Math.Round(MODGCVD.VgPli[Indice].MtoCVD * MODGCVD.VgPli[Indice].TipCam,MidpointRounding.AwayFromZero);
                MODGCON0.VMcd.CodMon = MODGCVD.CodMonedaNac;
                MODGCON0.VMcd.TipCam = MODGCVD.VgPli[Indice].TipCam;
            }
            else if (Format.StringToDouble(TipMcd) == 4)
            {
                //Vías al Haber.-
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = MODXVIA.VxVia[Indice].NemCta;
                MODGCON0.VMcd.cod_dh = "H";
                MODGCON0.VMcd.mtomcd = MODXVIA.VxVia[Indice].MtoTot;
                MODGCON0.VMcd.CodMon = MODXVIA.VxVia[Indice].CodMon;
                MODGCON0.VMcd.IdnCta = MODXVIA.VxVia[Indice].NumCta;
                n = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones.SyDatosAdic(initObject, unit, "V", Indice);
            }
            else if (Format.StringToDouble(TipMcd) == 5)
            {
                //Conversion al Debe.-
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetWorkSheet("5");
                MODGCON0.VMcd.cod_dh = "D";
                MODGCON0.VMcd.mtomcd = MODGCVD.VgPli[Indice].MtoCVD;
                MODGCON0.VMcd.CodMon = MODGCVD.VgPli[Indice].CodMnd;
            }
            else if (Format.StringToDouble(TipMcd) == 6)
            {
                //Cambio al Haber.-
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetWorkSheet("6") + VB6Helpers.Trim(VB6Helpers.Str(MODGCVD.VgPli[Indice].CodMnd));
                MODGCON0.VMcd.cod_dh = "H";
                MODGCON0.VMcd.mtomcd = Math.Round(MODGCVD.VgPli[Indice].MtoCVD * MODGCVD.VgPli[Indice].TipCam, MidpointRounding.AwayFromZero);
                MODGCON0.VMcd.CodMon = MODGCVD.CodMonedaNac;
                MODGCON0.VMcd.TipCam = MODGCVD.VgPli[Indice].TipCam;
            }
            else if (Format.StringToDouble(TipMcd) == 7)
            {
                //Conversion Compra al Haber.-
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetWorkSheet("5");
                MODGCON0.VMcd.cod_dh = "H";
                MODGCON0.VMcd.mtomcd = MODGARB.VArb[Indice].MtoCom;
                MODGCON0.VMcd.CodMon = MODGARB.VArb[Indice].MndCom;
            }
            else if (Format.StringToDouble(TipMcd) == 8)
            {
                //Conversion Venta al Debe.-
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetWorkSheet("5");
                MODGCON0.VMcd.cod_dh = "D";
                MODGCON0.VMcd.mtomcd = MODGARB.VArb[Indice].MtoVta;
                MODGCON0.VMcd.CodMon = MODGARB.VArb[Indice].MndVta;
            }
            else if (Format.StringToDouble(TipMcd) == 9)
            {
                //Cambio Compra al Debe.-
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetWorkSheet("6") + VB6Helpers.Trim(VB6Helpers.Str(MODGARB.VArb[Indice].MndCom));
                MODGCON0.VMcd.cod_dh = "D";
                MODGCON0.VMcd.mtomcd = MODGARB.VArb[Indice].MtoPes;
                MODGCON0.VMcd.TipCam = Format.StringToDouble(Format.FormatCurrency(MODGARB.VArb[Indice].MtoPes / MODGARB.VArb[Indice].MtoCom, "0.0000"));
                MODGCON0.VMcd.CodMon = MODGCVD.CodMonedaNac;
            }
            else if (Format.StringToDouble(TipMcd) == 10)
            {
                //Cambio Venta al Haber.-
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetWorkSheet("6") + VB6Helpers.Trim(VB6Helpers.Str(MODGARB.VArb[Indice].MndVta));
                MODGCON0.VMcd.cod_dh = "H";
                MODGCON0.VMcd.mtomcd =MODGARB.VArb[Indice].MtoPes;
                MODGCON0.VMcd.TipCam = Format.StringToDouble(Format.FormatCurrency(MODGARB.VArb[Indice].MtoPes / MODGARB.VArb[Indice].MtoVta, "0.0000"));
                MODGCON0.VMcd.CodMon = MODGCVD.CodMonedaNac;
            }
            else if (Format.StringToDouble(TipMcd) == 11)
            {
                //Conversion x Liquidar al Haber.-
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetWorkSheet("5");
                MODGCON0.VMcd.cod_dh = "H";
                MODGCON0.VMcd.mtomcd = MODXPLN0.VxDatP.MtoLiq;
                MODGCON0.VMcd.CodMon = MODXPLN0.VxDatP.CodMnd;
            }
            else if (Format.StringToDouble(TipMcd) == 12)
            {
                //Cambio x Liquidar al Debe.-
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetWorkSheet("6") + VB6Helpers.Trim(VB6Helpers.Str(MODXPLN0.VxDatP.CodMnd));
                MODGCON0.VMcd.cod_dh = "D";
                MODGCON0.VMcd.mtomcd = BCH.Comex.Core.BL.XCFT.Modulos.MODGCVD.GetMtoNac_xPlv(MODXPLN0);
                MODGCON0.VMcd.TipCam = MODXPLN0.VxDatP.TipCam;
                MODGCON0.VMcd.CodMon = MODGCVD.CodMonedaNac;
            }
            else if (Format.StringToDouble(TipMcd) == 13)
            {
                //Conversion x Reverso al Debe.-
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetWorkSheet("5");
                MODGCON0.VMcd.cod_dh = "D";
                MODGCON0.VMcd.mtomcd = MODGANU.VAnuPl[Indice].MtoAnu;
                MODGCON0.VMcd.CodMon = MODGANU.VAnuPl[Indice].CodMnd;
            }
            else if (Format.StringToDouble(TipMcd) == 14)
            {
                //Cambio x Reverso al Haber.-
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetWorkSheet("6") + VB6Helpers.Trim(VB6Helpers.Str(MODGANU.VAnuPl[Indice].CodMnd));
                MODGCON0.VMcd.cod_dh = "H";
                MODGCON0.VMcd.mtomcd = Math.Round(MODGANU.VAnuPl[Indice].MtoAnu * MODGANU.VAnuPl[Indice].TipCamo, MidpointRounding.AwayFromZero);
                MODGCON0.VMcd.CodMon = MODGCVD.CodMonedaNac;
                MODGCON0.VMcd.TipCam = MODGANU.VAnuPl[Indice].TipCam;
            }
            else if (Format.StringToDouble(TipMcd) == 15)
            {
                //Conversion x Reverso al Haber.-
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetWorkSheet("5");
                MODGCON0.VMcd.cod_dh = "H";
                MODGCON0.VMcd.mtomcd = MODGANU.VAnuPl[Indice].MtoAnu;
                MODGCON0.VMcd.CodMon = MODGANU.VAnuPl[Indice].CodMnd;
            }
            else if (Format.StringToDouble(TipMcd) == 16)
            {
                //Cambio x Reverso al Debe.-
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetWorkSheet("6") + VB6Helpers.Trim(VB6Helpers.Str(MODGANU.VAnuPl[Indice].CodMnd));
                MODGCON0.VMcd.cod_dh = "D";
                MODGCON0.VMcd.mtomcd = Math.Round(MODGANU.VAnuPl[Indice].MtoAnu * MODGANU.VAnuPl[Indice].TipCamo, MidpointRounding.AwayFromZero);
                MODGCON0.VMcd.CodMon = MODGCVD.CodMonedaNac;
                MODGCON0.VMcd.TipCam = MODGANU.VAnuPl[Indice].TipCam;
            }
            else if (Format.StringToDouble(TipMcd) == 17)
            {
                //Comisiones.-
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = Mdl_Funciones_Varias.V_gCom[Indice].NemCta;
                MODGCON0.VMcd.cod_dh = "H";
                MODGCON0.VMcd.mtomcd = Mdl_Funciones_Varias.V_gCom[Indice].MtoComp;
                MODGCON0.VMcd.CodMon = MODGCVD.CodMonedaNac;
                MODGCON0.VMcd.TipCam = MODGTAB0.VVmd.VmdObs;
            }
            else if (Format.StringToDouble(TipMcd) == 18)
            {
                //IVA de Comisiones.-
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetWorkSheet("8");
                MODGCON0.VMcd.cod_dh = "H";
                MODGCON0.VMcd.mtomcd = Mdl_Funciones_Varias.V_gCom[Indice].MtoIvap;
                MODGCON0.VMcd.CodMon = MODGCVD.CodMonedaNac;
                MODGCON0.VMcd.TipCam = MODGTAB0.VVmd.VmdObs;
            }
            else if (Format.StringToDouble(TipMcd) == 19)
            {
                //Conversion Total al Debe.-
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetWorkSheet("5");
                MODGCON0.VMcd.cod_dh = "D";
                MODGCON0.VMcd.CodMon = T_MODGTAB0.MndDol;
                MODGCON0.VMcd.mtomcd = TotalAnu(initObject, unit);
            }
            else if (Format.StringToDouble(TipMcd) == 20)
            {
                //Cambio al Haber.-
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetWorkSheet("6") + VB6Helpers.Trim(VB6Helpers.Str(T_MODGTAB0.MndDol));
                MODGCON0.VMcd.cod_dh = "H";
                MODGCON0.VMcd.CodMon = T_MODGTAB0.MndNac;
                MODGCON0.VMcd.TipCam = MODGCVD.VxPlaAnu.TipCam;
                MODGCON0.VMcd.mtomcd = TotalCambio(initObject, unit);
            }
            else if (Format.StringToDouble(TipMcd) == 21)
            {
                //TCvd_VisImp    CONVERSIÓN VENTA AL DEBE
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = "CONV";
                MODGCON0.VMcd.NemMon = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_NemMnd(MODGTAB0, unit, MODGFYS.VgFyS[Indice].CodMon[1]);
                MODGCON0.VMcd.CodMon = MODGFYS.VgFyS[Indice].CodMon[1];
                if (~MODCVDIMMM.EsReverso != 0)
                {
                    short _switchVar1 = MODGFYS.CVD.Operacion;
                    if (_switchVar1 == T_MODGFYS.FLT)
                    {
                        MtoAPago = MODGFYS.VgFyS[Indice].monto[1] - MODGFYS.VgFyS[Indice].MtoCob;
                        if (MtoAPago > 0)
                        {
                            MODGCON0.VMcd.mtomcd = MODGFYS.VgFyS[Indice].MtoCob;
                        }
                        else
                        {
                            MODGCON0.VMcd.mtomcd = MODGFYS.VgFyS[Indice].monto[1];
                        }
                    }
                    else if (_switchVar1 == T_MODGFYS.SEG)
                    {
                        MtoAPago = MODGFYS.VgFyS[Indice].monto[2] - MODGFYS.VgFyS[Indice].MtoCob;
                        if (MtoAPago > 0)
                        {
                            MODGCON0.VMcd.mtomcd = MODGFYS.VgFyS[Indice].MtoCob;
                        }
                        else
                        {
                            MODGCON0.VMcd.mtomcd = MODGFYS.VgFyS[Indice].monto[2];
                        }

                    }
                    else if (_switchVar1 == T_MODGFYS.FLTSEG)
                    {
                        MtoAPago = (MODGFYS.VgFyS[Indice].monto[1] + MODGFYS.VgFyS[Indice].monto[2]) - (MODGFYS.VgFyS[Indice].MtoCob + MODGFYS.VgFyS[Indice].MtoCob2);
                        if (MtoAPago > 0)
                        {
                            MODGCON0.VMcd.mtomcd = MODGFYS.VgFyS[Indice].MtoCob + MODGFYS.VgFyS[Indice].MtoCob2;
                        }
                        else
                        {
                            MODGCON0.VMcd.mtomcd = MODGFYS.VgFyS[Indice].monto[1] + MODGFYS.VgFyS[Indice].monto[2];
                        }

                    }
                    else if (_switchVar1 == T_MODGFYS.ENDREC)
                    {
                        MODGCON0.VMcd.mtomcd = MODGFYS.VgFyS[Indice].monto[1];
                    }
                    MODGCON0.VMcd.cod_dh = "D";
                }
                else
                {
                    if (ConReempl)
                    {
                        MODGCON0.VMcd.mtomcd = MODGFYS.VgFyS[Indice].monto[1];
                        MODGCON0.VMcd.cod_dh = "D";
                    }
                    else
                    {
                        MODGCON0.VMcd.mtomcd = MODGFYS.VgFyS[Indice].monto[1];
                        MODGCON0.VMcd.cod_dh = "H";
                    }
                }
            }
            else if (Format.StringToDouble(TipMcd) == 22)
            {
                //TCvd_VisImp        CAMBIO VENTA AL HABER.-
                MODGCON0.VMcd.nroimp = i;
                CodMon = VB6Helpers.Trim(VB6Helpers.Str(MODGFYS.VgFyS[Indice].CodMon[1]));
                MODGCON0.VMcd.NemCta = "CAMBIO" + CodMon;
                MODGCON0.VMcd.CodMon = MODGCVD.CodMonedaNac;
                MODGCON0.VMcd.NemMon = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_NemMnd(MODGTAB0, unit, MODGCON0.VMcd.CodMon);

                short _switchVar2 = MODGFYS.CVD.Operacion;
                if (_switchVar2 == T_MODGFYS.FLT)
                {
                    MODGCON0.VMcd.mtomcd = Math.Round(MODGFYS.VgFyS[Indice].MtoCob * MODGFYS.VgFyS[Indice].TipCam[1], MidpointRounding.AwayFromZero);
                }
                else if (_switchVar2 == T_MODGFYS.SEG)
                {
                    MODGCON0.VMcd.mtomcd = Math.Round(MODGFYS.VgFyS[Indice].MtoCob * MODGFYS.VgFyS[Indice].TipCam[1], MidpointRounding.AwayFromZero);
                }
                else if (_switchVar2 == T_MODGFYS.FLTSEG)
                {
                    MODGCON0.VMcd.mtomcd = Math.Round((MODGFYS.VgFyS[Indice].MtoCob + MODGFYS.VgFyS[Indice].MtoCob2) * MODGFYS.VgFyS[Indice].TipCam[1], MidpointRounding.AwayFromZero);
                }
                else if (_switchVar2 == T_MODGFYS.ENDREC)
                {
                    MODGCON0.VMcd.mtomcd =Math.Round(MODGFYS.VgFyS[Indice].monto[1] * MODGFYS.VgFyS[Indice].TipCam[1], MidpointRounding.AwayFromZero);
                }

                if (MODCVDIMMM.EsReverso != 0)
                {
                    if (ConReempl)
                    {
                        MODGCON0.VMcd.cod_dh = "H";
                    }
                    else
                    {
                        MODGCON0.VMcd.cod_dh = "D";
                    }
                }
                else
                {
                    MODGCON0.VMcd.cod_dh = "H";
                }

                MODGCON0.VMcd.TipCam = MODGFYS.VgFyS[Indice].TipCam[1];
            }
            else if (Format.StringToDouble(TipMcd) == 23)
            {
                //Anulación de Planilla (Conversión al Haber)
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetWorkSheet("5");
                MODGCON0.VMcd.cod_dh = "H";
                MODGCON0.VMcd.mtomcd = MODANUVI.V_PlAnu[Indice].MtoTot;
                MODGCON0.VMcd.CodMon = MODANUVI.V_PlAnu[Indice].CodMon;
            }
            else if (Format.StringToDouble(TipMcd) == 24)
            {
                //Anulación de Planilla (Cambio al Debe)
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetWorkSheet("6") + VB6Helpers.Trim(VB6Helpers.Str(MODANUVI.V_PlAnu[Indice].CodMon));
                MODGCON0.VMcd.cod_dh = "D";
                MODGCON0.VMcd.mtomcd =Math.Round(MODANUVI.V_PlAnu[Indice].MtoTot * MODANUVI.V_PlAnu[Indice].TipCam, MidpointRounding.AwayFromZero);
                MODGCON0.VMcd.CodMon = T_MODGTAB0.MndNac;
                MODGCON0.VMcd.TipCam = MODANUVI.V_PlAnu[Indice].TipCam;
            }
            else if (Format.StringToDouble(TipMcd) == 25)
            {
                //Reemplazo de Planilla (Conv. al Debe)
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetWorkSheet("5");
                MODGCON0.VMcd.cod_dh = "D";
                MODGCON0.VMcd.mtomcd = MODPREEM.Vx_PReem[Indice].TotOri;
                MODGCON0.VMcd.CodMon = MODPREEM.Vx_PReem[Indice].CodMon;
            }
            else if (Format.StringToDouble(TipMcd) == 26)
            {
                //Reemplazo de Planilla (Cambio al Haber)
                MODGCON0.VMcd.nroimp = i;
                MODGCON0.VMcd.NemCta = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetWorkSheet("6") + VB6Helpers.Trim(VB6Helpers.Str(MODPREEM.Vx_PReem[Indice].CodMon));
                MODGCON0.VMcd.cod_dh = "H";
                MODGCON0.VMcd.mtomcd = Math.Round(MODPREEM.Vx_PReem[Indice].TotOri * MODPREEM.Vx_PReem[Indice].TipCamo, MidpointRounding.AwayFromZero);
                MODGCON0.VMcd.CodMon = T_MODGTAB0.MndNac;
                MODGCON0.VMcd.TipCam = MODPREEM.Vx_PReem[Indice].TipCamo;
            }

            //Datos de la Cuenta.-
            n = BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.GetIndMcd(initObject, unit);
        }

        public static short HayPlnConVia(InitializationObject initObject)
        {
            short i = 0;
            short HayVia = 0;

            short n = 0;
            n = (short)VB6Helpers.UBound(initObject.MODGANU.VAnuPl);

            //Verifica Existencia de Planillas de Anulación (c/contab.).-
            for (i = 0; i <= (short)VB6Helpers.UBound(initObject.MODGANU.VAnuPl); i++)
            {
                if (initObject.MODGANU.VAnuPl[i].estado != T_MODGCVD.EstadoEli && initObject.MODGANU.VAnuPl[i].MtoAnu > 0)
                {
                    if (!string.IsNullOrEmpty(initObject.MODGANU.VAnuPl[i].Motivo))
                    {
                        short _switchVar1 = initObject.MODGANU.VAnuPl[i].TipPln;
                        if (_switchVar1 == T_Mdl_Funciones.TPli_Ingreso || _switchVar1 == T_Mdl_Funciones.TPli_Egreso)
                        {
                            HayVia = (short)(true ? -1 : 0);
                        }

                    }
                    else
                    {
                        //Saltarse las Estadísticas.-
                        if (initObject.MODGANU.VAnuPl[i].VisInv == "VIS" && VB6Helpers.Instr(T_MODXPLN1.PLNLIQ, VB6Helpers.CStr(initObject.MODGANU.VAnuPl[i].TipPln)) != 0)
                        {
                            HayVia = (short)(true ? -1 : 0);
                        }

                    }

                }

            }

            n = (short)VB6Helpers.UBound(initObject.MODGPLI1.Vplis);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            //Verifica Existencia de Planillas de Anulación (c/contab.).-
            for (i = 0; i <= (short)VB6Helpers.UBound(initObject.MODGPLI1.Vplis); i++)
            {
                if (initObject.MODGPLI1.Vplis[i].Estado != T_MODGCVD.EstadoEli)
                {
                    short _switchVar2 = initObject.MODGPLI1.Vplis[i].TipPln;
                    if (_switchVar2 == T_Mdl_Funciones.TPli_AnuIng || _switchVar2 == T_Mdl_Funciones.TPli_AnuEgr)
                    {
                        HayVia = (short)(true ? -1 : 0);
                    }

                }

            }

            //Verifica Existencia de Planillas de Anulación Sin Operación.- (c/contab.)
            for (i = 0; i <= (short)VB6Helpers.UBound(initObject.MODXANU.VxAnus); i++)
            {
                if (initObject.MODXANU.VxAnus[i].PlnEst != 1 && VB6Helpers.Instr(T_MODXPLN1.PLNLIQ, VB6Helpers.CStr(initObject.MODXANU.VxAnus[i].TipPln)) != 0)
                {
                    HayVia = (short)(true ? -1 : 0);
                }

            }

            return HayVia;
        }

        public static short Val_GasCorr(InitializationObject initObj)
        {
            short _retValue = 0;
            short i = 0;
            short n = 0;
            n = (short)VB6Helpers.UBound(initObj.MODGCON0.VMcds);

            _retValue = (short)(true ? -1 : 0);

            for (i = 0; i <= (short)n; i++)
            {
                if (initObj.MODGCON0.VMcds[i].NumCta == "30003039")
                {
                    if (string.IsNullOrEmpty(initObj.MODGCON0.VMch.operel))
                    {
                        return (short)(false ? -1 : 0);
                    }
                }
            }
            return _retValue;
        }

        //Retorna el monto anulado de las planillas.-
        public static double TotalAnu(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            short j = 0;
            short n = 0;
            double TotAnu = 0;

            for (j = 1; j <= (short)VB6Helpers.UBound(initObj.MODXANU.VxAnus); j++)
            {
                if (((initObj.MODXANU.VxAnus[j].Estado != T_MODGCVD.EstadoEli ? -1 : 0) & VB6Helpers.Instr(T_MODXPLN1.PLNLIQ, VB6Helpers.CStr(initObj.MODXANU.VxAnus[j].TipPln))) != 0)
                {
                    n = MODGTAB0.Get_VMndBC(initObj.MODGTAB0, unit, initObj.MODXANU.VxAnus[j].CodMnd);
                    TotAnu += initObj.MODXANU.VxAnus[j].MtoAnu;
                }
            }
            return TotAnu;
        }

        //Retorna el Cambio de las planillas.-
        public static double TotalCambio(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            short j = 0;
            double TotCam = 0;
            for (j = 1; j <= (short)VB6Helpers.UBound(initObj.MODXANU.VxAnus); j++)
            {
                if (((initObj.MODXANU.VxAnus[j].Estado != T_MODGCVD.EstadoEli ? -1 : 0) & VB6Helpers.Instr(T_MODXPLN1.PLNLIQ, VB6Helpers.CStr(initObj.MODXANU.VxAnus[j].TipPln))) != 0)
                {
                    TotCam = TotCam + (initObj.MODXANU.VxAnus[j].MtoAnu * initObj.MODGCVD.VxPlaAnu.TipCam);
                }
            }
            return TotCam;
        }
    }
}
