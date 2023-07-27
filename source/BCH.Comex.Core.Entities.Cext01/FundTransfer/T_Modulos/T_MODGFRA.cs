using BCH.Comex.Core.Entities.Cext01.FundTransfer.Domain;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    public class T_MODGFRA
    {
        public const short FunIng_CobExp = 1;
        public T_FraGen V_FraGen ;
        public T_InsEsp[] V_InsEsp ;

        public T_MODGFRA() {
            V_FraGen = new T_FraGen();
        }
    }
}
