using BCH.Comex.Core.BL.Portal.Users;
using BCH.Comex.Core.Entities.Portal;
using MvcRazorToPdf;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BCH.Comex.UI.Web.Helpers;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace BCH.Comex.UI.Web.Helpers.Extensions
{
    public enum ReportRequestType
    {
        Carta,
        Swift,
        SwiftOriginalCopia,
        Contabilidad,
        PlanillaInvisibleExportacion,
        PlanillaReemplazos,
        PlanillaAnulada,
        PlanillaVisibleExportacion,
        SwiftBaseSwift,
    }

    public class ReportRequest
    {
        public ReportRequestType type { get; set; }
        public ReportRequestCarta carta { get; set; }
        public ReportRequestSwift swift { get; set; }
        public ReportRequestSwiftOriginalCopia swiftOriginalCopia { get; set; }
        public ReportRequestContabilidad contabilidad { get; set; }
        public Planilla planillaInvisibleExportacion { get; set; }
        public Planilla planillaReemplazos { get; set; }
        public Planilla planillaAnulada { get; set; }
        public Planilla planillaVisibleExportacion { get; set; }
        public ReportRquestSwiftBaseSwift BaseSwift { get; set; }
    }

    public class ReportRequestCarta
    {
        public string numeroOperacion { get; set; }
        public int codDocumento { get; set; }
        public int nroCorrelativo { get; set; }
    }

    public class ReportRequestContabilidad
    {
        public int nroReporte { get; set; }
        public DateTime fechaOp { get; set; }
    }

    public class ReportRequestSwift
    {
        public string nroMensaje { get; set; }
        public int nroReporte { get; set; }
        public DateTime fechaOp { get; set; }
    }

    public class ReportRquestSwiftBaseSwift
    {
        public virtual int sesion { get; set; }
        public virtual int secuencia { get; set; } 
        public virtual int idMensaje { get; set; }
    }

    public class ReportRequestSwiftOriginalCopia
    {
        public string nroMensaje { get; set; }
        public string replace { get; set; }
        public string with { get; set; }
    }

    public class Planilla
    {
        public string nroPresentacion { get; set; }
        public DateTime fechaPresentacion { get; set; }
        public string centroCosto { get; set; }
        public string producto { get; set; }
        public string especialista { get; set; }
        public string empresa { get; set; }
        public string cobranza { get; set; }
    }

    public static class ControllerExtensions
    {
        public readonly static string PrintFormatViewDataKey = "PrintFormat";
        public static PrintFormat GetFinalFormat(this Controller me, PrintFormat? format)
        {
            return format ?? HttpContext.Current.GetCurrentUser().GetDatosUsuario().ConfigImpres_PrintFormat;
        }
        public static ActionResult AlternateOutput(this Controller me, PrintFormat? format, string view, object model = null, string filename = null, PdfGenerateConfig pdfGenerateConfig = null)
        {
            format = GetFinalFormat(me, format);
            me.ViewData[PrintFormatViewDataKey] = format;
            switch (format)
            {
                case PrintFormat.PDF:
                    return new PdfActionResult(view, model, i =>
                    {
                        /// Si no enviamos configuración de pagina, toma el por defecto
                        if (pdfGenerateConfig == null)
                            i.PageSize = PageSize.A3;
                        else
                        {
                            i.ManualPageSize = pdfGenerateConfig.ManualPageSize;
                            i.MarginBottom = pdfGenerateConfig.MarginBottom;
                            i.MarginLeft = pdfGenerateConfig.MarginLeft;
                            i.MarginRight = pdfGenerateConfig.MarginRight;
                            i.MarginTop = pdfGenerateConfig.MarginTop;
                            i.PageOrientation = pdfGenerateConfig.PageOrientation;
                            i.PageSize = pdfGenerateConfig.PageSize;
                        }
                    }) { FileDownloadName = filename == null ? null : filename + ".pdf" };
                case PrintFormat.TIFF:
                    return new TiffActionResult(view, model, i =>
                    {
                        i.PageSize = PageSize.A3;
                    }) { FileDownloadName = filename == null ? null : filename + ".tiff" };
                default:
                    return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="me"></param>
        /// <param name="format"></param>
        /// <param name="view"></param>
        /// <param name="model"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static ActionResult AlternateOutputCheque(this Controller me, PrintFormat? format, string view, object model = null, string filename = null)
        {
            format = GetFinalFormat(me, format);
            switch (format)
            {
                case PrintFormat.PDF:
                    return new PdfActionResult(view, model, i =>
                    {
                        i.ManualPageSize = new XSize(700, 1058); i.SetMargins(0);
                    }) { FileDownloadName = filename == null ? null : filename + ".pdf" };
                case PrintFormat.TIFF:
                    return new TiffActionResult(view, model, i =>
                    {
                        i.ManualPageSize = new XSize(700, 1058); i.SetMargins(0); i.PageSize = PageSize.Letter;
                    }) { FileDownloadName = filename == null ? null : filename + ".tiff" };
                case PrintFormat.HTML:
                    return null;
                default:
                    return null;
            }
        }
    }
}
