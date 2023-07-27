function inicializarTablas() {
    var dataIDI = {};
    var dataDec = {};

    $('#tablaDEC').bootstrapTable({
        classes: 'table table-hover',
        data: dataIDI,
        columns: GetColumnsDEC(),
        pagination: false,
        sidePagination: 'client',
        search: false,
        sortable: true,
        locale: "es-SP",
        showRefresh: false
    });


    $('#tablaIDI').bootstrapTable({
        classes: 'table table-hover',
        data: dataIDI,
        columns: GetColumnsIDI(),
        pagination: false,
        sidePagination: 'client',
        search: false,
        sortable: true,
        locale: "es-SP",
        showRefresh: false
    });
}

function validarNroOperacion() {
    if ($("#codcct").val().trim() === '') {
        showAlert("Error:", "Debe indicar el centro de costo", "alert-danger", true);
        $("#codcct").focus();
        return false;
    } else if ($("#codpro").val().trim() === '') {
        showAlert("Error:", "Debe indicar el código de producto", "alert-danger", true);
        $("#codpro").focus();
        return false;
    } else if ($("#codesp").val().trim() === '') {
        showAlert("Error:", "Debe indicar el código del especialista", "alert-danger", true);
        $("#codesp").focus();
        return false;
    } else if ($("#codofi").val().trim() === '') {
        showAlert("Error:", "Debe indicar el código de la oficina", "alert-danger", true);
        $("#codofi").focus();
        return false;
    } else if ($("#codope").val().trim() === '') {
        showAlert("Error:", "Debe indicar el código de operación", "alert-danger", true);
        $("#codope").focus();
        return false;
    }

    return true;
}

var operacionBuscada = null;
function buscarRelacionesIdiDec()
{
    if (validarNroOperacion()) {
        operacionBuscada = getParametrosBusqueda();

        $.blockUI({ message: '<h4>Buscando relaciones, por favor espere...</h4>' });
        return $.ajax({
            type: "GET",
            url: urlBuscar,
            data: operacionBuscada,
            success: function (resultado) {
                $('#tablaDEC').bootstrapTable('load', resultado.Declaraciones);
                $('#tablaIDI').bootstrapTable('load', resultado.IDIs);

                $('#badgeDeclaraciones').text(resultado.Declaraciones.length);
                $('#badgeIDIs').text(resultado.IDIs.length);

                if (resultado.Declaraciones.length > 0) {
                    $("#btnEliminar").prop("disabled", false);
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
                limpiarTablas();
            }
        });
    }
}

function eliminarRelacionesIdiDec()
{
    if (confirm("¿Confirma que desea eliminar las Declaraciones e IDI asociadas a la operación?")) {
        $.blockUI({ message: '<h4>Eliminando, por favor espere...</h4>' });
        return $.ajax({
            type: "POST",
            url: urlEliminar,
            data: operacionBuscada,
            success: function (resultado) {
                showAlert("Operación exitosa.", "Las Declaraciones e IDIs se eliminaron satisfactoriamente", "alert-success", true);
                limpiarControles();
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
}

function GetColumnsIDI() {
    return [
        { field: 'numidi', title: 'N° IDI', sortable: true },
        { field: 'fecidi', title: 'Fecha Dec.', sortable: true, formatter: fechaFormatter },
        { field: 'parrel', title: 'Par. Rel', sortable: true, formatter: paridadFormatter },
        { field: 'mtocub', title: 'Monto Cub.', sortable: true, formatter: cubFormatter }
    ];
}

function GetColumnsDEC() {
    return [
        { field: 'numdec', title: 'N° Declaración', sortable: true },
        { field: 'fecdec', title: 'Fecha Dec.', sortable: true, formatter: fechaFormatter },
        { field: 'parrel', title: 'Par. Rel', sortable: true, formatter: paridadFormatter },
        { field: 'cubfob', title: 'Cub. FOB', sortable: true, formatter: cubFormatter },
        { field: 'cubfte', title: 'Cub. Fte.', sortable: true, formatter: cubFormatter },
        { field: 'cubseg', title: 'Cub. Seg.', sortable: true, formatter: cubFormatter },
        { field: 'cubmer', title: 'Cub. Mer.', sortable: true, formatter: cubFormatter }
    ];
}

function paridadFormatter(value, row, index) {
    return numeral(value).format("0.0");
}

function cubFormatter(value, row, index) {
    return numeral(value).format("0,0.00");
}

function fechaFormatter(value, row, index) {
    if (value != null) {
        return moment(value).format("DD/MM/YYYY");
    }
}

function limpiarTodosLosErrores() {
    $(".lblMensajeError").remove();  //elimino cualquier label de error
    $("div.form-group.has-error").removeClass("has-error");
    $("#msg-zone").html("");
}

function limpiarControles()
{
    $("#codcct").val("");
    $("#codpro").val("");
    $("#codesp").val("");
    $("#codofi").val("");
    $("#codope").val("");
    limpiarTablas();
    $("#btnEliminar").prop("disabled", true);
}

function limpiarTablas()
{
    $('#badgeDeclaraciones').text("0");
    $('#badgeIDIs').text("0");
    $('#tablaDEC').bootstrapTable('removeAll');
    $('#tablaIDI').bootstrapTable('removeAll');
    
}

function pad(n, width, z) {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
}


$(function () {

    $(window).resize(function () {
        $('#tablaIDI').bootstrapTable('resetView');
        $('#tablaDEC').bootstrapTable('resetView');
    });

    $("#btnCancelar").click(function () {
        window.location = urlHome;
    });

    //hago padding de los nros de operacion
    $("#codcct").change(function () {
        $(this).val(pad($(this).val(), 3));
    });

    $("#codpro").change(function () {
        $(this).val(pad($(this).val(), 2));
    });

    $("#codesp").change(function () {
        $(this).val(pad($(this).val(), 2));
    });

    $("#codofi").focusout(function () {
        $(this).val(pad($(this).val(), 3));
    });

    $("#codope").change(function () {
        $(this).val(pad($(this).val(), 5));
    });

    $("#btnBuscar").click(buscarRelacionesIdiDec);
    $("#btnEliminar").click(eliminarRelacionesIdiDec);

    //cuando el ajax vuelve, desbloqueo el block UI si es que estaba bloqueado
    $(document).ajaxStop($.unblockUI);

    inicializarTablas();
    $(":input").inputmask();
});


