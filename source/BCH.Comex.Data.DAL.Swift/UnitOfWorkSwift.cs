using BCH.Comex.Core.Entities.Swift;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Data.DAL.Swift
{
    public class UnitOfWorkSwift : IDisposable
    {
        private swiftEntities context = new swiftEntities();
        private BancoRepository bancoRepository;
        private MensajeRepository mensajeRepository;
        private CasillaRepository casillaRepository;
        private TiposMensajeRepository tiposMensajeRepository;
        private TipoCampoRepository tipoCampoRepository;
        private CamposMsgRepository camposMsgRepository;
        private ValorCamposRepository valorCamposRepository;
        private CaracterErrorRepository caracterErrorRepository;                        
        private MonedaRepository monedaRepository;
        private FirmaRepository firmaRepository;
        private SwRepository swRepository;
        private AdministracionRepository administracionRepository;
        private GestionControlRepository gestionControlRepository;
        private MantencionSwiftRepository mantencionSwiftRepository;
        private PaymentPlusRepository paymentPlusRepository;

        public AdministracionRepository AdministracionRepository
        {
            get
            {
                if (this.administracionRepository == null)
                {
                    this.administracionRepository = new AdministracionRepository(context);
                }
                return this.administracionRepository;
            }
        }

        public MantencionSwiftRepository MantencionSwiftRepository
        {
            get
            {
                if (this.mantencionSwiftRepository == null)
                {
                    this.mantencionSwiftRepository = new MantencionSwiftRepository(context);
                }
                return mantencionSwiftRepository;
            }
        }

        public GestionControlRepository GestionControlRepository
        {
            get
            {
                if (this.gestionControlRepository == null)
                {
                    this.gestionControlRepository = new GestionControlRepository(context);
                }
                return this.gestionControlRepository;
            }
        }

        public SwRepository SwRepository
        {
            get
            {
                if (this.swRepository == null)
                {
                    this.swRepository = new SwRepository(context);
                }
                return swRepository;
            }
        }

        public MonedaRepository MonedaRepository
        {
            get
            {
                if (this.monedaRepository == null)
                {
                    this.monedaRepository = new MonedaRepository(context);
                }
                return monedaRepository;
            }
        }
        
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

        public CasillaRepository CasillaRepository
        {
            get
            {

                if (this.casillaRepository == null)
                {
                    this.casillaRepository = new CasillaRepository(context);
                }
                return casillaRepository;
            }
        }

        public TiposMensajeRepository TiposMensajeRepository
        {
            get
            {

                if (this.tiposMensajeRepository == null)
                {
                    this.tiposMensajeRepository = new TiposMensajeRepository(context);
                }
                return tiposMensajeRepository;
            }
        }

        public MensajeRepository MensajeRepository
        {
            get
            {

                if (this.mensajeRepository == null)
                {
                    this.mensajeRepository = new MensajeRepository(context);
                }
                return mensajeRepository;
            }
        }

        public CamposMsgRepository CamposMsgRepository
        {
            get
            {

                if (this.camposMsgRepository == null)
                {
                    this.camposMsgRepository = new CamposMsgRepository(context);
                }
                return camposMsgRepository;
            }
        }

        public TipoCampoRepository TipoCampoRepository
        {
            get
            {

                if (this.tipoCampoRepository == null)
                {
                    this.tipoCampoRepository = new TipoCampoRepository(context);
                }
                return tipoCampoRepository;
            }
        }
		
        public ValorCamposRepository ValorCamposRepository
        {
            get
            {

                if (this.valorCamposRepository == null)
                {
                    this.valorCamposRepository = new ValorCamposRepository(context);
                }
                return valorCamposRepository;
            }
        }

        public CaracterErrorRepository CaracterErrorRepository
        {
            get
            {

                if (this.caracterErrorRepository == null)
                {
                    this.caracterErrorRepository = new CaracterErrorRepository(context);
                }
                return caracterErrorRepository;
            }
        }     
   
        public FirmaRepository FirmaRepository
        {
            get
            {
                if(this.firmaRepository == null)
                {
                    this.firmaRepository = new FirmaRepository(context);
                }
                return this.firmaRepository;
            }
        }

        public PaymentPlusRepository PaymentPlusRepository
        {
            get
            {
                if (this.paymentPlusRepository == null)
                {
                    this.paymentPlusRepository = new PaymentPlusRepository(context);
                }
                return this.paymentPlusRepository;
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


        public IList<proc_sw_env_msg_por_rut_MS_Result> SyGet_Swift(int rut, DateTime fecha)
        {
            return context.proc_sw_env_msg_por_rut_MS(rut, fecha).ToList();
        }
    }
}
