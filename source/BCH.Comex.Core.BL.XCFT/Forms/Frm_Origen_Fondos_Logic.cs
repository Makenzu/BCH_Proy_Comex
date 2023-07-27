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
    public class Frm_Origen_Fondos_Logic
    {
        #region PUBLICOS
        public static void Form_Load(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODGMTA MODGMTA = initObject.MODGMTA;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            T_ModChVrf ModChVrf = initObject.ModChVrf;
            T_MODXORI MODXORI = initObject.MODXORI;
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;
            initObject.Frm_Origen_Fondos.Boton[0].Enabled = false;

            short[] Tabs = null;
            short i = 0;
            short n = 0;
            short b = 0;
            short bandpartys = 0;

            MODXORI.ori_des = "ori";
            Frm_Origen_Fondos.l_mto.Header = new List<string>() { "Moneda", "Monto" };
            Frm_Origen_Fondos.l_ori.Header = new List<string>() { "Orígen", "Moneda", "Monto" };
            ModChVrf.VgChV = new T_Chv[0];

            //Carga lista de montos.-
            Pr_Carga_l_mto(initObject, unit, Frm_Origen_Fondos.l_mto);

            //porque carga dos veces la moneda
            Frm_Origen_Fondos.l_mnd.Items.Clear();
            //Carga lista de monedas.-
            for (i = 0; i <= (short)VB6Helpers.UBound(MODXORI.VxMtoOri); i++)
            {
                n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, MODXORI.VxMtoOri[i].CodMon);
                Frm_Origen_Fondos.l_mnd.Items.Add(new UI_ComboItem()
                {
                    Data = MODGTAB0.VMnd[n].Mnd_MndCod,
                    Value = BCH.Comex.Core.BL.XCFT.Modulos.MODGPYF1.Minuscula(MODGTAB0.VMnd[n].Mnd_MndNom)
                });
            }

            if (Frm_Origen_Fondos.l_mnd.Items.Count > 0)
            {
                Frm_Origen_Fondos.l_mnd.ListIndex = 0;
                l_mnd_Click(initObject, unit);
            }

            ////Carga lista de origenes.-
            Frm_Origen_Fondos.l_ori.ListIndex = -1;
            l_ori_Click(initObject, unit);

            MODXORI.VgxOri.Acepto = (short)(false ? -1 : 0);
            Frm_Origen_Fondos.Boton[0].Enabled = false;
            b = BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.SyGetn_Suc(MODXORI, unit);

            //Deshabilita el boton del Impuesto en caso de que el flag este en 0
            if (T_MODGMTA.impflag == 0)
            {
                Frm_Origen_Fondos.Ch_ImpDeb.Value = (false ? -1 : 0);
                Frm_Origen_Fondos.Ch_ImpDeb.Enabled = false;
            }

            if ((MODXVIA.EsSolBcx & (VB6Helpers.Mid(MODXVIA.BcxEntrada2, 1, 1) == "0" ? -1 : 0)) != 0)
            {
                bandpartys = 1;
            }

            ModChVrf.AceptoPantallaChVrf = (short)(false ? -1 : 0);
            //----------------------------------------------------------------------------------------------------------

            //---------------------------------------------
            //Realsystems-Código Nuevo-Inicio
            //Fecha Modificación 20100623
            //Responsable: Pablo Millan
            //Versión: 1.0
            //Descripción : Se modifica generacion de CRN
            //---------------------------------------------
            if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 0)
            {
                Frm_Origen_Fondos.Text1[4].Text = VB6Helpers.Mid(Mdl_Funciones_Varias.LC_TRXID_MAN, 16, 10);  // Contract reference number
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

            short k = 0;

            if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
            {
                Carga_OriFondos(initObject, unit);
                Frm_Origen_Fondos.frm_datos.Enabled = false;
                Frm_Origen_Fondos.frm_datos.Visible = false;

                //'Busca en L_cta El indice de la cuenta automatica cosmos que sea Mon_Nacional o Mon_ext
                if (VB6Helpers.Int(Mdl_Funciones_Varias.LC_PRD) != 62)
                {
                    //'''' Outgoing = 72 - 74
                    for (k = 0; k <= (short)(Frm_Origen_Fondos.L_Cta.ListCount - 1); k++)
                    {
                        if ((MODXORI.VOvd[(Frm_Origen_Fondos.L_Cta.get_ItemData_(k))].NumCta == 62) || (MODXORI.VOvd[(Frm_Origen_Fondos.L_Cta.get_ItemData_(k))].NumCta == 63))
                        {
                            if (k != Frm_Origen_Fondos.L_Cta.ListIndex)
                            {
                                Frm_Origen_Fondos.L_Cta.ListIndex = k;  //'encontro indice 40
                                L_Cta_Click(initObject, unit);
                            }
                            if (Frm_Origen_Fondos.L_Partys.ListIndex != 0)
                            {
                                Frm_Origen_Fondos.L_Partys.ListIndex = 0;
                                L_Partys_Click(initObject, unit);
                            }
                            break;
                        }
                    }
                }
                //'Busca en L_cta El indice la cuenta Varios Acreedores Export
                if (VB6Helpers.Int(Mdl_Funciones_Varias.LC_PRD) == 62)
                {
                    //'''' Incoming = 62
                    for (k = 0; k <= (short)(Frm_Origen_Fondos.L_Cta.Items.Count - 1); k++)
                    {
                        if ((MODXORI.VOvd[(Frm_Origen_Fondos.L_Cta.get_ItemData_(k))].NumCta == 23))
                        {
                            if (Frm_Origen_Fondos.L_Cta.ListIndex != k)
                            {
                                Frm_Origen_Fondos.L_Cta.ListIndex = k;  //'encontro indice 6
                                L_Cta_Click(initObject, unit);
                            }
                        }
                    }
                }
                Frm_Origen_Fondos.Tx_Datos[0].Text = VB6Helpers.CStr(BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Rescata_Referencia(Mdl_Funciones_Varias.LC_MONEDA, unit));
                Frm_Origen_Fondos.Tx_Datos[1].Text = Mdl_Funciones_Varias.LC_CONREFNUM;
            }

            Frm_Origen_Fondos.CargaAutomatica = Mdl_Funciones_Varias.CARGA_AUTOMATICA;
        }

        public static void BNem_Click(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;
            UI_Frm_Cta Frm_Cta = initObject.Frm_Cta;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_MODCONT MODCONT = initObject.MODCONT;
            T_MODGNCTA MODGNCTA = initObject.MODGNCTA;


            if (!Frm_Origen_Fondos.VuelveDeNemonico)
            {
                short m = 0;
                // UPGRADE_WARNING (#80F4): The Screen6.MousePointer property sets or returns the MousePointer property of the active form, but only if it's a VB6Form.
                if (Frm_Origen_Fondos.l_mnd.ListIndex == -1)
                {
                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Debe seleccionar el monto para el cual se determinarán los O. de Fondo"
                    });
                    return;
                }

                m = (short)(Frm_Origen_Fondos.l_mnd.get_ItemData_((short)Frm_Origen_Fondos.l_mnd.ListIndex));
                if (m == 1)
                {
                    MODCONT.VCtaGl.moncta = 2;
                }
                else
                {
                    MODCONT.VCtaGl.moncta = 1;
                }

                if (~BCH.Comex.Core.BL.XCFT.Modulos.MODCONT.SyGetn_CtaCtb(initObject, unit) != 0)
                {
                    return;
                }
                MODCONT.VCtaGl.NemCta = "";
                initObject.FormularioQueAbrir = "NemonicoCuenta";
                initObject.Frm_Cta = new UI_Frm_Cta();
                initObject.Frm_Cta.VieneDe = "OrigenFondos";
                initObject.VieneDe = "OrigenFondos";
                Frm_Origen_Fondos.VuelveDeNemonico = true;
                return;
                //abrir frm_cta
            }
            else
            {
                short i = 0;
                short j = 0;
                short b = 0;

                Frm_Origen_Fondos.VuelveDeNemonico = false;
                //Verifica que Nemónico NO sea una Cuenta Especial.-
                for (i = 0; i <= (short)(Frm_Origen_Fondos.L_Cta.Items.Count - 1); i++)
                {
                    j = (short)(Frm_Origen_Fondos.L_Cta.get_ItemData_(i));

                    if (VB6Helpers.Trim(MODXORI.VOvd[j].NemCta) == VB6Helpers.Trim(MODCONT.VCtaGl.NemCta))
                    {

                        // Si la cuenta es vigenteable se permite el ingreso del numero de referencia
                        // de lo contrario debe limpiar y ocultar numero de partida
                        b = (short)BCH.Comex.Core.BL.XCFT.Modulos.MODGNCTA.Get_Cta(VB6Helpers.Trim(MODCONT.VCtaGl.NemCta), initObject, unit);
                        if (b != 0)
                        {
                            if ((MODGNCTA.VCta[b].Cta_Vig == 1))
                            {
                                Frm_Origen_Fondos.LB_Referencia.Visible = true;
                                Frm_Origen_Fondos.txtNumRef.Visible = true;
                            }
                            else
                            {
                                LimpiaNumPartida(initObject);
                            }

                        }

                        Frm_Origen_Fondos.L_Cta.ListIndex = i;
                        //Frm_Origen_Fondos.Tx_Datos[0].SetFocus();
                        return;
                    }

                }

                if (!string.IsNullOrWhiteSpace(MODCONT.VCtaGl.NemCta))
                {
                    Frm_Origen_Fondos.Tx_Datos[0].Text = VB6Helpers.Trim(MODCONT.VCtaGl.NemCta);
                    //Otro Nemónico M/N --- Otro Nemónico M/E.
                    if ((MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_ONMN) || (MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_ONME))
                    {
                        //Otro Nemónico M/N ---- Otro Nemónico M/E
                        b = (short)BCH.Comex.Core.BL.XCFT.Modulos.MODGNCTA.Get_Cta(VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text), initObject, unit);
                        if (b == 0)
                        {
                            Frm_Origen_Fondos.Lb_Oficina.Text = "";
                            //Validación Incorrecta
                            short _switchVar1 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                            if (_switchVar1 == T_MODGCON0.IdCta_ONMN)
                            {
                                Frm_Origen_Fondos.ONMN_OK = "00";
                            }
                            else if (_switchVar1 == T_MODGCON0.IdCta_ONME)
                            {
                                Frm_Origen_Fondos.ONME_OK = "00";
                            }

                        }
                        else
                        {
                            short _switchVar2 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                            if (_switchVar2 == T_MODGCON0.IdCta_ONMN)
                            {
                                if (MODGNCTA.VCta[b].Cta_Mon == 1)
                                {
                                    Frm_Origen_Fondos.Lb_Oficina.Text = "";
                                    //Validación Incorrecta
                                    short _switchVar3 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                                    if (_switchVar3 == T_MODGCON0.IdCta_ONMN)
                                    {
                                        Frm_Origen_Fondos.ONMN_OK = "00";
                                    }
                                    else if (_switchVar3 == T_MODGCON0.IdCta_ONME)
                                    {
                                        Frm_Origen_Fondos.ONME_OK = "00";
                                    }

                                }
                                else
                                {
                                    Frm_Origen_Fondos.Lb_Oficina.Text = MODGPYF1.Minuscula(VB6Helpers.Trim(MODGNCTA.VCta[b].Cta_Nom.Value));
                                    //Validación Correcta
                                    short _switchVar4 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                                    if (_switchVar4 == T_MODGCON0.IdCta_ONMN)
                                    {
                                        Frm_Origen_Fondos.ONMN_OK = "01";
                                    }
                                    else if (_switchVar4 == T_MODGCON0.IdCta_ONME)
                                    {
                                        Frm_Origen_Fondos.ONME_OK = "01";
                                    }

                                }

                            }
                            else if (_switchVar2 == T_MODGCON0.IdCta_ONME)
                            {
                                if (MODGNCTA.VCta[b].Cta_Mon == 2)
                                {
                                    Frm_Origen_Fondos.Lb_Oficina.Text = "";
                                    //Validación Incorrecta
                                    short _switchVar5 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                                    if (_switchVar5 == T_MODGCON0.IdCta_ONMN)
                                    {
                                        Frm_Origen_Fondos.ONMN_OK = "00";
                                    }
                                    else if (_switchVar5 == T_MODGCON0.IdCta_ONME)
                                    {
                                        Frm_Origen_Fondos.ONME_OK = "00";
                                    }

                                }
                                else
                                {
                                    Frm_Origen_Fondos.Lb_Oficina.Text = MODGPYF1.Minuscula(VB6Helpers.Trim(MODGNCTA.VCta[b].Cta_Nom.Value));
                                    //Validación Correcta
                                    short _switchVar6 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                                    if (_switchVar6 == T_MODGCON0.IdCta_ONMN)
                                    {
                                        Frm_Origen_Fondos.ONMN_OK = "01";
                                    }
                                    else if (_switchVar6 == T_MODGCON0.IdCta_ONME)
                                    {
                                        Frm_Origen_Fondos.ONME_OK = "01";
                                    }

                                }

                            }

                            // Si la cuenta es vigenteable se permite el ingreso del numero de referencia
                            // de lo contrario debe limpiar y ocultar numero de partida
                            if ((MODGNCTA.VCta[b].Cta_Vig == 1))
                            {
                                Frm_Origen_Fondos.LB_Referencia.Visible = true;
                                Frm_Origen_Fondos.txtNumRef.Visible = true;
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
                    #region Comentado
                    //if (Frm_Origen_Fondos.Tx_Datos[0].Enabled)
                    //{
                    //    Frm_Origen_Fondos.Tx_Datos[0].SetFocus();
                    //}
                    #endregion
                }
            }

        }

        public static void Boton_Click(InitializationObject initObject, short Index)
        {
            T_ModChVrf ModChVrf = initObject.ModChVrf;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            UI_Mdi_Principal mdi_Principal = initObject.Mdi_Principal;
            UI_Frm_Origen_Fondos Frm_Origenes_Fondos = initObject.Frm_Origen_Fondos;
            short a = 0;
            short HayChVrf = 0;
            short i = 0;
            short HayPlnChv = 0;

            a = (short)VB6Helpers.UBound(ModChVrf.VPlnChV);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            short cont = 0;
            short q = 0;
            switch (Index)
            {
                case 0:
                    //Ve si se han justificado los montos.-
                    if (~Fn_OrigenOK(initObject) != 0)
                    {
                        Frm_Origenes_Fondos.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Los montos NO han sido correctamente justificados. Debe asociar cada uno de ellos a una forma de pago."
                        });
                        return;
                    }

                    HayChVrf = (short)(false ? -1 : 0);
                    for (i = 0; i <= (short)VB6Helpers.UBound(MODXORI.VxOri); i++)
                    {
                        if (MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CHVRF)
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
                        Frm_Origenes_Fondos.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Debe definir las planillas para la cuenta Check Verification."
                        });
                        return;
                    }

                    MODXORI.VgxOri.Acepto = (short)(true ? -1 : 0);

                    if (Frm_Origenes_Fondos.Ch_ImpDeb.Value != 0)
                    {
                        MODXORI.VgxOri.ImpDeb = (short)(true ? -1 : 0);
                    }
                    else
                    {
                        MODXORI.VgxOri.ImpDeb = (short)(false ? -1 : 0);
                    }

                    if (MODXORI.VgxOri.HayVue != 0)
                    {

                        MODGCVD.VgCvd.Etapa += "VUE";
                        mdi_Principal.BUTTONS["tbr_vueltos"].Enabled = true;
                    }
                    else
                    {

                        mdi_Principal.BUTTONS["tbr_vueltos"].Enabled = false;
                        MODGCVD.VgCvd.Etapa = MODGPYF0.Componer(MODGCVD.VgCvd.Etapa, "VUE", "");
                    }

                    initObject.FormularioQueAbrir = "Index";
                    break;
                case 1:
                    MODXORI.VgxOri.Acepto = (short)(false ? -1 : 0);

                    MODXORI.VxOri = new T_xOri[0];
                    ModChVrf.VgChV = new T_Chv[0];
                    ModChVrf.VPlnChV = new T_PlnChV[0];

                    //elimina registros de Vx_SCodTran
                    for (q = 0; q < MODXORI.Vx_SCodTran.Length; q++)
                    {
                        if (MODXORI.Vx_SCodTran[q].Via == "ori")
                        {
                            cont = (short)(cont + 1);
                        }

                    }

                    VB6Helpers.RedimPreserve(ref MODXORI.Vx_SCodTran, 0, VB6Helpers.UBound(MODXORI.Vx_SCodTran) - cont);
                    Frm_Origenes_Fondos.ONMN_OK = "";
                    initObject.FormularioQueAbrir = "Index";
                    break;
            }
            Frm_Origenes_Fondos.ONMN_OK = "";
        }

        public static void Bt_PlnTrn_Click(InitializationObject initObject)
        {
            T_ModChVrf ModChVrf = initObject.ModChVrf;
            ModChVrf.CodigoIE = "I";
            initObject.Frm_ChVrf = new UI_Frm_ChVrf();
            initObject.FormularioQueAbrir = "PlanillasTransferencia";
            initObject.VieneDe = "OrigenFondos";
            initObject.Frm_Origen_Fondos.VuelveDeOtro = true;
        }

        public static void cmb_codtran_Click(InitializationObject initObject)
        {
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;

            short c = 0;
            short n = 0;

            for (c = 0; c <= (short)VB6Helpers.UBound(MODGTAB0.VMnd); c++)
            {
                if (MODGPYF1.Minuscula(MODGTAB0.VMnd[c].Mnd_MndNom) == MODGPYF1.Minuscula(Frm_Origen_Fondos.l_mnd.Text))
                {
                    MODXVIA.Moneda_TRX = VB6Helpers.Trim(MODGTAB0.VMnd[c].Mnd_MndSwf);
                    break;
                }

            }

            //==>> Se comento porque no hace nada
            //for (n = 0; n < Frm_Origen_Fondos.l_mto.Items.Count; n++)
            //{
            //    if (Frm_Origen_Fondos.l_mto.ListIndex==(n-1))
            //    {
            //        break;
            //    }

            //}

        }

        public static void Form_Activate(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            T_MODXORI MODXORI = initObject.MODXORI;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;
            //---------------- Fin Codigo Nuevo ----------------

            MODXORI.ori_des = "ori";
            Frm_Origen_Fondos.l_mnd.ListIndex = 0;
            l_mnd_Click(initObject, unit);
            if (!string.IsNullOrWhiteSpace(VB6Helpers.Mid(MODXVIA.VBcxCci2, 2600, 12)))
            {
                Frm_Origen_Fondos.L_Cta.ListIndex = 0;
                L_Cta_Click(initObject, unit);
                Frm_Origen_Fondos.L_Cuentas.ListIndex = MODGPYF0.PosListaLin(Frm_Origen_Fondos.L_Cuentas, VB6Helpers.Mid(MODXVIA.VBcxCci2, 2602, 3) + "-" + VB6Helpers.Mid(MODXVIA.VBcxCci2, 2605, 5) + "-" + VB6Helpers.Mid(MODXVIA.VBcxCci2, 2610, 2));
            }

        }

        public static void L_Cta_Click(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;

            short j = 0;
            short k = 0;
            short c = 0;

            initObject.MODCVDIM.CtaCCDin = new CtaCC[0];
            //Deshabilita todo.
            initObject.MODXVIA.Moneda_TRX = "";
            Frm_Origen_Fondos.frm_datos.Visible = false;
            Frm_Origen_Fondos.frm_datos.Enabled = true;
            Frm_Origen_Fondos.frm_infoctagap.Visible = false;
            Frm_Origen_Fondos.L_Cuentas.Visible = true;
            Frm_Origen_Fondos.Tx_Cuentas[5].Visible = true;

            Frm_Origen_Fondos.L_Partys.Items.Clear();
            Frm_Origen_Fondos.L_Cuentas.Items.Clear();
            Frm_Origen_Fondos.L_Partys.Enabled = false;
            Frm_Origen_Fondos.L_Cuentas.Enabled = false;
            Frm_Origen_Fondos.L_Partys.ListIndex = -1;
            Frm_Origen_Fondos.L_Cuentas.ListIndex = -1;
            Frm_Origen_Fondos.txtNumRef.Text = "";
            Frm_Origen_Fondos.txtNumRef.Visible = false;
            Frm_Origen_Fondos.LB_Referencia.Visible = false;
            Frm_Origen_Fondos.cmb_codtran.Enabled = false;
            Frm_Origen_Fondos.cmb_codtran.Visible = false;

            Frm_Origen_Fondos.Tx_Cuentas[0].Visible = false;

            for (j = 0; j <= 2; j++)
            {
                Frm_Origen_Fondos.Tx_Datos[j].Visible = false;
                Frm_Origen_Fondos.Lb_Datos[j].Visible = false;
            }

            Frm_Origen_Fondos.Lb_Oficina.Visible = false;
            Frm_Origen_Fondos.BNem.Visible = false;

            //Cuando se entra ninguna cuenta se debe encontrar cargada.-
            if (Frm_Origen_Fondos.L_Cta.Items.Count == 0 || Frm_Origen_Fondos.L_Cta.ListIndex == -1)
            {
                if (Frm_Origen_Fondos.L_Cta.ListIndex != -1)
                {
                    Frm_Origen_Fondos.L_Cta.ListIndex = -1;
                    L_Cta_Click(initObject, unit);
                }
                return;
            }

            Frm_Origen_Fondos.Indice_Cuenta = (short)(Frm_Origen_Fondos.L_Cta.get_ItemData_((short)Frm_Origen_Fondos.L_Cta.ListIndex));

            short _switchVar1 = initObject.MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta; //VOvd(i%).NumCta
            if (_switchVar1 == T_MODGCON0.IdCta_CtaCteMN)
            {
                //Cuenta Corriente M/N.
                Frm_Origen_Fondos.L_Partys.Enabled = true;
                Frm_Origen_Fondos.L_Cuentas.Enabled = true;
                for (k = 0; k <= (short)VB6Helpers.UBound(initObject.MODGCVD.Beneficiario); k++)
                {
                    if (VB6Helpers.Instr(initObject.MODXORI.VgxOri.Partys, VB6Helpers.Trim(VB6Helpers.Str(k))) != 0)
                    {
                        Frm_Origen_Fondos.L_Partys.Items.Add(new UI_ComboItem()
                        {
                            Data = k,
                            Value = initObject.MODGCVD.Beneficiario[k]
                        });
                    }

                }

                if (Frm_Origen_Fondos.L_Partys.Items.Count > 0)
                {
                    if (Frm_Origen_Fondos.L_Partys.ListIndex == -1)
                        Frm_Origen_Fondos.L_Partys.ListIndex = 0;
                    L_Partys_Click(initObject, unit);
                }
            }
            else if (_switchVar1 == T_MODGCON0.IdCta_CtaCteME || _switchVar1 == T_MODGCON0.IdCta_ChqCCME)
            {
                // Cheque  Cta Cte M/E                    'Cuenta Corriente M/E.
                Frm_Origen_Fondos.L_Partys.Enabled = true;
                Frm_Origen_Fondos.L_Cuentas.Enabled = true;
                for (k = 0; k <= (short)VB6Helpers.UBound(initObject.MODGCVD.Beneficiario); k++)
                {
                    if (VB6Helpers.Instr(initObject.MODXORI.VgxOri.Partys, VB6Helpers.Trim(VB6Helpers.Str(k))) != 0)
                    {
                        Frm_Origen_Fondos.L_Partys.Items.Add(new UI_ComboItem()
                        {
                            Data = k,
                            Value = initObject.MODGCVD.Beneficiario[k]
                        });
                    }

                }

                if (Frm_Origen_Fondos.L_Partys.Items.Count > 0)
                {
                    if (Frm_Origen_Fondos.L_Partys.ListIndex == -1)
                        Frm_Origen_Fondos.L_Partys.ListIndex = 0;
                    L_Partys_Click(initObject, unit);
                }
            }
            else if (_switchVar1 == T_MODGCON0.IdCta_SCSMN || _switchVar1 == T_MODGCON0.IdCta_SCSME)
            {
                //Saldos c/ Sucursales M/N.
                for (j = 0; j <= 2; j++)
                {
                    Frm_Origen_Fondos.Tx_Datos[j].Text = "";
                    Frm_Origen_Fondos.Tx_Datos[j].Visible = true;
                    Frm_Origen_Fondos.Lb_Datos[j].Visible = true;
                }

                Frm_Origen_Fondos.Lb_Oficina.Visible = true;
                Frm_Origen_Fondos.Lb_Oficina.Text = "";
                Frm_Origen_Fondos.Lb_Datos[0].Text = "Código Oficina";
                Frm_Origen_Fondos.Lb_Datos[1].Text = "Tipo Movimiento";
                Frm_Origen_Fondos.Lb_Datos[2].Text = "Número de Partida";
                Frm_Origen_Fondos.Tx_Datos[0].MaxLength = 3;
                Frm_Origen_Fondos.Tx_Datos[1].MaxLength = 1;
                Frm_Origen_Fondos.Tx_Datos[2].MaxLength = 8;
            }
            else if (_switchVar1 == T_MODGCON0.IdCta_CHMEBCH || _switchVar1 == T_MODGCON0.IdCta_CHMEOBC)
            {
                //Cheque M/E Emi. x B. Chile
                for (j = 0; j <= 1; j++)
                {
                    Frm_Origen_Fondos.Tx_Datos[j].Text = "";
                    Frm_Origen_Fondos.Tx_Datos[j].Visible = true;
                    Frm_Origen_Fondos.Lb_Datos[j].Visible = true;
                }

                Frm_Origen_Fondos.Lb_Datos[0].Text = "Swift";
                Frm_Origen_Fondos.Lb_Datos[1].Text = "Nº de Cheque";
                Frm_Origen_Fondos.Tx_Datos[0].MaxLength = 11;
                Frm_Origen_Fondos.Tx_Datos[1].MaxLength = 15;
            }
            else if (_switchVar1 == T_MODGCON0.IdCta_VVOB)
            {
                //Vale Vista Otro Banco
                for (j = 0; j <= 1; j++)
                {
                    Frm_Origen_Fondos.Tx_Datos[j].Text = "";
                    Frm_Origen_Fondos.Tx_Datos[j].Visible = true;
                    Frm_Origen_Fondos.Lb_Datos[j].Visible = true;
                }

                Frm_Origen_Fondos.Lb_Datos[0].Text = "Swift";
                Frm_Origen_Fondos.Lb_Datos[1].Text = "Nº de Vale Vista";
                Frm_Origen_Fondos.Tx_Datos[0].MaxLength = 11;
                Frm_Origen_Fondos.Tx_Datos[1].MaxLength = 15;

            }
            else if (_switchVar1 == T_MODGCON0.IdCta_CTACTEBC || _switchVar1 == T_MODGCON0.IdCta_CTAORD || _switchVar1 == T_MODGCON0.IdCta_DIVENPEN || _switchVar1 == T_MODGCON0.IdCta_CHVBNYM || _switchVar1 == 54)
            {
                //IdCta_OBCCIPLZ, 54  'Cta. Cte. Banco Central -- 'Corresponsal cuenta ordinaria
                for (j = 0; j <= 1; j++)
                {
                    Frm_Origen_Fondos.Tx_Datos[j].Text = "";
                    Frm_Origen_Fondos.Tx_Datos[j].Visible = true;
                    Frm_Origen_Fondos.Lb_Datos[j].Visible = true;
                }

                Frm_Origen_Fondos.Lb_Datos[0].Text = "Swift";
                Frm_Origen_Fondos.Lb_Datos[1].Text = "Nº de Referencia";
                Frm_Origen_Fondos.Tx_Datos[0].MaxLength = 11;
                Frm_Origen_Fondos.Tx_Datos[1].MaxLength = 15;

            }
            else if (_switchVar1 >= 40 && _switchVar1 <= 53)
            {
                //Cuentas de Obligaciones y Check Verification
                for (j = 0; j <= 1; j++)
                {
                    Frm_Origen_Fondos.Tx_Datos[j].Text = "";
                    Frm_Origen_Fondos.Tx_Datos[j].Visible = true;
                    Frm_Origen_Fondos.Lb_Datos[j].Visible = true;
                }

                Frm_Origen_Fondos.Lb_Datos[0].Text = "Swift";
                Frm_Origen_Fondos.Lb_Datos[1].Text = "Nº de Referencia";
                Frm_Origen_Fondos.Tx_Datos[0].MaxLength = 11;
                Frm_Origen_Fondos.Tx_Datos[1].MaxLength = 15;

            }
            else if (_switchVar1 == T_MODGCON0.IdCta_BOEREG || _switchVar1 == T_MODGCON0.IdCta_CHEREG || _switchVar1 == T_MODGCON0.IdCta_OBLREG || _switchVar1 == T_MODGCON0.IdCta_OBLARE || _switchVar1 == T_MODGCON0.IdCta_ACEREG)
            {
                if (initObject.Module1.Codop.Cent_Costo == "729" || initObject.Module1.Codop.Cent_Costo == "829" || initObject.Module1.Codop.Cent_Costo == "827" || initObject.Module1.Codop.Cent_Costo == "826")
                {
                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Esta Cuenta está habilitada solo para REGIONES"
                    });
                    return;
                }

                for (j = 0; j <= 1; j++)
                {
                    Frm_Origen_Fondos.Tx_Datos[j].Text = "";
                    Frm_Origen_Fondos.Tx_Datos[j].Visible = true;
                    Frm_Origen_Fondos.Lb_Datos[j].Visible = true;
                }

                Frm_Origen_Fondos.Lb_Datos[0].Text = "Swift";
                Frm_Origen_Fondos.Lb_Datos[1].Text = "Nº de Referencia";
                Frm_Origen_Fondos.Tx_Datos[0].MaxLength = 11;
                Frm_Origen_Fondos.Tx_Datos[1].MaxLength = 15;

            }
            else if (_switchVar1 == T_MODGCON0.IdCta_VAM || _switchVar1 == T_MODGCON0.IdCta_VAX || _switchVar1 == T_MODGCON0.IdCta_VAMC || _switchVar1 == T_MODGCON0.IdCta_VAMCC || _switchVar1 == T_MODGCON0.IdCta_VASC)
            {
                //Varios Acreedores Import. -- Varios Acreedores Export. -- 'Varios Acreedores Mcdo. Corr.
                for (j = 0; j <= 0; j++)
                {
                    Frm_Origen_Fondos.Tx_Datos[j].Text = "";
                    Frm_Origen_Fondos.Tx_Datos[j].Visible = true;
                    Frm_Origen_Fondos.Lb_Datos[j].Visible = true;
                }

                Frm_Origen_Fondos.Tx_Datos[0].Text = MODGPYF0.Componer(initObject.Module1.PartysOpe[0].LlaveArchivo, "~", "");
                Frm_Origen_Fondos.Lb_Datos[0].Text = "Participante";
                Frm_Origen_Fondos.Tx_Datos[0].MaxLength = 12;

                Frm_Origen_Fondos.Lb_Oficina.Visible = true;
                Frm_Origen_Fondos.Lb_Oficina.Text = initObject.Module1.PartysOpe[0].NombreUsado;
            }
            else if (_switchVar1 == T_MODGCON0.IdCta_VVBCH)
            {
                //Vale Vista Bco. Chile
                for (j = 0; j <= 1; j++)
                {
                    Frm_Origen_Fondos.Tx_Datos[j].Text = "";
                    Frm_Origen_Fondos.Tx_Datos[j].Visible = true;
                    Frm_Origen_Fondos.Lb_Datos[j].Visible = true;
                }

                Frm_Origen_Fondos.Lb_Datos[0].Text = "Código Oficina";
                Frm_Origen_Fondos.Lb_Datos[1].Text = "Nº Vale Vista";

                Frm_Origen_Fondos.Lb_Oficina.Visible = true;
                Frm_Origen_Fondos.Lb_Oficina.Text = "";
            }
            else if (_switchVar1 == T_MODGCON0.IdCta_OPC)
            {
                //Orden de Pago Convenio
                for (j = 0; j <= 1; j++)
                {
                    Frm_Origen_Fondos.Tx_Datos[j].Text = "";
                    Frm_Origen_Fondos.Tx_Datos[j].Visible = true;
                    Frm_Origen_Fondos.Lb_Datos[j].Visible = true;
                }

                Frm_Origen_Fondos.Lb_Datos[0].Text = "Swift";
                Frm_Origen_Fondos.Lb_Datos[1].Text = "Nº Reembolso";
                Frm_Origen_Fondos.Tx_Datos[0].MaxLength = 11;
                Frm_Origen_Fondos.Tx_Datos[1].MaxLength = 15;
            }
            else if (_switchVar1 == T_MODGCON0.IdCta_OPOP)
            {
                //Orden de Pago Otros Países
                for (j = 0; j <= 2; j++)
                {
                    Frm_Origen_Fondos.Tx_Datos[j].Text = "";
                    Frm_Origen_Fondos.Tx_Datos[j].Visible = true;
                    Frm_Origen_Fondos.Lb_Datos[j].Visible = true;
                }

                Frm_Origen_Fondos.Lb_Datos[0].Text = "Swift";
                Frm_Origen_Fondos.Lb_Datos[1].Text = "Cta. Corriente";
                Frm_Origen_Fondos.Lb_Datos[2].Text = "Nº Referencia";
                Frm_Origen_Fondos.Tx_Datos[0].MaxLength = 15;
                Frm_Origen_Fondos.Tx_Datos[1].MaxLength = 20;
                Frm_Origen_Fondos.Tx_Datos[2].MaxLength = 20;
            }
            else if (_switchVar1 == T_MODGCON0.IdCta_ONMN || _switchVar1 == T_MODGCON0.IdCta_ONME)
            {
                //Otro Nemónico M/N ---- Otro Nemónico M/E
                for (j = 0; j <= 0; j++)
                {
                    Frm_Origen_Fondos.Tx_Datos[j].Text = "";
                    Frm_Origen_Fondos.Tx_Datos[j].Visible = true;
                    Frm_Origen_Fondos.Lb_Datos[j].Visible = true;
                }

                Frm_Origen_Fondos.Lb_Datos[0].Text = "Nemónico";
                Frm_Origen_Fondos.Tx_Datos[0].MaxLength = 15;

                for (j = 1; j <= 2; j++)
                {
                    Frm_Origen_Fondos.Tx_Datos[j].Text = "";
                    Frm_Origen_Fondos.Tx_Datos[j].Visible = false;
                    Frm_Origen_Fondos.Lb_Datos[j].Visible = false;
                }

                Frm_Origen_Fondos.Lb_Oficina.Visible = true;
                Frm_Origen_Fondos.Lb_Oficina.Text = "";

                Frm_Origen_Fondos.BNem.Visible = true;
            }
            else if (_switchVar1 == T_MODGCON0.IdCta_CtaCteMANN || _switchVar1 == T_MODGCON0.IdCta_CtaCteAUTN)
            {
                //Cuenta Corriente M/N.
                if (initObject.MODXORI.gb_esCosmos == true)
                {
                    Frm_Origen_Fondos.frm_datos.Visible = true;
                }
                Frm_Origen_Fondos.L_Partys.Enabled = true;
                Frm_Origen_Fondos.L_Cuentas.Enabled = true;
                Frm_Origen_Fondos.cmb_codtran.Enabled = true;
                Frm_Origen_Fondos.Tx_Cuentas[0].Enabled = true;
                Frm_Origen_Fondos.cmb_codtran.Visible = true;
                Frm_Origen_Fondos.Tx_Cuentas[0].Visible = true;

                for (k = 0; k <= (short)VB6Helpers.UBound(initObject.MODGCVD.Beneficiario); k++)
                {
                    if (VB6Helpers.Instr(initObject.MODXORI.VgxOri.Partys, VB6Helpers.Trim(VB6Helpers.Str(k))) != 0)
                    {
                        Frm_Origen_Fondos.L_Partys.Items.Add(new UI_ComboItem()
                        {
                            Data = k,
                            Value = initObject.MODGCVD.Beneficiario[k]
                        });
                    }

                }

                //Obtiene Moneda Swift para consulta
                for (c = 0; c < initObject.MODGTAB0.VMnd.Length; c++)
                {
                    if (MODGPYF1.Minuscula(initObject.MODGTAB0.VMnd[c].Mnd_MndNom) == MODGPYF1.Minuscula(Frm_Origen_Fondos.l_mnd.Text))
                    {
                        initObject.MODXVIA.Moneda_TRX = VB6Helpers.Trim(initObject.MODGTAB0.VMnd[c].Mnd_MndSwf);
                    }

                }

                Cargar_CodTran(initObject, "CTA-CTE", initObject.MODXVIA.Moneda_TRX, "DR");

                if (initObject.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
                {
                    Frm_Origen_Fondos.frm_datos.Enabled = false;
                    Frm_Origen_Fondos.L_Partys.ListIndex = 0;
                    Frm_Origen_Fondos.Text1[0].Text = initObject.Mdl_Funciones_Varias.LC_ORD_INST1;
                    Frm_Origen_Fondos.Text1[1].Text = initObject.Mdl_Funciones_Varias.LC_PMNT_DET1;
                    Frm_Origen_Fondos.Text1[2].Text = initObject.Mdl_Funciones_Varias.LC_PMNT_DET2;
                    Frm_Origen_Fondos.Text1[3].Text = initObject.Mdl_Funciones_Varias.LC_PMNT_DET3;
                    Frm_Origen_Fondos.Text1[4].Text = initObject.Mdl_Funciones_Varias.LC_CONREFNUM;
                }
                else
                {
                    Frm_Origen_Fondos.Text1[0].Text = "";
                    Frm_Origen_Fondos.Text1[1].Text = "";
                    Frm_Origen_Fondos.Text1[2].Text = "";
                    Frm_Origen_Fondos.Text1[3].Text = "";
                    //---------------------------------------------
                    //Realsystems-Código Nuevo-Inicio
                    //Fecha Modificación 20100623
                    //Responsable: Pablo Millan
                    //Versión: 1.0
                    //Descripción : Se modifica generacion de CRN
                    //---------------------------------------------
                    Frm_Origen_Fondos.Text1[4].Text = VB6Helpers.Mid(initObject.Mdl_Funciones_Varias.LC_TRXID_MAN, 16, 10);
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
                }

                Frm_Origen_Fondos.cmb_codtran.ListIndex = -1;
                if (Frm_Origen_Fondos.L_Partys.Items.Count > 0)
                {
                    if (Frm_Origen_Fondos.L_Partys.ListIndex == -1)
                        Frm_Origen_Fondos.L_Partys.ListIndex = 0;
                    L_Partys_Click(initObject, unit);
                }

            }
            else if (_switchVar1 == T_MODGCON0.IdCta_CtaCteMANE || _switchVar1 == T_MODGCON0.IdCta_CtaCteAUTE)
            {
                //Cuenta Corriente M/E.
                if (initObject.MODXORI.gb_esCosmos == true)
                {
                    Frm_Origen_Fondos.frm_datos.Visible = true;
                }
                Frm_Origen_Fondos.L_Partys.Enabled = true;
                Frm_Origen_Fondos.L_Cuentas.Enabled = true;
                Frm_Origen_Fondos.cmb_codtran.Visible = true;
                Frm_Origen_Fondos.Tx_Cuentas[0].Visible = true;
                Frm_Origen_Fondos.cmb_codtran.Enabled = true;
                for (j = 0; j <= 3; j++)
                {
                    Frm_Origen_Fondos.Text1[j].Text = "";
                }

                for (k = 0; k <= (short)VB6Helpers.UBound(initObject.MODGCVD.Beneficiario); k++)
                {
                    if (VB6Helpers.Instr(initObject.MODXORI.VgxOri.Partys, VB6Helpers.Trim(VB6Helpers.Str(k))) != 0)
                    {
                        Frm_Origen_Fondos.L_Partys.Items.Add(new UI_ComboItem()
                        {
                            Data = k,
                            Value = initObject.MODGCVD.Beneficiario[k]
                        });
                    }

                }

                for (c = 0; c <= (short)VB6Helpers.UBound(initObject.MODGTAB0.VMnd); c++)
                {
                    if (MODGPYF1.Minuscula(initObject.MODGTAB0.VMnd[c].Mnd_MndNom.Trim()) == MODGPYF1.Minuscula(Frm_Origen_Fondos.l_mnd.Text))
                    {
                        initObject.MODXVIA.Moneda_TRX = VB6Helpers.Trim(initObject.MODGTAB0.VMnd[c].Mnd_MndSwf);
                        break;
                    }

                }

                Cargar_CodTran(initObject, "CTA-CTE", initObject.MODXVIA.Moneda_TRX, "DR");

                if (initObject.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
                {
                    Frm_Origen_Fondos.frm_datos.Enabled = false;
                    if (Frm_Origen_Fondos.L_Partys.ListIndex == -1)
                        Frm_Origen_Fondos.L_Partys.ListIndex = 0;
                    L_Partys_Click(initObject, unit);
                    Frm_Origen_Fondos.Text1[0].Text = initObject.Mdl_Funciones_Varias.LC_ULT_BEN1;
                    Frm_Origen_Fondos.Text1[1].Text = initObject.Mdl_Funciones_Varias.LC_ULT_BEN2;
                    Frm_Origen_Fondos.Text1[2].Text = initObject.Mdl_Funciones_Varias.LC_RECVR_CORRES2;
                    Frm_Origen_Fondos.Text1[3].Text = initObject.Mdl_Funciones_Varias.LC_PMNT_DET1;
                    Frm_Origen_Fondos.Text1[4].Text = initObject.Mdl_Funciones_Varias.LC_CONREFNUM;
                }
                else
                {
                    Frm_Origen_Fondos.cmb_codtran.Enabled = true;
                    //---------------------------------------------
                    //Realsystems-Código Nuevo-Inicio
                    //Fecha Modificación 20100623
                    //Responsable: Pablo Millan
                    //Versión: 1.0
                    //Descripción : Se modifica generacion de CRN
                    //---------------------------------------------
                    Frm_Origen_Fondos.Text1[4].Text = VB6Helpers.Mid(initObject.Mdl_Funciones_Varias.LC_TRXID_MAN, 16, 10);
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
                    Frm_Origen_Fondos.cmb_codtran.ListIndex = -1;
                    cmb_codtran_Click(initObject);
                    if (Frm_Origen_Fondos.L_Partys.Items.Count > 0)
                    {
                        if (Frm_Origen_Fondos.L_Partys.ListIndex == -1)
                            Frm_Origen_Fondos.L_Partys.ListIndex = 0;
                        L_Partys_Click(initObject, unit);
                    }
                }
            }
            else if (_switchVar1 == T_MODGCON0.IdCta_GAPMN || _switchVar1 == T_MODGCON0.IdCta_GAPME)
            {
                if (initObject.MODXORI.gb_esCosmos == true)
                {
                    Frm_Origen_Fondos.frm_infoctagap.Visible = true;
                }
                Frm_Origen_Fondos.L_Partys.Enabled = true;
                Frm_Origen_Fondos.L_Cuentas.Enabled = true;
                Frm_Origen_Fondos.cmb_codtran.Enabled = true;
                Frm_Origen_Fondos.cmb_codtran.Visible = true;
                Frm_Origen_Fondos.Tx_Cuentas[0].Visible = true;
                Frm_Origen_Fondos.L_Cuentas.Visible = false;
                Frm_Origen_Fondos.Tx_Cuentas[5].Visible = false;
                Frm_Origen_Fondos.txt_CRN.Text = Frm_Origen_Fondos.Text1[4].Text;

                for (k = 0; k <= (short)VB6Helpers.UBound(initObject.MODGCVD.Beneficiario); k++)
                {
                    if (VB6Helpers.Instr(initObject.MODXORI.VgxOri.Partys, VB6Helpers.Trim(VB6Helpers.Str(k))) != 0)
                    {
                        Frm_Origen_Fondos.L_Partys.Items.Add(new UI_ComboItem()
                        {
                            Data = k,
                            Value = initObject.MODGCVD.Beneficiario[k]
                        });
                    }

                }

                Cargar_CodTran(initObject, "GAP", "MM", "DR");
                if (Frm_Origen_Fondos.L_Partys.Items.Count > 0)
                {
                    Frm_Origen_Fondos.L_Partys.ListIndex = 0;
                    L_Partys_Click(initObject, unit);
                }

                Frm_Origen_Fondos.cmb_codtran.ListIndex = -1;
                cmb_codtran_Click(initObject);
            }

        }

        public static void L_Cuentas_Click(InitializationObject initObject)
        {
            if (initObject.Frm_Origen_Fondos.L_Cuentas.ListIndex != -1)
            {
                initObject.Frm_Origen_Fondos.Indice_CtaCte = (short)(initObject.Frm_Origen_Fondos.L_Cuentas.get_ItemData_(initObject.Frm_Origen_Fondos.L_Cuentas.ListIndex));
            }
            else
            {
                return;
            }

        }

        public static void l_mnd_Click(InitializationObject initObject, UnitOfWorkCext01 unit, bool limpiaVia = true)
        {
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            T_MODXORI MODXORI = initObject.MODXORI;
            short i = 0;
            short k = 0;
            short n = 0;
            short m = 0;
            if (Frm_Origen_Fondos.l_mnd.ListIndex == -1)
            {
                return;
            }

            //Valida Decimales de las Monedas.-
            i = (short)(Frm_Origen_Fondos.l_mnd.get_ItemData_(Frm_Origen_Fondos.l_mnd.ListIndex));
            m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, i);
            if (MODGTAB0.VMnd[m].Mnd_MndSin != 0)
            {
                Frm_Origen_Fondos.MtoOri.Tag = "_____________";
            }
            else
            {
                Frm_Origen_Fondos.MtoOri.Tag = "_____________.__";
            }

            //Carga los Origenes de acuerdo a la moneda.
            if (Frm_Origen_Fondos.l_mnd.get_ItemData_(Frm_Origen_Fondos.l_mnd.ListIndex) == (T_MODGTAB0.MndNac))
            {
                BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Carga_l_cta(initObject, Frm_Origen_Fondos.L_Cta, 1, true, false, 0, unit);
            }
            else
            {
                BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Carga_l_cta(initObject, Frm_Origen_Fondos.L_Cta, 2, true, false, 0, unit);
            }
            L_Cta_Click(initObject, unit);
            //Se posiciona en el monto con la moneda actual.
            for (k = 0; k < (short)(Frm_Origen_Fondos.l_mto.Items.Count); k++)
            {
                n = short.Parse(Frm_Origen_Fondos.l_mto.get_ItemData(k));
                if (Frm_Origen_Fondos.l_mnd.get_ItemData_((short)Frm_Origen_Fondos.l_mnd.ListIndex) == (MODXORI.VxMtoOri[n].CodMon))
                {
                    if (Frm_Origen_Fondos.l_mto.ListIndex != k)
                    {
                        Frm_Origen_Fondos.l_mto.ListIndex = k;
                        l_mto_Click(initObject, unit, limpiaVia);
                    }
                    break;
                }

            }

        }

        public static void l_mto_Click(InitializationObject initObject, UnitOfWorkCext01 unit, bool limpiaVia = true)
        {
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;
            //T_MODXORI MODXORI = initObject.MODXORI;
            int anterior = 0;
            double Saldo = 0;
            if (Frm_Origen_Fondos.l_mto.ListIndex == -1)
            {
                return;
            }
            Frm_Origen_Fondos.Indice_Monto = short.Parse(Frm_Origen_Fondos.l_mto.get_ItemData((short)Frm_Origen_Fondos.l_mto.ListIndex));
            Saldo = initObject.MODXORI.VxMtoOri[Frm_Origen_Fondos.Indice_Monto].MtoTot - BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_SumaVxOri(initObject, initObject.MODXORI.VxMtoOri[Frm_Origen_Fondos.Indice_Monto].CodMon);
            if (Saldo >= 0)
            {
                Frm_Origen_Fondos.MtoOri.Text = Format.FormatCurrency(Saldo, "0.00");
            }

            if (limpiaVia)
            {
                anterior = Frm_Origen_Fondos.l_ori.ListIndex;
                Frm_Origen_Fondos.l_ori.ListIndex = -1;
                if (anterior != Frm_Origen_Fondos.l_ori.ListIndex)
                    l_ori_Click(initObject, unit);
            }

            anterior = Frm_Origen_Fondos.l_mnd.ListIndex;
            Frm_Origen_Fondos.l_mnd.ListIndex = Frm_Origen_Fondos.l_mnd.Items.FindIndex(x => x.Data == initObject.MODXORI.VxMtoOri[Frm_Origen_Fondos.Indice_Monto].CodMon);
            if (anterior != Frm_Origen_Fondos.l_mnd.ListIndex)
            {
                l_mnd_Click(initObject, unit);
            }
        }

        public static void l_ori_Click(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_MODGCON0 MODGCON0 = initObject.MODGCON0;

            short i = 0;
            short j = 0;
            short k = 0;
            short n = 0;
            double Saldo = 0;
            string razon = "";
            string a = "";
            short Y = 0;
            short x = 0;


            i = (short)Frm_Origen_Fondos.l_ori.ListIndex;
            j = short.Parse(Frm_Origen_Fondos.l_ori.get_ItemData(i));

            //Despliega los datos de las Comisiones.
            if (j == -1)
            {
                //Busca el saldo de alguna moneda de la lista de montos.
                for (k = 0; k <= (short)(Frm_Origen_Fondos.l_mto.Items.Count - 1); k++)
                {
                    n = short.Parse(Frm_Origen_Fondos.l_mto.get_ItemData(k));
                    Saldo = MODXORI.VxMtoOri[n].MtoTot - BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_SumaVxOri(initObject, MODXORI.VxMtoOri[n].CodMon);
                    if (Saldo > 0)
                    {
                        Frm_Origen_Fondos.l_mnd.ListIndex = Frm_Origen_Fondos.l_mnd.Items.FindIndex(y => y.Data == MODXORI.VxMtoOri[n].CodMon);
                        l_mnd_Click(initObject, unit);
                        Frm_Origen_Fondos.MtoOri.Text = Format.FormatCurrency(Saldo, "0.00");
                        break;
                    }
                    else
                    {
                        Frm_Origen_Fondos.MtoOri.Text = Format.FormatCurrency(0, "0.00");
                    }

                }

                Frm_Origen_Fondos.NO.Enabled = false;
                Frm_Origen_Fondos.L_Cta.ListIndex = 0;
                L_Cta_Click(initObject, unit);
            }
            else
            {
                //Se posiciona en la lista de monedas.
                Frm_Origen_Fondos.l_mnd.ListIndex = Frm_Origen_Fondos.l_mnd.Items.FindIndex(y => y.Data == MODXORI.VxOri[j].CodMon);
                l_mnd_Click(initObject, unit, false);
                //Se posiciona en el origen actual.
                for (k = 0; k <= (short)(Frm_Origen_Fondos.L_Cta.Items.Count - 1); k++)
                {
                    if (MODXORI.VOvd[(Frm_Origen_Fondos.L_Cta.get_ItemData_(k))].NumCta == MODXORI.VxOri[j].NumCta)
                    {
                        Frm_Origen_Fondos.L_Cta.ListIndex = k;
                        L_Cta_Click(initObject, unit);
                        break;
                    }

                }

                Frm_Origen_Fondos.MtoOri.Text = Format.FormatCurrency((MODXORI.VxOri[j].MtoTot), "0.00");
                short _switchVar1 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta; //VOvd(i%).NumCta
                if (_switchVar1 == T_MODGCON0.IdCta_CtaCteMN || _switchVar1 == T_MODGCON0.IdCta_CtaCteME || _switchVar1 == T_MODGCON0.IdCta_ChqCCME)
                {
                    // Cheque  Cta Cte M/E  'Cuenta Corriente M/N. -- Cuenta Corriente M/E.
                    Ok_Partys_Click(initObject, unit);
                    Frm_Origen_Fondos.L_Partys.ListIndex = Frm_Origen_Fondos.L_Partys.Items.FindIndex(y => y.Data == MODXORI.Indice_Partys);
                    Frm_Origen_Fondos.L_Cuentas.ListIndex = VB6Helpers.CInt(Frm_Origen_Fondos.L_Cuentas.Items.FindIndex(z => z.Value.Equals(MODXORI.VxOri[j].CtaCte_t.ToString())));
                }
                else if (_switchVar1 == T_MODGCON0.IdCta_SCSMN || _switchVar1 == T_MODGCON0.IdCta_SCSME)
                {
                    //Saldos c/ Sucursales M/N.
                    Frm_Origen_Fondos.Tx_Datos[0].Text = VB6Helpers.Trim(VB6Helpers.Str(MODXORI.VxOri[j].codofi));
                    Frm_Origen_Fondos.Lb_Oficina.Text = BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_Buscar_Suc(initObject, VB6Helpers.Trim(VB6Helpers.Str(MODXORI.VxOri[j].codofi)));
                    Frm_Origen_Fondos.Tx_Datos[1].Text = VB6Helpers.Trim(VB6Helpers.Str(MODXORI.VxOri[j].TipMov));
                    Frm_Origen_Fondos.Tx_Datos[2].Text = VB6Helpers.Right("00000000" + VB6Helpers.Trim(VB6Helpers.Str(MODXORI.VxOri[j].NumPar)), 8);
                }
                else if (_switchVar1 == T_MODGCON0.IdCta_CHMEBCH || _switchVar1 == T_MODGCON0.IdCta_CHMEOBC)
                {
                    //Cheque M/E Emi. x B. Chile
                    Frm_Origen_Fondos.Tx_Datos[0].Text = VB6Helpers.Trim(MODXORI.VxOri[j].CodSwf);
                    Frm_Origen_Fondos.Tx_Datos[1].Text = VB6Helpers.Trim(MODXORI.VxOri[j].numdoc);
                }
                else if (_switchVar1 == T_MODGCON0.IdCta_VVOB)
                {
                    //Vale Vista Otro Banco
                    Frm_Origen_Fondos.Tx_Datos[0].Text = VB6Helpers.Trim(MODXORI.VxOri[j].CodSwf);
                    Frm_Origen_Fondos.Tx_Datos[1].Text = VB6Helpers.Trim(MODXORI.VxOri[j].numdoc);
                }
                else if (_switchVar1 == T_MODGCON0.IdCta_CTACTEBC)
                {
                    //Cta. Cte. Banco Central
                    Frm_Origen_Fondos.Tx_Datos[0].Text = VB6Helpers.Trim(MODXORI.VxOri[j].CodSwf);
                    Frm_Origen_Fondos.Tx_Datos[1].Text = VB6Helpers.Trim(MODXORI.VxOri[j].numdoc);
                }
                else if (_switchVar1 == T_MODGCON0.IdCta_CTAORD || _switchVar1 == T_MODGCON0.IdCta_CHVBNYM || _switchVar1 == T_MODGCON0.IdCta_BOEREG || _switchVar1 == T_MODGCON0.IdCta_CHEREG || _switchVar1 == T_MODGCON0.IdCta_OBLREG || _switchVar1 == T_MODGCON0.IdCta_OBLARE || _switchVar1 == T_MODGCON0.IdCta_ACEREG || _switchVar1 == 54)
                {
                    //IdCta_OBCCIPLZ, 54
                    Frm_Origen_Fondos.Tx_Datos[0].Text = VB6Helpers.Trim(MODXORI.VxOri[j].CodSwf);
                    Frm_Origen_Fondos.Tx_Datos[1].Text = VB6Helpers.Trim(MODXORI.VxOri[j].numdoc);
                }
                else if (_switchVar1 >= 40 && _switchVar1 <= 53)
                {
                    //Cuentas de Obligaciones y Check Verification
                    Frm_Origen_Fondos.Tx_Datos[0].Text = VB6Helpers.Trim(MODXORI.VxOri[j].CodSwf);
                    Frm_Origen_Fondos.Tx_Datos[1].Text = VB6Helpers.Trim(MODXORI.VxOri[j].numdoc);
                }
                else if (_switchVar1 == T_MODGCON0.IdCta_DIVENPEN)
                {
                    //Divisas Pendientes
                    Frm_Origen_Fondos.Tx_Datos[0].Text = VB6Helpers.Trim(MODXORI.VxOri[j].CodSwf);
                    Frm_Origen_Fondos.Tx_Datos[1].Text = VB6Helpers.Trim(MODXORI.VxOri[j].numdoc);
                }
                else if (_switchVar1 == T_MODGCON0.IdCta_VAM || _switchVar1 == T_MODGCON0.IdCta_VAX || _switchVar1 == T_MODGCON0.IdCta_VAMC || _switchVar1 == T_MODGCON0.IdCta_VAMCC || _switchVar1 == T_MODGCON0.IdCta_VASC)
                {
                    //Varios Acreedores Import. -- Varios Acreedores Export. -- 'Varios Acreedores Mcdo. Corr.
                    Frm_Origen_Fondos.Tx_Datos[0].Text = VB6Helpers.Trim(MODXORI.VxOri[j].IdPrty);
                    razon = BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.SyGet_Partys(unit, VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text));
                    Frm_Origen_Fondos.Lb_Oficina.Text = MODGPYF1.Minuscula(VB6Helpers.Trim(razon));
                    if (string.IsNullOrWhiteSpace(razon))
                    {
                        Frm_Origen_Fondos.Lb_Oficina.Text = "";
                    }

                }
                else if (_switchVar1 == T_MODGCON0.IdCta_VVBCH)
                {
                    //Vale Vista Bco. Chile
                    Frm_Origen_Fondos.Tx_Datos[0].Text = VB6Helpers.Trim(VB6Helpers.Str(MODXORI.VxOri[j].codofi));
                    Frm_Origen_Fondos.Tx_Datos[1].Text = VB6Helpers.Trim(MODXORI.VxOri[j].numdoc);
                    a = BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_Buscar_Suc(initObject, VB6Helpers.Trim(VB6Helpers.Str(MODXORI.VxOri[j].codofi)));
                    if (!string.IsNullOrEmpty(a))
                    {
                        Frm_Origen_Fondos.Lb_Oficina.Text = a;
                    }
                    else
                    {
                        Frm_Origen_Fondos.Lb_Oficina.Text = "";
                    }

                }
                else if (_switchVar1 == T_MODGCON0.IdCta_OPC)
                {
                    //Orden de Pago Convenio
                    Frm_Origen_Fondos.Tx_Datos[0].Text = VB6Helpers.Trim(MODXORI.VxOri[j].CodSwf);
                    Frm_Origen_Fondos.Tx_Datos[1].Text = VB6Helpers.Trim(MODXORI.VxOri[j].numdoc);
                }
                else if (_switchVar1 == T_MODGCON0.IdCta_OPOP)
                {
                    //Orden de Pago Otros Países
                    Frm_Origen_Fondos.Tx_Datos[0].Text = VB6Helpers.Trim(MODXORI.VxOri[j].CodSwf);
                    Frm_Origen_Fondos.Tx_Datos[1].Text = VB6Helpers.Trim(MODXORI.VxOri[j].ctacte);
                    Frm_Origen_Fondos.Tx_Datos[2].Text = VB6Helpers.Trim(MODXORI.VxOri[j].numdoc);
                }
                else if (_switchVar1 == T_MODGCON0.IdCta_ONMN || _switchVar1 == T_MODGCON0.IdCta_ONME)
                {
                    //Otro Nemónico M/N ---- Otro Nemónico M/E
                    Frm_Origen_Fondos.Tx_Datos[0].Text = VB6Helpers.Trim(MODXORI.VxOri[j].NemCta);

                    // Al editar si existe numero de partida debe habilitar los campos.
                    if ((MODXORI.VxOri[j].NumPar > 0))
                    {
                        Frm_Origen_Fondos.txtNumRef.Text = VB6Helpers.Trim(VB6Helpers.CStr(MODXORI.VxOri[j].NumPar));
                        Frm_Origen_Fondos.txtNumRef.Visible = true;
                        Frm_Origen_Fondos.LB_Referencia.Visible = true;
                    }

                }
                else if (_switchVar1 == T_MODGCON0.IdCta_GAPMN || _switchVar1 == T_MODGCON0.IdCta_GAPME)
                {
                    //Cuentas GAP
                    MODGCON0.VMcd.SwiBco = MODXORI.VxOri[j].SwiBco;
                    MODGCON0.VMcd.NroRef = MODXORI.VxOri[j].NroRef;
                    Frm_Origen_Fondos.txt_cuenta.Text = VB6Helpers.Trim(MODXORI.VxOri[j].ctacte);
                    for (Y = 0; Y < MODXORI.Vx_SCodTran.Length; Y++)
                    {
                        if (MODXORI.VxOri[j].IdCtran == MODXORI.Vx_SCodTran[Y].ID && MODXORI.Vx_SCodTran[Y].Via == "ori")
                        {
                            break;
                        }

                    }

                    for (x = 0; x <= (short)(Frm_Origen_Fondos.cmb_codtran.Items.Count - 1); x++)
                    {
                        if (Frm_Origen_Fondos.cmb_codtran.get_ItemData_(x) == (MODXORI.Vx_SCodTran[Y].nro_trx))
                        {
                            Frm_Origen_Fondos.cmb_codtran.ListIndex = x;
                            break;
                        }

                    }

                }
                else if (_switchVar1 == T_MODGCON0.IdCta_CtaCteMANN || _switchVar1 == T_MODGCON0.IdCta_CtaCteMANE)
                {
                    Frm_Origen_Fondos.Text1[0].Text = VB6Helpers.Trim(MODXORI.VxOri[j].Text1);
                    Frm_Origen_Fondos.Text1[1].Text = VB6Helpers.Trim(MODXORI.VxOri[j].Text2);
                    Frm_Origen_Fondos.Text1[2].Text = VB6Helpers.Trim(MODXORI.VxOri[j].Text3);
                    Frm_Origen_Fondos.Text1[3].Text = VB6Helpers.Trim(MODXORI.VxOri[j].Text4);

                    for (Y = 0; Y <= (short)VB6Helpers.UBound(MODXORI.Vx_SCodTran); Y++)
                    {
                        if (MODXORI.VxOri[j].IdCtran == MODXORI.Vx_SCodTran[Y].ID && MODXORI.Vx_SCodTran[Y].Via == "ori")
                        {
                            break;
                        }

                    }

                    for (x = 0; x <= (short)(Frm_Origen_Fondos.cmb_codtran.Items.Count - 1); x++)
                    {
                        if (Frm_Origen_Fondos.cmb_codtran.get_ItemData_(x).Equals(MODXORI.Vx_SCodTran[Y].nro_trx))
                        {
                            Frm_Origen_Fondos.cmb_codtran.ListIndex = x;
                            break;
                        }

                    }

                }

                Frm_Origen_Fondos.NO.Enabled = true;
            }

            //Deshabilita algunos objetos.-
            if (j == -1 && Saldo == 0)
            {
                Frm_Origen_Fondos.L_Partys.ListIndex = -1;
                L_Partys_Click(initObject, unit);
                Frm_Origen_Fondos.L_Cuentas.ListIndex = -1;
                L_Cuentas_Click(initObject);
                Frm_Origen_Fondos.L_Cta.ListIndex = 0;
                L_Cta_Click(initObject, unit);
            }
        }

        public static void L_Partys_Click(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;
            T_MODXORI MODXORI = initObject.MODXORI;
            if (Frm_Origen_Fondos.L_Partys.ListIndex == -1)
            {
                return;
            }
            MODXORI.Indice_Partys = (short)(Frm_Origen_Fondos.L_Partys.get_ItemData_(Frm_Origen_Fondos.L_Partys.ListIndex));
            Ok_Partys_Click(initObject, unit);

            if (Frm_Origen_Fondos.frm_datos.Visible == true)
            {
                //---------------------------------------------
                //Realsystems-Código Nuevo-Inicio
                //Fecha Modificación 20100615
                //Responsable: Pablo Millan.
                //Versión: 1.0
                //Descripción : Se omite precarga de cliente
                //---------------------------------------------
                Frm_Origen_Fondos.Text1[0].Text = "";
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

                Frm_Origen_Fondos.Text1[0].Text = "";
            }

        }

        public static void NO_Click(InitializationObject initObject, UnitOfWorkCext01 unit, bool pasoPorPopUp, bool aceptaVerificacion)
        {
            T_ModChVrf ModChVrf = initObject.ModChVrf;
            T_MODXORI MODXORI = initObject.MODXORI;
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;

            short a = 0;
            short i = 0;
            short j = 0;
            short p = 0;
            short sw = 0;
            short f = 0;
            a = (short)VB6Helpers.UBound(ModChVrf.VPlnChV);
            i = (short)Frm_Origen_Fondos.l_ori.ListIndex;
            j = short.Parse(Frm_Origen_Fondos.l_ori.get_ItemData(i));


            if (j != -1)
            {
                if (MODXORI.VxOri[j].NumCta == T_MODGCON0.IdCta_CHVRF)
                {
                    if (a > 0)
                    {
                        Frm_Origen_Fondos.Confirms.Add(new UI_Message()
                        {
                            Type = TipoMensaje.YesNo,
                            Text = "La cuenta Check Verification ya generó planillas para el origen de los fondos. Si continua se eliminarán. ¿Desea continuar? ",
                            Title = " Atención"
                        });
                        if (pasoPorPopUp && !aceptaVerificacion)
                        {
                            //CANCELAR
                            return;
                        }
                        else if (pasoPorPopUp && aceptaVerificacion)
                        {
                            ModChVrf.VPlnChV = new T_PlnChV[1];
                            ModChVrf.AceptoPantallaChVrf = (short)(false ? -1 : 0);
                        }
                        else { }
                    }
                }

                MODXORI.VxOri[j].Status = T_MODXORI.ExOri_Eli;
                //se cambia estado para matriz Vx_SCodTran
                for (f = 0; f < MODXORI.Vx_SCodTran.Length; f++)
                {
                    if (MODXORI.VxOri[j].IdCtran == MODXORI.Vx_SCodTran[f].ID)
                    {
                        MODXORI.Vx_SCodTran[f].Estado = 3;  //registro eliminado
                        break;
                    }
                }

                if (MODXORI.VxOri[j].NumCta == T_MODGCON0.IdCta_CHVRF)
                {
                    BCH.Comex.Core.BL.XCFT.Modulos.ModChVrf.TraeDatosOri_ChVrf(initObject, j);
                }
                if (i == Frm_Origen_Fondos.l_ori.Items.Count - 1)
                {
                    p = 0;
                }
                else
                {
                    p = i;
                }

                Frm_Origen_Fondos.l_ori.Items.RemoveAt(i);
                Frm_Origen_Fondos.l_ori.ListIndex = p;
                Pr_Llena_L_ori(initObject, unit);
                Frm_Origen_Fondos.Boton[0].Enabled = false;

                sw = (short)(false ? -1 : 0);
                for (i = 0; i < MODXORI.VxOri.Length; i++)
                {
                    if (MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CHVRF && MODXORI.VxOri[i].Status != 3)
                    {
                        sw = (short)(true ? -1 : 0);
                        break;
                    }
                }

                if (sw == -1)
                {
                    Frm_Origen_Fondos.Bt_PlnTrn.Enabled = true;
                }
                else
                {
                    Frm_Origen_Fondos.Bt_PlnTrn.Enabled = false;
                }
                // Al eliminar debe limpiar y ocultar objetos de numero de partida
                LimpiaNumPartida(initObject);
            }
        }

        public static void Ok_Partys_Click(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            T_Module1 Module1 = initObject.Module1;
            T_MODXORI MODXORI = initObject.MODXORI;
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;
            //if(Frm_Origen_Fondos.l_ori.ListIndex > -1)
            //    MODXORI.Indice_Partys = (short)Frm_Origen_Fondos.l_ori.ListIndex;
            short a;

            if (MODXORI.Indice_Partys == -1)
                return;

            a = BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.SyGetn_Ctas(initObject, unit, Module1.PartysOpe[MODXORI.Indice_Partys].LlaveArchivo.Replace('~', '|'));
            if (a != 0)
            {
                Frm_Origen_Fondos.L_Cuentas.Enabled = true;
                BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Pr_Cargar_Lista_Cuentas(initObject, unit, Frm_Origen_Fondos.L_Cuentas, Frm_Origen_Fondos.l_mnd, Frm_Origen_Fondos.Indice_Cuenta);
                if (Frm_Origen_Fondos.L_Cuentas.Items.Count > 0)
                {
                    L_Cuentas_Click(initObject);
                }
            }
            else
            {
                Frm_Origen_Fondos.L_Cuentas.Enabled = false;
            }
        }

        public static void Tx_Datos_KeyPress(InitializationObject initObject, UnitOfWorkCext01 unit, short Index)
        {
            using (var tracer = new Tracer())
            {
                T_MODXORI MODXORI = initObject.MODXORI;
                T_MODGUSR MODGUSR = initObject.MODGUSR;
                T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
                T_MODGNCTA MODGNCTA = initObject.MODGNCTA;
                UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;

                tracer.AddToContext("Index", Index);

                string a = "";
                string razon = "";
                short b = 0;
                short i = 0;
                short j = 0;
                Frm_Origen_Fondos.Tx_Datos[Index].Text = (Frm_Origen_Fondos.Tx_Datos[Index].Text ?? string.Empty).ToUpper();
                switch (Index)
                {
                    case 0:  //Tx_Datos(0)
                        if (!string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[0].Text))
                        {
                            if ((MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_SCSMN) || (MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_SCSME))
                            {
                                if (VB6Helpers.IsNumeric(Frm_Origen_Fondos.Tx_Datos[0].Text))
                                {
                                    a = BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_Buscar_Suc(initObject, VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text));
                                    if (!string.IsNullOrEmpty(a))
                                    {
                                        if (VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text) != MODGUSR.UsrEsp.CentroCosto)
                                        {
                                            Frm_Origen_Fondos.Lb_Oficina.Text = a;
                                            //Validación Correcta
                                            short _switchVar1 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                                            if (_switchVar1 == T_MODGCON0.IdCta_SCSMN)
                                            {
                                                Frm_Origen_Fondos.SCSMN_OK = "01";
                                            }
                                            else if (_switchVar1 == T_MODGCON0.IdCta_SCSME)
                                            {
                                                Frm_Origen_Fondos.SCSME_OK = "01";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Frm_Origen_Fondos.Lb_Oficina.Text = "";
                                        //Validación Incorrecta
                                        short _switchVar2 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                                        if (_switchVar2 == T_MODGCON0.IdCta_SCSMN)
                                        {
                                            Frm_Origen_Fondos.SCSMN_OK = "00";
                                        }
                                        else if (_switchVar2 == T_MODGCON0.IdCta_SCSME)
                                        {
                                            Frm_Origen_Fondos.SCSME_OK = "00";
                                        }

                                    }

                                }

                            }

                            //Varios Acreedores Import. -- Varios Acreedores Export. -- 'Varios Acreedores Mcdo. Corr.
                            short _switchVar3 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                            if (_switchVar3 == T_MODGCON0.IdCta_VAM || _switchVar3 == T_MODGCON0.IdCta_VAX || _switchVar3 == T_MODGCON0.IdCta_VAMC || _switchVar3 == T_MODGCON0.IdCta_VAMCC || _switchVar3 == T_MODGCON0.IdCta_VASC)
                            {
                                razon = BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.SyGet_Partys(unit, VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text));
                                Frm_Origen_Fondos.Lb_Oficina.Text = MODGPYF1.Minuscula(VB6Helpers.Trim(razon));
                                if (string.IsNullOrWhiteSpace(razon))
                                {
                                    Frm_Origen_Fondos.Lb_Oficina.Text = "";
                                    //Validación Incorrecta
                                    short _switchVar4 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                                    if (_switchVar4 == T_MODGCON0.IdCta_VAM)
                                    {
                                        Frm_Origen_Fondos.VAM_OK = "00";
                                    }
                                    else if (_switchVar4 == T_MODGCON0.IdCta_VAX)
                                    {
                                        Frm_Origen_Fondos.VAX_OK = "00";
                                    }
                                    else if (_switchVar4 == T_MODGCON0.IdCta_VAMC)
                                    {
                                        Frm_Origen_Fondos.VAMC_OK = "00";
                                    }
                                    else if (_switchVar4 == T_MODGCON0.IdCta_VAMCC || _switchVar4 == T_MODGCON0.IdCta_VASC)
                                    {
                                        Frm_Origen_Fondos.VAMC_OK = "00";
                                    }

                                }
                                else
                                {
                                    //Validación Correcta
                                    short _switchVar5 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                                    if (_switchVar5 == T_MODGCON0.IdCta_VAM)
                                    {
                                        Frm_Origen_Fondos.VAM_OK = "01";
                                    }
                                    else if (_switchVar5 == T_MODGCON0.IdCta_VAX)
                                    {
                                        Frm_Origen_Fondos.VAX_OK = "01";
                                    }
                                    else if (_switchVar5 == T_MODGCON0.IdCta_VAMC)
                                    {
                                        Frm_Origen_Fondos.VAMC_OK = "01";
                                    }
                                    else if (_switchVar5 == T_MODGCON0.IdCta_VAMCC || _switchVar5 == T_MODGCON0.IdCta_VASC)
                                    {
                                        Frm_Origen_Fondos.VAMC_OK = "01";
                                    }

                                }

                            }

                            //Vale Vista Bco. Chile
                            if ((MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_VVBCH))
                            {
                                if (VB6Helpers.IsNumeric(VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text)))
                                {
                                    a = BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_Buscar_Suc(initObject, VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text));
                                    if (!string.IsNullOrEmpty(a))
                                    {
                                        Frm_Origen_Fondos.Lb_Oficina.Text = a;
                                        //Validación Correcta
                                        Frm_Origen_Fondos.VVBCH_OK = "01";
                                    }
                                    else
                                    {
                                        Frm_Origen_Fondos.Lb_Oficina.Text = "";
                                        //Validación Incorrecta
                                        Frm_Origen_Fondos.VVBCH_OK = "00";
                                    }

                                }

                            }

                            //Cuenta Corriente Corresponsal.-
                            if ((MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_OPOP))
                            {
                                b = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VNom(initObject, unit, VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text), MODXORI.VxMtoOri[Frm_Origen_Fondos.Indice_Monto].CodMon);
                                if (b == -1)
                                {
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = "El Banco que Usted acaba de ingresar no es Corresponsal o no maneja la Moneda que Usted está enviando."
                                    });
                                    return;
                                }

                                if (MODGTAB0.VNom[b].Nom_Ala == 1)
                                {
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = "El Banco que Usted acaba de ingresar es Aladi. Debe ingresar un Banco Corresponsal."
                                    });
                                    return;
                                }

                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[1].Text))
                                {
                                    Frm_Origen_Fondos.Tx_Datos[1].Text = MODGTAB0.VNom[b].Nom_cta;
                                }

                            }

                            //Otro Nemónico M/N --- Otro Nemónico M/E.
                            if ((MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_ONMN) || (MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_ONME))
                            {
                                //Otro Nemónico M/N ---- Otro Nemónico M/E
                                b = (short)BCH.Comex.Core.BL.XCFT.Modulos.MODGNCTA.Get_Cta(VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text), initObject, unit);
                                if (b == -1 || b == 0)
                                {
                                    Frm_Origen_Fondos.Lb_Oficina.Text = "";
                                    //Validación Incorrecta
                                    short _switchVar6 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                                    if (_switchVar6 == T_MODGCON0.IdCta_ONMN)
                                    {
                                        Frm_Origen_Fondos.ONMN_OK = "00";
                                        Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                        {
                                            Type = TipoMensaje.Error,
                                            Text = "Usted seleccionó una Cuenta Nacional, por lo tanto el Nemónico que tendría que ingresar debería ser Nacional."
                                        });
                                    }
                                    else if (_switchVar6 == T_MODGCON0.IdCta_ONME)
                                    {
                                        Frm_Origen_Fondos.ONME_OK = "00";
                                        Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                        {
                                            Type = TipoMensaje.Error,
                                            Text = "Usted seleccionó una Cuenta Extranjera, por lo tanto el Nemónico que tendría que ingresar debería ser Extranjero."
                                        });
                                    }

                                }
                                else
                                {
                                    short _switchVar7 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                                    if (_switchVar7 == T_MODGCON0.IdCta_ONMN)
                                    {
                                        if (MODGNCTA.VCta[b].Cta_Mon == 1)
                                        {
                                            Frm_Origen_Fondos.Lb_Oficina.Text = "";
                                            //Validación Incorrecta
                                            short _switchVar8 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                                            if (_switchVar8 == T_MODGCON0.IdCta_ONMN)
                                            {
                                                Frm_Origen_Fondos.ONMN_OK = "00";
                                                Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                                {
                                                    Type = TipoMensaje.Error,
                                                    Text = "Usted seleccionó una Cuenta Nacional, por lo tanto el Nemónico que tendría que ingresar debería ser Nacional."
                                                });
                                            }
                                            else if (_switchVar8 == T_MODGCON0.IdCta_ONME)
                                            {
                                                Frm_Origen_Fondos.ONME_OK = "00";
                                                Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                                {
                                                    Type = TipoMensaje.Error,
                                                    Text = "Usted seleccionó una Cuenta Extranjera, por lo tanto el Nemónico que tendría que ingresar debería ser Extranjero."
                                                });
                                            }

                                        }
                                        else
                                        {
                                            Frm_Origen_Fondos.Lb_Oficina.Text = MODGPYF1.Minuscula(VB6Helpers.Trim(MODGNCTA.VCta[b].Cta_Nom.Value));
                                            //Validación Correcta
                                            short _switchVar9 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                                            if (_switchVar9 == T_MODGCON0.IdCta_ONMN)
                                            {
                                                Frm_Origen_Fondos.ONMN_OK = "01";
                                            }
                                            else if (_switchVar9 == T_MODGCON0.IdCta_ONME)
                                            {
                                                Frm_Origen_Fondos.ONME_OK = "01";
                                            }

                                        }

                                    }
                                    else if (_switchVar7 == T_MODGCON0.IdCta_ONME)
                                    {
                                        if (MODGNCTA.VCta[b].Cta_Mon == 2)
                                        {
                                            Frm_Origen_Fondos.Lb_Oficina.Text = "";
                                            //Validación Incorrecta
                                            short _switchVar10 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                                            if (_switchVar10 == T_MODGCON0.IdCta_ONMN)
                                            {
                                                Frm_Origen_Fondos.ONMN_OK = "00";
                                                Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                                {
                                                    Type = TipoMensaje.Error,
                                                    Text = "Usted seleccionó una Cuenta Nacional, por lo tanto el Nemónico que tendría que ingresar debería ser Nacional."
                                                });
                                            }
                                            else if (_switchVar10 == T_MODGCON0.IdCta_ONME)
                                            {
                                                Frm_Origen_Fondos.ONME_OK = "00";
                                                Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                                {
                                                    Type = TipoMensaje.Error,
                                                    Text = "Usted seleccionó una Cuenta Extranjera, por lo tanto el Nemónico que tendría que ingresar debería ser Extranjero."
                                                });
                                            }

                                        }
                                        else
                                        {
                                            Frm_Origen_Fondos.Lb_Oficina.Text = MODGPYF1.Minuscula(VB6Helpers.Trim(MODGNCTA.VCta[b].Cta_Nom.Value));
                                            //Validación Correcta
                                            short _switchVar11 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                                            if (_switchVar11 == T_MODGCON0.IdCta_ONMN)
                                            {
                                                Frm_Origen_Fondos.ONMN_OK = "01";
                                            }
                                            else if (_switchVar11 == T_MODGCON0.IdCta_ONME)
                                            {
                                                Frm_Origen_Fondos.ONME_OK = "01";
                                            }

                                        }

                                    }

                                    //Verifica que Nemónico NO sea una Cuenta Especial.-
                                    for (i = 0; i <= (short)(Frm_Origen_Fondos.L_Cta.Items.Count - 1); i++)
                                    {
                                        j = (short)(Frm_Origen_Fondos.L_Cta.get_ItemData_(i));
                                        //MsgBox VOvd(j%).NemCta
                                        if (VB6Helpers.Trim(MODXORI.VOvd[j].NemCta) == VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text))
                                        {
                                            Frm_Origen_Fondos.L_Cta.ListIndex = i;
                                            L_Cta_Click(initObject, unit);
                                            return;
                                        }

                                    }

                                    // Si la cuenta es vigenteable se habilita el ingreso de numero de referencia
                                    // de lo contrario limpia los valores y oculta objetos.
                                    if ((MODGNCTA.VCta[b].Cta_Vig == 1))
                                    {
                                        Frm_Origen_Fondos.LB_Referencia.Visible = true;
                                        Frm_Origen_Fondos.txtNumRef.Visible = true;
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
                            Frm_Origen_Fondos.SCSMN_OK = "00";
                            Frm_Origen_Fondos.SCSME_OK = "00";
                            Frm_Origen_Fondos.VVBCH_OK = "00";
                            Frm_Origen_Fondos.VAM_OK = "00";
                            Frm_Origen_Fondos.VAX_OK = "00";
                            Frm_Origen_Fondos.VAMC_OK = "00";
                            Frm_Origen_Fondos.ONMN_OK = "00";
                            Frm_Origen_Fondos.ONME_OK = "00";
                        }

                        //******************************************
                        break;
                    case 1:  //Tx_Datos(1)
                        if (!string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[1].Text))
                        {
                            if ((MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_SCSMN) || (MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_SCSME))
                            {
                                //Texto$ = "01;02;03;04"
                                string _switchVar12 = VB6Helpers.Format(Frm_Origen_Fondos.Tx_Datos[1].Text, "00");
                                if (_switchVar12 == "01")
                                {
                                    Pr_Generar_Automatica_Ini(initObject, unit);
                                }
                                else if (_switchVar12 == "02")
                                {
                                }
                                else
                                {
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = "El Tipo de Movimiento debe estar entra 1 y 2."
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[1].Enabled = true;
                                    return;
                                }

                            }

                        }

                        break;
                }
            }
        }

        public static void Tx_Datos_LostFocus(InitializationObject initObject, short Index)
        {
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;
            T_MODXORI MODXORI = initObject.MODXORI;

            if (Index == 2)
            {
                if (VB6Helpers.Val(Frm_Origen_Fondos.Tx_Datos[1].Text) == 2)
                {
                    short _switchVar1 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                    if (_switchVar1 == T_MODGCON0.IdCta_SCSMN || _switchVar1 == T_MODGCON0.IdCta_SCSME)
                    {
                        //Saldos c/ Sucursales M/N.
                        Frm_Origen_Fondos.Tx_Datos[Index].Text = VB6Helpers.Right("00000000" + Frm_Origen_Fondos.Tx_Datos[Index].Text, 8);
                    }

                }

            }
            else if (Index == 0)
            {
                // Si el objeto actual esta vacio, se debe limpiar y ocultar numero de partida
                // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'VB6Helpers.Empty'. Consider using the GetDefaultMember6 helper method.
                if ((VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text) == VB6Helpers.CStr(VB6Helpers.Empty)))
                {
                    LimpiaNumPartida(initObject);
                }

            }

        }

        public static short ok_Click_1(InitializationObject initObject, UnitOfWorkCext01 unit, bool vieneDeMensaje, bool resMensaje)
        {

            T_ModChVrf ModChVrf = initObject.ModChVrf;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_MODCVDIM MODCVDIM = initObject.MODCVDIM;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;
            T_MODSWENN MODSWENN = initObject.MODSWENN;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODGPYF0 MODGPYF0 = initObject.MODGPYF0;
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;

            short a = 0;
            short LC = 0;
            short lm = 0;
            short n = 0;
            short ln = 0;
            short i = 0;
            short s = 0;
            a = (short)VB6Helpers.UBound(ModChVrf.VPlnChV);

            // Si el objeto esta visible se debe validar que tenga informacion
            if ((Frm_Origen_Fondos.txtNumRef.Visible == true))
            {
                if (String.IsNullOrEmpty(Frm_Origen_Fondos.txtNumRef.Text))
                {
                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Es necesario que se ingrese un Número de Partida para poder realizar la operación.",
                        ControlName = "txtNumRef"
                    });
                    Frm_Origen_Fondos.txtNumRef.Enabled = true;
                    return 0;
                }
            }

            //Si el monto a ingresar es cero => Se omite.
            if (Format.StringToDouble((Frm_Origen_Fondos.MtoOri.Text)) == 0)
            {
                return 0;
            }

            if (Frm_Origen_Fondos.L_Cta.ListIndex == -1)
            {
                return 0;
            }

            //Me aseguro que los campos campos estan en mayusculas
            foreach (var elm in Frm_Origen_Fondos.Tx_Datos)
            {
                elm.Text = elm.Text == null ? elm.Text : elm.Text.ToUpper();
            }

            //Ingresa los datos en el arreglo.
            LC = (short)(Frm_Origen_Fondos.L_Cta.get_ItemData_(Frm_Origen_Fondos.L_Cta.ListIndex));
            lm = short.Parse(Frm_Origen_Fondos.l_mto.get_ItemData(Frm_Origen_Fondos.l_mto.ListIndex));

            T_xOri origenSeleccionado = null;
            if (Frm_Origen_Fondos.l_ori.ListIndex >= 0)
            {
                origenSeleccionado = Frm_Origen_Fondos.l_ori.Items[Frm_Origen_Fondos.l_ori.ListIndex].Tag as T_xOri;
            }

            if (MODXORI.VOvd[LC].NumCta == T_MODGCON0.IdCta_CHVRF)
            {
                if (a > 0)
                {
                    if (!vieneDeMensaje)
                    {
                        Frm_Origen_Fondos.Confirms.Add(new UI_Message()
                        {
                            Type = TipoMensaje.YesNo,
                            Text = "La cuenta Check Verification ya generó planillas para los origenes. Si continua tendrá que volver a definirlas ¿Desea continuar? "
                        });
                    }
                    else
                    {
                        if (resMensaje)
                        {
                            ModChVrf.VPlnChV = new T_PlnChV[0];
                            vieneDeMensaje = false;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    return 0;
                }
            }

            //Cambios
            if (Frm_Origen_Fondos.l_ori.ListIndex == -1 && Frm_Origen_Fondos.l_ori.Items.Count == MODXORI.VxOri.Where(x => x.Status != 3).ToArray().Length)
            {
                n = (short)(VB6Helpers.UBound(MODXORI.VxOri) + 1);
                VB6Helpers.RedimPreserve(ref MODXORI.VxOri, 0, n);
            }
            else
            {
                //Si no hay seleccionado para editar, y Frm_Origen_Fondos.l_ori.Items.Count != MODXORI.VxOri.Length
                if (Frm_Origen_Fondos.l_ori.ListIndex >= 0)
                {
                    n = short.Parse(Frm_Origen_Fondos.l_ori.get_ItemData(Frm_Origen_Fondos.l_ori.ListIndex));
                }
                else
                {
                    n = (short)(MODXORI.VxOri.Length - 1);
                }

                //si se reeemplaza dato en vxvia
                if (MODXORI.VxOri[n].IdCtran != 0)
                {
                    for (s = 0; s <= (short)VB6Helpers.UBound(MODXORI.Vx_SCodTran); s++)
                    {
                        if (MODXORI.VxOri[n].IdCtran == MODXORI.Vx_SCodTran[s].ID)
                        {
                            MODXORI.Vx_SCodTran[s].Estado = T_MODXORI.ExOri_Eli;
                            break;
                        }
                    }
                }
                else
                {
                    MODXORI.VxOri[n].Status = 0;
                }
            }
            MODXORI.VxOri[n].NumCta = MODXORI.VOvd[LC].NumCta;
            MODXORI.VxOri[n].NomOri = Frm_Origen_Fondos.L_Cta.get_List(Frm_Origen_Fondos.L_Cta.ListIndex);

            if (T_MODGCON0.IdCta_CtaCteMANN == MODXORI.VOvd[LC].NumCta || T_MODGCON0.IdCta_CtaCteMANE == MODXORI.VOvd[LC].NumCta)
            {
                for (i = 0; i <= (short)VB6Helpers.UBound(MODCVDIM.CtaCCDin); i++)
                {
                    if (VB6Helpers.Trim(VB6Helpers.Str(Frm_Origen_Fondos.cmb_codtran.ListIndex)) == MODCVDIM.CtaCCDin[i].NumCta)
                    {
                        MODXORI.VxOri[n].NemCta = VB6Helpers.Trim(MODCVDIM.CtaCCDin[i].NemCta);
                        break;
                    }
                }
            }
            else
            {
                MODXORI.VxOri[n].NemCta = MODXORI.VOvd[LC].NemCta;
            }

            MODXORI.VxOri[n].CodMon = MODXORI.VxMtoOri[lm].CodMon;
            ln = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, MODXORI.VxOri[n].CodMon);
            MODXORI.VxOri[n].NemMon = MODGTAB0.VMnd[ln].Mnd_MndNmc;
            decimal rs = 0;
            decimal.TryParse(Frm_Origen_Fondos.MtoOri.Text.Replace('.', ','), out rs);
            MODXORI.VxOri[n].MtoTot = Convert.ToDouble(decimal.Round(rs, 2));
            MODXORI.VxOri[n].Status = T_MODGCVD.EstadoIng;

            // Se consulta si existe algun dato para asignar en numero de partida
            if (!String.IsNullOrEmpty(VB6Helpers.Trim(Frm_Origen_Fondos.txtNumRef.Text)))
            {
                MODXORI.VxOri[n].NumPar = VB6Helpers.CInt(VB6Helpers.Trim(Frm_Origen_Fondos.txtNumRef.Text));
            }
            else
            {
                MODXORI.VxOri[n].NumPar = 0;
            }
            MODSWENN.RutAis = String.Empty;

            //short _switchVar2 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta; //VOvd(i%).NumCta
            initObject.Frm_Origen_Fondos.indiceVxOri = n;

            return MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
        }

        public static bool ok_Click_2(InitializationObject initObject, UnitOfWorkCext01 unit, short option, bool vieneDeMensaje, bool resMensaje)
        {

            T_MODXORI MODXORI = initObject.MODXORI;
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODGPYF0 MODGPYF0 = initObject.MODGPYF0;
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;


            double monto = 0;
            var n = initObject.Frm_Origen_Fondos.indiceVxOri;
            var _switchVar2 = option;

            if (_switchVar2 == T_MODGCON0.IdCta_CtaCteMN || _switchVar2 == T_MODGCON0.IdCta_CtaCteME || _switchVar2 == T_MODGCON0.IdCta_ChqCCME)
            {
                // Cheque  Cta Cte M/E   'Cuenta Corriente M/N.

                if (Fn_Cargar_Cta_Cte(initObject, unit, n) == 0)
                {
                    return false;
                }

                //Valida Monto con Saldo de CCte para clientes Bco. Chile.
                if ((Mdl_Funciones_Varias.CARGA_AUTOMATICA == 0 && MODXORI.gb_esCosmos == false))
                {
                    monto = ModSaldo.Obtiene_Monto(initObject, unit, T_MODGCON0.IdCta_CtaCteMN, T_MODGCON0.IdCta_CtaCteME, MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta, MODXORI.VxOri[n].ctacte);
                    if (monto < VB6Helpers.Val(BCH.Comex.Core.BL.XCFT.Modulos.MODGPYF0.unformat(MODGPYF0, Frm_Origen_Fondos.MtoOri.Text)))
                    {
                        if (!vieneDeMensaje)
                        {
                            Frm_Origen_Fondos.Confirms.Add(new UI_Message()
                            {
                                Type = TipoMensaje.YesNo,
                                Text = "Saldo disponible de la Cuenta Corriente ( " + Format.FormatCurrency((monto), "##,###,###,##0.00") + " ) es menor que el Monto a Cubrir. ¿Desea Aceptar el Origen seleccionado ? "
                            });
                            return false;
                        }
                        else
                        {
                            if (resMensaje)
                            {
                                vieneDeMensaje = false;
                            }
                            else { return false; }
                        }
                    }
                }

            }
            else if (_switchVar2 == T_MODGCON0.IdCta_SCSMN || _switchVar2 == T_MODGCON0.IdCta_SCSME)
            {
                //Saldos c/ Sucursales M/N.
                short _switchVar4 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                if (_switchVar4 == T_MODGCON0.IdCta_SCSMN)
                {
                    if (Fn_Cargar_Saldos(initObject, unit, n, T_MODGCON0.IdCta_SCSMN) == 0)
                    {
                        return false;
                    }
                }
                else if (_switchVar4 == T_MODGCON0.IdCta_SCSME)
                {
                    if (Fn_Cargar_Saldos(initObject, unit, n, T_MODGCON0.IdCta_SCSME) == 0)
                    {
                        return false;
                    }
                }

            }
            else if (_switchVar2 == T_MODGCON0.IdCta_CHMEBCH || _switchVar2 == T_MODGCON0.IdCta_CHMEOBC)
            {
                //Cheque M/E Emi. x B. Chile
                if (Fn_Cargar_Cheques(initObject, unit, n) == 0)
                {
                    return false;
                }
            }
            else if (_switchVar2 == T_MODGCON0.IdCta_VVOB)
            {
                //Vale Vista Otro Banco
                if (Fn_Cargar_Vales_Vistas(initObject, unit, n) == 0)
                {
                    return false;
                }
            }
            else if (_switchVar2 == T_MODGCON0.IdCta_CTACTEBC)
            {
                //Cta. Cte. Banco Central
                if (Fn_Cargar_Bco_Central(initObject, unit, n) == 0)
                {
                    return false;
                }
            }
            else if (_switchVar2 == T_MODGCON0.IdCta_CTAORD || _switchVar2 == T_MODGCON0.IdCta_CHVBNYM || _switchVar2 == T_MODGCON0.IdCta_BOEREG || _switchVar2 == T_MODGCON0.IdCta_CHEREG || _switchVar2 == T_MODGCON0.IdCta_OBLREG || _switchVar2 == T_MODGCON0.IdCta_OBLARE || _switchVar2 == T_MODGCON0.IdCta_ACEREG || _switchVar2 == 54)
            {
                if (Fn_Cargar_Corresponsal(initObject, unit, n) == 0)
                {
                    return false;
                }
            }
            else if (_switchVar2 >= 40 && _switchVar2 <= 53)
            {
                //Cuentas de Obligaciones y Check Verification
                if (Fn_Cargar_Corresponsal(initObject, unit, n) == 0)
                {
                    return false;
                }
            }
            else if (_switchVar2 == T_MODGCON0.IdCta_DIVENPEN)
            {
                //Divisas Pendientes.
                if (Fn_Cargar_Divisas_Pendientes(initObject, unit, n) == 0)
                {
                    return false;
                }
            }
            else if (_switchVar2 == T_MODGCON0.IdCta_VAM || _switchVar2 == T_MODGCON0.IdCta_VAX || _switchVar2 == T_MODGCON0.IdCta_VAMC || _switchVar2 == T_MODGCON0.IdCta_VAMCC || _switchVar2 == T_MODGCON0.IdCta_VASC)
            {
                //Varios Acreedores Import. -- Varios Acreedores Export. -- 'Varios Acreedores Mcdo. Corr.
                short _switchVar5 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                if (_switchVar5 == T_MODGCON0.IdCta_VAM)
                {
                    if (Fn_Cargar_Acreedores(initObject, unit, n, T_MODGCON0.IdCta_VAM) == 0)
                    {
                        return false;
                    }
                }
                else if (_switchVar5 == T_MODGCON0.IdCta_VAX)
                {
                    if (Fn_Cargar_Acreedores(initObject, unit, n, T_MODGCON0.IdCta_VAX) == 0)
                    {
                        return false;
                    }
                }
                else if (_switchVar5 == T_MODGCON0.IdCta_VAMC)
                {
                    if (Fn_Cargar_Acreedores(initObject, unit, n, T_MODGCON0.IdCta_VAMC) == 0)
                    {
                        return false;
                    }
                }
                else if (_switchVar5 == T_MODGCON0.IdCta_VAMCC || _switchVar5 == T_MODGCON0.IdCta_VASC)
                {
                    if (Fn_Cargar_Acreedores(initObject, unit, n, T_MODGCON0.IdCta_VAMC) == 0)
                    {
                        return false;
                    }
                }
            }
            else if (_switchVar2 == T_MODGCON0.IdCta_VVBCH)
            {
                //Vale Vista Bco. Chile
                if (Fn_Cargar_Vales_Vista_Bco(initObject, unit, n) == 0)
                {
                    return false;
                }
            }
            else if (_switchVar2 == T_MODGCON0.IdCta_OPC)
            {
                //Orden de Pago Convenio
                if (Fn_Cargar_Orden_Convenio(initObject, unit, n) == 0)
                {
                    return false;
                }
            }
            else if (_switchVar2 == T_MODGCON0.IdCta_OPOP)
            {
                //Orden de Pago Otros Países
                if (Fn_Cargar_Orden_Paises(initObject, unit, n) == 0)
                {
                    return false;
                }
            }
            else if (_switchVar2 == T_MODGCON0.IdCta_ONMN || _switchVar2 == T_MODGCON0.IdCta_ONME)
            {
                //Otro Nemónico M/N ---- Otro Nemónico M/E
                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[0].Text))
                {
                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Es necesario que se ingrese un Código de Nemónico para poder realizar la operación.",
                        ControlName = "Tx_Datos_0"
                    });
                    return false;
                }
                short _switchVar6 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                if (_switchVar6 == T_MODGCON0.IdCta_ONMN)
                {
                    if (Fn_Cargar_Otro_Nemonico(initObject, unit, n, T_MODGCON0.IdCta_ONMN) == 0)
                    {
                        return false;
                    }
                }
                else if (_switchVar6 == T_MODGCON0.IdCta_ONME)
                {
                    if (Fn_Cargar_Otro_Nemonico(initObject, unit, n, T_MODGCON0.IdCta_ONME) == 0)
                    {
                        return false;
                    }
                }

            }
            else if (_switchVar2 == T_MODGCON0.IdCta_CtaCteAUTN || _switchVar2 == T_MODGCON0.IdCta_CtaCteAUTE || _switchVar2 == T_MODGCON0.IdCta_CtaCteMANN || _switchVar2 == T_MODGCON0.IdCta_CtaCteMANE)
            {
                if (Frm_Origen_Fondos.cmb_codtran.ListIndex == -1)
                {
                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Debe seleccionar un tipo de transacción",
                        ControlName = "cmb_codtran"
                    });
                    return false;
                }

                if (Frm_Origen_Fondos.L_Partys.ListIndex == -1)
                {
                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Debe seleccionar una cuenta",
                        ControlName = "L_Partys"
                    });
                    return false;
                }

                MODXORI.VxOri[n].Text1 = Frm_Origen_Fondos.Text1[0].Text ?? String.Empty;
                MODXORI.VxOri[n].Text2 = Frm_Origen_Fondos.Text1[1].Text ?? String.Empty;
                MODXORI.VxOri[n].Text3 = Frm_Origen_Fondos.Text1[2].Text ?? String.Empty;
                MODXORI.VxOri[n].Text4 = Frm_Origen_Fondos.Text1[3].Text ?? String.Empty;
                MODXORI.VxOri[n].Text5 = Frm_Origen_Fondos.Text1[4].Text ?? String.Empty;

                VB6Helpers.RedimPreserve(ref MODXORI.Vx_SCodTran, 0, VB6Helpers.UBound(MODXORI.Vx_SCodTran) + 1);
                MODXORI.Vx_SCodTran[VB6Helpers.UBound(MODXORI.Vx_SCodTran)].nro_trx = (short)(Frm_Origen_Fondos.cmb_codtran.get_ItemData_(Frm_Origen_Fondos.cmb_codtran.ListIndex));
                MODXORI.Vx_SCodTran[VB6Helpers.UBound(MODXORI.Vx_SCodTran)].Moneda = MODXVIA.Moneda_TRX;
                MODXORI.Vx_SCodTran[VB6Helpers.UBound(MODXORI.Vx_SCodTran)].Via = MODXORI.ori_des;
                MODXORI.Vx_SCodTran[VB6Helpers.UBound(MODXORI.Vx_SCodTran)].Estado = 1;

                MODXVIA.IdCtran = (short)(MODXVIA.IdCtran + 1);
                MODXORI.VxOri[n].IdCtran = MODXVIA.IdCtran;

                MODXORI.Vx_SCodTran[VB6Helpers.UBound(MODXORI.Vx_SCodTran)].ID = MODXORI.VxOri[n].IdCtran;
                //'''''''''''''''''''''''
                if (Fn_Cargar_Cta_Cte(initObject, unit, n) == 0)
                {
                    return false;
                }

            }
            else if (_switchVar2 == T_MODGCON0.IdCta_GAPMN || _switchVar2 == T_MODGCON0.IdCta_GAPME)
            {
                if (Frm_Origen_Fondos.cmb_codtran.ListIndex == -1)
                {
                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Debe seleccionar un tipo de transacción",
                        ControlName = "cmb_codtran"
                    });
                    return false;
                }

                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.txt_cuenta.Text))
                {
                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Debe ingresar cuenta GAP",
                        ControlName = "txt_cuenta"
                    });
                    return false;
                }
                else
                {
                    MODXORI.VxOri[n].ctacte = VB6Helpers.Trim(Frm_Origen_Fondos.txt_cuenta.Text);
                    MODXORI.VxOri[n].CtaCte_t = VB6Helpers.Trim(Frm_Origen_Fondos.txt_cuenta.Text);
                }

                if (VB6Helpers.Trim(Frm_Origen_Fondos.txt_CRN.Text) != VB6Helpers.Mid(MODGCVD.VgCvd.OpeSin, 6, 10))
                {
                    MODXORI.VxOri[n].Text5 = VB6Helpers.Trim(Frm_Origen_Fondos.txt_CRN.Text);
                }
                else
                {
                    MODXORI.VxOri[n].Text5 = VB6Helpers.Mid(MODGCVD.VgCvd.OpeSin, 6, 10);
                }

                VB6Helpers.RedimPreserve(ref MODXORI.Vx_SCodTran, 0, VB6Helpers.UBound(MODXORI.Vx_SCodTran) + 1);
                MODXORI.Vx_SCodTran[VB6Helpers.UBound(MODXORI.Vx_SCodTran)].nro_trx = (short)(Frm_Origen_Fondos.cmb_codtran.get_ItemData_(Frm_Origen_Fondos.cmb_codtran.ListIndex));
                MODXORI.Vx_SCodTran[VB6Helpers.UBound(MODXORI.Vx_SCodTran)].Moneda = MODXVIA.Moneda_TRX;
                MODXORI.Vx_SCodTran[VB6Helpers.UBound(MODXORI.Vx_SCodTran)].Via = MODXORI.ori_des;
                MODXORI.Vx_SCodTran[VB6Helpers.UBound(MODXORI.Vx_SCodTran)].Estado = 1;

                MODXVIA.IdCtran = (short)(MODXVIA.IdCtran + 1);
                MODXORI.VxOri[n].IdCtran = MODXVIA.IdCtran;

                MODXORI.Vx_SCodTran[VB6Helpers.UBound(MODXORI.Vx_SCodTran)].ID = MODXORI.VxOri[n].IdCtran;
                Frm_Origen_Fondos.txt_cuenta.Text = "";
                Frm_Origen_Fondos.txt_CRN.Text = "";
            }
            return true;
        }

        public static void ok_Click_3(InitializationObject initObject, UnitOfWorkCext01 unit)
        {

            T_ModChVrf ModChVrf = initObject.ModChVrf;
            T_MODXORI MODXORI = initObject.MODXORI;
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;
            var n = initObject.Frm_Origen_Fondos.indiceVxOri;
            short i = 0;
            short sw = 0;

            //Asigna Posición del Party.-
            if (Frm_Origen_Fondos.L_Partys.ListIndex != -1)
            {
                MODXORI.VxOri[n].PosPrty = (short)(Frm_Origen_Fondos.L_Partys.get_ItemData_(Frm_Origen_Fondos.L_Partys.ListIndex));
            }

            //Se ingresan los nuevos datos en la lista.
            Pr_Llena_L_ori(initObject, unit);

            //Si todo está justificado => se posiciona en botón Aceptar.
            if (Fn_OrigenOK(initObject) != 0)
            {
                Frm_Origen_Fondos.Boton[0].Enabled = true;
                using (var trace = new Tracer("Datos de Origenes: "))
                {
                    double montoTotal = 0;
                    trace.TraceInformation(String.Format("Nro Operación: {0}", initObject.MODGCVD.VgCvd.OpeSin));
                    foreach (var Origenes in MODXORI.VxOri)
                    {                        
                        trace.TraceInformation(String.Format("Moneda: {0}", Origenes.NemMon));
                        trace.TraceInformation(String.Format("Memonico: {0}", Origenes.NemCta));
                        trace.TraceInformation(String.Format("Monto: {0}", Origenes.MtoTot));
                        trace.TraceInformation(String.Format("Tipo de Cuenta: {0}", Origenes.NomOri));
                        montoTotal += Origenes.MtoTot;
                    }
                    trace.TraceInformation(String.Format("Monto total: {0}", montoTotal));
                }
            }
            else
            {
                Frm_Origen_Fondos.Boton[0].Enabled = false;
            }

            if (MODXORI.VxOri[n].NumCta == T_MODGCON0.IdCta_CHVRF)
            {
                BCH.Comex.Core.BL.XCFT.Modulos.ModChVrf.TraeDatosOri_ChVrf(initObject, n);
                for (i = 0; i <= (short)VB6Helpers.UBound(ModChVrf.VgChV); i++)
                {
                    if (ModChVrf.VgChV[i].Saldo > 0)
                    {
                        ModChVrf.AceptoPantallaChVrf = (short)(false ? -1 : 0);
                        break;
                    }

                }
            }

            sw = (short)(false ? -1 : 0);
            for (i = 0; i <= (short)VB6Helpers.UBound(MODXORI.VxOri); i++)
            {
                if (MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CHVRF && MODXORI.VxOri[i].Status != T_MODXVIA.ExVia_Eli)
                {
                    sw = (short)(true ? -1 : 0);
                    break;
                }
            }

            if (sw == -1)
            {
                Frm_Origen_Fondos.Bt_PlnTrn.Enabled = true;
            }
            else
            {
                Frm_Origen_Fondos.Bt_PlnTrn.Enabled = false;
            }

            // Limpia los valores y oculta objetos.
            LimpiaNumPartida(initObject);
            //Frm_Origen_Fondos.L_Partys.Clear();
            //Frm_Origen_Fondos.L_Cuentas.Clear();
        }

        #endregion

        #region PRIVADOS

        //****************************************************************************
        //   1.  Envía orden para validar los campos necesarios para la operación
        //       con respecto a "Cuenta Corriente M/N." y si la validación es correcta
        //       carga los datos en los campos correspondientes dentro del arreglo
        //       VxOri.
        //****************************************************************************
        private static short Fn_Cargar_Cta_Cte(InitializationObject initObject, UnitOfWorkCext01 unit, short Indice)
        {
            T_MODXORI MODXORI = initObject.MODXORI;
            T_Module1 Module1 = initObject.Module1;
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;

            short _retValue = 0;

            _retValue = 0;

            if (Fn_Validar_Campos(initObject, unit, 3, 4, -1) == 0)
            {
                Frm_Origen_Fondos.Boton[0].Enabled = false;
                return _retValue;
            }

            MODXORI.VxOri[Indice].IdPrty = VB6Helpers.Trim(Module1.PartysOpe[MODXORI.Indice_Partys].LlaveArchivo);
            MODXORI.VxOri[Indice].PosPrty = MODXORI.Indice_Partys;
            MODXORI.VxOri[Indice].ctacte = VB6Helpers.Trim(MODXORI.Vx_OriCC[Frm_Origen_Fondos.Indice_CtaCte].ctacte);
            MODXORI.VxOri[Indice].CtaCte_t = VB6Helpers.Trim(MODXORI.Vx_OriCC[Frm_Origen_Fondos.Indice_CtaCte].CtaCte_t);
            MODXORI.VxOri[Indice].MonExt = MODXORI.Vx_OriCC[Frm_Origen_Fondos.Indice_CtaCte].MonExt;
            //Frm_Origen_Fondos.L_Cuentas.ListIndex = -1;
            L_Cuentas_Click(initObject);
            return 1;
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
                trace.AddToContext("Fn_Validar_Campos", "Valida los campos de existen dentro de la pantalla Origen.");
                T_MODXORI MODXORI = initObject.MODXORI;
                T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
                UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;
                T_Mdl_Funciones Mdl_Funciones = initObject.Mdl_Funciones;
                T_Module1 Module1 = initObject.Module1;
                T_MODGNCTA MODGNCTA = initObject.MODGNCTA;
                T_MODGUSR MODGUSR = initObject.MODGUSR;

                short _retValue = 0, i = 0;
                string a = "";
                short b = 0;
                string Swift = "", Secuencia = "";
                short c = 0;
                string razon = "", Texto = "", Msg = "";
                short resp = 0;
                string el_dv = "";
                dynamic num_par = null, nums = null;
                string dv = "";

                _retValue = 0;
                Frm_Origen_Fondos.ErrorEnOk = true;
                for (i = VB6Helpers.CShort(Campo_Inicial); i <= VB6Helpers.CShort(Campo_Final); i++)
                {
                    switch (i)
                    {
                        //**************************************************
                        case 0:  //Tx_Datos(0)
                            short _switchVar1 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                            if (_switchVar1 == T_MODGCON0.IdCta_SCSMN || _switchVar1 == T_MODGCON0.IdCta_SCSME)
                            {
                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[0].Text))
                                {
                                    Msg = "Es necesario que ingrese una Oficina.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }

                                if (VB6Helpers.IsNumeric(VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text)))
                                {
                                    Frm_Origen_Fondos.Tx_Datos[0].MaxLength = 0;
                                    a = BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_Buscar_Suc(initObject, VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text));
                                    if (!string.IsNullOrWhiteSpace(a))
                                    {

                                        if (VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text) == MODGPYF0.SyGet_OfiCod(unit, MODGUSR.UsrEsp.CentroCosto))
                                        {
                                            Msg = "La oficina de destino no puede ser igual al origen.";
                                            trace.TraceError(Msg);
                                            Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                            {
                                                Type = TipoMensaje.Error,
                                                Text = Msg,
                                                ControlName = "Tx_Datos_0"
                                            });
                                            Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                            return _retValue;
                                        }
                                        else
                                        {
                                            Frm_Origen_Fondos.Lb_Oficina.Text = a;
                                        }

                                    }
                                    else
                                    {
                                        Msg = "Ingresar nuevo código: Código de Oficina no válido.";
                                        trace.TraceError(Msg);
                                        Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                        {
                                            Type = TipoMensaje.Error,
                                            Text = Msg,
                                            ControlName = "Tx_Datos_0"
                                        });
                                        Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                        return _retValue;
                                    }

                                }

                            }
                            else if (_switchVar1 == T_MODGCON0.IdCta_CHMEBCH)
                            {
                                //Cheque M/ E Emi.x B. Chile
                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[0].Text))
                                {
                                    Msg = "Es necesario que se ingrese un Banco para poder realizar la operación.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }

                                b = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VNom(initObject, unit, VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text), MODXORI.VxMtoOri[Frm_Origen_Fondos.Indice_Monto].CodMon);
                                if (b == -1)
                                {
                                    Msg = "El Banco que Usted acaba de ingresar no es Corresponsal o no maneja la Moneda que Usted esta enviando.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }
                                else
                                {
                                    if (MODGTAB0.VNom[b].Nom_Ala == 1)
                                    {
                                        Msg = "El Banco que Usted acaba de ingresar es Aladi, por lo tanto se le solicita ingresar otro Banco que no sea Aladi.";
                                        trace.TraceError(Msg);
                                        Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                        {
                                            Type = TipoMensaje.Error,
                                            Text = Msg,
                                            ControlName = "Tx_Datos_0"
                                        });
                                        Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                        return _retValue;
                                    }

                                }

                            }
                            else if (_switchVar1 == T_MODGCON0.IdCta_CHMEOBC)
                            {
                                //Cheque M/ E Otro B (Chile)
                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[0].Text))
                                {
                                    Msg = "El Banco que Usted acaba de ingresar es Aladi, por lo tanto se le solicita ingresar otro Banco que no sea Aladi.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }

                                Swift = VB6Helpers.Trim(VB6Helpers.Mid(Frm_Origen_Fondos.Tx_Datos[0].Text, 1, 8));
                                Secuencia = VB6Helpers.Trim(VB6Helpers.Mid(Frm_Origen_Fondos.Tx_Datos[0].Text, 9, 11));
                                b = -1;
                                try
                                {
                                    var res = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones.SyGet_VBic(Swift, Secuencia, unit, initObject);
                                    if (res.Count == 0)
                                    {
                                        b = 0;
                                    }
                                }
                                catch
                                {
                                    b = 0;
                                }

                                if (~b != 0)
                                {
                                    Msg = "El Banco que Usted acaba de ingresar no está en la tabla de Sce_Bic, por lo tanto se le solicita ingresar otro Banco.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }

                            }
                            else if (_switchVar1 == T_MODGCON0.IdCta_VVOB)
                            {
                                //Vale Vista Otro Banco
                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[0].Text))
                                {
                                    Msg = "Es necesario que se ingrese un Banco para poder realizar la operación.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }

                                Swift = VB6Helpers.Trim(VB6Helpers.Mid(Frm_Origen_Fondos.Tx_Datos[0].Text, 1, 8));
                                Secuencia = VB6Helpers.Trim(VB6Helpers.Mid(Frm_Origen_Fondos.Tx_Datos[0].Text, 9, 11));
                                b = -1;
                                try
                                {
                                    var res = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones.SyGet_VBic(Swift, Secuencia, unit, initObject);
                                    if (res.Count == 0)
                                    {
                                        b = 0;
                                    }
                                }
                                catch
                                {
                                    b = 0;
                                }
                                if (~b != 0)
                                {
                                    Msg = "El Banco que Usted acaba de ingresar no está en la tabla de Sce_Bic, por lo tanto se le solicita ingresar otro Banco.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }

                            }
                            else if (_switchVar1 == T_MODGCON0.IdCta_CTACTEBC)
                            {

                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[0].Text))
                                {
                                    Msg = "Es necesario que se ingrese un Banco Aladi para poder realizar la operación.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    return _retValue;
                                }

                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[1].Text))
                                {
                                    Msg = "Es necesario que se ingrese un Número de Referencia para poder realizar la operación.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_1"
                                    });
                                    return _retValue;
                                }

                                //Valida Existencia de Banco Aladi.-
                                c = -1;
                                try
                                {
                                    var res = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones.SyGet_VBic(VB6Helpers.Left(Frm_Origen_Fondos.Tx_Datos[0].Text, 8), VB6Helpers.Right(Frm_Origen_Fondos.Tx_Datos[0].Text, 3), unit, initObject);
                                    if (res.Count == 0)
                                    {
                                        c = 0;
                                    }
                                }
                                catch
                                {
                                    c = 0;
                                }
                                if (~c != 0)
                                {
                                    Msg = "El Banco Aladi NO se encuentra registrado. Reporte este problema.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    return _retValue;
                                }

                                if (!Mdl_Funciones.VBic.BicAla)
                                {
                                    Msg = "El Banco ingresado NO se encuentra registrado como un Banco Aladi.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    return _retValue;
                                }

                                if (~BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_Valida_Aladi(VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[1].Text), initObject) != 0)
                                {
                                    Frm_Origen_Fondos.Tx_Datos[1].Enabled = true;
                                    return _retValue;
                                }

                            }
                            else if (_switchVar1 == T_MODGCON0.IdCta_CTAORD || _switchVar1 == T_MODGCON0.IdCta_DIVENPEN || _switchVar1 == T_MODGCON0.IdCta_CHVBNYM || _switchVar1 == 54)
                            {
                                //IdCta_OBCCIPLZ, 54   'Cta. Cte. Banco Central -- 'Corresponsal Cuenta Ordinaria
                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[0].Text))
                                {
                                    Msg = "Es necesario que se ingrese un Banco para poder realizar la operación.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }

                                b = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VNom(initObject, unit, VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text), MODXORI.VxMtoOri[Frm_Origen_Fondos.Indice_Monto].CodMon);
                                if (b == -1)
                                {
                                    Msg = "El Banco que Usted acaba de ingresar no es Corresponsal o no maneja la Moneda que Usted esta enviando.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }
                                else
                                {
                                    if (MODGTAB0.VNom[b].Nom_Ala == 1)
                                    {
                                        Msg = "El Banco que Usted acaba de ingresar es Aladi, por lo tanto se le solicita ingresar otro Banco que no sea Aladi.";
                                        trace.TraceError(Msg);
                                        Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                        {
                                            Type = TipoMensaje.Error,
                                            Text = Msg,
                                            ControlName = "Tx_Datos_0"
                                        });
                                        Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;

                                        return _retValue;
                                    }

                                }

                            }
                            else if (_switchVar1 >= 40 && _switchVar1 <= 53)
                            {
                                //Cuentas de Obligaciones y Check Verification
                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[0].Text))
                                {
                                    Msg = "Es necesario que se ingrese un Banco para poder realizar la operación.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;

                                    return _retValue;
                                }

                                b = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VAcr(initObject, unit, VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text), MODXORI.VxMtoOri[Frm_Origen_Fondos.Indice_Monto].CodMon);
                                if (b == 0)
                                {
                                    Msg = "El Banco que Usted acaba de ingresar no es Acreedor o no maneja la Moneda.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;

                                    return _retValue;
                                }

                            }
                            else if (_switchVar1 == T_MODGCON0.IdCta_BOEREG || _switchVar1 == T_MODGCON0.IdCta_CHEREG || _switchVar1 == T_MODGCON0.IdCta_OBLREG || _switchVar1 == T_MODGCON0.IdCta_OBLARE || _switchVar1 == T_MODGCON0.IdCta_ACEREG)
                            {
                                if (Module1.Codop.Cent_Costo == "729" || Module1.Codop.Cent_Costo == "829" || Module1.Codop.Cent_Costo == "827" || Module1.Codop.Cent_Costo == "826")
                                {
                                    Msg = "Esta Cuenta está habilitada solo para REGIONES";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }

                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[0].Text))
                                {
                                    Msg = "Es necesario que se ingrese un Banco para poder realizar la operación.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }

                                b = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VNom(initObject, unit, VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text), MODXORI.VxMtoOri[Frm_Origen_Fondos.Indice_Monto].CodMon);
                                if (b == -1)
                                {
                                    Msg = "El Banco que Usted acaba de ingresar no es Corresponsal o no maneja la Moneda que Usted esta enviando.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }
                                else
                                {
                                    if (MODGTAB0.VNom[b].Nom_Ala == 1)
                                    {
                                        Msg = "El Banco que Usted acaba de ingresar es Aladi, por lo tanto se le solicita ingresar otro Banco que no sea Aladi.";
                                        trace.TraceError(Msg);
                                        Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                        {
                                            Type = TipoMensaje.Error,
                                            Text = Msg,
                                            ControlName = "Tx_Datos_0"
                                        });
                                        Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                        return _retValue;
                                    }

                                }


                            }
                            else if (_switchVar1 == T_MODGCON0.IdCta_VAM || _switchVar1 == T_MODGCON0.IdCta_VAX || _switchVar1 == T_MODGCON0.IdCta_VAMC || _switchVar1 == T_MODGCON0.IdCta_VAMCC || _switchVar1 == T_MODGCON0.IdCta_VASC)
                            {
                                //Varios Acreedores Import. -- Varios Acreedores Export. -- 'Varios Acreedores Mcdo. Corr.
                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[0].Text))
                                {
                                    Msg = "Es necesario que se ingrese un Participante para poder realizar la operación.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }

                                razon = BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.SyGet_Partys(unit, VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text));
                                Frm_Origen_Fondos.Lb_Oficina.Text = MODGPYF1.Minuscula(VB6Helpers.Trim(razon));
                                if (string.IsNullOrWhiteSpace(razon))
                                {
                                    Msg = "El Participante que usted acaba de ingresar no existe dentro de los registros de los Partys.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }

                            }
                            else if (_switchVar1 == T_MODGCON0.IdCta_VVBCH)
                            {
                                //Vale Vista Bco.Chile
                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[0].Text))
                                {
                                    Msg = "Es necesario que ingrese una Oficina.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }

                                if (VB6Helpers.IsNumeric(VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text)))
                                {
                                    Frm_Origen_Fondos.Tx_Datos[0].MaxLength = 0;
                                    a = BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_Buscar_Suc(initObject, VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text));
                                    if (!string.IsNullOrWhiteSpace(a))
                                    {
                                        Frm_Origen_Fondos.Lb_Oficina.Text = a;
                                    }
                                    else
                                    {
                                        if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[0].Text))
                                        {
                                            Msg = "Ingresar nuevo código: Código de Oficina no válido.";
                                            trace.TraceError(Msg);
                                            Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                            {
                                                Type = TipoMensaje.Error,
                                                Text = Msg,
                                                ControlName = "Tx_Datos_0"
                                            });
                                            Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                            return _retValue;
                                        }

                                    }

                                }
                            }
                            else if (_switchVar1 == T_MODGCON0.IdCta_OPC)
                            {
                                //Orden de Pago Convenio
                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[0].Text))
                                {
                                    Msg = "Es necesario que se ingrese un Banco Aladi para poder realizar la operación.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }

                                b = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Fn_Get_VNom(initObject, unit, VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text));
                                if (b != -1)
                                {
                                    Msg = "El Banco que Usted acaba de ingresar es Corresponsal, por lo tanto se solicita ingresar otro Banco que no sea Corresponsal.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }

                                if (~BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_Valida_Aladi(VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[1].Text), initObject) != 0)
                                {
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;

                                    return _retValue;
                                }

                            }
                            else if (_switchVar1 == T_MODGCON0.IdCta_OPOP)
                            {
                                //Orden de Pago Otros Países
                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[0].Text))
                                {
                                    Msg = "Es necesario que se ingrese un Banco para poder realizar la operación.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }

                                b = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VNom(initObject, unit, VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text), MODXORI.VxMtoOri[Frm_Origen_Fondos.Indice_Monto].CodMon);
                                if (b == -1)
                                {
                                    Msg = "El Banco que Usted acaba de ingresar no es Corresponsal o no maneja la Moneda que Usted esta enviando.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }
                                else
                                {
                                    if (MODGTAB0.VNom[b].Nom_Ala == 1)
                                    {
                                        Msg = "El Banco que Usted acaba de ingresar es Aladi, por lo tanto se le solicita ingresar otro Banco que no sea Aladi.";
                                        trace.TraceError(Msg);
                                        Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                        {
                                            Type = TipoMensaje.Error,
                                            Text = Msg,
                                            ControlName = "Tx_Datos_0"
                                        });
                                        Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                        return _retValue;
                                    }

                                    if (Frm_Origen_Fondos.Tx_Datos[1].Text != MODGTAB0.VNom[b].Nom_cta)
                                    {
                                        Msg = "La Cuenta asociada al Banco Corresponsal no se encuentra registrada.";
                                        trace.TraceError(Msg);
                                        Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                        {
                                            Type = TipoMensaje.Error,
                                            Text = Msg,
                                            ControlName = "Tx_Datos_1"
                                        });
                                        Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                        return _retValue;
                                    }

                                }

                            }
                            else if (_switchVar1 == T_MODGCON0.IdCta_ONMN || _switchVar1 == T_MODGCON0.IdCta_ONME)
                            {
                                //Otro Nemónico M / N---- Otro Nemónico M / E
                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[0].Text))
                                {
                                    Msg = "Es necesario que se ingrese un Código de Nemónico para poder realizar la operación.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }

                                b = (short)BCH.Comex.Core.BL.XCFT.Modulos.MODGNCTA.Get_Cta(VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[0].Text), initObject, unit);
                                if (b == -1 || b == 0)
                                {
                                    Msg = "Es necesario que se ingrese un Código de Nemónico para poder realizar la operación.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_0"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                    return _retValue;
                                }
                                else
                                {
                                    /// Valida si la cuenta contable tiene o no nro de cuenta
                                    if (string.IsNullOrWhiteSpace(MODGNCTA.VCta[b].Cta_Num))
                                    {
                                        Msg = "No es posible obtener número de cuenta contable, favor de volver a repertir la acción hasta obtener número.";
                                        trace.TraceError(Msg);
                                        Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                        {
                                            Type = TipoMensaje.Error,
                                            Text = Msg,
                                            ControlName = "Tx_Datos_0"
                                        });
                                        //LimpiaNumPartida(initObject);
                                        Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                        Frm_Origen_Fondos.Lb_Oficina.Text = "";
                                        return _retValue;
                                    }
                                    short _switchVar2 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                                    if (_switchVar2 == T_MODGCON0.IdCta_ONMN)
                                    {
                                        if (MODGNCTA.VCta[b].Cta_Mon == 1)
                                        {
                                            Msg = "Usted seleccionó una Cuenta Nacional, por lo tanto el Nemónico que tendría que ingresar debería ser Nacional.";
                                            trace.TraceError(Msg);
                                            Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                            {
                                                Type = TipoMensaje.Error,
                                                Text = Msg,
                                                ControlName = "Tx_Datos_0"
                                            });
                                            Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                            Frm_Origen_Fondos.Lb_Oficina.Text = "";
                                            return _retValue;
                                        }
                                        else
                                        {
                                            Frm_Origen_Fondos.Lb_Oficina.Text = VB6Helpers.Trim(MODGNCTA.VCta[b].Cta_Nom.Value);
                                        }

                                    }
                                    else if (_switchVar2 == T_MODGCON0.IdCta_ONME)
                                    {
                                        if (MODGNCTA.VCta[b].Cta_Mon == 2)
                                        {
                                            Msg = "Usted seleccionó una Cuenta Extranjera, por lo tanto el Nemónico que tendría que ingresar debería ser Extranjero.";
                                            trace.TraceError(Msg);
                                            Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                            {
                                                Type = TipoMensaje.Error,
                                                Text = Msg,
                                                ControlName = "Tx_Datos_0"
                                            });
                                            Frm_Origen_Fondos.Tx_Datos[0].Enabled = true;
                                            Frm_Origen_Fondos.Lb_Oficina.Text = "";
                                            return _retValue;
                                        }
                                        else
                                        {
                                            Frm_Origen_Fondos.Lb_Oficina.Text = VB6Helpers.Trim(MODGNCTA.VCta[b].Cta_Nom.Value);
                                        }

                                    }

                                }

                            }
                            //************************************************************
                            break;
                        case 1:  //Tx_Datos(1)
                            short _switchVar3 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                            if (_switchVar3 == T_MODGCON0.IdCta_SCSMN || _switchVar3 == T_MODGCON0.IdCta_SCSME)
                            {
                                if (flag != 0)
                                {
                                    if (!string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[1].Text))
                                    {
                                        //Texto$ = "01;02;03;04"
                                        Texto = "01;02";
                                        if (VB6Helpers.Instr(Texto, VB6Helpers.Format(Frm_Origen_Fondos.Tx_Datos[1].Text, "00")) == 0)
                                        {
                                            Msg = "El Tipo de Movimiento debe estar entre 1 y 2.";
                                            trace.TraceError(Msg);
                                            Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                            {
                                                Type = TipoMensaje.Error,
                                                Text = Msg,
                                                ControlName = "Tx_Datos_1"
                                            });
                                            Frm_Origen_Fondos.Tx_Datos[1].Enabled = true;
                                            return _retValue;
                                        }

                                    }

                                    double _switchVar4 = VB6Helpers.Val(Frm_Origen_Fondos.Tx_Datos[1].Text);
                                    if (_switchVar4 == T_MODXORI.TP_INI)
                                    {
                                        Pr_Generar_Automatica_Ini(initObject, unit); //Generación Automática
                                    }
                                    else if (_switchVar4 == T_MODXORI.TP_CON || _switchVar4 == T_MODXORI.TP_COR)
                                    {
                                        //Ingreso Manual
                                        Frm_Origen_Fondos.Tx_Datos[2].Enabled = true;
                                    }
                                    else if (_switchVar4 == T_MODXORI.TP_COM)
                                    {
                                        Msg = "Desea hacer iniciativa de la partida ?";
                                        trace.TraceError(Msg);
                                        Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                        {
                                            Type = TipoMensaje.Informacion,
                                            Text = Msg,
                                            ControlName = "Tx_Datos_1"
                                        });
                                        if (resp == 6)
                                        {
                                            Pr_Generar_Automatica_Com(initObject, unit); //Generación Automática
                                        }
                                        else
                                        {
                                            Frm_Origen_Fondos.Tx_Datos[2].Enabled = true; //Ingreso Manual
                                        }

                                    }
                                    else if (_switchVar4 == 0)
                                    {
                                        Msg = "Es necesario que ingrese un Tipo de Movimiento.";
                                        trace.TraceError(Msg);
                                        Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                        {
                                            Type = TipoMensaje.Error,
                                            Text = Msg,
                                            ControlName = "Tx_Datos_1"
                                        });
                                        Frm_Origen_Fondos.Tx_Datos[1].Enabled = true;
                                        return _retValue;
                                    }

                                }

                            }
                            else if (_switchVar3 == T_MODGCON0.IdCta_CHMEBCH)
                            {
                                //Cheque M/ E Emi.x B. Chile
                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[1].Text))
                                {
                                    Msg = "Es necesario que se ingrese un Número de Cheque.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_1"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[1].Enabled = true;
                                    return _retValue;
                                }

                            }
                            else if (_switchVar3 == T_MODGCON0.IdCta_CHMEOBC)
                            {
                                //Cheque M/ E Otro B (Chile)
                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[1].Text))
                                {
                                    Msg = "Es necesario que se ingrese un Número de Cheque.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_1"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[1].Enabled = true;
                                    return _retValue;
                                }

                            }
                            else if (_switchVar3 == T_MODGCON0.IdCta_VVOB)
                            {
                                //Vale Vista Otro Banco
                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[1].Text))
                                {
                                    Msg = "Es necesario que se ingrese un Número de Vale Vista.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_1"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[1].Enabled = true;
                                    return _retValue;
                                }

                            }
                            else if (_switchVar3 == T_MODGCON0.IdCta_CTACTEBC || _switchVar3 == T_MODGCON0.IdCta_CTAORD || _switchVar3 == T_MODGCON0.IdCta_DIVENPEN || _switchVar3 == T_MODGCON0.IdCta_CHVBNYM || _switchVar3 == 54)
                            {
                                //IdCta_OBCCIPLZ, 54 'Cta. Cte. Banco Central -- 'Corresponsal Cuenta Ordinaria
                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[1].Text))
                                {
                                    Msg = "Es necesario que se ingrese un Número de Referencia para poder realizar la operación.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_1"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[1].Enabled = true;
                                    return _retValue;
                                }

                                if (MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_CTACTEBC)
                                {
                                    if (~BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_Valida_Aladi(VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[1].Text), initObject) != 0)
                                    {
                                        Frm_Origen_Fondos.Tx_Datos[1].Enabled = true;

                                        return _retValue;
                                    }

                                }

                            }
                            else if (_switchVar3 >= 40 && _switchVar3 <= 53)
                            {
                                //Cuentas de Obligaciones y Check Verification
                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[1].Text))
                                {
                                    Msg = "Es necesario que se ingrese un Número de Referencia para poder realizar la operación.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_1"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[1].Enabled = true;
                                    return _retValue;
                                }

                                if (MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_CTACTEBC)
                                {
                                    if (~BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_Valida_Aladi(VB6Helpers.Trim(Frm_Origen_Fondos.Tx_Datos[1].Text), initObject) != 0)
                                    {
                                        Frm_Origen_Fondos.Tx_Datos[1].Enabled = true;

                                        return _retValue;
                                    }

                                }

                            }
                            else if (_switchVar3 == T_MODGCON0.IdCta_VVBCH)
                            {
                                //Vale Vista Bco.Chile
                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[1].Text))
                                {
                                    Msg = "Es necesario que se ingrese un Número de Vale Vista.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_1"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[1].Enabled = true;
                                    return _retValue;
                                }

                            }
                            else if (_switchVar3 == T_MODGCON0.IdCta_OPC)
                            {
                                //Orden de Pago Convenio
                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[1].Text))
                                {
                                    Msg = "Es necesario que se ingrese un Número de Reembolso.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_1"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[1].Enabled = true;
                                    return _retValue;
                                }

                            }
                            else if (_switchVar3 == T_MODGCON0.IdCta_OPOP)
                            {
                                //Orden de Pago Otros Países
                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[1].Text))
                                {
                                    Msg = "Es necesario que se ingrese una Cta.Corriente.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_1"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[1].Enabled = true;
                                    return _retValue;
                                }

                            }

                            //******************************************
                            break;
                        case 2:  //Tx_Datos(2)
                            short _switchVar5 = MODXORI.VOvd[Frm_Origen_Fondos.Indice_Cuenta].NumCta;
                            if (_switchVar5 == T_MODGCON0.IdCta_SCSMN || _switchVar5 == T_MODGCON0.IdCta_SCSME)
                            {
                                //Saldos c/ Sucursales M / N.
                                if (!VB6Helpers.IsNumeric(Frm_Origen_Fondos.Tx_Datos[2].Text))
                                {
                                    Msg = "Debe ingresar un Número de Partida válido.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_2"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[2].Enabled = true;
                                    return _retValue;
                                }

                                num_par = VB6Helpers.Right("00000000" + Frm_Origen_Fondos.Tx_Datos[2].Text, 8);
                                Frm_Origen_Fondos.Tx_Datos[2].Text = VB6Helpers.CStr(num_par);
                                nums = VB6Helpers.Left(VB6Helpers.CStr(num_par), 6);
                                el_dv = VB6Helpers.Right(VB6Helpers.CStr(num_par), 2);
                                dv = BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_Calcula_Dig(VB6Helpers.Trim(VB6Helpers.CStr(nums)));
                                if (dv != el_dv)
                                {
                                    Msg = "Corregir Número de Partida. El Dígito Verificador no corresponde. (" + dv + ").";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_2"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[2].Enabled = true;
                                    return _retValue;
                                }

                            }
                            else if (_switchVar5 == T_MODGCON0.IdCta_OPOP)
                            {
                                //Orden de Pago Otros Países
                                if (string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[2].Text))
                                {
                                    Msg = "Es necesario que se ingrese un Número de Referencia.";
                                    trace.TraceError(Msg);
                                    Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Error,
                                        Text = Msg,
                                        ControlName = "Tx_Datos_2"
                                    });
                                    Frm_Origen_Fondos.Tx_Datos[2].Enabled = true;
                                    return _retValue;
                                }

                            }

                            //**************************************************
                            break;
                        case 3:  //L_Cuentas
                            if (Frm_Origen_Fondos.L_Cuentas.Items.Count == 0)
                            {
                                Msg = "El Cliente NO tiene Cuenta Corriente en la moneda especificada.";
                                trace.TraceError(Msg);
                                Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Error,
                                    Text = Msg,
                                    ControlName = "L_Cuentas"
                                });

                                return _retValue;
                            }

                            if (Frm_Origen_Fondos.L_Cuentas.ListIndex == -1)
                            {
                                Msg = "Es Necesario que seleccione una Cuenta.";
                                trace.TraceError(Msg);
                                Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Error,
                                    Text = Msg,
                                    ControlName = "L_Cuentas"
                                });

                                return _retValue;
                            }

                            //**************************************************
                            break;
                        case 4:  //L_Partys
                            if (Frm_Origen_Fondos.L_Partys.ListIndex == -1)
                            {
                                Msg = "Es Necesario que seleccione un Partys.";
                                trace.TraceError(Msg);
                                Frm_Origen_Fondos.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Error,
                                    Text = Msg,
                                    ControlName = "L_Partys"
                                });
                                Frm_Origen_Fondos.L_Partys.Enabled = true;

                                return _retValue;
                            }
                            break;
                    }
                }
                Frm_Origen_Fondos.ErrorEnOk = false;
                return 1;
            }
        }

        private static void Pr_Generar_Automatica_Ini(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;

            short si = (short)(false ? -1 : 0);
            short delista = 0;
            string linea = "";
            string TP = "";
            //Generación Automática
            if (~delista != 0)
            {
                if (Frm_Origen_Fondos.Tx_Datos[2].Enabled)
                {
                    si = (short)(true ? -1 : 0);
                }
            }
            else
            {
                /*
                NemMon
                MtoTot
                */
                linea = Frm_Origen_Fondos.l_ori.Items.ElementAt(Frm_Origen_Fondos.l_ori.ListIndex).GetColumn("NemMon");
                //TODO: @Emiliano --> Esto probablemente no va a funcionar de primera
                TP = MODGPYF0.copiardestring(linea, VB6Helpers.Chr(9), 7);
                if ((VB6Helpers.Val(TP) != T_MODXORI.TP_INI))
                {
                    si = (short)(true ? -1 : 0);
                    if ((VB6Helpers.Val(TP) == T_MODXORI.TP_COM) && Frm_Origen_Fondos.Tx_Datos[2].Enabled == false)
                    {
                        si = (short)(false ? -1 : 0);
                    }
                }

            }

            if (si != 0)
            {
                Frm_Origen_Fondos.Tx_Datos[2].Text = MODXORI.Fn_Genera_Num(initObject, unit);
                if (!string.IsNullOrWhiteSpace(Frm_Origen_Fondos.Tx_Datos[2].Text))
                {
                    Frm_Origen_Fondos.Tx_Datos[2].Enabled = false;
                }
            }

        }
        //****************************************************************************
        //   1.  Llena la lista l_ori ordenada por moneda.
        //****************************************************************************
        private static void Pr_Llena_L_ori(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;
            T_MODXORI MODXORI = initObject.MODXORI;

            short k = 0;
            short m = 0;
            short i = 0;
            Frm_Origen_Fondos.l_ori.Items.Clear();
            for (k = 0; k < Frm_Origen_Fondos.l_mto.Items.Count; k++)
            {
                m = MODXORI.VxMtoOri[int.Parse(Frm_Origen_Fondos.l_mto.get_ItemData(k))].CodMon;
                for (i = 0; i <= (short)VB6Helpers.UBound(MODXORI.VxOri); i++)
                {
                    if (MODXORI.VxOri[i].CodMon == m && MODXORI.VxOri[i].Status != T_MODXORI.ExOri_Eli)
                    {
                        var item = new UI_GridItem();

                        UI_GridItem nuevoItem = Fn_Linea_l_ori(initObject, i);
                        nuevoItem.Tag = MODXORI.VxOri[i];
                        Frm_Origen_Fondos.l_ori.Items.Add(nuevoItem);
                    }

                }
            }
            Frm_Origen_Fondos.l_ori.ListIndex = -1;
            l_ori_Click(initObject, unit);
        }

        //****************************************************************************
        //   1.  Retorna una linea para la lista de Orígenes de Fondos.
        //****************************************************************************
        private static UI_GridItem Fn_Linea_l_ori(InitializationObject initObject, short Indice)
        {
            T_MODXORI MODXORI = initObject.MODXORI;
            UI_GridItem item = new UI_GridItem();
            item.ID = Indice.ToString();
            item.AddColumn("NomOri", MODXORI.VxOri[Indice].NomOri);
            item.AddColumn("NemMon", MODXORI.VxOri[Indice].NemMon);
            item.AddColumn("MtoTot", MODGPYF0.forma(MODXORI.VxOri[Indice].MtoTot, T_MODGCON0.FormatoConDec));

            return item;
        }

        private static void Cargar_CodTran(InitializationObject initObject, string Tipcta, string Moneda, string CRDR)
        {
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_MODCVDIM MODCVDIM = initObject.MODCVDIM;

            short i = 0;
            Frm_Origen_Fondos.cmb_codtran.Items.Clear();
            for (i = 0; i < MODXORI.Vx_CodTran.Length; i++)
            {
                if (MODXORI.Vx_CodTran[i].tip_cta == Tipcta && VB6Helpers.Instr(MODXORI.Vx_CodTran[i].Moneda, Moneda) > 0 && MODXORI.Vx_CodTran[i].cr_dr == CRDR)
                {
                    Frm_Origen_Fondos.cmb_codtran.Items.Add(new UI_ComboItem()
                    {
                        Data = MODXORI.Vx_CodTran[i].nro_trx,
                        Value = MODGPYF1.Minuscula(VB6Helpers.Trim(MODXORI.Vx_CodTran[i].glosa_cosmos))
                    });

                    VB6Helpers.RedimPreserve(ref MODCVDIM.CtaCCDin, 0, VB6Helpers.UBound(MODCVDIM.CtaCCDin) + 1);
                    MODCVDIM.CtaCCDin[Frm_Origen_Fondos.cmb_codtran.Items.Count - 1].codtra = MODXORI.Vx_CodTran[i].cod_trx_cosmos;
                    if (VB6Helpers.Instr(Moneda, "CLP") > 0)
                    {
                        MODCVDIM.CtaCCDin[Frm_Origen_Fondos.cmb_codtran.Items.Count - 1].NemCta = Busca_OVD_MN(initObject, VB6Helpers.Format(MODCVDIM.CtaCCDin[Frm_Origen_Fondos.cmb_codtran.Items.Count - 1].codtra, "00000"), Moneda, i);
                    }
                    else
                    {
                        MODCVDIM.CtaCCDin[Frm_Origen_Fondos.cmb_codtran.Items.Count - 1].NemCta = Busca_OVD_ME(initObject, VB6Helpers.Format(MODCVDIM.CtaCCDin[Frm_Origen_Fondos.cmb_codtran.Items.Count - 1].codtra, "00000"), Moneda, i);
                    }

                    MODCVDIM.CtaCCDin[Frm_Origen_Fondos.cmb_codtran.Items.Count - 1].NumCta = VB6Helpers.Trim(VB6Helpers.Str(Frm_Origen_Fondos.cmb_codtran.Items.Count - 1));
                }

            }

            if (Frm_Origen_Fondos.cmb_codtran.Items.Count > 0)
            {
                Frm_Origen_Fondos.cmb_codtran.ListIndex = 0;
                cmb_codtran_Click(initObject);
            }

        }
        private static void LimpiaNumPartida(InitializationObject initObject)
        {
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;
            // Limpia los valores y oculta objetos.
            Frm_Origen_Fondos.LB_Referencia.Visible = false;
            Frm_Origen_Fondos.txtNumRef.Text = "";
            Frm_Origen_Fondos.txtNumRef.Visible = false;
        }

        //****************************************************************************
        //   1.  Si no están cargados los montos de los Origenes => Se cargan.
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        private static void Pr_Carga_l_mto(InitializationObject initObject, UnitOfWorkCext01 unit, UI_Grid Lista)
        {
            T_MODXORI MODXORI = initObject.MODXORI;
            short n = 0;
            short i = 0;
            n = (short)VB6Helpers.UBound(MODXORI.VxMtoOri);

            if (n == -1)
            {
                VB6Helpers.Redim(ref MODXORI.VxMtoOri, 0, 0);
            }
            Lista.Items.Clear();
            for (i = 0; i <= (short)n; i++)
            {
                Lista.Items.Add(Fn_Linea_l_mto(MODXORI, i));
            }

            if (Lista.Items.Count > 0)
            {
                Lista.ListIndex = 0;
                l_mto_Click(initObject, unit);
            }
        }


        //****************************************************************************
        //   1.  Retorna una linea para la lista de montos de los Orígenes.
        //****************************************************************************
        private static UI_GridItem Fn_Linea_l_mto(T_MODXORI MODXORI, short Indice)
        {
            UI_GridItem item = new UI_GridItem();
            item.AddColumn("NemMon", MODXORI.VxMtoOri[Indice].NemMon);
            item.AddColumn("MtoTot", Format.FormatCurrency(MODXORI.VxMtoOri[Indice].MtoTot, T_MODGCON0.FormatoConDec));
            item.ID = Indice.ToString();
            return item;
        }


        // UPGRADE_INFO (#0561): The 'Carga_OriFondos' symbol was defined without an explicit "As" clause.
        private static dynamic Carga_OriFondos(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;
            // UPGRADE_INFO (#0501): The 'i' member isn't used anywhere in current application.
            short i = 0;
            short j = 0;

            //'''''''''
            Frm_Origen_Fondos.Text1[0].Text = "";
            Frm_Origen_Fondos.Text1[1].Text = "";
            Frm_Origen_Fondos.Text1[2].Text = "";
            Frm_Origen_Fondos.Text1[3].Text = "";
            Frm_Origen_Fondos.Text1[4].Text = "";

            if (Frm_Origen_Fondos.l_mnd.get_ItemData_((short)Frm_Origen_Fondos.l_mnd.ListIndex) == (T_MODGTAB0.MndNac))
            {
                BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Carga_l_cta(initObject, Frm_Origen_Fondos.L_Cta, 1, false, true, -1, unit);
            }
            else
            {
                BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Carga_l_cta(initObject, Frm_Origen_Fondos.L_Cta, 2, false, true, -1, unit);
            }
            L_Cta_Click(initObject, unit);
            ////--------------------------------------------------
            ////Modificacion Carga Masiva - Realsystems
            ////Fecha: 21-09-2012
            ////------------------ Codigo Nuevo ------------------

            //if (MODCARMAS.CARGA_MASIVA == true)
            //{
            //    //If SPOT_OP = "OP" Then
            //    return null;
            //    //End If
            //}

            ////---------------- Fin Codigo Nuevo ----------------

            Frm_Origen_Fondos.Text1[0].Text = Mdl_Funciones_Varias.LC_ULT_BEN1;
            Frm_Origen_Fondos.Text1[1].Text = Mdl_Funciones_Varias.LC_ULT_BEN2;
            Frm_Origen_Fondos.Text1[2].Text = Mdl_Funciones_Varias.LC_RECVR_CORRES2;
            Frm_Origen_Fondos.Text1[3].Text = Mdl_Funciones_Varias.LC_PMNT_DET1;
            Frm_Origen_Fondos.Text1[4].Text = Mdl_Funciones_Varias.LC_CONREFNUM;  // Contract reference number
                                                                                  //''Me.Text1(4) = Mid$(VgCvd.OpeSin, 6, 2) & Mid$(VgCvd.OpeSin, 8, 3) & Mid$(VgCvd.OpeSin, 11, 5)   ' Contract reference number

            Frm_Origen_Fondos.frm_datos.Visible = true;
            //''''''''''''''''
            if (Format.StringToDouble(Mdl_Funciones_Varias.LC_PRD) == 62)
            {
                for (j = 0; j <= (short)(Frm_Origen_Fondos.L_Cta.ListCount - 1); j++)
                {
                    if (Frm_Origen_Fondos.L_Cta.get_ItemData_(j) == 16)
                    {
                        Frm_Origen_Fondos.L_Cta.ListIndex = j;
                        L_Cta_Click(initObject, unit);
                        break;
                    }
                }
            }

            Frm_Origen_Fondos.Tx_Datos[1].Text = Mdl_Funciones_Varias.LC_DEBIT_REF;
            if (Format.StringToDouble(Mdl_Funciones_Varias.LC_PRD) == 62)
            {
                Frm_Origen_Fondos.Tx_Datos[0].Text = Mdl_Funciones_Varias.LC_SWFT;  //CRACC
            }

            //ok_Click
            //Boton_Click (0)
            return null;
        }

        //****************************************************************************
        //   1.  True   : Si todo el Origen ha sido justificado.
        //   2.  False  : Lo contrario.
        //****************************************************************************
        private static short Fn_OrigenOK(InitializationObject initObject)
        {
            T_MODXORI MODXORI = initObject.MODXORI;
            short _retValue = 0;
            short i = 0;
            double dif = 0;
            //Ve si está todo justificado.-
            _retValue = (short)(true ? -1 : 0);
            MODXORI.VgxOri.HayVue = (short)(false ? -1 : 0);
            for (i = 0; i <= (short)VB6Helpers.UBound(MODXORI.VxMtoOri); i++)
            {
                dif = Format.StringToDouble(Format.FormatCurrency((BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_SumaVxOri(initObject, MODXORI.VxMtoOri[i].CodMon) - MODXORI.VxMtoOri[i].MtoTot), "0.00"));
                if (dif < 0)
                {
                    return (short)(false ? -1 : 0);
                }

                if (dif > 0)
                {
                    MODXORI.VgxOri.HayVue = (short)(true ? -1 : 0);
                }

            }

            return _retValue;
        }

        private static string Busca_OVD_MN(InitializationObject initObject, string codtra, string Moneda, short Indice)
        {
            T_MODXORI MODXORI = initObject.MODXORI;
            short c = 0;
            for (c = 0; c <= (short)VB6Helpers.UBound(MODXORI.VOvd); c++)
            {
                if (VB6Helpers.Instr(MODXORI.VOvd[c].codtra, codtra) > 0)
                {
                    if (MODXORI.VOvd[c].monnac == -1)
                    {
                        return MODXORI.VOvd[c].NemCta;
                    }

                }
                else
                {
                    if (MODXORI.VOvd[c].NumCta == T_MODGCON0.IdCta_CtaCteMANE && string.IsNullOrWhiteSpace(MODXORI.VOvd[c].codtra) && Moneda != "CLP")
                    {
                        return MODXORI.VOvd[c].NemCta;
                    }
                    else if (MODXORI.VOvd[c].NumCta == T_MODGCON0.IdCta_CtaCteMANN && string.IsNullOrWhiteSpace(MODXORI.VOvd[c].codtra.Trim()) && Moneda == "CLP")
                    {
                        return MODXORI.VOvd[c].NemCta;
                    }

                }

            }

            return "";
        }
        private static string Busca_OVD_ME(InitializationObject initObject, string codtra, string Moneda, short Indice)
        {
            T_MODXORI MODXORI = initObject.MODXORI;
            short c = 0;
            for (c = 0; c < MODXORI.VOvd.Length; c++)
            {
                if (VB6Helpers.Instr(MODXORI.VOvd[c].codtra, codtra) > 0)
                {
                    if (MODXORI.VOvd[c].monnac == 0)
                    {
                        return MODXORI.VOvd[c].NemCta;
                    }

                }
                else
                {
                    if (MODXORI.VOvd[c].NumCta == T_MODGCON0.IdCta_CtaCteMANE && string.IsNullOrWhiteSpace(MODXORI.VOvd[c].codtra) && Moneda != "CLP")
                    {
                        return MODXORI.VOvd[c].NemCta;
                    }
                    else if (MODXORI.VOvd[c].NumCta == T_MODGCON0.IdCta_CtaCteMANN && string.IsNullOrWhiteSpace(MODXORI.VOvd[c].codtra) && Moneda == "CLP")
                    {
                        return MODXORI.VOvd[c].NemCta;
                    }

                }

            }

            return "";
        }

        //****************************************************************************
        //   1.  Envía orden para validar los campos necesarios para la operación
        //       con respecto a "Saldos c/ Sucursales M/N." y si la validación es
        //       correcta carga los datos en los campos correspondientes dentro
        //       del arreglo VxOri.
        //****************************************************************************
        private static short Fn_Cargar_Saldos(InitializationObject initObject, UnitOfWorkCext01 unit, short Indice, short Saldo)
        {
            T_MODXORI MODXORI = initObject.MODXORI;

            short _retValue = 0;

            _retValue = 0;

            switch (Saldo)
            {
                case T_MODGCON0.IdCta_SCSMN:
                    if (Fn_Validar_Campos(initObject, unit, 0, 2, -1) == 0)
                    {
                        return _retValue;
                    }

                    break;
                case T_MODGCON0.IdCta_SCSME:
                    if (Fn_Validar_Campos(initObject, unit, 0, 2, -1) == 0)
                    {
                        return _retValue;
                    }

                    break;
            }

            MODXORI.VxOri[Indice].codofi = (short)VB6Helpers.Val(initObject.Frm_Origen_Fondos.Tx_Datos[0].Text);
            MODXORI.VxOri[Indice].TipMov = (short)VB6Helpers.Val(initObject.Frm_Origen_Fondos.Tx_Datos[1].Text);
            MODXORI.VxOri[Indice].NumPar = (int)VB6Helpers.Val(initObject.Frm_Origen_Fondos.Tx_Datos[2].Text);
            return 1;
        }

        //****************************************************************************
        //   1.  Envía orden para validar los campos necesarios para la operación
        //       con respecto a "Cheque M/E Emi. x B. Chile" y si la validación es
        //       correcta carga los datos en los campos correspondientes dentro del
        //       arreglo VxOri.
        //****************************************************************************
        private static short Fn_Cargar_Cheques(InitializationObject initObject, UnitOfWorkCext01 unit, short Indice)
        {
            T_MODXORI MODXORI = initObject.MODXORI;
            short _retValue = 0;

            _retValue = 0;

            if (Fn_Validar_Campos(initObject, unit, 0, 1, -1) == 0)
            {
                return _retValue;
            }

            MODXORI.VxOri[Indice].CodSwf = VB6Helpers.Trim(initObject.Frm_Origen_Fondos.Tx_Datos[0].Text);
            MODXORI.VxOri[Indice].numdoc = VB6Helpers.Trim(initObject.Frm_Origen_Fondos.Tx_Datos[1].Text);
            return 1;
        }


        //****************************************************************************
        //   1.  Envía orden para validar los campos necesarios para la operación
        //       con respecto a "Vale Vista Otro Banco" y si la validación es
        //       correcta carga los datos en los campos correspondientes dentro
        //       del arreglo VxOri.
        //****************************************************************************
        private static short Fn_Cargar_Vales_Vistas(InitializationObject initObject, UnitOfWorkCext01 unit, short Indice)
        {
            T_MODXORI MODXORI = initObject.MODXORI;

            short _retValue = 0;

            _retValue = 0;

            if (Fn_Validar_Campos(initObject, unit, 0, 1, -1) == 0)
            {
                return _retValue;
            }

            MODXORI.VxOri[Indice].CodSwf = VB6Helpers.Trim(initObject.Frm_Origen_Fondos.Tx_Datos[0].Text);
            MODXORI.VxOri[Indice].numdoc = VB6Helpers.Trim(initObject.Frm_Origen_Fondos.Tx_Datos[1].Text);
            return 1;
        }


        //****************************************************************************
        //   1.  Envía orden para validar los campos necesarios para la operación
        //       con respecto a "Cta. Cte. Banco Central" y si la validación es
        //       correcta carga los datos en los campos correspondientes dentro del
        //       arreglo VxOri.
        //****************************************************************************
        private static short Fn_Cargar_Bco_Central(InitializationObject initObject, UnitOfWorkCext01 unit, short Indice)
        {
            T_MODXORI MODXORI = initObject.MODXORI;
            short _retValue = 0;

            _retValue = 0;

            if (Fn_Validar_Campos(initObject, unit, 0, 1, -1) == 0)
            {
                return _retValue;
            }

            MODXORI.VxOri[Indice].CodSwf = VB6Helpers.Trim(initObject.Frm_Origen_Fondos.Tx_Datos[0].Text);
            MODXORI.VxOri[Indice].numdoc = VB6Helpers.Trim(initObject.Frm_Origen_Fondos.Tx_Datos[1].Text);
            return 1;
        }

        //****************************************************************************
        //   1.  Envía orden para validar los campos necesarios para la operación
        //       con respecto a "Corresponsal cuenta ordinaria" y si la validación
        //       es correcta carga los datos en los campos correspondientes dentro
        //       del arreglo VxOri.
        //****************************************************************************
        private static short Fn_Cargar_Corresponsal(InitializationObject initObject, UnitOfWorkCext01 unit, short Indice)
        {
            T_MODXORI MODXORI = initObject.MODXORI;

            short _retValue = 0;

            _retValue = 0;

            if (Fn_Validar_Campos(initObject, unit, 0, 1, -1) == 0)
            {
                return _retValue;
            }
            MODXORI.VxOri[Indice].CodSwf = VB6Helpers.Trim(initObject.Frm_Origen_Fondos.Tx_Datos[0].Text);
            MODXORI.VxOri[Indice].numdoc = VB6Helpers.Trim(initObject.Frm_Origen_Fondos.Tx_Datos[1].Text);
            return 1;
        }

        //****************************************************************************
        //   1.  Envía orden para validar los campos necesarios para la operación
        //       con respecto a "Divisas Pendientes." y si la validación es correcta
        //       carga los datos en los campos correspondientes dentro del arreglo
        //       VxOri.
        //****************************************************************************
        private static short Fn_Cargar_Divisas_Pendientes(InitializationObject initObject, UnitOfWorkCext01 unit, short Indice)
        {
            T_MODXORI MODXORI = initObject.MODXORI;

            short _retValue = 0;

            _retValue = 0;

            if (Fn_Validar_Campos(initObject, unit, 0, 1, -1) == 0)
            {
                return _retValue;
            }
            MODXORI.VxOri[Indice].CodSwf = VB6Helpers.Trim(initObject.Frm_Origen_Fondos.Tx_Datos[0].Text);
            MODXORI.VxOri[Indice].numdoc = VB6Helpers.Trim(initObject.Frm_Origen_Fondos.Tx_Datos[1].Text);
            return 1;
        }

        //****************************************************************************
        //   1.  Envía orden para validar los campos necesarios para la operación
        //       con respecto a "Varios Acreedores Import., Varios Acreedores Export.,
        //       Varios Acreedores Mcdo. Corr" y si la validación es correcta carga
        //       los datos en los campos correspondientes dentro del arreglo VxOri.
        //****************************************************************************
        private static short Fn_Cargar_Acreedores(InitializationObject initObject, UnitOfWorkCext01 unit, short Indice, short Acreedores)
        {
            T_MODXORI MODXORI = initObject.MODXORI;

            short _retValue = 0;

            _retValue = 0;
            switch (Acreedores)
            {
                case T_MODGCON0.IdCta_VAM:
                    if ((VB6Helpers.Trim(initObject.Frm_Origen_Fondos.VAM_OK) == "00") || (string.IsNullOrWhiteSpace(initObject.Frm_Origen_Fondos.VAM_OK)))
                    {
                        if (Fn_Validar_Campos(initObject, unit, 0, 0, -1) == 0)
                        {
                            return _retValue;
                        }

                    }

                    break;
                case T_MODGCON0.IdCta_VAX:
                    if ((VB6Helpers.Trim(initObject.Frm_Origen_Fondos.VAX_OK) == "00") || (string.IsNullOrWhiteSpace(initObject.Frm_Origen_Fondos.VAX_OK)))
                    {
                        if (Fn_Validar_Campos(initObject, unit, 0, 0, -1) == 0)
                        {
                            return _retValue;
                        }

                    }

                    break;
                case T_MODGCON0.IdCta_VAMC:
                case T_MODGCON0.IdCta_VAMCC:
                case T_MODGCON0.IdCta_VASC:
                    if ((VB6Helpers.Trim(initObject.Frm_Origen_Fondos.VAMC_OK) == "00") || (string.IsNullOrWhiteSpace(initObject.Frm_Origen_Fondos.VAMC_OK)))
                    {
                        if (Fn_Validar_Campos(initObject, unit, 0, 0, -1) == 0)
                        {
                            return _retValue;
                        }

                    }

                    break;
            }

            MODXORI.VxOri[Indice].IdPrty = VB6Helpers.Trim(initObject.Frm_Origen_Fondos.Tx_Datos[0].Text);
            return 1;
        }

        //****************************************************************************
        //   1.  Envía orden para validar los campos necesarios para la operación
        //       con respecto a "Vale Vista Bco. Chile" y si la validación es
        //       correcta carga los datos en los campos correspondientes dentro
        //       del arreglo VxOri.
        //****************************************************************************
        private static short Fn_Cargar_Vales_Vista_Bco(InitializationObject initObject, UnitOfWorkCext01 unit, short Indice)
        {
            T_MODXORI MODXORI = initObject.MODXORI;

            short _retValue = 0;

            _retValue = 0;

            if ((VB6Helpers.Trim(initObject.Frm_Origen_Fondos.VVBCH_OK) == "00") || (string.IsNullOrWhiteSpace(initObject.Frm_Origen_Fondos.VVBCH_OK)))
            {
                if (Fn_Validar_Campos(initObject, unit, 0, 1, -1) == 0)
                {
                    return _retValue;
                }

            }
            else if (VB6Helpers.Trim(initObject.Frm_Origen_Fondos.VVBCH_OK) == "01")
            {
                if (Fn_Validar_Campos(initObject, unit, 1, 1, -1) == 0)
                {
                    return _retValue;
                }

            }

            MODXORI.VxOri[Indice].codofi = (short)VB6Helpers.Val(initObject.Frm_Origen_Fondos.Tx_Datos[0].Text);
            MODXORI.VxOri[Indice].numdoc = VB6Helpers.Trim(initObject.Frm_Origen_Fondos.Tx_Datos[1].Text);
            return 1;
        }


        //****************************************************************************
        //   1.  Envía orden para validar los campos necesarios para la operación
        //       con respecto a "Orden de Pago Convenio" y si la validación es
        //       correcta carga los datos en los campos correspondientes dentro
        //       del arreglo VxOri.
        //****************************************************************************
        private static short Fn_Cargar_Orden_Convenio(InitializationObject initObject, UnitOfWorkCext01 unit, short Indice)
        {

            T_MODXORI MODXORI = initObject.MODXORI;
            short _retValue = 0;

            _retValue = 0;

            if (Fn_Validar_Campos(initObject, unit, 0, 1, -1) == 0)
            {
                return _retValue;
            }

            MODXORI.VxOri[Indice].CodSwf = VB6Helpers.Trim(initObject.Frm_Origen_Fondos.Tx_Datos[0].Text);
            MODXORI.VxOri[Indice].numdoc = VB6Helpers.Trim(initObject.Frm_Origen_Fondos.Tx_Datos[1].Text);
            return 1;
        }

        //****************************************************************************
        //   1.  Envía orden para validar los campos necesarios para la operación
        //       con respecto a "Orden de Otros Países" y si la validación es
        //       correcta carga los datos en los campos correspondientes dentro
        //       del arreglo VxOri.
        //****************************************************************************
        private static short Fn_Cargar_Orden_Paises(InitializationObject initObject, UnitOfWorkCext01 unit, short Indice)
        {
            T_MODXORI MODXORI = initObject.MODXORI;

            short _retValue = 0;

            _retValue = 0;

            if (Fn_Validar_Campos(initObject, unit, 0, 2, -1) == 0)
            {
                return _retValue;
            }

            MODXORI.VxOri[Indice].CodSwf = VB6Helpers.Trim(initObject.Frm_Origen_Fondos.Tx_Datos[0].Text);
            MODXORI.VxOri[Indice].ctacte = VB6Helpers.Trim(initObject.Frm_Origen_Fondos.Tx_Datos[1].Text);
            MODXORI.VxOri[Indice].numdoc = VB6Helpers.Trim(initObject.Frm_Origen_Fondos.Tx_Datos[2].Text);
            return 1;
        }

        //****************************************************************************
        //   1.  Envía orden para validar los campos necesarios para la operación
        //       con respecto a "Otro Nemónico M/N ---- Otro Nemónico M/E" y si la
        //       validación es correcta carga los datos en los campos correspondientes
        //       dentro del arreglo VxOri.
        //****************************************************************************
        private static short Fn_Cargar_Otro_Nemonico(InitializationObject initObject, UnitOfWorkCext01 unit, short Indice, short Nemonico)
        {
            T_MODXORI MODXORI = initObject.MODXORI;
            short _retValue = 0;

            _retValue = 0;
            switch (Nemonico)
            {
                case T_MODGCON0.IdCta_ONMN:
                    if (Fn_Validar_Campos(initObject, unit, 0, 0, -1) == 0)
                    {
                        return _retValue;
                    }
                    break;
                case T_MODGCON0.IdCta_ONME:
                    if (Fn_Validar_Campos(initObject, unit, 0, 0, -1) == 0)
                    {
                        return _retValue;
                    }
                    break;
            }

            MODXORI.VxOri[Indice].NemCta = VB6Helpers.Trim(initObject.Frm_Origen_Fondos.Tx_Datos[0].Text);
            return 1;
        }

        private static void Pr_Generar_Automatica_Com(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Origen_Fondos Frm_Origen_Fondos = initObject.Frm_Origen_Fondos;

            short si = (short)(false ? -1 : 0);
            short delista = 0;
            string linea = "";
            string TP = "";
            //Generación Automática
            if (~delista != 0)
            {
                if (Frm_Origen_Fondos.Tx_Datos[2].Enabled)
                {
                    si = (short)(true ? -1 : 0);
                }
            }
            else
            {
                linea = initObject.Frm_Origen_Fondos.l_ori.Items.ElementAt((short)initObject.Frm_Origen_Fondos.l_ori.ListIndex).GetColumn("NomOri");
                TP = MODGPYF0.copiardestring(linea, VB6Helpers.Chr(9), 7);
                if ((VB6Helpers.Val(TP) != T_MODXORI.TP_COM))
                {
                    si = (short)(true ? -1 : 0);
                    if ((VB6Helpers.Val(TP) == T_MODXORI.TP_INI) && Frm_Origen_Fondos.Tx_Datos[2].Enabled == false)
                    {
                        si = (short)(false ? -1 : 0);
                    }
                }

            }

            if (si != 0)
            {
                Frm_Origen_Fondos.Tx_Datos[2].Text = MODXORI.Fn_Genera_Num(initObject, unit);
                if (!string.IsNullOrEmpty(Frm_Origen_Fondos.Tx_Datos[2].Text))
                {
                    Frm_Origen_Fondos.Tx_Datos[2].Enabled = false;
                }
            }

        }


        #endregion
    }
}
