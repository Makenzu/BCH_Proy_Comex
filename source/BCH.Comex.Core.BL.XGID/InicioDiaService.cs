using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BCH.Comex.Core.BL.XGID
{
    public class InicioDiaService : IDisposable
    {
        private UnitOfWorkCext01 uow;

        public InicioDiaService()
        {
            uow = new UnitOfWorkCext01();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<sce_usr> GetEspecialistasParaUsuario()
        {
            return uow.UsuarioRepository.GetEspecialistasPorCentroDeCosto();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usr"></param>
        /// <returns></returns>
        public IList<sce_usr> GetEspecialistasParaUsuario(sce_usr usr)
        {
            return uow.UsuarioRepository.GetEspecialistasPorCentroDeCosto(usr.cent_costo, usr.id_especia);
        }

        /// <summary>
        /// Equivale al metodo legacy SyGet_RempOrig
        /// </summary>
        /// <returns></returns>
        public IList<string> GetReemplazosParaUsuario(string cc, string idEspecialista)
        {
            return uow.SceRepository.Sce_Usr_S06_MS(cc, idEspecialista).ToList();
        }

        public sce_usr GetUsuario(string cc, string idEspecialista)
        {
            sce_usr usr = uow.UsuarioRepository.GetUsuario(cc, idEspecialista);
            usr.ListaReemplazos = GetReemplazosParaUsuario(cc, idEspecialista);
            return usr;
        }

        public void ReemplazarUsuario(IDatosUsuario usuario, string cc, string idEspecialista)
        {
            usuario.Identificacion_CCtUsr = cc + idEspecialista;

            PortalService service = new PortalService();
            service.CambiarCCtUsr(usuario);

            using (var tracer = new Tracer())
            {
                tracer.AddToContext("Inicio Dia - ReemplazarUsuario", string.Empty);
                tracer.AddToContext("Especialista", usuario.Identificacion_CCtUsr);
                tracer.AddToContext("EspOrig", usuario.Identificacion_CCtUsro);
            }
        }

        public bool IniciarDia(IDatosUsuario usuario, out List<UI_Message> messages)
        {
            using (var tracer = new Tracer())
            {
                tracer.AddToContext("Inicio Dia - IniciarDia", string.Empty);
                tracer.AddToContext("Especialista", usuario.Identificacion_CCtUsr);
                tracer.AddToContext("EspOrig", usuario.Identificacion_CCtUsro);
            }

            bool inicioDiaOK = false;
            messages = new List<UI_Message>();

            if (usuario.Identificacion_CCtUsr != usuario.Identificacion_CCtUsro) //o el centro de costo o el id de especialista estan distintos
            {
                messages.AddRange(ValidarInicioDia(usuario));
                if (messages.Count > 0)
                {
                    return inicioDiaOK;
                }
            }

            sce_usr_u02_MS_Result result = uow.SceRepository.sce_usr_u02_MS(usuario.Identificacion_CentroDeCostosImpersonado, usuario.Identificacion_IdEspecialistaImpersonado, "I", DateTime.Now);
            if (result == null || result.Column1 == -1 && result.Column2 != "Grabacion Exitosa")
            {
                messages.Add(
                    new UI_Message()
                    {
                        Text = result.Column2 ?? "Se ha producido un error al tratar de actualizar la fecha en Sce_Usr.",
                        Type = TipoMensaje.Error
                    });
            }
            else
            {
                inicioDiaOK = true;
            }

            return inicioDiaOK;
        }

        public IList<sce_nov_s01_MS_Result> GetNovedadesParaUsuario(string centroCosto, string codUsr, decimal jerarquia, DateTime fecha, IList<UI_Message> messages)
        {
            using (var tracer = new Tracer("Inicio Dia - GetNovedadesParaUsuario"))
            {
                DateTime diaHabilAnterior = GetDiaHabilAnterior(fecha);
                int? cantNovedades = uow.SceRepository.Sce_Nov_s03(centroCosto, codUsr, jerarquia, diaHabilAnterior);

                if (cantNovedades.HasValue && cantNovedades.Value > 1500)
                {
                    messages.Add(new UI_Message() { Title = "Cantidad excedida.", Text = "Existen demasiadas novedades, para obtener la totalidad de ellas, debe obtenerlas via archivo FTP.", Type = TipoMensaje.Informacion });
                    return new List<sce_nov_s01_MS_Result>();
                }
                else
                {
                    IList<sce_nov_s01_MS_Result> novedades = uow.SceRepository.Sce_Nov_s01(centroCosto, codUsr, jerarquia, diaHabilAnterior);
                    IList<sgt_mnd_s02_MS_Result> monedas = uow.SgtRepository.Sgt_Mnd_S02_MS();

                    foreach (sce_nov_s01_MS_Result novedad in novedades)
                    {
                        try
                        {
                            // Obtenemos la razon social
                            string nombrePrtCli = GetNombreParty(novedad.prtcli);

                            if (!string.IsNullOrEmpty(nombrePrtCli))
                            {
                                novedad.NombreParty = TitleCaseConExcepcionesEspaniol(nombrePrtCli.Trim());
                                novedad.NemonicoMoneda = monedas.Where(m => m.mnd_mndcod == novedad.codmnd).Select(x => x.mnd_mndnmc).FirstOrDefault();
                            }
                            else
                            {
                                var mensaje = string.Format("Para el rut {0} no se encontró la Razón Social.", novedad.prtcli.Replace("|", ""));
                                messages.Add(new UI_Message() { Title = "Alerta", Text = mensaje, Type = TipoMensaje.Warning });
                            }
                        }
                        catch (Exception ex)
                        {
                            tracer.TraceException("Alerta al leer razon social", ex);
                            messages.Add(new UI_Message() { Title = "Alerta", Text = "Ocurrio un problema al leer la novedad para el rut " + novedad.prtcli.Replace("|", "") + " - " + ex.Message , Type = TipoMensaje.Warning });
                        }
                    }
                    if (novedades.Count == 0)
                    {
                        messages.Add(new UI_Message() { Title = "Operación Realizada", Text = "Detalle: No existen novedades para el dia indicado", Type = TipoMensaje.Informacion, AutoClose = true });
                    }
                    return novedades;
                }
            }
        }

        public DateTime GetDiaHabilAnterior(DateTime fecha)
        {
            fecha = fecha.Date;

            //me traigo los feriados del ultimo mes, esto tiene que ser más que suficiente para encontrar una fecha válida antes
            IList<DateTime> feriados = uow.SceRepository.GetFeriadosEntreFechas(fecha.AddMonths(-1), fecha);

            DateTime fechaPosible = fecha;
            bool esFechaValida = false;
            while (!esFechaValida)
            {
                fechaPosible = fechaPosible.AddDays(-1);
                if (fechaPosible.DayOfWeek != DayOfWeek.Saturday && fechaPosible.DayOfWeek != DayOfWeek.Sunday)
                {
                    esFechaValida = !feriados.Where(f => f == fechaPosible).Any();
                }
            }

            return fechaPosible;
        }



        private IList<UI_Message> ValidarInicioDia(IDatosUsuario usuario)
        {
            List<UI_Message> messages = new List<UI_Message>();
            bool? diaIniciado = DiaIniciado(usuario);
            if (diaIniciado.HasValue)
            {
                if (!diaIniciado.Value)
                {
                    messages.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "Antes de Realizar Inicio de Dia de otros Usuarios, debe realizar el suyo " + usuario.Identificacion_CentroDeCostosOriginal + " - " + usuario.Identificacion_IdEspecialistaOriginal });
                }
            }
            else
            {
                messages.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "Los Datos de las fechas de procesos del Usuario Original no han sido encontrados (Sce_Usr)" });
            }

            return messages;
        }

        public bool? DiaIniciado(IDatosUsuario usuario)
        {
            sce_usr_s04_MS_Result fechas = uow.UsuarioRepository.GetFechasUsuario(usuario.Identificacion_CentroDeCostosOriginal, usuario.Identificacion_IdEspecialistaOriginal);
            if (fechas != null)
            {
                return (fechas.fec_ini > fechas.fec_fin);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Se obtiene la razon social.
        /// </summary>
        /// <param name="idParty"></param>
        /// <returns></returns>
        private string GetNombreParty(string idParty)
        {
            idParty = idParty.PadRight(12, '|');
            return uow.SceRepository.sce_rsa_s03(idParty, 0).FirstOrDefault();
        }

        private static string TitleCaseConExcepcionesEspaniol(string palabra)
        {
            string[] excepcionesMayuscula = new string[] { "S.A.", "M/E", "M/N" };
            string[] excepcionesMinuscula = new string[] { "A", "DE", "Y", "O", "Y/O", "U", "AL", "DE", "LO", "LA", "EL", "SI", "NO", "E", "POR", "OF" };

            TextInfo info = new CultureInfo("es-ES", false).TextInfo;
            palabra = info.ToTitleCase(palabra.ToLower());

            string palabraTransformada = String.Empty;

            string[] partesPalabra = palabra.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < partesPalabra.Length; i++)
            {
                string parte = partesPalabra[i];

                if (excepcionesMayuscula.Contains(parte.ToUpper()))
                {
                    partesPalabra[i] = parte.ToUpper();
                }
                else if (excepcionesMinuscula.Contains(parte.ToUpper()))
                {
                    partesPalabra[i] = parte.ToLower();
                }
            }

            return String.Join(" ", partesPalabra);
        }

        public void Dispose()
        {
            if (uow != null)
            {
                uow.Dispose();
            }
        }

        /// <summary>
        /// Validamos si el usuario activo, cuenta con codigo de sucursal configurado.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool ValidarSucursalUsuario(IDatosUsuario usuario)
        {
            PortalService service = new PortalService();            
            return service.ValidarSucursalUsuario(usuario);
        }
    }
}
