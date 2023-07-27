using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.BL.XCFT;
using BCH.Comex.Core.BL.XCFT.Forms;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using BCH.Comex.UI.Web.Models;
using BCH.Comex.UI.Web.Models.FundTransfer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using BCH.Comex.Core.Entities.Portal;
using System.Net;
using System.Text.RegularExpressions;
using BCH.Comex.Core.BL.Mcambio;
using BCH.Comex.Core.Entities.Mcambio;

namespace BCH.Comex.UI.Web.Controllers
{
    public class FundTransferController : Controller
    {
        private FundTransferService ftService;
        private Dictionary<string, Action> initialize;
        private Dictionary<string, Func<Object>> viewModels;
        private const string FundTransferAppRole = "COMEX_CAMBIOS_XCFT";
        static FundTransferController()
        {
            new PortalService().RegisterApp("XCFT", "Fund Tranfer - Clientes Globales", "FX TRANSFER",
                FundTransferAppRole, "COMEX_GRP_CAMBIOS", "FundTransfer");
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Common.Utils.ValidarEjecucionFinDia(Session);

            bool errorDeSeguridad = false;

            if (this.InitObject == null && filterContext.ActionDescriptor.ActionName != "Index")
            {
                filterContext.Result = this.RedirectToAction("Index");
            }

            if (this.InitObject != null)
            {
                UI_Button botonAccion = this.InitObject.Mdi_Principal.BUTTONS.Values.Where(b => String.Compare(b.Tag.ToString().Substring(1), filterContext.HttpContext.Request.Url.AbsolutePath, true) == 0).FirstOrDefault();
                if (botonAccion != null)
                {
                    errorDeSeguridad = !botonAccion.Enabled;
                }
                else
                {
                    UI_Button opcionAccion = this.InitObject.Mdi_Principal.Opciones.Where(o => String.Compare(o.Tag.ToString().Substring(1), filterContext.HttpContext.Request.Url.AbsolutePath, true) == 0).FirstOrDefault();
                    if (opcionAccion != null)
                    {
                        errorDeSeguridad = !opcionAccion.Enabled;
                    }
                }
            }

            if (errorDeSeguridad)
            {
                this.InitObject.Mdi_Principal.MESSAGES.Clear();

                //no tiene permisos para ejecutar esta operacion
                this.InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Title = "Operación inválida",
                    Text = "No es posible acceder a la url deseada en este momento.",
                    Type = TipoMensaje.Error,
                    AutoClose = true,
                });

                filterContext.Result = this.RedirectToAction("Index");
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }

        /// <summary>
        /// Rutea el pedido segun la acción de InitObject.FormularioQueAbrir
        /// </summary>
        /// <param name="Logica">Funcion que contiene la lógica de la acción</param>
        /// <param name="ObtenerRetorno">Función que debe obtener el Retorno del Action en caso de no redireccionar</param>
        /// <returns></returns>
        private ActionResult Rutear(Action Logica, Func<ActionResult> ObtenerRetorno, bool limpiar = true)
        {
            using (var tracer = new Tracer())
            {
                tracer.AddToContext("limpiar", limpiar);

                if (InitObject != null && limpiar)
                {
                    this.InitObject.Mdi_Principal.MESSAGES.Clear();
                }

                if (Logica != null) //ejecuta la lógica
                {
                    try
                    {
                        Logica();
                    }
                     catch (Exception ex)
                    {
                        if (InitObject == null)
                            RefrescarSesionComex();

                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Title = "Error",
                            Text = ex.Message,
                            Type = TipoMensaje.Error
                        });
                        tracer.TraceException("No fue posible obtener un numero de operación valida", ex);
                        if (ex.Message.Trim() == "No fue posible obtener un numero de operación valida, valor retornado por BD:")
                        {
                            InitObject.DesabilitarBotones = true;
                            // Desabilitar todos los botones y solo habilita impresion.
                            InitObject.Mdi_Principal.BUTTONS.Keys
                                .ToList()
                                .ForEach(key =>
                                {
                                    InitObject.Mdi_Principal.BUTTONS[key].Enabled = (key == "tbr_impresion") ? true : (key == "tbr_nuevo") ? true : false;
                                });
                        }
                        return RedirectToAction("Index", "FundTransfer");
                    }
                }

                if (this.InitObject == null)
                {
                    tracer.TraceError("Estado no Inicializado");
                    InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Title = "Error",
                        Text = "Estado no Inicializado",
                        Type = TipoMensaje.Error
                    });
                    tracer.TraceVerbose("El objecto InitObject, es NULL");
                    return RedirectToAction("Index", "FundTransfer");
                }

                if (String.IsNullOrEmpty(this.InitObject.FormularioQueAbrir)) // si no tengo que redireccionar
                {
                    if (ObtenerRetorno == null)
                    {
                        tracer.TraceError("No hay que redireccionar, y la funcion que retorna la vista no esta implementada");
                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Title = "Error",
                            Text = "No hay que redireccionar, y la funcion que retorna la vista no esta implementada",
                            Type = TipoMensaje.Error
                        });
                        tracer.TraceVerbose("El objecto InitObject.FormularioQueAbrir, es NULL o vacio y el Func<ActionResult> ObtenerRetorno es NULL.");
                        return RedirectToAction("Index", "FundTransfer");
                    }
                    ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
                    ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
                    return ObtenerRetorno();
                }
                else // tengo que redireccionar
                {
                    var nuevaAccion = this.InitObject.FormularioQueAbrir;
                    this.InitObject.FormularioQueAbrir = String.Empty;
                    return RedirectToAction(nuevaAccion);
                }
            }
        }
        public void NoRutear()
        {
            this.InitObject.FormularioQueAbrir = String.Empty;
        }

        private ActionResult Execute(Action metodo, string thisAction)
        {
            if (this.InitObject == null)
            {
                throw new Exception("Error, no hay estado iniciado");
            }
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            metodo();
            if (this.InitObject.Mdi_Principal.MESSAGES.Any(x => x.Type == TipoMensaje.Error))
            {
                this.InitObject.FormularioQueAbrir = "Index";
            }
            if (string.IsNullOrEmpty(InitObject.FormularioQueAbrir))
            {
                this.InitObject.FormularioQueAbrir = "Index";
            }
            if (!String.IsNullOrEmpty(this.InitObject.VieneDe))
            {
                return RedirectToAction(this.InitObject.VieneDe);
            }
            else if (thisAction == this.InitObject.FormularioQueAbrir)
            {
                if (initialize.ContainsKey(this.InitObject.FormularioQueAbrir))
                {
                    initialize[this.InitObject.FormularioQueAbrir]();
                }
                dynamic viewModel = null;
                if (viewModels.ContainsKey(this.InitObject.FormularioQueAbrir))
                {
                    viewModel = viewModels[this.InitObject.FormularioQueAbrir]();
                }
                ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
                return View(thisAction, viewModel);
            }
            else
            {
                ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
                return RedirectToAction(this.InitObject.FormularioQueAbrir);
            }
        }

        public InitializationObject InitObject
        {
            get
            {
                var res = System.Web.HttpContext.Current.Session[SessionKeys.FundTransfer.InitializationObjectKey] as InitializationObject;
                return res;
            }
            set
            {
                System.Web.HttpContext.Current.Session[SessionKeys.FundTransfer.InitializationObjectKey] = value;
            }
        }

        public FundTransferController()
        {
            ftService = new FundTransferService();
            initialize = new Dictionary<string, Action>();
            viewModels = new Dictionary<string, Func<object>>();
            #region INITIALIZE
            initialize.Add("ComercioInvisible", () =>
            {
                ftService.LoadFrmComercioInvisible(this.InitObject);
            });
            initialize.Add("Ingreso_Valores", () =>
            {
                ftService.LoadIngresoValores(this.InitObject);
            });
            initialize.Add("Arbitrajes", () =>
            {
                ftService.LoadFrmArbitrajes(this.InitObject);
            });
            initialize.Add("ComercioVisibleExport", () =>
            {
                ftService.COMVISEXP_Form_Load(this.InitObject);
            });
            initialize.Add("RelacionarOperacion", () =>
            {
                ftService.FrmgAso_LoadFrm(this.InitObject);
            });
            initialize.Add("EmitirCheque", () =>
            {
                //ftService.EMITIRCHEQUE_Documentos(this.InitObject);
            });
            initialize.Add("PlanillaVisibleImport", () =>
            {
                ftService.PlvSO_LoadFrm(this.InitObject);
            });
            initialize.Add("OrigenFondos", () =>
            {
                ftService.ORIFOND_Form_Load(this.InitObject);
                ftService.ORIFOND_Form_Activate(this.InitObject);
                //ftService.ORIFOND_l_mnd_Click(this.InitObject);
                //ftService.ORIFOND_L_Cta_Click(this.InitObject);
                //ftService.ORIFOND_L_Partys_Click(this.InitObject);
                //ftService.ORIFOND_Ok_Partys_Click(this.InitObject);
                //ftService.ORIFOND_L_Cuentas_Click(this.InitObject);
            });
            //initialize.Add("DestinoFondos", () =>
            //{
            //    ftService.DESTFOND_Form_Load(this.InitObject);
            //    //ftService.DESTFOND_L_Mnd_Blur(this.InitObject);
            //    //ftService.DESTFOND_L_Cta_Blur(this.InitObject);
            //    //ftService.DESTFOND_L_Party_Blur(this.InitObject);
            //    //ftService.DESTFOND_L_Cuentas_Blur(this.InitObject);
            //});
            initialize.Add("ReversarOperacionExport", () =>
            {
                ftService.ReversarOperacionExportInit(this.InitObject);
            });
            #endregion
            #region VIEW MODELS
            viewModels.Add("Index", () => GetIndexViewModel());
            viewModels.Add("ComercioInvisible", () => GetComercioInvisibleViewModel());
            viewModels.Add("Ingreso_Valores", () => GetIngresoValoresViewModel());
            viewModels.Add("Arbitrajes", () => GetArbitrajesViewModel());
            viewModels.Add("ComercioVisibleExport", () => GetComercioVisibleExportViewModel());
            viewModels.Add("PlanillaVisibleImport", () => GetPlanillaVisibleImportViewModel());
            viewModels.Add("OrigenFondos", () => GetOrigenFondosViewModel());
            viewModels.Add("DestinoFondos", () => GetDestinoFondosViewModel());
            //viewModels.Add("SeleccionOficina", () => GetSeleccionOficinaViewModel());
            //viewModels.Add("ReversarOperacionExport", () => GetReversarOperacionExportViewModel());  
            #endregion
        }

        public JsonResult ImportModel<T>(T viewModel, Action<T> SetViewModel, Func<T> GetViewModel, Action metodo) where T : ViewModel
        {
            try
            {
                var initObject = this.InitObject;
                SetViewModel(viewModel);

                metodo();

                this.InitObject = initObject;
                var model = GetViewModel();
                this.InitObject.Mdi_Principal.MESSAGES.Clear();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                //viewModel.MensajesDeError = new List<string> { e.Message + " - " + e.StackTrace };
                viewModel.MensajesDeError = new List<string> { e.Message };
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }
        }


        //
        // GET: /FundTransfer/
        [AuthorizeOrForbidden(Roles = FundTransferAppRole)]
        public ActionResult Index(bool? refrescarSesion)
        {
            using (Tracer tracer = new Tracer("Index de FundTransfer"))
            {
                tracer.TraceVerbose("Entrando a Index de FT...");
                tracer.AddToContext("refrescarSesion", refrescarSesion);

                return Rutear(() =>
                {
                    try
                    {
                        if ((this.InitObject == null) || //primera vez que entro al sistema
                            (refrescarSesion.HasValue && refrescarSesion.Value)) // fuerzo a refrescar
                        {
                            RefrescarSesionComex();
                        }
                        else if (this.InitObject.refrescarSesion)
                        {
                            RefrescarSesionComex();
                        }

                        this.InitObject.FormularioQueAbrir = String.Empty;
                    }
                    catch (Exception ex)
                    {
                        if (ExceptionPolicy.HandleException(ex, "PoliticaUIFundTransfer")) throw;
                    }
                },
                () =>
                {
                    return View(GetIndexViewModel());
                }, false);
            }
        }

        /// <summary>
        /// Inicializa el objeto principal de la aplicación.
        /// </summary>
        internal void RefrescarSesionComex()
        {
            #region Variables
            string VieneDe = string.Empty, formulario = string.Empty;
            bool desablitarBotones = (InitObject != null) ? InitObject.DesabilitarBotones : false;
            #endregion
            #region Validaciones
            if (this.InitObject != null && this.InitObject.VieneDe != string.Empty)
                VieneDe = this.InitObject.VieneDe;

            if (this.InitObject != null && this.InitObject.FormularioQueAbrir != string.Empty)
                formulario = this.InitObject.FormularioQueAbrir;
            #endregion
            #region Iniciar
            var usuario = HttpContext.GetCurrentUser();
            var datos = usuario.GetDatosUsuario();
            this.InitObject = ftService.FundTransferInit(datos);
            this.InitObject.Usuario = datos;
            this.InitObject.VieneDe = VieneDe;
            this.InitObject.FormularioQueAbrir = formulario;
            #endregion
        }

        [HttpPost]
        public ActionResult Index_ConfigImprimirClick(string elem, bool value)
        {
            using (Tracer tracer = new Tracer("Index de FundTransfer"))
            {
                try
                {
                    switch (elem)
                    {
                        case "ChkImpresionCartas_Checked":
                            InitObject.Usuario.ConfigImpres_ImprimeCartas = value ? "-1" : "0";
                            InitObject.Mdi_Principal.mnu_cartas.Checked = value;
                            break;
                        case "ChkImpresionContabilidad_Checked":
                            InitObject.Usuario.ConfigImpres_ImprimeReporte = value ? "-1" : "0";
                            InitObject.Mdi_Principal.mnu_conta.Checked = value;
                            break;
                        case "ChkImpresionPlanillas_Checked":
                            InitObject.Usuario.ConfigImpres_ImprimePlanillas = value ? "-1" : "0";
                            InitObject.Mdi_Principal.mnu_planillas.Checked = value;
                            break;
                        default:
                            break;
                    }

                    ftService.Index_ConfigImprimirClick(this.InitObject);
                }
                catch (Exception ex)
                {
                    if (ExceptionPolicy.HandleException(ex, "PoliticaUIFundTransfer")) throw;
                }

                //no hay nada para hacer
                return new JsonResult();
            }
        }

        //POST: /FundTransfer/NuevaOperacion
        public ActionResult NuevaOperacion()
        {
            IniciarVariables();
            this.InitObject.DealsIngParaProces = null;
            this.InitObject.Frag_Anula = 0;
            this.InitObject.Frag_TransInt = 0;
            using (Tracer tracer = new Tracer("Nueva Operacion"))
            {
                return Rutear(
                        () =>
                        {
                            try
                            {
                                RefrescarSesionComex();
                                this.InitObject.FormularioQueAbrir = "Index";
                                this.InitObject = ftService.FundTransferInit(InitObject.Usuario);
                                ftService.DetectaNuevo(this.InitObject);
                            }
                            catch (Exception ex)
                            {
                                if (ExceptionPolicy.HandleException(ex, "PoliticaUIFundTransfer")) throw;
                            }
                        },
                        null //no retorno vista
                    );
            }
        }

        public ActionResult NuevaOperacion_DesdeSeleccionOficina()
        {
            return Rutear(
                () =>
                {
                    ftService.DetectaNuevo_DesdeSeleccionOficina(this.InitObject);
                },
                null //no retorno vista
            );
        }

        #region ConsultaDealChileFX
        public void IniciarVariables() 
        {
            this.InitObject.DealPrevSel = null;
            this.InitObject.DealActualSel = null;
            this.InitObject.DealManual = null;
            this.InitObject.datSPMcambioCDD = null;
            this.InitObject.PagOri = null;
            this.InitObject.Flag_transferencia = 0;
            this.InitObject.Flag_eliminar = -1;
        }
        public JsonResult COMINV_WM_Deals()
        {
            McambioService McambioServ = new McambioService();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            var jsonObj = McambioServ.Mcambio_GetDealsDisponibles(this.InitObject, ((char)TipoParametro.SoloSpotCruce).ToString(), TipoServicio.COMINV, this.InitObject.DealPrevSel, this.InitObject.DealActualSel, this.InitObject.DealsIngParaProces);
            if (jsonObj == null)
            {
                this.InitObject.datSPMcambioCDD = null;
                return Json(jsonObj, JsonRequestBehavior.AllowGet);
            }
            this.InitObject.datSPMcambioCDD = jsonObj.data;
            return Json(jsonObj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ARBITRAJES_WM_Deals()
        {
            McambioService McambioServ = new McambioService();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            var jsonObj = McambioServ.Mcambio_GetDealsDisponibles(this.InitObject, ((char)TipoParametro.SoloArbitraje).ToString(), TipoServicio.ARBI, this.InitObject.DealPrevSel, this.InitObject.DealActualSel, this.InitObject.DealsIngParaProces);
            if (jsonObj == null)
            {
                this.InitObject.datSPMcambioCDD = null;
                return Json(jsonObj, JsonRequestBehavior.AllowGet);
            }
            this.InitObject.datSPMcambioCDD = jsonObj.data;
            return Json(jsonObj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PlvSO_WM_Deals()
        {
            McambioService McambioServ = new McambioService();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            var jsonObj = McambioServ.Mcambio_GetDealsDisponibles(this.InitObject, ((char)TipoParametro.SoloSpotCruceVentas).ToString(), TipoServicio.PlvSO, this.InitObject.DealPrevSel, this.InitObject.DealActualSel, this.InitObject.DealsIngParaProces);
            if (jsonObj == null)
            {
                this.InitObject.datSPMcambioCDD = null;
                return Json(jsonObj, JsonRequestBehavior.AllowGet);
            }
            this.InitObject.datSPMcambioCDD = jsonObj.data;
            return Json(jsonObj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult COMINV_ch_deal_change(UI_Frm_Comercio_Invisibles jsonModel, string chk)
        {
            this.InitObject.Flag_eliminar = -1;
            using (Tracer tracer = new Tracer("COMINV_ch_deal_change"))
            {
                McambioService McambioServ = new McambioService();
                int idx = McambioServ.idxDealSel(chk);
                if (this.InitObject.DealActualSel != null && this.InitObject.DealsIngParaProces == null)
                {
                    if ((this.InitObject.DealActualSel - 1) == idx)
                    {
                        IniciarVariables();
                        try
                        {
                            ftService.LoadFrmComercioInvisible(this.InitObject);
                            var a = Json(this.InitObject.Frm_Comercio_Invisible, JsonRequestBehavior.AllowGet);
                            return a;
                        }
                        catch
                        {
                            return null;
                        }
                        
                    }
                }
                int DatCount = 0;
                if (this.InitObject.datSPMcambioCDD == null)
                {
                    DatCount = 0;
                }
                else
                {
                    DatCount = this.InitObject.datSPMcambioCDD.Count();
                }
                if (idx == -1 || idx > (DatCount - 1))
                {
                    this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[1].Enabled = true;
                    this.InitObject.PagOri = PaginaOrigen.COMINV.ToString();
                    McambioServ.MsgsValidarIngreso(InitObject, 7, this.InitObject.PagOri, "");
                }
                else
                {
                    this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[1].Enabled = true;
                    this.InitObject.Frm_Comercio_Invisible.Errors.Clear();
                    int pos = idx + 1;
                    if (this.InitObject.DealActualSel == null)
                    {
                        this.InitObject.DealActualSel = pos;
                    }
                    else if (this.InitObject.DealActualSel != pos)
                    {
                        this.InitObject.DealPrevSel = this.InitObject.DealActualSel;
                        this.InitObject.DealActualSel = pos;
                    }
                    McambioServ.COMINV_SetDivisaDealSelec(this.InitObject, this.InitObject.datSPMcambioCDD, idx);
                    ftService.CB_Divisa_Select(this.InitObject);
                    McambioServ.COMINV_SetConceptoDealSelec(this.InitObject, this.InitObject.datSPMcambioCDD, idx);
                    this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[1].Enabled = false;
                    this.InitObject.Frm_Comercio_Invisible.Switch = 0;
                    ftService.Lt_Tcp_Select(this.InitObject);
                    this.InitObject.Frm_Comercio_Invisible.Switch = 1;
                    McambioServ.COMINV_SetValoresSelecDeal(this.InitObject, this.InitObject.datSPMcambioCDD, idx);
                }
                return Json(this.InitObject.Frm_Comercio_Invisible);
            }
        }
        [HttpPost]
        public JsonResult PlvSO_ch_deal_change(PlanillaVisibleImportViewModel jsonModel, string chk)
        {
            this.InitObject.Flag_eliminar = -1;
            using (Tracer tracer = new Tracer("PlvSO_ch_deal_change"))
            {
                McambioService McambioServ = new McambioService();
                int idx = McambioServ.idxDealSel(chk);
                if (this.InitObject.DealActualSel != null && this.InitObject.DealsIngParaProces == null)
                {
                    if ((this.InitObject.DealActualSel - 1) == idx)
                    {
                        IniciarVariables();
                        try
                        {
                            ftService.PlvSO_LoadFrm(this.InitObject);
                            SetPlanillaVisibleImportViewModel(jsonModel);
                            jsonModel.MensajesDeError = new List<string> { };
                            var a = Json(jsonModel, JsonRequestBehavior.AllowGet);
                            ftService.PlvSO_Bot_OkDec_Click(this.InitObject);
                            return a;
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }
                int DatCount = 0;
                if (this.InitObject.datSPMcambioCDD == null)
                {
                    DatCount = 0;
                }
                else
                {
                    DatCount = this.InitObject.datSPMcambioCDD.Count();
                }
                if (idx == -1 || idx > (DatCount - 1))
                {
                    this.InitObject.Frm_PlvSO.Tx_TipCam.Enabled = true;
                    Exception e = new Exception("Identificador del Deal incorrecto.");
                    tracer.TraceException("Alerta en PlvSO_LoadFrm: ", e);
                    jsonModel.MensajesDeError = new List<string> { "<br />El identificador del Deal seleccionado es incorrecto." };
                }
                else
                {
                    jsonModel.MensajesDeError = new List<string> { };
                    this.InitObject.Mdi_Principal.MESSAGES.Clear();
                    this.InitObject.Frm_PlvSO.Tx_TipCam.Enabled = false;
                    int pos = idx + 1;
                    if (this.InitObject.DealActualSel == null)
                    {
                        this.InitObject.DealActualSel = pos;
                    }
                    else if (this.InitObject.DealActualSel != pos)
                    {
                        this.InitObject.DealPrevSel = this.InitObject.DealActualSel;
                        this.InitObject.DealActualSel = pos;
                    }
                    this.InitObject.Frm_PlvSO.Tx_TipCam.Enabled = false;

                    InitObject.Frm_PlvSO.Pn_RutImp.Text = MODGFYS.Rut_Formateado(InitObject.MODGCVD.VgCvd.rutcli);
                    InitObject.Frm_PlvSO.Pn_Import.Text = InitObject.Frm_Participantes.LstPartys.SelectedItem.Replace("Cliente\t", "");

                    McambioServ.PlvSO_SetValoresSelecDeal(this.InitObject, this.InitObject.datSPMcambioCDD, idx);
                    //ftService.PlvSO_Bot_OkDec_Click(this.InitObject);
                    var model = GetPlanillaVisibleImportViewModel();    // ?? jsonModel;
                    PlvSO_Cb_Moneda_Change(model);
                    ftService.PlvSO_Cb_Moneda_Click(this.InitObject);
                    ftService.PlvSO_MtoFob_Blur(this.InitObject);
                    
                    model = GetPlanillaVisibleImportViewModel();
                    model.MensajesDeError = new List<string> { };
                    SetPlanillaVisibleImportViewModel(model);
                    jsonModel = model;
                }
                return Json(jsonModel, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult ARBITRAJE_ch_deal_change(UI_Frm_Arbitrajes jsonModel, string chk)
        {
            this.InitObject.Flag_eliminar = -1;
            using (Tracer tracer = new Tracer("ARBITRAJE_ch_deal_change"))
            {
                McambioService McambioServ = new McambioService();
                int idx = McambioServ.idxDealSel(chk);
                if (this.InitObject.DealActualSel != null && this.InitObject.DealsIngParaProces == null)
                {
                    if ((this.InitObject.DealActualSel - 1) == idx)
                    {
                        IniciarVariables();
                        try
                        {
                            ftService.LoadFrmArbitrajes(this.InitObject);
                            this.InitObject.Frm_Arbitrajes.Tx_Mtoarb[0].Enabled = true;
                            var a = Json(this.InitObject.Frm_Arbitrajes, JsonRequestBehavior.AllowGet);
                            return a;
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }
                int DatCount=0;
                if (this.InitObject.datSPMcambioCDD == null)
                {
                    DatCount = 0;
                }
                else
                {
                    DatCount = this.InitObject.datSPMcambioCDD.Count();
                }
                if (idx == -1 || idx > (DatCount - 1))
                {
                    this.InitObject.Frm_Arbitrajes.Tx_Mtoarb[0].Enabled = true;
                    this.InitObject.PagOri = PaginaOrigen.ARBITRAJES.ToString();
                    McambioServ.MsgsValidarIngreso(InitObject, 7, this.InitObject.PagOri, "");
                }
                else
                {
                    this.InitObject.Frm_Arbitrajes.Tx_Mtoarb[0].Enabled = true;
                    this.InitObject.Frm_Arbitrajes.Errors.Clear();
                    int pos = idx + 1;
                    if (this.InitObject.DealActualSel == null)
                    {
                        this.InitObject.DealActualSel = pos;
                    }
                    else if (this.InitObject.DealActualSel != pos)
                    {
                        this.InitObject.DealPrevSel = this.InitObject.DealActualSel;
                        this.InitObject.DealActualSel = pos;
                    }
                    this.InitObject.Frm_Arbitrajes.Tx_Mtoarb[0].Enabled = false;
                    McambioServ.ARBITRAJE_SetValoresSelecDeal(this.InitObject, this.InitObject.datSPMcambioCDD, idx);
                    this.InitObject.Frm_Arbitrajes.Errors.Clear();
                }
                return Json(this.InitObject.Frm_Arbitrajes);
            }
        }

        #endregion

        #region Participantes
        //
        // GET: /Participantes/
        public ActionResult Participantes(string razonSocial)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
            this.InitObject.FormularioQueAbrir = null;

            if (!string.IsNullOrEmpty(razonSocial) && this.InitObject.Frm_Participantes != null)
                this.InitObject.Frm_Participantes.Llave.Text = razonSocial;
            else
                ftService.ParticipantesInit(this.InitObject, (bool?)Session[SessionKeys.FundTransfer.UltimaOperacionEsCosmosKey]);

            ParticipantesViewModel pvm = new ParticipantesViewModel(this.InitObject.Frm_Participantes, InitObject.FormularioQueAbrir,
                InitObject.Mdi_Principal.MESSAGES);

            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            pvm.ParticipanteCongelado = ParticipanteCongelado();
            ViewBag.OperacionReversa = false;
            ValidarSiEsReverso(pvm);
            return View(pvm);
        }

        private bool ParticipanteCongelado()
        {
            string[] etapasAunSePuedeCambiarElParticipante = new string[] { string.Empty, "ARB", "CVD", "VTA" };
            return (!etapasAunSePuedeCambiarElParticipante.Contains(this.InitObject.MODGCVD.VgCvd.Etapa));
        }

        /// <summary>
        /// Valida si la operación viene de una reversa de planilla, lo cual permite 
        /// cambiar el participante.-
        /// </summary>
        private void ValidarSiEsReverso(ParticipantesViewModel pvm)
        {
            if (InitObject.Frm_Anu_Vi != null && InitObject.Frm_Anu_Vi.Cb_TipAut.SelectedValue != -1)
            {
                pvm.ParticipanteCongelado = false;
                ViewBag.OperacionReversa = true;
            }
        }

        // POST: /Participantes/ 
        [HttpPost]
        public ActionResult Participantes(ParticipantesViewModel pvm, string Command)
        {
            //limpio el modelstate para que use los valoresde InitObj, no los del post
            ModelState.Clear();

            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            //pvm.Update(this.InitObject.Frm_Participantes);

            if (Command == "Aceptar")
            {
                ftService.Participantes_Aceptar(this.InitObject);

                if (this.InitObject.Mdi_Principal.MESSAGES.Count == 0)
                    return new RedirectResult("~/Fundtransfer");
            }
            else if (Command == "Cancelar")
            {
                ftService.Participantes_Cancelar(this.InitObject);

                return new RedirectResult("~/Fundtransfer");
            }

            pvm = new ParticipantesViewModel(this.InitObject.Frm_Participantes, this.InitObject.FormularioQueAbrir,
                InitObject.Mdi_Principal.MESSAGES);
            //ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon;
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;

            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            pvm.ParticipanteCongelado = ParticipanteCongelado();
            ViewBag.OperacionReversa = false;
            ValidarSiEsReverso(pvm);

            return View(pvm);
        }

        [HttpPost]
        public JsonResult Participantes_Eliminar_Click(string codParticipante)
        {
            ftService.Participantes_Eliminar(this.InitObject);
            var pvm = new ParticipantesViewModel(this.InitObject.Frm_Participantes, this.InitObject.FormularioQueAbrir,
                InitObject.Mdi_Principal.MESSAGES);

            if (String.IsNullOrEmpty(this.InitObject.MODGCVD.VgCvd.OpeCon))
            {
                pvm.OPE = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            }
            return Json(pvm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Participantes_Identificar_Click(string codParticipante, int selectedTipoOperacion)
        {
            InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.Frm_Participantes.Llave.Text = FormatUtils.TryFormatearRutParticipante(codParticipante, false); 

            this.InitObject.Frm_Participantes.TipoOperacion.ForEach(x => x.Selected = false);
            this.InitObject.Frm_Participantes.TipoOperacion.Where(x => x.ID == selectedTipoOperacion.ToString()).FirstOrDefault().Selected = true;

            ftService.Participantes_Identificar(this.InitObject);
            Session[SessionKeys.FundTransfer.UltimaOperacionEsCosmosKey] = InitObject.MODXORI.gb_esCosmos;

            var pvm = new ParticipantesViewModel(this.InitObject.Frm_Participantes, this.InitObject.FormularioQueAbrir,
                InitObject.Mdi_Principal.MESSAGES);
            pvm.OPE = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            return Json(pvm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Participantes_Donde_Click(int selectedValue, int prod)
        {
            ParticipantesViewModel pvm;
            try
            {
                this.InitObject.Frm_Participantes.Donde.ForEach(x => x.Selected = x.ID == selectedValue.ToString());
                this.InitObject.Frm_Participantes.TipoOperacion.ForEach(x => x.Selected = x.ID == prod.ToString());
                ftService.Participantes_Donde_Click(this.InitObject);
            }
            catch (Exception ex)
            {
                InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Type = TipoMensaje.Error,
                    Title = "Participantes",
                    Text = ex.Message
                });
            }
            pvm = new ParticipantesViewModel(this.InitObject.Frm_Participantes, InitObject.Mdi_Principal.MESSAGES);
            pvm.OPE = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            return Json(pvm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Participantes_LstPartys_Click(int selectedValue)
        {
            try
            {
                this.InitObject.Frm_Participantes.LstPartys.ListIndex = selectedValue;
                ftService.Participantes_LstPartys_Click(this.InitObject);
            }
            catch (Exception ex)
            {
                InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Type = TipoMensaje.Error,
                    Title = "Participantes",
                    Text = ex.Message
                });
            }

            var pvm = new ParticipantesViewModel(this.InitObject.Frm_Participantes, InitObject.Mdi_Principal.MESSAGES);
            pvm.OPE = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;

            if (selectedValue == 0) //esta seleccionado el Cliente, veo si se lo permito modificar o no. El "Otros" y "Comprador" siempre se lo dejo modificar
            {
                pvm.ParticipanteCongelado = ParticipanteCongelado();
            }

            return Json(pvm, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /ParticipantesIdentificar/
        public ActionResult ParticipantesIdentificar()
        {
            var pivm = new ParticipantesIdentificarViewModel(this.InitObject.Frm_Iden_Participantes);

            return new JsonResult()
            {
                Data = pivm,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public ActionResult ParticipantesIdentificar_Aceptar(string selectedRS, string selectedDir)
        {
            InitObject.Mdi_Principal.MESSAGES.Clear();
            int intval;

            InitObject.FormularioQueAbrir = string.Empty;

            if (int.TryParse(selectedRS, out intval))
                this.InitObject.Frm_Iden_Participantes.Nome.ListIndex = intval;

            if (int.TryParse(selectedDir, out intval))
                this.InitObject.Frm_Iden_Participantes.Dire.ListIndex = intval;

            ftService.ParticipantesIdentificar_Aceptar(this.InitObject);

            ParticipantesViewModel pvm = new ParticipantesViewModel(InitObject.Frm_Participantes,
                InitObject.FormularioQueAbrir,
                InitObject.Mdi_Principal.MESSAGES);

            pvm.OPE = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            return new JsonResult()
            {
                Data = pvm,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        //
        // GET: /ConsultaParticipantes/
        public ActionResult ConsultaParticipantes(string razonSocial)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            ConsultaParticipantesViewModel cpvm = new ConsultaParticipantesViewModel();
            cpvm.RazonSocial = razonSocial;
            cpvm.Update(this.InitObject.Frm_Con_Participantes);

            ftService.ConsultaParticipantes_Buscar(this.InitObject);

            cpvm = new ConsultaParticipantesViewModel(this.InitObject.Frm_Con_Participantes);
            //ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon;
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            return View(cpvm);
        }

        //
        // POST: /ConsultaParticipantes_Consultar/
        [HttpPost]
        public ActionResult ConsultaParticipantes_Consultar(string razonSocial)
        {
            razonSocial = Regex.Replace(razonSocial, @"[^0-9A-Za-z .ñç`´'áéíóúÁÉÍÓÚäëïöüÄËÏÖÜàèìòùÀÈÌÒÙâêîôûÂÊÎÔÛ]", "");
            if (razonSocial.Length < 4)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Debe ingresar al menos 4 caracteres.");
            }

            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            this.InitObject.Frm_Con_Participantes.caja.Text = razonSocial;

            ftService.ConsultaParticipantes_Buscar(this.InitObject);

            var resultado = this.InitObject.Frm_Con_Participantes.msg_datos.Items.Select(x => new
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

        //GET: /ParticipantesCrear/
        public ActionResult ParticipantesCrear()
        {
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
            InitObject.Mdi_Principal.MESSAGES.Clear();

            if (this.InitObject.Frm_Crea_Participante == null)
                this.InitObject.Frm_Crea_Participante = new UI_Frm_Crea_Participante();

            this.ftService.ParticipantesCrearInit(this.InitObject);

            ParticipantesCrearViewModel pcvm = new ParticipantesCrearViewModel(this.InitObject);
            //ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon;
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            return View(pcvm);
        }

        [HttpPost]
        public ActionResult ParticipantesCrear(ParticipantesCrearViewModel pcvm, string btnSubmit)
        {
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
            InitObject.Mdi_Principal.MESSAGES.Clear();

            pcvm.Update(this.InitObject);

            if (btnSubmit == "Aceptar")
            {
                if (!this.ftService.ParticipantesCrear_Aceptar(this.InitObject))
                {
                    pcvm = new ParticipantesCrearViewModel(this.InitObject);
                    return View(pcvm);
                }
            }
            else if (btnSubmit == "Cancelar")
            {
                if (!this.ftService.ParticipantesCrear_Cancelar(this.InitObject))
                {
                    pcvm = new ParticipantesCrearViewModel(this.InitObject);
                    return View(pcvm);
                }
            }

            return RedirectToAction("Participantes");
        }

        [HttpPost]
        public ActionResult ParticipantesCrear_Consultar()
        {
            InitObject.Mdi_Principal.MESSAGES.Clear();

            this.ftService.ParticipantesCrear_Consultar(this.InitObject);

            ParticipantesCrearViewModel pcvm = new ParticipantesCrearViewModel(this.InitObject);

            return new JsonResult
            {
                Data = pcvm,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #endregion Participantes

        public ActionResult ComercioInvisible()
        {
            McambioService McambioServ = new McambioService();
            return Rutear(() =>
            {
                this.InitObject.Frm_Comercio_Invisible = null;
                ftService.OpcionBotones(this.InitObject, 0);
            }, View);
        }

        public ActionResult Arbitrajes()
        {
            return Rutear(() =>
            {
                if (this.InitObject.Frm_Arbitrajes != null && this.InitObject.Frm_Arbitrajes.VieneDeIngresoValores)
                {
                    this.InitObject.Frm_Arbitrajes.VieneDeIngresoValores = false;
                }
                else
                {
                    this.InitObject.Frm_Arbitrajes = null;
                    ftService.OpcionBotones(this.InitObject, 1);
                }
            }, () =>
            {
                return View();
            });
        }

        public ActionResult Ingreso_Valores()
        {
            return Rutear(NoRutear,
                () =>
                {
                    ftService.LoadIngresoValores(this.InitObject);
                    return View(GetIngresoValoresViewModel());
                });
        }

        [HttpPost]
        public ActionResult Ingreso_Valores(IngresoValoresViewModel model)
        {
            string paridad = model.Paridad;
            string cambioObservado = model.TipoCambio;
            ftService.MergeValues(this.InitObject, paridad, cambioObservado);
            return Rutear(() =>
            {

                ftService.IngresoValores(this.InitObject);
                string queAbrir = this.InitObject.FormularioQueAbrir;
                if (this.InitObject.Frm_Arbitrajes != null && this.InitObject.Frm_Arbitrajes.VieneDeIngresoValores)
                {

                }
                else if (queAbrir.Equals("PlvSO_Finish"))
                {
                    this.InitObject.FormularioQueAbrir = "PlvSO_Finish";
                }
                else if (queAbrir.Equals("PLANVIS_Finish"))
                {
                    this.InitObject.FormularioQueAbrir = "PLANVIS_Finish";
                }
                else
                {
                    ftService.OpcionBotones(this.InitObject, -1);
                }
            }, null);
        }

        public ActionResult INGVAL_Cancelar_Click()
        {
            return Rutear(() => { ftService.INGVAL_Cancelar(this.InitObject); }, null);
        }


        public ActionResult INGVAL_Cancelar()
        {
            return Rutear(() =>
            {
                ftService.INGVAL_Cancelar(this.InitObject);
            }, null);
            //puede cambiarse por Rutear
        }

        public ActionResult PlanillaVisibleImport()
        {
            return Rutear(() =>
            {
                ftService.Ventas_Vis_Import(this.InitObject);
            }, () =>
            {
                ftService.PlvSO_LoadFrm(this.InitObject);
                var model = GetPlanillaVisibleImportViewModel();
                return View(model);
            });
        }

        public ActionResult PlanillaVisibleImport_PosCobraComis()
        {
            return Rutear(() =>
            {
                ftService.Ventas_Vis_Import_PosCobraComis(this.InitObject);
            }, null); //deberia rutear siempre a Index
        }

        public ActionResult RelacionarOperacion()
        {
            return Execute(() =>
            {
                ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
                this.InitObject.FormularioQueAbrir = "RelacionarOperacion";
            }, "RelacionarOperacion");
        }

        public ActionResult EmitirCheque()
        {
            return Rutear(null, View);
            //return Execute(() =>
            //{
            //    this.InitObject.FormularioQueAbrir = "EmitirCheque";
            //}, "EmitirCheque");
        }

        #region CargarOperaciones
        public ActionResult Frm_Consulta_img_Buscar_Click(string estado)
        {
            using (var trace = new Tracer("Frm_Consulta_img_Buscar_Click"))
            {
                this.InitObject.Mdi_Principal.MESSAGES.Clear();
                try
                {
                    //Es necesario eliminar las marcas de las procesadas al realizar la búsqueda desde el boton buscar
                    this.InitObject.Frm_Consulta.items.Where(c => c.Seleccionado == 1).ToList().ForEach(cc => cc.Seleccionado = 0);
                    List<pro_sce_prty_s04_MS_Result> data = ConsultaOperacionesBuscar(estado);

                    return new JsonResult()
                    {
                        Data = data,
                        MaxJsonLength = Int32.MaxValue,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    throw;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="estado"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult Frm_Consulta_img_Buscar_Click_Page(string estado, string year)
        {
            using (var trace = new Tracer("Frm_Consulta_img_Buscar_Click_Page"))
            {
                this.InitObject.Mdi_Principal.MESSAGES.Clear();
                try
                {
                    //Es necesario eliminar las marcas de las procesadas al realizar la búsqueda desde el boton buscar
                    this.InitObject.Frm_Consulta.items.Where(c => c.Seleccionado == 1).ToList().ForEach(cc => cc.Seleccionado = 0);
                    List<pro_sce_prty_s04_MS_Result> data = ConsultaOperacionesBuscar(estado, year);

                    return new JsonResult()
                    {
                        Data = data,
                        MaxJsonLength = Int32.MaxValue,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    throw;
                }
            }
        }

        private List<pro_sce_prty_s04_MS_Result> ConsultaOperacionesBuscar(string estado)
        {
            List<pro_sce_prty_s04_MS_Result> data = null;
            if (estado != null)
            {
                data = ftService.Frm_Consulta_img_Buscar_Click(this.InitObject, null, estado).OrderByDescending(x => x.MONTO_ORIGINAL).ToList();
            }
            else
            {
                data = InitObject.Frm_Consulta.items.OrderByDescending(x => x.MONTO_ORIGINAL).ToList();
            }

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="estado"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private List<pro_sce_prty_s04_MS_Result> ConsultaOperacionesBuscar(string estado, string year)
        {
            List<pro_sce_prty_s04_MS_Result> data = null;
            if (estado != null)
            {
                data = ftService.Frm_Consulta_img_Buscar_Click(this.InitObject, null, estado, year).OrderByDescending(x => x.MONTO_ORIGINAL).ToList();
            }
            else
            {
                data = InitObject.Frm_Consulta.items.OrderByDescending(x => x.MONTO_ORIGINAL).ToList();
            }

            return data;
        }

        public ActionResult Frm_Consulta_Salir()
        {
            using (var trace = new Tracer("Frm_Consulta_Salir"))
            {
                try
                {
                    InitObject.FormularioQueAbrir = "Index";
                    return new JsonResult()
                    {
                        Data = true,
                        MaxJsonLength = Int32.MaxValue,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    throw;
                }
            }
        }

        public ActionResult CargarOperaciones()
        {
            ftService.frm_Consulta_Load(this.InitObject);
            return Rutear(null,
                () =>
            {
                var model = new CargaOperacionesViewModel();
                model.Data = ConsultaOperacionesBuscar("1"); //por defecto cargo lo vigentes "1" al abrir el formulario
                model.Messages = this.InitObject.Mdi_Principal.MESSAGES;
                return View(model);
            }, false);
        }

        public ActionResult msg_operaciones_DblClick(pro_sce_prty_s04_MS_Result data)
        {

            bool result = true;
            try
            {
                //se guardan las operaciones de la consulta
                var item = this.InitObject.Frm_Consulta.items;
                //Se hace nueva operacion para limpiar posibles rastros de otras operaciones.
                this.InitObject = ftService.FundTransferInit(InitObject.Usuario);
                //se vuelven a asignar la lista de operaciones para poder obtener la informacion de la seleccionada
                this.InitObject.Frm_Consulta.items = item;
                ftService.msg_operaciones_DblClick(InitObject, data);
            }
            catch (Exception)
            {
                result = false;
            }
            this.InitObject.FormularioQueAbrir = "Index";
            return new JsonResult()
            {
                Data = result,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [AcceptVerbs(HttpVerbs.Post), FileDownload]
        public ActionResult ExportarOperaciones(List<pro_sce_prty_s04_MS_Result> data)
        {
            return File(ftService.ExportarOperaciones(InitObject, data),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Consulta de Operaciones.xlsx");
        }
        #endregion

        public ActionResult OrigenFondos(bool? hayMensaje, bool? respuestaMensaje)
        {
            return Rutear(() =>
            {
                if (this.InitObject.Frm_Origen_Fondos != null)
                {
                    if (this.InitObject.Frm_Origen_Fondos.VuelveDeNemonico)
                    {
                        ftService.ORIFOND_BNem_Click(this.InitObject);
                    }
                    else if (this.InitObject.Frm_Origen_Fondos.VuelveDeOtro)
                    {
                        //no hago nada 
                    }
                    else
                    {
                        ftService.OrigenFondosInit(this.InitObject, hayMensaje.HasValue ? hayMensaje.Value : false, respuestaMensaje.HasValue ? respuestaMensaje.Value : false);
                    }
                }
                else
                {
                    ftService.OrigenFondosInit(this.InitObject, hayMensaje.HasValue ? hayMensaje.Value : false, respuestaMensaje.HasValue ? respuestaMensaje.Value : false);
                }
            },
            () =>
            {
                return View(this.InitObject.Frm_Origen_Fondos);
            }
            );
        }

        public ActionResult FrmxPln0()
        {
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
            return View();
        }

        public ActionResult DestinoFondos(bool? hayMensaje, bool? respuestaMensaje)
        {
            return Rutear(() =>
            {
                InitObject.Mdi_Principal.BUTTONS["tbr_vueltos"].Focus = false;
                InitObject.Mdi_Principal.BUTTONS["tbr_grabar"].Focus = true;
                if (this.InitObject.Frm_Destino_Fondos != null)
                {
                    if (this.InitObject.Frm_Destino_Fondos.VuelveDeNemonico)
                    {
                        ftService.DESTFOND_BNem_Click(this.InitObject);
                    }
                    else if (this.InitObject.Frm_Destino_Fondos.VuelveDeOtro)
                    {
                        //no hago nada 
                    }
                    else
                    {
                        ftService.DestinoFondosInit(this.InitObject, hayMensaje.HasValue ? hayMensaje.Value : false, respuestaMensaje.HasValue ? respuestaMensaje.Value : false, this.InitObject.VieneDe == "Vueltos");
                    }
                }
                else
                {
                    ftService.DestinoFondosInit(this.InitObject, hayMensaje.HasValue ? hayMensaje.Value : false, respuestaMensaje.HasValue ? respuestaMensaje.Value : false, this.InitObject.VieneDe == "Vueltos");
                }
            },
            View);
        }

        public ActionResult DefinirNuevosDestinos()
        {
            return Rutear(null, View);
        }

        public ActionResult DefinirNuevosOrigenes()
        {
            return Rutear(null, () =>
            {
                return View(viewName: "DefinirNuevosOrigenes", model: String.IsNullOrEmpty(this.InitObject.VieneDe) ? "OrigenFondos" : this.InitObject.VieneDe);
            });
        }

        public ActionResult Ticket()
        {
            return Rutear(null, View, false);
        }

        public ActionResult Grabar1()
        {
            using (Tracer tracer = new Tracer("Guardar Operacion - Grabar1"))
            {
                return Rutear(() =>
                {
                    ftService.Grabar1(this.InitObject);
                }, null, false);
                //inicializo las variables del initObject para los loops 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Grabar2()
        {
            using (Tracer tracer = new Tracer("Guardar Operacion - Grabar2"))
            {
                return Rutear(() => { ftService.DecidirSiGrabar(this.InitObject); }, null, false);
            }
        }

        public ActionResult Grabar3()
        {
            using (Tracer tracer = new Tracer("Guardar Operacion - Grabar3"))
            {
                return Rutear(() => {
                                        string OpeSin = InitObject.MODGCVD.VgCvd.OpeSin.ToString();
                                        string PrtCli = InitObject.MODGCVD.VgCvd.PrtCli.ToString();
                                        this.InitObject.GrabarOk = 0;
                                        var Result = ftService.Grabar(this.InitObject);
                                        #region Anulación
                                            if (this.InitObject.Frag_Anula == 1)
                                            {
                                                McambioService McambioServ = new McambioService();
                                                McambioServ.Fn_Anulacion(this.InitObject, this.InitObject.NroSce_Anula);
                                            }
                                            this.InitObject.Frag_Anula = 0;
                                        #endregion
                                        if (this.InitObject.GrabarOk == 1 && this.InitObject.Frag_TransInt == 0)   //(Result == true)
                                        {
                                            McambioService McambioServ = new McambioService();
                                            McambioServ.Ejecutar_GuardarCambioEstado(this.InitObject.DealManual, this.InitObject.DealActualSel, this.InitObject, this.InitObject.datSPMcambioCDD, this.InitObject.DealsIngParaProces, this.InitObject.PagOri, this.InitObject.Flag_transferencia, OpeSin, PrtCli);
                                        }
                                        this.InitObject.GrabarOk = 0;
                                    }, null, false);
                //rompo las variables del initObject para los loops y llamo al Grabar de verdad
            }
        }

        public ActionResult ImprimirDocumentos()
        {
            return Rutear(null, View, false);
        }

        public JsonResult DocumentosAImprimir()
        {
            return Json(this.InitObject.DocumentosAImprimir, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Vueltos()
        {
            return Rutear(() => { ftService.Vueltos(this.InitObject); }, null);
        }

        public ActionResult NemonicoCuenta()
        {
            return Rutear(() => { ftService.NEMCTA_Inicializar(this.InitObject); }, View);
        }

        #region "PLANILLA INGRESO VISIBLE EXPORT"
        //Get Planilla Ingreso Visible Export
        public ActionResult PlanillaIngresoVisibleExport()
        {
            return Rutear(() =>
            {
                ModelState.Clear();
                this.InitObject.FormularioQueAbrir = null;
                ftService.PlanillaIngresoVisibleExportInit(this.InitObject);
            },
            () =>
            {
                PlanillaIngresoVisibleExportViewModel pive = new PlanillaIngresoVisibleExportViewModel(this.InitObject.Frm_Pln_VisExp);
                return View(pive);
            });
        }

        [HttpPost]
        public ActionResult PlanillaIngresoVisibleExport(PlanillaIngresoVisibleExportViewModel pive, string Command)
        {
            return Rutear(() =>
            {
                //limpio el modelstate para que use los valoresde InitObj, no los del post
                ModelState.Clear();
                pive.Update(this.InitObject.Frm_Pln_VisExp);

                if (Command == "<<")
                {
                    ftService.PlanillaIngresoVisibleExport_Retroceder(this.InitObject);
                }
                else if (Command == ">>")
                {
                    ftService.PlanillaIngresoVisibleExport_Avanzar(this.InitObject);
                }
                else if (Command == "√")
                {
                    ftService.PlanillaIngresoVisibleExport_Modificar(this.InitObject);
                }
                else if (Command == "Aceptar")
                {
                    ftService.PlanillaIngresoVisibleExport_Aceptar(this.InitObject);

                    if (string.IsNullOrEmpty(InitObject.VieneDe))
                        InitObject.FormularioQueAbrir = "Index";
                    else
                        InitObject.FormularioQueAbrir = InitObject.VieneDe;

                    InitObject.VieneDe = null;
                }
                else if (Command == "Cancelar")
                {
                    ftService.PlanillaIngresoVisibleExport_Cancelar(this.InitObject);

                    if (string.IsNullOrEmpty(InitObject.VieneDe))
                        InitObject.FormularioQueAbrir = "Index";
                    else
                        InitObject.FormularioQueAbrir = InitObject.VieneDe;

                    InitObject.VieneDe = null;
                }
            },
            () =>
            {
                pive = new PlanillaIngresoVisibleExportViewModel(this.InitObject.Frm_Pln_VisExp);
                return View(pive);
            });
        }


        #endregion

        #region UTILIDADES FUNDTRANSFER
        public IndexViewModel GetIndexViewModel()
        {
            var model = new IndexViewModel(InitObject.Frm_Principal, InitObject.Mdi_Principal);

            return model;
        }

        public ComercioInvisibleViewModel GetComercioInvisibleViewModel()
        {
            ComercioInvisibleViewModel modelo = new ComercioInvisibleViewModel();
            modelo.CB_Moneda = this.InitObject.Frm_Comercio_Invisible.Cb_Moneda;

            modelo.CB_Pais = new List<SelectListItem>(this.InitObject.Frm_Comercio_Invisible.Cb_Pais.Items.Select(
                x => new SelectListItem()
                {
                    Value = x.ID,
                    Text = x.Value
                }));
            modelo.Cb_Divisa = this.InitObject.Frm_Comercio_Invisible.Cb_Divisa;

            modelo.Lt_Tcp = new List<SelectListItem>(this.InitObject.Frm_Comercio_Invisible.Lt_Tcp.Items.Select(
                x => new SelectListItem()
                {
                    Text = x.Value,
                    Value = x.ID
                }));
            modelo.Cb_InsUt = new List<SelectListItem>(this.InitObject.Frm_Comercio_Invisible.Cb_InsUt.Items.Select(
                x => new SelectListItem()
                {
                    Text = x.Value,
                    Value = x.ID
                }));
            modelo.Cb_ArCon = new List<SelectListItem>(this.InitObject.Frm_Comercio_Invisible.Cb_ArCon.Items.Select(
                x => new SelectListItem()
                {
                    Text = x.Value,
                    Value = x.ID
                }));
            modelo.Cb_MonDes = new List<SelectListItem>(this.InitObject.Frm_Comercio_Invisible.Cb_MonDes.Items.Select(
                x => new SelectListItem()
                {
                    Text = x.Value,
                    Value = x.ID
                }));
            modelo.Cb_SecEcBen = new List<SelectListItem>(this.InitObject.Frm_Comercio_Invisible.Cb_SecEcBen.Items.Select(
                x => new SelectListItem()
                {
                    Text = x.Value,
                    Value = x.ID
                }));
            modelo.Cb_SecEcIn = new List<SelectListItem>(this.InitObject.Frm_Comercio_Invisible.Cb_SecEcIn.Items.Select(
                x => new SelectListItem()
                {
                    Text = x.Value,
                    Value = x.ID
                }));
            modelo.Cb_TipAut = new List<SelectListItem>(this.InitObject.Frm_Comercio_Invisible.Cb_TipAut.Items.Select(
                x => new SelectListItem()
                {
                    Text = x.Value,
                    Value = x.ID
                }));

            modelo.indexMoneda = this.InitObject.Frm_Comercio_Invisible.Cb_Moneda.ListIndex;
            modelo.indexPais = this.InitObject.Frm_Comercio_Invisible.Cb_Pais.ListIndex;
            modelo.indexDivisa = this.InitObject.Frm_Comercio_Invisible.Cb_Divisa.ListIndex;
            modelo.indexLt_Tcp = this.InitObject.Frm_Comercio_Invisible.Lt_Tcp.ListIndex;
            modelo.indexInsUt = this.InitObject.Frm_Comercio_Invisible.Cb_InsUt.ListIndex;
            modelo.indexArCon = this.InitObject.Frm_Comercio_Invisible.Cb_ArCon.ListIndex;
            modelo.indexMonDes = this.InitObject.Frm_Comercio_Invisible.Cb_MonDes.ListIndex;
            modelo.indexSecEcBen = this.InitObject.Frm_Comercio_Invisible.Cb_SecEcBen.ListIndex;
            modelo.indexSecEcIn = this.InitObject.Frm_Comercio_Invisible.Cb_SecEcIn.ListIndex;
            modelo.indexTipAut = this.InitObject.Frm_Comercio_Invisible.Cb_TipAut.ListIndex;


            //************************ FR OPE ******************************************
            modelo.Tx_MtoCV[0] = this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[0].Text; ;
            modelo.BTx_MtoCV[0] = this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[0].Enabled;
            modelo.Tx_MtoCV[1] = this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[1].Text; ;
            modelo.BTx_MtoCV[1] = this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[1].Enabled;
            modelo.Tx_MtoCV[2] = this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[2].Text; ;
            modelo.BTx_MtoCV[2] = this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[2].Enabled;
            modelo.Tx_MtoCV[3] = this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[3].Text; ;
            modelo.BTx_MtoCV[3] = this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[3].Enabled;

            modelo.Tx_CanAc = this.InitObject.Frm_Comercio_Invisible.Tx_CanAc.Text;
            //***************************************************

            //************************ FR OPE D ******************************************
            modelo.Tx_NumCon = this.InitObject.Frm_Comercio_Invisible.Tx_NumCon.Text;
            modelo.Tx_FecSus = this.InitObject.Frm_Comercio_Invisible.Tx_FecSus.Text;
            modelo.Tx_FecVen = this.InitObject.Frm_Comercio_Invisible.Tx_FecVen.Text;
            modelo.Tx_ParTip = this.InitObject.Frm_Comercio_Invisible.Tx_ParTip.Text;
            //***************************************************

            //************************ FR OFI ******************************************
            modelo.Tx_NumIns = this.InitObject.Frm_Comercio_Invisible.Tx_NumIns.Text;
            modelo.Tx_FecIns = this.InitObject.Frm_Comercio_Invisible.Tx_FecIns.Text;
            modelo.Tx_NomFin = this.InitObject.Frm_Comercio_Invisible.Tx_NomFin.Text;
            modelo.Tx_FecVC = this.InitObject.Frm_Comercio_Invisible.Tx_FecVC.Text;
            modelo.Tx_Fecha = this.InitObject.Frm_Comercio_Invisible.Tx_Fecha.Text;
            modelo.Tx_Mto = this.InitObject.Frm_Comercio_Invisible.Tx_Mto.Text;
            //***************************************************

            //************************ FR SEC ******************************************
            modelo.Tx_PrcPar = this.InitObject.Frm_Comercio_Invisible.Tx_PrcPar.Text;
            //***************************************************

            //************************ FR CONVENIO ******************************************
            modelo.Tx_FecDeb = this.InitObject.Frm_Comercio_Invisible.Tx_FecDeb.Text;
            modelo.Tx_DocNac = this.InitObject.Frm_Comercio_Invisible.Tx_DocNac.Text;
            modelo.Tx_DocExt = this.InitObject.Frm_Comercio_Invisible.Tx_DocExt.Text;
            //***************************************************

            //************************ FR AUTORI ******************************************
            modelo.Tx_NroAut = this.InitObject.Frm_Comercio_Invisible.Tx_NroAut.Text;
            modelo.Tx_FecAut = this.InitObject.Frm_Comercio_Invisible.Tx_FecAut.Text;
            modelo.Tx_SucBcch = this.InitObject.Frm_Comercio_Invisible.Tx_SucBcch.Text;
            //***************************************************

            //************************ FR DECLARACION **************************************
            modelo.Tx_NroDec = this.InitObject.Frm_Comercio_Invisible.Tx_NroDec.Text;
            modelo.Tx_FecDec = this.InitObject.Frm_Comercio_Invisible.Tx_FecDec.Text;
            modelo.Tx_CodAdn = this.InitObject.Frm_Comercio_Invisible.Tx_CodAdn.Text;
            modelo.Tx_ER = this.InitObject.Frm_Comercio_Invisible.Tx_ER.Text;
            //***************************************************

            //*********************** FRAMES ************************************************
            modelo.Fr_Ope_V = this.InitObject.Frm_Comercio_Invisible.Fr_Ope.Visible;
            modelo.Fr_Ope_D_V = this.InitObject.Frm_Comercio_Invisible.Fr_OpeD.Visible;
            modelo.Fr_Ofi_V = this.InitObject.Frm_Comercio_Invisible.Fr_OFI.Visible;
            modelo.Fr_Sec_V = this.InitObject.Frm_Comercio_Invisible.Fr_Sec.Visible;
            modelo.Fr_Convenio_V = this.InitObject.Frm_Comercio_Invisible.Fr_Convenio.Visible;
            modelo.Fr_Autori_V = this.InitObject.Frm_Comercio_Invisible.Fr_Autori.Visible;
            modelo.Fr_Declaracion_V = this.InitObject.Frm_Comercio_Invisible.Fr_Declaracion.Visible;
            modelo.Fr_OpRe_V = this.InitObject.Frm_Comercio_Invisible.Fr_OpRe.Visible;

            modelo.Fr_Ope_E = this.InitObject.Frm_Comercio_Invisible.Fr_Ope.Enabled;
            modelo.Fr_Ope_D_E = this.InitObject.Frm_Comercio_Invisible.Fr_OpeD.Enabled;
            modelo.Fr_Ofi_E = this.InitObject.Frm_Comercio_Invisible.Fr_OFI.Enabled;
            modelo.Fr_Sec_E = this.InitObject.Frm_Comercio_Invisible.Fr_Sec.Enabled;
            modelo.Fr_Convenio_E = this.InitObject.Frm_Comercio_Invisible.Fr_Convenio.Enabled;
            modelo.Fr_Autori_E = this.InitObject.Frm_Comercio_Invisible.Fr_Autori.Enabled;
            modelo.Fr_Declaracion_E = this.InitObject.Frm_Comercio_Invisible.Fr_Declaracion.Enabled;
            modelo.Fr_OpRe_E = this.InitObject.Frm_Comercio_Invisible.Fr_OpRe.Enabled;

            //***************************************************

            //******* OPERACIONES
            int i = 0;
            foreach (var elem in this.InitObject.Frm_Comercio_Invisible.Lt_Operacion.Items)
            {
                modelo.Lt_Operacion.Add(new Lt_Operacion_Model()
                {
                    Indice = i++,
                    Moneda = elem.GetColumn("moneda"),
                    Monto = elem.GetColumn("monto"),
                    Tipo = elem.GetColumn("tipo")
                });
            }

            modelo.Errores = this.InitObject.Frm_Comercio_Invisible.Errors.Select(x => x.Text).ToList();
            this.InitObject.Frm_Comercio_Invisible.Errors.Clear();
            //*******************
            return modelo;
        }
        public IngresoValoresViewModel GetIngresoValoresViewModel()
        {
            IngresoValoresViewModel modelo = new IngresoValoresViewModel();
            modelo.Moneda = this.InitObject.Frm_Ingreso_Valores.Moneda.Text;
            modelo.Fecha = this.InitObject.Frm_Ingreso_Valores.Fecha.Text;
            modelo.Paridad = this.InitObject.Frm_Ingreso_Valores.Paridad.Text;
            modelo.TipoCambio = this.InitObject.Frm_Ingreso_Valores.TC_Obs.Text;
            return modelo;
        }
        public ArbitrajesViewModel GetArbitrajesViewModel()
        {
            return null;
            //ArbitrajesViewModel modelo = new ArbitrajesViewModel();
            //modelo.irAIngresoValores = !String.IsNullOrEmpty(this.InitObject.FormularioQueAbrir) && (this.InitObject.FormularioQueAbrir.Equals("Ingreso_Valores"));
            //modelo.Headers = this.InitObject.Frm_Arbitrajes.Lt_Operacion.Header;
            //modelo.Cb_Pais = this.InitObject.Frm_Arbitrajes.Cb_Pais.Items.Select(x => new SelectListItem()
            //{
            //    Text = x.Value,
            //    Value = x.ID
            //}).ToList();
            //modelo.Cb_Moneda_Compra = this.InitObject.Frm_Arbitrajes.Cb_Moneda_Compra.Items.Select(x => new SelectListItem()
            //{
            //    Text = x.Value,
            //    Value = x.ID
            //}).ToList();
            //modelo.Cb_Moneda_Venta = this.InitObject.Frm_Arbitrajes.Cb_Moneda_Venta.Items.Select(x => new SelectListItem()
            //{
            //    Text = x.Value,
            //    Value = x.ID
            //}).ToList();

            //modelo.indexPais = this.InitObject.Frm_Arbitrajes.Cb_Pais.ListIndex;
            //try
            //{
            //    modelo.idPais = int.Parse(this.InitObject.Frm_Arbitrajes.Cb_Pais.Items.ElementAt(this.InitObject.Frm_Arbitrajes.Cb_Pais.ListIndex).ID);
            //}
            //catch
            //{
            //    modelo.idPais = -1;
            //}

            //try
            //{
            //    modelo.indexMonedaVenta = this.InitObject.Frm_Arbitrajes.Cb_Moneda_Venta.ListIndex;
            //}
            //catch
            //{
            //    modelo.indexMonedaVenta = -1;
            //}

            //try
            //{
            //    modelo.indexMonedaCompra = this.InitObject.Frm_Arbitrajes.Cb_Moneda_Compra.ListIndex;
            //}
            //catch
            //{
            //    modelo.indexMonedaCompra = -1;
            //}


            //modelo.Cb_Pais_Habilitado = this.InitObject.Frm_Arbitrajes.Cb_Pais.Enabled;

            //modelo.Tx_Mtoarb_000 = this.InitObject.Frm_Arbitrajes.Tx_Mtoarb[0].Text;
            //modelo.Tx_Mtoarb_001 = this.InitObject.Frm_Arbitrajes.Tx_Mtoarb[1].Text;
            //modelo.Tx_Mtoarb_002 = this.InitObject.Frm_Arbitrajes.Tx_Mtoarb[2].Text;
            //modelo.Tx_Mtoarb_003 = this.InitObject.Frm_Arbitrajes.Tx_Mtoarb[3].Text;
            //modelo.Tx_Mtoarb_004 = this.InitObject.Frm_Arbitrajes.Tx_Mtoarb[4].Text;
            //modelo.Tx_Mtoarb_005 = this.InitObject.Frm_Arbitrajes.Tx_Mtoarb[5].Text;

            //modelo.Lt_Operacion = this.InitObject.Frm_Arbitrajes.Lt_Operacion.Items.Select((x => new Lt_Operacion_Arbitraje_Model()
            //{
            //    MonedaCompra = x.GetColumn("mdacom"),
            //    MontoCompra = x.GetColumn("moncom"),
            //    MonedaVenta = x.GetColumn("mdaven"),
            //    MontoVenta = x.GetColumn("monven")
            //})).ToList();

            //modelo.MensajesDeError = this.InitObject.Frm_Arbitrajes.PopUps.Where(x => x.Type == TipoMensaje.Error || x.Type == TipoMensaje.Informacion).Select(x => x.Text).ToList();
            //modelo.MensajesDeConfirmacion = this.InitObject.Frm_Arbitrajes.PopUps.Where(x => x.Type == TipoMensaje.YesNo).Select(x => x.Text).ToList();
            //this.InitObject.Frm_Arbitrajes.PopUps = new List<UI_Message>();
            //modelo.Ch_Futuro = (this.InitObject.Frm_Arbitrajes.Ch_Futuro.Value == -1) ? true : false;
            //return modelo;
        }
        public ComercioVisibleExportViewModel GetComercioVisibleExportViewModel()
        {
            var model = new ComercioVisibleExportViewModel();
            model.Caption = this.InitObject.Frm_VisE.Caption;
            model.Cb_Mnd = this.InitObject.Frm_VisE.Cb_Mnd.Items.Select(x => new SelectListItem()
            {
                Value = x.ID,
                Text = x.Value
            }).ToList();
            model.indexCbMnd = this.InitObject.Frm_VisE.Cb_Mnd.ListIndex;
            model.idCbMnd = model.indexCbMnd == -1 ? model.indexCbMnd : int.Parse(this.InitObject.Frm_VisE.Cb_Mnd.Items.ElementAt(model.indexCbMnd).ID);
            model.Tx_MtoVisE_000 = this.InitObject.Frm_VisE.Tx_MtoVisE[0].Text;
            model.Tx_MtoVisE_001 = this.InitObject.Frm_VisE.Tx_MtoVisE[1].Text;
            model.Tx_MtoVisE_002 = this.InitObject.Frm_VisE.Tx_MtoVisE[2].Text;
            model.Tx_MtoVisE_003 = this.InitObject.Frm_VisE.Tx_MtoVisE[3].Text;
            model.MensajesDeError = this.InitObject.Frm_VisE.Messages;
            return model;
        }

        public void SetPlanillaVisibleImportViewModel(PlanillaVisibleImportViewModel model)
        {
            InitObject.Frm_PlvSO.Tx_NumDec.Text = model.Tx_NumDec;
            InitObject.Frm_PlvSO.Tx_FecDec.Text = model.Tx_FecDec;
            InitObject.Frm_PlvSO.Tx_CodPag.Text = model.Tx_CodPag;
            InitObject.Frm_PlvSO.Tx_NroPre.Text = model.Tx_NroPre;
            InitObject.Frm_PlvSO.Tx_FecRee.Text = model.Tx_FecRee;
            InitObject.Frm_PlvSO.Tx_Observ.Text = model.Tx_Observ;
            InitObject.Frm_PlvSO.Tx_MtoFob.Text = model.Tx_MtoFob;
            InitObject.Frm_PlvSO.Tx_MtoFle.Text = model.Tx_MtoFle;
            InitObject.Frm_PlvSO.Tx_MtoSeg.Text = model.Tx_MtoSeg;
            InitObject.Frm_PlvSO.Tx_TipCam.Text = model.Tx_TipCam;
            InitObject.Frm_PlvSO.Tx_Paridad.Text = model.Tx_Paridad;

            InitObject.Frm_PlvSO.Bot_Acepta.Enabled = model.Bot_Acepta;

            InitObject.Frm_PlvSO.Pn_CodMon.Text = model.Pn_CodMon;
            InitObject.Frm_PlvSO.Pn_NroPre.Text = model.Pn_NroPre;
            InitObject.Frm_PlvSO.Pn_Import.Text = model.Pn_Import;
            InitObject.Frm_PlvSO.Pn_RutImp.Text = model.Pn_RutImp;
            InitObject.Frm_PlvSO.Pn_ValCif.Text = model.Pn_ValCif;
            InitObject.Frm_PlvSO.Pn_MtoTot.Text = model.Pn_MtoTot;
            InitObject.Frm_PlvSO.Pn_CifDol.Text = model.Pn_CifDol;
            InitObject.Frm_PlvSO.Pn_TotDol.Text = model.Pn_TotDol;
            InitObject.Frm_PlvSO.Pn_TCDol.Text = model.Pn_TCDol;

            InitObject.Frm_PlvSO.Ch_Transf.Value = Convert.ToInt16(model.Ch_Transf ? 1 : 0);
            InitObject.Frm_PlvSO.Ch_PlanRee.Value = Convert.ToInt16(model.Ch_PlanRee ? 1 : 0);

            model.indexCbMoneda = InitObject.Frm_PlvSO.Cb_Moneda.get_Index(model.idCbMoneda);
            model.indexCbPbc = InitObject.Frm_PlvSO.Cb_Pbc.get_Index(model.idCbPbc);
            model.indexCbPPago = model.idCbPPago != -1 ? InitObject.Frm_PlvSO.Cb_PPago.get_Index(model.idCbPPago) : model.idCbPPago;

            InitObject.Frm_PlvSO.Cb_Moneda.ListIndex = model.indexCbMoneda;
            InitObject.Frm_PlvSO.Cb_PPago.ListIndex = model.indexCbPPago;
            InitObject.Frm_PlvSO.Cb_Pbc.ListIndex = model.indexCbPbc;
            InitObject.Frm_PlvSO.Lt_Final.ListIndex = model.indexFinal;
        }

        public PlanillaVisibleImportViewModel GetPlanillaVisibleImportViewModel()
        {
            try
            {
                var model = new PlanillaVisibleImportViewModel();
                model.FormularioQueAbrir = this.InitObject.FormularioQueAbrir;
                model.Tx_NumDec = this.InitObject.Frm_PlvSO.Tx_NumDec.Text;
                model.Tx_FecDec = this.InitObject.Frm_PlvSO.Tx_FecDec.Text;
                model.Tx_CodPag = this.InitObject.Frm_PlvSO.Tx_CodPag.Text;
                model.Tx_NroPre = this.InitObject.Frm_PlvSO.Tx_NroPre.Text;
                model.Tx_FecRee = this.InitObject.Frm_PlvSO.Tx_FecRee.Text;
                model.Tx_Observ = this.InitObject.Frm_PlvSO.Tx_Observ.Text;
                model.Tx_MtoFob = this.InitObject.Frm_PlvSO.Tx_MtoFob.Text;
                model.Tx_MtoFle = this.InitObject.Frm_PlvSO.Tx_MtoFle.Text;
                model.Tx_MtoSeg = this.InitObject.Frm_PlvSO.Tx_MtoSeg.Text;
                model.Tx_TipCam = this.InitObject.Frm_PlvSO.Tx_TipCam.Text;
                model.Tx_Paridad = this.InitObject.Frm_PlvSO.Tx_Paridad.Text;
                model.Tx_Paridad_Enabled = this.InitObject.Frm_PlvSO.Tx_Paridad.Enabled;
                model.Pn_CodMon = this.InitObject.Frm_PlvSO.Pn_CodMon.Text;
                model.Pn_NroPre = this.InitObject.Frm_PlvSO.Pn_NroPre.Text;
                model.Pn_Import = this.InitObject.Frm_PlvSO.Pn_Import.Text;
                model.Pn_RutImp = this.InitObject.Frm_PlvSO.Pn_RutImp.Text;
                model.Pn_ValCif = this.InitObject.Frm_PlvSO.Pn_ValCif.Text;
                model.Pn_MtoTot = this.InitObject.Frm_PlvSO.Pn_MtoTot.Text;
                model.Pn_CifDol = this.InitObject.Frm_PlvSO.Pn_CifDol.Text;
                model.Pn_TotDol = this.InitObject.Frm_PlvSO.Pn_TotDol.Text;
                model.Pn_TCDol = this.InitObject.Frm_PlvSO.Pn_TCDol.Text;

                model.Ch_Transf = this.InitObject.Frm_PlvSO.Ch_Transf.Value == 1;
                model.Ch_PlanRee = this.InitObject.Frm_PlvSO.Ch_PlanRee.Value == 1;

                model.Bot_Acepta = this.InitObject.Frm_PlvSO.Bot_Acepta.Enabled;

                //Cb_Moneda
                model.Cb_Moneda = this.InitObject.Frm_PlvSO.Cb_Moneda.Items.Select(x => new SelectListItem()
                {
                    Value = x.ID,
                    Text = x.Value
                }).ToList();
                model.indexCbMoneda = this.InitObject.Frm_PlvSO.Cb_Moneda.ListIndex;
                if (model.indexCbMoneda > 0)//se selecciona por defecto la segunda fila USD.
                {
                    model.idCbMoneda = int.Parse(this.InitObject.Frm_PlvSO.Cb_Moneda.Items.ElementAt(model.indexCbMoneda).ID);
                }

                //Cb_PPago
                model.Cb_PPago = this.InitObject.Frm_PlvSO.Cb_PPago.Items.Select(x => new SelectListItem()
                {
                    Value = x.ID,
                    Text = x.Value
                }).ToList();

                model.indexCbPPago = this.InitObject.Frm_PlvSO.Cb_PPago.ListIndex;
                if (model.indexCbPPago >= 0) //se selecciona por defecto la primera fila.
                {
                    model.idCbPPago = int.Parse(this.InitObject.Frm_PlvSO.Cb_PPago.Items.ElementAt(model.indexCbPPago).ID);
                }

                //Cb_Pbc
                model.Cb_Pbc = this.InitObject.Frm_PlvSO.Cb_Pbc.Items.Select(x => new SelectListItem()
                {
                    Value = x.ID,
                    Text = x.Value
                }).ToList();
                model.indexCbPbc = this.InitObject.Frm_PlvSO.Cb_Pbc.ListIndex;
                if (model.indexCbPbc >= 0)//se selecciona por defecto santiago
                {
                    model.idCbPbc = int.Parse(this.InitObject.Frm_PlvSO.Cb_Pbc.Items.ElementAt(model.indexCbPbc).ID);
                }

                //Lt_Final
                model.Lt_Final = this.InitObject.Frm_PlvSO.Lt_Final.Items.Select(x => new SelectListItem()
                {
                    Value = x.Data.ToString(),
                    Text = x.Value.Replace(" ", "\xA0").Replace("\t", "\xA0\xA0\xA0"),
                }).ToList();
                model.indexFinal = this.InitObject.Frm_PlvSO.Lt_Final.ListIndex;
                model.selectedLtFinal = model.Lt_Final.Select(x => x.Text);

                model.MensajesDeError = this.InitObject.Mdi_Principal.MESSAGES.Select(x => x.Text).ToList();
                return model;
            }
            catch
            {
                return null;
            }
        }

        public OrigenFondosViewModel GetOrigenFondosViewModel()
        {
            var model = new OrigenFondosViewModel();

            model.l_mto_headers = this.InitObject.Frm_Origen_Fondos.l_mto.Header;
            model.l_mto_items = this.InitObject.Frm_Origen_Fondos.l_mto.Items.Select(x => new L_Mto_Model()
            {
                Moneda = x.GetColumn("NemMon"),
                Monto = x.GetColumn("MtoTot")
            }).ToList<L_Mto_Model>();

            //TODO: CAMBIAR EL HEADER
            model.l_ori_headers = this.InitObject.Frm_Origen_Fondos.l_ori.Header;
            model.l_ori_items = this.InitObject.Frm_Origen_Fondos.l_ori.Items.Select(x => new L_Fondo_Model()
            {
                Monto = x.GetColumn("MtoTot"),
                Moneda = x.GetColumn("NemMon"),
                Fondo = x.GetColumn("NomOri")
            }).ToList();

            model.l_mnd = this.InitObject.Frm_Origen_Fondos.l_mnd.Items.Select(x => new SelectListItem()
            {
                Text = x.Value,
                Value = x.ID
            }).ToList();
            try
            {
                model.selected_mnd_id = int.Parse(this.InitObject.Frm_Origen_Fondos.l_mnd.get_ItemId(this.InitObject.Frm_Origen_Fondos.l_mnd.ListIndex));
            }
            catch (Exception e)
            {
                model.selected_mnd_id = -1;
            }


            model.L_Partys = this.InitObject.Frm_Origen_Fondos.L_Partys.Items.Select(x => new SelectListItem()
            {
                Text = x.Value,
                Value = x.ID
            }).ToList();
            try
            {
                model.selected_partys_id = int.Parse(this.InitObject.Frm_Origen_Fondos.L_Partys.get_ItemId(this.InitObject.Frm_Origen_Fondos.L_Partys.ListIndex));
            }
            catch (Exception e)
            {
                model.selected_partys_id = -1;
            }


            model.L_Cuentas = this.InitObject.Frm_Origen_Fondos.L_Cuentas.Items.Select(x => new SelectListItem()
            {
                Text = x.Value,
                Value = x.ID
            }).ToList();
            try
            {
                model.selected_cuentas_id = int.Parse(this.InitObject.Frm_Origen_Fondos.L_Partys.get_ItemId(this.InitObject.Frm_Origen_Fondos.L_Partys.ListIndex));
            }
            catch (Exception e)
            {
                model.selected_cuentas_id = -1;
            }


            model.cmb_codtran = this.InitObject.Frm_Origen_Fondos.cmb_codtran.Items.Select(x => new SelectListItem()
            {
                Text = x.Value,
                Value = x.ID
            }).ToList();
            try
            {
                model.selected_cmb_codtran = int.Parse(this.InitObject.Frm_Origen_Fondos.cmb_codtran.get_ItemId(this.InitObject.Frm_Origen_Fondos.cmb_codtran.ListIndex));
            }
            catch (Exception e)
            {
                model.selected_cmb_codtran = -1;
            }

            model.L_Cta = this.InitObject.Frm_Origen_Fondos.L_Cta.Items.Select(x => new SelectListItem()
            {
                Text = x.Value,
                Value = x.ID
            }).ToList();
            try
            {
                model.selected_l_cta_id = int.Parse(this.InitObject.Frm_Origen_Fondos.L_Cta.get_ItemId(this.InitObject.Frm_Origen_Fondos.L_Cta.ListIndex));
            }
            catch (Exception e)
            {
                model.selected_l_cta_id = -1;
            }


            model.txt_cuenta = this.InitObject.Frm_Origen_Fondos.txt_cuenta.Text;
            model.txt_CRN = this.InitObject.Frm_Origen_Fondos.txt_CRN.Text;

            model.frm_infoctagap_enabled = this.InitObject.Frm_Origen_Fondos.frm_infoctagap.Enabled;
            model.frm_infoctagap_visible = this.InitObject.Frm_Origen_Fondos.frm_infoctagap.Visible;

            model.MtoOri = this.InitObject.Frm_Origen_Fondos.MtoOri.Text;

            return model;
        }

        public DestinoFondosViewModel GetDestinoFondosViewModel()
        {
            var model = new DestinoFondosViewModel();

            model.l_mto_headers = this.InitObject.Frm_Destino_Fondos.l_mto.Header;
            model.l_mto_items = this.InitObject.Frm_Destino_Fondos.l_mto.Items.Select(x => new L_Mto_Model()
            {
                Moneda = x.GetColumn("NemMon"),
                Monto = x.GetColumn("MtoTot")
            }).ToList();

            //TODO: CAMBIAR EL HEADER
            model.l_via_headers = this.InitObject.Frm_Destino_Fondos.l_via.Header;
            model.l_via_items = this.InitObject.Frm_Destino_Fondos.l_via.Items.Select(x => new L_Fondo_Model()
            {
                Monto = x.GetColumn("MtoTot"),
                Moneda = x.GetColumn("NemMon"),
                Fondo = x.GetColumn("NomVia")
            }).ToList();

            model.l_mnd = this.InitObject.Frm_Destino_Fondos.L_Mnd.Items.Select(x => new SelectListItem()
            {
                Text = x.Value,
                Value = x.ID
            }).ToList();
            try
            {
                model.selected_mnd_id = int.Parse(this.InitObject.Frm_Destino_Fondos.L_Mnd.get_ItemId(this.InitObject.Frm_Origen_Fondos.l_mnd.ListIndex));
            }
            catch (Exception e)
            {
                model.selected_mnd_id = -1;
            }


            model.L_Partys = this.InitObject.Frm_Destino_Fondos.L_Partys.Items.Select(x => new SelectListItem()
            {
                Text = x.Value,
                Value = x.ID
            }).ToList();
            try
            {
                model.selected_partys_id = int.Parse(this.InitObject.Frm_Destino_Fondos.L_Partys.get_ItemId(this.InitObject.Frm_Origen_Fondos.L_Partys.ListIndex));
            }
            catch (Exception e)
            {
                model.selected_partys_id = -1;
            }


            model.L_Cuentas = this.InitObject.Frm_Destino_Fondos.L_Cuentas.Items.Select(x => new SelectListItem()
            {
                Text = x.Value,
                Value = x.ID
            }).ToList();
            try
            {
                model.selected_cuentas_id = int.Parse(this.InitObject.Frm_Destino_Fondos.L_Partys.get_ItemId(this.InitObject.Frm_Origen_Fondos.L_Partys.ListIndex));
            }
            catch (Exception e)
            {
                model.selected_cuentas_id = -1;
            }


            model.Cb_Destino = this.InitObject.Frm_Destino_Fondos.Cb_Destino.Items.Select(x => new SelectListItem()
            {
                Text = x.Value,
                Value = x.ID
            }).ToList();
            try
            {
                model.selected_cb_destino_id = int.Parse(this.InitObject.Frm_Destino_Fondos.Cb_Destino.get_ItemId(this.InitObject.Frm_Origen_Fondos.cmb_codtran.ListIndex));
            }
            catch (Exception e)
            {
                model.selected_cb_destino_id = -1;
            }
            model.Cb_Destino_E = this.InitObject.Frm_Destino_Fondos.Cb_Destino.Enabled;

            model.L_Cta = this.InitObject.Frm_Destino_Fondos.L_Cta.Items.Select(x => new SelectListItem()
            {
                Text = x.Value,
                Value = x.ID
            }).ToList();
            try
            {
                model.selected_l_cta_id = int.Parse(this.InitObject.Frm_Destino_Fondos.L_Cta.get_ItemId(this.InitObject.Frm_Origen_Fondos.L_Cta.ListIndex));
            }
            catch (Exception e)
            {
                model.selected_l_cta_id = -1;
            }

            model.MtoVia = this.InitObject.Frm_Destino_Fondos.MtoVia.Text;

            model.cmb_codtran = this.InitObject.Frm_Destino_Fondos.cmb_codtran.Items.Select(x => new SelectListItem()
            {
                Value = x.ID,
                Text = x.Value
            }).ToList();
            try
            {
                model.selected_cmb_codtran_id = int.Parse(this.InitObject.Frm_Destino_Fondos.cmb_codtran.get_ItemId(this.InitObject.Frm_Origen_Fondos.cmb_codtran.ListIndex));
            }
            catch (Exception e)
            {
                model.selected_cmb_codtran_id = -1;
            }
            model.cmb_codtran_V = this.InitObject.Frm_Destino_Fondos.cmb_codtran.Visible;
            model.cmb_codtran_E = this.InitObject.Frm_Destino_Fondos.cmb_codtran.Enabled;

            model.MensajesDeError = this.InitObject.Frm_Destino_Fondos.Errors.Select(x => x.Text).ToList();
            this.InitObject.Frm_Destino_Fondos.Errors.Clear();

            model.MostrarVolverADefinir = this.InitObject.Frm_Destino_Fondos.MostrarVolverADefinir;

            return model;
        }
        public void SetDestinoFondosViewModel(DestinoFondosViewModel model)
        {
            this.InitObject.Frm_Destino_Fondos.l_mto.ListIndex = model.l_mto_id;
            try
            {
                this.InitObject.Frm_Destino_Fondos.L_Mnd.ListIndex = model.l_mnd.FindIndex(x => x.Value.Equals(model.selected_mnd_id.ToString()));
            }
            catch (Exception e)
            {

            }
            try
            {
                this.InitObject.Frm_Origen_Fondos.L_Partys.ListIndex = model.L_Partys.FindIndex(x => x.Value.Equals(model.selected_partys_id.ToString()));
            }
            catch (Exception e)
            {

            }
            try
            {
                this.InitObject.Frm_Origen_Fondos.L_Cuentas.ListIndex = model.L_Cuentas.FindIndex(x => x.Value.Equals(model.selected_cuentas_id.ToString()));
            }
            catch (Exception e)
            {

            }



            model.Cb_Destino = this.InitObject.Frm_Destino_Fondos.Cb_Destino.Items.Select(x => new SelectListItem()
            {
                Text = x.Value,
                Value = x.ID
            }).ToList();
            try
            {
                model.selected_cb_destino_id = int.Parse(this.InitObject.Frm_Destino_Fondos.Cb_Destino.get_ItemId(this.InitObject.Frm_Origen_Fondos.cmb_codtran.ListIndex));
            }
            catch (Exception e)
            {
                model.selected_cb_destino_id = -1;
            }
            model.Cb_Destino_E = this.InitObject.Frm_Destino_Fondos.Cb_Destino.Enabled;

            model.L_Cta = this.InitObject.Frm_Destino_Fondos.L_Cta.Items.Select(x => new SelectListItem()
            {
                Text = x.Value,
                Value = x.ID
            }).ToList();
            try
            {
                model.selected_l_cta_id = int.Parse(this.InitObject.Frm_Destino_Fondos.L_Cta.get_ItemId(this.InitObject.Frm_Origen_Fondos.L_Cta.ListIndex));
            }
            catch (Exception e)
            {
                model.selected_l_cta_id = -1;
            }

            model.MtoVia = this.InitObject.Frm_Destino_Fondos.MtoVia.Text;

            model.cmb_codtran = this.InitObject.Frm_Destino_Fondos.cmb_codtran.Items.Select(x => new SelectListItem()
            {
                Value = x.ID,
                Text = x.Value
            }).ToList();
            try
            {
                model.selected_cmb_codtran_id = int.Parse(this.InitObject.Frm_Destino_Fondos.cmb_codtran.get_ItemId(this.InitObject.Frm_Origen_Fondos.cmb_codtran.ListIndex));
            }
            catch (Exception e)
            {
                model.selected_cmb_codtran_id = -1;
            }


        }

        public TicketViewModel GetTicketViewModel()
        {
            var model = new TicketViewModel();
            model.CAM_Concepto = this.InitObject.Frm_Ticket.CAM_Concepto.Text;
            model.CAM_Cuenta = this.InitObject.Frm_Ticket.CAM_Cuenta.Text;
            model.CAM_Monto = this.InitObject.Frm_Ticket.CAM_Monto.Text;
            model.CAM_Nemonico = this.InitObject.Frm_Ticket.CAM_Nemonico.Text;
            model.CAM_Nombre = this.InitObject.Frm_Ticket.CAM_Nombre.Text;

            model.CBO_DeHa = this.InitObject.Frm_Ticket.CBO_DeHa.Items.Select(x => new SelectListItem()
            {
                Value = x.ID,
                Text = x.Value
            }).ToList();
            model.Cb_ticket = this.InitObject.Frm_Ticket.Cb_ticket.Items.Select(x => new SelectListItem()
            {
                Value = x.ID,
                Text = x.Value
            }).ToList();

            model.selected_cbo_deha_id = this.InitObject.Frm_Ticket.CBO_DeHa.ListIndex;
            model.selected_cb_ticket_id = this.InitObject.Frm_Ticket.Cb_ticket.ListIndex;
            model.otro = this.InitObject.Frm_Ticket.otro.Value == -1 ? true : false;
            return model;
        }

        //public EmitirChequeViewModel GetAEmitirChequesViewModel()
        //{
        //    EmitirChequeViewModel modelo = new EmitirChequeViewModel();

        //    modelo.Label1 = this.InitObject.Frm_Chq.Label1.Text;
        //    modelo.Label2 = this.InitObject.Frm_Chq.Label2.Text;
        //    modelo.Label3 = this.InitObject.Frm_Chq.Label3.Text;
        //    modelo.Lb_Corresponsal = this.InitObject.Frm_Chq.Lb_Corresponsal.Text;
        //    modelo.Lb_Nombre = this.InitObject.Frm_Chq.Lb_Nombre.Text;
        //    modelo.Lb_rut = this.InitObject.Frm_Chq.Lb_rut.Text;

        //    modelo.Tx_Nombre = this.InitObject.Frm_Chq.Tx_Nombre.Text;
        //    modelo.Tx_Rut = this.InitObject.Frm_Chq.Tx_Rut.Text;

        //    modelo.l_plaza = this.InitObject.Frm_Chq.l_plaza.Items.Select(p=>new SelectListItem(){Text=p.Value, Value=p.Data.ToString()}).ToList();
        //    modelo.l_Benef = this.InitObject.Frm_Chq.l_benef.Items.Select(p=>new SelectListItem(){Text=p.Value, Value=p.Data.ToString()}).ToList();

        //    modelo.l_cor = this.InitObject.Frm_Chq.l_cor.Items.Select(o => new CorresponsalesModel() { Descripcion = o.Data.ToString()+o.Value }).ToList();
        //    //modelo.l_montos = this.InitObject.Frm_Chq.l_montos.Items.Select(o => new MontosModel() { Moneda = o.Value.ToString().Split('\t')[0].Trim(), Monto = o.Value.ToString().Split('\t')[1].Trim(), Documento = o.Value.ToString().Split('\t')[2].Trim(), Generado = o.Value.ToString().Split('\t')[3].Trim() }).ToList();

        //    modelo.Headers = new List<string>() { "Moneda", "Monto", "Documento", "Generado" };

        //    return modelo;
        //}

        #endregion

        #region EVENTOS INDEX
        [HttpPost]
        public JsonResult Index_Tx_RefCli_Blur(string text)
        {
            this.InitObject.Frm_Principal.Tx_RefCli.Text = text;
            ftService.Index_Tx_RefCli_Blur(this.InitObject);
            var model = new IndexViewModel(this.InitObject.Frm_Principal, this.InitObject.Mdi_Principal);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

            #region EVENTOS COMERCIO INVISIBLE
        #region AUXILIARES
        private void MergeModelComercioInvisible(ComercioInvisibleViewModel model, InitializationObject initObject)
        {
            var o = initObject.Frm_Comercio_Invisible;

            o.Tx_CanAc.Text = model.Tx_CanAc;

            o.ch_AfDer.Value = (short)(model.ch_AfDer ? -1 : 0);
            o.ch_ZoFra.Value = (short)(model.ch_ZoFra ? -1 : 0);

            o.Tx_NumCon.Text = model.Tx_NumCon;
            o.Tx_FecSus.Text = model.Tx_FecSus;
            o.Tx_FecVen.Text = model.Tx_FecVen;
            o.Cb_InsUt.ListIndex = o.Cb_InsUt.Items.FindIndex(x => x.ID.Equals(model.indexInsUt.ToString()));
            o.Tx_ParTip.Text = model.Tx_ParTip;
            o.Cb_ArCon.ListIndex = o.Cb_ArCon.Items.FindIndex(x => x.ID.Equals(model.indexArCon.ToString()));

            o.Tx_NumIns.Text = model.Tx_NumIns;
            o.Tx_FecIns.Text = model.Tx_FecIns;
            o.Tx_NomFin.Text = model.Tx_NomFin;
            o.Tx_FecVC.Text = model.Tx_FecVC;
            o.Tx_Fecha.Text = model.Tx_Fecha;
            o.Cb_MonDes.ListIndex = o.Cb_MonDes.Items.FindIndex(x => x.ID.Equals(model.indexMonDes.ToString()));
            o.Tx_Mto.Text = model.Tx_Mto;

            o.Tx_FecDeb.Text = model.Tx_FecDeb;
            o.Tx_DocNac.Text = model.Tx_DocNac;
            o.Tx_DocExt.Text = model.Tx_DocExt;

            o.Cb_SecEcBen.ListIndex = o.Cb_SecEcBen.Items.FindIndex(x => x.ID.Equals(model.indexSecEcBen.ToString()));
            //o.Cb_SecEcIn.ListCount = o.Cb_SecEcIn.Items.FindIndex(x => x.ID.Equals(model.indexSecEcIn.ToString()));
            o.Tx_PrcPar.Text = model.Tx_PrcPar;

            o.Cb_TipAut.ListIndex = o.Cb_TipAut.Items.FindIndex(x => x.Data.Equals(model.indexTipAut));
            o.Tx_NroAut.Text = model.Tx_NroAut;
            o.Tx_FecAut.Text = model.Tx_FecAut;
            o.Tx_SucBcch.Text = model.Tx_SucBcch;

            o.Tx_NroDec.Text = model.Tx_NroDec;
            o.Tx_FecDec.Text = model.Tx_FecDec;
            o.Tx_CodAdn.Text = model.Tx_CodAdn;
            o.Tx_ER.Text = model.Tx_ER;

            o.Tx_FecPre.Text = model.Tx_FecPre;
            o.Tx_NumPre.Text = model.Tx_NumPre;
            o.Tx_CodIns.Text = model.Tx_CodIns;

        }
        #endregion

        public JsonResult COMINV_Form_Load()
        {
            try
            {
                if (this.InitObject.PagOri != "COMINV" && this.InitObject.PagOri != null)
                {
                    IniciarVariables();
                }
                ftService.LoadFrmComercioInvisible(this.InitObject);
                var a = Json(this.InitObject.Frm_Comercio_Invisible, JsonRequestBehavior.AllowGet);
                return a;
            }
            catch
            {
                return null;
            }
        }
        [HttpPost]
        public JsonResult COMINV_CB_Divisa_Blur(UI_Frm_Comercio_Invisibles jsonModel)
        {
            this.InitObject.Frm_Comercio_Invisible = jsonModel;
            ftService.CB_Divisa_Select(this.InitObject);
            this.InitObject.Frm_Comercio_Invisible.Lt_Tcp.Enabled = true;
            if (this.InitObject.DealActualSel != null && this.InitObject.DealPrevSel != null)
            {
                string TipoDivisaPag = this.InitObject.Frm_Comercio_Invisible.Cb_Divisa.Text;
                string TipoDivisaVM = this.InitObject.datSPMcambioCDD[(int)this.InitObject.DealActualSel - 1].stTipoTransaccion;
                if (TipoDivisaPag != TipoDivisaVM)
                {
                    IniciarVariables();
                }
            }
            if (this.InitObject.Frm_Comercio_Invisible.Cb_Divisa.Text == "Transf. interna")
            {
                this.InitObject.Frag_TransInt = 1;
            }
            else
            {
                this.InitObject.Frag_TransInt = 0;
            }
            return Json(this.InitObject.Frm_Comercio_Invisible);
        }
        [HttpPost]
        public JsonResult COMINV_CB_Moneda_Blur(UI_Frm_Comercio_Invisibles jsonModel)
        {
            string valTC="";
            this.InitObject.Frm_Comercio_Invisible = jsonModel;
            if (InitObject.Frm_Comercio_Invisible.Tx_MtoCV[1].Enabled == false)
            {
                valTC = InitObject.Frm_Comercio_Invisible.Tx_MtoCV[1].Text;
            }
            ftService.CB_Moneda_Select(this.InitObject);
            if (valTC != "")
            {
                InitObject.Frm_Comercio_Invisible.Tx_MtoCV[1].Enabled = false;
                InitObject.Frm_Comercio_Invisible.Tx_MtoCV[1].Text = valTC;
                InitObject.Frm_Comercio_Invisible.Tx_MtoCV[2].Text = "";
            }
            return Json(this.InitObject.Frm_Comercio_Invisible);
        }
        [HttpPost]
        public void PlvSO_ConsPrec2(PlanillaVisibleImportViewModel jsonModel)
        {
            if (InitObject.Frm_PlvSO.Tx_TipCam.Enabled == true)
            {
                this.InitObject.PagOri = PaginaOrigen.PlvSO.ToString();
                McambioService McambioServ = new McambioService();
                jsonModel.MensajesDeError = new List<string> { };
                double ValidarIngPiz = McambioServ.PlvSO_ValidarIngresoPiz(this.InitObject, jsonModel, this.InitObject.DealsIngParaProces, this.InitObject.PagOri);
                string TipIng = McambioServ.TipoIngreso(this.InitObject.DealsIngParaProces);
                if (ValidarIngPiz <= 0 && TipIng == "")
                {
                    string MontoIngresado = jsonModel.Tx_MtoFob;
                    double ResPrecioFinal = McambioServ.ConsultaPrecios2(this.InitObject, this.InitObject.PagOri, MontoIngresado);
                    InitObject.Frm_PlvSO.Tx_TipCam.Text = McambioServ.FormatSinRedondear(ResPrecioFinal, 2, ""); //ResPrecioFinal.ToString();
                    jsonModel.Tx_TipCam = InitObject.Frm_PlvSO.Tx_TipCam.Text;
                }
                if (this.InitObject.DealsIngParaProces != null)
                {
                    if (this.InitObject.DealsIngParaProces.Count() > 0)
                    {
                        if (this.InitObject.DealsIngParaProces[0].TipoIngreso == "P")
                        {
                            UI_Frm_PlvSO FrmFrm_PlvSO = InitObject.Frm_PlvSO;
                        }
                    }
                }
            }
        }
        [HttpPost]
        public JsonResult COMINV_ConsPrec2(UI_Frm_Comercio_Invisibles jsonModel) 
        {
            this.InitObject.Frm_Comercio_Invisible = jsonModel;
            if (InitObject.Frm_Comercio_Invisible.Tx_MtoCV[1].Enabled == true)
            {
                InitObject.Frm_Comercio_Invisible.Errors.Clear();
                this.InitObject.PagOri = PaginaOrigen.COMINV.ToString();
                McambioService McambioServ = new McambioService();
                double ValidarIngPiz = McambioServ.COMINV_ValidarIngresoPiz(this.InitObject, this.InitObject.PagOri, this.InitObject.DealsIngParaProces);
                string TipIng = McambioServ.TipoIngreso(this.InitObject.DealsIngParaProces);
                if (ValidarIngPiz <= 0 && TipIng == "")
                {
                    string MontoIngresado = jsonModel.Tx_MtoCV[0].Text;
                    double ResPrecioFinal = McambioServ.ConsultaPrecios2(this.InitObject, this.InitObject.PagOri, MontoIngresado);
                    InitObject.Frm_Comercio_Invisible.Tx_MtoCV[1].Text = McambioServ.FormatSinRedondear(ResPrecioFinal, 4, ""); //ResPrecioFinal.ToString();
                    jsonModel.Tx_MtoCV[1].Text = InitObject.Frm_Comercio_Invisible.Tx_MtoCV[1].Text;
                    McambioServ.COMING_SetearValores(this.InitObject);
                }
                if (this.InitObject.DealsIngParaProces != null)
                {
                    if (this.InitObject.DealsIngParaProces.Count() > 0)
                    {
                        if (this.InitObject.DealsIngParaProces[0].TipoIngreso == "P")
                        {
                            UI_Frm_Comercio_Invisibles FrmCOMINV = InitObject.Frm_Comercio_Invisible;
                            McambioServ.MsgsValidarIngreso(InitObject, 2, this.InitObject.PagOri, "");
                        }
                    }
                }
            }
            return Json(this.InitObject.Frm_Comercio_Invisible);
        }
        [HttpPost]
        public JsonResult COMINV_Lt_Tcp_Blur(UI_Frm_Comercio_Invisibles jsonModel)
        {
            this.InitObject.Frm_Comercio_Invisible = jsonModel;
            ftService.Lt_Tcp_Select(this.InitObject);
            return Json(this.InitObject.Frm_Comercio_Invisible);
        }
        [HttpPost]
        public JsonResult COMINV_Tx_MtoCV_Blur(short id, UI_Frm_Comercio_Invisibles jsonModel)
        {
            try
            {
                this.InitObject.Frm_Comercio_Invisible = jsonModel;
                ftService.Tx_MtoCV_Key(this.InitObject, id);
                ftService.Tx_MtoCV_Blur(this.InitObject, id);
                return Json(this.InitObject.Frm_Comercio_Invisible);
            }
            catch (Exception ex)
            {
                this.InitObject.Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = ex.Message
                });
            }
            return Json(this.InitObject.Frm_Comercio_Invisible);
        }
        [HttpPost]
        public JsonResult COMINV_Ch_Convenio_Change(UI_Frm_Comercio_Invisibles jsonModel)
        {
            this.InitObject.Frm_Comercio_Invisible = jsonModel;
            ftService.Ch_Convenio_Click(this.InitObject);
            return Json(this.InitObject.Frm_Comercio_Invisible);
        }
        [HttpPost]
        public JsonResult COMINV_Ok_Click(UI_Frm_Comercio_Invisibles jsonModel)
        {
            string TC = this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[1].Text;
            string MP = this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[2].Text;
            string JTC = jsonModel.Tx_MtoCV[1].Text;
            string JMP = jsonModel.Tx_MtoCV[2].Text;
            if (!string.IsNullOrEmpty(TC) && !string.IsNullOrEmpty(JTC))
            {
                if (double.Parse(TC) != double.Parse(JTC))
                {
                    jsonModel.Tx_MtoCV[1].Text = TC;
                    jsonModel.Tx_MtoCV[2].Text = MP;
                }
            }
            this.InitObject.Frm_Comercio_Invisible = jsonModel;
            if (jsonModel.Cb_InsUt.ListIndex != -1 && jsonModel.Cb_ArCon.ListIndex != -1)
            {
                this.InitObject.Frm_Comercio_Invisible.Cb_InsUt.ListIndex = jsonModel.Cb_InsUt.ListIndex - 1;
                this.InitObject.Frm_Comercio_Invisible.Cb_ArCon.ListIndex = jsonModel.Cb_ArCon.ListIndex - 1;
            }
            this.InitObject.Flag_eliminar = -1;
            McambioService McambioServ = new McambioService();
            int tipo = -1;
            int pos = -1;
            int MsgPizarra = 0;
            double TC_Ing = 0;
            Nullable<int> temp;
            if (this.InitObject.DealActualSel == null)
            {
                temp = null;
            }
            else
            {
                temp = (int)this.InitObject.DealActualSel;
            }
            string T = "";
            if (this.InitObject.DealsIngParaProces != null)
            {
                if (this.InitObject.DealsIngParaProces.Count > 0)
                {
                    T = this.InitObject.DealsIngParaProces[0].TipoIngreso;
                }
            }
            bool ComInvValidar = McambioServ.COMINV_Validar(this.InitObject, T);
            if (ComInvValidar == true)
            {
                TC_Ing = double.Parse(this.InitObject.Frm_Comercio_Invisible.Tx_MtoCV[1].Text);
                this.InitObject.PagOri = PaginaOrigen.COMINV.ToString();
                var rutclie = this.InitObject.MODGCVD.VgCvd.rutcli;
                if (this.InitObject.DealActualSel != null && this.InitObject.DealsIngParaProces == null)
                {
                    pos = (int)this.InitObject.DealActualSel;
                    pos--;
                }
                if (this.InitObject.DealsIngParaProces == null)
                {
                    if (pos == -1)
                    {
                        double ValidarIngPiz = McambioServ.COMINV_ValidarIngresoPiz(this.InitObject, this.InitObject.PagOri, this.InitObject.DealsIngParaProces);
                        if (ValidarIngPiz != 0)
                        {
                            InitObject.Frm_Comercio_Invisible.Tx_MtoCV[1].Text = "";
                            return Json(this.InitObject.Frm_Comercio_Invisible);
                        }
                    }
                    else
                    {
                        int ValidarIng = McambioServ.ValidarIngreso(this.InitObject, this.InitObject.datSPMcambioCDD, this.InitObject.DealsIngParaProces, this.InitObject.PagOri, pos, temp);
                        if (ValidarIng != 0)
                        {
                            return Json(this.InitObject.Frm_Comercio_Invisible);
                        }
                    }
                    List<DealsIngresadosParaProcesar> IniDealsIngParaProces = new List<DealsIngresadosParaProcesar>();
                    this.InitObject.DealsIngParaProces = IniDealsIngParaProces;
                    if (pos == -1)
                    {
                        MsgPizarra = 1;
                        pos = -1;
                    }
                    this.InitObject.DealActualSel = null;
                }
                else
                {
                    if (this.InitObject.DealActualSel == null)
                    {
                        temp = -1;
                    }
                    else
                    {
                        temp = (int)this.InitObject.DealActualSel;
                    }
                    int ValidarIng = McambioServ.ValidarIngreso(this.InitObject, this.InitObject.datSPMcambioCDD, this.InitObject.DealsIngParaProces, this.InitObject.PagOri, pos, temp);
                    if (ValidarIng != 0)
                    {
                        return Json(this.InitObject.Frm_Comercio_Invisible);
                    }
                }
                tipo = McambioServ.COMEX_DatosIngresados(this.InitObject, this.InitObject.datSPMcambioCDD, this.InitObject.PagOri, pos, this.InitObject.DealsIngParaProces);
                temp = null;
            }
            this.InitObject.Frm_Comercio_Invisible.Switch = 0;
            ftService.ok_Click(this.InitObject);
            int ErroCount = this.InitObject.Frm_Comercio_Invisible.Errors.Count();
            if (ComInvValidar == true)
            {
                if (tipo != -1 && ErroCount == 0)
                {
                    this.InitObject.DealManual = tipo;
                    MsgPizarra = 1;
                }
                else if (this.InitObject.DealActualSel != null && ErroCount == 0)
                {
                    if (pos == -1)
                    {
                        pos = 0;
                    }
                    if (TC_Ing != this.InitObject.datSPMcambioCDD[pos].precioCliente)
                    {
                        MsgPizarra = 1;
                    }
                }
            }
            if (MsgPizarra == 1)
            {
                this.InitObject.Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                {
                    Type = TipoMensaje.Informacion,
                    Text = "Se agrego operación con tipo de cambio pizarra.",
                    Title = "Aviso!:   "
                });
                this.InitObject.DealManual = 0;
            }
            this.InitObject.Frm_Comercio_Invisible.Lt_Tcp.Enabled = true;
            this.InitObject.DealActualSel = temp;
            return Json(this.InitObject.Frm_Comercio_Invisible);
        }
        [HttpPost]
        public JsonResult COMINV_No_Click(UI_Frm_Comercio_Invisibles jsonModel)
        {
            this.InitObject.Frm_Comercio_Invisible = jsonModel;
            ftService.no_Click(this.InitObject);
            if (this.InitObject.DealsIngParaProces != null)
            {
                if (this.InitObject.Flag_eliminar != -1)
                {
                    this.InitObject.DealsIngParaProces.RemoveAt(this.InitObject.Flag_eliminar);
                    this.InitObject.Flag_eliminar = -1;
                }
                if (this.InitObject.Frm_Comercio_Invisible.Lt_Operacion.ListCount == 0)
                {
                    this.InitObject.DealsIngParaProces = null;
                    this.InitObject.DealPrevSel = null;
                    this.InitObject.DealActualSel = null;
                    this.InitObject.DealManual = null;
                }
            }
            this.InitObject.Frm_Comercio_Invisible.Lt_Tcp.Enabled = true;
            return Json(this.InitObject.Frm_Comercio_Invisible);
        }
        [HttpPost]
        public JsonResult COMINV_Co_Btn_Click(UI_Frm_Comercio_Invisibles jsonModel, short id)
        {
            this.InitObject.Frm_Comercio_Invisible = jsonModel;
            ftService.co_boton(this.InitObject, id);
            InitObject.Frm_Comercio_Invisible.Errors = InitObject.Mdi_Principal.MESSAGES;
            return Json(this.InitObject.Frm_Comercio_Invisible);
        }
        [HttpPost]
        public JsonResult COMINV_Lt_Operacion_Click(UI_Frm_Comercio_Invisibles jsonModel)
        {
            if (this.InitObject.DealsIngParaProces != null)
            {
                this.InitObject.Flag_eliminar = jsonModel.Lt_Operacion.ListIndex;
            }
            this.InitObject.Frm_Comercio_Invisible = jsonModel;
            ftService.lt_operacion_click(this.InitObject);
            return Json(this.InitObject.Frm_Comercio_Invisible);
        }
        [HttpPost]
        public JsonResult COMINV_Tx_CodAdn_Blur(UI_Frm_Comercio_Invisibles jsonModel)
        {
            this.InitObject.Frm_Comercio_Invisible = jsonModel;
            ftService.tx_codadn_Blur(this.InitObject);
            return Json(this.InitObject.Frm_Comercio_Invisible);
        }
        [HttpPost]
        public JsonResult COMINV_Tx_DocNac_Blur(UI_Frm_Comercio_Invisibles jsonModel)
        {
            this.InitObject.Frm_Comercio_Invisible = jsonModel;
            ftService.tx_docnac_Blur(this.InitObject);
            return Json(this.InitObject.Frm_Comercio_Invisible);
        }
        [HttpPost]
        public JsonResult COMINV_Tx_ER_Blur(UI_Frm_Comercio_Invisibles jsonModel)
        {
            this.InitObject.Frm_Comercio_Invisible = jsonModel;
            ftService.tx_er_Blur(this.InitObject);
            return Json(this.InitObject.Frm_Comercio_Invisible);
        }
        [HttpPost]
        public JsonResult COMINV_Tx_NroDec_Blur(UI_Frm_Comercio_Invisibles jsonModel)
        {
            this.InitObject.Frm_Comercio_Invisible = jsonModel;
            ftService.tx_nrodec_Blur(this.InitObject);
            return Json(this.InitObject.Frm_Comercio_Invisible);
        }
        public ActionResult COMINV_Seguir()
        {
            return Rutear(() => { ftService.OpcionBotones(this.InitObject, 0); }, null);
        }
        #endregion

        #region EVENTOS ARBITRAJES
        public JsonResult ARBITRAJES_Get_Model()
        {
            return Json(this.InitObject.Frm_Arbitrajes, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ARBITRAJES_Cb_Moneda_Compra_Blur(UI_Frm_Arbitrajes jsonModel)
        {
            McambioService McambioServ = new McambioService();
            this.InitObject.PagOri = PaginaOrigen.ARBITRAJES.ToString();
            string valor;
            if (this.InitObject.DealActualSel != null)
            {
                int pos;
                if (this.InitObject.DealsIngParaProces != null)
                {
                    pos = 0;
                    if (this.InitObject.DealsIngParaProces.Count() > 0)
                    {
                        this.InitObject.DealActualSel = 1;
                    }
                }
                else
                {
                    pos = (int)this.InitObject.DealActualSel - 1;
                }
                var MonPai = McambioServ.GetParamComexMonedaPais(jsonModel.Cb_Moneda_Compra.Value.ToString(), "CodMoneda");
                valor = McambioServ.ObtenerDato(this.InitObject.datSPMcambioCDD, this.InitObject.DealsIngParaProces, this.InitObject.PagOri, pos, "m1");
                if (valor != MonPai.SiglaMoneda)
                {
                    try
                    {
                        if (MonPai.SiglaMoneda != null)
                        {
                            McambioServ.MsgsValidarIngreso(InitObject, 1, this.InitObject.PagOri, "");
                        }
                        ftService.LoadFrmArbitrajes(this.InitObject);
                        this.InitObject.Frm_Arbitrajes.Tx_Mtoarb[0].Enabled = true;
                        var a = Json(this.InitObject.Frm_Arbitrajes, JsonRequestBehavior.AllowGet);
                        return a;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            this.InitObject.Frm_Arbitrajes = jsonModel;
            ftService.ARBITRAJES_cb_moneda_compra_Blur(this.InitObject);
            if (this.InitObject.DealActualSel != null)
            {
                valor = McambioServ.ObtenerDato(this.InitObject.datSPMcambioCDD, this.InitObject.DealsIngParaProces, this.InitObject.PagOri, ((int)this.InitObject.DealActualSel - 1), "pi");
                this.InitObject.Frm_Arbitrajes.Tx_Mtoarb[1].Text = valor;
                ftService.ARBITRAJES_tx_mtoarb_keypress(this.InitObject, 1);
                ftService.ARBITRAJES_tx_mtoarb_lostfocus(this.InitObject, 1);
                return Json(this.InitObject.Frm_Arbitrajes, JsonRequestBehavior.AllowGet);
            }
            return Json(this.InitObject.Frm_Arbitrajes, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ARBITRAJES_Cb_Moneda_Venta_Blur(UI_Frm_Arbitrajes jsonModel)
        {
            McambioService McambioServ = new McambioService();
            this.InitObject.PagOri = PaginaOrigen.ARBITRAJES.ToString();
            string valor;
            if (this.InitObject.DealActualSel != null)
            {
                if (jsonModel.Cb_Moneda_Venta.Value.ToString() != null && jsonModel.Cb_Moneda_Venta.Value.ToString() != "")
                {
                    int pos;
                    if (this.InitObject.DealsIngParaProces != null)
                    {
                        pos = 0;
                    }
                    else
                    {
                        pos = (int)this.InitObject.DealActualSel - 1;
                    }
                    var MonPai = McambioServ.GetParamComexMonedaPais(jsonModel.Cb_Moneda_Venta.Value.ToString(), "CodMoneda");
                    valor = McambioServ.ObtenerDato(this.InitObject.datSPMcambioCDD, this.InitObject.DealsIngParaProces, this.InitObject.PagOri, pos, "m2");
                    if (valor != MonPai.SiglaMoneda)
                    {
                        try
                        {
                            if (MonPai.SiglaMoneda != null)
                            {
                                McambioServ.MsgsValidarIngreso(InitObject, 1, this.InitObject.PagOri, "");
                            }
                            this.InitObject.Frm_Arbitrajes.Cb_Moneda_Venta.ListIndex = -1;
                            ftService.LoadFrmArbitrajes(this.InitObject);
                            this.InitObject.Frm_Arbitrajes.Tx_Mtoarb[0].Enabled = true;
                            var a = Json(this.InitObject.Frm_Arbitrajes, JsonRequestBehavior.AllowGet);
                            return a;
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }
            }
            this.InitObject.Frm_Arbitrajes = jsonModel;
            ftService.ARBITRAJES_cb_moneda_venta_Blur(this.InitObject);
            if (this.InitObject.DealActualSel != null && this.InitObject.Frm_Arbitrajes.Tx_Mtoarb[1].Text != null)
            {
                valor = McambioServ.ObtenerDato(this.InitObject.datSPMcambioCDD, this.InitObject.DealsIngParaProces, this.InitObject.PagOri, ((int)this.InitObject.DealActualSel - 1), "pi");
                this.InitObject.Frm_Arbitrajes.Tx_Mtoarb[1].Text = valor;
                ftService.ARBITRAJES_tx_mtoarb_keypress(this.InitObject, 1);
                ftService.ARBITRAJES_tx_mtoarb_lostfocus(this.InitObject, 1);
                return Json(this.InitObject.Frm_Arbitrajes);
            }
            return Json(this.InitObject.Frm_Arbitrajes);
        }
        //el popup lo llama con el valor de si o no dependiendo de si el usuario acepto todos los popup
        [HttpPost]
        public JsonResult ARBITRAJES_ok_Click(bool acepta, UI_Frm_Arbitrajes jsonModel)
        {
            this.InitObject.Frm_Arbitrajes = jsonModel;
            McambioService McambioServ = new McambioService();
            this.InitObject.PagOri = PaginaOrigen.ARBITRAJES.ToString();
            int posUpd = -1;
            if (this.InitObject.DealActualSel != null)
            {
                int pos;
                pos = (int)this.InitObject.DealActualSel - 1;
                if (this.InitObject.datSPMcambioCDD.Count() != 0)
                {
                    if (this.InitObject.datSPMcambioCDD[pos].tipoTransaccion != "ArbVta")
                    {
                        var jsonCC = ARBITRAJES_Tx_Mtoarb_Blur(2, jsonModel);
                    }
                    else
                    {
                        var jsonVV = ARBITRAJES_Tx_Mtoarb_Blur(3, jsonModel);
                    }
                }
                double Mto1Json = double.Parse(jsonModel.Tx_Mtoarb[2].Text.Replace(".", ""));
                double Mto2Json = double.Parse(jsonModel.Tx_Mtoarb[3].Text.Replace(".", ""));
                double Mto1Deal = 0;
                double Mto2Deal = 0;
                if (this.InitObject.datSPMcambioCDD.Count() != 0)
                {
                    Mto1Deal = double.Parse(this.InitObject.datSPMcambioCDD[pos].stSaldoMoneda1);
                    Mto2Deal = double.Parse(this.InitObject.datSPMcambioCDD[pos].stSaldoMoneda2);
                }
                if (this.InitObject.Flag_eliminar != -1)
                {
                    if (this.InitObject.DealsIngParaProces.Count() != 0)
                    {
                        if (Mto1Json != Mto1Deal && this.InitObject.DealsIngParaProces[0].tipoTransaccion != "ArbVta")
                        {
                            var json = ARBITRAJES_Tx_Mtoarb_Blur(2, jsonModel);
                        }
                        else if (Mto2Json != Mto2Deal && this.InitObject.DealsIngParaProces[0].tipoTransaccion == "ArbVta")
                        {
                            var json = ARBITRAJES_Tx_Mtoarb_Blur(3, jsonModel);
                        }
                    }
                }
                else
                {
                    if (Mto1Json != Mto1Deal && this.InitObject.datSPMcambioCDD[pos].tipoTransaccion != "ArbVta")
                    {
                        var json = ARBITRAJES_Tx_Mtoarb_Blur(2, jsonModel);
                    }
                    else if (Mto2Json != Mto2Deal && this.InitObject.datSPMcambioCDD[pos].tipoTransaccion == "ArbVta")
                    {
                        var json = ARBITRAJES_Tx_Mtoarb_Blur(3, jsonModel);
                    }
                }
                
            }
            int ValidarIng;
            if (this.InitObject.Flag_eliminar != -1)
            {
                this.InitObject.SelUdp = this.InitObject.Flag_eliminar + 1;
                ValidarIng = McambioServ.ValidarIngresoUdp(this.InitObject, this.InitObject.DealsIngParaProces, this.InitObject.PagOri, 0, this.InitObject.SelUdp);
            }
            else
            {
                ValidarIng = McambioServ.ValidarIngreso(this.InitObject, this.InitObject.datSPMcambioCDD, this.InitObject.DealsIngParaProces, this.InitObject.PagOri, 0, this.InitObject.DealActualSel);
            }
            
            if (ValidarIng != 0)
            {
                return Json(this.InitObject.Frm_Arbitrajes, JsonRequestBehavior.AllowGet);
            }
            bool FnParArbSis = McambioService.Fn_ParidArbiSis(this.InitObject);
            ftService.ARBITRAJES_ok_Click(this.InitObject, acepta);
            if (this.InitObject.Frm_Arbitrajes.Errors.Count() == 0 && (acepta == true || FnParArbSis == false))
            {
                if (this.InitObject.Flag_eliminar != -1)
                {
                    this.InitObject.DealsIngParaProces[this.InitObject.Flag_eliminar].Monto1_Ingresado = double.Parse(jsonModel.Tx_Mtoarb[2].Text.Replace(".", ""));
                    this.InitObject.DealsIngParaProces[this.InitObject.Flag_eliminar].Monto2_Ingresado = double.Parse(jsonModel.Tx_Mtoarb[3].Text.Replace(".", ""));
                    this.InitObject.Flag_eliminar = -1;
                    this.InitObject.SelUdp = null;
                }
                else
                {
                    this.InitObject.Flag_eliminar = -1;
                    int tipo = -1;
                    int pos = -1;
                    this.InitObject.PagOri = PaginaOrigen.ARBITRAJES.ToString();
                    var rutclie = this.InitObject.MODGCVD.VgCvd.rutcli;
                    if (this.InitObject.DealActualSel != null)
                    {
                        var MonPaiCompra = McambioServ.GetParamComexMonedaPais(jsonModel.Cb_Moneda_Compra.Value.ToString(), "CodMoneda");
                        var Mon1 = "";
                        var MonPaiVenta = McambioServ.GetParamComexMonedaPais(jsonModel.Cb_Moneda_Venta.Value.ToString(), "CodMoneda");
                        var Mon2 = "";
                        if (this.InitObject.datSPMcambioCDD.Count() != 0)
                        {
                            Mon1 = this.InitObject.datSPMcambioCDD[((int)this.InitObject.DealActualSel - 1)].moneda1.Trim();
                            Mon2 = this.InitObject.datSPMcambioCDD[((int)this.InitObject.DealActualSel - 1)].moneda2.Trim();
                        }
                        if (Mon1 != MonPaiCompra.SiglaMoneda || Mon2 != MonPaiVenta.SiglaMoneda)
                        {
                            this.InitObject.DealActualSel = null;
                        }
                    }
                    if (this.InitObject.DealActualSel != null)
                    {
                        pos = (int)this.InitObject.DealActualSel;
                        pos--;
                    }
                    if (this.InitObject.DealsIngParaProces == null)
                    {
                        List<DealsIngresadosParaProcesar> IniDealsIngParaProces = new List<DealsIngresadosParaProcesar>();
                        this.InitObject.DealsIngParaProces = IniDealsIngParaProces;
                    }
                    tipo = McambioServ.COMEX_DatosIngresados(this.InitObject, this.InitObject.datSPMcambioCDD, this.InitObject.PagOri, pos, this.InitObject.DealsIngParaProces);
                    if (tipo != -1)
                    {
                        this.InitObject.DealManual = tipo;
                    }
                }
            }
            return Json(this.InitObject.Frm_Arbitrajes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ARBITRAJES_ok_2_Click()
        {
            return Rutear(() =>
            {
                this.InitObject.FormularioQueAbrir = "Arbitrajes";
                ftService.ARBITRAJES_ok_2_Click(this.InitObject);
            }, null);
        }

        [HttpPost]
        public JsonResult ARBITRAJES_ok_2_Response()
        {
            ftService.ARBITRAJES_ok_2_Click(this.InitObject);
            if (this.InitObject.DealActualSel == null)
            {
                this.InitObject.Frm_Arbitrajes.Errors.Add(new UI_Message()
                {
                    Type = TipoMensaje.Informacion,
                    Text = "Se agrego operación con tipo de cambio pizarra.",
                    Title = "Aviso!:   "
                });
            }
            return Json(this.InitObject.Frm_Arbitrajes);
        }

        //esto es a donde redirige cuando JS se da cuenta que tiene que llamar a IngresoValores
        public ActionResult ARBITRAJES_LlamarIngresoValores()
        {
            return Rutear(() =>
            {
                this.InitObject.Frm_Ingreso_Valores = new UI_Frm_Ingreso_Valores();
                this.InitObject.Frm_Ingreso_Valores.VieneDe = "ARBITRAJES_ok_2_Click";
                this.InitObject.Frm_Ingreso_Valores.MensageCancelacion = "Como no ha ingresado los valores de Tipo de Cambio y Paridad, no se puede efectuar el Arbitraje.";
                this.InitObject.VieneDe = "ARBITRAJES_ok_2_Click";
            }, null);
        }
        [HttpPost]
        public JsonResult ARBITRAJES_no_Click(UI_Frm_Arbitrajes jsonModel)
        {
            this.InitObject.Frm_Arbitrajes = jsonModel;
            ftService.ARBITRAJES_no_click(this.InitObject);
            if (this.InitObject.DealsIngParaProces != null)
            {
                if (this.InitObject.Flag_eliminar != -1)
                {
                    this.InitObject.DealsIngParaProces.RemoveAt(this.InitObject.Flag_eliminar);
                    this.InitObject.Flag_eliminar = -1;
                }
                if (this.InitObject.Frm_Arbitrajes.Lt_Operacion.ListCount == 0)
                {
                    this.InitObject.DealsIngParaProces = null;
                    this.InitObject.DealPrevSel = null;
                    this.InitObject.DealActualSel = null;
                    this.InitObject.DealManual = null;
                }
            }
            IniciarVariables();
            return Json(this.InitObject.Frm_Arbitrajes);
        }
        [HttpPost]
        public JsonResult ARBITRAJES_Lt_Operacion_Click(UI_Frm_Arbitrajes jsonModel)
        {
            if (this.InitObject.DealsIngParaProces != null)
            {
                this.InitObject.Flag_eliminar = jsonModel.Lt_Operacion.ListIndex;
            }
            this.InitObject.Frm_Arbitrajes = jsonModel;
            ftService.ARBITRAJES_lt_operacion_Click(this.InitObject);
            return Json(this.InitObject.Frm_Arbitrajes);
        }
        [HttpPost]
        public JsonResult ARBITRAJES_Tx_Mtoarb_Blur(short id, UI_Frm_Arbitrajes jsonModel)
        {
            this.InitObject.Frm_Arbitrajes = jsonModel;
            McambioService McambioServ = new McambioService();
            this.InitObject.PagOri = PaginaOrigen.ARBITRAJES.ToString();
            if (this.InitObject.DealActualSel == null && this.InitObject.DealsIngParaProces == null && (id == 1 || id == 2 || id == 3))
            {
                McambioServ.ARBITRAJE_ConsPrec2(this.InitObject);
            }
            else
            {
                if (this.InitObject.DealsIngParaProces != null)
                {
                    if (this.InitObject.DealsIngParaProces.Count > 0)
                    {
                        if (this.InitObject.DealsIngParaProces[0].TipoIngreso == "P" && (id == 2 || id == 3))
                        {
                            McambioServ.MsgsValidarIngreso(InitObject, 2, this.InitObject.PagOri, "");
                        }
                        else if (this.InitObject.DealsIngParaProces[0].TipoIngreso == "S" && this.InitObject.DealActualSel == null && (id == 2 || id == 3))
                        {
                            McambioServ.MsgsValidarIngreso(InitObject, 3, this.InitObject.PagOri, "");
                        }
                    }
                }
            }
            ftService.ARBITRAJES_tx_mtoarb_keypress(this.InitObject, id);
            ftService.ARBITRAJES_tx_mtoarb_lostfocus(this.InitObject, id);
            return Json(this.InitObject.Frm_Arbitrajes);
        }
        [HttpPost]
        public JsonResult ARBITRAJES_co_boton_Click(short boton_op, bool mensaje_op, bool acepta_op, UI_Frm_Arbitrajes jsonModel)
        {
            this.InitObject.Frm_Arbitrajes = jsonModel;
            bool termino = ftService.ARBITRAJES_co_boton_Click(this.InitObject, boton_op, mensaje_op, acepta_op);
            return Json(this.InitObject.Frm_Arbitrajes);
        }

        public ActionResult ARBITRAJE_Finalizar()
        {
            return Rutear(() =>
            {
                ftService.OpcionBotones(this.InitObject, 1);
            }, null);
        }
        #endregion

        #region COMERCIO VISIBLE EXPORTACIONES


        public ActionResult ComercioVisibleExport()
        {
            return Rutear(() =>
            {
                this.InitObject.FormularioQueAbrir = null;
                this.InitObject.Frm_VisE = null;
                this.InitObject.FrmxPln0 = null;

                ftService.OpcionBotones(this.InitObject, 2);
                ftService.COMVISEXP_Form_Load(this.InitObject);
            },
            () =>
            {
                return View(GetComercioVisibleExportViewModel());
            });
        }

        public JsonResult COMVISEXP_Cb_Mnd_Blur(int id)
        {
            var initObject = this.InitObject;
            initObject.Frm_VisE.Messages.Clear();
            initObject.Frm_VisE.Cb_Mnd.ListIndex = id;
            ftService.COMVISEXP_Cb_Mnd_Blur(initObject);
            this.InitObject = initObject;
            var model = GetComercioVisibleExportViewModel();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult COMVISEXP_Tx_MtoVisE_Blur(short id, string text)
        {
            var initObject = this.InitObject;
            initObject.Frm_VisE.Messages.Clear();
            initObject.Frm_VisE.Tx_MtoVisE[id].Text = text;
            ftService.COMVISEXP_Tx_MtoVisE_Blur(initObject, id);
            this.InitObject = initObject;
            var model = GetComercioVisibleExportViewModel();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult COMVISEXP_Co_Boton_Click(short id)
        {
            this.InitObject.Frm_VisE.Messages.Clear();
            if (ftService.COMVISEXP_Co_Boton_Click(this.InitObject, id))
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var model = GetComercioVisibleExportViewModel();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult COMVISEXP_Finish()
        {
            this.InitObject.Frm_VisE.Messages.Clear();
            return Rutear(() =>
            {
                ftService.OpcionBotones(this.InitObject, 2);
            },
            () =>
            {
                return View(GetComercioInvisibleViewModel());
            });
        }

        public ActionResult PlanillaVisibleExport()
        {
            return Rutear(() =>
            {
                ftService.PlanillaVisibleExportInit(this.InitObject);
            },
            () =>
            {
                PlanillaVisibleExportViewModel pvevm = new PlanillaVisibleExportViewModel(this.InitObject.FrmxPln0,
                    InitObject.Mdi_Principal.MESSAGES);

                return View(pvevm);
            });
        }

        [HttpPost]
        public ActionResult PlanillaVisibleExport(PlanillaVisibleExportViewModel pvevm, string btnCommand)
        {
            return Rutear(() =>
            {
                pvevm.Update(this.InitObject.FrmxPln0);

                InitObject.FormularioQueAbrir = null;
                if (btnCommand == "Aceptar")
                {
                    ftService.PlanillaVisibleExport_Boton_Click(InitObject, 0);
                }
                else if (btnCommand == "Visualizar")
                {
                    ftService.PlanillaVisibleExport_Boton_Click(InitObject, 1);
                }
                else if (btnCommand == "Cancelar")
                {
                    ftService.PlanillaVisibleExport_Boton_Click(InitObject, 2);
                }
            },
            () =>
            {
                pvevm = new PlanillaVisibleExportViewModel(InitObject.FrmxPln0, InitObject.Mdi_Principal.MESSAGES);
                return View(pvevm);
            });
        }

        public ActionResult PLANVIS_Finish()
        {
            return Rutear(() =>
            {
                ftService.PlanillaVisibleExport_Boton_Click(InitObject, 0);
            },
            () =>
            {
                var pvevm = new PlanillaVisibleExportViewModel(InitObject.FrmxPln0, InitObject.Mdi_Principal.MESSAGES);
                return View(pvevm);
            });
        }

        [HttpPost]
        public ActionResult PlanillaVisibleExport_OK_Dec_Click(PlanillaVisibleExportViewModel pvevm)
        {
            return Rutear(() =>
            {
                this.InitObject.Mdi_Principal.MESSAGES.Clear();
                pvevm.Update(this.InitObject.FrmxPln0);
                ftService.PlanillaVisibleExport_OK_Dec_Click(InitObject);
            },
            () =>
            {
                var model = new PlanillaVisibleExportViewModel(InitObject.FrmxPln0, InitObject.Mdi_Principal.MESSAGES);
                return Json(model, JsonRequestBehavior.AllowGet);
            });

        }

        [HttpPost]
        public ActionResult PlanillaVisibleExport_Tx_NumDec_LostFocus(string numDec, string fechaDec, string adn)
        {
            return Rutear(() =>
            {
                InitObject.FrmxPln0.Tx_NumDec.Text = numDec;
                InitObject.FrmxPln0.Tx_FecDec.Text = fechaDec;
                InitObject.FrmxPln0.Tx_CodAdn.Text = adn;

                ftService.PlanillaVisibleExport_Tx_NumDec_LostFocus(InitObject);
            },
            () =>
            {
                var model = new PlanillaVisibleExportViewModel(InitObject.FrmxPln0, InitObject.Mdi_Principal.MESSAGES);
                return Json(model, JsonRequestBehavior.AllowGet);
            });

        }

        [HttpPost]
        public ActionResult PlanillaVisibleExport_OK_Click(PlanillaVisibleExportViewModel pvevm)
        {
            return Rutear(() =>
            {
                pvevm.Update(this.InitObject.FrmxPln0);
                ftService.PlanillaVisibleExport_OK_Click(InitObject);
            },
            () =>
            {
                var model = new PlanillaVisibleExportViewModel(InitObject.FrmxPln0, InitObject.Mdi_Principal.MESSAGES);
                return Json(model, JsonRequestBehavior.AllowGet);
            });
        }

        [HttpPost]
        public ActionResult PlanillaVisibleExport_NO_Click(PlanillaVisibleExportViewModel pvevm)
        {
            return Rutear(() =>
            {
                pvevm.Update(this.InitObject.FrmxPln0);
                ftService.PlanillaVisibleExport_NO_Click(InitObject);
            },
            () =>
            {
                var model = new PlanillaVisibleExportViewModel(InitObject.FrmxPln0, InitObject.Mdi_Principal.MESSAGES);
                return Json(model, JsonRequestBehavior.AllowGet);
            });
        }

        [HttpPost]
        public ActionResult PlanillaVisibleExport_LtMto_Click(int selectedValue)
        {
            return Rutear(() =>
            {
                this.InitObject.Mdi_Principal.MESSAGES.Clear();
                InitObject.FrmxPln0.LtMto.ListIndex = InitObject.FrmxPln0.LtMto.Items.FindIndex(x => x.Data == selectedValue);
                ftService.PlanillaVisibleExport_LtMto_Click(InitObject);
            },
            () =>
            {
                var model = new PlanillaVisibleExportViewModel(InitObject.FrmxPln0, InitObject.Mdi_Principal.MESSAGES);
                return Json(model, JsonRequestBehavior.AllowGet);
            });
        }

        [HttpPost]
        public ActionResult PlanillaVisibleExport_Ch_Deduc_Click(bool valChecked, short indice)
        {
            return Rutear(() =>
            {
                this.InitObject.Mdi_Principal.MESSAGES.Clear();
                InitObject.FrmxPln0.Ch_Deduc[indice].Checked = valChecked;
                ftService.PlanillaVisibleExport_Ch_Deduc_Click(InitObject, indice);
            },
            () =>
            {
                var model = new PlanillaVisibleExportViewModel(InitObject.FrmxPln0, InitObject.Mdi_Principal.MESSAGES);
                return Json(model, JsonRequestBehavior.AllowGet);
            });
        }

        [HttpPost]
        public ActionResult PlanillaVisibleExport_LtPln_Click(int selectedValue)
        {
            return Rutear(() =>
            {
                this.InitObject.Mdi_Principal.MESSAGES.Clear();
                InitObject.FrmxPln0.LtPln.ListIndex = InitObject.FrmxPln0.LtPln.Items.FindIndex(x => x.Data == selectedValue);
                ftService.PlanillaVisibleExport_LtPln_Click(InitObject);
            },
            () =>
            {
                var model = new PlanillaVisibleExportViewModel(InitObject.FrmxPln0, InitObject.Mdi_Principal.MESSAGES);
                return Json(model, JsonRequestBehavior.AllowGet);
            });
        }

        [HttpPost]
        public ActionResult PlanillaVisibleExport_LtTPln_Click(int selectedValue)
        {
            return Rutear(() =>
            {
                this.InitObject.Mdi_Principal.MESSAGES.Clear();
                InitObject.FrmxPln0.LtTPln.ListIndex = InitObject.FrmxPln0.LtTPln.Items.FindIndex(x => x.Data == selectedValue);
                ftService.PlanillaVisibleExport_LtTPln_Click(InitObject);
            },
            () =>
            {
                var model = new PlanillaVisibleExportViewModel(InitObject.FrmxPln0, InitObject.Mdi_Principal.MESSAGES);
                return Json(model, JsonRequestBehavior.AllowGet);
            });
        }

        [HttpPost]
        public ActionResult PlanillaVisibleExport_Tx_MtoDec_LostFocus(string texto, short indice)
        {
            return Rutear(() =>
            {
                this.InitObject.Mdi_Principal.MESSAGES.Clear();
                InitObject.FrmxPln0.Tx_MtoDec[indice].Text = texto;
                ftService.PlanillaVisibleExport_Tx_MtoDec_LostFocus(InitObject, indice);
            },
            () =>
            {
                var model = new PlanillaVisibleExportViewModel(InitObject.FrmxPln0, InitObject.Mdi_Principal.MESSAGES);
                return Json(model, JsonRequestBehavior.AllowGet);
            });
        }

        [HttpPost]
        public ActionResult PlanillaVisibleExport_LtTPln_LostFocus()
        {
            return Rutear(() =>
            {
                this.InitObject.Mdi_Principal.MESSAGES.Clear();
                ftService.PlanillaVisibleExport_LtTPln_LostFocus(InitObject);
            },
            () =>
            {
                var model = new PlanillaVisibleExportViewModel(InitObject.FrmxPln0, InitObject.Mdi_Principal.MESSAGES);
                return Json(model, JsonRequestBehavior.AllowGet);
            });
        }

        #endregion

        #region EVENTOS ORIGENES DE FONDOS

        [HttpPost]
        public JsonResult ORIFOND_BNem_Click(UI_Frm_Origen_Fondos jsonModel)
        {
            this.InitObject.Frm_Origen_Fondos = jsonModel;
            this.InitObject.Frm_Origen_Fondos.Confirms.Clear();
            this.InitObject.Frm_Origen_Fondos.Errors.Clear();
            ftService.ORIFOND_BNem_Click(this.InitObject);
            return Json(this.InitObject.Frm_Origen_Fondos);
        }
        [HttpPost]
        public JsonResult ORIFOND_Boton_Click(short id, UI_Frm_Origen_Fondos jsonModel)
        {
            this.InitObject.Frm_Origen_Fondos = jsonModel;
            this.InitObject.Frm_Origen_Fondos.Confirms.Clear();
            this.InitObject.Frm_Origen_Fondos.Errors.Clear();
            ftService.ORIFOND_Boton_Click(this.InitObject, id);
            return Json(jsonModel);
        }
        [HttpPost]
        public JsonResult ORIFOND_Bt_PlnTrn_Click(UI_Frm_Origen_Fondos jsonModel)
        {
            this.InitObject.Frm_Origen_Fondos = jsonModel;
            this.InitObject.Frm_Origen_Fondos.Confirms.Clear();
            this.InitObject.Frm_Origen_Fondos.Errors.Clear();
            ftService.ORIFOND_Bt_PlnTrn_Click(this.InitObject);
            return Json(this.InitObject.Frm_Origen_Fondos);
        }
        [HttpPost]
        public JsonResult ORIFOND_cmb_codtran_Blur(UI_Frm_Origen_Fondos jsonModel)
        {
            this.InitObject.Frm_Origen_Fondos = jsonModel;
            this.InitObject.Frm_Origen_Fondos.Confirms.Clear();
            this.InitObject.Frm_Origen_Fondos.Errors.Clear();
            ftService.ORIFOND_cmb_codtran_Click(this.InitObject);
            return Json(this.InitObject.Frm_Origen_Fondos);
        }
        [HttpPost]
        public JsonResult ORIFOND_L_Cta_Blur(UI_Frm_Origen_Fondos jsonModel)
        {
            this.InitObject.Frm_Origen_Fondos = jsonModel;
            this.InitObject.Frm_Origen_Fondos.Confirms.Clear();
            this.InitObject.Frm_Origen_Fondos.Errors.Clear();
            ftService.ORIFOND_L_Cta_Click(this.InitObject);
            return Json(this.InitObject.Frm_Origen_Fondos);
        }
        [HttpPost]
        public JsonResult ORIFOND_L_Cuentas_Blur(UI_Frm_Origen_Fondos jsonModel)
        {
            this.InitObject.Frm_Origen_Fondos = jsonModel;
            this.InitObject.Frm_Origen_Fondos.Confirms.Clear();
            this.InitObject.Frm_Origen_Fondos.Errors.Clear();
            ftService.ORIFOND_L_Cuentas_Click(this.InitObject);
            return Json(this.InitObject.Frm_Origen_Fondos);
        }
        [HttpPost]
        public JsonResult ORIFOND_l_mnd_Blur(UI_Frm_Origen_Fondos jsonModel)
        {
            this.InitObject.Frm_Origen_Fondos = jsonModel;
            this.InitObject.Frm_Origen_Fondos.Confirms.Clear();
            this.InitObject.Frm_Origen_Fondos.Errors.Clear();
            ftService.ORIFOND_l_mnd_Click(this.InitObject);
            return Json(this.InitObject.Frm_Origen_Fondos);
        }
        [HttpPost]
        public JsonResult ORIFOND_l_mto_Blur(UI_Frm_Origen_Fondos jsonModel)
        {
            this.InitObject.Frm_Origen_Fondos = jsonModel;
            this.InitObject.Frm_Origen_Fondos.Confirms.Clear();
            this.InitObject.Frm_Origen_Fondos.Errors.Clear();

            ftService.ORIFOND_l_mto_Click(this.InitObject);
            return Json(this.InitObject.Frm_Origen_Fondos);
        }
        [HttpPost]
        public JsonResult ORIFOND_l_ori_Blur(UI_Frm_Origen_Fondos jsonModel)
        {
            this.InitObject.Frm_Origen_Fondos = jsonModel;
            this.InitObject.Frm_Origen_Fondos.Confirms.Clear();
            this.InitObject.Frm_Origen_Fondos.Errors.Clear();
            ftService.ORIFOND_l_ori_Click(this.InitObject);
            return Json(this.InitObject.Frm_Origen_Fondos);
        }
        [HttpPost]
        public JsonResult ORIFOND_L_Partys_Blur(UI_Frm_Origen_Fondos jsonModel)
        {
            this.InitObject.Frm_Origen_Fondos = jsonModel;
            this.InitObject.Frm_Origen_Fondos.Confirms.Clear();
            this.InitObject.Frm_Origen_Fondos.Errors.Clear();
            ftService.ORIFOND_L_Partys_Click(this.InitObject);
            return Json(this.InitObject.Frm_Origen_Fondos);
        }
        [HttpPost]
        public JsonResult ORIFOND_NO_Click(bool? pasoPor, bool? acepta, UI_Frm_Origen_Fondos jsonModel)
        {
            this.InitObject.Frm_Origen_Fondos = jsonModel;
            this.InitObject.Frm_Origen_Fondos.Confirms.Clear();
            this.InitObject.Frm_Origen_Fondos.Errors.Clear();
            ftService.ORIFOND_NO_Click(this.InitObject, pasoPor.HasValue ? pasoPor.Value : false, acepta.HasValue ? acepta.Value : false);
            return Json(this.InitObject.Frm_Origen_Fondos);
        }
        [HttpPost]
        public JsonResult ORIFOND_Tx_Datos_Blur(short index, UI_Frm_Origen_Fondos jsonModel)
        {
            this.InitObject.Frm_Origen_Fondos = jsonModel;
            this.InitObject.Frm_Origen_Fondos.Confirms.Clear();
            this.InitObject.Frm_Origen_Fondos.Errors.Clear();
            var preValue = this.InitObject.Frm_Origen_Fondos.L_Cta.Value ?? 0;
            ftService.ORIFOND_Tx_Datos_KeyPress(this.InitObject, index);
            ftService.ORIFOND_Tx_Datos_LostFocus(this.InitObject, index, preValue);
            return Json(this.InitObject.Frm_Origen_Fondos);
        }
        [HttpPost]
        public JsonResult ORIFOND_ok_Click(bool? pasoPor, bool? acepta, UI_Frm_Origen_Fondos jsonModel)
        {
            this.InitObject.Frm_Origen_Fondos = jsonModel;
            ftService.ORIFOND_ok_Click(this.InitObject, pasoPor.HasValue ? pasoPor.Value : false, acepta.HasValue ? acepta.Value : false);
            return Json(this.InitObject.Frm_Origen_Fondos);
        }

        [HttpPost]
        public JsonResult ORIFOND_ok_Click_Final(UI_Frm_Origen_Fondos jsonModel)
        {
            this.InitObject.Frm_Origen_Fondos = jsonModel;
            ftService.ORIFOND_ok_Click_Final(this.InitObject);
            return Json(this.InitObject.Frm_Origen_Fondos);
        }

        public ActionResult ORIFOND_Aceptar()
        {
            return Rutear(() =>
            {
                ftService.ORIFOND_Aceptar(this.InitObject);
            }, null);
        }
        #endregion

        #region IMPRESION DE DOCUMENTOS

        public ActionResult ImpresionDeDocumentos()
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
            ImpresionDocumentosViewModel model = new ImpresionDocumentosViewModel();
            ObtenerNroDeOperacionPorDefecto(model);
            return View(model);
        }

        [HandleAjaxException]
        public ActionResult BuscarDocumentosOperaciones(DateTime? fechaOperacion, string codcct, string codpro, string codesp, string codofi, string codope, string contactReference)
        {
            FundTransferService service = new FundTransferService();
            IList<DocumentoOperacion> documentos = null;
            if (fechaOperacion.HasValue)
            {
                int centroCosto = int.Parse(this.InitObject.MODGUSR.UsrEsp.CostoSuper);
                string idUsuario = this.InitObject.MODGUSR.UsrEsp.Especialista;

                documentos = service.BuscarDocumentosOperacionesPorFecha(fechaOperacion.Value, centroCosto.ToString(), idUsuario);
            }
            else if (!String.IsNullOrEmpty(codcct) && !String.IsNullOrEmpty(codpro) && !String.IsNullOrEmpty(codesp) && !String.IsNullOrEmpty(codofi) && !String.IsNullOrEmpty(codope))
            {
                documentos = service.BuscarDocumentosOperacionesPorNroOperacion(codcct, codpro, codesp, codofi, codope);
            }
            else if (!String.IsNullOrEmpty(contactReference))
            {
                documentos = service.BuscarDocumentosOperacionesPorContactReference(contactReference);
            }
            else
            {
                throw new ArgumentNullException("Debe seleccionar un filtro");
            }

            LimpiarCacheImpresionDeDocumentos(); //si busco de nuevo puede ser que se haya ido a otra pagina por ej a anular un doc, asi q limpio el cache

            if (documentos != null)
            {
                var jsonResult = new JsonResult()
                {
                    Data = documentos,
                    MaxJsonLength = Int32.MaxValue,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                return jsonResult;
            }
            else return null;
        }

        private void LimpiarCacheImpresionDeDocumentos()
        {
            if (System.Web.HttpContext.Current.Session[SessionKeys.FundTransfer.ImpresionDocumentosDetalleUltimoSwiftKey] != null)
            {
                System.Web.HttpContext.Current.Session.Remove(SessionKeys.FundTransfer.ImpresionDocumentosDetalleUltimoSwiftKey);
            }

            if (System.Web.HttpContext.Current.Session[SessionKeys.FundTransfer.ImpresionDocumentosReporteContableKey] != null)
            {
                System.Web.HttpContext.Current.Session.Remove(SessionKeys.FundTransfer.ImpresionDocumentosReporteContableKey);
            }
        }

        public ActionResult GetDetalleSwift(string nroMensaje, int nroReporte, DateTime fechaOp, bool? generarHtmlCompleto, bool? imprimir, PrintFormat? format = null)
        {
            //sce_mch_s01_MS_Result cabecera = ftService.GetCabeceraReporteDetalleSwift(InitObject, nroReporte, fechaOp);
            //if (cabecera != null)
            //{
            //    ViewBag.Estado = cabecera.estado;
            //}
            //ViewBag.DetalleSwift = GetMensajeSwiftDeCacheOBL(nroMensaje);
            //ViewBag.GenerarHtmlCompleto = (generarHtmlCompleto.HasValue && generarHtmlCompleto.Value);
            //ViewBag.Imprimir = (imprimir.HasValue && imprimir.Value);

            //return this.AlternateOutput(format, "DetalleSwift", null)
            //    ?? View("DetalleSwift");
            string url = "~/Impresion/ImpresionDeDocumentos/GetDetalleSwift" + (Request.Url.Query ?? string.Empty);
            return new TransferResult(url);
            //return RedirectToAction("GetDetalleSwift", "ImpresionDeDocumentos", new { area = "Impresion", nroMensaje, nroReporte, fechaOp, generarHtmlCompleto, imprimir, format });
        }

        public ActionResult DetalleSwiftOriginalCopia(string nroMensaje, string replace, string with, PrintFormat? format = null)
        {
            //string detSwift = GetMensajeSwiftDeCacheOBL(nroMensaje);
            //detSwift = detSwift.Replace(replace, with);
            //ViewBag.DetalleSwift = detSwift;
            //ViewBag.GenerarHtmlCompleto = true;
            //ViewBag.Imprimir = false;

            //return this.AlternateOutput(format, "DetalleSwift", null)
            //    ?? View("DetalleSwift");
            string url = "~/Impresion/ImpresionDeDocumentos/DetalleSwiftOriginalCopia" + (Request.Url.Query ?? string.Empty);
            return new TransferResult(url);
            //return RedirectToAction("DetalleSwiftOriginalCopia", "ImpresionDeDocumentos", new { area = "Impresion", nroMensaje, replace, with, format });
        }

        public ActionResult ReporteContable(int nroReporte, DateTime fechaOp, bool? generarHtmlCompleto, bool? imprimir, PrintFormat? format = null)
        {
            /*ReporteContableViewModel model = new ReporteContableViewModel();
            model.Reporte = GetReporteContableDeCacheOBL(nroReporte, fechaOp);
            model.GenerarHtmlCompleto = (generarHtmlCompleto.HasValue && generarHtmlCompleto.Value);
            model.Imprimir = (imprimir.HasValue && imprimir.Value);


            return this.AlternateOutput(format, "ReporteContable", model)
                ?? View(model);*/

            //ControllerContext context = null;
            //var controller = ControllerFactory.CreateController<BCH.Comex.UI.Web.Areas.Impresion.Controllers.ImpresionDeDocumentosController>(out context);

            //return controller.ReporteContable(nroReporte, fechaOp, generarHtmlCompleto, imprimir, format);
            string url = "~/Impresion/ImpresionDeDocumentos/ReporteContable" + (Request.Url.Query ?? string.Empty);
            return new TransferResult(url);
            //return RedirectToAction("ReporteContable", "ImpresionDeDocumentos", new { area = "Impresion", nroReporte, fechaOp, generarHtmlCompleto, imprimir, format });
        }

        private IList<sce_dfc> GetDescripcionDeFuncionesContablesDeCacheOBL()
        {
            IList<sce_dfc> result = null;
            if (System.Web.HttpContext.Current.Cache[SessionKeys.FundTransfer.DescripcionFuncionesContablesKey] == null)
            {
                result = ftService.GetDescripcionesFuncionesContables();
                System.Web.HttpContext.Current.Cache[SessionKeys.FundTransfer.DescripcionFuncionesContablesKey] = result;
            }
            else
            {
                result = System.Web.HttpContext.Current.Cache[SessionKeys.FundTransfer.DescripcionFuncionesContablesKey] as IList<sce_dfc>;
            }

            return result;
        }

        private ReporteContable GetReporteContableDeCacheOBL(int nroReporte, DateTime fechaOp)
        {
            //el reporte contable lo guardo en el cache ya que posiblemente lo puedo ver primero en el popup y luego al darle imprimir lo abro de vuelta
            //en una pagina limpia
            ReporteContable reporte = null;
            if (System.Web.HttpContext.Current.Session[SessionKeys.FundTransfer.ImpresionDocumentosReporteContableKey] != null)
            {
                reporte = System.Web.HttpContext.Current.Session[SessionKeys.FundTransfer.ImpresionDocumentosReporteContableKey] as ReporteContable;
                if (reporte != null && reporte.Cabecera.nrorpt == nroReporte)
                {
                    return reporte;
                }
            }

            IList<sce_dfc> descripciones = GetDescripcionDeFuncionesContablesDeCacheOBL();
            reporte = ftService.GetReporteContable(this.InitObject, nroReporte, fechaOp, descripciones);
            System.Web.HttpContext.Current.Session[SessionKeys.FundTransfer.ImpresionDocumentosReporteContableKey] = reporte;

            return reporte;
        }

        private string GetMensajeSwiftDeCacheOBL(string nroMensaje)
        {
            //el mensaje lo guardo en el cache ya que posiblemente lo puedo ver primero en el popup y luego al darle imprimir lo abro de vuelta
            //en una pagina limpia
            string swift = null;
            if (System.Web.HttpContext.Current.Session[SessionKeys.FundTransfer.ImpresionDocumentosDetalleUltimoSwiftKey] != null)
            {
                swift = System.Web.HttpContext.Current.Session[SessionKeys.FundTransfer.ImpresionDocumentosDetalleUltimoSwiftKey] as string;
                if (swift != null &&
                    (string)System.Web.HttpContext.Current.Session[SessionKeys.FundTransfer.ImpresionDocumentosNroMensajeUltimoSwiftKey] == nroMensaje)
                {
                    return swift;
                }
            }

            swift = ftService.GetDetalleMensajeSwift(nroMensaje);
            System.Web.HttpContext.Current.Session[SessionKeys.FundTransfer.ImpresionDocumentosDetalleUltimoSwiftKey] = swift;
            System.Web.HttpContext.Current.Session[SessionKeys.FundTransfer.ImpresionDocumentosNroMensajeUltimoSwiftKey] = nroMensaje;

            return swift;
        }

        private void ObtenerNroDeOperacionPorDefecto(ImpresionDocumentosViewModel model)
        {
            model.codcct = int.Parse(this.InitObject.MODGUSR.UsrEsp.CentroCosto);
            model.codpro = int.Parse(T_MODGUSR.IdPro_ComVen);
            model.codesp = int.Parse(this.InitObject.MODGUSR.UsrEsp.Especialista);
            model.codofi = "000";
            model.codope = "00000";
        }

        #endregion

        #region EVENTOS DESTINOS DE FONDOS
        public JsonResult DESTFOND_Form_Get()
        {
            this.InitObject.Frm_Destino_Fondos.Boton[0].Enabled = this.InitObject.Frm_Destino_Fondos.VuelveDeOtro != true ? true : false;
            return Json(this.InitObject.Frm_Destino_Fondos, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DESTFOND_Ok_Click(UI_Frm_Destino_Fondos jsonModel, bool vieneDe, bool res)
        {
            this.InitObject.Frm_Destino_Fondos = jsonModel;
            ftService.DESTFOND_OK_CLick(this.InitObject, vieneDe, res);
            return Json(this.InitObject.Frm_Destino_Fondos);
        }
        //public ActionResult DESTFOND_Aceptar()
        //{
        //    return Execute(() =>
        //    {
        //        this.InitObject.FormularioQueAbrir = "DESTFOND_Aceptar";
        //        ftService.DESTFOND_Aceptar(this.InitObject);
        //    }, "DESTFOND_Aceptar");
        //}

        [HttpPost]
        public JsonResult DESTFOND_L_Cta_Blur(UI_Frm_Destino_Fondos jsonModel)
        {
            this.InitObject.Frm_Destino_Fondos = jsonModel;
            ftService.DESTFOND_L_Cta_Blur(this.InitObject);
            return Json(this.InitObject.Frm_Destino_Fondos);
        }

        [HttpPost]
        public JsonResult DESTFOND_L_Mnd_Blur(UI_Frm_Destino_Fondos jsonModel)
        {
            this.InitObject.Frm_Destino_Fondos = jsonModel;
            ftService.DESTFOND_L_Mnd_Blur(this.InitObject);
            return Json(this.InitObject.Frm_Destino_Fondos);
        }

        [HttpPost]
        public JsonResult DESTFOND_L_Party_Blur(UI_Frm_Destino_Fondos jsonModel)
        {
            this.InitObject.Frm_Destino_Fondos = jsonModel;
            ftService.DESTFOND_L_Party_Blur(this.InitObject);
            return Json(this.InitObject.Frm_Destino_Fondos);
        }

        [HttpPost]
        public JsonResult DESTFOND_L_Cuentas_Blur(UI_Frm_Destino_Fondos jsonModel)
        {
            this.InitObject.Frm_Destino_Fondos = jsonModel;
            ftService.DESTFOND_L_Cuentas_Blur(this.InitObject);
            return Json(this.InitObject.Frm_Destino_Fondos);
        }

        //public ActionResult DESTFOND_BNem_Click()
        //{
        //    return Execute(() =>
        //    {
        //        ftService.DESTFOND_BNem_Click(this.InitObject);
        //    }, "DestinoFondos");
        //}
        [HttpPost]
        public JsonResult DESTFOND_BNem_Click(UI_Frm_Destino_Fondos jsonModel)
        {
            this.InitObject.Frm_Destino_Fondos = jsonModel;
            ftService.DESTFOND_BNem_Click(this.InitObject);
            return Json(this.InitObject.Frm_Destino_Fondos);
        }

        [HttpPost]
        public JsonResult DESTFOND_Boton_Click(UI_Frm_Destino_Fondos jsonModel, short index)
        {
            this.InitObject.Frm_Destino_Fondos = jsonModel;
            this.InitObject.Frm_Destino_Fondos.Confirms.Clear();
            this.InitObject.Frm_Destino_Fondos.Errors.Clear();
            ftService.DESTFOND_Boton_Click(this.InitObject, index);
            return Json(jsonModel);
        }
        [HttpPost]
        public JsonResult DESTFOND_Bt_PlnTrn_Click(UI_Frm_Destino_Fondos jsonModel)
        {
            this.InitObject.Frm_Destino_Fondos = jsonModel;
            this.InitObject.Frm_Destino_Fondos.Confirms.Clear();
            this.InitObject.Frm_Destino_Fondos.Errors.Clear();
            ftService.DESTFOND_Bt_PlnTrn_Click(this.InitObject);
            return Json(this.InitObject.Frm_Destino_Fondos);
        }
        [HttpPost]
        public JsonResult DESTFOND_Cb_Destino_Blur(UI_Frm_Destino_Fondos jsonModel)
        {
            this.InitObject.Frm_Destino_Fondos = jsonModel;
            ftService.DESTFOND_Cb_Destino_Click(this.InitObject);
            return Json(this.InitObject.Frm_Destino_Fondos);
        }
        [HttpPost]
        public JsonResult DESTFOND_cmb_codtran_Blur(UI_Frm_Destino_Fondos jsonModel)
        {
            this.InitObject.Frm_Destino_Fondos = jsonModel;
            ftService.DESTFOND_cmb_codtran_Click(this.InitObject);
            return Json(this.InitObject.Frm_Destino_Fondos);
        }

        [HttpPost]
        public JsonResult DESTFOND_l_mto_Click(UI_Frm_Destino_Fondos jsonModel)
        {
            this.InitObject.Frm_Destino_Fondos = jsonModel;
            ftService.DESTFOND_l_mto_Click(this.InitObject);
            return Json(this.InitObject.Frm_Destino_Fondos);
        }

        [HttpPost]
        public JsonResult DESTFOND_l_via_Click(UI_Frm_Destino_Fondos jsonModel)
        {
            this.InitObject.Frm_Destino_Fondos = jsonModel;
            this.InitObject.Frm_Destino_Fondos.Confirms.Clear();
            this.InitObject.Frm_Destino_Fondos.Errors.Clear();
            ftService.DESTFOND_l_via_Click(this.InitObject);
            return Json(this.InitObject.Frm_Destino_Fondos);
        }

        [HttpPost]
        public JsonResult DESTFOND_NO_Click(UI_Frm_Destino_Fondos jsonModel, bool? deMensaje, bool? resMensaje)
        {
            this.InitObject.Frm_Destino_Fondos = jsonModel;
            ftService.DESTFOND_NO_Click(this.InitObject, (!deMensaje.HasValue || deMensaje.Value), (!resMensaje.HasValue || resMensaje.Value));
            return Json(this.InitObject.Frm_Destino_Fondos);
        }

        [HttpPost]
        public JsonResult DESTFOND_Tx_Datos_Blur(UI_Frm_Destino_Fondos jsonModel, short index)
        {
            this.InitObject.Frm_Destino_Fondos = jsonModel;
            ftService.DESTFOND_Tx_Datos_Blur(this.InitObject, index);
            return Json(this.InitObject.Frm_Destino_Fondos);
        }

        [HttpPost]
        public JsonResult DESTFOND_txtNumRef_Blur(UI_Frm_Destino_Fondos jsonModel)
        {
            this.InitObject.Frm_Destino_Fondos = jsonModel;
            ftService.DESTFOND_txtNumRef_Blur(this.InitObject);
            return Json(this.InitObject.Frm_Destino_Fondos);
        }

        public ActionResult DESTFOND_Aceptar()
        {
            return Rutear(() => { ftService.DESTFOND_Aceptar(this.InitObject); }, null);
        }
        #endregion

        #region EVENTOS TICKET GRABAR
        public JsonResult TICKGR_Form_Load()
        {
            ftService.TICKGR_Form_Load(this.InitObject);
            return Json(this.InitObject.Frm_Ticket, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult TICKGR_otro_Change(UI_Frm_Ticket jsonModel)
        {
            this.InitObject.Frm_Ticket = jsonModel;
            ftService.TICKGR_Otro_Click(this.InitObject);
            return Json(this.InitObject.Frm_Ticket, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult TICKGR_Cb_Ticket_Change(UI_Frm_Ticket jsonModel)
        {
            this.InitObject.Frm_Ticket = jsonModel;
            ftService.TICKGR_Cb_Ticket_Click(this.InitObject);
            return Json(this.InitObject.Frm_Ticket);
        }
        [HttpPost]
        public JsonResult TICKGR_PostModel(UI_Frm_Ticket jsonModel)
        {
            this.InitObject.Frm_Ticket = jsonModel;
            return Json(jsonModel);
        }
        public ActionResult TICKGR_Aceptar()
        {
            return Rutear(() => { ftService.TICKGR_Aceptar_Click(this.InitObject); }, View);
        }
        public ActionResult TICKGR_Cancelar()
        {
            ftService.TICKGR_Cancelar_Click(this.InitObject);
            return new RedirectResult("~/Fundtransfer");
            //return Rutear(() => { ftService.TICKGR_Cancelar_Click(this.InitObject); }, View);
        }
        #endregion

        #region EVENTOS PLANILLA VISIBLE IMPORT
        private JsonResult PlanillaVisibleImportEvent(PlanillaVisibleImportViewModel viewModel, Action metodo)
        {
            using (var tracer = new Tracer("PlanillaVisibleImportEvent"))
            {
                try
                {
                    var initObject = this.InitObject;
                    SetPlanillaVisibleImportViewModel(viewModel);

                    metodo();

                    this.InitObject = initObject;
                    var model = GetPlanillaVisibleImportViewModel() ?? viewModel;
                    model.FormularioQueAbrir = this.InitObject.FormularioQueAbrir;
                    this.InitObject.Mdi_Principal.MESSAGES.Clear();
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta en PlvSO_LoadFrm: ", e);
                    viewModel.MensajesDeError = new List<string> { "Ha ocurrido un error." };
                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult PlvSO_LoadFrm()
        {
            using (var tracer = new Tracer("PlvSO_LoadFrm"))
            {
                try
                {
                    if (InitObject != null)
                    {
                        IniciarVariables();
                        this.InitObject.DealsIngParaProces = null;
                        ftService.PlvSO_LoadFrm(InitObject);
                        var model = GetPlanillaVisibleImportViewModel();
                        return Json(model, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new Exception("Se ha perdido la sesión");
                    }
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta en PlvSO_LoadFrm: ", ex);
                    throw;
                }
            }
        }

        public JsonResult PlvSO_Bot_OkDec_Click(PlanillaVisibleImportViewModel viewModel)
        {
            if (this.InitObject.DealActualSel == null)
            {
                viewModel.idCbPPago = -1;
            }
            string TipoCambio_en_pagina = double.Parse(string.IsNullOrEmpty(this.InitObject.Frm_PlvSO.Tx_TipCam.Text) ? "0" : this.InitObject.Frm_PlvSO.Tx_TipCam.Text).ToString();
            return PlanillaVisibleImportEvent(viewModel, () =>
            {
                ftService.PlvSO_Bot_OkDec_Click(this.InitObject);
                string TipoCambio_sistema = double.Parse(string.IsNullOrEmpty(this.InitObject.Frm_PlvSO.Tx_TipCam.Text) ? "0" : this.InitObject.Frm_PlvSO.Tx_TipCam.Text).ToString();
                if (TipoCambio_en_pagina != "" && TipoCambio_en_pagina != TipoCambio_sistema)
                {
                    this.InitObject.Frm_PlvSO.Tx_TipCam.Text = TipoCambio_en_pagina;
                    this.InitObject.Frm_PlvSO.Pn_TCDol.Text = TipoCambio_en_pagina;
                }
            });
        }

        public JsonResult PlvSO_Bot_OkFinal_Click(PlanillaVisibleImportViewModel viewModel)
        {
            string MtoFob = viewModel.Tx_MtoFob;
            string PnTotDol = viewModel.Pn_TotDol;
            int FlagTipCam = this.InitObject.Frm_PlvSO.Flag_TipCam;
            this.InitObject.Flag_transferencia = this.InitObject.Frm_PlvSO.Ch_Transf.Value;
            this.InitObject.Flag_eliminar = -1;
            return PlanillaVisibleImportEvent(viewModel, () => {
                if (this.InitObject.Flag_transferencia == 1)
                {
                    ftService.PlvSO_Bot_OkFinal_Click(this.InitObject);
                }
                else
                {
                    McambioService McambioServ = new McambioService();
                    this.InitObject.PagOri = PaginaOrigen.PlvSO.ToString();
                    int ValidarIng = McambioServ.ValidarIngreso(this.InitObject, this.InitObject.datSPMcambioCDD, this.InitObject.DealsIngParaProces, this.InitObject.PagOri, 0, this.InitObject.DealActualSel);
                    if (ValidarIng == 0)
                    {
                        ftService.PlvSO_Bot_OkFinal_Click(this.InitObject);
                        if (this.InitObject.Flag_transferencia == 0)
                        {
                            if (InitObject.Mdi_Principal.MESSAGES.Count() == 0 || (InitObject.Mdi_Principal.MESSAGES.Count() == 1 ))
                            {
                                int tipo = -1;
                                int pos = -1;
                                double TC_Ing = 0;
                                Nullable<int> temp;
                                if (this.InitObject.DealActualSel == null)
                                {
                                    temp = null;
                                }
                                else
                                {
                                    temp = (int)this.InitObject.DealActualSel;
                                }
                                string T = "";
                                if (this.InitObject.DealsIngParaProces != null)
                                {
                                    if (this.InitObject.DealsIngParaProces.Count > 0)
                                    {
                                        T = this.InitObject.DealsIngParaProces[0].TipoIngreso;
                                    }
                                }
                                TC_Ing = double.Parse(this.InitObject.Frm_PlvSO.Tx_TipCam.Text);
                                this.InitObject.PagOri = PaginaOrigen.PlvSO.ToString();
                                var rutclie = this.InitObject.MODGCVD.VgCvd.rutcli;
                                if (this.InitObject.DealActualSel != null && this.InitObject.DealsIngParaProces == null)
                                {
                                    pos = (int)this.InitObject.DealActualSel;
                                    pos--;
                                }
                                this.InitObject.Frm_PlvSO.Tx_MtoFob.Text = MtoFob;
                                this.InitObject.Frm_PlvSO.Pn_TotDol.Text = PnTotDol;
                                if (this.InitObject.DealsIngParaProces == null)
                                {
                                    List<DealsIngresadosParaProcesar> IniDealsIngParaProces = new List<DealsIngresadosParaProcesar>();
                                    this.InitObject.DealsIngParaProces = IniDealsIngParaProces;
                                }
                                tipo = McambioServ.COMEX_DatosIngresados(this.InitObject, this.InitObject.datSPMcambioCDD, this.InitObject.PagOri, pos, this.InitObject.DealsIngParaProces);
                                temp = null;
                                if (tipo != -1)
                                {
                                    this.InitObject.DealManual = tipo;
                                }
                            }
                            this.InitObject.DealActualSel = -1;
                        }
                    }
                    this.InitObject.Frm_PlvSO.Cb_Moneda.Enabled = true;
                    this.InitObject.Frm_PlvSO.Cb_PPago.Enabled = true;
                    this.InitObject.Frm_PlvSO.Tx_MtoFob.Enabled = true;
                    this.InitObject.Frm_PlvSO.Tx_TipCam.Enabled = true;
                }
            });
        }

        public JsonResult PlvSO_Cb_Moneda_Change(PlanillaVisibleImportViewModel viewModel)
        {
            if (this.InitObject.DealActualSel == null)
            {
                viewModel.idCbPPago = -1;
            }
            return PlanillaVisibleImportEvent(viewModel, () =>
            {
                ftService.PlvSO_Cb_Moneda_Click(this.InitObject);
            });
        }

        public JsonResult PlvSO_MtoFob_Blur(PlanillaVisibleImportViewModel viewModel)
        {
            return PlanillaVisibleImportEvent(viewModel, () =>
            {
                PlvSO_ConsPrec2(viewModel);
                ftService.PlvSO_MtoFob_Blur(this.InitObject);
            });
        }

        public JsonResult PlvSO_MtoFle_Blur(PlanillaVisibleImportViewModel viewModel)
        {
            return PlanillaVisibleImportEvent(viewModel, () =>
            {
                ftService.PlvSO_MtoFle_Blur(this.InitObject);
            });
        }

        public JsonResult PlvSO_MtoSeg_Blur(PlanillaVisibleImportViewModel viewModel)
        {
            return PlanillaVisibleImportEvent(viewModel, () =>
            {
                ftService.PlvSO_MtoSeg_Blur(this.InitObject);
            });
        }

        public JsonResult PlvSO_NroPre_Blur(PlanillaVisibleImportViewModel viewModel, string NroPresentacion)
        {
            return PlanillaVisibleImportEvent(viewModel, () =>
            {
                InitObject.Frm_PlvSO.Tx_NroPre.Text = NroPresentacion;
                ftService.PlvSO_NroPre_Blur(this.InitObject);
            });
        }

        public JsonResult PlvSO_TipCam_Blur(PlanillaVisibleImportViewModel viewModel)
        {
            return PlanillaVisibleImportEvent(viewModel, () =>
            {
            if (viewModel.MensajesDeError == null )
                {
                    ftService.PlvSO_TipCam_Blur(this.InitObject);
                }
            });
        }

        public JsonResult PlvSO_Paridad_Blur(PlanillaVisibleImportViewModel viewModel)
        {
            return PlanillaVisibleImportEvent(viewModel, () =>
            {
                ftService.PlvSO_Paridad_Blur(this.InitObject);
            });
        }

        public JsonResult PlvSO_Tx_CodPag_Blur(PlanillaVisibleImportViewModel viewModel, string CodPag)
        {
            return PlanillaVisibleImportEvent(viewModel, () =>
            {
                InitObject.Frm_PlvSO.Tx_CodPag.Text = CodPag;
                ftService.PlvSO_Tx_CodPag_Blur(this.InitObject);
            });
        }

        public JsonResult PlvSO_DocChi_Blur(PlanillaVisibleImportViewModel viewModel)
        {
            return PlanillaVisibleImportEvent(viewModel, () =>
            {
                ftService.PlvSO_DocChi_Blur(this.InitObject);
            });
        }

        public JsonResult PlvSO_Tx_FecRee_Blur(PlanillaVisibleImportViewModel viewModel, string Fecha, string NroPresentacion)
        {
            return PlanillaVisibleImportEvent(viewModel, () =>
            {
                InitObject.Frm_PlvSO.Tx_FecRee.Text = Fecha;
                InitObject.Frm_PlvSO.Tx_NroPre.Text = NroPresentacion;
                ftService.PlvSO_Tx_FecRee_Blur(this.InitObject);
            });
        }

        public JsonResult PlvSO_Tt_Final_Click(PlanillaVisibleImportViewModel viewModel)
        {
            return PlanillaVisibleImportEvent(viewModel, () =>
            {
                if (this.InitObject.DealsIngParaProces != null)
                {
                    this.InitObject.Flag_eliminar = this.InitObject.Frm_PlvSO.Lt_Final.ListIndex; 
                }
                ftService.PlvSO_Tt_Final_Click(this.InitObject);
            });
        }

        public JsonResult PlvSO_Bot_Acepta_Click(PlanillaVisibleImportViewModel viewModel)
        {
            return PlanillaVisibleImportEvent(viewModel, () =>
            {
                ftService.PlvSO_Bot_Acepta_Click(this.InitObject);
                //No subir hasta que se implemente.
                //ftService.Ventas_Vis_Import_PosShow(InitObject);
            });
        }

        public ActionResult PlvSO_Finish()
        {
            return Rutear(() => ftService.Ventas_Vis_Import_PosShow(InitObject), null);
        }
        public JsonResult PlvSO_Bot_NoFinal_Click(PlanillaVisibleImportViewModel viewModel)
        {
            return PlanillaVisibleImportEvent(viewModel, () =>
            {
                if (this.InitObject.Frm_PlvSO.Lt_Final.ListCount == 1 && this.InitObject.Frm_PlvSO.Lt_Final.ListIndex == -1)
                {
                    this.InitObject.Frm_PlvSO.Lt_Final.ListIndex = 0;
                }
                ftService.PlvSO_Bot_NoFinal_Click(this.InitObject);
                if (this.InitObject.DealsIngParaProces != null)
                {
                    if (this.InitObject.Flag_eliminar != -1 && this.InitObject.Frm_PlvSO.Lt_Final.ListIndex != -1)
                    {
                        this.InitObject.DealsIngParaProces.RemoveAt(this.InitObject.Flag_eliminar);
                        this.InitObject.Flag_eliminar = -1;
                    }
                    if (this.InitObject.Frm_PlvSO.Lt_Final.ListCount == 0)
                    {
                        this.InitObject.DealsIngParaProces = null;
                        this.InitObject.DealPrevSel = null;
                        this.InitObject.DealActualSel = null;
                        this.InitObject.DealManual = null;
                        this.InitObject.Frm_PlvSO.Lt_Final.ListIndex = -1;
                    }
                }
            });
        }
        public ActionResult PlvSO_Bot_Cancel_Click(PlanillaVisibleImportViewModel viewModel)
        {
            return Rutear(() => { ftService.PlvSO_Bot_Cancel_Click(this.InitObject); }, null);
        }


        public JsonResult PlvSO_Ch_Transf_Click(PlanillaVisibleImportViewModel viewModel)
        {

            return PlanillaVisibleImportEvent(viewModel, () =>
            {
                ftService.PlvSO_Ch_Transf_Click(this.InitObject);
            });
        }

        public JsonResult PlvSO_Ch_PlanRee_Click(PlanillaVisibleImportViewModel viewModel)
        {
            return PlanillaVisibleImportEvent(viewModel, () =>
            {
                ftService.PlvSO_Ch_PlanRee_Click(this.InitObject);
            });
        }


        #endregion

        #region "Planilla Anulacion"

        public ActionResult PlanillaAnulacion()
        {
            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
            this.InitObject.FormularioQueAbrir = null;

            if (this.InitObject.Frmxanu == null)
                this.InitObject.Frmxanu = new UI_FrmxAnu();

            ftService.PlanillaAnulacionInit(this.InitObject);

            PlanillaAnulacionViewModel pa = new PlanillaAnulacionViewModel(this.InitObject.Frmxanu, this.InitObject);
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            return View(pa);
        }

        [HttpPost]
        public ActionResult PlanillaAnulacion(PlanillaAnulacionViewModel pa, string Command)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
            ModelState.Clear();
            pa.Update(InitObject.Frmxanu);
            if (Command == "<<")
            {
                ftService.PlanillaAnulacion_Retroceder(this.InitObject);
            }
            else if (Command == ">>")
            {
                ftService.PlanillaAnulacion_Avanzar(this.InitObject);
            }
            else if (Command == "√")
            {
                ftService.PlanillaAnulacion_Tickear(this.InitObject);
            }
            else if (Command == "Aceptar")
            {
                ftService.PlanillaAnulacion_Aceptar(this.InitObject);
                if (this.InitObject.MODXANU.VxAnus[0].Acepto == -1)
                {
                    return new RedirectResult("~/Fundtransfer");
                }
            }
            else if (Command == "Cancelar")
            {
                ftService.PlanillaAnulacion_Cancelar(this.InitObject);
                return new RedirectResult("~/Fundtransfer");
            }

            pa = new PlanillaAnulacionViewModel(this.InitObject.Frmxanu, this.InitObject);
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            return View(pa);
        }

        public ActionResult PlanillaAnulacion_TipoAnulacion_Blur(string selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.Frmxanu.Tx_PlAnu[2].Text = selectedValue;
            ftService.PlanillaAnulada_Blur(this.InitObject, 2);
            PlanillaAnulacionViewModel pavm = new PlanillaAnulacionViewModel(this.InitObject.Frmxanu, InitObject);
            return Json(pavm);
        }

        public ActionResult PlanillaAnulacion_CodigoDos_Blur(string selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.Frmxanu.Tx_PlAnu[4].Text = selectedValue;
            ftService.PlanillaAnulada_Blur(this.InitObject, 4);
            PlanillaAnulacionViewModel pavm = new PlanillaAnulacionViewModel(this.InitObject.Frmxanu, this.InitObject);
            return Json(pavm);
        }

        public ActionResult PlanillaAnulacion_RUT_Blur(string selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.Frmxanu.Tx_PlAnu[7].Text = selectedValue;
            ftService.PlanillaAnulada_Blur(this.InitObject, 7);
            PlanillaAnulacionViewModel pavm = new PlanillaAnulacionViewModel(this.InitObject.Frmxanu, this.InitObject);
            return Json(pavm);
        }

        public ActionResult PlanillaAnulacion_EntidadAutorizadaPlanillaAnulada_Blur(string selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.Frmxanu.Tx_PlAnu[9].Text = selectedValue;
            ftService.PlanillaAnulada_Blur(this.InitObject, 9);
            PlanillaAnulacionViewModel pavm = new PlanillaAnulacionViewModel(this.InitObject.Frmxanu, this.InitObject);
            return Json(pavm);
        }

        public ActionResult PlanillaAnulacion_TipoOperacionAnulada_Blur(string selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.Frmxanu.Tx_PlAnu[13].Text = selectedValue;
            ftService.PlanillaAnulada_Blur(this.InitObject, 13);
            PlanillaAnulacionViewModel pavm = new PlanillaAnulacionViewModel(this.InitObject.Frmxanu, this.InitObject);
            return Json(pavm);
        }

        public ActionResult PlanillaAnulacion_PlazaBancoCentralAnulada_Blur(string selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.Frmxanu.Tx_PlAnu[15].Text = selectedValue;
            ftService.PlanillaAnulada_Blur(this.InitObject, 15);
            PlanillaAnulacionViewModel pavm = new PlanillaAnulacionViewModel(this.InitObject.Frmxanu, this.InitObject);
            return Json(pavm);
        }

        public ActionResult PlanillaAnulacion_ParidadUssDatosPlanillaAnulada_Blur(string selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.Frmxanu.Tx_PlAnu[17].Text = selectedValue;
            ftService.PlanillaAnulada_Blur(this.InitObject, 17);
            PlanillaAnulacionViewModel pavm = new PlanillaAnulacionViewModel(this.InitObject.Frmxanu, this.InitObject);
            return Json(pavm);
        }


        public ActionResult PlanillaAnulacion_AduanaExportacion_Blur(string selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.Frmxanu.Tx_PlAnu[19].Text = selectedValue;
            ftService.PlanillaAnulada_Blur(this.InitObject, 19);
            PlanillaAnulacionViewModel pavm = new PlanillaAnulacionViewModel(this.InitObject.Frmxanu, this.InitObject);
            return Json(pavm);
        }

        public ActionResult PlanillaAnulacion_NumeroAceptacionExportacion_Blur(string selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.Frmxanu.Tx_PlAnu[20].Text = selectedValue;
            ftService.PlanillaAnulada_Blur(this.InitObject, 20);
            PlanillaAnulacionViewModel pavm = new PlanillaAnulacionViewModel(this.InitObject.Frmxanu, this.InitObject);
            return Json(pavm);
        }


        public ActionResult PlanillaAnulacion_CodigoMonedaAnulado_Blur(string selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.Frmxanu.Tx_PlAnu[24].Text = selectedValue;
            ftService.PlanillaAnulada_Blur(this.InitObject, 24);
            PlanillaAnulacionViewModel pavm = new PlanillaAnulacionViewModel(this.InitObject.Frmxanu, this.InitObject);
            return Json(pavm);
        }


        public ActionResult PlanillaAnulacion_TipoCambioAutorizacion_Blur(string selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.Frmxanu.Tx_PlAnu[32].Text = selectedValue;
            ftService.PlanillaAnulada_Blur(this.InitObject, 32);
            PlanillaAnulacionViewModel pavm = new PlanillaAnulacionViewModel(this.InitObject.Frmxanu, this.InitObject);
            return Json(pavm);
        }

        public ActionResult PlanillaAnulacion_MontoAnulado_Blur(string selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.Frmxanu.Tx_PlAnu[25].Text = selectedValue;
            ftService.PlanillaAnulada_Blur(this.InitObject, 25);
            PlanillaAnulacionViewModel pavm = new PlanillaAnulacionViewModel(this.InitObject.Frmxanu, this.InitObject);
            return Json(pavm);
        }

        public ActionResult PlanillaAnulacion_ParidadMmonMontoAnulado_Blur(string selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.Frmxanu.Tx_PlAnu[26].Text = selectedValue;
            ftService.PlanillaAnulada_Blur(this.InitObject, 26);
            PlanillaAnulacionViewModel pavm = new PlanillaAnulacionViewModel(this.InitObject.Frmxanu, this.InitObject);
            return Json(pavm);
        }

        public ActionResult PlanillaAnulacion_Index_Blur(string selectedValue, short index)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.Frmxanu.Tx_PlAnu[index].Text = selectedValue;
            ftService.PlanillaAnulada_Blur(this.InitObject, index);
            PlanillaAnulacionViewModel pavm = new PlanillaAnulacionViewModel(this.InitObject.Frmxanu, this.InitObject);
            return Json(pavm);
        }

        #endregion

        #region Planilla invisible Editar

        public ActionResult PlanillaInvisibleEditar()
        {
            ftService.PlanillaInvisibleEditarInit(this.InitObject);
            PlanillaInvisibleEditarViewModel pievm = new PlanillaInvisibleEditarViewModel(this.InitObject);
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            return View(pievm);
        }

        [HttpPost]
        public ActionResult PlanillaInvisibleEditar(PlanillaInvisibleEditarViewModel pievm, string btnComando)
        {

            pievm.Update(this.InitObject, this.InitObject.Frm_Pln_Invisible);
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            if (btnComando == ">>")
            {
                ftService.PlanillaInvisibleEditar_AdelanteClick(this.InitObject);
            }
            else if (btnComando == "<<")
            {
                ftService.PlanillaInvisibleEditar_AtrasClick(this.InitObject);
            }
            else if (btnComando == "√")
            {
                ftService.PlanillaInvisibleEditar_GenerarClick(this.InitObject);
            }
            else if (btnComando == "Aceptar")
            {
                ftService.PlanillaInvisibleEditar_AceptarClick(this.InitObject);
                return new RedirectResult("~/Fundtransfer");
            }
            else if (btnComando == "Cancelar")
            {
                return new RedirectResult("~/Fundtransfer");
            }

            pievm = new PlanillaInvisibleEditarViewModel(this.InitObject);
            return View(pievm);
        }

        #endregion

        #region EVENTOS REVERSAR OPERACION 
        #region [HttpGet]
        public ActionResult ReversarOperacionDeclaracion()
        {
            var pivm = new ReversarOperacionDeclaracionViewModel(this.InitObject.Frm_Declaracion);
            return new JsonResult()
            {
                Data = pivm,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult ReversarOperacionExport()
        {
            return Rutear(() =>
            {
                this.InitObject.FormularioQueAbrir = null;
                this.ftService.ReversarOperacionExportInit(this.InitObject);
            },
            () =>
            {
                ReversarOperacionExportViewModel o = new ReversarOperacionExportViewModel(InitObject.frmgrev, InitObject.MODGANU.VAnuPl);
                return View(o);
            });

        }

        /// <summary>
        /// se ejecuta luego de volver a ReversarOperacion luego de la pantalla de Ingreso de Valores
        /// </summary>
        /// <returns></returns>
        public ActionResult ReversarOperacionExport_CobraComis()
        {
            return Rutear(() =>
            {
                this.ftService.ReversarOperacion_CobraComis(InitObject);
            }, null);
        }

        public ReversarOperacionExportViewModel GetReversarOperacionExportViewModel()
        {
            ReversarOperacionExportViewModel modelo = new ReversarOperacionExportViewModel();
            //Llenar la views Model:

            return modelo;
        }
        #endregion

        #region [HttpPost]
        [HttpPost]
        public ActionResult ReversarOperacionExport(ReversarOperacionExportViewModel view, string command)
        {
            return Rutear(() =>
            {
                #region Llamada a Botones
                if (command == "Aceptar")
                {
                    short index = 0;
                    bool termino = ftService.ReversarOperacionExport_Co_Boton_Click(this.InitObject, index);
                    if (termino)
                    {
                        this.InitObject.Frag_Anula = 1;
                    }
                    if (termino && this.InitObject.MODGCVD.VgCvd.AceptoRev != -1)
                    {
                        this.InitObject.FormularioQueAbrir = "Index";
                    }
                }
                else if (command == "Cancelar")
                {
                    short index = 1;
                    this.InitObject.Frag_Anula = 0;
                    ftService.ReversarOperacionExport_Co_Boton_Click(this.InitObject, index);
                }
                else if (command == "Boton_ok_operacion")
                {
                    this.InitObject.Frag_Anula = 0;
                    InitObject.frmgrev.Tx_NroOpe[0].Text = (view.Tx_NroOpe_000 ?? string.Empty).PadLeft(3, '0');
                    InitObject.frmgrev.Tx_NroOpe[1].Text = (view.Tx_NroOpe_001 ?? string.Empty).PadLeft(2, '0');
                    InitObject.frmgrev.Tx_NroOpe[2].Text = (view.Tx_NroOpe_002 ?? string.Empty).PadLeft(2, '0');
                    InitObject.frmgrev.Tx_NroOpe[3].Text = (view.Tx_NroOpe_003 ?? string.Empty).PadLeft(3, '0');
                    InitObject.frmgrev.Tx_NroOpe[4].Text = (view.Tx_NroOpe_004 ?? string.Empty).PadLeft(5, '0');
                    this.InitObject.NroSce_Anula = InitObject.frmgrev.Tx_NroOpe[0].Text + InitObject.frmgrev.Tx_NroOpe[1].Text + InitObject.frmgrev.Tx_NroOpe[2].Text + InitObject.frmgrev.Tx_NroOpe[3].Text + InitObject.frmgrev.Tx_NroOpe[4].Text;
                    ftService.ReversarOperacionExport_Ok_Operacion_Click(this.InitObject);
                }
                #endregion
            },
            () =>
            {
                var rb = new ReversarOperacionExportViewModel(this.InitObject.frmgrev, InitObject.MODGANU.VAnuPl);
                return View(rb);
            });
        }

        [HttpPost]
        public JsonResult ReversarOperacionExport_Ok_Operacion_Click(string Tx_NroOpe_000, string Tx_NroOpe_001, string Tx_NroOpe_002, string Tx_NroOpe_003, string Tx_NroOpe_004)
        {
            this.InitObject.frmgrev.Errores = new List<UI_Message>();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();

            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
            InitObject.frmgrev.Tx_NroOpe[0].Text = Tx_NroOpe_000;
            InitObject.frmgrev.Tx_NroOpe[1].Text = Tx_NroOpe_001;
            InitObject.frmgrev.Tx_NroOpe[2].Text = Tx_NroOpe_002;
            InitObject.frmgrev.Tx_NroOpe[3].Text = Tx_NroOpe_003;
            InitObject.frmgrev.Tx_NroOpe[4].Text = Tx_NroOpe_004;
            ftService.ReversarOperacionExport_Ok_Operacion_Click(this.InitObject);
            var pvm = new ReversarOperacionExportViewModel(this.InitObject.frmgrev, InitObject.MODGANU.VAnuPl);
            return Json(pvm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Reversar_Operacion_Export_Lt_Pln_Click(int selectedValue)
        {
            InitObject.frmgrev.Lt_Pln.ListIndex = InitObject.frmgrev.Lt_Pln.Items.FindIndex(x => x.Data == selectedValue);
            ftService.ReversarOperacionExport_Lt_Pln_Click(InitObject);
            var model = new ReversarOperacionExportViewModel(InitObject.frmgrev, InitObject.MODGANU.VAnuPl);
            model.TipoAnuls = InitObject.MODGANU.VAnuPl[selectedValue].VisInv;

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Reversar_Operacion_Export_co_boton_Click(short boton_op)
        {
            bool termino = ftService.ReversarOperacionExport_Co_Boton_Click(this.InitObject, boton_op);
            var pvm = new ReversarOperacionExportViewModel(this.InitObject.frmgrev, InitObject.MODGANU.VAnuPl);
            return Json(pvm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ReversarOperacionExportBtnDeclaracion_Click()
        {
            ftService.ReversarOperacionExport_Bot_Dec_Click(this.InitObject);
            ftService.ReversarOperacionExportDeclaracion_Form_Load(this.InitObject);
            var pvm = new ReversarOperacionDeclaracionViewModel(this.InitObject.Frm_Declaracion);
            return Json(pvm, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ReversarOperacionExportBtnObservacion_Click()
        {
            ftService.ReversarOperacionExport_BOT_Obs_Click(this.InitObject);
            var pvm = new ReversarOperacionExportViewModel(this.InitObject.frmgrev, InitObject.MODGANU.VAnuPl);
            return Json(pvm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ReversarOperacionExportBtn_ok_Click_1(
            string _NumeroAutorizacion, string _FechaAutorizacion, string _MotivoAutorizacion, int _tipoAutorizacion, string _TipCamAutorizacion,
            int _SucursalBCCH_Autorizacion, int _TipoPlanilla, int _TipoAnulacion, string _ObservacionPln, bool _vieneDeMensaje, bool _resMensaje)
        {
            var model = new ReversarOperacionExportViewModel(this.InitObject.frmgrev, InitObject.MODGANU.VAnuPl);
            this.InitObject.frmgrev.Errores = new List<UI_Message>();

            #region Asignacion Parametros.
            InitObject.frmgrev.Cb_Tipo = model.Cb_Tipo;
            model.TipoAnuls = InitObject.MODGANU.VAnuPl[_TipoPlanilla].VisInv;

            var initObject = this.InitObject;
            this.InitObject.frmgrev.Tx_NroPln.Text = _NumeroAutorizacion;
            this.InitObject.frmgrev.Tx_Fecha.Text = _FechaAutorizacion;
            this.InitObject.frmgrev.Tx_Motivo.Text = _MotivoAutorizacion;
            this.InitObject.frmgrev.Cb_Tipo.ListIndex = _tipoAutorizacion;
            this.InitObject.frmgrev.Tx_TipCam.Text = _TipCamAutorizacion;
            this.InitObject.frmgrev.Cb_Pbc.ListIndex = _SucursalBCCH_Autorizacion;
            this.InitObject.frmgrev.CB_Tipanu.ListIndex = _TipoAnulacion;
            this.InitObject.frmgrev.Tx_ObsPln.Text = _ObservacionPln;
            #endregion

            #region Llamada Metodo Click
            model.vieneDeMensaje = ftService.ReversarOperacionExport_ok_Click_1(this.InitObject, _vieneDeMensaje, _resMensaje);
            #endregion
            this.InitObject = initObject;
            model.MensajesDeErrores = this.InitObject.frmgrev.Errores;
            model.MensajesDeConfirmacion = this.InitObject.frmgrev.PopUps.Where(x => x.Type == TipoMensaje.YesNo).Select(x => x.Text).ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Tx_CAM_Tipcam_Blur(string text)
        {
            var initObject = this.InitObject;
            initObject.frmgrev.CAM_Tipcam.Text = text;
            ftService.ReversarOperacionExport_CAM_Tipcam_Blur(initObject);
            this.InitObject = initObject;
            var model = new ReversarOperacionExportViewModel(this.InitObject.frmgrev, InitObject.MODGANU.VAnuPl);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Tx_Fecha_Blur(string text)
        {
            var initObject = this.InitObject;
            initObject.frmgrev.Tx_Fecha.Text = text;
            ftService.ReversarOperacionExport_Tx_Fecha_Blur(initObject);
            this.InitObject = initObject;
            var model = new ReversarOperacionExportViewModel(this.InitObject.frmgrev, InitObject.MODGANU.VAnuPl);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Tx_NroOpe_Blur(short id, string text)
        {
            try
            {
                var initObject = this.InitObject;
                initObject.frmgrev.Tx_NroOpe[id].Text = text;
                ftService.ReversarOperacionExport_Tx_NroOpe_Blur(initObject, id);
                this.InitObject = initObject;
            }
            catch (Exception ex)
            {
                this.InitObject.frmgrev.Errores.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = ex.Message
                });
            }
            var model = new ReversarOperacionExportViewModel(this.InitObject.frmgrev, InitObject.MODGANU.VAnuPl);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Tx_TipCam_Blur(string text)
        {
            var initObject = this.InitObject;
            initObject.frmgrev.Tx_TipCam.Text = text;
            ftService.ReversarOperacionExport_Tx_TipCam_Blur(initObject);
            var model = new ReversarOperacionExportViewModel(this.InitObject.frmgrev, InitObject.MODGANU.VAnuPl);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #region Modulo Reversa Operacion Export Modal:
        [HttpPost]
        public ActionResult ReversarOperacionExportDeclaracion_Aceptar_Click(string VClausula, string Comisiones, string CAM_Otros, string CAM_Liquido)
        {
            //Presionar Boton Aceptar carga eventos del formulario Declaracion.

            this.InitObject.Frm_Declaracion.CAM_Clausula.Text = VClausula;
            this.InitObject.Frm_Declaracion.CAM_Comisiones.Text = Comisiones;
            this.InitObject.Frm_Declaracion.CAM_Otros.Text = CAM_Otros;
            this.InitObject.Frm_Declaracion.CAM_Liquido.Text = CAM_Liquido;

            ftService.ReversarOperacionExportDeclaracion_Acepta_Click(this.InitObject);

            var pvm = new ReversarOperacionExportViewModel(this.InitObject.frmgrev, this.InitObject.MODGANU.VAnuPl);

            pvm.MensajesDeErrores = this.InitObject.frmgrev.Errores;

            return new JsonResult()
            {
                Data = pvm,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        [HttpPost]
        public JsonResult CAM_Clausula_Blur(string text)
        {
            var initObject = this.InitObject;
            initObject.Frm_Declaracion.CAM_Clausula.Text = text;
            ftService.Tx_CAM_Clausula_Blur(initObject);

            var model = new ReversarOperacionDeclaracionViewModel(this.InitObject.Frm_Declaracion);
            model.MensajesDeError = this.InitObject.frmgrev.Errores.Where(x => x.Type == TipoMensaje.Error || x.Type == TipoMensaje.Informacion).Select(x => x.Text).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CAM_Comisiones_Blur(string text)
        {
            var initObject = this.InitObject;
            initObject.Frm_Declaracion.CAM_Comisiones.Text = text;
            ftService.Tx_CAM_Clausula_Blur(initObject);

            var model = new ReversarOperacionDeclaracionViewModel(this.InitObject.Frm_Declaracion);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CAM_Otros_Blur(string text)
        {
            var initObject = this.InitObject;
            initObject.Frm_Declaracion.CAM_Otros.Text = text;
            ftService.Tx_CAM_Otros_Blur(initObject);
            var model = new ReversarOperacionDeclaracionViewModel(this.InitObject.Frm_Declaracion);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CAM_Liquido_Blur(string text)
        {
            var initObject = this.InitObject;
            initObject.Frm_Declaracion.CAM_Liquido.Text = text;
            ftService.Tx_CAM_Liquido_Blur(initObject);
            var model = new ReversarOperacionDeclaracionViewModel(this.InitObject.Frm_Declaracion);
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #endregion
        #endregion

        #region "ANULACION OPERACIONES"

        public ActionResult AnulacionOperaciones()
        {
            return Rutear(() =>
            {
                ModelState.Clear();
                this.InitObject.FormularioQueAbrir = null;
                ftService.AnulacionOperacionInit(this.InitObject);
            },
            () =>
            {
                GanuViewModel o = new GanuViewModel(InitObject.Frmganu);
                return View(o);
            });
        }


        [HttpPost]
        public ActionResult AnulacionOperaciones(GanuViewModel ganu, string Command)
        {
            return Rutear(() =>
            {
                ModelState.Clear();
                if (Command == "Aceptar")
                {
                    string NroSCE = "";
                    //if (InitObject.MODGANU.VAnuPl.Count() != 0)
                    //{
                    //    NroSCE = InitObject.MODGANU.VAnu.CodOpe_t;
                    //}
                    NroSCE = InitObject.MODGANU.VAnu.CodOpe_t;
                    McambioService McambioServ = new McambioService();
                    ftService.AnulacionOperacion_Aceptar(this.InitObject);
                    if (NroSCE != "")
                    {
                        McambioServ.Fn_Anulacion(this.InitObject, NroSCE);
                    }
                }
                else if (Command == "Cancelar")
                {
                    ftService.AnulacionOperacion_Cancelar(this.InitObject);
                }
                else if (Command == "√")
                {
                    ganu.Update(InitObject.Frmganu);
                    ftService.AnulacionOperacion_Cmd_ok(this.InitObject);
                }
            },
            () =>
            {
                ganu = new GanuViewModel(this.InitObject.Frmganu);
                ganu.MensajesDeError = InitObject.Mdi_Principal.MESSAGES;
                return View(ganu);
            });
        }


        [HttpPost]
        public ActionResult AnulacionOperacion_Tx_NroOpe_Blur(short id, string text)
        {
            var initObject = this.InitObject;
            initObject.Frmganu.Tx_NroOpe[id].Text = text;
            ftService.AnulacionOperacion_Tx_NroOpe_Blur(initObject, id);
            this.InitObject = initObject;

            var model = new GanuViewModel(InitObject.Frmganu);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Numero de planillas para anular"
        public ActionResult PlanillaAnularNumero()
        {
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            this.InitObject.Frmnroa = new UI_frmnroa();
            this.ftService.NumeroPlanillasAnularInit(this.InitObject);

            PlanillaAnularNumeroViewModel panvm = new PlanillaAnularNumeroViewModel(this.InitObject.Frmnroa);
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            return View(panvm);
        }

        [HttpPost]
        public ActionResult PlanillaAnularNumero(PlanillaAnularNumeroViewModel model, string command)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            model.Update(this.InitObject.Frmnroa);
            this.ftService.bot_acep_Click(this.InitObject);
            if (this.InitObject.MODXANU.VgAnu.AnuSinOK == -1)
            {
                return RedirectToAction("PlanillaAnulacion");
            }
            else
            {
                if (command == "Salir")
                {
                    return new RedirectResult("~/Fundtransfer");
                }
            }
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            PlanillaAnularNumeroViewModel panvm = new PlanillaAnularNumeroViewModel(this.InitObject.Frmnroa);
            return View(panvm);
        }

        public ActionResult PlanillaAnular_Cb_Moneda_Click(short monedaItemData)
        {
            return Json(ftService.Cb_Moneda_Click(this.InitObject, monedaItemData), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Seleccion Oficina"

        public ActionResult SeleccionOficina()
        {
            return Rutear(() =>
            {
                this.InitObject.FormularioQueAbrir = null;
                this.ftService.SeleccionBancoInit(this.InitObject);
            },
            () =>
            {
                SeleccionOficinaViewModel pcvm = new SeleccionOficinaViewModel(this.InitObject.Frm_SeleccionOficina);
                return View(pcvm);
            });
        }

        [HttpPost]
        public ActionResult SeleccionOficina(SeleccionOficinaViewModel sovm, string cmdButton)
        {
            return Rutear(() =>
            {
                if (InitObject != null && InitObject.Frm_SeleccionOficina != null)
                {
                    sovm.Update(this.InitObject.Frm_SeleccionOficina);

                    if (cmdButton == "Aceptar")
                    {
                        this.InitObject.Mdi_Principal.MESSAGES.Clear();
                        ftService.Seleccion_Banco_AceptarBanco(this.InitObject);
                        if (this.InitObject.Mdi_Principal.MESSAGES.Count == 0)
                        {
                            InitObject.FormularioQueAbrir = InitObject.VieneDe; //vuelvo al Action que me invoco
                            InitObject.VieneDe = string.Empty;
                        }
                    }
                    else if (cmdButton == "Cancelar")
                    {
                        ftService.Seleccion_Banco_CancelarBanco(this.InitObject);
                        InitObject.FormularioQueAbrir = "Index";
                        InitObject.refrescarSesion = true;
                    }
                }
            },
            () =>
            {
                SeleccionOficinaViewModel pcvm = new SeleccionOficinaViewModel(this.InitObject.Frm_SeleccionOficina);
                pcvm.MensajesDeError = this.InitObject.Mdi_Principal.MESSAGES;
                return View(pcvm);
            });
        }

        #endregion

        #region "COMISIONES"
        public ActionResult Comisiones(bool? hayMensaje, bool? respuestaMensaje)
        {
            return Rutear(() =>
            {
                this.InitObject.Mdi_Principal.MESSAGES.Clear();
                ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
                ftService.ComisionesInit(this.InitObject, hayMensaje.HasValue && hayMensaje.Value, respuestaMensaje.HasValue && respuestaMensaje.Value);
            },
            () =>
            {
                ComisionesViewModel o = new ComisionesViewModel(InitObject.Frm_Comisiones, InitObject.Mdi_Principal.MESSAGES);
                return View(o);
            });
        }

        [HttpPost]
        public ActionResult Comisiones(ComisionesViewModel model, string Command)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            model.Update(InitObject.Frm_Comisiones);
            //limpio el modelstate para que use actualice los valores en la pantalla
            ModelState.Clear();

            if (Command == "√")
            {
                // model.Update(InitObject.Frm_Comisiones);
                ftService.Comisiones_Ok(this.InitObject);
            }
            else if (Command == "X")
            {
                ftService.Comisiones_No(this.InitObject);
            }
            else if (Command == "Aceptar")
            {
                ftService.Comisiones_Cm_com(this.InitObject);
                return new RedirectResult("~/Fundtransfer/ComisionesFinish");
            }
            else if (Command == "Cancelar")
                return new RedirectResult("~/Fundtransfer/ComisionesFinish");
            model = new ComisionesViewModel(this.InitObject.Frm_Comisiones, this.InitObject.Mdi_Principal.MESSAGES);
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            return View(model);
        }

        public ActionResult ComisionesFinish()
        {
            return Rutear(() => { ftService.Comisiones_Finish(InitObject); }, null);
        }

        [HttpPost]
        public ActionResult SeleccionarComision_Click(int selectedValue)
        {
            InitObject.Frm_Comisiones.Ls_com.ListIndex = InitObject.Frm_Comisiones.Ls_com.Items.FindIndex(x => x.Data == selectedValue);
            ftService.Comisiones_Ls_Com_Click(this.InitObject);
            var model = new ComisionesViewModel(this.InitObject.Frm_Comisiones, this.InitObject.Mdi_Principal.MESSAGES);
            return Json(new
            {
                model = model
            });
        }

        [HttpPost]
        public ActionResult Comisiones_Tx_Com_Text_Blur(short id, string text)
        {
            var initObject = this.InitObject;
            initObject.Frm_Comisiones.Tx_Com[id].Text = text;
            ftService.Comisiones_Tx_Com_Text_Blur(initObject, id); this.InitObject = initObject;
            var model = new ComisionesViewModel(this.InitObject.Frm_Comisiones, this.InitObject.Mdi_Principal.MESSAGES);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Generacion Swift

        public ActionResult GenerarSwift(bool? generarDatosDummy)
        {
            bool estadoPrevioOK = true;
            if (generarDatosDummy == true)  //remover esta linea una vez que esté toda la aplicación integrada
            {
                ftService.CargarConDummiesTodasLasEstructurasQueDeberianVenirCargadasDePantallasAnterioresAGenerarSwift(this.InitObject);
            }
            else
            {
                estadoPrevioOK = ftService.InicializarGeneracionDeSwift(this.InitObject);
            }

            if (estadoPrevioOK)
            {
                ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
                this.InitObject.MODGSWF.VGSwf.Acepto = false; //todavia no esta generado el swift

                GenerarSwiftViewModel model = new GenerarSwiftViewModel();
                IList<T_Pai> paises = ftService.CargarPaisesClientes(this.InitObject).OrderBy(p => p.Pai_PaiNom).ToList();
                IList<sce_cpai> paisesBanco = ftService.CargarPaisesBancos();
                IList<T_Mnd> monedas = ftService.CargarMonedas(this.InitObject);

                model.EsCargaAutomatica = (this.InitObject.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1);
                TextInfo textCulture = Thread.CurrentThread.CurrentCulture.TextInfo;
                model.PaisesTPai = paises.Select(p => new SelectListItem() { Value = p.Pai_PaiCod.ToString(), Text = textCulture.ToTitleCase(p.Pai_PaiNom.Trim().ToLower()) }).ToList();
                model.PaisesCPai = paisesBanco.Select(s => new SelectListItem() { Value = s.cpai_codpaic, Text = textCulture.ToTitleCase(s.cpai_nompai.Trim().ToLower()) }).ToList();
                model.CodigosDeOrdenPosiblesCampo23 = ftService.GetCodigosDeOrdenPosiblesCampo23();
                model.ReglasCodigosDeOrden = ftService.GetReglasCodigosDeOrdenCampo23E();
                model.Monedas = monedas.Select(m => new SelectListItem() { Value = m.Mnd_MndCod.ToString(), Text = textCulture.ToTitleCase(m.Mnd_MndNom.Trim().ToLower()) }).ToList();
                DateTime fechaPagoInicial = ftService.CalcularFechaInicialSwift(this.InitObject.MODGTAB0);
                CargarListasTipoBancos(model);

                model.Cliente = this.InitObject.MODGSWF.VCliSwf;

                //paso a uppercase el cliente por si esta en minusculas
                model.Cliente.DirCli1 = (model.Cliente.DirCli1 ?? String.Empty).ToUpper();
                model.Cliente.DirCli2 = (model.Cliente.DirCli2 ?? String.Empty).ToUpper();
                model.Cliente.NomCli = (model.Cliente.NomCli ?? String.Empty).ToUpper();
                model.Cliente.PaiCli = (model.Cliente.PaiCli ?? String.Empty).ToUpper();
                model.Cliente.PaiCliCod = (model.Cliente.PaiCliCod ?? String.Empty).ToUpper();

                bool algunaGenerada = this.InitObject.MODGSWF.VSwf.Where(p => p.EstaGen != 0).Any();
                if (!algunaGenerada)
                {
                    //es la 1era vez, pongo el 50F como defecto
                    model.Cliente.Es50F = true;
                }
                else
                {
                    model.Cliente.Es50F = this.InitObject.MOD_50F.CHK_50F;
                }

                T_BenSwf ben;
                for (int i = 0; i < this.InitObject.MODGSWF.VBenSwf.Length; i++)
                {
                    ben = this.InitObject.MODGSWF.VBenSwf[i];
                    ben.DirBen1 = (ben.DirBen1 ?? String.Empty).ToUpper();
                    ben.DirBen2 = (ben.DirBen2 ?? String.Empty).ToUpper();
                    ben.NomBen = (ben.NomBen ?? String.Empty).ToUpper();
                    ben.PaiBen_t = (ben.PaiBen_t ?? String.Empty).ToUpper();
                    ben.PaiBen59F = ben.PaiBen.ToString();
                    model.BeneficiariosIniciales.Add(ben);
                }

                for (short i = 0; i < this.InitObject.MODGSWF.VSwf.Length; i++)
                {
                    T_Swf planilla = this.InitObject.MODGSWF.VSwf[i];

                    PlanillaViewModel info = new PlanillaViewModel()
                    {
                        DatosSwift = planilla,
                        Montos = this.InitObject.MODGSWF.VMT103[i],
                    };

                    //Se elimina el campo 70 de los campos manuales, ya que se reparo el campo referencia para que corte en lineas  
                    //y transpase la info a base swift
                    var aux = ftService.GetCamposManualesSwift(T_MODGSWF.MT_103).Where(c => c.CodCam.Trim() != "70").ToList();
                    if (aux.Any(c => c.CodCam.Trim() == "72"))
                    {
                        bool discarded;
                        List<SelectListLine> auxLineas = ftService.GetCodigosCampo72("", planilla.CodMon.ToString(), out discarded).ToList();
                        aux.Where(c => c.CodCam.Trim() == "72").SingleOrDefault().LineasSecundarias.FirstOrDefault().ValorCampo = auxLineas;
                    }
                    info.LineasManuales.AddRange(aux); //por si el beneficiario no es banco;
                    info.LineasManuales.AddRange(ftService.GetCamposManualesSwift(T_MODGSWF.MT_202));//por si el beneficiario es banco

                    if (!string.IsNullOrEmpty(planilla.DocSwf))
                    {
                        //var camposPais = ftService.GetCodigosCampo72PorPais(pais, moneda).ToList();
                        foreach (LineaMensajeSwift LineaManual in info.LineasManuales)
                        {
                            switch (LineaManual.CodMT)
                            {
                                case T_MODGSWF.MT_103:

                                    string campo = MODSWENN.Fn_CampoMTManual(planilla.DocSwf, LineaManual.CodCam.Trim() + " ", "INFO DE REMITENTE A DESTINATARIO").Trim();
                                    if (!string.IsNullOrEmpty(campo) && LineaManual.CodCam.Trim() == "72")
                                    {
                                        //Carga codigos para paises con condiciones epeciales
                                        if (!string.IsNullOrEmpty(planilla.BcoPag.SwfBco) && planilla.BcoPag.SwfBco.Length == 11)
                                        {
                                            var banco = ftService.GetBancoPorSwift(this.InitObject, planilla.BcoPag.SwfBco.Substring(0, 8), planilla.BcoPag.SwfBco.Substring(8, 3));
                                            if (banco != null)
                                            {
                                                var pais = banco.BicCod;
                                                var moneda = planilla.CodMon;
                                                bool discarded;
                                                var camposPais = ftService.GetCodigosCampo72(pais, moneda.ToString(), out discarded).ToList();
                                                LineaManual.LineasSecundarias[0].ValorCampo = camposPais;
                                            }
                                        }
                                        // activar check
                                        LineaManual.Incluido = true;
                                        // lectura de las lineas
                                        //string camp = MODSWENN.Fn_CampoMTManual(planilla.DocSwf, LineaManual.CodCam.Trim() + " ", "INFO DE REMITENTE A DESTINATARIO").Trim();
                                        var arreglo = campo.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToArray();

                                        for (int z = 0; z < arreglo.Length; z++)
                                        {
                                            string linea = arreglo[z];
                                            if (!string.IsNullOrEmpty(linea))
                                            {
                                                string linea2 = string.Empty;
                                                if (z == 0)
                                                {
                                                    var lst = LineaManual.LineasSecundarias[z].ValorCampo.Where(c => c.Value == linea).FirstOrDefault();
                                                    if (lst != null)
                                                    {
                                                        LineaManual.LineasSecundarias[z].ValorCampo.Remove(lst);
                                                        lst.Selected = true;
                                                        LineaManual.LineasSecundarias[z].ValorCampo.Add(lst);
                                                        LineaManual.LineasSecundarias[z].ValorCampo = LineaManual.LineasSecundarias[z].ValorCampo.OrderByDescending(c => c.Selected == true).ToList();
                                                    }
                                                    else
                                                    {
                                                        LineaManual.Detalle = linea;
                                                    }
                                                }
                                                else
                                                {
                                                    linea2 = linea.Replace("//", string.Empty);
                                                    LineaManual.LineasSecundarias[z - 1].Detalle = linea2;
                                                }

                                            }
                                        }
                                    }

                                    campo = MODSWENN.Fn_CampoMTManual(planilla.DocSwf, LineaManual.CodCam.Trim(), "INFO EXIGIDA POR REGLAMENTOS").Trim();
                                    if (!string.IsNullOrEmpty(campo) && LineaManual.CodCam.Trim() == "77B")
                                    {
                                        // activar check
                                        LineaManual.Incluido = true;
                                        // lectura de las lineas
                                        var arreglo = campo.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToArray();

                                        for (int z = 0; z < arreglo.Length; z++)
                                        {
                                            string linea = arreglo[z];
                                            if (!string.IsNullOrEmpty(linea))
                                            {
                                                if (z == 0)
                                                {
                                                    LineaManual.Detalle = linea;
                                                }
                                                else
                                                {
                                                    LineaManual.LineasSecundarias[z - 1].Detalle = linea;
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case T_MODGSWF.MT_202:

                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    model.Planillas.Add(info);

                    planilla.IndMT = i; //este campo no se usa, lo puedo cargar yo con el indice, lo utilizo en knockout
                    if (planilla.EstaGen == 0)
                    {
                        planilla.BenSwf.IndBen = (short)(model.BeneficiariosIniciales.Count - 1); //por defecto es el beneficiario (el último elemento) y no el cliente
                        planilla.BenSwf.Es59F = true;
                        planilla.DatSwf.FecPag = fechaPagoInicial.ToString("dd/MM/yyyy");

                        if (!planilla.EsAladi)
                        {
                            planilla.DatSwf.TipGas = 3; //SHA
                        }

                        //agrego espacio para todos los bancos que me puede cargar el usuario, aunque esten vacíos ahora.
                        foreach (SelectListItem item in model.TipoBancosSiBeneficiarioNoEsBanco)
                        {
                            //info.Bancos.Add(item.Value, new T_BcoSwf());
                            info.Bancos.Add(item.Value, new T_BcoSwf());
                        }

                        if (!model.EsCargaAutomatica)
                        {
                            if (InitObject.MODGTAB0.VMndPai.Any(c => c.mnd_mndcod == planilla.CodMon))
                            {
                                T_MndPai item = InitObject.MODGTAB0.VMndPai.Where(c => c.mnd_mndcod == planilla.CodMon).SingleOrDefault();
                                string mnd_mndnmc = item.mnd_mndnmc;
                                mnd_mndnmc = mnd_mndnmc.Replace("$", string.Empty);
                                planilla.BenSwf.PaiBen59F = mnd_mndnmc;
                                planilla.DatSwf.PlzPag = item.mnd_mndpai;
                            }
                            else
                            {
                                planilla.BenSwf.PaiBen59F = T_MODGTAB0.PaisEEUUEn59F;
                                planilla.DatSwf.PlzPag = T_MODGTAB0.PaisEEUU;
                            }
                        }
                    }
                    else
                    {
                        //ya se genero la planilla, todos los datos que muestro tienen que ser los ingresados anteriormente
                        info.Bancos = MapearBancosDeEstructuraLegacyAModelo(planilla);

                        List<T_Campo23E> camposExistentes = this.InitObject.MODGSWF.VCod.Where(c => c.numswi == i).ToList();
                        if (camposExistentes.Count > 0)
                        {
                            foreach (T_Campo23E tcampo in camposExistentes)
                            {
                                CodigoDeOrdenCampo23Swift codOrden = new CodigoDeOrdenCampo23Swift();

                                int indexOfSeparador = tcampo.Codigo.IndexOf('/');
                                string strCodigo = String.Empty;

                                if (indexOfSeparador > 0)
                                {
                                    strCodigo = tcampo.Codigo.Substring(0, indexOfSeparador);
                                    codOrden.TextoAdicional = tcampo.Codigo.Substring(indexOfSeparador + 1);
                                }
                                else
                                {
                                    strCodigo = tcampo.Codigo;
                                }

                                codOrden.Codigo = (CodigoDeOrdenCampo23Swift.CodigoOrden)Enum.Parse(typeof(CodigoDeOrdenCampo23Swift.CodigoOrden), strCodigo);
                                info.CodigosDeOrdenCampo23.Add(codOrden);
                            }
                        }
                    }
                }
                //ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon;
                ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
                return View(model);
            }
            else
            {
                InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Title = "Operación inválida",
                    Text = "No es posible generar el swift.",
                    Type = TipoMensaje.Error
                });
                // Retornar a la pantalla Index
                return RedirectToAction("Index", "Fundtransfer");
            }
        }

        private void CargarListasTipoBancos(GenerarSwiftViewModel model)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem() { Value = T_MODGSWF.BcoAla.ToString(), Text = "Banco Aladi" });
            lista.Add(new SelectListItem() { Value = T_MODGSWF.BcoPag.ToString(), Text = "Banco Pagador (57)", Selected = true });
            lista.Add(new SelectListItem() { Value = T_MODGSWF.BcoInt.ToString(), Text = "Banco Intermediario (56)" });

            model.TipoBancosSiBeneficiarioEsBanco = lista;
            lista = lista.ToList(); //para generar una copia

            lista.Add(new SelectListItem() { Value = T_MODGSWF.BcoCoE.ToString(), Text = "Banco Corresponsal Emisor (53)" });
            lista.Add(new SelectListItem() { Value = T_MODGSWF.BcoCoD.ToString(), Text = "Banco Corresponsal Destinatario (54)" });
            lista.Add(new SelectListItem() { Value = T_MODGSWF.BcoTer.ToString(), Text = "Tercera Entidad de Reembolso (55)" });

            model.TipoBancosSiBeneficiarioNoEsBanco = lista;
        }

        [HandleAjaxException]
        public ActionResult GetBancosPorCodigoSwift(string swiftBanco)
        {
            if (swiftBanco.Length == 11)
            {
                var data = ftService.GetBancoPorSwift(this.InitObject, swiftBanco.Substring(0, 8), swiftBanco.Substring(8, 3));

                if (data != null)
                {
                    var jsonResult = new JsonResult()
                    {
                        Data = data,
                        MaxJsonLength = Int32.MaxValue,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                    return jsonResult;
                }
                else return null;

            }
            else
            {
                throw new ArgumentException("El swift debe tener un largo de 11 caracteres");
            }

        }

        [HandleAjaxException]
        public ActionResult GetCorresponsales(short idPlazaDePago, short codMoneda)
        {
            ftService.CargarCorresponsales(this.InitObject);
            IList<T_Cor> corresponsales = Mdl_Funciones.Filtra_Cor(this.InitObject, idPlazaDePago, codMoneda, false);

            var jsonResult = new JsonResult()
            {
                Data = corresponsales,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            return jsonResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HandleAjaxException, HttpPost]
        public ActionResult GenerarSwiftValidaFechaPago(GenerarSwiftViewModel model)
        {
            PlanillaViewModel p = model.Planillas[model.IndiceP];
            string msg = string.Empty;
            ftService.ValidarFechaPago(this.InitObject.MODGTAB0, DateTime.Parse(p.DatosSwift.DatSwf.FecPag), out msg);

            List<UI_Message> mensajes = new List<UI_Message>();
            if (msg != string.Empty)
            {
                mensajes.Add(new UI_Message() { Type = TipoMensaje.Error, ControlName = "txtFechaPago", Text = msg });
            }

            var jsonResult = new JsonResult()
            {
                Data = mensajes,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            return jsonResult;
        }

        [HandleAjaxException, HttpPost]
        public ActionResult ValidarYGenerarSwift(GenerarSwiftViewModel model)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();

            PlanillaViewModel p = model.Planillas[model.IndiceP];
            MapearBancosDeModeloAEstructuraLegacy(p.Bancos, p.DatosSwift);

            IList<UI_Message> mensajes = ftService.ValidarSwiftCompleto(this.InitObject, p.DatosSwift, model.Cliente, p.Montos, p.LineasManuales);

            if (mensajes.Count == 0)
            {
                bool genereOK = ftService.GenerarSwift(this.InitObject, p.DatosSwift, model.Cliente, p.Montos, p.CodigosDeOrdenCampo23, p.LineasManuales, model.IndiceP);
                mensajes = this.InitObject.Mdi_Principal.MESSAGES;
            }

            var data = new
            {
                EstaGen = InitObject.MODGSWF.VSwf[model.IndiceP].EstaGen,
                DocSwift = InitObject.MODGSWF.VSwf[model.IndiceP].DocSwf,
                Mensajes = mensajes
            };

            var jsonResult = new JsonResult()
            {
                Data = data,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            return jsonResult;
        }

        [HttpPost]
        public ActionResult AceptarGenerarSwift()
        {
            this.InitObject.MODGSWF.VGSwf.Acepto = true;
            ftService.FinalizarGeneracionDeSwift(this.InitObject);
            return RedirectToAction("Index");
        }

        private void MapearBancosDeModeloAEstructuraLegacy(IDictionary<string, T_BcoSwf> bancos, T_Swf swift)
        {
            foreach (string keyBanco in bancos.Keys)
            {
                short key = short.Parse(keyBanco);
                T_BcoSwf banco = bancos[keyBanco];
                switch (key)
                {
                    case T_MODGSWF.BcoAla:
                        swift.BcoAla = banco;
                        break;

                    case T_MODGSWF.BcoCoD:
                        swift.BcoCoD = banco;
                        break;

                    case T_MODGSWF.BcoCoE:
                        swift.BcoCoE = banco;
                        break;

                    case T_MODGSWF.BcoInt:
                        swift.BcoInt = banco;
                        break;

                    case T_MODGSWF.BcoPag:
                        swift.BcoPag = banco;
                        break;

                    case T_MODGSWF.BcoTer:
                        swift.BcoTer = banco;
                        break;
                }
            }
        }

        private IDictionary<string, T_BcoSwf> MapearBancosDeEstructuraLegacyAModelo(T_Swf swift)
        {
            Dictionary<string, T_BcoSwf> bancos = new Dictionary<string, T_BcoSwf>();
            bancos.Add(T_MODGSWF.BcoAla.ToString(), swift.BcoAla);
            bancos.Add(T_MODGSWF.BcoCoD.ToString(), swift.BcoCoD);
            bancos.Add(T_MODGSWF.BcoCoE.ToString(), swift.BcoCoE);
            bancos.Add(T_MODGSWF.BcoInt.ToString(), swift.BcoInt);
            bancos.Add(T_MODGSWF.BcoPag.ToString(), swift.BcoPag);
            bancos.Add(T_MODGSWF.BcoTer.ToString(), swift.BcoTer);

            return bancos;
        }

        [HandleAjaxException]
        public ActionResult ValidarCodCom(string codCom)
        {
            List<UI_Message> mensajes = new List<UI_Message>();
            string mensajeError = string.Empty;
            bool esValido = esValido = ftService.ValidarCodComp(this.InitObject.MODGSWF, codCom, out mensajeError);
            mensajes.Add(new UI_Message()
            {
                Type = (esValido ? TipoMensaje.Nada : TipoMensaje.Error),
                Text = mensajeError,
                ControlName = "txtCodCompBanco"
            });

            var jsonResult = new JsonResult()
            {
                Data = mensajes,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            return jsonResult;
        }

        [HandleAjaxException]
        public ActionResult GetCondicionesPais(string swiftBanco, int moneda)
        {
            List<SelectListLine> codigosCampo72;
            bool condicionesEspecialesPais;
            string pais = "";
            if (swiftBanco.Length == 11)
            {
                T_Bic banco = ftService.GetBancoPorSwift(this.InitObject, swiftBanco.Substring(0, 8), swiftBanco.Substring(8, 3));
                if (banco != null)
                {
                    pais = banco.BicCod;
                }
            }
            codigosCampo72 = ftService.GetCodigosCampo72(pais, moneda.ToString(), out condicionesEspecialesPais).ToList();

            var data = new
            {
                condicionesEspeciales = condicionesEspecialesPais,
                codigos = codigosCampo72
            };

            var jsonResult = new JsonResult()
            {
                Data = data,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            return jsonResult;

        }

        #endregion

        #region IMPRESION GRABAR

        public ActionResult ImprimirPlanillaInvisible(int id)
        {
            using (var tracer = new Tracer("ImprimirPlanillaInvisible"))
            {
                try
                {
                    //id es el indice dentro de la lista PlanillasInvisibles que esta en el initObject
                    id = id >= 0 ? id : 0;
                    if (this.InitObject.PlanillasInvisibles.Any())
                    {
                        if (id <= (this.InitObject.PlanillasInvisibles.Count() - 1))
                        {
                            return View(this.InitObject.PlanillasInvisibles[id]);
                        }
                        else
                        {
                            throw new Exception("El indice solicitado es mayor al tamaño de la lista 'PlanillasInvisibles'");
                        }
                    }
                    else
                    {
                        throw new Exception("La coleccion no tiene registros");
                    }
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta en ImprimirPlanillaInvisible : ", ex);
                    throw;
                }
            }
        }

        public ActionResult ImprimirPlanilla(int id)
        {
            using (var tracer = new Tracer("ImprimirPlanilla"))
            {
                try
                {
                    //id es el indice dentro de la lista Planillas que esta en el initObject
                    if (this.InitObject.Planillas.Count == 0)
                        throw new Exception("La coleccion no tiene registros");
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta en ImprimirPlanilla : ", ex);
                    throw;
                }

                return View(this.InitObject.Planillas[id]);
            }
        }

        public ActionResult PlanillaReemplazos(int id)
        {
            return View(this.InitObject.PlanillasReemplazo[id]);
            //id es el indice dentro de la lista PlanillasReemplazo que esta en el initObject
        }

        public ActionResult IMPGRAB_Finish()
        {
            return Rutear(() => { this.InitObject.FormularioQueAbrir = this.InitObject.VieneDe; }, null, false);
        }

        public ActionResult ImprimirPlanilla500(int id)
        {
            return View(this.InitObject.Planillas500[id]);
            //id es el indice dentro de la lista Planillas500 que esta en el initObject
        }

        public ActionResult ImprimirPlanilla401(int id)
        {
            //id es el indice dentro de la lista Planillas401 que esta en el initObject
            return View(this.InitObject.Planillas401[id]);
        }

        //public ActionResult PlanillaVisibleAnulada(int id)
        //{
        //    //id es el indice dentro de la lista Planillas401 que esta en el initObject
        //    throw new NotImplementedException();
        //}


        public string PlanillaVisibleAnulada(int id)
        {
            string result = string.Empty;
            //id es el indice dentro de la lista PlanillasVisiblesAnuladas que esta en el initObject
            //throw new NotImplementedException();
            return result;
        }

        //public ActionResult ImprimirPlanillaVisibleExportacion(int id)
        //{
        //    //id es el indice dentro de la lista PlanillasVisiblesExportacion que esta en el initObject
        //    throw new NotImplementedException();
        //}

        //public ActionResult PlanillaEstadistica(int id)
        //{
        //    //id es el indice dentro de la lista PlanillasEstadisticas que esta en el initObject
        //    throw new NotImplementedException();
        //}


        #endregion

        #region Planilla Cobertura Visible de Importacion
        public ActionResult PlanillaVisibleImportEditar()
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            ftService.PlanillaVisibleImportEditarInit(InitObject);

            PlanillaVisibleImportEditarViewModel o = new PlanillaVisibleImportEditarViewModel(InitObject.Frm_Pln_cob);
            //ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon;
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            return View(o);
        }

        [HttpPost]
        public ActionResult PlanillaVisibleImportEditar(PlanillaVisibleImportEditarViewModel pvm, string Command)
        {
            //limpio el modelstate para que use los valoresde InitObj, no los del post
            ModelState.Clear();

            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            if (Command == "<<")
            {
                ftService.PlanillaVisibleImportEditar_Anterior(this.InitObject);

            }
            else if (Command == ">>")
            {
                ftService.PlanillaVisibleImportEditar_Siguiente(this.InitObject);

            }
            else if (Command == "Ok")
            {
                pvm.Update(this.InitObject.Frm_Pln_cob);
                ftService.PlanillaVisibleImportEditar_Ok(this.InitObject);

            }
            else if (Command == "Aceptar" || Command == "Cancelar")
            {
                return new RedirectResult("~/Fundtransfer");
            }
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            pvm = new PlanillaVisibleImportEditarViewModel(InitObject.Frm_Pln_cob);
            return View(pvm);
        }

        #endregion

        #region Emitir Nota de credito
        public ActionResult EmitirNotaCredito()
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            //limpio el modelstate para que use actualice los valores en la pantalla
            ModelState.Clear();

            //Es necesario revisar esto, ya que cuando viene desde el menu trae datos en formularioQueAbrir, por lo cual, cuando sea diferente se debe
            //trabajar como que es primera vez que viene y no que vuelve desde FacturasAsociadas
            if (InitObject.FormularioQueAbrir.Trim() != "EmitirNotaCredito")
            {
                ftService.EmitirNotaCreditoInit(InitObject);
            }
            else
            {
                InitObject.FormularioQueAbrir = string.Empty;
                ftService.EmitirNotaCredito_RetornoFacturasAsociadas(InitObject);
            }

            EmitirNotaCreditoViewModel o = new EmitirNotaCreditoViewModel(InitObject.Frmgnota);
            //ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon;
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            return View(o);
        }

        [HttpPost]
        public ActionResult EmitirNotaCredito(EmitirNotaCreditoViewModel encvm, string Command)
        {
            //limpio el modelstate para que use los valoresde InitObj, no los del post
            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            if (Command == "V")
            {
                encvm.Update(InitObject.Frmgnota);
                ftService.EmitirNotaCredito_Ok_Click(this.InitObject);

                if (!string.IsNullOrWhiteSpace(this.InitObject.FormularioQueAbrir))
                {
                    return RedirectToAction(this.InitObject.FormularioQueAbrir);
                }
            }
            else if (Command == "Aceptar")
            {
                ftService.EmitirNotaCredito_Aceptar_Click(InitObject);
                return new RedirectResult("~/Fundtransfer");
            }
            else if (Command == "Cancelar")
            {
                ftService.EmitirNotaCredito_Cancelar_Click(InitObject);
                return new RedirectResult("~/Fundtransfer");
            }
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            EmitirNotaCreditoViewModel o = new EmitirNotaCreditoViewModel(InitObject.Frmgnota);
            return View(o);
        }

        public ActionResult EmitirNotaCredito_Producto_Change(int selectedValue, int indexValue)
        {
            InitObject.Frmgnota.Cb_Producto.SelectedValue = selectedValue;
            InitObject.Frmgnota.Cb_Producto.ListIndex = indexValue - 1;

            ftService.EmitirNotaCredito_Producto_Change(InitObject);

            var model = new EmitirNotaCreditoViewModel(InitObject.Frmgnota);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EmitirNotaCredito_Tx_NroOpe_Blur(short id, string text)
        {
            var initObject = this.InitObject;
            initObject.Frmgnota.Tx_NumOpe[id].Text = text;
            ftService.EmitirNotaCredito_Tx_NroOpe_Blur(initObject, id);
            this.InitObject = initObject;

            var model = new EmitirNotaCreditoViewModel(InitObject.Frmgnota);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region RELACIONAR OPERACION
        private JsonResult RelacionarOperacionEvent(UI_FrmgAso jsonModel, Action metodo)
        {
            try
            {
                this.InitObject.Mdi_Principal.MESSAGES.Clear();
                InitObject.FrmgAso = jsonModel;
                metodo();
                InitObject.FrmgAso.Errores = this.InitObject.Mdi_Principal.MESSAGES;

                return Json(InitObject.FrmgAso, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                InitObject.FrmgAso.Errores.Add(new UI_Message
                {
                    Text = e.Message,
                    Title = "Ha ocurrido un error",
                    Type = TipoMensaje.Critical
                });
                return Json(InitObject.FrmgAso, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult FrmgAso_LoadFrm()
        {
            ftService.FrmgAso_LoadFrm(InitObject);
            InitObject.FrmgAso.OPE = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            return Json(InitObject.FrmgAso, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FrmgAso_ok_Click(UI_FrmgAso jsonModel)
        {
            return RelacionarOperacionEvent(jsonModel, () =>
            {
                ftService.FrmgAso_ok_Click(InitObject);
                InitObject.FrmgAso.OPE = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            });
        }

        [HttpPost]
        public JsonResult FrmgAso_Aceptar_Click(UI_FrmgAso jsonModel)
        {
            return RelacionarOperacionEvent(jsonModel, () =>
            {
                ftService.FrmgAso_Aceptar_Click(InitObject);
            });
        }

        [HttpPost]
        public JsonResult FrmgAso_Cancelar_Click(UI_FrmgAso jsonModel)
        {
            return RelacionarOperacionEvent(jsonModel, () =>
            {
                ftService.FrmgAso_Cancelar_Click(InitObject);
                InitObject.FormularioQueAbrir = "Index";
            });
        }

        [HttpPost]
        public JsonResult FrmgAso_Cb_Producto_Click(UI_FrmgAso jsonModel)
        {
            return RelacionarOperacionEvent(jsonModel, () =>
            {
                ftService.FrmgAso_Cb_Producto_Click(InitObject);
            });
        }

        [HttpPost]
        public ActionResult FrmgAso_Tx_NumOpe_LostFocus(UI_FrmgAso jsonModel, short id, string text)
        {
            var initObject = this.InitObject;
            initObject.FrmgAso.Tx_NroOpe[id].Text = text;
            ftService.FrmgAso_Tx_NumOpe_LostFocus(initObject, id);
            this.InitObject = initObject;
            return Json(InitObject.FrmgAso, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FrmgAso_Tx_NumOpe_001_LostFocus(UI_FrmgAso jsonModel)
        {
            return RelacionarOperacionEvent(jsonModel, () =>
            {
                ftService.FrmgAso_Tx_NumOpe_LostFocus(InitObject, 1);
            });
        }

        [HttpPost]
        public JsonResult FrmgAso_Tx_NumOpe_002_LostFocus(UI_FrmgAso jsonModel)
        {
            return RelacionarOperacionEvent(jsonModel, () =>
            {
                ftService.FrmgAso_Tx_NumOpe_LostFocus(InitObject, 2);
            });
        }

        [HttpPost]
        public JsonResult FrmgAso_Tx_NumOpe_003_LostFocus(UI_FrmgAso jsonModel)
        {
            return RelacionarOperacionEvent(jsonModel, () =>
            {
                ftService.FrmgAso_Tx_NumOpe_LostFocus(InitObject, 3);
            });
        }

        [HttpPost]
        public JsonResult FrmgAso_Tx_NumOpe_004_LostFocus(UI_FrmgAso jsonModel)
        {
            return RelacionarOperacionEvent(jsonModel, () =>
            {
                ftService.FrmgAso_Tx_NumOpe_LostFocus(InitObject, 4);
            });
        }

        [HttpPost]
        public JsonResult FrmgAso_Tx_NumOpe_005_LostFocus(UI_FrmgAso jsonModel)
        {
            return RelacionarOperacionEvent(jsonModel, () =>
            {
                ftService.FrmgAso_Tx_NumOpe_LostFocus(InitObject, 5);
            });
        }

        [HttpPost]
        public JsonResult FrmgAso_Tx_NumOpe_006_LostFocus(UI_FrmgAso jsonModel)
        {
            return RelacionarOperacionEvent(jsonModel, () =>
            {
                ftService.FrmgAso_Tx_NumOpe_LostFocus(InitObject, 6);
            });
        }
        #endregion

        #region FORMULARIO FACTURAS

        public ActionResult FacturasAsociadas()
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            ftService.FacturasAsociadasInit(InitObject);

            FacturasAsociadasViewModel o = new FacturasAsociadasViewModel(InitObject.FrmFact);
            //ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon;
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            return View(o);

        }

        [HttpPost]
        public ActionResult FacturasAsociadas(FacturasAsociadasViewModel favm, string Command)
        {
            //limpio el modelstate para que use los valoresde InitObj, no los del post
            ModelState.Clear();

            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            if (Command == "Aceptar")
            {
                favm.Update(InitObject.FrmFact);
                ftService.FacturasAsociadas_bot_acep_Click(InitObject);
                return RedirectToAction(InitObject.FormularioQueAbrir);
            }
            else if (Command == "Cancelar")
            {
                InitObject.FormularioQueAbrir = "EmitirNotaCredito";
                return RedirectToAction(InitObject.FormularioQueAbrir);
            }
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            FacturasAsociadasViewModel o = new FacturasAsociadasViewModel(InitObject.FrmFact);

            return View(o);

        }


        public ActionResult FacturasAsociadas_DobleClick_Factura(int selectedValue, int indexValue)
        {
            InitObject.FrmFact.L_Print.SelectedValue = selectedValue;
            InitObject.FrmFact.L_Print.ListIndex = indexValue;

            ftService.FacturasAsociadas_bot_acep_Click(InitObject);

            //return RedirectToAction("EmitirNotaCredito_RetornoFacturasAsociadas");

            //var model = new EmitirNotaCreditoViewModel(InitObject.Frmgnota);

            return Json(Url.Action("EmitirNotaCredito_RetornoFacturasAsociadas"), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Emitir Cheque
        private JsonResult EmitirChequeEvent(UI_FrmChq jsonModel, Action metodo)
        {
            try
            {
                ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
                InitObject.Frm_Chq = jsonModel;
                metodo();
                this.InitObject.Mdi_Principal.MESSAGES.Clear();
                return Json(InitObject.Frm_Chq ?? jsonModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                InitObject.Frm_Chq.Errors.Add(new UI_Message
                {
                    Text = e.Message,
                    Title = "Ha ocurrido un error",
                    Type = TipoMensaje.Critical
                });
                return Json(InitObject.Frm_Chq, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EmitirCheque_Load()
        {
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
            ftService.EMITIRCHEQUE_Documentos(this.InitObject);
            return Json(InitObject.Frm_Chq, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EmitirCheque_Co_Aceptar_Click(UI_FrmChq jsonModel)
        {
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            return EmitirChequeEvent(jsonModel, () =>
            {
                this.InitObject.Frm_Chq.Confirms.Clear();
                this.InitObject.Frm_Chq.Errors.Clear();
                ftService.EMITIRCHEQUE_Co_Aceptar_Click(InitObject);
                this.InitObject.FormularioQueAbrir = "Index";
            });
        }
        [HttpPost]
        public JsonResult EmitirCheque_Co_Cancelar_Click(UI_FrmChq jsonModel, bool? vieneDeMsg, bool? respMsg)
        {
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;

            if (this.InitObject.Frm_Chq.Confirms.Count > 0 && this.InitObject.Frm_Chq.Errors.Count == 0)
            {
                vieneDeMsg = false;
                respMsg = true;
            }
            this.InitObject.Frm_Chq.Confirms.Clear();
            this.InitObject.Frm_Chq.Errors.Clear();
            ftService.EMITIRCHEQUE_Co_Cancelar_Click(InitObject, vieneDeMsg.HasValue ? vieneDeMsg.Value : false, respMsg.HasValue ? respMsg.Value : false);
            return Json(this.InitObject.Frm_Chq);

            /*return EmitirChequeEvent(jsonModel, () =>
            {
                InitObject.Frm_Chq = jsonModel;  
                ftService.EMITIRCHEQUE_Co_Cancelar_Click(InitObject, vieneDeMsg.HasValue ? vieneDeMsg.Value : false, respMsg.HasValue ? respMsg.Value : false);
                //this.InitObject.FormularioQueAbrir = "Index";
            });
            */
        }
        [HttpPost]
        public JsonResult EMITIRCHEQUE_Co_Generar_Click(UI_FrmChq jsonModel)
        {
            return EmitirChequeEvent(jsonModel, () =>
            {
                this.InitObject.Frm_Chq.Confirms.Clear();
                this.InitObject.Frm_Chq.Errors.Clear();
                ftService.EMITIRCHEQUE_Co_Generar_Click(InitObject);
            });
        }
        [HttpPost]
        public JsonResult EmitirCheque_l_benef_Click(UI_FrmChq jsonModel)
        {
            return EmitirChequeEvent(jsonModel, () =>
            {
                this.InitObject.Frm_Chq.Confirms.Clear();
                this.InitObject.Frm_Chq.Errors.Clear();
                ftService.EMITIRCHEQUE_l_benef_Click(InitObject);
            });
        }
        [HttpPost]
        public JsonResult EmitirCheque_L_Montos_Click(UI_FrmChq jsonModel)
        {
            return EmitirChequeEvent(jsonModel, () =>
            {
                this.InitObject.Frm_Chq.Confirms.Clear();
                this.InitObject.Frm_Chq.Errors.Clear();
                this.InitObject.Frm_Chq = jsonModel;
                ftService.EMITIRCHEQUE_L_Montos_Click(InitObject);
            });
        }
        [HttpPost]
        public JsonResult EmitirCheque_l_montos_DblClick(UI_FrmChq jsonModel)
        {
            return EmitirChequeEvent(jsonModel, () =>
            {
                ftService.EMITIRCHEQUE_l_montos_DblClick(InitObject);
            });
        }
        [HttpPost]
        public JsonResult EmitirCheque_l_plaza_Click(UI_FrmChq jsonModel)
        {
            return EmitirChequeEvent(jsonModel, () =>
            {
                this.InitObject.Frm_Chq.Confirms.Clear();
                this.InitObject.Frm_Chq.Errors.Clear();
                ftService.EMITIRCHEQUE_l_plaza_Click(InitObject);
            });
        }
        #endregion

        #region ANULACION PLANILLA INVISIBLE IMPORT

        public ActionResult AnulacionPlanillaVisibleImportIndex()
        {
            return Rutear(() =>
            {
                RefrescarSesionComex();
                this.InitObject.VieneDe = "AnulacionPlanillaVisibleImport";
                ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
                ftService.AnulacionPlanillaVisibleImportInit(this.InitObject);
            },
            () =>
            {
                AnulacionPlanillaVisibleImportViewModel apiivm = new AnulacionPlanillaVisibleImportViewModel(this.InitObject.Frm_Anu_Vi);
                ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
                return View(apiivm);
            });
        }

        public ActionResult AnulacionPlanillaVisibleImport()
        {
            return Rutear(() =>
            {
                ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
                ftService.AnulacionPlanillaVisibleImportInit(this.InitObject);
            },
            () =>
            {
                AnulacionPlanillaVisibleImportViewModel apiivm = new AnulacionPlanillaVisibleImportViewModel(this.InitObject.Frm_Anu_Vi);
                ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
                return View(apiivm);
            });
        }

        [HttpPost]
        public ActionResult AnulacionPlanillaVisibleImport(AnulacionPlanillaVisibleImportViewModel apiivm, string btnComando)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            apiivm.Update(this.InitObject.Frm_Anu_Vi);

            if (btnComando == "Ok")
            {
                this.InitObject.Frag_Anula = 0;
                this.InitObject.NroSce_Anula = "";
                this.InitObject.NroSce_Anula = (apiivm.Tx_NroOpe_000.Text ?? string.Empty).PadLeft(3, '0');
                this.InitObject.NroSce_Anula = this.InitObject.NroSce_Anula + (apiivm.Tx_NroOpe_001.Text ?? string.Empty).PadLeft(2, '0');
                this.InitObject.NroSce_Anula = this.InitObject.NroSce_Anula + (apiivm.Tx_NroOpe_002.Text ?? string.Empty).PadLeft(2, '0');
                this.InitObject.NroSce_Anula = this.InitObject.NroSce_Anula + (apiivm.Tx_NroOpe_003.Text ?? string.Empty).PadLeft(3, '0');
                this.InitObject.NroSce_Anula = this.InitObject.NroSce_Anula + (apiivm.Tx_NroOpe_004.Text ?? string.Empty).PadLeft(5, '0');
                ftService.AnulacionPlanillaVisibleImport_OkClick(this.InitObject);
            }
            else if (btnComando == "Aceptar")
            {
                //this.InitObject.Mto_Anula = double.Parse(apiivm.Tx_MtoAnu.Text);
                ftService.AnulacionPlanillaVisibleImport_AceptarClick(this.InitObject);
                this.InitObject.Frag_Anula = 1;
                if (this.InitObject.MODANUVI.Vx_AnuReem.AcepAnu == -1)
                {
                    return new RedirectResult("~/Fundtransfer");
                }
            }
            else if (btnComando == "Cancelar")
            {
                this.InitObject.Frag_Anula = 0;
                ftService.AnulacionPlanillaVisibleImport_CancelarClick(this.InitObject);
                this.InitObject.refrescarSesion = true;
                return new RedirectResult("~/Fundtransfer");
            }

            apiivm = new AnulacionPlanillaVisibleImportViewModel(this.InitObject.Frm_Anu_Vi);
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            return View(apiivm);
        }

        [HandleAjaxException]
        public ActionResult AnulacionPlanillaVisibleImport_LtClick(string selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            if (selectedValue != string.Empty)
            {
                this.InitObject.Frm_Anu_Vi.Lt_PlAnul.ListIndex = this.InitObject.Frm_Anu_Vi.Lt_PlAnul.Items.FindIndex(x => x.Data == Convert.ToInt32(selectedValue));
                ftService.AnulacionPlanillaVisibleImport_LtClick(this.InitObject);
                var data = new AnulacionPlanillaVisibleImportViewModel(this.InitObject.Frm_Anu_Vi);

                var jsonResult = new JsonResult()
                {
                    Data = data,
                    MaxJsonLength = Int32.MaxValue,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                return jsonResult;
            }
            else
            {
                throw new ArgumentException("Debe seleccionar un elemento de la lista de planillas");
            }
        }

        [HttpPost]
        public JsonResult AnulacionPlanillaVisibleImport_Tx_FechaPre_Blur(string text)
        {
            var initObject = this.InitObject;
            initObject.Frm_Anu_Vi.Tx_FecPre.Text = text;
            //ftService.AnulacionPlanillaVisibleImport_Tx_FechaPre_Blur(initObject);
            this.InitObject = initObject;
            var model = new AnulacionPlanillaVisibleImportViewModel(this.InitObject.Frm_Anu_Vi);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AnulacionPlanillaVisibleImport_Tx_FechaPre_Keypress(short KeyAscii, string text)
        {
            var initObject = this.InitObject;
            initObject.Frm_Anu_Vi.Tx_FecPre.Text = text;
            ftService.AnulacionPlanillaVisibleImport_Tx_FechaPre_Keypress(initObject, KeyAscii, text);
            this.InitObject = initObject;
            var model = new AnulacionPlanillaVisibleImportViewModel(this.InitObject.Frm_Anu_Vi);
            model.FecPre = model.Tx_FecPre.Text;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AnulacionPlanillaVisibleImport_Tx_NroOpe_Blur(short id, string text)
        {
            try
            {
                var initObject = this.InitObject;
                initObject.Frm_Anu_Vi.Tx_NroOpe[id].Text = text;
                ftService.AnulacionPlanillaVisibleImport_Tx_NroOpe_Blur(initObject, id);
                this.InitObject = initObject;
            }
            catch (Exception ex)
            {
            }
            var model = new AnulacionPlanillaVisibleImportViewModel(this.InitObject.Frm_Anu_Vi);
            return Json(model, JsonRequestBehavior.AllowGet);
        }




        public ActionResult ReemAnulacionPlanillaVisibleImport()
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            ftService.ReemAnulacionPlanillaVisibleImportInit(this.InitObject);
            ReemAnulacionPlanillaVisibleImportViewModel rapiivm = new ReemAnulacionPlanillaVisibleImportViewModel(this.InitObject.Frm_Rem_PVI);
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            return View(rapiivm);
        }

        [HttpPost]
        public ActionResult ReemAnulacionPlanillaVisibleImport(ReemAnulacionPlanillaVisibleImportViewModel rapiivm, string btnComando)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            rapiivm.Update(this.InitObject.Frm_Rem_PVI);

            if (btnComando == "Ok1")
            {
                ftService.ReemAnulacionPlanillaVisibleImport_OkDecClick(this.InitObject);
            }
            else if (btnComando == "Aceptar")
            {
                ftService.ReemAnulacionPlanillaVisibleImport_AceptarClick(this.InitObject);
                if (InitObject.MODANUVI.Vx_AnuReem.AcepRee == -1)
                {
                    return new RedirectResult("~/Fundtransfer");
                }
            }
            else if (btnComando == "Cancelar")
            {
                ftService.ReemAnulacionPlanillaVisibleImport_CancelClick(this.InitObject);
                return new RedirectResult("~/Fundtransfer");
            }
            else if (btnComando == "Ok2")
            {
                ftService.ReemAnulacionPlanillaVisibleImport_OkFinalClick(this.InitObject);
            }


            rapiivm = new ReemAnulacionPlanillaVisibleImportViewModel(this.InitObject.Frm_Rem_PVI);
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon + " " + this.InitObject.CaptionAddition;
            return View(rapiivm);
        }

        #endregion

        #region NEMONICO CUENTA

        public JsonResult NEMCTA_Form_Load()
        {
            Frm_Cta_Logic.Form_Load(this.InitObject);
            return Json(this.InitObject.Frm_Cta.Lista.Items.Select(x => x.columns), JsonRequestBehavior.AllowGet);
        }

        public ActionResult NEMCTA_bot_acep_Click(string num, string nem, string desc)
        {
            return Rutear(() =>
            {
                Frm_Cta_Logic.bot_acep_Click(this.InitObject, num, nem, desc);
            }, null);
        }

        public ActionResult NEMCTA_bot_canc_Click()
        {
            return Rutear(() =>
            {
                Frm_Cta_Logic.bot_canc_Click(this.InitObject);
            }, null);
        }
        #endregion

        #region PLANILLAS DE TRANSFERENCIA
        public ActionResult PlanillasTransferencia()
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            ftService.PlanillasTransferenciaInit(this.InitObject);
            PlanillasTransferenciaViewModel ptvm = new PlanillasTransferenciaViewModel(this.InitObject.Frm_ChVrf, InitObject.Mdi_Principal.MESSAGES);
            ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon;
            return View(ptvm);
        }

        [HttpPost]
        public ActionResult PlanillasTransferencia(PlanillasTransferenciaViewModel ptvm, string btnComando)
        {
            return Rutear(() =>
            {
                this.InitObject.Mdi_Principal.MESSAGES.Clear();
                ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

                ptvm.Update(this.InitObject.Frm_ChVrf);
                this.InitObject.FormularioQueAbrir = null;

                #region Llamada a Botones
                if (btnComando == "Aceptar")
                {
                    ftService.PlanillasTransferencia_AceptarClick(this.InitObject);
                    if (InitObject.ModChVrf.AceptoPantallaChVrf == -1)
                    {
                        this.InitObject.FormularioQueAbrir = "DestinoFondos";
                        InitObject.Frm_Destino_Fondos.Boton[0].Enabled = true;
                    }
                }
                else if (btnComando == "Cancelar")
                {
                    ftService.PlanillasTransferencia_CancelarClick(this.InitObject);
                    if (InitObject.ModChVrf.AceptoPantallaChVrf == -1)
                    {
                        this.InitObject.FormularioQueAbrir = "DestinoFondos";
                    }
                }
                #endregion
            },
            () =>
            {
                ptvm = new PlanillasTransferenciaViewModel(this.InitObject.Frm_ChVrf, this.InitObject.Mdi_Principal.MESSAGES);
                ViewBag.Caption = this.InitObject.MODGCVD.VgCvd.OpeCon;
                ptvm._MensajesDeError = this.InitObject.Mdi_Principal.MESSAGES.Select(x => x.Text).ToList();
                return View(ptvm);
            });
        }

        [HttpPost]
        public ActionResult PlanillasTransferencia_SeleccionarMoneda_Click(int selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            InitObject.Frm_ChVrf.Cbo_Moneda.ListIndex = selectedValue;

            ftService.PlanillasTransferencia_SeleccionarMoneda_Click(this.InitObject);
            var model = new PlanillasTransferenciaViewModel(this.InitObject.Frm_ChVrf, this.InitObject.Mdi_Principal.MESSAGES);
            model.Cbo_Moneda = this.InitObject.Frm_ChVrf.Cbo_Moneda;
            return Json(new
            {
                model = model
            });
        }

        [HttpPost]
        public ActionResult PlanillasTransferencia_SeleccionarTcp_Click(int selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            InitObject.Frm_ChVrf.Cbo_CptoPln.ListIndex = selectedValue;
            var model = new PlanillasTransferenciaViewModel(this.InitObject.Frm_ChVrf, this.InitObject.Mdi_Principal.MESSAGES);
            model.Cbo_CptoPln = this.InitObject.Frm_ChVrf.Cbo_CptoPln;
            return Json(new
            {
                model = model
            });
        }

        [HttpPost]
        public ActionResult PlanillasTransferencia_OKClick()
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            ftService.PlanillasTransferencia_OKClick(this.InitObject);
            var model = new PlanillasTransferenciaViewModel(this.InitObject.Frm_ChVrf, this.InitObject.Mdi_Principal.MESSAGES);
            model._MensajesDeError = this.InitObject.Mdi_Principal.MESSAGES.Select(x => x.Text).ToList();
            return Json(new
            {
                model = model
            });
        }

        [HttpPost]
        public ActionResult PlanillasTransferencia_EliminarClick()
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            ftService.PlanillasTransferencia_EliminarClick(this.InitObject);
            var model = new PlanillasTransferenciaViewModel(this.InitObject.Frm_ChVrf, this.InitObject.Mdi_Principal.MESSAGES);
            model._MensajesDeError = this.InitObject.Mdi_Principal.MESSAGES.Select(x => x.Text).ToList();
            return Json(new
            {
                model = model
            });
        }

        [HttpPost]
        public ActionResult PlanillasTransferencia_SeleccionarPais_Click(int selectedValue)
        {
            InitObject.Frm_ChVrf.CbPais.ListIndex = selectedValue;
            var model = new PlanillasTransferenciaViewModel(this.InitObject.Frm_ChVrf, this.InitObject.Mdi_Principal.MESSAGES);
            model.CbPais = this.InitObject.Frm_ChVrf.CbPais;
            return Json(new
            {
                model = model
            });
        }

        [HttpPost]
        public ActionResult PlanillasTransferencia_ListaPlanillas_Click(int selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            InitObject.Frm_ChVrf.ListaPlanillas.ListIndex = selectedValue;
            ftService.PlanillasTransferencia_ListaPlanillas_Click(this.InitObject);
            var model = new PlanillasTransferenciaViewModel(this.InitObject.Frm_ChVrf, this.InitObject.Mdi_Principal.MESSAGES);
            model.ListaPlanillas = this.InitObject.Frm_ChVrf.ListaPlanillas;
            model._MensajesDeError = this.InitObject.Mdi_Principal.MESSAGES.Select(x => x.Text).ToList();
            return Json(new
            {
                model = model
            });
        }

        [HttpPost]
        public ActionResult PlanillasTransferencia_Tx_Monto_LostFocus_Blur(string text)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            var initObject = this.InitObject;
            initObject.Frm_ChVrf.Tx_Monto.Text = text;
            ftService.PlanillasTransferencia_Tx_Monto_LostFocus_Click(this.InitObject);
            var model = new PlanillasTransferenciaViewModel(this.InitObject.Frm_ChVrf, this.InitObject.Mdi_Principal.MESSAGES);
            model._MensajesDeError = this.InitObject.Mdi_Principal.MESSAGES.Select(x => x.Text).ToList();
            return Json(new
            {
                model = model
            });
        }
        #endregion

        #region "Impresion Planillas"

        //"Impresion Planilla Invisible"
        public ActionResult ImpresionPlanillaInvisible()
        {
            return Rutear(() =>
            {
                ModelState.Clear();
                this.InitObject.FormularioQueAbrir = null;
                ftService.ImpresionPlanillaInvisibleInit();
            },
            () =>
            {
                PlanillaInvisible pl = new PlanillaInvisible();
                pl.DatPrt1 = "Aguas CCU-Nestle Chile S.A.";
                pl.DatPrt2 = "Vitacura No.2670 Piso 23";
                pl.Palabras = new List<string>();
                pl.VMnd_Mnd_MndNom = "Euro";
                pl.VPai_Pai_PaiNom = "Alemania";
                pl.VPbc_Pbc_PbcDes = "SANTIAGO";
                pl.VplisApcNum = string.Empty;
                pl.Vplis_AnuFec = string.Empty;
                pl.Vplis_AnuNum = string.Empty;
                pl.Vplis_AnuPbc = string.Empty;
                pl.Vplis_ApcFec = string.Empty;
                pl.Vplis_ApcPbc = string.Empty;
                pl.Vplis_ApcTip = string.Empty;
                pl.Vplis_CodAdn = string.Empty;
                pl.Vplis_codcom = "20.00.00";
                pl.Vplis_CodEOR = string.Empty;
                pl.Vplis_CodMndBC = "142";
                pl.Vplis_CodOci = 210;
                pl.Vplis_codpai = "563";
                pl.Vplis_Concep = "000";
                pl.Vplis_Desacu = new List<string>();
                pl.Vplis_DieFec = string.Empty;
                pl.Vplis_DieNum = string.Empty;
                pl.Vplis_DiePbc = string.Empty;
                pl.Vplis_DocExt = string.Empty;
                pl.Vplis_DocNac = string.Empty;
                pl.Vplis_FecCr = string.Empty;
                pl.Vplis_FecDeb = string.Empty;
                pl.Vplis_FecDec = string.Empty;
                pl.Vplis_FecPli = "11/03/2015";
                pl.Vplis_MndCre = string.Empty;
                pl.Vplis_MndCreRepeat = string.Empty;
                pl.Vplis_MtoCre = string.Empty;
                pl.Vplis_MtoDol = "239,94";
                pl.Vplis_MtoNac = string.Empty;
                pl.Vplis_MtoOpe = "214,15";
                pl.Vplis_Mtopar = "0,8925";
                pl.Vplis_NumCre = string.Empty;
                pl.Vplis_numdec = string.Empty;
                pl.Vplis_NumPli = "154029";
                pl.Vplis_PlzBcc = "25";
                pl.Vplis_rutcli = "076007212-5";
                pl.Vplis_TipCam = string.Empty;
                pl.Vplis_TipPln = 9;
                pl.VTcp_DesTcp = string.Empty;

                return View(pl);
            });
        }
        //"Impresion Planilla Reemplazo"
        public ActionResult ImpresionPlanillaReemplazo()
        {

            return Rutear(() =>
            {
                ModelState.Clear();
                this.InitObject.FormularioQueAbrir = null;
                ftService.ImpresionPlanillaReemplazoInit();
            },
            () =>
            {
                PlanillaReemplazo pr = new PlanillaReemplazo();
                pr.Mensaje = string.Empty;
                pr.Vx_PReem_NumPla = "409020";
                pr.Vx_PReem_Codigo = "205000";
                pr.Vx_PReem_FecVen = "11/03/2015";
                pr.Vx_PReem_NomPlz = "SANTIAGO";
                pr.Vx_PReem_CodBch = "25";
                pr.Vx_PReem_NomImp = "Cervecera Ccu Chile Ltda.";
                pr.Vx_PReem_RutImp = "96.989.120-4";
                pr.Vx_PReem_NomPai = "Inglaterra";
                pr.Vx_PReem_CodPPa = "510";
                pr.Vx_PReem_NomMon = "Euro";
                pr.Vx_PReem_CodMPa = "142";
                pr.Vx_PReem_NumIdi = "000000";
                pr.Vx_PReem_FecIdi = string.Empty;
                pr.Vx_PReem_CodPlz = "25";
                pr.Vx_PReem_CodPag = "32";
                pr.Vx_PReem_NumCon = string.Empty;
                pr.Vx_PReem_FecCon = string.Empty;
                pr.Vx_PReem_FecVto = string.Empty;
                pr.Vx_PReem_MtoFob = string.Empty;
                pr.Vx_PReem_MtoFle = string.Empty;
                pr.Vx_PReem_MtoSeg = string.Empty;
                pr.Vx_PReem_MtoCif = string.Empty;
                pr.Vx_PReem_MtoInt = string.Empty;
                pr.Vx_PReem_GasBan = string.Empty;
                pr.Vx_PReem_TotOri = string.Empty;
                pr.Vx_PReem_CifDol = string.Empty;
                pr.Vx_PReem_TotDol = string.Empty;
                pr.Vx_PReem_TipCam = string.Empty;
                pr.Vx_PReem_ParPag = string.Empty;
                pr.Vx_PReem_NumCua = string.Empty;
                pr.Vx_PReem_numcuo = string.Empty;
                pr.Vx_PReem_NumAcu = string.Empty;
                pr.Vx_PReem_Acuer1 = string.Empty;
                pr.Vx_PReem_Acuer2 = string.Empty;
                pr.Vx_PReem_FecDeb = string.Empty;
                pr.Vx_PReem_DocChi = string.Empty;
                pr.Vx_PReem_DocExt = string.Empty;
                pr.Vx_PReem_NumPln_R = string.Empty;
                pr.Vx_PReem_FecPln_R = string.Empty;
                pr.Vx_PReem_CodPlz_R = string.Empty;
                pr.Vx_PReem_CodEnt_R = string.Empty;
                pr.Vx_PReem_NumInf_R = string.Empty;
                pr.Vx_PReem_FecInf_R = string.Empty;
                pr.Vx_PReem_PlzInf_R = string.Empty;
                pr.Vx_PReem_NumCon_R = string.Empty;
                pr.Vx_PReem_FecCon_R = string.Empty;
                pr.Vx_PReem_ObsDec = string.Empty;
                pr.Vx_PReem_observ = string.Empty;
                pr.Vx_PReem_ObsCob = string.Empty;
                return View(pr);
            });
        }
        //"Impresion Planilla VisibleExportacion"
        public ActionResult ImpresionPlanillaVisibleExportacion()
        {
            return Rutear(() =>
            {
                ModelState.Clear();
                this.InitObject.FormularioQueAbrir = null;
                ftService.ImpresionPlanillaVisibleExportacionInit();
            },
            () =>
            {
                PlanillaVisibleExportacion pve = new PlanillaVisibleExportacion();
                pve.Detalles = new List<Detalle>();
                pve.NroAcs = string.Empty;
                pve.Pl_Cif_Dolar = string.Empty;
                pve.Pl_Cif_Origen = string.Empty;
                pve.Pl_CodBCCh = string.Empty;
                pve.Pl_Codigo = string.Empty;
                pve.Pl_Cod_FormaPago = string.Empty;
                pve.Pl_cod_moneda = "013";
                pve.Pl_Cod_Paispago = "511";
                pve.Pl_Cod_Plaza = "25";
                pve.Pl_fecha_anulacion = string.Empty;
                pve.Pl_fecha_conocimiento = string.Empty;
                pve.Pl_fecha_debito = string.Empty;
                pve.Pl_fecha_idi = string.Empty;
                pve.Pl_fecha_vencimiento = string.Empty;
                pve.Pl_fecha_venta = "11/03/2015";
                pve.Pl_Flete_Origen = string.Empty;
                pve.Pl_Fob_Origen = string.Empty;
                pve.Pl_Gastos_Banco = string.Empty;
                pve.Pl_HastaFob = string.Empty;
                pve.Pl_Interes_Origen = string.Empty;
                pve.Pl_LineaObs1 = string.Empty;
                pve.Pl_LineaObs2 = string.Empty;
                pve.Pl_LineaObs3 = string.Empty;
                pve.Pl_Mercaderia = string.Empty;
                pve.Pl_NDoc1 = string.Empty;
                pve.Pl_NDoc2 = string.Empty;
                pve.Pl_nombre_moneda = "DOLAR USA";
                pve.Pl_NomImport = "Comercializadora Forestal Ltda.";
                pve.Pl_num_acuerdos = string.Empty;
                pve.Pl_num_conocimiento = string.Empty;
                pve.Pl_Num_Cuadro = string.Empty;
                pve.Pl_num_cuotas = string.Empty;
                pve.Pl_num_idi = string.Empty;
                pve.Pl_num_planilla = "069650-7";
                pve.Pl_ObsCobranza = string.Empty;
                pve.Pl_ObsDecl = string.Empty;
                pve.Pl_ObsMerma = string.Empty;
                pve.Pl_ObsParidad = string.Empty;
                pve.Pl_Paispago = string.Empty;
                pve.Pl_Paridad = string.Empty;
                pve.Pl_paridad_anulacion = string.Empty;
                pve.Pl_rut = "077486860-7";
                pve.Pl_Seguro_Origen = string.Empty;
                pve.Pl_tipo_cambio = string.Empty;
                pve.Pl_total_anulacion = string.Empty;
                pve.Pl_Total_Dolar = string.Empty;
                pve.Pl_Total_Origen = string.Empty;
                return View(pve);
            });
        }
        #endregion

        public void Dispose()
        {
            if (ftService != null)
            {
                ftService.Dispose();
            }
        }
    }
}