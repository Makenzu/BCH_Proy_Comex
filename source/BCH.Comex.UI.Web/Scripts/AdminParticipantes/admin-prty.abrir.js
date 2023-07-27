var v_idParticipante_Busq_Razon;
var elementoAgregarRazonSocial;

$(document).ready(function () {
    $('#modalSrchPart').modal('hide');
    $('#pnlResultados2').hide();
    $('#btnSelPartyByRazonSocial').hide();

    $('#toolbarMainPrty').hide();
    $('#mnuMainArchivo').hide();
    $('#mnuMainOpciones').hide();

    sessionStorage.removeItem(_I_OBJECT_);
    var tituloParty = $('#titlePrty');

    if (!S(tituloParty.text()).isEmpty()) {
        tituloParty.empty();
    }

    function IdReplace(value, row, index) {
        var str = value;
        var res = str.replace(/\|/g, '');
        return res;
    }

    var cfgColTableSearchByIdParty = [{
        field: 'state',
        radio: true
    }, {
        field: 'id_party',
        title: 'Id Participante',
        sortable: true,
        align: 'right',
        formatter: IdReplace,
    }, {
        field: 'razon_soci',
        title: 'Razón Social',
        visible: true,
        searchable: false
    }, ];


    var cfgColTableSearchByRazonSocial = [{
        field: 'state',
        radio: true
    }, {
        field: 'razon_soci',
        title: 'Razón Social',
        sortable: false,
        align: 'left',
    }, {
        field: 'direccion',
        title: 'Dirección',
        sortable: false,
        align: 'left',
    }, {
        field: 'ciudad',
        title: 'Ciudad',
        sortable: false,
        align: 'left',
    }, {
        field: 'pais',
        title: 'País',
        sortable: false,
        align: 'left',
    }, {
        field: 'id_party',
        title: 'Identificación',
        visible: true,
        formatter: IdReplace,
    }, {
        field: 'id_nombre',
        title: 'Id Nombre',
        visible: false
    }, {
        field: 'id_dir',
        title: 'Id Dir',
        visible: false
    }];

    //ejecuta función buscar default
    buscarByIdParty(cfgColTableSearchByIdParty);

    //asigna abrir modal en botón buscar principal
    $('#btnOpenPrtyBuscar').click(function () {
        $('#modalSrchPart').modal();

        $('#pnlResultados2').hide();
        $('#x').bootstrapTable('destroy');
    });

    $('#btnBuscarByRazonSocial').click(function () {
        buscarByRazonSocial(cfgColTableSearchByRazonSocial);
    });

    $('#btnSelPartyByRazonSocial').click(function () {
        $('#btnOpenPrtyAceptar').removeClass('disabled');
        $('#modalSrchPart').modal('hide');
        $('input[type=text][class="form-control"][placeholder="Buscar"]').prop("value", v_idParticipante_Busq_Razon);

        //Limpiamos el modal para la siguiente busqueda
        $('#searchRazonSocial').val('');
        $('#tablaSearchByRazonSocial').bootstrapTable('destroy');
        $('#btnSelPartyByRazonSocial').hide();

        //Creamos el elemento del participante y lo ponemos en la lista
        $('#tablaSearchByIdParty').bootstrapTable('prepend', elementoAgregarRazonSocial);

    });

    $('#modalSrchPart').on('hidden.bs.modal', function () {
        $('#searchRazonSocial').val('');
        $('#tablaSearchByRazonSocial').bootstrapTable('destroy');
        $('#btnSelPartyByRazonSocial').hide();
    })


    $('input[type=text][class="form-control"][placeholder="Buscar"]').keydown(function () {
        $('#btnOpenPrtyAceptar').removeClass('disabled');
    });

    $('input[type=text][class="form-control"][placeholder="Buscar"]').attr('maxlength', '12');

    $('input[type=text][class="form-control"][placeholder="Buscar"]').keyup(function (e) {
        if (e.keyCode == 13) {
            $('#btnOpenPrtyAceptar').click();
        }
    });
    $('input[type=text][class="form-control"][placeholder="Buscar"]').focus();
    // (moo) 2016-01-12 | Captura de Enter para buscar participante
    $('#searchRazonSocial').keyup(function (e) {
        if (e.keyCode == 13) {
            if (!S($('#searchRazonSocial').val()).isEmpty()) {
                $('#btnBuscarByRazonSocial').click();
            }
        }
    });
});

$('#btnOpenPrtyCancel').click(function () {
    window.location.href = $('#urlIndex').data('url');
});

//(moo) 2015-09-10 
$('#btnPrtyAddByBic').click(function () {
    var v_idParty = $('input[type=text][class="form-control"][placeholder="Buscar"]').val();

    $.getJSON($('#urlAddPartyByDataBic').data('url'), {
        idParty: v_idParty
    }, function (data) {
        if (data) {
            if (data[0].code == 0) {
                $('#modalPartyMensajeNoExisteBic').modal('hide');
            } else if (data[0].code == -1) {
                $('#modalPartyMensajeNoExisteBic').modal('hide');
                $('#modalPartyMensajeNoBaseBic').modal();
            }
        }
    });
});

//(moo 2015-09-07)
$('#btnOpenPrtyAceptar').click(function () {
    var v_urlGetPartyByIdParty = $('#urlGetPartyByIdParty').data('url');
    var v_idParty = $('#memoryInputIdParty').val();
    var v_idNombre = $('#memoryInputIdNombre').val();
    var v_idDir = $('#idDireccion').val();
    var v_iO = null;

    //(moo) validamos si existe en "memoria" ID Participante
    v_idParty = $('input[type=text][class="form-control"][placeholder="Buscar"]').val();

    v_iObject.PRTGLOB.Party.PrtGlob.Pertenece = 1;
    var v_argTemp1 = v_idParty.toUpperCase();
    $.post($('#urlAceptaParty').data('url'), {
        LlaveIng: v_argTemp1,
        modo: true,
        iObject: JSON.stringify(v_iObject),
        "_": $.now(),
    }, function (data) {
        if (data) {
            if (data['ErrorCode'] == 10) {
                $('#modalPrtyAdv01').modal();
            } else if (data['ErrorCode'] == 10) {
                $('#modalPartyMensajeNoExiste').modal();
            } else if (data['ErrorCode'] == 100) {
                v_iO = data['iObject'];
                sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iO));
                $('#modalPrtyEliminado').modal();
            } else {
                v_iO = data['iObject'];
                sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iO));
                v_iObject = v_iO;
                v_iObject.PRTGLOB.Party.PrtGlob.primera = 1;

                if (v_iObject.PRTYENT.EstadoParty != 0) {
                    v_iObject.PRTGLOB.Party.PrtGlob.Pertenece = 0;
                }

                //(moo) validar acceso segun usuario

                if (v_iObject.PRTGLOB.Party.tipo == 1) {
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

                        $.post($('#urlSygetn_Ejecutivos').data('url'), {
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
                        });
                    }
                });
            }
        } else {
            $('input[type=text][class="form-control"][placeholder="Buscar"]').focus();
        }
    });
});

$('#btnPrtySiActivar').click(function () {
    var iO = null;
    var dataAdicional = $('#memoryInputIdParty').val();

    $.post($('#urlPrtyReActivar').data('url'), {
        searchIdPrty: dataAdicional,
        iObject: v_iObject,
        "_": $.now(),
    }, function (data) {
        if (data) {
            $('#modalPrtyEliminado').modal('hide');

            if (data['ErrorCode'] == 99) {
                $('#modalMsgPrtyErrorUpd').modal();
            } else {
                $('#modalPrtyReActivado').modal({ backdrop: 'static', keyboard: false }).one('click', '#btnAceptarReactivado', function () {
                    $('#btnOpenPrtyAceptar').click();
                });
            }
        }
    });
});

$('#btnPrtyReactNo').click(function () {
    $('#modalPrtyEliminado').modal('hide');

    window.location.href = $('#urlGetPartyByIdParty').data('url');
});

//Cuando se selecciona un participante en la grilla de busqueda razon social
$('#tablaSearchByRazonSocial').on('check.bs.table', function (e, row) {
    var v_idParty = '';
    $('#btnSelPartyByRazonSocial').show();

    //(moo) optimizamod el código
    var v_elementByRazonSocial = $('#tablaSearchByRazonSocial').bootstrapTable('getSelections');

    //(moo) eliminamos los | del prtyID
    v_idParty = v_elementByRazonSocial[0].id_party.replace(/\|/g, '');

    $('#memoryInputIdParty').val(v_idParty);
    $('#RazPartiH2').val(v_elementByRazonSocial[0].razon_soci);
    $('#RazDirH').val(v_elementByRazonSocial[0].direccion);
    $('#RazCiuH').val(v_elementByRazonSocial[0].ciudad);
    $('#RazPaisH').val(v_elementByRazonSocial[0].pais);
    $('#memoryInputIdNombre').val(v_elementByRazonSocial[0].id_nombre);
    $('#idDireccion').val(v_elementByRazonSocial[0].id_dir);

    //(moo) copiamos prtyID en input de busqueda de la tabla
    //$('input[type=text][class="form-control"][placeholder="Buscar"]').prop("value", v_idParty);

    v_idParticipante_Busq_Razon = v_idParty;
    elementoAgregarRazonSocial = { state: 1 ,id_party: v_idParty, razon_soci: v_elementByRazonSocial[0].razon_soci };

    // (moo) 2016-01-12 | agregamos participante seleccionado a tabla para buscar participantes
    //$('#tablaSearchByIdParty').bootstrapTable('append', v_elementByRazonSocial[0]);
});

//Cuando se selecciona un participante en la grilla de busqueda por rut
$('#tablaSearchByIdParty').on('check.bs.table', function (e, row) {
    //(moo) optimizamos codigo
    var v_idParty = '';
    var v_elementByIdParty = $('#tablaSearchByIdParty').bootstrapTable('getSelections');

    $('#btnOpenPrtyAceptar').removeClass('disabled');
    
    v_idParty = v_elementByIdParty[0].id_party.replace(/\|/g, '');

    $('#memoryInputIdParty').val(v_idParty);

    $('input[type=text][class="form-control"][placeholder="Buscar"]').prop("value", v_idParty);
});

//Doble click en la grilla de busqueda por RUT
$('#tablaSearchByIdParty').on('dbl-click-row.bs.table', function (e, row) {
    
    var v_idParty = '';
    var v_elementByIdParty = $('#tablaSearchByIdParty').bootstrapTable('getSelections');
    $('#btnOpenPrtyAceptar').removeClass('disabled');
    v_idParty = v_elementByIdParty[0].id_party.replace(/\|/g, '');
    $('input[type=text][class="form-control"][placeholder="Buscar"]').prop("value", v_idParty);

    var v_urlGetPartyByIdParty = $('#urlGetPartyByIdParty').data('url');
    var v_iO = null;

    v_iObject.PRTGLOB.Party.PrtGlob.Pertenece = 1;
    var v_argTemp1 = v_idParty.toUpperCase();

    {
        $.post($('#urlAceptaParty').data('url'), {
            LlaveIng: v_argTemp1,
            modo: true,
            iObject: JSON.stringify(v_iObject),
            "_": $.now(),
        }, function (data) {
            if (data) {
                if (data['ErrorCode'] == 10) {
                    $('#modalPrtyAdv01').modal();
                } else if (data['ErrorCode'] == 10) {
                    $('#modalPartyMensajeNoExiste').modal();
                } else if (data['ErrorCode'] == 100) {
                    v_iO = data['iObject'];
                    sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iO));
                    $('#modalPrtyEliminado').modal();
                }
                else {
                    v_iO = data['iObject'];
                    sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iO));

                    v_iObject = v_iO;

                    v_iObject.PRTGLOB.Party.PrtGlob.primera = 1;

                    if (v_iObject.PRTYENT.EstadoParty != 0) {
                        v_iObject.PRTGLOB.Party.PrtGlob.Pertenece = 0;
                    }

                    //(moo) validar acceso segun usuario

                    if (v_iObject.PRTGLOB.Party.tipo == 1) {
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

                            $.post($('#urlSygetn_Ejecutivos').data('url'), {
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
                            });
                        }
                    });
                }
            } else {
                $('input[type=text][class="form-control"][placeholder="Buscar"]').focus();
            }
        });
    }

});

//Busqueda de participante por la razon social
var buscarByRazonSocial = function (PrmconfigColumnas) {
    $('#btnSelPartyByRazonSocial').hide();

    var v_searchRazonSocial = $('#searchRazonSocial').val();
    $('#searchRazonSocial').val($.trim(v_searchRazonSocial));

    if (v_searchRazonSocial != "") {
        var url = $('#urlGetListaDeParticipantesByRazonSocial').data('url');

        $('#pnlResultados2').show();

        $('#tablaSearchByRazonSocial').bootstrapTable('destroy');
        $('#tablaSearchByRazonSocial').bootstrapTable({
            height: getHeightSearchByRazonSocial(),
            url: url,
            queryParams: function (p) {
                return {
                    razonSocial: v_searchRazonSocial
                };
            },
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

//Llenado inicial de los participantes asignados a el en la grilla principal
var buscarByIdParty = function (PrmconfigColumnas) {
    var url = $('#urlGetListaDeParticipanteByUser').data('url');

    $('#tablaSearchByIdParty').bootstrapTable('destroy');
    $('#tablaSearchByIdParty').bootstrapTable({
        height: getHeightSearchByIdPrty(),
        url: url,
        showHeader: false,
        locale: "es-SP",
        columns: PrmconfigColumnas,
        searchAlign: 'left',
        showRefresh: false,
        clickToSelect: true,
        search: true,
        checkboxHeader: false,
        toolbar: '#toolbar-by-id-party',
        striped: true,
        pagination: true,
        pageSize: 10,
        pageList: [10, 25, 50, 100, 200]
    });
};

function getHeightSearchByIdPrty() {
    return 500;
}

function getHeightSearchByRazonSocial() {
    return 500; 
}
