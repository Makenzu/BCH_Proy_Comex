using BCH.Comex.UI.Web.Helpers;
using System;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Controllers
{
    public class NuevaController : BaseControllerCG
    {
        public ActionResult Operacion(bool vieneDeMsg = false, bool resMsg = false )
        {
            return Rutear(service.NuevaOperacion, null);
        }
        public ActionResult Operacion_2(bool vieneDeMsg = false, bool resMsg = false)
        {
            return Rutear(service.NuevaOperacion_2, null);
        }

    }
}