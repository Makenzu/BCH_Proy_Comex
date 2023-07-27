using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Swift;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BCH.Comex.Core.BL.SWMA
{
   public class swmaService
    {
        private UnitOfWorkSwift unitOfWork;
        private UnitOfWorkCext01 unitOfWorkCext01;
        public swmaService()
        {
            this.unitOfWork = new UnitOfWorkSwift();
            this.unitOfWorkCext01 = new UnitOfWorkCext01();
        }
        public IList<proc_sw_trae_usuarios_MS_Result1> GetUsuarios(proc_sw_trae_usuarios_MS_DTO users)
        {
            return unitOfWork.MantencionSwiftRepository.proc_sw_trae_usuarios_MS(users);
        }
        public bool? UpdateUsuarios(int rut, string nombre, string tipo)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_Actualiza_usuarios_MS(rut, nombre, tipo);
                return Resultado;
            }
        }
        public bool? InsertUsuarios(int rut, int CvRut, string nombre, string tipo)
        {

            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_graba_usuarios_MS(rut, CvRut, nombre, tipo);
                return Resultado;
            }
        }
        public bool? DeleteUsuarios(int rut)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_elimina_usuarios_MS(rut);
                return Resultado;
            }
        }
        public IList<proc_sw_trae_casillas_MS_Result> GetCasillas(proc_sw_trae_casillas_MS_DTO casillas)
        {
            return unitOfWork.MantencionSwiftRepository.proc_sw_trae_casillas_MS(casillas);
        }
        public bool? UpdateCasillas(int codigo, string nombre, string origen)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_Actualiza_casillas_MS(codigo, nombre, origen);
                return Resultado;
            }
        }
        public bool? InsertCasillas(int codigo, string nombre, string origen)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_graba_casillas_MS(codigo, nombre, origen);
                return Resultado;
            }
        }
        public bool? DeleteCasillas(int codigo)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_elimina_casillas_MS(codigo);
                return Resultado;
            }
        }
        public IList<proc_sw_trae_monedas_MS_Result> GetMonedas()
        {
            return unitOfWork.MantencionSwiftRepository.proc_sw_trae_monedas_MS();
        }
        public bool? InsertMonedas(string codigoSw, int? codigoBc, string nombre, int decimales, string uso)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_graba_monedas_MS(codigoSw, codigoBc, nombre, decimales, uso);
                return Resultado;
            }
        }
        public bool? DeleteMonedas(string codigoSw)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_elimina_monedas_MS(codigoSw);
                return Resultado;
            }
        }
        public bool? UpdateMonedas(string CodigoSwMoneda, string NombreMoneda, string UsoMoneda, int? CodigoBcMoneda, int? DecimalesMoneda)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_Actualiza_monedas_MS(CodigoSwMoneda, NombreMoneda, UsoMoneda, CodigoBcMoneda, DecimalesMoneda);
                return Resultado;
            }
        }

        public IList<proc_sw_trae_bancos2_MS_Result> GetBancos(string swift, string pais, string ciudad, string banco, 
            string direccion, string branch, string intercambioClave)
        {
            return unitOfWork.MantencionSwiftRepository.proc_sw_trae_bancos2_MS(swift, pais, ciudad, banco, direccion,
                branch, intercambioClave);
        }

        public MemoryStream DescargarExcelBancos(string swift, string pais, string ciudad, string banco,
             string direccion, string branch, string intercambioClave)
        {
            using (Tracer tracer = new Tracer("ExcelBancos"))
            {
                MemoryStream stream = new MemoryStream();
                try
                {
                    using (SLDocument doc = new SLDocument())
                    {
                        doc.SelectWorksheet(SLDocument.DefaultFirstSheetName);

                        IList<proc_sw_trae_bancos2_MS_Result> bancos = unitOfWork.MantencionSwiftRepository.proc_sw_trae_bancos2_MS(swift, pais, ciudad, banco, direccion, branch, intercambioClave);

                        //titulo
                        SLStyle styleTitulo = doc.CreateStyle();
                        styleTitulo.Font.Bold = true;
                        styleTitulo.Font.FontSize = 15;
                        styleTitulo.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

                        doc.SetCellValue(1, 1, "Consulta de Bancos");
                        doc.MergeWorksheetCells("A1", "J1");
                        doc.SetCellStyle("A1", styleTitulo);

                        //filtros
                        doc.SetCellValue(3, 1, "Swift");
                        doc.SetCellValue(4, 1, "Branch");
                        doc.SetCellValue(5, 1, "Banco");
                        doc.SetCellValue(6, 1, "Clave");
                        doc.SetCellValue(3, 4, "País");
                        doc.SetCellValue(4, 4, "Ciudad");
                        doc.SetCellValue(5, 4, "Dirección");

                        doc.SetCellValue(3, 2, (String.IsNullOrEmpty(swift) ? "-- Todos --" : swift));
                        doc.SetCellValue(4, 2, (String.IsNullOrEmpty(branch) ? "-- Todos --" : branch));
                        doc.SetCellValue(5, 2, (String.IsNullOrEmpty(banco) ? "-- Todos --" : banco));
                        doc.SetCellValue(6, 2, (String.IsNullOrEmpty(intercambioClave) ? "-- Indiferente --" : intercambioClave));
                        doc.SetCellValue(3, 5, (String.IsNullOrEmpty(pais) ? "-- Todos --" : pais));
                        doc.SetCellValue(4, 5, (String.IsNullOrEmpty(ciudad) ? "-- Todos --" : ciudad));
                        doc.SetCellValue(5, 5, (String.IsNullOrEmpty(direccion) ? "-- Todos --" : direccion));

                        //Style encabezados o filtros
                        SLStyle styleEncabezados = doc.CreateStyle();
                        styleEncabezados.Font.Bold = true;
                        styleEncabezados.Font.FontSize = 13;
                        doc.SetCellStyle("A3", "A6", styleEncabezados);
                        doc.SetCellStyle("D3", "D6", styleEncabezados);
                        doc.SetRowStyle(8, styleEncabezados);

                        int colIndex = 1;
                        int rowIndex = 8;
                        doc.SetCellValue(rowIndex, colIndex++, "Código");
                        doc.SetCellValue(rowIndex, colIndex++, "Branch");
                        doc.SetCellValue(rowIndex, colIndex++, "Nombre");
                        doc.SetCellValue(rowIndex, colIndex++, "País");
                        doc.SetCellValue(rowIndex, colIndex++, "Ciudad");
                        doc.SetCellValue(rowIndex, colIndex++, "Sucursal");
                        doc.SetCellValue(rowIndex, colIndex++, "Dirección");
                        doc.SetCellValue(rowIndex, colIndex++, "Localidad");
                        doc.SetCellValue(rowIndex, colIndex++, "PDB Nr");
                        doc.SetCellValue(rowIndex, colIndex++, "Clave");

                        foreach (proc_sw_trae_bancos2_MS_Result b in bancos)
                        {
                            colIndex = 1;
                            rowIndex++;

                            doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.cod_banco) ? String.Empty : b.cod_banco.Trim()));
                            doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.branch) ? String.Empty : b.branch.Trim()));
                            doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.nombre_banco) ? String.Empty : b.nombre_banco.Trim()));
                            doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.pais_banco) ? String.Empty : b.pais_banco.Trim()));
                            doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.ciudad_banco) ? String.Empty : b.ciudad_banco.Trim()));
                            doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.oficina_banco) ? String.Empty : b.oficina_banco.Trim()));
                            doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.direccion_banco) ? String.Empty : b.direccion_banco.Trim()));
                            doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.localidad_banco) ? String.Empty : b.localidad_banco.Trim()));
                            doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.pobnr_banco) ? String.Empty : b.pobnr_banco.Trim()));
                            doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.intercambio_clave) ? String.Empty : b.intercambio_clave.Trim()));
                        }

                        doc.AutoFitColumn(1, 10);
                        doc.SaveAs(stream);
                    }

                    stream.Position = 0; //importante, dejar el stream pronto para leer;
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, no se ha podido generar archivo excel de consulta devengamiento reajuste", ex);
                }
                return stream;
            }
        }

        public IList<int?> GetBancosCount(string swift, string pais, string ciudad, string banco, string direccion, string branch, string intercambioClave)
        {
            return unitOfWork.MantencionSwiftRepository.proc_sw_trae_count_bancos_MS(swift, pais, ciudad, direccion,
                banco, branch, intercambioClave).ToList();
        }
        public bool? InsertBancos(string codigo, string branch, string nombre, string direccion, string ciudad, string pais, string oficina, string clave, string localidad, string pob)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_graba_bancos_MS(codigo, branch, nombre, direccion, ciudad, pais, oficina, clave, localidad, pob);
                return Resultado;
            }
        }
        public bool? UpdateClaveBancos(string clave, string codigo, string branch, int flag)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_Actualiza_bancos_claves_MS(clave,codigo,branch,flag);
                return Resultado;
            }
        }
        public bool? UpdateBancos(string codigo, string nombre, string direccion, string ciudad, string oficina, string clave, string localidad, string pob, string pais, string branch)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_Actualiza_bancos_MS(codigo, nombre, direccion, ciudad, oficina, clave, localidad, pob, pais, branch);
                return Resultado;
            }
        }
        public IList<proc_sw_trae_bancos_verificados_MS_Result> GetBancosVerificados(string codigo)
        {
            return unitOfWork.MantencionSwiftRepository.proc_sw_trae_bancos_verificados_MS(codigo);
        }
        public bool? DeleteBancos(string codigo,string branch,int flag)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_elimina_bancos_MS(codigo, branch, flag);
                return Resultado;
            }
        }
        public IList<proc_sw_trae_paridad_MS_Result> GetParidades(proc_sw_trae_paridades_MS_DTO paridades)
        {
            return unitOfWork.MantencionSwiftRepository.proc_sw_trae_paridad_MS(paridades);
        }
        public IList<proc_sw_trae_valoresCampos_MS_Result> GetValoresCampos(proc_sw_trae_valoresCampos_MS_DTO ValoresCampos)
        {
            return unitOfWork.MantencionSwiftRepository.proc_sw_trae_valoresCampos_MS(ValoresCampos);
        }
        public bool? UpdateValoresCampos(string tipo, string tag, string condicion, string valor, int linea, int TotalValor)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_Actualiza_valoresCampos_MS(tipo, tag, condicion, valor, linea, TotalValor);
                return Resultado;
            }
        }
        public bool? InsertValoresCampos(string codigo, string tag, int linea, string condicion, string campos, int total)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_graba_ValoresCampos_MS(codigo, tag, linea, condicion, campos, total);
                return Resultado;
            }
        }
        public bool? DeleteValoresCampos(string codigo, string tag, int linea)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_elimina_ValoresCampos_MS(codigo, tag, linea);
                return Resultado;
            }
        }
        public IList<proc_sw_trae_TiposMensajes_MS_Result> GetTiposMensajes(proc_sw_trae_TiposMensajes_MS_DTO tipoMensaje)
        {
            return unitOfWork.MantencionSwiftRepository.proc_sw_trae_TiposMensajes_MS(tipoMensaje);
        }
        public bool? UpdateTiposMensjaes(string codigo, string nombre)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_Actualiza_TiposMensajes_MS(codigo, nombre);
                return Resultado;
            }
        }
        public bool? InsertTiposMensjaes(string codigo, string nombre)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_graba_TiposMensajes_MS(codigo, nombre);
                return Resultado;
            }
        }
        public bool? DeleteTiposMensjaes(string codigo)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_elimina_TiposMensajes_MS(codigo);
                return Resultado;
            }
        }
        public IList<proc_sw_trae_FormatoMensajes_MS_Result> GetFormatoMensajes(proc_sw_trae_FormatoMensajes_MS_DTO formatoMensajes)
        {
            return unitOfWork.MantencionSwiftRepository.proc_sw_trae_FormatoMensajes_MS(formatoMensajes);
        }
        public bool? UpdateFormatoMensajes(string codigo, int orden, string secuencia, int repeticion, string tag, string status, int ordenOriginal, string tagOriginal)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_Actualiza_FormatoMensajes_MS(codigo, orden, secuencia, repeticion, tag, status, ordenOriginal, tagOriginal);
                return Resultado;
            }
        }
        public bool? InsertFormatoMensajes(string codigo, int orden, string secuencia, int repeticion, string tag, string status)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_graba_FormatoMensajes_MS(codigo, orden, secuencia, repeticion, tag, status);
                return Resultado;
            }
        }
        public bool? DeleteFormatoMensajes(string codigo, int orden, string tag)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_elimina_FormatoMensajes_MS(codigo, orden,tag);
                return Resultado;
            }
        }
        public IList<proc_sw_trae_CampoMensajes_MS_Result> GetCampoMensajes(proc_sw_trae_CampoMensajes_MS_DTO camposMensajes)
        {
            return unitOfWork.MantencionSwiftRepository.proc_sw_trae_CampoMensajes_MS(camposMensajes);
        }
        public bool? UpdateCampoMensajes(string codigo, int linea, string nombre, string formato, int? largo)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_Actualiza_CampoMensajes_MS(codigo, linea, nombre,formato, largo);
                return Resultado;
            }
        }
        public bool? InsertCampoMensajes(string codigo, int linea, string nombre, string formato, int? largo)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_graba_CampoMensajes_MS(codigo, linea, nombre, formato, largo);
                return Resultado;
            }
        }
        public bool? DeleteCampoMensajes(string codigo, int linea)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_elimina_CampoMensajes_MS(codigo, linea);
                return Resultado;
            }
        }
        public IList<proc_sw_trae_GlosaCampos_MS_Result> GetGlosaCampos(proc_sw_trae_GlosaCampos_MS_DTO glosaCampos)
        {
            return unitOfWork.MantencionSwiftRepository.proc_sw_trae_GlosaCampos_MS(glosaCampos);
        }
        public bool? UpdateGlosaCampos(string codigo, string tag, string nombre)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_Actualiza_GlosaCampos_MS(codigo,tag,nombre);
                return Resultado;
            }
        }
        public bool? InsertGlosaCampos(string codigo, string tag, string nombre)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_graba_GlosaCampos_MS(codigo, tag, nombre);
                return Resultado;
            }
        }
        public bool? DeleteGlosaCampos(string codigo, string tag)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_elimina_GlosaCampos_MS(codigo, tag);
                return Resultado;
            }
        }
        public IList<proc_sw_trae_CaracterInvalido_MS_Result> GetCaracterInvalido(proc_sw_trae_CaracterInvalido_MS_DTO caracterInvalido)
        {
            return unitOfWork.MantencionSwiftRepository.proc_sw_trae_CaracterInvalido_MS(caracterInvalido);
        }
        public bool? UpdateCaracterInvalido(int codigo, string nombre)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_Actualiza_CaracterInvalido_MS(codigo, nombre);
                return Resultado;
            }
        }
        public bool? InsertCaracterInvalido(int codigo, string caracter, string descripcion)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_graba_CaracterInvalido_MS(codigo,caracter,descripcion);
                return Resultado;
            }
        }
        public bool? DeleteCaracterInvalido(int codigo)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool? Resultado = uowSwift.SwRepository.proc_sw_elimina_CaracterInvalido_MS(codigo);
                return Resultado;
            }
        }
        public int InsertPermisoConfiguracion(int rut,string aplicacion)
        {
             int Resultado = 5;//asigno 5 a si esta duplicado
            if (!ExistePermisosConfiguracion(rut, aplicacion))
            {
                using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
                {
                     Resultado = uowSwift.SwRepository.proc_sw_config_ing_MS(rut, aplicacion);
                   
                }
            }
            return Resultado;

        }

        public bool ExistePermisosConfiguracion(int rut, string aplicacion)
        {
            using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
            {
                bool Resultado = uowSwift.SwRepository.ExisteConfiguraciondeCasilla(rut, aplicacion);
                return Resultado;
            }
        }

    }
}
