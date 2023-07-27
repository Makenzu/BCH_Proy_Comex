using PdfSharp;
using PdfSharp.Pdf;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web.Mvc;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace MvcRazorToPdf
{
    public class MvcRazorToTiff
    {
        public Image GenerateTiffOutput2(ControllerContext context, object model = null, string viewName = null,
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

            var xSize = PageSizeConverter.ToSize(config.PageSize);
            
            var img = HtmlRender.RenderToImage(RenderRazorView(context, viewName), (int) xSize.Width);

            return img;
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