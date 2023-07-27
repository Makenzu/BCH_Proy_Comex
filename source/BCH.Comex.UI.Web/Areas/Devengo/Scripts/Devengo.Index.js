$(function () {
    $("#DescargarDiferenciaDevengo").off("Click");
    $("#DescargarDiferenciaDevengo").click(function () {
        EmptyMessageZone();
        var param = {};
        DescargarArchivo(urlDescargaDiferenciaDevengamiento, 'descarga diferencia devengo', urlDescarga);
        return false;
    });

    $("#descargaOperacionesCDR").off("Click");
    $("#descargaOperacionesCDR").click(function () {
        EmptyMessageZone();
        var param = {};
        DescargarArchivo(urlDescargaOperacionesCDR, 'descarga operaciones CDR', urlDescarga);
        return false;
    });
    
});