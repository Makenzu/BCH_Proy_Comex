using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XGGL.Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XGGL.Forms
{
    public class Frmg_Aso
    {

        /// <summary>
        /// Obtiene la lista de Productos
        /// </summary>
        /// <returns></returns>
        private static UI_Combo Obtener_Productos(UnitOfWorkCext01 uow)
        {
            UI_Combo Productos = new UI_Combo();
            Productos.AddItem(-1, "-- Seleccione --");
            // cargamos de manera dinámica la lista de productos, para productos que son relacionados
            foreach (sce_prd_s01_MS_Result producto in uow.SceRepository.sce_prd_s01_MS(true))
            {
                var codigoProducto = 0;
                int.TryParse(producto.codpro, out codigoProducto);
                if (!string.IsNullOrEmpty(producto.despro) && codigoProducto > 0)
                    Productos.AddItem(codigoProducto, producto.codpro + " - " + producto.despro);
            }
            Productos.ListIndex = 0;
            return Productos;
        }

        public static void ok_Click(DatosGlobales Globales, UnitOfWorkCext01 uow)
        {
            var MODGUSR = Globales.MODGUSR;
            if (!string.IsNullOrWhiteSpace(Globales.MODGASO.VgAso.PrtCli)) { Globales.MODGASO.VgAso = new T_Aso(); }

            var Cb_Producto = Globales.FrmgAso.Cb_Producto.SelectedValue.ToString().PadLeft(2, '0');
            var Tx_NomPrt = Globales.FrmgAso.Tx_NomPrt;
            var Tx_RutPrt = Globales.FrmgAso.Tx_RutPrt;
            var Tx_DirPrt = Globales.FrmgAso.Tx_DirPrt;
            string i = string.Empty;
            string Paso = string.Empty;
            short a = 0;

            //Se valida que se haya ingresado un numero de operacion completo
            if (string.IsNullOrEmpty(Globales.FrmgAso.Tx_NumOpe_000.Text) || string.IsNullOrEmpty(Globales.FrmgAso.Tx_NumOpe_002.Text)
                || string.IsNullOrEmpty(Globales.FrmgAso.Tx_NumOpe_003.Text) || string.IsNullOrEmpty(Globales.FrmgAso.Tx_NumOpe_004.Text))
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Debe ingresar un número de operación para realizar la búsqueda",
                    Title = T_MODGASO.MsgAso,
                    AutoClose = true
                });

                Tx_RutPrt.Text = string.Empty;
                Tx_NomPrt.Text = string.Empty;
                Tx_DirPrt.Text = string.Empty;
                Globales.FrmgAso.Tx_NumOpe_004.Text = "00000";
                Globales.FrmgAso.Tx_NumOpe_005.Text = "00";
                Globales.FrmgAso.Tx_NumOpe_006.Text = "00";
                return;
            }

            if (!string.IsNullOrEmpty(Cb_Producto))
            {
                Globales.FrmgAso.Tx_NumOpe_000.Text = Globales.FrmgAso.Tx_NumOpe_000.Text.PadLeft(3, '0');
                Globales.FrmgAso.Tx_NumOpe_001.Text = Cb_Producto;
                Globales.FrmgAso.Tx_NumOpe_002.Text = Globales.FrmgAso.Tx_NumOpe_002.Text.PadLeft(2, '0');
                Globales.FrmgAso.Tx_NumOpe_003.Text = Globales.FrmgAso.Tx_NumOpe_003.Text.PadLeft(3, '0');
                Globales.FrmgAso.Tx_NumOpe_004.Text = Globales.FrmgAso.Tx_NumOpe_004.Text.PadLeft(5, '0');

                Paso = Globales.FrmgAso.Tx_NumOpe_000.Text + Globales.FrmgAso.Tx_NumOpe_001.Text + Globales.FrmgAso.Tx_NumOpe_002.Text + Globales.FrmgAso.Tx_NumOpe_003.Text + Globales.FrmgAso.Tx_NumOpe_004.Text;

                switch (Cb_Producto)
                {
                    case T_MODGUSR.IdPro_CobExp:
                        a = MODGASO.SyGetp_xCob(Globales, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_CVD:
                        a = MODGASO.SyGetp_Crd(Globales, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_ComVen:
                        a = MODGASO.SyGetp_Crd(Globales, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_RetExp:
                        Globales.FrmgAso.Tx_NumOpe_005.Text = (Globales.FrmgAso.Tx_NumOpe_005.Text ?? string.Empty).PadLeft(2, '0');
                        Globales.FrmgAso.Tx_NumOpe_006.Text = (Globales.FrmgAso.Tx_NumOpe_006.Text ?? string.Empty).PadLeft(2, '0');
                        Paso += Globales.FrmgAso.Tx_NumOpe_005.Text + Globales.FrmgAso.Tx_NumOpe_006.Text;
                        a = MODGASO.SyGetp_Ret(Globales, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_ConGen:
                        a = MODGASO.SyGetp_GL(Globales, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_CobImp:
                        a = MODGASO.SyGet_CImp(Globales, uow, Paso);
                        // ********** JFO Se modifico para que cargara party de importaciones ***
                        break;
                    case T_MODGUSR.IdPro_CreImp:
                        a = MODGASO.SyGet_CCIM(Globales, uow, Paso);
                        // ======================================================================
                        break;
                    case T_MODGUSR.IdPro_CreExp:
                        a = MODGASO.SyGetp_xCob(Globales, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_PagCCE:
                        a = MODGASO.SyGetp_xCob(Globales, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_PreExp:
                        a = MODGASO.SyGetp_xPaeCdd(Globales, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_DesExp:
                        a = MODGASO.SyGetp_xPaeCdd(Globales, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_ComExp:
                        a = MODGASO.SyGetp_xPaeCdd(Globales, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_CreCon: // Carta Crédito Contado
                        a = MODGASO.SyGetp_CreCon(Globales, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_PreImp: // Prestamos a Importadores
                        a = MODGASO.SyGetp_PreImp(Globales, uow, Paso);
                        break;
                }


                if (a != 0)
                {
                    Globales.MODGASO.VgAso.IndNom_t = MODGNPRT.GetDatPrt(Globales, uow, Globales.MODGASO.VgAso.PrtCli, Globales.MODGASO.VgAso.IndNom, Globales.MODGASO.VgAso.IndDir, "N");
                    Globales.MODGASO.VgAso.IndDir_t = MODGNPRT.GetDatPrt(Globales, uow, Globales.MODGASO.VgAso.PrtCli, Globales.MODGASO.VgAso.IndNom, Globales.MODGASO.VgAso.IndDir, "D");
                    Tx_RutPrt.Text = MODGPYF0.Componer(Globales.MODGASO.VgAso.PrtCli, "~", string.Empty);
                    Tx_NomPrt.Text = Globales.MODGASO.VgAso.IndNom_t;
                    Tx_DirPrt.Text = Globales.MODGASO.VgAso.IndDir_t;
                }
                else
                {
                    if (VB6Helpers.Val(Globales.FrmgAso.Tx_NumOpe_004.Text) > 0)
                    {
                        Globales.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Critical,
                            Text = "La Operación indicada NO se encuentra registrada.",
                            Title = T_MODGASO.MsgAso,
                            AutoClose = true
                        });

                        Tx_RutPrt.Text = string.Empty;
                        Tx_NomPrt.Text = string.Empty;
                        Tx_DirPrt.Text = string.Empty;
                    }
                    else
                    {
                        Globales.MODGASO.VgAso = Globales.MODGASO.VgAsoNul;
                        Globales.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Critical,
                            Text = "La Operación indicada NO se encuentra registrada.",
                            Title = T_MODGASO.MsgAso,
                            AutoClose = true
                        });
                    }
                }
            }
        }

        public static void Form_Load(DatosGlobales Globales, UnitOfWorkCext01 uow)
        {
            if (Globales.FrmgAso == null) { Globales.FrmgAso = new FrmgAsoDTO(); }
            //Modificar para el ingreso de otros Productos.
            Globales.FrmgAso.Cb_Producto = Obtener_Productos(uow);

            //Carga los datos inciales.-
            if (!string.IsNullOrEmpty(Globales.MODGASO.VgAso.OpeSin))
            {
                Globales.FrmgAso.Cb_Producto.SelectedValue = int.Parse(VB6Helpers.Mid(Globales.MODGASO.VgAso.OpeSin, 4, 2));
                Globales.FrmgAso.Tx_NumOpe_000.Text = VB6Helpers.Mid(Globales.MODGASO.VgAso.OpeSin, 1, 3);
                // Globales.FrmgAso.Tx_NumOpe_001.Text = VB6Helpers.Mid(Globales.MODGASO.VgAso.OpeSin, 4, 2);
                Globales.FrmgAso.Tx_NumOpe_002.Text = VB6Helpers.Mid(Globales.MODGASO.VgAso.OpeSin, 6, 2);
                Globales.FrmgAso.Tx_NumOpe_003.Text = VB6Helpers.Mid(Globales.MODGASO.VgAso.OpeSin, 8, 3);
                Globales.FrmgAso.Tx_NumOpe_004.Text = VB6Helpers.Mid(Globales.MODGASO.VgAso.OpeSin, 11, 5);
                Globales.FrmgAso.Tx_NumOpe_005.Text = VB6Helpers.Mid(Globales.MODGASO.VgAso.OpeSin, 16, 2);
                Globales.FrmgAso.Tx_NumOpe_006.Text = VB6Helpers.Mid(Globales.MODGASO.VgAso.OpeSin, 18, 2);
                Globales.FrmgAso.Tx_RutPrt.Text = MODGPYF0.Componer(Globales.MODGASO.VgAso.PrtCli, "~", "");
                Globales.FrmgAso.Tx_NomPrt.Text = Globales.MODGASO.VgAso.IndNom_t;
                Globales.FrmgAso.Tx_DirPrt.Text = Globales.MODGASO.VgAso.IndDir_t;
            }
            else
            {
                Globales.FrmgAso.Tx_NumOpe_000.Text = Globales.MODGUSR.UsrEsp.CentroCosto.Trim();
                if (Globales.FrmgAso.Cb_Producto.SelectedValue > 0)
                {
                    Globales.FrmgAso.Tx_NumOpe_001.Text = VB6Helpers.Trim(VB6Helpers.Format(VB6Helpers.Str(Globales.FrmgAso.Cb_Producto.Items.Find(x => x.Data == Globales.FrmgAso.Cb_Producto.ListIndex).Data), "00"));
                }
                else
                {
                    Globales.FrmgAso.Tx_NumOpe_001.Text = "00";
                }
                Globales.FrmgAso.Tx_NumOpe_002.Text = Globales.MODGUSR.UsrEsp.Especialista.Trim();
                Globales.FrmgAso.Tx_NumOpe_003.Text = "000";
                Globales.FrmgAso.Tx_NumOpe_004.Text = "00000";
                Globales.FrmgAso.Tx_NumOpe_005.Text = "00";
                Globales.FrmgAso.Tx_NumOpe_006.Text = "00";
                Globales.FrmgAso.Tx_NumOpe_001.Enabled = false;
                Globales.FrmgAso.Tx_NumOpe_005.Enabled = false;
                Globales.FrmgAso.Tx_NumOpe_006.Enabled = false;

                //Se limpian los demás objetos
                Globales.FrmgAso.Tx_DirPrt.Text = string.Empty;
                Globales.FrmgAso.Tx_NomPrt.Text = string.Empty;
                Globales.FrmgAso.Tx_RutPrt.Text = string.Empty;
            }
        }

        public static void Cb_Producto_Click(DatosGlobales Globales, UnitOfWorkCext01 uow)
        {
            short i = 0;

            if (Globales.FrmgAso.Cb_Producto.Value != -1)
            {
                i = (short)Globales.FrmgAso.Cb_Producto.Value;
                if (i == Format.StringToDouble(T_MODGUSR.IdPro_CVD) || i == Format.StringToDouble(T_MODGUSR.IdPro_ComVen))
                {
                    Globales.FrmgAso.Tx_NumOpe_001.Enabled = true;
                }
                else
                {
                    Globales.FrmgAso.Tx_NumOpe_001.Enabled = false;
                }

                if (i != VB6Helpers.Val(T_MODGUSR.IdPro_RetExp))
                {
                    Globales.FrmgAso.Tx_NumOpe_005.Text = "00";
                    Globales.FrmgAso.Tx_NumOpe_006.Text = "00";
                    Globales.FrmgAso.Tx_NumOpe_005.Enabled = false;
                    Globales.FrmgAso.Tx_NumOpe_006.Enabled = false;
                }
                else
                {
                    Globales.FrmgAso.Tx_NumOpe_005.Enabled = true;
                    Globales.FrmgAso.Tx_NumOpe_006.Enabled = true;
                }

                Globales.FrmgAso.Tx_NumOpe_001.Text = VB6Helpers.Trim(VB6Helpers.Format(VB6Helpers.Str(i), "00"));
            }
            else
            {
                Globales.FrmgAso.Tx_NumOpe_001.Text = VB6Helpers.Trim(VB6Helpers.Format("0", "00"));
            }
        }

        public static void Aceptar_Click(DatosGlobales Globales, UnitOfWorkCext01 uow)
        {
            int a = 0;
            dynamic GPrt_EnPartys = null;

            if (string.IsNullOrEmpty(Globales.MODGASO.VgAso.OpeSin))
            {
                Globales.MODGASO.VgAso = Globales.MODGASO.VgAsoNul;
            }
            else
            {
                a = SYGETPRT.ResetParty(Globales, Globales.MODGCVD.Beneficiario);
                Globales.SYGETPRT.PartysOpe[0].LlaveArchivo = Globales.MODGASO.VgAso.PrtCli;
                Globales.SYGETPRT.PartysOpe[0].IndNombre = Globales.MODGASO.VgAso.IndNom;
                Globales.SYGETPRT.PartysOpe[0].IndDireccion = Globales.MODGASO.VgAso.IndDir;
                Globales.SYGETPRT.PartysOpe[0].Ubicacion = VB6Helpers.CShort(GPrt_EnPartys);
                Globales.SYGETPRT.PartysOpe[0].Status = T_SYGETPRT.GPrt_StatDatos;
                a = SYGETPRT.SyGet_Prt(ref Globales.SYGETPRT.Codop, -1, Globales, uow);
            }
        }

    }
}
