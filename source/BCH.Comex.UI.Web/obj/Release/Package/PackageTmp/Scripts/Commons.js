(function () {
    // Union of Chrome, Firefox, IE, Opera, and Safari console methods
    var methods = ["assert", "assert", "cd", "clear", "count", "countReset",
      "debug", "dir", "dirxml", "dirxml", "dirxml", "error", "error", "exception",
      "group", "group", "groupCollapsed", "groupCollapsed", "groupEnd", "info",
      "info", "log", "log", "markTimeline", "profile", "profileEnd", "profileEnd",
      "select", "table", "table", "time", "time", "timeEnd", "timeEnd", "timeEnd",
      "timeEnd", "timeEnd", "timeStamp", "timeline", "timelineEnd", "trace",
      "trace", "trace", "trace", "trace", "warn"];
    var length = methods.length;
    var console = (window.console = window.console || {});
    var method;
    var noop = function () { };
    while (length--) {
        method = methods[length];
        // define undefined methods as noops to prevent errors
        if (!console[method])
            console[method] = noop;
    }
})();

$(document).ready(function () {
    $.ajaxSetup({ cache: false });
    //$(document).ajaxStop(BindearMonto);
    BindearMonto();
});

$.fn.DetectarTecla = function (fnc) {
    return this.each(function () {
        $(this).keypress(function (ev) {
            var keycode = (ev.keyCode ? ev.keyCode : ev.which);
            if (keycode == '9') {   // Presiona Tab
                fnc.call(this, ev);
            }
            if (keycode == '13') {  // Presiona Enter
                fnc.call(this, ev);
            }
        })
    })
}

function DecimalDotToComa(e) {
    if (!e.charCode && e.keyCode == 46)//para detectar el supr en firefox
        return true;
    var charCode = e.charCode || e.keyCode;
    if (e.ctrlKey && charCode == 86 || e.ctrlKey && charCode == 67 || e.shiftKey && charCode == 35 || e.shiftKey && charCode == 36)
        return true;
    if (!((charCode >= 48 && charCode <= 57) || (charCode >= 96 && charCode <= 105)) //si no es un numero IE/CHROME || FIREFOX
        && !((charCode == '46') || (charCode == '44'))//si no es coma o punto
        && !((charCode == 8) || (charCode == 9) || (charCode == 190) || (charCode == 188) || (charCode == 13))// si no es backspace ni tab ni punto ni coma ni enter
        && !(charCode >= 37 && charCode <= 40)//Si no son las fechas
        && !(!e.charCode && e.keyCode == 110)//punto decimal numpad para firefox        
        )
        return false;
    if ((charCode == '46') || (!e.charCode && e.keyCode == 46) || (!e.charCode && e.keyCode == 110) || (charCode == 190)) {
        
        /// Validar si esta todo seleccionado y al presionar
        /// punto o coma, debe permitir continuar con operación.
        var input = $(this)[0];
        if (!(input.selectionStart == 0 && input.selectionEnd == input.value.length)) {
            //Verifica si ya ingreso coma
            if ($(this).val().indexOf(',') > -1)
                return false;
        }

        // IE
        if (document.selection) {
            // Determines the selected text. If no text selected,
            // the location of the cursor in the text is returned
            var range = document.selection.createRange();
            // Place the comma on the location of the selection,
            // and remove the data in the selection
            var largoSeleccion = range.text.length;
            var largoText = $(this).val().length;
            range.text = ",";
            //cambios para que funcione en IE8
            if (largoSeleccion == largoText ) {
                range.moveStart('character', 1);
                range.select();
            }
            // Chrome + FF
        } else if (this.selectionStart || this.selectionStart == '0') {
            // Determines the start and end of the selection.
            // If no text selected, they are the same and
            // the location of the cursor in the text is returned
            // Don't make it a jQuery obj, because selectionStart 
            // and selectionEnd isn't known.
            var start = this.selectionStart;
            var end = this.selectionEnd;
            // Place the comma on the location of the selection,
            // and remove the data in the selection
            $(this).val($(this).val().substring(0, start) + ','
            + $(this).val().substring(end, $(this).val().length));
            // Set the cursor back at the correct location in 
            // the text
            this.selectionStart = start + 1;
            this.selectionEnd = start + 1;
        } else {
            // if no selection could be determined, 
            // place the comma at the end.
            $(this).val($(this).val() + ',');
        }
        return false;
    }
    else if (charCode == '44' || (charCode == 188)) {
        /// Validar si esta todo seleccionado y al presionar
        /// punto o coma, debe permitir continuar con operación.
        var input = $(this)[0];
        if (!(input.selectionStart == 0 && input.selectionEnd == input.value.length)) {
            //Verifica si ya ingreso coma
            if ($(this).val().indexOf(',') > -1)
                return false;
        }
    }
}

function isNumeric(input) {
    return (input - 0) == input && ('' + input).trim().length > 0;
}

/*Types of alerts -- "alert-success","alert-info", "alert-warning","alert-danger"*/
function showAlert(msgDestacado, msgNormal, type, autoClose, alertPlaceholder) {
    if (typeof autoClose === undefined) {
        autoClose = true;
    }

    var message = { Title: msgDestacado, Text: msgNormal, Type: type, AutoClose: autoClose };
    var messages = [];
    messages.push(message);

    loadMessages(messages, alertPlaceholder);
}

function loadMessages(messages, alertPlaceholder) {
    if(typeof alertPlaceholder === undefined || alertPlaceholder == null || alertPlaceholder == "")
    {
        alertPlaceholder = "msg-zone";
    }
    
    EmptySelectedZone(alertPlaceholder);

    var divAlertas = $("#" + alertPlaceholder);
    
    if (messages) {
        $.each(messages, function (i, elem) {
            var clase = "";

            if (isNumeric(elem.Type)) {
                var enumTypes = {
                    0: "",
                    1: "alert-success",
                    2: "alert-info",
                    3: "alert-danger",
                    4: "alert-danger",
                    6: "alert-warning"
                };

                clase = enumTypes[elem.Type];
            }
            else {
                clase = elem.Type;
            }

            var idAlerta = "alertdiv" + i;

            var alerta = $('<div id="' + idAlerta + '" class="alert"><a class="close" data-dismiss="alert">×</a><span><strong>' + elem.Title + '</strong> ' + elem.Text + '</span></div>');
            alerta.addClass(clase);

            divAlertas.append(alerta);
                        
            if (elem.AutoClose) {
                setTimeout(function () {
                    $("#" + idAlerta).remove();
                }, 5000);
            }
        });
    }
};

function GenerateAlertCreator(divId, msgFnc) {
    return function (messages) {
        
    };
}

function EmptyMessageZone()
{
    EmptySelectedZone("msg-zone");
}

function EmptySelectedZone(id) {
    $("#" + id).empty();
}

$.validator.addMethod('formated-number', function (value, el, param) {
    var res = numeral().unformat(value);
    return res > 0;
}, "El valor debe ser un número distinto de cero.");

var FundTransfer = FundTransfer || {};
FundTransfer.Common = {};
/// Mapea un button a un control asumiendo que origen es de tipo UI_Button
FundTransfer.Common.MapButton = function (destino, origen) {

    if (origen.Enabled) {
        destino.removeAttr('disabled');
    } else {
        destino.attr('disabled', 'disabled');
    }

    destino.val(origen.Text);
}
/// Mapea un button a un control asumiendo que origen es de tipo UI_TextBox
FundTransfer.Common.MapTextBox = function (destino, origen) {
    if (origen.Enabled) {
        destino.removeAttr('disabled');
    } else {
        destino.attr('disabled', 'disabled');
    }

    destino.val(origen.Text);
}
/// Mapea un button a un control asumiendo que origen es de tipo UI_CheckBox
FundTransfer.Common.MapCheckBox = function (destino, origen) {
    if (origen.Enabled) {
        destino.removeAttr('disabled');
    } else {
        destino.attr('disabled', 'disabled');
    }

    destino.prop('checked', origen.Checked);
}
/// Mapea un button a un control asumiendo que origen es de tipo UI_Combo
FundTransfer.Common.MapComboBox = function (destino, origen) {
    if (origen.Enabled) {
        destino.removeAttr('disabled');
    } else {
        destino.attr('disabled', 'disabled');
    }

    destino.empty();

    ///agrego el valor por defecto si viene seleccionado el -1
    //if (origen.ListIndex === -1) {
        destino.append(
            $('<option/>', {
                value: '',
                text: '--Seleccione--',
                selected: true
            })
        );
    //}

    ///agrego el resto de los valores
    $.each(origen.Items, function (index, item) {
        destino.append(
            $('<option/>', {
                value: item.Data,
                text: item.Value,
                selected: (origen.ListIndex === index)
            })
        );
    });
}
/// Mapea un button a un control asumiendo que origen es de tipo UI_ListBox
FundTransfer.Common.MapListBox = function (destino, origen) {
    if (origen.Enabled) {
        destino.removeAttr('disabled');
    } else {
        destino.attr('disabled', 'disabled');
    }

    destino.empty();

    ///agrego el resto de los valores
    $.each(origen.Items, function (index, item) {
        destino.append(
            $('<option/>', {
                value: item.Data,
                text: item.Value,
                selected: (origen.ListIndex === index)
            })
        );
    });
}

ko.bindingHandlers["enableChildren"] = {
    "update": function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        if (value && element.disabled) {
            element.removeAttribute("disabled");
            $(element).find('*').removeAttr('disabled');
        }
        else if ((!value) && (!element.disabled)) {
            element.disabled = true;
            $(element).find('*').attr('disabled', 'disabled');
        }
    }
};
ko.bindingHandlers['disableChildren'] = {
    'update': function (element, valueAccessor) {
        ko.bindingHandlers['enableChildren']['update'](element, function () { return !ko.utils.unwrapObservable(valueAccessor()) });
    }
};

//custom bingind para knockout y jquery.inputmask
ko.bindingHandlers.inputmask =
{
    init: function (element, valueAccessor, allBindingsAccessor) {
        var mask = valueAccessor();
        var observable = mask.value;

        if (ko.isObservable(observable)) {
            $(element).on('focusout change', function () {
                if ($(element).inputmask('isComplete')) {
                    observable($(element).val());
                } else {
                    observable(null);
                }
            });
        }
        $(element).inputmask(mask);
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var mask = valueAccessor();
        var observable = mask.value;

        if (ko.isObservable(observable)) {
            var valuetoWrite = observable();
            $(element).val(valuetoWrite);
        }
    }
};

//inputmask: defino el alias montoConDecimales
$.extend($.inputmask.defaults.aliases, {
    montoConDecimales: {
        alias:"numeric",
        radixPoint: ",", //separador de decimales
        autoGroup: true,
        groupSeparator: ".",
        digits: 2,
        digitsOptional: true,
        placeholder: "",
        //numericInput: false,
        rightAlign: false,
        integerDigits: 12
        //showMaskOnHover: false,
        //showMaskOnFocus: false,
        //clearMaskOnLosFocus: true,
    }
});

$.extend($.inputmask.defaults.aliases, {
    montoTipoCambio: {
        alias: "numeric",
        radixPoint: ",", //separador de decimales
        groupSeparator: ".",
        //autoGroup: true,
        digits: 4,
        digitsOptional: true,
        placeholder: "0",
        numericInput: false,
        rightAlign: false,
        integerDigits: 8
        //showMaskOnHover: false,
        //showMaskOnFocus: false,
        //clearMaskOnLosFocus: true,
    }
});

$.extend($.inputmask.defaults.aliases, {
    montoParidad: {
        alias: "numeric",
        radixPoint: ",", //separador de decimales
        groupSeparator: ".",
        //autoGroup: true,
        digits: 10,
        digitsOptional: true,
        placeholder: "",
        numericInput: false,
        rightAlign: false
        //showMaskOnHover: false,
        //showMaskOnFocus: false,
        //clearMaskOnLosFocus: true,
    }
});

$.extend($.inputmask.defaults.definitions, {
    'K': {
        validator: "[0-9kK]",
        cardinality: 1,
        placeholder: '',
        casing: "upper" //auto uppercasing
    },
    'G': {
        validator: "[0-9\-]",
        cardinality: 1,
        placeholder: ''
    }
});

function BindearMonto() {
    $(document).on("keydown", "input[type='text'].monto", DecimalDotToComa);

    ////asigno las mascaras
    //$(".montoTipoCambio").inputmask({ "alias": "montoTipoCambio" });
    //$(".montoConDecimales").inputmask({ "alias": "montoConDecimales" });
    //$(".montoParidad").inputmask({ "alias": "montoParidad" });
}

function EsSoloCaracter(char) {
    var regex = new RegExp("^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ \s]$");
    return !regex.test(char);
}

function EsCaracterEspecial(char) {
    var regex = new RegExp("^[a-zA-Z0-9 \b\t$]+$");
    return !regex.test(char);
}

function _EsCaracterEspecialConPunto(char) {
    var regex = new RegExp("^[a-zA-Z0-9.ñ \b\t$]+$");
    return !regex.test(char);
}

function _EsCaracterEspecialNemonico(char) {
    var regex = new RegExp("^[a-zA-Z0-9-\/+ñÑ% \b\t$]+$");
    return !regex.test(char);
}

var yaEstoyEnEventoChange = false;
function toUppercase(target) {
    if (!yaEstoyEnEventoChange) {
        yaEstoyEnEventoChange = true;
        $(target).val($(target).val().toUpperCase()).change();
        yaEstoyEnEventoChange = false;
        return true;
    }
    else return false;
}

    
var showConfirmMessages = function (messages, validateAll, Callback) {
    function showModalConfirm() {
        if (currentMessageIndex == messagesConfirmed.length) {
            Callback(messagesConfirmed);
        } else {
            var msg = messages[currentMessageIndex];
            if (typeof msg == "object") {
                msg = msg.Text;
            }
            var body = $("<p>" + msg + "</p>");
            var confirmBtn = $("<button id='BtnModalConfirmar' class='btn btn-primary pull-right'>Confirmar</button>");
            confirmBtn.off("click");
            confirmBtn.click(function () {
                messagesConfirmed[currentMessageIndex] = true;
                currentMessageIndex++;
                modal.modal("hide");

            });

            var cancelBtn = $("<button class='btn btn-default pull-left'>Cancelar</button>");
            cancelBtn.off("click");
            cancelBtn.click(function () {
                if (validateAll) {
                    currentMessageIndex++;
                } else {
                    currentMessageIndex = messagesConfirmed.length;
                }
                modal.modal("hide");
                modal.on("hidden.bs.modal", function () {
                    showModalConfirm();
                });
            });

            modal.off("hidden.bs.modal");
            configureModal("Confirmación", body, [cancelBtn, confirmBtn]);
            modal.on("hidden.bs.modal", function () {
                showModalConfirm();
            });
            modal.modal("show");
        }
    }
    var modal = $("#modal");
    var messagesConfirmed = $.map(messages, function () {
        return false;
    });
    var currentMessageIndex = 0;
    showModalConfirm();
};

var configureModal = function (title, body, footer) {
    $("#modal-title").text(title);
    $("#modal-body").empty().append(body);
    $("#modal-footer").empty().append(footer);
};

// deshabilito el botón de atrás
function DeshabilitarBotonAtras() {
    window.location.hash = "no-back";
    window.location.hash = "otro-no-back";//workaround para chrome
    window.onhashchange = function () { window.location.hash = "no-back"; }
}

/**
 * Función que recorre los TabIndex de una pagina, si envias el valor por defecto -1 en TabIndex, este toma el foco por del elemento posicionado.
 * @param {number} TabIndex
 * @param {event} ev
 * @return 
 */
function RecorrerTabIndex(TabIndex, ev) {
    var maxtab = -1;
    var mintab = 1;
    $('[tabindex]').attr('tabindex', function (a, b) {
        maxtab = Math.max(maxtab, +b);
    });
    var tab = (TabIndex > -1) ? TabIndex : $(document.activeElement).attr("tabindex");
    if (typeof tab != "undefined") {
        ev.preventDefault();
        if (!ev.shiftKey) {
            tab++;
            while (!$("[tabindex='" + tab + "']").is(":visible") || $("[tabindex='" + tab + "']").is(":disabled")) {
                if (tab < maxtab)
                    tab++;
                else
                    tab = 1;
            }
        } else {
            tab--;
            while (!$("[tabindex='" + tab + "']").is(":visible") || $("[tabindex='" + tab + "']").is(":disabled")) {
                if (tab > 0)
                    tab--;
                else
                    tab = maxtab;
            }
        }
        $("[tabindex='" + tab + "']").focus().trigger('select');
    } else {
        ev.preventDefault();
        while (!$("[tabindex='" + mintab + "']").is(":visible") || $("[tabindex='" + mintab + "']").is(":disabled")) {
            if (mintab <= maxtab)
                mintab++;
            else
                mintab = 1;
        }
        $("[tabindex='" + mintab + "']").focus().trigger('select');
    }
}

$.fn.selectRange = function (start, end) {
    var e = document.getElementById($(this).attr('id'));
    if (!e) return;
    else if (e.setSelectionRange) { e.focus(); e.setSelectionRange(start, end); } /* WebKit */
    else if (e.createTextRange) { var range = e.createTextRange(); range.collapse(true); range.moveEnd('character', end); range.moveStart('character', start); range.select(); } /* IE */
    else if (e.selectionStart) { e.selectionStart = start; e.selectionEnd = end; }
};


function toDate(selector) {
    var $from = selector.split("/");
    var $year = $from[2].split(' ')[0].trim();
    return new Date($year, $from[1] - 1, $from[0], $from[2].split(' ')[1].split(':')[0].trim(), $from[2].split(' ')[1].split(':')[1].trim(), $from[2].split(' ')[1].split(':')[2].trim());
}

// Función que mediante el codigo de tecla recibido por parametro retorna true si esta dentro de los aceptados.
function activaTeclasEspeciales(codKey) {
    // En el posible caso de que codKey venga como numerico, lo convertimos en string
    codKey = codKey.toString();
    try {
        switch (codKey) {
            // Tecla FIN
            case "35":
                return true;
            // Tecla INICIO
            case "36":
                return true;
            // Tecla FLECHA IZQUIERDA
            case "37":
                return true;
            // Tecla FLECHA ARRIBA
            case "38":
                return true;
            // Tecla FLECHA DERECHA
            case "39":
                return true;
            // Tecla FLECHA ABAJO
            case "40":
                return true;
            // Tecla SUPRIMIR
            case "46":
                return true;
            default:
                return false;
        }
    } catch (e) {
        console.log(e);
    }
}

function focusOnErrorControl(MensajesDeError) {
    if (MensajesDeError != null && MensajesDeError.length > 0) {
        // le pasamos el nombre del control del primer error de la lista
        $("#" + MensajesDeError[0].ControlName).focus();
    }
}

function ValidarCaractereskeypress(textbox, opcion, signos) {
    $("#" + textbox).keypress(function (e) {

        var numeros = "1234567890";
        var letras = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ áéíóúÁÉÍÓÚ";
        var correo = "@._";
        var signo = "-";
        var digitoverificador = "kK";
        var puntuacion = ".,";
        var coma = ",";
        var varios = "@._+-.,;:!¡¿?/()*><°=";
        var slash = "/";

        var resultado = "";

        if (opcion == 1) {//solo numeros
            resultado = numeros;
        }
        else if (opcion == 2) {//solo letras
            resultado = letras;
        }
        else if (opcion == 3) {//alfanumericos
            resultado = letras + numeros;
        }
        else if (opcion == 4) {//e-mail
            resultado = letras + numeros + correo;
        }
        else if (opcion == 5) {//caracteres
            resultado = letras + numeros + varios;
        }
        else if (opcion == 6) {//digitoverificador
            resultado = numeros + digitoverificador;
        }
        else if (opcion == 7) {//numeros de certificados
            resultado = letras + numeros + signo;
        }
        else if (opcion == 8) {//fecha
            resultado = numeros + slash;
        }
        else if (opcion == 9) {//alfanumericos y Slash 
            resultado = letras + numeros + slash;
        }

        if (signos == 1) {//signo
            resultado += signo;
        }
        else if (signos == 2) {//signo y puntuacion
            resultado += signo + puntuacion;
        }
        else if (signos == 3) {//coma
            resultado += coma;
        }

        if (resultado.indexOf(String.fromCharCode(e.which)) != -1) {
            return true;
        }
        else {
            return false;
        }
    });
}
