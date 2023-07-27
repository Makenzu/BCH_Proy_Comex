using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODGMTA
    {
        public static int FindImp(DatosGlobales Globales,UnitOfWorkCext01 unit, string Cod)
        {
            T_MODGMTA MODGMTA = Globales.MODGMTA;

            int FindImp = 0;

            int a = 0;
            int k = 0;
            k = 0;
            k = MODGMTA.VImp.GetUpperBound(0);
            
            if (k == -1)
            {
                k = SyGetn_Imp(Globales,unit);
            }

            for (a = 1; a <= MODGMTA.VImp.GetUpperBound(0); a += 1)
            {
                if (MODGMTA.VImp[a].CodImp == Cod)
                {
                    FindImp = a;
                    break;
                }
            }

            return FindImp;
        }

        // **************************************************
        //                                                  *
        //  Función  : SyGetn_Imp ()                        *
        //                                                  *
        //  Objetivo : Lee la Tabla Impuesto (Sce_Mta3).-   *
        //                                                  *
        //  Fecha    : Marzo/1996                           *
        //                                                  *
        // **************************************************

        public static int SyGetn_Imp(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            T_MODGMTA MODGMTA = Globales.MODGMTA;
            int SyGetn_Imp = 0;
            try
            {
                var res = unit.SceRepository.EjecutarSP<sce_mta3_s01_Result>("sce_mta3_s01");

                if (res.Count == 0)
                {
                    return SyGetn_Imp;
                }
                res.Insert(0, new sce_mta3_s01_Result());
                MODGMTA.VImp = res.Select(x => new T_Imp()
                {
                    CodImp=x.codimp,
                    NomImp=x.nomimp,
                    MtoFij=x.mtofij.ToInt(),
                    TasMin=x.tasmin.ToDbl(),
                    tasmax=x.tasmax.ToDbl(),
                    MtoMin=x.mtomin.ToDbl(),
                    MtoMax=x.mtomax.ToDbl(),
                    cta_mn =x.cta_mn,
                    cta_me=x.cta_me
                }).ToArray();
                
                SyGetn_Imp = 1;

                return SyGetn_Imp;

            }
            catch (Exception exc)
            {
                Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                {
                    Type=Common.UI_Modulos.TipoMensaje.Error,
                    Text= "Se ha producido un error al tratar de leer la tabla Impuestos (Sce_Mta3)."
                });

            }
            return SyGetn_Imp;
        }
    }
}
