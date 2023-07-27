using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.BusquedaPlanilla;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XEVA.Forms
{
    public class FrmBPla
    {
        public static List<sce_xplv_MS_Result> BuscarOperacion(string numpre, string cui, DateTime? fechaDesde, DateTime? fechaHasta, UnitOfWorkCext01 uow)
        {
            List<sce_xplv_MS_Result> returnValue = new List<sce_xplv_MS_Result>();
            try
            {
                List<sce_xplv_MS_Result> result = uow.SceRepository.sce_xplv_MS(numpre, cui, fechaDesde, fechaHasta);
                if (result != null)
                {
                    returnValue = result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;

        }

    }
}
