$(document).ready(function () {
    var baseUrl = $("#base_url").val();
    HabilitaDeshabilitaObjetosInit();
    //setMascara();
    //var setMascara = function () {
    //    $("#Tx_PlazaAladi_Text").initMascaraNumero();
    //}

    function HabilitaDeshabilitaObjetosInit() {   
        var checked = $("#ch_Aladi_Checked").is(':checked');   
        if (checked == true) {         
            $("#Tx_PlazaAladi_Text").removeAttr('disabled');
        } else {
            $("#Tx_PlazaAladi_Text").attr('disabled', 'disabled');        
        }
    }

    $('#TipoContainer input[type=checkbox]').change(function () {
        $.ajax({
            url: baseUrl + "AdminParticipantes/DatosBanco_Index_TipoBanco_Click",
            method: "Post",
            data: { elem: $(this).attr('id'), value: $(this).prop('checked') },
            success: function (data) {
                TipoBancoViewModelToView(data);
            },
            complete: function () { }
        });
    });

    function TipoBancoViewModelToView(data) {
        //cargo los mensajes de error
        loadMessages(data.MensajesDeError);
        $("#ch_Acreedor_Checked").prop("disabled", data.ch_Acreedor.Enabled ? false : true);
        $("#ch_Corresponsal_Checked").prop("disabled", data.ch_Corresponsal.Enabled ? false : true);
        $("#ch_Avisador_Checked").prop("disabled", data.ch_Avisador.Enabled ? false : true);
        $("#Tx_EjecutivoCorresponsal").enabled = data.Tx_EjecutivoCorresponsal.Enabled;
        var botonAceptar = $('#AceptarDetalle');
        if (data.BotonAceptar.Enabled)
            botonAceptar.removeAttr('disabled');
        else
            botonAceptar.attr('disabled', 'disabled');
    }


    $('#TasaContainer input').change(function () {
        var value = $('#TasaContainer input:checked').val();

        $.ajax({
            url: baseUrl + "AdminParticipantes/DatosBanco_PrtTasaRefinanciamiento_Click",
            method: "POST",
            data: { selectedValue: value },
            success: function (data) {
                DatosTasaRefinanciamientoViewModelToView(data);
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });


    function DatosTasaRefinanciamientoViewModelToView(data) {

        //cargo los mensajes de error
        loadMessages(data.MensajesDeError);
        var TasaList = $('input:radio[name=SelectedTasaRefinanciamiento]');
        //alert(TasaList);
        $.each(data.prtTasaRefinanciamiento, function (index, item) {
            TasaList[index].checked = item.Selected;
        });

        var botonAceptar = $('#BotonAceptar');
        if (data.BotonAceptar.Enabled)
            botonAceptar.removeAttr('disabled');
        else
            botonAceptar.attr('disabled', 'disabled');

    }


    $("#ch_Aladi_Checked").click(function () {
        $.post(baseUrl + "AdminParticipantes/DatosBanco_prtaladi_Click", { elem: $(this).attr('id'), value: $(this).prop('checked') }, function (dataResult) {
            if (dataResult != null) {
                if (dataResult.ch_Aladi.Checked) {
                    $('#Tx_PlazaAladi_Text').removeAttr('disabled');
                } else {
                    $('#Tx_PlazaAladi_Text').attr('disabled', 'disabled');
                    $('#Tx_PlazaAladi_Text').val('');
                }

                var botonAceptar = $('#BotonAceptar');

                if (dataResult.BotonAceptar.Enabled)
                    botonAceptar.removeAttr('disabled');
                else
                    botonAceptar.attr('disabled', 'disabled');
            }
        });
    });


    $('#Tx_Codigo_Text').change(function () {
        $.post(baseUrl + "AdminParticipantes/DatosBanco_prtcodigo_Change", { selectedValue: $(this).val() }, function (dataResult) {
            if (dataResult != null) {
                var botonAceptar = $('#BotonAceptar');
                if (dataResult.BotonAceptar.Enabled)
                    botonAceptar.removeAttr('disabled');
                else
                    botonAceptar.attr('disabled', 'disabled');
            }
        });
    });

    //Index 9 --> LostFocus Rut
    $("#Tx_Rut_Text").blur(function () {
        $.post(baseUrl + "AdminParticipantes/DatosBanco_prtrut_Blur", { selectedValue: $(this).val() }, function (dataResult) {
            if (dataResult != null) {
                $('#Tx_Rut_Text').val(dataResult.Tx_Rut.Text);

                var botonAceptar = $('#BotonAceptar');

                if (dataResult.BotonAceptar.Enabled)
                    botonAceptar.removeAttr('disabled');
                else
                    botonAceptar.attr('disabled', 'disabled');
            }
        });
    });

    $('#Tx_Rut_Text').change(function () {
        $.post(baseUrl + "AdminParticipantes/DatosBanco_prtrut_Change", { selectedValue: $(this).val() }, function (dataResult) {
            if (dataResult != null) {
                $('#Tx_Rut_Text').val(dataResult.Tx_Rut.Text);

                var botonAceptar = $('#BotonAceptar');

                if (dataResult.BotonAceptar.Enabled)
                    botonAceptar.removeAttr('disabled');
                else
                    botonAceptar.attr('disabled', 'disabled');
            }
        });
    });

    $('#Tx_SpreadTasaRefinanciamiento_Text').change(function () {
        $.post(baseUrl + "AdminParticipantes/DatosBanco_prtspread_Change", { selectedValue: $(this).val() }, function (dataResult) {
            if (dataResult != null) {
                //$('#Tx_SpreadTasaRefinanciamiento_Text').val(dataResult.Tx_Codigo.Text);
                var botonAceptar = $('#BotonAceptar');
                if (dataResult.BotonAceptar.Enabled)
                    botonAceptar.removeAttr('disabled');
                else
                    botonAceptar.attr('disabled', 'disabled');
            }
        });
    });

    $('#Tx_DireccionSwiff_Text').change(function () {
        $.post(baseUrl + "AdminParticipantes/DatosBanco_prtswif_Change", { selectedValue: $(this).val() }, function (dataResult) {
            if (dataResult != null) {
                var botonAceptar = $('#BotonAceptar');
                if (dataResult.BotonAceptar.Enabled)
                    botonAceptar.removeAttr('disabled');
                else
                    botonAceptar.attr('disabled', 'disabled');
            }
        });
    });

    $('#Tx_PlazaAladi_Text').change(function () {
        $.post(baseUrl + "AdminParticipantes/DatosBanco_prtplaza_Change", { selectedValue: $(this).val() }, function (dataResult) {
            if (dataResult != null) {
                var botonAceptar = $('#BotonAceptar');
                if (dataResult.BotonAceptar.Enabled)
                    botonAceptar.removeAttr('disabled');
                else
                    botonAceptar.attr('disabled', 'disabled');
            }
        });
    });

    $("#Tx_SpreadTasaRefinanciamiento_Text").inputmask({
        mask: '[9][9][9][9][9][9][9][9][9][9]9,99',
        numericInput: true,
        placeholder: '0',
        greedy: false
    });

    $(":input").inputmask();
});