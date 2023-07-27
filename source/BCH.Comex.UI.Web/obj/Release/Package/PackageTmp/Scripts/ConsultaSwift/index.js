/* Este script debe utilizarse definiendo por fuera algunas variables */

var tipoUltimaBusqueda = 1; //1 = mensajes, 2 = estadisticas
var ultimosParamsConsultaSwift = null;
var ultimaRowAccionIndividual = null;

var formatoPOST = "DD/MM/YYYY";
var formatoGET = "YYYY/MM/DD";
var $tablaSwifts;
$.fx.off = true;

$(document).on('click', '.panel-heading span.clickable', function (e) {
    var $this = $(this);
    if (!$this.hasClass('panel-collapsed')) {
        $this.parents('.panel').find('.panel-body').hide();
        $this.addClass('panel-collapsed');
        $this.find('i').removeClass('glyphicon-minus').addClass('glyphicon-plus');
    } else {
        $this.parents('.panel').find('.panel-body').show();
        $this.removeClass('panel-collapsed');
        $this.find('i').removeClass('glyphicon-plus').addClass('glyphicon-minus');
    }
});
$(document).on('click', '.panel div.clickable', function (e) {
    var $this = $(this);
    if (!$this.hasClass('panel-collapsed')) {
        $this.parents('.panel').find('.panel-body').hide();
        $this.addClass('panel-collapsed');
        $this.find('i').removeClass('glyphicon-minus').addClass('glyphicon-plus');
    } else {
        $this.parents('.panel').find('.panel-body').show();
        $this.removeClass('panel-collapsed');
        $this.find('i').removeClass('glyphicon-plus').addClass('glyphicon-minus');
    }
});

function Buscar(noValidar) {
    if (noValidar || ValidateFilters(true)) {
        seleccionados = [];
        tipoUltimaBusqueda = 1;

        var columns = null;
        var direccion = 1; //enviados
        var incluirSoloPropios = false;

        var filtroEstadoRecibidos = $('#ddlFiltroEstado');
        filtroEstadoRecibidos.val('0');

        var filtroEstadoEnviados = $('input[name="filtroEstadoEnviados"]:checked').val();

        if ($('#direccionRecibidos').is(':checked')) {
            columns = GetColumnsRecibidos();
            direccion = 2;
            $('#formGroupFiltroEstado').show();
        }
        else {
            columns = GetColumnsEnviados(filtroEstadoEnviados);
            $('#formGroupFiltroEstado').hide();
        }

        if ($('#radIncluirSoloPropios').is(':checked')) {
            incluirSoloPropios = true;
        }

        $tablaSwifts.bootstrapTable('destroy'); // Ya que las columnas cambian entre busqueda de recibidos y enviados

        var fechaDesde = $('#dtpFechaDesde').data("DateTimePicker").date();

        if ($("#chkPeriodoFechas").is(":checked")) {
            fechaHasta = $('#dtpFechaHasta').data("DateTimePicker").date();
        }
        else {
            fechaHasta = fechaDesde;
        }

        $('#alertdiv0').hide();

        var idCasilla = $('#idCasilla').val();
        var nameCasilla = $('#idCasilla option:selected').text();

        var tabla = $tablaSwifts;
        tabla.bootstrapTable();
        tabla.bootstrapTable('destroy');

        tabla.bootstrapTable({
            classes: 'table table-hover resultRow',
            method: 'post',
            height: 550,
            url: urlBuscarSwifts,
            queryParams: function GetParamsParaBusquedaSwift(p) {
                //alert(JSON.stringify(p));
                ultimosParamsConsultaSwift = {
                    idCasilla: idCasilla,
                    fechaDesde: fechaDesde.format(formatoPOST),
                    fechaHasta: fechaHasta.format(formatoPOST),
                    direccion: direccion,
                    incluirSoloPropios: incluirSoloPropios,
                    pageSize: p.limit,
                    rowOffset: p.offset,
                    sortOrder: p.order,
                    searchText: p.search,
                    estadoEnviados: filtroEstadoEnviados
                };

                return ultimosParamsConsultaSwift;
            },
            queryParamsType: 'limit',
            columns: columns,
            pagination: true,
            sidePagination: 'server',
            pageSize: 25,
            pageList: [10, 25, 50, 100, 200],
            search: true,
            searchTimeOut: 1000, //pongo un timeout del doble de lo normal para que se dispare la busqueda ya que en esta grilla la busqueda es del lado del server, no quiero ir a buscar en cada key press
            trimOnSearch: true,
            searchAlign: 'left',
            clickToSelect: false,
            sortable: false,
            toolbar: "#toolbar",
            locale: "es-SP",
            maintainSelected: true,
            showRefresh: true,
            uniqueId: 'SesionSecuencia',
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

        ActualizarCantSeleccionados();
        if (!$('#collapseResultados').is(":visible")) {
            $('#lnkCollapseResultados').click();
        }

        setTituloSegunFiltrosSeleccionados(nameCasilla, incluirSoloPropios, fechaDesde, fechaHasta);
        $('#toolbar').show();

    }
}

function setTituloSegunFiltrosSeleccionados(nameCasilla, incluirSoloPropios, fechaDesde, fechaHasta) {
    var tipoMensajesBuscados = getTipoMensajesBuscados().trim();
    var titleFiltro = "";

    if (tipoMensajesBuscados != "Sin solicitud firmas") {
        var msgPropios = "";
        if (incluirSoloPropios) {
            msgPropios = " - <b>Usuario: </b>" + nombreCompletoUsuario;
        }

        var msgFechas = "";
        if (tipoMensajesBuscados != "Devueltos") {
            msgFechas = ' - <b>Desde:</b> ' + fechaDesde.format("DD-MM-YYYY") + ' <b>Hasta: </b>' + fechaHasta.format("DD-MM-YYYY");
        }

        titleFiltro = '<b>Filtros</b> [<b>Casilla:</b> ' + nameCasilla + msgPropios + ' - <b>Mensajes:</b> ' + tipoMensajesBuscados + msgFechas + ']';
    }
    else {
        titleFiltro = '<b>Filtros</b> [<b>Usuario: </b>' + nombreCompletoUsuario + ' - <b>Mensajes:</b> ' + tipoMensajesBuscados;
    }

    $('#titleFiltro').html(titleFiltro);
}

function ImprimirReporteConsulta() {
    try {
        var p = ultimosParamsConsultaSwift;
        if ((p['idCasilla'] == null || p['idCasilla'] == '') && (paraAccion != accionVerSinSolicitarFirmas)) {
            showAlert("Error en la operación", "Detalle: Debe seleccionar un elemento de casillas.", "alert-danger", true);
            return;
        }

        if (p != null) {
            //tener en cuenta que al hacer un GET, las fechas .NET las toma en invariant culture, a diferencia de un POST donde las toma con la current culture
            var urlReporte = urlReporteConsulta + "?idCasilla=" + p['idCasilla'] +
                "&fechaDesde=" + encodeURIComponent(moment(p['fechaDesde'], formatoPOST).format(formatoGET)) +
                "&fechaHasta=" + encodeURIComponent(moment(p['fechaHasta'], formatoPOST).format(formatoGET)) +
                "&direccion=" + p['direccion'] + "&incluirSoloPropios=" + p['incluirSoloPropios'];

            if (p['direccion'] == 2) { //recibidos
                var ddlFiltro = $("#ddlFiltroEstado")
                var filtroCampo = $("#ddlFiltroEstado").val();
                var filtroDesc = $("#ddlFiltroEstado option:selected").text();
                var incluirSoloPropios = $('#radIncluirSoloPropios').is(':checked');

                if (filtroCampo != "0") {
                    urlReporte += "&filtroCampo=" + filtroCampo + "&filtroDesc=" + filtroDesc;
                }

                urlReporte += "&incluirSoloPropios=" + incluirSoloPropios;
            }
            else if (p['direccion'] == 1) {
                urlReporte += "&estadoEnviados=" + p["estadoEnviados"];
            }

            var w = window.open(urlReporte, "_blank");
            return true;
        }
    } catch (e) {
        showAlert("Error en la operación", "Detalle: " + e.message, "alert-danger");
    }

}

function OnTablaSwiftLoadSuccess(data) {
    $('#tablaSwifts').bootstrapTable('resetView', {
        height: 550
    });
    $('#tablaSwifts').bootstrapTable('resetWidth');
    /// Valida que tenga resultado en la busqueda.
    var elementos = $('#tablaSwifts').bootstrapTable('getData');
    if (elementos.length > 0) {
        var divPanelFiltros = $('.panel div.clickable');
        if (!divPanelFiltros.hasClass('panel-collapsed')) {
            //no esta colapsado el panel de filtro 
            divPanelFiltros.click();
        }
    } 

    if ($('#tablaSwifts').bootstrapTable('getOptions').showMultiSort) {
        // Evitar doble cabecera del multiple ordenamiento.
        $("#sortModal_tablaSwifts").find('.btn-primary').click();
    }

    // si tengo selecciones, les pongo el check
    if (seleccionados.length > 0) {
        var selecciones = [];
        var tipoBusqueda = $('input[name=direccion]:checked').val(); // 1 = Enviados; 2 = Recibidos
        $.each(seleccionados, function (index, item) {
            selecciones.push(tipoBusqueda == 2 ? item.SesionSecuencia : item.id_mensaje );
        });

        $('#tablaSwifts').bootstrapTable('checkBy', { field: tipoBusqueda == 2 ? 'SesionSecuencia' : 'id_mensaje', values: selecciones });
    }
}

function OnTablaSwiftLoadError(e, status) {
    showAlert("Error en la búsqueda", "", "alert-danger");
}

function getTableHeight() {

    var offsetTop = $tablaSwifts.offset().top; //posicion donde empieza la tabla.

    //Quiero que la tabla ocupe desde q empieza hasta el final de la pantalla
    var heightFooter = 0;
    if (tipoUltimaBusqueda == 2) {
        //tengo q restarle los totales al height que le informo que quiero que tenga la tabla
        var footer = $(".fixed-table-footer");
        heightFooter = footer.height() + 5;
    }

    var restoPantalla = $(window).height() - (offsetTop + heightFooter);
    return restoPantalla;
}

function ToggleSeleccionRow(row, $element) {

}

function FiltrarResultadosPorEstado() {
    var val = $(this).val();
    var tabla = $tablaSwifts;
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
        //tabla.bootstrapTable('load', dataCumple);
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
    tabla.bootstrapTable('resetWidth');
}

function ActualizarCantSeleccionados(row) {
    if (row != null) {
        var selections = seleccionados; 
        if (selections.length > 0) {
            $('#btnLimpiarSeleccionados').removeAttr('disabled');
            $('.CantSeleccionados').text(selections.length);
            if (ultimosParamsConsultaSwift.estadoEnviados == EstadoSwiftEnviado_Modificado || ultimosParamsConsultaSwift.estadoEnviados == EstadoSwiftEnviado_Ingresado ||
                ultimosParamsConsultaSwift.estadoEnviados == EstadoSwiftEnviado_EnAprobacion || ultimosParamsConsultaSwift.estadoEnviados == EstadoSwiftEnviado_Devuelto || ultimosParamsConsultaSwift.estadoEnviados == EstadoSwiftEnviado_SinSolicitudFirmas) {
                $('.btnMultiAccion').children().prop('disabled', false);
            }
            return true;
        }
    }
    $('#btnLimpiarSeleccionados').attr('disabled', 'disabled');
    $('.btnMultiAccion').children().prop('disabled', true);
    $('.CantSeleccionados').text("0");
    return false;
}

function EjecutarBusquedaPorDefecto() {
    if (paraAccion == accionVerSinSolicitarFirmas) {
        $("#radFiltroSinSolicitudFirmas").prop('checked', true).change(); //este es un "estado" que esta invisible, igual lo chequeo para poder evaluar mas adelante
        Buscar(true);
    }
    else {
        if (ValidateFilters(false)) {
            Buscar(true);
        }
        else {
            //No lo molesto con validaciones ya que fue la busqueda por defecto, si no se pudo hacer no se hizo nomas
        }
    }
}

function VerEstadisticas() {

    $('#alertdiv0').hide();

    if (ValidateFilters()) {

        if (!$('#collapseResultados').is(":visible")) {
            $('#lnkCollapseResultados').click();        //para que se expandan los resultados y se colapse el filtro
        }

        tipoUltimaBusqueda = 2;

        var columns = [
            { field: 'TipoMsg', title: 'Tipo MT', sortable: true },
            { field: 'NombreTipo', title: 'Nombre Tipo', sortable: true, footerFormatter: estadisticasTotalNombreTipoFormatter },
            { field: 'Cantidad', title: 'Cantidad', sortable: true, footerFormatter: estadisticasTotalCantidadFormatter }
        ];

        var direccion = 1; //enviados
        if ($('#direccionRecibidos').is(':checked')) {
            direccion = 2;
        }

        $tablaSwifts.bootstrapTable('destroy'); //ya que las columnas cambian entre busqueda de recibidos y enviados y busqueda o swifts        

        var fechaDesde = $('#dtpFechaDesde').data("DateTimePicker").date();

        if ($("#chkPeriodoFechas").is(":checked")) {
            fechaHasta = $('#dtpFechaHasta').data("DateTimePicker").date();
        }
        else fechaHasta = fechaDesde;

        $tablaSwifts.bootstrapTable({
            method: 'post',
            height: 550,
            cache: false,
            columns: columns,
            pagination: true,
            sidePagination: 'client',
            pageSize: 25,
            pageList: [10, 25, 50, 100, 200],
            showRefresh: true,
            clickToSelect: false,
            sortable: true,
            showFooter: true,
            locale: "es-SP",
            showMultiSort: false
        });

        $.ajax({
            type: "POST",
            cache: false,
            url: urlEstadisticasSwifts,
            data: {
                idCasilla: $('#idCasilla').val(),
                fechaDesde: fechaDesde.format(formatoPOST),
                fechaHasta: fechaHasta.format(formatoPOST),
                direccion: direccion
            },
            success: function (data) {
                $tablaSwifts.bootstrapTable('load', data);
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
            },
        });

        $('#toolbar').hide();
    }
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

function montoFormatter(value, row, index) {
    return numeral(value).format("0,0.00");
}

function ArmarVisorMultiplesMensajes() {
    var tabla = $('#tablaSwifts');
    var selections = seleccionados;
    var recibidos = $('#direccionRecibidos').is(':checked');

    if (selections.length === 0 || selections.length == 0) {
        return false;
    }
    else if (selections.length == 1) {
        //hay un solo item seleccionado, abro el visor individual en lugar del multiple
        ArmarVisorIndividual(selections[0]);
    }
    else {
        var enviarPorMailHabilitado = (selections[0].estado_msg == "ENV" || recibidos); //solo dejo enviar por mail (posiblemente al cliente) si los mensajes son enviados o recibidos (alcanza con fijarme en el 1ero)
        $("#btnEnviarMultiplesMensajes").toggle(enviarPorMailHabilitado);

        $.when.apply(null, selections.map(function (item) {
            //hago un callback ajax por cada uno de mis mensajes seleccionados;
            var data = { sesion: item['sesion'], secuencia: item['secuencia'], htmlCompleto: false };

            if (!recibidos) {  //enviados
                data["idMensaje"] = item['id_mensaje'];
            } else {
                data["visualizacion"] = true;
            }

            return $.ajax({
                type: "GET",
                cache: false,
                url: urlDetalleSwift,
                data: data,
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

$('#modalVisorMultipleSwift').on('show.bs.modal', function () {
    $('.modal .modal-body').css('max-height', $(window).height() * 0.86);
});

$('#modalVisorIndividualSwift').on('hide.bs.modal', function () {
    $tablaSwifts.bootstrapTable('refresh');
});

/* Tengo un combo de "acciones" que desea hacer el usuario, y según lo que selecciona, muestro/oculto algunos estados que no tienen sentido para la acción */
function mostrarEstadosSegunAccion() {
    var accion = $("#ddlFiltrarEstadosPorAcciones").val();
    var valFiltro = $("input[name='filtroEstadoEnviados']:checked").val();

    var nombreLineaEstadosSeRepiten = "";

    switch (accion) {
        case "0":
            $("#linea1ModificarAnular").hide();
            $("#linea1SolicitarFirmas").hide();
            $("#linea2SolicitarFirmas").hide();
            $("#linea1Otros").show();
            $("#linea2Otros").show();
            $("#linea3Otros").show();
            nombreLineaEstadosSeRepiten = "linea1Otros";
            break;

        case "1":
            $("#linea1ModificarAnular").show();
            $("#linea1SolicitarFirmas").hide();
            $("#linea2SolicitarFirmas").hide();
            $("#linea1Otros").hide();
            $("#linea2Otros").hide();
            $("#linea3Otros").hide();

            nombreLineaEstadosSeRepiten = "linea1ModificarAnular";
            break;

        case "2":
        case "3":
            $("#linea1ModificarAnular").hide();
            $("#linea1SolicitarFirmas").show();
            $("#linea2SolicitarFirmas").show();
            $("#linea1Otros").hide();
            $("#linea2Otros").hide();
            $("#linea3Otros").hide();
            nombreLineaEstadosSeRepiten = "linea1SolicitarFirmas";
            break;
    }

    //Si se habia seleccionado "Ingresados" o "Modificados" pero para otra accion, selecciono el de la accion que corresponde
    if (valFiltro == EstadoSwiftEnviado_Ingresado || valFiltro == EstadoSwiftEnviado_Modificado) {
        $("#" + nombreLineaEstadosSeRepiten).find("input[value=" + valFiltro + "]").prop('checked', true).change();
    }
    else {
        //selecciono una opcion por defecto, en "Consultar" pongo "Enviados", en "Modificar" y "Solicitar firmas" pongo "Ingresados"
        switch (accion) {
            case "0":
                $("#linea2Otros").find("input[value=" + EstadoSwiftEnviado_Enviado + "]").prop('checked', true).change();
                break;

            case "1":
                $("#linea1ModificarAnular").find("input[value=" + EstadoSwiftEnviado_Ingresado + "]").prop('checked', true).change();
                break;

            case "2":
                $("#linea1SolicitarFirmas").find("input[value=" + EstadoSwiftEnviado_Ingresado + "]").prop('checked', true).change();
                break;
        }
    }
}

function ArmarVisorIndividual(row) {
    ultimaRowAccionIndividual = row;
    var data = { sesion: row['sesion'], secuencia: row['secuencia'], htmlCompleto: false };
    var recibidos = $('#direccionRecibidos').is(':checked');

    if (!recibidos) {  //enviados
        data["idMensaje"] = row['id_mensaje'];
    } else {
        data["visualizacion"] = true;
    }

    $.ajax({
        type: "GET",
        cache: false,
        url: urlActualizaImp,
        success: function (resultado) {
            $("#modalActualizarImp").html(resultado);
            $('#modalActualizarImp').modal({ backdrop: true });
            $('#modalActualizarImp').modal('hide');
            $('#modalVisorIndividualSwift').modal('show');

            $.ajax({
                type: "GET",
                cache: false,
                url: urlDetalleSwift,
                success: function (resultado) {
                    var enviarPorMailHabilitado = (row.estado_msg == "ENV" || recibidos); //solo dejo enviar por mail (posiblemente al cliente) si el estado es enviado o recibido
                    $("#btnEnviarMensajeIndividual").toggle(enviarPorMailHabilitado);
                    $("#divCuerpoMensajeSwift").html(resultado);
                    $('#modalVisorIndividualSwift').modal({ backdrop: true });
                },
                data: data,
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
        },
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
        todoOK = false;
        if (showErrors) {
            dtpFechaDesde.closest(".form-group").addClass("has-error");
            $("#lblErrorFechaDesde").show();
            $('#txtFechaDesde').focus();
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
                    $('#txtFechaHasta').focus();
                }
            }
        }
    }

    if (cmbCasilla.prop("selectedIndex") == 0) {
        if (showErrors) {
            cmbCasilla.closest(".form-group").addClass("has-error");
            $("#lblErrorCasilla").show();
            $('#idCasilla').focus();
        }
        todoOK = false;
    }
    else {
        cmbCasilla.closest(".form-group").removeClass("has-error");
        $("#lblErrorCasilla").hide();
    }

    return todoOK;
}

function AccionesGridFormatter(value, row, index) {
    var lineasHtmlAcciones = [
        '<a class="verDatos accionRow" href="javascript:void(0)" title="Ver datos">',
        '<i class="glyphicon glyphicon-th-list"></i>',
        '</a>  ',
        '<a class="verLog accionRow" href="javascript:void(0)" title="Ver log">',
        '<i class="glyphicon glyphicon-info-sign"></i>',
        '</a>  ',
        '<a class="verMensaje accionRow" href="javascript:void(0)" title="Ver mensaje">',
        '<i class="glyphicon glyphicon-file"></i>',
        '</a>  ',
    ];

    if (!soloConsulta) {
        if (ultimosParamsConsultaSwift.direccion == 1) {
            if (typeof ultimosParamsConsultaSwift.estadoEnviados === "undefined" || ultimosParamsConsultaSwift.estadoEnviados == null || ultimosParamsConsultaSwift.estadoEnviados == EstadoSwiftEnviado_Enviado) {
                //solo agrego el enviar mensaje para los mensajes en estado 'Enviado' o los recibidos)
                lineasHtmlAcciones.push('<a class="enviarMensaje accionRow" href="javascript:void(0)" title="Enviar mensaje">');
                lineasHtmlAcciones.push('<i class="glyphicon glyphicon-envelope"></i>');
                lineasHtmlAcciones.push('</a>  ');
            }
            else {
                if (ultimosParamsConsultaSwift.estadoEnviados == EstadoSwiftEnviado_Modificado || ultimosParamsConsultaSwift.estadoEnviados == EstadoSwiftEnviado_Ingresado ||
                    ultimosParamsConsultaSwift.estadoEnviados == EstadoSwiftEnviado_Rechazado) {
                    lineasHtmlAcciones.push('<a class="modificar accionRow" href="javascript:void(0)" title="Modificar">');
                    lineasHtmlAcciones.push('<i class="glyphicon glyphicon-pencil"></i>');
                    lineasHtmlAcciones.push('</a>  ');
                    lineasHtmlAcciones.push('<a class="anular accionRow" href="javascript:void(0)" title="Anular">');
                    lineasHtmlAcciones.push('<i class="glyphicon glyphicon-ban-circle"></i>');
                    lineasHtmlAcciones.push('</a>  ');
                }

                if (ultimosParamsConsultaSwift.estadoEnviados == EstadoSwiftEnviado_EnAprobacion) {
                    lineasHtmlAcciones.push('<a class="anular accionRow" href="javascript:void(0)" title="Anular">');
                    lineasHtmlAcciones.push('<i class="glyphicon glyphicon-ban-circle"></i>');
                    lineasHtmlAcciones.push('</a>  ');
                }

                if (ultimosParamsConsultaSwift.estadoEnviados == EstadoSwiftEnviado_Modificado || ultimosParamsConsultaSwift.estadoEnviados == EstadoSwiftEnviado_Ingresado ||
                    ultimosParamsConsultaSwift.estadoEnviados == EstadoSwiftEnviado_EnAprobacion || ultimosParamsConsultaSwift.estadoEnviados == EstadoSwiftEnviado_Devuelto || ultimosParamsConsultaSwift.estadoEnviados == EstadoSwiftEnviado_SinSolicitudFirmas) {
                    lineasHtmlAcciones.push('<a class="solicitarFirmas accionRow" href="javascript:void(0)" title="Solicitar firmas">');
                    lineasHtmlAcciones.push('<i class="glyphicon glyphicon-thumbs-up"></i>');
                    lineasHtmlAcciones.push('</a>  ');
                }
            }
        }
    }

    return lineasHtmlAcciones.join('');
}

function getTipoMensajesBuscados() {
    var tipo = "Recibidos";
    if (ultimosParamsConsultaSwift != null && ultimosParamsConsultaSwift.direccion == 1) {
        tipo = "Enviados";
        if (typeof ultimosParamsConsultaSwift.estadoEnviados !== "undefined" && ultimosParamsConsultaSwift.estadoEnviados != null && ultimosParamsConsultaSwift.estadoEnviados != EstadoSwiftEnviado_Enviado) {
            tipo = $('input[name="filtroEstadoEnviados"]:checked').parent().text(); //el parent es el label
        }
    }

    return tipo;
}

function GetColumnsEnviados(filtroEstadoEnviados) {
    if (typeof filtroEstadoEnviados === "undefined" || filtroEstadoEnviados == null || filtroEstadoEnviados == EstadoSwiftEnviado_Enviado) {
        return [
            { checkbox: true, title: '#' },
            { formatter: AccionesGridFormatter, events: operateEvents, title: 'Acciones' },
            { field: 'SesionSecuencia', visible: false }, //oculta, para identificar unicamente los msgs
            { field: 'id_mensaje', title: 'N° mensaje', sortable: true },
            { field: 'tipo_msg', title: 'Tipo MT', sortable: true },
            { field: 'sesion', title: 'Sesión', sortable: true },
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
            { field: 'CodigoYBranchEmisor', title: 'Cod Banco Em', sortable: true },
            { field: 'nombre_casilla', title: 'Unidad', sortable: true },
            { field: 'nombre_tipo', title: 'Nombre Tipo', sortable: true }
        ];
    }
    else {
        var verbo = "Ingreso";
        var nombreColFecha = "fecha_ingr";
        var nombreColHora = "hora_ingr";

        if (filtroEstadoEnviados == EstadoSwiftEnviado_Modificado) verbo = "Modificación";
        else if (filtroEstadoEnviados == EstadoSwiftEnviado_Autorizado) verbo = "Aprobación";
        else if (filtroEstadoEnviados == EstadoSwiftEnviado_Procesado) verbo = "Proceso";
        else if (filtroEstadoEnviados == EstadoSwiftEnviado_Rechazado) verbo = "Rechazo";
        else if (filtroEstadoEnviados == EstadoSwiftEnviado_Anulado) verbo = "Anulación";
        else if (filtroEstadoEnviados == EstadoSwiftEnviado_Bloqueado) verbo = "Bloqueo";

        if (verbo != "Ingreso") {
            nombreColFecha = "fecha_env"; //asi se llaman las columnas pero no es unicamente la fecha de enviado, es la efcha de la accion
            nombreColHora = "hora_env";
        }

        return [
            { checkbox: true, title: '#' },
            { formatter: AccionesGridFormatter, events: operateEvents, title: 'Acciones' },
            { field: 'SesionSecuencia', visible: false }, //oculta, para identificar unicamente los msgs
            { field: 'id_mensaje', title: 'N° mensaje', sortable: true },
            { field: 'tipo_msg', title: 'Tipo MT', sortable: true },
            { field: 'estado_msg', title: 'Estado', sortable: true },
            { field: 'CodigoYBranchReceptor', title: 'Banco Receptor', sortable: true },
            { field: 'referencia', title: 'Referencia', sortable: true },
            { field: 'cod_moneda', title: 'Moneda', sortable: true },
            { field: 'monto', title: 'Monto $', sortable: true, formatter: montoFormatter, align: 'right' },
            { field: nombreColFecha, title: 'Fecha ' + verbo, sortable: true },
            { field: nombreColHora, title: 'Hora ' + verbo, sortable: true }
        ];
    }
}

function GetColumnsRecibidos() {
    return [
        { checkbox: true, title: '#' },
        { formatter: AccionesGridFormatter, events: operateEvents, title: 'Acciones' },
        { field: 'SesionSecuencia', visible: false }, //oculta, para identificar unicamente los msgs
        { field: 'tipo_msg', title: 'Tipo MT', sortable: true },
        { field: 'sesion', title: 'Sesión', sortable: true },
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
        { field: 'oficina_banco', title: 'Of. Banco', sortable: true },
        { field: 'nombre_casilla', title: 'Unidad', sortable: true },
        { field: 'nombre_tipo', title: 'Nombre Tipo', sortable: true },
        { field: 'total_imp', title: 'Total Imp', sortable: true }
    ];
}

function InicializarFiltrosDeFecha() {
    var dateNow = moment().startOf("day").utc();

    $('#dtpFechaDesde').datetimepicker({ format: formatoPOST, locale: 'es', defaultDate: dateNow, maxDate: dateNow, debug: true });
    $('#dtpFechaHasta').datetimepicker({ format: formatoPOST, locale: 'es', defaultDate: dateNow, maxDate: dateNow, debug: true });
    $('#grpFechaHasta').hide();

    $("#dtpFechaDesde").on("dp.change", function (e) {
        var checkbox = $('#chkPeriodoFechas');
        if (!checkbox.is(':checked') && e.date != false) {
            $('#dtpFechaHasta').data("DateTimePicker").date(e.date);
        }
    });

    $('#chkPeriodoFechas').change(function () {
        if ($(this).is(':checked')) {
            $('#grpFechaHasta').show();
            $('#lblFechaDesde').text("Desde");
            $('#dtpFechaHasta').data("DateTimePicker").date(dateNow); //cada vez que lo muestro lo pongo en Today, es lo que hace mas sentido
        }
        else {
            $('#grpFechaHasta').hide();
            $('#lblFechaDesde').text("Fecha");
            $('#dtpFechaHasta').data("DateTimePicker").hide();
        }
    });
}

function EnviarMailMensajeIndividual(row) {
    var location = urlDescargarMailConSwift + '?sesion=' + row['sesion'] + '&secuencia=' + row['secuencia'];

    if (!$('#direccionRecibidos').is(':checked')) {
        //enviados
        location = location + "&idMensaje=" + row['id_mensaje'];
    }

    $.fileDownload(location)
        .done(function () { /* nada que alertar, con la ventanita del browser pidiendo para bajar el archivo alcanza*/ })
        .fail(function () { showAlert("Error en la descarga.", "El archivo del mensaje no se pudo generar", "alert-danger") });
}

function ImprimirMensajeIndividual(row) {

    if (ConfigImpres_PrintFormat == "HTML") {
        var location = urlDetalleSwift + '?sesion=' + row['sesion'] + '&secuencia=' + row['secuencia'] + '&htmlCompleto=true&pdf=true';
        if (!$('#direccionRecibidos').is(':checked')) {
            location = location + "&idMensaje=" + row['id_mensaje'];
        }
        var w = window.open();
        w.location = location;
    }
    else {
        var request = []
        var filename = "EMPTY";

        request[request.length] = ObtenerRequestRowDeTabla(row);
        if (filename == "EMPTY")
            filename = row.referencia.replace(/-/g, '');
        else if (filename != row.referencia)
            filename = "";

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

function ObtenerRequestRowDeTabla(row) {
    var result = {};
    result.type = "SwiftBaseSwift";
    result.BaseSwift = {
        sesion: row.sesion,
        secuencia: row.secuencia,
        idMensaje: row.id_mensaje
    }

    return result;
}

function EnviarMailMultiplesMensajes() {
    var tabla = $('#tablaSwifts');
    var selections = seleccionados;

    var recibidos = false;
    if ($('#direccionRecibidos').is(':checked')) {
        recibidos = true;
    }

    var separadorIds = "!@!";
    var separadorCampos = ";c;";

    if (selections.length > 0) {
        var data = [];

        selections.map(function (row) {
            var partesId = new Array(row["sesion"], row["secuencia"]);

            if (!recibidos) {
                partesId.push(row["id_mensaje"]);
            }

            var strIdsMensaje = partesId.join(separadorCampos);
            data.push(strIdsMensaje);
        });

        var strIdsMensajes = data.join(separadorIds);
        var location = urlDescargarMailConSwifts + '?idsAttachments=' + strIdsMensajes;

        $.fileDownload(location)
            .done(function () { /* nada que alertar, con la ventanita del browser pidiendo para bajar el archivo alcanza */ })
            .fail(function () { showAlert("Error en la descarga.", "El archivo del mensaje no se pudo generar", "alert-danger") });
    }
}

function GuardarCasillas() {
    //if (confirm('Confirma que desea guardar los cambios?')) {
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
            ActualizarDropDownCasillasSegunConfigurado();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $('#modalAdminCasillas').modal('hide');
            showAlert("Error en la operación", "Los cambios no se pudieron guardar.", "alert-danger");
        }
    });
    return false;
}

function SetEstiloCheckbox(checkbox) {
    if (checkbox.is(':checked')) {
        checkbox.closest("tr").addClass("info");
    }
    else {
        checkbox.closest("tr").removeClass("info");

        var radioCorresponde = $('input[name=radCasillaPrincipal][value=' + checkbox.val() + ']');
        radioCorresponde.prop("checked", false).change();
    }
}

function ActualizarDropDownCasillasSegunConfigurado() {
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

var seleccionados = [];
//Document ready, inicializo controles
$(function () {
    $tablaSwifts = $('#tablaSwifts');
    $tablaSwifts.on("dbl-click-row.bs.table", ToggleSeleccionRow);
    $tablaSwifts.on("load-error.bs.table", OnTablaSwiftLoadError);
    $tablaSwifts.on("load-success.bs.table", OnTablaSwiftLoadSuccess);
    $tablaSwifts.on("page-change.bs.table", function () {
        $('#ddlFiltroEstado').val('0');
        //ActualizarCantSeleccionados();
    });

    $tablaSwifts.on("check.bs.table", function (event, row) {
        // Si el arreglo esta vacío hacemos push directo
        if (seleccionados.length === 0) {
            seleccionados.push(row);
        } else {
            // Validamos que no esté repetido
            var registroRepetido = false;
            for (var i = 0; i < seleccionados.length; i++) {
                if (seleccionados[i].SesionSecuencia === row.SesionSecuencia && seleccionados[i].id_mensaje === row.id_mensaje) {
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
                if (seleccionados[i].SesionSecuencia === row.SesionSecuencia && seleccionados[i].id_mensaje === row.id_mensaje) {
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

    //tooltips
    $('[data-toggle="tooltip"]').tooltip();

    //filtros
    InicializarFiltrosDeFecha();
    $('#btnBuscarSwifts').click(function () {
        //$("#btnReporteConsulta").show();
        Buscar();
    });

    $('#btnEstadisticasSwifts').click(function () {
        VerEstadisticas();
    });

    $("#btnMultipleFirmas").click(solicitarMultiplesFirmas);

    //$("#btnMultipleAnular").click(solicitarMultipleAnulacion);

    $("#ddlFiltrarEstadosPorAcciones").change(mostrarEstadosSegunAccion).change();

    //toolbar de la tabla
    ActualizarCantSeleccionados();
    $('#btnEncabezadoVerMultiples').click(function () {
        ArmarVisorMultiplesMensajes();
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

    $('#btnReporteConsulta').click(function () {
        ImprimirReporteConsulta();
    });

    $('#btnEnviarMultiplesMensajes').click(function () {
        EnviarMailMultiplesMensajes();
    });

    $('#ddlFiltroEstado').change(FiltrarResultadosPorEstado);

    $("input[name='filtroEstadoEnviados']:radio").change(function () {
        MostrarUOcultarCamposSegunFiltros();
    });

    $('#btnEnviarMensajeIndividual').click(function () {
        EnviarMailMensajeIndividual(ultimaRowAccionIndividual);
        $("#modalVisorIndividualSwift").modal('hide');
    });

    $('#btnImprimirMensajeIndividual').click(function () {
        $.ajax({
            type: "GET",
            cache: false,
            url: urlActualizaImp,
            success: function (resultado) {
                $("#modalActualizarImp").html(resultado);
                $('#modalActualizarImp').modal({ backdrop: true });
                $('#modalActualizarImp').modal('hide');
                $('#modalVisorIndividualSwift').modal('show');
                ImprimirMensajeIndividual(ultimaRowAccionIndividual);
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

        $tablaSwifts.bootstrapTable('refresh');
    });

    $('input[name=chkCasillaVisible]:checked').each(function () {
        SetEstiloCheckbox($(this));
    });

    $('input[name=direccion]').change(function () {
        var direccionEnviados = $("#direccionEnviados").is(":checked");
        $("#pnlFiltrosAvanzadosEnviados").toggle(direccionEnviados);

        MostrarUOcultarCamposSegunFiltros();  //si habia puesto "devueltos", se oculta la fecha, luego hace click en recibidos, y se tiene que restablecer la fecha
    });

    $("#btnAnulaSwift").click(anularSwift);

    $('#toolbar').hide();

    //hago que todo el encabezado del panel lo colapse/expanda, no solo el link
    $(".panel-heading").click(function (e) {
        var target = $(e.target);
        if (!target.is("a")) { //pq si disparo esto cuando hace click en el <a> del panel, se invoca recursivamente indefinidamente
            var linkAsociado = $(this).find('a');
            if (linkAsociado != null) {
                linkAsociado.click();
            }
        }
    });

    //activo el bloquear la UI en requests asincronos
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>', baseZ: 2000 }) })
        .ajaxStop($.unblockUI);

    //para todos los modales, capturo el show y seteo el height segun el tamaño de la pantalla
    $('.modal').on('show.bs.modal', function () {
        var modalDialog = $(this).find('.modal-dialog');
        if (modalDialog != null && (modalDialog.hasClass("modal-lg") || modalDialog.hasClass("xlg"))) {
            var modalBody = modalDialog.find('.modal-lg ');
            modalBody.css('overflow-y', 'auto');
            modalBody.css('max-height', $(window).height() * 0.8);
            modalBody.css('height', $(window).height() * 0.8);
        }
    });

    if (paraAccion >= 0 && paraAccion <= 2) {
        $("#ddlFiltrarEstadosPorAcciones").val(paraAccion).change();
    }
    else if (paraAccion == accionVerSinSolicitarFirmas) {
        $("#ddlFiltrarEstadosPorAcciones").val(2).change();
        $("#ddlFiltrarEstadosPorAcciones").change();
    }

    //admin de casillas
    $(':checkbox').change(function () {
        SetEstiloCheckbox($(this));
    });

    $('#btnGuardarCasillas').click(function () {
        GuardarCasillas();
    });

    $("input[name=radCasillaPrincipal]:radio").change(function () {

        if ($(this).is(':checked')) {
            var checkboxCorresponde = $('input[type=checkbox][value=' + $(this).val() + ']');
            checkboxCorresponde.prop("checked", true).change();
        }
    });

    EjecutarBusquedaPorDefecto();
});

function MostrarUOcultarCamposSegunFiltros() {
    var direccionEnviados = $("#direccionEnviados").is(":checked");

    if (direccionEnviados) {
        var val = $('input[name="filtroEstadoEnviados"]:checked').val();
        if (val == EstadoSwiftEnviado_Devuelto) {

            $("#grpFechaDesde").hide();
            $("#chkPeriodoFechas").prop('checked', false).change();
            $('#radIncluirSoloPropios').prop('checked', true).change();
            $('#radIncluirTodaLaCasilla').attr('disabled', true);
            return;
        }
    }

    $("#grpFechaDesde").show();
    $('#radIncluirTodaLaCasilla').attr('disabled', false);
}

function solicitarFirmas(row) {
    var idMensaje = row.id_mensaje;
    var monto = row.monto;
    var tipo = row.tipo_msg;
    var moneda = row.cod_moneda;
    var casilla = parseInt($('#idCasilla').val());
    var datos = '[{"idMensaje": "' + idMensaje + '", "tipo": "' + tipo + '", "moneda": "' + moneda + '", "monto": "' + monto.toString().replace(/\./g, ',') + '"}]';

    $.ajax({
        url: urlSolicitarFirmas,
        data: { idMensaje: idMensaje, monto: monto, casilla: casilla },
        type: "POST",
        success: function (data) {
            $('#divSolFirmasSwift').html(data);
            $('#modalSolFirmasSwift').modal({ backdrop: false });
            $('#tablaMensajes').bootstrapTable({
                height: 72,
                columns:
                [{
                    field: 'idMensaje',
                    title: 'N° Mensaje'
                }, {
                    field: 'tipo',
                    title: 'Tipo'
                }, {
                    field: 'moneda',
                    title: 'Moneda'
                }, {
                    field: 'monto',
                    title: 'Monto',
                    formatter: montoFormatter,
                    align: 'right'
                }],
                data: JSON.parse(datos)
            });
            onFirmasSuccess(firmasSolicitadasSuccess);  //este "handler" onFirmasSuccess está en SolFirmas.cshtml
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

function solicitarMultipleAnulacion() {
    var tabla = $('#tablaSwifts');
    var selections = seleccionados; //tabla.bootstrapTable('getAllSelections');

    if (selections.length === 0 || selections.length == 0) {
        return false;
    }

    var respuesta = false;
    $.each(selections, function (index, item) {
        var e = e || window.event;
        respuesta = confirmarContinuarConModificacion(item.rut_ingreso, "anular");
        e.preventDefault();
        if (respuesta) {
            ultimaRowAccionIndividual = item;
            $.ajax({
                type: "POST",
                cache: false,
                url: urlValidarAnularSwift,
                data: { idMensaje: item.id_mensaje },
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
                    if (resultado == "0") {
                        $("#nroMensajeAnular").text(item.id_mensaje);
                        $('#modalAnularSwift').modal({ backdrop: false });
                        $('#comentarioAnula').val('');
                    }
                    else {
                        showAlert("Error en la operación.", "El mensaje N° " + item.id_mensaje + " no puede ser anulado por que tiene una o más firmas autorizadas", "alert-danger", false);
                    }
                }
            });
        }
    });

}

function solicitarMultiplesFirmas() {

    var tabla = $('#tablaSwifts');
    var selections = seleccionados; //tabla.bootstrapTable('getAllSelections');

    if (selections.length === 0 || selections.length == 0) {
        return false;
    }

    var idMensajeArray = new Array();
    var montoArray = new Array();
    var tipoArray = new Array();
    var monedaArray = new Array();
    var casilla = parseInt($('#idCasilla').val());
    var datos = "[";

    $.each(selections, function (index, value) {
        idMensajeArray.push(value.id_mensaje);
        montoArray.push(value.monto.toString().replace(/\./g, ','));
        tipoArray.push(value.tipo_msg);
        monedaArray.push(value.cod_moneda);

        if (selections.length - 1 == index)
            datos += '{"idMensaje": "' + value.id_mensaje + '", "tipo": "' + value.tipo_msg + '", "moneda": "' + value.cod_moneda + '", "monto": "' + value.monto.toString().replace(/\./g, ',') + '"}]'
        else
            datos += '{"idMensaje": "' + value.id_mensaje + '", "tipo": "' + value.tipo_msg + '", "moneda": "' + value.cod_moneda + '", "monto": "' + value.monto.toString().replace(/\./g, ',') + '"},'
    });

    $.ajax({
        url: urlSolicitarMultiplesFirmas,
        data: { idMensaje: idMensajeArray, monto: montoArray, casilla: casilla },
        type: "POST",
        cache: false,
        traditional: true,
        success: function (data) {
            $('#divSolFirmasSwift').html(data);
            $('#modalSolFirmasSwift').modal({ backdrop: false });
            $('#tablaMensajes').bootstrapTable({
                height: JSON.parse(datos).length == 1 ? 72 :
                    JSON.parse(datos).length == 2 ? 100 :
                    JSON.parse(datos).length == 3 ? 128 :
                    JSON.parse(datos).length == 4 ? 156 :
                    JSON.parse(datos).length == 5 ? 184 : 184,
                columns:
                [{
                    field: 'idMensaje',
                    title: 'N° Mensaje'
                }, {
                    field: 'tipo',
                    title: 'Tipo'
                }, {
                    field: 'moneda',
                    title: 'Moneda'
                }, {
                    field: 'monto',
                    title: 'Monto'
                }],
                data: JSON.parse(datos)
            });            
            onFirmasSuccess(firmasSolicitadasSuccess);  //este "handler" onFirmasSuccess está en SolFirmas.cshtml
        },
        error: function (xhr, status) {
            showAlert("Error en la operación.", xhr.status + " " + xhr.statusText, "alert-danger", true);
        }
    });
}

var firmasSolicitadasSuccess = function () {
    $('#tablaSwifts').bootstrapTable('refresh');
    seleccionados = [];
    ActualizarCantSeleccionados();
}

function anularSwift() {

    var row = ultimaRowAccionIndividual;
    var comentario = $('#comentarioAnula').val();

    return $.ajax({
        type: "POST",
        cache: false,
        url: urlAnularSwift,
        data: { idMensaje: row.id_mensaje, comentario: comentario },
        error: function (response, type, message) {
            $tablaSwifts.bootstrapTable('refresh');
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
            $tablaSwifts.bootstrapTable('refresh');
            $("#modalAnularSwift").modal('hide');
            $('#comentarioAnula').val('');
            if (resultado == 0) {
                showAlert("Operación exitosa!", "El mensaje N° " + row.id_mensaje + " fue anulado con éxito", "alert-success", false);
            } else if (resultado == -4) {
                showAlert("Error en la operación.", "El mensaje N° " + row.id_mensaje + " no puede ser anulado por que tiene una o más firmas autorizadas", "alert-danger", false);
            } else {
                showAlert("Error en la operación.", "El mensaje N° " + row.id_mensaje + " no puede ser anulado", "alert-danger", true);
            }
        }
    });
}

function confirmarContinuarConModificacion(rutMensaje, verbo) {
    if (rutMensaje != rutUsuario) {
        var msg = "ATENCION! Ud. desea " + verbo + " un mensaje que fue ingresado por otro usuario.\nDesea continuar?";
        return window.confirm(msg);
    }
    else return true;
}

function TraerDatosDeSwiftYActualizarVisorConResultado(row, urlParaEnviados, urlParaRecibidos, funcionAdicionalOnSuccess) {
    var data = null;
    var url = null;

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
        cache: false,
        url: url,
        success: function (resultado) {
            $("#divCuerpoSwift").html(resultado);
            $('#modalVisorSwift').modal({ backdrop: true });

            if (funcionAdicionalOnSuccess) {
                funcionAdicionalOnSuccess();
            };
        },
        data: data,
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

window.operateEvents = {
    'click .verDatos': function (e, value, row, index) {
        TraerDatosDeSwiftYActualizarVisorConResultado(row, urlDatosSwiftEnviado, urlDatosSwiftRecibido, null);
    },
    'click .verLog': function (e, value, row, index) {
        TraerDatosDeSwiftYActualizarVisorConResultado(row, urlLogSwiftEnviado, urlLogSwiftRecibido, null);
    },
    'click .verMensaje': function (e, value, row, index) {
        ArmarVisorIndividual(row);
    },
    'click .descargarMensaje': function (e, value, row, index) {
        ultimaRowAccionIndividual = row;

        var location = urlDetalleSwift + '?sesion=' + row['sesion'] + '&secuencia=' + row['secuencia'] + '&htmlCompleto=true&pdf=true';

        if (!$('#direccionRecibidos').is(':checked')) {
            //enviados
            location = location + "&idMensaje=" + row['id_mensaje'];
        }

        window.open(location, "_blank");
    },
    'click .enviarMensaje': function (e, value, row, index) {
        EnviarMailMensajeIndividual(row);
    },
    'click .modificar': function (e, value, row, index) {
        //Validar que el swift no tenga solicitud de firmas
        $tablaSwifts.bootstrapTable('refresh');
        $.ajax({
            type: "GET",
            url: urlValidarFirmas + '?idMensaje=' + row['id_mensaje'],
            dataType: 'json',
            success: function (response) {
                if (response != null && response.success) {
                    var location = urlModificarSwift + '?idMensaje=' + row['id_mensaje'];
                    window.open(location, "_blank");
                } else {
                    showAlert("", response.responseText, "alert-danger");
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
            }
        });
    },
    'click .anular': function (e, value, row, index) {
        ultimaRowAccionIndividual = row;
        if (confirmarContinuarConModificacion(row.rut_ingreso, "anular")) {
            $.ajax({
                type: "POST",
                cache: false,
                url: urlValidarAnularSwift,
                data: { idMensaje: row.id_mensaje },
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
                    if (resultado == "0") {
                        $("#nroMensajeAnular").text(row.id_mensaje);
                        $('#modalAnularSwift').modal({ backdrop: false });
                        $('#comentarioAnula').val('');
                    }
                    else {
                        showAlert("Error en la operación.", "El mensaje N° " + row.id_mensaje + " no puede ser anulado por que tiene una o más firmas autorizadas", "alert-danger", false);
                    }
                }
            });

        }
    },
    'click .solicitarFirmas': function (e, value, row, index) {
        ultimaRowAccionIndividual = row;
        solicitarFirmas(row);
    },
};

$('#dtpFechaDesde').on("dp.show", function (e) {
    $('#dtpFechaHasta').data('DateTimePicker').hide();
});

$('#dtpFechaHasta').on("dp.show", function (e) {
    $('#dtpFechaDesde').data('DateTimePicker').hide();
});
