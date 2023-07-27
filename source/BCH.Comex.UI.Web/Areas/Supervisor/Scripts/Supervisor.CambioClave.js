$(function () {
    var baseUrl = $("#base_url").val();
    $("#btnCancelar").click(function () { window.location.href = baseUrl + "Supervisor"; });
});