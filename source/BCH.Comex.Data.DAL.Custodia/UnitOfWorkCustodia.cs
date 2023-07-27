using BCH.Comex.Core.Entities.Custodia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Data.DAL.Custodia
{
    public class UnitOfWorkCustodia : IDisposable
    {
        private CustodiaEntities context = new CustodiaEntities();
        private CambiosRepository cambiosRepository;

        public CambiosRepository CambiosRepository
        {
            get
            {
                if (this.cambiosRepository == null)
                {
                    this.cambiosRepository = new CambiosRepository(context);
                }
                return cambiosRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
