var baseUrl = $("#base_url").val();
var my = {};
var stM = 0;
var ch_d = 0;
var ls_ch_d = 0;
var pais = -1;
var moneda = -1;
var concepto = -1;
var saldo = "";
var tipoCambio = "";
var paridad = 0;
var vPrTl = 0.05;
var swTl = 0;
$(document).ready(function () {
    $.fx.off = true;
    var baseUrl = $("#base_url").val();
    var isBlur = false;
    $(document).bind("ajaxStart", function () {
        $.blockUI({ message: "<h6>Espere un momento ...</h6>" });
    }).bind("ajaxStop", function () {
        $.unblockUI();
    });
    $(document).ajaxStart(function () {
        var indiceMoneda = $('#Cb_Moneda option:selected').index();
        var monedaVal = $('#Cb_Moneda option:selected').val();
        var divisaVal = $('#Cb_Divisa option:selected').val();
        if (indiceMoneda != 0) {
            if ((divisaVal == 4) && (monedaVal == 1)) { 
                $('#monto').removeClass('form-control monto').addClass('form-control montos');
            }
            else {
                $('#monto').removeClass('form-control montos').addClass('form-control monto');
            }
        }
    });
    var viewModel = {};
    var dataModel = null;
    var viewDataKeys = [];
    var keyIndex = 0;
    var mapping = {};
    var ComercioInvisible = $("#ComercioInvisible");
    function MapearModelo(newViewModel, ignoreList, callback) {
        if (viewDataKeys.length == 0) {
            for (var key in newViewModel) {
                viewDataKeys.push(key);
            }
        }
        if (keyIndex < viewDataKeys.length) {
            var key = viewDataKeys[keyIndex];
            setTimeout(function () {
                var dataObj = newViewModel[key];
                if (viewModel[key]) {
                    if (dataObj instanceof Array) {
                        for (var i = 0; i < dataObj.length; i++) {
                            var dataObjEntry = dataObj[i];
                            if (viewModel[key]()[i]) {
                                for (var prop in dataObjEntry) {
                                    if (ignoreList.indexOf(key + "." + prop) == -1) {
                                        if (ch_d != 0 && key == "Tx_MtoCV" && i == 0 && prop == "Text") {
                                            viewModel[key]()[i][prop](saldo);
                                        }
                                        else if (ch_d != 0 && key == "Tx_MtoCV" && i == 1 && prop == "Text") {
                                            viewModel[key]()[i][prop](tipoCambio);
                                        }
                                        else if (ch_d != 0 && key == "Tx_MtoCV" && i == 3 && prop == "Text") {
                                            viewModel[key]()[i][prop](paridad);
                                            if (numeral().unformat($('#tipoCambio').val()) > 0) {
                                                var monto = numeral().unformat(saldo) * numeral().unformat($('#tipoCambio').val());
                                                viewModel.Tx_MtoCV()[2].Text(numeral(monto).format('0,0'));
                                            }
                                        }
                                        else {
                                            viewModel[key]()[i][prop](dataObjEntry[prop]);
                                        }
                                    }
                                }
                            } else {
                                var dataModelEntry = {};
                                for (var prop in dataObjEntry) {
                                    if (ignoreList.indexOf(key + "." + prop) == -1) {
                                        dataModelEntry[prop] = ko.observable(dataObjEntry[prop]);
                                    }
                                }
                                viewModel[key].push(dataModelEntry);
                            }
                        }
                    } else if (dataObj instanceof Object) {
                        for (var prop in dataObj) {
                            if (ignoreList.indexOf(key + "." + prop) == -1) {
                                if (prop === "Items") {
                                    viewModel[key][prop].removeAll();
                                    for (var j = 0; j < dataObj[prop].length; j++) {
                                        viewModel[key][prop].push(dataObj[prop][j]);
                                    }
                                } else {
                                    if (ch_d != 0 && key == "Cb_Pais" && prop == "Value") {
                                        viewModel[key][prop](pais);
                                        var xx = $("#Cb_Pais")[0].selectedIndex - 1;
                                    } else if (ch_d != 0 && key == "Cb_Pais" && prop == "ListIndex") {
                                        viewModel[key][prop]($("#Cb_Pais")[0].selectedIndex - 1);
                                        var xx = $("#Cb_Pais")[0].selectedIndex - 1;
                                    }
                                    else if (ch_d != 0 && key == "Cb_Moneda" && prop == "Value") {
                                        viewModel[key][prop](moneda);
                                        var xx = $("#Cb_Moneda")[0].selectedIndex - 1;

                                    } else if (ch_d != 0 && key == "Cb_Moneda" && prop == "ListIndex") {
                                        viewModel[key][prop]($("#Cb_Moneda")[0].selectedIndex - 1);
                                        var xx = $("#Cb_Moneda")[0].selectedIndex - 1;

                                    } else if (ch_d != 0 && key == "Lt_Tcp" && prop == "Value") {
                                        viewModel[key][prop](concepto);
                                        var xx = $("#Lt_Tcp")[0].selectedIndex - 1;
                                    } else if (ch_d != 0 && key == "Lt_Tcp" && prop == "ListIndex") {
                                        viewModel[key][prop]($("#Lt_Tcp")[0].selectedIndex - 1);
                                        var xx = $("#Lt_Tcp")[0].selectedIndex - 1;
                                    }
                                    else {
                                        viewModel[key][prop](dataObj[prop]);
                                    }
                                }
                            }
                        }
                    } else {
                        viewModel[key](dataObj);
                    }
                } else {
                    if (dataObj instanceof Array) {
                        viewModel[key] = ko.observableArray();
                        for (var i = 0; i < dataObj.length; i++) {
                            var dataObjEntry = dataObj[i];
                            var dataModelEntry = {};
                            for (var prop in dataObjEntry) {
                                if (ignoreList.indexOf(key + "." + prop) == -1) {
                                    dataModelEntry[prop] = ko.observable(dataObjEntry[prop]);
                                }
                            }
                            viewModel[key].push(dataModelEntry);
                        }
                    } else if (dataObj instanceof Object) {
                        viewModel[key] = {};
                        for (var prop in dataObj) {
                            if (ignoreList.indexOf(key + "." + prop) == -1) {
                                if (prop == "Items") {
                                    viewModel[key][prop] = ko.observableArray(dataObj[prop]);
                                } else {
                                    viewModel[key][prop] = ko.observable(dataObj[prop]);
                                }
                            }
                        }
                    } else {
                        viewModel[key] = ko.observable(dataObj);
                    }
                }
                keyIndex++;
                MapearModelo(newViewModel, ignoreList, callback);
            }, 0);
        } else {
            keyIndex = 0;
            callback();
        }
        if (newViewModel.Lt_Operacion.Items.length == 0) {
            $("#btnAccept").prop("disabled", true);
        }
        else { $("#btnAccept").prop("disabled", false); }
    }
    var updateModel = function (newViewModel, callback) {
        dataModel = newViewModel;
        MapearModelo(dataModel, mapping.ignore, function () {
            var errors = dataModel.Errors;
            if (errors.length > 0) {
                $("#btnAccept").prop("disabled", true);
                $("#btn_ok").prop("disabled", false);
            }
            loadMessages(errors);
            focusOnErrorControl(errors);
            callback();
        });
    }
    var ignoreList = [
        "cb_divisa_blur",
        "cb_moneda_blur",
        "lt_tcp_blur",
        "tx_mtocv_blur",
        "ch_convenio_change",
        "myFunc",
        "ok_btn_click",
        "btn_chilefx_click",
        "ch_deal_change",
        "btn_cl_CDD_click",
        "no_btn_click",
        "co_btn_click",
        "lt_operacion_click",
        "tx_codadn_change",
        "tx_codadn_blur",
        "tx_docnac_change",
        "tx_docnac_blur",
        "tx_er_blur",
        "tx_er_change",
        "tx_nrodec_blur",
        "tx_nrodec_change",
        "cb_divisa_change",
        "cb_moneda_change",
        "cb_pais_change",
        "lt_tcp_change",
        "cb_mondes_change",
        "cb_sececben_change",
        "cb_sececin_change",
        "cb_insut_change",
        "cb_arcon_change",
        "cb_tipaut_change",
        "tx_datos_keypress",
        "Tx_NomFin_change",
        "co_btn_keypress",
        "enter_keypress",
        "Tx_NomFin_keypress"
    ];
    numeral.language('cl', {
        delimiters: {
            thousands: '.',
            decimal: ','
        },
        abbreviations: {
            thousand: 'k',
            million: 'm',
            billion: 'b',
            trillion: 't'
        },
        ordinal: function (number) {
            return number === 1 ? 'er' : 'ème';
        },
        currency: {
            symbol: '€'
        }
    });
    numeral.language('cl');
    var Tx_MtoCV_LostFocus = function (index) {
        var mto = 0;
        var divisaVal = $('#Cb_Divisa option:selected').val();
        var monedaVal = $('#Cb_Moneda option:selected').val();
        if (index == 0) {
            viewModel.Tx_MtoCV()[0].Text($('#monto').val());
            if (viewModel.Tx_MtoCV()[1].Enabled() == true) {
                AjaxCall("FundTransfer/COMINV_ConsPrec2", function () {
                    $("#tipoCambio")[0].setSelectionRange(0, $("#tipoCambio").val().length);
                });
            }
        }
        if (index == 0 || index == 1) {
            var vTole5 = 0;
            var vTole100 = 0;
            if (numeral().unformat(saldo) < 0) {
                vTole5 = ((numeral().unformat(saldo) * vPrTl) + -(numeral().unformat(saldo))) * -1;
                vTole100 = numeral().unformat(saldo) + 100;
            }
            else {
                vTole5 = (numeral().unformat(saldo) * vPrTl) + numeral().unformat(saldo);
                vTole100 = numeral().unformat(saldo) + 100;
            }
            
            var mIng = numeral().unformat($('#monto').val());
            if ((mIng > vTole5 || mIng > vTole100) && (saldo != 0 && saldo != "")) {
                swTl = 1;
                $('#msg_us').modal({ backdrop: 'static', keyboard: false });
                $('#msg_us').modal('show');
                $("#btn_ok").prop("disabled", true);
                $('#btn_Close_Adv').focus();
            }
            else {
                swTl = 0;
                $("#btn_ok").prop("disabled", false);
            }

            if ($('#monto').val().length != 0 && $('#tipoCambio').val().length != 0) {
                if (index != 0) {
                    if ((divisaVal == 4) && (monedaVal == 1)) {
                        mto = numeral().unformat($('#monto').val());
                    }
                    else {
                        mto = numeral().unformat($('#monto').val()) * numeral().unformat($('#tipoCambio').val());
                    }
                }
                else {
                    mto = numeral().unformat($('#monto').val()) * numeral().unformat($('#tipoCambio').val());
                }
                viewModel.Tx_MtoCV()[2].Text(numeral(mto).format('0,0'));
            }
            if ($('#monto').val().length == 0) {
                viewModel.Tx_MtoCV()[0].Text(numeral("0").format('0,0.0000'));
            }
        }
        if (divisaVal != 4) {
            if (numeral().unformat($('#paridad').val()) > 0) {
                $('#paridad').prop('disabled', true);
            } else {
                $('#paridad').prop('disabled', false);
            }
        }
    }
    var Tx_MtoCV_KeyPress = function (index) {
        var indiceMoneda = $('#Cb_Moneda option:selected').index();
        var monedaVal = $('#Cb_Moneda option:selected').val();
        var divisaVal = $('#Cb_Divisa option:selected').val();
        var esCorrecto = false;
        var texto = '';
        if (index == 0) {
            texto = $('#monto').val();
        } else if (index == 1) {
            texto = $('#tipoCambio').val();
        } else if (index == 3) {
            texto = $('#paridad').val();
        }
        if (indiceMoneda != 0) {
            if ((divisaVal == 4) && (monedaVal == 1)) {
                if (texto.length > 0) {
                    var valorMonto = numeral().unformat($('#monto').val());
                    viewModel.Tx_MtoCV()[0].Text(numeral(valorMonto).format('0,0'));
                    return;
                }
            }
        }
        if (index != 3) {
            if (index == 0) {
                if (texto.length == 0) {
                    showAlert('', 'Debe Ingresar el Monto.', 'alert-danger', true);
                } else {
                    if (numeral().unformat($('#tipoCambio').val()) > 0) {
                        var monto = numeral().unformat(texto) * numeral().unformat($('#tipoCambio').val());
                        viewModel.Tx_MtoCV()[2].Text(numeral(monto).format('0,0'));
                    }
                    var valorMonto = numeral().unformat($('#monto').val());
                    viewModel.Tx_MtoCV()[0].Text(numeral(valorMonto).format('0,0.00'));
                }
            }
            if (index == 1) {
                if (texto.length == 0) {
                    showAlert('', 'Debe Ingresar el tipo de cambio.', 'alert-danger', true);
                } else {
                    var monto = numeral().unformat($('#monto').val()) * numeral().unformat($('#tipoCambio').val());
                    viewModel.Tx_MtoCV()[2].Text(numeral(monto).format('0,0'));
                    viewModel.Tx_MtoCV()[1].Text(numeral($('#tipoCambio').val()).format('0,0.0000'));
                }
            }
        } else {
            var valorMonto = numeral().unformat($('#paridad').val());
            viewModel.Tx_MtoCV()[3].Text(numeral(valorMonto).format('0,0.0000000000'));
        }
    }
    function generarViewModel(newViewModel) {
        mapping = {
            "ignore": ["Co_Boton", "Label1", "Titulo", "Lb_Titulo_Operacion", "Label2", "Label3", "Label4", "Label8", "NO", "OK", "Lb_PrcPar", "Lb_SecEcBen"]
        };
        var data = newViewModel;
        for (var key in data) {
            try {
                mapping.ignore.push(key + ".ID");
                mapping.ignore.push(key + ".ListCount");
                mapping.ignore.push(key + ".SelectedValue");
                mapping.ignore.push(key + ".Tag");
                if (data[key]) {
                    if (data[key].Items) {
                        mapping.ignore.push(key + ".Text");
                    } else if (data[key].Controles) {
                        mapping.ignore.push(key + ".Caption");
                        mapping.ignore.push(key + ".Controles");
                    } else if (data[key].hasOwnProperty("EsTextArea")) {
                        mapping.ignore.push(key + ".EsTextArea");
                        mapping.ignore.push(key + ".MaxLength");
                        mapping.ignore.push(key + ".Rows");
                    } else if (data[key].hasOwnProperty("Checked")) {
                        mapping.ignore.push(key + ".Value");
                    } else if (data[key].length != null && data[key].length != undefined && data[key].length >= 0) {
                        mapping.ignore.push(key + ".EsTextArea");
                        mapping.ignore.push(key + ".MaxLength");
                        mapping.ignore.push(key + ".Rows");
                    }
                }
            } catch (ex) {
                console.error(key);
            }
        }
        viewModel = new Object();
        MapearModelo(data, mapping.ignore, comercioInvisibleViewModel);
    };
    function comercioInvisibleViewModel() {
        var that = viewModel;
        that.cb_divisa_blur = function () {
            if (isBlur) {
                AjaxCall("FundTransfer/COMINV_CB_Divisa_Blur");
            }
            if (ch_d != 0) {
                my.Tx_MtoCV()[0].Text(numeral(saldo).format('0,0.00'));
            }
        };
        that.cb_moneda_blur = function () {
            if (isBlur)
                AjaxCall("FundTransfer/COMINV_CB_Moneda_Blur", function () {
                    $("#monto")[0].setSelectionRange(0, $("#monto").val().length);
                });
            $("#monto")[0].setSelectionRange(0, $("#monto").val().length);
        };
        that.cb_moneda_change = function () {
            var index = $("#Cb_Moneda").get(0).selectedIndex - 1;
            if (index != -1) {
                moneda = $("#Cb_Moneda").get(0).value;
            }
            viewModel.Cb_Moneda.ListIndex(index);
            isBlur = true;
        };
        that.lt_tcp_blur = function () {
            if (isBlur)
                AjaxCall("FundTransfer/COMINV_Lt_Tcp_Blur", function () {
                    $("#Cb_TipAut").val(6);
                });
        };
        that.tx_mtocv_blur = function (index) {
            return function () {
                Tx_MtoCV_KeyPress(index);
                Tx_MtoCV_LostFocus(index);
                isBlur = false;
            };
        };
        that.ch_convenio_change = function () {
            if (viewModel.Ch_Convenio.Checked()) {
                viewModel.Tx_FecDeb.Text("");
                viewModel.Tx_DocExt.Text("");
                viewModel.Fr_Convenio.Enabled(false);
            } else {
                var today = new Date();
                viewModel.Fr_Convenio.Enabled(true);
                viewModel.Tx_FecDeb.Text(today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + today.getDate())
                viewModel.Tx_DocExt.Text(viewModel.OPESIN())
            }
        };
        that.myFunc = function (chk) {
            var data = $.extend({}, dataModel, ko.mapping.toJS(viewModel, {
                ignore: ignoreList
            }));
            $.ajax({
                method: "POST",
                url: baseUrl + "FundTransfer/COMINV_ch_deal_change/",
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
                    if (a.Tx_MtoCV[1].Enabled == false) {
                        $('#monto').focus();
                    }
                    else {
                        $('#Cb_Divisa').focus();
                    }
                    
                },
                error: function (err) {
                    console.error(err.responseText);
                }
            });
        };
        that.ok_btn_click = function () {
            if (swTl == 0) {
                var vSaldo = numeral().unformat(saldo);
                var vtipoCambio = numeral().unformat(tipoCambio);
                var mIng = numeral().unformat($('#monto').val());
                var mIngTC = numeral().unformat($('#tipoCambio').val());
                if (((vSaldo != 0 && (vSaldo != mIng && mIng != 0)) || (vSaldo == 0 && mIng != 0)) ||
                    ((vtipoCambio != 0 && (vtipoCambio != mIngTC && mIngTC != 0)) || (vtipoCambio == 0 && mIngTC != 0))
                    )
                {
                    $("#btnAccept").prop("disabled", true);
                    $("#btn_ok").prop("disabled", true);
                    $("#Cb_Divisa").get(0).value = 0;
                    pais = -1;
                    moneda = -1;
                    concepto = -1;
                    saldo = "";
                    tipoCambio = "";
                    paridad = "";
                    ls_ch_d = 0;
                    AjaxCall("FundTransfer/COMINV_Ok_Click", function () {
                        window.scrollTo(0, 0);
                        isBlur = false;
                        $("#btnAccept").prop("disabled", false);
                        $("#btnAccept").focus();
                        $("#btn_ok").prop("disabled", false);
                    });
                    $("#btnAccept").focus();
                }
                else {
                    $("#btnAccept").prop("disabled", true);
                    $("#btn_ok").prop("disabled", true);
                    $("#Cb_Divisa").get(0).value = 0;
                    pais = -1;
                    moneda = -1;
                    concepto = -1;
                    saldo = "";
                    tipoCambio = "";
                    paridad = "";
                    ls_ch_d = 0;
                    AjaxCall("FundTransfer/COMINV_Ok_Click", function () {
                        window.scrollTo(0, 0);
                        isBlur = false;
                        $("#btnAccept").prop("disabled", false);
                        $("#btnAccept").focus();
                        $("#btn_ok").prop("disabled", false);
                    });
                    $("#btnAccept").focus();
                }
            }
            else {
                $('#btn_Close_Adv').focus();
            }
        };
        that.btn_chilefx_click = function () {
            showConsultaDealsDisponible();
        };
        that.btn_cl_CDD_click = function () {
            $("#btn_ok").prop("disabled", false);
            closeModal();
        }
        that.no_btn_click = function () {
            $("[tabindex='1']").focus();
            AjaxCall("FundTransfer/COMINV_No_Click", function () {
                isBlur = false;
            });
        };
        that.co_btn_click = function (index) {
            return function () {
                AjaxCall("FundTransfer/COMINV_Co_Btn_Click/" + index, function () {
                    window.location = (baseUrl + "FundTransfer/COMINV_Seguir");
                });
            }
        };
        that.co_btn_keypress = function (data, event) {
            event.preventDefault();
            var key = (event.keyCode ? event.keyCode : event.which);
            if (key == 13) {
                $("#btnAccept").click();
            }
            return true;
        };
        that.enter_keypress = function (data, event) {
            event.preventDefault();
            var key = (event.keyCode ? event.keyCode : event.which);
            if (key == 13) {
            }
            return true;
        };
        that.lt_operacion_click = function (id) {
            return function () {
                var index = id();
                if (viewModel.Lt_Operacion.ListIndex() == index) {
                } else {
                    viewModel.Lt_Operacion.ListIndex(index);
                }
                $("[tabindex='1']").focus();
                AjaxCall("FundTransfer/COMINV_Lt_Operacion_Click", function () {

                    saldo = viewModel.Tx_MtoCV()[0].Text();
                    tipoCambio = viewModel.Tx_MtoCV()[1].Text();
                    paridad = viewModel.Tx_MtoCV()[3].Text();
                    Tx_MtoCV_KeyPress(0);
                    Tx_MtoCV_LostFocus(0);
                    isBlur = false;
                });
            }
        };
        that.tx_codadn_blur = function (id) {
            if (isBlur)
                AjaxCall("FundTransfer/COMINV_Tx_CodAdn_Blur");
        };
        that.tx_codadn_change = function () {
            isBlur = true;
        };
        that.tx_docnac_blur = function (id) {
            if (isBlur)
                AjaxCall("FundTransfer/COMINV_Tx_DocNac_Blur");
        };
        that.tx_docnac_change = function () {
            isBlur = true;
        };
        that.tx_er_blur = function (id) {
            if (isBlur)
                AjaxCall("FundTransfer/COMINV_Tx_ER_Blur");
        };
        that.tx_er_change = function () {
            isBlur = true;
        };
        that.tx_nrodec_blur = function (id) {
            if (isBlur)
                AjaxCall("FundTransfer/COMINV_Tx_NroDec_Blur");
        };
        that.tx_nrodec_change = function () {
            isBlur = true;
        };
        that.cb_divisa_change = function () {
            var index = $("#Cb_Divisa").get(0).selectedIndex;
            viewModel.Cb_Divisa.ListIndex(index);
            isBlur = true;
        };
        that.cb_pais_change = function () {
            var index = $("#Cb_Pais").get(0).selectedIndex - 1;
            if (index != -1) {
                pais = $("#Cb_Pais").get(0).value;
            }
            viewModel.Cb_Pais.ListIndex(index);
        };
        that.lt_tcp_change = function () {
            var index = $("#Lt_Tcp").get(0).selectedIndex;
            if (index != -1 && concepto == -1) {
                concepto = $("#Lt_Tcp").get(0).value;
            }
            else if (index == -1 && concepto != -1) {
                index = concepto;
            }
            viewModel.Lt_Tcp.ListIndex(index);
            isBlur = true;
        };
        that.cb_mondes_change = function () {
            var index = $("#Cb_MonDes").get(0).selectedIndex - 1;
            viewModel.Cb_MonDes.ListIndex(index);
        };
        that.cb_sececin_change = function () {
            var index = $("#Cb_SecEcIn").get(0).selectedIndex - 1;
            viewModel.Cb_SecEcIn.ListIndex(index);
        };
        that.cb_sececben_change = function () {
            var index = $("#Cb_SecEcBen").get(0).selectedIndex - 1;
            viewModel.Cb_SecEcBen.ListIndex(index);
        };
        that.cb_insut_change = function () {
            var index = $("#Cb_InsUt").get(0).selectedIndex;
            viewModel.Cb_InsUt.ListIndex(index);
        };
        that.cb_arcon_change = function () {
            var index = $("#Cb_ArCon").get(0).selectedIndex;
            viewModel.Cb_ArCon.ListIndex(index);
        };
        that.cb_tipaut_change = function () {
            var index = $("#Cb_TipAut").get(0).selectedIndex;
            viewModel.Cb_TipAut.ListIndex(index);
        };
        that.tx_datos_keypress = function (viewModel, event) {
            var key = String.fromCharCode(event.charCode || event.keyCode);
            var indiceMoneda = $('#Cb_Moneda option:selected').index();
            var monedaVal = $('#Cb_Moneda option:selected').val();
            var divisaVal = $('#Cb_Divisa option:selected').val();
            var peso = false;
            if (indiceMoneda != 0) {
                if ((divisaVal == 4) && (monedaVal == 1)) { 
                    peso = true;
                }
            }
            if (peso) {
                var regex = /[0-9]|\.\,/;
                if (!regex.test(key)) {
                    event.preventDefault();
                    return false;
                }
            }
            if (key === "\r") {
                event.preventDefault();
                return false;
            } else {
                return true;
            }
        };
        that.Tx_NomFin_change = function () {
            toUppercase($("#Tx_NomFin"));
        };
        that.Tx_NomFin_keypress = function (viewModel, event) {
            var key = String.fromCharCode(event.charCode || event.keyCode);
            if (EsSoloCaracter(key)) {
                event.preventDefault();
                return false;
            } else {
                return true;
            }
        };
        var nodo = ComercioInvisible.get(0);
        ko.cleanNode(nodo);
        setTimeout(function () {
            ko.applyBindings(viewModel, nodo);
            my = viewModel;
        }, 1);
    };
    $('#ConsultaDealsDisponible').on('hidden.bs.modal', function () {
        stM = 0;
        $("#btn_ok").prop("disabled", false);
    });
    $('#ConsultaDealsDisponible').on('show.bs.modal', function () {
        stM = 1;
        $("#btn_ok").prop("disabled", true);
    });
    $('#msg_us').on('show.bs.modal', function () {
        swTl = 1;
        $("#btn_ok").prop("disabled", true);
        $('#btn_Close_Adv').focus();
    });
    $('#msg_us').on('hidden.bs.modal', function () {
        $("#btn_ok").prop("disabled", false);
        swTl = 0;
        viewModel.Tx_MtoCV()[0].Text(numeral(saldo).format('0,0.00'));
    });
    $('#btn_Close_Adv').blur(function () {
        $('#btn_Close_Adv').focus();
    });
    function AjaxCall(url, func) {
        if ($("#Cb_SecEcBen").length > 0) {
            var index = $("#Cb_SecEcBen").get(0).selectedIndex - 1;
            viewModel.Cb_SecEcBen.ListIndex(index);
        }
        if ($("#Cb_TipAut").length > 0) {
            var index = $("#Cb_TipAut").get(0).selectedIndex;
            viewModel.Cb_TipAut.ListIndex(index);
        }
        viewModel.Errors([]);
        var data = $.extend({}, dataModel, ko.mapping.toJS(viewModel, {
            ignore: ignoreList
        }));
        $.ajax({
            method: "POST",
            url: baseUrl + url,
            data: {
                jsonModel: data
            },
            success: function (a) {
                updateModel(a, function () {
                    if (a.Errors.length == 0) {
                        $(":input").inputmask();
                        if (func) {
                            func();
                        }
                    }
                });
            },
            error: function (err) {
                //console.error(err.responseText);
            }
        });
    }
    $(":input").inputmask();
    $.ajax({
        method: "GET",
        cache: false,
        url: baseUrl + "FundTransfer/COMINV_Form_Load",
        success: function (data) {
            dataModel = data;
            generarViewModel(dataModel);
            if (dataModel.CargaAutomatica == 1) {
                $("#Lt_Tcp").focus();
            } else {
                $("#Cb_Divisa").focus();
            }
        }
    });
    $(window).keydown(function (ev) {
        var keycode = (ev.keyCode ? ev.keyCode : ev.which);
        if (keycode == 9) {
            var tab = $(document.activeElement).attr("tabindex");
            if ((tab == -1 && stM == 1) || swTl == 1) {
                RecorrerTabIndex(100, ev);
            }
            else {
                RecorrerTabIndex(-1, ev);
            }
        }
        if (keycode == 13) {
            $('#msg_us').modal('hide');
            ev.preventDefault();
            if ($("#btnAceptar").is(":enabled")) {
                $("#btnAceptar").focus().click();
                return;
            } else if ($("#btnAceptar").is(":focus")) {
                $("#btnAceptar").focus().click();
                return;
            } else {
                if ($("#btn_ok").is(":enabled")) {
                    $('#btn_ok').focus().click();
                }
            }
        }
        if (keycode == 27) {
            $('#msg_us').modal('hide');
        }
    });
    $(window).on('load', function () {
        showConsultaDealsDisponible();
    });
    $("#btn_cl_CDD").click(function () {
        $("#btn_ok").prop("disabled", false);
        closeModal();
    });
    function showConsultaDealsDisponible() {
        const bc = [" bcf ", " bcs "]
        const tg_td = "<td class='tg-td ";
        $.ajax({
            url: baseUrl + "FundTransfer/COMINV_WM_Deals",
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
                    if (lg == 0) {
                        $('#tb_CDD').empty();
                        $("#tb_CDD").append(data.MsgSP);
                        $(".tg-ct").height(86 + (43.2));
                        $(".scrollit").height(43.4);
                    }
                    else if (lg >= 10) {
                        $(".tg-ct").height(518);
                        $(".scrollit").height(432);
                    }
                    else {
                        $(".tg-ct").height(86 + (lg * 43.2));
                        $(".scrollit").height(lg * 43.4);
                    }
                    $.each(data.data, function (index, value) {
                        f = index + 1;
                        pf = pf == 1 ? 0 : 1;
                        row += "<tr>"
                            + tg_td + bc[pf] + "ac w64'>" + "<input type='checkbox' id='ch_dl_" + (f) + "' tabindex='" + (f + 100) + "' onclick='ch_deal_change(this)' />" + "</td>"
                            + tg_td + bc[pf] + "ac w60 td-arbt' id='td_TT" + (f) + "'>" + value.tipoTransaccion + "</td>"
                            + tg_td + bc[pf] + "ac w70 td-arbt' id='td_ic" + (f) + "'>" + value.numeroDeal + "</td>"
                            + tg_td + bc[pf] + "ac w60 td-arbt' id='td_tc" + (f) + "'>" + value.stPrecioCliente + "</td>"
                            + tg_td + bc[pf] + "ac w55 td-arbt' id='td_pa" + (f) + "' hidden>" + value.monedaMuestra + "</td>"
                            + tg_td + bc[pf] + "ac w55 td-arbt' id='td_mn" + (f) + "' hidden>" + value.monedaMuestra + "</td>"

                            + tg_td + bc[pf] + "ac w55 td-arbt' id='td_mn" + (f) + "'>" + value.moneda1 + "</td>"
                            + tg_td + bc[pf] + "ac w55 td-arbt' id='td_mn" + (f) + "'>" + value.moneda2 + "</td>"

                            + tg_td + bc[pf] + "ac w55 td-arbt' id='td_ppm1" + (f) + "' hidden>" + value.precioPoolMoneda1 + "</td>"
                            + tg_td + bc[pf] + "ac w55 td-arbt' id='td_ppm2" + (f) + "' hidden>" + value.precioPoolMoneda2 + "</td>"
                            + tg_td + bc[pf] + "ar w92 td-arbt' id='td_mo" + (f) + "'>" + value.stMontoBancoRecibe + "</td>"
                            + tg_td + bc[pf] + "ar w92 td-arbt' id='td_sa" + (f) + "'>" + value.stDelta + "</td>"
                            + tg_td + bc[pf] + "ar w100 td-arbt' id='td_saP" + (f) + "'>" + value.stMontoClienteRecibe + "</td>"
                            + tg_td + bc[pf] + "ac w80 td-arbt' id='td_fd" + (f) + "'>" + value.stFechaTransaccion + "</td>"
                            + tg_td + bc[pf] + "ac w80 td-arbt' id='td_fv" + (f) + "'>" + value.stFechaValuta1 + "</td>"
                            + tg_td + bc[pf] + "ac w70 td-arbt' id='td_or" + (f) + "'>" + value.txtcodigoOrigenCarga + "</td>"
                            + tg_td + bc[pf] + "ac w60 td-arbt' id='td_stTT" + (f) + "' hidden>" + value.stTipoTransaccion + "</td>"
                            + tg_td + bc[pf] + "ac w80 td-arbt' id='td_co" + (f) + "' hidden>" + value.stCodigoEstadoContable + "</td>"
                            + tg_td + bc[pf] + "ac w134 td-arbt' id='td_nst" + (f) + "'>" + value.stultimoNumeroTransitoria + "</td>"
                            + tg_td + bc[pf] + "ac w134 td-arbt' id='td_nsc" + (f) + "'>" + value.stultimoNumeroContingente + "</td>"
                            + tg_td + bc[pf] + "ac w88 td-arbt' id='td_cBC" + (f) + "'>" + value.codigoBancoCentral + "</td>"
                            + tg_td + bc[pf] + "ac w88 td-arbt' id='td_stCBC" + (f) + "' hidden>" + value.stCodigoBancoCentral + "</td>"
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
                    $(".tg-ct").height(0);
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
function ch_deal_change(chk) {
    my.myFunc(chk.id);
    var idx = chk.id.replace("ch_dl_", "");
    saldo = $("#td_sa" + idx).text();
    tipoCambio = $("#td_tc" + idx).text();
    $("#monto")[0].setSelectionRange(0, $("#monto").val().length);
};
function openModal() {
    $('#ConsultaDealsDisponible').modal('show');
}
function closeModal() {
    $('#ConsultaDealsDisponible').modal('hide');
}
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
        }
    });
};