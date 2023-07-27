$(function () {
    var baseUrl = $("#base_url").val();
    var filtrosBusqueda;
    var opcionBusqueda;
    var numpre;
    var cui;
    var fechaDesde;
    var fechaHasta;
    var soloNumeros = 1;
    var sinSignos = 0;

    $(document).ajaxStart(function () {
        $("#btnBuscar").prop("disabled", "disabled");
        $.blockUI({
            message: '<h6>Cargando...</h6>',
            baseZ: 2000
        })
    }).ajaxStop(function () {
        $("#btnBuscar").removeProp("disabled");
        $.unblockUI();
    });

    ValidarCaractereskeypress("txtCUI", soloNumeros, sinSignos);

    $("#btnCancelar").click(function () { window.location.href = baseUrl; });

    $("#btnBuscar").click(function () {
        numpre = $('#NroPresentacion').val().trim();
        cui = $('#txtCUI').val().trim();

        // validaciones 
        $('#NroPresentacion').closest(".form-group").removeClass("has-error");
        $('#lblErrorNroPresentacion').hide();
        if (numpre === '') {
            $('#NroPresentacion').closest(".form-group").addClass("has-error");
            $('#NroPresentacion').focus();
            $('#lblErrorNroPresentacion').show();
            showAlert("Error en la operación.", "Detalles: Debe ingresar un número de operación.", "alert-danger", true);
            return;
        }

        $('#fgFechaDesde').removeClass("has-error");
        $('#fgFechaHasta').removeClass("has-error");
        fechaDesde = $('#txtFechaDesde').val() == '' ? null : $('#dtpFechaDesde').data("DateTimePicker").date();
        fechaHasta = $('#txtFechaHasta').val() == '' ? null : $('#dtpFechaHasta').data("DateTimePicker").date();
        if (fechaDesde != null)
            fechaDesde = new Date(fechaDesde);

        if (fechaHasta != null)
            fechaHasta = new Date(fechaHasta);
        
        if ($("#chkPeriodoFechas").is(":checked")) {
            if (fechaDesde == null) {
                showAlert("Error en la operación.", "Detalles: Si la búsqueda es por rango de fecha, campo desde es obligatorio.", "alert-danger", true);
                $('#fgFechaDesde').addClass("has-error");
                $('#txtFechaDesde').focus();
                return;
            }
            if (fechaHasta == null) {
                showAlert("Error en la operación.", "Detalles: Si la búsqueda es por rango de fecha, campo hasta es obligatorio.", "alert-danger", true);
                $('#fgFechaHasta').addClass("has-error");
                $('#txtFechaHasta').focus();
                return;
            }
        } else {
            fechaHasta = fechaDesde;
        }

        // Si llego a este punto es porque no hubo errores en la validación de campos.
        // Refresco los datos obtenidos desde el servidor
        tablaPlanillas.bootstrapTable('refresh', {
            url: urlBuscar
        });
    });

    $('#NroPresentacion').inputmask("9{1,6}[K]{1}");

    $('#NroPresentacion').keydown(function (ev) {
        $('#NroPresentacion').change();
        keyDown(ev);
    });

    $('#NroPresentacion').change(function () {
        var nropre = $('#NroPresentacion');
        if (nropre.val().trim() !== '') {
            nropre.val(('0000000' + nropre.val()).slice(-7));
        }
    });

    $('#txtCUI').change(function () {
        var cui = $('#txtCUI');
        if (cui.val().trim() !== '') {
            cui.val(('00' + cui.val()).slice(-3)); 
        }
    });

    inicializarFiltrosDeFecha();
    
    $("#chkPeriodoFechas").change(function () {
        if ($(this).is(":checked")) {
            $("#lblFechaDesde").text("Desde");
            $("#dtpFechaHasta").data("DateTimePicker").enable();
            $("#txtFechaHasta").removeProp("disabled");
            $("#fgFechaHasta").show();
        }
        else {
            $("#lblFechaDesde").text("Fecha");
            $("#dtpFechaHasta").data("DateTimePicker").disable();
            $("#txtFechaHasta").prop("disabled", true);
            $("#fgFechaHasta").hide();
        }
    });

    $("#lblFechaDesde").text("Fecha");
    $("#dtpFechaDesde").data("DateTimePicker").enable();
    $("#dtpFechaHasta").data("DateTimePicker").disable();
    $("#txtFechaDesde").removeProp("disabled");
    $("#chkPeriodoFechas").removeProp("disabled").removeProp("checked");
    $("#fgFechaHasta").hide();

    $(window).keydown(keyDown);

    var tablaPlanillas = $('#tblPlanillas');
    tablaPlanillas.bootstrapTable({
        classes: 'table table-hover resultRow',
        method: 'post',
        columns: obtieneColumnas(),
        queryParams: function (p) {
            return {
                numpre: numpre === null ? '' : numpre,
                cui: cui === null ? '' : cui,
                fechaDesde: fechaDesde,
                fechaHasta: fechaHasta
            }
        },
        pagination: true, 
        sidePagination: 'client',
        search: true,
        searchAlign: 'left',
        locale: "es-SP",
        maintainSelected: true,
        pageList: [10, 25, 50, 100, 200],
        queryParamsType: 'limit',
        rowStyle: function (row, index) {
            return { classes: 'pointer' }
        },
        onLoadError: function (codError, Error) {
            try {
                var responseJson = JSON.parse(Error.responseText);
                showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
            }
            catch (err) {
                showAlert("Error en la operación.", "Detalles: " + err.message, "alert-danger", true);
            }
        },
        onLoadSuccess: function (data) {
            tablaPlanillas.bootstrapTable('load', data);
        }
    });
    
    tablaPlanillas.on("dbl-click-row.bs.table", function (row, $element) {
        // Creo un input
        var aux = document.createElement("input");
        // Le asigno el valor del número de operación
        aux.setAttribute("value", $element.Operacion);
        // Lo asigno al documento
        document.body.appendChild(aux);
        // Selecciono ese valor
        aux.select();
        // Copia el texto seleccionado
        document.execCommand("copy");
        // Le aviso al usuario que copie el texto al portapapeles
        showAlert("Búsqueda Planillas.", "Número de operación: " + $element.Operacion + " copiado al portapapeles.", "alert-info", true);
        // Removemos el input creado para copiar el texto al portapapeles
        document.body.removeChild(aux);
    });
});

function keyDown(ev) {
    try {
        var keycode = (ev.keyCode ? ev.keyCode : ev.which);
        if (keycode === 13) {  // Presiona Enter
            if ($("#btnBuscar").is(":enabled")) {
                $("#btnBuscar").click();
            }
        }
    } catch (e) { console.log(e); }
}

function obtieneColumnas() {
    return [
        { field: 'Operacion', title: '# Operación', sortable: true },
        { field: 'Tipo', title: 'Tipo', sortable: true },
        { field: 'FechaIngreso', title: 'Fecha Ingreso', sortable: true, formatter: fechaFormatter }
    ];
}

function fechaFormatter(value, row, index) {
    if (value !== null) {
        return moment.utc(value).format("DD/MM/YYYY");
    }
}

function inicializarFiltrosDeFecha() {
    var initialDate = moment(new Date()).utc().startOf('day');
    var finalDate = moment(new Date()).utc().endOf('day');

    var dateNow = moment().startOf("day").utc();
    $('#dtpFechaDesde').datetimepicker({
        format: 'DD/MM/YYYY',
        locale: 'es',
        focusOnShow: false,
        defaultDate: initialDate,
        maxDate: finalDate,
        debug: true,
        daysOfWeekDisabled: [0, 6],
        keyBinds: {
            enter: function (ev) {
                this.hide();
                $('#txtFechaDesde').change();
                if ($("#btnBuscar").is(":enabled")) {
                    $("#btnBuscar").click();
                }
            }
        }
    });

    $('#dtpFechaHasta').datetimepicker({
        format: 'DD/MM/YYYY',
        locale: 'es',
        focusOnShow: false,
        defaultDate: initialDate,
        maxDate: finalDate,
        debug: true, daysOfWeekDisabled: [0, 6],
        keyBinds: {
            enter: function (ev) {
                this.hide();
                $('#txtFechaHasta').change();
                if ($("#btnBuscar").is(":enabled")) {
                    $("#btnBuscar").click();
                }
            }
        }
    });

    $("#dtpFechaDesde").on("dp.change", function (e) {
        // Si la fecha desde no es válida, le damos la fecha actual
        var fechaHastaAux = $('#dtpFechaHasta').data("DateTimePicker");

        if (e.date) {
            fechaHastaAux.minDate(false);
            fechaHastaAux.minDate(e.date);
        }
    });
}
