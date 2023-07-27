using BCH.Comex.Core.Entities.Cext01.Devengo;
using BCH.Comex.UI.Web.Common;
using System.IO;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Devengo.Controllers
{
    public class DiferenciaDevengamientoController : BaseController
    {
        //
        // GET: /Devengo/DiferenciaDevengamiento/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DescargaDiferenciaDevengamiento()
        {
            if (this.Globales != null)
            {
                this.Globales.ListaMensajesError.Clear();
                if (this.Globales.MODDIFDEV == null)
                {
                    this.Globales.MODDIFDEV = new T_MODDIFDEV();
                }
                this.service.FrmDifDevInit(this.Globales);
                MemoryStream msFile = this.service.FrmDifDevFile(this.Globales.MODDIFDEV.DifDev);
                msFile.Position = 0;
                var fName = string.Format("DIFERENC-{0}.xlsx", System.DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                Session[fName] = msFile;
                return Json(new { success = true, fName }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
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