using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XGFD.Modulos
{
    internal static class MODGUSR1
    {

        /// <summary>
        /// Graba un Campo Memo y retorna el código de éste.-
        /// Retorno    <> 0    : Grabación Exitosa-
        ///            =  0    : Error o Grabación no Exitosa.-
        /// </summary>
        /// <param name="CenCos"></param>
        /// <param name="CodUsr"></param>
        /// <param name="CodFec"></param>
        /// <returns>true si hay error, false si no</returns>
        public static bool SyUpd_Usr(string CenCos, string CodUsr, string CodFec, UnitOfWorkCext01 uow, IList<UI_Message> listaMensajes)
        {
            using (var tracer = new Tracer())
            {
                try
                {
                    var result = uow.SceRepository.sce_usr_u02_MS(CenCos, CodUsr, CodFec, DateTime.Now);

                    if (result == null || result.Column2 != "Grabacion Exitosa")
                    {
                        tracer.AddToContext("result.Column1", result.Column1);
                        tracer.AddToContext("result.Column2", result.Column2);

                        listaMensajes.Add(new UI_Message
                        {
                            Title = "Fin de dia",
                            Text = "Se ha producido un error al tratar de actualizar la fecha en Sce_Usr. El Servidor reporta : [" +
                            result.Column2 + "]. Reporte este problema.",
                            Type = TipoMensaje.Error
                        });

                        return true;
                    }

                    return false;
                }
                catch (Exception exc)
                {
                    if (!ExceptionPolicy.HandleException(exc, "PoliticaBLFundTransfer")) throw;
                }

                return false;
            }
        }
    }
}
