using System;

namespace BCH.Comex.Core.Entities.Cext01.MT300Common
{
    public class Mt300ArchivoProcesado
    {
        public decimal id_procesados { get; set; }
        public decimal id_archivo_detalle { get; set; }
        public decimal id_swift { get; set; }
        public string reference { get; set; }
        public decimal amount_mn { get; set; }
        public decimal amount_me { get; set; }
        public string beneficiary { get; set; }
        public decimal safekeeping { get; set; }
        public DateTime value_date { get; set; }
        public decimal rate { get; set; }
        public DateTime booked_by { get; set; }
        public string estado { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_actualizacion { get; set; }
        public string mensaje_mt { get; set; }
        public string codigo_moneda_mn { get; set; }
        public string codigo_moneda_me { get; set; }
        public string campo22A { get; set; }
        public string campo22C { get; set; }
        public string campo82A { get; set; }
        public string campo87A { get; set; }
        public string campo53A { get; set; }
        public string campo57A { get; set; }
        public Byte flag_ingresado_nuevo { get; set; }
        public string estado_msg { get; set; }
        public string campo98D { get; set; }
    }

}
