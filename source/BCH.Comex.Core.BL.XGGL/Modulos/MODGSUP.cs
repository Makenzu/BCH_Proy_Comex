using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public class MODGSUP
    {
        public const int Jerarquia_Usr = 0;
        public const int Jerarquia_Sup = 1;
        public const int Jerarquia_Sec = 2;
        
        /// <summary>
        /// Obtiene los datos de usuario desde la BD
        /// </summary>
        /// <param name="cencos"></param>
        /// <param name="codusr"></param>
        /// <param name="globales"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public static bool SyGet_Usr(string cencos, string codusr, DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer())
            {

                try
                {
                    var r = uow.SceRepository.Sce_Usr_S05_MS(cencos, codusr);

                    // Se realizó el Query pero la consulta no retornó datos.-
                    if (r == null)
                    {
                        globales.MESSAGES.Add(new UI_Message
                        {
                            Text = "Los Datos del Usuario no han sido encontrados (Sce_Usr)",
                            Title = "Supervisor",
                            Type = TipoMensaje.Informacion
                        });
                        tracer.TraceError("Los Datos del Usuario no han sido encontrados (Sce_Usr)");
                        return false;
                    }

                    sce_usr datosUsuario = new sce_usr()
                    {
                        cent_costo = r.cent_costo,
                        cent_super = r.cent_super,
                        ciudad = r.ciudad,
                        comuna = r.comuna,
                        delegada = r.delegada,
                        direccion = r.direccion,
                        fax = r.fax,
                        id_especia = r.id_especia,
                        id_super = r.id_super,
                        jerarquia = r.jerarquia,
                        nombre = r.nombre,
                        ofic_orige = r.ofic_orige,
                        rut = r.rut,
                        seccion = r.seccion,
                        swift = r.swift,
                        telefono = r.telefono,
                        tipeje = r.tipeje
                    };

                    globales.UsrEsp = datosUsuario;

                    if (SyGet_Remp(datosUsuario.cent_costo, datosUsuario.id_especia, globales, uow) == "-1")
                    {
                        return false;
                    }

                    return true;

                }
                catch (Exception exc)
                {
                    tracer.TraceException("SyGet_Usr", exc);
                    //todo: manejar la excepcion como corresponde
                    throw;
                }
            }
        }

        /// <summary>
        /// Obtiene los remplazos del especialista
        /// </summary>
        /// <param name="codcct"></param>
        /// <param name="codesp"></param>
        /// <param name="globales"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public static string SyGet_Remp(string codcct, string codesp, DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer())
            {
                try
                {

                    var queryResult = uow.SceRepository.Sce_Usr_S06_MS(codcct, codesp);
                    if (queryResult != null && queryResult.Count > 0)
                    {
                        globales.UsrEsp.reemplazos = queryResult.Aggregate((x, y) => x + ";" + y); //concateno separando por ;
                    }

                    return globales.UsrEsp.reemplazos;

                }
                catch (Exception exc)
                {
                    globales.MESSAGES.Add(new UI_Message
                    {
                        Text = "Se ha producido un error al tratar de leer los reemplazos de los Usuarios",
                        Title = "Identificación de Usuarios",
                        Type = TipoMensaje.Informacion
                    });
                    tracer.TraceException("Alerta: No se ha podido leer los reemplazos de los Usuario", exc);
                    throw;
                }
            }
        }

    }
}
