//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BCH.Comex.Core.Entities.Sbcor
{

    public partial class sbc_vist
    {
        public string vist_codswi { get; set; }
        public int vist_codcor { get; set; }
        public System.DateTime vist_fecvis { get; set; }
        public int vist_codtem { get; set; }
        public int vist_numlin { get; set; }
        public string vist_texto { get; set; }
    
        public virtual sbc_vis sbc_vis { get; set; }
    }
}
