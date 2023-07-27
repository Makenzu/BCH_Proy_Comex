$(function () {
    var baseUrl = $("#base_url").val(); //obtengo la url base global

    //activo el bloquear la UI en requests asincronos
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI);

    $('#Cbo_Moneda_SelectedValue').change(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillasTransferencia_SeleccionarMoneda_Click",
            method: "Post",
            data: { selectedValue: $('#Cbo_Moneda_SelectedValue').find(":selected").index() - 1 },
            success: function (data) {
                PlanillasTransferenciaViewModelToView(data);
            },
            complete: function () {
            }
        });
    });
    $('#Cbo_CptoPln_SelectedValue').change(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillasTransferencia_SeleccionarTcp_Click",
            method: "Post",
            data: { selectedValue: $('#Cbo_CptoPln_SelectedValue').find(":selected").index() - 1},
            success: function (data) {
            },
            complete: function () {
            }
        });
    });
    $('#CbPais_SelectedValue').change(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillasTransferencia_SeleccionarPais_Click",
            method: "Post",
            data: { selectedValue: $('#CbPais_SelectedValue').find(":selected").index() - 1 },
            success: function (data) {
            },
            complete: function () {
            }
        });
    });
    $('#ListaPlanillas_SelectedValue').click(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillasTransferencia_ListaPlanillas_Click",
            method: "Post",
            data: { selectedValue: $('#ListaPlanillas_SelectedValue').find(":selected").index() },
            success: function (data) {
                var itemDatos = data.model;
                if (itemDatos != null) {
                    FundTransfer.Common.MapComboBox($('#Cbo_Moneda_SelectedValue'), itemDatos.Cbo_Moneda);
                    FundTransfer.Common.MapComboBox($('#Cbo_CptoPln_SelectedValue'), itemDatos.Cbo_CptoPln);
                    FundTransfer.Common.MapComboBox($('#CbPais_SelectedValue'), itemDatos.CbPais);
                    $("#Lb_Saldo_Text").val(itemDatos.Lb_Saldo.Text);
                    $("#Tx_Monto_Text").val(itemDatos.Tx_Monto.Text);
                    $('#btnAccept').removeAttr('disabled');
                }

            },
            complete: function () {
            }
        });
    });
    $('#Tx_Monto_Text').change(function () {
        var val = $("#Tx_Monto_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillasTransferencia_Tx_Monto_LostFocus_Blur",
            method: "Post",
            data: { text:val },
            success: function (data) {
                PlanillasTransferenciaViewModelToView(data);
            },
            complete: function () {
            }
        });
    });
    $('#btnAdd').click(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillasTransferencia_OKClick",
            method: "Post",
            data: {},
            success: function (data) {
                var itemDatos = data.model;
                if (itemDatos != null) {
                    $('#Cbo_CptoPln_SelectedValue').val('');
                    $('#CbPais_SelectedValue').val('');
                    $('#Tx_Monto_Text').val(itemDatos.Tx_Monto.Text);
                    $('#Lb_Saldo_Text').val(itemDatos.Lb_Saldo.Text);
                    FundTransfer.Common.MapListBox($('#ListaPlanillas_SelectedValue'), itemDatos.ListaPlanillas);
                    $('#btnAccept').removeAttr('disabled');
                }

                mostrarMensajesDeError(data);

            },
            complete: function () {
            }
        });
    });
    $('#btnDel').click(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillasTransferencia_EliminarClick",
            method: "Post",
            data: { },
            success: function (data) {
                _PlanillasTransferenciaViewModelToView(data);
                $('#btnAccept').removeAttr('disabled');
            },
            complete: function () {
            }
        });
    });

    function PlanillasTransferenciaViewModelToView(model) {
      var itemDatos = model.model;
        if (itemDatos != null) {
            $("#Lb_Saldo_Text").val(itemDatos.Lb_Saldo.Text);
            $("#Tx_Monto_Text").val(itemDatos.Tx_Monto.Text);
        }
    }
    function _PlanillasTransferenciaViewModelToView(model) {
        var itemDatos = model.model;
        if (itemDatos != null) {
            $('#Cbo_CptoPln_SelectedValue').val('');
            $('#CbPais_SelectedValue').val('');
            $('#Tx_Monto_Text').val(itemDatos.Tx_Monto.Text);
            FundTransfer.Common.MapComboBox($('#ListaPlanillas_SelectedValue'), itemDatos.ListaPlanillas);
            mostrarMensajesDeError(model);
        }
    }
    function mostrarMensajesDeError(viewModel) {
        var msgErr = viewModel.model._MensajesDeError;
        msgErr = $.map(msgErr, function (elem, index) {
            return { Type: 3, Text: elem, Title: "Error de Validación: " };
        });
        loadMessages(msgErr);
    };
});
