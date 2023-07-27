using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Devengo;
using BCH.Comex.UI.Web.Areas.Devengo.Models;
using BCH.Comex.UI.Web.Common;
using System.IO;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Devengo.Controllers
{
    public class ConsultaDevengamientoController : BaseController
    {
        //
        // GET: /Devengo/ConsultaDevengamiento/
        public ActionResult Index()
        {
            ConsultaDevengamientoViewModel o = new ConsultaDevengamientoViewModel();
            if (this.Globales != null)
            {
                if (this.Globales.DatosIntReaj == null)
                {
                    this.Globales.DatosIntReaj = new DevengIntReaj();
                }
                this.service.FrmDevengIntReajInit(this.Globales);
                string titulo = this.Globales.DatosIntReaj.TipoConsultaDev == 1 ? "Intereses" : "Reajustes";
                o = new ConsultaDevengamientoViewModel(this.Globales.DatosIntReaj.Cmbperiodo, titulo, this.Globales.ListaMensajesError);
            }
            return View(o);
        }

        public ActionResult ConsultaDevengamientoInteres()
        {
            ConsultaDevengamientoViewModel o = new ConsultaDevengamientoViewModel();
            if (this.Globales != null)
            {
                this.Globales.ListaMensajesError.Clear();
                if (this.Globales.DatosIntReaj == null)
                {
                    this.Globales.DatosIntReaj = new DevengIntReaj();
                }
                this.Globales.DatosIntReaj.TipoConsultaDev = 1;
                this.service.FrmDevengIntReajInit(this.Globales);
                string titulo = this.Globales.DatosIntReaj.TipoConsultaDev == 1 ? "Intereses" : "Reajustes";
                o = new ConsultaDevengamientoViewModel(this.Globales.DatosIntReaj.Cmbperiodo, titulo, Globales.ListaMensajesError);
            }

            return View("ConsultaDevengamiento", o);
        }

        public ActionResult ConsultaDevengamientoReajuste()
        {
            ConsultaDevengamientoViewModel o = new ConsultaDevengamientoViewModel();
            if (this.Globales != null)
            {
                if (this.Globales.DatosIntReaj == null)
                {
                    this.Globales.DatosIntReaj = new DevengIntReaj();
                }
                this.Globales.DatosIntReaj.TipoConsultaDev = 2;
                this.service.FrmDevengIntReajInit(this.Globales);
                string titulo = this.Globales.DatosIntReaj.TipoConsultaDev == 1 ? "Intereses" : "Reajustes";
                o = new ConsultaDevengamientoViewModel(this.Globales.DatosIntReaj.Cmbperiodo, titulo, Globales.ListaMensajesError);
            }
            return View("ConsultaDevengamiento", o);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listaPeriodos"></param>
        /// <param name="Command"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DescargaConsultaDevengamiento(string listaPeriodos, string Command)
        {
            if (Command == "Volver")
            {
                return new RedirectResult("~/Devengo");
            }
            else
            {
                ConsultaDevengamientoViewModel modelo = new ConsultaDevengamientoViewModel();
                this.Globales.ListaMensajesError.Clear();
                this.Globales.DatosIntReaj.CmbperiodoSelected = (int)(listaPeriodos == string.Empty ? 0 : int.Parse(listaPeriodos));
                string titulo = string.Empty;
                if (this.service.btnBuscar_click(this.Globales))
                {
                    titulo = this.Globales.DatosIntReaj.TipoConsultaDev == 1 ? "Intereses" : "Reajustes";
                    MemoryStream ms = new MemoryStream();
                    if (this.Globales.DatosIntReaj.TipoConsultaDev == 1)
                    {
                        if (this.Globales.DatosIntReaj.DevengInt != null && this.Globales.DatosIntReaj.DevengInt.Count > 0)
                        {
                            ms.Position = 0;
                            ms = this.service.FrmDevengIntReajFileConsulta(this.Globales.DatosIntReaj.DevengInt, titulo);
                            var fName = string.Format("DEVENGAMIENTOINTERESES-{0}.xlsx", System.DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                            Session[fName] = ms;
                            return Json(new { success = true, fName }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            this.Globales.ListaMensajesError.Add(new UI_Message() { Text = "No existe información para la consulta realizada.", Type = TipoMensaje.Informacion });
                        }
                    }
                    else if (this.Globales.DatosIntReaj.TipoConsultaDev == 2)
                    {
                        if (this.Globales.DatosIntReaj.DevengReaj != null && this.Globales.DatosIntReaj.DevengReaj.Count > 0)
                        {
                            ms.Position = 0;
                            ms = this.service.FrmDevengIntReajFileConsulta(this.Globales.DatosIntReaj.DevengReaj, titulo);
                            var fName = string.Format("DEVENGAMIENTOREAJUSTES-{0}.xlsx", System.DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                            Session[fName] = ms;
                            return Json(new { success = true, fName }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            this.Globales.ListaMensajesError.Add(new UI_Message() { Text = "No existe información para la consulta realizada.", Type = TipoMensaje.Informacion });
                        }
                    }
                }
                
                modelo.update(this.Globales.DatosIntReaj.Cmbperiodo, titulo, this.Globales.DatosIntReaj.CmbperiodoSelected, this.Globales.ListaMensajesError);
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
            if(ms == null)
                return new EmptyResult();
            Session[fName] = null;
            return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fName);
        }
    }
}