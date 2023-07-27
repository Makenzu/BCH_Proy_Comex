
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    public class T_CtaCtb
    {
        public string Cta_Nem;
        public short Cta_Mon;
        public string Cta_Num;
        public string Cta_Nom;
    }

    //Variables Generales de Cuentas.-
    public class T_CtaGl
    {
        public short moncta;
        public string NemCta;
    }
    public class T_MODCONT
    {
        public T_CtaCtb[] CtaCtb;
        public T_CtaGl VCtaGl;
        //Constantes para Funciones Contables.-
        // UPGRADE_INFO (#0561): The 'CodFun_CVD' symbol was defined without an explicit "As" clause.
        public  const short CodFun_CVD = 605;

        public T_MODCONT()
        {
            this.CtaCtb = new T_CtaCtb[0];
            this.VCtaGl = new T_CtaGl();
        }
    }
}
