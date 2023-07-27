$(document).ready(function () {
    var estadoCC = $('#EstadoConfigurarCasilla').val();
    alert(estadoCC);
    // var estadoCC = "False";
    if (estadoCC == "False") {
        $('#MenuConfigurarCasilla').addClass('disabled');
    }
    else {
        $('#MenuConfigurarCasilla').removeClass('disabled');
    }

    $('#btnConfigurarCasilla').click(function () {

        if (estadoCC == "False")
            return false;
    });

});