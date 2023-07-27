using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Services;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODXORI
    {
        public static T_MODXORI GetMODXORI() {
            return new T_MODXORI();
        }

        /// <summary>
        /// Lee los datos necesarios para el codigo extendido seleccionado.
        /// </summary>
        /// <returns></returns>
        public static bool SyGetn_Codtran(T_MODXORI tmodxori, UnitOfWorkCext01 uow)
        {
            try
            {
                if (!T_MODXORI.Vx_CodTranCache.ContainsKey(DateTime.Today))
                {
                    var codTranList = uow.SceRepository.pro_sce_codtran_s01_MS();
                    List<T_CodTran> lst = new List<T_CodTran>();
                    foreach (var item in codTranList)
                    {
                        lst.Add(new T_CodTran
                        {
                            cod_trx_cosmos = item.cod_trx_cosmos,
                            cr_dr = item.cr_dr,
                            glosa_cosmos = item.glosa_cosmos,
                            Moneda = item.moneda,
                            nro_trx = (short)item.nro_trx,
                            tip_cta = item.tip_cta
                        });
                    }
                    T_MODXORI.Vx_CodTranCache[DateTime.Today] = lst.ToArray();
                }
                //Genera Sentencia.
                tmodxori.Vx_CodTran = T_MODXORI.Vx_CodTranCache[DateTime.Today];

                return true;
            }
            catch (Exception ex) //TODO:@estanislao: modificar rever el tema de manejo de excepciones
            {
                //throw new Exception("Se ha producido un error al tratar de leer los datos de las Ctas. Contables. ", ex);
                return false;
            }
        }


        //Inicializa estructura de Orígenes de Fondos.-
        public static void Pr_Init_xOri(T_MODXORI MODXORI)
        {
            MODXORI.VxOri = new T_xOri[0];
            MODXORI.VxMtoOri = new T_xMtoOri[0];
            MODXORI.VgxOri = MODXORI.VgxOriNul;
        }

        public static bool SyGet_CtaCte(string Party, InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            bool _retValue = false;
            short extranjera = 0;

            sce_ctas_s03_MS_Result ctaParty;

            try
            {
                ctaParty = uow.SceRepository.sce_ctas_s03_MS(Party);

                if (ctaParty != null)
                {
                    initObj.MODXORI.gs_ctacte_party = ctaParty.cuenta;
                    extranjera = (short)(ctaParty.extranjera ? 1 : 0);

                    _retValue = true;
                }
            }
            catch (Exception)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message{
                    Text = "Se ha producido un error al tratar de leer los datos de las Ctas. Contables.",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODGTAB1.MsgTab
                });
            }

            return _retValue;
        }

        public static string SrvGetCtaCos(string nCta)
        {
            using (var tracer = new Tracer())
            {
            string token = null;

            try
            {
                 token = XCFTServices.ConsultaCuentaCorriente(nCta);
            }
                catch (Exception e)
            {
                    tracer.TraceException("Alerta", e);
            }

            return token;
        }
        }

        private static void Pr_Carga_OtroNem(InitializationObject initObj)
        {
            //short n = 0;
            var VOvdList = initObj.MODXORI.VOvd.ToList();
            //n = (short)(VB6Helpers.UBound(VOvd) + 1);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            var element = new T_Ovd { NumCta = T_MODGCON0.IdCta_ONMN, monnac = -1, CtaOri = -1, CtaVia = -1, CtaVue = 0 };
            element.NemCta = string.Empty;
            element.NomCta = "Otro Nemónico Moneda Nacional";
            VOvdList.Add(element);

            element = new T_Ovd { NumCta = T_MODGCON0.IdCta_ONME, monnac = 0, CtaOri = -1, CtaVia = -1, CtaVue = 0 };
            element.NemCta = string.Empty;
            element.NomCta = "Otro Nemónico Moneda Extranjera";
            VOvdList.Add(element);

            initObj.MODXORI.VOvd = VOvdList.ToArray();
        }

        //****************************************************************************
        //   1.  TipMon corresponde al Tipo de Moneda para las cuentas a cargar
        //       1      : Moneda Nacional.
        //       2      : Moneda Extranjera.
        //       0      : Moneda Nacional y Extranjera.
        //       Origen : Indica si cargar cuentas de Origen.
        //       Via    : Indica si cargar cuentas de Via.
        //       Vuelto : Indica si cargar cuentas de Vuelto.
        //********Carga_l_cta********************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        // UPGRADE_INFO (#0561): The 'TipoMon' symbol was defined without an explicit "As" clause.
        // UPGRADE_INFO (#0561): The 'origen' symbol was defined without an explicit "As" clause.
        // UPGRADE_INFO (#0561): The 'Via' symbol was defined without an explicit "As" clause.
        public static void Carga_l_cta(InitializationObject initObj, UI_Combo Lista, int TipoMon, bool origen, bool Via, short Vuelto, UnitOfWorkCext01 unit)
        {
            short x = 0;
            short i = 0;
            short o = 0;
            bool Lc_Existe = false;
            //Si no están cargadas las cuentas => Se cargan. SE CARGAN SIEMPRE PORQUE SACAN TODO LO QUE TENIA 
            //VB6Helpers.Redim(ref initObj.MODXORI.VOvd, 0, 0);
            if (VB6Helpers.UBound(initObj.MODXORI.VOvd) == -1)
            {
                SyGetn_Ovd(initObj, unit);
                Pr_Carga_OtroNem(initObj);
            }

            
            Lista.Items.Clear();
            
            for (i = 0; i < initObj.MODXORI.VOvd.Length; i++)
            {
                // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'TipoMon'. Consider using the GetDefaultMember6 helper method.
                if (((TipoMon == 0 ? -1 : 0) | ((TipoMon == 1 ? -1 : 0) & initObj.MODXORI.VOvd[i].monnac) | ((TipoMon == 2 ? -1 : 0) & ~(initObj.MODXORI.VOvd[i].monnac==-1 ? -1 : 0))) != 0)
                {
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'origen'. Consider using the GetDefaultMember6 helper method.
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Via'. Consider using the GetDefaultMember6 helper method.
                    if (((Convert.ToInt16(origen) & initObj.MODXORI.VOvd[i].CtaOri) | (Convert.ToInt16(Via) & initObj.MODXORI.VOvd[i].CtaVia) | (Vuelto & initObj.MODXORI.VOvd[i].CtaVue)) != 0)
                    {
                        Lc_Existe = false;
                        if (Lista.Items.Count > 0)
                        {
                            for (o = 0; o <= Lista.Items.Count - 1; o++)
                            {
                                if (Lista.Items[o].Value == initObj.MODXORI.VOvd[i].NomCta)
                                {
                                    Lc_Existe = true;
                                    break;
                                }
                            }
                        }

                        if (Lc_Existe == false)
                        {
                            Lista.Items.Add(new UI_ComboItem { Value = initObj.MODXORI.VOvd[i].NomCta, Tag = initObj.MODXORI.VOvd[i], ID = i.ToString(), Data=i});
                        }
                    }
                }
            }
            Lista.ListIndex = 0;
        }

        //Tabla de Origen Vía Destino
        //Retorno    <> ""  : Lectura Exitosa.-
        //           =  ""  : Error o Lectura no Exitosa.-
        public static void SyGetn_Ovd(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            using (var tracer = new Tracer())
            {
            List<pro_sce_ovd_ft_s01_MS_Result> result;
            try
            {

                if (initObj.MODXORI.gb_esCosmos)
                {
                    if (initObj.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
                    {
                        result = unit.SceRepository.EjecutarSP<pro_sce_ovd_ft_s01_MS_Result>("pro_sce_ovd_ft_s01_MS", "1", "0");
                        //Que = "Exec " + Mdl_SRM.ParamSrm8k.Base + "." + Mdl_SRM.ParamSrm8k.Usuario + "." + "pro_sce_ovd_ft_s01_MS 1, 0";
                    }
                    else
                    {
                        result = unit.SceRepository.EjecutarSP<pro_sce_ovd_ft_s01_MS_Result>("pro_sce_ovd_ft_s01_MS", "1", "1");
                        //Que = "Exec " + Mdl_SRM.ParamSrm8k.Base + "." + Mdl_SRM.ParamSrm8k.Usuario + "." + "pro_sce_ovd_ft_s01_MS 1, 1";
                    }

                }
                else
                {
                    result = unit.SceRepository.EjecutarSP<pro_sce_ovd_ft_s01_MS_Result>("pro_sce_ovd_ft_s01_MS", "0", "0");
                    //Que = "Exec " + Mdl_SRM.ParamSrm8k.Base + "." + Mdl_SRM.ParamSrm8k.Usuario + "." + "pro_sce_ovd_ft_s01_MS 0, 0";
                }


                    initObj.MODXORI.VOvd = result.Select(item => new T_Ovd()
                    {
                    NumCta = (short)item.numcta,
                    NomCta = item.nomcta.ToString(),// item.nomcta,
                    NemCta = item.nemcta.ToString(),//item.nemcta,
                    monnac = (short)Convert.ToInt16(item.monnac) == 1 ? (short)-1 : (short)0,
                    CtaOri = Convert.ToInt16(item.ctaori),
                    CtaVia = Convert.ToInt16(item.ctavia),
                    CtaVue = Convert.ToInt16(item.ctavue),
                    codtra = item.codtra != null ? item.codtra : string.Empty
                }).ToArray();
                }
                catch (Exception e)
            {
                    tracer.TraceException("Alerta", e);
                }
            }       
        }

        public static bool SyGet_CtaCte(InitializationObject initObj, UnitOfWorkCext01 unit, string Party)
        {
            try
            {
                var result = unit.SceRepository.sce_ctas_s03_MS(Party);
                if (result.cuenta == null)
                {
                    return false;
                }
                initObj.MODXORI.gs_ctacte_party = result.cuenta;
            }
            catch (Exception ex)
            {
                return false;
            }
            
            return true;
        }

        //****************************************************************************
        //   1.  Lee las Sucursales para luego cargarlas en un arreglo de Oficinas.
        //   2.  Si el resultado es 1(True)  => Lectura Exitosa.
        //       Si el resultado es 0(False) => Lectura No Exitosa.
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short SyGetn_Suc(T_MODXORI MODXORI,UnitOfWorkCext01 unit)
        {
            using (var tracer = new Tracer())
            {
            short _retValue = 0;
            short n = 0;
            string Que = "";
            string R = "";
            short i = 0;
            try
            {
                n = (short)VB6Helpers.UBound(MODXORI.Vx_Suc);
                // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
                // IGNORED: On Error GoTo 0

                if (n > 0)
                {
                    _retValue = (short)(true ? -1 : 0);

                }
                else
                {
                    //Genera Sentencia.

                    //MODXORI.Vx_Suc = new T_Suc[1];
                    MODXORI.Vx_Suc = unit.SgtRepository.EjecutarSP<sgt_suc_s01_MS_Result>("sgt_suc_s01_MS").Select(x => new T_Suc()
                    {
                        codsuc = (short)x.suc_succod,
                        nomsuc = x.suc_sucnom
                    }).ToArray();
                    _retValue = (short)(true ? -1 : 0);
                }
            }
                catch (Exception e)
            {
                    tracer.TraceException("Alerta", e);
                _retValue = 0;
            }

            return _retValue;
        }
        }

        //Ingresa un monto para justificar los Orígenes de Fondo.-
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        // UPGRADE_INFO (#0561): The 'Put_xOri' symbol was defined without an explicit "As" clause.
        public static dynamic Put_xOri(InitializationObject Modulos,UnitOfWorkCext01 unit, short CodMnd, double monto)
        {
            T_MODXORI MODXORI = Modulos.MODXORI;
            T_MODGTAB0 MODGTAB0 = Modulos.MODGTAB0;


            short m = 0;
            short n = 0;
            short j = 0;
            short l = 0;
            if (monto == 0)
            {
                return null;
            }

            //Obtiene dimension del arreglo.-
            
            m = (short)VB6Helpers.UBound(MODXORI.VxMtoOri);
            n = (short)VB6Helpers.UBound(MODXORI.VxOri);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            if (m == -1)
            {
                //VB6Helpers.Redim(ref MODXORI.VxMtoOri, 0, 0);
            }
            if (n == -1)
            {
                //VB6Helpers.Redim(ref MODXORI.VxOri, 0, 0);
            }
            n = -1;
            //Busca la moneda en el arreglo Vías.-
            for (j = 0; j <= (short)m; j++)
            {
                if (CodMnd == MODXORI.VxMtoOri[j].CodMon)
                {
                    n = j;
                    break;
                }

            }
            //Hay que crear la vía para la moneda.-
            if (n == -1)
            {
                n = (short)(VB6Helpers.UBound(MODXORI.VxMtoOri)+1);
                VB6Helpers.RedimPreserve(ref MODXORI.VxMtoOri, 0, n);
                MODXORI.VxMtoOri[n].CodMon = CodMnd;
                MODXORI.VxMtoOri[n].MtoTot = monto;
                l = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0,unit,MODXORI.VxMtoOri[n].CodMon);
                MODXORI.VxMtoOri[n].NemMon = MODGTAB0.VMnd[l].Mnd_MndNmc;
            }
            else
            {
                MODXORI.VxMtoOri[n].MtoTot += monto;
            }

            return true;
        }

        //****************************************************************************
        //   1.  Indica si hay montos para justificar como Orígenes.
        //****************************************************************************
        public static short HayVxOri(T_MODXORI MODXORI)
        {
            short i = 0;
            for (i = 0; i <= (short)VB6Helpers.UBound(MODXORI.VxMtoOri); i++)
            {
                if (MODXORI.VxMtoOri[i].MtoTot > 0)
                {
                    return (short)(true ? -1 : 0);
                }

            }

            return 0;
        }

        //****************************************************************************
        //   1.  Retorna el Saldo justificado para una moneda determinada.
        //****************************************************************************
        public static double Fn_SumaVxOri(InitializationObject initObject, short Moneda)
        {
            T_MODXORI MODXORI = initObject.MODXORI;
            short i = 0;
            double suma = 0;
            for (i = 0; i < MODXORI.VxOri.Length; i++)
            {
                if (MODXORI.VxOri[i].Status != T_MODXORI.ExOri_Eli && MODXORI.VxOri[i].CodMon == Moneda)
                {
                    suma += MODXORI.VxOri[i].MtoTot;
                }

            }

            return suma;
        }

        //****************************************************************************
        //   1.  Lee las Cuentas Contables dependiendo del Partys ingresado o
        //       seleccionado.
        //   2.  Si el resultado es 1(True)  => Lectura Exitosa.
        //       Si el resultado es 0(False) => Lectura No Exitosa.
        //****************************************************************************
        public static short SyGetn_Ctas(InitializationObject initObject,UnitOfWorkCext01 unit, string Party)
        {
            short _retValue = 0;
            try
            {
                // IGNORED: On Error GoTo SyGetn_CtasErr
                T_MODXORI MODXORI = initObject.MODXORI;

                //Genera Sentencia.
                MODXORI.Vx_OriCC = unit.SceRepository.EjecutarSP<sce_ctas_s03_MS_Result>("sce_ctas_s03_MS", Party).Select(x => new T_OriCC()
                {
                    ctacte = x.cuenta,
                    MonExt = (short)(x.extranjera ? -1 : 0),
                    Activa = (short)(x.activace ? -1 : 0),
                    CodMnd = (short)x.moneda
                }).ToArray();
                _retValue = (short)(true ? -1 : 0);
            }
            catch (Exception _ex)
            {
                _retValue = 0;
            }
            return _retValue;
        }

        //****************************************************************************
        //   1.  Carga en una combox todas las cuentas de un participante, sólo varía
        //       cuando es una cuenta extranjera ya que en ese caso se carga además el
        //       nemónico de la cuenta.
        //****************************************************************************
        public static void Pr_Cargar_Lista_Cuentas(InitializationObject initObject,UnitOfWorkCext01 unit, UI_Combo Lista, UI_Combo Lista_Mnd, short Indice_Paso)
        {
            T_MODXORI MODXORI = initObject.MODXORI;
            T_MODGCHQ MODGCHQ = initObject.MODGCHQ;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;

            short i = 0;
            string s = "";
            short Indice_Mnd = 0;
            short o = 0;
            short cont = 0;
            if (VB6Helpers.UBound(MODXORI.Vx_OriCC) >= 0)
            {
                Lista.Items.Clear();
                short _switchVar1 = MODXORI.VOvd[Indice_Paso].NumCta;
                if (_switchVar1 == T_MODGCON0.IdCta_CtaCteMN)
                {
                    //Cuenta Corriente M/N.
                    for (i = 0; i < MODXORI.Vx_OriCC.Length; i++)
                    {
                        if (MODXORI.Vx_OriCC[i].MonExt == 0)
                        {
                            s = "";
                            if (VB6Helpers.Len(VB6Helpers.Trim(MODXORI.Vx_OriCC[i].ctacte)) == 8)
                            {
                                //Cuenta BAE
                                MODXORI.Vx_OriCC[i].CtaCte_t = VB6Helpers.Trim(MODXORI.Vx_OriCC[i].ctacte);
                            }
                            else
                            {
                                //Cuenta BCH
                                string aux = MODXORI.Vx_OriCC[i].ctacte.Trim();
                                MODXORI.Vx_OriCC[i].CtaCte_t = VB6Helpers.Left(aux, 3) + "-" + VB6Helpers.Mid(aux, 4, 5) + "-" + VB6Helpers.Right(aux, 2);
                            }

                            Lista.Items.Add(new UI_ComboItem()
                            {
                                Value= MODXORI.Vx_OriCC[i].CtaCte_t,
                                Data=i
                            });
                        }

                    }

                }
                else if (_switchVar1 == T_MODGCON0.IdCta_CtaCteME || _switchVar1 == T_MODGCON0.IdCta_ChqCCME)
                {
                    //Cuenta Corriente M/E.
                    for (i = 0; i < MODXORI.Vx_OriCC.Length; i++)
                    {
                        if (MODXORI.Vx_OriCC[i].MonExt != 0)
                        {
                            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Lista_Mnd'. Consider using the GetDefaultMember6 helper method.
                            Indice_Mnd = (short)(Lista_Mnd.get_ItemData_(Lista_Mnd.ListIndex));
                            if (MODXORI.Vx_OriCC[i].CodMnd == Indice_Mnd)
                            {
                                s = "";
                                MODGCHQ.Indice = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0,unit, MODXORI.Vx_OriCC[i].CodMnd);
                                s = VB6Helpers.Trim(MODGTAB0.VMnd[MODGCHQ.Indice].Mnd_MndNmc) + VB6Helpers.Space(2) + VB6Helpers.Left(MODXORI.Vx_OriCC[i].ctacte, 4) + "-" + VB6Helpers.Mid(MODXORI.Vx_OriCC[i].ctacte, 5, 5) + "-" + VB6Helpers.Right(MODXORI.Vx_OriCC[i].ctacte, 2);

                                if (VB6Helpers.Len(VB6Helpers.Trim(MODXORI.Vx_OriCC[i].ctacte)) == 8)
                                {
                                    // Cuenta BAE
                                    MODXORI.Vx_OriCC[i].CtaCte_t = VB6Helpers.Trim(MODXORI.Vx_OriCC[i].ctacte);
                                }
                                else
                                {
                                    // Cuenta BCH
                                    MODXORI.Vx_OriCC[i].CtaCte_t = s;
                                }
                                Lista.Items.Add(new UI_ComboItem()
                                {
                                    Value = MODXORI.Vx_OriCC[i].CtaCte_t,
                                    Data = i
                                });
                            }

                        }

                    }

                }
                else if (_switchVar1 == T_MODGCON0.IdCta_CtaCteAUTN || _switchVar1 == T_MODGCON0.IdCta_CtaCteMANN || _switchVar1 == T_MODGCON0.IdCta_CtaCteAUTE || _switchVar1 == T_MODGCON0.IdCta_CtaCteMANE)
                {
                    for (i = 0; i < MODXORI.Vx_OriCC.Length; i++)
                    {
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Lista_Mnd'. Consider using the GetDefaultMember6 helper method.
                        Indice_Mnd = (short)Lista_Mnd.get_ItemData_(Lista_Mnd.ListIndex);
                        if (MODXORI.Vx_OriCC[i].CodMnd == Indice_Mnd)
                        {
                            MODXORI.Vx_OriCC[i].CtaCte_t = VB6Helpers.Trim(MODXORI.Vx_OriCC[i].ctacte);
                            Lista.Items.Add(new UI_ComboItem()
                            {
                                Value = MODXORI.Vx_OriCC[i].CtaCte_t,
                                Data = i
                            });
                        }

                    }

                }

                

                // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Lista'. Consider using the GetDefaultMember6 helper method.
                if (Lista.Items.Count > 0)
                {
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Lista'. Consider using the GetDefaultMember6 helper method.
                    Lista.ListIndex = 0;
                }
            }

        }

        //****************************************************************************
        //   1.  Busca en una estructura determinada el Código de una Sucursal que se
        //       ha ingresado recientemente por pantalla. Si la encuentra, retorna en
        //       el nombre de la función la Identificación de esta (el nombre de la
        //       Sucursal).
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static string Fn_Buscar_Suc(InitializationObject initObject, string Codigo)
        {
            T_MODXORI MODXORI = initObject.MODXORI;

            string _retValue = "";
            short Fin = -1;
            short i = 0;
            
            Fin = (short)MODXORI.Vx_Suc.Length;
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            for (i = 0; i < Fin; i++)
            {
                if (MODXORI.Vx_Suc[i].codsuc == VB6Helpers.Val(Codigo))
                {
                    _retValue = (VB6Helpers.Trim(MODXORI.Vx_Suc[i].nomsuc));
                    break;
                }

            }

            return _retValue;
        }

        //****************************************************************************
        //   1.  Lee las tablas de Sce_Prty de Sce_Rsa para verificar si existe un
        //       party determinado, y si existe, rescatar la identicicación de este.
        //   2.  Si el resultado es 1(True)  => Lectura Exitosa.
        //       Si el resultado es 0(False) => Lectura No Exitosa.
        //****************************************************************************
        public static string SyGet_Partys(UnitOfWorkCext01 unit, string Party)
        {
            string _retValue = "";
            string Que = "";
            string R = "";
            try
            {
                _retValue = unit.SceRepository.EjecutarSP<string>("sce_prty_s02", Party).First();
            }
            catch (Exception _ex)
            {
                _retValue = "-1";
            }
            return _retValue;
        }

        //****************************************************************************
        //   1.  Función que genera un número asociado a una operación. Esto se realiza
        //       además con la suma del número del Centro Costo más un dígito que se
        //       calcula para entregar la generación de este número.
        //****************************************************************************
        public static string Fn_Genera_Num(InitializationObject initObject,UnitOfWorkCext01 unit)
        {
            T_MODGRNG MODGRNG = initObject.MODGRNG;
            T_MODGUSR MODGUSR = initObject.MODGUSR;
            UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;
            string Num = "";
            // UPGRADE_INFO (#0561): The 'num_gen' symbol was defined without an explicit "As" clause.
            dynamic num_gen = null;
            string dv = "";
            double nro = BCH.Comex.Core.BL.XCFT.Modulos.MODGRNG.LeeSceRng(MODGRNG,MODGUSR,Mdi_Principal,unit, T_MODGRNG.Rng_SconSu);

            if (nro == -1)
            {
                Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type=TipoMensaje.Error,
                    Text= "Error al generar el número de partida.  Debe elegir otro tipo de movimiento u otra cuenta contable."
                });
                return "";
            }

            Num = VB6Helpers.Format(VB6Helpers.CStr(nro));

            if (VB6Helpers.Len(Num) > 3)
            {
                Num = VB6Helpers.Right(Num, 3);
            }

            num_gen = MODGUSR.UsrEsp.CentroCosto + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Val(Num)), "000");

            dv = "";
            dv = Fn_Calcula_Dig(VB6Helpers.Trim(VB6Helpers.CStr(num_gen)));

            return num_gen + dv;
        }

        //****************************************************************************
        //   1.  Función que calcula el dígito de una cuenta enviado por medio
        //       de un parámetro.
        //****************************************************************************
        public static string Fn_Calcula_Dig(string nums)
        {
            short i = 0;
            // UPGRADE_INFO (#0561): The 'suma' symbol was defined without an explicit "As" clause.
            dynamic suma = null;
            // UPGRADE_INFO (#0561): The 'digito' symbol was defined without an explicit "As" clause.
            dynamic digito = null;
            short Resultado = 0;
            // UPGRADE_INFO (#0561): The 'cuales' symbol was defined without an explicit "As" clause.
            const string cuales = "765432";

            for (i = 1; i <= 6; i++)
            {
                suma = Format.StringToDouble(suma) + (VB6Helpers.Val(VB6Helpers.Mid(nums, i, 1)) * VB6Helpers.Val(VB6Helpers.Mid(cuales, i, 1)));
            }

            Resultado = (short)(Format.StringToDouble(suma) % 11);

            digito = 11 - Resultado;

            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'digito'. Consider using the GetDefaultMember6 helper method.
            if (Format.StringToDouble(digito) == 11)
            {
                return "00";
            }
            else
            {
                return VB6Helpers.Format(VB6Helpers.CStr(digito), "00");
            }

        }

        //****************************************************************************
        //   1.  Función que Valida un Banco Aladi.
        //****************************************************************************'
        public static short Fn_Valida_Aladi(string Num, InitializationObject initObject)
        {
            short _retValue = 0;
            string dv = "";
            short mul = 0;
            short Sum = 0;
            short i = 0;
            short x = 0;
            string el_num = "";
            string el_dv = "";
            _retValue = (short)(false ? -1 : 0);
            if (! String.IsNullOrEmpty(Num))
            {
                if (VB6Helpers.Len(Num) < 18)
                {
                    initObject.Frm_Destino_Fondos.Errors.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "El Número Aladi ingresado no es correcto. Revise este dato y reintente.",
                        ControlName = "Tx_Datos_1"
                    });
                    _retValue = 1;
                    return _retValue;
                }

                el_num = VB6Helpers.Left(Num, 15);
                dv = VB6Helpers.Mid(Num, 16, 1);

                for (i = 1; i <= (short)VB6Helpers.Len(el_num); i++)
                {
                    if (i % 2 == 0)
                    {
                        mul = (short)(VB6Helpers.Val(VB6Helpers.Mid(el_num, i, 1)) * 2);
                        Sum = (short)(Sum + VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.Format(VB6Helpers.CStr(mul), "00"), 1, 1)) + VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.Format(VB6Helpers.CStr(mul), "00"), 2, 1)));
                    }
                    else
                    {
                        mul = (short)(VB6Helpers.Val(VB6Helpers.Mid(el_num, i, 1)) * 1);
                        Sum = (short)(Sum + VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.Format(VB6Helpers.CStr(mul), "00"), 2, 1)));
                    }

                }

                if (Sum == 0)
                {
                    x = 0;
                }
                else
                {
                    x = (short)((VB6Helpers.Int((Sum - 1) / 10) * 10) + 10);
                }

                x = (short)(x - Sum);
                el_dv = VB6Helpers.Right(VB6Helpers.Format(VB6Helpers.CStr(x), "00"), 1);
                if (el_dv != dv)
                {
                    initObject.Frm_Destino_Fondos.Errors.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Existe un error en el dígito verificador del Número Aladi ingresado.",
                        ControlName = "Tx_Datos_1"
                    });
                    _retValue = 0;
                    return _retValue;
                }

                _retValue = -1;
            }
            return _retValue;
        }
                
        public static void Inet1_StateChanged(int tipoOperacion, InitializationObject initObj)
        {

            if(tipoOperacion == 30)
            {
                initObj.MODXORI.gb_esCosmos = true;
                initObj.Module1.Codop = initObj.Module1.Codop_FT.Clone();
                initObj.CaptionAddition = "Cuenta Citi";
            }

            else
            {
                initObj.MODXORI.gb_esCosmos = false;
                initObj.Module1.Codop = initObj.Module1.Codop_CVD.Clone();
                initObj.CaptionAddition = "Cuenta BCH";
            }
                
            initObj.MODGCVD.VgCvd.codpro = initObj.Module1.Codop.Id_Product;
            initObj.MODGCVD.VgCvd.codope = initObj.Module1.Codop.Id_Operacion;
            initObj.MODGCVD.VgCvd.OpeSin = initObj.MODGCVD.VgCvd.codcct + initObj.MODGCVD.VgCvd.codpro + initObj.MODGCVD.VgCvd.codesp + initObj.MODGCVD.VgCvd.codofi + initObj.MODGCVD.VgCvd.codope;
            initObj.MODGCVD.VgCvd.OpeCon = initObj.MODGCVD.VgCvd.codcct + "-" + initObj.MODGCVD.VgCvd.codpro + "-" + initObj.MODGCVD.VgCvd.codesp + "-" + initObj.MODGCVD.VgCvd.codofi + "-" + initObj.MODGCVD.VgCvd.codope;
            if (initObj.MODGCVD.COMISION == true)
            {
                initObj.Frm_Principal.Caption = "Comisiones Fund Transfer  " + initObj.MODGCVD.VgCvd.OpeCon + " " + initObj.CaptionAddition;
            }
            else
            {
                initObj.Frm_Principal.Caption = "Compra Venta de Divisas Fund Transfer  " + initObj.MODGCVD.VgCvd.OpeCon + " " + initObj.CaptionAddition;
            }
        }

        public static string Get_CtaCte(UnitOfWorkCext01 unit, string Party)
        {
            try
            {
                var result = unit.SceRepository.sce_ctas_s03_MS(Party);
                if (result == null || result.cuenta == null)
                {
                    return "";
                }
                return result.cuenta;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

    }
}
