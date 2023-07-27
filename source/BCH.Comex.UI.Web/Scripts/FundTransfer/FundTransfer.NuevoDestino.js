$(document).ready(function () {
    var baseUrl = $("#base_url").val();
    var viewModel = function() {
        this.volverADefinir = function(){
            window.location.href = baseUrl + "FundTransfer/DestinoFondos?hayMensaje=true&respuestaMensaje=true";
        },
        this.noVolverADefinir = function () {
            window.location.href = baseUrl + "FundTransfer/DestinoFondos?hayMensaje=true&respuestaMensaje=false";
        }
    };
    ko.applyBindings(new viewModel());

    $("#btn_si").focus();
});