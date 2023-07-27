using BCH.Comex.Core.BL.SWEM;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BCH.Comex.UI.API.Controllers
{
    public class MensajeSwiftController : ApiController
    {
        private const string ConfigTamanioPaginaGridBusqueda = "ConsultaSwift.TamanioPaginaGridBusqueda";
        private SwiftMgr blSwem = new SwiftMgr();
        private const char ENVIADOS = 'E';
        private const char RECIBIDOS = 'R';

        public string Get(int idCasilla, DateTime fechaDesde, char filtro, DateTime? fechaHasta = null, short? pageSize = null, int? rowOffset = null)
        {
            fechaDesde = fechaDesde.Date;
            fechaHasta = fechaHasta.HasValue ? fechaHasta.Value.Date : fechaDesde.Date;
            var totalRows = 0;
            if (!pageSize.HasValue)
            {
                pageSize = short.Parse(ConfigurationManager.AppSettings[ConfigTamanioPaginaGridBusqueda]);
            }
            if (!rowOffset.HasValue)
            {
                rowOffset = 0;
            }
            if (filtro != ENVIADOS && filtro != RECIBIDOS) throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Debe de indicar un filtro R o S."));
            var resultado = blSwem.BuscarSwiftsPorCasillaYFechas(idCasilla, fechaDesde, fechaHasta.Value, filtro == ENVIADOS, null, out totalRows, rowOffset, pageSize, null);
            return JsonConvert.SerializeObject(new { Data = resultado, total = totalRows });
        }
    }
}
