using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.Common;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.UI.Web.Areas.Impresion.Models;
using BCH.Comex.UI.Web.Common;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

using BCH.Comex.UI.Web.Helpers;
using BCH.Comex.UI.Web.Helpers.Extensions;
using BCH.Comex.Core.Entities.Portal;

namespace BCH.Comex.UI.Web.Areas.Impresion.Controllers
{
    public class ImpresionDeDocumentosController : BaseController
    {
        private CommonService commonService;
        //
        // GET: /Impresion/ImpresionDeDocumentos/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        
        #region IMPRESION DE DOCUMENTOS
        public ActionResult ImpresionDeDocumentos()
        {
            ImpresionDocumentosViewModel model = new ImpresionDocumentosViewModel();

            int centroCosto = int.Parse(infoUsuario.Identificacion_CentroDeCostosImpersonado);
            string idUsuario = infoUsuario.Identificacion_IdEspecialistaImpersonado;
            ObtenerNroDeOperacionPorDefecto(model, centroCosto, int.Parse(idUsuario));

            return View(model);
        }

        [HandleAjaxException]
        public ActionResult BuscarDocumentosOperaciones(DateTime? fechaOperacion, string codcct, string codpro, string codesp, string codofi, string codope, string contactReference)
        {
            using (var tracer = new Tracer("BuscarDocumentosOperaciones"))
            {
                try
                {
                    CommonService service = new CommonService();
                    IList<DocumentoOperacion> documentos = null;
                    if (fechaOperacion.HasValue)
                    {
                        int centroCosto = int.Parse(infoUsuario.Identificacion_CentroDeCostosImpersonado);// int.Parse(this.InitObject.MODGUSR.UsrEsp.CostoSuper);
                        string idUsuario = infoUsuario.Identificacion_IdEspecialistaImpersonado;// this.InitObject.MODGUSR.UsrEsp.Especialista;

                        documentos = service.BuscarDocumentosOperacionesPorFecha(fechaOperacion.Value, centroCosto.ToString(), idUsuario);
                    }
                    else if (!String.IsNullOrEmpty(codcct) && !String.IsNullOrEmpty(codpro) && !String.IsNullOrEmpty(codesp) && !String.IsNullOrEmpty(codofi) && !String.IsNullOrEmpty(codope))
                    {
                        documentos = service.BuscarDocumentosOperacionesPorNroOperacion(codcct, codpro, codesp, codofi, codope);
                    }
                    else if (!String.IsNullOrEmpty(contactReference))
                    {
                        documentos = service.BuscarDocumentosOperacionesPorContactReference(contactReference);
                    }
                    else
                    {
                        throw new Exception("Debe seleccionar un filtro");
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
                catch (Exception ex)
                {
                    tracer.TraceException("Error obteniendo datos: ", ex);
                    throw;
                }
            }
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

        public ActionResult GetDetalleSwift(string nroMensaje, int nroReporte, DateTime fechaOp, bool? generarHtmlCompleto, bool? imprimir, PrintFormat? format = null, string filename = null)
        {
            commonService = new CommonService();
            sce_mch_s01_MS_Result cabecera = commonService.GetCabeceraReporteDetalleSwift(nroReporte, fechaOp);
            if (cabecera != null)
            {
                ViewBag.Estado = cabecera.estado;
            }

            ViewBag.DetalleSwift = GetMensajeSwiftDeCacheOBL(nroMensaje);
            ViewBag.GenerarHtmlCompleto = (generarHtmlCompleto.HasValue && generarHtmlCompleto.Value);
            ViewBag.Imprimir = (imprimir.HasValue && imprimir.Value);
            //return View("DetalleSwift");
            return this.AlternateOutput(format, "DetalleSwift", null, filename)
                ?? View("DetalleSwift");
        }

        public ActionResult DetalleSwiftOriginalCopia(string nroMensaje, string replace, string with, PrintFormat? format = null, string filename = null)
        {
            string detSwift = GetMensajeSwiftDeCacheOBL(nroMensaje);
            detSwift = detSwift.Replace(replace, with);
            ViewBag.DetalleSwift = detSwift;
            ViewBag.GenerarHtmlCompleto = true;
            ViewBag.Imprimir = false;

            //return View("DetalleSwift");
            return this.AlternateOutput(format, "DetalleSwift", null, filename)
                ?? View("DetalleSwift");
        }

        public ActionResult ReporteContable(int nroReporte, DateTime fechaOp, bool? generarHtmlCompleto, bool? imprimir, PrintFormat? format = null, string filename = null)
        {
            ReporteContableViewModel model = new ReporteContableViewModel();
            model.Reporte = GetReporteContableDeCacheOBL(nroReporte, fechaOp, false);
            model.GenerarHtmlCompleto = (generarHtmlCompleto.HasValue && generarHtmlCompleto.Value);
            model.Imprimir = (imprimir.HasValue && imprimir.Value);

            //return View(model);
            return this.AlternateOutput(format, "ReporteContable", model, filename)
                ?? View(model);
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

        private ReporteContable GetReporteContableDeCacheOBL(int nroReporte, DateTime fechaOp, bool desdeCache)
        {
            //el reporte contable lo guardo en el cache ya que posiblemente lo puedo ver primero en el popup y luego al darle imprimir lo abro de vuelta
            //en una pagina limpia
            commonService = new CommonService();
            ReporteContable reporte = null;
            if (this.ControllerContext.HttpContext.Cache[SessionKeys.Impresion.ImpresionDocumentosReporteContableKey] != null && desdeCache)
            {
                reporte = this.ControllerContext.HttpContext.Cache[SessionKeys.Impresion.ImpresionDocumentosReporteContableKey] as ReporteContable;
                if (reporte != null && reporte.Cabecera.nrorpt == nroReporte)
                {
                    return reporte;
                }
            }

            IList<sce_dfc> descripciones = GetDescripcionDeFuncionesContablesDeCacheOBL();
            reporte = commonService.GetReporteContable(nroReporte, fechaOp, descripciones);
            this.ControllerContext.HttpContext.Cache[SessionKeys.Impresion.ImpresionDocumentosReporteContableKey] = reporte;

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

        private void ObtenerNroDeOperacionPorDefecto(ImpresionDocumentosViewModel model, int centroCosto, int idUsuario)
        {
            model.codcct = centroCosto;
            model.codpro = 10;
            model.codesp = idUsuario;
            model.codofi = "000";
            model.codope = "00000";
        }

        #endregion
	}
}