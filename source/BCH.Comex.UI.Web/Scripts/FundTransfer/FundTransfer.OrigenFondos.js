$(document).ready(function () {
    var baseUrl = $("#base_url").val();
    $(document).ajaxStart(function () {
        $.blockUI({ message: "<h6>Espere un momento ...</h6>" });
    }).ajaxStop($.unblockUI);
    var changedMoneda = false;
    var viewModel = null;
    var boton = false;

    //inicializo KO
    var nodo = $("#OrigenFondos").get(0);
    ko.cleanNode(nodo);
    viewModel = new OrigenFondosViewModel(modeloCompleto);
    ko.applyBindings(viewModel, nodo);

    var updateModel = function (data) {
        ko.mapping.fromJS(data, {}, viewModel);
        //obtengo los errores y los muestro
        var errors = ko.mapping.toJS(viewModel.Errors);
        loadMessages(errors);
    }

    function OrigenFondosViewModel(data) {
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
        that.ORIFOND_BNem_Click = ORIFOND_BNem_Click;
        that.ORIFOND_Boton_Click = ORIFOND_Boton_Click;
        that.ORIFOND_Bt_PlnTrn_Click = ORIFOND_Bt_PlnTrn_Click;
        that.ORIFOND_cmb_codtran_Blur = ORIFOND_cmb_codtran_Blur;
        that.ORIFOND_L_Cta_Blur = ORIFOND_L_Cta_Blur;
        that.ORIFOND_L_Cuentas_Blur = ORIFOND_L_Cuentas_Blur;
        that.ORIFOND_l_mnd_Blur = ORIFOND_l_mnd_Blur;
        that.ORIFOND_l_mto_Blur = ORIFOND_l_mto_Blur;
        that.ORIFOND_l_ori_Blur = ORIFOND_l_ori_Blur;
        that.ORIFOND_L_Partys_Blur = ORIFOND_L_Partys_Blur;
        that.ORIFOND_NO_Click = ORIFOND_NO_Click;
        that.ORIFOND_Tx_Datos_Blur = ORIFOND_Tx_Datos_Blur
        that.ORIFOND_cmb_codtran_Change = ORIFOND_cmb_codtran_Change;
        that.ORIFOND_L_Cta_Change = ORIFOND_L_Cta_Change;
        that.ORIFOND_L_Cuentas_Change = ORIFOND_L_Cuentas_Change;
        that.ORIFOND_l_mnd_Change = ORIFOND_l_mnd_Change;
        that.ORIFOND_L_Partys_Change = ORIFOND_L_Partys_Change;

        that.ORIFOND_ok_Click = function () {
            //$('#L_Cuentas').attr('disabled', 'disabled');
            //$('#L_Partys').attr('disabled', 'disabled');

            if ($('#L_Cta').val() == '-1') {
                showAlert("", "Debe seleccionar un origen de fondos ", "alert-danger", true);
                $('#L_Cta').focus();
                return;
            }
            if (boton) {
                $('#btnAccept').removeAttr('disabled');
            }
            else {
                $('#btnAccept').attr('disabled', 'disabled');
            }
            AjaxCall("FundTransfer/ORIFOND_ok_Click", function () {
                if (viewModel.Errors().length == 0) {
                    if (viewModel.Confirms().length > 0) {
                        showConfirmMessages(ko.mapping.toJS(viewModel.Confirms), false, function (confirms) {
                            var allTrue = true;
                            for (var i = 0; i < confirms.length && allTrue; i++) {
                                allTrue = allTrue && confirms[i];
                            }
                            viewModel.Confirms([]);
                            if (allTrue) {
                                AjaxCall("FundTransfer/ORIFOND_ok_Click_Final", function () {

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
                                    var valorMonto = numeral().unformat($("#MtoOri").val());
                                    if (valorMonto > 0) {
                                        //$("#btn_ok").focus();
                                        $("#L_Cta").focus();
                                    }
                                    else {
                                        //$('#btn_ok').attr('disabled', 'disabled');
                                        $("#btnAccept").focus();
                                    }
                                    //
                                });
                            }
                        });
                    } else {
                        AjaxCall("FundTransfer/ORIFOND_ok_Click_Final", function () {

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
                            var valorMonto = numeral().unformat($("#MtoOri").val());
                            if (valorMonto > 0) {
                                //$("#btn_ok").focus();
                                $("#L_Cta").focus();
                            }
                            else {
                                //$('#btn_ok').attr('disabled', 'disabled');
                                $("#btnAccept").focus();
                            }
                            //
                        });
                    }                    
                }
            });
        }
        that.tx_datos_keypress = function (viewModel, event) {
            var codKey = event.charCode || event.keyCode;
            var key = String.fromCharCode(codKey);
            if (codKey == 13) {
                return function () {
                    AjaxCall("FundTransfer/ORIFOND_Tx_Datos_Blur?index=" + index, function () {
                        $(":focus").select();
                    });
                };
            }
            // Con el primer if, permitimos presionar las teclas de dirección
            if (activaTeclasEspeciales(codKey)) {
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
            if (codKey == 13) {
                event.preventDefault();
                AjaxCall("FundTransfer/ORIFOND_Tx_Datos_Blur?index=" + 0, function () {
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
        $('#Bt_PlnTrn').attr("style", "visibility: hidden");
        if (that.CargaAutomatica() == 1) {
            if (data.L_Cuentas.Enabled) {
                $("#L_Cuentas").focus();
            }
            else {
                $("#cmb_codtran").focus();
            }
        } else {
            if (data.Lb_Datos[0].Text == "Nemónico" && data.Tx_Datos[0].Text != '') {
                $("#btn_ok").focus();
            } else {
                $("#L_Cta").focus();
            }
        }
    }
    

    function AjaxCall(url, func) {
        var ignoreList = ["ORIFOND_BNem_Click",
                        "ORIFOND_Boton_Click",
                        "ORIFOND_Bt_PlnTrn_Click",
                        "ORIFOND_cmb_codtran_Blur",
                        "ORIFOND_L_Cta_Blur",
                        "ORIFOND_L_Cuentas_Blur",
                        "ORIFOND_l_mnd_Blur",
                        "ORIFOND_l_mto_Blur",
                        "ORIFOND_l_ori_Blur",
                        "ORIFOND_L_Partys_Blur",
                        "ORIFOND_NO_Click",
                        "ORIFOND_Tx_Datos_Blur",
                        "ORIFOND_ok_Click",
                        "ORIFOND_cmb_codtran_Change",
                        "ORIFOND_L_Cta_Change",
                        "ORIFOND_L_Cuentas_Change",
                        "ORIFOND_l_mnd_Change",
                        "ORIFOND_L_Partys_Change",
                        "tx_datos_keypress",
                        "tx_datos_keypress_ConPunto",
                        "Bt_PlnTrn.Visible"];
        viewModel.Errors([]);
        viewModel.Confirms([]);
        $.ajax({
            method: "POST",
            url: baseUrl + url,
            data: {
                jsonModel: ko.mapping.toJS(viewModel, {
                    ignore: ignoreList
                })
            },
            success: function (a) {
                boton = a.Boton[0].Enabled;
                //if (url == "FundTransfer/ORIFOND_ok_Click_Final") {
                //    $('#L_Cta').val('-1');
                //    if( $('#L_Cta').val() === null)
                //        $('#L_Cta').append($('<option></option>').val('-1').html('')).val('-1');
                //}
                updateModel(a);
                if (url == "FundTransfer/ORIFOND_l_ori_Blur")
                {
                    $('#L_Cta').val(a.L_Cta.Value);
                }
                if (a.Errors.length == 0) {
                    if (func) {
                        func();
                    }
                }
                //Si tenemos errores, intentamos poner el cursor sobre campo de error
                else {
                    focusOnErrorControl(a.Errors);
                }
            },
            error: function (err) {
                console.error(err);
            }
        });
    }

    function ORIFOND_BNem_Click() {
        AjaxCall("FundTransfer/ORIFOND_BNem_Click", function () {
            window.location.href = baseUrl + "FundTransfer/NemonicoCuenta";
        });
    }
    function ORIFOND_Boton_Click(but) {
        return function () {
            AjaxCall("FundTransfer/ORIFOND_Boton_Click/" + but, function () {
                if (viewModel.Errors().length == 0) {
                    window.location.href = baseUrl + "FundTransfer/ORIFOND_Aceptar";
                }
            })
        }
    }
    function ORIFOND_Bt_PlnTrn_Click() {
        AjaxCall("FundTransfer/ORIFOND_Bt_PlnTrn_Click", function () {
            window.location.href = baseUrl + "FundTransfer/PlanillasTransferencia";
        });
    }
    function ORIFOND_cmb_codtran_Change() {
        viewModel.cmb_codtran.ListIndex($("#cmb_codtran").get(0).selectedIndex - 1);
    }
    function ORIFOND_cmb_codtran_Blur() {
        //AjaxCall("FundTransfer/ORIFOND_cmb_codtran_Blur");
    }
    function ORIFOND_L_Cta_Change() {
        if (viewModel.L_Cta.ListIndex() != $("#L_Cta").get(0).selectedIndex) {
            viewModel.L_Cta.ListIndex($("#L_Cta").get(0).selectedIndex);
            if ($("#L_Cta").val() != -1) {
                AjaxCall("FundTransfer/ORIFOND_L_Cta_Blur");
                //$('#L_Cuentas').removeAttr('disabled');
                $('#L_Partys').removeAttr('disabled');
            }
        }
    }
    function ORIFOND_L_Cta_Blur() {
        //if ($("#L_Cta").val() != -1) {
        //    AjaxCall("FundTransfer/ORIFOND_L_Cta_Blur");
        //    //$('#L_Cuentas').removeAttr('disabled');
        //    $('#L_Partys').removeAttr('disabled');
        //}
    }
    function ORIFOND_L_Cuentas_Change() {
        viewModel.L_Cuentas.ListIndex($("#L_Cuentas").get(0).selectedIndex);
        //AjaxCall("FundTransfer/ORIFOND_L_Cuentas_Blur");
        if (viewModel.L_Cuentas.ListIndex() != "-1")
            viewModel.Indice_CtaCte(viewModel.L_Cuentas.Items()[viewModel.L_Cuentas.ListIndex()].Data());
    }
    function ORIFOND_L_Cuentas_Blur() {
//        AjaxCall("FundTransfer/ORIFOND_L_Cuentas_Blur");
    }
    function ORIFOND_l_mnd_Change() {
        changedMoneda = true;
        viewModel.l_mnd.ListIndex($("#L_Mnd").get(0).selectedIndex);
    }
    function ORIFOND_l_mnd_Blur() {
        if (changedMoneda) {
            changedMoneda = false;
            AjaxCall("FundTransfer/ORIFOND_l_mnd_Blur");
        }
    }
    function ORIFOND_l_mto_Blur(nuevo) {
        return function () {
            if (nuevo() == viewModel.l_mto.ListIndex()) {
                //viewModel.l_mto.ListIndex(-1);
            } else {
                viewModel.l_mto.ListIndex(nuevo());
            }
            AjaxCall("FundTransfer/ORIFOND_l_mto_Blur");
        }
    }
    function ORIFOND_l_ori_Blur(nuevo) {
        return function () {
            var n = nuevo();
            if (n == viewModel.l_ori.ListIndex()) {
                viewModel.l_ori.ListIndex(-1);
            }
            else {
                viewModel.l_ori.ListIndex(n);
            }

            $('#L_Cuentas').removeAttr('disabled');
            $('#L_Partys').removeAttr('disabled');
            $('#btn_no').removeAttr('disabled');
            AjaxCall("FundTransfer/ORIFOND_l_ori_Blur", function () {
                //$('#L_Partys').val(n);
            });
            $('#btn_ok').removeAttr('disabled');
        }
    }
    function ORIFOND_L_Partys_Change() {
        viewModel.L_Partys.ListIndex($("#L_Partys").get(0).selectedIndex);
    }
    function ORIFOND_L_Partys_Blur() {
        AjaxCall("FundTransfer/ORIFOND_L_Partys_Blur");
    }
    function ORIFOND_NO_Click() {
        AjaxCall("FundTransfer/ORIFOND_NO_Click", function () {
            if (viewModel.Confirms().Length > 0) {
                showConfirmMessages(ko.mapping.toJS(viewModel), false, function (confirms) {
                    var isTrue = confirms[0];
                    AjaxCall("FundTransfer/ORIFOND_NO_Click?pasoPor=true&acepta=" + isTrue);
                });                
            }
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
    }
    /// Cuando haga Enter en cualquier parte de la pantalla
    //$(window).bind('keypress', function (ev) {
    //    var keycode = (ev.keyCode ? ev.keyCode : ev.which);
    //    var Enter = false;
    //    if (keycode == '13' || keycode == 13) {  // Presiona Enter
    //        Enter = true;
    //    }

    //    if (Enter && !$("#btn_ok").is(":focus")) {
    //        $('#btn_ok').focus().ready().click();
    //    }
    //});

    function ORIFOND_Tx_Datos_Blur(indice) {
        //return function () {
        //    AjaxCall("FundTransfer/ORIFOND_Tx_Datos_Blur?index=" + indice);
        //}
    }
    
    $("#modal").on("shown.bs.modal", function () { $("#modal button.btn-primary").focus(); })
    $("#modal").on("hidden.bs.modal", function () { if ($("#btnAceptar").is(":enabled")) { $("#btnAceptar").focus(); } else { $("#l_cta").focus() } })

    $(window).keydown(function (ev) {
        var keycode = (ev.keyCode ? ev.keyCode : ev.which);
        if (keycode == 9) {  // Presiona Tab
            RecorrerTabIndex(-1, ev);
        }
        else if (keycode == '13' || keycode == 13) {  // Presiona Enter
            ev.preventDefault();
            if ($("#modal").is(":visible")) {
                $("#modal button.btn-primary").click();
            }
            else if ($("#btnAccept").is(":enabled") && parseInt($('#MtoOri').val()) == 0) {
                $("#btnAccept").click();
            }
            else {
                $('#btn_ok').focus().ready().click();
            }
        }
    });
});