using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.UI.Web.Areas.Supervisor.Models;
using BCH.Comex.UI.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using MvcRazorToPdf;
using System.Drawing;
using System.Drawing.Imaging;
using BCH.Comex.UI.Web.Helpers;
using BCH.Comex.UI.Web.Helpers.Extensions;
using BCH.Comex.Core.Entities.Portal;

namespace BCH.Comex.UI.Web.Areas.Supervisor.Controllers
{
    [SupervisorExigirInicioDia]
    public class DocumentosValoradosController : BaseController
    {
        //
        // GET: /Supervisor/DocumentosValorados/
        public ActionResult Index()
        {
            using (Tracer tracer = new Tracer())
            {
                LimpiarCacheCheques();

                if (this.Globales.FrmgChq == null)
                    this.Globales.FrmgChq = new FrmgChqDTO();

                service.DocumentosValoradosInit(this.Globales);
                DocumentosValoradosViewModel dvvm = new DocumentosValoradosViewModel(this.Globales);
                return View(dvvm);
            }
        }
        [HttpPost]
        public ActionResult Index(DocumentosValoradosViewModel dvvm, string command)
        {
            using (Tracer tracer = new Tracer())
            {
                if (command == "Cerrar")
                    return new RedirectResult("~/Supervisor");

                return View();
            }
        }

        [HttpGet, HandleError]
        public JsonResult DocumentosValorados_Buscar(int opDocumento, int opPlazaPago, string fechaEmision, bool todos)
        {
            using (Tracer tracer = new Tracer())
            {
                LimpiarCacheCheques();

                object data = null;
                switch (opDocumento)
                {
                    case 0:
                        List<T_Chq> cheques = service.DocumentosValorados_ObtenerCheques(opPlazaPago, fechaEmision, todos, this.Globales);
                        GuardarChequesBuscadosEnCache(cheques);
                        data = cheques;
                        break;
                    case 1:
                        data = service.DocumentosValorados_ObtenerValeVista(opPlazaPago, this.Globales);
                        break;
                }

                return new JsonResult()
                {
                    Data = data,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        public ActionResult ImpresionCheque(IList<T_Chq> cheques, int nroFolio, string filename)
        {
            //var datosUsuario = HttpContext.GetCurrentUser().GetDatosUsuario();
            //PrintFormat format =  datosUsuario.ConfigImpres_PrintFormat;
            PrintFormat format = PrintFormat.PDF;
            using (Tracer tracer = new Tracer("Impresion de Cheque"))
            {
                foreach (T_Chq cheque in cheques)
                {
                    cheque.NroFol = nroFolio;
                    service.PrepararChequeParaImprimir(cheque, this.Globales);
                }
                return this.AlternateOutputCheque(format, "ImpresionCheque", cheques, filename)
                        ?? View("ImpresionCheque", cheques);
            }
        }

        public ActionResult Cheque(string idsCheques, int nroFolio)
        {
            //var datosUsuario = HttpContext.GetCurrentUser().GetDatosUsuario();
            //PrintFormat format = datosUsuario.ConfigImpres_PrintFormat;
            PrintFormat format = PrintFormat.PDF;
            DocumentosValoradosViewModel model = new DocumentosValoradosViewModel(this.Globales);
            string filename = string.Empty;
            List<int> ids = null;
            List<ActionResult> AcctionResultados = new List<ActionResult>();

            if (!String.IsNullOrEmpty(idsCheques))
            {
                try
                {
                    ids = idsCheques.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
                }
                catch (Exception)
                {
                    throw new ArgumentNullException("Los ids de los cheques a imprimir no son válidos");
                }
                IList<T_Chq> cheques = GetUltimosChequesBuscadosDeCache();



                if (ids.Count > 0)
                {
                    filename = string.Format("Cheque_{0}", cheques[ids[0]].codcct + cheques[ids[0]].codpro + cheques[ids[0]].codesp + cheques[ids[0]].CodEmp + cheques[ids[0]].codope);
                }

                foreach (var item in ids)
                {
                    // Se revisa que todos los cheques a imprimir sean de la misma operacion
                    if (filename != string.Format("Cheque_{0}", cheques[item].codcct + cheques[item].codpro + cheques[item].codesp + cheques[item].CodEmp + cheques[item].codope))
                    {
                        //si no lo son, se cambia el nombre del archivo por Cheques_yyyyMMdd_HHmmss
                        filename = string.Format("Cheques_{0}", System.DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                        break;
                    }
                }


                for (int i = 0; i < ids.Count; i++)
                {
                    AcctionResultados.Add(GenerarCheque(cheques.Where(c => c.indice == ids[i]).ToList(), nroFolio, filename));
                    //AcctionResultados.Add(GenerarCheque(cheques, nroFolio, filename));
                    //cheques = GetUltimosChequesBuscadosDeCache();
                    nroFolio++;
                }


                if (format == PrintFormat.PDF)
                {
                    PdfDocument outputDocument = CombinePdfs(AcctionResultados.Cast<PdfActionResult>().Select(i => i.PdfDocument));
                    using (MemoryStream stream = new MemoryStream())
                    {
                        Response.Clear();
                        outputDocument.Save(stream, false);
                        outputDocument.Close();
                        if (string.IsNullOrEmpty(filename))
                            return File(stream.ToArray(), "application/pdf");
                        else
                            return File(stream.ToArray(), "application/pdf", filename + ".pdf");
                    }
                }
                else if (format == PrintFormat.TIFF)
                {
                    var items = AcctionResultados.Cast<TiffActionResult>().Select(i => new Bitmap(i.Image)).ToList();
                    Bitmap outputDocument = CombineImages(items);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        stream.Position = 0;
                        Response.Clear();
                        outputDocument.Save(stream, ImageFormat.Tiff);
                        Session[filename] = stream.ToArray();
                        return Json(new { success = true, filename }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (format == PrintFormat.HTML)
                {
                    return View("ImpresionCheque", cheques);
                }
                else
                    throw new ArgumentNullException("No es posible imprimir el formato");
            }
            throw new ArgumentNullException("Los ids de los cheques a imprimir no son válidos");
        }

        private ActionResult GenerarCheque(IList<T_Chq> cheques, int nroFolio, string filename)
        {
            var action = this.ImpresionCheque(cheques, nroFolio, filename);
            action.ExecuteResult(ControllerContext);
            return action;
        }

        private void GuardarChequesBuscadosEnCache(IList<T_Chq> resultadoBusqueda)
        {
            this.ControllerContext.HttpContext.Session[SessionKeys.Supervisor.UltimosChequesBuscadosKey] = resultadoBusqueda;
        }

        private IList<T_Chq> GetUltimosChequesBuscadosDeCache()
        {
            if (this.ControllerContext.HttpContext.Session[SessionKeys.Supervisor.UltimosChequesBuscadosKey] != null)
            {
                return this.ControllerContext.HttpContext.Session[SessionKeys.Supervisor.UltimosChequesBuscadosKey] as IList<T_Chq>;
            }
            else return null;
        }

        private void LimpiarCacheCheques()
        {
            if (this.ControllerContext.HttpContext.Session[SessionKeys.Supervisor.UltimosChequesBuscadosKey] != null)
            {
                this.ControllerContext.HttpContext.Session.Remove(SessionKeys.Supervisor.UltimosChequesBuscadosKey);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pdfs"></param>
        /// <returns></returns>
        private static PdfDocument CombinePdfs(IEnumerable<PdfDocument> pdfs)
        {
            var outputDocument = new PdfDocument();
            foreach (var pdf in pdfs)
            {
                PdfDocument importPdf = null;
                using (MemoryStream stream = new MemoryStream())
                {
                    pdf.Save(stream, false);
                    pdf.Close();
                    stream.Seek(0, SeekOrigin.Begin);
                    importPdf = PdfReader.Open(stream, PdfDocumentOpenMode.Import);
                    int count = importPdf.PageCount;
                    for (int idx = 0; idx < count; idx++)
                    {
                        PdfPage page = importPdf.Pages[idx];
                        outputDocument.AddPage(page);
                    }
                }
            }

            return outputDocument;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private Bitmap CombineImages(IEnumerable<Bitmap> images)
        {
            var enumerable = images as IList<Bitmap> ?? images.ToList();

            var width = 0;
            var height = 0;

            foreach (var image in enumerable)
            {
                height += image.Height;
                width = image.Width > width
                    ? image.Width
                    : width;
            }

            var bitmap = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bitmap))
            {
                var localHeight = 0;
                foreach (var image in enumerable)
                {
                    g.DrawImage(image, 0, localHeight);
                    localHeight += image.Height;
                }
            }
            return bitmap;
        }
    }
}