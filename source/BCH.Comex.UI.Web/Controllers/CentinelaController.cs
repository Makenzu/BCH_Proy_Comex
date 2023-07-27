using BCH.Comex.Common;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.Centinela;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.Core.Entities.Swift.Centinela;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Models.ConsultaSwift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Controllers
{
    public class CentinelaController: BaseController
    {
        CentinelaService service = new CentinelaService();

        [HandleAjaxException, HttpGet]
        public ActionResult GetAlertas()
        {
            using (var tracer = new Tracer("GetAlertas"))
            {
                bool EnvioSwiftAppRole = false;
                //si tiene permiso para Envio de mensajeria Swift, muestro alertas
                try
                {
                    EnvioSwiftAppRole = User.IsInRole(Constantes.AppRoles.EnvioSwiftAppRole);
                }
                catch (Exception ex)
                {
                    EnvioSwiftAppRole = false;
                    tracer.TraceException("Alerta al usar IsInRole, asumo que el usuario no pertenece", ex);
                }

                IList<ConfiguracionAlerta> config;
                if (EnvioSwiftAppRole)
                {
                    config = GetConfigAlertasDeCacheOBL();
                }
                else
                {
                    config = GetConfigAlertasDeCacheOBL().Where(x => x.Aplicacion != ConfiguracionAlerta.AplicacionEmiteAlertas.EnvioSwift).ToList();
                }

                IList<Alerta> alertas = GetAlertasDeCacheOBL(config);

                var data = new { Alertas = alertas, TodasDeshabilitadas = config.All(a => a.MinsIntervaloConsultar == null || a.MinsIntervaloConsultar.Value == 0) };

                return new JsonResult()
                {
                    Data = data,
                    MaxJsonLength = Int32.MaxValue,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        public ActionResult NavergarAAccionDeAlerta(ConfiguracionAlerta.AplicacionEmiteAlertas aplicacion)
        {
            EliminarAlerta(aplicacion);

            switch (aplicacion)
            {
                case ConfiguracionAlerta.AplicacionEmiteAlertas.EnvioSwift:
                    return RedirectToAction("Index", "ConsultaSwift", new { paraAccion = (byte)ConsultarParaAccion.FirmasNoSolicitadas });
                case ConfiguracionAlerta.AplicacionEmiteAlertas.AutorizacionSwift:
                    return RedirectToAction("Index", "AutorizacionSwift");
                case ConfiguracionAlerta.AplicacionEmiteAlertas.AdminEnvioSwift:
                    return RedirectToAction("GestionArchivos", "AdminSwift");

                case ConfiguracionAlerta.AplicacionEmiteAlertas.RecepcionSwift:
                    return RedirectToAction("Index", "RecibirMensaje"); 
                
                default:
                    return RedirectToAction("Index", "ConsultaSwift", new { paraAccion = (byte)ConsultarParaAccion.FirmasNoSolicitadas });
            
            }
        }

        private IList<Alerta> GetAlertasDeCacheOBL(IList<ConfiguracionAlerta> config)
        {
            List<Alerta> alertasExistentes = null;
            if (this.ControllerContext.HttpContext.Session[SessionKeys.Common.Alertas] != null)
            {
                alertasExistentes = this.ControllerContext.HttpContext.Session[SessionKeys.Common.Alertas] as List<Alerta>;
            }
            
            List<Alerta> result = service.ObtenerAlertasSiCorresponde(infoUsuario, config, alertasExistentes);
            this.ControllerContext.HttpContext.Session[SessionKeys.Common.Alertas] = result;
            return result;
        }
        
        private IList<ConfiguracionAlerta> GetConfigAlertasDeCacheOBL()
        {
            if (this.ControllerContext.HttpContext.Session[SessionKeys.Common.ConfigAlertas] == null)
            {
                IList<ConfiguracionAlerta> result = service.GetConfiguracionDeAlertasDeUsuario(infoUsuario);
                this.ControllerContext.HttpContext.Session[SessionKeys.Common.ConfigAlertas] = result;
                return result;
            }
            else
            {
                return this.ControllerContext.HttpContext.Session[SessionKeys.Common.ConfigAlertas] as IList<ConfiguracionAlerta>;
            }
        }

        private void EliminarAlerta(ConfiguracionAlerta.AplicacionEmiteAlertas aplicacion)
        {
            List<Alerta> alertasExistentes = null;
            if (this.ControllerContext.HttpContext.Session[SessionKeys.Common.Alertas] != null)
            {
                alertasExistentes = this.ControllerContext.HttpContext.Session[SessionKeys.Common.Alertas] as List<Alerta>;
                if (alertasExistentes != null)
                {
                    Alerta alertaClickeada = alertasExistentes.Where(a => a.Aplicacion == aplicacion).FirstOrDefault();
                    if (alertaClickeada != null)
                    {
                        alertasExistentes.Remove(alertaClickeada);
                    }
                }
            }
        }
    }
}