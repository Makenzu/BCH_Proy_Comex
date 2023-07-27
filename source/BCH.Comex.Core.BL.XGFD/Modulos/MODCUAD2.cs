using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FinDia;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XGFD.Modulos
{
    internal static class MODCUAD2
    {

        /// <summary>
        /// Cuadratura de inyecciones cuenta corriente online
        /// </summary>
        /// <param name="cencos"></param>
        /// <param name="codusr"></param>
        /// <param name="fecmov"></param>
        /// <returns></returns>
        public static bool SyGet_ContaEsp2(string cencos, string codusr, DateTime fecmov, T_MODCUACC modCuaCC, UnitOfWorkCext01 uow,
            IList<UI_Message> listaMensajes)
        {
            bool SyGet_ContaEsp2 = false;

            // *************************************************************************************************************************************
            //    Autor                      : Accenture - Continuidad Comex
            //    Incidente                  : 47012-198
            //    Descripcion                : Cuadratura de inyecciones cuenta corriente online.
            //    Fecha                      : Abril de 2012
            //    Identificador de Inicio    : ACC-001-I
            //    Identificador de Termino   : ACC-001-F
            // *************************************************************************************************************************************

            try
            {
                modCuaCC.VConCCLin2 = new List<T_ConCCLin>();
                SyGet_ContaEsp2 = true;

                var result = uow.SceRepository.Sce_cuadra_inyecciones_ctacte_MS(cencos, codusr, fecmov);

                // Resultado nulo de la Consulta.-
                if (result == null)
                {
                    return false;
                }

                if (result.Count > 0)
                {
                    foreach (var item in result)
                    {
                        modCuaCC.VConCCLin2.Add(new T_ConCCLin
                        {
                            cencos = cencos,
                            codusr = codusr,
                            codcct = item.codcct,
                            codpro = item.codpro,
                            codesp = item.codesp,
                            codofi = item.codofi,
                            codope = item.codope,
                            nrorpt = (int)item.nrorpt,
                            fecmov = item.fecmov,
                            cod_dh = item.cod_dh,
                            nemcta = item.nemcta,
                            numcct = item.numcct,
                            mtomcd = (double)item.mtomcd,
                            nemmon = item.nemmon,
                            estado = (int)item.estado,
                            cuadra = 0,
                            error = 0
                        });
                    }

                    //SyGet_ContaEsp2 = false;
                }

                return SyGet_ContaEsp2;
            }
            catch(Exception ex)
            {
                if (!ExceptionPolicy.HandleException(ex, "PoliticaBLFundTransfer")) throw;

                return SyGet_ContaEsp2;
            }
        }
    }
}
