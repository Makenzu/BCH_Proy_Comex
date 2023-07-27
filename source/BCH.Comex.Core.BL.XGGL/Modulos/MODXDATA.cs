using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    /// <summary>
    /// Clase estatica creada por @emiliano que representa la logica para ejecutar el BATCH en el Grabar
    /// </summary>
    public static class MODXDATA
    {
        public static void Cmd_Put(DatosGlobales Globales, Func<int> p)
        {
            Globales.MODXDATA.CmdsQuerysNew.Add(p);
        }

        public static int Cmd_Exe(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            using(var tracer = new Tracer())
            {
                int r = 0;
                int err = 999;
                try
                {
                    unit.BeginTransaction();
                    //comienza transaccion
                    for (short i = 0; i < Globales.MODXDATA.CmdsQuerysNew.Count; i++)
                    {
                        r = Globales.MODXDATA.CmdsQuerysNew.ElementAt(i)();
                        if (r != 0)
                        {
                            //rollback
                            err = i;
                            throw new Exception();
                        }
                    }
                    unit.Commit();
                    err = 0;
                }
                catch (Exception e)
                {
                    tracer.AddToContext("Cmd_Exe", "Error en Procedimiento " + err + ": " + e.Message);
                    unit.Rollback();
                }
                finally
                {
                    unit.EndTransaction();
                }
                return err;
            }
        }
    }
}
