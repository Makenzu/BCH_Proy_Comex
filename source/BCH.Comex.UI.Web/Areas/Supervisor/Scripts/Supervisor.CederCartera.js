$(document).ready(function () {
    var baseUrl = $("#base_url").val();

    $(document).ajaxStart(function () { $.blockUI({ message: '<h6>Cargando...</h6>' }) })
        .ajaxStop($.unblockUI).ajaxError($.unblockUI);

    var ignoreList = ["btn_AceptarHandler", "btn_CancelarHandler"];
    var viewModel = null;

    var updateModel = function (newViewModel) {
        ko.mapping.fromJS(newViewModel, {}, viewModel);
        var errors = ko.mapping.toJS(newViewModel.ListaErrores);
        loadMessages(errors);
    }

    var CederCarteraViewModel = function (data) {
        ko.mapping.fromJS(data, {}, this);
        var that = this

        this.productItems = ko.observableArray(data.Productos); // Initial items
        this.userItems = ko.observableArray(data.UsuariosActuales);
        this.newUserItems = ko.observableArray(data.UsuariosNuevos);
        this.clientItems = ko.observableArray(data.Clientes);
        this.operationItems = ko.observableArray(data.Operaciones);

        that.btn_AddClientesHandler = function () {
            var usuario = $('#userItems option:selected').text();
            var items = [];
            $('#productItems option:selected').each(function () { items.push($(this).val()); });
            var producto = items.join(';');

            if (usuario != '' && producto != '') {
                $.ajax({
                    method: "POST",
                    cache: false,
                    url: baseUrl + "Supervisor/CederCartera/ObtenerClientes",
                    data: {
                        usuarioActual: usuario,
                        producto: producto
                    },
                    success: function (result) {
                        function MapClients(clients) {
                            var len = clients.length;
                            function MapClient() {
                                if (len == 0) {
                                    return;
                                }
                                setTimeout(function () {
                                    viewModel.clientItems.push(clients.pop());
                                    len--;

                                    MapClient();
                                }, 0);
                            }
                            viewModel.clientItems.removeAll();
                            MapClient();
                        }
                        /************************************************************ */
                        var errors = ko.mapping.toJS(result.ListaErrores);
                        loadMessages(errors);
                        //ko.mapping.fromJS(result.Clientes, {}, viewModel.clientItems);
                        /* ESTO ES LO QUE SE CAMBIO */

                        MapClients(result.Clientes);

                        /* ********************** */
                        ko.mapping.fromJS(result.Operaciones, {}, viewModel.operationItems);
                    },
                    error: function (response, type, message) {
                        try {
                            //intento parsear la respesta como json.
                            var responseJson = JSON.parse(response.responseText);
                            showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
                        }
                        catch (err) {
                            showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", true);
                        }
                    }
                });
            } else {
                showAlert("Error en la operación.", "Detalles: Debe seleccionar un producto", "alert-danger", true);
            }


        };

        that.btn_AddOperacionesHandler = function () {

            var usuario = $('#userItems option:selected').text();
            var items = [];
            $('#productItems option:selected').each(function () { items.push($(this).val()); });
            var producto = items.join(';');
            var cliente = $('#clientItems option:selected').val();


            if (usuario != '' && producto != '' && cliente != '') {

                $.ajax({
                    method: "POST",
                    cache: false,
                    url: baseUrl + "Supervisor/CederCartera/ObtenerOperaciones",
                    data: {
                        usuarioActual: usuario,
                        producto: producto,
                        cliente: cliente
                    },
                    success: function (result) {
                        var errors = ko.mapping.toJS(result.ListaErrores);
                        loadMessages(errors);
                        ko.mapping.fromJS(result.Operaciones, {}, viewModel.operationItems);
                    },
                    error: function (response, type, message) {
                        try {
                            //intento parsear la respesta como json.
                            var responseJson = JSON.parse(response.responseText);
                            showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
                        }
                        catch (err) {
                            showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", true);
                        }
                    }
                });
            } else {
                showAlert("Error en la operación.", "Detalles: Debe seleccionar un cliente", "alert-danger", true);
            }
        };

        that.btn_AceptarHandler = function () {
            var usuario = $('#userItems option:selected').text();
            var usuarioNuevo = $('#newUserItems option:selected').text();
            var items = [];
            $('#productItems option:selected').each(function () { items.push($(this).val()); });
            var producto = items.join(';');
            var cliente = $('#clientItems option:selected').val();

            if (usuario != '' && usuarioNuevo != '' && producto != '' && cliente != '') {
                $.ajax({
                    method: "POST",
                    cache: false,
                    url: baseUrl + "Supervisor/CederCartera/ProcessCederCartera",
                    data: {
                        usuarioActual: usuario,
                        usuarioNuevo: usuarioNuevo,
                        producto: producto,
                        clienteID: cliente
                    },
                    success: function (result) {
                        var errors = ko.mapping.toJS(result.ListaErrores);
                        loadMessages(errors);
                        viewModel = null;
                        window.location.href = baseUrl + "Supervisor/";
                    }
                });
            } else {
                showAlert("Error en la operación.", "Detalles: Debe seleccionar un Cliente para poder Ceder la Cartera de este a otro Especialista. ", "alert-danger", true);
            }

        };


        that.btn_CancelarHandler = function () {
            window.location.href = baseUrl + "Supervisor/";
        };
    }

    $.ajax({
        method: "GET",
        cache: false,
        url: baseUrl + "Supervisor/CederCartera/CederCarteraInit",
        success: function (data) {
            viewModel = new CederCarteraViewModel(data);
            ko.applyBindings(viewModel);
        }
    });

    //$.unblockUI();
});





