using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.Core.Entities.Swift.Centinela;
using BCH.Comex.Data.DAL.Swift;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.Centinela
{
    public class CentinelaService: IDisposable
    {
        private UnitOfWorkSwift  uow;

        public CentinelaService()
        {
            uow = new UnitOfWorkSwift();
        }

        public void Dispose()
        {
            if (uow != null)
            {
                uow.Dispose();
            }
        }

        public IList<ConfiguracionAlerta> GetConfiguracionDeAlertasDeUsuario(IDatosUsuario datosUsuario)
        {
            List<ConfiguracionAlerta> configuracion = new List<ConfiguracionAlerta>();
            configuracion.Add(new ConfiguracionAlerta() { Aplicacion = ConfiguracionAlerta.AplicacionEmiteAlertas.EnvioSwift, MinsIntervaloConsultar = datosUsuario.MinsAlertaEnvioSwift });
            configuracion.Add(new ConfiguracionAlerta() { Aplicacion = ConfiguracionAlerta.AplicacionEmiteAlertas.AutorizacionSwift, MinsIntervaloConsultar = datosUsuario.MinsAlertaAutorizacionSwift });
            configuracion.Add(new ConfiguracionAlerta() { Aplicacion = ConfiguracionAlerta.AplicacionEmiteAlertas.AdminEnvioSwift, MinsIntervaloConsultar = datosUsuario.MinsAlertaAdminEnvioSwift });
            configuracion.Add(new ConfiguracionAlerta() { Aplicacion = ConfiguracionAlerta.AplicacionEmiteAlertas.RecepcionSwift, MinsIntervaloConsultar = datosUsuario.MinsAlertaRecepcionSwift });
            return configuracion;
        }


        public List<Alerta> ObtenerAlertasSiCorresponde(IDatosUsuario datosUsuario, IList<ConfiguracionAlerta> configuracion, List<Alerta> alertas)
        {
            if (configuracion != null)
            {
                if (alertas == null)
                {
                    alertas = new List<Alerta>();
                }

                foreach (ConfiguracionAlerta c in configuracion)
                {
                    if (c.MinsIntervaloConsultar.HasValue && c.MinsIntervaloConsultar.Value > 0)
                    {
                        if(!c.UltimaVezLeidas.HasValue || c.UltimaVezLeidas.Value.AddMinutes(c.MinsIntervaloConsultar.Value) <= DateTime.Now)
                        {
                            //nunca lei o ya transcurrió el tiempo configurado desde la última lectura

                            List<Alerta> alertasDeAplicacion = alertas.Where(a => a.Aplicacion == c.Aplicacion).ToList();
                            foreach (Alerta alertaExistenteEsaAplicacion in alertasDeAplicacion)
                            {
                                alertas.Remove(alertaExistenteEsaAplicacion);
                            }
                            
                            switch (c.Aplicacion)
                            {
                                case ConfiguracionAlerta.AplicacionEmiteAlertas.EnvioSwift:
                                    alertas.AddRange(ObtenerAlertasEnvioDeSwift(datosUsuario.RutEnFormatoBDSwift, c));
                                    break;

                                case ConfiguracionAlerta.AplicacionEmiteAlertas.AutorizacionSwift:
                                    alertas.AddRange(ObtenerAlertasAutorizacionSwift(datosUsuario.RutEnFormatoBDSwift, c));
                                    break;

                                case ConfiguracionAlerta.AplicacionEmiteAlertas.AdminEnvioSwift:
                                    alertas.AddRange(ObtenerAlertasAdminEnvioSwift(c));
                                    break;

                                case ConfiguracionAlerta.AplicacionEmiteAlertas.RecepcionSwift:
                                    alertas.AddRange(ObtenerAlertasRecepcionSwift(datosUsuario, c));
                                    break;
                            }
                        }
                    }
                    else
                    {
                        //la alerta esta desactivada
                    }
                }
            }

            return alertas;
        }

        private List<Alerta> ObtenerAlertasEnvioDeSwift(int rut, ConfiguracionAlerta c)
        {
            List<Alerta> alertas = new List<Alerta>();
            int cantMsgs = uow.MensajeRepository.proc_sw_env_test_firma_count(rut, DateTime.Now);

            if(cantMsgs > 0)
            {
                Alerta a = new Alerta()
                {
                    Aplicacion = ConfiguracionAlerta.AplicacionEmiteAlertas.EnvioSwift,
                    CantidadMensajes = cantMsgs,
                    RutUsuario = rut.ToString(),
                    Texto = "Mensajes swift sin solicitud de firmas"
                };

                alertas.Add(a);
            }
           
            c.UltimaVezLeidas = DateTime.Now;
            return alertas;
        }

        private List<Alerta> ObtenerAlertasAutorizacionSwift(int rut, ConfiguracionAlerta c)
        {
            List<Alerta> alertas = new List<Alerta>();
            int cantMsgs = 0;
            cantMsgs = uow.MensajeRepository.proc_sw_env_test_apr_MS_count(rut);

            if (cantMsgs > 0)
            {
                Alerta a = new Alerta()
                {
                    Aplicacion = ConfiguracionAlerta.AplicacionEmiteAlertas.AutorizacionSwift,
                    CantidadMensajes = cantMsgs,
                    RutUsuario = rut.ToString(),
                    Texto = "Autorizacion swift sin pendientes"
                };

                alertas.Add(a);
            }
           
            c.UltimaVezLeidas = DateTime.Now;
            return alertas;
        }

        private List<Alerta> ObtenerAlertasAdminEnvioSwift(ConfiguracionAlerta c)
        {
            List<Alerta> alertas = new List<Alerta>();
            int cantMsgs = uow.MensajeRepository.proc_sw_env_test_file_MS();

            if (cantMsgs > 0)
            {
                Alerta a = new Alerta()
                {
                    Aplicacion = ConfiguracionAlerta.AplicacionEmiteAlertas.AdminEnvioSwift,
                    CantidadMensajes = cantMsgs,
                    Texto = "Archivos pendientes para recepcionar"
                };

                alertas.Add(a);
            }
           
            c.UltimaVezLeidas = DateTime.Now;
            return alertas;
        }

        private List<Alerta> ObtenerAlertasRecepcionSwift(IDatosUsuario usuario, ConfiguracionAlerta c)
        {
            List<Alerta> alertas = new List<Alerta>();
            int casilla = 0;
            int.TryParse(usuario.Identificacion_CentroDeCostosOriginal, out casilla);

            int cantMsgs = uow.SwRepository.GetCountPendientesOReenviados(casilla);
            if (cantMsgs > 0)
            {
                Alerta a = new Alerta()
                {
                    Aplicacion = ConfiguracionAlerta.AplicacionEmiteAlertas.RecepcionSwift,
                    CantidadMensajes = cantMsgs,
                    Texto = "Mensajes pendientes o reenviados en Recepción Swift"
                };

                alertas.Add(a);
            }
           
            c.UltimaVezLeidas = DateTime.Now;
            return alertas;
        }


        
    }
}
