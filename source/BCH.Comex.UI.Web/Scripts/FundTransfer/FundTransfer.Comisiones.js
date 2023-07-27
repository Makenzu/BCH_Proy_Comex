$(function () {
    var baseUrl = $("#base_url").val(); //obtengo la url base global
    var isBlur = true;
    ////pongo el foco en el elemento seleccionado
    //$('#CmbComisiones_SelectedValue').val($('#CmbComisiones_SelectedValue :selected').val());

    //activo el bloquear la UI en requests asincronos
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI);

    $("#Tx_Com_1_Text").blur(function () {
        if (isBlur) {
            $.ajax({
                url: baseUrl + "FundTransfer/Comisiones_Tx_Com_Text_Blur",
                data: { id: 1, text: $(this).val() },
                method: "POST",
                success: function (data) {
                    $("#Tx_Com_3_Text").val(data.Tx_Com_3.Text);
                }
            });
        }
    });    

    $("#Tx_Com_1_Text").focus(function (e) {
        isBlur = true;
        $(this).select();
    });

    $("#Tx_Com_2_Text").focus(function (e) {
        isBlur = true;
        $(this).select();
    });

    $("#Tx_Com_4_Text").focus(function (e) {
        isBlur = true;
        $(this).select();
    });

    $("#Tx_Com_2_Text").blur(function () {        
        if (isBlur) {
            $.ajax({
                url: baseUrl + "FundTransfer/Comisiones_Tx_Com_Text_Blur",
                data: { id: 2, text: $(this).val() },
                method: "POST",
                success: function (data) {
                    $("#Tx_Com_3_Text").val(data.Tx_Com_3.Text);
                }
            });
        }
    });

    $("#Tx_Com_4_Text").blur(function () {
        $(this).val($(this).val().toUpperCase());
        //if (isBlur) {
            $.ajax({
                url: baseUrl + "FundTransfer/Comisiones_Tx_Com_Text_Blur",
                data: { id: 4, text: $(this).val() },
                method: "POST",
                success: function (data) {
                    $("#Tx_Com_5_Text").val(data.Tx_Com_5.Text);
                }
            });
        //}
    });

    $('#CmbComisiones_SelectedValue').change(function () {        
        $.ajax({
            url: baseUrl + "FundTransfer/SeleccionarComision_Click",
            method: "Post",
            data: { selectedValue: $('#CmbComisiones_SelectedValue').find(":selected").index() },
            success: function (data) {
                ListaComisionesViewModelToView(data);
            },
            complete: function () {
            }
        });
    });

    $('#CmbComisiones_SelectedValue').mousedown(function () {
        isBlur = false;
    });

    function ListaComisionesViewModelToView(model) {        
        var itemDatos = model.model;
        if (itemDatos != null) {
            $("#Tx_Com_0_Text").val(itemDatos.Tx_Com_0.Text);
            $("#Tx_Com_1_Text").val(itemDatos.Tx_Com_1.Text);
            $("#Tx_Com_2_Text").val(itemDatos.Tx_Com_2.Text);
            $("#Tx_Com_3_Text").val(itemDatos.Tx_Com_3.Text);
            $("#Tx_Com_4_Text").val(itemDatos.Tx_Com_4.Text);
            $("#Tx_Com_5_Text").val(itemDatos.Tx_Com_5.Text);         
            $("#Ch_com_Checked").prop("checked", itemDatos.Ch_com.Checked);
        }
    }

    if ($('#CmbComisiones_SelectedValue :selected').val() != "0") {
        $('[tabindex="7"]').focus();
    } else {
        $('[tabindex="1"]').focus();
        //pongo el foco en el elemento seleccionado (2 veces para que funcione en IE8) 
        $('#CmbComisiones_SelectedValue').val($('#CmbComisiones_SelectedValue :selected').val());
        $('#CmbComisiones_SelectedValue').val($('#CmbComisiones_SelectedValue :selected').val());
        $('#CmbComisiones_SelectedValue').focus();
    }

    $("#Tx_Com_2_Text").change(agregaTabIndexTipoCambio);
    agregaTabIndexTipoCambio();
    
    /// Cuando haga Enter en cualquier parte de la pantalla
    $(window).keydown(function (ev) {
        var keycode = (ev.keyCode ? ev.keyCode : ev.which);
        if (keycode == 9) {  // Presiona Tab
            RecorrerTabIndex(-1, ev);
        }

        if (keycode == 13) {  // Presiona Enter
            ev.preventDefault();
            if ($("[tabindex='7']").is(":focus")) {
                $("[tabindex='7']").click();
            } else {
                $("[tabindex='6']").click();
            }
        }
    });

    // Mejora Foco en campo de Error
    focusOnErrorControl(modelCompleto.MensajesDeError);
});

function agregaTabIndexTipoCambio() {
    var tipoCambio = $("#Tx_Com_2_Text");
    if (tipoCambio.val() == "" || parseInt(tipoCambio.val()) == 0) {
        tipoCambio.prop("tabindex", "4");
    } else {
        tipoCambio.removeAttr("tabindex");
    }
}