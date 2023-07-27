$(document).ready(function () {
    var baseUrl = $("#base_url").val();

    $(document).ajaxStart(function () {
        $.blockUI({ message: "<h6>Espere un momento ...</h6>" });
    }).ajaxStop($.unblockUI);

    var ignoreList = [];

    var loaded = false;
    var viewModel = null;
    var EmitirCheque = $("#EmitirCheque");

    var updateModel = function (newViewModel) {
        ko.mapping.fromJS(newViewModel, viewModel);
        var errors = ko.mapping.toJS(viewModel.Errors);
        loadMessages(errors);
        focusOnErrorControl(errors);
    }

    var emitirChequeViewModel = function (data) {
        ko.mapping.fromJS(data, {}, this);
        var that = this;
        that.newClass = function (index) {
            if (index() == viewModel.l_montos.ListIndex()) {
                return true;
            } else {
                return false;
            }            
        };
        that.l_cor_blur = function () {
            viewModel.l_cor.ListIndex = $("#l_cor").find(":selected").index();
        };
        that.l_mto_dblclick = function (index) {
            //if (loaded) {
            //    if (index == viewModel.l_montos.ListIndex()) {
            //        viewModel.l_montos.ListIndex(-1);
            //    } else {
            //        viewModel.l_montos.ListIndex(index);
            //    }
            //    AjaxCall("FundTransfer/EmitirCheque_l_montos_DblClick");
            //}
        };
        that.l_mto_click = function (index) {
            return function () {
                if (index() == viewModel.l_montos.ListIndex()) {
                    viewModel.l_montos.ListIndex(-1);
                } else {
                    viewModel.l_montos.ListIndex(index());
                }
                $.ajax({
                    url: baseUrl + "FundTransfer/EmitirCheque_L_Montos_Click",
                    data: {
                        jsonModel: ko.mapping.toJS(viewModel, {
                            ignore: ignoreList
                        })
                    },
                    method: "POST",
                    success: function (data) {
                        updateModel(data);
                        loaded = false;
                        viewModel.newClass(index);
                    }
                });
                if (viewModel.l_cor.ListCount() > 0) {
                    var lCor = $("#l_cor");
                    lCor.val(lCor.find("option")[0].value);
                }
                //AjaxCall("FundTransfer/EmitirCheque_L_Montos_Click");
            }
        };
        that.Co_Aceptar_Click = function () {
            AjaxCall("FundTransfer/EmitirCheque_Co_Aceptar_Click", function () {
                $.blockUI({ message: "<h6>Espere un momento ...</h6>" });
                window.location.href = baseUrl + "FundTransfer/";
            });
        };
        that.Co_Cancelar_Click = function () {
            AjaxCall("FundTransfer/EmitirCheque_Co_Cancelar_Click?vieneDeMsg=false&respMsg=false", function () {
            });
        };
        that.Co_Generar_Click = function () {
            viewModel.l_cor.ListIndex = $("#l_cor").find(":selected").index();
            AjaxCall("FundTransfer/EMITIRCHEQUE_Co_Generar_Click", function () {
                if (viewModel.l_cor.ListCount() > 0) {
                    var lCor = $("#l_cor");
                    lCor.val(lCor.find("option")[0].value);
                }
            });
        };
        that.l_benef_change = function () {
            viewModel.l_benef.ListIndex = $("#l_benef").find(":selected").index();
            AjaxCall("FundTransfer/EmitirCheque_l_benef_Click", function () {
                if (viewModel.l_cor.ListCount() > 0) {
                    var lCor = $("#l_cor");
                    lCor.val(lCor.find("option")[0].value);
                }
            });
        };
        that.l_plaza_change = function () {
            viewModel.l_plaza.ListIndex = $("#l_plaza").find(":selected").index() - 1;
            AjaxCall("FundTransfer/EmitirCheque_l_plaza_Click", function () {
                if (viewModel.l_cor.ListCount() > 0) {
                    var lCor = $("#l_cor");
                    lCor.val(lCor.find("option")[0].value);
                }
            });
        };
        $("#NombreBenef").change(function () { toUppercase(this) }).change();
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
                if (a.Errors.length == 0) {
                    if (a.Confirms.length > 0) {
                        $.unblockUI();
                        showConfirmMessages(ko.mapping.toJS(a.Confirms), false, function (confirms) {
                            var allTrue = true;
                            for (var i = 0; i < confirms.length && allTrue; i++) {
                                allTrue = allTrue && confirms[i];
                            }
                            AjaxCall("FundTransfer/EmitirCheque_Co_Cancelar_Click?vieneDeMsg=true&respMsg=" + allTrue, function () {
                            });
                        });
                    } else {
                        if (a.respuesta)
                            window.location.href = baseUrl + "FundTransfer/";
                    }
                }
                loaded = false;
            },
            error: function (err) {
                console.error(err.responseText);
            }
        });
    }

    $.ajax({
        method: "GET",
        cache: false,
        url: baseUrl + "FundTransfer/EmitirCheque_Load",
        success: function (data) {
            var nodo = EmitirCheque.get(0);
            ko.cleanNode(nodo);
            viewModel = new emitirChequeViewModel(data);
            ko.applyBindings(viewModel, nodo);
            loaded = true;
            if (viewModel.l_cor.ListCount() > 0) {
                var lCor = $("#l_cor");
                lCor.val(lCor.find("option")[0].value);
            }
            if ($('[tabindex="1"]').is(":enabled"))
                $('[tabindex="1"]').focus();
            else
                $('[tabindex="2"]').focus();
        }
    });
    
    $(document).keydown(function (ev) {
        var keycode = (ev.keyCode ? ev.keyCode : ev.which);
        if (keycode == 9) {  // Presiona Tab
            RecorrerTabIndex(-1, ev);
        }
        if (keycode == 13) { // Enter
            if (!$("#Co_Generar").is(':enabled')) {
                $('#Co_Aceptar').click();
            } else {
                $("#Co_Generar").click();
            }
        }
    });
});
