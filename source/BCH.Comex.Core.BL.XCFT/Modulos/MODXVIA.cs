using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
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
    public class MODXVIA
    {
        public static T_MODXVIA GetMODXVIA()
        {
            return new T_MODXVIA();
        }

        //****************************************************************************
        //   1.  Retorna los swifts en las Vías.
        //****************************************************************************
        public static IList<T_xVia> GetSwifts(T_MODXVIA MODXVIA)
        {
            return MODXVIA.VxVia.Where(v => (v.NumCta == T_MODGCON0.IdCta_OPOP || v.NumCta == T_MODGCON0.IdCta_OPC) && v.Status != T_MODGCVD.EstadoEli).ToList();
        }

        //****************************************************************************
        //   1.  Indica si hay montos para justificar como Orígenes.
        //****************************************************************************
        public static short HayVxVia(T_MODXVIA MODXVIA)
        {
            short i = 0;
            for (i = 0; i <= (short)VB6Helpers.UBound(MODXVIA.VxMtoVia); i++)
            {
                if (MODXVIA.VxMtoVia[i].MtoTot > 0)
                {
                    return (short)(true ? -1 : 0);
                }

            }

            return 0;
        }

        //****************************************************************************
        //   1.  Retorna el número de Cheques en las Vías.
        //****************************************************************************
        public static short TotalChq(T_MODXVIA MODXVIA)
        {
            short i = 0;
            short n = 0;
            for (i = 0; i <= (short)VB6Helpers.UBound(MODXVIA.VxVia); i++)
            {
                if (MODXVIA.VxVia[i].Status != T_MODXVIA.ExVia_Eli)
                {
                    if (MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CHMEBCH)
                    {
                        n = (short)(n + 1);
                    }
                    if (MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_VVBCH)
                    {
                        n = (short)(n + 1);
                    }
                }

            }

            return n;
        }

        //****************************************************************************
        //   1.  Inicializa estructura de Destino de Fondos.
        //****************************************************************************
        public static void Pr_Init_xVia(T_MODXVIA MODXVIA, T_MODGSWF MODGSWF, T_MODGCHQ MODGCHQ)
        {
            MODXVIA.VxMtoVia = new T_xMtoVia[0];
            MODXVIA.VxVia = new T_xVia[0];
            MODGSWF.VSwf = new T_Swf[0];
            MODGCHQ.V_Chq_VVi = new Chq_Vvi[0];
            MODGSWF.VMT103 = new T_mt103[0] ;
            MODGSWF.VCod = new T_Campo23E[0];
            MODXVIA.VgxVia = new T_gxVia();
        }

        //****************************************************************************
        //   1.  Lee la Tabla de Destinos de Fondos Moneda Extranjera.
        //   2.  Si el resultado es 1(True)  => Lectura Exitosa.
        //       Si el resultado es 0(False) => Lectura No Exitosa.
        //****************************************************************************
        public static short SyGetn_Tdme(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            short _retValue = 0;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            try
            {
                MODXVIA.VTDme = unit.SceRepository.EjecutarSP<sce_tdme_s01_MS_Result>("sce_tdme_s01_MS").Select(x => new T_Tdme()
                {
                    CodDme = (short)x.coddme,
                    DesDme = x.desdme
                }).ToArray();
                _retValue = (short)(true ? -1 : 0);
            }
            catch (Exception _ex)
            {
                _retValue = 0;
            }
            return _retValue;
        }

        public static void CambiaBcxEntrada(InitializationObject initObject, string band)
        {
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            string Ent = MODXVIA.BcxEntrada2;

            // Ventana de Participantes
            if (band == "P")
            {
                MODXVIA.BcxEntrada2 = "1" + VB6Helpers.Mid(MODXVIA.BcxEntrada2, 2, 4);
            }

            // Ventana de Anticipo C.
            if (band == "A")
            {
                MODXVIA.BcxEntrada2 = VB6Helpers.Mid(MODXVIA.BcxEntrada2, 1, 1) + "1" + VB6Helpers.Mid(MODXVIA.BcxEntrada2, 3, 3);
            }

            // Ventana de Vias
            if (band == "V")
            {
                MODXVIA.BcxEntrada2 = VB6Helpers.Mid(MODXVIA.BcxEntrada2, 1, 2) + "1" + VB6Helpers.Mid(MODXVIA.BcxEntrada2, 4, 2);
            }

            // Ventana de Cheques
            if (band == "C")
            {
                MODXVIA.BcxEntrada2 = VB6Helpers.Mid(MODXVIA.BcxEntrada2, 1, 3) + "1" + VB6Helpers.Mid(MODXVIA.BcxEntrada2, 5, 1);
            }

            // Ventana de Orden de Pagos
            if (band == "O")
            {
                MODXVIA.BcxEntrada2 = VB6Helpers.Mid(MODXVIA.BcxEntrada2, 1, 4) + "1";
            }

        }

        //Ingresa un monto para justificar en las Vias.-
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        // UPGRADE_INFO (#0561): The 'Put_xVia' symbol was defined without an explicit "As" clause.
        public static dynamic Put_xVia(InitializationObject initObject, UnitOfWorkCext01 unit, short CodMnd, double monto, short Vuelto)
        {
            UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;

            short m = 0;
            short n = 0;
            short j = 0;
            short l = 0;
            //Advertencia.-
            if (CodMnd == 0)
            {
                Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Se intenta justificar un monto sin moneda Banco de Chile. Reporte este problema."
                });
                return false;
            }

            m = (short)VB6Helpers.UBound(MODXVIA.VxMtoVia);
            n = (short)VB6Helpers.UBound(MODXVIA.VxVia);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            if (m == -1)
            {
                //VB6Helpers.Redim(ref MODXVIA.VxMtoVia, 0, 0);
            }
            if (n == -1)
            {
                //VB6Helpers.Redim(ref MODXVIA.VxVia, 0, 0);
            }

            //Busca la moneda en el arreglo Vías.-
            n = -1;
            for (j = 0; j <= (short)m; j++)
            {
                if (MODXVIA.VxMtoVia[j].CodMon == CodMnd && MODXVIA.VxMtoVia[j].Vuelto == Vuelto)
                {
                    n = j;
                    break;
                }

            }

            //Hay que crear la vía para la moneda.-
            if (n == -1)
            {
                n = (short)(VB6Helpers.UBound(MODXVIA.VxMtoVia)+1);
                VB6Helpers.RedimPreserve(ref MODXVIA.VxMtoVia, 0, n);
                MODXVIA.VxMtoVia[n].CodMon = CodMnd;
                MODXVIA.VxMtoVia[n].MtoTot = monto;
                MODXVIA.VxMtoVia[n].Vuelto = Vuelto;
                l = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, MODXVIA.VxMtoVia[n].CodMon);
                MODXVIA.VxMtoVia[n].NemMon = MODGTAB0.VMnd[l].Mnd_MndNmc;
            }
            else
            {
                MODXVIA.VxMtoVia[n].MtoTot += monto;
            }

            return true;
        }

        //****************************************************************************
        //   1.  Verifica si existe el número de una cuenta corriente en los arreglos
        //       VxVia(Destinos de Fondos), VxOri(Origenes de Fondos).
        //   2.  El resultado será:
        //       True  => Si existe CtaCte en VxOri o VxVia.
        //       False => Si no existe CtaCte en VxOri o VxVia.
        //****************************************************************************
        public static short Fn_ExisteCtaCte(InitializationObject initObject)
        {
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODXORI MODXORI = initObject.MODXORI;

            short _retValue = 0;
            short i = 0;
            _retValue = (short)(false ? -1 : 0);

            for (i = 0; i <= (short)VB6Helpers.UBound(MODXVIA.VxVia); i++)
            {
                if ((MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CtaCteMN) || (MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CtaCteME) || (MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CtaCteAUTN) || (MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CtaCteAUTE) || (MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CtaCteMANN) || (MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CtaCteMANE) || (MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_GAPMN) || (MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_GAPME))
                {

                    if (MODXVIA.VxVia[i].Status != T_MODXVIA.ExVia_Eli)
                    {
                        return (short)(true ? -1 : 0);
                    }

                }

            }

            for (i = 0; i <= (short)VB6Helpers.UBound(MODXORI.VxOri); i++)
            {
                if ((MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteMN) || (MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteME) || (MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteAUTN) || (MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteAUTE) || (MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteMANN) || (MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteMANE) || (MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_GAPMN) || (MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_GAPME))
                {

                    if (MODXORI.VxOri[i].Status != T_MODXVIA.ExVia_Eli)
                    {
                        return (short)(true ? -1 : 0);
                    }

                }

            }

            return _retValue;
        }


        //****************************************************************************
        //   1.  Graba los Avisos de Débito/Crédito encontrados en VxOri/VxVia.
        //   2.  Retorna un string "0"+n1+n2+...+n, donde 0 indica éxito de la
        //       operación seguida de los correlativos de las cartas.-
        //****************************************************************************
        public static string SyPutn_Adc(InitializationObject initObject, UnitOfWorkCext01 unit, string NumOpe, dynamic Referencia, string Usuario, short ImpDeb)
        {
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_Module1 Module1 = initObject.Module1;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_MODGMTA MODGMTA = initObject.MODGMTA;
            T_MODGSCE MODGSCE = initObject.MODGSCE;


            string _retValue = "";
            short a = 0;
            short i = 0;
            short x = 0;
            string s = "";
            double[] MtoDC;
            try
            {
                s = initObject.SyPutn_Adc_Str;
                _retValue = "0;" + s;

            }
            catch (Exception _ex)
            {
                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Informacion,
                    Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                    Title = T_MODXVIA.MsgXDoc
                });
            }
            return _retValue;
        }        

        //****************************************************************************
        //Esta función existía en módulo ModYcom.Bas
        //****************************************************************************
        //Entrega Glosa de Comisiones.-
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static string Fn_GetGlosa(InitializationObject initObj, short ind)
        {
            short m = 0;
            string n = "";
            short i = 0;
            
            m = (short)VB6Helpers.UBound(initObj.MODXVIA.Vxcom);

            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            n = "";
            for (i = 1; i <= (short)m; i++)
            {
                if(((initObj.MODXVIA.Vxcom[i].Status != T_MODGCVD.EstadoEli && !string.IsNullOrEmpty(initObj.MODXVIA.Vxcom[i].nomcom) && initObj.MODXVIA.Vxcom[i].MtoTot > 0 ? -1 : 0) & initObj.MODXVIA.Vxcom[i].Cobrar & (~initObj.MODXVIA.Vxcom[i].PagExt) & (initObj.MODXVIA.Vxcom[i].PosPrt == ind ? -1 : 0)) != 0)
                {
                    if (VB6Helpers.Trim(initObj.MODXVIA.Vxcom[i].nomcom) == "Impuesto Sobre Cheque")
                    {
                        // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 'n' variable as a StringBuilder6 object.
                        n = n + VB6Helpers.Trim(initObj.MODXVIA.Vxcom[i].nomcom) + "(s), ";
                    }
                    else
                    {
                        n = n + "Comisión de " + VB6Helpers.Trim(initObj.MODXVIA.Vxcom[i].nomcom) + ", ";
                    }

                }

            }

            n = VB6Helpers.Trim(n);
            if(!string.IsNullOrEmpty(n))
            {
                n = VB6Helpers.Left(n, VB6Helpers.Len(n) - 1);
            }

            return n;
        }

        //****************************************************************************
        //   1.  Función que en base a ciertos parámetros permite conformar un string
        //       con los cuales se estructura la carta de caracter general Nro. 999.
        //   2.  Los Parámetros son los sgtes.:
        //           -   Número de la Operación,
        //           -   Indice del PartysOpe(),
        //           -   Débito o Crédito,
        //           -   Número de Cta. Cte. Concepto,
        //           -   Arreglo de Montos().
        //   3.  Retorna el correlativo con el cual se graba la carta.-
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        // UPGRADE_INFO (#0561): The 'TipoDC' symbol was defined without an explicit "As" clause.
        // UPGRADE_INFO (#0561): The 'NroCtaCte' symbol was defined without an explicit "As" clause.
        // UPGRADE_INFO (#0561): The 'Referencia' symbol was defined without an explicit "As" clause.
        public static short Doc_gAdc(InitializationObject initObject, UnitOfWorkCext01 unit, string NumOpe, ref PartyKey Party, dynamic TipoDC, dynamic NroCtaCte, string Concepto, string NemMnd, double[] Montos, string Referencia, string Usuario)
        {
            short _retValue = 0;
            string s = "";
            short i = 0;
            short n = 0;

            n = (short)VB6Helpers.UBound(Montos);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            s = "";
            s = s + VB6Helpers.Trim(NumOpe) + VB6Helpers.Chr(9);
            s = s + Party.NombreUsado + VB6Helpers.Chr(9);
            s = s + Party.DireccionUsado + VB6Helpers.Chr(9);
            s = s + Party.CiudadUsado + VB6Helpers.Chr(9);
            s = s + Party.EstadoUsado + VB6Helpers.Chr(9);
            s = s + Party.PostalUsado + VB6Helpers.Chr(9);
            s = s + Party.PaisUsado + VB6Helpers.Chr(9);
            s = s + Party.Fax + VB6Helpers.Chr(9);
            s = s + Party.CasBanco + VB6Helpers.Chr(9);
            s = s + Party.CasPostal + VB6Helpers.Chr(9);
            s = s + VB6Helpers.Format(VB6Helpers.CStr(Party.Enviara)) + VB6Helpers.Chr(9);

            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'TipoDC'. Consider using the GetDefaultMember6 helper method.
            s = s + VB6Helpers.Trim(VB6Helpers.CStr(TipoDC)) + VB6Helpers.Chr(9);  //<D>ébito/<C>rédito.
            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'NroCtaCte'. Consider using the GetDefaultMember6 helper method.
            s = s + VB6Helpers.Trim(VB6Helpers.CStr(NroCtaCte)) + VB6Helpers.Chr(9);
            s = s + VB6Helpers.Trim(Concepto) + VB6Helpers.Chr(9);

            s = s + VB6Helpers.Trim(VB6Helpers.Str(n+1)) + VB6Helpers.Chr(9);
            for (i = 0; i <= (short)n; i++)
            {
                // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 's' variable as a StringBuilder6 object.
                s = s + NemMnd + VB6Helpers.Chr(9);
                s = s + Montos[i] + VB6Helpers.Chr(9);
            }

            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Referencia'. Consider using the GetDefaultMember6 helper method.
            s = s + Referencia + VB6Helpers.Chr(9);

            //Atributos del Usuario
            s = s + VB6Helpers.Left(Usuario, 3) + VB6Helpers.Chr(9);  //Centro de Costo.
            s = s + VB6Helpers.Right(Usuario, 2) + VB6Helpers.Chr(9);  //Especialista.

            //Retona el correlativo de la carta.
            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'TipoDC'. Consider using the GetDefaultMember6 helper method.
            if (TipoDC == "D")
            {
                _retValue = Mdl_Funciones_Varias.SyPut_xDoc(initObject, unit, NumOpe, T_MODXVIA.DocGAdeb, s, Usuario);
            }

            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'TipoDC'. Consider using the GetDefaultMember6 helper method.
            if (TipoDC == "C")
            {
                return Mdl_Funciones_Varias.SyPut_xDoc(initObject, unit, NumOpe, T_MODXVIA.DocGAcre, s, Usuario);
            }

            return _retValue;
        }
    }
}
