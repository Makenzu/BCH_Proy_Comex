
var primeraVezTablaInyectar = true; //ya estoy trayendo datos la 1era vez que se carga la pagina, viene en el ViewModel, 
var primeraVezTablaReversar = true; //pero la tabla me hace un ajax request igual, tengo esta variable para no devolver nada esa 1era vez

function BuscarParaInyectar() {
     var ie_fix = new Date().getTime();

    $('#tablaInyectar').bootstrapTable({
        classes: 'table table-hover resultRow',
        method: 'post',
        data: cargosYAbonosParaInyectar,
        url: urlCargarDatos,
        queryParams: function (p) {
            return {
                operacion: 0, //inyectar
                ignorarRequest: primeraVezTablaInyectar,
                ie_fix: ie_fix
            };
        },
        columns: GetColumns(),
        pagination: true,
        sidePagination: 'client',
        search: true,
        searchAlign: 'left',
        clickToSelect: false,
        sortable: true,
        pageSize: 10,
        pageList: [10, 25, 50, 100, 200],
        locale: "es-SP",
        maintainSelected: true,
        showRefresh: false,
        toolbar: "#toolbarInyectar",
        cache: false,
        onLoadSuccess: ActualizarCantidadInyectar,
        onLoadError: ActualizarCantidadInyectar
    });

    $('#tablaInyectar').on("dbl-click-row.bs.table", InyectarRow);
}

function BuscarParaReversar() {
    $('#tablaReversar').bootstrapTable({
        classes: 'table table-hover resultRow',
        method: 'post',
        data: cargosYAbonosParaReversar,
        url: urlCargarDatos,
        queryParams: function (p) {
            var ie_fix = new Date().getTime();

            return {
                operacion: 1, //reversar
                ignorarRequest: primeraVezTablaReversar,
                ie_fix: ie_fix
            };
        },
        columns: GetColumns(),
        pagination: true,
        sidePagination: 'client',
        search: true,
        searchAlign: 'left',
        clickToSelect: false,
        sortable: true,
        pageSize: 10,
        pageList: [10, 25, 50, 100, 200],
        locale: "es-SP",
        maintainSelected: true,
        showRefresh: false,
        toolbar: "#toolbarReversar",
        cache: false,
        onLoadSuccess: ActualizarCantidadReversar,
        onLoadError: ActualizarCantidadReversar,
        rowStyle: rowStyle,
        rowAttributes: rowAttributes
    });


    $('#tablaReversar').on("dbl-click-row.bs.table", ReversarRow);
}

function ActualizarCantidadInyectar(data)
{
    if (primeraVezTablaInyectar) {
        primeraVezTablaInyectar = false;
        $('#badgeInyectar').text(cargosYAbonosParaInyectar.length);
    }
    else if (data == null) {
        //no deberia estar entrando aca, si se llama esto como resultado de un refresh deberia traer data siempre. por las dudas igual se agrega el caso
        var dataFiltrada = $('#tablaInyectar').bootstrapTable('getData', { useCurrentPage: false });
        $('#badgeInyectar').text(dataFiltrada.length);
    }
    else {
        $('#badgeInyectar').text(data.length);
    }
}

function ActualizarCantidadReversar(data)
{
    if (primeraVezTablaReversar) {
        primeraVezTablaReversar = false;
        $('#badgeReversar').text(cargosYAbonosParaReversar.length);
    }
    else if (data == null) {
        //no deberia estar entrando aca, si se llama esto como resultado de un refresh deberia traer data siempre. por las dudas igual se agrega el caso
        var dataFiltrada = $('#tablaReversar').bootstrapTable('getData', { useCurrentPage: false });
        $('#badgeReversar').text(dataFiltrada.length);
    }
    else {
        $('#badgeReversar').text(data.length);
    }
}

function RefreshTablas(inyectando, ambas) {
    if (inyectando || ambas) {
        $('#tablaInyectar').bootstrapTable('refresh', { silent: true, query: { ie_fix: new Date().getTime() } });
    }
        
    if (!inyectando || ambas) { 
        $('#tablaReversar').bootstrapTable('refresh', { silent: true, query: { ie_fix: new Date().getTime() } });
    }

    //Se deselecciona todos los registros de la grilla.
    if (document.selection != null) {
        document.selection.empty();
    }
}

function InyectarRow(row, $element) {
    InyectarOReversar($element, true);
}

function ReversarRow(row, $element) {
    InyectarOReversar($element, false);
}

function InyectarOReversar(element, inyectando)
{
    var url;
    var verbo;

    if (inyectando) {
        url = urlInyectar;
        verbo = "Inyectando";
    }
    else {
        url = urlReversar;
        verbo = "Reversando";
    }

    if (element.IdAux != null) {
        var data = { idAux: element.IdAux };

        limpiarTodosLosErrores();
        $.blockUI({ message: '<h4>' + verbo + ', por favor espere...</h4>' })

        return $.ajax({
            type: "GET",
            cache:false,
            url: url,
            data: data,
            success: function (resultado) {
                loadMessages(resultado.Mensajes);
                RefreshTablas(inyectando, true);
            },
            error: function (response, type, message) {
                try {
                    //intento parsear la respesta como json.
                    var responseJson = JSON.parse(response.responseText);
                    showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", false);
                }
                catch (err) {
                    showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", false);
                }
                RefreshTablas(inyectando, true);
            }
        });
    }
    else {
        showAlert("Operación inválida.", "La operación que intenta inyectar/reversar no tiene un identificador válido", "alert-danger", false);
    }
}

function GetColumns() {
    return [
        { field: 'IdAux', visible: false, sortable: false },
        { field: 'PosEnLista', title: '#', sortable: false },
        { field: 'NroOperacionSinRaya', title: 'Nro. Operación', sortable: true },
        { field: 'numcct', title: 'Cta. Cte.', sortable: true },
        { field: 'tip_cta', title: 'T. Cta.', sortable: true},
        { field: 'nomcli', title: 'Nombre', sortable: true },
        { field: 'cod_dh', title: 'A / C', sortable: true, formatter: codDHFormatter, },
        { field: 'moneda', title: 'Moneda', sortable: true },
        { field: 'mtomcd', title: 'Monto', sortable: true, formatter: montoFormatter, align: 'right' },
        { field: 'nrorpt', title: 'Nro. Reporte', sortable: true },
        { field: 'fecmov', title: 'Fecha Mov.', sortable: true, formatter: fechaFormatter },
        { field: 'trx_id', title: 'Transaction ID', sortable: true }
    ];
}

function montoFormatter(value, row, index) {
    if (row['moneda'] == 'CLP') {
        return numeral(value).format("0,0");
    }
    else {
        return numeral(value).format("0,0.00");
    }
}

function codDHFormatter(value, row, index) {
    if (value == 'H') {
        return "Abono";
    }
    else {
        return "Cargo";
    }
}

function fechaFormatter(value, row, index) {
    if (value != null) {
        return moment(value).format("DD/MM/YYYY");
    }
}

function DescargarExcelCargosYAbonos()
{
    $.fileDownload(urlExportarAExcel)
        .done(function () { /* nada que alertar, con la ventanita del browser pidiendo para bajar el archivo alcanza */ })
        .fail(function () { showAlert("Error en la descarga.", "No se pudieron exportar los datos a Excel", "alert-danger") });
}

function limpiarTodosLosErrores() {
    $(".lblMensajeError").remove();  //elimino cualquier label de error
    $("div.form-group.has-error").removeClass("has-error");
    $("#msg-zone").html("");
}

function rowStyle(row, index) {
    if (row["TrxIdRepetido"]) {
        return {
            classes: 'danger'
        };
    }
    return {};
}

function rowAttributes(row, index) {
    if (row["TrxIdRepetido"]) {
        return {
            dataToggle: "tooltip",
            dataPlacement: "top",
            title: "Alerta, El TrxId de esta operación se encuentra repetido. Si necesita reversar, favor contactarse con soporte."
        };
    }
    return {};
}

$(function () {
    var baseUrl = $("#base_url").val();
    $(":input").inputmask();

    $(window).resize(function () {
        $('#tablaInyectar').bootstrapTable('resetView');
        $('#tablaReversar').bootstrapTable('resetView');
    });

    $('#tabInyectar').tab();
    $('#tabReversar').tab();

    $('#btnExcelAbonosYCargosInyectar').click(function () {
        DescargarExcelCargosYAbonos();
    });
    $('#btnExcelAbonosYCargosReversar').click(function () {
        DescargarExcelCargosYAbonos();
    });

    $('#btnRefreshInyectar').click(function () { RefreshTablas(true, false) });
    $('#btnRefreshReversar').click(function () { RefreshTablas(false, false) });

    $('#btnVolver').click(function () {
        window.location = baseUrl + "Supervisor/";
    });

    BuscarParaInyectar();
    BuscarParaReversar();

    //cuando el ajax vuelve, desbloqueo el block UI si es que estaba bloqueado
    $(document).ajaxStop($.unblockUI);
    
});


