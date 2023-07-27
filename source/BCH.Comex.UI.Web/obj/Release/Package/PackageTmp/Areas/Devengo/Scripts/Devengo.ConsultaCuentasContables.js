
$(function () {
    var dateNow = moment().startOf("day").utc();
    $('#dtpFechaDesde').datetimepicker({ format: 'DD/MM/YYYY', locale: 'es', defaultDate: dateNow, maxDate: dateNow, debug: true });
    $('#dtpFechaHasta').datetimepicker({ format: 'DD/MM/YYYY', locale: 'es', defaultDate: dateNow, maxDate: dateNow, debug: true });

    //$("#txtNumcta_Text").keypress(soloNumeros);
    //$("#txtCuentaCorriente_Text").keypress(soloNumeros);
    
    //Si se solicita que acepte K descomentar esta linea y sacar la subsiguientge
    $("#txtNumcta_Text").inputmask("9{1,7}[K]{1}");
    //$("#txtNumcta_Text").inputmask("9{1,8}");
    $('#txtCuentaCorriente_Text').inputmask("9{*}");

    //$("#btnGenerarExcel").off("Click");
    //$("#btnGenerarExcel").click(function () {
    //    EmptyMessageZone();
    //    DescargarArchivo(urlDescargaConsultaCuentasContables, 'descarga consulta cuentas contables');
    //});


    $("#txtNemcta_Text").change(function () { toUppercase(this) });
    

    $('input[name=FiltroSelected]').on('click',
        function () {
            switch (this.value) {
                case "1":
                    $("#txtNemcta_Text").removeAttr("disabled");
                    $("#txtCentroCosto_Text").removeAttr("disabled");
                    $("#txtNumcta_Text").attr("disabled", "disabled");
                    $("#txtNumcta_Text").val("");
                    $("#txtCuentaCorriente_Text").removeAttr("disabled");
                    break;
                case "2":
                    $("#txtNumcta_Text").removeAttr("disabled");
                    $("#txtCentroCosto_Text").removeAttr("disabled");
                    $("#txtNemcta_Text").attr("disabled", "disabled");
                    $("#txtNemcta_Text").val("");
                    $("#txtCuentaCorriente_Text").removeAttr("disabled");
                    break;
                default:
                    $("#txtNumcta_Text").attr("disabled", "disabled");
                    $("#txtNumcta_Text").val("");

                    $("#txtNemcta_Text").attr("disabled", "disabled");
                    $("#txtNemcta_Text").val("");

                    $("#txtCentroCosto_Text").attr("disabled", "disabled");
                    $("#txtCentroCosto_Text").val("");

                    //$("#txtCuentaCorriente_Text").attr("disabled", "disabled");
                    //$("#txtCuentaCorriente_Text").val("");

                    break;
            }
        });

    if (modelCompleto.ListaErrores != null && modelCompleto.ListaErrores.Length != 0) {
        focusOnErrorControl(modelCompleto.ListaErrores);
    }
});


var soloNumeros = function (evt) {
    evt = (evt) ? evt : window.event
    var charCode = (evt.which) ? evt.which : evt.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        status = "This field accepts numbers only."
        return false
    }
    status = ""
    return true
}