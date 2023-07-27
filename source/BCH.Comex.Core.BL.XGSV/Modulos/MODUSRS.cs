using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGSV.Modulos
{
    public static class MODUSRS
    {
        // Estructura para leer el usuario en cuestion.
        public struct T_Usrs
        {
            public string CCtUsr;   //Centro de Costo Usuario.
            public string codusr;   //Identificación Usuario.
            public string RutUsr;   //Rut del Usuario.
            public string NomUsr;   //Nombre del Especialista.
            public string CctSup;   //Centro de Costo Supervisor.
            public string CodSup;   //Identificación Supervisor.
        }
        public static sce_usr_s35_MS_Result[] VUsrs = null;
        public const string MsgUsrs = "Identificación de Usuario";
        // ****************************************************************************
        //    1.  Lee los Usuarios ESPECIALISTAS de un determinado Centro Costo.
        //    2.  Retorno    <> 0  : Lectura Exitosa.
        //                   =  0  : Error o Lectura no Exitosa.
        // ****************************************************************************
        public static int SyGetE_Usr(string Centro, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer())
            {
                int SyGetE_Usr = 0;

                try
                {
                    VUsrs = uow.SceRepository.sce_usr_s35_MS(Centro, true).ToArray();
                    if (!VUsrs.Any())
                        return SyGetE_Usr;

                    SyGetE_Usr = 1;
                    return SyGetE_Usr;
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Se ha producido un error al tratar de leer los datos de los Usuarios(Sce_Usr).", ex);
                    return SyGetE_Usr;
                }
            }
        }
        // ****************************************************************************
        //    1.  Lee los Usuarios de un determinado Centro Costo.
        //    2.  Retorno    <> 0  : Lectura Exitosa.
        //                   =  0  : Error o Lectura no Exitosa.
        // ****************************************************************************
        public static int SyGetv_Usr(string Centro, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer())
            {
                int SyGetE_Usr = 0;

                try
                {
                    VUsrs = uow.SceRepository.sce_usr_s35_MS(Centro, false).ToArray();
                    if (!VUsrs.Any())
                        return SyGetE_Usr;

                    SyGetE_Usr = 1;
                    return SyGetE_Usr;
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Se ha producido un error al tratar de leer los datos de los Usuarios(Sce_Usr)", ex);
                    return SyGetE_Usr;
                }
            }
        }
        }
}
