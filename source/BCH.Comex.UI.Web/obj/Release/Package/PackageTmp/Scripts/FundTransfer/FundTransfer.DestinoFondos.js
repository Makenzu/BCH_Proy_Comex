$(document).ready(function () {
    var baseUrl = $("#base_url").val();

    $(document).ajaxStart(function () {
        $.blockUI({ message: "<h6>Espere un momento ...</h6>" });
    }).ajaxStop($.unblockUI);

    var viewModel = null;
    var DestinoFondos = $("#DestinoFondos");
    var boton;


    var updateModel = function (newViewModel) {
        ko.mapping.fromJS(newViewModel, {}, viewModel);
        //obtengo los errores y los muestro
        var errors = ko.mapping.toJS(viewModel.Errors);
        loadMessages(errors);
    }
    var ignoreList = ["btn_ok_click", "btn_no_click", "aceptar", "cancelar", "l_cta_blur",
	                    "l_mnd_blur", "l_party_blur", "l_cuentas_blur", "bnem_click",
	                    "bt_plntrn_click", "cb_destino_blur", "cmb_codtran_blur",
	                    "l_mto_click", "l_via_click", "tx_datos_blur", "txtnumref_blur",
	                    "l_cta_change", "l_mnd_change", "l_partys_change", "l_cuentas_change",
	                    "cb_destino_change", "cmb_codtran_change", "tx_datos_keypress", "tx_datos_keypress_ConPunto", "selectInput",
	                    "Bt_PlnTrn.Visible"];

    var destinoFondosViewModel = function (data) {
        ko.mapping.fromJS(data, {}, this);
        var that = this;
        ko.bindingHandlers.scrollTo = {
            update: function (element, valueAccessor, allBindings) {
                var _value = valueAccessor();
                var _valueUnwrapped = ko.unwrap(_value);
                if (_valueUnwrapped) {
                    element.scrollIntoView();
                }
            }
        };
        that.scrolledItem = ko.observable();
        that.scrollToItem = function () {
            var index = viewModel.l_via.length;
            that.scrolledItem(viewModel.l_via.Items()[index]);
        };
        that.btn_ok_click = function () {
            AjaxCall("FundTransfer/DESTFOND_Ok_Click?vieneDe=false&res=false", function () {

                // Mover Scroll al final de la tabla
                $(".header-fixed tbody").scrollTop($('.header-fixed tbody').scrollTop() + 100);
                // Ajuste de cabecera
                if ($('.header-fixed tbody tr').length > 3) {
                    $('.header-fixed th').css('width', '32.55%')
                }
                else {
                    $('.header-fixed th').css('width', '33.33%')
                }

                if (boton) {
                    $('#btnAccept').removeAttr('disabled');
                }
                else {
                    $('#btnAccept').attr('disabled', 'disabled');
                }
                //
                var valorMonto = numeral().unformat($("#MtoVia").val());
                if (valorMonto > 0) {
                    //$("#btn_ok").focus();
                    $("#L_Cta").focus();
                }
                else {
                    $('#btn_ok').attr('disabled', 'disabled');
                    $("#btnAccept").focus();
                }
            });
            //$('#L_Cta').val('');
            //$('#L_Cuentas').val('');
            //$('#L_Partys').val('');
            //$('#L_Cuentas').attr('disabled', 'disabled');
            //$('#L_Partys').attr('disabled', 'disabled');
        };
        that.btn_no_click = function () {
            AjaxCall("FundTransfer/DESTFOND_No_Click", function () {
                // Ajuste de cabecera
                if ($('.header-fixed tbody tr').length > 3) {
                    $('.header-fixed th').css('width', '32.55%')
                }
                else {
                    $('.header-fixed th').css('width', '33.33%')
                }
            });
            $('#btnAccept').attr('disabled', 'disabled');
            $('#btn_no').attr('disabled', 'disabled');
            $('#btn_ok').removeAttr('disabled');
            $("#L_Cta").focus().select();
        };
        that.aceptar = function () {
            AjaxCall("FundTransfer/DESTFOND_Boton_Click?Index=0", function () {
                if (viewModel.Errors().length == 0) {
                    if (viewModel.VuelveDeOtro()) {
                        window.location.href = baseUrl + "FundTransfer/Vueltos";
                    } else {
                        window.location.href = baseUrl + "FundTransfer/DESTFOND_Aceptar";
                    }
                }
            });
        };
        that.cancelar = function () {
            AjaxCall("FundTransfer/DESTFOND_Boton_Click?Index=1", function () {
                window.location.href = baseUrl + "FundTransfer/DESTFOND_Aceptar";
            });
        };
        that.l_cta_blur = function () {
            //AjaxCall("FundTransfer/DESTFOND_L_Cta_Blur");
            ////$('#L_Cuentas').removeAttr('disabled');
            //$('#L_Partys').removeAttr('disabled');
        };
        that.l_mnd_blur = function () {
            AjaxCall("FundTransfer/DESTFOND_L_Mnd_Blur");
        };
        that.l_party_blur = function () {
            AjaxCall("FundTransfer/DESTFOND_L_Party_Blur", function () {
                if (viewModel.L_Cuentas.ListCount() == 0)
                    viewModel.L_Cuentas.Enabled(false);
                else
                    viewModel.L_Cuentas.Enabled(true);
            });
        };
        that.l_cuentas_blur = function () {
            //AjaxCall("FundTransfer/DESTFOND_L_Cuentas_Blur");
            viewModel.L_Cuentas.ListIndex($("#L_Cuentas").get(0).selectedIndex);
            if (viewModel.L_Cuentas.ListIndex() != "-1")
                viewModel.Indice_CtaCte(viewModel.L_Cuentas.Items()[viewModel.L_Cuentas.ListIndex()].Data());
        };
        that.bnem_click = function () {
            AjaxCall("FundTransfer/DESTFOND_BNem_Click", function () {
                window.location.href = baseUrl + "FundTransfer/NemonicoCuenta";
            });
        };
        that.bt_plntrn_click = function () {
            AjaxCall("FundTransfer/DESTFOND_Bt_PlnTrn_Click", function () {
                if (viewModel.VuelveDeOtro()) {
                    window.location.href = baseUrl + "FundTransfer/PlanillasTransferencia";
                }
            });
        };
        that.cb_destino_blur = function () {
            AjaxCall("FundTransfer/DESTFOND_Cb_Destino_Blur");
        };
        that.cmb_codtran_blur = function () {
            //AjaxCall("FundTransfer/DESTFOND_cmb_codtran_Blur");
            viewModel.Boton()[0].Enabled(false);
        };
        that.l_mto_click = function (index) {
            return function () {
                viewModel.l_mto.ListIndex(index);
                AjaxCall("FundTransfer/DESTFOND_l_mto_Click?index=" + index);
            }
        };
        that.l_via_click = function (index) {
            return function () {
                if (viewModel.l_via.ListIndex() == index) {
                    viewModel.l_via.ListIndex(-1);
                } else {
                    viewModel.l_via.ListIndex(index);
                }
                //AjaxCall("FundTransfer/DESTFOND_l_via_Click", function(){
                AjaxCall("FundTransfer/DESTFOND_l_via_Click", function () {
                    $('#btn_no').removeAttr('disabled');
                    $('#L_Cuentas').removeAttr('disabled');
                    $('#L_Partys').removeAttr('disabled');
                    $('#btn_ok').removeAttr('disabled');
                });
                // });
            };
        };
        that.tx_datos_blur = function (index) {
            //return function () {
            //    AjaxCall("FundTransfer/DESTFOND_Tx_Datos_Blur?index=" + index, function () {
            //        $(":focus").select();
            //    });
            //};
        };
        that.txtnumref_blur = function () {
            AjaxCall("FundTransfer/DESTFOND_txtNumRef_Blur");
        };
        that.selectInput = function () {
            var l = this;
            setTimeout(function () { $(l).select(); }, 0);
        };
        that.l_cta_change = function () {
            if (viewModel.L_Cta.ListIndex() != $("#L_Cta").get(0).selectedIndex) {
                viewModel.L_Cta.ListIndex($("#L_Cta").get(0).selectedIndex);
                AjaxCall("FundTransfer/DESTFOND_L_Cta_Blur", function () {
                    //$('#L_Cuentas').removeAttr('disabled');
                    $('#L_Partys').removeAttr('disabled');
                });

            }
        };
        that.l_mnd_change = function () {
            viewModel.L_Mnd.ListIndex($("#L_Mnd").get(0).selectedIndex);
        };
        that.l_partys_change = function () {
            viewModel.L_Partys.ListIndex($("#L_Partys").get(0).selectedIndex);

            if (viewModel.L_Cuentas.ListCount() == 0)
                viewModel.L_Cuentas.Enabled(false);
            else
                viewModel.L_Cuentas.Enabled(true);
        };
        that.l_cuentas_change = function () {
            viewModel.L_Cuentas.ListIndex($("#L_Cuentas").get(0).selectedIndex);
            //AjaxCall("FundTransfer/DESTFOND_L_Cuentas_Blur");
            if (viewModel.L_Cuentas.ListIndex() != "-1")
                viewModel.Indice_CtaCte(viewModel.L_Cuentas.Items()[viewModel.L_Cuentas.ListIndex()].Data());
        };
        that.cb_destino_change = function () {
            viewModel.Cb_Destino.ListIndex($("#Cb_Destino").get(0).selectedIndex - 1);
        };
        that.cmb_codtran_change = function () {
            viewModel.cmb_codtran.ListIndex($("#cmb_codtran").get(0).selectedIndex - 1);
        };
        that.tx_datos_keypress = function (viewModel, event) {
            var key = String.fromCharCode(event.charCode || event.keyCode);
            if (key == "13") {
                ev.preventDefault();
                return function () {
                    AjaxCall("FundTransfer/DESTFOND_Tx_Datos_Blur?index=" + index, function () {
                        $(":focus").select();
                    });
                };
            }

            // Con el primer if, permitimos presionar las teclas de dirección
            if (activaTeclasEspeciales(key)) {
                return true;
            } else {
                // En el caso de que no sea un caracter aceptado por la funcion anterior, le aplica una expresion regular
                if (EsCaracterEspecial(key)) {
                    event.preventDefault();
                    return false;
                } else {
                    return true;
                }
            }
        };
        that.tx_datos_keypress_ConPunto = function (viewModel, event) {
            var key = String.fromCharCode(event.charCode || event.keyCode);
            // Con el primer if, permitimos presionar las teclas de dirección
            if (activaTeclasEspeciales(key)) {
                return true;
            } else {
                // En el caso de que no sea un caracter aceptado por la funcion anterior, le aplica una expresion regular
                if (_EsCaracterEspecialConPunto(key)) {
                    event.preventDefault();
                    return false;
                } else {

                    return true;
                }
            }
        };
        that.tx_datos_keypress_Nemonico = function (data, event) {
            var codKey = event.charCode || event.keyCode;
            var key = String.fromCharCode(codKey);
            if (codKey == "13") {
                event.preventDefault();
                AjaxCall("FundTransfer/DESTFOND_Tx_Datos_Blur?index=" + 0, function () {
                    $(":focus").select();
                });
                return false;

            }
            // Con el primer if, permitimos presionar las teclas de dirección
            if (activaTeclasEspeciales(codKey)) {
                return true;
            } else {
                // En el caso de que no sea un caracter aceptado por la funcion anterior, le aplica una expresion regular
                if (_EsCaracterEspecialNemonico(key)) {
                    event.preventDefault();
                    return false;
                } else {
                    return true;
                }
            }
        };
        //convierte a uppercase el texto del campo
        that.tx_datos_change_toUpper = function (viewModel, event) {
            $(event.target).val($(event.target).val().toUpperCase());
            $(event.target).change();
            //if (event.which >= 65 && event.which <= 90) {
            //    $(event.target).val($(event.target).val().toUpperCase());
            //    event.preventDefault();
            //    return false;
            //}
            //return true;
        };
        //alert(that.CargaAutomatica());
        $('#Bt_PlnTrn').attr("style", "visibility: hidden");
        if (that.CargaAutomatica() == 1) {
            $("#btn_ok").focus();
        } else {
            if (data.Lb_Datos[0].Text == "Nemónico" && data.Tx_Datos[0].Text != '') {
                $("#btn_ok").focus();
            } else {
                $("#L_Cta").focus();
            }
        }
    };

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
                boton = a.Boton[0].Enabled;

                if (func) {
                    func();
                }

                if (a.Errors.length == 0) {
                    if (a.Confirms.length > 0) {
                        $.unblockUI();
                        showConfirmMessages(a.Confirms, false, function (confirms) {
                            var allTrue = true;
                            for (var i = 0; i < confirms.length && allTrue; i++) {
                                allTrue = allTrue && confirms[i];
                            }
                            $.ajax({
                                url: baseUrl + "FundTransfer/DESTFOND_Ok_Click?vieneDe=true&res=true",
                                method: "POST",
                                data: {
                                    jsonModel: ko.mapping.toJS(viewModel, {
                                        ignore: ignoreList
                                    })
                                },
                                success: function (a) {
                                    updateModel(a);
                                    if (a.Boton[0].Enabled) {
                                        $('#btnAccept').removeAttr('disabled');
                                    }
                                    else {
                                        $('#btnAccept').attr('disabled', 'disabled');
                                    }
                                }
                            })
                        });
                    }
                }
                //Si tenemos errores, intentamos poner el cursor sobre campo de error
                else {
                    focusOnErrorControl(a.Errors);
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
        url: baseUrl + "FundTransfer/DESTFOND_Form_Get",
        success: function (data) {
            var nodo = DestinoFondos.get(0);
            ko.cleanNode(nodo);
            viewModel = new destinoFondosViewModel(data);
            ko.applyBindings(viewModel, nodo);
            //$('#btnAccept').attr('disabled', data.Boton[0].Enabled);
        }
    });

    $(window).keydown(function (ev) {
        var keycode = (ev.keyCode ? ev.keyCode : ev.which);
        if (keycode == 9) {  // Presiona Tab
            RecorrerTabIndex(-1, ev);
        }

        var Enter = false;
        if (keycode == '13' || keycode == 13) {  // Presiona Enter
            ev.preventDefault();
            if ($("#btnAccept").is(":enabled") && !$('#btn_ok').is(":enabled")) {
                $("#btnAccept").click();
            }
            else {
                $('#btn_ok').focus().ready().click();
            }
        }
    });
});