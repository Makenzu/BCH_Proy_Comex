using PdfSharp.Pdf;
using System;
using System.Web.Mvc;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace MvcRazorToPdf
{
    public static class MvcRazorToPdfExtensions
    {
        public static byte[] GeneratePdf(this ControllerContext context, object model=null, string viewName=null,
            Action<PdfGenerateConfig> configureSettings=null)
        {
            return new MvcRazorToPdf().GeneratePdfOutput(context, model, viewName, configureSettings);
        }

        public static PdfDocument GeneratePdf2(this ControllerContext context, object model = null, string viewName = null,
            Action<PdfGenerateConfig> configureSettings = null)
        {
            return new MvcRazorToPdf().GeneratePdfOutput2(context, model, viewName, configureSettings);
        }
    }
}