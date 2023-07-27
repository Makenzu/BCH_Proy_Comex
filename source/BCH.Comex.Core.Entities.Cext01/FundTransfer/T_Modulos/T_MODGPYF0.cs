
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    //estructura de variables windows
    public class SeteosWin
    {
        public string SepDecimal;
        public string SepCientos;
        public string SepFecha;
        public string MinDate;
        public short PosDia;
        public short PosMes;
        public short PosAno;
    }

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
}
