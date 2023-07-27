using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.UI.Web.Common;
using Microsoft.VisualBasic;
using System.Linq;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Supervisor.Controllers
{
    public class ReemplazarUsuarioController : BaseController
    {
        // GET: Supervisor/ReemplazarUsuario
        public ActionResult Index()
        {
            using (Tracer tracer = new Tracer())
            {
                if (this.Globales.FrmTrasp == null)
                    this.Globales.FrmTrasp = new FrmTraspDTO();

                service.TraspasoUsuarioInit(this.Globales);
                return View(this.Globales.FrmTrasp);
            }
        }

        public JsonResult ObtenerReemplazos(string usuario) {
            using (Tracer tracer = new Tracer())
            {
                string ii = Strings.Left(usuario, 6);
                string CCtUsr = Strings.Left(ii.Trim(), 3);
                string codusr = Strings.Right(ii.Trim(), 2);

                var mappings = service.SyGetn_Trasp(CCtUsr, codusr);

                return Json(mappings.Select(m => new { id = m, nombre = m }), JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Reemplazar(string activo, string reemplazanUsuarios)
        {
            using (Tracer tracer = new Tracer())
            {
                var selectedUsers = reemplazanUsuarios.Split(';').Select(user => user.Substring(0, 6).Replace("-", string.Empty));
                string users = string.Join(";", selectedUsers).Trim();

                string idActivo = activo.Substring(0, 6);//Cb_UsrAct.GetItemData(Cb_UsrAct.SelectedIndex).ToDbl();
                //string ii = Strings.Format(i, "00000");
                string Parametro1 = Strings.Left(idActivo, 3);
                string Parametro2 = Strings.Right(idActivo, 2);
                // Parametro1$ = Left$(Trim$(Str$(i#)), 3)     'Trim$(Mid$(Cb_UsrAct, 1, 3))
                // Parametro2$ = Right$(Trim$(Str$(i#)), 2)    'Trim$(Mid$(Cb_UsrAct, 5, 2))

                service.SyPut_Trasp(Parametro1, Parametro2, users);
                /*a = MODTRASP.SyPut_Trasp(Parametro1, Parametro2, users);
                if (a != 0)
                {
                    //MigrationSupport.Utils.MsgBox("El Reemplazo de Usuarios ha resultado exitoso.", MODGPYF0.pito(64).Cast<MigrationSupport.MsgBoxStyle>(), MODTRASP.MgbTraspaso);
                }*/
                return new RedirectResult("~/Supervisor");
            }
        }


    }
}