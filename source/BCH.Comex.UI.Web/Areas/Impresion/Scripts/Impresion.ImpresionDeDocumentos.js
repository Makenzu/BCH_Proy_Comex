var esPrimera = true;
var _nroReporte = 0;


function BuscarDocsParaImprimir() {
    if (ValidateFilters(true)) {
        if (esPrimera) {
            var tablaDocs = $('#tablaDocs');
            tablaDocs.bootstrapTable({
                classes: 'table table-hover resultRow',
                method: 'post',
                url: urlCargarDatos,
                queryParams: function (p) {
                    return GetParametrosBusqueda();
                },
                columns: GetColumns(),
                pagination: false,
                sidePagination: 'client',
                search: true,
                searchAlign: 'left',
                clickToSelect: false,
                sortable: true,
                sortName: "NroOperacion",
                locale: "es-SP",
                maintainSelected: true,
                showRefresh: true,
                toolbar: "#toolbar",
                onLoadError: function (status, message, type) {
                    try {
                        ActualizarCantidadDocumentos();
                        ActualizarCantSeleccionados();
                        $('#tablaDocs').bootstrapTable('removeAll');

                        //intento parsear la respuesta como json.
                        var responseJson = JSON.parse(message.responseText);
                        showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
                        return false;
                    }
                    catch (err) {
                        showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", true);
                    }                   
                },
            });

            tablaDocs.on("load-success.bs.table", BusquedaOnSuccess);
            tablaDocs.on("check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table", ActualizarCantSeleccionados);
            esPrimera = false;
        }
        else {
            $('#tablaDocs').bootstrapTable('refresh');
        }
    }
}

function GetParametrosBusqueda()
{
    var parametros = {};

    var valorFiltro;
    var filtroSeleccionado = $("input[type='radio'][name='radTipoFiltro']:checked");
    if (filtroSeleccionado.length > 0) {
        switch (filtroSeleccionado.attr('id')) {
            case "radFiltroFecha":
                parametros["fechaOperacion"] = $('#dtpFechaOperacion').data("DateTimePicker").date().format("DD/MM/YYYY");
                break;

            case "radFiltroNroOperacion":
                parametros["codcct"] = $("#codcct").val();
                parametros["codpro"] = $("#codpro").val();
                parametros["codesp"] = $("#codesp").val();
                parametros["codofi"] = $("#codofi").val();
                parametros["codope"] = $("#codope").val();
                break;

            case "radFiltroCrfNumber":
                parametros["contactReference"] = $("#crfNumber").val();
                break;
        }
    }

    return parametros;
}

function GetColumns() {
    return [
        { checkbox: true, title: '#' },
        { title: 'Acciones', formatter: AccionesGridFormatter, events: operateEvents },
        { field: 'NroOperacion', title: '# Operación', sortable: true },
        { field: 'TipoDocDesc', title: 'Tipo', sortable: true },
        { field: 'FechaOperacion', title: 'Fecha', sortable: true, formatter: fechaFormatter },
        { field: 'DescripcionDoc', title: 'Documento', sortable: true },
        { field: 'DescripcionProducto', title: 'Producto', sortable: true }
    ];
}

function AccionesGridFormatter(value, row, index) {
    var htmlImprimir = '<a class="imprimirDoc accionRow" href="javascript:void(0)" title="Imprimir"><i class="glyphicon glyphicon-print"></i></a>  ';
    var htmlVer = "";

    var tipoDoc = row['TipoDoc'];

    if (tipoDoc == 1) { //Carta
        htmlVer = '<a class="verCarta accionRow" href="javascript:void(0)" title="Ver Carta"><i class="glyphicon glyphicon-file"></i></a>';
    }
    else if (tipoDoc == 2) { //swift
        htmlVer = '<a class="verSwift accionRow" href="javascript:void(0)" title="Ver Swift"><i class="glyphicon glyphicon-list-alt"></i></a>';
    }
    else if (tipoDoc == 3) { //Contabilidad
        htmlVer = '<a class="verContabilidad accionRow" href="javascript:void(0)" title="Ver reporte"><i class="glyphicon glyphicon-usd"></i></a>';
    }

    return htmlImprimir + htmlVer;
}

var paramsUltimaAccion = null;
window.operateEvents = {
    'click .imprimirDoc': function (e, value, row, index) {
        ImprimirRowDeTabla(row);
    },
    'click .verCarta': function (e, value, row, index) {
        paramsUltimaAccion = GetParamsParaCarta(row);
        paramsUltimaAccion.format = 'HTML';
        paramsUltimaAccion.filename = row['NroOperacion'].replace(/-/g, '');
        TraerDatosYActualizarVisorConResultado(paramsUltimaAccion, urlVerDocumento, null);
    },
    'click .verSwift': function (e, value, row, index) {
        paramsUltimaAccion = GetParamsParaMensajeSwift(row);
        paramsUltimaAccion.format = 'HTML';
        paramsUltimaAccion.filename = row['NroOperacion'];
        TraerDatosYActualizarVisorConResultado(paramsUltimaAccion, urlVerDetalleMensajeSwift, null);
    },
    'click .verContabilidad': function (e, value, row, index) {
        paramsUltimaAccion = GetParamsParaContabilidad(row);
        paramsUltimaAccion.format = 'HTML';
        paramsUltimaAccion.filename = row['NroOperacion'].replace(/-/g, '');
        TraerDatosYActualizarVisorConResultado(paramsUltimaAccion, urlVerReporteContable, null);
    }
};

function GetParamsParaMensajeSwift(row) {
    return {
        nroMensaje: row['CodigoPropio'],
        nroReporte: _nroReporte,
        fechaOp: moment.utc(row['FechaOperacion']).format("YYYY/MM/DD"),
        url: urlVerDetalleMensajeSwift
    };
}

function GetParamsParaContabilidad(row) {
    _nroReporte = row['NroRpt'];
    return { nroReporte: row['NroRpt'], fechaOp: moment.utc(row['FechaOperacion']).format("YYYY/MM/DD"), url: urlVerReporteContable };
}

function GetParamsParaCarta(row) {
    var nroOperacion = row['NroOperacion'].replace(/-/g, ''); //remuevo todos los guiones
    return { numeroOperacion: nroOperacion, codDocumento: row['CodigoPropio'], nroCorrelativo: row['NroRpt'], url: urlVerDocumento };
}

function ObtenerRequestRowDeTabla(row) {
    var tipoDoc = row['TipoDoc'];
    var result = {};

    if (tipoDoc == 1) { //Carta
        result.type = "Carta";
        result.carta = GetParamsParaCarta(row);
    }
    else if (tipoDoc == 2) { //swift
        result.type = "Swift"
        result.swift = GetParamsParaMensajeSwift(row);
    }
    else if (tipoDoc == 3) { //Contabilidad
        result.type = "Contabilidad";
        result.contabilidad = GetParamsParaContabilidad(row);
    }
    return result;
}

function ImprimirRowDeTabla(row)
{
    var tipoDoc = row['TipoDoc'];
    var paramsAccion = null;

    if (tipoDoc == 1) { //Carta
        paramsAccion = GetParamsParaCarta(row);
    }
    else if (tipoDoc == 2) { //swift
        paramsAccion = GetParamsParaMensajeSwift(row);
    }
    else if (tipoDoc == 3) { //Contabilidad
        paramsAccion = GetParamsParaContabilidad(row);
    }

    if (paramsAccion != null)
        paramsAccion.filename = row['NroOperacion'].replace(/-/g, '');

    //var filename = row['NroOperacion'];

    ImprimirDocumento(paramsAccion)
}

function ImprimirDocumento(paramsAccion) {
    if (paramsAccion != null) {
        var url = paramsAccion.url;
        var location = "";
        if (url===urlVerDocumento) {
            location = urlVerDocumento + "?numeroOperacion=" + paramsAccion['numeroOperacion'] + "&codDocumento=" + paramsAccion['codDocumento'] + "&nroCorrelativo=" + paramsAccion['nroCorrelativo'];
        }
        else if (url===urlVerReporteContable) {
            location = urlVerReporteContable + "?nroReporte=" + paramsAccion['nroReporte'] + "&fechaOp=" + encodeURIComponent(paramsAccion['fechaOp']) + "&generarHtmlCompleto=true";
        }
        else if (url === urlVerDetalleMensajeSwift) {
            location = urlVerDetalleMensajeSwift + "?nroMensaje=" + paramsAccion['nroMensaje'] + "&nroReporte=" + paramsAccion["nroReporte"] + "&fechaOp=" + paramsAccion["fechaOp"] + "&generarHtmlCompleto=true";
        }
        else{
            return false;
        }
        if(paramsAccion.filename != null)
            location += "&filename=" + paramsAccion.filename;

        //alert(location);
        var w = window.open();
        w.location = location;

        // Para que al estar configurada en HTML no se cierre el tab transcurridos 7 segundos.
        if (ConfigImpres_PrintFormat != 'HTML') {
            setTimeout(function () { w.close(); }, 7000);
        }

        return true;
    }
}

function ImprimirMultiplesDocumentos() {
    var tabla = $('#tablaDocs');
    var selections = tabla.bootstrapTable('getAllSelections');

    if (selections.length === 0 || selections.length == 0) {
        return false;
    }
    else {
        if (ConfigImpres_PrintFormat == 'HTML') {
            $.each(selections, function (i, item) {
                ImprimirRowDeTabla(item);
            });
        }
        else
        {
            var request = []
            var filename = "EMPTY";

            for (i = 0; i < selections.length; ++i) {
                var item = selections[i];
                request[request.length] = ObtenerRequestRowDeTabla(item);
                if (filename == "EMPTY")
                    filename = item['NroOperacion'].replace(/-/g, '');
                else if (filename != item['NroOperacion'])
                    filename = "";
            }
            
            

            //var w = window.open();
            //w.location = urlVerMultiples + "?request=" + JSON.stringify(request);
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", urlVerMultiples);
            form.setAttribute("target", "_blank");


            var hiddenField = document.createElement("input");
            hiddenField.setAttribute("name", "request");
            hiddenField.setAttribute("value", JSON.stringify(request));
            hiddenField.setAttribute("type", "hidden");
            form.appendChild(hiddenField);

            hiddenField = document.createElement("input");
            hiddenField.setAttribute("name", "filename");
            hiddenField.setAttribute("value", filename);
            hiddenField.setAttribute("type", "hidden");
            form.appendChild(hiddenField);

            

            document.body.appendChild(form);    // Not entirely sure if this is necessary           
            form.submit();

            form.parentNode.removeChild(form);
        }
        
    }
}

function ActualizarCantSeleccionados(row) {
    if (row != null) {
        var selections = $(this).bootstrapTable('getAllSelections');
        if (selections.length > 0) {
            $('#badgeCantSeleccionados').text(selections.length);
            return true;
        }
    }

    $('#badgeCantSeleccionados').text("0");
    return false;
}

function BusquedaOnSuccess() {
    $('#pnlResultados').show();
    ActualizarCantidadDocumentos();
    ActualizarCantSeleccionados();
}

function BusquedaOnError(status) {
    ActualizarCantidadDocumentos();
    ActualizarCantSeleccionados();
    $('#tablaDocs').bootstrapTable('removeAll');
    showAlert("Error al realizar la búsqueda", "", "alert-danger");
}

function ActualizarCantidadDocumentos()
{
    var data = $('#tablaDocs').bootstrapTable('getData', { useCurrentPage: false });
    $('#badgeCantDocumentos').text(data.length);
}

function fechaFormatter(value, row, index) {
    if (value != null) {
        return moment.utc(value).format("DD/MM/YYYY");
    }
}

function InicializarFiltrosDeFecha() {
    var dateNow = moment().startOf("day").utc();
    $('#dtpFechaOperacion').datetimepicker({ format: 'DD/MM/YYYY', locale: 'es', focusOnShow: false, defaultDate: dateNow, maxDate: dateNow, debug: true });
    
    $("#dtpFechaOperacion").on("dp.error", function (e) {
        //por ahora lo ignoro
    });

    $("#dtpFechaOperacion").on("dp.show", function (e) {
        $("#radFiltroFecha").prop("checked", true).change();
    });

    $("#dtpFechaOperacion").on("dp.change", function (e) {
        $("#radFiltroFecha").prop("checked", true).change();
    });
}

function ValidateFilters(showErrors) {
    var dtpFecha = $('#dtpFechaOperacion');
    var selectedFilter = $("input[name=radTipoFiltro]:checked");
    var txtContractReference = $("#crfNumber");
    var fechaDesde = dtpFecha.data("DateTimePicker").date();

    dtpFecha.closest(".form-group").removeClass("has-error");
    $("#lblErrorFecha").hide();
    txtContractReference.closest(".form-group").removeClass("has-error");
    $("#lblErrorCRNumber").hide();

    if (selectedFilter.attr("id") === "radFiltroFecha" && fechaDesde == null) {
        if (showErrors) {
            dtpFecha.closest(".form-group").addClass("has-error");
            $("#lblErrorFecha").show();
        }
        $("#txtfechaOperacion").focus();
        return false;

    } else if (selectedFilter.attr("id") === "radFiltroCrfNumber" && txtContractReference.val().trim() == "") {
        if (showErrors) {
            txtContractReference.closest(".form-group").addClass("has-error");
            $("#lblErrorCRNumber").show();
        }
        txtContractReference.focus();
        return false;
    }

    return true;
}

function TraerDatosYActualizarVisorConResultado(paramsAccion, urlAccion, funcionAdicionalOnSuccess) {
    var data = null;

    $.ajax({
        type: "GET",
        url: urlAccion,
        cache:false,
        success: function (resultado) {
            $("#divCuerpoDoc").html(resultado);
            $('#modalVisorDoc').modal({ backdrop: true });

            if (funcionAdicionalOnSuccess) {
                funcionAdicionalOnSuccess();
            };
        },
        data: paramsAccion,
        error: function (response, type, message) {
            try {
                //intento parsear la respesta como json.
                var responseJson = JSON.parse(response.responseText);
                showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
            }
            catch(err)
            {
                showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", true);
            }
        }
    });
}

function HabilitarFiltrosSegunSeleccionado(seleccionado) {
    switch (seleccionado) {
        case "radFiltroFecha":
            $("#dtpFechaOperacion").data("DateTimePicker").enable();
            $("#crfNumber").attr("disabled", "disabled");
            $('input[name=nroOp]').attr("disabled", "disabled");
            break;

        case "radFiltroNroOperacion":
            $("#dtpFechaOperacion").data("DateTimePicker").disable();
            $("#crfNumber").attr("disabled", "disabled");
            $('input[name=nroOp]').removeAttr("disabled");
            break;

        case "radFiltroCrfNumber":
            $("#dtpFechaOperacion").data("DateTimePicker").disable();
            $("#crfNumber").removeAttr("disabled");
            $('input[name=nroOp]').attr("disabled", "disabled");
            break;
    }
}

function pad(n, width, z) {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
}

$(function () {
    //tooltips
    $('[data-toggle="tooltip"]').tooltip();

    //filtros
    InicializarFiltrosDeFecha();
    
    $('#pnlResultados').hide();


    $(window).resize(function () {
        $('#tablaDocumentos').bootstrapTable('resetView');
    });

    $('#btnBuscarDocs').click(BuscarDocsParaImprimir);
    $("#btnImprimirMultiples").click(ImprimirMultiplesDocumentos);

    $("input[name=radTipoFiltro]:radio").change(function ()
    {
        HabilitarFiltrosSegunSeleccionado($(this).attr('id'));
    });

    $(".labelRadio").click(function () {
        var nombreRadAsociado = $(this).attr('for');
        var radAsociado = $("#" + nombreRadAsociado);
        radAsociado.prop("checked", true).change();
    });

    $("#btnImprimir").click(function () {
        ImprimirDocumento(paramsUltimaAccion);
    });

    $("#btnCancelar").click(function () {
        window.location = $.urlParam('urlHome');
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

    $("#codofi").change(function () {
        $(this).val(pad($(this).val(), 3));
    });

    $("#codope").change(function () {
        $(this).val(pad($(this).val(), 5));
    });

    HabilitarFiltrosSegunSeleccionado("radFiltroFecha"); //el filtro fecha es que esta seleccionado por defecto, eso no me dispara el change asi q lo llamo manualmente


    //activo el bloquear la UI en requests asincronos
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI).ajaxError($.unblockUI);

    $.urlParam = function (name) {
        var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
        return results[1] || 0;
    }


    

});
