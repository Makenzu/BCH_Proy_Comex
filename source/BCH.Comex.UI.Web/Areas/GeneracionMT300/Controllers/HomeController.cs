using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.Portal;
using BCH.Comex.UI.Web.Areas.GeneracionMT300.Models;
using BCH.Comex.UI.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.Core.Entities.Cext01.MT300Common;
using BCH.Comex.Core.BL.SWG3.Helpers;

namespace BCH.Comex.UI.Web.Areas.GeneracionMT300.Controllers
{
    public class HomeController : BaseControllerMT300
    {

        protected GenerarMT300Helper generarMT300Helper = new GenerarMT300Helper();

        static HomeController()
        {
            new PortalService().RegisterApp("SWG3", "Generación MT300", "SWIFT",
                "COMEX_MT300_ADMIN", "COMEX_GRP_SWIFT", "GeneracionMT300");
        }

        // GET: GeneracionMT300/Home
        [AuthorizeOrForbidden(Roles = "COMEX_MT300_ADMIN")]
        [HttpGet]
        public ActionResult Index()
        {
            using (Tracer tracer = new Tracer("Index de MT300"))
            {
                tracer.TraceVerbose("Entrando a Index de MT300...");
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, FormCollection fc)
        {
            using (Tracer tracer = new Tracer("Carga de archivo MT300"))
            {

                Dictionary<string, string> paramTiposArchivo = this.service.ObtieneParametrosArchivo(this.Globales);

                var lenghtFile = paramTiposArchivo["length_file"];
                var extensionFiles = paramTiposArchivo["extension_files"];
                string tipoArchivo = "";
                if (fc.Count > 0)
                {
                    tipoArchivo = fc["tipoArchivo"].ToString();
                }

                CargaViewModel model = new CargaViewModel();
                model.ListaMensajes = new List<Comex.Common.UI_Modulos.UI_Message>();
                try
                {
                    if (file == null)
                    {
                        throw new ArgumentException("Se debe seleccionar un archivo válido para cargar.");
                    }
                    if (tipoArchivo != "VALUTA" && tipoArchivo != "TREASURY")
                    {
                        throw new ArgumentException("Debe seleccionar un tipo de Archivo");
                    }

                    tracer.TraceVerbose("Procesando archivo " + file.FileName);

                    if (file.ContentLength > 0 && file.ContentLength <= Int32.Parse(lenghtFile))
                    {
                        if (!extensionFiles.Contains(file.ContentType))
                        {
                            throw new ArgumentException("No es posible continuar porque la extensión del archivo ingresado no es permitido en el Sistema de Comex");
                        }

                        IDatosUsuario usuario = HttpContext.GetCurrentUser().GetDatosUsuario(); //leo devuelta DatosUsuario ya que no puedo usar el de session porque fue leído en un context diferente

                        this.service.ProcesarArchivo(this.Globales, file, tipoArchivo, usuario);

                        model.archivoCargado = true;
                        model.archivo = this.Globales.DatosCarga.archivo;
                        model.registrosNuevosOK = this.Globales.DatosCarga.registros.Where(reg => reg.flag_citi == 1 && reg.flag_existente == 0 && reg.flag_validaciones == 1).ToList();
                        model.registrosExistentes = this.Globales.DatosCarga.registros.Where(reg => reg.flag_citi == 1 && reg.flag_existente == 1).ToList();

                        model.nRegistrosTotales = Decimal.ToInt32(model.archivo.total_registros);

                        model.nRegistrosNuevosOK = Decimal.ToInt32(model.archivo.total_mt300_nuevos);
                        model.nRegistrosExistentes = Decimal.ToInt32(model.archivo.total_mt300_existentes);
                        model.nRegistrosErrorFormato = this.Globales.DatosCarga.registros.Count(reg => reg.flag_citi == 1 && reg.flag_formato == 0);
                        model.nRegistrosErrorCaracteres = this.Globales.DatosCarga.registros.Count(reg => reg.flag_citi == 1 && reg.flag_nack == 0);

                        model.nRegistrosCandidatos = this.Globales.DatosCarga.registros.Count(reg => reg.flag_citi == 1);
                        model.nRegistrosNoCantidatos = Decimal.ToInt32(model.nRegistrosTotales - model.nRegistrosCandidatos);
                    }
                    else
                    {
                        throw new ArgumentException("No es posible continuar porque el archivo subido excede el tamaño permitido");
                    }
                    ViewBag.Message = "Archivo cargado correctamente!!";
                }
                catch (Exception e)
                {
                    tracer.TraceError("Ocurrió un error al procesar el archivo.");
                    tracer.TraceError(e.ToString());
                    ViewBag.Message = "Proceso de carga de archivo con estado fallido!!";
                    model.ListaMensajes.Add(new Comex.Common.UI_Modulos.UI_Message
                    {
                        Type = Comex.Common.UI_Modulos.TipoMensaje.Error,
                        Text = e.Message.ToString(),//"Ocurrió un error al intentar procesar el archivo.",
                        Title = "MT300"
                    });
                }
                return View(model);
            }
        }


        [HttpPost]
        public ActionResult GenerarSwiftMasivo(FormCollection fc)
        {
            using (Tracer tracer = new Tracer("Genera Swift Masivo mensajes MT300"))
            {
                CargaViewModel model = new CargaViewModel();
                GenerarMT300Result resultadoGeneracion = new GenerarMT300Result();

                GenerarMT300DatosUser infoUser = new GenerarMT300DatosUser();
                infoUser.usuarioNombre = infoUsuario.samAccountName;
                infoUser.usuarioRut = infoUsuario.Identificacion_Rut;
                infoUser.usuarioCentroCosto = infoUsuario.Identificacion_CentroDeCostosOriginal;


                Archivo archivo = this.Globales.DatosCarga.archivo;
                List<ArchivoDetalle> registros = new List<ArchivoDetalle>();
                registros.AddRange(this.Globales.DatosCarga.registros.Where(reg => reg.flag_existente == 0 && reg.flag_validaciones == 1).ToList());

                resultadoGeneracion = generarMT300Helper.GenerarSwiftMasivoMT300(registros, infoUser, GenerarMT300Helper.modoAutomatico, GenerarMT300Helper.flujoPlanilla);


                //****genera mensaje para tooltip resultado
                model.ListaMensajes = new List<Comex.Common.UI_Modulos.UI_Message>();
                model.ListaMensajes.Add(new Comex.Common.UI_Modulos.UI_Message
                {
                    Type = Comex.Common.UI_Modulos.TipoMensaje.Informacion,
                    Text = resultadoGeneracion.Mensaje,
                    Title = "MT300"
                });

                return View("index", model);
            }
        }

    }
}
