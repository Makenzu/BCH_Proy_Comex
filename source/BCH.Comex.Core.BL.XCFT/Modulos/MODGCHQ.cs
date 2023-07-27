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
    public class MODGCHQ
    {
        public static T_MODGCHQ GetMODGCHQ()
        {
            return new T_MODGCHQ();
        }

        //Entrega la Referencia del Banco de Chile de la Operación.-
        public static string Referencia(InitializationObject Modulos)
        {
            T_Module1 Module1 = Modulos.Module1;
            return Module1.Codop.Cent_Costo + Module1.Codop.Id_Product + Module1.Codop.Id_Especia + Module1.Codop.Id_Empresa + Module1.Codop.Id_Operacion;
        }

        //Llega un dato y se limpia aceptándose sólo números hasta un cierto largo.-
        public static string SoloNumeros(string Dato, short largo)
        {
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;

            // UPGRADE_INFO (#0561): The 'Numeros' symbol was defined without an explicit "As" clause.
            const string Numeros = "0123456789";
            // UPGRADE_INFO (#0561): The 'Pdato' symbol was defined without an explicit "As" clause.
            dynamic Pdato = VB6Helpers.Trim(Dato);
            string Respuesta = "";

            if (VB6Helpers.Len(VB6Helpers.CStr(Pdato)) <= 0)
            {
                return Dato;
            }

            Respuesta = "";
            for (i = 1; i <= (short)VB6Helpers.Len(VB6Helpers.CStr(Pdato)); i++)
            {
                if (VB6Helpers.Instr(1, Numeros, VB6Helpers.Mid(VB6Helpers.CStr(Pdato), i, 1)) != 0)
                {
                    // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 'Respuesta' variable as a StringBuilder6 object.
                    Respuesta += VB6Helpers.Mid(VB6Helpers.CStr(Pdato), i, 1);
                    if (VB6Helpers.Len(Respuesta) == largo)
                    {
                        break;
                    }

                }

            }

            if (VB6Helpers.Len(Respuesta) < largo)
            {
                for (i = 1; i <= (short)(largo - VB6Helpers.Len(Respuesta)); i++)
                {
                    Respuesta = "0" + Respuesta;
                }

            }

            return Respuesta;
        }

        //Graba un Cheque de una Operación.-
        //Retorno    = True  : Grabación Exitosa.-
        //           = False : Error o Grabación no Exitosa.-
        public static short SyPutn_Chq(InitializationObject initObject, UnitOfWorkCext01 unit, string NumOpe, string CodAnu)
        {
            using (var tracer = new Tracer("Graba Cheque Operación - SyPutn_Chq"))
            {
                T_MODGCHQ MODGCHQ = initObject.MODGCHQ;
                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;
                T_MODGUSR MODGUSR = initObject.MODGUSR;
                T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;


                short _retValue = 0;
                // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
                short i = 0;
                // UPGRADE_INFO (#05B1): The 'c' variable wasn't declared explicitly.
                short c = 0;
                // UPGRADE_INFO (#05B1): The 'j' variable wasn't declared explicitly.
                short j = 0;


                try
                {
                    // IGNORED: On Error GoTo SyPutn_ChqErr

                    for (i = 0; i <= (short)VB6Helpers.UBound(MODGCHQ.V_Chq_VVi); i++)
                    {
                        if (MODGCHQ.V_Chq_VVi[i].TipoDoc == T_MODGCHQ.DOCVAL_CHEQUE)
                        {
                            //Correlativo del Cheque.-
                            if (c == 0)
                            {
                                c = (short)(SyGetc_Chq(initObject, unit, NumOpe) + 1);
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
                            parameters.Add(MODGSYB.dbdatesy(DateTime.Now.ToString("dd/MM/yyyy")));
                            parameters.Add(MODGSYB.dbcharSy(CodAnu));
                            parameters.Add(MODGSYB.dbnumesy(T_MODGCON0.ECC_ANU));
                            parameters.Add(MODGSYB.dbcharSy(MODGUSR.UsrEsp.CostoSuper));
                            parameters.Add(MODGSYB.dbcharSy(MODGUSR.UsrEsp.Id_Super));
                            parameters.Add(MODGSYB.dbnumesy(T_MODGCHQ.DOCV_Ing));
                            j = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, MODGCHQ.V_Chq_VVi[i].CodMon);
                            parameters.Add(MODGSYB.dbcharSy(MODGTAB0.VMnd[j].Mnd_MndNmc));
                            parameters.Add(MODGSYB.dbmontoSyForRead(MODGCHQ.V_Chq_VVi[i].MtoDoc));
                            parameters.Add(MODGSYB.dbcharSy(MODGCHQ.V_Chq_VVi[i].NomBen));
                            parameters.Add(MODGSYB.dbcharSy(MODGCHQ.V_Chq_VVi[i].NomPag));
                            parameters.Add(MODGSYB.dbcharSy(MODGCHQ.V_Chq_VVi[i].DirPag));
                            parameters.Add(MODGSYB.dbcharSy(MODGCHQ.V_Chq_VVi[i].SwfPag));
                            parameters.Add(MODGSYB.dbcharSy(MODGCHQ.V_Chq_VVi[i].CiuPag));
                            parameters.Add(MODGSYB.dbcharSy(MODGCHQ.V_Chq_VVi[i].PaiPag));
                            parameters.Add(MODGSYB.dbcharSy(MODGCHQ.V_Chq_VVi[i].NumCta));
                            parameters.Add(MODGSYB.dbcharSy(MODGCHQ.V_Chq_VVi[i].NomCli));

                            int resOp = -1;
                            unit.SceRepository.ReadQuerySP((reader) =>
                            {
                                if (reader.Read())
                                {
                                    resOp = reader.GetInt32(0);
                                }
                                else
                                {
                                    resOp = -1;
                                }
                            }, "sce_chq_i01", parameters.ToArray());
                            //int rowsAffected = unit.SceRepository.ExecuteNonQuerySP("sce_xdoc_i01_MS", VB6Helpers.Mid(NumOpe, 1, 3), VB6Helpers.Mid(NumOpe, 4, 2), VB6Helpers.Mid(NumOpe, 6, 2), VB6Helpers.Mid(NumOpe, 8, 3), VB6Helpers.Mid(NumOpe, 11, 5), c.ToString(), VB6Helpers.Left(Usuario, 3), VB6Helpers.Right(Usuario, 2), CodDoc.ToString(), DateTime.Now.ToString("yyyy-MM-dd"), m.ToString());
                            //Hace un Put en Sce_xDoc.
                            if (resOp != 0)
                            {
                                tracer.TraceInformation("Se ha producido un error al tratar de grabar un Cheque de una Operación");
                                Mdi_Principal.MESSAGES.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Error,
                                    Text = "Se ha producido un error al tratar de grabar un Cheque de una Operación.",
                                });
                                _retValue = 0;
                            }

                            //int rowsAffected = unit.SceRepository.ExecuteNonQuerySP("sce_chq_i01", parameters.ToArray());
                            //if (rowsAffected == 0)
                            //{
                            //    Mdi_Principal.MESSAGES.Add(new UI_Message()
                            //    {
                            //        Type = TipoMensaje.Error,
                            //        Text = "Se ha producido un error al tratar de grabar un Cheque de una Operación.",
                            //    });
                            //    _retValue = 0;
                            //}
                            else
                            {
                                _retValue = 1;
                            }
                            //Se ejecuta el Procedimiento Almacenado.-
                        }

                    }

                    _retValue = (short)(true ? -1 : 0);
                }
                catch (Exception _ex)
                {
                    tracer.TraceException("Alerta:", _ex);
                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Se ha producido un error al tratar de grabar un Cheque de una Operación.",
                        Type = TipoMensaje.Error
                    });
                    _retValue = 0;
                }
                return _retValue;
            }
        }

        //Retorna el último correlativo de Cheque para una Operación.-
        public static short SyGetc_Chq(InitializationObject initObject, UnitOfWorkCext01 unit, string NumOpe)
        {
            using (var tracer = new Tracer())
            {
                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;
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
                    tracer.TraceException("Alerta:", _ex);
                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error al tratar de leer el último correlativo del Cheque."
                    });
                    _retValue = 0;
                }
                return _retValue;
            }
        }

        //Graba un Vale Vista de una Operación.-
        //Retorno    = True  : Grabación Exitosa.-
        //           = False : Error o Grabación no Exitosa.-
        public static short SyPutn_Vvi(InitializationObject initObject, UnitOfWorkCext01 unit, string NumOpe, string CodAnu)
        {
            using (var tracer = new Tracer("Graba Vale Vista - SyPutn_Vvi"))
            {
                T_MODGCHQ MODGCHQ = initObject.MODGCHQ;
                T_MODGUSR MODGUSR = initObject.MODGUSR;
                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;

                short _retValue = 0;
                short i = 0;
                short c = 0;
                string Que = "";
                string R = "";
                try
                {
                    for (i = 0; i <= (short)VB6Helpers.UBound(MODGCHQ.V_Chq_VVi); i++)
                    {
                        if (MODGCHQ.V_Chq_VVi[i].TipoDoc == T_MODGCHQ.DOCVAL_VALVIS)
                        {
                            //Correlativo del Vale Vista.-
                            if (c == 0)
                            {
                                c = (short)(SyGetc_Vvi(initObject, unit, NumOpe) + 1);
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
                            parameters.Add(MODGSYB.dbdatesy(DateTime.Now.ToString("yyyy-MM-dd")));
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
                                throw new Exception("Error de lectura sce_vvi_i01");
                            }
                        }

                    }
                    _retValue = -1;
                }
                catch (Exception _ex)
                {
                    tracer.TraceException("Alerta:", _ex);
                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error al tratar de grabar un Vale Vista de una Operación."
                    });
                    _retValue = 0;
                }
                return _retValue;
            }
        }

        //Retorna el último correlativo de Vale Vista para una Operación.-
        public static short SyGetc_Vvi(InitializationObject initObject, UnitOfWorkCext01 unit, string NumOpe)
        {
            using (var tracer = new Tracer("SyGetc_Vvi"))
            {
                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;
                short _retValue = 0;
                try
                {
                    // IGNORED: On Error GoTo SyGetc_VviErr
                    _retValue = (short)(true ? -1 : 0);

                    tracer.TraceInformation("Nro NumOpe: " + NumOpe);
                    //Genera Sentencia.-
                    List<string> parameters = new List<string>();
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)));
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)));
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)));
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)));
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)));
                    int res = unit.SceRepository.EjecutarSP<int>("sce_vvi_s02_MS", parameters.ToArray()).First();

                    _retValue = (short)res;
                }
                catch (Exception _ex)
                {
                    tracer.TraceException("Alerta: No se ha encontrado el último correlativo del Vale Vista.", _ex);
                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "No se ha encontrado el último correlativo del Vale Vista."
                    });
                    _retValue = 0;
                }
                return _retValue;
            }
        }


        //Indica si por lo menos algún documento ha sido generado.-
        public static short Existe_Generado_Chq(InitializationObject initObject)
        {
            T_MODGCHQ MODGCHQ = initObject.MODGCHQ;
            short _retValue = 0;
            short i = 0;

            _retValue = (short)(false ? -1 : 0);
            for (i = 0; i <= (short)VB6Helpers.UBound(MODGCHQ.V_Chq_VVi); i++)
            {
                if (MODGCHQ.V_Chq_VVi[i].Generado != 0)
                {
                    _retValue = (short)(true ? -1 : 0);
                    break;
                }
            }
            return _retValue;
        }

        //True si todos los Montos están distribuídos en Cheques.-
        public static short Todos_Cheq_Generados(InitializationObject initObject)
        {
            short _retValue = 0;
            short n = 0;
            short i = 0;
            VB6Helpers.ClearError();
            n = (short)VB6Helpers.UBound(initObject.MODGCHQ.V_Chq_VVi);
            if (n == -1)
            {
                return _retValue;
            }
            _retValue = (short)(true ? -1 : 0);
            for (i = 0; i <= (short)VB6Helpers.UBound(initObject.MODGCHQ.V_Chq_VVi); i++)
            {
                if (~initObject.MODGCHQ.V_Chq_VVi[i].Generado != 0)
                {
                    _retValue = (short)(false ? -1 : 0);
                    break;
                }
            }
            return _retValue;
        }

        //Recorre la Nómina y Retorna el índice de un Corresponsal
        //dado su Swift y la Moneda en que opera.-
        //
        public static short Find_Nom(InitializationObject initObject, string Swift, short Moneda)
        {
            short _retValue = 0;
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;

            _retValue = -1;
            for (i = 1; i <= (short)VB6Helpers.UBound(initObject.MODGTAB0.VNom); i++)
            {
                if ((VB6Helpers.Trim(initObject.MODGTAB0.VNom[i].Nom_Swf) == VB6Helpers.Trim(Swift)) && initObject.MODGTAB0.VNom[i].Nom_Mda == Moneda)
                {
                    _retValue = i;
                    break;
                }

            }

            return _retValue;
        }

        //****************************************************************************
        //   1.  Este procedimiento está destinado para determinar el monto no
        //       generado para así llevar el cursor al más proximo luego de haber
        //       generado uno.
        //****************************************************************************
        public static void Pr_No_Generado_Chq(InitializationObject initObject, UI_Grid Lista)
        {
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;

            initObject.MODCVDIMMM.Contador = 0;
            //Primero: Comienza desde el siguiente al recien generado hasta el final
            //         del arreglo.
            for (i = (short)(initObject.MODGCHQ.Indice); i <= (short)VB6Helpers.UBound(initObject.MODGCHQ.V_Chq_VVi); i++)
            {
                if (initObject.MODGCHQ.V_Chq_VVi[i].Generado == 0)
                {
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Lista'. Consider using the GetDefaultMember6 helper method.
                    //VB6Helpers.Set(VB6Helpers.CObj(Lista), "ListIndex", MODGPYF0.PosLista(Lista, i));
                    Lista.ListIndex = i;
                    initObject.MODCVDIMMM.Contador = 1;
                    break;
                }

            }

            //Segundo: Si no se encontro ninguno antes, estonces se comienza desde el
            //         principio hasta el que se acaba de generar.
            if (initObject.MODCVDIMMM.Contador == 0)
            {
                for (i = 0; i <= (short)initObject.MODGCHQ.Indice; i++)
                {
                    if (initObject.MODGCHQ.V_Chq_VVi[i].Generado == 0)
                    {
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Lista'. Consider using the GetDefaultMember6 helper method.
                        //VB6Helpers.Set(VB6Helpers.CObj(Lista), "ListIndex", MODGPYF0.PosLista(Lista, i));
                        Lista.ListIndex = MODGPYF0.PosLista(Lista, i);

                        initObject.MODCVDIMMM.Contador = 1;
                        break;
                    }

                }

            }

            //Tercero: Si en los casos anteriores no se encontro ninguno, se envía el
            //         indice al primer registro de la lista.
            if (initObject.MODCVDIMMM.Contador == 0)
            {
                //VB6Helpers.Set(VB6Helpers.CObj(Lista), "ListIndex", 0);
                Lista.ListIndex = 0;
            }

        }


        //Retorna el índice del Código de Moneda en el Arreglo de Monedas MNDS.-
        //Retorno = 0    => No se encontró Código Moneda.-
        public static short Find_Mnd(InitializationObject initObject, short Codigo)
        {
            short _retValue = 0;
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;

            _retValue = 0;
            for (i = 1; i <= (short)VB6Helpers.UBound(initObject.MODGTAB0.VMnd); i++)
            {
                if (Codigo == initObject.MODGTAB0.VMnd[i].Mnd_MndCod)
                {
                    _retValue = i;
                    break;
                }

            }

            return _retValue;
        }

        //Retorna el País de la Moneda Especificada.-
        public static short Fn_GetPai(short CodMnd)
        {

            switch (CodMnd)
            {
                case 11:
                    return 225;
                case 15:
                    return 226;
                case 16:
                    return 342;
                case 18:
                    return 405;
                case 20:
                    return 406;
                case 21:
                    return 509;
                case 41:
                    return 510;
                case 59:
                    return 507;
                case 61:
                    return 513;
                case 65:
                    return 515;
                case 66:
                    return 514;
                case 67:
                    return 505;
                case 71:
                    return 504;
                case 72:
                    return 563;
                case 81:
                    return 517;
                case 93:
                    return 331;
                case 95:
                    return 512;
            }

            return 0;
        }
    }
}
