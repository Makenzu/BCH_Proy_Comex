$(function () {
    $('.datepicker').datetimepicker({ locale: 'es', format: 'DD-MM-YYYY', maxDate: Date.now() });
});

$(document).ready(function () {
    Globalize.culture("es-CL");
});