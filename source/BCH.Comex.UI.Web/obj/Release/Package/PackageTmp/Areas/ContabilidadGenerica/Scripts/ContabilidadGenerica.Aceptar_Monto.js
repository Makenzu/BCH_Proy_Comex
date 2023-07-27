$(document).ready(function () {
    var baseUrl = $("#base_url").val() + "ContabilidadGenerica/"; //obtengo la url base global
    $("#cancelar").on("click", function () {
        window.location = baseUrl + "Home/Aceptar_Monto_2/false";
    });
    $("#aceptar").on("click", function () {
        window.location = baseUrl + "Home/Aceptar_Monto_2/true";
    });
});