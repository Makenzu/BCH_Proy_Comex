using BCH.Comex.Data.DAL.Cext01;
using System;

namespace BCH.Comex.Core.BL.Common
{
    public partial class CommonService : IDisposable
    {
        private UnitOfWorkCext01 uow;
        public CommonService()
        {
            uow = new UnitOfWorkCext01();
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
