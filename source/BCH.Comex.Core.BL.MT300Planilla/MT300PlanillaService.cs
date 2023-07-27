using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01.MT300Common;
using BCH.Comex.Core.Entities.Cext01.MT300Planilla;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Swift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.BL.MT300Planilla
{
    public class MT300PlanillaService : IDisposable
    {
        private readonly UnitOfWorkCext01 uow;
        private readonly UnitOfWorkSwift uowSwift;

        public MT300PlanillaService()
        {
            uow = new UnitOfWorkCext01();
            uowSwift = new UnitOfWorkSwift();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (uow != null)
            {
                uow.Dispose();
            }
            if (uowSwift != null)
            {
                uowSwift.Dispose();
            }
        }

        /// <summary>
        /// Inicia la aplicación con datos iniciales de configuración
        /// </summary>
        /// <param name="datosUsuario"></param>
        /// <returns></returns>
        public DatosGlobales Iniciar(IDatosUsuario datosUsuario)
        {
            using (Tracer tracer = new Tracer("MT300PlanillaService - Iniciar"))
            {
                DatosGlobales globales = new DatosGlobales();
                globales.DatosUsuario = datosUsuario;

                return globales;
            }
        }

        public IList<ResultadoArchivosMT300> BuscarArchivos(DateTime? fecha, int rowOffset, int pageSize, string sortOrder, out int totalRows)
        {
            var resultado = uow.Mt300PlanillaRepository.GetArchivosPag(fecha, rowOffset, pageSize);
            totalRows = uow.Mt300PlanillaRepository.GetArchivosTot(fecha);

            return resultado;
        }

        public ResumenArchivosMT300 TraeResumen(DateTime? fecha)
        {
            var resultado = uow.Mt300PlanillaRepository.GetResumen(fecha);

            return resultado;
        }

        public IList<ResultadoDetalleArchivosMT300> BuscarDetalleArchivo(Decimal? id_archivo, int rowOffset, int pageSize, string sortOrder, out int totalRows)
        {
            var resultado = uow.Mt300PlanillaRepository.GetDetalleArchivosPag(id_archivo, rowOffset, pageSize);
            totalRows = uow.Mt300PlanillaRepository.GetDetalleArchivosTot(id_archivo);

            return resultado;
        }

        public ResumenDetalleArchivosMT300 TraeDetalleResumen(Decimal? id_archivo)
        {
            var resultado = uow.Mt300PlanillaRepository.GetResumenDetalle(id_archivo);

            return resultado;
        }

        public DetalleMensajeMT300 GetDetalleArchivo(decimal id)
        {
            return uow.Mt300DetalleMensajeRepository.GetDetalleMensaje(id);
        }

        public List<ArchivoDetalle> ObtenerDetalleArchivo(decimal id)
        {

            List<ArchivoDetalle> registros = new List<ArchivoDetalle>();
            registros = this.uow.Mt300PlanillaRepository.getArchivoDetalle(id);
            return registros;
        }

        public DetalleMensajeMT300 SaveDetalleArchivo(decimal id_detalle,decimal safekeeping, string reference,DateTime bookedBy,DateTime valueDate,decimal rate,decimal amountMN,string codMonedaMN,decimal amountME,string codMonedaME )
        {
            DetalleMensajeMT300 detalleMensaje = new DetalleMensajeMT300();
            detalleMensaje.id_detalle = id_detalle;
            detalleMensaje.safekeeping = safekeeping;
            detalleMensaje.reference = reference;
            detalleMensaje.booked_by = bookedBy;
            detalleMensaje.value_date = valueDate;
            detalleMensaje.rate = rate;
            detalleMensaje.amount_mn = amountMN;
            detalleMensaje.codigo_moneda_mn = "CLP";//codMonedaMN;
            detalleMensaje.amount_me = amountME;
            detalleMensaje.codigo_moneda_me = codMonedaME;

            return uow.Mt300DetalleMensajeRepository.SaveDetalleMensaje(detalleMensaje);
        }

    }
}
