using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms
{
    public class UI_GetPrty3 : UI_Frm
    {
        public UI_CheckBox EsBanco { set; get; }
        public UI_TextBox Telex { set; get; }
        public UI_TextBox Postal { set; get; }
        public UI_TextBox cas_postal { set; get; }
        public UI_TextBox cas_bco { set; get; }
        public UI_Combo Pais { set; get; }
        public UI_TextBox Ciudad { set; get; }
        public UI_TextBox Estado { set; get; }
        public UI_TextBox comuna { set; get; }
        public UI_TextBox Direccion { set; get; }
        public UI_TextBox Nombre { set; get; }
        public UI_Frame Frame1 { set; get; }
        public List<UI_CheckBox> envio { set; get; }
        public UI_CheckBox _envio_0 { set; get; }
        public UI_CheckBox _envio_1 { set; get; }
        public UI_CheckBox _envio_2 { set; get; }
        public UI_CheckBox _envio_3 { set; get; }
        public UI_Button Cancelar { set; get; }
        public UI_Button Aceptar { set; get; }
        public UI_TextBox Fax { set; get; }
        public UI_TextBox Telefono { set; get; }
        public UI_TextBox Rut { set; get; }

        public UI_Label _Label_0 { set; get; }

        public string MaskedRut { set; get; }
        public string MaskedPhone { set; get; }
        public string MaskedFax { set; get; }

        int Rut_Haz = 0;
        int Fono_Haz = 0;
        int Fax_Haz = 0;
        // mascara rut y fono
        public const string GPrt_RutMascara = "___.___.___-_";
        public const string GPrt_SwiftMascara = "____________";
        public const string GPrt_FonoMascara = "(___) ___-____";
        public const string GPrt_RutMask = "###\\.###\\.###\\-A";
        public const string GPrt_FonoMask = "(###) ###-####";
        public const string GPrt_SwiftMask = "&&&&&&&&&&&&";
        public const string CapSwift = "Swift";
        public const string CapRut = "Rut";
        public const string GPrt_OtroPais = "(Otro pais) ";
        public const string GPrt_ErrTablaPais = "¡¡Atención!!, se ha producido un error al accesar la tabla de paises del sistema, no sera posible mostrar dicha tabla para la operación.";
        public const string GPrt_EditarPais = "Escriba el nombre del pais de recidencia del participante en operación que no se encuentra en la lista de paises.  (este pais quedara registrado sin codificación en el sistema).";
        int nuestro_pais = 0;
        public string EsBanco_Click_OldRut = "";
        public string EsBanco_Click_OldSwift = "";
        public int paisId { set; get; }
        public int enviarCorrespondenciaA { set; get; }
        public string enviaCorrespondencia { get { return _envio_0.Checked ? "DI" : _envio_1.Checked ? "FA" : _envio_2.Checked ? "CB" : "CP"; } }

        public UI_GetPrty3()
        {
            EsBanco = new UI_CheckBox();
            Telex = new UI_TextBox();
            Postal = new UI_TextBox();
            cas_postal = new UI_TextBox() ;
            cas_bco = new UI_TextBox();
            Pais = new UI_Combo();
            Ciudad = new UI_TextBox();
            Estado = new UI_TextBox();
            comuna = new UI_TextBox();
            Direccion = new UI_TextBox();
            Nombre = new UI_TextBox();
            Frame1 = new UI_Frame();
            envio = new List<UI_CheckBox>();
            _envio_0 = new UI_CheckBox() { Checked = true};
            _envio_1 = new UI_CheckBox();
            _envio_2 = new UI_CheckBox();
            _envio_3 = new UI_CheckBox();
            envio.Add(_envio_0);
            envio.Add(_envio_1);
            envio.Add(_envio_2);
            envio.Add(_envio_3);
            Cancelar = new UI_Button();
            Aceptar = new UI_Button();
            Fax = new UI_TextBox();
            Telefono = new UI_TextBox();
            Rut = new UI_TextBox();

            _Label_0 = new UI_Label() {};

            //enviaCorrespondencia = _envio_0.Checked ? "DI" : _envio_1.Checked ? "FA" : _envio_2.Checked ? "CB" : "CP";
        }
    }
}
