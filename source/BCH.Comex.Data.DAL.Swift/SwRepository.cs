using BCH.Comex.Core.Entities.Swift;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Data.DAL.Swift
{
    public class SwRepository : GenericRepository<sw_log_msg, swiftEntities>
    {
        private static readonly DateTime SqlSmallDateTimeMinValue = new DateTime(1900, 01, 01, 00, 00, 00);


        public SwRepository(swiftEntities context) : base(context) { }

        public List<ProcSwTraeFmtCampos> Proc_sw_trae_fmt_campos(string codMT)
        {
            List<ProcSwTraeFmtCampos> result = new List<ProcSwTraeFmtCampos>();
            ReadQuerySP((reader) =>
            {
                while (reader.Read())
                {
                    result.Add(new ProcSwTraeFmtCampos
                    {
                        Status = Utils.GetStringFromDataReader(reader, 0),
                        Tag = Utils.GetStringFromDataReader(reader, 1),
                        Nombre = Utils.GetStringFromDataReader(reader, 2),
                        Orden = Utils.GetIntFromDataReader(reader, 3),
                        Repeticion = Utils.GetIntFromDataReader(reader, 4),
                        Formato = Utils.GetStringFromDataReader(reader, 5),
                        Largo = Utils.GetIntFromDataReader(reader, 6),
                        Linea = Utils.GetIntFromDataReader(reader, 7),
                        Secuencia = Utils.GetStringFromDataReader(reader, 8)
                    });
                }
            }, "proc_sw_trae_fmt_campos_MS", codMT);

            return result;
        }

        public List<ProcSwTraeFmtCampos> Proc_sw_trae_ciclos_campos(string codMT)
        {
            List<ProcSwTraeFmtCampos> result = new List<ProcSwTraeFmtCampos>();
            ReadQuerySP((reader) =>
            {
                while (reader.Read())
                {
                    result.Add(new ProcSwTraeFmtCampos
                    {
                        Status = Utils.GetStringFromDataReader(reader, 0),
                        Tag = Utils.GetStringFromDataReader(reader, 1),
                        Nombre = Utils.GetStringFromDataReader(reader, 2),
                        Orden = Utils.GetIntFromDataReader(reader, 3),
                        Repeticion = Utils.GetIntFromDataReader(reader, 4),
                        Formato = Utils.GetStringFromDataReader(reader, 5),
                        Largo = Utils.GetIntFromDataReader(reader, 6),
                        Linea = Utils.GetIntFromDataReader(reader, 7),
                        Secuencia = Utils.GetStringFromDataReader(reader, 8)
                    });
                }
            }, "proc_sw_trae_fmt_ciclos_MS", codMT);

            return result;
        }


        public List<ProcSwTraeFmtCampos> TraeFormatoMsg(string codMT)
        {
            bool tieneAlgunoConRepeticion = context.sw_formatos.Where(f => f.tipo_msg_fmt == codMT && f.repeticion_fmt > 0).Any();
            if (tieneAlgunoConRepeticion)
            {
                return this.Proc_sw_trae_ciclos_campos(codMT);
            }
            else
            {
                return this.Proc_sw_trae_fmt_campos(codMT);
            }
        }

        public Boolean ExisteConfiguracionCasilla(int rutUsuario)
        {
            bool estado = false;
            int? Resul = context.sw_configura_s01_MS(rutUsuario).FirstOrDefault();
            if (Resul == 1)
                estado = true;
            return estado;
        }

        public Boolean ExisteObservacionMensaje(int sesion, int secuencia)
        {
            bool estado = false;
            int? Resul = context.sw_mensajes_add_s01_MS(sesion, secuencia).FirstOrDefault();
            if (Resul == 1)
                estado = true;
            return estado;
        }

        public int? Proc_sw_msg_s01_MS(int idMensaje)
        {
            return context.proc_sw_msg_s01_MS(idMensaje).FirstOrDefault();
        }

        public IList<origen_recep_s01_MS_Result> ListaConfiguraCasilla()
        {
            return context.origen_recep_s01_MS().ToList();
        }

        public int GetCountPendientesOReenviados(int casillaDefault)
        {
            IList<proc_sw_rec_trae_ree_rango_MS_Result> listaReenviados = ListaMensajesReenviados(casillaDefault, SqlSmallDateTimeMinValue, DateTime.Now);
            IList<proc_sw_rec_trae_enc_rango_MS_Result> listaPendientes = ListaMensajesPendientes(casillaDefault, SqlSmallDateTimeMinValue, DateTime.Now);

            return listaPendientes.Count + listaReenviados.Count;
        }

        public IList<proc_sw_rec_trae_enc_rango_MS_Result> ListaMensajesPendientes(int casilla, DateTime? fechaDesde, DateTime fechaHasta)
        {
            return context.proc_sw_rec_trae_enc_rango_MS(casilla, fechaDesde, fechaHasta).ToList();
        }

        public IList<proc_sw_rec_trae_cnf_rango_MS_Result> ListaMensajesConfirmados(int casilla, DateTime fechaDesde, DateTime fechaHasta)
        {
            return context.proc_sw_rec_trae_cnf_rango_MS(casilla, fechaDesde, fechaHasta).ToList();
        }

        public IList<proc_sw_rec_trae_imp_rango_MS_Result> ListaMensajesImpresos(int casilla, DateTime fechaDesde, DateTime fechaHasta)
        {
            return context.proc_sw_rec_trae_imp_rango_MS(casilla, fechaDesde, fechaHasta).ToList();
        }

        public IList<proc_sw_rec_trae_ree_rango_MS_Result> ListaMensajesReenviados(int casilla, DateTime? fechaDesde, DateTime fechaHasta)
        {
            return context.proc_sw_rec_trae_ree_rango_MS(casilla, fechaDesde, fechaHasta).ToList();
        }

        public bool? proc_sw_Actualiza_usuarios_MS(int rut, string nombre, string tipo)
        {
            return context.proc_sw_Actualiza_usuarios_MS(rut, nombre, tipo).FirstOrDefault();
        }
        public bool? proc_sw_elimina_usuarios_MS(int rut)
        {
            return context.proc_sw_elimina_usuarios_MS(rut).FirstOrDefault();
        }
        public bool? proc_sw_graba_usuarios_MS(int rut, int CvRut, string nombre, string tipo)
        {
            return context.proc_sw_graba_usuarios_MS(rut, CvRut, nombre, tipo).FirstOrDefault();
        }
        public bool? proc_sw_Actualiza_casillas_MS(int codigo, string nombre, string origen)
        {
            return context.proc_sw_Actualiza_casillas_MS(codigo, nombre, origen).FirstOrDefault();
        }
        public bool? proc_sw_graba_casillas_MS(int codigo, string nombre, string origen)
        {
            return context.proc_sw_graba_casillas_MS(codigo, nombre, origen).FirstOrDefault();
        }
        public bool? proc_sw_elimina_casillas_MS(int codigo)
        {
            return context.proc_sw_elimina_casillas_MS(codigo).FirstOrDefault();

        }
        public bool? proc_sw_graba_monedas_MS(string codigoSw, int? codigoBc, string nombre, int decimales, string uso)
        {
            return context.proc_sw_graba_monedas_MS(codigoSw, codigoBc, nombre, decimales, uso).FirstOrDefault();

        }
        public bool? proc_sw_elimina_monedas_MS(string codigoSw)
        {
            return context.proc_sw_elimina_monedas_MS(codigoSw).FirstOrDefault();

        }
        public bool? proc_sw_Actualiza_monedas_MS(string CodigoSwMoneda, string NombreMoneda, string UsoMoneda, int? CodigoBcMoneda, int? DecimalesMoneda)
        {
            return context.proc_sw_Actualiza_monedas_MS(CodigoSwMoneda, NombreMoneda, UsoMoneda, CodigoBcMoneda, DecimalesMoneda).FirstOrDefault();
        }
        public bool? proc_sw_graba_bancos_MS(string codigo, string branch, string nombre, string direccion, string ciudad, string pais, string oficina, string clave, string localidad, string pob)
        {
            return context.proc_sw_graba_bancos_MS(codigo, branch, nombre, direccion, ciudad, pais, oficina, clave, localidad, pob).FirstOrDefault();
        }
        public bool? proc_sw_Actualiza_bancos_claves_MS(string clave, string codigo, string branch, int flag)
        {
            return context.proc_sw_Actualiza_bancos_claves_MS(clave, codigo, branch, flag).FirstOrDefault();
        }
        public bool? proc_sw_Actualiza_bancos_MS(string codigo, string nombre, string direccion, string ciudad, string oficina, string clave, string localidad, string pob, string pais, string branch)
        {
            return context.proc_sw_Actualiza_bancos_MS(codigo, nombre, direccion, ciudad, oficina, clave, localidad, pob, pais, branch).FirstOrDefault();
        }
        public bool? proc_sw_elimina_bancos_MS(string codigo, string branch, int flag)
        {
            return context.proc_sw_elimina_bancos_MS(flag, codigo, branch).FirstOrDefault();
        }
        public bool? proc_sw_Actualiza_valoresCampos_MS(string tipo, string tag, string condicion, string valor, int linea, int TotalValor)
        {
            return context.proc_sw_Actualiza_valoresCampos_MS(tipo, tag, condicion, valor, linea, TotalValor).FirstOrDefault();
        }
        public bool? proc_sw_graba_ValoresCampos_MS(string codigo, string tag, int linea, string condicion, string campos, int total)
        {
            return context.proc_sw_graba_ValoresCampos_MS(codigo, tag, linea, condicion, campos, total).FirstOrDefault();
        }
        public bool? proc_sw_elimina_ValoresCampos_MS(string codigo, string tag, int linea)
        {
            return context.proc_sw_elimina_ValoresCampos_MS(codigo, tag, linea).FirstOrDefault();
        }
        public bool? proc_sw_Actualiza_TiposMensajes_MS(string codigo, string nombre)
        {
            return context.proc_sw_Actualiza_TiposMensajes_MS(codigo, nombre).FirstOrDefault();
        }
        public bool? proc_sw_graba_TiposMensajes_MS(string codigo, string nombre)
        {
            return context.proc_sw_graba_TiposMensajes_MS(codigo, nombre).FirstOrDefault();
        }
        public bool? proc_sw_elimina_TiposMensajes_MS(string codigo)
        {
            return context.proc_sw_elimina_TiposMensajes_MS(codigo).FirstOrDefault();
        }
        public bool? proc_sw_Actualiza_FormatoMensajes_MS(string codigo, int orden, string secuencia, int repeticion, string tag, string status, int ordenOriginal, string tagOriginal)
        {
            return context.proc_sw_Actualiza_FormatoMensajes_MS(codigo, orden, secuencia, repeticion, tag, status, ordenOriginal, tagOriginal).FirstOrDefault();
        }
        public bool? proc_sw_graba_FormatoMensajes_MS(string codigo, int orden, string secuencia, int repeticion, string tag, string status)
        {
            return context.proc_sw_graba_FormatoMensajes_MS(codigo, orden, tag, secuencia, repeticion, status).FirstOrDefault();
        }
        public bool? proc_sw_elimina_FormatoMensajes_MS(string codigo, int orden, string tag)
        {
            return context.proc_sw_elimina_FormatoMensajes_MS(codigo, orden, tag).FirstOrDefault();
        }
        public bool? proc_sw_Actualiza_CampoMensajes_MS(string codigo, int linea, string nombre, string formato, int? largo)
        {
            return context.proc_sw_Actualiza_CampoMensajes_MS(codigo, linea, nombre, largo, formato).FirstOrDefault();
        }
        public bool? proc_sw_graba_CampoMensajes_MS(string codigo, int linea, string nombre, string formato, int? largo)
        {
            return context.proc_sw_graba_CampoMensajes_MS(codigo, linea, nombre, largo, formato).FirstOrDefault();
        }
        public bool? proc_sw_elimina_CampoMensajes_MS(string codigo, int linea)
        {
            return context.proc_sw_elimina_CampoMensajes_MS(codigo, linea).FirstOrDefault();
        }
        public bool? proc_sw_Actualiza_GlosaCampos_MS(string codigo, string tag, string nombre)
        {
            return context.proc_sw_Actualiza_GlosaCampos_MS(codigo, tag, nombre).FirstOrDefault();
        }
        public bool? proc_sw_graba_GlosaCampos_MS(string codigo, string tag, string nombre)
        {
            return context.proc_sw_graba_GlosaCampos_MS(codigo, tag, nombre).FirstOrDefault();
        }
        public bool? proc_sw_elimina_GlosaCampos_MS(string codigo, string tag)
        {
            return context.proc_sw_elimina_GlosaCampos_MS(codigo, tag).FirstOrDefault();
        }
        public bool? proc_sw_Actualiza_CaracterInvalido_MS(int codigo, string nombre)
        {
            return context.proc_sw_Actualiza_CaracterInvalido_MS(codigo, nombre).FirstOrDefault();
        }
        public bool? proc_sw_graba_CaracterInvalido_MS(int codigo, string caracter, string descripcion)
        {
            return context.proc_sw_graba_CaracterInvalido_MS(codigo, caracter, descripcion).FirstOrDefault();
        }
        public bool? proc_sw_elimina_CaracterInvalido_MS(int codigo)
        {
            return context.proc_sw_elimina_CaracterInvalido_MS(codigo).FirstOrDefault();
        }
        public int proc_sw_config_ing_MS(int rut, string aplicacion)
        {
            return context.proc_sw_config_ing_MS(rut, aplicacion);
        }

        public void proc_sw_env_graba_cambios(int idMensaje, string cambios)
        {
            context.proc_sw_env_graba_cambios_MS(idMensaje, cambios);
        }

        public bool proc_sw_env_graba_sap(int casilla, int idMensaje, int rut, DateTime fecha, string comentario)
        {
            int result = this.EjecutarSPConRetornoSinTransaccion("proc_sw_env_graba_sap_MS", String.Empty, new List<string> { casilla.ToString(), idMensaje.ToString(), rut.ToString(), fecha.ToString("yyyy-MM-dd HH:mm:ss"), comentario }, null);
            return result == 0;
        }

        public void sw_mensajes_u01_MS(int sesion, int secuencia)
        {
            context.sw_mensajes_u01_MS(sesion, secuencia);
        }

        public Boolean ExisteConfiguraciondeCasilla(int rutUsuario, string apli)
        {
            bool estado = false;
            int? Resul = context.sw_configura_s02_MS(rutUsuario, apli).FirstOrDefault();
            if (Resul == 1)
                estado = true;
            return estado;
        }

        /// <summary>
        /// Permite a los campos libres, hablitar funcionalidad de pantalla cuando se requiera.
        /// </summary>
        /// <returns></returns>
        public string[] CamposMTActivosLibres()
        {                              
            var data = this.EjecutarSP<string>("proc_sw_traer_mt_texto_libre", new string[] { });
            return data.ToArray();
        }

        /// <summary>
        /// Se evalua el MT permite continuar sin validar campo Monto y Moneda.
        /// </summary>
        /// <param name="mt"></param>
        /// <returns>True si valida moneda y monto</returns>
        public bool ValidaMTMonedaMonto(string mt)
        {
            bool resultado = true;
            switch (mt.ToUpper())
            {
                case "MT799":
                case "MT707":
                case "MT752":
                case "MT422":
                    resultado = false;
                    break;
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene los campos que deben ser sumados y validados contra el total del encabezado si el MT lo requiere
        /// </summary>
        /// <param name="mt">MT del mensaje a consultar</param>
        /// <returns>Listado de campos a sumar</returns>
        public List<string> ObtieneCamposSumatoriaMontoTotalMT(string mt)
        {
            List<string> campos = new List<string>();

            switch (mt.ToUpper())
            {
                case "MT742":
                case "MT750":
                    campos.AddRange(new List<string>() { "32B", "33B" });
                    break;
            }

            return campos;
        }
    }
}