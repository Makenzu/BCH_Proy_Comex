var g_nuevoPrty = 0;
var g_razonSocialNueva = 0;
var g_direccionNueva = 0;
var g_actionCancelar = 0;
var jerarquia = 0;

$(document).ready(function () {
    var v_sessionStorage;
    var v_prtyID = $('#memoryIdParty').val();
    g_razonSocialNueva = 0;
    g_direccionNueva = 0;
    g_actionCancelar = 0;

    //if (v_iObject) {
    //    if (v_iObject.PRTGLOB) {
    //        if (v_iObject.PRTGLOB.Party) {
    //            if (typeof v_iObject.PRTGLOB.Party.idparty !== 'undefined' && v_iObject.PRTGLOB.Party.idparty != '') {
    //                //sessionStorage.setItem('modificando', true);
    //}
    //        }
    //    }
    //}

    var data = sessionStorage;

    $('input#rutBanco').rut({
        formatOn: 'keyup'
    });
   
    v_sessionStorage = sessionStorage.getItem('oComuna');
    if (v_sessionStorage) {
        v_oComuna = JSON.parse(v_sessionStorage);
        v_oComuna_1 = JSON.parse(v_sessionStorage);
    }

    v_sessionStorage = sessionStorage.getItem('oPais');
    if (v_sessionStorage) {
        v_oComuna = JSON.parse(v_sessionStorage);
    }

    $('#btnRSAceptar').click(btnRSActionAceptar);
    $('#btnRSCancelar').click(btnActionCancelar);
    $('#btnRSNuevo').click(btnActionNuevo);
    $('#btnRSEliminar').click(btnRSActionEliminar);
    $('#btnRSConfirmEliminar').click(btnRSConfirmEliminar);

    $('#btnDirAceptar').click(btnActionDirAceptar);
    $('#btnDirCancelar').click(btnActionDirCancelar);
    $('#btnDirActivar').click(btnDirActivarModal);

    //(moo) detectamos un cambio en los input, habilitamos botones
    //$("input[type='text']").keyup(changeInputText);
    $("input[type='email']").keyup(changeInputText);
    $("input[type='tel']").keyup(changeInputText);
    $("select").change(changeInputText);
    
    //Metodos de los nuevos botones para ingresar direcciones y razones sociales
    $('#nuevaRazonSocialP').click(nuevaRazonSocialP);
    $('#nuevaDireccionP').click(nuevaDireccionP);

    // (moo) 2016-01-11 | Evento seleccion de los radios "Enviar Correspondencia"
    $('input:radio[name=optionCorrespondencia]').change(changeInputText);

    $('#nombreRazonSocial').focus();

    var cfgColRazonSocial = [{
        field: 'state',
        radio: true
    }, {
        field: 'nombre',
        align: 'left'
    }, {
        field: 'borrado',
        align: 'right',
        formatter: fmtActivoInactivo
    }, {
        field: 'fantasia',
        visible: false
    }, {
        field: 'contacto',
        visible: false
    }, {
        field: 'sortKey',
        visible: false
    }];

    var cfgColDireccion = [{
        field: 'state',
        radio: true,
        visible: true
    }, {
        field: 'direccion',
        align: 'left',
        sortable: true,
        order: 'desc'
    }, {
        field: 'ciudad',
        align: 'left'
    }, {
        field: 'borrado',
        align: 'right',
        formatter: fmtActivoInactivo
    }];

    function fmtActivoInactivo(value, row, index) {
        if (value == "0") {
            return "[Activo]";
        } else {
            return "[Inactivo]";
        }
    };

    //Se lee la jerarquia del usuario en cuestion
    //jerarquia = 1;
    jerarquia = v_iObject? (v_iObject.UsrEsp? (v_iObject.UsrEsp.Jerarquia) :0 ) : 0;
    var dataNom = v_iObject ? (v_iObject.PRTGLOB ? (v_iObject.PRTGLOB.nom ? v_iObject.PRTGLOB.nom : []) : []) : [];
    
    if (dataNom.length == 0 || jerarquia != 1) {
        $("#nuevaRazonSocialP").addClass('disabled');
    }

    $('#tableRazonSocial').bootstrapTable('destroy');
    $('#tableRazonSocial').bootstrapTable({
        height: getHeight(),
        data: dataNom,
        showHeader: false,
        locale: "es-SP",
        columns: cfgColRazonSocial,
        searchAlign: 'left',
        showRefresh: false,
        clickToSelect: true,
        search: false,
        checkboxHeader: false,
        singleSelect: true,
        striped: true,
        cache: false
    });

    var dataDirec = v_iObject ? (v_iObject.PRTGLOB ? (v_iObject.PRTGLOB.direc ? v_iObject.PRTGLOB.direc : []) : []) : [];
    
    if (dataDirec.length == 0 || jerarquia != 1) {
        $("#nuevaDireccionP").addClass('disabled');
    }

    $('#tableDireccion').bootstrapTable('destroy');
    $('#tableDireccion').bootstrapTable({
        height: getHeight(),
        data: dataDirec,
        showHeader: false,
        locale: "es-SP",
        columns: cfgColDireccion,
        searchAlign: 'left',
        showRefresh: false,
        clickToSelect: true,
        search: false,
        checkboxHeader: false,
        singleSelect: true,
        striped: true,
        cache: false
    });

    $('#tableRazonSocial').on('dbl-click-row.bs.table', function (row, $element) {

        //Si no tiene jerarquia de supervisor deshabilitamos los textbox de razon social
        if (jerarquia != 1) {
            $('#nombreRazonSocial').attr("disabled", "disabled");
            $('#nombreFantasia').attr("disabled", "disabled");
            $('#contacto').attr("disabled", "disabled");
            $('#sortKey').attr("disabled", "disabled");
            $('#rutBanco').attr("disabled", "disabled");
            $('#btnRSAceptar').addClass('disabled');
            $('#btnRSNuevo').addClass('disabled');
            $('#btnRSEliminar').addClass('disabled');
        }

        $('#oldNombreRazonSocial').val($element['nombre']);
        $('#nombreRazonSocial').val($element['nombre']);
        $('#nombreFantasia').val($element['fantasia']);
        $('#contacto').val($element['contacto']);
        $('#sortKey').val($element['sortkey']);
        $('#rutBanco').val($element['rut']);
        $('#estadoRazonSocial').val($element['borrado']);

        g_razonSocialNueva = 0;
        g_direccionNueva = 0;
        g_actionCancelar = 0;

        if ($element['borrado']== 0) {
            $('#btnRSEliminar').removeClass('disabled');
        }
        else {
            $('#btnRSEliminar').addClass('disabled');
        }

        $('#modalRazonSocial').modal();
        $('#nombreRazonSocial').focus();
    });

    $('#tableDireccion').on('dbl-click-row.bs.table', function (row, $element) {

        //Si no tiene jerarquia de supervisor deshabilitamos los textbox de direccion
        if (jerarquia != 1) {
            $('#direccion').attr("disabled", "disabled");
            $('#comuna').attr("disabled", "disabled");
            $('#ciudad').attr("disabled", "disabled");
            $('#region').attr("disabled", "disabled");
            $('#pais').attr("disabled", "disabled");
            $('#casillaBancoChile').attr("disabled", "disabled");
            $('#casillaPostal').attr("disabled", "disabled");
            $('#correoElectronico').attr("disabled", "disabled");
            $('#telefono').attr("disabled", "disabled");
            $('#fax').attr("disabled", "disabled");
            $('#telex').attr("disabled", "disabled");
            $('#codPostal').attr("disabled", "disabled");
            $('#btnDirAceptar').addClass('disabled');
            $('#btnDirNuevo').addClass('disabled');
            $('#btnDirEliminar').addClass('disabled');
            $('#btnDirActivar').addClass('disabled');
            $('#optionDireccion').attr("disabled", "disabled");
            $('#optionFax').attr("disabled", "disabled");
            $('#optionCasillaBancoChile').attr("disabled", "disabled");
            $('#optionCasillaPostal').attr("disabled", "disabled");
            $('#optionEmail').attr("disabled", "disabled");
        }

        $('#oldDireccion').val($element['direccion']);
        $('#direccion').val($element['direccion']);
        $('#comuna').val($element['CodComuna']);
        $('#ciudad').val($element['ciudad']);
        $('#region').val($element['region']);
        $('#pais').val($element['CodPais']);
        $('#casillaBancoChile').val($element['CasBanco']);
        $('#casillaPostal').val($element['CasPostal']);
        $('#telefono').val($element['telefono']);
        $('#fax').val($element['fax']);
        $('#telex').val($element['telex']);
        $('#codPostal').val($element['codpostal']);
        $('#correoElectronico').val($element['email']);
        
        g_razonSocialNueva = 0;
        g_direccionNueva = 0;
        g_actionCancelar = 0;

            // (moo) 2016-01-11 | Correción, mostrar "Enviar Correspondencia" según corresponda
        switch ($element['enviar_a']) {
            case 0:
                $('#optionDireccion').prop('checked', true);
                break;

            case 1:
                $('#optionFax').prop('checked', true);
                break;

            case 2:
                $('#optionCasillaBancoChile').prop('checked', true);
                break;

            case 3:
                $('#optionCasillaPostal').prop('checked', true);
                break;

            case 4:
                $('#optionEmail').prop('checked', true);
                break;
        }
        
        if ($element['borrado'] == 0) {
            $('#btnDirEliminar').removeClass('disabled');
            $('#btnDirActivar').addClass('disabled');
        }
        else {
            $('#btnDirEliminar').addClass('disabled');
            $('#btnDirActivar').removeClass('disabled');
        }

        $('#modalDireccion').modal();
        $('#direccion').focus();
    });
    
    //GVT : Para validar el RUT en el modal de la razon social
    $('input#rutBanco')
        .rut({ formatOn: 'keyup', validateOn: 'keyup change' })
        .on('rutInvalido', function () {
            if ($(this).val() != '') {
                $('#btnRSAceptar').attr('disabled', true);
                $(this).parents(".form-group").addClass("has-error");
            } else {
                $(this).parents(".form-group").removeClass("has-error");
                $('#btnRSAceptar').attr('disabled', false);
            }
        }).on('rutValido', function () {
            $(this).parents(".form-group").removeClass("has-error");
            $('#btnRSAceptar').attr('disabled', false);
        });

    $("#telefono, #fax, #telex, #casillaPostal ").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });

    $('#nombreRazonSocial, #sortKey').keyup(function (e) {
        $('#nombreRazonSocial').parents(".col-md-8").removeClass("has-error");
        $('#sortKey').parents(".col-md-8").removeClass("has-error");
    });

    $('#direccion, #ciudad').keyup(function (e) {
        $('#direccion').parents(".col-md-8").removeClass("has-error");
        $('#ciudad').parents(".col-md-8").removeClass("has-error");
    });
});

//Evento para el nuevo boton de razon social
var nuevaRazonSocialP = function () {

    if ($("#nuevaRazonSocialP").hasClass('disabled')) {
        return;
    }

    g_razonSocialNueva = 1;
    $('#oldNombreRazonSocial').val('');
    $('#nombreRazonSocial').val('');
    $('#nombreFantasia').val('');
    $('#contacto').val('');
    $('#sortKey').val('');
    $('#rutBanco').val('');
    $('#estadoRazonSocial').val('');
    $('#btnRSAceptar').removeClass('disabled');
    $('#btnRSEliminar').addClass('disabled');
    $('#modalRazonSocial').modal();
    $('#nombreRazonSocial').focus();
}

//Evento para el nuevo boton de direccion
var nuevaDireccionP = function () {
    if ($("#nuevaDireccionP").hasClass('disabled')) {
        return;
    }

    g_direccionNueva = 1;
    $('#oldDireccion').val('');
    $('#direccion').val('');
    $('#comuna').val(0);
    $('#ciudad').val('');
    $('#region').val('');
    $('#pais').val(0);
    $('#casillaBancoChile').val('');
    $('#casillaPostal').val('');
    $('#telefono').val('');
    $('#fax').val('');
    $('#telex').val('');
    $('#codPostal').val('');
    $('#correoElectronico').val('');
    $('#btnDirAceptar').removeClass('disabled');
    $('#btnDirEliminar').addClass('disabled');
    $('#btnDirActivar').addClass('disabled');
    $('#modalDireccion').modal();
    $('#direccion').focus();
}

var getHeight = function () {
    return ($(window).height() - $('nav').outerHeight(true) - $('.header').outerHeight(true) - $('#toolbarMainPrty').outerHeight(true)) / 4;
}

$('#modalRSMensajeEliminar').on('hidden.bs.modal', function (e) {
    window.location.href = $('#urlIndex').data('url');
});

var changeInputText = function () {
    if (g_razonSocialNueva == 0) {
        $('#btnRSAceptar').removeClass('disabled');
        $('#btnRSCancelar').removeClass('disabled');
        $('#btnRSNuevo').addClass('disabled');
        $('#btnRSEliminar').addClass('disabled');

        g_actionCancelar = 1;
    }

    if (g_direccionNueva == 0) {
        $('#btnDirAceptar').removeClass('disabled');
        $('#btnDirCancelar').removeClass('disabled');
        $('#btnDirNuevo').addClass('disabled');
        $('#btnDirEliminar').addClass('disabled');
        $('#btnDirActivar').addClass('disabled');

        g_actionCancelar = 1;
    }
};

var btnRSActionAceptar = function () {

    $('#nombreRazonSocial').parents(".col-md-8").removeClass("has-error");
    $('#sortKey').parents(".col-md-8").removeClass("has-error");

    if ($("#btnRSAceptar").hasClass('disabled')) {
        return;
    }

    if ($('#nombreRazonSocial').val() == '') {
        $('#nombreRazonSocial').parents(".col-md-8").addClass("has-error");
        return;
    }

    if ($('#sortKey').val() == '') {
        $('#sortKey').parents(".col-md-8").addClass("has-error");
        return;
    }

    if (g_razonSocialNueva == 1) {
        var nom = null;

        if (v_iObject) {
            if (v_iObject.PRTGLOB) {
                if (!v_iObject.PRTGLOB.nom) {
                    v_iObject.PRTGLOB.nom = [];
                }

                //Desactivo las demas razones sociales
                var list = [];
                var indiceMayor = 99;
                v_iObject.PRTGLOB.nom.forEach(function (nom) {                    
                    nom.estado = 3;
                    nom.borrado = "1";

                    if (indiceMayor = 99) {
                        indiceMayor = nom.indice;
                    }
                    else {
                        if (nom.indice > indiceMayor) {
                            indiceMayor = nom.indice;
                        }
                    }
                    list.push(nom);
                });

                nom = {};
                nom.estado = 2;
                nom.nombre = $('#nombreRazonSocial').val();
                nom.fantasia = $('#nombreFantasia').val();
                nom.contacto = $('#contacto').val();
                nom.sortkey = $('#sortKey').val().toUpperCase();
                nom.rut = $('#rutBanco').val();
                nom.borrado = "0";
                nom.indice = indiceMayor + 1;
                list.push(nom);

                v_iObject.PRTGLOB.nom = list; //Reasigno las nuevas razones sociales
                v_iObject.PRTGLOB.Party.estado = 5; //Para activar la modal
                sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iObject));
                $('#tableRazonSocial').bootstrapTable('load', v_iObject.PRTGLOB.nom);
            }
        }
    } else {
        if (v_iObject) {
            if (v_iObject.PRTGLOB) {
                if (v_iObject.PRTGLOB.nom) {
                    var list = [];
                    v_iObject.PRTGLOB.nom.forEach(function (nom) {
                        if (nom.nombre == $('#oldNombreRazonSocial').val()) {
                            nom.estado = 3;
                            nom.nombre = $('#nombreRazonSocial').val();
                            nom.fantasia = $('#nombreFantasia').val();
                            nom.contacto = $('#contacto').val();
                            nom.sortKey = $('#sortKey').val().toUpperCase();
                            nom.rut = $('#rutBanco').val();
                            nom.borrado = $('#estadoRazonSocial').val();
                        }
                        list.push(nom);
                    });
                    v_iObject.PRTGLOB.nom = list;
                } else {
                    v_iObject.PRTGLOB.nom = [];
                    nom = {};
                    nom.estado = 2;
                    nom.nombre = $('#nombreRazonSocial').val();
                    nom.fantasia = $('#nombreFantasia').val();
                    nom.contacto = $('#contacto').val();
                    nom.sortkey = $('#sortKey').val();
                    nom.rut = $('#rutBanco').val();
                    nom.borrado = "0";
                    v_iObject.PRTGLOB.nom.push(nom);
                }

                v_iObject.PRTGLOB.Party.estado = 5; //Para activar la modal
                sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iObject));
                $('#tableRazonSocial').bootstrapTable('load', v_iObject.PRTGLOB.nom);
            }
        }
    }

    //$("#btnRSAceptar").addClass('disabled');
    $('#btnRSCancelar').removeClass('disabled');
    $('#btnRSNuevo').removeClass('disabled');
    $('#btnRSEliminar').removeClass('disabled');
    $('#nombreRazonSocial').focus();

    g_actionCancelar = 0;
    g_razonSocialNueva = 0;
    f_saveDataOject();

    $('#modalRazonSocial').modal('hide');
};

var btnActionDirAceptar = function () {
    var dir = null;

    $('#direccion').parents(".col-md-8").removeClass("has-error");
    $('#ciudad').parents(".col-md-8").removeClass("has-error");

    if ($("#btnDirAceptar").hasClass('disabled')) {
        return;
    }

    if ($('#direccion').val() == '') {
        $('#direccion').parents(".col-md-8").addClass("has-error");
        return;
    }

    if ($('#ciudad').val() == '') {
        $('#ciudad').parents(".col-md-8").addClass("has-error");
        return;
    }

    if (g_direccionNueva == 1) {
        if (v_iObject) {
            if (v_iObject.PRTGLOB) {
                if (!v_iObject.PRTGLOB.direc) {
                    v_iObject.PRTGLOB.direc = [];
                }

                var indiceMayor = 99;
                v_iObject.PRTGLOB.direc.forEach(function (nom) {
                    if (indiceMayor = 99) {
                        indiceMayor = nom.indice;
                    }
                    else {
                        if (nom.indice > indiceMayor) {
                            indiceMayor = nom.indice;
                        }
                    }
                });

                dir = {};
                dir.estado = 2;
                dir.indice = indiceMayor + 1;
                dir.direccion = $('#direccion').val();
                dir.comuna = $('#comuna option:selected').text();
                dir.CodComuna = $('#comuna').val() ? $('#comuna').val() : 0;
                dir.codpostal = $('#codPostal').val();
                dir.region = $('#region').val();
                dir.ciudad = $('#ciudad').val();
                dir.pais = $('#pais option:selected').text();
                dir.CodPais = $('#pais').val() ? $('#pais').val() : 0;
                dir.telefono = $('#telefono').val();
                dir.fax = $('#fax').val();
                dir.telex = $('#telex').val();
                dir.CasPostal = $('#casillaPostal').val();
                dir.CasBanco = $('#casillaBancoChile').val();

                switch ($('input:radio[name=optionCorrespondencia]:checked').val()) {
                    case 'optionDireccion':
                        dir.enviar_a = 0;
                        break;

                    case 'optionFax':
                        dir.enviar_a = 1;
                        break;

                    case 'optionCasillaBancoChile':
                        dir.enviar_a = 2;
                        break;

                    case 'optionCasillaPostal':
                        dir.enviar_a = 3;
                        break;

                    case 'optionEmail':
                        dir.enviar_a = 4;
                        break;

                    default:
                        dir.enviar_a = 0;
                        break;
                }

                dir.email = $('#correoElectronico').val();
                dir.borrado = "0";
                v_iObject.PRTGLOB.direc.push(dir);
                v_iObject.PRTGLOB.Party.estado = 5; //Para activar la modal de guardar los cambios
                sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iObject));
                $('#tableDireccion').bootstrapTable('load', v_iObject.PRTGLOB.direc);
            }
        }
    } else {
        if (v_iObject) {
            if (v_iObject.PRTGLOB) {
                if (v_iObject.PRTGLOB.direc) {
                    var list = [];
                    v_iObject.PRTGLOB.direc.forEach(function (direc) {
                        if (direc.direccion == $('#oldDireccion').val()) {
                            direc.estado = 3;
                            direc.direccion = $('#direccion').val();
                            direc.comuna = $('#comuna option:selected').text();
                            direc.CodComuna = $('#comuna').val() ? $('#comuna').val() : 0;
                            direc.codpostal = $('#codPostal').val();
                            direc.region = $('#region').val();
                            direc.ciudad = $('#ciudad').val();
                            direc.pais = $('#pais option:selected').text();
                            direc.CodPais = $('#pais').val() ? $('#pais').val() : 0;
                            direc.telefono = $('#telefono').val();
                            direc.fax = $('#fax').val();
                            direc.telex = $('#telex').val();
                            direc.CasPostal = $('#casillaPostal').val();
                            direc.CasBanco = $('#casillaBancoChile').val();

                            switch ($('input:radio[name=optionCorrespondencia]:checked').val()) {
                                case 'optionDireccion':
                                    direc.enviar_a = 0;
                                    break;

                                case 'optionFax':
                                    direc.enviar_a = 1;
                                    break;

                                case 'optionCasillaBancoChile':
                                    direc.enviar_a = 2;
                                    break;

                                case 'optionCasillaPostal':
                                    direc.enviar_a = 3;
                                    break;

                                case 'optionEmail':
                                    direc.enviar_a = 4;
                                    break;

                                default:
                                    direc.enviar_a = 0;
                                    break;
                            }

                            direc.email = $('#correoElectronico').val();
                            direc.borrado = "0";
                        }
                        list.push(direc);
                    });
                    v_iObject.PRTGLOB.direc = list;
                } else {
                    v_iObject.PRTGLOB.direc = [];
                    dir = {};
                    dir.estado = 2;
                    dir.direccion = $('#direccion').val();
                    dir.comuna = $('#comuna option:selected').text();
                    dir.CodComuna = $('#comuna').val() ? $('#comuna').val() : 0;
                    dir.codpostal = $('#codPostal').val();
                    dir.region = $('#region').val();
                    dir.ciudad = $('#ciudad').val();
                    dir.pais = $('#pais option:selected').text();
                    dir.CodPais = $('#pais').val() ? $('#pais').val() : 0;
                    dir.telefono = $('#telefono').val();
                    dir.fax = $('#fax').val();
                    dir.telex = $('#telex').val();
                    dir.CasPostal = $('#casillaPostal').val();
                    dir.CasBanco = $('#casillaBancoChile').val();

                    switch ($('input:radio[name=optionCorrespondencia]:checked').val()) {
                        case 'optionDireccion':
                            dir.enviar_a = 0;
                            break;

                        case 'optionFax':
                            dir.enviar_a = 1;
                            break;

                        case 'optionCasillaBancoChile':
                            dir.enviar_a = 2;
                            break;

                        case 'optionCasillaPostal':
                            dir.enviar_a = 3;
                            break;

                        case 'optionEmail':
                            dir.enviar_a = 4;
                            break;

                        default:
                            dir.enviar_a = 0;
                            break;
                    }

                    dir.email = $('#correoElectronico').val();
                    dir.borrado = "0";
                    v_iObject.PRTGLOB.direc.push(dir);
                }               

                v_iObject.PRTGLOB.Party.estado = 5; //Para activar la modal de guardar los cambios
                sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iObject));
                $('#tableDireccion').bootstrapTable('load', v_iObject.PRTGLOB.direc);
            }
        }
    }

    /*
    //$("#btnDirAceptar").addClass('disabled');
    $('#btnDirCancelar').removeClass('disabled');
    $('#btnDirNuevo').removeClass('disabled');
    $('#btnDirEliminar').removeClass('disabled');
    $('#btnDirActivar').removeClass('disabled');

    $('#direccion').focus();

    g_razonSocialNueva = 0;
    g_direccionNueva = 0;
    g_actionCancelar = 0;

    f_saveDataOject();

    $('#modalDireccion').modal('hide');*/
};

var btnDirActivarModal = function () {

    if (v_iObject) {
        if (v_iObject.PRTGLOB) {
            if (v_iObject.PRTGLOB.direc) {
                var list = [];
                v_iObject.PRTGLOB.direc.forEach(function (direc) {

                    if (direc.direccion == $('#direccion').val()) {
                        direc.estado = 3;
                        direc.comuna = $('#comuna option:selected').text();
                        direc.CodComuna = $('#comuna').val() ? $('#comuna').val() : 0;
                        direc.codpostal = $('#codPostal').val();
                        direc.region = $('#region').val();
                        direc.ciudad = $('#ciudad').val();
                        direc.pais = $('#pais option:selected').text();
                        direc.CodPais = $('#pais').val() ? $('#pais').val() : 0;
                        direc.telefono = $('#telefono').val();
                        direc.fax = $('#fax').val();
                        direc.telex = $('#telex').val();
                        direc.CasPostal = $('#casillaPostal').val();
                        direc.CasBanco = $('#casillaBancoChile').val();

                        switch ($('input:radio[name=optionCorrespondencia]:checked').val()) {
                            case 'optionDireccion':
                                direc.enviar_a = 0;
                                break;

                            case 'optionFax':
                                direc.enviar_a = 1;
                                break;

                            case 'optionCasillaBancoChile':
                                direc.enviar_a = 2;
                                break;

                            case 'optionCasillaPostal':
                                direc.enviar_a = 3;
                                break;

                            case 'optionEmail':
                                direc.enviar_a = 4;
                                break;

                            default:
                                direc.enviar_a = 0;
                                break;
                        }

                        direc.email = $('#correoElectronico').val();
                        direc.borrado = "0";
                    }
                    list.push(direc);
                });

                v_iObject.PRTGLOB.direc = list;
                v_iObject.PRTGLOB.Party.estado = 5; //Para activar la modal de guardar los cambios
                sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iObject));
                $('#tableDireccion').bootstrapTable('load', v_iObject.PRTGLOB.direc);
                $('#tableDireccion').bootstrapTable('refresh');
                v_iObject.PRTGLOB.Party.estado = 5;
            }
        }
    }

    f_saveDataOject();
    $('#modalDireccion').modal('hide');
}

var btnActionNuevo = function () {
    if ($("#btnRSNuevo").hasClass('disabled')) {
        return;
    }
    
    g_razonSocialNueva = 1;
    $('#nombreRazonSocial').focus();
    $('#nombreRazonSocial').val('');
    $('#nombreFantasia').val('');
    $('#contacto').val('');
    $('#sortKey').val('');
    $('#rutBanco').val('');
    $("#btnRSAceptar").removeClass('disabled');
    $('#btnRSCancelar').removeClass('disabled');
    $('#btnRSNuevo').addClass('disabled');
    $('#btnRSEliminar').addClass('disabled');
    g_actionCancelar = 1;
};

$('#btnDirNuevo').click(function () {
    if ($("#btnDirNuevo").hasClass('disabled')) {
        return;
    }

    g_direccionNueva = 1;
    $('#direccion').focus();
    $('#direccion').val('');
    $('#comuna').val('');
    $('#ciudad').val('');
    $('#casillaBancoChile').val('');
    $('#casillaPostal').val('');
    $('#correoElectronico').val('');
    $('#telefono').val('');
    $('#telex').val('');
    $('#fax').val('');
    $('#codPostal').val('');
    $("#btnDirAceptar").removeClass('disabled');
    $('#btnDirCancelar').removeClass('disabled');
    $('#btnDirNuevo').addClass('disabled');
    $('#btnDirEliminar').addClass('disabled');
    $('#btnDirActivar').addClass('disabled');
    g_actionCancelar = 1;
});

$('#btnDirEliminar').click(function () {
    if ($("#btnDirEliminar").hasClass('disabled')) {
        return;
    }

    $('#textDireccion').text($('#direccion').val());
    $('#modalDirEliminar').modal();
});

var btnRSActionEliminar = function () {
    if ($("#btnRSEliminar").hasClass('disabled')) {
        return;
    }

    $('#textNombreRazonSocial').text($('#nombreRazonSocial').val());
    $('#textNombreFantasia').text($('#nombreFantasia').val());
    $('#modalRSEliminar').modal();
};

//Aqui se confirma la eliminacion de razon social de la memoria
var btnRSConfirmEliminar = function () {
    if ($("#btnRSEliminar").hasClass('disabled')) {
        return;
    }

    if (v_iObject) {
        if (v_iObject.PRTGLOB) {

            if (v_iObject.PRTGLOB.nom) {
                var list = [];
                v_iObject.PRTGLOB.nom.forEach(function (nom) {
                    if (nom.nombre == $('#oldNombreRazonSocial').val()) {
                        nom.estado = 5;
                        nom.nombre = $('#nombreRazonSocial').val();
                        nom.fantasia = $('#nombreFantasia').val();
                        nom.contacto = $('#contacto').val();
                        nom.sortKey = $('#sortKey').val();
                        nom.rut = $('#rutBanco').val();
                        nom.borrado = "1";
                    }
                    list.push(nom);
                });
                v_iObject.PRTGLOB.nom = list;
            }

            v_iObject.PRTGLOB.Party.estado = 5;
            sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iObject));
            $('#tableRazonSocial').bootstrapTable('load', v_iObject.PRTGLOB.nom);
        }
    }

    f_saveDataOject();
    $('#modalRSEliminar').modal('hide');
    $('#modalRazonSocial').modal('hide');
};

//Aqui se codifica la eliminación de direccion de memoria
$('#btnDirConfirmEliminar').click(function () {
    if ($("#btnDirEliminar").hasClass('disabled')) {
        return;
    }

    if (v_iObject) {
        if (v_iObject.PRTGLOB) {
            if (v_iObject.PRTGLOB.direc) {
                var list = [];
                v_iObject.PRTGLOB.direc.forEach(function (direc) {
                    if (direc.direccion == $('#direccion').val()) {
                        direc.estado = 5;
                        direc.comuna = $('#comuna option:selected').text();
                        direc.CodComuna = $('#comuna').val() ? $('#comuna').val() : 0;
                        direc.codpostal = $('#codPostal').val();
                        direc.region = $('#region').val();
                        direc.ciudad = $('#ciudad').val();
                        direc.pais = $('#pais option:selected').text();
                        direc.CodPais = $('#pais').val() ? $('#pais').val() : 0;
                        direc.telefono = $('#telefono').val();
                        direc.fax = $('#fax').val();
                        direc.telex = $('#telex').val();
                        direc.CasPostal = $('#casillaPostal').val();
                        direc.CasBanco = $('#casillaBancoChile').val();

                        switch ($('input:radio[name=optionCorrespondencia]:checked').val()) {
                            case 'optionDireccion':
                                direc.enviar_a = 0;
                                break;

                            case 'optionFax':
                                direc.enviar_a = 1;
                                break;

                            case 'optionCasillaBancoChile':
                                direc.enviar_a = 2;
                                break;

                            case 'optionCasillaPostal':
                                direc.enviar_a = 3;
                                break;

                            case 'optionEmail':
                                direc.enviar_a = 4;
                                break;

                            default:
                                direc.enviar_a = 0;
                                break;
                        }

                        direc.email = $('#correoElectronico').val();
                        direc.borrado = "1";
                    }
                    list.push(direc);
                });
                v_iObject.PRTGLOB.direc = list;
                v_iObject.PRTGLOB.Party.estado = 5; //Para activar la modal de guardar los cambios
                sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iObject));
                $('#tableDireccion').bootstrapTable('load', v_iObject.PRTGLOB.direc);
            }
        }
    }

    f_saveDataOject();
    $('#modalDirEliminar').modal('hide');
    $('#modalDireccion').modal('hide');
});

var btnActionCancelar = function () {
    //$("#btnRSAceptar").addClass('disabled');
    $('#btnRSCancelar').removeClass('disabled');
    $('#btnRSNuevo').removeClass('disabled');
    $('#btnRSEliminar').removeClass('disabled');
    $('#nombreRazonSocial').focus();
    $('#modalRazonSocial').modal('hide');
    g_razonSocialNueva = 0;
};

var btnActionDirCancelar = function () {

    //$("#btnDirAceptar").addClass('disabled');
    $('#btnDirCancelar').removeClass('disabled');
    $('#btnDirNuevo').removeClass('disabled');
    $('#btnDirEliminar').removeClass('disabled');
    $('#btnDirActivar').removeClass('disabled');

    $('#modalDireccion').modal('hide');

    g_razonSocialNueva = 0;
}

// (moo) 2015-09-25 Si no existe información de busqueda, copiamos la razón social
$('#nombreRazonSocial').blur(function () {
    var v_sortKey = $('#sortKey').val();
    if (!v_sortKey || v_sortKey.length == 0) {
        $('#sortKey').val($('#nombreRazonSocial').val().toUpperCase());
    }
});

//
// (moo) 2015-09-30
//
$('#modalRazonSocial').on('hidden.bs.modal', function () {
    $(this).removeData('bs.modal');
});

$('#modalDireccion').on('hidden.bs.modal', function () {
    $(this).removeData('bs.modal');
});

$('#comuna').change(function () {
    var id = $(this).val();

    $.each(v_oComuna_1, function (key, value) {

        if (value.codigo == id) {

            switch (value.region) {
                case 1:
                    completarDatosDireccion("PRIMERA", value.nombre, "997");
                    break;
                case 2:
                    completarDatosDireccion("SEGUNDA", value.nombre, "997");
                    break;
                case 3:
                    completarDatosDireccion("TERCERA", value.nombre, "997");
                    break;
                case 4:
                    completarDatosDireccion("CUARTA", value.nombre, "997");
                    break;
                case 5:
                    completarDatosDireccion("QUINTA", value.nombre, "997");
                    break;
                case 6:
                    completarDatosDireccion("SEXTA", value.nombre, "997");
                    break;
                case 7:
                    completarDatosDireccion("SEPTIMA", value.nombre, "997");
                    break;
                case 8:
                    completarDatosDireccion("OCTAVA", value.nombre, "997");
                    break;
                case 9:
                    completarDatosDireccion("NOVENA", value.nombre, "997");
                    break;
                case 10:
                    completarDatosDireccion("DECIMA", value.nombre, "997");
                    break;
                case 11:
                    completarDatosDireccion("UNDECIMA", value.nombre, "997");
                    break;
                case 12:
                    completarDatosDireccion("DUODECIMA", value.nombre, "997");
                    break;
                case 13:
                    completarDatosDireccion("METROPOLITANA", value.nombre, "997");
                    break;
                default:
                    completarDatosDireccion("EXTRANJERA", value.nombre, "999");
                    break;
            }
            return;
        }
    });
})

function completarDatosDireccion(region, ciudad, pais) {
    $('#ciudad').val(ciudad);
    $("#region").val(region);
    $("#pais").val(pais);
}