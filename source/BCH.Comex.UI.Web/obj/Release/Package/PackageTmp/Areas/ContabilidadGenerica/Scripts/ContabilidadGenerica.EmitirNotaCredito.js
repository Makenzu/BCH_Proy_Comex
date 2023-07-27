$(document).ready(function () {
    var baseUrl = $("#base_url").val();

    var ignoreList = ["aceptar", "cancelar", "btn_ok_click", "btn_aceptar_click", "btn_cancelar_click"];

    var viewModel = null;

    var EmitirNotaCredito = $("#EmitirNotaCredito");

    var updateModel = function (newViewModel) {
        ko.mapping.fromJS(newViewModel, viewModel);
        var errors = ko.mapping.toJS(newViewModel.ListaErrores);
        loadMessages(errors);
    }

    var EmitirNotaCreditoViewModel = function (data) {
        ko.mapping.fromJS(data, {}, this);

        var that = this;
        that.btn_ok_click = function () {
            AjaxCall("ContabilidadGenerica/EmitirNotaCredito/FrmgAsoNC_ok_Click", function () {
                //funciono todo OK, actualizo la tabla de facturas
                var resultadoLlamado = ko.toJS(viewModel.ListaFacturas());
                cargarTablaBoostrap(resultadoLlamado);
                if (ko.toJS(viewModel.Tx_RutPrt.Text) != "" && resultadoLlamado.length > 0) {
                    $('#btnAceptar').removeAttr("disabled", "disabled");
                }
            });
        };
        that.btn_aceptar_click = function () {
            var resultadoLlamado = ko.toJS(viewModel.ListaFacturas());
            if (ko.toJS(viewModel.Tx_RutPrt.Text) != "" && resultadoLlamado.length > 0) {
                var elements = $('#tableDetalle').bootstrapTable('getSelections');
                if (elements.length > 0) {
                    $.ajax({
                        method: "POST",
                        url: baseUrl + "ContabilidadGenerica/EmitirNotaCredito/FrmgAsoNC_Aceptar_Click",
                        data: {
                            jsonModel: ko.mapping.toJS(viewModel, {
                                ignore: ignoreList
                            }),
                            jsonFactura: ko.mapping.toJS(elements[0])
                        },
                        success: function (a) {
                            //Habilita Objetos del formulario:
                            $("#monedas").removeAttr('disabled');
                            window.location.href = baseUrl + "ContabilidadGenerica/Home";
                        }
                    });
                } else {
                    showAlert("Error en la operación.", "Detalles: Debe seleccionar una factura asociada a la operación ", "alert-danger", true);
                }
            } else {
                showAlert("Error en la operación.", "Detalles: Debe consultar una operación ", "alert-danger", true);
            }
           
        };
        that.btn_cancelar_click = function () {
                limpiar_camposForm();
                limpiarTablas();
                window.location.href =  baseUrl + "ContabilidadGenerica/Home";
        };
        that.Cb_Producto_blur = function () {
            AjaxCall("ContabilidadGenerica/mitirNotaCredito/FrmgAsoNC_Cb_Producto_Click");
        };
        //extender para poder indicar nro de decimales
        ko.extenders.padConCeros = function (target, longitud) {
            var result = ko.computed({
                read: function () {
                    return pad(target(), longitud);
                },
                write: target
            });

            result.raw = target;
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

    $.ajax({
        method: "GET",
        cache: false,
        url: baseUrl + "ContabilidadGenerica/EmitirNotaCredito/FrmgAsoNC_LoadFrm",
        success: function (data) {
            var nodo = EmitirNotaCredito.get(0);
            ko.cleanNode(nodo);
            viewModel = new EmitirNotaCreditoViewModel(data);
            ko.applyBindings(viewModel, nodo);
            cargarTablaBoostrap(ko.toJS(viewModel.ListaFacturas()));
            limpiarTablas();
            $(":input").inputmask();
        }
    });
});

function cargarTablaBoostrap(datos) {
    $('#badge').text('0');
    $('#tableDetalle').bootstrapTable('destroy');
    if (datos.length > 0) {
        $('#badge').text(datos.length);
        $('#tableDetalle').bootstrapTable({
            classes: 'table table-hover resultRow',
            columns: [
                { radio: true, title: '#' },
                { field: 'NroFactura', title: 'Nro. Factura', sortable: false, align: 'left', width: '100px' },
                { field: 'NroReporte', title: 'Nro. Reporte', sortable: false, align: 'center', width: '80px' },
                { field: 'Tipo', title: 'Tipo Fac.', sortable: false, align: 'center', width: '100px' },
                { field: 'FechaFactura', title: 'Fecha Fac.', sortable: false, align: 'right', width: '80px' },
                { field: 'Moneda', title: '$ Moneda', sortable: false, align: 'center', width: '100px' },
                { field: 'Neto', title: '$ Neto', sortable: false, align: 'right', width: '120px', formatter: montoFormatter },
                { field: 'Iva', title: '$ Iva', sortable: false, align: 'right', width: '120px', formatter: montoFormatter },
                { field: 'Total', title: '$ Total', sortable: false, align: 'right', width: '150px', formatter: montoFormatter }
            ],
            data: datos,
            pagination: false,
            locale: 'es-CL',
            showHeader: true,
            clickToSelect: true,
            singleSelect: true
        });
    }
}

function pad(n, width, z) {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
}

function montoFormatter(value, row, index) {
    if (row['Moneda'] == 'Peso Chileno') {
        return numeral(value).format("0,0");
    }
    else {
        return numeral(value).format("0,0.00");
    }
}

function limpiarTablas() {
    $('#tableDetalle').bootstrapTable('removeAll');
    $('#badge').text("0");
}

function limpiar_camposForm() {
    $('#Tx_NumOpe_000').val('');
    $('#Tx_NumOpe_001').val('');
    $('#Tx_NumOpe_002').val('');
    $('#Tx_NumOpe_003').val('');
    $('#Tx_NumOpe_004').val('');
    $('#Tx_NumOpe_005').val('');
    $('#Tx_NumOpe_006').val('');
    $('#Tx_RutPrt').val('');
    $('#Tx_NomPrt').val('');
    $('#Tx_DirPrt').val('');
}

