using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms;
using BCH.Comex.UI.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Controllers
{
    public class ParticipantesController : BaseControllerCG
    {
        // GET: ContabilidadGenerica/Participantes
        public ActionResult Identificar(string razonSocial)
        {
            this.Globales.RAZON_SOCIAL = razonSocial;
            return Rutear(service.Partys_1_2,IdentificarView);
        }

        [HttpPost]
        public ActionResult Identificar(UI_GetPrty0 m, string Command)
        {
            Command = (Command != null ? Command : "Aceptar");

            this.Globales.Tag = Command;
            return Rutear(this.service.Partys_Submit, (g) => View(g.GetPrty0));
        }

        [HttpPost]
        public JsonResult Identificar_Click(string codParticipante)
        {   
            this.Globales.GetPrty0.Llave.Text = FormatUtils.TryFormatearRutParticipante(codParticipante, false);
            this.service.Partys_Identificar_Click_1_2(this.Globales);
            var ope = Globales.SYGETPRT.Codop.Cent_costo + "-" + Globales.SYGETPRT.Codop.Id_Product + "-" + Globales.SYGETPRT.Codop.Id_Especia + "-" + Globales.SYGETPRT.Codop.Id_Empresa + "-" + Globales.SYGETPRT.Codop.Id_Operacion;
            this.Globales.GetPrty0.OPE = ope;

            this.Globales.GetPrty0.ListaErrores = new List<Comex.Common.UI_Modulos.UI_Message>(this.Globales.MESSAGES);
            this.Globales.MESSAGES.Clear();

            return Json(this.Globales.GetPrty0);
        }

        [HttpPost]
        public JsonResult Eliminar_Click()
        {
            this.service.Partys_Eliminar(this.Globales);
            return Json(this.Globales.GetPrty0);
        }

        [HttpPost]
        public JsonResult LstPartys_Click(int selectedValue)
        {
            this.Globales.GetPrty0.LstPartys.ListIndex = selectedValue;
            this.service.Partys_LstPartys_Click(this.Globales);
            return Json(this.Globales.GetPrty0);
        }

        [HttpPost]
        public JsonResult Donde_Click(bool selectedValue)
        {
            this.Globales.GetPrty0._Donde_0.Checked = selectedValue;
            this.Globales.GetPrty0._Donde_1.Checked = !selectedValue;
            this.service.Partys_Donde_Click(this.Globales);
            return Json(this.Globales.GetPrty0);
        }

        public ActionResult IdentificarView(DatosGlobales Globales)
        {
            var ope = Globales.SYGETPRT.Codop.Cent_costo + "-" + Globales.SYGETPRT.Codop.Id_Product + "-" + Globales.SYGETPRT.Codop.Id_Especia + "-" + Globales.SYGETPRT.Codop.Id_Empresa + "-" + Globales.SYGETPRT.Codop.Id_Operacion;
            this.Globales.GetPrty0.OPE = ope;

            return View(Globales.GetPrty0);
        }

        public ActionResult Preguntar()
        {
            return Rutear(service.Partys_Preguntar_Load, (g)=> View(g.GetPrty2));
        }

        public ActionResult Crear()
        {
            return Rutear(this.service.Partys_Crear_Participante, (g) => View());
        }
        public JsonResult Crear_Get()
        {
            return Json(this.Globales.GetPrty3, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult es_banco_change(UI_GetPrty3 jsonModel)
        {
            this.Globales.GetPrty3 = jsonModel;
            //this.service.partys_crear_es_banco_checked(this.Globales);
            return Json(this.Globales.GetPrty3);
        }

        [HttpPost]
        public JsonResult Crear_Aceptar(UI_GetPrty3 jsonModel)
        {
            this.Globales.GetPrty3 = jsonModel;
            this.service.partys_crear_aceptar(this.Globales);
            jsonModel.ListaErrores = new List<Comex.Common.UI_Modulos.UI_Message>(this.Globales.MESSAGES);
            this.Globales.MESSAGES.Clear();
            return Json(jsonModel);
        }

        public ActionResult Crear_Aceptar_Post()
        {
            return Rutear(this.service.Partys_Crear_Aceptar_Post, null);
        }

        public ActionResult Crear_Cancelar()
        {
            return RedirectToAction("Identificar");
        }

        public ActionResult Consulta()
        {
            return Rutear(this.service.Partys_Consultar_Participante, (g) => View(g.PrtEnt09));
        }

        [HttpPost]
        public JsonResult Consultar_Datos(string razonSocial)
        {
            this.Globales.PrtEnt09.caja.Text = razonSocial ?? String.Empty;
            this.service.Partys_Consultar_Buscar(this.Globales);
            var resultado = this.Globales.PrtEnt09.lista.Items.Select(x => new
            {
                razon_social = x.GetColumn("Nombre o Razón Social"),
                identificador = x.GetColumn("Identificador")
            });

            //paginado, el objeto resultado tiene que tener esos atributos
            var jsonData = new { total = resultado.Count(), rows = resultado };

            return new JsonResult()
            {
                Data = jsonData,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult Get_ParticipantesIdentificar()
        {
            return Json(this.Globales.GetPrty1, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ParticipantesIdentificar_Aceptar(string selectedRS, string selectedDir)
        {
            int intval;

            if (int.TryParse(selectedRS, out intval))
            {
                this.Globales.GetPrty1.Nome.ListIndex = this.Globales.GetPrty1.Nome.get_Index(intval);
            }
            if (int.TryParse(selectedDir, out intval))
            {
                this.Globales.GetPrty1.Dire.ListIndex = this.Globales.GetPrty1.Dire.get_Index(intval);
            }
            this.service.Partys_ParticipantesIdentificar_Aceptar(this.Globales);

            this.Globales.GetPrty0.ListaErrores = new List<Comex.Common.UI_Modulos.UI_Message>(this.Globales.MESSAGES);
            this.Globales.MESSAGES.Clear();

            return Json(this.Globales.GetPrty0, JsonRequestBehavior.AllowGet);
        }

    }
}