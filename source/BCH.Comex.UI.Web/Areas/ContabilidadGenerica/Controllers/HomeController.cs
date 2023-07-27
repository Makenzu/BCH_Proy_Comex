using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.UI.Web.Helpers;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using BCH.Comex.Common;

namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Controllers
{
    public class HomeController : BaseControllerCG
    {

        static HomeController()
        {
            new PortalService().RegisterApp("XGGL", "Contabilidad Genérica (GL) - Ingreso de asientos contables", "MECO",
                Constantes.AppRoles.ContabilidadGenericaAppRole, "COMEX_GRP_CAMBIOS", "ContabilidadGenerica");
        }

        // GET: ContabilidadGenerica/Home
        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.ContabilidadGenericaAppRole)]
        public ActionResult Index()
        {
            return Rutear(null, IndexView, false);
        }

        public ActionResult Adicionales()
        {
            return Rutear(this.service.GLOV_FormLoad, (g) => View(g.FrmGLOV));
        }

        public void MapearErrores() 
        {
            Globales.gl.ListaErrores = new List<Comex.Common.UI_Modulos.UI_Message>(Globales.MESSAGES.Where(i => i.Type != Comex.Common.UI_Modulos.TipoMensaje.YesNo));
            Globales.gl.ListaConfirmaciones = new List<Comex.Common.UI_Modulos.UI_Message>(Globales.MESSAGES.Where(i => i.Type == Comex.Common.UI_Modulos.TipoMensaje.YesNo));
            Globales.MESSAGES.Clear();
        }

        #region PANTALLA PRINCIPAL
        public ActionResult IndexView(DatosGlobales Globales)
        {
            MapearErrores();
            service.IndexMapearConfigImprimirAControles(Globales);
            if (!string.IsNullOrEmpty(Globales.gl.Cliente.Text))
            {
                Globales.gl.Bot_Salvar.Enabled = true;
                Globales.gl.monedas.Enabled = true;
                Globales.gl.monto.Enabled = true;
            }

            return View(Globales.gl);
        }

        public JsonResult Monedas_Click(int value)
        {

            this.Globales.gl.monedas.ListIndex = this.Globales.gl.monedas.Items.FindIndex(x => x.Data == value);
            this.service.GL_Moneda_Click(this.Globales);
            MapearErrores();
            return Json(this.Globales.gl, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Monto_Blur(string monto)
        {
            this.Globales.gl.monto.Text = monto;
            this.service.GL_Monto_Blur(this.Globales);
            MapearErrores();
            return Json(this.Globales.gl, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DebeHaber_Change(bool debe)
        {
            this.Globales.gl.tipo_000.Checked = debe;
            this.Globales.gl.tipo_001.Checked = !debe;
            int index = debe ? 0 : 1;
            this.service.GL_DebeHaber_Change(this.Globales, index);
            MapearErrores();
            return Json(this.Globales.gl, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Nemonico_Blur(string texto)
        {
            this.Globales.gl.nemonico.Text = texto;
            this.service.GL_Nemonico_Blur(this.Globales);
            MapearErrores();
            return Json(this.Globales.gl, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Cambiar_Click(bool value)
        {
            this.Globales.gl.cambiar.Checked = value;
            MapearErrores();
            return Json(this.Globales.gl, JsonRequestBehavior.AllowGet);
        }

        public void txtNumRef_Blur(string texto)
        {
            this.Globales.gl.txtNumRef.Text = texto;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Tx_ReferenciaCliente_Blur(string texto)
        {
            Globales.gl.Tx_ReferenciaCliente.Text = texto;
            Globales.gl.txtReferenciaCliente = texto;
        }

        public ActionResult Ver_1()
        {
            return Rutear(this.service.GL_Ver_1_2, null);
        }

        public ActionResult Ver_2()
        {
            return Rutear(this.service.GL_Ver_2_2, null);
        }

        //public void Cambiar_Change(bool value)
        //{
        //    this.Globales.gl.cambiar.Checked = value;
        //}

        public void Impuesto_Change(bool value)
        {
            this.Globales.gl.Impuesto.Checked = value;
        }

        //public ActionResult Aceptar()
        //{
        //    return Rutear(this.service.GL_OK_Click, null);
        //}

        public JsonResult Aceptar()
        {
            this.service.GL_OK_Click(this.Globales);
            MapearErrores();
            this.Globales.MESSAGES.Clear();
            return Json(new { this.Globales.gl.ListaConfirmaciones, this.Globales.gl.ListaErrores }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GL_OK_Click_DespuesDeParticipantes()
        {
            return Rutear(this.service.GL_OK_Click_DespuesDeParticipantes, null);
        }

        public ActionResult Aceptar_Correcto()
        {
            //Si el llamado a la funcion Aceptar se completo sin problemas, se redirecciona a la accion que determino esa funcion
            //esto se hizo porque era necesario partir la funcion para poder mostrar el mensaje de confirmacion
            return Rutear(null, null);
        }

        public ActionResult Aceptar_ValidacionSaldo(bool saltarValidacionSaldo)
        {
            Globales.gl.ValidaMontos = false;
            this.service.GL_CuentaDebe_GOV(this.Globales);
            return Rutear(null, null);
        }

        public JsonResult m_n_Click(int id)
        {
            this.Globales.gl.m_n.ListIndex = id;
            this.service.GL_m_n_Click(this.Globales);
            this.service.GL_m_n_DoubleClick(this.Globales);
            return Json(this.Globales.gl, JsonRequestBehavior.AllowGet);
        }
        public JsonResult m_e_Click(int id)
        {
            this.Globales.gl.m_e.ListIndex = id;
            this.service.GL_m_e_Click(this.Globales);
            this.service.GL_m_e_DoubleClick(this.Globales);
            return Json(this.Globales.gl, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Cancelar(string nemMon,bool monNac,int indLista)
        {
            Globales.gl.MONEDAD = nemMon;
            Globales.gl.ESMN = monNac;
            Globales.gl.INDEX_LISTA = (monNac ? this.Globales.gl.m_n.get_IData(indLista) : this.Globales.gl.m_e.get_IData(indLista));
            return Rutear(this.service.GL_Cancelar, null);
        }
        
        [HttpPost]
        public ActionResult Index_ConfigImprimirClick(string elem, bool value)
        {
            try
            {
                switch (elem)
                {
                    case "ChkImpresionCartas_Checked":
                        this.Globales.gl.ChkImpresionCartas.Checked = value;
                        break;
                    case "ChkImpresionContabilidad_Checked":
                        this.Globales.gl.ChkImpresionContabilidad.Checked = value;
                        break;
                    default:
                        break;
                }

                service.IndexMapearConfigImprimirADatosUsuario(this.Globales);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la opción", ex);
            }
            //no hay nada para hacer
            return Json(this.Globales.gl, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region DESPUES DE GOV
        public ActionResult Aceptar_Despues_Aceptar_Adicionales_1()
        {
            return Rutear(this.service.GL_Aceptar_GOV_1, null);
        }

        public ActionResult Aceptar_Despues_Aceptar_Adicionales_2()
        {
            return Rutear(this.service.GL_Aceptar_GOV_2, null);
        }

        public ActionResult Aceptar_Despues_CuentaDebe_Adicionales()
        {
            return Rutear(this.service.GL_CuentaDebe_GOV, null);
        }

        public ActionResult Aceptar_Despues_CuentaHaber_Adicionales()
        {
            return Rutear(this.service.GL_CuentaHaber_GOV, null);
        }

        public ActionResult Aceptar_Despues_SyLeeCuentas_Adicionales_Debe()
        {
            this.Globales.Action = "Aceptar_Despues_CuentaDebe_Adicionales";
            this.Globales.Controller = "Home";
            return Rutear(null, null);
        }

        public ActionResult Aceptar_Despues_SyLeeCuentas_Adicionales_Haber()
        {
            return Rutear(this.service.GL_SyLeeCuentas_GOV_Haber, null);
        }

        public ActionResult Aceptar_Monto()
        {
            this.Globales.gl.ListaConfirmaciones = new List<Comex.Common.UI_Modulos.UI_Message>(this.Globales.MESSAGES);
            this.Globales.MESSAGES.Clear();
            return View(this.Globales.gl);
        }


        public ActionResult Aceptar_Monto_2(bool id)
        {
            Globales.vieneDeMsg = true;
            Globales.resMsg = id;
            Globales.Action = Globales.VieneDeAction;
            Globales.VieneDeAction = String.Empty;
            return Rutear(null, null);
        }
        #endregion
    }
}