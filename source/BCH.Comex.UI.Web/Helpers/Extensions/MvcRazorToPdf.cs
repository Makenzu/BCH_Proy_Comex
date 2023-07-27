using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace MvcRazorToPdf
{
    public class MvcRazorToPdf
    {
        public byte[] GeneratePdfOutput(ControllerContext context, object model = null, string viewName = null,
            Action<PdfGenerateConfig> configureSettings = null)
        {
            if (viewName == null)
            {
                viewName = context.RouteData.GetRequiredString("action");
            }

            context.Controller.ViewData.Model = model;

            byte[] output;
            PdfGenerateConfig config = new PdfGenerateConfig();
            if (configureSettings != null)
            {
                configureSettings(config);
            }



            using (MemoryStream stream = new MemoryStream())
            {
                var pdf = PdfGenerator.GeneratePdf(RenderRazorView(context, viewName), config); //PdfGenerator.GeneratePdf(RenderRazorView(context, viewName), config);

                pdf = ResizeToA4(pdf);

                pdf.Save(stream, false);
                pdf.Close();
                output = stream.ToArray();
            }


            return output;
        }

        public PdfDocument GeneratePdfOutput2(ControllerContext context, object model = null, string viewName = null,
            Action<PdfGenerateConfig> configureSettings = null)
        {
            if (viewName == null)
            {
                viewName = context.RouteData.GetRequiredString("action");
            }

            context.Controller.ViewData.Model = model;

            PdfGenerateConfig config = new PdfGenerateConfig();
            if (configureSettings != null)
            {
                configureSettings(config);
            }

            var pdf = PdfGenerator.GeneratePdf(RenderRazorView(context, viewName), config);
            pdf = ResizeToA4(pdf);
            pdf.Close();
            return pdf;
        }

        private PdfDocument ResizeToA4(PdfDocument doc)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                doc.Save(stream, false);
                stream.Seek(0, SeekOrigin.Begin);

                PdfDocument output = new PdfDocument();
                XRect box;
                
                // Open the external document as XPdfForm object
                XPdfForm form = XPdfForm.FromStream(stream);

                for (int idx = 0; idx < form.PageCount; idx++)
                {
                    // Add a new page to the output document
                    PdfPage page = output.AddPage();
                    page.Size = PageSize.A4;
                    page.TrimMargins = new TrimMargins();

                    page.TrimMargins.Top = new XUnit(idx == 0 ? 0 : 50, XGraphicsUnit.Point);
                    page.TrimMargins.Bottom = new XUnit(form.PageCount > 1 ? 50 : 0, XGraphicsUnit.Point);
                    double width = page.Width;
                    double height = page.Height;

                    // Set page number (which is one-based)
                    form.PageNumber = idx + 1;
                    box = new XRect(0, 0, width, height);

                    // Draw the page identified by the page number like an image
                    using (var gfx = XGraphics.FromPdfPage(page))
                    {                                                
                        gfx.DrawImage(form, box);
                    }
                }
                return output;
            }
        }

        public string RenderRazorView(ControllerContext context, string viewName)
        {
            IView viewEngineResult = ViewEngines.Engines.FindView(context, viewName, null).View;
            var sb = new StringBuilder();

            using (TextWriter tr = new StringWriter(sb))
            {
                var viewContext = new ViewContext(context, viewEngineResult, context.Controller.ViewData,
                    context.Controller.TempData, tr);
                viewEngineResult.Render(viewContext, tr);
            }
            return sb.ToString();
        }        
    }
}