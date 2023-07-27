using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.UI.Web.Areas.Supervisor.Models;
using BCH.Comex.UI.Web.Common;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Supervisor.Controllers
{
    public class RelacionesIdiDecController : BaseController
    {
        public ActionResult Index()
        {
            RelacionesIdiDecViewModel model = new RelacionesIdiDecViewModel();
            return View(model);
        }

        [HandleAjaxException]
        public ActionResult BuscarRelacionesIdiDec(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            using (Tracer tracer = new Tracer())
            {
                IList<sce_grdo_s01_MS_Result> declaraciones = service.BuscarDeclaracionesAsociadasOperacion(codcct, codpro, codesp, codofi, codope);
                IList<sce_grio_s01_MS_Result> idis = service.BuscarIDIsAsociadasOperacion(codcct, codpro, codesp, codofi, codope);

                object jsonData = new { Declaraciones = declaraciones, IDIs = idis };

                var jsonResult = new JsonResult()
                    {
                        Data = jsonData,
                        MaxJsonLength = Int32.MaxValue,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                return jsonResult;
            }
        }

        [HttpPost, HandleAjaxException]
        public ActionResult EliminarRelacionesIdiDec(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            using (Tracer tracer = new Tracer())
            {
                service.EliminarIdiDecAsociadasOperacion(codcct, codpro, codesp, codofi, codope);
                var jsonResult = new JsonResult()
                {
                    Data = String.Empty,
                    MaxJsonLength = Int32.MaxValue,
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };

                return jsonResult;
            }
        }

       
    }
}