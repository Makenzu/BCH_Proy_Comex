$(document).ready(function () {
    var baseUrl = $("#base_url").val()+"ContabilidadGenerica/";
    $(document).ajaxStart(function () {
        $.blockUI({ message: "<h6>Espere un momento ...</h6>" });
    }).ajaxStop($.unblockUI);


    var numero = "";
    var nemonico = "";
    var desc = "";

    var cuentas = $("#cuentas");


    function Click(row, replace) {
        if (replace) {
            numero = row.Cta_Num.replace(/[\.-]/g, "");
            nemonico = encodeURIComponent(row.Cta_Nem);
            desc = encodeURIComponent(row.Cta_Nom);
        }
    }

    function UnClick() {
        numero = "";
        nemonico = "";
        desc = "";
    }

    function Volver() {
        if (numero === "" || nemonico === "" || desc === "") {
            Cancel();
        } else {
            window.location.href = baseUrl + "Plan_De_Cuentas/Aceptar?num=" + numero + "&nem=" + nemonico + "&desc=" + desc;
        }
    }

    function Cancel() {
        window.location.href = baseUrl + "Plan_De_Cuentas/Cancelar";
    }
    cuentas.bootstrapTable({
        classes: "table table-hover table-condensed table-no-bordered",
        method: "get",
        cache: false,
        url: baseUrl + "Plan_De_Cuentas/Get_Ctas",
        pagination: true,
        sidePagination: 'client',
        pageSize: 100,
        search: true,
        searchAlign: 'left',
        clickToSelect: true,
        sortable: true,
        locale: "es-SP",
        onCheck: function (row) {
            Click(row, true);
        },
        onUncheck: function (row) {
            UnClick(row, true);
        }
    });

    $("#btn_aceptar").click(Volver);
    $("#btn_cancelar").click(Cancel);

});