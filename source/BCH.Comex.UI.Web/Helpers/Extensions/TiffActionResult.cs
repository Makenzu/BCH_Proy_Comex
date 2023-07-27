using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace MvcRazorToPdf
{
    public class TiffActionResult : ActionResult
    {
        public TiffActionResult(string viewName, object model)
        {
            ViewName = viewName;
            Model = model;
        }

        public TiffActionResult(object model)
        {
            Model = model;
        }

        public TiffActionResult(object model, Action<PdfGenerateConfig> configureSettings)
        {
            if (configureSettings == null) throw new ArgumentNullException("configureSettings");
            Model = model;
            ConfigureSettings = configureSettings;
        }

        public TiffActionResult(string viewName, object model, Action<PdfGenerateConfig> configureSettings)
        {
            if (configureSettings == null) throw new ArgumentNullException("configureSettings");
            ViewName = viewName;
            Model = model;
            ConfigureSettings = configureSettings;
        }

        public string ViewName { get; set; }
        public object Model { get; set; }
        public Action<PdfGenerateConfig> ConfigureSettings { get; set; }

        public string FileDownloadName { get; set; }

        public Image Image { get; set; }


        public override void ExecuteResult(ControllerContext context)
        {
            IView viewEngineResult;
            ViewContext viewContext;

            if (ViewName == null)
            {
                ViewName = context.RouteData.GetRequiredString("action");
            }

            context.Controller.ViewData.Model = Model;


            if (context.HttpContext.Request.QueryString["html"] != null &&
                context.HttpContext.Request.QueryString["html"].ToLower().Equals("true"))
            {
                RenderHtmlOutput(context);
            }
            else
            {
                if (!String.IsNullOrEmpty(FileDownloadName))
                {
                    context.HttpContext.Response.AddHeader("content-disposition",
                        "attachment; filename=" + FileDownloadName);
                }

                Image = context.GenerateTiff2(Model, ViewName, ConfigureSettings);

                using (MemoryStream stream = new MemoryStream())
                {
                    Image.Save(stream, ImageFormat.Tiff );

                    new FileContentResult(stream.ToArray(), "image/tiff")
                    .ExecuteResult(context);
                }
            }
        }

        private void RenderHtmlOutput(ControllerContext context)
        {
            IView viewEngineResult = ViewEngines.Engines.FindView(context, ViewName, null).View;
            var viewContext = new ViewContext(context, viewEngineResult, context.Controller.ViewData,
                context.Controller.TempData, context.HttpContext.Response.Output);
            viewEngineResult.Render(viewContext, context.HttpContext.Response.Output);
        }
    }
}