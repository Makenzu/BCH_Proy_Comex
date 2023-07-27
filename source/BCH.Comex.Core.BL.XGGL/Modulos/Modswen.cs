using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.SWI200;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Swift;
using CodeArchitects.VB6Library;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using BCH.Comex.Common.Tracing;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class Modswen
    {
        public static int Habil_SWIFT(DatosGlobales Globales,UnitOfWorkCext01 unit, string Producto)
        {
            int Habil_SWIFT = 0;
            
            int habil = 0;

            habil = false.ToInt();

            habil = SCEINI.SyGet_Ini(Globales,unit, "swifttodo", "0", false.ToInt()).ToInt();

            if (habil == 0)
            {
                habil = SCEINI.SyGet_Ini(Globales, unit, "swiftprod", Producto, false.ToInt()).ToInt();
            }

            if (habil == 0)
            {
                habil = SCEINI.SyGet_Ini(Globales, unit, "swiftesp", String.Empty, false.ToInt()).ToInt();
            }

            Habil_SWIFT = habil;


            return Habil_SWIFT;
        }

        public static bool Fn_Save_BaseSwft(DatosGlobales Globales, UnitOfWorkCext01 unit, string numneg, string tippro, string NumCpa, string numcuo, string NumCob)
        {
            using (var tracer = new Tracer())
            {
                T_MODGSWF MODGSWF = Globales.MODGSWF;
                T_Modswen Modswen = Globales.Modswen;

                bool Fn_Save_BaseSwft = false;
                
                int TipMT = 0;
                string s = "";
                string TipG = "";
                int i = 0;
                int l = 0;

                string CuerSw = "";
                string EncaSw = "";
                string Corr = "";
                try
                {
                    // Guarda el Texto del Swift
                    // Guarda el Encabezado del Swift
                    l = MODGSWF.VSwf.GetUpperBound(0);

                    //Modswen.RutAis = Fn_RutdeAis();
                    Modswen.RutAis = Globales.DatosUsuario.Identificacion_Rut;
                    if (Modswen.hab_swift != 0)
                    {
                        for (i = 0; i <= l; i += 1)
                        {
                            CuerSw = MODSWEN1.Fn_GenSwiftEnvio(Globales, unit, i);
                            Corr = Fn_Sw_GetCorr(Globales).ToString();     // Busco Correlativo en FLIO
                            if (Corr == "-1")
                            {
                                Globales.MESSAGES.Add(new UI_Message()
                                {
                                    Text = "No hay comunicación con el Servidor de Swift. El swift generado quedara grabado localmente.",
                                    Type = TipoMensaje.Error,
                                    Title = "SWIFT"
                                });
                                TipG = "L";     // Grabo Local
                                EncaSw = MODSWEN1.Fn_EncSwiLoc(Globales, i);
                                s = EncaSw + CuerSw;
                                if (!Fn_SwiftLocal(Globales, unit, s,i))
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "Se ha producido un Error al grabar el Swift Localmente. El swift deberá ser digitado en la aplicación Envio de Swift.",
                                        Title = "SWIFT",
                                        Type = TipoMensaje.Error
                                    });
                                }
                            }
                            else
                            {
                                TipG = "R";     // GraboRemoto
                                EncaSw = MODSWEN1.Fn_EncSwi(Globales, i, Modswen.RutAis.TrimB(), Corr);
                                s = EncaSw + CuerSw;
                                if (!Fn_Sw_PutSw(Globales, unit, Globales.MODGSWF.VSwf[i], CuerSw, Corr.ToInt()))
                                {
                                    TipG = "L";     // Grabo Local
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Title = "SWIFT",
                                        Text = "El Mensaje Swift generado, quedará grabado localmente.",
                                        Type = TipoMensaje.Error
                                    });
                                    EncaSw = MODSWEN1.Fn_EncSwiLoc(Globales, i);
                                    s = EncaSw + CuerSw;
                                    if (!Fn_SwiftLocal(Globales, unit, s, i))
                                    {
                                        Globales.MESSAGES.Add(new UI_Message()
                                        {
                                            Text = "Se ha producido un Error al grabar el Swift Localmente. El swift deberá ser digitado en la aplicación Envio de Swift.",
                                            Title = "SWIFT",
                                            Type = TipoMensaje.Error
                                        });

                                        return Fn_Save_BaseSwft;
                                    }
                                }
                            }
                            if (TipG == "R")
                            {
                                MODSWEN1.Pr_ActSwift(Globales, i, 1, Corr.ToInt());
                            }
                            else
                            {
                                MODSWEN1.Pr_ActSwift(Globales, i, 0, Corr.ToInt());
                            }
                            if (MODGSWF.VSwf[i].NroSwf == "MT-103")
                            {
                                TipMT = 103;
                            }
                            if (MODGSWF.VSwf[i].NroSwf == "MT-202")
                            {
                                TipMT = 202;
                            }

                            if (Fn_PutSwfSCE(Globales, unit, Modswen.RutAis, Corr, 0, TipG, numneg, tippro, NumCpa, numcuo, NumCob, TipMT) == 0)
                            {
                                return Fn_Save_BaseSwft;
                            }
                        }
                    }
                    Fn_Save_BaseSwft = true;

                    return Fn_Save_BaseSwft;

                }
                catch (Exception exc)
                {
                    tracer.TraceException("Alerta al guardar swift", exc);
                }

                return Fn_Save_BaseSwft;
            }
        }

        public static string Fn_FormateaString(ref string cadena, int largo)
        {
            string Fn_FormateaString = "";


            string dbcom = "";

            cadena = MODGPYF0.Componer(cadena, "=", " ");
            cadena = MODGPYF0.Componer(cadena, "&", " ");
            cadena = MODGPYF0.Componer(cadena, "$", "");
            cadena = MODGPYF0.Componer(cadena, ";", " ");
            cadena = MODGPYF0.Componer(cadena, "@", " ");
            cadena = MODGPYF0.Componer(cadena, "#", "N");
            cadena = MODGPYF0.Componer(cadena, "%", " ");
            cadena = MODGPYF0.Componer(cadena, "!", " ");
            cadena = MODGPYF0.Componer(cadena, "'", " ");
            cadena = MODGPYF0.Componer(cadena, "*", " ");
            cadena = MODGPYF0.Componer(cadena, ">", " ");
            cadena = MODGPYF0.Componer(cadena, "<", " ");
            cadena = MODGPYF0.Componer(cadena, "º", "");
            cadena = MODGPYF0.Componer(cadena, "ª", "");
            cadena = MODGPYF0.Componer(cadena, "Ñ", "N");
            cadena = MODGPYF0.Componer(cadena, "ñ", "n");
            cadena = MODGPYF0.Componer(cadena, "á", "a");
            cadena = MODGPYF0.Componer(cadena, "é", "e");
            cadena = MODGPYF0.Componer(cadena, "í", "i");
            cadena = MODGPYF0.Componer(cadena, "ó", "o");
            cadena = MODGPYF0.Componer(cadena, "ú", "u");
            cadena = MODGPYF0.Componer(cadena, "ü", "u");
            cadena = MODGPYF0.Componer(cadena, "Á", "A");
            cadena = MODGPYF0.Componer(cadena, "É", "E");
            cadena = MODGPYF0.Componer(cadena, "Í", "I");
            cadena = MODGPYF0.Componer(cadena, "Ó", "O");
            cadena = MODGPYF0.Componer(cadena, "Ú", "U");
            cadena = MODGPYF0.Componer(cadena, "Ü", "U");
            cadena = MODGPYF0.Componer(cadena, "´", "");
            cadena = MODGPYF0.Componer(cadena, "`", "");
            dbcom = "\"";
            dbcom = dbcom.Mid(1, 1);
            cadena = MODGPYF0.Componer(cadena, dbcom, "");

            if (cadena.Len() > largo)
            {
                cadena = cadena.Mid(1, largo);
            }

            Fn_FormateaString = cadena;

            return Fn_FormateaString;
        }

        //Esta función va a buscar los datos de los Bancos a la Base SWIFT
        //Remplaza a la funcion Fn_Trae_Bancos del legacy. Va a la base swift y busca un banco por código swift y retorna si intercambia clave o no
        public static bool BancoIntercambiaClave(DatosGlobales Globales,string codBco)
        {
            if (codBco != null && codBco.Length == 11)
            {
                using(var unit = new UnitOfWorkSwift())
                {
                    sw_bancos banco = unit.BancoRepository.GetBancosByCodAndBranch(codBco.Substring(0, 8), codBco.Substring(8, 3)).FirstOrDefault();
                    if (banco != null)
                    {
                        return banco.intercambio_clave == "S";
                    }
                    else
                    {
                        Globales.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = "El banco '" + codBco + "' no existe en la base SWIFT.",
                            Title = "Banco intercambia clave"
                        });

                        return false;
                    }
                }
            }
            else throw new ArgumentException("El código de banco enviado no tiene la longitud esperada (11)");
        }

        //Esta función va a buscar los datos a la base Swift
        public static short Fn_Trae_Fmt_Campos(DatosGlobales Globales, string MT)
        {
            List<ProcSwTraeFmtCampos> listaFormato = null;

            using (UnitOfWorkSwift uow = new UnitOfWorkSwift())
            {
                listaFormato = uow.SwRepository.Proc_sw_trae_fmt_campos(MT);

                //Resultado nulo de la Consulta.-
                if (listaFormato.Count == 0)
                {
                    return -1;
                }

                Globales.Modswen.VFmt_Swf = new Fmt_Swf[listaFormato.Count + 1];

                int i = 1;
                foreach (ProcSwTraeFmtCampos formatoCampo in listaFormato)
                {
                    Fmt_Swf formato = new Fmt_Swf()
                    {
                        Status_Campo = formatoCampo.Status,
                        Id_Campo = formatoCampo.Tag,
                        Nombre_Campo = formatoCampo.Nombre,
                        Orden_Campo = (short)(formatoCampo.Orden.HasValue ? formatoCampo.Orden.Value : 0),
                        Repeticion = (short)(formatoCampo.Repeticion.HasValue ? formatoCampo.Repeticion.Value : 0),
                        Formato_Campo = formatoCampo.Formato,
                        Largo_Campo = (short)(formatoCampo.Largo.HasValue ? formatoCampo.Largo.Value : 0),
                        Linea_Campo = (short)(formatoCampo.Linea.HasValue ? formatoCampo.Linea.Value : 0)
                    };

                    Globales.Modswen.VFmt_Swf[i++] = formato;
                }
            }

            return -1;
        }

        public static string Fn_CampoMT(DatosGlobales Globales, string Codigo, string MT, string campo, string sig, string subsig, short Espacio, short Lineas)
        {
            // UPGRADE_INFO (#0561): The 'sw2' symbol was defined without an explicit "As" clause.
            dynamic sw2 = "";
            string Tipo = "";
            string Com = "";
            short k = 0;
            string LinSinAc = "";

            if (VB6Helpers.Instr(MT, ": " + campo + " :") != 0)
            {

                switch (Codigo)
                {
                    case "72":
                        if (VB6Helpers.Instr(MT, ": " + sig + " :") != 0)
                        {
                            Com = VB6Helpers.Mid(MT, VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio, VB6Helpers.Instr(MT, ": " + sig + " :") - VB6Helpers.Instr(MT, ": " + campo + " :") - Espacio);
                        }
                        else
                        {
                            if (VB6Helpers.Instr(MT, ": " + subsig + " :") != 0)
                            {
                                Com = VB6Helpers.Mid(MT, VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio, VB6Helpers.Instr(MT, ": " + subsig + " :") - VB6Helpers.Instr(MT, ": " + campo + " :") - Espacio);
                            }
                            else
                            {
                                Com = VB6Helpers.Mid(MT, VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio);
                            }

                        }

                        break;
                    case "74":
                        if (VB6Helpers.Instr(MT, ": " + sig + " :") != 0)
                        {
                            Com = VB6Helpers.Mid(MT, VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio, VB6Helpers.Instr(MT, ": " + sig + " :") - VB6Helpers.Instr(MT, ": " + campo + " :") - Espacio);
                        }
                        else
                        {
                            if (VB6Helpers.Instr(MT, ": " + subsig + " :") != 0)
                            {
                                Com = VB6Helpers.Mid(MT, VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio, VB6Helpers.Instr(MT, ": " + subsig + " :") - VB6Helpers.Instr(MT, ": " + campo + " :") - Espacio);
                            }
                            else
                            {
                                Com = VB6Helpers.Mid(MT, VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio);
                            }

                        }

                        break;
                    case "75":
                        if (VB6Helpers.Instr(MT, ": " + sig + " :") != 0)
                        {
                            Com = VB6Helpers.Mid(MT, VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio, VB6Helpers.Instr(MT, ": " + sig + " :") - VB6Helpers.Instr(MT, ": " + campo + " :") - Espacio);
                        }
                        else
                        {
                            Com = VB6Helpers.Mid(MT, VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio, VB6Helpers.Len(MT) - VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio);
                        }

                        break;
                    case "76":
                        Com = VB6Helpers.Mid(MT, VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio);

                        break;
                    case "73":
                        Com = VB6Helpers.Mid(MT, VB6Helpers.Instr(MT, ": " + campo + " :") + Espacio);

                        break;
                }

                Com = MODGPYF0.Componer(Com, VB6Helpers.Space(Espacio), " ");
                Com = MODGPYF0.Componer(Com, ": 72 : SENDER TO RECEIVER INFORMATION", " ");
                Com = MODGPYF0.Componer(Com, ": 75 : QUERIES", " ");
                Com = MODGPYF0.Componer(Com, "//", "");
                Com = MODGPYF0.Componer(Com, "/", "");
                Com = MODGPYF0.Componer(Com, VB6Helpers.Chr(13) + VB6Helpers.Chr(10), " ");
                VB6Helpers.Erase(ref Globales.Modswen.Lin_Mts);

                if (Codigo == "72")
                {

                    //Dependiendo si tiene campo 57? pone "ACC" sino "REC"
                    //Esto es válido sólo para MT-100 y MT-202.-
                    Tipo = "";
                    if (VB6Helpers.Instr(MT, "MESSAGE TYPE   : 100") != 0 || VB6Helpers.Instr(MT, "MESSAGE TYPE   : 202") != 0)
                    {
                        Tipo = "/REC/";
                        if (VB6Helpers.Instr(MT, ": 57") != 0)
                        {
                            Tipo = "/ACC/";
                        }

                        Com = Tipo + Com;
                    }

                    //Si es MT-100 o MT-202 v/s los otros MT's.-
                    if (!string.IsNullOrEmpty(Tipo))
                    {
                        MODGPYF1.SeparaL(Globales, Com, ref Globales.Modswen.Lin_Mts, 33, 210); //Len(Com$))
                    }
                    else
                    {
                        MODGPYF1.SeparaL(Globales, Com, ref Globales.Modswen.Lin_Mts, 35, 210); //Len(Com$))
                    }

                    for (k = 1; k <= (short)VB6Helpers.UBound(Globales.Modswen.Lin_Mts); k++)
                    {
                        if (k > Lineas)
                        {
                            break;
                        }

                        LinSinAc = Fn_FormateaString(ref Globales.Modswen.Lin_Mts[k], (short)VB6Helpers.Len(Globales.Modswen.Lin_Mts[k]));
                        Globales.Modswen.Lin_Mts[k] = LinSinAc;
                        if (k == 1)
                        {

                            sw2 = sw2 + ":" + campo + ":" + VB6Helpers.LTrim(Globales.Modswen.Lin_Mts[k]) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Tipo))
                            {
                                sw2 = sw2 + "//" + VB6Helpers.LTrim(Globales.Modswen.Lin_Mts[k]) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }
                            else
                            {
                                sw2 = sw2 + VB6Helpers.LTrim(Globales.Modswen.Lin_Mts[k]) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }

                            //Se ponen dos lineas en comentarios del MT-100
                        }

                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(Com))
                    {
                        MODGPYF1.SeparaL(Globales, Com, ref Globales.Modswen.Lin_Mts, 35, 210); //Len(Com$))
                        for (k = 1; k <= (short)VB6Helpers.UBound(Globales.Modswen.Lin_Mts); k++)
                        {
                            if (k > Lineas)
                            {
                                break;
                            }

                            LinSinAc = Fn_FormateaString(ref Globales.Modswen.Lin_Mts[k], (short)VB6Helpers.Len(Globales.Modswen.Lin_Mts[k]));
                            Globales.Modswen.Lin_Mts[k] = LinSinAc;
                            if (k == 1)
                            {

                                sw2 = sw2 + ":" + campo + ":" + VB6Helpers.LTrim(Globales.Modswen.Lin_Mts[k]) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }
                            else
                            {
                                sw2 = sw2 + VB6Helpers.LTrim(Globales.Modswen.Lin_Mts[k]) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                            }

                        }

                    }

                }

            }

            return VB6Helpers.CStr(sw2);
        }

        public static int Fn_Sw_GetCorr(DatosGlobales Globales)
        {
            
            int? correlativo = GetCorrelativoSwift(Globales);
            if (correlativo == null)
            {
                correlativo = -1;
            }
            return correlativo.Value;
        }

        //Retorna la respuesta de una Consulta.-
        //RespuestaEnvioSwift="" => Hubo error.-
        public static int? GetCorrelativoSwift(DatosGlobales Globales)
        {
            try
            {
                using (var unit = new UnitOfWorkSwift())
                {
                    return unit.BancoRepository.GetCorrelativo();
                }
            }
            catch
            {

                Globales.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Informacion,
                    Text = "Se ha producido un error al rescatar correlativo de Mensaje Swift.",
                    Title = T_MODGCHQ.MsgDocVal
                });

                return null;
                
            }
        }

        /// <summary>
        /// Función para guardar el swift o los swift locales.
        /// </summary>
        /// <param name="Globales">Datos del objecto completo en memoria.</param>
        /// <param name="uow">Conección a la base de datos.</param>
        /// <param name="Texto">Descripción de la línea.</param>
        /// <param name="indice">Indice del arreglo donde buscar el Swift que fallo.</param>
        /// <returns></returns>
        public static bool Fn_SwiftLocal(DatosGlobales Globales, UnitOfWorkCext01 uow, string Texto, int indice)
        {
            using (Tracer tracer = new Tracer("XGGL - Fn_SwiftLocal"))
            {
                try
                {
                    string archivo = MODGRNG.LeeSceRng(Globales, uow, "CSW").ToString().PadLeft(6, '0');
                    string sistema = "SCE";
                    DateTime fecha = DateTime.Now;
                    string moneda = Globales.MODGSWF.VSwf[indice].SwfMon;
                    decimal monto = (decimal)Globales.MODGSWF.VSwf[indice].mtoswf;
                    string referencia = Globales.MODGSWF.VGSwf.NumOpe;
                    string tipo = "MT" + Globales.MODGSWF.VSwf[indice].NroSwf.Substring(3, 3);

                    uow.SceRepository.pro_sce_swf_pendientes_i01_MS(Globales.MODGUSR.UsrEsp.CentroCosto, Globales.MODGUSR.UsrEsp.Especialista, archivo, Globales.MODGUSR.UsrEsp.Rut.ToString(), sistema, fecha, tipo, moneda, monto, referencia, Texto, false);

                }
                catch (Exception _ex)
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "Se produjo un problema al generar el Swift Local",
                        Title = T_MODGSWF.MsgSwf
                    });

                    tracer.TraceException("Alerta, Problemas al generar el swift local",_ex);
                    return false;
                }

                return true;
            }
        }

        public static bool Fn_Sw_PutSw(DatosGlobales Globales, UnitOfWorkCext01 uow, T_Swf swift, string cuerpoSwift, int correlativo)
        {
            using (Tracer tracer = new Tracer("XGGL - Fn_Sw_PutSw"))
            {
                using (UnitOfWorkSwift uowSwift = new UnitOfWorkSwift())
                {
                    int? idExistente = uowSwift.SwRepository.Proc_sw_msg_s01_MS(correlativo);
                    if (!idExistente.HasValue || idExistente.Value == 0)
                    {
                        try
                        {
                            //todo ok, el id no existe
                            Swi200Service service = new SWI200.Swi200Service();

                            bool result = service.IngresaModificaMensajeSwift(1234, correlativo, int.Parse(Globales.Modswen.RutAis), int.Parse(Globales.MODGUSR.UsrEsp.CentroCosto), swift.SwfMon, swift.mtoswf, 'A', cuerpoSwift);
                            return result;
                        }
                        catch (Exception ex)
                        {
                            Globales.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Detalles: " + ex.Message,
                                Title = "Error al guardar swift en base SWIFT"
                            });

                            tracer.TraceException("Alerta, problemas al guardar swift en base SWIFT", ex);
                            return false;
                        }
                    }
                    else
                    {
                        Globales.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "El correlativo que se desea utilizar ya existe en la base de datos SWIFT",
                            Title = "SWIFT"
                        });

                        return false;
                    }
                }
            }
        }

        //Graba el Correlativo del mensaje
        public static short Fn_PutSwfSCE(DatosGlobales Globales, UnitOfWorkCext01 unit, string RutAis, string Corr, short estado, string TipGra, string numneg, string tippro, string NumCpa, string numcuo, string NumCob, int TipMT)
        {
            short _retValue = 0;
            try
            {
                unit.SceRepository.Sce_mts_i01(
                    Globales.SYGETPRT.Codop.Cent_costo,
                    Globales.SYGETPRT.Codop.Id_Product,
                    Globales.SYGETPRT.Codop.Id_Especia,
                    Globales.SYGETPRT.Codop.Id_Empresa,
                    Globales.SYGETPRT.Codop.Id_Operacion,
                    MODGSYB.dbDecimal(numneg),
                    MODGSYB.dbDecimal(tippro),
                    MODGSYB.dbDecimal(NumCpa),
                    MODGSYB.dbDecimal(numcuo),
                    MODGSYB.dbDecimal(NumCob),
                    MODGSYB.dbDecimal(RutAis),
                    DateTime.Now.Date,
                    MODGSYB.dbDecimal(Corr),
                    estado,
                    TipGra,
                    Globales.MODGCON0.VMch.NroRpt,
                    TipMT);
                ;

                _retValue = (short)(true ? -1 : 0);
            }
            catch (Exception _ex)
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Informacion,
                    Text = "No se pudo guardar el nro correlativo",
                    Title = "Error al guardar"
                });
            }
            return _retValue;
        }

        //Cambia el estado de un swift.-
        public static void ActivaBD_Swi(DatosGlobales Globales, UnitOfWorkCext01 unit, string OpeSin, decimal numneg, decimal tippro, decimal NuCorr, decimal numcuo, short NumCob, int Correlativo)
        {
            int estado = 1;
            MODXDATA.Cmd_Put(Globales, () =>
            {
                return (short)unit.SceRepository.Sce_mts_u01_MS(VB6Helpers.Mid(OpeSin, 1, 3),
                    VB6Helpers.Mid(OpeSin, 4, 2),
                    VB6Helpers.Mid(OpeSin, 6, 2),
                    VB6Helpers.Mid(OpeSin, 8, 3),
                    VB6Helpers.Mid(OpeSin, 11, 5),
                    numneg,
                    tippro,
                    NuCorr,
                    numcuo,
                    NumCob,
                    Correlativo,
                    estado);
            });
            
        }

        /// <summary>
        /// Trae todos los swift de un retorno
        /// </summary>
        /// <param name="estado"></param>
        /// <param name="TipGra"></param>
        /// <param name="numneg"></param>
        /// <param name="tippro"></param>
        /// <param name="NumCpa"></param>
        /// <param name="numcuo"></param>
        /// <param name="NumCob"></param>
        /// <returns></returns>
        public static int Fn_GetMts(int estado, string tipGra, string numNeg, string tipPro, string numCpa, string numCuo, string NumCob, DatosGlobales Globales, UnitOfWorkCext01 uow)
        {

            using (Tracer tracer = new Tracer("´PlanillaVisibleExportacion - Fn_GetMts"))
            {
                int Fn_GetMts = 0;
                try
                {
                    IList<sce_mts_s01_MS_Result> result = uow.SceRepository.sce_mts_s01_1_MS(Globales.SYGETPRT.Codop.Cent_costo, Globales.SYGETPRT.Codop.Id_Product,
                                        Globales.SYGETPRT.Codop.Id_Especia, Globales.SYGETPRT.Codop.Id_Empresa, Globales.SYGETPRT.Codop.Id_Operacion,
                                        numNeg.ToInt(), tipPro.ToInt(), numCpa.ToInt(), numCuo.ToInt(), NumCob.ToInt(), estado, tipGra);

                    if (result.Count() > 0)
                    {
                        Globales.Modswen.VMts = new T_Mtes[0];
                        int n = result.Count();
                        Globales.Modswen.VMts = new T_Mtes[n];

                        for (int i = 0; i < n; i += 1)
                        {
                            Globales.Modswen.VMts[i] = new T_Mtes();
                            Globales.Modswen.VMts[i].fecmsg = result[i].fecmsg.ToString("dd/MM/yyyy");
                            Globales.Modswen.VMts[i].id_mensaje = result[i].id_mensaje.ToInt();
                            Globales.Modswen.VMts[i].NroRpt = result[i].nrorpt.ToInt();
                        }
                        return true.ToInt();
                    }
                }
                catch (Exception exc)
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error de Comunicación al tratar Desactivar los Swift en la Base Cext01.",
                        Title = "Planillas Visibles de Exportación"
                    });
                }

                return Fn_GetMts;
            }
        }

        public static int Fn_AnulaSwift(string casilla, int Correl, string comentario, DatosGlobales Globales, UnitOfWorkSwift uow)
        {
            using (Tracer tracer = new Tracer("Anular Swift - Fn_AnulaSwift"))
            {
                try
                {
                    tracer.TraceInformation("Fn_AnulaSwift | proc_sw_env_graba_nul: {0} | {1} | {2} | {3}", Correl, Globales.Modswen.RutAis.ToInt(), casilla.ToInt(), comentario);
                    var result = uow.AdministracionRepository.proc_sw_env_graba_nul(Correl, Globales.Modswen.RutAis.ToInt(), casilla.ToInt(), comentario);
                    if (result != 0)
                    {
                        Globales.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Se ha producido un error al tratar de Anular el Swift Nº " + Correl.Str() + " desde la base SWIFT.",
                            Title = "SWIFT"
                        });
                        return false.ToInt();
                    }
                    return true.ToInt();
                }
                catch (Exception exc)
                {
                    tracer.TraceException("Alerta, problemas al intentar anular swift Fn_AnulaSwift", exc);
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error al tratar de Anular el Swift Nº " + Correl.Str() + " desde la base SWIFT. ",
                        Title = "Planillas Visibles de Exportación"
                    });
                    return false.ToInt();
                }
            }
        }
        
        /// <summary>
        /// Borra un swift de Comex.-
        /// Function Fn_BorraSwiCo (OpeSin As String, Correlativo As Long) As Integer
        /// </summary>
        /// <param name="OpeSin"></param>
        /// <param name="numneg"></param>
        /// <param name="tippro"></param>
        /// <param name="NuCorr"></param>
        /// <param name="numcuo"></param>
        /// <param name="NumCob"></param>
        /// <param name="Correlativo"></param>
        /// <returns></returns>
        public static int Fn_BorraSwiCo(string OpeSin, int numneg, int tippro, int NuCorr, int numcuo, int NumCob, int Correlativo, DatosGlobales Globales, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer("Borra_Swift_Comex - Fn_BorraSwiCo"))
            {
                try
                {
                    // Genera Comando.-
                    var result = uow.SceRepository.sce_mts_d01_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 1, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 4, 2)),
                                        MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 6, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 8, 3)),
                                        MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 11, 5)), numneg, tippro, NuCorr, numcuo, NumCob, Correlativo);

                    return true.ToInt();
                }
                catch (Exception exc)
                {
                    tracer.TraceException("Alerta en: Fn_BorraSwiCo", exc);
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error de Comunicación al tratar de Borrar Información del Swift en la Base Cext01. ",
                        Title = "Activación de Registros"
                    });
                    return false.ToInt();
                }
            }
        }
    }
}
