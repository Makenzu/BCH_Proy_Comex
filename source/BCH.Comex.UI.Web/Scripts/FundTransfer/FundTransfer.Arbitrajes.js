var my = {};
var ch_d = 0;
var ls_ch_d = 0;
var mnC = -1;
var mnV = -1;
var nueroDeal = "";
var tipoCambio = "";
var paridad = 0;
var pariOri = 0;
var vPrTl = 0.05;
var swTl = 0;
var mtoC = 0;
var mtoCOri = 0;
var mtoV = 0;
var mtoVOri = 0;
var posCual = 0;
$(document).ready(function () {
    var baseUrl = $("#base_url").val();

    $(document).ajaxStart(function () {
        $.blockUI({ message: "<h6>Espere un momento ...</h6>" });
    }).ajaxStop($.unblockUI);

    var viewModel = null;

    var Arbitrajes = $("#Arbitrajes");

    var updateModel = function (newViewModel) {
        ko.mapping.fromJS(newViewModel, viewModel);
        //obtengo los errores y los muestro
        var errors = ko.mapping.toJS(viewModel.Errors);
        loadMessages(errors);
        focusOnErrorControl(errors);
    }

    var ignoreList = [];

    var arbitrajesViewModel = function (data) {
        ko.mapping.fromJS(data, {}, this);
        var that = this;
        that.myFunc = function (chk) {
            var data = {
                jsonModel: ko.mapping.toJS(viewModel, {
                    ignore: ignoreList
                })
            };
            $.ajax({
                method: "POST",
                url: baseUrl + "FundTransfer/ARBITRAJE_ch_deal_change/",
                data: {
                    jsonModel: data,
                    chk: chk
                },
                success: function (a) {
                    updateModel(a, function () {
                        if (a.Errors.length == 0) {
                            $(":input").inputmask();
                        }
                    });
                    closeModal();
                },
                error: function (err) {
                    //console.error(err.responseText);
                }
            });
        };
        that.tx_modarb_blur = function (cual) {
            var str;
            if (cual == 3) {
                str = viewModel.Tx_Mtoarb()[cual].Text;
            }
            else {
                str = viewModel.Tx_Mtoarb()[cual].Text;
            }

            if (cual == 1) {
                paridad = viewModel.Tx_Mtoarb()[cual].Text();

            }
            return function () {
                AjaxCall("FundTransfer/ARBITRAJES_Tx_Mtoarb_Blur/" + cual, function () {
                    $(document.activeElement).select();
                });
                if (cual == 2) {
                    mtoC = $("#Tx_Monto_Compra").val();
                }
                if (cual == 3) {
                    mtoV = $("#Tx_Monto_Venta").val();
                }
            };
        };
        that.cb_moneda_compra_blur = function () {
            AjaxCall("FundTransfer/ARBITRAJES_Cb_Moneda_Compra_Blur", function () {
                msgNoUSD();
            });
            
            
        };
        that.cb_moneda_venta_blur = function () {
            AjaxCall("FundTransfer/ARBITRAJES_Cb_Moneda_Venta_Blur", function () {
                $(document.activeElement).select();
                msgNoUSD();
            });
        };
        that.ok_click = function () {
            AjaxCall("FundTransfer/ARBITRAJES_ok_Click?acepta=false", function () {
                if (viewModel.IrAIngresoValores()) {
                    window.location.href = baseUrl + "FundTransfer/ARBITRAJES_LlamarIngresoValores";
                } else {
                    if (viewModel.Errors().length == 0) {
                        if (viewModel.Confirms().length > 0) {
                            showConfirmMessages(ko.mapping.toJS(viewModel.Confirms), false, function (confirms) {
                                var allTrue = true;
                                for (var i = 0; i < confirms.length && allTrue; i++) {
                                    allTrue = allTrue && confirms[i];
                                }

                                viewModel.Confirms([]);
                                AjaxCall("FundTransfer/ARBITRAJES_ok_Click?acepta=" + allTrue, function () {
                                    if ($("#modal").css('display') == 'block' ? true : false) {
                                        $("button.btn.btn-primary.pull-right").click();
                                    }

                                    if (viewModel.Errors().length == 0) {
                                        if (viewModel.IrAIngresoValores()) {
                                            window.location.href = baseUrl + "FundTransfer/ARBITRAJES_LlamarIngresoValores";
                                        } else {
                                            AjaxCall("FundTransfer/ARBITRAJES_ok_2_Response", function () { $("#btnAccept").focus(); });
                                        }
                                    }
                                });
                            });
                        } else {
                            paridad = viewModel.Tx_Mtoarb()[1].Text();
                            AjaxCall("FundTransfer/ARBITRAJES_ok_2_Response", function () { $("#btnAccept").focus(); });
                            ch_d = 0;
                            ls_ch_d = 0;
                            mnC = -1;
                            mnV = -1;
                            nueroDeal = "";
                            tipoCambio = "";
                            paridad = 0;
                            pariOri = 0;
                            vPrTl = 0.05;
                            swTl = 0;
                            mtoC = 0;
                            mtoCOri = 0;
                            mtoV = 0;
                            mtoVOri = 0;
                            posCual = 0;
                        }
                    }
                }
            });
        };
        that.no_click = function () {
            AjaxCall("FundTransfer/ARBITRAJES_no_Click", function () {
                if ($('#TipoC').val() == 0) {
                    $('#TipoC').focus();
                } else {
                    $('[tabindex="2"]').focus();
                }
            });
        };
        that.operacion_click = function (id) {
            return function () {
                if (viewModel.Lt_Operacion.ListIndex() == id) {
                    viewModel.Lt_Operacion.ListIndex(-1);
                } else {
                    viewModel.Lt_Operacion.ListIndex(id);
                }
                AjaxCall("FundTransfer/ARBITRAJES_Lt_Operacion_Click");
            }
        };
        that.cb_moneda_compra_change = function () {
            var index = $("#Cb_Moneda_Compra").get(0).selectedIndex - 1;
            viewModel.Cb_Moneda_Compra.ListIndex(index);
        };
        that.cb_moneda_venta_change = function () {
            var index = $("#Cb_Moneda_Venta").get(0).selectedIndex - 1;
            viewModel.Cb_Moneda_Venta.ListIndex(index);
        };
        that.accept = function () {
            AjaxCall("FundTransfer/ARBITRAJES_co_boton_Click?boton_op=0&mensaje_op=false&acepta_op=false", function () {
                if (viewModel.Confirms().length == 0) {
                    window.location.href = baseUrl + "FundTransfer/ARBITRAJE_Finalizar";
                } else {
                    showConfirmMessages(ko.mapping.toJS(viewModel.Confirms), false, function (confirms) {
                        var allTrue = true;
                        for (var i = 0; i < confirms.length && allTrue; i++) {
                            allTrue = allTrue && confirms[i];
                        }
                        viewModel.Confirms([]);
                        AjaxCall("FundTransfer/ARBITRAJES_co_boton_Click?boton_op=0&mensaje_op=true&acepta_op=" + allTrue, function () {
                            window.location.href = baseUrl + "FundTransfer/ARBITRAJE_Finalizar";
                        });
                    });
                }
            });
        };
        that.cancel = function () {
            AjaxCall("FundTransfer/ARBITRAJES_co_boton_Click?boton_op=1&mensaje_op=false&acepta_op=false", function () {
                window.location.href = baseUrl + "FundTransfer/ARBITRAJE_Finalizar";
            });
        };
        that.btn_chilefx_click = function () {
            showConsultaDealsDisponible();
        };
    };
    function msgNoUSD() {
        var mc = $("#Cb_Moneda_Compra").get(0).value;
        var mv = $("#Cb_Moneda_Venta").get(0).value
        if (mc != '' && mc != '11' && mv != '' && mv != '11' ) {
            showAlert('Aviso! <br />', 'No ha seleccionado ninguna moneda de tipo dólar.', 'alert-info', true);
        }
    }
    function ctrlRev(cual) {
        var sw = true;
        var mtoOpe = 0;
        var mIng;
        if (cual == 2 || cual == 3) {
            if (cual == 2) {
                mIng = numeral().unformat(viewModel.Tx_Mtoarb()[cual].Text());
                mtoOpe = mtoC;
                if (posCual == 0) {
                    posCual = 2;
                }
            }
            else if (cual == 3) {
                mIng = numeral().unformat($("#Tx_Monto_Venta").val());
                mtoOpe = mtoV;
                posCual = 3;
            }
            var vTole5 = 0;
            var vTole100 = 0;
            if (numeral().unformat(saldo) < 0) {
                vTole5 = ((numeral().unformat(mtoOpe) * vPrTl) + -(numeral().unformat(mtoOpe))) * -1;
                vTole100 = numeral().unformat(mtoOpe) + 100;
            }
            else {
                vTole5 = (numeral().unformat(saldo) * vPrTl) + numeral().unformat(mtoOpe);
                vTole100 = numeral().unformat(saldo) + 100;
            }
            if ((mIng > vTole5 || mIng > vTole100) && (mtoOpe != 0 && mtoOpe != "") && cual == 2 && ls_ch_d != 0) {
                swTl = 1;
                sw = false;
            }
            else if ((mIng > vTole5 || mIng > vTole100) && (mtoOpe != 0 && mtoOpe != "") && cual == 3 && ls_ch_d != 0) {
                swTl = 1;
                sw = false;
            }
            else {
                swTl = 0;
                $("#btn_ok").prop("disabled", false);
            }
        }
        return sw;
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
                if (a.Errors.length == 0) {
                    if (func) {
                        func();
                    }
                }
            },
            error: function (err) {
            }
        });
    }

    $.ajax({
        method: "GET",
        cache: false,
        url: baseUrl + "FundTransfer/ARBITRAJES_Get_Model",
        success: function (data) {
            var nodo = Arbitrajes.get(0);
            ko.cleanNode(nodo);
            viewModel = new arbitrajesViewModel(data);
            ko.applyBindings(viewModel, nodo);
            my = viewModel;
            var tipoCambio = viewModel.Tx_Mtoarb()[0].Text();
            if (parseInt(tipoCambio) > 0) {
                $("#Cb_Moneda_Compra").focus();
            } else {
                $("#TipoC").focus().select();
            }
        }
    });

    if ($('#TipoC').val() == 0) {
        $('#TipoC').focus().select();
    } else {
        $('[tabindex="2"]').focus();
    }

    $("#modal").on("shown.bs.modal", function () { $("button.btn.btn-primary.pull-right").focus(); });

    /// Cuando haga Enter en cualquier parte de la pantalla
    $(window).keydown(function (ev) {
        var keycode = (ev.keyCode ? ev.keyCode : ev.which);
        if (swTl == 1) {
            $('#btn_Close_Adv').focus();
        }
        else {
            if (keycode == 9) {  // Presiona Tab
                var tab = $(document.activeElement).attr("tabindex");
                if ((tab == -1 && stM == 1) || swTl == 1) {
                    RecorrerTabIndex(100, ev);
                }
                else {
                    RecorrerTabIndex(-1, ev);
                }
            }
            if (keycode == 13) {  // Presiona Enter
                if (swTl == 1) {
                    $('#btn_Close_Adv').focus();
                }
                else {
                    ev.preventDefault();
                    if (stM == 0) {
                        if ($("#btnAccept").is(":focus")) {
                            $("#btnAccept").click();
                        } else if ($("#modal").css('display') == 'block' ? true : false) {
                            $("button.btn.btn-primary.pull-right").click();
                        } else {
                            $('#btn_ok').click();
                        }
                    }
                }
            }
        }
        if (keycode == 27) {    // Presiona Esc
            $('#msg_us').modal('hide');
        }
    });
    $('#ConsultaDealsDisponible').on('hidden.bs.modal', function () {
        stM = 0;
    });
    $('#ConsultaDealsDisponible').on('show.bs.modal', function () {
        stM = 1;
    });
    $('#msg_us').on('show.bs.modal', function () {
        swTl = 1;
        $("#btn_ok").prop("disabled", true);
        $('#btn_Close_Adv').focus();
    });
    $('#msg_us').on('hidden.bs.modal', function () {
        $("#btn_ok").prop("disabled", false);
        swTl = 0;
        if (posCual == 2) {
            posCual = 0;
            $("#Tx_Monto_Compra").focus();
        }
        if (posCual == 3) {
            posCual = 0;
            $("#Tx_Monto_Venta").focus();
        }
    });
    $('#btn_Close_Adv').blur(function () {
        $('#btn_Close_Adv').focus();
    });
    $(window).on('load', function () {
        showConsultaDealsDisponible();
    });
    $("#btn_cl_CDD").click(function () {
        closeModal();
    });
    function showConsultaDealsDisponible() {
        const bc = [" bcf ", " bcs "]
        const tg_td = "<td class='tg-td ";
        $.ajax({
            url: baseUrl + "FundTransfer/ARBITRAJES_WM_Deals",
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null && data.data != null) {
                    $(".tg-th").height(35);
                    var row = "";
                    $('#tb_CDD').empty();
                    var pf = 1;
                    var f = 0;
                    var lg = data.lg;
                    var wth = "w90";
                    if (lg == 0) {
                        row = data.MsgSP;
                        $(".tg-ct-Arb").height(86 + (43.2));
                        $(".scrollit").height(43.4);
                    }
                    else if (lg >= 10) {
                        $(".tg-ct-Arb").height(518);
                        $(".scrollit").height(432);
                        wth = "w74";
                    }
                    else {
                        $(".tg-ct-Arb").height(86 + (lg * 43.2));
                        $(".scrollit").height(lg * 43.4);
                    }
                    $.each(data.data, function (index, value) {
                        f = index + 1;
                        pf = pf == 1 ? 0 : 1;
                        var valCompra = value.tipoTransaccion == "ArbCom" ? value.stMontoBancoRecibe : value.stCalcSldMnd2;
                        var valVenta = value.tipoTransaccion == "ArbCom" ? value.stCalcSldMnd2 : value.stMontoBancoRecibe;
                        row += "<tr>"
                            + tg_td + bc[pf] + "ac w62 td-arbt'>" + "<input type='checkbox' id='ch_dl_" + (f) + "' tabindex='" + (f + 100) + "' onclick='ch_deal_change(this)' />" + "</td>"
                            + tg_td + bc[pf] + "ac w60 td-arbt' id='td_tp" + (f) + "'>" + value.tipoTransaccion + "</td>"
                            + tg_td + bc[pf] + "ac w68 td-arbt' id='td_ic" + (f) + "'>" + value.numeroDeal + "</td>"
                            + tg_td + bc[pf] + "ar w97 td-arbt' id='td_mo" + (f) + "'>" + valCompra + "</td>"
                            + tg_td + bc[pf] + "ac w59 td-arbt' id='td_mnc" + (f) + "'>" + value.moneda1 + "</td>"
                            + tg_td + bc[pf] + "ar w97 td-arbt' id='td_sa" + (f) + "'>" + value.stSaldoMoneda1 + "</td>"

                            + tg_td + bc[pf] + "ac w58 td-arbt' id='td_ppm2" + (f) + "'>" + value.stPrecioPoolMoneda2 + "</td>"

                            + tg_td + bc[pf] + "ar w97 td-arbt' id='td_saOri2" + (f) + "'>" + valVenta + "</td>"
                            + tg_td + bc[pf] + "ac w59 td-arbt' id='td_mnv" + (f) + "'>" + value.moneda2 + "</td>"
                            + tg_td + bc[pf] + "ar w97 td-arbt' id='td_sa2" + (f) + "'>" + value.stSaldoMoneda2 + "</td>"

                            + tg_td + bc[pf] + "ac w77 td-arbt' id='td_fd" + (f) + "'>" + value.stFechaTransaccion + "</td>"
                            + tg_td + bc[pf] + "ac w77 td-arbt' id='td_fv" + (f) + "'>" + value.stFechaValuta1 + "</td>"
                            + tg_td + bc[pf] + "ac w70 td-arbt' id='td_or" + (f) + "'>" + value.txtcodigoOrigenCarga + "</td>"
                            + tg_td + bc[pf] + "ac w132 td-arbt' id='td_nst" + (f) + "'>" + value.stultimoNumeroTransitoria + "</td>"
                            + tg_td + bc[pf] + "ac w132 td-arbt' id='td_nsc" + (f) + "'>" + value.stultimoNumeroContingente + "</td>"
                            + tg_td + bc[pf] + "ac td-arbt" + wth + "' id='td_cc" + (f) + "'>" + value.codigoBancoCentral + "</td>"
                            + tg_td + bc[pf] + "ac w55 td-arbt' id='td_tc" + (f) + "' hidden>" + value.stPrecioCliente + "</td>"

                            + tg_td + bc[pf] + "ac w55 td-arbt' id='td_do" + (f) + "' hidden>" + value.DeltaOrig + "</td>"

                            + "</tr>";
                    });
                    $('#tb_CDD').append(row);
                    if (data.dps != null) {
                        $("#ch_dl_" + data.dps).prop('checked', false);
                    }
                    if (data.das != null) {
                        $("#ch_dl_" + data.das).prop('checked', true);
                    }
                }
                else {
                    $(".tg-th").height(0);
                    $('#tb_CDD').empty();
                    $(".tg-ct-Arb").height(0);
                    $(".scrollit").height(0);
                    closeModal();
                    showAlert('', 'Cuando el primer registro se crea con tipo de cambio pizarra, no puede ingresar más.', 'alert-danger', true);
                }
            },
            error: function (data, status) {
                closeModal();
                showAlert('', 'Es posible que haya perdido la conexión. Actulice la página.', 'alert-danger', true);
            }
        });
        openModal();
    }
});
function openModal() {
    $('#ConsultaDealsDisponible').modal('show');
}
function closeModal() {
    $('#ConsultaDealsDisponible').modal('hide');
};
function selDesel(obj, idx) {
    $("#tb_CDD input[type=checkbox]").each(function () {
        if (this != obj) {
            $(this).attr('checked', false);
        }
        else {
            $(this).attr('checked', true);
            if ($(this).prop('checked')) {
                ch_d = idx;
                ls_ch_d = idx;
            }
            else {
                ch_d = 0;
            }
            closeModal();
        }
    });
};
function ch_deal_change(chk) {
    my.myFunc(chk.id);
    var idx = chk.id.replace("ch_dl_", "");
    if ($('#ch_dl_' + idx).is(':checked')) {
        nueroDeal = $("#td_ic" + idx).text();
        saldo = $("#td_sa" + idx).text();
        tipoCambio = $("#td_tc" + idx).text();
        mtoC = $("#td_sa" + idx).text();
        mtoCOri = $("#td_sa" + idx).text();
        mtoV = $("#td_sa2" + idx).text();
        pariOri = $("#td_ppm2" + idx).text();
        mtoVOri = $("#td_do" + idx).text();;
        ch_d = idx;
        ls_ch_d = idx;
    }
    else {
        nueroDeal = "";
        saldo = 0;
        tipoCambio = 0;
        mtoC = 0;
        mtoCOri = 0;
        mtoV = 0;
        mtoVOri = 0;
        ch_d = 0;
        swTl = 0;
        pariOri = 0;
    }
};
