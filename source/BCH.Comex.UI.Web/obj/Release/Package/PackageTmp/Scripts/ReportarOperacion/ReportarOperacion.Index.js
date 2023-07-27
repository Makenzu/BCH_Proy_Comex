$(document).ready(function () {

    InicializarFiltroDeFecha();

    var baseUrl = $("#base_url").val(); //obtengo la url base global

    $('#btnReportar').click(function () {
        var nroOperacion = $('#nroOperacion').val();
        var descripcion = $('#descripcionProblema').val();
        var fecha = $('#dtpFecha').data("DateTimePicker").date().format("YYYY-MM-DD");

        if (!S(nroOperacion).isEmpty() && nroOperacion.length >= 15) {
            $.blockUI;

            // se reemplaza el texto para dejar el numero
            nroOperacion = nroOperacion.replace(/[^0-9\.]+/g, "");

            var data = {
                numeroOperacion: nroOperacion,
                descripcionProblema: descripcion,
                fechaLogs: fecha
            };

            descargarArchivo(baseUrl + "ReportarOperacion/DescargarArchivosOperacion", data);
        }
        else {
            showAlert("Atención", "Debe ingresar un numero de operacion cuyo valor debe tener un largo de 15 caracteres", "alert-warning", true, "alerta");
        }
    });

    function InicializarFiltroDeFecha() {
        var dateNow = moment().startOf("day").utc();
        $('#dtpFecha').datetimepicker({ format: 'DD/MM/YYYY', locale: 'es', focusOnShow: false, defaultDate: dateNow, maxDate: dateNow, debug: true });
    }

    var descargarArchivo = function (location, data) {

        $.blockUI({ message: '<h6>Generando archivo...</h6>' })

        $.fileDownload(location, {
            httpMethod: 'POST',
            dataType: "json",
            contentType: "application/json",
            data: data
        })
        .done(function () { $.unblockUI(); })
        .fail(function () { showAlert("Error en la descarga.", "El archivo no se pudo generar", "alert-danger", true, "alerta"); $.unblockUI(); });
    };
});