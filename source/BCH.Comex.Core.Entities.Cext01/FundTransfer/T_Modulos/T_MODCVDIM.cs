
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    public class T_NotaCreGl
    {
        //moncta  As Integer
        public string NumFac;
        public string FecOpe;
        public string NumRep;
        public string Moneda;
        public string netofac;
        public string ivafac;
        public string tipofac;
        public string monto;
    }

    public class CtaCC
    {
        public string codtra;
        public string NumCta;
        public string NemCta;
    }
    public class T_MODCVDIM
    {
        public  short Gvar_NotaCredito;
        public  string ope0;
        public  string ope1;
        public  string ope2;
        public  string ope3;
        public  string ope4;
        public  T_NotaCreGl VNotaCreGl;

        public  CtaCC[] CtaCCDin;

        public T_MODCVDIM() {
            VNotaCreGl = new T_NotaCreGl();
            CtaCCDin = new CtaCC[0];
        }
    }
}
