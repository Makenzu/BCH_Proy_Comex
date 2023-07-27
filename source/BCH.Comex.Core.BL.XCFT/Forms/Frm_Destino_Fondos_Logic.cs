using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
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

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public static class Frm_Destino_Fondos_Logic
    {
        #region METODOS PUBLICOS
        public static void Form_Load(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_MODCVDIM MODCVDIM = initObject.MODCVDIM;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            T_MODGPYF0 MODGPYF0 = initObject.MODGPYF0;
            T_MODGMTA MODGMTA = initObject.MODGMTA;
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;
            T_ModChVrf ModChVrf = initObject.ModChVrf;
            initObject.Frm_Destino_Fondos.Boton[0].Enabled = false;

            short[] Tabs = null;
            MODCVDIM.CtaCCDin = new CtaCC[0];
            MODXORI.ori_des = "des";


            short i = 0;
            short rech = 0;
            short reop = 0;
            short ini = 0;
            short h = 0;
            short a = 0;
            short n = 0;
            short b = 0;

            Frm_Destino_Fondos.frm_datos.Visible = true;
            Frm_Destino_Fondos.l_mto.Header = new List<string>() { "Moneda", "Monto" };
            Frm_Destino_Fondos.l_via.Header = new List<string>() { "Destino", "Moneda", "Monto" };

            Tabs = new short[1];
            Tabs[0] = 22;

            Tabs = new short[2];
            Tabs[0] = 130;
            Tabs[1] = 150;

            //Carga lista de montos.
            Pr_Carga_l_mto(Frm_Destino_Fondos.l_mto, initObject);
            l_mto_Click(initObject, unit);

            //Carga lista de monedas.
            for (i = 0; i <= (short)VB6Helpers.UBound(MODXVIA.VxMtoVia); i++)
            {
                if ((~MODXVIA.VgxVia.Vuelto | (MODXVIA.VgxVia.Vuelto & MODXVIA.VxMtoVia[i].Vuelto)) != 0)
                {
                    n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, MODXVIA.VxMtoVia[i].CodMon);
                    Frm_Destino_Fondos.L_Mnd.Items.Add(new UI_ComboItem()
                    {
                        Data = MODGTAB0.VMnd[n].Mnd_MndCod,
                        Value = MODGPYF1.Minuscula(MODGTAB0.VMnd[n].Mnd_MndNom)
                    });
                }
            }

            if (Frm_Destino_Fondos.L_Mnd.Items.Count > 0)
            {
                Frm_Destino_Fondos.L_Mnd.ListIndex = 0;
                l_mnd_Click(initObject, unit);
            }

            if (MODXVIA.VgxVia.destino == 0)
            {
                Frm_Destino_Fondos.Cb_Destino.Enabled = false;
            }
            a = BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.SyGetn_Tdme(initObject, unit);
            if (a != 0)
            {
                Pr_Cargar_Destinos(initObject, Frm_Destino_Fondos.Cb_Destino);
                Cb_Destino_Click(initObject);
            }
            else
            {
                Frm_Destino_Fondos.Errors.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Se ha producido un error al tratar de leer los datos de los Destinos de Fondos M / E."
                });
            }

            //Carga lista de Vías.
            Frm_Destino_Fondos.Boton[0].Enabled = false;
            b = BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.SyGetn_Suc(MODXORI, unit);

            if (MODXVIA.VgxVia.Vuelto != 0)
            {
                Frm_Destino_Fondos.Caption = "Vueltos";
            }

            MODXVIA.VgxVia.Acepto = (short)(false ? -1 : 0);

            if ((MODXVIA.EsSolBcx & (VB6Helpers.Mid(MODXVIA.BcxEntrada2, 3, 1) == "0" ? -1 : 0)) != 0)
            {
                rech = 0;
                reop = 0;
                if (VB6Helpers.Len(MODXVIA.VBcxCci2) > 625)
                {
                    rech = (short)VB6Helpers.Val(VB6Helpers.Mid(MODXVIA.VBcxCci2, 278, 2));
                    reop = (short)VB6Helpers.Val(VB6Helpers.Mid(MODXVIA.VBcxCci2, 621, 2));
                }
                else if (VB6Helpers.Len(MODXVIA.VBcxCci2) > 281)
                {
                    rech = (short)VB6Helpers.Val(VB6Helpers.Mid(MODXVIA.VBcxCci2, 280, 2));
                }

                VB6Helpers.Redim(ref MODXVIA.VxVia, 0, rech + reop);

                // Cheques
                ini = 332;
                for (n = 0; n <= (short)rech; n++)
                {
                    MODXVIA.VxVia[n].NemCta = "COE";
                    MODXVIA.VxVia[n].MtoTot = VB6Helpers.Val(BCH.Comex.Core.BL.XCFT.Modulos.MODGPYF0.unformat(MODGPYF0, VB6Helpers.Mid(MODXVIA.VBcxCci2, ini, 16)));
                    MODXVIA.VxVia[n].CodMon = (short)VB6Helpers.Val(VB6Helpers.Mid(MODXVIA.VBcxCci2, 42, 3));
                    MODXVIA.VxVia[n].NemMon = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_NemMnd(MODGTAB0, unit, (short)VB6Helpers.Val(VB6Helpers.Mid(MODXVIA.VBcxCci2, 42, 3)));

                    for (i = 0; i <= (short)VB6Helpers.UBound(MODXORI.VOvd); i++)
                    {
                        if (VB6Helpers.Trim(MODXORI.VOvd[i].NemCta).Equals(MODXVIA.VxVia[n].NemCta.ToString()))
                        {
                            MODXVIA.VxVia[n].NumCta = MODXORI.VOvd[i].NumCta;
                            MODXVIA.VxVia[n].NomVia = MODXORI.VOvd[i].NomCta;
                            if (Frm_Destino_Fondos.Cb_Destino.Enabled)
                            {
                                if (Frm_Destino_Fondos.Cb_Destino.ListIndex != -1)
                                {
                                    MODXVIA.VxVia[n].CodDme = (short)(Frm_Destino_Fondos.Cb_Destino.get_ItemData_(Frm_Destino_Fondos.Cb_Destino.ListIndex));
                                }
                            }
                            else
                            {
                                MODXVIA.VxVia[n].CodDme = 0;
                            }

                            MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoIng;
                            MODXVIA.VxVia[n].Vuelto = MODXVIA.VgxVia.Vuelto;
                            if (Frm_Destino_Fondos.Ch_ImpChq.Value != 0)
                            {
                                MODXVIA.VxVia[n].ImpChq = (short)(true ? -1 : 0);
                            }
                            else
                            {
                                MODXVIA.VxVia[n].ImpChq = (short)(false ? -1 : 0);
                            }

                            MODXVIA.VxVia[n].ctacte = "";
                            break;
                        }
                    }
                    ini = (short)(ini + 68);
                }

                // ChequesOrdenes de Pago
                ini = 764;
                for (n = (short)(rech); n <= (short)(rech + reop); n++)
                {
                    MODXVIA.VxVia[n].NemCta = "OPE";
                    MODXVIA.VxVia[n].MtoTot = VB6Helpers.Val(BCH.Comex.Core.BL.XCFT.Modulos.MODGPYF0.unformat(MODGPYF0, VB6Helpers.Mid(MODXVIA.VBcxCci2, ini, 16)));
                    MODXVIA.VxVia[n].CodMon = VB6Helpers.CShort(VB6Helpers.Mid(MODXVIA.VBcxCci2, 42, 3));
                    MODXVIA.VxVia[n].NemMon = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_NemMnd(MODGTAB0, unit, (short)VB6Helpers.Val(VB6Helpers.Mid(MODXVIA.VBcxCci2, 42, 3)));

                    for (i = 0; i <= (short)VB6Helpers.UBound(MODXORI.VOvd); i++)
                    {
                        if (VB6Helpers.Trim(MODXORI.VOvd[i].NemCta).Equals(MODXVIA.VxVia[n].NemCta.ToString()))
                        {
                            MODXVIA.VxVia[n].NumCta = MODXORI.VOvd[i].NumCta;
                            MODXVIA.VxVia[n].NomVia = MODXORI.VOvd[i].NomCta;
                            if (Frm_Destino_Fondos.Cb_Destino.Enabled)
                            {
                                if (Frm_Destino_Fondos.Cb_Destino.ListIndex != -1)
                                {
                                    MODXVIA.VxVia[n].CodDme = (short)(Frm_Destino_Fondos.Cb_Destino.get_ItemData_(Frm_Destino_Fondos.Cb_Destino.ListIndex));
                                }
                            }
                            else
                            {
                                MODXVIA.VxVia[n].CodDme = 0;
                            }
                            MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoIng;
                            MODXVIA.VxVia[n].Vuelto = MODXVIA.VgxVia.Vuelto;
                            if (Frm_Destino_Fondos.Ch_ImpChq.Value != 0)
                            {
                                MODXVIA.VxVia[n].ImpChq = (short)(true ? -1 : 0);
                            }
                            else
                            {
                                MODXVIA.VxVia[n].ImpChq = (short)(false ? -1 : 0);
                            }
                            MODXVIA.VxVia[n].ctacte = "";
                            break;
                        }

                    }

                    ini = (short)(ini + 393);
                }

                //Se ingresan los nuevos datos en la lista.-
                Pr_Llena_l_via(initObject, unit);

                //Si todo está justificado => se posiciona en botón Aceptar.-
                if (Fn_ViasOK(initObject) == -1)
                {
                    Frm_Destino_Fondos.Boton[0].Enabled = true;
                }
                else
                {
                    Frm_Destino_Fondos.Boton[0].Enabled = false;
                }

            }

            //Deshabilita el boton del Impuesto en caso de que el flag este en 0
            if (T_MODGMTA.impflag == 0)
            {
                Frm_Destino_Fondos.Ch_ImpChq.Value = 0;
                Frm_Destino_Fondos.Ch_ImpChq.Enabled = false;
            }

            //-----------------------------------------------------------------------------------------------
            short k = 0;
            //---------------------------------------------
            //Realsystems-Código Nuevo-Inicio
            //Fecha Modificación 20100623
            //Responsable: Pablo Millan
            //Versión: 1.0
            //Descripción : Se modifica generacion de CRN
            //---------------------------------------------
            if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 0)
            {
                Frm_Destino_Fondos.Text1[4].Text = VB6Helpers.Mid(Mdl_Funciones_Varias.LC_TRXID_MAN, 16, 10);  // Contract reference number
            }
            //----------------------------------------
            // RealSystems - Código Nuevo - Termino
            //----------------------------------------
            // RealSystems - Código Antiguo - Inicio
            //----------------------------------------
            //If CARGA_AUTOMATICA = 0 Then Me.Text1(4) = Mid$(VgCvd.OpeSin, 6, 2) & Mid$(VgCvd.OpeSin, 8, 3) & Mid$(VgCvd.OpeSin, 11, 5)      ' Contract reference number
            //----------------------------------------
            // RealSystems - Código Antiguo - Termino
            //----------------------------------------
            ModChVrf.AceptoPantallaChVrf = (short)(false ? -1 : 0);

            if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
            {

                Pr_CargaDESTINOS(initObject, unit);

                Frm_Destino_Fondos.frm_datos.Enabled = false;
                Frm_Destino_Fondos.frm_datos.Visible = false;

                //'Busca en L_cta El indice de la cuenta automatica cosmos que sea Mon_Nacional o Mon_ext
                //'''' Incoming = 62
                if (VB6Helpers.Int(Mdl_Funciones_Varias.LC_PRD) == 62)
                {
                    for (k = 0; k <= (short)(Frm_Destino_Fondos.L_Cta.Items.Count - 1); k++)
                    {
                        if ((MODXORI.VOvd[Frm_Destino_Fondos.L_Cta.get_ItemData_(k)].NumCta == 62) || (MODXORI.VOvd[Frm_Destino_Fondos.L_Cta.get_ItemData_(k)].NumCta == 63))
                        {
                            //VxVia(j%).NumCta Then
                            Frm_Destino_Fondos.L_Cta.ListIndex = k;  //'encontro indice 40
                            L_Cta_Click(initObject, unit);
                        }
                    }
                }

                //'Busca en L_cta El indice la cuenta Varios Acreedores Export
                //'''' Outgoing = 72 - 74
                if (VB6Helpers.Int(Mdl_Funciones_Varias.LC_PRD) != 62)
                {
                    for (k = 0; k <= (short)(Frm_Destino_Fondos.L_Cta.Items.Count - 1); k++)
                    {
                        if ((MODXORI.VOvd[Frm_Destino_Fondos.L_Cta.get_ItemData_(k)].NumCta == 23))
                        {
                            Frm_Destino_Fondos.L_Cta.ListIndex = k;  //'encontro indice 6
                            L_Cta_Click(initObject, unit);
                        }

                    }

                    Frm_Destino_Fondos.Tx_Datos[0].Text = VB6Helpers.CStr(BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Rescata_Referencia(Mdl_Funciones_Varias.LC_MONEDA, unit));
                    Frm_Destino_Fondos.Tx_Datos[1].Text = Mdl_Funciones_Varias.LC_CONREFNUM;
                }
            }

            Frm_Destino_Fondos.CargaAutomatica = Mdl_Funciones_Varias.CARGA_AUTOMATICA;

            if (Frm_Destino_Fondos.L_Cuentas.Items.Count == 0)
                Frm_Destino_Fondos.L_Cuentas.Enabled = false;
        }
        public static void L_Cta_Click(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;
            T_MODCVDIM MODCVDIM = initObject.MODCVDIM;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODGMTA MODGMTA = initObject.MODGMTA;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_Module1 Module1 = initObject.Module1;
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;

            short j = 0;
            short c = 0;

            //Deshabilita todo.
            MODCVDIM.CtaCCDin = new CtaCC[0];
            MODXVIA.Moneda_TRX = "";
            Frm_Destino_Fondos.frm_datos.Visible = false;
            Frm_Destino_Fondos.frm_datos.Enabled = true;
            Frm_Destino_Fondos.frm_infoctagap.Visible = false;
            Frm_Destino_Fondos.L_Cuentas.Visible = true;
            Frm_Destino_Fondos.Cb_Destino.Visible = true;

            Frm_Destino_Fondos.L_Partys.Items.Clear();
            Frm_Destino_Fondos.L_Cuentas.Items.Clear();
            Frm_Destino_Fondos.L_Partys.Enabled = false;
            Frm_Destino_Fondos.L_Cuentas.Enabled = false;
            Frm_Destino_Fondos.Ok_Partys.Enabled = false;
            Frm_Destino_Fondos.L_Partys.ListIndex = -1;
            L_Partys_Click(initObject, unit);
            Frm_Destino_Fondos.L_Cuentas.ListIndex = -1;
            L_Cuentas_Click(initObject);
            Frm_Destino_Fondos.txtNumRef.Text = "";
            Frm_Destino_Fondos.txtNumRef.Visible = false;
            Frm_Destino_Fondos.LB_Referencia.Visible = false;
            Frm_Destino_Fondos.cmb_codtran.Enabled = false;
            Frm_Destino_Fondos.Tx_Cuentas[0].Visible = false;
            Frm_Destino_Fondos.cmb_codtran.Visible = false;
            Frm_Destino_Fondos.cmb_codtran.ListIndex = -1;

            for (j = 0; j <= 2; j++)
            {
                Frm_Destino_Fondos.Tx_Datos[j].Visible = false;
                Frm_Destino_Fondos.Lb_Datos[j].Visible = false;
            }

            Frm_Destino_Fondos.Lb_Oficina.Visible = false;
            Frm_Destino_Fondos.BNem.Visible = false;


            //Cuando se entra ninguna cuenta se debe encontrar cargada.-
            //if (Frm_Destino_Fondos.l_via.Items.Count == 0 || Frm_Destino_Fondos.L_Cta.ListIndex == -1)
            //{
            //    Frm_Destino_Fondos.L_Cta.ListIndex = -1;
            //    return;
            //}
            if (Frm_Destino_Fondos.L_Cta.ListIndex == -1)
            {
                return;
            }

            Frm_Destino_Fondos.Indice_Cuenta = (short)(Frm_Destino_Fondos.L_Cta.get_ItemData_(Frm_Destino_Fondos.L_Cta.ListIndex));

            //Deshabilita el boton del Impuesto en caso de que el flag este en 0
            if (T_MODGMTA.impflag == 0)
            {
                Frm_Destino_Fondos.Ch_ImpChq.Value = 0;
                Frm_Destino_Fondos.Ch_ImpChq.Enabled = false;
            }
            else
            {
                //Caja para determinar Impuesto sobre cheque.-
                if (MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_CHMEBCH)
                {
                    Frm_Destino_Fondos.Ch_ImpChq.Enabled = true;
                }
                else
                {
                    Frm_Destino_Fondos.Ch_ImpChq.Enabled = false;
                }

            }

            //-----------------------------------------------------------------------------------------------
            if (MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_CHVRF || MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_OPC || MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_OPOP || MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_CHMEBCH)
            {
                Frm_Destino_Fondos.Ch_GenPln.Enabled = true;
                if (Module1.Codop.Cent_Costo == "729")
                {
                    Frm_Destino_Fondos.Ch_GenPln.Value = 1;
                }
                else
                {
                    Frm_Destino_Fondos.Ch_GenPln.Value = 0;
                }

            }
            else
            {
                Frm_Destino_Fondos.Ch_GenPln.Value = 0;
                Frm_Destino_Fondos.Ch_GenPln.Enabled = false;
            }

            short _switchVar1 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta; //VOvd(i%).NumCta
            if (_switchVar1 == T_MODGCON0.IdCta_CtaCteMN)
            {
                //Cuenta Corriente M/N.
                Frm_Destino_Fondos.L_Partys.Enabled = true;
                Frm_Destino_Fondos.L_Cuentas.Enabled = true;
                Frm_Destino_Fondos.Ok_Partys.Enabled = true;
                Pr_Cargar_Beneficiario(initObject, unit); //Carga los Participantes

            }
            else if (_switchVar1 == T_MODGCON0.IdCta_CtaCteME || _switchVar1 == T_MODGCON0.IdCta_ChqCCME)
            {
                //Cuenta Corriente M/E.
                if (MODXORI.gb_esCosmos == true)
                {
                    Frm_Destino_Fondos.frm_datos.Visible = true;
                }
                Frm_Destino_Fondos.L_Partys.Enabled = true;
                Frm_Destino_Fondos.L_Cuentas.Enabled = true;
                Frm_Destino_Fondos.Ok_Partys.Enabled = true;
                Pr_Cargar_Beneficiario(initObject, unit); //Carga los Participantes
                if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
                {
                    Frm_Destino_Fondos.frm_datos.Enabled = false;
                }
            }
            else if (_switchVar1 == T_MODGCON0.IdCta_SCSMN || _switchVar1 == T_MODGCON0.IdCta_SCSME)
            {
                //Saldos c/ Sucursales M/N.
                for (j = 0; j <= 2; j++)
                {
                    Frm_Destino_Fondos.Tx_Datos[j].Text = "";
                    Frm_Destino_Fondos.Tx_Datos[j].Visible = true;
                    Frm_Destino_Fondos.Lb_Datos[j].Visible = true;
                }

                Frm_Destino_Fondos.L_Partys.Enabled = true;
                Frm_Destino_Fondos.Lb_Oficina.Visible = true;
                Frm_Destino_Fondos.Lb_Oficina.Text = "";
                Frm_Destino_Fondos.Lb_Datos[0].Text = "Código Oficina";
                Frm_Destino_Fondos.Lb_Datos[1].Text = "Tipo Movimiento";
                Frm_Destino_Fondos.Lb_Datos[2].Text = "Número de Partida";
                Frm_Destino_Fondos.Tx_Datos[0].MaxLength = 3;
                Frm_Destino_Fondos.Tx_Datos[1].MaxLength = 1;
                Frm_Destino_Fondos.Tx_Datos[2].MaxLength = 8;
                Pr_Cargar_Beneficiario(initObject, unit); //Carga los Participantes
                //Se bloquea L_Cuentas
                Frm_Destino_Fondos.L_Cuentas.Enabled = false;
            }
            else if (_switchVar1 == T_MODGCON0.IdCta_CTACTEBC || _switchVar1 == T_MODGCON0.IdCta_CTAORD || _switchVar1 == T_MODGCON0.IdCta_DIVENPEN || _switchVar1 == T_MODGCON0.IdCta_CHVBNYM || _switchVar1 == 54)
            {
                //Cta. Cte. Banco Central -- 'Corresponsal cuenta ordinaria
                for (j = 0; j <= 1; j++)
                {
                    Frm_Destino_Fondos.Tx_Datos[j].Text = "";
                    Frm_Destino_Fondos.Tx_Datos[j].Visible = true;
                    Frm_Destino_Fondos.Lb_Datos[j].Visible = true;
                }

                Frm_Destino_Fondos.L_Partys.Enabled = true;
                Frm_Destino_Fondos.Lb_Datos[0].Text = "Swift";
                Frm_Destino_Fondos.Lb_Datos[1].Text = "Nº de Referencia";
                Frm_Destino_Fondos.Tx_Datos[0].MaxLength = 11;
                Frm_Destino_Fondos.Tx_Datos[1].MaxLength = 15;
                Pr_Cargar_Beneficiario(initObject, unit); //Carga los Participantes
                //Se bloquea L_Cuentas
                Frm_Destino_Fondos.L_Cuentas.Enabled = false;
            }
            else if (_switchVar1 >= 40 && _switchVar1 <= 53)
            {
                //Cuentas de Obligaciones y Check Verification
                for (j = 0; j <= 1; j++)
                {
                    Frm_Destino_Fondos.Tx_Datos[j].Text = "";
                    Frm_Destino_Fondos.Tx_Datos[j].Visible = true;
                    Frm_Destino_Fondos.Lb_Datos[j].Visible = true;
                }

                Frm_Destino_Fondos.L_Partys.Enabled = true;
                Frm_Destino_Fondos.Lb_Datos[0].Text = "Swift";
                Frm_Destino_Fondos.Lb_Datos[1].Text = "Nº de Referencia";
                Frm_Destino_Fondos.Tx_Datos[0].MaxLength = 11;
                Frm_Destino_Fondos.Tx_Datos[1].MaxLength = 15;
                Pr_Cargar_Beneficiario(initObject, unit);
                //Se bloquea L_Cuentas
                Frm_Destino_Fondos.L_Cuentas.Enabled = false;
            }
            else if (_switchVar1 == T_MODGCON0.IdCta_BOEREG || _switchVar1 == T_MODGCON0.IdCta_CHEREG || _switchVar1 == T_MODGCON0.IdCta_OBLREG || _switchVar1 == T_MODGCON0.IdCta_OBLARE || _switchVar1 == T_MODGCON0.IdCta_ACEREG)
            {
                if (Module1.Codop.Cent_Costo == "729" || Module1.Codop.Cent_Costo == "829" || Module1.Codop.Cent_Costo == "827" || Module1.Codop.Cent_Costo == "826")
                {
                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                    {
                        Text = "Esta Cuenta está habilitada solo para REGIONES",
                        Type = TipoMensaje.Error
                    });
                    return;
                }

                for (j = 0; j <= 1; j++)
                {
                    Frm_Destino_Fondos.Tx_Datos[j].Text = "";
                    Frm_Destino_Fondos.Tx_Datos[j].Visible = true;
                    Frm_Destino_Fondos.Lb_Datos[j].Visible = true;
                }

                Frm_Destino_Fondos.L_Partys.Enabled = true;
                Frm_Destino_Fondos.Lb_Datos[0].Text = "Swift";
                Frm_Destino_Fondos.Lb_Datos[1].Text = "Nº de Referencia";
                Frm_Destino_Fondos.Tx_Datos[0].MaxLength = 11;
                Frm_Destino_Fondos.Tx_Datos[1].MaxLength = 15;
                Pr_Cargar_Beneficiario(initObject, unit); //Carga los Participantes
                //Se bloquea L_Cuentas
                Frm_Destino_Fondos.L_Cuentas.Enabled = false;
            }
            else if (_switchVar1 == T_MODGCON0.IdCta_VAM || _switchVar1 == T_MODGCON0.IdCta_VAX || _switchVar1 == T_MODGCON0.IdCta_VAMC || _switchVar1 == T_MODGCON0.IdCta_VAMCC || _switchVar1 == T_MODGCON0.IdCta_VASC)
            {
                //Varios Acreedores Import. -- Varios Acreedores Export. -- 'Varios Acreedores Mcdo. Corr.
                for (j = 0; j <= 0; j++)
                {
                    Frm_Destino_Fondos.Tx_Datos[j].Text = "";
                    Frm_Destino_Fondos.Tx_Datos[j].Visible = true;
                    Frm_Destino_Fondos.Lb_Datos[j].Visible = true;
                }

                Frm_Destino_Fondos.Tx_Datos[0].Text = MODGPYF0.Componer(Module1.PartysOpe[0].LlaveArchivo, "~", "");
                Frm_Destino_Fondos.Lb_Datos[0].Text = "Participante";
                Frm_Destino_Fondos.Tx_Datos[0].MaxLength = 12;
                Frm_Destino_Fondos.Lb_Oficina.Visible = true;
                Frm_Destino_Fondos.Lb_Oficina.Text = Module1.PartysOpe[0].NombreUsado;
                //Se bloquea L_Cuentas
                Frm_Destino_Fondos.L_Cuentas.Enabled = false;
            }
            else if (_switchVar1 == T_MODGCON0.IdCta_ONMN || _switchVar1 == T_MODGCON0.IdCta_ONME)
            {
                //Otro Nemónico M/N ---- Otro Nemónico M/E
                for (j = 0; j <= 0; j++)
                {
                    Frm_Destino_Fondos.Tx_Datos[j].Text = "";
                    Frm_Destino_Fondos.Tx_Datos[j].Visible = true;
                    Frm_Destino_Fondos.Lb_Datos[j].Visible = true;
                }

                Frm_Destino_Fondos.Lb_Datos[0].Text = "Nemónico";
                Frm_Destino_Fondos.Tx_Datos[0].MaxLength = 15;
                Frm_Destino_Fondos.Lb_Oficina.Visible = true;
                Frm_Destino_Fondos.Lb_Oficina.Text = "";
                Frm_Destino_Fondos.BNem.Visible = true;
                //Se bloquea L_Cuentas
                Frm_Destino_Fondos.L_Cuentas.Enabled = false;
            }
            else if (_switchVar1 == T_MODGCON0.IdCta_CTAPTEMN || _switchVar1 == T_MODGCON0.IdCta_CTAPTEME)
            {
                //Cuenta Puente M/N -- Cheque Puente M/E
                Frm_Destino_Fondos.L_Partys.Enabled = true;
                Pr_Cargar_Beneficiario(initObject, unit); //Carga los Participantes
                //Se bloquea L_Cuentas
                Frm_Destino_Fondos.L_Cuentas.Enabled = false;
            }
            else if (_switchVar1 == T_MODGCON0.IdCta_CtaCteMANN || _switchVar1 == T_MODGCON0.IdCta_CtaCteAUTN)
            {
                //Cuenta Corriente Cosmos M/N.
                if (MODXORI.gb_esCosmos == true)
                {
                    Frm_Destino_Fondos.frm_datos.Visible = true;
                }
                Frm_Destino_Fondos.L_Partys.Enabled = true;
                Frm_Destino_Fondos.L_Cuentas.Enabled = true;
                Frm_Destino_Fondos.Ok_Partys.Enabled = true;
                Frm_Destino_Fondos.cmb_codtran.Enabled = true;
                Frm_Destino_Fondos.cmb_codtran.Visible = true;
                Frm_Destino_Fondos.Tx_Cuentas[0].Visible = true;

                for (j = 0; j <= 3; j++)
                {
                    Frm_Destino_Fondos.Text1[j].Text = "";
                }

                //Obtiene Moneda Swift para consulta
                for (c = 0; c <= (short)VB6Helpers.UBound(MODGTAB0.VMnd); c++)
                {
                    if (MODGPYF1.Minuscula(MODGTAB0.VMnd[c].Mnd_MndNom) == MODGPYF1.Minuscula(Frm_Destino_Fondos.L_Mnd.Text))
                    {
                        MODXVIA.Moneda_TRX = VB6Helpers.Trim(MODGTAB0.VMnd[c].Mnd_MndSwf);
                    }

                }

                Cargar_CodTran(initObject, "CTA-CTE", MODXVIA.Moneda_TRX, "CR");
                Pr_Cargar_Beneficiario(initObject, unit); //Carga los Participantes
                                                          //            Me.Text1(0).Text = PartysOpe(Me.L_Partys.ListIndex).NombreUsado

                if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
                {
                    Frm_Destino_Fondos.frm_datos.Enabled = false;
                }
                else
                {
                    //---------------------------------------------
                    //Realsystems-Código Nuevo-Inicio
                    //Fecha Modificación 20100623
                    //Responsable: Pablo Millan
                    //Versión: 1.0
                    //Descripción : Se modifica generacion de CRN
                    //---------------------------------------------
                    Frm_Destino_Fondos.Text1[4].Text = VB6Helpers.Mid(Mdl_Funciones_Varias.LC_TRXID_MAN, 16, 10);
                    // Contract reference number
                    //----------------------------------------
                    // RealSystems - Código Nuevo - Termino
                    //----------------------------------------
                    // RealSystems - Código Antiguo - Inicio
                    //----------------------------------------
                    //Text1(4) = Mid$(VgCvd.OpeSin, 6, 2) & Mid$(VgCvd.OpeSin, 8, 3) & Mid$(VgCvd.OpeSin, 11, 5)      ' Contract reference number
                    //----------------------------------------
                    // RealSystems - Código Antiguo - Termino
                    //----------------------------------------
                }

                Frm_Destino_Fondos.cmb_codtran.ListIndex = -1;
                cmb_codtran_Click(initObject);
            }
            else if (_switchVar1 == T_MODGCON0.IdCta_CtaCteMANE || _switchVar1 == T_MODGCON0.IdCta_CtaCteAUTE)
            {
                //Cuenta Corriente Cosmos M/E.
                if (MODXORI.gb_esCosmos == true)
                {
                    Frm_Destino_Fondos.frm_datos.Visible = true;
                }
                Frm_Destino_Fondos.L_Partys.Enabled = true;
                Frm_Destino_Fondos.L_Cuentas.Enabled = true;
                Frm_Destino_Fondos.Ok_Partys.Enabled = true;
                Frm_Destino_Fondos.cmb_codtran.Enabled = true;
                Frm_Destino_Fondos.cmb_codtran.Visible = true;
                Frm_Destino_Fondos.Tx_Cuentas[0].Visible = true;

                //Obtiene Moneda Swift para consulta
                for (c = 0; c <= (short)VB6Helpers.UBound(MODGTAB0.VMnd); c++)
                {
                    if (MODGPYF1.Minuscula(MODGTAB0.VMnd[c].Mnd_MndNom) == MODGPYF1.Minuscula(Frm_Destino_Fondos.L_Mnd.Text))
                    {
                        MODXVIA.Moneda_TRX = VB6Helpers.Trim(MODGTAB0.VMnd[c].Mnd_MndSwf);
                    }

                }

                Cargar_CodTran(initObject, "CTA-CTE", MODXVIA.Moneda_TRX, "CR");
                Pr_Cargar_Beneficiario(initObject, unit); //Carga los Participantes
                if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
                {
                    Frm_Destino_Fondos.frm_datos.Enabled = false;
                }
                else
                {
                    for (j = 0; j <= 3; j++)
                    {
                        Frm_Destino_Fondos.Text1[j].Text = "";
                    }

                }

                Frm_Destino_Fondos.cmb_codtran.ListIndex = -1;
                cmb_codtran_Click(initObject);
            }
            else if (_switchVar1 == T_MODGCON0.IdCta_GAPMN || _switchVar1 == T_MODGCON0.IdCta_GAPME)
            {
                if (MODXORI.gb_esCosmos == true)
                {
                    Frm_Destino_Fondos.frm_infoctagap.Visible = true;
                    Frm_Destino_Fondos.L_Cuentas.Visible = false;
                    Frm_Destino_Fondos.Cb_Destino.Visible = false;
                }

                //Obtiene Moneda Swift para consulta
                for (c = 0; c <= (short)VB6Helpers.UBound(MODGTAB0.VMnd); c++)
                {
                    if (MODGPYF1.Minuscula(MODGTAB0.VMnd[c].Mnd_MndNom) == MODGPYF1.Minuscula(Frm_Destino_Fondos.L_Mnd.Text))
                    {
                        MODXVIA.Moneda_TRX = VB6Helpers.Trim(MODGTAB0.VMnd[c].Mnd_MndSwf);
                    }

                }

                Frm_Destino_Fondos.L_Partys.Enabled = true;
                Frm_Destino_Fondos.L_Cuentas.Enabled = true;
                Frm_Destino_Fondos.Ok_Partys.Enabled = true;
                Frm_Destino_Fondos.cmb_codtran.Enabled = true;
                Frm_Destino_Fondos.cmb_codtran.Visible = true;
                Frm_Destino_Fondos.Tx_Cuentas[0].Visible = true;
                Cargar_CodTran(initObject, "GAP", "MM", "CR");
                Pr_Cargar_Beneficiario(initObject, unit); //Carga los Participantes
                if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
                {
                    Frm_Destino_Fondos.frm_datos.Enabled = false;
                }
                else
                {
                    //---------------------------------------------
                    //Realsystems-Código Nuevo-Inicio
                    //Fecha Modificación 20100623
                    //Responsable: Pablo Millan
                    //Versión: 1.0
                    //Descripción : Se modifica generacion de CRN
                    //---------------------------------------------
                    Frm_Destino_Fondos.Text1[4].Text = VB6Helpers.Mid(Mdl_Funciones_Varias.LC_TRXID_MAN, 16, 10);
                    // Contract reference number
                    //----------------------------------------
                    // RealSystems - Código Nuevo - Termino
                    //----------------------------------------
                    // RealSystems - Código Antiguo - Inicio
                    //----------------------------------------
                    //Me.Text1(4) = Mid$(VgCvd.OpeSin, 6, 2) & Mid$(VgCvd.OpeSin, 8, 3) & Mid$(VgCvd.OpeSin, 11, 5)      ' Contract reference number
                    //----------------------------------------
                    // RealSystems - Código Antiguo - Termino
                    //----------------------------------------
                    Frm_Destino_Fondos.txt_CRN.Text = Frm_Destino_Fondos.Text1[4].Text;
                }

                Frm_Destino_Fondos.cmb_codtran.ListIndex = -1;
                cmb_codtran_Click(initObject);
            }

        }
        public static void BNem_Click(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObj.Frm_Destino_Fondos;
            T_MODXORI MODXORI = initObj.MODXORI;
            T_MODCONT MODCONT = initObj.MODCONT;
            T_MODGNCTA MODGNCTA = initObj.MODGNCTA;

            if (!initObj.Frm_Destino_Fondos.VuelveDeNemonico)
            {
                short m = 0;
                if (Frm_Destino_Fondos.L_Mnd.ListIndex == -1)
                {
                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Debe seleccionar el monto para el cual se determinarán los O. de Fondo"
                    });
                    return;
                }

                m = (short)(Frm_Destino_Fondos.L_Mnd.get_ItemData_((short)Frm_Destino_Fondos.L_Mnd.ListIndex));
                if (m == 1)
                {
                    MODCONT.VCtaGl.moncta = 2;
                }
                else
                {
                    MODCONT.VCtaGl.moncta = 1;
                }

                // se cambia la ayuda de cuentas para que muestre
                // todas las cuentas disponibles ( rto )
                //usa el mismo procedimiento que en origenes
                if (~BCH.Comex.Core.BL.XCFT.Modulos.MODCONT.SyGetn_CtaCtb(initObj, unit) != 0)
                {
                    return;
                }
                MODCONT.VCtaGl.NemCta = "";
                initObj.FormularioQueAbrir = "NemonicoCuenta";
                initObj.Frm_Cta = new UI_Frm_Cta();
                initObj.Frm_Cta.VieneDe = "DestinoFondos";
                initObj.VieneDe = "DestinoFondos";
                initObj.Frm_Destino_Fondos.VuelveDeNemonico = true;
                return;
            }
            else
            {
                initObj.Frm_Destino_Fondos.VuelveDeNemonico = false;
                short b = 0;
                // Este bloque proviene del formulario FrmxOri que no es valido
                // en destino de fondos para ayuda de cuentas
                if (!string.IsNullOrWhiteSpace(MODCONT.VCtaGl.NemCta))
                {
                    Frm_Destino_Fondos.Tx_Datos[0].Text = VB6Helpers.Trim(MODCONT.VCtaGl.NemCta);
                    //Otro Nemónico M/N --- Otro Nemónico M/E.
                    if ((MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_ONMN) || (MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_ONME))
                    {
                        //Otro Nemónico M/N ---- Otro Nemónico M/E
                        b = (short)BCH.Comex.Core.BL.XCFT.Modulos.MODGNCTA.Get_Cta(VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[0].Text), initObj, unit);
                        if (b == -1)
                        {
                            Frm_Destino_Fondos.Lb_Oficina.Text = "";
                            //Validación Incorrecta
                            short _switchVar1 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                            if (_switchVar1 == T_MODGCON0.IdCta_ONMN)
                            {
                                Frm_Destino_Fondos.ONMN_OK = "00";
                            }
                            else if (_switchVar1 == T_MODGCON0.IdCta_ONME)
                            {
                                Frm_Destino_Fondos.ONME_OK = "00";
                            }

                        }
                        else
                        {
                            short _switchVar2 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                            if (_switchVar2 == T_MODGCON0.IdCta_ONMN)
                            {
                                if (MODGNCTA.VCta[b].Cta_Mon == 1)
                                {
                                    Frm_Destino_Fondos.Lb_Oficina.Text = "";
                                    //Validación Incorrecta
                                    short _switchVar3 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                                    if (_switchVar3 == T_MODGCON0.IdCta_ONMN)
                                    {
                                        Frm_Destino_Fondos.ONMN_OK = "00";
                                    }
                                    else if (_switchVar3 == T_MODGCON0.IdCta_ONME)
                                    {
                                        Frm_Destino_Fondos.ONME_OK = "00";
                                    }

                                }
                                else
                                {
                                    Frm_Destino_Fondos.Lb_Oficina.Text = MODGPYF1.Minuscula(VB6Helpers.Trim(MODGNCTA.VCta[b].Cta_Nom.Value));
                                    //Validación Correcta
                                    short _switchVar4 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                                    if (_switchVar4 == T_MODGCON0.IdCta_ONMN)
                                    {
                                        Frm_Destino_Fondos.ONMN_OK = "01";
                                    }
                                    else if (_switchVar4 == T_MODGCON0.IdCta_ONME)
                                    {
                                        Frm_Destino_Fondos.ONME_OK = "01";
                                    }

                                }

                            }
                            else if (_switchVar2 == T_MODGCON0.IdCta_ONME)
                            {
                                if (MODGNCTA.VCta[b].Cta_Mon == 2)
                                {
                                    Frm_Destino_Fondos.Lb_Oficina.Text = "";
                                    //Validación Incorrecta
                                    short _switchVar5 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                                    if (_switchVar5 == T_MODGCON0.IdCta_ONMN)
                                    {
                                        Frm_Destino_Fondos.ONMN_OK = "00";
                                    }
                                    else if (_switchVar5 == T_MODGCON0.IdCta_ONME)
                                    {
                                        Frm_Destino_Fondos.ONME_OK = "00";
                                    }

                                }
                                else
                                {
                                    Frm_Destino_Fondos.Lb_Oficina.Text = MODGPYF1.Minuscula(VB6Helpers.Trim(MODGNCTA.VCta[b].Cta_Nom.Value));
                                    //Validación Correcta
                                    short _switchVar6 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                                    if (_switchVar6 == T_MODGCON0.IdCta_ONMN)
                                    {
                                        Frm_Destino_Fondos.ONMN_OK = "01";
                                    }
                                    else if (_switchVar6 == T_MODGCON0.IdCta_ONME)
                                    {
                                        Frm_Destino_Fondos.ONME_OK = "01";
                                    }

                                }

                            }

                            // Si la cuenta es vigenteable se permite el ingreso del numero de referencia
                            // de lo contrario debe limpiar y ocultar numero de partida
                            if ((MODGNCTA.VCta[b].Cta_Vig == 1))
                            {
                                Frm_Destino_Fondos.LB_Referencia.Visible = true;
                                Frm_Destino_Fondos.txtNumRef.Visible = true;
                            }
                            else
                            {
                                LimpiaNumPartida(initObj);
                            }

                        }

                    }

                }
                else
                {
                    if (Frm_Destino_Fondos.Tx_Datos[0].Enabled)
                    {

                    }
                }
            }
        }
        public static void Boton_Click(InitializationObject initObject, short Index)
        {
            T_ModChVrf ModChVrf = initObject.ModChVrf;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODXORI MODXORI = initObject.MODXORI;
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;

            short a = 0;
            short HayChVrf = 0;
            short i = 0;
            short HayPlnChv = 0;

            a = (short)VB6Helpers.UBound(ModChVrf.VPlnChV);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            switch (Index)
            {
                case 0:
                    //Ve si se han justificado los montos.-
                    if (~Fn_ViasOK(initObject) != 0)
                    {
                        Frm_Destino_Fondos.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Los montos NO han sido correctamente justificados. Debe asociar cada uno de ellos a una forma de pago."
                        });
                        Frm_Destino_Fondos.l_via.ListIndex = -1;
                        return;
                    }

                    HayChVrf = (short)(false ? -1 : 0);
                    for (i = 0; i <= (short)VB6Helpers.UBound(MODXVIA.VxVia); i++)
                    {
                        if ((((MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CHVRF || MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_OPC || MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_OPOP || MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CHMEBCH) ? -1 : 0) & MODXVIA.VxVia[i].GenPln) != 0)
                        {
                            HayChVrf = (short)(true ? -1 : 0);
                            break;
                        }

                    }

                    HayPlnChv = (short)(false ? -1 : 0);
                    if (a > 0)
                    {
                        HayPlnChv = (short)(true ? -1 : 0);
                    }

                    if ((HayChVrf & ~ModChVrf.AceptoPantallaChVrf & ~HayPlnChv) != 0)
                    {
                        Frm_Destino_Fondos.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Debe definir las planillas para la cuenta Check Verification."
                        });
                        return;
                    }

                    MODXVIA.VgxVia.Acepto = (short)(true ? -1 : 0);

                    if (MODXVIA.EsSolBcx != 0)
                    {
                        BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.CambiaBcxEntrada(initObject, "V");
                    }

                    //// UPGRADE_INFO (#0181): Reference to default form instance 'Frm_Destino_Fondos' was converted to Me/this keyword.
                    //VB6Helpers.Unload(this);


                    //si en este momento vuelve de otro solo puede ser de Vueltos
                    Frm_Destino_Fondos.VuelveDeOtro = false;
                    if (initObject.VieneDe == "Vueltos")
                    {
                        Frm_Destino_Fondos.VuelveDeOtro = true;
                    }
                    break;
                case 1:
                    // UPGRADE_INFO (#0181): Reference to default form instance 'Frm_Destino_Fondos' was converted to Me/this keyword.
                    //VB6Helpers.Unload(this);
                    //Si Cancela Vuelto  => Elimina sólo los Vueltos.-

                    if (MODXVIA.VgxVia.Vuelto != 0)
                    {
                        for (i = 0; i <= (short)VB6Helpers.UBound(MODXVIA.VxVia); i++)
                        {
                            if (MODXVIA.VxVia[i].Vuelto != 0)
                            {
                                MODXVIA.VxVia[i].Status = T_MODXVIA.ExVia_Eli;
                            }

                        }

                    }
                    else
                    {
                        //Si Cancela Vías    => Elimina todo.-
                        //VB6Helpers.Redim(ref MODXVIA.VxVia, 0, 0);
                        //MODXORI.Vx_SCodTran = new S_Codtran[1];
                        MODXVIA.VxVia = new T_xVia[0];
                        MODXORI.Vx_SCodTran = new S_Codtran[0];
                    }

                    ModChVrf.VgChV = new T_Chv[0];
                    ModChVrf.VPlnChV = new T_PlnChV[0];
                    Frm_Destino_Fondos.ONMN_OK = "";
                    initObject.VieneDe = string.Empty;
                    break;
            }
            initObject.Frm_Destino_Fondos = null;
            initObject.FormularioQueAbrir = "Index";


        }
        public static void Bt_PlnTrn_Click(InitializationObject initObject)
        {
            short k = 0;
            k = (short)VB6Helpers.UBound(initObject.ModChVrf.VgChV);
            if (k >= 0)
            {
                initObject.ModChVrf.CodigoIE = "E";
                initObject.FormularioQueAbrir = "PlanillasTransferencia";
                initObject.Frm_Destino_Fondos.VuelveDeOtro = true;
                initObject.VieneDe = "DestinoFondos";
            }
        }
        public static void Cb_Destino_Click(InitializationObject initObject)
        {
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;
            if (Frm_Destino_Fondos.Cb_Destino.ListIndex == -1)
            {
                return;
            }
            Frm_Destino_Fondos.Indice_Destino = (short)(Frm_Destino_Fondos.Cb_Destino.get_ItemData_(Frm_Destino_Fondos.Cb_Destino.ListIndex));
        }
        public static void cmb_codtran_Click(InitializationObject initObject)
        {
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;
            initObject.Frm_Destino_Fondos.Boton[0].Enabled = false;
            string Moneda_TRX = "";
            short c = 0;

            //Obtiene Moneda Swift para consulta
            for (c = 0; c <= (short)VB6Helpers.UBound(MODGTAB0.VMnd); c++)
            {
                if (MODGPYF1.Minuscula(MODGTAB0.VMnd[c].Mnd_MndNom) == MODGPYF1.Minuscula(Frm_Destino_Fondos.L_Mnd.Text))
                {
                    Moneda_TRX = VB6Helpers.Trim(MODGTAB0.VMnd[c].Mnd_MndSwf);
                    break;
                }
            }
        }
        public static void L_Cuentas_Click(InitializationObject initObject)
        {
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;
            if (Frm_Destino_Fondos.L_Cuentas.ListIndex != -1)
            {
                Frm_Destino_Fondos.Indice_CtaCte = (short)(Frm_Destino_Fondos.L_Cuentas.get_ItemData_(Frm_Destino_Fondos.L_Cuentas.ListIndex));
            }
            else
            {
                return;
            }

        }
        public static void l_mnd_Click(InitializationObject initObject, UnitOfWorkCext01 unit, bool limpiaVia = true)
        {
            UI_Frm_Destino_Fondos Frm_destino_Fondos = initObject.Frm_Destino_Fondos;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            int anterior;

            short i = (short)(Frm_destino_Fondos.L_Mnd.get_ItemData_(Frm_destino_Fondos.L_Mnd.ListIndex));
            short m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, i);
            short k = 0;
            short n = 0;
            //Valida Decimales de las Monedas.-
            if (MODGTAB0.VMnd[m].Mnd_MndSin != 0)
            {
                Frm_destino_Fondos.MtoVia.Tag = "_____________";
            }
            else
            {
                Frm_destino_Fondos.MtoVia.Tag = "_____________.__";
            }

            //Carga las Vías de acuerdo a la moneda.
            if (Frm_destino_Fondos.L_Mnd.get_ItemData_(Frm_destino_Fondos.L_Mnd.ListIndex) == T_MODGTAB0.MndNac)
            {
                MODXORI.Carga_l_cta(initObject, Frm_destino_Fondos.L_Cta, 1, false, true, -1, unit);
            }
            else
            {
                MODXORI.Carga_l_cta(initObject, Frm_destino_Fondos.L_Cta, 2, false, true, -1, unit);
            }
            L_Cta_Click(initObject, unit);
            //Se posiciona en el monto con la moneda actual.
            for (k = 0; k <= (short)(Frm_destino_Fondos.l_mto.Items.Count - 1); k++)
            {
                n = short.Parse(Frm_destino_Fondos.l_mto.get_ItemData(k));
                if (MODXVIA.VxMtoVia[n].CodMon == (Frm_destino_Fondos.L_Mnd.get_ItemData_(Frm_destino_Fondos.L_Mnd.ListIndex)))
                {
                    anterior = Frm_destino_Fondos.l_mto.ListIndex;
                    Frm_destino_Fondos.l_mto.ListIndex = k;
                    l_mto_Click(initObject, unit, limpiaVia);
                    break;
                }

            }

        }
        public static void l_mto_Click(InitializationObject initObject, UnitOfWorkCext01 unit, bool limpiaVia = true)
        {
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            int anterior;
            short i = 0;
            short j = 0;
            double Saldo = 0;
            if (Frm_Destino_Fondos.l_mto.ListIndex == -1)
            {
                return;
            }

            i = (short)Frm_Destino_Fondos.l_mto.ListIndex;
            j = short.Parse(Frm_Destino_Fondos.l_mto.get_ItemData(i));
            Frm_Destino_Fondos.Indice_Monto = short.Parse(Frm_Destino_Fondos.l_mto.get_ItemData(Frm_Destino_Fondos.l_mto.ListIndex));
            Saldo = MODXVIA.VxMtoVia[j].MtoTot - Fn_SumaVxVia(initObject, MODXVIA.VxMtoVia[j].CodMon, MODXVIA.VgxVia.Vuelto);
            if (Saldo >= 0)
            {
                if (limpiaVia)
                {
                    anterior = Frm_Destino_Fondos.l_via.ListIndex;
                    Frm_Destino_Fondos.l_via.ListIndex = -1;
                    if (anterior != Frm_Destino_Fondos.l_via.ListIndex)
                    {
                        l_via_Click(initObject, unit);
                    }
                }
                Frm_Destino_Fondos.MtoVia.Text = Format.FormatCurrency((Saldo), MODGPYF1.DecObjeto(Frm_Destino_Fondos.MtoVia));
            }
            anterior = Frm_Destino_Fondos.L_Mnd.ListIndex;
            Frm_Destino_Fondos.L_Mnd.ListIndex = Frm_Destino_Fondos.L_Mnd.Items.FindIndex(z => z.Data == MODXVIA.VxMtoVia[j].CodMon);
            if (anterior != Frm_Destino_Fondos.L_Mnd.ListIndex)
            {
                l_mnd_Click(initObject, unit);
            }
        }
        public static void L_Partys_Click(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;
            T_MODXORI MODXORI = initObject.MODXORI;
            if (Frm_Destino_Fondos.L_Partys.ListIndex == -1)
            {
                return;
            }
            MODXORI.Indice_Partys = (short)(Frm_Destino_Fondos.L_Partys.get_ItemData_(Frm_Destino_Fondos.L_Partys.ListIndex));
            Ok_Partys_Click(initObject, unit);

            if (Frm_Destino_Fondos.frm_datos.Visible == true)
            {
                //---------------------------------------------
                //Realsystems-Código Nuevo-Inicio
                //Fecha Modificación 20100615
                //Responsable: Pablo Millan.
                //Versión: 1.0
                //Descripción : Se omite precarga de cliente
                //---------------------------------------------
                Frm_Destino_Fondos.Text1[0].Text = "";
                //----------------------------------------
                // RealSystems - Código Nuevo - Termino
                //----------------------------------------
                // RealSystems - Código Antiguo - Inicio
                //----------------------------------------
                //Text1(0).Text = PartysOpe(Me.L_Partys.ListIndex).NombreUsado
                //Text1(1).Text = ""
                //Text1(2).Text = ""
                //Text1(3).Text = ""
                //----------------------------------------
                // RealSystems - Código Antiguo - Termino
                //----------------------------------------
            }

        }
        public static void l_via_Click(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODXORI MODXORI = initObject.MODXORI;

            short i = 0;
            short j = 0;
            short k = 0;
            short n = 0;
            double Saldo = 0;
            string razon = "";
            short x = 0;
            short Y = 0;
            if (Frm_Destino_Fondos.l_via.ListIndex == -1)
            {
                return;
            }

            i = (short)Frm_Destino_Fondos.l_via.ListIndex;
            j = short.Parse(Frm_Destino_Fondos.l_via.get_ItemData(i));

            //Despliega los datos de las Vias.
            if (j == -1)
            {
                //Busca el saldo de alguna moneda de la lista de montos.
                for (k = 0; k <= (short)(Frm_Destino_Fondos.l_mto.Items.Count - 1); k++)
                {
                    n = short.Parse(Frm_Destino_Fondos.l_mto.get_ItemData(k));
                    Saldo = MODXVIA.VxMtoVia[n].MtoTot - Fn_SumaVxVia(initObject, MODXVIA.VxMtoVia[n].CodMon, MODXVIA.VgxVia.Vuelto);
                    if (Saldo > 0)
                    {
                        Frm_Destino_Fondos.L_Mnd.ListIndex = Frm_Destino_Fondos.L_Mnd.Items.FindIndex(m => m.Data == MODXVIA.VxMtoVia[n].CodMon);
                        l_mnd_Click(initObject, unit);
                        Frm_Destino_Fondos.MtoVia.Text = Format.FormatCurrency((Saldo), MODGPYF1.DecObjeto(Frm_Destino_Fondos.MtoVia));
                        Frm_Destino_Fondos.Cb_Destino.ListIndex = MODGPYF0.PosLista(Frm_Destino_Fondos.Cb_Destino, MODXVIA.VgxVia.destino);
                        if (Frm_Destino_Fondos.Cb_Destino.ListIndex == -1)
                        {
                            Frm_Destino_Fondos.Cb_Destino.ListIndex = 0;
                        }
                        Cb_Destino_Click(initObject);
                        break;
                    }
                    else
                    {
                        Frm_Destino_Fondos.MtoVia.Text = Format.FormatCurrency(0, MODGPYF1.DecObjeto(Frm_Destino_Fondos.MtoVia));
                    }

                }

                Frm_Destino_Fondos.NO.Enabled = false;
                Frm_Destino_Fondos.L_Cta.ListIndex = 0;
                L_Cta_Click(initObject, unit);
            }
            else
            {
                //Se posiciona en la lista de monedas.
                Frm_Destino_Fondos.L_Mnd.ListIndex = Frm_Destino_Fondos.L_Mnd.Items.FindIndex(m => m.Data == MODXVIA.VxVia[j].CodMon);

                //Se deben cargar las cuentas correspondientes a la moneda de la via seleccionada
                l_mnd_Click(initObject, unit, false);

                //Se posiciona en el origen actual.
                for (k = 0; k <= (short)(Frm_Destino_Fondos.L_Cta.Items.Count - 1); k++)
                {
                    if (MODXORI.VOvd[(Frm_Destino_Fondos.L_Cta.get_ItemData_(k))].NumCta == MODXVIA.VxVia[j].NumCta)
                    {
                        if (Frm_Destino_Fondos.L_Cta.ListIndex != k)
                        {
                            Frm_Destino_Fondos.L_Cta.ListIndex = k;
                            L_Cta_Click(initObject, unit);
                        }
                        break;
                    }

                }

                Frm_Destino_Fondos.MtoVia.Text = Format.FormatCurrency((MODXVIA.VxVia[j].MtoTot), MODGPYF1.DecObjeto(Frm_Destino_Fondos.MtoVia));

                //Carga los Datos Sobre Impuesto al Cheque.-
                if (MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_CHMEBCH)
                {
                    if (MODXVIA.VxVia[j].ImpChq != 0)
                    {
                        Frm_Destino_Fondos.Ch_ImpChq.Value = -1;
                    }
                    else
                    {
                        Frm_Destino_Fondos.Ch_ImpChq.Value = 0;
                    }

                    Frm_Destino_Fondos.Ch_ImpChq.Enabled = true;
                }
                else
                {
                    Frm_Destino_Fondos.Ch_ImpChq.Enabled = false;
                    Frm_Destino_Fondos.Ch_ImpChq.Value = 0;
                }

                short _switchVar1 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;

                //Cuenta Corriente M/N. -- Cuenta Corriente M/E.
                if (_switchVar1 == T_MODGCON0.IdCta_CtaCteMN || _switchVar1 == T_MODGCON0.IdCta_CtaCteME || _switchVar1 == T_MODGCON0.IdCta_ChqCCME)
                {
                    Ok_Partys_Click(initObject, unit);
                    Frm_Destino_Fondos.L_Partys.ListIndex = Frm_Destino_Fondos.L_Partys.Items.FindIndex(m => m.Data == MODXVIA.VxVia[j].PosPrty);
                    Frm_Destino_Fondos.L_Cuentas.ListIndex = Frm_Destino_Fondos.L_Cuentas.Items.FindIndex(m => !String.IsNullOrEmpty(m.Value) && m.Value.Trim().Equals(MODXVIA.VxVia[j].CtaCte_t));

                    //Saldos c/ Sucursales M/N.
                }
                else if (_switchVar1 == T_MODGCON0.IdCta_SCSMN || _switchVar1 == T_MODGCON0.IdCta_SCSME)
                {
                    Frm_Destino_Fondos.L_Partys.ListIndex = Frm_Destino_Fondos.L_Partys.Items.FindIndex(m => m.Data == MODXVIA.VxVia[j].PosPrty);
                    Frm_Destino_Fondos.Tx_Datos[0].Text = VB6Helpers.Trim(VB6Helpers.Str(MODXVIA.VxVia[j].codofi));
                    Frm_Destino_Fondos.Lb_Oficina.Text = BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_Buscar_Suc(initObject, VB6Helpers.Trim(VB6Helpers.Str(MODXVIA.VxVia[j].codofi)));
                    Frm_Destino_Fondos.Tx_Datos[1].Text = VB6Helpers.Trim(VB6Helpers.Str(MODXVIA.VxVia[j].TipMov));
                    Frm_Destino_Fondos.Tx_Datos[2].Text = VB6Helpers.Trim(VB6Helpers.Str(MODXVIA.VxVia[j].NumPar));

                    //Cta. Cte. Banco Central -- Corresponsal cuenta ordinaria -- Divisas Pendientes
                }
                else if (_switchVar1 == T_MODGCON0.IdCta_CTACTEBC || _switchVar1 == T_MODGCON0.IdCta_CTAORD || _switchVar1 == T_MODGCON0.IdCta_DIVENPEN || _switchVar1 == T_MODGCON0.IdCta_CHVBNYM || _switchVar1 == 54)
                {
                    Frm_Destino_Fondos.L_Partys.ListIndex = Frm_Destino_Fondos.L_Partys.Items.FindIndex(m => m.Data == MODXVIA.VxVia[j].PosPrty);
                    Frm_Destino_Fondos.Tx_Datos[0].Text = VB6Helpers.Trim(MODXVIA.VxVia[j].CodSwf);
                    Frm_Destino_Fondos.Tx_Datos[1].Text = VB6Helpers.Trim(MODXVIA.VxVia[j].numdoc);

                    //Cuentas de Obligaciones y Check Verification
                }
                else if (_switchVar1 >= 40 && _switchVar1 <= 53)
                {
                    Frm_Destino_Fondos.L_Partys.ListIndex = Frm_Destino_Fondos.L_Partys.Items.FindIndex(m => m.Data == MODXVIA.VxVia[j].PosPrty);
                    Frm_Destino_Fondos.Tx_Datos[0].Text = VB6Helpers.Trim(MODXVIA.VxVia[j].CodSwf);
                    Frm_Destino_Fondos.Tx_Datos[1].Text = VB6Helpers.Trim(MODXVIA.VxVia[j].numdoc);
                }
                else if (_switchVar1 == T_MODGCON0.IdCta_BOEREG || _switchVar1 == T_MODGCON0.IdCta_CHEREG || _switchVar1 == T_MODGCON0.IdCta_OBLREG || _switchVar1 == T_MODGCON0.IdCta_OBLARE || _switchVar1 == T_MODGCON0.IdCta_ACEREG)
                {
                    Frm_Destino_Fondos.L_Partys.ListIndex = Frm_Destino_Fondos.L_Partys.Items.FindIndex(m => m.Data == MODXVIA.VxVia[j].PosPrty);
                    Frm_Destino_Fondos.Tx_Datos[0].Text = VB6Helpers.Trim(MODXVIA.VxVia[j].CodSwf);
                    Frm_Destino_Fondos.Tx_Datos[1].Text = VB6Helpers.Trim(MODXVIA.VxVia[j].numdoc);
                    //Varios Acreedores Import. -- Varios Acreedores Export. -- 'Varios Acreedores Mcdo. Corr.
                }
                else if (_switchVar1 == T_MODGCON0.IdCta_VAM || _switchVar1 == T_MODGCON0.IdCta_VAX || _switchVar1 == T_MODGCON0.IdCta_VAMC || _switchVar1 == T_MODGCON0.IdCta_VAMCC || _switchVar1 == T_MODGCON0.IdCta_VASC)
                {

                    Frm_Destino_Fondos.Tx_Datos[0].Text = VB6Helpers.Trim(MODXVIA.VxVia[j].IdPrty);
                    razon = BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.SyGet_Partys(unit, VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[0].Text));
                    Frm_Destino_Fondos.Lb_Oficina.Text = MODGPYF1.Minuscula(VB6Helpers.Trim(razon));
                    if (string.IsNullOrWhiteSpace(razon))
                    {
                        Frm_Destino_Fondos.Lb_Oficina.Text = "";
                    }

                    //Otro Nemónico M/N ---- Otro Nemónico M/E
                }
                else if (_switchVar1 == T_MODGCON0.IdCta_ONMN || _switchVar1 == T_MODGCON0.IdCta_ONME)
                {
                    Frm_Destino_Fondos.Tx_Datos[0].Text = VB6Helpers.Trim(MODXVIA.VxVia[j].NemCta);
                    // Al editar si existe numero de partida debe habilitar los campos.
                    if ((MODXVIA.VxVia[j].NumPar > 0))
                    {
                        Frm_Destino_Fondos.txtNumRef.Text = VB6Helpers.Trim(VB6Helpers.CStr(MODXVIA.VxVia[j].NumPar));
                        Frm_Destino_Fondos.txtNumRef.Visible = true;
                        Frm_Destino_Fondos.LB_Referencia.Visible = true;
                    }

                    //Cuenta Puente M/N -- Cheque Puente M/E
                }
                else if (_switchVar1 == T_MODGCON0.IdCta_CTAPTEMN || _switchVar1 == T_MODGCON0.IdCta_CTAPTEME)
                {
                    Frm_Destino_Fondos.L_Partys.ListIndex = Frm_Destino_Fondos.L_Partys.Items.FindIndex(m => m.Data == MODXVIA.VxVia[j].PosPrty);
                }
                else if (_switchVar1 == T_MODGCON0.IdCta_CtaCteMANN || _switchVar1 == T_MODGCON0.IdCta_CtaCteMANE)
                {

                    Frm_Destino_Fondos.Text1[0].Text = VB6Helpers.Trim(MODXVIA.VxVia[j].Text1);
                    Frm_Destino_Fondos.Text1[1].Text = VB6Helpers.Trim(MODXVIA.VxVia[j].Text2);
                    Frm_Destino_Fondos.Text1[2].Text = VB6Helpers.Trim(MODXVIA.VxVia[j].Text3);
                    Frm_Destino_Fondos.Text1[3].Text = VB6Helpers.Trim(MODXVIA.VxVia[j].Text4);

                    try
                    {
                        for (Y = 0; Y <= (short)VB6Helpers.UBound(MODXORI.Vx_SCodTran); Y++)
                        {
                            if (MODXVIA.VxVia[j].IdCtran == MODXORI.Vx_SCodTran[Y].ID && MODXORI.Vx_SCodTran[Y].Via == "des")
                            {
                                break;
                            }

                        }

                        for (x = 0; x <= (short)(Frm_Destino_Fondos.cmb_codtran.Items.Count - 1); x++)
                        {
                            if (Frm_Destino_Fondos.cmb_codtran.get_ItemData_(x) == (MODXORI.Vx_SCodTran[Y].nro_trx))
                            {
                                if (Frm_Destino_Fondos.cmb_codtran.ListIndex != x)
                                {
                                    Frm_Destino_Fondos.cmb_codtran.ListIndex = x;
                                    cmb_codtran_Click(initObject);
                                }
                                break;
                            }

                        }
                    }
                    catch
                    {

                    }
                }
                else if (_switchVar1 == T_MODGCON0.IdCta_GAPMN || _switchVar1 == T_MODGCON0.IdCta_GAPME)
                {

                    for (Y = 0; Y <= (short)VB6Helpers.UBound(MODXORI.Vx_SCodTran); Y++)
                    {
                        if (MODXVIA.VxVia[j].IdCtran == MODXORI.Vx_SCodTran[Y].ID && MODXORI.Vx_SCodTran[Y].Via == "des")
                        {
                            break;
                        }

                    }

                    for (x = 0; x <= (short)(Frm_Destino_Fondos.cmb_codtran.Items.Count - 1); x++)
                    {
                        if (Frm_Destino_Fondos.cmb_codtran.get_ItemData_(x) == (MODXORI.Vx_SCodTran[Y].nro_trx))
                        {
                            Frm_Destino_Fondos.cmb_codtran.ListIndex = x;
                            break;
                        }

                    }

                }
                Frm_Destino_Fondos.Cb_Destino.ListIndex = Frm_Destino_Fondos.Cb_Destino.Items.FindIndex(m => m.Data == MODXVIA.VxVia[j].CodDme);
                Cb_Destino_Click(initObject);
                Frm_Destino_Fondos.NO.Enabled = true;
            }

            //Deshabilita algunos objetos.-
            if (j == -1 && Saldo == 0)
            {
                Frm_Destino_Fondos.L_Partys.ListIndex = -1;
                L_Partys_Click(initObject, unit);
                Frm_Destino_Fondos.L_Cuentas.ListIndex = -1;
                L_Cuentas_Click(initObject);
                Frm_Destino_Fondos.L_Cta.ListIndex = 0;
                L_Cta_Click(initObject, unit);
                Frm_Destino_Fondos.Cb_Destino.ListIndex = -1;
                Cb_Destino_Click(initObject);
            }

        }
        public static void NO_Click(InitializationObject initObject, UnitOfWorkCext01 unit, bool vieneDeMensaje, bool resMensaje)
        {
            T_ModChVrf ModChVrf = initObject.ModChVrf;
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODXORI MODXORI = initObject.MODXORI;

            short a = 0;
            short i = 0;
            short j = 0;
            short p = 0;
            short sw = 0;
            short f = 0;

            a = (short)VB6Helpers.UBound(ModChVrf.VPlnChV);
            i = (short)Frm_Destino_Fondos.l_via.ListIndex;
            j = short.Parse(Frm_Destino_Fondos.l_via.get_ItemData(i));

            if (j != -1)
            {
                if ((MODXVIA.VxVia[j].NumCta == T_MODGCON0.IdCta_CHVRF || MODXVIA.VxVia[j].NumCta == T_MODGCON0.IdCta_OPC || MODXVIA.VxVia[j].NumCta == T_MODGCON0.IdCta_OPOP || MODXVIA.VxVia[j].NumCta == T_MODGCON0.IdCta_CHMEBCH))
                {
                    if (a > 0)
                    {
                        if (!vieneDeMensaje)
                        {
                            Frm_Destino_Fondos.Confirms.Add(new UI_Message()
                            {
                                Type = TipoMensaje.YesNo,
                                Text = "La cuenta corresponsales ya generó planillas para el destino de los fondos. Si continua se eliminarán. ¿Desea continuar? "
                            });
                            return;
                        }
                        else if (resMensaje)
                        {
                            //ModChVrf.VPlnChV = new T_PlnChV[1];
                            ModChVrf.AceptoPantallaChVrf = (short)(false ? -1 : 0);
                        }
                        else
                        {
                            //CANCELAR
                            //L_Cta.SetFocus();
                            return;
                        }
                    }
                }
                MODXVIA.VxVia[j].Status = T_MODXVIA.ExVia_Eli;

                //se cambia estado para matriz Vx_SCodTran
                for (f = 0; f <= (short)VB6Helpers.UBound(MODXORI.Vx_SCodTran); f++)
                {
                    if (MODXVIA.VxVia[j].IdCtran == MODXORI.Vx_SCodTran[f].ID)
                    {
                        MODXORI.Vx_SCodTran[f].Estado = 3;  //registro eliminado
                        break;
                    }
                }

                if ((MODXVIA.VxVia[j].NumCta == T_MODGCON0.IdCta_CHVRF || MODXVIA.VxVia[j].NumCta == T_MODGCON0.IdCta_OPC || MODXVIA.VxVia[j].NumCta == T_MODGCON0.IdCta_OPOP || MODXVIA.VxVia[j].NumCta == T_MODGCON0.IdCta_CHMEBCH))
                {
                    BCH.Comex.Core.BL.XCFT.Modulos.ModChVrf.TraeDatosVias_ChVrf(initObject, j);
                }

                if (i == Frm_Destino_Fondos.l_via.Items.Count - 1)
                {
                    p = -1;
                }
                else
                {
                    p = i;
                }

                Frm_Destino_Fondos.l_via.Items.RemoveAt(i);
                Frm_Destino_Fondos.l_via.ListIndex = p;

                Pr_Llena_l_via(initObject, unit);
                Frm_Destino_Fondos.Boton[0].Enabled = false;

                sw = (short)(false ? -1 : 0);
                for (i = 0; i <= (short)VB6Helpers.UBound(MODXVIA.VxVia); i++)
                {
                    if ((MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CHVRF || MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_OPC || MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_OPOP || MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CHMEBCH) && MODXVIA.VxVia[i].Status != 3)
                    {
                        sw = (short)(true ? -1 : 0);
                        break;
                    }
                }
                // Al eliminar debe limpiar y ocultar objetos de numero de partida
                LimpiaNumPartida(initObject);
            }
        }
        public static void ok_Click(InitializationObject initObject, UnitOfWorkCext01 unit, bool vieneDeMensaje, bool resMensaje)
        {
            using (var tracer = new Tracer("ok_Click"))
            {

                UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;
                T_ModChVrf ModChVrf = initObject.ModChVrf;
                T_MODGPYF0 MODGPYF0 = initObject.MODGPYF0;
                T_MODXORI MODXORI = initObject.MODXORI;
                T_MODXVIA MODXVIA = initObject.MODXVIA;
                T_MODGCVD MODGCVD = initObject.MODGCVD;
                T_MODCVDIM MODCVDIM = initObject.MODCVDIM;
                T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
                T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;
                short a = 0;
                short LC = 0;
                short lm = 0;
                short idItemEditando = 0;
                short moneda = 0;
                double montoItemEditando = 0;
                short n = 0;
                short ln = 0;
                short i = 0;
                short s = 0;
                short lc_cuenta = 0;

                a = (short)VB6Helpers.UBound(ModChVrf.VPlnChV);

                //Si el monto a ingresar es cero => Se omite.-
                if (Format.StringToDouble((Frm_Destino_Fondos.MtoVia.Text)) == 0)
                {
                    return;
                }

                if (Frm_Destino_Fondos.L_Cta.ListIndex == -1)
                {
                    return;
                }
                // Si el objeto esta visible se debe validar que tenga informacion
                if ((Frm_Destino_Fondos.txtNumRef.Visible == true))
                {
                    if (string.IsNullOrWhiteSpace(Frm_Destino_Fondos.txtNumRef.Text))
                    {
                        Frm_Destino_Fondos.txtNumRef.Enabled = true;
                        Frm_Destino_Fondos.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Es necesario que se ingrese un Número de Partida para poder realizar la operación.",
                            ControlName = "txtNumRef"
                        });
                        return;
                    }
                }

                //Me aseguro que los campos campos estan en mayusculas
                foreach (var elm in Frm_Destino_Fondos.Tx_Datos)
                {
                    elm.Text = elm.Text == null ? elm.Text : elm.Text.ToUpper();
                }

                //Ingresa los datos en el arreglo.-
                LC = (short)(Frm_Destino_Fondos.L_Cta.get_ItemData_((short)Frm_Destino_Fondos.L_Cta.ListIndex));
                lm = short.Parse(Frm_Destino_Fondos.l_mto.get_ItemData((short)Frm_Destino_Fondos.l_mto.ListIndex));
                idItemEditando = short.Parse(Frm_Destino_Fondos.l_via.get_ItemData(Frm_Destino_Fondos.l_via.ListIndex));


                if (MODXORI.VOvd[LC].NumCta == T_MODGCON0.IdCta_CHVRF || MODXORI.VOvd[LC].NumCta == T_MODGCON0.IdCta_OPC || MODXORI.VOvd[LC].NumCta == T_MODGCON0.IdCta_OPOP || MODXORI.VOvd[LC].NumCta == T_MODGCON0.IdCta_CHMEBCH)
                {
                    if (a >= 0)
                    {
                        if (!vieneDeMensaje)
                        {
                            Frm_Destino_Fondos.Confirms.Add(new UI_Message()
                            {
                                Type = TipoMensaje.YesNo,
                                Text = "La cuenta Check Verification ya generó planillas para el destino de los fondos. Si continua tendrá que volver a definirlas ¿Desea continuar? "
                            });
                            return;
                        }
                        else if (resMensaje)
                        {
                            ModChVrf.VPlnChV = new T_PlnChV[0];
                        }
                        else
                        {
                            return;
                        }
                    }

                }

                moneda = (short)(Frm_Destino_Fondos.L_Mnd.get_ItemData_((short)Frm_Destino_Fondos.L_Mnd.ListIndex));


                if (idItemEditando > -1)
                {
                    UI_GridItem itemEditando = Frm_Destino_Fondos.l_via.Items.Where(xx => xx.ID == idItemEditando.ToString()).FirstOrDefault();
                    if (itemEditando != null)
                    {
                        montoItemEditando = Double.Parse(itemEditando.GetColumn("MtoTot"));
                    }
                }

                //Validaciones.-
                //Si el monto a ingresar excede el máximo => No permitirlo.-
                /*try
                {
                    Lo = (Int16)(Lo == -1 ? Lo = (short)Frm_Destino_Fondos.l_via.ListIndex : Lo); 
                    Restar = MODXVIA.VxVia[Lo].MtoTot;
                }
                catch
                {
                    Restar = 0;
                }*/

                var auxSuma = Fn_SumaVxVia(initObject, moneda, MODXVIA.VgxVia.Vuelto);
                var auxValMtoVia = double.Parse(Frm_Destino_Fondos.MtoVia.Text);
                var auxMtoTot = MODXVIA.VxMtoVia[lm].MtoTot;
                //es necesario redondear, en legaciy usa format a "0,00"
                var aux = Math.Round((auxSuma + auxValMtoVia - montoItemEditando) - auxMtoTot, 4);

                tracer.TraceVerbose("Calculando el monto a justificar...");
                tracer.AddToContext("auxSuma", auxSuma);
                tracer.AddToContext("auxValMtoVia", auxValMtoVia);
                tracer.AddToContext("auxMtoTot", auxMtoTot);
                tracer.AddToContext("aux", aux);

                if (aux > 0)
                {
                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Está justificando un monto que execede el máximo. Revise sus datos y reintente."
                    });
                    return;
                }

                //Se asocia el indice de la lista al arreglo.-
                n = idItemEditando;
                if (n == -1)
                {
                    n = (short)(VB6Helpers.UBound(MODXVIA.VxVia) + 1);
                    VB6Helpers.RedimPreserve(ref MODXVIA.VxVia, 0, n);
                }
                else
                {
                    //si se reeemplaza dato en vxvia
                    if (MODXVIA.VxVia[n].IdCtran != 0)
                    {
                        for (s = 0; s <= (short)VB6Helpers.UBound(MODXORI.Vx_SCodTran); s++)
                        {
                            if (MODXVIA.VxVia[n].IdCtran == MODXORI.Vx_SCodTran[s].ID)
                            {
                                MODXORI.Vx_SCodTran[s].Estado = 3;
                                break;
                            }
                        }
                    }

                }
                Frm_Destino_Fondos.l_via.ListIndex = n;

                MODXVIA.VxVia[n].NumCta = MODXORI.VOvd[LC].NumCta;
                MODXVIA.VxVia[n].NomVia = Frm_Destino_Fondos.L_Cta.Items.ElementAt(Frm_Destino_Fondos.L_Cta.ListIndex).Value;
                if (T_MODGCON0.IdCta_CtaCteMANN == MODXORI.VOvd[LC].NumCta || T_MODGCON0.IdCta_CtaCteMANE == MODXORI.VOvd[LC].NumCta)
                {
                    for (i = 0; i <= (short)VB6Helpers.UBound(MODCVDIM.CtaCCDin); i++)
                    {
                        if (VB6Helpers.Trim(VB6Helpers.Str(Frm_Destino_Fondos.cmb_codtran.ListIndex)) == MODCVDIM.CtaCCDin[i].NumCta)
                        {
                            MODXVIA.VxVia[n].NemCta = VB6Helpers.Trim(MODCVDIM.CtaCCDin[i].NemCta);
                            break;
                        }
                    }
                }
                else
                {
                    MODXVIA.VxVia[n].NemCta = MODXORI.VOvd[LC].NemCta;
                }

                MODXVIA.VxVia[n].CodMon = MODXVIA.VxMtoVia[lm].CodMon;
                ln = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, MODXVIA.VxVia[n].CodMon);
                MODXVIA.VxVia[n].NemMon = MODGTAB0.VMnd[ln].Mnd_MndNmc;
                decimal rs = 0;
                decimal.TryParse(Frm_Destino_Fondos.MtoVia.Text.Replace('.', ','), out rs);
                MODXVIA.VxVia[n].MtoTot = Convert.ToDouble(decimal.Round(rs, 2));

                // Se consulta si existe algun dato para asignar en numero de partida
                if (!String.IsNullOrEmpty(Frm_Destino_Fondos.txtNumRef.Text) && !String.IsNullOrEmpty(Frm_Destino_Fondos.txtNumRef.Text.Trim()))
                {
                    MODXVIA.VxVia[n].NumPar = VB6Helpers.CInt(VB6Helpers.Trim(Frm_Destino_Fondos.txtNumRef.Text));
                }
                else
                {
                    MODXVIA.VxVia[n].NumPar = 0;
                }

                if (Frm_Destino_Fondos.Cb_Destino.Enabled)
                {
                    if (Frm_Destino_Fondos.Cb_Destino.ListIndex != -1)
                    {
                        MODXVIA.VxVia[n].CodDme = (short)(Frm_Destino_Fondos.Cb_Destino.get_ItemData_(Frm_Destino_Fondos.Cb_Destino.ListIndex));
                    }
                }
                else
                {
                    MODXVIA.VxVia[n].CodDme = 0;
                }

                MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoIng;
                MODXVIA.VxVia[n].Vuelto = MODXVIA.VgxVia.Vuelto;
                if (Frm_Destino_Fondos.Ch_ImpChq.Value != 0)
                {
                    MODXVIA.VxVia[n].ImpChq = (short)(true ? -1 : 0);
                }
                else
                {
                    MODXVIA.VxVia[n].ImpChq = (short)(false ? -1 : 0);
                }

                if (Frm_Destino_Fondos.Ch_GenPln.Checked == true)
                {
                    MODXVIA.VxVia[n].GenPln = (short)(true ? -1 : 0);
                }
                else
                {
                    MODXVIA.VxVia[n].GenPln = (short)(false ? -1 : 0);
                }

                MODXVIA.VxVia[n].ctacte = "";

                short largo = 0;
                short _switchVar2 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta; //VOvd(i%).NumCta
                if (_switchVar2 == T_MODGCON0.IdCta_CtaCteMN || _switchVar2 == T_MODGCON0.IdCta_CtaCteME || _switchVar2 == T_MODGCON0.IdCta_ChqCCME)
                {
                    //Cliente Banco de Chile.
                    if ((Mdl_Funciones_Varias.CARGA_AUTOMATICA == 0 && MODXORI.gb_esCosmos == false))
                    {
                        if (Frm_Destino_Fondos.L_Cuentas.Items.Count == 0)
                        {
                            Frm_Destino_Fondos.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "El Cliente NO tiene Cuenta Corriente en la moneda especificada."
                            });
                            MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoEli;
                            return;
                        }
                    }

                    //Cuenta Corriente M/N.---Cuenta Corriente M/E.
                    if (Fn_Cargar_Cta_Cte(initObject, unit, n) == 0)
                    {
                        MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoEli;
                        return;
                    }
                }
                else if (_switchVar2 == T_MODGCON0.IdCta_SCSMN || _switchVar2 == T_MODGCON0.IdCta_SCSME)
                {
                    //Saldos c/ Sucursales M/N.
                    short _switchVar3 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                    if (_switchVar3 == T_MODGCON0.IdCta_SCSMN)
                    {
                        if (Fn_Cargar_Saldos(initObject, unit, n, T_MODGCON0.IdCta_SCSMN) == 0)
                        {
                            MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoEli;
                            return;
                        }
                    }
                    else if (_switchVar3 == T_MODGCON0.IdCta_SCSME)
                    {
                        if (Fn_Cargar_Saldos(initObject, unit, n, T_MODGCON0.IdCta_SCSME) == 0)
                        {
                            MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoEli;
                            return;
                        }
                    }

                }
                else if (_switchVar2 == T_MODGCON0.IdCta_CTACTEBC)
                {
                    //Cta. Cte. Banco Central
                    if (Fn_Cargar_Datos_Comun(initObject, unit, n) == 0)
                    {
                        MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoEli;
                        return;
                    }

                }
                else if (_switchVar2 == T_MODGCON0.IdCta_CTAORD || _switchVar2 == T_MODGCON0.IdCta_CHVBNYM || _switchVar2 == T_MODGCON0.IdCta_BOEREG || _switchVar2 == T_MODGCON0.IdCta_CHEREG || _switchVar2 == T_MODGCON0.IdCta_OBLREG || _switchVar2 == T_MODGCON0.IdCta_OBLARE || _switchVar2 == T_MODGCON0.IdCta_ACEREG || _switchVar2 == 54)
                {
                    //Corresponsal cuenta ordinaria
                    if (Fn_Cargar_Datos_Comun(initObject, unit, n) == 0)
                    {
                        MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoEli;
                        return;
                    }

                }
                else if (_switchVar2 >= 40 && _switchVar2 <= 53)
                {
                    //Cuentas de Obligaciones y Check Verification
                    if (Fn_Cargar_Datos_Comun(initObject, unit, n) == 0)
                    {
                        MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoEli;
                        return;
                    }

                }
                else if (_switchVar2 == T_MODGCON0.IdCta_DIVENPEN)
                {
                    //Divisas Pendientes.
                    if (Fn_Cargar_Datos_Comun(initObject, unit, n) == 0)
                    {
                        MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoEli;
                        return;
                    }

                }
                else if (_switchVar2 == T_MODGCON0.IdCta_VAM || _switchVar2 == T_MODGCON0.IdCta_VAX || _switchVar2 == T_MODGCON0.IdCta_VAMC || _switchVar2 == T_MODGCON0.IdCta_VAMCC || _switchVar2 == T_MODGCON0.IdCta_VASC)
                {
                    //Varios Acreedores Import. -- Varios Acreedores Export. -- 'Varios Acreedores Mcdo. Corr.
                    short _switchVar4 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                    if (_switchVar4 == T_MODGCON0.IdCta_VAM)
                    {
                        if (Fn_Cargar_Acreedores(initObject, unit, n, T_MODGCON0.IdCta_VAM) == 0)
                        {
                            MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoEli;
                            return;
                        }
                    }
                    else if (_switchVar4 == T_MODGCON0.IdCta_VAX)
                    {
                        if (Fn_Cargar_Acreedores(initObject, unit, n, T_MODGCON0.IdCta_VAX) == 0)
                        {
                            MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoEli;
                            return;
                        }
                    }
                    else if (_switchVar4 == T_MODGCON0.IdCta_VAMC)
                    {
                        if (Fn_Cargar_Acreedores(initObject, unit, n, T_MODGCON0.IdCta_VAMC) == 0)
                        {
                            MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoEli;
                            return;
                        }
                    }
                    else if (_switchVar4 == T_MODGCON0.IdCta_VAMCC || _switchVar4 == T_MODGCON0.IdCta_VASC)
                    {
                        if (Fn_Cargar_Acreedores(initObject, unit, n, T_MODGCON0.IdCta_VAMC) == 0)
                        {
                            MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoEli;
                            return;
                        }
                    }

                    //Otro Nemónico M/N ---- Otro Nemónico M/E
                }
                else if (_switchVar2 == T_MODGCON0.IdCta_ONMN || _switchVar2 == T_MODGCON0.IdCta_ONME)
                {
                    if (string.IsNullOrWhiteSpace(Frm_Destino_Fondos.Tx_Datos[0].Text))
                    {
                        Frm_Destino_Fondos.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Es necesario que se ingrese un Código de Nemónico para poder realizar la operación.",
                            ControlName = "Tx_Datos_0"
                        });
                        MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoEli;
                        return;
                    }

                    short _switchVar5 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                    if (_switchVar5 == T_MODGCON0.IdCta_ONMN)
                    {
                        if (Fn_Cargar_Otro_Nemonico(initObject, unit, n, T_MODGCON0.IdCta_ONMN) == 0)
                        {
                            MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoEli;
                            return;
                        }
                    }
                    else if (_switchVar5 == T_MODGCON0.IdCta_ONME)
                    {
                        if (Fn_Cargar_Otro_Nemonico(initObject, unit, n, T_MODGCON0.IdCta_ONME) == 0)
                        {
                            MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoEli;
                            return;
                        }
                    }

                }
                else if (_switchVar2 == T_MODGCON0.IdCta_CTAPTEMN || _switchVar2 == T_MODGCON0.IdCta_CTAPTEME)
                {
                    //Cuenta Puente M/N -- Cheque Puente M/E
                    if (Fn_Cargar_Puente(initObject, unit, n) == 0)
                    {
                        MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoEli;
                        return;
                    }

                }
                else if (_switchVar2 == T_MODGCON0.IdCta_CtaCteAUTN || _switchVar2 == T_MODGCON0.IdCta_CtaCteAUTE || _switchVar2 == T_MODGCON0.IdCta_CtaCteMANN || _switchVar2 == T_MODGCON0.IdCta_CtaCteMANE)
                {

                    if (Frm_Destino_Fondos.cmb_codtran.ListIndex == -1)
                    {
                        Frm_Destino_Fondos.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Debe seleccionar un tipo de transacción",
                            ControlName = "cmb_codtran"
                        });
                        MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoEli;
                        return;
                    }
                    MODXVIA.VxVia[n].Text1 = Frm_Destino_Fondos.Text1[0].Text != null ? Frm_Destino_Fondos.Text1[0].Text : string.Empty;
                    largo = (short)VB6Helpers.Len(MODXVIA.VxVia[n].Text1);

                    if (!string.IsNullOrEmpty(MODXVIA.VxVia[n].Text1) && VB6Helpers.Len(MODXVIA.VxVia[n].Text1) < 35)
                    {
                        for (i = (short)largo; i <= 34; i++)
                        {
                            MODXVIA.VxVia[n].Text1 += " ";
                        }

                    }

                    MODXVIA.VxVia[n].Text2 = Frm_Destino_Fondos.Text1[1].Text != null ? Frm_Destino_Fondos.Text1[1].Text : string.Empty;
                    largo = (short)VB6Helpers.Len(MODXVIA.VxVia[n].Text2);

                    if (!String.IsNullOrEmpty(MODXVIA.VxVia[n].Text2) && VB6Helpers.Len(MODXVIA.VxVia[n].Text2) < 35)
                    {
                        for (i = (short)largo; i <= 34; i++)
                        {
                            MODXVIA.VxVia[n].Text2 += " ";
                        }

                    }

                    MODXVIA.VxVia[n].Text3 = Frm_Destino_Fondos.Text1[2].Text != null ? Frm_Destino_Fondos.Text1[2].Text : string.Empty;
                    largo = (short)VB6Helpers.Len(MODXVIA.VxVia[n].Text3);

                    if (String.IsNullOrEmpty(MODXVIA.VxVia[n].Text3) && VB6Helpers.Len(MODXVIA.VxVia[n].Text3) < 35)
                    {
                        for (i = (short)largo; i <= 34; i++)
                        {
                            MODXVIA.VxVia[n].Text3 += " ";
                        }

                    }

                    MODXVIA.VxVia[n].Text4 = Frm_Destino_Fondos.Text1[3].Text != null ? Frm_Destino_Fondos.Text1[3].Text : string.Empty;
                    largo = (short)VB6Helpers.Len(MODXVIA.VxVia[n].Text4);

                    if (!string.IsNullOrEmpty(MODXVIA.VxVia[n].Text1) && VB6Helpers.Len(MODXVIA.VxVia[n].Text1) < 35)
                    {
                        for (i = (short)largo; i <= 34; i++)
                        {
                            MODXVIA.VxVia[n].Text4 += " ";
                        }

                    }

                    MODXVIA.VxVia[n].Text5 = Frm_Destino_Fondos.Text1[4].Text != null ? Frm_Destino_Fondos.Text1[4].Text : string.Empty;
                    largo = (short)VB6Helpers.Len(MODXVIA.VxVia[n].Text5);

                    if (!string.IsNullOrEmpty(MODXVIA.VxVia[n].Text1) && VB6Helpers.Len(MODXVIA.VxVia[n].Text1) < 35)
                    {
                        for (i = (short)largo; i <= 34; i++)
                        {
                            MODXVIA.VxVia[n].Text1 += " ";
                        }

                    }

                    VB6Helpers.RedimPreserve(ref MODXORI.Vx_SCodTran, 0, VB6Helpers.UBound(MODXORI.Vx_SCodTran) + 1);
                    MODXORI.Vx_SCodTran[VB6Helpers.UBound(MODXORI.Vx_SCodTran)].nro_trx = (short)(Frm_Destino_Fondos.cmb_codtran.get_ItemData_(Frm_Destino_Fondos.cmb_codtran.ListIndex));
                    MODXORI.Vx_SCodTran[VB6Helpers.UBound(MODXORI.Vx_SCodTran)].Moneda = MODXVIA.Moneda_TRX;
                    if (MODXVIA.VgxVia.Vuelto == -1)
                    {
                        MODXORI.Vx_SCodTran[VB6Helpers.UBound(MODXORI.Vx_SCodTran)].Via = "vue";
                    }
                    else
                    {
                        MODXORI.Vx_SCodTran[VB6Helpers.UBound(MODXORI.Vx_SCodTran)].Via = MODXORI.ori_des;
                    }

                    MODXORI.Vx_SCodTran[VB6Helpers.UBound(MODXORI.Vx_SCodTran)].Estado = 1;

                    MODXVIA.IdCtran = (short)(MODXVIA.IdCtran + 1);
                    MODXVIA.VxVia[n].IdCtran = MODXVIA.IdCtran;

                    MODXORI.Vx_SCodTran[VB6Helpers.UBound(MODXORI.Vx_SCodTran)].ID = MODXVIA.VxVia[n].IdCtran;
                    //'''''''''''''''''''''''
                    if (Fn_Cargar_Cta_Cte(initObject, unit, n) == 0)
                    {
                        MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoEli;
                        return;
                    }

                }
                else if (_switchVar2 == T_MODGCON0.IdCta_GAPMN || _switchVar2 == T_MODGCON0.IdCta_GAPME)
                {

                    if (Frm_Destino_Fondos.cmb_codtran.ListIndex == -1)
                    {
                        Frm_Destino_Fondos.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Debe seleccionar un tipo de transacción",
                            ControlName = "cmb_codtran"
                        });
                        MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoEli;
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(Frm_Destino_Fondos.txt_cuenta.Text))
                    {
                        Frm_Destino_Fondos.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Debe ingresar cuenta GAP",
                            ControlName = "txt_cuenta"
                        });
                        MODXVIA.VxVia[n].Status = T_MODGCVD.EstadoEli;
                        return;
                    }
                    else
                    {
                        MODXVIA.VxVia[n].ctacte = VB6Helpers.Trim(Frm_Destino_Fondos.txt_cuenta.Text);
                        MODXVIA.VxVia[n].CtaCte_t = VB6Helpers.Trim(Frm_Destino_Fondos.txt_cuenta.Text);
                    }

                    MODXVIA.VxVia[n].Text5 = VB6Helpers.Mid(MODGCVD.VgCvd.OpeSin, 6, 10);

                    VB6Helpers.RedimPreserve(ref MODXORI.Vx_SCodTran, 0, VB6Helpers.UBound(MODXORI.Vx_SCodTran) + 1);
                    MODXORI.Vx_SCodTran[VB6Helpers.UBound(MODXORI.Vx_SCodTran)].nro_trx = (short)(Frm_Destino_Fondos.cmb_codtran.get_ItemData_(Frm_Destino_Fondos.cmb_codtran.ListIndex));
                    MODXORI.Vx_SCodTran[VB6Helpers.UBound(MODXORI.Vx_SCodTran)].Moneda = MODXVIA.Moneda_TRX;
                    MODXORI.Vx_SCodTran[VB6Helpers.UBound(MODXORI.Vx_SCodTran)].Via = MODXORI.ori_des;
                    MODXORI.Vx_SCodTran[VB6Helpers.UBound(MODXORI.Vx_SCodTran)].Estado = 1;

                    MODXVIA.IdCtran = (short)(MODXVIA.IdCtran + 1);
                    MODXVIA.VxVia[n].IdCtran = MODXVIA.IdCtran;

                    MODXORI.Vx_SCodTran[VB6Helpers.UBound(MODXORI.Vx_SCodTran)].ID = MODXVIA.VxVia[n].IdCtran;
                }

                ////Se ingresa una linea en blanco si es nueva.-
                //if (Frm_Destino_Fondos.l_via.get_List((short)l_via.ListIndex) == "")
                //{
                //    l_via.AddItem("");
                //}

                //Se ingresan los nuevos datos en la lista.-
                Pr_Llena_l_via(initObject, unit);

                //Si todo está justificado => se posiciona en botón Aceptar.-
                if (Fn_ViasOK(initObject) != 0)
                {
                    Frm_Destino_Fondos.Boton[0].Enabled = true;
                    if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 0)
                    {

                    }
                    using (var trace = new Tracer("Datos de Destino: "))
                    {
                        trace.TraceInformation(String.Format("Nro Operación: {0}", initObject.MODGCVD.VgCvd.OpeSin));
                        double montoTotal = 0;
                        foreach (var Destino in MODXVIA.VxVia)
                        {
                            trace.TraceInformation(String.Format("Moneda: {0}", Destino.NemMon));
                            trace.TraceInformation(String.Format("Memonico: {0}", Destino.NemCta));
                            trace.TraceInformation(String.Format("Monto: {0}", Destino.MtoTot));
                            trace.TraceInformation(String.Format("Tipo de Cuenta: {0}", Destino.NomVia));
                            montoTotal += Destino.MtoTot;
                        }
                        trace.TraceInformation(String.Format("Monto total: {0}", montoTotal));
                    }
                }
                else
                {
                    Frm_Destino_Fondos.Boton[0].Enabled = false;
                }

                if ((MODXVIA.VxVia[n].NumCta == T_MODGCON0.IdCta_CHVRF || MODXVIA.VxVia[n].NumCta == T_MODGCON0.IdCta_OPC || MODXVIA.VxVia[n].NumCta == T_MODGCON0.IdCta_OPOP || MODXVIA.VxVia[n].NumCta == T_MODGCON0.IdCta_CHMEBCH))
                {
                    BCH.Comex.Core.BL.XCFT.Modulos.ModChVrf.TraeDatosVias_ChVrf(initObject, n);
                    for (i = 0; i <= (short)VB6Helpers.UBound(ModChVrf.VgChV); i++)
                    {
                        if (ModChVrf.VgChV[i].Saldo > 0)
                        {
                            ModChVrf.AceptoPantallaChVrf = (short)(false ? -1 : 0);
                            break;
                        }

                    }

                }

                //Limpia los valores y oculta objetos.
                LimpiaNumPartida(initObject);

                //************************************************************************************
                //Si la cuenta seleccionada corresponden a Cuenta Corriente M/N o Cuenta Corriente M/E
                //debe enviar la cuenta al servicio web para saber si corresponde a cta. Chile o Cosmo
                if (MODXVIA.VxVia[n].NumCta != 0)
                {
                    lc_cuenta = MODXVIA.VxVia[n].NumCta;
                }

                if (lc_cuenta == 3 || lc_cuenta == 10)
                {
                    //llamar al servicio
                }

                //************************************************************************************
            }
        }
        public static void Tx_Datos_KeyPress(InitializationObject initObject, UnitOfWorkCext01 unit, short Index)
        {
            T_MODXORI MODXORI = initObject.MODXORI;
            T_MODGNCTA MODGNCTA = initObject.MODGNCTA;
            T_MODGUSR MODGUSR = initObject.MODGUSR;
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;

            string a = "";
            string razon = "";
            short b = 0;
            short i = 0;
            short j = 0;
            string Texto = "";
            Frm_Destino_Fondos.Tx_Datos[Index].Text = (Frm_Destino_Fondos.Tx_Datos[Index].Text ?? string.Empty).ToUpper();
            switch (Index)
            {
                case 0:  //Tx_Datos(0)
                    if (!string.IsNullOrWhiteSpace(Frm_Destino_Fondos.Tx_Datos[0].Text))
                    {
                        //Saldo con Sucursales M/N --- Saldo con Sucursales M/E
                        if ((MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_SCSMN) || (MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_SCSME))
                        {
                            if (VB6Helpers.IsNumeric(Frm_Destino_Fondos.Tx_Datos[0].Text))
                            {
                                Frm_Destino_Fondos.Tx_Datos[0].MaxLength = 0;
                                a = BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_Buscar_Suc(initObject, VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[0].Text));
                                if (!string.IsNullOrEmpty(a))
                                {
                                    if (VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[0].Text) != MODGUSR.UsrEsp.CentroCosto)
                                    {
                                        Frm_Destino_Fondos.Lb_Oficina.Text = a;
                                    }

                                    //Validación Correcta
                                    short _switchVar1 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                                    if (_switchVar1 == T_MODGCON0.IdCta_SCSMN)
                                    {
                                        Frm_Destino_Fondos.SCSMN_OK = "01";
                                    }
                                    else if (_switchVar1 == T_MODGCON0.IdCta_SCSME)
                                    {
                                        Frm_Destino_Fondos.SCSME_OK = "01";
                                    }

                                }
                                else
                                {
                                    Frm_Destino_Fondos.Lb_Oficina.Text = "";
                                    //Validación Incorrecta
                                    short _switchVar2 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                                    if (_switchVar2 == T_MODGCON0.IdCta_SCSMN)
                                    {
                                        Frm_Destino_Fondos.SCSMN_OK = "00";
                                    }
                                    else if (_switchVar2 == T_MODGCON0.IdCta_SCSME)
                                    {
                                        Frm_Destino_Fondos.SCSME_OK = "00";
                                    }

                                }

                            }

                        }

                        //Varios Acreedores Import. -- Varios Acreedores Export. -- 'Varios Acreedores Mcdo. Corr.
                        short _switchVar3 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                        if (_switchVar3 == T_MODGCON0.IdCta_VAM || _switchVar3 == T_MODGCON0.IdCta_VAX || _switchVar3 == T_MODGCON0.IdCta_VAMC || _switchVar3 == T_MODGCON0.IdCta_VAMCC || _switchVar3 == T_MODGCON0.IdCta_VASC)
                        {
                            razon = BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.SyGet_Partys(unit, VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[0].Text));
                            Frm_Destino_Fondos.Lb_Oficina.Text = MODGPYF1.Minuscula(VB6Helpers.Trim(razon));
                            if (string.IsNullOrWhiteSpace(razon))
                            {
                                Frm_Destino_Fondos.Lb_Oficina.Text = "";
                                //Validación Incorrecta
                                short _switchVar4 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                                if (_switchVar4 == T_MODGCON0.IdCta_VAM)
                                {
                                    Frm_Destino_Fondos.VAM_OK = "00";
                                }
                                else if (_switchVar4 == T_MODGCON0.IdCta_VAX)
                                {
                                    Frm_Destino_Fondos.VAX_OK = "00";
                                }
                                else if (_switchVar4 == T_MODGCON0.IdCta_VAMC)
                                {
                                    Frm_Destino_Fondos.VAMC_OK = "00";
                                }
                                else if (_switchVar4 == T_MODGCON0.IdCta_VAMCC || _switchVar4 == T_MODGCON0.IdCta_VASC)
                                {
                                    Frm_Destino_Fondos.VAMC_OK = "00";
                                }

                            }
                            else
                            {
                                //Validación Correcta
                                short _switchVar5 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                                if (_switchVar5 == T_MODGCON0.IdCta_VAM)
                                {
                                    Frm_Destino_Fondos.VAM_OK = "01";
                                }
                                else if (_switchVar5 == T_MODGCON0.IdCta_VAX)
                                {
                                    Frm_Destino_Fondos.VAX_OK = "01";
                                }
                                else if (_switchVar5 == T_MODGCON0.IdCta_VAMC)
                                {
                                    Frm_Destino_Fondos.VAMC_OK = "01";
                                }
                                else if (_switchVar5 == T_MODGCON0.IdCta_VAMCC || _switchVar5 == T_MODGCON0.IdCta_VASC)
                                {
                                    Frm_Destino_Fondos.VAMC_OK = "01";
                                }

                            }

                        }

                        //Otro Nemónico M/N --- Otro Nemónico M/E.
                        if ((MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_ONMN) || (MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_ONME))
                        {
                            //Otro Nemónico M/N ---- Otro Nemónico M/E
                            b = (short)BCH.Comex.Core.BL.XCFT.Modulos.MODGNCTA.Get_Cta(VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[0].Text), initObject, unit);
                            if (b == -1 || b == 0)
                            {
                                Frm_Destino_Fondos.Lb_Oficina.Text = "";
                                //Validación Incorrecta
                                short _switchVar6 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                                if (_switchVar6 == T_MODGCON0.IdCta_ONMN)
                                {
                                    Frm_Destino_Fondos.ONMN_OK = "00";
                                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = "Usted seleccionó una Cuenta Nacional, por lo tanto el Nemónico que tendría que ingresar debería ser Nacional."
                                    });
                                }
                                else if (_switchVar6 == T_MODGCON0.IdCta_ONME)
                                {
                                    Frm_Destino_Fondos.ONME_OK = "00";
                                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = "Usted seleccionó una Cuenta Extranjera, por lo tanto el Nemónico que tendría que ingresar debería ser Extranjero."
                                    });
                                }
                            }
                            else
                            {
                                short _switchVar7 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                                if (_switchVar7 == T_MODGCON0.IdCta_ONMN)
                                {
                                    if (MODGNCTA.VCta[b].Cta_Mon == 1)
                                    {
                                        Frm_Destino_Fondos.Lb_Oficina.Text = "";
                                        //Validación Incorrecta
                                        short _switchVar8 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                                        if (_switchVar8 == T_MODGCON0.IdCta_ONMN)
                                        {
                                            Frm_Destino_Fondos.ONMN_OK = "00";
                                            Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                            {
                                                Type = TipoMensaje.Error,
                                                Text = "Usted seleccionó una Cuenta Nacional, por lo tanto el Nemónico que tendría que ingresar debería ser Nacional."
                                            });
                                        }
                                        else if (_switchVar8 == T_MODGCON0.IdCta_ONME)
                                        {
                                            Frm_Destino_Fondos.ONME_OK = "00";
                                            Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                            {
                                                Type = TipoMensaje.Error,
                                                Text = "Usted seleccionó una Cuenta Extranjera, por lo tanto el Nemónico que tendría que ingresar debería ser Extranjero."
                                            });
                                        }
                                    }
                                    else
                                    {
                                        Frm_Destino_Fondos.Lb_Oficina.Text = MODGPYF1.Minuscula(VB6Helpers.Trim(MODGNCTA.VCta[b].Cta_Nom.Value));
                                        //Validación Correcta
                                        short _switchVar9 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                                        if (_switchVar9 == T_MODGCON0.IdCta_ONMN)
                                        {
                                            Frm_Destino_Fondos.ONMN_OK = "01";
                                        }
                                        else if (_switchVar9 == T_MODGCON0.IdCta_ONME)
                                        {
                                            Frm_Destino_Fondos.ONME_OK = "01";
                                        }

                                    }

                                }
                                else if (_switchVar7 == T_MODGCON0.IdCta_ONME)
                                {
                                    if (MODGNCTA.VCta[b].Cta_Mon == 2)
                                    {
                                        Frm_Destino_Fondos.Lb_Oficina.Text = "";
                                        //Validación Incorrecta
                                        short _switchVar10 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                                        if (_switchVar10 == T_MODGCON0.IdCta_ONMN)
                                        {
                                            Frm_Destino_Fondos.ONMN_OK = "00";
                                            Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                            {
                                                Type = TipoMensaje.Error,
                                                Text = "Usted seleccionó una Cuenta Nacional, por lo tanto el Nemónico que tendría que ingresar debería ser Nacional."
                                            });
                                        }
                                        else if (_switchVar10 == T_MODGCON0.IdCta_ONME)
                                        {
                                            Frm_Destino_Fondos.ONME_OK = "00";
                                            Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                            {
                                                Type = TipoMensaje.Error,
                                                Text = "Usted seleccionó una Cuenta Extranjera, por lo tanto el Nemónico que tendría que ingresar debería ser Extranjero."
                                            });
                                        }

                                    }
                                    else
                                    {
                                        Frm_Destino_Fondos.Lb_Oficina.Text = MODGPYF1.Minuscula(VB6Helpers.Trim(MODGNCTA.VCta[b].Cta_Nom.Value));
                                        //Validación Correcta
                                        short _switchVar11 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                                        if (_switchVar11 == T_MODGCON0.IdCta_ONMN)
                                        {
                                            Frm_Destino_Fondos.ONMN_OK = "01";
                                        }
                                        else if (_switchVar11 == T_MODGCON0.IdCta_ONME)
                                        {
                                            Frm_Destino_Fondos.ONME_OK = "01";
                                        }

                                    }

                                }

                                //Verifica que Nemónico NO sea una Cuenta Especial.-
                                for (i = 0; i <= (short)(Frm_Destino_Fondos.L_Cta.Items.Count - 1); i++)
                                {
                                    j = (short)(Frm_Destino_Fondos.L_Cta.get_ItemData_(i));
                                    if (VB6Helpers.Trim(MODXORI.VOvd[j].NemCta) == VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[0].Text))
                                    {
                                        //Se deja el comportamiento del legacy.
                                        Frm_Destino_Fondos.L_Cta.ListIndex = i;
                                        L_Cta_Click(initObject, unit);
                                        return;
                                    }

                                }

                                // Si la cuenta es vigenteable se habilita el ingreso de numero de referencia
                                // de lo contrario limpia los valores y oculta objetos.
                                if ((MODGNCTA.VCta[b].Cta_Vig == 1))
                                {
                                    Frm_Destino_Fondos.LB_Referencia.Visible = true;
                                    Frm_Destino_Fondos.txtNumRef.Visible = true;
                                }
                                else
                                {
                                    LimpiaNumPartida(initObject);
                                }

                            }

                        }

                    }
                    else
                    {
                        //Dejar en blanco las validaciones.
                        Frm_Destino_Fondos.SCSMN_OK = "00"; Frm_Destino_Fondos.SCSME_OK = "00";
                        Frm_Destino_Fondos.VAM_OK = "00"; Frm_Destino_Fondos.VAX_OK = "00"; Frm_Destino_Fondos.VAMC_OK = "00";
                        Frm_Destino_Fondos.ONMN_OK = "00"; Frm_Destino_Fondos.ONME_OK = "00";
                    }

                    //******************************************
                    break;
                case 1:  //Tx_Datos(1)
                    if (!string.IsNullOrWhiteSpace(Frm_Destino_Fondos.Tx_Datos[1].Text))
                    {
                        if ((MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_SCSMN) || (MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_SCSME))
                        {
                            Texto = "01;02;03;04";
                            if (VB6Helpers.Instr(Texto, Format.FormatCurrency(Format.StringToDouble(Frm_Destino_Fondos.Tx_Datos[1].Text), "00")) != 0)
                            {
                                Pr_Generar_Automatica_Ini(initObject, unit);
                            }
                            else
                            {
                                Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Error,
                                    Text = "El Tipo de Movimiento debe estar entre 1 y 4."
                                });
                                Frm_Destino_Fondos.Tx_Datos[1].Enabled = true;

                                return;
                            }

                        }

                    }

                    break;
            }

        }
        public static void txtNumRef_LostFocus(InitializationObject initObject)
        {
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;
            if (string.IsNullOrEmpty(Frm_Destino_Fondos.txtNumRef.Text))
            {
                Frm_Destino_Fondos.Errors.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Es necesario que se ingrese un Número de Partida para poder realizar la operación."
                });
                Frm_Destino_Fondos.txtNumRef.Enabled = true;
            }

        }
        #endregion

        #region METODOS PRIVADOS
        //****************************************************************************
        //   1.  Envía orden para validar los campos necesarios para la operación
        //       con respecto a "Cta. y Cheques Puente" y si la validación es
        //       correcta carga los datos en los campos correspondientes dentro del
        //       arreglo VxVia.
        //****************************************************************************
        private static short Fn_Cargar_Puente(InitializationObject initObject, UnitOfWorkCext01 unit, short Indice)
        {
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODXORI MODXORI = initObject.MODXORI;

            short _retValue = 0;

            _retValue = 0;

            if (Fn_Validar_Campos(initObject, unit, 4, 5, -1) == 0)
            {
                return _retValue;
            }

            MODXVIA.VxVia[Indice].CodDme = Frm_Destino_Fondos.Indice_Destino;
            MODXVIA.VxVia[Indice].PosPrty = MODXORI.Indice_Partys;
            return 1;
        }
        //****************************************************************************
        //   1.  Envía orden para validar los campos necesarios para la operación
        //       con respecto a "Otro Nemónico M/N ---- Otro Nemónico M/E" y si la
        //       validación es correcta carga los datos en los campos correspondientes
        //       dentro del arreglo VxVia.
        //****************************************************************************
        private static short Fn_Cargar_Otro_Nemonico(InitializationObject initObject, UnitOfWorkCext01 unit, short Indice, short Nemonico)
        {
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;
            T_MODXVIA MODXVIA = initObject.MODXVIA;

            short _retValue = 0;

            _retValue = 0;

            switch (Nemonico)
            {
                case T_MODGCON0.IdCta_ONMN:
                    if (Fn_Validar_Campos(initObject, unit, 0, 0, -1) == 0)
                    {
                        return _retValue;
                    }

                    if (Fn_Validar_Campos(initObject, unit, 5, 5, -1) == 0)
                    {
                        return _retValue;
                    }
                    break;
                case T_MODGCON0.IdCta_ONME:
                    if (Fn_Validar_Campos(initObject, unit, 0, 0, -1) == 0)
                    {
                        return _retValue;
                    }

                    if (Fn_Validar_Campos(initObject, unit, 5, 5, -1) == 0)
                    {
                        return _retValue;
                    }
                    break;
            }

            MODXVIA.VxVia[Indice].CodDme = Frm_Destino_Fondos.Indice_Destino;
            MODXVIA.VxVia[Indice].NemCta = VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[0].Text);
            return 1;
        }
        //****************************************************************************
        //   1.  Envía orden para validar los campos necesarios para la operación
        //       con respecto a "Varios Acreedores Import., Varios Acreedores Export.,
        //       Varios Acreedores Mcdo. Corr" y si la validación es correcta carga
        //       los datos en los campos correspondientes dentro del arreglo VxVia.
        //****************************************************************************
        private static short Fn_Cargar_Acreedores(InitializationObject initObject, UnitOfWorkCext01 unit, short Indice, short Acreedor)
        {
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODXORI MODXORI = initObject.MODXORI;

            short _retValue = 0;

            _retValue = 0;

            switch (Acreedor)
            {
                case T_MODGCON0.IdCta_VAM:
                    if ((VB6Helpers.Trim(Frm_Destino_Fondos.VAM_OK) == "00") || (string.IsNullOrWhiteSpace(Frm_Destino_Fondos.VAM_OK)))
                    {
                        if (Fn_Validar_Campos(initObject, unit, 0, 0, -1) == 0)
                        {
                            return _retValue;
                        }

                        if (Fn_Validar_Campos(initObject, unit, 5, 5, -1) == 0)
                        {
                            return _retValue;
                        }

                    }
                    else
                    {
                        if (Fn_Validar_Campos(initObject, unit, 5, 5, -1) == 0)
                        {
                            return _retValue;
                        }

                    }

                    break;
                case T_MODGCON0.IdCta_VAX:
                    if ((VB6Helpers.Trim(Frm_Destino_Fondos.VAX_OK) == "00") || (string.IsNullOrWhiteSpace(Frm_Destino_Fondos.VAX_OK)))
                    {
                        if (Fn_Validar_Campos(initObject, unit, 0, 0, -1) == 0)
                        {
                            return _retValue;
                        }

                        if (Fn_Validar_Campos(initObject, unit, 5, 5, -1) == 0)
                        {
                            return _retValue;
                        }

                    }
                    else
                    {
                        if (Fn_Validar_Campos(initObject, unit, 5, 5, -1) == 0)
                        {
                            return _retValue;
                        }

                    }

                    break;
                case T_MODGCON0.IdCta_VAMC:
                case T_MODGCON0.IdCta_VAMCC:
                case T_MODGCON0.IdCta_VASC:
                    if ((VB6Helpers.Trim(Frm_Destino_Fondos.VAMC_OK) == "00") || (string.IsNullOrWhiteSpace(Frm_Destino_Fondos.VAMC_OK)))
                    {
                        if (Fn_Validar_Campos(initObject, unit, 0, 0, -1) == 0)
                        {
                            return _retValue;
                        }

                        if (Fn_Validar_Campos(initObject, unit, 5, 5, -1) == 0)
                        {
                            return _retValue;
                        }

                    }
                    else
                    {
                        if (Fn_Validar_Campos(initObject, unit, 5, 5, -1) == 0)
                        {
                            return _retValue;
                        }

                    }

                    break;
            }

            MODXVIA.VxVia[Indice].PosPrty = MODXORI.Indice_Partys;
            MODXVIA.VxVia[Indice].CodDme = Frm_Destino_Fondos.Indice_Destino;
            MODXVIA.VxVia[Indice].IdPrty = VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[0].Text);
            MODXVIA.VxVia[Indice].NomPrty = VB6Helpers.Trim(Frm_Destino_Fondos.Lb_Oficina.Text);
            return 1;
        }
        //****************************************************************************
        //   1.  Envía orden para validar los campos necesarios para la operación
        //       con respecto a "Saldos c/ Sucursales M/N." y si la validación es
        //       correcta carga los datos en los campos correspondientes dentro
        //       del arreglo VxVia.
        //****************************************************************************
        private static short Fn_Cargar_Saldos(InitializationObject initObject, UnitOfWorkCext01 unit, short Indice, short Saldo)
        {
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_Module1 Module1 = initObject.Module1;

            short _retValue = 0;

            _retValue = 0;

            switch (Saldo)
            {
                case T_MODGCON0.IdCta_SCSMN:
                    if ((VB6Helpers.Trim(Frm_Destino_Fondos.SCSMN_OK) == "00") || (string.IsNullOrWhiteSpace(Frm_Destino_Fondos.SCSMN_OK)))
                    {
                        if (Fn_Validar_Campos(initObject, unit, 0, 2, -1) == 0)
                        {
                            return _retValue;
                        }

                        if (Fn_Validar_Campos(initObject, unit, 5, 5, -1) == 0)
                        {
                            return _retValue;
                        }

                    }
                    else if (VB6Helpers.Trim(Frm_Destino_Fondos.SCSMN_OK) == "01")
                    {
                        if (Fn_Validar_Campos(initObject, unit, 0, 2, -1) == 0)
                        {
                            return _retValue;
                        }

                        if (Fn_Validar_Campos(initObject, unit, 5, 5, -1) == 0)
                        {
                            return _retValue;
                        }

                    }
                    else
                    {
                        if (Fn_Validar_Campos(initObject, unit, 5, 5, -1) == 0)
                        {
                            return _retValue;
                        }

                    }

                    break;
                case T_MODGCON0.IdCta_SCSME:
                    if ((VB6Helpers.Trim(Frm_Destino_Fondos.SCSME_OK) == "00") || (string.IsNullOrWhiteSpace(Frm_Destino_Fondos.SCSME_OK)))
                    {
                        if (Fn_Validar_Campos(initObject, unit, 0, 2, -1) == 0)
                        {
                            return _retValue;
                        }

                        if (Fn_Validar_Campos(initObject, unit, 5, 5, -1) == 0)
                        {
                            return _retValue;
                        }

                    }
                    else if (VB6Helpers.Trim(Frm_Destino_Fondos.SCSME_OK) == "01")
                    {
                        if (Fn_Validar_Campos(initObject, unit, 0, 2, -1) == 0)
                        {
                            return _retValue;
                        }

                        if (Fn_Validar_Campos(initObject, unit, 5, 5, -1) == 0)
                        {
                            return _retValue;
                        }

                    }
                    else
                    {
                        if (Fn_Validar_Campos(initObject, unit, 5, 5, -1) == 0)
                        {
                            return _retValue;
                        }

                    }

                    break;
            }

            MODXVIA.VxVia[Indice].CodDme = Frm_Destino_Fondos.Indice_Destino;
            MODXVIA.VxVia[Indice].IdPrty = VB6Helpers.Trim(Module1.PartysOpe[MODXORI.Indice_Partys].LlaveArchivo);
            MODXVIA.VxVia[Indice].PosPrty = MODXORI.Indice_Partys;
            MODXVIA.VxVia[Indice].codofi = (short)VB6Helpers.Val(Frm_Destino_Fondos.Tx_Datos[0].Text);
            MODXVIA.VxVia[Indice].TipMov = (short)VB6Helpers.Val(Frm_Destino_Fondos.Tx_Datos[1].Text);
            MODXVIA.VxVia[Indice].NumPar = (int)VB6Helpers.Val(Frm_Destino_Fondos.Tx_Datos[2].Text);
            return 1;
        }
        private static void Pr_Generar_Automatica_Ini(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;

            short si = (short)(false ? -1 : 0);
            short delista = 0;
            string linea = "";
            string TP = "";
            //Generación Automática
            if (~delista != 0)
            {
                if (Frm_Destino_Fondos.Tx_Datos[2].Enabled)
                {
                    si = (short)(true ? -1 : 0);
                }
            }
            else
            {
                //TODO: EMILIANO --> REVISAR
                linea = Frm_Destino_Fondos.l_via.Items.ElementAt(Frm_Destino_Fondos.l_via.ListIndex).GetColumn("NemMon");
                TP = MODGPYF0.copiardestring(linea, VB6Helpers.Chr(9), 7);
                if ((VB6Helpers.Val(TP) != T_MODXORI.TP_INI))
                {
                    si = (short)(true ? -1 : 0);
                    if ((VB6Helpers.Val(TP) == T_MODXORI.TP_COM) && Frm_Destino_Fondos.Tx_Datos[2].Enabled == false)
                    {
                        si = (short)(false ? -1 : 0);
                    }
                }

            }

            if (si != 0)
            {
                Frm_Destino_Fondos.Tx_Datos[2].Text = MODXORI.Fn_Genera_Num(initObject, unit);
                if (!string.IsNullOrWhiteSpace(Frm_Destino_Fondos.Tx_Datos[2].Text))
                {
                    Frm_Destino_Fondos.Tx_Datos[2].Enabled = false;
                }
            }

        }
        //****************************************************************************
        //   1.  Envía orden para validar los campos necesarios para la operación
        //       con respecto a "Cta. Cte. Banco Central" y si la validación es
        //       correcta carga los datos en los campos correspondientes dentro del
        //       arreglo VxVia.
        //****************************************************************************
        private static short Fn_Cargar_Datos_Comun(InitializationObject initObject, UnitOfWorkCext01 unit, short Indice)
        {
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODXORI MODXORI = initObject.MODXORI;
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;
            short _retValue = 0;

            _retValue = 0;

            if (Fn_Validar_Campos(initObject, unit, 0, 1, -1) == 0)
            {
                return _retValue;
            }

            if (Fn_Validar_Campos(initObject, unit, 5, 5, -1) == 0)
            {
                return _retValue;
            }

            MODXVIA.VxVia[Indice].CodDme = Frm_Destino_Fondos.Indice_Destino;
            MODXVIA.VxVia[Indice].PosPrty = MODXORI.Indice_Partys;
            MODXVIA.VxVia[Indice].CodSwf = VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[0].Text);
            MODXVIA.VxVia[Indice].numdoc = VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[1].Text);
            return 1;
        }
        //****************************************************************************
        //   1.  Envía orden para validar los campos necesarios para la operación
        //       con respecto a "Cuenta Corriente M/N." y si la validación es correcta
        //       carga los datos en los campos correspondientes dentro del arreglo
        //       VxVia.
        //****************************************************************************
        private static short Fn_Cargar_Cta_Cte(InitializationObject initObject, UnitOfWorkCext01 unit, short Indice)
        {
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_Module1 Module1 = initObject.Module1;

            short _retValue = 0;

            _retValue = 0;

            if (Fn_Validar_Campos(initObject, unit, 3, 5, -1) == 0)
            {
                return _retValue;
            }

            MODXVIA.VxVia[Indice].CodDme = Frm_Destino_Fondos.Indice_Destino;
            MODXVIA.VxVia[Indice].IdPrty = VB6Helpers.Trim(Module1.PartysOpe[MODXORI.Indice_Partys].LlaveArchivo);
            MODXVIA.VxVia[Indice].PosPrty = MODXORI.Indice_Partys;

            MODXVIA.VxVia[Indice].ctacte = VB6Helpers.Trim(MODXORI.Vx_OriCC[Frm_Destino_Fondos.Indice_CtaCte].ctacte);
            MODXVIA.VxVia[Indice].CtaCte_t = VB6Helpers.Trim(MODXORI.Vx_OriCC[Frm_Destino_Fondos.Indice_CtaCte].CtaCte_t);
            MODXVIA.VxVia[Indice].MonExt = MODXORI.Vx_OriCC[Frm_Destino_Fondos.Indice_CtaCte].MonExt;
            if (Frm_Destino_Fondos.L_Cuentas.ListIndex != -1)
            {
                Frm_Destino_Fondos.L_Cuentas.ListIndex = -1;
                L_Cuentas_Click(initObject);
            }
            return 1;
        }
        private static void Ok_Partys_Click(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            T_MODXORI MODXORI = initObject.MODXORI;
            T_Module1 Module1 = initObject.Module1;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;
            //if (Frm_Destino_Fondos.l_via.ListIndex > -1 && Frm_Destino_Fondos.l_via.Items.Count != Frm_Destino_Fondos.l_via.ListIndex)
            //    MODXORI.Indice_Partys = (short)Frm_Destino_Fondos.l_via.ListIndex;

            short a, n = 0, i = 0;

            a = BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.SyGetn_Ctas(initObject, unit, Module1.PartysOpe[MODXORI.Indice_Partys].LlaveArchivo.Replace('~', '|'));
            if (a != 0)
            {
                Frm_Destino_Fondos.L_Cuentas.Enabled = true;
                if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
                {
                    MODXORI.Vx_OriCC = new T_OriCC[1] { new T_OriCC() };
                    MODXORI.Vx_OriCC[0].ctacte = VB6Helpers.Trim(Mdl_Funciones_Varias.Lc_BaseNumber);
                    MODXORI.Vx_OriCC[0].MonExt = 0;
                    MODXORI.Vx_OriCC[0].Activa = 1;
                    MODXORI.Vx_OriCC[0].CodMnd = Mdl_Funciones_Varias.LC_MONEDA;
                    BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Pr_Cargar_Lista_Cuentas(initObject, unit, Frm_Destino_Fondos.L_Cuentas, Frm_Destino_Fondos.L_Mnd, 0);
                }
                else
                {
                    BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Pr_Cargar_Lista_Cuentas(initObject, unit, Frm_Destino_Fondos.L_Cuentas, Frm_Destino_Fondos.L_Mnd, Frm_Destino_Fondos.Indice_Cuenta);
                    L_Cuentas_Click(initObject);
                }

            }
            else
            {
                if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
                {
                    n = 0;
                    MODXORI.Vx_OriCC = new T_OriCC[n + 1];
                    for (i = 0; i <= (short)n; i++)
                    {
                        MODXORI.Vx_OriCC[i].ctacte = VB6Helpers.Trim(Mdl_Funciones_Varias.Lc_BaseNumber);
                        MODXORI.Vx_OriCC[i].MonExt = 0;
                        MODXORI.Vx_OriCC[i].Activa = 1;
                        MODXORI.Vx_OriCC[i].CodMnd = Mdl_Funciones_Varias.LC_MONEDA;
                    }

                    BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Pr_Cargar_Lista_Cuentas(initObject, unit, Frm_Destino_Fondos.L_Cuentas, Frm_Destino_Fondos.L_Mnd, 1);
                    L_Cuentas_Click(initObject);
                }

                if (Frm_Destino_Fondos.L_Cuentas.Items.Count == 0)
                    Frm_Destino_Fondos.L_Cuentas.Enabled = false;
            }
        }
        private static void Pr_Cargar_Destinos(InitializationObject initObject, UI_Combo Combox)
        {
            short i = 0;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            Combox.Items.Clear();
            for (i = 0; i <= (short)VB6Helpers.UBound(MODXVIA.VTDme); i++)
            {
                Combox.Items.Add(new UI_ComboItem()
                {
                    Value = MODGPYF1.Minuscula(VB6Helpers.Trim(MODXVIA.VTDme[i].DesDme)),
                    ID = MODXVIA.VTDme[i].CodDme.ToString(),
                    Data = MODXVIA.VTDme[i].CodDme
                });
            }
            if (Combox.Items.Count > 0)
            {
                Combox.ListIndex = 0;

            }
        }
        //****************************************************************************
        //   1.  Llena la lista l_Via ordenada por moneda.
        //****************************************************************************
        private static void Pr_Llena_l_via(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Destino_Fondos Frm_destino_Fondos = initObject.Frm_Destino_Fondos;
            T_MODXVIA MODXVIA = initObject.MODXVIA;

            short k = 0;
            short m = 0;
            short i = 0;
            Frm_destino_Fondos.l_via.Items.Clear();
            for (k = 0; k <= (short)(Frm_destino_Fondos.l_mto.Items.Count - 1); k++)
            {
                m = MODXVIA.VxMtoVia[int.Parse(Frm_destino_Fondos.l_mto.get_ItemData(k))].CodMon;
                for (i = 0; i <= (short)VB6Helpers.UBound(MODXVIA.VxVia); i++)
                {
                    if (MODXVIA.VxVia[i].CodMon == m && MODXVIA.VxVia[i].Status != T_MODXVIA.ExVia_Eli && MODXVIA.VxVia[i].Vuelto == MODXVIA.VgxVia.Vuelto)
                    {
                        Frm_destino_Fondos.l_via.Items.Add(Fn_Linea_l_via(initObject, i));
                    }

                }

            }
            Frm_destino_Fondos.l_via.ListIndex = Frm_destino_Fondos.l_via.Items.Count;
            l_via_Click(initObject, unit);
        }
        //****************************************************************************
        //   1.  Retorna una linea para la lista de Destinos de Fondos.
        //****************************************************************************
        private static UI_GridItem Fn_Linea_l_via(InitializationObject initObject, int Indice)
        {
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            UI_GridItem item = new UI_GridItem();

            item.ID = Indice.ToString();
            item.AddColumn("NomVia", MODXVIA.VxVia[Indice].NomVia);
            item.AddColumn("NemMon", MODXVIA.VxVia[Indice].NemMon);
            item.AddColumn("MtoTot", Format.FormatCurrency(MODXVIA.VxVia[Indice].MtoTot, T_MODGCON0.FormatoConDec));
            return item;
        }
        //****************************************************************************
        //   1.  Retorna la suma de las Vías para una moneda determinada.
        //****************************************************************************
        // UPGRADE_INFO (#0561): The 'Moneda' symbol was defined without an explicit "As" clause.
        private static double Fn_SumaVxVia(InitializationObject initObj, short Moneda, short Vuelto)
        {
            short i = 0;
            double suma = 0;
            for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODXVIA.VxVia); i++)
            {
                // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Moneda'. Consider using the GetDefaultMember6 helper method.
                if (initObj.MODXVIA.VxVia[i].Status != T_MODXVIA.ExVia_Eli && initObj.MODXVIA.VxVia[i].CodMon == (Moneda) &&
                    initObj.MODXVIA.VxVia[i].Vuelto == Vuelto)
                {
                    suma += initObj.MODXVIA.VxVia[i].MtoTot;
                }

            }

            return suma;
        }
        //****************************************************************************
        //   1.  True   : Si todo el Destino ha sido justificado.
        //   2.  False  : Lo contrario.
        //****************************************************************************
        private static short Fn_ViasOK(InitializationObject initObject)
        {
            T_MODXVIA MODXVIA = initObject.MODXVIA;

            short _retValue = 0;
            short i = 0;
            double dif = 0;
            //Ve si está todo justificado.
            _retValue = (short)(true ? -1 : 0);
            for (i = 0; i <= (short)VB6Helpers.UBound(MODXVIA.VxMtoVia); i++)
            {
                if (MODXVIA.VxMtoVia[i].Vuelto == MODXVIA.VgxVia.Vuelto)
                {
                    dif = Format.StringToDouble(Format.FormatCurrency((Fn_SumaVxVia(initObject, MODXVIA.VxMtoVia[i].CodMon, MODXVIA.VgxVia.Vuelto) - MODXVIA.VxMtoVia[i].MtoTot), "0.00"));
                    if (dif != 0)
                    {
                        return (short)(false ? -1 : 0);
                    }

                }

            }

            return _retValue;
        }
        public static dynamic Pr_CargaDESTINOS(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;

            //SEGUN MONEDA ES EL TIPO DE CUENTA
            Frm_Destino_Fondos.Text1[0].Text = "";
            Frm_Destino_Fondos.Text1[1].Text = "";
            Frm_Destino_Fondos.Text1[2].Text = "";
            Frm_Destino_Fondos.Text1[3].Text = "";
            Frm_Destino_Fondos.Text1[4].Text = "";

            if (Frm_Destino_Fondos.L_Mnd.get_ItemData_(Frm_Destino_Fondos.L_Mnd.ListIndex) == T_MODGTAB0.MndNac)
            {
                MODXORI.Carga_l_cta(initObject, Frm_Destino_Fondos.L_Cta, 1, false, true, -1, unit);
            }
            else
            {
                MODXORI.Carga_l_cta(initObject, Frm_Destino_Fondos.L_Cta, 2, false, true, -1, unit);
            }



            Frm_Destino_Fondos.Text1[0].Text = Mdl_Funciones_Varias.LC_ORD_INST1;
            Frm_Destino_Fondos.Text1[1].Text = Mdl_Funciones_Varias.LC_PMNT_DET1;
            Frm_Destino_Fondos.Text1[2].Text = Mdl_Funciones_Varias.LC_PMNT_DET2;
            Frm_Destino_Fondos.Text1[3].Text = Mdl_Funciones_Varias.LC_PMNT_DET3;
            Frm_Destino_Fondos.Text1[4].Text = Mdl_Funciones_Varias.LC_CONREFNUM;  // Contract reference number

            Frm_Destino_Fondos.frm_datos.Visible = true;

            return null;
        }
        private static void Pr_Cargar_Beneficiario(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            short k = 0;
            for (k = 0; k <= initObj.MODGCVD.Beneficiario.Length - 1; k++)
            {
                if ((VB6Helpers.Instr(initObj.MODXORI.VgxOri.Partys, VB6Helpers.Trim(VB6Helpers.Str(k)))) != 0 || (VB6Helpers.Instr(initObj.MODXVIA.VgxVia.Partys, VB6Helpers.Trim(VB6Helpers.Str(k))) != 0))
                {
                    initObj.Frm_Destino_Fondos.L_Partys.Items.Add(new UI_ComboItem { Value = initObj.MODGCVD.Beneficiario[k], ID = k.ToString(), Data = k });
                }

            }

            if (initObj.Frm_Destino_Fondos.L_Partys.Items.Count > 0)
            {
                initObj.Frm_Destino_Fondos.L_Partys.ListIndex = 0;
                L_Partys_Click(initObj, unit);
            }

        }
        private static void Cargar_CodTran(InitializationObject initObject, string Tipcta, string Moneda, string CRDR)
        {
            UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_MODCVDIM MODCVDIM = initObject.MODCVDIM;

            short i = 0;
            Frm_Destino_Fondos.cmb_codtran.Items.Clear();
            for (i = 0; i <= (short)VB6Helpers.UBound(MODXORI.Vx_CodTran); i++)
            {
                if (MODXORI.Vx_CodTran[i].tip_cta == Tipcta && VB6Helpers.Instr(MODXORI.Vx_CodTran[i].Moneda, Moneda) > 0 && MODXORI.Vx_CodTran[i].cr_dr == CRDR)
                {
                    Frm_Destino_Fondos.cmb_codtran.Items.Add(new UI_ComboItem()
                    {
                        Value = MODGPYF1.Minuscula(VB6Helpers.Trim(MODXORI.Vx_CodTran[i].glosa_cosmos)),
                        Data = MODXORI.Vx_CodTran[i].nro_trx
                    });
                    VB6Helpers.RedimPreserve(ref MODCVDIM.CtaCCDin, 0, VB6Helpers.UBound(MODCVDIM.CtaCCDin) + 1);
                    MODCVDIM.CtaCCDin[Frm_Destino_Fondos.cmb_codtran.Items.Count - 1].codtra = MODXORI.Vx_CodTran[i].cod_trx_cosmos;
                    if (VB6Helpers.Instr(Moneda, "CLP") > 0)
                    {
                        MODCVDIM.CtaCCDin[Frm_Destino_Fondos.cmb_codtran.Items.Count - 1].NemCta = Busca_OVD_MN(initObject, VB6Helpers.Format(MODCVDIM.CtaCCDin[Frm_Destino_Fondos.cmb_codtran.Items.Count - 1].codtra, "00000"), Moneda, i);
                    }
                    else
                    {
                        MODCVDIM.CtaCCDin[Frm_Destino_Fondos.cmb_codtran.Items.Count - 1].NemCta = Busca_OVD_ME(initObject, VB6Helpers.Format(MODCVDIM.CtaCCDin[Frm_Destino_Fondos.cmb_codtran.Items.Count - 1].codtra, "00000"), Moneda, i);
                    }

                    MODCVDIM.CtaCCDin[Frm_Destino_Fondos.cmb_codtran.Items.Count - 1].NumCta = VB6Helpers.Trim(VB6Helpers.Str(Frm_Destino_Fondos.cmb_codtran.Items.Count - 1));
                }

            }

            if (Frm_Destino_Fondos.cmb_codtran.Items.Count > 0)
            {
                Frm_Destino_Fondos.cmb_codtran.ListIndex = 0;
                cmb_codtran_Click(initObject);
            }

        }
        private static string Busca_OVD_MN(InitializationObject initObj, string codtra, string Moneda, short Indice)
        {
            short c = 0;
            for (c = 0; c <= initObj.MODXORI.VOvd.Length - 1; c++)
            {
                if (VB6Helpers.Instr(initObj.MODXORI.VOvd[c].codtra, codtra) > 0)
                {
                    if (initObj.MODXORI.VOvd[c].monnac == -1)
                    {
                        return initObj.MODXORI.VOvd[c].NemCta;
                    }
                }
                else
                {
                    if (initObj.MODXORI.VOvd[c].NumCta == T_MODGCON0.IdCta_CtaCteMANE && string.IsNullOrWhiteSpace(initObj.MODXORI.VOvd[c].codtra) && Moneda != "CLP")
                    {
                        return initObj.MODXORI.VOvd[c].NemCta;
                    }
                    else if (initObj.MODXORI.VOvd[c].NumCta == T_MODGCON0.IdCta_CtaCteMANN && string.IsNullOrWhiteSpace(initObj.MODXORI.VOvd[c].codtra) && Moneda == "CLP")
                    {
                        return initObj.MODXORI.VOvd[c].NemCta;
                    }
                }
            }

            return "";
        }
        private static string Busca_OVD_ME(InitializationObject initObj, string codtra, string Moneda, short Indice)
        {
            short c = 0;
            for (c = 0; c <= initObj.MODXORI.VOvd.Length - 1; c++)
            {
                if (VB6Helpers.Instr(initObj.MODXORI.VOvd[c].codtra, codtra) > 0)
                {
                    if (initObj.MODXORI.VOvd[c].monnac == 0)
                    {
                        return initObj.MODXORI.VOvd[c].NemCta;
                    }

                }
                else
                {
                    if (initObj.MODXORI.VOvd[c].NumCta == T_MODGCON0.IdCta_CtaCteMANE && string.IsNullOrWhiteSpace(initObj.MODXORI.VOvd[c].codtra) && Moneda != "CLP")
                    {
                        return initObj.MODXORI.VOvd[c].NemCta;
                    }
                    else if (initObj.MODXORI.VOvd[c].NumCta == T_MODGCON0.IdCta_CtaCteMANN && string.IsNullOrWhiteSpace(initObj.MODXORI.VOvd[c].codtra) && Moneda == "CLP")
                    {
                        return initObj.MODXORI.VOvd[c].NemCta;
                    }
                }
            }

            return "";
        }
        private static void LimpiaNumPartida(InitializationObject initObj)
        {
            // Limpia los valores y oculta objetos.
            initObj.Frm_Destino_Fondos.LB_Referencia.Visible = false;
            initObj.Frm_Destino_Fondos.txtNumRef.Text = string.Empty;
            initObj.Frm_Destino_Fondos.txtNumRef.Visible = false;
        }
        //****************************************************************************
        //   1.  Si no están cargados los montos de las Vías => Se cargan.
        //****************************************************************************
        private static void Pr_Carga_l_mto(UI_Grid Lista, InitializationObject initObject)
        {
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            Lista.Items.Clear();
            short i = 0;
            string s = "";
            //Si no están cargados los montos de las Vías => Se cargan.-
            for (i = 0; i <= (short)VB6Helpers.UBound(MODXVIA.VxMtoVia); i++)
            {
                if ((~MODXVIA.VgxVia.Vuelto | (MODXVIA.VgxVia.Vuelto & MODXVIA.VxMtoVia[i].Vuelto)) != 0)
                {
                    Lista.Items.Add(Fn_Linea_l_mto(initObject, i));
                }

            }

            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Lista'. Consider using the GetDefaultMember6 helper method.
            if (Lista.Items.Count > 0)
            {
                Lista.ListIndex = 0;
            }

        }
        //****************************************************************************
        //   1.  Retorna una linea para la lista de montos de las Vías.
        //****************************************************************************
        private static UI_GridItem Fn_Linea_l_mto(InitializationObject initObject, short Indice)
        {
            T_MODXVIA MODXVIA = initObject.MODXVIA;

            var item = new UI_GridItem();
            item.AddColumn("NemMon", MODXVIA.VxMtoVia[Indice].NemMon);
            item.AddColumn("MtoTot", Format.FormatCurrency(MODXVIA.VxMtoVia[Indice].MtoTot, T_MODGCON0.FormatoConDec));
            item.ID = Indice.ToString();
            return item;
        }

        /// <summary>
        /// Valida los campos de existen dentro de la pantalla.
        /// </summary>
        /// <param name="initObject"></param>
        /// <param name="unit"></param>
        /// <param name="Campo_Inicial"></param>
        /// <param name="Campo_Final"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private static short Fn_Validar_Campos(InitializationObject initObject, UnitOfWorkCext01 unit, dynamic Campo_Inicial, dynamic Campo_Final, short flag)
        {
            using (var trace = new Tracer("Fn_Validar_Campos"))
            {
                trace.AddToContext("Fn_Validar_Campos", "Valida los campos de existen dentro de la pantalla desde Destino");
                T_MODXORI MODXORI = initObject.MODXORI;
                T_MODGUSR MODGUSR = initObject.MODGUSR;
                T_MODXVIA MODXVIA = initObject.MODXVIA;
                T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
                T_Module1 Module1 = initObject.Module1;
                T_MODGNCTA MODGNCTA = initObject.MODGNCTA;
                UI_Frm_Destino_Fondos Frm_Destino_Fondos = initObject.Frm_Destino_Fondos;

                short _retValue = 0;
                dynamic num_par = null;
                dynamic nums = null;
                string dv = string.Empty;
                short i = 0;
                string a = string.Empty;
                short b = 0;
                string razon = string.Empty, Texto = string.Empty, el_dv = string.Empty, msgAlert = string.Empty;
                _retValue = 0;

                for (i = VB6Helpers.CShort(Campo_Inicial); i <= VB6Helpers.CShort(Campo_Final); i++)
                {
                    switch (i)
                    {
                        case 0:  //Tx_Datos(0)
                            short _switchVar1 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                            if (_switchVar1 == T_MODGCON0.IdCta_SCSMN || _switchVar1 == T_MODGCON0.IdCta_SCSME)
                            {
                                if (string.IsNullOrWhiteSpace(Frm_Destino_Fondos.Tx_Datos[0].Text))
                                {
                                    msgAlert = "Es necesario que ingrese una Oficina.";
                                    trace.TraceError(msgAlert);
                                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = msgAlert,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Destino_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }

                                if (VB6Helpers.IsNumeric(VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[0].Text)))
                                {
                                    Frm_Destino_Fondos.Tx_Datos[0].MaxLength = 0;
                                    a = BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_Buscar_Suc(initObject, VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[0].Text));
                                    if (!string.IsNullOrWhiteSpace(a))
                                    {
                                        if (VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[0].Text) == MODGPYF0.SyGet_OfiCod(unit, MODGUSR.UsrEsp.CentroCosto))
                                        {
                                            msgAlert = "La oficina de destino no puede ser igual al origen.";
                                            trace.TraceError(msgAlert);
                                            Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                            {
                                                Type = TipoMensaje.Error,
                                                Text = msgAlert,
                                                ControlName = "Tx_Datos_0"
                                            });
                                            Frm_Destino_Fondos.Tx_Datos[0].Enabled = true;
                                            return _retValue;
                                        }
                                        else
                                        {
                                            Frm_Destino_Fondos.Lb_Oficina.Text = a;
                                        }
                                    }
                                    else
                                    {
                                        msgAlert = "Ingresar nuevo código: Código de Oficina no válido.";
                                        trace.TraceError(msgAlert);
                                        Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                        {
                                            Type = TipoMensaje.Error,
                                            Text = msgAlert,
                                            ControlName = "Tx_Datos_0"
                                        });
                                        Frm_Destino_Fondos.Tx_Datos[0].Enabled = true;

                                        return _retValue;
                                    }
                                }
                            }
                            else if (_switchVar1 == T_MODGCON0.IdCta_CTAORD || _switchVar1 == T_MODGCON0.IdCta_DIVENPEN || _switchVar1 == T_MODGCON0.IdCta_CHVBNYM || _switchVar1 == 54)
                            {
                                //Cta. Cte. Banco Central -- 'Corresponsal Cuenta Ordinaria
                                if (string.IsNullOrWhiteSpace(Frm_Destino_Fondos.Tx_Datos[0].Text))
                                {
                                    msgAlert = "Es necesario que se ingrese un Banco para poder realizar la operación.";
                                    trace.TraceError(msgAlert);
                                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = msgAlert,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Destino_Fondos.Tx_Datos[0].Enabled = true;

                                    return _retValue;
                                }

                                b = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VNom(initObject, unit, VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[0].Text), MODXVIA.VxMtoVia[Frm_Destino_Fondos.Indice_Monto].CodMon);
                                if (b == -1)
                                {
                                    msgAlert = "El Banco que Usted acaba de ingresar no es Corresponsal o no maneja la Moneda que Usted esta enviando.";
                                    trace.TraceError(msgAlert);
                                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = msgAlert,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Destino_Fondos.Tx_Datos[0].Enabled = true;

                                    return _retValue;
                                }
                                else
                                {
                                    if (MODGTAB0.VNom[b].Nom_Ala == 1)
                                    {
                                        msgAlert = "El Banco que Usted acaba de ingresar es Aladi, por lo tanto se le solicita ingresar otro Banco que no sea Aladi.";
                                        trace.TraceError(msgAlert);
                                        Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                        {
                                            Type = TipoMensaje.Error,
                                            Text = msgAlert,
                                            ControlName = "Tx_Datos_0"
                                        });
                                        Frm_Destino_Fondos.Tx_Datos[0].Enabled = true;

                                        return _retValue;
                                    }
                                }
                            }
                            else if (_switchVar1 >= 40 && _switchVar1 <= 53)
                            {
                                //Cuentas de Obligaciones y Check Verification
                                if (string.IsNullOrWhiteSpace(Frm_Destino_Fondos.Tx_Datos[0].Text))
                                {
                                    msgAlert = "Es necesario que se ingrese un Banco para poder realizar la operación.";
                                    trace.TraceError(msgAlert);
                                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = msgAlert,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Destino_Fondos.Tx_Datos[0].Enabled = true;

                                    return _retValue;
                                }

                                b = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VAcr(initObject, unit, VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[0].Text), MODXVIA.VxMtoVia[Frm_Destino_Fondos.Indice_Monto].CodMon);
                                if (b == 0)
                                {
                                    msgAlert = "El Banco que Usted acaba de ingresar no es Acreedor o no maneja la Moneda.";
                                    trace.TraceError(msgAlert);
                                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = msgAlert,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Destino_Fondos.Tx_Datos[0].Enabled = true;

                                    return _retValue;
                                }
                            }
                            else if (_switchVar1 == T_MODGCON0.IdCta_BOEREG || _switchVar1 == T_MODGCON0.IdCta_CHEREG || _switchVar1 == T_MODGCON0.IdCta_OBLREG || _switchVar1 == T_MODGCON0.IdCta_OBLARE || _switchVar1 == T_MODGCON0.IdCta_ACEREG)
                            {
                                if (Module1.Codop.Cent_Costo == "729" || Module1.Codop.Cent_Costo == "829" || Module1.Codop.Cent_Costo == "827" || Module1.Codop.Cent_Costo == "826")
                                {
                                    msgAlert = "Esta Cuenta está habilitada solo para REGIONES";
                                    trace.TraceError(msgAlert);
                                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = msgAlert,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    return _retValue;
                                }

                                if (string.IsNullOrWhiteSpace(Frm_Destino_Fondos.Tx_Datos[0].Text))
                                {
                                    msgAlert = "Es necesario que se ingrese un Banco para poder realizar la operación.";
                                    trace.TraceError(msgAlert);
                                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = msgAlert,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Destino_Fondos.Tx_Datos[0].Enabled = true;

                                    return _retValue;
                                }

                                b = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VNom(initObject, unit, VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[0].Text), MODXVIA.VxMtoVia[Frm_Destino_Fondos.Indice_Monto].CodMon);
                                if (b == -1)
                                {
                                    msgAlert = "El Banco que Usted acaba de ingresar no es Corresponsal o no maneja la Moneda que Usted esta enviando.";
                                    trace.TraceError(msgAlert);
                                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = msgAlert,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Destino_Fondos.Tx_Datos[0].Enabled = true;

                                    return _retValue;
                                }
                                else
                                {
                                    if (MODGTAB0.VNom[b].Nom_Ala == 1)
                                    {
                                        msgAlert = "El Banco que Usted acaba de ingresar es Aladi, por lo tanto se le solicita ingresar otro Banco que no sea Aladi.";
                                        trace.TraceError(msgAlert);
                                        Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                        {
                                            Type = TipoMensaje.Error,
                                            Text = msgAlert,
                                            ControlName = "Tx_Datos_0"
                                        });
                                        Frm_Destino_Fondos.Tx_Datos[0].Enabled = true;

                                        return _retValue;
                                    }
                                }
                            }
                            else if (_switchVar1 == T_MODGCON0.IdCta_CTACTEBC)
                            {
                                //Cta. Cte. Banco Central.-
                                if (string.IsNullOrWhiteSpace(Frm_Destino_Fondos.Tx_Datos[0].Text))
                                {
                                    msgAlert = "Es necesario que se ingrese un Banco para poder realizar la operación.";
                                    trace.TraceError(msgAlert);
                                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = msgAlert,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Destino_Fondos.Tx_Datos[0].Enabled = true;

                                    return _retValue;
                                }

                                b = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VNom(initObject, unit, VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[0].Text), MODXVIA.VxMtoVia[Frm_Destino_Fondos.Indice_Monto].CodMon);
                                if (b != -1)
                                {
                                    if (MODGTAB0.VNom[b].Nom_Ala == 1 && b != -1)
                                    {
                                        msgAlert = "El Banco que Usted acaba de ingresar es Aladi, por lo tanto se le solicita ingresar otro Banco que no sea Aladi.";
                                        trace.TraceError(msgAlert);
                                        Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                        {
                                            Type = TipoMensaje.Error,
                                            Text = msgAlert,
                                            ControlName = "Tx_Datos_0"
                                        });
                                        Frm_Destino_Fondos.Tx_Datos[0].Enabled = true;

                                        return _retValue;
                                    }
                                }

                            }
                            else if (_switchVar1 == T_MODGCON0.IdCta_VAM || _switchVar1 == T_MODGCON0.IdCta_VAX || _switchVar1 == T_MODGCON0.IdCta_VAMC || _switchVar1 == T_MODGCON0.IdCta_VAMCC || _switchVar1 == T_MODGCON0.IdCta_VASC)
                            {
                                //Varios Acreedores Import. -- Varios Acreedores Export. -- 'Varios Acreedores Mcdo. Corr.
                                if (string.IsNullOrWhiteSpace(Frm_Destino_Fondos.Tx_Datos[0].Text))
                                {
                                    msgAlert = "Es necesario que se ingrese un Participante para poder realizar la operación.";
                                    trace.TraceError(msgAlert);
                                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = msgAlert,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Destino_Fondos.Tx_Datos[0].Enabled = true;

                                    return _retValue;
                                }

                                razon = BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.SyGet_Partys(unit, VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[0].Text));
                                Frm_Destino_Fondos.Lb_Oficina.Text = MODGPYF1.Minuscula(VB6Helpers.Trim(razon));
                                if (string.IsNullOrWhiteSpace(razon))
                                {
                                    msgAlert = "El Participante que usted acaba de ingresar no existe dentro de los registros de los Partys.";
                                    trace.TraceError(msgAlert);
                                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = msgAlert,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Destino_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }

                            }
                            else if (_switchVar1 == T_MODGCON0.IdCta_ONMN || _switchVar1 == T_MODGCON0.IdCta_ONME)
                            {
                                //Otro Nemónico M/N ---- Otro Nemónico M/E
                                if (string.IsNullOrWhiteSpace(Frm_Destino_Fondos.Tx_Datos[0].Text))
                                {
                                    msgAlert = "Es necesario que se ingrese un Código de Nemónico para poder realizar la operación.";
                                    trace.TraceError(msgAlert);
                                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = msgAlert,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Destino_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }

                                b = (short)BCH.Comex.Core.BL.XCFT.Modulos.MODGNCTA.Get_Cta(VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[0].Text), initObject, unit);
                                if (b == -1 || b == 0)
                                {
                                    msgAlert = "El Nemónico que Usted acaba de ingresar no se encuentra dentro de las Cuentas Contables.";
                                    trace.TraceError(msgAlert);
                                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = msgAlert,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Destino_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }
                                else
                                {
                                    /// Valida si la cuenta contable tiene o no nro de cuenta
                                    if (string.IsNullOrWhiteSpace(MODGNCTA.VCta[b].Cta_Num))
                                    {
                                        msgAlert = "No es posible obtener número de cuenta contable, favor de volver a repertir la acción hasta obtener número.";
                                        trace.TraceError(msgAlert);
                                        Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                        {
                                            Type = TipoMensaje.Error,
                                            Text = msgAlert,
                                            ControlName = "Tx_Datos_0"
                                        });
                                        Frm_Destino_Fondos.Tx_Datos[0].Enabled = true;
                                        return _retValue;
                                    }

                                    short _switchVar2 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                                    if (_switchVar2 == T_MODGCON0.IdCta_ONMN)
                                    {
                                        if (MODGNCTA.VCta[b].Cta_Mon == 1)
                                        {
                                            msgAlert = "Usted seleccionó una Cuenta Nacional, por lo tanto el Nemónico que tendría que ingresar debería ser Nacional.";
                                            trace.TraceError(msgAlert);
                                            Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                            {
                                                Type = TipoMensaje.Error,
                                                Text = msgAlert,
                                                ControlName = "Tx_Datos_0"
                                            });
                                            Frm_Destino_Fondos.Tx_Datos[0].Enabled = true;
                                            Frm_Destino_Fondos.Lb_Oficina.Text = "";
                                            return _retValue;
                                        }
                                        else
                                        {
                                            Frm_Destino_Fondos.Lb_Oficina.Text = VB6Helpers.Trim(MODGNCTA.VCta[b].Cta_Nom.Value);
                                        }

                                    }
                                    else if (_switchVar2 == T_MODGCON0.IdCta_ONME)
                                    {
                                        if (MODGNCTA.VCta[b].Cta_Mon == 2)
                                        {
                                            msgAlert = "Usted seleccionó una Cuenta Extranjera, por lo tanto el Nemónico que tendría que ingresar debería ser Extranjero.";
                                            trace.TraceError(msgAlert);
                                            Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                            {
                                                Type = TipoMensaje.Error,
                                                Text = msgAlert,
                                                ControlName = "Tx_Datos_0"
                                            });
                                            Frm_Destino_Fondos.Tx_Datos[0].Enabled = true;
                                            Frm_Destino_Fondos.Lb_Oficina.Text = "";
                                            return _retValue;
                                        }
                                        else
                                        {
                                            Frm_Destino_Fondos.Lb_Oficina.Text = VB6Helpers.Trim(MODGNCTA.VCta[b].Cta_Nom.Value);
                                        }
                                    }
                                }
                            }
                            //************************************************************
                            break;
                        case 1:  //Tx_Datos(1)
                            short _switchVar3 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                            if (_switchVar3 == T_MODGCON0.IdCta_SCSMN || _switchVar3 == T_MODGCON0.IdCta_SCSME)
                            {
                                if (flag != 0)
                                {
                                    if (!string.IsNullOrWhiteSpace(Frm_Destino_Fondos.Tx_Datos[1].Text))
                                    {
                                        Texto = "01;02;03;04";
                                        if (VB6Helpers.Instr(Texto, VB6Helpers.Format(Frm_Destino_Fondos.Tx_Datos[1].Text, "00")) == 0)
                                        {
                                            msgAlert = "El Tipo de Movimiento debe estar entre 1 y 4.";
                                            trace.TraceError(msgAlert);
                                            Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                            {
                                                Type = TipoMensaje.Error,
                                                Text = msgAlert,
                                                ControlName = "Tx_Datos_1"
                                            });
                                            Frm_Destino_Fondos.Tx_Datos[1].Enabled = true;

                                            return _retValue;
                                        }
                                    }

                                    double _switchVar4 = VB6Helpers.Val(Frm_Destino_Fondos.Tx_Datos[1].Text);
                                    if (_switchVar4 == T_MODXORI.TP_INI)
                                    {
                                        Pr_Generar_Automatica_Ini(initObject, unit); //Generación Automática
                                    }
                                    else if (_switchVar4 == T_MODXORI.TP_CON || _switchVar4 == T_MODXORI.TP_COR)
                                    {
                                        //Ingreso Manual
                                        Frm_Destino_Fondos.Tx_Datos[2].Enabled = true;
                                    }
                                    else if (_switchVar4 == T_MODXORI.TP_COM)
                                    {
                                    }
                                    else if (_switchVar4 == 0)
                                    {
                                        msgAlert = "Es necesario que ingrese un Tipo de Movimiento.";
                                        trace.TraceError(msgAlert);
                                        Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                        {
                                            Type = TipoMensaje.Error,
                                            Text = msgAlert,
                                            ControlName = "Tx_Datos_1"
                                        });
                                        Frm_Destino_Fondos.Tx_Datos[1].Enabled = true;
                                        return _retValue;
                                    }

                                }

                            }
                            else if (_switchVar3 == T_MODGCON0.IdCta_CTACTEBC || _switchVar3 == T_MODGCON0.IdCta_CTAORD || _switchVar3 == T_MODGCON0.IdCta_DIVENPEN || _switchVar3 == T_MODGCON0.IdCta_CHVBNYM || _switchVar3 == 54)
                            {
                                //Cta. Cte. Banco Central -- 'Corresponsal Cuenta Ordinaria
                                if (string.IsNullOrWhiteSpace(Frm_Destino_Fondos.Tx_Datos[1].Text))
                                {
                                    msgAlert = "Es necesario que se ingrese un Número de Referencia para poder realizar la operación.";
                                    trace.TraceError(msgAlert);
                                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = msgAlert,
                                        ControlName = "Tx_Datos_1"
                                    });
                                    Frm_Destino_Fondos.Tx_Datos[1].Enabled = true;
                                    return _retValue;
                                }

                                if (MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_CTACTEBC)
                                {
                                    if (~BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_Valida_Aladi(VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[1].Text), initObject) != 0)
                                    {
                                        Frm_Destino_Fondos.Tx_Datos[1].Enabled = true;
                                        return _retValue;
                                    }

                                }

                            }
                            else if (_switchVar3 >= 40 && _switchVar3 <= 53)
                            {
                                //Cuentas de Obligaciones y Check Verification
                                if (string.IsNullOrWhiteSpace(Frm_Destino_Fondos.Tx_Datos[1].Text))
                                {
                                    msgAlert = "Es necesario que se ingrese un Número de Referencia para poder realizar la operación.";
                                    trace.TraceError(msgAlert);
                                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = msgAlert,
                                        ControlName = "Tx_Datos_1"
                                    });
                                    Frm_Destino_Fondos.Tx_Datos[1].Enabled = true;
                                    return _retValue;
                                }

                                if (MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_CTACTEBC)
                                {
                                    if (~BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_Valida_Aladi(VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[1].Text), initObject) != 0)
                                    {
                                        Frm_Destino_Fondos.Tx_Datos[1].Enabled = true;
                                        return _retValue;
                                    }

                                }

                            }
                            else if (_switchVar3 == T_MODGCON0.IdCta_BOEREG || _switchVar3 == T_MODGCON0.IdCta_CHEREG || _switchVar3 == T_MODGCON0.IdCta_OBLREG || _switchVar3 == T_MODGCON0.IdCta_OBLARE || _switchVar3 == T_MODGCON0.IdCta_ACEREG)
                            {
                                if (Module1.Codop.Cent_Costo == "729" || Module1.Codop.Cent_Costo == "829" || Module1.Codop.Cent_Costo == "827" || Module1.Codop.Cent_Costo == "826")
                                {
                                    msgAlert = "Esta Cuenta está habilitada solo para REGIONES";
                                    trace.TraceError(msgAlert);
                                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = "Esta Cuenta está habilitada solo para REGIONES",
                                        ControlName = "Tx_Datos_1"
                                    });
                                    return _retValue;
                                }

                                if (string.IsNullOrWhiteSpace(Frm_Destino_Fondos.Tx_Datos[1].Text))
                                {
                                    msgAlert = "Es necesario que se ingrese un Número de Referencia para poder realizar la operación.";
                                    trace.TraceError(msgAlert);
                                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = msgAlert,
                                        ControlName = "Tx_Datos_1"
                                    });
                                    Frm_Destino_Fondos.Tx_Datos[1].Enabled = true;
                                    return _retValue;
                                }

                                if (MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_CTACTEBC)
                                {
                                    if (~BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_Valida_Aladi(VB6Helpers.Trim(Frm_Destino_Fondos.Tx_Datos[1].Text), initObject) != 0)
                                    {
                                        Frm_Destino_Fondos.Tx_Datos[1].Enabled = true;
                                        return _retValue;
                                    }

                                }

                            }

                            //******************************************
                            break;
                        case 2:  //Tx_Datos(2)
                            short _switchVar5 = MODXORI.VOvd[Frm_Destino_Fondos.Indice_Cuenta].NumCta;
                            if (_switchVar5 == T_MODGCON0.IdCta_SCSMN || _switchVar5 == T_MODGCON0.IdCta_SCSME)
                            {
                                //Saldos c/ Sucursales M/N.
                                if (!VB6Helpers.IsNumeric(Frm_Destino_Fondos.Tx_Datos[2].Text))
                                {
                                    msgAlert = "Corregir Número de Partida: Se debe ingresar sólo números.";
                                    trace.TraceError(msgAlert);
                                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = msgAlert,
                                        ControlName = "Tx_Datos_2"
                                    });
                                    Frm_Destino_Fondos.Tx_Datos[2].Enabled = true;
                                    return _retValue;
                                }

                                num_par = Frm_Destino_Fondos.Tx_Datos[2].Text;
                                nums = VB6Helpers.Left(VB6Helpers.CStr(num_par), 6);
                                el_dv = VB6Helpers.Right(VB6Helpers.CStr(num_par), 2);
                                dv = BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_Calcula_Dig(VB6Helpers.Trim(VB6Helpers.CStr(nums)));
                                if (dv != el_dv)
                                {
                                    msgAlert = "Corregir Número de Partida: Dígito Verificador no corresponde.";
                                    trace.TraceError(msgAlert);
                                    Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = msgAlert,
                                        ControlName = "Tx_Datos_2"
                                    });
                                    Frm_Destino_Fondos.Tx_Datos[2].Enabled = true;
                                    return _retValue;
                                }
                            }
                            //**************************************************
                            break;
                        case 3:  //L_Cuentas
                            if (Frm_Destino_Fondos.L_Cuentas.ListIndex == -1 && Frm_Destino_Fondos.L_Cuentas.Items.Count > 0)
                            {
                                msgAlert = "Es Necesario que seleccione una Cuenta.";
                                trace.TraceError(msgAlert);
                                Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Error,
                                    Text = msgAlert,
                                    ControlName = "L_Cuentas"
                                });
                                Frm_Destino_Fondos.L_Cuentas.Enabled = true;
                                return _retValue;
                            }

                            if (Frm_Destino_Fondos.L_Cuentas.Items.Count == 0)
                            {
                                msgAlert = "El Cliente NO tiene Cuenta Corriente en la moneda especificada.";
                                trace.TraceError(msgAlert);
                                Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Error,
                                    Text = msgAlert,
                                    ControlName = "L_Cuentas"
                                });
                                return _retValue;
                            }

                            //**************************************************
                            break;
                        case 4:  //L_Partys
                            if (Frm_Destino_Fondos.L_Partys.ListIndex == -1)
                            {
                                msgAlert = "Es Necesario que seleccione un Partys.";
                                trace.TraceError(msgAlert);
                                Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Error,
                                    Text = "Es Necesario que seleccione un Partys.",
                                    ControlName = "L_Partys"
                                });
                                Frm_Destino_Fondos.L_Partys.Enabled = true;
                                return _retValue;
                            }

                            //**************************************************
                            break;
                        case 5:  //Cb_Destino
                            if (Frm_Destino_Fondos.Cb_Destino.ListIndex == -1 && Frm_Destino_Fondos.Cb_Destino.Enabled)
                            {
                                msgAlert = "Es Necesario que seleccione un Destino de Fondos.";
                                trace.TraceError(msgAlert);
                                Frm_Destino_Fondos.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Error,
                                    Text = msgAlert,
                                    ControlName = "Cb_Destino"
                                });
                                Frm_Destino_Fondos.Cb_Destino.Enabled = true;
                                return _retValue;
                            }

                            break;
                    }
                }

                return 1;
            }
            #endregion
        }
    }
}
