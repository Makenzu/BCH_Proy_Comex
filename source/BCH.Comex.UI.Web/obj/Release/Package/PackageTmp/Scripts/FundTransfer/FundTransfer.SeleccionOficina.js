$(function () {
    var baseUrl = $("#base_url").val(); //obtengo la url base global

    //activo el bloquear la UI en requests asincronos
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI);

    $("#btnAceptar").focus().select();
    loadMessages(modelCompleto.MensajesDeError);
    focusOnErrorControl(modelCompleto.MensajesDeError);
    
    $('#btnAceptar').click(function () {
        $("#FrmOfi").submit(function () {
            $("#btnAceptar").attr("disabled", "disabled");
            $("#btnCancelar").attr("disabled", "disabled");
            $(this).append('<input type="hidden" name="cmdButton" value="Aceptar" />');
        });
    });

    //$('#Cb_Oficina').change(function (item) {
    //    $.ajax({
    //        url: baseUrl + "FundTransfer/SeleccionOficina_Cb_Oficina_Click",
    //        method: "POST",
    //        data: { selectedValue: $('#Oficina').val() },
    //        success: function (data) {
    //            FrmOfiViewModel(data);
    //        },
    //        error: function (data) {
    //            alert('Ha ocurrido un error');
    //        }
    //    });
    //});

    //$('#btnAceptar').click(function (item) {
    //    $.ajax({
    //        url: baseUrl + "FundTransfer/SeleccionOficina_Aceptar",
    //        method: "POST",
    //        //data: { codParticipante: $('#KeyText').val() },
    //        success: function (data) {
    //            //FrmganuViewModel(data);

    //            if (data.Redireccionar) {
    //                window.location = data.Redireccionar;
    //            }
    //        },
    //        error: function (data) {
    //            alert('Ha ocurrido un error');
    //        }
    //    });
    //});

    //$('#btnCancelar').click(function (item) {
    //    $.ajax({
    //        url: baseUrl + "FundTransfer/SeleccionOficina_Cancelar",
    //        method: "POST",
    //        //data: { codParticipante: $('#KeyText').val() },
    //        success: function (data) {
    //            //FrmganuViewModel(data);

    //            if (data.Redireccionar) {
    //                window.location = data.Redireccionar;
    //            }
    //        },
    //        error: function (data) {
    //            alert('Ha ocurrido un error');
    //        }
    //    });
    //});


});