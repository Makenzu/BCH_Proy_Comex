using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.UI.Web.Common;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Supervisor.Controllers
{
    public class CederCarteraController : BaseController
    {
        // GET: Supervisor/CederCartera
        public ActionResult Index()
        {
            using (Tracer tracer = new Tracer())
            {
                if (this.Globales.FrmCeder == null)
                    this.Globales.FrmCeder = new FrmCederDTO() ;
                this.Globales.FrmCeder.ListaErrores.Clear();
                return View(this.Globales.FrmCeder);
            }
        }

        [HttpGet]
        public JsonResult CederCarteraInit()
        {
            this.service.CederCarteraInit(this.Globales);
            return Json(this.Globales.FrmCeder, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, HandleError]
        public JsonResult ObtenerClientes(string usuarioActual, string producto)
        {
            this.Globales.FrmCeder.ListaErrores.Clear();
            this.service.CederCarteraObtenerClientes(this.Globales, usuarioActual, producto);
            this.Globales.FrmCeder.ListaErrores = Globales.ListaMensajesError;
            return Json(new { this.Globales.FrmCeder.Clientes, 
                this.Globales.FrmCeder.Operaciones, 
                this.Globales.FrmCeder.ListaErrores }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, HandleError]
        public JsonResult ObtenerOperaciones(string usuarioActual, string producto, string cliente)
        {
            this.Globales.FrmCeder.ListaErrores.Clear();
            this.service.CederCarteraObtenerOperaciones(this.Globales, usuarioActual, producto, cliente);
            this.Globales.FrmCeder.ListaErrores = Globales.ListaMensajesError;
            return Json(new { this.Globales.FrmCeder.Operaciones, 
                this.Globales.FrmCeder.ListaErrores }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult ProcessCederCartera(string usuarioActual, string usuarioNuevo, string producto, string clienteID)
        {
            using (Tracer tracer = new Tracer())
            {
                this.Globales.FrmCeder.ListaErrores.Clear();
                this.service.CederCarteraSave(this.Globales, usuarioActual, usuarioNuevo, clienteID, producto);
                this.Globales.FrmCeder.ListaErrores = Globales.ListaMensajesError;
                return Json(this.Globales.FrmCeder, JsonRequestBehavior.AllowGet);
            }
        }

    }
}