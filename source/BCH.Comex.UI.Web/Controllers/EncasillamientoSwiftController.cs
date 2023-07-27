using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.BL.SWEM;
using BCH.Comex.Core.BL.SWEN;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.UI.Web.Helpers;
using BCH.Comex.UI.Web.Models.EncasillamientoSwift;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Controllers
{
    [AuthorizeOrForbidden(Roles = EncasillamientoSwiftAppRole)]
    public class EncasillamientoSwiftController : BCH.Comex.UI.Web.Common.BaseController
    {
        private SwiftMgr bl;
        private SwenService swenService;
        private const string CookieName = "BCHComexSwen_Casillas";
        private const string CookieValueCasillasVisibles = "CasillasVisibles";
        private const string CookieValueCasillaDefault = "CasillaDefault";
        private const string CacheCasillasSwiftTodas = "CasillasSwiftTodas";
        private const string EncasillamientoSwiftAppRole = "COMEX_SWIFT_SWEN";

        static EncasillamientoSwiftController()
        {
            new PortalService().RegisterApp("SWEN", "Encasillamiento de mensajes Swift", "SWIFT", EncasillamientoSwiftAppRole, "COMEX_GRP_SWIFT", "EncasillamientoSwift");
        }

        public EncasillamientoSwiftController()
        {
            this.bl = new SwiftMgr();
            this.swenService = new SwenService();
        }

        // GET: EncasillamientoSwift
        [AuthorizeOrForbidden(Roles = EncasillamientoSwiftAppRole)]
        public ActionResult Index()
        {
            IEnumerable<sw_casillas> casillas = GetCasillasDeCacheOBL().OrderBy(i => i.cod_casilla);
            SelectList todasLasCasillas = new SelectList(casillas, "cod_casilla", "DataTextField");
            var model = new IndexModel();
            model.TodasLasCasillas = todasLasCasillas;
            int rutOut = 0; 
            int.TryParse( HttpContext.GetCurrentUser().GetDatosUsuario().Identificacion_Rut , out rutOut );
            model.RutEntry = rutOut;

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains(CookieName))
            {
                //leo las casillas visibles y la casilla default de la cookie
                NameValueCollection collection = this.ControllerContext.HttpContext.Request.Cookies[CookieName].Values;
                model.IdsCasillasVisibles = collection[CookieValueCasillasVisibles].Split(new char[] { ',' }).ToList();
                IEnumerable<sw_casillas> casillasVisibles = casillas.Where(c => model.IdsCasillasVisibles.Contains(c.cod_casilla.ToString())).ToList();
                model.IdCasillaDefault = collection[CookieValueCasillaDefault];
                int valorDefault = 0;
                int.TryParse(model.IdCasillaDefault, out valorDefault);
                model.CasillasVisibles = new SelectList(casillasVisibles, "cod_casilla", "DataTextField", valorDefault);
            }
            return View(model);
        }

        public JsonResult GetMensajesRecibidosRango(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var result = swenService.GetMensajesRecibidosRango(idCasilla, fechaInicio, fechaFin);

            return Json(result.Select(i => new { 
                Secuencia = i.secuencia, 
                Sesion = i.sesion,
                Tipo = i.tipo_msg, 
                Estado = i.estado_msg, 
                FechaRecepcion = i.fecha_send + " " + i.hora_send,
                BancoEmisor = i.cod_banco_em.Trim() + i.branch_em.Trim(),
                FechaProceso = i.fecha_pro + " " + i.hora_pro, 
                Referencia = i.referencia, 
                Beneficiario = i.beneficiario, 
                Moneda = i.cod_moneda, 
                Monto = i.monto,
                UnidadProceso =i.unidad,
            }), JsonRequestBehavior.AllowGet);
        }

        //Replicado para desencasillar
        public ActionResult Desencasillar()
        {
            IEnumerable<sw_casillas> casillas = GetCasillasDeCacheOBL().OrderBy(i => i.cod_casilla);
            SelectList todasLasCasillas = new SelectList(casillas, "cod_casilla", "DataTextField");
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
                int.TryParse(model.IdCasillaDefault, out valorDefault);
                model.CasillasVisibles = new SelectList(casillasVisibles, "cod_casilla", "DataTextField", valorDefault);
            }
            return View(model);
        }

        //Agregado para opción desencasillar
        public JsonResult GetMensajesRecibidosRangoDes(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var result = swenService.GetMensajesRecibidosRangoDes(idCasilla, fechaInicio, fechaFin);
            return Json(result.Select(i => new
            {
                Secuencia = i.secuencia,
                Sesion = i.sesion,
                Tipo = i.tipo_msg,
                Casilla = i.casilla,
                Estado = i.estado_msg,
                FechaRecepcion = i.fecha_env + " " + i.hora_env,
                FechaProceso = i.fecha_enc + " " + i.hora_enc,
                Referencia = i.referencia,
                Beneficiario = i.beneficiario,
                Moneda = i.cod_moneda,
                Monto = i.monto
            }), JsonRequestBehavior.AllowGet);
        }

    /// <summary>
    /// Validamos si el mensaje fue encasillado y lo encasillamos
    /// </summary>
    /// <param name="encasillamientounoAuno"></param>
    /// <returns>resultado del encasillamiento</returns>
    public JsonResult GetMensajeEstado_unoAuno(string encasillamientounoAuno)
    {
        MensajeSwitchEstado mensajeSwitchEstado = new MensajeSwitchEstado();
        string obtenerEstado = "";
        String[] arreglo = encasillamientounoAuno.Split('ø');
        string salidaFinal = "";
        string idCasillaDestino, nroSesion, nroSecuencia, rutUsuario, mensajeObservacion;
        string fallo = "";
        try
        {
            for (int i = 1; i < arreglo.Length; i++)
            {
                string[] valores = arreglo[i].Split('¨');
                nroSesion = valores[0];
                nroSecuencia = valores[1];
                idCasillaDestino = valores[2];
                rutUsuario = valores[3];
                mensajeObservacion = valores[4];
                fallo = nroSecuencia;

                obtenerEstado = swenService.GetMensajeEstado(Convert.ToInt32(nroSesion), Convert.ToInt32(nroSecuencia));
                if (obtenerEstado == "ENC" || obtenerEstado == "REE" || obtenerEstado == "IMP" || obtenerEstado == "CNF")
                {
                    if (salidaFinal == "")
                    {
                        salidaFinal = "0-" + nroSecuencia;
                    }
                    else
                    {
                        salidaFinal = salidaFinal +
                            ",0-" + nroSecuencia;
                    }
                }
                else
                {
                    if (salidaFinal == "")
                    {
                        salidaFinal = swenService.SetChangeCasillaMensaje(Convert.ToInt32(idCasillaDestino), Convert.ToInt32(nroSesion), Convert.ToInt32(nroSecuencia), Convert.ToInt32(rutUsuario), mensajeObservacion).ToString() +
                                        '-' +
                                        nroSecuencia;
                    }
                    else
                    {
                        salidaFinal = salidaFinal +
                                        "," + swenService.SetChangeCasillaMensaje(Convert.ToInt32(idCasillaDestino), Convert.ToInt32(nroSesion), Convert.ToInt32(nroSecuencia), Convert.ToInt32(rutUsuario), mensajeObservacion).ToString() +
                                        "-" + nroSecuencia;
                    }
                }
            }


        }
        catch (Exception ex)
        {
            if (salidaFinal == "")
            {
                salidaFinal = "6-" + fallo + '(' + ex.Message + ')';
            }
            else
            {
                salidaFinal = salidaFinal +
                                "," + "6-" + fallo + '(' + ex.Message + ')';
            }
            mensajeSwitchEstado.estado = salidaFinal;
        }
        mensajeSwitchEstado.estado = salidaFinal;
        return Json(mensajeSwitchEstado, JsonRequestBehavior.AllowGet);
    }


/// <summary>
/// Validamos si el mensaje fue encasillado y actualizamos si corrsponde
/// </summary>
/// <param name="idCasillaDestino"></param>
/// <param name="secuencia"></param>
/// <param name="rutUsuario"></param>
/// <param name="mensajeObservacion"></param>
/// <returns></returns>
public JsonResult GetMensajeEstado_(int idCasillaDestino, string secuencia, int rutUsuario, string mensajeObservacion)
{
    MensajeSwitchEstado mensajeSwitchEstado = new MensajeSwitchEstado();
    string obtenerEstado = "";
    string salidaFinal = "";
    string fallo = "";
    try
    {
        String[] arreglo = secuencia.Split('ø');
        for (int i = 1; i < arreglo.Length; i++)
        {
            string[] sesionSecuencia = arreglo[i].Split('¨');
            fallo = sesionSecuencia[1];
            obtenerEstado = swenService.GetMensajeEstado(Convert.ToInt32(sesionSecuencia[0]), Convert.ToInt32(sesionSecuencia[1]));
            if (obtenerEstado == "ENC" || obtenerEstado == "REE" || obtenerEstado == "IMP" || obtenerEstado == "CNF")
            {
                if (salidaFinal == "")
                {
                    salidaFinal = "0-" + sesionSecuencia[1];
                }
                else
                {
                    salidaFinal = salidaFinal +
                        ",0-" + sesionSecuencia[1];
                }
            }
            else
            {
                if (salidaFinal == "")
                {
                    salidaFinal = swenService.SetChangeCasillaMensaje(idCasillaDestino, Convert.ToInt32(sesionSecuencia[0]), Convert.ToInt32(sesionSecuencia[1]), rutUsuario, mensajeObservacion).ToString() +
                    '-' +
                    sesionSecuencia[1];
                }
                else
                {
                    salidaFinal = salidaFinal +
                    "," + swenService.SetChangeCasillaMensaje(idCasillaDestino, Convert.ToInt32(sesionSecuencia[0]), Convert.ToInt32(sesionSecuencia[1]), rutUsuario, mensajeObservacion).ToString() +
                    "-" + sesionSecuencia[1];
                }
            }

        }
        mensajeSwitchEstado.estado = salidaFinal;
    }
    catch (Exception ex)
    {
        if (salidaFinal == "")
        {
            salidaFinal = "6-" + fallo + '(' + ex.Message + ')';
        }
        else
        {
            salidaFinal = salidaFinal +
            "," + "6-" + fallo + '(' + ex.Message + ')';
        }
        mensajeSwitchEstado.estado = salidaFinal;
    }
    return Json(mensajeSwitchEstado, JsonRequestBehavior.AllowGet);
}

        /// <summary>
        /// Validamos si el mensaje fue encasillado
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="secuencia"></param>
        /// <returns></returns>
        public JsonResult GetMensajeEstado(int sesion, int secuencia)
        {
            MensajeSwitchEstado mensajeSwitchEstado = new MensajeSwitchEstado(sesion, secuencia);
            mensajeSwitchEstado.estado = swenService.GetMensajeEstado(sesion, secuencia);
            return Json(mensajeSwitchEstado, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Graba cambio de Casilla para Mensaje Switf
        /// </summary>
        /// <param name="idCasillaDestino"></param>
        /// <param name="nroSesion"></param>
        /// <param name="nroSecuencia"></param>
        /// <param name="rutUsuario"></param>
        /// <param name="mensajeObservacion"></param>
        /// <returns></returns>
        public JsonResult SetChangeCasillaMensaje(int idCasillaDestino, int nroSesion, int nroSecuencia, int rutUsuario, string mensajeObservacion)
        {
            ResponseChangeCasilla responseChangeCasilla = new ResponseChangeCasilla(nroSesion, nroSecuencia);

            try
            {
                responseChangeCasilla.respuesta = swenService.SetChangeCasillaMensaje(idCasillaDestino, nroSesion, nroSecuencia, rutUsuario, mensajeObservacion);
                if (responseChangeCasilla.respuesta > 0)
                {
                    string error = string.Empty;

                    switch (responseChangeCasilla.respuesta)
                    {
                       default: error = "Error en actualización"; break;
                    }
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                responseChangeCasilla.msg = ex.Message;
            }
            return Json(responseChangeCasilla);
        }


        private IEnumerable<sw_casillas> GetCasillasDeCacheOBL()
        {

            if (this.ControllerContext.HttpContext.Cache[CacheCasillasSwiftTodas] == null)
            {
                IEnumerable<sw_casillas> result = bl.GetTodasLasCasillas();
                this.ControllerContext.HttpContext.Cache[CacheCasillasSwiftTodas] = result;
                return result;
            }
            else
                return this.ControllerContext.HttpContext.Cache[CacheCasillasSwiftTodas] as IEnumerable<sw_casillas>;
        }

        public JsonResult DesencasillarAction(int idCasilla, int Sesion, int Secuencia)
        {
            var result = swenService.Deshacer(idCasilla, Sesion, Secuencia, infoUsuario.RutEnFormatoBDSwift);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarCasillas(List<string> idsCasillasVisibles, string idCasillaDefault)
        {
            HttpCookie cookie = new HttpCookie(CookieName);
            cookie.Expires.AddDays(365);
            cookie.Values.Add(CookieValueCasillasVisibles, String.Join(",", idsCasillasVisibles.ToArray()));
            cookie.Values.Add(CookieValueCasillaDefault, idCasillaDefault);

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains(CookieName))
                this.ControllerContext.HttpContext.Response.SetCookie(cookie);
            else
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            
            return Json(String.Empty);
        }
    }

    public class MensajeSwitchEstado
    {
        public Int32 sesion;
        public Int32 secuencia;
        public String estado;

        public MensajeSwitchEstado() { }
        public MensajeSwitchEstado(Int32 sesion, Int32 secuencia) {
            this.sesion = sesion;
            this.secuencia = secuencia;
        }
    }

    public class ResponseChangeCasilla
    {
        public Int32 sesion;
        public Int32 secuencia;
        public int respuesta;
        public string msg;

        public ResponseChangeCasilla() { }
        public ResponseChangeCasilla(Int32 sesion, Int32 secuencia)
        {
            this.sesion = sesion;
            this.secuencia = secuencia;
            this.msg = string.Empty;
        }
    }
}