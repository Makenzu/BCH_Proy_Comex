using BCH.Comex.Common.Exceptions;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.BL.XGPL
{
    public static class MODGUSR
    {
        /// <summary>
        /// Valida que el usuario haga hecho inicio de dia, y no haya hecho fin de dia. Desde la tabla sce_usr
        /// </summary>
        /// <param name="MODGUSR"></param>
        /// <param name="Mdi_Principal"></param>
        /// <param name="unit"></param>
        /// <param name="cencos"></param>
        /// <param name="codusr"></param>
        /// <param name="etapa"></param>
        /// <returns></returns>
        public static void ValidarInicioFinDia(string centroDeCosto, string codigoUsuario, XgplService service)
        {
            using (var tracer = new Tracer())
            {
                sce_usr_s04_MS_Result fechas;

                try
                {
                    fechas = service.GetFechasUsuario(centroDeCosto, codigoUsuario); 
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, no se ha podido obtener las fechas de incio y fin de dia del usuario", ex);
                    throw new ComexUserException("Alerta, no se ha podido obtener las fechas de incio y fin de dia del usuario");
                }

                if (fechas == null)
                {
                    throw new ComexUserException("No fue posible saber si el usuario hizo inicio o fin de día");
                }

                //Rescata los datos.-
                DateTime FechaIni = fechas.fec_ini;
                DateTime FechaFin = fechas.fec_fin;
                DateTime FechaOut = fechas.fec_out;

                tracer.AddToContext("fec_ini", fechas.fec_ini);
                tracer.AddToContext("fec_fin", fechas.fec_ini);
                tracer.AddToContext("fec_out", fechas.fec_ini);

                if (FechaIni.Date == FechaOut.Date)
                {
                    throw new ComexUserException("Ya se ha efectuado el Cierre Diario de Comercio Exterior. No podrá utilizar esta aplicación.");
                }
                if (FechaIni < FechaFin)
                {
                    throw new ComexUserException("Antes de Operar con las aplicaciones debe ejecutar el Proceso de Inicio de Dia.");
                }                
            }
        }
    }
}
