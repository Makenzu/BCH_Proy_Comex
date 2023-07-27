var formatoFecha = "DD/MM/YYYY";

function Buscar(id_Archivo) {
    tipoUltimaBusqueda = 1;

    var columns = GetColumns();


    $('#alertdiv0').hide();


    var tabla = $('#tablaDetalleArchivos');
    tabla.bootstrapTable();
    tabla.bootstrapTable('destroy');

    tabla.bootstrapTable({
        classes: 'table table-hover resultRow',
        method: 'post',
        height: 550,
        url: urlBuscar,
        queryParams: function GetParamsParaBusquedaSwift(p) {
            //alert(JSON.stringify(p));
            paramsBuscar = {
                id_archivo: id_Archivo,
                pageSize: p.limit,
                rowOffset: p.offset,
                sortOrder: p.order,
            };

            return paramsBuscar;
        },
        queryParamsType: 'limit',
        columns: columns,
        pagination: true,
        sidePagination: 'server',
        pageSize: 25,
        pageList: [10, 25, 50, 100, 200],
        search: false,
        searchTimeOut: 1000,
        trimOnSearch: true,
        searchAlign: 'left',
        clickToSelect: false,
        sortable: false,
        toolbar: "#toolbar",
        locale: "es-SP",
        maintainSelected: true,
        showRefresh: true,
        uniqueId: 'id_archivo',
        showExport: true,
        exportTypes: ['excel', 'txt'],
        exportDataType: 'all',
        onLoadError: function (arg1, arg2, arg3) {
            if (arg2.responseJSON !== undefined) {
                var msgText = arg2.responseJSON.Message;
                if (msgText.toLowerCase().indexOf("timeout expired") >= 0) {
                    showAlert("Error", "Debe elegir un rango que contenga menos registros.", "alert-danger");
                }
            }
        },
        showMultiSort: true,
        cache: false
    });

    // cada vez que busco se agrega un nivel, lo elimino manualmente
    var multiSort = $("#multi-sort > tbody tr");
    if (multiSort.length > 1) {
        multiSort.first().remove();
    }

    //ActualizarCantSeleccionados();
    //if (!$('#collapseResultados').is(":visible")) {
    //    $('#lnkCollapseResultados').click();
    //}

    //setTituloSegunFiltrosSeleccionados(nameCasilla, incluirSoloPropios, fechaDesde, fechaHasta);
    //$('#toolbar').show();
    $.post(urlResumen, { id_archivo: id_Archivo }, function (data) {
        $("#ResumenArchivo").text('Archivo : ' + data.resumen.archivo);
        $("#ResumenFechaCarga").text(data.resumen.fecha_carga);
        $("#ResumenEstadoArchivo").text(data.resumen.estado);
        $("#ResumenMtGenerados").text(data.resumen.mensajes_generados);
        $("#ResumenMtPrevios").text(data.resumen.registros_existentes);
        $("#ResumenErrores").text(data.resumen.registros_errores);
        $("#ResumenRegTotales").text(data.resumen.registros_totales);
    });

}

function GetColumns() {
    return [
        { formatter: AccionesGridFormatter, events: operateEvents, title: 'Acciones' },
        { field: 'id_archivo', visible: false }, //oculta, para identificar unicamente los msgs
        { field: 'id_detalle', visible: false }, //oculta, para identificar unicamente los msgs
        { field: 'safekeeping', title: 'Safekeeping', sortable: true },
        { field: 'beneficiario', title: 'Beneficiario', sortable: true },
        { field: 'referencia', title: 'Referencia', sortable: true },
        { field: 'estado', title: 'Estado', sortable: true }
    ];

    //return [
    //    { checkbox: true, title: '#' },
    //    { formatter: AccionesGridFormatter, events: operateEvents, title: 'Acciones' },
    //    { field: 'SesionSecuencia', visible: false }, //oculta, para identificar unicamente los msgs
    //    { field: 'id_mensaje', title: 'N° mensaje', sortable: true },
    //    { field: 'tipo_msg', title: 'Tipo MT', sortable: true },
    //    { field: 'sesion', title: 'Sesión', sortable: true },
    //    { field: 'secuencia', title: 'Secuencia', sortable: true },
    //    { field: 'referencia', title: 'Referencia', sortable: true },
    //    { field: 'beneficiario', title: 'Beneficiario', sortable: true },
    //    { field: 'cod_moneda', title: 'Moneda', sortable: true },
    //    { field: 'monto', title: 'Monto $', sortable: true, formatter: montoFormatter, align: 'right' },
    //    { field: 'nombre_banco', title: 'Banco Receptor', sortable: true },
    //    { field: 'CodigoYBranchReceptor', title: 'Cod Banco Rec', sortable: true },
    //    { field: 'fecha_ingr', title: 'Fecha emisión', sortable: true },
    //    { field: 'hora_ingr', title: 'Hora emisión', sortable: true },
    //    { field: 'PrioridadDesc', title: 'Prioridad', sortable: true },
    //    { field: 'fecha_env', title: 'Fecha env', sortable: true },
    //    { field: 'hora_env', title: 'Hora env', sortable: true },
    //    { field: 'ciudad_banco', title: 'Ciudad Banco', sortable: true },
    //    { field: 'pais_banco', title: 'País Banco', sortable: true },
    //    { field: 'CodigoYBranchEmisor', title: 'Cod Banco Em', sortable: true },
    //    { field: 'nombre_casilla', title: 'Unidad', sortable: true },
    //    { field: 'nombre_tipo', title: 'Nombre Tipo', sortable: true }
    //];
}

function AccionesGridFormatter(value, row, index) {
    var lineasHtmlAcciones = [
        '<a class="verMensaje accionRow" href="javascript:void(0)" title="Ver Detalle">',
        '<i class="glyphicon glyphicon-th-list"></i>',
        '</a>  '
    ];
//    if (row["estado"] != "ERROR_VALIDACION") {
//        lineasHtmlAcciones = [];
//    }

    return lineasHtmlAcciones.join('');
}

function montoFormatter(value, row, index) {
    return numeral(value).format("0,0.00");
}


function ToggleSeleccionRow(row, $element) {

}

function OnTablaDetalleArchivosLoadError(e, status) {
    //showAlert("Error en la búsqueda", "", "alert-danger");
}

function OnTablaDetalleArchivosLoadSuccess(data) {
    $('#tablaDetalleArchivos').bootstrapTable('resetView', {
        height: 550
    });
    $('#tablaDetalleArchivos').bootstrapTable('resetWidth');
    /// Valida que tenga resultado en la busqueda.
    //var elementos = $('#tablaSwifts').bootstrapTable('getData');
    //if (elementos.length > 0) {
    //    var divPanelFiltros = $('.panel div.clickable');
    //    if (!divPanelFiltros.hasClass('panel-collapsed')) {
    //        //no esta colapsado el panel de filtro 
    //        divPanelFiltros.click();
    //    }
    //}

    //if ($('#tablaSwifts').bootstrapTable('getOptions').showMultiSort) {
    //    // Evitar doble cabecera del multiple ordenamiento.
    //    $("#sortModal_tablaSwifts").find('.btn-primary').click();
    //}

    //// si tengo selecciones, les pongo el check
    //if (seleccionados.length > 0) {
    //    var selecciones = [];
    //    var tipoBusqueda = $('input[name=direccion]:checked').val(); // 1 = Enviados; 2 = Recibidos
    //    $.each(seleccionados, function (index, item) {
    //        selecciones.push(tipoBusqueda == 2 ? item.SesionSecuencia : item.id_mensaje);
    //    });

    //    $('#tablaSwifts').bootstrapTable('checkBy', { field: tipoBusqueda == 2 ? 'SesionSecuencia' : 'id_mensaje', values: selecciones });
    //}
}

$(document).ready(function () {
    var $tablaArchivos = $('#tablaDetalleArchivos');

    $tablaArchivos.on("dbl-click-row.bs.table", ToggleSeleccionRow);
    $tablaArchivos.on("load-error.bs.table", OnTablaDetalleArchivosLoadError);
    $tablaArchivos.on("load-success.bs.table", OnTablaDetalleArchivosLoadSuccess);

    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    id_archivo = urlParams.get('id_archivo')

    Buscar(id_archivo);
});

window.operateEvents = {
    'click .verMensaje': function (e, value, row, index) {
        var location = urlVerMensaje + '/' + row['id_detalle'];
        window.open(location, "_self");
    }
};