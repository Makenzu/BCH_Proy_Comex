using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XGGL.Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Forms
{
    public class Frmg_AsoNC
    {

        /// <summary>
        /// Obtiene la lista de Productos
        /// </summary>
        /// <returns></returns>
        private static UI_Combo Obtener_Productos()
        {
            UI_Combo Productos = new UI_Combo();
            Productos.AddItem(int.Parse(T_MODGUSR.IdPro_CobExp), "Cobranza de Exportaciones");
            Productos.AddItem(int.Parse(T_MODGUSR.IdPro_CreExp), "Carta de Crédito Exportaciones");
            Productos.AddItem(int.Parse(T_MODGUSR.IdPro_CVD), "Compra / Venta Exportaciones");
            Productos.AddItem(int.Parse(T_MODGUSR.IdPro_RetExp), "Retornos de Exportaciones");
            Productos.AddItem(int.Parse(T_MODGUSR.IdPro_PagCCE), "Compra Anticipada de Carta de Crédito de Exportaciones");
            Productos.AddItem(int.Parse(T_MODGUSR.IdPro_ConGen), "Contabilidad Genérica");
            Productos.AddItem(int.Parse(T_MODGUSR.IdPro_CobImp), "Cobranzas Importaciones");
            Productos.AddItem(int.Parse(T_MODGUSR.IdPro_CreImp), "Carta de Crédito Importaciones");
            Productos.AddItem(int.Parse(T_MODGUSR.IdPro_PreExp), "Préstamos a Exportadores");
            Productos.AddItem(int.Parse(T_MODGUSR.IdPro_DesExp), "Descuentos Documentos Exportaciones");
            Productos.AddItem(int.Parse(T_MODGUSR.IdPro_ComExp), "Compra Documentos Exportaciones");

            return Productos;
        }

        public static void ok_Click(DatosGlobales Globales, UnitOfWorkCext01 uow)
        {
            var MODGUSR = Globales.MODGUSR;

            var Cb_Producto = Globales.FrmgAsoNC.Cb_Producto.SelectedValue.ToString().PadLeft(2, '0');
            var Tx_NomPrt = Globales.FrmgAsoNC.Tx_NomPrt;
            var Tx_RutPrt = Globales.FrmgAsoNC.Tx_RutPrt;
            var Tx_DirPrt = Globales.FrmgAsoNC.Tx_DirPrt;

            string i = string.Empty;
            string Paso = string.Empty;
            short a = 0;


            if (!string.IsNullOrEmpty(Cb_Producto))
            {

                Globales.FrmgAsoNC.Tx_NumOpe_000.Text = Globales.FrmgAsoNC.Tx_NumOpe_000.Text.PadLeft(3, '0');
                Globales.FrmgAsoNC.Tx_NumOpe_001.Text = Globales.FrmgAsoNC.Tx_NumOpe_001.Text.PadLeft(2, '0');
                Globales.FrmgAsoNC.Tx_NumOpe_002.Text = Globales.FrmgAsoNC.Tx_NumOpe_002.Text.PadLeft(2, '0');
                Globales.FrmgAsoNC.Tx_NumOpe_003.Text = Globales.FrmgAsoNC.Tx_NumOpe_003.Text.PadLeft(3, '0');
                Globales.FrmgAsoNC.Tx_NumOpe_004.Text = Globales.FrmgAsoNC.Tx_NumOpe_004.Text.PadLeft(5, '0');
                Globales.FrmgAsoNC.Tx_NumOpe_005.Text = (String.IsNullOrEmpty(Globales.FrmgAsoNC.Tx_NumOpe_005.Text) ? "00" : Globales.FrmgAsoNC.Tx_NumOpe_005.Text.PadLeft(2, '0'));
                Globales.FrmgAsoNC.Tx_NumOpe_006.Text = (String.IsNullOrEmpty(Globales.FrmgAsoNC.Tx_NumOpe_006.Text) ? "00" : Globales.FrmgAsoNC.Tx_NumOpe_006.Text.PadLeft(2, '0')); 

                Globales.FrmgAsoNC.Tx_NumOpe_001.Text = Cb_Producto;
                Paso = Globales.FrmgAsoNC.Tx_NumOpe_000.Text + Globales.FrmgAsoNC.Tx_NumOpe_001.Text + Globales.FrmgAsoNC.Tx_NumOpe_002.Text + Globales.FrmgAsoNC.Tx_NumOpe_003.Text + Globales.FrmgAsoNC.Tx_NumOpe_004.Text;

                switch (Cb_Producto)
                {
                    case T_MODGUSR.IdPro_CobExp:
                        a = MODGASO.SyGetp_xCob(Globales, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_CVD:
                        a = MODGASO.SyGetp_Crd(Globales, uow, Paso);
                        break;
                    case T_MODGUSR.IdPro_RetExp:
                        Paso += Globales.FrmgAsoNC.Tx_NumOpe_005.Text + Globales.FrmgAsoNC.Tx_NumOpe_006.Text;
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
                }


                if (a != 0)
                {
                    Globales.MODGASO.VgAso.IndNom_t = MODGNPRT.GetDatPrt(Globales, uow, Globales.MODGASO.VgAso.PrtCli, Globales.MODGASO.VgAso.IndNom, Globales.MODGASO.VgAso.IndDir, "N");
                    Globales.MODGASO.VgAso.IndDir_t = MODGNPRT.GetDatPrt(Globales, uow, Globales.MODGASO.VgAso.PrtCli, Globales.MODGASO.VgAso.IndNom, Globales.MODGASO.VgAso.IndDir, "D");
                    Tx_RutPrt.Text = MODGPYF0.Componer(Globales.MODGASO.VgAso.PrtCli, "~", string.Empty);
                    Tx_RutPrt.Text.Trim().Replace("|", "");
                    Tx_NomPrt.Text = Globales.MODGASO.VgAso.IndNom_t;
                    Tx_DirPrt.Text = Globales.MODGASO.VgAso.IndDir_t;
                }
                else
                {
                    //if (VB6Helpers.Val(Globales.FrmgAsoNC.Tx_NumOpe_004.Text) > 0)
                    //{
                        Globales.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = "La Operación indicada NO se encuentra registrada.",
                            Title = T_MODGASO.MsgAso,
                            AutoClose = true
                        });

                        Tx_RutPrt.Text = string.Empty;
                        Tx_NomPrt.Text = string.Empty;
                        Tx_DirPrt.Text = string.Empty;
                        Globales.FrmgAsoNC.Tx_NumOpe_004.Text = "00000";
                        Globales.FrmgAsoNC.Tx_NumOpe_005.Text = "00";
                        Globales.FrmgAsoNC.Tx_NumOpe_006.Text = "00";
                        if (Globales.FrmgAsoNC.Facturas != null)
                        {
                            Globales.FrmgAsoNC.Facturas.Clear();
                        }
                    //}
                    //else
                    //{
                    //    Globales.MODGASO.VgAso = Globales.MODGASO.VgAsoNul;
                    //}
                }
            }


            //Se cargan los datos de Facturas
            if (!string.IsNullOrEmpty(Tx_RutPrt.Text))
            {

                Globales.ModNotc.VPrn_cre = new T_ModNotc.T_Prn_cre[0];     // Inicialización del arreglo
                string operacion = Globales.FrmgAsoNC.Tx_NumOpe_000.Text + Globales.FrmgAsoNC.Tx_NumOpe_001.Text + Globales.FrmgAsoNC.Tx_NumOpe_002.Text + Globales.FrmgAsoNC.Tx_NumOpe_003.Text + Globales.FrmgAsoNC.Tx_NumOpe_004.Text;

                //Se debe limpiar las facturas siempre
                Globales.FrmgAsoNC.Facturas = new List<FrmgNCFacturasDTO>();

                if (SyGetn_Cre(Globales, uow, 1, operacion))
                {
                    
                    foreach (var item in Globales.ModNotc.VPrn_cre)
                    {
                        Globales.FrmgAsoNC.Facturas.Add(new FrmgNCFacturasDTO()
                        {
                            NroFactura = item.Factura.ToString("#,###,###,###,##0"),
                            NroReporte = item.NroRpt.ToString("#,###,###,###,##0"),
                            FechaFactura = item.FecOpe,
                            Moneda = MODGPYF1.Minuscula(Globales.MODGTAB0.VMnd[(item.monedafac.ToDbl()).ToInt()].Mnd_MndNom),
                            Neto = Format.FormatCurrency(item.neto, "#,###,###,###,##0.00"),
                            Iva = Format.FormatCurrency(item.iva, "#,###,###,###,##0.00"),
                            Total = Format.FormatCurrency(item.monto, "#,###,###,###,##0.00"),
                            Tipo = (item.tipofac == "A" ? "Afecta" : "Exenta")
                        });
                    }
                }
                else
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "Atención: No existen Facturas para el número de operación ingresado.",
                        Title = T_MODGASO.MsgAso,
                        AutoClose = true
                    });
                }
            }
        }

        public static void Form_Load(DatosGlobales Globales)
        {
            //Modificar para el ingreso de otros Productos.
            Globales.FrmgAsoNC.Cb_Producto = Obtener_Productos();
            Globales.MODGASO.VgAso = new T_Aso(); 
            //Carga los datos inciales.-
            if (!string.IsNullOrEmpty(Globales.MODGASO.VgAso.OpeSin))
            {
                Globales.FrmgAsoNC.Tx_NumOpe_000.Text = VB6Helpers.Mid(Globales.MODGASO.VgAso.OpeSin, 1, 3);
                // Globales.FrmgAsoNC.Tx_NumOpe_001.Text = VB6Helpers.Mid(Globales.MODGASO.VgAso.OpeSin, 4, 2);
                Globales.FrmgAsoNC.Tx_NumOpe_002.Text = VB6Helpers.Mid(Globales.MODGASO.VgAso.OpeSin, 6, 2);
                Globales.FrmgAsoNC.Tx_NumOpe_003.Text = VB6Helpers.Mid(Globales.MODGASO.VgAso.OpeSin, 8, 3);
                Globales.FrmgAsoNC.Tx_NumOpe_004.Text = VB6Helpers.Mid(Globales.MODGASO.VgAso.OpeSin, 11, 5);
                Globales.FrmgAsoNC.Tx_NumOpe_005.Text = VB6Helpers.Mid(Globales.MODGASO.VgAso.OpeSin, 16, 2);
                Globales.FrmgAsoNC.Tx_NumOpe_006.Text = VB6Helpers.Mid(Globales.MODGASO.VgAso.OpeSin, 18, 2);
                Globales.FrmgAsoNC.Tx_RutPrt.Text = MODGPYF0.Componer(Globales.MODGASO.VgAso.PrtCli, "~", "");
                Globales.FrmgAsoNC.Tx_NomPrt.Text = Globales.MODGASO.VgAso.IndNom_t;
                Globales.FrmgAsoNC.Tx_DirPrt.Text = Globales.MODGASO.VgAso.IndDir_t;
            }
            else
            {

                Globales.FrmgAsoNC.Tx_NumOpe_000.Text = Globales.MODGUSR.UsrEsp.CentroCosto.Trim();
                if (Globales.FrmgAsoNC.Cb_Producto.ListIndex > 0)
                {
                    Globales.FrmgAsoNC.Tx_NumOpe_001.Text = VB6Helpers.Trim(VB6Helpers.Format(VB6Helpers.Str(Globales.FrmgAsoNC.Cb_Producto.Items.Find(x => x.Data == Globales.FrmgAsoNC.Cb_Producto.ListIndex).Data), "00"));
                }
                else
                {
                    Globales.FrmgAsoNC.Tx_NumOpe_001.Text = "00";
                }
                Globales.FrmgAsoNC.Tx_NumOpe_002.Text = Globales.MODGUSR.UsrEsp.Especialista.Trim();
                Globales.FrmgAsoNC.Tx_NumOpe_003.Text = "000";
                Globales.FrmgAsoNC.Tx_NumOpe_004.Text = "00000";
                Globales.FrmgAsoNC.Tx_NumOpe_005.Text = "00";
                Globales.FrmgAsoNC.Tx_NumOpe_006.Text = "00";
                Globales.FrmgAsoNC.Tx_NumOpe_001.Enabled = false;
                Globales.FrmgAsoNC.Tx_NumOpe_005.Enabled = false;
                Globales.FrmgAsoNC.Tx_NumOpe_006.Enabled = false;
                Globales.FrmgAsoNC.Tx_RutPrt.Text = "";
                Globales.FrmgAsoNC.Tx_NomPrt.Text = "";
                Globales.FrmgAsoNC.Tx_DirPrt.Text = "";

            }
        }

        public static void Cb_Producto_Click(DatosGlobales Globales, UnitOfWorkCext01 uow)
        {
            short i = 0;

            if (Globales.FrmgAsoNC.Cb_Producto.ListIndex != -1)
            {
                i = (short)Globales.FrmgAsoNC.Cb_Producto.Items.Find(x => x.Data == Globales.FrmgAsoNC.Cb_Producto.ListIndex).Data;
                if (i == Format.StringToDouble(T_MODGUSR.IdPro_CVD))
                {
                    Globales.FrmgAsoNC.Tx_NumOpe_001.Enabled = true;
                }
                else
                {
                    Globales.FrmgAsoNC.Tx_NumOpe_001.Enabled = false;
                }

                if (i != VB6Helpers.Val(T_MODGUSR.IdPro_RetExp))
                {
                    Globales.FrmgAsoNC.Tx_NumOpe_005.Text = "00";
                    Globales.FrmgAsoNC.Tx_NumOpe_006.Text = "00";
                    Globales.FrmgAsoNC.Tx_NumOpe_005.Enabled = false;
                    Globales.FrmgAsoNC.Tx_NumOpe_006.Enabled = false;
                }
                else
                {
                    Globales.FrmgAsoNC.Tx_NumOpe_005.Enabled = true;
                    Globales.FrmgAsoNC.Tx_NumOpe_006.Enabled = true;
                }

                Globales.FrmgAsoNC.Tx_NumOpe_001.Text = VB6Helpers.Trim(VB6Helpers.Format(VB6Helpers.Str(i), "00"));
            }
            else
            {
                Globales.FrmgAsoNC.Tx_NumOpe_001.Text = VB6Helpers.Trim(VB6Helpers.Format("0", "00"));
            }
        }

        public static void Aceptar_Click(DatosGlobales Globales, UnitOfWorkCext01 uow)
        {
            short p = 0;
            int a = 0;
            dynamic GPrt_EnPartys = null;

            if (string.IsNullOrEmpty(Globales.MODGASO.VgAso.OpeSin))
            {
                Globales.MODGASO.VgAso = Globales.MODGASO.VgAsoNul;
            }
            else
            {
                p = (short)Globales.FrmgAsoNC.Cb_Producto.SelectedValue;
                if (p != VB6Helpers.Val(T_MODGUSR.IdPro_CreImp))
                {
                    a = SYGETPRT.ResetParty(Globales, Globales.MODGCVD.Beneficiario);
                    Globales.SYGETPRT.PartysOpe[0].LlaveArchivo = Globales.MODGASO.VgAso.PrtCli;
                    Globales.SYGETPRT.PartysOpe[0].IndNombre = Globales.MODGASO.VgAso.IndNom;
                    Globales.SYGETPRT.PartysOpe[0].IndDireccion = Globales.MODGASO.VgAso.IndDir;
                    Globales.SYGETPRT.PartysOpe[0].Ubicacion = VB6Helpers.CShort(GPrt_EnPartys);
                    Globales.SYGETPRT.PartysOpe[0].Status = T_SYGETPRT.GPrt_StatDatos;
                    Globales.MODGOVD.Gvar_NotaCredito = 1;
                    a = SYGETPRT.SyGet_Prt(ref Globales.SYGETPRT.Codop, -1, Globales, uow);
                }
            }


            Globales.gl.Cliente.Text = Globales.FrmgAsoNC.Tx_NomPrt.Text;
            Globales.gl.Num_Op.Text = Globales.gl.Tx_NroFac.Text != null ?
            Globales.FrmgAsoNC.Tx_NumOpe_000.Text + Globales.FrmgAsoNC.Tx_NumOpe_001.Text + Globales.FrmgAsoNC.Tx_NumOpe_002.Text + Globales.FrmgAsoNC.Tx_NumOpe_003.Text + Globales.FrmgAsoNC.Tx_NumOpe_004.Text : "";
            
            Globales.gl.Tx_NroFac.Text = Globales.MODCTA.VNotaCreGl.NumFac.ToString();
            Globales.gl.Tx_moneda.Text = Globales.MODCTA.VNotaCreGl.Moneda;
            Globales.gl.Tx_tipo.Text =Globales.gl.Tx_NroFac.Text != null ? Globales.MODCTA.VNotaCreGl.tipofac : "";

            Globales.gl.Tx_neto.Text = Globales.MODCTA.VNotaCreGl.netofac;
            Globales.gl.Tx_iva.Text = Globales.MODCTA.VNotaCreGl.ivafac;
            Globales.gl.Tx_MtoOri.Text = Globales.MODCTA.VNotaCreGl.monto;
            Globales.gl.monedas.Enabled = true;
          
        }

        /// <summary>
        /// Usado en Contabilidad Generica - Nota de Credito
        /// </summary>
        /// <param name="Globales"></param>
        /// <param name="unit"></param>
        /// <param name="Indice"></param>
        /// <param name="NumOpe"></param>
        /// <returns></returns>
        public static bool SyGetn_Cre(DatosGlobales Globales, UnitOfWorkCext01 unit, short Indice, string NumOpe)
        {
            bool _retValue = false;
            short m = 0;
            short n = 0;
            short i = 0;
            List<sce_mcd_s04_MS_Result> Result = new List<sce_mcd_s04_MS_Result>();
            try
            {
                switch (Indice)
                {
                    case 0:
                        break;
                    case 1:
                        Result = unit.SceRepository.sce_mcd_s04_MS(NumOpe);
                        break;
                }

                m = (short)(Globales.ModNotc.VPrn_cre.Length);
                n = (short)Result.Count();

                if (Result.Count() == 0)
                    return false;

                Array.Resize<T_ModNotc.T_Prn_cre>(ref Globales.ModNotc.VPrn_cre, m + n);
                int indexResult = 0;
                for (i = m; i < (short)Globales.ModNotc.VPrn_cre.Length; i++, indexResult++)
                {
                    Globales.ModNotc.VPrn_cre[i] = new T_ModNotc.T_Prn_cre();
                    Globales.ModNotc.VPrn_cre[i].CodCct = Result[indexResult].codcct;
                    Globales.ModNotc.VPrn_cre[i].CodPro = Result[indexResult].codpro;
                    Globales.ModNotc.VPrn_cre[i].CodEsp = Result[indexResult].codesp;
                    Globales.ModNotc.VPrn_cre[i].CodOfi = Result[indexResult].codofi;
                    Globales.ModNotc.VPrn_cre[i].CodOpe = Result[indexResult].codope;
                    Globales.ModNotc.VPrn_cre[i].Factura = (double)Result[indexResult].nrofac;
                    Globales.ModNotc.VPrn_cre[i].NroRpt = (int)Result[indexResult].nrorpt;
                    Globales.ModNotc.VPrn_cre[i].FecOpe = Result[indexResult].fecfac.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    Globales.ModNotc.VPrn_cre[i].neto = (double)Result[indexResult].netofac;
                    Globales.ModNotc.VPrn_cre[i].iva = (double)Result[indexResult].ivafac;
                    Globales.ModNotc.VPrn_cre[i].monto = (double)Result[indexResult].montofac;
                    Globales.ModNotc.VPrn_cre[i].monedafac = (int)Result[indexResult].monedafac;
                    Globales.ModNotc.VPrn_cre[i].tipofac = Result[indexResult].tipofac;
                    Globales.ModNotc.VPrn_cre[i].CodDoc = 0;
                    Globales.ModNotc.VPrn_cre[i].TipSwf = 0;
                    Globales.ModNotc.VPrn_cre[i].NroSwf = 0;
                    Globales.ModNotc.VPrn_cre[i].NroMem = 0;
                    Globales.ModNotc.VPrn_cre[i].TipDoc = 3;
                }

                _retValue = true;

                return _retValue;
            }
            catch (Exception _ex)
            {
                return _retValue;
            }
        }
        
    }
}
