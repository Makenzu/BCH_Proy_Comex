using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.BL.XGPL;
using BCH.Comex.UI.Web.Areas.Planillas.Models;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using BCH.Comex.Common.Tracing;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Planillas.Controllers
{
    [AuthorizeOrForbidden(Roles = PlanillasControllerAppRole)]
    public class PlanillasController : PlanillasBaseController
    {
        private UsuarioEspecialista usuarioEspecialista = null;
        private const decimal PlanillaVisibleImportacion = 1;
        private const decimal PlanillaEstadisticaImportacion = 2;

        private const string PlanillasControllerAppRole = "COMEX_GENERAL_XGPL";
        private XgplService bl
        {
            get
            {
                return new XgplService(HttpContext.GetCurrentUser().GetDatosUsuario());
            }
        }

        static PlanillasController()
        {
            new PortalService().RegisterApp("XGPL", "Administrador de Planillas Banco Central", "Generales", PlanillasControllerAppRole, "COMEX_GRP_GENERAL", "Planillas");
        }

        private UsuarioEspecialista UsuarioEspecialista()
        {
            if (usuarioEspecialista == null)
            {
                usuarioEspecialista = bl.ObtenerUsuarioEstacion();
            }
            return usuarioEspecialista;
        }

        [AuthorizeOrForbidden(Roles = PlanillasControllerAppRole)]
        public ActionResult Index()        
        {
            using (var tracer = new Tracer(PlanillasControllerAppRole))
            {
                var usuarioStr = HttpContext.GetCurrentUser().GetDatosUsuario().Identificacion_CCtUsr;
                string centroDeCosto = usuarioStr.Substring(0, 3);
                string codigoUsuario = usuarioStr.Substring(3, 2);

                MODGUSR.ValidarInicioFinDia(centroDeCosto, codigoUsuario, bl);
                ViewBag.UsuarioEspecialista = UsuarioEspecialista();
                ViewBag.TipoPlanilla = -1;
                var model = new ListadoPlanillasViewModel
                {
                    CentroCosto = centroDeCosto,
                    CodigoUsuario = codigoUsuario,
                    NombreUsuario = ViewBag.UsuarioEspecialista.Nombre,
                    FechaIngreso = DateTime.Today
                };

                return View(model);
            }
        }

        public ActionResult ListadoPlanillasVisiblesExportacion(ListadoPlanillasViewModel model)
        {
            ViewBag.UsuarioEspecialista = UsuarioEspecialista();
            ViewBag.TipoPlanilla = 0;
            if (ModelState.IsValid)
            {
                model.Tipo = FrmgPlv.TipoListado.tplVisibleExportacion;
                model.NombreUsuario = NombreUsuario(model);
                model.Detalle = GetListadoPlanillasVisiblesExportacion(model.CentroCosto, model.CodigoUsuario, model.FechaIngreso);
                AlmacenarNavegacionEnSesion(model);
            }
            return ListadoView(model);
        }

        public ActionResult ListadoPlanillasVisiblesImportacionEndosadas(ListadoPlanillasViewModel model)
        {
            ViewBag.UsuarioEspecialista = UsuarioEspecialista();
            ViewBag.TipoPlanilla = 3;
            if (ModelState.IsValid)
            {
                model.Tipo = FrmgPlv.TipoListado.tplVisibleImportacionEndosadas;
                model.NombreUsuario = NombreUsuario(model);
                model.Detalle = GetListadoPlanillasVisiblesImportacionEndosadas(model.CentroCosto, model.CodigoUsuario);
                AlmacenarNavegacionEnSesion(model);
            }
            return ListadoView(model);
        }

        public ActionResult ListadoPlanillasInvisibles(ListadoPlanillasViewModel model)
        {
            ViewBag.UsuarioEspecialista = UsuarioEspecialista();
            ViewBag.TipoPlanilla = 1;
            if (ModelState.IsValid)
            {
                model.Tipo = FrmgPlv.TipoListado.tplInvisibleExportacion;
                model.NombreUsuario = NombreUsuario(model);
                model.Detalle = GetListadoPlanillasInvisibles(model.CentroCosto, model.CodigoUsuario, model.FechaIngreso);
                AlmacenarNavegacionEnSesion(model);
            }
            return ListadoView(model);
        }

        public ActionResult ListadoPlanillasAnuladas(ListadoPlanillasViewModel model)
        {
            ViewBag.UsuarioEspecialista = UsuarioEspecialista();
            ViewBag.TipoPlanilla = 2;
            if (ModelState.IsValid)
            {
                model.Tipo = FrmgPlv.TipoListado.tplAnuladaExportacion;
                model.NombreUsuario = NombreUsuario(model);
                model.Detalle = GetListadoPlanillasAnuladas(model.CentroCosto, model.CodigoUsuario, model.FechaIngreso);
                AlmacenarNavegacionEnSesion(model);
            }

            return ListadoView(model); ;
        }

        public ActionResult ListadoPlanillasVisiblesImportacion(ListadoPlanillasViewModel model)
        {
            ViewBag.UsuarioEspecialista = UsuarioEspecialista();
            ViewBag.TipoPlanilla = 3;
            if (ModelState.IsValid)
            {
                model.Tipo = FrmgPlv.TipoListado.tplVisibleImportacion;
                model.NombreUsuario = NombreUsuario(model);
                model.Detalle = GetListadoPlanillasVisiblesImportacion(model.FechaIngreso, model.CentroCosto, model.CodigoUsuario, PlanillaVisibleImportacion);
                AlmacenarNavegacionEnSesion(model);
            }
            return ListadoView(model); ;
        }

        private IQueryable<ListadoPlanillasItemViewModel> GetListadoPlanillasVisiblesImportacion(DateTime fechaVenta, string codigoCentroCosto, string codigoUsuario, decimal tipo)
        {
            var query =  bl.Sce_Plan_S03(fechaVenta, codigoCentroCosto, codigoUsuario, tipo);
            return query.Select(
                x => new ListadoPlanillasItemViewModel()
                {
                    Presentacion = x.num_presen.ToString(),
                    Operacion = string.Format("{0}-{1}-{2}-{3}-{4}", x.cent_costo, x.id_product, x.id_especia, x.id_empresa, x.id_cobranz),
                    CodigoUsuario = string.Format("{0}-{1}", x.cent_costo, x.id_especia),
                    FechaPresentacion = x.fechaventa,
                    CodigoMoneda = x.codmone,
                    MontoCIF = x.mtototal.FormatoDecimal(),
                    Estado = decimal.ToInt32(x.estado),
                    EstadoString = MODXPLN1.GetEstadoPlanilla(decimal.ToInt32(x.estado)),
                    Planilla = "IMP",
                    QueryString = new
                    {
                        codigoCentroCosto = x.cent_costo,
                        codigoProducto = x.id_product,
                        codigoEspecialista = x.id_especia,
                        codigoEmpresa = x.id_empresa,
                        codigoCobranza = x.id_cobranz,
                        numeroPlanilla = x.num_presen.ToString(),
                        fechaVenta = x.fechaventa.ToString("yyyy-MM-dd")
                    }

                });
        }

        private string NombreUsuario(ListadoPlanillasViewModel model)
        {
            if (model != null)
            {
                return bl.GetNombreUsuario(model.CentroCosto, model.CodigoUsuario);
            }
            return "";
        }

        public ActionResult ObtenerUsuarioJSON(string CentroCosto, string CodigoUsuario)
        {
            ViewBag.UsuarioEspecialista = UsuarioEspecialista();
            var res = bl.Sce_Usr_S02(CentroCosto, CodigoUsuario).FirstOrDefault();
            if (res != null)
            {
                return new JsonNetResult()
                {
                    Data = res
                };
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        private IQueryable<ListadoPlanillasItemViewModel> GetListadoPlanillasInvisibles(string CentroCosto, string CodigoUsuario, DateTime? FechaIngreso)
        {
            return bl.Sce_Pli_S05(CentroCosto, CodigoUsuario, FechaIngreso.Value).Select(
                                    x => new ListadoPlanillasItemViewModel()
                                    {
                                        CodigoMoneda = x.codmnd,
                                        Estado = decimal.ToInt32(x.estado),
                                        EstadoString = MODXPLN1.EstadoPlanilla[decimal.ToInt32(x.estado)],
                                        FechaPresentacion = x.fecpli,
                                        MontoCIF = x.mtoope.FormatoDecimal(),
                                        Operacion = FrmgPlv.FormatoOrden(x.operacion),
                                        Presentacion = x.numpli,
                                        Planilla = x.tippln.ToString(),
                                        CodigoUsuario = UsuarioEspecialista().CentroCosto + "-" + UsuarioEspecialista().Especialista,
                                        QueryString = new
                                        {
                                            fechaPresentacion = x.fecpli,
                                            numeroPresentacion = x.numpli
                                        }
                                    });
        }

        private IQueryable<ListadoPlanillasItemViewModel> GetListadoPlanillasVisiblesExportacion(string CentroCosto, string CodigoUsuario, DateTime? FechaIngreso)
        {
            return bl.Sce_Xplv_S08(CentroCosto, CodigoUsuario, FechaIngreso.Value).Select(
                                    x => new ListadoPlanillasItemViewModel()
                        {
                            CodigoMoneda = x.codmnd,
                            Estado = decimal.ToInt32(x.estado),
                            EstadoString = MODXPLN1.GetEstadoPlanilla(decimal.ToInt32(x.estado)),
                            FechaPresentacion = x.fecpre,
                            MontoCIF = x.mtoliq.FormatoDecimal(),
                            Operacion = FrmgPlv.FormatoOrden(x.operacion),
                            Presentacion = x.numpre,
                            Planilla = x.tippln.ToString(),
                            CodigoUsuario = UsuarioEspecialista().CentroCosto + "-" + UsuarioEspecialista().Especialista,
                            QueryString = new
                            {
                                fechaPresentacion = x.fecpre,
                                numeroPresentacion = x.numpre
                            }
                        });
        }

        private IQueryable<ListadoPlanillasItemViewModel> GetListadoPlanillasVisiblesImportacionEndosadas(string CentroCosto, string CodigoUsuario)
        {
            return bl.Sce_Plan_S07(CentroCosto, CodigoUsuario).Select(
                x => new ListadoPlanillasItemViewModel()
                {
                    Presentacion = x.num_presen.ToString(),
                    Operacion = string.Format("{0}-{1}-{2}-{3}-{4}", x.cent_costo, x.id_product, x.id_especia, x.id_empresa, x.id_cobranz),
                    CodigoUsuario = string.Format("{0}-{1}", x.cent_costo, x.id_especia),
                    FechaPresentacion = x.fechaventa,
                    CodigoMoneda = x.codmone,
                    MontoCIF = x.mtototal.FormatoDecimal(),
                    Estado = decimal.ToInt32(x.estado),
                    EstadoString = MOD_PLAV.EstadoPlanilla[decimal.ToInt32(x.estado)],
                    QueryString = new
                    {
                        codigoCentroCosto = x.cent_costo,
                        codigoProducto = x.id_product,
                        codigoEspecialista = x.id_especia,
                        codigoEmpresa = x.id_empresa,
                        codigoCobranza = x.id_cobranz,
                        numeroPlanilla = x.num_presen.ToString(),
                        fechaVenta = x.fechaventa
                    }

                });
        }

        private IQueryable<ListadoPlanillasItemViewModel> GetListadoPlanillasAnuladas(string CentroCosto, string CodigoUsuario, DateTime? FechaIngreso)
        {
            return bl.Sce_XAnu_S01(CentroCosto, CodigoUsuario, FechaIngreso.Value).Select(
                        x => new ListadoPlanillasItemViewModel()
                        {
                            CodigoMoneda = x.codmnd,
                            Estado = decimal.ToInt32(x.estado),
                            EstadoString = MODXPLN1.EstadoPlanilla[decimal.ToInt32(x.estado)],
                            FechaPresentacion = x.fecpre,
                            MontoCIF = x.mtoanu.FormatoDecimal(),
                            Operacion = FrmgPlv.FormatoOrden(x.operacion),
                            Presentacion = x.numpre,
                            Planilla = x.tippln.ToString(),
                            CodigoUsuario = UsuarioEspecialista().CentroCosto + "-" + UsuarioEspecialista().Especialista,
                            QueryString = new
                            {
                                fechaPresentacion = x.fecpre,
                                numeroPresentacion = x.numpre
                            }
                        });
        }

        public ActionResult Volver()
        {
            ViewBag.UsuarioEspecialista = UsuarioEspecialista();
            var model = Session["navegacionPlanillas"] as ListadoPlanillasViewModel;
            if (model != null)
            {
                // Llenar listado
                return Listado(model);
            }
            return View("Index");
        }

        private ActionResult ListadoView(ListadoPlanillasViewModel model)
        {
            ViewBag.UsuarioEspecialista = UsuarioEspecialista();
            return View("Index", model);
        }

        private ActionResult Listado(ListadoPlanillasViewModel nav)
        {
            ViewBag.UsuarioEspecialista = UsuarioEspecialista();
            if (nav != null)
            {
                switch (nav.Tipo)
                {
                    case FrmgPlv.TipoListado.tplVisibleExportacion:
                        return ListadoPlanillasVisiblesExportacion(nav);
                    case FrmgPlv.TipoListado.tplInvisibleExportacion:
                        return ListadoPlanillasInvisibles(nav);
                    case FrmgPlv.TipoListado.tplAnuladaExportacion:
                        return ListadoPlanillasAnuladas(nav);
                    case FrmgPlv.TipoListado.tplVisibleImportacionEndosadas:
                        return ListadoPlanillasVisiblesImportacionEndosadas(nav);
                    case FrmgPlv.TipoListado.tplVisibleImportacion:
                        return ListadoPlanillasVisiblesImportacion(nav);
                    default:
                        return Index();
                }
            }
            return Index();
        }

        private void AlmacenarNavegacionEnSesion(ListadoPlanillasViewModel model)
        {
            Session["navegacionPlanillas"] = new ListadoPlanillasViewModel()
            {
                CentroCosto = model.CentroCosto,
                Tipo = model.Tipo,
                CodigoUsuario = model.CodigoUsuario,
                FechaIngreso = model.FechaIngreso
            };
        }

        [HttpPost]
        public ActionResult EliminarPlanilla(ListadoPlanillasItemViewModel planilla)
        {
            try
            {
                switch (planilla.Tipo)
                {
                    case 0:
                        //sce_xplv
                         bl.EliminarPlanillaVisibleExportacion(planilla.Presentacion, planilla.FechaPresentacion);
                        break;
                    case 1:
                        //sce_pli
                        bl.EliminarPlanillaInvisibleExportacion(planilla.Presentacion, planilla.FechaPresentacion);
                        break;
                    case 2:
                        //sce_xanu
                        bl.EliminarPlanillaAnuladaExportacion(planilla.Presentacion, planilla.FechaPresentacion);
                        break;
                    case 3:
                        //sce_plan
                        bl.EliminarPlanillaVisibleImportacion(Convert.ToDecimal(planilla.Presentacion), planilla.FechaPresentacion);
                        break;
                }

                return Json(new { success = true, responseText = "Planilla eliminada" }, JsonRequestBehavior.AllowGet);
                
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

    }
}