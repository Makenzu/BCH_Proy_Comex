using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public static class Frm_Chq_Logic
    {
        public static void Co_Aceptar_Click(InitializationObject initObject)
        {
            initObject.MODGCHQ.DocVals.AceptoChq = (short)(true ? -1 : 0);
            initObject.MODGCHQ.VGChq.Acepto = (short)(true ? -1 : 0);

            if ((initObject.MODXVIA.EsSolBcx & (VB6Helpers.Mid(initObject.MODXVIA.BcxEntrada2, 4, 1) == "0" ? -1 : 0)) != 0)
	        {
                BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.CambiaBcxEntrada(initObject, "C");
            }

            if (initObject.MODGCHQ.VGChq.Acepto != 0)
            {
                initObject.MODGCVD.VgCvd.Etapa = MODGPYF0.Componer(initObject.MODGCVD.VgCvd.Etapa, "DVS", "");
                //seteo foco en pantalla principal
                initObject.Mdi_Principal.BUTTONS.Keys.ToList().ForEach(key => { initObject.Mdi_Principal.BUTTONS[key].Focus = (key == "tbr_grabar" ? true : false); });
            }
        }

        public static void Co_Cancelar_Click(InitializationObject initObject, bool respuesta, bool primerLlamada)
        {
            //Verifica si por lo menos un Cheque ha sido generado.
            if (BCH.Comex.Core.BL.XCFT.Modulos.MODGCHQ.Existe_Generado_Chq(initObject) != 0)
            {
                if (!primerLlamada)
                {
                    initObject.Frm_Chq.Confirms.Add(new UI_Message()
                    {
                        Text = "Existen documentos generados que se perderán al cancelar esta ventana. ¿ Desea realmente cancelarla ?",
                        Type = TipoMensaje.YesNo
                    });
                    return;
                }
                else if (respuesta)
                {
                    //SI
                    //Borra todos los Cheques.
                    initObject.MODGCHQ.V_Chq_VVi = new Chq_Vvi[0];
                    initObject.Frm_Chq.respuesta = respuesta; 
                }
                else
                {
                    initObject.MODGCHQ.V_Chq_VVi = new Chq_Vvi[0];
                    initObject.Frm_Chq.respuesta = true; 
                    return;
                }
            }
            else
            {
                //SI
                //Borra todos los Cheques.
                initObject.MODGCHQ.V_Chq_VVi = new Chq_Vvi[0];
                initObject.Frm_Chq.respuesta = true; 
            }
            if (initObject.MODGCHQ.VGChq.Acepto != 0)
            {
                initObject.MODGCVD.VgCvd.Etapa = MODGPYF0.Componer(initObject.MODGCVD.VgCvd.Etapa, "DVS", "");
            }
        }

        public static void Co_Generar_Click(InitializationObject initObject)
        {
            short OtroBenef = 0;
            short EsValeVista = 0;
            short i = 0;
            short Ind_Ben = 0;
            short Ind_Bco = 0;
            string s = "";
            short Ind_Cor = 0;
            short j = 0;
            double q = 0;

            OtroBenef = (short)(false ? -1 : 0);
            EsValeVista = (short)(false ? -1 : 0);

            i = (short)initObject.Frm_Chq.l_montos.ListIndex;
            if (i != -1)
            {
                initObject.MODGCHQ.Indice = Convert.ToInt16(initObject.Frm_Chq.l_montos.get_ItemData(i));
            }
            else
            {
                initObject.MODGCHQ.Indice = 0;
            }

            if (initObject.Frm_Chq.l_benef.ListIndex == initObject.Frm_Chq.l_benef.ListCount - 1)
            {
                OtroBenef = (short)(true ? -1 : 0);
            }
            if (initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].TipoDoc == T_MODGCHQ.DOCVAL_VALVIS)
            {
                EsValeVista = (short)(true ? -1 : 0);
            }

            //Validaciones para emisión de los documentos.
            if ((OtroBenef & (string.IsNullOrWhiteSpace(initObject.Frm_Chq.Tx_Nombre.Text) ? -1 : 0)) != 0)
            {
                //VB6Helpers.MsgBox("Debe Ingresar Nombre del Beneficiario para emitir el Documento.", (MsgBoxStyle)MODGPYF0.pito(48), MODGCHQ.MsgDocVal);
                //initObject.Frm_Chq.Tx_Nombre.Enabled = true;
                ////initObject.Frm_Chq.Tx_Nombre.SetFocus();
                //return;

                initObject.Frm_Chq.Errors.Add(new UI_Message()
                {
                    Text = "Debe Ingresar Nombre del Beneficiario para emitir el Documento.",
                    Type = TipoMensaje.Error,
                    ControlName = "NombreBenef"
                });
                initObject.Frm_Chq.Tx_Nombre.Enabled = true;
                return;
            }

            if ((OtroBenef & EsValeVista & (string.IsNullOrWhiteSpace(initObject.Frm_Chq.Tx_Rut.Text) ? -1 : 0)) != 0)
            {
                //VB6Helpers.MsgBox("Debe Ingresar Rut del Beneficiario para emitir el Vale Vista.", (MsgBoxStyle)MODGPYF0.pito(48), MODGCHQ.MsgDocVal);
                initObject.Frm_Chq.Errors.Add(new UI_Message()
                {
                    Text = "Debe Ingresar Rut del Beneficiario para emitir el Vale Vista.",
                    Type = TipoMensaje.Error,
                    ControlName = "txtRutBeneficiario"
                });
                initObject.Frm_Chq.Tx_Rut.Enabled = true;
                return;
            }

            if (initObject.Frm_Chq.l_plaza.ListIndex == -1)
            {
                //VB6Helpers.MsgBox("Debe Elegir una Plaza de Pago para emitir el Documento.", (MsgBoxStyle)MODGPYF0.pito(16), MODGCHQ.MsgDocVal);
               initObject.Frm_Chq.Errors.Add(new UI_Message()
               {
                   Text = "Debe Elegir una Plaza de Pago para emitir el Documento.",
                   Type = TipoMensaje.Error,
                   ControlName = "l_plaza"
               });
                initObject.Frm_Chq.l_plaza.Enabled = true;
                return;
            }

            //------------------------------------------------------------------------
            //Datos Beneficiario.
            //------------------------------------------------------------------------
            if (initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].TipoDoc == T_MODGCHQ.DOCVAL_CHEQUE)
            {
                Ind_Ben = (short)initObject.Frm_Chq.l_benef.get_ItemData_((short)initObject.Frm_Chq.l_benef.ListIndex);
            }
            else if (initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].TipoDoc == T_MODGCHQ.DOCVAL_VALVIS)
            {
                Ind_Ben = (short)initObject.Frm_Chq.l_benef.get_ItemData_((short)(initObject.Frm_Chq.l_benef.ListCount - 1));
            }

            Ind_Bco = (short)initObject.Frm_Chq.l_plaza.get_ItemData_((short)initObject.Frm_Chq.l_plaza.ListIndex);
            initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].IndBenef = Ind_Ben;
            initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].IndBanco = Ind_Bco;
            initObject.Frm_Chq.Nombre_Ben = VB6Helpers.Trim(initObject.Frm_Chq.Tx_Nombre.Text);
            initObject.Frm_Chq.Rut_Ben = VB6Helpers.Trim(initObject.Frm_Chq.Tx_Rut.Text);
            initObject.Frm_Chq.Nombre_Cli = VB6Helpers.Trim(initObject.Module1.PartysOpe[initObject.MODGCHQ.DocVals.I_Clte].NombreUsado);
           
            if (~EsValeVista != 0)
            {
                //Busca datos del Banco Corresponsal para Cheques.
                s = initObject.Frm_Chq.l_cor.get_List((short)initObject.Frm_Chq.l_cor.ListIndex);
                if (initObject.Frm_Chq.l_cor.ListIndex != -1)
                {
                    Ind_Cor = Convert.ToInt16(initObject.Frm_Chq.l_cor.get_ItemData((short)initObject.Frm_Chq.l_cor.ListIndex));
                    initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].IndCorres = Ind_Cor;
                }
                else {

                    initObject.Frm_Chq.Errors.Add(new UI_Message()
                    {
                       Text = "Antes de generar documentos debe definir Bancos Corresponsales",
                       Type = TipoMensaje.Error,
                       ControlName = "l_cor"
                    });
                    return;
                }
                initObject.Frm_Chq.Swift_Corresponsal = VB6Helpers.Trim(BCH.Comex.Core.BL.XCFT.Modulos.MODGPYF0.copiardestring(s, "\xA0", 1));
                i = Mdl_Funciones.Find_Cor(initObject, initObject.Frm_Chq.Swift_Corresponsal);  //para buscar dir.
                j = BCH.Comex.Core.BL.XCFT.Modulos.MODGCHQ.Find_Nom(initObject, initObject.Frm_Chq.Swift_Corresponsal, initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].CodMon);  //para buscar cta.
                //Datos exclusivos para el Cheque.

                initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].NomPag = initObject.MODGTAB0.VCor[i].Cor_Nom;
                initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].SwfPag = initObject.MODGTAB0.VCor[i].Cor_Swf;
                initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].DirPag = VB6Helpers.UCase(initObject.MODGTAB0.VCor[i].Cor_Dir);
                initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].CiuPag = VB6Helpers.UCase(initObject.MODGTAB0.VCor[i].Cor_Ciu);
                initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].PaiPag = VB6Helpers.UCase(initObject.MODGTAB0.VCor[i].Cor_Pai);
                initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].BcoPag = initObject.MODGTAB0.VNom[j].Nom_Bco;
                initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].NumCta = initObject.MODGTAB0.VNom[j].Nom_cta;
                initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].ChqEmi = initObject.MODGTAB0.VNom[j].Nom_Emi;
                initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].NomCli = initObject.Frm_Chq.Nombre_Cli;
            }
            else
            {
                if (q == -1)
                {
                    //VB6Helpers.MsgBox("No se pudo obtener el Número de Folio del Vale Vista. Reporte este problema inmediatamente y cancele esta operación.", (MsgBoxStyle)MODGPYF0.pito(16), MODGRNG.MsgRng);
                    initObject.Frm_Chq.Errors.Add(new UI_Message()
                    {
                        Text = "No se pudo obtener el Número de Folio del Vale Vista. Reporte este problema inmediatamente y cancele esta operación.",
                        Type = TipoMensaje.Error
                    });
                    return;
                }

                initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].Folio = (int)q;
            }

            //------------------------------------------------------------------------
            //Datos para Cheques y Vales Vistas.
            //------------------------------------------------------------------------
            initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].NroOpe = BCH.Comex.Core.BL.XCFT.Modulos.MODGCHQ.Referencia(initObject);
            initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].SupUsr = initObject.MODGUSR.UsrEsp.Id_Super;
            initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].CCosto = initObject.MODGUSR.UsrEsp.CostoSuper;
            initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].NroPro = initObject.MODGCHQ.DocVals.codpro;
            initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].NomBen = initObject.Frm_Chq.Nombre_Ben;
            initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].RutTom = BCH.Comex.Core.BL.XCFT.Modulos.MODGCHQ.SoloNumeros((initObject.Frm_Chq.Tx_Rut.Text), 10);
            initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].NomTom = initObject.Module1.PartysOpe[initObject.MODGCHQ.ITom].NombreUsado;
            initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].MtoDoc = VB6Helpers.Val(BCH.Comex.Core.BL.XCFT.Modulos.MODGPYF0.unformat(initObject.MODGPYF0, initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].MtoDoc_t));
            initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].NroSiu = BCH.Comex.Core.BL.XCFT.Modulos.MODGPYF0.Siu();
            //------------------------------------------------------------------------
            if (~EsValeVista != 0)
            {
                //Genera Datos del Cheque.
                s = "";
                s = s + BCH.Comex.Core.BL.XCFT.Modulos.MODGCHQ.Referencia(initObject) + VB6Helpers.Chr(9);
                s = s + initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].NemMon + VB6Helpers.Chr(9);
                s = s + initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].MtoDoc_t + VB6Helpers.Chr(9);
                s = s + initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].NomBen + VB6Helpers.Chr(9);
                s = s + initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].NomPag + VB6Helpers.Chr(9);
                s = s + initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].DirPag + VB6Helpers.Chr(9);
                s = s + initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].SwfPag + VB6Helpers.Chr(9);
                s = s + initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].CiuPag + VB6Helpers.Chr(9);
                s = s + initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].PaiPag + VB6Helpers.Chr(9);
                //Siempre tengo que tener cuenta en el Banco a través del cual pago el Cheque.
                j = BCH.Comex.Core.BL.XCFT.Modulos.MODGCHQ.Find_Nom(initObject, initObject.Frm_Chq.Swift_Corresponsal, initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].CodMon);
                s = s + initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].NumCta + VB6Helpers.Chr(9);
                s = s + initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].NomCli + VB6Helpers.Chr(9);
                s = s + initObject.MODGCHQ.DocVals.ProChq + VB6Helpers.Chr(9);
                s = s + VB6Helpers.Mid(BCH.Comex.Core.BL.XCFT.Modulos.MODGCHQ.Referencia(initObject), 6, 2) + VB6Helpers.Chr(9);
            }
            else
            {
                //Genera Datos del Vale Vista.
                s = "";
                s = s + BCH.Comex.Core.BL.XCFT.Modulos.MODGCHQ.Referencia(initObject) + VB6Helpers.Chr(9);
                s = s + initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].MtoDoc_t + VB6Helpers.Chr(9);
                s = s + VB6Helpers.Format(VB6Helpers.CStr(initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].Folio), "000000") + VB6Helpers.Chr(9);
                s = s + initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].NomBen + VB6Helpers.Chr(9);
                s = s + initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].RutTom + VB6Helpers.Chr(9);
            }

            initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].Documento = s;
            initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].Pinchado = (short)(false ? -1 : 0);
            initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].Generado = (short)(true ? -1 : 0);
            initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].FecEmi = DateTime.Now.ToString("dd/MM/yyyy");

            //Se marca el Cheque como ya generado.
            initObject.Frm_Chq.l_montos.Items[initObject.MODGCHQ.Indice].SetValue("Generado", "SI");

            initObject.Frm_Chq.Co_Generar.Enabled = false;
            initObject.Frm_Chq.l_cor.Enabled = true;

            //descomentado por jzp 26/01/2010
            //-------------------------------
            if (initObject.Frm_Chq.l_montos.ListIndex != initObject.Frm_Chq.l_montos.ListCount - 1)
            {
                initObject.Frm_Chq.l_montos.ListIndex++;
                L_Montos_Click(initObject); 
            }
            else
            {
                initObject.Frm_Chq.l_montos.ListIndex = 0;
            }

            BCH.Comex.Core.BL.XCFT.Modulos.MODGCHQ.Pr_No_Generado_Chq(initObject, initObject.Frm_Chq.l_montos);
            if (BCH.Comex.Core.BL.XCFT.Modulos.MODGCHQ.Todos_Cheq_Generados(initObject) != 0)
            {
                initObject.Frm_Chq.Co_Aceptar.Enabled = true;
            }
        }

        public static void Form_Activate(InitializationObject initObject)
        {
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            short i = 0;
            short posbcx = 0;
            //Si se ingresa a esta pantalla cuando ya se ha generado un cheque
            //se verifica esto y se envia el foco al botón Aceptar.
            if ((MODXVIA.EsSolBcx & (VB6Helpers.Mid(MODXVIA.BcxEntrada2, 4, 1) == "0" ? -1 : 0) & (initObject.Frm_Chq.EsBcx == 1 ? -1 : 0)) != 0)
            {
                initObject.Frm_Chq.EsBcx = 0;
                posbcx = 283;
                for (i = 0; i <= (short)(VB6Helpers.Val(VB6Helpers.Mid(MODXVIA.VBcxCci2, 280, 2)) - 1); i++)
                {
                    initObject.Frm_Chq.l_montos.ListIndex = i;

                    initObject.Frm_Chq.Tx_Nombre.Text = VB6Helpers.Trim(VB6Helpers.Mid(MODXVIA.VBcxCci2, posbcx, 50));
                    if(!string.IsNullOrEmpty(initObject.Frm_Chq.Tx_Nombre.Text))
                    {
                        Co_Generar_Click(initObject);
                    }

                    posbcx = (short)(posbcx + 68);
                }

                if (BCH.Comex.Core.BL.XCFT.Modulos.MODGCHQ.Todos_Cheq_Generados(initObject) != 0)
                {
                    initObject.Frm_Chq.Co_Aceptar.Enabled = true;
                }
            }

            if (initObject.Frm_Chq.Co_Aceptar.Enabled == true)
            {
                //Co_Aceptar.SetFocus();
            }

        }

        public static void Form_Load(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            short[] Tabs = null;
            short[] tabs1 = null;
            short x = 0;
            short a = 0;
            short i = 0;
            short n = 0;

            initObject.Frm_Chq.EsBcx = 0;
            initObject.Frm_Chq.Label1.Text = "Moneda                 Monto          Documento       Generado";
            initObject.Frm_Chq.Label2.Text = "Beneficiario";
            initObject.Frm_Chq.Label3.Text = "Plaza de Pago";
            initObject.Frm_Chq.Lb_Nombre.Text = "Nombre";
            initObject.Frm_Chq.Lb_rut.Text = "Rut";
            initObject.Frm_Chq.Lb_Corresponsal.Text = "Bancos Corresponsales";
            initObject.MODGCHQ.VGChq.Acepto = (short)(false ? -1 : 0);
            initObject.Frm_Chq.En_Load = (short)(true ? -1 : 0);
            initObject.Frm_Chq.l_montos.Header = new List<string>() { "Moneda", "Monto", "Documento", "Generado" };

            //Carga los países.
            MODGTAB0.CargaEnLista_Pai(initObject.MODGTAB0, initObject.Frm_Chq.l_plaza, unit);
            //-----------------------------------
            //Se setea el tabulador para Montos.
            //-----------------------------------
            x = 3;
            Tabs = new short[x + 1];
            Tabs[0] = 0;
            Tabs[1] = 20;
            Tabs[2] = 108;
            Tabs[3] = 165;
            a = MODGPYF0.seteatabulador(initObject.Frm_Chq.l_montos, Tabs);
            //-------------------------------------------
            //Se setea el tabulador para Corresponsales.
            //-------------------------------------------jua
            x = 3;
            tabs1 = new short[x + 1];
            tabs1[0] = 0;
            tabs1[1] = 70;
            a = MODGPYF0.seteatabulador(initObject.Frm_Chq.l_cor, tabs1);
            
            //------------------------------------------------------------------------
            //Si no se ha entrado al Cheque/VV, se cargan los valores.
            //------------------------------------------------------------------------

            for (i = 0; i <= (short)VB6Helpers.UBound(initObject.MODGCHQ.V_Chq_VVi); i++)
            {
                if (initObject.MODGCHQ.V_Chq_VVi[i].TipoDoc == T_MODGCHQ.DOCVAL_VALVIS)
                {
                    initObject.MODGCHQ.V_Chq_VVi[i].TipoDoc_t = "Vale Vista";
                }
                else
                {
                    initObject.MODGCHQ.V_Chq_VVi[i].TipoDoc_t = "Cheque";
                }

                n = MODGCHQ.Find_Mnd(initObject, initObject.MODGCHQ.V_Chq_VVi[i].CodMon);
                initObject.MODGCHQ.V_Chq_VVi[i].NemMon = VB6Helpers.Trim(initObject.MODGTAB0.VMnd[n].Mnd_MndNmc);
                initObject.MODGCHQ.V_Chq_VVi[i].MtoDoc_t = MODGPYF0.forma(initObject.MODGCHQ.V_Chq_VVi[i].MtoDoc, T_MODGCHQ.FormatoChq);
            }

            initObject.Frm_Chq.Co_Aceptar.Enabled = false;
            //------------------------------------------------------------------------
            //Carga la lista de Montos de los Documentos.
            //------------------------------------------------------------------------
            initObject.Frm_Chq.l_montos.Clear();
            Pr_Cargar_Montos(initObject);

            //------------------------------------------------------------------------
            //Limpia la lista de Participantes.-
            //------------------------------------------------------------------------
            initObject.Frm_Chq.l_benef.Clear();
            //------------------------------------------------------------------------
            //Carga la lista de Participantes.-
            //------------------------------------------------------------------------

            for (i = 0; i <= (short)VB6Helpers.UBound(initObject.MODGCHQ.BenDocVal); i++)
            {
                if (string.IsNullOrEmpty(initObject.MODGCHQ.BenDocVal[i].FunBen) && 
                    !string.IsNullOrEmpty(initObject.MODGCHQ.BenDocVal[i].NomBen))
                {
                    if (i == T_MODGCVD.ICli)
                    {
                        initObject.MODGCHQ.BenDocVal[i].FunBen = "Cliente";
                    }
                    else if (i == T_MODGCVD.IOtr)
                    {
                        initObject.MODGCHQ.BenDocVal[i].FunBen = "Otro";
                    }
                    else if (i == T_MODGCVD.IOtr)
                    {
                        initObject.MODGCHQ.BenDocVal[i].FunBen = "Comprador";
                    }
                }
                
                if (!string.IsNullOrEmpty(initObject.MODGCHQ.BenDocVal[i].FunBen))
                {
                    initObject.Frm_Chq.l_benef.AddItem(i, initObject.MODGCHQ.BenDocVal[i].FunBen);
                }
            }

            //------------------------------------------------------------------------
            initObject.Frm_Chq.En_Load = (short)(false ? -1 : 0);
            initObject.Frm_Chq.l_benef.ListIndex = initObject.Frm_Chq.l_benef.ListCount - 1;  //el último.-
            l_benef_Click(initObject);

            initObject.Frm_Chq.l_montos.ListIndex = 0;
            L_Montos_Click(initObject);
            if ((initObject.MODXVIA.EsSolBcx & (VB6Helpers.Mid(initObject.MODXVIA.BcxEntrada2, 4, 1) == "0" ? -1 : 0)) != 0)
            {
                initObject.Frm_Chq.EsBcx = 1;
            }

            if (BCH.Comex.Core.BL.XCFT.Modulos.MODGCHQ.Todos_Cheq_Generados(initObject) != 0)
            {
                initObject.Frm_Chq.Co_Aceptar.Enabled = true;
            }
        }

        //Retorna el índice de un Monto seleccionado con doble click.-
        public static short Indice_Pinchado(InitializationObject initObject)
        {
            short _retValue = 0;
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;

            _retValue = -1;
            for (i = 0; i <= (short)VB6Helpers.UBound(initObject.MODGCHQ.V_Chq_VVi); i++)
            {
                if (initObject.MODGCHQ.V_Chq_VVi[i].Pinchado != 0)
                {
                    _retValue = i;
                    break;
                }

            }

            return _retValue;
        }

        public static void Inicializa_Objetos(InitializationObject initObject)
        {

            initObject.Frm_Chq.En_Load = (short)(true ? -1 : 0);

            initObject.Frm_Chq.l_cor.Clear();
            initObject.Frm_Chq.l_benef.ListIndex = -1;
            l_benef_Click(initObject);
            initObject.Frm_Chq.l_plaza.ListIndex = -1;
            l_plaza_Click(initObject);
            initObject.Frm_Chq.l_benef.Enabled = false;
            initObject.Frm_Chq.l_plaza.Enabled = false;
            initObject.Frm_Chq.Tx_Nombre.Enabled = false;
            initObject.Frm_Chq.Tx_Rut.Enabled = false;
            initObject.Frm_Chq.Tx_Nombre.Text = "";
            initObject.Frm_Chq.Tx_Rut.Text = "";

            initObject.Frm_Chq.En_Load = (short)(false ? -1 : 0);
        }

        public static void l_benef_Click(InitializationObject initObject)
        {
            T_MODGCHQ MODGCHQ = initObject.MODGCHQ;
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;
            // UPGRADE_INFO (#05B1): The 'indice_posNew' variable wasn't declared explicitly.
            short indice_posNew = 0;

            i = (short)initObject.Frm_Chq.l_montos.ListIndex;
            if (i != -1)
            {
                MODGCHQ.Indice = Convert.ToInt16(initObject.Frm_Chq.l_montos.get_ItemData(i));
            }
            else
            {
                MODGCHQ.Indice = 0;
            }

            if (MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].Generado != 0)
            {
                string _switchVar1 = MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].TipoDoc;
                if (_switchVar1 == T_MODGCHQ.DOCVAL_CHEQUE)
                {
                    initObject.Frm_Chq.Tx_Rut.Enabled = false;
                    initObject.Frm_Chq.l_plaza.Enabled = true;
                    initObject.Frm_Chq.l_cor.Enabled = true;
                    initObject.Frm_Chq.l_plaza.ListIndex = MODGPYF0.PosLista(initObject.Frm_Chq.l_plaza, MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].IndBanco);
                    initObject.Frm_Chq.l_cor.ListIndex = MODGPYF0.PosListBox(initObject.Frm_Chq.l_cor, MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].IndCorres);
                    initObject.Frm_Chq.l_cor.SelectedValue = initObject.Frm_Chq.l_cor.Items[initObject.Frm_Chq.l_cor.ListIndex].Data;

                    indice_posNew = MODGPYF0.PosLista(initObject.Frm_Chq.l_benef, MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].IndBenef);
                    if (indice_posNew == initObject.Frm_Chq.l_benef.ListIndex)
                    {
                        initObject.Frm_Chq.l_benef.ListIndex = MODGPYF0.PosLista(initObject.Frm_Chq.l_benef, MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].IndBenef);
                        initObject.Frm_Chq.Tx_Nombre.Text = VB6Helpers.Trim(VB6Helpers.UCase(MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].NomBen));
                    }
                    else
                    {
                        i = (short)initObject.Frm_Chq.l_benef.get_ItemData_((short)initObject.Frm_Chq.l_benef.ListIndex);
                        initObject.Frm_Chq.Tx_Nombre.Text = VB6Helpers.Trim(VB6Helpers.UCase(MODGCHQ.BenDocVal[i].NomBen));
                    }

                }
                else if (_switchVar1 == T_MODGCHQ.DOCVAL_VALVIS)
                {
                    initObject.Frm_Chq.Tx_Rut.Enabled = true;
                    initObject.Frm_Chq.l_plaza.ListIndex = MODGPYF0.PosLista(initObject.Frm_Chq.l_plaza, 997);
                    initObject.Frm_Chq.l_plaza.Enabled = false;
                    initObject.Frm_Chq.l_cor.Enabled = false;
                    indice_posNew = MODGPYF0.PosLista(initObject.Frm_Chq.l_benef, MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].IndBenef);
                    if (indice_posNew == initObject.Frm_Chq.l_benef.ListIndex)
                    {
                        initObject.Frm_Chq.l_benef.ListIndex = MODGPYF0.PosLista(initObject.Frm_Chq.l_benef, MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].IndBenef);
                        initObject.Frm_Chq.Tx_Nombre.Text = VB6Helpers.Trim(VB6Helpers.UCase(MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].NomBen));
                        initObject.Frm_Chq.Tx_Rut.Text = VB6Helpers.Trim(VB6Helpers.UCase(MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].RutTom));
                    }
                    else
                    {
                        i = (short)initObject.Frm_Chq.l_benef.get_ItemData_((short)initObject.Frm_Chq.l_benef.ListIndex);
                        initObject.Frm_Chq.Tx_Nombre.Text = VB6Helpers.Trim(VB6Helpers.UCase(MODGCHQ.BenDocVal[i].NomBen));
                        initObject.Frm_Chq.Tx_Rut.Text = VB6Helpers.Trim(VB6Helpers.UCase(MODGCHQ.BenDocVal[i].RutTom));
                    }

                }

            }
            else
            {
                i = (short)initObject.Frm_Chq.l_benef.get_ItemData_((short)initObject.Frm_Chq.l_benef.ListIndex);
                initObject.Frm_Chq.Tx_Nombre.Text = VB6Helpers.Trim(VB6Helpers.UCase(MODGCHQ.BenDocVal[i].NomBen));
                string _switchVar2 = MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].TipoDoc;
                if (_switchVar2 == T_MODGCHQ.DOCVAL_CHEQUE)
                {
                    initObject.Frm_Chq.Tx_Rut.Enabled = false;
                    initObject.Frm_Chq.l_plaza.Enabled = true;
                    initObject.Frm_Chq.l_cor.Enabled = true;
                }
                else if (_switchVar2 == T_MODGCHQ.DOCVAL_VALVIS)
                {
                    initObject.Frm_Chq.Tx_Rut.Enabled = true;
                    initObject.Frm_Chq.l_plaza.Enabled = false;
                    initObject.Frm_Chq.l_cor.Enabled = false;
                    initObject.Frm_Chq.Tx_Rut.Text = VB6Helpers.Trim(VB6Helpers.UCase(MODGCHQ.BenDocVal[i].RutTom));
                }

            }

        } 

        public static void L_Montos_Click(InitializationObject initObject)
        {
            short i = 0;
            initObject.Frm_Chq.Tx_Rut.Enabled = false;
            i = short.Parse(initObject.Frm_Chq.l_montos.Items.ElementAt(initObject.Frm_Chq.l_montos.ListIndex).ID);

            if (i != -1)
            {
                initObject.MODGCHQ.Indice = Convert.ToInt16(initObject.Frm_Chq.l_montos.get_ItemData(i));
            }
            else
            {
                initObject.MODGCHQ.Indice = 0;
            }

            if (initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].Generado != 0)
            {
                string _switchVar1 = initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].TipoDoc;
                if (_switchVar1 == T_MODGCHQ.DOCVAL_CHEQUE)
                {
                    initObject.Frm_Chq.Tx_Rut.Enabled = false;
                    initObject.Frm_Chq.l_plaza.Enabled = true;
                    initObject.Frm_Chq.l_cor.Enabled = true;
                    initObject.Frm_Chq.l_plaza.ListIndex = MODGPYF0.PosLista(initObject.Frm_Chq.l_plaza, initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].IndBanco);
                    initObject.Frm_Chq.l_cor.ListIndex = MODGPYF0.PosListBox(initObject.Frm_Chq.l_cor, initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].IndCorres);
                    initObject.Frm_Chq.l_cor.SelectedValue = initObject.Frm_Chq.l_cor.Items[initObject.Frm_Chq.l_cor.ListIndex].Data;
                }
                else if (_switchVar1 == T_MODGCHQ.DOCVAL_VALVIS)
                {
                    initObject.Frm_Chq.Tx_Rut.Enabled = true;
                    initObject.Frm_Chq.l_plaza.ListIndex = MODGPYF0.PosLista(initObject.Frm_Chq.l_plaza, 997);
                    initObject.Frm_Chq.l_plaza.Enabled = false;
                    initObject.Frm_Chq.l_cor.Clear();
                    initObject.Frm_Chq.l_cor.Enabled = false;
                }
                initObject.Frm_Chq.l_benef.ListIndex = MODGPYF0.PosLista(initObject.Frm_Chq.l_benef, initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].IndBenef);
                l_benef_Click(initObject);
                initObject.Frm_Chq.Tx_Nombre.Enabled = true;
                initObject.Frm_Chq.Tx_Nombre.Text = VB6Helpers.Trim(VB6Helpers.UCase(initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].NomBen));
                initObject.Frm_Chq.Tx_Rut.Text = VB6Helpers.Trim(VB6Helpers.UCase(initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].RutTom));
            }
            else
            {
                initObject.Frm_Chq.l_plaza.ListIndex = -1;
                l_plaza_Click(initObject);
                initObject.Frm_Chq.l_cor.Clear();
                initObject.Frm_Chq.Tx_Nombre.Text = "";
                initObject.Frm_Chq.Tx_Nombre.Enabled = true;
                i = (short)initObject.Frm_Chq.l_benef.get_ItemData_((short)initObject.Frm_Chq.l_benef.ListIndex);
                initObject.Frm_Chq.Tx_Nombre.Text = VB6Helpers.Trim(VB6Helpers.UCase(initObject.MODGCHQ.BenDocVal[i].NomBen));
                
                string _switchVar2 = initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].TipoDoc;
                if (_switchVar2 == T_MODGCHQ.DOCVAL_CHEQUE)
                {
                    initObject.Frm_Chq.Tx_Rut.Enabled = false;
                    initObject.Frm_Chq.l_plaza.Enabled = true;
                    initObject.Frm_Chq.l_plaza.ListIndex = MODGPYF0.PosLista(initObject.Frm_Chq.l_plaza, (BCH.Comex.Core.BL.XCFT.Modulos.MODGCHQ.Fn_GetPai(initObject.MODGCHQ.V_Chq_VVi[initObject.MODGCHQ.Indice].CodMon)));
                    l_plaza_Click(initObject);
                    initObject.Frm_Chq.l_cor.Enabled = true;
                }
                else if (_switchVar2 == T_MODGCHQ.DOCVAL_VALVIS)
                {
                    initObject.Frm_Chq.Tx_Rut.Enabled = true;
                    initObject.Frm_Chq.l_plaza.ListIndex = MODGPYF0.PosLista(initObject.Frm_Chq.l_plaza, 997);
                    l_plaza_Click(initObject);
                    initObject.Frm_Chq.Tx_Rut.Text = VB6Helpers.Trim(VB6Helpers.UCase(initObject.MODGCHQ.BenDocVal[i].RutTom));
                    initObject.Frm_Chq.l_plaza.Enabled = false;
                    initObject.Frm_Chq.l_cor.Clear();
                    initObject.Frm_Chq.l_cor.Enabled = false;
                }
            }
        }

        public static void l_montos_DblClick(InitializationObject initObject)
        {
            T_MODGCHQ MODGCHQ = initObject.MODGCHQ;
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;
            // UPGRADE_INFO (#05B1): The 'k' variable wasn't declared explicitly.
            short k = 0;

            //Determina si un Monto ya ha sido pinchado (Pincha = DblClick).-
            i = Indice_Pinchado(initObject);
            if (i != -1)
            {
                //Está pinchando el que ya tiene pinchado.-
                if (i == initObject.Frm_Chq.l_montos.ListIndex + 1)
                {
                    //initObject.Frm_Chq.l_benef.SetFocus();
                    return;
                }

                //Está pinchando uno <> del que tiene pinchado => Despincha y Pincha.-
                MODGCHQ.V_Chq_VVi[i].Pinchado = (short)(false ? -1 : 0);
            }

            //Verifica que Monto que estoy pinchando no haya sido generado.-
            k = (short)(initObject.Frm_Chq.l_montos.ListIndex);
            //Esta pinchando por primera vez este Documento.-
            Pincha(initObject);
            // initObject.Frm_Chq.l_benef.SetFocus();
        }

        public static void l_montos_KeyPress(InitializationObject initObject, ref short KeyAscii)
        {

            //if (KeyAscii == 13)
            //{
            //    KeyAscii = 0;
            l_montos_DblClick(initObject);
            //}

        }

        public static void l_plaza_Click(InitializationObject initObject)
        {
            T_MODGCHQ MODGCHQ = initObject.MODGCHQ;
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;

            initObject.Frm_Chq.Co_Generar.Enabled = true;

            i = (short)initObject.Frm_Chq.l_montos.ListIndex;
            if (i != -1)
            {
                MODGCHQ.Indice = Convert.ToInt16(initObject.Frm_Chq.l_montos.get_ItemData(i));
            }
            else
            {
                MODGCHQ.Indice = 0;
            }

            i = (short)(initObject.Frm_Chq.l_montos.ListIndex);
            if (MODGCHQ.V_Chq_VVi[i].TipoDoc == T_MODGCHQ.DOCVAL_VALVIS)
            {
                return;
            }

            initObject.Frm_Chq.l_cor.Clear();
            if (initObject.Frm_Chq.l_plaza.ListIndex != -1)
            {
                if (MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].TipoDoc == "VV")
                {
                    return;
                }
                //Determia si al Beneficiario le estoy pagando en su país.
                initObject.Frm_Chq.Plaza_Pago = (short)initObject.Frm_Chq.l_plaza.get_ItemData_((short)initObject.Frm_Chq.l_plaza.ListIndex);
                Pr_Carga_Nomina(initObject);
            }

            if (initObject.Frm_Chq.l_cor.ListIndex == -1)
            {
                initObject.Frm_Chq.Co_Generar.Enabled = false;
            }

        }

        /*public static void l_plaza_LostFocus(InitializationObject initObject)
        {
            T_MODGCHQ MODGCHQ = initObject.MODGCHQ;
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;

            if (initObject.Frm_Chq.En_Load != 0)
            {
                return;
            }
            if (initObject.Frm_Chq.l_plaza.ListIndex == -1)
            {
                return;
            }

            i = (short)initObject.Frm_Chq.l_montos.ListIndex;
            if (i != -1)
            {
                MODGCHQ.Indice = Convert.ToInt16(initObject.Frm_Chq.l_montos.get_ItemData(i));
            }
            else
            {
                MODGCHQ.Indice = 0;
            }

            if (MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].TipoDoc == "VV")
            {
                return;
            }

            //Determia si al Beneficiario le estoy pagando en su país.
            initObject.Frm_Chq.Plaza_Pago = (short)initObject.Frm_Chq.l_plaza.get_ItemData_((short)initObject.Frm_Chq.l_plaza.ListIndex);
        }*/

        //Asigna una linea en la lista de Cheques/ValesVistas.
        private static void LineaChq(InitializationObject initObject, short Indice, UI_Grid Lista)
        {
            T_MODGCHQ MODGCHQ = initObject.MODGCHQ;
            short i = 0;
            string s = "";
            i = (short)(Indice);
            s = MODGCHQ.V_Chq_VVi[i].NemMon + VB6Helpers.Chr(9) + MODGCHQ.V_Chq_VVi[i].MtoDoc_t + VB6Helpers.Chr(9) + MODGCHQ.V_Chq_VVi[i].TipoDoc_t + VB6Helpers.Chr(9) + "SI";

            if (Indice > Lista.ListCount)
            {
                var gridItem = new UI_GridItem();
                gridItem.AddColumn("Moneda", MODGCHQ.V_Chq_VVi[i].NemMon);
                gridItem.AddColumn("Monto", MODGCHQ.V_Chq_VVi[i].MtoDoc_t);
                gridItem.AddColumn("Documento", MODGCHQ.V_Chq_VVi[i].TipoDoc_t);
                gridItem.AddColumn("Generado", "SI");
                gridItem.ID = i.ToString();
                initObject.Frm_Chq.l_montos.Items.Add(gridItem);
            }
            else
            {
             //   initObject.Frm_Chq.l_montos.set_List(Indice, s);
                UI_GridItem item = initObject.Frm_Chq.l_montos.Items[Indice];
                //item.Value = s; TODO SERGIO
            }

        }

        private static void Pincha(InitializationObject initObject)
        {
            T_MODGCHQ MODGCHQ = initObject.MODGCHQ;
            MODGCHQ.Indice = (short)(initObject.Frm_Chq.l_montos.ListIndex);
            MODGCHQ.V_Chq_VVi[MODGCHQ.Indice].Pinchado = (short)(true ? -1 : 0);
        }

        private static void Pr_Carga_Nomina(InitializationObject initObject)
        {
            T_MODGCHQ MODGCHQ = initObject.MODGCHQ;
            // UPGRADE_INFO (#05B1): The 'p' variable wasn't declared explicitly.
            short p = 0;
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;
            // UPGRADE_INFO (#05B1): The 'n' variable wasn't declared explicitly.
            short n = 0;

            // UPGRADE_WARNING (#80F4): The Screen6.MousePointer property sets or returns the MousePointer property of the active form, but only if it's a VB6Form.
            //VB6Project.Screen.MousePointer = (VBRUN.MousePointerConstants)11;
            p = initObject.Frm_Chq.Plaza_Pago;

            i = (short)(initObject.Frm_Chq.l_montos.ListIndex);
            initObject.Frm_Chq.l_cor.Clear();

            //Busca en Nómina, sólo los Bancos Activos y no Aladi.-
            dynamic _tempVar1 = false;            
            n = Mdl_Funciones.Filtra_Cor(initObject, p, MODGCHQ.V_Chq_VVi[i].CodMon, ref _tempVar1, 0, initObject.Frm_Chq.l_cor);
            if (MODGCHQ.V_Chq_VVi[Convert.ToInt32(initObject.Frm_Chq.l_montos.get_ItemData((short)initObject.Frm_Chq.l_montos.ListIndex))].Generado != 0)
            {
                initObject.Frm_Chq.l_cor.ListIndex = MODGPYF0.PosListBox(initObject.Frm_Chq.l_cor, MODGCHQ.V_Chq_VVi[Convert.ToInt32(initObject.Frm_Chq.l_montos.get_ItemData((short)initObject.Frm_Chq.l_montos.ListIndex))].IndCorres);
                if (initObject.Frm_Chq.l_cor.ListIndex!=-1)
                initObject.Frm_Chq.l_cor.SelectedValue = initObject.Frm_Chq.l_cor.Items[initObject.Frm_Chq.l_cor.ListIndex].Data;
            }
            else
            {
                if (initObject.Frm_Chq.l_cor.ListCount > 0)
                {
                    initObject.Frm_Chq.l_cor.ListIndex = MODGPYF0.PosListBox(initObject.Frm_Chq.l_cor, n);
                    
                    if (initObject.Frm_Chq.l_cor.ListIndex == -1)
                    {
                        initObject.Frm_Chq.l_cor.ListIndex = 0;
                        initObject.Frm_Chq.l_cor.SelectedValue = null;
                    }
                    else
                    {
                        initObject.Frm_Chq.l_cor.SelectedValue = initObject.Frm_Chq.l_cor.Items[initObject.Frm_Chq.l_cor.ListIndex].Data;
                    }
                }

            }

            // UPGRADE_WARNING (#80F4): The Screen6.MousePointer property sets or returns the MousePointer property of the active form, but only if it's a VB6Form.
            //VB6Project.Screen.MousePointer = 0;
        }

        private static void Pr_Cargar_Montos(InitializationObject initObject)
        {
            short i = 0;
            string s = "";
            string Generado = "";

            for (i = 0; i <= (short)VB6Helpers.UBound(initObject.MODGCHQ.V_Chq_VVi); i++)
                {
                s = initObject.MODGCHQ.V_Chq_VVi[i].NemMon + VB6Helpers.Chr(9);
                s = s + initObject.MODGCHQ.V_Chq_VVi[i].MtoDoc_t + VB6Helpers.Chr(9);
                s = s + initObject.MODGCHQ.V_Chq_VVi[i].TipoDoc_t + VB6Helpers.Chr(9);
                Generado = initObject.MODGCHQ.V_Chq_VVi[i].Generado != 0 ? "SI" : "NO";
                s += Generado;

                var gridItem = new UI_GridItem();
                gridItem.AddColumn("Moneda", initObject.MODGCHQ.V_Chq_VVi[i].NemMon);
                gridItem.AddColumn("Monto", initObject.MODGCHQ.V_Chq_VVi[i].MtoDoc_t);
                gridItem.AddColumn("Documento", initObject.MODGCHQ.V_Chq_VVi[i].TipoDoc_t);
                gridItem.AddColumn("Generado", Generado);
                gridItem.ID = i.ToString();
                initObject.Frm_Chq.l_montos.Items.Add(gridItem);
            }
        }
    }
}
