using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public class MODGASO
    {
        public static T_MODGASO GetMODGASO()
        {
            return new T_MODGASO();
        }

        /// <summary>
        /// SyGetp_xPaeCdd
        /// </summary>
        /// <param name="globales"></param>
        /// <param name="uow"></param>
        /// <param name="NumOpe"></param>
        /// <returns></returns>
        public static short SyGetp_xPaeCdd(DatosGlobales globales, UnitOfWorkCext01 uow, string NumOpe)
        {
            short _retValue = 0;

            try
            {
                sce_ppae_s02_MS_Result Result = uow.SceRepository.sce_ppae_s02_MS(
                                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)),
                                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)),
                                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)),
                                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)),
                                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)));

                globales.MODGASO.VgAso.OpeSin = NumOpe.Trim();
                globales.MODGASO.VgAso.OpeCon = NumOpe.Substring(0, 3).Trim() + "-" + NumOpe.Substring(3, 2).Trim() + "-" + NumOpe.Substring(5, 2).Trim() + "-" + NumOpe.Substring(7, 3).Trim() + "-" + NumOpe.Substring(10, 5).Trim();
                if (Result != null)
                {
                    globales.MODGASO.VgAso.PrtCli = Result.prtexp.Trim().Replace("|", "~");
                    globales.MODGASO.VgAso.IndNom = (short)Result.codnom;
                    globales.MODGASO.VgAso.IndDir = (short)Result.coddir;
                    _retValue = (short)(true ? -1 : 0);
                }

                return _retValue;
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        
        /// <summary>
        /// Leer datos de la tabla carta de crédito de importaciones
        /// </summary>
        /// <param name="globales"></param>
        /// <param name="uow"></param>
        /// <param name="NumOpe"></param>
        /// <returns></returns>
        public static short SyGet_CCIM(DatosGlobales globales, UnitOfWorkCext01 uow, string NumOpe)
        {
            short _retValue = 0;

            try
            {
                sce_jprt_s02_MS_Result Result = uow.SceRepository.sce_jprt_s02_MS(
                                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)),
                                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)),
                                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)),
                                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)),
                                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5))
                );

                if (Result != null)
                {
                    globales.MODGASO.VgAso.OpeSin = NumOpe.Trim();
                    globales.MODGASO.VgAso.OpeCon = NumOpe.Substring(0, 3).Trim() + "-" + NumOpe.Substring(3, 2).Trim() + "-" + NumOpe.Substring(5, 2).Trim() + "-" + NumOpe.Substring(7, 3).Trim() + "-" + NumOpe.Substring(10, 5).Trim();
                    globales.MODGASO.VgAso.PrtCli = Result.codprt.Trim().Replace("|", "~");
                    globales.MODGASO.VgAso.IndNom = (short)Result.indnom;
                    globales.MODGASO.VgAso.IndDir = (short)Result.inddir;
                    _retValue = 1;
                }
                

                return _retValue;
            }
            catch (Exception _ex)
            {
                return (short)0;
            }
        }
       
        /// <summary>
        /// Leer la tabla de cobranza importaciones
        /// </summary>
        /// <param name="globales"></param>
        /// <param name="uow"></param>
        /// <param name="NumOpe"></param>
        /// <returns></returns>
        public static short SyGet_CImp(DatosGlobales globales, UnitOfWorkCext01 uow, string NumOpe)
        {
            short _retValue = 0;
            try
            {
                sce_pcol_s01_MS_Result Result = uow.SceRepository.sce_pcol_s01_MS(
                                     MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)),
                                     MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)),
                                     MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)),
                                     MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)),
                                     MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5))
               );

                globales.MODGASO.VgAso.OpeSin = NumOpe.Trim();
                globales.MODGASO.VgAso.OpeCon = NumOpe.Substring(0, 3).Trim() + "-" + NumOpe.Substring(3, 2).Trim() + "-" + NumOpe.Substring(5, 2).Trim() + "-" + NumOpe.Substring(7, 3).Trim() + "-" + NumOpe.Substring(10, 5).Trim();

                if (Result != null)
                {
                    globales.MODGASO.VgAso.PrtCli = Result.id_party.Trim().Replace("|", "~");
                    globales.MODGASO.VgAso.IndNom = (short)Result.nombre;
                    globales.MODGASO.VgAso.IndDir = (short)Result.direccion;
                    _retValue = (short)(true ? -1 : 0);
                }

                return _retValue;
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        
        /// <summary>
        /// Objetivo  : Leer tabla de Compar Venta
        /// </summary>
        /// <param name="globales"></param>
        /// <param name="uow"></param>
        /// <param name="NumOpe"></param>
        /// <returns></returns>
        public static short SyGetp_Crd(DatosGlobales globales, UnitOfWorkCext01 uow, string NumOpe)
        {
            short _retValue = 0;
            try
            {
                sce_cvd_s05_MS_Result Result = uow.SceRepository.sce_cvd_s05_MS(
                                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)),
                                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)),
                                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)),
                                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)),
                                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5))
                );

                globales.MODGASO.VgAso.OpeSin = NumOpe.Trim();
                globales.MODGASO.VgAso.OpeCon = NumOpe.Substring(0, 3).Trim() + "-" + NumOpe.Substring(3, 2).Trim() + "-" + NumOpe.Substring(5, 2).Trim() + "-" + NumOpe.Substring(7, 3).Trim() + "-" + NumOpe.Substring(10, 5).Trim();

                if (Result != null)
                {
                    globales.MODGASO.VgAso.PrtCli = Result.prtcli.Trim().Replace("|", "~");
                    globales.MODGASO.VgAso.IndNom = (short)Result.indnomc;
                    globales.MODGASO.VgAso.IndDir = (short)Result.inddirc;
                    _retValue = (short)(true ? -1 : 0);
                }

                return _retValue;
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }

        /// <summary>
        /// Procedimiento almacenado para carta crédito contado
        /// </summary>
        /// <param name="globales"></param>
        /// <param name="uow">Conexión a la base de datos</param>
        /// <param name="paso"></param>
        /// <returns></returns>
        internal static short SyGetp_PreImp(DatosGlobales globales, UnitOfWorkCext01 uow, string paso)
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
        /// <param name="globales">Objecto transversal </param>
        /// <param name="uow">Conexión a la base de datos</param>
        /// <param name="paso"></param>
        /// <returns></returns>
        internal static short SyGetp_CreCon(DatosGlobales globales, UnitOfWorkCext01 uow, string paso)
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

        /// <summary>
        /// 1.  Lee el Partys, IndNom e IndDir de un GL.-
        /// </summary>
        /// <param name="globales"></param>
        /// <param name="uow"></param>
        /// <param name="NumOpe"></param>
        /// <returns></returns>
        public static short SyGetp_GL(DatosGlobales globales, UnitOfWorkCext01 uow, string NumOpe)
        {
            short _retValue = 0;

            try
            {
                sce_mch_s05_MS_Result Result = uow.SceRepository.sce_mch_s05_MS(
                                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)),
                                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)),
                                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)),
                                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)),
                                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)));

                globales.MODGASO.VgAso.OpeSin = NumOpe.Trim();
                globales.MODGASO.VgAso.OpeCon = NumOpe.Substring(0, 3).Trim() + "-" + NumOpe.Substring(3, 2).Trim() + "-" + NumOpe.Substring(5, 2).Trim() + "-" + NumOpe.Substring(7, 3).Trim() + "-" + NumOpe.Substring(10, 5).Trim();

                if (Result != null)
                {  //Se debe cambiar el | por ~ para que siga igual que el legacy
                    globales.MODGASO.VgAso.PrtCli = Result.prtcli.Trim().Replace("|", "~");
                    globales.MODGASO.VgAso.IndNom = (short)Result.indcli;
                    globales.MODGASO.VgAso.IndDir = 0;
                    _retValue = (short)(true ? -1 : 0);
                }

                return _retValue;
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }

        /// <summary>
        /// Objetivo  : Leer tabla de retornos
        /// </summary>
        /// <param name="globales"></param>
        /// <param name="uow"></param>
        /// <param name="numOpe"></param>
        /// <returns></returns>
        public static short SyGetp_Ret(DatosGlobales globales, UnitOfWorkCext01 uow, string numOpe)
        {
            using (Tracer tracer = new Tracer("SyGetp_Ret"))
            {
                short _retValue = 0;

                try
                {
                    //sce_xret_s05_MS_Result Result = uow.SceRepository.sce_xret_s05_MS(
                    //                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)),
                    //                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)),
                    //                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)),
                    //                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)),
                    //                      MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)),
                    //                      MODGSYB.dbnumesy(VB6Helpers.Mid(NumOpe, 16, 2)),
                    //                      MODGSYB.dbnumesy(VB6Helpers.Mid(NumOpe, 18, 8))
                    sce_xret_s05_MS_Result Result = uow.SceRepository.sce_xret_s05_MS(
                                                                        numOpe.Substring(0, 3),
                                                                        numOpe.Substring(3, 2),
                                                                        numOpe.Substring(5, 2),
                                                                        numOpe.Substring(7, 3),
                                                                        numOpe.Substring(10, 5),
                                                                        numOpe.Substring(15, 2),
                                                                        numOpe.Substring(17, 2)
                        );

                    globales.MODGASO.VgAso.OpeSin = numOpe.Trim();
                    globales.MODGASO.VgAso.OpeCon = numOpe.Substring(0, 3).Trim() + "-" + numOpe.Substring(3, 2).Trim() + "-" + numOpe.Substring(5, 2).Trim() + "-" + numOpe.Substring(7, 3).Trim() + "-" + numOpe.Substring(10, 5).Trim();

                    if (Result != null)
                    {
                        globales.MODGASO.VgAso.PrtCli = Result.prtexp.Trim().Replace("|", "~");
                        globales.MODGASO.VgAso.IndNom = (short)Result.indnome;
                        globales.MODGASO.VgAso.IndDir = (short)Result.inddire;
                        _retValue = (short)(true ? -1 : 0);
                    }



                    return _retValue;
                }
                catch (Exception _ex)
                {
                    tracer.TraceException("Alerta al Leer tabla de retornos", _ex);
                    return _retValue;
                } 
            }
        }
        
        /// <summary>
        /// 1.  Lee el Partys, IndNom e IndDir de una Operación Determinada.
        /// </summary>
        /// <param name="globales"></param>
        /// <param name="uow"></param>
        /// <param name="NumOpe"></param>
        /// <returns></returns>
        public static short SyGetp_xCob(DatosGlobales globales, UnitOfWorkCext01 uow, string NumOpe)
        {
            short _retValue = 0;

            try
            {
                uow.SceRepository.ReadQuerySP((reader) =>
                {
                    if (reader.Read())
                    {
                        globales.MODGASO.VgAso.OpeSin = NumOpe.Trim();
                        globales.MODGASO.VgAso.OpeCon = NumOpe.Substring(0, 3).Trim() + "-" + NumOpe.Substring(3, 2).Trim() + "-" + NumOpe.Substring(5, 2).Trim() + "-" + NumOpe.Substring(7, 3).Trim() + "-" + NumOpe.Substring(10, 5).Trim();
                        globales.MODGASO.VgAso.PrtCli = reader.GetValue(0).ToString().Replace("|", "~");
                        globales.MODGASO.VgAso.IndNom = Convert.ToInt16(reader.GetValue(1));
                        globales.MODGASO.VgAso.IndDir = Convert.ToInt16(reader.GetValue(2));
                        _retValue = (short)(true ? -1 : 0); ;
                    }
                    else
                    {
                        _retValue = (short)(false ? -1 : 0); ;
                    }
                }, "sce_xcob_s04_MS", NumOpe.Substring(0, 3), NumOpe.Substring(3, 2), NumOpe.Substring(5, 2), NumOpe.Substring(7, 3), NumOpe.Substring(10, 5));

                return _retValue;
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }

        ///// <summary>
        ///// Este metodo se agrega ya que se encontraba en formulario Frmgnota 
        ///// </summary>
        ///// <param name="globales"></param>
        ///// <param name="uow"></param>
        ///// <param name="Indice"></param>
        ///// <param name="NumOpe"></param>
        ///// <returns></returns>
        //public static short SyGetn_Cre(DatosGlobales globales, UnitOfWorkCext01 uow, short Indice, string NumOpe)
        //{
        //    short _retValue = 0;
        //    string Que = "";
        //    string R = "";
        //    dynamic MsgxCob = null;
        //    short m = 0;
        //    short n = 0;
        //    short i = 0;
        //    List<sce_mcd_s04_MS_Result> Result = new List<sce_mcd_s04_MS_Result>();
        //    try
        //    {
        //        switch (Indice)
        //        {
        //            case 0:
        //                break;
        //            case 1:
        //                Result = uow.SceRepository.sce_mcd_s04_MS(NumOpe);

        //                break;
        //        }

        //        m = (short)(globales.Mdl_Funciones.VPrn_cre.Length);
        //        n = (short)Result.Count();

        //        Array.Resize<T_Prn_cre>(ref globales.Mdl_Funciones.VPrn_cre, m + n);
        //        int indexResult = 0;
        //        for (i = m; i < (short)globales.Mdl_Funciones.VPrn_cre.Length; i++, indexResult++)
        //        {
        //            globales.Mdl_Funciones.VPrn_cre[i] = new T_Prn_cre();
        //            globales.Mdl_Funciones.VPrn_cre[i].codcct = Result[indexResult].codcct;// VB6Helpers.Trim(VB6Helpers.CStr( R)));
        //            globales.Mdl_Funciones.VPrn_cre[i].codpro = Result[indexResult].codpro;// VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R)));
        //            globales.Mdl_Funciones.VPrn_cre[i].codesp = Result[indexResult].codesp;// VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R)));
        //            globales.Mdl_Funciones.VPrn_cre[i].codofi = Result[indexResult].codofi;// VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R)));
        //            globales.Mdl_Funciones.VPrn_cre[i].codope = Result[indexResult].codope;// VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R)));
        //            globales.Mdl_Funciones.VPrn_cre[i].Factura = (double)Result[indexResult].nrofac;// Format.StringToDouble(MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R));
        //            globales.Mdl_Funciones.VPrn_cre[i].NroRpt = (int)Result[indexResult].nrorpt;// VB6Helpers.CInt(MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R));
        //            globales.Mdl_Funciones.VPrn_cre[i].FecOpe = Result[indexResult].fecfac.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);// VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumSig(), "F", R));
        //            globales.Mdl_Funciones.VPrn_cre[i].neto = (double)Result[indexResult].netofac;// Format.StringToDouble(MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R));
        //            globales.Mdl_Funciones.VPrn_cre[i].iva = (double)Result[indexResult].ivafac;// Format.StringToDouble(MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R));
        //            globales.Mdl_Funciones.VPrn_cre[i].monto = (double)Result[indexResult].montofac;// Format.StringToDouble(MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R));
        //            globales.Mdl_Funciones.VPrn_cre[i].monedafac = (int)Result[indexResult].monedafac; // VB6Helpers.CInt(MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R));
        //            globales.Mdl_Funciones.VPrn_cre[i].tipofac = Result[indexResult].tipofac;// VB6Helpers.Trim(VB6Helpers.CStr(MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R)));
        //            globales.Mdl_Funciones.VPrn_cre[i].CodDoc = 0;
        //            globales.Mdl_Funciones.VPrn_cre[i].TipSwf = 0;
        //            globales.Mdl_Funciones.VPrn_cre[i].NroSwf = 0;
        //            globales.Mdl_Funciones.VPrn_cre[i].NroMem = 0;
        //            globales.Mdl_Funciones.VPrn_cre[i].TipDoc = 3;  //Identifica 1 = Carta, 2 = Swift, 3 = Contabilidad
        //            //R = Mdl_SRM.NuevaRespuesta(13, R);  //El número "13" son la cantidad de campos que vienen del procedimiento y la "R$" es la data que viene del proc.
        //        }

        //        _retValue = (short)(true ? -1 : 0);

        //        return _retValue;
        //    }
        //    catch (Exception _ex)
        //    {
        //        return _retValue;
        //    }
        //}


    }
}
