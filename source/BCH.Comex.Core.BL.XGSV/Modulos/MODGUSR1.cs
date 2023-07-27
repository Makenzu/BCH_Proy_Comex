using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.XEGI.Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XGSV.Modulos
{
    public static class MODGUSR1
    {
        public const string MsgCie = "Cierre Diario de Comercio Exterior";

        /// <summary>
        /// Lee todos los usuarios asociados a un centro de costo.
        /// Retorno <> 0 Lectura exitosa
        ///          = 0 Error o letura no exitosa
        /// </summary>
        /// <param name="CenCos"></param>
        /// <returns></returns>
        public static bool SyGetn_Usr1(string CenCos, DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer())
            {
                bool returnValue = false;
                try
                {
                    var result = uow.SceRepository.sce_usr_s03_MS(MODGSYB.dbcharSy(CenCos));

                    if (result != null)
                    {
                        globales.VUsr = new List<T_Usr>();
                        foreach (sce_usr_s03_MS_Result item in result)
                        {
                            T_Usr usr = new T_Usr();

                            usr.CenCos = item.cent_costo;
                            usr.CodEsp = item.id_especia;
                            usr.CenSup = item.cent_super;
                            usr.CodSup = item.id_super;
                            usr.NomEsp = item.nombre;
                            usr.Jerarquia = (int)item.jerarquia;
                            usr.FecIni = item.fec_ini.ToString("dd/MM/yyyy HH:mm:ss");
                            usr.FecFin = item.fec_fin.ToString("dd/MM/yyyy HH:mm:ss");
                            usr.FecOut = item.fec_out.ToString("dd/MM/yyyy HH:mm:ss");

                            if (!string.IsNullOrEmpty(usr.FecIni) && !string.IsNullOrEmpty(usr.FecFin))
                            {
                                if (DateTime.Parse(usr.FecIni).CompareTo(DateTime.Parse(usr.FecFin)) == -1)
                                {
                                    usr.ConFin = 1;
                                }
                                else
                                {
                                    usr.ConFin = 0;
                                }
                            }

                            globales.VUsr.Add(usr);
                        }

                        returnValue = true;
                    }
                    else
                    {
                        if (result == null)
                        {
                            globales.ListaMensajesError.Add(new UI_Message
                            {
                                Text = "Se ha producido un error al leer los usuarios asociados al centro de costo " + CenCos,
                                Type = TipoMensaje.Informacion,
                                Title = MODGUSR.MsgUsr
                            });
                            tracer.TraceError("Se ha producido un error al leer los usuarios asociados al centro de costo " + CenCos);
                            return returnValue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    tracer.TraceException("usuarios asociados a un centro de costo", ex);
                    throw ex;
                }
                return returnValue;
            }
        }
 
        /// <summary>
        ///  Graba un Campo Memo y retorna el código de éste.-
        ///  Retorno    <> 0    : Grabación Exitosa-</summary>
        ///             =  0    : Error o Grabación no Exitosa.-
        /// </summary>
        /// <param name="CenCos"></param>
        /// <param name="CodUsr"></param>
        /// <param name="CodFec"></param>
        /// <returns></returns>
        public static int SyUpd_Usr(string CenCos, string CodUsr, string CodFec, string FechaActual, DatosGlobales globales,  UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer())
            {
                int SyUpd_Usr = 0;

                try
                {
                    var result = uow.SceRepository.sce_usr_u02_MS(MODGSYB.dbcharSy(CenCos),
                                                                MODGSYB.dbcharSy(CodUsr),
                                                                MODGSYB.dbcharSy(CodFec),
                                                                MODGSYB.dbcharSy(FechaActual));

                    if (result != null)
                    {
                        SyUpd_Usr = 1;
                    }

                    if (result.Column1 == -1 || result.Column2 != "Grabacion Exitosa")
                    {
                        globales.ListaMensajesError.Add(new UI_Message
                        {
                            Text = "Se ha producido un error al tratar de actualizar la fecha en Sce_Usr.",
                            Type = TipoMensaje.Error,
                            Title = MODGUSR.MsgUsr
                        });
                        tracer.TraceError("Se ha producido un error al tratar de actualizar la fecha en Sce_Usr.");
                    }

                    return SyUpd_Usr;

                }
                catch (Exception exc)
                {
                    tracer.TraceException("SyUpd_Usr", exc);
                    throw exc;
                }
            }
        }

    }
}
