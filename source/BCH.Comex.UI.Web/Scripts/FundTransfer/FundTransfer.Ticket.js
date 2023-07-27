$(document).ready(function () {
    $(document).ajaxStart(function () {
        $.blockUI({ message: "<h6>Espere un momento ...</h6>" });
    }).ajaxStop($.unblockUI);

    var baseUrl = $("#base_url").val();
    var viewModel = null;
    var ticket = $("#ticket");
    var updateModel = function (newViewModel) {
        ko.mapping.fromJS(newViewModel, viewModel);
        //obtengo los errores y los muestro
        var errors = ko.mapping.toJS(viewModel.Errors);
        loadMessages(errors);
    };

    var ignoreList = ["otro_change", "cb_ticket_change", "cb_ticket_Blur","aceptar","cancelar"];
    
    var ticketViewModel = function (data) {
        ko.mapping.fromJS(data, {}, this);
        var that = this;
        that.otro_change = function () {
            AjaxCall("FundTransfer/TICKGR_otro_Change", function () {
                $("#check").prop('checked', viewModel.otro.Checked());
            });
        };
        that.cb_ticket_change = function () {
            var index = $("#Cb_ticket").get(0).selectedIndex;
            viewModel.Cb_ticket.ListIndex(index);
            AjaxCall("FundTransfer/TICKGR_Cb_Ticket_Change");
        };
        //that.cb_ticket_Blur = function () {
        //    AjaxCall("FundTransfer/TICKGR_Cb_Ticket_Change"); 
        //};
        that.aceptar = function (e) {
            
            $('#aceptar').attr('disabled', true);
            $('#cancelar').attr('disabled', true);

            viewModel.Errors([]);
            $.ajax({
                method: "POST",
                url: baseUrl + "FundTransfer/TICKGR_PostModel",
                data: {
                    jsonModel: ko.mapping.toJS(viewModel, {
                        ignore: ignoreList
                    })
                },
                success: function (a) {
                    updateModel(a);
                    window.location.href = baseUrl + "FundTransfer/TICKGR_Aceptar";
                },
                error: function (err) {
                    $('#aceptar').attr('disabled', false);
                    $('#cancelar').attr('disabled', false);
                }
            });
        };
        that.cancelar = function () {
            AjaxCall("FundTransfer/TICKGR_PostModel", function () {
                window.location.href = baseUrl + "FundTransfer/TICKGR_Cancelar";
            });
        };
    }

    function AjaxCall(url, func) {
        viewModel.Errors([]);
        $.ajax({
            method: "POST",
            url: baseUrl + url,
            data: {
                jsonModel: ko.mapping.toJS(viewModel, {
                    ignore: ignoreList
                })
            },
            success: function (a) {
                updateModel(a);
                if (func) {
                    func();
                }
            },
            error: function (err) {
                //console.error(err.responseText);
            }
        });
    }
    
    $.ajax({
        method: "GET",
        cache: false,
        url: baseUrl + "FundTransfer/TICKGR_Form_Load",
        success: function (data) {
            viewModel = new ticketViewModel(data);
            ko.applyBindings(viewModel);
            $("#aceptar").focus();
        }
    });


});