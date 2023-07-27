using BCH.Comex.Core.BL.Common;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.UI.Web.Areas.Impresion.Models;
using BCH.Comex.UI.Web.Common;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.UI.Web.Helpers;

namespace BCH.Comex.UI.Web.Areas.Impresion.Controllers
{
    public class ImpresionDocumentoDevengController : BaseController
    {

        private CommonService commonService;
        
        //
        // GET: /Impresion/ImpresionDocumentoDeveng/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        #region IMPRESION DE DOCUMENTOS


        public ActionResult ImpresionDeDocumentos()
        {
            ImpresionDocumentosViewModel model = new ImpresionDocumentosViewModel();
            return View(model);
        }

        [HandleAjaxException]
        public ActionResult BuscarDocumentosOperaciones(DateTime? fechaOperacion)
        {
            CommonService service = new CommonService();
            IList<DocumentoOperacion> documentos = null;
            if (fechaOperacion.HasValue)
            {
                documentos = service.BuscarDocumentosDevengoPorFecha(fechaOperacion.Value);
            }
            else
            {
                throw new ArgumentNullException("No se envió una combinación válida de parámetros");
            }

            LimpiarCacheImpresionDeDocumentos(); //si busco de nuevo puede ser que se haya ido a otra pagina por ej a anular un doc, asi q limpio el cache

            if (documentos != null)
            {
                var jsonResult = new JsonResult()
                {
                    Data = documentos,
                    MaxJsonLength = Int32.MaxValue,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                return jsonResult;
            }
            else return null;
        }



        private void LimpiarCacheImpresionDeDocumentos()
        {
            if (this.ControllerContext.HttpContext.Session[SessionKeys.Impresion.ImpresionDocumentosDetalleUltimoSwiftKey] != null)
            {
                this.ControllerContext.HttpContext.Session.Remove(SessionKeys.Impresion.ImpresionDocumentosDetalleUltimoSwiftKey);
            }

            if (this.ControllerContext.HttpContext.Session[SessionKeys.Impresion.ImpresionDocumentosReporteContableKey] != null)
            {
                this.ControllerContext.HttpContext.Session.Remove(SessionKeys.Impresion.ImpresionDocumentosReporteContableKey);
            }
        }

        public ActionResult GetDetalleSwift(string nroMensaje, int nroReporte, DateTime fechaOp, bool? generarHtmlCompleto, bool? imprimir, PrintFormat? format = null)
        {
            //ViewBag.DetalleSwift = GetMensajeSwiftDeCacheOBL(nroMensaje);
            //ViewBag.GenerarHtmlCompleto = (generarHtmlCompleto.HasValue && generarHtmlCompleto.Value);
            //ViewBag.Imprimir = (imprimir.HasValue && imprimir.Value);

            //return View("DetalleSwift");
            //return RedirectToAction("GetDetalleSwift", "ImpresionDeDocumentos", new { area = "Impresion", nroMensaje, nroReporte, fechaOp, generarHtmlCompleto, imprimir, format });
            string url = "~/Impresion/ImpresionDeDocumentos/GetDetalleSwift" + (Request.Url.Query ?? string.Empty);
            return new TransferResult(url);
        }

        public ActionResult DetalleSwiftOriginalCopia(string nroMensaje, string replace, string with, PrintFormat? format = null)
        {
            //string detSwift = GetMensajeSwiftDeCacheOBL(nroMensaje);
            //detSwift = detSwift.Replace(replace, with);
            //ViewBag.DetalleSwift = detSwift;
            //ViewBag.GenerarHtmlCompleto = true;
            //ViewBag.Imprimir = false;

            //return View("DetalleSwift");
            //return RedirectToAction("DetalleSwiftOriginalCopia", "ImpresionDeDocumentos", new { area = "Impresion", nroMensaje, replace, with, format });
            string url = "~/Impresion/ImpresionDeDocumentos/DetalleSwiftOriginalCopia" + (Request.Url.Query ?? string.Empty);
            return new TransferResult(url);
        }

        public ActionResult ReporteContable(int nroReporte, DateTime fechaOp, bool? generarHtmlCompleto, bool? imprimir, PrintFormat? format = null)
        {
            //ReporteContableViewModel model = new ReporteContableViewModel();
            //model.Reporte = GetReporteContableDeCacheOBL(nroReporte, fechaOp);
            //model.GenerarHtmlCompleto = (generarHtmlCompleto.HasValue && generarHtmlCompleto.Value);
            //model.Imprimir = (imprimir.HasValue && imprimir.Value);

            //return View(model);
            //return RedirectToAction("ReporteContable", "ImpresionDeDocumentos", new { area = "Impresion", nroReporte, fechaOp, generarHtmlCompleto, imprimir, format });
            string url = "~/Impresion/ImpresionDeDocumentos/ReporteContable" + (Request.Url.Query ?? string.Empty);
            return new TransferResult(url);
        }

        private IList<sce_dfc> GetDescripcionDeFuncionesContablesDeCacheOBL()
        {
            IList<sce_dfc> result = null;
            if (this.ControllerContext.HttpContext.Cache[SessionKeys.Impresion.DescripcionFuncionesContablesKey] == null)
            {
                result = commonService.GetDescripcionesFuncionesContables();
                this.ControllerContext.HttpContext.Cache[SessionKeys.Impresion.DescripcionFuncionesContablesKey] = result;
            }
            else
            {
                result = this.ControllerContext.HttpContext.Cache[SessionKeys.Impresion.DescripcionFuncionesContablesKey] as IList<sce_dfc>;
            }

            return result;
        }

        private ReporteContable GetReporteContableDeCacheOBL(int nroReporte, DateTime fechaOp)
        {
            //el reporte contable lo guardo en el cache ya que posiblemente lo puedo ver primero en el popup y luego al darle imprimir lo abro de vuelta
            //en una pagina limpia
            if (commonService == null) { commonService = new CommonService(); }
            ReporteContable reporte = null;
            if (this.ControllerContext.HttpContext.Session[SessionKeys.Impresion.ImpresionDocumentosReporteContableKey] != null)
            {
                reporte = this.ControllerContext.HttpContext.Session[SessionKeys.Impresion.ImpresionDocumentosReporteContableKey] as ReporteContable;
                if (reporte != null && reporte.Cabecera.nrorpt == nroReporte)
                {
                    return reporte;
                }
            }

            IList<sce_dfc> descripciones = GetDescripcionDeFuncionesContablesDeCacheOBL();
            reporte = commonService.GetReporteContable(nroReporte, fechaOp, descripciones);
            this.ControllerContext.HttpContext.Session[SessionKeys.Impresion.ImpresionDocumentosReporteContableKey] = reporte;

            return reporte;
        }

        private string GetMensajeSwiftDeCacheOBL(string nroMensaje)
        {
            commonService = new CommonService();

            //el mensaje lo guardo en el cache ya que posiblemente lo puedo ver primero en el popup y luego al darle imprimir lo abro de vuelta
            //en una pagina limpia
            string swift = null;
            if (this.ControllerContext.HttpContext.Session[SessionKeys.Impresion.ImpresionDocumentosDetalleUltimoSwiftKey] != null)
            {
                swift = this.ControllerContext.HttpContext.Session[SessionKeys.Impresion.ImpresionDocumentosDetalleUltimoSwiftKey] as string;
                if (swift != null &&
                    this.ControllerContext.HttpContext.Session[SessionKeys.Impresion.ImpresionDocumentosNroMensajeUltimoSwiftKey] == nroMensaje)
                {
                    return swift;
                }
            }

            swift = commonService.GetDetalleMensajeSwift(nroMensaje);
            this.ControllerContext.HttpContext.Session[SessionKeys.Impresion.ImpresionDocumentosDetalleUltimoSwiftKey] = swift;
            this.ControllerContext.HttpContext.Session[SessionKeys.Impresion.ImpresionDocumentosNroMensajeUltimoSwiftKey] = nroMensaje;

            return swift;
        }

       

        #endregion
	}
}