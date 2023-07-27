using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.XGSV;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.UI.Web.Areas.Supervisor.Models;
using BCH.Comex.UI.Web.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Supervisor.Controllers
{
    [SupervisorExigirInicioDia]
    public class InyectarYReversarController : BaseController
    {
        public ActionResult Index()
        {
            using (Tracer tracer = new Tracer())
            {
                InyectarYReversarViewModel model = new InyectarYReversarViewModel();
                model.CargosYAbonosParaInyectar = GetCargosYAbonosParaUsuarioLogeado(XgsvService.OperacionInyeccion.Inyectar);
                model.CargosYAbonosParaReversar = GetCargosYAbonosParaUsuarioLogeado(XgsvService.OperacionInyeccion.Reversar);
                return View(model);
            }
        }

        public ActionResult BuscarCargosYAbonos(byte operacion, bool ignorarRequest)
        {
            using (Tracer tracer = new Tracer())
            {
                if (!ignorarRequest)
                {
                    if (operacion == 0 || operacion == 1)
                    {
                        var jsonResult = new JsonResult()
                        {
                            Data = GetCargosYAbonosParaUsuarioLogeado((XgsvService.OperacionInyeccion)operacion),
                            MaxJsonLength = Int32.MaxValue,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };

                        return jsonResult;
                    }
                    else
                    {
                        throw new ArgumentException("La operación enviada no es válida");
                    }
                }
                else
                {
                    return new EmptyResult();
                }
            }
        }

        [AcceptVerbs(HttpVerbs.Get), FileDownload]
        public ActionResult ExcelCargosYAbonos()
        {
            using (Tracer tracer = new Tracer())
            {
                IList<AbonoCargoResultDTO> paraInyectar = GetCargosYAbonosParaUsuarioLogeado(XgsvService.OperacionInyeccion.Inyectar);
                IList<AbonoCargoResultDTO> paraReversar = GetCargosYAbonosParaUsuarioLogeado(XgsvService.OperacionInyeccion.Reversar);

                MemoryStream stream = service.GetExcelDeAbonosYCargos(paraInyectar, paraReversar);
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte.xlsx");
            }
        }


        private IList<AbonoCargoResultDTO> GetCargosYAbonosParaUsuarioLogeado(XgsvService.OperacionInyeccion operacion)
        {
            using (Tracer tracer = new Tracer())
            {
                DatosGlobales datos = new DatosGlobales();

                int centroCostoSupervisor = int.Parse(this.Globales.UsrEsp.cent_costo);
                string idSupervisor = this.Globales.UsrEsp.id_especia;

                IList<AbonoCargoResultDTO> abonosCargos = service.GetAbonosCargos(centroCostoSupervisor, idSupervisor, operacion);
                GuardarCargosYAbonosEnElCache(abonosCargos, operacion);
                return abonosCargos;
            }
        }

        [HandleAjaxException]
        public ActionResult InyectarCargoYAbono(Guid idAux)
        {
            using (Tracer tracer = new Tracer())
            {
                AbonoCargoResultDTO ac = this.GetCargoYAbonoDeCache(idAux, XgsvService.OperacionInyeccion.Inyectar);

                List<UI_Message> menssages = service.InyectarOReversarCargoAbono(ac, XgsvService.OperacionInyeccion.Inyectar, this.Globales.DatosUsuario.Identificacion_Rut);

                var data = new
                {
                    Mensajes = menssages
                };

                var jsonResult = new JsonResult()
                {
                    Data = data,
                    MaxJsonLength = Int32.MaxValue,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                return jsonResult;
            }
        }

        [HandleAjaxException]
        public ActionResult ReversarCargoYAbono(Guid idAux)
        {
            using (Tracer tracer = new Tracer())
            {
                AbonoCargoResultDTO ac = this.GetCargoYAbonoDeCache(idAux, XgsvService.OperacionInyeccion.Reversar);
                List<UI_Message> menssages = service.InyectarOReversarCargoAbono(ac, XgsvService.OperacionInyeccion.Reversar, this.Globales.DatosUsuario.Identificacion_Rut);

                var data = new
                {
                    Mensajes = menssages
                };

                var jsonResult = new JsonResult()
                {
                    Data = data,
                    MaxJsonLength = Int32.MaxValue,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                return jsonResult;
            }
        }

        /// <summary>
        /// Obtener información de la grillas guardadas en el historial
        /// </summary>
        /// <param name="idAux">Numero de la TRX ID</param>
        /// <param name="operacion">Tipo de Operación</param>
        /// <returns></returns>
        private AbonoCargoResultDTO GetCargoYAbonoDeCache(Guid idAux, XgsvService.OperacionInyeccion operacion)
        {
            using (Tracer tracer = new Tracer("Inicia GetCargoYAbonoDeCache"))
            {
                AbonoCargoResultDTO ac = null;

                if (idAux != Guid.Empty)
                {
                    string key = GetKeyDeCacheParaOperacion(operacion);
                    if (this.ControllerContext.HttpContext.Session[key] != null)
                    {
                        IList<AbonoCargoResultDTO> abonosCargos = this.ControllerContext.HttpContext.Session[key] as IList<AbonoCargoResultDTO>;
                        ac = abonosCargos.Where(x => x.IdAux == idAux).FirstOrDefault();
                        // Actualizar el TRX de la operación
                        service.UpdateCargoYAbono(ref ac);
                    }
                }

                if (ac == null)
                {
                    throw new ArgumentException("El id del cargo/abono enviado no es válido");
                }
                else return ac;
            }
        }

        private void GuardarCargosYAbonosEnElCache(IList<AbonoCargoResultDTO> acs, XgsvService.OperacionInyeccion operacion)
        {
            this.ControllerContext.HttpContext.Session[GetKeyDeCacheParaOperacion(operacion)] = acs;
        }

        private string GetKeyDeCacheParaOperacion(XgsvService.OperacionInyeccion op)
        {
            return (op == XgsvService.OperacionInyeccion.Inyectar ? SessionKeys.FundTransfer.CargosYAbonosAInyectarKey : SessionKeys.FundTransfer.CargosYAbonosAReversarKey);
        }

        [HttpPost]
        public ActionResult VolverInyectarYReversar(InyectarYReversarViewModel pive, string Command)
        {
            using (Tracer tracer = new Tracer())
            {
                this.Globales.ListaMensajesError.Clear();
                switch (Command)
                {
                    case "Volver":
                        return new RedirectResult("~/Supervisor");
                }
                pive = new InyectarYReversarViewModel();
                return View(pive);
            }
        }
    }
}
