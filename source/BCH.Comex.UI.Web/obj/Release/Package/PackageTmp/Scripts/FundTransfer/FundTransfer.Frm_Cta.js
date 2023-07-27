$(document).ready(function () {
    var baseUrl = $("#base_url").val();
    $(document).ajaxStart(function () {
        $.blockUI({ message: "<h6>Espere un momento ...</h6>" });
    }).ajaxStop($.unblockUI);

    
    var numero = "";
    var nemonico = "";
    var desc = "";

    var cuentas = $("#cuentas");


    function Click(row,replace) {
        var esNueva = !row[0];
        if (esNueva || !replace) {
            numero = row.Cta_Num.replace(/[\.-]/g, "");
            nemonico = encodeURIComponent(row.Cta_Nem);
            desc = encodeURIComponent(row.Cta_Nom);
        } else {
            numero = "";
            nemonico = "";
            desc = "";
        }

    }

    function Volver() {
        var rowSelect = $("#cuentas").bootstrapTable("getSelections");

        if(rowSelect.length == 0){
            Cancel();
        } else {
            numero = rowSelect[0].Cta_Num.replace(/[\.-]/g, "");
            nemonico = encodeURIComponent(rowSelect[0].Cta_Nem);
            desc = encodeURIComponent(rowSelect[0].Cta_Nom);
            window.location.href = baseUrl + "FundTransfer/NEMCTA_bot_acep_Click?num=" + numero + "&nem=" + nemonico + "&desc=" + desc;
        }        
    }

    function Cancel() {
        window.location.href = baseUrl + "FundTransfer/NEMCTA_bot_canc_Click";
    }
    cuentas.bootstrapTable({
        classes: "table table-hover table-condensed table-no-bordered",
        method: "get",
        cache: false,
        url: baseUrl + "FundTransfer/NEMCTA_Form_Load",
        pagination:true,
        sidePagination: 'client',
        pageSize:100,
        search:true,
        searchAlign:'left',
        clickToSelect:true,
        sortable:true,
        locale: "es-SP"
    });

    $("#btn_aceptar").click(Volver);
    $("#btn_cancelar").click(Cancel);

});