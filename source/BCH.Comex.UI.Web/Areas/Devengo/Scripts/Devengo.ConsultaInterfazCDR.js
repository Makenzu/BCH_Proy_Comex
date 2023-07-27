$(function () {

    if (modelCompleto.ListaErrores != null && modelCompleto.ListaErrores.Length != 0) {
        focusOnErrorControl(modelCompleto.ListaErrores);
    }

    $("#btnGenerarExcel").off("Click");
    $("#btnGenerarExcel").click(function () {
        var periodo = $("#listaPeriodos_SelectedValue").val();
        var dia = $("#listaDias_SelectedValue").val();
        var txtRut = $("#txtRut").val();
        EmptyMessageZone();
        var param = { listaPeriodos: periodo, listaDias: dia, rut: txtRut, Command: "Descarga" };
        DescargarArchivo(urlDescargarConsultaInterfaz, 'Descarga Interfaz CDR', urlDescarga, param);
        return false;
    });
    $("#txtRut").inputmask('999.999.999-9|K|k');
    $("#txtRut").on('change', function () {
        var Objeto = this;
        var tmpstr = "";
        var intlargo = this.value
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
                showAlert("", "El RUT ingresado no es válido", "alert-danger", true);
                Objeto.focus()
                return false;
            }
            return true;
        }
    });
    if (tipoConsulta == 'True') {
        $("#listaPeriodos_SelectedValue").off('change');

        $("#listaPeriodos_SelectedValue").on('change', function () {
            var per = this.value;
            $.ajax({
                type: "POST",
                url: urlobtenerDiasDisponibles,
                data: { periodo: per },
                cache: false,
                error: function (response, type, message) {
                    try {
                        //intento parsear la respesta como json.
                        var responseJson = JSON.parse(response.responseText);
                        showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
                    }
                    catch (err) {
                        showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", true);
                    }
                },
                success: function (data) {
                    if (data.listaMensajes.length > 0) {
                        //llamo a la funcion del layout de mostrar mensajes UI
                        loadMessages(data.ListaProblemas);
                        focusOnErrorControl(data.ListaProblemas);
                    } else {
                        var listaAConsiderar = data.listaDias;

                        var ddlDias = $("#listaDias_SelectedValue");
                        ddlDias.find("option").remove();

                        ddlDias
                                .append($("<option></option>")
                                .attr("value", 0)
                                .text("Mensual"));

                        $.each(listaAConsiderar, function (i, item) {
                            ddlDias
                                .append($("<option></option>")
                                .attr("value", item)
                                .text(pad(item,2))); 
                        });

                        $.unblockUI();
                    }
                }
            });
        });
    }
});

function Valida_Rut() {
    
}


function pad(str, max) {
    str = str.toString();
    return str.length < max ? pad("0" + str, max) : str;
}