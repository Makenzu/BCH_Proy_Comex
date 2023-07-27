using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Devengo;
using BCH.Comex.UI.Web.Common;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System;

namespace BCH.Comex.UI.Web.Areas.Devengo.Controllers
{
    public class OperacionesCDRController : BaseController
    {
        //
        // GET: /Devengo/OperacionesCDR/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DescargaOperacionesCDR()
        {
            if (this.Globales != null)
            {
                T_CDR cdr = new T_CDR();
                List<UI_Message> listaMensajes = new List<UI_Message>();
                this.service.FrmCDR_btn_Buscar(cdr, listaMensajes);
                this.Globales.ListaMensajesError.Clear();
                if (listaMensajes.Count > 0)
                {
                    this.Globales.ListaMensajesError.Add(new UI_Message() { Text = listaMensajes[0].Text, Type = TipoMensaje.Informacion });
                    return Json(new { success = false, this.Globales.ListaMensajesError }, JsonRequestBehavior.AllowGet);
                }

                MemoryStream msFile = this.service.FrmCDRGetFile(cdr.DvgCredE, listaMensajes);

                if (listaMensajes.Count > 0)
                {
                    this.Globales.ListaMensajesError.Add(new UI_Message() { Text = listaMensajes[0].Text, Type = TipoMensaje.Informacion });
                    return Json(new { success = false, this.Globales.ListaMensajesError }, JsonRequestBehavior.AllowGet);
                }

                msFile.Position = 0;
                var fName = string.Format("OPERACIONESCDR-{0}.xlsx", System.DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                Session[fName] = msFile;
                return Json(new { success = true, fName }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { success = false, this.Globales.ListaMensajesError }, JsonRequestBehavior.AllowGet);
        }

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