var seleccionados = [];
var formatoFecha = "DD/MM/YYYY";
var ultimaBusquedaFiltros = false;

var pageSize = 25;
var rowOffset = 0;

function Buscar(usarFiltros = false) {
    console.log(modelCompleto);

    ultimaBusquedaFiltros = usarFiltros;
    seleccionados = [];
    tipoUltimaBusqueda = 1;

    
    var columns = GetColumns();

    //var filtroEstadoRecibidos = $('#ddlFiltroEstado');
    //filtroEstadoRecibidos.val('0');

    //var filtroEstadoEnviados = $('input[name="filtroEstadoEnviados"]:checked').val();

    //if ($('#direccionRecibidos').is(':checked')) {
    //    columns = GetColumnsRecibidos();
    //    direccion = 2;
    //    $('#formGroupFiltroEstado').show();
    //}
    //else {
    //    columns = GetColumnsEnviados(filtroEstadoEnviados);
    //    $('#formGroupFiltroEstado').hide();
    //}

    //if ($('#radIncluirSoloPropios').is(':checked')) {
    //    incluirSoloPropios = true;
    //}

    //$tablaSwifts.bootstrapTable('destroy'); // Ya que las columnas cambian entre busqueda de recibidos y enviados

    //var fechaDesde = $('#dtpFechaDesde').data("DateTimePicker").date();

    //if ($("#chkPeriodoFechas").is(":checked")) {
    //    fechaHasta = $('#dtpFechaHasta').data("DateTimePicker").date();
    //}
    //else {
    //    fechaHasta = fechaDesde;
    //}

    var fecha = $('#dtpFiltroFecha').data("DateTimePicker").date();
    var referencia = $('#filtroReferencia').val();
    var destino = $('#filtroDestino').val();
    var cuenta = $('#filtroCuenta').val();

    var filtroFecha = null;
    if (fecha != null) {
        filtroFecha = fecha.format(formatoFecha);
    }

    $('#alertdiv0').hide();

    //var idCasilla = $('#idCasilla').val();
    //var nameCasilla = $('#idCasilla option:selected').text();

    var tabla = $('#tablaSwifts');
    tabla.bootstrapTable();
    tabla.bootstrapTable('destroy');

    tabla.bootstrapTable({
        classes: 'table table-hover resultRow',
        method: 'post',
        height: 550,
        url: urlBuscar,
        queryParams: function GetParamsParaBusquedaSwift(p) {
            //alert(JSON.stringify(p));
            pageSize = p.limit;

            paramsBuscar = {
                usarFiltros: usarFiltros,
                referencia: referencia,
                destino: destino,
                cuenta: cuenta,
                fecha: filtroFecha,
                pageSize: p.limit,
                rowOffset: p.offset,
                sortOrder: p.order,
                searchText: p.search
            };

            return paramsBuscar;
        },
        queryParamsType: 'limit',
        columns: columns,
        pagination: true,
        sidePagination: 'server',
        pageSize: pageSize,
        pageNumber: rowOffset / pageSize + 1,
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
        uniqueId: 'id_procesados',
        showExport: true,
        exportTypes: ['excel', 'txt'],
        exportDataType: 'all',
        onLoadError: function (arg1, arg2, arg3) {
            if (arg2.responseJSON !== undefined) {
                var msgText = arg2.responseJSON.Message;
                if (msgText.toLowerCase().indexOf("timeout expired") >= 0) {
                    showAlert("Error", "Debe elegir un rango que contenga menos registros.", "alert-danger");
                }
                else {
                    showAlert("Error en la búsqueda", "", "alert-danger");
                }
            }
            else if (arg2.statusText !== "abort") {
                showAlert("Error en la búsqueda", "", "alert-danger");
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

}

function GetColumns() {
    var columns = [
        { formatter: AccionesGridFormatter, events: operateEvents, title: 'Acciones' },
        { field: 'id_procesados', visible: false }, //oculta, para identificar unicamente los msgs
        { field: 'id_swift', title: 'N° mensaje', sortable: true },
        { field: 'reference', title: 'Referencia', sortable: true },
        { field: 'booked_by', title: 'Fecha', sortable: true },
        { field: 'bic_destino', title: 'Destino', sortable: true },
        { field: 'safekeeping', title: 'Número de cuenta', sortable: true },
        { field: 'rate', title: 'Tipo de cambio', sortable: true },
        { field: 'amount_mn', title: 'Moneda, monto [:32B:]', sortable: true, formatter: montoYMonedaMNFormatter},
        { field: 'amount_me', title: 'Moneda, monto [:33B:]', sortable: true, formatter: montoYMonedaMEFormatter},
        { field: 'tipo_operacion', title: 'Tipo Operación', sortable: true },
        { field: 'beneficiary', title: 'Beneficiario', sortable: true },
        { field: 'fecha_proceso', title: 'Fecha de proceso', sortable: true },
        { field: 'fecha_envio', title: 'Fecha de envío', sortable: true },
        { field: 'estado_msg', title: 'Estado', sortable: true },
        { title: 'Estado interno', formatter: estadosGridFormatter, sortable: true }
    ];
    // Se agregan checkboxes para gestión
    if (!soloConsulta) {
        columns.unshift({ checkbox: true, formatter: CheckboxFormatter, title: '#' })
    }

    return columns;
}

function estadosGridFormatter(value, row, index) {
    if (row['estado_interno'] == procesadoNuevo) {
        return 'Enviado';
    }
    else if (row['estado_interno'] == procesadoModificado) {
        return 'Reenviado';
    }
    else if (row['estado_interno'] == procesadoAnulado) {
        return 'Anulado';
    }
    else {
        return row['estado_interno'];
    }
}

function AccionesGridFormatter(value, row, index) {
    var lineasHtmlAcciones = [
        '<a class="verMensaje accionRow" href="javascript:void(0)" title="Ver mensaje">',
        '<i class="glyphicon glyphicon-file"></i>',
        '</a>  ',
    ];


    if (!soloConsulta) {
        if (row['estado_interno'] == procesadoAnulado) {
            lineasHtmlAcciones.push('<a class="modificarMensaje accionRow" href="javascript:void(0)" title="Modificar mensaje">');
            lineasHtmlAcciones.push('<i class="glyphicon glyphicon-pencil"></i>');
            lineasHtmlAcciones.push('</a>  ');
        }
        else {
            lineasHtmlAcciones.push('<a class="anularMensaje accionRow" href="javascript:void(0)" title="Anular mensaje">');
            lineasHtmlAcciones.push('<i class="glyphicon glyphicon-ban-circle"></i>');
            lineasHtmlAcciones.push('</a>  ');
        }
    }

    return lineasHtmlAcciones.join('');
}

function montoFormatter(value, row, index) {
    return numeral(value).format("0,0.00");
}

function montoYMonedaMEFormatter(value, row) {
    return row.codigo_moneda_me + numeral(value).format("0,0.[00]");
}

function montoYMonedaMNFormatter(value, row) {
    return row.codigo_moneda_mn + numeral(value).format("0,0.[00]");
}

function CheckboxFormatter(value, row, index) {
    if (row.estado_interno === procesadoAnulado) {
        return { disabled: true }
    }
    return value;
}

function InicializarFiltros() {
    console.log(modelCompleto.fecha);

    //fecha
    var dateNow = moment().startOf("day").utc();
    if (modelCompleto.fecha) {
        $('#dtpFiltroFecha').datetimepicker({ format: formatoFecha, locale: 'es', defaultDate: modelCompleto.fecha, maxDate: dateNow, debug: true });
    }
    else {
        $('#dtpFiltroFecha').datetimepicker({ format: formatoFecha, locale: 'es', maxDate: dateNow, debug: true });
    }

    //referencia
    $('#filtroReferencia').val(modelCompleto.referencia);
    //destino
    $('#filtroDestino').val(modelCompleto.destino);
    //cuenta
    $('#filtroCuenta').val(modelCompleto.cuenta);
}

function ToggleSeleccionRow(row, $element) {

}

function OnTablaSwiftLoadError(e, status) {
    showAlert("Error en la búsqueda", "", "alert-danger");
}

function OnTablaSwiftLoadSuccess(data) {
    $('#tablaSwifts').bootstrapTable('resetView', {
        height: 550
    });
    $('#tablaSwifts').bootstrapTable('resetWidth');
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

function confirmarContinuarAnulacion(referencia) {
    var msg = "ATENCION! Está a punto de anular el mensaje con referencia " + referencia + ".\n¿Desea continuar?";
    return window.confirm(msg);
}

function confirmarContinuarAnulacionSeleccionados() {
    var referencias = seleccionados.map(x => x.reference);
    var msg = "ATENCION! Está a punto de anular los mensajes con las referencias: " + referencias.join(", ") + ".\n¿Desea continuar?";
    return window.confirm(msg);
}

$(document).ready(function () {
    var $tablaSwifts = $('#tablaSwifts');

    $tablaSwifts.on("dbl-click-row.bs.table", ToggleSeleccionRow);
    //$tablaSwifts.on("load-error.bs.table", OnTablaSwiftLoadError);
    $tablaSwifts.on("load-success.bs.table", OnTablaSwiftLoadSuccess);

    $tablaSwifts.on("check.bs.table", function (event, row) {
        // Si el arreglo esta vacío hacemos push directo
        if (seleccionados.length === 0) {
            seleccionados.push(row);
        } else {
            // Validamos que no esté repetido
            var registroRepetido = false;
            for (var i = 0; i < seleccionados.length; i++) {
                if (seleccionados[i].reference === row.reference) {
                    registroRepetido = true;
                    break;
                }
            }
            // si no está repetido lo agregamos
            if (!registroRepetido) {
                seleccionados.push(row);
            }
        }
        ActualizarCantSeleccionados(this);
    });

    $tablaSwifts.on("uncheck.bs.table", function (event, row) {
        // validamos el largo de seleccionados
        if (seleccionados.length > 0) {
            // recorremos el arreglo en caso de tener registros
            for (var i = 0; i < seleccionados.length; i++) {
                // verificamos que el row exista dentro del arreglo
                if (seleccionados[i].reference === row.reference) {
                    // lo eliminamos
                    seleccionados.splice(i, 1);
                    break;
                }
            }
        }
        ActualizarCantSeleccionados(this);
    });

    $tablaSwifts.on("check-all.bs.table", function (event) {
        // obtenemos los rows seleccionados de la pagina actual.
        var rowsSeleccionadas = $tablaSwifts.bootstrapTable('getAllSelections');

        // validamos que tengamos items en nuestro arreglo global.
        if (seleccionados.length > 0) {
            $.each(rowsSeleccionadas, function (index, item) {
                // agregamos solo los que no estén repetidos.
                var registroRepetido = false;
                for (var i = 0; i < seleccionados.length; i++) {
                    if (seleccionados[i].SesionSecuencia === item.SesionSecuencia && seleccionados[i].id_mensaje === item.id_mensaje) {
                        registroRepetido = true;
                        break;
                    }
                }
                // si no está repetido lo agregamos
                if (!registroRepetido) {
                    seleccionados.push(item);
                }
            });
        } else {
            // si no tenemos items en seleccionados, asignamos el objeto completo.
            seleccionados = $tablaSwifts.bootstrapTable('getAllSelections');
        }
        ActualizarCantSeleccionados(this);
    });

    $tablaSwifts.on("uncheck-all.bs.table", function (event) {
        // obtenemos los rows "seleccionados" de la pagina actual.
        var rowsSeleccionadas = $tablaSwifts.bootstrapTable('getData');
        $.each(rowsSeleccionadas, function (index, item) {
            // recorremos los rows seleccionados de nuestro arreglo global
            for (var i = 0; i < seleccionados.length; i++) {
                // si encontramos el row dentro del arreglo global, lo eliminamos
                if (seleccionados[i].SesionSecuencia === item.SesionSecuencia && seleccionados[i].id_mensaje === item.id_mensaje) {
                    seleccionados.splice(i, 1);
                    // detenemos el for interno para continuar con el siguiente row del foreach
                    break;
                }
            }
        });
        ActualizarCantSeleccionados(this);
    });

    pageSize = modelCompleto.pageSize;
    rowOffset = modelCompleto.rowOffset;

    //filtros
    InicializarFiltros();
    $('#btnBuscar').click(function () {
        rowOffset = 0;
        Buscar(true);
    });

    $('#btnLimpiarSeleccionados').click(function () {
        // Limpiamos nuestro arreglo global de seleccionados
        seleccionados = [];
        // Limpiamos los checks de bootstrap table
        $('#tablaSwifts').bootstrapTable('uncheckAll');
        // limpiamos los rows que tengan la clase selected
        $('#tablaSwifts tr.selected').removeClass('selected');
        // Actualizamos la cantidad de seleccionados
        ActualizarCantSeleccionados();
        // Escondemos el tooltip
        $(this).tooltip('hide');
    });

    $('#btnAnularSeleccionados').click(AnularSeleccionados);

    Buscar(modelCompleto.usarFiltros);
});

function ActualizarCantSeleccionados(row) {
    if (row != null) {
        var selections = seleccionados;
        if (selections.length > 0) {
            $('#btnAnularSeleccionados').removeAttr('disabled');
            $('#btnLimpiarSeleccionados').removeAttr('disabled');
            $('.CantSeleccionados').text(selections.length);
            return true;
        }
    }
    $('#btnAnularSeleccionados').attr('disabled', 'disabled');
    $('#btnLimpiarSeleccionados').attr('disabled', 'disabled');
    $('.CantSeleccionados').text("0");
    return false;
}

window.operateEvents = {
    'click .verMensaje': function (e, value, row, index) {
        var location = urlVerMensaje + '/' + row['id_procesados'];
        window.open(location, "_blank");
    },
    'click .modificarMensaje': function (e, value, row, index) {
        var location = urlModificarMensaje + '/' + row['id_procesados'];
        window.open(location, "_blank");
    },
    'click .anularMensaje': function (e, value, row, index) {
        ultimaRowAccionIndividual = row;
        if (confirmarContinuarAnulacion(row.reference)) {
            $.ajax({
                type: "POST",
                cache: false,
                url: urlAnularMensaje,
                data: { idProcesados: [row.id_procesados] },
                error: function (response, type, message) {
                    try {
                        //intento parsear la respesta como json.
                        var responseJson = JSON.parse(response.responseText);
                        showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
                    }
                    catch (err) {
                        showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", true);
                    }
                },
                success: function (resultado) {
                    //alert(JSON.stringify(resultados));
                    console.log(resultado);
                    limpiarTodosLosErrores();

                    loadMessages(resultado.Mensajes);

                    var tabla = $('#tablaSwifts');
                    tabla.bootstrapTable('refresh');

                } //fin success
            });

        }
    },
};

function limpiarTodosLosErrores() {
    $("#msg-zone").html("");
}

function AnularSeleccionados() {

    if (seleccionados.length === 0) {
        return false;
    }

    var idSeleccionados = seleccionados.map(x => x.id_procesados);

    if (confirmarContinuarAnulacionSeleccionados()) {
        $.ajax({
            type: "POST",
            cache: false,
            url: urlAnularMensaje,
            data: { idProcesados: idSeleccionados },
            error: function (response, type, message) {
                try {
                    //intento parsear la respesta como json.
                    var responseJson = JSON.parse(response.responseText);
                    showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
                }
                catch (err) {
                    showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", true);
                }
            },
            success: function (resultado) {
                //alert(JSON.stringify(resultados));
                console.log(resultado);
                limpiarTodosLosErrores();

                loadMessages(resultado.Mensajes);

                var tabla = $('#tablaSwifts');
                tabla.bootstrapTable('refresh');

            } //fin success
        });

    }
}