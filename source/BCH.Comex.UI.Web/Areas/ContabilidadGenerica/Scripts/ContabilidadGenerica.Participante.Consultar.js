
$(function () {

    $("#caja_Text").focus().select();
    //actualiza la tabla
    $('#tableParticipantes').bootstrapTable({
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
                razonSocial: $('#caja_Text').val()
            };
        },
        //pageSize: 100,
        sidePagination: 'server',
        pagination: false,
        locale: 'es-CL'
    });

    $('#btnBuscar').click(function () {
        if ($("#caja_Text").val().trim() === "") {
            // limpio los registros
            $('#tableParticipantes').bootstrapTable('removeAll');
            // muestro mensaje de error
            showAlert("Consulta Participantes:", "Debe ingresar la razón social", "alert-info", true);
            // pongo foco en campo de error
            $("#caja_Text").focus();
            // me salgo de la funcion
            return;
        }
        $('#tableParticipantes').bootstrapTable('refresh');
    });

    // contorlo el evento para poder ejecutar la busqueda con la tecla Enter
    $('#frmConParticipantes').submit(function (event) {
        // prevenir el submit
        event.preventDefault();

        $('#tableParticipantes').bootstrapTable('refresh');
    });

    //agrego el evento doble click a las filas de la grilla
    $('#tableParticipantes').on('dblclick', 'tbody tr', function (arg) {
        var val = arg.currentTarget.children[1].innerHTML;

        window.location.href = participantesUrl + "?razonSocial=" + val;
    });

    $('#btnVolver').click(function () {
        window.location.href = participantesUrl;
    });


});
