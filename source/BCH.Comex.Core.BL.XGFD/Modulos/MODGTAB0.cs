using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.FinDia;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGFD.Modulos
{
    internal static class MODGTAB0
    {
        public static string Get_NemMnd(T_MODGTAB0 MODGTAB0, UnitOfWorkCext01 unit, short CodMnd)
        {
            short n = 0;
            short i = 0;
            short X = 0;

            n = (short)MODGTAB0.VMnd.Count;
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            if (n == 0)
            {
                X = SyGetn_Mnd(MODGTAB0, unit);
            }

            for (i = 0; i <= (short)MODGTAB0.VMnd.Count; i++)
            {
                if (MODGTAB0.VMnd[i].Mnd_MndCod == CodMnd)
                {
                    return MODGTAB0.VMnd[i].Mnd_MndNmc;
                }

            }

            return "";
        }

        // Lee la tabla Sgt_Pai y la deja en la estructura VMnd().
        // Retorno =  True    => Exitoso.
        //            False   => Erróneo.
        public static short SyGetn_Mnd(T_MODGTAB0 MODGTAB0, UnitOfWorkCext01 unit)
        {
            short _retValue;
            try
            {
                MODGTAB0.VMnd = unit.SgtRepository.EjecutarSP<sgt_mnd_s02_MS_Result>("sgt_mnd_s02_MS").Select(x => new T_Mnd()
                {
                    Mnd_MndCbc = (short)x.mnd_mndcbc,
                    Mnd_MndCod = (short)x.mnd_mndcod,
                    Mnd_MndNmc = x.mnd_mndnmc,
                    Mnd_MndNom = x.mnd_mndnom,
                    Mnd_MndSwf = x.mnd_mndswf,
                    Mnd_MndSin = (int)(x.mnd_mndcod != -1 ? -1 : 0)
                }).ToArray();
                _retValue = -1;

            }
            catch (Exception ex)
            {
                _retValue = 0;
            }
            return _retValue;
        }

    }

}