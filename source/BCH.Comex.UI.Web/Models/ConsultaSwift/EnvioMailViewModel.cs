using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BCH.Comex.UI.Web.Models.ConsultaSwift
{
    public class EnvioMailViewModel
    {

        [DisplayName("Para")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        [EmailAddress(ErrorMessage="Debe ingresar un correo válido")]
        public string To { get; set; }
        [DisplayName("Asunto")]
        public string Subject { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "El campo es obligatorio")]
        [DisplayName("Cuerpo")]
        public string Body { get; set; }
        [DisplayName("Mensajes swift")]
        [ReadOnly(true)]
        public List<MensajeSwiftAdjunto> Attachments { get; set; }

        public string IdsAttachments { get; set; } //esta propiedad es para que se me pueda hacer postback de los ids de los attachments y no perderlos

        public EnvioMailViewModel()
        {
            Attachments = new List<MensajeSwiftAdjunto>();
        }

        


    }
}
