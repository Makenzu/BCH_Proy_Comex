using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Common.Exceptions;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.MT300Gestion;
using BCH.Comex.Core.Entities.Cext01.MT300Gestion;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.MT300Gestion.Controllers
{
    /// <summary>
    /// Funcionalidad compartida por todos los controladores de GeneracionMT300
    /// </summary>
    public class BaseControllerMT300Gestion : BaseController
    {
        protected MT300GestionService service = new MT300GestionService();

        /// <summary>
        /// Datos globales del modulo GeneracionMT300
        /// </summary>
        protected DatosGlobales Globales
        {
            get
            {
                var dato = Session[SessionKeys.MT300Gestion.DatosGlobalesKey] as DatosGlobales;
                if (dato == null)
                {
                    Session[SessionKeys.MT300Gestion.DatosGlobalesKey] = dato = service.Iniciar(HttpContext.GetCurrentUser().GetDatosUsuario());
                }
                return dato;
            }
            set
            {
                Session[SessionKeys.MT300Gestion.DatosGlobalesKey] = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (service != null)
            {
                service.Dispose();
            }
            base.Dispose(disposing);
        }

        protected List<UI_Message> CargarMensajesGlobales()
        {
            List<UI_Message> mensajes = Globales.ListaMensajes;
            Globales.ListaMensajes = new List<UI_Message>();
            return mensajes;
        }

    }
}
