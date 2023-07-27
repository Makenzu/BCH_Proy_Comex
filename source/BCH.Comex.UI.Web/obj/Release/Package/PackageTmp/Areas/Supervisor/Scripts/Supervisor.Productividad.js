$(function () {
    $("#btnAceptar").click(DetalleProductividadPrint);
});


function DetalleProductividadPrint() {
    var baseUrl = $("#base_url").val();
    var url = baseUrl + "Supervisor/Productividad/DetalleProductividad?anio=" + $("#anio").val() + "&mes=" + $("#mesSelected").val() + "&generarHtmlCompleto=true&imprimir=true"; 

    window.open(url, "_blank");
}