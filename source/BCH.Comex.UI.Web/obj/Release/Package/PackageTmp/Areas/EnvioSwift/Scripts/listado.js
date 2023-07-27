var _swift = "AVOID_SEARCH";

$("body").keyup(function (e) {
    if (e.keyCode == 13 ) buscar();
    //Click it again to slide up back up, right?
});

$(window).resize(function () {
    $('#tableBancos').bootstrapTable('resetView', {
        height: getHeight()
    });
});

$('#tableBancos').bootstrapTable({
    height: getHeight(),
    onLoadSuccess: function (data) {
        if (data == "LIMIT_EXCEEDED") {
            $('#tableBancos').bootstrapTable('removeAll');
            $('.no-records-found').find('td').html("Su búsqueda retornó demasiados resultados, agregue algún filtro o <a class='lnkDescargarExcel' href='#'>descargue todos los resultados</a> en formato Excel");
            $('#badgeCantResultados').text(0);
        } else {
            $('#badgeCantResultados').text(data.length);
        }
    },
    method: 'post',
    url: $('#urlBancaMundial').val(),
    queryParams: function (p) {
        return {
            swift: _swift,
            pais: $('#ddl_paises').val(),
            ciudad: $('#txtCiudad').val(),
            banco: $('#txtBanco').val(),
            direccion: $('#txtDireccion').val()
        };
    },
    height: 550,
    pagination: true,
    search: true,
    showRefresh: true,
    pageSize: 25,
    pageList: [10, 25, 50, 100, 200],
});

function buscar() {
    _swift = $('#txtSwift').val();
    $('#tableBancos').bootstrapTable('refresh');
}

function getHeight() {
    return $(window).height() - $('h1').outerHeight(true);
}

function imprimir() {
    var swift = _swift;
    var pais = $('#ddl_paises').val();
    var ciudad = $('#txtCiudad').val();
    var banco = $('#txtBanco').val();
    var direccion = $('#txtDireccion').val();
    window.open($('#urlBancaMundialImprimir').val() + "?swift=" + swift + "&pais=" + pais + "&ciudad=" + ciudad + "&banco=" + banco + "&direccion=" + direccion);
}

function excel() {
    var swift = _swift;
    var pais = $('#ddl_paises').val();
    var ciudad = $('#txtCiudad').val();
    var banco = $('#txtBanco').val();
    var direccion = $('#txtDireccion').val();
    var postal = $('#txtPostal').val();
    window.open($('#urlBancaMundialExcel').val() + "?swift=" + swift + "&pais=" + pais + "&ciudad=" + ciudad + "&banco=" + banco + "&direccion=" + direccion , "_blank");
}

function clickDescargarExcel() {
    var swift = $('#txtSwift').val();
    var pais = $('#ddl_paises').val();
    var ciudad = $('#txtCiudad').val();
    var banco = $('#txtBanco').val();
    var direccion = $('#txtDireccion').val();
    var postal = $('#txtPostal').val();
    var url = $('#urlBancaMundialExcel').val() + "?swift=" + swift + "&pais=" + pais + "&ciudad=" + ciudad + "&banco=" + banco + "&direccion=" + direccion + "&postal=" + postal;

    DescargarArchivo(url, "resultados");
}

function DescargarArchivo(location, titulo) {
    $.blockUI({ message: '<h6>Generando, esta operación puede demorar algunos minutos...</h6>' })
    var tituloLocal = '';
    if (typeof titulo != 'undefined')
        tituloLocal = titulo;
    $.fileDownload(location)
        .done(function () { $.unblockUI(); /* nada que alertar, con la ventanita del browser pidiendo para bajar el archivo alcanza*/ })
        .fail(function () { showAlert("Error en la descarga.", "El archivo de " + tituloLocal + " no se pudo generar", "alert-danger"); $.unblockUI(); });
}

$(function () {
    $('#tableBancos').on('click', 'a.lnkDescargarExcel', clickDescargarExcel); //esta es la forma correcta de attachearse a elemento no creados todavia
});