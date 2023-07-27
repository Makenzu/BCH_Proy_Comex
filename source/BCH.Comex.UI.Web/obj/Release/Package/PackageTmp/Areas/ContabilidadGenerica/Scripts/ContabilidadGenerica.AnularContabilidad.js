$(function () {
    var baseUrl = $("#base_url").val();
    $('#btnAceptar').prop('disabled', true);
    $("#Tx_NroRep").focus();

    $(document).ajaxStart(function () {
        $.blockUI({ message: "<h6>Espere un momento ...</h6>" });
    }).ajaxStop($.unblockUI);

    var ignoreList = ["aceptar", "cancelar", "btn_ok_click", "btn_aceptar_click", "btn_cancelar_click"];
    var viewModel = null;
    var AnularContabilidad = $("#AnularContabilidad");
  
    $('#tableDetalle').bootstrapTable({
        classes: 'table table-hover resultRow',
        columns: [
            { field: 'Cuenta', title: 'Cuenta', sortable: false, align: 'left', width: '100px' },
            { field: 'Moneda', title: 'Moneda', sortable: false, align: 'center', width: '100px' },
            { field: 'Debe', title: 'Debe', sortable: false, align: 'right', width: '120px', formatter: montoFormatter },
            { field: 'Haber', title: 'Haber', sortable: false, align: 'right', width: '120px', formatter: montoFormatter }
        ],
        pagination: false,
        locale: 'es-CL',
        showHeader: true,
        clickToSelect: false,
        search: true,
        showRefresh: true
    });

    var updateModel = function (newViewModel) {
        ko.mapping.fromJS(newViewModel, viewModel);
        var errors = ko.mapping.toJS(newViewModel.ListaErrores);
        loadMessages(errors);
        focusOnErrorControl(errors);
    }
    var AnularContabilidadViewModel = function (data) {
        ko.mapping.fromJS(data, {}, this);
        var that = this;
        that.btn_ok_click = function () {
            viewModel.Tx_NroRep.Text($('#Tx_NroRep').val());
            viewModel.Tx_FecRep.Text($('#Tx_FecRep').val());
            AjaxCall("ContabilidadGenerica/AnularContabilidad/FrmGlanu_OkClick", function () {
                    var resultadoLlamado = ko.toJS(viewModel.ListaDatos());
                    resultadoLlamado = resultadoLlamado == "" || resultadoLlamado == null ? null : resultadoLlamado;
                    if (resultadoLlamado != null) {
                        $('#Tx_Cliente').val(viewModel.Tx_Cliente.Text());
                        $('#Tx_NroRep').val(viewModel.Tx_NroRep.Text());
                        $('#Tx_FecRep').val(viewModel.Tx_FecRep.Text());
                        cargarTablaBoostrap(resultadoLlamado);
                    } else {
                        cargarTablaBoostrap([]);
                    }
            });
        };
        that.btn_aceptar_click = function () {
            if ($('#Tx_NroRep').val() != "") {
                if (confirm("¿ Desea anular este Reporte Contable ?")) {
                    $.ajax({
                        method: "POST",
                        url: baseUrl + "ContabilidadGenerica/AnularContabilidad/FrmGlanu_AceptarClick",
                        data: {
                            jsonModel: ko.mapping.toJS(viewModel, {
                                ignore: ignoreList
                            })
                        },
                        success: function (model) {
                            if (model.RptContable && model.RptContable.length > 0) {
                                window.location.href = urlImpresion;
                            }
                        },
                        error: function (err) {
                            showAlert("Error en la operación.", "Detalles: " + err, "alert-danger", true);
                        }
                    });
                }
            } else {
                showAlert("Error en la operación.", "No fue posible actualizar el Nro. Reporte Contable", "alert-danger", true);
            }
        };
        that.btn_cancelar_click = function () {
            
            $.ajax({
                method: "GET",
                url: baseUrl + "ContabilidadGenerica/AnularContabilidad/FrmGlanu_CancelarClick",
                success: function (data) {
                    window.location.href = baseUrl + "ContabilidadGenerica/Home";
                },
                error: function (err) {
                    showAlert("Error en la operación.", "Detalles: " + err, "alert-danger", true);
                }
            });
        };

    }

    function AjaxCall(url, func) {
        //viewModel.Errores([]);
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

    $.ajax({
        method: "GET",
        cache: false,
        url: baseUrl + "ContabilidadGenerica/AnularContabilidad/FrmGlanu_Init",
        success: function (data) {
            var nodo = AnularContabilidad.get(0);
           // ko.cleanNode(nodo);
            viewModel = new AnularContabilidadViewModel(data);
            ko.applyBindings(viewModel, nodo);
            if (viewModel.ListaDatos() != null) 
                if(viewModel.ListaDatos()[viewModel.ListaDatos().length - 1] != null)
                    cargarTablaBoostrap(ko.toJS(viewModel.ListaDatos()));
            $("#Tx_FecRep").val($('#dtpFechaOperacion').data("DateTimePicker").date().format("DD/MM/YYYY"));
            $(":input").inputmask();
        }
    });

    var dateNow = moment().startOf("day").utc();
    $('#dtpFechaOperacion').datetimepicker({ format: 'DD/MM/YYYY', locale: 'es', focusOnShow: false, defaultDate: dateNow, maxDate: dateNow, debug: true });

    // Cuando haga Enter en cualquier parte de la pantalla
    $(document).keydown(function (ev) {
        try {
            var keycode = (ev.keyCode ? ev.keyCode : ev.which);
            if (keycode == 9) {  // Presiona Tab
                RecorrerTabIndex(-1, ev);
            }

            if (keycode == 13) {  // Presiona Enter
                ev.preventDefault();
                if ($("#btnAceptar").is(":enabled")) {
                    $('#btnAceptar').click();
                } else{
                    $("#btn_ok").click();
                }
            }
        } catch (e) { console.log(e); }
    });
    $("#Tx_FecRep").keydown(function (ev) {
        try {
            var keycode = (ev.keyCode ? ev.keyCode : ev.which);
            if (keycode == 13) {  // Presiona Enter
                ev.preventDefault();
                if ($("#btnAceptar").is(":enabled")) {
                    $('#btnAceptar').click();
                } else {

                    $("#btn_ok").click();
                }
            }
        } catch (e) { console.log(e); }
    });
    
});

function cargarTablaBoostrap(datos) {
        
        $('#tableDetalle').bootstrapTable('destroy');
        if (datos && datos.length > 0) {
            $('#btnAceptar').removeAttr('disabled');
            $('#tableDetalle').bootstrapTable({
                classes: 'table table-hover resultRow',
                columns: [
                    { field: 'Cuenta', title: 'Cuenta', sortable: false, align: 'left', width: '100px' },
                    { field: 'Moneda', title: 'Moneda', sortable: false, align: 'center', width: '100px' },
                    { field: 'Debe', title: 'Debe', sortable: false, align: 'right', width: '120px', formatter: montoFormatter },
                    { field: 'Haber', title: 'Haber', sortable: false, align: 'right', width: '120px', formatter: montoFormatter }
                ],
                data: datos,
                pagination: false,
                locale: 'es-CL',
                showHeader: true,
                clickToSelect: false
            });
        }
        else {
            $('#btnAceptar').attr('disabled', 'disabled');
        }
}

function montoFormatter(value, row, index) {
    if (row['Moneda'] == 'Peso Chileno') {
        return numeral(value).format("0,0");
    }
    else {
        return numeral(value).format("0,0.00");
    }
}