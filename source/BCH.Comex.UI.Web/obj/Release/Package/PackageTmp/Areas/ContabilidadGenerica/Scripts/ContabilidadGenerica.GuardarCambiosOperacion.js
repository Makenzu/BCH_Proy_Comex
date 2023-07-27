$(document).ready(function () {
    var action = $("form").attr("action");
    $("#cancel").focus();

    $("#cancel").on("click", function () {
        $("#ok").val("false");
    });
    $("#accept").on("click", function () {
        $("#ok").val("true");
    });
});