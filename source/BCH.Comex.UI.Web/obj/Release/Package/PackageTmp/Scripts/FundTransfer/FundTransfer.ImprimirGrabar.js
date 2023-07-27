
$(document).ready(function () {
    var baseUrl = $("#base_url").val();
    $.ajax({
        url: baseUrl + "FundTransfer/DocumentosAImprimir",
        method: "GET",
        cache:false,
        success: function (data) {
            //console.log(data);
            if (ConfigImpres_PrintFormat == 'HTML') {
                for (var i = 0; i < data.length; i++) {
                    window.open(baseUrl + data[i].URL);
                }
                window.location = (baseUrl + "FundTransfer/IMPGRAB_Finish");
            } else {
                var request = []
                var nroOpt = '';
                for (i = 0; i < data.length; ++i) {
                    var item = data[i];
                    request[request.length] = ObtenerRequestRowDeTabla(item);
                    if ( nroOpt == '') {
                        nroOpt = item.fileName;
                    }

                }

                //var w = window.open();
                //w.location = baseUrl + "Impresion/Imprimir/Multiple" + "?request=" + JSON.stringify(request);
                var location = baseUrl + "Impresion/Imprimir/Multiple"; //+ "?request=" + JSON.stringify(request);
                
                //$.fileDownload(location, {
                //    httpMethod: "POST",
                //    data: { request: JSON.stringify(request) },
                //    successCallback: function (url) {alert(url);}
                //})
                //    .done(function () { $.unblockUI();  /* nada que alertar, con la ventanita del browser pidiendo para bajar el archivo alcanza*/ })
                //    .fail(function () { showAlert("Error en la descarga.", "Error al generar los documentos", "alert-danger"); $.unblockUI(); })
                //.complete(function () { window.location = (baseUrl + "FundTransfer/IMPGRAB_Finish"); });
                //$.ajax({
                //    method: "POST",
                //    url: location,
                //    data: { request: JSON.stringify(request) },
                //    success: function (dat) {
                //        console.log("llego:" + dat);
                //    }
                //});
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

               // $(Field).appendTo($(form));

                //var hiddenField = $("input")
                //   .attr("name", "request")
                //   .attr("id", "request")
                //   .attr("type", "hidden")
                //   .val(JSON.stringify(request));
                //$(Field).append($(hiddenField));

                //$(form).append($(Field));
                

                form.appendTo(document.body);    // Not entirely sure if this is necessary           
                $(form).submit();

                setTimeout(function () { window.location = (baseUrl + "FundTransfer/IMPGRAB_Finish"); }, 0);
               
            }
        },
        error: function (error) {
            //console.error(error);
        }
    })
});

function GetParamsParaMensajeSwift(row) {
    return {
        nroMensaje: row.nroMensaje,
        nroReporte: row.nroReporte,
        fechaOp: moment.utc(row.fechaOp).format("YYYY/MM/DD")
    };
}

function GetParamsParaMensajeSwiftOriginaCopia(row) {
    return {
        nroMensaje: row.nroMensaje,
        replace: row.replace,
        with: row.with
    };
}

function GetParamsParaContabilidad(row) {
    _nroReporte = row.nroReporte;
    return { nroReporte: row.nroReporte, fechaOp: moment.utc(row.fechaOp).format("YYYY/MM/DD") };
}

function GetParamsParaCarta(row) {
    var nroOperacion = row.NumeroOperacion.replace(/-/g, ''); //remuevo todos los guiones
    return { numeroOperacion: nroOperacion, codDocumento: row.CodigoDocumento, nroCorrelativo: row.NumeroCorrelativo };
}

function GetParamsParaPlanillas(row) {
    return { nroPresentacion: row.nroPresentacion, fechaPresentacion: moment.utc(row.fechaOp).format("YYYY/MM/DD") };
}

function GetParamsParaPlanillasReemplazo(row) {
    return {
        nroPresentacion: row.nroPresentacion, fechaPresentacion: moment.utc(row.fechaOp).format("YYYY/MM/DD"),
        centroCosto: row.NumeroOperacion.substring(0, 3),
        producto: row.NumeroOperacion.substring(3, 5),
        especialista: row.NumeroOperacion.substring(5, 7),
        empresa: row.NumeroOperacion.substring(7, 10),
        cobranza: row.NumeroOperacion.substring(10, 15)
    };
}

function ObtenerRequestRowDeTabla(row) {
    var tipoDoc = row.tipoDoc;
    var result = {};

    if (tipoDoc == 1) { //Carta
        result.type = "Carta";
        result.carta = GetParamsParaCarta(row);
    }
    else if (tipoDoc == 2) { //swift
        result.type = "Swift"
        result.swift = GetParamsParaMensajeSwift(row);
    }
    else if (tipoDoc == 3) { //Contabilidad
        result.type = "Contabilidad";
        result.contabilidad = GetParamsParaContabilidad(row);
    } else if (tipoDoc == 4) {//swiftOriginalCopia
        result.type = "SwiftOriginalCopia";
        result.swiftOriginalCopia = GetParamsParaMensajeSwiftOriginaCopia(row);
    }
    else if (tipoDoc == 5) {//PlanillaInvisibleExportacion
        result.type = "planillaInvisibleExportacion";
        result.planillaInvisibleExportacion = GetParamsParaPlanillas(row);
    }
    else if (tipoDoc == 6) {//PlanillaReemplazos
        result.type = "planillaReemplazos";
        result.planillaReemplazos = GetParamsParaPlanillasReemplazo(row);
    }
    else if (tipoDoc == 7) {//PlanillaAnulada
        result.type = "PlanillaAnulada";
        result.planillaAnulada = GetParamsParaPlanillas(row);
    }
    else if (tipoDoc == 8) {//PlanillaVisibleExportacion
        result.type = "PlanillaVisibleExportacion";
        result.planillaVisibleExportacion = GetParamsParaPlanillas(row);
    }

    return result;
}
