
namespace BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos
{

    public struct Cuentas
    {
        public string tipo;
        public string nrocta;
    }
    public class T_MODWS
    {
        public static string ACCESO = "";
        public static string MSJRET = "";
        public static string tipocta = "";
        public static string RUTTIT = "";
        public static string FLAGOPEN = "";
        public Cuentas[] CtaCCOL;

        public T_MODWS()
        {
            CtaCCOL = new Cuentas[0];

        }

    }
}
