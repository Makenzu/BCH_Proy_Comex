using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.SWI102;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Sbcor;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Sbcor;
using BCH.Comex.Data.DAL.Swift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System.IO;

namespace BCH.Comex.Core.BL.SWSE
{
    public class EnvioSwiftService : IDisposable
    {
        private UnitOfWorkSwift uow;
        private UnitOfWorkCext01 uowCext01;

        public EnvioSwiftService()
        {
            uow = new UnitOfWorkSwift();
            uowCext01 = new UnitOfWorkCext01();
        }

        public void Dispose()
        {
            if (uow != null)
            {
                uow.Dispose();
            }
        }

        public IList<sw_bancos> BuscarBancos(string swift, string pais, string ciudad, string banco, string direccion)
        {
            try
            {
                // Proceso de sanitización
                //swift = Utilidad.SanitizarString(swift);
                pais = Utilidad.SanitizarString(pais);
                ciudad = Utilidad.SanitizarString(ciudad);
                banco = Utilidad.SanitizarString(banco);
                direccion = Utilidad.SanitizarString(direccion);
                // Fin de sanitización

                if (swift == "AVOID_SEARCH" //||
                                            //    (string.IsNullOrWhiteSpace(swift)
                                            //    && string.IsNullOrWhiteSpace(pais)
                                            //    && string.IsNullOrWhiteSpace(ciudad)
                                            //    && string.IsNullOrWhiteSpace(banco)
                                            //    && string.IsNullOrWhiteSpace(direccion) )
                    )
                {
                    return new List<sw_bancos>();
                }

                //Si el swift es de 11 caracteres, se presupone que los 3 ultimos son el branch y los 9 primeros el base
                var branch = string.Empty;
                if (swift.Length == 11)
                {
                    branch = swift.Substring(swift.Length - 3);
                    swift = swift.Substring(0, swift.Length - 3);
                }
                var result = uow.BancoRepository.proc_sw_trae_bancos((swift ?? "").Trim().ToUpper(), (pais ?? "").Trim().ToUpper(),
                    (ciudad ?? "").Trim().ToUpper(), (banco ?? "").Trim().ToUpper(), (direccion ?? "").Trim().ToUpper(), branch.ToUpper());

                return result;
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("BuscarBancos"));
            }         
        }

        public int CuentaBancos(string swift, string pais, string ciudad, string banco, string direccion)
        {
            try
            {
                //Proceso de sanitización
               // swift = Utilidad.SanitizarString(swift);
                pais = Utilidad.SanitizarString(pais);
                ciudad = Utilidad.SanitizarString(ciudad);
                banco = Utilidad.SanitizarString(banco);
                direccion = Utilidad.SanitizarString(direccion);
                //Fin proceso
                if (swift == "AVOID_SEARCH")
                {
                    return 0;
                }

                //Si el swift es de 11 caracteres, se presupone que los 3 ultimos son el branch y los 9 primeros el base
                var branch = string.Empty;
                if (swift.Length == 11)
                {
                    branch = swift.Substring(swift.Length - 3);
                    swift = swift.Substring(0, swift.Length - 3);
                }
                var result = uow.BancoRepository.proc_sw_count_bancos((swift ?? "").Trim().ToUpper(), (pais ?? "").Trim().ToUpper(),
                    (ciudad ?? "").Trim().ToUpper(), (banco ?? "").Trim().ToUpper(), (direccion ?? "").Trim().ToUpper(), branch.ToUpper());

                return (int)result;
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("CuentaBancos"));
            }
       
        }

        public MemoryStream ExcelBancos(string swift, string pais, string ciudad, string banco, string direccion)
        {
            try
            {
                //Proceso de sanitización
               // swift = Utilidad.SanitizarString(swift);
                pais = Utilidad.SanitizarString(pais);
                ciudad = Utilidad.SanitizarString(ciudad);
                banco = Utilidad.SanitizarString(banco);
                direccion = Utilidad.SanitizarString(direccion);
                //Fin proceso
                using (Tracer tracer = new Tracer("ExcelBancos"))
                {
                    MemoryStream stream = new MemoryStream();
                    try
                    {
                        using (SLDocument doc = new SLDocument())
                        {
                            doc.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Lista Bancos");

                            IList<sw_bancos> bancos = BuscarBancos(swift, pais, ciudad, banco, direccion);

                            //titulo
                            SLStyle styleTitulo = doc.CreateStyle();
                            styleTitulo.Font.Bold = true;
                            styleTitulo.Font.FontSize = 15;
                            styleTitulo.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

                            doc.SetCellValue(1, 1, "Consulta de Banca Mundial");
                            doc.MergeWorksheetCells("A1", "H1");
                            doc.SetCellStyle("A1", styleTitulo);


                            //Style encabezados o filtros
                            SLStyle styleEncabezados = doc.CreateStyle();
                            styleEncabezados.Font.Bold = true;
                            styleEncabezados.Font.FontSize = 13;
                            doc.SetCellStyle("A2", "H2", styleEncabezados);


                            int colIndex = 1;
                            int rowIndex = 2;
                            doc.SetCellValue(rowIndex, colIndex++, "Código Banco");
                            doc.SetCellValue(rowIndex, colIndex++, "Branch");
                            doc.SetCellValue(rowIndex, colIndex++, "Nombre de Banco");
                            doc.SetCellValue(rowIndex, colIndex++, "Dirección");
                            doc.SetCellValue(rowIndex, colIndex++, "Ciudad");
                            doc.SetCellValue(rowIndex, colIndex++, "País");
                            doc.SetCellValue(rowIndex, colIndex++, "Oficina");
                            doc.SetCellValue(rowIndex, colIndex++, "Intercambio Clave");

                            doc.ImportDataTable(++rowIndex, 1, ConvertToDataTable(bancos.Select(c => new { c.cod_banco, c.branch, c.nombre_banco, c.direccion_banco, c.ciudad_banco, c.pais_banco, c.oficina_banco, c.intercambio_clave }).ToList()), false);

                            //foreach (sw_bancos b in bancos)
                            //{
                            //    colIndex = 1;
                            //    rowIndex++;

                            //    doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.cod_banco) ? String.Empty : b.cod_banco.Trim()));
                            //    doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.branch) ? String.Empty : b.branch.Trim()));
                            //    doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.nombre_banco) ? String.Empty : b.nombre_banco.Trim()));
                            //    doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.direccion_banco) ? String.Empty : b.direccion_banco.Trim()));
                            //    doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.ciudad_banco) ? String.Empty : b.ciudad_banco.Trim()));
                            //    doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.pais_banco) ? String.Empty : b.pais_banco.Trim()));
                            //    doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.oficina_banco) ? String.Empty : b.oficina_banco.Trim()));
                            //    doc.SetCellValue(rowIndex, colIndex++, (String.IsNullOrEmpty(b.intercambio_clave) ? String.Empty : b.intercambio_clave.Trim()));
                            //}

                            doc.AutoFitColumn(1, 8);

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
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("ExcelBancos"));
            }
          
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
                DataTable table = new DataTable();
                foreach (PropertyDescriptor prop in properties)
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    table.Rows.Add(row);
                }
                return table;
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("ConvertToDataTable"));
            }          
        }

        public IList<sw_tipos_msg> GetTipoMensajesConFormato()
        {
            try
            {
                IList<sw_tipos_msg> tipos = uow.TiposMensajeRepository.GetTipoMensajesConFormato();
                foreach (sw_tipos_msg tipo in tipos)
                {
                    tipo.cod_tipo = tipo.cod_tipo.Trim();
                }

                return tipos; 
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("GetTipoMensajesConFormato"));
            }          
        }

        public IList<sw_monedas> GetMonedas()
        {
            try
            {
                return uow.MonedaRepository.GetMonedasUsoBanco();
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("GetMonedas"));
            }            
        }

        public IList<CampoMonto> GetCamposMontos()
        {
            try
            {
                List<CampoMonto> camposMonto = new List<CampoMonto>();
                camposMonto.Add(new CampoMonto()
                {
                    Campo = "32A",
                    PosMto = 10,
                    LenMto = 15,
                    PosMnd = 7,
                    LenMnd = 3,
                    TipoVal = 1
                });

                camposMonto.Add(new CampoMonto()
                {
                    Campo = "32B",
                    PosMto = 4,
                    LenMto = 15,
                    PosMnd = 1,
                    LenMnd = 3,
                    TipoVal = 1
                });

                camposMonto.Add(new CampoMonto()
                {
                    Campo = "33A",
                    PosMto = 10,
                    LenMto = 15,
                    PosMnd = 7,
                    LenMnd = 3,
                    TipoVal = 1
                });

                camposMonto.Add(new CampoMonto()
                {
                    Campo = "33B",
                    PosMto = 4,
                    LenMto = 15,
                    PosMnd = 1,
                    LenMnd = 3,
                    TipoVal = 1
                });

                camposMonto.Add(new CampoMonto()
                {
                    Campo = "34A",
                    PosMto = 10,
                    LenMto = 15,
                    PosMnd = 7,
                    LenMnd = 3,
                    TipoVal = 1
                });

                camposMonto.Add(new CampoMonto()
                {
                    Campo = "34B",
                    PosMto = 4,
                    LenMto = 15,
                    PosMnd = 1,
                    LenMnd = 3,
                    TipoVal = 1
                });
                camposMonto.Add(new CampoMonto()
                {
                    Campo = "71F",
                    PosMto = 4,
                    LenMto = 15,
                    PosMnd = 1,
                    LenMnd = 3,
                    TipoVal = 0
                });


                return camposMonto;
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("GetCamposMontos"));
            }
           
        }

        public IList<sw_caracter_error> GetCaracteresError()
        {
            try
            {
                return uow.CaracterErrorRepository.LlenaMatrizCaracter();
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("CuentaBancos"));
            }            
        }

        public IList<sw_caracter_error> GetCaracteresError_Z()
        {
            try
            {
                return uow.CaracterErrorRepository.LlenaMatrizCaracterZ();
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("GetCaracteresError_Z"));
            }            
        }

        public sw_bancos ObtenerBancoPorSwift(string swift)
        {
            try
            {
                return uow.BancoRepository.GetBancosByCodAndBranch(swift.Substring(0, 8), swift.Substring(8, 3)).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("ObtenerBancoPorSwift"));
            }                        
        }

        public List<LineaEditorMensajeSwift> GetLineasYFormatosParaMT(string codMT)
        {
            try
            {
                codMT = Utilidad.SanitizarString(codMT);

                List<ProcSwTraeFmtCampos> formatos = uow.SwRepository.TraeFormatoMsg(codMT);
                List<LineaEditorMensajeSwift> lineas = new List<LineaEditorMensajeSwift>();

                LineaEditorMensajeSwift ultimaLinea = null;

                List<int> camposUnicos = formatos.Select(f => f.Orden.Value).Distinct().ToList();

                foreach (int orden in camposUnicos)
                {
                    List<ProcSwTraeFmtCampos> formatosCampo = formatos.Where(f => f.Orden == orden).ToList();
                    List<string> variantes = formatosCampo.Select(f => f.Tag).Distinct().ToList();

                    ProcSwTraeFmtCampos formato = formatosCampo.FirstOrDefault();

                    if (formato.Tag.Contains("57"))
                    {
                        var p = "hola";
                    }

                    LineaEditorMensajeSwift linea = new LineaEditorMensajeSwift()
                    {
                        CodMT = codMT,
                        CodCam = formato.Tag,
                        Descripcion = (String.IsNullOrEmpty(formato.Nombre) ? String.Empty : formato.Nombre.ToLower()),
                        Formato = TransformarFormato(formato.Formato),
                        LenLinea = (formato.Largo.HasValue ? (short)formato.Largo.Value : (short)0),
                        NumLineas = (short)formato.Linea.Value,
                        Obligatorio = (formato.Status == "M" ? true : false),
                        Orden = (formato.Orden.HasValue ? formato.Orden.Value : 1),
                        Linea = formato.Linea.Value,
                        Secuencia = formato.Secuencia
                    };

                    //linea.Incluido = linea.Obligatorio; //si es obligatorio entonces no tiene la opcion de no incluirlo

                    if (variantes.Count > 1)
                    {
                        linea.Variantes = variantes;
                        linea.CodCam = formato.Tag.Substring(0, 2);
                        int indexOfSeparador = Math.Max(linea.Descripcion.IndexOf("-"), linea.Descripcion.IndexOf("("));
                        if (indexOfSeparador > 0)
                        {
                            linea.Descripcion = linea.Descripcion.Substring(0, indexOfSeparador - 1).Trim();
                        }

                        foreach (ProcSwTraeFmtCampos otroFormato in formatosCampo)
                        {
                            LineaSecundariaEditorMensajeSwift lineaSecundaria = new LineaSecundariaEditorMensajeSwift()
                            {
                                Descripcion = (String.IsNullOrEmpty(otroFormato.Nombre) ? String.Empty : otroFormato.Nombre.ToLower()),
                                CodCam = otroFormato.Tag,
                                Formato = TransformarFormato(otroFormato.Formato),
                                LenLinea = (otroFormato.Largo.HasValue ? (short)otroFormato.Largo.Value : (short)0),
                                Linea = otroFormato.Linea.Value,
                            };

                            linea.LineasSecundarias.Add(lineaSecundaria);
                        }
                    }
                    else
                    {
                        if (formatosCampo.Count > 1)
                        {
                            //no hay variantes pero si es un campo multilínea
                            foreach (ProcSwTraeFmtCampos otroFormato in formatosCampo)
                            {
                                if (otroFormato != formato) //tengo que hacer esta comprobacion para ignorar el primer elemento que ya lo consideré
                                {
                                    LineaSecundariaEditorMensajeSwift lineaSecundaria = new LineaSecundariaEditorMensajeSwift()
                                    {
                                        Descripcion = (String.IsNullOrEmpty(otroFormato.Nombre) ? String.Empty : otroFormato.Nombre.ToLower()),
                                        CodCam = otroFormato.Tag,
                                        Formato = TransformarFormato(otroFormato.Formato),
                                        LenLinea = (otroFormato.Largo.HasValue ? (short)otroFormato.Largo.Value : (short)0),
                                        Linea = otroFormato.Linea.Value
                                    };

                                    linea.LineasSecundarias.Add(lineaSecundaria);
                                }
                            }
                        }
                    }

                    lineas.Add(linea);
                    ultimaLinea = linea;
                }

                return lineas;
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("GetLineasYFormatosParaMT"));
            }
        
        }

        /// <summary>
        /// Verifica si los valores de los campos y si los caracteres de las lineas del mensaje son validos
        /// </summary>
        /// <param name="lineas">Lineas del mensaje</param>
        /// <returns>Verdadero si es valido</returns>
        public bool ValidarLineasMensaje(IList<LineaEditorMensajeSwift> lineas)
        {
            try
            {
                bool todoOK = true;

                List<sw_valor_campos> valoresCampos = uow.ValorCamposRepository.LlenaMatrizValores(lineas.First().CodMT);
                foreach (LineaEditorMensajeSwift linea in lineas)
                {
                    if (linea.Incluido)
                    {
                        bool result = ValidarLineaMensaje(linea, valoresCampos);
                        if (!result)
                        {
                            todoOK = false;
                        }
                    }
                }

                if (todoOK)
                {
                    LimpiarErroresEnLineas(lineas);

                    todoOK = ValidarCaracteresLineas(lineas);

                    //TODO: Cambios aplicados el 12-07-2021                                
                    var codMT = lineas.First().CodMT;

                    switch (codMT)
                    {
                        case "MT760":
                            todoOK = this.ValidarMT760(lineas.ToList());
                            break;
                        case "MT761":
                            todoOK = this.ValidarMT761(lineas.ToList());
                            break;
                        case "MT765":
                            todoOK = this.ValidarMT765(lineas.ToList());
                            break;
                        case "MT767":
                            todoOK = this.ValidarMT767(lineas.ToList());
                            break;
                        case "MT768":
                            todoOK = this.ValidarMT768(lineas.ToList());
                            break;
                        case "MT769":
                            todoOK = this.ValidarMT769(lineas.ToList());
                            break;
                        case "MT775":
                            todoOK = this.ValidarMT775(lineas.ToList());
                            break;
                        case "MT785":
                            todoOK = this.ValidarMT785(lineas.ToList());
                            break;
                        default:
                            break;
                    }



                    //Fin Cambios 12-07-2021
                }

                return todoOK;
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("ValidarLineasMensaje"));
            }
           
        }


        /// <summary>
        /// Se genera validación correspondiente al MT760
        /// </summary>
        private bool ValidarMT760(List<LineaEditorMensajeSwift> lineas)
        {
            var todoOk = true;

            var secuencias = new string[] { "A", "B", "C" };

            //******* Se recorren las 3 secuencias para optimizar el código y las validaciones *******//
            foreach (var s in secuencias)
            {
                //******* Mensajes que se mostrarán en pantalla en caso que ocurra un error *******//
                var mensajeC1 = string.Format("En la secuencia {0}, si el campo 23B es FIXD, entonces el campo 31E debe estar presente, si el campo 23B es COND, entonces el campo 31E, puede estar presente, de lo contrario, el campo 31E no está permitido.", s);
                var mensajeC2 = string.Format("En la secuencia {0}, si el campo 23B es COND, entonces el campo 35G debe estar presente; de ​​lo contrario, el campo 35G no está permitido.", s);
                var mensajeC3 = string.Format("En la secuencia {0}, si el campo 23B es OPEN, el campo 23F no está permitido.", s);
                var mensajeC4 = string.Format("En la secuencia {0}, si el campo 22A es ISSU, entonces en la secuencia B el campo 50 debe estar presente.", s);
                var mensajeC5 = string.Format("En la secuencia {0}, si el campo 22A es ISSU y en la secuencia B el campo 22D es SBTY, entonces el campo 49 de la secuencia B, debe estar presente.", s);
                var mensajeC5_1 = string.Format("En la secuencia {0}, si el campo 22D es DGAR, entonces el campo 49 no está permitido.", s);
                var mensajeC6 = string.Format("En la secuencia {0}, si el campo 22A es ISCO o ICCO, entonces, en la secuencia B los campos 48D, 24E y 24G no están permitidos.", s);
                var mensajeC6_1 = string.Format("En la secuencia {0}, si el campo 22A es ISCO o ICCO, entonces la secuencia C debe estar presente, de lo contrario, la secuencia C no está permitida.", s);
                var mensajeC7 = string.Format("En la secuencia {0} si el campo 57a está presente, entonces el campo 56a también debe estarlo.", s);
                var mensajeC8 = string.Format("En la secuencia {0} si el campo 23F está ausente, los campos 78, 26E y 31S no están permitidos.", s);
                var mensajeC9 = string.Format("En la secuencia B, si el campo 49 es CONFIRM o es MAY ADD, entonces el campo 58a debe estar presente, de lo contrario, no debe estarlo.");
                var mensajeC10 = string.Format("En la secuencia C, si el campo 22Y está presente, entonces el campo 22K también debe estarlo.");
                var mensajeC11 = string.Format("En la secuencia B, si el campo 22D es DGAR, entonces el campo 41 no está permitido.");


                //******* Se declaran los mensajes que se ocuparán en más de una secuencia *******//
                var res23B = lineas.FirstOrDefault(l => l.CodCam == "23B" && l.Secuencia == s);
                var res31E = lineas.FirstOrDefault(l => l.CodCam == "31E" && l.Secuencia == s);
                var res35G = lineas.FirstOrDefault(l => l.CodCam == "35G" && l.Secuencia == s);
                var res23F = lineas.FirstOrDefault(l => l.CodCam == "23F" && l.Secuencia == s);
                var res22D = lineas.FirstOrDefault(l => l.CodCam == "22D" && l.Secuencia == "B");
                var res49 = lineas.FirstOrDefault(l => l.CodCam == "49" && l.Secuencia == "B");
                

                //******* Acá comienzan las validaciones como tal basadas en el PDF *******//                
                if (s == "B" || s == "C")
                {
                    if (res23B.Detalle != null)
                    {
                        // C1
                        if (res23B.Detalle == "FIXD" || res23B.Detalle == "COND")
                        {
                            if (res31E.Detalle == null && res23B.Detalle == "FIXD")
                            {
                                res31E.MensajeError = mensajeC1;
                                res31E.TieneErrorDetalle = true;

                                todoOk = false;
                            }

                            // C2
                            if (res23B.Detalle == "COND")
                            {
                                if (res35G.Detalle == null)
                                {
                                    res35G.MensajeError = mensajeC2;
                                    res35G.TieneErrorDetalle = true;

                                    todoOk = false;
                                }
                            }
                            else
                            {
                                if (res35G.Detalle != null)
                                {
                                    res35G.MensajeError = mensajeC2;
                                    res35G.TieneErrorDetalle = true;

                                    todoOk = false;
                                }
                            }
                        }
                        else
                        {
                            if (res31E.Detalle != null)
                            {
                                res31E.MensajeError = mensajeC1;
                                res31E.TieneErrorDetalle = true;

                                todoOk = false;
                            }

                            if (res35G.Detalle != null)
                            {
                                res35G.MensajeError = mensajeC2;
                                res35G.TieneErrorDetalle = true;

                                todoOk = false;
                            }

                            //C3
                            if (res23B.Detalle == "OPEN")
                            {
                                if (res23F.Detalle != null)
                                {
                                    res23F.MensajeError = mensajeC3;
                                    res23F.TieneErrorDetalle = true;

                                    todoOk = false;
                                }
                            }
                        }
                    }

                    if (res23F.Detalle == null)
                    {
                        var res78 = lineas.FirstOrDefault(l => l.CodCam == "78" && l.Secuencia == s);
                        var res26E = lineas.FirstOrDefault(l => l.CodCam == "26E" && l.Secuencia == s);
                        var res31S = lineas.FirstOrDefault(l => l.CodCam == "31S" && l.Secuencia == s);

                        if (res78.Detalle != null)
                        {
                            res78.MensajeError = mensajeC8;
                            res78.TieneErrorDetalle = true;

                            todoOk = false;
                        }

                        if (res26E.Detalle != null)
                        {
                            res26E.MensajeError = mensajeC8;
                            res26E.TieneErrorDetalle = true;

                            todoOk = false;
                        }

                        if (res31S.Detalle != null)
                        {
                            res31S.MensajeError = mensajeC8;
                            res31S.TieneErrorDetalle = true;

                            todoOk = false;
                        }
                    }

                    if (s == "B")
                    {
                        if (res22D.Detalle == "DGAR")
                        {
                            if (res49.Detalle != null)
                            {
                                res49.MensajeError = mensajeC5_1;
                                res49.TieneErrorDetalle = true;

                                todoOk = false;
                            }

                            var lst41 = lineas.FirstOrDefault(l => l.Secuencia == s && l.CodCam == "41");
                            var existe41a = lst41.LineasSecundarias.Exists(l => (l.Detalle != null && l.Detalle != "") && l.CodCam == "41A");
                            
                            if (existe41a)
                            {
                                foreach (var item in lst41.LineasSecundarias.Where(l => (l.Detalle != null && l.Detalle != "") && l.CodCam == "41A"))
                                {
                                    item.MensajeError = mensajeC11;
                                    item.TieneErrorDetalle = true;
                                }
                                
                                todoOk = false;
                            }
                        }

                        var lst57A = lineas.FirstOrDefault(l => l.Secuencia == s && l.CodCam == "57");
                        var res57A = lst57A.LineasSecundarias.Exists(x => (x.Detalle != null && x.Detalle != "") && x.CodCam == "57A"); // lst57A.LineasSecundarias.FirstOrDefault(l => l.CodCam == "57A");
                        if (res57A)
                        {
                            var lst56A = lineas.FirstOrDefault(l => l.Secuencia == s && l.CodCam == "56");
                            var res56A = lst56A.LineasSecundarias.Exists(x => (x.Detalle != null && x.Detalle != "") && x.CodCam == "56A");
                            if (!res56A)
                            {
                                foreach (var item in lst56A.LineasSecundarias.Where(x => x.CodCam == "56A"))
                                {
                                    item.MensajeError = mensajeC7;
                                    item.TieneErrorDetalle = true;
                                }

                                foreach (var item in lst57A.LineasSecundarias.Where(x => x.Detalle != null && x.CodCam == "57A"))
                                {
                                    item.MensajeError = mensajeC7;
                                    item.TieneErrorDetalle = true;
                                }
                                
                                todoOk = false;
                            }
                        }

                        var lst58 = lineas.FirstOrDefault(l => l.CodCam == "58" && l.Secuencia == s);
                        
                        if (res49.Detalle == "CONFIRM" || res49.Detalle == "MAY ADD")
                        {
                            foreach (var item in lst58.LineasSecundarias.Where(l => l.Detalle == null && l.CodCam == "58A"))
                            {
                                item.MensajeError = mensajeC9;
                                item.TieneErrorDetalle = true;

                                todoOk = false;
                            }
                            
                        } else
                        {
                            foreach (var item in lst58.LineasSecundarias.Where(l => l.Detalle != null && l.CodCam == "58A"))
                            {

                                item.MensajeError = mensajeC9;
                                item.TieneErrorDetalle = true;

                                todoOk = false;
                            }                            
                        }
                    }  
                    
                    if (s == "C")
                    {
                        var res22Y = lineas.FirstOrDefault(l => l.CodCam == "22Y" && l.Secuencia == s);
                        if (res22Y.Detalle != null)
                        {
                            var res22K = lineas.FirstOrDefault(l => l.CodCam == "22K" && l.Secuencia == s);
                            if (res22K.Detalle == null)
                            {
                                res22K.MensajeError = mensajeC10;
                                res22K.TieneErrorDetalle = true;

                                todoOk = false;
                            }
                        }
                    }
                }
                else  //******* Por defecto siempre que  s = "A" entrará acá *******//  
                {
                    var existeSecuenciaC = lineas.Exists(l => l.Secuencia == "C" && l.Detalle != null);
                    // C4
                    var res22A = lineas.FirstOrDefault(l => l.CodCam == "22A" && l.Secuencia == s);

                    if (res22A.Detalle == "ISSU")
                    {
                        var res50 = lineas.FirstOrDefault(l => l.CodCam == "50" && l.Secuencia == "B");
                        if (res50.Detalle == null)
                        {
                            res50.MensajeError = mensajeC4;
                            res50.TieneErrorDetalle = true;

                            todoOk = false;
                        }

                        // C5                        
                        if (res22D.Detalle == "STBY")
                        {                            
                            if (res49.Detalle == null)
                            {
                                res49.MensajeError = mensajeC5;
                                res49.TieneErrorDetalle = true;

                                todoOk = false;
                            }
                        }

                        if (existeSecuenciaC)
                        {
                            foreach (var item in lineas.Where(l => l.Secuencia == "C" && l.Detalle != null))
                            {
                                item.MensajeError = mensajeC6_1;
                                item.TieneErrorDetalle = true;
                            }

                            todoOk = false;
                        }

                    } else if (res22A.Detalle == "ISCO" || res22A.Detalle == "ICCO")
                    {
                        // C6
                        var res48D = lineas.FirstOrDefault(l => l.CodCam == "48D" && l.Secuencia == "B");
                        var res24E = lineas.FirstOrDefault(l => l.CodCam == "24E" && l.Secuencia == "B");
                        var res24G = lineas.FirstOrDefault(l => l.CodCam == "24G" && l.Secuencia == "B");

                        if (res48D.Detalle != null)
                        {
                            res48D.MensajeError = mensajeC6;
                            res48D.TieneErrorDetalle = true;

                            todoOk = false;
                        }

                        if (res24E.Detalle != null)
                        {
                            res24E.MensajeError = mensajeC6;
                            res24E.TieneErrorDetalle = true;

                            todoOk = false;
                        }

                        if (res24G.Detalle != null)
                        {
                            res24G.MensajeError = mensajeC6;
                            res24G.TieneErrorDetalle = true;

                            todoOk = false;
                        }

                        // C6_1
                        if (!existeSecuenciaC)
                        {
                            res22A.MensajeError = mensajeC6_1;
                            res22A.TieneErrorDetalle = true;

                            todoOk = false;

                        }

                        var res15C = lineas.FirstOrDefault(l => l.Secuencia == "C" && l.CodCam == "15C");
                        if (res15C.Detalle == null)
                        {
                            res15C.MensajeError = "El campo 15C de la secuencia C es obligatorio.";
                            res15C.TieneErrorDetalle = true;

                            todoOk = false;
                        }

                        var res22D_C = lineas.FirstOrDefault(l => l.Secuencia == "C" && l.CodCam == "22D");
                        if (res22D_C.Detalle == null)
                        {
                            res22D_C.MensajeError = "El campo 22D de la secuencia C es obligatorio.";
                            res22D_C.TieneErrorDetalle = true;

                            todoOk = false;
                        }

                        var res40C = lineas.FirstOrDefault(l => l.Secuencia == "C" && l.CodCam == "40C");
                        if (res40C.Detalle == null)
                        {
                            res40C.MensajeError = "El campo 40C de la secuencia C es obligatorio.";
                            res40C.TieneErrorDetalle = true;

                            todoOk = false;
                        }

                        var res23B_C = lineas.FirstOrDefault(l => l.Secuencia == "C" && l.CodCam == "23B");
                        if (res23B_C.Detalle == null)
                        {
                            res23B_C.MensajeError = "El campo 23B de la secuencia C es obligatorio.";
                            res23B_C.TieneErrorDetalle = true;

                            todoOk = false;
                        }

                        var res50_C = lineas.FirstOrDefault(l => l.Secuencia == "C" && l.CodCam == "50");
                        if (res50_C.Detalle == null)
                        {
                            res50_C.MensajeError = "El campo 50 de la secuencia C es obligatorio.";
                            res50_C.TieneErrorDetalle = true;

                            todoOk = false;
                        }

                        var res59_C = lineas.FirstOrDefault(l => l.Secuencia == "C" && l.CodCam == "59");
                        if (res59_C.Detalle == null)
                        {
                            res59_C.MensajeError = "El campo 59 de la secuencia C es obligatorio.";
                            res59_C.TieneErrorDetalle = true;

                            todoOk = false;
                        }

                        var res32B_C = lineas.FirstOrDefault(l => l.Secuencia == "C" && l.CodCam == "32B");
                        if (res32B_C.Detalle == null)
                        {
                            res32B_C.MensajeError = "El campo 32B de la secuencia C es obligatorio.";
                            res32B_C.TieneErrorDetalle = true;

                            todoOk = false;
                        }

                        var res45L_C = lineas.FirstOrDefault(l => l.Secuencia == "C" && l.CodCam == "45L");
                        if (res45L_C.Detalle == null)
                        {
                            res45L_C.MensajeError = "El campo 45L de la secuencia C es obligatorio.";
                            res45L_C.TieneErrorDetalle = true;

                            todoOk = false;
                        }

                    } else
                    {
                        if (existeSecuenciaC)
                        {
                            foreach (var item in lineas.Where(l => l.Secuencia == "C" && l.Detalle != null))
                            {
                                item.MensajeError = mensajeC6_1;
                                item.TieneErrorDetalle = true;
                            }

                            todoOk = false;
                        }
                    }


                }


            }

            return todoOk;
        }

        /// <summary>
        /// Se genera validación correspondiente al MT768
        /// </summary>
        private bool ValidarMT768(List<LineaEditorMensajeSwift> lineas)
        {
            var todoOK = true;
            var MT768_25_Exist = lineas.ToList().Exists(l => (l.Detalle != null && l.Detalle.Length > 0) && l.CodCam == "25");
            var lst57 = lineas.FirstOrDefault(x => x.CodCam == "57");
            if (lst57 != null)
            {
                var MT768_57a_Exist = lst57.LineasSecundarias.Exists(l => (l.Detalle != null && l.Detalle.Length > 0) && l.CodCam == "57A");
                if (MT768_25_Exist && MT768_57a_Exist) //  C1
                {
                    var res = lineas.FirstOrDefault(l => l.CodCam == "25");
                    if (res != null)
                    {
                        res.MensajeError = "Puede estar presente el campo 25 o 57A, pero no ambos";
                        res.TieneErrorDetalle = true;
                    }
                    foreach (var l in lst57.LineasSecundarias.Where(l => (l.Detalle != null && l.Detalle.Length > 0) && l.CodCam == "57A"))
                    {
                        l.MensajeError = "Puede estar presente el campo 25 o 57A, pero no ambos";
                        l.TieneErrorDetalle = true;
                    }
                    todoOK = false;
                }

                var lst32 = lineas.FirstOrDefault(x => x.CodCam == "32");
                if (lst32 != null)
                {
                    var MT768_32D_Exist = lst32.LineasSecundarias.Exists(l => (l.Detalle != null && l.Detalle.Length > 0) && l.CodCam == "32D");
                    if (MT768_32D_Exist && MT768_57a_Exist) // C2
                    {
                        foreach (var l in lst32.LineasSecundarias.Where(l => (l.Detalle != null && l.Detalle.Length > 0) && l.CodCam == "32D"))
                        {
                            l.MensajeError = "Si el campo 32D está presente, el campo 57a no debe estar presente.";
                            l.TieneErrorDetalle = true;
                        }
                        foreach (var l in lst57.LineasSecundarias.Where(l => (l.Detalle != null && l.Detalle.Length > 0) && l.CodCam == "57A"))
                        {
                            l.MensajeError = "Si el campo 32D está presente, el campo 57a no debe estar presente.";
                            l.TieneErrorDetalle = true;
                        }
                        todoOK = false;
                    }

                    var lst71 = lineas.FirstOrDefault(x => x.CodCam == "71D"); // C3
                    var MT768_71D_Exist = lst71 != null ? lst71.LineasSecundarias.Exists(l => (l.Detalle != null && l.Detalle.Length > 0) && l.CodCam == "32") : false;
                    MT768_71D_Exist = (lst71.Detalle != null && lst71.Detalle.Length > 0);
                    var MT768_32A_Exist = lst32.LineasSecundarias.Exists(l => (l.Detalle != null && l.Detalle.Length > 0));

                    if (MT768_71D_Exist && !MT768_32A_Exist)
                    {
                        if (lst71.Detalle != null && lst71.Detalle.Length > 0)
                        {
                            lst71.MensajeError = "Si el campo 71D está presente, el campo 32a también debe estarlo.";
                            lst71.TieneErrorDetalle = true;
                        }

                        foreach (var l in lst71.LineasSecundarias.Where(l => (l.Detalle != null && l.Detalle.Length > 0) && l.CodCam == "71D"))
                        {
                            l.MensajeError = "Si el campo 71D está presente, el campo 32a también debe estarlo.";
                            l.TieneErrorDetalle = true;
                        }

                        todoOK = false;
                    }
                }
            }
            return todoOK;
        }

        /// <summary>
        /// Se genera validación correspondiente al MT769       
        /// </summary>
        private bool ValidarMT769(List<LineaEditorMensajeSwift> lineas)
        {
            var todoOK = true;

            // C1
            var exist_25 = lineas.Exists(l => l.CodCam == "25" && l.Detalle != null);
            var exist_57a = lineas.FirstOrDefault(l => l.CodCam == "57").LineasSecundarias.Exists(l => l.CodCam == "57A" && l.Detalle != null);
            var mensajeC1 = "Puede estar presente el campo 25 o 57a, pero no ambos.";

            if (exist_25 && exist_57a)
            {
                var res25 = lineas.FirstOrDefault(l => l.CodCam == "25" && l.Detalle != null);

                if (res25 != null)
                {
                    res25.MensajeError = mensajeC1;
                    res25.TieneErrorDetalle = true;
                }
              

                foreach (var res57 in lineas.FirstOrDefault(l => l.CodCam == "57").LineasSecundarias.Where(l => l.CodCam == "57A" && l.Detalle != null))
                {
                    res57.MensajeError = mensajeC1;
                    res57.TieneErrorDetalle = true;
                }
                todoOK = false;
            }

            // C2
            var exist_33B = lineas.Exists(l => l.CodCam == "33B" && l.Detalle != null);
            var exist_39C = lineas.Exists(l => l.CodCam == "39C" && l.Detalle != null);
            var mensajeC2 = "Puede estar el campo 33B o el campo 39C, pero no ambos.";

            if (exist_33B && exist_39C)
            {
                var res33B = lineas.FirstOrDefault(l => l.CodCam == "33B");
                res33B.MensajeError = mensajeC2;
                res33B.TieneErrorDetalle = true;

                var res39C = lineas.FirstOrDefault(l => l.CodCam == "39C");
                res39C.MensajeError = mensajeC2;
                res39C.TieneErrorDetalle = true;

                todoOK = false;
            }

            // C3
            var exist_32D = lineas.FirstOrDefault(l => l.CodCam == "32").LineasSecundarias.Exists(l => l.CodCam == "32D" && l.Detalle != null);
            var mensajeC3 = "Si el campo 32D está presente, el campo 57a no debe estarlo.";
            if (exist_32D && exist_57a)
            {
                foreach (var res32D in lineas.FirstOrDefault(l => l.CodCam == "32").LineasSecundarias.Where(l => l.CodCam == "32D" && l.Detalle != null))
                {
                    res32D.MensajeError = mensajeC3;
                    res32D.TieneErrorDetalle = true;
                }

                foreach (var res57 in lineas.FirstOrDefault(l => l.CodCam == "57").LineasSecundarias.Where(l => l.CodCam == "57A" && l.Detalle != null))
                {
                    res57.MensajeError = mensajeC3;
                    res57.TieneErrorDetalle = true;
                }
                todoOK = false;
            }


            // C4
            var exist_71D = lineas.Exists(l => l.CodCam == "71D" && l.Detalle != null);
            var exist_32A = lineas.FirstOrDefault(l => l.CodCam == "32").LineasSecundarias.Exists(l => l.Detalle != null);
            var mensajeC4 = "Si el campo 71D está presente, el campo 32A también debe estarlo.";
            if (exist_71D && !exist_32A)
            {
                var res71 = lineas.FirstOrDefault(l => l.CodCam == "71D" && l.Detalle != null);

                if (res71 != null)
                {
                    res71.MensajeError = mensajeC4;
                    res71.TieneErrorDetalle = true;

                    todoOK = false;
                }
               
            }

            // C5           
            var exist_34B = lineas.Exists(l => l.CodCam == "34B" && l.Detalle != null);
            var mensajeC5 = "El código de moneda del campo 33B y 34B, deben ser iguales";
            if (exist_33B && exist_34B)
            {
                var tipoMoneda_33B = lineas.FirstOrDefault(l => l.CodCam == "33B").Detalle.Substring(0, 3);
                var tipoMoneda_34B = lineas.FirstOrDefault(l => l.CodCam == "34B").Detalle.Substring(0, 3);

                if (tipoMoneda_33B.ToLower() != tipoMoneda_34B.ToLower())
                {
                    var res33B = lineas.FirstOrDefault(l => l.CodCam == "33B");
                    res33B.MensajeError = mensajeC5;
                    res33B.TieneErrorDetalle = true;

                    var res34B = lineas.FirstOrDefault(l => l.CodCam == "34B");
                    res34B.MensajeError = mensajeC5;
                    res34B.TieneErrorDetalle = true;

                    todoOK = false;
                }
            }
            return todoOK;
        }

        /// <summary>
        /// Se genera validación correspondiente al MT775
        /// </summary>
        private bool ValidarMT775(List<LineaEditorMensajeSwift> lineas)
        {
            var todoOk = true;

            var exist_77U = lineas.Exists(l => l.Detalle != null && l.CodCam == "77U");
            var exist_77L = lineas.Exists(l => l.Detalle != null && l.CodCam == "77L");
            var mensajeC1 = "Debe estar presente el campo 77U o el campo 77L, ambos pueden estar presentes.";

            if (!exist_77L && !exist_77U)
            {
                var res77U = lineas.FirstOrDefault(l => l.CodCam == "77U");
                res77U.MensajeError = mensajeC1;
                res77U.TieneErrorDetalle = true;
                var res77L = lineas.FirstOrDefault(l => l.CodCam == "77L");
                res77L.MensajeError = mensajeC1;
                res77L.TieneErrorDetalle = true;
                todoOk = false;
            }


            return todoOk;
        }

        /// <summary>
        /// Se genera validación correspondiente al MT785
        /// </summary>
        private bool ValidarMT785(List<LineaEditorMensajeSwift> lineas)
        {
            var todoOk = true;

            var exist57A = lineas.FirstOrDefault(l => l.CodCam == "57").LineasSecundarias.Exists(l => l.Detalle != null && l.CodCam == "57A");
            var exist56A = lineas.FirstOrDefault(l => l.CodCam == "56").LineasSecundarias.Exists(l => l.Detalle != null && l.CodCam == "56A");

            if (exist57A && !exist56A)
            {
                foreach (var item in lineas.FirstOrDefault(l => l.CodCam == "57").LineasSecundarias.Where(l => l.Detalle != null && l.CodCam == "57A"))
                {
                    item.MensajeError = "Si el campo 57A está presente, entonces el campo 56A también debe estarlo.";
                    item.TieneErrorDetalle = true;
                }

                todoOk = false;
            }
            return todoOk;
        }

        /// <summary>
        /// Se genera validación correspondiente al MT767
        /// </summary>
        private bool ValidarMT761(List<LineaEditorMensajeSwift> lineas)
        {
            var todoOk = true;

            var exist77U = lineas.Exists(l => l.Detalle != null && l.CodCam == "77U");
            var exist77L = lineas.Exists(l => l.Detalle != null && l.CodCam == "77L");

            if (!exist77U && !exist77L)
            {
                lineas.FirstOrDefault(l => l.CodCam == "77U").MensajeError = "Debe estar presente el campo 77U o el campo 77L.";
                lineas.FirstOrDefault(l => l.CodCam == "77U").TieneErrorDetalle = true;

                lineas.FirstOrDefault(l => l.CodCam == "77L").MensajeError = "Debe estar presente el campo 77U o el campo 77L.";
                lineas.FirstOrDefault(l => l.CodCam == "77L").TieneErrorDetalle = true;

                todoOk = false;
            }


            return todoOk;
        }

        /// <summary>
        /// Se genera validación correspondiente al MT765
        /// </summary>
        private bool ValidarMT765(List<LineaEditorMensajeSwift> lineas)
        {
            var todoOk = true;

            // C1 
            var exist49A = lineas.Exists(l => l.CodCam == "49A" && l.Detalle == "INCP");
            var exist77 = lineas.Exists(l => l.CodCam == "77" && l.Detalle != null);
            var mensajeC1 = "Si el campo 49A es INCP, entonces el campo 77 debe estar presente, de lo contrario el campo 77 no debe estarlo.";

            if (exist49A && !exist77)
            {
                var resp49A = lineas.FirstOrDefault(l => l.CodCam == "49A" && l.Detalle == "INCP");
                resp49A.TieneErrorDetalle = true;
                resp49A.MensajeError = mensajeC1;

                todoOk = false;

            }
            else if (!exist49A && exist77)
            {
                var resp77 = lineas.FirstOrDefault(l => l.CodCam == "77" && l.Detalle == null);
                if (resp77 != null)
                {
                    resp77.TieneErrorDetalle = true;
                    resp77.MensajeError = mensajeC1;

                    todoOk = false;
                }

            }

            //C2
            var exist22G = lineas.Exists(l => l.CodCam == "22G" && l.Detalle == "PAYM");
            var mensajeC2 = "Si el campo 22G es PAYM, entonces el campo 31E no está permitido.";

            if (exist22G)
            {
                var exist31E = lineas.Exists(l => l.CodCam == "31E" && l.Detalle != null);

                if (exist31E)
                {
                    var resp31E = lineas.FirstOrDefault(l => l.CodCam == "31E" && l.Detalle != null);

                    if (resp31E != null)
                    {
                        resp31E.MensajeError = mensajeC2;
                        resp31E.TieneErrorDetalle = true;

                        todoOk = false;
                    }
                 
                }
            }

            return todoOk;
        }

        /// <summary>
        /// Se genera validación correspondiente al MT767
        /// </summary>
        private bool ValidarMT767(List<LineaEditorMensajeSwift> lineas)
        {

            var todoOk = true;

            string[] secuencias = { "A", "B", "C" };

            foreach (var s in secuencias)
            {
                var mensajeC1 = string.Format("En la secuencia {0}, el campo 32B o el campo 33B, pero no ambos, pueden estar presentes.", s);
                var mensajeC2 = string.Format("El la secuencia {0}, si el campo 23B es COND, el campo 35G debe estar presente en la secuencia {0}; de lo contrario, el campo 35G no está permitido", s);

                switch (s)
                {
                    case "A":
                        // C3 & C4
                        var res22A = lineas.FirstOrDefault(l => l.CodCam == "22A" && (l.Detalle == "ISCA" || l.Detalle == "ICCA") && l.Secuencia == s);
                        if (res22A != null)
                        {
                            if (lineas.Exists(l => l.Secuencia == "C" && l.Detalle != null))
                            {
                                var res15C = lineas.FirstOrDefault(l => l.Secuencia == "C" && l.CodCam == "15C");
                                if (res15C.Detalle != null)
                                {
                                    if (!lineas.Exists(l => l.Secuencia == "C" && l.Detalle != null && l.CodCam != "15C"))
                                    {
                                        foreach (var item in lineas.Where(l => l.Secuencia == "C" && l.CodCam != "15C"))
                                        {
                                            item.MensajeError = "En la secuencia C, si el campo 15C está presente, entonces al menos uno de los otros campos de la secuencia C debe estar presente.";
                                            item.TieneErrorDetalle = true;
                                        }
                                        todoOk = false;
                                    }
                                }
                                else
                                {
                                    res15C.MensajeError = "En la secuencia A, si el campo 22A es ISCA o CCA, el campo 15C de la secuencia C es obligatorio.";
                                    res15C.TieneErrorDetalle = true;
                                    todoOk = false;

                                }
                            }
                            else
                            {
                                res22A.MensajeError = "En la secuencia A, si el campo 22A es ISCA o ICCA, la secuencia C debe estar presente; de ​​lo contrario, la secuencia C no se permite.";
                                res22A.TieneErrorDetalle = true;
                                todoOk = false;
                            }
                        }
                        break;
                    default: //Solo si es B o C

                        // C1
                        var res32B = lineas.FirstOrDefault(l => l.CodCam == "32B" && l.Detalle != null && l.Secuencia == s);
                        var res33B = lineas.FirstOrDefault(l => l.CodCam == "33B" && l.Detalle != null && l.Secuencia == s);

                        if (res32B != null && res33B != null)
                        {
                            if (res32B != null)
                            {
                                res32B.MensajeError = mensajeC1;
                                res32B.TieneErrorDetalle = true;
                            }

                            if (res33B != null)
                            {
                                res33B.MensajeError = mensajeC1;
                                res33B.TieneErrorDetalle = true;
                            }
                            todoOk = false;
                        }

                        // C2
                        var res35G = lineas.FirstOrDefault(l => l.CodCam == "35G" && l.Secuencia == s);

                        if (lineas.Exists(l => l.CodCam == "23B" && l.Detalle == "COND" && l.Secuencia == s))
                        {
                            if (res35G.Detalle == null)
                            {
                                res35G.MensajeError = mensajeC2;
                                res35G.TieneErrorDetalle = true;

                                todoOk = false;
                            }
                        }
                        else
                        {
                            if (res35G != null && res35G.Detalle != null)
                            {
                                res35G.MensajeError = mensajeC2;
                                res35G.TieneErrorDetalle = true;

                                todoOk = false;
                            }
                        }

                        break;
                }
            }

            return todoOk;
        }

        /// <summary>
        /// Se limpian errores y mensajes de error correspondientes a las lineas primarias y secundarias del mensaje
        /// </summary>
        /// <param name="lineas">Lista de lineas del mensaje</param>
        private void LimpiarErroresEnLineas(IList<LineaEditorMensajeSwift> lineas)
        {
            try
            {
                // Se remueven mensajes de error en las lineas primarias
                foreach (var linea in lineas)
                {
                    linea.TieneErrorDetalle = false;
                    linea.MensajeError = string.Empty;

                    // Si tiene lineas secundarias se les remueven mensajes de error
                    foreach (var lineaSecundarias in linea.LineasSecundarias)
                    {
                        lineaSecundarias.TieneErrorDetalle = false;
                        lineaSecundarias.MensajeError = string.Empty;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("LimpiarErroresEnLineas"));
            }
       
        }

        /// <summary>
        /// Se validan las lineas del mensaje para verificar que los caracteres sean validos segun Normativa SWIFT
        /// </summary>
        /// <param name="lineas">Lista de lineas del mensaje</param>
        /// <returns>Verdadero si los caracteres son validos</returns>
        private bool ValidarCaracteresLineas(IList<LineaEditorMensajeSwift> lineas)
        {
            try
            {
                using (var tracer = new Tracer("ValidarCaracteresLineas - EnvioSwiftService"))
                {
                    bool result = true;
                    try
                    {
                        IList<sw_caracter_error> caracteresError_X = GetCaracteresError();
                        IList<sw_caracter_error> caracteresError_Z = GetCaracteresError_Z();

                        foreach (var linea in lineas)
                        {
                            string formatoLinea = linea.Formato;

                            var caracteresError = formatoLinea.Contains("Z") ? caracteresError_Z : caracteresError_X;

                            #region Validacion lineas primarias
                            var caracterInvalido = string.Empty;

                            // validamos que la linea contenga detalle
                            if (!string.IsNullOrEmpty(linea.Detalle))
                            {
                                // se obtienen los valores de la lista extendida de ASCII
                                var encoding = Encoding.GetEncoding("iso-8859-1");
                                byte[] asciiBytes = encoding.GetBytes(linea.Detalle);
                                List<string> invalidosPrimarias = new List<string>();

                                for (var i = 0; i < linea.Detalle.Length; i++)
                                {
                                    caracterInvalido = ObtenerCaracterInvalido((int)asciiBytes[i], caracteresError);

                                    // si tiene caracter invalido se añade a la lista
                                    if (caracterInvalido != string.Empty)
                                    {
                                        invalidosPrimarias.Add(caracterInvalido);
                                    }
                                }

                                // si nuestra lista de caracteres invalidos contiene elementos se mostraran en el mensaje de error de la linea
                                if (invalidosPrimarias.Count > 0)
                                {
                                    string mensaje = "El texto no debe contener el/los caracteres ";

                                    foreach (var caracter in invalidosPrimarias)
                                    {
                                        mensaje += caracter;
                                    }

                                    linea.TieneErrorDetalle = true;
                                    linea.MensajeError = mensaje;

                                    result = false;
                                }
                            }
                            #endregion

                            #region Validacion lineas secundarias
                            var caracterInvalidoSecundaria = string.Empty;
                            List<string> invalidosSecundarias = new List<string>();

                            for (int i = 0; i < linea.LineasSecundarias.Count; i++)
                            {
                                // ya que revisamos todas las lineas secundarias validamos las que tengan detalle
                                if (!string.IsNullOrEmpty(linea.LineasSecundarias[i].Detalle))
                                {
                                    string detalleLineaSecundaria = linea.LineasSecundarias[i].Detalle;

                                    // se obtienen los valores de la lista extendida de ASCII
                                    var encoding = Encoding.GetEncoding("iso-8859-1");
                                    byte[] asciiBytes = encoding.GetBytes(detalleLineaSecundaria);

                                    for (int j = 0; j < detalleLineaSecundaria.Count(); j++)
                                    {
                                        caracterInvalidoSecundaria = ObtenerCaracterInvalido((int)asciiBytes[j], caracteresError);

                                        // si tiene caracter invalido se añade a la lista
                                        if (caracterInvalidoSecundaria != string.Empty)
                                        {
                                            invalidosSecundarias.Add(caracterInvalidoSecundaria);
                                        }
                                    }
                                }

                                // si nuestra lista de caracteres invalidos contiene elementos se mostraran en el mensaje de error de la linea
                                if (invalidosSecundarias.Count > 0)
                                {
                                    string mensaje = "El texto no debe contener el/los caracteres ";

                                    foreach (var caracter in invalidosSecundarias)
                                    {
                                        mensaje += caracter;
                                    }

                                    linea.LineasSecundarias[i].TieneErrorDetalle = true;
                                    linea.LineasSecundarias[i].MensajeError = mensaje;

                                    caracterInvalidoSecundaria = string.Empty;
                                    invalidosSecundarias = new List<string>();
                                    result = false;
                                }
                            }
                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        tracer.TraceException("Error al validar caracteres de la linea del mensaje ", ex);
                        throw new Exception("Se ha producido un error al tratar de validar los caracteres del mensaje", ex);
                    }

                    return result;
                }
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("ValidarCaracteresLineas"));
            }
        
        }

        /// <summary>
        /// Validamos el valor ASCII del caracter contra la lista de caracteres invalidos Z
        /// </summary>
        /// <param name="asciiCode">Valor ASCII del caracter</param>
        /// <param name="caracteresError">Lista de caracteres invalidos</param>
        /// <returns>Caracter invalido o vacio de no ser invalido</returns>
        private string ObtenerCaracterInvalido(int asciiCode, IList<sw_caracter_error> caracteresError)
        {
            try
            {
                using (var tracer = new Tracer("ObtenerCaracterInvalido - EnvioSwiftService"))
                {
                    sw_caracter_error caracter = null;
                    try
                    {
                        for (int i = 0; i < caracteresError.Count; i++)
                        {
                            caracter = caracteresError[i];

                            if (caracter.valor_ascii == asciiCode)
                            {
                                return caracter.caracter;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        tracer.TraceException("Error al obtener el caracter invalido, valor ascii: " + asciiCode.ToString() ?? "<NULL> ", ex);
                        throw new Exception("Hubo un problema al intentar validar el caracter, valor ASCII: " + asciiCode.ToString() ?? "<NULL>", ex);
                    }

                    return string.Empty;
                }
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("ObtenerCaracterInvalido"));
            }
            
        }

        public string GenerarMensajeSwift(IList<LineaEditorMensajeSwift> lineas, string codMT, string swiftBanco)
        {
            try
            {
                swiftBanco = Utilidad.SanitizarString(swiftBanco);

                string prioridadNormal = "N";
                StringBuilder mensaje = new StringBuilder(Convert.ToChar(1) + "{1:F01BCHICLRMAXXX          }{2:I" + codMT.Substring(2, 3) /*Ej, del "MT103" me quedo con 103 */);
                string banco = String.Empty;
                if (!String.IsNullOrEmpty(swiftBanco))
                {
                    banco = swiftBanco.Substring(0, 8) + "A" + swiftBanco.Substring(8, 3); //ej: CITIUS33XXX lo transformo en CITIUS33AXXX
                }
                else
                {
                    banco = String.Empty.PadLeft(12); //12 espacios
                }

                mensaje.AppendLine(banco.ToUpper() + prioridadNormal + "}{4:");


                List<string> lineasCuerpo = GetCuerpoMensajeSwiftFormatoParaGuardar(lineas);
                foreach (string linea in lineasCuerpo)
                {
                    mensaje.AppendLine(linea);
                }

                mensaje.Append("-}" + Convert.ToChar(3));
                return mensaje.ToString();
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("GenerarMensajeSwift"));
            }          
        }

        public List<string> GetCuerpoMensajeSwiftFormatoParaGuardar(IList<LineaEditorMensajeSwift> lineas)
        {
            try
            {
                List<string> lineasCuerpo = new List<string>();

                List<LineaEditorMensajeSwift> lineasIncluidas = lineas.Where(l => l.Incluido).ToList();
                foreach (LineaEditorMensajeSwift linea in lineasIncluidas)
                {
                    int indiceLineasSecundariasComenzar = 0;
                    List<LineaSecundariaEditorMensajeSwift> lineasSecundariasCorresponden = null;
                    LineaSecundariaEditorMensajeSwift primerLsTieneDetalle = null;

                    string textoLinea = ":" + (linea.TieneVariantes ? linea.VarianteSeleccionada : linea.CodCam) + ":";

                    if (!linea.TieneVariantes)
                    {
                        //se revisa si el detalle es null or empty
                        if (string.IsNullOrEmpty(linea.Detalle))
                        {
                            //si tiene lineas secundarias
                            if (linea.LineasSecundarias != null && linea.LineasSecundarias.Count() > 0)
                            {
                                //se agrega el detalle del primer elemento
                                var detalleLinea1 = linea.LineasSecundarias.First();
                                textoLinea += detalleLinea1.Detalle;
                                //se dejan las demas lineas para escribir lo que falta, santandose la primera
                                lineasSecundariasCorresponden = linea.LineasSecundarias.Skip(1).ToList();
                            }
                        }
                        else
                        {
                            textoLinea += linea.Detalle;
                            lineasSecundariasCorresponden = linea.LineasSecundarias;
                        }
                    }
                    else
                    {
                        lineasSecundariasCorresponden = linea.LineasSecundarias.Where(s => s.CodCam == linea.VarianteSeleccionada).ToList();

                        primerLsTieneDetalle = lineasSecundariasCorresponden.Where(ls => ls.Detalle != null && !String.IsNullOrEmpty(ls.Detalle.Trim())).FirstOrDefault();
                        if (primerLsTieneDetalle != null)
                        {
                            textoLinea += primerLsTieneDetalle.Detalle;
                            indiceLineasSecundariasComenzar = primerLsTieneDetalle.Linea;
                        }
                    }

                    lineasCuerpo.Add(textoLinea);

                    if (lineasSecundariasCorresponden != null && lineasSecundariasCorresponden.Count > 0)
                    {
                        for (int i = indiceLineasSecundariasComenzar; i < lineasSecundariasCorresponden.Count; i++)
                        {
                            LineaSecundariaEditorMensajeSwift lineaS = lineasSecundariasCorresponden[i];
                            if (!String.IsNullOrEmpty(lineaS.Detalle)) lineaS.Detalle = lineaS.Detalle.Trim();

                            if (!String.IsNullOrEmpty(lineaS.Detalle))
                            {
                                lineasCuerpo.Add(lineaS.Detalle);
                            }
                        }
                    }
                }

                return lineasCuerpo;
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("GetCuerpoMensajeSwiftFormatoParaGuardar"));
            }
         
        }

        //casi misma logica que metodo anterior pero devuelve la lista de LineaDetalleMensajeSwift que es mas practica para mostrar
        public List<LineaDetalleMensajeSwift> GetCuerpoMensajeSwiftFormatoParaVisualizar(IList<LineaEditorMensajeSwift> lineas)
        {
            try
            {
                List<LineaDetalleMensajeSwift> lineasCuerpo = new List<LineaDetalleMensajeSwift>();

                List<LineaEditorMensajeSwift> lineasIncluidas = lineas.Where(l => l.Incluido).ToList();
                foreach (LineaEditorMensajeSwift linea in lineasIncluidas)
                {
                    int indiceLineasSecundariasComenzar = 0;
                    List<LineaSecundariaEditorMensajeSwift> lineasSecundariasCorresponden = null;
                    LineaDetalleMensajeSwift lineaDetalle = new LineaDetalleMensajeSwift() { EsNuevaLineaDeCampo = true };

                    lineaDetalle.Codigo = (linea.TieneVariantes ? linea.VarianteSeleccionada : linea.CodCam);

                    if (!linea.TieneVariantes)
                    {
                        lineaDetalle.Detalle = linea.Detalle;
                        lineaDetalle.Descripcion = linea.Descripcion;
                        lineasSecundariasCorresponden = linea.LineasSecundarias;
                        lineasCuerpo.Add(lineaDetalle);
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(linea.VarianteSeleccionada))
                        {
                            lineasSecundariasCorresponden = linea.LineasSecundarias.Where(s => s.CodCam == linea.VarianteSeleccionada).ToList();

                            LineaSecundariaEditorMensajeSwift primerLsTieneDetalle = lineasSecundariasCorresponden.Where(ls => ls.Detalle != null && !String.IsNullOrEmpty(ls.Detalle.Trim())).FirstOrDefault();
                            if (primerLsTieneDetalle != null)
                            {
                                lineaDetalle.Detalle = primerLsTieneDetalle.Detalle;
                                lineaDetalle.Descripcion = primerLsTieneDetalle.Descripcion;
                                indiceLineasSecundariasComenzar = primerLsTieneDetalle.Linea;
                                lineasCuerpo.Add(lineaDetalle);

                                AgregarLineasDetalleDeBancoSegunSwift(lineasCuerpo, primerLsTieneDetalle);
                            }
                        }
                    }

                    if (lineasSecundariasCorresponden != null && lineasSecundariasCorresponden.Count > 0)
                    {
                        for (int i = indiceLineasSecundariasComenzar; i < lineasSecundariasCorresponden.Count; i++)
                        {
                            LineaSecundariaEditorMensajeSwift lineaS = lineasSecundariasCorresponden[i];
                            if (!String.IsNullOrEmpty(lineaS.Detalle)) lineaS.Detalle = lineaS.Detalle.Trim();

                            if (!String.IsNullOrEmpty(lineaS.Detalle))
                            {
                                lineasCuerpo.Add(new LineaDetalleMensajeSwift()
                                {
                                    EsNuevaLineaDeCampo = false,
                                    Detalle = lineaS.Detalle
                                });

                                AgregarLineasDetalleDeBancoSegunSwift(lineasCuerpo, lineaS);
                            }
                        }
                    }
                }

                return lineasCuerpo;
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("GetCuerpoMensajeSwiftFormatoParaVisualizar"));
            }
           
        }

        private void AgregarLineasDetalleDeBancoSegunSwift(List<LineaDetalleMensajeSwift> lineasCuerpo, LineaSecundariaEditorMensajeSwift lineaS)
        {
            try
            {
                if ((lineaS.Formato.StartsWith("11T") && lineaS.Detalle != null && lineaS.Detalle.Length == 11) || (lineaS.CodCam == "57A" && lineaS.Linea == 1 && lineaS.Detalle.Length == 11 && uow.BancoRepository.GetBancosByCodAndBranch(lineaS.Detalle.Substring(0, 8), lineaS.Detalle.Substring(8, 3)) != null))
                {
                    //este es el formato de los swift, si escribio un codigo de swift entonces al mensaje swift le tengo que agregar las lineas relativas al nombre del banco y la dir.
                    //tener en cuenta que esta info adicional solo se agrega para visualizar, no afectan las lineas del swift guardado
                    sw_bancos banco = ObtenerBancoPorSwift(lineaS.Detalle);
                    if (banco != null)
                    {
                        lineasCuerpo.Add(new LineaDetalleMensajeSwift()
                        {
                            EsNuevaLineaDeCampo = false,
                            Detalle = banco.nombre_banco.Trim()
                        });
                        lineasCuerpo.Add(new LineaDetalleMensajeSwift()
                        {
                            EsNuevaLineaDeCampo = false,
                            Detalle = banco.ciudad_banco.Trim()
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("AgregarLineasDetalleDeBancoSegunSwift"));
            }            
        }

        public ResultadoBusquedaSwift GetSwiftConLineasParaEditor(int idMensaje)
        {
            try
            {
                ResultadoBusquedaSwift mensaje = uow.MensajeRepository.GetSwiftEnviado(idMensaje);
                if (mensaje != null)
                {
                    string codMT, swiftBanco; //no los uso pq esta info tambien la tengo en la base, es redundante
                    mensaje.LineasEditor = ObtenerMensajeSwiftParaEditar(idMensaje, out codMT, out swiftBanco);

                    return mensaje;
                }
                else throw new ArgumentException("No se encontró en la base de datos el swift de idMensaje " + idMensaje);
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("GetSwiftConLineasParaEditor"));
            }        
        }

        public IList<LineaEditorMensajeSwift> ObtenerMensajeSwiftParaEditar(int idMensaje, out string codMT, out string swiftBanco)
        {
            try
            {
                MensajeSwiftService service = new MensajeSwiftService();
                string mensajeRaw = service.DesencriptaMensajeEnviado(idMensaje);
                if (!String.IsNullOrEmpty(mensajeRaw))
                {
                    return DescomponerMensajeSwift(mensajeRaw, out codMT, out swiftBanco);
                }
                else
                {
                    throw new ArgumentException("No se pudo obtener el swift con el idMensaje enviado");
                }
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("ObtenerMensajeSwiftParaEditar"));
            }
          
        }

        public IList<LineaEditorMensajeSwift> DescomponerMensajeSwift(string mensaje, out string codMT, out string swiftBanco)
        {
            try
            {
                using (var tracer = new Tracer("DescomponerMensajeSwift"))
                {
                    try
                    {
                        string textoInicio = "{1:F01BCHICLRMAXXX          }{2:I";
                        int indiceComienzoCodMT = mensaje.IndexOf(textoInicio) + textoInicio.Length;
                        int nroMT = 0;
                        if (int.TryParse(mensaje.Substring(indiceComienzoCodMT, 3), out nroMT))
                        {
                            codMT = "MT" + nroMT.ToString();

                            swiftBanco = mensaje.Substring(indiceComienzoCodMT + 3, 12); //el swift banco son 11 caracteres pero tiene una "A" entre medio
                            swiftBanco = swiftBanco.Substring(0, 8) + swiftBanco.Substring(9, 3); //saco la A

                            List<LineaEditorMensajeSwift> lineasEditor = this.GetLineasYFormatosParaMT(codMT);
                            if (lineasEditor != null && lineasEditor.Count > 0)
                            {
                                string[] lineasMensaje = mensaje.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                                char caracterLineaCampoNuevo = ':';

                                LineaEditorMensajeSwift ultimaLineaEditorLeida = null;
                                int ordenUltimoCampoLeido = 0;
                                int indiceUltimaLineaLeida = 0;

                                lineasEditor.Where(l => l.Obligatorio).ToList().ForEach(x => x.Incluido = true); //todas las lineas obligatorias tienen que estar incluidas

                                for (int i = 1; i < lineasMensaje.Length - 1; i++) //ignoro la primer y última línea que no me interesan
                                {

                                    string linea = lineasMensaje[i].Trim();
                                    char primerCaracterLinea = linea[0];

                                    //es un campo nuevo o es la continuación de un campo anterior? hay campos que ocupan varias líneas
                                    if (primerCaracterLinea == caracterLineaCampoNuevo)
                                    {
                                        string[] partesNuevaLinea = linea.Split(new char[] { caracterLineaCampoNuevo }, StringSplitOptions.RemoveEmptyEntries);

                                        // Validacion agregada, en algunos casos no se escribe correctamente el mensaje en BD por lo tanto al tratar de cargar trae una linea sin el codigo del campo y no carga la informacion
                                        if (partesNuevaLinea.Length > 0)
                                        {
                                            string codCam = partesNuevaLinea[0];

                                            string detalle = String.Empty;

                                            if (partesNuevaLinea.Length == 2)
                                            {
                                                detalle = partesNuevaLinea[1];
                                            }
                                            else
                                            {
                                                detalle = string.Join(caracterLineaCampoNuevo.ToString(), partesNuevaLinea.Skip(1).ToArray()); //me salteo el primer elemento que es el codigo, y vuelvo a unir todo el resto
                                            }

                                            /// Para obtener la linea correcta y la linea no posee variante, se debe buscar mediante su codCam completo (ej "44C") 
                                            ultimaLineaEditorLeida = lineasEditor.Where(l => l.CodCam.Equals(codCam) && l.Orden > ordenUltimoCampoLeido).FirstOrDefault();

                                            /// Si no se obtiene la linea correcta, es posible que sea variante de otra linea principal (ej "41A" es variante de "41").
                                            /// En este caso, se debe buscar la linea principal que contenga la variante (ej buscar linea "41" para encontrar la variante "41A"). 
                                            /// lineasEditor contiene lineas principales. Las variantes se encuentran contenidas en su linea principal.
                                            if (ultimaLineaEditorLeida == null)
                                                ultimaLineaEditorLeida = lineasEditor.Where(l => l.CodCam.StartsWith(codCam.Substring(0, 2)) && l.Orden > ordenUltimoCampoLeido).FirstOrDefault();

                                            if (ultimaLineaEditorLeida != null)
                                            {
                                                ultimaLineaEditorLeida.Incluido = true;
                                                if (ultimaLineaEditorLeida.TieneVariantes)
                                                {
                                                    ultimaLineaEditorLeida.VarianteSeleccionada = codCam;
                                                    LineaSecundariaEditorMensajeSwift lSecundaria = null;
                                                    var primera = true;
                                                    //se hace esto para decidir si el dato va o no en la 1ra linea
                                                    foreach (var lS in ultimaLineaEditorLeida.LineasSecundarias.Where(s => s.CodCam == codCam))
                                                    {
                                                        //se revisa si el formato de la linea empieza por / y si el detalle tabien. O si el formato no empieza por / y es la primera linea
                                                        if ((lS.Formato.Substring(0, 1) == "/" && lS.Formato.Substring(0, 1) == detalle.Substring(0, 1)) || (primera && lS.Formato.Substring(0, 1) != "/"))
                                                        {
                                                            lSecundaria = lS;
                                                            break;
                                                        }
                                                        else if (!primera)
                                                        {
                                                            lSecundaria = lS;
                                                            break;
                                                        }
                                                        primera = false;
                                                    };
                                                    lSecundaria.Detalle = detalle;
                                                    indiceUltimaLineaLeida = lSecundaria.Linea;
                                                }
                                                else
                                                {
                                                    //casos especiales que parten con formato /34X que es el numero de cuenta Ejm campo 59 
                                                    //si el detalle no es null o blanco && no empieza por / && el campo tienes lineas secundarias se prepara para no dejarla en la linea con formato /34X
                                                    if (ultimaLineaEditorLeida.Formato.StartsWith("/") && !string.IsNullOrEmpty(detalle) && !detalle.StartsWith("/") && ultimaLineaEditorLeida.LineasSecundarias.Count > 0)
                                                    {
                                                        LineaSecundariaEditorMensajeSwift lSecundaria = null;
                                                        var primera = true;
                                                        //se hace esto para decidir si el dato va o no en la 1ra linea
                                                        foreach (var lS in ultimaLineaEditorLeida.LineasSecundarias.Where(s => s.CodCam == codCam))
                                                        {
                                                            //se revisa si el formato de la linea empieza por / y si el detalle tabien. O si el formato no empieza por / y es la primera linea
                                                            if ((lS.Formato.Substring(0, 1) == "/" && lS.Formato.Substring(0, 1) == detalle.Substring(0, 1)) || (primera && lS.Formato.Substring(0, 1) != "/"))
                                                            {
                                                                lSecundaria = lS;
                                                                break;
                                                            }
                                                            else if (!primera)
                                                            {
                                                                lSecundaria = lS;
                                                                break;
                                                            }
                                                            primera = false;
                                                        };
                                                        lSecundaria.Detalle = detalle;
                                                        indiceUltimaLineaLeida = lSecundaria.Linea;
                                                    }
                                                    else
                                                    {
                                                        ultimaLineaEditorLeida.Detalle = detalle;
                                                        indiceUltimaLineaLeida = ultimaLineaEditorLeida.Linea;
                                                    }
                                                }
                                                ordenUltimoCampoLeido = ultimaLineaEditorLeida.Orden;
                                            }
                                            else
                                            {
                                                //la linea leida no esta como campo valido en la BD, tiro una excepcion? ignoro la linea?
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (ultimaLineaEditorLeida != null && ultimaLineaEditorLeida.LineasSecundarias.Count > 0)
                                        {
                                            //continuacion de campo anterior
                                            LineaSecundariaEditorMensajeSwift lSecundaria = null;
                                            if (ultimaLineaEditorLeida.TieneVariantes)
                                            {
                                                lSecundaria = ultimaLineaEditorLeida.LineasSecundarias.Where(s => s.Linea > indiceUltimaLineaLeida && s.CodCam == ultimaLineaEditorLeida.VarianteSeleccionada).FirstOrDefault();
                                            }
                                            else
                                            {
                                                lSecundaria = ultimaLineaEditorLeida.LineasSecundarias.Where(s => s.Linea > indiceUltimaLineaLeida).FirstOrDefault();
                                            }

                                            if (lSecundaria != null)
                                            {
                                                lSecundaria.Detalle = linea;
                                                indiceUltimaLineaLeida = lSecundaria.Linea;
                                            }
                                            else
                                            {
                                                //esto no tiene sentido, lanzo error de formato?
                                            }
                                        }
                                        else
                                        {
                                            //esto no tiene sentido, lanzo error de formato?
                                        }
                                    }
                                }

                                return lineasEditor;
                            }
                            else
                            {
                                throw new FormatException("No se pudo encontrar en la BD el formato del tipo de mensaje " + codMT);
                            }
                        }
                        else
                        {
                            throw new FormatException("El CodMT del mensaje swift no se pudo leer");
                        }
                    }
                    catch (FormatException _fe)
                    {
                        tracer.TraceException("Alerta, no es posible leer MT", _fe);
                        throw;
                    }
                    catch (Exception _e)
                    {
                        tracer.TraceException("Alerta, no es posible leer MT", _e);
                        throw new FormatException("No es posible leer el MT", _e);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("DescomponerMensajeSwift"));
            }
            
        }

        public bool GuardarMensaje(int idMensaje, string rutUsuario, int centroCosto, string moneda, double monto, string textoMensajeSwift, out int? idMensajeNuevo)
        {
            try
            {
                idMensajeNuevo = null;
                using (Tracer tracer = new Tracer("Inicio grabar mensaje"))
                {
                    try
                    {
                        SWI200.Swi200Service service = new SWI200.Swi200Service();

                        int rutEnFormatoSwift = int.Parse(rutUsuario);

                        int vista = 0;
                        if (idMensaje <= 0)
                        {
                            vista = 1234; //vista para nuevo mensaje
                            SWI300.Swi300Service serviceCorrelativo = new SWI300.Swi300Service();
                            idMensajeNuevo = serviceCorrelativo.GetCorrelativo();
                            idMensaje = idMensajeNuevo.Value;
                        }

                        return service.IngresaModificaMensajeSwift(vista, idMensaje, rutEnFormatoSwift, centroCosto, moneda, monto, 'M', textoMensajeSwift);
                    }
                    catch (Exception ex)
                    {
                        tracer.TraceException("Alerta, problemas al grabar el mensaje swift", ex);
                        throw;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("GuardarMensaje"));
            }
           
        }

        private bool ValidarLineaMensaje(ILineaMensajeSwift linea, List<sw_valor_campos> valoresCampos)
        {
            try
            {
                sw_valor_campos valorCampo = null;
                LineaEditorMensajeSwift lineaPrincipal = linea as LineaEditorMensajeSwift;

                if (lineaPrincipal == null || !lineaPrincipal.TieneVariantes)
                {
                    valorCampo = valoresCampos.Where(v => v.tag_campo == linea.CodCam && v.linea_campo == linea.Linea).FirstOrDefault();

                    if (valorCampo != null)
                    {
                        List<string> valoresAceptables = valorCampo.valor_campo.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                        bool satisfaceCondicion = false;
                        string msg = String.Empty;

                        if (valorCampo.cond_valor == "=")
                        {
                            msg = "El texto debe ser igual a alguna de las siguientes opciones:<br>";
                            foreach (string valorAceptable in valoresAceptables)
                            {
                                if (linea.Detalle == valorAceptable)
                                {
                                    satisfaceCondicion = true;
                                    break;
                                }
                            }

                        }
                        else if (valorCampo.cond_valor == "<>")
                        {
                            satisfaceCondicion = true;
                            msg = "El texto debe ser distinto a las siguientes opciones:<br>";
                            foreach (string valorInaceptable in valoresAceptables)
                            {
                                if (linea.Detalle == valorInaceptable)
                                {
                                    satisfaceCondicion = false;
                                    break;
                                }
                            }
                        }
                        else if (valorCampo.cond_valor == "like")
                        {
                            msg = "El texto debe comenzar con alguna de las siguientes opciones:<br>";
                            foreach (string valorAceptable in valoresAceptables)
                            {
                                if (String.IsNullOrEmpty(linea.Detalle) || linea.Detalle.StartsWith(valorAceptable))
                                {
                                    satisfaceCondicion = true;
                                    break;
                                }
                            }
                        }
                        else if (valorCampo.cond_valor == "not like")
                        {
                            satisfaceCondicion = true;
                            msg = "El texto no debe comenzar con las siguientes opciones:<br>";
                            foreach (string valorInaceptable in valoresAceptables)
                            {
                                if (!String.IsNullOrEmpty(linea.Detalle) && linea.Detalle.StartsWith(valorInaceptable))
                                {
                                    satisfaceCondicion = false;
                                    break;
                                }
                            }
                        }

                        if (satisfaceCondicion)
                        {
                            linea.TieneErrorDetalle = false;
                        }
                        else
                        {
                            linea.TieneErrorDetalle = true;
                            string opcionesParaMensaje = valorCampo.valor_campo.TrimEnd(new char[] { ';' }).Replace(";", ", ");
                            linea.MensajeError = msg + opcionesParaMensaje;
                            return false;
                        }
                    }
                }

                if (lineaPrincipal != null)
                {
                    //hay lineas secundarias, tengo que evaluar 
                    bool todasOK = true;
                    string codCamLinea = (lineaPrincipal.TieneVariantes ? lineaPrincipal.VarianteSeleccionada : lineaPrincipal.CodCam);
                    List<LineaSecundariaEditorMensajeSwift> lineasSecundariasCorresponden = lineaPrincipal.LineasSecundarias.Where(s => s.CodCam == codCamLinea).ToList();
                    foreach (LineaSecundariaEditorMensajeSwift secundaria in lineasSecundariasCorresponden)
                    {
                        if (!ValidarLineaMensaje(secundaria, valoresCampos))
                        {
                            todasOK = false;
                        }
                    }

                    return todasOK;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("CuentaBancos"));
            }
           
        }

        public sw_casillas ObtenerCasillaPorId(int idCasilla)
        {
            try
            {
                return uow.CasillaRepository.GetByID(idCasilla);
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("ObtenerCasillaPorId"));
            }            
        }

        public sw_monedas ObtenerMonedaPorCodSw(string codSw)
        {
            try
            {
                return uow.MonedaRepository.Get(m => m.cod_moneda_sw == codSw).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("ObtenerMonedaPorCodSw"));
            }
            
        }

        public sw_tipos_msg ObtenerTipoMensajePorId(string codMT)
        {
            try
            {
                return uow.TiposMensajeRepository.Get(t => t.cod_tipo == codMT).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("ObtenerTipoMensajePorId"));
            }
            
        }

        private string TransformarFormato(string formato)
        {
            try
            {
                formato = formato.Replace("[", "");
                formato = formato.Replace("]", "");
                formato = formato.Replace("06G", "AAMMDD");
                formato = formato.Replace("08G", "AAAAMMDD");
                formato = formato.Replace("11T", "11T (Cód. Swift)");

                return formato;
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("TransformarFormato"));
            }
           
        }

        public IList<sbc_cpai> ListPaises()
        {
            try
            {
                using (var unitOfWork = new UnitOfWorkSbcor())
                {
                    return unitOfWork.PaisRepository.GetAll();
                }
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("ListPaises"));
            }            
        }

        public IList<pro_sce_swf_pendientes_s01_MS_Result> GetSwiftPendientes(string ctecct, string codusr)
        {
            try
            {
                var result = uowCext01.SceRepository.pro_sce_swf_pendientes_s01_MS(ctecct, codusr).ToList();
                return result;
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("GetSwiftPendientes"));
            }        
        }

        public pro_sce_swf_pendientes_s02_MS_Result ObtenerMensajeSwiftPendiente(string ctecct, string codusr, string archivo)
        {
            try
            {
                archivo = Utilidad.SanitizarString(archivo);

                var result = uowCext01.SceRepository.pro_sce_swf_pendientes_s02_MS(ctecct, codusr, archivo);
                return result;
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("ObtenerMensajeSwiftPendiente"));
            }            
        }

        public void EliminarMensajeSwiftPendiente(string ctecct, string codusr, string archivo)
        {
            try
            {
                archivo = Utilidad.SanitizarString(archivo);
                uowCext01.SceRepository.pro_sce_swf_pendiente_d01_MS(ctecct, codusr, archivo);
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("EliminarMensajeSwiftPendiente"));
            }            
        }

        public int ObtenerNumeroArchivo(string ctecct, string codusr, string codfun)
        {
            try
            {
                var mensaje = string.Empty;
                return (int)uowCext01.SceRepository.sce_rng_u01_MS(ctecct, codusr, codfun, out mensaje);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(string.Format("{0}({1}-{2} en {3})", "Error al obtener rango de correlativo del usuario: ", ctecct, codusr, codfun), ex);
            }
        }

        public int ObtenerNumeroArchivoSwift(string ctecct, string codusr, string codfun, string rutesp, float nummin, float nummax, float numact)
        {
            try
            {
                int result = uowCext01.SceRepository.sce_rng_ui01_MS(ctecct, codusr, codfun, rutesp, nummin, nummax, numact);
                return result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(string.Format("{0}({1}-{2} en {3})", "Error al obtener rango de correlativo del usuario: ", ctecct, codusr, codfun, rutesp, nummin, nummax, numact), ex);
            }
        }

        public bool GrabarBorrador(string ctecct, string codusr, string rut, string archivo, string sistema, DateTime fecha, string moneda, decimal monto, string referencia, string tipo, string cuerpo, bool esPlantilla)
        {
            try
            {
                archivo = Utilidad.SanitizarString(archivo);
                sistema = Utilidad.SanitizarString(sistema);
                referencia = Utilidad.SanitizarString(referencia);
                cuerpo = Utilidad.SanitizarString(cuerpo);
                //uowCext01.SceRepository.pro_sce_swf_pendiente_i01(ctecct, codusr, archivo, rut, sistema, fecha, tipo, moneda, monto, referencia, cuerpo, esPlantilla);
                uowCext01.SceRepository.pro_sce_swf_pendientes_i01_MS(ctecct, codusr, archivo, rut, sistema, fecha, tipo, moneda, monto, referencia, cuerpo, esPlantilla);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(Utilidad.customMessageError("GrabarBorrador"));
                // throw new ArgumentException(ex.Message, ex);
            }
        }

        public void GrabaCambiosMensaje(int idMensaje, string cambios)
        {
            try
            {
                cambios = Utilidad.SanitizarString(cambios);
                uow.SwRepository.proc_sw_env_graba_cambios(idMensaje, cambios);
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("GrabaCambiosMensaje"));
            }            
        }

        public bool UsuarioEsSupervisor(string ctecct, string codusr)
        {
            try
            {
                return uowCext01.SceRepository.Sce_Usr_S05_MS(ctecct, codusr).jerarquia == 1;
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("UsuarioEsSupervisor"));
            }            
        }

        /// <summary>
        /// <see cref="SwRepository.CamposMTActivosLibres() />
        /// </summary>
        /// <returns></returns>
        public static string[] CamposMTActivosLibres()
        {
            try
            {
                using (UnitOfWorkSwift _uow = new UnitOfWorkSwift())
                {
                    return _uow.SwRepository.CamposMTActivosLibres();
                }
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("CamposMTActivosLibres"));
            }            
        }

        /// <summary>
        /// <see cref="SwRepository.ValidaMTMonedaMonto(int)"/>
        /// </summary>
        /// <param name="mt"><paramref name="mt"/></param>
        /// <returns></returns>
        public bool ValidaMTMonedaMonto(string mt)
        {
            try
            {
                mt = Utilidad.SanitizarString(mt);
                return uow.SwRepository.ValidaMTMonedaMonto(mt);
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("ValidaMTMonedaMonto"));
            }            
        }

        /// <summary>
        /// <see cref="SwRepository.ObtieneCamposSumatoriaMontoTotalMT(int)"/>
        /// </summary>
        /// <param name="mt"><paramref name="mt"/>MT mensaje a consultar</param>
        /// <returns>Listado de campos para la sumatoria del monto total</returns>
        public List<string> ObtieneCamposSumatoriaMontoTotalMT(string mt)
        {
            try
            {
                mt = Utilidad.SanitizarString(mt);
                return uow.SwRepository.ObtieneCamposSumatoriaMontoTotalMT(mt);
            }
            catch (Exception)
            {
                throw new Exception(Utilidad.customMessageError("ObtieneCamposSumatoriaMontoTotalMT"));
            }
            
        }
    }
}
