$(function () {
    numeral.language("es");
    inicializarPopover();
    obtenerAlertas();

    $('body').on('click', '.linkAlerta', function () {

        $("#" + $(this).attr("id")).remove();
        var cantAlertas = parseInt($("#badgeCantAlertas").text()) - 1;
        $("#badgeCantAlertas").text(cantAlertas);
        if (cantAlertas == 0) {
            htmlAlertas = htmlCeroAlertas;
        }
        else {
            htmlAlertas = $("#divAlertas").parent().html();
        }
    });
});

function inicializarPopover() {
    var triggerVar = "focus";
    var detectIEregexp = /MSIE (\d+\.\d+);/ 
    if (detectIEregexp.test(navigator.userAgent)) { 
        var ieversion = new Number(RegExp.$1) 
        if (ieversion <= 8) {
            triggerVar = "click";
        }
    }
    $("#popoverAlertas").popover(
    {
        content: function () {
            return htmlAlertas;
        },
        html: true,
        container: 'body',
        trigger: triggerVar,
        placement: 'bottom'
    });
}

var htmlAlertas;
var htmlCeroAlertas = "<pan>No hay alertas que mostrar</span>";
function obtenerAlertas()
{
    $.ajax({
        timeout: 35000,
        url: urlGetAlertas,
        success: function (data) {
            if (data != null) {
                if(data.TodasDeshabilitadas){
                    $("#contenedorAlertas").hide();
                }
                else {
                    $("#badgeCantAlertas").text(data.Alertas.length);
                    $("#contenedorAlertas").show();
                    if (data.Alertas.length > 0) {
                        htmlAlertas = "<div class='list-group' id='divAlertas'>";
                        $.each(data.Alertas, function (i, item) {
                            htmlAlertas += "<a class='list-group-item linkAlerta' id='alerta" + i + "' target='_blank' href='" + urlNavegarAlerta + "?aplicacion=" + item.Aplicacion + "'><span class='badge'>" + item.CantidadMensajes + "</span>" + item.Texto + "</a>";
                        });
                        htmlAlertas += "</div>";
                    }
                    else {
                        htmlAlertas = htmlCeroAlertas;
                    }
                                                
                    setTimeout(obtenerAlertas, 60000); 
                }
            }
        },
        global: false,
        dataType: 'json'
    });
}