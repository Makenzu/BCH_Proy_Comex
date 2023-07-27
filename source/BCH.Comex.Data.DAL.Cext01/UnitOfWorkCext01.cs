using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Portal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;


namespace BCH.Comex.Data.DAL.Cext01
{
    public class UnitOfWorkCext01 : IDisposable
    {
        private bool disposed = false;
        private cext01Entities context;
        private SceRepository sceRepository;
        private SgtRepository sgtRepository;
        private UsuarioRepository usuarioRepository;
        private DocumentoOperacionRepository docsRepository;
        private PrtyEjcMSRepository prtyEjcMSRepository;
        private BancoRepository bancoRepository;
        private DbContextTransaction transaction;
        private AdministracionRepository administracionRepository;
        private ParametroComexRepository parametroComexRepository;
        private ArchivosRepository archivosRepository;
        private ArchivosDetalleRepository archivosDetalleRepository;
        private Mt300BitacoraRepository mt300BitacoraRepository;
        private Mt300CustodiaRepository mt300CustodiaRepository;
        private Mt300ArchivosProcesadosRepository mt300ArchivosProcesadosRepository;
        private Mt300GestionRepository mt300GestionRepository;
        private Mt300PlanillaRepository mt300PlanillaRepository;
        private Mt300DetalleMensajeRepository mt300DetalleMensajeRepository;

        public AdministracionRepository AdministracionRepository
        {
            get
            {
                if (administracionRepository == null)
                {
                    administracionRepository = new AdministracionRepository(context);
                }
                return administracionRepository;
            }
        }

        public UnitOfWorkCext01()
        {
            context = new cext01Entities();
        }

        public SceRepository SceRepository        
        {
            get 
            {
                if (this.sceRepository == null)
                {
                    this.sceRepository = new SceRepository(context);
                }
                return this.sceRepository;
            }
        }

        public SgtRepository SgtRepository
        {
            get
            {
                if (this.sgtRepository == null)
                {
                    this.sgtRepository = new SgtRepository(context);
                }
                return this.sgtRepository;
            }
        }

        public UsuarioRepository UsuarioRepository
        {
            get
            {
                if (this.usuarioRepository == null)
                {
                    this.usuarioRepository = new UsuarioRepository(context);
                }
                return this.usuarioRepository;
            }
        }

        public DocumentoOperacionRepository DocumentosOperacionesRepository
        {
            get
            {
                if (this.docsRepository == null)
                {
                    this.docsRepository = new DocumentoOperacionRepository(context);
                }
                return this.docsRepository;
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
                return this.bancoRepository;
            }
        }

        public ParametroComexRepository ParametroComexRepository
        {
            get
            {
                if (this.parametroComexRepository == null)
                {
                    this.parametroComexRepository = new ParametroComexRepository(context);
                }
                return this.parametroComexRepository;
            }
        }

        public ArchivosRepository ArchivosRepository
        {
            get
            {
                if (this.archivosRepository == null)
                {
                    this.archivosRepository = new ArchivosRepository(context);
                }
                return this.archivosRepository;
            }
        }

        public ArchivosDetalleRepository ArchivosDetalleRepository
        {
            get
            {
                if (this.archivosDetalleRepository == null)
                {
                    this.archivosDetalleRepository = new ArchivosDetalleRepository(context);
                }
                return this.archivosDetalleRepository;
            }
        }

        public Mt300BitacoraRepository Mt300BitacoraRepository
        {
            get
            {
                if (this.mt300BitacoraRepository == null)
                {
                    this.mt300BitacoraRepository = new Mt300BitacoraRepository(context);
                }
                return this.mt300BitacoraRepository;
            }
        }

        public Mt300CustodiaRepository Mt300CustodiaRepository
        {
            get
            {
                if (this.mt300CustodiaRepository == null)
                {
                    this.mt300CustodiaRepository = new Mt300CustodiaRepository(context);
                }
                return this.mt300CustodiaRepository;
            }
        }

        public Mt300ArchivosProcesadosRepository Mt300ArchivosProcesadosRepository
        {
            get
            {
                if (this.mt300ArchivosProcesadosRepository == null)
                {
                    this.mt300ArchivosProcesadosRepository = new Mt300ArchivosProcesadosRepository(context);
                }
                return this.mt300ArchivosProcesadosRepository;
            }
        }

        public Mt300GestionRepository Mt300GestionRepository
        {
            get
            {
                if (this.mt300GestionRepository == null)
                {
                    this.mt300GestionRepository = new Mt300GestionRepository(context);
                }
                return this.mt300GestionRepository;
            }
        }

        public Mt300PlanillaRepository Mt300PlanillaRepository
        {
            get
            {
                if (this.mt300PlanillaRepository == null)
                {
                    this.mt300PlanillaRepository = new Mt300PlanillaRepository(context);
                }
                return this.mt300PlanillaRepository;
            }
        }

        public Mt300DetalleMensajeRepository Mt300DetalleMensajeRepository
        {
            get
            {
                if (this.mt300DetalleMensajeRepository == null)
                {
                    this.mt300DetalleMensajeRepository = new Mt300DetalleMensajeRepository(context);
                }
                return this.mt300DetalleMensajeRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
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

        public void BeginTransaction()
        {
            this.transaction = this.context.Database.BeginTransaction();
        }

        public void EndTransaction()
        {
            this.transaction.Dispose();
        }

        public void Rollback()
        {
            this.transaction.Rollback();
        }
        public void Commit()
        {
            this.transaction.Commit();
        }

        public void CommitUnderlyingTransaction()
        {
            context.Database.CurrentTransaction.Commit();
        }

        /// <summary>
        /// Actualiza los datos de impresion del usuario
        /// </summary>
        /// <param name="datosUsuario"></param>
        public void ActualizarImpresion(IDatosUsuario datosUsuario)
        {
            try
            {
                context.pro_sce_datusr_u01_MS(datosUsuario.samAccountName,
                    datosUsuario.ConfigImpres_ImprimeCartas, datosUsuario.ConfigImpres_ImprimePlanillas,
                    datosUsuario.ConfigImpres_ImprimeReporte);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la opción", ex);
            }
        }
        public IList<sce_mch_s14_MS_DTO> GetContabilidadVigente(string CentroCosto, string Especialidad)
        {
            return context.Database.SqlQuery<sce_mch_s14_MS_DTO>("exec sce_mch_s14_MS  @cencos,@codusr",
            new SqlParameter("cencos", CentroCosto), new SqlParameter("codusr", Especialidad)).ToList<sce_mch_s14_MS_DTO>();
        }

        public IList<sce_mts_s02_MS_Result> SyGet_Mts(decimal rut, DateTime fecha)
        {
            return context.sce_mts_s02_MS(rut, fecha).ToList();
        }
        public int EliminaMT(string codcct, string codesp, string codofi, string codope, string codpro, string glosa_estado, string mensaje_error, decimal? ncorr, string referencia, string tipo_mt, string tipo_mt_decimal)
        {
            decimal? numneg=0;
            decimal? tippro=0;
            decimal? numcpa=0;
            decimal? numcuo=0;
            decimal? numcob=0;
            int? estado = 8;       
            return (int)(context.sce_mts_u01_MS(codcct, codpro, codesp, codofi, codope, numneg, tippro, numcpa, numcuo, numcob, ncorr, estado).FirstOrDefault() ?? -1);

        }
        public IList<sce_usr_s16_MS_DTO> GetPassword()
        {
            string centroCosto = "912";
            string Especia = "20";
            return context.Database.SqlQuery<sce_usr_s16_MS_DTO>("exec sce_usr_s16_MS  @cent_costo,@id_especia",
            new SqlParameter("cent_costo", centroCosto), new SqlParameter("id_especia", Especia)).ToList<sce_usr_s16_MS_DTO>();
        }

        public IList<sce_cuadra_inyecciones_ctacte_MS_DTO> Frm(string CentroCosto, string Especialidad, DateTime fecmov)
        {
            return context.Database.SqlQuery<sce_cuadra_inyecciones_ctacte_MS_DTO>("exec sce_cuadra_inyecciones_ctacte_MS  @cencos,@codusr,@fecmov",
            new SqlParameter("cencos", CentroCosto), new SqlParameter("codusr", Especialidad), new SqlParameter("fecmov", fecmov)).ToList<sce_cuadra_inyecciones_ctacte_MS_DTO>();
        }

        public IList<sce_mcd_s71_MS_Result> sce_mcd_s71(string CentroCosto, string Especialidad, string rutais, DateTime fecmov)
        {
            return context.Database.SqlQuery<sce_mcd_s71_MS_Result>("exec sce_mcd_s71_MS  @cencos,@codusr,@rutais,@fecmov",
            new SqlParameter("cencos", CentroCosto), new SqlParameter("codusr", Especialidad), new SqlParameter("rutais", rutais), new SqlParameter("fecmov", fecmov)).ToList<sce_mcd_s71_MS_Result>();
        }

        /// <summary>
        /// Actualiza los datos de impresion del usuario Contabilidad Generica
        /// </summary>
        /// <param name="datosUsuario"></param>
        public void ActualizarImpresionContabilidadGenerica(IDatosUsuario datosUsuario)
        {
            try
            {
                context.pro_sce_datusr_u02_MS(datosUsuario.samAccountName,
                    datosUsuario.ConfigImpres_ContabilidadGenerica);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la opción", ex);
            }
        }


    }
}
