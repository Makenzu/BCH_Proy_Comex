var formatoPOST = "DD/MM/YYYY";
var formatoGET = "YYYY/MM/DD"

var $tablePendientes = $('#TablaPendientes');
var $tableConfirmados = $('#TablaConfirmados');
var $tableImpresos = $('#TablaImpresos');
var $tableReenviados = $('#TablaReenviados');
var _casilla = "-1";

var dateNow = moment().startOf("day").utc();
$('#dtpFechaDesde').datetimepicker({ format: 'DD/MM/YYYY', locale: 'es', defaultDate: dateNow, maxDate: dateNow, debug: false });
$('#dtpFechaHasta').datetimepicker({ format: 'DD/MM/YYYY', locale: 'es', defaultDate: dateNow, maxDate: dateNow, debug: false });


var fechaDesde = $('#dtpFechaDesde').data("DateTimePicker").date()
var fechaHasta = $('#dtpFechaHasta').data("DateTimePicker").date()

$("body").keyup(function (e) {
    if (e.keyCode == 13) buscarInforme();
    //Click it again to slide up back up, right?
});

/*Seteando Variables al inicializar controller*/

$('#grpFechaHasta').hide();
$("#chkPeriodoFechas").prop('checked', false);

$("#lblMensaje").text("Total Mensajes Pendientes")
$("#divPendientes").show();
$("#divConfirmados").hide();
$("#divImpresos").hide();
$("#divReenviados").hide();



/*Fin */


$(window).resize(function () {
    $tablePendientes.bootstrapTable('resetView', {
        height: getHeight()
    });
    $tableConfirmados.bootstrapTable('resetView', {
        height: getHeight()
    });
    $tableImpresos.bootstrapTable('resetView', {
        height: getHeight()
    });
    $tableReenviados.bootstrapTable('resetView', {
        height: getHeight()
    })
});



$tablePendientes.bootstrapTable({
    height: getHeight(),
    onLoadSuccess: function (data) {
        $('#badgeCantResultados').text(data.length);
    },
    method: 'post',
    url: $('#urlMensajePendientes').val(),
    queryParams: function (p) {
        return {
            casilla: _casilla,
            fechaDesde: fechaDesde.format("YYYY/MM/DD"),
            fechaHasta: fechaHasta.format("YYYY/MM/DD"),
        };
    },
    clickToSelect: true,
    search: true,
    checkboxHeader: false,
    pagination: true,
    pageSize: 10,
    pageList: [10, 25, 50, 100, 200],
    cache: false
});


$tableConfirmados.bootstrapTable({
    height: getHeight(),
    onLoadSuccess: function (data) {
        $('#badgeCantResultados').text(data.length);
    },
    method: 'post',
    url: $('#urlMensajeConfirmados').val(),
    queryParams: function (p) {
        return {
            casilla: _casilla,
            fechaDesde: fechaDesde.format("YYYY/MM/DD"),
            fechaHasta: fechaHasta.format("YYYY/MM/DD"),
        };
    },
    clickToSelect: true,
    search: true,
    checkboxHeader: false,
    pagination: true,
    pageSize: 25,
    pageList: [10, 25, 50, 100, 200],
    cache: false
});

$tableImpresos.bootstrapTable({
    height: getHeight(),
    onLoadSuccess: function (data) {
        $('#badgeCantResultados').text(data.length);
    },
    method: 'post',
    url: $('#urlMensajeImpresos').val(),
    queryParams: function (p) {
        return {
            casilla: _casilla,
            fechaDesde: fechaDesde.format("YYYY/MM/DD"),
            fechaHasta: fechaHasta.format("YYYY/MM/DD"),
        };
    },
    clickToSelect: true,
    search: true,
    checkboxHeader: false,
    pagination: true,
    pageSize: 25,
    pageList: [10, 25, 50, 100, 200],
    cache: false
});


$tableReenviados.bootstrapTable({
    height: getHeight(),
    onLoadSuccess: function (data) {
        $('#badgeCantResultados').text(data.length);
    },
    method: 'post',
    url: $('#urlMensajeReenviados').val(),
    queryParams: function (p) {
        return {
            casilla: _casilla,
            fechaDesde: fechaDesde.format("YYYY/MM/DD"),
            fechaHasta: fechaHasta.format("YYYY/MM/DD"),
        };
    },
    clickToSelect: true,
    search: true,
    checkboxHeader: false,
    pagination: true,
    pageSize: 10,
    pageList: [10, 25, 50, 100, 200],
    cache: false
});


function buscarInforme() {

    if (ValidateFilters(true)) {
        _casilla = $('#idCasilla').val();
        var nameCasilla = $('#idCasilla option:selected').text();
        fechaDesde = $('#dtpFechaDesde').data("DateTimePicker").date();
        fechaHasta;
        if ($("#chkPeriodoFechas").is(":checked")) {
            fechaHasta = $('#dtpFechaHasta').data("DateTimePicker").date();
        }
        else {
            fechaHasta = fechaDesde;
        }

        var valueOpcion = $('#TipoContainer input:checked').val();

        if (valueOpcion == 0) //Pendientes
        {
            $tablePendientes.bootstrapTable('refresh');
        }
        else if (valueOpcion == 1) //Confirmados
        {
            $tableConfirmados.bootstrapTable('refresh');
        }
        else if (valueOpcion == 2) //Impresos
        {
            $tableImpresos.bootstrapTable('refresh');
        }
        else if (valueOpcion == 3) //Reenviados
        {
            $tableReenviados.bootstrapTable('refresh');
        }
    }
}


$('#TipoContainer input').change(function () {
    var valueOpcion = $('#TipoContainer input:checked').val();

    if (valueOpcion == 0) //Pendientes
    {
        $("#lblMensaje").text("Total Mensajes Pendientes");
        $('#badgeCantResultados').text("0");
        $("#divPendientes").show();
        $("#divConfirmados").hide();
        $("#divImpresos").hide();
        $("#divReenviados").hide();
        $tableConfirmados.bootstrapTable('removeAll');
        $tableImpresos.bootstrapTable('removeAll');
        $tableReenviados.bootstrapTable('removeAll');


    }
    if (valueOpcion == 1) { //Confirmados
        $("#lblMensaje").text("Total Mensajes Confirmados");
        $('#badgeCantResultados').text("0");
        $("#divPendientes").hide();
        $("#divConfirmados").show();
        $("#divImpresos").hide();
        $("#divReenviados").hide();
        $tablePendientes.bootstrapTable('removeAll');
        $tableImpresos.bootstrapTable('removeAll');
        $tableReenviados.bootstrapTable('removeAll');

    }
    if (valueOpcion == 2) { //Impresos
        $("#lblMensaje").text("Total Mensajes Impresos");
        $('#badgeCantResultados').text("0");
        $("#divPendientes").hide();
        $("#divConfirmados").hide();
        $("#divImpresos").show();
        $("#divReenviados").hide();
        $tablePendientes.bootstrapTable('removeAll');
        $tableConfirmados.bootstrapTable('removeAll');
        $tableReenviados.bootstrapTable('removeAll');

    }
    if (valueOpcion == 3) { //Reenviados

        $("#lblMensaje").text("Total Mensajes Reenviados");
        $('#badgeCantResultados').text("0");
        $("#divPendientes").hide();
        $("#divConfirmados").hide();
        $("#divImpresos").hide();
        $("#divReenviados").show();
        $tablePendientes.bootstrapTable('removeAll');
        $tableImpresos.bootstrapTable('removeAll');
        $tableConfirmados.bootstrapTable('removeAll');

    }


});


function getHeight() {
    return $(window).height() - $('h1').outerHeight(true);
}


//(jfm 2015-08-24) solo mostramos el filtro en caso de que exista información
$('#TablaPendientes').on("load-success.bs.table", function (e, data) {
    if (data.length > 0) {
        //(jfm 2015-08-25) cerramos panel filtro
        $('.panel-heading span.clickable').click();
        $('.panel div.clickable').click();
    }
});

//(jfm 2015-10-07) capturar evento de cambio en seleccion de periodo de fechas
$('#chkPeriodoFechas').change(function () {
    $('#grpFechaHasta').toggle();
    if ($(this).is(':checked')) {
        $('#lblFechaDesde').text("Desde");
        $('#dtpFechaHasta').data("DateTimePicker").show();
    } else {
        $('#lblFechaDesde').text("Fecha");
        $('#dtpFechaHasta').data("DateTimePicker").hide();
    }
});


function ValidateFilters(showErrors) {
    var todoOK = true;

    var cmbCasilla = $('#idCasilla');
    var dtpFechaDesde = $('#dtpFechaDesde');
    var dtpFechaHasta = $('#dtpFechaHasta');

    dtpFechaHasta.closest(".form-group").removeClass("has-error");
    $("#lblErrorFechaHasta").hide();

    var fechaDesde = dtpFechaDesde.data("DateTimePicker").date();
    if (fechaDesde == null) {
        if (showErrors) {
            dtpFechaDesde.closest(".form-group").addClass("has-error");
            $("#lblErrorFechaDesde").show();
        }
    }
    else {
        dtpFechaDesde.closest(".form-group").removeClass("has-error");
        $("#lblErrorFechaDesde").hide();


        if ($("#chkPeriodoFechas").is(":checked")) {

            fechaHasta = dtpFechaHasta.data("DateTimePicker").date();

            if (fechaHasta < fechaDesde) {
                todoOK = false;

                if (showErrors) {
                    dtpFechaHasta.closest(".form-group").addClass("has-error");
                    $("#lblErrorFechaHasta").show();
                }
            }
        }
    }

    if (cmbCasilla.prop("selectedIndex") == 0) {
        if (showErrors) {
            cmbCasilla.closest(".form-group").addClass("has-error");
            $("#lblErrorCasilla").show();
        }
        todoOK = false;
    }
    else {
        cmbCasilla.closest(".form-group").removeClass("has-error");
        $("#lblErrorCasilla").hide();
    }

    return todoOK;
}


