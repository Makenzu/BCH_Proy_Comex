using BCH.Comex.Core.BL.XGGL.Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Swift;
using System;
using System.Collections.Generic;
namespace BCH.Comex.Core.BL.XGGL.Forms
{
    public class FrmGlanu
    {
        public static void Aceptar_Click(string Identificacion_Rut, DatosGlobales Globales, UnitOfWorkCext01 uow, UnitOfWorkSwift uowSwift)
        {
            int X = 0;
            int i = 0;
            int k = 0;
            int n = 0;
            int p = 0;
            string FecPln = string.Empty;
            int NumPln = 0;
            int a = 0;

            if (MODGLANU.SyPute_Gl((Globales.FrmGlanu.Tx_NroRep.Text.ToDbl()).ToInt(), Globales.FrmGlanu.Tx_FecRep.Text.TrimB(), Globales, uow))
            {
                // OGG    16 Diciembre 1997.-
                a = MODGCHQ.SyAnu_Chq(Globales.MODGCON1.V_IMch.CodCct + Globales.MODGCON1.V_IMch.CodPro + Globales.MODGCON1.V_IMch.CodEsp + Globales.MODGCON1.V_IMch.CodOfi + Globales.MODGCON1.V_IMch.CodOpe, "000001", Globales, uow);
                // OGG    16 Diciembre 1997.-
                NumPln = Globales.FrmGlanu.Tx_NroRep.Text.ToInt();

                FecPln = Globales.FrmGlanu.Tx_FecRep.Text;
                DateTime dateFecPln;
                if (!DateTime.TryParse(FecPln, out dateFecPln))
                {
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message
                    {
                        Title= "Contabilidad Genérica",
                        Text = "Debe ingresar una fecha correcta. {0} no es válida",
                        Type = Common.UI_Modulos.TipoMensaje.Error
                    });
                    return;
                }

                // ****** ANULACIÓN DE SWIFT DE LA BASE SWIFT Y COMEX ***********
                Globales.SYGETPRT.Codop.Cent_costo = Globales.MODGCON1.V_IMch.CodCct;
                Globales.SYGETPRT.Codop.Id_Product = Globales.MODGCON1.V_IMch.CodPro;
                Globales.SYGETPRT.Codop.Id_Especia = Globales.MODGCON1.V_IMch.CodEsp;
                Globales.SYGETPRT.Codop.Id_Empresa = Globales.MODGCON1.V_IMch.CodOfi;
                Globales.SYGETPRT.Codop.Id_Operacion = Globales.MODGCON1.V_IMch.CodOpe;
                p = Modswen.Fn_GetMts(1, "R", "0", "0", "0", "0", n.Str(), Globales, uow);
                Globales.Modswen.RutAis = Identificacion_Rut;
                k = 0;
                k = Globales.Modswen.VMts.GetUpperBound(0);
                
                if (k >= 0)
                {
                    for (i = 0; i <= k; i += 1)
                    {
                        Modswen.Fn_AnulaSwift(Globales.MODGCON1.V_IMch.CodCct, Globales.Modswen.VMts[i].id_mensaje, "Anulación de Cancelación de Cobranza", Globales, uowSwift);
                        Modswen.Fn_BorraSwiCo(Globales.MODGCON1.V_IMch.CodCct + Globales.MODGCON1.V_IMch.CodPro + Globales.MODGCON1.V_IMch.CodEsp + Globales.MODGCON1.V_IMch.CodOfi + Globales.MODGCON1.V_IMch.CodOpe, 0, 0, 0, 0, n, Globales.Modswen.VMts[i].id_mensaje, Globales, uow);
                    }
                }
                // **************************************************************
                MODGCON1.Pr_Imprime_Contab80(Globales, NumPln, dateFecPln.ToString("yyyy-MM-dd"));
                Globales.FrmGlanu.ListaDatos.Clear();
                //this.Boton[0].Enabled = false;
                Globales.FrmGlanu.Tx_Cliente.Text = string.Empty;
                Globales.FrmGlanu.Tx_FecRep.Text = string.Empty;
                Globales.FrmGlanu.Tx_NroRep.Text = string.Empty;
                //Una vez terminada la anulación, es necesario limpiar el numero de opración para que tome el número que corresponde y no el de la operación annulada
                Globales.SYGETPRT.Codop.Id_Operacion = string.Empty;
            }

        }
        private static bool Fn_Valida_Campos(DatosGlobales Globales)
        {
            if (Globales.FrmGlanu.Tx_NroRep.Text.TrimB() == string.Empty)
            {
                Globales.FrmGlanu.ListaErrores.Add(new Common.UI_Modulos.UI_Message()
                {
                    Type = Common.UI_Modulos.TipoMensaje.Error,
                    Text = "Es necesario que se ingrese el Nro. del Reporte Contable.",
                    Title = "Anulación de Contabilidad Genérica",
                    ControlName = "Tx_NroRep",
                    AutoClose = true
                });
                return false;
            }

            if (Globales.FrmGlanu.Tx_FecRep.Text.TrimB() == string.Empty)
            {
                Globales.FrmGlanu.ListaErrores.Add(new Common.UI_Modulos.UI_Message()
                {
                    Type = Common.UI_Modulos.TipoMensaje.Error,
                    Text = "Es necesario que se ingrese la Fecha del Reporte Contable.",
                    Title = "Anulación de Contabilidad Genérica",
                    ControlName = "Tx_FecRep",
                    AutoClose = true
                });
                return false;
            }
            return true;
        }
        public static void ok_Click(DatosGlobales Globales, UnitOfWorkCext01 uow)
        {
            int c = 0;
            int b = 0;

            if (!Fn_Valida_Campos(Globales))
            {
                return;
            }
            //Leer Reporte Contabilidad Genérica.
            b = MODGCON1.SyGet_xMch((Globales.FrmGlanu.Tx_NroRep.Text.ToDbl()).ToInt(), Globales.FrmGlanu.Tx_FecRep.Text.TrimB(), Globales, uow);
            if (b != 0)
            {
                //Lee Detalle Reporte Contabilidad Genérica.
                c = MODGCON1.SyGetn_xMcd((Globales.FrmGlanu.Tx_NroRep.Text.ToDbl()).ToInt(), Globales.FrmGlanu.Tx_FecRep.Text.TrimB(), Globales, uow);
            }
            if (string.IsNullOrWhiteSpace(Globales.MODGCON1.V_IMch.CodPro.TrimB()))
            {
                Globales.FrmGlanu.ListaErrores.Add(new Common.UI_Modulos.UI_Message()
                {
                    Type = Common.UI_Modulos.TipoMensaje.Error,
                    Text = "No existe el Reporte Contable Nro : " + Globales.FrmGlanu.Tx_NroRep.Text.TrimB() + " para la fecha indicada, por lo tanto debe consultar nuevamente por otro Reporte Contable.",
                    Title = "Anulación de Contabilidad Genérica",
                    AutoClose = true
                });
                
                if (Globales.FrmGlanu.ListaDatos != null) { Globales.FrmGlanu.ListaDatos.Clear(); }
                Globales.FrmGlanu.Tx_FecRep.Text = string.Empty;
                Globales.FrmGlanu.Tx_Cliente.Text = string.Empty;
                return;
            }
            
            if (Globales.MODGCON1.V_IMch.CodPro.TrimB() != T_MODGUSR.IdPro_ConGen)
            {
                Globales.FrmGlanu.ListaErrores.Add(new Common.UI_Modulos.UI_Message()
                {
                    Type = Common.UI_Modulos.TipoMensaje.Error,
                    Text = "El Reporte Contable solicitado no se puede anular debido a que NO pertenecen al producto Contabilidad Genérica.",
                    Title = "Anulación de Contabilidad Genérica",
                    AutoClose = true
                });
                Globales.FrmGlanu.Tx_NroRep.Text = string.Empty;
                Globales.FrmGlanu.Tx_FecRep.Text = string.Empty;
                return;
            }
            else
            {
                if (Globales.MODGCON1.V_IMch.estado == T_MODGCON0.ECC_ANU)
                {
                    Globales.FrmGlanu.ListaErrores.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type = Common.UI_Modulos.TipoMensaje.Error,
                        Text = "El Reporte Contable solicitado ya se encuentra anulado.",
                        Title = "Anulación de Contabilidad Genérica",
                        AutoClose = true
                    });
                    Globales.FrmGlanu.Tx_NroRep.Text = string.Empty;
                    Globales.FrmGlanu.Tx_FecRep.Text = string.Empty;
                    return;
                }
                else
                {
                    if (MODGLANU.LineasYaInyectadasEnOperacion(uow, Globales.MODGCON1.V_IMch.CodCct, Globales.MODGCON1.V_IMch.CodPro, Globales.MODGCON1.V_IMch.CodEsp, Globales.MODGCON1.V_IMch.CodOfi, Globales.MODGCON1.V_IMch.CodOpe))
                    {
                        Globales.FrmGlanu.ListaErrores.Add(new Common.UI_Modulos.UI_Message()
                        {
                            Text = "No se puede anular una operación que tiene realizados abonos o cargos.",
                            Type = Common.UI_Modulos.TipoMensaje.Error,
                            AutoClose = true
                        });
                        return;
                    }
                }
            }

            Globales.FrmGlanu.Tx_Cliente.Text = Globales.MODGCON1.V_IMch.NomCli.TrimB();
            Pr_Cargar_Lista(Globales);
        }
        private static void Pr_Cargar_Lista(DatosGlobales Globales)
        {
            int n = 0;
            n = Globales.MODGCON1.V_IMcd.GetUpperBound(0);
            
            string CuentaStr = string.Empty;
            string MonedaStr = string.Empty;
            string DebeStr = string.Empty;
            string HaberStr = string.Empty;

            Globales.FrmGlanu.ListaDatos = new List<FrmGlanuMovimientoDTO>();
            for (int i = 0; i <= n; i += 1)
            {
                CuentaStr = Globales.MODGCON1.V_IMcd[i].numcta.Mid(1, 3) + "." + Globales.MODGCON1.V_IMcd[i].numcta.Mid(4, 2) + "." + Globales.MODGCON1.V_IMcd[i].numcta.Mid(6, 2) + "-" + Globales.MODGCON1.V_IMcd[i].numcta.Mid(8, 1);
                MonedaStr = Globales.MODGCON1.V_IMcd[i].NemMon;

                // ----------------------------
                // Determina si es Debe o Haber.
                // ----------------------------
                switch (Globales.MODGCON1.V_IMcd[i].cod_dh.UCase().TrimB())
                {
                    case "D":
                        DebeStr = Utils.Format.FormatCurrency(Globales.MODGCON1.V_IMcd[i].mtomcd, "#,###,###,###,##0.00");
                        HaberStr = Utils.Format.FormatCurrency(0, "#,###,###,###,##0.00");
                        break;
                    case "H":
                        DebeStr = Utils.Format.FormatCurrency(0, "#,###,###,###,##0.00");
                        HaberStr = Utils.Format.FormatCurrency(Globales.MODGCON1.V_IMcd[i].mtomcd, "#,###,###,###,##0.00");
                        break;
                }

                Globales.FrmGlanu.ListaDatos.Add(
                    new FrmGlanuMovimientoDTO()
                    {
                        Cuenta = CuentaStr,
                        Moneda = MonedaStr,
                        Debe = DebeStr,
                        Haber = HaberStr
                    });
            }
        }
    }
}
