$(document).ready(function () {

    var baseUrl = $("#base_url").val();
    var viewModel = null;
    var CaracteristicasParticipantes = $('#frmCaracteristicasParticipante');

    /// Comentado, funciones para hacerlo Knochout
    //$(document).ajaxStart(function () {
    //    $.blockUI({ message: "<h6>Espere un momento ...</h6>" });
    //}).ajaxStop($.unblockUI);

    //var updateModel = function (newViewModel) {
    //    debugger;
    //    ko.mapping.fromJS(newViewModel, {}, viewModel);
    //    //obtengo los errores y los muestro
    //    var errors = ko.mapping.toJS(viewModel.Errors);
    //    loadMessages(errors);
    //}
    //var ignoreList = [];

    //var caracteristicasParticipanteViewModel = function(data){
    //    ko.mapping.fromJS(data, {}, this);
    //    var that = this;
    //};

    //$.ajax({
    //    method: "GET",
    //    cache: false,
    //    url: baseUrl + "AdminParticipantes/CaracteristicasParticipante",
    //    success: function (data) {
    //        var nodo = CaracteristicasParticipantes.get(0);
    //        ko.cleanNode(nodo);
    //        viewModel = new caracteristicasParticipanteViewModel(data);
    //        ko.applyBindings(viewModel, nodo);

    //        //updateModel(viewModel);
    //    }
    //});

    //function AjaxCall(url, func) {
    //    viewModel.Errors([]);
    //    $.ajax({
    //        method: "POST",
    //        url: baseUrl + url,
    //        data: {
    //            jsonModel: ko.mapping.toJS(viewModel, {
    //                ignore: ignoreList
    //            })
    //        },
    //        success: function (a) {
    //            updateModel(a);

    //            if (func) {
    //                func();
    //            }
    //            if (a.Errors.length == 0) {
    //                $.unblockUI();
    //            }
    //        },
    //        error: function (err) {
    //            //console.error(err.responseText);
    //        }
    //    });
    //}

    $('#toolbarMainPrty').hide();
    $('#mnuMainArchivo').hide();
    $('#mnuMainOpciones').hide();    
    
    $('#BtnAceptar').click(function () {
        sessionStorage.setItem('modificando', true);
        modificando = sessionStorage.getItem('modificando');
    });

    $('#clienteContainer input').change(function () {
        var value = $('#clienteContainer input:checked').val();

        $.post(baseUrl + "AdminParticipantes/Caracteristicas_PrtCliente_Click", { selectedValue: value }, function (dataresult) {
            if (dataresult != null) {
                var botonAceptar = $('#BtnAceptar');
                if (dataresult.BtnAceptar.Enabled)
                    botonAceptar.removeAttr('disabled');
                else
                    botonAceptar.attr('disabled', 'disabled');

                var cOficina = $('#cbOficina_SelectedValue');
                if (dataresult.cboOficina.Enabled)
                    cOficina.removeAttr('disabled');
                else
                    cbOficina.attr('disabled', 'disabled');

                var ejecutivo = $('#cbEjecutivo_SelectedValue');
                if (dataresult.cbEjecutivo.Enabled)
                    ejecutivo.removeAttr('disabled');
                else
                    ejecutivo.attr('disabled', 'disabled');

                var cbActividadEconomico = $('#cbActividadEconomico_SelectedValue');
                if (dataresult.cbActividadEconomico.Enabled)
                    cbActividadEconomico.removeAttr('disabled');
                else
                    cbActividadEconomico.attr('disabled', 'disabled');

                var cbClaseRiesgo = $('#cbClaseRiesgo_SelectedValue');
                if (dataresult.cbClaseRiesgo.Enabled)
                    cbClaseRiesgo.removeAttr('disabled');
                else
                    cbClaseRiesgo.attr('disabled', 'disabled');

                var cbClasificacion = $('#cbClasificacion_SelectedValue');
                if (dataresult.cbClasificacion.Enabled)
                    cbClasificacion.removeAttr('disabled');
                else
                    cbClasificacion.attr('disabled', 'disabled');
            }
        });
    });

    $("#cbEjecutivo_SelectedValue").change(function () {
        var selectedItem = $(this).val();
        $.post(baseUrl + "AdminParticipantes/Cb_Ejecutivo_Click", { idcbEjecutivo: selectedItem }, function (dataresult) {
            if (dataresult != null) {

                var botonAceptar = $('#BtnAceptar');
                if (dataresult.BtnAceptar.Enabled)
                    botonAceptar.removeAttr('disabled');
                else
                    botonAceptar.attr('disabled', 'disabled');
            }
        });
    });

    $("#cboOficina_SelectedValue").change(function () {

        var selectedItem = $(this).val();

        // Buscamos los datos relacionados a la oficina seleccionada.
        $.post(baseUrl + "AdminParticipantes/Cb_Oficina_Click", { idcboficina: selectedItem }, function (dataresult) {
            if (dataresult != null) {

                var cb_bEjecutivo = $('#cbEjecutivo_SelectedValue');

                // Traemos la lista de Ejecutivos relacionados a la oficina seleccionada.
                var traeLista = dataresult.cbEjecutivo.Items;
                cb_bEjecutivo.html('');
                $.each(traeLista, function (id, option) {
                    cb_bEjecutivo.append($('<option></option>').val(option.Data).html(option.Value));
                    cb_bEjecutivo.selected = option.Selected;
                });

                cb_bEjecutivo.val(dataresult.cbEjecutivo.Value);

                var botonAceptar = $('#BtnAceptar');

                if (dataresult.BtnAceptar.Enabled)
                    botonAceptar.removeAttr('disabled');
                else
                    botonAceptar.attr('disabled', 'disabled');
            }
        });
    });

    $("#cbActividadEconomico_SelectedValue").change(function () {
        var selectedItem = $(this).val();
        $.post(baseUrl + "AdminParticipantes/Cb_ActividadEconomica_Click", { idcbActividadEconomica: selectedItem }, function (dataresult) {
            if (dataresult != null) {

                var botonAceptar = $('#BtnAceptar');
                if (dataresult.BtnAceptar.Enabled)
                    botonAceptar.removeAttr('disabled');
                else
                    botonAceptar.attr('disabled', 'disabled');
            }
        });
    });

    $("#cbClaseRiesgo_SelectedValue").change(function () {
        var selectedItem = $(this).val();
        $.post(baseUrl + "AdminParticipantes/Cb_ClaseRiesgo_Click", { idcbClaseRiesgo: selectedItem }, function (dataresult) {
            if (dataresult != null) {

                var botonAceptar = $('#BtnAceptar');
                if (dataresult.BtnAceptar.Enabled)
                    botonAceptar.removeAttr('disabled');
                else
                    botonAceptar.attr('disabled', 'disabled');
            }
        });
    });

    $("#cbClasificacion_SelectedValue").change(function () {
        var selectedItem = $(this).val();
        $.post(baseUrl + "AdminParticipantes/Cb_Clasificacion_Click", { idcbClasificacion: selectedItem }, function (dataresult) {
            if (dataresult != null) {

                var botonAceptar = $('#BtnAceptar');
                if (dataresult.BtnAceptar.Enabled)
                    botonAceptar.removeAttr('disabled');
                else
                    botonAceptar.attr('disabled', 'disabled');
            }
        });
    });

    //Importacion  
    $("#CbCenCosImportacion_SelectedValue").change(function () {
        var selectedItem = $(this).val();
        var ddlCbo_EspecImp = $("#CbEspecImportacion_SelectedValue");
        $.ajax({
            cache: false,
            url: baseUrl + "AdminParticipantes/Cb_EspecImpIdCbCenCosImportacion",
            type: "POST",
            data: { idcbCenCosImportacion: selectedItem },
            success: function (data) {
                if (data.CbEspecImportacion.Items != null) {
                    var traeLista = data.CbEspecImportacion.Items;
                    loadMessages(data.MensajesDeError);
                    ddlCbo_EspecImp.html('');
                    $.each(traeLista, function (id, option) {
                        ddlCbo_EspecImp.append($('<option></option>').val(option.Data).html(option.Value));
                    });
                }
            }
        });
    });


    $("#CbEspecImportacion_SelectedValue").change(function () {
        var selectedItem = $(this).val();
        $.ajax({
            cache: false,
            url: baseUrl + "AdminParticipantes/Cb_EspecImportacion",
            type: "POST",
            data: { idcbEspecImportacion: selectedItem },
            success: function (data) {
                if (data != null)
                    loadMessages(data.MensajesDeError);
            }
        });
    });

    //Exportacion 
    $("#CbCenCosExportacion_SelectedValue").change(function () {
        var selectedItem = $(this).val();
        var ddlCbo_EspecExp = $("#CbEspecExportacion_SelectedValue");
        $.ajax({
            cache: false,
            url: baseUrl + "AdminParticipantes/Cb_EspecExpIdCbCenCosExportacion",
            type: "POST",
            data: { idcbCenCosExportacion: selectedItem },
            success: function (data) {
                if (data.CbEspecExportacion.Items != null) {
                    var traeLista = data.CbEspecExportacion.Items;
                    ddlCbo_EspecExp.html("");
                    $.each(traeLista, function (id, option) {
                        ddlCbo_EspecExp.append($('<option></option>').val(option.Data).html(option.Value));
                    });
                }
            }
        });
    });

    $("#CbEspecExportacion_SelectedValue").change(function () {
        var selectedItem = $(this).val();
        $.ajax({
            cache: false,
            url: baseUrl + "AdminParticipantes/Cb_EspecExportacion",
            type: "POST",
            data: { idcbEspecExportacion: selectedItem },
            success: function (data) {
                if (data != null)
                    loadMessages(data.MensajesDeError);
            }
        });
    });

    //Negocio 
    $("#CbCenCosNegocio_SelectedValue").change(function () {
        var selectedItem = $(this).val();
        var ddlCbo_EspecNeg = $("#CbEspecNegocio_SelectedValue");
        $.ajax({
            cache: false,
            url: baseUrl + "AdminParticipantes/Cb_EspecExpIdCbCenCosNegocio",
            type: "POST",
            data: { idcbCenCosNegocio: selectedItem },
            success: function (data) {
                if (data.CbEspecNegocio.Items != null) {
                    var traeLista = data.CbEspecNegocio.Items;
                    ddlCbo_EspecNeg.html('');
                    $.each(traeLista, function (id, option) {
                        ddlCbo_EspecNeg.append($('<option></option>').val(option.Data).html(option.Value));
                    });
                }


            }
        });
    });

    $("#CbEspecNegocio_SelectedValue").change(function () {
        var selectedItem = $(this).val();
        $.ajax({
            cache: false,
            url: baseUrl + "AdminParticipantes/Cb_EspecNegocio",
            type: "POST",
            data: { idcbEspecNegocio: selectedItem },
            success: function (data) {
                if (data != null)
                    loadMessages(data.MensajesDeError);
            }
        });
    });

    //Importacion
    $("#BtnIngresoImportacion").click(function () {
        var idCbCenCosImportacion = $("#CbCenCosImportacion_SelectedValue").val();
        if (idCbCenCosImportacion == "")
            idCbCenCosImportacion = -1
        var idCbEspecImportacion = $("#CbEspecImportacion_SelectedValue").val();
        if (idCbEspecImportacion == "")
            idCbEspecImportacion = -1
        //alert(idCbCenCosImportacion);     
        //alert(idCbEspecImportacion);
        $.post(baseUrl + "AdminParticipantes/Boton_IngImportacion_Click", { idCbCenCosImportacion: idCbCenCosImportacion, idCbEspecImportacion: idCbEspecImportacion }, function (dataresult) {
            if (dataresult != null) {
                loadMessages(dataresult.MensajesDeError);
                focusOnErrorControl(dataresult.MensajesDeError);
                $("#Tx_Importacion_Text").val(dataresult.Tx_Importacion.Text);
                var botonAceptar = $('#BtnAceptar');
                if (dataresult.BtnAceptar.Enabled)
                    botonAceptar.removeAttr('disabled');
                else
                    botonAceptar.attr('disabled', 'disabled');
            }
        });
    });

    $("#BtnEliminarImportacion").click(function () {
        $.post(baseUrl + "AdminParticipantes/Boton_ElimImportacion_Click", function (dataresult) {
            if (dataresult != null) {
                loadMessages(dataresult.MensajesDeError);
                focusOnErrorControl(dataresult.MensajesDeError);
                $("#CbCenCosImportacion_SelectedValue").val("");
                $("#CbEspecImportacion_SelectedValue").val("");
                $("#Tx_Importacion_Text").val("");
                var botonAceptar = $('#BtnAceptar');
                if (dataresult.BtnAceptar.Enabled)
                    botonAceptar.removeAttr('disabled');
                else
                    botonAceptar.attr('disabled', 'disabled');
            }
        });
    });

    //Exportacion
    $("#BtnIngresoExportacion").click(function () {
        var idCbCenCosExportacion = $("#CbCenCosExportacion_SelectedValue").val();
        if (idCbCenCosExportacion == "")
            idCbCenCosExportacion = -1

        var idCbEspecExportacion = $("#CbEspecExportacion_SelectedValue").val();
        if (idCbEspecExportacion == "")
            idCbEspecExportacion = -1

        $.post(baseUrl + "AdminParticipantes/Boton_IngExportacion_Click", { idCbCenCosExportacion: idCbCenCosExportacion, idCbEspecExportacion: idCbEspecExportacion }, function (dataresult) {
            if (dataresult != null) {
                loadMessages(dataresult.MensajesDeError);
                focusOnErrorControl(dataresult.MensajesDeError);
                $("#Tx_Exportacion_Text").val(dataresult.Tx_Exportacion.Text);
                var botonAceptar = $('#BtnAceptar');
                if (dataresult.BtnAceptar.Enabled)
                    botonAceptar.removeAttr('disabled');
                else
                    botonAceptar.attr('disabled', 'disabled');
            }
        });
    });

    $("#BtnEliminarExportacion").click(function () {
        $.post(baseUrl + "AdminParticipantes/Boton_ElimExportacion_Click", function (dataresult) {
            if (dataresult != null) {
                loadMessages(dataresult.MensajesDeError);
                focusOnErrorControl(dataresult.MensajesDeError);
                $("#CbCenCosExportacion_SelectedValue").val("");
                $("#CbEspecExportacion_SelectedValue").val("");
                $("#Tx_Exportacion_Text").val("");
                var botonAceptar = $('#BtnAceptar');
                if (dataresult.BtnAceptar.Enabled)
                    botonAceptar.removeAttr('disabled');
                else
                    botonAceptar.attr('disabled', 'disabled');
            }
        });
    });

    //Negocio
    $("#BtnIngresoNegocio").click(function () {
        var idCbCenCosNegocio = $("#CbCenCosNegocio_SelectedValue").val();
        if (idCbCenCosNegocio == "")
            idCbCenCosNegocio = -1

        var idCbEspecNegocio = $("#CbEspecNegocio_SelectedValue").val();
        if (idCbEspecNegocio == "")
            idCbEspecNegocio = -1

        $.post(baseUrl + "AdminParticipantes/Boton_IngNegocio_Click", { idCbCenCosNegocio: idCbCenCosNegocio, idCbEspecNegocio: idCbEspecNegocio }, function (dataresult) {
            if (dataresult != null) {
                loadMessages(dataresult.MensajesDeError);
                focusOnErrorControl(dataresult.MensajesDeError);
                $("#Tx_Negocio_Text").val(dataresult.Tx_Negocio.Text);
                var botonAceptar = $('#BtnAceptar');
                if (dataresult.BtnAceptar.Enabled)
                    botonAceptar.removeAttr('disabled');
                else
                    botonAceptar.attr('disabled', 'disabled');
            }
        });
    });

    $("#BtnEliminarNegocio").click(function () {
        $.post(baseUrl + "AdminParticipantes/Boton_ElimNegocio_Click", function (dataresult) {
            if (dataresult != null) {
                loadMessages(dataresult.MensajesDeError);
                focusOnErrorControl(dataresult.MensajesDeError);
                $("#CbCenCosNegocio_SelectedValue").val("");;
                $("#CbEspecNegocio_SelectedValue").val("");
                $("#Tx_Negocio_Text").val("");
                var botonAceptar = $('#BtnAceptar');
                if (dataresult.BtnAceptar.Enabled)
                    botonAceptar.removeAttr('disabled');
                else
                    botonAceptar.attr('disabled', 'disabled');
            }
        });
    });

    //$('#Tx_Rut_Text').rut({
    //});

    $("#Tx_Rut_Text").blur(function () {
        var Rut = $(this).val();
        $.post(baseUrl + "AdminParticipantes/FormateaRut", { Rut: Rut }, function (dataresult) {
            if (dataresult != null) {
                if (dataresult != "0")
                    $("#Tx_Rut_Text").val(dataresult.rutValido);
                else {
                    $("#Tx_Rut_Text").val("");
                    bootbox("Rut Invalido");
                }


            }
        })

    });
});