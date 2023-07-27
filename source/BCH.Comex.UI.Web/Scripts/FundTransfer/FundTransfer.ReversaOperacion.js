$(function () {
    var baseUrl = $("#base_url").val(); //obtengo la url base global
    //activo el bloquear la UI en requests asincronos
    $(document).ajaxStart(function () {
        $.blockUI({ message: '<h6>Cargando...</h6>' })
    }).ajaxStop($.unblockUI);

    $("#Tx_NroOpe_000").change(function () {
        var val = $("#Tx_NroOpe_000").val();
        $.ajax({
            url: baseUrl + "FundTransfer/Tx_NroOpe_Blur",
            data: { id: 0, text: val },
            method: "POST",
            success: function (data) {
                ReversarOperacionExportViewModelToView(data);                
            }
        });
    });
    $("#Tx_NroOpe_001").change(function () {
        var val = $("#Tx_NroOpe_001").val();
        $.ajax({
            url: baseUrl + "FundTransfer/Tx_NroOpe_Blur",
            data: { id: 1, text: val },
            method: "POST",
            success: function (data) {
                ReversarOperacionExportViewModelToView(data);
            }
        });
    });
    $("#Tx_NroOpe_002").change(function () {
        var val = $("#Tx_NroOpe_002").val();
        $.ajax({
            url: baseUrl + "FundTransfer/Tx_NroOpe_Blur",
            data: { id: 2, text: val },
            method: "POST",
            success: function (data) {
                ReversarOperacionExportViewModelToView(data);
            }
        });
    });
    $("#Tx_NroOpe_003").change(function () {
        var val = $("#Tx_NroOpe_003").val();
        $.ajax({
            url: baseUrl + "FundTransfer/Tx_NroOpe_Blur",
            data: { id: 3, text: val },
            method: "POST",
            success: function (data) {
                ReversarOperacionExportViewModelToView(data);
            }
        });
    });
    $("#Tx_NroOpe_004").change(function () {
        var val = $("#Tx_NroOpe_004").val();
        $.ajax({
            url: baseUrl + "FundTransfer/Tx_NroOpe_Blur",
            data: { id: 4, text: val },
            method: "POST",
            success: function (data) {
                ReversarOperacionExportViewModelToView(data);
            }
        });
    });

    var viewModelToView = function(viewModel){
        $.blockUI({ message: '<h6>Cargando...</h6>' })
        function mostrarMensajesDeError(viewModel) {
            var msgErr = viewModel.MensajesDeErrores;
            msgErr = $.map(msgErr, function (elem, index) {
                return { Type: 3, Text: elem.Text, Title: "Error de Validación: " };
            });
            loadMessages(msgErr);
            focusOnErrorControl(viewModel.MensajesDeErrores);
        };
        mostrarMensajesDeError(viewModel);

        if (viewModel.MensajesDeErrores.length == 0 && viewModel.MensajesDeConfirmacion.length == 0 ) {
            //Habilitar Boton Aceptar:
            if (viewModel.TipoAnuls === 'TRN') {
                $('#Tx_Observacion_Text').val('');
                $('#btnAceptar').removeAttr('disabled');
            }
            else if (viewModel.TipoAnuls === 'VIS') {
                $('#Tx_Observacion_Text').val('');
                $('#btnAceptar').removeAttr('disabled');
            }
            else if (viewModel.TipoAnuls === 'INV') {
                $('#Tx_Observacion_Text').val('');
                $("#Numero_Text").val('');
                $("#Fecha_Text").val('');
                $("#Motivo_Text").val('');
                $("#Tx_TipCam_Text").val('');
                $('#btnAceptar').removeAttr('disabled');
                $("#Cb_Tipo_SelectedValue").val('');
                $("#Cb_SucursalBCCH_SelectedValue").val('');
                $("#Cb_TipAnu_SelectedValue").val('');
           }
        }
        $.unblockUI();
    };

    //Click En la lista de Planilla Export.
    $('#CmbPlanilla_SelectedValue').click(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/Reversar_Operacion_Export_Lt_Pln_Click",
            method: "Post",
            data: { selectedValue: $('#CmbPlanilla_SelectedValue').find(":selected").index() },
            success: function (data) {
                ReversarOperacionExportViewModelToView(data);
            },
            complete: function () {}
        });
    });
    $('#BtnDeclaracion').click(function (item) {
        $.ajax({
            url: baseUrl + "FundTransfer/ReversarOperacionExportBtnDeclaracion_Click",
            method: "POST",
            data: { },
            success: function (data) {
                ReversarOperacionDeclaracionViewModelToView(data);

                if (data.Redireccionar) {
                    window.location = data.Redireccionar;
                }
                AbrirModuloDeclaracion(true);
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });
    $('#ReversarOperacionDeclaracion').on('show.bs.modal', function (event) {
        $.ajax({
            url: baseUrl + "FundTransfer/ReversarOperacionDeclaracion",
            method: "GET",
            cache: false,
            success: function (data) {
                ReversarOperacionDeclaracionViewModelToView(data);
            }
        });
    })
    $('#BtnObservacion').click(function (item) {
        $('#divAutorizacion').addClass('hidden');
        $('#divObservacion').removeClass('hidden');

        var planillaVal = $('#CmbPlanilla_SelectedValue').val();
        var tipoAnuls = $('#TipoAnuls').val();

        if (tipoAnuls === 'VIS') {
            $('#txtObservaciones').text('Observaciones de Planilla Visible');//setear caption
        } else if (tipoAnuls === 'INV') {
            $('#txtObservaciones').text('Observaciones de Planilla Invisible');
            $('#Tx_Observacion_Text').first().focus();
        }
        //Inabilitar Frm_Frame3D1
        $('#Tx_NroOpe_000').attr('disabled', 'disabled');
        $('#Tx_NroOpe_001').attr('disabled', 'disabled');
        $('#Tx_NroOpe_002').attr('disabled', 'disabled');
        $('#Tx_NroOpe_003').attr('disabled', 'disabled');
        $('#Tx_NroOpe_004').attr('disabled', 'disabled');
        $('#-Operacion').attr('disabled', 'disabled');
        $('#Tx_Cliente').attr('disabled', 'disabled');

        //Inabilitar Frm_Frame3D2
        $('#CmbPlanilla.SelectedValue').attr('disabled', 'disabled');
        $('#Cb_TipAnu_SelectedValue').attr('disabled', 'disabled');
        $('#CamTipCam_Text').attr('disabled', 'disabled');
        $('#BtnDeclaracion').attr('disabled', 'disabled');
        $('#BtnObservacion').attr('disabled', 'disabled');
        $('#BtnOk').attr('disabled', 'disabled');

        $('#BtnAceptar').attr('disabled', 'disabled');
        $('#BtnCancelar').attr('disabled', 'disabled');
    });
    $('#btnOk').click(function (item) {
        $.ajax({
            url: baseUrl + "FundTransfer/ReversarOperacionExportBtn_ok_Click_1",
            method: "POST",
            data: {
                _NumeroAutorizacion: $('#Numero_Text').val(),
                _FechaAutorizacion: $('#Fecha_Text').val(),
                _MotivoAutorizacion: $('#Motivo_Text').val(),
                _tipoAutorizacion: $("#Cb_Tipo_SelectedValue").find(":selected").index()-1 != null ? $("#Cb_Tipo_SelectedValue").find(":selected").index()-1 : 0,
                _TipCamAutorizacion: $('#Tx_TipCam_Text').val(),
                _SucursalBCCH_Autorizacion: $("#Cb_SucursalBCCH_SelectedValue").find(":selected").index()-1,
                _TipoPlanilla: $("#CmbPlanilla_SelectedValue").find(":selected").index(),
                _TipoAnulacion: $("#Cb_TipAnu_SelectedValue").find(":selected").index(),
                _ObservacionPln: $('#Tx_Observacion_Text').val(),
                _vieneDeMensaje: false,
                _resMensaje: false
            },
            success: function (a) {
                if (a.MensajesDeErrores.length == 0) {
                    if (a.MensajesDeConfirmacion.length > 0) {
                        $.unblockUI();
                        showConfirmMessages(a.MensajesDeConfirmacion, false, function (confirms) {
                            var allTrue = true;
                            for (var i = 0; i < confirms.length && allTrue; i++) {
                                allTrue = allTrue && confirms[i];
                            }
                            $.ajax({
                                url: baseUrl + "FundTransfer/ReversarOperacionExportBtn_ok_Click_1",
                                method: "POST",
                                data: {
                                    _vieneDeMensaje: allTrue,
                                    _resMensaje: allTrue,
                                    _NumeroAutorizacion: $('#Numero_Text').val(),
                                    _FechaAutorizacion: $('#Fecha_Text').val(),
                                    _MotivoAutorizacion: $('#Motivo_Text').val(),
                                    _tipoAutorizacion: $("#Cb_Tipo_SelectedValue").val(),
                                    _TipCamAutorizacion: $('#Tx_TipCam_Text').val(),
                                    _SucursalBCCH_Autorizacion: $("#Cb_SucursalBCCH_SelectedValue").find(":selected").index(),
                                    _TipoPlanilla: $("#CmbPlanilla_SelectedValue").find(":selected").index(),
                                    _TipoAnulacion: $("#Cb_TipAnu_SelectedValue").find(":selected").index()
                                },
                                success: function (a) {
                                    viewModelToView(a);
                                }
                            })
                        });
                    } else {
                        viewModelToView(a);
                        MapComboBox($('#CmbPlanilla_SelectedValue'), a.CmbPlanilla);
                    }
                } else {
                    viewModelToView(a);
                    MapComboBox($('#CmbPlanilla_SelectedValue'), a.CmbPlanilla);
                }
            }
        });
    });
    $('#BtnVolver').click(function (item) {
        //Desaparece formulario Observaciones
        $('#divObservacion').addClass('hidden');
        $('#divAutorizacion').removeClass('hidden');

        //Habilitar
        $('#Tx_NroOpe_000').removeAttr('disabled');
        $('#Tx_NroOpe_001').removeAttr('disabled');
        $('#Tx_NroOpe_002').removeAttr('disabled');
        $('#Tx_NroOpe_003').removeAttr('disabled');
        $('#Tx_NroOpe_004').removeAttr('disabled');
        $('#Tx_Cliente').removeAttr('disabled');

        $('#CmbPlanilla.SelectedValue').removeAttr('disabled');
        $('#Cb_TipAnu.SelectedValue').removeAttr('disabled');
        $('#CamTipCam_Text').removeAttr('disabled');
        $('#BtnDeclaracion').removeAttr('disabled');
        $('#BtnObservacion').removeAttr('disabled');
        $('#BtnOk').removeAttr('disabled');
        $('#BtnAceptar').removeAttr('disabled');
        $('#BtnCancelar').removeAttr('disabled');

        var planillaVal = $('#CmbPlanilla_SelectedValue').val();
        var tipoAnuls = $('#TipoAnuls').val();

        if (tipoAnuls === 'VIS' || tipoAnuls === 'TRN') {
            //Habilita Campos Frm_Invisible.
            $("#Numero_Text").removeAttr('disabled');
            $("#Fecha_Text").removeAttr('disabled');
            $("#Cb_Tipo_SelectedValue").removeAttr('disabled');
            $("#Cb_SucursalBCCH_SelectedValue").removeAttr('disabled');
            $("#Motivo_Text").removeAttr('disabled');
            $("#Tx_TipCam_Text").removeAttr('disabled');

        } else if (tipoAnuls === 'INV') {
            //Habilita Campos Frm_Invisible.
            $("#Numero_Text").removeAttr('disabled');
            $("#Fecha_Text").removeAttr('disabled');
            $("#Cb_Tipo_SelectedValue").removeAttr('disabled');
            $("#Cb_SucursalBCCH_SelectedValue").removeAttr('disabled');
            $("#Motivo_Text").removeAttr('disabled');
            $("#Tx_TipCam_Text").removeAttr('disabled');

        }
        else if (tipoAnuls === 'OBS') {
            //InHabilitar Campos Frm_Invisible
            $("#Numero_Text").attr('disabled', 'disabled');
            $("#Fecha_Text").attr('disabled', 'disabled');
            $("#Cb_Tipo_SelectedValue").attr('disabled', 'disabled');
            $("#Cb_SucursalBCCH_SelectedValue").attr('disabled', 'disabled');
            $("#Motivo_Text").attr('disabled', 'disabled');
            $("#Tx_TipCam_Text").attr('disabled', 'disabled');
            //Inhabilitar Frame3D1
            $('#Tx_NroOpe_000').attr('disabled', 'disabled');
            $('#Tx_NroOpe_001').attr('disabled', 'disabled');
            $('#Tx_NroOpe_002').attr('disabled', 'disabled');
            $('#Tx_NroOpe_003').attr('disabled', 'disabled');
            $('#Tx_NroOpe_004').attr('disabled', 'disabled');
            $('#Tx_Cliente').attr('disabled', 'disabled');
        }
    });
    $('#btnAceptarReversaOperacionDeclaracion').click(function (item) {
        $("#ReversarOperacionDeclaracion").modal('hide');
        $.ajax({
            url: baseUrl + "FundTransfer/ReversarOperacionExportDeclaracion_Aceptar_Click",
            method: "POST",
            data: { VClausula: $('#txt_VClausula').val(), Comisiones: $('#txt_Comisiones').val(), CAM_Otros: $('#txt_CAM_Otros').val(), CAM_Liquido: $('#txt_CAM_Liquido').val() },
            success: function (data) {
                if (data.MensajesDeErrores.length > 0) {
                    _viewModelDeclaracionToView(data);
                }
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });

    var _viewModelDeclaracionToView = function (viewModel) {

        function mostrarMensajesDeError(viewModel) {
            var msgErr = viewModel.MensajesDeErrores;
            msgErr = $.map(msgErr, function (elem, index) {
                return { Type: 3, Text: elem.Text, Title: "Error de Validación: " };
            });
            loadMessages(msgErr);
            focusOnErrorControl(viewModel.MensajesDeErrores);
        };
        mostrarMensajesDeError(viewModel);
    };
    $("#CamTipCam_Text").blur(function () {
        var val = $("#CamTipCam_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/Tx_CAM_Tipcam_Blur",
            data: { text: val },
            method: "POST",
            success: function (data) {
                ReversarOperacionExportViewModelToView(data);
            }
        });
    });
    $("#Tx_TipCam_Text").blur(function () {
        var val = $("#Tx_TipCam_Text").val();
        $.ajax({
            url: baseUrl + "FundTransfer/Tx_TipCam_Blur",
            data: { text: val },
            method: "POST",
            success: function (data) {
                $("#Tx_TipCam_Text").val(data.Tx_TipCam.Text);
            }
        });
    });
   
    var viewModelDeclaracionToView = function (model) {
        
        //Mapeo controles
        FundTransfer.Common.MapTextBox($('#txt_VClausula'), model.CAM_Clausula);
        FundTransfer.Common.MapTextBox($('#txt_Comisiones'), model.CAM_Comisiones);
        FundTransfer.Common.MapTextBox($('#txt_CAM_Otros'), model.CAM_Otros);
        FundTransfer.Common.MapTextBox($('#txt_CAM_Liquido'), model.CAM_Liquido);

    };
    $("#txt_VClausula").blur(function () {
        var val = $("#txt_VClausula").val();
        $.ajax({
            url: baseUrl + "FundTransfer/CAM_Clausula_Blur",
            data: { text: val },
            method: "POST",
            success: function (data) {
                viewModelDeclaracionToView(data);
            }
        });
    });
    $("#txt_Comisiones").blur(function () {
        var val = $("#txt_Comisiones").val();
        $.ajax({
            url: baseUrl + "FundTransfer/CAM_Comisiones_Blur",
            data: { text: val },
            method: "POST",
            success: function (data) {
                viewModelDeclaracionToView(data);
            }
        });
    });
    $("#txt_CAM_Otros").blur(function () {
        var val = $("#txt_CAM_Otros").val();
        $.ajax({
            url: baseUrl + "FundTransfer/CAM_Otros_Blur",
            data: { text: val },
            method: "POST",
            success: function (data) {
                viewModelDeclaracionToView(data);
            }
        });
    });
    $("#txt_CAM_Liquido").blur(function () {
        var val = $("#txt_CAM_Liquido").val();
        $.ajax({
            url: baseUrl + "FundTransfer/CAM_Liquido_Blur",
            data: { text: val },
            method: "POST",
            success: function (data) {
                viewModelDeclaracionToView(data);
            }
        });
    });

    function ReversarOperacionExportViewModelToView(model) {
        if (model != null) {

            MapComboBox($('#CmbPlanilla_SelectedValue'), model.CmbPlanilla);
            MapComboBox($('#Cb_SucursalBCCH_SelectedValue'), model.Cb_SucursalBCCH);
            MapComboBox($('#Cb_TipAnu_SelectedValue'), model.Cb_TipAnu);
            MapComboBox($("#Cb_Tipo_SelectedValue"), model.Cb_Tipo);

            if (model.Cb_TipAnu.ListIndex === -1) {
                $("#Cb_TipAnu_SelectedValue").val('');
            }

            $("#CamTipCam_Text").val(model.CamTipCam.Text);
            $("#Fecha_Text").val(model.Fecha.Text);
            $("#TipoAnuls").val(model.TipoAnuls);
            $("#Tx_TipCam_Text").val(model.Tx_TipCam.Text);
            $("#Tx_Observacion_Text").val(model.Tx_Observacion.Text);
            $("#Motivo_Text").val(model.Motivo.Text);
            $("#Numero_Text").val(model.Numero.Text);

            FundTransfer.Common.MapTextBox($('#CamTipCam_Text'), model.CamTipCam);
            FundTransfer.Common.MapTextBox($('#Fecha_Text'), model.Fecha);
            $("#Tx_NroOpe_000").val(model.Tx_NroOpe_000);
            $("#Tx_NroOpe_001").val(model.Tx_NroOpe_001);
            $("#Tx_NroOpe_002").val(model.Tx_NroOpe_002);
            $("#Tx_NroOpe_003").val(model.Tx_NroOpe_003);
            $("#Tx_NroOpe_004").val(model.Tx_NroOpe_004);

            if (model.Cb_TipAnu.Enabled) {
                $('#Cb_TipAnu_SelectedValue').removeAttr('disabled');
            }
            else {
                $('#Cb_TipAnu_SelectedValue').attr('disabled', 'disabled');
            }

            if (model.BtnDeclaracion.Enabled) {
                $('#BtnDeclaracion').removeAttr('disabled');
            }
            else {
                $('#BtnDeclaracion').attr('disabled', 'disabled');
            }

            if (model.Cb_SucursalBCCH.Enabled) {
                $('#Cb_SucursalBCCH_SelectedValue').val("25");
            }

            if (model.Cb_Tipo.Enabled) {
                $("#Cb_Tipo_SelectedValue").val("6");
            }
        }       
    }
    function ReversarOperacionDeclaracionViewModelToView(model) {
        if (model != null) {
            $("#txt_VClausula").val(model.CAM_Clausula.Text);
            $("#txt_Comisiones").val(model.CAM_Comisiones.Text);
            $("#txt_CAM_Otros").val(model.CAM_Otros.Text);
            $("#txt_CAM_Liquido").val(model.CAM_Liquido.Text);
        }
        //Deshabilitar:
        $('#txt_CAM_Liquido').attr('disabled', 'disabled');
    }
    function AbrirModuloDeclaracion(mostrar) {
        //var popup = $('#AbrirForm').val();
        if (mostrar) {
            $("#ReversarOperacionDeclaracion").modal({
                backdrop: 'static'
            });
        }
    }
    function MapComboBox(destino, origen) {
        destino.empty();
        $.each(origen.Items, function (index, item) {
            destino.append(
                $('<option/>', {
                    value: item.Data,
                    text: item.Value,
                    selected: (origen.ListIndex == index)
                })
            );
        });
    }
    $(":input").inputmask();

    if ($("#CmbPlanilla_SelectedValue").is(":enabled")) {
        $('#CmbPlanilla_SelectedValue').focus().select();
    } else {
        $('[tabindex="1"]').focus().select();
    }
    
    if ($("#Cb_Tipo_SelectedValue").is(":enabled")) {
        $("#Cb_Tipo_SelectedValue").val("6");
    }

    $("#modal").on("shown.bs.modal", function () { $("#modal button.btn-primary").focus(); });
    $("#modal").on("hidden.bs.modal", function () { $("#btnAceptar").focus(); });

    /// Cuando haga Enter en cualquier parte de la pantalla
    $(window).keydown(function (ev) {
        var keycode = (ev.keyCode ? ev.keyCode : ev.which);
        if (keycode == 9) {  // Presiona Tab
            RecorrerTabIndex(-1, ev);
        }

        if (keycode == 13) {  // Presiona Enter
            ev.preventDefault();
            if ($("#modal").is(":visible")) {
                $("#modal button.btn-primary").click();
            }
            if ($("#btnAceptar").is(":enabled")) {
                $.blockUI({ message: '<h6>Cargando...</h6>' });
                $("#btnAceptar").click();                
            } else if ($("#btnOk").is(":enabled")) {
                $("#btnOk").click();
            }
            else {
                $.blockUI({ message: '<h6>Cargando...</h6>' });
                var numeroOperacion = $("#Tx_NroOpe_004").val();
                $("#Tx_NroOpe_004").val(numeroOperacion.padStart(5, "0"));
                $("#BtnOkOperacion").click();                
            }
        }
    });
});