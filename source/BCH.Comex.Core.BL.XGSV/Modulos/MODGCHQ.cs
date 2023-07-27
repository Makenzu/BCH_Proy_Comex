using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.XEGI.Modulos;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XGSV.Modulos
{
    public class MODGCHQ
    {
        // Constantes estado Doctos. Valorados
        public const int DOCEMI = 1;
        public const int DOCIMP = 2;
        public const string MsgDoc = "Tipos de Documentos";

        /// <summary>
        /// 1.  Lee todos los documentos de tipo Cheque
        /// 2.  Retorna
        ///      1=> Lectura exitosa
        ///      0=> Lectura no exitosa
        /// </summary>
        /// <param name="FecEmision"></param>
        /// <param name="codcct"></param>
        /// <param name="codusr"></param>
        /// <returns></returns>
        public static List<T_Chq> SyGetn_Chq(string FecEmision, string codcct, string codusr, DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer())
            {
                List<T_Chq> returnValue = new List<T_Chq>();
                try
                {
                    var result = uow.SceRepository.sce_chq_s03_MS(codcct, MODGSYB.dbcharSy(codusr), DateTime.Parse(FecEmision));

                    if (result != null)
                    {
                        returnValue = new List<T_Chq>();
                        foreach (var item in result)
                        {
                            T_Chq T_Chq = new T_Chq();

                            T_Chq.FecEmi = FecEmision;
                            T_Chq.CctSup = globales.UsrEsp.cent_costo;
                            T_Chq.UsrSup = globales.UsrEsp.id_especia;

                            T_Chq.codcct = item.codcct;
                            T_Chq.codpro = item.codpro;
                            T_Chq.codesp = item.codesp;
                            T_Chq.CodEmp = item.codofi;
                            T_Chq.codope = item.codope;
                            T_Chq.NroCor = (int)item.nrocor;
                            T_Chq.estado = (int)item.estado;
                            T_Chq.NroFol = (int)item.nrofol;
                            T_Chq.MonSwf = item.monswf;
                            T_Chq.MtoChq = (double)item.mtochq;
                            T_Chq.NomBen = item.nomben;
                            T_Chq.NomPag = item.nompag;
                            T_Chq.DirPag = item.dirpag;
                            T_Chq.swfpag = item.swfpag;
                            T_Chq.CiuPag = item.ciupag;
                            T_Chq.PaiPag = item.paipag;
                            T_Chq.numcta = item.numcta;
                            T_Chq.NomCli = item.nomcli;

                            returnValue.Add(T_Chq);
                        }
                    }
                    else
                    {
                        if (result == null)
                        {
                            globales.ListaMensajesError.Add(new UI_Message
                            {
                                Text = "Error al leer los cheques asociados al supervisor [Sce_Chq].",
                                Type = TipoMensaje.Informacion,
                                Title = MODGCHQ.MsgDoc
                            });
                            tracer.TraceError("Error al leer los cheques asociados al supervisor [Sce_Chq].");
                            return returnValue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    globales.ListaMensajesError.Add(new UI_Message
                    {
                        Text = "Error al leer los cheques asociados al supervisor [Sce_Chq].",
                        Type = TipoMensaje.Informacion,
                        Title = MODGCHQ.MsgDoc
                    });
                    tracer.TraceError("Error al leer los cheques asociados al supervisor [Sce_Chq].");
                    throw ex;
                }
                return returnValue;
            }
        }

        // Actualiza el estado y el número de folio de un cheque
        // Retorno    <> 0    : Grabación Exitosa-
        //            =  0    : Error o Grabación no Exitosa.-
        public static bool SyUpd_Chq(string codcct, string codpro, string codesp, string codemp, string codope, string nrocor, string nrofol, string estado, DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            bool returnValue = false;
            try
            {

                var result = uow.SceRepository.sce_chq_u01_MS(codcct, codpro, codesp, codemp, codope, nrocor, nrofol, estado);

                if (result != null)
                {
                    returnValue = (result.Column1 == 0 ? true : false);
                }
                else
                {
                    globales.ListaMensajesError.Add(new UI_Message
                    {
                        Text = "Se ha producido un error al tratar de actualizar el estado y número de folio del cheque.",
                        Type = TipoMensaje.Critical,
                        Title = MODGCHQ.MsgDoc
                    });
                    return returnValue;
                }

            }
            catch (Exception ex)
            {
                globales.ListaMensajesError.Add(new UI_Message
                {
                    Text = "Se ha producido un error al tratar de actualizar el estado y número de folio del cheque.",
                    Type = TipoMensaje.Critical,
                    Title = MODGCHQ.MsgDoc
                });
                throw ex;
            }
            return returnValue;
        }

        public static List<T_Vvi> SyGetn_Vvi(string FecEmision, DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            List<T_Vvi> returnValue = new List<T_Vvi>();
            try
            {
                var result = uow.SceRepository.sce_vvi_s03_MS(globales.UsrEsp.cent_costo, MODGSYB.dbcharSy(globales.UsrEsp.id_especia), DateTime.Parse(FecEmision));

                if (result != null)
                {
                    returnValue = new List<T_Vvi>();
                    foreach (var item in result)
                    {
                        T_Vvi T_Chq = new T_Vvi();

                        T_Chq.FecEmi = FecEmision;
                        T_Chq.CctSup = globales.UsrEsp.cent_costo;
                        T_Chq.UsrSup = globales.UsrEsp.id_especia;

                        T_Chq.codcct   = item.codcct; 
                        T_Chq.codpro   = item.codpro; 
                        T_Chq.codesp   = item.codesp; 
                        T_Chq.CodEmp   = item.codofi; 
                        T_Chq.codope   = item.codope; 
                        T_Chq.NroCor   = (int)item.nrocor;
                        T_Chq.estado = (int)item.estado;
                        T_Chq.numfol = (int)item.numfol;
                        T_Chq.NomBen   = item.nomben; 
                        T_Chq.RutTom   = item.ruttom; 
                        T_Chq.NomTom   = item.nomtom;
                        T_Chq.MtoVvi   = (double)item.mtovvi;

                        returnValue.Add(T_Chq);
                    }
                }
                else
                {
                    if (result == null)
                    {
                        globales.ListaMensajesError.Add(new UI_Message
                        {
                            Text = "Error al leer los vales vista asociados al supervisor [Sce_Chq]",
                            Type = TipoMensaje.Informacion,
                            Title = MODGCHQ.MsgDoc
                        });
                        return returnValue;
                    }
                }
            }
            catch (Exception ex)
            {
                globales.ListaMensajesError.Add(new UI_Message
                {
                    Text = "Error al leer los vales vista asociados al supervisor [Sce_Chq]",
                    Type = TipoMensaje.Informacion,
                    Title = MODGCHQ.MsgDoc
                });
                throw ex;
            }
            return returnValue;
        }

        // ****************************************************************************
        //    1.  Lee todas las monedas
        // ****************************************************************************
        public static List<T_MndIng> SyGetn_MndIng(DatosGlobales globales, UnitOfWorkCext01 uow)
        {

            List<T_MndIng> returnValue = new List<T_MndIng>();
            try
            {
                var result = uow.SgtRepository.sgt_mnd_MS();

                if (result != null)
                {
                    returnValue = new List<T_MndIng>();
                    foreach (var item in result)
                    {
                        T_MndIng t_mnding = new T_MndIng();
                        t_mnding.mnd_mndina = item.mnd_mndina;
                        t_mnding.Mnd_MndNmc = item.mnd_mndnmc;

                        returnValue.Add(t_mnding);
                    }
                }
                else
                {
                    if (result == null)
                    {
                        globales.ListaMensajesError.Add(new UI_Message
                        {
                            Text = "Error al Leer los datos de las Monedas [Sgt_Mnd].",
                            Type = TipoMensaje.Critical,
                            Title = MODGCHQ.MsgDoc
                        });
                        return returnValue;
                    }
                }
            }
            catch (Exception ex)
            {
                globales.ListaMensajesError.Add(new UI_Message
                {
                    Text = "Error al Leer los datos de las Monedas [Sgt_Mnd].",
                    Type = TipoMensaje.Critical,
                    Title = MODGCHQ.MsgDoc
                });
                throw ex;
            }
            return returnValue;

        }

        // ****************************************************************************
        //    1.  Retorna la descripción en inglés del tipo de moneda.
        // ****************************************************************************
        public static string Fn_NomMonIng(string Nem, DatosGlobales globales)
        {
            string returnValue = string.Empty;
            foreach (var item in globales.VMndIng)
            {
                if (item.Mnd_MndNmc.Trim().ToUpper() == Nem.Trim().ToUpper())
                {
                    returnValue = item.mnd_mndina.ToUpper().Trim();
                    break;
                }
            }
            return returnValue;
        }

    }
}
