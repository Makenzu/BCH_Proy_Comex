using System;

namespace BCH.Comex.Data.DAL.Sbcor
{
    public class UnitOfWorkSbcor : IDisposable
    {
        private sbcorEntities context = new sbcorEntities();
        private BancoRepository bancoRepository;
        private PaisRepository paisRepository;

        public BancoRepository BancoRepository
        {
            get
            {

                if (this.bancoRepository == null)
                {
                    this.bancoRepository = new BancoRepository(context);
                }
                return bancoRepository;
            }
        }

        public PaisRepository PaisRepository
        {
            get
            {

                if (this.paisRepository == null)
                {
                    this.paisRepository = new PaisRepository(context);
                }
                return paisRepository;
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
