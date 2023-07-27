using BCH.Comex.Core.BL.BancaMundial;
using BCH.Comex.Core.Entities.Sbcor;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;

namespace BCH.Comex.UI.API.Controllers
{
    public class SbcorController : ApiController
    {
        BancaMundialService service;

        public SbcorController()
        {
            service = new BancaMundialService();
        }

        [System.Web.Http.HttpGet]
        public ActionResult BancaMundial(string swift = "", string pais = "", string ciudad = "", string banco = "", string direccion = "", string postal = "")
        {
            
            int count = service.CountBancos(swift, pais, ciudad, banco, direccion, postal);

            object data;
            if(count >= 700 )
            {
                data = "Su búsqueda retornó demasiados resultados, debe agregar algún filtro";
            }
            else 
            {
                List<sce_bic> listaBancos = service.BuscarBancaMundial(swift, pais, ciudad, banco, direccion, postal);
                data = listaBancos;
            }
            
            return new JsonResult()
            {
                Data = data,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
