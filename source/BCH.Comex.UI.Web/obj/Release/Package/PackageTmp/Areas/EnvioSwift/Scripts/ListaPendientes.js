function ArmarTablaBootstrap(modelLista) {
    var tablaSwift = $("#tablaSwift");
    tablaSwift.bootstrapTable('destroy');
    tablaSwift.bootstrapTable({
        classes: "table table-hover table-condensed table-no-bordered",
        data: modelLista,
        clickToSelect: true,
        singleSelect: true,
        sortable: false,
        uniqueId: 'archivo',
        cache: false,
        height: screen.height > 1000 ? 550 : 280,
        pagination: true,
        search: true,
        showRefresh: true,
        pageSize: 25,
        pageList: [10, 25, 50, 100, 200],
    });

    tablaSwift.on("check.bs.table uncheck.bs.table", tablaSelectionChanged);
    $('#modalBuscarSwift').modal({ backdrop: true });
}

var esSupervisor = false;
var codUsr = 0;
function inicializarListaPendientes(modelLista, unCodUsr, esSup) {
    codUsr = unCodUsr;
    esSupervisor = esSup;
    ArmarTablaBootstrap(modelLista);
    $('#BtnEditar').on('click', editarMensaje);
    $('#BtnEliminar').on('click', eliminarMensaje);
    $('#divErroneoEnvioSwift').hide();
}

function tablaSelectionChanged()
{
    var sel = $("#tablaSwift").bootstrapTable('getSelections');
    if (sel.length == 0)
    {
        $('#BtnEliminar').hide();
        $('#BtnEditar').hide();
    }
    else {
        $('#BtnEditar').show();

        var tienePermisos = (sel[0].codesp == codUsr || esSupervisor);
        if (!tienePermisos) {
            $('#BtnEliminar').hide();
        }
        else {
            $('#BtnEliminar').show();
        }
    }
}

function editarMensaje() {
    var sel = $("#tablaSwift").bootstrapTable('getSelections');

    if (sel.length == 0) {
        //showAlert("", "Es necesario seleccionar un mensaje para editar", "alert-danger", false);
        $('#divErroneoEnvioSwift').show();
        $('#LblAlertaMensaje').text("Es necesario seleccionar un mensaje para editar.");

    } else {
        $('#modalBuscarSwift').modal('hide');
        //window.open(urleditarSwiftPendiente + '?archivoEditar=' + sel[0].archivo, "_blank");
        return $.ajax({
            type: "POST",
            cache: false,
            url: urleditarSwiftPendiente,
            data: { ctecct: sel[0].ctecct, codesp: sel[0].codesp, archivoEditar: sel[0].archivo },
            success: function (result) {
                ko.mapping.fromJS(result.Model, {}, viewModel);
                if (result.Model.Mensajes.length > 0) {
                    loadMessages(result.Model.Mensajes);
                }
            },
            error: function () { }
        });
    }
}

function eliminarMensaje() {
    var sel = $("#tablaSwift").bootstrapTable('getSelections');
    if (sel.length == 0) {
        //showAlert("", "Es necesario seleccionar un mensaje para eliminar", "alert-danger", false);
        $('#divErroneoEnvioSwift').show();
        $('#LblAlertaMensaje').text("Es necesario seleccionar un mensaje para eliminar.");
    } else {
        $.ajax({
            type: "POST",
            cache: false,
            url: urleliminarSwfitPendiente,
            data: { archivoEliminar: sel[0].archivo},
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
            success: function (resultado) {
                if (resultado.estado == true)
                    $("#tablaSwift").bootstrapTable('removeByUniqueId', sel[0].archivo);
                else
                    showAlert("Error en la operación.", "Detalles: " + resultado.message, "alert-danger", false);
            }
        });
    }
}

function montoFormatter(value, row, index) {
    return numeral(value).format("0,0.00");
}

function booleanFormatter(value, row, index) {
    if (value != null) {
        if (value) return "Si";
        else return "No";
    }
    else return "No";
}

function fechaFormatter(value, row, index) {
    if (value != null) {
        var fechaStr = moment(value).format("DD/MM/YYYY HH:mm:ss");;
        if (fechaStr != '01/01/1900') {
            return fechaStr;
        }
        else return null;
    }
}
