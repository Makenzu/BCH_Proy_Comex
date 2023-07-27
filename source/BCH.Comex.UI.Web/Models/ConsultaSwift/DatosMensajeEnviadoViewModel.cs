using BCH.Comex.Core.Entities.Swift;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Models.ConsultaSwift
{
    public class DatosMensajeEnviadoViewModel
    {
        public proc_sw_env_trae_datos_msg_MS_Result Datos { get; set; }
        public IList<sw_msgsend_firma> Firmas { get; set; }
    }
}