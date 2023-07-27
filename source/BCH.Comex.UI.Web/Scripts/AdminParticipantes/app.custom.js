
var AppAdminParticipantes = function () {


    //var setCulture = function () {
    //    Globalize.culture("es-CL");
    //}

    //var validatorCustom = function () {
    //    $.validator.setDefaults({
    //        errorElement: 'span',
    //        errorClass: 'help-block',
    //        focusInvalid: false,
    //        ignore: "",
    //        highlight: function (element) {
    //            $(element).closest('.form-group').addClass('has-error');
    //        },
    //        unhighlight: function (element) {
    //            $(element).closest('.form-group').removeClass('has-error');
    //        }
    //    });
    //}
    var ajaxLoading = function () {
        $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI);
    }

    return {
        initCustom: function () {

            //validatorCustom();
        },
        initAjaxLoading: function () {
            ajaxLoading();
        },
    }
}();

(function ($) {
    $.fn.AppAdminParticipantes = function () {
        var jq = this;
        return {
         
            initMascaraMonto: function (decimales) {
                var opciones = { radixPoint: ",", digits: decimales, autoGroup: true, groupSeparator: ".", groupSize: 3 };
                if (decimales == 0)
                    opciones = { digits: decimales, autoGroup: true, groupSeparator: ".", groupSize: 3 };
                return jq.each(function () {
                    jq.attr('maxlength', '17'); //Incluye Puntos
                    jq.inputmask("decimal", opciones);
                });
            },         
            initMascaraTasa: function () {
                var opciones = { rightAlignNumerics: false, radixPoint: ",", digits: 6, autoGroup: true, groupSeparator: ".", groupSize: 3 };
                return jq.each(function () {
                    jq.attr('maxlength', '9');
                    jq.inputmask("decimal", opciones);
                });
            },

            initMascaraNumero: function (Largo) {               
                opciones = { rightAlignNumerics: false, mask: '9', repeat: Largo, greedy: false };
                return jq.each(function () {
                    jq.attr('maxlength', Largo);
                    jq.inputmask("decimal", opciones);
                });
            }

            ////$("#MontoCuota").AppAdminParticipantes().setValueMonto(dataResult.MontoCuota, usoDecimales);
            //setValueMonto: function (value, decimales) {
            //    var formato = 'n' + decimales;
            //    var numero = Globalize.format(value, formato);
            //    return jq.each(function () {
            //        jq.val(numero);
            //    });
            //},

            //// $("#lblCapital").AppAdminParticipantes().setTextMonto(dataResult.Capital, usoDecimales);
            //setTextMonto: function (value, decimales) {
            //    var formato = 'n' + decimales;
            //    var numero = Globalize.format(value, formato);
            //    return jq.each(function () {
            //        jq.text(numero);
            //    });
            //}
        }
    }
})(jQuery);