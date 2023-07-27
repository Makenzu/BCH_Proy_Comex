$(document).ready(function () {
    var baseUrl = $("#base_url").val();
    $(document).ajaxStart(function () {
		$.blockUI({ message: "<h6>Espere un momento ...</h6>" });
	}).ajaxStop($.unblockUI);

	var ignoreList = ["aceptar", "cancelar", "btn_ok_click", "btn_aceptar_click", "btn_cancelar_click",
        "Cb_Producto_blur", "Tx_NumOpe_000_blur", "Tx_NumOpe_001_blur", "Tx_NumOpe_002_blur", "Tx_NumOpe_003_blur", "Tx_NumOpe_004_blur", "Tx_NumOpe_005_blur", "Tx_NumOpe_006_blur"];

	var viewModel = null;
	var RelacionarOperacion = $("#RelacionarOperacion");

	var updateModel = function (newViewModel) {
	    ko.mapping.fromJS(newViewModel, viewModel);
	    var errors = ko.mapping.toJS(viewModel.Errores);
	    loadMessages(errors);

	    $("#caption").text("Asociación Operación " + viewModel.OPE());
	}
	var relacionarOperacionViewModel = function (data) {
		ko.mapping.fromJS(data, {}, this);
		var that = this;
		that.btn_ok_click = function () {
		    AjaxCall("FundTransfer/FrmgAso_ok_Click", function () {
		        if ($("#Tx_RutPrt").val().length > 0) {
		            $("#Boton_000").focus();
		        }
		    });
		};
		that.btn_aceptar_click = function () {
		    AjaxCall("FundTransfer/FrmgAso_Aceptar_Click", function () {
		        window.location.href = baseUrl + "FundTransfer/";
		    });
		};
		that.btn_cancelar_click = function () {
		    AjaxCall("FundTransfer/FrmgAso_Cancelar_Click", function () {
		        window.location.href = baseUrl + "FundTransfer/";
		    });
		};
		that.Cb_Producto_blur = function () {
		    AjaxCall("FundTransfer/FrmgAso_Cb_Producto_Click");
		};
     	that.Tx_NumOpe_000_blur = function () {
		    var val = $("#Tx_NumOpe_000").val();
		    $.ajax({
		        url: baseUrl + "FundTransfer/FrmgAso_Tx_NumOpe_LostFocus",
		        data: { id: 0, text: val },
		        method: "POST",
		        success: function (data) {
		            $("#Tx_NumOpe_000").val(data.Tx_NumOpe_000.Text);
		            //$("#Tx_NumOpe_002").focus().select();
		        }
		    });
		};
		that.Tx_NumOpe_001_blur = function () {
		    var val = $("#Tx_NumOpe_001").val();
		    $.ajax({
		        url: baseUrl + "FundTransfer/FrmgAso_Tx_NumOpe_LostFocus",
		        data: { id: 1, text: val },
		        method: "POST",
		        success: function (data) {
		            $("#Tx_NumOpe_001").val(data.Tx_NumOpe_001.Text);
		            //$("#Tx_NumOpe_002").focus().select();
		        }
		    });
		};
		that.Tx_NumOpe_002_blur = function () {
		    var val = $("#Tx_NumOpe_002").val();
		    $.ajax({
		        url: baseUrl + "FundTransfer/FrmgAso_Tx_NumOpe_LostFocus",
		        data: { id: 2, text: val },
		        method: "POST",
		        success: function (data) {
		            $("#Tx_NumOpe_002").val(data.Tx_NumOpe_002.Text);
		            //$("#Tx_NumOpe_003").focus().select();
		        }
		    });

		};
		that.Tx_NumOpe_003_blur = function () {
		    var val = $("#Tx_NumOpe_003").val();
		    $.ajax({
		        url: baseUrl + "FundTransfer/FrmgAso_Tx_NumOpe_LostFocus",
		        data: { id: 3, text: val },
		        method: "POST",
		        success: function (data) {
		            $("#Tx_NumOpe_003").val(data.Tx_NumOpe_003.Text);
		            //$("#Tx_NumOpe_004").focus().select();
		        }
		    });
		};
		that.Tx_NumOpe_004_blur = function () {
		    var val = $("#Tx_NumOpe_004").val();
		    $.ajax({
		        url: baseUrl + "FundTransfer/FrmgAso_Tx_NumOpe_LostFocus",
		        data: { id: 4, text: val },
		        method: "POST",
		        success: function (data) {
		            $("#Tx_NumOpe_004").val(data.Tx_NumOpe_004.Text);
		            //$("#Tx_NumOpe_005").focus().select();
		        }
		    });
		};
		that.Tx_NumOpe_005_blur = function () {
		    var val = $("#Tx_NumOpe_005").val();
		    $.ajax({
		        url: baseUrl + "FundTransfer/FrmgAso_Tx_NumOpe_LostFocus",
		        data: { id: 5, text: val },
		        method: "POST",
		        success: function (data) {
		            $("#Tx_NumOpe_005").val(data.Tx_NumOpe_005.Text);
		            //$("#Tx_NumOpe_006").focus().select();
		        }
		    });
		};
		that.Tx_NumOpe_006_blur = function () {
		    var val = $("#Tx_NumOpe_006").val();
		    $.ajax({
		        url: baseUrl + "FundTransfer/FrmgAso_Tx_NumOpe_LostFocus",
		        data: { id: 6, text: val },
		        method: "POST",
		        success: function (data) {
		            $("#Tx_NumOpe_006").val(data.Tx_NumOpe_006.Text);
		        }
		    });
		};
	}

	function AjaxCall(url, func) {
		viewModel.Errores([]);
		$.ajax({
			method: "POST",
			url: baseUrl + url,
			data: {
				jsonModel: ko.mapping.toJS(viewModel, {
					ignore: ignoreList
				})			    
			},
			success: function (a) {
			    updateModel(a);
			    if (func) {
			        func();
			      
                }
			},
			error: function (err) {
			    //console.error(err.responseText);
			}
		});
	}		
	$.ajax({
	    method: "GET",
        cache: false,
		url: baseUrl + "FundTransfer/FrmgAso_LoadFrm",
		success: function (data) {
			var nodo = RelacionarOperacion.get(0);
			ko.cleanNode(nodo);
			viewModel = new relacionarOperacionViewModel(data);
			ko.applyBindings(viewModel, nodo);
			$(":input").inputmask();
		}
	});

	$('[tabindex="1"]').focus();

    /// Cuando haga Enter en cualquier parte de la pantalla
	$(window).keydown(function (ev) {
	    var keycode = (ev.keyCode ? ev.keyCode : ev.which);
	    if (keycode == 9) {  // Presiona Tab
	        RecorrerTabIndex(-1, ev);
	    }

	    if (keycode == 13) {  // Presiona Enter
	        ev.preventDefault();
	        if ($("#Boton_000").is(":focus")) {
	            viewModel.btn_aceptar_click();
	        } else {
	            viewModel.btn_ok_click();
	        }
	    }
	    return true;
	});

});



