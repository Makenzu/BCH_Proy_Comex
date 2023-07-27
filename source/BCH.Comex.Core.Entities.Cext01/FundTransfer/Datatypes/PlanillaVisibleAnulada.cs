using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes
{
    public class PlanillaVisibleAnulada
    {
        public PlanillaVisibleAnulada()
        {
            Palabras = new List<string>();
        }
        public string COD_PLAZA_25 { get; set; }
        public string DatPrt { get; set; }
        public string DatPrt2 { get; set; }
        public List<string> Palabras { get; set; }
        public string VAdn_NomAdn { get; set; }
        public string VBco_NomBco { get; set; }
        public string VMnd_Mnd_MndCbc { get; set; }
        public string VMnd_Mnd_MndNom { get; set; }
        public string VPbc_Pbc_PbcDes { get; set; }
        public string VxAnus_CodAdn { get; set; }
        public string VxAnus_EntAut { get; set; }
        public string VxAnus_FecDec { get; set; }
        public string VxAnus_fecpre { get; set; }
        public string VxAnus_FecpreO { get; set; }
        public string VxAnus_FecVen { get; set; }
        public string VxAnus_MtoAnu { get; set; }
        public string VxAnus_MtoDol { get; set; }
        public string VxAnus_MtoDolA { get; set; }
        public string VxAnus_MtoDolPo { get; set; }
        public string VxAnus_Mtopar { get; set; }
        public string VxAnus_MtoParA { get; set; }
        public string VxAnus_numdec { get; set; }
        public string VxAnus_NumPre { get; set; }
        public string VxAnus_NumpreO { get; set; }
        public string VxAnus_PlzBcc { get; set; }
        public string VxAnus_RutExp { get; set; }
        public string VxAnus_TipAnu { get; set; }
        public string VxAnus_TipPln { get; set; }
        public string VxAnus_NomTipPln { get; set; }
    }
}
