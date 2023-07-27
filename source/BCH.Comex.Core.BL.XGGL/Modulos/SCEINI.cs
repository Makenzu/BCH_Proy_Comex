using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class SCEINI
    {
        public static double SyGet_Ini(DatosGlobales Globales, UnitOfWorkCext01 unit,string grupo, string elem, int flgAviso)
        {
            using(var tracer = new Tracer("SyGet_Ini"))
            {
                double SyGet_Ini = 0.0;

                string r = "";
                string Query = "";

                string Tipo = "";
                string valor = "";
                int largo = 0;
                int decim = 0;


                try
                {
                    sce_ini_s01_MS_Result res = unit.SceRepository.EjecutarSP<sce_ini_s01_MS_Result>("sce_ini_s01_MS", grupo, elem).First();

                    Tipo = res.tipov;
                    largo = res.largo.ToInt();
                    decim = res.decim.ToInt();
                    valor = res.valor;

                    if (Tipo == "N")
                    {
                        SyGet_Ini = valor.ToVal();
                    }
                    else if (Tipo == "C")
                    {
                        SyGet_Ini = valor.ToDbl();
                    }

                    return SyGet_Ini;

                }
                catch (Exception exc)
                {
                    tracer.AddToContext("Excepcion", exc.Message);
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type = Common.UI_Modulos.TipoMensaje.Error,
                        Text = "Se ha producido un error al tratar de leer la tabla sce_ini."
                    });
                }
                return SyGet_Ini;
            }
        }
    }
}
