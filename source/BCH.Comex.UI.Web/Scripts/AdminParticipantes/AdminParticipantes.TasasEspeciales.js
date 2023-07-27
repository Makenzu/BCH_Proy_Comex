$(document).ready(function () {
    var baseUrl = $("#base_url").val();
    var usoDecimales = 2;
    var formulario = $("#frmTasaEspecialesParticipante");
    $('#toolbarMainPrty').hide();
    $('#mnuMainArchivo').hide();
    $('#mnuMainOpciones').hide();
    setMascara();

    function setMascara() {
        $("#MontoGastos_Text").AppAdminParticipantes().initMascaraMonto(usoDecimales);
        $("#TasaInteres_Text").AppAdminParticipantes().initMascaraTasa();
    }

    $('#ListaComision_SelectedValue').dblclick(function () {
        var selectedValue = $('#ListaComision_SelectedValue').val();
        if (selectedValue != null) {
            $.post(baseUrl + "AdminParticipantes/TasaEspeciales_ListaComision_Click", { selectedValue: selectedValue }, function (dataResult) {
                if (dataResult != null) {
                    $("#modalTasaEspecial_Content").html(dataResult);
                    $('#modalTasaEspecial').modal('show');
                }
            });
        }
    });

    $("#TasaInteres_Text").blur(function () {
        //var string = numeral($("#TasaInteres_Text").val()).format("##,###0.000000");
        //$("#TasaInteres_Text").val(string);

        var tasaInteres = $("#TasaInteres_Text").val();
        $.post(baseUrl + "AdminParticipantes/TasaEspeciales_TasaInteres_Change", { tasaInteres: tasaInteres }, function (dataResult) {
            if (dataResult != null) {
                //  $("#MontoGastos_Text").val(string);
            }
        });
    });

    $('#ListaModulo_SelectedValue').dblclick(function () {
        var selectedValue = $('#ListaModulo_SelectedValue').val();
        var Cb_Comision = $("#ListaComision_SelectedValue");
        if (selectedValue != null) {
            $.post(baseUrl + "AdminParticipantes/TasaEspeciales_ListaModulo_Click", { selectedValue: selectedValue }, function (dataResult) {
                if (dataResult != null) {
                    if (dataResult.EstadoMsjeConfirmacion == 0) {
                        escribecom(dataResult);
                        escribeint(dataResult);
                        escribegas(dataResult);
                        seteObjetosBD(dataResult);
                        habilitaDeshabilitaBotones(dataResult);
                        escribelista(dataResult);
                        escribetitulo(dataResult);
                        //alert(dataResult.BtnAgregar.Text);
                        //$("#BtnAgregar").text(dataResult.BtnAgregar.Text);

                    } else if (dataResult.EstadoMsjeConfirmacion == 1) {
                        bootbox.confirm("Tasa Especial en proceso de Borrado." + " " + "Si desea reactivar la Tasa Especial" + " " + "debería elegir SI y podrá acceder a toda su información." + "Si elige NO volverá a la lista de Tasas Especiales.", function (result) {

                            if (result) {
                                $.post(baseUrl + "AdminParticipantes/TasaEspeciales_ListaModulo_Si_No_Click", { result: result }, function (dataResultSiNo) {
                                    if (dataResultSiNo != null) {
                                        if (result) {
                                            escribecom(dataResultSiNo);
                                            escribeint(dataResultSiNo);
                                            escribegas(dataResultSiNo);
                                            habilitaDeshabilitaBotones(dataResultSiNo);
                                            seteObjetosBD(dataResultSiNo);
                                            escribelista(dataResultSiNo);
                                            escribetitulo(dataResultSiNo);
                                        } else {
                                            limpiacom(dataResultSiNo);
                                            limpiagas(dataResultSiNo);
                                            limpiaint(dataResultSiNo);

                                            var ListaComision = $('#ListaComision_SelectedValue');
                                            ListaComision.val(dataResultSiNo.ListaComision.ListIndex);

                                            var botonAgregar = $('#BtnAgregar');
                                            botonAgregar.val(dataResultSiNo.BtnAgregar.Text);

                                            if (dataResultSiNo.BtnAgregar.Enabled)
                                                botonAgregar.removeAttr('disabled');
                                            else
                                                botonAgregar.attr('disabled', 'disabled');

                                            var botonEliminar = $('#BtnEliminar');
                                            if (dataResultSiNo.BtnEliminar.Enabled)
                                                botonEliminar.removeAttr('disabled');
                                            else
                                                botonEliminar.attr('disabled', 'disabled');
                                            seteObjetosBD(dataResultSiNo);
                                            escribetitulo(dataResultSiNo);
                                        }
                                    }
                                });
                            }
                        });
                    }
                }
            });
        }
    });

    $("#tarifa_Checked").click(function () {
        $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Tarifa_Click", { elem: $(this).attr('id'), value: $(this).prop('checked') }, function (dataResult) {
            if (dataResult != null) {
                if (dataResult.tarifa.Checked) {
                    $('#MontoGastos_Text').val('');
                    $('#MontoGastos_Text').attr('disabled', 'disabled');
                } else {
                    $('#MontoGastos_Text').removeAttr('disabled');
                }
            }
        });
    });

    $('#prtTipoInteres input').change(function () {
        var value = $('#prtTipoInteres input:checked').val();
        // alert(value);
        $.ajax({
            url: baseUrl + "AdminParticipantes/TasaEspeciales_TipoInteres_Click",
            method: "POST",
            data: { selectedValue: value },
            success: function (data) {
                var botonAgregar = $('#BtnAgregar');

                if (data.BtnAgregar.Enabled)
                    botonAgregar.removeAttr('disabled');
                else
                    botonAgregar.attr('disabled', 'disabled');

                var InteresList = $('input:radio[name=prtTipoInteres]');

                $.each(data.prtTipoInteres, function (index, item) {
                    InteresList[index].checked = item.Selected;
                    InteresList[index].disabled = item.Enabled ? false : true
                });

                $("#Flotante_Checked").attr('checked', data.Flotante.Checked);
                $("#Flotante_Checked").prop("disabled", data.Flotante.Enabled ? false : true);
            },
            error: function (data) {
                //alert('Ha ocurrido un error');
            }
        });
    });

    $("#MontoGastos_Text").blur(function () {
        // var string = numeral($("#TasaInteres_Text").val()).format("##,###0.000000");
        var montoGastos = $("#MontoGastos_Text").val();
        $.post(baseUrl + "AdminParticipantes/TasaEspeciales_MontoGastos_Change", { montoGastos: montoGastos }, function (dataResult) {
            if (dataResult != null) {
                //  $("#MontoGastos_Text").val(string);
            }
        });
    });

    function habilitaDeshabilitaBotones(dataResult) {
        var botonAceptar = $('#BtnAceptar');
        if (dataResult.BtnAceptar.Enabled)
            botonAceptar.removeAttr('disabled');
        else
            botonAceptar.attr('disabled', 'disabled');

        var botonCancelar = $('#Btncancelar');
        if (dataResult.Btncancelar.Enabled)
            botonCancelar.removeAttr('disabled');
        else
            botonCancelar.attr('disabled', 'disabled');

        //alert(dataResult.BtnAgregar.Text);
        var botonAgregar = $('#BtnAgregar');
        botonAgregar.val(dataResult.BtnAgregar.Text);
        if (dataResult.BtnAgregar.Enabled)
            botonAgregar.removeAttr('disabled');
        else
            botonAgregar.attr('disabled', 'disabled');

        var botonEliminar = $('#BtnEliminar');
        if (dataResult.BtnEliminar.Enabled)
            botonEliminar.removeAttr('disabled');
        else
            botonEliminar.attr('disabled', 'disabled');
    }

    function escribelista(dataResult) {
        var cb_Modulo = $('#ListaModulo_SelectedValue');
        var traeLista = dataResult.ListaModulo.Items;
        cb_Modulo.html('');
        $.each(traeLista, function (id, option) {
            cb_Modulo.append($('<option></option>').val(option.Data).html(option.Value));
        });
    }

    function seteObjetosBD(dataResult) {
        //INTERES
        //$('#TasaInteres_Text').val(dataResult.TasaInteres.Text);
        //$("#TasaInteres_Text").prop("disabled", dataResult.TasaInteres.Enabled ? false : true);

        //var InteresList = $('input:radio[name=prtTipoInteres]');
        ////alert(TasaList);
        //$.each(dataResult.prtTipoInteres, function (index, item) {
        //    InteresList[index].checked = item.Selected;
        //    InteresList[index].disabled = item.Enabled ? false : true
        //});

        //$("#Flotante_Checked").attr('checked', dataResult.Flotante.Checked);
        //$("#Flotante_Checked").prop("disabled", dataResult.Flotante.Enabled ? false : true);

        ////GASTOS
        //$('#MontoGastos_Text').val(dataResult.MontoGastos.Text);
        //$("#MontoGastos_Text").prop("disabled", dataResult.MontoGastos.Enabled ? false : true);
        //$("#tarifa_Checked").attr('checked', dataResult.tarifa.Checked);
        //$("#tarifa_Checked").prop("disabled", dataResult.tarifa.Enabled ? false : true);
    }

    //Importaciones

    $("#etacobComisionPago, #etacobAnulacion, #etacobTraspasoOtrosBancos, #etacobProrroga, #etacobMantencion").click(function () {
        // var listItem = //$("input").index(this);
        var tab = -1;
        tab = $(this).attr('tabindex');
        tab++;
        var Etapa = (tab - 1);
        //alert(Etapa);
        var data = formulario.serialize();

        $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Importaciones_Cobranza", { Etapa: Etapa }, function (dataResult) {

            if (dataResult != null) {
                if (dataResult.idEstadoMsjeExportacion == 0) {
                    escribecom(dataResult);
                    //limpiaint(dataResult);
                    escribegas(dataResult);
                    limpiaint(dataResult);
                    escribetitulo(dataResult);

                } else if (dataResult.idEstadoMsjeExportacion == 1) {

                    bootbox.confirm("Tasa Especial en proceso de Borrado. Si desea reactivar la Tasa Especial debería elegir SI y podrá acceder a toda su información. Si elige NO volverá a la lista de Tasas Especiales.", function (result) {

                        if (result) {
                            data = formulario.serialize();
                            $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Importaciones_Cobranza_Si", { Etapa: Etapa }, function (dataResultSi) {
                                if (dataResultSi != null) {
                                    escribecom(dataResultSi);
                                    limpiaint(dataResultSi);
                                    escribegas(dataResultSi);
                                    escribetitulo(dataResultSi);
                                }
                            });
                        } else {
                            return;
                        }
                    });
                }
            }
        });
    });

    $("#etacarApertura, #etacarModificacion1, #etacarModificacion2, #etacarAnulacionSaldo, #etacarNegociacionCobertura, #etacarNegociacionVencimiento, #etacarProrroga").click(function () {

        var tab = -1;
        tab = $(this).attr('tabindex');
        tab++;
        var Etapa = (tab - 1);
        //alert(Etapa);
        var data = formulario.serialize();

        $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Importaciones_CartaCredito", { Etapa: Etapa }, function (dataResult) {
            if (dataResult != null) {
                if (dataResult.idEstadoMsjeExportacion == 0) {

                    if (Etapa == 0) {
                        escribecom(dataResult);
                        escribeint(dataResult);
                        escribegas(dataResult);
                        escribetitulo(dataResult);
                    }
                    else if (Etapa == 1 || Etapa == 2) {
                        escribecom(dataResult);
                        limpiaint(dataResult);
                        escribegas(dataResult);
                        escribetitulo(dataResult);
                    }
                    else if (Etapa == 3) {
                        limpiacom(dataResult);
                        escribeint(dataResult);
                        escribegas(dataResult);
                        escribetitulo(dataResult);
                    }
                    else if (Etapa == 4 || Etapa == 5) {
                        limpiacom(dataResult);
                        escribeint(dataResult);
                        limpiagas(dataResult);
                        escribetitulo(dataResult);
                    }
                    else if (Etapa == 6) {
                        escribecom(dataResult);
                        limpiaint(dataResult);
                        escribegas(dataResult);
                        escribetitulo(dataResult);
                    }
                    //alert(dataResult.BtnAgregar.Text);
                } else if (dataResult.idEstadoMsjeExportacion == 1) {

                    bootbox.confirm("Tasa Especial en proceso de Borrado. Si desea reactivar la Tasa Especial debería elegir SI y podrá acceder a toda su información. Si elige NO volverá a la lista de Tasas Especiales.", function (result) {

                        if (result) {

                            //  alert(result);
                            //data = formulario.serialize();
                            $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Importaciones_CartaCredito_Si", { Etapa: Etapa }, function (dataResultSi) {
                                if (dataResultSi != null) {
                                    if (Etapa == 0) {
                                        escribecom(dataResultSi);
                                        escribeint(dataResultSi);
                                        escribegas(dataResultSi);
                                        escribetitulo(dataResultSi);
                                    }
                                    else if (Etapa == 1 || Etapa == 2) {
                                        escribecom(dataResultSi);
                                        limpiaint(dataResultSi);
                                        escribegas(dataResultSi);
                                        escribetitulo(dataResultSi);
                                    }
                                    else if (Etapa == 3) {
                                        limpiacom(dataResultSi);
                                        escribeint(dataResultSi);
                                        escribegas(dataResultSi);
                                        escribetitulo(dataResultSi);
                                    }
                                    else if (Etapa == 4 || Etapa == 5) {
                                        limpiacom(dataResultSi);
                                        escribeint(dataResultSi);
                                        limpiagas(dataResultSi);
                                        escribetitulo(dataResultSi);
                                    }
                                    else if (Etapa == 6) {
                                        escribecom(dataResultSi);
                                        limpiaint(dataResultSi);
                                        escribegas(dataResultSi);
                                        escribetitulo(dataResultSi);
                                    }
                                }
                            });
                        } else {
                            return;
                        }

                    });
                }

            }

        });
    });

    //Exportaciones
    $("#etapaeIngreso").click(function () {
        //var data = formulario.serialize();
        $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Exportaciones_Nivel2_Ingreso", function (dataResult) {

            if (dataResult != null) {
                if (dataResult.idEstadoMsjeExportacion == 0) {
                    escribecom(dataResult);
                    escribeint(dataResult);
                    limpiagas(dataResult);
                    escribetitulo(dataResult);
                } else if (dataResult.idEstadoMsjeExportacion == 1) {

                    bootbox.confirm("Tasa Especial en proceso de Borrado. Si desea reactivar la Tasa Especial debería elegir SI y podrá acceder a toda su información Si elige NO volverá a la lista de Tasas Especiales.", function (result) {

                        if (result) {
                            // data = formulario.serialize();
                            $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Exportaciones_Nivel2_Ingreso_Si", function (dataResultSi) {
                                if (dataResultSi != null) {
                                    escribecom(dataResultSi);
                                    escribeint(dataResultSi);
                                    limpiagas(dataResultSi);
                                    escribetitulo(dataResultSi);
                                }
                            });
                        } else {
                            return;
                        }

                    });
                }
            }
        });
    });

    $("#etacomPais, #etacomAladi, #etacomOtrosPaises").click(function () {
        // var listItem = //$("input").index(this);
        var tab = -1;
        tab = $(this).attr('tabindex');
        tab++;
        var Etapa = (tab - 1);
        //alert(Etapa);
        var data = formulario.serialize();

        $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Exportaciones_Nivel2_GestionCompraDocumentos", { Etapa: Etapa }, function (dataResult) {

            if (dataResult != null) {
                if (dataResult.idEstadoMsjeExportacion == 0) {
                    escribecom(dataResult);
                    escribeint(dataResult);
                    limpiagas(dataResult);
                    escribetitulo(dataResult);
                } else if (dataResult.idEstadoMsjeExportacion == 1) {

                    bootbox.confirm("Tasa Especial en proceso de Borrado. Si desea reactivar la Tasa Especial debería elegir SI y podrá acceder a toda su información. Si elige NO volverá a la lista de Tasas Especiales.", function (result) {

                        if (result) {
                            data = formulario.serialize();
                            $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Exportaciones_Nivel2_GestionCompraDocumentos_Si", { Etapa: Etapa }, function (dataResultSi) {
                                if (dataResultSi != null) {
                                    escribecom(dataResultSi);
                                    escribeint(dataResultSi);
                                    limpiagas(dataResultSi);
                                    escribetitulo(dataResultSi);
                                }
                            });
                        } else {
                            return;
                        }
                    });
                }
            }
        });
    });


    $("#etadesPais, #etadesAladi, #etadesOtrosPaises").click(function () {
        var tab = -1;
        tab = $(this).attr('tabindex');
        tab++;
        var Etapa = (tab - 1);
        //alert(Etapa);
        var data = formulario.serialize();
        $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Exportaciones_Nivel2_GestionDescuentoDocumentos", { Etapa: Etapa }, function (dataResult) {

            if (dataResult != null) {
                if (dataResult.idEstadoMsjeExportacion == 0) {
                    escribecom(dataResult);
                    limpiaint(dataResult);
                    limpiagas(dataResult);
                    escribetitulo(dataResult);
                } else if (dataResult.idEstadoMsjeExportacion == 1) {

                    bootbox.confirm("Tasa Especial en proceso de Borrado. Si desea reactivar la Tasa Especial debería elegir SI y podrá acceder a toda su información. Si elige NO volverá a la lista de Tasas Especiales.", function (result) {

                        if (result) {
                            data = formulario.serialize();
                            $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Exportaciones_Nivel2_GestionDescuentoDocumentos_Si", { Etapa: Etapa }, function (dataResultSi) {
                                if (dataResultSi != null) {
                                    escribecom(dataResultSi);
                                    limpiaint(dataResultSi);
                                    limpiagas(dataResultSi);
                                    escribetitulo(dataResultSi);
                                }
                            });

                        } else {
                            return;
                        }
                    });
                }
            }
        });
    });


    $("#etacredAviso, #etacredConfirmacion, #etacredMotivacion, #etacredTransferencia, #etacredNoUtilizacion, #etacredPago, #etacredRevision, #etacredTraspaso, #etacredAvisoBenificiario").click(function () {

        var tab = -1;
        tab = $(this).attr('tabindex');
        tab++;
        var Etapa = (tab - 1);
        //alert(Etapa);
        //var data = formulario.serialize();

        $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Exportaciones_Nivel2_CartaCredito", { Etapa: Etapa }, function (dataResult) {
            if (dataResult != null) {
                if (dataResult.idEstadoMsjeExportacion == 0) {
                    escribecom(dataResult);
                    limpiaint(dataResult);
                    escribegas(dataResult);
                    escribetitulo(dataResult);
                } else if (dataResult.idEstadoMsjeExportacion == 1) {

                    bootbox.confirm("Tasa Especial en proceso de Borrado. Si desea reactivar la Tasa Especial debería elegir SI y podrá acceder a toda su información. Si elige NO volverá a la lista de Tasas Especiales.", function (result) {

                        if (result) {
                            //data = formulario.serialize();
                            $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Exportaciones_Nivel2_CartaCredito_Si", { Etapa: Etapa }, function (dataResultSi) {
                                if (dataResultSi != null) {
                                    escribecom(dataResultSi);
                                    limpiaint(dataResultSi);
                                    escribegas(dataResultSi);
                                    escribetitulo(dataResultSi);
                                }
                            });

                        } else  {
                            return;
                        }
                    });
                }
            }
        });
    });

    $("#prodexpPagoAnticipado").click(function () {
        var data = formulario.serialize();
        $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Exportaciones", data, function (dataResult) {
            if (dataResult != null) {
                if (dataResult.idEstadoMsjeExportacion == 0) {
                    limpiacom(dataResult);
                    escribeint(dataResult);
                    limpiagas(dataResult);
                    habilitaDeshabilitaBotones(dataResult);
                    escribetitulo(dataResult);
                } else if (dataResult.idEstadoMsjeExportacion == 1) {
                    bootbox.confirm("Tasa Especial en proceso de Borrado. Si desea reactivar la Tasa Especial debería elegir SI y podrá acceder a toda su información.Si elige NO volverá a la lista de Tasas Especiales.", function (result) {

                        if (result == true) {
                            $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Exportaciones_Si", function (dataResultSi) {
                                if (dataResultSi != null) {
                                    //ObjetosExportacion(dataResultSi);
                                    limpiacom(dataResult);
                                    escribeint(dataResult);
                                    limpiagas(dataResult);
                                    habilitaDeshabilitaBotones(dataResult);
                                    escribetitulo(dataResult);
                                }
                            });
                        } else {
                            return;
                        }
                    });
                }
            }
        });
    });


    $("#etapaManejo, #etapaMensaje").click(function () {
        var tab = -1;
        tab = $(this).attr('tabindex');
        tab++;
        var Etapa = (tab - 1);
        // alert(Etapa);
        //var data = formulario.serialize();
        $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Exportaciones_Nivel2_Cobranza", { Etapa: Etapa }, function (dataResult) {
            if (dataResult != null) {
                if (dataResult.idEstadoMsjeExportacion == 0) {
                    if (Etapa == 0) {
                        escribecom(dataResult);
                        limpiaint(dataResult);
                        escribegas(dataResult);
                    } else {
                        limpiacom(dataResult);
                        limpiaint(dataResult);
                        escribegas(dataResult);
                    }

                    escribetitulo(dataResult);
                }
                else if (dataResult.idEstadoMsjeExportacion == 1) {

                    bootbox.confirm("Tasa Especial en proceso de Borrado. Si desea reactivar la Tasa Especial debería elegir SI y podrá acceder a toda su información. Si elige NO volverá a la lista de Tasas Especiales.", function (result) {
                        if (result) {
                            //data = formulario.serialize();
                            $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Exportaciones_Nivel2_Cobranza_Si", { Etapa: Etapa }, function (dataResultSi) {
                                if (dataResultSi != null) {
                                    if (Etapa == 0) {
                                        escribecom(dataResultSi);
                                        limpiaint(dataResultSi);
                                        escribegas(dataResultSi);
                                    }
                                    else {
                                        limpiacom(dataResultSi);
                                        limpiaint(dataResultSi);
                                        escribegas(dataResultSi);

                                    }

                                    escribetitulo(dataResultSi);
                                }
                            });
                        } else {
                            return;
                        }
                    });
                }
            }
        });
    });

    $("#etaretTraspaso, #etaretAdicion").click(function () {

        var tab = -1;
        tab = $(this).attr('tabindex');
        tab++;
        var Etapa = (tab - 1);
        //alert(Etapa);
        //var data = formulario.serialize();

        $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Exportaciones_Nivel2_Retorno", { Etapa: Etapa }, function (dataResult) {
            if (dataResult != null) {
                if (dataResult.idEstadoMsjeExportacion == 0) {
                    escribecom(dataResult);
                    limpiaint(dataResult);
                    limpiagas(dataResult);
                    escribetitulo(dataResult);
                } else if (dataResult.idEstadoMsjeExportacion == 1) {

                    bootbox.confirm("Tasa Especial en proceso de Borrado. Si desea reactivar la Tasa Especial debería elegir SI y podrá acceder a toda su información. Si elige NO volverá a la lista de Tasas Especiales.", function (result) {

                        if (result) {
                            //data = formulario.serialize();
                            $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Exportaciones_Nivel2_Retorno_Si", { Etapa: Etapa }, function (dataResultSi) {
                                if (dataResultSi != null) {
                                    escribecom(dataResultSi);
                                    limpiaint(dataResultSi);
                                    limpiagas(dataResultSi);
                                    escribetitulo(dataResultSi);
                                }
                            });
                        } else {
                            return;
                        }
                    });
                }
            }
        });
    });

    //Servicios
    $("#prodserEndosoPlanilla, #prodserTraspasoIDI, #prodserCoberturaFlote, #prodserVentaDivisas").click(function () {
        var tab = -1;
        tab = $(this).attr('tabindex');
        tab++;
        var Etapa = (tab - 1);
        //var data = formulario.serialize();

        $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Servicios_Nivel1", { Etapa: Etapa }, function (dataResult) {
            if (dataResult != null) {
                if (dataResult.idEstadoMsjeExportacion == 0) {
                    escribecom(dataResult);
                    limpiaint(dataResult);
                    limpiagas(dataResult);
                    //seteObjetosBD(dataResult);
                    escribetitulo(dataResult);
                }
                else if (dataResult.idEstadoMsjeExportacion == 1) {

                    bootbox.confirm("Tasa Especial en proceso de Borrado. Si desea reactivar la Tasa Especial debería elegir SI y podrá acceder a toda su información. Si elige NO volverá a la lista de Tasas Especiales.", function (result) {

                        if (result) {
                            //data = formulario.serialize();
                            $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Servicios_Nivel1_Si", { Etapa: Etapa }, function (dataResultSi) {
                                if (dataResultSi != null) {
                                    escribecom(dataResultSi);
                                    limpiaint(dataResultSi);
                                    limpiagas(dataResultSi);
                                    escribetitulo(dataResultSi);
                                }
                            });
                        } else {
                            return;
                        }
                    });
                }
            }
        });
    });

    $("#exFinanciamientoProceso, #exFinanciamientoAnulacion").click(function () {
        var tab = -1;
        tab = $(this).attr('tabindex');
        tab++;
        var Etapa = (tab - 1);
        //var data = formulario.serialize();

        $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Servicios_Nivel2_Ex_Financiamiento", { Etapa: Etapa }, function (dataResult) {
            if (dataResult != null) {
                if (dataResult.idEstadoMsjeExportacion == 0) {
                    escribecom(dataResult);
                    limpiaint(dataResult);
                    limpiagas(dataResult);
                    escribetitulo(dataResult);
                } else if (dataResult.idEstadoMsjeExportacion == 1) {

                    bootbox.confirm("Tasa Especial en proceso de Borrado. Si desea reactivar la Tasa Especial debería elegir SI y podrá acceder a toda su información. Si elige NO volverá a la lista de Tasas Especiales.", function (result) {

                        if (result) {
                            //data = formulario.serialize();
                            $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Servicios_Nivel2_Ex_Financiamiento_Si", { Etapa: Etapa }, function (dataResultSi) {
                                if (dataResultSi != null) {
                                    escribecom(dataResultSi);
                                    limpiaint(dataResultSi);
                                    limpiagas(dataResultSi);
                                    escribetitulo(dataResultSi);
                                }
                            });
                        } else {
                            return;
                        }
                    });
                }
            }
        });
    });

    $("#EtaordenPagoFinanAviso, #EtaordenPagoFinanPago").click(function () {
        var tab = -1;
        tab = $(this).attr('tabindex');
        tab++;
        var Etapa = (tab - 1);

        $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Servicios_Nivel2_Orden_Pago_Condicionado", { Etapa: Etapa }, function (dataResult) {
            if (dataResult != null) {
                if (dataResult.idEstadoMsjeExportacion == 0) {
                    escribecom(dataResult);
                    limpiaint(dataResult);
                    limpiagas(dataResult);
                    escribetitulo(dataResult);
                }
                else if (dataResult.idEstadoMsjeExportacion == 1) {

                    bootbox.confirm("Tasa Especial en proceso de Borrado. Si desea reactivar la Tasa Especial debería elegir SI y podrá acceder a toda su información. Si elige NO volverá a la lista de Tasas Especiales.", function (result) {

                        if (result) {
                            //data = formulario.serialize();
                            $.post(baseUrl + "AdminParticipantes/TasaEspeciales_Menu_Servicios_Nivel2_Orden_Pago_Condicionado_Si", { Etapa: Etapa }, function (dataResultSi) {
                                if (dataResultSi != null) {
                                    escribecom(dataResultSi);
                                    limpiaint(dataResultSi);
                                    limpiagas(dataResultSi);
                                    escribetitulo(dataResultSi);
                                }
                            });
                        } else {
                            return;
                        }
                    });
                }
            }
        });
    });

    function limpiacom(dataResult) {
        var ListaComision = $('#ListaComision_SelectedValue');
        ListaComision.html("");

        if (dataResult.ListaComision.Enabled)
            ListaComision.removeAttr('disabled');
        else
            ListaComision.attr('disabled', 'disabled');
    }

    function escribeint(dataResult) {
        //INTERES
        $('#TasaInteres_Text').val(dataResult.TasaInteres.Text);
        $("#TasaInteres_Text").prop("disabled", dataResult.TasaInteres.Enabled ? false : true);

        var InteresList = $('input:radio[name=prtTipoInteres]');

        $.each(dataResult.prtTipoInteres, function (index, item) {
            InteresList[index].checked = item.Selected;
            InteresList[index].disabled = item.Enabled ? false : true
        });

        $("#Flotante_Checked").attr('checked', dataResult.Flotante.Checked);
        $("#Flotante_Checked").prop("disabled", dataResult.Flotante.Enabled ? false : true);
    }

    function escribecom(dataResult) {
        var botonAgregar = $('#BtnAgregar');
        botonAgregar.val(dataResult.BtnAgregar.Text);

        if (dataResult.BtnAgregar.Enabled)
            botonAgregar.removeAttr('disabled');
        else
            botonAgregar.attr('disabled', 'disabled');

        var botonEliminar = $('#BtnEliminar');
        if (dataResult.BtnEliminar.Enabled)
            botonEliminar.removeAttr('disabled');
        else
            botonEliminar.attr('disabled', 'disabled');

        var ListaComision = $('#ListaComision_SelectedValue');
        var traeLista = dataResult.ListaComision.Items;
        ListaComision.html('');

        $.each(traeLista, function (id, option) {
            ListaComision.append($('<option></option>').val(option.Data).html(option.Value));
        });

        ListaComision.val(dataResult.ListaComision.ListIndex);
        prtcomision(dataResult);
    }

    function escribegas(dataResult) {
        $('#MontoGastos_Text').val(dataResult.MontoGastos.Text);
        $("#MontoGastos_Text").prop("disabled", dataResult.MontoGastos.Enabled ? false : true);

        $("#tarifa_Checked").attr('checked', dataResult.tarifa.Checked);
        $("#tarifa_Checked").prop("disabled", dataResult.tarifa.Enabled ? false : true);

        var botonAgregar = $('#BtnAgregar');
        botonAgregar.val(dataResult.BtnAgregar.Text);

        var botonEliminar = $('#BtnEliminar');
        if (dataResult.BtnEliminar.Enabled)
            botonEliminar.removeAttr('disabled');
        else
            botonEliminar.attr('disabled', 'disabled');
    }

    function escribetitulo(dataResult) {
        var Titulo = $('#Titulo');
        Titulo.text(dataResult.Titulo.Text);
    }

    function prtcomision(dataResult) {
        var ListaComision = $('#ListaComision_SelectedValue');
        if (dataResult.ListaComision.Enabled)
            ListaComision.removeAttr('disabled');
        else
            ListaComision.attr('disabled', 'disabled');
    }

    function limpiagas(dataResult) {
        //GASTOS
        $('#MontoGastos_Text').val(dataResult.MontoGastos.Text);
        //$("#MontoGastos_Text").prop("disabled", dataResult.MontoGastos.Enabled ? false : true);
        $("#MontoGastos_Text").prop("disabled", true);
        $("#tarifa_Checked").attr('checked', dataResult.tarifa.Checked);
        $("#tarifa_Checked").prop("disabled", dataResult.tarifa.Enabled ? false : true);

    }

    function prtGasto(dataResult) {
        $('#MontoGastos_Text').val(dataResult.MontoGastos.Text);
        $("#MontoGastos_Text").prop("disabled", dataResult.MontoGastos.Enabled ? false : true);
    }

    //Funciones
    function limpiaint(dataResult) {
        $('#TasaInteres_Text').val(dataResult.TasaInteres.Text);
        //$("#TasaInteres_Text").prop("disabled", dataResult.TasaInteres.Enabled ? false : true);
        $("#TasaInteres_Text").prop("disabled", true);

        var InteresList = $('input:radio[name=prtTipoInteres]');
        $.each(dataResult.prtTipoInteres, function (index, item) {
            InteresList[index].checked = item.Selected;
            InteresList[index].disabled = item.Enabled ? false : true
        });
        $("#Flotante_Checked").attr('checked', dataResult.Flotante.Checked);
        $("#Flotante_Checked").prop("disabled", dataResult.Flotante.Enabled ? false : true);
    }

    $('body').on('click', function (e) {
        $('li.dropdown').removeClass('open');

    });
});