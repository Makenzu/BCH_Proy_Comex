
namespace BCH.Comex.Core.BL.XEGI.Modulos
{
    public static class MODXDOC01
    {
        // Constantes de Impresión y Salir.-
        public const int CmdDoc_Imp = 1;
        public const int CmdDoc_Sal = 9;
        // Tabla de Documentos Export.-
        public struct TxDoc
        {
            public string FecEmi;
            public int NroMem;
        }
        public static TxDoc VDocx = new TxDoc();

    }
}
