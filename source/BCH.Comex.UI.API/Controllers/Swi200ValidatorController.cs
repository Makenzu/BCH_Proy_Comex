using BCH.Comex.Core.BL.SWI200;
using BCH.Comex.Data.DAL.Swift.DTO;
using BCH.Comex.UI.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BCH.Comex.UI.API.Controllers
{
    public class Swi200ValidatorController : ApiController
    {
        [HttpPost]
        public string Swi200Validator([FromBody] Swi200Input input)
        {
            try
            {
                new Swi200Service().VerificaSwift(new MensajeSwiftSwi200(input.IdMensaje, input.RutDigitador, input.Casilla, input.Moneda, input.Monto, input.TipoIngreso, input.MensajeSwift));
                return JsonConvert.SerializeObject(new { result = HttpStatusCode.OK, message = input });
            }
            catch (Swi200Exception ex)
            {
                return JsonConvert.SerializeObject(new { result = HttpStatusCode.BadRequest, message = ex.Message });
            }
        }
    }
}
