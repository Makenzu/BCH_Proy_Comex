$(function () {
    $("#btnGenerarExcel").off("Click");
    $("#btnGenerarExcel").click(function () {
        var periodo = $("#listaPeriodos_SelectedValue").val();
        EmptyMessageZone();
        var param = { listaPeriodos: periodo, Command: "Descarga" };
        DescargarArchivo(urlDescargaDevengamientoHistorico, 'descarga devengamiento histórico', urlDescarga, param);
        return false;
    });
});