using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODTDME
    {
        //****************************************************************************
        //   1.  Lee la Tabla de Destinos de Fondos Moneda Extranjera.
        //   2.  Si el resultado es 1(True)  => Lectura Exitosa.
        //       Si el resultado es 0(False) => Lectura No Exitosa.
        //****************************************************************************
        public static short SyGetn_Tdme(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            short _retValue = 0;
            try
            {
                var res = unit.SceRepository.EjecutarSP<sce_tdme_s01_MS_Result>("sce_tdme_s01_MS");
                res.Insert(0, new sce_tdme_s01_MS_Result());

                Globales.MODTDME.VTDme = res.Select(x => new T_Tdme()
                {
                    CodDme = (short)x.coddme,
                    DesDme = x.desdme
                }).ToArray();
                _retValue = (short)(true ? -1 : 0);
            }
            catch (Exception _ex)
            {
                _retValue = 0;
            }
            return _retValue;
        }
    }
}
