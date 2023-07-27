
namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    public class T_Cta
    {
        public string Cta_Nem;//MAX 15
        public int Cta_Mon;
        public string Cta_Num;//MAX 8
        public string Cta_Nom;//MAX 38
        public int Cta_GL;
        public int Cta_NroTO;
        public int Cta_IndTO;
        public int Cta_CIT;
        public int Cta_CVT;
        public int Cta_CAP;
        public int Cta_CTD;
        public int Cta_POS;
        public int Cta_CDR;
        public int Cta_Vig;   // Vigenteable
    }
    public class T_MODGNCTA
    {
        public T_Cta[] VCta = new T_Cta[0];
        public const int CtaMonExt = 1;
        public const int CtaMonNac = 2;
        public const int CtaMonAmb = 3;
    }
}
