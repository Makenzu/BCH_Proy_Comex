//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BCH.Comex.Core.Entities.sbcor
{
    using System;
    using System.Collections.Generic;
    
    public partial class sbc_grp
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sbc_grp()
        {
            this.sbc_ope = new HashSet<sbc_ope>();
        }
    
        public string grp_codgrp { get; set; }
        public string grp_nomgrp { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sbc_ope> sbc_ope { get; set; }
    }
}
