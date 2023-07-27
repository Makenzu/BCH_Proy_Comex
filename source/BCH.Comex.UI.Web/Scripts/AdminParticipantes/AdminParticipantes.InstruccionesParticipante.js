//$(document).ready(function () {
$(function () {
    var baseUrl = $("#base_url").val();
    $('#toolbarMainPrty').hide();
    $('#mnuMainArchivo').hide();
    $('#mnuMainOpciones').hide();
    var frmInstruccionesEspeciales = $("#frmInstruccionesEspeciales");
    contenido_textarea = ""
    num_caracteres_permitidos = 250
    //    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
    //.ajaxStop($.unblockUI);

    $("#CmbMemo_SelectedValue").change(function () {
        var selectedValue = $(this).val();
        $.post(baseUrl + "AdminParticipantes/InstruccionesEspeciales_CmbMemo_Click", { selectedValue: selectedValue }, function (dataresult) {
            if (dataresult != null) {
                InstruccionesEspecialesViewModelToView(dataresult);
                contenido_textarea = "";
                cuenta();
            }
        });
    });

    function InstruccionesEspecialesViewModelToView(data) {
        $('#prtinstruc_Text').val(data.prtinstruc.Text);
        if (data.prtinstruc.Enabled)
            $('#prtinstruc_Text').attr('disabled', false);
        else
            $('#prtinstruc_Text').attr('disabled', true);
    }

    //function InstruccionesEspecialesViewModelToView(data) {
    //    var botonAceptar = $('#Aceptar');
    //    $('#prtinstruc_Text').val(data.prtinstruc.Text);
    //    if (data.prtinstruc.Enabled) {
    //        $('#prtinstruc_Text').attr('disabled', false);
    //        botonAceptar.removeAttr('disabled');
    //    }

    //    else {
    //        $('#prtinstruc_Text').attr('disabled', true);
    //        botonAceptar.attr('disabled', 'disabled');
    //    }
    //}

    $(document).ready(function () {
        $("#prtinstruc_Text").keydown(function () {
            valida_longitud();
        });
    });

    $(document).ready(function () {
        $("#prtinstruc_Text").keyup(function () {
            valida_longitud();
        });
    });

    function valida_longitud() {
        num_caracteres = $('#prtinstruc_Text').val().length + 1;

        if (num_caracteres <= num_caracteres_permitidos + 1) {
            contenido_textarea = $('#prtinstruc_Text').val()
        } else {
            $('#prtinstruc_Text').val(contenido_textarea)
        }

        cuenta();
    }

    function cuenta() {
        var cantidad = $('#prtinstruc_Text').val().length;

        if (cantidad < 10) {
            $('#caracteres').val('00' + cantidad)
            return;
        }

        if (cantidad < 100) {
            $('#caracteres').val('0' + cantidad)
            return;
        }

        if (cantidad >= 100) {
            $('#caracteres').val(cantidad)
            return;
        }
    }

    $("#prtinstruc_Text").change(function () {      
        $.post(baseUrl + "AdminParticipantes/InstruccionesEspeciales_prtinstruc_Change", function (dataresult) {
            if (dataresult != null) {
                //  loadMessages(dataresult.MensajesDeError);
            }
        });
    });

    $("#prtinstruc_Text").blur(function () {
        //alert("Handler for .blur() called.");
        var instrucciones = $("#prtinstruc_Text").val();

        $.post(baseUrl + "AdminParticipantes/InstruccionesEspeciales_prtinstruc_LostFocus", { instrucciones: instrucciones }, function (dataresult) {
            if (dataresult != null) {
                // loadMessages(dataresult.MensajesDeError);
                var cmbInstrucciones = $('#CmbMemo_SelectedValue');
                var traeListaInstrucciones = dataresult.CmbMemo.Items
                cmbInstrucciones.html('');

                $.each(traeListaInstrucciones, function (id, option) {
                    cmbInstrucciones.append($('<option></option>').val(option.Data).html(option.Value));
                });

                cmbInstrucciones.val(dataresult.CmbMemo.ListIndex);

                var botonAceptar = $('#Aceptar');

                if (dataresult.Aceptar.Enabled)
                    botonAceptar.removeAttr('disabled');
                else
                    botonAceptar.attr('disabled', 'disabled');
            }
        });
    });

    //$("#Aceptar").click(function () {       
    //    var data = frmInstruccionesEspeciales.serialize();
    //    $.post(baseUrl + "AdminParticipantes/InstruccionesEspeciales_Aceptar", data, function (dataresult2) {
    //        if (dataresult2 != null) {            
    //            window.location.href = $('#urlIndex').data('url');
    //        }

    //    });
    //})
});