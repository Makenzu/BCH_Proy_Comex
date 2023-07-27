using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.UI.Web.Areas.Supervisor.Models;
using BCH.Comex.UI.Web.Common;
using System.Web.Mvc;
using System.Linq;
using BCH.Comex.Core.BL.XGSV.Modulos;

namespace BCH.Comex.UI.Web.Areas.Supervisor.Controllers
{
    public class VisualizacionUsuarioController : BaseController
    {

        //
        // GET: /Supervisor/VisualizacionUsuario/

        public ActionResult Index()
        {
            using (Tracer tracer = new Tracer())
            {
                if (this.Globales.FrmUsr == null)
                    this.Globales.FrmUsr = new FrmgUsrDTO();

                Globales.ListaMensajesError.Clear();
                service.VisualizacionUsuarioInit(this.Globales);
                EspecialistaViewModel evm = InicializarModelo(this.Globales);
                return View(evm);
            }
        }

        [HttpPost]
        public ActionResult Index(EspecialistaViewModel evm, string Command)
        {
            using (Tracer tracer = new Tracer("Supervisor - Visualizacion de usuarios"))
            {
                this.Globales.ListaMensajesError.Clear();
                switch (Command)
                {
                    case "Cancelar":
                        return new RedirectResult("~/Supervisor");
                    case "Cierre Diario":
                        service.VisualizacionUsuario_CierreClick(this.Globales);
                        break;
                    case "Des. Cierre":
                        bool deshacerCierreOK = service.VisualizacionUsuario_UnCierreClick(this.Globales.UsrEsp.cent_costo, 
                            this.Globales.UsrEsp.id_super, this.Globales);
                        //if (deshacerCierreOK)
                        //{
                        //    //quito la marca de no dejar entrar a las otras apps
                        //    Session[SessionKeys.Common.FinDiaEjecutado] = null;
                        //}
                        break;
                }
                evm = InicializarModelo(this.Globales);
                return View(evm);
            }
        }

        private EspecialistaViewModel InicializarModelo(DatosGlobales g)
        {
            EspecialistaViewModel model = new EspecialistaViewModel();
            model.supervisor = g.FrmUsr.Supervisor;
            model.ltEspecialista = (from x in g.VUsr
                              where x.CodEsp != "00" &&
                              x.Jerarquia != MODGSUP.Jerarquia_Sec
                              select x).ToList();

            model.ltEspecialistaFill = (from x in g.VUsr
                                  where x.CenSup == g.UsrEsp.cent_costo &&
                                  x.CodSup == g.UsrEsp.id_especia &&
                                  x.CodEsp != "00" &&
                                  x.Jerarquia != MODGSUP.Jerarquia_Sec
                                  select x).ToList();

            model.opciones = g.FrmUsr.Opcion.ToList().Select(x => new SelectListItem { Text = x.Value, Value = x.Key }).ToList();

            model.unCierre = g.FrmUsr.UnCierre;
            model.cierre = g.FrmUsr.Cierre;
            model.MsgCierre = g.FrmUsr.MsgCierre;
            model.ClassMsgCierre = g.FrmUsr.ClassMsgCierre;

            model.ListaErrores = g.ListaMensajesError;

            return model;
        }

        [HttpPost]
        public ActionResult VisualizacionUsuario_FinDia(string cecoEsp)
        {
            using (Tracer tracer = new Tracer("Forzar Fin de Dia"))
            {
                this.Globales.ListaMensajesError.Clear();
                service.VisualizacionUsuario_FinDiaClick(cecoEsp.Split('-')[0], cecoEsp.Split('-')[1], this.Globales);

                //marco la variable para no dejar entrar a las otras apps
                Session[SessionKeys.Common.FinDiaEjecutado] = true.ToString();

                var evm = InicializarModelo(this.Globales);
                return Json(evm, JsonRequestBehavior.AllowGet);
            }
        }

	}
}