using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XGSV.Modulos
{
    public static class MODPROD
    {
        public const string MsgProd = "Productos de COMEX";

        public static bool SyGetn_Pro(DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer())
            {
                List<T_Prod> result = new List<T_Prod>();
                try
                {
                    List<sce_prd_s01_MS_Result> productsData = uow.SceRepository.sce_prd_s01_MS();
                    if (productsData != null)
                    {
                        if (globales.FrmCeder.Productos != null)
                        {
                            globales.FrmCeder.Productos.Clear();
                        }
                        else
                        {
                            globales.FrmCeder.Productos = new List<T_Prod>();
                        }
                            
                        foreach (sce_prd_s01_MS_Result product in productsData)
                        {
                            result.Add(new T_Prod
                            {
                                codPro = product.codpro,
                                desPro = product.despro,
                                estado = 0
                            });
                        }
                        globales.FrmCeder.Productos.AddRange(result);
                    }
                    else
                    {
                        globales.ListaMensajesError.Add(new UI_Message
                        {
                            Text = "Se ha producido un error al tratar de leer la tabla de Productos Comex.",
                            Type = TipoMensaje.Error,
                            Title = MODPROD.MsgProd
                        });
                        tracer.TraceError("Se ha producido un error al tratar de leer la tabla de Productos Comex.");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    globales.ListaMensajesError.Add(new UI_Message
                    {
                        Text = "Se ha producido un error al tratar de leer la tabla de Productos Comex: " + ex.Message,
                        Type = TipoMensaje.Error,
                        Title = "Error"
                    });
                    tracer.TraceException("Se ha producido un error al tratar de leer la tabla de Productos Comex:", ex);
                    throw ex;
                }

                return true;
            }
        }
    }
}
