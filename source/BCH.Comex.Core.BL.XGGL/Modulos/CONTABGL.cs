using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class CONTABGL
    {
        // Procedimiento que carga los valores de los Cheques.-
        // Requiere las Tablas :
        //  Sgt_Pai
        //  Sce_Nom
        //  Sce_Cor
        public static bool InterfazSwf(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;
            T_MODGMTS MODGMTS = Globales.MODGMTS;
            T_CONTAB01 CONTAB01 = Globales.CONTAB01;
            T_MODGSWF MODGSWF = Globales.MODGSWF;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;

            string d = "";
            int j = 0;
            int a = 0;

            // Carga las Tablas.-
            a = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.SyGetn_Pai(Globales, unit);     // Países.-
            a = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.SyGetn_Nom(Globales, unit);     // Nómina.-
            a = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.SyGetn_Cor(Globales, unit);     // Corresponsales.-

            // Carga otras Tablas.-
                if (MODGTAB0.VFer.GetUpperBound(0) == 0)
                {
                    a = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.SyGetn_VFer(Globales, unit);     // Feriados.-
                }     // Feriados.-
                if (MODGMTS.VCCs.GetUpperBound(0) == 0)
                {
                    a = BCH.Comex.Core.BL.XGGL.Modulos.MODGMTS.SyGetn_VCcs(Globales,unit);     // Campos Swift.-
                }     // Campos Swift.-

            IList<Tipo_Contabilidad> contabilidadRequierenSwift = GetContabilidadesRequierenSwift(CONTAB01);

            if (contabilidadRequierenSwift.Any())
            {
                // Se cargan los Documentos según Vías.-
                if (MODGSWF.VSwf == null || MODGSWF.VSwf.Length == 0 || MODGSWF.VSwf.Length > contabilidadRequierenSwift.Count)
                {
                    MODGSWF.VSwf = new T_Swf[contabilidadRequierenSwift.Count];
                    MODGSWF.VMT103 = new T_mt103[contabilidadRequierenSwift.Count];
                }
                else if (MODGSWF.VSwf.Length < contabilidadRequierenSwift.Count)
                {
                    Array.Resize(ref MODGSWF.VSwf, contabilidadRequierenSwift.Count);
                    Array.Resize(ref MODGSWF.VMT103, contabilidadRequierenSwift.Count);
                }

                // Recorre Módulo de Vías y Vueltos buscando las Id-Cuenta para las Ordenes de Pago.-
                int i = 0;
                foreach (Tipo_Contabilidad contab in contabilidadRequierenSwift)
                {
                    //if (contab.Ind_ChqSwf == 0) //si no esta generado
                    //{
                        contab.Ind_ChqSwf = (short)(i + 1);
                        T_Swf swift = new T_Swf();
                        T_mt103 monto = new T_mt103();

                        swift.mtoswf = contab.Monto.ToVal();     // Monto del Swift.-
                        swift.CodMon = contab.CodMoneda.ToShort();     // Código de Moneda del Swift.-
                        if (contab.Ind_Cuenta == T_CONTAB01.OPOP)
                        {
                            swift.EsAladi = false;     // OP Otros Países.-
                        }
                        else
                        {
                            swift.EsAladi = true;     // OP Aladi.-
                        }

                        swift.SwfMon = contab.SwfMoneda;

                        monto.MtoOri = swift.mtoswf;
                        monto.MndOri = swift.CodMon;

                        MODGSWF.VSwf[i] = swift;
                        MODGSWF.VMT103[i] = monto;
                   // }

                    i++;
                }
            }
                      

            // Carga los posibles beneficiarios de los Swift's.-
            MODGSWF.VBenSwf = new T_BenSwf[1];
            MODGSWF.VBenSwf[0] = new T_BenSwf() { IndBen = 0, FunBen = "Beneficiario" };

            if(MODGSWF.VCliSwf == null)
            {
                T_CliSwf cliente = new T_CliSwf();
                MODGSWF.VCliSwf = cliente;
            }

            // Carga el Cliente.-
            MODGSWF.VCliSwf.NomCli = SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].NombreUsado;
            MODGSWF.VCliSwf.rutcli = SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].Rut;
            MODGSWF.VCliSwf.DirCli1 = SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].DireccionUsado;
            MODGSWF.VCliSwf.DirCli2 = SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].CiudadUsado;
            MODGSWF.VCliSwf.PaiCli = SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].PaisUsado;
            MODGSWF.VCliSwf.CiuCli = SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].CiudadUsado.ToUpper();

            if (Globales.MODXORI != null)
            {
                var ope = Globales.MODXORI.gs_ctacte_party;
                if (ope != null)
                {
                    MODGSWF.VCliSwf.CtaCli = ope;
                }
                else
                {
                    MODGSWF.VCliSwf.CtaCli = "";
                }
            }
            else
            {
                MODGSWF.VCliSwf.CtaCli = MODXORI.Get_CtaCte(unit, MODGSWF.VCliSwf.rutcli.TrimStart('0').PadRight(12, '|'));
            }
            MODGSWF.VCliSwf.CtaCli = MODGSWF.VCliSwf.CtaCli.TrimStart('0');

            MODGSWF.VGSwf.NumOpe = BCH.Comex.Core.BL.XGGL.Modulos.MODGCHQ.Referencia(Globales);
            
            return true;
        }

        public static IList<Tipo_Contabilidad> GetContabilidadesRequierenSwift(T_CONTAB01 contab)
        {
            return contab.Est_Contab.Where(x => (x.modulo == T_CONTAB01.MODULO_VIAS || x.modulo == T_CONTAB01.MODULO_VUELTO) && (x.Ind_Cuenta == T_CONTAB01.OPOP || x.Ind_Cuenta == T_CONTAB01.OPC) && x.Borrado == 0).ToList();
        }


        // Procedimiento que carga los valores de los Cheques.-
        // Requiere las Tablas :
        //  Sgt_Pai
        //  Sce_Nom
        //  Sce_Cor
        public static void InterfazChq(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            T_MODGCHQ MODGCHQ = Globales.MODGCHQ;
            T_CONTAB01 CONTAB01 = Globales.CONTAB01;
            T_GLOBAL GLOBAL = Globales.GLOBAL;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;

            int m = 0;
            int j = 0;
            int i = 0;
            int a = 0;

            // Carga las Tablas.-
            a = MODGTAB0.SyGetn_Pai(Globales,unit);
            a = MODGTAB0.SyGetn_Nom(Globales, unit);
            a = MODGTAB0.SyGetn_Cor(Globales, unit);

            // Si los Cheques NO están cargados => Se cargan según Vías.-
            if (MODGCHQ.V_Chq_VVi.GetUpperBound(0) == 0)
            {
                for (i = 0; i <= CONTAB01.Est_Contab.GetUpperBound(0); i += 1)
                {
                    // Recorre Módulo de Vías y Vueltos buscando las Id-Cuenta para Cheques y Vales Vistas.-
                    if (CONTAB01.Est_Contab[i].modulo == T_CONTAB01.MODULO_VIAS || CONTAB01.Est_Contab[i].modulo == T_CONTAB01.MODULO_VUELTO)
                    {
                        if (CONTAB01.Est_Contab[i].Ind_Cuenta == T_CONTAB01.CHMNBCH || CONTAB01.Est_Contab[i].Ind_Cuenta == T_CONTAB01.CHMEBCH || CONTAB01.Est_Contab[i].Ind_Cuenta == T_CONTAB01.VVBCH)
                        {
                            j = MODGCHQ.V_Chq_VVi.GetUpperBound(0) + 1;
                            Array.Resize(ref MODGCHQ.V_Chq_VVi, j + 1);
                            MODGCHQ.V_Chq_VVi[j] = new Chq_Vvi();
                            MODGCHQ.V_Chq_VVi[j].MtoDoc = CONTAB01.Est_Contab[i].Monto.ToVal();     // Monto  del Documento.-
                            MODGCHQ.V_Chq_VVi[j].CodMon = CONTAB01.Est_Contab[i].CodMoneda.ToInt();     // Moneda del Documento.-
                            CONTAB01.Est_Contab[i].Ind_ChqSwf = j;
                            if (CONTAB01.Est_Contab[i].Ind_Cuenta == T_CONTAB01.VVBCH)
                            {
                                MODGCHQ.V_Chq_VVi[j].TipoDoc = "VV";     // Es Vale Vista.-
                            }
                            else
                            {
                                MODGCHQ.V_Chq_VVi[j].TipoDoc = "CH";     // Es Cheque.-
                            }
                        }
                    }
                }
            }

            // Carga los posibles Beneficiarios.-
            // On Error Resume Next
            // m% = UBound(BenDocVal)
            // On Error GoTo 0
            // If m% = 0 Then
            //     ReDim BenDocVal(1)
            //     BenDocVal(1).FunBen = "Beneficiario"
            //     BenDocVal(1).RutTom = SoloNumeros(PartysOpe(ITom).Rut, 10)
            // End If
            // 
            // Carga los posibles Beneficiarios.-
            MODGCHQ.BenDocVal = new Type_BenDocVal[1];
            for (i = 0; i <= GLOBAL.Beneficiario.GetUpperBound(0); i += 1)
            {
                if (SYGETPRT.PartysOpe[i].NombreUsado != "")
                {
                    m = MODGCHQ.BenDocVal.GetUpperBound(0) + 1;
                    Array.Resize(ref MODGCHQ.BenDocVal, m + 1);
                    MODGCHQ.BenDocVal[m] = new Type_BenDocVal();
                    MODGCHQ.BenDocVal[m].FunBen = GLOBAL.Beneficiario[i];
                    MODGCHQ.BenDocVal[m].NomBen = SYGETPRT.PartysOpe[i].NombreUsado;
                    MODGCHQ.BenDocVal[m].RutTom = BCH.Comex.Core.BL.XGGL.Modulos.MODGCHQ.SoloNumeros(SYGETPRT.PartysOpe[MODGCHQ.ITom].Rut, 10);
                    MODGCHQ.BenDocVal[m].PaiBen = SYGETPRT.PartysOpe[i].codpais;
                }
            }
            m = MODGCHQ.BenDocVal.GetUpperBound(0) + 1;
            Array.Resize(ref MODGCHQ.BenDocVal, m + 1);
            MODGCHQ.BenDocVal[m] = new Type_BenDocVal();
            MODGCHQ.BenDocVal[m].FunBen = "Beneficiario";

        }


        // Lee los datos de una cuenta de Nemónico CUENTA.NEM.-
        public static int Lee_CtaCbe(DatosGlobales Globales,UnitOfWorkCext01 unit, string Parametro)
        {
            int Lee_CtaCbe = 0;
            T_CONTAB01 CONTAB01 = Globales.CONTAB01;
            T_MODGNCTA MODGNCTA = Globales.MODGNCTA;
            int n = 0;
            try
            {

                n = BCH.Comex.Core.BL.XGGL.Modulos.MODGNCTA.Get_Cta(Parametro,Globales,unit);
                if (n == 0)
                {
                    return Lee_CtaCbe;
                }
                CONTAB01.Cuenta.Nem = MODGNCTA.VCta[n].Cta_Nem;
                CONTAB01.Cuenta.Mon = MODGNCTA.VCta[n].Cta_Mon;
                CONTAB01.Cuenta.num = MODGNCTA.VCta[n].Cta_Num;
                CONTAB01.Cuenta.Nom = MODGNCTA.VCta[n].Cta_Nom;
                CONTAB01.Cuenta.gl = MODGNCTA.VCta[n].Cta_GL;
                CONTAB01.Cuenta.NroTO = MODGNCTA.VCta[n].Cta_NroTO;
                CONTAB01.Cuenta.IndTO = MODGNCTA.VCta[n].Cta_IndTO;
                CONTAB01.Cuenta.CIT = MODGNCTA.VCta[n].Cta_CIT;
                CONTAB01.Cuenta.CVT = MODGNCTA.VCta[n].Cta_CVT;
                CONTAB01.Cuenta.CAP = MODGNCTA.VCta[n].Cta_CAP;
                CONTAB01.Cuenta.CTD = MODGNCTA.VCta[n].Cta_CTD;
                CONTAB01.Cuenta.pos = MODGNCTA.VCta[n].Cta_POS;
                CONTAB01.Cuenta.CDR = MODGNCTA.VCta[n].Cta_CDR;
                //  Performance
                //  01-06-2009
                //  Se agrega elemento para determinar cuenta vigenteable
                CONTAB01.Cuenta.Vig = MODGNCTA.VCta[n].Cta_Vig;
                Lee_CtaCbe = true.ToInt();

                return Lee_CtaCbe;

            }
            catch (Exception exc)
            {
                
            }
            return Lee_CtaCbe;
        }
    }
}
