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
    public class Frm_Arbitrajes_Logic
    {
        #region METODOS PUBLICOS

        public static void Form_Load(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Arbitrajes Frm_Arbitrajes = initObject.Frm_Arbitrajes;
            short[] Tab1 = null;
            short x = 2;
            short a;
            short b;
            short i = 0;
            Frm_Arbitrajes.Lt_Operacion.Header = new List<string>() { "Mda. Compra", "Monto Compra", "Mda. Venta", "Monto Venta" };

            Tab1 = new short[x + 1];
            Tab1[0] = 48;
            Tab1[1] = 135;
            Tab1[2] = 165;

            //Lee los Países.
            BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.CargaEnLista_Pai(initObject.MODGTAB0, Frm_Arbitrajes.Cb_Pais, unit);
            Frm_Arbitrajes.Cb_Pais.ListIndex = Frm_Arbitrajes.Cb_Pais.Items.FindIndex(pai => pai.ID.Equals("997"));
            Frm_Arbitrajes.Cb_Pais.Enabled = false;

            //Lee las Monedas.
            b = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.SyGetn_Mnd(initObject.MODGTAB0, unit);
            if (b != 0)
            {
                for (i = 0; i < initObject.MODGTAB0.VMnd.Length; i++)
                {
                    if (initObject.MODGTAB0.VMnd[i].Mnd_MndCod != T_MODGTAB0.MndNac)
                    {
                        Frm_Arbitrajes.Cb_Moneda_Compra.Items.Add(new UI_ComboItem()
                        {
                            Value = MODGPYF1.Minuscula(initObject.MODGTAB0.VMnd[i].Mnd_MndNom),
                            ID = initObject.MODGTAB0.VMnd[i].Mnd_MndCod.ToString(),
                            Data = initObject.MODGTAB0.VMnd[i].Mnd_MndCod
                        });
                        Frm_Arbitrajes.Cb_Moneda_Venta.Items.Add(new UI_ComboItem()
                        {
                            Value = MODGPYF1.Minuscula(initObject.MODGTAB0.VMnd[i].Mnd_MndNom),
                            ID = initObject.MODGTAB0.VMnd[i].Mnd_MndCod.ToString(),
                            Data = initObject.MODGTAB0.VMnd[i].Mnd_MndCod
                        });
                    }
                }
            }
            //Carga algunos datos ya generados.-
            x = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.SyGet_Vmd(initObject.MODGTAB0, unit, DateTime.Now.ToString("yyyy-MM-dd"), initObject.MODGCVD.CodMonDolar);
            initObject.Frm_Arbitrajes.Tx_Mtoarb[0].Text = Format.FormatCurrency((initObject.MODGTAB0.VVmd.VmdObs), MODGPYF1.DecObjeto(initObject.Frm_Arbitrajes.Tx_Mtoarb[0]));
            Pr_Llena_Lt_Operacion(initObject.MODGCVD, initObject.MODGARB, Frm_Arbitrajes, initObject, unit);
        }

        public static void Cb_Moneda_Compra_Click(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            T_MODGPYF0 MODGPYF0 = initObject.MODGPYF0;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            UI_Frm_Arbitrajes Frm_Arbitrajes = initObject.Frm_Arbitrajes;

            short Indice_Moneda_Compra = 0;
            short n = 0;
            short a = 0;
            double Par = 0;
            //Control.-
            if (Frm_Arbitrajes.En_Load != 0)
            {
                return;
            }

            if (Frm_Arbitrajes.Cb_Moneda_Compra.ListIndex == -1)
            {
                return;
            }

            //Decimales para Moneda.-
            Indice_Moneda_Compra = short.Parse(Frm_Arbitrajes.Cb_Moneda_Compra.Items.ElementAt(Frm_Arbitrajes.Cb_Moneda_Compra.ListIndex).ID);
            n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, Indice_Moneda_Compra);
            if (MODGTAB0.VMnd[n].Mnd_MndSin != 0)
            {
                Frm_Arbitrajes.Tx_Mtoarb[2].Tag = "____________";
            }
            else
            {
                Frm_Arbitrajes.Tx_Mtoarb[2].Tag = "_____________.__";
            }

            Frm_Arbitrajes.En_Load = (short)(true ? -1 : 0);
            a = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.SyGet_Vmd(MODGTAB0, unit, (DateTime.Now).ToString("yyyy-MM-dd"), Indice_Moneda_Compra);
            if (a != 0)
            {
                Frm_Arbitrajes.Tx_Mtoarb[4].Text = Format.FormatCurrency((MODGTAB0.VVmd.VmdPrd), MODGPYF1.DecObjeto(Frm_Arbitrajes.Tx_Mtoarb[4]));
                // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'MODGPYF1.ValTexto(Tx_Mtoarb[4])'. Consider using the GetDefaultMember6 helper method.
                // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'MODGPYF1.ValTexto(Tx_Mtoarb[5])'. Consider using the GetDefaultMember6 helper method.
                if (Format.StringToDouble((Frm_Arbitrajes.Tx_Mtoarb[4].Text)) > 0 && Format.StringToDouble((Frm_Arbitrajes.Tx_Mtoarb[5].Text)) > 0)
                {
                    Par = (Format.StringToDouble((Frm_Arbitrajes.Tx_Mtoarb[5].Text)) / Format.StringToDouble((Frm_Arbitrajes.Tx_Mtoarb[4].Text)));
                    Frm_Arbitrajes.Tx_Mtoarb[1].Text = Format.FormatCurrency((Par), MODGPYF1.DecObjeto(Frm_Arbitrajes.Tx_Mtoarb[1]));
                }

            }
            else
            {
                Frm_Arbitrajes.Tx_Mtoarb[4].Text = Format.FormatCurrency(0, MODGPYF1.DecObjeto(Frm_Arbitrajes.Tx_Mtoarb[4]));
            }

            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'MODGPYF1.ValTexto(Tx_Mtoarb[4])'. Consider using the GetDefaultMember6 helper method.
            if (Format.StringToDouble((Frm_Arbitrajes.Tx_Mtoarb[4].Text)) == 0)
            {
                Frm_Arbitrajes.Tx_Mtoarb[4].Enabled = true;
            }
            else
            {
                Frm_Arbitrajes.Tx_Mtoarb[4].Enabled = false;
            }

            Frm_Arbitrajes.En_Load = (short)(false ? -1 : 0);
            // UPGRADE_WARNING (#80F4): The Screen6.MousePointer property sets or returns the MousePointer property of the active form, but only if it's a VB6Form.
        }

        public static void Cb_Moneda_Venta_Click(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            T_MODGPYF0 MODGPYF0 = initObject.MODGPYF0;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            UI_Frm_Arbitrajes Frm_Arbitrajes = initObject.Frm_Arbitrajes;

            short n = 0;
            short a = 0;
            short Indice_Moneda_Venta = 0;
            double Par = 0;
            //Control.-
            if (Frm_Arbitrajes.En_Load != 0)
            {
                return;
            }
            if (Frm_Arbitrajes.Cb_Moneda_Venta.ListIndex == -1)
            {
                // UPGRADE_WARNING (#80F4): The Screen6.MousePointer property sets or returns the MousePointer property of the active form, but only if it's a VB6Form.
                return;
            }


            //Decimales para Moneda.-
            Indice_Moneda_Venta = short.Parse(Frm_Arbitrajes.Cb_Moneda_Venta.Items.ElementAt(Frm_Arbitrajes.Cb_Moneda_Venta.ListIndex).ID);
            n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, Indice_Moneda_Venta);
            if (MODGTAB0.VMnd[n].Mnd_MndSin != 0)
            {
                Frm_Arbitrajes.Tx_Mtoarb[3].Tag = "_____________";
            }
            else
            {
                Frm_Arbitrajes.Tx_Mtoarb[3].Tag = "_____________.__";
            }

            Frm_Arbitrajes.En_Load = (short)(true ? -1 : 0);

            a = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.SyGet_Vmd(MODGTAB0, unit, DateTime.Now.ToString("dd/MM/yyyy"), Indice_Moneda_Venta);
            if (a != 0)
            {
                Frm_Arbitrajes.Tx_Mtoarb[5].Text = Format.FormatCurrency((MODGTAB0.VVmd.VmdPrd), MODGPYF1.DecObjeto(Frm_Arbitrajes.Tx_Mtoarb[5]));
                // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'MODGPYF1.ValTexto(Tx_Mtoarb[4])'. Consider using the GetDefaultMember6 helper method.
                // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'MODGPYF1.ValTexto(Tx_Mtoarb[5])'. Consider using the GetDefaultMember6 helper method.
                if (Format.StringToDouble((Frm_Arbitrajes.Tx_Mtoarb[4].Text)) > 0 && Format.StringToDouble((Frm_Arbitrajes.Tx_Mtoarb[5].Text)) > 0)
                {

                    Par = (Format.StringToDouble((Frm_Arbitrajes.Tx_Mtoarb[5].Text)) / Format.StringToDouble((Frm_Arbitrajes.Tx_Mtoarb[4].Text)));
                    Frm_Arbitrajes.Tx_Mtoarb[1].Text = Format.FormatCurrency((Par), MODGPYF1.DecObjeto(Frm_Arbitrajes.Tx_Mtoarb[1]));
                }

            }
            else
            {
                Frm_Arbitrajes.Tx_Mtoarb[5].Text = Format.FormatCurrency(0, MODGPYF1.DecObjeto(Frm_Arbitrajes.Tx_Mtoarb[5]));
            }

            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'MODGPYF1.ValTexto(Tx_Mtoarb[5])'. Consider using the GetDefaultMember6 helper method.
            if (Format.StringToDouble((Frm_Arbitrajes.Tx_Mtoarb[5].Text)) == 0)
            {
                Frm_Arbitrajes.Tx_Mtoarb[5].Enabled = true;
            }
            else
            {
                Frm_Arbitrajes.Tx_Mtoarb[5].Enabled = false;
            }

            Frm_Arbitrajes.En_Load = (short)(false ? -1 : 0);
        }

        public static bool Co_Boton_Click(InitializationObject initObject, UnitOfWorkCext01 unit, short Index, bool esRespuestaAPopUp, bool respuestaPopUp)
        {
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_MODGARB MODGARB = initObject.MODGARB;
            UI_Frm_Arbitrajes Frm_Arbitrajes = initObject.Frm_Arbitrajes;

            double div = 0;
            short i = 0;
            switch (Index)
            {
                case 0:  //Aceptar
                    if (esRespuestaAPopUp)
                    {
                        if (respuestaPopUp)
                        {

                        }
                        else
                        {
                            //CANCELAR
                            if (Frm_Arbitrajes.Lt_Operacion.Items.Count > 0)
                            {
                                Frm_Arbitrajes.Lt_Operacion.ListIndex = 0;
                                Lt_Operacion_Click(initObject, unit);
                            }
                            return false;
                        }
                    }
                    else
                    {
                        if (Frm_Arbitrajes.Ch_Futuro.Value != 0)
                        {
                            MODGCVD.VgCvd.Futuro = (short)(true ? -1 : 0);
                        }
                        else
                        {
                            MODGCVD.VgCvd.Futuro = (short)(false ? -1 : 0);
                        }

                        for (i = 0; i <= (short)VB6Helpers.UBound(MODGARB.VArb); i++)
                        {
                            div = MODGARB.VArb[i].PrdVta / MODGARB.VArb[i].PrdCom;
                            if (Math.Abs(MODGARB.VArb[i].PrdArb - div) > div * 0.03)
                            {
                                Frm_Arbitrajes.Confirms.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.YesNo,
                                    Text = "El valor de la paridad de arbitraje ha superado el 3% permitido del valor de la paridad de pizarra. Desea continuar? "
                                });
                            }

                        }
                        if (Frm_Arbitrajes.Confirms.Count > 0)
                        {
                            //tengo que esperar confirmacion por todos
                            return false;
                        }
                    }
                    MODGCVD.VgCvd.Acepto = (short)(true ? -1 : 0);
                    //al devolver true le estoy diciendo que debe cerrar el formulario actual
                    return true;
                case 1:  //Cancelar
                    MODGCVD.VgCvd.Acepto = (short)(false ? -1 : 0);
                    return true;
            }
            return false;
        }

        public static void Lt_Operacion_Click(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Arbitrajes Frm_arbitraje = initObject.Frm_Arbitrajes;
            short i;
            if (Frm_arbitraje.Lt_Operacion.ListIndex == -1)
            {
                i = 0;
                Pr_Limpiar_Campos(Frm_arbitraje);
            }
            else
            {
                i = short.Parse(Frm_arbitraje.Lt_Operacion.Items.ElementAt(Frm_arbitraje.Lt_Operacion.ListIndex).ID);
                Pr_Cargar_Datos_Operacion(initObject, unit, i);
            }

            if (i > 0)
            {
                Frm_arbitraje.NO.Enabled = true;
            }
            else
            {
                Frm_arbitraje.NO.Enabled = false;
            }
        }

        public static void Tx_Mtoarb_KeyPress(UI_Frm_Arbitrajes Frm_Arbitraje, short Index)
        {
            if (Frm_Arbitraje.Tx_Mtoarb[Index].Text != null)
            {
                double m = Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[Index].Text);
                //Tab.
                switch (Index)
                {
                    case 1:
                    case 4:
                    case 5:
                        Frm_Arbitraje.Tx_Mtoarb[Index].Text = Format.FormatCurrency(m, "##,###0.0000000000");
                        break;
                    case 0:
                        Frm_Arbitraje.Tx_Mtoarb[Index].Text = Format.FormatCurrency(m, "##,###0.0000");
                        break;
                    case 2:
                    case 3:
                        Frm_Arbitraje.Tx_Mtoarb[Index].Text = Format.FormatCurrency(m, "##,###0.00");
                        break;
                    default:
                        Frm_Arbitraje.Tx_Mtoarb[Index].Text = Format.FormatCurrency(m, "##,###0.00");
                        break;
                }
            }
        }

        //Calcula Monto de Venta, Paridad Arbitraje y Tipo de Cambio Usd.
        public static void Tx_Mtoarb_LostFocus(T_MODGARB MODGARB, UI_Frm_Arbitrajes Frm_Arbitraje, T_MODGTAB0 MODGTAB0, short Index)
        {
            if (Frm_Arbitraje.Tx_Mtoarb[Index].Text != null)
            {
                double tot = 0;
                double Par = 0;
                if ((Frm_Arbitraje.Tx_Mtoarb[Index].Text ?? "").Trim().Length == 0)
                {
                    Frm_Arbitraje.Tx_Mtoarb[Index].Text = "0";
                    Frm_Arbitraje.Tx_Mtoarb[Index].Text = Format.FormatCurrency(Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[Index].Text), "##,###0.00");
                    return;
                }

                tot = 0D;
                for (int i = 0; i < 6; i++)
                {
                    if (String.IsNullOrEmpty(Frm_Arbitraje.Tx_Mtoarb[i].Text))
                    {
                        Frm_Arbitraje.Tx_Mtoarb[i].Text = "0";
                    }
                }
                switch (Index)
                {
                    case 1:
                    case 2:
                        double a1 = Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[1].Text);
                        double a2 = Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[2].Text);
                        string formato = "##,###0.00";
                        if (a1 > 0 && a2 > 0)
                        {
                            tot = Math.Round(a2 * a1, 2);
                            if (MODGTAB0.VMnd.Where(c => c.Mnd_MndCod == Frm_Arbitraje.Cb_Moneda_Venta.Items[Frm_Arbitraje.Cb_Moneda_Venta.ListIndex].Data).FirstOrDefault().Mnd_MndSin == -1)
                            {
                                formato = "##,###0";
                            }
                            Frm_Arbitraje.Tx_Mtoarb[3].Text = Format.FormatCurrency(tot, formato);
                        }
                        if (Frm_Arbitraje.Cb_Moneda_Compra.ListIndex > -1 && MODGTAB0.VMnd.Where(c => c.Mnd_MndCod == Frm_Arbitraje.Cb_Moneda_Compra.Items[Frm_Arbitraje.Cb_Moneda_Compra.ListIndex].Data).FirstOrDefault().Mnd_MndSin == -1)
                        {
                            formato = "##,###0";
                        }
                        else
                        {
                            formato = "##,###0.00";
                        }
                        Frm_Arbitraje.Tx_Mtoarb[2].Text = Format.FormatCurrency(a2, formato);
                        break;
                    case 3:
                        double b1 = Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[1].Text);
                        double b2 = Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[3].Text);
                        string formato3 = "##,###0.00";
                        if (string.IsNullOrEmpty(VB6Helpers.Trim(Frm_Arbitraje.Tx_Mtoarb[1].Text)))
                        {
                            Frm_Arbitraje.Tx_Mtoarb[1].Text = MODGPYF0.forma(MODGARB.VArb[int.Parse(Frm_Arbitraje.Lt_Operacion.Items.ElementAt(Frm_Arbitraje.Lt_Operacion.ListIndex).ID)].PrdArb, MODGPYF1.FmtObjeto(Frm_Arbitraje.Tx_Mtoarb[1])).Trim();
                        }

                        if (b1 > 0 && b2 > 0)
                        {
                            tot = Math.Round(b2 / b1, 2);
                            if (MODGTAB0.VMnd.Where(c => c.Mnd_MndCod == Frm_Arbitraje.Cb_Moneda_Compra.Items[Frm_Arbitraje.Cb_Moneda_Compra.ListIndex].Data).FirstOrDefault().Mnd_MndSin == -1)
                            {
                                formato3 = "##,###0";
                            }
                            Frm_Arbitraje.Tx_Mtoarb[2].Text = Format.FormatCurrency(tot, formato3);
                        }
                        if (Frm_Arbitraje.Cb_Moneda_Venta.ListIndex > -1 && MODGTAB0.VMnd.Where(c => c.Mnd_MndCod == Frm_Arbitraje.Cb_Moneda_Venta.Items[Frm_Arbitraje.Cb_Moneda_Venta.ListIndex].Data).FirstOrDefault().Mnd_MndSin == -1)
                        {
                            formato3 = "##,###0";
                        }
                        else
                        {
                            formato3 = "##,###0.00";
                        }
                        Frm_Arbitraje.Tx_Mtoarb[3].Text = Format.FormatCurrency(b2, formato3);

                        break;
                    case 0:
                        Frm_Arbitraje.Tx_Mtoarb[0].Text = Format.FormatCurrency(Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[0].Text), "##,###0.000");
                        break;
                }

                if (Index == 4 || Index == 5)
                {
                    Par = 0D;
                    if (Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[4].Text) > 0 && Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[5].Text) > 0)
                    {

                        Par = (Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[5].Text) / Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[4].Text));

                        Frm_Arbitraje.Tx_Mtoarb[4].Text = Format.FormatCurrency(Par, "##,###0.000000000");
                    }

                }
            }
        }

        public static void NO_Click(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Arbitrajes Frm_Arbitrajes = initObject.Frm_Arbitrajes;
            short i = (short)Frm_Arbitrajes.Lt_Operacion.ListIndex;
            short j = short.Parse(Frm_Arbitrajes.Lt_Operacion.get_ItemData(i));
            short p = 0;

            if (j == -1)
            {
                return;
            }

            initObject.MODGARB.VArb[j].Status = T_MODGCVD.EstadoEli;
            if (i == -1)
            {
                p = -1;
            }
            else
            {
                p = i;
                Frm_Arbitrajes.Lt_Operacion.Items.RemoveAt(i);
                Frm_Arbitrajes.Lt_Operacion.ListIndex = p - 1;
                Lt_Operacion_Click(initObject, unit);
            }
            Pr_Llena_Lt_Operacion(initObject.MODGCVD, initObject.MODGARB, initObject.Frm_Arbitrajes, initObject, unit);
        }

        public static bool ok_Click(InitializationObject initObject, UnitOfWorkCext01 unit, bool acepta)
        {
            UI_Frm_Arbitrajes Frm_Arbitraje = initObject.Frm_Arbitrajes;
            double div = 0;
            short a = Fn_Validar_Campos(Frm_Arbitraje, 0, 8);
            if (a == 0)
            {
                return false;
            }
            short Moneda_Compra = 0;
            short Moneda_Venta = 0;
            short n = 0;
            short Indice_Arreglo = 0;

            //Si Paridad Plan Venta es distinto a 0 y Paridad Plan Compra es distinto a cero:
            if (VB6Helpers.Val(Frm_Arbitraje.Tx_Mtoarb[5].Text) != 0 && VB6Helpers.Val(Frm_Arbitraje.Tx_Mtoarb[4].Text) != 0)
            {
                //div = ParidadPlanVenta/ParidadPlanCompra.
                div = Format.StringToDouble((Frm_Arbitraje.Tx_Mtoarb[5].Text)) / Format.StringToDouble((Frm_Arbitraje.Tx_Mtoarb[4].Text));
                //Si (Paridad Arbitraje - div) > div * 0.03)
                if (Math.Abs(Format.StringToDouble((Frm_Arbitraje.Tx_Mtoarb[1].Text)) - div) > div * 0.03 && (!acepta))
                {
                    Frm_Arbitraje.Confirms.Add(new UI_Message()
                    {
                        Type = TipoMensaje.YesNo,
                        Text = "El valor de la paridad de arbitraje ha superado el 3% permitido del valor de la paridad de pizarra. Desea continuar? "
                    });
                    return false;
                }
            }

            int indice = Frm_Arbitraje.Lt_Operacion.ListIndex;
            if (indice == -1)
            {
                //Nuevo = Se adiciona Uno Más.
                n = (short)VB6Helpers.UBound(initObject.MODGARB.VArb);
                Indice_Arreglo = (short)(n + 1);
                VB6Helpers.RedimPreserve(ref initObject.MODGARB.VArb, 0, Indice_Arreglo);
            }
            else
            {
                int _switchVar2 = int.Parse(Frm_Arbitraje.Lt_Operacion.Items.ElementAt(Frm_Arbitraje.Lt_Operacion.ListIndex).ID);
                //Viejo = Se Modifica este.
                Indice_Arreglo = (short)_switchVar2;
            }
            Moneda_Compra = short.Parse(Frm_Arbitraje.Cb_Moneda_Compra.Items.ElementAt(Frm_Arbitraje.Cb_Moneda_Compra.ListIndex).ID);
            Moneda_Venta = short.Parse(Frm_Arbitraje.Cb_Moneda_Venta.Items.ElementAt(Frm_Arbitraje.Cb_Moneda_Venta.ListIndex).ID);

            if (Moneda_Compra != 11 && Moneda_Venta != 11)
            {
                if (initObject.MODGTAB0.VVmd.VmdObs == 0)
                {
                    initObject.MODGTAB0.VVmd.VmdCod = 11;
                    initObject.MODGTAB0.VVmd.VmdFec = DateTime.Now.ToString("yyyy-MM-dd");
                    Frm_Arbitraje.IrAIngresoValores = true;
                    //SI DEVUELVE FALSE Y NO TIENE NINGUN POPUP ENTONCES TIENE QUE IR A INGRESO VALORES
                    return false;
                }
            }
            return true;
        }

        public static void ok_2_Click(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            T_MODGARB MODGARB = initObject.MODGARB;
            T_MODGPYF0 MODGPYF0 = initObject.MODGPYF0;
            UI_Frm_Arbitrajes Frm_Arbitraje = initObject.Frm_Arbitrajes;

            short Indice_Arreglo = short.Parse(Frm_Arbitraje.Lt_Operacion.get_ItemData(Frm_Arbitraje.Lt_Operacion.ListIndex));
            if (Indice_Arreglo == -1)
            {
                Indice_Arreglo = (short)VB6Helpers.UBound(MODGARB.VArb);
            }

            Pr_Carga_Estructura(MODGARB, MODGCVD, MODGTAB0, Frm_Arbitraje, unit, Indice_Arreglo);
            Pr_Llena_Lt_Operacion(MODGCVD, MODGARB, Frm_Arbitraje, initObject, unit);
            Frm_Arbitraje.Ch_Futuro.Enabled = false;
            Frm_Arbitraje.Lt_Operacion.ListIndex = -1;
            Lt_Operacion_Click(initObject, unit);
            Pr_Limpiar_Campos(Frm_Arbitraje);
        }
        #endregion

        #region METODOS PRIVADOS
        //****************************************************************************
        //   1.  Llena la lista Lt_Operacion.
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        private static void Pr_Llena_Lt_Operacion(T_MODGCVD MODGCVD, T_MODGARB MODGARB, UI_Frm_Arbitrajes Frm_Arbitraje, InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            short n = 0;
            short i = 0;
            n = (short)VB6Helpers.UBound(MODGARB.VArb);
            Frm_Arbitraje.Lt_Operacion.Items.Clear();
            if (n >= 0)
            {
                for (i = 0; i <= (short)n; i++)
                {
                    if (MODGARB.VArb[i].Status != T_MODGCVD.EstadoEli)
                    {
                        Frm_Arbitraje.Lt_Operacion.Items.Add(Fn_Linea_Lt_Oper(MODGARB, MODGCVD, i));
                    }

                }
            }
            Frm_Arbitraje.Lt_Operacion.ListIndex = -1;
            Lt_Operacion_Click(initObject, unit);
        }


        //****************************************************************************
        //   1.  Retorna una linea para la lista de Operaciones.
        //****************************************************************************
        private static UI_GridItem Fn_Linea_Lt_Oper(T_MODGARB MODGARB, T_MODGCVD MODGCVD, short Indice)
        {
            UI_GridItem item = new UI_GridItem();
            item.AddColumn("mdacom", VB6Helpers.Trim(MODGARB.VArb[Indice].NemMndC));
            item.AddColumn("moncom", MODGPYF0.forma(MODGARB.VArb[Indice].MtoCom, T_MODGCVD.FormatoDec));
            item.AddColumn("mdaven", VB6Helpers.Trim(MODGARB.VArb[Indice].NemMndV));
            item.AddColumn("monven", MODGPYF0.forma(MODGARB.VArb[Indice].MtoVta, T_MODGCVD.FormatoDec));
            item.ID = Indice.ToString();
            return item;
        }

        //****************************************************************************
        //   1.  Carga la Estructura de VArb() que será la que actualiza la
        //       Lista de Operaciones.
        //****************************************************************************
        private static void Pr_Carga_Estructura(T_MODGARB MODGARB, T_MODGCVD MODGCVD, T_MODGTAB0 MODGTAB0, UI_Frm_Arbitrajes Frm_Arbitraje, UnitOfWorkCext01 unit, short Indice)
        {
            short Moneda_Venta;
            short N_Compra;
            short N_Venta;
            short Moneda_Compra;

            Moneda_Compra = short.Parse(Frm_Arbitraje.Cb_Moneda_Compra.Items.ElementAt(Frm_Arbitraje.Cb_Moneda_Compra.ListIndex).ID);
            Moneda_Venta = short.Parse(Frm_Arbitraje.Cb_Moneda_Venta.Items.ElementAt(Frm_Arbitraje.Cb_Moneda_Venta.ListIndex).ID);

            MODGARB.VArb[Indice].codope = MODGCVD.VgCvd.codope;
            MODGARB.VArb[Indice].codpai = short.Parse(Frm_Arbitraje.Cb_Pais.Items.ElementAt(Frm_Arbitraje.Cb_Pais.ListIndex).ID);
            MODGARB.VArb[Indice].MndCom = Moneda_Compra;
            MODGARB.VArb[Indice].MndVta = Moneda_Venta;

            //Nemónicos de las Monedas Compra - Venta.
            N_Compra = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, Moneda_Compra);
            N_Venta = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, Moneda_Venta);
            MODGARB.VArb[Indice].NemMndC = MODGTAB0.VMnd[N_Compra].Mnd_MndNmc;
            MODGARB.VArb[Indice].NemMndV = MODGTAB0.VMnd[N_Venta].Mnd_MndNmc;

            //Mtos Compra - Venta.
            MODGARB.VArb[Indice].TipCam = Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[0].Text);
            MODGARB.VArb[Indice].PrdArb = Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[1].Text);

            //Se cargan variables de compra
            //monto compra
            MODGARB.VArb[Indice].MtoCom = Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[2].Text);

            //paridad compra
            MODGARB.VArb[Indice].PrdCom = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB1.SyGet_Vmc(MODGTAB0, unit, Moneda_Compra, DateTime.Now.ToString("yyyy-MM-dd"), "P");
            if (MODGARB.VArb[Indice].PrdCom == 0)
            {
                MODGARB.VArb[Indice].PrdCom = Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[4].Text);
            }

            //monto en dolares de la compra
            MODGARB.VArb[Indice].DolCom = Format.StringToDouble(Format.FormatCurrency(((MODGARB.VArb[Indice].MtoCom / MODGARB.VArb[Indice].PrdCom)), "0.00"));

            //tipo de cambio de la compra
            MODGARB.VArb[Indice].CamCom = Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[0].Text);

            //monto en pesos de la compra
            MODGARB.VArb[Indice].MtoPes = Format.StringToDouble(Format.FormatCurrency((MODGARB.VArb[Indice].DolCom * MODGARB.VArb[Indice].CamCom), "0"));

            //Se cargan datos de la Venta
            //Monto venta
            MODGARB.VArb[Indice].MtoVta = Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[3].Text);

            //paridad de la venta
            MODGARB.VArb[Indice].PrdVta = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB1.SyGet_Vmc(MODGTAB0, unit, Moneda_Venta, DateTime.Now.ToString("yyyy-MM-dd"), "P");
            if (MODGARB.VArb[Indice].PrdVta == 0)
            {
                MODGARB.VArb[Indice].PrdVta = Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[5].Text);
            }

            //monto de la venta en dolares
            MODGARB.VArb[Indice].DolVta = MODGARB.VArb[Indice].MtoVta / MODGARB.VArb[Indice].PrdVta;

            //tipo de cambio de la venta
            MODGARB.VArb[Indice].CamVta = MODGARB.VArb[Indice].MtoPes / MODGARB.VArb[Indice].DolVta;
            MODGARB.VArb[Indice].Status = T_MODGCVD.EstadoIng;
        }

        //****************************************************************************
        //   1.  Valida los campos de la pantalla retornando el control del error al
        //       campo determinado.
        //****************************************************************************
        private static short Fn_Validar_Campos(UI_Frm_Arbitrajes Frm_Arbitraje, short CampoInicial, short CampoFinal)
        {
            short i = 0;
            string Paso = "";
            for (i = (short)CampoInicial; i <= (short)CampoFinal; i++)
            {
                switch (i)
                {

                    case 0:  //Tipo de Cambio
                        if (Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[0].Text) == 0)
                        {
                            Frm_Arbitraje.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Falta Ingresar el tipo de cambio.",
                                ControlName = "TipoC"
                            });
                            return 0;
                        }

                        break;
                    case 1:  //Cb_Moneda_Compra
                        if (Frm_Arbitraje.Cb_Moneda_Compra.ListIndex == -1)
                        {
                            Frm_Arbitraje.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Falta Seleccionar una Moneda de Compra.",
                                ControlName = "Cb_Moneda_Compra"
                            });
                            return 0;
                        }

                        break;
                    case 2:  //Cb_Moneda_Venta
                        if (Frm_Arbitraje.Cb_Moneda_Venta.ListIndex == -1)
                        {
                            Frm_Arbitraje.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Falta Seleccionar una Moneda de Venta.",
                                ControlName = "Cb_Moneda_Venta"
                            });
                            return 0;
                        }

                        break;
                    case 3:  //Tx_Mtoarb(1).text
                        if (Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[1].Text) == 0)
                        {
                            Frm_Arbitraje.Tx_Mtoarb[1].Enabled = true;
                            Frm_Arbitraje.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Falta Ingresar la Paridad del Arbitraje.",
                                ControlName = "Tx_Paridad_Arbitraje"
                            });
                            return 0;
                        }

                        break;
                    case 4:  //Tx_Mtoarb(2).text
                        if (Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[2].Text) == 0)
                        {
                            Frm_Arbitraje.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Falta Ingresar el Monto de Compra.",
                                ControlName = "Tx_Monto_Compra"
                            });
                            return 0;
                        }

                        break;
                    case 5:  //Tx_Mtoarb(3).text
                        if (Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[3].Text) == 0)
                        {
                            Frm_Arbitraje.Tx_Mtoarb[0].Enabled = true;
                            Frm_Arbitraje.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Falta el Monto de Venta.",
                                ControlName = "Tx_Monto_Venta"
                            });
                            return 0;
                        }

                        break;
                    case 6:  //Valida Moneda Iguales en el Arbitraje.-
                        if ((Frm_Arbitraje.Cb_Moneda_Compra.ListIndex == Frm_Arbitraje.Cb_Moneda_Venta.ListIndex) && (Frm_Arbitraje.Cb_Moneda_Compra.ListIndex != -1))
                        {
                            Frm_Arbitraje.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Para efectuar un Arbitraje debe seleccionar monedas diferentes.",
                                ControlName = "Cb_Moneda_Compra"
                            });
                            return 0;
                        }

                        break;
                    case 7:  //Tx_Mtoarb(4).text
                        if (Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[4].Text) == 0)
                        {
                            Frm_Arbitraje.Tx_Mtoarb[4].Enabled = true;
                            Frm_Arbitraje.Tx_Mtoarb[5].Enabled = true;
                            Frm_Arbitraje.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Debe Ingresar la Paridad de la Moneda de Compra " + Paso,
                                ControlName = "Tx_Paridad_Plan_Compra"
                            });
                            return 0;
                        }

                        if (Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[5].Text) == 0)
                        {
                            Frm_Arbitraje.Tx_Mtoarb[5].Enabled = true;
                            Frm_Arbitraje.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Debe Ingresar la Paridad de la Moneda de Venta. " + Paso,
                                ControlName = "Tx_Paridad_Plan_Venta"
                            });
                            return 0;
                        }
                        else
                        {
                            Frm_Arbitraje.Tx_Mtoarb[5].Enabled = false;
                        }

                        break;
                    case 8:  //Lista de pais
                        if (Frm_Arbitraje.Cb_Pais.ListIndex == -1)
                        {
                            Frm_Arbitraje.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Para efectuar un Arbitraje debe seleccionar pais.",
                                ControlName = "Cb_Pais"
                            });
                            return 0;
                        }
                        break;
                }
            }
            return 1;
        }

        //****************************************************************************
        //   1.  Carga los Datos desde la Estructura de VArb() hacia los campos en
        //       Pantalla.
        //****************************************************************************
        private static void Pr_Cargar_Datos_Operacion(InitializationObject initObject, UnitOfWorkCext01 unit, short Indice_Oper)
        {
            short x = 0;
            T_MODGARB MODGARB = initObject.MODGARB;
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            UI_Frm_Arbitrajes Frm_Arbitraje = initObject.Frm_Arbitrajes;

            Frm_Arbitraje.Cb_Moneda_Compra.ListIndex = Frm_Arbitraje.Cb_Moneda_Compra.Items.FindIndex(mon => mon.ID.Equals(MODGARB.VArb[Indice_Oper].MndCom.ToString()));
            Cb_Moneda_Compra_Click(initObject, unit);
            Frm_Arbitraje.Cb_Moneda_Venta.ListIndex = Frm_Arbitraje.Cb_Moneda_Venta.Items.FindIndex(mon => mon.ID.Equals(MODGARB.VArb[Indice_Oper].MndVta.ToString()));
            Cb_Moneda_Venta_Click(initObject, unit);

            //Mtos Compra - Venta.
            Frm_Arbitraje.Tx_Mtoarb[1].Text = VB6Helpers.Trim(MODGPYF0.forma(MODGARB.VArb[Indice_Oper].PrdArb, MODGPYF1.FmtObjeto(Frm_Arbitraje.Tx_Mtoarb[1])));
            Frm_Arbitraje.Tx_Mtoarb[2].Text = Format.FormatCurrency((MODGARB.VArb[Indice_Oper].MtoCom), MODGPYF1.DecObjeto(Frm_Arbitraje.Tx_Mtoarb[2]));
            Frm_Arbitraje.Tx_Mtoarb[3].Text = Format.FormatCurrency(MODGARB.VArb[Indice_Oper].MtoVta, MODGPYF1.DecObjeto(Frm_Arbitraje.Tx_Mtoarb[3]));
            Frm_Arbitraje.Tx_Mtoarb[4].Text = Format.FormatCurrency((MODGARB.VArb[Indice_Oper].PrdCom), MODGPYF1.DecObjeto(Frm_Arbitraje.Tx_Mtoarb[4]));
            Frm_Arbitraje.Tx_Mtoarb[5].Text = Format.FormatCurrency((MODGARB.VArb[Indice_Oper].PrdVta), MODGPYF1.DecObjeto(Frm_Arbitraje.Tx_Mtoarb[5]));

            if (MODGARB.VArb[Indice_Oper].TipCam == 0)
            {
                x = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.SyGet_Vmd(MODGTAB0, unit, DateTime.Now.ToString("yyyy-MM-dd"), MODGCVD.CodMonDolar);
                Frm_Arbitraje.Tx_Mtoarb[0].Text = Format.FormatCurrency((MODGTAB0.VVmd.VmdObs), MODGPYF1.DecObjeto(Frm_Arbitraje.Tx_Mtoarb[0]));
            }
            else
            {
                Frm_Arbitraje.Tx_Mtoarb[0].Text = Format.FormatCurrency((MODGARB.VArb[Indice_Oper].TipCam), MODGPYF1.DecObjeto(Frm_Arbitraje.Tx_Mtoarb[0]));
            }
        }

        private static void Pr_Limpiar_Campos(UI_Frm_Arbitrajes Frm_Arbitrajes)
        {
            Frm_Arbitrajes.Cb_Moneda_Compra.ListIndex = -1;
            Frm_Arbitrajes.Cb_Moneda_Venta.ListIndex = -1;
            Frm_Arbitrajes.Ch_Futuro.Value = 0;
            //Frm_Arbitrajes.Tx_Mtoarb[0].Text = String.Empty;
            Frm_Arbitrajes.Tx_Mtoarb[1].Text = String.Empty;
            Frm_Arbitrajes.Tx_Mtoarb[2].Text = String.Empty;
            Frm_Arbitrajes.Tx_Mtoarb[3].Text = String.Empty;
            Frm_Arbitrajes.Tx_Mtoarb[4].Text = String.Empty;
            Frm_Arbitrajes.Tx_Mtoarb[5].Text = String.Empty;
        }
        #endregion
    }
}
