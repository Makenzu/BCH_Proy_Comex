
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public class FrmgAso
    {
        public static void Form_Load(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            //Modificar para el ingreso de otros Productos.
            if (initObj.FrmgAso.Cb_Producto.ListIndex <= 0)
            {
                Pr_Cargar_Productos(initObj, uow);
            }
            //Carga los datos inciales.-
            if (!string.IsNullOrEmpty(initObj.MODGASO.VgAso.OpeSin))
            {
                initObj.FrmgAso.Tx_NumOpe_000.Text = VB6Helpers.Mid(initObj.MODGASO.VgAso.OpeSin, 1, 3);
                initObj.FrmgAso.Tx_NumOpe_001.Text = VB6Helpers.Mid(initObj.MODGASO.VgAso.OpeSin, 4, 2);
                initObj.FrmgAso.Tx_NumOpe_002.Text = VB6Helpers.Mid(initObj.MODGASO.VgAso.OpeSin, 6, 2);
                initObj.FrmgAso.Tx_NumOpe_003.Text = VB6Helpers.Mid(initObj.MODGASO.VgAso.OpeSin, 8, 3);
                initObj.FrmgAso.Tx_NumOpe_004.Text = VB6Helpers.Mid(initObj.MODGASO.VgAso.OpeSin, 11, 5);
                initObj.FrmgAso.Tx_NumOpe_005.Text = VB6Helpers.Mid(initObj.MODGASO.VgAso.OpeSin, 16, 2).PadLeft(2, '0');
                initObj.FrmgAso.Tx_NumOpe_006.Text = VB6Helpers.Mid(initObj.MODGASO.VgAso.OpeSin, 18, 2).PadLeft(2, '0');
                initObj.FrmgAso.Tx_RutPrt.Text = MODGPYF0.Componer(initObj.MODGASO.VgAso.PrtCli, "~", "");
                initObj.FrmgAso.Tx_NomPrt.Text = initObj.MODGASO.VgAso.IndNom_t;
                initObj.FrmgAso.Tx_DirPrt.Text = initObj.MODGASO.VgAso.IndDir_t;
            }
            else
            {
                initObj.FrmgAso.Tx_NumOpe_000.Text = VB6Helpers.Trim(initObj.MODGUSR.UsrEsp.CentroCosto);
                if (initObj.FrmgAso.Cb_Producto.ListIndex > 0)
                {
                    initObj.FrmgAso.Tx_NumOpe_001.Text = VB6Helpers.Trim(VB6Helpers.Format(VB6Helpers.Str(initObj.FrmgAso.Cb_Producto.Items.Find(x => x.Data == initObj.FrmgAso.Cb_Producto.ListIndex).Data), "00"));
                }
                else
                {
                    initObj.FrmgAso.Tx_NumOpe_001.Text = "00";
                }
                initObj.FrmgAso.Tx_NumOpe_002.Text = VB6Helpers.Trim(initObj.MODGUSR.UsrEsp.Especialista);
                initObj.FrmgAso.Tx_NumOpe_003.Text = "000";
                initObj.FrmgAso.Tx_NumOpe_004.Text = "00000";
                initObj.FrmgAso.Tx_NumOpe_005.Text = "00";
                initObj.FrmgAso.Tx_NumOpe_006.Text = "00";
                initObj.FrmgAso.Tx_NumOpe_001.Enabled = false;
                initObj.FrmgAso.Tx_NumOpe_005.Enabled = false;
                initObj.FrmgAso.Tx_NumOpe_006.Enabled = false;
            }
        }

        private static void Pr_Cargar_Productos(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            var Cb_Producto = initObj.FrmgAso.Cb_Producto;

            Cb_Producto.Clear();
            Cb_Producto.AddItem(-1, "-- Seleccione --");
            // cargamos de manera dinámica la lista de productos, para productos que son relacionados
            foreach (sce_prd_s01_MS_Result producto in uow.SceRepository.sce_prd_s01_MS(true))
            {
                var codigoProducto = 0;
                int.TryParse(producto.codpro, out codigoProducto);
                if (!string.IsNullOrEmpty(producto.despro) && codigoProducto > 0)
                    Cb_Producto.AddItem(codigoProducto, producto.codpro + " - " + producto.despro);
            }
            Cb_Producto.ListIndex = 0;
        }

        public static void ok_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            var MODGUSR = initObj.MODGUSR;
            var Cb_Producto = initObj.FrmgAso.Cb_Producto;
            var Tx_NomPrt = initObj.FrmgAso.Tx_NomPrt;
            var Tx_RutPrt = initObj.FrmgAso.Tx_RutPrt;
            var Tx_DirPrt = initObj.FrmgAso.Tx_DirPrt;

            string i = "";
            string Paso = "";
            short a = 0;

            if (Cb_Producto.SelectedValue != -1)
            {
                i = VB6Helpers.Format(VB6Helpers.CStr(Cb_Producto.Items.Find(x => x.Data == Cb_Producto.SelectedValue).Data), "00");
                if (i == T_MODGUSR.IdPro_ComVen)
                {
                    if (initObj.FrmgAso.Tx_NumOpe_001.Text != "20" && initObj.FrmgAso.Tx_NumOpe_001.Text != T_MODGUSR.IdPro_ComVen)
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = "Numero de Producto no corresponde a Compra Venta de Divisas.",
                            Title = T_MODGASO.MsgAso
                        });

                        initObj.FrmgAso.Tx_NumOpe_001.Text = "30";
                        return;
                    }

                }
                else
                {
                    initObj.FrmgAso.Tx_NumOpe_001.Text = VB6Helpers.Trim(VB6Helpers.Format(i, "00"));
                }
                initObj.FrmgAso.Tx_NroOpe[0].Text = VB6Helpers.Format(VB6Helpers.CStr((int)Format.StringToDouble(initObj.FrmgAso.Tx_NroOpe[0].Text)), "000");
                initObj.FrmgAso.Tx_NroOpe[1].Text = VB6Helpers.Format(VB6Helpers.CStr((int)Format.StringToDouble(initObj.FrmgAso.Tx_NroOpe[1].Text)), "00");
                initObj.FrmgAso.Tx_NroOpe[2].Text = VB6Helpers.Format(VB6Helpers.CStr((int)Format.StringToDouble(initObj.FrmgAso.Tx_NroOpe[2].Text)), "00");
                initObj.FrmgAso.Tx_NroOpe[3].Text = VB6Helpers.Format(VB6Helpers.CStr((int)Format.StringToDouble(initObj.FrmgAso.Tx_NroOpe[3].Text)), "000");
                initObj.FrmgAso.Tx_NroOpe[4].Text = VB6Helpers.Format(VB6Helpers.CStr((int)Format.StringToDouble(initObj.FrmgAso.Tx_NroOpe[4].Text)), "00000");
                initObj.FrmgAso.Tx_NroOpe[5].Text = VB6Helpers.Format(VB6Helpers.CStr((int)Format.StringToDouble(initObj.FrmgAso.Tx_NroOpe[5].Text)), "00");
                initObj.FrmgAso.Tx_NroOpe[6].Text = VB6Helpers.Format(VB6Helpers.CStr((int)Format.StringToDouble(initObj.FrmgAso.Tx_NroOpe[6].Text)), "00");
                Paso = VB6Helpers.Trim(initObj.FrmgAso.Tx_NumOpe_000.Text + initObj.FrmgAso.Tx_NumOpe_001.Text + initObj.FrmgAso.Tx_NumOpe_002.Text + initObj.FrmgAso.Tx_NumOpe_003.Text + initObj.FrmgAso.Tx_NumOpe_004.Text);

                switch (i)
                {
                    case T_MODGUSR.IdPro_CobExp:  //Cobranza de exportaciones
                        a = MODGASO.SyGetp_xCob(initObj, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_ComVen:  //Compra / Venta
                        a = MODGASO.SyGetp_Crd(initObj, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_RetExp:  //Retornos Exportaciones
                        Paso =
                            VB6Helpers.Trim(initObj.FrmgAso.Tx_NroOpe[0].Text + initObj.FrmgAso.Tx_NroOpe[1].Text + initObj.FrmgAso.Tx_NroOpe[2].Text + initObj.FrmgAso.Tx_NroOpe[3].Text + initObj.FrmgAso.Tx_NroOpe[4].Text + initObj.FrmgAso.Tx_NroOpe[5].Text + initObj.FrmgAso.Tx_NroOpe[6].Text);
                        a = MODGASO.SyGetp_Ret(initObj, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_ConGen:  //Contabilidad Genérica
                        a = MODGASO.SyGetp_GL(initObj, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_CobImp:  //Cobranzas Importaciones
                        a = MODGASO.SyGet_CImp(initObj, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_CreImp:  //Carta de Crédito Importaciones
                        a = (short)(true ? -1 : 0);
                        initObj.MODGASO.VgAso.OpeSin = VB6Helpers.Trim(Paso);
                        initObj.MODGASO.VgAso.OpeCon = VB6Helpers.Trim(VB6Helpers.Mid(Paso, 1, 3) + "-" + VB6Helpers.Mid(Paso, 4, 2) + "-" + VB6Helpers.Mid(Paso, 6, 2) + "-" + VB6Helpers.Mid(Paso, 8, 3) + "-" + VB6Helpers.Mid(Paso, 11, 5));
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
                    case T_MODGUSR.IdPro_CreCon: // Carta Crédito Contado
                        a = MODGASO.SyGetp_CreCon(initObj, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_PreImp: // Prestamos a Importadores
                        a = MODGASO.SyGetp_PreImp(initObj, uow, Paso);
                        break;
                }

                if (a != 0)
                {
                    initObj.MODGASO.VgAso.IndNom_t = Mdl_Funciones_Varias.GetDatPrt(initObj, uow, initObj.MODGASO.VgAso.PrtCli, initObj.MODGASO.VgAso.IndNom, initObj.MODGASO.VgAso.IndDir, "N");
                    initObj.MODGASO.VgAso.IndDir_t = Mdl_Funciones_Varias.GetDatPrt(initObj, uow, initObj.MODGASO.VgAso.PrtCli, initObj.MODGASO.VgAso.IndNom, initObj.MODGASO.VgAso.IndDir, "D");
                    Tx_RutPrt.Text = MODGPYF0.Componer(initObj.MODGASO.VgAso.PrtCli, "~", "");
                    Tx_NomPrt.Text = initObj.MODGASO.VgAso.IndNom_t;
                    Tx_DirPrt.Text = initObj.MODGASO.VgAso.IndDir_t;

                    //Revisa si operacion es Cosmos
                    initObj.FrmgAso.Boton_000.Enabled = false;
                    initObj.MODXORI.gb_esCosmos = false;

                    MODXORI.Inet1_StateChanged(int.Parse(initObj.FrmgAso.Tx_NumOpe_001.Text), initObj);
                    initObj.FrmgAso.Boton_000.Enabled = true;

                }
                else
                {
                    if (VB6Helpers.Val(initObj.FrmgAso.Tx_NumOpe_004.Text) > 0)
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = "La Operación indicada NO se encuentra registrada.",
                            Title = T_MODGASO.MsgAso
                        });

                        Tx_RutPrt.Text = "";
                        Tx_NomPrt.Text = "";
                        Tx_DirPrt.Text = "";
                        initObj.FrmgAso.Tx_NumOpe_004.Text = VB6Helpers.Format("0", "00000");
                        initObj.FrmgAso.Tx_NumOpe_005.Text = VB6Helpers.Format("0", "00");
                        initObj.FrmgAso.Tx_NumOpe_006.Text = VB6Helpers.Format("0", "00");
                    }
                    else
                    {
                        initObj.MODGASO.VgAso = initObj.MODGASO.VgAsoNul.Clone();
                    }
                }
            }
        }

        public static void Aceptar_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
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
                p = (short)initObj.FrmgAso.Cb_Producto.Items.Find(x => x.Data == initObj.FrmgAso.Cb_Producto.SelectedValue).Data;
                if (p != VB6Helpers.Val(T_MODGUSR.IdPro_CreImp))
                {
                    a = Module1.ResetParty(initObj.Module1, initObj.MODGCVD.Beneficiario);
                    initObj.Module1.PartysOpe[0].LlaveArchivo = initObj.MODGASO.VgAso.PrtCli;
                    initObj.Module1.PartysOpe[0].IndNombre = initObj.MODGASO.VgAso.IndNom;
                    initObj.Module1.PartysOpe[0].IndDireccion = initObj.MODGASO.VgAso.IndDir;
                    initObj.Module1.PartysOpe[0].Ubicacion = VB6Helpers.CShort(GPrt_EnPartys);
                    initObj.Module1.PartysOpe[0].Status = T_Module1.GPrt_StatDatos;
                    a = MODSYGETPRT.SyGet_Prt(ref initObj.Module1.Codop, -1, initObj, uow);
                    initObj.Mdi_Principal.BUTTONS["tbr_grabar"].Enabled = false;
                    initObj.Mdi_Principal.BUTTONS["tbr_comisiones"].Enabled = false;
                    initObj.Mdi_Principal.BUTTONS["tbr_nota"].Enabled = false;
                    //Se llama al metodo que llena la informacion del participante
                    mdi_PrincipalLogic.Participantes2_3(initObj, true);
                }
            }
        }

        public static void Cancelar_Click(InitializationObject initObj)
        {
            initObj.MODGASO.VgAso = initObj.MODGASO.VgAsoNul.Clone();
            initObj.MODGASO.VgAso.OpeSin = "";
            initObj.FrmgAso.Tx_RutPrt.Text = "";
            initObj.FrmgAso.Tx_NomPrt.Text = "";
            initObj.FrmgAso.Tx_DirPrt.Text = "";
        }

        public static void Cb_Producto_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short i = 0;

            if (initObj.FrmgAso.Cb_Producto.SelectedValue != -1)
            {
                //i = (short)initObj.FrmgAso.Cb_Producto.get_ItemData_((short)initObj.FrmgAso.Cb_Producto.ListIndex);
                i = (short)initObj.FrmgAso.Cb_Producto.Items.Find(x => x.Data == initObj.FrmgAso.Cb_Producto.SelectedValue).Data;
                if (i == Format.StringToDouble(T_MODGUSR.IdPro_ComVen))
                {
                    initObj.FrmgAso.Tx_NumOpe_001.Enabled = true;
                }
                else
                {
                    initObj.FrmgAso.Tx_NumOpe_001.Enabled = false;
                }

                if (i != VB6Helpers.Val(T_MODGUSR.IdPro_RetExp))
                {
                    initObj.FrmgAso.Tx_NumOpe_005.Text = "00";
                    initObj.FrmgAso.Tx_NumOpe_006.Text = "00";
                    initObj.FrmgAso.Tx_NumOpe_005.Enabled = false;
                    initObj.FrmgAso.Tx_NumOpe_006.Enabled = false;
                }
                else
                {
                    initObj.FrmgAso.Tx_NumOpe_005.Enabled = true;
                    initObj.FrmgAso.Tx_NumOpe_006.Enabled = true;
                }

                initObj.FrmgAso.Tx_NumOpe_001.Text = VB6Helpers.Trim(VB6Helpers.Format(VB6Helpers.Str(i), "00"));
            }
            else
            {
                initObj.FrmgAso.Tx_NumOpe_001.Text = VB6Helpers.Trim(VB6Helpers.Format("0", "00"));
            }
        }

        //public static void Tx_NumOpe_LostFocus(InitializationObject initObj, short Index)
        //{

        //    switch (Index)
        //    {
        //        case 0:
        //            initObj.FrmgAso.Tx_NumOpe_000.Text = "000";
        //            break;
        //        case 1:
        //            initObj.FrmgAso.Tx_NumOpe_001.Text = "00";
        //            break;
        //        case 2:
        //            initObj.FrmgAso.Tx_NumOpe_002.Text = "00";
        //            break;
        //        case 3:
        //            initObj.FrmgAso.Tx_NumOpe_003.Text = "000";
        //            break;
        //        case 4:
        //            initObj.FrmgAso.Tx_NumOpe_004.Text = "00000";
        //            break;
        //        case 5:
        //            initObj.FrmgAso.Tx_NumOpe_005.Text = "00";
        //            break;
        //        case 6:
        //            initObj.FrmgAso.Tx_NumOpe_006.Text = "00";
        //            break;
        //    }
        //}

        public static void Tx_NumOpe_LostFocus(ref short Index, UI_FrmgAso FrmgAso, InitializationObject initObj)
        {

            T_MODGPYF0 MODGPYF0 = initObj.MODGPYF0;
            int n = 0;
            if (string.IsNullOrEmpty(FrmgAso.Tx_NroOpe[Index].Text))
            {
                n = 0;
            }
            else
            {
                n = (int)Format.StringToDouble(FrmgAso.Tx_NroOpe[Index].Text);
            }

            switch (Index)
            {
                case 0:
                    FrmgAso.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "000");
                    break;
                case 1:
                    FrmgAso.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "00");
                    break;
                case 2:
                    FrmgAso.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "00");
                    break;
                case 3:
                    FrmgAso.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "000");
                    break;
                case 4:
                    FrmgAso.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "00000");
                    break;
                case 5:
                    FrmgAso.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "00");
                    break;

                case 6:
                    FrmgAso.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "00");
                    break;

            }

        }




    }
}
