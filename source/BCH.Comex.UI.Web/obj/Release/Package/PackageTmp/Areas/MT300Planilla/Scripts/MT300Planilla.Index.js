var formatoFecha = "DD/MM/YYYY";

function Buscar(usarFiltros = false) {
    console.log('BUSCAR');
    tipoUltimaBusqueda = 1;

    var columns = GetColumns();

    var fecha = $('#dtpFiltroFecha').data("DateTimePicker").date();

    var filtroFecha = null;
    if (fecha != null) {
        filtroFecha = fecha.format(formatoFecha);
    }

    $('#alertdiv0').hide();


    var tabla = $('#tablaArchivos');
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
                fecha: filtroFecha,
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


    $.post(urlResumen, { fechafiltro: filtroFecha }, function (data) {
        $("#archProcesados").text(data.resumen.archivos_procesados);
        $("#errorNack").text(data.resumen.registros_error_nack);
        $("#mt300Generados").text(data.resumen.mensajes_generados);
        $("#regProcesados").text(data.resumen.registros_procesados);
        $("#errFormato").text(data.resumen.registros_error_formato);
        $("#mt300Existentes").text(data.resumen.mensajes_existentes);
    });

}

function GetColumns() {
    return [
        { formatter: AccionesGridFormatter, events: operateEvents, title: 'Acciones' },
        { field: 'id_archivo', visible: false }, //oculta, para identificar unicamente los msgs
        //{ field: 'id_swift', title: 'N° mensaje', sortable: true },
        { field: 'nombre', title: 'Nombre Archivo', sortable: true },
        { field: 'fecha_carga', title: 'Fecha', sortable: true },
        { field: 'usuario', title: 'Usuario', sortable: true },
        { field: 'error_nack', title: 'Error Nack', sortable: true },
        { field: 'error_formato', title: 'Error formato', sortable: true },
        { field: 'mt300_generados', title: 'MT300 Generados', sortable: true },
        { field: 'mt300_existentes', title: 'MT300 Existentes', sortable: true },
        { field: 'total_registros', title: 'Total Registros', sortable: true },
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
        //'<a class="verMensaje accionRow" href="' + urldet +'" title="Ver Detalle">',
        //'<a class="verMensaje accionRow" href="javascript:void(0)" onclick="Detalle(' + row['id_archivo'] +')" title="Ver Detalle">',
        '<a class="verMensaje accionRow" href="javascript:void(0)" title="Ver detalle">',
        '<i class="glyphicon glyphicon-th-list"></i>',
        '</a>  '
    ];



    return lineasHtmlAcciones.join('');
}

function Detalle(id_archivo) {
    //console.log('url : ' + urlVerMensaje + '?id_archivo=' + id_archivo)
    //var location = urlVerMensaje + '?id_archivo=' + id_archivo;
    var location = urlVerMensaje + '?id_archivo=' + id_archivo;
    window.open(location, "_blank");
}

function montoFormatter(value, row, index) {
    return numeral(value).format("0,0.00");
}

function InicializarFiltrosDeFecha() {
    var dateNow = moment().startOf("day").utc();

    $('#dtpFiltroFecha').datetimepicker({ format: formatoFecha, locale: 'es', maxDate: dateNow, debug: true, defaultDate: dateNow });

}

function ToggleSeleccionRow(row, $element) {

}

function OnTablaArchivosLoadError(e, status) {
    //showAlert("Error en la búsqueda", "", "alert-danger");
}

function OnTablaArchivosLoadSuccess(data) {
    $('#tablaArchivos').bootstrapTable('resetView', {
        height: 550
    });
    $('#tablaArchivos').bootstrapTable('resetWidth');
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
    var $tablaArchivos = $('#tablaArchivos');

    $tablaArchivos.on("dbl-click-row.bs.table", ToggleSeleccionRow);
    $tablaArchivos.on("load-error.bs.table", OnTablaArchivosLoadError);
    $tablaArchivos.on("load-success.bs.table", OnTablaArchivosLoadSuccess);

    //$tablaArchivos.on("check.bs.table", function (event, row) {
    //    // Si el arreglo esta vacío hacemos push directo
    //    if (seleccionados.length === 0) {
    //        seleccionados.push(row);
    //    } else {
    //        // Validamos que no esté repetido
    //        var registroRepetido = false;
    //        for (var i = 0; i < seleccionados.length; i++) {
    //            if (seleccionados[i].reference === row.reference) {
    //                registroRepetido = true;
    //                break;
    //            }
    //        }
    //        // si no está repetido lo agregamos
    //        if (!registroRepetido) {
    //            seleccionados.push(row);
    //        }
    //    }
    //    ActualizarCantSeleccionados(this);
    //});

    //$tablaSwifts.on("uncheck.bs.table", function (event, row) {
    //    // validamos el largo de seleccionados
    //    if (seleccionados.length > 0) {
    //        // recorremos el arreglo en caso de tener registros
    //        for (var i = 0; i < seleccionados.length; i++) {
    //            // verificamos que el row exista dentro del arreglo
    //            if (seleccionados[i].reference === row.reference) {
    //                // lo eliminamos
    //                seleccionados.splice(i, 1);
    //                break;
    //            }
    //        }
    //    }
    //    ActualizarCantSeleccionados(this);
    //});

    //$tablaSwifts.on("check-all.bs.table", function (event) {
    //    // obtenemos los rows seleccionados de la pagina actual.
    //    var rowsSeleccionadas = $tablaSwifts.bootstrapTable('getAllSelections');

    //    // validamos que tengamos items en nuestro arreglo global.
    //    if (seleccionados.length > 0) {
    //        $.each(rowsSeleccionadas, function (index, item) {
    //            // agregamos solo los que no estén repetidos.
    //            var registroRepetido = false;
    //            for (var i = 0; i < seleccionados.length; i++) {
    //                if (seleccionados[i].SesionSecuencia === item.SesionSecuencia && seleccionados[i].id_mensaje === item.id_mensaje) {
    //                    registroRepetido = true;
    //                    break;
    //                }
    //            }
    //            // si no está repetido lo agregamos
    //            if (!registroRepetido) {
    //                seleccionados.push(item);
    //            }
    //        });
    //    } else {
    //        // si no tenemos items en seleccionados, asignamos el objeto completo.
    //        seleccionados = $tablaSwifts.bootstrapTable('getAllSelections');
    //    }
    //    ActualizarCantSeleccionados(this);
    //});

    //$tablaSwifts.on("uncheck-all.bs.table", function (event) {
    //    // obtenemos los rows "seleccionados" de la pagina actual.
    //    var rowsSeleccionadas = $tablaSwifts.bootstrapTable('getData');
    //    $.each(rowsSeleccionadas, function (index, item) {
    //        // recorremos los rows seleccionados de nuestro arreglo global
    //        for (var i = 0; i < seleccionados.length; i++) {
    //            // si encontramos el row dentro del arreglo global, lo eliminamos
    //            if (seleccionados[i].SesionSecuencia === item.SesionSecuencia && seleccionados[i].id_mensaje === item.id_mensaje) {
    //                seleccionados.splice(i, 1);
    //                // detenemos el for interno para continuar con el siguiente row del foreach
    //                break;
    //            }
    //        }
    //    });
    //    ActualizarCantSeleccionados(this);
    //});

    //filtros
    InicializarFiltrosDeFecha();
    $('#btnBuscar').click(function () {
        Buscar(true);
    });

    Buscar(false);
});

window.operateEvents = {
    'click .verMensaje': function (e, value, row, index) {
        console.log('DETALLE : ' + urlVerMensaje);
        //var location = urlVerMensaje + '?id_archivo=' + row['id_archivo'];
        var location = urlVerMensaje + "/?id_archivo="+ row['id_archivo'];
        window.open(location, "_blank");
    }
};