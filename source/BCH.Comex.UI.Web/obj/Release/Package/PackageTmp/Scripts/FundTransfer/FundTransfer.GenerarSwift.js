var planilla = null;
var viewModel = null;
var fechaHoyDefault = null;
var condicionesEspecialesPais57 = false;

function montoFormatter(value, row, index) {
    return numeral(value).format("0,0.00");
}

function montoCellStyle(value, row, index) {
    return {
        css: {
            "text-align": "right"
        }
    };
}

function generadoFormatter(value, row, index) {
    if (value == 0) {
        return "<span class='glyphicon glyphicon-remove' aria-hidden='true'></span>";
    }
    else {
        return "<span class='glyphicon glyphicon-ok' aria-hidden='true'></span>";
    }
}

function visualizarSwift(cuerpoSwift)
{
    $("#divCuerpoSwift").html(cuerpoSwift);
    $("#modalVisor").modal({ backdrop: true });
}

function changePlanillaSeleccionada(row)
{
    var selections = $(this).bootstrapTable('getSelections');
    if (selections.length == 1) {
        if (planilla != null && planilla.IndMT != viewModel.IndiceP()) { 
            limpiarTodosLosErrores();
        }
        
        planilla = selections[0];

        if (planilla != null) {
            viewModel.IndiceP(planilla.IndMT);

            var esAladi = planilla.EsAladi;
            cargarTipoBancos(esAladi, viewModel.BeneficiariosIniciales[viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.BenSwf.IndBen()].esBanco);
            SeleccionarTipoBanco(esAladi);
            $("#ddlPaisBeneficiario").change(); //para setear la carga de la plaza de pagos y por ende los corresponsales
            $("#chkBeneficiarioEsBanco").change(); //para que se dispare el onchange y se establezcan cosas como gastos 71a
            $("#ddlPaisPlazaDePago").change();
        }
    }
    else
    {
        planilla = null;
    }
}

function changeBeneficiario() {
    var selectedIndex = $(this)[0].selectedIndex;
    
    if (selectedIndex >= 0 && selectedIndex <= viewModel.BeneficiariosIniciales.length) {
        var benOriginal = viewModel.BeneficiariosIniciales[selectedIndex];
        var benSwift = viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.BenSwf;
        var datSwf = viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.DatSwf;
        
        //todo: asignar todo el objeto si es posible
        benSwift.NomBen(benOriginal.NomBen);
        benSwift.DirBen1(benOriginal.DirBen1);
        benSwift.DirBen2(benOriginal.DirBen2);
        benSwift.PaiBen(benOriginal.PaiBen);
        benSwift.EsBanco(benOriginal.EsBanco);
        benSwift.PaiBen59F(benOriginal.PaiBen59F);
        $("#chkBeneficiarioEsBanco").change(); //no deberia ser necesario, pero la linea anterior no me dispara el evento

        if ($("#ddlBeneficiario").val() == 0) {
            //Cliente.
            benSwift.PaiBen59F(benOriginal.PaiBen59F);
            benSwift.PaiBen(benOriginal.PaiBen);
            datSwf.PlzPag(benOriginal.PaiBen);
            $("#lstCorresponsales").empty();
        }
        else {
            //Beneficiario.
            benSwift.DirBen1("");
            benSwift.DirBen2("");
            benSwift.PaiBen59F("");
            datSwf.PlzPag("");
            $("#lstCorresponsales").empty();
        }
    }
}

function setDatosBeneficiario(nombre, dir1, dir2, codPais) {
    var ben = viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.BenSwf();
    ben.NomBen = nombre;
    ben.DirBen1 = dir1;
    ben.DirBen2 = dir2;
    ben.PaiBen = codPais;
}

function changeGastos71A() {
    viewModel.Planillas()[viewModel.IndiceP()].Montos.GasRec(0);
    viewModel.Planillas()[viewModel.IndiceP()].Montos.GasEmi(0);
    for (var i = viewModel.IndiceP() ; i < viewModel.Planillas().length; i++) {
        viewModel.Planillas()[i].DatosSwift.DatSwf.TipGas($("#ddlGastos71A").val());
    }    
}

function habilitarODeshabilitarElemento(elemento, habilitar) {
    if (habilitar) {
        elemento.removeAttr("disabled");
    }
    else {
        elemento.attr("disabled", "disabled");
        if (elemento.is("input:text")) {
            elemento.val("").change();
        }
        else if (elemento.is("input:checkbox")) {
            elemento.prop("checked", false).change();
        }
    }
}

function cargarTipoBancos(esAladi, beneficiarioEsBanco) {
    var ddlBanco = $("#ddlTipoBanco");
    ddlBanco.find("option").remove();

    var listaAConsiderar = null;
    if(beneficiarioEsBanco){
        listaAConsiderar = viewModel.TipoBancosSiBeneficiarioEsBanco;
    }
    else{
        listaAConsiderar = viewModel.TipoBancosSiBeneficiarioNoEsBanco;
    }

    $.each(listaAConsiderar, function (i, item) {
        ddlBanco
            .append($("<option></option>")
            .attr("value", item['Value'])
            .text(item['Text']));
    });
    

    mostrarUOcultarOpcionEnSelect(ddlBanco, valueBancoAladi, esAladi);
    
    var countItems = ddlBanco.find("option");
    if (countItems == 5 || !esAladi) {
        viewModel.tipoBanco(valueBancoPagador);
    }
    else {
        if (esAladi) {
            viewModel.tipoBanco(valueBancoAladi);
        }
    }
}

function SeleccionarTipoBanco(esAladi) {
    var valorSeleccionar = null;
    
    if (esAladi) {
        if (BancoCalificaParaSeleccionarTipoBanco(planilla['BcoAla'])) {
            valorSeleccionar = valueBancoAladi;
        }
    }
    
    if (valorSeleccionar == null) {
        if (BancoCalificaParaSeleccionarTipoBanco(planilla['BcoPag'])) {
            valorSeleccionar = valueBancoPagador;
        }
        else if (BancoCalificaParaSeleccionarTipoBanco(planilla['BcoInt'])) {
            valorSeleccionar = valueBancoIntermediario;
        }
    }

    if (valorSeleccionar != null) {
        viewModel.tipoBanco(valorSeleccionar);
    }
}

function BancoCalificaParaSeleccionarTipoBanco(banco) {
    if (banco != null) {
        return ($.trim(banco['SwfBco']) != null || $.trim(banco['NomBco']) != null || $.trim(banco['DirBco1']) != null || $.trim(banco['DirBco2']) != null || $.trim(banco['PaiBco']) != null);
    }
    else {
        return false;
    }
}

function changeCorresponsales() {
    var swiftCorresponsalSeleccionado = $(this).val();
    
    var encontrado = false;
    if (corresponsalesAplican != null) {
        if (swiftCorresponsalSeleccionado != null && swiftCorresponsalSeleccionado != "") {
            var corresponsal = $.grep(corresponsalesAplican, function (c) { return c.Cor_Swf === swiftCorresponsalSeleccionado; });
            if (corresponsal.length == 1) {
                encontrado = true;
                viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.DatSwf.NomBco(corresponsal.Cor_Nom);
                viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.DatSwf.CiuBco(corresponsal.Cor_Ciu);
                viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.DatSwf.PaiBco(corresponsal.Cor_Pai);
            } 
        }
    }

    if(!encontrado)
    {
        viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.DatSwf.NomBco("");
        viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.DatSwf.CiuBco("");
        viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.DatSwf.PaiBco("");
    }
}

var corresponsalesAplican = null;
function cargarCorresponsales() {
    if (planilla != null) {

        var ddlPlazaDePago = $("#ddlPaisPlazaDePago");
        var ddlCorresponsales = $("#lstCorresponsales");
        var ddlPaisBeneficiario = $("#ddlPaisBeneficiario");

        if (ddlCorresponsales.is(':enabled')) {

            var data = {
                idPlazaDePago: ddlPlazaDePago.val(),
                codMoneda: planilla.CodMon
            };

            if (data.idPlazaDePago != null) {
                return $.ajax({
                    type: "GET",
                    cache: false,
                    url: urlGetCorresponsales,
                    data: data,
                    error: function (response, type, message) {
                        corresponsalesAplican = null;
                        try {
                            //intento parsear la respesta como json.
                            var responseJson = JSON.parse(response.responseText);
                            showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
                        }
                        catch (err) {
                            showAlert("Error en la operación.", "Detalles: " + message, "alert-danger", true);
                        }
                    },
                    success: function (resultado) {
                        corresponsalesAplican = resultado;
                        ddlCorresponsales.find('option').remove();
                        $.each(resultado, function (i, item) {
                            ddlCorresponsales
                                .append($("<option></option>")
                                .attr("value", item['Cor_Swf'])
                                .text(item['Cor_Swf'] + "   " + item['Cor_Nom']));
                        });

                        if (viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.DatSwf.SwfCor() != undefined && viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.DatSwf.SwfCor()) {
                            //a pesar de que está el binding, esto es necesario porque Knockout no sabe que cambió la lista de opciones
                            ddlCorresponsales.val(viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.DatSwf.SwfCor());
                        }
                    }
                });
            }
        }
    }
}

function buscarBancoBeneficiarioPorSwift() {
    if ($("#chkBeneficiarioEsBanco").is(":checked")) {
        var txtSwift = $("#txtSwiftBancoBeneficiario");
        txtSwift.val(txtSwift.val().toUpperCase()).change();

        obtenerBancoPorSwift(txtSwift, false, function (r) {
            if (r != null && r != "") {
                var dirCompleta = r['BicDir'] + "," + r['BicCiu'] + "," + r['BicPos'];
                var benSwift = viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.BenSwf;
                
                //todo: asignar todo el objeto si es posible
                benSwift.NomBen(r.BicNom);
                benSwift.DirBen1(dirCompleta.substr(0, 35));
                benSwift.DirBen2((dirCompleta.length > 35 ? dirCompleta.substring(35) : "")); //el resto de los caracteres en la 2nda línea
                benSwift.PaiBen(r.CouCod);
                benSwift.EsBanco(true);
            }
            else {
                showAlert("Swift inexistente", "No existe el banco ingresado, por lo tanto debe ingresar los datos en forma manual.", "alert-info", true);
            }
        });
    }
}

function changeCodCompBanco() {

    if(toUppercase(this)){ //evaluo el resultado del toUppercase, dado que me dispara el change, si no lo evaluo entonces entro en loop infinito
        if ($(this).val() == ""){
            setValidezDeControl($(this), true, null);
        }
        else {
            $.ajax({
                type: "GET",
                cache:false,
                url: urlValidarCodCom,
                data: { codcom: $(this).val() },
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
                success: function (resultados) {
                    //alert(JSON.stringify(resultados));
                    var algunoEnError = false;
                    $.each(resultados, function (i, resultado) {
                        var esValido = !resultado['IsError'];
                        setValidezDeControl(
                            $("#" + resultado['ControlName']),
                            esValido,
                            resultado['Text']);

                        if (!esValido) {
                            algunoEnError = true;
                        }
                        return true; //para que el each siga iterando
                    });
                }
            });
        }

        
    }
}

function getBancoSeleccionado() {
    return viewModel.Planillas()[viewModel.IndiceP()].Bancos[viewModel.tipoBanco()];
}

function changeTxtSwiftBanco() {
    if (toUppercase(this)) { //evaluo el resultado del toUppercase, dado que me dispara el change, si no lo evaluo entonces entro en loop infinito

        var txtSwift = $(this);
        var banco = getBancoSeleccionado(); 
        
        var cantTipoBancos = $("#ddlTipoBanco > option").length;
       
        if (cantTipoBancos == 5) {
            if (viewModel.tipoBanco() == valueBancoIntermediario) {
                var bancoPagador = viewModel.Planillas()[viewModel.IndiceP()].Bancos[valueBancoPagador];
                if (txtSwift.val() == bancoPagador.SwfBco) {
                    setValidezDeControl(txtSwift, false, "Banco intermediario no puede ser el mismo banco como pagador.");
                    banco.SwfBco("");
                    return false;
                }
            }
            else if (viewModel.tipoBanco() == valueBancoPagador) {
                var bancoInt = viewModel.Planillas()[viewModel.IndiceP()].Bancos[valueBancoIntermediario];
                if (banco.SwfBco() == bancoInt.SwfBco) {
                    setValidezDeControl(txtSwift, false, "Banco pagador no puede ser el mismo banco como intermediario.");
                    banco.SwfBco("");
                    return false;
                }
            }
        }

        obtenerBancoPorSwift(txtSwift, true, function (r) {
            var txt = $("#txtSwiftBanco");
            
            if (r != null && r != "") {
                var dirCompleta = r.BicDir + "," + r.BicCiu + "," + r.BicPos;

                banco.NomBco(r.BicNom);
                banco.DirBco1(dirCompleta.substr(0, 35)); //los primeros 35 caracteres en una línea
                banco.DirBco2((dirCompleta.length > 35 ? dirCompleta.substring(35) : ""));  //el resto de los caracteres en la 2nda línea
                //banco.BicPai = r['BicPai']; //BicPai siempre esta vacío en la BD, en la legacy nunca se muestra, se intenta arreglar:
                banco.PaiBco($("#ddlPaisBeneficiario option[value='" + r.CouCod + "']").text().toUpperCase());
                banco.IngMan(0);

                setValidezDeControl(txt, true, "");
            }
            else {
                limpiarDatosBancoSeleccioando(false);
                setValidezDeControl(txt, false, "El banco ingresado no existe.");
                banco.IngMan(1);
                //showAlert("Banco inexistente", "No existe el banco ingresado, por lo tanto debe ingresar los datos en forma manual.", "alert-info", true);
            }
        });
    }
}

function limpiarDatosBancoSeleccioando(limpiarSwift) {
    var banco = getBancoSeleccionado();
    if(limpiarSwift) banco.SwfBco("");
    banco.NomBco("");
    banco.DirBco1("");
    banco.DirBco2("");
    banco.PaiBco("");
}

function obtenerBancoPorSwift(campo, mensajeErrorEnCampo, funcionSuccess) {
    var texto = campo.val();
    if (texto.length == 8)
    {
        texto = texto + "XXX";
        campo.val(texto).change();
    }
    if (viewModel.tipoBanco() == valueBancoPagador) {
        getCondicionesEspecialesPais57();
    }
    if (texto.length == 0) {
        limpiarDatosBancoSeleccioando(true);
    }
    else if (texto.length == 11) {
        return $.ajax({
            type: "GET",
            cache: false,
            url: urlGetBancoPorSwift,
            data: { swiftBanco: texto },
            error: function (response, type, message) {
                var responseJson = JSON.parse(response.responseText);
                showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
            },
            success: function(r){
                funcionSuccess(r);
            }
        });
    }
    else {
        var msgError = "El código ingresado debe tener 11 caracteres.";
        if (mensajeErrorEnCampo) {
            setValidezDeControl(campo, false, msgError);
        }
        else {
            showAlert("Formato incorrecto", msgError, "alert-danger", true);
        }
        limpiarDatosBancoSeleccioando(false);
    }
}

function chkBeneficiarioEsBancoChanged() {
    var esBanco = $("#chkBeneficiarioEsBanco").is(":checked");
    var txtSwiftBanco = $("#txtSwiftBancoBeneficiario");
    var btnBuscarPorSwift = $("#btnBuscarBancoPorSwift");

    cargarTipoBancos(planilla['EsAladi'], esBanco);
    mostrarUOcultarOpcionEnSelect($("#ddlGastos71A"), 3, !esBanco); //oculto o muestro opción SHA

    habilitarODeshabilitarElemento(txtSwiftBanco, esBanco);
    habilitarODeshabilitarElemento($("#chk59F"), !esBanco);
    viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.BenSwf.Es59F(!esBanco);
    
    $("#frmGroupCampo23E").toggle(!esBanco);
    $("#formGroupTablaCampo23").toggle(!esBanco);

    if (esBanco) {
        viewModel.Planillas()[viewModel.IndiceP()].Montos.Ch_Ori(false);
        $("#chkMontoOriginal").prop("checked", false).change();
    }
}

function changeChk59F() {
    var ddlNormal = $("#ddlPaisBeneficiario");
    var ddlF = $("#ddlPaisBeneficiario59F");

    //busco la opcion por nombre, si es el mismo nombre en ambas listas, lo selecciono.
    if ($(this).is(":checked")) {
        setOpcionPorTextoEnSelect(ddlNormal.find("option:selected").text(), ddlF, viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.BenSwf.PaiBen59F);
        ddlNormal.val("").change();
        $("#txtNombreBeneficiario").prop("maxlength", 66).change();
        $("#txtDir1Beneficiario").prop("maxlength", 66).change();
    }
    else {
        setOpcionPorTextoEnSelect(ddlF.find("option:selected").text(), ddlNormal, viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.BenSwf.PaiBen);
        ddlF.val("").change();
        $("#txtNombreBeneficiario").prop("maxlength", 33).val($("#txtNombreBeneficiario").val().substr(0,33)).change();
        $("#txtDir1Beneficiario").prop("maxlength", 33).val($("#txtDir1Beneficiario").val().substr(0, 33)).change();
    }
}

function setOpcionPorTextoEnSelect(textoOpcion, ddl, observableSelect)
{
    if (textoOpcion != "") {
        textoOpcion = textoOpcion.trim().toUpperCase();

        var options = ddl.find("option");

        var encontrado = false;
        $.each(options, function (i, item) {
            if (item.text.toUpperCase() === textoOpcion) {
                observableSelect(item.value);
                encontrado = true;
                return false; //para no seguir en el each
            }
            else {
                return true;
            }
        });

        if (!encontrado) {
            observableSelect(""); //esto no selecciona la opcion vacia del combo a noser de que tenga puesto allowUnset
            //ddl.val(""); //selecciono opcion vacia
        }
    }
}

function changeChk50F() {
    var ddlPaises = $("#ddlPaisesCliente");
    var txtPaisCliente = $("#txtClientePais");

    var es50F = $(this).is(":checked");
    if (es50F) {
        //si habia escrito un país y luego seleccionó 50F, busco en la lista de países uno con el nombre de lo que había escrito
        setOpcionPorTextoEnSelect(txtPaisCliente.val(), ddlPaises, viewModel.Cliente.PaiCliCod);
    }
    else
    {
        //si habia seleccionado un país en el combo y luego deselecciona el 50F, le pongo como texto escrito el nombre del país que estaba seleccionado
        txtPaisCliente.val(ddlPaises.find("option:selected").text().toUpperCase()).change();
    }
}

function changeChkMontoOriginal() {
    var esMontoOriginal = $(this).is(":checked");
    if (!esMontoOriginal) {
        //vuelvo a los valores originales que tenian los inputs, estos son los de la planilla seleccionada
        viewModel.Planillas()[viewModel.IndiceP()].Montos.MndOri(planilla['CodMon']);
        viewModel.Planillas()[viewModel.IndiceP()].Montos.MtoOri(planilla['mtoswf']);
        viewModel.Planillas()[viewModel.IndiceP()].Montos.TipCam(0);
    }
}

//funcion que "oculta" una opcion de un select. lo logra encapsulando la <option> en un <span>
function mostrarUOcultarOpcionEnSelect(select, valorOpcion, mostrar){
    var opcion = select.find("option[value='" + valorOpcion + "']");
    if (mostrar) {
        if (opcion.parent().is("span")) {
            opcion.unwrap();
        }
    }
    else {
        opcion.wrap('<span/>');
    }
}

function paisBeneficiarioChanged() {
    if ($(this).val() != null) {
        viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.BenSwf.PaiBen($(this).val());
    }
    if (!$("#chk59F").is(":checked") && viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.DatSwf.SwfCor() == null) {
        $("#ddlPaisPlazaDePago").val($(this).val()).change();
    }
}

function clickBtnToogleCampo23()
{
    var span = $("#btnToogleCampo23").find("span");
    span.toggleClass("glyphicon glyphicon-chevron-down");
    span.toggleClass("glyphicon glyphicon-chevron-up");
    $("#divAyudaCodigoDeOrden").hide();
}

function clickBtnAgregarCodigoDeOrden() {
    var ddlCodigosDeOrden = $("#ddlCodigosDeOrden");
    var txtAdicional = $("#txtTextoAdicionalCodigoOrden");

    var valorCodigo = ddlCodigosDeOrden.val();
    var descOpcion = ddlCodigosDeOrden.find("option:selected").text();
    var textoAdicional = txtAdicional.val();

    if (validarCodigoDeOrden(valorCodigo, descOpcion, textoAdicional)) {
        viewModel.Planillas()[viewModel.IndiceP()].CodigosDeOrdenCampo23.push({ Codigo: valorCodigo, CodigoDesc: descOpcion, TextoAdicional: textoAdicional, PermiteTextoAdicional: 0 });

        //reseteo la row de agregar
        txtAdicional.val("");
        $("#ddlCodigosDeOrden option:first").prop('selected', true);
    }
}

function validarCodigoDeOrden(valorCodigo, descCodigo, textoAdicional) {
    var esValido = true;
    var mensajeError = "";

    var countRows = viewModel.Planillas()[viewModel.IndiceP()].CodigosDeOrdenCampo23().length;
    //primero valido la cantidad maxima de elementos
    if (countRows < 5) { 
        //luego valido que se haya seleccionado una opción
        if (valorCodigo != null && valorCodigo != "") {
            var reglasDeItemAAgregar = viewModel.ReglasCodigosDeOrden[descCodigo];

            $.each(viewModel.Planillas()[viewModel.IndiceP()].CodigosDeOrdenCampo23(), function (i, item) {
                //valido que ya no exista ese código
                if (item.Codigo == valorCodigo) {
                    esValido = false;
                    mensajeError = "El Codigo de Orden seleccionado ya fue ingresado";
                    return false;
                }
                else {
                    //por último valido si las combinaciones son posibles
                    if (reglasDeItemAAgregar == null) {
                        return true; //no hay reglas para este item, estamos OK
                    }
                    else {
                        if ($.inArray(item.CodigoDesc, reglasDeItemAAgregar) != -1) {
                            esValido = false;
                            mensajeError = "El código " + item.CodigoDesc + " no se puede combinar con el código " + descCodigo;
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            });
        }
        else {
            esValido = false;
            mensajeError = "El Código de Orden es requerido";
        }
    }
    else {
        esValido = false;
        mensajeError = "No se pueden ingresar mas Codigos de Orden, se ha superado el maximo establecido";
    }

    setValidezDeControl($("#ddlCodigosDeOrden"), esValido, mensajeError);
    return esValido;
}

function setValidezDeControl(control, esValido, mensaje) {
    var formGroup = control.closest(".form-group, .form-group-sm");
    control.siblings(".lblMensajeError").remove();
    formGroup.toggleClass("has-error", !esValido);

    if (!esValido) {
        var htmlLabel = "<label class='lblMensajeError control-label'>" + mensaje + "</label>";
        control.after(htmlLabel);
    }
}

function limpiarTodosLosErrores()
{
    $(".lblMensajeError").remove();  //elimino cualquier label de error
    $("div.form-group.has-error, div.form-group-sm.has-error").removeClass("has-error");
    $("#msg-zone").html("");
}

function clickGenerarSwift() {

    if (!ValidarFechaPago($('#txtFechaPago')))
        return false;

    if (planilla != null && confirmarCondicionesEspecialesPais57()) {

        var data = { model: ko.mapping.toJS(viewModel, { ignore: getIgnoreListParaMapping() }) };
        for (i = 0; i < data.model.Planillas.length; i++)
        {
            data.model.Planillas[i].DatosSwift.mtoswf = numeral(data.model.Planillas[i].DatosSwift.mtoswf).format('0.00');
            data.model.Planillas[i].Montos.MtoOri = numeral(data.model.Planillas[i].Montos.MtoOri).format('0.00');
            data.model.Planillas[i].Montos.GasEmi = numeral(data.model.Planillas[i].Montos.GasEmi).format('0.00');
            data.model.Planillas[i].Montos.TipCam = numeral(data.model.Planillas[i].Montos.TipCam).format('0.00');
        }        

       $.ajax({
            type: "POST",
            url: urlValidarSwiftCompleto,
            data: data,
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
            success: function (resultado) {
                //alert(JSON.stringify(resultados));
                limpiarTodosLosErrores();

                var algunErrorNoAsociadoAUnControl = false;
                var algunoEnError = false;
                //recorro los errores

                viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.EstaGen(resultado.EstaGen);
                viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.DocSwf(resultado.DocSwift);
                var flagFocus = true;
                $.each(resultado.Mensajes, function (i, msg) {
                    var esValido = !msg['IsError'];
                    var controlError = msg['ControlName'];

                    if (controlError != null && controlError != "") {
                        setValidezDeControl($("#" + controlError), esValido, msg['Text']);                        
                        algunoEnError = true;
                        if (flagFocus) {
                            $("#" + controlError).focus();
                            flagFocus = false;
                        }
                    }
                    else {
                        algunErrorNoAsociadoAUnControl = true;
                        algunoEnError = true;
                    }

                    return true; //para que el each siga iterando
                });

                if (algunErrorNoAsociadoAUnControl) {
                    //llamo a la funcion del layout de mostrar mensajes UI
                    loadMessages(resultado.Mensajes);
                    focusOnErrorControl(resultado.Mensajes);
                }

                if (resultado.EstaGen && !algunoEnError) {
                    showAlert("Generación exitosa!", "El swift se generó correctamente", "alert-success", true);

                    $("#btnVisualizarSwift").removeAttr('disabled')
                    $("#btnAceptar").removeAttr("disabled");

                    var tablaPlanillas = $("#tablaPlanillas");
                    planilla.EstaGen = true;
                    tablaPlanillas.bootstrapTable('load', viewModel.DatosSwiftDeTodasLasPlanillas);
                    tablaPlanillas.bootstrapTable('uncheckAll');

                    //selecciono la 1er planilla no generada
                    var seleccioneAlguna = false;
                    $.each(viewModel.DatosSwiftDeTodasLasPlanillas, function (i, item) {
                        if (!item.EstaGen) {
                            tablaPlanillas.bootstrapTable('check', i);
                            window.scrollTo(0, 0);
                            seleccioneAlguna = true;
                            $("#btnAceptar").attr("disabled", "disabled");
                            $("[tabindex='1']").focus();
                            return false; //para no seguir recorriendo
                        }
                        else return true;
                    });

                    if (!seleccioneAlguna)
                    {
                        tablaPlanillas.bootstrapTable('check', 0);
                        $("#btnAceptar").focus();
                    }
                } else {
                    //Si existe error al generar o al imprimir, se deben bloquear los botones de visualizar y de acaptar
                    $("#btnVisualizarSwift").attr('disabled', 'disabled')
                    $("#btnAceptar").attr("disabled", "disabled");
                }

            } //fin success
        });
    }

}

function ArmarTablaBootstrap() {
    var tablaPlanillas = $("#tablaPlanillas");
    tablaPlanillas.bootstrapTable({
        classes: "table table-hover table-condensed table-no-bordered",
        data: viewModel.DatosSwiftDeTodasLasPlanillas,
        clickToSelect: true,
        singleSelect: true,
        sortable: false
    });

    tablaPlanillas.on("check.bs.table", changePlanillaSeleccionada);
    tablaPlanillas.bootstrapTable('check', 0);
    //cargarBeneficiarios();
    $("ddlBeneficiario").change();
}

function ValidarSwift() {
    $.ajax({
        type: "GET",
        cache: false,
        url: urlValidarSwift,
        data: { swiftBanco: texto },
        error: function (response, type, message) {
            var responseJson = JSON.parse(response.responseText);
            showAlert("Error en la operación.", "Detalles: " + responseJson.Message, "alert-danger", true);
        },
        success: function (r) {
            if (r != null) {
                var dirCompleta = r['BicDir'] + "," + r['BicCiu'] + "," + r['BicPos'];

                setControlesConDatosBeneficiario(
                    r['BicNom'],
                    dirCompleta.substr(0, 35), //los primeros 35 caracteres en una línea
                    (dirCompleta.length > 35 ? dirCompleta.substring(36) : ""), //el resto de los caracteres en la 2nda línea
                    r['CouCod']);
            }
            else {
                showAlert("Swift inexistente", "No existe el banco ingresado, por lo tanto debe ingresar los datos en forma manual.", "alert-info", true);
            }
        }
    });
}

var yaEstoyEnEventoChange = false;
function toUppercase(target) {
    if (!yaEstoyEnEventoChange) {
        yaEstoyEnEventoChange = true;
        $(target).val($(target).val().toUpperCase()).change();
        yaEstoyEnEventoChange = false;
        return true;
    }
    else return false;
}

function clickBtnCancelar() {
    var mensaje = "¿Confirma que desea abandonar la emisión del swift?";

    $.each(viewModel.DatosSwiftDeTodasLasPlanillas, function (i, item) {
        if (item.EstaGen) {
            mensaje = "Advertencia : Existe al menos un documento generado, al cancelar perderá estos cambios.\n" + mensaje;
            return false; //para no seguir recorriendo
        }
        else return true;
    });


    if (confirm(mensaje)) {
        window.location = urlHome;
    }
}

//retorno elementos de mi modelo que no cambian, por lo tanto a la ida no me interesa convertirlos en Observables, y a la vuelta no me interesa enviarlos
function getIgnoreListParaMapping() {
    return   ["DatosSwiftDeTodasLasPlanillas",
                "CodigosDeOrdenPosiblesCampo23",
                "ReglasCodigosDeOrden",
                "TipoBancosSiBeneficiarioEsBanco",
                "TipoBancosSiBeneficiarioNoEsBanco",
                "BeneficiariosIniciales",
                "Monedas",
                "PaisesTPai",
                "PaisesCPai",
                "EsCargaAutomatica"];
}

$(function () {

    $(document).ajaxStart(function () {
        $.blockUI({ message: "<h6>Espere un momento ...</h6>" });
    }).ajaxStop($.unblockUI);
    var tabindex = 15;
    //inicializo knockout
    var inicializarViewModel = function () {
        var mapping = {
            copy: getIgnoreListParaMapping()
        };

        
        var vm = ko.mapping.fromJS(modelCompleto, mapping, this);
        vm.tipoBanco = ko.observable(); //inicializo en "Banco pagador"
        vm.indiceCodigo23 = ko.observable();
        vm.corresponales = ko.observableArray();
        vm.tabindex = ko.observable();
                
        vm.planillasIncompletas = ko.computed(function () {
            return ko.utils.arrayFilter(vm.Planillas(), function (planilla) { return !planilla.DatosSwift.EstaGen() });
        });

        vm.removerCodigo23 = function (codigo23) { vm.Planillas()[vm.IndiceP()].CodigosDeOrdenCampo23.remove(codigo23) };

        vm.lineaManualEsVisible = function (linea) { 
            return ((linea.CodMT() == 202 && vm.Planillas()[vm.IndiceP()].DatosSwift.BenSwf.EsBanco()) || (linea.CodMT() == 103 && !vm.Planillas()[vm.IndiceP()].DatosSwift.BenSwf.EsBanco()) || (linea.CodMT() == 100 && !vm.Planillas()[vm.IndiceP()].DatosSwift.BenSwf.EsBanco()))
        };
        vm.tabindex = function () {
            return tabindex++;
        };


        //extender para poder indicar nro de decimales
        ko.extenders.numeral = function (target, format) {
            var result = ko.computed({
                read: function () {
                    return numeral(target()).format(format);
                },
                write: target
            });

            result.raw = target;
            return result;
        };

        ko.extenders.uppercase = function (target, option) {
            target.subscribe(function (newValue) {
                if (newValue != null) {
                    target(newValue.toUpperCase());
                }
                /*else {
                    target(null);
                }*/
            });
            return target;
        };

        ko.extenders.maxLength = function (target, maxLength) {
            //create a writeable computed observable to intercept writes to our observable
            var result = ko.computed({
                read: target,  //always return the original observables value
                write: function (newValue) {
                    var current = target(),
                        valueToWrite = newValue ? newValue.substring(0, Math.min(newValue.length, maxLength)) : null;

                    //only write if it changed
                    if (valueToWrite !== current) {
                        target(valueToWrite);
                    } else {
                        //if the rounded value is the same, but a different value was written, force a notification for the current field
                        if (newValue !== current) {
                            target.notifySubscribers(valueToWrite);
                        }
                    }
                }
            });

            //initialize with current value to make sure it is rounded appropriately
            result(target());

            //return the new computed observable
            return result;
        };
    };

    viewModel = new inicializarViewModel(); // ko.mapping.fromJS(modelCompleto, mapping, this);

    var nodo = $("#divContainer").get(0);
    ko.cleanNode(nodo);
    ko.applyBindings(viewModel, nodo);
    

    //tooltips
    $('[data-toggle="tooltip"]').tooltip();

    $("#ddlPaisBeneficiario").change(paisBeneficiarioChanged);
    $("#ddlPaisPlazaDePago").change(function () {
        cargarCorresponsales();
    });
    $("#chkBeneficiarioEsBanco").change(chkBeneficiarioEsBancoChanged);
    $("#ddlBeneficiario").change(changeBeneficiario);
    $("#lstCorresponsales").change(changeCorresponsales);
    $("#ddlGastos71A").change(changeGastos71A);
    $("#chk59F").change(changeChk59F);
    $("#chk50F").change(changeChk50F);
    if(viewModel.Cliente.Es50F()){
        $("#chk50F").change(); //simulo en change
    }
    $("#chkMontoOriginal").change(changeChkMontoOriginal);

    $("#ddlPaisBeneficiario").change(function ()
    {
        viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.BenSwf.PaiBen_t = $(this).find("option:selected").text();
    });

    $("#btnBuscarBancoPorSwift").click(buscarBancoBeneficiarioPorSwift);
    $("#txtSwiftBancoBeneficiario").focusout(buscarBancoBeneficiarioPorSwift);
    $("#btnToogleCampo23").click(clickBtnToogleCampo23);
    $("#btnAgregarCodigoDeOrden").click(clickBtnAgregarCodigoDeOrden);

    $("#headTablaCodigosDeOrden").focusin(function () {
        $("#divAyudaCodigoDeOrden").show();
    });
    $("#headTablaCodigosDeOrden").focusout(function () {
        $("#divAyudaCodigoDeOrden").hide();
    });
          
    $("#btnGenerarSwift").click(clickGenerarSwift);
    $("#btnCancelar").click(clickBtnCancelar);

    $("#btnVisualizarSwift").click(function () {
        visualizarSwift(viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.DocSwf());
    });

    //datos beneficiario
    $("#txtNombreBeneficiario").change(function () { toUppercase(this) }).change();
    $("#txtDir1Beneficiario").change(function () { toUppercase(this) }).change();
    $("#txtDir2Beneficiario").change(function () { toUppercase(this) }).change();
    $("#txtCuentaBeneficiario").change(function () { toUppercase(this) }).change();
    $("#txtReferBeneficiario").change(function () { toUppercase(this) }).change();

    $("#txtNombreBeneficiario").on('input', (function () { testCharacterSetX(this) }));
    $("#txtDir1Beneficiario").on('input', (function () { testCharacterSetX(this) }));
    $("#txtDir2Beneficiario").on('input', (function () { testCharacterSetX(this) }));
    $("#txtCuentaBeneficiario").on('input', (function () { testCharacterSetX(this) }));
    $("#txtReferBeneficiario").on('input', (function () { testCharacterSetX(this) }));
    
    //datos bancos
    $("#txtCodCompBanco").change(changeCodCompBanco);
    $("#txtSwiftBanco").change(changeTxtSwiftBanco);
    $("#txtNombreBanco").change(function () { toUppercase(this) }).change();
    $("#txtDir1Banco").change(function () { toUppercase(this) }).change();
    $("#txtDir2Banco").change(function () { toUppercase(this) }).change();
    $("#txtPaisBanco").change(changePaisBanco).change();
    $("#ddlTipoBanco").change(function() {
        setValidezDeControl($("#txtCodCompBanco"), true, ""); //reseteo los mensajes de error
        setValidezDeControl($("#txtSwiftBanco"), true, ""); //reseteo los mensajes de error
    });

    //datos cliente
    $("#txtClienteNombre").change(function () { toUppercase(this) }).change();
    $("#txtClienteDir1").change(function () { toUppercase(this) }).change();
    $("#txtClienteDir2").change(function () { toUppercase(this) }).change();
    $("#txtClientePais").change(function () { toUppercase(this) }).change();

    $("#divCuerpoCamposManuales input").change(function () { toUppercase(this); }).change();

    $("#divCuerpoCamposManuales input[type=text]").on('input', (function () { testCharacterSetX(this) }));

    $(":input").inputmask(); //aplico el inputmask para todos los inputs (en esta pagina en realidad es solo el de fecha de pago)
    ArmarTablaBootstrap();

    $("#ddlGastos71A").focus();

    var fechaHoy = new Date();

    $('.date').datetimepicker({
        format: 'DD/MM/YYYY',
        locale: 'es',
        daysOfWeekDisabled: [0, 6],
        showTodayButton: false,
        showClose: false,
        focusOnShow: false,
        minDate: moment(fechaHoy).hours(0).minutes(0).seconds(0).milliseconds(0),
        debug: true
    });

    fechaHoyDefault = viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.DatSwf.FecPag().split("-");
    if (fechaHoyDefault.length < 2) {
        fechaHoyDefault = viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.DatSwf.FecPag().split("/");
    } else {
        fechaHoyDefault = fechaHoyDefault[0] + "/" + fechaHoyDefault[1] + "/" + fechaHoyDefault[2];
    }
    $("#txtFechaPago").val(fechaHoyDefault);

    $("#txtFechaPago")
        .focusout(function () {
            ValidarFechaPago($(this));
        })
        .blur(function () {
            ValidarFechaPago($(this));
    });

    $("#modalVisor").on("hidden.bs.modal", function () { $("[tabindex='17']").focus(); })

    var EnterGenerar = false;

    $(window).keydown(function (ev) {
        var keycode = (ev.keyCode ? ev.keyCode : ev.which);
        if (keycode == 9) {  // Presiona Tab
            RecorrerTabIndex(-1, ev);
        }
        else if (keycode == '13') { // Enter
            if($("#btnVisualizarSwift").is(':focus')){
                $('#btnVisualizarSwift').click();
                return;
            } else if (!ValidarFechaPago($('#txtFechaPago'))) {
                EnterGenerar = false;
                return;
            } else if ($("#btnAceptar").is(':enabled') && EnterGenerar) {
                if (ValidarFechaPago($('#txtFechaPago'))) {
                    $("#btnGenerarSwift").click();
                    $('#btnAceptar').click();
                }
            } else {
                $("#btnGenerarSwift").click();
                EnterGenerar = true;
            }
        }
    });

    $('#txtFechaPago').keydown(function (ev) {
        var keycode = (ev.keyCode ? ev.keyCode : ev.which);
        if (keycode == '13') { // Enter
            if ($("#btnVisualizarSwift").is(':focus')) {
                $('#btnVisualizarSwift').click();
                return;
            } else if (!ValidarFechaPago($('#txtFechaPago'))) {
                EnterGenerar = false;
                return;
            } else if ($("#btnAceptar").is(':enabled') && EnterGenerar) {
                $("#btnGenerarSwift").click();
                $('#btnAceptar').click();
            } else {
                $("#btnGenerarSwift").click();
                EnterGenerar = true;
            }
        }
    });

    // Mejora Foco en campo de Error
    focusOnErrorControl(modelCompleto.MensajesDeError);
});

function ValidarFechaPago($fecha) {
    if (validarFormatoFecha($fecha.val())) {
        if (!existeFecha($fecha.val())) {
            showAlert("Error de formato de fecha", "La fecha introducida no existe.", "alert-danger", true);
            return false;
        }
    }
    viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.DatSwf.FecPag($fecha.val());
    return true;
}

function existeFecha(fecha) {
    var fechaf = fecha.split("/");
    var day = fechaf[0];
    var month = fechaf[1];
    var year = fechaf[2];
    var date = new Date(year, month, '0');
    if ((day - 0) > (date.getDate() - 0)) {
        return false;
    }
    return true;
}

function validarFormatoFecha(campo) {
    var fecModelFinalday = fechaHoyDefault[0];
    var fecModelFinalmonth = fechaHoyDefault[1];
    var fecModelFinalyear = fechaHoyDefault[2];
    var RegExPattern = /^\d{1,2}\/\d{1,2}\/\d{2,4}$/;
    if ((campo.match(RegExPattern)) && (campo != '')) {
        return true;
    } else {
        var fechaFinal;
        var fechaf = campo.split("/");
        var day = fechaf[0];
        var month = fechaf[1];
        var year = fechaf[2];
        RegExPattern = /^\d{1,2}$/;
        // validar día
        if (day.match(RegExPattern))
            fechaFinal = day;
        else
            fechaFinal = fecModelFinalday
        // validar mes
        if (month.match(RegExPattern))
            fechaFinal = fechaFinal + "/" + month;
        else
            fechaFinal = fechaFinal + "/" + fecModelFinalmonth
        // validar anio
        RegExPattern = /^\d{2,4}$/;
        if (year.match(RegExPattern))
            fechaFinal = fechaFinal + "/" + year;
        else
            fechaFinal = fechaFinal + "/" + fecModelFinalyear

        $("#txtFechaPago").val(fechaFinal);
        return true;
    }
}

function changePaisBanco() {
    if (toUppercase($("#txtPaisBanco"))) { //evaluo el resultado del toUppercase, dado que me dispara el change, si no lo evaluo entonces entro en loop infinito
        if (viewModel.tipoBanco() == valueBancoPagador && $("#txtPaisBanco").val() != "") {
            getCondicionesEspecialesPais57();
        }
    }
}

function getCondicionesEspecialesPais57(showAlertBool = true) {
    var banco = viewModel.Planillas()[viewModel.IndiceP()].Bancos[valueBancoPagador];

    $.ajax({
        type: "get",
        cache: false,
        url: urlGetCondicionesPais,
        data: { swiftBanco: banco.SwfBco(), moneda: viewModel.Planillas()[viewModel.IndiceP()].Montos.MndOri() },
        error: function (response, type, message) {
            try {
                //intento parsear la respesta como json.
                var responsejson = json.parse(response.responsetext);
                showalert("error en la operación.", "detalles: " + responsejson.message, "alert-danger", true);
            }
            catch (err) {
                showalert("error en la operación.", "detalles: " + message, "alert-danger", true);
            }
        },
        success: function (resultado) {
            var opciones = resultado['codigos'];
            cargarMt103Campo72(opciones);
            if (resultado['condicionesEspeciales']) {
                condicionesEspecialesPais57 = true;
                if (showAlertBool && !viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.BenSwf.EsBanco()) {
                    showAlert("Condiciones especiales", "El pais del banco pagador establece condiciones especiales para recibir fondos del exterior. Debe ingresar información complementaria para el envío del pago en el campo 72.", "alert-warning", false);
                }
            }
            else {
                condicionesEspecialesPais57 = false;
                //setValidezDeControl($("#txtPaisBanco"), true, '');
            }
        }
    });
}

function getMt103Campo72() {
    var lineasManuales = viewModel.Planillas()[viewModel.IndiceP()].LineasManuales();
    var result = null;
    for (var linea of lineasManuales) {
        if (linea.CodMT() == 103 && linea.CodCam().trim() == '72') {
            result = linea;
            break;
        }
    };
    return result;
}

function cargarMt103Campo72(opciones) {
    var opcionesAct = getMt103Campo72().LineasSecundarias()[0].ValorCampo();
    var opcionesObs = convertToObservable(opciones);

    //En caso de que la opción ya seleccionada exista en la nueva lista, se deja seleccionada
    if (opcionesObs.length > 0) {
        var selectedOp = '';
        for (var op of opcionesAct) {
            if (op.Selected()) {
                selectedOp = op.Value();
                break;
            }
        }
        if (selectedOp != '') {
            for (var op of opcionesObs) {
                if (op.Value() == selectedOp) {
                    op.Selected(true);
                }
            }
        }
    }
    getMt103Campo72().LineasSecundarias()[0].ValorCampo(opcionesObs);

    if (opcionesObs.length == 0 && opcionesAct.length > 0) {
        getMt103Campo72().Detalle("")
        $("#txtLineaManualMT103Cod72Principal").on('input', (function () { testCharacterSetX(this) }));
    }
}

function convertToObservable(list) {
    var newList = [];
    $.each(list, function (i, obj) {
        var newObj = {};
        Object.keys(obj).forEach(function (key) {
            newObj[key] = ko.observable(obj[key]);
        });
        newList.push(newObj);
    });
    return newList;
}

function confirmarCondicionesEspecialesPais57() {
    if (!viewModel.Planillas()[viewModel.IndiceP()].DatosSwift.BenSwf.EsBanco() && condicionesEspecialesPais57 && getMt103Campo72().Incluido() == false) {
        if (confirm("Advertencia: El pais del banco pagador establece condiciones especiales para recibir fondos del exterior. Debe ingresar información complementaria para el envío del pago en el campo 72.\n¿Confirma que desea generar el swift?")) {
            return true;
        }
        else {
            return false;
        }
    } else {
        return true;
    }
}

function testCharacterSetX(element) {
    var c = element.selectionStart;
    var r = /[^A-Za-z0-9/\-?:().,'+\n\r ]/g;
    var v = $(element).val();
    if (r.test(v)) {
        $(element).val(v.replace(r, ''));
        c--;
    }
    element.setSelectionRange(c, c);
}