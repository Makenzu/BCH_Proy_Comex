$(function () {
    var baseUrl = $("#base_url").val(); //obtengo la url base global

	iniciarFormulario();
	$("#Rut").focus().select();
	$('#CasillaBanco').change(function (data) {
	    var envio = $('#ddlEnvio option[value=2]'); //opcion casilla banco

	    if (data.target.value !== '') {
	        envio.removeAttr('disabled');
	    } else {
	        envio.attr('disabled', 'disabled');
	        envio.removeAttr('selected');
	    }
	});

	$('#CasillaPostal').change(function (data) {
	    var envio = $('#ddlEnvio option[value=3]'); //opcion casilla postal

	    if (data.target.value !== '') {
	        envio.removeAttr('disabled');
	    } else {
	        envio.attr('disabled', 'disabled');
	        envio.removeAttr('selected');
	    }
	});

	$('#Fax').change(function (event) {
	    var envio = $('#ddlEnvio option[value=1]'); //opcion casilla postal

	    if (event.target.value !== '') {
	        envio.removeAttr('disabled');
	    } else {
	        envio.attr('disabled', 'disabled');
	        envio.removeAttr('selected');
	    }
	});

	$('#Ciudad').change(function (data) {
	    if (data.target.value !== '') {
	        $('#Pais').removeAttr('disabled');
	    }
	});

	$('#Pais').change(function (event) {
	    var valor = $(event.target).val();

	    if (valor === '999') //otros
	        $('#OtroPais').removeClass('hidden').val('');
	    else
	        $('#OtroPais').addClass('hidden').val('');

	    var nuestroPais = $('#NuestroPais').val();
	    if (valor === nuestroPais) {
	        $('#Telefono').inputmask('(999) 999-9999');
	        $('#Fax').inputmask('(999) 999-9999');
	    } else {
	        $('#Telefono').inputmask('remove');
	        $('#Fax').inputmask('');
	    }
	});

	$('#EsBanco').change(function (event) {
	    setearMascaraRut();
	});

	$('#btnGenerar').click(function (event) {

	    $.ajax({
	        url: baseUrl + "FundTransfer/ParticipantesCrear_Consultar",
	        method: "POST",
	        //data: { codParticipante: $('#KeyText').val() },
	        success: function (data) {
	            ParticipantesCrearViewModelToView(data);
	        },
	        error: function (data) {
	            alert('Ha ocurrido un error');
	        }
	    });
	});

});

function iniciarFormulario() {

    if ($('#CargaAutomatica').val() == 'True') {

		$('#btnAceptar').removeAttr('disabled');
		$('#btnGenerar').attr('visible', 'false');

		//deshabilito los controles
		$('#frmParticipantesCrear .form-control').attr('disabled', 'disabled');
		$('#frmParticipantesCrear .checkbox').attr('disabled', 'disabled');
    }

    setearMascaraRut();
}

function setearMascaraRut()
{
    var txtRut = $('#Rut');
    var chkBanco = $('#EsBanco');

    if (chkBanco.prop('checked')) {
        txtRut.inputmask('************');
    } else {
        txtRut.inputmask('999.999.999-9|K|k');
    }
}

function ParticipantesCrearViewModelToView(data) {
    //cargo los mensajes de error
    loadMessages(data.MensajesDeError);

    $('#Rut').val(data.Rut);
    $('#RazonSocial').val(data.RazonSocial);
    $('#Direccion').val(data.Direccion);
    $('#CodPostal').val(data.CodPostal);
    $('#Localidad').val(data.Localidad);
    $('#Region').val(data.Region);
    $('#Ciudad').val(data.Ciudad);
    $('#Fax').val(data.Fax);
    $('#Pais').val(data.Pais);
    $('#OtroPais').val(data.OtroPais);
    $('#CasillaBanco').val(data.CasillaBanco);
    $('#CasillaPostal').val(data.CasillaPostal);
    $('#Telefono').val(data.Telefono);
    $('#Telex').val(data.Telex);
    $('#EnvioCorrespondencia').val(data.EnvioCorrespondencia);
}

function Valida_Rut(Objeto) {
	var tmpstr = "";
	var intlargo = Objeto.value
	if (intlargo.length > 0) {
		crut = Objeto.value
		largo = crut.length;
		if (largo < 2) {
			alert('rut inválido')
			Objeto.focus()
			return false;
		}
		for (i = 0; i < crut.length ; i++)
			if (crut.charAt(i) != ' ' && crut.charAt(i) != '.' && crut.charAt(i) != '-') {
				tmpstr = tmpstr + crut.charAt(i);
			}
		rut = tmpstr;
		crut = tmpstr;
		largo = crut.length;

		if (largo > 2)
			rut = crut.substring(0, largo - 1);
		else
			rut = crut.charAt(0);

		dv = crut.charAt(largo - 1);

		if (rut == null || dv == null)
			return 0;

		var dvr = '0';
		suma = 0;
		mul = 2;

		for (i = rut.length - 1 ; i >= 0; i--) {
			suma = suma + rut.charAt(i) * mul;
			if (mul == 7)
				mul = 2;
			else
				mul++;
		}

		res = suma % 11;
		if (res == 1)
			dvr = 'k';
		else if (res == 0)
			dvr = '0';
		else {
			dvi = 11 - res;
			dvr = dvi + "";
		}

		if (dvr != dv.toLowerCase()) {
			alert('El Rut Ingreso es Invalido')
			Objeto.focus()
			return false;
		}
		alert('El Rut Ingresado es Correcto!')
		Objeto.focus()
		return true;
	}
}
