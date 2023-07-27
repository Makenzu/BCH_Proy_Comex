using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Controllers
{
    public class DatosAdicionalesController : BaseControllerCG
    {
        public JsonResult GetForm()
        {
            this.Globales.FrmGLOV.Operacion.Text = "Datos Adicionales " + this.Globales.SYGETPRT.Codop.Cent_costo + "-" + this.Globales.SYGETPRT.Codop.Id_Product + "-" + this.Globales.SYGETPRT.Codop.Id_Especia + "-" + this.Globales.SYGETPRT.Codop.Id_Empresa + "-" + this.Globales.SYGETPRT.Codop.Id_Operacion;
            return Json(this.Globales.FrmGLOV, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Tx_Datos_Blur(UI_FrmGLOV jsonModel,int id)
        {
            this.Globales.FrmGLOV = jsonModel;
            this.service.GLOV_Tx_Datos_Blur(this.Globales, id);
            this.Globales.FrmGLOV.ListaErrores = new List<Comex.Common.UI_Modulos.UI_Message>(this.Globales.MESSAGES);
            this.Globales.MESSAGES.Clear();
            return Json(this.Globales.FrmGLOV);
        }

        [HttpPost]
        public JsonResult Boton_Click(UI_FrmGLOV jsonModel,int id,bool? vieneDeMsg, bool? resMsg)
        {
            using (var tracer = new Tracer("Aceptar de datos adicionales"))
            {
                try
                {
                    this.Globales.vieneDeMsg = vieneDeMsg ?? false;
                    this.Globales.resMsg = resMsg ?? false;
                    this.Globales.FrmGLOV = jsonModel;

                    this.service.GLOV_Boton(this.Globales, id);
                    jsonModel.ListaErrores = new List<Comex.Common.UI_Modulos.UI_Message>(this.Globales.MESSAGES.Where(x => x.Type != Comex.Common.UI_Modulos.TipoMensaje.YesNo));
                    jsonModel.ListaConfirmaciones = new List<Comex.Common.UI_Modulos.UI_Message>(this.Globales.MESSAGES.Where(x => x.Type == Comex.Common.UI_Modulos.TipoMensaje.YesNo));
                    this.Globales.MESSAGES.Clear();
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta al aceptar Datos Adicionales de XGGL", ex);
                    if (ExceptionPolicy.HandleException(ex, "PoliticaUIFundTransfer")) throw;
                }

                return Json(jsonModel);
            }
        }

        public ActionResult Fin()
        {
            return Rutear(this.service.GLOV_Fin, null);
        }
    }
}