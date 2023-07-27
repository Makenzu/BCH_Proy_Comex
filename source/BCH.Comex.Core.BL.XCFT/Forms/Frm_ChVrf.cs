using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public static class Frm_ChVrf
    {
        private static short IndVgChV;
        private static string FtoSal = "";
        private static short ModificaPln;
        private static short EjecProcMon;
        
        public static void CargaEnListaTcp(InitializationObject initObj, UI_Combo Lista, string Tipo)
        {
            //   Deja el arreglo de Conceptos de Planillas VTcp() en una lista.
            //****************************************************************************
            short n = 0;
            short i = 0;
            string s = "";
            short Cargar = 0;
            n = (short)VB6Helpers.UBound(initObj.ModChVrf.VCcpl);

            VB6Helpers.Invoke(VB6Helpers.CObj(Lista), "Clear");
            for (i = 1; i <= (short)n; i++)
            {
                if (Format.StringToDouble(initObj.ModChVrf.VCcpl[i].tipope) >= 110 && Format.StringToDouble(initObj.ModChVrf.VCcpl[i].tipope) <= 240)
                {
                    Cargar = (short)(false ? -1 : 0);
                    switch (Tipo)
                    {
                        case "I":  //Ingresos
                            if (initObj.ModChVrf.VCcpl[i].flging != 0)
                            {
                                Cargar = (short)(true ? -1 : 0);
                            }
                            break;
                        case "E":  //Egresos
                            if (~initObj.ModChVrf.VCcpl[i].flging != 0)
                            {
                                Cargar = (short)(true ? -1 : 0);
                            }
                            break;
                    }

                    if (Cargar != 0)
                    {
                        s = "";
                        s = s + VB6Helpers.Trim(initObj.ModChVrf.VCcpl[i].CodCom + "-" + initObj.ModChVrf.VCcpl[i].CptCom) + VB6Helpers.Space(2);
                        s += VB6Helpers.Trim(VB6Helpers.Mid(MODGPYF1.Minuscula(initObj.ModChVrf.VCcpl[i].DesCom), 1, 55));
                        Lista.Items.Add(new UI_ComboItem() { Data = i, Value = s, ID = i.ToString() });
                    }

                }

            }

        }
        public static void Bt_Aceptar_Click(InitializationObject initObj)
        {
            initObj.ModChVrf.AceptoPantallaChVrf = 0;
            short b = 0;
            short i = 0;
            double suma = 0;
            short j = 0;
            short a = 0;
            //Llena estructura del saldo del destino de fondo.
            a = (short)VB6Helpers.UBound(initObj.ModChVrf.Aux_VgChV);
            //Llena estructura en memoria respecto al listado de Planilla de Transferencia.
            b = 0;
            b = (short)VB6Helpers.UBound(initObj.ModChVrf.Aux_VPlnChV);

            for (i = 0; i <= (short)a; i++)
            {
                suma = 0;
                for (j = 0; j <= (short)b; j++)
                {
                    if (initObj.ModChVrf.Aux_VgChV[i].CodMon == initObj.ModChVrf.Aux_VPlnChV[j].CodMon)
                    {
                        suma += initObj.ModChVrf.Aux_VPlnChV[j].Monto;
                    }
                }
                if (initObj.ModChVrf.Aux_VgChV[i].Saldo > suma)
                {
                    if (initObj.ModChVrf.Aux_VgChV[i].Cod_IE == "E")
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "Existe diferencia entre el monto definido en el destino y el total de las planillas para la moneda " + initObj.ModChVrf.Aux_VgChV[i].NemMon + ".",
                            Title = "Atención",
                            Type = TipoMensaje.Informacion
                        });
                    }
                    else if (initObj.ModChVrf.Aux_VgChV[i].Cod_IE == "I")
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "Existe diferencia entre el monto definido en el origen y el total de las planillas para la moneda " + initObj.ModChVrf.Aux_VgChV[i].NemMon + ".",
                            Title = "Atención",
                            Type = TipoMensaje.Informacion
                        });
                    }
                    return;
                }
            }

            initObj.ModChVrf.Aux_VgChV[IndVgChV].Saldo = suma;

            VB6Helpers.RedimPreserve(ref initObj.ModChVrf.VPlnChV, 0, b);
            for (i = 0; i <= (short)b; i++)
            {
                if (initObj.ModChVrf.Aux_VPlnChV[i].estado != 9)
                {
                    initObj.ModChVrf.VPlnChV[i] = initObj.ModChVrf.Aux_VPlnChV[i];
                }
            }

            initObj.ModChVrf.AceptoPantallaChVrf = (short)(true ? -1 : 0);
            BCH.Comex.Core.BL.XCFT.Forms.Frm_Destino_Fondos_Logic.Bt_PlnTrn_Click(initObj);

        }
        public static void Bt_Cancelar_Click(InitializationObject initObj)
        {
            initObj.ModChVrf.AceptoPantallaChVrf = -1;
            BCH.Comex.Core.BL.XCFT.Forms.Frm_Destino_Fondos_Logic.Bt_PlnTrn_Click(initObj);
        }
        public static void Bt_Eliminar_Click(InitializationObject initObj)
        {
            short a = 0;
            short j = 0;
            string s = "";
            short i = 0;

            a = (short)VB6Helpers.UBound(initObj.ModChVrf.Aux_VPlnChV);

            if (initObj.Frm_ChVrf.ListaPlanillas.ListCount == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "No existen elementos en la lista.",
                    Title = "Atención",
                    Type = TipoMensaje.Informacion
                });
                return;
            }

            if (initObj.Frm_ChVrf.ListaPlanillas.ListCount > 0)
            {
                if (initObj.Frm_ChVrf.ListaPlanillas.ListIndex != -1)
                {
                    j = (short)initObj.Frm_ChVrf.ListaPlanillas.get_ItemData((short)initObj.Frm_ChVrf.ListaPlanillas.ListIndex);
                    initObj.ModChVrf.Aux_VPlnChV[j].estado = 9;
                    initObj.ModChVrf.Aux_VgChV[IndVgChV].Saldo += initObj.ModChVrf.Aux_VPlnChV[j].Monto;
                    initObj.Frm_ChVrf.Lb_Saldo.Text = Format.FormatCurrency(initObj.ModChVrf.Aux_VgChV[IndVgChV].Saldo, FtoSal);
                    initObj.Frm_ChVrf.ListaPlanillas.Items.Clear();

                    for (i = 0; i <= (short)a; i++)
                    {
                        if (initObj.ModChVrf.Aux_VPlnChV[i].estado != 9)
                        {
                            s = "";
                            s = s + VB6Helpers.Trim(initObj.ModChVrf.Aux_VPlnChV[i].CodCom + "-" + initObj.ModChVrf.Aux_VPlnChV[i].CptCom) + VB6Helpers.Chr(9);
                            s = s + VB6Helpers.Trim(initObj.ModChVrf.Aux_VPlnChV[i].NemMon) + VB6Helpers.Chr(9);
                            s = s + MODGPYF0.forma(initObj.ModChVrf.Aux_VPlnChV[i].Monto, FtoSal) + VB6Helpers.Chr(9);
                            s += VB6Helpers.Trim(VB6Helpers.Mid(MODGPYF1.Minuscula(initObj.ModChVrf.Aux_VPlnChV[i].DesCom), 1, 55));
                            initObj.Frm_ChVrf.ListaPlanillas.Items.Add(new UI_ListBoxItem() { Value = s });
                        }
                    }
                    ModificaPln = (short)(false ? -1 : 0);
                }
                else
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "Debe seleccionar un elemento de la lista",
                        Title = "Atención",
                        Type = TipoMensaje.Informacion
                    });
                    return;
                }
            }
            initObj.Frm_ChVrf.ListaPlanillas.ListIndex = -1;
            initObj.Frm_ChVrf.Cbo_CptoPln.ListIndex = -1;
            initObj.Frm_ChVrf.CbPais.ListIndex = -1;
            initObj.Frm_ChVrf.Tx_Monto.Text = Format.FormatCurrency(0, FtoSal);
        }
        public static void Bt_OK_Click(InitializationObject initObj)
        {
            short a = 0;
            short i = 0;
            string s = "";
            double Saldo = 0;
            short j = 0;

            //Llena listado Planillas transferencia.
            a = (short)VB6Helpers.UBound(initObj.ModChVrf.Aux_VPlnChV);

            if (initObj.Frm_ChVrf.Cbo_Moneda.ListIndex == -1)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe seleccionar la Moneda",
                    Title = "Atención",
                    Type = TipoMensaje.Informacion
                });
                return;
            }
            if (initObj.Frm_ChVrf.Cbo_CptoPln.ListIndex == -1)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe seleccionar el Código de Comercio",
                    Title = "Atención",
                    Type = TipoMensaje.Informacion
                });
                return;
            }
            if (initObj.Frm_ChVrf.CbPais.ListIndex == -1)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe seleccionar el País",
                    Title = "Atención",
                    Type = TipoMensaje.Informacion
                });
                return;
            }
            if (Format.StringToDouble(initObj.Frm_ChVrf.Lb_Saldo.Text) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "No hay saldo disponible.",
                    Title = "Atención",
                    Type = TipoMensaje.Informacion
                });
                initObj.Frm_ChVrf.Tx_Monto.Text = Format.FormatCurrency(0, FtoSal);
                return;
            }

            if (Format.StringToDouble(initObj.Frm_ChVrf.Tx_Monto.Text) > Format.StringToDouble(initObj.Frm_ChVrf.Lb_Saldo.Text))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "El monto ingresado no puede ser mayor que saldo",
                    Title = "Atención",
                    Type = TipoMensaje.Informacion
                });
                initObj.Frm_ChVrf.Tx_Monto.Text = Format.FormatCurrency(0, FtoSal);
                return;
            }
            if (Format.StringToDouble(initObj.Frm_ChVrf.Tx_Monto.Text) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe ingresar el monto para el concepto seleccionado",
                    Title = "Atención",
                    Type = TipoMensaje.Informacion
                });
                return;
            }
            initObj.ModChVrf.Aux_VgChV[IndVgChV].Saldo = Format.StringToDouble(initObj.Frm_ChVrf.Lb_Saldo.Text);  //ValTexto(Lb_Saldo)

            if (~ModificaPln != 0)
            {
                i = (short)(a + 1);
                VB6Helpers.RedimPreserve(ref initObj.ModChVrf.Aux_VPlnChV, 0, i);
                initObj.ModChVrf.Aux_VPlnChV[i].Monto = Format.StringToDouble(initObj.Frm_ChVrf.Tx_Monto.Text);  //ValTexto(Tx_Monto)
                initObj.ModChVrf.Aux_VPlnChV[i].NemMon = initObj.ModChVrf.Aux_VgChV[IndVgChV].NemMon;
                initObj.ModChVrf.Aux_VPlnChV[i].CodMon = initObj.ModChVrf.Aux_VgChV[IndVgChV].CodMon;
                initObj.ModChVrf.Aux_VPlnChV[i].IdParty = initObj.ModChVrf.Aux_VgChV[IndVgChV].IdParty;
                initObj.ModChVrf.Aux_VPlnChV[i].IdNom = initObj.ModChVrf.Aux_VgChV[IndVgChV].IdNom;
                initObj.ModChVrf.Aux_VPlnChV[i].IdDir = initObj.ModChVrf.Aux_VgChV[IndVgChV].IdDir;
                initObj.ModChVrf.Aux_VPlnChV[i].CodCom = initObj.ModChVrf.VCcpl[initObj.Frm_ChVrf.Cbo_CptoPln.get_ItemData_((short)initObj.Frm_ChVrf.Cbo_CptoPln.ListIndex)].CodCom;
                initObj.ModChVrf.Aux_VPlnChV[i].CptCom = initObj.ModChVrf.VCcpl[initObj.Frm_ChVrf.Cbo_CptoPln.get_ItemData_((short)initObj.Frm_ChVrf.Cbo_CptoPln.ListIndex)].CptCom;
                initObj.ModChVrf.Aux_VPlnChV[i].DesCom = initObj.ModChVrf.VCcpl[initObj.Frm_ChVrf.Cbo_CptoPln.get_ItemData_((short)initObj.Frm_ChVrf.Cbo_CptoPln.ListIndex)].DesCom;
                initObj.ModChVrf.Aux_VPlnChV[i].CodPais = (short)initObj.Frm_ChVrf.CbPais.get_ItemData_((short)initObj.Frm_ChVrf.CbPais.ListIndex);
                initObj.ModChVrf.Aux_VPlnChV[i].estado = 3;
                initObj.ModChVrf.Aux_VPlnChV[i].Cod_IE = initObj.ModChVrf.Aux_VgChV[IndVgChV].Cod_IE;
                initObj.ModChVrf.Aux_VgChV[IndVgChV].Saldo -= initObj.ModChVrf.Aux_VPlnChV[i].Monto;

                s = "";
                s = s + VB6Helpers.Trim(initObj.ModChVrf.Aux_VPlnChV[i].CodCom + "-" + initObj.ModChVrf.Aux_VPlnChV[i].CptCom) + VB6Helpers.Chr(9);
                s = s + VB6Helpers.Trim(initObj.ModChVrf.Aux_VPlnChV[i].NemMon) + VB6Helpers.Chr(9);
                s = s + MODGPYF0.forma(initObj.ModChVrf.Aux_VPlnChV[i].Monto, FtoSal) + VB6Helpers.Chr(9);
                s += VB6Helpers.LTrim(VB6Helpers.Mid(MODGPYF1.Minuscula(initObj.ModChVrf.Aux_VPlnChV[i].DesCom), 1, 55));

                initObj.Frm_ChVrf.ListaPlanillas.Items.Add(new UI_ListBoxItem
                {
                    Value = s,
                    Data = i,
                    ID = i.ToString()
                });

                Saldo = initObj.ModChVrf.Aux_VgChV[IndVgChV].Saldo;
                initObj.Frm_ChVrf.Lb_Saldo.Text = Format.FormatCurrency((Saldo), FtoSal);
            }
            else
            {
                j = (short)initObj.Frm_ChVrf.ListaPlanillas.get_ItemData((short)initObj.Frm_ChVrf.ListaPlanillas.ListIndex);
                initObj.ModChVrf.Aux_VPlnChV[j].Monto = Format.StringToDouble((initObj.Frm_ChVrf.Tx_Monto.Text));
                initObj.ModChVrf.Aux_VPlnChV[j].CodCom = initObj.ModChVrf.VCcpl[initObj.Frm_ChVrf.Cbo_CptoPln.get_ItemData_((short)initObj.Frm_ChVrf.Cbo_CptoPln.ListIndex)].CodCom;
                initObj.ModChVrf.Aux_VPlnChV[j].CptCom = initObj.ModChVrf.VCcpl[initObj.Frm_ChVrf.Cbo_CptoPln.get_ItemData_((short)initObj.Frm_ChVrf.Cbo_CptoPln.ListIndex)].CptCom;
                initObj.ModChVrf.Aux_VPlnChV[j].DesCom = initObj.ModChVrf.VCcpl[initObj.Frm_ChVrf.Cbo_CptoPln.get_ItemData_((short)initObj.Frm_ChVrf.Cbo_CptoPln.ListIndex)].DesCom;
                initObj.ModChVrf.Aux_VPlnChV[j].CodPais = (short)initObj.Frm_ChVrf.CbPais.get_ItemData_((short)initObj.Frm_ChVrf.CbPais.ListIndex);
                initObj.ModChVrf.Aux_VPlnChV[j].Cod_IE = initObj.ModChVrf.Aux_VgChV[IndVgChV].Cod_IE;
                initObj.ModChVrf.Aux_VgChV[IndVgChV].Saldo -= initObj.ModChVrf.Aux_VPlnChV[j].Monto;
                Saldo = initObj.ModChVrf.Aux_VgChV[IndVgChV].Saldo;
                initObj.Frm_ChVrf.Lb_Saldo.Text = Format.FormatCurrency((Saldo), FtoSal);
                initObj.Frm_ChVrf.ListaPlanillas.Clear();

                for (i = 0; i <= (short)a; i++)
                {
                    if (initObj.ModChVrf.Aux_VPlnChV[i].estado != 9)
                    {
                        s = "";
                        s = s + VB6Helpers.Trim(initObj.ModChVrf.Aux_VPlnChV[i].CodCom + "-" + initObj.ModChVrf.Aux_VPlnChV[i].CptCom) + VB6Helpers.Chr(9);
                        s = s + VB6Helpers.Trim(initObj.ModChVrf.Aux_VPlnChV[i].NemMon) + VB6Helpers.Chr(9);
                        s = s + MODGPYF0.forma(initObj.ModChVrf.Aux_VPlnChV[i].Monto, FtoSal) + VB6Helpers.Chr(9);
                        s += VB6Helpers.Trim(VB6Helpers.Mid(MODGPYF1.Minuscula(initObj.ModChVrf.Aux_VPlnChV[i].DesCom), 1, 55));
                        initObj.Frm_ChVrf.ListaPlanillas.Items.Add(new UI_ListBoxItem
                        {
                            Value = s,
                            Data = i,
                            ID = i.ToString()
                        });
                    }
                }
                ModificaPln = (short)(false ? -1 : 0);
            }

            initObj.Frm_ChVrf.Cbo_CptoPln.ListIndex = -1;
            initObj.Frm_ChVrf.CbPais.ListIndex = -1;
            initObj.Frm_ChVrf.Tx_Monto.Text = Format.FormatCurrency(0, FtoSal);
            if (Format.StringToDouble((initObj.Frm_ChVrf.Lb_Saldo.Text)) == 0)
            {
                //Bt_Aceptar_Click(initObj);
            }
        }
        public static void Cbo_Moneda_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short a = 0;
            short i = 0;
            short CodMon = 0;
            short Ind_MoneSin = 0;
            double Saldo = 0;

            a = (short)VB6Helpers.UBound(initObj.ModChVrf.Aux_VgChV);

            if (EjecProcMon != 0)
            {
                if (initObj.Frm_ChVrf.Cbo_Moneda.ListIndex != -1)
                {
                    CodMon = (short)initObj.Frm_ChVrf.Cbo_Moneda.get_ItemData_((short)initObj.Frm_ChVrf.Cbo_Moneda.ListIndex);
                    Ind_MoneSin = MODGTAB0.Get_VMnd(initObj.MODGTAB0, uow, CodMon);
                    if (initObj.MODGTAB0.VMnd[Ind_MoneSin].Mnd_MndSin != 0)
                    {
                        initObj.Frm_ChVrf.Lb_Saldo.Tag = "_____________";
                        initObj.Frm_ChVrf.Tx_Monto.Tag = "_____________";
                        FtoSal = T_MODGCON0.FormatoSinDec;
                    }
                    else
                    {
                        initObj.Frm_ChVrf.Lb_Saldo.Tag = "_____________.__";
                        initObj.Frm_ChVrf.Tx_Monto.Tag = "_____________.__";
                        FtoSal = T_MODGCON0.FormatoConDec;
                    }
                }

                for (i = 0; i <= (short)a; i++)
                {
                    if (initObj.ModChVrf.Aux_VgChV[i].CodMon == CodMon)
                    {
                        Saldo = initObj.ModChVrf.Aux_VgChV[i].Saldo;
                        initObj.Frm_ChVrf.Lb_Saldo.Text = Format.FormatCurrency((Saldo), FtoSal);
                        initObj.Frm_ChVrf.Tx_Monto.Text = Format.FormatCurrency((Saldo), FtoSal);
                        IndVgChV = i;
                        break;
                    }
                }
                initObj.Frm_ChVrf.ListaPlanillas.ListIndex = -1;
                ModificaPln = (short)(false ? -1 : 0);
            }
        }
        public static void Form_Load(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            initObj.Frm_ChVrf = new UI_Frm_ChVrf();
            short[] lostab = null;
            short a = 0;
            short x = 0;
            short i = 0;
            short c = 0;
            short b = 0;
            short indmon = 0;

            a = (short)VB6Helpers.UBound(initObj.ModChVrf.VgChV);
            b = 0;
            b = (short)VB6Helpers.UBound(initObj.ModChVrf.VPlnChV);

            lostab = new short[4];
            lostab[0] = 60;
            lostab[1] = 90;
            lostab[2] = 170;

            x = MODGPYF0.seteatabulador(initObj.Frm_ChVrf.ListaPlanillas, lostab);
            VB6Helpers.RedimPreserve(ref initObj.ModChVrf.Aux_VgChV, 0, a);

            for (i = 0; i <= (short)a; i++)
            {
                if (initObj.ModChVrf.VgChV[i].Saldo > 0)
                {
                    initObj.ModChVrf.Aux_VgChV[i] = initObj.ModChVrf.VgChV[i];
                }
            }

            initObj.ModChVrf.Aux_VPlnChV = new T_PlnChV[0];
            VB6Helpers.RedimPreserve(ref initObj.ModChVrf.Aux_VPlnChV, 0, b);

            for (i = 0; i <= (short)b; i++)
            {
                initObj.ModChVrf.Aux_VPlnChV[i] = initObj.ModChVrf.VPlnChV[i];
            }

            if (b >= 0)
            {
                CargaPln(initObj);
            }

            ModificaPln = (short)(false ? -1 : 0);

            c = ModChVrf.SyGet_ccpl(initObj.ModChVrf, uow);
            CargaEnListaTcp(initObj, initObj.Frm_ChVrf.Cbo_CptoPln, initObj.ModChVrf.CodigoIE);
            MODGTAB0.CargaEnLista_Pai(initObj.MODGTAB0, initObj.Frm_ChVrf.CbPais, uow);

            for (i = 0; i <= (short)a; i++)
            {
                indmon = MODGTAB0.Get_VMnd(initObj.MODGTAB0, uow, initObj.ModChVrf.Aux_VgChV[i].CodMon);
                initObj.Frm_ChVrf.Cbo_Moneda.Items.Add(new UI_ComboItem()
                {
                    Data = initObj.MODGTAB0.VMnd[indmon].Mnd_MndCod,
                    ID = initObj.MODGTAB0.VMnd[indmon].Mnd_MndCod.ToString(),
                    Value = initObj.MODGTAB0.VMnd[indmon].Mnd_MndNom
                });
            }
            EjecProcMon = (short)(true ? -1 : 0);
        }
        public static void ListaPlanillas_Click(InitializationObject initObj)
        {
            short a = 0;
            short j = 0;
            short i = 0;
            double Saldo = 0;
            short ind = 0;

            a = (short)VB6Helpers.UBound(initObj.ModChVrf.VCcpl);

            if (initObj.Frm_ChVrf.ListaPlanillas.ListIndex != -1)
            {
                j = (short)initObj.Frm_ChVrf.ListaPlanillas.get_ItemData((short)initObj.Frm_ChVrf.ListaPlanillas.ListIndex);

                for (i = 0; i <= (short)a; i++)
                {
                    if ((initObj.ModChVrf.Aux_VPlnChV[j].CodCom == initObj.ModChVrf.VCcpl[i].CodCom) && (initObj.ModChVrf.Aux_VPlnChV[j].CptCom == initObj.ModChVrf.VCcpl[i].CptCom))
                    {
                        ind = i;
                        break;
                    }

                }

                for (i = 0; i <= (short)(initObj.Frm_ChVrf.Cbo_CptoPln.ListCount - 1); i++)
                {
                    if (initObj.Frm_ChVrf.Cbo_CptoPln.get_ItemData_(i) == ind)
                    {
                        initObj.Frm_ChVrf.Cbo_CptoPln.ListIndex = i;
                        break;
                    }

                }

                EjecProcMon = (short)(false ? -1 : 0);
                for (i = 0; i <= (short)(initObj.Frm_ChVrf.Cbo_Moneda.ListCount - 1); i++)
                {
                    if (initObj.Frm_ChVrf.Cbo_Moneda.get_ItemData_(i) == initObj.ModChVrf.Aux_VPlnChV[j].CodMon)
                    {
                        initObj.Frm_ChVrf.Cbo_Moneda.ListIndex = i;
                        break;
                    }

                }

                EjecProcMon = (short)(true ? -1 : 0);

                for (i = 0; i <= (short)(initObj.Frm_ChVrf.CbPais.ListCount - 1); i++)
                {
                    if (initObj.Frm_ChVrf.CbPais.get_ItemData_(i) == initObj.ModChVrf.Aux_VPlnChV[j].CodPais)
                    {
                        initObj.Frm_ChVrf.CbPais.ListIndex = i;
                        break;
                    }
                }

                Saldo = initObj.ModChVrf.Aux_VgChV[IndVgChV].Saldo + initObj.ModChVrf.Aux_VPlnChV[j].Monto;
                initObj.Frm_ChVrf.Lb_Saldo.Text = Format.FormatCurrency((Saldo), FtoSal);
                initObj.Frm_ChVrf.Tx_Monto.Text = Format.FormatCurrency((initObj.ModChVrf.Aux_VPlnChV[j].Monto), FtoSal);
                ModificaPln = (short)(true ? -1 : 0);
            }

        }
        public static void Tx_Monto_LostFocus(InitializationObject initObj)
        {
            initObj.Frm_ChVrf.Tx_Monto.Text = Format.FormatCurrency(Format.StringToDouble(initObj.Frm_ChVrf.Tx_Monto.Text), FtoSal);// VB6Helpers.Trim(VB6Helpers.Format(initObj.Frm_ChVrf.Tx_Monto.Text, FtoSal));
        }
        private static void CargaPln(InitializationObject initObj)
        {
            string s = "";
            short i = 0;
            for (i = 0; i <= (short)VB6Helpers.UBound(initObj.ModChVrf.Aux_VPlnChV); i++)
            {
                s = "";
                s = s + VB6Helpers.Trim(initObj.ModChVrf.Aux_VPlnChV[i].CodCom + "-" + initObj.ModChVrf.Aux_VPlnChV[i].CptCom) + VB6Helpers.Chr(9);
                s = s + VB6Helpers.Trim(initObj.ModChVrf.Aux_VPlnChV[i].NemMon) + VB6Helpers.Chr(9);
                s = s + MODGPYF0.forma(initObj.ModChVrf.Aux_VPlnChV[i].Monto, FtoSal) + VB6Helpers.Chr(9);
                s += VB6Helpers.Trim(VB6Helpers.Mid(MODGPYF1.Minuscula(initObj.ModChVrf.Aux_VPlnChV[i].DesCom), 1, 55));
                initObj.Frm_ChVrf.ListaPlanillas.Items.Add(new UI_ListBoxItem
                {
                    Data = i,
                    ID = i.ToString(),
                    Value = s
                });
            }
        }
    }
}
