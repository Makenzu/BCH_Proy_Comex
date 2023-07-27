using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCH.Comex.UI.Web.Models.ControlIntegral
{

    public class EnvioMensajeEReparo
    {
        public string Ejecutivo { get; set; }
        public string Segmento { get; set; }
        public string Rut { get; set; }
        public string Cliente { get; set; }
        public string Monto { get; set; }
        public string Document_Name { get; set; }
        public string Asunto { get; set; }
        public bool GenerarHtmlCompleto { get; set; }
        public List<string> Titulo { get; set; }
        public string MailEjecutivoCorreo { get; set; }
    }

}
