$(document).ready(function () {
    var baseUrl = $("#base_url").val();

    //activo el bloquear la UI en requests asincronos
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI);

    function viewModelToView(viewModel) {
        function loadTxs(viewModel) {
            for (var i = 0; i < 4; i++) {
                $("#Tx_MtoVisE_00" + i).val(viewModel["Tx_MtoVisE_00" + i]);
            }
        }
        function mostrarMensajesDeError(viewModel) {
            loadMessages(viewModel.MensajesDeError);
            focusOnErrorControl(viewModel.MensajesDeError);
        };
        loadTxs(viewModel);
        mostrarMensajesDeError(viewModel);
    }

    $("#idCbMnd").blur(function () {
        var index = $("#idCbMnd").find(":selected").index() - 1;
        $.ajax({
            url: baseUrl + "FundTransfer/COMVISEXP_Cb_Mnd_Blur/" + index,
            method: "GET",
            cache: false,
            success: function (a) {
                viewModelToView(a);
            }
        });
    });

    var tx_Blur = function (i) {
        return function () {
            $.ajax({
                url: baseUrl + "FundTransfer/COMVISEXP_Tx_MtoVisE_Blur",
                method: "POST",
                data: { id: i, text: $("#Tx_MtoVisE_00" + i).val() },
                success: function (a) {
                    viewModelToView(a);
                }
            });
        };
    };
    for (var i = 0; i < 4; i++) {
        $("#Tx_MtoVisE_00" + i).blur(tx_Blur(i));
    }

    $("#btnCancel").click(function (i) {
        $.ajax({
            url: baseUrl + "FundTransfer/COMVISEXP_Co_Boton_Click/1",
            cache: false,
            success:function(a){
                if (a.success) {
                    window.location.href = baseUrl + "FundTransfer/COMVISEXP_Finish";
                }
            }
        });
    });

    $("#btnAccept").click(function (i) {
        $.ajax({
            url: baseUrl + "FundTransfer/COMVISEXP_Co_Boton_Click/0",
            cache: false,
            success: function (a) {
                if (a.success) {
                    window.location.href = baseUrl + "FundTransfer/COMVISEXP_Finish";
                } else {
                    viewModelToView(a);
                }
            }
        });
    });

    Init();
    function Init() {
        $("#idCbMnd").focus().select();
    }
});