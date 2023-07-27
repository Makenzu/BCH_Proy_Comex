
$(document).ready(function () {
    if (modelCompleto.EsNuevo || modelCompleto.EsModificacion) {
        if (modelCompleto.EsNuevo) {
            $("#Mensaje_reference").removeAttr("Readonly");
            $("#Mensaje_campo22C").removeAttr("Readonly");
            $("#Mensaje_campo82A").removeAttr("Readonly");
            $("#Mensaje_campo87A").removeAttr("Readonly");
            $("#Mensaje_bookedBy").removeAttr("Readonly");
            $("#Mensaje_valueDate").removeAttr("Readonly");
            $("#Mensaje_rate").removeAttr("Readonly");
            $("#Mensaje_campo32B").removeAttr("Readonly");
            $("#Mensaje_campo33B").removeAttr("Readonly");
            $("#Mensaje_campo53A").removeAttr("Readonly");
            $("#Mensaje_campo57A").removeAttr("Readonly");
        }
        else if (modelCompleto.EsModificacion) {
            $("#Mensaje_bookedBy").removeAttr("Readonly");
            $("#Mensaje_valueDate").removeAttr("Readonly");
            $("#Mensaje_rate").removeAttr("Readonly");
            $("#Mensaje_campo32B").removeAttr("Readonly");
            $("#Mensaje_campo33B").removeAttr("Readonly");
        }

        $("#Mensaje_reference").change(changeReference);
        $("#Mensaje_campo22C").change(function () { toUppercase(this) }).change();
        $("#Mensaje_campo82A").change(changeCampo82A);
        $("#Mensaje_campo87A").change(changeCampo87A);
        $("#Mensaje_bookedBy").change(changeBookedBy);
        $("#Mensaje_valueDate").change(changeValueDate);
        $("#Mensaje_rate").change(changeRate).change();
        $("#Mensaje_campo32B").change(changeCampo32B);
        $("#Mensaje_campo33B").change(changeCampo33B);
        $("#Mensaje_campo53A").change(function () { toUppercase(this) }).change();
        $("#Mensaje_campo57A").change(function () { toUppercase(this) }).change();

        $('#formMensaje').change(validarForm).change();
    }
});

function validarForm() {
    if (modelCompleto.EsNuevo || modelCompleto.EsModificacion) {
        var form = $('#formMensaje');

        if ($('#formMensaje').valid()) {
            $('#btnSubmit').removeAttr('disabled');
        } else {
            $('#btnSubmit').attr('disabled', 'disabled');
        }
    }
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

function changeReference() {
    if (toUppercase(this)) { //evaluo el resultado del toUppercase, dado que me dispara el change, si no lo evaluo entonces entro en loop infinito
        if ($(this).val() == "") {
            setValidezDeControl($(this), true, null);
        }
        else {
            $.ajax({
                type: "GET",
                cache: false,
                url: urlValidarReferencia,
                data: { referencia: $(this).val() },
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
                success: function (resultados) {
                    var algunoEnError = false;
                    $.each(resultados, function (i, resultado) {
                        var esValido = !resultado['IsError'];
                        setValidezDeControl(
                            $("#" + resultado['ControlName']),
                            esValido,
                            resultado['Text']);

                        if (!esValido) {
                            algunoEnError = true;
                        }
                        return true; //para que el each siga iterando
                    });
                }
            });
        }
    }
}

function changeValueDate() {
    if (toUppercase(this)) { //evaluo el resultado del toUppercase, dado que me dispara el change, si no lo evaluo entonces entro en loop infinito
        if ($(this).val() == "") {
            setValidezDeControl($(this), true, null);
        }
        else {
            $.ajax({
                type: "GET",
                cache: false,
                url: urlValidarFechaYYYYMMDD,
                data: { fecha: $(this).val() },
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
                success: function (resultados) {
                    var algunoEnError = false;
                    $.each(resultados, function (i, resultado) {
                        var esValido = !resultado['IsError'];
                        setValidezDeControl(
                            $("#Mensaje_valueDate"),
                            esValido,
                            resultado['Text']);

                        if (!esValido) {
                            algunoEnError = true;
                        }
                        return true; //para que el each siga iterando
                    });
                }
            });
        }
    }
}

function changeBookedBy() {
    if (toUppercase(this)) { //evaluo el resultado del toUppercase, dado que me dispara el change, si no lo evaluo entonces entro en loop infinito
        if ($(this).val() == "") {
            setValidezDeControl($(this), true, null);
        }
        else {
            var val30T = $(this).val();
            var val98Dhrs = "000000";
            if ($("#Mensaje_campo98D").val().length == 14) {
                val98Dhrs = $("#Mensaje_campo98D").val().substr(-6);
            }
            $.ajax({
                type: "GET",
                cache: false,
                url: urlValidarFechaYYYYMMDD,
                data: { fecha: $(this).val() },
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
                success: function (resultados) {
                    var algunoEnError = false;
                    $.each(resultados, function (i, resultado) {
                        var esValido = !resultado['IsError'];
                        setValidezDeControl(
                            $("#Mensaje_bookedBy"),
                            esValido,
                            resultado['Text']);

                        if (!esValido) {
                            algunoEnError = true;
                        } else {
                            $("#Mensaje_campo98D").val(val30T + val98Dhrs);
                        }
                        return true; //para que el each siga iterando
                    });
                }
            });
        }
    }
}

function changeCampo32B() {
    if (toUppercase(this)) { //evaluo el resultado del toUppercase, dado que me dispara el change, si no lo evaluo entonces entro en loop infinito
        if ($(this).val() == "") {
            setValidezDeControl($(this), true, null);
        }
        else {
            var v = $(this).val();
            $(this).val(v.replace('.', ''));

            $.ajax({
                type: "GET",
                cache: false,
                url: urlValidarCampoMonto,
                data: { monedaValor: $(this).val() },
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
                success: function (resultados) {
                    var algunoEnError = false;
                    $.each(resultados, function (i, resultado) {
                        var esValido = !resultado['IsError'];
                        setValidezDeControl(
                            $("#Mensaje_campo32B"),
                            esValido,
                            resultado['Text']);

                        if (!esValido) {
                            algunoEnError = true;
                        }
                        return true; //para que el each siga iterando
                    });
                }
            });
        }
    }
}

function changeCampo33B() {
    if (toUppercase(this)) { //evaluo el resultado del toUppercase, dado que me dispara el change, si no lo evaluo entonces entro en loop infinito
        if ($(this).val() == "") {
            setValidezDeControl($(this), true, null);
        }
        else {
            var v = $(this).val();
            $(this).val(v.replace('.', ''));

            $.ajax({
                type: "GET",
                cache: false,
                url: urlValidarCampoMonto,
                data: { monedaValor: $(this).val() },
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
                success: function (resultados) {
                    var algunoEnError = false;
                    $.each(resultados, function (i, resultado) {
                        var esValido = !resultado['IsError'];
                        setValidezDeControl(
                            $("#Mensaje_campo33B"),
                            esValido,
                            resultado['Text']);

                        if (!esValido) {
                            algunoEnError = true;
                        }
                        return true; //para que el each siga iterando
                    });
                }
            });
        }
    }
}

function changeCampo82A() {
    if (toUppercase(this)) { //evaluo el resultado del toUppercase, dado que me dispara el change, si no lo evaluo entonces entro en loop infinito
        if ($(this).val() == "") {
            setValidezDeControl($(this), true, null);
        }
        else {
            var v = $(this).val();
            //if (v.length !== 8 && v.length !== 11) {
            //    setValidezDeControl($(this), false, "Debe ingresar un BIC válido");
            //}
            if (v.length < 8) {
                setValidezDeControl($(this), false, "El campo debe tener al menos 8 caracteres");
            }
            else {
                setValidezDeControl($(this), true, null);
            }
            //if ($(this).val().length === 8) {
            //    $(this).val($(this).val() + "XXX");
            //}
        }
    }
}

function changeCampo87A() {
    if (toUppercase(this)) { //evaluo el resultado del toUppercase, dado que me dispara el change, si no lo evaluo entonces entro en loop infinito
        if ($(this).val() == "") {
            setValidezDeControl($(this), true, null);
        }
        else {
            var v = $(this).val();
            if (v.length !== 8 && v.length !== 11) {
                setValidezDeControl($(this), false, "Debe ingresar un BIC válido");
            }
            else if (v.length === 8) {
                $(this).val($(this).val() + "XXX");
                setValidezDeControl($(this), true, null);
            }
            else {
                setValidezDeControl($(this), true, null);
            }
        }
    }
}

function changeRate() {
    if ($(this).val() == "") {
        setValidezDeControl($(this), true, null);
    }
    else {
        var v = $(this).val();
        $(this).val(v.replace('.', ''));

        $.ajax({
            type: "GET",
            cache: false,
            url: urlValidarRate,
            data: { rate: $(this).val() },
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
            success: function (resultados) {
                var algunoEnError = false;
                $.each(resultados, function (i, resultado) {
                    var esValido = !resultado['IsError'];
                    setValidezDeControl(
                        $("#" + resultado['ControlName']),
                        esValido,
                        resultado['Text']);

                    if (!esValido) {
                        algunoEnError = true;
                    }
                    return true; //para que el each siga iterando
                });
            }
        });
    }
}