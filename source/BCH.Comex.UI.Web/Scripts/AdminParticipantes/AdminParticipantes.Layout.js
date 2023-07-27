(function () {
    $("#modal").modal({ show: false, keyboard: false });
})();

var clearCombo = function (combo) {
    combo
        .find("option")
        .remove()
        .end()
        .append("<option value='-1'>-- Seleccione --</option>");
};

var loadCombo = function (items, combo) {
    $.each(items, function (i, item) {
        combo.append($("<option>", {
            value: item.Value,
            text: item.Text
        }));
    });
}

var clearMesssages = function () {
    $("#msg-zone").empty();
};

/*
    muestra una serie de mensajes de confirmacion y en el
    Callback pasa como parametro un array de bits indicando 
    cuales fueron exitosos
*/
var showConfirmMessages = function (messages, validateAll, Callback) {
    function showModalConfirm() {
        if (currentMessageIndex == messagesConfirmed.length) {
            Callback(messagesConfirmed);
        } else {
            var msg = messages[currentMessageIndex];

            if (typeof msg == "object") {
                msg = msg.Text;
            }

            var body = $("<p>" + msg + "</p>");
            var confirmBtn = $("<button class='btn btn-primary pull-right'>Confirmar</button>");
            confirmBtn.off("click");

            confirmBtn.click(function () {
                messagesConfirmed[currentMessageIndex] = true;
                currentMessageIndex++;
                modal.modal("hide");

            });

            var cancelBtn = $("<button class='btn btn-danger pull-left'>Cancelar</button>");
            cancelBtn.off("click");

            cancelBtn.click(function () {
                if (validateAll) {
                    currentMessageIndex++;
                } else {
                    currentMessageIndex = messagesConfirmed.length;
                }

                modal.modal("hide");
                modal.on("hidden.bs.modal", function () {
                    showModalConfirm();
                });
            });

            modal.off("hidden.bs.modal");
            configureModal("Confirmación", body, [cancelBtn, confirmBtn]);

            modal.on("hidden.bs.modal", function () {
                showModalConfirm();
            });

            modal.modal("show");
        }
    }
    var modal = $("#modal");

    var messagesConfirmed = $.map(messages, function () {
        return false;
    });

    var currentMessageIndex = 0;
    showModalConfirm();
};

var configureModal = function (title, body, footer) {
    $("#modal-title").text(title);
    $("#modal-body").empty().append(body);
    $("#modal-footer").empty().append(footer);
};