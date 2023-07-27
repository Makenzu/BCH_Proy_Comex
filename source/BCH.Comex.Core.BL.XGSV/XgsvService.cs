using BCH.Comex.Core.BL.XGSV.Modulos;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XGSV
{
    public partial class XgsvService: IDisposable
    {
        private UnitOfWorkCext01 uow;
        public XgsvService()
        {
            uow = new UnitOfWorkCext01();
        }

        public IEnumerable<string> SyGetn_Trasp(string cCtUsr, string codusr)
        {
            return MODTRASP.SyGetn_Trasp(cCtUsr, codusr, uow);
        }

        public int SyPut_Trasp(string cCtUsr, string codusr, string reemplazos)
        {
            return MODTRASP.SyPut_Trasp(cCtUsr, codusr, reemplazos, uow);
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
