using BCH.Comex.Core.BL.SWSE;
using BCH.Comex.Core.Entities.Swift;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using BCH.Comex.UI.Web.Common;

namespace BCH.Comex.UI.Web.Areas.EnvioSwift.Controllers
{
    public class BancosController : Controller
    {
        protected EnvioSwiftService service = new EnvioSwiftService();
        private const int LIMIT_BANCOS = 700;
        // GET: EnvioSwift/Bancos
        public ActionResult Listado()
        {
            ViewBagPaises();
            return View();
        }

        public JsonResult Buscar(string swift, string pais, string ciudad, string banco, string direccion)
        {
            int count = service.CuentaBancos(swift, pais, ciudad, banco, direccion);

            if (count > LIMIT_BANCOS)
            {
                return new JsonResult()
                {
                    Data = "LIMIT_EXCEEDED",
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            IEnumerable<sw_bancos> data = service.BuscarBancos(swift, pais, ciudad, banco, direccion);
            return new JsonResult()
            {
                Data = data,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult Imprimir(string swift, string pais, string ciudad, string banco, string direccion)
        {
            var data = service.BuscarBancos(swift, pais, ciudad, banco, direccion);
            return View(data);
        }

        [FileDownload]
        public FileResult Excel(string swift, string pais, string ciudad, string banco, string direccion)
        {
            MemoryStream stream = service.ExcelBancos(swift, pais, ciudad, banco, direccion);
            string Nombre = "Consulta Bancos " + (DateTime.Now).ToString("yyyy-MM-dd hh:mm") + ".xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Nombre);
            //var data = service.BuscarBancos(swift, pais, ciudad, banco, direccion);

            //var bancos = new System.Data.DataTable("bancos");

            //bancos.Columns.Add("Código Banco", typeof(string));
            //bancos.Columns.Add("Branch", typeof(string));
            //bancos.Columns.Add("Nombre de Banco", typeof(string));
            //bancos.Columns.Add("Dirección", typeof(string));
            //bancos.Columns.Add("Ciudad", typeof(string));
            //bancos.Columns.Add("País", typeof(string));
            //bancos.Columns.Add("Oficina", typeof(string));
            //bancos.Columns.Add("Intercambio Clave", typeof(string));

            //foreach (var d in data)
            //{
            //    bancos.Rows.Add(d.cod_banco, d.branch, d.nombre_banco, d.direccion_banco, d.ciudad_banco, d.pais_banco, d.oficina_banco, d.intercambio_clave);
            //}

            //var grid = new GridView();
            //grid.DataSource = bancos;
            //grid.DataBind();

            //Response.ClearContent();
            //Response.Buffer = true;
            //string name = "Consulta Bancos " + (DateTime.Now).ToString("yyyy-MM-dd hh:mm");
            //Response.AddHeader("content-disposition", "attachment; filename=" + name + ".xls");
            //Response.ContentType = "application/vnd.ms-excel";

            //Response.Charset = "";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);

            //grid.RenderControl(htw);

            //Response.Output.Write(sw.ToString());
            //Response.Flush();
            //Response.End();
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
                    Value = item.cpai_nompai
                });
            }
            ViewBag.lst_Paises = paisesSelectListItem;
        }
    }
}