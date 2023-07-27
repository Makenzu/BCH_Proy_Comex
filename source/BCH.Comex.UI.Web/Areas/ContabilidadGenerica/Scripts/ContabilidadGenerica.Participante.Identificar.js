
$(function () {
    var baseUrl = $("#base_url").val()+"ContabilidadGenerica/"; //obtengo la url base global

    $("#Llave_Text").focus().select();
    //activo el bloquear la UI en requests asincronos
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI).ajaxError($.unblockUI);

    //agrego el parametro de la key al get
    $('#Bot_Nem').click(function () {
        var route = $(this).attr('href');
        $(this).attr('href', route + '?razonSocial=' + $('#Llave_Text').val());
    });

    $('#btnIdentificar').click(function (item) {
        if ($('#Llave_Text').val() == "") {
            $('#Llave_Text').focus().select();
            return false;
        }
        $.ajax({
            url: baseUrl + "Participantes/Identificar_Click",
            method: "POST",
            data: { codParticipante: $('#Llave_Text').val() },
            success: function (data) {
                ParticipantesViewModelToView(data);
                $("#btnAceptar").focus().select();
                if (data.Redireccionar) {
                    window.location = baseUrl + data.Redireccionar;
                }

                if (data.Aceptar.Enabled)
                {
                    //muestro el modal de Participantes identificar si es necesario
                    AbrirSeleccionNombreDir(data.AbrirIdentParticipantes);
                }
                
            },
            error: function (response, type, message) {
                try {
                    var responseJson = JSON.parse(response.responseText);
                    $('#Llave_Text').focus().select();
                    showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
                }
                catch (err) {
                    $('#Llave_Text').focus().select();
                    showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", true);
                }
            }
        });
    });

    $('#btnEliminar').click(function (item) {
        $.ajax({
            url: baseUrl + "Participantes/Eliminar_Click",
            method: "POST",
            success: function (data) {
                ParticipantesViewModelToView(data);
            },
            error: function (response, type, message) {
                try {
                    var responseJson = JSON.parse(response.responseText);
                    $('#Llave_Text').focus().select();
                    showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
                }
                catch (err) {
                    $('#Llave_Text').focus().select();
                    showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", true);
                }
            }
        });
    });

    $('#LstPartys_SelectedValue').change(function (item) {
        $.ajax({
            url: baseUrl + "Participantes/LstPartys_Click",
            method: "POST",
            data: { selectedValue: $('#LstPartys_SelectedValue option:selected').val() },
            success: function (data) {
                ParticipantesViewModelToView(data);
            },
            error: function (response, type, message) {
                try {
                    var responseJson = JSON.parse(response.responseText);
                    showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
                }
                catch (err) {
                    showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", true);
                }
            }
        });
    });

    $('#dondeContainer input').change(function () {
        var value = $('#dondeContainer input:checked').val();

        $.ajax({
            url: baseUrl + "Participantes/Donde_Click",
            method: "POST",
            data: { selectedValue: value == "Donde_0" ? true : false },
            success: function (data) {
                if (data.Redireccionar){
                    window.Location = data.Redireccionar;
                }
                else {
                    ParticipantesViewModelToView(data);
                }
            },
            error: function (response, type, message) {
                try {
                    var responseJson = JSON.parse(response.responseText);
                    showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
                }
                catch (err) {
                    showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", true);
                }
            }
        });
    });

    $('#ParticipantesIdentificar').on('show.bs.modal', function (event) {
        //var button = $(event.relatedTarget) // Button that triggered the modal
        //var recipient = button.data('whatever') // Extract info from data-* attributes
        // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).

        $.ajax({
            url: baseUrl + "Participantes/Get_ParticipantesIdentificar",
            method: "GET",
            cache: false,
            success: function (data) {
                ParticipantesIdentificarViewModelToView(data);
            },
            error: function (response, type, message) {
                try {
                    var responseJson = JSON.parse(response.responseText);
                    showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
                }
                catch (err) {
                    showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", true);
                }
            }
        });

        // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
        //var modal = $(this)
        //modal.find('.modal-title').text('New message to ' + recipient)
        //modal.find('.modal-body input').val(recipient)
    })

    var ConfirmModal = false;

    $('#btnAceptarPIdentificar').click(function () {
        $("#ParticipantesIdentificar").modal('hide');

        $.ajax({
            url: baseUrl + "Participantes/ParticipantesIdentificar_Aceptar",
            method: "Post",
            data: { selectedRS: $('#ddlRazonSocial').val(), selectedDir: $('#ddlDireccion').val() },
            success: function (data) {
                ParticipantesViewModelToView(data);
                ConfirmModal = true;
            },
            complete: function () {

            }
        });
    });

    $("#ParticipantesIdentificar").on("shown.bs.modal", function () { $("#btnAceptarPIdentificar").focus(); })
    $("#ParticipantesIdentificar").on("hidden.bs.modal", function () {
        if (ConfirmModal)
            $("#btnAceptar").focus();
        else
            $('#Llave_Text').focus().select();
    })

    // Cuando haga Enter en cualquier parte de la pantalla
    $(document).keydown(function (ev) {
        try {
            var keycode = (ev.keyCode ? ev.keyCode : ev.which);
            if (keycode == 9) {  // Presiona Tab                
                RecorrerTabIndex(-1, ev);                
            }

            if (keycode == 13) {  // Presiona Enter                
                ev.preventDefault();
                if ($('#btnAceptar').is(":focus"))
                    $('#btnAceptar').click();
                else if ($("#btnAceptarPIdentificar").is(":focus"))
                    $("#btnAceptarPIdentificar").click()
                else
                    $('#btnIdentificar').click();
            }
        } catch (e) { console.log(e); }
    });
});

function ParticipantesViewModelToView(data) {

    //cargo los mensajes de error
    loadMessages(data.ListaErrores);
    focusOnErrorControl(data.ListaErrores);

    var partiList = $('#LstPartys_SelectedValue');
    var selected = data.LstPartys.ListIndex;
    partiList
        .empty()
        .append($.map(data.LstPartys.Items, function (item, index) {
            var options = {
                value: item.Data,
                text: item.Value.replace("\t", " ")
            };
            if (index == selected) {
                options.selected = ""
            }
            return $('<option/>', options);
        }));

    var ubicacionList = $('input:radio[name=Donde]');
    $.each(data.Donde, function (index, item) {
        ubicacionList[index].checked = item.Checked;
    });

    var dir = $('#Tx_Dir');
    dir.val(data.Tx_Dir.Text);

    var key = $('#Llave_Text');
    if (data.Llave.Enabled) {
        key.removeAttr('disabled');
    } else {
        key.attr('disabled', 'disabled');
    }


    var buscar = $('#Bot_Nem');
    if (data.Bot_Nem.Enabled) {
        buscar.removeAttr('disabled');
    } else {
        buscar.attr('disabled', 'disabled');
    }

    var btnIdentificar = $('#btnIdentificar');
    btnIdentificar.val(data.Identificar.Text);
    if (data.Identificar.Enabled) {
        btnIdentificar.removeAttr('disabled');
    } else {
        btnIdentificar.attr('disabled', 'disabled');
    }

    var btnEliminar = $('#btnEliminar');
    if (data.Eliminar.Enabled) {
        btnEliminar.removeAttr('disabled');
    } else {
        btnEliminar.attr('disabled', 'disabled');
    }

    var btnAceptar = $('#btnAceptar');
    if (data.Aceptar.Enabled) {
        btnAceptar.removeAttr('disabled');
    } else {
        btnAceptar.attr('disabled', 'disabled');
    }

    var btnBuscar = $('#btnBuscar');
    if (data.Bot_Nem.Enabled) {
        btnBuscar.removeAttr('disabled');
        btnBuscar.attr('href', btnBuscar.data('href'));
    } else {
        btnBuscar.attr('disabled', 'disabled');
        btnBuscar.attr('href', '#')
    }

    var caption = $("#tituloParticipantes").text("Participantes " + data.OPE);

    $('#Llave_Text').focus().select();
}

function ParticipantesIdentificarViewModelToView(data) {

    var rsList = $('#ddlRazonSocial');
    rsList.empty();

    $.each(data.Nome.Items, function (index, item) {
        rsList.append(
            $('<option/>', {
                value: item.Data,
                text: item.Value
            })
        );
    });

    var dirList = $('#ddlDireccion');
    dirList.empty();

    $.each(data.Dire.Items, function (index, item) {
        dirList.append(
            $('<option/>', {
                value: item.Data,
                text: item.Value
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

