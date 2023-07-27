var viewModel = null;
var montos = null;
var validarCamposMT = true;
var tieneMontosAdicionales = false;

var sumatoriaDeImportes = [];
var camposParaSumatoriaMontoTotal = [];

/**
 * Array.prototype.[method name] allows you to define/overwrite an objects method
 * needle is the item you are searching for
 * this is a special variable that refers to "this" instance of an Array.
 * returns true if needle is in the array, and false otherwise
 */
Array.prototype.contains = function (needle) {
    return RegExp('^' + needle + '$').test(this)
}

function btnGrabarClick() {

    if (viewModel.IdMensaje() == 0) {
        //grabado de mensajes sin IdMensaje
        if (validarSwift(true, false)) {
            //esta todo OK con el mensaje

            var data = { model: ko.mapping.toJS(viewModel, { ignore: getIgnoreListParaMapping() }) };

            return $.ajax({
                type: "POST",
                cache: false,
                url: urlGrabarMensaje,
                data: data,
                error: function (response, type, message) {
                    try {
                        //intento parsear la respesta como json.
                        var responseJson = JSON.parse(response.responseText);
                        showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
                    }
                    catch (err) {
                        showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", true);
                    }
                },
                success: function (result) {
                    if (result.TodoOK) {
                        showAlert("Operación exitosa", "El swift se grabó satisfactoriamente", "alert-success", true);
                        ko.mapping.fromJS(result.Modelo, {}, viewModel);
                        data.model.IdMensaje = result.IdMensajeNuevo;
                        solicitarFirmas(data.model);
                    }
                    else {
                        //hay error asi que actualizo las lineas del MT, para poder desplegar sus errores
                        ko.mapping.fromJS(result.LineasMT, {}, viewModel.LineasMT);
                        posicionarCursorEnPrimerCampoEnError();
                    }
                }
            });
        }
        else {
            posicionarCursorEnPrimerCampoEnError();
        }
    } else {
        //Si se modifica un mensaje con IdMensaje
        if (confirm("¿ Acepta modificar el mensaje Nº : " + viewModel.IdMensaje() + " ?")) {
            if (validarSwift(true, false)) {
                //esta todo OK con el mensaje

                var data = { model: ko.mapping.toJS(viewModel, { ignore: getIgnoreListParaMapping() }) };

                return $.ajax({
                    type: "POST",
                    cache: false,
                    url: urlGrabarMensajeModificado,
                    data: data,
                    error: function (response, type, message) {
                        try {
                            //intento parsear la respesta como json.
                            var responseJson = JSON.parse(response.responseText);
                            showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
                        }
                        catch (err) {
                            showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", true);
                        }
                    },
                    success: function (result) {
                        if (result.TodoOK) {
                            showAlert("Operación exitosa", "El swift se grabó satisfactoriamente", "alert-success", true);
                            solicitarFirmas(data.model);
                            ko.mapping.fromJS(result.Modelo, {}, viewModel);
                        }
                        else {

                            if (result.listaMensajes.length > 0) {
                                loadMessages(result.listaMensajes);
                            }
                            else {
                                //hay error asi que actualizo las lineas del MT, para poder desplegar sus errores
                                ko.mapping.fromJS(result.LineasMT, {}, viewModel.LineasMT);
                                posicionarCursorEnPrimerCampoEnError();
                            }
                        }
                    }
                });
            }
            else {
                posicionarCursorEnPrimerCampoEnError();
            }
        }
    }
}

function posicionarCursorEnPrimerCampoEnError() {
    //esto es mas facil hacerlo por jQuery que iterando por el viewModel
    $(".lblMensajeError:visible").first().siblings(":visible").focus();
}

function validarSwift(validarParaEnviar, borrador) {
    var algunoEnError = false;
    sumatoriaDeImportes = [];

    if (viewModel.SwiftBancoReceptor() == null || viewModel.SwiftBancoReceptor() == "") {
        algunoEnError = true;
        setValidezDeControl($("#txtSwiftBancoReceptor"), false, "Debe ingresar un valor");
    }
    else {
        setValidezDeControl($("#txtSwiftBancoReceptor"), true, "");
    }

    if (validarParaEnviar) {
        obtenerValidacionMonedaYMonto(viewModel.TipoMT(), function (resp) {
            if (resp) {
                if (viewModel.Monto() <= 0) {
                    algunoEnError = true;
                    setValidezDeControl($("#txtMonto"), false, "Debe ingresar un valor");
                }
                else {
                    setValidezDeControl($("#txtMonto"), true, "");
                }

                if (viewModel.CodMonedaSW() == null || viewModel.CodMonedaSW() == "") {
                    algunoEnError = true;
                    setValidezDeControl($("#ddlMoneda"), false, "Debe ingresar un valor");
                }
                else {
                    setValidezDeControl($("#ddlMoneda"), true, "");
                }
            }
        });
    }

    $.each(viewModel.LineasMT(), function (i, linea) {
        linea.TieneErrorVariante(false);
        linea.TieneErrorDetalle(false);

        if (linea.Incluido() || (linea.Obligatorio() && !borrador)) {
            if (linea.TieneVariantes()) {
                if (linea.VarianteSeleccionada() != null) {
                    var algunaLineaTieneDetalle = false;
                    $.each(linea.LineasSecundarias(), function (j, lSecundaria) {
                        if (lSecundaria.CodCam() == linea.VarianteSeleccionada()) {
                            if (lSecundaria.Detalle() == null || lSecundaria.Detalle().trim().length == 0) {
                                //esta vacio, no tiene error
                                lSecundaria.TieneErrorDetalle(false);
                            }
                            else {
                                algunaLineaTieneDetalle = true;
                                if (!validarFormatos(lSecundaria)) {
                                    algunoEnError = true;
                                }
                                else {
                                    lSecundaria.TieneErrorDetalle(false);
                                }
                            }
                        }
                    });

                    if (!algunaLineaTieneDetalle) {
                        linea.TieneErrorVariante(true)
                        linea.MensajeError("Debe ingresar un valor para algúna de las líneas");
                        algunoEnError = true;
                    }
                }
                else {
                    linea.TieneErrorVariante(true)
                    linea.MensajeError("Seleccione una variante");
                    algunoEnError = true;
                }
            }
            else {
                if (linea.Detalle() == null || linea.Detalle().trim().length == 0) {
                    if (linea.Formato().indexOf('/') == 0 && linea.LineasSecundarias().length > 0) {
                        if (linea.LineasSecundarias()[0].Detalle() == null || linea.LineasSecundarias()[0].Detalle().trim().length == 0) {
                            linea.TieneErrorDetalle(true);
                            linea.MensajeError("Debe ingresar un valor");
                            algunoEnError = true;
                        }
                    }
                    else {
                        linea.TieneErrorDetalle(true);
                        linea.MensajeError("Debe ingresar un valor");
                        algunoEnError = true;
                    }
                }
                else {
                    if (!validarFormatos(linea)) {
                        algunoEnError = true;
                    }
                }
            }
        }

        return true; //para que el each siga iterando
    });

    if (tieneMontosAdicionales && sumatoriaDeImportes.length > 0) {
        var montoTotalImportes = 0;

        $.each(sumatoriaDeImportes, function (i, montoAdicionalMT) {
            var valorMonto = numeral().unformat(montoAdicionalMT.valorAdicional);
            montoTotalImportes += valorMonto;
        });

        var valorTotalMensaje = numeral().unformat(viewModel.Monto());

        if (valorTotalMensaje !== montoTotalImportes) {
            if (sumatoriaDeImportes.length > 1) {
                $.each(sumatoriaDeImportes, function (i, montoAdicionalMT) {
                    montoAdicionalMT.linea.TieneErrorDetalle(true);
                    montoAdicionalMT.linea.MensajeError("La sumatoria de los montos no corresponde con el monto del encabezado");
                });
            }
            else {
                sumatoriaDeImportes[0].linea.TieneErrorDetalle(true);
                sumatoriaDeImportes[0].linea.MensajeError("El monto no corresponde con el monto del encabezado");
            }

            algunoEnError = true;
        }
    }

    return !algunoEnError;
}

function obtenerValidacionMonedaYMonto(campo, functionSuccess) {
    $.ajax({
        type: "POST",
        cache: false,
        url: urlGetValitedMontoMoneda,
        data: { mt: campo },
        success: function (r) {
            if (typeof (r) === "boolean")
                functionSuccess(r);
            else {
                showAlert("Error en la operación.", "Detalles: " + r, "alert-danger", true);
                functionSuccess(false);
            }
        }
    });
}

function obtenerCamposSumatoriaMontoTotalMT(mt) {
    $.ajax({
        type: "POST",
        cache: false,
        url: urlObtieneCamposSumatoriaMtoTotal,
        data: { mt: mt },
        success: function (data) {
            camposParaSumatoriaMontoTotal = data;

            if (camposParaSumatoriaMontoTotal.length > 0) {
                tieneMontosAdicionales = true;
            }
        },
        error: function (error) {
            showAlert("Error en la operación.", "Detalles: " + error, "alert-danger", true);
        }
    });
}

function obtenerCaracterInvalido(asciiCode) {
    if (viewModel.CaracteresError != null) {
        for (var j = 0; j < viewModel.CaracteresError.length; j++) {
            var caracter = viewModel.CaracteresError[j];
            if (caracter.valor_ascii == asciiCode) {
                return caracter.caracter;
            }
        }
        return null;
    }
}

function obtenerCaracterInvalido_Z(asciiCode) {
    if (viewModel.CaracteresError_Z != null) {
        for (var j = 0; j < viewModel.CaracteresError_Z.length; j++) {
            var caracter = viewModel.CaracteresError_Z[j];
            if (caracter.valor_ascii == asciiCode) {
                return caracter.caracter;
            }
        }
        return null;
    }
}

function validarFormatos(linea) {
    var todoOK = validarTextoCaracteresInvalidos(linea) && validarTextoFechas(linea)
        && validarTextoGuion(linea) && validarCampoMonto(linea);
    return todoOK;
}

function validarTextoCaracteresInvalidos(linea) {
    var result = true;

    var formato = linea.Formato();
    var validarCaracteresZ = false;

    // se verifica el formato para validar X o Z
    if (formato.indexOf("Z") !== -1) {
        validarCaracteresZ = true;
    }

    // se valida detalle lineas primarias
    var caracterInvalido = null;
    var invalidosPrimaria = [];

    for (var i = 0; i < linea.Detalle().length; i++) {
        var asciiCode = linea.Detalle().charCodeAt(i);
        if (validarCaracteresZ)
            caracterInvalido = obtenerCaracterInvalido_Z(asciiCode);
        else
            caracterInvalido = obtenerCaracterInvalido(asciiCode);

        // si tenemos caracter invalido lo añadimos al arreglo
        if (caracterInvalido != null) {
            invalidosPrimaria.push(caracterInvalido);
        }
    }

    // Si se tienen varios caracteres invalidos en la linea se muestran en el mensaje de error
    if (invalidosPrimaria && invalidosPrimaria.length > 0) {
        var caracteresInvalidos = "";
        for (var i = 0; i < invalidosPrimaria.length; i++) {
            caracteresInvalidos += invalidosPrimaria[i];
        }

        linea.TieneErrorDetalle(true);
        linea.MensajeError("El texto no debe contener el/los caracteres " + caracteresInvalidos);

        result = false;
    }
    else {
        limpiarErroresLineaPrimaria(linea);
    }

    // se validan lineas secundarias
    var caracterInvalidoSecundarias = null;
    var invalidosSecundaria = [];

    if ((linea.LineasSecundarias != undefined) && linea.LineasSecundarias().length > 0) {
        for (var j = 0; j < linea.LineasSecundarias().length; j++) {
            // ya que revisamos todas las lineas secundarias validamos las que tengan detalle
            if (linea.LineasSecundarias()[j].Detalle()) {
                for (var k = 0; k < linea.LineasSecundarias()[j].Detalle().length; k++) {
                    var detalle = linea.LineasSecundarias()[j].Detalle();
                    var asciiCode = detalle.charCodeAt(k);
                    if (validarCaracteresZ)
                        caracterInvalidoSecundarias = obtenerCaracterInvalido_Z(asciiCode);
                    else
                        caracterInvalidoSecundarias = obtenerCaracterInvalido(asciiCode);

                    // si tenemos caracter invalido lo añadimos al arreglo
                    if (caracterInvalidoSecundarias != null) {
                        invalidosSecundaria.push(caracterInvalidoSecundarias);
                    }
                }
            }

            // Si se tienen varios caracteres invalidos en la linea se muestran en el mensaje de error
            if (invalidosSecundaria && invalidosSecundaria.length > 0) {
                var caracteresInvalidos = "";
                for (var i = 0; i < invalidosSecundaria.length; i++) {
                    caracteresInvalidos += invalidosSecundaria[i];
                }

                linea.LineasSecundarias()[j].TieneErrorDetalle(true);
                linea.LineasSecundarias()[j].MensajeError("El texto no debe contener el/los caracteres " + caracteresInvalidos);

                caracterInvalidoSecundarias = null;
                invalidosSecundaria = [];
                result = false;
            }
            else {
                var lineaSecundaria = linea.LineasSecundarias()[j];
                limpiarErroresLineaSecundaria(lineaSecundaria)
            }
        }
    }

    return result;
}

function limpiarErroresLineaPrimaria(linea) {
    // limpiamos errores previos que haya tenido la linea en caso de no tener caracter invalido
    linea.TieneErrorDetalle(false);
    linea.MensajeError("");
}

function limpiarErroresLineaSecundaria(lineaSecundaria) {
    // limpiamos errores previos que haya tenido la linea en caso de no tener caracter invalido
    lineaSecundaria.TieneErrorDetalle(false);
    lineaSecundaria.MensajeError("");
}

function validarTextoFechas(linea) {
    var textoFecha = null;
    var formatoMoment = null;

    var indiceFecha = -1;

    indiceFecha = linea.Formato().indexOf("AAMMDD")
    if (indiceFecha != -1) {
        formatoMoment = "YYMMDD";
    }
    else {
        indiceFecha = linea.Formato().indexOf("AAAAMMDD");
        if (indiceFecha != -1) {
            formatoMoment = "YYYYMMDD";
        }
    }

    if (formatoMoment != null) {
        var textoFecha = linea.Detalle().substr(indiceFecha, formatoMoment.length);
        var fecha = moment(textoFecha, formatoMoment);

        if (!fecha.isValid()) {
            linea.TieneErrorDetalle(true);
            linea.MensajeError("La fecha no es válida");
            return false;
        }
    }
    return true;
}

function validarTextoGuion(linea) {
    var indiceCaracterEnFormato = linea.Formato().indexOf("/");
    if (indiceCaracterEnFormato != -1) {
        var indiceCaracterEnDetalle = linea.Detalle().indexOf("/");

        if (indiceCaracterEnDetalle == -1) {
            linea.TieneErrorDetalle(true);
            if (indiceCaracterEnFormato == 0) {
                linea.MensajeError("El texto debe ser un número de cuenta y comenzar con el caracter /.<br>Si no tiene el dato, deje la línea en blanco.");
            }
            else {
                linea.MensajeError("El texto no contiene el caracter /.<br>Si no tiene el dato, deje la línea en blanco.");
            }
            return false;
        }
        else {
            if (indiceCaracterEnFormato == 0 && indiceCaracterEnDetalle == 0) {
                var textoNroCuenta = linea.Detalle().substr(1).trim();
                if (textoNroCuenta.length == 0) {
                    linea.TieneErrorDetalle(true);
                    linea.MensajeError("El texto debe contener número de cuenta después del caracter /.<br>Si no tiene el dato, deje la línea en blanco.");
                }
            }
        }
    }
    return true;
}

function validarCampoMonto(linea) {
    var todosLosMontosOK = true;
    var encontreLineaMontoQueCorrespondeAMontoIngresadoEnEncabezado = false;
    if (validarCamposMT) {
        $.each(viewModel.CamposMontos(), function (i, campoMonto) {
            if (linea.CodCam() == campoMonto.Campo()) {
                var montoOK = true;

                var indiceEspacio = linea.Detalle().indexOf(" ");
                if (indiceEspacio != -1) {
                    linea.TieneErrorDetalle(true);
                    linea.MensajeError("El monto no debe contener espacios");
                    todosLosMontosOK = false;
                    montoOK = false;
                }
                else {
                    var indiceComa = linea.Detalle().indexOf(",");
                    if (indiceComa == -1) {
                        linea.TieneErrorDetalle(true);
                        linea.MensajeError("El monto debe contener coma decimal");
                        todosLosMontosOK = false;
                        montoOK = false;
                    }
                }

                if (montoOK) {
                    campoMonto.Existe(true);
                    campoMonto.ValorMto(linea.Detalle().substr(campoMonto.PosMto() - 1, campoMonto.LenMto()));
                    campoMonto.ValorMnd(linea.Detalle().substr(campoMonto.PosMnd() - 1, campoMonto.LenMnd()));


                    if (campoMonto.TipoVal() == 0) {//solo valida lo basico y que el monto sea igual a 0
                        if (viewModel.CodMonedaSW() != campoMonto.ValorMnd()) {
                            linea.TieneErrorDetalle(true);
                            linea.MensajeError("La moneda no corresponde con la moneda indicada en el encabezado");
                            todosLosMontosOK = false;
                        } else if (numeral().unformat(campoMonto.ValorMto()) != 0) {
                            linea.TieneErrorDetalle(true);
                            linea.MensajeError("El monto del campo no puede ser diferente a 0");
                            todosLosMontosOK = false;
                        }
                        else {
                            linea.TieneErrorDetalle(false);
                            linea.MensajeError("");
                        }
                    }
                    else if (campoMonto.TipoVal() == 1) {//valida de forma normal
                        if (viewModel.CodMonedaSW() != campoMonto.ValorMnd()) {
                            linea.TieneErrorDetalle(true);
                            linea.MensajeError("La moneda no corresponde con la moneda indicada en el encabezado");
                            todosLosMontosOK = false;
                            return;
                        }
                        else {
                            linea.TieneErrorDetalle(false);
                            linea.MensajeError("");
                        }

                        if (tieneMontosAdicionales && camposParaSumatoriaMontoTotal.includes(linea.CodCam())) {
                            $.each(camposParaSumatoriaMontoTotal, function (i, campo) {
                                if (linea.CodCam() === campo) {
                                    var montoAdicional = { linea: linea, valorAdicional: campoMonto.ValorMto() };
                                    sumatoriaDeImportes.push(montoAdicional);
                                }
                            });
                        }
                        else {
                            if (viewModel.Monto() != "" && numeral().unformat(viewModel.Monto()) != numeral().unformat(campoMonto.ValorMto())) {
                                linea.TieneErrorDetalle(true);
                                linea.MensajeError("El monto no corresponde con el monto indicado en el encabezado");
                                todosLosMontosOK = false;
                            }
                        }
                    } else if (campoMonto.TipoVal() == 2) {//valida de el total de la operacion y los gastos                     
                        var gastos = $.grep(montos, function (e) { return e.Campo == "71F"; })
                        var montoTotal = numeral().unformat(viewModel.Monto());
                        if (gastos.length > 0)
                            montoTotal += numeral().unformat(gastos[0].Monto);

                        if (montoTotal != numeral().unformat(campoMonto.ValorMto())) {
                            linea.TieneErrorDetalle(true);
                            linea.MensajeError("El monto no corresponde con el total de la operación");
                            todosLosMontosOK = false;
                        }
                        else if (viewModel.CodMonedaSW() != campoMonto.ValorMnd()) {
                            linea.TieneErrorDetalle(true);
                            linea.MensajeError("La moneda no corresponde con la moneda indicada en el encabezado");
                            todosLosMontosOK = false;
                        }
                        else {
                            linea.TieneErrorDetalle(false);
                            linea.MensajeError("");
                        }
                    }
                }
                else {
                    campoMonto.Existe(false);
                }
            }

            return true; //para que el each siga iterando
        });
    }

    return todosLosMontosOK;
}

function setValidezDeControl(control, esValido, mensaje) {
    var formGroup = control.closest(".form-group");
    control.siblings(".lblMensajeError").remove();
    formGroup.toggleClass("has-error", !esValido);

    if (!esValido) {
        var htmlLabel = "<label class='lblMensajeError control-label'>" + mensaje + "</label>";
        control.after(htmlLabel);
    }
}

function limpiarTodosLosErrores() {
    $(".lblMensajeError").remove();  //elimino cualquier label de error
    $("div.form-group.has-error").removeClass("has-error");
    $("#msg-zone").html("");
}

function buscarBancoPorSwift() {
    var txtSwift = $("#txtSwiftBancoReceptor");
    txtSwift.val(txtSwift.val().toUpperCase());

    setValidezDeControl(txtSwift, true, "");
    viewModel.DescBancoReceptor("");

    obtenerBancoPorSwift(txtSwift, true, function (r) {
        if (r != null && r != "") {
            viewModel.DescBancoReceptor(r.nombre_banco + "<br/>" + r.ciudad_banco);
            viewModel.SwiftBancoReceptor(txtSwift.val());
        }
        else {
            setValidezDeControl(txtSwift, false, "No existe el banco ingresado");
        }
    });


    obtenerMoneda("", true, function (r) {
        if (r.Monedas.length > 0) {
            //viewModelmodel.Monedas =r.Monedas;
        }
        else {
            // setValidezDeControl(txtSwift, false, "No existe el banco ingresado");
        }
    });




}

function limpiarDatosBancoSeleccioando() {
    viewModel.DescBancoReceptor("");
    viewModel.SwiftBancoReceptor("");
}

function limpiarControles() {
    limpiarTodosLosErrores();
    limpiarDatosBancoSeleccioando();
    viewModel.TipoMT(null);
    viewModel.CodMonedaSW(null);
    viewModel.Monto(0);
    viewModel.IdMensaje(0);
    viewModel.LineasMT(null);
}

function btnNuevoClick() {
    if (viewModel.TipoMT() != null) {
        if (confirm("¿Confirma que desea continuar sin guardar los cambios?")) {
            limpiarControles();
            $("#ddlTipoMT").focus();
        }
    } else {
        $("#ddlTipoMT").focus();
    }
}

function btnVisualizarClick() {
    var data = { modelEditor: ko.mapping.toJS(viewModel, { ignore: getIgnoreListParaMapping() }) };
    $.ajax({
        type: "POST",
        cache: false,
        url: urlVisualizarMensaje,
        success: function (resultado) {
            $("#divCuerpoMensajeSwift").html(resultado);
            $('#modalVisorSwift').modal({ backdrop: true });
        },
        data: data,
        error: function (request, type, message) {
            alert(type + " - " + message);
        }
    });
}

function btnConfirmarGuardarBorradorClick() {
    if (viewModel.nuevoNombreArchivo() == null || viewModel.nuevoNombreArchivo() == "") {
        setValidezDeControl($("#txtNombreBorrador"), false, "Debe ingresar un valor");
    }
    else {
        setValidezDeControl($("#txtNombreBorrador"), true);
        guardarBorradorConfirmado();
    }
}

function guardarBorradorConfirmado() {
    viewModel.EsPlantilla(guardandoComoPlantilla);

    var data = { model: ko.mapping.toJS(viewModel, { ignore: getIgnoreListParaMapping() }) };
    $.ajax({
        type: "POST",
        chache: false,
        url: urlGrabarBorrador,
        data: data,
        success: function (resultado) {
            $('#modalBorrador').modal('hide');

            if (resultado.todoOK) {
                ko.mapping.fromJS(resultado.modelo, {}, viewModel);
                showAlert("Operación exitosa", resultado.mensaje, "alert-success", true);
            } else {
                showAlert("Operación erronea", resultado.mensaje, "alert-danger", true);
            }
        },
        error: function (response, type, message) {
            try {
                //intento parsear la respesta como json.
                var responseJson = JSON.parse(response.responseText);
                showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
            }
            catch (err) {
                showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", true);
            }
        }
    });
}

function mostrarModalDatosBorrador(pedirNombre) {
    if (guardandoComoPlantilla) {
        $('#titleModalBorrador').text("Datos de la plantilla");
    }
    else {
        $('#titleModalBorrador').text("Datos del borrador");
    }

    if (pedirNombre) {
        //muestro modal para solicitar nombre
        $('#modalBorrador').modal({ backdrop: true });

    }
    else {
        //me salteo el modal, llamo a la accion directamente
        guardarBorradorConfirmado();
    }
}

var guardandoComoPlantilla = false;

function btnGrabarPlantillaClick() {
    btnGrabarBorradorClick(true);
}

function btnGrabarBorradorClick(guardarComoPlantilla) {
    if (guardarComoPlantilla === true)
        guardandoComoPlantilla = true;
    else guardandoComoPlantilla = false;

    var pedirNombre = false;

    if (viewModel.esPendiente()) {
        if (viewModel.EsPlantilla() == true && !guardandoComoPlantilla) {
            //es una plantilla que ahora esta guardando como borrador normal, le muestro una confirmación por las dudas
            if (confirm("¿Confirma que desea guardar la plantilla como borrador?\nLa plantilla original no se verá afectada y se generará un nuevo borrador en swift pendientes.")) {
                pedirNombre = true;
            }
            else return;
        }
        else pedirNombre = (!viewModel.EsPlantilla() && guardandoComoPlantilla);
    }
    else {
        pedirNombre = true;
    }

    if (viewModel.LineasMT() != null) {
        limpiarTodosLosErrores();
        mostrarModalDatosBorrador(pedirNombre);
    } else {
        showAlert("Error en la operación.", "Detalles: No existe Formato para tipo de mensaje.", "alert-danger", true);
    }
}

function btnAbrirClick() {
    $.ajax({
        type: "POST",
        cache: false,
        url: urlListaPendientes,
        success: function (resultado) {
            var modelLista = resultado.listaPendientes;
            inicializarListaPendientes(modelLista, resultado.codUsr, viewModel.EsSupervisor());
        },
        error: function (request, type, message) {
            alert(type + " - " + message);
        }
    });
}

function obtenerBancoPorSwift(campo, mensajeErrorEnCampo, funcionSuccess) {
    var texto = campo.val();
    if (texto.length == 8) {
        texto = texto + "XXX";
        campo.val(texto);
    }

    if (texto.length == 11) {
        return $.ajax({
            type: "GET",
            cache: false,
            url: urlGetBancoPorSwift,
            data: { swift: texto },
            error: function (response, type, message) {
                try {
                    //intento parsear la respesta como json.
                    var responseJson = JSON.parse(response.responseText);
                    showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", false);
                }
                catch (err) {
                    showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", false);
                }
            },
            success: function (r) {
                funcionSuccess(r);
            }
        });
    }
    else {
        if (texto.length == 0) {
            limpiarDatosBancoSeleccioando();
        }
        else {
            var msgError = "El código ingresado debe tener 11 caracteres.";
            if (mensajeErrorEnCampo) {
                setValidezDeControl(campo, false, msgError);
            }
            else {
                showAlert("Formato incorrecto", msgError, "alert-danger", true);
            }
            limpiarDatosBancoSeleccioando();
        }
    }
}

function obtenerMoneda(campo, mensajeErrorEnCampo, funcionSuccess) {
    return $.ajax({
        type: "GET",
        cache: false,
        url: urlGetMoneda,
        data: {},
        success: function (r) {
            try {
                ko.mapping.fromJS(r.Monedas, {}, viewModel.Monedas);
                ko.mapping.fromJS(r.CaracteresError, {}, viewModel.CaracteresError);
                ko.mapping.fromJS(r.CaracteresError_Z, {}, viewModel.CaracteresError_Z);
                ko.mapping.fromJS(r.CamposMontos, {}, viewModel.CamposMontos);

            }
            catch (err) {
                showAlert("Error en la operación.", "Detalles: " + r, "alert-danger", true);
                viewModel.Monedas(null);
            }
        }
    });
}

function changeTipoMT() {
    if (viewModel.TipoMT() != null) {

        return $.ajax({
            type: "GET",
            cache: true,
            url: urlGetLineasYFormatosMT,
            data: { codMT: viewModel.TipoMT() },
            error: function (response, type, message) {
                try {
                    //intento parsear la respesta como json.
                    var responseJson = JSON.parse(response.responseText);
                    showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
                    viewModel.TipoMT(null);
                    viewModel.LineasMT(null);
                    // Ocultar row de acciones
                    viewModel.ActivaAcciones(false);
                }
                catch (err) {
                    showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", true);
                    viewModel.TipoMT(null);
                    viewModel.LineasMT(null);
                    // Ocultar row de acciones
                    viewModel.ActivaAcciones(false);
                }
            },
            success: function (r) {
                try {
                    ko.mapping.fromJS(r, {}, viewModel.LineasMT);
                    viewModel.ActivaAcciones(false);

                    if (viewModel.CamposMT() == undefined) {
                        viewModel.CamposMT = ko.observableArray();
                    }

                    viewModel.CamposMT.removeAll();

                    //si es obligatorio, marco la linea como incluida
                    for (var i = 0; i < viewModel.LineasMT().length; i++) {
                        if (viewModel.LineasMT()[i].Obligatorio()) {
                            viewModel.LineasMT()[i].Incluido(true);
                        }

                        viewModel.CamposMTActivoLibre().forEach(function (Campo) {

                            if (viewModel.LineasMT()[i].CodCam().indexOf(Campo) != -1) {
                                // Mostrar row de acciones
                                viewModel.ActivaAcciones(true);
                                // Llenar arreglo con los campos del MT
                                viewModel.CamposMT.push(viewModel.LineasMT()[i].CodCam());
                            }
                        });
                    }

                    //Se deja el focus en el campo swift
                    setTablaEditorMaxLength();
                    $("#txtSwiftBancoReceptor").focus();

                    // Dependiendo del MT seleccionado se validarán o no los campos.
                    obtenerValidacionMonedaYMonto(viewModel.TipoMT(), function (resp) {
                        validarCamposMT = resp;
                    });

                    // Dependiendo del MT se realiza sumatoria de montos en el detalle contra el encabezado
                    obtenerCamposSumatoriaMontoTotalMT(viewModel.TipoMT());
                }
                catch (err) {
                    showAlert("Error en la operación.", "Detalles: " + r, "alert-danger", true);
                    viewModel.TipoMT(null);
                    viewModel.LineasMT(null);
                    // Ocultar row de acciones
                    viewModel.ActivaAcciones(false);
                }
            }
        });
    }
}

function pad(n, width, z) {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
}

//retorno elementos de mi modelo que no cambian, por lo tanto a la ida no me interesa convertirlos en Observables, y a la vuelta no me interesa enviarlos
function getIgnoreListParaMapping() {
    return ["TiposMT", "CaracteresError", "CaracteresError_Z"];
}

//Solicitad firmas, después de enviar un mensaje
function solicitarFirmas(model) {
    var idMensaje = model.IdMensaje;
    var monto = model.Monto;
    var tipo = model.TipoMT;
    var moneda = model.CodMonedaSW;
    var casilla = model.Casilla;

    $.ajax({
        url: urlSolicitarFirmas,
        data: { idMensaje: idMensaje, monto: monto, casilla: casilla },
        type: "POST",
        success: function (data) {
            $('#divSolFirmasSwift').html(data);
            $('#modalSolFirmasSwift').modal({ backdrop: false });
            $('#lblMensaje').text(idMensaje);
            $('#lblTipo').text(tipo);
            $('#lblMoneda').text(moneda);
            $('#lblMonto').text(numeral(monto).format("0,0.00"));
            // le decimos al modal que en caso de exito al guardar las firmas, cierre la ventana/tab (depende del navegador)
            onFirmasSuccess(function () { window.close(); });

            $("#btnCancelar").click(function () {
                window.close();
            });
        },
        error: function (xhr, status) {
            showAlert("Error en la operación.", xhr.status + " " + xhr.statusText, "alert-danger", true);
        }
    });



    //$.get(urlSolicitarFirmas, { idMensaje: idMensaje, monto: monto, casilla: casilla }, function (data) {
    //    $('#divSolFirmasSwift').html(data);
    //    $('#modalSolFirmasSwift').modal();
    //    $('#lblMensaje').text(idMensaje);
    //    $('#lblTipo').text(tipo);
    //    $('#lblMoneda').text(moneda);
    //    $('#lblMonto').text(numeral(monto).format("0,0.00"));
    //});
}

function validarCheckbox(valor) {
    return !valor();
}

function setTablaEditorMaxLength() {
    $("#panelTablaSwift").css('max-height', ($(window).height() - 310).toString() + 'px'); //que la tabla del editor ocupe todo menos los encabezados, la tabla tiene overflow-y por lo que le agregara scrollbars si es necesario
}

$(function () {

    //inicializo knockout
    var inicializarViewModel = function () {

        var mapping = {
            copy: getIgnoreListParaMapping()
        };

        var vm = ko.mapping.fromJS(modelCompleto, mapping, this);

        //extender para poder indicar nro de decimales
        ko.extenders.decimalesMoneda = function (target, opciones) {
            var result = ko.computed({
                read: function () {
                    var cantDecimales = 2;
                    if (viewModel.CodMonedaSW() != null && viewModel.CodMonedaSW() != "") {
                        //obtengo la cant de decimales de la moneda seleccionada
                        $.each(viewModel.Monedas(), function (i, item) {
                            if (item.cod_moneda_sw() == viewModel.CodMonedaSW()) {
                                cantDecimales = item.decimales();
                                return false; //para no seguir iterando
                            }
                            else {
                                return true;
                            }
                        });
                    }
                    return numeral(target()).format("0,0." + pad("", cantDecimales));
                },
                write: target
            });

            result.raw = target;
            return result;
        };

        //extender transforma todo en uppercase
        ko.extenders.uppercase = function (target, option) {
            target.subscribe(function (newValue) {
                try {
                    target(newValue().toUpperCase());
                } catch (e) {
                    target(newValue.toUpperCase());
                }
            });
            return target;
        };

        //-------------------------
        // Funciones personalizada
        //-------------------------

        vm.txtTextoEnLineas = ko.observable("");
        vm.txtMensajeErrorAgregarLinea = ko.observable("");

        // Activa de botones de acciones
        vm.ActivaAcciones = ko.observable(false);

        // Array de campos del MT
        vm.CamposMT = ko.observableArray();
        vm.selectedCamposMT = ko.observable();

        // Función abrir modal
        ko.bindingHandlers.modal = {
            init: function (element, valueAccessor) {
                $(element).modal({
                    show: false
                });

                var value = valueAccessor();
                if (typeof value === 'function') {
                    $(element).on('hide.bs.modal', function () {
                        value(false);
                    });
                }
                ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                    $(element).modal("destroy");
                });

            },
            update: function (element, valueAccessor) {
                var value = valueAccessor();
                if (ko.utils.unwrapObservable(value)) {
                    $(element).modal('show');
                } else {
                    $(element).modal('hide');
                }
            }
        }

        vm.ValidarAgregarLineas = function () {
            var totalCaracteresLineas = 0;
            // Si es nulo o no tine datos en linea, retorna cero.
            if (this.LineasMT() === null)
                return totalCaracteresLineas;
            else if (this.LineasMT().length === 0)
                return totalCaracteresLineas;

            for (var i = 0; i < this.LineasMT().length; i++) {
                if (this.LineasMT()[i].CodCam() === this.selectedCamposMT()) {
                    totalCaracteresLineas = this.LineasMT()[i].LenLinea();
                    for (var j = 0; j < this.LineasMT()[i].LineasSecundarias().length; j++) {
                        totalCaracteresLineas = totalCaracteresLineas + this.LineasMT()[i].LineasSecundarias()[j].LenLinea();
                    }
                }
            }
            return totalCaracteresLineas;
        }

        vm.showDialog = ko.observable(false);

        vm.btnAgregarLineas = function () {
            // si el texto ingresado es mayor al total de lineas, no permite continuar.
            if (this.txtTextoEnLineas().length > this.ValidarAgregarLineas()) {
                return;
            }

            // limpiar lineas
            for (var i = 0; i < this.LineasMT().length; i++) {
                if (this.LineasMT()[i].CodCam() === this.selectedCamposMT()) {
                    // si el campo no esta checkeado lo habilitamos
                    this.LineasMT()[i].Incluido(true);

                    this.LineasMT()[i].Detalle(ko.observable(""));
                    for (var j = 0; j < this.LineasMT()[i].LineasSecundarias().length; j++) {
                        this.LineasMT()[i].LineasSecundarias()[j].Detalle(ko.observable(""));
                    }
                }
            }

            var texto = this.txtTextoEnLineas();
            for (var contLineasMT = 0; contLineasMT < this.LineasMT().length; contLineasMT++) {
                if (this.LineasMT()[contLineasMT].CodCam() === this.selectedCamposMT()) {
                    var contVueltas = 0;
                    var words = separadorDePalabras(texto, ' ', this.LineasMT()[contLineasMT].LenLinea());
                    var frases = generadorDeFrases(words, this.LineasMT()[contLineasMT].LenLinea());
                    var frase = "";
                    var flagPrimeraLinea = true;

                    for (var contFrases = 0; contFrases < frases.length; contFrases++) {
                        // primera linea
                        if (contFrases == 0) {
                            this.LineasMT()[contLineasMT].Detalle(ko.observable(frases[contFrases]));
                        } else {
                            for (var contLineasSecundarias = contVueltas; contLineasSecundarias < this.LineasMT()[contLineasMT].LineasSecundarias().length; contLineasSecundarias++) {
                                this.LineasMT()[contLineasMT].LineasSecundarias()[contLineasSecundarias].Detalle(ko.observable(frases[contFrases]));
                                contVueltas++;
                                break;
                            }
                        }
                    }
                }
            }

            this.txtTextoEnLineas("");
            this.showDialog(false);
        }

        vm.btnLimpiar = function () {
            this.txtTextoEnLineas("");
        }

        vm.txtMensajeErrorAgregarLinea = ko.computed(function () {
            if (this.txtTextoEnLineas().length > this.ValidarAgregarLineas()) {
                return "Sobrepasa el límite máximo de caracteres. Ingreso un total de " + this.txtTextoEnLineas().length + " caracteres y el máximo es de " + this.ValidarAgregarLineas() + ".";
            }
            return "";
        }, this);

        //si es obligatorio, marco la linea como incluida
        for (var i = 0; i < this.LineasMT().length; i++) {
            if (this.LineasMT()[i].Obligatorio()) {
                this.LineasMT()[i].Incluido(true);
            }
        }
    };

    viewModel = new inicializarViewModel();

    var nodo = $("#divContainer").get(0);
    ko.cleanNode(nodo);
    ko.applyBindings(viewModel, nodo);

    // Si hay mensaje, lo imprime
    if (modelCompleto.Mensajes.length > 0) {
        loadMessages(modelCompleto.Mensajes);
    }

    //activo el bloquear la UI en requests asincronos
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI);

    //para todos los modales, capturo el show y seteo el height segun el tamaño de la pantalla
    $('.modal:not(.noForzarHeight)').on('show.bs.modal', function () {
        var modalBody = $(this).find('.modal-body');

        modalBody.css('overflow-y', 'auto');
        modalBody.css('max-height', $(window).height() * 0.8);
        modalBody.css('height', $(window).height() * 0.8);
    });

    $(window).on('resize', setTablaEditorMaxLength).resize();

    $("#txtSwiftBancoReceptor").focusout(buscarBancoPorSwift);
    $("#ddlTipoMT").change(changeTipoMT);
    $("#btnGrabar").click(btnGrabarClick);
    $("#btnVisualizar").click(btnVisualizarClick);
    $("#btnNuevo").click(btnNuevoClick);
    $("#btnAbrir").click(btnAbrirClick);
    $("#btnGrabarBorrador").click(btnGrabarBorradorClick);
    $("#btnGrabarPlantilla").click(btnGrabarPlantillaClick);
    $("#btnConfirmarGrabarBorrador").click(btnConfirmarGuardarBorradorClick);

    $("#btnSalir").click(function () {
        window.close();
    });

    //capturo el evento del click del chk de incluir la linea, si se esta deseleccionando la linea entonces borro lo escrito en la linea principal y las lineas secundarias de ese campo
    $(".container").on("click", ".chkIncluido", function () {
        var linea = ko.dataFor(this);
        if (!linea.Incluido()) {
            linea.Detalle("");
            if (linea.LineasSecundarias().length > 0) {
                $.each(linea.LineasSecundarias(), function (i, lineaS) {
                    lineaS.Detalle("");
                    return true; //para que el each siga iterando
                });
            }

        }
    });

    $("#ddlTipoMT").focus();


    function separadorDePalabras(texto, separador, largoPalabra) {
        var newLine = " //newline ";
        var textoSeparado = texto.replace(/\n/g, newLine).split(separador);
        var salida = [];
        for (var i = 0; i < textoSeparado.length; i++) {
            var palabra = textoSeparado[i];
            var aux = "";
            do {
                if (palabra.length > largoPalabra) {
                    aux = palabra.substring(0, largoPalabra);
                    palabra = palabra.substring(largoPalabra);
                } else {
                    aux = palabra;
                    palabra = "";
                }
                if (aux.trim() != "")
                {
                    salida.push(aux);
                }
            } while (palabra.length > 0);
        }
        return salida;
    }

    function generadorDeFrases(palabras, largoLineaFrase) {
        var auxFrase = "";
        var newLine = "//newline";
        var salida = [];
        for (var i = 0; i < palabras.length; i++) {
            var palabra = palabras[i];

            if (palabra === newLine) {
                if (auxFrase != "" && palabra != " ") {
                    salida.push(auxFrase);
                    auxFrase = "";
                }else if (auxFrase == "") {
                    salida.push(".");
                }
            } else {
                if (palabra.length + auxFrase.length < largoLineaFrase) {
                    auxFrase += palabra + " ";
                }
                else if (palabra.length + auxFrase.length == largoLineaFrase) {
                    auxFrase += palabra;
                    if (auxFrase != "") {
                        salida.push(auxFrase);
                    }
                    auxFrase = "";
                }
                else if (palabra.length + auxFrase.length > largoLineaFrase) {
                    if (auxFrase != "") {
                        salida.push(auxFrase);
                    }
                    if (palabra.length < largoLineaFrase) {
                        auxFrase = palabra + " ";
                    }
                    else {
                        auxFrase = palabra;
                    }
                }
            }
            // última vez que pasa.
            if (i == palabras.length - 1) {
                salida.push(auxFrase);
            }
        }
        return salida;
    }

});


