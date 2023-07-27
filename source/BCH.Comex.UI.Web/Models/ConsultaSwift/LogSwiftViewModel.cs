using BCH.Comex.Core.Entities.Swift;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Models.ConsultaSwift
{
    public class LogSwiftViewModel
    {
        public IList<proc_sw_log_trae_msg_MS_Result> Log { get; set; }
        public int? Sesion { get; set; }
        public int? Secuencia { get; set; }
        public int? IdMensaje { get; set; }
    }
}