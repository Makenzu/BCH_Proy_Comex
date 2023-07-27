using System;

namespace BCH.Comex.Core.Entities.Cext01.Common
{
    public class Det_Pai
    {
        public string PaiCod = String.Empty;   //Código País.
        public string PaiNom = String.Empty;   //Nombre País.
        public int Painum;
    }
    public class T_MOD_50F
    {
        // Flag 50F
        // ------------------------------------------------------------------------------
        // Modulo insertado para modificacion MT-103 por Realsystems
        // Fecha 05/10/2007
        // Modulo que contiene declaraciones, y 2 funciones  para llenado de combo de pais
        // -------------------------------------------------------------------------------
        // Variable global booleana para identificar
        // si Checkbox 50F se encuentra activo
        public bool CHK_50F = false;
        public string[,] VG_50F = new string[0,0];
        public int VG_N = 0;
        public Det_Pai[] VG_Pais = new Det_Pai[0];
    }
}
