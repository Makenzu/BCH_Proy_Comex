using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Portal;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.MT300Common
{
    /*Esta clase se ocupa para la carga y procesamiento de archivo*/
    public class ArchivoDetalle
    {
        public decimal id_archivo_detalle { get; set; }
        public decimal id_archivo { get; set; }
        public string id_swift { get; set; }
        public string reference { get; set; }
        public decimal amount_mn { get; set; }
        public decimal amount_me { get; set; }
        public string beneficiary { get; set; }
        public decimal safekeeping { get; set; }
        public DateTime value_date { get; set; }
        public decimal rate { get; set; }
        public DateTime booked_by { get; set; }
        public Byte flag_nack { get; set; }
        public Byte flag_validaciones { get; set; }
        public Byte flag_existente { get; set; }
        public Byte flag_formato { get; set; }
        public Byte flag_citi { get; set; }
        public string estado { get; set; }
        public string resultado { get; set; }
        public DateTime fecha_carga { get; set; }
        public DateTime? fecha_actualizacion { get; set; }
        public string mensaje_mt { get; set; }
        public string codigo_moneda_mn { get; set; }
        public string codigo_moneda_me { get; set; }
        public string campo22A { get; set; }
        public string campo22C { get; set; }
        public string campo82A { get; set; }
        public string campo87A { get; set; }
        public string campo53A { get; set; }
        public string campo57A { get; set; }
        public string tipo_operacion { get; set; }
        public Byte flag_ingresado_nuevo { get; set; }
        public decimal id_procesados { get; set; }
        public string executionTimehhmmss { get; set; } 
        public string campo98D { get; set; }

        public ArchivoDetalle()
        {
            executionTimehhmmss = "000000";
        }
    }
    
}
