using System;
using System.Collections.Generic;
using BCH.Comex.Common.Tracing;

namespace BCH.Comex.Data.DAL.Services
{
    public static class XGPYServices
    {
        /// <summary>
        /// Servicio para obtener la razón social de un RUT
        /// </summary>
        /// <param name="rutPrty"></param>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public static String ConsultaRazonSocialPorRut(String rutPrty, ref string mensaje)
        {
            using (Tracer tracer = new Tracer("XGPYServices ConsultaRazonSocialPorRut"))
            {
                try
                {
                    tracer.AddToContext("Rut", rutPrty);
                    //Primero buscamos en el webService de persona
                    XCFT.ConsultaInformacionBasicaPersona.datosHeaderRequest datosHeaderRequest = new XCFT.ConsultaInformacionBasicaPersona.datosHeaderRequest();
                    datosHeaderRequest.consumidor = new XCFT.ConsultaInformacionBasicaPersona.datosConsumidor
                    {
                        idApp = "Citidocs1.0",
                        usuario = "EJB-COMEX",
                    };
                    datosHeaderRequest.transaccion = new XCFT.ConsultaInformacionBasicaPersona.datosTransaccion
                    {
                        internalCode = "1",
                        canal = "COMEX00001",
                        sucursal = "000",
                        idTransaccionNegocio = "1",
                        fechaHora = DateTime.Now,
                    };

                    XCFT.ConsultaInformacionBasicaPersona.datosEntrada reqConsultaTipoCliente = new XCFT.ConsultaInformacionBasicaPersona.datosEntrada();
                    reqConsultaTipoCliente.rutPersona = rutPrty;
                    reqConsultaTipoCliente.rutEmpresa = string.Empty;

                    XCFT.ConsultaInformacionBasicaPersona.datosHeaderResponse datosHeaderResponse;
                    XCFT.ConsultaInformacionBasicaPersona.datosSalida respConsultaInfoBasica;

                    XCFT.ConsultaInformacionBasicaPersona.ConsultaInformacionBasicaPersonaClient clientConsultaTipoCliente = new XCFT.ConsultaInformacionBasicaPersona.ConsultaInformacionBasicaPersonaClient();
                    datosHeaderResponse = clientConsultaTipoCliente.ConsultarInfoBasica(datosHeaderRequest, reqConsultaTipoCliente, out respConsultaInfoBasica);

                    return string.IsNullOrEmpty(respConsultaInfoBasica.razonSocial) ?
                            respConsultaInfoBasica.nombres + " " + respConsultaInfoBasica.apellidoPaterno + " " +
                            respConsultaInfoBasica.apellidoMaterno : respConsultaInfoBasica.razonSocial;

                }
                catch
                {
                    try
                    {
                        tracer.AddToContext("Fallo busqueda RUT natural", "Comienza busqueda RUT Empresa: " + rutPrty);
                        XCFT.ConsultaInformacionBasicaEmpresas.datosHeaderRequest datosHeaderRequest2 = new XCFT.ConsultaInformacionBasicaEmpresas.datosHeaderRequest();
                        datosHeaderRequest2.consumidor = new XCFT.ConsultaInformacionBasicaEmpresas.datosConsumidor
                        {
                            idApp = "Citidocs1.0",
                            usuario = "EJB-COMEX",
                        };
                        datosHeaderRequest2.transaccion = new XCFT.ConsultaInformacionBasicaEmpresas.datosTransaccion
                        {
                            internalCode = "1",
                            canal = "COMEX00001",
                            sucursal = "000",
                            idTransaccionNegocio = "1",
                            fechaHora = DateTime.Now,
                        };

                        XCFT.ConsultaInformacionBasicaEmpresas.datosEntrada reqConsultaEmpresa = new XCFT.ConsultaInformacionBasicaEmpresas.datosEntrada();
                        reqConsultaEmpresa.rutEmpresa = rutPrty;

                        XCFT.ConsultaInformacionBasicaEmpresas.datosHeaderResponse datosHeaderResponseEmpresa;
                        XCFT.ConsultaInformacionBasicaEmpresas.datosSalida respConsultaTipoClienteEmpresa;

                        XCFT.ConsultaInformacionBasicaEmpresas.ConsultaInformacionBasicaEmpresasClient clienteEmpresas = new XCFT.ConsultaInformacionBasicaEmpresas.ConsultaInformacionBasicaEmpresasClient();
                        datosHeaderResponseEmpresa = clienteEmpresas.ConsultarInfoBasica(datosHeaderRequest2, reqConsultaEmpresa, out respConsultaTipoClienteEmpresa);
                        return respConsultaTipoClienteEmpresa.razonSocial;
                    }
                    catch(Exception ex)
                    {
                        tracer.TraceException("Alerta WS ConsultaInformacionBasica", ex);
                        mensaje = ex.Message;
                        return null;
                    }
                }
            }
        }

        public static String ConsultaOficinaPorRut(String rutPrty)
        {
            using (Tracer tracer = new Tracer("XGPYServices ConsultaOficinaPorRut"))
            {
                try
                {
                    tracer.AddToContext("Rut", rutPrty);
                    XCFT.ObtenerDatosFichaChica.datosHeaderRequest datosHeaderRequest = new XCFT.ObtenerDatosFichaChica.datosHeaderRequest();


                    XCFT.ObtenerDatosFichaChica.reqObtener reqObtener = new XCFT.ObtenerDatosFichaChica.reqObtener();

                    XCFT.ObtenerDatosFichaChica.datosHeaderResponse datosHeaderResponse;
                    XCFT.ObtenerDatosFichaChica.respObtener respObtener;

                    reqObtener.Cuerpo = new XCFT.ObtenerDatosFichaChica.Cuerpo();

                    reqObtener.Cuerpo.rutCliente = rutPrty;

                    XCFT.ObtenerDatosFichaChica.ObtenerDatosFichaChicaClient clientObtenerDatosFichaChica = new XCFT.ObtenerDatosFichaChica.ObtenerDatosFichaChicaClient();
                    datosHeaderResponse = clientObtenerDatosFichaChica.Obtener(datosHeaderRequest, reqObtener, out respObtener);

                    return respObtener.Cuerpo.oficinaEjecutivo;
                }
                catch(Exception ex)
                {
                    tracer.TraceException("Alerta WS ObtenerDatosFichaChica", ex);
                    return null;
                }
            }
        }

        public static List<string> ConsultarDireccionPorRut(string rutPrty) 
        {
            using (Tracer tracer = new Tracer("XGPYServices ConsultarDireccionPorRut"))
            {
                var listaFinal = new List<string>();

                try
                {
                    tracer.AddToContext("Rut", rutPrty);
                    //Primero buscamos en el webService de persona
                    XCFT.ConsultaInformacionBasicaPersona.datosHeaderRequest datosHeaderRequest = new XCFT.ConsultaInformacionBasicaPersona.datosHeaderRequest();
                    datosHeaderRequest.consumidor = new XCFT.ConsultaInformacionBasicaPersona.datosConsumidor
                    {
                        idApp = "Citidocs1.0",
                        usuario = "EJB-COMEX",
                    };
                    datosHeaderRequest.transaccion = new XCFT.ConsultaInformacionBasicaPersona.datosTransaccion
                    {
                        internalCode = "1",
                        canal = "COMEX00001",
                        sucursal = "000",
                        idTransaccionNegocio = "1",
                        fechaHora = DateTime.Now,
                    };

                    XCFT.ConsultaInformacionBasicaPersona.datosEntrada reqConsultaTipoCliente = new XCFT.ConsultaInformacionBasicaPersona.datosEntrada();
                    reqConsultaTipoCliente.rutPersona = rutPrty;
                    reqConsultaTipoCliente.rutEmpresa = string.Empty;

                    XCFT.ConsultaInformacionBasicaPersona.datosHeaderResponse datosHeaderResponse;
                    XCFT.ConsultaInformacionBasicaPersona.datosSalida respConsultaInfoBasica;

                    XCFT.ConsultaInformacionBasicaPersona.ConsultaInformacionBasicaPersonaClient clientConsultaTipoCliente = new XCFT.ConsultaInformacionBasicaPersona.ConsultaInformacionBasicaPersonaClient();
                    datosHeaderResponse = clientConsultaTipoCliente.ConsultarInfoBasica(datosHeaderRequest, reqConsultaTipoCliente, out respConsultaInfoBasica);

                    var dataDireccion = respConsultaInfoBasica.listaDirecciones[0];
                    var dataTelefono = respConsultaInfoBasica.listaTelefonos[0];
                    var dataCorreo = respConsultaInfoBasica.listaCorreos[0];

                    listaFinal.Add(dataDireccion.direccion1 + " " + dataDireccion.numero);
                    listaFinal.Add(dataDireccion.comuna);
                    listaFinal.Add(dataDireccion.region);
                    listaFinal.Add(dataDireccion.ciudad);
                    listaFinal.Add(COD_CHILE.ToString());
                    listaFinal.Add(dataCorreo.email.email);
                    listaFinal.Add(dataTelefono.telefono.numero);
                    return listaFinal;
                }
                catch
                {
                    try
                    {
                        tracer.AddToContext("Fallo busqueda RUT natural", "Comienza busqueda RUT Empresa: " + rutPrty);
                        XCFT.ConsultaInformacionBasicaEmpresas.datosHeaderRequest datosHeaderRequest2 = new XCFT.ConsultaInformacionBasicaEmpresas.datosHeaderRequest();
                        datosHeaderRequest2.consumidor = new XCFT.ConsultaInformacionBasicaEmpresas.datosConsumidor
                        {
                            idApp = "Citidocs1.0",
                            usuario = "EJB-COMEX",
                        };
                        datosHeaderRequest2.transaccion = new XCFT.ConsultaInformacionBasicaEmpresas.datosTransaccion
                        {
                            internalCode = "1",
                            canal = "COMEX00001",
                            sucursal = "000",
                            idTransaccionNegocio = "1",
                            fechaHora = DateTime.Now,
                        };

                        XCFT.ConsultaInformacionBasicaEmpresas.datosEntrada reqConsultaEmpresa = new XCFT.ConsultaInformacionBasicaEmpresas.datosEntrada();
                        reqConsultaEmpresa.rutEmpresa = rutPrty;

                        XCFT.ConsultaInformacionBasicaEmpresas.datosHeaderResponse datosHeaderResponseEmpresa;
                        XCFT.ConsultaInformacionBasicaEmpresas.datosSalida respConsultaTipoClienteEmpresa;

                        XCFT.ConsultaInformacionBasicaEmpresas.ConsultaInformacionBasicaEmpresasClient clienteEmpresas = new XCFT.ConsultaInformacionBasicaEmpresas.ConsultaInformacionBasicaEmpresasClient();
                        datosHeaderResponseEmpresa = clienteEmpresas.ConsultarInfoBasica(datosHeaderRequest2, reqConsultaEmpresa, out respConsultaTipoClienteEmpresa);

                        var dataDireccion = respConsultaTipoClienteEmpresa.listaDirecciones[0];
                        var dataTelefono = respConsultaTipoClienteEmpresa.listaTelefonos[0];
                        var dataCorreo = respConsultaTipoClienteEmpresa.listaCorreos[0];

                        listaFinal.Add(dataDireccion.direccion1 + " " + dataDireccion.numero); //0
                        listaFinal.Add(dataDireccion.comuna); //1
                        listaFinal.Add(dataDireccion.region); //2
                        listaFinal.Add(dataDireccion.ciudad); //3
                        listaFinal.Add(COD_CHILE.ToString());
                        listaFinal.Add(dataCorreo.email.email); // 5
                        listaFinal.Add(dataTelefono.telefono.numero); // 6
                        return listaFinal;
                    }
                    catch(Exception ex)
                    {
                        tracer.TraceException("Alerta WS ConsultaInformacionBasica", ex);
                        return null;
                    }
                }
            }
        }

        public const int COD_CHILE = 997;

        public static BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos.T_PRTGLOB ObtenerDatosWebService (string rutPrty)
        {
            using (Tracer tracer = new Tracer("XGPYServices ObtenerDatosWebService"))
            {
                tracer.AddToContext("Rut", rutPrty);
                var dataItem = new Core.Entities.Cext01.AdminParticipantes.T_Modulos.T_PRTGLOB();
                dataItem.direc = new Core.Entities.Cext01.AdminParticipantes.T_Modulos.prtydireccion[1];
                dataItem.direc[0] = new Core.Entities.Cext01.AdminParticipantes.T_Modulos.prtydireccion();
                dataItem.ejecutivos = new Core.Entities.Cext01.AdminParticipantes.T_Modulos.tipo_riesgo[1];
                dataItem.ejecutivos[0] = new Core.Entities.Cext01.AdminParticipantes.T_Modulos.tipo_riesgo();

                try
                {
                    //Primero buscamos en el webService de persona
                    XCFT.ConsultaInformacionBasicaPersona.datosHeaderRequest datosHeaderRequest = new XCFT.ConsultaInformacionBasicaPersona.datosHeaderRequest();
                    datosHeaderRequest.consumidor = new XCFT.ConsultaInformacionBasicaPersona.datosConsumidor
                    {
                        idApp = "Citidocs1.0",
                        usuario = "EJB-COMEX",
                    };
                    datosHeaderRequest.transaccion = new XCFT.ConsultaInformacionBasicaPersona.datosTransaccion
                    {
                        internalCode = "1",
                        canal = "COMEX00001",
                        sucursal = "000",
                        idTransaccionNegocio = "1",
                        fechaHora = DateTime.Now,
                    };

                    XCFT.ConsultaInformacionBasicaPersona.datosEntrada reqConsultaTipoCliente = new XCFT.ConsultaInformacionBasicaPersona.datosEntrada();
                    reqConsultaTipoCliente.rutPersona = rutPrty;
                    reqConsultaTipoCliente.rutEmpresa = string.Empty;

                    XCFT.ConsultaInformacionBasicaPersona.datosHeaderResponse datosHeaderResponse;
                    XCFT.ConsultaInformacionBasicaPersona.datosSalida respConsultaInfoBasica;

                    XCFT.ConsultaInformacionBasicaPersona.ConsultaInformacionBasicaPersonaClient clientConsultaTipoCliente = new XCFT.ConsultaInformacionBasicaPersona.ConsultaInformacionBasicaPersonaClient();
                    datosHeaderResponse = clientConsultaTipoCliente.ConsultarInfoBasica(datosHeaderRequest, reqConsultaTipoCliente, out respConsultaInfoBasica);

                    var dataDireccion = respConsultaInfoBasica.listaDirecciones.Length > 0 ? respConsultaInfoBasica.listaDirecciones[0] : null;
                    var dataTelefono = respConsultaInfoBasica.listaTelefonos.Length > 0 ? respConsultaInfoBasica.listaTelefonos[0] : null;
                    var dataCorreo = respConsultaInfoBasica.listaCorreos.Length > 0 ? respConsultaInfoBasica.listaCorreos[0] : null;
                    var dataEjecutivo = respConsultaInfoBasica.listaEjecutivos.Length > 0 ? respConsultaInfoBasica.listaEjecutivos[0] : null;

                    if (dataDireccion != null)
                    {
                        dataItem.direc[0].direccion = dataDireccion.direccion1 + " " + dataDireccion.numero;
                        dataItem.direc[0].comuna = dataDireccion.comuna;
                        dataItem.direc[0].region = dataDireccion.region;
                        dataItem.direc[0].ciudad = dataDireccion.ciudad;
                    }

                    if (dataCorreo != null)
                    {
                        dataItem.direc[0].email = dataCorreo.email.email;
                    }

                    if (dataTelefono != null)
                    {
                        dataItem.direc[0].telefono = dataTelefono.telefono.numero;
                    }
                    dataItem.direc[0].CodPais = COD_CHILE;

                    if (dataEjecutivo != null)
                    {
                        dataItem.ejecutivos[0].codigo = dataEjecutivo.codPosition;
                        dataItem.ejecutivos[0].nombre = dataEjecutivo.paterno + " " + dataEjecutivo.materno + " " + dataEjecutivo.nombres;
                        dataItem.Party.oficina = dataEjecutivo.oficina;
                    }

                    return dataItem;
                }
                catch
                {
                    try
                    {
                        tracer.AddToContext("Fallo busqueda RUT natural","Comienza busqueda RUT Empresa: " + rutPrty);
                        XCFT.ConsultaInformacionBasicaEmpresas.datosHeaderRequest datosHeaderRequest2 = new XCFT.ConsultaInformacionBasicaEmpresas.datosHeaderRequest();
                        datosHeaderRequest2.consumidor = new XCFT.ConsultaInformacionBasicaEmpresas.datosConsumidor
                        {
                            idApp = "Citidocs1.0",
                            usuario = "EJB-COMEX",
                        };
                        datosHeaderRequest2.transaccion = new XCFT.ConsultaInformacionBasicaEmpresas.datosTransaccion
                        {
                            internalCode = "1",
                            canal = "COMEX00001",
                            sucursal = "000",
                            idTransaccionNegocio = "1",
                            fechaHora = DateTime.Now,
                        };

                        XCFT.ConsultaInformacionBasicaEmpresas.datosEntrada reqConsultaEmpresa = new XCFT.ConsultaInformacionBasicaEmpresas.datosEntrada();
                        reqConsultaEmpresa.rutEmpresa = rutPrty;

                        XCFT.ConsultaInformacionBasicaEmpresas.datosHeaderResponse datosHeaderResponseEmpresa;
                        XCFT.ConsultaInformacionBasicaEmpresas.datosSalida respConsultaTipoClienteEmpresa;

                        XCFT.ConsultaInformacionBasicaEmpresas.ConsultaInformacionBasicaEmpresasClient clienteEmpresas = new XCFT.ConsultaInformacionBasicaEmpresas.ConsultaInformacionBasicaEmpresasClient();
                        datosHeaderResponseEmpresa = clienteEmpresas.ConsultarInfoBasica(datosHeaderRequest2, reqConsultaEmpresa, out respConsultaTipoClienteEmpresa);

                        var dataDireccion = respConsultaTipoClienteEmpresa.listaDirecciones.Length > 0 ? respConsultaTipoClienteEmpresa.listaDirecciones[0] : null;
                        var dataTelefono = respConsultaTipoClienteEmpresa.listaTelefonos.Length > 0 ? respConsultaTipoClienteEmpresa.listaTelefonos[0] : null;
                        var dataCorreo = respConsultaTipoClienteEmpresa.listaCorreos.Length > 0 ? respConsultaTipoClienteEmpresa.listaCorreos[0] : null;

                        if (dataDireccion != null)
                        {
                            dataItem.direc[0].direccion = dataDireccion.direccion1 + " " + dataDireccion.numero;
                            dataItem.direc[0].comuna = dataDireccion.comuna;
                            dataItem.direc[0].region = dataDireccion.region;
                            dataItem.direc[0].ciudad = dataDireccion.ciudad;
                        }

                        if (dataCorreo != null)
                        {
                            dataItem.direc[0].email = dataCorreo.email.email;
                        }

                        if (dataTelefono != null)
                        {
                            dataItem.direc[0].telefono = dataTelefono.telefono.numero;
                        }
                        dataItem.direc[0].CodPais = COD_CHILE;
                        return dataItem;
                    }
                    catch(Exception ex)
                    {
                        tracer.TraceException("Alerta WS ConsultaInformacionBasica", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Servicio para consultar las cuentas de un cliente por el RUT
        /// </summary>
        /// <param name="rutPrty"></param>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public static IList<XCFT.ConsultaProductoCliente.producto> ObtenerCuentasPorRut(String rutPrty, ref string mensaje)
        {
            using (Tracer tracer = new Tracer("XGPYServices ObtenerCuentasPorRut"))
            {
                try
                {
                    tracer.AddToContext("Rut", rutPrty);
                    XCFT.ConsultaProductoCliente.datosHeaderRequest datosHeaderRequest = new XCFT.ConsultaProductoCliente.datosHeaderRequest()
                    {
                        consumidor = new XCFT.ConsultaProductoCliente.datosConsumidor
                        {
                            idApp = "Citidocs1.0",
                            usuario = "EJB-COMEX",
                        },
                        transaccion = new XCFT.ConsultaProductoCliente.datosTransaccion
                        {
                            internalCode = "1",
                            canal = "COMEX00001",
                            sucursal = "000",
                            idTransaccionNegocio = "1",
                            fechaHora = DateTime.Now,
                        },
                    }; ;
                    XCFT.ConsultaProductoCliente.datosEntrada datosEntrada = new XCFT.ConsultaProductoCliente.datosEntrada();

                    XCFT.ConsultaProductoCliente.datosHeaderResponse datosHeaderResponse;
                    XCFT.ConsultaProductoCliente.datosSalida datosSalida;

                    datosEntrada.rutCliente = rutPrty;
                    datosEntrada.listaProductos = new List<string>().ToArray();

                    XCFT.ConsultaProductoCliente.ConsultaProductoClienteClient clientConsultaProductoCliente = new XCFT.ConsultaProductoCliente.ConsultaProductoClienteClient();
                    datosHeaderResponse = clientConsultaProductoCliente.ConsultarProdCliente(datosHeaderRequest, datosEntrada, out datosSalida);

                    return datosSalida.listaProductos;
                }
                catch(Exception ex)
                {
                    tracer.TraceException("Alerta WS ConsultaProductoCliente", ex);
                    mensaje = ex.Message;
                    return null;
                }
            }
        }

        public static IList<string> ObtenerDatosFichaChica(String rutPrty)
        {
            using (Tracer tracer = new Tracer("XGPYServices ObtenerDatosFichaChica"))
            {
                try
                {
                    tracer.TraceInformation("XGPYServices ObtenerDatosFichaChica Rut: " + rutPrty);
                    XCFT.ObtenerDatosFichaChica.datosHeaderRequest datosHeaderRequest = new XCFT.ObtenerDatosFichaChica.datosHeaderRequest();


                    XCFT.ObtenerDatosFichaChica.reqObtener reqObtener = new XCFT.ObtenerDatosFichaChica.reqObtener();

                    XCFT.ObtenerDatosFichaChica.datosHeaderResponse datosHeaderResponse;
                    XCFT.ObtenerDatosFichaChica.respObtener respObtener;

                    reqObtener.Cuerpo = new XCFT.ObtenerDatosFichaChica.Cuerpo();

                    reqObtener.Cuerpo.rutCliente = rutPrty;

                    XCFT.ObtenerDatosFichaChica.ObtenerDatosFichaChicaClient clientObtenerDatosFichaChica = new XCFT.ObtenerDatosFichaChica.ObtenerDatosFichaChicaClient();
                    datosHeaderResponse = clientObtenerDatosFichaChica.Obtener(datosHeaderRequest, reqObtener, out respObtener);

                    List<string> resp = null;

                    if (respObtener.Cuerpo != null)
                    {
                        resp = new List<string>();
                        resp.Add(respObtener.Cuerpo.oficinaEjecutivo);
                        resp.Add(respObtener.Cuerpo.codigoEjecutivo);
                        resp.Add(respObtener.Cuerpo.codigoActividadEconomica);
                        resp.Add(respObtener.Cuerpo.clasificacionRiesgo);
                    }

                    return resp; 
                }
                catch(Exception ex)
                {
                    tracer.TraceException("Alerta WS ObtenerDatosFichaChica", ex);
                    return null;
                }
            }
        }

        //INICIO MODIFICACION CNC - ACCENTURE 
        public static IList<string> ObtenerDatosClasificacionCliente(String rutPrty, String credU, String credP, String kDec)
        {
            String espacio_vacio = " ";
            List<string> ListadoClasificacion = new List<string>();

            using (Tracer tracer = new Tracer("XGPYServices ObtenerDatosClasificacionCliente"))
            {
                try
                {
                    tracer.TraceInformation("XGPYServices ObtenerDatosClasificacionCliente Rut: " + rutPrty);

                    // creamos los objetos
                    CTR.ConsultarFichaClienteFull.consultarFichaClienteFullRequest _consultarFichaClienteFullRequest = new CTR.ConsultarFichaClienteFull.consultarFichaClienteFullRequest();

                    var _HeaderRequest = new CTR.ConsultarFichaClienteFull.HeaderRequest();
                    var _DatosConsumidor = new CTR.ConsultarFichaClienteFull.DatosConsumidor();
                    var _DatosTransaccion = new CTR.ConsultarFichaClienteFull.DatosTransaccion();
                    var _consultarFichaClienteFullRqType = new CTR.ConsultarFichaClienteFull.consultarFichaClienteFullRqType();

                    // se añade a lista de respuesta

                    try
                    {
                        // datos consumidor
                        #region Sección HEADER
                        _DatosConsumidor.usuario = "1";
                        _DatosConsumidor.pathServices = "1";
                        // datos transacción
                        _DatosTransaccion.idTransaccionNegocio = "1";
                        _DatosTransaccion.internalCode = "1";
                        _DatosTransaccion.fechaHora = DateTime.Now;
                        _DatosTransaccion.idCanal = "NIE";
                        _DatosTransaccion.numeroSucursal = "000";

                        _HeaderRequest.datosConsumidor = _DatosConsumidor;
                        _HeaderRequest.datosTransaccion = _DatosTransaccion;
                        #endregion

                        #region Sección BODY
                        // agregamos el rut enviado
                        _consultarFichaClienteFullRqType.rutCliente = rutPrty;
                        #endregion

                        #region Invocación BUS + Preparación Password Digest

                        CTR.ConsultarFichaClienteFull.consultarFichaClienteFullResponse _consultarFichaClienteFullResponse = new CTR.ConsultarFichaClienteFull.consultarFichaClienteFullResponse();
                        CTR.ConsultarFichaClienteFull.consultarFichaClienteFullPortClient _consultarFichaClienteFullPortClient = new CTR.ConsultarFichaClienteFull.consultarFichaClienteFullPortClient();
                        CTR.ConsultarFichaClienteFull.HeaderResponse _HeaderResponse = new CTR.ConsultarFichaClienteFull.HeaderResponse();

                        string DecU = Aes256CbcEncrypter.Decrypt(credU, kDec);
                        string DecP = Aes256CbcEncrypter.Decrypt(credP, kDec);

                        var behavior = new consultarFichaClienteFull.PasswordDigestBehavior(DecU, DecP);

                        _consultarFichaClienteFullPortClient.Endpoint.EndpointBehaviors.Add(behavior);

                        _consultarFichaClienteFullRequest.consultarFichaClienteFullRq = _consultarFichaClienteFullRqType;
                        _consultarFichaClienteFullRequest.headerRequest = _HeaderRequest;

                        // llamamos al servicio   
                        _consultarFichaClienteFullPortClient.Open();
                        _consultarFichaClienteFullPortClient.consultarFichaClienteFull(_HeaderRequest, _consultarFichaClienteFullRqType, out _consultarFichaClienteFullResponse.consultarFichaClienteFullRs);
                        _consultarFichaClienteFullPortClient.Close();

                        #endregion

                        var FichaClienteFullRs = _consultarFichaClienteFullResponse.consultarFichaClienteFullRs;

                        // guardamos el resultado

                        if (FichaClienteFullRs != null)
                        {

                            if (FichaClienteFullRs.respuesta == null)
                            {
                                ListadoClasificacion.Add(espacio_vacio);
                                ListadoClasificacion.Add(espacio_vacio);
                                ListadoClasificacion.Add(espacio_vacio);
                                ListadoClasificacion.Add(espacio_vacio);
                                ListadoClasificacion.Add(espacio_vacio);
                                ListadoClasificacion.Add(espacio_vacio);
                            }
                            else
                            {
                                if (FichaClienteFullRs.respuesta.clasificacionRiesgo != null)
                                {
                                    ListadoClasificacion.Add(FichaClienteFullRs.respuesta.clasificacionRiesgo);
                                }
                                else
                                {
                                    ListadoClasificacion.Add(espacio_vacio);
                                }
                                if (FichaClienteFullRs.respuesta.actividadEconomica != null)
                                {
                                    ListadoClasificacion.Add(FichaClienteFullRs.respuesta.actividadEconomica);
                                }
                                else
                                {
                                    ListadoClasificacion.Add(espacio_vacio);
                                }
                                if (FichaClienteFullRs.respuesta.codigoActividadEconomica != null)
                                {
                                    ListadoClasificacion.Add(FichaClienteFullRs.respuesta.codigoActividadEconomica);
                                }
                                else
                                {
                                    ListadoClasificacion.Add(espacio_vacio);
                                }
                                if (FichaClienteFullRs.respuesta.evaluacionRiesgo != null)
                                {
                                    ListadoClasificacion.Add(FichaClienteFullRs.respuesta.evaluacionRiesgo);
                                }
                                else
                                {
                                    ListadoClasificacion.Add(espacio_vacio);
                                }
                                if (FichaClienteFullRs.respuesta.composicionInstitucional != null)
                                {
                                    ListadoClasificacion.Add(FichaClienteFullRs.respuesta.composicionInstitucional);
                                }
                                else
                                {
                                    ListadoClasificacion.Add(espacio_vacio);
                                }
                                if (FichaClienteFullRs.respuesta.tipoClienteNormativo != null)
                                {
                                    ListadoClasificacion.Add(FichaClienteFullRs.respuesta.tipoClienteNormativo);
                                }
                                else
                                {
                                    ListadoClasificacion.Add(espacio_vacio);
                                }
                            }
                        }
                        else
                        {
                            ListadoClasificacion.Add(espacio_vacio);
                            ListadoClasificacion.Add(espacio_vacio);
                            ListadoClasificacion.Add(espacio_vacio);
                            ListadoClasificacion.Add(espacio_vacio);
                            ListadoClasificacion.Add(espacio_vacio);
                            ListadoClasificacion.Add(espacio_vacio);

                        }

                    }
                    catch (Exception ex)
                    {
                        string excepcion = ex.ToString();
                        return null;
                    }

                    // retornamos las oficinas
                    return ListadoClasificacion;
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta WS ObtenerDatosClasificacionCliente", ex);
                    return null;
                }
            }

        }
        //FIN MODIFICACION CNC - ACCENTURE 

    }
}
