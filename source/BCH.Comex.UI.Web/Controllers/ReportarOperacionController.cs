using BCH.Comex.Common.Utility;
using BCH.Comex.Core.BL.XCFT;
using BCH.Comex.UI.Web.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Controllers
{
    public class ReportarOperacionController : Controller
    {
        private FundTransferService ftService;

        public ActionResult Index()
        {
            return View();
        }
                
        public ReportarOperacionController()
        {
            ftService = new FundTransferService();
        }

        [HttpPost]
        [FileDownload]
        public FileResult DescargarArchivosOperacion(string numeroOperacion, string descripcionProblema, string fechaLogs)
        {
            // Archivos a incluir en el zip
            Dictionary<string, MemoryStream> archivos = new Dictionary<string, MemoryStream>();
            byte[] datosOperacionZip = null;

            if (!string.IsNullOrWhiteSpace(numeroOperacion) && numeroOperacion.Length == 15)
            {
                string codcct = numeroOperacion.Substring(0, 3);
                string codpro = numeroOperacion.Substring(3, 2);
                string codesp = numeroOperacion.Substring(5, 2);
                string codofi = numeroOperacion.Substring(7, 3);
                string codope = numeroOperacion.Substring(10, 5);

                MemoryStream archivoExcel = ftService.GenerarArchivoExcelOperacionReportada(codcct, codpro, codesp, codofi, codope);

                archivos.Add("DatosOperacion.xlsx", archivoExcel);

                if (!string.IsNullOrWhiteSpace(descripcionProblema))
                {
                    // Se genera el archivo de texto con la descripcion del problema
                    byte[] archivoDescripcion = FileHelper.GenerateTextFile(descripcionProblema);

                    MemoryStream ms = new MemoryStream(archivoDescripcion);
                    archivos.Add("DescripcionOperacion.txt", ms);
                }

                // Obtenemos archivos de logs
                var archivosLog = FileHelper.GetLogFiles(string.IsNullOrWhiteSpace(fechaLogs)? DateTime.Now.ToString("yyyy-MM-dd") : fechaLogs);

                foreach (var logs in archivosLog)
                {
                    archivos.Add(logs.Key, logs.Value);
                }

                datosOperacionZip = FileHelper.GetZippedFiles(archivos);
            }

            return File(datosOperacionZip, "application/zip", string.Format("datos_ope{0}.zip", Guid.NewGuid().ToString()));
        }
    }
}