$(function () {
    var baseUrl = $("#base_url").val(); //obtengo la url base global
    $('#FechaPicker').datetimepicker({
        locale: 'es',
        format: 'DD/MM/YYYY'
    });
    
    //Index 2 --> Tipo Anulacion
    $("#Tx_TipoAnulacion_Text").blur(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaAnulacion_TipoAnulacion_Blur",
            method: "POST",
            data: { selectedValue: $(this).val() },
            success: function (data) {
                if (data != null) {
                    $("#Tx_TipoAnulacion_Text").val(data.Tx_TipoAnulacion.Text);

                    loadMessages(data.MensajesDeError);
                }
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });

    //Index 4 --> Codigo Dos
    $("#Tx_CodigoDos_Text").blur(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaAnulacion_CodigoDos_Blur",
            method: "POST",
            data: { selectedValue: $(this).val() },
            success: function (data) {
                if (data != null) {
                    $("#Tx_PlazaBancoContabiliza_Text").val(data.Tx_PlazaBancoContabiliza.Text);

                    loadMessages(data.MensajesDeError);
                }
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });

    //Index 7 --> R.U.T
    $("#Tx_Rut_Text").blur(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaAnulacion_RUT_Blur",
            method: "POST",
            data: { selectedValue: $(this).val() },
            success: function (data) {
                if (data != null) {
                    $("#Tx_Nombre_Text").val(data.Tx_Nombre.Text);
                    $("#Tx_Direccion_Text").val(data.Tx_Direccion.Text);
                    $("#Tx_Rut_Text").val(data.Tx_Rut.Text);

                    loadMessages(data.MensajesDeError);
                }
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });

    //Index 9 --> Entidad Autorizada
    $("#Tx_CodEntidadAutorizadaPlanillaAnulada_Text").blur(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaAnulacion_EntidadAutorizadaPlanillaAnulada_Blur",
            method: "POST",
            data: { selectedValue: $(this).val() },
            success: function (data) {
                if (data != null) {
                    $("#Tx_DesEntidadAutorizadaPlanillaAnulada_Text").val(data.Tx_DesEntidadAutorizadaPlanillaAnulada.Text);

                    loadMessages(data.MensajesDeError);
                }
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });

    });

    //Index 13 --> Tipo Operacion Anulada 
    $("#Tx_CodigoOperacionAnulada_Text").blur(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaAnulacion_TipoOperacionAnulada_Blur",
            method: "POST",
            data: { selectedValue: $(this).val() },
            success: function (data) {
                if (data != null) {
                    $("#Tx_TipoOperacionAnulada_Text").val(data.Tx_TipoOperacionAnulada.Text);

                    loadMessages(data.MensajesDeError);
                }
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });

    //Index 15 --> Plaza Banco Central 
    $("#Tx_CodigoBancaCentralAnulada_Text").blur(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaAnulacion_PlazaBancoCentralAnulada_Blur",
            method: "POST",
            data: { selectedValue: $(this).val() },
            success: function (data) {
                if (data != null) {
                    $("#Tx_PlazaBancoCentralAnulada_Text").val(data.Tx_PlazaBancoCentralAnulada.Text);

                    loadMessages(data.MensajesDeError);
                }
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });

    //Index 17 - Paridad Datos Planilla Anulada
    $("#Tx_ParidadMMONAnulada_Text").blur(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaAnulacion_ParidadUssDatosPlanillaAnulada_Blur",
            method: "POST",
            data: { selectedValue: $(this).val() },
            success: function (data) {
                if (data != null) {
                    $("#Tx_MontoMMONParidadOriginalAnulado_Text").val(data.Tx_MontoMMONParidadOriginalAnulado.Text);

                    loadMessages(data.MensajesDeError);
                }

            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });

    //Index 19 --> Aduana 
    $("#Tx_CodigoExportacion_Text").blur(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaAnulacion_AduanaExportacion_Blur",
            method: "POST",
            data: { selectedValue: $(this).val() },
            success: function (data) {
                if (data != null) {
                    $("#Tx_AduanaExportacion_Text").val(data.Tx_AduanaExportacion.Text);

                    loadMessages(data.MensajesDeError);
                }

            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });

    //Index 20 --> Numero de Aceptacion 
    $("#Tx_NumeroAceptacionExportacion_Text").blur(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaAnulacion_NumeroAceptacionExportacion_Blur",
            method: "POST",
            data: { selectedValue: $(this).val() },
            success: function (data) {
                if (data != null) {
                    $("#Tx_NumeroAceptacionExportacion_Text").val(data.Tx_NumeroAceptacionExportacion.Text);

                    loadMessages(data.MensajesDeError);
                }

            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });

    //Index 24 --> Codigo Moneda
    $("#Tx_CodigoMonedaAnulado_Text").blur(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaAnulacion_CodigoMonedaAnulado_Blur",
            method: "POST",
            data: { selectedValue: $(this).val() },
            success: function (data) {
                if (data != null) {
                    $("#Tx_DescripcionMonedaMontoAnulado_Text").val(data.Tx_DescripcionMonedaMontoAnulado.Text);
                    $("#Tx_ParidadMMONMontoAnulado_Text").val(data.Tx_ParidadMMONMontoAnulado.Text);

                    loadMessages(data.MensajesDeError);
                }
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });

    //Index 10 -  Numero Presentacion / Numero Aceptacion 
    $("#Tx_NumeroPresentacionAnulada_Text").blur(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaAnulacion_Index_Blur",
            method: "POST",
            data: { selectedValue: $(this).val(), index: 10 },
            success: function (data) {
                if (data != null) {

                }
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });

    //Index 11, 21,22,31 - Fecha Autorizacion
    //Index 11
    $("#Tx_FechaPresentacionAnulada_Text").blur(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaAnulacion_Index_Blur",
            method: "POST",
            data: { selectedValue: $(this).val(), index: 11 },
            success: function (data) {
                if (data != null) {

                }
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });
    //Index 21
    $("#Tx_FechaAceptacionExportacion_Text").blur(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaAnulacion_Index_Blur",
            method: "POST",
            data: { selectedValue: $(this).val(), index: 21 },
            success: function (data) {
                if (data != null) {
                    loadMessages(data.MensajesDeError);
                }
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });
    //Index 22
    $("#Tx_FechaVencimientoRetornoExportacion_Text").blur(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaAnulacion_Index_Blur",
            method: "POST",
            data: { selectedValue: $(this).val(), index: 22 },
            success: function (data) {
                if (data != null) {
                    loadMessages(data.MensajesDeError);
                }
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });
    //Index 31
    $("#Tx_FechaAutorizacion_Text").blur(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaAnulacion_Index_Blur",
            method: "POST",
            data: { selectedValue: $(this).val(), index: 31 },
            success: function (data) {
                if (data != null) {
                    loadMessages(data.MensajesDeError);
                }
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });
    
    //Index 25   Monto Anulado 
    $("#Tx_MontoAnulado_Text").blur(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaAnulacion_MontoAnulado_Blur",
            method: "POST",
            data: { selectedValue: $(this).val() },
            success: function (data) {
                if (data != null) {
                    $("#Tx_MontoMMONMontoAnulado_Text").val(data.Tx_MontoMMONMontoAnulado.Text);
                    $("#Tx_MontoMMONParidadOriginalAnulado_Text").val(data.Tx_MontoMMONParidadOriginalAnulado.Text);

                    loadMessages(data.MensajesDeError);
                }
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });

    //Index 26   Paridad Anulado
    $("#Tx_ParidadMMONMontoAnulado_Text").blur(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaAnulacion_ParidadMmonMontoAnulado_Blur",
            method: "POST",
            data: { selectedValue: $(this).val() },
            success: function (data) {
                if (data != null) {
                    $("#Tx_MontoMMONMontoAnulado_Text").val(data.Tx_MontoMMONMontoAnulado.Text);

                    loadMessages(data.MensajesDeError);
                }
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });

    //Index 32   Tipo Cambio - Datos de la Autorizacion
    $("#Tx_TipoCambioAutorizacion_Text").blur(function () {
        $.ajax({
            url: baseUrl + "FundTransfer/PlanillaAnulacion_TipoCambioAutorizacion_Blur",
            method: "POST",
            data: { selectedValue: $(this).val() },
            success: function (data) {
                if (data != null) {
                    $("#Tx_TipoCambioAutorizacion_Text").val(data.Tx_TipoCambioAutorizacion.Text);

                    loadMessages(data.MensajesDeError);
                }
            },
            error: function (data) {
                alert('Ha ocurrido un error');
            }
        });
    });

    $(":input").inputmask();

    if (modeloCompleto != null && modeloCompleto.MensajesDeError != null) {
        focusOnErrorControl(modeloCompleto.MensajesDeError);
    }
});