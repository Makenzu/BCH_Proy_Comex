using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes
{
    public class Planilla_xPlv
    {
        public string VxPlvs_NumPre { set; get; }
        public string VPbc_Pbc_PbcDes { set; get; }
        public string VxPlvs_PlzBcc { set; get; }
        public string VxPlvs_fecpre { set; get; }
        public string NomPLn { set; get; }
        public string VxPlvs_TipPln { set; get; }
        public string DatPrt { set; get; }
        public string DatPrtn { set; get; }
        public string VxPlvs_RutExp { set; get; }
        public List<string> Palabras { set; get; }

        public Planilla_xPlv()
        {
            Palabras = new List<string>(); 
        }
    }
}
