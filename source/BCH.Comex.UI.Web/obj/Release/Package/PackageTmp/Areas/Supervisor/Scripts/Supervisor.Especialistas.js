
$(function () {
	var baseUrl = $("#base_url").val();
	var dataTable;
	if ($("#opciones").val() == '1') {
	    dataTable = dataltEspecialista;
	} else {
	    dataTable = dataltEspecialistaFill;
	}

	//actualiza la tabla
	$('#tableEspecialistas').bootstrapTable({
		classes: 'table table-hover resultRow',
		columns: [
			{ title: 'Fin Dia', formatter: AccionesGridFormatter, events: operateEvents, align: 'center', width: '80px' },
            { field: 'CecoEsp', title: 'Cod. Especialista', sortable: true, align: 'center', width: '150px' },
			{ field: 'NomEsp', title: 'Nom. Especialista', sortable: true },
            { field: 'FecIni', title: 'Inicio', sortable: true, align: 'center' },
			{ field: 'FecFin', title: 'Fin', sortable: true, align: 'center' },
			{ field: 'ConFin', title: '', sortable: false, visible: false }
		],
		hideColumn: 'ConFin',
		data: dataTable,
		method: 'post',
		pagination: false,
		locale: 'es-CL',
		search: true,
		showRefresh: true
	});

		
	$("#opciones").change(function () {
		if ($("#opciones").val() == '0') {
			$('#tableEspecialistas').bootstrapTable('load', dataltEspecialistaFill);
		} else {
			$('#tableEspecialistas').bootstrapTable('load', dataltEspecialista);
		}
	});
});


function AccionesGridFormatter(value, row, index) {
	var htmlAccion = "";

	var ConFin = row['ConFin'];

	if (ConFin == 0) { //Tiene Cierre
	    htmlAccion = '<a class="btnFinDia accionRow" href="javascript:void(0)" title="Fin de Dia"><i class="glyphicon glyphicon-log-out"></i></a>';
	} else {
	    htmlAccion = '<span class="label label-default">Cerrado</span>';
	}
	
	return htmlAccion;
}

window.operateEvents = {
	'click .btnFinDia': function (e, value, row, index) {
		btnFinDiaDeRow(row);
    }	
};

function btnFinDiaDeRow(row) {
    var baseUrl = $("#base_url").val();

    if (confirm('Esta seguro que quiere forzar el fin de dia de: ' + row['NomEsp'])) {
        $.ajax({
            url: baseUrl + "Supervisor/VisualizacionUsuario/VisualizacionUsuario_FinDia",
            method: "POST",
            data: { cecoEsp: row.CecoEsp },
            success: function (data) {
                loadMessages(data.ListaErrores);
                VisualizacionUsuarioViewModelToView(data);
            },
            error: function (data) {

            }
        });
    }


}

function VisualizacionUsuarioViewModelToView(data) {

    dataltEspecialista = data.ltEspecialista;
    dataltEspecialistaFill = data.ltEspecialistaFill;

    if ($("#opciones").val() == '0') {
        $('#tableEspecialistas').bootstrapTable('load', dataltEspecialistaFill);
    } else {
        $('#tableEspecialistas').bootstrapTable('load', dataltEspecialista);
    }

}



