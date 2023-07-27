using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.SWG3;
using BCH.Comex.Core.BL.SWG3.Helpers;
using BCH.Comex.Core.Entities.Cext01.MT300Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace BCH.Comex.UI.Web.Areas.GeneracionMT300.Controllers
{
    public class ApiEnvioMT300Controller : ApiController
    {
        
        protected Swg3Service service = new Swg3Service();
        protected GenerarMT300Helper generarMT300Helper = new GenerarMT300Helper();
        protected TokenApiMT300Service tokenService = new TokenApiMT300Service();

        [AllowAnonymous]
        [Route("api/genera-swift-MT300")]
        [HttpPost]
        public IHttpActionResult GetGeneraMT300AutorizacionAutomatica()
        {

            GenerarMT300Result resultadoGeneracion = new GenerarMT300Result();

            using (Tracer tracer = new Tracer("Genera Swift Masivo mensajes MT300 desde API"))
            {
                try
                {   
                    IEnumerable<string> authzHeaders;
                    if (!Request.Headers.TryGetValues("client-id", out authzHeaders) || authzHeaders.Count() > 1)
                    {
                        tracer.TraceError("Header [client-id] no informado");
                        resultadoGeneracion.CodigoSalida = -3;
                        resultadoGeneracion.Mensaje = "No se recibio token de autenticacion";
                        return Content(HttpStatusCode.Unauthorized, resultadoGeneracion);
                    }
                    var token = authzHeaders.ElementAt(0);
                    if (!tokenService.ValidarToken(token))
                    {
                        tracer.TraceError("Error: Token invalido");
                        resultadoGeneracion.CodigoSalida = -2;
                        resultadoGeneracion.Mensaje = "Token invalido";
                        return Content(HttpStatusCode.Unauthorized, resultadoGeneracion);
                    }    

                    //obtener archivos a procesar
                    List<ArchivoDetalle> registros = service.ObtenerArchivoDetalleDesdeMesaMT300();
                    resultadoGeneracion = generarMT300Helper.GenerarSwiftMasivoMT300(registros, null, GenerarMT300Helper.modoAutomatico, GenerarMT300Helper.flujoMesaCambio);

                    if (resultadoGeneracion.CantTotal == 0) {

                        Mt300Bitacora mt300Bitacora = new Mt300Bitacora();
                        mt300Bitacora.usuario = "ejecucionAPI";
                        mt300Bitacora.tipo_movimiento = TipoMovimientoBitacora.generacion;
                        mt300Bitacora.resultado = "Se ejecuto la API sin encontrar registros para procesar";
                        service.registraBitacora(mt300Bitacora);
                    } else {
                        if (resultadoGeneracion.CantGenerados == 0)
                        {
                            Mt300Bitacora mt300Bitacora = new Mt300Bitacora();
                            mt300Bitacora.usuario = "ejecucionAPI";
                            mt300Bitacora.tipo_movimiento = TipoMovimientoBitacora.generacion;
                            mt300Bitacora.resultado = "Se ejecuto la API pero no se genero ningun mensaje swift";
                            service.registraBitacora(mt300Bitacora);
                        }
                    }

                    return Content(HttpStatusCode.OK, resultadoGeneracion);

                }
                catch (Exception ex)
                {
                    resultadoGeneracion.CodigoSalida = -1;
                    resultadoGeneracion.Mensaje = "Error al tratar de procesar los datos";
                    tracer.TraceError(string.Format("Error: {0}", ex.Message));

                    Mt300Bitacora mt300Bitacora = new Mt300Bitacora();
                    mt300Bitacora.usuario = "ejecucionAPI";
                    mt300Bitacora.tipo_movimiento = TipoMovimientoBitacora.generacion;
                    mt300Bitacora.resultado = "Error al tratar de procesar los datos desde API. Error: " + ex.Message;
                    service.registraBitacora(mt300Bitacora);

                    return Content(HttpStatusCode.Accepted, resultadoGeneracion);
                }

            }


        }

    }
}
