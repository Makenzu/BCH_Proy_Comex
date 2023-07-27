using BCH.Comex.Common.Tracing;
using BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Controllers
{
    public class SeleccionOficinaController : BaseControllerCG
    {
        //
        // GET: /ContabilidadGenerica/SeleccionOficina/
        public ActionResult Index()
        {
            using (Tracer tracer = new Tracer())
            {
                return View();
            }
        }
     
        public ActionResult SeleccionOficina()
        {
            return Rutear(globales =>
            {
                globales.Action = null;
                this.service.SeleccionBancoInit(globales);
            },
            globales =>
            {
                SeleccionOficinaViewModel pcvm = new SeleccionOficinaViewModel(globales.Frm_SeleccionOficina);
                return View(pcvm);
            });
        }
 
        [HttpPost]
        public ActionResult SeleccionOficina(SeleccionOficinaViewModel sovm, string cmdButton)
        {
            return Rutear(globales =>
            {
                globales.MESSAGES.Clear();                
                sovm.Update(globales.Frm_SeleccionOficina);

                if (cmdButton == "Aceptar")
                {
                    service.Seleccion_Banco_AceptarBanco(globales);

                    if (globales.MESSAGES.Count() == 0)
                    {
                        globales.Controller = "Nueva";
                        globales.Action = "Operacion_2";
                        globales.VieneDeAction = string.Empty;
                    }
                }
                else if (cmdButton == "Cancelar")
                {
                    service.Seleccion_Banco_CancelarBanco(globales);
                    globales.Controller = "Home";
                    globales.Action = "Index";
                }
                
            },
            globales =>
            {
                SeleccionOficinaViewModel pcvm = new SeleccionOficinaViewModel(globales.Frm_SeleccionOficina);
                pcvm.ListaErrores = globales.MESSAGES;
                return View(pcvm);
            });
        }


	}
}