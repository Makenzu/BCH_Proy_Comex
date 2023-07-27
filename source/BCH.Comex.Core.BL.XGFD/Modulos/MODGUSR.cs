using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Common.Exceptions;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.FinDia;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BCH.Comex.Core.BL.XGFD.Modulos
{
    internal static class MODGUSR
    {

        public static bool VerRegistroUsuario(int Valida, DatosGlobales globales, IList<UI_Message> ListaMensajesError, UnitOfWorkCext01 uow)
        {
            bool VerRegistroUsuario = false;

            using (Tracer tracer = new Tracer("Lectura usuario"))
            {
                string UsrOrig = "";
                bool X = false;
                string usuario = "";

                // ----------------------------------------------------------------------------
                // © 13/10/93 by WaldoSoft, Version 1.0
                // Esta rutina carga los atributos del especialista asociado al Rut, conectando
                // solo aquel que esta marcado como "en operacion".-
                // ----------------------------------------------------------------------------

                const string Validos = "0123456789K";
                const string FormaRut = "@@@.@@@.@@@-@";
                string LeRut = "";
                string Rut = "";
                string Aux = "";
                int ind = 0;
                string BaseUser = "";
                string Selec = "";
                int Cuantos = 0;
                int Operando = 0;
                string LeRutOtro = "";

                // ok, preparamos para posible error.-
                try
                {

                    // Obtiene los datos del Especialista.-

                    X = SyGet_Usr(globales.Usuario.Identificacion_CCtUsr.Substring(0, 3), globales.Usuario.Identificacion_CCtUsr.Substring(3, 2),globales,ListaMensajesError,uow);
                    if (!X)
                    {
                        return VerRegistroUsuario;
                    }
                    X = SyGet_OfiUsr(globales.Usuario.Identificacion_CCtUsr.Substring(0, 3), globales.Usuario.Identificacion_CCtUsr.Substring(3, 2), globales.MODGUSR, ListaMensajesError, uow);
                    if (!X)
                    {
                        return VerRegistroUsuario;
                    }

                    // Verifica que se haya hecho Inicio de Día Hoy.-
                    if (globales.MODGUSR.UsrEsp.Tipeje == "O")
                    {
                        if (Valida != 0)
                        {
                            X = SyGetf_Usr(globales.MODGUSR, ListaMensajesError, uow,globales.Usuario.Identificacion_CCtUsr.Substring(0, 3), globales.Usuario.Identificacion_CCtUsr.Substring(3, 2), "I");
                            if (!X )
                            {
                                return VerRegistroUsuario;
                            }
                        }
                    }

                    // Identifica Usuario Original.-
                    //UsrOrig = MODGPYF0.GetSceIni("Identificacion", "CCtUsro");
                    globales.MODGUSR.UsrEsp.CCtOrig = globales.Usuario.Identificacion_CCtUsro.Substring(0, 3); //UsrOrig.Left(3);
                    globales.MODGUSR.UsrEsp.EspOrig = globales.Usuario.Identificacion_CCtUsro.Substring(3, 2);//UsrOrig.Right(2);

                    // Reemplzaos del Usuario Original.-
                    //globales.MODGUSR.UsrEsp.RempOrig = globales.Usuario.//SyGet_RempOrig(UsrEsp.CCtOrig, UsrEsp.EspOrig);


                    VerRegistroUsuario = true;

                }
                catch (Exception exc)
                {
                    if (ExceptionPolicy.HandleException(exc, "PoliticaBLFundTransfer")) throw;
                }
            }
            return VerRegistroUsuario;
        }
      
        public static bool SyGet_Usr(string cencos, string codusr, DatosGlobales globales, IList<UI_Message> ListaMensajesError, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer())
            {

                try
                {
                    var r = uow.SceRepository.Sce_Usr_S05_MS(cencos, codusr);

                    // Se realizó el Query pero la consulta no retornó datos.-
                    if (r == null)
                    {
                        ListaMensajesError.Add(new UI_Message
                        {
                            Text = "Los Datos del Usuario no han sido encontrados (Sce_Usr)",
                            Title = "Devengo",
                            Type = TipoMensaje.Informacion
                        });
                        tracer.TraceError("Los Datos del Usuario no han sido encontrados (Sce_Usr)");
                        return false;
                    }

                    EstrucUsuarios datosUsuario = new EstrucUsuarios()
                    {
                        Rut = r.rut,
                        Jerarquia = (int) r.jerarquia,
                        CentroCosto = r.cent_costo,
                        Especialista = r.id_especia,
                        Delegada = r.delegada?-1:0,
                        CostoSuper = r.cent_super,
                        Id_Super = r.id_super,
                        nombre = r.nombre,
                        Direccion = r.direccion,
                        comuna = r.comuna,
                        Ciudad = r.ciudad,
                        Seccion = r.seccion,
                        Oficina = (int) r.ofic_orige,
                        Telefono = r.telefono,
                        swift = r.swift,
                        Fax = r.fax,
                        Tipeje = r.tipeje
                    };

                    globales.MODGUSR.UsrEsp = datosUsuario;

                    if (SyGet_Remp(datosUsuario.CentroCosto, datosUsuario.Especialista, globales, ListaMensajesError, uow) == "-1")
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

        public static string SyGet_Remp(string CodCct, string CodEsp, DatosGlobales globales, IList<UI_Message> ListaMensajesError, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer())
            {
                try
                {

                    var queryResult = uow.SceRepository.Sce_Usr_S06_MS(CodCct, CodEsp);
                    if (queryResult != null && queryResult.Count > 0)
                    {
                        globales.MODGUSR.UsrEsp.reemplazos = queryResult.Aggregate((x, y) => x + ";" + y); //concateno separando por ;
                    }

                    return globales.MODGUSR.UsrEsp.reemplazos;

                }
                catch (Exception exc)
                {
                    ListaMensajesError.Add(new UI_Message
                    {
                        Text = "Se ha producido un error al tratar de leer los reemplazos de los Usuarios",
                        Title = "Identificación de Usuarios",
                        Type = TipoMensaje.Informacion
                    });
                    tracer.TraceException("Se ha producido un error al tratar de leer los reemplazos de los Usuario", exc);
                    throw;
                }
            }

        }

        public static bool SyGet_OfiUsr(string cencos, string codusr, T_MODGUSR MODGUSR, IList<UI_Message> ListaMensajesError, UnitOfWorkCext01 unit)
        {
            bool _retValue = false;
            try
            {

                string userOfi = unit.SceRepository.EjecutarSP<string>("sce_usr_s09_MS", cencos, codusr).First();
                MODGUSR.UsrEsp.OfixUser = userOfi;

                _retValue = true;
            }
            catch (Exception e)
            {

            }
            return _retValue;
        }

        //Lee las Fechas de la Tabla de Usuarios : Sce_Usr.-
        public static bool SyGetf_Usr(T_MODGUSR MODGUSR , IList<UI_Message> ListaMensajesError, UnitOfWorkCext01 unit, string cencos, string codusr, string Etapa)
        {
            bool _retValue = false;
            try
            {
                var fechas = unit.SceRepository.EjecutarSP<sce_usr_s04_MS_Result>("sce_usr_s04_MS", cencos, codusr).First();
                DateTime FechaIni = fechas.fec_ini;
                DateTime FechaFin = fechas.fec_fin;
                DateTime FechaOut = fechas.fec_out;
                if (MODGUSR.UsrEsp.Especialista != "00")
                {
                    switch (Etapa)
                    {
                        case "I":
                            if (FechaIni.Date == FechaOut.Date)
                            {
                                throw new ComexUserException("Ya se ha efectuado el Cierre Diario de Comercio Exterior. No podrá utilizar esta aplicación.");
                            }
                            if (FechaIni < FechaFin)
                            {
                                throw new ComexUserException("Antes de Operar con las aplicaciones debe ejecutar el Proceso de Inicio de Dia.");
                            }
                            break;
                    }
                }
                _retValue = true;

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "PoliticaBLFundTransfer")) throw;
            }

            return _retValue;
        }


        public static bool SyGetn2_Usr(string cCostoSuper, string idSuper, T_MODGUSR modGUsr, UnitOfWorkCext01 uow, 
            IList<UI_Message> listaMensajes)
        {
            bool SyGetn2_Usr = false;

            try
            {
                var result = uow.SceRepository.sce_usr_s10_MS(cCostoSuper, idSuper);

                // Se realizó el Query pero la consulta no retornó datos.-
                if (result == null || result.Count == 0)
                {
                    listaMensajes.Add(new UI_Message
                    {
                        Text = "El Líder no tiene Especialistas Asociados.",
                        Title = "Fin de dia",
                        Type = TipoMensaje.Error
                    });
                    
                    return false;
                }

                modGUsr.UsrLidEsp = new List<EstrucUsuarios>();
                foreach (var item in result)
                {
                    modGUsr.UsrLidEsp.Add(new EstrucUsuarios
                    {
                        Rut = item.rut,
                        Jerarquia = (int)item.jerarquia,
                        CentroCosto = item.cent_costo,
                        Especialista = item.id_especia,
                        Delegada = item.delegada == false ? 0 : 1,
                        CostoSuper = item.cent_super,
                        Id_Super = item.id_super,
                        nombre = item.nombre,
                        Direccion = item.direccion,
                        comuna = item.comuna,
                        Ciudad = item.ciudad,
                        Seccion = item.seccion,
                        Oficina = string.IsNullOrEmpty(item.Oficina) ? 0 : int.Parse(item.Oficina),
                        Telefono = item.telefono,
                        swift = item.swift,
                        Fax = item.fax,
                    });
                }

                SyGetn2_Usr = true;

                return SyGetn2_Usr;

            }
            catch (Exception exc)
            {
                if (!ExceptionPolicy.HandleException(exc)) throw;
            }

            return SyGetn2_Usr;
        }
    }
}
