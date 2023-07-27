$(document).ready(function () {
    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI).ajaxError($.unblockUI);

    var lastIndexPlaza =$("#l_plaza").prop("selectedIndex");
    var lastIndexbenef = $("#l_benef").prop("selectedIndex");
    var lastNombre = $("#Tx_Nombre").val();



    $("#l_benef").on("blur", function () {
        var newI = $(this).prop("selectedIndex");
        if (newI != lastIndexbenef) {
            $.ajax({
                url: $("#urlBenef").val() + "/" +newI,
                success: function (data) {
                    UpdateView(data);
                    lastIndexbenef = newI;
                }
            });
        }
    });

    $("#l_plaza").on("blur", function () {
        var newI = $(this).prop("selectedIndex");
        if (newI != lastIndexPlaza) {
            $.ajax({
                url: $("#urlPlaza").val() + "/" + newI,
                success: function (data) {
                    UpdateView(data);
                    lastIndexPlaza = newI;
                }
            });
        }
    });

    $("#Co_Generar").click(function () {
        $.ajax({
            url: $("#urlGenerar").val(),
            method: "POST",
            data: { jsonModel: [$("#Tx_Nombre").val().toUpperCase(), $("#Tx_Rut").val().toUpperCase(), $("#l_cor").prop("selectedIndex")] },
            success: function(data){
                UpdateView(data);
                if (data.ListaErrores.length == 0) {
                    loadMessages([{ Type: 1, Text: "El Documento ha sido Generado Correctamente", Title: "Documentos Valorados" }]);
                }
            } 
        });
    });

    $("#Co_Cancelar").click(function () {
        $.ajax({
            url: $("#urlCancelar").val(),
            method: "GET",
            success: function (data) {
                var confirmaciones = data.ListaConfirmaciones;
                showConfirmMessages(confirmaciones, false, function (confirms) {
                    var allTrue = true;
                    for (var i = 0; i < confirms.length && allTrue; i++) {
                        allTrue = allTrue && confirms[i];
                    }
                    if (allTrue) {
                        $.ajax({
                            url: $("#urlCancelar").val()+"?resMsg=true",
                            method: "GET",
                            success: function (data) {
                                window.location = $("#base_url").val() + "ContabilidadGenerica/Grabar/Despues_De_Cheques"
                            }
                        });
                    }
                });
            }
        });
    });


    $("#Co_Aceptar").click(function () {
        window.location = $("#urlAceptar").val()
    });


    function UpdateView(data) {
        var docs = "l_montos";
        $("#" + docs)
            .empty()
            .append($.map(data[docs].Items, function (item) {
                return $("<option/>", {
                    value: item.Data,
                    text: (item.Value.replace(/\t/g, "\xA0\xA0\xA0\xA0\xA0\xA0\xA0\xA0"))
                });
            }));

        var cors = "l_cor";
        $("#" + cors)
            .empty()
            .append($.map(data[cors].Items, function (item) {
                return $("<option/>", {
                    value: item.Data,
                    text: item.Value
                });
            }));

        var listEnable = ["Tx_Rut", "l_plaza", "l_cor", "Co_Generar", "Co_Aceptar"];
        $.each(listEnable, function (index, value) {
            $("#" + value).prop("disabled", !data[value].Enabled);
        });

        var listIndexes = ["l_cor", "l_montos"];
        $.each(listIndexes, function (index, value) {
            $("#" + value).prop("selectedIndex", data[value].ListIndex);
        });

        var listIndexesPlus = ["l_plaza"];
        $.each(listIndexesPlus, function (index, value) {
            $("#" + value).prop("selectedIndex", data[value].ListIndex+1);
        });

        var listTexts = ["Tx_Nombre", "Tx_Rut"];
        $.each(listTexts, function (index, value) {
            $("#" + value).val(data[value].Text);
        });

        loadMessages(data.ListaErrores);
        focusOnErrorControl(data.ListaErrores);
    }

    $(window).keydown(function (ev) {
        try {
            var keycode = (ev.keyCode ? ev.keyCode : ev.which);
            if (keycode == 9) {  // Presiona Tab
                RecorrerTabIndex(-1, ev);
            }
            else if (keycode == 13) // Enter
            {
                if ($("#Co_Aceptar").is(":enabled")) {
                    $("#Co_Aceptar").click();
                } else if ($("#Co_Generar").is(":enabled")) {
                    $("#Co_Generar").click();
                }
            }
        } catch (e) { console.log(e); }
    });
});