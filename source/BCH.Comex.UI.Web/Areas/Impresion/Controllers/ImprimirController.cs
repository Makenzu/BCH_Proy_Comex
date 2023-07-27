using BCH.Comex.Core.BL.XEGI;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.UI.Web.Controllers;
using BCH.Comex.UI.Web.Helpers;
using BCH.Comex.UI.Web.Helpers.Extensions;
using MvcRazorToPdf;
using Newtonsoft.Json;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BCH.Comex.UI.Web.Common;
using System.Drawing;
using System.Drawing.Imaging;
using BCH.Comex.Common.Tracing;
using BCH.Comex.UI.Web.Areas.Planillas.Controllers;

namespace BCH.Comex.UI.Web.Areas.Impresion.Controllers
{
    public class ImprimirController : Controller
    {
        private readonly Dictionary<int, string> dicTemplates = new Dictionary<int, string>{
            {402, "VentasVisiblesImport_402"},
            {601, "RegistroCobranza_601"},
            {602, "AceptacionLetras_602"},
            {610, "PagoDirectoCobranza_610"},
            {611, "CancelacionRetornoExport_611"},
            {612, "RegistroRetorno_612"},
            {613, "PlanillaVisibleExport_613"},
            {614, "RegistroCancelacionRetorno_614"},
            {620, "CompraVenta_620"},
            {621, "Arbitraje_621"},
            {998, "AvisoDebito_998"},
            {999, "AvisoCredito_999"}
        };

        // GET: Impresion/Imprimir
        public ActionResult Index(string numeroOperacion, int codDocumento, int nroCorrelativo, PrintFormat? format = null, string filename = null)
        {
            if (!dicTemplates.ContainsKey(codDocumento)) return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "El código de documento no existe en el servidor de reportes.");
            bool primero = false;
            bool segundo = false;
            bool tercero = true;
            bool cuarto = false;
            var service = new XegiService();
            service.ProcesaComando(numeroOperacion, codDocumento, nroCorrelativo, primero, segundo, tercero, cuarto);

            string viewName = this.dicTemplates[codDocumento];
            return this.AlternateOutput(format, viewName, service.Documento, filename)
                ?? View(viewName, service.Documento);
        }



        [FileDownload]
        public ActionResult Multiple(string request, PrintFormat? format = null, string filename = null)
        {
            using (var tracer = new Tracer("ImpresionMultiple"))
            {
                try
                {
                    format = this.GetFinalFormat(format);
                    if (format == PrintFormat.HTML)
                        throw new ArgumentException("Format HTML is not supported", "format");

                    var reports = JsonConvert.DeserializeObject<ReportRequest[]>(request).ToList();

                    var actionResults = new List<ActionResult>();

                    foreach (var report in reports)
                    {
                        ActionResult result = null;
                        switch (report.type)
                        {
                            case ReportRequestType.Carta:
                                result = GenerateCarta(format, report.carta);
                                break;
                            case ReportRequestType.Contabilidad:
                                result = GenerateReporteContable(format, report.contabilidad);
                                break;
                            case ReportRequestType.PlanillaAnulada:
                                result = GeneratePlanillasAnuladas(format, report.planillaAnulada);
                                break;
                            case ReportRequestType.PlanillaInvisibleExportacion:
                                result = GeneratePlanillaInvisibleExport(format, report.planillaInvisibleExportacion);
                                break;
                            case ReportRequestType.PlanillaReemplazos:
                                result = GeneratePlanillasReemplazo(format, report.planillaReemplazos);
                                break;
                            case ReportRequestType.PlanillaVisibleExportacion:
                                result = GeneratePlanillasVisibleExport(format, report.planillaVisibleExportacion);
                                break;
                            case ReportRequestType.Swift:
                                result = GenerateDetalleSwift(format, report.swift);
                                break;
                            case ReportRequestType.SwiftOriginalCopia:
                                result = GenerateDetalleSwiftOriginalCopia(format, report.swiftOriginalCopia);
                                break;
                            case ReportRequestType.SwiftBaseSwift:
                                result = GenerateDetalleSwiftBaseSwift(format, report.BaseSwift);
                                break;
                        }

                        if (result != null)
                        {
                            actionResults.Add(result);
                        }
                    }


                    if (format == PrintFormat.PDF)
                    {
                        PdfDocument outputDocument = CombinePdfs(actionResults.Cast<PdfActionResult>().Select(i => i.PdfDocument));

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
                        var items = actionResults.Cast<TiffActionResult>().Select(i => new Bitmap(i.Image)).ToList();
                        Bitmap outputDocument = CombineImages(items);

                        using (MemoryStream stream = new MemoryStream())
                        {
                            Response.Clear();
                            outputDocument.Save(stream, ImageFormat.Tiff);
                            if (string.IsNullOrEmpty(filename))
                                return File(stream.ToArray(), "image/tiff");
                            else
                                return File(stream.ToArray(), "image/tiff", filename + ".tiff");
                        }
                    }
                    else
                    {
                        throw new ArgumentException(string.Format("Format {0} is not supported", format ?? PrintFormat.HTML), "format");
                    }

                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta en ImpresionMultiple : ", ex);
                    throw;
                }
            }
        }
        private static ActionResult GeneratePlanillasVisibleExport(PrintFormat? format, Planilla item)
        {
            if (item == null)
                return null;

            ControllerContext context = null;
            var routeData = new RouteData();
            routeData.DataTokens.Add("area", "Impresion");
            var controller = ControllerFactory.CreateController<PlanillasController>(out context, routeData);

            var action = controller.ImprimirPlanillaVisibleExportacion(item.nroPresentacion, item.fechaPresentacion, format);
            action.ExecuteResult(context);
            return action;
        }

        private static ActionResult GeneratePlanillasAnuladas(PrintFormat? format, Planilla item)
        {
            if (item == null)
                return null;

            ControllerContext context = null;
            var routeData = new RouteData();
            routeData.DataTokens.Add("area", "Impresion");
            var controller = ControllerFactory.CreateController<PlanillasController>(out context, routeData);

            var action = controller.ImprimirPlanillaAnulada(item.nroPresentacion, item.fechaPresentacion, format);
            action.ExecuteResult(context);
            return action;
        }

        private static ActionResult GeneratePlanillasReemplazo(PrintFormat? format, Planilla item)
        {
            if (item == null)
                return null;

            ControllerContext context = null;
            var routeData = new RouteData();
            routeData.DataTokens.Add("area", "Impresion");
            var controller = ControllerFactory.CreateController<PlanillasController>(out context, routeData);

            var action = controller.ImprimirPlanillaReemplazos(item.centroCosto, item.producto, item.especialista, item.empresa, item.cobranza, int.Parse(item.nroPresentacion), item.fechaPresentacion, format);
            action.ExecuteResult(context);
            return action;
        }

        private static ActionResult GeneratePlanillaInvisibleExport(PrintFormat? format, Planilla item)
        {
            if (item == null)
                return null;

            ControllerContext context = null;
            var routeData = new RouteData();
            routeData.DataTokens.Add("area", "Impresion");
            var controller = ControllerFactory.CreateController<PlanillasController>(out context, routeData);

            var action = controller.ImprimirPlanillaInvisibleExportacion(item.nroPresentacion, item.fechaPresentacion, format);
            action.ExecuteResult(context);
            return action;
        }

        private static ActionResult GenerateDetalleSwiftOriginalCopia(PrintFormat? format, ReportRequestSwiftOriginalCopia item)
        {
            if (item == null)
                return null;

            ControllerContext context = null;
            var routeData = new RouteData();
            routeData.DataTokens.Add("area", "Impresion");
            var controller = ControllerFactory.CreateController<ImpresionDeDocumentosController>(out context, routeData);

            var action = controller.DetalleSwiftOriginalCopia(item.nroMensaje, item.replace, item.with, format);
            action.ExecuteResult(context);
            return action;
        }

        private static ActionResult GenerateDetalleSwiftBaseSwift(PrintFormat? format, ReportRquestSwiftBaseSwift item)
        {
            if (item == null)
                return null;

            ControllerContext context = null;
            var routeData = new RouteData();
            routeData.DataTokens.Add("area", "Impresion");
            var controller = ControllerFactory.CreateController<ConsultaSwiftController>(out context, routeData);

            var action = controller.DetalleSwift(item.sesion, item.secuencia, item.idMensaje, true, true,true,false);
            action.ExecuteResult(context);
            return action;
        }

        private static ActionResult GenerateDetalleSwift(PrintFormat? format, ReportRequestSwift item)
        {
            if (item == null)
                return null;

            ControllerContext context = null;
            var routeData = new RouteData();
            routeData.DataTokens.Add("area", "Impresion");
            var controller = ControllerFactory.CreateController<ImpresionDeDocumentosController>(out context, routeData);

            var action = controller.GetDetalleSwift(item.nroMensaje, item.nroReporte, item.fechaOp, null, null, format);
            action.ExecuteResult(context);
            return action;
        }

        private static ActionResult GenerateReporteContable(PrintFormat? format, ReportRequestContabilidad item)
        {
            if (item == null)
                return null;

            ControllerContext context = null;
            var routeData = new RouteData();
            routeData.DataTokens.Add("area", "Impresion");
            var controller = ControllerFactory.CreateController<ImpresionDeDocumentosController>(out context, routeData);

            var action = controller.ReporteContable(item.nroReporte, item.fechaOp, null, null, format);
            action.ExecuteResult(context);
            return action;
        }

        private ActionResult GenerateCarta(PrintFormat? format, ReportRequestCarta item)
        {
            if (item == null)
                return null;

            var action = this.Index(item.numeroOperacion, item.codDocumento, item.nroCorrelativo, format);
            action.ExecuteResult(ControllerContext);
            return action;
        }

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
        private static PdfDocument CombinePdfs(IEnumerable<PdfDocument> pdfs)
        {
            var outputDocument = new PdfDocument();

            // Iterate files
            foreach (var pdf in pdfs)
            {
                PdfDocument importPdf = null;
                using (MemoryStream stream = new MemoryStream())
                {
                    pdf.Save(stream, false);
                    pdf.Close();
                    stream.Seek(0, SeekOrigin.Begin);
                    importPdf = PdfReader.Open(stream, PdfDocumentOpenMode.Import);

                    // Iterate pages
                    int count = importPdf.PageCount;
                    for (int idx = 0; idx < count; idx++)
                    {
                        // Get the page from the external document...
                        PdfPage page = importPdf.Pages[idx];
                        // ...and add it to the output document.
                        outputDocument.AddPage(page);

                    }
                }
            }

            return outputDocument;
        }

        // GET: Impresion/Imprimir/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Impresion/Imprimir/_402
        public ActionResult _402()
        {
            string numeroOperacion = "753207800003448";
            decimal coddoc = 402;
            decimal nrocor = 2;
            bool primero = false;
            bool segundo = true;
            bool tercero = true;
            bool cuarto = true;

            var service = new XegiService();
            service.ProcesaComando(numeroOperacion, coddoc, nrocor, primero, segundo, tercero, cuarto);
            return View("VentasVisiblesImport_402", service.Documento);
        }

        // GET: Impresion/Imprimir/_601
        public ActionResult _601()
        {
            string numeroOperacion = "729067000007074";
            decimal coddoc = 601;
            decimal nrocor = 1;
            bool primero = false;
            bool segundo = false;
            bool tercero = true;
            bool cuarto = false;

            var service = new XegiService();
            service.ProcesaComando(numeroOperacion, coddoc, nrocor, primero, segundo, tercero, cuarto);
            return View("RegistroCobranza_601", service.Documento);
        }

        // GET: Impresion/Imprimir/_602
        public ActionResult _602()
        {
            string numeroOperacion = "729067000007112";
            decimal coddoc = 602;
            decimal nrocor = 2;
            bool primero = false;
            bool segundo = true;
            bool tercero = true;
            bool cuarto = true;

            var service = new XegiService();
            service.ProcesaComando(numeroOperacion, coddoc, nrocor, primero, segundo, tercero, cuarto);
            return View("AceptacionLetras_602", service.Documento);
        }

        // GET: Impresion/Imprimir/_610
        public ActionResult _610()
        {
            string numeroOperacion = "729069300004460";
            decimal coddoc = 610;
            decimal nrocor = 3;
            bool primero = false;
            bool segundo = true;
            bool tercero = true;
            bool cuarto = true;

            var service = new XegiService();
            service.ProcesaComando(numeroOperacion, coddoc, nrocor, primero, segundo, tercero, cuarto);
            return View("PagoDirectoCobranza_610", service.Documento);
        }

        // GET: Impresion/Imprimir/_611
        public ActionResult _611()
        {
            string numeroOperacion = "729097129008024";
            decimal coddoc = 611;
            decimal nrocor = 9;
            bool primero = false;
            bool segundo = true;
            bool tercero = true;
            bool cuarto = true;

            var service = new XegiService();
            service.ProcesaComando(numeroOperacion, coddoc, nrocor, primero, segundo, tercero, cuarto);
            return View("CancelacionRetornoExport_611", service.Documento);
        }

        // GET: Impresion/Imprimir/_612
        public ActionResult _612()
        {
            string numeroOperacion = "753176800002906";
            decimal coddoc = 612;
            decimal nrocor = 1;
            bool primero = false;
            bool segundo = true;
            bool tercero = true;
            bool cuarto = true;

            var service = new XegiService();
            service.ProcesaComando(numeroOperacion, coddoc, nrocor, primero, segundo, tercero, cuarto);
            return View("RegistroRetorno_612", service.Documento);
        }

        // GET: Impresion/Imprimir/_613
        public ActionResult _613()
        {
            //string numeroOperacion = "714302300000016";
            //string numeroOperacion = "714302200000016";
            string numeroOperacion = "714300800003191";
            decimal coddoc = 613;
            decimal nrocor = 1;
            bool primero = false;
            bool segundo = true;
            bool tercero = true;
            bool cuarto = true;

            var service = new XegiService();
            service.ProcesaComando(numeroOperacion, coddoc, nrocor, primero, segundo, tercero, cuarto);
            return View("PlanillaVisibleExport_613", service.Documento);
        }

        // GET: Impresion/Imprimir/_614
        public ActionResult _614()
        {
            string numeroOperacion = "753177300000079";
            decimal coddoc = 614;
            decimal nrocor = 3;
            bool primero = false;
            bool segundo = true;
            bool tercero = true;
            bool cuarto = true;

            var service = new XegiService();
            service.ProcesaComando(numeroOperacion, coddoc, nrocor, primero, segundo, tercero, cuarto);
            return View("RegistroCancelacionRetorno_614", service.Documento);
        }

        // GET: Impresion/Imprimir/_620
        public ActionResult _620()
        {
            string numeroOperacion = "714300900003748";
            decimal coddoc = 620;
            decimal nrocor = 2;
            bool primero = false;
            bool segundo = true;
            bool tercero = true;
            bool cuarto = true;

            var service = new XegiService();
            service.ProcesaComando(numeroOperacion, coddoc, nrocor, primero, segundo, tercero, cuarto);
            return View("CompraVenta_620", service.Documento);
        }

        // GET: Impresion/Imprimir/_621
        public ActionResult _621()
        {
            string numeroOperacion = "753204500002386";
            decimal coddoc = 621;
            decimal nrocor = 3;
            bool primero = false;
            bool segundo = true;
            bool tercero = true;
            bool cuarto = true;

            var service = new XegiService();
            service.ProcesaComando(numeroOperacion, coddoc, nrocor, primero, segundo, tercero, cuarto);
            return View("Arbitraje_621", service.Documento);
        }

        // GET: Impresion/Imprimir/_998
        public ActionResult _998()
        {
            string numeroOperacion = "729097129008024";
            decimal coddoc = 998;
            decimal nrocor = 6;
            bool primero = false;
            bool segundo = true;
            bool tercero = true;
            bool cuarto = true;

            var service = new XegiService();
            service.ProcesaComando(numeroOperacion, coddoc, nrocor, primero, segundo, tercero, cuarto);
            return View("AvisoDebito_998", service.Documento);
        }

        // GET: Impresion/Imprimir/_999
        public ActionResult _999()
        {
            string numeroOperacion = "729097129008024";
            decimal coddoc = 999;
            decimal nrocor = 8;
            bool primero = false;
            bool segundo = true;
            bool tercero = true;
            bool cuarto = true;

            var service = new XegiService();
            service.ProcesaComando(numeroOperacion, coddoc, nrocor, primero, segundo, tercero, cuarto);
            return View("AvisoCredito_999", service.Documento);
        }

        //XgcvService xgcvService = new XgcvService();

        //// GET: Impresion/Imprimir/PrintXGCV
        //public string PrintXGCV()
        //{
        //    string output = string.Empty;
        //    //string input =  "1	753300700000153	620	3	 1	False	True	True	True";
        //    string input = "1	714200900078861	620	1	 1	False	True	True	True";


        //    output = xgcvService.Print(input);

        //    return output; // Content(output);
        //}

    }
}
