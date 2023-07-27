using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    public class OrientStructure
    {
        public int Orientation;
        public string Pad = String.Empty; //max 15
    }
    public class T_MODGPYF0
    {
        private const int WM_USER = 0x400;
        private const int LB_SETTABSTOPS = WM_USER + 19;
        private const int MF_BYPOSITION = 0x400;
        public static int OLD_ORIENT = 0;
        public const int VERTICAL = 1;
        public const int HORIZONTAL = 2;
        public const int GETSETPAPERORIENT = 30;

        public string Siu_FechaHora = "";
        public int Siu_Vez = 0;
    }
}
