using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Mcambio
{
    public class Cext01_sp_sce_anulacion_net
    {
        public int idChileFx { get; set; }
        public double mtoChileFx { get; set; }
        public string rutchilefx { get; set; }
        public int cod_error { get; set; }
        public string msg_error { get; set; }
    }
}
