using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODCTA
    {
        public static int SyGetn_CtaCtb1(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            T_MODCTA MODCTA = Globales.MODCTA;
            short _retValue = 0;
            try
            {
                MODCTA.CtaCtb = unit.SceRepository.EjecutarSP<sce_cta_s06_Result>("sce_cta_s06").Select(x => new T_CtaCtb()
                {
                    Cta_Mon = (short)x.cta_mon,
                    Cta_Nem = x.cta_nem,
                    Cta_Nom = x.cta_nom,
                    Cta_Num = x.cta_num
                }).ToArray();
                _retValue = (short)(true ? -1 : 0);
            }
            catch (Exception e)
            {
                _retValue = 0;
            }
            return _retValue;
        }
    }
}
