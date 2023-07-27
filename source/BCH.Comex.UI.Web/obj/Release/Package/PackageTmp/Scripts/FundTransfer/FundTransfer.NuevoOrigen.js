$(document).ready(function () {
    var baseUrl = $("#base_url").val();
    var urlTo = $("#urlTo").val();

    var viewModel = {
        volverADefinir: function () {
            window.location.href = baseUrl + "FundTransfer/"+urlTo+"?hayMensaje=true&respuestaMensaje=true";
        },
        noVolverADefinir: function () {
            window.location.href = baseUrl + "FundTransfer/" + urlTo + "?hayMensaje=true&respuestaMensaje=false";
        }
    };
    ko.applyBindings(viewModel, $("#NuevosOrigenes").get(0));

    $("#btn_si").focus();
});