using BCH.Comex.Common;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.BL.XGFD;
using BCH.Comex.Core.Entities.Cext01.FinDia;
using BCH.Comex.UI.Web.Areas.FinDia.Models;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Filters;
using BCH.Comex.UI.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BCH.Comex.UI.Web.Areas.FinDia.Controllers
{
    [LoggingFilter]
    public class InicioController : BaseController
    {
        private FinDiaService XGFDService;
        
        static InicioController()
        {
            new PortalService().RegisterApp("XGFD", "Fin de Día", "Generales", 
                Constantes.AppRoles.FinDiaAppRole, "COMEX_GRP_GENERAL", "FinDia"); ;
        }

        public InicioController()
        {
            this.XGFDService = new FinDiaService();
        }

        // GET: FinDia/Inicio
        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.FinDiaAppRole)]        
        public ActionResult Index()
        {            
            InicioViewModel o = new InicioViewModel();
            List<UI_Message> listaErrores = new List<UI_Message>();
            this.Globales =  this.fdService.Iniciar(HttpContext.GetCurrentUser().GetDatosUsuario(), listaErrores);

            if (listaErrores.Count() > 0)
            {
                o.update(listaErrores);
                return View(o);
            }
            
            string formulario = "";
            this.fdService.FinDiaIniciar(Globales.MODCUACC, Globales.CCIRLLVR, listaErrores, Globales.Usuario, ref formulario);

            if(!string.IsNullOrEmpty(formulario))
            {
                return RedirectToAction(formulario, "Aceptacion", new
                {
                    centroCosto = Globales.Usuario.Identificacion_CentroDeCostosImpersonado,
                    especialista = Globales.Usuario.Identificacion_IdEspecialistaImpersonado,
                    Errores = listaErrores[0].Text
                });
                //AceptacionController acepController = new AceptacionController();
                //return acepController.Aceptacion( globales.Usuario.Identificacion_CentroDeCostosImpersonado,
                //                                  globales.Usuario.Identificacion_IdEspecialistaImpersonado,
                //                                   globales.CCIRLLVR,listaErrores);
            }
            //MODGUSR.UsrEsp.CentroCosto + "-" + MODGUSR.UsrEsp.Especialista + 9.Char() + MODGUSR.UsrEsp.nombre + 9.Char() + MODGUSR.UsrEsp.CostoSuper + "-" + MODGUSR.UsrEsp.Id_Super + 9.Char() + MODGUSR.UsrEsp.Seccion + 9.Char() + MODGUSR.UsrEsp.Ciudad;
            o.addUsuario(Globales.MODGUSR.UsrEsp.CentroCosto + "-" + Globales.MODGUSR.UsrEsp.Especialista,Globales.MODGUSR.UsrEsp.nombre,Globales.MODGUSR.UsrEsp.CostoSuper + "-" + Globales.MODGUSR.UsrEsp.Id_Super, Globales.MODGUSR.UsrEsp.Seccion, Globales.MODGUSR.UsrEsp.Ciudad);

            return View(o);
        }

        //public JsonResult GetContabilidadVigente(string CentroCosto, string Especialidad)
        //{

        //    var result = this.fdService.GetContabilidadVigente(CentroCosto, Especialidad);
        //    return Json(result.Select(i => new
        //    {
        //        NumeroOperacion=i.codcct + "-" + i.codpro + "-" + i.codesp + "-" + i.codofi + "-" + i.codope,             
        //        NumeroReporte = i.nrorpt,
        //        FechaContable = i.fecmov.ToString("dd-MM-yyyy")
        //    }).ToList(), JsonRequestBehavior.AllowGet);
        //}

        public JsonResult btnBajarDatos_Click(bool ChkImpresionListado)
        {
            List<UI_Message> listaMensaje = new List<UI_Message>();
            this.Globales.MODFDIA.ListaReportes.Clear();
            this.Globales.MODFDIA.DetallesReportes.Clear();

            Globales.MODFDIA.ChkImpresionListado = ChkImpresionListado;

            this.fdService.btnBajarDatos_Click(this.Globales.MODGUSR, listaMensaje, this.Globales.MODFDIA);
            bool hayProblemas = (listaMensaje.Count > 0) || (this.Globales.MODFDIA.ListaReportes.Count > 0) ? true : false;

            return Json(new { hayProblemas = hayProblemas, ListaProblemas = listaMensaje, ListaReportes = this.Globales.MODFDIA.ListaReportes });
        }

        public JsonResult btnBajarDatos_Click_2()
        {
            List<UI_Message> listaMensaje = new List<UI_Message>();
            this.Globales.MODFDIA.ListaReportes.Clear();

            this.fdService.btnBajarDatos_Click_2(this.Globales.MODGUSR,listaMensaje,this.Globales.MODFDIA,this.Globales.MODCUACC);

            bool hayProblemas = (listaMensaje.Where(c => c.IsError).Count() > 0 ) || (this.Globales.MODFDIA.ListaReportes.Count > 0) ? true : false;

            return Json(new { hayProblemas = hayProblemas, ListaProblemas = listaMensaje, ListaReportes = this.Globales.MODFDIA.ListaReportes });
        }

        public JsonResult btnBajarDatos_Click_3()
        {
            List<UI_Message> listaMensaje = new List<UI_Message>();
            this.Globales.MODFDIA.ListaReportes.Clear(); 
            this.Globales.MODFDIA.ListaConfirmaciones.Clear();

            this.fdService.btnBajarDatos_Click_3(this.Globales.MODGUSR, listaMensaje, this.Globales.MODFDIA, this.Globales.MODCUACC);

            bool hayProblemas = (listaMensaje.Where(c => c.IsError).Count() > 0) || (this.Globales.MODFDIA.ListaReportes.Count > 0) || (this.Globales.MODFDIA.ListaConfirmaciones.Count > 0) ? true : false;

            return Json(new {   hayProblemas = hayProblemas,
                                ListaProblemas = listaMensaje, 
                                ListaReportes = this.Globales.MODFDIA.ListaReportes, 
                                ListaConfirmaciones = this.Globales.MODFDIA.ListaConfirmaciones,
                                imprimirListado = this.Globales.MODFDIA.impresionListado.Length > 0  && this.Globales.MODFDIA.ChkImpresionListado
            });
        }

        public JsonResult btnBajarDatos_Click_4()
        {
            List<UI_Message> listaMensaje = new List<UI_Message>();
            this.Globales.MODFDIA.ListaReportes.Clear();
            this.Globales.MODFDIA.ListaConfirmaciones.Clear();

            this.fdService.btnBajarDatos_Click_4(this.Globales.MODGUSR, listaMensaje, this.Globales.MODFDIA, this.Globales.MODCUACC);

            bool hayProblemas = (listaMensaje.Where(c => c.IsError).Count() > 0) || (this.Globales.MODFDIA.ListaReportes.Count > 0) || (this.Globales.MODFDIA.ListaConfirmaciones.Count > 0) ? true : false;

            return Json(new { hayProblemas = hayProblemas, 
                            ListaProblemas = listaMensaje, 
                            ListaReportes = this.Globales.MODFDIA.ListaReportes, 
                            ListaConfirmaciones = this.Globales.MODFDIA.ListaConfirmaciones ,
                              Supervisor = (this.Globales.MODGUSR.UsrEsp.Jerarquia == 1)
            });
        }

        public JsonResult BtnBajarDatos_Click_final(bool cierreForzado = false)
        {
            List<UI_Message> listaMensaje = new List<UI_Message>();
            this.Globales.MODFDIA.ListaReportes.Clear();
            this.Globales.MODFDIA.ListaConfirmaciones.Clear();

            this.fdService.btnBajarDatos_Click_final(this.Globales.MODGUSR, listaMensaje, this.Globales.MODFDIA, this.Globales.MODCUACC, cierreForzado);

            bool hayProblemas = (listaMensaje.Where(c => c.IsError).Count() > 0) || (this.Globales.MODFDIA.ListaReportes.Count > 0) || (this.Globales.MODFDIA.ListaConfirmaciones.Count > 0) ? true : false;

            //escribo la variables para que no deje entrar a las otras aplicaciones
            Session[SessionKeys.Common.FinDiaEjecutado] = (!hayProblemas).ToString();

            return Json(new { hayProblemas = hayProblemas, ListaProblemas = listaMensaje, ListaReportes = this.Globales.MODFDIA.ListaReportes, ListaConfirmaciones = this.Globales.MODFDIA.ListaConfirmaciones });
        }

        public JsonResult btnBajarDatos_Click_Especialista_1()
        {
            List<UI_Message> listaMensaje = new List<UI_Message>();
            this.Globales.MODFDIA.ListaReportes.Clear();
            this.Globales.MODFDIA.ListaConfirmaciones.Clear();

            this.fdService.BajarDatos_Click_Especialista_1(this.Globales.MODGUSR, listaMensaje, this.Globales.MODFDIA, this.Globales.MODCUACC);

            bool hayProblemas = (listaMensaje.Where(c => c.IsError).Count() > 0) || (this.Globales.MODFDIA.ListaReportes.Count > 0) || (this.Globales.MODFDIA.ListaConfirmaciones.Count > 0) ? true : false;
            bool imprimir = this.Globales.MODFDIA.impresionPorInyectar.Length > 0;

            return Json(new {   hayProblemas = hayProblemas, 
                                ListaProblemas = listaMensaje, 
                                ListaReportes = this.Globales.MODFDIA.ListaReportes, 
                                ListaConfirmaciones = this.Globales.MODFDIA.ListaConfirmaciones,
                                imprimirPorInyectar = imprimir
            });
        }

        public JsonResult btnBajarDatos_Click_Supervisor_1()
        {
            List<UI_Message> listaMensaje = new List<UI_Message>();
            this.Globales.MODFDIA.ListaReportes.Clear();
            this.Globales.MODFDIA.ListaConfirmaciones.Clear();

            this.fdService.BajarDatos_Click_Supervisor_1(this.Globales.MODGUSR, listaMensaje, this.Globales.MODFDIA, this.Globales.MODCUACC);

            bool hayProblemas = (listaMensaje.Where(c => c.IsError).Count() > 0) || (this.Globales.MODFDIA.ListaReportes.Count > 0) || (this.Globales.MODFDIA.ListaConfirmaciones.Count > 0) ? true : false;
            bool imprimir = this.Globales.MODFDIA.impresionPorInyectar.Length > 0;

            return Json(new
            {
                hayProblemas = hayProblemas,
                ListaProblemas = listaMensaje,
                ListaReportes = this.Globales.MODFDIA.ListaReportes,
                ListaConfirmaciones = this.Globales.MODFDIA.ListaConfirmaciones,
                mostrarFrmResDesc = this.Globales.MODCUACC.VConCCLin2.Count > 0
            });
        }

        public JsonResult btnBajarDatos_Click_Supervisor_2()
        {
            List<UI_Message> listaMensaje = new List<UI_Message>();
            this.Globales.MODFDIA.ListaReportes.Clear();
            this.Globales.MODFDIA.ListaConfirmaciones.Clear();

            this.fdService.BajarDatos_Click_Supervisor_2(this.Globales.MODGUSR, listaMensaje, this.Globales.MODFDIA, this.Globales.MODCUACC);

            bool hayProblemas = (listaMensaje.Where(c => c.IsError).Count() > 0) || (this.Globales.MODFDIA.ListaReportes.Count > 0) || (this.Globales.MODFDIA.ListaConfirmaciones.Count > 0) ? true : false;
            bool imprimir = this.Globales.MODFDIA.impresionPorInyectar.Length > 0;

            return Json(new
            {
                hayProblemas = hayProblemas,
                ListaProblemas = listaMensaje,
                ListaReportes = this.Globales.MODFDIA.ListaReportes,
                ListaConfirmaciones = this.Globales.MODFDIA.ListaConfirmaciones,
                imprimirPorInyectar = imprimir
            });
        }

        //public JsonResult btnAceptar_Confirmacion(string dato)
        //{
        //    List<UI_Message> listaMensaje = new List<UI_Message>();
        //    fdService.btnAceptar_Confirmacion(dato,this.Globales.MODFDIA.ListaReportes[0].formMostrarConfirmacion, Globales.MODCUACC, Globales.MODFDIA, listaMensaje, this.Globales.MODGUSR);
        //    return Json(new { listaMensajes = listaMensaje });
        //}

        public JsonResult btnAceptar_Confirmacion_Clave(string dato)
        {
            List<UI_Message> listaMensaje = new List<UI_Message>();
            fdService.btnAceptar_Confirmacion(dato, FormMostrar.frmClave, Globales.MODCUACC, Globales.MODFDIA, listaMensaje, this.Globales.MODGUSR);
            return Json(new { listaMensajes = listaMensaje });
        }

        public JsonResult btnAceptar_Confirmacion(string dato, FormMostrar tipo )
        {
            List<UI_Message> listaMensaje = new List<UI_Message>();
            fdService.btnAceptar_Confirmacion(dato, tipo, Globales.MODCUACC, Globales.MODFDIA, listaMensaje, this.Globales.MODGUSR);
            return Json(new { listaMensajes = listaMensaje });
        }

        public ActionResult ImpresionPorInyectar()
        {
            using (var tracer = new Tracer("ImpresionPorInyectar"))
            {
                if (Globales == null || Globales.MODFDIA == null)
                {
                    ViewBag.Detalle = "No es posible mostrar los datos en este momento";
                    tracer.TraceError("No es posible mostrar los datos en este momento");
                }
                else
                {
                    ViewBag.Detalle = this.Globales.MODFDIA.impresionPorInyectar;
                }
                ViewBag.GenerarHtmlCompleto = false;
                ViewBag.Imprimir = false;
                return View();
            }
        }

        public ActionResult ImpresionListado()
        {
            using (var tracer = new Tracer("ImpresionListado"))
            {
                if (Globales == null || Globales.MODFDIA == null)
                {
                    ViewBag.Detalle = "No es posible mostrar los datos en este momento";
                    tracer.TraceError("No es posible mostrar los datos en este momento");
                }
                else
                {
                    ViewBag.Detalle = this.Globales.MODFDIA.impresionListado;
                }                
                ViewBag.GenerarHtmlCompleto = false;
                ViewBag.Imprimir = false;
                return View();
            }            
        }

        public JsonResult GetContabilidadVigente(string CentroCosto, string Especialidad)
        {
            var result = fdService.GetContabilidadVigente(CentroCosto, Especialidad);
            return Json(result.Select(i => new

            {
                NumeroOperacion = i.codcct + "-" + i.codpro + "-" + i.codesp + "-" + i.codofi + "-" + i.codope,
                NumeroReporte = i.nrorpt,
                FechaContable = i.fecmov.ToString("dd-MM-yyyy")
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ValidacionCuadraturaSwift()
        {
            var result = fdService.ValidacionCuadraturaSwift(this.Globales);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListadoCuadraturaSwift()
        {
            return View();
        }
        public JsonResult GetElementosCuadratura()
        {
            var result = fdService.GetElementosCuadratura(this.Globales);
            return Json(result.Select(i => new
            {
               codcct= i.codcct,
               codesp= i.codesp,
               codofi= i.codofi,
               codope= i.codope,
               codpro= i.codpro,
               glosa_estado= i.glosa_estado,
               mensaje_error= i.mensaje_error,
               ncorr= i.ncorr,
               referencia= i.referencia,
               tipo_mt= i.tipo_mt,
               tipo_mt_decimal= i.tipo_mt_decimal            
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult EliminaMT(string codcct, string codesp, string codofi, string codope, string codpro, string glosa_estado, string mensaje_error, decimal? ncorr, string referencia, string tipo_mt, string tipo_mt_decimal)
        {

            var result = fdService.EliminaMT(codcct, codesp, codofi, codope, codpro, glosa_estado, mensaje_error, ncorr, referencia, tipo_mt, tipo_mt_decimal);
            if (result != -1)
            {
                Globales.MODCUAD.Elementos = Globales.MODCUAD.Elementos.Where(i => i.ncorr != ncorr).ToArray();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPassword()
        {
            var result = fdService.GetPassword();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Set_MODCUACC_true()
        {
            var result = fdService.Set_MODCUACC_true(this.Globales);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_MODCUACC_ABONO_VB()
        {
            var result = fdService.Get_MODCUACC_ABONO_VB(this.Globales);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Set_MODCUACC_false()
        {
            var result = fdService.Set_MODCUACC_false(this.Globales);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReporteCuadratura()
        {
            var result = fdService.GetElementosCuadratura(this.Globales);
            var listado = from p in result
                          select new DetalleReporteCuadraturaModel()
                          {
                           codcct= p.codcct,
                           codesp= p.codesp,
                           codofi=p.codofi,
                           codope= p.codope,
                           codpro= p.codpro,
                           glosa_estado= p.glosa_estado,
                           mensaje_error= p.mensaje_error,
                           ncorr= p.ncorr,
                           referencia= p.referencia,
                           tipo_mt= p.tipo_mt,
                           tipo_mt_decimal=p.tipo_mt_decimal                        
                          };
            int countlistado = listado.Count();
            var reporte = new ReporteCuadraturaModel()
            {
                Total = "Total de archivos: " + countlistado,
                Titulo = "Listado de archivos automáticos.",
                Detalle = listado
            };

            return View(reporte);

    }
        public ActionResult FrmResDesc()
        {
            return View();
        }
        public JsonResult GetElementosSce_cuadra_inyecciones_ctacte_MS()
        {
            var result = fdService.GetElementosSce_cuadra_inyecciones_ctacte_MS(this.Globales); 
            return Json(result.Select(i => new
            {
                codcct = i.codcct,
                codesp = i.codesp,
                codofi = i.codofi,
                codope = i.codope,
                codpro = i.codpro,
                cencos = i.cencos,
                cod_dh = i.cod_dh,
                codusr = i.codusr,
                cuadra = i.cuadra,
                error = i.error,
                estado = i.estado,
                fecmov = i.fecmov,
                mtomcd = i.mtomcd,
                nemcta = i.nemcta,
                nemmon = i.nemmon,
                nrorpt = i.nrorpt,
                numcct = i.numcct
            }).ToList(), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetElementosSce_mcd_s71()
        {
            var result = fdService.Sce_mcd_s71(this.Globales);
            return Json(result.Select(i => new
            {
                codcct = i.codcct,
                codesp = i.codesp,
                codofi = i.codofi,
                codope = i.codope,
                codpro = i.codpro,
                cencos = i.cencos,
                cod_dh = i.cod_dh,
                codusr = i.codusr,
                cuadra = i.cuadra,
                error = i.error,
                estado = i.estado,
                fecmov = i.fecmov,
                mtomcd = i.mtomcd,
                nemcta = i.nemcta,
                nemmon = i.nemmon,
                nrorpt = i.nrorpt,
                numcct = i.numcct
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReporteDescuadre()
        {
            var resultInyecciones = fdService.GetElementosSce_cuadra_inyecciones_ctacte_MS(this.Globales);
            var resultSce = fdService.Sce_mcd_s71(this.Globales);
            var listadoInyecciones = from i in resultInyecciones
                                     select new DetalleReporteDescuadraturaModel()
                                     {
                                         codcct = i.codcct,
                                         codesp = i.codesp,
                                         codofi = i.codofi,
                                         codope = i.codope,
                                         codpro = i.codpro,
                                         cencos = i.cencos,
                                         cod_dh = i.cod_dh,
                                         codusr = i.codusr,
                                         cuadra = i.cuadra,
                                         error = i.error,
                                         estado = i.estado,
                                         fecmov = i.fecmov,
                                         mtomcd = i.mtomcd,
                                         nemcta = i.nemcta,
                                         nemmon = i.nemmon,
                                         nrorpt = i.nrorpt,
                                         numcct = i.numcct
                                     };
            var listadoSce = from i in resultSce
                             select new DetalleReporteDescuadraturaSceModel()
                                        {
                                            codcct = i.codcct,
                                            codesp = i.codesp,
                                            codofi = i.codofi,
                                            codope = i.codope,
                                            codpro = i.codpro,
                                            cencos = i.cencos,
                                            cod_dh = i.cod_dh,
                                            codusr = i.codusr,
                                            cuadra = i.cuadra,
                                            error = i.error,
                                            estado = i.estado,
                                            fecmov = i.fecmov,
                                            mtomcd = i.mtomcd,
                                            nemcta = i.nemcta,
                                            nemmon = i.nemmon,
                                            nrorpt = i.nrorpt,
                                            numcct = i.numcct
                                        };

            int countlistado = listadoInyecciones.Count() + listadoSce.Count();
            foreach (var item in resultInyecciones)
            {
                if(item.estado!=9)
                {
                    countlistado--;
                }

    }
            var reporte = new ReporteDescuadraturaModel()
            {
                Total = "Total de archivos: " + countlistado,
                Titulo = "Listado de elementos.",
                DetalleInyeccion = listadoInyecciones,
                DetalleSce = listadoSce
            };
            return View(reporte);
        }

        public ActionResult ImpresionDescuadre()
        {
            string imp = this.fdService.imprimirReporteDescuadratura(this.Globales.MODCUACC);
            ViewBag.Detalle = imp;
            ViewBag.GenerarHtmlCompleto = false;
            ViewBag.Imprimir = false;
            return View();
        } 
    }
}
