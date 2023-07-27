using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class ModChVrf
    {
        public static T_ModChVrf GetModChVrf() {
            return new T_ModChVrf();
        }

        public static short SyGet_ccpl(T_ModChVrf ModChVrf,UnitOfWorkCext01 unit)
        {
            short _retValue = 0;
            try
            {
                // IGNORED: On Error GoTo SyGet_CcplErr
                ModChVrf.VCcpl = unit.SceRepository.EjecutarSP<sce_ccpl_s01_MS_Result>("sce_ccpl_s01_MS").Select(x =>
                new T_CCpl(){
                    CodCom = x.codcom,
                    CptCom = x.cptcom,
                    DesCom = x.descom,
                    tipope = x.tipope,
                    flging = (short)(x.flging ? -1 : 0),
                    rutint = (short)(x.rutint ? -1 : 0),
                    nomint = (short)(x.nomint ? -1 : 0),
                    dirint = (short)(x.dirint ? -1 : 0),
                    codpai = (short)(x.codpai ? -1 : 0),
                    mtopss = (short)(x.mtopss ? -1 : 0),
                    dataut = (short)(x.dataut ? -1 : 0),
                    operel = (short)(x.operel ? -1 : 0),
                    numins = (short)(x.numins ? -1 : 0),
                    fecins = (short)(x.fecins ? -1 : 0),
                    finext = (short)(x.finext ? -1 : 0),
                    vtocic = (short)(x.vtocic ? -1 : 0),
                    fecdes = (short)(x.fecdes ? -1 : 0),
                    mondes = (short)(x.mondes ? -1 : 0),
                    mtodes = (short)(x.mtodes ? -1 : 0),
                    impadc = (short)(x.impadc ? -1 : 0),
                    decexp = (short)(x.decexp ? -1 : 0),
                    infexp = (short)(x.infexp ? -1 : 0),
                    vtoret = (short)(x.vtoret ? -1 : 0),
                    mtoexp = (short)(x.mtoexp ? -1 : 0),
                    vtofin = (short)(x.vtofin ? -1 : 0),
                    nomcom = (short)(x.nomcom ? -1 : 0),
                    infimp = (short)(x.infimp ? -1 : 0),
                    decimp = (short)(x.decimp ? -1 : 0),
                    codfdp = (short)(x.codfdp ? -1 : 0),
                    conemb = (short)(x.conemb ? -1 : 0),
                    vtoope = (short)(x.vtoope ? -1 : 0),
                    mtoimp = (short)(x.mtoimp ? -1 : 0),
                    datint = (short)(x.datint ? -1 : 0),
                    datder = (short)(x.datder ? -1 : 0),
                    acuaco = (short)(x.acuaco ? -1 : 0),
                    codccr = (short)(x.codccr ? -1 : 0),
                    observ = (short)(x.observ  ? -1 : 0)
                }).ToArray();
                _retValue = (short)(true ? -1 : 0);
            }
            catch (Exception _ex)
            {
                _retValue = 0;
            }
            return _retValue;
        }

        public static void LimpiaVPlis(T_MODGPLI1 MODGPLI1, string Cod_IE)
        {
            // UPGRADE_INFO (#05B1): The 'a' variable wasn't declared explicitly.
            short a = 0;
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;
            // UPGRADE_INFO (#05B1): The 'b' variable wasn't declared explicitly.
            short b = 0;
            T_Pli[] Loc_Vplis = new T_Pli[0];

            a = 0;
            
            a = (short)VB6Helpers.UBound(MODGPLI1.Vplis);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            Loc_Vplis = new T_Pli[0];
            for (i = 0; i <= (short)a; i++)
            {
                if (MODGPLI1.Vplis[i].Cod_IE != Cod_IE)
                {
                    b = (short)(VB6Helpers.UBound(Loc_Vplis) + 1);
                    VB6Helpers.RedimPreserve(ref Loc_Vplis, 0, b );
                    Loc_Vplis[b] = MODGPLI1.Vplis[i].Copy();
                }

            }

            b = (short)VB6Helpers.UBound(Loc_Vplis);
            MODGPLI1.Vplis = new T_Pli[b + 1];

            for (i = 0; i <= (short)b; i++)
            {
                MODGPLI1.Vplis[i] = Loc_Vplis[i].Copy();
            }

        }

        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static void SumaPlnInv(InitializationObject Modulos,UnitOfWorkCext01 unit, short ind_cliente, short estado)
        {
            T_ModChVrf ModChVrf = Modulos.ModChVrf;
            T_MODGPLI1 MODGPLI1 = Modulos.MODGPLI1;
            T_MODGRNG MODGRNG = Modulos.MODGRNG;
            T_MODGUSR MODGUSR = Modulos.MODGUSR;
            T_Module1 Module1 = Modulos.Module1;
            T_MODGSCE MODGSCE = Modulos.MODGSCE;
            T_MODGTAB0 MODGTAB0 = Modulos.MODGTAB0;

            // UPGRADE_INFO (#05B1): The 'a' variable wasn't declared explicitly.
            short a = 0;
            // UPGRADE_INFO (#05B1): The 'b' variable wasn't declared explicitly.
            short b = 0;
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;
            // UPGRADE_INFO (#05B1): The 'Vx_Anul' variable wasn't declared explicitly.
            dynamic Vx_Anul = null;
            a = 0;
            
            a = (short)VB6Helpers.UBound(ModChVrf.VPlnChV);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            b = (short)VB6Helpers.UBound(MODGPLI1.Vplis);

            for (i = 0; i <= (short)a; i++)
            {
                VB6Helpers.RedimPreserve(ref MODGPLI1.Vplis, 0, b);
                MODGPLI1.Vplis[b].NumPli = VB6Helpers.Str(BCH.Comex.Core.BL.XCFT.Modulos.MODGRNG.LeeSceRng(Modulos.MODGRNG,Modulos.MODGUSR,Modulos.Mdi_Principal,unit, T_MODGRNG.Rng_PlaInv));
                MODGPLI1.Vplis[b].FecPli = DateTime.Now.ToString("dd/MM/yyyy");
                MODGPLI1.Vplis[b].cencos = MODGUSR.UsrEsp.CentroCosto;
                MODGPLI1.Vplis[b].codusr = MODGUSR.UsrEsp.Especialista;
                MODGPLI1.Vplis[b].Fecing = DateTime.Now.ToString("dd/MM/yyyy");
                MODGPLI1.Vplis[b].FecAct = DateTime.Now.ToString("dd/MM/yyyy");
                MODGPLI1.Vplis[b].codcct = Module1.Codop.Cent_Costo;
                MODGPLI1.Vplis[b].codpro = Module1.Codop.Id_Product;
                MODGPLI1.Vplis[b].codesp = Module1.Codop.Id_Especia;
                MODGPLI1.Vplis[b].codofi = Module1.Codop.Id_Empresa;
                MODGPLI1.Vplis[b].codope = Module1.Codop.Id_Operacion;
                MODGPLI1.Vplis[b].CodAnu = VB6Helpers.CStr(Vx_Anul);
                MODGPLI1.Vplis[b].PlzBcc = MODGSCE.VGen.CodPbc;
                MODGPLI1.Vplis[b].rutcli = Module1.PartysOpe[ind_cliente].rut;
                MODGPLI1.Vplis[b].PrtCli = Module1.PartysOpe[ind_cliente].LlaveArchivo;
                MODGPLI1.Vplis[b].IndNom = Module1.PartysOpe[ind_cliente].IndNombre;
                MODGPLI1.Vplis[b].IndDir = Module1.PartysOpe[ind_cliente].IndDireccion;
                if (ModChVrf.VPlnChV[i].Cod_IE == "I")
                {
                    MODGPLI1.Vplis[b].CodOci = 110;
                }
                else
                {
                    MODGPLI1.Vplis[b].CodOci = 210;
                }

                MODGPLI1.Vplis[b].codcom = ModChVrf.VPlnChV[i].CodCom;
                MODGPLI1.Vplis[b].Concep = ModChVrf.VPlnChV[i].CptCom;
                MODGPLI1.Vplis[b].Estado = estado;
                MODGPLI1.Vplis[b].codpai = ModChVrf.VPlnChV[i].CodPais;
                MODGPLI1.Vplis[b].CodMnd = ModChVrf.VPlnChV[i].CodMon;
                MODGPLI1.Vplis[b].CodMndBC = MODGTAB0.VMnd[BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0,unit,ModChVrf.VPlnChV[i].CodMon)].Mnd_MndCbc;
                MODGPLI1.Vplis[b].Mtopar = MODGTAB1.SyGet_Vmc(MODGTAB0,unit, MODGPLI1.Vplis[b].CodMnd, MODGPLI1.Vplis[b].FecPli, "P");
                MODGPLI1.Vplis[b].MtoOpe = ModChVrf.VPlnChV[i].Monto;
                if (MODGPLI1.Vplis[b].Mtopar == 0)
                {
                    MODGPLI1.Vplis[b].Mtopar = 1;
                }
                MODGPLI1.Vplis[b].MtoDol = ((ModChVrf.VPlnChV[i].Monto / MODGPLI1.Vplis[b].Mtopar));
                MODGPLI1.Vplis[b].TipCam = 0D;
                MODGPLI1.Vplis[b].MtoNac = (MODGPLI1.Vplis[b].MtoDol * MODGPLI1.Vplis[b].TipCam);
                MODGPLI1.Vplis[b].IndPrt = ind_cliente;
                if (ModChVrf.VPlnChV[i].Cod_IE == "I")
                {
                    MODGPLI1.Vplis[b].TipPln = 8;
                }
                else
                {
                    MODGPLI1.Vplis[b].TipPln = 9;
                }

                MODGPLI1.Vplis[b].ObsPli = MODGPYF1.Fn_OpeConRaya(MODGCHQ.Referencia(Modulos)) + " Planilla de Transferencia.";

                MODGPLI1.Vplis[b].ZonFra = (short)(false ? -1 : 0);
                MODGPLI1.Vplis[b].EsTrn = (short)(true ? -1 : 0);
                MODGPLI1.Vplis[b].Cod_IE = ModChVrf.VPlnChV[i].Cod_IE;
            }

        }

        public static dynamic TraeDatosOri_ChVrf(InitializationObject initObject, short indori)
        {
            T_MODXORI MODXORI = initObject.MODXORI;
            T_ModChVrf ModChVrf = initObject.ModChVrf;
            T_Module1 Module1 = initObject.Module1;

            // UPGRADE_INFO (#05B1): The 'a' variable wasn't declared explicitly.
            short a = 0;
            // UPGRADE_INFO (#05B1): The 'b' variable wasn't declared explicitly.
            short b = 0;
            // UPGRADE_INFO (#05B1): The 'CodMon' variable wasn't declared explicitly.
            short CodMon = 0;
            // UPGRADE_INFO (#05B1): The 'Total' variable wasn't declared explicitly.
            double Total = 0;
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;
            // UPGRADE_INFO (#05B1): The 'sw' variable wasn't declared explicitly.
            short sw = 0;
            // UPGRADE_INFO (#05B1): The 'Posicion' variable wasn't declared explicitly.
            short Posicion = 0;
            a = 0;
            
            a = (short)VB6Helpers.UBound(MODXORI.VxOri);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            b = 0;
           
            b = (short)VB6Helpers.UBound(ModChVrf.VgChV);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            CodMon = MODXORI.VxOri[indori].CodMon;

            Total = 0;
            for (i = 0; i <= (short)a; i++)
            {
                if (MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CHVRF && MODXORI.VxOri[i].Status != T_MODXVIA.ExVia_Eli)
                {
                    if (MODXORI.VxOri[i].CodMon == CodMon)
                    {
                        Total += MODXORI.VxOri[i].MtoTot;
                    }

                }

            }

            sw = (short)(false ? -1 : 0);
            for (i = 0; i <= (short)b; i++)
            {
                if (ModChVrf.VgChV[i].CodMon == CodMon)
                {
                    ModChVrf.VgChV[i].Saldo = Total;
                    sw = (short)(true ? -1 : 0);
                }

            }

            if (~sw != 0)
            {
                b = (short)(b + 1);
                VB6Helpers.RedimPreserve(ref ModChVrf.VgChV, 0, b);
                ModChVrf.VgChV[b].Saldo = Total;
                ModChVrf.VgChV[b].NemMon = MODXORI.VxOri[indori].NemMon;
                ModChVrf.VgChV[b].CodMon = MODXORI.VxOri[indori].CodMon;
                ModChVrf.VgChV[b].IdParty = MODXORI.VxOri[indori].IdPrty;
                Posicion = MODXORI.VxOri[indori].PosPrty;
                ModChVrf.VgChV[b].IdNom = Module1.PartysOpe[Posicion].IndNombre;
                ModChVrf.VgChV[b].IdDir = Module1.PartysOpe[Posicion].IndDireccion;
                ModChVrf.VgChV[b].Cod_IE = "I";
            }

            return null;
        }

        public static void TraeDatosVias_ChVrf(InitializationObject initObject, short indvia)
        {
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_ModChVrf ModChVrf = initObject.ModChVrf;
            T_Module1 Module1 = initObject.Module1;
            T_MODGCON0 T_MODGCON0 = initObject.MODGCON0; 
           
            short a = 0;
            short b = 0;
            short CodMon = 0;
            double Total = 0;
            short i = 0;
            short sw = 0;
            short Posicion = 0;
            
            a = 0;
            a = (short)VB6Helpers.UBound(MODXVIA.VxVia);
            
            b = 0;
            b = (short)VB6Helpers.UBound(ModChVrf.VgChV);
            CodMon = MODXVIA.VxVia[indvia].CodMon;

            Total = 0;
            for (i = 0; i <= (short)a; i++)
            {
                if ((((
                    MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CHVRF || 
                    MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_OPC   || 
                    MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_OPOP  || 
                    MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CHMEBCH) 
                    && MODXVIA.VxVia[i].Status != T_MODXVIA.ExVia_Eli ? -1 : 0) & MODXVIA.VxVia[i].GenPln) != 0) {
                    if (MODXVIA.VxVia[i].CodMon == CodMon)
                    {
                        Total += MODXVIA.VxVia[i].MtoTot;
                    }
                }
            }

            sw = (short)(false ? -1 : 0);
            for (i = 0; i <= (short)b; i++)
            {
                if (ModChVrf.VgChV[i].CodMon == CodMon)
                {
                    ModChVrf.VgChV[i].Saldo = Total;
                    sw = (short)(true ? -1 : 0);
                }
            }

            if (~sw != 0)
            {
                b = (short)(b + 1);
                VB6Helpers.RedimPreserve(ref ModChVrf.VgChV, 0, b);
                ModChVrf.VgChV[b].Saldo = Total;
                ModChVrf.VgChV[b].NemMon = MODXVIA.VxVia[indvia].NemMon;
                ModChVrf.VgChV[b].CodMon = MODXVIA.VxVia[indvia].CodMon;
                ModChVrf.VgChV[b].IdParty = MODXVIA.VxVia[indvia].IdPrty;
                Posicion = MODXVIA.VxVia[indvia].PosPrty;
                ModChVrf.VgChV[b].IdNom = Module1.PartysOpe[Posicion].IndNombre;
                ModChVrf.VgChV[b].IdDir = Module1.PartysOpe[Posicion].IndDireccion;
                ModChVrf.VgChV[b].Cod_IE = "E";
            }

        }
    }
}
