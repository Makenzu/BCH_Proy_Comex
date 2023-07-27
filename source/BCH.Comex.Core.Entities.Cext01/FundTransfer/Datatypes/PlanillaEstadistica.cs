using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes
{
    public class PlanillaEstadistica
    {
        public string tint;

        public List<Detalle> Detalles { get; set; }
        public string Linea { get; set; }
        public string NroAcs { get; set; }
        public string PlnVEst_CanAco { get; set; }
        public string PlnVEst_CifDol { get; set; }
        public string PlnVEst_CifOri { get; set; }
        public string PlnVEst_CodEnt_R { get; set; }
        public string PlnVEst_codfdp { get; set; }
        public string PlnVEst_CodMndBcc { get; set; }
        public string PlnVEst_CodPaiPag { get; set; }
        public string PlnVEst_CodPem { get; set; }
        public string PlnVEst_CodPla { get; set; }
        public string PlnVEst_CodPlz { get; set; }
        public string PlnVEst_CodPlz_R { get; set; }
        public string PlnVEst_FecAutDeb { get; set; }
        public string PlnVEst_FecCon { get; set; }
        public string PlnVEst_FecCon_R { get; set; }
        public string PlnVEst_FecIdi { get; set; }
        public string PlnVEst_FecInf_R { get; set; }
        public string PlnVEst_FecPln_R { get; set; }
        public string PlnVEst_FecVop { get; set; }
        public string PlnVEst_FecVta { get; set; }
        public string PlnVEst_FleOri { get; set; }
        public string PlnVEst_FobOri { get; set; }
        public string PlnVEst_GasBco { get; set; }
        public string PlnVEst_HasFob { get; set; }
        public string PlnVEst_IntOri { get; set; }
        public string PlnVEst_NomImp { get; set; }
        public string PlnVEst_NroCcp { get; set; }
        public string PlnVEst_NroDcp { get; set; }
        public string PlnVEst_NroDocChi { get; set; }
        public string PlnVEst_NroDocExt { get; set; }
        public string PlnVEst_NroPln { get; set; }
        public string PlnVEst_NumCon { get; set; }
        public string PlnVEst_NumCon_R { get; set; }
        public string PlnVEst_NumIdi { get; set; }
        public string PlnVEst_NumInf_R { get; set; }
        public string PlnVEst_NumPln_R { get; set; }
        public string PlnVEst_ParPla { get; set; }
        public string PlnVEst_PlzInf_R { get; set; }
        public string PlnVEst_RutImp { get; set; }
        public string PlnVEst_SegOri { get; set; }
        public string PlnVEst_TotDol { get; set; }
        public string PlnVEst_TotOri { get; set; }
        public string PlnVEst_ValMer { get; set; }
        public string VMnd_Mnd_MndNom { get; set; }
        public string VPai_Pai_PaiNom { get; set; }
    }
}
