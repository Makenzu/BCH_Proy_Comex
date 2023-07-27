using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public class Frm_PlvSO
    {
        public static void Bot_Acepta_Click(InitializationObject initObj)
        {
            short TotPlanilla = 0;
            short i = 0;
            short TotReg = 0;
            TotReg = (short)VB6Helpers.UBound(initObj.MODPREEM.Vx_PReem);


            initObj.MODANUVI.Vx_AnuReem.HayRee = (short)(true ? -1 : 0);
            for (i = 0; i <= (short)TotReg; i++)
            {
                if (initObj.MODPREEM.Vx_PReem[i].Estado == 1)
                {
                    initObj.MODPREEM.Vx_PReem[i].numdec =
                        initObj.MODPREEM.Vx_PReem[i].numdec != "________________-_" ? initObj.MODPREEM.Vx_PReem[i].numdec : "";
                    //total de planillas
                    TotPlanilla = (short)(TotPlanilla + 1);
                }
            }

            MODPREEM.Pr_LlenaMtoRee(initObj.MODPREEM, initObj.MODANUVI);

            //Refunde
            //------------------
            MODPREEM.Pr_RefundeGrDO(initObj.MODCVDIMMM);
            //------------------

            initObj.MODANUVI.Vx_AnuReem.TotPln = TotPlanilla;
            initObj.MODANUVI.Vx_AnuReem.AcepRee = (short)(true ? -1 : 0);

            initObj.Frm_PlvSO = null;
            initObj.FormularioQueAbrir = "PlvSO_Finish";
            //initObj.Mdi_Principal.BUTTONS["tbr_dedfondos"].Enabled = true;
            //if(initObj.MODANUVI.Vx_AnuReem.TotPln >= 1)
            //{
            //    initObj.Mdi_Principal.BUTTONS["tbr_planilla4"].Enabled = true;
            //}

            ////VB6Helpers.Unload(this);
        }

        public static void Bot_Cancel_Click(InitializationObject initObj)
        {

            initObj.MODPREEM.Vx_PReem = new Plan_Reemp[0];
            initObj.MODVPLE.IntPla = new T_IntPla[0];

            initObj.MODGFYS.RIdiFin = new R_Idi[0];
            initObj.MODCVDIMMM.RDecFin = new r_dec[0];

            initObj.FormularioQueAbrir = "Index";
            initObj.VieneDe = String.Empty;
            VB6Helpers.Erase(ref initObj.MODGFYS.RIdiIni);
            VB6Helpers.Erase(ref initObj.MODCVDIMMM.RDecIni);
            initObj.Frm_PlvSO = null;
            //VB6Helpers.Unload(this);
        }

        public static void Bot_Clientes_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short NumParty;

            //TODO: Revisar: getParty tiene un .show adentro, cambio a GetParty1_2
            NumParty = Module1.GetParty1_2(initObj.MODGCVD.Beneficiario, -1, 0, initObj, uow);
            if (NumParty != 0)
            {
                initObj.MODGCVD.VgCvd.IndPrt = T_MODGCVD.ICli;
                initObj.MODGCVD.VgCvd.PrtCli = initObj.Module1.PartysOpe[T_MODGCVD.ICli].LlaveArchivo;
                initObj.MODGCVD.VgCvd.rutcli = initObj.Module1.PartysOpe[T_MODGCVD.ICli].rut;
                initObj.Frm_PlvSO.Pn_RutImp.Text = MODGFYS.Rut_Formateado(initObj.MODGCVD.VgCvd.rutcli);
                initObj.Frm_PlvSO.Pn_Import.Text = initObj.Module1.PartysOpe[T_MODGCVD.ICli].NombreUsado;
            }
            else if (NumParty == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "La Operación no podrá ser grabada mientras no se ingrese el Participante.",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODANUVI.MsgPlaSO
                });
                return;
            }
            // UPGRADE_WARNING (#80F4): The Screen6.MousePointer property sets or returns the MousePointer property of the active form, but only if it's a VB6Form.
            //VB6Project.Screen.MousePointer = 0;
        }

        public static void Bot_NoFinal_Click(InitializationObject initObj)
        {
            UI_Frm_PlvSO Frm_PlvSO = initObj.Frm_PlvSO;
            short IndLis = 0;
            if (initObj.Frm_PlvSO.Lt_Final.ListIndex > -1)
            {
                IndLis = Convert.ToInt16(initObj.Frm_PlvSO.Lt_Final.get_ItemId((short)initObj.Frm_PlvSO.Lt_Final.ListIndex));
                initObj.MODPREEM.Vx_PReem[IndLis].Estado = 9;
                initObj.Frm_PlvSO.Lt_Final.RemoveItem((short)initObj.Frm_PlvSO.Lt_Final.ListIndex);
                //initObj.Frm_PlvSO.Lt_Final.Refresh();
                Pr_LimIdiDecSO(initObj);
                Pr_LimOtros(initObj);
                initObj.Frm_PlvSO.IndiceFin = -1;
            }
        }

        public static void Bot_OkDec_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            UI_Frm_PlvSO Frm_PlvSO = initObj.Frm_PlvSO;
            short x = 0;
            string s = "";
            string p = "";
            var MODGCVD = initObj.MODGCVD;

            initObj.Frm_PlvSO.Tx_MtoFob.Enabled = true;

            // UPGRADE_INFO (#0501): The 'PrtImp' member isn't used anywhere in current application.
            string PrtImp = "";

            if (~Fn_ValIdiDecSO(initObj) != 0)
            {
                return;
            }

            //Paridad y Tipo Cambio.-
            //------------------------

            if ((int)initObj.Frm_PlvSO.Ch_Transf.Value == 0)
            {
                Pr_ParyTipCa(initObj, uow);

                initObj.Frm_PlvSO.Tx_Observ.Text = initObj.Frm_PlvSO.guardaobs;
            }
            else
            {
                initObj.Frm_PlvSO.Tx_TipCam.Text = VB6Helpers.Format("0", "0.0000");
                if (VB6Helpers.Instr(initObj.Frm_PlvSO.Tx_Observ.Text, "Planilla de transferencia") == 0)
                {
                    initObj.Frm_PlvSO.Tx_Observ.Text = initObj.Frm_PlvSO.Tx_Observ.Text + " Planilla de transferencia.";
                }
                initObj.Frm_PlvSO.Tx_Paridad.Text =
                    Format.FormatCurrency(MODGTAB1.SyGet_Vmc(initObj.MODGTAB0, uow, Convert.ToInt16(initObj.Frm_PlvSO.Cb_Moneda.get_ItemId((short)initObj.Frm_PlvSO.Cb_Moneda.ListIndex)), DateTime.Now.ToString("yyyy-MM-dd"), "P"), T_MODGPYF2.Formato_Par);

                float paridad = 0;
                float.TryParse(initObj.Frm_PlvSO.Tx_Paridad.Text, out paridad);
                if (paridad == 0f)
                {
                    //Si no hay paridad, se habilita
                    initObj.Frm_PlvSO.Tx_Paridad.Enabled = true;  //el campo para su ingreso.-
                }
            }

            //Importador
            //------------------------
            //djt
            initObj.Frm_PlvSO.Pn_RutImp.Text = MODGFYS.Rut_Formateado(initObj.Module1.PartysOpe[T_MODGCVD.ICli].rut);
            //djt
            initObj.Frm_PlvSO.Pn_Import.Text = initObj.Module1.PartysOpe[T_MODGCVD.ICli].NombreUsado;
            initObj.Frm_PlvSO.Cb_Pbc.ListIndex = MODGPYF0.PosLista(initObj.Frm_PlvSO.Cb_Pbc, initObj.MODGSCE.VGen.CodPbc);

            initObj.Frm_PlvSO.Tx_NumDec.Text = initObj.Frm_PlvSO.Tx_NumDec.Text != "________________-_" ? initObj.Frm_PlvSO.Tx_NumDec.Text : "";
            initObj.Frm_PlvSO.Tx_NumDec.Text = "";
            if (VB6Helpers.Trim(initObj.Frm_PlvSO.Tx_NumDec.Text) == "")
            {
                initObj.Frm_PlvSO.numdec = "";
            }
            else
            {
                initObj.Frm_PlvSO.numdec = VB6Helpers.Trim(initObj.Frm_PlvSO.Tx_NumDec.Text).Replace("-", "");
            }
            initObj.Frm_PlvSO.FecDec = VB6Helpers.Format(initObj.Frm_PlvSO.Tx_FecDec.Text, "yyyy-MM-dd");
            initObj.Frm_PlvSO.CodPag = VB6Helpers.CShort(MODGPYF1.ValTexto(initObj.MODGPYF0, initObj.Frm_PlvSO.Tx_CodPag.Text));

            if ((int)initObj.Frm_PlvSO.Ch_Endoso.Value == 0)
            {
                if ((int)initObj.Frm_PlvSO.Ch_ClauRo.Value == 0)
                {
                    //NO es clausula Roja.-
                    if (!string.IsNullOrEmpty(initObj.Frm_PlvSO.numdec))
                    {
                        if (MODPREEM.SyGet_IdiDec(initObj, uow, initObj.Frm_PlvSO.numdec, initObj.Frm_PlvSO.FecDec, initObj.Frm_PlvSO.CodPag, MODGCVD.VgCvd.PrtCli) != 0)
                        {
                            if (initObj.MODPREEM.VxIdiDec.flag == 1)
                            {
                                if (initObj.Frm_PlvSO.numdec != "________________-_")
                                {
                                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                                    {
                                        Text = "Esta Declaración No existe.",
                                        Type = TipoMensaje.Informacion,
                                        Title = T_MODANUVI.MsgPlaSO
                                    });
                                }
                                return;
                            }

                            if (initObj.MODPREEM.VxIdiDec.ForPag != initObj.Frm_PlvSO.CodPag)
                            {
                                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                                {
                                    Text = "La forma de pago ingresada no corresponde a la definida en la declaración. Ingrésela nuevamente.",
                                    Type = TipoMensaje.Informacion,
                                    Title = T_MODANUVI.MsgPlaSO
                                });

                                //initObj.Frm_PlvSO.Tx_CodPag.SetFocus();TODO ARKANO
                                return;
                            }

                            //El primer num. de conocim. Si no es Clausula Roja y
                            //si no es Endoso.-
                            //-------------------------------------------------------------------
                            x = MODPREEM.Getn_Conoc(initObj, uow, initObj.Frm_PlvSO.numdec, initObj.Frm_PlvSO.FecDec);
                            if (VB6Helpers.UBound(initObj.MODPREEM.Vx_ConEmb) >= 1)
                            {
                                initObj.Frm_PlvSO.Tx_NroCon.Text = MODGFYS.FtoNumCon(initObj.MODPREEM.Vx_ConEmb[1].NumBlw);
                                initObj.Frm_PlvSO.Tx_FecCon.Text = VB6Helpers.Format(initObj.MODPREEM.VxIdiDec.FecEmb, "yyyy-MM-dd");
                            }

                            if (initObj.MODPREEM.VxIdiDec.codpai != 0)
                            {
                                initObj.Frm_PlvSO.Cb_PPago.ListIndex = MODGPYF0.PosLista(initObj.Frm_PlvSO.Cb_PPago, initObj.MODPREEM.VxIdiDec.codpai);
                            }

                        }

                    }
                    else
                    {
                        initObj.MODPREEM.VxIdiDec.DisFob_M = 9999999999999.99;
                        initObj.MODPREEM.VxIdiDec.DisFle_M = 9999999999999.99;
                        initObj.MODPREEM.VxIdiDec.DisSeg_M = 9999999999999.99;
                        initObj.MODPREEM.VxIdiDec.DisCif_M = 9999999999999.99;
                    }

                }
                else if ((int)initObj.Frm_PlvSO.Ch_ClauRo.Value == -1)
                {
                    //SI es clausula Roja.-

                    // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'MODGPYF1.ValTexto(Tx_CodPag)'. Consider using the GetDefaultMember6 helper method.
                    if (Format.StringToDouble((initObj.Frm_PlvSO.Tx_CodPag.Text)) != 32)
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "El Código de Forma de Pago debe ser 32 cuando existe Cláusula Roja",
                            Type = TipoMensaje.Informacion,
                            Title = T_MODANUVI.MsgPlaSO
                        });
                        initObj.Frm_PlvSO.Tx_CodPag.Text = "32";
                    }

                }

            }
            else if ((int)initObj.Frm_PlvSO.Ch_Endoso.Value == -1)
            {
                s = Mdl_Funciones.SyGet_Fra(initObj, uow, 91, "E", p);
                initObj.Frm_PlvSO.Tx_Observ.Text = s;
            }

            //Si es transferencia no vemos el acceso.-
            if ((int)initObj.Frm_PlvSO.Ch_Endoso.Value == 0)
            {
                MODPREEM.Pr_PlAcceso(initObj, uow, initObj.Frm_PlvSO.FecDec, initObj.Frm_PlvSO.numdec);
            }

            Pr_HabilitaSO(initObj, -1);

            //initObj.Frm_PlvSO.Tx_FecVen.SetFocus();TODO ARKANO
            //''Cb_PPago.ListIndex = PosLista(Cb_PPago, 997)  'Pais Chile = 997.-

            //Flag para determinar si hay datos de planilla que no han sido
            //ingresados a la lista.-
            initObj.Frm_PlvSO.IngPlan = (short)(true ? -1 : 0);
        }

        public static void Bot_OkFinal_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            UI_Frm_PlvSO Frm_PlvSO = initObj.Frm_PlvSO;
            short Indice_Ree = 0;
            int Plani = VB6Helpers.CInt(MODGPYF1.ValTexto(initObj.MODGPYF0, initObj.Frm_PlvSO.Pn_NroPre.Text));

            ValidaTipoCambio(initObj);

            //Numero de Planilla
            if (Plani == 0)
            {
                //=> si aun no se ingresa a la lista
                Plani = 999999;
            }

            //Validacion de campos
            if (~Fn_ValIdiDecSO(initObj) != 0)
            {
                return;
            }
            if (~Fn_ValiCamposSO(initObj) != 0)
            {
                return;
            }

            //Llenamos la estructura
            Indice_Ree = Fn_LlenaPlaSO(initObj, uow);
            //Si retorna -1 se genero un error por lo cual se aborta
            if(Indice_Ree == -1)
            {
                return;
            }
            if ((int)initObj.Frm_PlvSO.Ch_Endoso.Value == 0)
            {
                //si no es endoso
                //Relacion Declaracion
                if (initObj.MODPREEM.Vx_PReem[Indice_Ree].ClauRo == 0)
                {
                    MODPREEM.Pr_RelDec(initObj, Indice_Ree, initObj.MODPREEM.NuevaPlan);
                }
                //Relacion Rebajes
                MODPREEM.Pr_RelReb(initObj, Indice_Ree, initObj.MODPREEM.NuevaPlan);
            }

            //Cargamos la lista
            Pr_CargaLisSO(initObj);

            //Limpiamos los campos
            Pr_LimIdiDecSO(initObj);
            Pr_LimOtros(initObj);
            initObj.Frm_PlvSO.Cb_Moneda.Enabled = false;

            initObj.Frm_PlvSO.Bot_Acepta.Enabled = true;
            initObj.Frm_PlvSO.IngPlan = (short)(false ? -1 : 0);
            initObj.FormularioQueAbrir = String.Empty;
        }

        public static void Cb_Moneda_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {

            //La moneda no puede ser Peso.-
            if (initObj.Frm_PlvSO.Cb_Moneda.ListIndex != -1)
            {
                if (initObj.Frm_PlvSO.Cb_Moneda.get_ItemId(Convert.ToInt16(initObj.Frm_PlvSO.Cb_Moneda.ListIndex)) == "1")
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "La Moneda No Puede Ser Pesos.",
                        Type = TipoMensaje.Informacion,
                        Title = "Validación de Data"
                    });

                    initObj.Frm_PlvSO.Cb_Moneda.ListIndex = -1;
                    return;
                }

                Pr_GetMoneda(initObj, uow);
            }

            if (initObj.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
            {
                initObj.Mdl_Funciones_Varias.LC_NOM_MDA = initObj.Frm_PlvSO.Cb_Moneda.Items[(Convert.ToInt16(initObj.Frm_PlvSO.Cb_Moneda.ListIndex))].Value;
            }
        }

        public static void Cb_Moneda_LostFocus(InitializationObject initObj)
        {

            if (initObj.Frm_PlvSO.Cb_Moneda.ListIndex == -1)
            {
                return;
            }
            if (initObj.Frm_PlvSO.Cb_Moneda.get_ItemId(Convert.ToInt16(initObj.Frm_PlvSO.Cb_Moneda.ListIndex)) == "1")
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "La Moneda No Puede Ser Pesos.",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODANUVI.MsgPlaSO
                });
                return;
            }
            //MODPREEM.Pr_Color_Objeto(Lb_Moneda, 0);
        }

        public static void Ch_Acuerdo_Click(InitializationObject initObj)
        {
            short lc_variable = (short)initObj.Frm_PlvSO.Ch_Acuerdo.Value;

            switch (lc_variable)
            {
                case 0:
                    initObj.Frm_PlvSO.Tx_CantAc.Text = "";
                    initObj.Frm_PlvSO.Tx_NumAc1.Text = "";
                    initObj.Frm_PlvSO.Tx_NumAc2.Text = "";
                    initObj.Frm_PlvSO.Fr_Acuerdos.Enabled = false;

                    break;
                case -1:
                    initObj.Frm_PlvSO.Fr_Acuerdos.Enabled = true;
                    break;
            }
        }

        public static void Ch_ClauRo_Click(InitializationObject initObj)
        {
            short lc_variable = (short)initObj.Frm_PlvSO.Ch_ClauRo.Value;

            if (lc_variable == 1)
            {
                initObj.Frm_PlvSO.Tx_NumDec.Text = "________________-_";
                initObj.Frm_PlvSO.Tx_FecDec.Text = "";
                initObj.Frm_PlvSO.Tx_CodPag.Text = "32";
                initObj.Frm_PlvSO.Tx_NumDec.Enabled = false;
                initObj.Frm_PlvSO.Tx_FecDec.Enabled = false;
            }
            else if (lc_variable == 0)
            {
                initObj.Frm_PlvSO.Tx_NumDec.Text = "________________-_";
                initObj.Frm_PlvSO.Tx_FecDec.Text = "";
                initObj.Frm_PlvSO.Tx_CodPag.Text = "";
                initObj.Frm_PlvSO.Tx_NumDec.Enabled = true;
                initObj.Frm_PlvSO.Tx_FecDec.Enabled = true;
            }
        }

        public static void Ch_ConvCre_Click(InitializationObject initObj)
        {
            short lc_variable = (short)initObj.Frm_PlvSO.Ch_ConvCre.Value;

            switch (lc_variable)
            {
                case 0:
                    initObj.Frm_PlvSO.Tx_FecDeb.Text = "";
                    initObj.Frm_PlvSO.Tx_DocChi.Text = "";
                    initObj.Frm_PlvSO.Tx_DocExt.Text = "";
                    initObj.Frm_PlvSO.Fr_ConvCre.Enabled = false;
                    break;
                case -1:
                    initObj.Frm_PlvSO.Fr_ConvCre.Enabled = true;
                    break;
            }
        }

        public static void Ch_CPagos_Click(InitializationObject initObj)
        {
            short lc_variable = (short)initObj.Frm_PlvSO.Ch_CPagos.Value;

            switch (lc_variable)
            {
                case 0:
                    initObj.Frm_PlvSO.Tx_NCpago.Text = "";
                    initObj.Frm_PlvSO.Tx_NCuota.Text = "";
                    initObj.Frm_PlvSO.Fr_Cpagos.Enabled = false;

                    break;
                case -1:
                    initObj.Frm_PlvSO.Fr_Cpagos.Enabled = true;
                    break;
            }
        }

        public static void Ch_Endoso_Click(InitializationObject initObj)
        {
            short lc_variable = (short)initObj.Frm_PlvSO.Ch_Endoso.Value;

            if (lc_variable == 1)
            {
                initObj.Frm_PlvSO.Bot_Clientes.Enabled = true;
            }
            else if (lc_variable == 0)
            {
                initObj.Frm_PlvSO.Bot_Clientes.Enabled = false;
            }
        }

        public static void Ch_PlanRee_Click(InitializationObject initObj)
        {
            short lc_variable = (short)initObj.Frm_PlvSO.Ch_PlanRee.Value;

            if (lc_variable == 1)
            {
                initObj.Frm_PlvSO.Tx_NroPre.Text = "";
                initObj.Frm_PlvSO.Tx_FecRee.Text = "";
                initObj.Frm_PlvSO.Tx_NroPre.Enabled = true;
                initObj.Frm_PlvSO.Tx_FecRee.Enabled = true;
            }
            else if (lc_variable == 0)
            {
                initObj.Frm_PlvSO.Tx_NroPre.Text = "";
                initObj.Frm_PlvSO.Tx_FecRee.Text = "";
                initObj.Frm_PlvSO.Tx_NroPre.Enabled = false;
                initObj.Frm_PlvSO.Tx_FecRee.Enabled = false;
            }
        }

        public static void Ch_Transf_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            UI_Frm_PlvSO Frm_PlvSO = initObj.Frm_PlvSO;
            short lc_variable = (short)initObj.Frm_PlvSO.Ch_Transf.Value;

            if (initObj.Frm_PlvSO.Bandera == 0)
            {
                if (!(lc_variable == 1))
                {
                    Pr_ParyTipCa(initObj, uow);
                    initObj.Frm_PlvSO.Tx_Observ.Text = initObj.Frm_PlvSO.guardaobs;
                }
                else
                {
                    initObj.Frm_PlvSO.Tx_TipCam.Text = VB6Helpers.Format("0", "0.0000");
                    initObj.Frm_PlvSO.Tx_Observ.Text = initObj.Frm_PlvSO.Tx_Observ.Text + " Planilla de transferencia.";
                    if (initObj.Frm_PlvSO.Cb_Moneda.ListIndex != -1)
                    {
                        initObj.Frm_PlvSO.Tx_Paridad.Text =
                            VB6Helpers.CStr(MODGTAB1.SyGet_Vmc(initObj.MODGTAB0, uow, Convert.ToInt16((initObj.Frm_PlvSO.Cb_Moneda.get_ItemId(Convert.ToInt16(initObj.Frm_PlvSO.Cb_Moneda.ListIndex)))), DateTime.Now.ToString("yyyy-MM-dd"), "P"));
                    }

                }

            }
            else
            {
                if (VB6Helpers.Instr(initObj.Frm_PlvSO.Tx_Observ.Text, "Planilla de transferencia") == 0)
                {
                    initObj.Frm_PlvSO.Tx_Observ.Text = initObj.Frm_PlvSO.guardaobs + " Planilla de transferencia";
                }

            }
            initObj.Frm_PlvSO.Bandera = 0;
        }

        public static void Lt_Final_DblClick(InitializationObject initObj)
        {
            UI_Frm_PlvSO Frm_PlvSO = initObj.Frm_PlvSO;
            short i = 0;
            if (initObj.Frm_PlvSO.Lt_Final.ListIndex == -1)
            {
                return;
            }
            short index = Convert.ToInt16(initObj.Frm_PlvSO.Lt_Final.ListIndex);
            if (Convert.ToInt16(initObj.Frm_PlvSO.Lt_Final.get_ItemId(Convert.ToInt16(index))) > -1)
            {
                i = Convert.ToInt16(initObj.Frm_PlvSO.Lt_Final.get_ItemData_(index));
                initObj.Frm_PlvSO.IndiceFin = i;
                Pr_MuesPlan(initObj, i);
            }
        }

        public static void Form_Load(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            // UPGRADE_INFO (#05B1): The 'Tabs' variable wasn't declared explicitly.
            short[] Tabs = null;
            short a;
            short p;
            //CenterForm FrmPlvSO

            //Lista Final.-
            Tabs = new short[3];
            Tabs[0] = 10;
            Tabs[1] = 45;
            UI_Frm_PlvSO Frm_PlvSO = initObj.Frm_PlvSO;
            a = MODGPYF0.seteatabulador(initObj.Frm_PlvSO.Lt_Final, Tabs);

            Pr_Inicializa(initObj, uow);

            //Llena Concepto,Tipo y Moneda.-
            Pr_LlenaCombo(initObj, uow);

            //Carga Plazas del Banco
            p = MODGTAB1.SyGetn_Pbc(initObj.MODGTAB1, uow);
            if (!initObj.Frm_PlvSO.Cb_Pbc.Items.Any(x => x.Data == 25))
            {
                initObj.Frm_PlvSO.Cb_Pbc.Items.Add(new UI_ComboItem()
                {
                    Value = "Santiago",
                    ID = "25",
                    Data = 25
                });

                //initObj.Frm_PlvSO.Cb_Pbc.set_ItemData(initObj.Frm_PlvSO.Cb_Pbc.NewIndex, 25);
                initObj.Frm_PlvSO.Cb_Pbc.ListIndex = 0;
                initObj.Frm_PlvSO.Cb_Pbc.Enabled = false;
            }

            initObj.Frm_PlvSO.Tx_Observ.Text = "Ope. " + initObj.MODGCVD.VgCvd.OpeSin;
            if (!string.IsNullOrEmpty(initObj.MODGASO.VgAso.OpeSin))
            {
                initObj.Frm_PlvSO.Tx_Observ.Text = initObj.Frm_PlvSO.Tx_Observ.Text + " Ope. Rel. " + initObj.MODGASO.VgAso.OpeSin;
            }

            initObj.Frm_PlvSO.guardaobs = initObj.Frm_PlvSO.Tx_Observ.Text;
            initObj.Frm_PlvSO.Bandera = 0;
        }

        private static void Pr_LlenaCombo(InitializationObject initObj, UnitOfWorkCext01 uow)
        {

            //Llena Combo Moneda
            //------------------------
            initObj.Frm_PlvSO.Cb_Moneda = new UI_Combo();
            MODGTAB0.CargaEnLista_Mnd(initObj.MODGTAB0, initObj.Frm_PlvSO.Cb_Moneda);

            //Llena el Combo Paises.-
            //------------------------
            MODGTAB0.CargaEnLista_Pai(initObj.MODGTAB0, initObj.Frm_PlvSO.Cb_PPago, uow);
        }

        private static void Pr_Inicializa(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            UI_Frm_PlvSO Frm_PlvSO = initObj.Frm_PlvSO;

            initObj.Frm_PlvSO.Ch_ClauRo.Value = 0;
            initObj.Frm_PlvSO.Ch_Endoso.Value = 0;
            initObj.Frm_PlvSO.Bot_Acepta.Enabled = false;

            //Limpia el frame de ingreso de Idi y Dec.-
            //-----------------------------------------
            Pr_LimIdiDecSO(initObj);

            //Limpiamos los campos.-
            //----------------------------
            Pr_LimOtros(initObj);
            Pr_LimParTCMon(initObj);

            //Deshabilita Frames, menos el de ingreso de informe
            //--------------------------------------------------
            Pr_HabilitaSO(initObj, 0);

            //Limpiamos variables y estructuras.-
            //------------------------------------
            //Vx_DatReem = Vx_DatReemNull
            initObj.MODPREEM.VxIdiDec = initObj.MODPREEM.VxIdiDecNull;
            initObj.MODANUVI.Vx_AnuReem = initObj.MODANUVI.Vx_AnuReemNull;

            //VB6Helpers.Erase(ref initObj.MODGFYS.RIdiIni);
            //VB6Helpers.Erase(ref initObj.MODCVDIMMM.RDecIni);
            initObj.MODGFYS.RIdiIni = new R_Idi[0];
            initObj.MODCVDIMMM.RDecIni = new r_dec[0];

            initObj.MODPREEM.Vx_PReem = new Plan_Reemp[0];
            initObj.MODGFYS.RIdiFin = new R_Idi[0];
            initObj.MODCVDIMMM.RDecFin = new r_dec[0];
            initObj.MODCVDIMMM.AnuCob = new ParaAnuCob[0];

            initObj.MODANUVI.Var_CodMon = 0;

            initObj.Frm_PlvSO.IndiceFin = -1;
            initObj.Frm_PlvSO.IndiceInt = 0;
            initObj.Frm_PlvSO.MtoInteres = 0;
            initObj.Frm_PlvSO.FlgYaMostro = (short)(false ? -1 : 0);
            initObj.Frm_PlvSO.IngPlan = (short)(false ? -1 : 0);
            initObj.MODPREEM.NuevaPlan = (short)(false ? -1 : 0);
        }

        private static void Pr_GetMoneda(InitializationObject initObj, UnitOfWorkCext01 uow)
        {

            short Pos_Cod = MODGTAB0.Get_VMnd(initObj.MODGTAB0, uow, Convert.ToInt16((initObj.Frm_PlvSO.Cb_Moneda.get_ItemId(Convert.ToInt16(initObj.Frm_PlvSO.Cb_Moneda.ListIndex)))));
            string FtoEnt = "";
            //Obtenemos la posicion para ese codigo.-
            //---------------------------------------

            //Asignamos el tag correspondiente segun la moneda.-
            //---------------------------------------
            if (initObj.MODGTAB0.VMnd[Pos_Cod].Mnd_MndSin != 0)
            {
                initObj.Frm_PlvSO.Tx_MtoFob.Tag = "_____________";
                initObj.Frm_PlvSO.Tx_MtoFle.Tag = "_____________";
                initObj.Frm_PlvSO.Tx_MtoSeg.Tag = "_____________";
                FtoEnt = "0";
                initObj.MODANUVI.FtoSal = T_MODGCON0.FormatoSinDec;
            }
            else
            {
                initObj.Frm_PlvSO.Tx_MtoFob.Tag = "_____________.__";
                initObj.Frm_PlvSO.Tx_MtoFle.Tag = "_____________.__";
                initObj.Frm_PlvSO.Tx_MtoSeg.Tag = "_____________.__";
                FtoEnt = "0.00";
                initObj.MODANUVI.FtoSal = T_MODGCON0.FormatoConDec;
            }

            //Desplegamos el codigo de la Moneda Banco Central.-
            //---------------------------------------
            if (Pos_Cod != 0)
            {
                initObj.Frm_PlvSO.Pn_CodMon.Text = VB6Helpers.CStr(initObj.MODGTAB0.VMnd[Pos_Cod].Mnd_MndCbc);
            }

            //Desplegamos la Moneda.-
            //---------------------------------------
            //Moneda$ = Get_NomMnd(Var_CodMon)
            //Pn_Moneda.Text = Moneda$
            initObj.MODPREEM.NemMoneda = MODGTAB0.Get_NemMnd(initObj.MODGTAB0, uow, Convert.ToInt16((initObj.Frm_PlvSO.Cb_Moneda.get_ItemId(Convert.ToInt16(initObj.Frm_PlvSO.Cb_Moneda.ListIndex)))));
            initObj.MODANUVI.Var_CodMon = Convert.ToInt16(initObj.Frm_PlvSO.Cb_Moneda.get_ItemId(Convert.ToInt16(initObj.Frm_PlvSO.Cb_Moneda.ListIndex)));
        }

        private static void Pr_ParyTipCa(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            double Paridad = 0;
            short x = 0;
            //Paridad.-
            //-----------------
            if (initObj.Frm_PlvSO.Cb_Moneda.ListIndex != -1)
            {
                string fecha = DateTime.Now.ToString("yyyy-MM-dd");
                //string fecha = "2015-03-31";//TODO JUAN, SE CLAVA LA FECHA PARA QUE RETORNE DATOS

                Paridad =
                    MODGTAB1.SyGet_Vmc(initObj.MODGTAB0, uow, Convert.ToInt16(initObj.Frm_PlvSO.Cb_Moneda.get_ItemId(Convert.ToInt16(initObj.Frm_PlvSO.Cb_Moneda.ListIndex))), fecha, "P");
                initObj.Frm_PlvSO.Tx_Paridad.Text = VB6Helpers.CStr(Paridad);
                if (Paridad == 0D)
                {
                    //Si no hay paridad, se habilita
                    initObj.Frm_PlvSO.Tx_Paridad.Enabled = true;  //el campo para su ingreso.-
                }

            }

            //Tipo de cambio.-
            //-----------------
            if (initObj.Frm_PlvSO.Cb_Moneda.ListIndex != -1)
            {
                string fecha = System.DateTime.Now.ToString("yyyy-MM-dd");
                //string fecha = "2015-03-31";//TODO JUAN
                x = MODGTAB0.SyGet_Vmd(initObj.MODGTAB0, uow, fecha, Convert.ToInt16(initObj.Frm_PlvSO.Cb_Moneda.get_ItemId(Convert.ToInt16(initObj.Frm_PlvSO.Cb_Moneda.ListIndex))));
                initObj.Frm_PlvSO.Tx_TipCam.Text = Format.FormatCurrency(initObj.MODGTAB0.VVmd.VmdMbv, "##0.000#");
                initObj.Frm_PlvSO.Lb_NemTC.Text = MODGTAB0.Get_NemMnd(initObj.MODGTAB0, uow, Convert.ToInt16(initObj.Frm_PlvSO.Cb_Moneda.get_ItemId((short)initObj.Frm_PlvSO.Cb_Moneda.ListIndex)));

                //Tipo de cambio equivalente en dolar.-
                //---------------------------------------------------
                initObj.Frm_PlvSO.Pn_TCDol.Text = Format.FormatCurrency(initObj.MODGTAB0.VVmd.VmdMbv * Paridad, "###,##0.0000");
            }

        }

        private static void Pr_HabilitaSO(InitializationObject initObj, short Valor)
        {

            initObj.Frm_PlvSO.Ch_ConvCre.Enabled = Valor != 0;
            initObj.Frm_PlvSO.Ch_Acuerdo.Enabled = Valor != 0;
            initObj.Frm_PlvSO.Ch_CPagos.Enabled = Valor != 0;
            initObj.Frm_PlvSO.Ch_PlanRee.Enabled = Valor != 0;
            initObj.Frm_PlvSO.Fr_Presen.Enabled = Valor != 0;
            initObj.Frm_PlvSO.Fr_Montos.Enabled = Valor != 0;

            initObj.Frm_PlvSO.Fr_Final.Enabled = Valor != 0;
            initObj.Frm_PlvSO.Fr_Montos.Enabled = Valor != 0;

            initObj.Frm_PlvSO.Tx_Observ.Enabled = Valor != 0;
        }

        //Limpia frame que contiene datos del Idi y la declaracion.-
        private static void Pr_LimIdiDecSO(InitializationObject initObj)
        {

            initObj.Frm_PlvSO.Tx_CodPag.Text = "";
            initObj.Frm_PlvSO.Tx_NumDec.Text = "________________-_";
            initObj.Frm_PlvSO.Tx_FecDec.Text = "";
        }

        private static void Pr_LimOtros(InitializationObject initObj)
        {
            UI_Frm_PlvSO Frm_PlvSO = initObj.Frm_PlvSO;
            //Limpia Textos
            //--------------
            initObj.Frm_PlvSO.Tx_NroCon.Text = "";
            initObj.Frm_PlvSO.Tx_FecCon.Text = "";
            initObj.Frm_PlvSO.Tx_FecVen.Text = "";
            initObj.Frm_PlvSO.Tx_NumAc1.Text = "";
            initObj.Frm_PlvSO.Tx_NumAc2.Text = "";
            initObj.Frm_PlvSO.Tx_FecDeb.Text = "";
            initObj.Frm_PlvSO.Tx_DocChi.Text = "";
            initObj.Frm_PlvSO.Tx_DocExt.Text = "";
            initObj.Frm_PlvSO.Tx_CantAc.Text = "";
            initObj.Frm_PlvSO.Tx_NumAc1.Text = "";
            initObj.Frm_PlvSO.Tx_NumAc2.Text = "";
            initObj.Frm_PlvSO.Tx_NCpago.Text = "";
            initObj.Frm_PlvSO.Tx_NCuota.Text = "";
            initObj.Frm_PlvSO.Tx_NroPre.Text = "";
            initObj.Frm_PlvSO.Tx_FecRee.Text = "";
            initObj.Frm_PlvSO.Tx_MtoFob.Text = "";
            initObj.Frm_PlvSO.Tx_MtoFle.Text = "";
            initObj.Frm_PlvSO.Tx_MtoSeg.Text = "";

            //Limpia las Check.-
            //------------------
            initObj.Frm_PlvSO.Ch_Acuerdo.Value = 0;
            initObj.Frm_PlvSO.Ch_ConvCre.Value = 0;
            initObj.Frm_PlvSO.Ch_CPagos.Value = 0;
            initObj.Frm_PlvSO.Ch_PlanRee.Value = 0;
            initObj.Frm_PlvSO.Bandera = 1;
            initObj.Frm_PlvSO.Ch_Transf.Value = 0;
            initObj.Frm_PlvSO.Ch_ZonFra.Value = 0;
            initObj.Frm_PlvSO.Tx_Observ.Text = "Ope. " + initObj.MODGCVD.VgCvd.OpeSin;
            if (!string.IsNullOrEmpty(initObj.MODGASO.VgAso.OpeSin))
            {
                initObj.Frm_PlvSO.Tx_Observ.Text = initObj.Frm_PlvSO.Tx_Observ.Text + " Ope. Rel. " + initObj.MODGASO.VgAso.OpeSin;
            }

            initObj.Frm_PlvSO.guardaobs = initObj.Frm_PlvSO.Tx_Observ.Text;

            //Limpiamos los paneles.-
            //------------------------
            //Pn_CodMon.Text = ""
            initObj.Frm_PlvSO.Pn_NroPre.Text = "";
            initObj.Frm_PlvSO.Lb_FecRee.Text = "";
            initObj.Frm_PlvSO.Pn_ValCif.Text = "";
            initObj.Frm_PlvSO.Pn_MtoTot.Text = "";
            initObj.Frm_PlvSO.Pn_CifDol.Text = "";
            initObj.Frm_PlvSO.Pn_TotDol.Text = "";

            //Combo
            //------------------------
            initObj.Frm_PlvSO.Cb_PPago.ListIndex = -1;

            //Boton
            initObj.Frm_PlvSO.Bot_Clientes.Enabled = false;

            initObj.Frm_PlvSO.Flag_TipCam = 0;
        }

        private static void Pr_LimParTCMon(InitializationObject initObj)
        {

            initObj.Frm_PlvSO.Tx_TipCam.Text = "";
            initObj.Frm_PlvSO.Tx_Paridad.Text = "";
            initObj.Frm_PlvSO.Pn_TCDol.Text = "";
            initObj.Frm_PlvSO.Lb_NemTC.Text = "";
            initObj.Frm_PlvSO.Pn_CodMon.Text = "";
            //Pn_Moneda.Text = ""
        }

        private static void Pr_ListaInt(int NumPlan)
        {
        }

        private static short Fn_ValIdiDecSO(InitializationObject initObj)
        {
            short _retValue = 0;

            _retValue = (short)(false ? -1 : 0);

            if (string.IsNullOrEmpty(initObj.Frm_PlvSO.Tx_CodPag.Text))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar el Código Forma de Pago.",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODANUVI.MsgPlaSO,
                    ControlName = "Tx_CodPag"
                });
                //initObj.Frm_PlvSO.Tx_CodPag.SetFocus();//TODO ARKANO
                return _retValue;
            }

            //Si la forma de pago es contado no se exige datos de informe,
            //conocimiento embarque, ni declaracion.-
            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'MODGPYF1.ValTexto(Tx_CodPag)'. Consider using the GetDefaultMember6 helper method.
            if (Format.StringToDouble((initObj.Frm_PlvSO.Tx_CodPag.Text)) == 32)
            {
                if (initObj.Frm_PlvSO.Cb_Moneda.ListIndex == -1)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "Debe Ingresar la Moneda.",
                        Type = TipoMensaje.Informacion,
                        Title = T_MODANUVI.MsgPlaSO,
                        ControlName = "idCbMoneda"
                    });

                    //initObj.Frm_PlvSO.Cb_Moneda.SetFocus();TODO ARKANO
                    return _retValue;
                }

                return (short)(true ? -1 : 0);
            }

            if (initObj.Frm_PlvSO.Cb_Moneda.ListIndex == -1)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar la Moneda.",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODANUVI.MsgPlaSO,
                    ControlName = "idCbMoneda"
                });
                //initObj.Frm_PlvSO.Cb_Moneda.SetFocus();TODO ARKANO
                return _retValue;
            }

            return (short)(true ? -1 : 0);
        }

        private static void Pr_CargaLisSO(InitializationObject initObj)
        {
            short largo_reem = 0;
            short i = 0;
            string ListaRee = "";


            largo_reem = (short)VB6Helpers.UBound(initObj.MODPREEM.Vx_PReem);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            //Cargamos en la lista numero de planilla y monto.-
            //-------------------------------------------------
            initObj.Frm_PlvSO.Lt_Final = new UI_Combo();
            for (i = 0; i <= (short)largo_reem; i++)
            {
                if (initObj.MODPREEM.Vx_PReem[i].Estado == 1)
                {
                    ListaRee = "";
                    ListaRee = ListaRee + initObj.MODPREEM.Vx_PReem[i].NumPla + "     ";
                    ListaRee = ListaRee +
                        initObj.MODPREEM.Vx_PReem[i].NemMon + " " + Format.FormatCurrency(initObj.MODPREEM.Vx_PReem[i].TotOri, initObj.MODANUVI.FtoSal);
                    initObj.Frm_PlvSO.Lt_Final.AddItem(i, ListaRee);
                }
            }
        }

        //Valida el Ingreso de todos los campos.
        private static short Fn_ValiCamposSO(InitializationObject initObj)
        {
            short _retValue = 0;
            int NroPre = 0;
            string FecRee = "";
            string s = "";
            _retValue = (short)(false ? -1 : 0);
            if (VB6Helpers.Val(initObj.Frm_PlvSO.Tx_CodPag.Text) == 11 || VB6Helpers.Val(initObj.Frm_PlvSO.Tx_CodPag.Text) == 12)
            {
                if (initObj.Frm_PlvSO.Cb_PPago.Text != "Chile")
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "El código de forma de pago seleccionado requiere que el país de pago sea Chile.",
                        Type = TipoMensaje.Informacion,
                        Title = T_MODANUVI.MsgPlaSO,
                        ControlName = "idCbPPago"
                    });
                    return _retValue;
                }

            }

            if (VB6Helpers.Val(initObj.Frm_PlvSO.Tx_CodPag.Text) == 1 || VB6Helpers.Val(initObj.Frm_PlvSO.Tx_CodPag.Text) == 2)
            {
                if (initObj.Frm_PlvSO.Cb_PPago.Text == "Chile")
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "El código de forma de pago seleccionado requiere que el país de pago sea Chile.",
                        Type = TipoMensaje.Informacion,
                        Title = T_MODANUVI.MsgPlaSO,
                        ControlName = "idCbPPago"
                    });
                    return _retValue;
                }

            }

            if (initObj.Frm_PlvSO.Cb_Pbc.ListIndex == -1)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar el Código de la Plaza.",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODANUVI.MsgPlaSO,
                    ControlName = "idCbPbc"
                });
                return _retValue;
            }

            if (initObj.Frm_PlvSO.Cb_PPago.ListIndex == -1)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar el Código del Pais.",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODANUVI.MsgPlaSO,
                    ControlName = "idCbPPago"
                });
                return _retValue;
            }

            if (VB6Helpers.Val(initObj.Frm_PlvSO.Tx_CodPag.Text) == 2)
            {
                if (string.IsNullOrEmpty(initObj.Frm_PlvSO.Tx_FecVen.Text))
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "Debe Ingresar la Fecha de Vencimiento de la Operación.",
                        Type = TipoMensaje.Informacion,
                        Title = T_MODANUVI.MsgPlaSO,
                        ControlName = "Tx_FecDec"
                    });
                    return _retValue;
                }

                if ((int)initObj.Frm_PlvSO.Ch_CPagos.Value == 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "La forma de pago utilizada requiere que se seleccione el campo Cuadro de Pagos.",
                        Type = TipoMensaje.Informacion,
                        Title = T_MODANUVI.MsgPlaSO,
                        ControlName = "Ch_CPagos"
                    });
                    return _retValue;
                }

                if (string.IsNullOrEmpty(initObj.Frm_PlvSO.Tx_NCpago.Text))
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "La forma de pago utilizada requiere que se ingrese el número de Cuadro de Pago.",
                        Type = TipoMensaje.Informacion,
                        Title = T_MODANUVI.MsgPlaSO,
                        ControlName = "Tx_NCpago"
                    });
                    return _retValue;
                }

                if (string.IsNullOrEmpty(initObj.Frm_PlvSO.Tx_NCuota.Text))
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "La forma de pago utilizada requiere que se ingrese el número de la cuota del Cuadro de Pago.",
                        Type = TipoMensaje.Informacion,
                        Title = T_MODANUVI.MsgPlaSO,
                        ControlName = "Tx_NCuota"
                    });
                    return _retValue;
                }

            }

            //Check de Acuerdo.-
            //------------------
            if ((int)initObj.Frm_PlvSO.Ch_Acuerdo.Value == -1 && Format.StringToDouble((initObj.Frm_PlvSO.Tx_CantAc.Text)) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Debe Ingresar la Cantidad de Acuerdos.",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODANUVI.MsgPlaSO,
                    ControlName = "Tx_CantAc"
                });
                return _retValue;
            }

            if ((int)initObj.Frm_PlvSO.Ch_Acuerdo.Value == -1 && Format.StringToDouble((initObj.Frm_PlvSO.Tx_NumAc1.Text)) == 0 && Format.StringToDouble((initObj.Frm_PlvSO.Tx_NumAc2.Text)) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Debe Ingresar el Número de Acuerdos.",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODANUVI.MsgPlaSO,
                    ControlName = "Tx_NumAc1"
                });
                return _retValue;
            }

            //Check de Convenio de Crédito.-
            //------------------------------
            if ((int)initObj.Frm_PlvSO.Ch_ConvCre.Value == -1 && Format.StringToDouble((initObj.Frm_PlvSO.Tx_FecDeb.Text)) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Debe Ingresar la Fecha de Débito para el Convenio.",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODANUVI.MsgPlaSO,
                    ControlName = "Tx_FecDeb"
                });
                return _retValue;
            }

            if ((int)initObj.Frm_PlvSO.Ch_ConvCre.Value == -1 && Format.StringToDouble((initObj.Frm_PlvSO.Tx_DocChi.Text)) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Debe Ingresar el Número de Documento en Chile para el Convenio. ",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODANUVI.MsgPlaSO,
                    ControlName = "Tx_DocChi"
                });
                return _retValue;
            }

            if ((int)initObj.Frm_PlvSO.Ch_ConvCre.Value == -1 && Format.StringToDouble((initObj.Frm_PlvSO.Tx_DocExt.Text)) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Debe Ingresar el Número de Documento en el Extranjero para el Convenio. ",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODANUVI.MsgPlaSO,
                    ControlName = "Tx_DocExt"
                });
                return _retValue;
            }

            //Chek de Pagos
            //-------------------------
            if ((int)initObj.Frm_PlvSO.Ch_CPagos.Value == -1 && Format.StringToDouble((initObj.Frm_PlvSO.Tx_NCpago.Text)) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Debe Ingresar Número de Cuadro de Pago.",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODANUVI.MsgPlaSO,
                    ControlName = "Tx_NCpago"
                });
                return _retValue;
            }

            if ((int)initObj.Frm_PlvSO.Ch_CPagos.Value == -1 && Format.StringToDouble((initObj.Frm_PlvSO.Tx_NCuota.Text)) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Debe Ingresar Número de Cuotas del Cuadro de Pago. ",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODANUVI.MsgPlaSO,
                    ControlName = "Tx_NCuota"
                });
                return _retValue;
            }

            //Validamos los datos de antecedentes de Planilla reemplazada.-
            //-------------------------------------------------------------
            if ((int)initObj.Frm_PlvSO.Ch_PlanRee.Value == -1)
            {
                if (Format.StringToDouble((initObj.Frm_PlvSO.Tx_NroPre.Text)) == 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = " Debe Ingresar el Número de Presentación de la Planilla Reemplazada.",
                        Type = TipoMensaje.Informacion,
                        Title = T_MODANUVI.MsgPlaSO,
                        ControlName = "Tx_NroPre"
                    });
                    return _retValue;
                }

                if (Format.StringToDouble((initObj.Frm_PlvSO.Tx_FecRee.Text)) == 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = " Debe Ingresar la Fecha de la Planilla Reemplazada.",
                        Type = TipoMensaje.Informacion,
                        Title = T_MODANUVI.MsgPlaSO,
                        ControlName = "Tx_FecRee"
                    });
                    return _retValue;
                }
            }

            NroPre = VB6Helpers.CInt(MODGPYF1.ValTexto(initObj.MODGPYF0, initObj.Frm_PlvSO.Tx_NroPre.Text));
            FecRee = VB6Helpers.Format(initObj.Frm_PlvSO.Tx_FecRee.Text, "yyyy-MM-dd");

            //Valores de Tipo Cambio y Paridad.-
            //-----------------------------------
            if ((int)initObj.Frm_PlvSO.Ch_Transf.Value == 0)
            {
                if (Format.StringToDouble((initObj.Frm_PlvSO.Tx_TipCam.Text)) == 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = " Debe Ingresar el Tipo de Cambio.",
                        Type = TipoMensaje.Informacion,
                        Title = T_MODANUVI.MsgPlaSO,
                        ControlName = "Tx_TipCam"
                    });
                    return _retValue;
                }

            }

            if (Format.StringToDouble((initObj.Frm_PlvSO.Tx_Paridad.Text)) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Debe Ingresar la Paridad.",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODANUVI.MsgPlaSO,
                    ControlName = "Tx_Paridad"
                });
                initObj.Frm_PlvSO.Tx_Paridad.Enabled = true;
                return _retValue;
            }

            //Validamos los montos ingresados con respecto al disponible
            //solo si no es Endoso.-
            //---------------------------------------------
            if ((int)initObj.Frm_PlvSO.Ch_Endoso.Value == -1)
            {
                if (string.IsNullOrEmpty(initObj.Frm_PlvSO.Tx_Observ.Text))
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = " Debe Ingresar las Observaciones.",
                        Type = TipoMensaje.Informacion,
                        Title = T_MODANUVI.MsgPlaSO,
                        ControlName = "Tx_Observ"
                    });
                    return _retValue;
                }

                s = initObj.Frm_PlvSO.Tx_Observ.Text;
                if (VB6Helpers.Instr(1, s, "@") != 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = " Debe Ingresar los datos de los parámetros de las Observaciones.",
                        Type = TipoMensaje.Informacion,
                        Title = T_MODANUVI.MsgPlaSO,
                        ControlName = "Tx_Observ"
                    });
                    return _retValue;
                }

            }

            if ((int)initObj.Frm_PlvSO.Ch_Endoso.Value == 0)
            {
                if ((int)initObj.Frm_PlvSO.Ch_ClauRo.Value == 0)
                {
                    if (Format.StringToDouble((initObj.Frm_PlvSO.Tx_CodPag.Text)) != 32)
                    {
                        if (Format.StringToDouble((initObj.Frm_PlvSO.Tx_MtoFob.Text)) > initObj.MODPREEM.VxIdiDec.DisFob_M)
                        {
                            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                            {
                                Text = "El Monto Ingresado Sobrepasa el FOB Disponible de la Declaración que es " + initObj.MODPREEM.NemMoneda + " " + Format.FormatCurrency((initObj.MODPREEM.VxIdiDec.DisFob_M), initObj.MODANUVI.FtoSal),
                                Type = TipoMensaje.Informacion,
                                Title = T_MODANUVI.MsgPlaSO,
                                ControlName = "Tx_MtoFob"
                            });
                            initObj.Frm_PlvSO.Tx_MtoFob.Text = "";
                            return _retValue;
                        }

                        initObj.Frm_PlvSO.Tx_MtoFle.Text = VB6Helpers.Format(initObj.Frm_PlvSO.Tx_MtoFle.Text, initObj.MODANUVI.FtoSal);
                        if (Format.StringToDouble((initObj.Frm_PlvSO.Tx_MtoFle.Text)) > initObj.MODPREEM.VxIdiDec.DisFle_M)
                        {
                            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                            {
                                Text = "El Monto Ingresado Sobrepasa el Flete Disponible de la Declaracion que es " + initObj.MODPREEM.NemMoneda + " " + Format.FormatCurrency((initObj.MODPREEM.VxIdiDec.DisFle_M), initObj.MODANUVI.FtoSal),
                                Type = TipoMensaje.Informacion,
                                Title = T_MODANUVI.MsgPlaSO,
                                ControlName = "Tx_MtoFle"
                            });
                            initObj.Frm_PlvSO.Tx_MtoFle.Text = "";
                            return _retValue;
                        }

                        initObj.Frm_PlvSO.Tx_MtoSeg.Text = VB6Helpers.Format(initObj.Frm_PlvSO.Tx_MtoSeg.Text, initObj.MODANUVI.FtoSal);
                        if (Format.StringToDouble((initObj.Frm_PlvSO.Tx_MtoSeg.Text)) > initObj.MODPREEM.VxIdiDec.DisSeg_M)
                        {
                            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                            {
                                Text = "El Monto Ingresado Sobrepasa el Seguro Disponible de la Declaración que es " + initObj.MODPREEM.NemMoneda + " " + Format.FormatCurrency((initObj.MODPREEM.VxIdiDec.DisSeg_M), initObj.MODANUVI.FtoSal),
                                Type = TipoMensaje.Informacion,
                                Title = T_MODANUVI.MsgPlaSO,
                                ControlName = "Tx_MtoSeg"
                            });
                            initObj.Frm_PlvSO.Tx_MtoSeg.Text = "";
                            return _retValue;
                        }
                    }
                }
                else
                {
                    if (Format.StringToDouble((initObj.Frm_PlvSO.Tx_CodPag.Text)) != 32)
                    {
                        initObj.Frm_PlvSO.Tx_MtoFle.Text = VB6Helpers.Format(initObj.Frm_PlvSO.Tx_MtoFle.Text, initObj.MODANUVI.FtoSal);
                        initObj.Frm_PlvSO.Tx_MtoSeg.Text = VB6Helpers.Format(initObj.Frm_PlvSO.Tx_MtoSeg.Text, initObj.MODANUVI.FtoSal);
                    }
                }
            }
            return -1;
        }
        /// <summary>
        /// Llenamos la estructura de Planilla .-
        /// </summary>
        /// <param name="initObj"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        private static short Fn_LlenaPlaSO(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            UI_Frm_PlvSO Frm_PlvSO = initObj.Frm_PlvSO;
            short largo_reem = 0;
            short Largo_Int = 0;
            double NroPres = 0;
            string x = "";
            string ru = "";
            string RutSin = "";
            short PosPbc = 0;
            short PosPais = 0;
            string IdRee = "";
            string observ = "";



            largo_reem = (short)VB6Helpers.UBound(initObj.MODPREEM.Vx_PReem);
            Largo_Int = (short)VB6Helpers.UBound(initObj.MODVPLE.IntPla);

            if (initObj.Frm_PlvSO.IndiceFin == -1)
            {
                //Ingreso Nuevo.-
                largo_reem = (short)(largo_reem + 1);
                VB6Helpers.RedimPreserve(ref initObj.MODPREEM.Vx_PReem, 0, largo_reem);
                initObj.MODPREEM.NuevaPlan = (short)(true ? -1 : 0);
            }
            else
            {
                largo_reem = initObj.Frm_PlvSO.IndiceFin;  //Modifico.-
                initObj.MODPREEM.NuevaPlan = (short)(false ? -1 : 0);
            }

            //Número Presentación.
            //--------------------
            if (Format.StringToDouble((initObj.Frm_PlvSO.Pn_NroPre.Text)) == 0)
            {
                NroPres = MODGRNG.LeeSceRng(initObj.MODGRNG, initObj.MODGUSR, initObj.Mdi_Principal, uow, T_MODGRNG.Rng_PlaVis);  //Nuevo
                if(NroPres == -1)
                {
                    return -1;
                }
            }
            else
            {
                NroPres = Format.StringToDouble((initObj.Frm_PlvSO.Pn_NroPre.Text));
            }

            initObj.MODPREEM.Vx_PReem[largo_reem].NumPla = (int)NroPres;
            //--------------------
            //Fecha de Venta
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].FecVen = DateTime.Now.ToString("yyyy-MM-dd");
            //Nombre Importador
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].NomImp = initObj.Frm_PlvSO.Pn_Import.Text;

            //Rut
            //----------------------
            x = initObj.Frm_PlvSO.Pn_RutImp.Text;
            //if (x != "")
            if (!string.IsNullOrEmpty(x))
            {
                ru = VB6Helpers.Trim(VB6Helpers.Mid(x, 1, VB6Helpers.Len(x) - 2)) + VB6Helpers.Right(x, 1);
            }

            RutSin = MODGPYF0.Componer(ru, ".", "");
            initObj.MODPREEM.Vx_PReem[largo_reem].RutImp = VB6Helpers.Right("000000000" + RutSin, 10);

            initObj.MODPREEM.Vx_PReem[largo_reem].CodPlz = 25;

            //Declaracion
            //--------------------
            if (string.IsNullOrWhiteSpace(initObj.Frm_PlvSO.Tx_NumDec.Text))
            {
                initObj.MODPREEM.Vx_PReem[largo_reem].numdec = "";
                initObj.MODPREEM.Vx_PReem[largo_reem].FecDec = "";
            }
            else
            {
                initObj.MODPREEM.Vx_PReem[largo_reem].numdec = initObj.Frm_PlvSO.Tx_NumDec.Text;
                initObj.MODPREEM.Vx_PReem[largo_reem].FecDec = VB6Helpers.Format(initObj.Frm_PlvSO.Tx_FecDec.Text, "yyyy-MM-dd");
            }

            //Conocimiento
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].NumCon = initObj.Frm_PlvSO.Tx_NroCon.Text;
            initObj.MODPREEM.Vx_PReem[largo_reem].FecCon = VB6Helpers.Format(initObj.Frm_PlvSO.Tx_FecCon.Text, "yyyy-MM-dd");
            //Codigo y Codigo Banco Chile
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].Codigo = "205000";
            //Vx_PReem(largo_reem%).CodBch = ValTexto(Tx_CodPlz)
            initObj.MODPREEM.Vx_PReem[largo_reem].CodBch = Convert.ToInt16(initObj.Frm_PlvSO.Cb_Pbc.get_ItemId(Convert.ToInt16(initObj.Frm_PlvSO.Cb_Pbc.ListIndex)));
            PosPbc = MODGTAB1.Get_VPbc(initObj, uow, Convert.ToInt16((initObj.Frm_PlvSO.Cb_Pbc.get_ItemId(Convert.ToInt16(initObj.Frm_PlvSO.Cb_Pbc.ListIndex)))));
            initObj.MODPREEM.Vx_PReem[largo_reem].NomPlz = string.Empty;
            if (PosPbc >= 0)
                initObj.MODPREEM.Vx_PReem[largo_reem].NomPlz = initObj.MODGTAB1.VPbc[PosPbc].Pbc_PbcDes;



            //Forma Pago
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].CodPag = VB6Helpers.CShort(MODGPYF1.ValTexto(initObj.MODGPYF0, initObj.Frm_PlvSO.Tx_CodPag.Text));
            //Pais de Pago
            initObj.MODPREEM.Vx_PReem[largo_reem].CodPPa = Convert.ToInt16(initObj.Frm_PlvSO.Cb_PPago.get_ItemId(Convert.ToInt16(initObj.Frm_PlvSO.Cb_PPago.ListIndex)));
            PosPais = MODGTAB0.Get_VPai(initObj, uow, Convert.ToInt16((initObj.Frm_PlvSO.Cb_PPago.get_ItemId(Convert.ToInt16(initObj.Frm_PlvSO.Cb_PPago.ListIndex)))));
            initObj.MODPREEM.Vx_PReem[largo_reem].NomPai = initObj.MODGTAB0.VPai[PosPais].Pai_PaiNom;

            //Moneda
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].CodMPa = VB6Helpers.CShort(MODGPYF1.ValTexto(initObj.MODGPYF0, initObj.Frm_PlvSO.Pn_CodMon.Text));  //Moneda Bco.Central
            initObj.MODPREEM.Vx_PReem[largo_reem].CodMon = initObj.MODANUVI.Var_CodMon;  //Moneda BCh.
            initObj.MODPREEM.Vx_PReem[largo_reem].NomMon = MODGTAB0.Get_NomMnd(initObj, initObj.MODANUVI.Var_CodMon);
            initObj.MODPREEM.Vx_PReem[largo_reem].NemMon = initObj.MODPREEM.NemMoneda;
            //Paridad y Tipo de Cambio
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].ParPag = Format.StringToDouble((initObj.Frm_PlvSO.Tx_Paridad.Text));
            initObj.MODPREEM.Vx_PReem[largo_reem].TipCamo = Format.StringToDouble((initObj.Frm_PlvSO.Tx_TipCam.Text));  //Tipo de cambio original
            initObj.MODPREEM.Vx_PReem[largo_reem].TipCam = Format.StringToDouble((initObj.Frm_PlvSO.Pn_TCDol.Text));  //Tip.Cambio equivalente.-
            //Montos
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].MtoFob = Format.StringToDouble((initObj.Frm_PlvSO.Tx_MtoFob.Text));
            initObj.MODPREEM.Vx_PReem[largo_reem].MtoFle = Format.StringToDouble((initObj.Frm_PlvSO.Tx_MtoFle.Text));
            initObj.MODPREEM.Vx_PReem[largo_reem].MtoSeg = Format.StringToDouble((initObj.Frm_PlvSO.Tx_MtoSeg.Text));
            initObj.MODPREEM.Vx_PReem[largo_reem].MtoCif = Format.StringToDouble((initObj.Frm_PlvSO.Pn_ValCif.Text));
            initObj.MODPREEM.Vx_PReem[largo_reem].TotOri = Format.StringToDouble((initObj.Frm_PlvSO.Pn_MtoTot.Text));
            initObj.MODPREEM.Vx_PReem[largo_reem].CifDol = Format.StringToDouble((initObj.Frm_PlvSO.Pn_CifDol.Text));
            initObj.MODPREEM.Vx_PReem[largo_reem].TotDol = Format.StringToDouble((initObj.Frm_PlvSO.Pn_TotDol.Text));
            //Fecha de Vencimiento
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].FecVto = VB6Helpers.Format(initObj.Frm_PlvSO.Tx_FecVen.Text, "yyyy-MM-dd");
            //Cuadro de Pagos.-
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].HayCua = (short)initObj.Frm_PlvSO.Ch_CPagos.Value;
            initObj.MODPREEM.Vx_PReem[largo_reem].NumCua = VB6Helpers.CInt(MODGPYF1.ValTexto(initObj.MODGPYF0, initObj.Frm_PlvSO.Tx_NCpago.Text));
            initObj.MODPREEM.Vx_PReem[largo_reem].numcuo = VB6Helpers.CShort(MODGPYF1.ValTexto(initObj.MODGPYF0, initObj.Frm_PlvSO.Tx_NCuota.Text));
            //Acuerdo
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].HayAcu = (short)initObj.Frm_PlvSO.Ch_Acuerdo.Value;
            initObj.MODPREEM.Vx_PReem[largo_reem].NumAcu = VB6Helpers.CShort(MODGPYF1.ValTexto(initObj.MODGPYF0, initObj.Frm_PlvSO.Tx_CantAc.Text));
            initObj.MODPREEM.Vx_PReem[largo_reem].Acuer1 = initObj.Frm_PlvSO.Tx_NumAc1.Text;
            initObj.MODPREEM.Vx_PReem[largo_reem].Acuer2 = initObj.Frm_PlvSO.Tx_NumAc2.Text;
            //Datos de Anulacion
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].HayAnu = 0;
            initObj.MODPREEM.Vx_PReem[largo_reem].VenAnu = "";
            initObj.MODPREEM.Vx_PReem[largo_reem].NumReg = 0;
            initObj.MODPREEM.Vx_PReem[largo_reem].ParAnu = 0;
            initObj.MODPREEM.Vx_PReem[largo_reem].TotAnu = 0;
            initObj.MODPREEM.Vx_PReem[largo_reem].FecAnu = "";
            //Planilla Reemplazada.-
            //--------------------
            if ((int)initObj.Frm_PlvSO.Ch_PlanRee.Value == -1)
            {
                initObj.MODPREEM.Vx_PReem[largo_reem].HayRpl = (short)(true ? -1 : 0);
                initObj.MODPREEM.Vx_PReem[largo_reem].IndAnu = 2;
                initObj.MODPREEM.Vx_PReem[largo_reem].NumPln_R = VB6Helpers.CInt(MODGPYF1.ValTexto(initObj.MODGPYF0, initObj.Frm_PlvSO.Tx_NroPre.Text));
                initObj.MODPREEM.Vx_PReem[largo_reem].FecPln_R = VB6Helpers.Format(initObj.Frm_PlvSO.Tx_FecRee.Text, "yyyy-MM-dd");
                initObj.MODPREEM.Vx_PReem[largo_reem].CodPlz_R = initObj.MODPREEM.Vx_PReem[largo_reem].CodPlz;
                MODPREEM.Pr_DatosPl_R(initObj, initObj.MODPREEM.Vx_PReem[largo_reem].NumPln_R, initObj.MODPREEM.Vx_PReem[largo_reem].FecPln_R, largo_reem);
            }
            else
            {
                initObj.MODPREEM.Vx_PReem[largo_reem].HayRpl = (short)(false ? -1 : 0);
                initObj.MODPREEM.Vx_PReem[largo_reem].IndAnu = 0;
            }

            //Convenio Credito
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].FecDeb = VB6Helpers.Format(initObj.Frm_PlvSO.Tx_FecDeb.Text, "yyyy-MM-dd");
            initObj.MODPREEM.Vx_PReem[largo_reem].DocChi = initObj.Frm_PlvSO.Tx_DocChi.Text;
            initObj.MODPREEM.Vx_PReem[largo_reem].DocExt = initObj.Frm_PlvSO.Tx_DocExt.Text;

            //Observaciones
            //--------------------
            IdRee = initObj.Module1.Codop.Cent_Costo + "-" + initObj.Module1.Codop.Id_Product + "-" + initObj.Module1.Codop.Id_Especia + "-" + initObj.Module1.Codop.Id_Empresa + "-" + initObj.Module1.Codop.Id_Operacion;
            observ = "OPERACION  " + IdRee + "  ESP  " + initObj.Module1.Codop.Id_Especia;
            if (VB6Helpers.Trim(initObj.Module1.PartysOpe[T_MODGCVD.ICli].rut) != VB6Helpers.Trim(initObj.MODPREEM.Vx_PReem[largo_reem].RutImp))
            {
                observ = observ + " PARTY " + MODGFYS.Rut_Formateado(initObj.MODPREEM.Vx_PReem[largo_reem].RutImp);
            }

            initObj.MODPREEM.Vx_PReem[largo_reem].observ = initObj.Frm_PlvSO.Tx_Observ.Text;
            initObj.MODPREEM.Vx_PReem[largo_reem].ObsDec = " D.I: " + " " + initObj.MODPREEM.Vx_PReem[largo_reem].numdec;
            initObj.MODPREEM.Vx_PReem[largo_reem].ObsMer = "";
            initObj.MODPREEM.Vx_PReem[largo_reem].ObsPar = "";
            initObj.MODPREEM.Vx_PReem[largo_reem].ObsCob = observ;
            initObj.MODPREEM.Vx_PReem[largo_reem].Estado = 1;

            //Clausula Roja.-
            //--------------------------------------------------
            if ((int)initObj.Frm_PlvSO.Ch_ClauRo.Value == -1)
            {
                initObj.MODPREEM.Vx_PReem[largo_reem].ClauRo = 1;
            }
            else
            {
                initObj.MODPREEM.Vx_PReem[largo_reem].ClauRo = 0;
            }

            //Transferencia.-
            //--------------------------------------------------
            if ((int)initObj.Frm_PlvSO.Ch_Transf.Value == 1)
            {
                initObj.MODPREEM.Vx_PReem[largo_reem].IndAnu = 3;
            }

            if ((int)initObj.Frm_PlvSO.Ch_ZonFra.Value == -1)
            {
                initObj.MODPREEM.Vx_PReem[largo_reem].ZonFra = (short)(true ? -1 : 0);
            }
            else
            {
                initObj.MODPREEM.Vx_PReem[largo_reem].ZonFra = (short)(false ? -1 : 0);
            }

            //Cambiamos el numero de planilla de los intereses.-
            //--------------------------------------------------
            //For i% = 1 To Largo_Int%
            //    If Format(IntPla(i%).NroPln, "000000") = 999999 Then
            //        IntPla(i%).NroPln = Vx_PReem(largo_reem%).NumPla
            //    End If
            //Next i%

            initObj.Frm_PlvSO.IndiceFin = -1;

            return largo_reem;
        }

        //Despliega los valores de las planillas en la pantalla.-
        public static void Pr_MuesPlan(InitializationObject initObj, short Indice)
        {
            var MODPREEM = initObj.MODPREEM;
            //Numero de Planilla
            //------------------------
            initObj.Frm_PlvSO.Pn_NroPre.Text = VB6Helpers.Format(VB6Helpers.CStr(MODPREEM.Vx_PReem[Indice].NumPla), "000000");
            //Importador
            //------------------------
            initObj.Frm_PlvSO.Pn_Import.Text = MODGPYF1.Minuscula(MODPREEM.Vx_PReem[Indice].NomImp);
            initObj.Frm_PlvSO.Pn_RutImp.Text = MODGFYS.Rut_Formateado(MODPREEM.Vx_PReem[Indice].RutImp);
            //Declaracion
            //------------------------
            //Clausula Roja.-
            if (initObj.MODPREEM.Vx_PReem[Indice].ClauRo == 1)
            {
                //Si es Clausula Roja
                initObj.Frm_PlvSO.Ch_ClauRo.Value = -1;
            }
            else
            {
                initObj.Frm_PlvSO.Ch_ClauRo.Value = 0;
                if (!string.IsNullOrWhiteSpace(MODPREEM.Vx_PReem[Indice].numdec))
                {
                    initObj.Frm_PlvSO.Tx_NumDec.Text = MODPREEM.Vx_PReem[Indice].numdec;
                }

                initObj.Frm_PlvSO.Tx_FecDec.Text = VB6Helpers.Format(MODPREEM.Vx_PReem[Indice].FecDec, "yyyy-MM-dd");
            }

            if (MODPREEM.Vx_PReem[Indice].IndAnu == 3)
            {
                initObj.Frm_PlvSO.Ch_Transf.Value = 1;
            }

            if (MODPREEM.Vx_PReem[Indice].ZonFra == -1)
            {
                initObj.Frm_PlvSO.Ch_ZonFra.Value = -1;
            }
            else
            {
                initObj.Frm_PlvSO.Ch_ZonFra.Value = 0;
            }

            //Conocimiento
            //------------------------
            initObj.Frm_PlvSO.Tx_NroCon.Text = MODPREEM.Vx_PReem[Indice].NumCon;
            initObj.Frm_PlvSO.Tx_FecCon.Text = VB6Helpers.Format(MODPREEM.Vx_PReem[Indice].FecCon, "yyyy-MM-dd");

            //Plaza Banco Central
            //------------------------
            //Tx_CodPlz.Text = Vx_PReem(indice).CodPlz '????
            initObj.Frm_PlvSO.Cb_Pbc.ListIndex = MODGPYF0.PosLista(initObj.Frm_PlvSO.Cb_Pbc, MODPREEM.Vx_PReem[Indice].CodBch);
            //Pn_NomPlz.Text = Vx_PReem(indice).NomPlz

            //Forma de Pago
            //------------------------
            initObj.Frm_PlvSO.Tx_CodPag.Text = VB6Helpers.Format(VB6Helpers.CStr(MODPREEM.Vx_PReem[Indice].CodPag), "00");

            //Pais de pago
            //------------------------
            initObj.Frm_PlvSO.Cb_PPago.ListIndex = MODGPYF0.PosLista(initObj.Frm_PlvSO.Cb_PPago, MODPREEM.Vx_PReem[Indice].CodPPa);

            //Moneda
            //------------------------
            initObj.Frm_PlvSO.Pn_CodMon.Text = VB6Helpers.CStr(MODPREEM.Vx_PReem[Indice].CodMPa);  //Moneda Bco.Central
            initObj.Frm_PlvSO.Cb_Moneda.Enabled = true;
            initObj.Frm_PlvSO.Cb_Moneda.ListIndex = MODGPYF0.PosLista(initObj.Frm_PlvSO.Cb_Moneda, MODPREEM.Vx_PReem[Indice].CodMon);
            initObj.Frm_PlvSO.Cb_Moneda.Enabled = false;
            //Paridad y tipo de cambio
            //------------------------
            initObj.Frm_PlvSO.Tx_Paridad.Text = VB6Helpers.CStr(MODPREEM.Vx_PReem[Indice].ParPag);
            initObj.Frm_PlvSO.Tx_TipCam.Text = VB6Helpers.CStr(MODPREEM.Vx_PReem[Indice].TipCamo);
            //Montos
            //------------------------
            initObj.Frm_PlvSO.Tx_MtoFob.Text = Format.FormatCurrency((MODPREEM.Vx_PReem[Indice].MtoFob), initObj.MODANUVI.FtoSal);
            initObj.Frm_PlvSO.Tx_MtoFle.Text = Format.FormatCurrency((MODPREEM.Vx_PReem[Indice].MtoFle), initObj.MODANUVI.FtoSal);
            initObj.Frm_PlvSO.Tx_MtoSeg.Text = Format.FormatCurrency((MODPREEM.Vx_PReem[Indice].MtoSeg), initObj.MODANUVI.FtoSal);
            initObj.Frm_PlvSO.Pn_ValCif.Text = Format.FormatCurrency((MODPREEM.Vx_PReem[Indice].MtoCif), initObj.MODANUVI.FtoSal);
            initObj.Frm_PlvSO.Pn_MtoTot.Text = Format.FormatCurrency((MODPREEM.Vx_PReem[Indice].TotOri), initObj.MODANUVI.FtoSal);
            initObj.Frm_PlvSO.Pn_CifDol.Text = Format.FormatCurrency((MODPREEM.Vx_PReem[Indice].CifDol), initObj.MODANUVI.FtoSal);
            initObj.Frm_PlvSO.Pn_TotDol.Text = Format.FormatCurrency((MODPREEM.Vx_PReem[Indice].TotDol), initObj.MODANUVI.FtoSal);
            //Fecha de Vencimiento
            //------------------------
            initObj.Frm_PlvSO.Tx_FecVen.Text = VB6Helpers.Format(MODPREEM.Vx_PReem[Indice].FecVto, "yyyy-MM-dd");
            //Convenio de Credito
            //------------------------
            if (!string.IsNullOrEmpty(MODPREEM.Vx_PReem[Indice].FecDeb) && !string.IsNullOrEmpty(MODPREEM.Vx_PReem[Indice].DocChi) && !string.IsNullOrEmpty(MODPREEM.Vx_PReem[Indice].DocExt))
            {
                initObj.Frm_PlvSO.Ch_ConvCre.Value = -1;
                initObj.Frm_PlvSO.Tx_FecDeb.Text = VB6Helpers.Format(MODPREEM.Vx_PReem[Indice].FecDeb, "yyyy-MM-dd");
                initObj.Frm_PlvSO.Tx_DocChi.Text = MODPREEM.Vx_PReem[Indice].DocChi;
                initObj.Frm_PlvSO.Tx_DocExt.Text = MODPREEM.Vx_PReem[Indice].DocExt;
            }
            else
            {
                initObj.Frm_PlvSO.Ch_ConvCre.Value = 0;
                initObj.Frm_PlvSO.Tx_FecDeb.Text = "";
                initObj.Frm_PlvSO.Tx_DocChi.Text = "";
                initObj.Frm_PlvSO.Tx_DocExt.Text = "";
            }
            //Cuadro de Pago.-
            //------------------------
            if (MODPREEM.Vx_PReem[Indice].HayCua == -1)
            {
                initObj.Frm_PlvSO.Ch_CPagos.Value = MODPREEM.Vx_PReem[Indice].HayCua;
                initObj.Frm_PlvSO.Tx_NCpago.Text = VB6Helpers.CStr(MODPREEM.Vx_PReem[Indice].NumCua);
                initObj.Frm_PlvSO.Tx_NCuota.Text = VB6Helpers.CStr(MODPREEM.Vx_PReem[Indice].numcuo);
            }
            else
            {
                initObj.Frm_PlvSO.Ch_CPagos.Value = 0;
                initObj.Frm_PlvSO.Tx_NCpago.Text = "";
                initObj.Frm_PlvSO.Tx_NCuota.Text = "";
            }
            //Acuerdo.-
            //------------------------
            if (MODPREEM.Vx_PReem[Indice].HayAcu == -1)
            {
                initObj.Frm_PlvSO.Ch_Acuerdo.Value = MODPREEM.Vx_PReem[Indice].HayAcu;
                initObj.Frm_PlvSO.Tx_CantAc.Text = VB6Helpers.CStr(MODPREEM.Vx_PReem[Indice].NumAcu);
                initObj.Frm_PlvSO.Tx_NumAc1.Text = MODPREEM.Vx_PReem[Indice].Acuer1;
                initObj.Frm_PlvSO.Tx_NumAc2.Text = MODPREEM.Vx_PReem[Indice].Acuer2;
            }
            else
            {
                initObj.Frm_PlvSO.Ch_Acuerdo.Value = 0;
                initObj.Frm_PlvSO.Tx_CantAc.Text = "";
                initObj.Frm_PlvSO.Tx_NumAc1.Text = "";
                initObj.Frm_PlvSO.Tx_NumAc2.Text = "";
            }
            //Planilla Reemplazada.-
            //------------------------
            initObj.Frm_PlvSO.Tx_NroPre.Text = VB6Helpers.Format(VB6Helpers.CStr(MODPREEM.Vx_PReem[Indice].NumPln_R), "000000");
            initObj.Frm_PlvSO.Tx_FecRee.Text = VB6Helpers.Format(MODPREEM.Vx_PReem[Indice].FecPln_R, "yyyy-MM-dd");
            if (!string.IsNullOrEmpty(MODPREEM.Vx_PReem[Indice].FecDeb) && !string.IsNullOrEmpty(MODPREEM.Vx_PReem[Indice].DocChi) && !string.IsNullOrEmpty(MODPREEM.Vx_PReem[Indice].DocExt))
            {
                initObj.Frm_PlvSO.Ch_ConvCre.Value = -1;
                initObj.Frm_PlvSO.Tx_FecDeb.Text = VB6Helpers.Format(MODPREEM.Vx_PReem[Indice].FecDeb, "yyyy-MM-dd");
                initObj.Frm_PlvSO.Tx_DocChi.Text = MODPREEM.Vx_PReem[Indice].DocChi;
                initObj.Frm_PlvSO.Tx_DocExt.Text = MODPREEM.Vx_PReem[Indice].DocExt;
            }
            //Observaciones
            //------------------------
            initObj.Frm_PlvSO.Tx_Observ.Text = MODPREEM.Vx_PReem[Indice].observ;
            //Intereses.-
            //------------------------
            Pr_ListaInt(initObj.MODPREEM.Vx_PReem[Indice].NumPla);
        }
        public static void Tx_MtoFob_LostFocus(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            UI_Frm_PlvSO Frm_PlvSO = initObj.Frm_PlvSO;

            //Si no es Endoso
            if ((int)initObj.Frm_PlvSO.Ch_Endoso.Value == 0)
            {
                if (!string.IsNullOrEmpty(initObj.Frm_PlvSO.numdec))
                {
                    if (MODPREEM.SyGet_IdiDec(initObj, uow, initObj.Frm_PlvSO.numdec, initObj.Frm_PlvSO.FecDec, initObj.Frm_PlvSO.CodPag, initObj.MODGCVD.VgCvd.PrtCli) != 0)
                    {
                        if (initObj.MODPREEM.VxIdiDec.flag != 1 && initObj.MODPREEM.VxIdiDec.flag != 2)
                        {
                            if (Format.StringToDouble((initObj.Frm_PlvSO.Tx_MtoFob.Text)) > initObj.MODPREEM.VxIdiDec.DisFob_M)
                            {
                                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = "El Monto Ingresado Sobrepasa el FOB Disponible de la Declaración que es " + initObj.MODPREEM.NemMoneda + " " + Format.FormatCurrency((initObj.MODPREEM.VxIdiDec.DisFob_M), initObj.MODANUVI.FtoSal),
                                    Title = T_MODANUVI.MsgPlaSO
                                });
                                initObj.Frm_PlvSO.Tx_MtoFob.Text = "";
                                return;
                            }
                        }
                    }
                }
            }
            //$Monto FOB:
            initObj.Frm_PlvSO.Tx_MtoFob.Text = Format.FormatCurrency(Convert.ToDouble(initObj.Frm_PlvSO.Tx_MtoFob.Text), initObj.MODANUVI.FtoSal);

            //$ Valor CIF = $Monto FOB + $Monto Flete + $Monto Seguro:
            initObj.Frm_PlvSO.Pn_ValCif.Text =
                Format.FormatCurrency(Format.StringToDouble(initObj.Frm_PlvSO.Tx_MtoFob.Text) + Format.StringToDouble(initObj.Frm_PlvSO.Tx_MtoFle.Text) + Format.StringToDouble(initObj.Frm_PlvSO.Tx_MtoSeg.Text),
                initObj.MODANUVI.FtoSal);

            //$Monto Total:
            initObj.Frm_PlvSO.Pn_MtoTot.Text = Format.FormatCurrency(Convert.ToDouble(initObj.Frm_PlvSO.Pn_ValCif.Text), T_MODGCON0.FormatoConDec).Replace(".", "");

            //Si paridad es distinto a cero. 
            if (Format.StringToDouble((initObj.Frm_PlvSO.Tx_Paridad.Text)) != 0)
            {
                //Valor CIF en US$:
                initObj.Frm_PlvSO.Pn_CifDol.Text = Format.FormatCurrency((Format.StringToDouble((initObj.Frm_PlvSO.Pn_ValCif.Text)) / Format.StringToDouble((initObj.Frm_PlvSO.Tx_Paridad.Text))), T_MODGCON0.FormatoConDec);
                //Valor Total en US$:
                initObj.Frm_PlvSO.Pn_TotDol.Text = Format.FormatCurrency((Format.StringToDouble((initObj.Frm_PlvSO.Pn_MtoTot.Text)) / Format.StringToDouble((initObj.Frm_PlvSO.Tx_Paridad.Text))), T_MODGCON0.FormatoConDec);
            }
        }
        public static void Tx_MtoFle_LostFocus(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            UI_Frm_PlvSO Frm_PlvSO = initObj.Frm_PlvSO;
            //$ Monto Flete:
            initObj.Frm_PlvSO.Tx_MtoFle.Text = Format.FormatCurrency(Convert.ToDouble(initObj.Frm_PlvSO.Tx_MtoFle.Text), initObj.MODANUVI.FtoSal);

            if ((int)initObj.Frm_PlvSO.Ch_Endoso.Value == 0)
            {
                //Si NO es Clausula Roja y no es Endoso
                if ((int)initObj.Frm_PlvSO.Ch_ClauRo.Value == 0)
                {
                    if (initObj.Frm_PlvSO.NumIdi != 0 && !string.IsNullOrEmpty(initObj.Frm_PlvSO.numdec))
                    {
                        if (MODPREEM.SyGet_IdiDec(initObj, uow, initObj.Frm_PlvSO.numdec, initObj.Frm_PlvSO.FecDec, initObj.Frm_PlvSO.CodPag, initObj.MODGCVD.VgCvd.PrtCli) != 0)
                        {
                            if (initObj.MODPREEM.VxIdiDec.flag != 1 && initObj.MODPREEM.VxIdiDec.flag != 2)
                            {
                                if (Format.StringToDouble((initObj.Frm_PlvSO.Tx_MtoFle.Text)) > initObj.MODPREEM.VxIdiDec.DisFle_M)
                                {
                                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Informacion,
                                        Text = "El Monto Ingresado Sobrepasa el Flete Disponible de la Declaración que es " + initObj.MODPREEM.NemMoneda + " " + Format.FormatCurrency((initObj.MODPREEM.VxIdiDec.DisFle_M), initObj.MODANUVI.FtoSal),
                                        Title = T_MODANUVI.MsgPlaSO
                                    });
                                    initObj.Frm_PlvSO.Tx_MtoFle.Text = "";
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            //$ Valor CIF = $Monto FOB + $Monto Flete + $Monto Seguro:
            initObj.Frm_PlvSO.Pn_ValCif.Text =
                Format.FormatCurrency(Format.StringToDouble(initObj.Frm_PlvSO.Tx_MtoFob.Text) + Format.StringToDouble(initObj.Frm_PlvSO.Tx_MtoFle.Text) + Format.StringToDouble(initObj.Frm_PlvSO.Tx_MtoSeg.Text),
                initObj.MODANUVI.FtoSal);

            //$Monto Total:            
            initObj.Frm_PlvSO.Pn_MtoTot.Text = Format.FormatCurrency(Convert.ToDouble(initObj.Frm_PlvSO.Pn_ValCif.Text), T_MODGCON0.FormatoConDec).Replace(".", "");

            //Si paridad es distinto a cero. 
            if (Format.StringToDouble((initObj.Frm_PlvSO.Tx_Paridad.Text)) != 0)
            {
                //Valor CIF en US$:
                initObj.Frm_PlvSO.Pn_CifDol.Text = Format.FormatCurrency((Format.StringToDouble((initObj.Frm_PlvSO.Pn_ValCif.Text)) / Format.StringToDouble((initObj.Frm_PlvSO.Tx_Paridad.Text))), T_MODGCON0.FormatoConDec);
                //Valor Total en US$:
                initObj.Frm_PlvSO.Pn_TotDol.Text = Format.FormatCurrency((Format.StringToDouble((initObj.Frm_PlvSO.Pn_MtoTot.Text)) / Format.StringToDouble((initObj.Frm_PlvSO.Tx_Paridad.Text))), T_MODGCON0.FormatoConDec);
            }
        }
        public static void Tx_MtoSeg_LostFocus(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            UI_Frm_PlvSO Frm_PlvSO = initObj.Frm_PlvSO;
            //$ Monto Seguro:
            initObj.Frm_PlvSO.Tx_MtoSeg.Text = Format.FormatCurrency(Format.StringToDouble(initObj.Frm_PlvSO.Tx_MtoSeg.Text), initObj.MODANUVI.FtoSal);

            if ((int)initObj.Frm_PlvSO.Ch_Endoso.Value == 0)
            {
                //Si NO es Clausula Roja o no es endoso.-
                if ((int)initObj.Frm_PlvSO.Ch_ClauRo.Value == 0)
                {
                    if (initObj.Frm_PlvSO.NumIdi != 0 && !string.IsNullOrEmpty(initObj.Frm_PlvSO.numdec))
                    {
                        if (MODPREEM.SyGet_IdiDec(initObj, uow, initObj.Frm_PlvSO.numdec, initObj.Frm_PlvSO.FecDec, initObj.Frm_PlvSO.CodPag, initObj.MODGCVD.VgCvd.PrtCli) != 0)
                        {
                            if (initObj.MODPREEM.VxIdiDec.flag != 1 && initObj.MODPREEM.VxIdiDec.flag != 2)
                            {
                                if (Format.StringToDouble((initObj.Frm_PlvSO.Tx_MtoSeg.Text)) > initObj.MODPREEM.VxIdiDec.DisSeg_M)
                                {
                                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                                    {
                                        Type = TipoMensaje.Informacion,
                                        Text = "El Monto Ingresado Sobrepasa el Seguro Disponible de la Declaración que es " + initObj.MODPREEM.NemMoneda + " " + Format.FormatCurrency((initObj.MODPREEM.VxIdiDec.DisSeg_M), initObj.MODANUVI.FtoSal),
                                        Title = T_MODANUVI.MsgPlaSO
                                    });
                                    initObj.Frm_PlvSO.Tx_MtoSeg.Text = "";
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            //$ Valor CIF = $Monto FOB + $Monto Flete + $Monto Seguro:
            initObj.Frm_PlvSO.Pn_ValCif.Text =
                Format.FormatCurrency(Format.StringToDouble(initObj.Frm_PlvSO.Tx_MtoFob.Text) + Format.StringToDouble(initObj.Frm_PlvSO.Tx_MtoFle.Text) + Format.StringToDouble(initObj.Frm_PlvSO.Tx_MtoSeg.Text),
                initObj.MODANUVI.FtoSal);

            //$Monto Total:            
            initObj.Frm_PlvSO.Pn_MtoTot.Text = Format.FormatCurrency(Convert.ToDouble(initObj.Frm_PlvSO.Pn_ValCif.Text), T_MODGCON0.FormatoConDec).Replace(".", "");

            //Si paridad es distinto a cero. 
            if (Format.StringToDouble((initObj.Frm_PlvSO.Tx_Paridad.Text)) != 0)
            {
                //Valor CIF en US$:
                initObj.Frm_PlvSO.Pn_CifDol.Text = Format.FormatCurrency((Format.StringToDouble((initObj.Frm_PlvSO.Pn_ValCif.Text)) / Format.StringToDouble((initObj.Frm_PlvSO.Tx_Paridad.Text))), T_MODGCON0.FormatoConDec);
                //Valor Total en US$:
                initObj.Frm_PlvSO.Pn_TotDol.Text = Format.FormatCurrency((Format.StringToDouble((initObj.Frm_PlvSO.Pn_MtoTot.Text)) / Format.StringToDouble((initObj.Frm_PlvSO.Tx_Paridad.Text))), T_MODGCON0.FormatoConDec);
            }
        }
        public static void Tx_TipCam_LostFocus(InitializationObject initObj)
        {
            UI_Frm_PlvSO Frm_PlvSO = initObj.Frm_PlvSO;

            if (ValidaTipoCambio(initObj))
            {
                if (Format.StringToDouble(initObj.Frm_PlvSO.Tx_Paridad.Text) > 0)
                {
                    //Tipo Cambio Eq. US$:
                    initObj.Frm_PlvSO.Pn_TCDol.Text = Format.FormatCurrency(Format.StringToDouble(initObj.Frm_PlvSO.Tx_TipCam.Text) * Format.StringToDouble(initObj.Frm_PlvSO.Tx_Paridad.Text), "###,##0.0000");
                }
            }
        }


        public static bool ValidaTipoCambio(InitializationObject initObj)
        {
            if ((int)initObj.Frm_PlvSO.Ch_Transf.Value == 0)
            {
                if (Format.StringToDouble((initObj.Frm_PlvSO.Tx_TipCam.Text)) == 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "El Tipo de Cambio Debe ser Mayor que Cero.",
                        Title = T_MODANUVI.MsgPlaSO,
                        ControlName = "Tx_TipCam"
                    });
                    return false;
                }
            }

            if ((int)initObj.Frm_PlvSO.Ch_Transf.Value == 0)
            {
                if (initObj.MODGTAB0.VVmd.VmdObs > 0)
                {
                    if (VB6Helpers.Abs((Format.StringToDouble(initObj.Frm_PlvSO.Tx_TipCam.Text) - initObj.MODGTAB0.VVmd.VmdMbv) / initObj.MODGTAB0.VVmd.VmdObs * 100) > 2 )
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = "El Tipo de Cambio Venta supera en un 2% el Tipo de Cambio de pizarra.",
                            Title = T_MODANUVI.MsgPlaSO,
                            ControlName = "Tx_TipCam"
                        });

                        initObj.Frm_PlvSO.Flag_TipCam = 1;
                    }
                }
            }
            return true;
        }

        public static void Tx_Paridad_LostFocus(InitializationObject initObj)
        {
            if (Format.StringToDouble((initObj.Frm_PlvSO.Tx_Paridad.Text)) > 0)
            {
                //Tipo de Cambio US$:
                initObj.Frm_PlvSO.Pn_TCDol.Text = Format.FormatCurrency((Format.StringToDouble(initObj.Frm_PlvSO.Tx_TipCam.Text)) * Format.StringToDouble((initObj.Frm_PlvSO.Tx_Paridad.Text)), "###,##0.0000");
                //Valor CIF en USD$:
                initObj.Frm_PlvSO.Pn_CifDol.Text = Format.FormatCurrency(((Format.StringToDouble(initObj.Frm_PlvSO.Pn_ValCif.Text)) / Format.StringToDouble((initObj.Frm_PlvSO.Tx_Paridad.Text))), initObj.MODANUVI.FtoSal);
                //Valor total en US$:
                initObj.Frm_PlvSO.Pn_TotDol.Text = Format.FormatCurrency(((Format.StringToDouble(initObj.Frm_PlvSO.Pn_MtoTot.Text)) / Format.StringToDouble((initObj.Frm_PlvSO.Tx_Paridad.Text))), initObj.MODANUVI.FtoSal);
            }
        }
        public static void Tx_NroPre_LostFocus(InitializationObject initObj)
        {

            initObj.Frm_PlvSO.Tx_NroPre.Text = VB6Helpers.Format(initObj.Frm_PlvSO.Tx_NroPre.Text, "000000");

            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'MODGPYF1.ValTexto(Tx_NroPre)'. Consider using the GetDefaultMember6 helper method.
            if (Format.StringToDouble((initObj.Frm_PlvSO.Tx_NroPre.Text)) != 0 && !string.IsNullOrWhiteSpace(initObj.Frm_PlvSO.Tx_FecRee.Text))
            {
                initObj.Frm_PlvSO.Tx_Observ.Text = "Se complementa con planilla anulada " + VB6Helpers.Format(initObj.Frm_PlvSO.Tx_NroPre.Text, "000000") + " con fecha " + VB6Helpers.Format(initObj.Frm_PlvSO.Tx_FecRee.Text, "dd/MM/yyyy");
            }

        }
        public static void Tx_CodPag_LostFocus(InitializationObject initObj, UnitOfWorkCext01 uow)
        {

            UI_Frm_PlvSO Frm_PlvSO = initObj.Frm_PlvSO;
            short x = MODPREEM.SyGet_Fdp(initObj, uow);
            short i = 0;

            initObj.Frm_PlvSO.Bandera = (short)(true ? -1 : 0);
            if (!string.IsNullOrEmpty(initObj.Frm_PlvSO.Tx_CodPag.Text))
            {
                initObj.Frm_PlvSO.Bandera = (short)(false ? -1 : 0);
                for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODPREEM.Vx_Fdp); i++)
                {
                    if (initObj.MODPREEM.Vx_Fdp[i].codfdp == Format.StringToDouble((initObj.Frm_PlvSO.Tx_CodPag.Text)))
                    {
                        initObj.Frm_PlvSO.Bandera = (short)(true ? -1 : 0);
                        break;
                    }
                }
            }

            if (~initObj.Frm_PlvSO.Bandera != 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Informacion,
                    Text = "El código de Forma de Pago ingresado no existe.",
                    Title = T_MODANUVI.MsgPlaSO
                });
                return;
            }
            initObj.Frm_PlvSO.Tx_CodPag.Text = VB6Helpers.Format(initObj.Frm_PlvSO.Tx_CodPag.Text, "00");
        }
        public static void Tx_DocChi_LostFocus(InitializationObject initObj)
        {
            if (!string.IsNullOrEmpty(initObj.Frm_PlvSO.Tx_DocChi.Text))
            {
                if (~Mdl_Funciones.Fn_ValidaAladiold(initObj, (initObj.Frm_PlvSO.Tx_DocChi.Text)) != 0)
                {
                    initObj.Frm_PlvSO.Tx_DocChi.Text = "";
                }
            }
        }
        public static void Tx_FecRee_LostFocus(InitializationObject initObj)
        {
            UI_Frm_PlvSO Frm_PlvSO = initObj.Frm_PlvSO;
            double fech = 0;
            if (VB6Helpers.Trim(initObj.Frm_PlvSO.Tx_FecRee.Text) != "")
            {
                if (~MODGPYF1.EsFecha2000(initObj.Frm_PlvSO.Tx_FecRee.Text, initObj, T_MODGCVD.MsgCVD) != 0)
                {
                    return;
                }
                fech = MODGPYF1.ValidaFecha(initObj.Frm_PlvSO.Tx_FecRee.Text);
                if (fech == 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "La Fecha Ingresada es Incorrecta.",
                        Type = TipoMensaje.Informacion,
                        Title = "Planilla Visible Import"
                    });
                    return;
                }
                initObj.Frm_PlvSO.Tx_FecRee.Text = VB6Helpers.Format(VB6Helpers.CStr(fech), "dd/MM/yyyy");
            }
            if (VB6Helpers.CDbl(MODGPYF1.ValTexto(initObj.MODGPYF0, initObj.Frm_PlvSO.Tx_NroPre.Text)) != 0 && VB6Helpers.Trim(initObj.Frm_PlvSO.Tx_FecRee.Text) != "")
            {
                initObj.Frm_PlvSO.Tx_Observ.Text =
                    "Se complementa con planilla anulada " + VB6Helpers.Format(initObj.Frm_PlvSO.Tx_NroPre.Text, "000000") + " con fecha " + VB6Helpers.Format(initObj.Frm_PlvSO.Tx_FecRee.Text, "dd/MM/yyyy");
            }
        }
    }
}
