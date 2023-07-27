using BCH.Comex.Core.BL.BancaMundial;
using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.Entities.Sbcor;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Controllers
{
    public class BancaMundialController : Controller
    {
        BancaMundialService service;

        private const string BancaMundialAppRole = "COMEX_SBCOR_SBCB";
        private const int LIMIT_BANCOS = 700; 

        static BancaMundialController()
        {
            new PortalService().RegisterApp("SBCB", "Consulta Banca Mundial", "Corresponsal", BancaMundialAppRole, "COMEX_GRP_SBCOR", "BancaMundial");
        }
        
        public BancaMundialController()
        {
            service = new BancaMundialService();
        }

        // GET: BancaMundial
        [AuthorizeOrForbidden(Roles = BancaMundialAppRole)]
        public ActionResult Index()
        {
            ViewBagPaises();
            return View(new List<sce_bic>());
        }

        public ActionResult Buscar(string swift, string pais, string ciudad, string banco, string direccion, string postal)
        {
            string AVOID_SEARCH = "AVOID_SEARCH";
            List<sce_bic> bancos = null;

            if(swift != AVOID_SEARCH)
            {
                int count = service.CountBancos(swift, pais, ciudad, banco, direccion, postal);
                if(count > LIMIT_BANCOS)
                {
                    return new JsonResult()
                    {
                        Data = BancaMundialService.LIMIT_EXCEEDED,
                        MaxJsonLength = Int32.MaxValue,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    bancos = service.BuscarBancaMundial(swift, pais, ciudad, banco, direccion, postal);
                }
            }
            else
            {
                bancos = new List<sce_bic>();
            }

            return new JsonResult()
            {
                Data = bancos,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [FileDownload]
        public FileResult DescargarExcel(string swift, string pais, string ciudad, string banco, string direccion, string postal)
        {
            MemoryStream stream = service.ExcelBancos(swift, pais, ciudad, banco, direccion, postal);
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "resultados banca mundial.xlsx");
        }   

        private void ViewBagPaises()
        {
            var paises = service.ListPaises();
            List<SelectListItem> paisesSelectListItem = new List<SelectListItem>();
            foreach (var item in paises)
            {
                paisesSelectListItem.Add(new SelectListItem
                {
                    Text = item.cpai_nompai,
                    Value = item.cpai_codpaic
                });
            }
            ViewBag.lst_Paises = paisesSelectListItem;
        }
    }
}