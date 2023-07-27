using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.SWG3;
using BCH.Comex.Core.Entities.Cext01.MT300Common;
using BCH.Comex.Core.Entities.Swift;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace BCH.Comex.Core.BL.SWG3.Helpers
{
    public class GenerarMT300Helper
    {

        protected Swg3Service service = new Swg3Service();
        protected EnvioSwiftServiceMT300 serviceEnvioSwift = new EnvioSwiftServiceMT300();
        private readonly AutorizacionMT300Service autService = new AutorizacionMT300Service();
        public const char modoAutomatico = 'A';
        public const char modoManual = 'M';
        public const string flujoPlanilla = "PLANILLA";
        public const string flujoMesaCambio = "MESACAMBIO";
        public const string flujoNuevoDesdeCero = "NUEVODESDECERO";
        public const string flujoModificacion = "GESTIONMODIFICACION";
        public const string flujoAnulacion = "GESTIONANULACION";
        public const string firmasParaPlanilla = "Firma SWIFT";
        public const string firmasParaMesa = "Firma SWIFT Mesa";
        /// <summary>
        /// Construye el string de mensaje swift segun datos de registros, envia el mensaje y autoriza segun parametros
        /// </summary>
        /// <param name="registrosParaProcesar">aqui debe venir uno o mas registros a procesar</param>
        /// <param name="infoUsuario">debe contener 3 datos importantes, nombre usuario, rut y centro de costo</param>
        /// <param name="modoEnvio">A: automatico, M:manual</param>
        /// <param name="flujoOrigen">indica el flujo desde el cual se invoca este metodo</param>
        public GenerarMT300Result GenerarSwiftMasivoMT300(List<ArchivoDetalle> registrosParaProcesar, GenerarMT300DatosUser infoUsuario, char modoEnvio, string flujoOrigen)
        {
            using (Tracer tracer = new Tracer("Genera Swift Masivo mensajes MT300 Helper"))
            {
                int totalProcesados = 0;
                int totalError = 0;
                int totalEnviadosSwiftOK = 0;
                int idMensajeVacio = 0;//cuando se envia cero genera id de mensaje swift automagicamente
                string idRegProcesado;
                string msgResultado = "";
                string msgBitacora = "";
                GenerarMT300Result genResult = new GenerarMT300Result();
                HashSet<decimal> listaIdArchivos = new HashSet<decimal>();
                string usuarioNombre;
                string usuarioRut;
                string usuarioCentroCosto;
                string monedaSwift ;
                double montoSwift; 


                try
                {
                    //Medicion tiempo ejecucion                    
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    Stopwatch swCiclo;

                    Mt300Bitacora bitacora = new Mt300Bitacora();

                    if (registrosParaProcesar == null)
                    {
                        genResult.CodigoSalida = 1;
                        genResult.Mensaje = "No hay datos para procesar";
                        return genResult;
                    }

                    //****Obtiene constantes desde tabla parametrica, si falta alguno envia mensaje y sale del flujo
                    Dictionary<string, string> paramGeneracion = this.service.ObtieneParametrosGeneracion();

                    List<int> listaRutFirmantes = new List<int>();
                    if (modoEnvio == modoAutomatico) {
                        if (flujoOrigen == flujoPlanilla) {
                            listaRutFirmantes = this.service.ObtieneListaRutFirmantes(firmasParaPlanilla);
                        }
                        else if (flujoOrigen == flujoMesaCambio)
                        {
                            listaRutFirmantes = this.service.ObtieneListaRutFirmantes(firmasParaMesa);
                        }
                    }

                    string resultadoValidacion = ValidacionParametrosGeneracion(paramGeneracion, listaRutFirmantes, modoEnvio, infoUsuario);
                    if ("".CompareTo(resultadoValidacion) != 0)
                    {
                        resultadoValidacion = "Faltan parametros en tablon stp_parametros_comex: " + resultadoValidacion;
                        tracer.TraceError(resultadoValidacion);
                        //registra bitacora
                        bitacora.usuario = (infoUsuario != null)?infoUsuario.usuarioNombre: "";
                        bitacora.tipo_movimiento = TipoMovimientoBitacora.generacion;
                        bitacora.resultado = resultadoValidacion;
                        service.registraBitacora(bitacora);
                        genResult.CodigoSalida = 1;
                        genResult.Mensaje = resultadoValidacion;
                        return genResult;

                    }

                    //****Inicializa algunos parametros comunes para los mensajes
                    String tipoMensajeMT = paramGeneracion["tipoMensajeMT"];
                    String codMonedaCLP = paramGeneracion["codMonedaCLP"];
                    String formatoFecha = paramGeneracion["formatoFecha"];

                    //****Obtiene formato generico vacio (platilla) de MT300
                    List<LineaEditorMensajeSwift> formatoLineas = serviceEnvioSwift.GetLineasYFormatosParaMT(tipoMensajeMT);
                    List<LineaEditorMensajeSwift> lineasMensaje;

                    //****Obtiene y recorre detalle de registros nuevos para enviar a swift
                    foreach (ArchivoDetalle item in registrosParaProcesar)
                    {
                        swCiclo = new Stopwatch();
                        swCiclo.Start();
                        item.estado = EstadosRegistro.procesando;
                        bitacora = new Mt300Bitacora();
                        bitacora.id_archivo = item.id_archivo;
                        bitacora.id_archivo_detalle = item.id_archivo_detalle;
                        bitacora.tipo_movimiento = TipoMovimientoBitacora.generacion;
                        msgBitacora = "";
                        listaIdArchivos.Add(item.id_archivo);

                        try
                        {

                            //****inicializa datos usuario, si viene desde web comex existe infoUsuario, si es null se saca de parametros
                            if (infoUsuario != null)
                            {
                                usuarioNombre = infoUsuario.usuarioNombre;
                                usuarioRut = infoUsuario.usuarioRut;
                                usuarioCentroCosto = infoUsuario.usuarioCentroCosto;
                            }
                            else {
                                if (item.reference.StartsWith(paramGeneracion["apiUsuarioCentroCostoNoGSS"]))
                                {
                                    usuarioNombre = paramGeneracion["apiUsuarioNombreNoGSS"];
                                    usuarioRut = paramGeneracion["apiUsuarioRutNoGSS"];
                                    usuarioCentroCosto = paramGeneracion["apiUsuarioCentroCostoNoGSS"];
                                }
                                else {
                                    usuarioNombre = paramGeneracion["apiUsuarioNombreGSS"];
                                    usuarioRut = paramGeneracion["apiUsuarioRutGSS"];
                                    usuarioCentroCosto = paramGeneracion["apiUsuarioCentroCostoGSS"];
                                }
                            }
                            bitacora.usuario = usuarioNombre;

                            //****construye campos MT-300
                            tracer.TraceInformation(String.Format("Procesando registro con id_archivo:{0}, id_archivo_detalle: {1}, reference:{2}  ", item.id_archivo, item.id_archivo_detalle, item.reference));
                            totalProcesados++;

                            //inicializa campo98D si esta vacio
                            if (string.IsNullOrWhiteSpace(item.campo98D)) {
                                item.campo98D = item.booked_by.ToString(formatoFecha) + item.executionTimehhmmss;
                            }

                            if (flujoOrigen == flujoPlanilla)
                            {
                                item.codigo_moneda_mn = codMonedaCLP;

                                //cuando es compra invierte campos montos que registra la tabla msgsend
                                if (TipoFormatoPlanilla.venta == item.tipo_operacion)
                                {
                                    monedaSwift = item.codigo_moneda_mn;
                                    montoSwift = Decimal.ToDouble(item.amount_mn);
                                }
                                else
                                {
                                    monedaSwift = item.codigo_moneda_me;
                                    montoSwift = Decimal.ToDouble(item.amount_me);
                                }

                                lineasMensaje = ConstruyelineasMensajePlanilla(formatoLineas, item, paramGeneracion);
                            }
                            else if (flujoOrigen == flujoMesaCambio)
                            {
                                //cuando es compra invierte campos montos que registra la tabla msgsend
                                if (TipoFormatoPlanilla.venta == item.tipo_operacion)
                                {
                                    monedaSwift = item.codigo_moneda_mn;
                                    montoSwift = Decimal.ToDouble(item.amount_mn);
                                }
                                else
                                {
                                    monedaSwift = item.codigo_moneda_me;
                                    montoSwift = Decimal.ToDouble(item.amount_me);
                                }

                                lineasMensaje = ConstruyelineasMensajeMesa(formatoLineas, item, paramGeneracion);
                            }
                            else if (flujoOrigen == flujoAnulacion)
                            {
                                lineasMensaje = ConstruyelineasMensajeWeb(formatoLineas, item, paramGeneracion, true);
                                monedaSwift = item.codigo_moneda_mn;
                                montoSwift = Decimal.ToDouble(item.amount_mn);
                            }
                            else
                            {
                                lineasMensaje = ConstruyelineasMensajeWeb(formatoLineas, item, paramGeneracion, false);
                                monedaSwift = item.codigo_moneda_mn;
                                montoSwift = Decimal.ToDouble(item.amount_mn);
                            }

                            bool validacionOK = serviceEnvioSwift.ValidarLineasMensaje(lineasMensaje);
                            if (validacionOK)
                            {
                                string swiftBancoReceptor = item.campo87A;
                                //****AQUI CONSTRUYE STRING DE SWIFT con formato de llaves tipo '{1:blabla}'
                                string mensajeSwiftFormatoLlaves = serviceEnvioSwift.GenerarMensajeSwift(lineasMensaje, tipoMensajeMT, swiftBancoReceptor);
                                int? idMensajeNuevo = null;
                                tracer.TraceInformation("Swift generado : [" + mensajeSwiftFormatoLlaves + "]");
                                item.mensaje_mt = mensajeSwiftFormatoLlaves;

                                //****AQUI GUARDA EN LA BD SWIFT (en sw_msgsend con estado INY)
                                bool envioOK = serviceEnvioSwift.GuardarMensajeSwift(idMensajeVacio, usuarioRut, int.Parse(usuarioCentroCosto), monedaSwift, montoSwift, item.beneficiary, mensajeSwiftFormatoLlaves, out idMensajeNuevo, modoEnvio);
                                item.id_swift = idMensajeNuevo.ToString();
                                if (envioOK)
                                {
                                    if (modoEnvio == modoAutomatico)
                                    {
                                        tracer.TraceInformation("Se va a autorizar en modo automatico");
                                        bool autorizacionOK = false;
                                        //****AQUI FIRMA Y AUTORIZA EL MENSAJE SWIFT (en sw_msgsend con estado SAP y luego AUM)
                                        autService.FirmaryAutorizarMensaje((int)idMensajeNuevo, int.Parse(usuarioRut), int.Parse(usuarioCentroCosto), listaRutFirmantes, paramGeneracion, ref autorizacionOK);
                                        if (autorizacionOK)
                                        {
                                            if (flujoOrigen == flujoModificacion)
                                            {
                                                item.estado = EstadosRegistro.procesadoModificado;
                                            }
                                            else if (flujoOrigen == flujoAnulacion)
                                            {
                                                item.estado = EstadosRegistro.procesadoAnulado;
                                            }
                                            else
                                            {
                                                item.estado = EstadosRegistro.procesadoNuevo;
                                            }
                                            
                                            totalEnviadosSwiftOK++;
                                            msgResultado = "Envio swift automatico OK con id swift[" + idMensajeNuevo + "]. ";
                                            tracer.TraceInformation(msgResultado);
                                            msgBitacora += "| " + msgResultado;
                                        }
                                        else
                                        {
                                            item.estado = EstadosRegistro.procesadoError;
                                            msgResultado = "Envio swift automatico con problemas al autorizar con id swift [" + idMensajeNuevo + "]. ";
                                            tracer.TraceError(msgResultado);
                                            msgBitacora += "| " + msgResultado;
                                            totalError++;
                                        }
                                    }
                                    else
                                    {
                                        tracer.TraceInformation("Se solicita autorizacion manual.");
                                        if (envioOK)
                                        {
                                            if (flujoOrigen == flujoModificacion)
                                            {
                                                item.estado = EstadosRegistro.procesadoModificado;
                                            }
                                            else if (flujoOrigen == flujoAnulacion)
                                            {
                                                item.estado = EstadosRegistro.procesadoAnulado;
                                            }
                                            else
                                            {
                                                item.estado = EstadosRegistro.procesadoNuevo;
                                            }
                                            totalEnviadosSwiftOK++;
                                            msgResultado = "Envio swift manual OK con id swift[" + idMensajeNuevo + "]. ";
                                            tracer.TraceInformation(msgResultado);
                                            msgBitacora += "| " + msgResultado;
                                        }
                                        else
                                        {
                                            item.estado = EstadosRegistro.procesadoError;
                                            msgResultado = "Envio swift manual con problemas [" + idMensajeNuevo + "]. ";
                                            tracer.TraceError(msgResultado);
                                            msgBitacora += "| " + msgResultado;
                                            totalError++;
                                        }
                                    }

                                }
                                else
                                {
                                    item.estado = EstadosRegistro.procesadoError;
                                    msgResultado = "Envio swift con problemas. ";
                                    tracer.TraceVerbose(msgResultado);
                                    msgBitacora += "| " + msgResultado;
                                    totalError++;
                                }
                            }
                            else
                            {
                                item.estado = EstadosRegistro.procesadoError;
                                msgResultado = "Validacion swift con problemas. ";
                                tracer.TraceVerbose(msgResultado);
                                msgBitacora += "| " + msgResultado;
                                totalError++;
                            }

                        }
                        catch (Exception ex)
                        {
                            totalError++;
                            item.estado = EstadosRegistro.procesadoError;
                            msgResultado = "Error en el proceso de Generar Swift MT300. ";
                            tracer.TraceError(msgResultado + ex);
                            msgBitacora += "| " + msgResultado + ex.Message;

                        }
                        finally
                        {
                            try
                            {
                                //si es registro nuevo desde cero lo marca en tabla procesados
                                if (flujoOrigen == flujoNuevoDesdeCero)
                                {
                                    item.flag_ingresado_nuevo = 1;
                                }
                                else
                                {
                                    item.flag_ingresado_nuevo = 0;
                                }

                                //****AQUI GUARDA EN EL MODELO MT300 tabla procesados
                                tracer.TraceVerbose("Se va a guardar el registro procesado en BD y en bitacora");
                                if (flujoOrigen == flujoModificacion || flujoOrigen == flujoAnulacion)
                                {
                                    idRegProcesado = service.UpdateArchivoProcesado(item);
                                }
                                else
                                {
                                    idRegProcesado = service.InsertaArchivoProcesado(item);
                                }
                                bitacora.id_procesados = Decimal.Parse(idRegProcesado, CultureInfo.InvariantCulture);
                                if (msgBitacora.StartsWith("|"))
                                {
                                    msgBitacora = msgBitacora.Remove(0, 1);
                                }
                                bitacora.resultado = msgBitacora;
                                bitacora.resultado_1 = swCiclo.Elapsed.ToString("hh\\:mm\\:ss");
                                service.registraBitacora(bitacora);

                            }
                            catch (Exception e)
                            {
                                tracer.TraceError("Hubo un problema al tratar de guardar el registro procesado en BD " + e);
                            }


                        }
                        swCiclo.Stop();
                        tracer.TraceInformation(string.Format("Tiempo de ejecucion registro: {0}", swCiclo.Elapsed.ToString("hh\\:mm\\:ss")));

                    }//(termina de recorrer todos los registros de la lista)

                    //marca lote como procesado en tabla archivo y archivo detalle cuando viene de planilla o api
                    if (flujoOrigen == flujoPlanilla || flujoOrigen == flujoMesaCambio) 
                    { 
                        foreach (decimal idArch in listaIdArchivos)
                        {
                            service.ActualizarArchivoEstadosFinProceso(idArch);
                        }
                    }
                    genResult.CodigoSalida = 0;
                    genResult.Mensaje = String.Format("Se generaron con exito [{0}] MT300 para el archivo cargado. Total procesados [{1}], Total erroneos [{2}]", totalEnviadosSwiftOK, totalProcesados, totalError);
                    genResult.CantTotal = totalProcesados;
                    genResult.CantGenerados = totalEnviadosSwiftOK;
                    genResult.CantError = totalError;
                    tracer.TraceInformation(String.Format("Se generaron {0} MT300 para el archivo cargado. Total procesados {1}, Total erroneos {2}", totalEnviadosSwiftOK, totalProcesados, totalError));

                    sw.Stop();
                    tracer.TraceInformation(string.Format("Tiempo de ejecucion totalgeneracion MT: {0}", sw.Elapsed.ToString("hh\\:mm\\:ss")));
                }
                catch (Exception e)
                {
                    genResult.CodigoSalida = 1;
                    genResult.Mensaje = "Hubo un error desconocido en la generacion de mensajes MT300, ver detalles en log de sistema.";
                    tracer.TraceError("Hubo un Error Desconocido en la generacion de mensajes" + e);
                }

                return genResult;
            }
        }


        public List<LineaEditorMensajeSwift> ConstruyelineasMensajePlanilla(List<LineaEditorMensajeSwift> lineasMensaje, ArchivoDetalle reg, Dictionary<string, string> paramGeneracion)
        {

            String formatoFecha = paramGeneracion["formatoFecha"];
            String constante15A = paramGeneracion["constante15A"];
            String constante22A = paramGeneracion["constante22A-nuevo"];
            String constante22C = paramGeneracion["constante22C"];
            String constante82A = paramGeneracion["constante82A"];
            String constante15B = paramGeneracion["constante15B"];
            String constante53A57Abch = paramGeneracion["constante53A-57A-bch"];

            bool esPrimerTag53 = true;
            bool esPrimerTag57 = true;
            string codigoMonedaAux ;
            decimal amountAux ;

            foreach (LineaEditorMensajeSwift campoMT300 in lineasMensaje)
            {
                switch (campoMT300.CodCam)
                {

                    case "15A":
                        campoMT300.Detalle = constante15A;
                        campoMT300.Incluido = true;
                        break;

                    case "20":
                        campoMT300.Detalle = reg.reference;
                        campoMT300.Incluido = true;
                        break;

                    case "22A":
                        campoMT300.Detalle = constante22A;
                        campoMT300.Incluido = true;
                        reg.campo22A = constante22A;
                        break;

                    case "22C":
                        campoMT300.Detalle = constante22C;
                        campoMT300.Incluido = true;
                        reg.campo22C = constante22C;
                        break;

                    case "82":
                        campoMT300.Detalle = constante82A;
                        campoMT300.Incluido = true;
                        campoMT300.VarianteSeleccionada = "82A";
                        foreach (LineaSecundariaEditorMensajeSwift lineaSecundaria in campoMT300.LineasSecundarias)
                        {
                            if (lineaSecundaria.CodCam == "82A")
                            {
                                lineaSecundaria.Detalle = constante82A;
                                reg.campo82A = constante82A;
                            }
                        }
                        break;
                    case "87":
                        //Aqui va el BIC del banco receptor, en el caso planilla es siempre el citi, desde la mesa puede variar
                        campoMT300.Detalle = reg.campo87A; ;
                        campoMT300.Incluido = true;
                        campoMT300.VarianteSeleccionada = "87A";
                        foreach (LineaSecundariaEditorMensajeSwift lineaSecundaria in campoMT300.LineasSecundarias)
                        {
                            if (lineaSecundaria.CodCam == "87A")
                            {
                                lineaSecundaria.Detalle = reg.campo87A;
                            }
                        }
                        break;
                    case "15B":
                        campoMT300.Detalle = constante15B;
                        campoMT300.Incluido = true;
                        break;
                    case "30T":
                        campoMT300.Detalle = reg.booked_by.ToString(formatoFecha);
                        campoMT300.Incluido = true;
                        break;
                    case "30V":
                        campoMT300.Detalle = reg.value_date.ToString(formatoFecha);
                        campoMT300.Incluido = true;
                        break;
                    case "36":
                        campoMT300.Detalle = campoRateAString(reg.rate);
                        campoMT300.Incluido = true;
                        break;
                    case "32B":
                        if (campoMT300.Secuencia == "B")
                        {
                            if (TipoFormatoPlanilla.venta == reg.tipo_operacion)
                            {
                                campoMT300.Detalle = reg.codigo_moneda_mn + serviceEnvioSwift.ObtieneMontoFormatoSwift(reg.codigo_moneda_mn, reg.amount_mn);
                            }
                            else
                            {
                                campoMT300.Detalle = reg.codigo_moneda_me + serviceEnvioSwift.ObtieneMontoFormatoSwift(reg.codigo_moneda_me, reg.amount_me);
                            }
                            campoMT300.Incluido = true;
                        }
                        break;
                    case "53":
                        if (campoMT300.Secuencia == "B")
                        {
                            campoMT300.Incluido = true;
                            campoMT300.VarianteSeleccionada = "53A";
                            foreach (LineaSecundariaEditorMensajeSwift lineaSecundaria in campoMT300.LineasSecundarias)
                            {
                                if (lineaSecundaria.CodCam == "53A" && lineaSecundaria.Linea == 1)
                                {
                                    if (esPrimerTag53)//bloque arriba de 33B
                                    {
                                        lineaSecundaria.Detalle = reg.campo53A;
                                    }
                                    else//bloque abajo de 33B
                                    {
                                        lineaSecundaria.Detalle = constante53A57Abch;
                                    }
                                    esPrimerTag53 = false;
                                }
                            }
                        }
                        break;
                    case "57":
                        if (campoMT300.Secuencia == "B")
                        {
                            campoMT300.Incluido = true;
                            campoMT300.VarianteSeleccionada = "57A";
                            foreach (LineaSecundariaEditorMensajeSwift lineaSecundaria in campoMT300.LineasSecundarias)
                            {
                                if (lineaSecundaria.CodCam == "57A" && lineaSecundaria.Linea == 1)
                                {
                                    if (esPrimerTag57)//bloque arriba de 33B
                                    {
                                        lineaSecundaria.Detalle = constante53A57Abch;
                                        reg.campo57A = constante53A57Abch;

                                    }
                                    else//bloque abajo de 33B
                                    {
                                        lineaSecundaria.Detalle = reg.campo53A;
                                    }
                                    esPrimerTag57 = false;
                                }
                            }
                        }
                        break;
                    case "33B":
                        //nombre en tabla: amount_clp
                        if (campoMT300.Secuencia == "B")
                        {
                            if (TipoFormatoPlanilla.venta == reg.tipo_operacion)
                            {
                                campoMT300.Detalle = reg.codigo_moneda_me + serviceEnvioSwift.ObtieneMontoFormatoSwift(reg.codigo_moneda_me, reg.amount_me);
                            }
                            else
                            {
                                campoMT300.Detalle = reg.codigo_moneda_mn + serviceEnvioSwift.ObtieneMontoFormatoSwift(reg.codigo_moneda_mn, reg.amount_mn);
                            }
                            campoMT300.Incluido = true;
                        }
                        break;
                    case "98D":
                        campoMT300.Detalle = reg.campo98D;
                        campoMT300.Incluido = true;
                        break;
                }
            }

            //cuando es compra invierte campo para guardar en registro procesados
            if (TipoFormatoPlanilla.compra == reg.tipo_operacion)
            {
                codigoMonedaAux = reg.codigo_moneda_mn;
                amountAux = reg.amount_mn;
                reg.codigo_moneda_mn = reg.codigo_moneda_me;
                reg.amount_mn = reg.amount_me;
                reg.codigo_moneda_me = codigoMonedaAux;
                reg.amount_me = amountAux;
            }

            return lineasMensaje;
        }



        public List<LineaEditorMensajeSwift> ConstruyelineasMensajeMesa(List<LineaEditorMensajeSwift> lineasMensaje, ArchivoDetalle reg, Dictionary<string, string> paramGeneracion)
        {
            String formatoFecha = paramGeneracion["formatoFecha"];
            String constante15A = paramGeneracion["constante15A"];
            String constante22A = paramGeneracion["constante22A-nuevo"];
            String constante15B = paramGeneracion["constante15B"];


            bool esPrimerTag53 = true;
            bool esPrimerTag57 = true;
            string codigoMonedaAux;
            decimal amountAux;

            foreach (LineaEditorMensajeSwift campoMT300 in lineasMensaje)
            {
                switch (campoMT300.CodCam)
                {

                    case "15A":
                        campoMT300.Detalle = constante15A;
                        campoMT300.Incluido = true;
                        break;

                    case "20":
                        campoMT300.Detalle = reg.reference;
                        campoMT300.Incluido = true;
                        break;

                    case "22A":
                        campoMT300.Detalle = constante22A;
                        campoMT300.Incluido = true;
                        reg.campo22A = constante22A;
                        break;

                    case "22C":
                        campoMT300.Detalle = (reg.campo22C == null) ? "" : reg.campo22C;
                        campoMT300.Incluido = true;
                        break;

                    case "82":
                        campoMT300.Detalle = reg.campo82A;
                        campoMT300.Incluido = true;
                        campoMT300.VarianteSeleccionada = "82A";
                        foreach (LineaSecundariaEditorMensajeSwift lineaSecundaria in campoMT300.LineasSecundarias)
                        {
                            if (lineaSecundaria.CodCam == "82A")
                            {
                                lineaSecundaria.Detalle = (reg.campo82A == null) ? "" : reg.campo82A; 
                            }
                        }
                        break;
                    case "87":
                        //Aqui va el BIC del banco receptor, en el caso planilla es siempre el citi, desde la mesa puede variar
                        campoMT300.Detalle = reg.campo87A;
                        campoMT300.Incluido = true;
                        campoMT300.VarianteSeleccionada = "87A";
                        foreach (LineaSecundariaEditorMensajeSwift lineaSecundaria in campoMT300.LineasSecundarias)
                        {
                            if (lineaSecundaria.CodCam == "87A")
                            {
                                lineaSecundaria.Detalle = reg.campo87A;
                                reg.campo87A = reg.campo87A;
                            }
                        }
                        break;
                    case "15B":
                        campoMT300.Detalle = constante15B;
                        campoMT300.Incluido = true;
                        break;
                    case "30T":
                        campoMT300.Detalle = reg.booked_by.ToString(formatoFecha);
                        campoMT300.Incluido = true;
                        break;
                    case "30V":
                        campoMT300.Detalle = reg.value_date.ToString(formatoFecha);
                        campoMT300.Incluido = true;
                        break;
                    case "36":
                        campoMT300.Detalle = campoRateAString(reg.rate);
                        campoMT300.Incluido = true;
                        break;
                    case "32B":
                        if (campoMT300.Secuencia == "B")
                        {
                            if (TipoFormatoPlanilla.venta == reg.tipo_operacion)
                            {
                                campoMT300.Detalle = reg.codigo_moneda_mn + serviceEnvioSwift.ObtieneMontoFormatoSwift(reg.codigo_moneda_mn, reg.amount_mn);
                            }
                            else
                            {
                                campoMT300.Detalle = reg.codigo_moneda_me + serviceEnvioSwift.ObtieneMontoFormatoSwift(reg.codigo_moneda_me, reg.amount_me);
                            }
                            campoMT300.Incluido = true;
                        }
                        break;
                    case "53":
                        if (campoMT300.Secuencia == "B")
                        {
                            campoMT300.Incluido = true;
                            campoMT300.VarianteSeleccionada = "53A";
                            foreach (LineaSecundariaEditorMensajeSwift lineaSecundaria in campoMT300.LineasSecundarias)
                            {
                                if (lineaSecundaria.CodCam == "53A" && lineaSecundaria.Linea == 1)
                                {
                                    if (esPrimerTag53)//bloque arriba de 33B
                                    {
                                        lineaSecundaria.Detalle = reg.campo53A;
                                    }
                                    else//bloque abajo de 33B
                                    {
                                        lineaSecundaria.Detalle = reg.campo57A;
                                    }
                                    esPrimerTag53 = false;
                                }
                            }
                        }
                        break;
                    case "57":
                        if (campoMT300.Secuencia == "B")
                        {
                            campoMT300.Incluido = true;
                            campoMT300.VarianteSeleccionada = "57A";
                            foreach (LineaSecundariaEditorMensajeSwift lineaSecundaria in campoMT300.LineasSecundarias)
                            {
                                if (lineaSecundaria.CodCam == "57A" && lineaSecundaria.Linea == 1)
                                {
                                    if (esPrimerTag57)//bloque arriba de 33B
                                    {
                                        lineaSecundaria.Detalle = reg.campo57A; ;
                                        reg.campo57A = reg.campo57A; ;

                                    }
                                    else//bloque abajo de 33B
                                    {
                                        lineaSecundaria.Detalle = reg.campo53A;
                                    }
                                    esPrimerTag57 = false;
                                }
                            }
                        }
                        break;
                    case "33B":
                        //nombre en tabla: amount_clp
                        if (campoMT300.Secuencia == "B")
                        {
                            if (TipoFormatoPlanilla.venta == reg.tipo_operacion)
                            {
                                campoMT300.Detalle = reg.codigo_moneda_me + serviceEnvioSwift.ObtieneMontoFormatoSwift(reg.codigo_moneda_me, reg.amount_me);
                            }
                            else
                            {
                                campoMT300.Detalle = reg.codigo_moneda_mn + serviceEnvioSwift.ObtieneMontoFormatoSwift(reg.codigo_moneda_mn, reg.amount_mn);
                            }
                            campoMT300.Incluido = true;
                        }
                        break;
                    case "98D":
                        campoMT300.Detalle = reg.campo98D;
                        campoMT300.Incluido = true;
                        break;
                }
            }

            //cuando es compra invierte campo para guardar en registro procesados
            if (TipoFormatoPlanilla.compra == reg.tipo_operacion) {                
                codigoMonedaAux = reg.codigo_moneda_mn;
                amountAux = reg.amount_mn;
                reg.codigo_moneda_mn = reg.codigo_moneda_me;
                reg.amount_mn = reg.amount_me;
                reg.codigo_moneda_me = codigoMonedaAux;
                reg.amount_me = amountAux;
            }

            return lineasMensaje;
        }

        public List<LineaEditorMensajeSwift> ConstruyelineasMensajeWeb(List<LineaEditorMensajeSwift> lineasMensaje, ArchivoDetalle reg, Dictionary<string, string> paramGeneracion, bool esCancelacion)
        {
            String formatoFecha = paramGeneracion["formatoFecha"];
            String constante15A = paramGeneracion["constante15A"];
            String constante15B = paramGeneracion["constante15B"];
            String constante22A;

            if (esCancelacion)
            {
                constante22A = paramGeneracion["constante22A-cancel"];
            } else
            {
                constante22A = paramGeneracion["constante22A-nuevo"];
            }


            bool esPrimerTag53 = true;
            bool esPrimerTag57 = true;

            foreach (LineaEditorMensajeSwift campoMT300 in lineasMensaje)
            {
                switch (campoMT300.CodCam)
                {

                    case "15A":
                        campoMT300.Detalle = constante15A;
                        campoMT300.Incluido = true;
                        break;

                    case "20":
                        campoMT300.Detalle = reg.reference;
                        campoMT300.Incluido = true;
                        break;

                    case "21":
                        if (esCancelacion)
                        {
                            campoMT300.Detalle = reg.reference;
                            campoMT300.Incluido = true;
                        }
                        break;

                    case "22A":
                        campoMT300.Detalle = constante22A;
                        campoMT300.Incluido = true;
                        reg.campo22A = constante22A;
                        break;

                    case "22C":
                        campoMT300.Detalle = (reg.campo22C == null) ? "" : reg.campo22C;
                        campoMT300.Incluido = true;
                        break;

                    case "82":
                        campoMT300.Detalle = reg.campo82A;
                        campoMT300.Incluido = true;
                        campoMT300.VarianteSeleccionada = "82A";
                        foreach (LineaSecundariaEditorMensajeSwift lineaSecundaria in campoMT300.LineasSecundarias)
                        {
                            if (lineaSecundaria.CodCam == "82A")
                            {
                                lineaSecundaria.Detalle = (reg.campo82A == null) ? "" : reg.campo82A;
                            }
                        }
                        break;
                    case "87":
                        //Aqui va el BIC del banco receptor, en el caso planilla es siempre el citi, desde la mesa puede variar
                        campoMT300.Detalle = reg.campo87A;
                        campoMT300.Incluido = true;
                        campoMT300.VarianteSeleccionada = "87A";
                        foreach (LineaSecundariaEditorMensajeSwift lineaSecundaria in campoMT300.LineasSecundarias)
                        {
                            if (lineaSecundaria.CodCam == "87A")
                            {
                                lineaSecundaria.Detalle = reg.campo87A;
                                reg.campo87A = reg.campo87A;
                            }
                        }
                        break;
                    case "15B":
                        campoMT300.Detalle = constante15B;
                        campoMT300.Incluido = true;
                        break;
                    case "30T":
                        campoMT300.Detalle = reg.booked_by.ToString(formatoFecha);
                        campoMT300.Incluido = true;
                        break;
                    case "30V":
                        campoMT300.Detalle = reg.value_date.ToString(formatoFecha);
                        campoMT300.Incluido = true;
                        break;
                    case "36":
                        campoMT300.Detalle = campoRateAString(reg.rate);
                        campoMT300.Incluido = true;
                        break;
                    case "32B":
                        if (campoMT300.Secuencia == "B")
                        {
                            campoMT300.Detalle = reg.codigo_moneda_mn + serviceEnvioSwift.ObtieneMontoFormatoSwift(reg.codigo_moneda_mn, reg.amount_mn);
                            campoMT300.Incluido = true;
                        }
                        break;
                    case "53":
                        if (campoMT300.Secuencia == "B")
                        {
                            campoMT300.Incluido = true;
                            campoMT300.VarianteSeleccionada = "53A";
                            foreach (LineaSecundariaEditorMensajeSwift lineaSecundaria in campoMT300.LineasSecundarias)
                            {
                                if (lineaSecundaria.CodCam == "53A" && lineaSecundaria.Linea == 1)
                                {
                                    if (esPrimerTag53)//bloque arriba de 33B
                                    {
                                        lineaSecundaria.Detalle = reg.campo53A;
                                    }
                                    else//bloque abajo de 33B
                                    {

                                        lineaSecundaria.Detalle = reg.campo57A;
                                    }
                                    esPrimerTag53 = false;
                                }
                            }
                        }
                        break;
                    case "57":
                        if (campoMT300.Secuencia == "B")
                        {
                            campoMT300.Incluido = true;
                            campoMT300.VarianteSeleccionada = "57A";
                            foreach (LineaSecundariaEditorMensajeSwift lineaSecundaria in campoMT300.LineasSecundarias)
                            {
                                if (lineaSecundaria.CodCam == "57A" && lineaSecundaria.Linea == 1)
                                {
                                    if (esPrimerTag57)//bloque arriba de 33B
                                    {
                                        lineaSecundaria.Detalle = reg.campo57A; ;
                                        reg.campo57A = reg.campo57A; ;

                                    }
                                    else//bloque abajo de 33B
                                    {
                                        lineaSecundaria.Detalle = reg.campo53A;
                                    }
                                    esPrimerTag57 = false;
                                }
                            }
                        }
                        break;
                    case "33B":
                        //nombre en tabla: amount_clp
                        if (campoMT300.Secuencia == "B")
                        {
                            campoMT300.Detalle = reg.codigo_moneda_me + serviceEnvioSwift.ObtieneMontoFormatoSwift(reg.codigo_moneda_me, reg.amount_me);
                            campoMT300.Incluido = true;
                        }
                        break;
                    case "98D":
                        campoMT300.Detalle = reg.campo98D;
                        campoMT300.Incluido = true;
                        break;
                }
            }

            return lineasMensaje;
        }

        public string ValidacionParametrosGeneracion(Dictionary<string, string> paramGeneracion, List<int> rutFirmantes, char modoEnvio, GenerarMT300DatosUser infousr)
        {

            string txtFaltantes = "";
            if (!paramGeneracion.ContainsKey("tipoMensajeMT") || String.IsNullOrEmpty(paramGeneracion["tipoMensajeMT"]))
                txtFaltantes += "[tipoMensajeMT] ";

            if (!paramGeneracion.ContainsKey("codMonedaCLP"))
                txtFaltantes += "[codMonedaCLP] ";

            if (!paramGeneracion.ContainsKey("formatoFecha"))
                txtFaltantes += "[formatoFecha] ";

            if (!paramGeneracion.ContainsKey("constante15A"))
                txtFaltantes += "[constante15A] ";

            if (!paramGeneracion.ContainsKey("constante22A-nuevo"))
                txtFaltantes += "[constante22A-nuevo] ";

            if (!paramGeneracion.ContainsKey("constante22C"))
                txtFaltantes += "[constante22C] ";

            if (!paramGeneracion.ContainsKey("constante82A"))
                txtFaltantes += "[constante82A] ";

            if (!paramGeneracion.ContainsKey("constante15B"))
                txtFaltantes += "[constante15B] ";

            if (!paramGeneracion.ContainsKey("constante53A-57A-bch"))
                txtFaltantes += "[constante53A-57A-bch] ";

            if (!paramGeneracion.ContainsKey("firma-tipo"))
                txtFaltantes += "[firma-tipo] ";

            if (!paramGeneracion.ContainsKey("firma-estado"))
                txtFaltantes += "[firma-estado] ";

            if (!paramGeneracion.ContainsKey("firma-revisa"))
                txtFaltantes += "[firma-revisa] ";

            if (!paramGeneracion.ContainsKey("firma-avisado"))
                txtFaltantes += "[firma-avisado] ";

            if (modoEnvio == modoAutomatico && rutFirmantes.Count <= 0)
                txtFaltantes += "[no se pudo obtener Responsables Firmas] ";

            if (infousr == null) {
                if (!paramGeneracion.ContainsKey("apiUsuarioNombreGSS"))
                    txtFaltantes += "[apiUsuarioNombreGSS] ";

                if (!paramGeneracion.ContainsKey("apiUsuarioRutGSS"))
                    txtFaltantes += "[apiUsuarioRutGSS] ";

                if (!paramGeneracion.ContainsKey("apiUsuarioCentroCostoGSS"))
                    txtFaltantes += "[apiUsuarioCentroCostoGSS] ";

                if (!paramGeneracion.ContainsKey("apiUsuarioNombreNoGSS"))
                    txtFaltantes += "[apiUsuarioNombreNoGSS] ";

                if (!paramGeneracion.ContainsKey("apiUsuarioRutNoGSS"))
                    txtFaltantes += "[apiUsuarioRutNoGSS] ";

                if (!paramGeneracion.ContainsKey("apiUsuarioCentroCostoNoGSS"))
                    txtFaltantes += "[apiUsuarioCentroCostoNoGSS] ";
            }

            return txtFaltantes;
        }

        public string campoRateAString(decimal campoRate) {
            string rate = campoRate.ToString("0.00000");
            return rate;
        }


    }

}