using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODGTAB0
    {
        // Lee la tabla Sgt_Pai y la deja en la estructura VPai().
        // Retorno =  True    => Exitoso.
        //            False   => Erróneo.
        public static int SyGetn_Pai(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            using(var tracer = new Tracer("SyGetn_Pai"))
            {
                T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;

                int SyGetn_Pai = 0;

                int DoEvents = 0;
                int X = 0;
                int RegLei = 0;
                int i = 0;
                string R = "";
                string Que = "";
                int n = 0;

                // Verifica lectura previa de países.
                n = MODGTAB0.VPai.GetUpperBound(0);
                
                if (n > 0)
                {
                    SyGetn_Pai = Convert.ToInt16(true);
                    return SyGetn_Pai;
                }
                try
                {
                    var res = unit.SceRepository.EjecutarSP<sgt_pai_s02_Result>("sgt_pai_s02");
                    if (res.Count == 0)
                    {
                        tracer.AddToContext("Paises", "No se han encontrado datos en la Tabla de Países (Sgt_Pai).");
                        Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                        {
                            Type = Common.UI_Modulos.TipoMensaje.Error,
                            Text = "No se han encontrado datos en la Tabla de Países (Sgt_Pai)."
                        });
                    }
                    res.Insert(0, new sgt_pai_s02_Result());
                    MODGTAB0.VPai = res.Select(x => new T_Pai()
                    {
                        Pai_PaiCod = (short)x.pai_paicod,
                        Pai_PaiNom = x.pai_painom,
                        Pai_PaiAla = (short)x.pai_paiala,
                        Estado = (short)RegLei
                    }).OrderBy(x => x == null ? "AAA" : x.Pai_PaiNom).ToArray();
                    SyGetn_Pai = true.ToInt();
                }
                catch(Exception ex)
                {
                    tracer.AddToContext("Excepcion", ex.Message);
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type = Common.UI_Modulos.TipoMensaje.Error,
                        Text = "Se ha producido un error al tratar de leer la Tabla de Países(Sgt_Pai)."
                    });
                    return SyGetn_Pai;
                }

                return SyGetn_Pai;
            }
        }


        // Lee la tabla Sgt_Pai y la deja en la estructura VMnd().
        // Retorno =  True    => Exitoso.
        //            False   => Erróneo.
        public static int SyGetn_Mnd(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            using(var tracer = new Tracer("SyGetn_Mnd"))
            {
                T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;

                int SyGetn_Mnd = 0;

                int i = 0;
                string MndSinDec = "";
                string R = "";
                string Que = "";
                int n = 0;

                // Verifica Lectura previa de monedas.
                //MigrationSupport.Utils.ResumeNext(() =>
                //{
                //    n = MODGTAB0.VMnd.GetUpperBound(0);
                //});
                //if (n > 0)
                //{
                //    SyGetn_Mnd = true.ToInt();
                //    return SyGetn_Mnd;
                //}
                //Siempre se va a buscar las monedas a la bd, se vacia MODGTAB0.VMnd
                MODGTAB0.VMnd = new T_Mnd[0];
                try
                {
                    var res = unit.SceRepository.EjecutarSP<sgt_mnd_s02_MS_Result>("sgt_mnd_s02_MS");
                    if (res.Count == 0)
                    {
                        Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                        {
                            Type = Common.UI_Modulos.TipoMensaje.Error,
                            Title = "No se han encontrado datos en la Tabla de Monedas (Sgt_Mnd)."
                        });
                    }
                    else
                    {
                        MndSinDec = MODGPYF0.GetSceIni("General", "MndSinDec");
                        res.Insert(0, new sgt_mnd_s02_MS_Result());
                        MODGTAB0.VMnd = res.Select(x => new T_Mnd()
                        {
                            Mnd_MndCod = (short)x.mnd_mndcod,
                            Mnd_MndCbc = (short)x.mnd_mndcbc,
                            Mnd_MndNom = x.mnd_mndnom,
                            Mnd_MndNmc = x.mnd_mndnmc,
                            Mnd_MndSwf = x.mnd_mndswf,
                            Mnd_MndSin = (short)(MndSinDec.Split(';').Contains(MigrationSupport.Utils.Format(x.mnd_mndcod.ToString(), "000")) ? -1 : 0)
                        }).ToArray();
                        //Procedemos a crear un diccionario para mejorar la búsqueda
                        T_MODGTAB0.VMndDict = new Dictionary<short, int>();
                        for (int z = 0; z < MODGTAB0.VMnd.Length; z++)
                        {
                            //T_MODGTAB0.VMndDict.Add(MODGTAB0.VMnd[z].Mnd_MndCod, z);
                            T_MODGTAB0.VMndDict[MODGTAB0.VMnd[z].Mnd_MndCod] = z;
                        }
                        SyGetn_Mnd = Convert.ToInt16(true);
                    }
                }
                catch(Exception e)
                {
                    tracer.AddToContext("Excepcion", e.Message);
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type = Common.UI_Modulos.TipoMensaje.Error,
                        Text = "Se ha producido un error al tratar de leer la Tabla de Monedas (Sgt_Mnd)."
                    });
                }
                return SyGetn_Mnd;
            }
        }

        //Lee la Tabla Nómina Sce_Nom y la deja en la estructura VNom().
        //Retorno =  True    => Exitoso.
        //           False   => Erróneo.
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short SyGetn_Nom(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            using(var tracer = new Tracer("SyGetn_Nom"))
            {
                short _retValue = 0;
                T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;

                try
                {
                    if (MODGTAB0.VNom.Length > 0)
                    {
                        return -1;
                    }
                    MODGTAB0.VNom = unit.SceRepository.EjecutarSP<sce_nom_s01_MS_Result>("sce_nom_s01_MS").Select(x => new T_Nom()
                    {
                        Nom_Pai = (short)x.nom_pai,
                        Nom_Mda = (short)x.nom_mda,
                        Nom_Swf = x.nom_swf,
                        Nom_Bco = (short)x.nom_bco,
                        Nom_cta = x.nom_cta,
                        Nom_Act = (short)(x.nom_act ? -1 : 0),
                        Nom_Ala = (short)(x.nom_ala ? -1 : 0),
                        Nom_Emi = (short)(x.nom_emi ? -1 : 0)
                    }).ToArray();
                    _retValue = -1;
                }
                catch (Exception ex)
                {
                    tracer.AddToContext("Excepcion", ex.Message);
                    _retValue = 0;
                }
                return _retValue;
            }
        }

        //trae los corresponsales y los deja en VCor
        public static short SyGetn_Cor(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            using(var tracer = new Tracer("SyGetn_Cor"))
            {
                try
                {
                    Globales.MODGTAB0.VCor = unit.SceRepository.EjecutarSP<sce_cor_s03_MS_Result>("sce_cor_s03_MS")
                                            .Select(x => new T_Cor()
                                            {
                                                Cor_Swf = x.cor_swf.Trim(),
                                                Cor_Nom = x.cor_nom.Trim(),
                                                Cor_Ciu = x.cor_ciu.Trim(),
                                                Cor_Dir = x.cor_dir.Trim(),
                                                Cor_Pos = x.cor_pos.Trim(),
                                                Cor_Pai = x.cor_pai.Trim(),
                                                Cor_CPa = (short)x.cor_cpa
                                            }).ToArray();
                    return -1;
                }
                catch(Exception ex)
                {
                    tracer.AddToContext("Excepcion", ex.Message);
                    return 0;
                }
            }      
        }

        //****************************************************************************
        //   1.  Lee las Fechas Feriadas correspondientes a las del año 1995
        //       ingresadas en el archivo Sce_Fer las que son cargadas en un
        //       arreglo para su posterior uso.
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short SyGetn_VFer(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            using(var tracer = new Tracer("SyGetn_VFer"))
            {
                T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;

                short _retValue = 0;
                string R = "";
                short i = 0;
                short n = 0;
                string Que = "";
                //Verifica lectura previa de países.

                n = (short)VB6Helpers.UBound(MODGTAB0.VFer);
                // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
                // IGNORED: On Error GoTo 0
                if (n > 0)
                {
                    _retValue = (short)(true ? -1 : 0);
                    return _retValue;
                }
                try
                {
                    MODGTAB0.VFer = unit.SceRepository.EjecutarSP<DateTime>("sce_fer_s01").Select(x => x.ToString("yyyy-MM-dd")).ToArray();
                }
                catch (Exception e)
                {
                    tracer.AddToContext("Excepcion", e.Message);
                    _retValue = 0;
                }
                return _retValue;
            }
            
        }

        // ****************************************************************************
        //    1.  Retorno > 0 : Indice en VNom del Código Swift.
        //    2.  Retorno = 0 : No se encontró el  Código Swift.
        // ****************************************************************************
        public static int Fn_Get_VNom(DatosGlobales Globales,UnitOfWorkCext01 unit, string Swift)
        {
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;

            int Fn_Get_VNom = 0;

            int i = 0;
            int y = 0;
            int n = 0;

            n = MODGTAB0.VNom.GetUpperBound(0);
            
            if (n == 0)
            {
                y = SyGetn_Nom(Globales,unit);
            }
            for (i = 1; i <= MODGTAB0.VNom.GetUpperBound(0); i += 1)
            {
                if (MODGTAB0.VNom[i].Nom_Swf == Swift)
                {
                    Fn_Get_VNom = i;
                    return Fn_Get_VNom;
                }
            }

            return Fn_Get_VNom;
        }

        // Retorno > -1 : Indice en VNom del Código Swift.
        // Retorno = -1 : No se encontró el  Código Swift.
        public static int Get_VNom(DatosGlobales Globales, UnitOfWorkCext01 unit, string Swift, int moneda)
        {
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;
            //Si retorna -1 es porque no encontro conicidencia con la moneda y swift
            int Get_VNom = -1;

            int i = 0;
            int y = 0;
            int n = 0;

            n = MODGTAB0.VNom.GetUpperBound(0);
            var localSwift = Swift ?? string.Empty;

            // If n% = 0 Then y% = SyGetn_Nom()
            y = SyGetn_Nom(Globales,unit);
            for (i = 0; i <= MODGTAB0.VNom.GetUpperBound(0); i += 1)
            {
                if (MODGTAB0.VNom[i].Nom_Swf.ToLower() == localSwift.ToLower() && MODGTAB0.VNom[i].Nom_Mda == moneda)
                {
                    return i;
                }
            }
            return Get_VNom;
        }

        //Lee Vlores (Paridad y Tipo de Cambio) de la Tabla Sgt_Vmd.
        //Retorno =  True    => Exitoso.
        //           False   => Erróneo.
        public static short SyGet_Vmd(DatosGlobales Globales, UnitOfWorkCext01 unit, string FecVmd, int CodMnd)
        {
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;
            short _retValue;
            try
            {
                DateTime auxfec;
                DateTime? fec = null;
                if (DateTime.TryParse(FecVmd, out auxfec))
                {
                    fec = auxfec;
                }

                var result = unit.SgtRepository.sgt_vmd_s02_MS(fec, CodMnd);
                if (result != null)
                {
                    MODGTAB0.VVmd.VmdCod = (short)CodMnd;
                    MODGTAB0.VVmd.VmdFec = FecVmd;
                    MODGTAB0.VVmd.VmdMbc = (double)result.vmd_vmdmbc;
                    MODGTAB0.VVmd.VmdMbv = (double)result.vmd_vmdmbv;
                    MODGTAB0.VVmd.VmdMcc = (double)result.vmd_vmdmcc;
                    MODGTAB0.VVmd.VmdMcv = (double)result.vmd_vmdmcv;
                    MODGTAB0.VVmd.VmdPrd = (double)result.vmd_vmdprd;
                    MODGTAB0.VVmd.VmdAcd = (double)result.vmd_vmdacd;
                    MODGTAB0.VVmd.VmdObs = (double)result.vmd_vmdobs;

                    _retValue = -1;
                }
                else
                {
                    _retValue = 0;
                }
            }
            catch (Exception ex)
            {
                _retValue = 0;
            }

            return _retValue;
        }

        //Retorno > 0 : Indice en VMnd del Código de Moneda.
        //Retorno = 0 : No se encontró el  Código de Moneda.
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short Get_VMnd(DatosGlobales Globales, UnitOfWorkCext01 unit, int Codigo)
        {
            short n = 0;
            short i = 0;
            short Y = 0;
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;

            n = (short)VB6Helpers.UBound(MODGTAB0.VMnd);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            if (n == 0)
            {
                Y = (short)SyGetn_Mnd(Globales, unit);
            }
            for (i = 1; i <= (short)VB6Helpers.UBound(MODGTAB0.VMnd); i++)
            {
                if (MODGTAB0.VMnd[i].Mnd_MndCod == Codigo)
                {
                    return i;
                }

            }

            return 0;
        }

        //Retorna el nemónico de la moneda especificada.-
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static string Get_NemMnd(DatosGlobales Globales, UnitOfWorkCext01 unit, int CodMnd)
        {
            short n = 0;
            short i = 0;
            short X = 0;
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;

            n = (short)VB6Helpers.UBound(MODGTAB0.VMnd);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            if (n == 0)
            {
                X = (short)SyGetn_Mnd(Globales, unit);
            }

            for (i = 1; i <= (short)VB6Helpers.UBound(MODGTAB0.VMnd); i++)
            {
                if (MODGTAB0.VMnd[i].Mnd_MndCod == CodMnd)
                {
                    return MODGTAB0.VMnd[i].Mnd_MndNmc;
                }

            }

            return "";
        }

        // Lee un Banco Corresponsal.-
        // Retorno =  True    => Exitoso.-
        //            False   => Erróneo.-
        public static int SyGet_Cor(DatosGlobales Globales,UnitOfWorkCext01 unit, string Swift)
        {
            int SyGet_Cor = 0;
            try
            {
                T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;
                
                string R = "";
                string Que = "";

                // Se construye la Consulta.
                MODGTAB0.WCor = MODGTAB0.WCorNul.Copy();
                var res = unit.SceRepository.EjecutarSP<sce_cor_s01_Result>("sce_cor_s01", Swift).First();

                // Asigna los valores.-
                MODGTAB0.WCor.CorSwf = res.cor_swf;
                MODGTAB0.WCor.CorNom = res.cor_nom;
                MODGTAB0.WCor.CorCiu = res.cor_ciu;
                MODGTAB0.WCor.CorDes = res.cor_des;
                MODGTAB0.WCor.CorDir = res.cor_dir;
                MODGTAB0.WCor.CorPos = res.cor_pos;
                MODGTAB0.WCor.CorPai = res.cor_pai;
                MODGTAB0.WCor.CorBco = res.cor_bco.ToShort();
                MODGTAB0.WCor.CorCPa = res.cor_cpa.ToShort();

                SyGet_Cor = true.ToInt();
                
            }
            catch
            {
                Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                {
                    Title= T_MODGTAB0.MsgTab0,
                    Text= "Se ha producido un error al tratar de leer la Tabla de Corresponsales (Sce_Cor).",
                    Type=Common.UI_Modulos.TipoMensaje.Error
                });
            }
            return SyGet_Cor;
        }

        public static short Fn_Buscar_Fecha_Fer(T_MODGTAB0 MODGTAB0, UnitOfWorkCext01 unit, DateTime fecha)
        {
            //cargo los feriados si aun no estan cargados
            if (MODGTAB0.VFer == null || MODGTAB0.VFer.Length == 0)
            {
                SyGetn_VFer(MODGTAB0, unit);
            }

            bool esFeriado = MODGTAB0.VFer.Where(f => DateTime.Parse(f) == fecha).Any();
            return (short)(esFeriado ? 0 : -1);
        }

        public static bool FechaEsFeriado(T_MODGTAB0 mod, UnitOfWorkCext01 uow, DateTime fecha)
        {
            short result = Fn_Buscar_Fecha_Fer(mod, uow, fecha);
            return (result == 0); //el 0 es feriado
        }

        //****************************************************************************
        //   1.  Lee las Fechas Feriadas correspondientes a las del año 1995
        //       ingresadas en el archivo Sce_Fer las que son cargadas en un
        //       arreglo para su posterior uso.
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short SyGetn_VFer(T_MODGTAB0 MODGTAB0, UnitOfWorkCext01 unit)
        {
            short _retValue = 0;
            short n = 0;
            //Verifica lectura previa de países.

            n = (short)VB6Helpers.UBound(MODGTAB0.VFer);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            if (n > 0)
            {
                _retValue = (short)(true ? -1 : 0);
                return _retValue;
            }
            try
            {
                MODGTAB0.VFer = unit.SceRepository.EjecutarSP<DateTime>("sce_fer_s01").Select(x => x.ToString("yyyy-MM-dd")).ToArray();
            }
            catch (Exception e)
            {
                _retValue = 0;
            }
            return _retValue;
        }

        public static void CargaEnLista_Pai(DatosGlobales Globales,UnitOfWorkCext01 unit, UI_Combo lista)
        {
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;
            SyGetn_Pai(Globales,unit);
            for (int i = 1; i <= MODGTAB0.VPai.GetUpperBound(0); i += 1)
            {
                lista.AddItem(MODGTAB0.VPai[i].Pai_PaiCod,MODGPYF1.Minuscula(MODGTAB0.VPai[i].Pai_PaiNom));
            }
        }


    }
}
