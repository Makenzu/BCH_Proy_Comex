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
    using System.Collections.Generic;
    
    public partial class sw_mensajes_add
    {
        public int sesion { get; set; }
        public int secuencia { get; set; }
        public string send_recv { get; set; }
        public int casilla { get; set; }
        public string observ_encas { get; set; }
        public Nullable<System.DateTime> fecha_recibe { get; set; }
        public Nullable<int> unidad_recibe { get; set; }
        public Nullable<int> unidad_imprime { get; set; }
        public Nullable<int> rut_imprime { get; set; }
        public Nullable<System.DateTime> fecha_imprime { get; set; }
        public Nullable<int> unidad_rechazo { get; set; }
        public Nullable<int> rut_rechazo { get; set; }
        public Nullable<System.DateTime> fecha_rechazo { get; set; }
        public string texto_rechazo { get; set; }
        public Nullable<int> veces_rechazo { get; set; }
        public Nullable<int> rut_reenvio { get; set; }
        public Nullable<System.DateTime> fecha_reenvio { get; set; }
        public string texto_reenvio { get; set; }
        public Nullable<int> veces_reenvio { get; set; }
    }
}