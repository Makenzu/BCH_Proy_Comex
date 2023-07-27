var my = {};
var stM = 0;
var ch_d = 0;
var ls_ch_d = 0;
var tbMo;
var fp = "32";
var pais = -1;
var moneda = -1;
var saldo = "";
var ValCif = "";
var MtoTot = "";
var CifDol = "";
var TotDol = "";
var tipoCambio = "";
var paridad = 0;
var vPrTl = 0.05;
var swTl = 0;
$(document).ready(function () {
    var baseUrl = $("#base_url").val();

    $(document).ajaxStart(function () {
        $.blockUI({ message: "<h6>Espere un momento ...</h6>" });
    }).ajaxStop($.unblockUI);

    var monedaIndex = 1;
    var viewModel = null;
    var pvimp = $("#pvimp")[0];
    var updateModel = function (newViewModel) {
        ko.cleanNode(pvimp);
        viewModel = new pvimpViewModel(newViewModel);
        ko.applyBindings(viewModel, pvimp);
        my = viewModel;
        ajustarIndexFinal();
        if ($("#Tx_CodPag").val() != "" && $("#Tx_CodPag").val() != fp) {
            my.Tx_CodPag = $("#Tx_CodPag").val();
        }
        else {
            $("#Tx_CodPag").val(fp);
            my.Tx_CodPag = fp;
        };
        if ($('#idCbPPago option:selected').val() != '' && $('#idCbPPago option:selected').index() != pais && pais != -1) {
            my.idCbPPago = $('#idCbPPago option:selected').val();
        }
        else {
            $("select#idCbPPago option").each(function () { this.selected = (this.value == $('#idCbPPago option:selected').val()); });
            my.idCbPPago = $('#idCbPPago option:selected').val();
        }
        if ($("#Tx_MtoFob").val() != "" && $("#Tx_MtoFob").val() != saldo) {
            my.Tx_MtoFob = $("#Tx_MtoFob").val();
        }
        else {
            $("#Tx_MtoFob").val(saldo);
            my.Tx_MtoFob = saldo;
        }
        if ($("#Pn_ValCif").val() != "" && $("#Pn_ValCif").val() != ValCif) {
            ValCif = $("#Pn_ValCif").val();
            my.Pn_ValCif = ValCif;
        }
        else {
            $("#Pn_ValCif").val(ValCif);
            my.Pn_ValCif = ValCif;
        }

        if ($("#Pn_MtoTot").val() != "" && $("#Pn_MtoTot").val() != MtoTot) {
            MtoTot = $("#Pn_MtoTot").val();
            my.Pn_MtoTot = MtoTot;
        }
        else {
            $("#Pn_MtoTot").val(MtoTot);
            my.Pn_MtoTot = MtoTot;
        }
        if ($("#Pn_CifDol").val() != "" && $("#Pn_CifDol").val() != CifDol) {
            CifDol = $("#Pn_CifDol").val();
            my.Pn_CifDol = CifDol;
        }
        else {
            $("#Pn_CifDol").val(CifDol);
            my.Pn_CifDol = CifDol;
        }

        if ($("#Pn_TotDol").val() != "" && $("#Pn_TotDol").val() != TotDol) {
            TotDol = $("#Pn_TotDol").val();
            my.Pn_TotDol = TotDol;
        }
        else {
            $("#Pn_TotDol").val(TotDol);
            my.Pn_TotDol = TotDol;
        }

        if ($("#Tx_TipCam").val() != "" && $("#Tx_TipCam").val() != tipoCambio && $("#Tx_TipCam").val() != '0,000') {
            my.Tx_TipCam = $("#Tx_TipCam").val();
        }
        else {
            $("#Tx_TipCam").val(tipoCambio);
            my.Tx_TipCam = tipoCambio;
        }
        if ($("#Pn_TCDol").val() != "" && $("#Pn_TCDol").val() != tipoCambio && $("#Pn_TCDol").val() != '0,0000') {
            my.Pn_TCDol = $("#Pn_TCDol").val();
        }
        else {
            $("#Pn_TCDol").val(tipoCambio);
            my.Pn_TCDol = tipoCambio;
        }
        if ($("#Tx_Paridad").val() != "" && $("#Tx_Paridad").val() != paridad && $("#Tx_Paridad").val() != '0,000') {
            my.Tx_Paridad = $("#Tx_Paridad").val();
        }
        else {
            $("#Tx_Paridad").val(paridad);
            my.Tx_Paridad = paridad;
        }
    }

    var pvimpViewModel = function (data) {
        ko.mapping.fromJS(data, {}, this);
        var that = this;
        that.cb_moneda_change = function () {
        };
        that.cb_moneda_Blur = function () {
            if (monedaIndex > 0 && monedaIndex != $("#idCbMoneda").find(":selected").index() && $("#idCbMoneda").find(":selected").index() > 0) {
                monedaIndex = $("#idCbMoneda").find(":selected").index();
                event('PlvSO_Cb_Moneda_Change', function () { $("#btn_OkDec").click(); });
            }
            // el caso de que sea la primera vez que entre con el enter, tengo que validar los campos.
            else {
                if (!validateOkDec_Click()) {
                    return;
                }
            }

        };
        that.tx_MtoFob_Blur = function () {
            if ($('#Tx_MtoFob').val() != "") {
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
                var mIng = numeral().unformat($('#Tx_MtoFob').val());
                if (((mIng > vTole5 || mIng > vTole100) && (saldo != 0 && saldo != "")) && !$('#Ch_Transf').prop('checked')) {
                    swTl = 1;
                    $('#msg_us').modal({ backdrop: 'static', keyboard: false });
                    $('#msg_us').modal('show');
                    $("#btn_ok").prop("disabled", true);
                    $('#btn_Close_Adv').focus();
                }
                else {
                    swTl = 0;
                    event('PlvSO_MtoFob_Blur');
                }
            }
        };
        that.tx_MtoFle_Blur = function () {
            if ($('#Tx_MtoFle').val() != "")
                event('PlvSO_MtoFle_Blur');
        };
        that.tx_MtoSeg_Blur = function () {
            if ($('#Tx_MtoSeg').val() != "")
                event('PlvSO_MtoSeg_Blur');
            $("#Tx_NroPre").focus().select();
        };
        that.tx_NroPre_Blur = function () {
            if ($('#Tx_NroPre').val() != "")
                $.ajax({
                    url: baseUrl + "FundTransfer/PlvSO_NroPre_Blur",
                    data: {
                        viewModel: ko.mapping.toJS(viewModel), NroPresentacion: $('#Tx_NroPre').val(),
                    },
                    method: "POST",
                    success: function (data) {
                        updateModel(data);
                    }
                });
        };
        that.Tx_FecRee_Blur = function () {
            if ($('#Tx_FecRee').val() != "")
                $.ajax({
                    url: baseUrl + "FundTransfer/PlvSO_Tx_FecRee_Blur",
                    data: { viewModel: ko.mapping.toJS(viewModel), Fecha: $('#Tx_FecRee').val(), NroPresentacion: $('#Tx_NroPre').val() },
                    method: "POST",
                    success: function (data) {
                        updateModel(data);
                    }
                });
        };
        that.tx_TipCam_Blur = function () {
            var rutImp = viewModel.Pn_RutImp();
            if (rutImp != null || rutImp != "") {
                event('PlvSO_TipCam_Blur', function () {
                    console.log(ch_d + '  -  ch_d');
                    console.log(ls_ch_d + '  -  ls_ch_d');
                });
            }
        };
        that.btn_chilefx_click = function () {
            showConsultaDealsDisponible();
        };
        that.myFunc = function (chk) {
            var data = { jsonModel: ko.mapping.toJS(viewModel) };
            $.ajax({
                method: "POST",
                url: baseUrl + "FundTransfer/PlvSO_ch_deal_change/",
                data: {
                    jsonModel: { jsonModel: ko.mapping.toJS(viewModel) },
                    chk: chk
                },
                success: function (a) {
                    closeModal();
                    updateModel(a);
                    $('#idCbPPago').prop("disabled", false);
                    $('#Tx_MtoFob').prop("disabled", false);
                    $('#Tx_MtoFle').prop("disabled", false);
                    $('#Tx_MtoSeg').prop("disabled", false);
                    $('#Tx_TipCam').prop("disabled", true);
                    $('#Tx_Observ').prop("disabled", false);
                    $('#Ch_PlanRee').prop("disabled", false);
                    $('#btn_OkFinal').prop("disabled", false);
                    $('#Tx_CodPag').focus();
                },
                error: function (err) {
                    //console.error(err.responseText);
                }
            });
        };
        that.tx_Paridad_Blur = function () {
            event('PlvSO_Paridad_Blur');
        };
        that.tx_CodPag_Blur = function () {
            viewModel.id
        };
        that.tx_DocChi_Blur = function () {
            event('PlvSO_Tx_DocChi_Blur');
        };
        that.lt_Final_Click = function () {
            if ($("#selectedLtFinal option:selected").val() > -1) {
                event('PlvSO_Tt_Final_Click', function () {
                    $('#idCbPPago').prop("disabled", false);
                    $('#Tx_MtoFob').prop("disabled", false);
                    $('#Tx_MtoFle').prop("disabled", false);
                    $('#Tx_MtoSeg').prop("disabled", false);
                    $('#Tx_TipCam').prop("disabled", false);
                    $('#Tx_Observ').prop("disabled", false);
                    $('#Ch_PlanRee').prop("disabled", false);
                    $("#btn_OkFinal").prop("disabled", false);
                });
            }
        };
        that.btn_NoFinal_Click = function () {
            event('PlvSO_Bot_NoFinal_Click');
            $("#Tx_NroPre").val('');
            $("#Tx_FecRee").val('');
            $("#btn_OkFinal").prop("disabled", true);
        };
        that.bot_Acepta_Click = function () {
            $.ajax({
                url: baseUrl + "FundTransfer/PlvSO_Bot_Acepta_Click/",
                data: { viewModel: ko.mapping.toJS(viewModel) },
                method: "POST",
                success: function (data) {
                    window.location.href = baseUrl + "FundTransfer/" + data.FormularioQueAbrir;
                }
            });
        };
        that.btn_OkDec_Click = function () {
            if (!validateOkDec_Click()) {
                return;
            }
            $.ajax({
                url: baseUrl + "FundTransfer/PlvSO_Bot_OkDec_Click/",
                data: { viewModel: ko.mapping.toJS(viewModel) },
                method: "POST",
                success: function (data) {
                    updateModel(data);
                    $('#idCbPPago').prop("disabled", false);
                    $('#Tx_MtoFob').prop("disabled", false);
                    $('#Tx_MtoFle').prop("disabled", false);
                    $('#Tx_MtoSeg').prop("disabled", false);
                    $('#Tx_TipCam').prop("disabled", false);
                    $('#Tx_Observ').prop("disabled", false);
                    $('#Ch_PlanRee').prop("disabled", false);
                    $('#btn_OkFinal').prop("disabled", false);
                }
            });
            $("#Ch_Transf").focus().select();
        };
        that.btn_OkFinal_Click = function () {
            var flagTransfe = $('#Ch_Transf').prop('checked');
            $.ajax({
                url: baseUrl + "FundTransfer/PlvSO_Bot_OkFinal_Click/",
                data: { viewModel: ko.mapping.toJS(viewModel) },
                method: "POST",
                success: function (data) {
                    updateModel(data);
                    if (viewModel.MensajesDeError().length == 0) {
                        var TC_SD = parseFloat(tipoCambio.replace('.', '|').replace(',', '.').replace('|', ''));
                        var TC_ID = parseFloat($('#Tx_TipCam').val() .replace('.', '|').replace(',', '.').replace('|', ''));
                        if (TC_ID != TC_SD && tipoCambio != "" && flagTransfe == false) {
                            var msgErr = ['Se agrego operación con tipo de cambio pizarra.'];
                            msgErr = $.map(msgErr, function (elem, index) {
                                return { Type: 2, Text: elem, Title: "Aviso! <br />" };
                            });
                            loadMessages(msgErr);
                        }
                        $("#Tx_NroPre").val('');
                        $("#Tx_FecRee").val('');

                        $('#idCbMoneda').val('--Seleccione--');
                        $('#Tx_MtoFob').val('');
                        $('#Tx_MtoFle').val('');
                        $('#Tx_MtoSeg').val('');
                        $('#Pn_ValCif').val('');
                        $('#Pn_MtoTot').val('');
                        $('#Pn_CifDol').val('');
                        $('#Pn_TotDol').val('');
                        $('#Tx_TipCam').val('');
                        $('#Pn_TCDol').val('');
                        $('#Tx_Paridad').val('0');

                        $('#btnAceptar').prop("disabled", false);
                        disable();
                        // se deshabilitan los controles que se habilitan ejecutar la funcion btn_OkDec_Click
                        $('#idCbPPago').prop("disabled", true);
                        $('#Tx_MtoFob').prop("disabled", true);
                        $('#Tx_MtoFle').prop("disabled", true);
                        $('#Tx_MtoSeg').prop("disabled", true);
                        $('#Tx_TipCam').prop("disabled", true);
                        $("#btn_OkFinal").prop("disabled", true);
                        // fin correccion
                        $("#btnAceptar").focus().select();
                    }
                    else {
                        var msg = viewModel.MensajesDeError()[0];
                        if (msg == 'El Tipo de Cambio Venta supera en un 2% el Tipo de Cambio de pizarra.' || msg == 'Cuando el primer registro se crea con tipo de cambio pizarra, no puede ingresar más.') {
                            $("#Tx_NroPre").val('');
                            $("#Tx_FecRee").val('');
                            $('#idCbMoneda').val('--Seleccione--');
                            $('#Tx_MtoFob').val('');
                            $('#Tx_MtoFle').val('');
                            $('#Tx_MtoSeg').val('');
                            $('#Pn_ValCif').val('');
                            $('#Pn_MtoTot').val('');
                            $('#Pn_CifDol').val('');
                            $('#Pn_TotDol').val('');
                            $('#Tx_TipCam').val('');
                            $('#Pn_TCDol').val('');
                            $('#Tx_Paridad').val('0');
                        }
                    }
                }
            });
        };
        that.Ch_Transf_Click = function () {
            if ($('#Ch_Transf').prop('checked')) {
                $("#Tx_TipCam").val(0);
                my.Tx_TipCam = 0;
                $("#Pn_TCDol").val(0);
                my.Pn_TCDol = 0;
                $("#Tx_Paridad").val(0);
                my.Tx_Paridad = 0;
            }
            $.ajax({
                url: baseUrl + "FundTransfer/PlvSO_Ch_Transf_Click/",
                data: { viewModel: ko.mapping.toJS(viewModel) },
                method: "POST",
                success: function (data) {
                    updateModel(data);
                }
            });
        };
        that.Ch_PlanRee_Click = function () {
            $.ajax({
                url: baseUrl + "FundTransfer/PlvSO_Ch_PlanRee_Click/",
                data: { viewModel: ko.mapping.toJS(viewModel) },
                method: "POST",
                success: function (data) {
                    updateModel(data);
                    if (data.Ch_PlanRee == true) {
                        $('#Tx_NroPre').prop("disabled", false);
                        $('#Tx_FecRee').prop("disabled", false);

                    }
                    else {
                        $('#Tx_NroPre').prop("disabled", true);
                        $('#Tx_FecRee').prop("disabled", true);
                    }
                }
            });
        };
        that.Ch_PlanRee_Click = function () {
            $.ajax({
                url: baseUrl + "FundTransfer/PlvSO_Ch_PlanRee_Click/",
                data: { viewModel: ko.mapping.toJS(viewModel) },
                method: "POST",
                success: function (data) {
                    updateModel(data);
                    if (data.Ch_PlanRee == true) {
                        $('#Tx_NroPre').prop("disabled", false);
                        $('#Tx_FecRee').prop("disabled", false);

                    }
                    else {
                        $('#Tx_NroPre').prop("disabled", true);
                        $('#Tx_FecRee').prop("disabled", true);
                    }
                }
            });
        };

        function disable() {
            $('#idCbMoneda').prop("disabled", true);
            $('#idCbPbc').prop("disabled", true);
        }
        function validateOkDec_Click() {
            if ($("#Tx_CodPag").val() == "") {
                mostrarMensajesDeError(['Debe Ingresar el Código Forma de Pago.']);
                $("#Tx_CodPag").focus();
                return false;
            }

            if ($("#idCbMoneda").find(":selected").index() == 0) {
                mostrarMensajesDeError(['Debe Ingresar la Moneda.']);
                $("#idCbMoneda").focus();
                return false;
            }
            if ($("#Pn_CodMon").val().trim() == "") {
                mostrarMensajesDeError(['Debe Ingresar la Moneda.']);
                $("#idCbMoneda").focus();
                return false;
            }
            return true;
        }
        function mostrarMensajesDeError(error) {
            var msgErr = error;
            msgErr = $.map(msgErr, function (elem, index) {
                return { Type: 3, Text: elem, Title: "Error de Validación: " };
            });
            loadMessages(msgErr);
        };
        function cancel() {
            window.location.href = baseUrl + "FundTransfer/PlvSO_Bot_Cancel_Click";
        }
        function event(name, func) {
            $.ajax({
                url: baseUrl + "FundTransfer/" + name,
                data: { viewModel: ko.mapping.toJS(viewModel) },
                method: "POST",
                async: false,
                success: function (data) {
                    updateModel(data);
                    if (func) {
                        func();
                    }
                }
            });
        }

        mostrarMensajesDeError(data.MensajesDeError);
        focusOnErrorControl(data.MensajesDeError);
        $("#btnCancelar").click(cancel);
        $("#idCbMoneda").blur(that.cb_moneda_Blur);
        $("#idCbMoneda").change(that.cb_moneda_change);
        $("#btn_OkDec").click(that.btn_OkDec_Click);
        $("#btn_chilefx").click(that.btn_chilefx_click);
        $("#btn_OkFinal").click(that.btn_OkFinal_Click);
        $("#btnAceptar").click(that.bot_Acepta_Click);
        $("#btnCancelar").click(that.btn_Cancel_Click);
        $("#btn_NoFinal").click(that.btn_NoFinal_Click);
        $("#Tx_MtoFob").blur(that.tx_MtoFob_Blur);
        $("#Tx_MtoFle").blur(that.tx_MtoFle_Blur);
        $("#Tx_MtoSeg").blur(that.tx_MtoSeg_Blur);
        $("#Tx_NroPre").blur(that.tx_NroPre_Blur);
        $("#Tx_FecRee").blur(that.Tx_FecRee_Blur);
        $("#Tx_TipCam").blur(that.tx_TipCam_Blur);
        $("#Tx_Paridad").blur(that.tx_Paridad_Blur);
        $("#Tx_CodPag").blur(that.tx_CodPag_Blur);
        $("#Tx_DocChi").blur(that.tx_DocChi_Blur);
        $("#selectedLtFinal").click(that.lt_Final_Click);
        $(":input").inputmask();
    };

    function ajustarIndexFinal() {
        var options = $('#selectedLtFinal > option')
        var length = options.length;

        for (i = 0; i < length; i++) {
            $('#selectedLtFinal > option')[i].value = i;
        }
    }
    var selectViewModel = function (data) {
        ko.mapping.fromJS(data, {}, this);
    };
    $.ajax({
        method: "GET",
        cache: false,
        url: baseUrl + "FundTransfer/PlvSO_LoadFrm",
        success: function (data) {
            updateModel(data);
        }
    });

    $('#Tx_NumDec').inputmask("9{1,15}\-[A9]");
    $("#Tx_CodPag").focus().select();
    $('#btn_OkFinal').prop("disabled", true);

    $('#ConsultaDealsDisponible').on('hidden.bs.modal', function () {
        stM = 0;
    });
    $('#ConsultaDealsDisponible').on('show.bs.modal', function () {
        stM = 1;
    });
    $('#msg_us').on('show.bs.modal', function () {
        swTl = 1;
    });
    $('#msg_us').on('hidden.bs.modal', function () {
        swTl = 0;
        $("#Tx_MtoFob").val(saldo);
        my.Tx_MtoFob = saldo;
        $("#Pn_ValCif").val(saldo);
        ValCif = saldo;
        my.Pn_ValCif = ValCif;
        $("#Pn_MtoTot").val(saldo);
        MtoTot = saldo;
        my.Pn_MtoTot = MtoTot;
        $("#Pn_CifDol").val(saldo);
        CifDol = saldo;
        my.Pn_CifDol = CifDol;
        $("#Pn_TotDol").val(saldo);
        TotDol = saldo;
        my.Pn_TotDol = TotDol;
        my.tx_MtoFob_Blur();
    });
    /// Cuando haga Enter en cualquier parte de la pantalla
    $(window).keydown(function (ev) {
        var keycode = (ev.keyCode ? ev.keyCode : ev.which);
        if (keycode == 9) {  // Presiona Tab
            var tab = $(document.activeElement).attr("tabindex");
            if ((tab == -1 && stM == 1) || swTl == 1 || typeof tab == "undefined") {
                RecorrerTabIndex(100, ev);
            }
            else {
                RecorrerTabIndex(-1, ev);
            }
        }

        if (keycode == 13)  // Presiona Enter
        {
            $('#msg_us').modal('hide');
            ev.preventDefault();
            if ($("#btnAceptar").is(":enabled") && $("#btnAceptar").is(":focus")) {
                $("#btnAceptar").click();
            }
            else if ($("#btn_OkFinal").is(":disabled")) {
                if (viewModel.Pn_CodMon() == "") {
                    $("#idCbMoneda").blur();
                }
                else {
                    $("#btn_OkDec").focus().click();
                }
            }
            else if ($("#btn_OkDec").is(":focus")) {
                $("#btn_OkDec").click();
            }
            else {
                if ($("#Tx_MtoFob").is(":focus")) {
                    $("#Tx_MtoFob").blur();
                }

                $("#btn_OkFinal").focus().click();
            }
        }
        if (keycode == 27) {    // Presiona Esc
            $('#msg_us').modal('hide');
        }
    });
    $(window).on('load', function () {
        showConsultaDealsDisponible();
    });
    $("#btn_cl_CDD").click(function () {
        closeModal();
    });
    function showConsultaDealsDisponible() {
        EmptyMessageZone();
        const bc = [" bcf ", " bcs "]
        const tg_td = "<td class='tg-td ";
        $.ajax({
            url: baseUrl + "FundTransfer/PlvSO_WM_Deals",
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
                            + tg_td + bc[pf] + "ac w64 td-arbt'>" + "<input type='checkbox' id='ch_dl_" + (f) + "' tabindex='" + (f + 100) + "' onclick='ch_deal_change(this)' />" + "</td>"
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
    if ($(chk).is(":checked") == true)
    {
        $('#Tx_TipCam').prop("disabled", true);
        var idx = chk.id.replace("ch_dl_", "");
        saldo = $("#td_sa" + idx).text();
        tipoCambio = $("#td_tc" + idx).text();
        pais = $('#idCbPPago option:selected').index();
    }
    else {
        $('#Tx_TipCam').prop("disabled", false);
        saldo = "";
        tipoCambio = "";
        pais = "";
        ValCif = "";
        MtoTot = "";
        CifDol = "";
        TotDol = "";
    }
    
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
            closeModal();
        }
    });
};
