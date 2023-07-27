using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Cext01.MT300Planilla
{
    public class ResultadoDetalleArchivosMT300
    {
        public decimal id_archivo { get; set; }
        public decimal id_detalle { get; set; }
        public decimal safekeeping { get; set; }
        public string beneficiario { get; set; }
        public string referencia { get; set; }
        public string estado { get; set; }
    }
}
