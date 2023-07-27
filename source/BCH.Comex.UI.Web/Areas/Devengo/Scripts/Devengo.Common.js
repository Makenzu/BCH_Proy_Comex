var FileDownload = null;
var tituloLocal = '';

function DescargarArchivo(location, titulo, urlFile, param) {
    
    if (typeof titulo != 'undefined')
        tituloLocal = titulo;

    $.ajax({
        url: location,
        method: 'POST',
        data: JSON.stringify(param),
        contentType: "application/json",
        dataType: 'json',
        success: function (data, textStatus, jqXHR) {
            if (data.success) {
                $.blockUI({ message: '<h6>Descargando ' + data.fName + '</h6>' })

                $.fileDownload(urlFile, {
                    httpMethod: 'POST',
                    dataType: "json", // data type of response
                    contentType: "application/json",
                    data: { fName: data.fName }
                })
                .done(function () { $.unblockUI(); }) /* nada que alertar, con la ventanita del browser pidiendo para bajar el archivo alcanza*/
                .fail(function () { showAlert("Error en la descarga.", "El archivo de " + tituloLocal + " no se pudo generar", "alert-danger"); $.unblockUI(); });
            }
            else {
                loadMessages(data.ListaMensajesError);
                focusOnErrorControl(data.ListaMensajesError);
                $.unblockUI();
            }
            
        },
        complete: function (jqXHR, textStatus) {
           
        }
    }).fail(function () {
        $.unblockUI();
    });
    return false;
}

$(function () {
    //activo el bloquear la UI en requests asincronos
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...' + tituloLocal + '</h6>' }) })
        .ajaxStop($.unblockUI());
});