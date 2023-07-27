using BCH.Comex.Core.Entities.Cext01.FundTransfer.Domain;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    public class T_MODGASO
    {
        public T_Aso VgAso;
        public T_Aso VgAsoNul;
        public const string MsgAso = "Asociación de Operación";

        public T_MODGASO()
        {
            VgAsoNul = new T_Aso();
            VgAso = new T_Aso();
        }
    }
}
