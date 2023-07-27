using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;
using BCH.Comex.Common.Tracing;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODGCHQ
    {
        // Entrega la Referencia del Banco de Chile de la Operación.-
        public static string Referencia(DatosGlobales Globales)
        {
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;

            string Referencia = "";


            Referencia = SYGETPRT.Codop.Cent_costo + SYGETPRT.Codop.Id_Product + SYGETPRT.Codop.Id_Especia + SYGETPRT.Codop.Id_Empresa + SYGETPRT.Codop.Id_Operacion;

            return Referencia;
        }

        // Entrega la Referencia del Banco de Chile de la Operación.-
        public static string FileName(DatosGlobales Globales)
        {
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;

            string Referencia = "";

            //Referencia = SYGETPRT.Codop.Cent_costo + "-" + SYGETPRT.Codop.Id_Product + "-" + SYGETPRT.Codop.Id_Especia + "-" + SYGETPRT.Codop.Id_Empresa + "-" + SYGETPRT.Codop.Id_Operacion;
            Referencia = SYGETPRT.Codop.Cent_costo + SYGETPRT.Codop.Id_Product + SYGETPRT.Codop.Id_Especia + SYGETPRT.Codop.Id_Empresa + SYGETPRT.Codop.Id_Operacion;

            return Referencia;
        }

        // Llega un dato y se limpia aceptándose sólo números hasta un cierto largo.-
        public static string SoloNumeros(string Dato, int largo)
        {
            string SoloNumeros = "";

            int i = 0;

            const string Numeros = "0123456789";
            string Pdato = "";
            string Respuesta = "";

            Pdato = Dato.TrimB();
            if (Pdato.Len() <= 0)
            {
                SoloNumeros = Dato;
                return SoloNumeros;
            }
            Respuesta = "";
            for (i = 1; i <= Pdato.Len(); i += 1)
            {
                if (Numeros.InStr(Pdato.Mid(i, 1), 1, StringComparison.CurrentCulture) != 0)
                {
                    Respuesta = Respuesta + Pdato.Mid(i, 1);
                    if (Respuesta.Len() == largo)
                    {
                        break;
                    }
                }
            }
            if (Respuesta.Len() < largo)
            {
                for (i = 1; i <= largo - Respuesta.Len(); i += 1)
                {
                    Respuesta = "0" + Respuesta;
                }
            }
            SoloNumeros = Respuesta;

            return SoloNumeros;
        }

        // Graba un Cheque de una Operación.-
        // Retorno    = True  : Grabación Exitosa.-
        //            = False : Error o Grabación no Exitosa.-
        public static int SyPutn_Chq(DatosGlobales Globales,UnitOfWorkCext01 unit, string NumOpe, string CodAnu)
        {
            T_MODGCHQ MODGCHQ = Globales.MODGCHQ;
            T_MODGUSR MODGUSR = Globales.MODGUSR;
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;

            int SyPutn_Chq = 0;

            string R = "";
            int j = 0;
            string que = "";
            int c = 0;
            int i = 0;

            try
            {
                
                for (i = 1; i <= MODGCHQ.V_Chq_VVi.GetUpperBound(0); i += 1)
                {
                    if (MODGCHQ.V_Chq_VVi[i].TipoDoc == T_MODGCHQ.DOCVAL_CHEQUE)
                    {

                        // Correlativo del Cheque.-
                        if (c == 0)
                        {
                            c = SyGetc_Chq(Globales,unit, NumOpe) + 1;
                        }
                        else
                        {
                            c = c + 1;
                        }
                       
                        List<string> parameters = new List<string>();

                        parameters.Add(MODGSYB.dbcharSy(NumOpe.Mid(1, 3)));
                        parameters.Add(MODGSYB.dbcharSy(NumOpe.Mid(4, 2)));
                        parameters.Add(MODGSYB.dbcharSy(NumOpe.Mid(6, 2)));
                        parameters.Add(MODGSYB.dbcharSy(NumOpe.Mid(8, 3)));
                        parameters.Add(MODGSYB.dbcharSy(NumOpe.Mid(11, 5)));
                        parameters.Add(MODGSYB.dbnumesy(c));
                        parameters.Add(MODGSYB.dbdatesy(MigrationSupport.Utils.Format(DateTime.Now, "dd/mm/yyyy")));
                        parameters.Add(MODGSYB.dbcharSy(CodAnu));
                        parameters.Add(MODGSYB.dbnumesy(T_MODGCON0.ECC_ANU));
                        parameters.Add(MODGSYB.dbcharSy(MODGUSR.UsrEsp.CostoSuper));
                        parameters.Add(MODGSYB.dbcharSy(MODGUSR.UsrEsp.Id_Super));
                        parameters.Add(MODGSYB.dbnumesy(T_MODGCHQ.DOCV_Ing));
                        j = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.Get_VMnd(Globales, unit, MODGCHQ.V_Chq_VVi[i].CodMon);
                        parameters.Add(MODGSYB.dbcharSy(MODGTAB0.VMnd[j].Mnd_MndNmc));
                        // --------------------
                        // OGG    03/03/1999.-
                        // Que$ = Que$ + dbnumeSy(V_Chq_VVi(i%).MtoDoc))
                        parameters.Add(MODGSYB.dbmontoSyForRead(MODGCHQ.V_Chq_VVi[i].MtoDoc));
                        // OGG    03/03/1999.-
                        // --------------------
                        parameters.Add(MODGSYB.dbcharSy(MODGCHQ.V_Chq_VVi[i].NomBen));
                        parameters.Add(MODGSYB.dbcharSy(MODGCHQ.V_Chq_VVi[i].NomPag));
                        parameters.Add(MODGSYB.dbcharSy(MODGCHQ.V_Chq_VVi[i].DirPag));
                        parameters.Add(MODGSYB.dbcharSy(MODGCHQ.V_Chq_VVi[i].SwfPag));
                        parameters.Add(MODGSYB.dbcharSy(MODGCHQ.V_Chq_VVi[i].CiuPag));
                        parameters.Add(MODGSYB.dbcharSy(MODGCHQ.V_Chq_VVi[i].PaiPag));
                        parameters.Add(MODGSYB.dbcharSy(MODGCHQ.V_Chq_VVi[i].NumCta));
                        parameters.Add(MODGSYB.dbcharSy(MODGCHQ.V_Chq_VVi[i].NomCli));
                        int result = -1;
                        unit.SceRepository.ReadQuerySP((dr) =>
                        {
                            if (dr.Read())
                            {
                                result = dr.GetInt32(0);
                            }
                        }, "sce_chq_i01", parameters.ToArray());

                        if (result == -1)
                        {
                            Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                            {
                                Type=Common.UI_Modulos.TipoMensaje.Error,
                                Title= T_MODGCHQ.MsgDocVal,
                                Text= "Se ha producido un error al tratar de grabar un Cheque de una Operación."
                            });
                            return SyPutn_Chq;
                        }
                    }
                }

                SyPutn_Chq = true.ToInt();

                return SyPutn_Chq;

            }
            catch (Exception exc)
            {
                Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                {
                    Type = Common.UI_Modulos.TipoMensaje.Error,
                    Title = T_MODGCHQ.MsgDocVal,
                    Text = "Se ha producido un error al tratar de grabar un Cheque de una Operación."
                });

            }
            return SyPutn_Chq;
        }

        //Retorna el último correlativo de Cheque para una Operación.-
        public static short SyGetc_Chq(DatosGlobales Globales, UnitOfWorkCext01 unit, string NumOpe)
        {
            short _retValue = 0;
            try
            {
                _retValue = (short)(true ? -1 : 0);
                List<string> parameters = new List<string>();
                //Genera Sentencia.-
                parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)));
                parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)));
                parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)));
                parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)));
                parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)));

                int? res = unit.SceRepository.EjecutarSP<int?>("sce_chq_s02", parameters.ToArray()).First();

                _retValue = (short)(res.HasValue ? res.Value : 0);
            }
            catch (Exception _ex)
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Se ha producido un error al tratar de leer el último correlativo del Cheque.",
                    Title= T_MODGCHQ.MsgDocVal
                });
                _retValue = 0;
            }
            return _retValue;
        }

        //Graba un Vale Vista de una Operación.-
        //Retorno    = True  : Grabación Exitosa.-
        //           = False : Error o Grabación no Exitosa.-
        public static short SyPutn_Vvi(DatosGlobales Globales, UnitOfWorkCext01 unit, string NumOpe, string CodAnu)
        {
            T_MODGCHQ MODGCHQ = Globales.MODGCHQ;
            T_MODGUSR MODGUSR = Globales.MODGUSR;
            
            short _retValue = 0;
            short i = 0;
            short c = 0;
            string Que = "";
            string R = "";
            try
            {
                for (i = 1; i <= (short)VB6Helpers.UBound(MODGCHQ.V_Chq_VVi); i++)
                {
                    if (MODGCHQ.V_Chq_VVi[i].TipoDoc == T_MODGCHQ.DOCVAL_VALVIS)
                    {
                        //Correlativo del Vale Vista.-
                        if (c == 0)
                        {
                            c = (short)(SyGetc_Vvi(Globales, unit, NumOpe) + 1);
                        }
                        else
                        {
                            c = (short)(c + 1);
                        }

                        List<string> parameters = new List<string>();

                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)));
                        parameters.Add(MODGSYB.dbnumesy(c));
                        parameters.Add(MODGSYB.dbdatesy(DateTime.Now.ToString("yyyy-mm-dd")));
                        parameters.Add(MODGSYB.dbcharSy(CodAnu));
                        parameters.Add(MODGSYB.dbnumesy(T_MODGCON0.ECC_ANU));
                        parameters.Add(MODGSYB.dbcharSy(MODGUSR.UsrEsp.CostoSuper));
                        parameters.Add(MODGSYB.dbcharSy(MODGUSR.UsrEsp.Id_Super));
                        parameters.Add(MODGSYB.dbnumesy(T_MODGCHQ.DOCV_Ing));
                        parameters.Add(MODGSYB.dbcharSy(MODGCHQ.V_Chq_VVi[i].NomBen));
                        parameters.Add(MODGSYB.dbcharSy(MODGCHQ.V_Chq_VVi[i].RutTom));
                        parameters.Add(MODGSYB.dbcharSy(MODGCHQ.V_Chq_VVi[i].NomTom));
                        parameters.Add(MODGSYB.dbmontoSyForRead(MODGCHQ.V_Chq_VVi[i].MtoDoc));

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
                        }, "sce_vvi_i01", parameters.ToArray());

                        if (resOpe == 0)
                        {

                        }
                        else
                        {
                            throw new Exception();
                        }
                    }

                }
                _retValue = -1;
            }
            catch (Exception _ex)
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Se ha producido un error al tratar de grabar un Vale Vista de una Operación.",
                    Title= T_MODGCHQ.MsgDocVal
                });
                _retValue = 0;
            }
            return _retValue;
        }

        //Retorna el último correlativo de Vale Vista para una Operación.-
        public static short SyGetc_Vvi(DatosGlobales Globales, UnitOfWorkCext01 unit, string NumOpe)
        {
            short _retValue = 0;
            try
            {
                // IGNORED: On Error GoTo SyGetc_VviErr

                _retValue = (short)(true ? -1 : 0);

                //Genera Sentencia.-
                List<string> parameters = new List<string>();
                parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)));
                parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)));
                parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)));
                parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)));
                parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)));

                int res = unit.SceRepository.EjecutarSP<int>("sce_vvi_s02", parameters.ToArray()).First();

                _retValue = (short)res;
            }
            catch (Exception _ex)
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "No se ha encontrado el último correlativo del Vale Vista.",
                    Title= T_MODGCHQ.MsgDocVal
                });
                _retValue = 0;
            }
            return _retValue;
        }

        /// <summary>
        /// Anula un cheque -> Deja el estado del cheque con el valor 9.-
        /// </summary>
        /// <param name="OpeSin"></param>
        /// <param name="CodAnu"></param>
        /// <param name="Globales"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public static int SyAnu_Chq(string OpeSin, string CodAnu, DatosGlobales Globales, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer("Anular_Cheque - SyAnu_Chq"))
            {
                int SyAnu_Chq = 0;
                try
                {
                    return uow.SceRepository.sce_chq_u02_MS(
                        MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 1, 3)),
                        MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 4, 2)),
                        MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 6, 2)),
                        MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 8, 3)),
                        MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 11, 5)),
                        CodAnu,
                        T_MODGCON0.ECC_ANU);
                }
                catch (Exception exc)
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error de Comunicación al tratar de anular un Cheque de una Operación.",
                        Title = "Anulación de Registros"
                    });
                    return SyAnu_Chq;
                }
            }
        }

        // Retorna el índice del Código de Moneda en el Arreglo de Monedas MNDS.-
        // Retorno = 0    => No se encontró Código Moneda.-
        public static int Find_Mnd(int codigo,DatosGlobales Globales)
        {
            return Globales.MODGTAB0.VMnd.ToList().FindIndex(x => x.Mnd_MndCod == codigo);
        }

        // True si todos los Montos están distribuídos en Cheques.-
        public static int Todos_Cheq_Generados(DatosGlobales Globales)
        {
            T_MODGCHQ MODGCHQ = Globales.MODGCHQ;

            int Todos_Cheq_Generados = 0;

            int i = 0;
            int n = 0;

            n = MODGCHQ.V_Chq_VVi.GetUpperBound(0);

            if (n == 0)
            {
                return Todos_Cheq_Generados;
            }

            Todos_Cheq_Generados = true.ToInt();
            for (i = 1; i <= MODGCHQ.V_Chq_VVi.GetUpperBound(0); i += 1)
            {
                if (MODGCHQ.V_Chq_VVi[i].Generado == 0)
                {
                    Todos_Cheq_Generados = false.ToInt();
                    break;
                }
            }

            return Todos_Cheq_Generados;
        }

        // Retorna el País de la Moneda Especificada.-
        public static int Fn_GetPai(int CodMnd)
        {
            int Fn_GetPai = 0;


            switch (CodMnd)
            {
                case 11:
                    Fn_GetPai = 225;
                    break;
                case 15:
                    Fn_GetPai = 226;
                    break;
                case 16:
                    Fn_GetPai = 342;
                    break;
                case 18:
                    Fn_GetPai = 405;
                    break;
                case 20:
                    Fn_GetPai = 406;
                    break;
                case 21:
                    Fn_GetPai = 509;
                    break;
                case 41:
                    Fn_GetPai = 510;
                    break;
                case 59:
                    Fn_GetPai = 507;
                    break;
                case 61:
                    Fn_GetPai = 513;
                    // Case 62
                    // Fn_GetPai =
                    break;
                case 65:
                    Fn_GetPai = 515;
                    break;
                case 66:
                    Fn_GetPai = 514;
                    break;
                case 67:
                    Fn_GetPai = 505;
                    // Case 68
                    // Fn_GetPai =
                    break;
                case 71:
                    Fn_GetPai = 504;
                    break;
                case 72:
                    Fn_GetPai = 563;
                    break;
                case 81:
                    Fn_GetPai = 517;
                    // Case 90
                    // Fn_GetPai =
                    break;
                case 93:
                    Fn_GetPai = 331;
                    break;
                case 95:
                    Fn_GetPai = 512;
                    break;
            }

            return Fn_GetPai;
        }

        // Recorre la Nómina y Retorna el índice de un Corresponsal
        // dado su Swift y la Moneda en que opera.-

        public static int Find_Nom(DatosGlobales Globales, string Swift, int Moneda)
        {
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;
            int Find_Nom = 0;

            int i = 0;

            Find_Nom = -1;
            for (i = 1; i <= MODGTAB0.VNom.GetUpperBound(0); i += 1)
            {
                if (MODGTAB0.VNom[i].Nom_Swf.TrimB() == Swift.TrimB() && MODGTAB0.VNom[i].Nom_Mda == Moneda)
                {
                    Find_Nom = i;
                    break;
                }
            }

            return Find_Nom;
        }

        // ****************************************************************************
        //    1.  Este procedimiento está destinado para determinar el monto no
        //        generado para así llevar el cursor al más proximo luego de haber
        //        generado uno.
        // ****************************************************************************
        public static void Pr_No_Generado_Chq(DatosGlobales Globales, UI_ListBox lista)
        {
            T_MODGCHQ MODGCHQ = Globales.MODGCHQ;

            int i = 0;
            int Contador = 0;

            Contador = 0;
            // Primero: Comienza desde el siguiente al recien generado hasta el final
            //          del arreglo.
            for (i = MODGCHQ.Indice + 1; i <= MODGCHQ.V_Chq_VVi.GetUpperBound(0); i += 1)
            {
                if (!MODGCHQ.V_Chq_VVi[i].Generado.ToBool())
                {
                    lista.ListIndex = lista.Items.FindIndex(x => x.Data == i);
                    Contador = 1;
                    break;
                }
            }
            // Segundo: Si no se encontro ninguno antes, estonces se comienza desde el
            //          principio hasta el que se acaba de generar.
            if (Contador == 0)
            {
                for (i = 1; i <= MODGCHQ.Indice; i += 1)
                {
                    if (!MODGCHQ.V_Chq_VVi[i].Generado.ToBool())
                    {
                        lista.ListIndex = lista.Items.FindIndex(x => x.Data == i);
                        Contador = 1;
                        break;
                    }
                }
            }
            // Tercero: Si en los casos anteriores no se encontro ninguno, se envía el
            //          indice al primer registro de la lista.
            if (Contador == 0)
            {
                lista.ListIndex = 0;
            }

        }

        // Indica si por lo menos algún documento ha sido generado.-
        public static int Existe_Generado_Chq(DatosGlobales Globales)
        {
            int Existe_Generado_Chq = 0;

            int i = 0;

            Existe_Generado_Chq = false.ToInt();
            for (i = 1; i <= Globales.MODGCHQ.V_Chq_VVi.GetUpperBound(0); i += 1)
            {
                if (Globales.MODGCHQ.V_Chq_VVi[i].Generado != 0)
                {
                    Existe_Generado_Chq = true.ToInt();
                    break;
                }
            }

            return Existe_Generado_Chq;
        }
    }
}
