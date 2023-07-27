$(function () {
    $("#btnGenerarExcel").off("Click");
    $("#btnGenerarExcel").click(function () {
        var periodo = $("#listaPeriodos_SelectedValue").val();
        EmptyMessageZone();
        var param = { listaPeriodos: periodo, Command: "Descarga" };
        DescargarArchivo(urlDescargaConsultaDevengamiento, 'descarga consulta devengamiento', urlDescarga, param);
        return false;
    });


});