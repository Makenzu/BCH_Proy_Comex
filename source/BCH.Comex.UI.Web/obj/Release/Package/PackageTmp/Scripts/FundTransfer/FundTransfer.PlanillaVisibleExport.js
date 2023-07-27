$(function () {
    var baseUrl = $("#base_url").val(); //obtengo la url base global
    //activo el bloquear la UI en requests asincronos
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI);

    var viewModel = null;
    var plainillaVisible = $("#divPlanillaVisibleExport");

    $('#LstMontos_SelectedValue').change(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaVisibleExport_LtMto_Click",
            method: "Post",
            data: { selectedValue: $(this).val() },
            success: function (data) {
                PlanillaVisibleExportViewModelToView(data);
            },
            complete: function () {

            }
        });
    });
    $('#LstPlanillas_SelectedValue').change(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaVisibleExport_LtPln_Click",
            method: "Post",
            data: { selectedValue: $(this).val() },
            success: function (data) {
                PlanillaVisibleExportViewModelToView(data);
            },
            complete: function () {

            }
        });
    });
    $('#CmbTipoPlanilla_SelectedValue').change(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaVisibleExport_LtTPln_Click",
            method: "Post",
            data: { selectedValue: $(this).val() },
            success: function (data) {
                PlanillaVisibleExportViewModelToView(data);
                $("#CmbPaisOperacion_SelectedValue").focus().select();
            },
            complete: function () {

            }
        });
    });
    $('#divDeclaracionImportacion .tx_mtodec').blur(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaVisibleExport_Tx_MtoDec_LostFocus",
            method: "Post",
            data: {
                texto: $(this).val(),
                indice: $(this).data('indice')
            },
            success: function (data) {
                PlanillaVisibleExportViewModelToView(data);
                $("#CmbTipoPlanilla_SelectedValue").focus().select();
            },
            complete: function () {

            }
        });
    });
    $('#TxtNumeroDeclaracion_Text').blur(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaVisibleExport_Tx_NumDec_LostFocus",
            method: "Post",
            data: { 
                numDec: $('#TxtNumeroDeclaracion_Text').val(), 
                fechaDec: $('#TxtFechaDeclaracion_Text').val(),
                adn: $('#TxtCodigoDeclaracion_Text').val()
            },
            success: function (data) {
                PlanillaVisibleExportViewModelToView(data);
            },
            complete: function () {

            }
        });
    });
    $('#BtnOK').click(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaVisibleExport_OK_Click",
            method: "Post",
            data: $('#frmPlanillaVisibleExport').serialize(),
            success: function (data) {
                PlanillaVisibleExportViewModelToView(data);
                $("#BtnAceptar").focus().select();
            },
            complete: function () {
            }
        });
    });
    $('#BtnNO').click(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaVisibleExport_NO_Click",
            method: "Post",
            success: function (data) {
                PlanillaVisibleExportViewModelToView(data);
            },
            complete: function () {

            }
        });
    });
    $('#BtnOkDeclaracion').click(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaVisibleExport_OK_Dec_Click",
            method: "Post",
            data: {},
            success: function (data) {
                PlanillaVisibleExportViewModelToView(data);
            },
            complete: function () {

            }
        });
    });
    $('#divDeclaracionImportacion .ch_deduc').change(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaVisibleExport_Ch_Deduc_Click",
            method: "Post",
            data: { 
                valChecked: this.checked, 
                indice: $(this).data('indice')
            },
            success: function (data) {
                PlanillaVisibleExportViewModelToView(data);
            },
            complete: function () {

            }
        });
    });
    $("#TxtNombreComprador_Text").change(function () { toUppercase(this) }).change();

    function PlanillaVisibleExportViewModelToView(model)
    {
        //cargo los mensajes de error
        loadMessages(model.MensajesDeError);
        focusOnErrorControl(model.MensajesDeError);
        
        //mapeo controles
        FundTransfer.Common.MapTextBox($('#TxtNumeroDeclaracion_Text'), model.TxtNumeroDeclaracion);
        FundTransfer.Common.MapTextBox($('#TxtFechaDeclaracion_Text'), model.TxtFechaDeclaracion);
        FundTransfer.Common.MapTextBox($('#TxtCodigoDeclaracion_Text'), model.TxtCodigoDeclaracion);
        FundTransfer.Common.MapTextBox($('#TxtValBrutoDeclaracion_Text'), model.TxtValBrutoDeclaracion);
        FundTransfer.Common.MapTextBox($('#TxtValComisionDeclaracion_Text'), model.TxtValComisionDeclaracion);
        FundTransfer.Common.MapTextBox($('#TxtValFleteDeclaracion_Text'), model.TxtValFleteDeclaracion);
        FundTransfer.Common.MapTextBox($('#TxtValSeguroDeclaracion_Text'), model.TxtValSeguroDeclaracion);
        FundTransfer.Common.MapTextBox($('#TxtValGastoCorredorDeclaracion_Text'), model.TxtValGastoCorredorDeclaracion);
        FundTransfer.Common.MapTextBox($('#TxtValAjusteDeclaracion_Text'), model.TxtValAjusteDeclaracion);
        FundTransfer.Common.MapTextBox($('#TxtValLiquidoDeclaracion_Text'), model.TxtValLiquidoDeclaracion);
        FundTransfer.Common.MapTextBox($('#TxtValTranVariasDeclaracion_Text'), model.TxtValTranVariasDeclaracion);
        FundTransfer.Common.MapTextBox($('#TxtValTotalCompraDeclaracion_Text'), model.TxtValTotalCompraDeclaracion);
        FundTransfer.Common.MapCheckBox($('#ChkValComisionesDeclaracion_Checked'), model.ChkValComisionesDeclaracion);
        FundTransfer.Common.MapCheckBox($('#ChkValFleteDeclaracion_Checked'), model.ChkValFleteDeclaracion);
        FundTransfer.Common.MapCheckBox($('#ChkValSeguroDeclaracion_Checked'), model.ChkValSeguroDeclaracion);
        FundTransfer.Common.MapComboBox($('#CmbSecEcBen_SelectedValue'), model.CmbSecEcBen);
        FundTransfer.Common.MapComboBox($('#CmbSecEcInv_SelectedValue'), model.CmbSecEcInv);
        FundTransfer.Common.MapTextBox($('#TxtValPorcentParticipante_Text'), model.TxtValPorcentParticipante);
        FundTransfer.Common.MapTextBox($('#TxtNombreComprador_Text'), model.TxtNombreComprador);

        FundTransfer.Common.MapTextBox($('#TxtValTotalPlanilla_Text'), model.TxtValTotalPlanilla);
        FundTransfer.Common.MapComboBox($('#CmbTipoPlanilla_SelectedValue'), model.CmbTipoPlanilla);
        FundTransfer.Common.MapTextBox($('#TxtValBrutoPlanilla_Text'), model.TxtValBrutoPlanilla);
        FundTransfer.Common.MapTextBox($('#TxtValComisionPlanilla_Text'), model.TxtValComisionPlanilla);
        FundTransfer.Common.MapTextBox($('#TxtOtrosGastosPlanilla_Text'), model.TxtOtrosGastosPlanilla);
        FundTransfer.Common.MapTextBox($('#TxtValLiquidoPlanilla_Text'), model.TxtValLiquidoPlanilla);
        FundTransfer.Common.MapTextBox($('#TxtTipoCambioPlanilla_Text'), model.TxtTipoCambioPlanilla);
        FundTransfer.Common.MapTextBox($('#TxtFecVencRetornoPlanilla_Text'), model.TxtFecVencRetornoPlanilla);
        FundTransfer.Common.MapTextBox($('#TxtObservacionesPlanilla_Text'), model.TxtObservacionesPlanilla);

        FundTransfer.Common.MapTextBox($('#TxtPlazoFinanciamiento_Text'), model.TxtPlazoFinanciamiento);
        FundTransfer.Common.MapTextBox($('#TxtNumPresentacion_Text'), model.TxtNumPresentacion);
        FundTransfer.Common.MapTextBox($('#TxtFechaPresentacion_Text'), model.TxtFechaPresentacion);
        FundTransfer.Common.MapComboBox($('#CmbBanco_SelectedValue'), model.CmbBanco);
        FundTransfer.Common.MapComboBox($('#CmbPlazaBancoCentral_SelectedValue'), model.CmbPlazaBancoCentral);
        FundTransfer.Common.MapComboBox($('#CmbTipoPlanillaInformar_SelectedValue'), model.CmbTipoPlanillaInformar);
        FundTransfer.Common.MapTextBox($('#TxtFechaInscripcion_Text'), model.TxtFechaInscripcion);
        FundTransfer.Common.MapTextBox($('#TxtNumeroInscripcion_Text'), model.TxtNumeroInscripcion);
        FundTransfer.Common.MapComboBox($('#CmbPaisOperacion_SelectedValue'), model.CmbPaisOperacion);

        FundTransfer.Common.MapListBox($('#LstMontos_SelectedValue'), model.LstMontos);
        FundTransfer.Common.MapListBox($('#LstPlanillas_SelectedValue'), model.LstPlanillas);

        FundTransfer.Common.MapButton($('#BtnAceptar'), model.BtnAceptar);
        FundTransfer.Common.MapButton($('#BtnVisualizar'), model.BtnVisualizar);
        FundTransfer.Common.MapButton($('#BtnCancelar'), model.BtnCancelar);

        FundTransfer.Common.MapButton($('#BtnOK'), model.BtnOK);
        FundTransfer.Common.MapButton($('#BtnNO'), model.BtnNO);
    }
    $(":input").inputmask();
});