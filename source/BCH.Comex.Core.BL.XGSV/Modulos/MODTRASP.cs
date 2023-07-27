using BCH.Comex.Common.Tracing;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XGSV.Modulos
{
    public static class MODTRASP
    {
        // ****************************************************************************
        public const string MgbTraspaso = "Traspaso de Cartera";
        // ****************************************************************************
        // ****************************************************************************
        //    1.  Lee los datos de los Usuarios en relación al traspaso de las Carteras.
        //    2.  Retorno    <> 0  : Lectura Exitosa.
        //                   =  0  : Error o Lectura no Exitosa.
        // ****************************************************************************
        public static IEnumerable<string> SyGetn_Trasp(string CenCos, string CodUsr, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer())
            {
                try
                {
                    var mappings = uow.SceRepository.sce_usr_s01_MS(CenCos, CodUsr);
                    string usersStr = string.Join(";", mappings);

                    var users = usersStr.Split(';').Select(i => i.Trim()).Where(i => !string.IsNullOrEmpty(i));

                    MODUSRS.SyGetv_Usr(string.Empty, uow);

                    var result = new List<string>();
                    foreach (var user in users)
                    {
                        var foundUser = MODUSRS.VUsrs.SingleOrDefault(usr => user.Equals(usr.CCtUsr.Trim() + usr.codusr.Trim()));
                        if (foundUser == null)
                            continue;
                        string Paso = "";
                        Paso = Paso + foundUser.CCtUsr.Trim() + "-" + foundUser.codusr.Trim() + new string(' ', 3);
                        Paso = Paso + foundUser.NomUsr.Trim();

                        result.Add(Paso);
                    }

                    return result;
                }
                catch (Exception exc)
                {
                    tracer.TraceException("Se ha producido un error al tratar de leer los datos de los Traspasos de los Usuarios.", exc);
                    return new string[0];
                }
            }
        }
        // ****************************************************************************
        //    1.  Actualiza una Cartera en relación a un Traspaso de un Especialista.
        //        Retorno    = True  : Grabación Exitosa.
        //                   = False : Error o Grabación no Exitosa.
        // ****************************************************************************
        public static int SyPut_Trasp(string CenCos, string CodUsr, string Reemplazo, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer())
            {
                int SyPut_Trasp = 0;
                try
                {
                    var result = uow.SceRepository.sce_usr_u01_MS(CenCos, CodUsr, Reemplazo);
                    if (result != null)
                        SyPut_Trasp = result.codigo;
                    return SyPut_Trasp;

                }
                catch (Exception exc)
                {
                    tracer.TraceException("Se ha producido un error al tratar de grabar los datos de Traspaso de Cartera.", exc);
                }
                return SyPut_Trasp;
            }
        }
    }
}
