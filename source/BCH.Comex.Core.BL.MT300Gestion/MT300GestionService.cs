using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.SWG3;
using BCH.Comex.Core.BL.SWG3.Helpers;
using BCH.Comex.Core.Entities.Cext01.MT300Common;
using BCH.Comex.Core.Entities.Cext01.MT300Gestion;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Swift;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BCH.Comex.Core.BL.MT300Gestion
{
    public class MT300GestionService : IDisposable
    {
        private readonly UnitOfWorkCext01 uow;
        private readonly UnitOfWorkSwift uowSwift;
        private readonly GenerarMT300Helper generarMT300Helper;

        public MT300GestionService()
        {
            uow = new UnitOfWorkCext01();
            uowSwift = new UnitOfWorkSwift();
            generarMT300Helper = new GenerarMT300Helper();
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
            using (Tracer tracer = new Tracer("MT300GestionService - Iniciar"))
            {
                DatosGlobales globales = new DatosGlobales();
                globales.DatosUsuario = datosUsuario;

                return globales;
            }
        }

        public IList<ResultadoBusquedaMT300> BuscarSwifts(bool usarFiltros, string referencia, string destino, string cuenta, DateTime? fecha, int rowOffset, int pageSize, string sortOrder, string searchText, out int totalRows)
        {
            var resultado = uow.Mt300GestionRepository.GetRegistrosEnviadosPag(usarFiltros, referencia, destino, cuenta, fecha, rowOffset, pageSize);
            totalRows = uow.Mt300GestionRepository.GetRegistrosEnviadosTot(usarFiltros, referencia, destino, cuenta, fecha);

            return resultado;
        }

        public Mt300ArchivoProcesado GetArchivoProcesado(decimal id)
        {
            return uow.Mt300ArchivosProcesadosRepository.GetArchivoProcesado(id);
        }

        public bool ValidarReferencia(string reference, out string mensajeError)
        {
            mensajeError = "";
            if (String.IsNullOrEmpty(reference))
            {
                mensajeError = "La referencia no puede estar vacía.";
                return false;
            }
            else if (this.uow.Mt300ArchivosProcesadosRepository.ExistsArchivoProcesado(reference))
            {
                mensajeError = "Ya existe un mensaje con esta referencia.";
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidarCampoMonto(string monedaValor, out string mensajeError)
        {
            mensajeError = "";
            if (monedaValor.Length <= 3)
            {
                mensajeError = "El campo debe estar formado por un código de moneda de 3 caracteres y un monto";
                return false;
            }
            else
            {
                string moneda = monedaValor.Substring(0, 3);
                string monto = monedaValor.Substring(3);
                if (!moneda.All(Char.IsLetterOrDigit))
                {
                    mensajeError = "El código de moneda no es alfanumérico";
                    return false;
                }

                var registroMon = uowSwift.MonedaRepository.Get(m => m.cod_moneda_sw == moneda && m.uso_moneda_banco == "S").FirstOrDefault();
                if (registroMon == null)
                {
                    mensajeError = "Codigo de Moneda no reconocido (formato ISO 4217​)";
                    return false;
                }
                decimal number;
                if (!Decimal.TryParse(monto, out number))
                {
                    mensajeError = "El formato de moneda no es válido";
                    return false;
                }
            }
             return true;
        }

        public bool GenerarNuevoMensaje(DatosGlobales globales, ArchivoDetalle registro)
        {
            string msg;
            if (!ValidarReferencia(registro.reference, out msg))
            {
                globales.ListaMensajes.Add(new Comex.Common.UI_Modulos.UI_Message
                {
                    Type = Comex.Common.UI_Modulos.TipoMensaje.Error,
                    Text = msg,
                    ControlName = "Mensaje_reference"
                });
                return false;
            }

            List<ArchivoDetalle> registros = new List<ArchivoDetalle>();
            registros.Add(registro);

            GenerarMT300DatosUser infoUser = new GenerarMT300DatosUser
            {
                usuarioNombre = globales.DatosUsuario.samAccountName,
                usuarioRut = globales.DatosUsuario.Identificacion_Rut,
                usuarioCentroCosto = globales.DatosUsuario.Identificacion_CentroDeCostosOriginal
            };

            GenerarMT300Result resultadoGeneracion = generarMT300Helper.GenerarSwiftMasivoMT300(registros, infoUser, GenerarMT300Helper.modoManual, GenerarMT300Helper.flujoNuevoDesdeCero);

            if (resultadoGeneracion.CantGenerados == 1)
            {
                globales.ListaMensajes.Add(new Comex.Common.UI_Modulos.UI_Message
                {
                    Type = Comex.Common.UI_Modulos.TipoMensaje.Informacion,
                    Text = "Mensaje generado correctamente",
                    Title = "MT300"
                });
                return true;
            }
            else
            {
                globales.ListaMensajes.Add(new Comex.Common.UI_Modulos.UI_Message
                {
                    Type = Comex.Common.UI_Modulos.TipoMensaje.Error,
                    Text = resultadoGeneracion.Mensaje,
                    Title = "MT300"
                });
                return false;
            }
        }


        public bool ModificarMensaje(DatosGlobales globales, ArchivoDetalle registroModificado)
        {
            Mt300ArchivoProcesado registroOriginal = GetArchivoProcesado(registroModificado.id_procesados);

            ArchivoDetalle registro = new ArchivoDetalle
            {
                id_procesados = registroOriginal.id_procesados,
                id_archivo_detalle = registroOriginal.id_archivo_detalle,
                reference = registroOriginal.reference,
                campo22C = registroModificado.campo22C,
                value_date = registroModificado.value_date,
                booked_by = registroModificado.booked_by,
                rate = registroModificado.rate,
                codigo_moneda_mn = registroModificado.codigo_moneda_mn,
                codigo_moneda_me = registroModificado.codigo_moneda_me,
                amount_mn = registroModificado.amount_mn,
                amount_me = registroModificado.amount_me,
                campo82A = registroOriginal.campo82A,
                campo87A = registroOriginal.campo87A,
                beneficiary = registroOriginal.beneficiary,
                safekeeping = registroOriginal.safekeeping,
                estado = "MODIFICADO",
                campo53A = registroOriginal.campo53A,
                campo57A = registroOriginal.campo57A,
                campo98D = registroModificado.campo98D
            };

            List <ArchivoDetalle> registros = new List<ArchivoDetalle>();
            registros.Add(registro);

            GenerarMT300DatosUser infoUser = new GenerarMT300DatosUser
            {
                usuarioNombre = globales.DatosUsuario.samAccountName,
                usuarioRut = globales.DatosUsuario.Identificacion_Rut,
                usuarioCentroCosto = globales.DatosUsuario.Identificacion_CentroDeCostosOriginal
            };

            GenerarMT300Result resultadoGeneracion = generarMT300Helper.GenerarSwiftMasivoMT300(registros, infoUser, GenerarMT300Helper.modoManual, GenerarMT300Helper.flujoModificacion);

            if (resultadoGeneracion.CantGenerados == 1)
            {
                globales.ListaMensajes.Add(new Comex.Common.UI_Modulos.UI_Message
                {
                    Type = Comex.Common.UI_Modulos.TipoMensaje.Informacion,
                    Text = "Mensaje generado correctamente",
                    Title = "MT300"
                });
                return true;
            }
            else
            {
                globales.ListaMensajes.Add(new Comex.Common.UI_Modulos.UI_Message
                {
                    Type = Comex.Common.UI_Modulos.TipoMensaje.Error,
                    Text = resultadoGeneracion.Mensaje,
                    Title = "MT300"
                });
                return false;
            }
        }

        public bool AnularMensajes(DatosGlobales globales, decimal[] idProcesados)
        {
            using (Tracer tracer = new Tracer("Anulación de mensajes MT300"))
            {
                List<ArchivoDetalle> registros = new List<ArchivoDetalle>();
                List<UI_Message> mensajesError = new List<UI_Message>();

                foreach (decimal id in idProcesados)
                {
                    Mt300ArchivoProcesado registroOriginal = GetArchivoProcesado(id);

                    if (registroOriginal.estado != EstadosRegistro.procesadoNuevo && registroOriginal.estado != EstadosRegistro.procesadoModificado)
                    {
                        tracer.TraceError("El mensaje no se encuentra en estado NUEVO o MODIFICADO");
                        mensajesError.Add(new UI_Message()
                        {
                            Type = (TipoMensaje.Error),
                            Text = "El mensaje " + id + " no se encuentra en un estado que pueda ser anulado.",
                            Title = "MT300"
                        });
                    }
                    else if (registroOriginal.estado_msg != "ENV")
                    {
                        tracer.TraceError("El mensaje " + id + " no se encuentra en estado ENV.");
                        mensajesError.Add(new UI_Message()
                        {
                            Type = (TipoMensaje.Error),
                            Text = "El mensaje " + id + "no ha sido enviado, por lo que todavía no puede ser anulado.",
                            Title = "MT300"
                        });
                    }

                    ArchivoDetalle registro = new ArchivoDetalle
                    {
                        id_procesados = registroOriginal.id_procesados,
                        id_archivo_detalle = registroOriginal.id_archivo_detalle,
                        reference = registroOriginal.reference,
                        campo22C = registroOriginal.campo22C,
                        value_date = registroOriginal.value_date,
                        booked_by = registroOriginal.booked_by,
                        rate = registroOriginal.rate,
                        codigo_moneda_mn = registroOriginal.codigo_moneda_mn,
                        codigo_moneda_me = registroOriginal.codigo_moneda_me,
                        amount_mn = registroOriginal.amount_mn,
                        amount_me = registroOriginal.amount_me,
                        campo82A = registroOriginal.campo82A,
                        campo87A = registroOriginal.campo87A,
                        beneficiary = registroOriginal.beneficiary,
                        safekeeping = registroOriginal.safekeeping,
                        estado = "CANCELADO",
                        campo53A = registroOriginal.campo53A,
                        campo57A = registroOriginal.campo57A,
                        campo98D = registroOriginal.campo98D
                    };

                    registros.Add(registro);
                }

                if (mensajesError.Count > 0)
                {
                    return false;
                }

                GenerarMT300DatosUser infoUser = new GenerarMT300DatosUser
                {
                    usuarioNombre = globales.DatosUsuario.samAccountName,
                    usuarioRut = globales.DatosUsuario.Identificacion_Rut,
                    usuarioCentroCosto = globales.DatosUsuario.Identificacion_CentroDeCostosOriginal
                };

                GenerarMT300Result resultadoGeneracion = generarMT300Helper.GenerarSwiftMasivoMT300(registros, infoUser, GenerarMT300Helper.modoManual, GenerarMT300Helper.flujoAnulacion);

                if (resultadoGeneracion.CantGenerados > 0 && resultadoGeneracion.CantError == 0)
                {
                    string msg;
                    if (resultadoGeneracion.CantGenerados == 1)
                    {
                        msg = "Mensaje anulado correctamente";
                    }
                    else
                    {
                        msg = resultadoGeneracion.CantGenerados + " mensajes anulados correctamente";
                    }
                    globales.ListaMensajes.Add(new Comex.Common.UI_Modulos.UI_Message
                    {
                        Type = Comex.Common.UI_Modulos.TipoMensaje.Informacion,
                        Text = msg,
                        Title = "MT300"
                    });
                    return true;
                }
                else
                {
                    globales.ListaMensajes.Add(new Comex.Common.UI_Modulos.UI_Message
                    {
                        Type = Comex.Common.UI_Modulos.TipoMensaje.Error,
                        Text = resultadoGeneracion.Mensaje,
                        Title = "MT300"
                    });
                    return false;
                }
            }
        }

        public bool ValidarRate(string rate, out string msg)
        {
            decimal output;
            msg = "";
            bool esValido = Decimal.TryParse(rate, out output);

            if (!esValido)
            {
                msg = "El campo debe ser un número válido";
                return false;
            }

            if (!rate.Contains(','))
            {
                msg = "El campo debe incluir una coma decimal";
                return false;
            }

            if (rate.Length > 12)
            {
                msg = "El campo no debe tener más de 12 caracteres, incluyendo una coma decimal";
                return false;
            }
            return true;
        }
    }
}
