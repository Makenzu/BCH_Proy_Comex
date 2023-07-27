/**
 * Bootstrap Table Spanish (España) translation
 * Author: Antonio Pérez <anpegar@gmail.com>
 */
 (function ($) {
    'use strict';
    
    $.fn.bootstrapTable.locales['es-SP'] = {
        formatLoadingMessage: function () {
            return 'Cargando, por favor espera...';
        },
        formatRecordsPerPage: function (pageNumber) {
            return pageNumber + ' registros por p&#225;gina.';
        },
        formatShowingRows: function (pageFrom, pageTo, totalRows) {
            return pageFrom + ' - ' + pageTo + ' de ' + totalRows + ' registros.';
        },
        formatDetailPagination: function (totalRows) {
            return "Mostrando " + totalRows + " registros";
        },
        formatSearch: function () {
            return 'Buscar';
        },
        formatNoMatches: function () {
            return 'No se han encontrado registros.';
        },
        formatRefresh: function () {
            return 'Actualizar';
        },
        formatToggle: function () {
            return 'Alternar';
        },
        formatColumns: function () {
            return 'Columnas';
        },
        formatAllRows: function () {
            return 'Todo';
        },
        formatMultipleSort: function () {
            return 'Ordenamiento múltiple';
        },
        formatAddLevel: function () {
            return "Agregar nivel";
        },
        formatDeleteLevel: function () {
            return "Eliminar nivel";
        },
        formatColumn: function () {
            return "Columna";
        },
        formatOrder: function () {
            return "Orden";
        },
        formatSortBy: function () {
            return "Ordenar por";
        },
        formatThenBy: function () {
            return "Luego por";
        },
        formatSort: function () {
            return "Ordenar";
        },
        formatCancel: function () {
            return "Cancelar";
        },
        formatDuplicateAlertTitle: function () {
            return "Duplicado(s) detectados!";
        },
        formatDuplicateAlertDescription: function () {
            return "Por favor remueva o cambie las columnas duplicadas.";
        }


    };

    $.extend($.fn.bootstrapTable.defaults, $.fn.bootstrapTable.locales['es-SP']);

})(jQuery);