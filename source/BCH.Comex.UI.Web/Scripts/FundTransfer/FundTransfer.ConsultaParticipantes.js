var tableParticipantes;
$(function () {
    
    $("#RazonSocial").focus().select();
    
    //actualiza la tabla
    tableParticipantes = $('#tableParticipantes').bootstrapTable({
        classes: 'table table-hover resultRow',
        columns: [
            { field: 'razon_social', title: 'Nombre o Razón Social', sortable: false },
            { field: 'identificador', title: 'Identificador', sortable: false }
        ],
        onLoadSuccess: function (data) {
            //var d = data;
            $('#tableParticipantes tbody tr').css("cursor", "pointer");
        },
        method: 'post',
        url: queryUrl,
        queryParams: function (p) {
            return {
                razonSocial: $('#RazonSocial').val()
            };
        },
        //pageSize: 100,
        sidePagination: 'server',
        pagination: false,
        locale: 'es-CL'
    });

    $('#btnBuscar').click(buscar);

    // contorlo el evento para poder ejecutar la busqueda con la tecla Enter
    $('#frmConParticipantes_FundTransfer').on('submit', function (event) {
        // prevenir el submit
        event.preventDefault();
        // prevenir submit en IE
        event.returnValue = false;
    });

    //agrego el evento doble click a las filas de la grilla
    $('#tableParticipantes').on('dblclick', 'tbody tr', function (arg) {
        var val = arg.currentTarget.children[1].innerHTML;

        window.location.href = participantesUrl + "?razonSocial=" + val;
    });

    $("#RazonSocial").change(eliminarCaracteresEspeciales);
});

$(window).keydown(function (ev) {
    var keycode = (ev.keyCode ? ev.keyCode : ev.which);
    if (keycode === 13) {    // Presiona Enter
        buscar();
    }
});

function buscar() {
    if ($("#RazonSocial").val().trim() === "") {
        // limpio los registros
        tableParticipantes.bootstrapTable('removeAll');
        // muestro mensaje de error
        showAlert("Consulta Participantes:", "Debe ingresar la razón social", "alert-info", true);
        // pongo foco en campo de error
        $("#RazonSocial").focus().select();
        // me salgo de la funcion
        return;
    }
    else if ($("#RazonSocial").val().length < 4) {
        // limpio los registros
        tableParticipantes.bootstrapTable('removeAll');
        // muestro mensaje de error
        showAlert("Consulta Participantes:", "Debe ingresar al menos 4 caracteres", "alert-info", true);
        // pongo foco en campo de error
        $("#RazonSocial").focus().select();
        // me salgo de la funcion
        return;
    }
    // vuelvo a buscar
    tableParticipantes.bootstrapTable('refresh');
}

function eliminarCaracteresEspeciales() {
    if (!($("#RazonSocial").val().trim() === "")) {
        var pattern = /[^0-9A-Za-z .ñç`´'áéíóúÁÉÍÓÚäëïöüÄËÏÖÜàèìòùÀÈÌÒÙâêîôûÂÊÎÔÛ]/g;
        var replaced = $("#RazonSocial").val().trim().replace(pattern, "");
        $("#RazonSocial").val(replaced);
    }
}
