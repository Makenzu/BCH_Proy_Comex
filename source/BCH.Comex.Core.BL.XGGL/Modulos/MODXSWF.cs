using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODXSWF
    {
        //Graba los Swift's de una Operación.-
        //Retorno    = True  : Grabación Exitosa.-
        //           = False : Error o Grabación no Exitosa.-
        public static short SyPutn_Swf(DatosGlobales Globales, UnitOfWorkCext01 unit, string NumOpe, string CodAnu, string Usuario)
        {
            using (var tracer = new Tracer())
            {
                T_MODGSWF MODGSWF = Globales.MODGSWF;
                T_MODXSWF MODXSWF = Globales.MODXSWF;
                

                short _retValue = 0;
                short i = 0;
                int m = 0;
                short c = 0;
                int n = 0;
                string R = "";
                short HayError = 0;
                try
                {

                    for (i = 0; i < (short)MODGSWF.VSwf.Count(); i++)
                    {
                        //Ingresa el Swift como memo.-
                        m = MODGMEM.SyPutn_Mem(Globales, unit, "s", 0, MODGSWF.VSwf[i].DocSwf);
                        if (m == 0)
                        {
                            tracer.TraceError("No se pudo ingresar el Documento Swift (Sce_MemS).");

                            Globales.MESSAGES.Add(new UI_Message()
                            {
                                Text = "No se pudo ingresar el Documento Swift (Sce_MemS).",
                                Type = TipoMensaje.Error,
                                Title= T_MODGCHQ.MsgDocVal
                            });
                            return _retValue;
                        }

                        //Correlativo del Swift.-
                        if (c == 0)
                        {
                            c = (short)(SyGetc_Swf(Globales, unit, NumOpe) + 1);
                        }
                        else
                        {
                            c++;
                        }

                        n = MODXSWF.VxSwfGen.GetUpperBound(0) + 1;
                        Array.Resize(ref MODXSWF.VxSwfGen, n + 1);
                        MODXSWF.VxSwfGen[n] = m;

                        List<string> parameters = new List<string>();
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)));
                        parameters.Add(MODGSYB.dbnumesy(c));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Left(Usuario, 3)));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Right(Usuario, 2)));
                        //-----------------------------
                        //Determina el Tipo de Swift.
                        if (MODGSWF.VSwf[i].BenSwf.EsBanco)
                        {
                            parameters.Add(MODGSYB.dbnumesy(T_MODGSWF.MT_202));
                        }
                        else
                        {
                            parameters.Add(MODGSYB.dbnumesy(T_MODGSWF.MT_103));
                        }

                        //-----------------------------
                        parameters.Add(DateTime.Now.ToString("yyyy-MM-dd"));
                        parameters.Add(MODGSYB.dbcharSy(CodAnu));  //Arreglar #Can.-
                        parameters.Add(MODGSYB.dbnumesy(m));

                        int resOpe = -1;
                        unit.SceRepository.ReadQuerySP((reader) =>
                        {
                            if (reader.Read())
                            {
                                resOpe = reader.GetInt32(0);
                            }
                            else
                            {
                                resOpe = -1;
                            }
                        }, "sce_swf_i01", parameters.ToArray());

                        if (resOpe == -1)
                        {
                            tracer.TraceError("Se ha producido un error al tratar de grabar un Swift de una Operación.");
                            Globales.MESSAGES.Add(new UI_Message()
                            {
                                Text = "Se ha producido un error al tratar de grabar un Swift de una Operación.",
                                Type = TipoMensaje.Error,
                                Title= T_MODGCHQ.MsgDocVal
                            });
                            HayError = -1;
                        }
                    }

                    if (~HayError != 0)
                    {
                        _retValue = (short)(true ? -1 : 0);
                    }


                }
                catch (Exception _ex)
                {
                    tracer.TraceException("Alerta, no se ha podido grabar un Swift de una Operación: ", _ex);

                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Se ha producido un error al tratar de grabar un Swift de una Operación.",
                        Type = TipoMensaje.Error,
                        Title = T_MODGCHQ.MsgDocVal
                    });
                    _retValue = 0;
                }
                return _retValue;
            }
        }

        //Retorna el último correlativo de Swift para una Operación.-
        public static short SyGetc_Swf(DatosGlobales Globales, UnitOfWorkCext01 unit, string NumOpe)
        {
            using (var tracer = new Tracer())
            {
                short _retValue = 0;
                try
                {
                    // IGNORED: On Error GoTo SyGetc_SwfErr
                    List<string> parameters = new List<string>();
                    //Genera Sentencia.-
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)));
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)));
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)));
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)));
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)));


                    decimal? maxNroCorrelativo = unit.SceRepository.EjecutarSP<decimal?>("sce_swf_s01", parameters.ToArray()).FirstOrDefault();
                    if (maxNroCorrelativo.HasValue)
                    {
                        return (short)maxNroCorrelativo.Value;
                    }
                    else return 0;
                }
                catch (Exception _ex)
                {
                    tracer.TraceException("Alerta, no se ha podido obtener el ultimo correlativo swift, llamdo a sce_swf_s01", _ex);

                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Se ha producido un error al tratar de leer el último correlativo del Swift.",
                        Type = TipoMensaje.Error,
                        Title= T_MODGCHQ.MsgDocVal
                    });

                    _retValue = 0;
                }
                return _retValue;
            }
        }

        //Procedimiento que imprime los Swift's generados.-
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static void Print_xSwf(DatosGlobales Globales, UnitOfWorkCext01 unit, short Copias)
        {
            T_MODXSWF MODXSWF = Globales.MODXSWF;
            foreach (var swiftGenerado in MODXSWF.VxSwfGen)
            {
                for (int j = 1; j <= (short)Copias; j++)
                {
                    if (j == 1)
                    {
                        var di = "Impresion/ImpresionDeDocumentos/DetalleSwiftOriginalCopia?nroMensaje=" + swiftGenerado + "&replace=S W I F T&with=S W I F T             (ORIGINAL)";
                        Globales.DocumentosAImprimir.Add(di);
                    }
                    else
                    {
                        var di = "Impresion/ImpresionDeDocumentos/DetalleSwiftOriginalCopia?nroMensaje=" + swiftGenerado + "&replace=S W I F T&with=S W I F T             (COPIA)";
                        Globales.DocumentosAImprimir.Add(di);
                    }
                }
            }
        }
    }
}
