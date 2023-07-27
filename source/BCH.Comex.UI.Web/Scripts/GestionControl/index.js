var tipoUltimaBusqueda = 2; //1 = mensajes, 2 = estadisticas
var ultimosParamsConsultaSwift = null;
var formatoPOST = "DD/MM/YYYY";
var formatoGET = "YYYY/MM/DD";

function BuscarMensajesEnviadosExteriorSwif() {
    var rowSelected;
    var configColumnas = [{
        field: 'state',
        radio: true
    }, {
        field: 'id_mensaje',
        title: 'N° Mensaje',
        sortable: true,
        visible: true
    },
    {
        field: 'casilla',
        title: 'N° Casilla',
        sortable: true,
        visible: true
    }, {
        field: 'secuencia',
        title: 'Secuencia',
        sortable: true,
        visible: true
    }, {
        field: 'sesion',
        title: 'Sesión',
        sortable: true,
        visible: true
    }, {
        field: 'tipo_msg',
        title: 'Tipo',
        visible: true
    }, {
        field: 'estado_msg',
        title: 'Estado',
        visible: false
    }, {
        field: 'fecha_env',
        title: 'Fecha Recepción',
        sortable: true,
        visible: true
    }, {
        field: 'referencia',
        title: 'Referencia',
        sortable: true
    }, {
        field: 'beneficiario',
        title: 'Beneficiario',
        sortable: true
    }, {
        field: 'cod_moneda',
        title: 'Moneda',
        sortable: true
    }, {
        field: 'monto',
        title: 'Monto',
        sortable: true
    }, ];

    var direccion = 1; //enviados
    if ($('#direccionRecibidos').is(':checked')) {
        direccion = 2;
    }

    var fechaDesde = $('#dtpFechaDesde').data("DateTimePicker").date();

    if ($("#chkPeriodoFechas").is(":checked")) {
        fechaHasta = $('#dtpFechaHasta').data("DateTimePicker").date();
    }
    else fechaHasta = fechaDesde;
    $('#tablaSwifts').bootstrapTable({
        method: 'post',
        url: urlMensajesEnviadosExteriorSwift,
        queryParams: function GetParamsParaBusquedaSwift(p) {
            ultimosParamsConsultaSwift = {
                idCasilla: $('#idCasilla').val()||0,
                fechaDesde: fechaDesde.format("DD/MM/YYYY"),
                fechaHasta: fechaHasta.format("DD/MM/YYYY"),
                //direccion: direccion
            };
            return ultimosParamsConsultaSwift;
        },
        columns: configColumnas,
        height: 550,
        pagination: true,
        search: true,
        pageSize: 25,
        pageList: [10, 25, 50, 100, 200],
        clickToSelect: false,
        sortable: true,
        showFooter: true,
        locale: "es-SP",
        showExport: true,
        exportTypes: ['excel', 'txt'],
        exportDataType: 'all',
        onLoadError: function (arg1, arg2, arg3) {
            if (arg1 == "500") {
                showAlert("Error", "Demasiados registros. Contacte al administrador del sistema.", "alert-danger");
            }
        }
    });
    $('#pnlResultados').show();
}

function ReporteMensajesEnviadosExteriorSwif() {
    var p = ultimosParamsConsultaSwift;
    if (p != null) {
        //tener en cuenta que al hacer un GET, las fechas .NET las toma en invariant culture, a diferencia de un POST donde las toma con la current culture
        var urlReporte = urlReporteMensajesEnviadosExteriorSwift + "?idCasilla=" + p['idCasilla'] +
            "&fechaDesde=" + encodeURIComponent(moment(p['fechaDesde'], formatoPOST).format(formatoGET)) +
            "&fechaHasta=" + encodeURIComponent(moment(p['fechaHasta'], formatoPOST).format(formatoGET));

        var w = window.open(urlReporte, "_blank");
        return true;
    }
}

function VerEstadisticas() {

    $('#alertdiv0').hide();

    if (ValidateFilters()) {

        var columns = [
                { field: 'TipoMsg', title: 'Tipo MT', sortable: true },
                { field: 'NombreTipo', title: 'Nombre Tipo', sortable: true, footerFormatter: estadisticasTotalNombreTipoFormatter },
                { field: 'Cantidad', title: 'Cantidad', sortable: true, footerFormatter: estadisticasTotalCantidadFormatter }
        ];

        var direccion = 1; //enviados
        if ($('#direccionRecibidos').is(':checked')) {
            direccion = 2;
        }

        $('#tablaSwifts').bootstrapTable('destroy'); //ya que las columnas cambian entre busqueda de recibidos y enviados y busqueda o swifts

        var fechaDesde = $('#dtpFechaDesde').data("DateTimePicker").date();

        if ($("#chkPeriodoFechas").is(":checked")) {
            fechaHasta = $('#dtpFechaHasta').data("DateTimePicker").date();
        }
        else fechaHasta = fechaDesde;

        $('#tablaSwifts').bootstrapTable({
            method: 'post',
            url: urlEstadisticasSwifts,
            queryParams: function GetParamsParaBusquedaSwift(p) {
                ultimosParamsConsultaSwift = {
                    idCasilla: $('#idCasilla').val()||0,
                    fechaDesde: fechaDesde.format("DD/MM/YYYY"),
                    fechaHasta: fechaHasta.format("DD/MM/YYYY"),
                    direccion: direccion
                };
                return ultimosParamsConsultaSwift;
            },
            columns: columns,
            height: 550,
            pagination: true,
            search: true,
            pageSize: 25,
            pageList: [10, 25, 50, 100, 200],
            clickToSelect: false,
            sortable: true,
            showFooter: true,
            locale: "es-SP",
            showExport: true,
            exportTypes: ['excel', 'txt'],
            exportDataType: 'all',
            onLoadError: function (arg1, arg2, arg3) {
                if (arg1 == "500") {
                    showAlert("Error", "Debe elegir un rango que contenga menos registros.", "alert-danger");
                }
            }
        });
        $('#pnlResultados').show();
    }
}

function ImprimirReporteConsulta() {
    //alert("1111");
    var p = ultimosParamsConsultaSwift;
    //alert(p);
    if (p != null) {
        //tener en cuenta que al hacer un GET, las fechas .NET las toma en invariant culture, a diferencia de un POST donde las toma con la current culture
        var urlReporte = urlReporteConsulta + "?idCasilla=" + p['idCasilla'] +
            "&fechaDesde=" + encodeURIComponent(moment(p['fechaDesde'], formatoPOST).format(formatoGET)) +
            "&fechaHasta=" + encodeURIComponent(moment(p['fechaHasta'], formatoPOST).format(formatoGET)) +
            "&direccion=" + p['direccion'];

        if (p['direccion'] == 2) { //recibidos
            var ddlFiltro = $("#ddlFiltroEstado")
            var filtroCampo = $("#ddlFiltroEstado").val();
            var filtroDesc = $("#ddlFiltroEstado option:selected").text();

            if (filtroCampo != "0") {
                urlReporte += "&filtroCampo=" + filtroCampo + "&filtroDesc=" + filtroDesc;
            }
        }
        //alert(urlReporte);
        var w = window.open(urlReporte, "_blank");
        return true;
    }
}

var ultimaRowAccionIndividual = null;

function TraerDatosDeSwiftYActualizarVisorConResultado(row, urlParaEnviados, urlParaRecibidos, funcionAdicionalOnSuccess) {
    var data = null;
    var url = null;
    alert("02");
    ultimaRowAccionIndividual = row;

    if ($('#direccionRecibidos').is(':checked')) {
        data = { sesion: row['sesion'], secuencia: row['secuencia'] };
        url = urlParaRecibidos;
    }
    else {
        //enviados
        data = { idMensaje: row['id_mensaje'] };
        url = urlParaEnviados;
    }

    $.ajax({
        type: "GET",
        url: url,
        success: function (resultado) {
            $("#divCuerpoSwift").html(resultado);
            $('#modalVisorSwift').modal({ backdrop: true });

            if (funcionAdicionalOnSuccess) {
                funcionAdicionalOnSuccess();
            };
        },
        data: data,
        error: function (request, type, message) {
            alert(type + " - " + message);
        }
    });
}

function estadisticasTotalNombreTipoFormatter(data) {
    return "<b>TOTAL</b>";
}

function estadisticasTotalCantidadFormatter(data) {
    var total = 0;
    $.each(data, function (i, row) {
        total += +(row.Cantidad);
    });
    return "<b>" + total + "</b>";
}

function EjecutarBusquedaPorDefecto() {
    var rowSelected;
    var configColumnas = [{
        field: 'state',
        radio: true
    }, {
        field: 'id_mensaje',
        title: 'N° Mensaje',
        sortable: true,
        visible: true
    },
    {
        field: 'casilla',
        title: 'N° Casilla',
        sortable: true,
        visible: true
    }, {
        field: 'secuencia',
        title: 'Secuencia',
        sortable: true,
        visible: true
    }, {
        field: 'sesion',
        title: 'Sesion',
        sortable: true,
        visible: true
    }, {
        field: 'tipo_msg',
        title: 'Tipo',
        visible: true
    }, {
        field: 'estado_msg',
        title: 'Estado',
        visible: false
    }, {
        field: 'fecha_env',
        title: 'Fecha Recepción',
        sortable: true,
        visible: true
    }, {
        field: 'referencia',
        title: 'Referencia',
        sortable: true
    }, {
        field: 'beneficiario',
        title: 'Beneficiario',
        sortable: true
    }, {
        field: 'cod_moneda',
        title: 'Moneda',
        sortable: true
    }, {
        field: 'monto',
        title: 'Monto',
        sortable: true
    }, ];

    var direccion = 1; //enviados
    if ($('#direccionRecibidos').is(':checked')) {
        direccion = 2;
    }
    //$('#tablaSwifts').bootstrapTable('destroy'); //ya que las columnas cambian entre busqueda de recibidos y enviados y busqueda o swifts

    var fechaDesde = $('#dtpFechaDesde').data("DateTimePicker").date();

    if ($("#chkPeriodoFechas").is(":checked")) {
        fechaHasta = $('#dtpFechaHasta').data("DateTimePicker").date();
    }
    else fechaHasta = fechaDesde;

    $('#tablaSwifts').bootstrapTable({
        method: 'post',
        url: urlMensajesEnviadosExteriorSwift,
        queryParams: function (p) {
            ultimosParamsConsultaSwift = {
                idCasilla: 0,//$('#idCasilla').val(),
                fechaDesde: fechaDesde.format("DD/MM/YYYY"),
                fechaHasta: fechaHasta.format("DD/MM/YYYY"),
                //direccion: direccion
            };
        },
        columns: configColumnas,
        height: 550,
        pagination: true,
        search: true,
        pageSize: 25,
        pageList: [10, 25, 50, 100, 200],
        clickToSelect: false,
        sortable: true,
        showFooter: true,
        locale: "es-SP",
        showExport: true,
        exportTypes: ['excel', 'txt'],
        exportDataType: 'all',
        onLoadError: function (arg1, arg2, arg3) {
            if (arg1 == "500") {
                showAlert("Error", "Demasiados registros. Contacte al administrador del sistema.", "alert-danger");
            }
        }
    });
    $('#pnlResultados').show();
    //if (ValidateFilters(false)) {
    //    //Buscar();
    //}
    //else {
    //    //No lo molesto con validaciones ya que fue la busqueda por defecto, si no se pudo hacer no se hizo nomas
    //}
}

function InicializarFiltrosDeFecha() {
    var dateNow = moment().startOf("day").utc();

    $('#dtpFechaDesde').datetimepicker({ format: 'DD/MM/YYYY', locale: 'es', defaultDate: dateNow, maxDate: dateNow, debug: true });
    $('#dtpFechaHasta').datetimepicker({ format: 'DD/MM/YYYY', locale: 'es', defaultDate: dateNow, maxDate: dateNow, debug: true });
    $('#grpFechaHasta').hide();
    $("#dtpFechaDesde").on("dp.change", function (e) {
        var checkbox = $('#chkPeriodoFechas');
        if (!checkbox.is(':checked')) {
            $('#dtpFechaHasta').data("DateTimePicker").date(e.date);
        }

        $('#dtpFechaHasta').data("DateTimePicker").minDate(e.date);
    });
    $("#dtpFechaDesde").on("dp.error", function (e) {
        //por ahora lo ignoro
    });
    $("#dtpFechaHasta").on("dp.error", function (e) {
        //por ahora lo ignoro
    });
    $('#chkPeriodoFechas').change(function () {
        $('#grpFechaHasta').toggle();

        if ($(this).is(':checked')) {
            $('#lblFechaDesde').text("Desde");
            $('#dtpFechaHasta').data("DateTimePicker").show();
        }
        else {
            $('#lblFechaDesde').text("Fecha");
            $('#dtpFechaHasta').data("DateTimePicker").hide();
        }
    });
}

function montoFormatter(value, row, index) {
    return numeral(value).format("0,0.00");
}

function ArmarVisorMultiplesMensajes() {
    var tabla = $('#tablaSwifts');
    var selections = tabla.bootstrapTable('getAllSelections');

    if (selections.length === 0 || selections.length == 0) {
        return false;
    }
    else if (selections.length == 1) {
        //hay un solo item seleccionado, abro el visor individual en lugar del multiple
        ArmarVisorIndividual(selections[0]);
    }
    else {
        $.when.apply(null, selections.map(function (item) {
            //hago un callback ajax por cada uno de mis mensajes seleccionados;
            var data = { sesion: item['sesion'], secuencia: item['secuencia'], htmlCompleto: false };

            if (!$('#direccionRecibidos').is(':checked')) {  //enviados
                data["idMensaje"] = item['id_mensaje'];
            }

            return $.ajax({
                type: "GET",
                url: urlDetalleSwift,
                data: data,
                error: function (request, type, message) {
                    alert(type + " - " + message);
                }
            });
        })).then(function (resultadoMultiple) {
            //esto se ejecuta cuando todos los callbacks terminaron

            //reseteo los elementos del carrusel
            $("#wraperSlidesCarruselMensajes").html("");
            $("#lstCarruselIndicators").html("");

            $.each(arguments, function (index, resultado) {
                var classAdicional = "";
                var atributoCompletoClassAdicional = "";
                if (index == 0) {
                    classAdicional = " active";
                    atributoCompletoClassAdicional = "class='active'";
                }

                var itemContent = "<div class='item" + classAdicional + "'><div>" + resultado[0] + "</div></div>";
                $("#wraperSlidesCarruselMensajes").append(itemContent);
                $("#lstCarruselIndicators").append("<li data-target='#carrusel-mensajes' data-slide-to='" + index + "' " + atributoCompletoClassAdicional + "></li>");
            });
        });

        $('#carrusel-mensajes').carousel({ interval: false, wrap: false });
        $('#modalVisorMultipleSwift').modal({ backdrop: true });
    }
}

function ArmarVisorIndividual(row) {
    ultimaRowAccionIndividual = row;
    var data = { sesion: row['sesion'], secuencia: row['secuencia'], htmlCompleto: false };

    if (!$('#direccionRecibidos').is(':checked')) {  //enviados
        data["idMensaje"] = row['id_mensaje'];
    }

    $.ajax({
        type: "GET",
        url: urlDetalleSwift,
        success: function (resultado) {
            $("#divCuerpoMensajeSwift").html(resultado);
            $('#modalVisorIndividualSwift').modal({ backdrop: true });
        },
        data: data,
        error: function (request, type, message) {
            alert(type + " - " + message);
        }
    });

}

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

    //if (cmbCasilla.prop("selectedIndex") == 0) {
    //    if (showErrors) {
    //        cmbCasilla.closest(".form-group").addClass("has-error");
    //        $("#lblErrorCasilla").show();
    //    }
    //    todoOK = false;
    //}
    //else {
    //    cmbCasilla.closest(".form-group").removeClass("has-error");
    //    $("#lblErrorCasilla").hide();
    //}

    return todoOK;
}

function AccionesGridFormatter(value, row, index) {
    return [
        '<a class="verDatos accionRow" href="javascript:void(0)" title="Ver datos">',
        '<i class="glyphicon glyphicon-th-list"></i>',
        '</a>  ',
        '<a class="verLog accionRow" href="javascript:void(0)" title="Ver log">',
        '<i class="glyphicon glyphicon-info-sign"></i>',
        '</a>  ',
        '<a class="verMensaje accionRow" href="javascript:void(0)" title="Ver mensaje">',
        '<i class="glyphicon glyphicon-file"></i>',
        '</a>  ',
        /*'<a class="descargarMensaje accionRow" href="javascript:void(0)" title="DescargarMensaje">',
        '<i class="glyphicon glyphicon-save-file"></i>',*/
        '</a>  ',
        '<a class="enviarMensaje accionRow" href="javascript:void(0)" title="Enviar mensaje">',
        '<i class="glyphicon glyphicon-envelope"></i>',
        '</a>'
    ].join('');
}

function GetColumnsEnviados() {
    return [
        { checkbox: true, title: '#' },
        { formatter: AccionesGridFormatter, events: operateEvents, title: 'Acciones' },
        { field: 'SesionSecuencia', visible: false }, //oculta, para identificar unicamente los msgs
        { field: 'id_mensaje', title: 'N° mensaje', sortable: true },
        { field: 'tipo_msg', title: 'Tipo MT', sortable: true },
        { field: 'sesion', title: 'Sesion', sortable: true },
        { field: 'secuencia', title: 'Secuencia', sortable: true },
        { field: 'referencia', title: 'Referencia', sortable: true },
        { field: 'beneficiario', title: 'Beneficiario', sortable: true },
        { field: 'cod_moneda', title: 'Moneda', sortable: true },
        { field: 'monto', title: 'Monto $', sortable: true, formatter: montoFormatter, align: 'right' },
        { field: 'nombre_banco', title: 'Banco Receptor', sortable: true },
        { field: 'CodigoYBranchReceptor', title: 'Cod Banco Rec', sortable: true },
        { field: 'fecha_ingr', title: 'Fecha emisión', sortable: true },
        { field: 'hora_ingr', title: 'Hora emisión', sortable: true },
        { field: 'PrioridadDesc', title: 'Prioridad', sortable: true },
        { field: 'fecha_env', title: 'Fecha env', sortable: true },
        { field: 'hora_env', title: 'Hora env', sortable: true },
        { field: 'ciudad_banco', title: 'Ciudad Banco', sortable: true },
        { field: 'pais_banco', title: 'País Banco', sortable: true },
        { field: 'CodigoYBranEmisor', title: 'Cod Banco Em', sortable: true },
        { field: 'nombre_casilla', title: 'Unidad', sortable: true },
        { field: 'nombre_tipo', title: 'Nombre Tipo', sortable: true }
    ];
}

function GetColumnsRecibidos() {
    return [
        { checkbox: true, title: '#' },
        { formatter: AccionesGridFormatter, events: operateEvents, title: 'Acciones' },
        { field: 'SesionSecuencia', visible: false }, //oculta, para identificar unicamente los msgs
        { field: 'tipo_msg', title: 'Tipo MT', sortable: true },
        { field: 'sesion', title: 'Sesion', sortable: true },
        { field: 'secuencia', title: 'Secuencia', sortable: true },
        { field: 'referencia', title: 'Referencia', sortable: true },
        { field: 'beneficiario', title: 'Beneficiario', sortable: true },
        { field: 'cod_moneda', title: 'Moneda', sortable: true },
        { field: 'monto', title: 'Monto $', sortable: true, formatter: montoFormatter, align: 'right' },
        { field: 'nombre_banco', title: 'Banco Em', sortable: true },
        { field: 'estado_msg', title: 'Estado', sortable: true },
        { field: 'fecha_rec', title: 'Fecha Rec', sortable: true },
        { field: 'hora_rec', title: 'Hora Rec', sortable: true },
        { field: 'PrioridadDesc', title: 'Prioridad', sortable: true },
        { field: 'cod_banco_rec', title: 'Cod Banco Rec', sortable: true },
        { field: 'branch_rec', title: 'Branch Rec', sortable: true },
        { field: 'cod_banco_em', title: 'Cod Banco Em', sortable: true },
        { field: 'branch_em', title: 'Branch Em', sortable: true },
        { field: 'ciudad_banco', title: 'Ciudad Banco', sortable: true },
        { field: 'pais_banco', title: 'País Banco', sortable: true },
        { field: 'nombre_casilla', title: 'Unidad', sortable: true },
        { field: 'nombre_tipo', title: 'Nombre Tipo', sortable: true },
        { field: 'total_imp', title: 'Total Imp', sortable: true }
    ];
}

function FiltrarResultadosPorEstado() {
    var val = $(this).val();
    var tabla = $("#tablaSwifts");
    if (val != 0) {
        var that = this;
        tabla.bootstrapTable('showLoading');

        var data = tabla.bootstrapTable('getData', true);
        var cantMostrados = 0;

        var dataCumple = $.grep(data, function (item, i) {
            if (item['estado_msg'] !== val) {
                tabla.bootstrapTable('hideRow', { index: item['SesionSecuencia'], isIdField: true });
                return false;
            }
            else {
                cantMostrados++;
                tabla.bootstrapTable('showRow', { index: item['SesionSecuencia'], isIdField: true });
                return true;
            }
        });

        tabla.bootstrapTable('hideLoading');

        var msgFiltros = "Mostrando " + cantMostrados + " registros luego de refinar por estado en esta página.";

        var filteringInfo = $(".infoFiltros");
        if (filteringInfo.length == 0) {
            $('.pagination-info').after("<span class='infoFiltros'>" + msgFiltros + "</span>")
        }
        else {
            filteringInfo.text(msgFiltros);
        }
    }
    else {
        tabla.bootstrapTable('getRowsHidden', true);
        var filteringInfo = $(".infoFiltros");
        if (filteringInfo.length > 0) {
            filteringInfo.text("");
        }
    }
}

var actualizarDropDownCasillasSegunConfigurado = function () {
    var dropDown = $('#idCasilla');
    dropDown.find('option').remove().end().append('<option value="">-- Seleccione --</option>');

    $('input[name=chkCasillaVisible]:checked').each(function () {
        var strSelected = "";
        if ($('input[name=radCasillaPrincipal][value=' + $(this).val() + ']').is(':checked')) {
            strSelected = " selected";
        }
        dropDown.append("<option value='" + $(this).val() + "'" + strSelected + ">" + $(this).closest("td").siblings('#trNombreCasilla').eq(0).text() + "</option>");
    });

}
var guardarCasillas = function () {
    var casillasSeleccionadas = [];
    $('input[name=chkCasillaVisible]:checked').each(function () {
        casillasSeleccionadas.push($(this).val());
    });

    var valorCasillaDefault = "";
    var selected = $("input[type='radio'][name='radCasillaPrincipal']:checked");
    if (selected.length > 0) {
        valorCasillaDefault = selected.val();
    }

    var parametros = { idsCasillasVisibles: casillasSeleccionadas, idCasillaDefault: valorCasillaDefault };

    $.ajax({
        type: "POST",
        url: urlGuardarCasillas,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(parametros),
        dataType: "json",
        success: function () {
            $('#modalAdminCasillas').modal('hide');
            showAlert("Operación exitosa!", "Las casillas se guardaron satisfactoriamente.", "alert-success");
            actualizarDropDownCasillasSegunConfigurado();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $('#modalAdminCasillas').modal('hide');
            showAlert("Error en la operación", "Los cambios no se pudieron guardar.", "alert-danger");
        }
    });
    return false;
};

//Document ready, inicializo controles
$(function () {
    //tooltips
    $('[data-toggle="tooltip"]').tooltip();

    //filtros
    InicializarFiltrosDeFecha();
    $('#btnBuscarSwifts').click(function () {
        Buscar();
    });

    $('#btnEstadisticasSwifts').click(function () {
        VerEstadisticas();
    });
    $('#btnMensajesEnviadosExterior').click(function () {
        //alert("presiono boton buscar btnMensajesEnviadosExterio");
        BuscarMensajesEnviadosExteriorSwif();
    });
    $('#btnReporteMensajesEnviadosExterior').click(function () {
        ReporteMensajesEnviadosExteriorSwif();
    });
    $('#btnReporteConsulta').click(function () {
        ImprimirReporteConsulta();
    });
    $('#ddlFiltroEstado').change(FiltrarResultadosPorEstado);
    $('#pnlResultados').hide();

    $('#btnGuardarCasillas').click(function () {
        guardarCasillas();
    });
});

$('#dtpFechaDesde').on("dp.show", function (e) {
    $('#dtpFechaHasta').data('DateTimePicker').hide();
});
$('#dtpFechaHasta').on("dp.show", function (e) {
    $('#dtpFechaDesde').data('DateTimePicker').hide();
});

