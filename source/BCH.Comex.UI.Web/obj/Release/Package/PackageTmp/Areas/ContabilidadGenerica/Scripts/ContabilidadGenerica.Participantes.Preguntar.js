$(document).ready(function () {
    var baseUrl = $("#base_url").val(); //obtengo la url base global
    $("#crear").click(function () {
        window.location.replace(baseUrl + "ContabilidadGenerica/Participantes/Crear");
    });
    $("#consultar").click(function () {
        window.location.replace(baseUrl + "ContabilidadGenerica/Participantes/Consultar");
    });
    $("#cancelar").click(function () {
        window.location.replace(baseUrl + "ContabilidadGenerica/Participantes/Identificar");
    });
});