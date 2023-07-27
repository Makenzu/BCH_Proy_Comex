using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.Portal;
using BCH.Comex.UI.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Globalization;
using BCH.Comex.UI.Web.Areas.MT300Planilla.Models;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.Core.Entities.Cext01.MT300Common;
using BCH.Comex.Core.BL.SWG3.Helpers;

namespace BCH.Comex.UI.Web.Areas.MT300Planilla.Controllers
{
    public class DetalleMensajeController : BaseControllerMT300Planilla
    {
        protected GenerarMT300Helper generarMT300Helper = new GenerarMT300Helper();

        // GET: GeneracionMT300/Mensajes
        [AuthorizeOrForbidden(Roles = "COMEX_MT300_ADMIN,COMEX_MT300_CONSULTA")]
        [HttpGet]
        public ActionResult Detalle(string id)
        {
            using (Tracer tracer = new Tracer("Visualizar MT300"))
            {
                tracer.TraceVerbose("Entrando a detalle de Mensaje MT300 " + id);

                var detalleArchivo = service.GetDetalleArchivo(Decimal.Parse(id));

                if (detalleArchivo.codigo_moneda_mn == "")
                {
                    detalleArchivo.codigo_moneda_mn = "CLP";
                }

                DetalleMensajeViewModel viewModel = new DetalleMensajeViewModel();
                viewModel.Mensaje = new MensajeViewModel()
                {
                    idArchivo = detalleArchivo.id_archivo,
                    idDetalle = detalleArchivo.id_detalle,
                    safekeeping = detalleArchivo.safekeeping,
                    reference = detalleArchivo.reference,
                    bookedBy = detalleArchivo.booked_by.ToString("yyyyMMdd"),
                    valueDate = detalleArchivo.value_date.ToString("yyyyMMdd"),
                    rate = detalleArchivo.rate,
                    campo32B = detalleArchivo.amount_mn.ToString(),
                    codMonedamn = detalleArchivo.codigo_moneda_mn,
                    campo33B = detalleArchivo.amount_me.ToString(),
                    codMonedame = detalleArchivo.codigo_moneda_me,
                    estado = detalleArchivo.estado,
                    mensajes = detalleArchivo.mensajes.Split('|'),
                    executionTimehhmmss = detalleArchivo.exec_time_hhmmss.Substring(0,4).Insert(2, ":")
                };
                return View("Detalle", viewModel);
                
            }
        }

        [AuthorizeOrForbidden(Roles = "COMEX_MT300_ADMIN")]
        [HttpPost]
        public ActionResult Generar(DetalleMensajeViewModel model)
        {
            using (Tracer tracer = new Tracer("Modificar detalle registro"))
            {
                tracer.TraceVerbose("Entrando a modificar registro planilla ");

                var detalleArchivo = service.GetDetalleArchivo(model.Mensaje.idDetalle);


                if (detalleArchivo.estado != "ERROR_VALIDACION" && detalleArchivo.estado != "MT300_PREVIO")
                {
                    model.ListaMensajes.Clear();
                    model.ListaMensajes.Add(new Comex.Common.UI_Modulos.UI_Message
                    {
                        Type = Comex.Common.UI_Modulos.TipoMensaje.Error,
                        Text = "Este mensaje no puede ser editado.",
                        Title = "MT300"
                    });

                    return View("Detalle", model);
                }

                if (model.Mensaje.estado == null)
                    model.Mensaje.estado = detalleArchivo.estado;

                model.Mensaje.idArchivo = detalleArchivo.id_archivo;

                if (ModelState.IsValid)
                {

                    if (model.Mensaje.reference != detalleArchivo.reference)
                        detalleArchivo.reference = model.Mensaje.reference;
                    if (model.Mensaje.safekeeping != detalleArchivo.safekeeping)
                        detalleArchivo.safekeeping = model.Mensaje.safekeeping;
                    if (model.Mensaje.bookedBy != detalleArchivo.booked_by.ToString("yyyyMMdd"))
                        detalleArchivo.booked_by = DateTime.ParseExact(model.Mensaje.bookedBy,"yyyyMMdd", CultureInfo.InvariantCulture);
                    if (model.Mensaje.valueDate != detalleArchivo.value_date.ToString("yyyyMMdd"))
                        detalleArchivo.value_date = DateTime.ParseExact(model.Mensaje.valueDate,"yyyyMMdd", CultureInfo.InvariantCulture);

                    if (model.Mensaje.rate != detalleArchivo.rate)
                        detalleArchivo.rate = model.Mensaje.rate;
                    if (model.Mensaje.campo32B != detalleArchivo.amount_mn.ToString())
                        detalleArchivo.amount_mn = Convert.ToDecimal(model.Mensaje.campo32B);
                    if (model.Mensaje.codMonedamn != detalleArchivo.codigo_moneda_mn)
                        detalleArchivo.codigo_moneda_mn = model.Mensaje.codMonedamn;
                    if (model.Mensaje.campo33B != detalleArchivo.amount_me.ToString())
                        detalleArchivo.amount_me = Convert.ToDecimal(model.Mensaje.campo33B);
                    if (model.Mensaje.codMonedame != detalleArchivo.codigo_moneda_me)
                            detalleArchivo.codigo_moneda_me = model.Mensaje.codMonedame;

                    DetalleMensajeViewModel viewModel = new DetalleMensajeViewModel();
                    viewModel.Mensaje = new MensajeViewModel()
                    {
                        idArchivo = detalleArchivo.id_archivo,
                        idDetalle = detalleArchivo.id_detalle,
                        safekeeping = detalleArchivo.safekeeping,
                        reference = detalleArchivo.reference,
                        bookedBy = detalleArchivo.booked_by.ToString("yyyyMMdd"),
                        valueDate = detalleArchivo.value_date.ToString("yyyyMMdd"),
                        rate = detalleArchivo.rate,
                        campo32B = detalleArchivo.amount_mn.ToString(),
                        codMonedamn = detalleArchivo.codigo_moneda_mn,
                        campo33B = detalleArchivo.amount_me.ToString(),
                        codMonedame = detalleArchivo.codigo_moneda_me,
                        executionTimehhmmss = (model.Mensaje.executionTimehhmmss == null) ? detalleArchivo.exec_time_hhmmss.Substring(0, 4).Insert(2, ":") : model.Mensaje.executionTimehhmmss,
                    };

                    tracer.TraceVerbose("Guardar detalle registro ");
                    var detalle = service.SaveDetalleArchivo(detalleArchivo.id_detalle,detalleArchivo.safekeeping, detalleArchivo.reference, detalleArchivo.booked_by, detalleArchivo.value_date, detalleArchivo.rate, detalleArchivo.amount_mn, detalleArchivo.codigo_moneda_mn, detalleArchivo.amount_me, detalleArchivo.codigo_moneda_me);

                    GenerarMT300Result resultadoGeneracion = new GenerarMT300Result();

                    GenerarMT300DatosUser infoUser = new GenerarMT300DatosUser();
                    infoUser.usuarioNombre = infoUsuario.samAccountName;
                    infoUser.usuarioRut = infoUsuario.Identificacion_Rut;
                    infoUser.usuarioCentroCosto = infoUsuario.Identificacion_CentroDeCostosOriginal;


                    List<ArchivoDetalle> registros = service.ObtenerDetalleArchivo(detalleArchivo.id_detalle);

                    tracer.TraceVerbose("Generar mensaje MT300 ");
                    resultadoGeneracion = generarMT300Helper.GenerarSwiftMasivoMT300(registros, infoUser, GenerarMT300Helper.modoManual, GenerarMT300Helper.flujoPlanilla);


                    //****genera mensaje para tooltip resultado
                    viewModel.ListaMensajes = new List<Comex.Common.UI_Modulos.UI_Message>();
                    viewModel.ListaMensajes.Add(new Comex.Common.UI_Modulos.UI_Message
                    {
                        Type = Comex.Common.UI_Modulos.TipoMensaje.Informacion,
                        Text = resultadoGeneracion.Mensaje,
                        Title = "MT300"
                    });




                    return View("Detalle", viewModel);
                }
                else
                {
                    return View("Detalle", model);
                }
                
                            
            }
        }

    }
}
