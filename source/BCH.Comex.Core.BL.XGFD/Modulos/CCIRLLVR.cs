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
    internal static class CCIRLLVR
    {
        /// <summary>
        /// Muestra listado de vencimientos de los rollover del especialista
        /// </summary>
        /// <param name="CentroCosto"></param>
        /// <param name="especialista"></param>
        /// <param name="ListaErrores"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public static int SyGet_jAcp(string CentroCosto, string especialista, IList<UI_Message> ListaErrores, UnitOfWorkCext01 uow)
        {
            using (var trace = new Tracer("SyGet_jAcp"))
            {
                int SyGet_jAcp = 0;
                try
                {
                    SyGet_jAcp = uow.SceRepository.scejacp_s07_MS(CentroCosto, especialista, DateTime.Today);
                }
                catch (Exception exc)
                {
                    ListaErrores.Add(new UI_Message() { Text = "Error al leer la cantidad de aceptaciones de corto plazo vencidas.", Type = TipoMensaje.Error });
                    trace.TraceException("Alerta, no se ha podido obtener la lista de aceptaciones de corto plazo vencidas", exc);
                }
                return SyGet_jAcp;
            }            
        }

        /// <summary>
        /// Muestra listado de vencimientos de los rollover del especialista
        /// </summary>
        /// <param name="CentroCosto"></param>
        /// <param name="especialista"></param>
        /// <param name="CC"></param>
        /// <param name="ListaErrores"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public static int SyGet_jAcp2(string CentroCosto, string especialista, T_CCIRLLVR CC, IList<UI_Message> ListaErrores, UnitOfWorkCext01 uow)
        {
            int SyGet_jAcp2 = 0;
            using (Tracer tracer = new Tracer("SyGet_jAcp2"))
            {
                try
                {
                    CC.VjAcp = new List<T_jAcp>();
                    List<scejacp_s05_MS_Result> resultado = uow.SceRepository.scejacp_s05_MS(CentroCosto, especialista, DateTime.Today).ToList();
                    foreach (scejacp_s05_MS_Result item in resultado)
                    {
                        CC.VjAcp.Add(new T_jAcp()
                        {
                            codcct = item.codcct,
                            codesp = item.codesp,
                            codpro = item.codpro,
                            codofi = item.codofi,
                            codope = item.codope,
                            numneg = (int)item.numneg,
                            numacp = (int)item.numacp,
                            refere = item.codcct + "-" + item.codpro + "-" + item.codesp + "-" + item.codofi + "-" + item.codope + "-" + item.numneg.ToString().Trim() + "-" + item.numacp.ToString().Trim(),
                            monacp = (int)item.monacp,
                            salacp = (double)item.salacp,
                            venacp = item.venacp.ToShortDateString(),
                            rollover = item.rollover,

                            monacpnemonico = MODGTAB0.Get_NemMnd(new T_MODGTAB0(), uow, (short)item.monacp)
                        });
                    }
                    return SyGet_jAcp2;
                }
                catch (Exception exc)
                {
                    ListaErrores.Add(new UI_Message() { Text = "Error al leer la cantidad de aceptaciones de corto plazo vencidas.", Type = TipoMensaje.Error });
                    tracer.TraceException("Alerta, no se ha podido obtener la lista de aceptaciones de corto plazo vencidas", exc);
                }
            }
            return SyGet_jAcp2;
        }
    }
}
