using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes
{
    public class PlanillaInvisible
    {
        public string DatPrt1 { get; set; }
        public string DatPrt2 { get; set; }
        public List<string> Palabras { get; set; }
        public string VMnd_Mnd_MndNom { get; set; }
        public string VPai_Pai_PaiNom { get; set; }
        public string VPbc_Pbc_PbcDes { get; set; }
        public string VplisApcNum { get; set; }
        public string Vplis_AnuFec { get; set; }
        public string Vplis_AnuNum { get; set; }
        public string Vplis_AnuPbc { get; set; }
        public string Vplis_ApcFec { get; set; }
        public string Vplis_ApcPbc { get; set; }
        public string Vplis_ApcTip { get; set; }
        public string Vplis_CodAdn { get; set; }
        public string Vplis_codcom { get; set; }
        public string Vplis_CodEOR { get; set; }
        public string Vplis_CodMndBC { get; set; }
        public double Vplis_CodOci { get; set; }
        public string Vplis_codpai { get; set; }
        public string Vplis_Concep { get; set; }
        public List<string> Vplis_Desacu { get; set; }
        public string Vplis_DieFec { get; set; }
        public string Vplis_DieNum { get; set; }
        public string Vplis_DiePbc { get; set; }
        public string Vplis_DocExt { get; set; }
        public string Vplis_DocNac { get; set; }
        public string Vplis_FecCr { get; set; }
        public string Vplis_FecDeb { get; set; }
        public string Vplis_FecDec { get; set; }
        public string Vplis_FecPli { get; set; }
        public string Vplis_MndCre { get; set; }
        public string Vplis_MndCreRepeat { get; set; }
        public string Vplis_MtoCre { get; set; }
        public string Vplis_MtoDol { get; set; }
        public string Vplis_MtoNac { get; set; }
        public string Vplis_MtoOpe { get; set; }
        public string Vplis_Mtopar { get; set; }
        public string Vplis_NumCre { get; set; }
        public string Vplis_numdec { get; set; }
        public string Vplis_NumPli { get; set; }
        public string Vplis_PlzBcc { get; set; }
        public string Vplis_rutcli { get; set; }
        public string Vplis_TipCam { get; set; }
        public short Vplis_TipPln { get; set; }
        public string VTcp_DesTcp { get; set; }

        public PlanillaInvisible()
        {
            Palabras = new List<string>();
        }
    }
}
