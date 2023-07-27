$(document).ready(function () {
    $(document).ajaxStart(function () {
        $.blockUI({ message: "<h6>Espere un momento ...</h6>" });
    }).ajaxStop($.unblockUI);

    var baseUrl = $("#base_url").val()+"ContabilidadGenerica/";
    var viewModel = null;
    var ticket = $("#ticket");
    var updateModel = function (newViewModel) {
        ko.mapping.fromJS(newViewModel, viewModel);
        //obtengo los errores y los muestro
        var errors = ko.mapping.toJS(viewModel.ListaErrores);
        loadMessages(errors);
    };

    var ignoreList = ["otro_change", "cb_ticket_change", "cb_ticket_Blur", "aceptar", "cancelar"];

    var ticketViewModel = function (data) {
        ko.mapping.fromJS(data, {}, this);
        var that = this;
        that.otro_change = function () {
            AjaxCall("Tickets/otro_Change", function () {
                $("#check").prop('checked', viewModel.otro.Checked());
            });
        };
        that.cb_ticket_change = function () {
            var index = $("#Cb_ticket").get(0).selectedIndex;
            viewModel.Cb_ticket.ListIndex(index);
            var txt = viewModel.Cb_ticket.Items()[index].Value();
            viewModel.CAM_Concepto.Text(txt);
        };
        that.aceptar = function () {
           
            $('#aceptar').attr('disabled', true);
            $('#cancelar').attr('disabled', true);
            viewModel.ListaErrores([]);
            $.ajax({
                method: "POST",
                url: baseUrl + "Tickets/Aceptar",
                data: {
                    jsonModel: ko.mapping.toJS(viewModel, {
                        ignore: ignoreList
                    })
                },
                success: function (a) {
                    updateModel(a);
                    window.location.href = baseUrl + "Grabar/Ticket_Post";
                },
                error: function (err) {
                    $('#aceptar').attr('disabled', false);
                    $('#cancelar').attr('disabled', false);
                }
            });

        };
        that.cancelar = function () {
            window.location.href = baseUrl + "Tickets/Cancelar";
        };
    }

    function AjaxCall(url, func) {
        viewModel.ListaErrores([]);
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
                if (a.ListaErrores.length == 0) {
                    if (func) {
                        func();
                    }
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
        url: baseUrl + "Tickets/Get_Form",
        success: function (data) {
            viewModel = new ticketViewModel(data);
            ko.applyBindings(viewModel);
        }
    });

    $("#aceptar").focus();
});