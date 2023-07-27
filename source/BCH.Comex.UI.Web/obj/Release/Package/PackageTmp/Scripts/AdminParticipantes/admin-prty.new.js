var io_temp;
$(document).ready(function () {
    $('#titlePrty').text('');
    $('#toolbarMainPrty').hide();
    $('#mnuMainArchivo').hide();
    $('#mnuMainOpciones').hide();

    newPrtyInit();

    $.post($('#urlDataObject').data('url'),
        { iO: "", "_": $.now() },
        function (data) {
            if (data) {
                v_iObject = data;
                sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iObject));
            } else {
                v_iObject = null;
                sessionStorage.removeItem(_I_OBJECT_);
            }
        }
    );
});

var newPrtyInit = function () {
    g_nuevoPrty = 1;

    var v_fmtTipoRut = $('#optRut').prop('checked');
    // (moo) 2016-01-12 | Funcionalidad para validar el R.U.T. Chile
    $('input#llaveDeIdRut')
        .rut({ formatOn: 'keyup change', validateOn: 'keyup change' })
        .on('rutInvalido', function () {
            $(this).parents(".form-group").addClass("has-error");
            f_messageBlock('Atención', 'RUT ingresado Inválido', 'warning');
            $('#btnNewPrtyAceptar').attr('disabled', true);
        })
        .on('rutValido', function () {
            $("#messageBlockAlert").alert("close");
            $(this).parents(".form-group").removeClass("has-error");
            $('#btnNewPrtyAceptar').attr('disabled', false);
        });

    //$('#llaveDeIdRut').bind('input propertychange', function () {

    //    var valorFormato = $('#llaveDeIdRut').val();
    //    if ($.validateRut(valorFormato)) {
    //        $('#llaveDeIdRut').val($.formatRut(valorFormato));
    //        $('#btnNewPrtyAceptar').attr('disabled', false);
    //        $('#llaveDeIdRut').parents(".form-group").removeClass("has-error");
    //        $("#messageBlockAlert").alert("close");
    //    }
    //});

    $('#optIndividuo').click(apNpOptIndividuo);
    $('#optBanco').click(apNpOptBanco);
    $('#optRut').click(optRut);
    $('#optAlfaNumerico').click(optAlfaNumerico);

    if (v_fmtTipoRut) {
        $('input#llaveDeIdAlfa').hide();
        $('input#llaveDeIdRut').show();
    } else {
        $('input#llaveDeIdRut').hide();
        $('input#llaveDeIdAlfa').show();
    }
}

var apNpOptIndividuo = function () {
    if ($('#optIndividuo').prop('checked')) {
        // Habilitamos check cliente comex
        $('#chkClienteComex').prop('disabled', false);
        $('#chkClienteComex').prop('checked', true);
        // Deshabilitamos y desmarcamos checks Bancos
        $('#chkAcreedor').prop('disabled', true);
        $('#chkCorresponsal').prop('disabled', true);
        $('#chkAvisador').prop('disabled', true);
        $('#chkAcreedor').prop('checked', false);
        $('#chkCorresponsal').prop('checked', false);
        $('#chkAvisador').prop('checked', false);
        // No es CITI
        v_iObject.PRTGLOB.Party.PrtGlob.EsCITI = 0;
        // Limpiamos controles
        $('#llaveDeIdAlfa').val('');
        $('#llaveDeIdRut').val('');
        // cambio de label de controles de formato
        $('#optRut').get(0).nextSibling.textContent = "R.U.T.";
        $('#optAlfaNumerico').get(0).nextSibling.textContent = "Alfanumérico";
        // Mostramos automáticamente ALfanumérico y escondemos alfanumerico
        $('input#llaveDeIdRut').show();
        $('input#llaveDeIdAlfa').hide();
        $('input#llaveDeIdAlfa').prop('placeholder', 'Alfanumérico');
        // mostramos los controles de formato y seleccionamos el de rut
        $("#optRut").parent().show().click();
        $("#optAlfaNumerico").parent().show();
        // estas variables no se instancian ni se ocupan
        //v_oPrty.esBanco = true;
        //v_oPrty.banco = true;
    }
}

$('#optCuentaGlobal').click(function () {
    if ($('#optCuentaGlobal').prop('checked')) {
        // Habilitamos cliente comex
        $('#chkClienteComex').prop('disabled', false);
        // Deshabilitamos y desmarcamos Checks Bancos
        $('#chkAcreedor').prop('disabled', true);
        $('#chkCorresponsal').prop('disabled', true);
        $('#chkAvisador').prop('disabled', true);
        $('#chkAcreedor').prop('checked', false);
        $('#chkCorresponsal').prop('checked', false);
        $('#chkAvisador').prop('checked', false);
        // Es CITI
        v_iObject.PRTGLOB.Party.PrtGlob.EsCITI = 1;
        // Limpiamos Campos
        $('#llaveDeIdAlfa').val('');
        $('#llaveDeIdRut').val('');
        // Mostramos ambos campos de texto
        $('input#llaveDeIdAlfa').show();
        $('input#llaveDeIdRut').show();
        $('input#llaveDeIdAlfa').prop('placeholder', 'Base Number');
        // escondemos los controles de formato
        $("#optRut").parent().hide();
        $("#optAlfaNumerico").parent().hide();
        $("#optAlfaNumerico").prop('checked', true);
        // estas variables no se instancian ni se ocupan
        //v_oPrty.esBanco = true;
        //v_oPrty.banco = true;
    }
});

var apNpOptBanco = function () {
    if ($('#optBanco').prop('checked')) {
        // Deshabilitamos cliente comex
        $('#chkClienteComex').prop('disabled', true);
        $('#chkClienteComex').prop('checked', false);
        // Habilitamos checks Bancos
        $('#chkAcreedor').prop('disabled', false);
        $('#chkCorresponsal').prop('disabled', false);
        $('#chkAvisador').prop('disabled', false);
        // No es CITI
        v_iObject.PRTGLOB.Party.PrtGlob.EsCITI = 0;
        // limpiamos los campos
        $('#llaveDeIdAlfa').val('');
        $('#llaveDeIdRut').val('');
        // cambio de label de controles de formato
        $('#optRut').get(0).nextSibling.textContent = "Swift";
        $('#optAlfaNumerico').get(0).nextSibling.textContent = "Alfanumérico";
        // mostramos los controles de formato
        $("#optRut").parent().show().click();
        $("#optAlfaNumerico").parent().show();
        // mostramos ambos campos de texto
        $('input#llaveDeIdAlfa').show();
        $('input#llaveDeIdAlfa').prop('placeholder', 'Swift');
        $('input#llaveDeIdRut').show();
        // estas variables no se instancian ni se ocupan
        //v_oPrty.esBanco = true;
        //v_oPrty.banco = true;
    }
}

var optRut = function () {
    if ($('#optIndividuo').prop('checked')) {
        if ($('#optRut').prop('checked')) {
            $('input#llaveDeIdAlfa').hide();
            $('input#llaveDeIdAlfa').val('');
            $('input#llaveDeIdRut').show();
            $('#chkClienteComex').prop('checked', true);
        }
    } else if ($('#optBanco').prop('checked')) {
        if ($('#optRut').prop('checked')) {
            $('input#llaveDeIdAlfa').prop('placeholder', 'Swift');
        }
    }
}

var optAlfaNumerico = function () {
    if ($('#optAlfaNumerico').prop('checked')) {
        f_messageBlock_Off();

        if ($('#optIndividuo').prop('checked')) {
            $('input#llaveDeIdAlfa').show();
            $('input#llaveDeIdRut').hide();
            $('input#llaveDeIdRut').val('');
            $('#chkClienteComex').prop('checked', false);
        } else if ($('#optBanco').prop('checked')) {
            $('input#llaveDeIdAlfa').prop('placeholder', 'Alfanumérico');
        }
    }
}

$('#llaveDeIdRut').keypress(function () {
    $('#btnNewPrtyAceptar').prop('disabled', false);
});

$('#llaveDeIdAlfa').keypress(function () {
    $('#btnNewPrtyAceptar').prop('disabled', false);
});

$('#btnNewPrtyAceptar').click(function () {
    var rutData = $('input#llaveDeIdRut').val();
    var llavePuraTxt;
    var rutaDataNoFormat;

    // agregamos validacion para cuenta global
    if ($('#optCuentaGlobal').prop('checked')) {
        if ($('input#llaveDeIdRut').val().trim() !== '' && !($.validateRut(rutData))) {
            f_messageBlock('Atención', 'RUT ingresado Inválido', 'warning');
            $('#llaveDeIdRut').focus();
            return;
        } else if ($('input#llaveDeIdRut').val().trim() === '') {
            f_messageBlock('Atención', 'Debe ingresar un RUT', 'warning');
            $('#llaveDeIdRut').focus();
            return;
        }

        if ($('input#llaveDeIdAlfa').val().trim() === '') {
            f_messageBlock('Atención', 'Debe ingresar Base Number', 'warning');
            $('#llaveDeIdAlfa').focus();
            return;
        }

        // Llave es el Base Number
        v_iObject.PRTGLOB.Party.PrtGlob.llave = $('input#llaveDeIdAlfa').val(); // id_party
        llavePuraTxt = S($('input#llaveDeIdRut').val()).replaceAll(".", "").s;
        //llavePuraTxt = $('input#llaveDeIdAlfa').val();  // rut para el servicio de consulta razon social
        rutaDataNoFormat = llavePuraTxt; 
    }

    // agregamos validacion para banco
    else if ($('#optBanco').prop('checked')) {
        if ($('input#llaveDeIdRut').val().trim() !== '' && !($.validateRut(rutData))) {
            f_messageBlock('Atención', 'RUT ingresado Inválido', 'warning');
            $('#llaveDeIdRut').focus();
            return;
        }

        if ($('input#llaveDeIdRut').val().trim() === '') {
            f_messageBlock('Atención', 'Debe ingresar un RUT', 'warning');
            $('#llaveDeIdRut').focus();
            return;
        }

        if ($('input#llaveDeIdAlfa').val().trim() === '') {
            if ($('#optAlfaNumerico').prop('checked')) {
                f_messageBlock('Atención', 'Debe ingresar código alfanumérico', 'warning');
                $('#llaveDeIdAlfa').focus();
                return;
            } else {
                f_messageBlock('Atención', 'Debe ingresar Swift', 'warning');
                $('#llaveDeIdAlfa').focus();
                return;
            }
        } 

        // Llave es el Base Number en ambos casos. 
        v_iObject.PRTGLOB.Party.PrtGlob.llave = $('input#llaveDeIdAlfa').val();
        llavePuraTxt = S($('input#llaveDeIdRut').val()).replaceAll(".", "").s;
        //llavePuraTxt = $('input#llaveDeIdAlfa').val();
        rutaDataNoFormat = llavePuraTxt;
    } else { // agregamos validacion para individuo
        if (!($('#optAlfaNumerico').prop('checked'))) {
            if ($('input#llaveDeIdRut').val().trim() !== '' && !($.validateRut(rutData))) {
                f_messageBlock('Atención', 'RUT ingresado Inválido', 'warning');
                $('#llaveDeIdRut').focus();
                return;
            }

            if ($('input#llaveDeIdRut').val().trim() === '') {
                f_messageBlock('Atención', 'Debe ingresar un RUT', 'warning');
                $('#llaveDeIdRut').focus();
                return;
            }

            // La llave es el rut
            v_iObject.PRTGLOB.Party.PrtGlob.llave = S(S($('input#llaveDeIdRut').val()).replaceAll(".", "").s).replaceAll("-", "").s;
            llavePuraTxt = S($('input#llaveDeIdRut').val()).replaceAll(".", "").s;
            rutaDataNoFormat = S(S($('input#llaveDeIdRut').val()).replaceAll(".", "").s).replaceAll("-", "").s;
            rutaDataNoFormat = rutaDataNoFormat.replace(/^0+/, '').toUpperCase();
        } else {
            if ($('input#llaveDeIdAlfa').val().trim() === '') {
                f_messageBlock('Atención', 'Debe ingresar un código alfanumérico', 'warning');
                $('#llaveDeIdRut').focus();
                return;
            }

            // La llave es el Base Number
            v_iObject.PRTGLOB.Party.PrtGlob.llave = $('input#llaveDeIdAlfa').val();
            llavePuraTxt = $('input#llaveDeIdAlfa').val();
            rutaDataNoFormat = llavePuraTxt;
        }
    }

    // (GVT) 2016-02-29 | Funcion que evalua si el RUT existe. En caso existiera, se le pide al usuario si desea consultarlo
    $.post($('#urlAceptaParty').data('url'), {
        LlaveIng: rutaDataNoFormat,
        modo: true,
        iObject: JSON.stringify(v_iObject),
        "_": $.now(),
    }, function (data) {
        if (data) {
            if (data['ErrorCode'] === 0) {
                io_temp = data['iObject'];
                //Consulta de Participante
                $('#confirmarNuevaConsultaParticipante').modal({ backdrop: 'static', keyboard: false }).one('click', '#aceptarConsultaNuevoParticipante', function () {
                    //Codigo Existente en el buscar participante
                    {
                        v_iO = io_temp;
                        sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iO));
                        v_iObject = v_iO;
                        v_iObject.PRTGLOB.Party.PrtGlob.primera = 1;

                        if (v_iObject.PRTYENT.EstadoParty !== 0) {
                            v_iObject.PRTGLOB.Party.PrtGlob.Pertenece = 0;
                        }

                        //(moo) validar acceso segun usuario

                        if (v_iObject.PRTGLOB.Party.tipo === 1) {
                            v_iObject.PRTGLOB.Party.PrtGlob.EsBanco = 1;
                        } else {
                            v_iObject.PRTGLOB.Party.PrtGlob.EsBanco = 0;
                        }

                        sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iObject));

                        $.post($('#urllee_ejecutivosSy').data('url'), {
                            codigo: v_iObject.PRTGLOB.Party.oficina,
                            iObject: JSON.stringify(v_iObject),
                            "_": $.now(),
                            }, function (data) {
                                if (data) {
                                    v_iO = data;
                                    sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iO));
                                    v_iObject = v_iO;

                                    $.post(
                                        $('#urlSygetn_Ejecutivos').data('url'), {
                                            iObject: JSON.stringify(v_iObject),
                                            "_": $.now(),
                                        }, function (data) {
                                            if (data) {
                                                v_iO = data;
                                                sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iO));
                                                v_iObject = v_iO;

                                                //TODO: volver validar si es solo "vista consulta"

                                                //carga de tasas
                                                $.post($('#urllee_tcomSy').data('url'), {
                                                    llaveIdPrty: v_iObject.PRTGLOB.Party.idparty,
                                                    "_": $.now(),
                                                }, function (data) {
                                                    if (data) {
                                                        v_iObject.PRTGLOB.tasacom = data;
                                                        sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iObject));
                                                    }

                                                    $.post($('#urllee_tgasSy').data('url'), {
                                                        llaveIdPrty: v_iObject.PRTGLOB.Party.idparty,
                                                        "_": $.now(),
                                                    }, function (data) {
                                                        if (data) {
                                                            v_iObject.PRTGLOB.tasagas = data;
                                                            sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iObject));
                                                        }

                                                        $.post($('#urllee_tintSy').data('url'), {
                                                            llaveIdPrty: v_iObject.PRTGLOB.Party.idparty,
                                                            "_": $.now(),
                                                        }, function (data) {
                                                            if (data) {
                                                                v_iObject.PRTGLOB.tasaint = data;
                                                                sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iObject));
                                                            }

                                                            window.location.href = $('#urlIndex').data('url');
                                                        });
                                                    });
                                                });
                                            }
                                        }
                                    );
                                }
                            });
                    }

                    return false;
                });
            } else { //Sigue el flujo normal de Nuevo Participante

                v_iObject.PRTGLOB.Party.swif = llavePuraTxt;

                if (($('#optIndividuo').prop('checked') && $('#optRut').prop('checked')) || ($('#optCuentaGlobal').prop('checked') && $('#optRut').prop('checked'))) {
                    // eliminamos los ceros
                    v_iObject.PRTGLOB.Party.PrtGlob.llave = v_iObject.PRTGLOB.Party.PrtGlob.llave.replace(/\b0+/g, '');
                }

                if (v_iObject.PRTGLOB.Party.PrtGlob.EsCITI === 0) {
                    // (moo) 2016-01-13 | validar si existe participante
                    $.get($('#urlexistesy').data('url'),
                        { llaveSy: S(v_iObject.PRTGLOB.Party.PrtGlob.llave).padRight(12, '|').s, modo: true, llavePura: llavePuraTxt.replace(/^0+/, '').toUpperCase(), "_": $.now() },
                        function (data) {
                            if (data) {
                                if (data.Party.estado === 120) {
                                    $('input#llaveDeIdRut').focus();
                                    f_messageBlock('Error Nuevo Participante', data.Party.riesgo, 'warning');
                                    return;
                                } else {
                                    //Carga el nombre
                                    v_iObject.PRTGLOB.nom = [];
                                    var nom = {};
                                    nom.nombre = data.nom[0].nombre;
                                    v_iObject.PRTGLOB.nom.push(nom);

                                    /// Si existen datos con error
                                    v_iObject.PRTGLOB.Party.Mensajes = data.Party.Mensajes;

                                    //Carga las cuentas
                                    v_iObject.MODWS.CtaCCOL = [];
                                    for (var i = 0; i < data.ctaclie.length; i++) {
                                        var cta = {};
                                        cta.tipo = data.ctaclie[i].moneda;
                                        cta.nrocta = data.ctaclie[i].cuenta;
                                        v_iObject.MODWS.CtaCCOL.push(cta);
                                    }

                                    f_cargarInfoPrty();
                                }
                            } else {
                                $('input#llaveDeIdRut').focus();
                                f_messageBlock('Error Nuevo Participante', 'Problema con acceder a Servidor Central', 'warning');
                                return;
                            }
                        }
                    );
                }

                if ($('#optCuentaGlobal').prop('checked')) {
                    if (v_iObject.PRTGLOB.Party.PrtGlob.EsCITI === 1) {
                        v_iObject.PRTGLOB.Party.Bnumber = $('input#llaveDeIdAlfa').val();
                        // (moo) 2016-01-13 | validar si existe participante
                        $.get($('#urlexistesy').data('url'),
                            { llaveSy: S(v_iObject.PRTGLOB.Party.Bnumber).padRight(12, '|').s, llavePura: llavePuraTxt.replace(/^0+/, '').toUpperCase(), modo: true, "_": $.now() },
                            function (data) {
                                if (data) {
                                    if (data.ErrorCode !== undefined && data.ErrorCode !== 0) {
                                        $('input#llaveDeIdAlfa').focus();
                                        f_messageBlock('Error Nuevo Participante', data.Message, 'warning');
                                        return;
                                    } else {
                                        //Carga el nombre
                                        v_iObject.PRTGLOB.nom = [];
                                        var nom = {};
                                        nom.nombre = data.nom[0].nombre;
                                        v_iObject.PRTGLOB.nom.push(nom);

                                        /// Si existen datos con error
                                        v_iObject.PRTGLOB.Party.Mensajes = data.Party.Mensajes;

                                        //Carga las cuentas
                                        v_iObject.MODWS.CtaCCOL = [];
                                        if (data.ctaclie !== undefined) {
                                            for (var i = 0; i < data.ctaclie.length; i++) {
                                                var cta = {};
                                                cta.tipo = data.ctaclie[i].moneda;
                                                cta.nrocta = data.ctaclie[i].cuenta;
                                                v_iObject.MODWS.CtaCCOL.push(cta);
                                            }
                                        }

                                        f_cargarInfoPrty();
                                    }
                                } else {
                                    $('input#llaveDeIdAlfa').focus();
                                    f_messageBlock('Error Nuevo Participante', 'Problema con acceder a Servidor Central', 'warning');
                                    return;
                                }
                            });
                    }
                }
            }
        }
        else {
            $('input#llaveDeIdRut').focus();
            f_messageBlock('Error Nuevo Participante', 'Problema con acceder a Servidor Central', 'warning');
            return;
        }
    });
});


$('#btnNewPrtyCancelar').click(function () {
    v_iObject = null;
    sessionStorage.removeItem(_I_OBJECT_);
    window.location.href = $('#urlIndex').data('url');
});

// (moo) 2016-01-13 | Función contenedora de información a cargar para el nuevo participante
var f_cargarInfoPrty = function () {
    v_iObject.PRTGLOB.Party.PrtGlob.primera = 1;
    v_iObject.PRTGLOB.Party.PrtGlob.Pertenece = 1;
    v_iObject.PRTGLOB.Party.estado = 2;//v_iObject.PRTGLOB.nuevo;
    sessionStorage.setItem('modificando', true);
    var rut;

    if ($('#optIndividuo').prop('checked') || $('#optCuentaGlobal').prop('checked')) {
        if ($('#optRut').prop('checked')) {
            v_iObject.PRTGLOB.Party.idparty = S(v_iObject.PRTGLOB.Party.PrtGlob.llave).padRight(12, '|').s;
            v_iObject.PRTGLOB.Party.sirut = 1;
            v_iObject.PRTGLOB.Party.rut = v_iObject.PRTGLOB.Party.PrtGlob.llave;
            if ($('#chkClienteComex').prop('checked')) {
                v_iObject.PRTGLOB.Party.tipo = 2;
            } else {
                v_iObject.PRTGLOB.Party.tipo = 0;
            }
        } else {
            v_iObject.PRTGLOB.Party.idparty = S(v_iObject.PRTGLOB.Party.PrtGlob.llave).padRight(12, '|').s;
            v_iObject.PRTGLOB.Party.sirut = 0;
            v_iObject.PRTGLOB.Party.rut = "";
            v_iObject.PRTGLOB.Party.tipo = 0;

            if ($('#optCuentaGlobal').prop('checked')) {
                v_iObject.PRTGLOB.Party.sirut = 1;
                rut = S(S($('input#llaveDeIdRut').val()).replaceAll(".", "").s).replaceAll("-", "").s;
                rut = rut.replace(/^0+/, '').toUpperCase();
                v_iObject.PRTGLOB.Party.rut = rut;

                if ($('#chkClienteComex').prop('checked')) {
                    v_iObject.PRTGLOB.Party.tipo = 2;
                } else {
                    v_iObject.PRTGLOB.Party.tipo = 0;
                }
            }
        }
    } else {
        v_iObject.PRTGLOB.Party.idparty = S(v_iObject.PRTGLOB.Party.PrtGlob.llave).padRight(12, '|').s;
        v_iObject.PRTGLOB.Party.tipo = 1;
        v_iObject.PRTGLOB.Party.sirut = 1;
        rut = S(S($('input#llaveDeIdRut').val()).replaceAll(".", "").s).replaceAll("-", "").s;
        rut = rut.replace(/^0+/, '').toUpperCase();
        v_iObject.PRTGLOB.Party.rut = rut;
        
        if ($('#chkAcreedor').prop('checked')) {
            v_iObject.PRTGLOB.Party.Flag = v_iObject.PRTGLOB.Party.Flag + 16;
            if (v_iObject.PRTGLOB.Party.PrtGlob.FlagParty !== 1) {
                v_iObject.PRTGLOB.Party.PrtGlob.FlagParty = 1;
            }
        }

        if ($('chkCorresponsal').prop('checked')) {
            v_iObject.PRTGLOB.Party.Flag = v_iObject.PRTGLOB.Party.Flag + 8;
            if (v_iObject.PRTGLOB.Party.PrtGlob.FlagParty !== 1) {
                v_iObject.PRTGLOB.Party.PrtGlob.FlagParty = 1;
            }
        }

        if ($('chkAvisador').prop('checked')) {
            v_iObject.PRTGLOB.Party.Flag = v_iObject.PRTGLOB.Party.Flag + 128;
            if (v_iObject.PRTGLOB.Party.PrtGlob.FlagParty !== 1) {
                v_iObject.PRTGLOB.Party.PrtGlob.FlagParty = 1;
            }
        }

        if ($('#optRut').prop('checked')) {
            v_iObject.PRTGLOB.Party.swif = v_iObject.PRTGLOB.Party.PrtGlob.llave;
            v_iObject.PRTGLOB.Party.Flag = v_iObject.PRTGLOB.Party.Flag + 32;
            if (v_iObject.PRTGLOB.Party.PrtGlob.FlagParty !== 1) {
                v_iObject.PRTGLOB.Party.PrtGlob.FlagParty = 1;
            }
        }
    }

    sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iObject))

    $.post($('#urlDataObject').data('url'),
        { iO: JSON.stringify(v_iObject), "_": $.now() },
        function (data) {
            if (data) {
                v_iObject = data;
                sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iObject));
                window.location.href = $('#urlRazonSocial').data('url');
            }
        }
    );
};