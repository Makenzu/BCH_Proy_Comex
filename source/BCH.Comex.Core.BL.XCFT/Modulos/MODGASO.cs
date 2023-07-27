using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODGASO
    {
        public static T_MODGASO GetMODGASO()
        {
            return new T_MODGASO();
        }

        public static short SyGetp_xPaeCdd(InitializationObject initObj, UnitOfWorkCext01 unit, string NumOpe)
        {
            using (var trace = new Tracer())
            {
                short _retValue = 0;
                try
                {
                    sce_ppae_s02_MS_Result Result = unit.SceRepository.sce_ppae_s02_MS(
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5))
                    );

                    if (Result != null)
                    {
                        initObj.MODGASO.VgAso.OpeSin = NumOpe.Trim();
                        initObj.MODGASO.VgAso.OpeCon = MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)).Trim();

                        initObj.MODGASO.VgAso.PrtCli = VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", Result.prtexp)));
                        initObj.MODGASO.VgAso.IndNom = (short)Result.codnom;
                        initObj.MODGASO.VgAso.IndDir = (short)Result.coddir;
                        _retValue = (short)(true ? -1 : 0);
                    }
                    return _retValue;
                }
                catch (Exception _ex)
                {
                    trace.TraceException("Alerta", _ex);
                    throw;
                }
            }
        }
        //*********************************************************
        //Leer datos de la tabla carta de crédito de importaciones
        //*********************************************************
        // UPGRADE_INFO (#0561): The 'SyGet_CCIM' symbol was defined without an explicit "As" clause.
        public static dynamic SyGet_CCIM(InitializationObject initObj, UnitOfWorkCext01 unit, string NumOpe)
        {
            using (var trace = new Tracer())
            {
                dynamic _retValue = null;
                try
                {
                    sce_jprt_s02_MS_Result Result = unit.SceRepository.sce_jprt_s02_MS(
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5))
                    );
                    initObj.MODGASO.VgAso.OpeSin = NumOpe.Trim();
                    initObj.MODGASO.VgAso.OpeCon = MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)).Trim();
                    initObj.MODGASO.VgAso.PrtCli = VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", Result.codprt))); ;
                    initObj.MODGASO.VgAso.IndNom = (short)Result.indnom;
                    initObj.MODGASO.VgAso.IndDir = (short)Result.inddir;
                    _retValue = true;
                    return _retValue;
                }
                catch (Exception _ex)
                {
                    trace.TraceException("Alerta", _ex);
                    return (short)0;
                }
            }
        }
        //*******************************************
        //Leer la tabla de cobranza importaciones
        //*******************************************
        public static short SyGet_CImp(InitializationObject initObj, UnitOfWorkCext01 unit, string NumOpe)
        {
            using (var trace = new Tracer())
            {
                short _retValue = 0;
                try
                {
                    sce_pcol_s01_MS_Result Result = unit.SceRepository.sce_pcol_s01_MS(
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5))
                    );


                    if (Result != null)
                    {
                        initObj.MODGASO.VgAso.OpeSin = NumOpe.Trim();
                        initObj.MODGASO.VgAso.OpeCon = MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)).Trim();

                        initObj.MODGASO.VgAso.PrtCli = VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", Result.id_party))); ;
                        initObj.MODGASO.VgAso.IndNom = (short)Result.nombre;
                        initObj.MODGASO.VgAso.IndDir = (short)Result.direccion;
                        _retValue = (short)(true ? -1 : 0);
                    }

                    return _retValue;
                }
                catch (Exception _ex)
                {
                    trace.TraceException("Alerta", _ex);
                    throw;
                }
            }
        }
        ////**************************************************
        //// Objetivo  : Leer tabla de Compar Venta          *
        ////**************************************************
        public static short SyGetp_Crd(InitializationObject initObj, UnitOfWorkCext01 unit, string NumOpe)
        {
            using (var trace = new Tracer())
            {
                short _retValue = 0;
                try
                {
                    sce_cvd_s05_MS_Result Result = unit.SceRepository.sce_cvd_s05_MS(
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5))
                    );

                    if (Result != null)
                    {
                        initObj.MODGASO.VgAso.OpeSin = NumOpe.Trim();
                        initObj.MODGASO.VgAso.OpeCon = MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)).Trim();

                        initObj.MODGASO.VgAso.PrtCli = VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", Result.prtcli)));
                        initObj.MODGASO.VgAso.IndNom = (short)Result.indnomc;
                        initObj.MODGASO.VgAso.IndDir = (short)Result.inddirc;
                        _retValue = (short)(true ? -1 : 0);
                    }

                    return _retValue;
                }
                catch (Exception _ex)
                {
                    trace.TraceException("Alerta", _ex);
                    throw;
                }
            }
        }
        ////****************************************************************************
        ////   1.  Lee el Partys, IndNom e IndDir de un GL.-
        ////****************************************************************************
        public static short SyGetp_GL(InitializationObject initObj, UnitOfWorkCext01 unit, string NumOpe)
        {
            using (var trace = new Tracer())
            {
                short _retValue = 0;
                try
                {

                    sce_mch_s05_MS_Result Result = unit.SceRepository.sce_mch_s05_MS(
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)));

                    if (Result != null)
                    {
                        initObj.MODGASO.VgAso.OpeSin = NumOpe.Trim();
                        initObj.MODGASO.VgAso.OpeCon = MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)).Trim();

                        initObj.MODGASO.VgAso.PrtCli = VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", Result.prtcli))); ;
                        initObj.MODGASO.VgAso.IndNom = (short)Result.indcli;
                        initObj.MODGASO.VgAso.IndDir = 0;
                        _retValue = (short)(true ? -1 : 0);
                    }

                    return _retValue;
                }
                catch (Exception _ex)
                {
                    trace.TraceException("Alerta", _ex);
                    throw;
                }
            }
        }

        /// <summary>
        /// Procedimiento almacenado para carta crédito contado
        /// </summary>
        /// <param name="initObj"></param>
        /// <param name="uow">Conexión a la base de datos</param>
        /// <param name="paso"></param>
        /// <returns></returns>
        internal static short SyGetp_PreImp(InitializationObject initObj, UnitOfWorkCext01 uow, string paso)
        {
            using (Tracer tracer = new Tracer("SyGetp_PreImp"))
            {
                short _returnValue = 0;
                try
                {
                    //var result = uow.SceRepository.
                }
                catch (Exception _ex)
                {
                    tracer.TraceException("Alerta", _ex);
                }
                return _returnValue;
            }
        }

        /// <summary>
        /// Procedimiento almacenado para prestamos a importadores
        /// </summary>
        /// <param name="initObj"></param>
        /// <param name="uow">Conexión a la base de datos</param>
        /// <param name="paso"></param>
        /// <returns></returns>
        internal static short SyGetp_CreCon(InitializationObject initObj, UnitOfWorkCext01 uow, string paso)
        {
            using (Tracer tracer = new Tracer("SyGetp_CreCon"))
            {
                short _returnValue = 0;
                try
                {
                    //var result = uow.SceRepository.
                }
                catch (Exception _ex)
                {
                    tracer.TraceException("Alerta", _ex);
                }
                return _returnValue;
            }
        }

        ////**************************************************
        //// Objetivo  : Leer tabla de retornos              *
        ////**************************************************
        public static short SyGetp_Ret(InitializationObject initObj, UnitOfWorkCext01 unit, string NumOpe)
        {
            using (var trace = new Tracer())
            {
                short _retValue = 0;
                try
                {
                    sce_xret_s05_MS_Result Result = unit.SceRepository.sce_xret_s05_MS(
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)),
                                          MODGSYB.dbnumesy(VB6Helpers.Mid(NumOpe, 16, 3)),
                                          MODGSYB.dbnumesy(VB6Helpers.Mid(NumOpe, 19, 3))
                    );

                    if (Result != null)
                    {
                        initObj.MODGASO.VgAso.OpeSin = NumOpe.Trim();
                        initObj.MODGASO.VgAso.OpeCon = MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)).Trim();

                        initObj.MODGASO.VgAso.PrtCli = VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", Result.prtexp))); ;
                        initObj.MODGASO.VgAso.IndNom = (short)Result.indnome;
                        initObj.MODGASO.VgAso.IndDir = (short)Result.inddire;
                        _retValue = (short)(true ? -1 : 0);
                    }
                    return _retValue;
                }
                catch (Exception _ex)
                {
                    trace.TraceException("Alerta",_ex);
                    throw;
                }
            }
        }
        ////****************************************************************************
        ////   1.  Lee el Partys, IndNom e IndDir de una Operación Determinada.
        ////****************************************************************************
        public static short SyGetp_xCob(InitializationObject initObj, UnitOfWorkCext01 unit, string NumOpe)
        {
            using (var trace = new Tracer())
            {
                short _retValue = 0;
                try
                {
                    unit.SceRepository.ReadQuerySP((reader) =>
                    {
                        if (reader.Read())
                        {
                            initObj.MODGASO.VgAso.OpeSin = NumOpe.Trim();
                            initObj.MODGASO.VgAso.OpeCon = MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)).Trim() + "-" + MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)).Trim();
                            initObj.MODGASO.VgAso.PrtCli = VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", reader.GetValue(0).ToString())));
                            initObj.MODGASO.VgAso.IndNom = Convert.ToInt16(reader.GetValue(1));
                            initObj.MODGASO.VgAso.IndDir = Convert.ToInt16(reader.GetValue(2));
                            _retValue = (short)(true ? -1 : 0); ;
                        }
                        else
                        {
                            _retValue = (short)(false ? -1 : 0); ;
                        }
                    }, "sce_xcob_s04_MS", MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)),
                                          MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)));
                    return _retValue;
                }
                catch (Exception _ex)
                {
                    trace.TraceException("Alerta", _ex);
                    throw;
                }
            }
        }
        /// <summary>
        /// Este metodo se agrega ya que se encontraba en formulario Frmgnota 
        /// </summary>
        /// <param name="initObj"></param>
        /// <param name="unit"></param>
        /// <param name="NumOpe"></param>
        /// <returns></returns>
        public static short SyGetn_Cre(InitializationObject initObj, UnitOfWorkCext01 unit, short Indice, string NumOpe)
        {
            using (var trace = new Tracer())
            {
                short _retValue = 0;
                short m = 0;
                short n = 0;
                short i = 0;
                List<sce_mcd_s04_MS_Result> Result = new List<sce_mcd_s04_MS_Result>();
                try
                {
                    switch (Indice)
                    {
                        case 0:
                            break;
                        case 1:
                            Result = unit.SceRepository.sce_mcd_s04_MS(NumOpe);
                            break;
                    }

                    m = (short)(initObj.Mdl_Funciones.VPrn_cre.Length);
                    n = (short)Result.Count();
                    Array.Resize<T_Prn_cre>(ref initObj.Mdl_Funciones.VPrn_cre, m + n);
                    int indexResult = 0;
                    for (i = m; i < (short)initObj.Mdl_Funciones.VPrn_cre.Length; i++, indexResult++)
                    {
                        initObj.Mdl_Funciones.VPrn_cre[i] = new T_Prn_cre();
                        initObj.Mdl_Funciones.VPrn_cre[i].codcct = Result[indexResult].codcct;// VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", R)));
                        initObj.Mdl_Funciones.VPrn_cre[i].codpro = Result[indexResult].codpro;// VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R)));
                        initObj.Mdl_Funciones.VPrn_cre[i].codesp = Result[indexResult].codesp;// VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R)));
                        initObj.Mdl_Funciones.VPrn_cre[i].codofi = Result[indexResult].codofi;// VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R)));
                        initObj.Mdl_Funciones.VPrn_cre[i].codope = Result[indexResult].codope;// VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R)));
                        initObj.Mdl_Funciones.VPrn_cre[i].Factura = (double)Result[indexResult].nrofac;// Format.StringToDouble(MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R));
                        initObj.Mdl_Funciones.VPrn_cre[i].NroRpt = (int)Result[indexResult].nrorpt;// VB6Helpers.CInt(MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R));
                        initObj.Mdl_Funciones.VPrn_cre[i].FecOpe = Result[indexResult].fecfac.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);// VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumSig(), "F", R));
                        initObj.Mdl_Funciones.VPrn_cre[i].neto = (double)Result[indexResult].netofac;// Format.StringToDouble(MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R));
                        initObj.Mdl_Funciones.VPrn_cre[i].iva = (double)Result[indexResult].ivafac;// Format.StringToDouble(MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R));
                        initObj.Mdl_Funciones.VPrn_cre[i].monto = (double)Result[indexResult].montofac;// Format.StringToDouble(MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R));
                        initObj.Mdl_Funciones.VPrn_cre[i].monedafac = (int)Result[indexResult].monedafac; // VB6Helpers.CInt(MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R));
                        initObj.Mdl_Funciones.VPrn_cre[i].tipofac = Result[indexResult].tipofac;// VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R)));
                        initObj.Mdl_Funciones.VPrn_cre[i].CodDoc = 0;
                        initObj.Mdl_Funciones.VPrn_cre[i].TipSwf = 0;
                        initObj.Mdl_Funciones.VPrn_cre[i].NroSwf = 0;
                        initObj.Mdl_Funciones.VPrn_cre[i].NroMem = 0;
                        initObj.Mdl_Funciones.VPrn_cre[i].TipDoc = 3;  //Identifica 1 = Carta, 2 = Swift, 3 = Contabilidad
                    }

                    _retValue = (short)(true ? -1 : 0);

                    return _retValue;
                }
                catch (Exception _ex)
                {
                    trace.TraceException("Alerta",_ex);
                    return _retValue;
                }
            }
        }
    }
}
