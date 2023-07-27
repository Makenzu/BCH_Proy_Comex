using BCH.Comex.Core.BL.SWEM.Datatypes;
using BCH.Comex.Core.BL.SWRE;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using BCH.Comex.UI.Web.Models.RecibirMensaje;
using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BCH.Comex.Core.BL.SWGC;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.UI.Web.Models;
using Newtonsoft.Json;

namespace BCH.Comex.UI.Web.Controllers
{
    public class RecibirMensajeController : BCH.Comex.UI.Web.Common.BaseController
    {
        private const string CookieName = "BCHComexSwre_Casillas";
        private const string CookieValueCasillasVisibles = "CasillasVisibles";
        private const string CookieValueCasillaDefault = "CasillaDefault";
        private const string CacheCasillasSwiftTodas = "CasillasSwiftTodas";
        private SwreService bl;
        private static readonly DateTime SqlSmallDateTimeMinValue = new DateTime(1900, 01, 01, 00, 00, 00);

        static RecibirMensajeController()
        {
            new PortalService().RegisterApp("SWRE", "Recepción de Mensajeria Swift - Configurador", "SWIFT",
                Constantes.AppRoles.RecibirMensajeSwiftAppRole, "COMEX_GRP_SWIFT", "RecibirMensaje");
        }

        public RecibirMensajeController()
        {
            bl = new SwreService();
        }

        public InitialBotones InitBotones
        {
            get
            {
                InitialBotones InitBotones = (InitialBotones)Session[SessionKeys.ConsultaSwift.InitializationBotonesKey];

                return InitBotones;
            }
            set
            {
                Session[SessionKeys.ConsultaSwift.InitializationBotonesKey] = value;
            }
        }

        public ActionResult Index()
        {
            IDatosUsuario usuario = HttpContext.GetCurrentUser().GetDatosUsuario();
            
            Int32 rutUsuario = int.Parse(usuario.Identificacion_Rut);

            this.InitBotones = (InitialBotones)Session[SessionKeys.ConsultaSwift.InitializationBotonesKey];

            if (this.InitBotones == null) //primera vez que entro al sistema
            {
                this.InitBotones = bl.RecepcionMensajeriaSwiftInit();
            }

            InitBotones.usuario.rutUsuaro = rutUsuario;
            bool estado = bl.ExisteConfiguraCasilla(InitBotones.usuario.rutUsuaro);
            InitBotones.Mdi_Principal.Opciones[0].Enabled = estado;
            ViewBag.MdiPrincipal = InitBotones.Mdi_Principal;
            //return View();
            //ViewBag.MdiPrincipal = InitBotones.Mdi_Principal;
            IEnumerable<sw_casillas> casillas = GetCasillasDeCacheOBL();
            SelectList todasLasCasillas = new SelectList(casillas.OrderBy(i => i.cod_casilla), "cod_casilla", "DataTextField", HttpContext.GetCurrentUser().GetDatosUsuario().Identificacion_CentroDeCostosOriginal);
            var model = new IndexModel();
            model.TodasLasCasillas = todasLasCasillas;
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains(CookieName))
            {
                //leo las casillas visibles y la casilla default de la cookie
                NameValueCollection collection = this.ControllerContext.HttpContext.Request.Cookies[CookieName].Values;
                model.IdsCasillasVisibles = collection[CookieValueCasillasVisibles].Split(new char[] { ',' }).ToList();

                IEnumerable<sw_casillas> casillasVisibles = casillas.Where(c => model.IdsCasillasVisibles.Contains(c.cod_casilla.ToString())).ToList();
                model.IdCasillaDefault = collection[CookieValueCasillaDefault];
                int valorDefault = 0;
                if(!int.TryParse(model.IdCasillaDefault, out valorDefault))
                {
                     model.IdCasillaDefault = usuario.Identificacion_CentroDeCostosOriginal;
                }

                model.CasillasVisibles = new SelectList(casillasVisibles, "cod_casilla", "DataTextField", valorDefault);
            }
            else
            {
                model.IdCasillaDefault = usuario.Identificacion_CentroDeCostosOriginal;
            }

            return View(model);
        }

        public ActionResult ReporteRecepcion(int casilla, DateTime fechaDesde, DateTime fechaHasta, string estado)
        {
            IList<ReporteItem> result;
            string tipoMensaje;
            var casillas = bl.GetTodasLasCasillas();
            var casillaSeleccionada = casillas.Where(i => i.cod_casilla == casilla).Select(i => i.nombre_casilla).FirstOrDefault();
            if (estado == "REC")
            {
                fechaDesde = SqlSmallDateTimeMinValue;
                fechaHasta = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                tipoMensaje = "Pendientes";
                result = bl.ListaPendiente(casilla, fechaDesde, fechaHasta).Select(i => new ReporteItem { 
                    Sesion = i.sesion, 
                    Secuencia = i.secuencia, 
                    Beneficiario = i.beneficiario, 
                    Moneda = i.cod_moneda, 
                    Monto = i.monto.GetValueOrDefault(), 
                    Prioridad = i.prioridad, 
                    Referencia = i.referencia, 
                    TipoMt = i.tipo_msg,
                    BancoEmisor = i.cod_banco_em.Trim() + i.branch_em.Trim(),
                    Fecha = i.fecha_enc + " " + i.hora_enc
                }).ToList();
            }
            else if (estado == "CNR")
            {
                tipoMensaje = "Confirmados";
                result = bl.ListaConfirmados(casilla, fechaDesde, fechaHasta).Select(i => new ReporteItem
                {
                    Sesion = i.sesion,
                    Secuencia = i.secuencia,
                    Beneficiario = i.beneficiario,
                    Moneda = i.cod_moneda,
                    Monto = i.monto.GetValueOrDefault(),
                    Prioridad = i.prioridad,
                    Referencia = i.referencia,
                    TipoMt = i.tipo_msg,
                    BancoEmisor = i.nombre_banco,
                    Fecha = i.fecha_rcb + " " + i.hora_rcb
                }).ToList();
            }
            else if (estado == "CNF")
            {
                tipoMensaje = "Confirmados";
                result = bl.ListaConfirmados(casilla, fechaDesde, fechaHasta).Select(i => new ReporteItem
                {
                    Sesion = i.sesion,
                    Secuencia = i.secuencia,
                    Beneficiario = i.beneficiario,
                    Moneda = i.cod_moneda,
                    Monto = i.monto.GetValueOrDefault(),
                    Prioridad = i.prioridad,
                    Referencia = i.referencia,
                    TipoMt = i.tipo_msg,
                    BancoEmisor = i.nombre_banco,
                    Fecha = i.fecha_rcb + " " + i.hora_rcb
                }).ToList();
            }
            else if (estado == "IMR")
            {
                tipoMensaje = "Impresos";
                result = bl.ListaImpresos(casilla, fechaDesde, fechaHasta).Select(i => new ReporteItem
                {
                    Sesion = i.sesion,
                    Secuencia = i.secuencia,
                    Beneficiario = i.beneficiario,
                    Moneda = i.cod_moneda,
                    Monto = i.monto.GetValueOrDefault(),
                    Prioridad = i.prioridad,
                    Referencia = i.referencia,
                    TipoMt = i.tipo_msg,
                    BancoEmisor = i.nombre_banco,
                    Fecha = i.fecha_imp + " " + i.hora_imp
                }).ToList();
            }
            else
            {
                tipoMensaje = "Reenviados";
                result = bl.ListaReenviados(casilla, fechaDesde, fechaHasta).Select(i => new ReporteItem
                {
                    Sesion = i.sesion,
                    Secuencia = i.secuencia,
                    Beneficiario = i.beneficiario,
                    Moneda = i.cod_moneda,
                    Monto = i.monto.GetValueOrDefault(),
                    Prioridad = i.prioridad,
                    Referencia = i.referencia,
                    TipoMt = i.tipo_msg,
                    BancoEmisor = i.nombre_banco,
                    Fecha = i.fecha_ree + " " + i.hora_ree
                }).ToList();
            }
            var model = new ReporteModel
            {
                Casilla = casillaSeleccionada,
                Estado = estado,
                FechaInicio = fechaDesde,
                FechaFin = fechaHasta,
                Result = result,
                TipoMensaje = tipoMensaje
            };
            return View(model);
        }

        public ActionResult ListaMensajesPendiente(int? Casilla, DateTime fechaDesde, DateTime fechaHasta)
        {
            ViewBag.MdiPrincipal = InitBotones.Mdi_Principal;
            var casilla = Casilla ?? 0;
                        
            var result = bl.ListaPendiente(casilla, fechaDesde, fechaHasta); 
            var jsonResult = Json(result.Select(i => new
            {
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Casilla = i.casilla,
                NombreCasilla = i.nombre_casilla,
                TipoMsg = i.tipo_msg,
                NombreTipo = i.nombre_tipo,
                Prioridad = i.prioridad,
                EstadoMsg = i.estado_msg,
                FechaEnv = i.fecha_env,
                FechaEnc = i.fecha_enc,
                HoraEnv = i.hora_env,
                HoraEnc = i.hora_enc,
                CodBancoRec = i.cod_banco_rec,
                BranchRec = i.branch_rec,
                CodBancoEm = i.cod_banco_em,
                BranchEm = i.branch_em,
                NombreBanco = i.nombre_banco,
                CiudadBanco = i.ciudad_banco,
                PaisBanco = i.pais_banco,
                OficinaBanco = i.oficina_banco,
                CodMoneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                TotalImp = i.total_imp,
                Comentario = i.comentario,
                Encasillamiento = i.fecha_enc + " " + i.hora_enc
            }).ToList(), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ListaMensajesConfirmados(int Casilla, DateTime fechaDesde, DateTime fechaHasta)
        {
            ViewBag.MdiPrincipal = InitBotones.Mdi_Principal;

            //object data = bl.BuscarListaMensajesConfirmados(Casilla, Convert.ToDateTime(fechaDesde), Convert.ToDateTime(fechaHasta));
            var result = bl.ListaConfirmados(Casilla, fechaDesde, fechaHasta);
            var jsonResult = Json(result.Select(i => new
            {
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Casilla = i.casilla,
                NombreCasilla = i.nombre_casilla,
                TipoMsg = i.tipo_msg,
                NombreTipo = i.nombre_tipo,
                Prioridad = i.prioridad,
                EstadoMsg = i.estado_msg,
                FechaEnc = i.fecha_enc,
                FechaRcb = i.fecha_rcb,
                HoraEnc = i.hora_enc,
                HoraRcb = i.hora_rcb,
                CodBancoRec = i.cod_banco_rec,
                BranchRec = i.branch_rec,
                CodBancoEm = i.cod_banco_em,
                BranchEm = i.branch_em,
                NombreBanco = i.nombre_banco,
                CiudadBanco = i.ciudad_banco,
                PaisBanco = i.pais_banco,
                OficinaBanco = i.oficina_banco,
                CodMoneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                TotalImp = i.total_imp,
                Comentario = i.comentario,
                Confirmacion = i.fecha_rcb + " " + i.hora_rcb
            }).ToList(), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult ListaMensajesImpresos(int Casilla, DateTime fechaDesde, DateTime fechaHasta)
        {
            ViewBag.MdiPrincipal = InitBotones.Mdi_Principal;

            //object data = bl.BuscarListaMensajesImpresos(Casilla, Convert.ToDateTime(fechaDesde), Convert.ToDateTime(fechaHasta));
            var result = bl.ListaImpresos(Casilla, fechaDesde, fechaHasta);
            var jsonResult = Json(result.Select(i => new
            {
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Casilla = i.casilla,
                NombreCasilla = i.nombre_casilla,
                TipoMsg = i.tipo_msg,
                NombreTipo = i.nombre_tipo,
                Prioridad = i.prioridad,
                EstadoMsg = i.estado_msg,
                FechaEnc = i.fecha_enc,
                FechaImp = i.fecha_imp + " " + i.hora_imp,
                HoraEnc = i.hora_enc,
                HoraImp = i.hora_imp,
                CodBancoRec = i.cod_banco_rec,
                BranchRec = i.branch_rec,
                CodBancoEm = i.cod_banco_em,
                BranchEm = i.branch_em,
                NombreBanco = i.nombre_banco,
                CiudadBanco = i.ciudad_banco,
                PaisBanco = i.pais_banco,
                OficinaBanco = i.oficina_banco,
                CodMoneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                TotalImp = i.total_imp,
                Comentario = i.comentario
            }).ToList(), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult ListaMensajesReenviados(int? Casilla, DateTime? fechaDesde, DateTime fechaHasta)
        {
            ViewBag.MdiPrincipal = InitBotones.Mdi_Principal;

            var casilla = Casilla ?? 0;
            fechaDesde = fechaDesde == null ? SqlSmallDateTimeMinValue : fechaDesde;
            var result = bl.ListaReenviados(casilla, fechaDesde, fechaHasta);
            var jsonResult = Json(result.Select(i => new
            {
                Sesion = i.sesion,
                Secuencia = i.secuencia,
                Casilla = i.casilla,
                NombreCasilla = i.nombre_casilla,
                TipoMsg = i.tipo_msg,
                NombreTipo = i.nombre_tipo,
                Prioridad = i.prioridad,
                EstadoMsg = i.estado_msg,
                FechaRee = i.fecha_ree,
                FechaRec = i.fecha_rec,
                HoraRec = i.hora_rec,
                HoraRee = i.hora_ree,
                CodBancoRec = i.cod_banco_rec,
                BranchRec = i.branch_rec,
                CodBancoEm = i.cod_banco_em,
                BranchEm = i.branch_em,
                NombreBanco = i.nombre_banco,
                CiudadBanco = i.ciudad_banco,
                PaisBanco = i.pais_banco,
                OficinaBanco = i.oficina_banco,
                CodMoneda = i.cod_moneda,
                Monto = i.monto,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                TotalImp = i.total_imp,
                Comentario = i.comentario,
                FechaReenvio = i.fecha_ree + " " + i.hora_ree
            }).ToList(), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        private IList<sw_casillas> GetCasillasDeCacheOBL()
        {

            if (this.ControllerContext.HttpContext.Cache[CacheCasillasSwiftTodas] == null)
            {
                IList<sw_casillas> result = bl.GetTodasLasCasillas();
                this.ControllerContext.HttpContext.Cache[CacheCasillasSwiftTodas] = result;
                return result;
            }
            else
            {
                return this.ControllerContext.HttpContext.Cache[CacheCasillasSwiftTodas] as IList<sw_casillas>;
            }
        }

        public ActionResult Rechazar(string estadoRechazo)
        {
            ViewBag.Estado = estadoRechazo;
            return View();
        }

        [HttpPost]
        public JsonResult Rechazar(int casilla, int sesion, int secuencia, string estado, string texto)
        {
            var datosUsuario = HttpContext.GetCurrentUser().GetDatosUsuario();
            var rut = int.Parse(datosUsuario.Identificacion_Rut);
            bl.GrabaRechazo(casilla, sesion, secuencia, rut, estado, texto);
            return Json("OK");
        }

        [HttpPost]
        public JsonResult Confirmar(int casilla, int sesion, int secuencia)
        {
            var datosUsuario = HttpContext.GetCurrentUser().GetDatosUsuario();
            var rut = int.Parse(datosUsuario.Identificacion_Rut);
            bl.GrabaConfirmacion(casilla, sesion, secuencia, rut, "Mensaje Confirmado por Usuario");
            return Json("OK");
        }

        [HttpPost, HandleAjaxException]
        public JsonResult ConfirmarVarios(string casillaSesionSecuencia)
        {
            var datosUsuario = HttpContext.GetCurrentUser().GetDatosUsuario();
            var rut = int.Parse(datosUsuario.Identificacion_Rut);

            if (!String.IsNullOrEmpty(casillaSesionSecuencia))
            {
                List<string> partes = casillaSesionSecuencia.Split(new string[] { "#$#" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                foreach(string parte in partes)
                {
                    string[] s = parte.Split(new string[] { "!!"}, StringSplitOptions.RemoveEmptyEntries);
                    bl.GrabaConfirmacion(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), rut, "Mensaje Confirmado por Usuario");
                }

                return Json("OK");
            }
            else
            {
                throw new ArgumentException("Debe enviar un conjunto de nros de casilla/sesion/secuencia");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarCasillas(List<string> idsCasillasVisibles, string idCasillaDefault)
        {
            HttpCookie cookie = new HttpCookie(CookieName);
            cookie.Expires.AddDays(365);
            cookie.Values.Add(CookieValueCasillasVisibles, String.Join(",", idsCasillasVisibles.ToArray()));
            cookie.Values.Add(CookieValueCasillaDefault, idCasillaDefault);

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains(CookieName))
            {
                this.ControllerContext.HttpContext.Response.SetCookie(cookie);
            }
            else
            {
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }

            return Json(String.Empty);
        }

        public JsonResult VerificarImpresion(int sesion, int secuencia)
        {
            var item = bl.Proc_sw_trae_flag_impreso(sesion, secuencia);
            return Json(new { Result = item }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CambiarEstadoImprimir(int casilla, int sesion, int secuencia, int rutaLog, string estado, string comentario)
        {
            estado = "IMP";
            bl.Proc_sw_rec_graba_imp(casilla, sesion, secuencia, infoUsuario.RutEnFormatoBDSwift, estado, comentario);
            return Json(string.Empty);
        }

        
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CambiarEstadoImprimirLista(string listamensajes)
        {
            var mensajes = JsonConvert.DeserializeObject<List<MensajeItem>>(listamensajes).ToList();
            var estado = "IMP";
            foreach (var mjs in mensajes)
            {
                bl.Proc_sw_rec_graba_imp(mjs.Casilla, mjs.Sesion, mjs.Secuencia, infoUsuario.RutEnFormatoBDSwift, estado, mjs.Comentario);
            }
            return Json(string.Empty);
        }


        public ActionResult Alertas()
        {
            var model = new ConfiguracionAlertasModel(infoUsuario.MinsAlertaRecepcionSwift ?? 0);
            return View(model);
        }

        public ActionResult EditarConfiguracionAlertas(short? id)
        {
            id = id ?? 0;

            IDatosUsuario usuario = HttpContext.GetCurrentUser().GetDatosUsuario(); //leo devuelta DatosUsuario ya que no puedo usar el de session porque fue leído en un context diferente
            usuario.MinsAlertaRecepcionSwift = id;
            PortalService serviceP = new PortalService();
            serviceP.CambiarMinsAlertaRecepcionSwift(usuario);
            ActualizarDatosUsuarioEnCache(usuario);
            this.ControllerContext.HttpContext.Session.Remove(SessionKeys.Common.ConfigAlertas); //invalido el cache de config de alertas
            this.ControllerContext.HttpContext.Session.Remove(SessionKeys.Common.Alertas);
            return RedirectToAction("Index");
        }
    }
}