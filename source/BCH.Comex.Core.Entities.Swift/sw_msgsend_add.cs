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
    
    public partial class sw_msgsend_add
    {
        public int id_mensaje { get; set; }
        public Nullable<int> unidad_rechazo { get; set; }
        public Nullable<int> rut_rechazo { get; set; }
        public Nullable<System.DateTime> fecha_rechazo { get; set; }
        public string texto_rechazo { get; set; }
        public Nullable<int> veces_rechazo { get; set; }
        public Nullable<int> unidad_modifica { get; set; }
        public Nullable<int> rut_modifica { get; set; }
        public Nullable<System.DateTime> fecha_modifica { get; set; }
        public Nullable<int> veces_modifica { get; set; }
        public Nullable<System.DateTime> fecha_aprobac { get; set; }
        public Nullable<int> rut_bloqueo { get; set; }
        public Nullable<System.DateTime> fecha_bloqueo { get; set; }
        public Nullable<int> veces_bloqueo { get; set; }
        public string texto_bloqueo { get; set; }
        public Nullable<int> unidad_anula { get; set; }
        public Nullable<int> rut_anula { get; set; }
        public Nullable<System.DateTime> fecha_anula { get; set; }
        public string texto_anula { get; set; }
    }
}
