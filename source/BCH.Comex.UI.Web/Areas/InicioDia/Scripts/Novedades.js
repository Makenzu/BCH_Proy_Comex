function inicializarTabla() {
    $('#tablaNovedades').bootstrapTable({
        data: datosNovedades,
        columns: [
            { field: 'NroOperacion', title: 'Operación', sortable: true },
            { field: 'NroEspecialista', title: 'Especialista', sortable: true },
            { field: 'NombreParty', title: 'Nombre Cliente', sortable: true },
            { field: 'NemonicoMoneda', title: 'Mnd.', sortable: true },
            { field: 'mtoope', title: 'Monto', sortable: true, formatter: 'montoFormatter', align: 'right' },
            { field: 'fecven', title: 'Fec. Ref.', sortable: true, formatter: 'fechaFormatter' },
            { field: 'glosa', title: 'Tipo Novedad', sortable: true }
        ],
        pagination: false,
        search: !impresion,
        sortable: !impresion,
        locale: "es-SP",
        showRefresh: !impresion,
        clickToSelect: false,
        showExport: !impresion,
        exportTypes: ['pdf', 'excel'],
        toolbar: (impresion ? "": "toolbar")
    });
}

function isValidDate(d) {
    if (Object.prototype.toString.call(d) === "[object Date]") {
        if (isNaN(d.getTime())) {
            // date is not valid
            return false;
        }
        else {
            // date is valid
            return true;
        }
    } else {
        return false;
    }
}

function buscarNovedades() {
    var txtFecha = $('#txtFechaNovedad')
    var fecha = moment(txtFecha.data('date'), "DD/MM/YYYY");

    if (isValidDate(new Date(fecha))) {
        setValidezDeControl(txtFecha, true, "");
        $.blockUI({ message: '<h4>Buscando novedades, por favor espere...</h4>' });
        return $.ajax({
            type: "GET",
            url: urlBuscarNovedades,
            data: { fecha: fecha.format("YYYY/MM/DD") },
            success: function (resultado) {
                datosNovedades = resultado.Novedades;
                loadMessages(resultado.Mensajes);
                FiltrarResultadosPorEstado();
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
                $('#tablaNovedades').bootstrapTable('removeAll');
                $('#badgeCantResultados').text("0");
            }
        });
    }
    else
    {
        showAlert("Error en la operación.", "Detalles: La fecha no es válida", "alert-danger", true);
        setValidezDeControl(txtFecha, false, "La fecha no es válida");
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

function montoFormatter(value, row, index) {
    return numeral(value).format("0,0.00");
}

function fechaFormatter(value, row, index) {
    if (value != null) {
        var fechaStr = moment(value).format("DD/MM/YYYY");;
        if (fechaStr != '01/01/1900') {
            return fechaStr;
        }
        else return null;
    }
}

function montoCellStyle(value, row, index) {
    return {
        css: {
            "text-align": "right"
        }
    };
}

function FiltrarResultadosPorEstado() {
    var val = $("#ddlFiltroTipo").val();
    var tabla = $("#tablaNovedades");

    var strFecha = $('#txtFechaNovedad').val();

    if (datosNovedades != null) {
        var dataCumple = $.grep(datosNovedades, function (item, i) {
            if (item.estado != val) {
                return false;
            }
            else {
                return true;
            }
        });

        $('#tablaNovedades').bootstrapTable('load', dataCumple);
        if (val == 0)
        {
            $('#titleResultados').html("Novedades del día " + strFecha);
        }
        else {
            $('#titleResultados').html("Novedades anteriores sin resolver al día " + strFecha);
        }

        $('#badgeCantResultados').text(dataCumple.length);
    }
}


$(function () {

    var dateNow = moment().startOf("day").utc();
    $('#txtFechaNovedad').datetimepicker({ format: 'DD/MM/YYYY', locale: 'es', defaultDate: dateNow, maxDate: dateNow, debug: true });


    if (!impresion)
    {
        $(window).resize(function () {
            $('#tablaNovedades').bootstrapTable('resetView');
        });

        //cuando el ajax vuelve, desbloqueo el block UI si es que estaba bloqueado
        $(document).ajaxStop($.unblockUI);

        $(":input").inputmask(); //aplico el inputmask para la fecha
        $("#txtFechaNovedad").val(moment().format("DD/MM/YYYY"));
        $('#txtFechaNovedad').keypress(function (e) {
            if (e.which == '13') {
                $("#btnBuscar").click();
            }
        });
        $("#btnBuscar").click(buscarNovedades);
        $("#btnCancelar").click(function () {
            window.location = urlIndex;
        });
        $("#btnImprimirReporte").click(function () {
            var txtFecha = $('#txtFechaNovedad')
            var fecha = moment(txtFecha.data('date'), "DD/MM/YYYY");
            if (isValidDate(new Date(fecha))) {
                window.open(urlReporte + "?fecha=" + fecha.format("YYYY/MM/DD"), "_blank");
            }
            else { console.log("La fecha no es valida.") }
        });

        $('#ddlFiltroTipo').change(FiltrarResultadosPorEstado).change();
        if (typeof datosMensajes !== 'undefined' && datosMensajes != null && datosMensajes.length > 0) {
            loadMessages(datosMensajes);
        }
    }

    inicializarTabla();
});


