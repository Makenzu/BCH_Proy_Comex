using BCH.Comex.Core.BL.SWI102;
using BCH.Comex.Core.BL.SWI200;
using BCH.Comex.Core.BL.SWI300;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BCH.Comex.UI.API.Controllers
{
    public class SwiftController : ApiController
    {
        private MensajeSwiftService mensajeSwiftService = new MensajeSwiftService();
        private Swi200Service swi200Service = new Swi200Service();
        private Swi300Service swi300Service = new Swi300Service();

        // GET api/swift/5
        public string Get(char messageType, string sesion, string secuencia)
        {
            try
            {
                return JsonConvert.SerializeObject(new { result = mensajeSwiftService.DesencriptaMensajeSwift(messageType, sesion, secuencia) });
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
        }

        public string GetRecibido(int sesion, int secuencia)
        {
            return JsonConvert.SerializeObject(new { result = mensajeSwiftService.DesencriptaMensajeRecibido(sesion, secuencia) });
        }

        public string GetEnviado(int nroMensaje)
        {
            return JsonConvert.SerializeObject(new { result = mensajeSwiftService.DesencriptaMensajeEnviado(nroMensaje) });
        }

        public string GetCorrelativo()
        {
            return JsonConvert.SerializeObject(new { result = swi300Service.GetCorrelativo() });
        }

        [HttpPost]
        //public string Add(int rut_digita, string txt_mensaje, int casilla, string moneda, float monto, string ch_rutdigita)
        public string Swi200(string mensajeSwift)
        {
            //success = "", error = ErrorMessage
            return JsonConvert.SerializeObject(new { result = swi200Service.Swi200(mensajeSwift) });
        }
    }
}
