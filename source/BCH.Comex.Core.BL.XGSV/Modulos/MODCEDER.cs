using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XGSV.Modulos
{
    public static class MODCEDER
    {
        public const int AnchoColumnaIdCliente = 12;

        public const string MsgCeder = "Ceder Cartera";

        public static List<string> VCli = null;

        public static bool SyGetn_Prt(DatosGlobales globales, UnitOfWorkCext01 uow, string CenCos, string CodUsr, string productos)
        {
            bool result = false;
            using (Tracer tracer = new Tracer())
            {
                try
                {
                    if (globales.FrmCeder.Clientes != null)
                    {
                        globales.FrmCeder.Clientes.Clear();
                        if (globales.FrmCeder.Operaciones != null)
                            globales.FrmCeder.Operaciones.Clear();
                    }


                    List<sce_gcar_s05_MS_Result> customerData = uow.SceRepository.sce_gcar_s05_MS(CenCos, CodUsr, productos);
                    if (customerData.Any() || customerData.Count() != 0)
                    {

                        foreach (sce_gcar_s05_MS_Result cliente in customerData)
                        {
                            List<string> razonSocialCliente = uow.SceRepository.sce_rsa_s03(cliente.prtcli, 0).ToList();
                            string clienteID = cliente.prtcli.Replace("|", String.Empty);

                            globales.FrmCeder.Clientes.Add(new T_Cliente()
                            {
                                rutOriginal = cliente.prtcli,
                                rut = cliente.prtcli.Replace("|", String.Empty),
                                razonSocial = razonSocialCliente.FirstOrDefault()
                            });

                        }

                        result = true;
                    }
                    else
                    {
                        globales.ListaMensajesError.Add(new UI_Message
                        {
                            Text = "No se encontró información de clientes para los datos seleccionados ",
                            Type = TipoMensaje.Informacion,
                            Title = MODCEDER.MsgCeder
                        });
                    }
                }
                catch (Exception ex)
                {
                    globales.ListaMensajesError.Add(new UI_Message
                    {
                        Text = "Se ha producido un error al tratar de leer los datos de los Clientes y sus Productos.",
                        Type = TipoMensaje.Error,
                        Title = "Error"
                    });
                    tracer.TraceException("Alerta: Se produjo un problema en SyGetn_Prt", ex);
                    throw ex;
                }
            }

            return result;
        }

        public static bool SyGetn_Prod(DatosGlobales globales, UnitOfWorkCext01 uow, string CenCos, string CodUsr, string productos, string clienteID)
        {
            bool result = false;

            try
            {
                string codigoCliente = clienteID;
                codigoCliente = codigoCliente.Substring(0, 12).Trim();
                if (globales.FrmCeder.Operaciones != null)
                    globales.FrmCeder.Operaciones.Clear();

                IList<sce_gcar_s06_MS_Result> operationCount = uow.SceRepository.sce_gcar_s06_MS(CenCos, CodUsr, codigoCliente, productos);
                if (operationCount.Any())
                {
                    foreach (var item in operationCount)
                    {
                        string producto = globales.FrmCeder.Productos.Where(p => p.codPro == item.codpro.ToString().PadLeft(2, '0')).Select(p => p.desPro).FirstOrDefault();
                        globales.FrmCeder.Operaciones.Add(new T_OpeCli() { canOpe = (int)item.cantidad, codPro = (int)item.codpro, desPro = producto });
                    }
                    result = true;
                }
                else
                {
                    globales.ListaMensajesError.Add(new UI_Message
                    {
                        Text = "Se ha producido un error al tratar de leer los datos de las Operaciones del Cliente.",
                        Type = TipoMensaje.Error,
                        Title = MODCEDER.MsgCeder
                    });
                }
            }
            catch (Exception ex)
            {
                globales.ListaMensajesError.Add(new UI_Message
                {
                    Text = "Se ha producido un error al tratar de leer los datos de las Operaciones del Cliente: " + ex.Message,
                    Type = TipoMensaje.Error,
                    Title = "Error"
                });

                throw ex;
            }

            return result;
        }

        public static bool SyPut_Cart(DatosGlobales globales, UnitOfWorkCext01 uow, string CenCos, string CodUsr, string CctAct, string UsrAct, string party, string productos)
        {
            using (Tracer tracer = new Tracer())
            {
                bool result = false;
                try
                {
                    DateTime? fechaCesion = DateTime.Now;

                    sce_gcar_u03_MS_Result insertResult = uow.SceRepository.sce_gcar_u03_MS(CenCos, CodUsr, CctAct, UsrAct, fechaCesion, party, productos);
                    if (insertResult.Column1 == 0)
                    {
                        globales.FrmCeder = new FrmCederDTO();
                        globales.ListaMensajesError.Add(new UI_Message
                        {
                            Text = "Se acaban de Ceder con éxito algunas Carteras.",
                            Type = TipoMensaje.Informacion,
                            Title = MODCEDER.MsgCeder,
                            AutoClose = true
                        });
                    }
                    else
                    {
                        globales.ListaMensajesError.Add(new UI_Message
                        {
                            Text = "Se ha producido un error al tratar de grabar los datos.",
                            Type = TipoMensaje.Error,
                            Title = MODCEDER.MsgCeder
                        });
                        tracer.TraceError("Se ha producido un error al tratar de grabar los datos: " + insertResult.Column2);
                    }

                    result = true;
                }
                catch (Exception ex)
                {
                    globales.ListaMensajesError.Add(new UI_Message
                    {
                        Text = "Se ha producido un error al tratar de grabar los datos: " + ex.Message,
                        Type = TipoMensaje.Error,
                        Title = "Error"
                    });
                    tracer.TraceException("Se ha producido un error al tratar de grabar los datos", ex);
                    throw ex;
                }

                return result;
            }
        }
    }
}
