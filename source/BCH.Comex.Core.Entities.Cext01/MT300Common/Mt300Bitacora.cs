using System;

namespace BCH.Comex.Core.Entities.Cext01.MT300Common
{
    public class Mt300Bitacora
    {
        public decimal id_bitacora { get; set; }
        public decimal? id_archivo { get; set; }
        public decimal? id_procesados { get; set; }
        public decimal? id_archivo_detalle { get; set; }
        public string usuario { get; set; }
        public string resultado { get; set; }
        public string resultado_1 { get; set; }
        public string resultado_2 { get; set; }
        public string tipo_movimiento { get; set; }
        public DateTime fecha_registro { get; set; }
    }

}
