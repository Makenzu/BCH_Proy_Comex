$(function () {
    var baseUrl = $("#base_url").val();

    //activo el bloquear la UI en requests asincronos
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI).ajaxError($.unblockUI);

    var dateNow = moment().startOf("day").utc();
    $('#dtpFechaEmision').datetimepicker({ format: 'DD/MM/YYYY', locale: 'es', focusOnShow: false, defaultDate: dateNow, maxDate: dateNow, debug: true });
    $("input[type='radio'][name='opciones']").change(ObtenerDetalleData);
    $("input[type='radio'][name='plazaPago']").change(ObtenerDetalleData);
    $('#chkTodos').click(ObtenerDetalleData);
    $('#btnActualizar').click(ObtenerDetalleData);
    $('#btnImprimir').click(ImprimirChequesSeleccionados);
    $('#NroFol').inputmask("999999", {placeholder: ""});

    $('#tableDetalle').bootstrapTable({
        classes: 'table table-hover resultRow',
        columns: [
            { checkbox: true, title: '#' },
            { field: 'operacion', title: '', sortable: false, align: 'left', width: '200px' },
			{ field: 'FecEmi', title: '', sortable: false, align: 'center', width: '120px' },
            { field: 'MonSwf', title: '', sortable: false, align: 'center', width: '80px' },
			{ field: 'MtoChq', title: '', sortable: false, align: 'right', width: '200px', formatter: montoFormatter },
            { field: 'estimp', title: '', sortable: false, align: 'center', width: '50px' },
            { field: 'NomBen', title: '', sortable: false, align: 'left', width: '300px' }
        ],       
        method: 'post',
        pagination: false,
        locale: 'es-CL',
        showHeader: false
    });

    $('#tableDetalle').on("check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table", ActualizarCantSeleccionados);

    $(document).ready(function () {
        $('#btnActualizar').click();
    });

});
function montoFormatter(value, row, index) {
    if (row['moneda'] == 'CLP') {
        return numeral(value).format("0,0");
    }
    else {
        return numeral(value).format("0,0.00");
    }
}

function ActualizarCantSeleccionados(row) {
    if (row != null) {
        var selections = $(this).bootstrapTable('getAllSelections');
        if (selections.length > 0) {
            $('#badgeCantSeleccionados').text(selections.length);
            $('#btnImprimir').prop("disabled", false);
            $('#NroFol').prop("disabled", false);

            if (selections.length == 1)
            {
                if (selections[0].NroFol > 1) {
                    $('#NroFol').val(selections[0].NroFol);
                }
            }
            return true;
        }
    }

    $('#badgeCantSeleccionados').text("0");
    $('#btnImprimir').prop("disabled", true);
    $('#NroFol').prop("disabled", true);
    return false;
}

function ObtenerDetalleData() {

    var opDocumento = $("input[type='radio'][name='opciones']:checked").val();
    var opPlazaPago = $("input[type='radio'][name='plazaPago']:checked").val();
    var fechaEmision = $('#dtpFechaEmision').data("DateTimePicker").date().format("DD/MM/YYYY");
    var todos = $('#chkTodos').is(':checked');

    $.ajax({
        url: urlBuscar,
        method: "GET",
        cache: false,
        data: {
            opDocumento: opDocumento,
            opPlazaPago: opPlazaPago,
            fechaEmision: fechaEmision,
            todos: todos
        },
        success: function (resultado) {
            $('#tableDetalle').bootstrapTable('load', resultado);
            $('#badgeDetalle').text(resultado.length);
            //$('#NroFol').val('');
            ActualizarCantSeleccionados();
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

function limpiarTablas() {
    $('#tableDetalle').bootstrapTable('removeAll');
    $('#badge').text("0");
    $('#NroFol').val('');
}

function ImprimirChequesSeleccionados() {
    var tabla = $('#tableDetalle');
    var selections = tabla.bootstrapTable('getAllSelections');
    var value = $('#NroFol').val();
    var valueTmp = value;
    var flagImp = false;
    if(selections.length === 0 || selections.length == 0){
        return false;
    }
    else 
    {
        if (value == 0) {
            showAlert("Error en la operación.", "Detalles: El Número de Folio del cheque debe ser mayor que Cero.", "alert-danger", true, null)
            return false;
        } else if (isNaN(value) || parseInt(Number(value)) != value) {
            showAlert("Error en la operación.", "Detalles: El Número de Folio debe contener solo números", "alert-danger", true, null)
            return false;
        } else if (value.length > 6) {
            showAlert("Error en la operación.", "Detalles: El Número de Folio no debe ser mayor a 6 digitos", "alert-danger", true, null)
            return false;
        }


       
            var ids = [];
            for (var i = 0; i < selections.length; i++) {
                if (selections[i].estimp == 'IMP') {
                    flagImp = true;
                }
                ids.push(selections[i].indice);
                valueTmp++;
            }
            if (flagImp) {
                if (confirm("El Cheque ya fue impreso, desea reimprimirlo?")) {
                    window.open(urlImprimirCheque + '?nroFolio=' + value + '&idsCheques=' + ids.join(","), "_blank");
                    $('#NroFol').val(valueTmp);
                    ObtenerDetalleData();
                }
            } else {
                window.open(urlImprimirCheque + '?nroFolio=' + value + '&idsCheques=' + ids.join(","), "_blank");
                $('#NroFol').val(valueTmp);
                ObtenerDetalleData();
            }
            


    }
}
