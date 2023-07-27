$(document).ready(function () {
    var baseUrl = $("#base_url").val();

    $("#txtPar").focus().select();

    $("form").validate({
        
    });

    $("#txtPar").blur(function () {
        var string = numeral($("#txtPar").val()).format("##,###0.000000000");
        $("#txtPar").val(string.substring(0, 8));
    });

    $("#txtCam").blur(function () {
        var string = numeral($("#txtCam").val()).format("##,###0.0000");
        $("#txtCam").val(string.substring(0, 15));
    });

    function cancel() {
        window.location.href = baseUrl + "FundTransfer/INGVAL_Cancelar_Click";
    }

    $("#btnCancelar").click(cancel);

    $('#txtCam').DetectarTecla(function () {
        $('#btnAceptar').click();
    });

});