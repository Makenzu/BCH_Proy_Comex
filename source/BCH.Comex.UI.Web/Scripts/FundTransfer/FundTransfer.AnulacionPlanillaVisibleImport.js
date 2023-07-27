
$(function () {
    var baseUrl = $("#base_url").val(); //obtengo la url base global

    $(document).ajaxStart(function () {
        $.blockUI({ message: '<h6>Cargando...</h6>' })
    }).ajaxStop($.unblockUI);


    $("#Lt_PlAnul").change(AnulacionPlanillaVisibleImport_LtClick);

    $("#Lt_PlAnul").click(AnulacionPlanillaVisibleImport_LtClick);

    function AnulacionPlanillaVisibleImport_LtClick()
    {
        $.ajax({
            url: baseUrl + "FundTransfer/AnulacionPlanillaVisibleImport_LtClick",
            method: "POST",
            data: { selectedValue: $('#Lt_PlAnul').val() },
            success: function (data) {
                AnulacionPlanillaVisibleImportViewModelToView(data, null);
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    }

    $("#Tx_NroOpe_000_Text").change(function () {
        var val = $("#Tx_NroOpe_000_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/AnulacionPlanillaVisibleImport_Tx_NroOpe_Blur",
            data: { id: 0, text: val },
            method: "POST",
            success: function (data) {
                AnulacionPlanillaVisibleImportViewModelToView(data, null);                
            }
        });
    });
    $("#Tx_NroOpe_001_Text").change(function () {
        var val = $("#Tx_NroOpe_001_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/AnulacionPlanillaVisibleImport_Tx_NroOpe_Blur",
            data: { id: 1, text: val },
            method: "POST",
            success: function (data) {
                AnulacionPlanillaVisibleImportViewModelToView(data, $("#Tx_NroOpe_004_Text"));
            }
        });
    })
    .focusin(function () {
        $(this).trigger("select");
    });
    $("#Tx_NroOpe_002_Text").change(function () {
        var val = $("#Tx_NroOpe_002_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/AnulacionPlanillaVisibleImport_Tx_NroOpe_Blur",
            data: { id: 2, text: val },
            method: "POST",
            success: function (data) {
                AnulacionPlanillaVisibleImportViewModelToView(data, null);
            }
        });
    });
    $("#Tx_NroOpe_003_Text").change(function () {
        var val = $("#Tx_NroOpe_003_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/AnulacionPlanillaVisibleImport_Tx_NroOpe_Blur",
            data: { id: 3, text: val },
            method: "POST",
            success: function (data) {
                AnulacionPlanillaVisibleImportViewModelToView(data, null);
            }
        });
    });
    $("#Tx_NroOpe_004_Text").change(function () {
        var val = $("#Tx_NroOpe_004_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/AnulacionPlanillaVisibleImport_Tx_NroOpe_Blur",
            data: { id: 4, text: val },
            method: "POST",
            success: function (data) {
                AnulacionPlanillaVisibleImportViewModelToView(data, null);
            }
        });
    });

    $("#Tx_FecPre_Text").blur(function () {
        var val = $("#Tx_FecPre_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/AnulacionPlanillaVisibleImport_Tx_FechaPre_Blur",
            data: { text: val },
            method: "POST",
            success: function (data) {
                AnulacionPlanillaVisibleImportViewModelToView(data, null);
                $('[tabindex="5"]').focus();
            }
        });
    });

    function AnulacionPlanillaVisibleImportViewModelToView(data, objecto) {
        FundTransfer.Common.MapTextBox($('#Tx_MtoAnu'), data.Tx_MtoAnu);
        FundTransfer.Common.MapTextBox($('#Tx_TipCam'), data.Tx_TipCam);
        FundTransfer.Common.MapTextBox($('#Tx_ObsAnu'), data.Tx_ObsAnu);
        FundTransfer.Common.MapCheckBox($('#Ch_Reemp'), data.Ch_Reemp);

        $("#Tx_NroOpe_000_Text").val(data.Tx_NroOpe_000.Text);
        $("#Tx_NroOpe_001_Text").val(data.Tx_NroOpe_001.Text);
        $("#Tx_NroOpe_002_Text").val(data.Tx_NroOpe_002.Text);
        $("#Tx_NroOpe_003_Text").val(data.Tx_NroOpe_003.Text);
        $("#Tx_NroOpe_004_Text").val(data.Tx_NroOpe_004.Text);
        if (objecto != null) {
            console.log(objecto[0].maxLength);
            objecto.selectRange(0, objecto[0].maxLength);
        }
    }
    $(":input").inputmask();

    if ($('#Cb_TipAut_SelectedValue').val() != 6) {
        $('#Cb_TipAut_SelectedValue').val('6');
    }

    if ($("#Lt_PlAnul option").length > 0) {
        $("#Lt_PlAnul option:first").attr('selected', 'selected').focus();
        $('#Lt_PlAnul').focus().click();
    } else{
        $('[tabindex="1"]').focus().select();
    }

    if (MensajesDeError != null || MensajesDeError != undefined) {
        focusOnErrorControl(MensajesDeError);
    }

    /// Cuando haga Enter en cualquier parte de la pantalla
    $(window).keydown(function (ev) {
        var keycode = (ev.keyCode ? ev.keyCode : ev.which);
        if (keycode == 9) {  // Presiona Tab
            RecorrerTabIndex(-1, ev);
        }

        if (keycode == 13) {  // Presiona Enter
            ev.preventDefault();
            if ($("#btnAccept").is(":focus") || $("#Lt_PlAnul option").length > 0) {
                //$.blockUI({ message: '<h6>Cargando...</h6>' });
                $("#btnAccept").click();
            } else {
                //$.blockUI({ message: '<h6>Cargando...</h6>' });
                $("#btnComando").click();
            }
        }
    });
});