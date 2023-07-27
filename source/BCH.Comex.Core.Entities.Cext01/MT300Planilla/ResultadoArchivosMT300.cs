using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Cext01.MT300Planilla
{
    public class ResultadoArchivosMT300
    {
        public decimal id_archivo { get; set; }
        public string nombre { get; set; }
        public string fecha_carga { get; set; }
        public string usuario { get; set; }
        public int error_nack { get; set; }
        public int error_formato { get; set; }
        public int mt300_generados { get; set; }
        public decimal mt300_existentes { get; set; }
        public decimal total_registros { get; set; }
        public string estado { get; set; }
    }
}
