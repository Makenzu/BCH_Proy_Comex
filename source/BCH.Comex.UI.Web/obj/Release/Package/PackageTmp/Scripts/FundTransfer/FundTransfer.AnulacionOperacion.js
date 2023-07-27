$(function () {
    var baseUrl = $("#base_url").val(); //obtengo la url base global
    //activo el bloquear la UI en requests asincronos
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI);

    if ($("#Lt_Pln_Text").val().trim().length > 1)
    { $('[tabindex=7]').focus().select(); }

    if (modelCompleto != null)
    {
        if (modelCompleto.Co_Boton_0.Enabled)
            $("[tabindex='4']").prop("disabled", true);
        else
            $("[tabindex='4']").prop("disabled", false);

        if (modelCompleto.MensajesDeError != null && modelCompleto.MensajesDeError.length > 0)
            $("[tabindex='4']").prop("disabled", true);
    }
    else 
        $("[tabindex='4']").prop("disabled", true);

    $("#Tx_NroOpe_0_Text").change(function () {
        var val = $("#Tx_NroOpe_0_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/AnulacionOperacion_Tx_NroOpe_Blur",
            data: { id: 0, text: val },
            method: "POST",
            success: function (data) {
                GanuViewModel(data);
                //$("#Tx_NroOpe_1_Text").focus().select();
            }
        });
    });
    $("#Tx_NroOpe_1_Text").change(function () {
        var val = $("#Tx_NroOpe_1_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/AnulacionOperacion_Tx_NroOpe_Blur",
            data: { id: 1, text: val },
            method: "POST",
            success: function (data) {
                GanuViewModel(data);
                //$("#Tx_NroOpe_2_Text").focus().select();
            }
        });
    });
    $("#Tx_NroOpe_2_Text").change(function () {
        var val = $("#Tx_NroOpe_2_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/AnulacionOperacion_Tx_NroOpe_Blur",
            data: { id: 2, text: val },
            method: "POST",
            success: function (data) {
                GanuViewModel(data);
                //$("#Tx_NroOpe_3_Text").focus().select();
            }
        });
    });
    $("#Tx_NroOpe_3_Text").change(function () {
        var val = $("#Tx_NroOpe_3_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/AnulacionOperacion_Tx_NroOpe_Blur",
            data: { id: 3, text: val },
            method: "POST",
            success: function (data) {
                GanuViewModel(data);
                //$("#Tx_NroOpe_4_Text").focus().select();
            }
        });
    });
    $("#Tx_NroOpe_4_Text").change(function () {
        var val = $("#Tx_NroOpe_4_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/AnulacionOperacion_Tx_NroOpe_Blur",
            data: { id: 4, text: val },
            method: "POST",
            success: function (data) {
                GanuViewModel(data);
            }
        });
    });

    function GanuViewModel(model) {
        $("#Tx_NroOpe_0_Text").val(model.Tx_NroOpe_0.Text);
        $("#Tx_NroOpe_1_Text").val(model.Tx_NroOpe_1.Text);
        $("#Tx_NroOpe_2_Text").val(model.Tx_NroOpe_2.Text);
        $("#Tx_NroOpe_3_Text").val(model.Tx_NroOpe_3.Text);
        $("#Tx_NroOpe_4_Text").val(model.Tx_NroOpe_4.Text);
    }
    $(":input").inputmask();

    if ($('#Tx_Prty_Text').val() == '' || $('#Lt_Pln_Text').val() == '') {
        $('[tabindex="1"]').focus().select();
    }
    else {
        $("[tabindex='4']").focus();
    }

    /// Cuando haga Enter en cualquier parte de la pantalla
    $(window).keydown(function (ev) {
        var keycode = (ev.keyCode ? ev.keyCode : ev.which);
        if (keycode == 9) {  // Presiona Tab
            RecorrerTabIndex(-1, ev);
        }

        if (keycode == 13) {  // Presiona Enter
            ev.preventDefault();
            if ($("[tabindex='4']").is(":enabled")) {
                $("[tabindex='4']").click();
            } else {
                $("[tabindex='3']").click();
            }
        }
    });
});
