using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.SWSE;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.UI.Web.Areas.EnvioSwift.Models;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Models.ConsultaSwift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BCH.Comex.Common.Tracing;

namespace BCH.Comex.UI.Web.Areas.EnvioSwift.Controllers
{
    public class MensajesController : BaseController
    {
        protected EnvioSwiftService service = new EnvioSwiftService();


        [Authorize]
        public ActionResult Ingresar()
        {
            EditorMensajeViewModel model = InicializarModeloEditor();
            return View("Editor", model);
        }

        [Authorize]
        public ActionResult Modificar(int idMensaje)
        {
            EditorMensajeViewModel model = InicializarModeloEditor();
            try
            {
                int MensajeID = 0;
                int.TryParse(idMensaje.ToString(), out MensajeID);
                model.IdMensaje = MensajeID;

                ResultadoBusquedaSwift swift = service.GetSwiftConLineasParaEditor(MensajeID);
                model.LineasMT = swift.LineasEditor;
                model.SwiftBancoReceptor = swift.CodigoYBranchReceptor;
                model.DescBancoReceptor = swift.nombre_banco + "<br/>" + swift.ciudad_banco;
                model.TipoMT = swift.tipo_msg.Trim();
                model.CodMonedaSW = swift.cod_moneda;
                model.Monto = swift.monto.Value;
                model.Casilla = swift.casilla;
            }
            catch (Exception)
            {
                model.Mensajes = new List<UI_Message>() {
                    new UI_Message() {
                        Text = "ATENCION! No es posible modificar los datos del Mensaje, favor volver a intentar.",
                        Type = TipoMensaje.Error
                    }
                };
            }

            return View("Editor", model);
        }

        [HandleAjaxException]
        public ActionResult GetLineasYFormatosParaMT(string codMT)
        {
            JsonResult jsonResult;
            try
            {
                if (!String.IsNullOrEmpty(codMT))
                {
                    codMT = codMT.Trim();
                    if (codMT.Length == 5)
                    {
                        List<LineaEditorMensajeSwift> lineas = service.GetLineasYFormatosParaMT(codMT);
                        if (lineas != null && lineas.Any())
                        {
                            jsonResult = new JsonResult()
                            {
                                Data = lineas,
                                MaxJsonLength = Int32.MaxValue,
                                JsonRequestBehavior = JsonRequestBehavior.AllowGet
                            };

                            return jsonResult;
                        }
                        else
                        {
                            jsonResult = new JsonResult()
                            {
                                Data = "No existe Formato para tipo de mensaje.",
                                MaxJsonLength = Int32.MaxValue,
                                JsonRequestBehavior = JsonRequestBehavior.AllowGet
                            };
                            return jsonResult;
                        }
                    }
                }

                jsonResult = new JsonResult()
                {
                    Data = "El código MT enviado debe tener 5 caracteres",
                    MaxJsonLength = Int32.MaxValue,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                return jsonResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HandleAjaxException, HttpPost, ValidateInput(false)]
        public ActionResult GrabarMensaje(EditorMensajeViewModel model)
        {
            bool todoOK = service.ValidarLineasMensaje(model.LineasMT);
            int? idMensajeNuevo = null;
            if (todoOK)
            {
                string mensajeSwift = service.GenerarMensajeSwift(model.LineasMT, model.TipoMT, model.SwiftBancoReceptor);
                todoOK = service.GuardarMensaje(model.IdMensaje, infoUsuario.Identificacion_Rut, int.Parse(infoUsuario.Identificacion_CentroDeCostosOriginal), model.CodMonedaSW, Convert.ToDouble(model.Monto), mensajeSwift, out idMensajeNuevo);
            }

            //Si se estaba editando un mensaje pendiente o un borrador y todo sale bien, se debe eliminar de la tabla de swift pendientes
            if (todoOK && model.esPendiente && !model.EsPlantilla)
            {
                service.EliminarMensajeSwiftPendiente(model.ctectt, model.codusr, model.archivo);
            }

            var data = new { TodoOK = todoOK, LineasMT = model.LineasMT, Modelo = InicializarModeloEditor(), IdMensajeNuevo = idMensajeNuevo };

            var jsonResult = new JsonResult()
            {
                Data = data,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            return jsonResult;
        }

        [HandleAjaxException, HttpPost, ValidateInput(false)]
        public ActionResult GrabarMensajeModificado(EditorMensajeViewModel model)
        {
            List<UI_Message> listaMensajes = new List<UI_Message>();
            string mensajeSwift = string.Empty;
            ResultadoBusquedaSwift swiftOld = new ResultadoBusquedaSwift();
            bool todoOK = service.ValidarLineasMensaje(model.LineasMT);
            if (todoOK)
            {
                mensajeSwift = service.GenerarMensajeSwift(model.LineasMT, model.TipoMT, model.SwiftBancoReceptor);
                //todoOK = service.GuardarMensaje(model.IdMensaje, infoUsuario.Identificacion_Rut, int.Parse(infoUsuario.Identificacion_CentroDeCostosOriginal), model.CodMonedaSW, Convert.ToDouble(model.Monto), mensajeSwift);

                swiftOld = service.GetSwiftConLineasParaEditor(model.IdMensaje);

                if (swiftOld.CodigoYBranchReceptor == model.SwiftBancoReceptor
                    && swiftOld.cod_moneda == model.CodMonedaSW
                    && swiftOld.monto == model.Monto)
                {
                    string mensajeSwiftOld = service.GenerarMensajeSwift(swiftOld.LineasEditor, swiftOld.tipo_msg.Trim(), swiftOld.CodigoYBranchReceptor);

                    if (mensajeSwiftOld.Equals(mensajeSwift))
                    {
                        listaMensajes.Add(new UI_Message() { Text = "ATENCION! No se han modificado datos del Mensaje.", Type = TipoMensaje.Error });
                        todoOK = false;
                    }
                }

            }

            if (todoOK)
            {
                int? idMensajeNuevo = null; //no se usa, el metodo no devolvera un nuevo id de mensaje
                todoOK = service.GuardarMensaje(model.IdMensaje, infoUsuario.Identificacion_Rut, int.Parse(infoUsuario.Identificacion_CentroDeCostosOriginal), model.CodMonedaSW, Convert.ToDouble(model.Monto), mensajeSwift, out idMensajeNuevo);
            }

            if (todoOK)
            {
                string cambios = string.Empty;
                if (swiftOld.CodigoYBranchReceptor != model.SwiftBancoReceptor)
                {
                    cambios += "Banco Receptor, ";
                }
                if (swiftOld.cod_moneda != model.CodMonedaSW)
                {
                    cambios += "Monto, ";
                }
                cambios += detecta_cambios(model.LineasMT, swiftOld.LineasEditor);

                service.GrabaCambiosMensaje(model.IdMensaje, cambios);
            }

            var data = new { TodoOK = todoOK, LineasMT = model.LineasMT, listaMensajes, Modelo = InicializarModeloEditor() };//new EditorMensajeViewModel()};

            var jsonResult = new JsonResult()
            {
                Data = data,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            return jsonResult;
        }


        [HandleAjaxException, HttpPost, ValidateInput(false)]
        public ActionResult VisualizarMensaje(EditorMensajeViewModel modelEditor)
        {
            ResultadoBusquedaSwift swift = new ResultadoBusquedaSwift(); //el swift a visualizar no viene de un "resultado de busqueda", pero utilizo una vista muy similar a la visualizar de la busqueda
            sw_casillas casilla = service.ObtenerCasillaPorId(int.Parse(infoUsuario.Identificacion_CentroDeCostosOriginal));
            if (casilla != null)
            {
                swift.casilla = casilla.cod_casilla;
                swift.nombre_casilla = casilla.nombre_casilla;
            }

            swift.prioridad = "N";
            swift.cod_banco_em = "BCHICLRM";
            swift.branch_em = "XXX";
            swift.monto = modelEditor.Monto;

            if (!String.IsNullOrEmpty(modelEditor.SwiftBancoReceptor) && modelEditor.SwiftBancoReceptor.Length == 11)
            {
                sw_bancos banco = service.ObtenerBancoPorSwift(modelEditor.SwiftBancoReceptor);
                if (banco != null)
                {
                    swift.cod_banco_rec = banco.cod_banco;
                    swift.branch_rec = banco.branch;
                    swift.pais_banco = banco.pais_banco;
                    swift.ciudad_banco = banco.ciudad_banco;
                }
            }

            sw_monedas moneda = service.ObtenerMonedaPorCodSw(modelEditor.CodMonedaSW);
            swift.cod_moneda = modelEditor.CodMonedaSW;
            if (moneda != null)
            {
                swift.nombre_moneda = moneda.nombre_moneda;
            }

            swift.tipo_msg = modelEditor.TipoMT;
            sw_tipos_msg tipoMT = service.ObtenerTipoMensajePorId(modelEditor.TipoMT);
            if (tipoMT != null)
            {
                swift.nombre_tipo = tipoMT.nombre_tipo;
            }

            swift.LineasDetalle = service.GetCuerpoMensajeSwiftFormatoParaVisualizar(modelEditor.LineasMT);

            DetalleSwiftEnviadoViewModel modelVisualizar = new DetalleSwiftEnviadoViewModel();
            modelVisualizar.Swift = swift;
            return View("Visualizar", modelVisualizar);
        }

        [HandleAjaxException]
        public ActionResult ObtenerBancoPorSwift(string swift)
        {
            if (swift.Length == 11)
            {
                sw_bancos banco = service.ObtenerBancoPorSwift(swift);
                if (banco != null)
                {
                    if (banco.intercambio_clave != "S")
                    {
                        throw new ArgumentException("El banco ingresado no recibe mensajes swift");
                    }
                    else
                    {
                        var jsonResult = new JsonResult()
                        {
                            Data = banco,
                            MaxJsonLength = Int32.MaxValue,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };

                        return jsonResult;
                    }
                }
                else return null;
            }
            else
            {
                throw new ArgumentException("El swift ingresado debe tener 11 caracteres");
            }
        }

        public JsonResult ListaPendientes()
        {
            var listaPendientes = this.service.GetSwiftPendientes(this.infoUsuario.Identificacion_CentroDeCostosImpersonado, this.infoUsuario.Identificacion_IdEspecialistaImpersonado);
            //ListaPendientesViewModel o = new ListaPendientesViewModel(listaPendientes);
            return Json(new { listaPendientes = listaPendientes, codUsr = this.infoUsuario.Identificacion_IdEspecialistaImpersonado });
        }

        public JsonResult EditarSwiftPendiente(string ctecct, string codesp, string archivoEditar)
        {
            using (var tracer = new Tracer("Editar Swift Pendiente"))
            {
                EditorMensajeViewModel model = InicializarModeloEditor();
                try
                {
                    string codMT = String.Empty;
                    string swiftBanco = String.Empty;
                    var swiftPendiente = service.ObtenerMensajeSwiftPendiente(ctecct, codesp, archivoEditar);

                    model.LineasMT = service.DescomponerMensajeSwift(swiftPendiente.msjSwift, out codMT, out swiftBanco);
                    model.SwiftBancoReceptor = swiftBanco.Trim();
                    model.TipoMT = codMT;
                    model.Monto = (double)swiftPendiente.monto;
                    model.CodMonedaSW = swiftPendiente.moneda;

                    //se setean los datos en el modelo para distinguir que es un mensaje pendiente
                    model.esPendiente = true;
                    model.ctectt = ctecct;
                    model.codusr = codesp;
                    model.archivo = archivoEditar;

                    if (swiftPendiente.esPlantilla.HasValue && swiftPendiente.esPlantilla.Value)
                        model.EsPlantilla = true;

                    sw_bancos bancoReceptor = service.ObtenerBancoPorSwift(swiftBanco);
                    if (bancoReceptor != null)
                    {
                        model.DescBancoReceptor = bancoReceptor.nombre_banco + "<br/>" + bancoReceptor.ciudad_banco;
                    }
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta al Editar Swift Pendiente", e);
                    model.Mensajes.Add(new UI_Message() { Text = "ATENCION! " + e.Message, Type = TipoMensaje.Error });
                }
                return Json(new { Model = model });
            }
        }

        public JsonResult EliminarSwiftPendiente(string archivoEliminar)
        {
            using (var tracer = new Tracer("Eliminar Swift Pendiente"))
            {
                try
                {
                    service.EliminarMensajeSwiftPendiente(this.infoUsuario.Identificacion_CentroDeCostosImpersonado, this.infoUsuario.Identificacion_IdEspecialistaImpersonado, archivoEliminar);
                    return Json(new { estado = true });
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta al Eliminar Swift Pendiente", e);
                    return Json(new { estado = false, message = e.Message });
                }
            }
        }

        public JsonResult EnviarSwiftPendiente(string archivoEnviar)
        {
            bool todoOK = false;
            int? idMensajeNuevo = null;

            using (var tracer = new Tracer("EnviarSwiftPendiente"))
            {
                try
                {
                    EditorMensajeViewModel model = InicializarModeloEditor();
                    string codMT = String.Empty;
                    string swiftBanco = String.Empty;
                    var swiftPendiente = service.ObtenerMensajeSwiftPendiente(this.infoUsuario.Identificacion_CentroDeCostosImpersonado, this.infoUsuario.Identificacion_IdEspecialistaImpersonado, archivoEnviar);

                    model.LineasMT = service.DescomponerMensajeSwift(swiftPendiente.msjSwift, out codMT, out swiftBanco);
                    model.SwiftBancoReceptor = swiftBanco;
                    model.TipoMT = codMT;
                    model.Monto = (double)swiftPendiente.monto;
                    model.CodMonedaSW = swiftPendiente.moneda;

                    //se setean los datos en el modelo para distinguir que es un mensaje pendiente
                    model.esPendiente = true;
                    model.ctectt = this.infoUsuario.Identificacion_CentroDeCostosImpersonado;
                    model.codusr = this.infoUsuario.Identificacion_IdEspecialistaImpersonado;
                    model.archivo = archivoEnviar;

                    sw_bancos bancoReceptor = service.ObtenerBancoPorSwift(swiftBanco);
                    if (bancoReceptor != null)
                    {
                        model.DescBancoReceptor = bancoReceptor.nombre_banco + "<br/>" + bancoReceptor.ciudad_banco;
                    }

                    todoOK = service.ValidarLineasMensaje(model.LineasMT);
                    if (todoOK)
                    {
                        string mensajeSwift = service.GenerarMensajeSwift(model.LineasMT, model.TipoMT, model.SwiftBancoReceptor);
                        todoOK = service.GuardarMensaje(model.IdMensaje, infoUsuario.Identificacion_Rut, int.Parse(infoUsuario.Identificacion_CentroDeCostosOriginal), model.CodMonedaSW, Convert.ToDouble(model.Monto), mensajeSwift, out idMensajeNuevo);
                    }

                    if (todoOK && (swiftPendiente.esPlantilla.HasValue && !swiftPendiente.esPlantilla.Value))
                    {
                        service.EliminarMensajeSwiftPendiente(this.infoUsuario.Identificacion_CentroDeCostosImpersonado, this.infoUsuario.Identificacion_IdEspecialistaImpersonado, archivoEnviar);
                    }
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta al Enviar Swift Pendiente", e);
                }
                return Json(new { TodoOk = todoOK, IdMensajeNuevo = idMensajeNuevo });
            }
        }

        [HandleAjaxException, HttpPost, ValidateInput(false)]
        public ActionResult GrabarBorrador(EditorMensajeViewModel model)
        {
            using (var tracer = new Tracer("Grabar Borrador"))
            {
                string nombreArchivo = String.Empty;
                try
                {
                    bool generarNombreNuevo = false;
                    string mensaje = String.Empty;

                    if (model.EsPlantilla)
                        mensaje = "La plantilla de swift se guardó satisfactoriamente";
                    else
                        mensaje = "El borrador de swift se guardó satisfactoriamente";


                    if (String.IsNullOrEmpty(model.nuevoNombreArchivo))
                    {
                        if (model.esPendiente)
                        {
                            if (model.EsPlantilla)
                                mensaje = "La plantilla de swift se actualizó satisfactoriamente";
                            else
                                mensaje = "El borrador de swift se actualizó satisfactoriamente";
                        }
                        else generarNombreNuevo = true;
                    }
                    else
                    {
                        var swiftPendiente = service.ObtenerMensajeSwiftPendiente(this.infoUsuario.Identificacion_CentroDeCostosImpersonado, this.infoUsuario.Identificacion_IdEspecialistaImpersonado, model.nuevoNombreArchivo);
                        if (swiftPendiente != null)
                        {
                            //me vino un nuevo nombre de archivo, pero el mensaje ya existe en BD pendientes, quiere decir que usó un nombre que ya existía
                            return Json(new { todoOK = false, mensaje = "El nombre de archivo ya existe" });
                        }
                        else
                        {
                            model.archivo = model.nuevoNombreArchivo;
                        }
                    }

                    if (generarNombreNuevo)
                    {
                        int correlativoArchivo = service.ObtenerNumeroArchivoSwift(this.infoUsuario.Identificacion_CentroDeCostosImpersonado, this.infoUsuario.Identificacion_IdEspecialistaImpersonado, "CSW", this.infoUsuario.Identificacion_Rut, 1, 99999, 1);
                        nombreArchivo = correlativoArchivo.ToString().PadLeft(6, '0');
                    }
                    else
                        nombreArchivo = model.archivo;

                    /// Se valida que la estructura de las Lineas del MT, contenga al menos un elemento.
                    if(model.LineasMT == null)
                        model.LineasMT = new List<LineaEditorMensajeSwift>();

                    /// Validamos la primera linea
                    if (model.LineasMT.Count == 0)
                        model.LineasMT[0] = new LineaEditorMensajeSwift();

                    if (model.LineasMT.Count > 0 && string.IsNullOrEmpty(model.LineasMT[0].Detalle))
                        model.LineasMT[0].Detalle = string.Empty;

                    string mensajeSwift = service.GenerarMensajeSwift(model.LineasMT, model.TipoMT, model.SwiftBancoReceptor);
                                        
                    bool todoOK = service.GrabarBorrador(this.infoUsuario.Identificacion_CentroDeCostosImpersonado, this.infoUsuario.Identificacion_IdEspecialistaImpersonado, this.infoUsuario.Identificacion_Rut,
                        nombreArchivo, "ESW", DateTime.Now, model.CodMonedaSW, (decimal)model.Monto, model.LineasMT[0].Detalle, model.TipoMT, mensajeSwift, model.EsPlantilla);

                    EditorMensajeViewModel modelo = InicializarModeloEditor();
                    return Json(new { todoOK, mensaje, modelo });
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta al Guardar Borrador Swift Pendiente", ex);
                    throw new ArgumentException("Error al grabar borrador. Detalle: " + ex.Message.ToString());
                }
            }
        }

        [HandleAjaxException]
        public ActionResult ObtenerMoneda()
        {
            using (var tracer = new Tracer("ObtenerMoneda"))
            {
                EditorMensajeViewModel model = new EditorMensajeViewModel();
                model.Monedas = service.GetMonedas();
                model.CaracteresError = service.GetCaracteresError();
                model.CaracteresError_Z = service.GetCaracteresError_Z();
                model.CamposMontos = service.GetCamposMontos();

                var jsonResult = new JsonResult()
                {
                    Data = model,
                    MaxJsonLength = Int32.MaxValue,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                return jsonResult;
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ValidaMTMonedaMonto(string mt)
        {
            using (var tracer = new Tracer("ValidaMTMonedaMonto"))
            {
                try
                {
                    var jsonResult = new JsonResult()
                    {
                        Data = service.ValidaMTMonedaMonto(mt),
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                    return jsonResult;
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta al realizar la validacion moneda/monto", ex);
                    var jsonResult = new JsonResult()
                    {
                        Data = "No es posible realizar la validación del campo Moneda y Monto según MT.",
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                    return jsonResult;
                }
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ObtenerCamposSumatoriaMontoTotalMT(string mt)
        {
            using (var tracer = new Tracer("ObtenerCamposSumatoriaMontoTotalMT"))
            {
                try
                {
                    var jsonResult = new JsonResult()
                    {
                        Data = service.ObtieneCamposSumatoriaMontoTotalMT(mt),
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                    return jsonResult;
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta al realizar la obtención de los campos para la sumatoria de montos para el total", ex);
                    var jsonResult = new JsonResult()
                    {
                        Data = "No es posible realizar la obtención de los campos para la sumatoria de montos para el total",
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                    return jsonResult;
                }
            }
        }

        #region Metodos privados

        private EditorMensajeViewModel InicializarModeloEditor()
        {
            EditorMensajeViewModel model = new EditorMensajeViewModel();
            model.TiposMT = service.GetTipoMensajesConFormato();
            model.Monedas = service.GetMonedas();
            model.CaracteresError = service.GetCaracteresError();
            model.CaracteresError_Z = service.GetCaracteresError_Z();
            model.CamposMontos = service.GetCamposMontos();

            model.EsSupervisor = this.service.UsuarioEsSupervisor(this.infoUsuario.Identificacion_CentroDeCostosImpersonado, this.infoUsuario.Identificacion_IdEspecialistaImpersonado);
            return model;
        }

        protected override void Dispose(bool disposing)
        {
            if (service != null)
            {
                service.Dispose();
            }
        }

        private string detecta_cambios(IList<LineaEditorMensajeSwift> swiftNew, IList<LineaEditorMensajeSwift> swiftOld)
        {
            string cambios = string.Empty;

            foreach (var lineaNew in swiftNew)
            {
                var lineaOld = (LineaEditorMensajeSwift)swiftOld.Where(c => c.CodCam == lineaNew.CodCam).FirstOrDefault();
                if (lineaNew.Detalle != lineaOld.Detalle || lineaNew.Incluido != lineaOld.Incluido || lineaNew.VarianteSeleccionada != lineaOld.VarianteSeleccionada)
                {
                    cambios += lineaNew.CodCam + " ,";
                }
                if (lineaNew.LineasSecundarias != null)
                {
                    for (int i = 0; i < lineaNew.LineasSecundarias.Count; i++)
                    {
                        if (lineaNew.LineasSecundarias[i].Detalle != lineaOld.LineasSecundarias[i].Detalle)
                        {
                            cambios += lineaNew.CodCam + " ,";
                            break;
                        }
                    }
                }
            }
            return cambios;
        }

        #endregion
    }
}