
$(document).ready(function () {
    if (modelCompleto.Mensaje.estado != "ERROR_VALIDACION" && modelCompleto.Mensaje.estado != "MT300_PREVIO") {
        $("#panelMensajes").hide()
    }
    else {
        $("#Mensaje_reference").removeAttr("Disabled");
        $("#Mensaje_safekeeping").removeAttr("Disabled");
        $("#Mensaje_bookedBy").removeAttr("Disabled");
        $("#Mensaje_valueDate").removeAttr("Disabled");
        $("#Mensaje_rate").removeAttr("Disabled");
        $("#Mensaje_codMonedame").removeAttr("Disabled");
        $("#Mensaje_campo32B").removeAttr("Disabled");
        $("#Mensaje_codMonedamn").removeAttr("Disabled");
        $("#Mensaje_campo33B").removeAttr("Disabled");
    }
});

jQuery.extend(jQuery.validator.methods, {
    number: function (value, element) {
        return this.optional(element)
            || /^-?(?:\d+|\d{1,3}(?:\.\d{3})+)(?:,\d+)?$/.test(value)
            || /^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/.test(value);
    }
});