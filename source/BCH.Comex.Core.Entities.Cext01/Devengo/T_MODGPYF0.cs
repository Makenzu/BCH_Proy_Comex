
namespace BCH.Comex.Core.Entities.Cext01.Devengo
{
    public class T_MODGPYF0
    {
        //Declaración de constantes generales
        private const short WM_USER = 0x400;
        public SeteosWin Win;

        public T_MODGPYF0()
        {
            Win = new SeteosWin();
        }
    }
    
    public struct SeteosWin
    {
        public string SepDecimal;
        public string SepCientos;
        public string SepFecha;
        public string MinDate;
        public int PosDia;
        public int PosMes;
        public int PosAno;
    }

}
