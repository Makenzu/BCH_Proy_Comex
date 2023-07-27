using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FinDia;
using BCH.Comex.UI.Web.Areas.FinDia.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.FinDia.Controllers
{
    public class AceptacionController : BaseController
    {
      
        //
        // GET: /FinDia/Aceptacion/
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Aceptacion(string centroCosto, string especialista, T_CCIRLLVR CC, IList<UI_Message> ListaErrores )
        //{
        //    this.fdService.GetListaAceptacionesVencidas(centroCosto, especialista, CC, ListaErrores);

        //    //Se guarda en sesion los datos necesarios para poder generar el archivo
        //    this.Globales = new DatosGlobales() { CCIRLLVR = CC };

        //    AceptacionViewModel o = new AceptacionViewModel(CC.VjAcp, ListaErrores);
        //    return View("Aceptacion", o);
        //}

        public ActionResult Aceptacion(string centroCosto, string especialista, string Errores)
        {
            T_CCIRLLVR CC = new T_CCIRLLVR();
            List<UI_Message> ListaErrores = new List<UI_Message>();
            ListaErrores.Add(new UI_Message() { Text = Errores, Type = TipoMensaje.Error });

            this.fdService.GetListaAceptacionesVencidas(centroCosto, especialista, CC, ListaErrores);

            //Se guarda en sesion los datos necesarios para poder generar el archivo
            this.Globales = new DatosGlobales() { CCIRLLVR = CC };

            AceptacionViewModel o = new AceptacionViewModel(CC.VjAcp, ListaErrores);
            return View("Aceptacion", o);
        }

        public ActionResult AceptacionBotones(string Command)
        {
            if (Command == "Aceptar")
            {
                return new RedirectResult("~/Home");
            }
            else if (Command == "Imprimir")
            {
                
            }
            return View();
        }

        public ActionResult ImpresionAceptacion()
        {
            ViewBag.Detalle = this.fdService.GetArchivoAceptacionesVencidas(this.Globales.CCIRLLVR); ;
            ViewBag.GenerarHtmlCompleto = false;
            ViewBag.Imprimir = false;

            return View();
        }
	}
}