using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XGGL.Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Models;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Models.FundTransfer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Controllers
{
    public class GenerarSwiftController : BaseControllerCG
    {
        //
        // GET: /ContabilidadGenerica/GenerarSwift/
        public ActionResult Index()
        {
            return View();
        }


        #region Generacion Swift

        public ActionResult GenerarSwift(bool? generarDatosDummy)
        {
            bool estadoPrevioOK = true;
            if (generarDatosDummy == true)  //remover esta linea una vez que esté toda la aplicación integrada
            {
                //service.CargarConDummiesTodasLasEstructurasQueDeberianVenirCargadasDePantallasAnterioresAGenerarSwift(this.Globales);
            }
            else
            {
                estadoPrevioOK = service.InicializarGeneracionDeSwift(this.Globales);
            }

            if (estadoPrevioOK)
            {
                //ViewBag.MdiPrincipal = this.Globales.Mdi_Principal;
                this.Globales.MODGSWF.VGSwf.Acepto = false; //todavia no esta generado el swift

                GenerarSwiftBaseViewModel model = new GenerarSwiftBaseViewModel();
                IList<T_Pai> paises = service.CargarPaisesClientes(this.Globales).OrderBy(p => p.Pai_PaiNom).ToList();
                IList<sce_cpai> paisesBanco = service.CargarPaisesBancos();
                IList<T_Mnd> monedas = service.CargarMonedas(this.Globales);

                //model.EsCargaAutomatica = (this.Globales.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1);
                TextInfo textCulture = Thread.CurrentThread.CurrentCulture.TextInfo;
                var a = paises.Select(p => new SelectListItem() { Value = p.Pai_PaiCod.ToString(), Text = textCulture.ToTitleCase(p.Pai_PaiNom.Trim().ToLower()) }).ToList();

                model.PaisesTPai = paises.Select(p => new SelectListItem() { Value = p.Pai_PaiCod.ToString(), Text = textCulture.ToTitleCase(p.Pai_PaiNom.Trim().ToLower()) }).ToList();
                model.PaisesCPai = paisesBanco.Select(s => new SelectListItem() { Value = s.cpai_codpaic, Text = textCulture.ToTitleCase(s.cpai_nompai.Trim().ToLower()) }).ToList();
                model.CodigosDeOrdenPosiblesCampo23 = service.GetCodigosDeOrdenPosiblesCampo23();
                model.ReglasCodigosDeOrden = service.GetReglasCodigosDeOrdenCampo23E();
                model.Monedas = monedas.Select(m => new SelectListItem() { Value = m.Mnd_MndCod.ToString(), Text = textCulture.ToTitleCase(m.Mnd_MndNom.Trim().ToLower()) }).ToList();               
                DateTime fechaPagoInicial = service.CalcularFechaInicialSwift(this.Globales.MODGTAB0);
                CargarListasTipoBancos(model);

                model.Cliente = this.Globales.MODGSWF.VCliSwf;
                bool algunaGenerada = this.Globales.MODGSWF.VSwf.Where(p => p.EstaGen != 0).Any();
                if (!algunaGenerada)
                {
                    //es la 1era vez, pongo el 50F como defecto
                    model.Cliente.Es50F = true;
                }
                else
                {
                    model.Cliente.Es50F = this.Globales.MOD_50F.CHK_50F;
                }

                for (int i = 0; i < this.Globales.MODGSWF.VBenSwf.Length; i++)
                {
                    model.BeneficiariosIniciales.Add(this.Globales.MODGSWF.VBenSwf[i]);
                }

                for (short i = 0; i < this.Globales.MODGSWF.VSwf.Length; i++)
                {
                    T_Swf planilla = this.Globales.MODGSWF.VSwf[i];

                    PlanillaViewModel info = new PlanillaViewModel()
                    {
                        DatosSwift = planilla,
                        Montos = this.Globales.MODGSWF.VMT103[i],
                    };
                    var aux = service.GetCamposManualesSwift(T_MODGSWF.MT_103).Where(c => c.CodCam.Trim() != "70").ToList();
                    // Cargo los valores por default para el campo 72 según moneda
                    if (aux.Any(c => c.CodCam.Trim() == "72")) 
                    {
                        bool discarded;
                        List<SelectListLine> auxLineas = service.GetCodigosCampo72("", planilla.CodMon.ToString(), out discarded).ToList();
                        aux.Where(c => c.CodCam.Trim() == "72").SingleOrDefault().LineasSecundarias.FirstOrDefault().ValorCampo = auxLineas;
                    }
                    
                    info.LineasManuales.AddRange(aux); //por si el beneficiario no es banco;
                    info.LineasManuales.AddRange(service.GetCamposManualesSwift(T_MODGSWF.MT_202));//por si el beneficiario es banco

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

                        if (!model.EsCargaAutomatica && planilla.CodMon == T_MODGTAB0.MndDol)
                        {
                            //planilla.BenSwf.PaiBen = T_MODGTAB0.PaisEEUU; //Se comenta esta linea y se agregan las siguientes para el 59F por default
                            planilla.BenSwf.PaiBen59F = T_MODGTAB0.PaisEEUUEn59F;
                            planilla.DatSwf.PlzPag = T_MODGTAB0.PaisEEUU;
                        }
                    }
                    else
                    {
                        //ya se genero la planilla, todos los datos que muestro tienen que ser los ingresados anteriormente
                        info.Bancos = MapearBancosDeEstructuraLegacyAModelo(planilla);

                        List<T_Campo23E> camposExistentes = this.Globales.MODGSWF.VCod.Where(c => c.numswi == i).ToList();
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
                ViewBag.Caption = this.Globales.MODGCVD.VgCvd.OpeCon;
                //ViewBag.Caption = this.Globales.MODGCVD.VgCvd.OpeCon + " " + this.Globales.CaptionAddition;
                return View(model);
            }
            else
            {
                throw new Exception("No se puede generar el swift, la aplicación no está en un estado consistente");
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
                var data = service.GetBancoPorSwift(this.Globales, swiftBanco.Substring(0, 8), swiftBanco.Substring(8, 3));

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
            service.CargarCorresponsales(this.Globales);
            IList<T_Cor> corresponsales = MODGCOR.Filtra_Cor(this.Globales, idPlazaDePago, codMoneda, false);

            var jsonResult = new JsonResult()
            {
                Data = corresponsales,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            return jsonResult;
        }

        [HandleAjaxException, HttpPost]
        public ActionResult ValidarYGenerarSwift(GenerarSwiftViewModel model)
        {
            this.Globales.MESSAGES.Clear();

            PlanillaViewModel p = model.Planillas[model.IndiceP];
            MapearBancosDeModeloAEstructuraLegacy(p.Bancos, p.DatosSwift);

            IList<UI_Message> mensajes = service.ValidarSwiftCompleto(this.Globales, p.DatosSwift, model.Cliente, p.Montos, p.LineasManuales);

            if (mensajes.Count == 0)
            {
                bool genereOK = service.GenerarSwift(this.Globales, p.DatosSwift, model.Cliente, p.Montos, p.CodigosDeOrdenCampo23, p.LineasManuales, model.IndiceP);
                mensajes = this.Globales.MESSAGES;
            }

            var data = new
            {
                EstaGen = this.Globales.MODGSWF.VSwf[model.IndiceP].EstaGen,
                DocSwift = this.Globales.MODGSWF.VSwf[model.IndiceP].DocSwf,
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
            this.Globales.MODGSWF.VGSwf.Acepto = true;
            return RedirectToAction("MT", "Grabar");
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
            string mensajeError = "";
            bool esValido = esValido = service.ValidarCodComp(this.Globales.MODGSWF, codCom, out mensajeError);
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
                T_Bic banco = service.GetBancoPorSwift(this.Globales, swiftBanco.Substring(0, 8), swiftBanco.Substring(8, 3));
                if (banco != null)
                {
                    pais = banco.BicCod;
                }
            }
            codigosCampo72 = service.GetCodigosCampo72(pais, moneda.ToString(), out condicionesEspecialesPais).ToList();

            //var camposManuales = service.GetCamposManualesSwift(T_MODGSWF.MT_103).Where(c => c.CodCam.Trim() == "72").ToList();

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
    }
}