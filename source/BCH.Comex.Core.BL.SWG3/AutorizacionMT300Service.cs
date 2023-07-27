using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.Data.DAL.Swift;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.SWG3
{
    public class AutorizacionMT300Service : IDisposable
    {
        private readonly UnitOfWorkSwift uow;
        private const string textoEsperaAut= "Mensaje en espera de Autorización";
        private const string textoAutorizado = "Mensaje Autorizado";

        public AutorizacionMT300Service()
        {
            uow = new UnitOfWorkSwift();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (uow != null)
            {
                uow.Dispose();
            }
        }

        public void FirmaryAutorizarMensaje(int idMensaje, int rutUsuario, int casilla,List<int> rutFirmantes, Dictionary<string, string> paramGeneracion, ref bool estado)
        {
            try
            {
                using (var tracer = new Tracer("Firmar y Autorizar Mensaje"))
                {
                    tracer.TraceVerbose("Comienza el borrado de firmas");
                    estado = uow.FirmaRepository.proc_sw_env_del_firnul_MS(idMensaje, rutUsuario, DateTime.Now); 

                    if (estado)
                    {
                        IList<sw_msgsend_firma> firmas = obtieneListaFirmantes(rutFirmantes, paramGeneracion);
                        tracer.TraceVerbose("Comienza el grabado de las firmas");
                        estado = SaveFirmas(idMensaje, firmas, rutUsuario);

                        tracer.TraceVerbose("Comienza Cambio de estado firmas SAP");
                        string comentario = textoEsperaAut;

                        estado = CambiaEstadoFirmasSAP(casilla, idMensaje, rutUsuario, DateTime.Now, comentario);
                        if (estado)
                        {
                            tracer.TraceVerbose("Las firmas se solicitaron satisfactoriamente");
                            comentario = textoAutorizado;
                            bool tieneFirmasNecesarias = false;
                            estado = SaveFirmaEnvAutm(idMensaje, casilla, rutUsuario, DateTime.Now, comentario, out tieneFirmasNecesarias);
                            if (estado)
                            {
                                if (tieneFirmasNecesarias)
                                {
                                    tracer.TraceInformation("El mensaje se autorizó satisfactoriamente.");
                                }
                                else {
                                    throw new Exception("No tiene las firmas necesarias para autorizar mensaje");
                                } 
                          
                            }
                            else
                            {
                                throw new Exception("No se ha podido autorizar el mensaje ");
                            }
                        }
                        else
                        {
                            throw new Exception("No se ha podido actualizar el mensaje ");
                        }

                    }
                    else
                    {
                        throw new Exception("No se pudieron eliminar las firmas removidas");
                    }

                }

            }
            catch (Exception x)
            {
                throw new Exception("FirmaryAutorizarMensaje " + x.Message);
            }
        }


        public bool SaveFirmas(int idMensaje, IList<sw_msgsend_firma> firmas, int rutUsuarioSolicita)
        {
            bool todosOK = true;

            using (Tracer tracer = new Tracer())
            {
                try
                {
                    foreach (sw_msgsend_firma firma in firmas)
                    {
                        bool ok = SaveFirma(idMensaje, firma, rutUsuarioSolicita, DateTime.Now);
                        if (!ok)
                        {
                            todosOK = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, problemas al grabar las firmas", ex);
                    return false;
                }
            }

            return todosOK;
        }

        public bool SaveFirma(int idmensaje, sw_msgsend_firma firma, int rut_solic, DateTime fecha_solic)
        {
            return uow.AdministracionRepository.proc_sw_env_ing_firma(idmensaje, firma.rut_firma, firma.tipo_firma, firma.estado_firma, firma.revisa_firma, rut_solic, fecha_solic.ToString("yyyy-MM-dd HH:mm:ss"), firma.avisado);
        }

        public bool CambiaEstadoFirmasSAP(int casilla, int idMensaje, int rut, DateTime fecha, string comentario)
        {
            using (Tracer tracer = new Tracer())
            {
                try
                {
                    return uow.SwRepository.proc_sw_env_graba_sap(casilla, idMensaje, rut, fecha, comentario);
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, ha ocurrido un problema al cambiar estado firmas SAP", ex);
                    throw;
                }
            }
        }


        public bool SaveFirmaEnvAutm(int idmensaje, int casilla, int rut, DateTime fecha_solic, string comentario, out bool tieneFirmasNecesarias)
        {
            using (Tracer tracer = new Tracer())
            {
                try
                {
                    int resultadoNoTieneFirmasNecesarias = -100;
                    int result = uow.AdministracionRepository.proc_sw_env_graba_aum_MS(idmensaje, casilla, rut, fecha_solic, comentario);
                    tieneFirmasNecesarias = false;

                    if (result < 0)
                    {
                        if (result == resultadoNoTieneFirmasNecesarias)
                        {
                            tieneFirmasNecesarias = false;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        tieneFirmasNecesarias = true;
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, ha ocurrido un problema al cambiar al autorizar mensaje en EnvAutm", ex);
                    throw;
                }
            }
        }

        IList<sw_msgsend_firma> obtieneListaFirmantes(List<int> rutFirmantes, Dictionary<string, string> paramGeneracion)
        {

            IList<sw_msgsend_firma> listaFirmas = new List<sw_msgsend_firma>();
            sw_msgsend_firma firma;

            foreach (int rut in rutFirmantes)
            {
                firma = new sw_msgsend_firma();
                firma.tipo_firma = paramGeneracion["firma-tipo"];
                firma.estado_firma = paramGeneracion["firma-estado"];
                firma.revisa_firma = paramGeneracion["firma-revisa"];
                firma.avisado = paramGeneracion["firma-avisado"];
                firma.rut_firma = rut;
                listaFirmas.Add(firma);
            }

            return listaFirmas;
        }
    }
}
