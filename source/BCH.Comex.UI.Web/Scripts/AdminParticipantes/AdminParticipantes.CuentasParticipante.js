$(document).ready(function () {
    var baseUrl = $("#base_url").val();
    $('#toolbarMainPrty').hide();
    $('#mnuMainArchivo').hide();
    $('#mnuMainOpciones').hide();

    $('#Lista1_SelectedValue').dblclick(function () {
        var nroCuenta = $('#Lista1_SelectedValue')[0].selectedIndex;
        if (nroCuenta != null) {          
            $.post(baseUrl + "AdminParticipantes/CuentasParticipante_CuentaNacionalVerificaEstado_Click", { nroCuenta: nroCuenta }, function (dataResult) {
                if (dataResult != null) {    
                    if (dataResult.idEstadoMensaje == 0) {                       
                        ModalMonedaNacional();
                    } else if (dataResult.idEstadoMensaje == 1) { //tipo_cliente
                        bootbox.confirm("Cuenta Corriente en proceso de Borrado. Si desea reactivar la Cuenta Corriente debería elegir SI y podrá acceder a toda su información. Si elige NO volverá a la lista de Cuentas Corrientes.", function (result) {
                            if (result) {
                                $.post(baseUrl + "AdminParticipantes/CuentasParticipante_CuentaNacional_TipoCliente_Click", { nroCuenta: nroCuenta }, function (dataResultTC) {
                                    if (dataResultTC != null) {
                                        // ModalMonedaNacional();
                                        $("#modalSrchPart_Content").html(dataResultTC);
                                        $('#modalSrchPart').modal('show');
                                        activarControlCTAS();
                                    }
                                });
                            }
                            else {
                                return;
                            }
                        });
                    } else if (dataResult.idEstadoMensaje == 2) { //tipo_banco  
                        bootbox.confirm("Cuenta Corriente en proceso de Borrado. Si desea reactivar la Cuenta Corriente debería elegir SI y podrá acceder a toda su información. Si elige NO volverá a la lista de Cuentas Corrientes.", function (result) {
                            if (result) {
                                $.post(baseUrl + "AdminParticipantes/CuentasParticipante_CuentaNacional_TipoCliente_Click", { nroCuenta: nroCuenta }, function (dataResultTB) {
                                    if (dataResultTB != null) {
                                        // ModalMonedaNacional();
                                        $("#modalSrchPart_Content").html(dataResultTB);
                                        $('#modalSrchPart').modal('show');
                                        activarControlCTAS();
                                    }
                                });
                            } else {
                                return;
                            }
                        });
                    }
                }
            });
        }
    });

    function ModalMonedaNacional() {
        var nroCuenta = $('#Lista1_SelectedValue')[0].selectedIndex;
        //alert(nroCuenta);
        $.post(baseUrl + "AdminParticipantes/CuentasParticipante_CuentaNacional_Click", { nroCuenta: nroCuenta }, function (dataResult) {
            if (dataResult != null) {
                $("#modalSrchPart_Content").html(dataResult);
                $('#modalSrchPart').modal('show');
                activarControlCTAS();
            }
        });
    }

    $('#Lista2_SelectedValue').dblclick(function () {
        var nroCuenta = $('#Lista2_SelectedValue')[0].selectedIndex;
        if (nroCuenta != null) {
            $.post(baseUrl + "AdminParticipantes/CuentasParticipante_CuentaExtranjeraVerificaEstado_Click", { nroCuenta: nroCuenta }, function (dataResult) {
                if (dataResult != null) {

                    if (dataResult.idEstadoMensaje == 0) {
                        ModalMonedaExtranjera();
                    } else if (dataResult.idEstadoMensaje == 1) { //Cuenta Corriente                    
                        bootbox.confirm("Cuenta Corriente en proceso de Borrado. Si desea reactivar la Cuenta Corriente debería elegir SI y podrá acceder a toda su información. Si elige NO volverá a la lista de Cuentas Corrientes.", function (result) {
                            if (result) {
                                $.post(baseUrl + "AdminParticipantes/CuentasParticipante_CuentaExtranjera_CuentaCorriente_Si_Click", { nroCuenta: nroCuenta }, function (dataResultCC) {
                                    if (dataResultCC != null) {

                                        $("#modalSrchPart_Content").html(dataResultCC);
                                        $('#modalSrchPart').modal('show');
                                        activarControlCTAS();
                                    }
                                });
                            } else {
                                return;
                            }
                        });
                    }
                    else if (dataResult.idEstadoMensaje == 2) { //Linea de Credito                    
                        bootbox.confirm("Línea de Crédito en proceso de Borrado. Si desea reactivar la Línea de Crédito debería elegir SI y podrá acceder a toda su información. Si elige NO volverá a la lista de Líneas de Crédito.", function (result) {
                            if (result) {
                                $.post(baseUrl + "AdminParticipantes/CuentasParticipante_CuentaExtranjera_CuentaCorriente_Si_Click", { nroCuenta: nroCuenta }, function (dataResultLC) {
                                    if (dataResultLC != null) {
                                        $("#modalSrchPart_Content").html(dataResultLC);
                                        $('#modalSrchPart').modal('show');
                                        activarControlCTAS();
                                    }
                                });
                            } else {
                                return;
                            }
                        });
                    }
                }
            });
        }
    });

    function ModalMonedaExtranjera() {
        var nroCuenta = $('#Lista2_SelectedValue')[0].selectedIndex;
        //alert(nroCuenta);
        $.post(baseUrl + "AdminParticipantes/CuentasParticipante_CuentaExtranjera_Click", { nroCuenta: nroCuenta }, function (dataResult) {
            if (dataResult != null) {
                $("#modalSrchPart_Content").html(dataResult);
                $('#modalSrchPart').modal('show');
                activarControlCTAS();
            }
        });
    }

    function activarControlCTAS() {
        var msj = '<div class="alert alert-warning"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>Atención!</strong> No se encontraron cuentas para habilitar el control.</div>';
        if ($('#cbo_cta_SelectedValue > option').length > 1) {
            $('#cbo_cta_SelectedValue').prop('disabled', false);
        } else {
            $('#cbo_cta_SelectedValue').prop('disabled', true).after(msj);
        }
    }

    // Dejar marcado siempre el primer elemento
    $('#Lista1_SelectedValue').val(0);
    $('#Lista1_SelectedValue').val(0);
    $('#Lista2_SelectedValue').val(0);
    $('#Lista2_SelectedValue').val(0);
});