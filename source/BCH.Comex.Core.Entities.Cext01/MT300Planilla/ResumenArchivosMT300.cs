using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Cext01.MT300Planilla
{
    public class ResumenArchivosMT300
    {
        public int archivos_procesados { get; set; }
        public decimal registros_procesados { get; set; }
        public int registros_error_nack { get; set; }
        public int registros_error_formato { get; set; }
        public int mensajes_generados { get; set; }
        public decimal mensajes_existentes { get; set; }

    }
}
