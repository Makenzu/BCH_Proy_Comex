using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class Mdl_Funciones_Varias
    {

        public static bool vaAIngresoValores(InitializationObject initObject, UnitOfWorkCext01 unit, string dondeIr)
        {
            var m = initObject.MODXPLN0.VxDatP.CodMnd;
            if (m == 0)
            {
                m = initObject.MODGCVD.CodMonDolar;
            }
            var a = MODGTAB0.SyGet_Vmd(initObject.MODGTAB0, unit, DateTime.Now.ToString("yyyy-MM-dd"), m);
            bool entraAIngresoValores = (initObject.MODGTAB0.VVmd.VmdPrd == 0 || initObject.MODGTAB0.VVmd.VmdObs == 0) && (~a != 0);
            if (entraAIngresoValores)
            {
                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Text = "No se han encontrado los valores de las Paridades y Tipos de Cambio para efectuar el Cobro de Comisiones. Debe ingresar los valores manualmente.",
                    Type = TipoMensaje.Informacion
                });
                initObject.MODGTAB0.VVmd.VmdCod = m;
                initObject.MODGTAB0.VVmd.VmdFec = DateTime.Now.ToString("yyyy-MM-dd");

                initObject.FormularioQueAbrir = "Ingreso_Valores";
                initObject.Frm_Ingreso_Valores = new UI_Frm_Ingreso_Valores();
                initObject.Frm_Ingreso_Valores.VieneDe = dondeIr;
            }
            return entraAIngresoValores;
        }


        //Delivered Duty Paid.
        public static Type_DatPrty[] DatPrtys;

        //ESTO ESTABA EN mdl_srm
        public static string srm_num(string campo)
        {
            string Lc_C = "";
            short i = 0;
            string Lc_decimal = ",";
            for (i = 1; i <= (short)VB6Helpers.Len(VB6Helpers.Trim(campo)); i++)
            {
                if (VB6Helpers.Mid(campo, i, 1) == ".")
                {
                    // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 'Lc_C' variable as a StringBuilder6 object.
                    Lc_C += Lc_decimal;
                }
                else
                {
                    Lc_C += VB6Helpers.Mid(campo, i, 1);
                }

            }

            return Lc_C;
        }

        public static T_Mdl_Funciones_Varias GetMdl_Funciones_Varias()
        {
            return new T_Mdl_Funciones_Varias();
        }

        public static short Guarda_Oper_Manual(InitializationObject initObject, UnitOfWorkCext01 unit, string OpeSin, short indvia, short indori, short IdcCtaCte, string IdPrty)
        {
            short _retValue = 0;
            short Li_Largo1 = 0;
            short Li_Largo2 = 0;
            short APartirDe = 0;
            bool sigue = false;
            short num_registros_msg = 0;
            string ls_ret = "";
            string ls_sql = "";
            short ls_orden = 0;
            string ls_retorno_venta = "";
            string ls_mensaje = "";

            // I. Determinar tipo de Cuenta Via
            // --------------------------------
            // Cuenta Corriente
            // Corresponsal
            // Contable
            // GAP
            double n_contableOri = 0;
            string Lc_CuentaVia = "";
            dynamic tipo_cuentaVia = null;
            string tipo_cosmosVia = "NO";
            short i = 0;
            string Lc_Sin_Via = "0";
            short d = 0;

            using (var tracer = new Tracer("Guarda_Oper_Manual"))
            {
                try
                {
                    if (initObject.MODXVIA.VxVia.Length == 0)
                    {
                        Lc_Sin_Via = "1";
                        Lc_CuentaVia = "";
                        tipo_cuentaVia = "CONTABLE";
                    }
                    else
                    {
                        //1. Busca en Vias si es tipo cuenta_cte es Nacional o Extranjera código 64 or codigo 64
                        tracer.TraceInformation("1. Busca en Vias si es tipo cuenta_cte es Nacional o Extranjera código 64 or codigo 64");
                        if ((initObject.MODXVIA.VxVia[indvia].NumCta == T_MODGCON0.IdCta_CtaCteMANN) || (initObject.MODXVIA.VxVia[indvia].NumCta == T_MODGCON0.IdCta_CtaCteMANE))
                        {
                            if (string.IsNullOrEmpty(initObject.MODXVIA.VxVia[indvia].CtaCte_t))
                            {
                                Lc_CuentaVia = "";
                            }
                            else
                            {
                                Lc_CuentaVia = initObject.MODXVIA.VxVia[indvia].CtaCte_t;
                            }

                            tipo_cuentaVia = "CtaCte";
                            //'Consulta si es cosmos en el Via
                            if (initObject.MODXORI.gb_esCosmos == true)
                            {
                                tipo_cosmosVia = "SI";
                            }
                            tracer.TraceInformation("1.","tipo_cuentaVia: " + tipo_cuentaVia, "tipo_cosmosVia: " + tipo_cosmosVia);
                        }

                        // 2. Si no encontro en punto anterior sigue y Busca código 23 Corresponsal
                        tracer.TraceInformation("2. Si no encontro en punto anterior sigue y Busca código 23 Corresponsal");
                        if ((initObject.MODXVIA.VxVia[indvia].NumCta == T_MODGCON0.IdCta_CTAORD))
                        {
                            Lc_CuentaVia = "";
                            tipo_cuentaVia = "CORRESPONSAL";
                            tracer.TraceInformation("2.", "Lc_CuentaVia: " + Lc_CuentaVia, "tipo_cuentaVia: " + tipo_cuentaVia);
                        }

                        //3. Si no encontro en punto anterior sigue y Busca código <> 64-65-23
                        tracer.TraceInformation("3. Si no encontro en punto anterior sigue y Busca código <> 64-65-23");
                        if ((initObject.MODXVIA.VxVia[indvia].NumCta != 0) && (initObject.MODXVIA.VxVia[indvia].NumCta != T_MODGCON0.IdCta_CtaCteMANN) && (initObject.MODXVIA.VxVia[indvia].NumCta != T_MODGCON0.IdCta_CtaCteMANE) && (initObject.MODXVIA.VxVia[indvia].NumCta != T_MODGCON0.IdCta_CTAORD))
                        {
                            Lc_CuentaVia = "";
                            for (d = 0; d <= (short)VB6Helpers.UBound(initObject.MODGCON0.VMcds); d++)
                            {
                                if (initObject.MODXVIA.VxVia[indvia].NemCta == initObject.MODGCON0.VMcds[d].NemCta || initObject.MODGCON0.VMcds[d].NemCta == "OPEPEND")
                                {
                                    Lc_CuentaVia = initObject.MODGCON0.VMcds[d].NumCta;
                                    break;
                                }

                            }

                            tipo_cuentaVia = "CONTABLE";
                            tracer.TraceInformation("3.", "Lc_CuentaVia: " + Lc_CuentaVia, "tipo_cuentaVia: " + tipo_cuentaVia);
                            //Consulta por Cuenta GAP = IdCta_GAPMN = 66 or  IdCta_GAPME = 67
                            tracer.TraceInformation("3. Consulta por Cuenta GAP = IdCta_GAPMN = 66 or  IdCta_GAPME = 67");
                            if (initObject.MODXVIA.VxVia[indvia].NumCta == T_MODGCON0.IdCta_GAPMN || initObject.MODXVIA.VxVia[indvia].NumCta == T_MODGCON0.IdCta_GAPME)
                            {
                                tipo_cuentaVia = "GAP";
                                if (string.IsNullOrEmpty(initObject.MODXVIA.VxVia[indvia].CtaCte_t))
                                {
                                    Lc_CuentaVia = "";
                                }
                                else
                                {
                                    Lc_CuentaVia = initObject.MODXVIA.VxVia[indvia].CtaCte_t;
                                }

                            }
                            tracer.TraceInformation("3.", "Lc_CuentaVia: " + Lc_CuentaVia, "tipo_cuentaVia: " + tipo_cuentaVia);
                        }
                    }

                    // II. Deteminar para el Origen
                    //---------------------------------
                    string Lc_CuentaOri = "";
                    dynamic tipo_cuentaOri = null;
                    string tipo_cosmosOri = "NO";

                    //1. Busca en Ori si es tipo cuenta_cte es  código 64 or codigo 65
                    tracer.TraceInformation("1. Busca en Ori si es tipo cuenta_cte es  código 64 or codigo 65");
                    if ((initObject.MODXORI.VxOri[indori].NumCta == T_MODGCON0.IdCta_CtaCteMANN) || (initObject.MODXORI.VxOri[indori].NumCta == T_MODGCON0.IdCta_CtaCteMANE))
                    {
                        if (string.IsNullOrEmpty(initObject.MODXORI.VxOri[indori].CtaCte_t))
                        {
                            Lc_CuentaOri = "";
                        }
                        else
                        {
                            Lc_CuentaOri = initObject.MODXORI.VxOri[indori].CtaCte_t;
                        }

                        tipo_cuentaOri = "CtaCte";
                        //Consulta si es cosmos en el Origen
                        if (initObject.MODXORI.gb_esCosmos == true)
                        {
                            tipo_cosmosOri = "SI";
                        }
                        tracer.TraceInformation("3.", "Lc_CuentaOri: " + Lc_CuentaOri, "tipo_cuentaOri: " + tipo_cuentaOri, "tipo_cosmosOri: " + tipo_cosmosOri);
                    }

                    //2. Si no encontro en punto anterior sigue y Busca código 23 Corresponsal
                    tracer.TraceInformation("2. Si no encontro en punto anterior sigue y Busca código 23 Corresponsal");
                    if ((initObject.MODXORI.VxOri[indori].NumCta == T_MODGCON0.IdCta_CTAORD))
                    {
                        for (d = 0; d <= (short)VB6Helpers.UBound(initObject.MODGCON0.VMcds); d++)
                        {
                            if (initObject.MODXORI.VxOri[indvia].NemCta == initObject.MODGCON0.VMcds[d].NemCta || initObject.MODGCON0.VMcds[d].NemCta == "OPEPEND")
                            {
                                Lc_CuentaOri = initObject.MODGCON0.VMcds[d].NumCta;
                                break;
                            }

                        }
                        tipo_cuentaOri = "CORRESPONSAL";
                        tracer.TraceInformation("3.", "Lc_CuentaOri: " + Lc_CuentaOri, "tipo_cuentaOri: " + tipo_cuentaOri, "tipo_cosmosOri: " + tipo_cosmosOri);
                    }

                    //3. Si no encontro en punto anterior sigue y Busca código <> 64 OR 65 OR 23
                    tracer.TraceInformation("3. Si no encontro en punto anterior sigue y Busca código <> 64 OR 65 OR 23");
                    if ((initObject.MODXORI.VxOri[indori].NumCta != 0) && (initObject.MODXORI.VxOri[indori].NumCta != T_MODGCON0.IdCta_CtaCteMANN) && (initObject.MODXORI.VxOri[indori].NumCta != T_MODGCON0.IdCta_CtaCteMANE) && (initObject.MODXORI.VxOri[indori].NumCta != T_MODGCON0.IdCta_CTAORD))
                    {
                        Lc_CuentaOri = "";
                        for (d = 0; d <= (short)VB6Helpers.UBound(initObject.MODGCON0.VMcds); d++)
                        {
                            if (initObject.MODXORI.VxOri[indori].NemCta == initObject.MODGCON0.VMcds[d].NemCta || initObject.MODGCON0.VMcds[d].NemCta == "OPEPEND")
                            {
                                Lc_CuentaOri = initObject.MODGCON0.VMcds[d].NumCta;
                                break;
                            }
                        }

                        tipo_cuentaOri = "CONTABLE";
                        //Consulta por cuentas GAP 66 or 67
                        if ((initObject.MODXORI.VxOri[indori].NumCta == T_MODGCON0.IdCta_GAPMN || initObject.MODXORI.VxOri[indori].NumCta == T_MODGCON0.IdCta_GAPME))
                        {
                            if (string.IsNullOrEmpty(initObject.MODXORI.VxOri[indori].CtaCte_t))
                            {
                                Lc_CuentaOri = "";
                            }
                            else
                            {
                                Lc_CuentaOri = initObject.MODXORI.VxOri[indori].CtaCte_t;
                            }
                            tipo_cuentaOri = "GAP";
                        }

                        /// Considera nemónico COE y si la cuenta va vacía, busca el número de cuenta.
                        if(initObject.MODXORI.VxOri[indori].NumCta == T_MODGCON0.IdCta_CHMEBCH && string.IsNullOrEmpty(Lc_CuentaOri))
                        {
                            /// se recorren las cuentas corrientes origenes de las monedas
                            foreach (var origenCuenta in initObject.MODXORI.Vx_OriCC)
                            {
                                if(origenCuenta.CodMnd == initObject.MODXORI.VxOri[indori].CodMon)
                                {
                                    Lc_CuentaOri = origenCuenta.ctacte.Trim().ToString();
                                }
                            }
                        }

                        tracer.TraceInformation("3.", "Lc_CuentaOri: " + Lc_CuentaOri, "tipo_cuentaOri: " + tipo_cuentaOri);
                    }

                    //------------------------------------------------------------------
                    //-- Valida que solo se ingresen Cuentas Corrientes en FTS ---------
                    //------------------------------------------------------------------
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    if (tipo_cuentaOri != "CtaCte" && tipo_cuentaVia != "CtaCte")
                    {
                        //And tipo_cuentaOri <> "GAP" And tipo_cuentaVia <> "GAP" Then
                        return (short)(true ? -1 : 0);
                    }

                    //******************************************************************
                    //** SE SEPARA POR BLOQUES LOS CAMPOS NECESIARIO PARA FTS **********
                    //** POR CONSTANTES CAMBIOS EN DEFINICIONES EN CAMPOS     **********
                    //******************************************************************
                    //------------------------------------------------------------------
                    //-- Obtiene Base Number y Genera Base Number Formateado Largo 11 --
                    //------------------------------------------------------------------
                    string Lc_BaseNumber = "";
                    string Lc_BaseNumber_11 = "";
                    string Lc_ContracRefNumber = "";
                    string Lc_FechaCorta = "";
                    int Ll_HoraIngreso = 0;
                    if (!string.IsNullOrWhiteSpace(IdPrty))
                    {
                        Lc_BaseNumber = MODGPYF0.Componer(IdPrty, "~", "");
                        //Lc_BaseNumber_11 = "0" + VB6Helpers.LTrim(VB6Helpers.RTrim(VB6Helpers.Str(Lc_BaseNumber))) + "0000";
                        Lc_BaseNumber_11 = "0" + Lc_BaseNumber.ToString().Trim().Replace("|", "") + "0000";
                    }
                    else
                    {
                        Lc_BaseNumber = MODGPYF0.Componer(initObject.MODGCVD.VgCvd.PrtCli, "~", "");
                        //Lc_BaseNumber_11 = "0" + VB6Helpers.LTrim(VB6Helpers.RTrim(VB6Helpers.Str(Lc_BaseNumber))) + "0000";
                        Lc_BaseNumber_11 = "0" + Lc_BaseNumber.ToString().Trim().Replace("|", "") + "0000";
                    }

                    //---------------------------------------------
                    //Realsystems-Código Nuevo-Inicio
                    //Fecha Modificación 20100623
                    //Responsable: Pablo Millan
                    //Versión: 1.0
                    //Descripción : Se modifica generacion de CRN
                    //---------------------------------------------
                    Lc_ContracRefNumber = VB6Helpers.Mid(initObject.Mdl_Funciones_Varias.LC_TRXID_MAN, 16, 10);  // Contract reference number
                                                                                                                 //----------------------------------------
                                                                                                                 // RealSystems - Código Nuevo - Termino
                                                                                                                 //----------------------------------------
                                                                                                                 // RealSystems - Código Antiguo - Inicio
                                                                                                                 //----------------------------------------
                                                                                                                 //Lc_ContracRefNumber = Mid$(OpeSin, 6, 2) & Mid$(OpeSin, 8, 3) & Mid$(OpeSin, 11, 5)
                                                                                                                 //----------------------------------------
                                                                                                                 // RealSystems - Código Antiguo - Termino
                                                                                                                 //----------------------------------------

                    //Fecha en formato YYMMDD
                    Lc_FechaCorta = VB6Helpers.Format(initObject.MODGCON0.VMch.fecmov, "yymmdd");
                    //Hora en formato HHMM
                    Ll_HoraIngreso = (int)VB6Helpers.Val(VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.TimeOfDay), "hhmm"));

                    //------------------------------------------------------------------
                    //-- Campo 4) ORDERING CUSTOMER ------------------------------------
                    //------------------------------------------------------------------
                    string Lc_OrderCustomer = "";

                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    if (tipo_cuentaOri != "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        Lc_OrderCustomer = "00000000000";
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia != "CtaCte")
                    {
                        Lc_OrderCustomer = Lc_BaseNumber_11;
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        if (IdcCtaCte == 1)
                        {
                            Lc_OrderCustomer = "00000000000";
                        }
                        else if (IdcCtaCte == 2)
                        {
                            Lc_OrderCustomer = Lc_BaseNumber_11;
                        }

                    }

                    //------------------------------------------------------------------
                    //-- Campo 7) RECEIVER ---------------------------------------------
                    //------------------------------------------------------------------
                    string Lc_Receiver = "";

                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    if (tipo_cuentaOri != "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        Lc_Receiver = Lc_BaseNumber_11;
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia != "CtaCte")
                    {
                        Lc_Receiver = "00000000000";
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        if (IdcCtaCte == 1)
                        {
                            Lc_Receiver = Lc_BaseNumber_11;
                        }
                        else if (IdcCtaCte == 2)
                        {
                            Lc_Receiver = "00000000000";
                        }

                    }

                    //------------------------------------------------------------------
                    //-- Campo 9) ORDERING CUSTOMER ACCOUNT NUMBER ---------------------
                    //------------------------------------------------------------------
                    short Lc_Largo = 0;
                    string Lc_Correlativo = "";
                    double Lc_OrderCustNumber = 0;
                    // UPGRADE_INFO (#0501): The 'prueba' member isn't used anywhere in current application.
                    string prueba = "";

                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    if (tipo_cuentaOri != "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        Lc_OrderCustNumber = 0;
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia != "CtaCte")
                    {
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                        if (tipo_cuentaVia == "CONTABLE" || tipo_cuentaVia == "CORRESPONSAL")
                        {
                            if (string.IsNullOrEmpty(Lc_CuentaOri))
                            {
                                Lc_OrderCustNumber = 0;
                            }
                            else
                            {
                                Lc_Largo = (short)VB6Helpers.Len(VB6Helpers.Trim(Lc_CuentaOri));
                                if (Lc_Largo > 9)
                                {
                                    Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaOri, Lc_Largo - 2, 3);
                                    Lc_OrderCustNumber = Format.StringToDouble(Lc_BaseNumber + VB6Helpers.Mid(Lc_CuentaOri, 1, 1) + Lc_Correlativo);
                                }
                                else
                                {
                                    Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaOri, Lc_Largo - 2, 3);
                                    Lc_OrderCustNumber = Format.StringToDouble(Lc_BaseNumber + "0" + Lc_Correlativo);
                                }

                            }

                            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                        }
                        else if (string.IsNullOrEmpty(tipo_cuentaVia))
                        {
                            Lc_OrderCustNumber = 0;
                        }
                        else
                        {
                            Lc_Largo = (short)VB6Helpers.Len(VB6Helpers.Trim(Lc_CuentaVia));
                            if (Lc_Largo > 9)
                            {
                                Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaVia, Lc_Largo - 2, 3);
                                Lc_OrderCustNumber = Format.StringToDouble(Lc_BaseNumber + VB6Helpers.Mid(Lc_CuentaVia, 1, 1) + Lc_Correlativo);
                            }
                            else
                            {
                                Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaVia, Lc_Largo - 2, 3);
                                Lc_OrderCustNumber = Format.StringToDouble(Lc_BaseNumber + "0" + Lc_Correlativo);
                            }

                        }

                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        if (IdcCtaCte == 1)
                        {
                            Lc_OrderCustNumber = 0;
                        }
                        else if (IdcCtaCte == 2)
                        {
                            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                            if (string.IsNullOrEmpty(tipo_cuentaOri))
                            {
                                Lc_OrderCustNumber = 0;
                            }
                            else
                            {
                                Lc_Largo = (short)VB6Helpers.Len(VB6Helpers.Trim(Lc_CuentaOri));
                                if (Lc_Largo > 9)
                                {
                                    Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaOri, Lc_Largo - 2, 3);
                                    Lc_OrderCustNumber = Format.StringToDouble(Lc_BaseNumber + VB6Helpers.Mid(Lc_CuentaOri, 1, 1) + Lc_Correlativo);
                                }
                                else
                                {
                                    Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaOri, Lc_Largo - 2, 3);
                                    Lc_OrderCustNumber = Format.StringToDouble(Lc_BaseNumber + "0" + Lc_Correlativo);
                                }

                            }

                        }

                    }

                    //------------------------------------------------------------------
                    //-- Campo 10) RECEIVER ACCOUNT NUMBER -----------------------------
                    //------------------------------------------------------------------
                    double Lc_ReceiverNumber = 0;
                    // UPGRADE_INFO (#0501): The 'Li_ReceiverPaso' member isn't used anywhere in current application.
                    string Li_ReceiverPaso = "";

                    Lc_Largo = 0;
                    Lc_Correlativo = "";

                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    if (tipo_cuentaOri != "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        //    If tipo_cuentaOri = "CONTABLE" Or tipo_cuentaOri = "CORRESPONSAL" Then
                        Lc_Largo = (short)VB6Helpers.Len(VB6Helpers.Trim(Lc_CuentaVia));
                        if (Lc_Largo > 9)
                        {
                            Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaVia, Lc_Largo - 2, 3);
                            Lc_ReceiverNumber = Format.StringToDouble(Lc_BaseNumber + VB6Helpers.Mid(Lc_CuentaVia, 1, 1) + Lc_Correlativo);
                        }
                        else
                        {
                            Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaVia, Lc_Largo - 2, 3);
                            Lc_ReceiverNumber = Format.StringToDouble(Lc_BaseNumber + "0" + Lc_Correlativo);
                        }

                        //    Else
                        //        Lc_Largo = Len(Trim(Lc_CuentaOri))
                        //        Lc_Correlativo = Mid$(Lc_CuentaOri, Lc_Largo - 2, 3)
                        //        Lc_ReceiverNumber = Lc_BaseNumber & "0" & Lc_Correlativo
                        //    End If
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia != "CtaCte")
                    {
                        Lc_ReceiverNumber = Format.StringToDouble("00000000000");
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        if (IdcCtaCte == 1)
                        {
                            if (string.IsNullOrEmpty(Lc_CuentaVia))
                            {
                                Lc_ReceiverNumber = 0;
                            }
                            else
                            {
                                Lc_Largo = (short)VB6Helpers.Len(VB6Helpers.Trim(Lc_CuentaVia));
                                if (Lc_Largo > 9)
                                {
                                    Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaVia, Lc_Largo - 2, 3);
                                    Lc_ReceiverNumber = Format.StringToDouble(Lc_BaseNumber + VB6Helpers.Mid(Lc_CuentaVia, 1, 1) + Lc_Correlativo);
                                }
                                else
                                {
                                    Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaVia, Lc_Largo - 2, 3);
                                    Lc_ReceiverNumber = Format.StringToDouble(Lc_BaseNumber + "0" + Lc_Correlativo);
                                }

                            }

                        }
                        else if (IdcCtaCte == 2)
                        {
                            Lc_ReceiverNumber = 0;
                        }

                    }

                    //------------------------------------------------------------------
                    //-- Campo 15) SWIFT CURRENCY CODE (código de moneda swift) --------
                    //-- Nemotécnico ---------------------------------------------------
                    //-- Campo 16) CURRENCY CODE código de moneda swift) ---------------
                    //-- Numerico ------------------------------------------------------
                    //------------------------------------------------------------------
                    string Lc_MndCargo = "";
                    string Li_MndCargo = "";
                    string Lc_MndAbono = "";
                    string Li_MndAbono = "";

                    string Lc_SwfCurrCode = "";
                    short Li_CurrCode = 0;

                    short c = 0;
                    //Obtiene Moneda Swift para consulta
                    if (Format.StringToDouble(Lc_Sin_Via) == 0)
                    {
                        for (c = 0; c <= (short)VB6Helpers.UBound(initObject.MODGTAB0.VMnd); c++)
                        {
                            if (MODGPYF1.Minuscula(initObject.MODGTAB0.VMnd[c].Mnd_MndNmc) == MODGPYF1.Minuscula(initObject.MODXVIA.VxVia[indvia].NemMon))
                            {
                                Lc_MndAbono = VB6Helpers.Trim(initObject.MODGTAB0.VMnd[c].Mnd_MndSwf);
                                break;
                            }

                        }

                        Li_MndAbono = VB6Helpers.CStr(initObject.MODXVIA.VxVia[indvia].CodMon);
                    }

                    //Obtiene Moneda Swift para consulta
                    for (c = 0; c <= (short)VB6Helpers.UBound(initObject.MODGTAB0.VMnd); c++)
                    {
                        if (MODGPYF1.Minuscula(initObject.MODGTAB0.VMnd[c].Mnd_MndNmc) == MODGPYF1.Minuscula(initObject.MODXORI.VxOri[indori].NemMon))
                        {
                            Lc_MndCargo = VB6Helpers.Trim(initObject.MODGTAB0.VMnd[c].Mnd_MndSwf);
                            break;
                        }

                    }

                    Li_MndCargo = VB6Helpers.CStr(initObject.MODXORI.VxOri[indori].CodMon);

                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    if (tipo_cuentaOri != "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        Lc_SwfCurrCode = Lc_MndAbono;
                        Li_CurrCode = VB6Helpers.CShort(Li_MndAbono);
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia != "CtaCte")
                    {
                        Lc_SwfCurrCode = Lc_MndCargo;
                        Li_CurrCode = VB6Helpers.CShort(Li_MndCargo);
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        if (IdcCtaCte == 1)
                        {
                            Lc_SwfCurrCode = Lc_MndAbono;
                            Li_CurrCode = VB6Helpers.CShort(Li_MndAbono);
                        }
                        else if (IdcCtaCte == 2)
                        {
                            Lc_SwfCurrCode = Lc_MndCargo;
                            Li_CurrCode = VB6Helpers.CShort(Li_MndCargo);
                        }

                    }

                    //------------------------------------------------------------------
                    //-- Campo 17) CHARGES DEBIT ACCOUNT NUMBER-------------------------
                    //------------------------------------------------------------------
                    //-- Campo 50) COMMISSION AMOUNT -----------------------------------
                    //------------------------------------------------------------------
                    double Ln_Comi_Cuenta = 0;
                    double Ln_Comi_Monto = 0;

                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    if (tipo_cuentaOri != "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        Ln_Comi_Monto = 0;
                        Ln_Comi_Cuenta = 0;
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia != "CtaCte")
                    {
                        Ln_Comi_Monto = 0;
                        Ln_Comi_Cuenta = 0;
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        if (IdcCtaCte == 1)
                        {
                            Ln_Comi_Monto = 0;
                            Ln_Comi_Cuenta = 0;
                        }
                        else if (IdcCtaCte == 2)
                        {
                            Ln_Comi_Monto = 0;
                            Ln_Comi_Cuenta = 0;
                        }

                    }

                    //--------------------------------------------------------------
                    //--Campo 18) TRANSFER AMOUNT ----------------------------------
                    //--------------------------------------------------------------
                    double Ln_Monto = 0;

                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    if (tipo_cuentaOri != "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        Ln_Monto = initObject.MODXVIA.VxVia[indvia].MtoTot;
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia != "CtaCte")
                    {
                        Ln_Monto = initObject.MODXORI.VxOri[indori].MtoTot;
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        if (IdcCtaCte == 1)
                        {
                            Ln_Monto = initObject.MODXVIA.VxVia[indvia].MtoTot;
                        }
                        else if (IdcCtaCte == 2)
                        {
                            Ln_Monto = initObject.MODXORI.VxOri[indori].MtoTot;
                        }

                    }

                    //--------------------------------------------------------------
                    //--Campo 24) TEXT ---------------------------------------------
                    //--------------------------------------------------------------
                    string Lc_Text = "";
                    //busca indice party dentro de estructura
                    short Contador = 0;
                    for (Contador = 0; Contador <= (short)VB6Helpers.UBound(initObject.Module1.PartysOpe); Contador++)
                    {
                        if (VB6Helpers.Trim(IdPrty) == VB6Helpers.Trim(initObject.Module1.PartysOpe[Contador].LlaveArchivo))
                        {
                            break;
                        }

                    }

                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    if (tipo_cuentaOri != "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        Lc_Text = initObject.Module1.PartysOpe[Contador].NombreUsado;
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia != "CtaCte")
                    {
                        //---------------------------------------------
                        //Realsystems-Código Nuevo-Inicio
                        //Fecha Modificación 20100713
                        //Responsable: Pablo Millan V.
                        //Versión: 1.0
                        //Descripción : Se cambia valor campo 24
                        //---------------------------------------------
                        if (string.IsNullOrWhiteSpace(initObject.MODXORI.VxOri[indori].Text1))
                        {
                            Lc_Text = "INTERNAL ACCOUNT";
                        }
                        else
                        {
                            Lc_Text = VB6Helpers.Trim(initObject.MODXORI.VxOri[indori].Text1);
                        }

                        //----------------------------------------
                        // RealSystems - Código Nuevo - Termino
                        //----------------------------------------
                        // RealSystems - Código Antiguo - Inicio
                        //----------------------------------------
                        //        Lc_Text = "INTERNAL ACCOUNT"
                        //----------------------------------------
                        // RealSystems - Código Antiguo - Termino
                        //----------------------------------------
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        if (IdcCtaCte == 1)
                        {
                            Lc_Text = initObject.Module1.PartysOpe[Contador].NombreUsado;
                        }
                        else if (IdcCtaCte == 2)
                        {
                            //---------------------------------------------
                            //Realsystems-Código Nuevo-Inicio
                            //Fecha Modificación 20100713
                            //Responsable: Pablo Millan V.
                            //Versión: 1.0
                            //Descripción : Se cambia valor campo 24
                            //---------------------------------------------
                            if (string.IsNullOrWhiteSpace(initObject.MODXORI.VxOri[indori].Text1))
                            {
                                Lc_Text = "INTERNAL ACCOUNT";
                            }
                            else
                            {
                                Lc_Text = VB6Helpers.Trim(initObject.MODXORI.VxOri[indori].Text1);
                            }

                            //----------------------------------------
                            // RealSystems - Código Nuevo - Termino
                            //----------------------------------------
                            // RealSystems - Código Antiguo - Inicio
                            //----------------------------------------
                            //        Lc_Text = "INTERNAL ACCOUNT"
                            //----------------------------------------
                            // RealSystems - Código Antiguo - Termino
                            //----------------------------------------
                        }

                    }

                    //--------------------------------------------------------------
                    //--Campo 26) BY ORDER OF  -------------------------------------
                    //--------------------------------------------------------------
                    string Lc_ByOrder = "";

                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                    if (tipo_cuentaOri != "CtaCte" && tipo_cuentaVia == "CtaCte" && tipo_cuentaOri != "GAP")
                    {
                        //---------------------------------------------
                        //Realsystems-Código Nuevo-Inicio
                        //Fecha Modificación 20100713
                        //Responsable: Pablo Millan V.
                        //Versión: 1.0
                        //Descripción : Se cambia valor campo 26
                        //---------------------------------------------
                        if (string.IsNullOrWhiteSpace(initObject.MODXVIA.VxVia[indvia].Text1))
                        {
                            Lc_ByOrder = "INTERNAL ACCOUNT";
                        }
                        else
                        {
                            Lc_ByOrder = VB6Helpers.Trim(initObject.MODXVIA.VxVia[indvia].Text1);
                        }

                        //----------------------------------------
                        // RealSystems - Código Nuevo - Termino
                        //----------------------------------------
                        // RealSystems - Código Antiguo - Inicio
                        //----------------------------------------
                        //        Lc_ByOrder = "INTERNAL ACCOUNT"
                        //----------------------------------------
                        // RealSystems - Código Antiguo - Termino
                        //----------------------------------------
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia != "CtaCte")
                    {
                        Lc_ByOrder = initObject.Module1.PartysOpe[Contador].NombreUsado;
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        if (IdcCtaCte == 1)
                        {
                            //---------------------------------------------
                            //Realsystems-Código Nuevo-Inicio
                            //Fecha Modificación 20100713
                            //Responsable: Pablo Millan V.
                            //Versión: 1.0
                            //Descripción : Se cambia valor campo 26
                            //---------------------------------------------
                            if (string.IsNullOrWhiteSpace(initObject.MODXVIA.VxVia[indvia].Text1))
                            {
                                Lc_ByOrder = "INTERNAL ACCOUNT";
                            }
                            else
                            {
                                Lc_ByOrder = VB6Helpers.Trim(initObject.MODXVIA.VxVia[indvia].Text1);
                            }

                            //----------------------------------------
                            // RealSystems - Código Nuevo - Termino
                            //----------------------------------------
                            // RealSystems - Código Antiguo - Inicio
                            //----------------------------------------
                            //        Lc_ByOrder = "INTERNAL ACCOUNT"
                            //----------------------------------------
                            // RealSystems - Código Antiguo - Termino
                            //----------------------------------------
                        }
                        else if (IdcCtaCte == 2)
                        {
                            Lc_ByOrder = initObject.Module1.PartysOpe[Contador].NombreUsado;
                        }

                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "GAP" && tipo_cuentaVia == "CtaCte")
                    {
                        Lc_ByOrder = initObject.Module1.PartysOpe[Contador].NombreUsado;
                    }

                    //--------------------------------------------------------------
                    //--Campo 27) BENEFICIARY --------------------------------------
                    //--------------------------------------------------------------
                    string Lc_Beneficiary = "";

                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    if (tipo_cuentaOri != "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        Lc_Beneficiary = initObject.Module1.PartysOpe[Contador].NombreUsado;
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia != "CtaCte")
                    {
                        //---------------------------------------------
                        //Realsystems-Código Nuevo-Inicio
                        //Fecha Modificación 20100713
                        //Responsable: Pablo Millan V.
                        //Versión: 1.0
                        //Descripción : Se cambia valor campo 27
                        //---------------------------------------------
                        if (string.IsNullOrWhiteSpace(initObject.MODXORI.VxOri[indori].Text1))
                        {
                            Lc_Beneficiary = "INTERNAL ACCOUNT";
                        }
                        else
                        {
                            Lc_Beneficiary = VB6Helpers.Trim(initObject.MODXORI.VxOri[indori].Text1);
                        }

                        //----------------------------------------
                        // RealSystems - Código Nuevo - Termino
                        //----------------------------------------
                        // RealSystems - Código Antiguo - Inicio
                        //----------------------------------------
                        //    Lc_Beneficiary = "INTERNAL ACCOUNT"
                        //----------------------------------------
                        // RealSystems - Código Antiguo - Termino
                        //----------------------------------------
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        if (IdcCtaCte == 1)
                        {
                            Lc_Beneficiary = initObject.Module1.PartysOpe[Contador].NombreUsado;
                        }
                        else if (IdcCtaCte == 2)
                        {
                            //---------------------------------------------
                            //Realsystems-Código Nuevo-Inicio
                            //Fecha Modificación 20100713
                            //Responsable: Pablo Millan V.
                            //Versión: 1.0
                            //Descripción : Se cambia valor campo 27
                            //---------------------------------------------
                            if (string.IsNullOrWhiteSpace(initObject.MODXORI.VxOri[indori].Text1))
                            {
                                Lc_Beneficiary = "INTERNAL ACCOUNT";
                            }
                            else
                            {
                                Lc_Beneficiary = VB6Helpers.Trim(initObject.MODXORI.VxOri[indori].Text1);
                            }

                            //----------------------------------------
                            // RealSystems - Código Nuevo - Termino
                            //----------------------------------------
                            // RealSystems - Código Antiguo - Inicio
                            //----------------------------------------
                            //        Lc_Beneficiary = "INTERNAL ACCOUNT"
                            //----------------------------------------
                            // RealSystems - Código Antiguo - Termino
                            //----------------------------------------
                        }

                    }

                    //--------------------------------------------------------------
                    //--Campo 35) ORDERING CUSTOMER ACCOUNT NUMBER -----------------
                    //--------------------------------------------------------------
                    string Lc_OrderCustAccount = "";

                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    if (tipo_cuentaOri != "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        if (tipo_cuentaOri == "GAP")
                        {
                            Lc_Largo = (short)VB6Helpers.Len(VB6Helpers.Trim(Lc_CuentaOri));
                            if (Lc_Largo > 9)
                            {
                                Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaOri, Lc_Largo - 2, 3);
                                Lc_OrderCustAccount = Lc_BaseNumber + VB6Helpers.Mid(Lc_CuentaOri, 1, 1) + Lc_Correlativo;
                            }
                            else
                            {
                                Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaOri, Lc_Largo - 2, 3);
                                Lc_OrderCustAccount = Lc_BaseNumber + "0" + Lc_Correlativo;
                            }

                        }
                        else
                        {
                            Lc_Largo = (short)VB6Helpers.Len(VB6Helpers.Trim(Lc_CuentaOri));
                            Lc_OrderCustAccount = VB6Helpers.Mid(Lc_CuentaOri, 1, Lc_Largo - 1);
                        }

                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia != "CtaCte")
                    {
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                        if (tipo_cuentaVia == "CONTABLE" || tipo_cuentaVia == "CORRESPONSAL")
                        {
                            if (string.IsNullOrEmpty(Lc_CuentaOri))
                            {
                                Lc_OrderCustAccount = "0";
                            }
                            else
                            {
                                Lc_Largo = (short)VB6Helpers.Len(VB6Helpers.Trim(Lc_CuentaOri));
                                if (Lc_Largo > 9)
                                {
                                    Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaOri, Lc_Largo - 2, 3);
                                    Lc_OrderCustAccount = Lc_BaseNumber + VB6Helpers.Mid(Lc_CuentaOri, 1, 1) + Lc_Correlativo;
                                }
                                else
                                {
                                    Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaOri, Lc_Largo - 2, 3);
                                    Lc_OrderCustAccount = Lc_BaseNumber + "0" + Lc_Correlativo;
                                }

                            }

                            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                        }
                        else if (string.IsNullOrEmpty(tipo_cuentaVia))
                        {
                            Lc_OrderCustAccount = "0";
                        }
                        else
                        {
                            Lc_Largo = (short)VB6Helpers.Len(VB6Helpers.Trim(Lc_CuentaVia));
                            if (Lc_Largo > 9)
                            {
                                Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaVia, Lc_Largo - 2, 3);
                                Lc_OrderCustAccount = Lc_BaseNumber + VB6Helpers.Mid(Lc_CuentaVia, 1, 1) + Lc_Correlativo;
                            }
                            else
                            {
                                Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaVia, Lc_Largo - 2, 3);
                                Lc_OrderCustAccount = Lc_BaseNumber + "0" + Lc_Correlativo;
                            }

                        }

                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        if (IdcCtaCte == 1)
                        {
                            if (Li_CurrCode == 1)
                            {
                                MODGNCTA.SyGet_Cta(("ENLACEMNCITI"), initObject, unit);
                            }
                            else
                            {
                                MODGNCTA.SyGet_Cta(("ENLAMECITI"), initObject, unit);
                            }

                            for (i = 0; i <= (short)VB6Helpers.UBound(initObject.MODGNCTA.VCta); i++)
                            {
                                if (Li_CurrCode == 1)
                                {
                                    if (VB6Helpers.UCase(VB6Helpers.Trim(initObject.MODGNCTA.VCta[i].Cta_Nem.Value)) == "ENLACEMNCITI")
                                    {
                                        Lc_OrderCustAccount = initObject.MODGNCTA.VCta[i].Cta_Num.Value;
                                        break;
                                    }

                                }
                                else
                                {
                                    if (VB6Helpers.UCase(VB6Helpers.Trim(initObject.MODGNCTA.VCta[i].Cta_Nem.Value)) == "ENLAMECITI")
                                    {
                                        Lc_OrderCustAccount = initObject.MODGNCTA.VCta[i].Cta_Num.Value;
                                        break;
                                    }

                                }

                            }

                            Lc_Largo = (short)VB6Helpers.Len(VB6Helpers.Trim(Lc_OrderCustAccount));
                            Lc_OrderCustAccount = VB6Helpers.Mid(Lc_OrderCustAccount, 1, Lc_Largo - 1);
                        }
                        else if (IdcCtaCte == 2)
                        {
                            if (string.IsNullOrEmpty(Lc_CuentaOri))
                            {
                                Lc_OrderCustAccount = "0";
                            }
                            else
                            {
                                Lc_Largo = (short)VB6Helpers.Len(VB6Helpers.Trim(Lc_CuentaOri));
                                if (Lc_Largo > 9)
                                {
                                    Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaOri, Lc_Largo - 2, 3);
                                    Lc_OrderCustAccount = Lc_BaseNumber + VB6Helpers.Mid(Lc_CuentaOri, 1, 1) + Lc_Correlativo;
                                }
                                else
                                {
                                    Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaOri, Lc_Largo - 2, 3);
                                    Lc_OrderCustAccount = Lc_BaseNumber + "0" + Lc_Correlativo;
                                }

                            }

                        }

                    }

                    //-------------------------------------------------------
                    //--Campo 36) INPUT DATE
                    //-------------------------------------------------------
                    //--Campo 44) INPUT DATE
                    //-------------------------------------------------------
                    // UPGRADE_INFO (#0561): The 'Li_dif_dias' symbol was defined without an explicit "As" clause.
                    dynamic Li_dif_dias = VB6Helpers.DateDiff("d", VB6Helpers.CDate("01/01/2000"), DateTime.Now);

                    //--------------------------------------------------------------
                    //--Campo 43) RECEIVER ACCOUNT NUMBER --------------------------
                    //--------------------------------------------------------------
                    string Lc_ReceiverAccount = "";

                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    if (tipo_cuentaOri != "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        if (tipo_cuentaOri == "CONTABLE" || tipo_cuentaOri == "CORRESPONSAL")
                        {
                            Lc_Largo = (short)VB6Helpers.Len(VB6Helpers.Trim(Lc_CuentaVia));
                            if (Lc_Largo > 9)
                            {
                                Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaVia, Lc_Largo - 2, 3);
                                Lc_ReceiverAccount = Lc_BaseNumber + VB6Helpers.Mid(Lc_CuentaVia, 1, 1) + Lc_Correlativo;
                            }
                            else
                            {
                                Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaVia, Lc_Largo - 2, 3);
                                Lc_ReceiverAccount = Lc_BaseNumber + "0" + Lc_Correlativo;
                            }

                            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        }
                        else if (tipo_cuentaOri == "GAP")
                        {
                            Lc_Largo = (short)VB6Helpers.Len(VB6Helpers.Trim(Lc_CuentaVia));
                            if (Lc_Largo > 9)
                            {
                                Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaVia, Lc_Largo - 2, 3);
                                Lc_ReceiverAccount = Lc_BaseNumber + VB6Helpers.Mid(Lc_CuentaVia, 1, 1) + Lc_Correlativo;
                            }
                            else
                            {
                                Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaVia, Lc_Largo - 2, 3);
                                Lc_ReceiverAccount = Lc_BaseNumber + "0" + Lc_Correlativo;
                            }

                        }

                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia != "CtaCte")
                    {
                        if (string.IsNullOrEmpty(Lc_CuentaVia))
                        {
                            Lc_ReceiverAccount = "0";
                        }
                        else
                        {
                            Lc_Largo = (short)VB6Helpers.Len(VB6Helpers.Trim(Lc_CuentaVia));
                            Lc_ReceiverAccount = VB6Helpers.Mid(Lc_CuentaVia, 1, Lc_Largo - 1);
                        }

                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        if (IdcCtaCte == 1)
                        {
                            if (string.IsNullOrEmpty(Lc_CuentaVia))
                            {
                                Lc_ReceiverAccount = "0";
                            }
                            else
                            {
                                Lc_Largo = (short)VB6Helpers.Len(VB6Helpers.Trim(Lc_CuentaVia));
                                if (Lc_Largo > 9)
                                {
                                    Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaVia, Lc_Largo - 2, 3);
                                    Lc_ReceiverAccount = Lc_BaseNumber + VB6Helpers.Mid(Lc_CuentaVia, 1, 1) + Lc_Correlativo;
                                }
                                else
                                {
                                    Lc_Correlativo = VB6Helpers.Mid(Lc_CuentaVia, Lc_Largo - 2, 3);
                                    Lc_ReceiverAccount = Lc_BaseNumber + "0" + Lc_Correlativo;
                                }

                            }

                        }
                        else if (IdcCtaCte == 2)
                        {
                            if (Li_CurrCode == 1)
                            {
                                MODGNCTA.SyGet_Cta(("ENLACEMNCITI"), initObject, unit);
                            }
                            else
                            {
                                MODGNCTA.SyGet_Cta(("ENLAMECITI"), initObject, unit);
                            }

                            for (i = 0; i <= (short)VB6Helpers.UBound(initObject.MODGNCTA.VCta); i++)
                            {
                                if (Li_CurrCode == 1)
                                {
                                    if (VB6Helpers.UCase(VB6Helpers.Trim(initObject.MODGNCTA.VCta[i].Cta_Nem.Value)) == "ENLACEMNCITI")
                                    {
                                        Lc_ReceiverAccount = initObject.MODGNCTA.VCta[i].Cta_Num.Value;
                                        break;
                                    }

                                }
                                else
                                {
                                    if (VB6Helpers.UCase(VB6Helpers.Trim(initObject.MODGNCTA.VCta[i].Cta_Nem.Value)) == "ENLAMECITI")
                                    {
                                        Lc_ReceiverAccount = initObject.MODGNCTA.VCta[i].Cta_Num.Value;
                                        break;
                                    }

                                }

                            }

                            Lc_Largo = (short)VB6Helpers.Len(VB6Helpers.Trim(Lc_ReceiverAccount));
                            Lc_ReceiverAccount = VB6Helpers.Mid(Lc_ReceiverAccount, 1, Lc_Largo - 1);
                        }

                    }

                    //--------------------------------------------------------------
                    //--Campo 63) NUMBER OF LINES TEXT FOR ORDERING CUSTOMER -------
                    //--------------------------------------------------------------
                    short Li_Ordering = 0;
                    string Lc_Ordering = "";

                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    if (tipo_cuentaOri != "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        Li_Ordering = 1;
                        //---------------------------------------------
                        //Realsystems-Código Nuevo-Inicio
                        //Fecha Modificación 20100713
                        //Responsable: Pablo Millan V.
                        //Versión: 1.0
                        //Descripción : Se cambia valor campo 63
                        //---------------------------------------------
                        if (string.IsNullOrWhiteSpace(initObject.MODXVIA.VxVia[indvia].Text1))
                        {
                            //        Lc_Ordering = "INTERNAL ACCOUNT                   " ---Solicitud de cambio por usuario
                            Lc_Ordering = "";
                            Li_Ordering = 0;
                        }
                        else
                        {
                            Lc_Ordering = llena_blancos_der(VB6Helpers.Mid(initObject.MODXVIA.VxVia[indvia].Text1, 1, 35), 35);
                        }

                        //----------------------------------------
                        // RealSystems - Código Nuevo - Termino
                        //----------------------------------------
                        // RealSystems - Código Antiguo - Inicio
                        //----------------------------------------
                        //   Lc_Ordering = "INTERNAL ACCOUNT                   "
                        //----------------------------------------
                        // RealSystems - Código Antiguo - Termino
                        //----------------------------------------
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia != "CtaCte")
                    {
                        Li_Ordering = 4;
                        Lc_Ordering = VB6Helpers.Mid(initObject.Module1.PartysOpe[Contador].NombreUsado, 1, 35);
                        Lc_Ordering = llena_blancos_der(Lc_Ordering, 35);
                        Lc_Ordering += VB6Helpers.Mid(initObject.Module1.PartysOpe[Contador].NombreUsado, 36, 70);
                        Lc_Ordering = llena_blancos_der(Lc_Ordering, 70);
                        Lc_Ordering += VB6Helpers.Mid(initObject.Module1.PartysOpe[Contador].DireccionUsado, 1, 35);
                        Lc_Ordering = llena_blancos_der(Lc_Ordering, 105);
                        Lc_Ordering = Lc_Ordering + VB6Helpers.Mid(initObject.Module1.PartysOpe[Contador].CiudadUsado + " " + initObject.Module1.PartysOpe[Contador].PaisUsado, 1, 35);
                        Lc_Ordering = llena_blancos_der(Lc_Ordering, 140);

                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        if (IdcCtaCte == 1)
                        {
                            Li_Ordering = 1;
                            //---------------------------------------------
                            //Realsystems-Código Nuevo-Inicio
                            //Fecha Modificación 20100713
                            //Responsable: Pablo Millan V.
                            //Versión: 1.0
                            //Descripción : Se cambia valor campo 63
                            //---------------------------------------------
                            if (string.IsNullOrEmpty(initObject.MODXVIA.VxVia[indvia].Text1))
                            {
                                //            Lc_Ordering = "INTERNAL ACCOUNT                   "---Solicitud de cambio por usuario
                                Lc_Ordering = "";
                                Li_Ordering = 0;
                            }
                            else
                            {
                                Lc_Ordering = llena_blancos_der(VB6Helpers.Mid(initObject.MODXVIA.VxVia[indvia].Text1, 1, 35), 35);
                            }

                            //----------------------------------------
                            // RealSystems - Código Nuevo - Termino
                            //----------------------------------------
                            // RealSystems - Código Antiguo - Inicio
                            //----------------------------------------
                            //       Lc_Ordering = "INTERNAL ACCOUNT                   "
                            //----------------------------------------
                            // RealSystems - Código Antiguo - Termino
                            //----------------------------------------
                        }
                        else if (IdcCtaCte == 2)
                        {
                            Li_Ordering = 4;
                            Lc_Ordering = VB6Helpers.Mid(initObject.Module1.PartysOpe[Contador].NombreUsado, 1, 35);
                            Lc_Ordering = llena_blancos_der(Lc_Ordering, 35);
                            Lc_Ordering += VB6Helpers.Mid(initObject.Module1.PartysOpe[Contador].NombreUsado, 36, 70);
                            Lc_Ordering = llena_blancos_der(Lc_Ordering, 70);
                            Lc_Ordering += VB6Helpers.Mid(initObject.Module1.PartysOpe[Contador].DireccionUsado, 1, 35);
                            Lc_Ordering = llena_blancos_der(Lc_Ordering, 105);
                            Lc_Ordering = Lc_Ordering + VB6Helpers.Mid(initObject.Module1.PartysOpe[Contador].CiudadUsado + " " + initObject.Module1.PartysOpe[Contador].PaisUsado, 1, 35);
                            Lc_Ordering = llena_blancos_der(Lc_Ordering, 140);
                        }

                    }

                    //--------------------------------------------------------------
                    //--Campo 64) NUMBER OF LINES TEXT FOR RECEIVER ----------------
                    //--------------------------------------------------------------
                    short Li_Receiver = 0;
                    string Lc_Text_Receiver = "";

                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    if (tipo_cuentaOri != "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        Li_Receiver = 4;
                        Lc_Text_Receiver = VB6Helpers.Mid(initObject.Module1.PartysOpe[Contador].NombreUsado, 1, 35);
                        Lc_Text_Receiver = llena_blancos_der(Lc_Text_Receiver, 35);
                        Lc_Text_Receiver += VB6Helpers.Mid(initObject.Module1.PartysOpe[Contador].NombreUsado, 36, 70);
                        Lc_Text_Receiver = llena_blancos_der(Lc_Text_Receiver, 70);
                        Lc_Text_Receiver += VB6Helpers.Mid(initObject.Module1.PartysOpe[Contador].DireccionUsado, 1, 35);
                        Lc_Text_Receiver = llena_blancos_der(Lc_Text_Receiver, 105);
                        Lc_Text_Receiver = Lc_Text_Receiver + VB6Helpers.Mid(initObject.Module1.PartysOpe[Contador].CiudadUsado + " " + initObject.Module1.PartysOpe[Contador].PaisUsado, 1, 35);
                        Lc_Text_Receiver = llena_blancos_der(Lc_Text_Receiver, 140);
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia != "CtaCte")
                    {
                        Li_Receiver = 0;
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        if (IdcCtaCte == 1)
                        {
                            Li_Receiver = 4;
                            Lc_Text_Receiver = VB6Helpers.Mid(initObject.Module1.PartysOpe[Contador].NombreUsado, 1, 35);
                            Lc_Text_Receiver = llena_blancos_der(Lc_Text_Receiver, 35);
                            Lc_Text_Receiver += VB6Helpers.Mid(initObject.Module1.PartysOpe[Contador].NombreUsado, 36, 70);
                            Lc_Text_Receiver = llena_blancos_der(Lc_Text_Receiver, 70);
                            Lc_Text_Receiver += VB6Helpers.Mid(initObject.Module1.PartysOpe[Contador].DireccionUsado, 1, 35);
                            Lc_Text_Receiver = llena_blancos_der(Lc_Text_Receiver, 105);
                            Lc_Text_Receiver = Lc_Text_Receiver + VB6Helpers.Mid(initObject.Module1.PartysOpe[Contador].CiudadUsado + " " + initObject.Module1.PartysOpe[Contador].PaisUsado, 1, 35);
                            Lc_Text_Receiver = llena_blancos_der(Lc_Text_Receiver, 140);
                        }
                        else if (IdcCtaCte == 2)
                        {
                            Li_Receiver = 0;
                        }

                    }

                    //---------------------------------------------
                    //Realsystems-Código Nuevo-Inicio
                    //Fecha Modificación 20100713
                    //Responsable: Pablo Millan V.
                    //Versión: 1.0
                    //Descripción : Se cambia valor campo 65
                    //---------------------------------------------

                    //--------------------------------------------------------------
                    //--Campo 65) BENEFICIARY BANK ---------------------------------
                    //--------------------------------------------------------------
                    string Lc_BenefBank = "";

                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    if (tipo_cuentaOri != "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        Lc_BenefBank = "1";
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia != "CtaCte")
                    {
                        if (string.IsNullOrEmpty(initObject.MODXORI.VxOri[indori].Text1))
                        {
                            Lc_BenefBank = "0";
                        }
                        else
                        {
                            Lc_BenefBank = "1";
                        }

                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        if (IdcCtaCte == 1)
                        {
                            Lc_BenefBank = "1";
                        }
                        else if (IdcCtaCte == 2)
                        {
                            if (string.IsNullOrEmpty(initObject.MODXORI.VxOri[indori].Text1))
                            {
                                Lc_BenefBank = "0";
                            }
                            else
                            {
                                Lc_BenefBank = "1";
                            }

                        }

                    }

                    //----------------------------------------
                    // RealSystems - Código Nuevo - Termino
                    //----------------------------------------

                    //--------------------------------------------------------------
                    //--Campo 67) NUMBER OF LINES TEXT FOR REASON ------------------
                    //--------------------------------------------------------------
                    short Li_Reason = 0;
                    string Lc_Reason1 = "";
                    string Lc_Reason2 = "";
                    string Lc_Reason3 = "";
                    string Lc_Reason4 = "";

                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    if (tipo_cuentaOri != "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        if (!string.IsNullOrEmpty(initObject.MODXVIA.VxVia[indvia].Text2))
                        {
                            Li_Reason = (short)(Li_Reason + 1);
                            Lc_Reason1 += initObject.MODXVIA.VxVia[indvia].Text2;
                            Lc_Reason1 = llena_blancos_der(Lc_Reason1, 35);
                        }

                        if (!string.IsNullOrEmpty(initObject.MODXVIA.VxVia[indvia].Text3))
                        {
                            Li_Reason = (short)(Li_Reason + 1);
                            Lc_Reason2 += initObject.MODXVIA.VxVia[indvia].Text3;
                            Lc_Reason2 = llena_blancos_der(Lc_Reason2, 35);
                        }

                        if (!string.IsNullOrEmpty(initObject.MODXVIA.VxVia[indvia].Text4))
                        {
                            Li_Reason = (short)(Li_Reason + 1);
                            Lc_Reason3 += initObject.MODXVIA.VxVia[indvia].Text4;
                            Lc_Reason3 = llena_blancos_der(Lc_Reason3, 35);
                        }

                        Lc_Reason4 = Lc_Reason1 + Lc_Reason2 + Lc_Reason3;
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia != "CtaCte")
                    {
                        if (!string.IsNullOrEmpty(initObject.MODXORI.VxOri[indori].Text2))
                        {
                            Li_Reason = (short)(Li_Reason + 1);
                            Lc_Reason1 += initObject.MODXORI.VxOri[indori].Text2;
                            Lc_Reason1 = llena_blancos_der(Lc_Reason1, 35);
                        }

                        if (!string.IsNullOrEmpty(initObject.MODXORI.VxOri[indori].Text3))
                        {
                            Li_Reason = (short)(Li_Reason + 1);
                            Lc_Reason2 += initObject.MODXORI.VxOri[indori].Text3;
                            Lc_Reason2 = llena_blancos_der(Lc_Reason2, 35);
                        }

                        if (!string.IsNullOrEmpty(initObject.MODXORI.VxOri[indori].Text4))
                        {
                            Li_Reason = (short)(Li_Reason + 1);
                            Lc_Reason3 += initObject.MODXORI.VxOri[indori].Text4;
                            Lc_Reason3 = llena_blancos_der(Lc_Reason3, 35);
                        }

                        Lc_Reason4 = Lc_Reason1 + Lc_Reason2 + Lc_Reason3;
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaOri'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'tipo_cuentaVia'. Consider using the GetDefaultMember6 helper method.
                    }
                    else if (tipo_cuentaOri == "CtaCte" && tipo_cuentaVia == "CtaCte")
                    {
                        if (IdcCtaCte == 1)
                        {
                            if (!string.IsNullOrEmpty(initObject.MODXVIA.VxVia[indvia].Text2))
                            {
                                Li_Reason = (short)(Li_Reason + 1);
                                Lc_Reason1 += initObject.MODXVIA.VxVia[indvia].Text2;
                                Lc_Reason1 = llena_blancos_der(Lc_Reason1, 35);
                            }

                            if (!string.IsNullOrEmpty(initObject.MODXVIA.VxVia[indvia].Text3))
                            {
                                Li_Reason = (short)(Li_Reason + 1);
                                Lc_Reason2 += initObject.MODXVIA.VxVia[indvia].Text3;
                                Lc_Reason2 = llena_blancos_der(Lc_Reason2, 35);
                            }

                            if (!string.IsNullOrEmpty(initObject.MODXVIA.VxVia[indvia].Text4))
                            {
                                Li_Reason = (short)(Li_Reason + 1);
                                Lc_Reason3 += initObject.MODXVIA.VxVia[indvia].Text4;
                                Lc_Reason3 = llena_blancos_der(Lc_Reason3, 35);
                            }

                        }
                        else if (IdcCtaCte == 2)
                        {
                            if (!string.IsNullOrEmpty(initObject.MODXORI.VxOri[indori].Text2))
                            {
                                Li_Reason = (short)(Li_Reason + 1);
                                Lc_Reason1 += initObject.MODXORI.VxOri[indori].Text2;
                                Lc_Reason1 = llena_blancos_der(Lc_Reason1, 35);
                            }

                            if (!string.IsNullOrEmpty(initObject.MODXORI.VxOri[indori].Text3))
                            {
                                Li_Reason = (short)(Li_Reason + 1);
                                Lc_Reason2 += initObject.MODXORI.VxOri[indori].Text3;
                                Lc_Reason2 = llena_blancos_der(Lc_Reason2, 35);
                            }

                            if (!string.IsNullOrEmpty(initObject.MODXORI.VxOri[indori].Text4))
                            {
                                Li_Reason = (short)(Li_Reason + 1);
                                Lc_Reason3 += initObject.MODXORI.VxOri[indori].Text4;
                                Lc_Reason3 = llena_blancos_der(Lc_Reason3, 35);
                            }

                        }

                        Lc_Reason4 = Lc_Reason4 + Lc_Reason1 + Lc_Reason2 + Lc_Reason3;
                    }

                    //--------------------------------------------------------------
                    //--Campo 70) TOTAL NUMBER OF LINES OF TEXT --------------------
                    //--------------------------------------------------------------
                    short Li_Total = 0;

                    Li_Total = (short)(Li_Ordering + Li_Receiver + Li_Reason);
                    //--------------------------------------------------------------
                    //--Campo 71) TEXT LINE                     --------------------
                    //--------------------------------------------------------------
                    string Lc_Total = "";

                    Lc_Total = Lc_Ordering + Lc_Text_Receiver + Lc_Reason4;
                    //--------------------------------------------------------------
                    //--SE CONSTRUYE INSERT PARA TABLA TBL_SCE_FTS -----------------
                    //--------------------------------------------------------------
                    List<string> listaParam = new List<string>();
                    _retValue = (short)(false ? -1 : 0);

                    listaParam.Add("FTR");  //Campo 01) RECORD TYPE
                    listaParam.Add(VB6Helpers.Str(T_MODGCON0.IdBRANCH_NUMBER) != "" ? VB6Helpers.Str(T_MODGCON0.IdBRANCH_NUMBER) : "0");  //Campo 02) BRANCH NUMBER
                    listaParam.Add(Lc_ContracRefNumber.Replace("|", "") != "" ? Lc_ContracRefNumber.Replace("|", "") : "0");  //Campo 03) CONTRACT REFERENCE NUMBER
                    listaParam.Add(Lc_OrderCustomer.Replace("|", ""));  //Campo 04) ORDERING CUSTOMER    ---->11T:  OK <----
                    listaParam.Add("A");  //Campo 05) HISTORY/ACTIVE INDICATOR
                    listaParam.Add(Lc_FechaCorta != "" ? Lc_FechaCorta : "0");  //Campo 06) INPUT DATE (fecha de ingreso)
                    listaParam.Add(Lc_Receiver.Replace("|", "") != "" ? Lc_Receiver.Replace("|", "") : "0");  //Campo 07) RECEIVER (receptor)   ---->11N: OK  <----
                    listaParam.Add(Lc_FechaCorta != "" ? Lc_FechaCorta : "0"); //Campo 08) CREDIT ENTRY DATE (fecha de la partida de abono)  OK
                    listaParam.Add(VB6Helpers.Str(Lc_OrderCustNumber) != "" ? VB6Helpers.Str(Lc_OrderCustNumber) : "0");  //Campo 09) ORDERING CUSTOMER ACCOUNT NUMBER (número de cuenta del cliente que envía)  ----> 11N  (CTACTE COSMOS 01102330507) <----  OK
                    listaParam.Add(VB6Helpers.Str(Lc_ReceiverNumber) != "" ? VB6Helpers.Str(Lc_ReceiverNumber) : "0");  //Campo 10) RECEIVER ACCOUNT NUMBER (número de cuenta del receptor) ---->11N: (CTACTE COSMOS 04500004014) <----  OK
                    listaParam.Add("0");  //Campo 11) AUTHORIZATION STATUS INDICATOR (indicador del estado de autorización)  OK
                    listaParam.Add("0");  //Campo 12) TRANSACTION TYPE CODE (T: Código del tipo de transacción LC_INCOMING :1 or LC_OUTGOING:2)  OK
                    listaParam.Add("0");  //Campo 13) EXECUTION TYPE CODE - TRANSFER METHOD (método de transferencia - código del tipo de ejecución) -->1A:  0 : cuenta a cuenta.==> GAP <---  OK
                    listaParam.Add("0");  //Campo 14) PRODUCT TYPE (tipo de producto)  OK
                    listaParam.Add(Lc_SwfCurrCode != "" ? Lc_SwfCurrCode : "0");  //Campo 15) SWIFT CURRENCY CODE (T: código de moneda swift) ---> 4A vmoneda<--- ??
                    listaParam.Add(VB6Helpers.Str(Li_CurrCode) != "" ? VB6Helpers.Str(Li_CurrCode) : "0");  //Campo 16) CURRENCY CODE (N: código de moneda)  ??
                    listaParam.Add(VB6Helpers.Str(Ln_Comi_Cuenta) != "" ? VB6Helpers.Str(Ln_Comi_Cuenta) : "0");  //Campo 17) CHARGES DEBIT ACCOUNT NUMBER (número de la cuenta de cargo que se cobra)  ??
                    listaParam.Add(MODGSYB.dbmontoSyForRead(Ln_Monto) != "" ? MODGSYB.dbmontoSyForRead(Ln_Monto) : "0");  //Campo 18) TRANSFER AMOUNT (monto de la transferencia)  -->17N vmonto1<--- ??
                    listaParam.Add("+");  //Campo 19) SIGN (signo)
                    listaParam.Add(VB6Helpers.Str(0));  //Campo 20) LEGAL VEHICLE CODE (código del vehículo legal)
                    listaParam.Add(Lc_FechaCorta != "" ? Lc_FechaCorta : "0");  //Campo 21) DEBIT VALUE DATE (fecha valor del cargo)
                    listaParam.Add(Lc_FechaCorta != "" ? Lc_FechaCorta : "0");  //Campo 22) DATA ENTRY DATE (fecha del ingreso de datos)
                    listaParam.Add(Lc_FechaCorta != "" ? Lc_FechaCorta : "0");  //Campo 23) CREDIT VALUE DATE (fecha valor del abono)
                    listaParam.Add(Lc_Text);  //Campo 24) TEXT (texto)  --> 24A Nombre del Beneficiario <--
                    listaParam.Add(" ");  //Campo 25) STATUS (estado)
                    listaParam.Add(Lc_ByOrder);  //Campo 26) BY ORDER OF (ordenado por)  --> 35A Nombre del Ordenante <---
                    listaParam.Add(Lc_Beneficiary);  //Campo 27) BENEFICIARY (beneficiario)  --> 35A Nombre del Beneficiario<---
                    listaParam.Add(Lc_FechaCorta);  //Campo 28) LAST INPUT/CHANGED DATE (fecha del último ingreso o modificación)
                    listaParam.Add("0");  //Campo 29) TRANSFER CHARGES INDICATOR (indicador de los cobros de la transferencia T: "0")  se cobra al beneficiario OK
                    listaParam.Add(" ");  //Campo 30) INPUT OPERATOR ID (ID de quien ingresa)
                    listaParam.Add(Lc_FechaCorta);  //Campo 31) INPUT DATE (fecha de ingreso)
                    listaParam.Add(VB6Helpers.Str(Ll_HoraIngreso));  //Campo 32) INPUT TIME (7N hora de ingreso)
                    listaParam.Add(" ");  //Campo 33) AUTHORIZER ID (ID de quien autoriza)
                    listaParam.Add(VB6Helpers.Str(Ll_HoraIngreso));  //Campo 34) AUTHORIZER TIME (7N hora de autorización)
                    listaParam.Add(VB6Helpers.Str(Lc_OrderCustAccount.Replace("|", "") != "" ? VB6Helpers.Str(Lc_OrderCustAccount.Replace("|", "")) : "0"));  //Campo 35) ORDERING CUSTOMER ACCOUNT NUMBER (número de cuenta del cliente que envía)  -->11N Out = Cta Cte Cliente Cosmos <---
                    listaParam.Add(VB6Helpers.Str(Li_dif_dias));  //Campo 36) INPUT DATE (fecha de ingreso)  --> 5N dias_hasta_hoy <---
                    listaParam.Add(" ");  //Campo 37) ALPHA-NAME (nombre en caractéres alfanuméricos)   ES BLANCO OK
                    listaParam.Add(Lc_SwfCurrCode);  //Campo 38) SWIFT CURRENCY CODE OF EQUIVALENT (código de la moneda Swift del equivalente)  --> 4A  vmoneda <--  ??
                    listaParam.Add(VB6Helpers.Str(Li_CurrCode));  //Campo 39) CURRENCY CODE OF EQUIVALENT (código de la moneda del equivalente)  -->3N id_vmoneda_bch <--- ??
                    listaParam.Add(MODGSYB.dbmontoSyForRead(Ln_Monto));  //Campo 40) EQUIVALENT OF TRANSFER AMOUNT (equivalente del monto de la transferencia)  -->17N vmonto1 <--- ??
                    listaParam.Add("+");  //Campo 41) SIGN (signo)
                    listaParam.Add(VB6Helpers.Str(0));  //Campo 42) FCY EXCHANGE RATE (tasa de cambio de la moneda extranjera)
                    listaParam.Add(VB6Helpers.Str(Lc_ReceiverAccount.Replace("|", "")) != "" ? VB6Helpers.Str(Lc_ReceiverAccount.Replace("|", "")) : "0");  //Campo 43) RECEIVER ACCOUNT NUMBER (número de cuenta del receptor)  -->11N Inc = Cta Cte Cliente Cosmos <--
                    listaParam.Add(VB6Helpers.Str(Li_dif_dias));  //Campo 44) INPUT DATE (fecha de ingreso)  -->5N dias_hasta_hoy <---
                    listaParam.Add(" ");  //Campo 45) SHORT NAME OF BENEFICIARY BANK (32T: nombre corto del banco de la cuenta beneficiaria) OK
                    listaParam.Add(" ");  //Campo 46) ALPHA REFERENCE (referencia alfanumérica)
                    listaParam.Add("F");  //Campo 47) LTO INDICATOR (indicador LTO)
                    listaParam.Add(" ");  //Campo 48) BENEFICIARY ACCOUNT NUMBER (número de la cuenta beneficiaria)
                    listaParam.Add(VB6Helpers.Str(0));  //Campo 49) COMMISSION RATE (tasa de commission)
                    listaParam.Add(MODGSYB.dbmontoSyForRead(Ln_Comi_Monto));  //Campo 50) COMMISSION AMOUNT (monto de la comisión)
                    listaParam.Add("+");  //Campo 51) SIGN (signo)
                    listaParam.Add(VB6Helpers.Str(0));  //Campo 52) COMMISSION RATE (tasa de commission)
                    listaParam.Add(VB6Helpers.Str(0));  //Campo 53) COMMISSION RATE (tasa de commission)
                    listaParam.Add("+");  //Campo 54) SIGN (signo)
                    listaParam.Add(VB6Helpers.Str(0));  //Campo 55) COMMISSION RATE (tasa de commission)
                    listaParam.Add("+");  //Campo 56) SIGN (signo)
                    listaParam.Add("CLP");  //Campo 57) SWIFT CURRENCY CODE FOR CHARGES (código de la moneda Swifts para los cobros)  -->4A :VMONEDA<-- ??
                    listaParam.Add(VB6Helpers.Str(1));  //Campo 58) CURRENCY CODE FOR CHARGES (código de moneda para los cobros)  -->3N id_vmoneda_bch<---
                    listaParam.Add(VB6Helpers.Str(0));  //Campo 59) CHRG-BASE-NBR
                    listaParam.Add(" ");  //Campo 60) SHORT NAME FOR CHARGES ACCOUNT (nombre corto para la cuenta de cargo).  OK
                    listaParam.Add(" ");  //Campo 61) THEIR REFERENCE NUMBER (su número de referencia)  ok
                    listaParam.Add(VB6Helpers.Str(0));  //Campo 62) CENTRAL BANK CODE (código del banco central)  OK
                    listaParam.Add(VB6Helpers.Str(Li_Ordering));  //Campo 63) NUMBER OF LINES TEXT FOR ORDERING CUSTOMER (cantidad de líneas de texto)  OK
                    listaParam.Add(VB6Helpers.Str(Li_Receiver));  //Campo 64) NUMBER OF LINES TEXT FOR RECEIVER (cantidad de líneas de texto para el receptor) -->1N<--
                                                                  //---------------------------------------------
                                                                  //Realsystems-Código Nuevo-Inicio
                                                                  //Fecha Modificación 20100713
                                                                  //Responsable: Pablo Millan V.
                                                                  //Versión: 1.0
                                                                  //Descripción : Se cambia valor campo 65
                                                                  //---------------------------------------------
                    listaParam.Add(Lc_BenefBank);  //Campo 65) NUMBER OF LINES TEXT FOR BENEFICIARY BANK (cantidad de líneas de texto para banco del beneficiario)  -->1N<-- OK
                                                   //----------------------------------------
                                                   // RealSystems - Código Nuevo - Termino
                                                   //----------------------------------------
                                                   // RealSystems - Código Antiguo - Inicio
                                                   //----------------------------------------
                                                   //ls_sql = ls_sql & Str(0) + ","                      'Campo 65) NUMBER OF LINES TEXT FOR BENEFICIARY BANK (cantidad de líneas de texto para banco del beneficiario)  -->1N<-- OK
                                                   //----------------------------------------
                                                   // RealSystems - Código Antiguo - Termino
                                                   //----------------------------------------
                    listaParam.Add(VB6Helpers.Str(0));  //Campo 66) NUMBER OF LINES TEXT FOR BENEFICIARY (cantidad de líneas de texto para el beneficiario) --> 1N-contador-<--
                    listaParam.Add(VB6Helpers.Str(Li_Reason));  //Campo 67) NUMBER OF LINES TEXT FOR REASON (cantidad de líneas de texto para el motivo de la -->1N contrador_reason<--transferencia)contador_reason <--
                    listaParam.Add(VB6Helpers.Str(0));  //Campo 68) NUMBER OF LINES TEXT FOR BANK TO BANK INFO (cantidad de líneas de texto para la información banco a banco)  -->1N<-- OK
                    listaParam.Add(VB6Helpers.Str(0));  //Campo 69) NUMBER OF LINES TEXT FOR CHARGES (cantidad de líneas de texto para los cobros) -->1N<-- OK
                    listaParam.Add(VB6Helpers.Str(Li_Total));  //Campo 70) TOTAL NUMBER OF LINES OF TEXT (cantidad total de líneas de texto)  -->2N : total_lineas <--

                    if (VB6Helpers.Len(Lc_Total) <= 250)
                    {
                        //Campo 71) TEXT LINE (línea de texto) -->455A:  vtext_line <--
                        listaParam.Add(VB6Helpers.UCase(Lc_Total) + "X");
                        listaParam.Add("");
                    }
                    else
                    {
                        listaParam.Add(VB6Helpers.UCase(VB6Helpers.Mid(Lc_Total, 1, 250)));
                        listaParam.Add(VB6Helpers.UCase(VB6Helpers.Mid(Lc_Total, 251, 250)) + "X");
                    }
                    APartirDe = 1;
                    sigue = true;
                    while (sigue)
                    {
                        string lc_retorno = string.Empty;
                        string lc_mensaje = string.Empty;

                        unit.SceRepository.pro_sce_fts_i01_MS(ref lc_retorno, ref lc_mensaje, listaParam);

                        if (!lc_retorno.Equals("E00"))
                        {
                            tracer.TraceError("Ingreso de Operaciones Manuales", "Se ha producido un error al insertar los registros en la Base de datos");
                            initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Se ha producido un error al insertar los registros en la Base de datos",
                                Title = "Ingreso de Operaciones Manuales"
                            });
                            return 0;
                        }
                        else
                        {
                            sigue = false;
                            _retValue = -1;
                        }
                    }
                    return _retValue;
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta", e);
                    return _retValue;
                }
            }
        }

        public static short Put_Gcom(T_Mdl_Funciones_Varias Mdl_Funciones_Varias, T_MODGSCE MODGSCE, UI_Mdi_Principal Mdi_Principal, short Indice, dynamic GlsCom, string NemMnd, double MtoCom, short ConIVA, double TipCam, string NemCta)
        {
            short n = 0;
            //Determina el índice.-
            //---------------------------------------------------------------------------------------------------
            //Código Nuevo-Inicio
            //Fecha Modificación 05-09-2011
            //Responsable: Angel Donoso Gonzalez.
            //Empresa: Accenture
            //Versión:
            //Descripción : se agrega nueva variable para calcular comisiones en moneda nacional y se incializa.
            //---------------------------------------------------------------------------------------------------
            int Comisp = 0;
            //-----------------------------------------------------------------------------------
            // Accenture - Código Nuevo - Termino
            //-----------------------------------------------------------------------------------
            if (MODGPYF0.Componer(NemCta, VB6Helpers.Chr(0), "") == "")
            {
                Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "La operación " + GlsCom + " No tiene nemónico de la cuenta, por lo tanto deberá ingresarlo manualmente." });
                return 0;
            }

            n = Indice;
            if (Indice == 0)
            {
                // n = UBound(V_gCom) + 1
                // VB6Helpers.RedimPreserve(ref V_gCom, 0, n);
                //n = (short)(VB6Helpers.UBound(Mdl_Funciones_Varias.V_gCom) + 1);              
                //VB6Helpers.RedimPreserve(ref Mdl_Funciones_Varias.V_gCom, 0, n);
                //Mdl_Funciones_Varias.V_gCom = new T_gCom[1];
                //Mdl_Funciones_Varias.V_gCom[0] = new T_gCom();
                n = (short)(Mdl_Funciones_Varias.V_gCom.Length);
                VB6Helpers.RedimPreserve(ref Mdl_Funciones_Varias.V_gCom, 0, n);
                // n--;
            }
            Mdl_Funciones_Varias.V_gCom[n].GlsCom = VB6Helpers.CStr(GlsCom);
            Mdl_Funciones_Varias.V_gCom[n].NemMnd = NemMnd;
            Mdl_Funciones_Varias.V_gCom[n].MtoCom = MtoCom;
            Mdl_Funciones_Varias.V_gCom[n].ConIVA = ConIVA;
            Mdl_Funciones_Varias.V_gCom[n].TipCam = TipCam;
            Mdl_Funciones_Varias.V_gCom[n].NemCta = NemCta;
            Mdl_Funciones_Varias.V_gCom[n].estado = 1;
            if (Mdl_Funciones_Varias.V_gCom[n].ConIVA != 0)
            {
                if (NemMnd != "$")
                {
                    //-----------------------------------------------------------------------------------
                    //Código Nuevo-Inicio
                    //Fecha Modificación 05-09-2011
                    //Responsable: Angel Donoso Gonzalez.
                    //Empresa: Accenture
                    //Versión:
                    //Descripción : se hace la conversión de M/E a moneda nacional y se
                    //              calcula Iva.(en codigo antiguo se hacia calculo de iva de M/E
                    //              para luego calcular el iva de M/E por el tipo de cambio en M/N)
                    //-----------------------------------------------------------------------------------
                    Comisp = (int)(Mdl_Funciones_Varias.V_gCom[n].MtoCom * Mdl_Funciones_Varias.V_gCom[n].TipCam);
                    Mdl_Funciones_Varias.V_gCom[n].MtoIvap = Format.StringToDouble(Format.FormatCurrency((Comisp * (MODGSCE.VGen.MtoIva / 100)), "0"));
                    //-----------------------------------------------------------------------------------
                    // Accenture - Código Nuevo - Termino
                    //-----------------------------------------------------------------------------------
                    //-----------------------------------------------------------------------------------
                    //Antiguo código - Inicio
                    //Fecha Modificación 20101222
                    //Responsable: Angel Donoso Gonzalez.
                    //Empresa: Accenture
                    //Descripcion: se deja como comentario ya que se agrega nuevo calculo para iva en M/N
                    //-----------------------------------------------------------------------------------
                    //V_gCom(n%).MtoIva = Format(V_gCom(n%).MtoCom * (VGen.MtoIva / 100), "0.00")
                    //V_gCom(n%).MtoIvap = Format(V_gCom(n%).MtoIva * V_gCom(n%).TipCam, "0")
                    //-----------------------------------------------------------------------------------
                    // Accenture - Código antiguo - Termino
                    //-----------------------------------------------------------------------------------
                }
                else
                {
                    Mdl_Funciones_Varias.V_gCom[n].MtoIva = Format.StringToDouble(Format.FormatCurrency((Mdl_Funciones_Varias.V_gCom[n].MtoCom * (MODGSCE.VGen.MtoIva / 100)), "0"));
                    Mdl_Funciones_Varias.V_gCom[n].MtoIvap = Format.StringToDouble(Format.FormatCurrency((Mdl_Funciones_Varias.V_gCom[n].MtoIva), "0"));
                }

            }
            else
            {
                Mdl_Funciones_Varias.V_gCom[n].MtoIva = 0;
                Mdl_Funciones_Varias.V_gCom[n].MtoIvap = 0;
            }

            if (NemMnd != "$")
            {
                Mdl_Funciones_Varias.V_gCom[n].MtoTot = Mdl_Funciones_Varias.V_gCom[n].MtoCom + Mdl_Funciones_Varias.V_gCom[n].MtoIva;
                Mdl_Funciones_Varias.V_gCom[n].MtoComp = Format.StringToDouble(Format.FormatCurrency((Mdl_Funciones_Varias.V_gCom[n].MtoCom * Mdl_Funciones_Varias.V_gCom[n].TipCam), "0"));
                //Mdl_Funciones_Varias.V_gCom[n].MtoTotp = Format.StringToDouble(VB6Helpers.Format(VB6Helpers.CStr(Mdl_Funciones_Varias.V_gCom[n].MtoComp) + VB6Helpers.CStr(Mdl_Funciones_Varias.V_gCom[n].MtoIvap), "0"));
                Mdl_Funciones_Varias.V_gCom[n].MtoTotp = Format.StringToDouble(Format.FormatCurrency(Mdl_Funciones_Varias.V_gCom[n].MtoComp + Mdl_Funciones_Varias.V_gCom[n].MtoIvap, "0"));
            }
            else
            {
                Mdl_Funciones_Varias.V_gCom[n].MtoTot = Mdl_Funciones_Varias.V_gCom[n].MtoCom + Mdl_Funciones_Varias.V_gCom[n].MtoIva;
                Mdl_Funciones_Varias.V_gCom[n].MtoComp = Format.StringToDouble(Format.FormatCurrency((Mdl_Funciones_Varias.V_gCom[n].MtoCom), "0"));
                //Mdl_Funciones_Varias.V_gCom[n].MtoTotp = Format.StringToDouble(VB6Helpers.Format(VB6Helpers.CStr(Mdl_Funciones_Varias.V_gCom[n].MtoComp) + VB6Helpers.CStr(Mdl_Funciones_Varias.V_gCom[n].MtoIvap), "0"));
                Mdl_Funciones_Varias.V_gCom[n].MtoTotp = Format.StringToDouble(Format.FormatCurrency(Mdl_Funciones_Varias.V_gCom[n].MtoComp + Mdl_Funciones_Varias.V_gCom[n].MtoIvap, "0"));


            }

            return n;
        }

        public static short Put_Gcom_2(T_Mdl_Funciones_Varias Mdl_Funciones_Varias, T_MODGSCE MODGSCE, UI_Mdi_Principal Mdi_Principal, short Indice, dynamic GlsCom, string NemMnd, double MtoCom, short ConIVA, double TipCam, string NemCta)
        {
            short n = 0;
            //Determina el índice.-
            //---------------------------------------------------------------------------------------------------
            //Código Nuevo-Inicio
            //Fecha Modificación 05-09-2011
            //Responsable: Angel Donoso Gonzalez.
            //Empresa: Accenture
            //Versión:
            //Descripción : se agrega nueva variable para calcular comisiones en moneda nacional y se incializa.
            //---------------------------------------------------------------------------------------------------
            long Comisp = 0;
            //-----------------------------------------------------------------------------------
            // Accenture - Código Nuevo - Termino
            //-----------------------------------------------------------------------------------
            if (MODGPYF0.Componer(NemCta, VB6Helpers.Chr(0), "") == "")
            {
                Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "La operación " + GlsCom + " No tiene nemónico de la cuenta, por lo tanto deberá ingresarlo manualmente." });
                return 0;
            }

            n = Indice;
            if (Indice == -1)
            {
                n = (short)(Mdl_Funciones_Varias.V_gCom.Length);
                VB6Helpers.RedimPreserve(ref Mdl_Funciones_Varias.V_gCom, 0, n);
                // n--;
            }
            Mdl_Funciones_Varias.V_gCom[n].GlsCom = VB6Helpers.CStr(GlsCom);
            Mdl_Funciones_Varias.V_gCom[n].NemMnd = NemMnd;
            Mdl_Funciones_Varias.V_gCom[n].MtoCom = MtoCom;
            Mdl_Funciones_Varias.V_gCom[n].ConIVA = ConIVA;
            Mdl_Funciones_Varias.V_gCom[n].TipCam = TipCam;
            Mdl_Funciones_Varias.V_gCom[n].NemCta = NemCta;
            Mdl_Funciones_Varias.V_gCom[n].estado = 1;
            if (Mdl_Funciones_Varias.V_gCom[n].ConIVA != 0)
            {
                if (NemMnd != "$")
                {
                    //-----------------------------------------------------------------------------------
                    //Código Nuevo-Inicio
                    //Fecha Modificación 05-09-2011
                    //Responsable: Angel Donoso Gonzalez.
                    //Empresa: Accenture
                    //Versión:
                    //Descripción : se hace la conversión de M/E a moneda nacional y se
                    //              calcula Iva.(en codigo antiguo se hacia calculo de iva de M/E
                    //              para luego calcular el iva de M/E por el tipo de cambio en M/N)
                    //-----------------------------------------------------------------------------------
                    Comisp = long.Parse((Mdl_Funciones_Varias.V_gCom[n].MtoCom * Mdl_Funciones_Varias.V_gCom[n].TipCam).ToString("0"));
                    Mdl_Funciones_Varias.V_gCom[n].MtoIvap = Format.StringToDouble(Format.FormatCurrency((Comisp * (MODGSCE.VGen.MtoIva / 100)), "0"));
                    //-----------------------------------------------------------------------------------
                    // Accenture - Código Nuevo - Termino
                    //-----------------------------------------------------------------------------------
                    //-----------------------------------------------------------------------------------
                    //Antiguo código - Inicio
                    //Fecha Modificación 20101222
                    //Responsable: Angel Donoso Gonzalez.
                    //Empresa: Accenture
                    //Descripcion: se deja como comentario ya que se agrega nuevo calculo para iva en M/N
                    //-----------------------------------------------------------------------------------
                    //V_gCom(n%).MtoIva = Format(V_gCom(n%).MtoCom * (VGen.MtoIva / 100), "0.00")
                    //V_gCom(n%).MtoIvap = Format(V_gCom(n%).MtoIva * V_gCom(n%).TipCam, "0")
                    //-----------------------------------------------------------------------------------
                    // Accenture - Código antiguo - Termino
                    //-----------------------------------------------------------------------------------
                }
                else
                {
                    Mdl_Funciones_Varias.V_gCom[n].MtoIva = Format.StringToDouble(Format.FormatCurrency((Mdl_Funciones_Varias.V_gCom[n].MtoCom * (MODGSCE.VGen.MtoIva / 100)), "0"));
                    Mdl_Funciones_Varias.V_gCom[n].MtoIvap = Format.StringToDouble(Format.FormatCurrency((Mdl_Funciones_Varias.V_gCom[n].MtoIva), "0"));
                }

            }
            else
            {
                Mdl_Funciones_Varias.V_gCom[n].MtoIva = 0;
                Mdl_Funciones_Varias.V_gCom[n].MtoIvap = 0;
            }

            if (NemMnd != "$")
            {
                Mdl_Funciones_Varias.V_gCom[n].MtoTot = Mdl_Funciones_Varias.V_gCom[n].MtoCom + Mdl_Funciones_Varias.V_gCom[n].MtoIva;
                Mdl_Funciones_Varias.V_gCom[n].MtoComp = Format.StringToDouble(Format.FormatCurrency((Mdl_Funciones_Varias.V_gCom[n].MtoCom * Mdl_Funciones_Varias.V_gCom[n].TipCam), "0"));
                //Mdl_Funciones_Varias.V_gCom[n].MtoTotp = Format.StringToDouble(Format.FormatCurrency((Mdl_Funciones_Varias.V_gCom[n].MtoComp) + (Mdl_Funciones_Varias.V_gCom[n].MtoIvap), "0"));
                Mdl_Funciones_Varias.V_gCom[n].MtoTotp = Format.StringToDouble(Format.FormatCurrency(Mdl_Funciones_Varias.V_gCom[n].MtoComp + Mdl_Funciones_Varias.V_gCom[n].MtoIvap, "0"));
            }
            else
            {
                Mdl_Funciones_Varias.V_gCom[n].MtoTot = Mdl_Funciones_Varias.V_gCom[n].MtoCom + Mdl_Funciones_Varias.V_gCom[n].MtoIva;
                Mdl_Funciones_Varias.V_gCom[n].MtoComp = Format.StringToDouble(Format.FormatCurrency((Mdl_Funciones_Varias.V_gCom[n].MtoCom), "0"));
                //Mdl_Funciones_Varias.V_gCom[n].MtoTotp = Format.StringToDouble(Format.FormatCurrency((Mdl_Funciones_Varias.V_gCom[n].MtoComp) + (Mdl_Funciones_Varias.V_gCom[n].MtoIvap), "0"));
                Mdl_Funciones_Varias.V_gCom[n].MtoTotp = Format.StringToDouble(Format.FormatCurrency(Mdl_Funciones_Varias.V_gCom[n].MtoComp + Mdl_Funciones_Varias.V_gCom[n].MtoIvap, "0"));


            }

            return n;
        }

        //Entrega el total de las Comisiones a cobrar.-
        public static double TotalComis(T_Mdl_Funciones_Varias Mdl_Funciones_Varias)
        {
            short i = 0;
            double Valor = 0;
            for (i = 0; i <= (short)VB6Helpers.UBound(Mdl_Funciones_Varias.V_gCom); i++)
            {
                if (Mdl_Funciones_Varias.V_gCom[i].estado != 3)
                {
                    Valor += Mdl_Funciones_Varias.V_gCom[i].MtoTotp;
                }

            }

            return Valor;
        }

        public static string llena_blancos_der(string Text, short Num)
        {
            short faltan = 0;

            if ((VB6Helpers.Len(Text) < Num))
            {
                faltan = (short)(Num - VB6Helpers.Len(Text));
                return Text + VB6Helpers.Space(faltan);
            }
            else if ((VB6Helpers.Len(Text) > Num))
            {
                return VB6Helpers.Left(Text, Num);
            }
            else
            {
                return Text;
            }

        }

        //Obtenemos los tipos de autorizacion
        public static short SyGet_TipAut(T_Mdl_Funciones_Varias Mdl_Funciones_Varias, UnitOfWorkCext01 unit)
        {
            short _retValue = 0;
            string Que = "";
            string R = "";
            short n = 0;
            short i = 0;
            const string cacheKey = "V_TipAutCache";
            try
            {
                var cache = MemoryCache.Default;
                if (!cache.Contains(cacheKey))
                //if (!T_Mdl_Funciones_Varias.V_TipAutCache.ContainsKey(DateTime.Today))
                {
                    var result = unit.SceRepository.EjecutarSP<sce_aut_s01_MS_Result>("sce_aut_s01_MS").Select(x =>
                    new T_VTipAut()
                    {
                        CodAut = x.codaut,
                        DesAut = x.desaut
                    }).ToArray();
                    cache.Set(cacheKey, result, DateTimeOffset.Now.AddDays(1));
                }
                Mdl_Funciones_Varias.V_TipAut = cache[cacheKey] as T_VTipAut[];
                _retValue = (short)(true ? -1 : 0);
            }
            catch (Exception _ex)
            {
                _retValue = 0;
            }
            return _retValue;
        }

        //Obtiene los instrumentos utilizados.-
        public static short SyGet_Instru(T_Mdl_Funciones_Varias Mdl_Funciones_Varias, UnitOfWorkCext01 unit)
        {
            short _retValue = 0;
            const string cacheKey = "V_InstruCache";
            try
            {
                var cache = MemoryCache.Default;
                if (!cache.Contains(cacheKey))
                {
                    var result = unit.SceRepository.EjecutarSP<sce_intr_s01_MS_Result>("sce_intr_s01_MS").Select(x =>
                    new T_VInstru()
                    {
                        CodIntr = x.codintr.ToString(),
                        DesIntr = x.desintr
                    }).ToArray();
                    cache.Set(cacheKey, result, DateTimeOffset.Now.AddDays(1));
                }
                Mdl_Funciones_Varias.V_Instru = cache[cacheKey] as T_VInstru[];
                _retValue = (short)(true ? -1 : 0);
            }
            catch (Exception ex)
            {
                _retValue = 0;
            }
            return _retValue;
        }

        //Obtiene las areas contables.-
        public static short SyGet_AreCon(T_Mdl_Funciones_Varias Mdl_Funciones_Varias, UnitOfWorkCext01 unit)
        {
            short _retValue = 0;
            const string cacheKey = "V_AreConCache";
            try
            {
                var cache = MemoryCache.Default;
                if (!cache.Contains(cacheKey))
                {
                    var result = unit.SceRepository.EjecutarSP<sce_acon_s01_MS_Result>("sce_acon_s01_MS")
                    .Select(x => new T_AreCon()
                    {
                        CodACon = x.codacon.ToString(),
                        DesACon = x.desacon
                    }).ToArray();
                    cache.Set(cacheKey, result, DateTimeOffset.Now.AddDays(1));
                }
                Mdl_Funciones_Varias.V_AreCon = cache[cacheKey] as T_AreCon[];
                _retValue = (short)(true ? -1 : 0);
            }
            catch (Exception _ex)
            {
                _retValue = 0;
            }
            return _retValue;
        }
        public static void Limpia_Grilla(UI_Grid uI_Grid)
        {
            uI_Grid.Items.Clear();
        }

        //Retorno    :  = "", si el número está bueno.-
        //             <> "", si el número está malo.-
        public static short Fn_ValidaAladi(UI_Mdi_Principal Mdi_Principal, string NumAla)
        {
            string s = "";
            short i = 0;
            short x = 0;
            short mul = 0;
            short Sum = 0;
            string dv = "";
            //Largo.-
            //If Len(NumAla) <> 15 Then
            if (VB6Helpers.Len(NumAla) != 18)
            {
                Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "El Número Aladi debe tener 18 dígitos."
                });
                return 0;
            }

            //Dígito año.-
            //Agno% = Val(Right$(Format$(Now, "yyyy"), 1))
            //If Val(Mid$(NumAla, 6, 1)) <> Agno% Then
            //    MsgBox "El Año del Número Aladi NO corresponde.", vbInformation, "Validación Número Aladi"
            //    Exit Function
            //End If

            //Correlativo.-
            //If Val(Mid$(NumAla, 7, 6)) <= 0 Then
            if (VB6Helpers.Val(VB6Helpers.Mid(NumAla, 10, 6)) <= 0)
            {
                Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "El Correlativo del Número Aladi debe ser mayor que cero."
                });
                return 0;
            }

            //Calcula Dígito Verificador.-
            //s$ = Left$(NumAla, 12)
            s = VB6Helpers.Left(NumAla, 15);
            for (i = 1; i <= (short)VB6Helpers.Len(s); i++)
            {
                if (i % 2 == 0)
                {
                    mul = (short)(VB6Helpers.Val(VB6Helpers.Mid(s, i, 1)) * 2);
                    Sum = (short)(Sum + VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.Format(VB6Helpers.CStr(mul), "00"), 1, 1)) + VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.Format(VB6Helpers.CStr(mul), "00"), 2, 1)));
                }
                else
                {
                    mul = (short)(VB6Helpers.Val(VB6Helpers.Mid(s, i, 1)) * 1);
                    Sum = (short)(Sum + VB6Helpers.Val(VB6Helpers.Mid(VB6Helpers.Format(VB6Helpers.CStr(mul), "00"), 2, 1)));
                }

            }

            if (Sum == 0)
            {
                x = 0;
            }
            else
            {
                x = (short)((VB6Helpers.Int((Sum - 1) / 10) * 10) + 10);
            }

            x = (short)(x - Sum);
            dv = VB6Helpers.Right(VB6Helpers.Format(VB6Helpers.CStr(x), "00"), 1) + "00";

            //Dígito Verificador.-
            if (dv != VB6Helpers.Right(NumAla, 3))
            {
                Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "El Dígito Verificador del Número Aladi no está correcto."
                });
                return 0;
            }

            return (short)(true ? -1 : 0);
        }

        public static string Rescata_Referencia(short Moneda, UnitOfWorkCext01 unit)
        {
            return unit.SceRepository.pro_sce_parametros_s01_MS(Moneda);
            //dynamic _retValue = null;
            //short Li_Largo1 = 0;
            //short Li_Largo2 = 0;
            //short APartirDe = 1;
            //bool sigue = true;
            //short num_registros_msg = 0;
            //string ls_ret = "";
            //string ls_sql = "";
            //string ls_retorno = "";
            //string ls_mensaje = "";

            //_retValue = false;

            //ls_sql += " exec pro_sce_parametros_s01_MS ";
            //ls_sql = ls_sql + "'APLI_DES/ORI' ," + VB6Helpers.CStr(Moneda);

            //while (sigue)
            //{

            //    VB6Helpers.DoEvents();
            //    Mdl_SRM.ParamSrm8k.APartirDe = APartirDe;
            //    ls_ret = Mdl_SRM.SendQuery(Mdl_SRM.ParamSrm8k.Base, Mdl_SRM.ParamSrm8k.Servidor.Value, Mdl_SRM.ParamSrm8k.Nodo.Value, ref ls_sql);
            //    APartirDe = Mdl_SRM.ParamSrm8k.APartirDe;

            //    if (!(VB6Helpers.Mid(Mdl_SRM.ParamSrm8k.mensaje.Value, 1, 2) == "00"))
            //    {
            //        VB6Helpers.Beep();
            //        VB6Helpers.MsgBox("Se ha producido un error en la consulta a la Base de datos", MsgBoxStyle.Information, "Ingreso de Operaciones Manuales");
            //        return false;
            //    }
            //    else
            //    {
            //        if (VB6Helpers.Mid(Mdl_SRM.ParamSrm8k.mensaje.Value, 3, 1) == "N")
            //        {
            //            sigue = false;
            //        }

            //        if (ls_ret == "")
            //        {
            //            _retValue = false;
            //            sigue = false;
            //            return _retValue;
            //        }

            //        num_registros_msg = Mdl_SRM.getocurrs();

            //        if (num_registros_msg == 0)
            //        {
            //            _retValue = false;
            //            sigue = false;
            //        }
            //        else
            //        {
            //            //Captura de Mensaje de retorno
            //            Li_Largo1 = (short)VB6Helpers.Len(ls_ret);
            //            Li_Largo2 = 1;

            //            //llena datos
            //            ls_retorno = Mdl_SRM.Trae_Campo(ls_ret, Li_Largo2);
            //            Li_Largo2 = (short)(Li_Largo2 + VB6Helpers.Len(ls_retorno) + 1);

            //            ls_mensaje = Mdl_SRM.Trae_Campo(ls_ret, Li_Largo2);
            //            Li_Largo2 = (short)(Li_Largo2 + VB6Helpers.Len(ls_mensaje) + 1);
            //        }

            //        _retValue = ls_retorno;
            //    }

            //}

            //return _retValue;
        }

        //Graba un Documento de Cobranza Exportación.
        //Retorno    <> 0 : Correlativo de la Carta.
        //           =  0 : Error o Grabación no Exitosa.
        public static short SyPut_xDoc(InitializationObject initObject, UnitOfWorkCext01 unit, string NumOpe, int CodDoc, string Memo, string Usuario)
        {
            UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;
            short _retValue = 0;
            decimal? c = 0;
            int m = 0;
            try
            {
                // IGNORED: On Error GoTo SyPut_xDocErr
                c = unit.SceRepository.EjecutarSP<decimal?>("sce_xdoc_s02_MS", VB6Helpers.Mid(NumOpe, 1, 3), VB6Helpers.Mid(NumOpe, 4, 2), VB6Helpers.Mid(NumOpe, 6, 2), VB6Helpers.Mid(NumOpe, 8, 3), VB6Helpers.Mid(NumOpe, 11, 5)).First();
                //Obtiene el Correlativo para el documento.
                if (c == null)
                {
                    c = 1;
                }
                else
                {
                    c = (c.Value + 1);
                }

                //Se ejecuta el Procedimiento Almacenado.

                //VB6Helpers.MsgBox("Se ha producido un error al tratar de leer el correlativo del Documento (Sce_xDoc). El Servidor reporta : [" + VB6Helpers.Left(VB6Helpers.Trim(Mdl_SRM.ParamSrm8k.mensaje.Value), 100) + "]. Reporte este problema.", MsgBoxStyle.Information, "Validación de Datos");


                //Resultado nulo de la Consulta.

                //Para un memo.
                m = MODGMEM.SyPutn_Mem(initObject, unit, "x", 0, Memo);
                if (m == 0)
                {
                    return 0;
                }
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
                }, "sce_xdoc_i01_MS", VB6Helpers.Mid(NumOpe, 1, 3), VB6Helpers.Mid(NumOpe, 4, 2), VB6Helpers.Mid(NumOpe, 6, 2), VB6Helpers.Mid(NumOpe, 8, 3), VB6Helpers.Mid(NumOpe, 11, 5), c.ToString(), VB6Helpers.Left(Usuario, 3), VB6Helpers.Right(Usuario, 2), CodDoc.ToString(), DateTime.Now.ToString("yyyy-MM-dd"), m.ToString());
                //int rowsAffected = unit.SceRepository.ExecuteNonQuerySP("sce_xdoc_i01_MS", VB6Helpers.Mid(NumOpe, 1, 3), VB6Helpers.Mid(NumOpe, 4, 2), VB6Helpers.Mid(NumOpe, 6, 2), VB6Helpers.Mid(NumOpe, 8, 3), VB6Helpers.Mid(NumOpe, 11, 5), c.ToString(), VB6Helpers.Left(Usuario, 3), VB6Helpers.Right(Usuario, 2), CodDoc.ToString(), DateTime.Now.ToString("yyyy-MM-dd"), m.ToString());
                //Hace un Put en Sce_xDoc.
                if (resOp != 0)
                {
                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error al tratar de grabar un Conocimiento de Embarque (Sce_xCem)."
                    });
                    return 0;
                }
                //Se ejecuta el Procedimiento Almacenado.
                _retValue = (short)(c.Value);
            }
            catch (Exception _ex)
            {
                Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Se ha producido un error al tratar de grabar un Conocimiento de Embarque (Sce_xCem)."
                });
                _retValue = 0;
            }
            return _retValue;
        }

        //****************************************************************************
        //   1.  Graba la Carta de Compra - Venta.
        //   2.  Se confecciona el string de la carta.
        //       DATO1 + CHR$(9) + ...+ DATON
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short Doc_ComVta(InitializationObject initObject, UnitOfWorkCext01 unit, string NumOpe)
        {
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_Module1 Module1 = initObject.Module1;
            T_MODGASO MODGASO = initObject.MODGASO;
            T_MODGCHQ MODGCHQ = initObject.MODGCHQ;
            T_MODGSWF MODGSWF = initObject.MODGSWF;
            T_MODGSCE MODGSCE = initObject.MODGSCE;
            T_Mdl_Funciones_Varias Mdl_FuncionesVarias = initObject.Mdl_Funciones_Varias;
            T_MODGMTA MODGMTA = initObject.MODGMTA;
            T_MODGUSR MODGUSR = initObject.MODGUSR;
            T_MODGFRA MODGFRA = initObject.MODGFRA;

            short n = 0;
            short m = 0;
            short z = 0;
            string s = "";
            short ContadorCompra = 0;
            short ContadorVenta = 0;
            short i = 0;
            short Contador1 = 0;
            string NomBen = "";
            short Contador2 = 0;
            short Contador3 = 0;
            short hayiva = 0;
            short NumDeb = 0;
            double suma = 0;
            double SumaIVA = 0;
            string Usr = "";


            n = (short)VB6Helpers.UBound(MODGCVD.VgPli);  //Compra - Venta.
            m = (short)VB6Helpers.UBound(MODXVIA.VxVia);  //Vías.
            z = (short)VB6Helpers.UBound(MODXORI.VxOri);  //Orígenes.
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            //Partys Exportador Principal.
            s = "";
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].NombreUsado.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].DireccionUsado.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].CiudadUsado.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].PaisUsado.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].EstadoUsado.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].PostalUsado.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].Fax.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].CasBanco.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].CasPostal.Trim() + VB6Helpers.Chr(9);
            s = s + VB6Helpers.Str(0) + VB6Helpers.Chr(9);  //Indicador de que datos Imprimir.

            //Operación Relacionada.-
            s = s + MODGASO.VgAso.OpeCon + VB6Helpers.Chr(9);

            //Referencia Operación.-
            s = s + MODGCVD.VgCvd.OpeCon + VB6Helpers.Chr(9);

            //Referencia Cliente.-
            s = s + MODGCVD.VgCvd.RefCli + VB6Helpers.Chr(9);

            //Contar cuantas Compras y Ventas existen no eliminadas.
            ContadorCompra = 0; ContadorVenta = 0;
            for (i = 0; i <= (short)n; i++)
            {
                if (MODGCVD.VgPli[i].Status != T_MODGCVD.EstadoEli)
                {
                    string _switchVar1 = VB6Helpers.Trim(MODGCVD.VgPli[i].TipCVD);
                    if (_switchVar1 == "C")
                    {
                        ContadorCompra = (short)(ContadorCompra + 1);
                    }
                    else if (_switchVar1 == "V" || _switchVar1 == "W")
                    {
                        ContadorVenta = (short)(ContadorVenta + 1);
                    }

                }

            }

            //envío de Nro. de Compras.
            s = s + VB6Helpers.Trim(VB6Helpers.Str(ContadorCompra)) + VB6Helpers.Chr(9);
            //envío de Nro. de Ventas.
            s = s + VB6Helpers.Trim(VB6Helpers.Str(ContadorVenta)) + VB6Helpers.Chr(9);

            //Ventas de Divisas.
            for (i = 0; i <= (short)n; i++)
            {
                if ((VB6Helpers.Trim(MODGCVD.VgPli[i].TipCVD) == "V" || VB6Helpers.Trim(MODGCVD.VgPli[i].TipCVD) == "W"))
                {
                    if (MODGCVD.VgPli[i].Status != T_MODGCVD.EstadoEli)
                    {
                        // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 's' variable as a StringBuilder6 object.
                        s = s + VB6Helpers.Trim(MODGCVD.VgPli[i].NemMnd) + VB6Helpers.Chr(9);
                        s = s + VB6Helpers.Trim(VB6Helpers.Str(MODGCVD.VgPli[i].MtoCVD)) + VB6Helpers.Chr(9);
                        s = s + VB6Helpers.Trim(VB6Helpers.Str(MODGCVD.VgPli[i].TipCam)) + VB6Helpers.Chr(9);
                        s = s + VB6Helpers.Trim(VB6Helpers.Str(MODGCVD.VgPli[i].MtoPes)) + VB6Helpers.Chr(9);
                    }

                }

            }

            //Compras de Divisas.
            for (i = 0; i <= (short)n; i++)
            {
                if (VB6Helpers.Trim(MODGCVD.VgPli[i].TipCVD) == "C")
                {
                    if (MODGCVD.VgPli[i].Status != T_MODGCVD.EstadoEli)
                    {
                        s = s + VB6Helpers.Trim(MODGCVD.VgPli[i].NemMnd) + VB6Helpers.Chr(9);
                        s = s + VB6Helpers.Trim(VB6Helpers.Str(MODGCVD.VgPli[i].MtoCVD)) + VB6Helpers.Chr(9);
                        s = s + VB6Helpers.Trim(VB6Helpers.Str(MODGCVD.VgPli[i].TipCam)) + VB6Helpers.Chr(9);
                        s = s + VB6Helpers.Trim(VB6Helpers.Str(MODGCVD.VgPli[i].MtoPes)) + VB6Helpers.Chr(9);
                    }

                }

            }

            //Contar cuantas Vías existen que no esten eliminadas en Moneda Extrajera.
            Contador1 = 0;
            for (i = 0; i <= (short)m; i++)
            {
                if ((MODXVIA.VxVia[i].Status != T_MODGCVD.EstadoEli) && (MODXVIA.VxVia[i].CodMon != MODGCVD.CodMonedaNac))
                {
                    Contador1 = (short)(Contador1 + 1);
                }

            }

            s = s + VB6Helpers.Trim(VB6Helpers.Str(Contador1)) + VB6Helpers.Chr(9);

            //Vía de la Remesa.
            //Datos correspondientes a las Vías en Moneda Extranjera.
            for (i = 0; i <= (short)m; i++)
            {
                if ((MODXVIA.VxVia[i].Status != T_MODGCVD.EstadoEli) && (MODXVIA.VxVia[i].CodMon != MODGCVD.CodMonedaNac))
                {
                    NomBen = Module1.PartysOpe[MODXVIA.VxVia[i].PosPrty].NombreUsado;
                    //Incluir Beneficiarios Cheques, Vales Vistas, Swift's.
                    short _switchVar2 = MODXVIA.VxVia[i].NumCta;
                    if (_switchVar2 == T_MODGCON0.IdCta_CHMEBCH || _switchVar2 == T_MODGCON0.IdCta_VVBCH)
                    {
                        n = MODXVIA.VxVia[i].IndChq;
                        NomBen = MODGCHQ.V_Chq_VVi[n].NomBen;
                    }
                    else if (_switchVar2 == T_MODGCON0.IdCta_OPC || _switchVar2 == T_MODGCON0.IdCta_OPOP)
                    {
                        n = MODXVIA.VxVia[i].IndSwf;
                        NomBen = MODGSWF.VSwf[n - 1].BenSwf.NomBen;
                    }
                    else if (_switchVar2 == T_MODGCON0.IdCta_VAM || _switchVar2 == T_MODGCON0.IdCta_VAX || _switchVar2 == T_MODGCON0.IdCta_VAMC)
                    {
                        NomBen = GetDatPrt(initObject, unit, MODXVIA.VxVia[i].IdPrty, 0, 0, "N");
                    }

                    s = s + VB6Helpers.Trim(MODGPYF1.Minuscula(NomBen)) + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Trim(MODXVIA.VxVia[i].NomVia) + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Trim(MODXVIA.VxVia[i].NemMon) + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Trim(VB6Helpers.Str(MODXVIA.VxVia[i].MtoTot)) + VB6Helpers.Chr(9);
                }

            }

            //Cargos y Abonos.
            //Contar cuantas Vías existen que no esten eliminadas en Moneda Extrajera.
            Contador2 = 0;
            for (i = 0; i <= (short)m; i++)
            {
                if ((MODXVIA.VxVia[i].Status != T_MODGCVD.EstadoEli) && (MODXVIA.VxVia[i].CodMon == MODGCVD.CodMonedaNac))
                {
                    Contador2 = (short)(Contador2 + 1);
                }

            }

            s = s + VB6Helpers.Trim(VB6Helpers.Str(Contador2)) + VB6Helpers.Chr(9);

            //Datos correspondientes a las Vías en Moneda Nacional.
            for (i = 0; i <= (short)m; i++)
            {
                if ((MODXVIA.VxVia[i].Status != T_MODGCVD.EstadoEli) && (MODXVIA.VxVia[i].CodMon == MODGCVD.CodMonedaNac))
                {
                    s = s + "Abono" + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Trim(MODXVIA.VxVia[i].NomVia) + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Trim(MODXVIA.VxVia[i].NemMon) + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Trim(VB6Helpers.Str(MODXVIA.VxVia[i].MtoTot)) + VB6Helpers.Chr(9);
                }

            }

            //Contar cuantos Orígenes existen que no esten eliminados en Moneda Extrajera.
            Contador3 = 0;
            for (i = 0; i <= (short)z; i++)
            {
                if (MODXORI.VxOri[i].Status != T_MODGCVD.EstadoEli)
                {
                    Contador3 = (short)(Contador3 + 1);
                }

            }

            s = s + VB6Helpers.Trim(VB6Helpers.Str(Contador3)) + VB6Helpers.Chr(9);

            //Datos correspondientes a las Vías en Moneda Nacional.
            for (i = 0; i <= (short)z; i++)
            {
                if (MODXORI.VxOri[i].Status != T_MODGCVD.EstadoEli)
                {
                    s = s + "Débito" + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Trim(MODXORI.VxOri[i].NomOri) + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Trim(MODXORI.VxOri[i].NemMon) + VB6Helpers.Chr(9);
                    if (((MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteMN ? -1 : 0) & MODXORI.VgxOri.ImpDeb) != 0)
                    {
                        s = s + VB6Helpers.Str((MODXORI.VxOri[i].MtoTot) + (MODGSCE.VGen.MtoDeb)) + VB6Helpers.Chr(9);
                    }
                    else
                    {
                        s = s + VB6Helpers.Trim(VB6Helpers.Str(MODXORI.VxOri[i].MtoTot)) + VB6Helpers.Chr(9);
                    }

                }

            }

            if (Mdl_FuncionesVarias.CARGA_AUTOMATICA == 0)
            {
                //------------------------------------------------
                //Contar cuantas comisiones existen
                //------------------------------------------------
                Contador3 = 0;
                for (i = 0; i <= (short)VB6Helpers.UBound(Mdl_FuncionesVarias.V_gCom); i++)
                {
                    if (Mdl_FuncionesVarias.V_gCom[i].estado != T_MODGCVD.EstadoEli && Mdl_FuncionesVarias.V_gCom[i].MtoComp > 0)
                    {
                        Contador3 = (short)(Contador3 + 1);
                        if (Mdl_FuncionesVarias.V_gCom[i].MtoIvap > 0)
                        {
                            hayiva = (short)(true ? -1 : 0);
                        }
                    }

                }

                for (i = 0; i <= (short)VB6Helpers.UBound(MODXORI.VxOri); i++)
                {
                    if (((MODXORI.VxOri[i].Status != T_MODGCVD.EstadoEli && MODXORI.VxOri[i].CodMon == MODGSCE.VGen.MndNac && MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteMN ? -1 : 0) & MODXORI.VgxOri.ImpDeb) != 0)
                    {
                        NumDeb = (short)(NumDeb + 1);
                    }

                }

                if (hayiva != 0)
                {
                    Contador3 = (short)(Contador3 + 1);
                }
                if (NumDeb > 0)
                {
                    Contador3 = (short)(Contador3 + 1);
                }
                s = s + VB6Helpers.Trim(VB6Helpers.Str(Contador3)) + VB6Helpers.Chr(9);
                //------------------------------------------------
                //Datos correspondientes a las comisiones cobradas
                //------------------------------------------------
                if (Contador3 > 0)
                {
                    if (NumDeb > 0 && T_MODGMTA.impflag == 1)
                    {
                        //'Impuesto al Débito.-
                        s = s + "Impuesto Fijo por débito en Cuenta Corriente" + VB6Helpers.Chr(9);
                        s = s + "$" + VB6Helpers.Chr(9);
                        s = s + VB6Helpers.Str((NumDeb * MODGSCE.VGen.MtoDeb)) + VB6Helpers.Chr(9);
                        suma = suma + Format.StringToDouble(Format.FormatCurrency((NumDeb * MODGSCE.VGen.MtoDeb), "0"));
                    }

                    for (i = 0; i <= (short)VB6Helpers.UBound(Mdl_FuncionesVarias.V_gCom); i++)
                    {
                        if (Mdl_FuncionesVarias.V_gCom[i].estado != T_MODGCVD.EstadoEli && Mdl_FuncionesVarias.V_gCom[i].MtoComp > 0)
                        {
                            s = s + Mdl_FuncionesVarias.V_gCom[i].GlsCom + VB6Helpers.Chr(9);
                            s = s + "$" + VB6Helpers.Chr(9);
                            s = s + VB6Helpers.Trim(VB6Helpers.Str(Mdl_FuncionesVarias.V_gCom[i].MtoComp)) + VB6Helpers.Chr(9);
                            SumaIVA += Mdl_FuncionesVarias.V_gCom[i].MtoIvap;
                            suma += Mdl_FuncionesVarias.V_gCom[i].MtoTotp;
                        }

                    }

                    if (hayiva != 0)
                    {
                        s = s + "IVA" + VB6Helpers.Chr(9);
                        s = s + "$" + VB6Helpers.Chr(9);
                        s = s + VB6Helpers.Str(SumaIVA) + VB6Helpers.Chr(9);
                    }

                    s = s + "$" + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Str(suma) + VB6Helpers.Chr(9);
                }

            }

            //Instrucciones Especiales.-
            s = s + BCH.Comex.Core.BL.XCFT.Modulos.MODGFRA.Get_InsEsp(MODGFRA, MODGCVD.VgCvd.InsExp) + VB6Helpers.Chr(9);

            //Atributos del Usuario
            s = s + MODGUSR.UsrEsp.CentroCosto + VB6Helpers.Chr(9);  //Centro de Costo.
            s = s + MODGUSR.UsrEsp.Especialista + VB6Helpers.Chr(9);  //Especialista.
            Usr = MODGUSR.UsrEsp.CentroCosto + MODGUSR.UsrEsp.Especialista;

            //Retona el correlativo de la carta.
            return SyPut_xDoc(initObject, unit, NumOpe, T_Mdl_Funciones_Varias.DocCVD, s, Usr);
        }

        //****************************************************************************
        //   1.  Graba la Carta de Arbitraje.
        //   2.  Se confecciona el string de la carta.
        //       DATO1 + CHR$(9) + ...+ DATON
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short Doc_Arbitraje(InitializationObject initObject, UnitOfWorkCext01 unit, string NumOpe)
        {
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_Module1 Module1 = initObject.Module1;
            T_MODGASO MODGASO = initObject.MODGASO;
            T_MODGCHQ MODGCHQ = initObject.MODGCHQ;
            T_MODGSWF MODGSWF = initObject.MODGSWF;
            T_MODGSCE MODGSCE = initObject.MODGSCE;
            T_Mdl_Funciones_Varias Mdl_FuncionesVarias = initObject.Mdl_Funciones_Varias;
            T_MODGMTA MODGMTA = initObject.MODGMTA;
            T_MODGUSR MODGUSR = initObject.MODGUSR;
            T_MODGARB MODGARB = initObject.MODGARB;
            T_MODGFRA MODGFRA = initObject.MODGFRA;

            short n = 0;
            short m = 0;
            short z = 0;
            short c = 0;
            string s = "";
            short i = 0;
            short ContadorArbitraje = 0;
            short Contador1 = 0;
            string NomBen = "";
            short Contador2 = 0;
            short Contador3 = 0;
            short hayiva = 0;
            short NumDeb = 0;
            double suma = 0;
            double SumaIVA = 0;
            string Usr = "";


            n = (short)VB6Helpers.UBound(MODGARB.VArb);  //Arbitraje.
            m = (short)VB6Helpers.UBound(MODXVIA.VxVia);  //Vías.
            z = (short)VB6Helpers.UBound(MODXORI.VxOri);  //Orígenes.
            c = (short)VB6Helpers.UBound(Mdl_FuncionesVarias.V_gCom);  //Comisiones
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            //Partys Exportador Principal.
            s = "";
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].NombreUsado.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].DireccionUsado.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].CiudadUsado.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].PaisUsado.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].EstadoUsado.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].PostalUsado.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].Fax.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].CasBanco.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].CasPostal.Trim() + VB6Helpers.Chr(9);
            s = s + VB6Helpers.Str(0) + VB6Helpers.Chr(9);  //Indicador de que datos se deben Imprimir.

            //Operación Relacionada.-
            s = s + MODGASO.VgAso.OpeCon + VB6Helpers.Chr(9);

            //Referencia Exportador Principal.
            s = s + MODGCVD.VgCvd.OpeCon + VB6Helpers.Chr(9);

            //Referencia Cliente.
            s = s + MODGCVD.VgCvd.RefCli + VB6Helpers.Chr(9);

            //Contar cuantas Compras y Ventas existen no eliminadas.
            ContadorArbitraje = 0;
            for (i = 0; i <= (short)n; i++)
            {
                if (MODGARB.VArb[i].Status != T_MODGCVD.EstadoEli)
                {
                    ContadorArbitraje = (short)(ContadorArbitraje + 1);
                }

            }

            //envío de Nro. de Arbitarjes.
            s = s + VB6Helpers.Trim(VB6Helpers.Str(ContadorArbitraje)) + VB6Helpers.Chr(9);

            //Compras, Ventas y Paridades de Divisas.
            for (i = 0; i <= (short)n; i++)
            {
                if (MODGARB.VArb[i].Status != T_MODGCVD.EstadoEli)
                {
                    // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 's' variable as a StringBuilder6 object.
                    s = s + VB6Helpers.Trim(MODGARB.VArb[i].NemMndC) + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Trim(VB6Helpers.Str(MODGARB.VArb[i].MtoCom)) + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Trim(MODGARB.VArb[i].NemMndV) + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Trim(VB6Helpers.Str(MODGARB.VArb[i].MtoVta)) + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Trim(VB6Helpers.Str(MODGARB.VArb[i].PrdArb)) + VB6Helpers.Chr(9);
                }

            }

            //Contar cuantas Vías existen que no esten eliminadas en Moneda Extrajera.
            Contador1 = 0;
            for (i = 0; i <= (short)m; i++)
            {
                if ((MODXVIA.VxVia[i].Status != T_MODGCVD.EstadoEli) && (MODXVIA.VxVia[i].CodMon != MODGCVD.CodMonedaNac))
                {
                    Contador1 = (short)(Contador1 + 1);
                }

            }

            s = s + VB6Helpers.Trim(VB6Helpers.Str(Contador1)) + VB6Helpers.Chr(9);

            //Vía de la Remesa.
            //Datos correspondientes a las Vías en Moneda Extranjera.
            for (i = 0; i <= (short)m; i++)
            {
                if ((MODXVIA.VxVia[i].Status != T_MODGCVD.EstadoEli) && (MODXVIA.VxVia[i].CodMon != MODGCVD.CodMonedaNac))
                {
                    NomBen = Module1.PartysOpe[MODXVIA.VxVia[i].PosPrty].NombreUsado;
                    //Incluir Beneficiarios Cheques, Vales Vistas, Swift's.
                    short _switchVar1 = MODXVIA.VxVia[i].NumCta;
                    if (_switchVar1 == T_MODGCON0.IdCta_CHMEBCH || _switchVar1 == T_MODGCON0.IdCta_VVBCH)
                    {
                        n = MODXVIA.VxVia[i].IndChq;
                        NomBen = MODGCHQ.V_Chq_VVi[n].NomBen;
                    }
                    else if (_switchVar1 == T_MODGCON0.IdCta_OPC || _switchVar1 == T_MODGCON0.IdCta_OPOP)
                    {
                        n = MODXVIA.VxVia[i].IndSwf;
                        NomBen = MODGSWF.VSwf[n - 1].BenSwf.NomBen;
                    }
                    else if (_switchVar1 == T_MODGCON0.IdCta_VAM || _switchVar1 == T_MODGCON0.IdCta_VAX || _switchVar1 == T_MODGCON0.IdCta_VAMC)
                    {
                        NomBen = GetDatPrt(initObject, unit, MODXVIA.VxVia[i].IdPrty, 0, 0, "N");
                    }
                    else
                    {
                    }

                    s = s + VB6Helpers.Trim(MODGPYF1.Minuscula(NomBen)) + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Trim(MODXVIA.VxVia[i].NomVia) + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Trim(MODXVIA.VxVia[i].NemMon) + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Trim(VB6Helpers.Str(MODXVIA.VxVia[i].MtoTot)) + VB6Helpers.Chr(9);
                }

            }

            //Cargos y Abonos.
            //Contar cuantos Orígenes existen.
            Contador2 = 0;
            for (i = 0; i <= (short)z; i++)
            {
                if (MODXORI.VxOri[i].Status != T_MODGCVD.EstadoEli)
                {
                    Contador2 = (short)(Contador2 + 1);
                }

            }

            s = s + VB6Helpers.Trim(VB6Helpers.Str(Contador2)) + VB6Helpers.Chr(9);

            //Datos correspondientes a los Orígenes en Moneda Nacional.
            for (i = 0; i <= (short)z; i++)
            {
                if (MODXORI.VxOri[i].Status != T_MODGCVD.EstadoEli)
                {
                    s = s + "Débito" + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Trim(MODXORI.VxOri[i].NomOri) + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Trim(MODXORI.VxOri[i].NemMon) + VB6Helpers.Chr(9);
                    if (((MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteMN ? -1 : 0) & MODXORI.VgxOri.ImpDeb) != 0)
                    {
                        s = s + VB6Helpers.Str((MODXORI.VxOri[i].MtoTot) + (MODGSCE.VGen.MtoDeb)).Trim() + VB6Helpers.Chr(9);
                    }
                    else
                    {
                        s = s + VB6Helpers.Str((MODXORI.VxOri[i].MtoTot)).Trim() + VB6Helpers.Chr(9);
                    }

                }

            }

            //------------------------------------------------
            //Contar cuantas comisiones existen
            //------------------------------------------------
            Contador3 = 0;
            for (i = 0; i <= (short)VB6Helpers.UBound(Mdl_FuncionesVarias.V_gCom); i++)
            {
                if (Mdl_FuncionesVarias.V_gCom[i].estado != T_MODGCVD.EstadoEli && Mdl_FuncionesVarias.V_gCom[i].MtoComp > 0)
                {
                    Contador3 = (short)(Contador3 + 1);
                    if (Mdl_FuncionesVarias.V_gCom[i].MtoIvap > 0)
                    {
                        hayiva = (short)(true ? -1 : 0);
                    }
                }

            }

            for (i = 0; i <= (short)VB6Helpers.UBound(MODXORI.VxOri); i++)
            {
                if (((MODXORI.VxOri[i].Status != T_MODGCVD.EstadoEli && MODXORI.VxOri[i].CodMon == MODGSCE.VGen.MndNac && MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteMN ? -1 : 0) & MODXORI.VgxOri.ImpDeb) != 0)
                {
                    NumDeb = (short)(NumDeb + 1);
                }

            }

            if (hayiva != 0)
            {
                Contador3 = (short)(Contador3 + 1);
            }
            if (NumDeb > 0)
            {
                Contador3 = (short)(Contador3 + 1);
            }
            s = s + VB6Helpers.Trim(VB6Helpers.Str(Contador3)) + VB6Helpers.Chr(9);
            //------------------------------------------------
            //Datos correspondientes a las comisiones cobradas
            //------------------------------------------------
            if (Contador3 > 0)
            {
                //For i% = 1 To UBound(VxOri)         '
                if (NumDeb > 0 && T_MODGMTA.impflag == 1)
                {
                    //'Impuesto al Débito.-
                    s = s + "Impuesto Fijo por débito en Cuenta Corriente" + VB6Helpers.Chr(9);
                    s = s + "$" + VB6Helpers.Chr(9);
                    s = s + Format.FormatCurrency((NumDeb * MODGSCE.VGen.MtoDeb), "0") + VB6Helpers.Chr(9);
                    suma = suma + Format.StringToDouble(Format.FormatCurrency((NumDeb * MODGSCE.VGen.MtoDeb), "0"));
                }

                //Next
                for (i = 0; i <= (short)VB6Helpers.UBound(Mdl_FuncionesVarias.V_gCom); i++)
                {
                    if (Mdl_FuncionesVarias.V_gCom[i].estado != T_MODGCVD.EstadoEli && Mdl_FuncionesVarias.V_gCom[i].MtoComp > 0)
                    {
                        s = s + Mdl_FuncionesVarias.V_gCom[i].GlsCom + VB6Helpers.Chr(9);
                        s = s + "$" + VB6Helpers.Chr(9);
                        s = s + VB6Helpers.Trim(VB6Helpers.Str(Mdl_FuncionesVarias.V_gCom[i].MtoComp)) + VB6Helpers.Chr(9);
                        SumaIVA += Mdl_FuncionesVarias.V_gCom[i].MtoIvap;
                        suma += Mdl_FuncionesVarias.V_gCom[i].MtoTotp;
                    }

                }

                if (hayiva != 0)
                {
                    s = s + "IVA" + VB6Helpers.Chr(9);
                    s = s + "$" + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Str(SumaIVA) + VB6Helpers.Chr(9);
                }

                s = s + "$" + VB6Helpers.Chr(9);
                s = s + VB6Helpers.Str(suma) + VB6Helpers.Chr(9);
            }

            //Instrucciones Especiales.-
            s = s + BCH.Comex.Core.BL.XCFT.Modulos.MODGFRA.Get_InsEsp(MODGFRA, MODGCVD.VgCvd.InsExp) + VB6Helpers.Chr(9);

            //Atributos del Usuario
            s = s + MODGUSR.UsrEsp.CentroCosto + VB6Helpers.Chr(9);  //Centro de Costo.
            s = s + MODGUSR.UsrEsp.Especialista + VB6Helpers.Chr(9);  //Especialista.
            Usr = MODGUSR.UsrEsp.CentroCosto + MODGUSR.UsrEsp.Especialista;

            //Retona el correlativo de la carta.
            return SyPut_xDoc(initObject, unit, NumOpe, T_Mdl_Funciones_Varias.DocArb, s, Usr);
        }

        //Busca el nombre del Party; 1º en memoria; 2º a disco.-
        public static string GetDatPrt(InitializationObject initObject, UnitOfWorkCext01 unit, string PrtImp, short IndNom, short IndDir, string NomDir)
        {
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;

            string _retValue = "";
            string p = "";
            short i = 0;
            string s1 = "";
            string s2 = "";
            short n = 0;
            if (string.IsNullOrEmpty(PrtImp))
            {
                return _retValue;
            }
            p = PrtImp;
            p = PoneMarcaParty(PrtImp);
            i = VB6Helpers.CShort(FindPrt(Mdl_Funciones_Varias, p, IndNom, IndDir));
            if (i != -1)
            {
                s1 = Mdl_Funciones_Varias.DatPrtys[i].NomPrty;
                s2 = Mdl_Funciones_Varias.DatPrtys[i].DirPrty;
            }
            else
            {
                if (IndNom != -1)
                {
                    s1 = VB6Helpers.Trim(SyGet_Rsa(unit, p, IndNom));
                }
                if (IndDir != -1)
                {
                    s2 = VB6Helpers.Trim(SyGet_Dad(unit, p, IndDir));
                }
                n = (short)(VB6Helpers.UBound(Mdl_Funciones_Varias.DatPrtys) + 1);
                VB6Helpers.RedimPreserve(ref Mdl_Funciones_Varias.DatPrtys, 0, n);
                Mdl_Funciones_Varias.DatPrtys[n].PrtImp = p;
                Mdl_Funciones_Varias.DatPrtys[n].IndNom = IndNom;
                Mdl_Funciones_Varias.DatPrtys[n].IndDir = IndDir;
                Mdl_Funciones_Varias.DatPrtys[n].NomPrty = s1;
                Mdl_Funciones_Varias.DatPrtys[n].DirPrty = s2;
            }

            if (NomDir == "N")
            {
                _retValue = s1;
            }
            if (NomDir == "D")
            {
                return s2;
            }

            return _retValue;
        }

        //Rellena el Party con la marca |.-
        public static string PoneMarcaParty(string Party)
        {
            string p = Party;
            short n = 0;
            short i = 0;
            if (VB6Helpers.Instr(p, "|") == 0)
            {
                n = (short)(12 - VB6Helpers.Len(p));
            }

            if (n > 0)
            {
                for (i = 1; i <= (short)n; i++)
                {
                    p += "|";
                }

            }
            return p;
        }

        //Retorna el indice en el arreglo de nombres de partys en memoria.-
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        // UPGRADE_INFO (#0561): The 'FindPrt' symbol was defined without an explicit "As" clause.
        public static dynamic FindPrt(T_Mdl_Funciones_Varias Mdl_Funciones_Varias, string PrtImp, short IndNom, short IndDir)
        {
            dynamic _retValue = null;
            short n = 0;
            short i = 0;
            _retValue = -1;  //Inicializa como no encontrado.-
            try
            {
                n = (short)VB6Helpers.UBound(Mdl_Funciones_Varias.DatPrtys);
                if (n >= 0)
                {
                    for (i = 0; i <= (short)n; i++)
                    {
                        if (Mdl_Funciones_Varias.DatPrtys[i].PrtImp == PrtImp && Mdl_Funciones_Varias.DatPrtys[i].IndNom == IndNom && Mdl_Funciones_Varias.DatPrtys[i].IndDir == IndDir)
                        {
                            _retValue = i;
                            break;
                        }

                    }

                }
                else
                {
                    Mdl_Funciones_Varias.DatPrtys = new Type_DatPrty[0];
                }
            }
            catch (Exception e)
            {
                Mdl_Funciones_Varias.DatPrtys = new Type_DatPrty[0];
            }
            return _retValue;
        }

        //Retorna la Razón Social del Party.-
        public static string SyGet_Rsa(UnitOfWorkCext01 unit, string IdParty, short IdNombre)
        {
            string _retValue = "-1";
            try
            {
                // IGNORED: On Error GoTo SyGet_RsaErr
                _retValue = unit.SceRepository.EjecutarSP<string>("sce_rsa_s03", MODGSYB.dbcharSy(IdParty), IdNombre.ToString()).First();
            }
            catch (Exception _ex)
            {
                _retValue = "-1";
                //VB6Helpers.MsgBox("Se ha producido un error al tratar de leer la Razon Social del Participante (Sce_Rsa). El Servidor reporta : [" + VB6Helpers.Left(VB6Helpers.Trim(Mdl_SRM.ParamSrm8k.mensaje.Value), 100) + "]. Reporte este problema.", MsgBoxStyle.Information, "Participantes de Comercio Exterior");
            }
            return _retValue;
        }

        //****************************************************************************
        //   1.  Retorna la Dirección de un Party en particular.
        //****************************************************************************
        public static string SyGet_Dad(UnitOfWorkCext01 unit, string IdParty, short IdDireccion)
        {
            string _retValue = String.Empty;
            string Que = "";
            string R = "";
            try
            {
                _retValue = unit.SceRepository.EjecutarSP<string>("sce_dad_s03", MODGSYB.dbcharSy(IdParty), IdDireccion.ToString()).First();
            }
            catch (Exception _ex)
            {
                //VB6Helpers.MsgBox("Se ha producido un error al tratar de leer la Dirección del Participante (Sce_Dad). El Servidor reporta : [" + VB6Helpers.Left(VB6Helpers.Trim(Mdl_SRM.ParamSrm8k.mensaje.Value), 100) + "]. Reporte este problema.", MsgBoxStyle.Information, "Participantes de Comercio Exterior");
                _retValue = String.Empty;
            }
            return _retValue;
        }

        //****************************************************************************
        //   1.  Graba la Carta de Planillas Visibles de Exportación.
        //   2.  Se confecciona el string de la carta.
        //       DATO1 + CHR$(9) + ...+ DATON
        //****************************************************************************
        public static short Doc_xPlvCob(InitializationObject initObject, UnitOfWorkCext01 unit, string NumOpe, string RefExp)
        {
            T_Module1 Module1 = initObject.Module1;
            T_MODGASO MODGASO = initObject.MODGASO;
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_MODXPLN1 MODXPLN1 = initObject.MODXPLN1;
            T_MODXPLN0 MODXPLN0 = initObject.MODXPLN0;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODGSCE MODGSCE = initObject.MODGSCE;
            T_MODGMTA MODGMTA = initObject.MODGMTA;
            T_MODGUSR MODGUSR = initObject.MODGUSR;
            T_MODGFRA MODGFRA = initObject.MODGFRA;
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;

            string s = "";
            short n = 0;
            short i = 0;
            short Contador3 = 0;
            short hayiva = 0;
            short NumDeb = 0;
            double suma = 0;
            double SumaIVA = 0;
            string Usr = "";
            //Partys Exportador Principal.
            s = s + Module1.PartysOpe[Mdl_Funciones_Varias.IExp].NombreUsado.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[Mdl_Funciones_Varias.IExp].DireccionUsado.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[Mdl_Funciones_Varias.IExp].CiudadUsado.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[Mdl_Funciones_Varias.IExp].EstadoUsado.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[Mdl_Funciones_Varias.IExp].PostalUsado.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[Mdl_Funciones_Varias.IExp].PaisUsado.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[Mdl_Funciones_Varias.IExp].Fax.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[Mdl_Funciones_Varias.IExp].CasBanco.Trim() + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[Mdl_Funciones_Varias.IExp].CasPostal.Trim() + VB6Helpers.Chr(9);
            s = s + " " + VB6Helpers.Str(Module1.PartysOpe[Mdl_Funciones_Varias.IExp].Enviara) + VB6Helpers.Chr(9);

            //Operación Relacionada.-
            s = s + MODGASO.VgAso.OpeCon + VB6Helpers.Chr(9);

            //Referencia Exportador Principal.
            s = s + RefExp + VB6Helpers.Chr(9);

            //Referencia Cliente.-
            s = s + MODGCVD.VgCvd.RefCli + VB6Helpers.Chr(9);

            //Planillas Visibles de Exportación.
            for (i = 0; i <= (short)VB6Helpers.UBound(MODXPLN1.VxPlvs); i++)
            {
                if (MODXPLN1.VxPlvs[i].Status != T_MODGCVD.EstadoEli)
                {
                    n = (short)(n + 1);
                }

            }

            s = s + VB6Helpers.Str(n) + VB6Helpers.Chr(9);

            //Se aborta la carta porque no hay planillas.-
            if (n == 0)
            {
                return 0;
            }

            //Número de Planillas.-
            for (i = 0; i <= (short)VB6Helpers.UBound(MODXPLN1.VxPlvs); i++)
            {
                if (MODXPLN1.VxPlvs[i].Status != T_MODGCVD.EstadoEli)
                {
                    // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 's' variable as a StringBuilder6 object.
                    s = s + VB6Helpers.Trim(MODXPLN1.VxPlvs[i].NumPre) + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Trim(VB6Helpers.Str(MODXPLN1.VxPlvs[i].TipPln)) + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Trim(MODXPLN1.VxPlvs[i].numdec) + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Trim(MODXPLN0.VxDatP.NemMnd) + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Trim(VB6Helpers.Str(MODXPLN1.VxPlvs[i].ValLiq)) + VB6Helpers.Chr(9);
                }

            }

            //Débitos y Abonos.
            n = 0;
            for (i = 0; i <= (short)VB6Helpers.UBound(MODXORI.VxOri); i++)
            {
                if (!string.IsNullOrEmpty(MODXORI.VxOri[i].NomOri) && MODXORI.VxOri[i].MtoTot > 0)
                {
                    n = (short)(n + 1);
                }

            }

            for (i = 0; i <= (short)VB6Helpers.UBound(MODXVIA.VxVia); i++)
            {
                if (!string.IsNullOrEmpty(MODXVIA.VxVia[i].NomVia) && MODXVIA.VxVia[i].MtoTot > 0)
                {
                    n = (short)(n + 1);
                }

            }

            s = s + VB6Helpers.Str(n) + VB6Helpers.Chr(9);
            if (n > 0)
            {
                //se ponen los paddings para que guarde lo mismo que el legacy
                for (i = 0; i <= (short)VB6Helpers.UBound(MODXORI.VxOri); i++)
                {
                    if (!string.IsNullOrEmpty(MODXORI.VxOri[i].NomOri))
                    {
                        s = s + "Debitamos en " + MODXORI.VxOri[i].NomOri.PadRight(50) + VB6Helpers.Chr(9);
                        s = s + MODXORI.VxOri[i].NemMon + VB6Helpers.Chr(9);
                        if (((MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteMN ? -1 : 0) & MODXORI.VgxOri.ImpDeb) != 0)
                        {
                            s = s + VB6Helpers.Str((MODXORI.VxOri[i].MtoTot) + (MODGSCE.VGen.MtoDeb)) + VB6Helpers.Chr(9);
                        }
                        else
                        {
                            s = s + VB6Helpers.Str(MODXORI.VxOri[i].MtoTot) + VB6Helpers.Chr(9);
                        }

                    }

                }

                for (i = 0; i <= (short)VB6Helpers.UBound(MODXVIA.VxVia); i++)
                {
                    if (!string.IsNullOrEmpty(MODXVIA.VxVia[i].NomVia))
                    {
                        s = s + "Acreditamos en " + MODXVIA.VxVia[i].NomVia.PadRight(50) + VB6Helpers.Chr(9);
                        s = s + MODXVIA.VxVia[i].NemMon + VB6Helpers.Chr(9);
                        s = s + VB6Helpers.Str(MODXVIA.VxVia[i].MtoTot) + VB6Helpers.Chr(9);
                    }

                }

            }

            if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 0)
            {
                //------------------------------------------------
                //Contar cuantas comisiones existen
                //------------------------------------------------
                Contador3 = 0;
                for (i = 0; i <= (short)VB6Helpers.UBound(Mdl_Funciones_Varias.V_gCom); i++)
                {
                    if (Mdl_Funciones_Varias.V_gCom[i].estado != T_MODGCVD.EstadoEli && Mdl_Funciones_Varias.V_gCom[i].MtoComp > 0)
                    {
                        Contador3 = (short)(Contador3 + 1);
                        if (Mdl_Funciones_Varias.V_gCom[i].MtoIvap > 0)
                        {
                            hayiva = (short)(true ? -1 : 0);
                        }
                    }

                }

                for (i = 0; i <= (short)VB6Helpers.UBound(MODXORI.VxOri); i++)
                {
                    if (((MODXORI.VxOri[i].Status != T_MODGCVD.EstadoEli && MODXORI.VxOri[i].CodMon == MODGSCE.VGen.MndNac && MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteMN ? -1 : 0) & MODXORI.VgxOri.ImpDeb) != 0)
                    {
                        NumDeb = (short)(NumDeb + 1);
                    }

                }

                if (hayiva != 0)
                {
                    Contador3 = (short)(Contador3 + 1);
                }
                if (NumDeb > 0)
                {
                    Contador3 = (short)(Contador3 + 1);
                }
                s = s + VB6Helpers.Trim(VB6Helpers.Str(Contador3)) + VB6Helpers.Chr(9);
                //------------------------------------------------
                //Datos correspondientes a las comisiones cobradas
                //------------------------------------------------
                if (Contador3 > 0)
                {
                    //For i% = 1 To UBound(VxOri)             'se comentario por problemas al generar cartas
                    if (NumDeb > 0 && T_MODGMTA.impflag == 1)
                    {
                        //Impuesto al Débito.-
                        s = s + "Impuesto Fijo por débito en Cuenta Corriente" + VB6Helpers.Chr(9);
                        s = s + "$" + VB6Helpers.Chr(9);
                        s = s + VB6Helpers.Str(NumDeb * MODGSCE.VGen.MtoDeb) + VB6Helpers.Chr(9);
                        suma = suma + NumDeb * MODGSCE.VGen.MtoDeb;
                    }

                    //Next

                    for (i = 0; i <= (short)VB6Helpers.UBound(Mdl_Funciones_Varias.V_gCom); i++)
                    {
                        if (Mdl_Funciones_Varias.V_gCom[i].estado != T_MODGCVD.EstadoEli && Mdl_Funciones_Varias.V_gCom[i].MtoComp > 0)
                        {
                            s = s + Mdl_Funciones_Varias.V_gCom[i].GlsCom + VB6Helpers.Chr(9);
                            s = s + "$" + VB6Helpers.Chr(9);
                            s = s + VB6Helpers.Trim(VB6Helpers.Str(Mdl_Funciones_Varias.V_gCom[i].MtoComp)) + VB6Helpers.Chr(9);
                            SumaIVA += Mdl_Funciones_Varias.V_gCom[i].MtoIvap;
                            suma += Mdl_Funciones_Varias.V_gCom[i].MtoTotp;
                        }

                    }

                    if (hayiva != 0)
                    {
                        s = s + "IVA" + VB6Helpers.Chr(9);
                        s = s + "$" + VB6Helpers.Chr(9);
                        s = s + VB6Helpers.Str(SumaIVA) + VB6Helpers.Chr(9);
                    }

                    s = s + "$" + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Str(suma) + VB6Helpers.Chr(9);
                }

            }

            //Instrucciones al Exportador.
            s = s + BCH.Comex.Core.BL.XCFT.Modulos.MODGFRA.Get_InsEsp(MODGFRA, MODGCVD.VgCvd.InsExp) + VB6Helpers.Chr(9);

            //Atributos del Usuario
            s = s + MODGUSR.UsrEsp.CentroCosto + VB6Helpers.Chr(9);  //Centro de Costo.
            s = s + MODGUSR.UsrEsp.Especialista + VB6Helpers.Chr(9);  //Especialista.
            Usr = MODGUSR.UsrEsp.CentroCosto + MODGUSR.UsrEsp.Especialista;

            //Retona el correlativo de la carta. --> ESTO
            return SyPut_xDoc(initObject, unit, NumOpe, T_Mdl_Funciones_Varias.DocxRegPln, s, Usr);
        }

        public static short Doc_CVDImp(InitializationObject initObject, UnitOfWorkCext01 unit, string NumOpe)
        {
            T_MODGASO MODGASO = initObject.MODGASO;
            T_Module1 Module1 = initObject.Module1;
            T_MODGUSR MODGUSR = initObject.MODGUSR;
            T_MODGFYS MODGFYS = initObject.MODGFYS;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_MODGMTA MODGMTA = initObject.MODGMTA;
            T_MODGSCE MODGSCE = initObject.MODGSCE;
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;

            short _retValue = 0;
            string s = "";
            short i = 0;
            string mevd = "";
            short z = 0;
            short Contador3 = 0;
            short hayiva = 0;
            double SumaIVA = 0;
            double suma = 0;
            string iva = "";
            short DebCtaCte = 0;
            string Usr = "";
            //Carta de Compra Venta.-

            _retValue = (short)(false ? -1 : 0);

            s = s + NumOpe + VB6Helpers.Chr(9);  //Referencia.-

            s = s + MODGASO.VgAso.OpeSin + VB6Helpers.Chr(9);

            //------------------------------------------------------------------------
            //Datos del Importador.-
            //------------------------------------------------------------------------

            s = s + BCH.Comex.Core.BL.XCFT.Modulos.MODGFYS.Escribe_Nombre(ref Module1.PartysOpe[T_MODGCVD.ICli].NombreUsado) + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].DireccionUsado + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].CiudadUsado + VB6Helpers.Chr(9);
            if (VB6Helpers.Trim(VB6Helpers.UCase(Module1.PartysOpe[T_MODGCVD.ICli].PaisUsado)) == "CHILE")
            {
                s = s + "" + VB6Helpers.Chr(9);
            }
            else
            {
                s = s + Module1.PartysOpe[T_MODGCVD.ICli].EstadoUsado + VB6Helpers.Chr(9);
            }

            s = s + Module1.PartysOpe[T_MODGCVD.ICli].PostalUsado + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].Fax + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].CasBanco + VB6Helpers.Chr(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].CasPostal + VB6Helpers.Chr(9);
            s = s + VB6Helpers.Format(VB6Helpers.CStr(Module1.PartysOpe[T_MODGCVD.ICli].Enviara)) + VB6Helpers.Chr(9);

            //------------------------------------------------------------------------
            //Compra, Venta o Ambos.-
            //------------------------------------------------------------------------

            s = s + VB6Helpers.Str(2) + VB6Helpers.Chr(9);

            //------------------------------------------------------------------------
            //Seguro, Flete y Gastos del Cedente.-
            //------------------------------------------------------------------------

            s = s + MODGUSR.UsrEsp.Nombre + VB6Helpers.Chr(9);  //Datos del Especialista.-
            s = s + MODGUSR.UsrEsp.Direccion + VB6Helpers.Chr(9);
            s = s + MODGUSR.UsrEsp.Telefono + VB6Helpers.Chr(9);
            s = s + MODGUSR.UsrEsp.Fax + VB6Helpers.Chr(9);
            //------------------------------------------------------------------------
            //Concepto de la Compra Venta.-
            //------------------------------------------------------------------------
            short _switchVar1 = MODGFYS.CVD.Operacion;

            if (_switchVar1 == T_MODGFYS.FLT)
            {
                s = s + "Flete en Chile" + VB6Helpers.Chr(9);
            }
            else if (_switchVar1 == T_MODGFYS.SEG)
            {
                s = s + "Seguro en Chile" + VB6Helpers.Chr(9);
            }
            else if (_switchVar1 == T_MODGFYS.FLTSEG)
            {
                s = s + "Flete y Seguro en Chile" + VB6Helpers.Chr(9);
            }
            else if (_switchVar1 == T_MODGFYS.ENDREC)
            {
                s = s + "Endoso Recibido" + VB6Helpers.Chr(9);
            }
            else
            {
                s = s + "" + VB6Helpers.Chr(9);
            }

            //------------------------------------------------------------------------
            //operaciones de compra y venta realizadas.-
            //------------------------------------------------------------------------
            s = s + VB6Helpers.Str(VB6Helpers.UBound(MODGFYS.VgFyS) + 1) + VB6Helpers.Chr(9);
            for (i = 0; i <= (short)VB6Helpers.UBound(MODGFYS.VgFyS); i++)
            {
                // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 's' variable as a StringBuilder6 object.
                s = s + ((MODGFYS.VgFyS[i].EleTip) + 1) + VB6Helpers.Chr(9);
                s = s + BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_NemMnd(MODGTAB0, unit, MODGFYS.VgFyS[i].CodMon[1]) + VB6Helpers.Chr(9);
                s = s + MODGSYB.dbmontoSyForRead((MODGFYS.VgFyS[i].monto[1]) + (MODGFYS.VgFyS[i].monto[2])) + VB6Helpers.Chr(9);
                s = s + MODGSYB.dbmontoSyForRead(MODGFYS.VgFyS[i].TipCam[1]) + VB6Helpers.Chr(9);
                s = s + MODGSYB.dbmontoSyForRead((MODGFYS.VgFyS[i].mtopss[1]) + (MODGFYS.VgFyS[i].mtopss[2])) + VB6Helpers.Chr(9);
                mevd = MODGSYB.dbmontoSyForRead(MODGFYS.VgFyS[i].TotPss);
            }

            //------------------------------------------------------------------------
            //Número de Beneficiarios.-
            //------------------------------------------------------------------------

            s += Mdl_Funciones.Det_Vias(initObject, unit);  //Se agrega el detalle de las vias

            //------------------------------------------------------------------------
            //Número de Débitos.-
            //------------------------------------------------------------------------
            z = -1;

            z = (short)VB6Helpers.UBound(MODXORI.VxOri);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            if (z >= 0)
            {
                Contador3 = 0;
                for (i = 0; i <= (short)z; i++)
                {
                    if (MODXORI.VxOri[i].Status != T_MODGCVD.EstadoEli)
                    {
                        Contador3 = (short)(Contador3 + 1);
                    }

                }

                s = s + VB6Helpers.Trim(VB6Helpers.Str(Contador3)) + VB6Helpers.Chr(9);
                for (i = 0; i <= (short)z; i++)
                {
                    if (MODXORI.VxOri[i].Status != T_MODGCVD.EstadoEli)
                    {
                        s = s + "Débito" + VB6Helpers.Chr(9);  //Ver el caso de los abonos
                        if (string.IsNullOrWhiteSpace(MODXORI.VxOri[i].CtaCte_t))
                        {
                            s = s + VB6Helpers.Trim(MODXORI.VxOri[i].NomOri) + VB6Helpers.Chr(9);
                        }
                        else
                        {
                            s = s + VB6Helpers.Trim(MODXORI.VxOri[i].NomOri) + " " + VB6Helpers.Trim(MODXORI.VxOri[i].CtaCte_t) + VB6Helpers.Chr(9);
                        }

                        s = s + VB6Helpers.Trim(MODXORI.VxOri[i].NemMon) + VB6Helpers.Chr(9);
                        s = s + VB6Helpers.Trim(VB6Helpers.Str(MODXORI.VxOri[i].MtoTot)) + VB6Helpers.Chr(9);
                    }

                }

            }
            else
            {
                s = s + VB6Helpers.Str(-1) + VB6Helpers.Chr(9);
            }

            if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 0)
            {
                //------------------------------------------------------------------------
                //Número de Detalles.-
                //------------------------------------------------------------------------
                Contador3 = 0;
                for (i = 0; i <= (short)VB6Helpers.UBound(Mdl_Funciones_Varias.V_gCom); i++)
                {
                    if (Mdl_Funciones_Varias.V_gCom[i].estado != T_MODGCVD.EstadoEli && Mdl_Funciones_Varias.V_gCom[i].MtoComp > 0)
                    {
                        Contador3 = (short)(Contador3 + 1);
                        if (Mdl_Funciones_Varias.V_gCom[i].MtoIvap > 0)
                        {
                            hayiva = (short)(true ? -1 : 0);
                        }
                    }

                }

                if (!string.IsNullOrEmpty(MODGCVD.VgCvd.AvisoDC) && VB6Helpers.Instr(MODGCVD.VgCvd.AvisoDC, "N") > 0)
                {
                    Contador3 = (short)(Contador3 + 1);
                }

                if ((hayiva & (T_MODGMTA.impflag == 1 ? -1 : 0)) != 0)
                {
                    Contador3 = (short)(Contador3 + 1);  // validación para los espacios al generar cartas JFO
                }
                if (Contador3 > 0)
                {
                    s = s + VB6Helpers.Trim(VB6Helpers.Str(Contador3 + 1)) + VB6Helpers.Chr(9);  //Se suma uno por el total de mnd. Ext. Vendida
                }
                else
                {
                    s = s + VB6Helpers.Trim(VB6Helpers.Str(Contador3)) + VB6Helpers.Chr(9);
                }

                //------------------------------------------------
                //Datos correspondientes a las comisiones cobradas
                //------------------------------------------------
                if (Contador3 > 0)
                {
                    for (i = 0; i <= (short)VB6Helpers.UBound(Mdl_Funciones_Varias.V_gCom); i++)
                    {
                        if (Mdl_Funciones_Varias.V_gCom[i].estado != T_MODGCVD.EstadoEli && Mdl_Funciones_Varias.V_gCom[i].MtoComp > 0)
                        {
                            s = s + Mdl_Funciones_Varias.V_gCom[i].GlsCom + VB6Helpers.Chr(9);
                            s = s + "$" + VB6Helpers.Chr(9);
                            s = s + VB6Helpers.Trim(VB6Helpers.Str(Mdl_Funciones_Varias.V_gCom[i].MtoComp)) + VB6Helpers.Chr(9);
                            SumaIVA += Mdl_Funciones_Varias.V_gCom[i].MtoIvap;
                            suma += Mdl_Funciones_Varias.V_gCom[i].MtoTotp;
                        }

                    }

                    if (hayiva != 0)
                    {
                        iva = Mdl_Acceso.GetConfigValue("FundTransfer.General.MontoIVA");
                        s = s + "Impuesto IVA del " + iva + "%. " + VB6Helpers.Chr(9);
                        s = s + "$" + VB6Helpers.Chr(9);
                        s = s + VB6Helpers.Str(SumaIVA) + VB6Helpers.Chr(9);
                    }

                    DebCtaCte = (short)(false ? -1 : 0);
                    if (!string.IsNullOrEmpty(MODGCVD.VgCvd.AvisoDC) && VB6Helpers.Instr(MODGCVD.VgCvd.AvisoDC, "N") > 0)
                    {
                        DebCtaCte = (short)(true ? -1 : 0);
                    }

                    if ((DebCtaCte & (T_MODGMTA.impflag == 1 ? -1 : 0)) != 0)
                    {
                        s = s + "Impuesto Fijo por débito en Cuenta Corriente." + VB6Helpers.Chr(9);
                        s = s + "$" + VB6Helpers.Chr(9);
                        s = s + VB6Helpers.Str(MODGSCE.VGen.MtoDeb) + VB6Helpers.Chr(9);
                    }

                    s = s + "Monto en pesos de la moneda extranjera vendida" + VB6Helpers.Chr(9);
                    s = s + "$" + VB6Helpers.Chr(9);
                    s = s + mevd + VB6Helpers.Chr(9);
                }

            }

            Usr = MODGUSR.UsrEsp.CentroCosto + MODGUSR.UsrEsp.Especialista;

            MODGCVD.VgCvd.DocCvdI = SyPut_xDoc(initObject, unit, NumOpe, T_Mdl_Funciones_Varias.DocCvdI, s, Usr);
            //------------------------------------------------------------------------
            return (short)(true ? -1 : 0);
        }

        //Registra el Log de la Compra-Venta.-
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static void Log_Cvd(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            // UPGRADE_INFO (#05B1): The 'Vli' variable wasn't declared explicitly.
            short Vli = 0;
            // UPGRADE_INFO (#05B1): The 'Arb' variable wasn't declared explicitly.
            short Arb = 0;
            // UPGRADE_INFO (#05B1): The 'pli' variable wasn't declared explicitly.
            short pli = 0;
            // UPGRADE_INFO (#05B1): The 'Ope' variable wasn't declared explicitly.
            string Ope = "";
            // UPGRADE_INFO (#05B1): The 'rut' variable wasn't declared explicitly.
            string rut = "";
            // UPGRADE_INFO (#05B1): The 'tip' variable wasn't declared explicitly.
            string tip = "";
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;
            // UPGRADE_INFO (#05B1): The 'Tpli' variable wasn't declared explicitly.
            string Tpli = "";
            // UPGRADE_INFO (#05B1): The 'Nem' variable wasn't declared explicitly.
            string Nem = "";
            // UPGRADE_INFO (#05B1): The 'mto' variable wasn't declared explicitly.
            string mto = "";
            // UPGRADE_INFO (#05B1): The 'TC' variable wasn't declared explicitly.
            string TC = "";
            // UPGRADE_INFO (#05B1): The 'datos' variable wasn't declared explicitly.
            string datos = "";
            // UPGRADE_INFO (#05B1): The 'NemC' variable wasn't declared explicitly.
            string NemC = "";
            // UPGRADE_INFO (#05B1): The 'MtoC' variable wasn't declared explicitly.
            string MtoC = "";
            // UPGRADE_INFO (#05B1): The 'NemV' variable wasn't declared explicitly.
            string NemV = "";
            // UPGRADE_INFO (#05B1): The 'MtoV' variable wasn't declared explicitly.
            string MtoV = "";
            // UPGRADE_INFO (#05B1): The 'NemM' variable wasn't declared explicitly.
            string NemM = "";
            // UPGRADE_INFO (#05B1): The 'MtoL' variable wasn't declared explicitly.
            string MtoL = "";
            // UPGRADE_INFO (#05B1): The 'MtoI' variable wasn't declared explicitly.
            string MtoI = "";
            // UPGRADE_INFO (#05B1): The 'MtoE' variable wasn't declared explicitly.
            string MtoE = "";
            // UPGRADE_INFO (#05B1): The 'TipC' variable wasn't declared explicitly.
            string TipC = "";
            // UPGRADE_INFO (#05B1): The 'lvs' variable wasn't declared explicitly.
            short lvs = 0;
            // UPGRADE_INFO (#05B1): The 'n' variable wasn't declared explicitly.
            short n = 0;
            // UPGRADE_INFO (#05B1): The 'MtoB' variable wasn't declared explicitly.
            string MtoB = "";
            // UPGRADE_INFO (#05B1): The 'MtoP' variable wasn't declared explicitly.
            string MtoP = "";
            // UPGRADE_INFO (#05B1): The 'MtoD' variable wasn't declared explicitly.
            string MtoD = "";
            // UPGRADE_INFO (#05B1): The 'NumPli' variable wasn't declared explicitly.
            string NumPli = "";
            // UPGRADE_INFO (#05B1): The 'FecPli' variable wasn't declared explicitly.
            string FecPli = "";
            // UPGRADE_INFO (#05B1): The 'MtoOpe' variable wasn't declared explicitly.
            string MtoOpe = "";
            // UPGRADE_INFO (#05B1): The 'Mtopar' variable wasn't declared explicitly.
            string Mtopar = "";
            // UPGRADE_INFO (#05B1): The 'MtoDol' variable wasn't declared explicitly.
            string MtoDol = "";
            // UPGRADE_INFO (#05B1): The 'TipCam' variable wasn't declared explicitly.
            string TipCam = "";
            // UPGRADE_INFO (#05B1): The 'MtoNac' variable wasn't declared explicitly.
            string MtoNac = "";


            Vli = (short)VB6Helpers.UBound(initObj.MODGPLI1.Vplis);
            Arb = (short)VB6Helpers.UBound(initObj.MODGARB.VArb);
            pli = (short)VB6Helpers.UBound(initObj.MODGCVD.VgPli);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            Ope = initObj.MODGCVD.VgCvd.OpeSin;
            rut = initObj.Module1.PartysOpe[0].rut;
            if (initObj.MODGCVD.VgCvd.TipCVD == 1)
            {
                tip = "CVD";
            }

            if (initObj.MODGCVD.VgCvd.TipCVD == 2)
            {
                tip = "ARB";
            }

            if (initObj.MODGCVD.VgCvd.TipCVD == 3)
            {
                tip = "VEX";
            }

            //Se verifica el tipo de operción realizada
            //-----------------------------------------
            short _switchVar1 = initObj.MODGCVD.VgCvd.TipCVD;
            //Planillas invisibles ( Compra/Venta )
            if (_switchVar1 == 1)
            {
                for (i = 1; i <= (short)pli; i++)
                {
                    Tpli = initObj.MODGCVD.VgPli[i].TipCVD;
                    Nem = VB6Helpers.Right(VB6Helpers.Space(3) + initObj.MODGCVD.VgPli[i].NemMnd, 3);
                    mto = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((initObj.MODGCVD.VgPli[i].MtoCVD), T_MODGCON0.FormatoConDec), 20);
                    TC = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((initObj.MODGCVD.VgPli[i].TipCam), "###,###,###,##0.0000"), 20);
                }

                datos = Tpli + " " + Nem + " " + mto + " " + TC;
                goto Planillas;

                //Arbitrajes
            }
            else if (_switchVar1 == 2)
            {
                for (i = 1; i <= (short)Arb; i++)
                {
                    NemC = VB6Helpers.Right(VB6Helpers.Space(3) + initObj.MODGARB.VArb[i].NemMndC, 3);
                    MtoC = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((initObj.MODGARB.VArb[i].MtoCom), T_MODGCON0.FormatoConDec), 20);
                    NemV = VB6Helpers.Right(VB6Helpers.Space(3) + initObj.MODGARB.VArb[i].NemMndV, 3);
                    MtoV = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((initObj.MODGARB.VArb[i].MtoVta), T_MODGCON0.FormatoConDec), 20);
                }

                datos = NemC + " " + MtoC + " " + NemV + " " + MtoV;
                goto Planillas;

                //Planillas exportaciones
            }
            else if (_switchVar1 == 3)
            {
                NemM = VB6Helpers.Right(VB6Helpers.Space(3) + initObj.MODXPLN0.VxDatP.NemMnd, 3);
                MtoL = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((initObj.MODXPLN0.VxDatP.MtoLiq), T_MODGCON0.FormatoConDec), 20);
                MtoI = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((initObj.MODXPLN0.VxDatP.MtoInf), T_MODGCON0.FormatoConDec), 20);
                MtoE = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((initObj.MODXPLN0.VxDatP.mtotran), T_MODGCON0.FormatoConDec), 20);
                TipC = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((initObj.MODXPLN0.VxDatP.TipCam), "###,###,###,##0.0000"), 20);
                datos = NemM + " " + MtoL + " " + MtoI + " " + MtoE + " " + TipC;

                lvs = (short)VB6Helpers.UBound(initObj.MODXPLN1.VxPlvs);
                // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
                // IGNORED: On Error GoTo 0
                for (i = 1; i <= (short)lvs; i++)
                {
                    n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(initObj.MODGTAB0, unit, initObj.MODXPLN1.VxPlvs[i].CodMnd);
                    NemM = VB6Helpers.Right(VB6Helpers.Space(3) + initObj.MODGTAB0.VMnd[n].Mnd_MndNmc, 3);
                    MtoB = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((initObj.MODXPLN1.VxPlvs[i].MtoBru), T_MODGCON0.FormatoConDec), 20);
                    MtoL = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((initObj.MODXPLN1.VxPlvs[i].MtoLiq), T_MODGCON0.FormatoConDec), 20);
                    MtoP = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((initObj.MODXPLN1.VxPlvs[i].Mtopar), "###,###,###,##0.0000"), 20);
                    MtoD = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((initObj.MODXPLN1.VxPlvs[i].MtoDol), T_MODGCON0.FormatoConDec), 20);
                    TipC = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((initObj.MODXPLN1.VxPlvs[i].TipCam), "###,###,###,##0.0000"), 20);
                    datos = NemM + " " + MtoB + " " + MtoL + " " + MtoP + " " + MtoD + " " + TipC;
                }

            }

            Planillas:

            Vli = (short)VB6Helpers.UBound(initObj.MODGPLI1.Vplis);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            for (i = 1; i <= (short)Vli; i++)
            {
                NumPli = initObj.MODGPLI1.Vplis[i].NumPli;
                FecPli = VB6Helpers.Format(initObj.MODGPLI1.Vplis[i].FecPli, "dd/MM/yyyy");
                MtoOpe = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((initObj.MODGPLI1.Vplis[i].MtoOpe), T_MODGCON0.FormatoConDec), 20);
                Mtopar = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((initObj.MODGPLI1.Vplis[i].Mtopar), "###,###,###,##0.0000"), 20);
                MtoDol = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((initObj.MODGPLI1.Vplis[i].MtoDol), T_MODGCON0.FormatoConDec), 20);
                TipCam = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((initObj.MODGPLI1.Vplis[i].TipCam), "###,###,###,##0.0000"), 20);
                MtoNac = VB6Helpers.Right(VB6Helpers.Space(20) + Format.FormatCurrency((initObj.MODGPLI1.Vplis[i].MtoNac), T_MODGCON0.FormatoConDec), 20);
                datos = NumPli + " " + FecPli + " " + MtoOpe + " " + Mtopar + " " + MtoDol + " " + TipCam + " " + MtoNac;
            }

        }

        //****************************************************************************
        //   1.  Graba la Carta de Planillas Sin Operacion.
        //   2.  Se confecciona el string de la carta.
        //       DATO1 + CHR$(9) + ...+ DATON
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short Doc_PlanSO(InitializationObject initObject, UnitOfWorkCext01 unit, string NumOpe)
        {
            T_MODGASO MODGASO = initObject.MODGASO;//esto es lo primero que hay que revisar
            T_Module1 Module1 = initObject.Module1;
            T_MODGUSR MODGUSR = initObject.MODGUSR;
            T_MODPREEM MODPREEM = initObject.MODPREEM;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_MODGSCE MODGSCE = initObject.MODGSCE;
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_MODGMTA MODGMTA = initObject.MODGMTA;
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;

            short _retValue = 0;
            //Carta de Planillas Sin Operacion.-
            Plan_Reemp Reem_Aux = new Plan_Reemp();
            // UPGRADE_INFO (#0561): The 'primero' symbol was defined without an explicit "As" clause.
            dynamic primero = null;
            // UPGRADE_INFO (#0561): The 'comparado' symbol was defined without an explicit "As" clause.
            dynamic comparado = null;
            // UPGRADE_INFO (#0561): The 'contpos' symbol was defined without an explicit "As" clause.
            dynamic contpos = null;
            short conttran = 0;
            double MtoTotPss = 0D;
            string s = "";
            short i = 0;
            short j = 0;
            double mtopss = 0;
            string mevd = "";
            short z = 0;
            short Contador3 = 0;
            short hayiva = 0;
            double SumaIVA = 0;
            double suma = 0;
            string iva = "";
            short DebCtaCte = 0;
            string Usr = "";

            _retValue = (short)(false ? -1 : 0);

            s = s + NumOpe + Convert.ToChar(9);  //Referencia.-
            s = s + MODGASO.VgAso.OpeSin + Convert.ToChar(9);

            //------------------------------------------------------------------------
            //Datos del Importador.-
            //------------------------------------------------------------------------

            s = s + MODGFYS.Escribe_Nombre(ref Module1.PartysOpe[T_MODGCVD.ICli].NombreUsado) + Convert.ToChar(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].DireccionUsado.Trim() + Convert.ToChar(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].CiudadUsado.Trim() + Convert.ToChar(9);
            if (VB6Helpers.Trim(VB6Helpers.UCase(Module1.PartysOpe[T_MODGCVD.ICli].PaisUsado)) == "CHILE")
            {
                s = s + "" + Convert.ToChar(9);
            }
            else
            {
                s = s + Module1.PartysOpe[T_MODGCVD.ICli].EstadoUsado.Trim() + Convert.ToChar(9);
            }

            s = s + Module1.PartysOpe[T_MODGCVD.ICli].PostalUsado.Trim() + Convert.ToChar(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].Fax.Trim() + Convert.ToChar(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].CasBanco.Trim() + Convert.ToChar(9);
            s = s + Module1.PartysOpe[T_MODGCVD.ICli].CasPostal.Trim() + Convert.ToChar(9);
            s = s + VB6Helpers.Format(VB6Helpers.CStr(Module1.PartysOpe[T_MODGCVD.ICli].Enviara)).Trim() + Convert.ToChar(9);

            //------------------------------------------------------------------------
            //Compra, Venta o Ambos.-
            //------------------------------------------------------------------------
            s = s + VB6Helpers.Str(2) + Convert.ToChar(9);

            //------------------------------------------------------------------------
            //Seguro, Flete y Gastos del Cedente.-
            //------------------------------------------------------------------------
            s = s + MODGUSR.UsrEsp.Nombre.Trim() + Convert.ToChar(9);  //Datos del Especialista.-
            s = s + MODGUSR.UsrEsp.Direccion.Trim() + Convert.ToChar(9);
            s = s + MODGUSR.UsrEsp.Telefono.Trim() + Convert.ToChar(9);
            s = s + MODGUSR.UsrEsp.Fax.Trim() + Convert.ToChar(9);

            //------------------------------------------------------------------------
            //Concepto de la Compra Venta.-
            //------------------------------------------------------------------------
            //Select Case CVD.Operacion
            //
            //    Case FLT: s$ = s$ + "Flete en Chile" + Chr$(9)
            //
            //    Case SEG: s$ = s$ + "Seguro en Chile" + Chr$(9)
            //
            //    Case FLTSEG: s$ = s$ + "Flete y Seguro en Chile" + Chr$(9)
            //
            //    Case ENDREC: s$ = s$ + "Endoso Recibido" + Chr$(9)
            //
            //    Case Else: s$ = s$ + "" + Chr$(9)
            //
            //End Select

            s = s + "" + Convert.ToChar(9);  //Concepto vacio

            //------------------------------------------------------------------------
            //operaciones de compra y venta realizadas.-
            //------------------------------------------------------------------------
            //s$ = s$ + Str$(UBound(VgFyS) + 1) + Chr$(9)
            //For i% = 0 To UBound(VgFyS)
            //    s$ = s$ + Str$(VgFyS(i%).EleTip + 1) + Chr$(9)
            //    s$ = s$ + Get_NemMnd(VgFyS(i%).CodMon(1)) + Chr$(9)
            //    s$ = s$ + Str$(VgFyS(i%).Monto(1) + VgFyS(i%).Monto(2)) + Chr$(9)
            //    s$ = s$ + Str$(VgFyS(i%).TipCam(1)) + Chr$(9)
            //    s$ = s$ + Str$(VgFyS(i%).MtoPss(1) + VgFyS(i%).MtoPss(2)) + Chr$(9)
            //    mevd$ = Str$(VgFyS(i%).TotPss)
            //Next i%

            if (VB6Helpers.UBound(MODPREEM.Vx_PReem) >= 0)
            {
                for (i = 0; i <= (short)(VB6Helpers.UBound(MODPREEM.Vx_PReem) - 1); i++)
                {
                    primero = MODPREEM.Vx_PReem[i].IndAnu;
                    for (j = (short)(i + 1); j <= (short)VB6Helpers.UBound(MODPREEM.Vx_PReem); j++)
                    {
                        comparado = MODPREEM.Vx_PReem[j].IndAnu;
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'primero'. Consider using the GetDefaultMember6 helper method.
                        // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'comparado'. Consider using the GetDefaultMember6 helper method.
                        if (Format.StringToDouble(primero) == 3 && (Format.StringToDouble(comparado) == 0 || Format.StringToDouble(comparado) == 2))
                        {
                            Reem_Aux = MODPREEM.Vx_PReem[j];
                            MODPREEM.Vx_PReem[j] = MODPREEM.Vx_PReem[i];
                            MODPREEM.Vx_PReem[i] = Reem_Aux;
                        }

                    }

                }

            }

            contpos = 0;
            conttran = 0;
            for (i = 0; i <= (short)VB6Helpers.UBound(MODPREEM.Vx_PReem); i++)
            {
                if (MODPREEM.Vx_PReem[i].IndAnu == 0 || MODPREEM.Vx_PReem[i].IndAnu == 2)
                {
                    contpos = Format.StringToDouble(contpos) + 1;
                }
                else if (MODPREEM.Vx_PReem[i].IndAnu == 3)
                {
                    conttran = (short)(conttran + 1);
                }

            }

            s = s + VB6Helpers.Str(contpos) + Convert.ToChar(9);
            for (i = 0; i <= (short)VB6Helpers.UBound(MODPREEM.Vx_PReem); i++)
            {
                if (MODPREEM.Vx_PReem[i].IndAnu == 0 || MODPREEM.Vx_PReem[i].IndAnu == 2)
                {
                    // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 's' variable as a StringBuilder6 object.
                    s = s + VB6Helpers.Str(1) + Convert.ToChar(9);
                    s = s + BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_NemMnd(MODGTAB0, unit, MODPREEM.Vx_PReem[i].CodMon) + Convert.ToChar(9);
                    s = s + VB6Helpers.Str(MODPREEM.Vx_PReem[i].TotOri) + Convert.ToChar(9);
                    s = s + VB6Helpers.Str(MODPREEM.Vx_PReem[i].TipCamo) + Convert.ToChar(9);
                    mtopss = MODPREEM.Vx_PReem[i].TotOri * MODPREEM.Vx_PReem[i].TipCamo;
                    s = s + VB6Helpers.Str(mtopss) + Convert.ToChar(9);
                    s = s + VB6Helpers.Str(MODPREEM.Vx_PReem[i].NumPla) + Convert.ToChar(9);
                    s = s + MODPREEM.Vx_PReem[i].numdec + Convert.ToChar(9);
                    MtoTotPss += mtopss;
                    mevd = VB6Helpers.Str(MtoTotPss);
                }

            }

            s = s + VB6Helpers.Str(conttran) + Convert.ToChar(9);
            for (i = 0; i <= (short)VB6Helpers.UBound(MODPREEM.Vx_PReem); i++)
            {
                if (MODPREEM.Vx_PReem[i].IndAnu == 3)
                {
                    s = s + BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_NemMnd(MODGTAB0, unit, MODPREEM.Vx_PReem[i].CodMon) + Convert.ToChar(9);
                    s = s + VB6Helpers.Str(MODPREEM.Vx_PReem[i].TotOri) + Convert.ToChar(9);
                    s = s + VB6Helpers.Str(MODPREEM.Vx_PReem[i].NumPla) + Convert.ToChar(9);
                    s = s + MODPREEM.Vx_PReem[i].numdec + Convert.ToChar(9);
                }

            }

            //------------------------------------------------------------------------
            //Número de Beneficiarios.-
            //------------------------------------------------------------------------

            s += Mdl_Funciones.Det_Vias(initObject, unit);  //Se agrega el detalle de las vias

            //------------------------------------------------------------------------
            //Número de Débitos.-
            //------------------------------------------------------------------------
            z = -1;

            z = (short)VB6Helpers.UBound(MODXORI.VxOri);

            if (z >= 0)
            {
                Contador3 = 0;
                for (i = 0; i <= (short)z; i++)
                {
                    if (MODXORI.VxOri[i].Status != T_MODGCVD.EstadoEli)
                    {
                        Contador3 = (short)(Contador3 + 1);
                    }

                }

                s = s + Contador3.ToString() + Convert.ToChar(9);
                for (i = 0; i <= (short)z; i++)
                {
                    if (MODXORI.VxOri[i].Status != T_MODGCVD.EstadoEli)
                    {
                        s = s + "Débito" + Convert.ToChar(9);  //Ver el caso de los abonos
                        if (string.IsNullOrWhiteSpace(MODXORI.VxOri[i].CtaCte_t))
                        {
                            s = s + VB6Helpers.Trim(MODXORI.VxOri[i].NomOri) + Convert.ToChar(9);
                        }
                        else
                        {
                            s = s + VB6Helpers.Trim(MODXORI.VxOri[i].NomOri) + " " + VB6Helpers.Trim(MODXORI.VxOri[i].CtaCte_t) + Convert.ToChar(9);
                        }

                        s = s + VB6Helpers.Trim(MODXORI.VxOri[i].NemMon) + Convert.ToChar(9);
                        if (string.IsNullOrEmpty(MODXORI.VxOri[i].CtaCte_t))
                        {
                            s = s + VB6Helpers.Trim(VB6Helpers.Str(MODXORI.VxOri[i].MtoTot)) + Convert.ToChar(9);
                        }
                        else
                        {
                            if (VB6Helpers.Trim(MODXORI.VxOri[i].NemMon) == "$")
                            {
                                s = s + VB6Helpers.Trim(VB6Helpers.Str((MODXORI.VxOri[i].MtoTot) + (MODGSCE.VGen.MtoDeb))) + Convert.ToChar(9);
                            }
                            else
                            {
                                s = s + VB6Helpers.Trim(VB6Helpers.Str(MODXORI.VxOri[i].MtoTot)) + Convert.ToChar(9);
                            }

                        }

                    }

                }

            }
            else
            {
                s = s + VB6Helpers.Str(-1) + Convert.ToChar(9);
            }

            //------------------------------------------------------------------------
            //Número de Detalles.-
            //------------------------------------------------------------------------
            if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 0)
            {
                Contador3 = 0;

                for (i = 0; i <= (short)VB6Helpers.UBound(Mdl_Funciones_Varias.V_gCom); i++)
                {
                    if (Mdl_Funciones_Varias.V_gCom[i].estado != T_MODGCVD.EstadoEli && Mdl_Funciones_Varias.V_gCom[i].MtoComp > 0)
                    {
                        Contador3 = (short)(Contador3 + 1);
                        if (Mdl_Funciones_Varias.V_gCom[i].MtoIvap > 0)
                        {
                            hayiva = (short)(true ? -1 : 0);
                        }
                    }

                }

                if (!string.IsNullOrEmpty(MODGCVD.VgCvd.AvisoDC) && VB6Helpers.Instr(MODGCVD.VgCvd.AvisoDC, "N") > 0)
                {
                    Contador3 = (short)(Contador3 + 1);
                }

                if ((hayiva & (T_MODGMTA.impflag == 1 ? -1 : 0)) != 0)
                {
                    Contador3 = (short)(Contador3 + 1);  // validación para los espacios al generar cartas
                }
                if (Contador3 > 0)
                {
                    s = s + VB6Helpers.Trim(VB6Helpers.Str(Contador3 + 1)) + Convert.ToChar(9);  //Se suma uno por el total de mnd. Ext. Vendida
                }
                else
                {
                    s = s + VB6Helpers.Trim(VB6Helpers.Str(Contador3)) + Convert.ToChar(9);
                }

                //------------------------------------------------
                //Datos correspondientes a las comisiones cobradas
                //------------------------------------------------
                if (Contador3 > 0)
                {
                    for (i = 0; i <= (short)VB6Helpers.UBound(Mdl_Funciones_Varias.V_gCom); i++)
                    {
                        if (Mdl_Funciones_Varias.V_gCom[i].estado != T_MODGCVD.EstadoEli && Mdl_Funciones_Varias.V_gCom[i].MtoComp > 0)
                        {
                            s = s + Mdl_Funciones_Varias.V_gCom[i].GlsCom + Convert.ToChar(9);
                            s = s + "$" + Convert.ToChar(9);
                            s = s + VB6Helpers.Trim(VB6Helpers.Str(Mdl_Funciones_Varias.V_gCom[i].MtoComp)) + Convert.ToChar(9);
                            SumaIVA += Mdl_Funciones_Varias.V_gCom[i].MtoIvap;
                            suma += Mdl_Funciones_Varias.V_gCom[i].MtoTotp;
                        }

                    }

                    if (hayiva != 0)
                    {
                        iva = Mdl_Acceso.GetConfigValue("FundTransfer.General.MontoIVA");
                        s = s + "Impuesto IVA del " + iva + "%. " + Convert.ToChar(9);
                        s = s + "$" + Convert.ToChar(9);
                        s = s + VB6Helpers.Str(SumaIVA) + Convert.ToChar(9);
                    }

                    DebCtaCte = (short)(false ? -1 : 0);
                    if (!string.IsNullOrEmpty(MODGCVD.VgCvd.AvisoDC) && VB6Helpers.Instr(MODGCVD.VgCvd.AvisoDC, "N") > 0)
                    {
                        DebCtaCte = (short)(true ? -1 : 0);
                    }

                    if ((DebCtaCte & (T_MODGMTA.impflag == 1 ? -1 : 0)) != 0)
                    {
                        s = s + "Impuesto Fijo por débito en Cuenta Corriente." + Convert.ToChar(9);
                        s = s + "$" + Convert.ToChar(9);
                        s = s + VB6Helpers.Str(MODGSCE.VGen.MtoDeb) + Convert.ToChar(9);
                    }

                    s = s + "Monto en pesos de la moneda extranjera vendida" + Convert.ToChar(9);
                    s = s + "$" + Convert.ToChar(9);
                    s = s + mevd + Convert.ToChar(9);
                }

            }

            Usr = MODGUSR.UsrEsp.CentroCosto + MODGUSR.UsrEsp.Especialista;

            MODGCVD.VgCvd.DocCvdI = SyPut_xDoc(initObject, unit, NumOpe, T_Mdl_Funciones_Varias.DocCvdI, s, Usr);
            //------------------------------------------------------------------------
            return (short)(true ? -1 : 0);
        }

        public static dynamic TraspasoComision(InitializationObject Modulos)
        {

            short Com = (short)VB6Helpers.UBound(Modulos.Mdl_Funciones_Varias.V_gCom);
            short i = 0;

            for (i = 1; i <= (short)Com; i++)
            {
                VB6Helpers.RedimPreserve(ref Modulos.MODXVIA.VxVia, 0, i);
                Modulos.MODXVIA.VxVia[i].CodMon = BuscaMoneda(Modulos, Modulos.Mdl_Funciones_Varias.V_gCom[i].NemMnd);
                Modulos.MODXVIA.VxVia[i].NemCta = Modulos.Mdl_Funciones_Varias.V_gCom[i].NemCta;
                Modulos.MODXVIA.VxVia[i].MtoTot = Modulos.Mdl_Funciones_Varias.V_gCom[i].MtoCom;
                Modulos.MODXVIA.VxVia[i].NumCta = Modulos.MODXORI.VOvd[i].NumCta;
                Modulos.MODXVIA.VxVia[i].NemMon = Modulos.Mdl_Funciones_Varias.V_gCom[i].NemMnd;
            }
            return null;
        }

        public static short BuscaMoneda(InitializationObject Modulos, string Moneda)
        {
            short _retValue = 0;
            short i = 0;
            _retValue = 0;
            for (i = 1; i <= (short)VB6Helpers.UBound(Modulos.MODGTAB0.VMnd); i++)
            {
                if (Modulos.MODGTAB0.VMnd[i].Mnd_MndNmc == Moneda)
                {
                    return Modulos.MODGTAB0.VMnd[i].Mnd_MndCod;
                }

            }

            return _retValue;
        }

        public static short Guarda_Oper_CodTran(InitializationObject initObject, UnitOfWorkCext01 unit, string OpeSin)
        {
            T_MODXORI MODXORI = initObject.MODXORI;
            T_MODGCON0 MODGCON0 = initObject.MODGCON0;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;

            short _retValue = 0;
            bool sigue = false;
            short i = 0;
            short o = 0;

            _retValue = (short)(true ? -1 : 0);
            for (i = 0; i <= (short)VB6Helpers.UBound(MODXORI.Vx_SCodTran); i++)
            {
                if (MODXORI.Vx_SCodTran[i].nro_trx > 0 && MODXORI.Vx_SCodTran[i].Estado == 1)
                {
                    List<string> parameters = new List<string>();
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 1, 3)));
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 4, 2)));
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 6, 2)));
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 8, 3)));
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 11, 5)));
                    parameters.Add(VB6Helpers.CStr(MODGCON0.VMch.NroRpt));
                    parameters.Add(VB6Helpers.Format(MODGCON0.VMch.fecmov, "yyyy-mm-dd"));
                    parameters.Add(VB6Helpers.Str(MODXORI.Vx_SCodTran[i].nro_trx));

                    _retValue = (short)(false ? -1 : 0);


                    // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 'ls_sql' variable as a StringBuilder6 object.
                    //ls_sql += " exec pro_sce_codtran_i01_MS ";


                    if (VB6Helpers.Trim(MODXORI.Vx_SCodTran[i].Via) == "des" || VB6Helpers.Trim(MODXORI.Vx_SCodTran[i].Via) == "vue")
                    {
                        for (o = 0; o <= (short)VB6Helpers.UBound(MODXVIA.VxVia); o++)
                        {
                            if (MODXORI.Vx_SCodTran[i].ID == MODXVIA.VxVia[o].IdCtran)
                            {
                                parameters.Add("H");
                                parameters.Add(MODXVIA.VxVia[o].Text1);//DATA 1  BENEFICIARIO
                                parameters.Add(MODXVIA.VxVia[o].Text2); //DATA 2  CUENTA N
                                parameters.Add(MODXVIA.VxVia[o].Text3);//DATA 3  CON
                                parameters.Add(MODXVIA.VxVia[o].Text4);//DATA 4  CUENTA
                                parameters.Add(MODXVIA.VxVia[o].Text5);//DATA 5  CONTRACT REFERENCE NUMBER
                                parameters.Add(MODXVIA.VxVia[o].nroimp.ToString());

                                break;
                            }

                        }

                    }
                    else if (VB6Helpers.Trim(MODXORI.Vx_SCodTran[i].Via) == "ori")
                    {
                        for (o = 0; o <= (short)VB6Helpers.UBound(MODXORI.VxOri); o++)
                        {
                            if (MODXORI.Vx_SCodTran[i].ID == MODXORI.VxOri[o].IdCtran)
                            {
                                parameters.Add("D");
                                parameters.Add(MODXORI.VxOri[o].Text1);//DATA 6  POR ORDEN
                                parameters.Add(MODXORI.VxOri[o].Text2); //DATA 7  DETALLE 1
                                parameters.Add(MODXORI.VxOri[o].Text3);//DATA 8  DETALLE 2
                                parameters.Add(MODXORI.VxOri[o].Text4); //DATA 9  DETALLE 3
                                parameters.Add(MODXORI.VxOri[o].Text5); //DATA 10 CONTRACT REFERENCE
                                parameters.Add(MODXORI.VxOri[o].nroimp.ToString());
                            }

                        }

                    }

                    sigue = true;

                    while (sigue)
                    {
                        string lc_retorno = String.Empty;
                        string lc_mensaje = String.Empty;
                        int cant = unit.SceRepository.pro_sce_codtran_i01_MS(ref lc_retorno, ref lc_mensaje, parameters.ToArray());
                        if (!lc_retorno.Equals("E00"))
                        {
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Se ha producido un error en la consulta a la Base de datos"
                            });
                            _retValue = -1;
                            sigue = false;
                        }
                        else
                        {
                            if (cant == 0)
                            {
                                _retValue = 0;
                            }
                            else
                            {
                                //si inserta los valores deberia salir del ciclo con _retValue en -1 (true)
                                sigue = false;
                                _retValue = -1;
                            }
                        }
                        //if (!lc_mensaje.Equals("E00"))
                        //{
                        //    Mdi_Principal.MESSAGES.Add(new UI_Message()
                        //    {
                        //        Type = TipoMensaje.Error,
                        //        Text = "Se ha producido un error en la consulta a la Base de datos"
                        //    });
                        //    _retValue = -1;
                        //    sigue = false;
                        //}
                        //else
                        //{
                        //    if (cant == 0)
                        //    {
                        //        sigue = false;
                        //        _retValue = 0;
                        //    }
                        //    else
                        //    {
                        //        _retValue = -1;
                        //    }
                        //}
                    }

                }
                else
                {
                    _retValue = (short)(true ? -1 : 0);
                }

            }

            return _retValue;
        }

        public static short Guarda_Inf_Adic_CVD(InitializationObject initObject, UnitOfWorkCext01 unit, string OpeSin)
        {
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;

            short _retValue = 0;

            short Li_Largo1 = 0;
            short Li_Largo2 = 0;
            short APartirDe = 0;
            short num_registros_msg = 0;

            bool sigue = false;
            string ls_ret = "";
            string ls_sql = "";
            string ls_retorno_venta = "";
            string ls_mensaje = "";
            short i = 0;
            short conta = 0;

            if (MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_CVD)
            {
                conta = (short)VB6Helpers.UBound(MODGCVD.VgPli);
            }
            else
            {
                conta = 0;
            }

            for (i = 0; i <= (short)conta; i++)
            {
                //Si se elimino la partida (planilla) no se debe guardar
                if (MODGCVD.VgPli.Length == 0 || MODGCVD.VgPli[i].Status != T_MODGCVD.EstadoEli)
                {
                    List<string> parameters = new List<string>();

                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 1, 3)));  //CENTRO COSTO
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 4, 2)));  //CODIGO PRODUCTO
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 6, 2)));  //ESPECIALISTA
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 8, 3)));  //OFICINA
                    parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 11, 5)));  //CODIGO OPERACION
                    if (MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_CVD)
                    {
                        parameters.Add(MODGCVD.VgPli[i].TipCVD);  //Tipo de Operacion
                    }
                    else
                    {
                        parameters.Add("AV");  //Para el resto
                    }

                    parameters.Add("false");  //Variable Inyeccion
                    string lc_retorno = String.Empty;
                    string lc_mensaje = String.Empty;
                    int cant = unit.SceRepository.pro_sce_cvd_ft_i01_MS(ref lc_retorno, ref lc_mensaje, parameters.ToArray());

                    APartirDe = 1;
                    sigue = true;

                    if (!lc_retorno.Equals("E00"))
                    {
                        Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Se ha producido un error en la consulta a la Base de datos"
                        });
                        //Se ocurre error al ingresar los datos en cvd_ft no debe dejar continuar con la grabación
                        _retValue = 0;
                        sigue = false;
                    }
                    else
                    {
                        if (cant == 0)
                        {
                            _retValue = 0;
                            sigue = false;
                        }
                        else
                        {
                            _retValue = -1;
                        }
                    }
                }
            }


            return _retValue;
        }

        public static short Guarda_Oper_Relacion(InitializationObject initObject, UnitOfWorkCext01 unit, string OpeSin)
        {
            using (var tracer = new Tracer("Guarda_Oper_Relacion"))
            {
                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;
                T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;
                T_MODXVIA MODXVIA = initObject.MODXVIA;
                T_MODXORI MODXORI = initObject.MODXORI;
                T_MODGCON0 MODGCON0 = initObject.MODGCON0;

                short _retValue = 0,
                    Vias = 0,
                    origen = 0,
                    i = 0;

                string trx_id = "00",
                    mensajeError = string.Empty;

                if (String.IsNullOrEmpty(Mdl_Funciones_Varias.LC_MONTO))
                {
                    Mdl_Funciones_Varias.LC_MONTO = "0";
                }

                /// Validamos si la Operación cuenta con identificador de transacción
                if (string.IsNullOrEmpty(Mdl_Funciones_Varias.LC_TRXID_MAN))
                {
                    tracer.TraceError("Alerta", "La OP." + initObject.MODGCVD.VgCvd.OpeSin + " no genero TRXID");
                    Mdl_Funciones_Varias.LC_TRXID_MAN = MODGCVD.GeneraTRXID(initObject.MODGCVD.VgCvd.OpeSin, unit, initObject.Mdi_Principal.MESSAGES);
                    tracer.TraceInformation("Nuevo TRXID: ", Mdl_Funciones_Varias.LC_TRXID_MAN);
                }

                Vias = (short)VB6Helpers.UBound(MODXVIA.VxVia);
                origen = (short)VB6Helpers.UBound(MODXORI.VxOri);

                _retValue = (short)(true ? -1 : 0);

                for (i = 0; i <= (short)Vias; i++)
                {
                    if (MODXVIA.VxVia[i].Status != 3)
                    {

                        short _switchVar1 = MODXVIA.VxVia[i].NumCta;

                        if (_switchVar1 == T_MODGCON0.IdCta_CtaCteAUTN || _switchVar1 == T_MODGCON0.IdCta_CtaCteAUTE || _switchVar1 == T_MODGCON0.IdCta_CtaCteMANN || _switchVar1 == T_MODGCON0.IdCta_CtaCteMANE || _switchVar1 == T_MODGCON0.IdCta_GAPMN || _switchVar1 == T_MODGCON0.IdCta_GAPME || _switchVar1 == T_MODGCON0.IdCta_CtaCteMN || _switchVar1 == T_MODGCON0.IdCta_CtaCteME)
                        {
                            List<string> parameters = new List<string>();
                            parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 1, 3)));  //CENTRO COSTO
                            parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 4, 2)));  //CODIGO PRODUCTO
                            parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 6, 2)));  //ESPECIALISTA
                            parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 8, 3)));  //OFICINA
                            parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 11, 5)));  //CODIGO OPERACION
                            parameters.Add(Mdl_Funciones_Varias.LC_XREF);

                            if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
                            {
                                parameters.Add(Mdl_Funciones_Varias.LC_COD_TRANS);  //CODIGO TRANSACCION
                                parameters.Add(VB6Helpers.CStr(Mdl_Funciones_Varias.LC_MONEDA));
                                parameters.Add((Mdl_Funciones_Varias.LC_MONTO));
                            }
                            else
                            {
                                parameters.Add(Mdl_Funciones_Varias.LC_TRXID_MAN + trx_id);  //CODIGO TRANSACCION
                                parameters.Add(VB6Helpers.CStr(MODXVIA.VxVia[i].CodMon));
                                parameters.Add(Format.FormatCurrency(MODXVIA.VxVia[i].MtoTot, "#.#0"));
                            }

                            parameters.Add(VB6Helpers.CStr(MODXVIA.VxVia[i].nroimp));
                            parameters.Add(VB6Helpers.CStr(Mdl_Funciones_Varias.CARGA_AUTOMATICA));
                            parameters.Add(MODGSYB.dbdatesy(MODGCON0.VMch.fecmov));//FECHA DE INGRESO
                            parameters.Add(VB6Helpers.Format(MODGCON0.VMch.fecmov, "yyyymmdd"));//FECHA DE INGRESO


                            if (MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CtaCteMN || MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CtaCteME)
                            {
                                //parameters.Add(VB6Helpers.Mid(Mdl_Funciones_Varias.LC_TRXID_MAN, 16, 10));
                                //Para operaciones producto 20, el contract reference debe ser el nro de reporte, para que no duplique las inyecciones si se inyecta desde legacy
                                parameters.Add(MODGCON0.VMch.NroRpt.ToString());// Contract reference number
                            }
                            else
                            {
                                parameters.Add(MODXVIA.VxVia[i].Text5 != "" ? MODXVIA.VxVia[i].Text5 : "0");//CONTRACT REFERENCE NUMBER
                            }
                            string lc_codigo = String.Empty;
                            string lc_mensaje = String.Empty;

                            int cant = unit.SceRepository.pro_sce_relacion_i01_MS(ref lc_codigo, ref lc_mensaje, parameters.ToArray());
                            //Se agrega Trx_Id_Rev para manejo de reversas de Ctas. Ctes.

                            //VB6Helpers.MsgBox("Se ha producido un error al tratar de Grabar datos de la operación. El Servidor reporta : [" + VB6Helpers.Left(VB6Helpers.Trim(Mdl_SRM.ParamSrm8k.mensaje.Value), 100) + "]. Reporte este problema.", MsgBoxStyle.Information, "Validación de Datos");

                            trx_id = VB6Helpers.Format(VB6Helpers.Str(VB6Helpers.Int(trx_id) + 1), "00");
                        }
                    }
                }

                for (i = 0; i <= (short)origen; i++)
                {
                    if (MODXORI.VxOri[i].Status != 3)
                    {
                        short _switchVar2 = MODXORI.VxOri[i].NumCta;

                        if (_switchVar2 == T_MODGCON0.IdCta_CtaCteAUTN || _switchVar2 == T_MODGCON0.IdCta_CtaCteAUTE || _switchVar2 == T_MODGCON0.IdCta_CtaCteMANN || _switchVar2 == T_MODGCON0.IdCta_CtaCteMANE || _switchVar2 == T_MODGCON0.IdCta_GAPMN || _switchVar2 == T_MODGCON0.IdCta_GAPME || _switchVar2 == T_MODGCON0.IdCta_CtaCteMN || _switchVar2 == T_MODGCON0.IdCta_CtaCteME)
                        {
                            List<string> parameters = new List<string>();
                            parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 1, 3)));  //CENTRO COSTO
                            parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 4, 2)));  //CODIGO PRODUCTO
                            parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 6, 2)));  //ESPECIALISTA
                            parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 8, 3)));  //OFICINA
                            parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 11, 5)));  //CODIGO OPERACION
                            parameters.Add(Mdl_Funciones_Varias.LC_XREF);
                            if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
                            {
                                parameters.Add(Mdl_Funciones_Varias.LC_COD_TRANS);  //CODIGO TRANSACCION
                                parameters.Add(VB6Helpers.CStr(Mdl_Funciones_Varias.LC_MONEDA));
                                parameters.Add(Mdl_Funciones_Varias.LC_MONTO);
                            }
                            else
                            {
                                parameters.Add(Mdl_Funciones_Varias.LC_TRXID_MAN + trx_id);  //CODIGO TRANSACCION
                                                                                             //Hay que ingresar moneda manual
                                parameters.Add(VB6Helpers.CStr(MODXORI.VxOri[i].CodMon));
                                parameters.Add(MODGSYB.dbmontoSyForRead(MODXORI.VxOri[i].MtoTot));
                                //Hay que ingresar monto manual
                            }


                            parameters.Add(VB6Helpers.CStr(MODXORI.VxOri[i].nroimp));
                            parameters.Add(VB6Helpers.CStr(Mdl_Funciones_Varias.CARGA_AUTOMATICA));
                            parameters.Add(MODGSYB.dbdatesy(MODGCON0.VMch.fecmov));  //FECHA DE INGRESO
                            parameters.Add(DateTime.Parse(MODGCON0.VMch.fecmov).ToString("yyyyMMdd"));  //FECHA DE INGRESO

                            if (MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteMN || MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteME)
                            {
                                //parameters.Add(VB6Helpers.Mid(Mdl_Funciones_Varias.LC_TRXID_MAN, 16, 10));  // Contract reference number
                                //Para operaciones producto 20, el contract reference debe ser el nro de reporte, para que no duplique las inyecciones si se inyecta desde legacy
                                parameters.Add(MODGCON0.VMch.NroRpt.ToString());// Contract reference number
                            }
                            else
                            {
                                parameters.Add(MODXORI.VxOri[i].Text5);  //CONTRACT REFERENCE NUMBER
                            }

                            string lc_codigo = String.Empty;
                            string lc_mensaje = String.Empty;

                            int cant = unit.SceRepository.pro_sce_relacion_i01_MS(ref lc_codigo, ref lc_mensaje, parameters.ToArray());

                            tracer.TraceInformation("pro_sce_relacion_i01_MS: ", lc_codigo, lc_mensaje, parameters, cant);

                            if (lc_codigo != "E00")
                            {
                                mensajeError = "Se ha producido un error al tratar de Grabar datos de la operación. Reporte este problema.";
                                Mdi_Principal.MESSAGES.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Error,
                                    Text = mensajeError
                                });
                                tracer.TraceError("Alerta", mensajeError);
                                _retValue = (short)(false ? -1 : 0);
                            }

                            trx_id = VB6Helpers.Format(VB6Helpers.Str(VB6Helpers.Int(trx_id) + 1), "00");
                        }
                    }

                }

                return _retValue;
            }
        }

        public static void Cmd_Init(T_Mdl_Funciones_Varias Mdl_Funciones_Varias)
        {
            Mdl_Funciones_Varias.CmdsQuery = new T_Cmd[0];
        }

        //Ingresa un Comando al Arreglo.-        
        public static short Cmd_Put(T_Mdl_Funciones_Varias Mdl_Funciones_Varias, dynamic Comando, string Tabla, short Quiebre)
        {
            short _retValue = 0;
            short n = 0;

            //Dimensiona nuevamente el Arreglo.-

            n = (short)VB6Helpers.UBound(Mdl_Funciones_Varias.CmdsQuery);

            //Ingresa el Comando.-
            n = (short)(VB6Helpers.UBound(Mdl_Funciones_Varias.CmdsQuery) + 1);
            VB6Helpers.RedimPreserve(ref Mdl_Funciones_Varias.CmdsQuery, 0, n);
            Mdl_Funciones_Varias.CmdsQuery[n].Cmd = "exec @r = " + VB6Helpers.Right(VB6Helpers.CStr(Comando), VB6Helpers.Len(VB6Helpers.CStr(Comando)) - 4);
            Mdl_Funciones_Varias.CmdsQuery[n].Tab = Tabla;
            Mdl_Funciones_Varias.CmdsQuery[n].Brk = Quiebre;
            _retValue = (short)(true ? -1 : 0);

            return _retValue;
        }

        public static short Cmd_Put_New(T_Mdl_Funciones_Varias Mdl_Funciones_Varias, Func<short> command)
        {
            using (var trace = new Tracer("Cmd_Put_New"))
            {
                try
                {
                    Mdl_Funciones_Varias.CmdsQuerysNew.Add(command);
                    return -1;
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta",e);
                    return 0;
                }
            }
        }

        //Retorna el Comando Final.-
        public static string Cmd_Build(T_Mdl_Funciones_Varias Mdl_Funciones_Varias)
        {
            short ultcmd = -1;
            string Que = "";
            short i = 0;

            ultcmd = (short)VB6Helpers.UBound(Mdl_Funciones_Varias.CmdsQuery);

            Que = "";
            if (ultcmd < 1)
            {
                return Que;
            }
            else
            {
                Que = Que + "declare @r smallint, @rr smallint " + VB6Helpers.Chr(13);
                Que = Que + "select @rr = 0 " + VB6Helpers.Chr(13);
                //Que$ = Que$ + "BEGIN TRAN " + Chr$(13)  //Comentado Akzio Migracion Mayo 2014
                for (i = 0; i <= (short)ultcmd; i++)
                {
                    if (i > 1)
                    {
                        // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 'Que' variable as a StringBuilder6 object.
                        Que += "if @r = 0 begin ";
                    }
                    Que = Que + Mdl_Funciones_Varias.CmdsQuery[i].Cmd + VB6Helpers.Chr(13);
                    if (Mdl_Funciones_Varias.CmdsQuery[i].Brk != 0)
                    {
                        Que = Que + "if @r <> 0 select @rr = " + VB6Helpers.Str(i) + VB6Helpers.Chr(13);
                    }

                    if (i > 1)
                    {
                        Que = Que + "end" + VB6Helpers.Chr(13);
                    }
                }

                Que = Que + "if @r = 0 begin if @@trancount > 0 COMMIT TRAN end else begin  if @@trancount > 0 ROLLBACK TRAN end" + VB6Helpers.Chr(13);
                Que = Que + "select @rr " + VB6Helpers.Chr(13);
                return Que;
            }

        }

        public static string llena_ceros(string Text, decimal Num)
        {
            short faltan = 0;
            string llena_cero = "";
            short i = 0;

            if ((VB6Helpers.Len(Text) <= Num))
            {
                faltan = (short)(Num - VB6Helpers.Len(Text));
                llena_cero = Text;
                for (i = 1; i <= (short)faltan; i++)
                {
                    llena_cero = "0" + llena_cero;
                }

                return llena_cero;
            }
            else
            {
                return VB6Helpers.String((int)Num, "*");
            }
        }

        /// <summary>
        /// Rescata_descripcion_parametros
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="Cod_parametro"></param>
        /// <param name="Moneda"></param>
        /// <returns></returns>
        public static string Rescata_descripcion_parametros(UnitOfWorkCext01 uow, string Cod_parametro, short Moneda)
        {
            using (var trace = new Tracer())
            {
                dynamic _retValue = null;
                try
                {
                    _retValue = uow.SceRepository.pro_sce_parametros_s01_MS(Cod_parametro, Moneda);
                }
                catch (Exception ex)
                {
                    trace.TraceException("Alerta", ex);
                    throw;
                }
                return _retValue;
            }
        }

        /// <summary>
        /// Busca el nombre del Party; 1º en memoria; 2º a disco.-
        /// </summary>
        /// <param name="PrtImp"></param>
        /// <param name="IndNom"></param>
        /// <param name="IndDir"></param>
        /// <param name="NomDir"></param>
        /// <param name="Tipo"></param>
        /// <returns></returns>
        public static string GetDatPrtn(UnitOfWorkCext01 uow, string PrtImp, short IndNom, short IndDir, string NomDir, string Tipo)
        {
            string _retValue = "";
            string p = "";
            short n = 0;
            string s1 = "";
            string s2 = "";
            if (string.IsNullOrEmpty(PrtImp))
            {
                return _retValue;
            }
            p = PrtImp;
            p = PoneMarcaParty(PrtImp);
            if (IndNom != -1)
            {
                s1 = VB6Helpers.Trim(SyGet_Rsa(uow, p, IndNom));
            }
            if (IndDir != -1)
            {
                s2 = VB6Helpers.Trim(SyGet_Dadn(uow, p, IndDir, Tipo));
            }

            n = 1;
            VB6Helpers.RedimPreserve(ref DatPrtys, 0, n);
            DatPrtys[n].PrtImp = p;
            DatPrtys[n].IndNom = IndNom;
            DatPrtys[n].IndDir = IndDir;
            DatPrtys[n].NomPrty = s1;
            DatPrtys[n].DirPrty = s2;

            if (NomDir == "N")
            {
                _retValue = s1;
            }
            if (NomDir == "D")
            {
                return s2;
            }

            return _retValue;
        }

        /// <summary>
        /// lee datos de la tabla de participantes
        /// Obtiene la direccion de participante
        /// </summary>
        /// <param name="IdParty"></param>
        /// <param name="IdDireccion"></param>
        /// <param name="Tipo"></param>
        /// <returns></returns>
        public static string SyGet_Dadn(UnitOfWorkCext01 uow, string IdParty, short IdDireccion, string Tipo)
        {
            using (var trace = new Tracer())
            {
                string _retValue = string.Empty;

                try
                {
                    _retValue = uow.SceRepository.sce_dad_s04_MS(MODGSYB.dbcharSy(IdParty), IdDireccion, MODGSYB.dbcharSy(Tipo));
                }
                catch (Exception ex)
                {
                    trace.TraceException("Alerta", ex);
                    throw;
                }
                return _retValue;
            }
        }

        public static short Cmd_Execute(T_Mdl_Funciones_Varias Mdl_Funciones_Varias, UnitOfWorkCext01 unit)
        {
            using (var tracer = new Tracer())
            {
                short r = 0, cont = 0;
                short err = 999;
                string MethodEX = string.Empty, TargetEX = string.Empty;
                try
                {
                    unit.BeginTransaction();
                    //comienza transaccion
                    for (short i = 0; i < Mdl_Funciones_Varias.CmdsQuerysNew.Count; i++)
                    {
                        cont = i;
                        MethodEX = Mdl_Funciones_Varias.CmdsQuerysNew.ElementAt(cont).GetInvocationList()[0].Method.ToString();
                        TargetEX = Mdl_Funciones_Varias.CmdsQuerysNew.ElementAt(cont).GetInvocationList()[0].Target.ToString();
                        r = Mdl_Funciones_Varias.CmdsQuerysNew.ElementAt(i)();
                        if (r != 0)
                        {
                            //rollback
                            err = i;
                            tracer.AddToContext("Transacción SyBase en Cmd_Execute", "Alerta en Cmd_Execute, en la funcion de indice: " + cont + " Metodo: " + MethodEX + " Target: " + TargetEX);
                            throw new Exception("Alerta en Cmd_Execute, en la funcion de indice: " + i + " en la Query: " + Mdl_Funciones_Varias.CmdsQuerysNew.ElementAt(i).ToString());
                        }
                    }
                    unit.Commit();
                    err = 0;
                }
                catch (Exception e)
                {
                    tracer.AddToContext("Transacción SyBase en Cmd_Execute", "Alerta en Cmd_Execute, en la funcion de indice: " + cont + " Metodo: " + MethodEX + " Target: " + TargetEX);
                    tracer.TraceException("Alerta en Cmd_Execute", e);
                    unit.Rollback();
                }
                finally
                {
                    unit.EndTransaction();
                }

                return err;
            }
        }

        /// <summary>
        /// Busca el nombre del Party; 1º en memoria; 2º a disco.-
        /// </summary>
        /// <param name="initObject"></param>
        /// <param name="unit"></param>
        /// <param name="PrtImp"></param>
        /// <param name="IndNom"></param>
        /// <param name="IndDir"></param>
        /// <param name="NomDir"></param>
        /// <param name="Tipo"></param>
        /// <returns></returns>
        public static string GetDatPrtn(InitializationObject initObject, UnitOfWorkCext01 unit, string PrtImp, short IndNom, short IndDir, string NomDir, string Tipo)
        {
            using (var tracer = new Tracer())
            {
                tracer.TraceInformation("Nro Op: " + initObject.MODGCVD.VgCvd.OpeSin);
                tracer.AddToContext("Invocación metodo: GetDatPrtn", "Busca el nombre del Party; 1º en memoria; 2º a disco. Datos: PrtImp: " + PrtImp + ", IndNom: " + IndNom + ", IndDir: " + IndDir + ", NomDir: " + NomDir + ", Tipo: " + Tipo);
                T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;
                short n = 0;
                string _retValue = string.Empty, p = string.Empty, s1 = string.Empty, s2 = string.Empty;

                try
                {
                    if (string.IsNullOrEmpty(PrtImp))
                    {
                        return _retValue;
                    }
                    p = PrtImp;
                    p = PoneMarcaParty(PrtImp);
                    if (IndNom != -1)
                    {
                        s1 = VB6Helpers.Trim(SyGet_Rsa(unit, p, IndNom));
                    }
                    if (IndDir != -1)
                    {
                        s2 = VB6Helpers.Trim(SyGet_Dadn(unit, p, IndDir, Tipo));
                    }

                    n = 0;
                    VB6Helpers.RedimPreserve(ref Mdl_Funciones_Varias.DatPrtys, 0, n);
                    Mdl_Funciones_Varias.DatPrtys[n].PrtImp = p;
                    Mdl_Funciones_Varias.DatPrtys[n].IndNom = IndNom;
                    Mdl_Funciones_Varias.DatPrtys[n].IndDir = IndDir;
                    Mdl_Funciones_Varias.DatPrtys[n].NomPrty = s1;
                    Mdl_Funciones_Varias.DatPrtys[n].DirPrty = s2;

                    if (NomDir == "N")
                    {
                        _retValue = s1;
                    }
                    if (NomDir == "D")
                    {
                        return s2;
                    }

                    return _retValue;
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta al GetDatPrtn", e);
                    return string.Empty;
                }
            }
        }

        //public static string SyGet_Dadn(UnitOfWorkCext01 unit, string IdParty, short IdDireccion, string Tipo)
        //{
        //    string _retValue = String.Empty;
        //    string Que = "";
        //    string R = "";
        //    try
        //    {
        //        _retValue = unit.SceRepository.EjecutarSP<string>("sce_dad_s04_MS", MODGSYB.dbcharSy(IdParty), IdDireccion.ToString(), Tipo).First();
        //    }
        //    catch (Exception _ex)
        //    {
        //        //VB6Helpers.MsgBox("Se ha producido un error al tratar de leer la Dirección del Participante (Sce_Dad). El Servidor reporta : [" + VB6Helpers.Left(VB6Helpers.Trim(Mdl_SRM.ParamSrm8k.mensaje.Value), 100) + "]. Reporte este problema.", MsgBoxStyle.Information, "Participantes de Comercio Exterior");
        //        _retValue = String.Empty;
        //    }
        //    return _retValue;
        //}

        public static string Busca_TipCVD(string OpeSin, UnitOfWorkCext01 unit, InitializationObject initObj)
        {
            using (var trace = new Tracer())
            {
                #region Inicializacion Variables
                string Result = null;
                #endregion

                try
                {
                    Result = unit.SceRepository.pro_sce_cvd_ft_s01_MS(
                                MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 1, 3)),
                                MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 4, 2)),
                                MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 6, 2)),
                                MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 8, 3)),
                                MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 11, 5))
                        );
                }
                catch (Exception ex)
                {
                    trace.TraceException("Alerta", ex);
                    throw;
                }
                return Result;
            }
        }

        public static dynamic Valida_Anulacion(string OpeSin, InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            //dynamic _retValue = null;


            //short Li_Largo1 = 0;
            //short Li_Largo2 = 0;
            //short APartirDe = 1;
            //bool sigue = true;
            //short num_registros_msg = 0;
            //string ls_ret = "";
            //string ls_sql = "";
            //string ls_retorno = "";
            //string ls_mensaje = "";

            return unit.SceRepository.pro_sce_cvd_ft_s02_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 1, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 4, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 6, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 8, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 11, 5)));

            //var RESULTADO = Convert.ToBoolean(unit.SceRepository.pro_sce_cvd_ft_s02_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 1, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 4, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 6, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 8, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 11, 5))));
            //ls_sql = unit.SceRepository.pro_sce_cvd_ft_s02_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 1, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 4, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 6, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 8, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 11, 5)));

            //while (sigue)
            //{
            //    if (!(VB6Helpers.Mid(OpeSin, 1, 2) == "00"))
            //    {
            //        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
            //        {
            //            Type = TipoMensaje.Error,
            //            Text = "Se ha producido un error en la consulta a la Base de datos."
            //        });
            //        //return false;
            //        _retValue = "false";
            //       }
            //    else
            //    {
            //        if (VB6Helpers.Mid(OpeSin, 3, 1) == "N")
            //        {
            //            sigue = false;
            //        }

            //        if (ls_ret == "")
            //        {
            //            //_retValue = false;
            //            sigue = false;
            //            return _retValue;
            //        }

            //        //if (num_registros_msg == 0)
            //        //{
            //        //    _retValue = false;
            //        //    sigue = false;
            //        //}
            //        //else
            //        //{
            //        //    Li_Largo1 = (short)VB6Helpers.Len(ls_ret);
            //        //    Li_Largo2 = 1;
            //        //    ls_retorno = Mdl_SRM.Trae_Campo(ls_ret, Li_Largo2);
            //        //}

            //        _retValue = ls_retorno;
            //    }

            //}
            //return _retValue;
        }

        public static string BuscaCta(string NemCta, InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            sce_cta_s01_MS_Result R = null;



            //Se ejecuta la consulta.
            R = unit.SceRepository.EjecutarSP<sce_cta_s01_MS_Result>("sce_cta_s01_MS", NemCta).LastOrDefault();

            if (R != null)
                return R.cta_nom;
            else
                return null;
        }

        public static dynamic Rescata_Contract_Auto(UnitOfWorkCext01 unit, string trxid)
        {
            dynamic _retValue = null;
            bool sigue = true;
            _retValue = false;

            while (sigue)
            {
                unit.SceRepository.ReadQuerySP((reader) =>
                {
                    if (reader.Read())
                    {
                        _retValue = reader.GetString(0);
                        sigue = false;
                    }
                }, "pro_sce_relacion_s01_MS", trxid, "0");

                //VB6Helpers.DoEvents();
                //Mdl_SRM.ParamSrm8k.APartirDe = APartirDe;
                //ls_ret = Mdl_SRM.SendQuery(Mdl_SRM.ParamSrm8k.Base, Mdl_SRM.ParamSrm8k.Servidor.Value, Mdl_SRM.ParamSrm8k.Nodo.Value, ref ls_sql);
                //APartirDe = Mdl_SRM.ParamSrm8k.APartirDe;

                /*if (!(VB6Helpers.Mid(Mdl_SRM.ParamSrm8k.mensaje.Value, 1, 2) == "00"))
                {
                    VB6Helpers.Beep();
                    VB6Helpers.MsgBox("Se ha producido un error en la consulta a la Base de datos", MsgBoxStyle.Information, "Ingreso de Operaciones Manuales");
                    return false;
                }
                else
                {
                    if (VB6Helpers.Mid(Mdl_SRM.ParamSrm8k.mensaje.Value, 3, 1) == "N")
                    {
                        sigue = false;
                    }

                    if (ls_ret == "")
                    {
                        _retValue = false;
                        sigue = false;
                        return _retValue;
                    }

                    num_registros_msg = Mdl_SRM.getocurrs();

                    if (num_registros_msg == 0)
                    {
                        _retValue = false;
                        sigue = false;
                    }
                    else
                    {
                        //Captura de Mensaje de retorno
                        Li_Largo1 = (short)VB6Helpers.Len(ls_ret);
                        Li_Largo2 = 1;

                        //llena datos
                        ls_retorno = Mdl_SRM.Trae_Campo(ls_ret, Li_Largo2);
                    }

                    _retValue = ls_retorno;
                //}*/

            }

            return _retValue;
        }

        public static void SyGet_TipAut(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            try
            {
                var result = unit.SceRepository.sce_aut_s01_MS();
                if (result.Count == 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "No se han encontrado datos en la Tabla de Autorizaciones (sce_aut)" });
                    return;
                }
                initObj.Mdl_Funciones_Varias.V_TipAut = new T_VTipAut[result.Count];
                for (int i = 0; i < result.Count; i++)
                {
                    initObj.Mdl_Funciones_Varias.V_TipAut[i] = new T_VTipAut { CodAut = result[i].codaut, DesAut = result[i].desaut };
                    //initObj.Mdl_Funciones_Varias.V_TipAut[i].CodAut = result[i].codaut;
                    //initObj.Mdl_Funciones_Varias.V_TipAut[i].DesAut = result[i].desaut;
                }
            }
            catch
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message() { Type = TipoMensaje.Error, Text = "Se ha producido un error al tratar de leer la Tabla de Autorizaciones(sce_aut)." });
                return;
            }



        }


        public static void RecuperaNumeroOperacionNoUtilizado(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            using (var trace = new Tracer())
            {
                trace.TraceInformation("Entra Metodo: RecuperaNumeroOperacionNoUtilizado");
                try
                {
                    //Si el producto es 30 se debe enviar al procedimiento "CVI" y viceversa, ya que el que se debe descontar es el que no se uso
                    string codfun = initObj.Module1.Codop.Id_Product == "30" ? T_Mdl_Funciones_Varias.cod_producto_CVD : T_Mdl_Funciones_Varias.cod_producto;
                    string codcct = initObj.Module1.Codop.Cent_Costo;
                    string codesp = initObj.Module1.Codop.Id_Especia;
                    //Si el producto es 30 se debe enviar al procedimiento el numero de "CVI" y viceversa, ya que el que se debe descontar es el que no se uso
                    string numact = (initObj.Module1.Codop.Id_Product == "30" ? initObj.Module1.Codop_CVD.Id_Operacion : initObj.Module1.Codop_FT.Id_Operacion) ?? "0";
                    trace.TraceInformation("Consulta SP pro_sce_rng_u01_MS: " + codcct + codesp + codfun + int.Parse(numact));
                    unit.SceRepository.pro_sce_rng_u01_MS(codcct, codesp, codfun, int.Parse(numact));
                }
                catch (Exception ex)
                {
                    trace.TraceException("Alerta", ex);
                    throw;
                }
            }
        }
    }
}
