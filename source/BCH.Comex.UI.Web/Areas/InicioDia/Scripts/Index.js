function inicializarTabla() {
    $('#tablaEspecialistas').bootstrapTable({
        classes: 'table table-hover',
        data: datosEspecialistas,
        columns: [
            { field: 'CentroCostoYCodUsr', title: 'Especialista', sortable: true },
            { field: 'rut', title: 'Rut', sortable: true },
            { field: 'nombre', title: 'Nombre', sortable: true },
            { field: 'CentroCostoYCodUsrSupervisor', title: 'Líder', sortable: true }
        ],
        pagination: false,
        sidePagination: 'client',
        search: true,
        sortable: true,
        locale: "es-SP",
        showRefresh: true,
        clickToSelect: false,
        rowStyle: function (row, index) {
            if (row.CentroCostoYCodUsr == ccYCodUsrActual) {
                return { classes: 'info' };
            }
            else {
                return {};
            }
        },
    });

    $('#tablaEspecialistas').on("dbl-click-row.bs.table", impersonarUsuario);
    $('#badgeEspecialistas').text(datosEspecialistas.length);
}

function limpiarTodosLosErrores() {
    $(".lblMensajeError").remove();  //elimino cualquier label de error
    $("div.form-group.has-error").removeClass("has-error");
    $("#msg-zone").html("");
}

function grabar() {
    var message = "Confirma que desea iniciar el día?";
    if (confirm(message)) {
        $("#formGuardar").submit();
    }
}

function impersonarUsuario(row, $element) {
    var usr = $element;
    var message = "Confirma que desea reemplazar al usuario " + usr.nombre + " ( " + usr.CentroCostoYCodUsr + ")?";
    if (confirm(message)) {
        $.blockUI({ message: '<h4>Reemplazando usuario, por favor espere...</h4>' });
        return $.ajax({
            type: "POST",
            url: urlReemplazarUsuario,
            data: { centroCostoYCodUsr: usr.CentroCostoYCodUsr },
            success: function (resultado) {
                if (resultado.estado) {
                    ccYCodUsrActual = resultado.info;
                    $('#tablaEspecialistas').bootstrapTable("load", datosEspecialistas);
                    $(".userBoxInfo").html("(" + usr.CentroCostoYCodUsr + ")");
                    showAlert("Reemplazo exitoso!. ", "Debe tener en cuenta que ahora todas las operaciones que realice estarán asociadas al usuario " + usr.nombre + " (" + usr.CentroCostoYCodUsr + ")", "alert-success", true);
                }
                else
                {
                    showAlert("Error de Reemplazo!. ", resultado.mensaje, "alert-danger", true);
                }
            },
            error: function (response, type, message) {
                try {
                    //intento parsear la respesta como json.
                    var responseJson = JSON.parse(response.responseText);
                    showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", false);
                }
                catch (err) {
                    showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", false);
                }
            }
        });
    }
}

$(function () {

    $(window).resize(function () {
        $('#tablaEspecialistas').bootstrapTable('resetView');
    });

    $("#btnGuardar").click(grabar);
       
    //cuando el ajax vuelve, desbloqueo el block UI si es que estaba bloqueado
    $(document).ajaxStop($.unblockUI);

    inicializarTabla();
    loadMessages(mensajes);
});


