$(function () {

 //activo el bloquear la UI en requests asincronos
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI);

    ArmarTablaBootstrap();
    $('#BtnValidacion').click(bajaDatos_1);
    $('#BtnAceptaFrmResDesc').click(BtnFrmResDescReporte);
});

function ArmarTablaBootstrap() {
    var tablaPlanillas = $("#tablaUsuarios");
    tablaPlanillas.bootstrapTable({
        classes: "table table-hover table-condensed table-no-bordered",
        data: modelTabla,
        clickToSelect: true,
        sortable: false,
        checkboxHeader: false,
        singleSelect: true,
        search: true,
        showRefresh: true
    });

}


function ArmarTablaBootstrap_ReportePlanillas(tPlanillas) {
    var tablaPlanillas = $("#tablaReportePlanillas");
    tablaPlanillas.bootstrapTable({
        classes: "table table-hover table-condensed table-no-bordered",
        data: tPlanillas,
        clickToSelect: false,
        sortable: false,
        search: true,
        showRefresh: true
    });

}

function mostrarProblemas(data, funcionSiguiente, parametroFuncionSiguiente) {
    var baseUrl = $("#base_url").val();

    //si vienen error para desplegar
    var tieneProblemas = (data.ListaProblemas.length > 0);
    var alertPlaceholder = null;
    
    if (typeof data.ListaConfirmaciones != 'undefined' && data.ListaConfirmaciones.length > 0) {
        alertPlaceholder = "divAlertaEnFormConfirmacion"; //si muestro la pantalla de confirmacion, y ademas hay alertas, muestro las alertas dentro de la pantalla de confirmacion,
        //ya que de nada sirve mostrar las alertas por debajo si encima va a haber una pantalla modal tapándolas total o parcialmente
        
        preparaFormConfirmacion(data.ListaConfirmaciones[0].formMostrarConfirmacion);
        showConfirmMessages(data.ListaConfirmaciones[0].Confirmacion.Text, urlValidacionConfirmacion, urlValidacionindex, funcionSiguiente, data.ListaConfirmaciones[0].formMostrarConfirmacion, tieneProblemas, parametroFuncionSiguiente);
        //}
    }

    if (tieneProblemas) {
        //llamo a la funcion del layout de mostrar mensajes UI
        loadMessages(data.ListaProblemas, alertPlaceholder); //notar que paso el alertPlaceholder que corresponde, ya sea null para el placeholder de siempre o el placeholder de la pantalla de confirmacion
    }//Si viene con reportes, abre el modal para mostarlos

    if (data.ListaReportes.length > 0) {
        muestraReportes(data.ListaReportes);
        //Si el reporte, trae confirmaciones pendientes
        if (data.ListaReportes[0].formMostrarConfirmacion > 0) {
            $('#btnAceptarModal').off('click');
            $('#btnAceptarModal').click(function () {
                $("#modalVisor").modal('hide');
                preparaFormConfirmacion(data.ListaReportes[0].formMostrarConfirmacion);
                showConfirmMessages(data.ListaReportes[0].listaConfirmacion.Text, urlValidacionConfirmacion, urlValidacionindex, funcionSiguiente, data.ListaReportes[0].formMostrarConfirmacion, tieneProblemas, parametroFuncionSiguiente);
            });
        } else {
            $('#btnAceptarModal').off('click');
            $('#btnAceptarModal').click(function () {
                $("#modalVisor").modal('hide');
                showAlert("", "El Proceso de Fin de Día ha sido cancelado.", "alert-danger", false);
            });
        }
    }

    
}

function bajaDatos_1() {
    var ChkImpresionListado = $('#ChkImpresionListado_Checked').is(':checked');
    $.ajax({
        type: "POST",
        url: urlEjecuatarFinDia_1,
        data:{ChkImpresionListado: ChkImpresionListado},
        cache: false,
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
        success: function (data) {
            //Si no completo el proceso de fin dia
            if (data.hayProblemas) {
                mostrarProblemas(data,bajaDatos_2);
            } else {
                ValidaCuadraturaSwift();
            }
        }
    });
};

function bajaDatos_2() {
    $.ajax({
        type: "POST",
        url: urlEjecuatarFinDia + '2',
        cache: false,
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
        success: function (data) {
            //Si no completo el proceso de fin dia
            if (data.hayProblemas) {
                mostrarProblemas(data,bajaDatos_3);
            } else {
                bajaDatos_3();
            }
        }
    });
};

function bajaDatos_3() {
    $.ajax({
        type: "POST",
        url: urlEjecuatarFinDia + '3',
        cache: false,
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
        success: function (data) {
            //se imprimi la lista, si coresponde
            if (data.imprimirListado) {
                window.open(urlImpresionListado, "_blank");
            }
            //Si no completo el proceso de fin dia
            if (data.hayProblemas) {
                mostrarProblemas(data, bajaDatos_4);
            } else {
                bajaDatos_4()
            }
        }
    });
};

function bajaDatos_4() {
    $.ajax({
        type: "POST",
        url: urlEjecuatarFinDia + '4',
        cache: false,
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
        success: function (data) {
            //Si no completo el proceso de fin dia
            if (data.hayProblemas) {
                mostrarProblemas(data);
            } else {
                if (data.Supervisor) {
                    bajaDatos_Supervisor();
                } else {
                    bajaDatos_Especialista();
                }
            }
        }
    });
};

function bajaDatos_Especialista() {
    $.ajax({
        type: "POST",
        url: urlEjecuatarFinDia + 'Especialista_1',
        cache: false,
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
        success: function (data) {
            //Si no completo el proceso de fin dia
            if (data.hayProblemas) {
                mostrarProblemas(data, bajaDatos_Final, true);
                if (data.imprimirPorInyectar) {
                    window.open(urlImpresionPorInyectar, "_blank");
                }
            } else {
                bajaDatos_Final(false);
            }
        }
    });
};

function bajaDatos_Supervisor() {
    $.ajax({
        type: "POST",
        url: urlEjecuatarFinDia + 'Supervisor_1',
        cache: false,
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
        success: function (data) {
            //Si no completo el proceso de fin dia
            if (data.hayProblemas) {
                mostrarProblemas(data, bajaDatos_Supervisor_2);
            } else {
                if (data.mostrarFrmResDesc) {
                    llamarFRMResDesc();
                } else {
                    bajaDatos_Supervisor_2();
                }
            }
        }
    });
};

function bajaDatos_Supervisor_2() {
    $.ajax({
        type: "POST",
        url: urlEjecuatarFinDia + 'Supervisor_2',
        cache: false,
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
        success: function (data) {
            //Si no completo el proceso de fin dia
            if (data.hayProblemas) {
                mostrarProblemas(data, bajaDatos_Final, true);
                if (data.imprimirPorInyectar) {
                    window.open(urlImpresionPorInyectar, "_blank");
                }
            } else {
                bajaDatos_Final(false);
            }
        }
    });
};


function bajaDatos_Final(cierreForzado) {
    $.ajax({
        type: "POST",
        url: urlEjecuatarFinDia + 'final',
        cache: false,
        data: {cierreForzado: cierreForzado},
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
        success: function (data) {
            //Si no completo el proceso de fin dia
            if (data.hayProblemas) {
                mostrarProblemas(data);
            } else {
                loadMessages(data.ListaProblemas);
            }
        }
    });
};

function bajaDatos_5() {
    $.ajax({
        type: "POST",
        url: urlEjecuatarFinDia + '4',
        cache: false,
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
        success: function (data) {
            //Si no completo el proceso de fin dia
            if (data.hayProblemas) {
                mostrarProblemas(data);
            } else {
                loadMessages(data.ListaProblemas);
            }
        }
    });
};


function muestraReportes(Lista) {
    $.each(Lista, function (i, val) {
        $("#lblTitulo").empty().text(val.Titulo);
        if (val.tipoReporte == 0) {
            $('#divReportePlanillas').hide();
            $('#divContableErroneoSwift').show();
            $('#LblAlertaMensajeContable').text(val.Mensaje);
            $("#SeleccionValores").empty();
            $.each(val.DetallesProblemas, function (i, value) {
                $("#SeleccionValores").append("<option>" + "&nbsp;&nbsp;" + value.NroOperacion + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;" + value.NroReporte + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + value.FechaContable + "</option>");
            });
        } else if (val.tipoReporte == 1) {
            $('#divContableErroneoSwift').hide();
            $('#divReportePlanillas').show();
            ArmarTablaBootstrap_ReportePlanillas(val.DetallesProblemas);
        }
    });
    $('#divContableErroneoSwift').html();
    $('#modalContableErroneoSwift').modal();
}

var showConfirmMessages = function (messages, ActionYes, ActionNo, funcionSiguiente, tipoF, ademasTieneAlertas, parametroFuncionSiguiente) {

    if (typeof messages == "object") {
        messages = messages.Text;
    }

    var body = "";

    if (ademasTieneAlertas)
    {
        body += "<div class='row'><div class='col-xs-12'><div id='divAlertaEnFormConfirmacion'></div></div></div>";
    }
    
    body += "<div class='row'><div class='col-xs-12'><p>" + messages + "</p></div></div>";
    var confirmBtn = $("<button class='btn btn-primary pull-right'>Si</button>");
    confirmBtn.off("click");

    confirmBtn.click(function () {
        var dato = $('#txtDatoConfirmacion').val();
        $.ajax({
            type: "POST",
            url: ActionYes,
            data: { dato: dato, tipo: tipoF },
            cache: false,
            success: function (result) {
                if (result.listaMensajes.length > 0) {
                    var texto = '';
                    $.each(result.listaMensajes, function (i, val) {
                        texto = texto + val.Text;
                    });
                    $('#lbMensaje').text(texto);
                } else {
                    $("#modalVisor").modal('hide');
                    funcionSiguiente(parametroFuncionSiguiente);
                }
            }
        });
    });

    var cancelBtn = $("<button class='btn btn-default pull-right'>No</button>");
    cancelBtn.off("click");
    cancelBtn.click(function () {
        $("#modalVisor").modal('hide');
        showAlert("", "El Proceso de Fin de Día ha sido cancelado.", "alert-danger", false);
    });

    var mod = $("#modalVisor");

    mod.off("hidden.bs.modal");
    configureModal("Confirmación", body, [cancelBtn, confirmBtn]);
    mod.on("hidden.bs.modal", function () {
        $("#modalVisor").modal('hide');
    });
    mod.modal("show");
};

var configureModal = function (title, body, footer) {
    $("#modal-title").text(title);
    $("#modal-body").empty().append(body);
    $("#modal-footer").empty().append(footer);
};


function preparaFormConfirmacion(tipo) {
    //Rut
    if (tipo == 1) {
        $('#FormConfirmacion').show();
        $('#lbFormConfirmacion').text('RUT: ');
        $('#txtDatoConfirmacion').removeAttr("type");
        $('#txtDatoConfirmacion').removeAttr("maxlength");
        $('#txtDatoConfirmacion').inputmask('remove');
        $('#txtDatoConfirmacion').inputmask('(9999999K)|(99999999K)', {
            placeholder: " ",
            "oncomplete": function () {
                var Objeto = this;
                var tmpstr = "";
                var intlargo = this.value
                if (intlargo.length > 0) {
                    crut = Objeto.value
                    largo = crut.length;
                    if (largo < 2) {
                        alert('rut inválido')
                        Objeto.focus()
                        return false;
                    }
                    for (i = 0; i < crut.length ; i++)
                        if (crut.charAt(i) != ' ' && crut.charAt(i) != '.' && crut.charAt(i) != '-') {
                            tmpstr = tmpstr + crut.charAt(i);
                        }
                    rut = tmpstr;
                    crut = tmpstr;
                    largo = crut.length;

                    if (largo > 2)
                        rut = crut.substring(0, largo - 1);
                    else
                        rut = crut.charAt(0);

                    dv = crut.charAt(largo - 1);

                    if (rut == null || dv == null)
                        return 0;

                    var dvr = '0';
                    suma = 0;
                    mul = 2;

                    for (i = rut.length - 1 ; i >= 0; i--) {
                        suma = suma + rut.charAt(i) * mul;
                        if (mul == 7)
                            mul = 2;
                        else
                            mul++;
                    }

                    res = suma % 11;
                    if (res == 1)
                        dvr = 'k';
                    else if (res == 0)
                        dvr = '0';
                    else {
                        dvi = 11 - res;
                        dvr = dvi + "";
                    }

                    if (dvr != dv.toLowerCase()) {
                        showAlert("", "El RUT ingresado no es válido", "alert-danger", true, 'divAlerta');
                        Objeto.focus()
                        return false;
                    }
                    return true;
                }
            }
        });
        $('#txtDatoConfirmacion').prop('type', 'text');
    } else if (tipo == 2) {
        //clave
        $('#FormConfirmacion').show();
        $('#lbFormConfirmacion').text('Clave: ');
        $('#txtDatoConfirmacion').removeAttr("type");
        $('#txtDatoConfirmacion').removeAttr("maxlength");
        $('#txtDatoConfirmacion').inputmask('remove');
        $('#txtDatoConfirmacion').prop('type', 'password');
        $('#txtDatoConfirmacion').prop('maxlength', '9');
    } else {
        $('#FormConfirmacion').hide();
    }
}


function llamarFRMResDesc() {
    $.get(urlFrmResDesc, {}).done(function (data) {
        $('#modalReporteFrmResDesc').off("hidden.bs.modal")
        $('#modalReporteFrmResDesc').on("hidden.bs.modal", function () {
            bajaDatos_Supervisor_2();
        });
        $('#divmodalReporteFrmResDesc').html(data);
        $('#modalReporteFrmResDesc').modal();
    });
}

function BtnFrmResDescReporte() {
    window.open(urlImpresionDescuadre, "_blank");
}


//Reporte cuadratura.
function ValidaCuadraturaSwift() {
    cancelado_Proceso = false;
    VistaReporte = false;
    $.ajax({
        type: "POST",
        url: urlValidacionCuadraturaSwift,
        cache: false,
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
        success: function (data) {
            var status = data;
            if (status == false) {
                $.get(urlListadoCuadraturaSwift, {}, function (data) {
                    $('#divCuadratura').html(data);
                    $('#modalCuadratura').modal();
                });
            }
            else {
                bajaDatos_2();
            }
        }
        });
};

function ValidaClave() {
    cancelado_Proceso = true;
    $.get(urlGetPassword, {}, function (data) {
        clasup = data[0]["clasup"];
        $('#divClave').html();
        $('#modalClave').modal();
    });

};
function AceptaClave() {
    var clave = $('#TxtClave').val();
    var claveRetornada = clasup;
    clave = clave.trim();
    claveRetornada = claveRetornada.trim();

    if (clave != claveRetornada) {
        $("#ErrorClave").html("La clave no coincide.").show().fadeOut(3000);

    }
    else {
        $.get(urlSet_MODCUACC_true, {}, function (data) { });
        $("#ErrorClave").html("Procesado.").show().fadeOut(3000);
        $('#modalClave').modal('toggle');
    }
};

var Reporte = function () {
    VistaReporte = true;
};


$('#BtnValidacionCuadraturaSwift').click(ValidaCuadraturaSwift);
$('#BtnAceptaInfoCuadratura').click(ValidaClave);
$('#BtnAceptaClave').click(AceptaClave);
$('#BtnAceptaReporte').click(Reporte);

$("#modalCuadraturaSwift").on("hidden.bs.modal", function () {
    if (cancelado_Proceso == false) {
        //alert("Proceso no sigue");
    }
});


$("#modalReporteSwift").on("hidden.bs.modal", function () {
    $.get(urlValidacionCuadraturaSwift, {}, function (data) {
        var status = data;
        if (status == false) {
            $('#LblCuadraturaInfo').text("El fin de día se cancelará debido a las diferencias que existen en la cuadratura del Swift. Pero el Supervisor podra pasar por alto este Error.");
            $('#divCuadraturaErroneoSwift').html();
            $('#modalCuadraturaSwift').modal();
        }
    });
});
