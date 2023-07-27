using System;
using System.Drawing;
using System.Web.Mvc;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace MvcRazorToPdf
{
    public static class MvcRazorToTiffExtensions
    {
        public static Image GenerateTiff2(this ControllerContext context, object model=null, string viewName=null,
            Action<PdfGenerateConfig> configureSettings=null)
        {
            return new MvcRazorToTiff().GenerateTiffOutput2(context, model, viewName, configureSettings);
        }
        


    }
}