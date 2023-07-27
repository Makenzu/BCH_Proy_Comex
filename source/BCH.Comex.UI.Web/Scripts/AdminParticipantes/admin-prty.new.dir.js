$(document).ready(function () {
    $('#toolbarMainPrty').hide();
    $('#mnuMainArchivo').hide();
    $('#mnuMainOpciones').hide();
    $("#btnDirEliminarNPrty").addClass('disabled');

    var dataDirec;

    if (v_iObject) {
        if (v_iObject.PRTGLOB) {
            if (v_iObject.PRTGLOB.direc) {

                if (v_iObject.PRTGLOB.direc.length > 0) {
                    var direc = v_iObject.PRTGLOB.direc;
                    $('#direccion').val(direc[0].direccion);

                    if (direc[0].CodComuna != 0 && direc[0].CodComuna != null) {
                        $('#comuna').val(direc[0].CodComuna);
                    }

                    if (direc[0].region != 0 && direc[0].region != null) {
                        $('#region').val(direc[0].region);
                    }

                    $('#ciudad').val(direc[0].ciudad);
                    $('#pais').val(direc[0].CodPais);
                    $('#correoElectronico').val(direc[0].email);
                    $('#telefono').val(direc[0].telefono);

                    if (direc[0].direccion.trim().length > 0) {
                        dataDirec = v_iObject.PRTGLOB.direc;
                    } else {
                        v_iObject.PRTGLOB.direc.pop();
                    }
                }

                $.get($('#urlGetPreviousDireccion').data('url'), { key: v_iObject.PRTGLOB.Party.idparty }, function (data) {
                    if (data.Direccion !== "") {

                        $('#direccion').val(data.Direccion);

                        if (data.CodComuna != 0 && data.CodComuna != null) {
                            $('#comuna').val(data.CodComuna);
                        }

                        if (data.Region != 0 && data.Region != null) {
                            $('#region').val(data.Region);
                        }

                        $('#ciudad').val(data.Ciudad);
                        $('#pais').val(data.CodPais);
                        $('#correoElectronico').val(data.Email);
                        $('#telefono').val(data.Telefono);

                        if (data.Direccion.trim().length > 0) {
                            let dir_ = {
                                direccion: data.Direccion,
                                comuna: data.Comuna,
                                cod_comuna: data.CodComuna,
                                cod_postal: data.CodPostal,
                                region: data.Region,
                                ciudad: data.Ciudad,
                                pais: data.Pais,
                                cod_pais: data.CodPais,
                                telefono: data.Telefono,
                                fax: data.Fax,
                                telex: data.Telex,
                                cas_postal: data.CasPostal,
                                cas_banco: data.CasBanco,
                                envio_sce: data.EnviarA,
                                recibe_sce: data.Recibe,
                                email: data.Email,
                                borrado: data.Borrado
                            }

                            dataDirec = dir_;
                        } else {
                            v_iObject.PRTGLOB.direc.pop();
                        }
                    }
                });
            }
        }
    }

    var cfgColDireccion = [{
        field: 'state',
        radio: true,
        visible: true
    }, {
        field: 'direccion',
        align: 'left'
    }, {
        field: 'ciudad',
        align: 'left'
    }, {
        field: 'borrado',
        align: 'right',
        formatter: fmtActivoInactivo
    }];


    $('#tableDireccionNuePrty').bootstrapTable('destroy');
    $('#tableDireccionNuePrty').bootstrapTable({
        height: 200,
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

    $('#tableDireccionNuePrty').on('check.bs.table', function (row, $element) {
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

        $('#direccion').focus();
        $("#btnDirEliminarNPrty").removeClass('disabled');
        $("#btnDirNuevoNPrty").text('Actualizar');
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

    $('#direccion, #ciudad').keyup(function (e) {
        $('#direccion').parents(".col-md-6").removeClass("has-error");
        $('#ciudad').parents(".col-md-6").removeClass("has-error");
        $('#btnDirAceptar').removeClass('disabled');
        f_messageBlock_Off();
    });
});

$('#btnDirAceptar').click(function () {
    $('#direccion').parents(".col-md-6").removeClass("has-error");
    $('#ciudad').parents(".col-md-6").removeClass("has-error");

    if ($("#btnDirAceptar").hasClass('disabled')) {
        return;
    }

    var direccion = $('#direccion');
    var ciudad = $('#ciudad');

    if (!S(direccion.val()).isEmpty()) {
        var regex = /[^a-zA-Z0-9.,\s]/g;
        var direccionEsInvalida = regex.test(direccion.val());

        if (direccionEsInvalida) {
            direccion.parents(".col-md-6").addClass("has-error");
            direccion.focus();
            f_messageBlock('Atención', 'La direccion solo debe contener letras, números, (puntos) y (comas)', 'warning');
            return;
        }
    } else {
        direccion.parents(".col-md-6").addClass("has-error");
        direccion.focus();
        f_messageBlock('Atención', 'Debe ingresar una Dirección', 'warning');
        return;
    }

    if (S(ciudad.val()).isEmpty()) {
        ciudad.parents(".col-md-6").addClass("has-error");
        ciudad.focus();
        f_messageBlock('Atención', 'Debe ingresar una Ciudad', 'warning');
        return;
    }

    var dir = null;

    if (v_iObject.PRTGLOB.direc.length == 0) {
        dir = {};
        dir.estado = 2;
        dir.direccion = $('#direccion').val();
        dir.comuna = $('#comuna option:selected').text();
        dir.CodComuna = $('#comuna').val();
        dir.codpostal = $('#codPostal').val();
        dir.region = $('#region').val();
        dir.ciudad = $('#ciudad').val();
        dir.pais = $('#pais option:selected').text();
        dir.CodPais = $('#pais').val();
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

    if (v_iObject) {
        if (v_iObject.PRTGLOB) {
            if (v_iObject.PRTGLOB.direc) {

                sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iObject));
                var pruebatest = v_iObject.PRTGLOB.Party.swif;

                $.get($('#urlGetHasDirecciones').data('url'), { key: v_iObject.PRTGLOB.Party.idparty }, function (data) {
                    debugger;
                    v_iObject.PRTGLOB.direc[0].estado = data.Estado;

                    $.post(
                        $('#urlDataObject').data('url'), { iO: JSON.stringify(v_iObject), "_": $.now() },
                        function (data) {
                            debugger;
                                if (data) {
                                    sessionStorage.setItem(_I_OBJECT_, JSON.stringify(data));
                                    v_iObject = data;
                                    $('#tableDireccion').bootstrapTable('refresh');
                                    $("#btnDirAceptar").addClass('disabled');
                                    $('#btnDirCancelar').removeClass('disabled');
                                    window.location.href = $('#urlCuentasParticipante').data('url');
                                }
                            }
                    );
                });

            } else {
                $('#direccion').focus();
            }
        } else {
            $('#direccion').focus();
        }
    } else {
        $('#direccion').focus();
    }
});

$('#btnDirGuardar').click(function () {
    $('#direccion').parents(".col-md-6").removeClass("has-error");
    $('#ciudad').parents(".col-md-6").removeClass("has-error");

    if ($("#btnDirAceptar").hasClass('disabled')) {
        return;
    }

    var direccion = $('#direccion');
    var ciudad = $('#ciudad');

    if (!S(direccion.val()).isEmpty()) {
        var regex = /[^a-zA-Z0-9.,\s]/g;
        var direccionEsInvalida = regex.test(direccion.val());

        if (direccionEsInvalida) {
            direccion.parents(".col-md-6").addClass("has-error");
            direccion.focus();
            f_messageBlock('Atención', 'La direccion solo debe contener letras, números, (puntos) y (comas)', 'warning');
            return;
        }
    } else {
        direccion.parents(".col-md-6").addClass("has-error");
        direccion.focus();
        f_messageBlock('Atención', 'Debe ingresar una Dirección', 'warning');
        return;
    }

    if (S(ciudad.val()).isEmpty()) {
        ciudad.parents(".col-md-6").addClass("has-error");
        ciudad.focus();
        f_messageBlock('Atención', 'Debe ingresar una Ciudad', 'warning');
        return;
    }

    var dir = null;

    if (v_iObject.PRTGLOB.direc.length == 0) {
        dir = {};
        dir.estado = 2;
        dir.direccion = $('#direccion').val();
        dir.comuna = $('#comuna option:selected').text();
        dir.CodComuna = $('#comuna').val();
        dir.codpostal = $('#codPostal').val();
        dir.region = $('#region').val();
        dir.ciudad = $('#ciudad').val();
        dir.pais = $('#pais option:selected').text();
        dir.CodPais = $('#pais').val();
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

    if (v_iObject) {
        if (v_iObject.PRTGLOB) {
            if (v_iObject.PRTGLOB.direc) {
                debugger;
                sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iObject));
                var pruebatest = v_iObject.PRTGLOB.Party.swif;

                $.get($('#urlGetHasDirecciones').data('url'), { key: v_iObject.PRTGLOB.Party.idparty }, function (data) {

                    v_iObject.PRTGLOB.direc[0].estado = data.Estado;
                    v_iObject.PRTGLOB.direc[0].direccion = $('#direccion').val();
                    v_iObject.PRTGLOB.direc[0].comuna = $('#comuna option:selected').text();
                    v_iObject.PRTGLOB.direc[0].CodComuna = $('#comuna').val();
                    v_iObject.PRTGLOB.direc[0].codpostal = $('#codPostal').val();
                    v_iObject.PRTGLOB.direc[0].region = $('#region').val();
                    v_iObject.PRTGLOB.direc[0].ciudad = $('#ciudad').val();
                    v_iObject.PRTGLOB.direc[0].pais = $('#pais option:selected').text();
                    v_iObject.PRTGLOB.direc[0].CodPais = $('#pais').val();
                    v_iObject.PRTGLOB.direc[0].telefono = $('#telefono').val();
                    v_iObject.PRTGLOB.direc[0].fax = $('#fax').val();
                    v_iObject.PRTGLOB.direc[0].telex = $('#telex').val();
                    v_iObject.PRTGLOB.direc[0].CasPostal = $('#casillaPostal').val();
                    v_iObject.PRTGLOB.direc[0].CasBanco = $('#casillaBancoChile').val();

                    switch ($('input:radio[name=optionCorrespondencia]:checked').val()) {
                        case 'optionDireccion':
                            v_iObject.PRTGLOB.direc[0].enviar_a = 0;
                            break;

                        case 'optionFax':
                            v_iObject.PRTGLOB.direc[0].enviar_a = 1;
                            break;

                        case 'optionCasillaBancoChile':
                            v_iObject.PRTGLOB.direc[0].enviar_a = 2;
                            break;

                        case 'optionCasillaPostal':
                            v_iObject.PRTGLOB.direc[0].enviar_a = 3;
                            break;

                        case 'optionEmail':
                            v_iObject.PRTGLOB.direc[0].enviar_a = 4;
                            break;

                        default:
                            v_iObject.PRTGLOB.direc[0].enviar_a = 0;
                            break;
                    }

                    v_iObject.PRTGLOB.direc[0].email = $('#correoElectronico').val();
                    v_iObject.PRTGLOB.direc[0].borrado = "0";

                    $.post($('#urlDataObjectSaveAddress').data('url'),
                        { iO: JSON.stringify(v_iObject), idParty: v_iObject.PRTGLOB.Party.idparty, "_": $.now() },
                        function (data) {
                            debugger;
                            if (data) {
                                sessionStorage.setItem(_I_OBJECT_, JSON.stringify(data.iObject));
                                v_iObject = data.iObject;
                                $('#tableDireccion').bootstrapTable('refresh');
                                $("#btnDirGuardar").addClass('disabled');
                                $('#btnDirCancelar').removeClass('disabled');

                                if (data.ErrorCode === 0)
                                    f_messageBlock('Grabar Participante', 'Datos del Participante grabados con Exito!', 'success');
                                else
                                    f_messageBlock('Error Grabar Participante', data.Message, 'danger');
                            }
                        }
                    )
                });
            } else {
                $('#direccion').focus();
            }
        } else {
            $('#direccion').focus();
        }
    } else {
        $('#direccion').focus();
    }
});

$('#btnDirNuevoNPrty').click(function () {

    if ($('#direccion').val() == '')
        return;

    if (v_iObject) {
        if (v_iObject.PRTGLOB) {
            if (v_iObject.PRTGLOB.direc) {
                var selected = $('#tableDireccionNuePrty').bootstrapTable('getSelections');
                if (selected.length > 0) {
                    for (ind = 0 ; ind < v_iObject.PRTGLOB.direc.length ; ind++) {
                        if (v_iObject.PRTGLOB.direc[ind].direccion == selected[0].direccion) {
                            v_iObject.PRTGLOB.direc[ind].estado = 2;
                            v_iObject.PRTGLOB.direc[ind].direccion = $('#direccion').val();
                            v_iObject.PRTGLOB.direc[ind].comuna = $('#comuna option:selected').text();
                            v_iObject.PRTGLOB.direc[ind].CodComuna = $('#comuna').val();
                            v_iObject.PRTGLOB.direc[ind].codpostal = $('#codPostal').val();
                            v_iObject.PRTGLOB.direc[ind].region = $('#region').val();
                            v_iObject.PRTGLOB.direc[ind].ciudad = $('#ciudad').val();
                            v_iObject.PRTGLOB.direc[ind].pais = $('#pais option:selected').text();
                            v_iObject.PRTGLOB.direc[ind].CodPais = $('#pais').val();
                            v_iObject.PRTGLOB.direc[ind].telefono = $('#telefono').val();
                            v_iObject.PRTGLOB.direc[ind].fax = $('#fax').val();
                            v_iObject.PRTGLOB.direc[ind].telex = $('#telex').val();
                            v_iObject.PRTGLOB.direc[ind].CasPostal = $('#casillaPostal').val();
                            v_iObject.PRTGLOB.direc[ind].CasBanco = $('#casillaBancoChile').val();

                            switch ($('input:radio[name=optionCorrespondencia]:checked').val()) {
                                case 'optionDireccion':
                                    v_iObject.PRTGLOB.direc[ind].enviar_a = 0;
                                    break;

                                case 'optionFax':
                                    v_iObject.PRTGLOB.direc[ind].enviar_a = 1;
                                    break;

                                case 'optionCasillaBancoChile':
                                    v_iObject.PRTGLOB.direc[ind].enviar_a = 2;
                                    break;

                                case 'optionCasillaPostal':
                                    v_iObject.PRTGLOB.direc[ind].enviar_a = 3;
                                    break;

                                case 'optionEmail':
                                    v_iObject.PRTGLOB.direc[ind].enviar_a = 4;
                                    break;

                                default:
                                    dir.enviar_a = 0;
                                    break;
                            }

                            v_iObject.PRTGLOB.direc[ind].email = $('#correoElectronico').val();
                            v_iObject.PRTGLOB.direc[ind].borrado = "0";
                            $('#tableDireccionNuePrty').bootstrapTable('uncheckAll');
                            $("#btnDirNuevoNPrty").text('Agregar');
                            break;
                        }
                    }
                }
                else {
                    dir = {};
                    dir.estado = 2;
                    dir.direccion = $('#direccion').val();
                    dir.comuna = $('#comuna option:selected').text();
                    dir.CodComuna = $('#comuna').val();
                    dir.codpostal = $('#codPostal').val();
                    dir.region = $('#region').val();
                    dir.ciudad = $('#ciudad').val();
                    dir.pais = $('#pais option:selected').text();
                    dir.CodPais = $('#pais').val();
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
                limpiarPantalla();
                $('#tableDireccionNuePrty').bootstrapTable('load', v_iObject.PRTGLOB.direc);
                //$('#btnDirAceptar').removeClass('disabled');
            }
        }
    }
});

$('#btnDirEliminarNPrty').click(function () {

    if ($("#btnDirEliminarNPrty").hasClass('disabled')) {
        return;
    }

    if (v_iObject) {
        if (v_iObject.PRTGLOB) {
            if (v_iObject.PRTGLOB.direc) {
                var list = [];

                v_iObject.PRTGLOB.direc.forEach(function (direc) {
                    if (direc.direccion != $('#direccion').val()) {
                        list.push(direc);
                    }
                });

                v_iObject.PRTGLOB.direc = list;
                sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iObject));

                $('#tableDireccionNuePrty').bootstrapTable('load', v_iObject.PRTGLOB.direc);
                if (list.length < 1) {
                    $("#btnDirAceptar").addClass('disabled');
                }
                $("#btnDirEliminarNPrty").addClass('disabled');
            }
        }
    }

    limpiarPantalla();
});

$('#btnDirCancelar').click(function () {
    $("#btnDirCancelar").addClass('disabled');
    v_iObject = null;
    sessionStorage.removeItem(_I_OBJECT_);
    window.location.href = $('#urlIndex').data('url');
});

$('#comuna').change(function () {
    var id = $(this).val();

    $.each(v_oComuna, function (key, value) {

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
});

function completarDatosDireccion(region, ciudad, pais) {
    $('#ciudad').val(ciudad);
    $("#region").val(region);
    $("#pais").val(pais);
    $('#ciudad').parents(".col-md-6").removeClass("has-error");
    f_messageBlock_Off();
}

function limpiarPantalla() {
    $('#direccion').focus();
    $('#direccion').val('');
    // evitamos el blanco
    $("#comuna option").first()[0].selected = true;
    $('#ciudad').val('');
    $('#casillaBancoChile').val('');
    $('#casillaPostal').val('');
    $('#correoElectronico').val('');
    $('#telefono').val('');
    $('#telex').val('');
    $('#fax').val('');
    $('#codPostal').val('');
}

function fmtActivoInactivo(value, row, index) {
    if (value == "0") {
        return "[Activo]";
    } else {
        return "[Inactivo]";
    }
};