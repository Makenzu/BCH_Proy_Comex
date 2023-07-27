using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using System;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODCONGL
    {
        // Realiza la Contabilidad del GL.-
        public static int SyConGL(DatosGlobales Globales,UnitOfWorkCext01 unit, string usuario, int Impuesto)
        {
            using(var tracer = new Tracer("SyConGL"))
            {
            T_MODCONGL MODCONGL = Globales.MODCONGL;
            T_MODGCON0 MODGCON0 = Globales.MODGCON0;
            T_CONTAB01 CONTAB01 = Globales.CONTAB01;
            int SyConGL = 0;

            string MsgCvd = "";
            int i = 0;

            SyConMch(Globales, T_GLOBAL.I_Cli, T_MODCONGL.CodFun_GL);
            MODGCON0.VMcds = new T_Mcd[1] { new T_Mcd() };
            for (i = 0; i <= CONTAB01.Est_Contab.GetUpperBound(0); i += 1)
            {
                if (CONTAB01.Est_Contab[i].Borrado == 0)
                {
                        SyConMcd(Globales, unit, i);
                }
            }

            if (Val_GasCorr(Globales) == 0)
            {
                    tracer.AddToContext("Gastos por Cobrar", "La Cuenta 300.03.03-9 (Gastos por Cobrar) necesita tener operación asociada.");
                Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                {
                        Text = "La Cuenta 300.03.03-9 (Gastos por Cobrar) necesita tener operación asociada. Su operación será cancelada.",
                        Title = MsgCvd,
                        Type = Common.UI_Modulos.TipoMensaje.Error
                });
                SyConGL = false.ToInt();
                return SyConGL;
            }

                SyConGL = BCH.Comex.Core.BL.XGGL.Modulos.MODGCON0.SyPutCon(Globales, unit, usuario, Impuesto);

            return SyConGL;
        }
        }

        // Carga el Header del Reporte Contable.-
        public static void SyConMch(DatosGlobales Globales, int ICli, int codfun)
        {
            T_MODGCON0 MODGCON0 = Globales.MODGCON0;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            T_MODGASO MODGASO = Globales.MODGASO;
            // Header.-
            MODGCON0.VMch = MODGCON0.VmchNul.Copy();
            MODGCON0.VMch.CodCct = SYGETPRT.Codop.Cent_costo;
            MODGCON0.VMch.CodPro = SYGETPRT.Codop.Id_Product;
            MODGCON0.VMch.CodEsp = SYGETPRT.Codop.Id_Especia;
            MODGCON0.VMch.CodOfi = SYGETPRT.Codop.Id_Empresa;
            MODGCON0.VMch.CodOpe = SYGETPRT.Codop.Id_Operacion;
            MODGCON0.VMch.NroRpt = BCH.Comex.Core.BL.XGGL.Modulos.MODGCON0.GetNroPar(Globales);
            MODGCON0.VMch.FecMov = DateTime.Now.ToString("yyyy-MM-dd");
            MODGCON0.VMch.estado = T_MODGCON0.ECC_ING;
            MODGCON0.VMch.PrtCli = SYGETPRT.PartysOpe[ICli].LlaveArchivo;
            MODGCON0.VMch.IndCli = SYGETPRT.PartysOpe[ICli].IndNombre;
            MODGCON0.VMch.rutcli = SYGETPRT.PartysOpe[ICli].Rut;
            MODGCON0.VMch.NomCli = SYGETPRT.PartysOpe[ICli].NombreUsado;
            MODGCON0.VMch.DirCli = SYGETPRT.PartysOpe[ICli].DireccionUsado;
            MODGCON0.VMch.ComCli = SYGETPRT.PartysOpe[ICli].ComunaUsado;
            MODGCON0.VMch.CiuCli = SYGETPRT.PartysOpe[ICli].CiudadUsado;
            MODGCON0.VMch.PaiCli = SYGETPRT.PartysOpe[ICli].PaisUsado;
            MODGCON0.VMch.codfun = codfun;
            MODGCON0.VMch.operel = MODGASO.VgAso.OpeSin;
            MODGCON0.VMch.DesGen = MODGASO.VgAso.OpeCon;

        }


        public static int Val_GasCorr(DatosGlobales Globales)
        {
            T_MODGCON0 MODGCON0 = Globales.MODGCON0;
            int Val_GasCorr = 0;

            int i = 0;
            int n = 0;

            n = MODGCON0.VMcds.GetUpperBound(0);

            Val_GasCorr = true.ToInt();

            for (i = 1; i <= n; i += 1)
            {
                if (MODGCON0.VMcds[i].numcta == "30003039")
                {
                    if (MODGCON0.VMch.operel == "")
                    {
                        Val_GasCorr = false.ToInt();
                        return Val_GasCorr;
                    }
                }
            }

            return Val_GasCorr;
        }

        // Carga el Detalle del Reporte Contable.-
        public static void SyConMcd(DatosGlobales Globales,UnitOfWorkCext01 unit, int Indice)
        {
            T_MODGCON0 MODGCON0 = Globales.MODGCON0;
            T_CONTAB01 CONTAB01 = Globales.CONTAB01;
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;
            T_MODGCHQ MODGCHQ = Globales.MODGCHQ;
            T_MODGSWF MODGSWF = Globales.MODGSWF;

            int m = 0;
            int n = 0;
            int i = 0;
            int DoEvents = 0;
            int a = 0;

            // Detalle.-
            MODGCON0.VMcd = MODGCON0.VMcdNul.Copy();
            MODGCON0.VMcd.CodCct = MODGCON0.VMch.CodCct;
            MODGCON0.VMcd.CodPro = MODGCON0.VMch.CodPro;
            MODGCON0.VMcd.CodEsp = MODGCON0.VMch.CodEsp;
            MODGCON0.VMcd.CodOfi = MODGCON0.VMch.CodOfi;
            MODGCON0.VMcd.CodOpe = MODGCON0.VMch.CodOpe;
            MODGCON0.VMcd.NroRpt = MODGCON0.VMch.NroRpt;
            MODGCON0.VMcd.FecMov = MODGCON0.VMch.FecMov;
            MODGCON0.VMcd.estado = MODGCON0.VMch.estado;
            MODGCON0.VMcd.TipMcd = T_MODGCON0.CONTAB_ING;
            // -----------------------------------------------
            // Datos Generales.-
            // -----------------------------------------------
            MODGCON0.VMcd.NroImp = MODGCON0.VMcds.GetUpperBound(0) + 1;
            MODGCON0.VMcd.NemCta = CONTAB01.Est_Contab[Indice].Nemonico;
            MODGCON0.VMcd.CodMon = CONTAB01.Est_Contab[Indice].CodMoneda.ToInt();
            MODGCON0.VMcd.mtomcd = CONTAB01.Est_Contab[Indice].Monto.ToVal();
            if (CONTAB01.Est_Contab[Indice].debe != 0)
            {
                MODGCON0.VMcd.cod_dh = "D";
            }
            else
            {
                MODGCON0.VMcd.cod_dh = "H";
            }
            // -----------------------------------------------
            // Formas y Vías de Pago, Vueltos.-
            // -----------------------------------------------
            MODGCON0.VMcd.NumPar = CONTAB01.Est_Contab[Indice].Num_Partida.ToInt();     // ScS.-
            MODGCON0.VMcd.TipMov = CONTAB01.Est_Contab[Indice].Tipo_Mov;
            MODGCON0.VMcd.OfiDes = CONTAB01.Est_Contab[Indice].Of_Origen.ToInt();
            if (MODGCON0.VMcd.SwiBco == "")
            {
                MODGCON0.VMcd.numcct = MODGPYF0.Componer(CONTAB01.Est_Contab[Indice].CtaCte, "-", "");     // CC.-
            }
            else
            {
                MODGCON0.VMcd.numcct = CONTAB01.Est_Contab[Indice].CtaCte;     // CC.-
            }
            MODGCON0.VMcd.NroRef = CONTAB01.Est_Contab[Indice].Num_Ref;
            MODGCON0.VMcd.SwiBco = CONTAB01.Est_Contab[Indice].Swift;
            MODGCON0.VMcd.CodBco = CONTAB01.Est_Contab[Indice].CodBco;
            MODGCON0.VMcd.IdnCta = CONTAB01.Est_Contab[Indice].Ind_Cuenta;
            switch (CONTAB01.Est_Contab[Indice].Ind_Cuenta)
            {
                // Cheques al Haber.-
                case T_CONTAB01.CHMEBCH:
                    if (CONTAB01.Est_Contab[Indice].debe == 0)
                    {
                        i = CONTAB01.Est_Contab[Indice].Ind_ChqSwf;
                        MODGCON0.VMcd.SwiBco = MODGCHQ.V_Chq_VVi[i].SwfPag;
                        MODGCON0.VMcd.CodBco = MODGCHQ.V_Chq_VVi[i].BcoPag;
                        MODGCON0.VMcd.numcct = MODGCHQ.V_Chq_VVi[i].NumCta;
                        MODGCON0.VMcd.NroRef = MODGCON0.VMcd.CodCct + MODGCON0.VMcd.CodPro + MODGCON0.VMcd.CodEsp + MODGCON0.VMcd.CodOfi + MODGCON0.VMcd.CodOpe;
                        if (MODGCHQ.V_Chq_VVi[i].ChqEmi != 0)
                        {
                            MODGCON0.VMcd.NemCta = "CHEME";
                        }
                    }
                    else
                    {
                        n = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.Get_VNom(Globales,unit, MODGCON0.VMcd.SwiBco, MODGCON0.VMcd.CodMon);
                        //Si retorna -1 no encontro coicidencia con las moneda y el swift
                        if (n > -1 && MODGTAB0.VNom[n].Nom_Emi != 0)
                        {
                            MODGCON0.VMcd.NemCta = "CHEME";
                        }
                    }
                    // OP al Haber.-
                    break;
                case T_CONTAB01.OPOP:
                case T_CONTAB01.OPEPEND:
                    if (CONTAB01.Est_Contab[Indice].debe == 0)
                    {
                        i = CONTAB01.Est_Contab[Indice].Ind_ChqSwf - 1;
                        MODGCON0.VMcd.SwiBco = MODGSWF.VSwf[i].DatSwf.SwfCor;
                        MODGCON0.VMcd.CodBco = MODGSWF.VSwf[i].DatSwf.BcoCor;
                        MODGCON0.VMcd.numcct = MODGSWF.VSwf[i].DatSwf.CtaCor;
                        MODGCON0.VMcd.NroRef = MODGSWF.VSwf[i].DatSwf.RefOpe;
                        MODGCON0.VMcd.FecVen = MODGSWF.VSwf[i].DatSwf.FecPag;
                        // Cambia la Cuenta Contable a OpePend.-
                        if (MigrationSupport.Utils.Format(MODGSWF.VSwf[i].DatSwf.FecPag, "dd/mm/yyyy") != MigrationSupport.Utils.Format(DateTime.Now, "dd/mm/yyyy"))
                        {
                            MODGCON0.VMcd.IdnCta = T_MODGCON0.IdCta_OPEPEND;
                            MODGCON0.VMcd.NemCta = "OPEPEND";
                        }
                    }
                    // OPC al Haber.-
                    break;
                case T_CONTAB01.OPC:
                    if (CONTAB01.Est_Contab[Indice].debe == 0)
                    {
                        i = CONTAB01.Est_Contab[Indice].Ind_ChqSwf;
                        MODGCON0.VMcd.SwiBco = MODGSWF.VSwf[i].BcoAla.SwfBco;
                        MODGCON0.VMcd.NroRef = MODGSWF.VSwf[i].DatSwf.NroAla;
                    }
                    MODGCON0.VMcd.CodBco = T_MODGCON0.CodBcoBC;
                    // Cuenta Corriente Banco Central.-
                    break;
                case T_CONTAB01.CTACTEBC:
                    MODGCON0.VMcd.CodBco = T_MODGCON0.CodBcoBC;
                    // Varios Acreedores.-
                    break;
                case T_CONTAB01.VAM:
                case T_CONTAB01.VAX:
                case T_CONTAB01.VAMC:
                    MODGCON0.VMcd.PrtCli = CONTAB01.Est_Contab[Indice].Party.LlaveArchivo;
                    break;
                default:
                    // Obligaciones 
                    if(unit.SceRepository.EsObligacion(CONTAB01.Est_Contab[Indice].Ind_Cuenta))
                    {
                        MODGCON0.VMcd.FecVen = CONTAB01.Est_Contab[Indice].fecVen;
                        break;
                    }
                    // Cuenta Corriente (Igual para Debe y Haber).-
                    if (CONTAB01.Est_Contab[Indice].CtaCte != "")
                    {
                        MODGCON0.VMcd.PrtCli = CONTAB01.Est_Contab[Indice].Party.LlaveArchivo;
                        MODGCON0.VMcd.rutcli = CONTAB01.Est_Contab[Indice].Party.Rut;
                    }
                    break;
            }

            // 
            // ------------------------------------------------------------
            // Se lee el Tipo de Cambio del Día para las Cuentas CAMBIOXX.-
            // ------------------------------------------------------------
            if (MODGCON0.VMcd.NemCta.Left(6) == "CAMBIO")
            {
                if (BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.SyGet_Vmd(Globales,unit, MigrationSupport.Utils.Format(DateTime.Now, "dd/mm/yyyy"), (MODGCON0.VMcd.NemCta.Mid(7, 2)).ToInt()) != 0)
                {
                    MODGCON0.VMcd.TipCam = MigrationSupport.Utils.Format(MODGTAB0.VVmd.VmdObs / MODGTAB0.VVmd.VmdPrd, "0.0000").ToDbl();
                }
            }

            m = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.Get_VMnd(Globales, unit, (short)MODGCON0.VMcd.CodMon);
            MODGCON0.VMcd.NemMon = MODGTAB0.VMnd[m].Mnd_MndNmc;

            n = BCH.Comex.Core.BL.XGGL.Modulos.MODGCON0.GetIndMcd(Globales,unit);

        }
    }
}
