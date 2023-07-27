$(document).ready(function () {

    function CheckTextAvailability() {
        var isChecked = $("#noRecibirAlertas").is(":checked");
        var txtIntervalo = $("#intervaloMinutos");
        txtIntervalo.prop("disabled", isChecked);

        if (isChecked) {
            txtIntervalo.val("0");
        }
    }

    function OnlyNumbers(e) {
        var charCode = (e.which) ? e.which : e.keyCode;
        var leftArrow = (e.keyCode == 37), rightArrow = (e.keyCode == 39);
        if (leftArrow || rightArrow) {
           
        } else {
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
        }
    }

    $("#noRecibirAlertas").on("click", CheckTextAvailability);
    $("#intervaloMinutos").on("keypress", OnlyNumbers);
    $("#cancelar").on("click", function () {
        window.location = $("#urlCancel").val();
    });
    $("#guardar").on("click", function () {
        window.location = $("#urlAccept").val() + "/" + ($("#noRecibirAlertas").is(":checked") ? 0 : $("#intervaloMinutos").val());
    });
    CheckTextAvailability();

    $("#intervaloMinutos").bind('input propertychange', function () {
        var maxLength = $(this).attr('maxlength');
        if ($(this).val().length > maxLength) {
            $(this).val($(this).val().substring(0, maxLength));
        }
    });
});