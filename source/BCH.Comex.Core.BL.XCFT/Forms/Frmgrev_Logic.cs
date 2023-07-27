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

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public static class Frmgrev_Logic
    {
        #region METODOS PUBLICOS
        public static void Form_Load(InitializationObject initObj, UnitOfWorkCext01 uow, UI_Frmgrev frmgrev)
        {
            
            #region Inicializacion Variables
            short[] Tabs = null;
            short a;
            short e;
            short b = 0;
            #endregion

            #region Inicializacion Clases
            initObj.MODGANU.VAnu = new T_Anu();
            #endregion

            //-----------------------------
            //Tabuladores.
            //----------------------------------------------------------
            Tabs = new short[4];
            Tabs[0] = 33;
            Tabs[1] = 77;
            Tabs[2] = 97;
            Tabs[3] = 120;
            a = MODGPYF0.seteatabulador(frmgrev.Lt_Pln, Tabs);
            //----------------------------------------------------------
            frmgrev.Co_Boton[0].Enabled = false;
            //Llamada Metodo Privado.
            InhabilitarCampos_Fr_Invisible(initObj, frmgrev);
            frmgrev.CB_Tipanu.Enabled = false;
            frmgrev.CAM_Tipcam.Enabled = false;
            

            frmgrev.BOT_Obs.Enabled = false;
            frmgrev.Ok.Enabled = false;
            frmgrev.Lt_Pln.Enabled = false;

            frmgrev.Tx_NroOpe[0].Text = initObj.MODGUSR.UsrEsp.CentroCosto;
            frmgrev.Tx_NroOpe[1].Text = T_MODGUSR.IdPro_ComVen;
            frmgrev.Tx_NroOpe[2].Text = initObj.MODGUSR.UsrEsp.Especialista;
            frmgrev.Tx_NroOpe[3].Text = initObj.Module1.Codop.Id_Empresa;
            frmgrev.Tx_NroOpe[4].Text = "";
            
            //Llamada StoredProcedure:
            e = MODGTAB1.SyGetn_Pbc(initObj.MODGTAB1, uow);
            if (e != 0)
            {
                frmgrev.Cb_Pbc.Items.Clear();
                //Llamada StoredProcedure:
                MODGTAB1.CargaEnListaPbc(frmgrev.Cb_Pbc, initObj);
            }

            //************************************
            frmgrev.Cb_Tipo.Items.Clear();
            //Llamada StoredProcedure:
            b = Mdl_Funciones_Varias.SyGet_TipAut(initObj.Mdl_Funciones_Varias,uow);
            MODPREEM.CargaEnLista_TipAut(initObj.Mdl_Funciones_Varias, frmgrev.Cb_Tipo);
            
            //Seteo tipo de anulación
            frmgrev.CB_Tipanu.Items.Clear();
            frmgrev.CB_Tipanu.Items.Add(new UI_ComboItem { Value = "095", Data = 95 });
            frmgrev.CB_Tipanu.Items.Add(new UI_ComboItem { Value = "085", Data = 85 });
            frmgrev.CB_Tipanu.ListIndex = 0;
            frmgrev.Bot_Dec.Enabled = false;
            ReversarOperacion_ClearReport(initObj, uow);
                 
        }
        public static void Ok_Operacion_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            #region Inicializacion Variables
            int x;
            short a = 0;
            short Contador_Rev = 0;
            short i = 0;
            string s = "";
            #endregion
            
            #region inicializacion Clases
            T_MODGCVD MODGCVD = initObj.MODGCVD;

            #endregion

            initObj.frmgrev.Lt_Pln.Items.Clear();
            initObj.frmgrev.Tx_Prty.Text = "";

            //Rescata el Partys de la Operación.
            initObj.MODGANU.VAnu.CodOpe_t = 
                initObj.frmgrev.Tx_NroOpe[0].Text + 
                initObj.frmgrev.Tx_NroOpe[1].Text + 
                initObj.frmgrev.Tx_NroOpe[2].Text + 
                initObj.frmgrev.Tx_NroOpe[3].Text + 
                initObj.frmgrev.Tx_NroOpe[4].Text;

            //initObj.MODGANU.VAnu.CodOpe_t = "827"+"07"+"39"+"000" + "01310";

          //Busca la Operación de compra y ventas
          x = MODGANU.SyGet_CVD(initObj.MODGANU.VAnu.CodOpe_t, initObj, uow);//--> Llamada Stored Procedure.

            //-----------VALIDACIONES-------------
            //Se verifica que exista la operación.
            if (string.IsNullOrEmpty(initObj.MODGCVD.VgCVDo.codope))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message{
                    Text = "La operación de Comercio Exterior no existe.",
                    Title = T_MODGCVD.MsgCVD,
                    Type = TipoMensaje.Informacion
                });
               return;
            }
            //Llamada Metodo Privado.
            HabilitarCampos_Frame3D1(initObj, initObj.frmgrev);
            HabilitarCampos_Frame3D2(initObj, initObj.frmgrev);
           
            //La Fecha de Ingreso NO debe ser igual a la fecha de Hoy.
            initObj.MODCVDIMMM.FechaHoy = DateTime.Now.ToString("dd/MM/yyyy");

            if(!string.IsNullOrEmpty(initObj.MODGCVD.VgCVDo.Fecing))
            {
                if(DateTime.Parse(initObj.MODGCVD.VgCVDo.Fecing).Date == DateTime.Now.Date)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "No podrá revesar esta operación debido a que fue creada con fecha de Hoy.",
                        Title = T_MODGCVD.MsgCVD,
                        Type = TipoMensaje.Informacion
                    });
                    return;
                }
            }

            #region Buscar el nombre del Cliente.
            initObj.frmgrev.Tx_Prty.Text = VB6Helpers.Trim(Mdl_Funciones_Varias.GetDatPrt( initObj ,uow, initObj.MODGCVD.VgCVDo.PrtCli, initObj.MODGCVD.VgCVDo.IndNomC, initObj.MODGCVD.VgCVDo.IndDirC, "N"));
            #endregion

            a = Module1.ResetParty(initObj.Module1, MODGCVD.Beneficiario);
            initObj.Module1.PartysOpe[0].LlaveArchivo = initObj.MODGCVD.VgCVDo.PrtCli;
            initObj.Module1.PartysOpe[0].IndNombre = initObj.MODGCVD.VgCVDo.IndNomC;
            initObj.Module1.PartysOpe[0].IndDireccion = initObj.MODGCVD.VgCVDo.IndDirC;

            initObj.Module1.PartysOpe[0].Status = T_Module1.GPrt_StatDatos;
            //Llamada a StoredProcedure:
            a = MODSYGETPRT.SyGet_Prt(ref initObj.Module1.Codop, -1, initObj,uow);

            //Revisa si operacion es Cosmos
            initObj.MODXORI.gb_esCosmos = false;

            /// Buscar si tiene cuentas activivas del participante.
            MODXORI.SyGet_CtaCte(initObj, uow, initObj.MODGCVD.VgCVDo.PrtCli);

            /// Invovar al WS para validar cuenta activa del participante.
            string token = MODXORI.SrvGetCtaCos(initObj.MODXORI.gs_ctacte_party);

            /// Si WS no retorna nada, se asume que la operación usara cuenta de Banco de Chile.
            Inet1_StateChanged(token, initObj);

            //-->Llamada StoredProcedure:
            if (Mdl_Funciones_Varias.Busca_TipCVD(initObj.MODGANU.VAnu.CodOpe_t,uow,initObj) != "TIN")
            {
                // Busca que no sea transferencia interna
                //Rescata las planillas de la Operación.
                x = MODGANU.SyGetpl_CVD(initObj.MODGANU.VAnu.CodOpe_t, initObj, uow);//Llamada Stored Procedure.
                if (~x != 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "No se han encontrado planillas para la operación de Comercio Exterior.",
                        Title = T_MODGCVD.MsgCVD,
                        Type = TipoMensaje.Informacion
                    });
                    return;
                }

                Contador_Rev = 0;
                //Carga en la lista las planillas de la Operación.
                for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODGANU.VAnuPl); i++)
                {
                    if(!string.IsNullOrEmpty(initObj.MODGANU.VAnuPl[i].NumPln))
                    {                        
                        s = "";
                        s = s + initObj.MODGANU.VAnuPl[i].NumPln + VB6Helpers.Chr(9);
                        s = s + VB6Helpers.Format(initObj.MODGANU.VAnuPl[i].FecPln, "dd/MM/yyyy") + VB6Helpers.Chr(9);
                        s = s + initObj.MODGANU.VAnuPl[i].VisInv + VB6Helpers.Chr(9);
                        s = s + initObj.MODGANU.VAnuPl[i].NemMnd + VB6Helpers.Chr(9);
                        s += initObj.MODGANU.VAnuPl[i].MtoPln_t;
                        if(initObj.MODGANU.VAnuPl[i].estado == T_MODGPLI1.EPli_Rev)
                        {
                            s = s + "  " + T_MODGANU.LstMarca;
                            Contador_Rev = (short)(Contador_Rev + 1);
                        }

                        initObj.frmgrev.Lt_Pln.Items.Add(new UI_ComboItem
                        {
                            Value = s,
                            Data = i
                        });
                    }                
                }

                if (VB6Helpers.UBound(initObj.MODGANU.VAnuPl) == (Contador_Rev -1))
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "La operación de Compra-Venta se encuentra totalmente reversada.",
                        Title = T_MODGCVD.MsgCVD,
                        Type = TipoMensaje.Informacion
                    });
                    initObj.frmgrev.Lt_Pln.Enabled = false;
                    Pr_Habilitar_Pantalla(0,initObj.frmgrev);
                    return;
                }
                else
                {
                    if (initObj.frmgrev.Lt_Pln.Items.Count  > 0)
                    {
                        initObj.frmgrev.Lt_Pln.ListIndex = 0;
                        Lt_Pln_Click(initObj.Mdi_Principal, initObj.MODGANU, initObj.frmgrev);
                    }
                }
                initObj.frmgrev.BOT_Obs.Enabled = true;
                initObj.frmgrev.Ok.Enabled = true;
                HabilitarCampos_Frame3D2(initObj, initObj.frmgrev); 
                HabilitarCampos_Fr_Invisible(initObj, initObj.frmgrev);
            }
            else
            {
                InhabilitarCampos_Frame3D2(initObj, initObj.frmgrev);
                InhabilitarCampos_Fr_Invisible(initObj, initObj.frmgrev);  
                initObj.frmgrev.Lt_Pln.Items.Add(new UI_ComboItem 
                {
                    Value = "Transferencia Interna",
                    Data = 1
                });
                initObj.frmgrev.Co_Boton[0].Enabled = false;
            }
            return;
        }
        public static void Pr_TabStop(string Planilla, InitializationObject initObj)
        {
            short i = 0;
            for (i = 0; i <= 4; i++)
            {
                //initObj.frmgrev.Tx_NroOpe[i].TabStop = true;
                //initObj.frmgrev.Tx_NroOpe[i].TabIndex = i;
            }

            //initObj.frmgrev.Ok_Operacion.TabStop = true;
            //initObj.frmgrev.Ok_Operacion.TabIndex = 5;
            //initObj.frmgrev.Lt_Pln.TabStop = true;
            //initObj.frmgrev.Lt_Pln.TabIndex = 6;

            switch (Planilla)
            {
                case "VIS":
                case "TRN":
                    //-----------------------------
                    //Inhabilitar el TabIndex para el caso de Planilla Invisible.
                    //-----------------------------
                    /*
                    initObj.frmgrev.Fr_Invisible.Enabled = true;
                    initObj.frmgrev.Tx_NroPln.TabStop = false;
                    initObj.frmgrev.Tx_Fecha.TabStop = false;
                    initObj.frmgrev.Cb_Pbc.TabStop = false;
                    initObj.frmgrev.Tx_Motivo.TabStop = false;
                    initObj.frmgrev.Cb_Tipo.TabStop = false;
                    initObj.frmgrev.Tx_ObsPln.TabStop = false;
                    initObj.frmgrev.Tx_TipCam.TabStop = false;
                    initObj.frmgrev.Tx_ObsPln.TabStop = false;
                    initObj.frmgrev.Co_Volver.TabStop = false;
                    //-----------------------------
                    //Habilitar los campos necesarios.
                    //-----------------------------
                    //Tx_MtoAnu.TabStop = True
                    //Tx_MtoAnu.TabIndex = 7
                    initObj.frmgrev.BOT_Obs.TabStop = false;
                    //Co_Observaciones.TabIndex = 8
                    initObj.frmgrev.Ok.TabStop = true;
                    initObj.frmgrev.Ok.TabIndex = 8;
                    initObj.frmgrev.Co_Boton[0].TabStop = true;
                    initObj.frmgrev.Co_Boton[0].TabIndex = 9;
                    initObj.frmgrev.Co_Boton[1].TabStop = true;
                    initObj.frmgrev.Co_Boton[1].TabIndex = 10;
                    */
                    break;
                case "INV":
                    //-----------------------------
                    //Inhabilitar el TabIndex para el caso de Planilla Visible.
                    //-----------------------------
                    HabilitarCampos_Fr_Invisible(initObj,initObj.frmgrev); 
                    //Tx_MtoAnu.TabStop = False
                    /*initObj.frmgrev.Tx_ObsPln.TabStop = false;
                    initObj.frmgrev.Co_Volver.TabStop = false;*/
                    //-----------------------------
                    //Habilitar los campos necesarios.
                    //-----------------------------
                    /*
                    initObj.frmgrev.Tx_NroPln.TabStop = true;
                    initObj.frmgrev.Tx_NroPln.TabIndex = 7;
                    initObj.frmgrev.Tx_Fecha.TabStop = true;
                    initObj.frmgrev.Tx_Fecha.TabIndex = 8;
                    initObj.frmgrev.Cb_Pbc.TabStop = true;
                    initObj.frmgrev.Cb_Pbc.TabIndex = 9;
                    initObj.frmgrev.Tx_Motivo.TabStop = true;
                    initObj.frmgrev.Tx_Motivo.TabIndex = 10;
                    initObj.frmgrev.Cb_Tipo.TabStop = true;
                    initObj.frmgrev.Cb_Tipo.TabIndex = 11;
                    initObj.frmgrev.Tx_TipCam.TabStop = true;
                    initObj.frmgrev.Tx_TipCam.TabIndex = 12;
                    initObj.frmgrev.BOT_Obs.TabStop = false;*/
                    //Co_Observaciones.TabIndex = 13
                    /*
                    initObj.frmgrev.Ok.TabStop = true;
                    initObj.frmgrev.Ok.TabIndex = 13;
                    initObj.frmgrev.Co_Boton[0].TabStop = true;
                    initObj.frmgrev.Co_Boton[0].TabIndex = 14;
                    initObj.frmgrev.Co_Boton[1].TabStop = true;
                    initObj.frmgrev.Co_Boton[1].TabIndex = 15;
                    */
                    break;
                case "OBS":
                    //-----------------------------
                    //Inhabilitar el TabIndex para el caso de Planillas.
                    //-----------------------------
                    InhabilitarCampos_Fr_Invisible(initObj, initObj.frmgrev);
                    InhabilitarCampos_Frame3D2(initObj, initObj.frmgrev); 
                    /*initObj.frmgrev.Tx_NroPln.TabStop = false;
                    initObj.frmgrev.Tx_Fecha.TabStop = false;
                    initObj.frmgrev.Cb_Pbc.TabStop = false;
                    initObj.frmgrev.Tx_Motivo.TabStop = false;
                    initObj.frmgrev.Cb_Tipo.TabStop = false;
                    initObj.frmgrev.Tx_ObsPln.TabStop = false;
                    */
                    for (i = 0; i <= 4; i++)
                    {
                        //initObj.frmgrev.Tx_NroOpe[i].TabStop = false;
                    }

                    /*
                    initObj.frmgrev.Ok_Operacion.TabStop = false;
                    initObj.frmgrev.Lt_Pln.TabStop = false;
                    */

                    //-----------------------------
                    //Habilitar los campos necesarios.
                    //-----------------------------
                    /*
                    initObj.frmgrev.Tx_ObsPln.TabStop = true;
                    initObj.frmgrev.Tx_ObsPln.TabIndex = 0;
                    initObj.frmgrev.Co_Volver.TabStop = true;
                    initObj.frmgrev.Co_Volver.TabIndex = 1;
                    */
                    break;
            }

        }
        public static void Bot_Dec_Click(InitializationObject initObj)
        {
            initObj.frmgrev.AbrirReversarOperacionDeclaracion = true; 
        }
        public static void BOT_Obs_Click(InitializationObject initObj)
        {
            short i = 0;
            if (initObj.frmgrev.Lt_Pln.ListIndex > -1)
            {
                i = (short)(initObj.frmgrev.Lt_Pln.get_ItemData_(initObj.frmgrev.Lt_Pln.ListIndex));
                string _switchVar1 = VB6Helpers.Trim(initObj.MODGANU.VAnuPl[i].VisInv);

                if (_switchVar1 == "VIS")
                {
                    initObj.frmgrev.Fr_Observaciones.Caption = "Observaciones de Planilla Visible";
                }
                else if (_switchVar1 == "INV")
                {
                    initObj.frmgrev.Fr_Observaciones.Caption = "Observaciones de Planilla Invisible";
                }
           }

            //initObj.frmgrev.Fr_Observaciones.Left = 90;
            //initObj.frmgrev.Fr_Observaciones.Top = 2910;
            initObj.frmgrev.Fr_Observaciones.Visible = true;
            InhabilitarCampos_Frame3D1(initObj, initObj.frmgrev); 
            initObj.frmgrev.Tx_Prty.Enabled = false;
            initObj.frmgrev.Frame3D2.Enabled = false;
            initObj.frmgrev.Co_Boton[0].Enabled = false;
            initObj.frmgrev.Co_Boton[1].Enabled = false;
            //initObj.frmgrev.Co_Volver.Cancel = true;

            //initObj.frmgrev.Pr_TabStop("OBS");
            //initObj.frmgrev.Tx_ObsPln.SetFocus();
        }
        public static void CAM_TipCam_KeyPress(ref short KeyAscii, InitializationObject initObj)
        {
            if (KeyAscii == 13)
            {
                KeyAscii = 0;
                //VB6Helpers.SendKeys("{Tab}");
            }
            else
            {
                KeyAscii = MODGPYF0.Mascara(KeyAscii, initObj.frmgrev.CAM_Tipcam);
            }
        }
        public static void CAM_Tipcam_LostFocus(InitializationObject initObj)
        {
            initObj.frmgrev.CAM_Tipcam.Tag = "________.____";   
            short a = MODGPYF0.MascaraLost(initObj.frmgrev.CAM_Tipcam, initObj.MODGPYF0);
            short i;
            initObj.frmgrev.CAM_Tipcam.Text = Utils.Format.FormatCurrency(double.Parse(initObj.frmgrev.CAM_Tipcam.Text), "###,##0.0000");

            i = (short)initObj.frmgrev.Lt_Pln.get_ItemData_((short)initObj.frmgrev.Lt_Pln.ListIndex);
            initObj.MODGANU.VAnuPl[i].TipCam = double.Parse(initObj.frmgrev.CAM_Tipcam.Text);
            
        }
        public static bool Co_Boton_Click(ref short Index, T_MODXORI MODXORI, InitializationObject initObj, UnitOfWorkCext01 uow, UI_Frmgrev frmgrev)
        {
            #region Inicializacion Variables 
            short a = 0;
            short j = 0;
            short i = 0;
            short n = 0;
            #endregion
            BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.SyGetn_Codtran(MODXORI, uow); //---> Llamada a StoredProcedure.
            double x = 0;
        
            switch (Index)
            {
                #region Botón Aceptar
                case 0:  //Bóton Aceptar.
                    //Inicializar Ingreso de valores:
                    initObj.Frm_Ingreso_Valores = new BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos.UI_Frm_Ingreso_Valores();
                    for (a = 0; a <= (short)VB6Helpers.UBound(initObj.MODGANU.VAnuPl); a++)
                    {
                        x += initObj.MODGANU.VAnuPl[a].MtoAnu;
                    }

                    if (x == 0 && Index == 0)
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "Debe definir monto a anular",
                            Title = T_MODGCVD.MsgCVD,
                            Type = TipoMensaje.Informacion
                        });
                        return false;
                    }

                    for (j = 0; j <= (short)(frmgrev.Lt_Pln.Items.Count - 1); j++)
                    {
                        i = (short)frmgrev.Lt_Pln.get_ItemData_(j);
                        
                        if (initObj.MODGANU.VAnuPl[i].MtoAnu > 0)
                        {
                            string _switchVar1 = VB6Helpers.Trim(initObj.MODGANU.VAnuPl[i].VisInv);
                            if (_switchVar1 == "VIS" || _switchVar1 == "TRN")
                            {
                                #region Planilla Visible / Transferencia.
                                //-------------------------------------
                                //Adiciona una Planilla Visible Anulada.
                                //-------------------------------------
                                //-->Llamada Stored Procedure
                                a = BCH.Comex.Core.BL.XCFT.Modulos.MODXANU.SyGet_xPlAnu(initObj.MODGANU.VAnuPl[i].NumPln, Convert.ToDateTime(initObj.MODGANU.VAnuPl[i].FecPln), (initObj.MODGANU.VAnuPl[i].MtoAnu), initObj.MODGANU.VAnuPl[i].ObsPln, initObj, uow);
                                
                                n = (short)VB6Helpers.UBound(initObj.MODXANU.VxAnus);
                                
                                initObj.MODXANU.VxAnus[n].codcct = initObj.MODGCVD.VgCVDNul.codcct;
                                initObj.MODXANU.VxAnus[n].codpro = initObj.MODGCVD.VgCVDNul.codpro;
                                initObj.MODXANU.VxAnus[n].codesp = initObj.MODGCVD.VgCVDNul.codesp;
                                initObj.MODXANU.VxAnus[n].codofi = initObj.MODGCVD.VgCVDNul.codofi;
                                initObj.MODXANU.VxAnus[n].codope = initObj.MODGCVD.VgCVDNul.codope;
                                initObj.MODXANU.VxAnus[n].TipAnu = initObj.MODGANU.VAnuPl[i].TipAnu;
                                initObj.MODXANU.VxAnus[n].MtoAnu = initObj.MODGANU.VAnuPl[i].MtoAnu;
                                initObj.MODXANU.VxAnus[n].TipCam = initObj.MODGANU.VAnuPl[i].TipCam;
                                initObj.MODXANU.VxAnus[n].ObsPln = initObj.MODGANU.VAnuPl[i].ObsPln;
                                initObj.MODXANU.VxAnus[n].Estado = initObj.MODGANU.VAnuPl[i].estado;
                                initObj.MODXANU.VxAnus[n].PlzBcc = initObj.MODGANU.VAnuPl[i].PlzBcc;
                                initObj.MODXANU.VxAnus[n].TipAut = initObj.MODGANU.VAnuPl[i].ApcTip;
                                initObj.MODXANU.VxAnus[n].NroAut = VB6Helpers.Val(initObj.MODGANU.VAnuPl[i].ApcNum);
                                initObj.MODXANU.VxAnus[n].FecAut = initObj.MODGANU.VAnuPl[i].ApcFec;
                                #endregion
                            }
                            else if (_switchVar1 == "INV")
                            {
                                #region Planilla Invisible
                                //------------------------------
                                //Adiciona una Planilla Invisible.
                                //------------------------------
                                a = 
                                    BCH.Comex.Core.BL.XCFT.Modulos.MODGANU.SyGet_PliAnu(i,initObj,uow);//-->Llamada Stored Procedure.
                                n = 
                                    (short)VB6Helpers.UBound(initObj.MODGPLI1.Vplis);
                                
                                initObj.MODGPLI1.Vplis[n].codcct = initObj.MODGCVD.VgCVDNul.codcct;
                                initObj.MODGPLI1.Vplis[n].codpro = initObj.MODGCVD.VgCVDNul.codpro;
                                initObj.MODGPLI1.Vplis[n].codesp = initObj.MODGCVD.VgCVDNul.codesp;
                                initObj.MODGPLI1.Vplis[n].codofi = initObj.MODGCVD.VgCVDNul.codofi;
                                initObj.MODGPLI1.Vplis[n].codope = initObj.MODGCVD.VgCVDNul.codope;
                                
                                //Llamada Stored Procedure.
                                initObj.MODGPLI1.Vplis[n].Mtopar = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB1.SyGet_Vmc(initObj.MODGTAB0, uow, initObj.MODGPLI1.Vplis[n].CodMnd, initObj.MODGPLI1.Vplis[n].FecPli, "P");
                                if (initObj.MODGPLI1.Vplis[n].Mtopar > 0)
                                {
                                    initObj.MODGPLI1.Vplis[n].MtoDol = initObj.MODGPLI1.Vplis[n].MtoOpe / initObj.MODGPLI1.Vplis[n].Mtopar;
                                }

                                if (initObj.MODGANU.VAnuPl[i].TipPln == T_Mdl_Funciones.TPli_Ingreso)
                                {
                                    initObj.MODGPLI1.Vplis[n].TipPln = T_Mdl_Funciones.TPli_AnuIng;
                                }
                                else if (initObj.MODGANU.VAnuPl[i].TipPln == T_Mdl_Funciones.TPli_Egreso)
                                {
                                    initObj.MODGPLI1.Vplis[n].TipPln = T_Mdl_Funciones.TPli_AnuEgr;
                                }
                                else if (initObj.MODGANU.VAnuPl[i].TipPln == T_Mdl_Funciones.TPli_TranIng)
                                {
                                    initObj.MODGPLI1.Vplis[n].TipPln = T_Mdl_Funciones.TPli_AnuTranIng;
                                }
                                else if (initObj.MODGANU.VAnuPl[i].TipPln == T_Mdl_Funciones.TPli_TranEg)
                                {
                                    initObj.MODGPLI1.Vplis[n].TipPln = T_Mdl_Funciones.TPli_AnuTranEg;
                                }
                                #endregion
                            }
                        }
                    }
                    initObj.MODGCVD.VgCvd.codcct = initObj.MODGCVD.VgCVDNul.codcct;
                    initObj.MODGCVD.VgCvd.codpro = initObj.MODGCVD.VgCVDNul.codpro;
                    initObj.MODGCVD.VgCvd.codesp = initObj.MODGCVD.VgCVDNul.codesp;
                    initObj.MODGCVD.VgCvd.codofi = initObj.MODGCVD.VgCVDNul.codofi;
                    initObj.MODGCVD.VgCvd.codope = initObj.MODGCVD.VgCVDNul.codope;
                    initObj.MODGCVD.VgCvd.estado = T_MODGCVD.ECvd_Ing;
                    initObj.MODGCVD.VgCvd.operel = initObj.MODGCVD.VgCVDo.codcct + initObj.MODGCVD.VgCVDo.codpro + initObj.MODGCVD.VgCVDo.codesp + initObj.MODGCVD.VgCVDo.codofi + initObj.MODGCVD.VgCVDo.codope;
                    initObj.MODGCVD.VgCvd.PrtCli = initObj.MODGCVD.VgCVDo.PrtCli;
                    initObj.MODGCVD.VgCvd.IndNomC = initObj.MODGCVD.VgCVDo.IndNomC;
                    initObj.MODGCVD.VgCvd.IndDirC = initObj.MODGCVD.VgCVDo.IndDirC;
                    initObj.MODGCVD.VgCvd.IndPopC = initObj.MODGCVD.VgCVDo.IndPopC;
                    initObj.MODGCVD.VgCvd.PrtOtr = initObj.MODGCVD.VgCVDo.PrtOtr;
                    initObj.MODGCVD.VgCvd.IndNomO = initObj.MODGCVD.VgCVDo.IndNomO;
                    initObj.MODGCVD.VgCvd.IndDirO = initObj.MODGCVD.VgCVDo.IndDirO;
                    initObj.MODGCVD.VgCvd.IndPopO = initObj.MODGCVD.VgCVDo.IndPopO;
                    initObj.MODGCVD.VgCvd.Acepto = (short)(true ? -1 : 0);
                    initObj.MODGCVD.VgCvd.AceptoRev = (short)(true ? -1 : 0);
                    break;
                #endregion

                #region Boton Cancelar
                case 1:  //Cancelar
                    initObj.MODGCVD.VgCvd.Acepto = (short)(false ? -1 : 0);
                    initObj.MODGCVD.VgCvd.AceptoRev = (short)(false ? -1 : 0);
                    break;
                #endregion
            }
            return true;
        }
        public static void Co_Volver_Click(UI_Frmgrev frmgrev, InitializationObject initObj)
        {
            #region Inicializacion Variables
            short i = 0;
            #endregion

            #region Inicializacion Clases
            T_MODGANU MODGANU = initObj.MODGANU;
            #endregion

            #region Object Formulario
            frmgrev.Fr_Observaciones.Visible = false;
            //frmgrev.Fr_Observaciones.Left = -90;
            //frmgrev.Fr_Observaciones.Top = 29100;
            HabilitarCampos_Frame3D1(initObj, initObj.frmgrev); 

            frmgrev.Tx_Prty.Enabled = true;
            HabilitarCampos_Frame3D2(initObj, initObj.frmgrev);
            frmgrev.Co_Boton[0].Enabled = true;
            frmgrev.Co_Boton[1].Enabled = true;
            //frmgrev.Co_Boton[1].Cancel = true;
            #endregion

            if (frmgrev.Lt_Pln.ListIndex > -1)
            {
                i = (short)(frmgrev.Lt_Pln.get_ItemData_((short)frmgrev.Lt_Pln.ListIndex));

                string _switchVar1 = VB6Helpers.Trim(MODGANU.VAnuPl[i].VisInv);
                if (_switchVar1 == "VIS")
                {
                    Pr_TabStop("VIS",initObj);
                }
                else if (_switchVar1 == "INV")
                {
                    Pr_TabStop("INV",initObj);
                }
            }
        }
        public static void Inet1_StateChanged(string TOKEN, InitializationObject initObj)
        {
            if (TOKEN == "YTD" || TOKEN == "YEX")
            {
                initObj.MODXORI.gb_esCosmos = true;
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Atención: Cliente posee Cuenta Cosmos ",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODGCVD.MsgCVD
                });
                initObj.Module1.Codop = initObj.Module1.Codop_FT.Clone();
                initObj.CaptionAddition = "Cuenta Citi";
            }

            if (TOKEN == "CTD" || TOKEN == "CEX")
            {
                initObj.MODXORI.gb_esCosmos = false;
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Atención: Cliente posee Cuenta Banco de Chile ",
                    Type = TipoMensaje.Informacion,
                    Title = T_MODGCVD.MsgCVD
                });
                initObj.Module1.Codop = initObj.Module1.Codop_CVD.Clone();
                initObj.CaptionAddition = "Cuenta BCH";
            }

            if (string.IsNullOrEmpty(TOKEN))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "No se encontró el Tipo de Cuenta del Participante, Se asume que es una Cuenta Banco de Chile.",
                    Type = TipoMensaje.Warning,
                    Title = T_MODGCVD.MsgCVD
                });
                initObj.Module1.Codop = initObj.Module1.Codop_CVD.Clone();
                initObj.CaptionAddition = "Cuenta BCH";
            }

            initObj.MODGCVD.VgCvd.codpro = initObj.Module1.Codop.Id_Product;
            initObj.MODGCVD.VgCvd.codope = initObj.Module1.Codop.Id_Operacion;
            initObj.MODGCVD.VgCvd.OpeSin = initObj.MODGCVD.VgCvd.codcct + initObj.MODGCVD.VgCvd.codpro + initObj.MODGCVD.VgCvd.codesp + initObj.MODGCVD.VgCvd.codofi + initObj.MODGCVD.VgCvd.codope;
            initObj.MODGCVD.VgCvd.OpeCon = initObj.MODGCVD.VgCvd.codcct + "-" + initObj.MODGCVD.VgCvd.codpro + "-" + initObj.MODGCVD.VgCvd.codesp + "-" + initObj.MODGCVD.VgCvd.codofi + "-" + initObj.MODGCVD.VgCvd.codope;

            initObj.MODGCVD.VgCVDNul.codpro = initObj.Module1.Codop.Id_Product;
            initObj.MODGCVD.VgCVDNul.codope = initObj.Module1.Codop.Id_Operacion;

            if (initObj.MODGCVD.COMISION == true)
            {
                initObj.Frm_Principal.Caption = "Comisiones Fund Transfer  " + initObj.MODGCVD.VgCvd.OpeCon + " " + initObj.CaptionAddition;
            }
            else
            {
                initObj.Frm_Principal.Caption = "Compra Venta de Divisas Fund Transfer  " + initObj.MODGCVD.VgCvd.OpeCon + " " + initObj.CaptionAddition;
            }
        }
        public static void Lt_Pln_Click(UI_Mdi_Principal Mdi_Principal,T_MODGANU MODGANU, UI_Frmgrev frmgrev)
        {
            #region Variables
            short i = 0;
            short j = 0;
            short Moneda_Compra = 0;
            #endregion

            if (frmgrev.Lt_Pln.ListIndex > -1)
            {
                 i = (short)frmgrev.Lt_Pln.get_ItemData_(frmgrev.Lt_Pln.ListIndex);
                 j = (short)frmgrev.Lt_Pln.ListIndex;

                 if(!string.IsNullOrEmpty(MODGANU.VAnuPl[i].numdec) && !string.IsNullOrEmpty(MODGANU.VAnuPl[i].FecDec) && MODGANU.VAnuPl[i].CodAdn > 0)
                {
                    frmgrev.Bot_Dec.Enabled = true;
                }
                else
                {
                    frmgrev.Bot_Dec.Enabled = false;
                }

                string _switchVar1 = VB6Helpers.Trim(MODGANU.VAnuPl[i].VisInv);
                if (_switchVar1 == "VIS" || _switchVar1 == "TRN")
                {
                    if (MODGANU.VAnuPl[i].estado == T_MODGPLI1.EPli_Rev)
                    {
                        Pr_Habilitar_Pantalla(0, frmgrev);
                        //Si una Planilla esta reversada => no se puede volver a reversar.
                        Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "Esta planilla de la operación de Compra - Venta ya se encuentra totalmente reversada.",
                            Type = TipoMensaje.Informacion,
                            Title = T_MODGCVD.MsgCVD
                        });
                        return;
                    }
                    else
                    {
                        //---------------------------
                        //Entrega los TabIndex ordenador para el caso del Tipo de Planilla.
                        //---------------------------
                        frmgrev.CAM_Tipcam.Enabled = true;
                        frmgrev.CAM_Tipcam.Text = Utils.Format.FormatCurrency(MODGANU.VAnuPl[i].TipCam, "###,##0.0000");
                        frmgrev.Tx_ObsPln.Text = MODGANU.VAnuPl[i].ObsPln;
                        frmgrev.CB_Tipanu.ListIndex = MODGPYF0.PosLista(frmgrev.CB_Tipanu,MODGANU.VAnuPl[i].TipAnu);
                        if (frmgrev.CB_Tipanu.ListIndex == -1)
                        {
                            frmgrev.CB_Tipanu.ListIndex = 0;
                        }
                    }
                }
                else if (_switchVar1 == "INV")
                {
                    frmgrev.CB_Tipanu.Enabled = false;
                    frmgrev.CB_Tipanu.ListIndex = -1;
                    frmgrev.CAM_Tipcam.Enabled = false;
                    
                    if (MODGANU.VAnuPl[i].estado == T_MODGPLI1.EPli_Rev)
                    {
                        //Si una Planilla esta reversada => no se puede volver a reversar.
                        Pr_Habilitar_Pantalla(0, frmgrev);
                        Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "Esta planilla de la operación de Compra-Venta ya se encuentra totalmente reversada.",
                            Type = TipoMensaje.Informacion,
                            Title = T_MODGCVD.MsgCVD
                        });
                        return;
                    }
                    else
                    {
                        //---------------------------
                        //Entrega los TabIndex ordenador para el caso del Tipo de Planilla.
                        //---------------------------
                        //Si tiene datos los despliega.
                        //---------------------------
                        Moneda_Compra = MODGANU.VAnuPl[i].CodMnd;
                        frmgrev.Tx_TipCam.Text = decimal.Round(decimal.Parse(MODGANU.VAnuPl[i].TipCam.ToString()), 2).ToString("###,##0.00");
                        frmgrev.Tx_Motivo.Text = (MODGANU.VAnuPl[i].Motivo ?? string.Empty).Trim();
                        frmgrev.Tx_ObsPln.Text = MODGANU.VAnuPl[i].ObsPln;
                        frmgrev.Tx_NroPln.Text = (MODGANU.VAnuPl[i].ApcNum ?? string.Empty).Trim();
                        frmgrev.Tx_Fecha.Text = (MODGANU.VAnuPl[i].ApcFec ?? string.Empty).Trim();
                        frmgrev.Cb_Pbc.ListIndex = MODGPYF0.PosLista(frmgrev.Cb_Pbc, MODGANU.VAnuPl[i].PlzBcc);
                    }
                }
            }
        }
        public static bool ok_Click_1(UI_Mdi_Principal Mdi_Principal, T_MODGANU MODGANU, UI_Frmgrev frmgrev, InitializationObject initObj, UnitOfWorkCext01 uow, bool vieneDeMensaje, bool resMensaje)
        {
            #region Inicializacion Variables
            short i = 0;
            short x = 0;
            short R = 0;
            string Num = "";
            #endregion
            //------------------------------------------
            //Se debe haber seleccionado una planilla.
            //------------------------------------------
            if (frmgrev.Lt_Pln.ListIndex == -1)
            {
                return false;
            }
            
            i = (short)frmgrev.Lt_Pln.get_ItemData_(frmgrev.Lt_Pln.ListIndex);

            if (MODGANU.VAnuPl[i].ValLiq == 0 && frmgrev.Bot_Dec.Enabled)
            {
                frmgrev.Errores.Add(new UI_Message(){
                    Type=TipoMensaje.Error,
                    Text = "Debe ingresar los valores para devolver el Saldo a la Declaración."
                });
                return false;
            }
            else {
                 MODGANU.VAnuPl[i].MtoAnu = MODGANU.VAnuPl[i].ValLiq == 0 ? MODGANU.VAnuPl[i].MtoPln : MODGANU.VAnuPl[i].ValLiq;
            }

            //Si tiene planillas anuladas ---
            Num = MODXPLN1.ConvRut(MODGANU.VAnuPl[i].NumPln);
            string _switchVar1 = VB6Helpers.Trim(MODGANU.VAnuPl[i].VisInv);
            //------------------------------------------
            //Planilla Transferencia.
            //------------------------------------------
            if (_switchVar1 == "TRN")
            {
                #region Planilla Transferencia.
                //Llamada StoredProcedure:
                x = Modulos.MODGANU.SyGet_ANU(Num, Convert.ToDateTime(MODGANU.VAnuPl[i].FecPln), initObj, uow);
                if (x != 0)
                {

                    if (!vieneDeMensaje)
                    {
                        if (frmgrev.PopUps.Count <= 0)
                        {
                            frmgrev.PopUps.Add(new UI_Message()
                            {
                                Type = TipoMensaje.YesNo,
                                Text = "Esta planilla ya ha sido anulada ¿ Desea Continuar ?"
                            });
                            return false;
                        }
                    }
                    else if (resMensaje) { }
                }

                if (frmgrev.Cb_Tipo.ListIndex == -1)
                {
                    frmgrev.Errores.Add(new UI_Message() 
                    {
                        Type = TipoMensaje.Error,
                        Text = "Atención: Es necesario que se seleccione un Tipo para la Autorización Previa del Banco Central para poder Reversar la Operación.",
                        ControlName = "Cb_Tipo_SelectedValue"
                    });
                    return false;
                }


                if (initObj.frmgrev.Cb_Tipo.get_ItemData_((short)initObj.frmgrev.Cb_Tipo.ListIndex) != 6)
                {
                    if (string.IsNullOrEmpty(frmgrev.Tx_NroPln.Text))
                    {
                        frmgrev.Errores.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Atención: Es necesario que ingrese un Nro. de autorización para poder Reversar la Operación.",
                            ControlName = "Numero_Text"
                        });
                        return false;
                    }

                    if (string.IsNullOrEmpty(frmgrev.Tx_Fecha.Text))
                    {

                        frmgrev.Errores.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Atención: Es necesario que ingrese una Fecha de autorización para poder Reversar la Operación.",
                            ControlName = "Fecha_Text"
                        });
                        return false;
                    }

                    if (!string.IsNullOrEmpty(initObj.MODCVDIMMM.FechaHoy))
                    {
                        if (DateTime.Parse(initObj.MODCVDIMMM.FechaHoy).Date < DateTime.Parse(MODGANU.VAnuPl[i].FecPln).Date)
                        {
                            frmgrev.Errores.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "No podrá reversar esta Operación debido a que la fecha original de la planilla es es menor a la fecha recien ingresada.",
                                ControlName = "Fecha_Text"

                            });
                            return false;
                        }
                    }

                    if (string.IsNullOrEmpty(frmgrev.Tx_Motivo.Text))
                    {
                        frmgrev.Errores.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Atención: Es necesario que ingrese un Motivo para poder Reversar la Operación.",
                            ControlName = "Motivo_Text"
                        });
                        return false;
                    }
                }
                

                if (frmgrev.Cb_Pbc.ListIndex == -1)
                {
                    frmgrev.Errores.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Atención: Es necesario que se seleccione una Plaza del Banco Central para poder Reversar la Operación.",
                        ControlName = "Cb_SucursalBCCH_SelectedValue"
                    });
                    return false;
                }

                initObj.MODCVDIMMM.FechaHoy = VB6Helpers.Format(frmgrev.Tx_Fecha.Text, "dd/MM/yyyy");
                //------------------------------------------
                //Validar que la fecha ingresada sea mayor o igual a la fecha original.
                //------------------------------------------
                //------------------------------------------
                //Validar que la fecha ingresada sea menor o igual a la fecha de hoy.
                //------------------------------------------
                if(!string.IsNullOrEmpty(initObj.MODCVDIMMM.FechaHoy))
                {
                    if(DateTime.Parse(initObj.MODCVDIMMM.FechaHoy).Date > DateTime.Now.Date)
                    { 
                    frmgrev.Errores.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "No podrá reversar esta Operación debido a que la fecha ingresada es menor a la fecha actual.",
                        ControlName = "Fecha_Text"
                    });
                    return false;
                    }
                }

                if (frmgrev.CB_Tipanu.ListIndex == -1)
                {
                    frmgrev.Errores.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Atención: Es necesario que se ingrese el tipo de anulación para poder Reversar la Operación.",
                        ControlName = "Cb_TipAnu_SelectedValue"
                    });
                    return false;
                }
                //------------------------------------------
                //Traspaso de Datos.
                //------------------------------------------
                MODGANU.VAnuPl[i].TipDoc = 10;
                MODGANU.VAnuPl[i].TipAnu = (short)VB6Helpers.Val(frmgrev.CB_Tipanu.Text);
                MODGANU.VAnuPl[i].ObsPln = VB6Helpers.Trim(frmgrev.Tx_ObsPln.Text);
                if(!string.IsNullOrEmpty(frmgrev.CB_Tipanu.Text))
                {
                    MODGANU.VAnuPl[i].TipAnu = VB6Helpers.CShort(frmgrev.CB_Tipanu.Text);
                }
                MODGANU.VAnuPl[i].MtoAnu = MODGANU.VAnuPl[i].MtoPln;
                MODGANU.VAnuPl[i].TipCam = Format.StringToDouble((frmgrev.Tx_TipCam.Text));
                MODGANU.VAnuPl[i].Motivo = VB6Helpers.Trim(frmgrev.Tx_Motivo.Text);
                MODGANU.VAnuPl[i].ObsPln = VB6Helpers.Trim(frmgrev.Tx_ObsPln.Text);
                
                if ((short)frmgrev.Lt_Pln.get_ItemData_(frmgrev.Lt_Pln.ListIndex) != 6)
                {
                    MODGANU.VAnuPl[i].ApcTip = VB6Helpers.Left(VB6Helpers.Trim(frmgrev.Cb_Tipo.get_List((short)frmgrev.Cb_Tipo.ListIndex)), 2);
                    MODGANU.VAnuPl[i].ApcNum = VB6Helpers.Trim(frmgrev.Tx_NroPln.Text);
                    MODGANU.VAnuPl[i].ApcFec = VB6Helpers.Trim(frmgrev.Tx_Fecha.Text);
                }
                else
                {
                    MODGANU.VAnuPl[i].ApcTip = "";
                    MODGANU.VAnuPl[i].ApcNum = "";
                    MODGANU.VAnuPl[i].ApcFec = "";
                }
                
                MODGANU.VAnuPl[i].PlzBcc = (short)(frmgrev.Cb_Pbc.get_ItemData_(frmgrev.Lt_Pln.ListIndex));
               
                //------------------------------------------
                //Limpia los campos.
                //------------------------------------------
                frmgrev.Tx_ObsPln.Text = "";
                frmgrev.Co_Boton[0].Enabled = true;
                frmgrev.CB_Tipanu.ListIndex = -1;
                
                #endregion
            }
            //------------------------------------------
            //Planilla Visible.
            //------------------------------------------
            else if (_switchVar1 == "VIS")
            {
                #region Planilla Visible.
                //--> Llamada StoredProcedure
                x = Modulos.MODGANU.SyGet_ANU(Num, Convert.ToDateTime(MODGANU.VAnuPl[i].FecPln), initObj, uow);//Llamada StoredProcedure falta.
                if (x != 0)
                {
                    if (vieneDeMensaje)
                    {
                        if (frmgrev.PopUps.Count <= 0)
                        {
                            frmgrev.PopUps.Add(new UI_Message()
                            {
                                Type = TipoMensaje.YesNo,
                                Text = "Esta planilla ya ha sido anulada ¿ Desea Continuar ?"
                            });
                            return false;
                        }
                    }
                    else if (resMensaje) { }
                 }

                if (frmgrev.Cb_Tipo.ListIndex == -1)
                {
                    frmgrev.Errores.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Atención: Es necesario que se seleccione un Tipo para la Autorización Previa del Banco Central para poder Reversar la Operación.",
                        ControlName = "Cb_Tipo_SelectedValue"
                    });
                    return false;
                }


                if ((short)frmgrev.Cb_Tipo.get_ItemData_(frmgrev.Cb_Tipo.ListIndex) != 6)
                {

                    if (string.IsNullOrWhiteSpace(frmgrev.Tx_NroPln.Text))
                    {
                        frmgrev.Errores.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Atención: Es necesario que ingrese un Nro. de Autorización para poder Reversar la Operación.",
                            ControlName = "Numero_Text"
                        });
                        return false;
                    }

                    if (VB6Helpers.Trim(frmgrev.Tx_Fecha.Text) == "")
                    {
                        frmgrev.Errores.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Atención: Es necesario que ingrese una Fecha de Autorización para poder Reversar la Operación.",
                            ControlName = "Fecha_Text"
                        });
                        return false;
                    }

                    if (!string.IsNullOrEmpty(initObj.MODCVDIMMM.FechaHoy))
                    {
                        if (VB6Helpers.StrComp(VB6Helpers.Format(initObj.MODCVDIMMM.FechaHoy, "yyyyMMdd"), VB6Helpers.Format(MODGANU.VAnuPl[i].FecPln, "yyyyMMdd")) < 0)
                        {
                            frmgrev.Errores.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "No podrá reversar esta Operación debido a que la fecha original de la planilla es menor a la fecha recien ingresada.",
                                ControlName = "Fecha_Text"
                            });
                            return false;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(frmgrev.Tx_Motivo.Text))
                    {
                        frmgrev.Errores.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Atención: Es necesario que ingrese un Motivo para poder Reversar la Operación.",
                            ControlName = "Motivo_Text"
                        });
                        return false;
                    }
                }
                

                if (frmgrev.Cb_Pbc.ListIndex == -1)
                {
                    frmgrev.Errores.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Atención: Es necesario que se seleccione una Plaza del Banco Central para poder Reversar la Operación.",
                        ControlName = "Cb_SucursalBCCH_SelectedValue"
                    });
                    return false;
                }

                if (MODGANU.VAnuPl[i].TipPln == T_Mdl_Funciones.TPli_Ingreso || MODGANU.VAnuPl[i].TipPln == T_Mdl_Funciones.TPli_Egreso)
                {
                    if (VB6Helpers.Val(frmgrev.Tx_TipCam.Text) == 0)
                    {
                        frmgrev.Errores.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Atención: Es necesario que ingrese un Tipo de Cambio para poder Reversar la Operación.",
                            ControlName = "Tx_TipCam_Text"
                        });
                        return false;
                    }

                }

                initObj.MODCVDIMMM.FechaHoy = VB6Helpers.Format(frmgrev.Tx_Fecha.Text, "dd/MM/yyyy");
                //------------------------------------------
                //Validar que la fecha ingresada sea mayor o igual a la fecha original.
                //------------------------------------------
                //------------------------------------------
                //Validar que la fecha ingresada sea menor o igual a la fecha de hoy.
                //------------------------------------------
                
                if(!string.IsNullOrEmpty(initObj.MODCVDIMMM.FechaHoy))
                {
                    if(DateTime.Parse(initObj.MODCVDIMMM.FechaHoy).Date > DateTime.Now.Date)
                    {
                        frmgrev.Errores.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "No podrá reversar esta Operación debido a que la fecha ingresada es menor a la fecha actual.",
                            ControlName = "Fecha_Text"
                        });
                        return false;
                    }
                }


                if (frmgrev.CB_Tipanu.ListIndex == -1)
                {
                    frmgrev.Errores.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Atención: Es necesario que se ingrese el tipo de anulación para poder Reversar la Operación.",
                        ControlName = "Cb_TipAnu_SelectedValue"
                    });
                    return false;
                }

                if (Format.StringToDouble((frmgrev.Tx_TipCam.Text)) == 0)
                {
                    frmgrev.Errores.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Debe ingresar el tipo de cambio para poder reversar la operación.",
                        ControlName = "Tx_TipCam"
                    });
                    return false;
                }

                //------------------------------------------
                //Traspaso de Datos.
                //------------------------------------------

                MODGANU.VAnuPl[i].TipDoc = 1;
                MODGANU.VAnuPl[i].TipAnu = (short)VB6Helpers.Val(frmgrev.CB_Tipanu.Text);
                MODGANU.VAnuPl[i].ObsPln = VB6Helpers.Trim(frmgrev.Tx_ObsPln.Text);
                if(!string.IsNullOrEmpty(frmgrev.CB_Tipanu.Text))
                {
                    MODGANU.VAnuPl[i].TipAnu = VB6Helpers.CShort(frmgrev.CB_Tipanu.Text);
                }
                MODGANU.VAnuPl[i].MtoAnu = MODGANU.VAnuPl[i].MtoPln;
                MODGANU.VAnuPl[i].TipCam = Format.StringToDouble(frmgrev.Tx_TipCam.Text);
                MODGANU.VAnuPl[i].Motivo = VB6Helpers.Trim(frmgrev.Tx_Motivo.Text);
                MODGANU.VAnuPl[i].ObsPln = VB6Helpers.Trim(frmgrev.Tx_ObsPln.Text);

                if((short)frmgrev.Cb_Tipo.get_ItemData_(frmgrev.Cb_Tipo.ListIndex) != 6)
                {
                    MODGANU.VAnuPl[i].ApcNum = VB6Helpers.Trim(frmgrev.Tx_NroPln.Text);
                    MODGANU.VAnuPl[i].ApcFec = VB6Helpers.Trim(frmgrev.Tx_Fecha.Text);
                    MODGANU.VAnuPl[i].ApcTip = VB6Helpers.Left(VB6Helpers.Trim(frmgrev.Cb_Tipo.get_List((short)frmgrev.Cb_Tipo.ListIndex)), 2);
                }
                else
                {
                    MODGANU.VAnuPl[i].ApcNum = "";
                    MODGANU.VAnuPl[i].ApcFec = "";
                    MODGANU.VAnuPl[i].ApcTip = "";
                }
                
                MODGANU.VAnuPl[i].PlzBcc = (short)(frmgrev.Cb_Pbc.get_ItemData_(frmgrev.Lt_Pln.ListIndex));
                //------------------------------------------
                //Limpia los campos.
                //------------------------------------------
                frmgrev.Tx_ObsPln.Text = "";
                frmgrev.Co_Boton[0].Enabled = true;
                frmgrev.CB_Tipanu.ListIndex = -1;
                #endregion
            }
            //------------------------------------------
            //Planilla Invisible.
            //------------------------------------------
            else if (_switchVar1 == "INV")
            {
                #region Planilla Invisible
                x = BCH.Comex.Core.BL.XCFT.Modulos.MODGANU.SyGetA_Pli(Num, Convert.ToDateTime (MODGANU.VAnuPl[i].FecPln), initObj, uow); //-->Llamada StoredProcedure.
                if (x != 0)
                {
                    if (!vieneDeMensaje) {
                        if (frmgrev.PopUps.Count <= 0) { 
                        frmgrev.PopUps.Add(new UI_Message() {
                            Type = TipoMensaje.YesNo,
                            Text = "Esta planilla ya ha sido anulada ¿ Desea Continuar ?"
                        });
                        return false;
                    }
                   }
                   else if (resMensaje) { }
                }

                //------------------------------------------
                //Validaciones Correspondientes.
                //------------------------------------------
                short z = (short)frmgrev.Cb_Tipo.get_ItemData_(frmgrev.Cb_Tipo.ListIndex);

                if (frmgrev.Cb_Tipo.ListIndex == -1)
                {
                    frmgrev.Errores.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Atención: Es necesario que se seleccione un Tipo para la Autorización Previa del Banco Central para poder Reversar la Operación.",
                        ControlName = "Cb_Tipo_SelectedValue"
                    });
                    return false;
                }


                if ((short)frmgrev.Cb_Tipo.get_ItemData_(frmgrev.Cb_Tipo.ListIndex) != 6)
                {
                    if (string.IsNullOrWhiteSpace(frmgrev.Tx_NroPln.Text))
                    {
                        frmgrev.Errores.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Atención: Es necesario que ingrese un Nro. de autorización para poder Reversar la Operación.",
                            ControlName = "Numero_Text"
                        });
                        return false;
                    }

                    if (string.IsNullOrWhiteSpace(frmgrev.Tx_Fecha.Text))
                    {
                        frmgrev.Errores.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Atención: Es necesario que ingrese una Fecha de autorización para poder Reversar la Operación.",
                            ControlName = "Fecha_Text"
                        });
                        return false;
                    }

                    if (!string.IsNullOrEmpty(initObj.MODCVDIMMM.FechaHoy))
                    {
                        if (DateTime.Parse(initObj.MODCVDIMMM.FechaHoy).Date < DateTime.Parse(MODGANU.VAnuPl[i].FecPln).Date)
                        {
                            frmgrev.Errores.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "No podrá reversar esta Operación debido a que la fecha original de la planilla es es menor a la fecha recien ingresada.",
                                ControlName = "Fecha_Text"
                            });
                            return false;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(frmgrev.Tx_Motivo.Text))
                    {
                        frmgrev.Errores.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Atención: Es necesario que ingrese un Motivo para poder Reversar la Operación.",
                            ControlName = "Motivo_Text"
                        });
                        return false;
                    }
                }
                

                if (frmgrev.Cb_Pbc.ListIndex == -1)
                {
                    frmgrev.Errores.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Atención: Es necesario que se seleccione una Plaza del Banco Central para poder Reversar la Operación.",
                        ControlName = "Cb_SucursalBCCH_SelectedValue"
                    });
                    return false;
                }

                if (MODGANU.VAnuPl[i].TipPln == T_Mdl_Funciones.TPli_Ingreso || MODGANU.VAnuPl[i].TipPln == T_Mdl_Funciones.TPli_Egreso)
                {
                    if (VB6Helpers.Val(frmgrev.Tx_TipCam.Text) == 0)
                    {
                        frmgrev.Errores.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Atención: Es necesario que ingrese un Tipo de Cambio para poder Reversar la Operación.",
                            ControlName = "Tx_TipCam_Text"
                        });
                        return false;
                    }
                }

                initObj.MODCVDIMMM.FechaHoy = VB6Helpers.Format(frmgrev.Tx_Fecha.Text, "dd/MM/yyyy");
                //------------------------------------------
                //Validar que la fecha ingresada sea mayor o igual a la fecha original.
                //------------------------------------------
                //------------------------------------------
                //Validar que la fecha ingresada sea menor o igual a la fecha de hoy.
                //------------------------------------------


                if(!string.IsNullOrEmpty(initObj.MODCVDIMMM.FechaHoy))
                {
                    if(DateTime.Parse(initObj.MODCVDIMMM.FechaHoy).Date > DateTime.Now.Date)
                    {
                        frmgrev.Errores.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "No podrá reversar esta Operación debido a que la fecha ingresada es menor a la fecha actual.",
                            ControlName = "Fecha_Text"
                        });
                        return false;
                    }
                }
                
                //------------------------------------------
                //Traspaso de Datos.
                //------------------------------------------

                if(!string.IsNullOrEmpty(frmgrev.CB_Tipanu.Text))
                {
                    MODGANU.VAnuPl[i].TipAnu = VB6Helpers.CShort(frmgrev.CB_Tipanu.Text);
                }
                MODGANU.VAnuPl[i].MtoAnu = MODGANU.VAnuPl[i].MtoPln;
                MODGANU.VAnuPl[i].TipCam = Format.StringToDouble(frmgrev.Tx_TipCam.Text);
                MODGANU.VAnuPl[i].Motivo = VB6Helpers.Trim(frmgrev.Tx_Motivo.Text);
                MODGANU.VAnuPl[i].ObsPln = VB6Helpers.Trim(frmgrev.Tx_ObsPln.Text);
                MODGANU.VAnuPl[i].TipDoc = MODGANU.VAnuPl[i].TipPln;

                if ((short)frmgrev.Cb_Tipo.get_ItemData_(frmgrev.Cb_Tipo.ListIndex) != 6)
                {
                    MODGANU.VAnuPl[i].ApcTip = VB6Helpers.Left(VB6Helpers.Trim(frmgrev.Cb_Tipo.get_List((short)frmgrev.Cb_Tipo.ListIndex)), 2);
                    MODGANU.VAnuPl[i].ApcNum = VB6Helpers.Trim(frmgrev.Tx_NroPln.Text);
                    MODGANU.VAnuPl[i].ApcFec = VB6Helpers.Trim(frmgrev.Tx_Fecha.Text);
                }
                else
                {
                    MODGANU.VAnuPl[i].ApcTip = "";
                    MODGANU.VAnuPl[i].ApcNum = "";
                    MODGANU.VAnuPl[i].ApcFec = "";
                }
                
                MODGANU.VAnuPl[i].PlzBcc = (short)(frmgrev.Cb_Pbc.get_ItemData_(frmgrev.Lt_Pln.ListIndex));
               //------------------------------------------
                //Limpia los campos.
                //------------------------------------------
                frmgrev.Tx_ObsPln.Text = "";
                frmgrev.Tx_NroPln.Text = "";
                frmgrev.Tx_Fecha.Text = "";
                frmgrev.Tx_Motivo.Text = "";
                frmgrev.Tx_TipCam.Text = VB6Helpers.Format("0", "0,0000");
                frmgrev.Cb_Pbc.ListIndex = -1;
                frmgrev.Cb_Tipo.ListIndex = -1;
                frmgrev.Co_Boton[0].Enabled = true;
                frmgrev.CB_Tipanu.ListIndex = -1;
                #endregion
            }

            //DDJT
            frmgrev.Co_Boton[0].Enabled = true;
            //DDJT

            //------------------------------------------
            //Coloca el foco en la Planilla o en el botón Aceptar.
            //------------------------------------------
            initObj.MODGCHQ.Indice = (short)frmgrev.Lt_Pln.ListIndex;
            initObj.frmgrev.Lt_Pln.Items[initObj.MODGCHQ.Indice].Value =
                                   initObj.frmgrev.Lt_Pln.Items[initObj.MODGCHQ.Indice].Value + "  " + T_MODGANU.LstMarca;

            if (initObj.MODGCHQ.Indice < frmgrev.Lt_Pln.Items.Count - 1)
            {
                frmgrev.Lt_Pln.ListIndex = initObj.MODGCHQ.Indice + 1;
            }
            else
            {
                frmgrev.Lt_Pln.ListIndex = 0;
            }
            return true;
        }        
        public static void Tx_Fecha_LostFocus(UI_Mdi_Principal Mdi_Principal, UI_Frmgrev frmgrev, InitializationObject initObj , T_MODCVDIMMM MODCVDIMMM)
        {
            double fech = 0;
            short i = 0;
            MODGPYF1.selTexto(initObj.frmgrev.Tx_Fecha.Text);
            T_MODGANU MODGANU = initObj.MODGANU;

            if(!string.IsNullOrWhiteSpace(frmgrev.Tx_Fecha.Text))
            {
                if (~MODGPYF1.EsFecha2000(frmgrev.Tx_Fecha.Text, initObj, T_MODGCVD.MsgCVD) != 0)
                {
                    return;
                }

                fech = MODGPYF1.ValidaFecha(initObj.frmgrev.Tx_Fecha.Text);

                if (fech == 0)
                {
                    Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "Atención: La Fecha ingresada es incorrecta.",
                        Type = TipoMensaje.Informacion,
                        Title = T_MODGCVD.MsgCVD,
                        ControlName = "Fecha_Text"
                    });
                    return;
                }

                frmgrev.Tx_Fecha.Text = VB6Helpers.Format(VB6Helpers.CStr(fech), "dd/MM/yyyy");
                if (frmgrev.Lt_Pln.ListIndex > -1)
                {
                    i = (short)frmgrev.Lt_Pln.get_ItemData_(frmgrev.Lt_Pln.ListIndex);

                    MODCVDIMMM.FechaHoy = VB6Helpers.Format(initObj.frmgrev.Tx_Fecha.Text, "dd/MM/yyyy");
                    //--------------------------
                    //Validar que la fecha ingresada sea mayor o igual a la fecha original.
                    //--------------------------

                    if(!string.IsNullOrEmpty(initObj.MODCVDIMMM.FechaHoy))
                    {
                        if(DateTime.Parse(MODCVDIMMM.FechaHoy).Date < DateTime.Parse(MODGANU.VAnuPl[i].FecPln).Date)
                        {
                            Mdi_Principal.MESSAGES.Add(new UI_Message
                            {
                                Text = "La fecha de autorización debe ser mayor o igual a la fecha de creación de la Planilla.",
                                Type = TipoMensaje.Informacion,
                                Title = T_MODGCVD.MsgCVD,
                                ControlName = "Fecha_Text"
                            });
                            return;
                        }
                    }

                    //--------------------------
                    //Validar que la fecha ingresada sea menor o igual a la fecha de hoy.
                    //--------------------------

                    if(!string.IsNullOrEmpty(initObj.MODCVDIMMM.FechaHoy))
                    {
                        if(DateTime.Parse(MODCVDIMMM.FechaHoy).Date > DateTime.Now.Date)
                        {
                            Mdi_Principal.MESSAGES.Add(new UI_Message
                            {
                                Text = "La fecha de autorización debe ser menor o igual a la fecha actual.",
                                Type = TipoMensaje.Informacion,
                                Title = T_MODGCVD.MsgCVD,
                                ControlName = "Fecha_Text"
                            });
                            return;
                        }
                    }
                }
            }
        }
        public static void Tx_NroOpe_LostFocus(ref short Index, UI_Frmgrev frmgrev, InitializationObject initObj)
         {

            T_MODGPYF0 MODGPYF0 = initObj.MODGPYF0;

             //short a = BCH.Comex.Core.BL.XCFT.Modulos.MODGPYF0.MascaraLost(frmgrev.Tx_NroOpe[Index], MODGPYF0);
             int n = 0;
             if (string.IsNullOrEmpty(frmgrev.Tx_NroOpe[Index].Text))
             {
                 n = 0;
             }
             else
             {
                 n = (int)Format.StringToDouble(frmgrev.Tx_NroOpe[Index].Text);
             }

             switch (Index)
             {
                 case 0:
                    frmgrev.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "000");
                     break;
                 case 1:
                    frmgrev.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "00");
                     break;
                 case 2:
                    frmgrev.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "00");
                     break;
                 case 3:
                    frmgrev.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "000");
                     break;
                 case 4:
                    frmgrev.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "00000");
                     break;
             }

         }
        /// <summary>
        /// Posible Implementación JavaScripts.
        /// </summary>
        /// <param name="Estado"></param>
        public static void Pr_Habilitar_Pantalla(short Estado, UI_Frmgrev frmgrev)
         {
             //-----------------------------
             //Habilitar o Inhabilitar los campos de la pantalla dependiendo del Estado que se envíe.
             //-----------------------------
             
             //frmgrev.Fr_Invisible.Enabled = Estado != 0;
             frmgrev.Ok.Enabled = Estado != 0;
             
            frmgrev.Tx_NroPln.Enabled = Estado != 0;
             frmgrev.Tx_Fecha.Enabled = Estado != 0;
             frmgrev.Cb_Pbc.Enabled = Estado != 0;
             frmgrev.Tx_Motivo.Enabled = Estado != 0;
             frmgrev.Cb_Tipo.Enabled = Estado != 0;
             frmgrev.Tx_TipCam.Enabled = Estado != 0;
             
            frmgrev.Fr_Observaciones.Enabled = Estado != 0;
             frmgrev.BOT_Obs.Enabled = Estado != 0;
             frmgrev.Tx_ObsPln.Enabled = Estado != 0;
             frmgrev.Co_Volver.Enabled = Estado != 0;
             frmgrev.CAM_Tipcam.Enabled = Estado != 0;
             
         }
        public static void Tx_TipCam_LostFocus(UI_Frmgrev frmgrev, InitializationObject initObj)
        {
            initObj.frmgrev.Tx_TipCam.Tag = "________.____";
            short a = MODGPYF0.MascaraLost(initObj.frmgrev.Tx_TipCam ,initObj.MODGPYF0);
            initObj.frmgrev.Tx_TipCam.Text =
                Format.FormatCurrency((MODGPYF1.ValTexto(initObj.MODGPYF0, initObj.frmgrev.Tx_TipCam.Text)), MODGPYF1.DecObjeto(initObj.frmgrev.Tx_TipCam));
        }
        #endregion

        #region METODO PRIVADO
        private static void InhabilitarCampos_Fr_Invisible(InitializationObject initObj, UI_Frmgrev frmgrev)
        {
            #region Autorización previa del Banco Central
            frmgrev.Tx_NroPln.Enabled = false;
            frmgrev.Tx_Fecha.Enabled  = false;
            frmgrev.Cb_Pbc.Enabled    = false;
            frmgrev.Tx_Motivo.Enabled = false;
            frmgrev.Cb_Tipo.Enabled   = false;
            frmgrev.Tx_TipCam.Enabled = false;  
            #endregion
        }
        private static void HabilitarCampos_Fr_Invisible(InitializationObject initObj, UI_Frmgrev frmgrev)
        {
            #region Autorización previa del Banco Central
            frmgrev.Tx_NroPln.Enabled = true;
            frmgrev.Tx_Fecha.Enabled  = true;
            frmgrev.Cb_Pbc.Enabled    = true;
            frmgrev.Tx_Motivo.Enabled = true;
            frmgrev.Cb_Tipo.Enabled   = true;
            frmgrev.Tx_TipCam.Enabled = true;
            #endregion
        }
        private static void HabilitarCampos_Frame3D1(InitializationObject initObj, UI_Frmgrev frmgrev)
        {
            frmgrev.Tx_NroOpe[0].Enabled = true;
            frmgrev.Tx_NroOpe[1].Enabled = true;
            frmgrev.Tx_NroOpe[2].Enabled = true;
            frmgrev.Tx_NroOpe[3].Enabled = true;
            frmgrev.Tx_NroOpe[4].Enabled = true;
            frmgrev.Ok_Operacion.Enabled = true;  
        }
        private static void InhabilitarCampos_Frame3D1(InitializationObject initObj, UI_Frmgrev frmgrev)
        {
            frmgrev.Tx_NroOpe[0].Enabled = false;
            frmgrev.Tx_NroOpe[1].Enabled = false;
            frmgrev.Tx_NroOpe[2].Enabled = false;
            frmgrev.Tx_NroOpe[3].Enabled = false;
            frmgrev.Tx_NroOpe[4].Enabled = false;
            frmgrev.Ok_Operacion.Enabled = false;
        }
        private static void InhabilitarCampos_Frame3D2(InitializationObject initObj, UI_Frmgrev frmgrev)
        {
            #region Planillas
            frmgrev.Lt_Pln.Enabled     = false;
            frmgrev.CB_Tipanu.Enabled  = false;
            frmgrev.CAM_Tipcam.Enabled = false;
            frmgrev.Bot_Dec.Enabled    = false;
            frmgrev.BOT_Obs.Enabled    = false;
            frmgrev.Ok.Enabled         = false;  
            #endregion
        }
        private static void HabilitarCampos_Frame3D2(InitializationObject initObj, UI_Frmgrev frmgrev)
        {
            #region Planillas
            frmgrev.Lt_Pln.Enabled     = true;
            frmgrev.CB_Tipanu.Enabled  = true;
            frmgrev.CAM_Tipcam.Enabled = true;
            frmgrev.BOT_Obs.Enabled    = true;
            frmgrev.Ok.Enabled         = true;  
            #endregion
        }
        public static  void ReversarOperacion_ClearReport(InitializationObject Modulos, UnitOfWorkCext01 uow)
        {
            //-------Fin deshabilitar los botones maximizar y minimizar en un formulario MDI Parent -------
            string ic;
            string ir = "";
            string ip = "";

            //Inicializa Objetos y Variables.
            //Se limpia variable
            //Modulos.Mdl_Funciones_Varias.LC_TRXID_MAN = "";

            Modulos.MODGCVD.BotPrt = 0;
            Modulos.Mdl_Funciones_Varias.CARGA_AUTOMATICA = 0;

            Modulos.Mdl_Funciones_Varias.LC_MONEDA = 0;
            Modulos.Mdl_Funciones_Varias.LC_MONTO = "";
            Modulos.Mdl_Funciones_Varias.LC_XREF = "";
            Modulos.Mdl_Funciones_Varias.LC_CONREFNUM = "";
            Modulos.Mdl_Funciones_Varias.LC_PRD = "";
            Modulos.Mdl_Funciones_Varias.LC_OUTGOING = "";
            Modulos.Mdl_Funciones_Varias.LC_INCOMING = "";
            Modulos.Mdl_Funciones_Varias.LC_ORD_INST1 = "";  //DESTINO FONDOS
            Modulos.Mdl_Funciones_Varias.LC_PMNT_DET1 = "";  //DESTINO FONDOS
            Modulos.Mdl_Funciones_Varias.LC_PMNT_DET2 = "";  //DESTINO FONDOS
            Modulos.Mdl_Funciones_Varias.LC_PMNT_DET3 = "";  //DESTINO FONDOS
            Modulos.Mdl_Funciones_Varias.LC_PMNT_DET4 = "";  //DESTINO FONDOS
            Modulos.Mdl_Funciones_Varias.LC_COD_TRANS = "";  //CODIGO TRANSACCION
            Modulos.Mdl_Funciones_Varias.Lc_BaseNumber = "";
            Modulos.Mdl_Funciones_Varias.LC_BASENUMBER_NUEVO = "";
            Modulos.Mdl_Funciones_Varias.LC_SWFT = "";
            Modulos.Mdl_Funciones_Varias.LC_SWFT = "";
            Modulos.Mdl_Funciones_Varias.LC_BEN_INST1 = "";  //BEN_INST1
            Modulos.Mdl_Funciones_Varias.LC_ULT_BEN1 = "";  //ULT_BEN1
            Modulos.Mdl_Funciones_Varias.LC_ULT_BEN2 = "";  //ULT_BEN2
            Modulos.Mdl_Funciones_Varias.LC_ULT_BEN3 = "";  //ULT_BEN3
            Modulos.Mdl_Funciones_Varias.LC_ULT_BEN4 = "";  //ULT_BEN4
            Modulos.Mdl_Funciones_Varias.LC_CHG_WHOM = "";  //CHG_WHOM
            Modulos.Mdl_Funciones_Varias.LC_FCCFT = "";  //FCCFT
            Modulos.Mdl_Funciones_Varias.LC_DRVALDT = "";  //DRVALDT
            Modulos.Mdl_Funciones_Varias.LC_NOM_MDA = "";  //NOMBRE MONEDA
            Modulos.Mdl_Funciones_Varias.LC_INTRMD1 = "";
            Modulos.Mdl_Funciones_Varias.LC_US_PAY_ID = "";
            Modulos.Mdl_Funciones_Varias.LC_RECVR_CORRES1 = "";
            Modulos.Mdl_Funciones_Varias.LC_RECVR_CORRES2 = "";
            Modulos.Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO1 = "";
            Modulos.Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO2 = "";
            Modulos.Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO3 = "";
            Modulos.Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO4 = "";
            Modulos.Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO5 = "";
            Modulos.Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO6 = "";

            Modulos.Frm_Principal.Tx_NomPrt.Text = "";
            Modulos.Frm_Principal.Tx_RefCli.Text = "";
            Modulos.Frm_Principal.Tx_iva.Text = "";
            Modulos.Frm_Principal.Tx_moneda.Text = "";
            Modulos.Frm_Principal.Tx_MtoOri.Text = "";
            Modulos.Frm_Principal.Tx_neto.Text = "";
            Modulos.Frm_Principal.Tx_NroFac.Text = "";
            Modulos.Frm_Principal.Tx_tipo.Text = "";
            Modulos.Frm_Principal.Lt_CI.Clear();
            Modulos.Frm_Principal.Lt_CVE.Clear();
            Modulos.Frm_Principal.Lt_CVI.Clear();
            Modulos.Frm_Principal.Num_Op.Text = "";

            MODXVIA.Pr_Init_xVia(Modulos.MODXVIA, Modulos.MODGSWF, Modulos.MODGCHQ);
            MODXORI.Pr_Init_xOri(Modulos.MODXORI);



            Modulos.Mdl_Funciones_Varias.V_gCom = new T_gCom[0];
            Modulos.MODGMTA.VVdi = new T_Vdi[1];
            Modulos.MODGMTA.VVdi[0] = new T_Vdi();
            //initObj.MODGASO.VgAsoNul = new T_Aso();
            Modulos.MODGASO.VgAso = Modulos.MODGASO.VgAsoNul.Clone();
            Modulos.MODGCVD.VgPli = new T_gPli[0];
            Modulos.MODGARB.VArb = new T_Arb[0];
            Modulos.MODGPLI1.Vplis = new T_Pli[0];
            Modulos.MODXPLN1.VxPlvs = new T_xPlv[0];
            Modulos.MODXANU.VxAnus = new T_xAnu[0];
            Modulos.MODXPLN0.VxDecP = new T_xDecP[0];
            Modulos.MODGANU.VAnuPl = new T_AnuPl[0];

            Modulos.MODXORI.Vx_SCodTran = new S_Codtran[0];
            Modulos.MODXPLN1.VCom_xPlv = Modulos.MODXPLN1.VCom_xPlvNul;
            Modulos.MODGCVD.VgCVDNul = Modulos.MODGCVD.VgCVDVacia;
            
            //initObj.MODGCVD.VgCvd = initObj.MODGCVD.VgCVDNul;
            Modulos.MODXANU.VgAnu.AnuSin = 0;




            ic = Modulos.Usuario.ConfigImpres_ImprimeCartas;
            if (string.IsNullOrEmpty(ic))
            {
                ic = "-1";
            }

            ir = Modulos.Usuario.ConfigImpres_ImprimeReporte;
            if (string.IsNullOrEmpty(ir))
            {
                ir = "-1";
            }

            ip = Modulos.Usuario.ConfigImpres_ImprimePlanillas;
            if (string.IsNullOrEmpty(ip))
            {
                ip = "-1";
            }

            Modulos.Mdi_Principal.mnu_cartas.Checked = VB6Helpers.CBool(ic);
            Modulos.Mdi_Principal.mnu_conta.Checked = VB6Helpers.CBool(ir);
            Modulos.Mdi_Principal.mnu_planillas.Checked = VB6Helpers.CBool(ip);

           
            //Rango permitido para el ingreso de operaciones
            Modulos.MODGRNG.Rango_Permitido = true;
        }
        #endregion
    }
}
