using BCH.Comex.Core.Entities.Swift;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Models.ConsultaSwift
{
    public class DetalleSwiftEnviadoViewModel
    {
        public ResultadoBusquedaSwift Swift { get; set; }
        public IList<sw_msgsend_firma> Firmas { get; set; }
        public bool GenerarHtmlCompleto { get; set; }
    }
}