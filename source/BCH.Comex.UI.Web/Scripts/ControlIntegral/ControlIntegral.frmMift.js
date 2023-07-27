var baseUrl = $("#base_url").val();
var formConsultaControlIntegralTab = $("#frmConsultaControlIntegralTab1");

window.eventoObservacion = {
    'click .Observacion': function (e, value, row, index) {
        //if (formConsultaControlIntegralTab.valid()) {
        var data = formConsultaControlIntegralTab.serialize();
        $.post(baseUrl + "ControlIntegral/PreflxTarifas", data, function (dataResult1) {
            if (dataResult1.Data.txtMonto > 0) {
                $.post(baseUrl + "ControlIntegral/VerificaExisteValorPizarra", { row: row['Observacion'], index: index }, function (dataResult2) {
                    if (dataResult2.tCal) {
                        if (row['Tarifas'] == "b) Swift" || row['Tarifas'] == "c) Speed Transfer")
                            buscarFlxPizarraObs(cfgColTableFlxPizarraObs2, row['Tarifas'], index, dataResult2.tCal);
                        else
                            buscarFlxPizarraObs(cfgColTableFlxPizarraObs1, row['Tarifas'], index, dataResult2.tCal);
                    }
                    else {
                        buscarFlxPizarraObs(cfgColTableFlxPizarraObs, row['Tarifas'], index, dataResult2.tCal);
                    }
                });
            }
            else {
                bootbox.alert("Debe indicar el monto de la operación");
            }

        });
        //}
    }
}

var buscarFlxPizarraObs = function (PrmconfigColumnas, row, index, opcion) {
    $('#datatable_flxPizarra').bootstrapTable('destroy');
    $('#datatable_flxPizarra').bootstrapTable({
        height: 130, //getHeight(),
        method: 'post',
        url: baseUrl + "ControlIntegral/flxTarifas_Click",
        queryParams: function (p) {
            return {
                row: row,
                index: index,
                tCal: opcion
            };
        },
        showHeader: true,
        locale: "es-SP",
        columns: PrmconfigColumnas,
        searchAlign: 'left',
        showRefresh: false,
        clickToSelect: true,
        search: false,
        checkboxHeader: false,
        toolbar: '#custom-toolbar',
        striped: true
    });
}

var cfgColTableFlxPizarraObs1 = [
  {
      field: 'Pizarra', //Columna Dinamica
      title: 'Pizarra',
      sortable: false,
      align: 'left',
  },
   {
       field: 'USD_Min',
       title: 'USD Min',
       sortable: false,
       align: 'left'
   },
  {
      field: 'USD_Max',
      title: 'USD Max',
      sortable: false,
      align: 'right'
  },
  {
      field: 'Tasa',
      title: 'Tasa',
      sortable: false,
      align: 'right'
  },
  {
      field: 'Minimo',
      title: 'Minimo',
      sortable: false,
      align: 'right'
  },
  {
      field: 'Maximo',
      title: 'Maximo',
      sortable: false,
      align: 'right'
  }];

var cfgColTableFlxPizarraObs2 = [

  {
      field: 'Pizarra',
      title: 'Pizarra',
      sortable: false,
      align: 'left',
  },
   {
       field: 'Valor',
       title: 'Valor',
       sortable: false,
       align: 'left'
   }];

var cfgColTableFlxPizarraObs = [{
    field: 'Observaciones',
    title: 'Observaciones',
    sortable: false,
    align: 'left',
}];




$(document).ready(function () {
    var baseUrl = $("#base_url").val();
    var txtLlave = $("#txtCuenta").val().trim();

    //activo el bloquear la UI en requests asincronos
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI);

    var resetTabsNav = function () {
        $(".nav-tabs").find('li').each(function () {
            $(this).removeClass('active');
        });
        $(".tab-content").find('.tab-pane').each(function () {
            $(this).removeClass('active');
        });
    }

    var defineTabsNav = function () {
        resetTabsNav();
        $("#tab_Consulta").show().addClass('active');
        $("#tab_0").addClass('active');
        $("#tab_Mandato").hide();

    }

    var visibleOInvisbleBotones = function (EsVisibleReparar, EsVisibleCheckListar, EsVisibleVerRecurrencia, EsVisibleTipoCambio) {
        var btnReparar = $("#btnReparar");
        var btnCheckListar = $("#btnCheckList");
        var btnVerRecurrencia = $("#btnVerRecurrencia");
        var btnTipoCambio = $("#btnTipoCambio");
        visibleOInvisbleElemento(btnReparar, EsVisibleReparar);
        visibleOInvisbleElemento(btnCheckListar, EsVisibleCheckListar);
        visibleOInvisbleElemento(btnVerRecurrencia, EsVisibleVerRecurrencia);
        visibleOInvisbleElemento(btnTipoCambio, EsVisibleTipoCambio);
    }

    var visibleOInvisbleElementosDatosOrdenante = function (esvisibleRut, esVisibleBase, esVisibleNombreClte, esVisibleLblEjecutivo, esVisibleLblSegmento, esVisibleOpTipoCargo) {
        var txtRut = $("#txtRut");
        var txtDV = $("#txtDV");
        var divRut = $("#divDatosOrdenanteRut");

        var txtBase = $("#txtBase");
        var divBase = $("#divDatosOrdenanteBase");

        var lblNombreClte = $("#lblNombreClte");
        var lblEjecutivo = $("#lblEjecutivo");
        var lblSegmento = $("#lblSegmento");

        var opTipoCargo = $("#EsVisibleChkOpc");

        visibleOInvisbleElemento(divRut, esvisibleRut);
        visibleOInvisbleElemento(divBase, esVisibleBase);
        visibleOInvisbleElemento(lblNombreClte, esVisibleNombreClte);
        visibleOInvisbleElemento(lblEjecutivo, esVisibleLblEjecutivo);
        visibleOInvisbleElemento(lblSegmento, esVisibleLblSegmento);
        visibleOInvisbleElemento(opTipoCargo, esVisibleOpTipoCargo);
    }

    function visibleOInvisbleElemento(elemento, visible) {
        if (visible)
            elemento.show();
        else
            elemento.hide();
    }

    function limpiarObjetos() {
        $("#txtCuenta").val("");
        $("#txtRut").val("");
        $("#txtDV").val("");
        $("#txtBase").val("");
        $("#lblNombreClte").val("");
        //$("#lblEjecutivo").val("");
        //$("#lblSegmento").val("");
        $("#lblEjecutivo").text("");
        $("#lblSegmento").text("");

        $("#txtMonto").val("");
        $("#txtCuentab").val("");
        $("#txtNombreb").val("");
        $("#txtBancoib").val("");
        $("#txtBancopb").val("");
    }

    function escondeCirculo(esVisible) {

        if (esVisible)
            $("#circulo").show();
        else {
            $("#lblResultado").text('');
            $("#circulo").hide();
        }
    }

    defineTabsNav();
    visibleOInvisbleBotones(false, false, false, false);
    visibleOInvisbleElementosDatosOrdenante(false, false, false, false, false, false);
    escondeCirculo(false);
    $("#idDivFrame1").hide(); //Frame1
    $("#divLblLog2").hide();
    $("#lblLog2").text("");

    var formConsultaControlIntegralTab1 = $("#frmConsultaControlIntegralTab1");
    formConsultaControlIntegralTab1.validate({
        rules: {
            txtCuenta: {
                required: true
            },
            txtMonto: {
                required: true,
                number: true,
                min: 0
            }
        },
        messages: {
            txtCuenta: {
                required: "Llave es requerido."
            },
            txtMonto: {
                required: "Monto de operación requerido.",
                number: "Ingrese números.",
                min: "Ingrese números positivos."
            }//,
            //txtCuentab: {
            //    number: "Ingrese números."
            //}
        }
    });

    //$("#txtCuenta").change(function (e) {
    $('#txtCuenta').keyup(function (e) {
        if (e.keyCode == 13) {
            if ($(this).val() == "") {
                cmdLimpiar();
            }
            else if ($(this).val() != "") {
                botonBuscar();
            }
        }
    });

    //$("#txtMonto").change(function (e) {
    $('#txtMonto').keyup(function (e) {
        if (e.keyCode == 13) {
            if ($(this).val() != "" && $("#txtCuenta").val() != "") {
                botonBuscar();
            }
        }
    });

    $("#txtCuenta").blur(function (e) {
        if ($(this).val() == "") {
            cmdLimpiar();
        }
    });

    function cmdLimpiar() {
        var data = formConsultaControlIntegralTab1.serialize();
        $.post(baseUrl + "ControlIntegral/CmdLimpiar", data, function (dataLimpiar) {
            if (dataLimpiar != null) {
                $("#txtRut").val("");
                $("#txtCuenta").val("");
                $("#txtMonto").val("");
                $("#txtCuentab").val("");
                $("#txtNombreb").val("");
                $("#txtBancoib").val("");
                $("#txtBancopb").val("");
                $("#lblNombreClte").css("background-color", "black");
                $("#btnTipoCambio").removeClass("btn-warning");
                $("#btnTipoCambio").removeClass("btn-success");
                $("#btnTipoCambio").addClass("btn-primary");
                // $("#btnTipoCambio").text("aaaaaaa");  
                $("#cmdMesa").val(false);
                $("#cmdVRecurrencia").val(false);
                visibleOInvisbleElemento($("#btnTipoCambio"), false);
                visibleOInvisbleElemento($("#btnCheckList"), false);
                visibleOInvisbleElemento($("#btnVerRecurrencia"), false);
                visibleOInvisbleElemento($("#btnReparar"), false);
                escondeCirculo(false);
                Limpiar_Resultado();

            }
        });
    }

    $('#btnBuscarDatosCliente').click(function (e) {
        e.preventDefault();
        cmdLimpiar();
        $.post(baseUrl + "ControlIntegral/Empresa", function (dataResult) {
            if (dataResult != null) {
                $("#modal_dialog_Empresa_content").html(dataResult);
                $('#modal_dialog_Empresa').modal('show');
            }
            else
                bootbox.alert("Error al abrir lista de Ordenantes");
        });

    });

    function botonBuscar() {

        if (formConsultaControlIntegralTab1.valid()) {


            var data = formConsultaControlIntegralTab1.serialize();
            $.post(baseUrl + "ControlIntegral/BotonBuscar", data, function (dataResult1) {
                $("#txtCuenta").val(dataResult1.txtCuenta);
                Limpiar_Resultado();
                visibleOInvisbleBotones(true, true, false, false);

                Cargar_Mensajes();
                //buscarFlxTarifas(cfgColTableFlxTarifas);//Metodo Cargar_Cliente aqui cargar obs 
                VerificaTipoFlxTarifas();
                $("#idDivFrame1").hide(); //id Div Documentos Duplicados
                $("#Frame1").val(dataResult1.Frame1);
                buscarFlxCitidocDPL(cfgColTableFlxCitidocDPL);

                if ($("#Frame1").val() == false) {
                    bootbox.alert("Advertencia existe DUPLICIDAD EN CITIDOCS.");
                }

                $.post(baseUrl + "ControlIntegral/GetDatosOrdenantes", data, function (dataResult2) {
                    visibleOInvisbleElementosDatosOrdenante(false, false, true, true, true, false);
                    if (dataResult2.MaxJsonLength > 0) { //Si no es fin de archivo

                        $("#lblNombreClte").show(); //Por el tema que se ve punto del span negro
                        $("#HiNombreClte").val(dataResult2.Data.lblNombreClte);
                        $("#HiEjecutivo").val(dataResult2.Data.lblEjecutivo);
                        $("#HiSegmento").val(dataResult2.Data.lblSegmento);
                        $("#lblNombreClte").text(dataResult2.Data.lblNombreClte);
                        //$("#lblEjecutivo").val(dataResult2.Data.lblEjecutivo);
                        //$("#lblSegmento").val(dataResult2.Data.lblSegmento);
                        $("#lblEjecutivo").text(dataResult2.Data.lblEjecutivo);
                        $("#lblSegmento").text(dataResult2.Data.lblSegmento);

                        if (dataResult2.Data.est_recurrencia == 1) {
                            if (dataResult2.Data.cmdVRecurrencia)
                                $("#btnVerRecurrencia").show();
                        }

                        if (dataResult2.Data.lblNombreClte == "" && dataResult2.Data.lblEjecutivo == "" && dataResult2.Data.lblSegmento) {
                            Cambiar_Color("vbRed");
                            $("#Error_original").val("");

                            $.post(baseUrl + "ControlIntegral/Grabar_Log", data, function (dataGrabarResult1) {
                                if (dataGrabarResult1.SeGrabo != 1) {
                                    bootbox.alert("Error en Grabar_Log() " + " Informe al administrador del sistema.");
                                }
                            });
                            bootbox.alert("Cliente llave: " + $("#txtCuenta").val() + "No existe, verifique los datos de busqueda.");
                        }

                        if (!dataResult2.Paso_Contratos) {
                            cmdMesa = $("#cmdMesa").val();
                            var data1 = formConsultaControlIntegralTab1.serialize();
                            $.post(baseUrl + "ControlIntegral/Cargar_Contratos", data1, function (dataResult4) {
                                if (dataResult4 != null) {

                                    if (dataResult4.MaxJsonLength > 0) { //Si no es Fin de archivo

                                        if (dataResult4.Data.Indicador_mift == "NO" && dataResult4.Data.Indicador_fax_local == "NO" && dataResult4.Data.Indicador_citi_offshore == "NO" &&
                                            dataResult4.Data.Indicador_fax_NY_Londres == "NO" && dataResult4.Data.Indicador_anexo_mail == "NO" && dataResult4.Data.Indicador_otros == "NO") {

                                            Cambiar_Color("vbRed");
                                            $("#Error_original").val("Cliente no tiene datos de contratos.");

                                            $.post(baseUrl + "ControlIntegral/Grabar_Log", data, function (dataGrabarResult2) {
                                                if (dataGrabarResult2.SeGrabo != 1) {
                                                    bootbox.alert("Error en Grabar_Log() " + " Informe al administrador del sistema.");
                                                }
                                            });
                                            bootbox.alert("Cliente no tiene datos de contratos.");
                                        }
                                        else {

                                            $("#lblChkContratoMift").text(dataResult4.Data.lblChkContratoMift);
                                            $("#lblChkContratoFax").text(dataResult4.Data.lblChkContratoFax);
                                            $("#lblChkCiti").text(dataResult4.Data.lblChkCiti);
                                            $("#lblChkfaxNY").text(dataResult4.Data.lblChkfaxNY);

                                            if (dataResult4.Data.Indicador_mift == "SI") {
                                                $("#chkContratoMift").prop("checked", true);
                                                $("#lblChkContratoMift").css("color", "black");
                                            }
                                            else {
                                                $("#chkContratoMift").prop("checked", false);
                                                $("#lblChkContratoMift").css("color", "red");
                                            }

                                            if (dataResult4.Data.Indicador_fax_local == "SI") {

                                                $("#chkContratoFax").prop("checked", true);
                                                $("#lblChkContratoFax").css("color", "black");
                                            }
                                            else {
                                                $("#chkContratoFax").prop("checked", false);
                                                $("#lblChkContratoFax").css("color", "red");
                                            }

                                            if (dataResult4.Data.Indicador_citi_offshore == "SI") {
                                                $("#chkCiti").prop("checked", true);
                                                $("#lblChkCiti").css("color", "black");
                                            }
                                            else {
                                                $("#chkCiti").prop("checked", false);
                                                $("#lblChkCiti").css("color", "red");
                                            }

                                            if (dataResult4.Data.Indicador_fax_NY_Londres == "SI") {
                                                $("#chkfaxNY").prop("checked", true);
                                                $("#lblChkfaxNY").css("color", "black");
                                            }
                                            else {
                                                $("#chkfaxNY").prop("checked", false);
                                                $("#lblChkfaxNY").css("color", "red");
                                            }

                                            if (dataResult4.Data.resultado == "OK")
                                                Cambiar_Color("vbGreen");
                                            else if (dataResult4.Data.resultado == "DIS") {
                                                Cambiar_Color("vbRed");
                                                $("#lblNombreClte").css("background-color", "red");
                                                $("#lblResultado").text("SOC. DISUELTA");
                                                bootbox.alert("Advertencia SOCIEDAD DISUELTA");
                                            }
                                            else
                                                Cambiar_Color("vbYellow");

                                            if (($("#chkContratoMift").prop('checked') && !dataResult4.Paso_Recurrencia) || (dataResult4.Data.cmdVRecurrencia && !$("#txtCuentab").val() != "")) {

                                                if (parseFloat($("#txtMonto").val()) > 0) {

                                                    Cambiar_Color("vbYellow");
                                                    var opcionTipo = $('input[type=radio][name=opTipo]:checked').attr('id');
                                                    //alert("cmdMesa: " + dataResult4.Data.cmdMesa)
                                                    if (opcionTipo == "opTipo1") {
                                                        var opcion = 0;

                                                        $.post(baseUrl + "ControlIntegral/CantidadRegistrosRecurrencia", function (dataRest) {
                                                            if (dataRest.RegistrosRecurrencia.length != 0) {
                                                                $.post(baseUrl + "ControlIntegral/Cargar_Recurrencia", { opcion: opcion }, function (dataContrato) {
                                                                    if (dataContrato != null) {
                                                                        $("#modal_dialog_Detalle_content").html(dataContrato);
                                                                        $('#modal_dialog_Detalle').modal('show');
                                                                    }
                                                                });
                                                            }
                                                        });

                                                        $.post(baseUrl + "ControlIntegral/cambios_mift_recurrencia_01b", function (dataRestb) {
                                                            if (dataRestb != null) {                                                            
                                                                if (dataRestb.retorno == "-1")
                                                                    Cambiar_Color("vbBurdeo");
                                                                else if (dataRestb.retorno == "-2")
                                                                    Cambiar_Color("vbRed");
                                                                else 
                                                                    Cambiar_Color("vbGreen")
                                                            }
                                                        })

                                                    }
                                                    else {

                                                        if (dataResult4.Data.cmdMesa) {
                                                            if (formConsultaControlIntegralTab1.valid()) {
                                                                $.post(baseUrl + "ControlIntegral/DetalleMesa", function (dataResult) {
                                                                    $("#modal_dialog_DetalleMesa_content").html(dataResult);
                                                                    $('#modal_dialog_DetalleMesa').modal('show');
                                                                });
                                                            }
                                                        }
                                                    }

                                                }
                                                else {

                                                    Cambiar_Color("vbYellow");
                                                    $("#lblResultado").text("Ingresar Monto");
                                                    bootbox.alert("Debe indicar el monto de la operación.");
                                                }
                                            }
                                        }
                                    }
                                    else {
                                        Cambiar_Color("vbRed");
                                        $('#modal_dialog_Detalle').modal('hide');
                                        $.post(baseUrl + "ControlIntegral/Grabar_Log", data, function (dataGrabarResult4) {
                                            if (dataGrabarResult4.SeGrabo != 1) {
                                                bootbox.alert("Error en Grabar_Log() " + " Informe al administrador del sistema.");
                                            }
                                        });
                                        bootbox.alert("Cliente llave " + $("#txtCuenta").val() + " no tiene contratos firmados.");

                                    }
                                }
                            });
                        }
                    }
                    else {
                        Cambiar_Color("vbRed");
                        $('#modal_dialog_Detalle').modal('hide');
                        $("#Error_original").val("");
                        $.post(baseUrl + "ControlIntegral/Grabar_Log", data, function (dataGrabarResult3) {
                            if (dataGrabarResult3.SeGrabo != 1) {
                                bootbox.alert("Error en Grabar_Log() " + " Informe al administrador del sistema.");
                            }
                        });
                        bootbox.alert("Llave " + $("#txtCuenta").val() + " No existe, verifique los datos de busqueda.");
                    }

                });

            });
        }


    }

    $('#btnBuscar').click(function (e) {
        e.preventDefault();
        botonBuscar();
    });

    $("input[name=opTipo]:radio").click(function () {
        opTipo = $('input[type=radio][name=opTipo]:checked').attr('id');

        if (opTipo == "opTipo1") {
            $("#opTipo1").prop("checked", true);
            $("#opTipo2").prop("checked", false);
        }
        else {
            $("#opTipo2").prop("checked", true);
            $("#opTipo1").prop("checked", false);
        }
    });

    function Cambiar_Color(Color) {
        if (Color == "vbRed") {
            $("#lblResultado").text("Rechazar");
            $("#circulo").css("background-color", "red");
        }
        else if (Color == "vbYellow") {
            $("#lblResultado").text("Indeterminado");
            $("#circulo").css("background-color", "yellow");
        }
        else if (Color == "vbGreen") {
            $("#lblResultado").text("Recurrencia OK");
            $("#circulo").css("background-color", "green");
        }
        else if (Color == "vbBurdeo") {
            $("#lblResultado").text("Solicitar CallBack");
            $("#circulo").css("background-color", "#A00C0C");
        }
        escondeCirculo(true);
    }

    function Cargar_Mensajes() {

        if (formConsultaControlIntegralTab1.valid()) {
            var data = formConsultaControlIntegralTab1.serialize()
            $.post(baseUrl + "ControlIntegral/Cargar_Mensajes", data, function (dataResult) {
                if (dataResult != null) {
                    if (dataResult.cantidadMiftCallFax > 0) {
                        $("#lblMensaje").val(dataResult.Data.lblMensaje);
                    }
                    if (dataResult.cantidadMiftMensajes > 0) {
                        $("#divLblLog2").show();
                        $("#lblLog2").text(dataResult.Data.lblLog2);
                    }
                    if (dataResult.cantidadMiftMesa > 0) {
                        if (dataResult.estado > 0) {
                            if (dataResult.color == "02550") {
                                $("#btnTipoCambio").removeClass("btn-primary");
                                $("#btnTipoCambio").removeClass("btn-warning");
                                $("#btnTipoCambio").addClass("btn-success");
                            }
                            else if (dataResult.color == "255255128") {
                                $("#btnTipoCambio").removeClass("btn-primary");
                                $("#btnTipoCambio").removeClass("btn-success");
                                $("#btnTipoCambio").addClass("btn-warning");

                            }
                            else {
                                $("#btnTipoCambio").removeClass("btn-warning");
                                $("#btnTipoCambio").removeClass("btn-success");
                                $("#btnTipoCambio").addClass("btn-primary");
                            }
                            $("#btnTipoCambio").text(dataResult.mensaje);
                            $("#btnTipoCambio").show();
                            $("#cmdMesa").val(dataResult.Data.cmdMesa)
                        }
                    }
                }
            });
        }
    }

    function VerificaTipoFlxTarifas() {

        $.post(baseUrl + "ControlIntegral/GetListaDeFlxTarifasVerificaTipo", function (dataResult) {
            if (dataResult != null) {
                if (dataResult.Tipo == "1")
                    buscarFlxTarifas(cfgColTableFlxTarifasSICC);
                else
                    buscarFlxTarifas(cfgColTableFlxTarifasPizarra);
            }
        });
    }

    var buscarFlxTarifas = function (PrmconfigColumnas) {
        var txtCuenta = $('#txtCuenta').val();
        var cbMoneda = $('#cmbMoneda').val();
        var txtMonto = $('#txtMonto').val();

        if (txtCuenta != "" || cbMoneda != "" || txtMonto != "") {
            $('#datatable_flxTarifas').bootstrapTable('destroy');
            $('#datatable_flxTarifas').bootstrapTable({
                height: 180,
                method: 'post',
                url: baseUrl + "ControlIntegral/GetListaDeFlxTarifas",
                showHeader: true,
                locale: "es-SP",
                columns: PrmconfigColumnas,
                searchAlign: 'left',
                showRefresh: false,
                clickToSelect: true,
                search: false,
                checkboxHeader: false,
                toolbar: '#custom-toolbar',
                striped: true
            });
        }
    };


    var cfgColTableFlxTarifasSICC = [
    {
        field: 'Rut',
        sortable: false,
        visible: false
    },
    {
        field: 'Tarifas', //titulo Dinamico
        title: 'Tarifas S.I.C.C.',
        sortable: false,
        align: 'left',
    },
      {
          field: 'Mon',
          title: 'Mon',
          sortable: false,
          align: 'left'
      },
    {
        field: 'Valor',
        title: 'Valor',
        sortable: false,
        align: 'right'
    },
    { title: 'Observación', formatter: AccionesGridFormatterObservacion, events: eventoObservacion, sortable: false, align: 'left', width: '150px' },
    {
        field: 'Impto',
        title: 'Monto $.',
    },
    {
        field: 'Tipo',
        visible: false
    }];


    var cfgColTableFlxTarifasPizarra = [
        {
            field: 'Rut',
            sortable: false,
            visible: false
        },
        {
            field: 'Tarifas', //titulo Dinamico
            title: 'Tarifas Pizarra',
            sortable: false,
            align: 'left',
        },
          {
              field: 'Mon',
              title: 'Mon',
              sortable: false,
              align: 'left'
          },
        {
            field: 'Valor',
            title: 'Valor',
            sortable: false,
            align: 'right'
        },
        { title: 'Observación', formatter: AccionesGridFormatterObservacion, events: eventoObservacion, sortable: false, align: 'left', width: '150px' },
        {
            field: 'Impto',
            title: 'Monto $.',
        },
        {
            field: 'Tipo',
            visible: false
        }];

    function AccionesGridFormatterObservacion(value, row, index) {
        var htmlAccion = "";
        var obs = row['Observacion'];
        if (obs == "Valor Pizarra") {
            //htmlAccion = '<a title="" class="Observacion editable editable-click" href="javascript:void(0)" data-original-title="" data-pk="5" data-name="Observacion">' + obs + '</a>';
            htmlAccion = '<a title="" class="Observacion accionRow" href="#">' + obs + '</a>';
        } else {
            htmlAccion = '<a title="" class="Observacion accionRow" style="color:black;" href="#">' + obs + '</a>';
        }
        return htmlAccion;
    }

    $('#datatable_flxTarifas').on("load-success.bs.table", function (e, data) {

        //alert(data.length);
        if (data.length > 0) {
            //alert(data.length);
            buscarFlxPizarra(cfgColTableFlxPizarra);
        }
    });

    var buscarFlxPizarra = function (PrmconfigColumnas) {

        var txtCuenta = $('#txtCuenta').val();
        if (txtCuenta != "") {
            $('#datatable_flxPizarra').bootstrapTable('destroy');
            $('#datatable_flxPizarra').bootstrapTable({
                height: 130, //getHeight(),
                method: 'post',
                url: baseUrl + "ControlIntegral/GetListaDeFlxPizarra",
                showHeader: true,
                locale: "es-SP",
                columns: PrmconfigColumnas,
                searchAlign: 'left',
                showRefresh: false,
                clickToSelect: true,
                search: false,
                checkboxHeader: false,
                toolbar: '#custom-toolbar',
                striped: true
            });
        }
    };

    var cfgColTableFlxPizarra = [{
        field: 'Observaciones',
        title: 'Observaciones',
        sortable: false,
        align: 'left',
    }];

    var buscarFlxCitidocDPL = function (PrmconfigColumnas) {

        var txtCuenta = $('#txtCuenta').val();

        if (txtCuenta != "") {
            $('#datatable_flxCitidocDPL').bootstrapTable('destroy');
            $('#datatable_flxCitidocDPL').bootstrapTable({
                height: 240,//300, //getHeight(),
                method: 'post',
                url: baseUrl + "ControlIntegral/GetListaDeFlxCitidocDPL",
                showHeader: false,
                locale: "es-SP",
                columns: PrmconfigColumnas,
                searchAlign: 'left',
                showRefresh: false,
                clickToSelect: true,
                search: false,
                checkboxHeader: false,
                toolbar: '#custom-toolbar',
                striped: true
            });
        }
    };

    var cfgColTableFlxCitidocDPL = [{
        field: 'hora', //Columna Dinamica
        title: 'hora',
        sortable: false,
        align: 'left',
    }, {
        field: 'Folder',
        title: 'Folder',
        sortable: false,
        align: 'left',
    }, {
        field: 'document_name',
        title: 'document name.',
        sortable: false,
        align: 'left',
    }, {
        field: 'referencia',
        title: 'referencia',
        sortable: false,
        align: 'left',
    }, {
        field: 'Monto',
        title: 'Monto',
        align: 'right',
    }
    ];

    $('#datatable_flxCitidocDPL').on("load-success.bs.table", function (e, data) {
        if (data.length == 0) {
            $.post(baseUrl + "ControlIntegral/GetListaDeFlxCitidocDPLHora", function (dataResult) {

                $("#lblLog").text("Hora de la ultima carga: " + dataResult);
                $('#datatable_flxCitidocDPL').hide();
                $('#datatable_flxCitidocDPL').bootstrapTable('destroy');
                $("#idDivFrame1").show();
            });
            $("#Frame1").val(true);
        }
        else {
            $("#idDivFrame1").hide();
            $('#datatable_flxCitidocDPL').show();
        }
    });

    function getHeight() {
        return $(window).height() / 2;
    }

    $('#btnLimpiar').click(function (e) {
        e.preventDefault(); //si se llama a este método, no se activará la acción predeterminada del evento
        cmdLimpiar();
    });

    function Limpiar_Resultado() {
        $("#lblNombreClte").text("");
        $("#HiNombreClte").val("");
        $("#txtDV").val("");
        $("#txtBase").val("");
        $("#lblResultado").val("");

        $("#txtCuentab").val("");
        $("#txtNombreb").val("");
        $("#txtBancoib").val("");
        $("#txtBancopb").val("");


        $("#chkContratoFax").prop("checked", false);
        $("#lblChkContratoFax").text("");

        $("#chkContratoMift").prop("checked", false);
        $("#lblChkContratoMift").text("");

        $("#chkfaxNY").prop("checked", false);
        $("#lblChkfaxNY").text("");

        $("#chkCiti").prop("checked", false);
        $("#lblChkCiti").text("");

        $("#lblChkContratoFax").val("");
        $("#lblChkContratoMift").val("");
        $("#lblChkfaxNY").val("");
        $("#lblChkCiti").val("");
        $("#txtRut").val('');

        $("#txtCuentab").val("");
        $("#txtNombreb").val("");
        $("#txtBancoib").val("");
        $("#txtBancopb").val("");

        //$("#lblSegmento").val("");
        $("#lblSegmento").text("");
        $("#HiSegmento").val("");
        //$("#lblEjecutivo").val("");
        $("#lblEjecutivo").text("");
        $("#HiEjecutivo").val("");
        $("#lblMensaje").val("");

        $("#chkContratoFax").prop("checked", false);
        $("#lblChkContratoFax").text("");

        $("#chkContratoMift").prop("checked", false);
        $("#lblChkContratoMift").text("");

        $("#chkfaxNY").prop("checked", false);
        $("#lblChkfaxNY").text("");

        $("#chkCiti").prop("checked", false);
        $("#lblChkCiti").text("");

        $("#Frame1").val(false);

        $('#datatable_flxCitidocDPL').bootstrapTable('destroy');
        $('#datatable_flxTarifas').bootstrapTable('destroy');
        $('#datatable_flxPizarra').bootstrapTable('destroy');

        $("#idDivFrame1").hide();

        $("#Error_original").val("");
        $("#lblNombreClte").hide();


        $("#divLblLog2").hide();
        $("#lblLog2").text("");
    }

    $('#btnReparar').click(function (e) {
        e.preventDefault(); //si se llama a este método, no se activará la acción predeterminada del evento
        if (formConsultaControlIntegralTab1.valid()) {
            var lblNombreClte = $("#lblNombreClte").text();
            var data = formConsultaControlIntegralTab1.serialize();
            $.post(baseUrl + "ControlIntegral/EReparo", data, function (dataResult) {
                if (dataResult != null) {
                    $("#modal_dialog_Reparo_content").html(dataResult);
                    $('#modal_dialog_Reparo').modal('show');
                }
                else
                    bootbox.alert("Debe ingresar un criterio para realizar la busqueda");
            });
        }
    });

    $('#btnCheckList').click(function (e) {
        e.preventDefault(); //si se llama a este método, no se activará la acción predeterminada del evento
        if (formConsultaControlIntegralTab1.valid()) {
            $.post(baseUrl + "ControlIntegral/CheckListControl", function (dataResult) {
                $("#modal_dialog_CheckListControl_content").html(dataResult);
                $('#modal_dialog_CheckListControl').modal('show');
            });
        }
    });

    $('#btnVerRecurrencia').click(function (e) {
        e.preventDefault(); //si se llama a este método, no se activará la acción predeterminada del evento

        if (formConsultaControlIntegralTab1.valid()) {
            var data = formConsultaControlIntegralTab1.serialize();
            var opcion = 1;
            $.post(baseUrl + "ControlIntegral/BotonBuscarRecurrencia", data, function (dataResult) {
                if (dataResult != null) {
                    $.post(baseUrl + "ControlIntegral/Cargar_Recurrencia", { opcion: opcion }, function (dataResult) {
                        if (dataResult != null) {
                            $("#modal_dialog_Detalle_content").html(dataResult);
                            $('#modal_dialog_Detalle').modal('show');
                        }
                    });


                }
            });
        }
    });

    $('#btnTipoCambio').click(function (e) {
        e.preventDefault(); //si se llama a este método, no se activará la acción predeterminada del evento
        if (formConsultaControlIntegralTab1.valid()) {
            $.post(baseUrl + "ControlIntegral/DetalleMesa", function (dataResult) {
                $("#modal_dialog_DetalleMesa_content").html(dataResult);
                $('#modal_dialog_DetalleMesa').modal('show');
            });
        }
    });

    $('#lblEjecutivo').click(function (e) {

        if (formConsultaControlIntegralTab1.valid()) {
            //var location = urlDescargarMailControlIntegral + '?ejecutivo=' + $("#lblEjecutivo").val() + '&segmento=' + $("#lblSegmento").val();
            var location = urlDescargarMailControlIntegral + '?ejecutivo=' + $("#lblEjecutivo").text() + '&segmento=' + $("#lblSegmento").text();
            $.fileDownload(location)
         .done(function () { /* nada que alertar, con la ventanita del browser pidiendo para bajar el archivo alcanza*/ })
         .fail(function () { showAlert("Error en la descarga.", "El archivo del mensaje no se pudo generar", "alert-danger") });

        }
    });

    $('#lblSegmento').click(function (e) {
        if (formConsultaControlIntegralTab1.valid()) {
            //var location = urlDescargarMailControlIntegral + '?ejecutivo=' + $("#lblEjecutivo").val() + '&segmento=' + $("#lblSegmento").val();
            var location = urlDescargarMailControlIntegral + '?ejecutivo=' + $("#lblEjecutivo").text() + '&segmento=' + $("#lblSegmento").text();
            $.fileDownload(location)
         .done(function () { /* nada que alertar, con la ventanita del browser pidiendo para bajar el archivo alcanza*/ })
         .fail(function () { showAlert("Error en la descarga.", "El archivo del mensaje no se pudo generar", "alert-danger") });

        }
    });

    function botonBuscarRecurrencia() {
        if (formConsultaControlIntegralTab1.valid()) {
            var data = formConsultaControlIntegralTab1.serialize();
            var opcion = 1;
            $.post(baseUrl + "ControlIntegral/BotonBuscarRecurrencia", data, function (dataResult) {
                if (dataResult != null) {

                }
            });
        }
    }

    $("#txtCuentab").change(function () {
        if ($(this).val().trim().length == 0) {
            txtCuentab = $("#txtCuentab").val("");
            txtNombreb = $("#txtNombreb").val("");
            txtBancopb = $("#txtBancopb").val("");
        }

        botonBuscarRecurrencia();
    });

    $("#txtCuentab").focus(function () {
        if ($(this).val() != "")
            copiar($(this).val());
    });

    $("#txtNombreb").focus(function () {
        if ($(this).val() != "")
            copiar($(this).val());
    });

    $("#txtBancoib").focus(function () {
        if ($(this).val() != "")
            copiar($(this).val());
    });

    $("#txtBancoib").focus(function () {
        if ($(this).val() != "")
            copiar($(this).val());
    });

    $("#txtBancopb").focus(function () {
        if ($(this).val() != "")
            copiar($(this).val());
    });

    function copiar(str) {
        window.clipboardData.setData('Text', str);
        toastr["success"]("Texto Copiado.", _ToastrTitulo);
    }

    //Tab 2 "Mantenedor Contratos"
    $("#divListaFaxNYM").hide();
    $("#divListaOtrosM").hide();

    //var formConsultaControlIntegralTab2 = $("#frmConsultaControlIntegralTab2");
    //formConsultaControlIntegralTab2.validate({
    //    rules: {
    //        txtRutM: {
    //            number: true
    //        }
    //    },
    //    messages: {
    //        txtRutM: {
    //            number: "Ingrese números.",
    //        }
    //    }
    //});

    //$('#btnCheckList').click(function (e) {
    //    e.preventDefault(); //si se llama a este método, no se activará la acción predeterminada del evento
    //    $.post(baseUrl + "ControlIntegral/CheckListControl", function (dataResult) {
    //        //if (dataResult != null) {
    //        $("#modal_dialog_CheckListControl_content").html(dataResult);
    //        $('#modal_dialog_CheckListControl').modal('show');
    //        //}
    //        //else
    //        //    bootbox.alert("Debe ingresar un criterio para realizar la busqueda");  
    //    });
    //});

    function cmdLimpiarM() {
        var data = formConsultaControlIntegralTab1.serialize();
        $.post(baseUrl + "ControlIntegral/LimpiarM_click", data, function (dataLimpiar) {
            if (dataLimpiar != null) {
                $("#txtRut").val("");
                $("#txtCuentaM").val("");
                $("#txtBaseM").val("");
                $("#txtRutM").val("");
                $("#txtDVM").val("");

                $("#chkContratoFax").prop("checked", false);
                $("#LblchkCitiM").text("");

                $("#chkContratoFaxM").prop("checked", false);
                $("#LblchkContratoFaxM").text("");

                $("#chkContratoMiftM").prop("checked", false);
                $("#LblchkContratoMiftM").text("");

                $("#chkfaxNYM").prop("checked", false);
                $("#LblchkfaxNYM").text("");

                $("#chkMailM").prop("checked", false);
                $("#LblchkMailM").text("");

                $("#chkOtrosM").prop("checked", false);
                $("#LblchkOtrosM").text("");

                $("#lblSegmentoM").text("");
                $("#lblEjecutivoM").text("");

                $("#cmdAgregar").hide();
                $("#cmdModificar").hide();
                $("#divListaFaxNYM").hide();
                $("#divListaOtrosM").hide();


            }
        });
    }

    $('#frmConsultaControlIntegral input[type=checkbox]').change(function () {

        //alert("elem:" + $(this).attr('id') + "value:" + $(this).prop('checked'));
        var elem = $(this).attr('id');
        var value = $(this).prop('checked');
        switch (elem) {

            case "chkContratoFaxM":
                if (value == true)
                    $("#LblchkContratoFaxM").text("CONTRATO VIA FAX FIRMADO");
                else
                    $("#LblchkContratoFaxM").text("SIN CONTRATO CONTRATO FAX LOCAL");
                break;

            case "chkCitiM":
                if (value == true)
                    $("#LblchkCitiM").text("CONTINGENCY MEANS OF COMMUNICATION");
                else
                    $("#LblchkCitiM").text("SIN CONTRATO CONTINGENCY");
                break;

            case "chkContratoMiftM":
                if (value == true)
                    $("#LblchkContratoMiftM").text("CONTRATO CALLBACK OK");
                else
                    $("#LblchkContratoMiftM").text("SIN CONTRATO CALLBACK");
                break;

            case "chkfaxNYM":
                if (value == true) {
                    $("#LblchkfaxNYM").text("Seleccione:");
                    $("#divListaFaxNYM").show();
                }
                else {
                    $("#LblchkfaxNYM").text("NO OPERA CON CITI");
                    $("#divListaFaxNYM").hide();
                }
                break;

            case "chkOtrosM":
                if (value == true) {
                    $("#LblchkOtrosM").text("Seleccione:");
                    $("#divListaOtrosM").show();
                }
                else {
                    $("#LblchkOtrosM").text("SIN CONTRATO OTROS");
                    $("#divListaOtrosM").hide();
                }
                break;

            case "chkMailM":
                if (value == true)
                    $("#LblchkMailM").text("PODER E INSTRUCCIONES PARA REALIZAR OPERACIONES BANCARIAS(MAILS)");
                else
                    $("#LblchkMailM").text("SIN CONTRATO MAIL");
                break;

        }
    });

    $('#cmdBuscarM').click(function (e) {
        e.preventDefault(); //si se llama a este método, no se activará la acción predeterminada del evento

        if (formConsultaControlIntegralTab2.valid()) {

        }

    });

    $('#cmdModificar').click(function (e) {
        e.preventDefault(); //si se llama a este método, no se activará la acción predeterminada del evento

    });

    $('#cmdLimpiarM').click(function (e) {
        e.preventDefault(); //si se llama a este método, no se activará la acción predeterminada del evento
        cmdLimpiarM();
    });





});