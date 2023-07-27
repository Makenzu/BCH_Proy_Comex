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
    public static class Frmgnota
    {
        public static dynamic Nota(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            initObj.MODCVDIM.VNotaCreGl.NumFac = "0";
            initObj.MODCVDIM.VNotaCreGl.tipofac = "";
            initObj.MODCVDIM.VNotaCreGl.Moneda = "";
            initObj.MODCVDIM.VNotaCreGl.netofac = "0";
            initObj.MODCVDIM.VNotaCreGl.ivafac = "0";
            initObj.MODCVDIM.VNotaCreGl.monto = "0";

            initObj.MODGASO.VgAso.OpeSin = "";
            initObj.MODGASO.VgAso.PrtCli="";
            initObj.MODGASO.VgAso.IndNom_t="";
            initObj.MODGASO.VgAso.IndDir_t="";
            //----------------------------------------
            //Realsystems-Código Nuevo-Inicio
            //Fecha Modificación 20100615
            //Responsable: Pablo Millan
            //Versión: 1.0
            //Descripción : Se genera Transaction ID
            //----------------------------------------
            if (string.IsNullOrEmpty(initObj.Mdl_Funciones_Varias.LC_TRXID_MAN))
            {
                initObj.Mdl_Funciones_Varias.LC_TRXID_MAN = MODGCVD.GeneraTRXID(initObj.MODGCVD.VgCvd.OpeSin, uow, initObj.Mdi_Principal.MESSAGES);
            }
            
            return null;
        }

        public static void Form_Load(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            //MZ

            if (initObj.MODCVDIM.Gvar_NotaCredito == 1)
            {
                //FrmgAso.DefInstance.Caption = "Nota de Crédito";
                //FrmgAso.DefInstance.BackColor = VB6Helpers.FromOleColor(unchecked((int)0x8000000F));
            }

            if (initObj.MODCVDIM.Gvar_NotaCredito == 0)
            {
                //FrmgAso.DefInstance.Move(2335, 1995, 6120, 2835);
            }
            // FIN MZ
            //Modificar para el ingreso de otros Productos.
            Pr_Cargar_Productos(initObj);

            //Carga los datos inciales.-
            if (!string.IsNullOrEmpty(initObj.MODGASO.VgAso.OpeSin))
            {
                initObj.Frmgnota.Tx_NumOpe[0].Text = VB6Helpers.Mid(initObj.MODGASO.VgAso.OpeSin, 1, 3);
                initObj.Frmgnota.Tx_NumOpe[1].Text = VB6Helpers.Mid(initObj.MODGASO.VgAso.OpeSin, 4, 2);
                initObj.Frmgnota.Tx_NumOpe[2].Text = VB6Helpers.Mid(initObj.MODGASO.VgAso.OpeSin, 6, 2);
                initObj.Frmgnota.Tx_NumOpe[3].Text = VB6Helpers.Mid(initObj.MODGASO.VgAso.OpeSin, 8, 3);
                initObj.Frmgnota.Tx_NumOpe[4].Text = VB6Helpers.Mid(initObj.MODGASO.VgAso.OpeSin, 11, 5);
                initObj.Frmgnota.Tx_NumOpe[5].Text = VB6Helpers.Mid(initObj.MODGASO.VgAso.OpeSin, 16, 2);
                initObj.Frmgnota.Tx_NumOpe[6].Text = VB6Helpers.Mid(initObj.MODGASO.VgAso.OpeSin, 18, 2);
                initObj.Frmgnota.Tx_RutPrt.Text = MODGPYF0.Componer(initObj.MODGASO.VgAso.PrtCli, "~", "");
                initObj.Frmgnota.Tx_NomPrt.Text = initObj.MODGASO.VgAso.IndNom_t;
                initObj.Frmgnota.Tx_DirPrt.Text = initObj.MODGASO.VgAso.IndDir_t;
            }
            else
            {
                initObj.Frmgnota.Tx_NumOpe[0].Text = VB6Helpers.Trim(initObj.MODGUSR.UsrEsp.CentroCosto);
                initObj.Frmgnota.Tx_NumOpe[1].Text = VB6Helpers.Trim(VB6Helpers.Format(VB6Helpers.Str(initObj.Frmgnota.Cb_Producto.get_ItemData_((short)initObj.Frmgnota.Cb_Producto.ListIndex)), "00"));
                initObj.Frmgnota.Tx_NumOpe[2].Text = VB6Helpers.Trim(initObj.MODGUSR.UsrEsp.Especialista);
                initObj.Frmgnota.Tx_NumOpe[3].Text = "000";
                initObj.Frmgnota.Tx_NumOpe[4].Text = "00000";
                initObj.Frmgnota.Tx_NumOpe[5].Text = "00";
                initObj.Frmgnota.Tx_NumOpe[6].Text = "00";
            }
        }

        public static void Pr_Cargar_Productos(InitializationObject initObj)
        {

            initObj.Frmgnota.Cb_Producto.AddItem(int.Parse(T_MODGUSR.IdPro_CobExp), "Cobranza de Exportaciones");
            initObj.Frmgnota.Cb_Producto.AddItem(int.Parse(T_MODGUSR.IdPro_CreExp), "Carta de Crédito Exportaciones");
            initObj.Frmgnota.Cb_Producto.AddItem(int.Parse(T_MODGUSR.IdPro_ComVen), "Compra / Venta Exportaciones");
            initObj.Frmgnota.Cb_Producto.AddItem(int.Parse(T_MODGUSR.IdPro_RetExp), "Retornos de Exportaciones");
            initObj.Frmgnota.Cb_Producto.AddItem(int.Parse(T_MODGUSR.IdPro_PagCCE), "Compra Anticipada de Carta de Crédito de Exportaciones");
            initObj.Frmgnota.Cb_Producto.AddItem(int.Parse(T_MODGUSR.IdPro_ConGen), "Contabilidad Genérica");
            initObj.Frmgnota.Cb_Producto.AddItem(int.Parse(T_MODGUSR.IdPro_CobImp), "Cobranzas Importaciones");
            initObj.Frmgnota.Cb_Producto.AddItem(int.Parse(T_MODGUSR.IdPro_CreImp), "Carta de Crédito Importaciones");
            initObj.Frmgnota.Cb_Producto.AddItem(int.Parse(T_MODGUSR.IdPro_PreExp), "Préstamos a Exportadores");
            initObj.Frmgnota.Cb_Producto.AddItem(int.Parse(T_MODGUSR.IdPro_DesExp), "Descuentos Documentos Exportaciones");
            initObj.Frmgnota.Cb_Producto.AddItem(int.Parse(T_MODGUSR.IdPro_ComExp), "Compra Documentos Exportaciones");

            initObj.Frmgnota.Cb_Producto.ListIndex = 0;
            Cb_Producto_Click(initObj);
        }

        public static void ok_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            string i = "";
            string Paso = "";
            short a = 0;
            short b = 0;
            dynamic MsgPrn = null;
            //Revisa si operacion es Cosmos

            if (initObj.Frmgnota.Cb_Producto.ListIndex != -1)
            {
                i = VB6Helpers.Format(VB6Helpers.CStr(initObj.Frmgnota.Cb_Producto.get_ItemData_((short)initObj.Frmgnota.Cb_Producto.ListIndex)), "00");
                initObj.Frmgnota.Tx_NumOpe[0].Text = VB6Helpers.Format(VB6Helpers.CStr((int)Format.StringToDouble(initObj.Frmgnota.Tx_NumOpe[0].Text)), "000");
                initObj.Frmgnota.Tx_NumOpe[1].Text = VB6Helpers.Format(VB6Helpers.CStr((int)Format.StringToDouble(initObj.Frmgnota.Tx_NumOpe[1].Text)), "00");
                initObj.Frmgnota.Tx_NumOpe[2].Text = VB6Helpers.Format(VB6Helpers.CStr((int)Format.StringToDouble(initObj.Frmgnota.Tx_NumOpe[2].Text)), "00");
                initObj.Frmgnota.Tx_NumOpe[3].Text = VB6Helpers.Format(VB6Helpers.CStr((int)Format.StringToDouble(initObj.Frmgnota.Tx_NumOpe[3].Text)), "000");
                initObj.Frmgnota.Tx_NumOpe[4].Text = VB6Helpers.Format(VB6Helpers.CStr((int)Format.StringToDouble(initObj.Frmgnota.Tx_NumOpe[4].Text)), "00000");
                initObj.Frmgnota.Tx_NumOpe[5].Text = VB6Helpers.Format(VB6Helpers.CStr((int)Format.StringToDouble(initObj.Frmgnota.Tx_NumOpe[5].Text)), "00");
                initObj.Frmgnota.Tx_NumOpe[6].Text = VB6Helpers.Format(VB6Helpers.CStr((int)Format.StringToDouble(initObj.Frmgnota.Tx_NumOpe[6].Text)), "00");

                Paso = VB6Helpers.Trim(initObj.Frmgnota.Tx_NumOpe[0].Text + initObj.Frmgnota.Tx_NumOpe[1].Text + initObj.Frmgnota.Tx_NumOpe[2].Text + initObj.Frmgnota.Tx_NumOpe[3].Text + initObj.Frmgnota.Tx_NumOpe[4].Text);
                //MZ
                initObj.MODCVDIM.ope0 = initObj.Frmgnota.Tx_NumOpe[0].Text;
                initObj.MODCVDIM.ope1 = initObj.Frmgnota.Tx_NumOpe[1].Text;
                initObj.MODCVDIM.ope2 = initObj.Frmgnota.Tx_NumOpe[2].Text;
                initObj.MODCVDIM.ope3 = initObj.Frmgnota.Tx_NumOpe[3].Text;
                initObj.MODCVDIM.ope4 = initObj.Frmgnota.Tx_NumOpe[4].Text;
                //fin MZ
                switch (initObj.MODCVDIM.ope1)
                {
                    case T_MODGUSR.IdPro_CobExp:  //Cobranza de exportaciones
                        a = MODGASO.SyGetp_xCob(initObj,uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_ComVen:
                    case "20":  //Compra / Venta
                        a = MODGASO.SyGetp_Crd(initObj, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_RetExp:  //Retornos Exportaciones
                        Paso = VB6Helpers.Trim(initObj.Frmgnota.Tx_NumOpe[0].Text + initObj.Frmgnota.Tx_NumOpe[1].Text + initObj.Frmgnota.Tx_NumOpe[2].Text + initObj.Frmgnota.Tx_NumOpe[3].Text + initObj.Frmgnota.Tx_NumOpe[4].Text + initObj.Frmgnota.Tx_NumOpe[5].Text + initObj.Frmgnota.Tx_NumOpe[6].Text);
                        a = MODGASO.SyGetp_Ret(initObj, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_ConGen:  //Contabilidad Genérica
                        a = MODGASO.SyGetp_GL(initObj, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_CobImp:  //Cobranzas Importaciones
                        a = MODGASO.SyGet_CImp(initObj, uow, Paso);

                        //********** JFO Se modifico para que cargara party de importaciones ***
                        break;
                    case T_MODGUSR.IdPro_CreImp:  //Carta de Crédito Importaciones
                        a = VB6Helpers.CShort(MODGASO.SyGet_CCIM(initObj, uow, Paso));
                        //======================================================================

                        break;
                    case T_MODGUSR.IdPro_CreExp:  //Carta de Crédito Exportaciones
                        a = MODGASO.SyGetp_xCob(initObj, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_PagCCE:  //Compra Anticipada Carta de Crédito Exportaciones
                        a = MODGASO.SyGetp_xCob(initObj, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_PreExp:  //Préstamos a Exportadores
                        a = MODGASO.SyGetp_xPaeCdd(initObj, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_DesExp:  //Descuentos Documentos Exportaciones
                        a = MODGASO.SyGetp_xPaeCdd(initObj, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_ComExp:  //Compra Documentos Exportaciones
                        a = MODGASO.SyGetp_xPaeCdd(initObj, uow, Paso);
                        break;
                }

                if (a != 0)
                {
                    initObj.MODGASO.VgAso.IndNom_t = Mdl_Funciones_Varias.GetDatPrt(initObj, uow, initObj.MODGASO.VgAso.PrtCli, initObj.MODGASO.VgAso.IndNom, initObj.MODGASO.VgAso.IndDir, "N");
                    initObj.MODGASO.VgAso.IndDir_t = Mdl_Funciones_Varias.GetDatPrt(initObj, uow, initObj.MODGASO.VgAso.PrtCli, initObj.MODGASO.VgAso.IndNom, initObj.MODGASO.VgAso.IndDir, "D");
                    initObj.Frmgnota.Tx_RutPrt.Text = MODGPYF0.Componer(initObj.MODGASO.VgAso.PrtCli, "~", "");
                    initObj.Frmgnota.Tx_NomPrt.Text = initObj.MODGASO.VgAso.IndNom_t;
                    initObj.Frmgnota.Tx_DirPrt.Text = initObj.MODGASO.VgAso.IndDir_t;
                    initObj.MODXORI.gb_esCosmos = false;
                    if (MODXORI.SyGet_CtaCte(initObj.MODGASO.VgAso.PrtCli,initObj,uow))
                    {
                        //Llama al WS
                        string token = MODXORI.SrvGetCtaCos(initObj.MODXORI.gs_ctacte_party);
                        Inet1_StateChanged(token, initObj);
                    }
                }
                else
                {
                    if (VB6Helpers.Val(initObj.Frmgnota.Tx_NumOpe[4].Text) > 0)
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "La Operación indicada NO se encuentra registrada.",
                            Type = TipoMensaje.Informacion,
                            Title = T_MODGASO.MsgAso
                        });
                        initObj.Frmgnota.Tx_RutPrt.Text = "";
                        initObj.Frmgnota.Tx_NomPrt.Text = "";
                        initObj.Frmgnota.Tx_DirPrt.Text = "";
                        initObj.Frmgnota.Tx_NumOpe[4].Text = VB6Helpers.Format("0", "00000");
                        initObj.Frmgnota.Tx_NumOpe[5].Text = VB6Helpers.Format("0", "00");
                        initObj.Frmgnota.Tx_NumOpe[6].Text = VB6Helpers.Format("0", "00");
                        return;
                    }
                    else
                    {
                        initObj.MODGASO.VgAso = initObj.MODGASO.VgAsoNul.Clone();
                    }
                }
            }

            //MZ 2009 Abre ventana facturas
            if(initObj.MODCVDIM.Gvar_NotaCredito == 1 && !string.IsNullOrEmpty(initObj.Frmgnota.Tx_RutPrt.Text))
            {
                initObj.Mdl_Funciones.VPrn_cre = new T_Prn_cre[0];  //Inicialización del arreglo
                b = MODGASO.SyGetn_Cre(initObj, uow, 1, Paso);

                if (b == 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "Atención: No existen Facturas para el número de operación ingresado.",
                        Type = TipoMensaje.Informacion,
                        Title = VB6Helpers.CStr(MsgPrn)
                    });
                }

                if (b == -1)
                {
                    // Aqui es donde abre la ventana nueva, revisar como se debe hacer
                    initObj.FormularioQueAbrir = "FacturasAsociadas";

                     /************************************************************************************
                     *  SE DEBE EJECUTAR DESPUES DE QUE SE CIERRE LA VENTANA DE FACTURAS
                     * **********************************************************************************/
                    //if (Format.StringToDouble(initObj.MODCVDIM.VNotaCreGl.NumFac) > 0)
                    //{
                    //    //Tx_NroFac.Text = VB6Helpers.Right(VB6Helpers.Space(20) + VB6Helpers.Format(VB6Helpers.Str(VB6Helpers.UCase(MODCVDIM.VNotaCreGl.NumFac)), MODGCON0.FormatoSinDec), 20);
                    //    //Tx_FecRep.Text = VB6Helpers.UCase(MODCVDIM.VNotaCreGl.FecOpe);
                    //    //Tx_NroRep.Text = VB6Helpers.Right(VB6Helpers.Space(20) + VB6Helpers.Format(VB6Helpers.Str(VB6Helpers.UCase(MODCVDIM.VNotaCreGl.NumRep)), MODGCON0.FormatoSinDec), 20);
                    //    //Tx_Moneda.Text = MODCVDIM.VNotaCreGl.Moneda;
                    //    //Tx_MtoOri.Text = VB6Helpers.Right(VB6Helpers.Space(20) + VB6Helpers.Format(VB6Helpers.Str(VB6Helpers.UCase(MODCVDIM.VNotaCreGl.monto)), MODGCON0.FormatoConDec), 20);
                    //    //Tx_Neto.Text = VB6Helpers.Right(VB6Helpers.Space(20) + VB6Helpers.Format(VB6Helpers.Str(VB6Helpers.UCase(MODCVDIM.VNotaCreGl.netofac)), MODGCON0.FormatoConDec), 20);
                    //    //Tx_iva.Text = VB6Helpers.Right(VB6Helpers.Space(20) + VB6Helpers.Format(VB6Helpers.Str(VB6Helpers.UCase(MODCVDIM.VNotaCreGl.ivafac)), MODGCON0.FormatoConDec), 20);
                    //    //Tx_tipo.Text = MODCVDIM.VNotaCreGl.tipofac;
                    //}

                }

            }

        }

        public static void Cb_Producto_Click(InitializationObject initObj)
        {
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;

            if (initObj.Frmgnota.Cb_Producto.ListIndex != -1)
            {
                i = (short)initObj.Frmgnota.Cb_Producto.get_ItemData_((short)initObj.Frmgnota.Cb_Producto.ListIndex);
                if (i == Format.StringToDouble("30"))
                {
                    initObj.Frmgnota.Tx_NumOpe[1].Enabled = true;
                }
                else
                {
                    initObj.Frmgnota.Tx_NumOpe[1].Enabled = false;
                }

                if (i != VB6Helpers.Val(T_MODGUSR.IdPro_RetExp))
                {
                    initObj.Frmgnota.Tx_NumOpe[5].Text = "00";
                    initObj.Frmgnota.Tx_NumOpe[6].Text = "00";
                    initObj.Frmgnota.Tx_NumOpe[5].Enabled = false;
                    initObj.Frmgnota.Tx_NumOpe[6].Enabled = false;
                }
                else
                {
                    initObj.Frmgnota.Tx_NumOpe[5].Enabled = true;
                    initObj.Frmgnota.Tx_NumOpe[6].Enabled = true;
                }

                initObj.Frmgnota.Tx_NumOpe[1].Text = VB6Helpers.Trim(VB6Helpers.Format(VB6Helpers.Str(i), "00"));
            }
            else
            {
                initObj.Frmgnota.Tx_NumOpe[1].Text = VB6Helpers.Trim(VB6Helpers.Format("0", "00"));
            }

        }

        private static void Inet1_StateChanged(string TOKEN, InitializationObject initObj)
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
                    Type = TipoMensaje.Informacion,
                    Title = T_MODGCVD.MsgCVD
                });
                initObj.Module1.Codop = initObj.Module1.Codop_CVD.Clone();
                initObj.CaptionAddition = "Cuenta BCH";
            }

            initObj.MODGCVD.VgCvd.codpro = initObj.Module1.Codop.Id_Product;
            initObj.MODGCVD.VgCvd.codope = initObj.Module1.Codop.Id_Operacion;
            initObj.MODGCVD.VgCvd.OpeSin = initObj.MODGCVD.VgCvd.codcct + initObj.MODGCVD.VgCvd.codpro + initObj.MODGCVD.VgCvd.codesp + initObj.MODGCVD.VgCvd.codofi + initObj.MODGCVD.VgCvd.codope;
            initObj.MODGCVD.VgCvd.OpeCon = initObj.MODGCVD.VgCvd.codcct + "-" + initObj.MODGCVD.VgCvd.codpro + "-" + initObj.MODGCVD.VgCvd.codesp + "-" + initObj.MODGCVD.VgCvd.codofi + "-" + initObj.MODGCVD.VgCvd.codope;
            if (initObj.MODGCVD.COMISION == true)
            {
                initObj.Frm_Principal.Caption = "Comisiones Fund Transfer  " + initObj.MODGCVD.VgCvd.OpeCon + " " + initObj.CaptionAddition;
            }
            else
            {
                initObj.Frm_Principal.Caption = "Compra Venta de Divisas Fund Transfer  " + initObj.MODGCVD.VgCvd.OpeCon + " " + initObj.CaptionAddition;
            }

        }

        public static void RetornoFacturasAsociadas(InitializationObject initObj)
        {

            /************************************************************************************
             *  SE DEBE EJECUTAR DESPUES DE QUE SE CIERRE LA VENTANA DE FACTURAS
             * **********************************************************************************/
            if (Format.StringToDouble(initObj.MODCVDIM.VNotaCreGl.NumFac) > 0)
            {
                initObj.Frmgnota.Tx_NroFac.Text = VB6Helpers.Right(VB6Helpers.Space(20) + VB6Helpers.Format(VB6Helpers.Str(VB6Helpers.UCase(initObj.MODCVDIM.VNotaCreGl.NumFac)), T_MODGCON0.FormatoSinDec), 20);
                initObj.Frmgnota.Tx_FecRep.Text = VB6Helpers.UCase(initObj.MODCVDIM.VNotaCreGl.FecOpe);
                initObj.Frmgnota.Tx_NroRep.Text = VB6Helpers.Right(VB6Helpers.Space(20) + VB6Helpers.Format(VB6Helpers.Str(VB6Helpers.UCase(initObj.MODCVDIM.VNotaCreGl.NumRep)), T_MODGCON0.FormatoSinDec), 20);
                initObj.Frmgnota.Tx_Moneda.Text = initObj.MODCVDIM.VNotaCreGl.Moneda;
                initObj.Frmgnota.Tx_MtoOri.Text = VB6Helpers.Right(VB6Helpers.Space(20) + VB6Helpers.Format(VB6Helpers.Str(VB6Helpers.UCase(initObj.MODCVDIM.VNotaCreGl.monto)), T_MODGCON0.FormatoConDec), 20);
                initObj.Frmgnota.Tx_Neto.Text = VB6Helpers.Right(VB6Helpers.Space(20) + VB6Helpers.Format(VB6Helpers.Str(VB6Helpers.UCase(initObj.MODCVDIM.VNotaCreGl.netofac)), T_MODGCON0.FormatoConDec), 20);
                initObj.Frmgnota.Tx_iva.Text = VB6Helpers.Right(VB6Helpers.Space(20) + VB6Helpers.Format(VB6Helpers.Str(VB6Helpers.UCase(initObj.MODCVDIM.VNotaCreGl.ivafac)), T_MODGCON0.FormatoConDec), 20);
                initObj.Frmgnota.Tx_tipo.Text = initObj.MODCVDIM.VNotaCreGl.tipofac;
            }
        }

        public static void Bot_canc_click(InitializationObject initObj)
        {
            UnitOfWorkCext01 uow = new UnitOfWorkCext01(); 
            initObj.MODGASO.VgAso = initObj.MODGASO.VgAsoNul.Clone();
            initObj.MODCVDIM.VNotaCreGl.NumFac = "0";
            initObj.MODCVDIM.VNotaCreGl.monto = "0";
            Nota(initObj,uow); 
        }

        public static void Bot_acep_click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short p = 0;
            short a = 0;
            dynamic GPrt_EnPartys = null;
            if (string.IsNullOrEmpty(initObj.MODGASO.VgAso.OpeSin))
            {
                initObj.MODGASO.VgAso = initObj.MODGASO.VgAsoNul.Clone();
            }
            else
            {
                p = (short)initObj.Frmgnota.Cb_Producto.get_ItemData_((short)initObj.Frmgnota.Cb_Producto.ListIndex);
                //If p% <> Val(IdPro_CreImp) Then
                a = Module1.ResetParty(initObj.Module1, initObj.MODGCVD.Beneficiario);
                initObj.Module1.PartysOpe[0].LlaveArchivo = initObj.MODGASO.VgAso.PrtCli;
                initObj.Module1.PartysOpe[0].IndNombre = initObj.MODGASO.VgAso.IndNom;
                initObj.Module1.PartysOpe[0].IndDireccion = initObj.MODGASO.VgAso.IndDir;
                initObj.Module1.PartysOpe[0].Ubicacion = VB6Helpers.CShort(GPrt_EnPartys);
                initObj.Module1.PartysOpe[0].Status = T_Module1.GPrt_StatDatos;
                a = MODSYGETPRT.SyGet_Prt(ref initObj.Module1.Codop, -1,initObj,uow);
                initObj.Mdi_Principal.BUTTONS["tbr_Comercio_invisible"].Enabled = true;
                ObjetosToVar(initObj);
                initObj.MODGCVD.NOTACRED = true;
                //ReDim Vx_CodTran(0)
                initObj.MODXORI.Vx_SCodTran = new S_Codtran[0];
                //End If
            }
        }

        private static short ObjetosToVar(InitializationObject initObj)
        {
            short _retValue = 0;

            try
            {
                // IGNORED: On Error GoTo ObjetosToVarErr

                //-------------------------------------------
                if (initObj.MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_Rev)
                {
                    return (short)(true ? -1 : 0);
                }

                //-------------------------------------------
                initObj.MODGCVD.VgCvd.PrtCli = initObj.Module1.PartysOpe[T_MODGCVD.ICli].LlaveArchivo;
                initObj.MODGCVD.VgCvd.IndNomC = initObj.Module1.PartysOpe[T_MODGCVD.ICli].IndNombre;
                initObj.MODGCVD.VgCvd.IndDirC = initObj.Module1.PartysOpe[T_MODGCVD.ICli].IndDireccion;
                initObj.MODGCVD.VgCvd.PrtOtr = initObj.Module1.PartysOpe[T_MODGCVD.IOtr].LlaveArchivo;
                initObj.MODGCVD.VgCvd.IndNomO = initObj.Module1.PartysOpe[T_MODGCVD.IOtr].IndNombre;
                initObj.MODGCVD.VgCvd.IndDirO = initObj.Module1.PartysOpe[T_MODGCVD.IOtr].IndDireccion;
                _retValue = (short)(true ? -1 : 0);

                
            }
            catch (Exception _ex)
            {
                // IGNORED: ObjetosToVarErr:
                VB6Helpers.SetError(_ex);
                //VB6Helpers.MsgBox("[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number), (MsgBoxStyle)MODGPYF0.pito(48), MODGCVD.MsgCVD);
                //goto ObjetosToVarEnd;
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                    Type = TipoMensaje.Informacion,
                    Title = T_MODGCVD.MsgCVD
                });
            }

            return _retValue;
        }

        public static void Nota_Salida(InitializationObject initObj) 
        {
            dynamic I_Cli = null;
            initObj.Frm_Principal.Tx_NomPrt.Text = initObj.Module1.PartysOpe[0].NombreUsado;
            initObj.Frm_Principal.Num_Op.Text = initObj.MODGASO.VgAso.OpeSin;

            //Habilita Frames.-
            if(!string.IsNullOrEmpty(initObj.MODGASO.VgAso.OpeSin))
            {
                initObj.Frm_Principal.Tx_NomPrt.Text = initObj.Module1.PartysOpe[VB6Helpers.CInt(I_Cli)].NombreUsado;
                //datos.Enabled = True
                //frame_ext.Enabled = True
                //frame_nac.Enabled = True
            }
            else
            {
                //VB6Helpers.SendKeys("{Tab}");
            }

            //Tx_NroFac = VNotaCreGl.NumFac
            initObj.Frm_Principal.Tx_NroFac.Text = VB6Helpers.Right(VB6Helpers.Space(20) + VB6Helpers.Format(VB6Helpers.Str(initObj.MODCVDIM.VNotaCreGl.NumFac), T_MODGCON0.FormatoSinDec), 20);
            initObj.Frm_Principal.Tx_tipo.Text = initObj.MODCVDIM.VNotaCreGl.tipofac;
            initObj.Frm_Principal.Tx_moneda.Text = initObj.MODCVDIM.VNotaCreGl.Moneda;
            initObj.Frm_Principal.Tx_neto.Text = VB6Helpers.Right(VB6Helpers.Space(20) + VB6Helpers.Format(VB6Helpers.Str(initObj.MODCVDIM.VNotaCreGl.netofac), T_MODGCON0.FormatoConDec), 20);
            initObj.Frm_Principal.Tx_iva.Text = VB6Helpers.Right(VB6Helpers.Space(20) + VB6Helpers.Format(VB6Helpers.Str(initObj.MODCVDIM.VNotaCreGl.ivafac), T_MODGCON0.FormatoConDec), 20);

            initObj.Frm_Principal.Tx_MtoOri.Text = Format.FormatCurrency(Format.StringToDouble(initObj.MODCVDIM.VNotaCreGl.monto), T_MODGCON0.FormatoConDec);

        }

        public static void Tx_NroOpe_LostFocus(ref short Index, UI_Frmgnota Frmgnota, InitializationObject initObj)
        {
            T_MODGPYF0 MODGPYF0 = initObj.MODGPYF0;
            int n = 0;
            if (string.IsNullOrEmpty(Frmgnota.Tx_NumOpe[Index].Text))
            {
                n = 0;
            }
            else
            {
                n = (int)Format.StringToDouble(Frmgnota.Tx_NumOpe[Index].Text);
            }

            switch (Index)
            {
                case 0:
                    Frmgnota.Tx_NumOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "000");
                    break;
                case 1:
                    Frmgnota.Tx_NumOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "00");
                    break;
                case 2:
                    Frmgnota.Tx_NumOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "00");
                    break;
                case 3:
                    Frmgnota.Tx_NumOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "000");
                    break;
                case 4:
                    Frmgnota.Tx_NumOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "00000");
                    break;
                case 5:
                    Frmgnota.Tx_NumOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "00");
                    break;
                case 6:
                    Frmgnota.Tx_NumOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "00");
                    break;
            }
        }
    }
}
