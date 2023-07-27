using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_Crea_Participante
    {
        public short Rut_Haz;
        public short Fax_Haz;

        //mascara rut y fono
        public const string GPrt_RutMascara = "___.___.___-_";
        public const string GPrt_SwiftMascara = "____________";
        public const string GPrt_FonoMascara = "(___) ___-____";

        public const string GPrt_RutMask = @"###\.###\.###\-A";
        public const string GPrt_FonoMask = "(###) ###-####";
        public const string GPrt_SwiftMask = "&&&&&&&&&&&&";

        public const string CapSwift = "&Swift";
        public const string CapRut = "&Rut";

        public const string GPrt_OtroPais = "(Otro pais) ";
        public const string GPrt_ErrTablaPais = "¡¡Atención!!, se ha producido un error al accesar la tabla de paises del sistema, no sera posible mostrar dicha tabla para la operación.";
        public const string GPrt_EditarPais = "Escriba el nombre del pais de recidencia del participante en operación que no se encuentra en la lista de paises.  (este pais quedara registrado sin codificación en el sistema).";

        public short nuestro_pais;

        // Estas definiciones están tomadas de lo mostrado en el explorador
        // al seleccionar cada una de las funciones del servicio Web
        public const string cSOAPCta = @"<?xml version=""1.0"" encoding=""utf-8""?>" + @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:con=""http://osb.bancochile.cl/ConsultaCuentaCorriente/"" xmlns:head=""http://osb.bancochile.cl/common/HeaderRequest"" xmlns:opc=""http://osb.bancochile.cl/ESB/ConsultaCuentaCorriente/OpConsultaCuentaCorrienteRequest"">" + "<soapenv:Header>" + "<con:headerRequest>" + "<head:consumidor>" + "<head:idApp>Citidocs1.0</head:idApp>" + "<head:usuario>EJB-COMEX</head:usuario>" + "</head:consumidor>" + "<head:transaccion>" + "<head:internalCode></head:internalCode>" + "<head:idTransaccionNegocio>CDSWCX091028000093013915400</head:idTransaccionNegocio>" + "<head:fechaHora>2009-10-28T10:52:34</head:fechaHora>" + "<head:canal>COMEX00001</head:canal>" + "<head:sucursal>000</head:sucursal>" + "</head:transaccion>" + "</con:headerRequest>";
        public const string cSOAPCta2 = "</soapenv:Header>" + "<soapenv:Body>" + "<con:ConsultaCuentaCorriente>" + "<reqConsultaCuentaCorriente>" + "<opc:cuenta>1</opc:cuenta>" + "</reqConsultaCuentaCorriente>" + "</con:ConsultaCuentaCorriente>" + "</soapenv:Body>" + "</soapenv:Envelope>";
        public const string cSOAPDatos1 = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:con=""http://osb.bancochile.cl/ConsultaDireccionesProducto/"" xmlns:head=""http://osb.bancochile.cl/common/HeaderRequest"" xmlns:opc=""http://osb.bancochile.cl/ESB/ConsultaDireccionesProducto/OpConsultarDireccionesProductoRequest"">" + "<soapenv:Header>" + "<con:headerRequest>" + "<head:consumidor>" + "<head:idApp>Teller1.0</head:idApp>" + "<head:usuario>jtilleria</head:usuario>" + "</head:consumidor>" + "<head:transaccion>" + "<head:internalCode>?</head:internalCode>" + "<head:idTransaccionNegocio>GENlkjdsadljalskjd0001</head:idTransaccionNegocio>" + "<head:fechaHora>2009-11-03T13:13:13</head:fechaHora>" + "<head:canal>CAJA</head:canal>" + "<head:sucursal>000</head:sucursal>" + "</head:transaccion>" + "</con:headerRequest>" + "</soapenv:Header>";
        public const string cSOAPDatos2 = "<soapenv:Body>" + "<con:ConsultarDireccionesProducto>" + "<datosEntrada>" + "<opc:rutCliente>10280083-4</opc:rutCliente>" + "<opc:numeroProducto>1700109601</opc:numeroProducto>" + "</datosEntrada>" + "</con:ConsultarDireccionesProducto>" + "</soapenv:Body>" + "</soapenv:Envelope>";

        private string OldRut;
        private string OldSwift;
        //Obtenemos los datos
        private string Tablas;

        public string Caption { get; set; }

        public UI_TextBox rut { get; set; }
        public UI_CheckBox EsBanco { get; set; }
        public UI_TextBox Nombre { get; set; }
        public UI_TextBox Direccion { get; set; }
        public UI_TextBox comuna {get; set;}
        public UI_TextBox Ciudad {get; set;}
        public UI_TextBox Estado {get; set;}
        public UI_TextBox Postal { get; set; }
        public UI_TextBox cas_postal { get; set; }
        public UI_TextBox cas_bco { get; set; }
        public UI_TextBox Telefono {get; set;}
        public UI_TextBox Fax {get; set;}
        public UI_TextBox Telex {get; set;}
        public UI_Combo Pais { get; set; }
        public UI_TextBox OtroPais { get; set; }
        public List<UI_OptionItem> envio { get; set; }
        public UI_Button Aceptar { get; set; }
        public UI_Button Cancelar { get; set; }
        public UI_Button Consultar { get; set; }

        public UI_Frm_Crea_Participante()
        {
            rut = new UI_TextBox();
            EsBanco = new UI_CheckBox();
            Nombre = new UI_TextBox();
            Direccion = new UI_TextBox();
            comuna = new UI_TextBox();
            Ciudad = new UI_TextBox();
            Estado = new UI_TextBox();
            Postal = new UI_TextBox();
            cas_postal = new UI_TextBox();
            cas_bco = new UI_TextBox();
            Telefono = new UI_TextBox();
            Fax = new UI_TextBox();
            Telex = new UI_TextBox();
            Pais = new UI_Combo();
            OtroPais = new UI_TextBox();
            envio = new List<UI_OptionItem>() {
                new UI_OptionItem(){ Value = "Dirección" },
                new UI_OptionItem() {Value = "Casilla Bco."},
                new UI_OptionItem() {Value = "Fax"},
                new UI_OptionItem() {Value = "Casilla"},
            };
            Aceptar = new UI_Button();
            Cancelar = new UI_Button();
            Consultar = new UI_Button();
        }

    }
}
