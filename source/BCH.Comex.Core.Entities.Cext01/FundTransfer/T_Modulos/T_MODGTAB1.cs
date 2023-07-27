using BCH.Comex.Core.Entities.Cext01.FundTransfer.Domain;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    public class T_MODGTAB1
    {
        public T_Adn[] VAdn;
        public T_Pbc[] VPbc;
        public T_Tcp[] VTcp;

        public const string MsgTab = "Tablas Generales";
        public T_MODGTAB1()
        {
            VAdn = new T_Adn[0];
            VPbc = new T_Pbc[0];
            VTcp = new T_Tcp[0];
        }
    }
}
