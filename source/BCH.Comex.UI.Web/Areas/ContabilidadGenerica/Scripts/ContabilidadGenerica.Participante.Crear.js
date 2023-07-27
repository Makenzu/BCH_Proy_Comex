$(document).ready(function () {
    $('#Telefono').inputmask('(999) 999-9999');
    $('#Fax').inputmask('(999) 999-9999');

    var rutMask = '999.999.999-*';
    var swiftMask = '************';

    var baseUrl = $("#base_url").val()+"ContabilidadGenerica/"; //obtengo la url base global
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI).ajaxError($.unblockUI);

    var viewModel = null;
    //cuando se genera el evento click este sube por el DOM hasta el 'document' y cambia la URL del form
    $(document).on("click", "[type='submit'][data-form-action]", function (event) {
        var that = $(this);
        var formAction = that.attr("data-form-action");
        that.closest("form").attr("action", formAction);
    });
    $.ajax({
        url: baseUrl + "Participantes/Crear_Get",
        success: function (data) {
            viewModel = new crearParticipanteViewModel(data);
            ko.applyBindings(viewModel);
            if (data.Rut.Tag == '0') {
                $('#RUT').inputmask(rutMask);
            } else if (data.Rut.Tag == '1') {
                $('#RUT').inputmask(swiftMask);
            }
        }
    });
    var ignoreList = [];
    var viewModelFunctions = function(){
        var that = {};
        that.es_banco_change = function () {
            viewModel.EsBanco.Checked(!viewModel.EsBanco.Checked());
            AjaxCall("Participantes/es_banco_change");
        };
        that.telefono_lost_focus = function () {
            if (viewModel.Telefono.Text() == "") {
                return;
            }
            if (viewModel.Pais.ListIndex() >= 0) {
                if (viewModel.Pais.Items()[viewModel.Pais.ListIndex()].Data() == viewModel.nuestro_pais()) {
                    viewModel.Telefono.Text = "";
                }
            }
        };
        that.aceptar = function () {
            AjaxCall("Participantes/Crear_Aceptar", function () {
                window.location = baseUrl + "Participantes/Crear_Aceptar_Post";
            });
        };
        that.cancelar = function () {
            window.location = baseUrl + "Participantes/Crear_Cancelar";
        };
        that.cas_bco_lost_focus = function () {
            if (viewModel.cas_bco.Text() != "") {
                viewModel._envio_2.Enabled(true);
            }
            else {
                viewModel._envio_2.Checked(false);
                viewModel._envio_2.Enabled(false);
            }
        };
        that.cas_pos_lost_focus = function () {
            if (viewModel.cas_postal.Text() != "") {
                viewModel._envio_3.Enabled(true);
            }
            else {
                viewModel._envio_3.Checked(false);
                viewModel._envio_3.Enabled(false);
            }
        };
        that.fax_lost_focus = function () {
            if (viewModel.Fax.Text() != "")
            {
                viewModel._envio_1.Enabled(true);
            }
            else
            {
                viewModel._envio_1.Checked(false);
                viewModel._envio_1.Enabled(false);
            }
        };
        that.pais_change = function () {
            viewModel.Pais.ListIndex($("#Pais")[0].selectedIndex-1);
        };
        return that;
    };
    var crearParticipanteViewModel = function (data) {
        ko.mapping.fromJS(data, {}, this);
        var that = this;
        var functions = viewModelFunctions();
        $.each(functions, function (name) {
            ignoreList.push(name);
        });
        that = $.extend(that,that, functions);
    };
    var updateModel = function (newViewModel) {
        newViewModel.MaskedRut = newViewModel.Rut.Text;
        ko.mapping.fromJS(newViewModel, viewModel);

        //obtengo los errores y los muestro
        loadMessages(newViewModel.ListaErrores);
        if (newViewModel.Rut.Tag == '0') {
            $('#RUT').inputmask(rutMask);
        } else if (newViewModel.Rut.Tag == '1') {
            $('#RUT').inputmask(swiftMask);
        }
    }
    function AjaxCall(url, func) {
        viewModel.ListaErrores([]);
        viewModel.Rut.Text($("#RUT").inputmask("unmaskedvalue").toUpperCase());
        viewModel.Telefono.Text($("#Telefono").inputmask("unmaskedvalue").toUpperCase());
        viewModel.Fax.Text($("#Fax").inputmask("unmaskedvalue").toUpperCase());
        $.ajax({
            method: "POST",
            url: baseUrl + url,
            data: {
                jsonModel: ko.mapping.toJS(viewModel, {
                    ignore: ignoreList
                })
            },
            success: function (a) {
                updateModel(a);
                if (a.ListaErrores.length == 0) {
                    if (func) {
                        func();
                    }
                }
            },
            error: function (err) {
                //console.error(err.responseText);
            }
        });
    }
});