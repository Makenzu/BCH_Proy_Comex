//using BCH.Comex.Common.XGPY.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;
using CodeArchitects.VB6Library;

namespace BCH.Comex.Core.BL.XGPY.Modulos
{
    public class PRTGLOB
    {
        public static T_PRTGLOB GetPRTGLOB()
        {
            return new T_PRTGLOB();
        }
        public static double ValTexto(string caja)
        {
            string s = caja;
            s = UTILES.unformat(s);
            return VB6Helpers.Val(s);           
        }

        public const int individuo = 0;
        public const int tipo_banco = 1;
        public const int tipo_cliente = 2;

        public const int GPRT_FlagInstrucciones = 1;
        public const int Gprt_FlagTasas = 2;
        public const int Gprt_FlagCuentas = 4;
        public const int Gprt_FlagCorresponsal = 8;
        public const int Gprt_FlagAcreedor = 16;
        public const int GPRT_FlagSwift = 32;
        public const int GPRT_FlagAladi = 64;
        public const int GPRT_FlagAvisador = 128;

    }
}
