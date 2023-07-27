using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Devengo;
using BCH.Comex.UI.Web.Areas.Devengo.Models;
using BCH.Comex.UI.Web.Common;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Devengo.Controllers
{
    public class ConsultaInterfazCDRController : BaseController
    {
        //
        // GET: /Devengo/ConsultaIntefazCDR/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConsultaInterfazCDR()
        {
            ConsultaInterfazCDRViewModel o = new ConsultaInterfazCDRViewModel();
            return View(o);
        }

        public ActionResult ConsultaCarteraCDR()
        {
            T_INFORME infor = new T_INFORME(true);
            List<UI_Message> listaMensajes = new List<UI_Message>();

            this.service.FrmInformeInit(infor, listaMensajes);

            ConsultaInterfazCDRViewModel o = new ConsultaInterfazCDRViewModel(true, infor.periodos);
            this.Globales.DatosInforme = infor;

            return View("ConsultaInterfazCDR",o);
        }

        public ActionResult ConsultaDevengamientoCDR()
        {
            T_INFORME infor = new T_INFORME(false);
            List<UI_Message> listaMensajes = new List<UI_Message>();

            this.service.FrmInformeInit(infor, listaMensajes);
            this.Globales.DatosInforme = infor;

            ConsultaInterfazCDRViewModel o = new ConsultaInterfazCDRViewModel(false, infor.periodos);
            return View("ConsultaInterfazCDR",o);
        }

        public JsonResult obtenerDiasDisponibles(string periodo)
        {
            this.Globales.DatosInforme.periodoSeleccionado = int.Parse(periodo);
            
            List<UI_Message> listaMensajes = new List<UI_Message>();
            this.service.FrmInforme_obtenerDias(this.Globales.DatosInforme, listaMensajes);

            return Json(new { listaMensajes = listaMensajes, listaDias = this.Globales.DatosInforme.dias });
        }

        //public ActionResult obtenerDiasDisponibles(ConsultaInterfazCDRViewModel modelo)
        //{
        //    this.Globales.DatosInforme.periodoSeleccionado = (int)(modelo.listaPeriodos.SelectedValue ?? -1);

        //    List<UI_Message> listaMensajes = new List<UI_Message>();
        //    this.service.FrmInforme_obtenerDias(this.Globales.DatosInforme, listaMensajes);

        //    ConsultaInterfazCDRViewModel o = new ConsultaInterfazCDRViewModel(this.Globales.DatosInforme.esCartera, this.Globales.DatosInforme.periodos, this.Globales.DatosInforme.dias);

        //    return View("ConsultaInterfazCDR", o);
        //}

        [HttpPost, FileDownload]
        public ActionResult DescargarConsultaInterfaz(string listaPeriodos, string listaDias, string rut, string Command)
        {
            if (Command == "Volver")
            {
                return new RedirectResult("~/Devengo");
            }
            else
            {
                List<UI_Message> listaMensajes = new List<UI_Message>();
                ConsultaInterfazCDRViewModel model = new ConsultaInterfazCDRViewModel();
                this.Globales.ListaMensajesError.Clear();
                this.Globales.DatosInforme.periodoSeleccionado = (int)(listaPeriodos == string.Empty ? -1 : int.Parse(listaPeriodos)); //model.listaPeriodos.SelectedValue ?? -1;
                this.Globales.DatosInforme.diaSeleccionado = (int)(listaDias == string.Empty ? -1 : int.Parse(listaDias));//model.listaDias.SelectedValue ?? -1;
                this.Globales.DatosInforme.txtRut = (rut ?? "").Replace(".", "").Replace("-", "");
                if (!string.IsNullOrEmpty(this.Globales.DatosInforme.txtRut))
                    this.Globales.DatosInforme.txtRut = this.Globales.DatosInforme.txtRut.PadLeft(10, '0');

                if(this.service.FrmInforme_buscar(this.Globales.DatosInforme, listaMensajes))
                {
                    if (listaMensajes.Count == 0)
                    {
                        if (this.Globales.DatosInforme.tipoConsulta == 1)
                        {
                            if (this.Globales.DatosInforme.CDR_Cartera.Count > 0)
                            {
                                MemoryStream msFile = this.service.FrmInforme_GetFileCartera(this.Globales.DatosInforme.CDR_Cartera);
                                msFile.Position = 0;
                                var fName = string.Format("CarteraCDR-{0}.xlsx", System.DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                                Session[fName] = msFile;
                                return Json(new { success = true, fName }, JsonRequestBehavior.AllowGet);

                            }
                            else
                            {
                                listaMensajes.Add(new UI_Message() { Text = "No existe información para la consulta realizada.", Type = TipoMensaje.Informacion });
                            }
                        }
                        else
                        {
                            if (this.Globales.DatosInforme.CDR_Deveng.Count > 0)
                            {
                                MemoryStream msFile = this.service.FrmInforme_GetFileDevengo(this.Globales.DatosInforme.CDR_Deveng);
                                msFile.Position = 0;
                                var fName = string.Format("DevengosCDR-{0}.xlsx", System.DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                                Session[fName] = msFile;
                                return Json(new { success = true, fName }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                listaMensajes.Add(new UI_Message() { Text = "No existe información para la consulta realizada.", Type = TipoMensaje.Informacion });
                            }
                        }
                    }
                }                
                this.Globales.ListaMensajesError = listaMensajes;
                ConsultaInterfazCDRViewModel o = new ConsultaInterfazCDRViewModel(this.Globales.DatosInforme.esCartera, this.Globales.DatosInforme.periodos, this.Globales.DatosInforme.dias);
                o.update(listaMensajes);
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