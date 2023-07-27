﻿using System;

namespace BCH.Comex.Core.Entities.Swift
{
    public class proc_sw_log_trae_enc_rangoDTO
    {
        public int sesion { get; set; }
        public int secuencia { get; set; }
        public string tipo_msg { get; set; }
        public string prioridad { get; set; }
        public string estado_msg { get; set; }
        public string fecha1 { get; set; }
        public string hora1 { get; set; }
        public string fecha_pro { get; set; }
        public string hora_pro { get; set; }
        public string cod_banco_rec { get; set; }
        public string branch_rec { get; set; }
        public string cod_banco_em { get; set; }
        public string branch_em { get; set; }
        public string cod_moneda { get; set; }
        public double monto { get; set; }
        public string referencia { get; set; }
        public string beneficiario { get; set; }
        public int total_imp { get; set; }
        public int casilla { get; set; }
        public DateTime fecha_hora { get; set; }
    }
}
