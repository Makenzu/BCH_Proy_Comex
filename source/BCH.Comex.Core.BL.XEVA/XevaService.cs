using BCH.Comex.Core.BL.XEVA.Forms;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.BusquedaPlanilla;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XEVA
{
    public class XevaService : IDisposable
    {
        private UnitOfWorkCext01 uow;
        public XevaService()
        {
            uow = new UnitOfWorkCext01();
        }

        public List<sce_xplv_MS_Result> BuscarOperacion(string numpre, string cui, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            numpre = numpre == null ? "" : numpre.PadLeft(7, '0');
            return FrmBPla.BuscarOperacion(numpre, cui, fechaDesde, fechaHasta, uow);
        }

        public void Dispose()
        {
            if (uow != null)
            {
                uow.Dispose();
            }
        }
    }
}
