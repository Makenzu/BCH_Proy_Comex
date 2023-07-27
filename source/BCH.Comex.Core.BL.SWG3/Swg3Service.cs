using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.GeneracionMT300;
using BCH.Comex.Core.Entities.Cext01.MT300Common;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Swift;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BCH.Comex.Core.BL.SWG3
{
    public class Swg3Service : IDisposable
    {
        private readonly UnitOfWorkCext01 uow;
        private readonly UnitOfWorkSwift uowSwift;

        public Swg3Service()
        {
            uow = new UnitOfWorkCext01();
            uowSwift = new UnitOfWorkSwift();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (uow != null)
            {
                uow.Dispose();
            }
            if (uowSwift != null)
            {
                uowSwift.Dispose();
            }
        }

        /// <summary>
        /// Inicia la aplicación con datos iniciales de configuración
        /// </summary>
        /// <param name="datosUsuario"></param>
        /// <returns></returns>
        public DatosGlobales Iniciar(IDatosUsuario datosUsuario)
        {
            using (Tracer tracer = new Tracer("Swg3Service - Iniciar"))
            {
                DatosGlobales globales = new DatosGlobales();
                globales.DatosUsuario = datosUsuario;

                return globales;
            }
        }

        public Dictionary<string, string> ObtieneParametrosArchivo(DatosGlobales globales)
        {
            using (Tracer tracer = new Tracer("Carga de archivo MT300"))
            {
                tracer.TraceVerbose("Iniciamos la carga de configuraciones de archivo");

                List<ParametroComex> parametrosConfig = this.uow.ParametroComexRepository.GetParametrosMT300("CONFIG", "", "").ToList();

                Dictionary<string, string> outputConfig = new Dictionary<string, string>();

                foreach (ParametroComex parametro in parametrosConfig)
                {
                    outputConfig.Add(parametro.trans_dsc_parametro, parametro.trans_vlr_parametro);
                }

                return outputConfig;
            }
        }

        public void ProcesarArchivo(DatosGlobales globales, HttpPostedFileBase file, string tipoArchivo, IDatosUsuario usuario)
        {
            using (Tracer tracer = new Tracer("Carga de archivo MT300"))
            {
                tracer.TraceVerbose("Iniciamos la apertura del archivo " + file.FileName);
                ParametroComex paramTiposArchivo;
                try
                {
                    List<ParametroComex> paramsConfig = this.uow.ParametroComexRepository.GetParametrosMT300("CARGA", "", "").ToList();

                    paramTiposArchivo = paramsConfig.Single(param => param.trans_dsc_parametro == "tipos_archivo");
                }
                catch (Exception e)
                {
                    tracer.TraceVerbose("Error al cargar parámetros de carga, paso 1" + file.FileName);
                    tracer.TraceVerbose(e.ToString());
                    throw new Exception("Error interno: No se pueden identificar los tipos de archivos en el Sistema Comex, contacte al administrador", e);
                }

                if (string.IsNullOrEmpty(paramTiposArchivo.trans_vlr_parametro))
                {
                    tracer.TraceVerbose("Error al cargar tipos de archivos desde los parametros porque no cumplen con lo esperado VALUTA;TREASURY, paso 2" + file.FileName);
                    throw new Exception("Error interno: No existe registro de los tipos de archivos del Sistema Comex, contacte al administrador");
                }

                string formatoFecha, campoSafekeeping, campoReference, campoAmountClp, campoBookedBy, campoRate, campoAmountUsd, campoDate, campoCurrency, campoCompra,campoVenta, campoExecutionTime;
                int rowIni;
                try
                {
                    tracer.TraceVerbose("Inicio de proceso de carga de información de los registros de archivo, paso 3" + file.FileName);

                    List<ParametroComex> paramConfigTipo = this.uow.ParametroComexRepository.GetParametrosMT300("CARGA", tipoArchivo, "").ToList();

                    formatoFecha = paramConfigTipo.Single(param => param.trans_dsc_parametro.Equals("formato_fecha")).trans_vlr_parametro;
                    campoSafekeeping = paramConfigTipo.Single(param => param.trans_dsc_parametro.Equals("safekeeping")).trans_vlr_parametro;
                    campoReference = paramConfigTipo.Single(param => param.trans_dsc_parametro.Equals("reference")).trans_vlr_parametro;
                    campoAmountClp = paramConfigTipo.Single(param => param.trans_dsc_parametro.Equals("amount_mn")).trans_vlr_parametro;
                    campoBookedBy = paramConfigTipo.Single(param => param.trans_dsc_parametro.Equals("booked_by")).trans_vlr_parametro;
                    campoRate = paramConfigTipo.Single(param => param.trans_dsc_parametro.Equals("rate")).trans_vlr_parametro;
                    campoAmountUsd = paramConfigTipo.Single(param => param.trans_dsc_parametro.Equals("amount_me")).trans_vlr_parametro;
                    campoDate = paramConfigTipo.Single(param => param.trans_dsc_parametro.Equals("date")).trans_vlr_parametro;
                    campoCurrency = paramConfigTipo.Single(param => param.trans_dsc_parametro.Equals("currency")).trans_vlr_parametro;
                    if (tipoArchivo != "VALUTA")
                    {
                        campoCompra = paramConfigTipo.Single(param => param.trans_dsc_parametro.Equals("compra")).trans_vlr_parametro;
                        campoVenta = paramConfigTipo.Single(param => param.trans_dsc_parametro.Equals("venta")).trans_vlr_parametro;
                        campoExecutionTime = paramConfigTipo.Single(param => param.trans_dsc_parametro.Equals("time")).trans_vlr_parametro;
                    }
                    else 
                    {
                        campoCompra = "";
                        campoVenta = "";
                        campoExecutionTime = "";
                    }
                    rowIni = int.Parse(paramConfigTipo.Single(param => param.trans_dsc_parametro.Equals("row_ini")).trans_vlr_parametro);
                }
                catch (Exception e)
                {
                    tracer.TraceVerbose("Error al cargar los campos que deben ser procesados de la tabla de parametros, paso 3" + file.FileName);
                    tracer.TraceVerbose(e.ToString());
                    throw new Exception("Error interno: No se puede identificar las configuraciones de los campos en el Sistema Comex, contacte al administrador", e);
                }

                Archivo archivo = new Archivo
                {
                    nombre = file.FileName,
                    total_registros = 0,
                    total_mt300_nuevos = 0,
                    total_mt300_existentes = 0,
                    total_registros_error = 0,
                    fecha_carga = DateTime.Now,
                    estado = "CARGA",
                    origen = "WEB_COMEX",
                    tipo_archivo = tipoArchivo
                };

                try
                {
                    tracer.TraceVerbose("Procedemos a persistir la información del archivo en BD, paso 3.1" + file.FileName);
                    archivo = this.uow.ArchivosRepository.AddArchivoMT300(archivo);
                }
                catch (Exception e)
                {
                    tracer.TraceVerbose("Error al persistir la información del archivo en BD, paso 3.1" + file.FileName);
                    tracer.TraceVerbose(e.ToString());
                    throw new Exception("Error interno: Existe un al intentar procesar el archivo, contacte al administrador", e);
                }

                List<ArchivoDetalle> registros = new List<ArchivoDetalle>();

                var fileStream = file.InputStream;

                try
                {
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        SLDocument doc = new SLDocument(fileStream);
                        SLWorksheetStatistics stats = doc.GetWorksheetStatistics();
                        /* Datos encabezado Archivo */
                        var compra = GetCellValueFromStartingCoord(doc, campoCompra, 0);
                        var venta = GetCellValueFromStartingCoord(doc, campoVenta, 0);
                        var compra_venta = "";
                        var excutionTimehhmmss = "000000";
                        int regblancos = 0;

                        //lee campo time, si no viene o si hay error asume cero
                        if (tipoArchivo != "VALUTA")
                        {
                            try
                            {
                                excutionTimehhmmss = GetCellValueFromStartingCoord(doc, campoExecutionTime, 0);
                                excutionTimehhmmss = excutionTimehhmmss.Replace(":","");
                                if (excutionTimehhmmss.Length == 4 && int.Parse(excutionTimehhmmss) < 2400)
                                {
                                    //incorpora segundos
                                    excutionTimehhmmss = excutionTimehhmmss + "00"; 
                                }
                                else {
                                    //sino tiene el formato de digitos asume default 
                                    excutionTimehhmmss = "000000";
                                }
                            }
                            catch (Exception e)
                            {
                                tracer.TraceVerbose("Error al tratar de obtener valor campo time, asume por defecto 00:00 " + file.FileName);
                                tracer.TraceVerbose(e.ToString());
                            }
                        }

                        if (!String.IsNullOrWhiteSpace(compra) || !String.IsNullOrWhiteSpace(venta))
                        {
                            if (!String.IsNullOrWhiteSpace(compra))
                                compra_venta = TipoFormatoPlanilla.compra;
                            else
                                compra_venta = TipoFormatoPlanilla.venta;
                        }
                        else
                        {
                            if (tipoArchivo == "VALUTA")
                                compra_venta = TipoFormatoPlanilla.venta;
                            else
                                compra_venta = "";
                        }

                        for (var row = rowIni; row <= stats.EndRowIndex; row++)
                        {
                            var safekeeping = GetCellValueFromStartingCoord(doc, campoSafekeeping, row);
                            var reference = GetCellValueFromStartingCoord(doc, campoReference, row);
                            var amountClp = GetCellValueFromStartingCoord(doc, campoAmountClp, row);
                            var bookedBy = GetCellValueFromStartingCoord(doc, campoBookedBy, row);
                            var rate = GetCellValueFromStartingCoord(doc, campoRate, row);
                            var amountUsd = GetCellValueFromStartingCoord(doc, campoAmountUsd, row);
                            var date = GetCellValueFromStartingCoord(doc, campoDate, row);
                            var curr = GetCellValueFromStartingCoord(doc, campoCurrency, row);

                            if (String.IsNullOrEmpty(safekeeping) && String.IsNullOrEmpty(reference) && String.IsNullOrEmpty(amountClp))
                            {
                                regblancos++;
                                if (regblancos <= 10)
                                {
                                    continue;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            archivo.total_registros++;

                            //Valida si es candidato?

                            ArchivoDetalle registro = new ArchivoDetalle();
                            registro.id_archivo = archivo.id_archivo;
                            registro.flag_formato = 1;
                            
                            string messageError = "";

                            //Validaciones de Safekeeping
                            if (!String.IsNullOrWhiteSpace(safekeeping))
                            {
                                try
                                {
                                    registro.safekeeping = Decimal.Parse(safekeeping);
                                    if (registro.safekeeping.ToString().Length != 6)
                                    {
                                        messageError += "El campo safekeeping " + safekeeping + " supera el largo máximo (6) permitido.|";
                                        registro.flag_formato = 0;
                                    }
                                }
                                catch
                                {
                                    messageError += "El campo safekeeping " + safekeeping + " no es un valor númerico valido.|";
                                    registro.flag_formato = 0;
                                }

                            }
                            else
                            {
                                messageError += "El campo safekeeping " + safekeeping + " es nulo, esta vacia o contiene caracteres de espacio en blanco.|";
                                registro.flag_formato = 0;
                            }

                            IList<Mt300Custodia> custodia = this.uow.Mt300CustodiaRepository.GetCustodia(registro.safekeeping.ToString()).ToList();
                            if (custodia.Count == 0 || custodia.First().ind_mt300 != "S" || custodia.First().tipo_mt300 != "B")
                            {
                                registro.flag_citi = 0;
                                messageError += "El registro no pertenece a un cliente válido.|";
                                registro.campo87A = "";
                                registro.campo53A = "";
                            }
                            else
                            {
                                registro.flag_citi = 1;
                                registro.campo87A = custodia.First().bic_destino_mt300;
                                registro.campo53A = custodia.First().bic_destino_mt300;
                            }

                            //Validaciones de reference
                            if (!String.IsNullOrWhiteSpace(reference))
                            {
                                if (reference.Trim().Length <= 15)
                                    registro.reference = reference.Trim();
                                else
                                {
                                    messageError += "El largo del campo reference " + reference + " no debe ser mayor a 15.|";
                                    registro.flag_formato = 0;
                                    registro.reference = "";
                                }
                            }
                            else
                            {
                                messageError += "El campo reference " + reference + " es nulo, esta vacia o contiene caracteres de espacio en blanco.|";
                                registro.flag_formato = 0;
                                registro.reference = "";
                            }

                            //Validaciones de amountClp
                            if (!String.IsNullOrWhiteSpace(amountClp))
                            {
                                try
                                {
                                    registro.amount_mn = Decimal.Parse(amountClp, CultureInfo.InvariantCulture);
                                }
                                catch
                                {
                                    messageError += "El campo amountClp " + amountClp + " no cumple con el formato moneda valido.|";
                                    registro.flag_formato = 0;
                                }
                            }
                            else
                            {
                                messageError += "El campo amountClp " + amountClp + " es nulo, esta vacia o contiene caracteres de espacio en blanco.|";
                                registro.flag_formato = 0;
                            }

                            //Validaciones de amountUsd
                            if (!String.IsNullOrWhiteSpace(amountUsd))
                            {
                                try
                                {
                                    registro.amount_me = Decimal.Parse(amountUsd, CultureInfo.InvariantCulture);
                                }
                                catch
                                {
                                    messageError += "El campo amountUsd " + amountUsd + " no cumple con el formato moneda valido.|";
                                    registro.flag_formato = 0;
                                }
                            }
                            else
                            {
                                messageError += "El campo amountUsd " + amountUsd + " es nulo, esta vacia o contiene caracteres de espacio en blanco.|";
                                registro.flag_formato = 0;
                            }

                            //Validaciones de rate
                            if (!String.IsNullOrWhiteSpace(rate))
                            {
                                if (rate.Trim().Length <= 12)
                                {
                                    try
                                    {
                                        registro.rate = Decimal.Parse(rate, CultureInfo.InvariantCulture);
                                    }
                                    catch
                                    {
                                        messageError += "El campo rate " + rate + " no cumple con el formato moneda valido.|";
                                        registro.flag_formato = 0;
                                    }
                                }
                                else
                                {
                                    messageError += "El campo rate " + rate + " supera el màximo largo definido.|";
                                    registro.flag_formato = 0;
                                }
                            }
                            else
                            {
                                messageError += "El campo rate " + rate + " es nulo, esta vacia o contiene caracteres de espacio en blanco.|";
                                registro.flag_formato = 0;
                            }

                            //Validaciones de bookedBy
                            if (!String.IsNullOrWhiteSpace(bookedBy) && bookedBy.Replace("-", "") != "19000101" && bookedBy.Replace("-", "") != "01011900")
                            {
                                try
                                {
                                    registro.booked_by = DateTime.ParseExact(bookedBy, formatoFecha, CultureInfo.InvariantCulture);
                                }
                                catch
                                {
                                    messageError += "El campo bookedBy " + bookedBy + " no cumple con el formato fecha valido.|";
                                    registro.flag_formato = 0;
                                    registro.booked_by = DateTime.ParseExact("19000101", "yyyyMMdd", null);
                                }
                            }
                            else
                            {
                                messageError += "El campo bookedBy " + bookedBy + " es nulo, esta vacia o el formato no es valido.|";
                                registro.flag_formato = 0;
                                registro.booked_by = DateTime.ParseExact("19000101", "yyyyMMdd", null);
                            }

                            //Validaciones de codigo moneda
                            if (!String.IsNullOrWhiteSpace(curr))
                            {
                                if (isAlphaNumeric(curr))
                                {
                                    if (curr.Length == 3)
                                        registro.codigo_moneda_me = curr;
                                    else
                                    {
                                        messageError += "El largo campo codigo moneda " + curr + " no corresponde.|";
                                        registro.flag_formato = 0;
                                        registro.codigo_moneda_me = "";
                                    }
                                }
                                else
                                {
                                    messageError += "El campo codigo moneda " + curr + " no es alfanumérico.|";
                                    registro.flag_formato = 0;
                                    registro.codigo_moneda_me = "";
                                }
                            }
                            else
                            {
                                messageError += "El campo codigo moneda " + curr + " es nulo, esta vacia o contiene caracteres de espacio en blanco.|";
                                registro.flag_formato = 0;
                                registro.codigo_moneda_me = "";
                            }

                            if (!String.IsNullOrWhiteSpace(date) && date.Replace("-", "") != "19000101" && date.Replace("-", "") != "01011900")
                            {
                                try
                                {
                                    registro.value_date = DateTime.ParseExact(date, formatoFecha, CultureInfo.InvariantCulture);
                                }
                                catch
                                {
                                    messageError += "El campo date " + date + " no cumple con el formato fecha valido.|";
                                    registro.flag_formato = 0;
                                    registro.value_date = DateTime.ParseExact("19000101", "yyyyMMdd", null);
                                }
                            }
                            else
                            {
                                messageError += "El campo date " + date + " es nulo, esta vacia o el formato no es valido.|";
                                registro.flag_formato = 0;
                                registro.value_date = DateTime.ParseExact("19000101", "yyyyMMdd", null);
                            }

                            //Se asigna nombre beneficiario desde campo benfname de tabla custodia
                            if (custodia.Count > 0)
                            {
                                registro.beneficiary = custodia.First().benfname;
                            }
                            else
                            {
                                registro.beneficiary = "SIN INFORMACION";
                            }

                            if (String.IsNullOrEmpty(messageError))
                            {
                                registro.flag_formato = 1;
                            }
                            else
                                registro.flag_formato = 0;

                            if (!String.IsNullOrWhiteSpace(reference))
                            {
                                if (this.uow.Mt300ArchivosProcesadosRepository.ExistsArchivoProcesado(registro.reference))
                                {
                                    registro.flag_existente = 1;
                                }
                                else
                                {
                                    registro.flag_existente = 0;
                                }
                            }
                            else
                            {
                                registro.flag_existente = 0;
                            }

                            /*TODO: Averiguaría que tipos de reglas nack se aplica acá*/

                            string errorX = "";
                            if (Fn_ValidaCaracteresX(registro.reference, out errorX))
                            {
                                registro.flag_nack = 1;
                            }
                            else
                            {
                                messageError += "Error en la validación de caracteres campo reference : " + errorX + ".|";
                                registro.flag_nack = 0;
                            }

                            if (registro.flag_formato == 1 && registro.flag_nack == 1 && registro.flag_citi == 1)
                            {
                                registro.flag_validaciones = 1;
                                registro.estado = EstadosRegistro.pendiente;
                            }
                            else
                            {
                                registro.flag_validaciones = 0;
                                registro.estado = EstadosRegistro.errorValidacion;
                            }


                            registro.resultado = "OK";
                            registro.codigo_moneda_mn = "";
                            registro.campo22C = "";
                            registro.campo82A = "BCHICLRM";
                            //registro.campo87A = "";
                            //registro.campo53A = "";
                            registro.campo57A = "";
                            registro.tipo_operacion = compra_venta;
                            registro.executionTimehhmmss = excutionTimehhmmss;

                            registro = this.uow.ArchivosDetalleRepository.AddArchivoDetalleMT300(registro);

                            Mt300Bitacora mt300Bitacora = new Mt300Bitacora();
                            mt300Bitacora.id_archivo = archivo.id_archivo;
                            mt300Bitacora.id_archivo_detalle = registro.id_archivo_detalle;
                            mt300Bitacora.usuario = usuario.samAccountName;
                            mt300Bitacora.resultado = messageError;
                            mt300Bitacora.tipo_movimiento = TipoMovimientoBitacora.procesamiento;
                            this.uow.Mt300BitacoraRepository.AddBitacoraMT300(mt300Bitacora);

                            // Guarda en el arreglo los registros candidatos
                            if (registro.flag_citi == 1)
                            {
                                registros.Add(registro);
                            }

                        }
                        if (archivo.total_registros == 0)
                        {
                            throw new Mt300Exception("El archivo cargado no tiene registros para procesar.");
                        }
                        //else
                        //{
                        //    decimal totalreg = archivo.total_registros;
                        //    archivo = this.uow.ArchivosRepository.UpdaterArchivoMT300(archivo);
                        //    archivo.total_registros = totalreg;
                        //}
                    }
                }
                catch (Mt300Exception err300)
                {
                    tracer.TraceVerbose("Error de negocio al cargar archivo a procesar." + file.FileName);
                    tracer.TraceVerbose(err300.ToString());
                    throw new Exception(err300.Message, err300);
                }
                catch (Exception e)
                {
                    tracer.TraceVerbose("Error al abrir el stream del archivo a procesar, paso 3.2" + file.FileName);
                    tracer.TraceVerbose(e.ToString());
                    //TODO : se debe validar como discriminar por el error en archivos de tipo excel que no se pueden procesar
                    if (e.Message.Trim() != "El archivo contiene datos dañados." && e.Message.Trim() != "File contains corrupted data.")
                    {
                        throw new Exception("Existe un error de comunicación con el procesamiento del archivo en el sistema contacte al administrador", e);
                    }
                    else
                    {
                        throw new Exception("El archivo a cargar está dañado o el formato no es válido. Favor verificar.", e);
                    }
                }

                archivo.total_mt300_nuevos = registros.Count(reg => reg.flag_citi == 1 && reg.flag_existente == 0 && reg.flag_validaciones == 1);
                archivo.total_mt300_existentes = registros.Count(reg => reg.flag_citi == 1 && reg.flag_existente == 1);
                archivo.total_registros_error = registros.Count(reg => reg.flag_citi == 1 && reg.flag_validaciones == 0);

                archivo = this.uow.ArchivosRepository.UpdateArchivoMT300(archivo);

                globales.DatosCarga = new T_ModCarga();
                globales.DatosCarga.archivo = archivo;
                globales.DatosCarga.registros = registros;
            }
        }

        private string GetCellValueFromStartingCoord(SLDocument doc, string startingCoord, int currentRow)
        {
            string result = "";
            if (!String.IsNullOrEmpty(startingCoord))
            {
                string[] coordArray = startingCoord.Split('|');
                foreach (string coord in coordArray)
                {
                    if (coord.Contains("$"))
                    {
                        var FieldType = doc.GetCellStyle(coord.Replace("$", ""));

                        if (FieldType.FormatCode.Contains("d-mmm-yy"))
                        {
                            var valueDateTime = doc.GetCellValueAsDateTime(coord.Replace("$", ""));
                            result = valueDateTime.ToString("dd-MM-yyyy");
                        }
                        else if (FieldType.FormatCode.Contains("h:mm"))
                        {
                            var valueDateTime = doc.GetCellValueAsDateTime(coord.Replace("$", ""));
                            result = valueDateTime.ToString("HH:mm");
                        }
                        else
                        {
                            result = doc.GetCellValueAsString(coord.Replace("$", ""));
                        }
                    }
                    else if (coord.Contains("&"))
                    {
                        result = doc.GetCellValueAsString(coord.Replace("&", ""));
                    }
                    else
                    {
                        result = doc.GetCellValueAsString(currentRow, GetColumnFromExcelCoordinate(startingCoord));
                    }

                    if (!string.IsNullOrEmpty(result) && result.Replace("-","").Replace("/","")!="01011900")
                    {
                        break;
                    }
                }
            }

            return result;
        }

        private int GetColumnFromExcelCoordinate(string coordinate)
        {
            int rowStart = coordinate.IndexOfAny("0123456789".ToArray());
            string column = coordinate.Substring(0, rowStart);
            return ExcelColumnNameToNumber(column);
        }
        
        private int ExcelColumnNameToNumber(string columnName)
        {
            columnName = columnName.ToUpperInvariant();

            int sum = 0;
            for (int i = 0; i < columnName.Length; i++)
            {
                sum *= 26;
                sum += columnName[i] - 'A' + 1;
            }

            return sum;
        }


        private static Boolean isAlphaNumeric(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9\s,]*$");
            return rg.IsMatch(strToCheck);
        }
        
        public string InsertaArchivoProcesado(ArchivoDetalle registro) {

            using (Tracer tracer = new Tracer("Inserta Archivos Procesados MT300"))
            {
                string retorno = "";
                string benecifiario = registro.beneficiary != null ? registro.beneficiary : "";
                this.uow.SceRepository.mt300_ins_registro_procesado(ref retorno, registro, benecifiario);
                tracer.TraceVerbose(string.Format("se inserto registro id swift {0} reference {1} con resultado {2}", registro.id_swift, registro.reference, retorno));

                return retorno;
            }    
        }

        public string UpdateArchivoProcesado(ArchivoDetalle registro)
        {

            using (Tracer tracer = new Tracer("Actualiza Archivos Procesados MT300"))
            {
                string retorno = "";
                registro.beneficiary = registro.beneficiary != null ? registro.beneficiary : "";
                this.uow.SceRepository.mt300_upd_registro_procesado(ref retorno, registro);
                tracer.TraceVerbose(string.Format("se actualizó registro procesado {0} id swift {1} reference {2} con resultado {3}", registro.id_procesados, registro.id_swift, registro.reference, retorno));

                return retorno;
            }
        }

        public Dictionary<string, string> ObtieneParametrosGeneracion()
        {
            using (Tracer tracer = new Tracer("Obtiene Parametros Generacion MT300"))
            {
                tracer.TraceVerbose("Iniciamos la carga de constantes para generacion de MT300");
                List<ParametroComex> parametrosConfig = this.uow.ParametroComexRepository.GetParametrosMT300("GENERACION MT 300", "", "").ToList();
                Dictionary<string, string> outputConfig = new Dictionary<string, string>();
                foreach (ParametroComex parametro in parametrosConfig)
                {
                    outputConfig.Add(parametro.trans_dsc_parametro, parametro.trans_vlr_parametro);
                }

                return outputConfig;
            }

        }

        public List<int> ObtieneListaRutFirmantes(string tipoFirmante)
        {
            using (Tracer tracer = new Tracer("Obtiene Lista Rut Firmantes"))
            {
                tracer.TraceVerbose("Iniciamos la carga de Lista Rut Firmantes");
                List<ParametroComex> parametrosConfig = this.uow.ParametroComexRepository.GetParametrosMT300(tipoFirmante, "Autorizador", "*").ToList();
                List<int> listaRuts = new List<int>();
                string rutSinDigitoV = "";
                try {

                    foreach (ParametroComex parametro in parametrosConfig)
                    {
                        rutSinDigitoV = parametro.trans_vlr_parametro;
                        if (rutSinDigitoV.Contains("-"))
                        {
                            rutSinDigitoV = rutSinDigitoV.Split('-')[0];
                        }
                        listaRuts.Add(int.Parse(rutSinDigitoV));
                    }
                }
                catch (FormatException)
                {
                    listaRuts = new List<int>();//si falla a lo menos 1 rut no retorna ninguno.
                    tracer.TraceError("Problemas al leer formato de rut en parametros Responsable Firma. [" + rutSinDigitoV+"] debe contener solo numeros. ");
                }

                return listaRuts;
            }

        }

        public void registraBitacora(Mt300Bitacora mt300Bitacora) {
            using (Tracer tracer = new Tracer("Registra Bitacora"))
            {
                try
                {
                    this.uow.Mt300BitacoraRepository.AddBitacoraMT300(mt300Bitacora);
                }
                catch (Exception ex)
                {
                    tracer.TraceError("Error al insertar bitacora " + ex);
                }
            }
        }

        private static bool Fn_ValidaCaracteresX(string cadena, out string mensajeError)
        {
            mensajeError = String.Empty;



            Regex regex = new Regex("[^A-Za-z0-9/\\-?:().,'+\n\r ]");
            MatchCollection invalidCharacters = regex.Matches(cadena);

            if (invalidCharacters.Count > 0)
            {
                string[] arr;
                arr = invalidCharacters.OfType<Match>()
                .Select(m => m.Groups[0].Value)
                .Distinct()
                .ToArray();

                string invalidCharsString = "'" + string.Join("', '", arr) + "'";
                mensajeError = "Caracteres inválidos: " + invalidCharsString;

                return false;
            }

            return true;
        }

        public List<ArchivoDetalle>  ObtenerArchivoDetalleDesdeMesaMT300() {

            List<ArchivoDetalle> registros = new List<ArchivoDetalle>();
            registros = this.uow.ArchivosDetalleRepository.getArchivoDetalleDesdeMesaMT300();
            return registros;
        }

        public int ActualizarArchivoEstadosFinProceso(decimal id_archivo)
        {
            return this.uow.ArchivosRepository.UpdateArchivoEstadosFinProceso(id_archivo);

        }
 
    }
}
