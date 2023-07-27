var _I_OBJECT_ = 'iObject';
var v_oPrty;
var v_oComuna;
var v_oPais;
var v_oComuna_1;
var v_sessionStorage;
var v_iObject;
var baseUrl = $("#base_url").val();
var redirigir = 0;

$(document).ready(function () {
    $(document).ajaxStart(function () { $.blockUI({ message: '<h3><i class="fa fa-cog fa-spin"></i>&nbsp;&nbsp;Cargando...</h3>', baseZ: 2000 }) })
        .ajaxStop($.unblockUI).ajaxError($.unblockUI);

    //(Marco Orellana) verificar datos de session
    var v_data = sessionStorage.getItem(_I_OBJECT_);
    if (v_data) {
        v_iObject = JSON.parse(v_data);
    }

    //(Marco Orellana) si InitObject no esta ir a buscar a servidor
    if (!v_iObject) {
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
    }

    if (referrerPathname() === 'AdminParticipantes/CuentasParticipante') {
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
    }

    fTitleMain();

    //fMenuHabilitar(false);

    var v_urlGetComunasLocalidades = $('#urlGetComuna').data('url');
    var v_urlGetPais = $('#urlGetPais').data('url');

    v_sessionStorage = sessionStorage.getItem('oComuna');
    if (v_sessionStorage) {
        v_oComuna = JSON.parse(v_sessionStorage);
    }

    v_sessionStorage = sessionStorage.getItem('oPais');
    if (v_sessionStorage) {
        v_oPais = JSON.parse(v_sessionStorage);
    }

    //(Marco Orellana) comuna
    if (!v_oComuna) {
        $.get(v_urlGetComunasLocalidades,
            {},
            function (data) {
                v_oComuna = data;
                sessionStorage.setItem('oComuna', JSON.stringify(v_oComuna));
            }
        );
    }

    if (v_oComuna) {
        $('#comuna').empty();
        $.each(v_oComuna, function (key, value) {
            $('#comuna').append('<option value=' + value.codigo + '>' + value.nombre + '</option>');
        });
    }

    //(Marco Orellana) pais
    if (!v_oPais) {
        $.get(v_urlGetPais,
            {},
            function (data) {
                v_oPais = data;
                sessionStorage.setItem('oPais', JSON.stringify(v_oPais));
            }
        );
    }

    if (v_oPais) {
        $('#pais').empty();
        $.each(v_oPais, function (key, value) {
            $('#pais').append('<option value=' + value.codigo + '>' + value.nombre + '</option>');
        });
    }

    //
    // (Marco Orellana) menu eventos
    //

    var modificando = sessionStorage.getItem('modificando');

    $('#mnuNuevoParticipante, #tbr_nuevoParticipante').click(function (e) {
        e.preventDefault();
        modificando = sessionStorage.getItem('modificando');
        if (v_iObject.PRTGLOB.Party.estado == 5 || modificando == 'true') {
            $('#confirmarSalirGuardar').modal({ backdrop: 'static', keyboard: false }).one('click', '#aceptarPerderCambios', function () {
                redirigir = 1;
                $('#tbr_Grabar').click();
                $("#confirmarSalirGuardar").modal('hide');
            })
            .one('click', '#perderCambios', function () {
                sessionStorage.removeItem('modificando');
                v_iObject = null;
                window.location.href = $('#urlNuevoParticipante').data('url');
            })
            .one('click', '#cancelarSalir', function () {
                $("#confirmarSalirGuardar").modal('hide');
            });

        } else {
            sessionStorage.removeItem(_I_OBJECT_);
            v_iObject = null;
            window.location.href = $('#urlNuevoParticipante').data('url');
        }
    });

    $('#mnuAbrirParticipante, #tbr_AbrirParticipante').click(function (e) {
        e.preventDefault();
        modificando = sessionStorage.getItem('modificando');
        if (v_iObject.PRTGLOB.Party.estado == 5 || modificando == 'true') {
            $('#confirmarSalirGuardar').modal({ backdrop: 'static', keyboard: false }).one('click', '#aceptarPerderCambios', function () {
                redirigir = 2;
                $('#tbr_Grabar').click();
                $("#confirmarSalirGuardar").modal('hide');
            })
            .one('click', '#perderCambios', function () {
                sessionStorage.removeItem('modificando');
                v_iObject = null;
                window.location.href = $('#urlAbrirParticipante').data('url');
            })
            .one('click', '#cancelarSalir', function () {
                $("#confirmarSalirGuardar").modal('hide');
            });
        } else {
            sessionStorage.removeItem(_I_OBJECT_);
            v_iObject = null;
            window.location.href = $('#urlAbrirParticipante').data('url');
        }
    });

    function redirigirAbrir() {
        window.location.href = $('#urlAbrirParticipante').data('url');
    }

    function redirigirNuevo() {
        window.location.href = $('#urlNuevoParticipante').data('url');
    }
});

//
// Cambia el titulo con información del participante en "memoria"
//
var fTitleMain = function () {
    if (v_iObject) {
        var v_idPrty;

        if (v_iObject.PRTGLOB) {
            if (v_iObject.PRTGLOB.Party) {
                if ((v_iObject.PRTGLOB.Party.PrtGlob.EsCITI != 0) && v_iObject.PRTGLOB.Party.Bnumber) {
                    v_idPrty = v_iObject.PRTGLOB.Party.Bnumber.replace(/[|]/gi, "");
                } else if (v_iObject.PRTGLOB.Party.idparty) {
                    v_idPrty = v_iObject.PRTGLOB.Party.idparty.replace(/[|]/gi, "");
                }
            }
        }

        if (v_idPrty) {
            $('#titlePrty').text('[' + v_idPrty + '] ' + v_iObject.PRTGLOB.Party.creacosto + ':' + v_iObject.PRTGLOB.Party.creauser);
        } else {
            $('#titlePrty').text('');
        }
    } else {
        $('#titlePrty').text('');
    }
};

function referrerPathname() {
    var r = document.createElement('a');
    r.href = document.referrer;
    var p = r.pathname;
    r = '';
    return p.replace(/\/+$/, '');
}


$('#mnuRecuperarParticipante').click(function () {
    var v_urlIndex = $('#urlIndex').data('url');
    //(Marco Orellana) participante esta ya activo vía busqueda!
    $.get($('#urlGetPartyDataFromServer').data('url'), {
        "_": $.now(),
    }, function (data) {
        if (data) {
            $('#tableRazonSocial').bootstrapTable('refresh');
            $('#tableDireccion').bootstrapTable('refresh');
            window.location.href = v_urlIndex;
        }
    });
});

$('#mnuEliminarParticipante').click(function () {
    if (!$('#mnuEliminarParticipante').prop('disabled')) {
        $('#IdParticipanteMsg').text('Desea eliminar el Participante ' + v_iObject.PRTGLOB.Party.rut);
        $('#modalMsgEliminarParticipando').modal();
    }
});

$('#BtnAceptarEliminaPari').click(function () {

    $.post($('#urlEliminarParticipante').data('url'), {
        llaveIdPrty: v_iObject.PRTGLOB.Party.idparty
    }, function (data) {
        if (data.ErrorCode == 0) {
            f_messageBlock('Eliminar Participante', 'El participante ha sido marcado como eliminado. Se borrara durante la noche.', 'success');
            $('#modalMsgEliminarParticipando').hide();
            GrabarOcultar();
        } else if (data.ErrorCode == 1) {
            f_messageBlock('Eliminar Participante', data.Message , 'warning');
        } else {
            f_messageBlock('Eliminar Participante', 'Error eliminando Participante.', 'danger');
        }
    })
});

$('#mnuSalirParticipante').click(function () {
    var v_urlCleanPrty = $('#urlCleanPrty').data('url');
    sessionStorage.removeItem(_I_OBJECT_);
    v_iObject = null;

    $.post(v_urlCleanPrty, {
        "_": $.now(),
    }, function (data) {
        if (data) {
            //window.location.href = $('#urlIndex').data('url');
            close();
        }
    });
});

$('#mnuPrtyActivar').click(function () {
    if (!$('#mnuPrtyActivar').hasClass('disabled')) {
        var v_urlActivarRazon = $('#urlActivarRazon').data('url');
        var estado = 0;

        $.post(v_urlActivarRazon, {
            "_": $.now()
        }, function (data) {
            if (data) {
                if (data.IdEstado == 1) {
                    bootbox.confirm("Este cambio puede provocar problemas con planillas al Banco Central. ¿Desea Continuar?", function (result) {
                        if (result == true) {
                            $.post($('#urlAceptaParty').data('url'), function (data1) {
                                if (data1 != null) {
                                    loadMessages(data1.MensajesDeError);
                                }
                            });
                        } else {
                            window.location.href = $('#urlIndex').data('url');;
                        }
                    });
                }
            }
        });
    }
});

$('#OpcionCaracteristica').click(function () {
    if (!$('#OpcionCaracteristica').hasClass('disabled')) {
        window.location.href = $('#urlCaracteristicaParticipante').data('url');
    }
});

$('#OpcionInstrucciones').click(function () {
    if (!$('#OpcionInstrucciones').hasClass('disabled')) {
        window.location.href = $('#urlInstruccionesParticipante').data('url');
    }
});

$('#OpcionCuentas').click(function () {
    if (!$('#OpcionCuentas').hasClass('disabled')) {
        window.location.href = $('#urlCuentasParticipante').data('url');
    }
});

$('#OpcionTasas').click(function () {
    if (!$('#OpcionTasas').hasClass('disabled')) {
        window.location.href = $('#urlTasasParticipante').data('url');
    }
});

$('#mnuSalvarParticipante, #tbr_Grabar').click(function () {

    f_messageBlock_Off();

    if (v_iObject.PRTGLOB.nom.length == 0) {
        f_messageBlock('Error Grabar Participante', 'No se puede grabar participante por que no tiene registrada razones sociales.', 'danger');
        return;
    }

    if (v_iObject.PRTGLOB.direc.length == 0) {
        f_messageBlock('Error Grabar Participante', 'No se puede grabar participante por que no tiene registrada direcciones.', 'danger');
        return;
    }
    var urlTamanoCuentas = $('#urlGetTamanoCuentas').data('url');
    $.get(urlTamanoCuentas, function (data) {
        //console.log(data.NumeroCuentas);

        if (data.NumeroCuentas == 0) {
            $('#modalPrtEnt12').modal({ backdrop: 'static', keyboard: false }).one('click', '#prtEnt12Cliente', function () {
                //Redirigimos a cuentas
                sessionStorage.setItem('modificando', true);
                var v_urlCuentasParticipante = $('#urlCuentasParticipante').data('url');
                window.location.href = v_urlCuentasParticipante;
            })
            .one('click', '#prtEnt12Individuo', function () {
                v_iObject.PRTGLOB.Party.tipo = 0 // Tipo Individuo
                $("#modalPrtEnt12").modal('hide');
                _SalvaPrtySy(true);
            })
            .one('click', '#prtEnt12Cancelar', function () {
                sessionStorage.setItem('modificando', true);
                $('#modalPrtEnt12').modal('hide');
            });
        }
        else {
            _SalvaPrtySy(true);
        }
    });
    //var tamCuentas = ;
});

var _SalvaPrtySy = function (modo, callback) {
    var nomEli = false;
    var dirEli = false;
    var v_miEstado = 0;

    //(Marco Orellana) validar si tiene razon social
    v_iObject.PRTGLOB.nom.forEach(function (razonSocial) {
        if (razonSocial.borrado) {
            nomEli = true;
        } else {
            nomEli = false;
        }
    });

    //(Marco Orellana) validar si tiene direccion
    v_iObject.PRTGLOB.direc.forEach(function (direccion) {
        if (direccion.borrado) {
            dirEli = true;
        } else {
            dirEli = false;
        }
    });

    //(Marco Orellana) validar si tipo banco, tiene cuentas eliminadas
    if (v_iObject.PRTGLOB.Party.tipo == v_iObject.PRTGLOB.tipo_banco && v_iObject.PRTGLOB.bctas_eliminadas != 0) {
        if ((v_iObject.PRTGLOB.Party.Flag & v_iObject.PRTGLOB.Gprt_FlagCorresponsal) != 0) {
            v_iObject.PRTGLOB.Party.Flag = v_iObject.PRTGLOB.Party.Flag - v_iObject.PRTGLOB.Gprt_FlagCorresponsal;
        }
        v_iObject.PRTGLOB.bctas_eliminadas = 0;
        if (v_iObject.PRTGLOB.FlagPrty == 0) {
            v_iObject.PRTGLOB.FlagPrty = 1;
            if (v_iObject.PRTGLOB.Party.estado == v_iObject.PRTGLOB.leido) {
                v_iObject.PRTGLOB.Party.estado = v_iObject.PRTGLOB.modificado;
            }
        }
    }

    //(Marco Orellana) validar si tipo banco, tiene lineas eliminadas
    if (v_iObject.PRTGLOB.Party.tipo == v_iObject.PRTGLOB.tipo_banco && v_iObject.PRTGLOB.blin_eliminadas != 0) {
        if ((v_iObject.PRTGLOB.Party.Flag & v_iObject.PRTGLOB.Gprt_FlagAcreedor) != 0) {
            v_iObject.PRTGLOB.Party.Flag = v_iObject.PRTGLOB.Party.Flag - v_iObject.PRTGLOB.Gprt_FlagAcreedor;
        }
        v_iObject.PRTGLOB.blin_eliminadas = 0;
        if (v_iObject.PRTGLOB.FlagPrty == 0) {
            v_iObject.PRTGLOB.FlagPrty = 1;
            if (v_iObject.PRTGLOB.Party.estado == v_iObject.PRTGLOB.leido) {
                v_iObject.PRTGLOB.Party.estado = v_iObject.PRTGLOB.modificado;
            }
        }
    }

    if (v_iObject.PRTGLOB.Party.tipo == v_iObject.PRTGLOB.tipo_banco) {
        v_iObject.PRTGLOB.Party.clasificacion = 2;
    }

    switch (v_iObject.PRTGLOB.Party.estado) {
        case v_iObject.PRTGLOB.eliminado_nuevo:
            v_iObject = null;
            sessionStorage.removeItem(_I_OBJECT_);
            window.location.href = $('#urlIndex').data('url');
            break;

        case v_iObject.PRTGLOB.leido:
            if (modo) {
                v_miEstado = 3;
            } else {
                v_iObject = null;
                sessionStorage.removeItem(_I_OBJECT_);
                window.location.href = $('#urlIndex').data('url');
            }
            break;

        case v_iObject.PRTGLOB.modificado:
            if (nomEli || dirEli) {
                nomEli = false;
                dirEli = false;
                $('#modalMsgPrtyNomDir').modal();
                return;
            }

            if (v_iObject.PRTGLOB.FlagPrty != 0) {
                switch (v_iObject.PRTGLOB.Party.tipo) {
                    case v_iObject.PRTGLOB.tipo_banco:
                        //(Marco Orellana) valida cuenta banco
                        if ((v_iObject.PRTGLOB.Party.Flag & g_constPrty.flagCorresponsal) == g_constPrty.flagCorresponsal) {
                            if (v_iObject.PRTGLOB.ctabancos) {
                                if (!v_iObject.PRTGLOB.ctabancos[0].cuenta) {
                                    $('#modalMsgPrtyCuentas').modal();
                                    return;
                                }
                            } else {
                                $('#modalMsgPrtyCuentas').modal();
                                return;
                            }
                        }

                        //(Marco Orellana) valida linea banco
                        if ((v_iObject.PRTGLOB.Party.Flag & v_iObject.PRTGLOB.Gprt_FlagAcreedor) == v_iObject.PRTGLOB.Gprt_FlagAcreedor) {
                            if (v_iObject.PRTGLOB.linbancos) {
                                if (!v_iObject.PRTGLOB.linbancos[0].cuenta) {
                                    $('#modalMsgPrtyCuentas').modal();
                                    return;
                                }
                            } else {
                                $('#modalMsgPrtyCuentas').modal();
                                return;
                            }
                        }

                        break;

                    case v_iObject.PRTGLOB.tipo_cliente:
                        if (v_iObject.PRTGLOB.ctaclie) {
                            if (!v_iObject.PRTGLOB.ctaclie[0].cuenta) {
                                $('#modalPrtEnt12').modal();
                                return;
                            }
                        } else {
                            $('#modalPrtEnt12').modal();
                            return;
                        }
                        break;
                }
            }

            v_miEstado = v_iObject.PRTGLOB.modificado;

            break;

        case v_iObject.PRTGLOB.nuevo:
            if (v_iObject.PRTGLOB.FlagRazones == 0 || v_iObject.PRTGLOB.FlagDireccion == 0 || nomEli || dirEli) {
                $('#modalMsgPrtyNomDir').modal();
                nomEli = false;
                dirEli = false;
                return;
            }

            switch (v_iObject.PRTGLOB.Party.tipo) {
                case v_iObject.PRTGLOB.tipo_cliente:
                    if (v_iObject.PRTGLOB.FlagCuentas == 0) {
                        $('#modalPrtEnt12').modal();
                        return;
                    }
                    break;

                case v_iObject.PRTGLOB.tipo_banco:
                    //(Marco Orellana) valida cuenta banco
                    if ((v_iObject.PRTGLOB.Party.Flag & v_iObject.PRTGLOB.FlagCorresponsal) == v_iObject.PRTGLOB.FlagCorresponsal) {
                        if (v_iObject.PRTGLOB.FlagCtaBco == 0) {
                            $('#modalMsgPrtyCuentas').modal();
                            return;
                        }
                    }

                    //(Marco Orellana) valida linea banco
                    if ((v_iObject.PRTGLOB.Party.Flag & v_iObject.PRTGLOB.Gprt_FlagAcreedor) == v_iObject.PRTGLOB.Gprt_FlagAcreedor) {
                        if (v_iObject.PRTGLOB.FlagLineas == 0) {
                            $('#modalMsgPrtyCuentas').modal();
                            return;
                        }
                    }
                    break;
            }

            v_miEstado = v_iObject.PRTGLOB.nuevo;
            break;

        case v_iObject.PRTGLOB.eliminado_leido:
        case v_iObject.PRTGLOB.eliminado_modificado:
            v_miEstado = v_iObject.PRTGLOB.Party.estado;
            break;
    }

    if (v_iObject.PRTGLOB.Party.tipo == 0 || v_iObject.PRTGLOB.Party.tipo == 1 || v_iObject.PRTGLOB.Party.tipo == 2) {
        $.get(urlGetCurrentUICuentasData, function (data) {
            data.forEach(d => v_iObject.PRTGLOB.ctaclie.push(d));
            $.get($('#urlGetCurrentUITipClieData').data('url'), function (dd) {
                v_iObject.PrtEnt07.prtcliente = dd;
                f_GrabarParticipante(v_iObject.PRTGLOB.Party.idparty, v_miEstado);
            });
        });
    } else {
        f_GrabarParticipante(v_iObject.PRTGLOB.Party.idparty, v_miEstado);
    }
    
    typeof callback === 'function' && callback();
}

//
// (Marco Orellana) actualizar initObject en Servidor
//
var f_saveDataOject = function () {
    $.post(
        $('#urlDataObject').data('url'), {
            iO: JSON.stringify(v_iObject),
            "_": $.now()
        },
        function (data) {
            if (data) {
            }
        }
    );
};

var f_saveDataOjectAlterno = function () {
    $.post(
        $('#urlDataObject').data('url'),
    {
        iO: JSON.stringify(v_iObject),
        "_": $.now()
    },
        function (data) {
            if (data) {
                sessionStorage.removeItem('modificando');
                v_iObject.PRTGLOB.Party.estado = 1;
                f_messageBlock('Grabar Participante', 'Datos del Participante grabados con Exito!', 'success');
                GrabarOcultar();
                if(redirigir == 1)
                    window.location.href = $('#urlNuevoParticipante').data('url');                    
                else if (redirigir == 2)
                    window.location.href = $('#urlAbrirParticipante').data('url');
            }
        }
    );
};

function GrabarOcultar() {
    // GVT : debido que los elementos <a> no se pueden deshabilitar en jquery usando IE8, se crea un menu alterno con las funciones de 
    // jerarquia de supervisor impuestas, ya que son los unicos que pueden grabar.
    $("#toolbarMainPrty").css("display", "none");
    $("#toolbarMainPrty_afterSave").css("display", "block");

    $('#mnuEliminarParticipante').addClass('disabled');
    $('#mnuSalvarParticipante').addClass('disabled');
    $('#mnuRecuperarParticipante').addClass('disabled');
    
    $('#titlePrty').text('');
    $('#OpcionCaracteristica').addClass('disabled');
    $('#OpcionInstrucciones').addClass('disabled');
    $('#OpcionCuentas').addClass('disabled');
    $('#OpcionTasas').addClass('disabled');
    $('#mnuPrtyActivar').addClass('disabled');

    $("#nuevaRazonSocialP").addClass('disabled');
    $("#nuevaDireccionP").addClass('disabled');
    $('#tableDireccion').bootstrapTable('removeAll');
    $('#tableRazonSocial').bootstrapTable('removeAll');
}


// (Marco Orellana) 2016-01-11 | Muesta "alerta" por un tiempo de 15 segundos y desaparece
var f_messageBlock = function (messageTitle, messageText, messageType) {
    var v_alert = "<div id='messageBlockAlert' class='alert alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button><strong id='messageBlockTitle'></strong><br /><span id='messageBlockText'></span></div>";
    $("#messageBlockAlert").alert("close");
    $("#messageBlock").append(v_alert);
    messageType = messageType || "info";
    $('#messageBlockAlert').addClass('alert-' + messageType);
    $('#messageBlockTitle').text(messageTitle);
    $('#messageBlockText').text(messageText);
    $("#messageBlockAlert").alert();

    // (Marco Orellana) 2016-01-12 | 15 segundos y cerramos "alerta"
    window.setTimeout(function () { $("#messageBlockAlert").alert("close") }, 15000);
}

var f_messageBlock_Off = function () {
    $("#messageBlockAlert").alert("close");
}

// (Marco Orellana) 2016-01-11 | Correción, responder mensaje de Exito/Error al Grabar
var f_GrabarParticipante = function (idPrty, miEstado) {

    $.post($('#urlGuardaParticipanteGlobal').data('url'), {
        llaveIdPrty: idPrty,
        estadoPrty: miEstado,
        iO: JSON.stringify(v_iObject),
        "_": $.now()
    }, function (data) {
        if (data.ErrorCode == 0) {
            if (data.Message.length > 0) {
                //f_messageBlock('Grabar Participante', data.Message, 'info');
                bootbox.alert(data.Message, function () {
                    v_iObject = f_saveSessionObject(data.iObject);
                    f_saveDataOjectAlterno();
                });
            }else{
                v_iObject = f_saveSessionObject(data.iObject);
                f_saveDataOjectAlterno();
            }
        } else {
            f_messageBlock('Error Grabar Participante', data.Message, 'danger');
        }
    }).done(function () {
        $.post($('#urlReloadSessionObject').data('url'));
    });
}

// (Marco Orellana) 2016-01-11 | funcion para guardar initObject
var f_saveSessionObject = function (iO) {
    sessionStorage.setItem(_I_OBJECT_, JSON.stringify(iO));
    return iO;
}