using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public static class MODXSWF
    {
        public static T_MODXSWF GetMODXSWF()
        {
            return new T_MODXSWF();
        }
        //Graba los Swift's de una Operación.-
        //Retorno    = True  : Grabación Exitosa.-
        //           = False : Error o Grabación no Exitosa.-
        public static short SyPutn_Swf(InitializationObject initObject,UnitOfWorkCext01 unit, string NumOpe, string CodAnu, string Usuario)
        {
            using (var tracer = new Tracer("Graba Swift de operacion: SyPutn_Swf"))
            {
                T_MODGSWF MODGSWF = initObject.MODGSWF;
                T_MODXSWF MODXSWF = initObject.MODXSWF;
                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;

                short _retValue = 0;
                short i = 0;
                int m = 0;
                short c = 0;
                short n = 0;
                string R = "";
                short HayError = 0;
                try
                {
                    // Si no tiene datos Swift, sale.
                    if (MODGSWF.VSwf.Length == 0)
                        initObject.MODSWENN.Hab_Swift = 0;

                    for (i = 0; i <= (short)VB6Helpers.UBound(MODGSWF.VSwf); i++)
                    {

                        //Ingresa el Swift como memo.-
                        m = MODGMEM.SyPutn_Mem(initObject, unit, "s", 0, MODGSWF.VSwf[i].DocSwf);
                        if (m == 0)
                        {
                            tracer.TraceError("No se pudo ingresar el Documento Swift (Sce_MemS).");

                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Text = "No se pudo ingresar el Documento Swift (Sce_MemS).",
                                Type = TipoMensaje.Error
                            });
                            return _retValue;
                        }

                        //Correlativo del Swift.-
                        if (c == 0)
                        {
                            c = (short)(SyGetc_Swf(initObject, unit, NumOpe) + 1);
                        }
                        else
                        {
                            c++;
                        }

                        MODXSWF.VxSwfGen.Add(new T_MODXSWF.SwiftGenerado()
                        {
                            NroOperacion = c.ToString(),
                            CodMem = m
                        });

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
                            tracer.TraceError("Alerta, no se ha podido grabar un Swift de una Operación.");
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Text = "Se ha producido un error al tratar de grabar un Swift de una Operación.",
                                Type = TipoMensaje.Error
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
                    tracer.TraceException("Alerta, no se a podido grabar un Swift de una Operación: ", _ex);

                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Se ha producido un error al tratar de grabar un Swift de una Operación.",
                        Type = TipoMensaje.Error
                    });
                    _retValue = 0;
                }
                return _retValue;
            }
        }

        //Retorna el último correlativo de Swift para una Operación.-
        public static short SyGetc_Swf(InitializationObject initObject,UnitOfWorkCext01 unit, string NumOpe)
        {
            using (var tracer = new Tracer())
            {

                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;

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

                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Se ha producido un error al tratar de leer el último correlativo del Swift.",
                        Type = TipoMensaje.Error
                    });

                    _retValue = 0;
                }
                return _retValue;
            }
        }

        //Procedimiento que imprime los Swift's generados.-
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static void Print_xSwf(InitializationObject initObject,UnitOfWorkCext01 unit, short Copias)
        {
            short n = 0;
            short j = 0;
            short i = 0;
            T_MODXSWF MODXSWF = initObject.MODXSWF;
            
            foreach (var swiftGenerado in MODXSWF.VxSwfGen)
            {
                for (j = 0; j <= (short)Copias; j++)
                {
                    if (j == 0)
                    {
                        var di = new DataImpresion()
                        {
                            URL = "Impresion/ImpresionDeDocumentos/DetalleSwiftOriginalCopia?nroMensaje=" + swiftGenerado.CodMem + "&replace=S W I F T&with=S W I F T             (ORIGINAL)",
                            nroMensaje = swiftGenerado.CodMem.ToString(),
                            replace = "S W I F T",
                            with = "S W I F T             (ORIGINAL)",
                            tipoDoc = 4, //swiftOriginalCopia
                            fileName =initObject.MODGCVD.VgCvd.OpeSin
                        };
                        initObject.DocumentosAImprimir.Add(di);
                    }
                    else
                    {
                        var di = new DataImpresion()
                        {
                            URL = "Impresion/ImpresionDeDocumentos/DetalleSwiftOriginalCopia?nroMensaje=" + swiftGenerado.CodMem + "&replace=S W I F T&with=S W I F T             (COPIA)",
                            nroMensaje = swiftGenerado.CodMem.ToString(),
                            replace = "S W I F T",
                            with = "S W I F T             (COPIA)",
                            tipoDoc = 4, //swiftOriginalCopia
                            fileName = initObject.MODGCVD.VgCvd.OpeSin
                        };
                        initObject.DocumentosAImprimir.Add(di);
                    }
                }
            }
        }

        //****************************************************************************
        //   1.  En conjunto con el número de la operación y el arreglo de los
        //       correlativos del Switf ya generados, se realiza la búsqueda de los
        //       códigos de memo, con los cuales se obtiene el Memo Swift para su
        //       posterior impresión.
        //****************************************************************************
        public static string SyGet_xSwf(InitializationObject initObject, UnitOfWorkCext01 unit, string NumOpe, short Correlativo)
        {
            using (var tracer = new Tracer())
            {

                string _retValue = "";
                string Que = "";
                string R = "";
                decimal e = 0;
                string s = "";
                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;
                try
                {
                    e = unit.SceRepository.EjecutarSP<decimal>("sce_swf_s02_MS",
                        MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)),
                        MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)),
                        MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)),
                        MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)),
                        MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)),
                        MODGSYB.dbnumesy(Correlativo)).First();

                    s = MODGMEM.SyGetn_Mem(initObject, unit, "s", (int)e);
                    _retValue = s;

                }
                catch (Exception _ex)
                {
                    tracer.TraceException("Alerta al llamar a sce_swf_s02_MS: ", _ex);

                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error al tratar de leer el código del memo asociado a un Swift. Reporte este problema."
                    });
                }
                return _retValue;
            }
        }
    }
}
