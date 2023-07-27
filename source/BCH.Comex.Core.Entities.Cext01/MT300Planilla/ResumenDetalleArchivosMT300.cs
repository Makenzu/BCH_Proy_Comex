using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Cext01.MT300Planilla
{
    public class ResumenDetalleArchivosMT300
    {
        public string archivo { get; set; }
        public string fecha_carga { get; set; }
        public string estado { get; set; }
        public int mensajes_generados { get; set; }
        public int registros_errores { get; set; }
        public int registros_existentes { get; set; }
        public decimal registros_totales { get; set; }


    }
}
