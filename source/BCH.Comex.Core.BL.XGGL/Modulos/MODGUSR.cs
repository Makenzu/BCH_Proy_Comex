using BCH.Comex.Common.Exceptions;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public class MODGUSR
    {

        public static bool SyGet_OfiUsr(string cencos, string codusr, DatosGlobales globales, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer())
            {
                try
                {
                    T_MODGUSR MODGUSR = globales.MODGUSR;

                    var result = uow.SceRepository.Sce_Usr_S09_MS(MODGSYB.dbcharSy(cencos), MODGSYB.dbcharSy(codusr));

                    if (string.IsNullOrWhiteSpace(result))
                    {
                        globales.MESSAGES.Add(new UI_Message
                        {
                            Text = "Los Datos de las Oficinas asociadas al Usuario no han sido encontrados (Sce_Usr)",
                            Title = "Supervisor",
                            Type = TipoMensaje.Informacion
                        });
                        tracer.TraceError("Los Datos de las Oficinas asociadas al Usuario no han sido encontrados (Sce_Usr)");
                        return false;
                    }
                    globales.UsrEsp.ofixusr = result;
                    MODGUSR.UsrEsp.OfixUser = result;
                    return true;
                }
                catch (Exception exc)
                {
                    globales.MESSAGES.Add(new UI_Message
                    {
                        Text = "Los Datos de las Oficinas asociadas al Usuario no han sido encontrados (Sce_Usr)",
                        Title = "Supervisor",
                        Type = TipoMensaje.Informacion
                    });
                    tracer.TraceException("Los Datos de las Oficinas asociadas al Usuario no han sido encontrados (Sce_Usr)", exc);
                }

                return false;
            }
        }

        //Lee la Tabla de Usuarios : Sce_Usr.-
        public static short SyGet_Usr(DatosGlobales Globales, UnitOfWorkCext01 unit, string cencos, string codusr)
        {
            using(var tracer = new Tracer("SyGet_Usr"))
            {
                T_MODGUSR MODGUSR = Globales.MODGUSR;
                short _retValue = 0;
                try
                {

                    var usuario = unit.SceRepository.EjecutarSP<sce_usr_s05_MS_Result>("sce_usr_s05_MS", cencos, codusr).First();
                    MODGUSR.UsrEsp.Rut = usuario.rut;
                    MODGUSR.UsrEsp.Jerarquia = (short)usuario.jerarquia;
                    MODGUSR.UsrEsp.CentroCosto = usuario.cent_costo;
                    MODGUSR.UsrEsp.Especialista = usuario.id_especia;
                    MODGUSR.UsrEsp.Delegada = (short)(usuario.delegada ? -1 : 0);
                    MODGUSR.UsrEsp.CostoSuper = usuario.cent_super;
                    MODGUSR.UsrEsp.Id_Super = usuario.id_super;
                    MODGUSR.UsrEsp.Nombre = usuario.nombre;
                    MODGUSR.UsrEsp.Direccion = usuario.direccion;
                    MODGUSR.UsrEsp.Comuna = usuario.comuna;
                    MODGUSR.UsrEsp.Ciudad = usuario.ciudad;
                    MODGUSR.UsrEsp.Seccion = usuario.seccion;
                    MODGUSR.UsrEsp.Oficina = (short)usuario.ofic_orige;
                    MODGUSR.UsrEsp.Telefono = usuario.telefono;
                    MODGUSR.UsrEsp.Swift = usuario.swift;
                    MODGUSR.UsrEsp.Fax = usuario.fax;
                    MODGUSR.UsrEsp.Tipeje = usuario.tipeje;

                    if (SyGet_Remp(MODGUSR, unit, MODGUSR.UsrEsp.CentroCosto, MODGUSR.UsrEsp.Especialista) == "-1")
                    {
                        tracer.AddToContext("Especialista","Reemplazo del Especialista es -1");
                        return 0;
                    }
                    _retValue = -1;
                }
                catch (Exception ex)
                {
                    tracer.AddToContext("Excepcion", ex.Message);
                    _retValue = 0;
                }
                return _retValue;
            }
        }

        //Retorna los Reemplazos de un Usuario.-
        public static string SyGet_Remp(T_MODGUSR MODGUSR, UnitOfWorkCext01 unit, string codcct, string codesp)
        {
            using(var tracer = new Tracer("SyGet_Remp"))
            {
                try
                {
                    List<string> reemp = unit.SceRepository.EjecutarSP<string>("sce_usr_s06_MS", codcct, codesp);
                    return String.Join(",", reemp.ToArray());
                }
                catch (Exception ex)
                {
                    tracer.AddToContext("Excepcion", ex.Message);
                    return "-1";
                }
            }
        }

        public static short SyGet_OfiUsr(T_MODGUSR MODGUSR, UnitOfWorkCext01 unit, string cencos, string codusr)
        {
            short _retValue = 0;
            try
            {

                string userOfi = unit.SceRepository.EjecutarSP<string>("sce_usr_s09_MS", cencos, codusr).First();
                MODGUSR.UsrEsp.OfixUser = userOfi;

                _retValue = -1;
            }
            catch (Exception e)
            {

            }
            return _retValue;
        }

        //Lee las Fechas de la Tabla de Usuarios : Sce_Usr.-
        public static short SyGetf_Usr(DatosGlobales Globales, UnitOfWorkCext01 unit, string cencos, string codusr, string Etapa)
        {
            using(var tracer = new Tracer("SyGetf_Usr"))
            {
                short _retValue;
                try
                {
                    T_MODGUSR MODGUSR = Globales.MODGUSR;
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
                                    tracer.AddToContext("FechaIni = FechaOut", "Ya se ha efectuado el Cierre Diario de Comercio Exterior. No podrá utilizar esta aplicación.");
                                    throw new ComexUserException("Ya se ha efectuado el Cierre Diario de Comercio Exterior. No podrá utilizar esta aplicación.");
                                }
                                if (FechaIni < FechaFin)
                                {
                                    tracer.AddToContext("FechaIni < FechaFin", "Antes de Operar con las aplicaciones debe ejecutar el Proceso de Inicio de Dia.");
                                    throw new ComexUserException("Antes de Operar con las aplicaciones debe ejecutar el Proceso de Inicio de Dia.");
                                }
                                break;
                        }
                    }
                    _retValue = -1;

                }
                catch(ComexUserException)
                {
                    tracer.AddToContext("ComexUserException SyGetf_Usr", "SyGetf_Usr");
                    throw;
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, se produjo un problema en SyGetf_Usr", ex);
                    tracer.AddToContext("Excepcion SyGetf_Usr", ex.Message);
                    _retValue = 0;
                }
                return _retValue;
            }
        }

        public static string SyGet_RempOrig(DatosGlobales Globales, UnitOfWorkCext01 unit, string cencos, string codusr)
        {
            using(var tracer = new Tracer("SyGet_RempOrig"))
            {
                //****************************************************************************
                //Retorna los Usuarios que se pueden reemplazar.-
                //****************************************************************************
                string _retValue = String.Empty;
                try
                {
                    List<string> reemp = unit.SceRepository.EjecutarSP<string>("sce_usr_s06_MS", cencos, codusr);
                    _retValue = String.Join(",", reemp.ToArray());
                }
                catch (Exception ex)
                {
                    tracer.AddToContext("Excepcion", ex.Message);
                    _retValue = "-1";
                }
                return _retValue;
            }
        }

        public static int VerRegistroUsuario(DatosGlobales Globales,UnitOfWorkCext01 unit, int Valida)
        {
            using(var tracer = new Tracer("VerRegistroUsuario"))
            {
                T_MODGUSR MODGUSR = Globales.MODGUSR;

                int VerRegistroUsuario = 0;

                string UsrOrig = "";
                int X = 0;
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
                    usuario = Globales.DatosUsuario.Identificacion_CCtUsr;
                    if (String.IsNullOrEmpty(usuario))
                    {
                        tracer.AddToContext("Identificación de Usuario", "No está especificado el usuario correspondiente a la Estación de Trabajo.");
                        Globales.MESSAGES.Add(new UI_Message()
                        {
                            Text = "No está especificado el usuario correspondiente a la Estación de Trabajo. Reporte este problema.",
                            Type = TipoMensaje.Error,
                            Title = "Contabilidad Genérica"
                        });
                        return true.ToInt();
                    }
                    X = SyGet_Usr(Globales, unit, usuario.Left(3), usuario.Right(2));
                    if (X == 0)
                    {
                        tracer.AddToContext("SyGet_Usr", "Error al Buscar al Usuario");
                        return true.ToInt();
                    }
                    X = SyGet_OfiUsr(usuario.Left(3), usuario.Right(2), Globales, unit).ToInt();
                    if (X == 0)
                    {
                        tracer.AddToContext("SyGet_OfiUsr", "Error al Buscar la Oficina del Usuario");
                        return true.ToInt();
                    }

                    // Verifica que se haya hecho Inicio de Día Hoy.-
                    if (MODGUSR.UsrEsp.Tipeje == "O")
                    {
                        if (Valida != 0)
                        {
                            X = SyGetf_Usr(Globales, unit, usuario.Left(3), usuario.Right(2), "I");
                            if (X == 0)
                            {
                                tracer.AddToContext("SyGetf_Usr", "Error Relacionado al Inicio del Dia");
                                return true.ToInt();
                            }
                        }
                    }

                    // Identifica Usuario Original.-
                    UsrOrig = Globales.DatosUsuario.Identificacion_CCtUsro;
                    MODGUSR.UsrEsp.CCtOrig = UsrOrig.Left(3);
                    MODGUSR.UsrEsp.EspOrig = UsrOrig.Right(2);

                    // Reemplzaos del Usuario Original.-
                    MODGUSR.UsrEsp.RempOrig = SyGet_RempOrig(Globales, unit, MODGUSR.UsrEsp.CCtOrig, MODGUSR.UsrEsp.EspOrig);


                    return VerRegistroUsuario;

                }
                catch (ComexUserException)
                {
                    throw;
                }
                catch (Exception exc)
                {
                    tracer.TraceException("Alerta: Se produjo un problema en VerRegistroUsuario", exc);
                    tracer.AddToContext("Excepcion VerRegistroUsuario", exc.Message);
                    VerRegistroUsuario = true.ToInt();
                }
                return VerRegistroUsuario;
            }
        }
    }
}
