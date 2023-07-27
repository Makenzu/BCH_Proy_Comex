var baseUrl = $("#base_url").val();
var records;
var urlEnTablaYaSeteada = false;

$(document).ready(function () {
    
    var dateNow = moment().startOf("day").utc();
    $('#txtFechaIngreso').datetimepicker({
        format: 'YYYY'
        , locale: 'es'
        , defaultDate: dateNow
        , maxDate: dateNow
        , debug: true
    });

    $('#cmb_estado').change(filtrar);
    load();

    //$("#btnModalNo").on('click', function () {
    //    $('#txtFechaIngreso').focus();
    //});

    //$("#btnModalSi").click(function () {
    //    $("#myModal").modal("hide");
    //    filtrar();
    //});

    $('.export').hide();

    $(".columns.columns-right.btn-group.pull-right").appendTo("#AccionBotonesGrid");
});


function exportar() {

    $.fileDownload(urlExportarAExcel, {
        httpMethod: "POST",
        data: { data: ko.mapping.toJS(records) },
        successCallback: function (url) {
            $.blockUI({ message: '<h6>Cargando...</h6>' });
        },
        failCallback: function (responseHtml, url) {
            $.unblockUI;
            $('.dropdown-menu li[data-type="excel"] a').click();
        }
    });
}

function load() {
    inicializarGrid(urlCargarOperacionesPage);
    
    $('#gridOperaciones').bootstrapTable('load', dataInicial);
    gridOnLoadSuccess(dataInicial); //pq la primera vez como la data no se carga desde url, no se dispara el evento
    loadMessages(mensajesIniciales);
    $('#gridOperaciones').bootstrapTable('resetView');
}

function inicializarGrid(cargarUrl) {
    var paramsParaTabla = {
        onLoadSuccess: gridOnLoadSuccess,
        onDblClickRow: function (row, $element) {
            $.ajax({
                url: baseUrl + "FundTransfer/msg_operaciones_DblClick/",
                data: { data: row },
                method: "POST",
                success: function (data) {
                    //updateModel(data);
                    window.location.href = baseUrl + "FundTransfer/";
                }
            });
        },
        method: 'post',
        //height: 640,
        queryParams: getQueryParamsPage,// getQueryParamsPage , //getQueryParams,
        pagination: true,
        pageSize: 6,
        pageList: [6,10, 25, 50, 100, 200],
        sidePagination: 'client', //a pesar de que no hay paginacion, esta setting hace que la busqueda sea del lado del client tambien 
        search: true,
        showRefresh: true,
        cache: false,
        clickToSelect: true,
        singleSelect: true,
        rowStyle: rowFormatter,
        toolbar: "#toolbar",
        showExport: true,
        exportTypes: ['excel'],
        exportDataType: 'all',
        searchAlign: 'left'
    };

    if (cargarUrl)
    {
        paramsParaTabla.url = urlCargarOperaciones; //urlCargarOperacionesPage; //urlCargarOperaciones;
    }

    $('#gridOperaciones').bootstrapTable(paramsParaTabla);
    $(".pull-left.search").css("margin-top", "20px");
}

function gridOnLoadSuccess(data)
{
    if (data === 0) {
        showAlert('Consulta: ', 'No se han encontrado registros', 'alert-info');
    }

    $('#txt_tot_oper').val(data.length);
    records = data;
}

function getQueryParams()
{
    return {
        estado: $('#cmb_estado').val()
    };
}

function getQueryParamsPage(p) {
    return {
        estado: $('#cmb_estado').val(),
        year: ($('#txtFechaIngreso').data('date') === '' || $('#txtFechaIngreso').data('date') === undefined ? 0 : $('#txtFechaIngreso').data('date'))
    };
}

function filtrar()
{
    if (urlEnTablaYaSeteada) {
        $('#gridOperaciones').bootstrapTable('refresh');
    }
    else {
        $('#gridOperaciones').bootstrapTable('destroy');
        $('#txt_tot_oper').val("");
        urlEnTablaYaSeteada = true;
        inicializarGrid(urlEnTablaYaSeteada);
        $('.export').hide();
    }
}

function filtrarPorEstado() {
    EmptyMessageZone();

    if ($('#txtFechaIngreso').data('date') === '' || $('#txtFechaIngreso').data('date') === undefined) {
        $("#myModal").modal("show");
    } else {
        filtrar();
    }
}

function limpiar() {
    $('#txt_tot_oper').val("");
    $('#txt_tot_oper').val("0");

    $('#gridOperaciones').bootstrapTable('removeAll');
}

function salir() {
    $.ajax({
        url: baseUrl + "FundTransfer/Frm_Consulta_Salir/",        
        method: "POST",
        success: function (data) {
            window.location.href = baseUrl + "FundTransfer/";
        }
    });
}

function montoFormatter(value, row, index) {
    return numeral(value).format("0,0.00");
}

function rowFormatter(row, value, index) {
    return {
        css: { "color": row.color }
    };
}