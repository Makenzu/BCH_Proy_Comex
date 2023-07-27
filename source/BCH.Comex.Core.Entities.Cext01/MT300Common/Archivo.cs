using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Portal;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.MT300Common
{
    public class Archivo
    {
        public decimal id_archivo { get; set; }
        public string nombre { get; set; }
        public decimal total_registros { get; set; }
        public decimal total_mt300_nuevos { get; set; }
        public decimal total_mt300_existentes { get; set; }
        public decimal total_registros_error { get; set; }
        public DateTime fecha_carga { get; set; }
        public string estado { get; set; }
        public string origen { get; set; }
        public string resultado { get; set; }
        public string tipo_archivo { get; set; }
        public string compra_venta { get; set; }
        public DateTime? fecha_actualizacion { get; set; }

    }

}
