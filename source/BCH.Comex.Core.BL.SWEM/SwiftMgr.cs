using BCH.Comex.Core.BL.SWI102;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Swift;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;

using BCH.Comex.Common.Tracing;

namespace BCH.Comex.Core.BL.SWEM
{
    public class SwiftMgr
    {
        private UnitOfWorkSwift uow;

        
        public SwiftMgr()
        {
            uow = new UnitOfWorkSwift();
        }

        public IList<sw_casillas> GetTodasLasCasillas()
        {
            return uow.CasillaRepository.Get(orderBy: ( q => q.OrderBy(c => c.cod_casilla )));
        }

        public IList<proc_trae_tipo_campos_MS_Result> GetTipoCamposConMaximo()
        {
            return uow.TipoCampoRepository.GetTipoCamposConMaximo();
        }

        public IList<sw_campos_msg> GetCampos()
        {
            return uow.CamposMsgRepository.GetFormatoCamposSWEM();
        }
        
        public IList<ResultadoBusquedaSwift> BuscarSwiftsPorCasillaYFechas(int? idCasilla, DateTime? fechaDesde, DateTime? fechaHasta, bool enviados, int? rutSoloPropios, out int totalCount, int? rowOffset, short? fetchRows, string searchText, EstadoSwiftEnviado estado = EstadoSwiftEnviado.Enviado)
        {
            
            using (Tracer tracer = new Tracer()) 
            {
                List<ResultadoBusquedaSwift> results = null;
                totalCount = 0;
                try
                {
                    if (enviados)
                    {
                        results = uow.MensajeRepository.BuscarSwiftsEnviadosPorCasillaYFechas(idCasilla, fechaDesde, 
                            fechaHasta, rutSoloPropios, out totalCount, rowOffset, fetchRows, searchText, estado).ToList();
                    }
                    else
                    {
                        results = uow.MensajeRepository.BuscarSwiftsRecibidosPorCasillaYFechas(idCasilla.Value, fechaDesde, fechaHasta, 
                            rutSoloPropios, out totalCount, rowOffset, fetchRows, searchText).ToList();
                    }

                    if (results != null && enviados && estado != EstadoSwiftEnviado.Enviado)
                    {
                        if (rutSoloPropios.HasValue && estado != EstadoSwiftEnviado.SinSolicitudFirmas)
                        {
                            //los sps de enviado salvo el de estado ENV y Sin Solciitud de Firma no reciben el rut como parametro, se debe filtrar en memoria una vez traido todo de la bd
                            results = results.Where(r => r.rut_ingreso == rutSoloPropios.Value).ToList();
                        }

                        if(!String.IsNullOrEmpty(searchText))
                        {
                            //todos los sps que no son para estado ENV no paginan y por lo tanto no tendremos problema en hacer los filtros en memoria una vez que ya obtuvimos los datos de la BD
                            //si fuera ineficiente habría que reemplazar los sps pero son 8 (uno por cada estado) por lo que esta solucion tiene menor impacto
                            results = results.Where(r => 
                                r.id_mensaje.ToString().Contains(searchText) || 
                                r.sesion.ToString().Contains(searchText) || 
                                r.secuencia.ToString().Contains(searchText) ||
                                r.casilla.ToString() == searchText ||
                                r.tipo_msg == searchText ||
                                (r.referencia != null && r.referencia.Contains(searchText)) ||
                                (r.beneficiario != null && r.beneficiario.Contains(searchText)) ||
                                r.cod_moneda == searchText ||
                                r.monto.ToString().Contains(searchText) ||
                                (r.cod_banco_em.Trim() + r.branch_em.Trim()).Contains(searchText) ||
                                (r.cod_banco_rec.Trim() + r.branch_rec.Trim()).Contains(searchText) 
                                ).ToList(); 
                        }

                        totalCount = results.Count;
                    }
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, problemas al Buscar swift por casilla y fechas ",ex);
                }
                return results;
            }
            
        }

        public ResultadoBusquedaSwift CargarMensajeSwiftRecibido(int sesion, int secuencia, IList<proc_trae_tipo_campos_MS_Result> tipoCamposConMaximo, IList<sw_campos_msg> campos)
        {
            ResultadoBusquedaSwift swift = uow.MensajeRepository.GetSwiftRecibido(sesion, secuencia);
            return CargarMensajeSwift(swift, tipoCamposConMaximo, campos, true);
        }

        public ResultadoBusquedaSwift CargarMensajeSwiftEnviado(int idMensaje, IList<proc_trae_tipo_campos_MS_Result> tipoCamposConMaximo, IList<sw_campos_msg> campos)
        {
            ResultadoBusquedaSwift swift = uow.MensajeRepository.GetSwiftEnviado(idMensaje);
            return CargarMensajeSwift(swift, tipoCamposConMaximo, campos, false);
        }

        public IList<EstadisticaCasillla> GetEstadisticasMensajesPorCasilla(int idCasilla, DateTime fechaDesde, DateTime fechaHasta, bool enviados)
        {
            if (enviados)
            {
                return uow.MensajeRepository.GetEstadisticaMsgPorCasilla(idCasilla, fechaDesde, fechaHasta, MensajeRepository.Direccion.Enviado);
            }
            else
            {
                return uow.MensajeRepository.GetEstadisticaMsgPorCasilla(idCasilla, fechaDesde, fechaHasta, MensajeRepository.Direccion.Recibido);
            }
        }

        public proc_sw_env_trae_datos_msg_MS_Result GetDatosMensajeEnviado(int idMensaje)
        {
            return uow.MensajeRepository.GetDatosMensajeEnviado(idMensaje);
        }

        public proc_sw_rec_trae_datos_msg_MS_Result GetDatosMensajeRecibido(int sesion, int secuencia)
        {
            return uow.MensajeRepository.GetDatosMensajeRecibido(sesion, secuencia);
        }


        public bool SetFirmasAnuladasAnteriores(int idmensaje, int rut, DateTime p_fecha_solic)
        {
            return uow.FirmaRepository.proc_sw_env_del_firnul_MS(idmensaje, rut, p_fecha_solic);
        }

        public bool EliminarFirmas(int idMensaje, int rut)
        {
            return uow.FirmaRepository.proc_sw_env_del_firma_MS(idMensaje,rut);
        }

        public IList<sw_msgsend_firma> GetFirmasDeMensajeEnviado(int idMensaje, bool noNuevas = true)
        {
            IList<sw_msgsend_firma>  firmas = uow.FirmaRepository.Get(f => f.id_mensaje == idMensaje)
                .OrderBy(f => f.fecha_solic)
                .ThenBy(f => f.fecha_firma).ThenByDescending(f => f.revisa_firma).ToList();

            if(noNuevas)
            {
                firmas = firmas.Where(f => f.estado_firma != "N").ToList();
            }

            List<int> rutsDeInteres = new List<int>();
            rutsDeInteres.AddRange(firmas.Select(f => f.rut_solic).ToList());
            rutsDeInteres.AddRange(firmas.Select(f => f.rut_firma).ToList());

            IList<sce_usr> usuariosFirmas = GetUsuariosDeCextEnBaseARutsDeSwift(rutsDeInteres.ToArray());

            foreach (sw_msgsend_firma firma in firmas)
            {
                sce_usr usr = BuscarUsuarioCextComparandoConRutSwift(usuariosFirmas, firma.rut_solic);
                if (usr != null)
                {
                    firma.NombrePersonaSolicita = usr.nombre;
                }

                usr = BuscarUsuarioCextComparandoConRutSwift(usuariosFirmas, firma.rut_firma);
                if (usr != null)
                {
                    firma.NombrePersonaFirma = usr.nombre;
                    firma.RutFirmaConDigitoVerificador = usr.rut;
                }

                
            }
            
            
            return firmas;
        }

        public void ActualizaContadorImpresionMensajeRecibido(int sesion, int secuencia)
        {
            using (Tracer tracer = new Tracer("SwiftMgr - ActualizaContadorImpresionMensajeRecibido"))
            {
                try
                {
                    uow.SwRepository.sw_mensajes_u01_MS(sesion, secuencia);
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, problema al actualizar contador de impresion", ex);
                    throw;
                }
            }
        }

        public bool SaveFirma(int idmensaje, int rut, string p_tipo_firma, string p_estado, string p_revfir, int rut_solic, DateTime fecha_solic, string p_avisado)
        {
            return uow.AdministracionRepository.proc_sw_env_ing_firma(idmensaje, rut, p_tipo_firma, p_estado, p_revfir, rut_solic, fecha_solic.ToString("yyyy-MM-dd HH:mm:ss"), p_avisado);
        }

        public bool SaveFirmas(int idMensaje, IList<sw_msgsend_firma> firmas, int rutUsuarioSolicita)
        {
            bool todosOK = true;

            using (Tracer tracer = new Tracer()) {
                try
                {
                    foreach (sw_msgsend_firma firma in firmas)
                    {
                        int rutSolicitaCorresponde = rutUsuarioSolicita;
                        if (firma.estado_firma != "N")
                        {
                            rutSolicitaCorresponde = firma.rut_solic;  //si el mensaje es nuevo entonces paso el rut del usuario logueado, pero si estoy editando tengo que pasar el rut del usuario que solicitó originalmente, sino el SP no actualiza.
                        }

                        bool ok = SaveFirma(idMensaje, firma.rut_firma, firma.tipo_firma, firma.estado_firma, firma.revisa_firma, rutSolicitaCorresponde, DateTime.Now, "S");
                        if (!ok)
                        {
                            todosOK = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, problemas al grabar las firmas",ex);
                    return false;
                }
            }

            return todosOK;
        }

        
        public IList<sw_msgsend_firma> GetAtributosPersona(int rut)
        {
            IList<sw_msgsend_firma> usuario = uow.FirmaRepository.Get(f => f.rut_firma == rut).ToList();

            return usuario;
        }

        public IList<sce_usr> GetUsuariosPorNombre(string nombre)
        {
            using (UnitOfWorkCext01 uowCext = new UnitOfWorkCext01())
            {
                string nombreLower = nombre.ToLower();
                IList<sce_usr> Usuarios = uowCext.UsuarioRepository.Get(f => f.nombre.ToLower().Contains(nombreLower))
                 .OrderBy(f => f.nombre).ToList();

                return Usuarios;
            }
        }

        public IList<proc_sw_log_trae_msg_MS_Result> GetLogDeMensajeRecibido(int sesion, int secuencia)
        {
            IList<proc_sw_log_trae_msg_MS_Result> logs = uow.MensajeRepository.GetLogDeMensajeRecibido(sesion, secuencia);
            ComplementarLogsConNombresPersonas(logs);
            return logs;
        }

        public IList<proc_sw_log_trae_msg_MS_Result> GetLogDeMensajeEnviado(int idMensaje)
        {
            IList<proc_sw_log_trae_msg_MS_Result> logs = uow.MensajeRepository.GetLogDeMensajeEnviado(idMensaje);
            ComplementarLogsConNombresPersonas(logs);
            return logs;
        }

        private ResultadoBusquedaSwift CargarMensajeSwift(ResultadoBusquedaSwift swift, IList<proc_trae_tipo_campos_MS_Result> tipoCamposConMaximo, IList<sw_campos_msg> campos, bool recibido)
        {
            if (swift != null)
            {
                try
                {
                    MensajeSwiftService service = new MensajeSwiftService();
                    if (recibido)
                    {
                        swift.DetalleRaw = service.DesencriptaMensajeRecibido(swift.sesion, swift.secuencia);
                        FormatUtils.CargarDatosFijosSwift(swift.DetalleRaw, swift);
                    }
                    else
                    {
                        swift.DetalleRaw = service.DesencriptaMensajeEnviado(swift.id_mensaje);
                    }
                    swift.LineasDetalle = GetDetallesMensajeSwift(swift, tipoCamposConMaximo, campos, recibido);
                }
                catch (Exception ex)
                {

                }

                return swift;
            }
            else
            {
                return null;
            }
        }
        
        //Este metodo en version VB6 se llama ArmaMensajeRec y ArmaEnvioMensaje segun si es swift recibido o enviado, aquí se unifican
        private List<LineaDetalleMensajeSwift> GetDetallesMensajeSwift(ResultadoBusquedaSwift swift, IList<proc_trae_tipo_campos_MS_Result> tipoCamposConMaximo, IList<sw_campos_msg> campos, bool recibido)
        {
            List<LineaDetalleMensajeSwift> resultado = new List<LineaDetalleMensajeSwift>();
            string pvarMensaje = swift.DetalleRaw;
            
            string separadorSeccionInteresa = "{4:";
            short intLargo = 0;
            int intPos = 0;
            
            string pvarTipoMsg = (String.IsNullOrEmpty(swift.tipo_msg) ? String.Empty: swift.tipo_msg.Trim());
            string pvarMoneda = (String.IsNullOrEmpty(swift.cod_moneda) ? String.Empty: swift.cod_moneda.Trim());

            string[] parts = pvarMensaje.Split(new string[] { separadorSeccionInteresa }, StringSplitOptions.RemoveEmptyEntries);

            string seccionInteresa = String.Empty;

            if (parts.Length == 2)
            {
                seccionInteresa = parts[1];
            }
            else if (parts.Length == 3)
            {
                seccionInteresa = parts[2];
            }

            string[] lineas = null;
            if (recibido)
            {
                lineas = seccionInteresa.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                lineas = seccionInteresa.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            }

            char caracterLineaCampoNuevo = ':';
            char caracterLineaContinuacionCampo = '/';

            string strFormato = String.Empty;


            //recorro todas las lineas menos la última que no me interesa
            for (int indiceLinea = 0; indiceLinea < lineas.Length - 1; indiceLinea++) 
            {
                string linea = lineas[indiceLinea].Trim();
                char primerCaracterLinea = linea[0];

                string codigoCampo = String.Empty;
                string nombreCampo = String.Empty;
                
                LineaDetalleMensajeSwift lineaDetalle = new LineaDetalleMensajeSwift();

                //es un campo nuevo o es la continuación de un campo anterior? hay campos que ocupan varias líneas
                if (primerCaracterLinea == caracterLineaCampoNuevo)
                {
                    //campo nuevo
                    string[] partesNuevaLinea = linea.Split(new char[] { caracterLineaCampoNuevo }, StringSplitOptions.RemoveEmptyEntries);
                    codigoCampo = partesNuevaLinea[0];
                    lineaDetalle.Codigo = codigoCampo;
                    lineaDetalle.EsNuevaLineaDeCampo = true;
                    strFormato = String.Empty;

                    if (partesNuevaLinea.Length == 2)
                    {
                        lineaDetalle.EsNuevaLineaDeCampo = true;
                        lineaDetalle.Detalle = partesNuevaLinea[1];
                    }
                    else
                    {
                        lineaDetalle.Detalle = string.Join(caracterLineaCampoNuevo.ToString(),  partesNuevaLinea.Skip(1).ToArray()); //me salteo el primer elemento que es el codigo, y vuelvo a unir todo el resto
                    }

                    //como es campo nuevo, voy a ver si tengo la descripción en la BD
                    var tipoCampo = tipoCamposConMaximo.Where(tc => tc.tipo_msg_tipcam == pvarTipoMsg & tc.tag_campo_tipcam == codigoCampo).FirstOrDefault();
                    if (tipoCampo == null)
                    {
                        tipoCampo = tipoCamposConMaximo.Where(tc => tc.tipo_msg_tipcam == "MT000" & tc.tag_campo_tipcam == codigoCampo).FirstOrDefault();
                        if (tipoCampo == null)
                        {
                            nombreCampo = "Campo Desconocido";
                            intLargo = 0;
                        }
                    }

                    if (tipoCampo != null)
                    {
                        nombreCampo = tipoCampo.nombre_campo_tipcam;
                        intLargo = (short)tipoCampo.max_largo.Value;
                    }

                    lineaDetalle.Descripcion = nombreCampo;

                    //además de la descripcón, ahora voy a ver si tengo el formato en la BD
                    sw_campos_msg campo = campos.Where(c => c.tag_campo.Substring(0, c.tag_campo.Length) == codigoCampo).FirstOrDefault();
                    if (campo != null)
                    {
                        strFormato = campo.formato_campo;
                        intPos = campo.linea_campo;
                    }

                    ProcesarLinea(lineaDetalle, resultado, strFormato, pvarMoneda);
                }
                else
                {
                    //no es campo nuevo, debería ser un campo multilínea
                    string[] partesCampoMultilinea = linea.Split(new char[] { caracterLineaContinuacionCampo }, StringSplitOptions.RemoveEmptyEntries);
                    lineaDetalle.Detalle = linea;
                    ProcesarLinea(lineaDetalle, resultado, strFormato, pvarMoneda);
                }
            }
            
            return resultado;
        }

        private void ProcesarLinea(LineaDetalleMensajeSwift lineaDetalle, List<LineaDetalleMensajeSwift> resultado, string strFormato, string codMoneda)
        {
            //es el campo banco?
            if (!String.IsNullOrEmpty(strFormato) && strFormato.Substring(0, 3) == "11T")
            {
                sw_bancos banco = BuscaDatosBanco(lineaDetalle.Detalle);
                if (banco == null)
                {
                    resultado.Add(lineaDetalle);
                }
                else
                {
                    lineaDetalle.Detalle = banco.cod_banco.Trim() + banco.branch.Trim();
                    resultado.Add(lineaDetalle);

                    if (!String.IsNullOrEmpty(banco.nombre_banco))
                    {
                        resultado.Add(new LineaDetalleMensajeSwift()
                        {
                            //Codigo = "-",
                            Detalle = banco.nombre_banco
                        });
                    }

                    if (!String.IsNullOrEmpty(banco.oficina_banco))
                    {
                        resultado.Add(new LineaDetalleMensajeSwift()
                        {
                            //Codigo = "-",
                            Detalle = banco.oficina_banco
                        });
                    }

                    if (!String.IsNullOrEmpty(banco.ciudad_banco))
                    {
                        resultado.Add(new LineaDetalleMensajeSwift()
                        {
                            //Codigo = "-",
                            Detalle = banco.ciudad_banco
                        });
                    }

                    if (!String.IsNullOrEmpty(banco.pais_banco))
                    {
                        resultado.Add(new LineaDetalleMensajeSwift()
                        {
                            //Codigo = "-",
                            Detalle = banco.pais_banco
                        });
                    }
                }
            }
            else
            {
                lineaDetalle.Detalle = FormatearDetalle(codMoneda, strFormato, lineaDetalle.Detalle);
                resultado.Add(lineaDetalle);
            }
        }

        private static string FormatearDetalle(string pvarMoneda, string strFormato, string detalle)
        {
            string detConFormato = detalle;
            if (!String.IsNullOrEmpty(strFormato)) //voy a ver si es algún formato que reconozca
            {
                try
                {
                    if (strFormato.StartsWith("01A 03D 02A 03C 15N"))
                    {
                        detConFormato = VB6Helpers.Mid(detalle, 1, 1) + " ";
                        detConFormato = detConFormato + VB6Helpers.Mid(detalle, 2, 3) + " ";
                        detConFormato = detConFormato + VB6Helpers.Mid(detalle, 5, 2) + " ";
                        detConFormato += FormatUtils.FormatMontoUSD(pvarMoneda, VB6Helpers.Mid(detalle, 7));
                    }
                    else if (strFormato.StartsWith("01A 06G 03C"))
                    {
                        detConFormato = VB6Helpers.Mid(detalle, 1, 1) + " ";
                        detConFormato = detConFormato + FormatUtils.FormaFecha(VB6Helpers.Mid(detalle, 2, 6)) + " ";
                        detConFormato += FormatUtils.FormatMontoUSD(pvarMoneda, VB6Helpers.Mid(detalle, 8));
                    }
                    else if (strFormato.StartsWith("05D 03C 15N"))
                    {
                        detConFormato = VB6Helpers.Mid(detalle, 1, 5) + " ";
                        detConFormato += FormatUtils.FormatMontoUSD(pvarMoneda, VB6Helpers.Mid(detalle, 6));
                    }
                    else if (strFormato.StartsWith("06G 03C"))
                    {
                        detConFormato = FormatUtils.FormaFecha(VB6Helpers.Mid(detalle, 1, 6)) + " ";
                        detConFormato += FormatUtils.FormatMontoUSD(pvarMoneda, VB6Helpers.Mid(detalle, 7));
                    }
                    else if (strFormato.StartsWith("03C 15"))
                    {
                        detConFormato = FormatUtils.FormatMontoUSD(pvarMoneda, detalle);
                    }
                    else
                    {
                        string substr3 = strFormato.Substring(0, 3);
                        if (substr3 == "03C" || substr3 == "03A")
                        {
                            detConFormato = VB6Helpers.Mid(detalle, 1, 3) + " ";
                            detConFormato += VB6Helpers.Mid(detalle, 4);
                        }
                        else if (substr3 == "06G")
                        {
                            detConFormato = FormatUtils.FormaFecha(VB6Helpers.Mid(detalle, 1, 6)) + " ";
                            detConFormato += VB6Helpers.Mid(detalle, 7);
                        }
                        else if (substr3 == "08G")
                        {
                            detConFormato = FormatUtils.FormaFecha(VB6Helpers.Mid(detalle, 3, 6)) + " ";
                            detConFormato += VB6Helpers.Mid(detalle, 9);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //ignoro este error si no pude parsear con algun formato especifico, igual muestro el dato crudo sin parsear
                }

            }

            return detConFormato;
        }
        
        private sw_bancos BuscaDatosBanco(string strCodigoYBranch)
        {
            string strBanco = "";
            string strCampo = "";
            string strCodBco = "";
            string strBranch = "";
            short intPos = 0;
            
            // Particonar el Mensaje Ver aplicaicon PB

            intPos = (short)VB6Helpers.Instr(1, strCodigoYBranch, VB6Helpers.Chr(10));
            if (intPos == 0)
            {
                strBanco = strCodigoYBranch;
                strCampo = "";
            }
            else
            {
                strBanco = VB6Helpers.Mid(strCodigoYBranch, 1, intPos - 1);
                strCampo = VB6Helpers.Mid(strCodigoYBranch, intPos + 1);
            }

            strCodBco = VB6Helpers.Mid(strBanco, 1, 8);
            strBranch = VB6Helpers.Mid(strBanco, 9, 3);

            if (VB6Helpers.Trim(strBranch) == "" || VB6Helpers.Trim(strBranch) == VB6Helpers.Chr(10) || VB6Helpers.Trim(strBranch) == VB6Helpers.Chr(13))
            {
                strBranch = "XXX";
            }
            sw_bancos result = uow.BancoRepository.GetBancosByCodAndBranch(strCodBco, strBranch).FirstOrDefault();
            if (result == null)
            {
                strBanco = strCampo;
                strCampo = "";
                strCodBco = VB6Helpers.Mid(strBanco, 1, 8);
                strBranch = VB6Helpers.Mid(strBanco, 9, 3);
                if (String.IsNullOrEmpty(strBranch))
                {
                    strBranch = "XXX";
                }
                result = uow.BancoRepository.GetBancosByCodAndBranch(strCodBco, strBranch).FirstOrDefault();
            }

            return result;
        }

        //funcion que recibe una lista de ruts de siwft (no tienen digito verificador), y trae los usuarios correspondientes de la base Cext01, donde si tienen digito verificador y tienen un tamanio fijo de 10 caracteres
        //
        private IList<sce_usr> GetUsuariosDeCextEnBaseARutsDeSwift(params int[] rutsDeInteres)
        {

            if (rutsDeInteres != null && rutsDeInteres.Length > 0)
            {
                using (UnitOfWorkCext01 uowCext = new UnitOfWorkCext01())
                {
                    IList<string> rutsDeInteresComoFormateo = rutsDeInteres.Distinct().Select(r => r.ToString().PadLeft(9, '0')).ToList();
                    return uowCext.UsuarioRepository.Get(u => rutsDeInteresComoFormateo.Contains(u.rut.Substring(0, u.rut.Length - 1))).ToList(); //esta consulta deberia ser un SP
                }
            }
            else return null;
        }

        private IList<sce_usr> GetUsuariosNombre(string nombre)
        {
            using (UnitOfWorkCext01 uowCext = new UnitOfWorkCext01())
            {
                //IList<string> usuarios = nombre.Select(n => n.ToString()).ToList();
                return uowCext.UsuarioRepository.Get(u => nombre.Contains(nombre)).ToList(); 
               
            }
        }

        private sce_usr BuscarUsuarioCextComparandoConRutSwift(IList<sce_usr> users, int rutSwift)
        {
            return users.Where(u => u.rut.Substring(0, u.rut.Length - 1) == rutSwift.ToString().PadLeft(9, '0')).FirstOrDefault();
        }

        private void ComplementarLogsConNombresPersonas(IList<proc_sw_log_trae_msg_MS_Result> logs)
        {
            if (logs != null)
            {
                //busco los nombres de las personas ya que el log solo me trae los ruts

                IList<proc_sw_log_trae_msg_MS_Result> logsConRutAis = logs.Where(l => l.rutais_log != null).ToList();
                int[] rutsDeInteres = logsConRutAis.Select(l => l.rutais_log.Value).ToArray();
                IList<sce_usr> usuariosLogs = GetUsuariosDeCextEnBaseARutsDeSwift(rutsDeInteres);

                foreach (proc_sw_log_trae_msg_MS_Result log in logsConRutAis)
                {
                    sce_usr usr = BuscarUsuarioCextComparandoConRutSwift(usuariosLogs, log.rutais_log.Value);
                    if (usr != null)
                    {
                        log.NombrePersonaAis = usr.nombre;
                    }
                }
            }
        }

        public bool SaveFirmaEnvAutm(int idmensaje, int casilla, int rut, DateTime fecha_solic, string comentario, out bool tieneFirmasNecesarias)
        {
            using (Tracer tracer = new Tracer())
            {
                try
                {
                    int resultadoNoTieneFirmasNecesarias = -100;
                    int result = uow.AdministracionRepository.proc_sw_env_graba_aum_MS(idmensaje, casilla, rut, fecha_solic, comentario);
                    tieneFirmasNecesarias = false;

                    if (result < 0)
                    {
                        if (result == resultadoNoTieneFirmasNecesarias)
                        {
                            tieneFirmasNecesarias = false;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        tieneFirmasNecesarias = true;
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, ha ocurrido un problema al cambiar al autorizar mensaje en EnvAutm", ex);
                    throw;
                }
            }
        }

        public bool CambiaEstadoFirmasSAP(int casilla, int idMensaje, int rut, DateTime fecha, string comentario)
        {
            using (Tracer tracer = new Tracer())
            {
                try
                {
                    return uow.SwRepository.proc_sw_env_graba_sap(casilla, idMensaje, rut, fecha, comentario);
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, ha ocurrido un problema al cambiar estado firmas SAP", ex);
                    throw;
                }
            }
        }
        
        public bool ValidaInyecciones(int idMensaje, ref int cantidadOriginal, ref int cantidadRelacionada)
        {
            using (UnitOfWorkCext01 unitOfWorkCext01 = new UnitOfWorkCext01()) {
                bool retorno = true;
                var mensaje = uow.MensajeRepository.Get(idMensaje);
                if (mensaje != null && !string.IsNullOrEmpty(mensaje.referencia) && mensaje.referencia.Length >= 15)
                {
                    var referencia = mensaje.referencia;
                    var cantidad = unitOfWorkCext01.SceRepository.get_cantidad_inyecciones_pendientes(referencia.Substring(0, 3), referencia.Substring(3, 2), referencia.Substring(5, 2), referencia.Substring(7, 3), referencia.Substring(10, 5));
                    cantidadOriginal = (int)cantidad.Cantidad_Original;
                    cantidadRelacionada = (int)cantidad.Cantidad_Relacionada;
                    //si hay cargos por inyectar debemos evitar que firme
                    if (cantidadOriginal + cantidadRelacionada > 0)
                        retorno = false;
                }
                return retorno;
            }
        }

    }
}
