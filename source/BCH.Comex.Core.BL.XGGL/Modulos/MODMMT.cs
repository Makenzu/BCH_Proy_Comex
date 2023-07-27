using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using System;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODMMT
    {
        // Crea una entrada para un MT.-
        public static int Put_MMT(DatosGlobales Globales, int codmt, string Titulo, string MT)
        {
            T_MODMMT MODMMT = Globales.MODMMT;
            int Put_MMT = 0;
            int n = 0;

            n = MODMMT.VMT_R.GetUpperBound(0);
            
            if (n == -1)
            {
                MODMMT.VMT_R = new T_MT_R[1] { new T_MT_R() };
                n++;
            }
            else
            {
                n = MODMMT.VMT_R.GetUpperBound(0) + 1;
                Array.Resize(ref MODMMT.VMT_R, n + 1);
                MODMMT.VMT_R[n] = new T_MT_R();
            }
            
            MODMMT.VMT_R[n].codmt = codmt;
            MODMMT.VMT_R[n].Titulo = Titulo;
            MODMMT.VMT_R[n].ValAnt = MT;
            MODMMT.VMT_R[n].ValAct = MT;
            Put_MMT = true.ToInt();

            return Put_MMT;
        }
    }
}
