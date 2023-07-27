var esPrimera = true;
var _nroReporte = 0;

function mostrarRegistrosExistentes() {
    var tablaRegistrosExistentes = $('#tablaRegistrosExistentes');
    tablaRegistrosExistentes.bootstrapTable({
        classes: 'table table-hover resultRow',
        data: modelCompleto.registrosExistentes,
        columns: getColumnsRegistrosExistentes(),
        pagination: false,
        sidePagination: 'client',
        search: false,
        searchAlign: 'left',
        clickToSelect: false,
        sortable: true,
        sortName: "NroOperacion",
        locale: "es-SP",
        maintainSelected: true,
        showRefresh: false,
        toolbar: "#toolbar"
    });
}

function mostrarRegistrosNuevos() {
    var tablaRegistrosNuevos = $('#tablaRegistrosNuevos');
    tablaRegistrosNuevos.bootstrapTable({
        classes: 'table table-hover resultRow',
        data: modelCompleto.registrosNuevosOK,
        columns: getColumnsRegistrosNuevos(),
        pagination: false,
        sidePagination: 'client',
        search: false,
        searchAlign: 'left',
        clickToSelect: false,
        sortable: true,
        sortName: "NroOperacion",
        locale: "es-SP",
        maintainSelected: true,
        showRefresh: false,
        toolbar: "#toolbar"
    });
}

function getColumnsRegistrosNuevos() {
    return [
        //{ checkbox: true, title: '#' },
        //{ title: 'Acciones', formatter: AccionesGridFormatter, events: operateEvents },
        { field: 'safekeeping', title: 'Safekeeping', sortable: true },
        { field: 'beneficiary', title: 'Beneficiario', sortable: true },
        { field: 'reference', title: 'Referencia', sortable: true },
        { field: 'estado', title: 'Estado', sortable: true },
        //{ title: 'Opciones', sortable: false }
    ];
}

function getColumnsRegistrosExistentes() {
    return [
        //por nueva definicion no se debe mostrar check en este caso
        //{ checkbox: true, title: '#' },
        //{ title: 'Acciones', formatter: AccionesGridFormatter, events: operateEvents },
        { field: 'safekeeping', title: 'Safekeeping', sortable: true },
        { field: 'beneficiary', title: 'Beneficiario', sortable: true },
        { field: 'reference', title: 'Referencia', sortable: true },
        { field: 'estado', title: 'Estado', sortable: true },
        //{ title: 'Opciones', sortable: false }
    ];
}

function clickGenerarSwift() {
    GenerarSwiftMasivo(modelCompleto);
}

function clickGenerarSwiftMasivo() {
    var tabla = $('#tablaRegistrosExistentes');
    var selections = tabla.bootstrapTable('getAllSelections');
    var idMarcados = "";

    for (let i = 0; i < selections.length; ++i) {
        var registroDetalle = selections[i];
        if (i === 0) {
            idMarcados = registroDetalle['id_archivo_detalle'];
        } else {
            idMarcados = idMarcados + ";" + registroDetalle['id_archivo_detalle'];
        }
    }

    var hiddenField = document.createElement("input");
    hiddenField.setAttribute("name", "idMarcados");
    hiddenField.setAttribute("value", idMarcados);
    hiddenField.setAttribute("type", "hidden");
    this.form.appendChild(hiddenField);

    this.form.submit();

}

$(document).ready(function () {
    if (modelCompleto && modelCompleto.archivoCargado) {
        mostrarRegistrosExistentes();
        mostrarRegistrosNuevos();
    }
    $("#btnGenerarSwiftMasivo").click(clickGenerarSwiftMasivo);
});