
namespace BCH.Comex.Common.XGPY.T_Modulos
{
    public class SeteosWin
    {
        public string SepDecimal;
        public string SepCientos;
        public string SepFecha;
        public string MinDate;
        public int PosDia;
        public int PosMes;
        public int PosAno;
    }

    public class T_UTILES
    {
        public static SeteosWin SeteosWin;

        public T_UTILES()
        {
            SeteosWin = new SeteosWin();
        
        }
    }


}
