$(function () {
    ArmarTablaBootstrap();
    $("#btnImprimir").click(AceptacionPrint);
});

function ArmarTablaBootstrap() {
	var tablaPlanillas = $("#tablaPlanillas");
	tablaPlanillas.bootstrapTable({
		classes: "table table-hover table-condensed table-no-bordered",
		data: modelTabla,
		clickToSelect: false,
		sortable: false,
	    search: true,
        showRefresh: true
	});

}

function montoFormatter(value, row, index) {
    return numeral(value).format("0,0.00");
}

function AceptacionPrint() {
    var baseUrl = $("#base_url").val();
    var url = baseUrl + "FinDia/Aceptacion/ImpresionAceptacion";

    window.open(url, "_blank");
}