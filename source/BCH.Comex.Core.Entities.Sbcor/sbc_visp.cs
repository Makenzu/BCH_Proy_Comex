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

    public partial class sbc_visp
    {
        public string visp_codswi { get; set; }
        public int visp_codcor { get; set; }
        public System.DateTime visp_fecvis { get; set; }
        public int visp_numcor { get; set; }
        public string visp_nompar { get; set; }
        public string visp_carpar { get; set; }
        public string visp_tippar { get; set; }
        public int visp_rutusr { get; set; }
        public string visp_dvusr { get; set; }
    
        public virtual sbc_vis sbc_vis { get; set; }
    }
}