//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BCH.Comex.Core.Entities.Swift
{
    using System;
    
    public partial class proc_sw_env_trae_dev_firma_Result
    {
        public int id_mensaje { get; set; }
        public int sesion { get; set; }
        public int secuencia { get; set; }
        public int casilla { get; set; }
        public string nombre_casilla { get; set; }
        public string tipo_msg { get; set; }
        public string nombre_tipo { get; set; }
        public string estado_msg { get; set; }
        public string prioridad { get; set; }
        public int rut_ingreso { get; set; }
        public string fecha_ingr { get; set; }
        public string hora_ingr { get; set; }
        public string cod_banco_rec { get; set; }
        public string branch_rec { get; set; }
        public string cod_banco_em { get; set; }
        public string branch_em { get; set; }
        public string nombre_banco { get; set; }
        public string ciudad_banco { get; set; }
        public string pais_banco { get; set; }
        public string oficina_banco { get; set; }
        public string cod_moneda { get; set; }
        public string nombre_moneda { get; set; }
        public string cod_moneda_banco { get; set; }
        public Nullable<double> monto { get; set; }
        public string referencia { get; set; }
        public string beneficiario { get; set; }
        public int rut_firma { get; set; }
    }
}
