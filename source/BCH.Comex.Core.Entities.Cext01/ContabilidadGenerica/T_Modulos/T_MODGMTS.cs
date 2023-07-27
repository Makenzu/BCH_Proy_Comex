using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    public class T_VCcs
    {
        public int codmt;   //Código de Mt
        public string codcam = String.Empty;   //Código de Campo
        public string DesCam = String.Empty;   //Descripción Campo
        public int CamMan;   //Campo Manual
        public int numlin;   //Número de Líneas del campo
        public int lenlin;   //Largo de La Línea
        public int LenTot;   //Largo total del campo
    }
    public class T_Mts
    {
        public string NomArc = String.Empty;
        public int CodArc;
    }
    public class T_MODGMTS
    {
        public T_VCcs[] VCCs = new T_VCcs[0];
        public T_Mts VgMts = new T_Mts();
        public const int TabT = 27;
        public const int TabH = 16;
        public const int TabD = 44;
        public const int TabE = 30;
        // *************************************************
        public const string CamposLibres = "72-73-75-76-79";
    }
}
