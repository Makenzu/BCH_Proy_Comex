$(function () {
    var baseUrl = $("#base_url").val(); //obtengo la url base global

    //activo el bloquear la UI en requests asincronos
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI);

    $('#L_Print_SelectedValue').focus();
    
    $('#L_Print_SelectedValue').dblclick(function () {
        $('#bot_acet').click();
    });

    /// Cuando haga Enter en cualquier parte de la pantalla
    $(window).keydown(function (ev) {
        var keycode = (ev.keyCode ? ev.keyCode : ev.which);
        if (keycode == 13) {  // Presiona Enter
            ev.preventDefault();
            $('#bot_acet').click();
        }
    });
});