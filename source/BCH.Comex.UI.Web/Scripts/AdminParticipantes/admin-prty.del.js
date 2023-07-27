$(document).ready(function () {
});

//$('#BtnAceptarEliminaPari').click(function () {

//    var model = { idPrty: v_oPrty.idPrty, Sflag: 2 };
//    $.ajax({
//        url: './AdminParticipantes/SyGet_Cta',
//        contentType: 'application/json; charset=utf-8',
//        type: 'POST',
//        dataType: 'html',
//        data: JSON.stringify(model),
//        statusCode: {
//            500: function () {
//                alert("Error al cambiar");
//            },

//            404: function () {
//                alert("Recurso no encontrado");
//            }
//        },
//        success: function (result) {
//            if (parseInt(result) > 0) {
//                alert("Este Participante no puede ser eliminado debido a que existe un movimiento con una de sus cuentas.");
//            } else {
//                alert(v_oPrty.estado);
//            }
//        }
//    })
//});