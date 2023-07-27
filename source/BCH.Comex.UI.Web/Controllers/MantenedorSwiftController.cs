using BCH.Comex.Common;
using BCH.Comex.Core.BL.BancaMundial;
using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.BL.SWMA;
using BCH.Comex.Core.Entities.Sbcor;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Controllers
{
    public class MantenedorSwiftController : Controller
    {
        BancaMundialService BancaMundialservice;
        private swmaService Service;

        static MantenedorSwiftController()
        {
            new PortalService().RegisterApp("SWMA", "Mantención de tablas Swift", "SWIFT", Constantes.AppRoles.MantenedorSwiftAppRole, 
                "COMEX_GRP_SWIFT", "MantenedorSwift");
        }
        public MantenedorSwiftController()
        {
            this.Service = new swmaService();
            BancaMundialservice = new BancaMundialService();
        }
        [AuthorizeOrForbidden(Roles = Constantes.AppRoles.MantenedorSwiftAppRole)]
        public ActionResult Index()
        {
            ViewBagPaises();
            return View(new List<sce_bic>());
        }
        private void ViewBagPaises()
        {
            var paises = BancaMundialservice.ListPaises();
            List<SelectListItem> paisesSelectListItem = new List<SelectListItem>();
            foreach (var item in paises)
            {
                paisesSelectListItem.Add(new SelectListItem
                {
                    Text = item.cpai_nompai,
                    Value = item.cpai_codpaic
                });
            }
            ViewBag.lst_Paises = paisesSelectListItem;
        }
        public JsonResult GetUsuarios(int? rut, string nombre, string tipo)
        {
            proc_sw_trae_usuarios_MS_DTO users = new proc_sw_trae_usuarios_MS_DTO();
            if (nombre.Length <= 0)
            { nombre = null; };
            int indice = 0;
            users.nombre = nombre;
            users.rut = rut;
            users.tipo = tipo;
            var result = Service.GetUsuarios(users);
            return Json(result.Select(i => new
            {
                id = indice++,
                dv = i.digv_user,
                rut = i.rut_user,
                nombre = i.nombre_user,
                tipo = i.tipo_user
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateUsuarios(int rut, string nombre, string tipo)
        {
            bool? result = Service.UpdateUsuarios(rut, nombre.ToUpper(), tipo.ToUpper());
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertUsuarios(int rut,int CvRut, string nombre, string tipo)
        {
            bool? result = Service.InsertUsuarios(rut, CvRut, nombre.ToUpper(), tipo.ToUpper());
     
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteUsuarios(int rut)
        {
            bool? result = Service.DeleteUsuarios(rut);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCasillas(int? codigo, string nombre, string origenCasilla)
        {
            int indice = 0;
            proc_sw_trae_casillas_MS_DTO Casillas = new proc_sw_trae_casillas_MS_DTO();
            if (nombre.Length<=0)
            { nombre = null; };
            if (origenCasilla.Length <= 0)
            { origenCasilla = null; };
            Casillas.codigo = codigo;
            Casillas.nombre = nombre;
            Casillas.origen = origenCasilla;
            var result = Service.GetCasillas(Casillas);
            return Json(result.Select(i => new
            {
                id = indice++,
                codigo = i.cod_casilla,
                nombre = i.nombre_casilla,
                origen = i.origen_recep
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateCasillas(int codigo, string nombre, string origen)
        {
            bool? result = Service.UpdateCasillas(codigo, nombre, origen);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertCasillas(int codigo, string nombre, string origen)
        {
            bool? result = Service.InsertCasillas(codigo, nombre, origen);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteCasillas(int codigo)
        {
            bool? result = Service.DeleteCasillas(codigo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMonedas(string codMonedaSw, int? codMonedaBc, string Nombre)
        {
            int indice = 0;
            var result = Service.GetMonedas();
            var resultData = result.Select(i => new
            {
                id = indice++,
                codMonedaSw = string.IsNullOrEmpty(i.cod_moneda_sw) ? string.Empty : i.cod_moneda_sw,
                codMonedaBc = string.IsNullOrEmpty(i.cod_moneda_banco) ? string.Empty : i.cod_moneda_banco,
                Uso = i.uso_moneda_banco,
                Nombre = i.nombre_moneda,
                decimales = i.decimales
            }).Where(item => item.codMonedaBc.Contains((string.IsNullOrWhiteSpace(codMonedaBc.ToString()) ? item.codMonedaBc : codMonedaBc.ToString())) &&
                item.codMonedaSw.Contains((string.IsNullOrWhiteSpace(codMonedaSw) ? item.codMonedaSw : codMonedaSw.ToUpper())) &&
                item.Nombre.Contains((string.IsNullOrWhiteSpace(Nombre) ? item.Nombre : Nombre.ToUpper()))).ToList();


            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertMonedas(string CodigoSw, int? CodigoBc, string Nombre, int Decimales, string Uso)
        {
            bool? result = Service.InsertMonedas(CodigoSw, CodigoBc, Nombre, Decimales, Uso);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteMonedas(string codigo)
        {
            bool? result = Service.DeleteMonedas(codigo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateMonedas(string CodigoSwMoneda,string NombreMoneda,string UsoMoneda,int? CodigoBcMoneda,int? DecimalesMoneda)
        {
            if (DecimalesMoneda==null)
            {
                DecimalesMoneda = 0;
            }
            bool? result = Service.UpdateMonedas(CodigoSwMoneda, NombreMoneda, UsoMoneda, CodigoBcMoneda,DecimalesMoneda);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBancosCount(string swift, string pais, string ciudad, string banco, string direccion,
            string branch, string intercambioClave)
        {
            swift = (swift.Length <= 0 ? null : swift.ToUpper().Trim());
            ciudad = (ciudad.Length <= 0 ? null : ciudad.ToUpper().Trim());
            banco = (banco.Length <= 0 ? null : banco.ToUpper().Trim());
            direccion = (direccion.Length <= 0 ? null : direccion.ToUpper().Trim());
            branch = string.IsNullOrEmpty(branch) ? null : branch.ToUpper().Trim();
            intercambioClave = string.IsNullOrEmpty(intercambioClave) ? null : intercambioClave.ToUpper().Trim();

            var strPais = BancaMundialservice.ListPaises().Where(t => t.cpai_codpaic == pais).Select(i=> i.cpai_nompai.Trim()).FirstOrDefault();
            int count=0;
            var resultCount = Service.GetBancosCount(swift, strPais, ciudad, banco, direccion, branch, intercambioClave);
            foreach (var item in resultCount)
            {
                count = int.Parse(item.Value.ToString());
            }
            if (count >=700)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [FileDownload]
        public FileResult DescargarExcelBancos(string swift, string pais, string ciudad, string banco, string direccion, string branch, string intercambioClave)
        {
            swift = (swift.Length <= 0 ? null : swift.ToUpper().Trim());
            ciudad = (ciudad.Length <= 0 ? null : ciudad.ToUpper().Trim());
            banco = (banco.Length <= 0 ? null : banco.ToUpper().Trim());
            direccion = (direccion.Length <= 0 ? null : direccion.ToUpper().Trim());
            var strPais = BancaMundialservice.ListPaises().Where(t => t.cpai_codpaic == pais).Select(i => i.cpai_nompai.Trim()).FirstOrDefault();
            branch = string.IsNullOrEmpty(branch) ? null : branch.ToUpper().Trim();
            intercambioClave = string.IsNullOrEmpty(intercambioClave) ? null : intercambioClave.ToUpper().Trim();

            MemoryStream stream = Service.DescargarExcelBancos(swift, strPais, ciudad, banco, direccion, branch, intercambioClave);
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "resultados consulta bancos.xlsx");
        }   

        public JsonResult GetBancos(string swift, string pais, string ciudad, string banco, string direccion,
            string branch, string intercambioClave)
        {
            swift = (swift.Length <= 0 ? null : swift.ToUpper().Trim());
            ciudad = (ciudad.Length <= 0 ? null : ciudad.ToUpper().Trim());
            banco = (banco.Length <= 0 ? null : banco.ToUpper().Trim());
            direccion = (direccion.Length <= 0 ? null : direccion.ToUpper().Trim());
            var strPais = BancaMundialservice.ListPaises().Where(t => t.cpai_codpaic == pais).Select(i => i.cpai_nompai.Trim()).FirstOrDefault();
            branch = string.IsNullOrEmpty(branch) ? null : branch.ToUpper().Trim();
            intercambioClave = string.IsNullOrEmpty(intercambioClave) ? null : intercambioClave.ToUpper().Trim();

            int indice = 0;
            var result = Service.GetBancos(swift, strPais, ciudad, banco, direccion, branch, intercambioClave);
               return Json(result.Select(i => new
               {
                   id = indice++,
                   branch = i.branch,
                   ciudad_banco = i.ciudad_banco,
                   cod_banco = i.cod_banco,
                   direccion_banco = i.direccion_banco,
                   intercambio_clave = i.intercambio_clave,
                   localidad_banco = i.localidad_banco,
                   nombre_banco = i.nombre_banco,
                   oficina_banco = i.oficina_banco,
                   pais_banco = i.pais_banco,
                   pobnr_banco = i.pobnr_banco
               }).ToList(), JsonRequestBehavior.AllowGet);
      
        }
        public JsonResult InsertBancos(string codigo, string branch, string nombre, string direccion, string ciudad, string pais, string oficina, string clave, string localidad, string pob)
        {
            if (direccion.Length <= 0)
            {
                direccion = null;
            }
            if (ciudad.Length <= 0)
            {
                ciudad = null;
            }
            if (pais.Length <= 0)
            {
                pais = null;
            }
            if (oficina.Length <= 0)
            {
                oficina = null;
            }
            if (localidad.Length <= 0)
            {
                localidad = null;
            }
            if (pob.Length <= 0)
            {
                pob = null;
            }
            bool? result = Service.InsertBancos(codigo, branch, nombre, direccion, ciudad, pais, oficina, clave, localidad, pob);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateClaveBancos(string clave, string codigo, string branch, int flag)
        {
            bool? result = Service.UpdateClaveBancos(clave,codigo,branch,flag);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateBancos(string codigo, string nombre, string direccion, string ciudad, string oficina, string clave, string localidad, string pob, string pais, string branch)
        {
            if(direccion.Length<=0)
            {
                direccion = null;
            }
            if (ciudad.Length <= 0)
            {
                ciudad = null;
            }
            if (oficina.Length <= 0)
            {
                oficina = null;
            }
            if (localidad.Length <= 0)
            {
                localidad = null;
            }
            if (pob.Length <= 0)
            {
                pob = null;
            }
            if (pais.Length <= 0)
            {
                pais = null;
            }
            if (branch.Length <= 0)
            {
                branch = null;
            }
            bool? result = Service.UpdateBancos(codigo, nombre, direccion, ciudad, oficina, clave, localidad, pob, pais, branch);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBancosVerificados(string codigo)
        {
            var result = Service.GetBancosVerificados(codigo);
            return Json(result.Select(i => new
            {
                ciudad_banco = i.ciudad_banco,
                cod_banco = i.cod_banco,
                direccion_banco = i.direccion_banco,
                intercambio_clave = i.intercambio_clave,
                localidad_banco = i.localidad_banco,
                nombre_banco = i.nombre_banco,
                oficina_banco = i.oficina_banco,
                pais_banco = i.pais_banco,
                pobnr_banco = i.pobnr_banco
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteBancos(string codigo,string branch,int flag)
        {
            bool? result = Service.DeleteBancos(codigo,branch,flag);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetParidades(string codigo, string nombre)
        {
            int indice = 0;
            if (nombre.Length <= 0)
            {
                nombre = null;
            }
            if (codigo.Length <= 0)
            {
                codigo = null;
            }
            proc_sw_trae_paridades_MS_DTO paridades = new proc_sw_trae_paridades_MS_DTO();
            paridades.codigo = codigo;
            paridades.nombre = nombre;
            var result = Service.GetParidades(paridades);
            return Json(result.Select(i => new
            {
                id = indice++,
                cod_moneda_banco = i.cod_moneda_banco,
                cod_moneda_sw = i.cod_moneda_sw,
                fecha_valor = i.fecha_valor,
                nombre_moneda = i.nombre_moneda,
                paridad = i.paridad,
                tipcam_observ = i.tipcam_observ
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetValoresCampos(string codigo)
        {
            int indice = 0;
     
            if (codigo.Length <= 0)
            {
                codigo = null;
            }
            proc_sw_trae_valoresCampos_MS_DTO ValoresCampos = new proc_sw_trae_valoresCampos_MS_DTO();
            ValoresCampos.codigo = codigo;
            var result = Service.GetValoresCampos(ValoresCampos);

            foreach (var i in result) { 
               
                if (i.cond_valor == "=") {
                        i.cond_valor = "Igual a";
                }
                else if ( i.cond_valor == "<>") {
                    i.cond_valor = "Distinto a";
                }
                else if (i.cond_valor == "like"){
                    i.cond_valor = "Comienza con";
                }
                else if (i.cond_valor== "not like") {
                    i.cond_valor = "No debe comenzar con";
                }
            }

            return Json(result.Select(i => new
            {
                id = indice++,
                cond_valor = i.cond_valor,
                linea_campo = i.linea_campo,
                tag_campo = i.tag_campo,
                tipo_msg = i.tipo_msg,
                total_valor = i.total_valor,
                valor_campo = i.valor_campo
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateValoresCampos(string tipo, string tag, string condicion, string valor, int linea)
        {
            int totalValor = valor.Count(c => c == ';');
            var result = Service.UpdateValoresCampos(tipo, tag, condicion, valor, linea, totalValor);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertValoresCampos(string codigo, string tag, int linea, string condicion, string campos, int total)
        { 
            bool? result = Service.InsertValoresCampos(codigo, tag, linea, condicion, campos, total);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteValoresCampos(string codigo, string tag, int linea)
        {
            bool? result = Service.DeleteValoresCampos(codigo, tag, linea);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTiposMensajes(string codigo, string nombre)
        {
            int indice = 0;

            if (codigo.Length <= 0)
            {
                codigo = null;
            }
            if (nombre.Length <= 0)
            {
                nombre = null;
            }
            proc_sw_trae_TiposMensajes_MS_DTO tipoMensaje = new proc_sw_trae_TiposMensajes_MS_DTO();
            tipoMensaje.codigo = codigo;
            tipoMensaje.nombre = nombre;
            var result = Service.GetTiposMensajes(tipoMensaje);
            return Json(result.Select(i => new
            {
                id = indice++,
                cod_tipo = i.cod_tipo,
                nombre_tipo = i.nombre_tipo
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateTiposMensajes(string codigo, string nombre)
        {
            var result = Service.UpdateTiposMensjaes(codigo, nombre);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertTiposMensajes(string codigo, string nombre)
        {
            var result = Service.InsertTiposMensjaes(codigo, nombre);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteTiposMensajes(string codigo)
        {
            var result = Service.DeleteTiposMensjaes(codigo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFormatoMensajes(string codigo)
        {
            int indice = 0;

            if (codigo.Length <= 0)
            {
                codigo = null;
            }
            proc_sw_trae_FormatoMensajes_MS_DTO formatoMensajes = new proc_sw_trae_FormatoMensajes_MS_DTO();
            formatoMensajes.codigo = codigo;
            var result = Service.GetFormatoMensajes(formatoMensajes);
            return Json(result.Select(i => new
            {
                id = indice++,
                orden_fmt = i.orden_fmt,
                repeticion_fmt = i.repeticion_fmt,
                secuencia_fmt=i.secuencia_fmt,
                status_fmt=i.status_fmt,
                tag_fmt=i.tag_fmt,
                tipo_msg_fmt=i.tipo_msg_fmt
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateFormatoMensajes(string codigo, int orden, string secuencia, int repeticion, string tag, string status, int ordenOriginal, string tagOriginal)
        {
            var result = Service.UpdateFormatoMensajes(codigo, orden, secuencia, repeticion, tag, status, ordenOriginal, tagOriginal);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertFormatoMensajes(string codigo, int orden, string secuencia, int repeticion, string tag, string status)
        {
            if (secuencia.Length <= 0)
            {
                secuencia = null;
            }
            var result = Service.InsertFormatoMensajes(codigo, orden, secuencia, repeticion, tag, status);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteFormatoMensajes(string codigo, int orden, string tag)
        {
            var result = Service.DeleteFormatoMensajes(codigo, orden, tag);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCampoMensajes(string codigo)
        {
            int indice = 0;

            if (codigo.Length <= 0)
            {
                codigo = null;
            }
            proc_sw_trae_CampoMensajes_MS_DTO camposMensajes = new proc_sw_trae_CampoMensajes_MS_DTO();
            camposMensajes.codigo = codigo;
            var result = Service.GetCampoMensajes(camposMensajes);
            return Json(result.Select(i => new
            {
                id = indice++,
                formato_campo = i.formato_campo,
                largo_campo = i.largo_campo,
                linea_campo = i.linea_campo,
                nombre_campo = i.nombre_campo,
                tag_campo = i.tag_campo
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateCampoMensajes(string codigo, int linea, string nombre, string formato, int? largo)
        {
            if (nombre.Length <= 0)
            {
                nombre = null;
            }
            if (formato.Length <= 0)
            {
                formato = null;
            }
            var result = Service.UpdateCampoMensajes(codigo, linea, nombre,formato, largo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertCampoMensajes(string codigo, int linea, string nombre, string formato, int? largo)
        {
            if (nombre.Length <= 0)
            {
                nombre = null;
            }
            if (formato.Length <= 0)
            {
                formato = null;
            }
            var result = Service.InsertCampoMensajes(codigo, linea, nombre, formato, largo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteCampoMensajes(string codigo, int linea)
        {
            var result = Service.DeleteCampoMensajes(codigo, linea);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGlosaCampos(string codigo)
        {
            int indice = 0;

            if (codigo.Length <= 0)
            {
                codigo = null;
            }
            proc_sw_trae_GlosaCampos_MS_DTO glosaCampos = new proc_sw_trae_GlosaCampos_MS_DTO();
            glosaCampos.codigo = codigo;
            var result = Service.GetGlosaCampos(glosaCampos);
            return Json(result.Select(i => new
            {
                id = indice++,
                nombre_campo_tipcam = i.nombre_campo_tipcam,
                tag_campo_tipcam = i.tag_campo_tipcam,
                tipo_msg_tipcam = i.tipo_msg_tipcam
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateGlosaCampos(string codigo, string tag, string nombre)
        {
            if (nombre.Length <= 0)
            {
                nombre = null;
            }
            var result = Service.UpdateGlosaCampos(codigo, tag, nombre);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertGlosaCampos(string codigo, string tag, string nombre)
        {
            if (nombre.Length <= 0)
            {
                nombre = null;
            }
            var result = Service.InsertGlosaCampos(codigo, tag, nombre);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteGlosaCampos(string codigo, string tag)
        {
            var result = Service.DeleteGlosaCampos(codigo, tag);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCaracterInvalido(string codigo)
        {
            int indice = 0;

            if (codigo.Length <= 0)
            {
                codigo = null;
            }
            proc_sw_trae_CaracterInvalido_MS_DTO caracterInvalido = new proc_sw_trae_CaracterInvalido_MS_DTO();
            caracterInvalido.codigo = codigo;
            var result = Service.GetCaracterInvalido(caracterInvalido);
            return Json(result.Select(i => new
            {
                id = indice++,
                caracter = i.caracter,
                descripcion = i.descripcion,
                valor_ascii = i.valor_ascii
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateCaracterInvalido(int codigo, string nombre)
        {
            if (nombre.Length <= 0)
            {
                nombre = null;
            }
            var result = Service.UpdateCaracterInvalido(codigo, nombre);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertCaracterInvalido(int codigo, string caracter, string descripcion)
        {
            if (descripcion.Length <= 0)
            {
                descripcion = null;
            }
            var result = Service.InsertCaracterInvalido(codigo,caracter,descripcion);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteCaracterInvalido(int codigo)
        {
            var result = Service.DeleteCaracterInvalido(codigo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertPermiso(int rut,string aplicacion)
        {
           
            var result = Service.InsertPermisoConfiguracion(rut, aplicacion);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FormatRut(string rut)
        {
            string Rut = string.Empty;

            if (!string.IsNullOrEmpty(rut))
              Rut = formatRut(rut);
            return Json(new 
            {
                rutFormateado = Rut

            });
        }


        #region "Method Internos"


        public static string formatRut(string rut)
        {
            int cont = 0;
            string format;
            if (rut.Length == 0)
            {
                return "";
            }
            else
            {
                rut = rut.TrimStart('0');
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                format = "-" + rut.Substring(rut.Length - 1);
                for (int i = rut.Length - 2; i >= 0; i--)
                {
                    format = rut.Substring(i, 1) + format;
                    cont++;
                    if (cont == 3 && i != 0)
                    {
                        format = "." + format;
                        cont = 0;
                    }
                }
                return format;
            }
        }


        #endregion 
    }
}