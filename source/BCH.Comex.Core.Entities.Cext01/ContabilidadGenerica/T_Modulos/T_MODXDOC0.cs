using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    public class TxDoc
    {
        public string FecEmi = String.Empty;
        public int NroMem;
    }
    public class T_MODXDOC0
    {
        // Constantes de Impresión y Salir.-
        public const int CmdDoc_Imp = 1;
        public const int CmdDoc_Sal = 9;
        public TxDoc VDocx = new TxDoc();
    }
}
