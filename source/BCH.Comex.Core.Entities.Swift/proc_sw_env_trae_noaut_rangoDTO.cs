using System;

namespace BCH.Comex.Core.Entities.Swift
{
    public class proc_sw_env_trae_noaut_rangoDTO
    {
        public int id_mensaje { get; set; }
        public int sesion { get; set; }
        public int secuencia { get; set; }
        public int casilla { get; set; }
        public string nombre_casilla { get; set; }
        public string tipo_msg { get; set; }
        public string nombre_tipo { get; set; }
        public string prioridad { get; set; }
        public string estado_msg { get; set; }
        public string fecha_ingr { get; set; }
        public string hora_ingr { get; set; }
        public string cod_banco_em { get; set; }
        public string branch_em { get; set; }
        public string cod_banco_rec { get; set; }
        public string branch_rec { get; set; }
        public string nombre_banco { get; set; }
        public string ciudad_banco { get; set; }
        public string pais_banco { get; set; }
        public string oficina_banco { get; set; }
        public string cod_moneda { get; set; }
        public string nombre_moneda { get; set; }
        public string cod_moneda_banco { get; set; }
        public double monto { get; set; }
        public string referencia { get; set; }
        public string beneficiario { get; set; }
        public DateTime fecha_de_orden { get; set; }
        public DateTime fecha_hora_ingreso
        {
            get
            {
                DateTime fecha = new DateTime(1900, 1, 1);
                return (DateTime.TryParse(fecha_ingr + " " + hora_ingr, out fecha)) ? fecha : DateTime.Parse(fecha_ingr);
            }
        }
    }
}
