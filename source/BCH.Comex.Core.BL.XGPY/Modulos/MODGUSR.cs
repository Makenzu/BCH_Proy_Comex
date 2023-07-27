//using BCH.Comex.Common.XGPY.Datatypes;
//using BCH.Comex.Common.XGPY.T_Modulos;
using BCH.Comex.Common.Exceptions;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGPY.Modulos
{
    public static class MODGUSR
    {
        public static T_MODGUSR.EstrucUsuarios UsrEsp = new T_MODGUSR.EstrucUsuarios();
        public const string MsgUsr = "Identificación de Usuario.";

        public static Boolean VerRegistroUsuario(
            Int32 Valida, 
            String centroCosto, 
            String codigoUsuario, 
            String centroCostoOriginal, 
            String codigoUsuarioOriginal,
            ref InitializationObject iO,
            XgpyService xS)
        {
            string UsrOrig = "";
            int x = 0;
            string usuario = "";
            const string Validos = "0123456789K";
            const string FormaRut = "@@@.@@@.@@@-@";
            string LeRut = "";
            string rut = "";
            string Aux = "";
            int Ind = 0;
            string BaseUser = "";
            string Selec = "";
            int Cuantos = 0;
            int Operando = 0;
            string LeRutOtro = "";

            // ok, preparamos para posible error.-
            try
            {
                if (SyGet_Usr(centroCosto, codigoUsuario, ref iO, xS))
                {
                    x = SyGet_OfiUsr(centroCosto, codigoUsuario, ref iO, xS) ? 1 : 0;
                    if (x == 0)
                    {
                        return false ;
                    }
                }
                

                // Verifica que se haya hecho Inicio de Día Hoy.-
                if (UsrEsp.Tipeje == "O")
                {
                    if (Valida != 0)
                    {
                        x = SyGetf_Usr(centroCosto, codigoUsuario, "I", xS) ? 1 : 0;
                        if (x == 0)
                        {
                            return false; ;
                        }
                    }
                }

                // Identifica Usuario Original.-
                UsrEsp.CCtOrig = centroCostoOriginal;
                UsrEsp.EspOrig = codigoUsuarioOriginal;

                iO.UsrEsp = UsrEsp;

                // Reemplzaos del Usuario Original.-
                UsrEsp.RempOrig = SyGet_RempOrig(UsrEsp.CCtOrig, UsrEsp.EspOrig, xS);

                return true;

            }
            catch {
            
            
            
            }

            return false;
        }

        public static Boolean SyGet_Usr(
            string Cencos, 
            string CodUsr,
            ref InitializationObject iO,
            XgpyService xS)
        {
            try
            {
                var resSceUsr = xS.Sce_Usr_S05_MS(Cencos, CodUsr);

                UsrEsp = iO.UsrEsp;

                UsrEsp.rut = resSceUsr.rut;
                UsrEsp.Jerarquia = (Int32)resSceUsr.jerarquia;
                UsrEsp.CentroCosto = resSceUsr.cent_costo;
                UsrEsp.Especialista = resSceUsr.id_especia;
                UsrEsp.Delegada = resSceUsr.delegada ? 1 : 0;
                UsrEsp.CostoSuper = resSceUsr.cent_super;
                UsrEsp.Id_Super = resSceUsr.id_super;
                UsrEsp.Nombre = resSceUsr.nombre;
                UsrEsp.Direccion = resSceUsr.direccion;
                UsrEsp.comuna = resSceUsr.comuna;
                UsrEsp.Ciudad = resSceUsr.ciudad;
                UsrEsp.Seccion = resSceUsr.seccion;
                UsrEsp.Oficina = (Int32)resSceUsr.ofic_orige;
                UsrEsp.Telefono = resSceUsr.telefono;
                UsrEsp.swift = resSceUsr.swift;
                UsrEsp.Fax = resSceUsr.fax;
                UsrEsp.Tipeje = resSceUsr.tipeje;

                iO.UsrEsp = UsrEsp;

                if (SyGet_Remp(UsrEsp.CentroCosto, UsrEsp.Especialista, xS) == null)
                {
                    return false;
                }

                return true;

            }
            catch { }
     
            return false;
        }

        public static Boolean SyGet_OfiUsr(string Cencos, string CodUsr, ref InitializationObject iO, XgpyService xS)
        {
            int sceUsrCount;
            try
            {
                var resSceUsr = xS.Sce_Usr_S09_MS(Cencos, CodUsr);
                sceUsrCount = resSceUsr.Count();

                if(sceUsrCount == 0)
                {
                    return false;
                }

                UsrEsp = iO.UsrEsp;

                UsrEsp.OfixUser = resSceUsr;//MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", R).ToStr();

                iO.UsrEsp = UsrEsp;

                return true;

            }
            catch { }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cencos"></param>
        /// <param name="CodUsr"></param>
        /// <param name="Etapa"></param>
        /// <param name="xS"></param>
        /// <returns></returns>
        public static Boolean SyGetf_Usr(string Cencos, string CodUsr, string Etapa, XgpyService xS)
        {
            string msj = string.Empty;
            return SyGetf_Usr(Cencos, CodUsr, Etapa, xS, ref msj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cencos"></param>
        /// <param name="CodUsr"></param>
        /// <param name="Etapa"></param>
        /// <param name="xS"></param>
        /// <returns></returns>
        public static Boolean SyGetf_Usr(string Cencos, string CodUsr, string Etapa, XgpyService xS, ref string mensaje)
        {
            using (var tracer = new Tracer())
            {
                Int32 sceUsrCount;
                DateTime FechaIni;
                DateTime FechaFin;
                DateTime FechaOut;

                var resSceUsr = xS.Sce_Usr_S04_MS(Cencos, CodUsr);
                sceUsrCount = resSceUsr.Count();
                if (sceUsrCount == 0)
                {
                    return false;
                }

                // Rescata los datos.-
                FechaIni = resSceUsr[0].fec_ini;
                FechaFin = resSceUsr[0].fec_fin;
                FechaOut = resSceUsr[0].fec_out;

                if (UsrEsp.Especialista != "00")
                {
                    switch (Etapa)
                    {
                        case "I":
                            if (DateTime.Compare(FechaIni, FechaOut) == 0)
                            {
                                mensaje = "Ya se ha efectuado el Cierre Diario de Comercio Exterior. No podrá utilizar esta aplicación.";
                                tracer.TraceWarning(mensaje);
                                //MigrationSupport.Utils.MsgBox("Ya se ha efectuado el Cierre Diario de Comercio Exterior. No podrá utilizar esta aplicación.", UTILES.pito(64).Cast<MigrationSupport.MsgBoxStyle>(), MODGUSR.MsgUsr);
                                return true;
                            }
                            if (DateTime.Compare(FechaIni, FechaFin) < 0)
                            {
                                mensaje = "Antes de Operar con las aplicaciones debe ejecutar el Proceso de Inicio de Dia.";
                                tracer.TraceWarning(mensaje);
                                //MigrationSupport.Utils.MsgBox("Antes de Operar con las aplicaciones debe ejecutar el Proceso de Inicio de Dia.", UTILES.pito(64).Cast<MigrationSupport.MsgBoxStyle>(), MODGUSR.MsgUsr);
                                return true;
                            }
                            break;
                    }
                }
                return false;
            }
        }

        public static string SyGet_Remp(string codcct, string codesp, XgpyService xS)
        {
            String retReemplazos = "";

            try
            {
                var resSceUsr = xS.Sce_Usr_S06_MS(codcct, codesp);
                for (int i = 0; i < resSceUsr.Count(); i++)
                {
                    if (String.IsNullOrEmpty(retReemplazos))
                    {
                        retReemplazos = resSceUsr[i];
                    }
                    else
                    {
                        retReemplazos = resSceUsr[i] + ";" + retReemplazos;
                    }
                }


                return retReemplazos;

            }
            catch { }

            return null;
        }

        public static String SyGet_RempOrig(string Cencos, string CodUsr, XgpyService xS)
        {
            String retReemplazos = "";

            try
            {
                var resSceUsr = xS.Sce_Usr_S06_MS(Cencos, CodUsr);
                for (int i = 0; i < resSceUsr.Count(); i++)
                {
                    if (String.IsNullOrEmpty(retReemplazos))
                    {
                        retReemplazos = resSceUsr[i];
                    }
                    else
                    {
                        retReemplazos = resSceUsr[i] + ";" + retReemplazos;
                    }
                }

                return retReemplazos;

            }
            catch { }

            return null;
        }
    }


}
