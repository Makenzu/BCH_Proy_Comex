using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FinDia
{
    public class T_MODGPLN
    {
        public IList<T_gPlnCon> VPlnCon { get; set; }
    }

    // Arreglo General de Planillas para los Montos (Sce_xPlv ; Sce_Pli).
      public class T_gPlnCon
      {
          public int codmnd { get; set; }
          public double MtoEgr { get; set; }
          public double MtoIng { get; set; }
          public double MtoDeb { get; set; }
          public double MtoHab { get; set; }
          public double DifDeb { get; set; }
          public double DifHab { get; set; }
      }
      
}
