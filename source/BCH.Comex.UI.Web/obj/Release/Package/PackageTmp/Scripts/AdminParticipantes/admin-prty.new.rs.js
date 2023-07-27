var g_nuevoPrty = 0;
var g_razonSocialNueva = 0;
var g_direccionNueva = 0;
var g_actionCancelar = 0;

$(document).ready(function () {

    if (v_iObject) {
        if (v_iObject.PRTGLOB) {
            if (v_iObject.PRTGLOB.nom) {
                $.get($('#urlGetPreviousRazonSocial').data('url'), { key: v_iObject.PRTGLOB.Party.idparty }, function (data) {
                    if (data.Nombre) {
                        $('#nombreRazonSocial').val(data.Nombre);
                        $('#nombreFantasia').val(data.Fantasia);
                        $('#sortKey').val(data.SortKey);
                    } else {
                        $('#nombreRazonSocial').val(v_iObject.PRTGLOB.nom[0].nombre);
                        $('#nombreFantasia').val(v_iObject.PRTGLOB.nom[0].nombre);
                        $('#sortKey').val(v_iObject.PRTGLOB.nom[0].nombre);
                    }
                });
            }
        }
    }    

    $('#toolbarMainPrty').hide();
    $('#mnuMainArchivo').hide();
    $('#mnuMainOpciones').hide();
    $('#btnRSAceptar').click(btnRSActionAceptar);
    $('#btnRSGuardar').click(btnRSActionGuardar);

    $('input#rutBanco')
        .rut({ formatOn: 'keyup', validateOn: 'keyup change' })
        .on('rutInvalido', function () {
            if ($(this).val() != '') {
                $('#btnRSAceptar').attr('disabled', true);
                $(this).parents(".form-group").addClass("has-error");
            }
            else {
                $(this).parents(".form-group").removeClass("has-error");
                $('#btnRSAceptar').attr('disabled', false);
            }
        })
        .on('rutValido', function () {
            $(this).parents(".form-group").removeClass("has-error");
            $('#btnRSAceptar').attr('disabled', false);
        });

    $('#nombreRazonSocial').focus();

    $('#nombreRazonSocial, #sortKey').keyup(function (e) {
        $('#nombreRazonSocial').parents(".col-md-8").removeClass("has-error");
        $('#sortKey').parents(".col-md-8").removeClass("has-error");
        f_messageBlock_Off();
    });
});

$('#nombreRazonSocial').keyup(function () {
    $('#btnRSAceptar').removeClass('disabled');
});

$('#nombreRazonSocial').blur(function () {
    if ($('#sortKey').val() == "") {
        $('#sortKey').val($('#nombreRazonSocial').val());
    }
});

var btnRSActionAceptar = function () {
    $('#nombreRazonSocial').parents(".col-md-8").removeClass("has-error");
    $('#sortKey').parents(".col-md-8").removeClass("has-error");

    if ($("#btnRSAceptar").hasClass('disabled')) {
        return;
    }

    if ($('#nombreRazonSocial').val() == '') {
        $('#nombreRazonSocial').parents(".col-md-8").addClass("has-error");
        $('#nombreRazonSocial').focus();
        f_messageBlock('Atención', 'Debe ingresar una Razón Social', 'warning');
        return;
    }

    if ($('#sortKey').val() == '') {
        $('#sortKey').parents(".col-md-8").addClass("has-error");
        $('#sortKey').focus();
        f_messageBlock('Atención', 'Debe ingresar una Sort Key', 'warning');
        return;
    }

    var nom = null;
    v_iObject.PRTGLOB.nom = [];

    $.get($('#urlGetHasRazonesSociales').data('url'), { key: v_iObject.PRTGLOB.Party.idparty }, function (data) {
        let estado = data.Estado;

        if (v_iObject) {
            if (v_iObject.PRTGLOB) {

                if (v_iObject.PRTGLOB.nom) {
                    nom = {};
                    nom.estado = estado;
                    nom.nombre = $('#nombreRazonSocial').val();
                    nom.fantasia = $('#nombreFantasia').val();
                    nom.contacto = $('#contacto').val();
                    nom.sortkey = $('#sortKey').val();
                    nom.rut = $('#rutBanco').val();
                    nom.borrador = "0";
                    v_iObject.PRTGLOB.nom.push(nom);
                    sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iObject));
                    var pruebatest = v_iObject.PRTGLOB.Party.swif;

                    $.post('GetDireccionWebServer', { rut: pruebatest },
                        function (data) {
                            debugger;
                            $.get($('#urlGetPreviousDireccion').data('url'), { key: v_iObject.PRTGLOB.Party.idparty }, function (dd) {

                                if (data.direc.length == 1) {
                                    var dir = null;
                                    dir = {};
                                    dir.estado = dd.Direccion === '' ? 2 : 3;
                                    dir.direccion = dd.Direccion === '' ? data.direc[0].direccion : dd.Direccion;
                                    dir.CodComuna = dd.CodComuna === 0 ? data.direc[0].CodComuna : dd.CodComuna;
                                    dir.comuna = dd.Comuna === '' ? data.direc[0].comuna : dd.Comuna;
                                    dir.region = dd.Region === '' ? data.direc[0].region : dd.Region;
                                    dir.ciudad = dd.Ciudad === '' ? data.direc[0].ciudad : dd.Ciudad;
                                    dir.CodPais = dd.CodPais === 0 ? data.direc[0].CodPais : dd.CodPais;
                                    dir.pais = dd.Pais === '' ? data.direc[0].pais : dd.Pais;
                                    dir.email = dd.Email === '' ? data.direc[0].email : dd.Email;
                                    dir.telefono = dd.Telefono === '' ? data.direc[0].telefono : dd.Telefono;
                                    v_iObject.PRTGLOB.direc.push(dir);

                                    $.post($('#urlDataObject').data('url'),
                                        { iO: JSON.stringify(v_iObject), "_": $.now() },
                                        function (data) {
                                            if (data) {
                                                sessionStorage.setItem(_I_OBJECT_, JSON.stringify(data));
                                                v_iObject = data;
                                                $('#tableRazonSocial').bootstrapTable('refresh');
                                                $("#btnRSAceptar").addClass('disabled');
                                                $('#btnRSCancelar').removeClass('disabled');
                                                window.location.href = $('#urlDireccion').data('url');
                                            }
                                        }
                                    );
                                } else {

                                    $.post($('#urlDataObject').data('url'),
                                        { iO: JSON.stringify(v_iObject), "_": $.now() },
                                        function (data) {
                                            if (data) {
                                                sessionStorage.setItem(_I_OBJECT_, JSON.stringify(data));
                                                v_iObject = data;
                                                $('#tableRazonSocial').bootstrapTable('refresh');
                                                $("#btnRSAceptar").addClass('disabled');
                                                $('#btnRSCancelar').removeClass('disabled');
                                                window.location.href = $('#urlDireccion').data('url');
                                            }
                                        }
                                    );
                                }
                            });
                        }
                    );
                } else {
                    $('#nombreRazonSocial').focus();
                }
            } else {
                $('#nombreRazonSocial').focus();
            }
        } else {
            $('#nombreRazonSocial').focus();
        }
    });
}

var btnRSActionGuardar = function () {
    $('#nombreRazonSocial').parents(".col-md-8").removeClass("has-error");
    $('#sortKey').parents(".col-md-8").removeClass("has-error");

    if ($("#btnRSAceptar").hasClass('disabled')) {
        return;
    }

    if ($('#nombreRazonSocial').val() == '') {
        $('#nombreRazonSocial').parents(".col-md-8").addClass("has-error");
        $('#nombreRazonSocial').focus();
        f_messageBlock('Atención', 'Debe ingresar una Razón Social', 'warning');
        return;
    }

    if ($('#sortKey').val() == '') {
        $('#sortKey').parents(".col-md-8").addClass("has-error");
        $('#sortKey').focus();
        f_messageBlock('Atención', 'Debe ingresar una Sort Key', 'warning');
        return;
    }

    var nom = null;
    v_iObject.PRTGLOB.nom = [];

    $.get($('#urlGetHasRazonesSociales').data('url'), { key: v_iObject.PRTGLOB.Party.idparty }, function (data) {
        let estado = data.Estado;

        if (v_iObject) {
            if (v_iObject.PRTGLOB) {
                if (v_iObject.PRTGLOB.nom) {
                    nom = {};
                    nom.estado = estado;
                    nom.nombre = $('#nombreRazonSocial').val();
                    nom.fantasia = $('#nombreFantasia').val();
                    nom.contacto = $('#contacto').val();
                    nom.sortkey = $('#sortKey').val();
                    nom.rut = $('#rutBanco').val();
                    nom.borrador = "0";
                    v_iObject.PRTGLOB.nom.push(nom);
                    sessionStorage.setItem(_I_OBJECT_, JSON.stringify(v_iObject));
                    var pruebatest = v_iObject.PRTGLOB.Party.swif;

                    $.post('GetDireccionWebServer', { rut: pruebatest },
                        function (data) {
                            debugger;
                            if (data.direc.length == 1 && estado !== 2) {
                                var dir = null;
                                dir = {};
                                dir.estado = estado;
                                dir.direccion = data.direc[0].direccion;
                                dir.CodComuna = data.direc[0].CodComuna;
                                dir.comuna = data.direc[0].comuna;
                                dir.region = data.direc[0].region;
                                dir.ciudad = data.direc[0].ciudad;
                                dir.CodPais = data.direc[0].CodPais;
                                dir.pais = data.direc[0].pais;
                                dir.email = data.direc[0].email;
                                dir.telefono = data.direc[0].telefono;
                                v_iObject.PRTGLOB.direc.push(dir);
                            }

                            $.post($('#urlDataObjectSave').data('url'),
                                { iO: JSON.stringify(v_iObject), idParty: v_iObject.PRTGLOB.Party.idparty, "_": $.now() },
                                function (data) {
                                    if (data) {
                                        sessionStorage.setItem(_I_OBJECT_, JSON.stringify(data.iObject));
                                        v_iObject = data.iObject;
                                        $('#tableRazonSocial').bootstrapTable('refresh');
                                        $("#btnRSGuardar").addClass('disabled');
                                        $('#btnRSCancelar').removeClass('disabled');
                                        f_messageBlock('Grabar Participante', 'Datos del Participante grabados con Exito!', 'success');
                                    }
                                }
                            ).fail(function (data) {
                                debugger;
                                f_messageBlock('Error Grabar Participante', 'Se ha producido un error al guardar razon social', 'danger');
                            });;
                        }
                    );
                } else {
                    $('#nombreRazonSocial').focus();
                }
            } else {
                $('#nombreRazonSocial').focus();
            }
        } else {
            $('#nombreRazonSocial').focus();
        }
    });
};

$('#btnRSCancelar').click(function () {
    $("#btnRSAceptar").addClass('disabled');
    v_iObject = null;
    sessionStorage.removeItem(_I_OBJECT_);
    window.location.href = $('#urlIndex').data('url');
});