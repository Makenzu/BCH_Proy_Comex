$(function () {
    var baseUrl = $("#base_url").val(); //obtengo la url base global

    //$('#btnAceptar').click(function (item) {
    //    $.ajax({
    //        url: alert(baseUrl) + "FundTransfer/PlanillaIngresoVisExport_Aceptar_Click",
    //        method: "POST",
    //        data: { codParticipante: $('#KeyText').val() },
    //        success: function (data) {
    //            ParticipantesViewModelToView(data);

    //            if (data.Redireccionar) {
    //                window.location = data.Redireccionar;
    //            }

    //        },
    //        error: function (data) {
    //            alert('Ha ocurrido un error');
    //        }
    //    });
    //});


    //$("#FechaCalculoPicker").datetimepicker()
    //      .on('changeDate', function (e) {

    //      });
    $('#FechaPicker').datetimepicker({
        locale: 'es',
        format: 'DD/MM/YYYY'
        //format: 'LT'
    });



});