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
using BCH.Comex.UI.Web.Areas.MT300Gestion.Models;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.Core.Entities.Cext01.MT300Common;
using BCH.Comex.Common.UI_Modulos;

namespace BCH.Comex.UI.Web.Areas.MT300Gestion.Controllers
{
    public class MensajesController : BaseControllerMT300Gestion
    {

        // GET: GeneracionMT300/Mensajes
        [AuthorizeOrForbidden(Roles = "COMEX_MT300_ADMIN,COMEX_MT300_CONSULTA")]
        [HttpGet]
        public ActionResult Detalle(string id)
        {
            using (Tracer tracer = new Tracer("Visualizar MT300"))
            {
                tracer.TraceVerbose("Entrando a detalle de Mensaje MT300 " + id);

                var archivoProcesado = service.GetArchivoProcesado(Decimal.Parse(id));

                if (archivoProcesado.estado_msg != "ENV")
                {
                    tracer.TraceVerbose("El mensaje no se encuentra en estado ENV");
                    Globales.ListaMensajes.Clear();
                    Globales.ListaMensajes.Add(new UI_Message()
                    {
                        Type = (TipoMensaje.Error),
                        Text = "El mensaje no ha sido enviado.",
                        Title = "MT300"
                    });

                    return RedirectToAction("Index", "Home");
                }

                string rate = archivoProcesado.rate.ToString("0.#####");
                if (archivoProcesado.rate % 1 == 0)
                {
                    rate = rate + ",";
                }

                DetalleViewModel viewModel = new DetalleViewModel();
                viewModel.Mensaje = new MensajeViewModel()
                {
                    idProcesados = archivoProcesado.id_procesados,
                    reference = archivoProcesado.reference,
                    campo22A = archivoProcesado.campo22A,
                    campo22C = archivoProcesado.campo22C,
                    campo82A = archivoProcesado.campo82A,
                    campo87A = archivoProcesado.campo87A,
                    bookedBy = archivoProcesado.booked_by.ToString("yyyyMMdd"),
                    valueDate = archivoProcesado.value_date.ToString("yyyyMMdd"),
                    rate = rate,
                    campo32B = archivoProcesado.codigo_moneda_mn + archivoProcesado.amount_mn.ToString(),
                    campo53A = archivoProcesado.campo53A,
                    campo57A = archivoProcesado.campo57A,
                    campo33B = archivoProcesado.codigo_moneda_me + archivoProcesado.amount_me.ToString(),
                    campo98D = archivoProcesado.campo98D,
                };

                return View("Detalle", viewModel);
            }
        }

        [AuthorizeOrForbidden(Roles = "COMEX_MT300_ADMIN")]
        [HttpGet]
        public ActionResult Modificar(string id)
        {
            using (Tracer tracer = new Tracer("Modificar MT300"))
            {
                tracer.TraceVerbose("Entrando a modificación de Mensaje MT300 " + id);

                var archivoProcesado = service.GetArchivoProcesado(Decimal.Parse(id));

                if (archivoProcesado.estado != BCH.Comex.Core.BL.SWG3.EstadosRegistro.procesadoAnulado)
                {
                    tracer.TraceVerbose("El mensaje no se encuentra en estado ANULADO");
                    Globales.ListaMensajes.Clear();
                    Globales.ListaMensajes.Add(new UI_Message()
                    {
                        Type = (TipoMensaje.Error),
                        Text = "Solo es posible modificar mensajes previamente anulados.",
                        Title = "MT300"
                    });

                    return RedirectToAction("Index", "Home");
                }
                else if (archivoProcesado.estado_msg != "ENV")
                {
                    tracer.TraceVerbose("El mensaje no se encuentra en estado ENV");
                    Globales.ListaMensajes.Clear();
                    Globales.ListaMensajes.Add(new UI_Message()
                    {
                        Type = (TipoMensaje.Error),
                        Text = "El mensaje no ha sido enviado.",
                        Title = "MT300"
                    });

                    return RedirectToAction("Index", "Home");
                }

                DetalleViewModel viewModel = new DetalleViewModel();
                string rate = archivoProcesado.rate.ToString("0.#####");
                if (archivoProcesado.rate % 1 == 0)
                {
                    rate = rate + ",";
                }
                viewModel.Mensaje = new MensajeViewModel()
                {
                    idProcesados = archivoProcesado.id_procesados,
                    reference = archivoProcesado.reference,
                    campo22A = archivoProcesado.campo22A,
                    campo22C = archivoProcesado.campo22C,
                    campo82A = archivoProcesado.campo82A,
                    campo87A = archivoProcesado.campo87A,
                    bookedBy = archivoProcesado.booked_by.ToString("yyyyMMdd"),
                    valueDate = archivoProcesado.value_date.ToString("yyyyMMdd"),
                    rate = rate,
                    campo32B = archivoProcesado.codigo_moneda_mn + archivoProcesado.amount_mn.ToString(),
                    campo53A = archivoProcesado.campo53A,
                    campo57A = archivoProcesado.campo57A,
                    campo33B = archivoProcesado.codigo_moneda_me + archivoProcesado.amount_me.ToString(),
                    campo98D = archivoProcesado.campo98D,
                };

                viewModel.EsModificacion = true;
                viewModel.EsNuevo = false;

                return View("Detalle", viewModel);
            }
        }

        [AuthorizeOrForbidden(Roles = "COMEX_MT300_ADMIN")]
        [HttpPost]
        public ActionResult Generar(DetalleViewModel model)
        {
            using (Tracer tracer = new Tracer("Generar MT300"))
            {
                tracer.TraceVerbose("Entrando a generación de Mensaje MT300 ");

                if (model.Mensaje.idProcesados == 0)
                {

                    model.EsNuevo = true;
                }
                else
                {
                    model.EsModificacion = true;
                }

                if (!ValidarFormatos(model.Mensaje))
                {
                    model.ListaMensajes = CargarMensajesGlobales();
                    return View("Detalle", model);
                }

                ArchivoDetalle registro = GetArchivoProcesadoFromMensajeViewModel(model.Mensaje);

                bool result;
                // mensaje nuevo
                if (registro.id_procesados == 0)
                {

                    result = service.GenerarNuevoMensaje(Globales, registro);
                }
                else
                {
                    result = service.ModificarMensaje(Globales, registro);
                }

                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }

                model.ListaMensajes = CargarMensajesGlobales();

                return View("Detalle", model);
            }
        }

        [AuthorizeOrForbidden(Roles = "COMEX_MT300_ADMIN")]
        [HttpGet]
        public ActionResult GenerarNuevo()
        {
            using (Tracer tracer = new Tracer("Nuevo MT300"))
            {
                tracer.TraceVerbose("Entrando a generar nuevo Mensaje MT300 ");

                DetalleViewModel viewModel = new DetalleViewModel();
                viewModel.EsNuevo = true;

                viewModel.Mensaje = new MensajeViewModel();
                viewModel.Mensaje.campo22A = "NEWT";

                return View("Detalle", viewModel);
            }
        }

        [AuthorizeOrForbidden(Roles = "COMEX_MT300_ADMIN")]
        [HandleAjaxException, HttpPost]
        public ActionResult Anular(decimal[] idProcesados)
        {
            using (Tracer tracer = new Tracer("Anular MT300"))
            {
                tracer.TraceVerbose("Entrando a anulación de Mensaje MT300 ");

                bool result = service.AnularMensajes(Globales, idProcesados);

                List<UI_Message> mensajes = CargarMensajesGlobales();

                var data = new
                {
                    Resultado = result,
                    Mensajes = mensajes
                };

                var jsonResult = new JsonResult()
                {
                    Data = data,
                    MaxJsonLength = Int32.MaxValue,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                return jsonResult;
            }
        }

        [HandleAjaxException]
        public ActionResult ValidarReferencia(string referencia)
        {
            List<UI_Message> mensajes = new List<UI_Message>();
            string mensajeError = "";
            bool esValido = this.service.ValidarReferencia(referencia, out mensajeError);

            mensajes.Add(new UI_Message()
            {
                Type = (esValido ? TipoMensaje.Nada : TipoMensaje.Error),
                Text = mensajeError,
                ControlName = "Mensaje_reference"
            });

            var jsonResult = new JsonResult()
            {
                Data = mensajes,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            return jsonResult;
        }

        [HandleAjaxException]
        public ActionResult ValidarFechaYYYYMMDD(string fecha)
        {
            List<UI_Message> mensajes = new List<UI_Message>();
            DateTime output;
            bool esValido = DateTime.TryParseExact(fecha, "yyyyMMdd", null, DateTimeStyles.None, out output);

            mensajes.Add(new UI_Message()
            {
                Type = (esValido ? TipoMensaje.Nada : TipoMensaje.Error),
                Text = "La fecha debe estar en formato AAAAMMDD",
                ControlName = ""
            });

            var jsonResult = new JsonResult()
            {
                Data = mensajes,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            return jsonResult;
        }

        [HandleAjaxException]
        public ActionResult ValidarRate(string rate)
        {
            List<UI_Message> mensajes = new List<UI_Message>();
            string msg;
            bool esValido = service.ValidarRate(rate, out msg);

            mensajes.Add(new UI_Message()
            {
                Type = (esValido ? TipoMensaje.Nada : TipoMensaje.Error),
                Text = msg,
                ControlName = "Mensaje_rate"
            });

            var jsonResult = new JsonResult()
            {
                Data = mensajes,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            return jsonResult;
        }

        [HandleAjaxException]
        public ActionResult ValidarCampoMonto(string monedaValor)
        {
            List<UI_Message> mensajes = new List<UI_Message>();
            string mensajeError = "";
            bool esValido = this.service.ValidarCampoMonto(monedaValor.Trim(), out mensajeError);

            mensajes.Add(new UI_Message()
            {
                Type = (esValido ? TipoMensaje.Nada : TipoMensaje.Error),
                Text = mensajeError,
                ControlName = ""
            });

            var jsonResult = new JsonResult()
            {
                Data = mensajes,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            return jsonResult;
        }

        private bool ValidarFormatos(MensajeViewModel mensaje)
        {
            bool result = true;
            string mensajeError = "";
            if (!service.ValidarCampoMonto(mensaje.campo32B, out mensajeError))
            {
                result = false;
                Globales.ListaMensajes.Add(new UI_Message()
                {
                    Type = (TipoMensaje.Error),
                    Text = mensajeError,
                    ControlName = "Mensaje_campo32B"
                });
            }
            if (!service.ValidarCampoMonto(mensaje.campo33B, out mensajeError))
            {
                result = false;
                Globales.ListaMensajes.Add(new UI_Message()
                {
                    Type = (TipoMensaje.Error),
                    Text = mensajeError,
                    ControlName = "Mensaje_campo33B"
                });
            }

            DateTime output;
            if (!DateTime.TryParseExact(mensaje.bookedBy, "yyyyMMdd", null, DateTimeStyles.None, out output))
            {
                result = false;
                Globales.ListaMensajes.Add(new UI_Message()
                {
                    Type = (TipoMensaje.Error),
                    Text = "La fecha debe estar en formato AAAAMMDD",
                    ControlName = "Mensaje_bookedBy"
                });
            }
            if (!DateTime.TryParseExact(mensaje.valueDate, "yyyyMMdd", null, DateTimeStyles.None, out output))
            {
                result = false;
                Globales.ListaMensajes.Add(new UI_Message()
                {
                    Type = (TipoMensaje.Error),
                    Text = "La fecha debe estar en formato AAAAMMDD",
                    ControlName = "Mensaje_valueDate"
                });
            }

            return result;
        }

        private ArchivoDetalle GetArchivoProcesadoFromMensajeViewModel(MensajeViewModel mensaje)
        {
            return new ArchivoDetalle
            {
                id_procesados = mensaje.idProcesados,
                id_archivo_detalle = 0,
                id_swift = null,
                reference = mensaje.reference,
                amount_mn = String.IsNullOrEmpty(mensaje.campo32B) ? 0 : Decimal.Parse(mensaje.campo32B.Trim().Substring(3)),
                amount_me = String.IsNullOrEmpty(mensaje.campo32B) ? 0 : Decimal.Parse(mensaje.campo33B.Trim().Substring(3)),
                beneficiary = "",
                safekeeping = 0,
                value_date = DateTime.ParseExact(mensaje.valueDate, "yyyyMMdd", null, DateTimeStyles.None),
                rate = Decimal.Parse(mensaje.rate.Trim()),
                booked_by = DateTime.ParseExact(mensaje.bookedBy, "yyyyMMdd", null, DateTimeStyles.None),
                estado = "",
                mensaje_mt = "",
                codigo_moneda_mn = String.IsNullOrEmpty(mensaje.campo32B) ? "" : mensaje.campo32B.Trim().Substring(0, 3),
                codigo_moneda_me = String.IsNullOrEmpty(mensaje.campo33B) ? "" : mensaje.campo33B.Trim().Substring(0, 3),
                campo22A = "",
                campo22C = String.IsNullOrEmpty(mensaje.campo22C) ? "" : mensaje.campo22C.Trim(),
                campo82A = String.IsNullOrEmpty(mensaje.campo82A) ? "" : mensaje.campo82A.Trim(),
                campo87A = String.IsNullOrEmpty(mensaje.campo87A) ? "" : mensaje.campo87A.Trim(),
                campo53A = String.IsNullOrEmpty(mensaje.campo53A) ? "" : mensaje.campo53A.Trim(),
                campo57A = String.IsNullOrEmpty(mensaje.campo57A) ? "" : mensaje.campo57A.Trim(),
                flag_ingresado_nuevo = 1,
                campo98D = ObtenerTimeExecution(mensaje.bookedBy, mensaje.campo98D)
            };
        }

        private string ObtenerTimeExecution(string fechaExec, string campo98DIng) {
            DateTime dateTime;
            string formatofecha = "yyyyMMddhhmmss";
            string campo98Ddefault = fechaExec + "000000";
            try
            {
                campo98DIng = campo98DIng.Trim();
                //si el campo tiene formato correcto retorna lo ingresado, sino aplica default 
                if (DateTime.TryParseExact(campo98DIng, formatofecha, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                {
                    return campo98DIng;
                }
                else {
                    return campo98Ddefault;
                }
            }
            catch {
                return campo98Ddefault;
            }            
        }

    }
}
