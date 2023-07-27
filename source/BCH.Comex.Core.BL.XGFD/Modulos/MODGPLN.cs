using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FinDia;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XGFD.Modulos
{
    public static class MODGPLN
    {
        /// <summary>
        /// Lee varias Planillas en General tanto Plan. Visibles como Invisibles.
        /// Entrega un listado de Moneda y Montos de las tablas Sce_xPlv y Sce_Pli.
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns> <> 0    : Retorna los datos de la Planilla. =  0    : No existen datos de esa Planilla.</returns>
        public static bool SyGetn_PlnConv(string cencos, string codusr, DateTime fecha, T_MODGPLN modgpln, IList<UI_Message> listaMensajes, UnitOfWorkCext01 uow)
        {
            bool SyGetn_PlnConv = false;

            using(Tracer tracer = new Tracer())
            try
            {

                modgpln.VPlnCon = new List<T_gPlnCon>();
                var Resultados = uow.SceRepository.Sce_Gpln_S12(cencos, codusr, fecha).ToList();

                if (Resultados == null)
                {
                    listaMensajes.Add(new UI_Message()
                        {
                            Text = "Se ha producido un error al tratar de leer las Tablas de Planillas Visibles e Invisibles (Sce_xPl;Sce_Pli)",
                            Type = TipoMensaje.Error
                        });
                }

                foreach (var result in Resultados)
                {
                    modgpln.VPlnCon.Add(
                        new T_gPlnCon() { 
                            codmnd = (int)(result.codmnd??0),
                            MtoEgr = (double)(result.mtoegrt??0),
                            MtoIng = (double)(result.mtoingt ?? 0),
                            MtoDeb = (double)(result.mtodeb ?? 0),
                            MtoHab = (double)(result.mtohab ?? 0),
                            DifDeb = (double)(result.difdeb ?? 0),
                            DifHab = (double)(result.difhab ?? 0)
                        });
                }

                SyGetn_PlnConv = true;

                return SyGetn_PlnConv;
            }
            catch (Exception exc)
            {
                tracer.TraceException("Error en SyGetn_PlnConv", exc);

                listaMensajes.Add(new UI_Message()
                {
                    Text = "Se ha producido un error al tratar de leer las Tablas de Planillas Visibles e Invisibles (Sce_xPl;Sce_Pli)",
                    Type = TipoMensaje.Error
                });
            }
            return SyGetn_PlnConv;
        }
     
    }
}
