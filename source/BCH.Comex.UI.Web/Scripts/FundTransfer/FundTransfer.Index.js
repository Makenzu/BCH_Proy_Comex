$(function () {

    $('[data-toggle="tooltip"]').tooltip()

    var baseUrl = $("#base_url").val(); //obtengo la url base global

    //activo el bloquear la UI en requests asincronos
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI).ajaxError($.unblockUI);

    $('#mnuConfiguracion input[type=checkbox]').change(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/Index_ConfigImprimirClick",
            method: "Post",
            data: { elem: $(this).attr('id'), value: $(this).prop('checked') },
            success: function (data) {
                PlanillaVisibleExportViewModelToView(data);
            },
            complete: function () {

            }
        });
    });

    $("#Referencia_Text").change(function () {
        var val = $("#Referencia_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/Index_Tx_RefCli_Blur",
            data: { text: val },
            method: "POST",
            success: function (data) {
                indextViewModelToView(data);
            }
        });
    });

    function indextViewModelToView(model) {
        if (model != null) {
            $("#Referencia_Text").val(model.Referencia.Text);
        }
    }

    $('.btnMenu').click(function () {
        if ($(this).attr("href") != "#") {
            $(this).attr('disabled', 'disabled');
            $.blockUI({ message: "<h6>Cargando...</h6>" });

        }
    });

    $(window).on("pageload", function () {
        $.unblockUI;
    });

    $(window).on("unload", function () {
        $.blockUI({ message: "<h6>Espere un momento ...</h6>" });
    });

    if ($("[focus='true']").is(":enabled") || !$("[focus='true']").is(":disabled")) {
        $("[focus='true']").focus().select();
    } else {
        var tab = $(document.activeElement).attr("tabindex");
        RecorrerTabIndex(tab, ev);
    }

    // Cuando haga Enter en cualquier parte de la pantalla
    $(document).keydown(function (ev) {
        var keycode = (ev.keyCode ? ev.keyCode : ev.which);
        if (keycode == 9) {  // Presiona Tab
            RecorrerTabIndex(-1, ev);
        }
    });

});