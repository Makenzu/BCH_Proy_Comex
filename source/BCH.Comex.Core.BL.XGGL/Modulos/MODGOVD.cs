using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODGOVD
    {
        // Tabla de Origen Vía Destino
        // Retorno    <> ""  : Lectura Exitosa.-
        //            =  ""  : Error o Lectura no Exitosa.-
        public static int SyGetn_Ovd(DatosGlobales Globales,UnitOfWorkCext01 unit)
        {
            using(var tracer = new Tracer("SyGetn_Ovd"))
            {
                int SyGetn_Ovd = 0;
                T_MODGOVD MODGOVD = Globales.MODGOVD;
                try
                {

                    var res = unit.SceRepository.EjecutarSP<sce_ovd_s01_Result>("sce_ovd_s01");

                    // Resultado nulo de la Consulta.-
                    if (res.Count == 0)
                    {
                        tracer.AddToContext("Consulta", "Resultado Nulo");
                        return SyGetn_Ovd;
                    }
                    //res.Insert(0, new sce_ovd_s01_Result());

                    MODGOVD.VOvd = res.Select(x => new T_Ovd()
                    {
                        numcta = (int)x.numcta,
                        NomCta = x.nomcta,
                        NemCta = x.nemcta,
                        monnac = x.monnac.ToShort(),
                        CtaOri = x.ctaori.ToInt(),
                        CtaVia = x.ctavia.ToInt(),
                        CtaVue = x.ctavue.ToInt()
                    }).ToArray();

                    SyGetn_Ovd = true.ToInt();
                }
                catch (Exception exc)
                {
                    tracer.AddToContext("Excepcion", exc.Message);
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type = Common.UI_Modulos.TipoMensaje.Error,
                        Text = "Se ha producido un error al tratar de leer la Tabla Origen Vía Destino(Sce_Ovd)."
                    });
                }
                return SyGetn_Ovd;
            }
        }

        //****************************************************************************
        //   1.  Lee las Sucursales para luego cargarlas en un arreglo de Oficinas.
        //   2.  Si el resultado es 1(True)  => Lectura Exitosa.
        //       Si el resultado es 0(False) => Lectura No Exitosa.
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short SyGetn_Suc(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            using(var tracer = new Tracer("SyGetn_Suc"))
            {
                T_MODGOVD MODGOVD = Globales.MODGOVD;
                short _retValue = 0;
                short n = 0;
                string Que = "";
                string R = "";
                short i = 0;
                try
                {
                    n = (short)VB6Helpers.UBound(MODGOVD.Vx_Suc);
                    // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
                    // IGNORED: On Error GoTo 0

                    if (n > 0)
                    {
                        _retValue = (short)(true ? -1 : 0);

                    }
                    else
                    {
                        //Genera Sentencia.

                        //MODXORI.Vx_Suc = new T_Suc[1];
                        var res = unit.SgtRepository.EjecutarSP<sgt_suc_s01_MS_Result>("sgt_suc_s01_MS");
                        res.Insert(0, new sgt_suc_s01_MS_Result());
                        MODGOVD.Vx_Suc = res.Select(x => new T_Suc()
                        {
                            codsuc = (short)x.suc_succod,
                            nomsuc = x.suc_sucnom
                        }).ToArray();

                        _retValue = (short)(true ? -1 : 0);
                    }
                }
                catch (Exception e)
                {
                    tracer.AddToContext("Excepcion", e.Message);
                    _retValue = 0;
                }
                return _retValue;
            }
        }

        // ****************************************************************************
        //    1.  Busca en una estructura determinada el Código de una Sucursal que se
        //        ha ingresado recientemente por pantalla. Si la encuentra, retorna en
        //        el nombre de la función la Identificación de esta (el nombre de la
        //        Sucursal).
        // ****************************************************************************
        public static string Fn_Buscar_Suc(DatosGlobales Globales, string Codigo)
        {
            T_MODGOVD MODGOVD = Globales.MODGOVD;
            string Fn_Buscar_Suc = "";

            int i = 0;
            int fin = 0;

            fin = -1;
            fin = MODGOVD.Vx_Suc.GetUpperBound(0);
            
            for (i = 0; i <= fin; i += 1)
            {
                if (MODGOVD.Vx_Suc[i].codsuc == Codigo.ToVal())
                {
                    Fn_Buscar_Suc = MODGOVD.Vx_Suc[i].nomsuc.TrimB();
                    break;
                }
            }

            return Fn_Buscar_Suc;
        }

        // ****************************************************************************
        //    1.  Lee las tablas de Sce_Prty de Sce_Rsa para verificar si existe un
        //        party determinado, y si existe, rescatar la identicicación de este.
        //    2.  Si el resultado es 1(True)  => Lectura Exitosa.
        //        Si el resultado es 0(False) => Lectura No Exitosa.
        // ****************************************************************************
        public static string SyGet_Partys(DatosGlobales Globales,UnitOfWorkCext01 unit, string Party)
        {
            string SyGet_Partys = "";

            string R = "";
            string Que = "";

            try
            {
                var res = unit.SceRepository.EjecutarSP<string>("sce_prty_s02", MODGSYB.dbcharSy(MODGPYF1.Fn_FormateaPrty(Party.TrimB())));
                
                

                // Se realizó el Query pero la consulta no retornó datos.
                if (res.Count==0)
                {
                    return SyGet_Partys;
                }

                SyGet_Partys = res.First();

                return SyGet_Partys;

            }
            catch (Exception exc)
            {
                Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                {
                    Text= "Se ha producido un error al tratar de leer los datos de los Partys.",
                    Type=Common.UI_Modulos.TipoMensaje.Error
                });

            }
            return SyGet_Partys;
        }
    }
}
