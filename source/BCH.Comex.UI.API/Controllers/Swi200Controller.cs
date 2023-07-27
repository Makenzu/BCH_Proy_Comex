using BCH.Comex.Core.BL.SWI200;
using BCH.Comex.UI.API.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BCH.Comex.UI.API.Controllers
{
    public class Swi200Controller : ApiController
    {
        [HttpPost]
        public string Swi200([FromBody] Swi200Input input)
        {
            try
            {
                var result = new Swi200Service().IngresaModificaMensajeSwift(input.Vista, input.IdMensaje, input.RutDigitador, input.Casilla, input.Moneda, input.Monto, input.TipoIngreso, input.MensajeSwift);
                return JsonConvert.SerializeObject(new { result = input.IdMensaje });
            }
            catch (Swi200Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
            
        }
    }
}
