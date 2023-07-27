$(document).ready(function () {
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI).ajaxError($.unblockUI);
    var baseUrl = $("#base_url").val() + "ContabilidadGenerica/";

    mostrarConfirmacionesSiExisten();
    aplicaMascara($("#monedas").val());

    $('#mnuConfiguracion input[type=checkbox]').change(function () {
        $.ajax({
            url: baseUrl + "Home/Index_ConfigImprimirClick",
            method: "Post",
            data: { elem: $(this).attr('id'), value: $(this).prop('checked') },
            success: function (data) {
                ViewModelToView(data);
            },
            error: function (response, type, message) {
                try {
                    var responseJson = JSON.parse(response.responseText);
                    showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
                }
                catch (err) {
                    showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", true);
                }
            }
        });
    });

    var lastValueMoneda = $("#monedas").val();
    var lastValueNem = $("#nemonico").val();
    var lastValueMonto = $("#monto").val();
    var lastNumRef = $("#txtNumRef").val(); 
    var lastReferenciaCliente = $("#Tx_ReferenciaCliente").val();
    var monedaActual = "";
    var esMN = false;
    var indiceLista = -1;

    //esto se hece para mantener la linea seleccionada despues de recargar la página
    if (m_e_index != "-1") {
        $("#m_e tbody tr:nth-child(" + (m_e_index + 1)  + ")").addClass("blue-text");
    }

    if (m_n_index != "-1") {
        $("#m_n tbody tr:nth-child(" + (m_n_index + 1) + ")").addClass("blue-text");
    }

    var tipo0IsChecked = $("#tipo_000").data("checked");
    if (tipo0IsChecked == "True") {
        $("#tipo_000").prop("checked", true);
    } else if (tipo0IsChecked == "False") {
        $("#tipo_001").prop("checked", true);
    }

    $("#monedas").on("keyup click change", function (e) { //no solo en change por firefox
        var keyCode = e.keyCode || e.which;
        var newValue = $("#monedas").val();
        if (lastValueMoneda != newValue && newValue != '') {
            lastValueMoneda = newValue;
            $.ajax({
                url: baseUrl + "Home/Monedas_Click",
                data: { value: newValue },
                success: function (data, e) {
                    $("#monto").inputmask("remove");
                    ViewModelToView(data);
                    aplicaMascara(newValue);
                },
                error: function (response, type, message) {
                    try {
                        var responseJson = JSON.parse(response.responseText);
                        showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
                    }
                    catch (err) {
                        showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", true);
                    }
                }
            });
        }
    });

    $("#monto").on("blur", function () {
        var newValue = $("#monto").val();
        if (newValue != lastValueMonto) {
            $.ajax({
                url: baseUrl + "Home/Monto_Blur",
                data: { monto: newValue },
                success: function (data) {
                    ViewModelToView(data);
                    lastValueMonto = $("#monto").val();
                }
            });
        }
    });
    $("#monto").on("focus", function () {
        $(this).select();
    });
    $("[name='debehaber']").on("change", function () {
        $.ajax({
            url: baseUrl + "Home/DebeHaber_Change",
            data: { debe: $("#tipo_000").prop("checked") },
            success: function (data) {
                ViewModelToView(data);
            }
        });
    });
    $("#nemonico").on("blur", function () {
        var texto = $(this).val();
        if (texto != lastValueNem) {
            lastValueNem = texto;
            $.ajax({
                url: baseUrl + "Home/Nemonico_Blur",
                data: { texto: texto.toUpperCase()},
                success: function (data) {
                    ViewModelToView(data);
                }
            });
        }
    });
    $("#ver").on("click", function () {
        window.location = baseUrl + "Home/Ver_1";
    });
    $("#txtNumRef").on("blur", function () {
        var newVal = $("#txtNumRef").val();
        if (newVal != lastNumRef) {
            lastNumRef = newVal;
            $.ajax({
                url: baseUrl + "Home/txtNumRef_Blur",
                data:{texto:newVal}
            });
        }
    });
    $("#Tx_ReferenciaCliente").on("blur", function () {
        var newVal = $("#Tx_ReferenciaCliente").val();
        if (newVal != lastReferenciaCliente) {
            lastReferenciaCliente = newVal;
            $.ajax({
                url: baseUrl + "Home/Tx_ReferenciaCliente_Blur",
                data: { texto: newVal }
            });
        }
    });
    $('#cambiar').change(function () {
        $.ajax({
            url: baseUrl + "Home/Cambiar_Click",
            method: "Post",
            data: {value: $(this).prop('checked') },
            success: function (data) {
            },
            complete: function () {
            }
        });
    });
    $("#impuesto").on("change", function () {
        $.ajax({
            url: baseUrl + "Home/Impuesto_Change",
            data: { value: $("#impuesto").prop("Checked") }
        });
    });
    $("#aceptar").on("click", function () {
        $("#nemonico").blur();
        var monedaValue = $("#monedas").val();
        if (monedaValue != "") {
            $("#aceptar").attr('disabled', 'disabled');
            $.ajax({
                url: baseUrl + "Home/Aceptar",
                cache: false,
                success: function (data) {
                    if (data.ListaErrores.length > 0 || data.ListaConfirmaciones.length > 0) {
                        if(data.ListaErrores.length > 0 ){
                            loadMessages(data.ListaErrores);
                            focusOnErrorControl(data.ListaErrores);
                            $("#aceptar").removeAttr('disabled');
                        }
                        if (data.ListaConfirmaciones.length > 0) {
                            showConfirmMessages(data.ListaConfirmaciones, false, function (confirms) {
                                var allTrue = true;
                                for (var i = 0; i < confirms.length && allTrue; i++) {
                                    allTrue = allTrue && confirms[i];
                                }
                                if (allTrue) {
                                    window.location = baseUrl + "Home/Aceptar_ValidacionSaldo?saltarValidacionSaldo=" + allTrue;
                                }

                            });
                        }
                    } else {
                        window.location = baseUrl + "Home/Aceptar_Correcto";
                    }
                }
            });
        } else {
            $("#monedas").focus();
            showAlert("Error en la operación.", "Detalles: Debe seleccionar una moneda", "alert-danger", true);
        }
    });
    $("#cancelar").on("click", function () {
        window.location = baseUrl + "Home/Cancelar?nemMon="+monedaActual+"&monNac="+esMN+"&indLista="+indiceLista;
    });
    $("#tbr_nuevo").on("click", function () {
        /*$('#Cliente_Text').val('');
        $('#Num_Op_Text').val('');
        $('#Tx_NroFac_Text').val('');
        $('#Tx_moneda_Text').val('');
        $('#Tx_neto_Text').val('');
        $('#Tx_iva_Text').val('');
        $('#Tx_MtoOri_Text').val('');
        $('#Tx_tipo_Text').val('');
        */
    });
    
    $(document).on("click", "#m_e tr", function (e) {

        var haceClickEnMoneda = $($(this).children()[0]).text() === "Moneda";
        var haceClickEnTotal = $($(this).children()[1]).text() === "---- Total ----";
        if (!haceClickEnMoneda && !haceClickEnTotal) {
            //if (!haceClickEnTotal) {
                monedaActual = $($(this).children()[0]).text();
                var nemonico = $($(this).children()[1]).text();
                var montoDebe = $($(this).children()[2]).text();
                var montoHaber = $($(this).children()[3]).text();
                var monto;

                if (parseFloat(montoDebe) > 0) {
                    monto = montoDebe;
                } else {
                    monto = montoHaber;
                }

                esMN = false;
                indiceLista = $(this).index();
                $.ajax({
                    url: baseUrl + "Home/m_e_Click/" + indiceLista,
                    success: function (data) {
                        ViewModelToView(data);
                        $("#monedas").val(data.monedas.Value);
                        //$("#nemonico").val(data.nemonico.Text);
                        //$("#monto").val(data.monto.Text);
                        if (data.tipo_000.Value == "-1") {
                            $("#tipo_000").prop("checked", true);
                            $("#tipo_001").prop("checked", false);
                        } else {
                            $("#tipo_000").prop("checked", false);
                            $("#tipo_001").prop("checked", true);
                        }

                        if (data.cambiar.Value == "1") {
                            $("#cambiar").prop("checked", true);
                        }
                        else {
                            $("#cambiar").prop("checked", false);
                        }

                        $("#cancelar").prop("disabled", false);
                        $("#monedas").focus();
                    },
                    error: function (response, type, message) {
                        try {
                            var responseJson = JSON.parse(response.responseText);
                            showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
                        }
                        catch (err) {
                            showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", true);
                        }
                    }
                });

            //} else {
            //    $("#cancelar").prop("disabled", true);
            //}
            var sacar = $(this).hasClass("blue-text");
            var rows = $(this).parent().children();
            rows.removeClass("blue-text");
            if (sacar) {
                $(this).removeClass("blue-text");
            } else {
                $(this).addClass("blue-text");
            }
        }
    
    });
    $(document).on("click", "#m_n tr", function (e) {
        var haceClickEnMoneda = $($(this).children()[0]).text() === "Moneda";
        var haceClickEnTotal = $($(this).children()[1]).text() === "---- Total ----";

        if (!haceClickEnMoneda && !haceClickEnTotal) {
           // if (!haceClickEnTotal) {
                monedaActual = $($(this).children()[0]).text();
                var nemonico = $($(this).children()[1]).text();
                var montoDebe = $($(this).children()[2]).text();
                var montoHaber = $($(this).children()[3]).text();
                var monto;

                if (parseFloat(montoDebe) > 0) {
                    monto = montoDebe;
                } else {
                    monto = montoHaber;
                }

                esMN = true;
                indiceLista = $(this).index();

                $.ajax({
                    url: baseUrl + "Home/m_n_Click/" + indiceLista,
                    success: function (data) {
                        ViewModelToView(data);
                        $("#monedas").val(data.monedas.Value);
                        //$("#nemonico").val(data.nemonico.Text);
                        //$("#monto").val(data.monto.Text);
                        if (data.tipo_000.Value == "-1") {
                            $("#tipo_000").prop("checked", true);
                            $("#tipo_001").prop("checked", false);
                        } else {
                            $("#tipo_000").prop("checked", false);
                            $("#tipo_001").prop("checked", true);
                        }

                        if (data.cambiar.Value == "1") {
                            $("#cambiar").prop("checked", true);
                        }
                        else {
                            $("#cambiar").prop("checked", false);
                        }

                        $("#cancelar").prop("disabled", false);
                    },
                    error: function (response, type, message) {
                        try {
                            var responseJson = JSON.parse(response.responseText);
                            showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
                        }
                        catch (err) {
                            showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", true);
                        }
                    }
                });
            //}
            //else {
            //    $("#cancelar").prop("disabled", true);
            //}
                var sacar = $(this).hasClass("blue-text");
                var rows = $(this).parent().children();
                rows.removeClass("blue-text");
                if (sacar) {
                    $(this).removeClass("blue-text");
                } else {
                    $(this).addClass("blue-text");
                }
        }
        
    });
    
    function ViewModelToView(viewModel) {

        var controlList = ["monto", "nemonico", "ver", "cambiar", "tipo_000", "tipo_001", "aceptar", "cancelar"];

        $.each(controlList, function (index, value) {
            var id = "#" + value;
            $(id).prop("disabled", !viewModel[value].Enabled);
            $(id).css("visibility", viewModel[value].Visible ? "":"hidden" );
            if (viewModel[value].Text) {
                $(id).val(viewModel[value].Text);
            }
            if (viewModel[value].Items) {
                $(id)
                    .empty()
                    .append($.map(viewModel[value].Items,function(item){
                        return $("<tr/>").data("item", item.Data).append($.map(item.columns, function (v) {
                            return $("<td/>").text(v);
                        }));
                    }));
            }
            $.each(viewModel.ListaErrores, function (i, elem) {
                if (viewModel.ListaErrores[i].ControlName == value) {
                    $(id).focus().select();
                }
            });
        });      

        if (viewModel.txtNumRef.Visible) {
            $("#refDiv").css("visibility", "visible");
        } else {
            $("#refDiv").css("visibility", "hidden");
        }
             
        $("#monedas").prop("disabled", false);
        loadMessages(viewModel.ListaErrores);
        focusOnErrorControl(viewModel.ListaErrores);
    }

    var marca = $("#marca");
    var montoElem = $("#monto");
    if (!montoElem.prop("disabled")) {
        var top = marca.position().top;
        window.scroll(0, top);
        if ($("#monedas").prop("selectedIndex") == 0) {
            $("#monedas").focus();
        } else if(montoElem.val()=="") {
            montoElem.focus();
        } else if ($("#nemonico").val()=="") {
            $("#nemonico").focus();
        } else {
            $("#cancelar").focus();
        }
    }

    $("#cancelar").prop("disabled", true);

    /// Manejo de indice al cargar la pagina.
    if ($("#monedas").is(":disabled")) {
        $('[tabindex="1"]').focus();
    }
    if ($("#tbr_participantes").is(":enabled")) {
        $('[tabindex="2"]').focus();
    }
    if ($("#monedas").is(":enabled")) {
        if($("#monedas").val() == "")
            $("[tabindex='3']").focus();
        else
            $("[tabindex='4']").focus();
    }

    $('#modal').on('shown.bs.modal', function () {
        $("#BtnModalConfirmar").focus();
    });

    $('#modal').on('hidden.bs.modal', function () {
        $("#monedas").focus();
    });

    $("#tbr_grabar").click(function () {
        // volvemos a comprobar ya que puede hacerse con click normal y no con enter.
        if ($("#tbr_grabar").attr("disabled") !== "disabled") {
            // deshabilitamos los botónes importantes una vez entra aqui.
            $("#tbr_grabar").attr("disabled", "disabled");
            $("#aceptar").attr("disabled", "disabled");
            // Mostramos mensaje de cargando y continuamos con el flujo normal.
            $.blockUI({ message: '<h6>Cargando...</h6>' });
        }
    });

    /// Cuando haga Enter en cualquier parte de la pantalla
    $(document).keydown(function (ev) {
        try {            
            var keycode = (ev.keyCode ? ev.keyCode : ev.which);            
            if (keycode == 9) {  // Presiona Tab
                if($(document.activeElement).is("#Tx_ReferenciaCliente"))
                    RecorrerTabIndex(2, ev);
                else
                    RecorrerTabIndex(-1, ev);
            }

            if (keycode == 13) {  // Presiona Enter 
                // Si botón no está deshabilitado y además tiene el foco en él.
                if ($("#tbr_grabar").attr("disabled") !== "disabled" && $("#tbr_grabar").is(":focus")) {
                    $("#tbr_grabar").click();
                }else if (($("#aceptar").is(":enabled") || $("#aceptar").is(":visible"))
                    && !$("#BtnModalConfirmar").is(":focus")
                    && !$('[tabindex="1"]').is(":focus")
                    && !$('[tabindex="2"]').is(":focus")) {
                    ev.preventDefault();
                    $('#aceptar').click();
                }
            }
        } catch (e) { console.log(e); }
    });

});

function mostrarConfirmacionesSiExisten() {
    var baseUrl = $("#base_url").val() + "ContabilidadGenerica/";

    if (modelListaConfirmaciones.length > 0) {
        showConfirmMessages(modelListaConfirmaciones, false, function (confirms) {
            var allTrue = true;
            for (var i = 0; i < confirms.length && allTrue; i++) {
                allTrue = allTrue && confirms[i];
            }
            if (allTrue) {
                window.location = baseUrl + "Home/Aceptar_ValidacionSaldo?saltarValidacionSaldo=" + allTrue;
            }

        });
    } 

}

function aplicaMascara(newValue) {
    if (newValue == 1) {
        $("#monto").inputmask({ mask: "9{1,12}", placeholder: '', onBeforePaste: function (pastedValue) { var pos = pastedValue.indexOf(","); if (pos >= 0) { showAlert("", "Número ingresado no es válido, se permiten hasta 12 enteros", "alert-info", false); return pastedValue.substring(0, pos); } else { return pastedValue } } });
    } else {
        var monto = $("#monto").val();
        $("#monto").inputmask({ alias: "numeric", placeholder: '', radixPoint: ',', groupseparator: '', greedy: false, rightAlign: false, digits: 2, integerDigits: 12 });
        $("#monto").val(monto);
    }
}