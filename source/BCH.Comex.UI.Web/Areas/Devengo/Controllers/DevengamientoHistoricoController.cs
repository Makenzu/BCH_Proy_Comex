using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.UI.Web.Areas.Devengo.Models;
using BCH.Comex.UI.Web.Common;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;


namespace BCH.Comex.UI.Web.Areas.Devengo.Controllers
{
    public class DevengamientoHistoricoController : BaseController
    {
        //
        // GET: /Devengo/DevengamientoHistorico/
        public ActionResult Index()
        {
            List<UI_Message> listaMensajes = new List<UI_Message>();
            DevengamientoHistoricoViewModel o = new DevengamientoHistoricoViewModel();
            if (this.Globales != null)
            {
                this.service.FrmDevHistInit(this.Globales.DatosDevHist, listaMensajes);
                o = new DevengamientoHistoricoViewModel(this.Globales.DatosDevHist.periodos, listaMensajes);
            }
            else
            {
                listaMensajes.Add(new UI_Message() { Text = "Problemas al obtener la información del usuario", Type = TipoMensaje.Error });
                o.update(listaMensajes);
            }

            return View(o);
        }
        

        public ActionResult DevengamientoHistorico()
        {
            List<UI_Message> listaMensajes = new List<UI_Message>();
            DevengamientoHistoricoViewModel o = new DevengamientoHistoricoViewModel();
            if (this.Globales != null)
            {
                this.service.FrmDevHistInit(this.Globales.DatosDevHist, listaMensajes);
                o = new DevengamientoHistoricoViewModel(this.Globales.DatosDevHist.periodos,listaMensajes);
            }
            else
            {
                listaMensajes.Add(new UI_Message() { Text = "Problemas al obtener la información del usuario", Type = TipoMensaje.Error });
                o.update(listaMensajes);
            }

            return View(o);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listaPeriodos"></param>
        /// <param name="Command"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DescargaDevengamientoHistorico(string listaPeriodos, string Command)
        {
            if (Command == "Volver")
            {
                return new RedirectResult("~/Devengo");
            }
            else
            {
                DevengamientoHistoricoViewModel modelo = new DevengamientoHistoricoViewModel();
                this.Globales.ListaMensajesError.Clear();
                this.Globales.DatosDevHist.periodoSeleccionado = (int)(listaPeriodos == string.Empty ? 0 : int.Parse(listaPeriodos));
                // dentro de buscar, existe una validacion de campos, si no es valida no continuamos con la búsqueda y ponemos el foco en el control
                if(this.service.FrmDevHist_btnBuscar_click(this.Globales.DatosDevHist, this.Globales.ListaMensajesError))
                {
                    MemoryStream ms = new MemoryStream();

                    if (this.Globales.ListaMensajesError.Count == 0)
                    {
                        ms.Position = 0;
                        ms = this.service.FrmDevHistGetFile(this.Globales.DatosDevHist);
                        var fName = string.Format("DEVENGAMIENTOHISTORICOS-{0}.xlsx", System.DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                        Session[fName] = ms;
                        return Json(new { success = true, fName }, JsonRequestBehavior.AllowGet);
                    }
                }
                modelo.update(this.Globales.DatosDevHist.periodos, this.Globales.DatosDevHist.periodoSeleccionado, this.Globales.ListaMensajesError);

            }
            return Json(new { success = false, this.Globales.ListaMensajesError }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fName"></param>
        /// <returns></returns>
        [HttpPost, FileDownload]
        public ActionResult DescargaDocumento(string fName)
        {
            var ms = Session[fName] as MemoryStream;
            if (ms == null)
                return new EmptyResult();
            Session[fName] = null;
            return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fName);
        }
    }
}