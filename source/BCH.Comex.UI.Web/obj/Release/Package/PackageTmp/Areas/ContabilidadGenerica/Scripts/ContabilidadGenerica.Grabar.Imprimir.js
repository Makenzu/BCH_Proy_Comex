
$(document).ready(function () {
    var baseUrl = $("#base_url").val();
    //console.log(documentos);
    var docs = documentos.split(",");
    if (ConfigImpres_PrintFormat == 'HTML'){
        $.each(docs, function (index, value) {
            window.open(baseUrl + value);
        });
        window.location.replace(baseUrl + "ContabilidadGenerica/Home/Index");
    } else {
        var request = []
        var nroOpt = fileName;
        $.each(docs, function (index, value) {
            request[request.length] = getParam(value);
        });

        var location = baseUrl + "Impresion/Imprimir/Multiple";
        
        var EnviarForm = function () {

            var form = $("<form>")
                    .attr("method", "post")
                    .attr("action", location)
                    .attr("target", "_blank");

            $("<input>")
               .attr({
                   name: "request",
                   id: "request",
                   type: "hidden"
               })
               .val(JSON.stringify(request))
                .appendTo($(form));
            $("<input>")
                     .attr({
                         name: "filename",
                         id: "filename",
                         type: "hidden"
                     })
                .val(nroOpt)
                .appendTo($(form));

            form.appendTo(document.body);          
            $(form).submit();
        }

        var VolverPagina = function () {

            $.ajax({
                type: "POST",
                url: location,
                data: {
                    request: JSON.stringify(request),
                    format: ConfigImpres_PrintFormat,
                    filename: nroOpt
                },
                beforeSend: function (xhr) {
                    $.blockUI({ message: '<h6>Descargando ' + nroOpt + '</h6>' });
                },
                success: function (response, status, xhr) {
                    window.location = (baseUrl + "ContabilidadGenerica/Home/Index");
                }
            });
        }

        var ColaEjecucion = $.Callbacks();
        ColaEjecucion.add(EnviarForm, VolverPagina);
        ColaEjecucion.fire();
        ColaEjecucion.fired();
    }
});

function getParam(url) {
    var result = {};
    //1: carta / 2: Swift / 3: ReporteContable
    var tipo = url.indexOf("Impresion/Imprimir") != -1 ? 1 : url.indexOf("DetalleSwiftOriginalCopia") != -1 ? 2 : url.indexOf("ReporteContable") != -1 ? 3 : 0;
    var param = url.split("?")[1];
    param = param.split("&");
    if (tipo == 1) {
        //carta
        result.type = "Carta";
        result.carta = {
            numeroOperacion: param[0].split("=")[1],
            codDocumento: param[1].split("=")[1],
            nroCorrelativo: param[2].split("=")[1]
        };
    } else if (tipo == 2) {
        //swift
        result.type = "SwiftOriginalCopia";
        result.swiftOriginalCopia = {
            nroMensaje: param[0].split("=")[1],
            replace: param[1].split("=")[1],
            with: param[2].split("=")[1]
        };
    } else if (tipo == 3) {
        //contabilidad
        result.type = "Contabilidad";
        result.contabilidad = {
            nroReporte: param[0].split("=")[1],
            fechaOp: param[1].split("=")[1]
        };
    }

    return result;
}
