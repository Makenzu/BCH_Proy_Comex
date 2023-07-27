function pad(n, width, z) {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
}

$(document).ready(function () {
    var baseUrl = $("#base_url").val();
    $("#btnaceptar").prop("disabled", true);
    $(document).ajaxStart(function () { $.blockUI({ message: "<h6>Espere un momento ...</h6>" }); }).ajaxStop($.unblockUI);

    var ignoreList = ["aceptar", "cancelar", "btn_ok_click", "btn_aceptar_click", "btn_cancelar_click",
        "Cb_Producto_blur", "Tx_NumOpe_000_blur", "Tx_NumOpe_001_blur", "Tx_NumOpe_002_blur", "Tx_NumOpe_003_blur", "Tx_NumOpe_004_blur", "Tx_NumOpe_005_blur", "Tx_NumOpe_006_blur"];

    var viewModel = null;
    var RelacionarOperacion = $("#RelacionarOperacion");

    var updateModel = function (newViewModel) {
        ko.mapping.fromJS(newViewModel, viewModel);
        var errors = ko.mapping.toJS(viewModel.ListaErrores);
        if (errors.length > 0) {
            $("#Tx_NumOpe_004").focus().trigger('select');
            $("#btnaceptar").prop("disabled", true);
        }
        loadMessages(errors);        
    }

    var relacionarOperacionViewModel = function (data) {
        ko.mapping.fromJS(data, {}, this);
        var that = this;

        that.btn_ok_click = function () {
            var urlStr = baseUrl + "ContabilidadGenerica/RelacionarOperacion/FrmgAso_ok_Click";
            var $Cb_Producto = $("#Cb_Producto");
            var producto = $("#Tx_NumOpe_001").val();
            var value = $Cb_Producto.val();
            var indice = $Cb_Producto.prop('selectedIndex');
            if (value != producto)
                value = producto;
            viewModel.Cb_Producto.Value = ko.observable(value);
            viewModel.Cb_Producto.SelectedValue = ko.observable(value);
            viewModel.Cb_Producto.ListIndex = ko.observable(indice);

            $.ajax({
                method: "POST",
                url: urlStr,
                cache: false,
                data: {
                    jsonModel: ko.mapping.toJS(viewModel, {
                        ignore: ignoreList
                    })
                },
                success: function (data) {
                    updateModel(data);
                    $("#btnaceptar").prop("disabled", false);
                    $("#btnaceptar").focus();
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
        };

        that.btn_aceptar_click = function () {
            AjaxCall("ContabilidadGenerica/RelacionarOperacion/FrmgAso_Aceptar_Click", function () {
                window.location.href = baseUrl + "ContabilidadGenerica/Home";
            });
        };

        that.btn_cancelar_click = function () {
            $.ajax({
                method: "GET",
                url: baseUrl + "ContabilidadGenerica/RelacionarOperacion/FrmgAso_Cancelar_Click",
                success: function () {
                    var nodo = RelacionarOperacion.get(0);
                    ko.cleanNode(nodo);
                    viewModel = null;
                    window.location.href = baseUrl + "ContabilidadGenerica/Home";
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
        };

        that.Cb_Producto_blur = function () {
            var $Cb_Producto = $("#Cb_Producto");
            var value = $Cb_Producto.val();
            var indice = $Cb_Producto.prop('selectedIndex');
            viewModel.Cb_Producto.Value(value);
            viewModel.Cb_Producto.SelectedValue(value);
            viewModel.Cb_Producto.ListIndex(indice);
            AjaxCall("ContabilidadGenerica/RelacionarOperacion/FrmgAso_Cb_Producto_Click", function () {
                $Cb_Producto.focus();
            });
        };

        //extender para poder indicar nro de decimales
        ko.extenders.padConCeros = function (target, longitud) {

            var result = ko.computed({
                read: function () {
                    return pad(target(), longitud);
                },
                write: function (newValue) {
                    var valueToWrite = pad(newValue, longitud);
                    if (valueToWrite !== target()) { // si el valor transformado es diferente, lo escribo
                        target(valueToWrite);
                    } else { //si es igual, pero el nuevo valor es diferente al del viewmodel no escribo, pero notifico un cambio para actualizar el textbox
                        if (newValue !== target()) {
                            target.notifySubscribers(valueToWrite);
                        }
                    }
                }
            });

            //result.raw = target;
            result(target());
            return result;
        };
    }

    function AjaxCall(url, func) {
        // viewModel.Errores([]);
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
            }
        });
    }

    var urlStr = baseUrl + "ContabilidadGenerica/RelacionarOperacion/FrmgAso_LoadFrm";
    $.ajax({
        method: "GET",
        url: urlStr,
        cache: false,
        success: function (data) {
            var nodo = RelacionarOperacion.get(0);
            ko.cleanNode(nodo);
            viewModel = new relacionarOperacionViewModel(data);
            ko.applyBindings(viewModel, nodo);
            $(":input").inputmask();
            $("#Cb_Producto").focus().select();

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

    // Cuando haga Enter en cualquier parte de la pantalla
    $(document).keydown(function (ev) {
        try {
            var keycode = (ev.keyCode ? ev.keyCode : ev.which);
            if (keycode == 9) {  // Presiona Tab
                RecorrerTabIndex(-1, ev);
            }

            if (keycode == 13) {  // Presiona Enter                
                if ($("#btnaceptar").is(":enabled")) {
                    $("#btnaceptar").click();
                } else {
                    $("#btn_ok").click();
                }
            }
        } catch (e) { console.log(e); }
    });
});
