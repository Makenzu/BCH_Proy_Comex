using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using System;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MOD_ADIC
    {
        public static int ind_numero(DatosGlobales Globales, int Indice)
        {
            T_MOD_ADIC MOD_ADIC = Globales.MOD_ADIC;
            int ind_numero = 0;

            int i = 0;
            int fin = 0;

            fin = -1;
            fin = MOD_ADIC.Numeros.GetUpperBound(0);

            for (i = 0; i <= fin; i += 1)
            {
                if (MOD_ADIC.Numeros[i].Indice == Indice && !MOD_ADIC.Numeros[i].listo.ToBool() && !MOD_ADIC.Numeros[i].Borrado.ToBool())
                {
                    ind_numero = i;
                    break;
                }
            }

            return ind_numero;
        }
    }
}
