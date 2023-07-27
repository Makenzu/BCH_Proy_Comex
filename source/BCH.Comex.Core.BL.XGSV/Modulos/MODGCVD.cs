using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XGSV.Modulos
{
    public class MODGCVD
    {
        public static string GeneraTRXID(string opeSin, UnitOfWorkCext01 uow, List<UI_Message> mensajes)
        {
            using (Tracer tracer = new Tracer())
            {
                string correlativo;
                string fecjul = Fecha_Juliana();

                correlativo = SyGetCorre(uow, mensajes);

                if (string.IsNullOrEmpty(fecjul) || correlativo == "0")
                {
                    mensajes.Add(new UI_Message
                    {
                        Text = "Problemas en la generacion del TransactionId.",
                        Type = TipoMensaje.Critical,
                        Title = "Error en la operación"
                    });
                    tracer.TraceError("Problemas en la generacion del TransactionId.");
                    return String.Empty;
                }
                else
                {
                    correlativo = correlativo.PadLeft(5, '0');
                    return "CBSCVD" + DateTime.Now.ToString("yyMMdd") + opeSin.Substring(0, 3) + fecjul + correlativo;
                }
            }
        }

        public static string Fecha_Juliana()
        {
            return DateTime.Now.ToString("yy") + DateTime.Now.DayOfYear.ToString("000");
        }

        /// <summary>
        /// Se consulta correlativo trxid
        /// </summary>
        /// <returns></returns>
        public static string SyGetCorre(UnitOfWorkCext01 uow, List<UI_Message> mensajes)
        {
            using (Tracer tracer = new Tracer())
            {
                string _retValue = "";
                string mensaje = "";
                string corre = "";
                int? intCorre;
                string Retorno = "";
                int queryResultCount;

                try
                {
                    queryResultCount = uow.SceRepository.pro_sce_trxcor_ft_MS(out Retorno, out mensaje, out intCorre);
                    corre = intCorre.HasValue ? intCorre.ToString() : string.Empty;

                    _retValue = corre;
                }
                catch (Exception ex)
                {
                    //TODO:@estanislao manejo de excepciones
                    mensajes.Add(new UI_Message
                    {
                        Text = "Se ha producido un error al tratar de rescatar correlativo de TrxId",
                        Type = TipoMensaje.Informacion,
                        Title = "Error en la operación"
                    });
                    tracer.TraceException("Se ha producido un error al tratar de rescatar correlativo de TrxId", ex);
                }

                return _retValue;
            }
        }
    }
}
