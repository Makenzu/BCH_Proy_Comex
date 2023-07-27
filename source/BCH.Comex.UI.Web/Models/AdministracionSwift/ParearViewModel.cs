using BCH.Comex.Core.Entities.Swift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCH.Comex.UI.Web.Models.AdministracionSwift
{
    public class ParearViewModel
    {
        public int IdArchivo { get; set; }
        public IList<proc_sw_env_trae_detfile_MS_Result> DetalleArchivo { get; set; }
        public IList<proc_sw_env_trae_nop_MS_Result> MensajesNop { get; set; }
    }
}