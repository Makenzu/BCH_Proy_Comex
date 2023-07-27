using BCH.Comex.Core.Entities.Cext01.FundTransfer.Domain;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using CodeArchitects.VB6Library;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODGFRA
    {
        public const short FunIng_CobExp = 1;

        public static T_MODGFRA GetMODGFRA() {
            return new T_MODGFRA();
        }

        //****************************************************************************
        //   1.  Se rescata el Ubound del arreglo + 1 para redimencionarlo
        //       y agregarle uno nuevo, el que es el campo Título.
        //       Borrar : Indica si además se limpia la estructura antes de usarla.-
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short Put_InsEsp(T_MODGFRA MODGFRA, string Texto, short borrar, string Idioma)
        {
            short n = 0;
            if (borrar != 0)
            {
                MODGFRA.V_InsEsp = new T_InsEsp[0];
            }
            n = (short)VB6Helpers.UBound(MODGFRA.V_InsEsp);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            n = (short)(n + 1);
            var aux = new List<T_InsEsp>(MODGFRA.V_InsEsp);
            aux.Add(new T_InsEsp());
            MODGFRA.V_InsEsp = aux.ToArray();
            MODGFRA.V_InsEsp[n].Titulo = Texto;
            MODGFRA.V_InsEsp[n].Idioma = Idioma;

            return n;
        }

        //***********************************************************
        //Rescata la Frase Estándard desde una posición determinada.-
        //***********************************************************
        public static string Get_InsEsp(T_MODGFRA MODGFRA, short Indice)
        {
            return MODGFRA.V_InsEsp[Indice].Valor_Act;
        }
    }
}
