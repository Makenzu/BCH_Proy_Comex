var tipoOperacionTraidoDelServer = null;

$(function () {
    var baseUrl = $("#base_url").val(); //obtengo la url base global

    tipoOperacionTraidoDelServer = $('input[name=SelectedTipoOperacion]:checked').val();
    
    if (modelCompleto != null) {
        if (modelCompleto.KeyText != null && modelCompleto.KeyText != "") {
            $("#btnIdentificar").focus();
        } else {
            $("#SelectedTipoOperacion").focus();
        }
    } else {
        $("#SelectedTipoOperacion").focus();
    }

    //activo el bloquear la UI en requests asincronos
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI).ajaxError($.unblockUI);

    //agrego el parametro de la key al get
    $('#btnSearch').click(function () {
        var route = $(this).attr('href');
        $(this).attr('href', route + '?razonSocial=' + $('#KeyText').val().trim());
    });

    $("#frmParticipantes").submit(function (e) {
        $.blockUI({ message: '<h6>Cargando...</h6>' })
        if (tipoOperacionTraidoDelServer != null) {
            var tipoOperacionSeleccionado = $('input[name=SelectedTipoOperacion]:checked').val();
            if (tipoOperacionTraidoDelServer != tipoOperacionSeleccionado) {
                //se muestra este warning para que el usuario no piense que esta cambiando el tipo de operacion y en verdad si no hace click en Modificar no lo esta haciendo
                if (!window.confirm("Atención! Ud. cambió el Tipo de Operación pero si continua este cambio será ignorado.\nDebe hacer click en Modificar el participante para que este cambio sea considerado. ¿Desea continuar?")) {
                    e.preventDefault(e);
                    $.unblockUI();
                }
            }
        }
    });

    $('#btnIdentificar').click(function (item) {

        var codParticipante = $('#KeyText').val().trim();

        var tipoOperacionSeleccionado = $('input[name=SelectedTipoOperacion]:checked').val();

        if (confirmarTipoDeOperacion(codParticipante)) {
            $.ajax({
                url: baseUrl + "FundTransfer/Participantes_Identificar_Click",
                method: "POST",
                data: { codParticipante: codParticipante, selectedTipoOperacion: tipoOperacionSeleccionado },
                success: function (data) {
                    tipoOperacionTraidoDelServer = tipoOperacionSeleccionado;
                    ParticipantesViewModelToView(data);
                    $("#btnAceptar").focus().select();
                    if (data.Redireccionar) {
                        window.location = data.Redireccionar;
                    }
                    // Si el botón aceptar esta habilitado, permitimos la validación para abrir el modal con participantes
                    if (data.BtnAceptar.Enabled) {
                        //muestro el modal de Participantes identificar si es necesario
                        AbrirSeleccionNombreDir(data.AbrirIdentParticipantes);
                    }
                },
                error: function (data) {
                    alert('Ha ocurrido un error');
                }
            });
        }
    });

    $('#btnEliminar').click(function (item) {
        $.ajax({
            url: baseUrl + "FundTransfer/Participantes_Eliminar_Click",
            method: "POST",
            success: function (data) {
                ParticipantesViewModelToView(data);
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });

    $('#SelectedPartiList').change(function (item) {
        $.ajax({
            url: baseUrl + "FundTransfer/Participantes_LstPartys_Click",
            method: "POST",
            data: { selectedValue: $('#SelectedPartiList').val() },
            success: function (data) {
                ParticipantesViewModelToView(data);
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });

    $('#dondeContainer input').change(function () {
        var value = $('#dondeContainer input:checked').val();
        var prod = $('input[name=SelectedTipoOperacion]:checked').val();
        $.ajax({
            url: baseUrl + "FundTransfer/Participantes_Donde_Click",
            method: "POST",
            data: { selectedValue: value, prod: prod },
            success: function (data) {
                if (data.Redireccionar)
                    window.Location = data.Redireccionar;
                else
                    ParticipantesViewModelToView(data);
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });

    $('#ParticipantesIdentificar').on('show.bs.modal', function (event) {
        //var button = $(event.relatedTarget) // Button that triggered the modal
        //var recipient = button.data('whatever') // Extract info from data-* attributes
        // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).

        $.ajax({
            url: baseUrl + "FundTransfer/ParticipantesIdentificar",
            method: "GET",
            cache: false,
            success: function (data) {
                ParticipantesIdentificarViewModelToView(data);
            }
        });


        // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
        //var modal = $(this)
        //modal.find('.modal-title').text('New message to ' + recipient)
        //modal.find('.modal-body input').val(recipient)
    })

    $('#btnAceptarPIdentificar').click(function () {
        $("#ParticipantesIdentificar").modal('hide');

        $.ajax({
            url: baseUrl + "FundTransfer/ParticipantesIdentificar_Aceptar",
            method: "Post",
            data: { selectedRS: $('#ddlRazonSocial').val(), selectedDir: $('#ddlDireccion').val() },
            success: function (data) {
                ParticipantesViewModelToView(data);
            },
            complete: function () {

            }
        });
    });

    if (DesdeCargaOperaciones == 'True') {
        $('#btnIdentificar').click();
    }

    if (CargaAutomatica == "1") {
        $("#btnAceptar").focus().select();
    }
    $("#ParticipantesIdentificar").on("shown.bs.modal", function () { $("#btnAceptarPIdentificar").focus(); })
    $("#ParticipantesIdentificar").on("hidden.bs.modal", function () { $("#btnAceptar").focus(); })

    ValidarSiEsReverso();

    $('input[name=SelectedTipoOperacion]').DetectarTecla(function () {
        $('[tabindex="3"]').focus(); // Posiciona Foco en TAB indicado
    });

    $('#KeyText').DetectarTecla(function (ev) {
        $('#btnIdentificar').click();
        ev.preventDefault(ev);
    });

});

function confirmarTipoDeOperacion(codParticipante) {
    var baseNumberLength = 6;
    var prodSeleccionado = $('input[name=SelectedTipoOperacion]:checked').val();
    var mostrarWarning = false;

    if ($.isNumeric(codParticipante) && codParticipante.toString().length == baseNumberLength) {
        //ingresó un base number, si no puso operacion Cosmos (30) muestro un warning
        mostrarWarning = (prodSeleccionado != 30);
    }
    else {
        mostrarWarning = (prodSeleccionado != 20);
    }

    if (mostrarWarning) {
        return window.confirm("El tipo de operación y el participante seleccionado no concuerdan. ¿Desea continuar?");
    }
    else return true;
}

/// Desabilita campos si viene de Reverso.
function ValidarSiEsReverso() {
    if (OperacionReversa == "True") {
        $("#KeyText").attr("disabled", true);
        $("#btnBuscar").attr("disabled", true);
        $('input:radio[name=SelectedDonde]').attr("disabled", true);
        $("#btnEliminar").css("display", "none");
        $("#btnInstruccion").css("display", "none");
    }
    else {
        $("#KeyText").removeAttr("disabled");
        $("#btnBuscar").removeAttr("disabled");
        $('input:radio[name=SelectedDonde]').removeAttr("disabled"); 1, 2
    }
}

function ParticipantesViewModelToView(data) {

    //cargo los mensajes de error
    loadMessages(data.MensajesDeError);
    focusOnErrorControl(data.MensajesDeError);

    var partiList = $('#SelectedPartiList');
    partiList.empty();
    $.each(data.PartiList, function (index, item) {
        partiList.append(
            $('<option/>', {
                value: item.Value,
                text: item.Text.replace("\t", " "),
                selected: item.Selected
            })
        );
    });

    var ubicacionList = $('input:radio[name=SelectedDonde]');
    $.each(data.Donde, function (index, item) {
        ubicacionList[index].checked = item.Selected;
    });

    var tipoOperacionList = $('input:radio[name=SelectedTipoOperacion]');
    $.each(data.TipoOperacion, function (index, item) {
        tipoOperacionList[index].checked = item.Selected;
    });

    var dir = $('#Tx_Dir');
    dir.val(data.Tx_Dir);

    var key = $('#KeyText');
    if (data.Llave.Enabled) {
        key.removeAttr('disabled');
    } else {
        key.attr('disabled', 'disabled');
    }

    var btnIdentificar = $('#btnIdentificar');
    btnIdentificar.val(data.BtnIdentificar.Text);
    if (data.BtnIdentificar.Enabled && !data.ParticipanteCongelado) {
        btnIdentificar.removeAttr('disabled');
    } else {
        btnIdentificar.attr('disabled', 'disabled');
    }

    var btnEliminar = $('#btnEliminar');
    if (data.BtnEliminar.Enabled && !data.ParticipanteCongelado) {
        btnEliminar.removeAttr('disabled');
    } else {
        btnEliminar.attr('disabled', 'disabled');
    }

    var btnAceptar = $('#btnAceptar');
    if (data.BtnAceptar.Enabled) {
        btnAceptar.removeAttr('disabled');
    } else {
        btnAceptar.attr('disabled', 'disabled');
    }

    var btnBuscar = $('#btnBuscar');
    if (data.BtnBuscar.Enabled) {
        btnBuscar.removeAttr('disabled');
        btnBuscar.attr('href', btnBuscar.data('href'));
    } else {
        btnBuscar.attr('disabled', 'disabled');
        btnBuscar.attr('href', '#')
    }

    var caption = $("#tituloParticipantes").text("Participantes " + data.OPE);

    ValidarSiEsReverso();
}

function ParticipantesIdentificarViewModelToView(data) {

    var rsList = $('#ddlRazonSocial');
    rsList.empty();

    $.each(data.RazonSocialList, function (index, item) {
        rsList.append(
            $('<option/>', {
                value: item.Value,
                text: item.Text
            })
        );
    });

    var dirList = $('#ddlDireccion');
    dirList.empty();

    $.each(data.DirList, function (index, item) {
        dirList.append(
            $('<option/>', {
                value: item.Value,
                text: item.Text
            })
        );
    });
}

///Si es necesario, abre la ventana de seleccion de Nombres y Direcciones
function AbrirSeleccionNombreDir(mostrar) {
    //var popup = $('#AbrirForm').val();

    if (mostrar) {
        $("#ParticipantesIdentificar").modal({
            backdrop: 'static'
        });
    }
}

$(window).keydown(function (ev) {
    var keycode = (ev.keyCode ? ev.keyCode : ev.which);
    if (keycode == 9) {     // Presiona Tab
        RecorrerTabIndex(-1, ev);
    }
    if (keycode == 13) {    // Presiona Enter
    }
});