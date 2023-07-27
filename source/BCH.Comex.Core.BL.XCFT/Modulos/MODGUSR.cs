using BCH.Comex.Common.Exceptions;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODGUSR
    {
        public static T_MODGUSR GetMODGUSR() {
            return new T_MODGUSR();
        }

        //Lee la Tabla de Usuarios : Sce_Usr.-
        public static short SyGet_Usr(T_MODGUSR MODGUSR, UI_Mdi_Principal Mdi_Principal,UnitOfWorkCext01 unit, string cencos, string codusr)
        {
            using (var tracer = new Tracer())
            {
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
                    Mdi_Principal.MESSAGES.Add(new UI_Message());
                }
                _retValue = -1;
            }
            catch (Exception ex)
            {
                    tracer.TraceException("Alerta", ex);
                _retValue = 0;
            }
            return _retValue;
        }
        }

        //Retorna los Reemplazos de un Usuario.-
        public static string SyGet_Remp(T_MODGUSR MODGUSR,UnitOfWorkCext01 unit, string codcct, string codesp)
        {
            using (var tracer = new Tracer())
            {
            try
            {
                List<string> reemp = unit.SceRepository.EjecutarSP<string>("sce_usr_s06_MS", codcct, codesp);
                return String.Join(",", reemp.ToArray());
            }
            catch (Exception ex)
            {
                    tracer.TraceException("Alerta", ex);
                return "-1";
            }
        }
        }

        public static short SyGet_OfiUsr(T_MODGUSR MODGUSR,UnitOfWorkCext01 unit, string cencos, string codusr)
        {
            using (var tracer = new Tracer())
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
                    tracer.TraceException("Alerta", e);
            }
            return _retValue;
        }
        }

        /// <summary>
        /// Valida que el usuario haga hecho inicio de dia, y no haya hecho fin de dia. Desde la tabla sce_usr
        /// </summary>
        /// <param name="MODGUSR"></param>
        /// <param name="Mdi_Principal"></param>
        /// <param name="unit"></param>
        /// <param name="cencos"></param>
        /// <param name="codusr"></param>
        /// <param name="etapa"></param>
        /// <returns></returns>
        public static short SyGetf_Usr(T_MODGUSR MODGUSR, UI_Mdi_Principal Mdi_Principal, UnitOfWorkCext01 unit,
            string cencos, string codusr, string etapa)
        {
            using (var tracer = new Tracer())
            {
                short _retValue;
                var fechas = new sce_usr_s04_MS_Result();

                try
                {
                    fechas = unit.UsuarioRepository.GetFechasUsuario(cencos, codusr);
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta, no se ha podido obtener las fechas de incio y fin de dia del usuario", ex);

                    return _retValue = 0;
                }

                if (fechas == null)
                {
                    throw new ComexUserException("No fue posible saber si el usuario hizo inicio o fin de día");
                }

                //Rescata los datos.-
                DateTime FechaIni = fechas.fec_ini;
                DateTime FechaFin = fechas.fec_fin;
                DateTime FechaOut = fechas.fec_out;

                tracer.AddToContext("fec_ini", fechas.fec_ini);
                tracer.AddToContext("fec_fin", fechas.fec_fin);
                tracer.AddToContext("fec_out", fechas.fec_out);
                tracer.AddToContext("Especialista", MODGUSR.UsrEsp.Especialista);
                tracer.AddToContext("EspOrig", MODGUSR.UsrEsp.EspOrig);

                if (MODGUSR.UsrEsp.Especialista != "00")
                {
                    switch (etapa)
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
                _retValue = -1;

                return _retValue;
            }
        }

        public static string SyGet_RempOrig(T_MODGUSR MODGUSR, UnitOfWorkCext01 unit, string cencos, string codusr)
        {
            using (var tracer = new Tracer())
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
                    tracer.TraceException("Alerta en SyGet_RempOrig", ex);
                    _retValue = "-1";
                }
                return _retValue;
            }
        }
     }
}
