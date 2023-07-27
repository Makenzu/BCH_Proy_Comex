using System;
using System.Data.Entity.Validation;
using System.Text;

namespace BCH.Comex.Data.DAL.Portal
{
    public class UnitOfWorkPortal : IDisposable
    {
        private bool disposed = false;
        private portalEntities context;
        private GruposAplicacionesRepository gruposAplicacionesRepository;
        private AplicacionesRepository aplicacionesRepository;
        private DatosUsuarioRepository datosUsuarioRepository;


        public UnitOfWorkPortal()
        {
            context = new portalEntities();
        }

        public GruposAplicacionesRepository GruposAplicacionesRepository
        {
            get 
            {
                if (this.gruposAplicacionesRepository == null)
                {
                    this.gruposAplicacionesRepository = new GruposAplicacionesRepository(context);
                }
                return this.gruposAplicacionesRepository;
            }
        }

        public AplicacionesRepository AplicacionesRepository
        {
            get
            {
                if (this.aplicacionesRepository == null)
                {
                    this.aplicacionesRepository = new AplicacionesRepository(context);
                }
                return this.aplicacionesRepository;
            }
        }

        public DatosUsuarioRepository DatosUsuarioRepository
        {
            get
            {
                if (this.datosUsuarioRepository == null)
                {
                    this.datosUsuarioRepository = new DatosUsuarioRepository(context);
                }
                return this.datosUsuarioRepository;
            }
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                StringBuilder builder = new StringBuilder();
                foreach (var eve in e.EntityValidationErrors)
                {
                    builder.AppendLine(string.Format("Entidad \"{0}\" en estado \"{1}\" tiene los siguientes errores de validación:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        builder.AppendLine(string.Format("- Propiedad: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }

                throw new Exception(builder.ToString(), e);
            }
        }

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
