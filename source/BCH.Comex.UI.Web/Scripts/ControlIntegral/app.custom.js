var _ToastrTitulo = "Control Integral";

var AppControlIntegral = function () {
    var toastrCustom = function () {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "positionClass": "toast-bottom-right",
            "onclick": null,
            "showDuration": "1000",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
    }

    var validatorCustom = function () {
        $.validator.setDefaults({
            errorElement: 'span',
            errorClass: 'help-block',
            focusInvalid: false,
            ignore: "",
            highlight: function (element) {
                $(element).closest('.form-group').addClass('has-error');
            },
            unhighlight: function (element) {
                $(element).closest('.form-group').removeClass('has-error');
            }
        });
    }
    return {
        initCustom: function () {           
            toastrCustom();
            validatorCustom();
        }
    }

}();

(function ($) {
    $.fn.AppControlIntegral = function () {
        var jq = this;
        return {

            //initMascaraMonto: function (decimales) {
            //    var opciones = { radixPoint: ",", digits: decimales, autoGroup: true, groupSeparator: ".", groupSize: 3 };
            //    if (decimales == 0)
            //        opciones = { digits: decimales, autoGroup: true, groupSeparator: ".", groupSize: 3 };
            //    return jq.each(function () {
            //        jq.attr('maxlength', '17'); //Incluye Puntos
            //        jq.inputmask("decimal", opciones);
            //    });
            //},

        }
    }
})(jQuery);