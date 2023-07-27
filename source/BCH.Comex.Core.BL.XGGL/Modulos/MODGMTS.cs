using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODGMTS
    {
        // ****************************************************************************
        //    1.  Lee la tabla de Códigos de Campos Swift cargándolos en un arreglo.
        // ****************************************************************************
        public static int SyGetn_VCcs(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            using(var tracer = new Tracer("SyGetn_VCcs"))
            {
                T_MODGMTS MODGMTS = Globales.MODGMTS;
                int SyGetn_VCcs = 0;
                try
                {

                    MODGMTS.VCCs = new T_VCcs[1];
                    var res = unit.SceRepository.EjecutarSP<sce_tccs_s04_Result>("sce_tccs_s04");
                    if (res.Count == 0)
                    {
                        tracer.AddToContext("Campos", "No se han encontrado datos en la Tabla de Códigos de Campos de Swift");
                        Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                        {
                            Type = Common.UI_Modulos.TipoMensaje.Error,
                            Text = "No se han encontrado datos en la Tabla de Códigos de Campos de Swift (Sce_Tccs).",
                            Title = T_MODGSWF.MsgSwf
                        });
                        return SyGetn_VCcs;
                    }
                    res.Insert(0, new sce_tccs_s04_Result());
                    MODGMTS.VCCs = res.Select(x => new T_VCcs()
                    {
                        codmt = x.codmt.ToInt(),
                        codcam = x.codcam,
                        DesCam = x.descam,
                        CamMan = x.camman.ToInt(),
                        numlin = x.numlin.ToInt(),
                        lenlin = x.lenlin.ToInt(),
                        LenTot = x.lentot.ToInt()
                    }).ToArray();


                    SyGetn_VCcs = true.ToInt();
                    return SyGetn_VCcs;

                }
                catch (Exception exc)
                {
                    tracer.AddToContext("Excepcion", exc.Message);
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type = Common.UI_Modulos.TipoMensaje.Error,
                        Text = "Se ha producido un error al tratar de leer la Tabla de Códigos de Campos de Swift (Sce_Tccs).",
                        Title = T_MODGSWF.MsgSwf
                    });
                }
                return SyGetn_VCcs;
            }
        }
    }
}
