$(function () {
    var baseUrl = $("#base_url").val(); //obtengo la url base global
    //activo el bloquear la UI en requests asincronos
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI);

    $('#Cb_Producto_SelectedValue').change(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/EmitirNotaCredito_Producto_Change",
            method: "Post",
            data: {
                selectedValue: $('#Cb_Producto_SelectedValue').val()
                , indexValue: $('#Cb_Producto_SelectedValue')[0].selectedIndex
            },
            success: function (data) {
                EmitirNotaCreditoModelToView(data)
            },
            complete: function () {

            }
        });
    });

    function Formato(value, formato)
    {
        return numeral(value).format(formato);
    }
    function GeneraFormato(largo)
    {
        var format = "";
        for (i = 0; i < largo; i++)
        {
            format += "0";
        }
        return format;
    }

    if ($('#Tx_MtoOri_Text').val() != '') {
        $('#Boton_000').focus();
    } else {
        $('[tabindex="1"]').focus();
    }

    
    $("#Tx_NumOpe_000_Text").change(function () {
        var val = $("#Tx_NumOpe_000_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/EmitirNotaCredito_Tx_NroOpe_Blur",
            data: { id: 0, text: val },
            method: "POST",
            success: function (data) {
                EmitirNotaCreditoModelToView(data);
                //$("#Tx_NumOpe_002_Text").focus().select();
            }
        });
    });
    $("#Tx_NumOpe_001_Text").change(function () {
        var val = $("#Tx_NumOpe_001_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/EmitirNotaCredito_Tx_NroOpe_Blur",
            data: { id: 1, text: val },
            method: "POST",
            success: function (data) {
                EmitirNotaCreditoModelToView(data);
                //$("#Tx_NumOpe_002_Text").focus().select();
            }
        });
    });
    $("#Tx_NumOpe_002_Text").change(function () {
        var val = $("#Tx_NumOpe_002_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/EmitirNotaCredito_Tx_NroOpe_Blur",
            data: { id: 2, text: val },
            method: "POST",
            success: function (data) {
                EmitirNotaCreditoModelToView(data);
                //$("#Tx_NumOpe_003_Text").focus().select();
            }
        });
    });
    $("#Tx_NumOpe_003_Text").change(function () {
        var val = $("#Tx_NumOpe_003_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/EmitirNotaCredito_Tx_NroOpe_Blur",
            data: { id: 3, text: val },
            method: "POST",
            success: function (data) {
                EmitirNotaCreditoModelToView(data);
                //$("#Tx_NumOpe_004_Text").focus().select();
            }
        });
    });
    $("#Tx_NumOpe_004_Text").change(function () {
        var val = $("#Tx_NumOpe_004_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/EmitirNotaCredito_Tx_NroOpe_Blur",
            data: { id: 4, text: val },
            method: "POST",
            success: function (data) {
                EmitirNotaCreditoModelToView(data);
                //$("#Tx_NumOpe_005_Text").focus().select();

            }
        });
    });
    $("#Tx_NumOpe_005_Text").change(function () {
        var val = $("#Tx_NumOpe_005_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/EmitirNotaCredito_Tx_NroOpe_Blur",
            data: { id: 5, text: val },
            method: "POST",
            success: function (data) {
                EmitirNotaCreditoModelToView(data);
                //$("#Tx_NumOpe_006_Text").focus().select();
            }
        });
    });
    $("#Tx_NumOpe_006_Text").change(function () {
        var val = $("#Tx_NumOpe_006_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/EmitirNotaCredito_Tx_NroOpe_Blur",
            data: { id: 6, text: val },
            method: "POST",
            success: function (data) {
                EmitirNotaCreditoModelToView(data);
            }
        });
    });

    function EmitirNotaCreditoModelToView(model) {
        var tx0 = $('#Tx_NumOpe_000_Text');
        tx0.val(model.Tx_NumOpe_000.Text);
        if (model.Tx_NumOpe_000.Enabled)
        {
            tx0.removeAttr('disabled');
        } else {
            tx0.attr('disabled', 'disabled');
        }

        var tx1 = $('#Tx_NumOpe_001_Text');
        tx1.val(model.Tx_NumOpe_001.Text);
        if (model.Tx_NumOpe_001.Enabled) {
            tx1.removeAttr('disabled');
        } else {
            tx1.attr('disabled', 'disabled');
        }

        var tx2 = $('#Tx_NumOpe_002_Text');
        tx2.val(model.Tx_NumOpe_002.Text);
        if (model.Tx_NumOpe_002.Enabled) {
            tx2.removeAttr('disabled');
        } else {
            tx2.attr('disabled', 'disabled');
        }

        var tx3 = $('#Tx_NumOpe_003_Text');
        tx3.val(model.Tx_NumOpe_003.Text);
        if (model.Tx_NumOpe_003.Enabled) {
            tx3.removeAttr('disabled');
        } else {
            tx3.attr('disabled', 'disabled');
        }

        var tx4 = $('#Tx_NumOpe_004_Text');
        tx4.val(model.Tx_NumOpe_004.Text);
        if (model.Tx_NumOpe_004.Enabled) {
            tx4.removeAttr('disabled');
        } else {
            tx4.attr('disabled', 'disabled');
        }

        var tx5 = $('#Tx_NumOpe_005_Text');
        tx5.val(model.Tx_NumOpe_005.Text);
        if (model.Tx_NumOpe_005.Enabled) {
            tx5.removeAttr('disabled');
        } else {
            tx5.attr('disabled', 'disabled');
        }

        var tx6 = $('#Tx_NumOpe_006_Text');
        tx6.val(model.Tx_NumOpe_006.Text);
        if (model.Tx_NumOpe_006.Enabled) {
            tx6.removeAttr('disabled');
        } else {
            tx6.attr('disabled', 'disabled');
        }
    }
    $(":input").inputmask();

    $(document).submit(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) });

    /// Cuando haga Enter en cualquier parte de la pantalla
    $(document).keydown(function (ev) {
        var keycode = (ev.keyCode ? ev.keyCode : ev.which);
        if (keycode == 9) {  // Presiona Tab
            RecorrerTabIndex(-1, ev);
        }

        if (keycode == 13) {  // Presiona Enter
            ev.preventDefault();

            if ($("#Boton_000").is(":focus")) {
                $('#Boton_000').click();
            } else {
                $('#bot_Ok').click();
            }
        }

    });
});