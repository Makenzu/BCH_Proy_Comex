
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes
{
    public class Planilla401 : Planilla_xPlv
    {
        public string VMnd_Mnd_MndNom { set; get; }
        public string VMnd_Mnd_MndCbc { set; get; }
        public string VxPlvs_AfiPar { set; get; }
        public string VxPlvs_AfiMto { set; get; }
        public string VxPlvs_AfiMtoD { set; get; }
        public string VxPlvs_AfiVen { set; get; }
        public string VxPlvs_TipCam { get; set; }
    }
}
