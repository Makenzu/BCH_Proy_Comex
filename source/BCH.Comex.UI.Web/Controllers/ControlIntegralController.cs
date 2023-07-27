using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Common.Exceptions;
using BCH.Comex.Common.Tracing;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
using BCH.Comex.Core.BL.CONTROLINTEGRAL;
using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using BCH.Comex.UI.Web.Models.ControlIntegral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Caching;
using System.Web.Mvc;
using BCH.Comex.Core.BL.Portal;
using BCH.Comex.UI.Web.Helpers;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using BCH.Comex.UI.Web.Models.ControlIntegral;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Common.Exceptions;
using Microsoft.VisualBasic;
using BCH.Comex.Core.BL.CONTROLINTEGRAL.Modulos;


using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using BCH.Comex.Utils;
using System.Configuration;
using System.Net.Mime;
using RazorPDF;
using System.Net;
using BCH.Comex.Common.Tracing;
using System.Drawing;
using System.Text;
using BCH.Comex.Core.Entities.Custodia.ControlIntegral.DataTypes;
using BCH.Comex.Core.Entities.Custodia;

namespace BCH.Comex.UI.Web.Controllers
{
    public class ControlIntegralController : Controller
    {
        private const string CookieName = "BCHComexXCIN";
        private const string ControlIntegralAppRole = "COMEX_GENERAL_XCIN";
        private ControlIntegralService ControlIntegralService;

        private string Correos = string.Empty;
        private string CorreosFinal = string.Empty;

        private ActionResult Rutear(Action Logica, Func<ActionResult> ObtenerRetorno, bool limpiar = true)
        {
            using (var tracer = new Tracer())
            {
                tracer.AddToContext("limpiar", limpiar);

                if (InitObject != null && limpiar)
                {
                    //this.InitObject.Mdi_Principal.MESSAGES.Clear();
                }

                if (Logica != null) //ejecuta la lógica
                {
                    try
                    {
                        Logica();
                    }
                    catch (Exception ex)
                    {
                        if (ExceptionPolicy.HandleException(ex, "PoliticaUIControlIntegral")) throw;
                    }
                }

                if (this.InitObject == null)
                {
                    throw new ComexApplicationException("Error! Estado no Inicializado.");
                }

                if (String.IsNullOrEmpty(this.InitObject.PaginaWebQueAbrir)) // si no tengo que redireccionar
                {
                    if (ObtenerRetorno == null)
                    {
                        throw new ComexApplicationException("No hay que redireccionar, y la funcion que retorna la vista no esta implementada");
                    }

                    //ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
                    return ObtenerRetorno();
                }
                else // tengo que redireccionar
                {
                    var nuevaAccion = this.InitObject.PaginaWebQueAbrir;
                    this.InitObject.PaginaWebQueAbrir = String.Empty;
                    return RedirectToAction(nuevaAccion);
                }
            }
        }
        static ControlIntegralController()
        {
            new PortalService().RegisterApp("XCIN", "Control Integral (Aplicación de apoyo para la unidad de Cambios)", "Generales", ControlIntegralAppRole, "COMEX_GRP_GENERAL", "ControlIntegral");
        }

        public InitializationObject InitObject
        {
            get
            {
                var res = Session[SessionKeys.ControlIntegral.InitializationObjectKey] as InitializationObject;
                return res;
            }
            set
            {
                Session[SessionKeys.ControlIntegral.InitializationObjectKey] = value;
            }
        }
        public ControlIntegralController()
        {
            this.ControlIntegralService = new ControlIntegralService();
        }

        #region "MIFT - LENGUETA 2"

        [HttpPost]
        public ActionResult txtDVM_KeyPress(IndexViewModel mift)
        {
            if (mift.txtRutM.Trim().Length > 8)
                //txtRutM = Left(txtRutM, Len(txtRutM) - 1)

                if (mift.txtRutM.Length == 0)
                    cmdLimpiarM_Click(mift);


            return Json(new
            {
                txtCuenta = mift.txtCuenta,
                txtRut = mift.txtRut
            });
        }


        #endregion

        #region "MIFT"
        [AuthorizeOrForbidden(Roles = ControlIntegralAppRole)]
        public ActionResult Index()
        {
            this.InitObject = (InitializationObject)Session[SessionKeys.ControlIntegral.InitializationObjectKey];
            IndexViewModel MiftVM = new IndexViewModel();
            return Rutear(() =>
            {
                if (this.InitObject == null) //primera vez que entro al sistema
                {

                    this.InitObject = ControlIntegralService.Inicializar();

                }
                this.InitObject.PaginaWebQueAbrir = String.Empty;
            },
           () =>
           {
               ViewData["listaMoneda"] = ControlIntegralService.CargarMonedas(1);
               ViewData["listaFaxNYM"] = ControlIntegralService.CargarFaxNYM();
               ViewData["listaOtrosM"] = ControlIntegralService.CargarOtrosM();

               cmdLimpiarM_Click(MiftVM);
               return View(MiftVM);
           }, false);
        }
        public void Limpiar_Cta(IndexViewModel mift)
        {
            InitObject.ModFunc.sCuenta = mift.txtCuenta;
            string rdv, rut;
            if (mift.txtCuenta.Length > 1)
            {
                string[] ARut = mift.txtCuenta.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                int result;
                if ((ARut.Length == 2))
                {
                    rut = ARut[0];
                    rdv = ARut[1];
                    if (Int32.TryParse(rut, out result) && (rdv.Length == 1))
                    {
                        if (rdv == ModFunc.Verificar_Rut(int.Parse(rut)))
                        {
                            mift.txtCuenta = (rut + rdv);
                        }
                    }
                }
            }

        }
        public void Limpiar_Resultado(IndexViewModel mift)
        {
            //mift.lblNombreClte.BackColor = &H8000000F
            mift.lblNombreClte = string.Empty;
            mift.HiNombreClte = string.Empty; //Hidden
            mift.txtDV = string.Empty;
            mift.txtBase = string.Empty;
            mift.lblResultado = string.Empty;

            mift.chkCiti = false;
            mift.chkContratoFax = false;
            mift.chkContratoMift = false;
            mift.chkfaxNY = false;
            //chkMail = 0;
            //chkOtros = 0;

            mift.lblChkContratoFax = string.Empty;
            mift.lblChkContratoMift = string.Empty;
            mift.lblChkfaxNY = string.Empty;
            mift.lblChkCiti = string.Empty;
            //chkMail.Caption = ""
            //chkOtros.Caption = ""


            mift.lblSegmento = string.Empty;
            mift.HiSegmento = string.Empty; //Hidden
            mift.lblEjecutivo = string.Empty;
            mift.HiEjecutivo = string.Empty; //Hidden
            mift.lblMensaje = "";
            mift.Frame1 = false;
            //flxCitidocDPL.Cols = 0
            //flxCitidocDPL.Clear
            //flxTarifas.Cols = 0
            //flxTarifas.Clear
            //flxPizarra.Cols = 0
            //flxPizarra.Clear
            InitObject.ModFunc.Paso_Cliente = false;
            InitObject.ModFunc.Paso_Cuentas = false;
            InitObject.ModFunc.Paso_Contratos = false;
            InitObject.ModFunc.Paso_Recurrencia = false;
            InitObject.ModFunc.Paso_Mensajes = false;
            mift.cmdMesa = false;//jquery
            mift.cmdChkList = false; //jquery
            mift.cmdReparo = false; //jquery
            //cmdMesa.Caption = "&Tipos de Cambios"
            //cmdMesa.BackColor = &H8000000F
            //mift.Frame3.Visible = false;
            mift.lblLog2 = string.Empty;
            mift.Error_original = string.Empty;
            InitObject.ModFunc.sCHK = null;

            ControlIntegralService.LimpiarEReparo(InitObject);
            EReparoViewModel eReparoVM = new Models.ControlIntegral.EReparoViewModel(InitObject.frmEReparo);
            ControlIntegralService.LimpiarChkList(InitObject);
            ChkListViewModel chkListVM = new Models.ControlIntegral.ChkListViewModel(InitObject.frmChkList);


        }
        public JsonResult GetDatosOrdenantes(IndexViewModel mift)
        {
            long rut = 0;
            if (InitObject.ModFunc.sRut == null)
                rut = 0;
            else
                rut = long.Parse(InitObject.ModFunc.sRut);

            mift.cmdVRecurrencia = false;

            IList<cambios_mift_cliente_00_MS_Result> datosOrdenante = ControlIntegralService.DatosOrdenante(rut, InitObject.ModFunc.sCuenta);
            foreach (cambios_mift_cliente_00_MS_Result datos in datosOrdenante)
            {

                InitObject.Modulo.AutoChange = false;
                mift.lblSegmento = datos.segmento == null ? string.Empty : datos.segmento.Trim().ToUpper(); //"472425";
                mift.lblEjecutivo = datos.ejecutivo == null ? string.Empty : datos.ejecutivo.Trim().ToUpper(); // "KCATALDOW";
                mift.lblNombreClte = datos.nombre == null ? string.Empty : datos.nombre.Trim().ToUpper();
                mift.est_recurrencia = datos.est_recurrencia == null ? 0 : datos.est_recurrencia;
                if (mift.est_recurrencia == 1)
                    mift.cmdVRecurrencia = true;

                if (string.IsNullOrEmpty(mift.lblSegmento) && string.IsNullOrEmpty(mift.lblNombreClte) && string.IsNullOrEmpty(mift.lblEjecutivo))
                {
                    InitObject.ModFunc.Paso_Contratos = true;
                }
            }
            //if (!InitObject.ModFunc.Paso_Contratos)
            //{
            //    Datos(mift);
            //    Cargar_Contratos(mift);
            //}

            return Json(new
            {
                Data = mift,
                MaxJsonLength = datosOrdenante.Count(),
                Paso_Contratos = InitObject.ModFunc.Paso_Contratos,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            });
        }
        public JsonResult Grabar_Log(IndexViewModel mift)
        {
            long monto = 0;
            string txtRut = string.Empty;
            string txtDV = string.Empty;
            string lblNombreClte = string.Empty;
            string lblSegmento = string.Empty;

            string lblEjecutivo = string.Empty;
            string txtCuentab = string.Empty;
            string txtNombreb = string.Empty;
            string txtBancoib = string.Empty;
            string txtBancopb = string.Empty;
            string sIndDpl = string.Empty;
            string sTxtDpl = string.Empty;
            string lblResultado = string.Empty;
            string lblMensaje = string.Empty;
            string Error_original = string.Empty;
            if (mift.txtMonto == null)
                monto = 0;
            else
                monto = long.Parse(mift.txtMonto);

            string Get_User_Name = "CI";
            txtRut = mift.txtRut == null ? "" : mift.txtRut;
            txtDV = mift.txtDV == null ? "" : mift.txtDV;
            lblNombreClte = mift.lblNombreClte == null ? "" : mift.lblNombreClte;
            lblSegmento = mift.lblEjecutivo == null ? "" : mift.lblEjecutivo;
            lblEjecutivo = mift.lblEjecutivo == null ? "" : mift.lblEjecutivo;
            txtCuentab = mift.txtCuentab == null ? "" : mift.txtCuentab;
            txtNombreb = mift.txtNombreb == null ? "" : mift.txtNombreb;
            txtBancoib = mift.txtBancoib == null ? "" : mift.txtBancoib;
            txtBancopb = mift.txtBancopb == null ? "" : mift.txtBancopb;
            sIndDpl = InitObject.ModFunc.sIndDpl == null ? "" : InitObject.ModFunc.sIndDpl;
            sTxtDpl = InitObject.ModFunc.sTxtDpl == null ? "" : InitObject.ModFunc.sTxtDpl;
            lblResultado = mift.lblResultado == null ? "" : mift.lblResultado;
            lblMensaje = mift.lblMensaje == null ? "" : mift.lblMensaje;
            Error_original = mift.Error_original == null ? "" : mift.Error_original;

            int insert = ControlIntegralService.Cambios_mift_log_insert_2_MS(DateTime.Now.ToString(), DateTime.Now.ToShortDateString(), txtRut, txtDV, mift.txtCuenta, lblNombreClte,
                lblSegmento, lblEjecutivo, mift.cmbMoneda, monto, txtCuentab, txtNombreb, txtBancoib,
                txtBancopb, sIndDpl, mift.chkContratoMift == true ? "SI" : "NO", mift.chkContratoFax == true ? "SI" : "NO", mift.chkCiti == true ? "SI" : "NO",
                mift.chkfaxNY == true ? "SI" : "NO", mift.chkMail == true ? "SI" : "NO", sTxtDpl, lblResultado, lblMensaje.Count() > 99 ? "" : lblMensaje + Error_original, Get_User_Name);

            mift.Error_original = string.Empty;
            InitObject.ModFunc.Paso_Contratos = true;

            //return Json(mift, JsonRequestBehavior.AllowGet);
            return Json(new
            {
                SeGrabo = insert,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            });
        }


        public JsonResult GetListaDeFlxTarifasVerificaTipo()
        {

             decimal monto = 0;
             if (!string.IsNullOrEmpty(InitObject.ModFunc.sMonto))
                 monto = Convert.ToDecimal(InitObject.ModFunc.sMonto);

             IList<cambios_mift_tarifas_01_MS_Result> data = ControlIntegralService.ListaFlxTarifasVerificaTipo(InitObject.ModFunc.sCuenta, InitObject.ModFunc.sMoneda, monto);
            string tipo = "0";
            foreach (cambios_mift_tarifas_01_MS_Result var in data)
            {
                tipo = var.Tipo;
            }

            return Json(new
            {
                Tipo = tipo,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            });
        }

        public JsonResult GetListaDeFlxTarifas()
        {
            object data = ControlIntegralService.ListaFlxTarifas(InitObject.ModFunc.sCuenta, InitObject.ModFunc.sMoneda, Convert.ToDecimal(InitObject.ModFunc.sMonto));
            return new JsonResult()
            {
                Data = data,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult PreflxTarifas(IndexViewModel indexVM)
        {
            if (double.Parse(indexVM.txtMonto) > 0)
                Datos(indexVM);

            return Json(new
            {
                Data = indexVM,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            });
        }
        public JsonResult VerificaExisteValorPizarra(string row, int index)
        {
            bool tCal = false;
            if (row == "Valor Pizarra")
                tCal = true;
            return Json(new
            {
                tCal = tCal,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            });

        }
        public JsonResult flxTarifas_Click(string row, int index, bool tCal)
        {
            string tTar = string.Empty;

            object Observaciones;
            if (tCal)
            {

                tTar = row;
                if (row == "b) Swift" || row == "c) Speed Transfer")
                    Observaciones = ControlIntegralService.ListaFlxPizarra_Obs2_Click("OPC", tTar, InitObject.ModFunc.sMoneda, decimal.Parse(InitObject.ModFunc.sMonto));
                else
                    Observaciones = ControlIntegralService.ListaFlxPizarra_Obs1_Click("OPC", tTar, InitObject.ModFunc.sMoneda, decimal.Parse(InitObject.ModFunc.sMonto));

            }
            else
            {
                Observaciones = ControlIntegralService.ListaFlxPizarra(InitObject.ModFunc.sCuenta);

            }


            return new JsonResult()
            {
                Data = Observaciones,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }

        public JsonResult GetListaDeFlxPizarra()//string pCuenta)
        {
            object Observaciones = ControlIntegralService.ListaFlxPizarra(InitObject.ModFunc.sCuenta);

            //var result = from sec in Observaciones select new[] { sec.Seccion.ToString(), string.Format(formato, sec.MontoSaldoInicial), sec.CantidadCuotas.ToString(), string.Format(formato, sec.MontoCuota), string.Format(formato, sec.MontoSaldoFinal), sec.Seccion.ToString() };
            return new JsonResult()
            {
                Data = Observaciones,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetListaDeFlxCitidocDPL()
        {
            object data = ControlIntegralService.ListaFlxCitidocDPL(this.InitObject, InitObject.ModFunc.sCuenta, Convert.ToDecimal(InitObject.ModFunc.sMonto));
            return new JsonResult()
            {
                Data = data,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetListaDeFlxCitidocDPLHora()
        {
            InitObject.ModFunc.sIndDpl = "NO";
            string horaLog = string.Empty;
            IList<string> Object = ControlIntegralService.cambios_mift_citidoc_duplicados_hora_01_MS();
            foreach (string hora in Object)
            {
                if (hora != null)
                    horaLog = hora.ToString();
                else
                    horaLog = string.Empty;
            }
            return new JsonResult()
            {
                Data = horaLog,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult Cargar_Contratos(IndexViewModel indexVM)
        {
            bool SaveLog = false;
            // Datos(indexVM); //Jfernandez el 19_02_2016
            InitObject.ModFunc.Paso_Contratos = true;


            InitObject.ModFunc.sRut = InitObject.ModFunc.sRut == "" ? null : InitObject.ModFunc.sRut;
            long sRut = 0;
            if (InitObject.ModFunc.sRut != null)
                sRut = short.Parse(InitObject.ModFunc.sRut);


            IList<cambios_mift_contratos_00_MS_Result> datosContratos = ControlIntegralService.cambios_mift_contratos_00_MS(1, sRut, InitObject.ModFunc.sCuenta);

            if (datosContratos.Count > 0)
            {
                foreach (cambios_mift_contratos_00_MS_Result datos in datosContratos)
                {
                    indexVM.Indicador_mift = datos.indicador_mift == null ? string.Empty : datos.indicador_mift;
                    indexVM.Indicador_fax_local = datos.indicador_fax_local == null ? string.Empty : datos.indicador_fax_local;
                    indexVM.Indicador_citi_offshore = datos.indicador_citi_offshore == null ? string.Empty : datos.indicador_citi_offshore;
                    indexVM.Indicador_fax_NY_Londres = datos.indicador_fax_NY_Londres == null ? string.Empty : datos.indicador_fax_NY_Londres;
                    indexVM.Indicador_anexo_mail = datos.indicador_anexo_mail == null ? string.Empty : datos.indicador_anexo_mail;
                    indexVM.Indicador_otros = datos.indicador_otros == null ? string.Empty : datos.indicador_otros;
                    if (indexVM.Indicador_mift.ToUpper() == "NO" && indexVM.Indicador_fax_local.ToUpper() == "NO" && indexVM.Indicador_citi_offshore.ToUpper() == "NO" &&
                        indexVM.Indicador_fax_NY_Londres.ToUpper() == "NO" && indexVM.Indicador_anexo_mail.ToUpper() == "NO" && indexVM.Indicador_otros.ToUpper() == "NO")
                    {


                    }
                    else
                    {
                        indexVM.lblChkContratoMift = datos.contrato_mift == null ? "NO CUMPLE" : datos.contrato_mift.Trim();
                        indexVM.lblChkContratoFax = datos.contrato_fax_local == null ? "NO CUMPLE" : datos.contrato_fax_local.Trim();
                        indexVM.lblChkCiti = datos.contrato_citi_offshore == null ? "NO CUMPLE" : datos.contrato_citi_offshore.Trim();
                        indexVM.lblChkfaxNY = datos.contrato_fax_NY_Londres == null ? "NO CUMPLE" : datos.contrato_fax_NY_Londres.Trim();
                        if (datos.indicador_mift == "SI")
                        {
                            indexVM.chkContratoMift = true;
                        }
                        else
                        {
                            indexVM.chkContratoMift = false;
                        }
                        if (datos.indicador_fax_local == "SI")
                            indexVM.chkContratoFax = true;
                        else
                            indexVM.chkContratoFax = false;

                        if (datos.indicador_citi_offshore == "SI")
                            indexVM.chkCiti = true;
                        else
                            indexVM.chkCiti = false;

                        if (datos.indicador_fax_NY_Londres == "SI")
                            indexVM.chkfaxNY = true;
                        else
                            indexVM.chkfaxNY = false;

                        indexVM.resultado = datos.result_contrato;


                        //          if  (indexVM.resultado == "OK")
                        ////Cambiar_Color (vbGreen)
                        //        else if (indexVM.resultado = "DIS")
                        //        {
                        ////Cambiar_Color (vbRed)
                        ////lblNombreClte.BackColor = vbRed
                        ////lblResultado = "SOC. DISUELTA"
                        ////MsgBox "Advertencia SOCIEDAD DISUELTA", vbOKOnly + vbInformation, "Verificar"
                        //        }
                        //        else
                        ////Cambiar_Color (vbYellow)

                        if ((indexVM.chkContratoMift == true && !InitObject.ModFunc.Paso_Recurrencia) || (indexVM.cmdVRecurrencia && indexVM.txtCuentab != ""))
                        {
                            //      if (double.Parse(indexVM.txtMonto) > 0)
                            //          //Cambiar_Color (vbYellow)
                            //          //Datos
                            //          //if (indexVM.opTipo1)
                            //          //   // Cargar_Recurrencia
                            //          //else
                            //          //    if (indexVM.cmdMesa)
                            //                  //cmdMesa_Click

                            //          //SaveLog = false;

                            //          //Cambiar_Color (vbYellow)
                            //          //lblResultado = "Ingresar Monto"
                            //          //MsgBox "Debe indicar el monto de la operación", vbCritical + vbOKOnly, "Error"
                            //          //txtMonto.SetFocus
                            //          SaveLog = false;

                        }

                        //  if (SaveLog)
                        //Grabar_Log

                    }
                }
            }
            return Json(new
            {
                Data = indexVM,
                Paso_Recurrencia = InitObject.ModFunc.Paso_Recurrencia,
                MaxJsonLength = datosContratos.Count(),
                //Paso_Contratos = InitObject.ModFunc.Paso_Contratos,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            });
        }
        public JsonResult Cargar_Mensajes(IndexViewModel indexVM)
        {
            long rut = 0;
            if (InitObject.ModFunc.sRut == null)
                rut = 0;
            else
                rut = long.Parse(InitObject.ModFunc.sRut);


            IList<cambios_mift_callfax_00_MS_Result> ListaMiftCallfax = ControlIntegralService.cambios_mift_callfax_00_MS(1, rut, InitObject.ModFunc.sCuenta);
            foreach (cambios_mift_callfax_00_MS_Result mensaje in ListaMiftCallfax)
            {
                indexVM.lblMensaje = string.IsNullOrEmpty(mensaje.mensaje_01.Trim()) ? string.Empty : mensaje.mensaje_01.ToUpper();
            }
            byte option = 1;
            if (indexVM.opTipo1 == false)
                option = 2;

            IList<string> ListaMiftMensajes = ControlIntegralService.cambios_mift_mensajes_01_MS(option, InitObject.ModFunc.sCuenta);
            foreach (string mensaje in ListaMiftMensajes)
            {
                indexVM.lblLog2 = mensaje;
            }

            int Estado = 0;
            //int R = 0;
            //int G = 0;
            //int B = 0;
            string color = string.Empty;
            string Mensaje = string.Empty;

            IList<cambios_mift_mesa_cvd_00a_MS_ResultDTO> ListaMiftMesa = ControlIntegralService.cambios_mift_mesa_cvd_00a_MS(1, rut, InitObject.ModFunc.sCuenta, InitObject.ModFunc.sMoneda, decimal.Parse(InitObject.ModFunc.sMonto));
            foreach (cambios_mift_mesa_cvd_00a_MS_ResultDTO mesa in ListaMiftMesa)
            {
                Estado = mesa.ESTADO;
                color = mesa.R.ToString() + mesa.G.ToString() + mesa.B.ToString();
                Mensaje = mesa.MENSAJE;
                indexVM.cmdMesa = true;
            }


            return Json(new
            {
                Data = indexVM,
                cantidadMiftCallFax = ListaMiftCallfax.Count(),
                cantidadMiftMensajes = ListaMiftMensajes.Count(),
                cantidadMiftMesa = ListaMiftMesa.Count(),
                estado = Estado.ToString(),
                color = color,
                mensaje = Mensaje,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            });

        }

        #endregion

        #region "Procedimientos y Funciones"
        private void cmdLimpiar(IndexViewModel indexVM)
        {
            InitObject.Modulo.AutoChange = true;
            indexVM.txtRut = string.Empty;
            indexVM.txtCuenta = string.Empty;
            indexVM.txtMonto = string.Empty;
            indexVM.txtCuentab = string.Empty;
            indexVM.txtNombreb = string.Empty;
            indexVM.txtBancoib = string.Empty;
            indexVM.txtBancopb = string.Empty;
            indexVM.cmdVRecurrencia = false;
            InitObject.Modulo.AutoChange = false;
            Limpiar_Resultado(indexVM);
        }
        private void cmdLimpiarM_Click(IndexViewModel indexVM)
        {
            indexVM.lblNombreClteM = string.Empty;
            indexVM.txtCuentaM = string.Empty;
            indexVM.txtBaseM = string.Empty;
            indexVM.txtRutM = string.Empty;
            indexVM.txtDVM = string.Empty;

            indexVM.chkCitiM = false;
            indexVM.chkContratoFaxM = false;
            indexVM.chkContratoMiftM = false;
            indexVM.chkfaxNYM = false;
            indexVM.chkMailM = false;
            indexVM.chkOtrosM = false;

            indexVM.LblchkCitiM = "SIN CONTRATO CONTINGENCY";
            indexVM.LblchkContratoFaxM = "SIN CONTRATO CONTRATO FAX LOCAL";
            indexVM.LblchkContratoMiftM = "SIN CONTRATO CALLBACK";
            indexVM.LblchkfaxNYM = "NO OPERA CON CITI";
            indexVM.LblchkMailM = "SIN CONTRATO MAIL";
            indexVM.LblchkOtrosM = "SIN CONTRATO OTROS";
            indexVM.lblSegmentoM = string.Empty;
            indexVM.lblEjecutivoM = string.Empty;
            indexVM.cmdAgregar = false;
            indexVM.cmdModificar = false;
            indexVM.cmbFaxNYM = false;
            indexVM.cmbOtrosM = false;
        }

        public ActionResult CmdLimpiar(IndexViewModel indexVM)
        {
            //IndexViewModel indexVM = new Models.ControlIntegral.IndexViewModel();
            cmdLimpiar(indexVM);


            return Json(indexVM, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LimpiarM_click(IndexViewModel indexVM)
        {
            //IndexViewModel indexVM = new Models.ControlIntegral.IndexViewModel();
            cmdLimpiarM_Click(indexVM);
            return Json(indexVM, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region "MIFT Botones Tab 1"

        [HttpPost]
        public ActionResult BotonBuscar(IndexViewModel mift)
        {

            InitObject = (InitializationObject)Session[SessionKeys.ControlIntegral.InitializationObjectKey];
            if (InitObject == null)
            {
                return Redirect("~/ControlIntegral");
            }
            ModelState.Clear();

            Limpiar_Cta(mift); //add el 16-12
            Limpiar_Resultado(mift); //add el 16-12
            Datos(mift);
            mift.Frame1 = false;
            return Json(mift, JsonRequestBehavior.AllowGet);

        }

        private void Datos(IndexViewModel mift)
        {
            mift.txtRut = mift.txtRut == null ? string.Empty : mift.txtRut.Trim();
            mift.txtCuenta = mift.txtCuenta == null ? string.Empty : mift.txtCuenta.Trim();
            mift.lblNombreClte = mift.lblNombreClte == null ? string.Empty : mift.lblNombreClte.Trim();
            mift.txtBase = mift.txtBase == null ? string.Empty : mift.txtBase.Trim();
            mift.txtCuentab = mift.txtCuentab == null ? string.Empty : mift.txtCuentab.Trim();
            mift.txtNombreb = mift.txtNombreb == null ? string.Empty : mift.txtNombreb.Trim();
            mift.txtBancopb = mift.txtBancopb == null ? string.Empty : mift.txtBancopb.Trim();
            mift.txtMonto = mift.txtMonto == null ? string.Empty : mift.txtMonto.Trim();

            if (!string.IsNullOrEmpty(mift.txtRut))
            {
                InitObject.ModFunc.sRut = mift.txtRut.Trim();
                InitObject.ModFunc.sRutr = "'0" + mift.txtRut.Trim() + mift.txtDV.Trim();
            }
            else
            {
                InitObject.ModFunc.sRut = null;
                InitObject.ModFunc.sRutr = null;
            }

            if (!string.IsNullOrEmpty(mift.txtCuenta))
                InitObject.ModFunc.sCuenta = mift.txtCuenta.Trim();
            else
                InitObject.ModFunc.sCuenta = null; //"Null;

            if (!string.IsNullOrEmpty(mift.lblNombreClte))
                InitObject.ModFunc.sNombre = mift.lblNombreClte.Trim();
            else
                InitObject.ModFunc.sNombre = null;

            if (!string.IsNullOrEmpty(mift.txtBase))
                InitObject.ModFunc.sBase = mift.txtBase.Trim();
            else
                InitObject.ModFunc.sBase = null;

            if (!string.IsNullOrEmpty(mift.txtCuentab))
                InitObject.ModFunc.sCuentaB = mift.txtCuentab.Trim();
            else
                InitObject.ModFunc.sCuentaB = null;

            if (!string.IsNullOrEmpty(mift.txtNombreb))
                InitObject.ModFunc.sNombreB = mift.txtNombreb.Trim();
            else
                InitObject.ModFunc.sNombreB = null;

            if (!string.IsNullOrEmpty(mift.txtBancopb))
                InitObject.ModFunc.sBancoBp = mift.txtBancopb.Trim();
            else
                InitObject.ModFunc.sBancoBp = null;

            if (!string.IsNullOrEmpty(mift.cmbMoneda))
                InitObject.ModFunc.sMoneda = mift.cmbMoneda;
            else
                InitObject.ModFunc.sMoneda = null;

            if (!string.IsNullOrEmpty(mift.txtMonto))
                InitObject.ModFunc.sMonto = mift.txtMonto;
            else
                InitObject.ModFunc.sMonto = null;
        }

        private void DatosM(IndexViewModel mift)
        {
            if (mift.txtRutM != null)
                InitObject.ModFunc.sRut = mift.txtRutM.Trim();
            else
                InitObject.ModFunc.sRut = null;

            if (mift.txtCuentaM != null)
                InitObject.ModFunc.sCuenta = mift.txtCuentaM.Trim();
            else
                InitObject.ModFunc.sCuenta = null;

            if (mift.txtBaseM != null)
                InitObject.ModFunc.sBase = mift.txtBaseM.Trim();
            else
                InitObject.ModFunc.sBase = null;

        }

        #endregion

        #region "EMPRESA"

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ActionResult Empresa()
        {
            object model = "1";
            return PartialView("_Empresa", model);
        }
        public JsonResult GetListaDeOrdenantesRazonSocial(byte opcion, String razonSocial)
        {
            object data = ControlIntegralService.BuscarOrdenantes(opcion, razonSocial);
            return new JsonResult()
            {
                Data = data,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #endregion

        #region "EREPARO"
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ActionResult EReparo(IndexViewModel indexVM)
        {
            InitObject = (InitializationObject)Session[SessionKeys.ControlIntegral.InitializationObjectKey];

            if (InitObject == null)
            {
                return Redirect("~/ControlIntegral");
            }
            ModelState.Clear();
            InitObject.ModFunc.Datos = new string[3];

            ControlIntegralService.InitComboDocName(InitObject);
            ControlIntegralService.LimpiarEReparo(InitObject);
            EReparoViewModel reparoVM = new EReparoViewModel(InitObject.frmEReparo);

            string rut = string.Empty;
            int result;

            InitObject.ModFunc.Datos[1] = indexVM.HiNombreClte;
            InitObject.ModFunc.Datos[2] = InitObject.ModFunc.sMonto;


            if (Int32.TryParse(InitObject.ModFunc.Datos[0], out result))
            {
                int largoRut = InitObject.ModFunc.Datos[0].Trim().Length;
                InitObject.ModFunc.Datos[0] = InitObject.ModFunc.Datos[0].Substring(0, (largoRut - 1)) + "-" + InitObject.ModFunc.Datos[0].Substring((largoRut - 1), 1); //"76415140-2";

            }
            Correos = string.Empty;
            CorreosFinal = string.Empty;

            if (!string.IsNullOrEmpty(indexVM.HiEjecutivo))
            {
                Correos = Correos + indexVM.HiEjecutivo + ";";
                CorreosFinal = CorreosFinal + indexVM.HiEjecutivo + "@bancochile.cl" + ",";
            }

            if (!string.IsNullOrEmpty(indexVM.HiSegmento))
            {
                Correos = Correos + indexVM.HiSegmento + ";";
                CorreosFinal = CorreosFinal + indexVM.HiSegmento + "@bancochile.cl";
            }
            reparoVM.NombreCliente = InitObject.ModFunc.Datos[1];
            reparoVM.MailEjecutivo = Correos;
            reparoVM.MailEjecutivoCorreo = CorreosFinal;

            reparoVM.txtOtros_0.Text = string.Empty;
            reparoVM.txtOtros_1.Text = string.Empty;
            reparoVM.txtOtros_2.Text = string.Empty;
            reparoVM.txtOtros_3.Text = string.Empty;
            reparoVM.txtOtros_4.Text = string.Empty;

            return PartialView("_EReparo", reparoVM);
        }
        public ActionResult EReparo_chkReparo_ChkOtros_Lista_Click(string elem, bool value)
        {
            // ModelState.Clear();
            // ViewData["listaDocName"] = "";
            switch (elem)
            {
                case "chkReparo_0_Checked":
                    InitObject.frmEReparo.chkReparo[0].ID = elem;
                    InitObject.frmEReparo.chkReparo[0].Tag = elem;
                    InitObject.frmEReparo.chkReparo[0].Checked = value;
                    break;
                case "chkReparo_1_Checked":
                    InitObject.frmEReparo.chkReparo[1].ID = elem;
                    InitObject.frmEReparo.chkReparo[1].Tag = elem;
                    InitObject.frmEReparo.chkReparo[1].Checked = value;
                    break;
                case "chkReparo_2_Checked":
                    InitObject.frmEReparo.chkReparo[2].ID = elem;
                    InitObject.frmEReparo.chkReparo[2].Tag = elem;
                    InitObject.frmEReparo.chkReparo[2].Checked = value;
                    break;
                case "chkReparo_3_Checked":
                    InitObject.frmEReparo.chkReparo[3].ID = elem;
                    InitObject.frmEReparo.chkReparo[3].Tag = elem;
                    InitObject.frmEReparo.chkReparo[3].Checked = value;
                    break;
                case "chkReparo_4_Checked":
                    InitObject.frmEReparo.chkReparo[4].ID = elem;
                    InitObject.frmEReparo.chkReparo[4].Tag = elem;
                    InitObject.frmEReparo.chkReparo[4].Checked = value;
                    break;
                case "chkReparo_5_Checked":
                    InitObject.frmEReparo.chkReparo[5].ID = elem;
                    InitObject.frmEReparo.chkReparo[5].Tag = elem;
                    InitObject.frmEReparo.chkReparo[5].Checked = value;
                    break;
                case "chkReparo_6_Checked":
                    InitObject.frmEReparo.chkReparo[6].ID = elem;
                    InitObject.frmEReparo.chkReparo[6].Tag = elem;
                    InitObject.frmEReparo.chkReparo[6].Checked = value;
                    break;
                case "chkReparo_7_Checked":
                    InitObject.frmEReparo.chkReparo[7].ID = elem;
                    InitObject.frmEReparo.chkReparo[7].Tag = elem;
                    InitObject.frmEReparo.chkReparo[7].Checked = value;
                    break;
                case "chkReparo_8_Checked":
                    InitObject.frmEReparo.chkReparo[8].ID = elem;
                    InitObject.frmEReparo.chkReparo[8].Tag = elem;
                    InitObject.frmEReparo.chkReparo[8].Checked = value;
                    break;
                case "chkReparo_9_Checked":
                    InitObject.frmEReparo.chkReparo[9].ID = elem;
                    InitObject.frmEReparo.chkReparo[9].Tag = elem;
                    InitObject.frmEReparo.chkReparo[9].Checked = value;
                    break;
                case "chkReparo_10_Checked":
                    InitObject.frmEReparo.chkReparo[10].ID = elem;
                    InitObject.frmEReparo.chkReparo[10].Tag = elem;
                    InitObject.frmEReparo.chkReparo[10].Checked = value;

                    break;
                case "chkReparo_11_Checked":
                    InitObject.frmEReparo.chkReparo[11].ID = elem;
                    InitObject.frmEReparo.chkReparo[11].Tag = elem;
                    InitObject.frmEReparo.chkReparo[11].Checked = value;

                    break;
                case "chkReparo_12_Checked":
                    InitObject.frmEReparo.chkReparo[12].ID = elem;
                    InitObject.frmEReparo.chkReparo[12].Tag = elem;
                    InitObject.frmEReparo.chkReparo[12].Checked = value;

                    break;
                case "chkReparo_13_Checked":
                    InitObject.frmEReparo.chkReparo[13].ID = elem;
                    InitObject.frmEReparo.chkReparo[13].Tag = elem;
                    InitObject.frmEReparo.chkReparo[13].Checked = value;

                    break;
                case "chkReparo_14_Checked":
                    InitObject.frmEReparo.chkReparo[14].ID = elem;
                    InitObject.frmEReparo.chkReparo[14].Tag = elem;
                    InitObject.frmEReparo.chkReparo[14].Checked = value;

                    break;
                case "chkReparo_15_Checked":
                    InitObject.frmEReparo.chkReparo[15].ID = elem;
                    InitObject.frmEReparo.chkReparo[15].Tag = elem;
                    InitObject.frmEReparo.chkReparo[15].Checked = value;

                    break;
                case "chkReparo_16_Checked":
                    InitObject.frmEReparo.chkReparo[16].ID = elem;
                    InitObject.frmEReparo.chkReparo[16].Tag = elem;
                    InitObject.frmEReparo.chkReparo[16].Checked = value;
                    break;

                case "chkReparo_17_Checked":
                    InitObject.frmEReparo.chkReparo[17].ID = elem;
                    InitObject.frmEReparo.chkReparo[17].Tag = elem;
                    InitObject.frmEReparo.chkReparo[17].Checked = value;

                    break;

                case "chkReparo_18_Checked":
                    InitObject.frmEReparo.chkReparo[18].ID = elem;
                    InitObject.frmEReparo.chkReparo[18].Tag = elem;
                    InitObject.frmEReparo.chkReparo[18].Checked = value;

                    break;

                case "chkReparo_19_Checked":
                    InitObject.frmEReparo.chkReparo[19].ID = elem;
                    InitObject.frmEReparo.chkReparo[19].Tag = elem;
                    InitObject.frmEReparo.chkReparo[19].Checked = value;
                    break;
                case "chkOtros_0_Checked":
                    InitObject.frmEReparo.chkOtros[0].ID = elem;
                    InitObject.frmEReparo.chkOtros[0].Tag = elem;
                    InitObject.frmEReparo.chkOtros[0].Checked = value;
                    break;
                case "chkOtros_1_Checked":
                    InitObject.frmEReparo.chkOtros[1].ID = elem;
                    InitObject.frmEReparo.chkOtros[1].Tag = elem;
                    InitObject.frmEReparo.chkOtros[1].Checked = value;
                    break;
                case "chkOtros_2_Checked":
                    InitObject.frmEReparo.chkOtros[2].ID = elem;
                    InitObject.frmEReparo.chkOtros[2].Tag = elem;
                    InitObject.frmEReparo.chkOtros[2].Checked = value;
                    break;
                case "chkOtros_3_Checked":
                    InitObject.frmEReparo.chkOtros[3].ID = elem;
                    InitObject.frmEReparo.chkOtros[3].Tag = elem;
                    InitObject.frmEReparo.chkOtros[3].Checked = value;
                    break;
                case "chkOtros_4_Checked":
                    InitObject.frmEReparo.chkOtros[4].ID = elem;
                    InitObject.frmEReparo.chkOtros[4].Tag = elem;
                    InitObject.frmEReparo.chkOtros[4].Checked = value;
                    break;
            }
            EReparoViewModel reparoVM = new EReparoViewModel(this.InitObject.frmEReparo);

            reparoVM.Frame1 = "ATRIBUCIONES DE FIRMA EN IUV:";
            reparoVM.Frame2 = "[ MIFT / SVS ]";
            reparoVM.Frame3 = "[ ESTRUCTURA DE CARTA: ]";
            reparoVM.Frame4 = " [ CIERRE TIPO DE CAMBIO: ]";
            reparoVM.Frame5 = "[ OTROS REPAROS: ]";


            return PartialView("_EReparo", reparoVM);
        }
        public ActionResult EReparo_btnGenerar_Click(EReparoViewModel reparoVM)
        {
            InitObject.ModFunc.sCHK = null;
            ControlIntegralService.LimpiarChkList(InitObject);
            InitObject.frmEReparo.cmbDocName.ListIndex = InitObject.frmEReparo.cmbDocName.Items.FindIndex(x => x.Data == reparoVM.cmbDocName.SelectedValue);
            InitObject.frmEReparo.txtOtros[0].Text = reparoVM.txtOtros_0.Text == null ? "" : reparoVM.txtOtros_0.Text;
            InitObject.frmEReparo.txtOtros[1].Text = reparoVM.txtOtros_1.Text == null ? "" : reparoVM.txtOtros_1.Text;
            InitObject.frmEReparo.txtOtros[2].Text = reparoVM.txtOtros_2.Text == null ? "" : reparoVM.txtOtros_2.Text;
            InitObject.frmEReparo.txtOtros[3].Text = reparoVM.txtOtros_3.Text == null ? "" : reparoVM.txtOtros_3.Text;
            InitObject.frmEReparo.txtOtros[4].Text = reparoVM.txtOtros_4.Text == null ? "" : reparoVM.txtOtros_4.Text;

            ControlIntegralService.BotonGenerar(InitObject, "", reparoVM.NombreCliente, "", reparoVM.MailEjecutivo, "USER");


            reparoVM = new EReparoViewModel(InitObject.frmEReparo);


            return Json(reparoVM, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult Cb_DocumentName(int selectedItem)
        //{
        //    ModelState.Clear();
        //    InitObject.frmEReparo.cmbDocName.ListIndex = InitObject.frmEReparo.cmbDocName.Items.FindIndex(x => x.Data == selectedItem);
        //    EReparoViewModel cp = new EReparoViewModel(this.InitObject.frmEReparo);
        //    return Json(cp);
        //}

        #endregion

        #region "CHKLIST"
        public ActionResult CheckListControl()
        {

            ControlIntegralService.LimpiarEReparo(InitObject);
            EReparoViewModel eReparoVM = new EReparoViewModel(InitObject.frmEReparo);


            ControlIntegralService.Main(InitObject);
            ChkListViewModel optCtrl1 = new ChkListViewModel(InitObject.frmChkList);


            return PartialView("_ChkList", optCtrl1);
        }

        public JsonResult BotonCopiar(string TextoBin)
        {
            decimal monto = 0;
            int estado = 0;
            if (!string.IsNullOrEmpty(TextoBin))
            {
                monto = InitObject.ModFunc.sMonto == null ? 0 : decimal.Parse(InitObject.ModFunc.sMonto);

                ControlIntegralService.BotonCopiar(InitObject.ModFunc.sCuenta, InitObject.ModFunc.sMoneda, monto, "User", TextoBin);
                estado = 1;

            }
            return Json(new
            {
                estado = estado.ToString(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            });

        }


        #endregion

        #region "DETALLE"
        public ActionResult Cargar_Recurrencia(int? opcion)
        {
            short sRut = 0;
            if (InitObject.ModFunc.sRut != null)
                sRut = short.Parse(InitObject.ModFunc.sRut);
            else
                sRut = 0;
            ViewBag.opcion = opcion;
            return PartialView("_Detalle");
        }
        
        public JsonResult CantidadRegistrosRecurrencia()
        {
            short sRut = 0;
            if (InitObject.ModFunc.sRut != null)
                sRut = short.Parse(InitObject.ModFunc.sRut);
            else
                sRut = 0;
            object data = ControlIntegralService.ListaFlxDetalle(sRut, InitObject.ModFunc.sCuenta, InitObject.ModFunc.sBancoBp, InitObject.ModFunc.sCuentaB, InitObject.ModFunc.sNombreB, InitObject.ModFunc.sMoneda, decimal.Parse(InitObject.ModFunc.sMonto));


            return Json(new
            {
                RegistrosRecurrencia = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            });

        }
        public ActionResult GetListaDeFlxDetalle()
        {
            InitObject.ModFunc.sRut = InitObject.ModFunc.sRut == "" ? null : InitObject.ModFunc.sRut;
            short sRut = 0;
            if (InitObject.ModFunc.sRut != null)
                sRut = short.Parse(InitObject.ModFunc.sRut);
            else
                sRut = 0;
            object data = ControlIntegralService.ListaFlxDetalle(sRut, InitObject.ModFunc.sCuenta, InitObject.ModFunc.sBancoBp, InitObject.ModFunc.sCuentaB, InitObject.ModFunc.sNombreB, InitObject.ModFunc.sMoneda, decimal.Parse(InitObject.ModFunc.sMonto));

            return new JsonResult()
            {
                Data = data,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult GetRecurrencia(string Cuentab, string Nombreb, string Bancopb)
        {

            short sRut = 0;
            string sCuentaB = null;
            string sNombreB = null;
            string sBancoBp = null;

            InitObject.ModFunc.sRut = InitObject.ModFunc.sRut == "" ? null : InitObject.ModFunc.sRut;

            InitObject.ModFunc.sCuentaB = Cuentab == "" ? null : Cuentab;

            InitObject.ModFunc.sNombreB = Nombreb == "" ? null : Nombreb;
            InitObject.ModFunc.sBancoBp = Bancopb == "" ? null : Bancopb;

            if (InitObject.ModFunc.sRut != null)
                sRut = short.Parse(InitObject.ModFunc.sRut);
            else
                sRut = 0;

            if (InitObject.ModFunc.sCuentaB != null)
                sCuentaB = InitObject.ModFunc.sCuentaB;


            if (InitObject.ModFunc.sNombreB != null)
                sNombreB = InitObject.ModFunc.sNombreB;


            if (InitObject.ModFunc.sBancoBp != null)
                sBancoBp = InitObject.ModFunc.sBancoBp;

            //object data = ControlIntegralService.ListaFlxDetalle(sRut, InitObject.ModFunc.sCuenta, InitObject.ModFunc.sBancoBp, InitObject.ModFunc.sCuentaB, InitObject.ModFunc.sNombreB, InitObject.ModFunc.sMoneda, decimal.Parse(InitObject.ModFunc.sMonto));
            object data = ControlIntegralService.ListaFlxDetalle(sRut, InitObject.ModFunc.sCuenta, sBancoBp, sCuentaB, sNombreB, InitObject.ModFunc.sMoneda, decimal.Parse(InitObject.ModFunc.sMonto));
            return new JsonResult()
            {
                Data = data,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        public ActionResult BotonBuscarRecurrencia(IndexViewModel mift)
        {

            InitObject = (InitializationObject)Session[SessionKeys.ControlIntegral.InitializationObjectKey];
            if (InitObject == null)
            {
                return Redirect("~/ControlIntegral");
            }
            //ModelState.Clear();

            //Limpiar_Cta(mift); //add el 16-12
            //Limpiar_Resultado(mift); //add el 16-12
            Datos(mift);
            // mift.Frame1 = false;
            return Json(mift, JsonRequestBehavior.AllowGet);

        }


        public ActionResult cambios_mift_recurrencia_01b(IndexViewModel mift)
        {
            string sBancoBp = null;
            string sCuentaB = null;
            decimal? Monto;

            InitObject.ModFunc.sRut = InitObject.ModFunc.sRut == "" ? null : InitObject.ModFunc.sRut;
            string sRut = "0";
            if (InitObject.ModFunc.sRut != null)
                sRut = InitObject.ModFunc.sRut;
            else
                sRut = null;


            if (InitObject.ModFunc.sBancoBp != null)
                sBancoBp = InitObject.ModFunc.sBancoBp;

            if (InitObject.ModFunc.sCuentaB != null)
                sCuentaB = InitObject.ModFunc.sCuentaB;

            Monto = decimal.Parse(InitObject.ModFunc.sMonto);
            object data = ControlIntegralService.cambios_mift_recurrencia_02_MS(1, sRut, InitObject.ModFunc.sCuenta, sBancoBp, sCuentaB, InitObject.ModFunc.sMoneda, Monto);

            return Json(new
            {
                retorno = data


            });


        }


        #endregion

        #region "DETALLE MESA"

        public ActionResult DetalleMesa()
        {
            return PartialView("_DetalleMesa");
        }
        public ActionResult GetListaDeFlxMesa()
        {
            InitObject.ModFunc.sRut = InitObject.ModFunc.sRut == "" ? null : InitObject.ModFunc.sRut;
            short sRut = 0;
            if (InitObject.ModFunc.sRut != null)
                sRut = short.Parse(InitObject.ModFunc.sRut);
            else
                sRut = 0;
            object data = ControlIntegralService.ListaFlxDetalleMesa(1, sRut, InitObject.ModFunc.sCuenta, InitObject.ModFunc.sMoneda, decimal.Parse(InitObject.ModFunc.sMonto));

            return new JsonResult()
            {
                Data = data,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #endregion

        #region "MIFT Botones Tab 2"


        #endregion

        #region "MIFT - Tab 2 - CheckList Contratos Firmados"

        public ActionResult CheckListContratosFirmados_click(string elem, string value)
        {

            //switch (elem)
            //{
            //    case "chkContratoFaxM":
            //        if (bool.Parse(value) == true)
            //            chkContratoMiftM = "CONTRATO CALLBACK OK";
            //        else
            //            chkContratoMiftM.Caption = "SIN CONTRATO CALLBACK";

            //            break;

            //    case "chkCitiM":
            //        break;


            //}

            //InitObject.frmEReparo.txtOtros[0].Text = reparoVM.txtOtros_0.Text == null ? "" : reparoVM.txtOtros_0.Text;
            //InitObject.frmEReparo.txtOtros[1].Text = reparoVM.txtOtros_1.Text == null ? "" : reparoVM.txtOtros_1.Text;
            //InitObject.frmEReparo.txtOtros[2].Text = reparoVM.txtOtros_2.Text == null ? "" : reparoVM.txtOtros_2.Text;
            //InitObject.frmEReparo.txtOtros[3].Text = reparoVM.txtOtros_3.Text == null ? "" : reparoVM.txtOtros_3.Text;
            //InitObject.frmEReparo.txtOtros[4].Text = reparoVM.txtOtros_4.Text == null ? "" : reparoVM.txtOtros_4.Text;



            return Json(elem, JsonRequestBehavior.AllowGet);
        }




        #endregion

        #region "ENVIO MENSAJE DESDE EJECUTIVO Y SEGMENTO"

        [AcceptVerbs(HttpVerbs.Get), FileDownload]
        public ActionResult DescargarMailCI(string ejecutivo, string segmento)
        {
            string Correos = string.Empty;
            if (!string.IsNullOrEmpty(ejecutivo))
                Correos = Correos + ejecutivo + "@bancochile.cl" + ",";

            if (!string.IsNullOrEmpty(segmento))
                Correos = Correos + segmento + "@bancochile.cl" + "";

            return DescargarMailControlIntegral(Correos);
        }

        #endregion

        #region "ENVIO MENSAJE DESDE EREPARO"
        [AcceptVerbs(HttpVerbs.Get), FileDownload]
        public ActionResult DescargaEreparo(string MailEjecutivoCorreo)
        {
            EnvioMensajeEReparo EnvioMensaje = new EnvioMensajeEReparo();

            if (!string.IsNullOrEmpty(InitObject.frmEReparo.cmbDocName.Text.Trim()))
                EnvioMensaje.Asunto = "Aviso de Reparo Operación : " + InitObject.ModFunc.Datos[0] + " " + InitObject.ModFunc.Datos[1] + " - " + InitObject.frmEReparo.cmbDocName.Text;
            else
                EnvioMensaje.Asunto = "Aviso de Reparo Operación : " + InitObject.ModFunc.Datos[0] + " " + InitObject.ModFunc.Datos[1];

            string Datos0 = string.Empty;
            string Datos1 = string.Empty;
            string Datos2 = string.Empty;
            string DocumentName = string.Empty;
            Datos0 = InitObject.ModFunc.Datos[0] == null ? string.Empty : InitObject.ModFunc.Datos[0];
            Datos1 = InitObject.ModFunc.Datos[1] == null ? string.Empty : InitObject.ModFunc.Datos[1];
            Datos2 = InitObject.ModFunc.Datos[2] == null ? string.Empty : InitObject.ModFunc.Datos[2];
            DocumentName = InitObject.frmEReparo.cmbDocName.Text == null ? "" : InitObject.frmEReparo.cmbDocName.Text;

            if (!string.IsNullOrEmpty(MailEjecutivoCorreo))
            {
                EnvioMensaje.MailEjecutivoCorreo = MailEjecutivoCorreo;
            }
            EnvioMensaje.Rut = Datos0;
            EnvioMensaje.Cliente = Datos1;
            EnvioMensaje.Monto = Datos2;
            EnvioMensaje.Document_Name = DocumentName.ToString();

            return DescargarMailEReparo(EnvioMensaje);
        }

        #endregion


        #region Private methods


        private ActionResult DescargarMailEReparo(EnvioMensajeEReparo envioMensajeEreparo)
        {
            using (var tracer = new Tracer())
            {
                if (envioMensajeEreparo != null)
                {
                    string from = String.Empty;
                    string to = string.Empty;
                    try
                    {
                        var usuario = HttpContext.GetCurrentUser();
                        tracer.AddToContext("email", usuario.EMail);
                        from = usuario.EMail;
                    }
                    catch (Exception ex)
                    {
                        tracer.TraceException("Alerta", ex);
                    }
                    if (String.IsNullOrEmpty(from))
                    {
                        from = "especialista@bancochile.cl";
                    }
                    if (!String.IsNullOrEmpty(envioMensajeEreparo.MailEjecutivoCorreo))
                    {
                        to = envioMensajeEreparo.MailEjecutivoCorreo;
                    }
                    string htmlBody = BotonGeneraListaReparo(envioMensajeEreparo);
                    AlternateView alternateViewHtml = AlternateView.CreateAlternateViewFromString(htmlBody, Encoding.UTF8, MediaTypeNames.Text.Html);
                    string path = Server.MapPath(@"~/content/img/logo.jpg");
                    string pathPie = Server.MapPath(@"~/content/img/pie.jpg");
                    LinkedResource windowsLogo = new LinkedResource(path, MediaTypeNames.Image.Jpeg);
                    LinkedResource windowsLogoPie = new LinkedResource(pathPie, MediaTypeNames.Image.Jpeg);
                    windowsLogo.ContentId = "WinLogo";
                    windowsLogoPie.ContentId = "WinLogoPie";
                    alternateViewHtml.LinkedResources.Add(windowsLogo);
                    alternateViewHtml.LinkedResources.Add(windowsLogoPie);

                    string subject = envioMensajeEreparo.Asunto;

                    MailMessage message = new MailMessage(from, to);
                    message.Subject = envioMensajeEreparo.Asunto;
                    message.AlternateViews.Add(alternateViewHtml);

                    message.IsBodyHtml = true;
                    byte[] fileContent = message.SaveToStream();
                    FileContentResult result = new FileContentResult(fileContent, "message/rfc822");
                    result.FileDownloadName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".eml"; //genero un nombre de archivo aux con la fecha del momento
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        public string BotonGeneraListaReparo(EnvioMensajeEReparo Model)
        {
            string CadenaCompleta = string.Empty;
            string table = string.Empty;

            CadenaCompleta = "";
            CadenaCompleta += "<html lang='es'>";
            CadenaCompleta += "<style>";
            CadenaCompleta += "#divCIEReparo .form-control {";
            CadenaCompleta += "display: block;";
            CadenaCompleta += "height: 18px;";
            CadenaCompleta += "padding: 6px 12px;";
            CadenaCompleta += "font-size: 12px;";
            CadenaCompleta += "line-height: 1.42857143;";
            CadenaCompleta += "color: #555;";
            CadenaCompleta += "background-color: #fff;";
            CadenaCompleta += "background-image: none;";
            CadenaCompleta += "border-radius: 4px;";
            CadenaCompleta += "-webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);";
            CadenaCompleta += "box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075)";
            CadenaCompleta += "-webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;";
            CadenaCompleta += "-o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;";
            CadenaCompleta += "transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;";
            CadenaCompleta += "}";
            CadenaCompleta += "   ";
            CadenaCompleta += "#divCIEReparo .small {";
            CadenaCompleta += "font-size: 80%;";
            CadenaCompleta += "}";
            CadenaCompleta += "";
            CadenaCompleta += "#divCIEReparo .otros2 {";
            CadenaCompleta += "font-family: Verdana, Arial, Helvetica, sans-serif";
            CadenaCompleta += "font-size: 8.0pt;";
            CadenaCompleta += "color:  red;";
            CadenaCompleta += "}";
            CadenaCompleta += "  ";
            CadenaCompleta += "#divCIEReparo .form-group-sm {";
            CadenaCompleta += "height: 15px;";
            CadenaCompleta += "padding: 5px 10px;";
            CadenaCompleta += "font-size: 11px;";
            CadenaCompleta += "line-height: 1.5;";
            CadenaCompleta += "border-radius: 3px;";
            CadenaCompleta += "}";
            CadenaCompleta += "    ";
            CadenaCompleta += "</style>";
            CadenaCompleta += "<body>";
            CadenaCompleta += "<div id='divCIEReparo'>";
            CadenaCompleta += "<table width='95%' border='0' align='center' cellpadding='0' cellspacing='0'>";
            CadenaCompleta += "<tr>";
            CadenaCompleta += "<td width='15%' height='24' valign='bottom'>";
            CadenaCompleta += "<img src='cid:WinLogo' />";
            CadenaCompleta += "</td>";
            CadenaCompleta += "<td width='70%' height='24' align='center' class='encabezado'><span class='encabezadotop1'>AVISO DE REPARO PARA OPERACIÓN DE CAMBIOS INTERNACIONALES</span></td>";
            CadenaCompleta += "<td width='15%'></td>";
            CadenaCompleta += "</tr>";
            CadenaCompleta += " </table>";
            CadenaCompleta += "<br>";
            CadenaCompleta += "<br>";
            CadenaCompleta += "<table width='95%' border='0' align='center' cellpadding='0' cellspacing='0'>";
            CadenaCompleta += "<tr>";
            CadenaCompleta += "<td height='25' align='left' class='encabezadotop2'>Estimado Equipo de Cubertura</td>";
            CadenaCompleta += "<td height='25' class='encabezadotop2'>&nbsp;</td>";
            CadenaCompleta += "</tr>";
            CadenaCompleta += "<tr>";
            CadenaCompleta += " <td colspan='3' width='100%' class='texto'><p align='left'>Informamos a ustedes que en controles realizados por la operación indicada en la referencia hemos detectado los siguientes reparos que impiden su proceso.</p></td>";
            CadenaCompleta += "</tr>";
            CadenaCompleta += "<tr><td><br /></td></tr>";
            CadenaCompleta += "<tr>";
            CadenaCompleta += "<td width='20%' height='15' align='left' class='texto'>Rut</td>";
            CadenaCompleta += "<td width='4%' class='texto'>:</td>";
            CadenaCompleta += "<td width='55%' class='texto'>" + Model.Rut + "</td>";
            CadenaCompleta += "<td width='21%'></td>";
            CadenaCompleta += "</tr>";
            CadenaCompleta += "<tr>";
            CadenaCompleta += "<td width='20%' height='15' align='left' class='texto'>Cliente</td>";
            CadenaCompleta += "<td width='4%' class='texto'>:</td>";
            CadenaCompleta += "<td width='55%' class='texto'>" + Model.Cliente + "</td>";
            CadenaCompleta += "<td width='21%'></td>";
            CadenaCompleta += "</tr>";
            CadenaCompleta += "<tr>";
            CadenaCompleta += "<td width='20%' height='15' align='left' class='texto'>Monto</td>";
            CadenaCompleta += "<td width='4%' class='texto'>:</td>";
            CadenaCompleta += "<td width='55%' class='texto'>" + Model.Monto + "</td>";
            CadenaCompleta += "<td width='21%'></td>";
            CadenaCompleta += "</tr>";
            CadenaCompleta += "<tr>";
            CadenaCompleta += "<td width='20%' height='15' align='left' class='texto'>Document Name</td>";
            CadenaCompleta += "<td width='4%' class='texto'>:</td>";
            CadenaCompleta += "<td width='55%' class='texto'>" + Model.Document_Name + "</td>";
            CadenaCompleta += "<td width='21%'></td>";
            CadenaCompleta += "</tr>";
            CadenaCompleta += "</table>";

            CadenaCompleta += "<table width='95%' border='0' align='center' cellpadding='0' cellspacing='0'>";
            if (InitObject.frmEReparo.chkReparo[0].Checked || InitObject.frmEReparo.chkReparo[1].Checked || InitObject.frmEReparo.chkReparo[2].Checked || InitObject.frmEReparo.chkReparo[3].Checked || InitObject.frmEReparo.chkReparo[4].Checked || (InitObject.frmEReparo.chkOtros[0].Checked || InitObject.frmEReparo.txtOtros[0].Text.Trim().Length > 0))
            {
                if (InitObject.frmEReparo.chkReparo[0].Checked || InitObject.frmEReparo.chkReparo[1].Checked)
                {
                    CadenaCompleta += "<tr><td><br /></td></tr>";
                    CadenaCompleta += "<tr>";
                    CadenaCompleta += "<td class='form-control'><b>[ ATRIBUCIONES DE FIRMA EN IUV: ]</b></td>";
                    CadenaCompleta += "</tr>";
                    CadenaCompleta += "<tr><td><br /></td></tr>";
                }
                else
                {
                    CadenaCompleta += "<tr><td><br /></td></tr>";
                    CadenaCompleta += "<tr>";
                    CadenaCompleta += "<td class='form-control'><b>[ ATRIBUCIONES DE FIRMA EN IUV: ]</b></td>";
                    CadenaCompleta += "</tr>";
                    CadenaCompleta += "<tr><td><br /></td></tr>";
                }
                CadenaCompleta += "<tr>";
                CadenaCompleta += "<td>";
                CadenaCompleta += "<table width='100%' cellspacing='0' cellpadding='0'>";

                if (InitObject.frmEReparo.chkReparo[0].Checked)
                {
                    CadenaCompleta += "<tr>";
                    CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                    CadenaCompleta += "<td width='1%'></td>";
                    CadenaCompleta += "<td width='5%' class='form-group-sm small'></td>";
                    CadenaCompleta += "<td width='97%' align='left' class='form-group-sm small'>FALTA FIRMA APODERADO</td>";
                    CadenaCompleta += "</tr>";
                }

                if (InitObject.frmEReparo.chkReparo[1].Checked)
                {
                    CadenaCompleta += "<tr>";
                    CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                    CadenaCompleta += "<td width='1%'></td>";
                    CadenaCompleta += "<td width='5%' class='form-group-sm small'></td>";
                    CadenaCompleta += "<td width='97%' align='left' class='form-group-sm small'>APODERADOS SIN FACULTADES SUFICIENTES PARA :</td>";
                    CadenaCompleta += "</tr>";

                    if (InitObject.frmEReparo.chkReparo[17].Checked || InitObject.frmEReparo.chkReparo[18].Checked || InitObject.frmEReparo.chkReparo[19].Checked)
                    {
                        if (InitObject.frmEReparo.chkReparo[17].Checked)
                        {
                            CadenaCompleta += "<td width='2%'></td>";
                            CadenaCompleta += "<td width='1%'></td>";
                            CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-group-sm small' align='center'>X</td>";
                            CadenaCompleta += "<td width='97%' style='border: 1px solid black; color:black;' align='left' class='form-group-sm small'>Facultad 2 (Giro en cuenta corriente)</td>";
                            CadenaCompleta += "</tr>";
                        }
                        if (InitObject.frmEReparo.chkReparo[18].Checked)
                        {
                            CadenaCompleta += "<tr>";
                            CadenaCompleta += "<td width='2%'></td>";
                            CadenaCompleta += "<td width='1%'></td>";
                            CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-group-sm small' align='center'>X</td>";
                            CadenaCompleta += "<td width='97%' align='left' style='border: 1px solid black; color:black;' class='form-group-sm small'>Facultad 17 (Adquirir bienes inmuebles)</td>";
                            CadenaCompleta += "</tr>";
                        }
                        if (InitObject.frmEReparo.chkReparo[19].Checked)
                        {
                            CadenaCompleta += "<tr>";
                            CadenaCompleta += "<td width='2%'></td>";
                            CadenaCompleta += "<td width='1%'></td>";
                            CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-group-sm small' align='center'>X</td>";
                            CadenaCompleta += "<td width='97%' align='left' style='border: 1px solid black; color:black;' class='form-group-sm small'>Facultad 25 /27 (Efectuar operaciones de Cambio y Comex)</td>";
                            CadenaCompleta += "</tr>";
                        }
                    }
                }


                if (InitObject.frmEReparo.chkReparo[2].Checked || InitObject.frmEReparo.chkReparo[3].Checked || InitObject.frmEReparo.chkReparo[4].Checked || (InitObject.frmEReparo.chkOtros[0].Checked && (InitObject.frmEReparo.txtOtros[0].Text.Trim().Length) > 0))
                {
                    if (InitObject.frmEReparo.chkReparo[2].Checked)
                    {
                        CadenaCompleta += "<tr>";
                        CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                        CadenaCompleta += "<td width='1%'></td>";
                        CadenaCompleta += "<td width='5%' class='form-group-sm small'></td>";
                        CadenaCompleta += "<td width='97%' align='left' class='form-group-sm small'>SE REQUIERE IDENTIFICAR CON NOMBRE Y RUT A LOS APODERADOS QUE FIRMAN.</td>";
                        CadenaCompleta += "</tr>";
                    }
                    if (InitObject.frmEReparo.chkReparo[3].Checked)
                    {
                        CadenaCompleta += "<tr>";
                        CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                        CadenaCompleta += "<td width='1%'></td>";
                        CadenaCompleta += "<td width='5%' class='form-group-sm small'></td>";
                        CadenaCompleta += "<td width='97%' align='left' class='form-group-sm small'>IUV EN MANTENCION..</td>";
                        CadenaCompleta += "</tr>";
                    }
                    if (InitObject.frmEReparo.chkReparo[4].Checked)
                    {
                        CadenaCompleta += "<tr>";
                        CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                        CadenaCompleta += "<td width='1%'></td>";
                        CadenaCompleta += "<td width='5%' class='form-group-sm small'></td>";
                        CadenaCompleta += "<td width='97%' align='left' class='form-group-sm small'>FIRMAS NO REGISTRADAS O NO DIGITALIZADAS.</td>";
                        CadenaCompleta += "</tr>";
                    }
                    if (InitObject.frmEReparo.chkOtros[0].Checked && (InitObject.frmEReparo.txtOtros[0].Text.Trim().Length > 0))
                    {
                        CadenaCompleta += "<tr>";
                        CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                        CadenaCompleta += "<td width='1%'></td>";
                        CadenaCompleta += "<td width='5%' class='form-group-sm small'>OTROS </td>";
                        CadenaCompleta += "<td width='92%' align='left' class='form-group-sm small'>" + InitObject.frmEReparo.txtOtros[0].Text.Trim() + "</td>";
                        CadenaCompleta += "</tr>";
                    }
                }
                CadenaCompleta += "</table>";
                CadenaCompleta += "</td>";
                CadenaCompleta += "</tr>";
            }

            if (InitObject.frmEReparo.chkReparo[5].Checked || InitObject.frmEReparo.chkReparo[6].Checked || (InitObject.frmEReparo.chkOtros[1].Checked && (InitObject.frmEReparo.txtOtros[1].Text.Trim().Length > 0)))
            {
                CadenaCompleta += "<tr><td><br /></td></tr>";
                CadenaCompleta += "<tr>";
                CadenaCompleta += "<td class='form-control'><b>[ MIFT / SVS: ]</b></td>";
                CadenaCompleta += "</tr>";
                CadenaCompleta += "<tr><td><br /></td></tr>";
                CadenaCompleta += "<tr>";
                CadenaCompleta += "<td>";
                CadenaCompleta += "<table width='100%' cellspacing='0' cellpadding='0'>";

                CadenaCompleta += "<tr>";
                CadenaCompleta += "<td>";
                CadenaCompleta += "<table width='100%' cellspacing='0' cellpadding='0'>";
                if (InitObject.frmEReparo.chkReparo[5].Checked)
                {
                    CadenaCompleta += "<tr>";
                    CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                    CadenaCompleta += "<td width='1%'></td>";
                    CadenaCompleta += "<td width='5%' class='form-group-sm small'></td>";
                    CadenaCompleta += "<td width='97%' align='left' class='form-group-sm small'>CLIENTE NO AUTORIZADO A OPERAR POR FAX</td>";
                    CadenaCompleta += "</tr>";
                }
                if (InitObject.frmEReparo.chkReparo[6].Checked)
                {
                    CadenaCompleta += "<tr>";
                    CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                    CadenaCompleta += "<td width='1%'></td>";
                    CadenaCompleta += "<td width='5%' class='form-group-sm small'></td>";
                    CadenaCompleta += "<td width='97%' align='left' class='form-group-sm small'>BENEFICIARIO NO RECURRENTE. DEBE SOLICITAR CALL BACK</td>";
                    CadenaCompleta += "</tr>";
                }
                if (InitObject.frmEReparo.chkOtros[1].Checked && (InitObject.frmEReparo.txtOtros[1].Text.Trim().Length > 0))
                {
                    CadenaCompleta += "<tr>";
                    CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                    CadenaCompleta += "<td width='1%'></td>";
                    CadenaCompleta += "<td width='5%' class='form-group-sm small'>OTROS </td>";
                    CadenaCompleta += "<td width='92%' align='left' class='form-group-sm small'>" + InitObject.frmEReparo.txtOtros[1].Text.Trim() + "</td>";
                    CadenaCompleta += "</tr>";
                }

                CadenaCompleta += "</table>";
                CadenaCompleta += "</td>";
                CadenaCompleta += "</tr>";
            }
            if (InitObject.frmEReparo.chkReparo[7].Checked || InitObject.frmEReparo.chkReparo[8].Checked || InitObject.frmEReparo.chkReparo[9].Checked || (InitObject.frmEReparo.chkOtros[2].Checked && (InitObject.frmEReparo.txtOtros[2].Text.Trim().Length > 0)))
            {

                CadenaCompleta += "<tr><td><br /></td></tr>";
                CadenaCompleta += "<tr>";
                CadenaCompleta += "<td class='form-control'><b>[ ESTRUCTURA DE CARTA: ]</b></td>";
                CadenaCompleta += "</tr>";
                CadenaCompleta += "<tr><td><br /></td></tr>";
                CadenaCompleta += "<tr>";
                CadenaCompleta += "<td>";
                CadenaCompleta += "<table width='100%' cellspacing='0' cellpadding='0'>";

                CadenaCompleta += "<tr>";
                CadenaCompleta += "<td>";
                CadenaCompleta += "<table width='100%' cellspacing='0' cellpadding='0'>";

                if (InitObject.frmEReparo.chkReparo[7].Checked)
                {
                    //model.Reparos.Add(new Models.ControlIntegral.AgregaListaReparos { GenerarHtmlNivel2 = false, GenerarOtros = false, SubTitulo = "FECHA EMISIÓN ERRONEA O FALTANTE" });
                    CadenaCompleta += "<tr>";
                    CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                    CadenaCompleta += "<td width='1%'></td>";
                    CadenaCompleta += "<td width='5%' class='form-group-sm small'></td>";
                    CadenaCompleta += "<td width='97%' align='left' class='form-group-sm small'>FECHA EMISIÓN ERRONEA O FALTANTE</td>";
                    CadenaCompleta += "</tr>";
                }
                if (InitObject.frmEReparo.chkReparo[8].Checked)
                {
                    //model.Reparos.Add(new Models.ControlIntegral.AgregaListaReparos { GenerarHtmlNivel2 = false, GenerarOtros = false, SubTitulo = "FALTA CUENTA ORIGEN/CARGO" });
                    CadenaCompleta += "<tr>";
                    CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                    CadenaCompleta += "<td width='1%'></td>";
                    CadenaCompleta += "<td width='5%' class='form-group-sm small'></td>";
                    CadenaCompleta += "<td width='97%' align='left' class='form-group-sm small'>FALTA CUENTA ORIGEN/CARGO</td>";
                    CadenaCompleta += "</tr>";
                }
                if (InitObject.frmEReparo.chkReparo[9].Checked)
                {
                    //model.Reparos.Add(new Models.ControlIntegral.AgregaListaReparos { GenerarHtmlNivel2 = false, GenerarOtros = false, SubTitulo = "FALTA CONCEPTO ORIGEN/DESTINO DE LAS DIVISAS O CÓDIGO BCCH INDICADO ES ERRONEO" });
                    CadenaCompleta += "<tr>";
                    CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                    CadenaCompleta += "<td width='1%'></td>";
                    CadenaCompleta += "<td width='5%' class='form-group-sm small'></td>";
                    CadenaCompleta += "<td width='97%' align='left' class='form-group-sm small'>FALTA CONCEPTO ORIGEN/DESTINO DE LAS DIVISAS O CÓDIGO BCCH INDICADO ES ERRONEO</td>";
                    CadenaCompleta += "</tr>";
                }
                if (InitObject.frmEReparo.chkOtros[2].Checked && (InitObject.frmEReparo.txtOtros[2].Text.Trim().Length > 1))
                {
                    //model.Reparos.Add(new Models.ControlIntegral.AgregaListaReparos { GenerarHtmlNivel2 = false, GenerarOtros = true, Otros = "OTROS: ", SubTitulo = InitObject.frmEReparo.txtOtros[2].Text.Trim() });
                    CadenaCompleta += "<tr>";
                    CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                    CadenaCompleta += "<td width='1%'></td>";
                    CadenaCompleta += "<td width='5%' class='form-group-sm small'>OTROS </td>";
                    CadenaCompleta += "<td width='92%' align='left' class='form-group-sm small'>" + InitObject.frmEReparo.txtOtros[2].Text.Trim() + "</td>";
                    CadenaCompleta += "</tr>";

                }
                CadenaCompleta += "</table>";
                CadenaCompleta += "</td>";
                CadenaCompleta += "</tr>";

            }

            if (InitObject.frmEReparo.chkReparo[10].Checked || InitObject.frmEReparo.chkReparo[11].Checked || InitObject.frmEReparo.chkReparo[12].Checked || InitObject.frmEReparo.chkReparo[13].Checked || (InitObject.frmEReparo.chkOtros[3].Checked && (InitObject.frmEReparo.txtOtros[3].Text.Trim().Length > 0)))
            {
                CadenaCompleta += "<tr><td><br /></td></tr>";
                CadenaCompleta += "<tr>";
                CadenaCompleta += "<td class='form-control'><b>[ CIERRE TIPO DE CAMBIO: ]</b></td>";
                CadenaCompleta += "</tr>";
                CadenaCompleta += "<tr><td><br /></td></tr>";
                CadenaCompleta += "<tr>";
                CadenaCompleta += "<td>";
                CadenaCompleta += "<table width='100%' cellspacing='0' cellpadding='0'>";

                CadenaCompleta += "<tr>";
                CadenaCompleta += "<td>";
                CadenaCompleta += "<table width='100%' cellspacing='0' cellpadding='0'>";

                if (InitObject.frmEReparo.chkReparo[10].Checked)
                {
                    CadenaCompleta += "<tr>";
                    CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                    CadenaCompleta += "<td width='1%'></td>";
                    CadenaCompleta += "<td width='5%' class='form-group-sm small'></td>";
                    CadenaCompleta += "<td width='97%' align='left' class='form-group-sm small'>FALTA REGISTRO DE CIERRE DE TIPO DE CAMBIO EN CVD</td>";
                    CadenaCompleta += "</tr>";
                }
                if (InitObject.frmEReparo.chkReparo[11].Checked)
                {
                    CadenaCompleta += "<tr>";
                    CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                    CadenaCompleta += "<td width='1%'></td>";
                    CadenaCompleta += "<td width='5%' class='form-group-sm small'></td>";
                    CadenaCompleta += "<td width='97%' align='left' class='form-group-sm small'>TIPO DE CAMBIO NO ES COINCIDENTE ENTRE CARTA Y REGISTRO CVD</td>";
                    CadenaCompleta += "</tr>";

                }
                if (InitObject.frmEReparo.chkReparo[12].Checked)
                {
                    //model.Reparos.Add(new Models.ControlIntegral.AgregaListaReparos { GenerarHtmlNivel2 = false, GenerarOtros = false, SubTitulo = "RUT DE REGISTRO EN CVD NO CORRESPONDE" });
                    CadenaCompleta += "<tr>";
                    CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                    CadenaCompleta += "<td width='1%'></td>";
                    CadenaCompleta += "<td width='5%' class='form-group-sm small'></td>";
                    CadenaCompleta += "<td width='97%' align='left' class='form-group-sm small'>RUT DE REGISTRO EN CVD NO CORRESPONDE</td>";
                    CadenaCompleta += "</tr>";
                }
                if (InitObject.frmEReparo.chkReparo[13].Checked)
                {
                    CadenaCompleta += "<tr>";
                    CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                    CadenaCompleta += "<td width='1%'></td>";
                    CadenaCompleta += "<td width='5%' class='form-group-sm small'></td>";
                    CadenaCompleta += "<td width='97%' align='left' class='form-group-sm small'>CIERRE CON DESFASE MAYOR A 48 HORAS</td>";
                    CadenaCompleta += "</tr>";
                }

                if (InitObject.frmEReparo.chkOtros[3].Checked && (InitObject.frmEReparo.txtOtros[3].Text.Trim().Length > 1))
                {
                    //model.Reparos.Add(new Models.ControlIntegral.AgregaListaReparos { GenerarHtmlNivel2 = false, GenerarOtros = true, Otros = "OTROS: ", SubTitulo = InitObject.frmEReparo.txtOtros[3].Text.Trim() });
                    CadenaCompleta += "<tr>";
                    CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                    CadenaCompleta += "<td width='1%'></td>";
                    CadenaCompleta += "<td width='5%' class='form-group-sm small'>OTROS </td>";
                    CadenaCompleta += "<td width='92%' align='left' class='form-group-sm small'>" + InitObject.frmEReparo.txtOtros[3].Text.Trim() + "</td>";
                    CadenaCompleta += "</tr>";
                }
                CadenaCompleta += "</table>";
                CadenaCompleta += "</td>";
                CadenaCompleta += "</tr>";
            }

            if (InitObject.frmEReparo.chkReparo[14].Checked || InitObject.frmEReparo.chkReparo[15].Checked || InitObject.frmEReparo.chkReparo[16].Checked || (InitObject.frmEReparo.chkOtros[4].Checked && (InitObject.frmEReparo.txtOtros[4].Text.Trim().Length > 1)))
            {
                CadenaCompleta += "<tr><td><br /></td></tr>";
                CadenaCompleta += "<tr>";
                CadenaCompleta += "<td class='form-control'><b>[ OTROS REPAROS: ]</b></td>";
                CadenaCompleta += "</tr>";
                CadenaCompleta += "<tr><td><br /></td></tr>";
                CadenaCompleta += "<tr>";
                CadenaCompleta += "<td>";
                CadenaCompleta += "<table width='100%' cellspacing='0' cellpadding='0'>";

                CadenaCompleta += "<tr>";
                CadenaCompleta += "<td>";
                CadenaCompleta += "<table width='100%' cellspacing='0' cellpadding='0'>";
                if (InitObject.frmEReparo.chkReparo[14].Checked)
                {
                    CadenaCompleta += "<tr>";
                    CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                    CadenaCompleta += "<td width='1%'></td>";
                    CadenaCompleta += "<td width='5%' class='form-group-sm small'></td>";
                    CadenaCompleta += "<td width='97%' align='left' class='form-group-sm small'>REPARO</td>";
                    CadenaCompleta += "</tr>";
                }
                if (InitObject.frmEReparo.chkReparo[15].Checked)
                {
                    CadenaCompleta += "<tr>";
                    CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                    CadenaCompleta += "<td width='1%'></td>";
                    CadenaCompleta += "<td width='5%' class='form-group-sm small'></td>";
                    CadenaCompleta += "<td width='97%' align='left' class='form-group-sm small'>CUENTA CORRIENTE SIN FONDOS SUFICIENTES PARA REALIZAR OPERACIÓN</td>";
                    CadenaCompleta += "</tr>";
                }
                if (InitObject.frmEReparo.chkReparo[16].Checked)
                {
                    CadenaCompleta += "<tr>";
                    CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                    CadenaCompleta += "<td width='1%'></td>";
                    CadenaCompleta += "<td width='5%' class='form-group-sm small'></td>";
                    CadenaCompleta += "<td width='97%' align='left' class='form-group-sm small'>PARTICIPANTE NO CREADO EN BASE COMEX.</td>";
                    CadenaCompleta += "</tr>";

                }
                if (InitObject.frmEReparo.chkOtros[4].Checked && (InitObject.frmEReparo.txtOtros[4].Text.Trim().Length > 1))
                {
                    CadenaCompleta += "<tr>";
                    CadenaCompleta += "<td width='2%' style='border: 1px solid black; color:red;' class='form-control' align='center'>X</td>";
                    CadenaCompleta += "<td width='1%'></td>";
                    CadenaCompleta += "<td width='5%' class='form-group-sm small'>OTROS </td>";
                    CadenaCompleta += "<td width='92%' align='left' class='form-group-sm small'>" + InitObject.frmEReparo.txtOtros[4].Text.Trim() + "</td>";
                    CadenaCompleta += "</tr>";
                }
                CadenaCompleta += "</table>";
                CadenaCompleta += "</td>";
                CadenaCompleta += "</tr>";
            }

            CadenaCompleta += "</table>";


            CadenaCompleta += "<hr width='100%' size='3' style='border-color:#002464' />";
            CadenaCompleta += "<div class='otros2'>";
            CadenaCompleta += "<b>";
            CadenaCompleta += "Este correo se entendera como un aviso de reparo y no debe ser respondido. La operación sera derivada a la carpeta de <span class='navbar-link'>operaciones de rechazadas</span>en CITIDOCS donde uds. deberan gestionar su regularización dentro";
            CadenaCompleta += " de los horarios establecidos, segun SLA.";
            CadenaCompleta += "</b>";
            CadenaCompleta += "</div>";
            CadenaCompleta += "<div class='otros2'>";
            CadenaCompleta += "<img src='cid:WinLogoPie' />";
            CadenaCompleta += "</div'>";
            CadenaCompleta += "</div'>";
            CadenaCompleta += "</body>";
            CadenaCompleta += "</html>";

            return CadenaCompleta;
        }
        private ActionResult DescargarMailControlIntegral(string destinario)
        {
            using (var tracer = new Tracer())
            {
                if (!string.IsNullOrEmpty(destinario))
                {
                    string from = String.Empty;
                    try
                    {
                        var usuario = HttpContext.GetCurrentUser();
                        tracer.AddToContext("email", usuario.EMail);
                        from = usuario.EMail;
                    }
                    catch (Exception ex)
                    {
                        tracer.TraceException("Alerta", ex);
                    }

                    if (String.IsNullOrEmpty(from))
                    {
                        from = "especialista@bancochile.cl";
                    }

                    MailMessage message = new MailMessage(from, destinario); //"direccion@cliente.cl" remplazar el from con la dir del usuario del principal                  
                    message.IsBodyHtml = false;
                    byte[] fileContent = message.SaveToStream();
                    FileContentResult result = new FileContentResult(fileContent, "message/rfc822");
                    result.FileDownloadName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".eml"; //genero un nombre de archivo aux con la fecha del momento
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

    }
}