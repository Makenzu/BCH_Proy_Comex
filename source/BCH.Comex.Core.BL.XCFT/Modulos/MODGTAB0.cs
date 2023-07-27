using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODGTAB0
    {
        public static T_MODGTAB0 GetMODGTAB0()
        {
            return new T_MODGTAB0();
        }

        //Retorno > 0 : Indice en VMnd del Código de Moneda.
        //Retorno = 0 : No se encontró el  Código de Moneda.
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short Get_VMndBC(T_MODGTAB0 MODGTAB0, UnitOfWorkCext01 unit, short Codigo)
        {
            short n = 0;
            short i = 0;
            short Y = 0;
            
            n = (short)VB6Helpers.UBound(MODGTAB0.VMnd);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            if (n == 0)
            {
                Y = SyGetn_Mnd(MODGTAB0, unit);
            }
            for (i = 0; i <= (short)VB6Helpers.UBound(MODGTAB0.VMnd); i++)
            {
                if (MODGTAB0.VMnd[i].Mnd_MndCbc == Codigo)
                {
                    return i;
                }

            }

            return 0;
        }

        //Retorno > 0 : Indice en VAcr del Código Swift.
        //Retorno = 0 : No se encontró el  Código Swift.
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short Get_VAcr(InitializationObject initObject, UnitOfWorkCext01 unit, string Swift, short Moneda)
        {
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            short n = 0;
            short i = 0;
            short Y = 0;
            
            n = (short)VB6Helpers.UBound(MODGTAB0.VAcr);
            if (n == -1)
            {
                Y = SyGetn_Acr(initObject, unit);
            }
            for (i = 0; i <= (short)VB6Helpers.UBound(MODGTAB0.VAcr); i++)
            {
                if (MODGTAB0.VAcr[i].acr_swf == Swift && MODGTAB0.VAcr[i].acr_mda == Moneda)
                {
                    return i;
                }

            }

            return 0;
        }

        public static short SyGetn_Acr(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;

            try
            {
                MODGTAB0.VAcr = unit.SceRepository.EjecutarSP<sce_acr_s05_MS_Result>("sce_acr_s05_MS").Select(x => new T_Acr()
                {
                    acr_bco = (short)x.acr_bco,
                    acr_mda = x.acr_mda,
                    acr_swf = x.acr_swf
                }).ToArray();
                return -1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public static short SyGetn_Mnd(T_MODGTAB0 MODGTAB0, UnitOfWorkCext01 unit)
        {
            const string cacheKey = "VMndCache";
            short _retValue;
            try
            { 
                var cache = MemoryCache.Default;
                if (!cache.Contains(cacheKey))
                //if (T_MODGTAB0.VMndCache == null)
                {
                    string MndSinDec = Mdl_Acceso.GetConfigValue("FundTransfer.General.MndSinDec");
                    var result = unit.SgtRepository.EjecutarSP<sgt_mnd_s02_MS_Result>("sgt_mnd_s02_MS").Select(x => new T_Mnd()
                    {
                        Mnd_MndCbc = (short)x.mnd_mndcbc,
                        Mnd_MndCod = (short)x.mnd_mndcod,
                        Mnd_MndNmc = x.mnd_mndnmc,
                        Mnd_MndNom = x.mnd_mndnom.Trim(),
                        Mnd_MndSwf = x.mnd_mndswf,
                        Mnd_MndSin = (short)(MndSinDec.IndexOf(x.mnd_mndcod.ToString("000")) != -1 ? -1 : 0)
                    }).ToArray();
                    
                    cache.Set(cacheKey, result, DateTimeOffset.Now.AddMinutes(10));
                }
                MODGTAB0.VMnd = cache[cacheKey] as T_Mnd[];
                //Procedemos a crear un diccionario para mejorar la búsqueda
                T_MODGTAB0.VMndDict = new Dictionary<short, int>();
                for (int i = 0; i < MODGTAB0.VMnd.Length; i++)
                {
                    T_MODGTAB0.VMndDict.Add(MODGTAB0.VMnd[i].Mnd_MndCod, i);
                }
                _retValue = -1;
                
            }
            catch (Exception ex)
            {
                _retValue = 0;
            }
            return _retValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MODGTAB0"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static short SyGetn_MndPai(T_MODGTAB0 MODGTAB0, UnitOfWorkCext01 unit)
        {
            using (Tracer tracer = new Tracer("SyGetn_MndPai"))
            {
                const string cacheKey = "VMndPaiCache";
                short _retValue;
                try
                {
                    var cache = MemoryCache.Default;
                    if (!cache.Contains(cacheKey))
                    {
                        var result = unit.SgtRepository.sgt_mnd_s03_MS().Select(x => new T_MndPai()
                        {
                            mnd_mndcod = (short)x.mnd_mndcod,
                            mnd_mndcbc = (short)x.mnd_mndcbc,
                            mnd_mndnom = x.mnd_mndnom,
                            mnd_mndnmc = x.mnd_mndnmc,
                            mnd_mndsbf = (short)x.mnd_mndsbf,
                            mnd_mndswf = x.mnd_mndswf,
                            mnd_mndpai = (short)x.mnd_mndpai,
                            mnd_mndfiv = x.mnd_mndfiv,
                            mnd_mndftv = x.mnd_mndftv,
                            mnd_mndina = x.mnd_mndina
                        }).ToArray();

                        cache.Set(cacheKey, result, DateTimeOffset.Now.AddDays(1));
                    }
                    MODGTAB0.VMndPai = cache[cacheKey] as T_MndPai[];
                    //Procedemos a crear un diccionario para mejorar la búsqueda
                    T_MODGTAB0.VMndPaiDict = new Dictionary<short, int>();
                    for (int i = 0; i < MODGTAB0.VMndPai.Length; i++)
                    {
                        T_MODGTAB0.VMndPaiDict.Add(MODGTAB0.VMndPai[i].mnd_mndcod, i);
                    }
                    _retValue = -1;

                }
                catch (Exception ex)
                {
                    _retValue = 0;
                    tracer.TraceException("Error al cargar las monedas de los paises.", ex);
                }
                return _retValue;
            }
        }

        /// <summary>
        /// Lee la tabla Sgt_Pai y la deja en la estructura VPai().
        /// Retorno =  True    => Exitoso.
        ///            False   => Erróneo.
        /// </summary>
        /// <returns></returns>
        public static short SyGetn_Pai(T_MODGTAB0 MODGTB0, UnitOfWorkCext01 unit) 
        {
            short _retValue;
            const string cacheKey = "VPaiCache";
            try
            {
                var cache = MemoryCache.Default;
                if (!cache.Contains(cacheKey))
                {
                    IList<sgt_pai_s02_MS_Result> resultados = unit.SgtRepository.sgt_pai_s02_MS();
                    var result = resultados.Select(x => new T_Pai()
                    {
                        Pai_PaiCod = (short)x.pai_paicod,
                        Pai_PaiNom = x.pai_painom,
                        Pai_PaiAla = (short)x.pai_paiala
                    }).OrderBy(x => x.Pai_PaiNom).ToArray();
                    cache.Set(cacheKey, result, DateTimeOffset.Now.AddDays(1));
                }
                MODGTB0.VPai = cache[cacheKey] as T_Pai[];
                
                _retValue = -1;
                
            }
            catch (Exception ex)
            {
                _retValue = 0;
            }
            return _retValue;
        }

        //Lee Vlores (Paridad y Tipo de Cambio) de la Tabla Sgt_Vmd.
        //Retorno =  True    => Exitoso.
        //           False   => Erróneo.
        public static short SyGet_Vmd(T_MODGTAB0 MODGTAB0, UnitOfWorkCext01 unit, string FecVmd, short CodMnd)
        {
            short _retValue;
            try
            {
                DateTime auxfec;
                DateTime? fec = null;
                if(DateTime.TryParse(FecVmd, out auxfec))
                {
                    fec = auxfec;
                }

                var result = unit.SgtRepository.sgt_vmd_s02_MS(fec, CodMnd);
                if (result != null)
                {
                    MODGTAB0.VVmd.VmdCod = CodMnd;
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
        public static short Get_VMnd(T_MODGTAB0 MODGTAB0, UnitOfWorkCext01 unit, short Codigo)
        {
            short n = 0;
            short i = 0;
            short Y = 0;
            
            n = (short)VB6Helpers.UBound(MODGTAB0.VMnd);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            if (n == 0)
            {
                Y = SyGetn_Mnd(MODGTAB0, unit);
            }
            var elemento = MODGTAB0.VMnd.Select((item, index) => new { Id = index, cod = item.Mnd_MndCod }).Where(c => c.cod == Codigo).FirstOrDefault();
            if (elemento != null)
            {
                return (short)elemento.Id;
            }
            //for (i = 0; i <= (short)VB6Helpers.UBound(MODGTAB0.VMnd); i++)
            //{
            //    if (MODGTAB0.VMnd[i].Mnd_MndCod == Codigo)
            //    {
            //        return i;
            //    }

            //}

            return 0;
        }

        //Deja el contenido del arreglo de Paises en una lista. --> UI_Combo
        public static void CargaEnLista_Pai(T_MODGTAB0 MODGTAB0, UI_Combo combo, UnitOfWorkCext01 unit)
        {
            short X = SyGetn_Pai(MODGTAB0, unit);
            combo.Items = MODGTAB0.VPai.Select(x => new UI_ComboItem() 
            {
                Data = x.Pai_PaiCod,
                ID = x.Pai_PaiCod.ToString(),
                Value = x.Pai_PaiNom.Trim()
            }).ToList();
        }

        public static dynamic SyGetSecEc(T_MODGTAB0 MODGTAB0, UnitOfWorkCext01 unit)
        {
            const string cacheKey = "SecEcCache";
            dynamic _retValue = null;
            short n = 0;

            var cache = MemoryCache.Default;
            //Verifica lectura previa de países.
            //n = (short)VB6Helpers.UBound(MODGTAB0.SecEc);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            //if (n > 0)
            //{
            //    _retValue = true;
            //    return _retValue;
            //}
            try
            {
                if (!cache.Contains(cacheKey))
                {
                    var result = unit.SceRepository.EjecutarSP<sce_sec_s01_MS_Result>("sce_sec_s01_MS").Select(x => new T_SecEc()
                    {
                        CodSec = (short)x.codsec,
                        NomSec = x.nomsec.Trim()
                    }).ToArray();
                    cache.Set(cacheKey, result, DateTimeOffset.Now.AddDays(1));
                }
                MODGTAB0.SecEc = cache[cacheKey] as T_SecEc[];
                _retValue = true;
            }
            catch (Exception e)
            {

            }
            return _retValue;
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
            string R = "";
            short i = 0;
            short n = 0;
            string Que = "";
            const string cacheKey = "VFerCache";
            //Verifica lectura previa de países.
            var cache = MemoryCache.Default;
            if (cache.Contains(cacheKey))
            {
                MODGTAB0.VFer = cache[cacheKey] as string[];
                _retValue = (short)(true ? -1 : 0);
                return _retValue;
            }

            //n = (short)VB6Helpers.UBound(MODGTAB0.VFer);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            //if (n > 0)
            //{
            //    _retValue = (short)(true ? -1 : 0);
            //    return _retValue;
            //}
            try
            {
                var result = unit.SceRepository.EjecutarSP<DateTime>("sce_fer_s01").Select(x => x.ToString("yyyy-MM-dd")).ToArray();
                cache.Set(cacheKey, result, DateTimeOffset.Now.AddDays(1));
                MODGTAB0.VFer = cache[cacheKey] as string[];
            }
            catch (Exception e)
            {
                _retValue = 0;
            }
            return _retValue;           
        }

        //****************************************************************************
        //   1.  Esta función calcula el último día hábil de un mes enviado como
        //       parámetro.
        //****************************************************************************
        public static string Fn_Calcula_Dia_Habil(T_MODGTAB0 MODGTAB0, UnitOfWorkCext01 unit, string año, string mes)
        {
            short DiaHabil = 0;
            string Dias = "";
            short Verdadero = 0;
            string Febrero = "";
            string Dias_Meses = "";
            var cache = MemoryCache.Default;
            if (cache.Contains(año + "-" + mes))
            //if (T_MODGTAB0.CalculaDiaHabilCache.ContainsKey(año + "-" + mes))
            {
                return cache[año + "-" + mes] as string;
            }

            //Determinar si el año es bisciesto.
            if ((VB6Helpers.Val(año) % 4 == 0 && VB6Helpers.Val(año) % 100 != 0) || VB6Helpers.Val(año) % 400 == 0)
            {
                Febrero = "29;";
            }
            else
            {
                Febrero = "28;";
            }

            //Registra el máximo de días de un mes.
            Dias_Meses = "31;" + Febrero + "31;30;31;30;31;31;30;31;30;31;";

            Dias = VB6Helpers.Trim(MODGPYF0.copiardestring(Dias_Meses, ";", (short)VB6Helpers.Val(mes)));
            DiaHabil = VB6Helpers.Weekday(VB6Helpers.CDate(VB6Helpers.Format(Dias + "/" + mes + "/" + año, "dd/MM/yyyy")));

            Verdadero = (short)(true ? -1 : 0);
            while (Verdadero != 0)
            {

                //¿Es día es hábil?.
                if (DiaHabil == 1 || DiaHabil == 7)
                {
                    Dias = VB6Helpers.Trim(VB6Helpers.Str(VB6Helpers.Val(Dias) - 1));
                    DiaHabil = VB6Helpers.Weekday(VB6Helpers.CDate(VB6Helpers.Format(Dias + "/" + mes + "/" + año, "dd/MM/yyyy")));
                    Verdadero = (short)(true ? -1 : 0);
                }
                else
                {
                    //¿Es Feriado?.
                    if (Fn_Buscar_Fecha_Fer(MODGTAB0, unit, VB6Helpers.Format(Dias + "/" + mes + "/" + año, "dd/MM/yyyy")) == 0)
                    {
                        Dias = VB6Helpers.Trim(VB6Helpers.Str(VB6Helpers.Val(Dias) - 1));
                        DiaHabil = VB6Helpers.Weekday(VB6Helpers.CDate(VB6Helpers.Format(Dias + "/" + mes + "/" + año, "dd/MM/yyyy")));
                        Verdadero = (short)(true ? -1 : 0);
                    }
                    else
                    {
                        Verdadero = (short)(false ? -1 : 0);
                    }

                }

            }
            var result = VB6Helpers.Trim(VB6Helpers.Format(año + "-" + mes + "-" + Dias, "yyyy-mm-dd"));
            cache.Set(año + "-" + mes, result, DateTimeOffset.Now.AddDays(1));
            //T_MODGTAB0.CalculaDiaHabilCache[año + "-" + mes] = result;
            return result;
        }


        //****************************************************************************
        //   1.  Esta Función busca una fecha (que el usuario ingreso )en el arreglo
        //       de los días feriados del año. Ocurriendo esto así se saca directamente
        //       de esta función para enviar un mensaje de aviso y enviando el foco
        //       al campo fecha determinado.
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short Fn_Buscar_Fecha_Fer(T_MODGTAB0 MODGTAB0, UnitOfWorkCext01 unit, string strFecha)
        {
            DateTime fecha = DateTime.Parse(strFecha);
            return Fn_Buscar_Fecha_Fer(MODGTAB0, unit, fecha);
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

        public static void CargaEnLista_Mnd(T_MODGTAB0 MODGTAB0, UI_Combo Lista)
        {
            short i = 0;
            for (i = 0; i < (short)VB6Helpers.UBound(MODGTAB0.VMnd); i++)
            {
                Lista.Items.Add(new UI_ComboItem()
                {
                    Value = MODGTAB0.VMnd[i].Mnd_MndNom,
                    ID = MODGTAB0.VMnd[i].Mnd_MndCod.ToString(),
                    Data = MODGTAB0.VMnd[i].Mnd_MndCod
                });
            }

        }
        /// <summary>
        /// Retorno >  0 : Indice en VNom del Código Swift.
        /// Retorno = -1 : No se encontró el  Código Swift.
        /// </summary>
        /// <param name="initObject"></param>
        /// <param name="unit"></param>
        /// <param name="Swift"></param>
        /// <param name="Moneda"></param>
        /// <returns></returns>
        public static short Get_VNom(InitializationObject initObject, UnitOfWorkCext01 unit, string Swift, short Moneda)
        {
            short Y = 0;
            short n = 0;
            n = (short)VB6Helpers.UBound(initObject.MODGTAB0.VNom);
            Y = SyGetn_Nom(initObject, unit);
            return (short)initObject.MODGTAB0.VNom.ToList().FindIndex(x => x.Nom_Swf.Equals(Swift,StringComparison.InvariantCultureIgnoreCase) && x.Nom_Mda == Moneda);
        }

        //Lee la Tabla Nómina Sce_Nom y la deja en la estructura VNom().
        //Retorno =  True    => Exitoso.
        //           False   => Erróneo.
        public static short SyGetn_Nom(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            short _retValue = 0;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;

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
                _retValue = 0;
            }
            return _retValue;
            
        }

        //Retorno > 0 : Indice en VPai del Código de País.
        //Retorno = 0 : No se encontró el  Código de País.
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short Get_VPai(InitializationObject initObj, UnitOfWorkCext01 uow, short Codigo)
        {
            short n = 0;
            short i = 0;
            short X = 0;
            
            n = (short)initObj.MODGTAB0.VPai.Length;
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            if (n == 0)
            {
                X = SyGetn_Pai(initObj.MODGTAB0, uow);
            }

            for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODGTAB0.VPai); i++)
            {
                if (initObj.MODGTAB0.VPai[i].Pai_PaiCod == Codigo)
                {
                    return i;
                }
            }
            return 0;
        }

        //Retorna el nombre de la moneda especificada.-
        public static string Get_NomMnd(InitializationObject initObj, short CodMnd)
        {
            short i = 0;
            for (i = 1; i <= (short)VB6Helpers.UBound(initObj.MODGTAB0.VMnd); i++)
            {
                if (initObj.MODGTAB0.VMnd[i].Mnd_MndCod == CodMnd)
                {
                    return initObj.MODGTAB0.VMnd[i].Mnd_MndNom;
                }
            }
            return "";
        }

        //Retorna el nemónico de la moneda especificada.-
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static string Get_NemMnd(T_MODGTAB0 MODGTAB0, UnitOfWorkCext01 unit, short CodMnd)
        {
            short n = 0;
            short i = 0;
            short X = 0;
            
            n = (short)VB6Helpers.UBound(MODGTAB0.VMnd);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            if (n == 0)
            {
                X = SyGetn_Mnd(MODGTAB0, unit);
            }

            for (i = 0; i <= (short)VB6Helpers.UBound(MODGTAB0.VMnd); i++)
            {
                if (MODGTAB0.VMnd[i].Mnd_MndCod == CodMnd)
                {
                    return MODGTAB0.VMnd[i].Mnd_MndNmc;
                }

            }

            return "";
        }

        public static short Fecha_Habil(InitializationObject initObj, UnitOfWorkCext01 uow, ref string Fecha)
        {
            short _retValue = 0;
            short X = SyGetn_VFer(initObj.MODGTAB0, uow);
            short Fecha_Paso = 0;
            short i = 0;
            double fech = 0;
            _retValue = (short)(false ? -1 : 0);

            if(!string.IsNullOrEmpty(Fecha))
            {
                fech = MODGPYF1.ValidaFecha(Fecha);
                if (fech == 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Informacion, Text = "La Fecha ingresada es incorrecta.", Title = "Atención" });
                    return _retValue;
                }

                Fecha = VB6Helpers.Format(VB6Helpers.CStr(fech), "yyyy-mm-dd");

                //La fecha no puede ser sábado ni domingo
                Fecha_Paso = VB6Helpers.Weekday(VB6Helpers.CDate(Fecha));
                if (Fecha_Paso == 1 || Fecha_Paso == 7)
                {
                    //Sólo si es fin de semana
                    //MsgBox "La Fecha no puede ser de un fin de semana.", vbInformation, "Atención"
                    return _retValue;
                }

                //Buscar si la Fecha es Feriado.
                for (i = 0; i < (short)VB6Helpers.UBound(initObj.MODGTAB0.VFer); i++)
                {
                    if (VB6Helpers.Format(initObj.MODGTAB0.VFer[i], "yyyy-mm-dd") == Fecha)
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Informacion, Text = "La Fecha no puede ser un Feriado.", Title = "Atención" });
                        return _retValue;
                    }
                }
            }
            return (short)(true ? -1 : 0);
        }

        //****************************************************************************
        //   1.  Esta función calcula el penúltimo día hábil de un mes enviado como
        //       parámetro.
        //****************************************************************************
        public static string Fn_Penultimo_Dia_Habil(InitializationObject initObject, UnitOfWorkCext01 unit, string año, string mes)
        {
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;

            short DiaHabil = 0;
            string Febrero = "";
            string Dias = "";
            short Verdadero = 0;
            string fecfer = "";
            string Dias_Meses = "";
            short Paso = 0;
            //Determinar si el año es bisciesto.
            if ((VB6Helpers.Val(año) % 4 == 0 && VB6Helpers.Val(año) % 100 != 0) || VB6Helpers.Val(año) % 400 == 0)
            {
                Febrero = "29;";
            }
            else
            {
                Febrero = "28;";
            }

            //Registra el máximo de días de un mes.
            Dias_Meses = "31;" + Febrero + "31;30;31;30;31;31;30;31;30;31;";

            Dias = VB6Helpers.Trim(MODGPYF0.copiardestring(Dias_Meses, ";", (short)VB6Helpers.Val(mes)));
            DiaHabil = VB6Helpers.Weekday(VB6Helpers.CDate(VB6Helpers.Format(Dias + "/" + mes + "/" + año, "dd/MM/yyyy")));

            Paso = (short)(false ? -1 : 0);
            Verdadero = (short)(true ? -1 : 0);
            while (Verdadero != 0)
            {

                //¿Es día es hábil?.
                if (DiaHabil == 1 || DiaHabil == 7)
                {
                    Dias = VB6Helpers.Trim(VB6Helpers.Str(VB6Helpers.Val(Dias) - 1));
                    DiaHabil = VB6Helpers.Weekday(VB6Helpers.CDate(VB6Helpers.Format(Dias + "/" + mes + "/" + año, "dd/MM/yyyy")));
                    Verdadero = (short)(true ? -1 : 0);
                }
                else
                {
                    //¿Es Feriado?.
                    fecfer = VB6Helpers.Format(Dias + "/" + mes + "/" + año, "dd/MM/yyyy");
                    if (Fn_Buscar_Fecha_Fer(MODGTAB0, unit, VB6Helpers.Trim(fecfer)) == 0)
                    {
                        Dias = VB6Helpers.Trim(VB6Helpers.Str(VB6Helpers.Val(Dias) - 1));
                        DiaHabil = VB6Helpers.Weekday(VB6Helpers.CDate(VB6Helpers.Format(Dias + "/" + mes + "/" + año, "dd/MM/yyyy")));
                        Verdadero = (short)(true ? -1 : 0);
                    }
                    else
                    {
                        if (Paso != 0)
                        {
                            Verdadero = (short)(false ? -1 : 0);
                        }
                        else
                        {
                            Dias = VB6Helpers.Trim(VB6Helpers.Str(VB6Helpers.Val(Dias) - 1));
                            DiaHabil = VB6Helpers.Weekday(VB6Helpers.CDate(VB6Helpers.Format(Dias + "/" + mes + "/" + año, "dd/MM/yyyy")));
                            Paso = (short)(true ? -1 : 0);
                        }

                    }

                }

            }

            return VB6Helpers.Trim(VB6Helpers.Format(Dias + "/" + mes + "/" + año, "dd/MM/yyyy"));
        }

        //****************************************************************************
        //   1.  Retorno > 0 : Indice en VBco del Código del Banco.
        //   2.  Retorno = 0 : No se encontró el  Código Banco.
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short Get_Bco(InitializationObject initObject,UnitOfWorkCext01 unit, short Codigo)
        {
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;

            short n = 0;
            short i = 0;
            short X = 0;
            
            n = (short)VB6Helpers.UBound(MODGTAB0.VBco);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            if (n == 0)
            {
                X = SyGetn_Bco(initObject,unit);
            }
            for (i = 0; i <= (short)VB6Helpers.UBound(MODGTAB0.VBco); i++)
            {
                if (MODGTAB0.VBco[i].CodBco == Codigo)
                {
                    return i;
                }

            }

            return -1;
        }

        //******************************************************
        //   1.  Lee todos los Bancos dejandolos en un arreglo.-
        //******************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short SyGetn_Bco(InitializationObject initObject,UnitOfWorkCext01 unit)
        {
            using (var tracer = new Tracer())
            {
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;

            short _retValue = 0;
            short n = 0;
            short i = 0;
            string Que = "";
            string R = "";
            //Verifica que ya se hayan leído los Bancos.-
            n = (short)VB6Helpers.UBound(MODGTAB0.VBco);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            try
            {
                if (n > 0)
                {
                    _retValue = (short)(true ? -1 : 0);
                }
                else
                {
                    MODGTAB0.VBco = unit.SceRepository.EjecutarSP<sce_bco_s01_MS_Result>("sce_bco_s01_MS").Select(x => new T_Bco()
                    {
                        CodBco = (short)x.codbco,
                        NomBco = x.nombco
                    }).ToArray();
                    _retValue = (short)(true ? -1 : 0);
                    if (MODGTAB0.VBco.Length == 0)
                    {
                            initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                            Text = "No se han encontrado datos en la Tabla de los Bancos (Sce_Bco).",
                            Type = TipoMensaje.Error
                        });
                        _retValue = 0;
                    }
                }
            }
                catch (Exception e)
            {
                    tracer.TraceException("Alerta", e);

                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                        Text = "Se ha producido un error al tratar de leer la Tabla de los Bancos (Sce_Bco).",
                        Type = TipoMensaje.Error
                });
                _retValue = 0;
            }
            return _retValue;
        }
        }

        //trae los corresponsales y los deja en VCor
        public static void SyGetn_Cor(T_MODGTAB0 mod, UnitOfWorkCext01 uow)
        {
            IList<T_Cor> corresponsales = uow.SceRepository.sce_cor_s03_MS();
            mod.VCor = corresponsales.ToArray();
        }


        //****************************************************************************
        //   1.  Retorno > 0 : Indice en VNom del Código Swift.
        //   2.  Retorno = 0 : No se encontró el  Código Swift.
        //****************************************************************************
        public static short Fn_Get_VNom(InitializationObject initObject,UnitOfWorkCext01 unit, string Swift)
        {
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;

            short n = 0;
            short i = 0;
            short Y = 0;
            
            n = (short)VB6Helpers.UBound(MODGTAB0.VNom);
            if (n == -1)
            {
                Y = SyGetn_Nom(initObject,unit);
            }
            for (i = 0; i <= (short)VB6Helpers.UBound(MODGTAB0.VNom); i++)
            {
                if (MODGTAB0.VNom[i].Nom_Swf == Swift)
                {
                    return i;
                }

            }

            return 0;
        }
    }
}
