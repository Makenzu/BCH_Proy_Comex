using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XGGL.Forms;
using BCH.Comex.Core.BL.XGGL.Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace BCH.Comex.Core.BL.XGGL
{
    public partial class XgglService
    {

        #region Generar Swift

        public bool InicializarGeneracionDeSwift(DatosGlobales Globales)
        {
            if (!Globales.MODGSWF.VGSwf.Acepto)
            {
                string nroOperacion = MODGCHQ.Referencia(Globales);
                return CONTABGL.InterfazSwf(Globales, uow);
            }
            return false;
        }
 
        public IList<T_Pai> CargarPaisesClientes(DatosGlobales initObject)
        {
            if (initObject.MODGTAB0.VPai == null || initObject.MODGTAB0.VPai.Length == 0)
            {
                MODGTAB0.SyGetn_Pai(initObject, uow);
            }
            else
            {
                //ya estan cargados los paises, no lo traigo de nuevo de BD
            }
            return initObject.MODGTAB0.VPai.Where(x => x.Pai_PaiNom != null).ToList();
        }

        public IList<sce_cpai> CargarPaisesBancos()
        {
            return uow.SceRepository.sce_cpai_s01_MS();
        }

        public IList<T_Mnd> CargarMonedas(DatosGlobales initObject)
        {

            MODGTAB0.SyGetn_Mnd(initObject, uow);
            return initObject.MODGTAB0.VMnd.Where(x => x.Mnd_MndNom != null).ToList();
        }

        public IList<T_Cor> CargarCorresponsales(DatosGlobales initObject)
        {
            MODGTAB0.SyGetn_Cor(initObject, uow);
            return initObject.MODGTAB0.VCor;
        }

        /// <summary>
        /// Funcion que trae los datos del banco y también el código del país al que pertenece, unifica las funciones SyGet_VBic y SyGet_Cou del legacy
        /// </summary>
        /// <param name="initObject"></param>
        /// <param name="swiftBanco"></param>
        /// <param name="secuencia"></param>
        /// <returns></returns>
        public T_Bic GetBancoPorSwift(DatosGlobales initObject, string swiftBanco, string secuencia)
        {
            var result = MODGBIC.GetBancoPaymentPlus(initObject, uowSwift, swiftBanco, secuencia);
            if (result != 0)
            {
                T_Bic banco = initObject.MODGBIC.VBic;
                //initObject.MODGBIC.VBic = banco;
                if (banco != null)
                {
                    int? couCod = uow.BancoRepository.sce_cou_s03_MS(banco.BicCod);
                    if (couCod.HasValue)
                    {
                        banco.CouCod = couCod.Value;
                    }
                }
                return banco;
            }
            else return null;
        }

        public IList<CodigoDeOrdenCampo23Swift> GetCodigosDeOrdenPosiblesCampo23()
        {
            List<CodigoDeOrdenCampo23Swift> lista = new List<CodigoDeOrdenCampo23Swift>();
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.BONL });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.CHQB });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.CORT });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.HOLD, PermiteTextoAdicional = true });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.INTC });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.PHOB, PermiteTextoAdicional = true });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.PHOI, PermiteTextoAdicional = true });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.PHON, PermiteTextoAdicional = true });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.SDVA });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.TELB, PermiteTextoAdicional = true });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.TELE, PermiteTextoAdicional = true });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.TELI, PermiteTextoAdicional = true });

            return lista;
        }

        public IDictionary<string, IList<string>> GetReglasCodigosDeOrdenCampo23E()
        {
            Dictionary<string, IList<string>> reglas = new Dictionary<string, IList<string>>();

            //leo mi custom section del web.config
            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            XmlElement node = doc.SelectSingleNode("/configuration/FundTransfer.ReglasCodigoDeOrden") as XmlElement;
            if (node != null)
            {
                foreach (XmlNode nodoRegla in node.ChildNodes)
                {
                    string valor1 = nodoRegla.Attributes["Valor1"].Value;
                    string valor2 = nodoRegla.Attributes["Valor2"].Value;

                    IList<string> reglasDeKey = null;
                    if (reglas.ContainsKey(valor1))
                    {
                        reglasDeKey = reglas[valor1];
                    }
                    else
                    {
                        reglasDeKey = new List<string>();
                        reglas.Add(valor1, reglasDeKey);
                    }

                    reglasDeKey.Add(valor2);
                }
            }

            return reglas;
        }

        public DateTime CalcularFechaInicialSwift(T_MODGTAB0 mod)
        {
            return Frm_Swf0.Pr_Fecha_Inicial(mod, uow);
        }
        public bool ValidarFechaPago(T_MODGTAB0 mod, DateTime fechaPago, out string mensajeError)
        {
            if (fechaPago.Date < DateTime.Now.Date)
            {
                mensajeError = "Fecha no puede ser menor a la fecha de hoy";
                return false;
            }
            else return Frm_Swf0.ValidarFechaPago(mod, uow, fechaPago, out mensajeError);
        }

        public bool ValidarCodComp(T_MODGSWF mod, string codComp, out string mensajeError)
        {
            return Frm_Swf0.ValidarCodComp(mod, uow, codComp, out mensajeError);
        }

        public IList<UI_Message> ValidarSwiftCompleto(DatosGlobales initObj, T_Swf swiftNuevo, T_CliSwf cliente, T_mt103 montos, IList<LineaMensajeSwift> lineasManuales)
        {
            return Frm_Swf0.ValidarSwiftCompleto(initObj, uow, uowSwift, swiftNuevo, cliente, montos, lineasManuales);
        }

        public bool GenerarSwift(DatosGlobales initObj, T_Swf swiftNuevo, T_CliSwf cliente, T_mt103 montos, IList<CodigoDeOrdenCampo23Swift> codigosOrden, IList<LineaMensajeSwift> lineasManuales, short indiceSwift)
        {
            return Frm_Swf0.GenerarSwift(initObj, uow, swiftNuevo, cliente, montos, codigosOrden, lineasManuales, indiceSwift);
        }

        public IList<LineaMensajeSwift> GetCamposManualesSwift(int codMt)
        {
            IList<LineaMensajeSwift> Resultado = new List<LineaMensajeSwift>();
            Resultado = uow.SceRepository.GetCamposManualesSwift(codMt);

            if (Resultado.Any(c => c.CodCam.Trim() == "72"))
            {
                List<Entities.Swift.sw_valor_campos> valoresCampos = uowSwift.ValorCamposRepository.LlenaMatrizValores("MT" + codMt);
                valoresCampos = valoresCampos.Where(c => c.linea_campo == 1 && c.tag_campo.Trim() == "72").ToList();
                if (valoresCampos.Count > 0)
                {
                    List<SelectListLine> fill = valoresCampos.SingleOrDefault().valor_campo.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(c => new SelectListLine { Text = c, Value = c }).ToList<SelectListLine>();
                    Resultado.Where(c => c.CodCam.Trim() == "72").SingleOrDefault().LineasSecundarias.FirstOrDefault().ValorCampo = fill;
                }
            }
            return Resultado;
        }

        public IList<SelectListLine> GetCodigosCampo72(string pais, string moneda, out bool condEspecialesPais)
        {
            List<SelectListLine> result = new List<SelectListLine>();
            List<ParametroComex> resultParams = uow.ParametroComexRepository.GetParametrosComex("CAMPO72", pais, moneda)
                .OrderBy(x => x.trans_nmb_agrupacion_3).ThenBy(x => x.trans_vlr_parametro).ToList();
            condEspecialesPais = resultParams.Exists(x => x.trans_nmb_agrupacion_3 != "*");

            foreach (var param in resultParams)
            {
                var linea = new SelectListLine
                {
                    Disabled = false,
                    Selected = false,
                    Text = string.IsNullOrWhiteSpace(param.trans_dsc_parametro) ? param.trans_vlr_parametro : param.trans_vlr_parametro + " - " + param.trans_dsc_parametro,
                    Value = param.trans_vlr_parametro
                };
                result.Add(linea);
            }
            return result;
        }

        #endregion

    }
}
