$(document).ready(function () {
    
    var baseUrl = $("#base_url").val() + "ContabilidadGenerica/"; //obtengo la url base global
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI).ajaxError($.unblockUI);

    var ignoreList = [];
    var viewModelFunctions = function () {
        var that = {};
        that.moneda_change = function () {
            var index = $("#moneda").get(0).selectedIndex - 1;
            viewModel.Cb_Mnd.ListIndex(index);
        };
        that.cuentas_change = function () {
            var index = $("#cuentas").get(0).selectedIndex - 1;
            viewModel.L_Cuentas.ListIndex(index);
        };
        that.tx_datos_blur = function (ind) {
            return function () {
                AjaxCall("DatosAdicionales/Tx_Datos_Blur/" + ind);
            };
        };
        that.tx_fecha_vencimiento_blur = function () {
            viewModel.Tx_Datos()[3].Text($("#txtFechaVencimiento").val());
        };
        that.aceptar = function () {
            $.blockUI({ message: '<h6>Cargando...</h6>' });
            AjaxCall("DatosAdicionales/Boton_Click/0", function () {
                if (viewModel.ListaConfirmaciones.length == 0) {
                    window.location = baseUrl + "DatosAdicionales/Fin";
                    $.blockUI({ message: '<h6>Cargando...</h6>' });
                    $('#aceptar').prop("disabled", true);
                    $('#cancelar').prop("disabled", true);
                } else {
                    showConfirmMessages(ko.mapping.toJS(viewModel.ListaConfirmaciones), false, function (confirms) {
                        var allTrue = true;
                        for (var i = 0; i < confirms.length && allTrue; i++) {
                            allTrue = allTrue && confirms[i];
                        }
                        viewModel.ListaConfirmaciones([]);
                        AjaxCall("DatosAdicionales/Boton_Click/0?vieneDeMsg=true&resMsg=" + allTrue, function () {
                            if (allTrue) {
                                window.location = baseUrl + "DatosAdicionales/Fin";
                                $.blockUI({ message: '<h6>Cargando...</h6>' });
                                $('#aceptar').prop("disabled", true);
                                $('#cancelar').prop("disabled", true);
                            }
                        });
                    });
                }
            });
        };
        that.cancelar = function () {
            AjaxCall("DatosAdicionales/Boton_Click/1", function () {
                window.location = baseUrl;
            });
        };
        that.tx_datos_keypress = function (viewModel, event) {
            var key = String.fromCharCode(event.charCode || event.keyCode);
            if (EsCaracterEspecial(key)) {
                event.preventDefault();
                return false;
            } else {
                return true;
            }
        };

        return that;
    };
    var adicionalesViewModel = function (data) {
        ko.mapping.fromJS(data, {}, this);
        var that = this;
        var functions = viewModelFunctions();
        $.each(functions, function (name) {
            ignoreList.push(name);
        });
        that = $.extend(that, that, functions);
    };
    var updateModel = function (newViewModel) {
        ko.mapping.fromJS(newViewModel, viewModel);
        //obtengo los errores y los muestro
        loadMessages(newViewModel.ListaErrores);
        focusOnErrorControl(newViewModel.ListaErrores);
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
        url: baseUrl + "DatosAdicionales/GetForm",
        success: function (data) {
            viewModel = new adicionalesViewModel(data);
            ko.applyBindings(viewModel);
            IniPage();
        }
    });

    //engancho el evento de toUpperCase a los controles que lo requieran
    $(".upperCaseField").change(function () { toUppercase(this) }).change();

    // Iniciar TAB
    function IniPage() {
        if (!$("[tabindex='2']").is(":visible")) {
            $("#cuentas :first").attr("selected", "selected");
            $("#cuentas").focus().trigger('select');
            $('#cuentas').val($('#cuentas :selected').val());
            $('#cuentas').val($('#cuentas :selected').val());
        } else {
            $("[tabindex='2']").focus().trigger('select');
        }
    }

    // Cuando haga Enter en cualquier parte de la pantalla
    $(document).keydown(function (ev) {
        try {
            var keycode = (ev.keyCode ? ev.keyCode : ev.which);
            if (keycode == 9) {  // Presiona Tab
                RecorrerTabIndex(-1, ev);
            }

            if (keycode == 13) {  // Presiona Enter                
                if (($("#aceptar").is(":enabled") || $("#aceptar").is(":visible"))) {
                    ev.preventDefault();
                    $('#aceptar').click();
                }
            }
        } catch (e) { console.log(e); }
    });

    inicializarFiltrosDeFecha();

});

// inicializo los controles de fecha
function inicializarFiltrosDeFecha() {
    var initialDate = moment(new Date()).utc().startOf('day');
    var fechaMin = moment(new Date()).utc().startOf('day');

    var dateNow = moment().startOf("day").utc();
    $('#dtpFechaVencimiento').datetimepicker({
        format: 'DD/MM/YYYY',
        locale: 'es',
        focusOnShow: true,
        defaultDate: initialDate,
        minDate: fechaMin,
        debug: true,
        daysOfWeekDisabled: [0, 6],
        keyBinds: {
            enter: function (ev) {
                this.hide();
                $('#txtFechaVencimiento').change();
                if ($("#aceptar").is(":enabled")) {
                    viewModel.aceptar();
                }
            }
        }
    });

    $("#dtpFechaVencimiento").on("dp.change", function (e) {
        if (!e.date) {
            $(this).data("DateTimePicker").date(moment(new Date()).utc().startOf('day'))
            viewModel.Tx_Datos()[3].Text($("#txtFechaVencimiento").val());
        }
    });
}