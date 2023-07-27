using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public static class Frm_Pln_VisExp
    {
        private const string Tx_Modificable = "00;08;11;13;24;25";
        private const string Tx_Numerico = "09;10;11;15";
        private const string Txt_Fecha = "";
        private const string Tx_String = "39";


        public static void Form_Activate(InitializationObject initObject, UnitOfWorkCext01 uow)
        {
            if (initObject.MODXPLN1.VxPlvs.Length > 0)
            {
                GetFrm_VxPlv((short)VB6Helpers.Val(initObject.Frm_Pln_VisExp.Boton[0].Tag), initObject, uow);
            }
        }

        public static void Form_Load(InitializationObject initObject, UnitOfWorkCext01 uow)
        {
            short i = 0;
            short a = 0;
            UI_Frm_Pln_VisExp Frm_Pln_VisExp = initObject.Frm_Pln_VisExp;
            //Habilita campos modificables.
            for (i = 0; i <= 25; i++)
            {
                if (VB6Helpers.Instr(Tx_Modificable, VB6Helpers.Format(VB6Helpers.CStr(i), "00")) != 0)
                    HabilitaDeshabilitaPicture(initObject, i, true);
                else
                    HabilitaDeshabilitaPicture(initObject, i, false);
            }
            HabilitaDeshabilitaPicture(initObject, 8, false);

            //Inicializa.
            for (i = 0; i <= (short)VB6Helpers.UBound(initObject.MODXPLN1.VxPlvs); i++)
            {
                //a = (short)VB6Helpers.DoEvents();
                initObject.MODXPLN1.VxPlvs[i].Acepto = (short)(false ? -1 : 0);
            }

            //Lee tabla de Bancos.-
            a = MODGTAB0.SyGetn_Bco(initObject, uow);

            if (VB6Helpers.UBound(initObject.MODXPLN1.VxPlvs) == 0) //si hay un solo elemento elimino la navegacion
            {
                initObject.Frm_Pln_VisExp.Boton[0].Enabled = false;
                initObject.Frm_Pln_VisExp.Boton[1].Enabled = false;
            }

            //Busca la Primera Planilla NO eliminada.-
            if (initObject.MODXPLN1.IndPlv == 0)
                initObject.Frm_Pln_VisExp.Boton[0].Tag = Fn_GetProximo(initObject, 0, 1);
            else
                initObject.Frm_Pln_VisExp.Boton[0].Tag = initObject.MODXPLN1.IndPlv;


            initObject.Frm_Pln_VisExp.Tx_Plnv[43].Text = "15";


        }


        public static void HabilitaDeshabilitaPicture(InitializationObject init, int indexPicture, bool modificable)
        {

            if (indexPicture == 0)
            {
                init.Frm_Pln_VisExp.Tx_Plnv[0].Enabled = false;
                init.Frm_Pln_VisExp.Tx_Fecha.Enabled = modificable;
            }
            else if (indexPicture == 1)
            {
                init.Frm_Pln_VisExp.Tx_Plnv[40].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[41].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[2].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[3].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[43].Enabled = modificable;
                //init.Frm_Pln_VisExp.Lb_Plnv[52].Enabled = modificable;
            }
            else if (indexPicture == 5)
            {
                init.Frm_Pln_VisExp.Tx_Plnv[4].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[5].Enabled = modificable;
            }
            else if (indexPicture == 7)
            {
                init.Frm_Pln_VisExp.Tx_Plnv[6].Enabled = modificable;
            }

            else if (indexPicture == 8)
            {
                init.Frm_Pln_VisExp.Tx_Plnv[7].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[8].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[9].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[10].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[11].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[12].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[13].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[14].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[15].Enabled = modificable;
            }
            else if (indexPicture == 14)
            {
                init.Frm_Pln_VisExp.Tx_Plnv[16].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[17].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[18].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[19].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[20].Enabled = modificable;
            }
            else if (indexPicture == 16)
            {
                init.Frm_Pln_VisExp.Tx_Plnv[21].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[22].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[23].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[24].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[25].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[26].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[27].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[28].Enabled = modificable;

            }
            else if (indexPicture == 20)
            {
                init.Frm_Pln_VisExp.Tx_Plnv[29].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[30].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[31].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[32].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[33].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[34].Enabled = modificable;

            }
            else if (indexPicture == 22)
            {
                init.Frm_Pln_VisExp.Tx_Plnv[35].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[36].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[37].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[38].Enabled = modificable;
                init.Frm_Pln_VisExp.Tx_Plnv[42].Enabled = modificable;
            }

            else if (indexPicture == 24)
            {
                init.Frm_Pln_VisExp.Tx_Plnv[39].Enabled = modificable;
            }
            else if (indexPicture == 25)
            {
                init.Frm_Pln_VisExp.Boton[0].Enabled = modificable;
                init.Frm_Pln_VisExp.Boton[1].Enabled = modificable;
                init.Frm_Pln_VisExp.Boton[2].Enabled = modificable;
                init.Frm_Pln_VisExp.Boton[3].Enabled = modificable;
                init.Frm_Pln_VisExp.Boton[4].Enabled = modificable;
            }

        }

        public static void BotonRetroceder_Click(InitializationObject initObject, UnitOfWorkCext01 uow)
        {
            short a = 0;
            short i = 0;

            a = nueva_vis(initObject);
            a = (short)VB6Helpers.Val(initObject.Frm_Pln_VisExp.Boton[0].Tag);
            i = Fn_GetProximo(initObject, a, -1);
            if (a == i)
            {
                initObject.Frm_Pln_VisExp.Boton[0].Enabled = false;
            }
            else
            {
                initObject.Frm_Pln_VisExp.Boton[1].Enabled = true;
            }

            initObject.Frm_Pln_VisExp.Boton[0].Tag = i;
            GetFrm_VxPlv(i, initObject, uow);
            if (Fn_GetProximo(initObject, i, -1) == i)
            {
                initObject.Frm_Pln_VisExp.Boton[0].Enabled = false;
            }

            //if (initObject.Frm_Pln_VisExp.Boton[0].Enabled == false && initObject.Frm_Pln_VisExp.Boton[1].Enabled)
            //{
            //    initObject.Frm_Pln_VisExp.Boton[1].Focus();
            //}
        }

        public static void BotonAvanzar_Click(InitializationObject initObject, UnitOfWorkCext01 uow)
        {
            short a = 0;
            short i = 0;

            a = nueva_vis(initObject);
            a = (short)VB6Helpers.Val(initObject.Frm_Pln_VisExp.Boton[0].Tag);
            i = Fn_GetProximo(initObject, a, 1);
            if (a == i)
            {
                initObject.Frm_Pln_VisExp.Boton[1].Enabled = false;
            }
            else
            {
                initObject.Frm_Pln_VisExp.Boton[0].Enabled = true;
            }

            initObject.Frm_Pln_VisExp.Boton[0].Tag = i;
            GetFrm_VxPlv(i, initObject, uow);
            if (Fn_GetProximo(initObject, i, 1) == i)
            {
                initObject.Frm_Pln_VisExp.Boton[1].Enabled = false;
            }

            //if (initObject.Frm_Pln_VisExp.Boton[1].Enabled == false && initObject.Frm_Pln_VisExp.Boton[0].Enabled)
            //{
            //    initObject.Frm_Pln_VisExp.Boton[0].SetFocus();
            //}
        }

        public static void BotonModificar_Click(InitializationObject initObject, UnitOfWorkCext01 uow)
        {
            short a = 0;
            short i = 0;
            double MVB = 0;
            double MVC = 0;
            double MOG = 0;
            string Pl = "";
            short Fecha_Paso = 0;
            double MTC = 0;
            double MVL = 0;
            double dif = 0;

            i = (short)VB6Helpers.Val(initObject.Frm_Pln_VisExp.Boton[0].Tag);
            MVB = Format.StringToDouble((initObject.Frm_Pln_VisExp.Tx_Plnv[9].Text));  //Valor Bruto.
            if (!initObject.Frm_Pln_VisExp.L_PaI[0].Visible)
            {
                MVC = Format.StringToDouble((initObject.Frm_Pln_VisExp.Tx_Plnv[10].Text));  //Valor Comisión.
            }
            else
            {
                MVC = 0;
            }

            if (!initObject.Frm_Pln_VisExp.L_PaI[1].Visible)
            {
                MOG = Format.StringToDouble((initObject.Frm_Pln_VisExp.Tx_Plnv[11].Text));  //Otros Gastos.
            }
            else
            {
                MOG = 0;
            }

            MTC = Format.StringToDouble((initObject.Frm_Pln_VisExp.Tx_Plnv[15].Text));  //Tipo de Cambio.
            MVL = Format.StringToDouble((initObject.Frm_Pln_VisExp.Tx_Plnv[12].Text));  //Valor Líquido.
            dif = (MVB - (MVC + MOG)) - MVL;
            //Valida que el Líquido concuerde.
            if (dif != 0)
            {
                //VB6Helpers.MsgBox("Los montos modificados no concuerdan con el Valor Líquido. Corrija los valores y reintente.", MsgBoxStyle.Information, BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos.T_MODXPLN1.MsgxPlv);
                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Los montos modificados no concuerdan con el Valor Líquido. Corrija los valores y reintente.",
                    Title = T_MODXPLN1.MsgxPlv,
                    Type = TipoMensaje.Informacion
                });
                return;
            }

            //Valida que el Tipo de Cambio tenga valor.
            Pl = initObject.Frm_Pln_VisExp.Tx_Plnv[3].Text;
            initObject.Frm_Pln_VisExp.Boton[0].Tag = i;
            //if (VB6Helpers.Instr(MODXPLN1.PLNLIQ, Pl) > 0)
            if (VB6Helpers.Instr(BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos.T_MODXPLN1.PLNLIQ, Pl) > 0)
            {
                if (~initObject.MODXPLN1.VxPlvs[i].PlnEst != 0)
                {
                    if (MTC == 0)
                    {
                        //VB6Helpers.Beep();
                        //VB6Helpers.MsgBox("El Tipo de Cambio debe tener un valor.", MsgBoxStyle.Information, BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos.T_MODXPLN1.MsgxPlv);
                        // Tx_Plnv[15].SetFocus();
                        initObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = " El Tipo de Cambio debe tener un valor.",
                            Title = T_MODXPLN1.MsgxPlv,
                            Type = TipoMensaje.Informacion
                        });
                        return;
                    }

                }

            }
            else
            {
                if (VB6Helpers.Val(initObject.Frm_Pln_VisExp.Tx_Plnv[15].Text) > 0)
                {
                    //VB6Helpers.Beep();
                    //VB6Helpers.MsgBox("Para este tipo de planilla el tipo de cambio no debe llevar valor", MsgBoxStyle.Information, BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos.T_MODXPLN1.MsgxPlv);
                    initObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = " Para este tipo de planilla el tipo de cambio no debe llevar valor.",
                        Title = T_MODXPLN1.MsgxPlv,
                        Type = TipoMensaje.Informacion
                    });
                }

                initObject.Frm_Pln_VisExp.Tx_Plnv[15].Text = "";
            }

            initObject.MODXPLN1.VxPlvs[i].MtoBru = MVB;
            initObject.MODXPLN1.VxPlvs[i].MtoCom = MVC;
            initObject.MODXPLN1.VxPlvs[i].MtoOtg = MOG;
            initObject.MODXPLN1.VxPlvs[i].TipCam = MTC;
            initObject.MODXPLN1.VxPlvs[i].ObsPln = initObject.Frm_Pln_VisExp.Tx_Plnv[39].Text;

            if (string.IsNullOrEmpty(initObject.Frm_Pln_VisExp.Tx_Fecha.Text))
            {
                //VB6Helpers.MsgBox("Debe ingresar un valor para la fecha de presentación", MsgBoxStyle.Information, Comex.Common.XCFT.T_Modulos.T_MODXPLN1.MsgxPlv);
                // initObject.Frm_Pln_VisExp.Tx_Fecha.SetFocus();
                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Debe ingresar un valor para la fecha de presentación.",
                    Title = T_MODXPLN1.MsgxPlv,
                    Type = TipoMensaje.Informacion
                });
                return;
            }

            //La fecha no puede ser sábado ni domingo
            Fecha_Paso = VB6Helpers.Weekday(VB6Helpers.CDate(initObject.Frm_Pln_VisExp.Tx_Fecha.Text));
            if (Fecha_Paso == 1 || Fecha_Paso == 7)
            {
                //Sólo si es fin de semana
                //VB6Helpers.MsgBox("Atención: La Fecha no puede ser fin de semana.", MsgBoxStyle.Information, Comex.Common.XCFT.T_Modulos.T_MODXPLN1.MsgxPlv);
                //Tx_Fecha.SetFocus();
                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Atención: La Fecha no puede ser fin de semana.",
                    Title = T_MODXPLN1.MsgxPlv,
                    Type = TipoMensaje.Informacion
                });
                return;
            }

            //La fecha no puede ser un feriado de este año
            a = MODGTAB0.Fn_Buscar_Fecha_Fer(initObject.MODGTAB0, uow, VB6Helpers.Trim(initObject.Frm_Pln_VisExp.Tx_Fecha.Text));
            if (a == 0)
            {
                //VB6Helpers.MsgBox("Atención: La Fecha no corresponde, porque existe como fecha feriada de este año.", MsgBoxStyle.Information, Comex.Common.XCFT.T_Modulos.T_MODXPLN1.MsgxPlv);
                //Tx_Fecha.SetFocus();
                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Atención: La Fecha no corresponde, porque existe como fecha feriada de este año.",
                    Title = T_MODXPLN1.MsgxPlv,
                    Type = TipoMensaje.Informacion
                });
                return;
            }

            initObject.MODXPLN1.VxPlvs[i].fecpre = VB6Helpers.Format(initObject.Frm_Pln_VisExp.Tx_Fecha.Text, "dd/MM/yyyy");
            initObject.MODXPLN1.VxPlvs[i].Fecing = VB6Helpers.Format(initObject.Frm_Pln_VisExp.Tx_Fecha.Text, "dd/MM/yyyy");
            initObject.MODXPLN1.VxPlvs[i].Status = T_MODGCVD.EstadoMod;

        }

        public static void BotonAceptar_Click(InitializationObject initObject, UnitOfWorkCext01 uow)
        {
            short i = 0;
            i = (short)VB6Helpers.Val(initObject.Frm_Pln_VisExp.Boton[0].Tag);
            if (initObject.MODXPLN1.VxPlvs.ElementAtOrDefault(i) != null)
            {
                initObject.MODXPLN1.VxPlvs[i].Acepto = (short)(true ? -1 : 0);
            }
        }

        public static void BotonCancelar_Click(InitializationObject initObject, UnitOfWorkCext01 uow)
        {
            return;
            // VB6Helpers.Unload(this);
        }




        //Carga en Formulario la Planilla de Indice Ixplv.
        private static void GetFrm_VxPlv(short IxPlv, InitializationObject initObject, UnitOfWorkCext01 uow)
        {
            string n = "";
            short m = 0;
            string R = "";
            short i = IxPlv;
            short j = 0;
            short PLNTRN = 0;
            short k = 0;

            initObject.Frm_Pln_VisExp.L_PaI[0].Visible = false;
            initObject.Frm_Pln_VisExp.L_PaD[0].Visible = false;
            initObject.Frm_Pln_VisExp.L_PaI[1].Visible = false;
            initObject.Frm_Pln_VisExp.L_PaD[1].Visible = false;


            //Número Presentación.
            if (!string.IsNullOrEmpty(initObject.MODXPLN1.VxPlvs[i].NumPre))
            {
                //n = VB6Helpers.Format(MODXPLN1.VxPlvs[i].NumPre, "0000000");
                n = VB6Helpers.Format(initObject.MODXPLN1.VxPlvs[i].NumPre, "0000000");

                initObject.Frm_Pln_VisExp.Tx_Plnv[0].Text = VB6Helpers.Left(n, 6) + "-" + VB6Helpers.Right(n, 1);
            }

            //Fecha Presentación.
            initObject.Frm_Pln_VisExp.Tx_Fecha.Text = VB6Helpers.Format(initObject.MODXPLN1.VxPlvs[i].fecpre, "dd/MM/yyyy");

            if (initObject.MODXPLN1.VxPlvs[i].PlnEst != 0)
            {
                initObject.Frm_Pln_VisExp.Tx_Fecha.Enabled = true;
            }
            else
            {
                initObject.Frm_Pln_VisExp.Tx_Fecha.Enabled = false;
            }

            //Codigo Plaza Banco Central que Contabiliza.
            
            initObject.Frm_Pln_VisExp.Tx_Plnv[2].Text = VB6Helpers.Format(VB6Helpers.CStr(initObject.MODXPLN1.VxPlvs[i].PlzBcc), "00");

            //Código Tipo de Operación.
            initObject.Frm_Pln_VisExp.Tx_Plnv[3].Text = VB6Helpers.Format(VB6Helpers.CStr(initObject.MODXPLN1.VxPlvs[i].TipPln), "000");

            //Plaza Banco Central que Contabiliza

            m = MODGTAB1.Get_VPbc(initObject, uow, initObject.MODXPLN1.VxPlvs[i].PlzBcc);
            initObject.Frm_Pln_VisExp.Tx_Plnv[40].Text = string.Empty;
            if (m >= 0)
                initObject.Frm_Pln_VisExp.Tx_Plnv[40].Text = initObject.MODGTAB1.VPbc[m].Pbc_PbcDes;


            //Tipo de Operación.
            initObject.Frm_Pln_VisExp.Tx_Plnv[41].Text = MODXPLN1.GetNomPLn(initObject.MODXPLN1.VxPlvs[i].TipPln);

            //Nombre.
            initObject.Frm_Pln_VisExp.Tx_Plnv[4].Text = Mdl_Funciones_Varias.GetDatPrtn(initObject, uow, initObject.MODXPLN1.VxPlvs[i].PrtExp, initObject.MODXPLN1.VxPlvs[i].IndNom, initObject.MODXPLN1.VxPlvs[i].IndDir, "N", "DC");

            //Dirección.
            initObject.Frm_Pln_VisExp.Tx_Plnv[5].Text = Mdl_Funciones_Varias.GetDatPrtn(initObject, uow, initObject.MODXPLN1.VxPlvs[i].PrtExp, initObject.MODXPLN1.VxPlvs[i].IndNom, initObject.MODXPLN1.VxPlvs[i].IndDir, "D", "DC");

            //Rut.
            if(!string.IsNullOrWhiteSpace(initObject.MODXPLN1.VxPlvs[i].RutExp))
            {
                R = MODXPLN1.ConvRut(initObject.MODXPLN1.VxPlvs[i].RutExp);
                initObject.Frm_Pln_VisExp.Tx_Plnv[6].Text = VB6Helpers.Mid(R, 1, VB6Helpers.Len(R) - 1) + "-" + VB6Helpers.Mid(R, VB6Helpers.Len(R), 1);
            }

            if (VB6Helpers.Instr(BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos.T_MODXPLN1.PLN400, VB6Helpers.CStr(initObject.MODXPLN1.VxPlvs[i].TipPln)) == 0)
            {

                for (j = 7; j <= 14; j++)
                {
                    initObject.Frm_Pln_VisExp.Tx_Plnv[j].Enabled = true;
                }

                //Moneda.
                m = MODGTAB0.Get_VMnd(initObject.MODGTAB0, uow, initObject.MODXPLN1.VxPlvs[i].CodMnd);
                if (m != 0)
                {
                    initObject.Frm_Pln_VisExp.Tx_Plnv[7].Text = initObject.MODGTAB0.VMnd[m].Mnd_MndNom;
                }
                else
                {
                    initObject.Frm_Pln_VisExp.Tx_Plnv[7].Text = "";
                }

                //Código Moneda.
                if (initObject.MODGTAB0.VMnd[m].Mnd_MndCbc > 0)
                {
                    initObject.Frm_Pln_VisExp.Tx_Plnv[8].Text = VB6Helpers.Format(VB6Helpers.CStr(initObject.MODGTAB0.VMnd[m].Mnd_MndCbc), "000");
                }

                //Valor Bruto.
                if (initObject.MODXPLN1.VxPlvs[i].MtoBru > 0)
                {
                    initObject.Frm_Pln_VisExp.Tx_Plnv[9].Text = MODGPYF0.forma(initObject.MODXPLN1.VxPlvs[i].MtoBru, "#,###,###,###,##0.00");
                }

                //Comisiones.
                if (~initObject.MODXPLN1.VxPlvs[i].DedCom != 0)
                {
                    if (initObject.MODXPLN1.VxPlvs[i].MtoCom > 0)
                    {
                        initObject.Frm_Pln_VisExp.Tx_Plnv[10].Text = MODGPYF0.forma(initObject.MODXPLN1.VxPlvs[i].MtoCom, "#,###,###,###,##0.00");
                    }

                }
                else
                {
                    if (initObject.MODXPLN1.VxPlvs[i].MtoCom > 0)
                    {
                        initObject.Frm_Pln_VisExp.Tx_Plnv[10].Text = "( " + MODGPYF0.forma(initObject.MODXPLN1.VxPlvs[i].MtoCom, "#,###,###,###,##0.00") + " )";
                    }

                }

                //Otros Gastos Deducibles.
                //If Not VxPlvs(i%).DedFle Then
                if (initObject.MODXPLN1.VxPlvs[i].MtoOtg > 0)
                {
                    initObject.Frm_Pln_VisExp.Tx_Plnv[11].Text = MODGPYF0.forma(initObject.MODXPLN1.VxPlvs[i].MtoOtg, "#,###,###,###,##0.00");
                }

                //End If

                //Valor Líquido.
                if (initObject.MODXPLN1.VxPlvs[i].MtoLiq > 0)
                {
                    initObject.Frm_Pln_VisExp.Tx_Plnv[12].Text = MODGPYF0.forma(initObject.MODXPLN1.VxPlvs[i].MtoLiq, "#,###,###,###,##0.00");
                }

                //Paridad a US$.
                if (initObject.MODXPLN1.VxPlvs[i].Mtopar > 0)
                {
                    initObject.Frm_Pln_VisExp.Tx_Plnv[13].Text = MODGPYF0.forma(initObject.MODXPLN1.VxPlvs[i].Mtopar, "#,###,##0.0000");
                }

                //Monto en US$.
                if (initObject.MODXPLN1.VxPlvs[i].MtoDol > 0)
                {
                    initObject.Frm_Pln_VisExp.Tx_Plnv[14].Text = MODGPYF0.forma(initObject.MODXPLN1.VxPlvs[i].MtoDol, "#,###,###,###,##0.00");
                }

            }
            else
            {
                for (j = 7; j <= 14; j++)
                {
                    initObject.Frm_Pln_VisExp.Tx_Plnv[j].Text = "";
                    initObject.Frm_Pln_VisExp.Tx_Plnv[j].Enabled = false;
                }

            }

            //************

            //Tipo de Cambio de la Operación.
            if (VB6Helpers.Instr(VB6Helpers.CStr(PLNTRN), VB6Helpers.CStr(initObject.MODXPLN1.VxPlvs[i].TipPln)) == 0 && VB6Helpers.Instr(BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos.T_MODXPLN1.PLNINF, VB6Helpers.CStr(initObject.MODXPLN1.VxPlvs[i].TipPln)) == 0 && initObject.MODXPLN1.VxPlvs[i].TipPln != 402)
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[15].Text = MODGPYF0.forma(initObject.MODXPLN1.VxPlvs[i].TipCam, "#,###,##0.0000");
            }
            else
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[15].Text = "";
            }

            //Aduana.
            m = MODGTAB1.Get_VAdn(initObject.MODGTAB1, uow, initObject.MODXPLN1.VxPlvs[i].CodAdn);
            initObject.Frm_Pln_VisExp.Tx_Plnv[16].Text = string.Empty;
            if (m >= 0)
                initObject.Frm_Pln_VisExp.Tx_Plnv[16].Text = initObject.MODGTAB1.VAdn[m].NomAdn;


            //Código Aduana.
            if (initObject.MODXPLN1.VxPlvs[i].CodAdn != 0)
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[17].Text = VB6Helpers.Format(VB6Helpers.CStr(initObject.MODXPLN1.VxPlvs[i].CodAdn), "00");
            }

            //Número Aceptación.
            if(!string.IsNullOrWhiteSpace(initObject.MODXPLN1.VxPlvs[i].numdec))
            {
                n = VB6Helpers.Format(initObject.MODXPLN1.VxPlvs[i].numdec, "0000000");
                initObject.Frm_Pln_VisExp.Tx_Plnv[18].Text = VB6Helpers.Left(n, 6) + "-" + VB6Helpers.Right(n, 1);
            }
            else
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[18].Text = "";
            }

            //Fecha Aceptación.
            if (!string.IsNullOrEmpty(initObject.MODXPLN1.VxPlvs[i].FecDec))
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[19].Text = VB6Helpers.Format(initObject.MODXPLN1.VxPlvs[i].FecDec, "dd/MM/yyyy");
            }
            else
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[19].Text = "";
            }

            //Fecha Vencimiento Retorno.
            if(!string.IsNullOrEmpty(initObject.MODXPLN1.VxPlvs[i].FecVen))
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[20].Text = VB6Helpers.Format(initObject.MODXPLN1.VxPlvs[i].FecVen, "dd/MM/yyyy");
            }
            else
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[20].Text = "";
            }

            //*****************
            //Código Entidad Autorizada.
            if (initObject.MODXPLN1.VxPlvs[i].DfoCea != 0)
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[22].Text = VB6Helpers.Format(VB6Helpers.CStr(initObject.MODXPLN1.VxPlvs[i].DfoCea), "00");

                //Entidad Autorizada.
                // m = MODGTAB0.Get_Bco(initObject.MODXPLN1.VxPlvs[i].DfoCea);
                m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_Bco(initObject, uow, initObject.MODXPLN1.VxPlvs[i].DfoCea);
                if (m >= 0)
                {
                    initObject.Frm_Pln_VisExp.Tx_Plnv[21].Text = VB6Helpers.Trim(initObject.MODGTAB0.VBco[m].NomBco);
                }
                else
                {
                    initObject.Frm_Pln_VisExp.Tx_Plnv[21].Text = "";
                }

            }
            else
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[22].Text = "";
                initObject.Frm_Pln_VisExp.Tx_Plnv[21].Text = "";
            }

            //Tipo Financiamiento.
            if (initObject.MODXPLN1.VxPlvs[i].DfoCtf != 0)
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[23].Text = MODXPLN1.GetNomPLn(initObject.MODXPLN1.VxPlvs[i].DfoCtf);
                //Código Tipo Financiamiento.
                initObject.Frm_Pln_VisExp.Tx_Plnv[24].Text = VB6Helpers.CStr(initObject.MODXPLN1.VxPlvs[i].DfoCtf);
            }
            else
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[23].Text = "";
                initObject.Frm_Pln_VisExp.Tx_Plnv[24].Text = "";
            }

            //Plaza Banco Central.
            if (initObject.MODXPLN1.VxPlvs[i].DfoCbc != 0)
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[26].Text = VB6Helpers.Format(VB6Helpers.CStr(initObject.MODXPLN1.VxPlvs[i].DfoCbc), "00");

                //Código Plaza Banco Central.
                m = MODGTAB1.Get_VPbc(initObject, uow, initObject.MODXPLN1.VxPlvs[i].DfoCbc);
                initObject.Frm_Pln_VisExp.Tx_Plnv[25].Text = string.Empty;
                if (m >= 0)
                    initObject.Frm_Pln_VisExp.Tx_Plnv[25].Text = VB6Helpers.Trim(initObject.MODGTAB1.VPbc[m].Pbc_PbcDes);
            }
            else
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[26].Text = "";
                initObject.Frm_Pln_VisExp.Tx_Plnv[25].Text = "";
            }

            //Número Presentación.
            if(!string.IsNullOrWhiteSpace(initObject.MODXPLN1.VxPlvs[i].DfoNpr))
            {
                if (VB6Helpers.Val(initObject.MODXPLN1.VxPlvs[i].DfoNpr) != 0)
                {
                    n = VB6Helpers.Format(initObject.MODXPLN1.VxPlvs[i].DfoNpr, "0000000");
                    //Tx_Plnv(27).Text = Left$(n$, 6) + "-" + Right$(n$, 1)
                    initObject.Frm_Pln_VisExp.Tx_Plnv[27].Text = initObject.MODXPLN1.VxPlvs[i].DfoNpr;
                }
                else
                {
                    initObject.Frm_Pln_VisExp.Tx_Plnv[27].Text = "";
                }

            }
            else
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[27].Text = "";
            }

            //Fecha Presentación.
            if(!string.IsNullOrWhiteSpace(initObject.MODXPLN1.VxPlvs[i].DfoFpr))
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[28].Text = VB6Helpers.Format(initObject.MODXPLN1.VxPlvs[i].DfoFpr, "dd/MM/yyyy");
            }
            else
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[28].Text = "";
            }

            //-----------------------------------------
            // Antecedentes financiamiento
            //-----------------------------------------

            if (initObject.MODXPLN1.VxPlvs[i].AfiMnd != 0)
            {
                m = MODGTAB0.Get_VMnd(initObject.MODGTAB0, uow, initObject.MODXPLN1.VxPlvs[i].AfiMnd);
                initObject.Frm_Pln_VisExp.Tx_Plnv[30].Text = VB6Helpers.Format(VB6Helpers.CStr(initObject.MODGTAB0.VMnd[m].Mnd_MndCbc), "000");
                initObject.Frm_Pln_VisExp.Tx_Plnv[29].Text = initObject.MODGTAB0.VMnd[m].Mnd_MndNom;
            }
            else
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[30].Text = "";
                initObject.Frm_Pln_VisExp.Tx_Plnv[29].Text = "";
            }

            if (initObject.MODXPLN1.VxPlvs[i].AfiPar != 0)
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[31].Text = Format.FormatCurrency((initObject.MODXPLN1.VxPlvs[i].AfiPar), "0.0000");
            }

            if (initObject.MODXPLN1.VxPlvs[i].AfiMto != 0)
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[32].Text = Format.FormatCurrency((initObject.MODXPLN1.VxPlvs[i].AfiMto), "0.00");
            }

            if (initObject.MODXPLN1.VxPlvs[i].AfiMtoD != 0)
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[33].Text = Format.FormatCurrency((initObject.MODXPLN1.VxPlvs[i].AfiMtoD), "0.00");
            }

            //Plazo Vencimiento Financiamiento.
            if (initObject.MODXPLN1.VxPlvs[i].AfiVen != 0)
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[34].Text = VB6Helpers.Trim(VB6Helpers.Str(initObject.MODXPLN1.VxPlvs[i].AfiVen));
            }
            else
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[34].Text = "";
            }

            //****************************************

            //Die, Código Plaza Banco Central.
            if (initObject.MODXPLN1.VxPlvs[i].DiePbc != 0)
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[35].Text = VB6Helpers.Format(VB6Helpers.CStr(initObject.MODXPLN1.VxPlvs[i].DiePbc), "00");
            }

            //Die, Plaza Banco Central.
            m = MODGTAB1.Get_VPbc(initObject, uow, initObject.MODXPLN1.VxPlvs[i].DiePbc);
            //initObject.Frm_Pln_VisExp.Tx_Plnv[36].Text = initObject.MODGTAB1.VPbc[m].Pbc_PbcDes.Value;
            initObject.Frm_Pln_VisExp.Tx_Plnv[36].Text = string.Empty;
            if (m >= 0)
                initObject.Frm_Pln_VisExp.Tx_Plnv[36].Text = initObject.MODGTAB1.VPbc[m].Pbc_PbcDes;


            //Die, Número de Emisión.
            if(!string.IsNullOrWhiteSpace(initObject.MODXPLN1.VxPlvs[i].DieNum))
            {
                n = VB6Helpers.Format(initObject.MODXPLN1.VxPlvs[i].DieNum, "0000000");
                initObject.Frm_Pln_VisExp.Tx_Plnv[37].Text = VB6Helpers.Left(n, 6) + "-" + VB6Helpers.Right(n, 1);
            }

            //Die, Fecha de Emisión.
            initObject.Frm_Pln_VisExp.Tx_Plnv[38].Text = VB6Helpers.Format(initObject.MODXPLN1.VxPlvs[i].DieFec, "dd/MM/yyyy");

            //Observaciones.
            initObject.Frm_Pln_VisExp.Tx_Plnv[39].Text = initObject.MODXPLN1.VxPlvs[i].ObsPln;

            for (k = 0; k <= (short)VB6Helpers.UBound(initObject.MODGTAB0.VPai); k++)
            {
                if (initObject.MODXPLN1.VxPlvs[i].codpai == initObject.MODGTAB0.VPai[k].Pai_PaiCod)
                {
                    initObject.Frm_Pln_VisExp.Tx_Plnv[42].Text = initObject.MODGTAB0.VPai[k].Pai_PaiNom;
                    break;
                }
            }
        }

        private static short Fn_GetProximo(InitializationObject initObject, dynamic Actual, short Paso)
        {
            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Actual'. Consider using the GetDefaultMember6 helper method.

            T_MODGCVD _MODGCVD = initObject.MODGCVD;

            short n = (short)(Format.StringToDouble(Actual) + Paso);
            while (true)
            {

                if (n == 0 || n > VB6Helpers.UBound(initObject.MODXPLN1.VxPlvs))
                {
                    return VB6Helpers.CShort(Actual);
                }
                else
                {
                    //if (initObject.MODXPLN1.VxPlvs[n].Status != MODGCVD.EstadoEli)
                    if (initObject.MODXPLN1.VxPlvs[n].Status != BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos.T_MODGCVD.EstadoEli)
                    {
                        return n;
                    }
                    else
                    {
                        n = (short)(n + Paso);
                    }

                }

            }
            return 0;
        }

        private static short nueva_vis(InitializationObject initObject)
        {
            short i = 0;
            for (i = 2; i <= 42; i++)
            {
                initObject.Frm_Pln_VisExp.Tx_Plnv[i].Text = "";
            }

            return 0;
        }


        private static void Tx_Fecha_LostFocus(InitializationObject initObject)
        {
            if(!string.IsNullOrEmpty(initObject.Frm_Pln_VisExp.Tx_Fecha.Text))
            {
                if (~MODGPYF1.EsFecha2000(initObject.Frm_Pln_VisExp.Tx_Fecha.Text, initObject, T_MODXPLN1.MsgxPlv) != 0)
                {
                    return;
                }
            }

        }

        private static void Tx_Plnv_LostFocus(InitializationObject initObject, ref short Index)
        {
            double m = 0;
            double fech = 0;



            //Verifica si el Campo es Numérico.
            if (VB6Helpers.Instr(Tx_Numerico, VB6Helpers.Format(VB6Helpers.CStr(Index), "00")) != 0)
            {
                if (VB6Helpers.Val(initObject.Frm_Pln_VisExp.Tx_Plnv[Index].Text) > 0)
                {
                    m = Format.StringToDouble((initObject.Frm_Pln_VisExp.Tx_Plnv[Index].Text));
                    initObject.Frm_Pln_VisExp.Tx_Plnv[Index].Text = MODGPYF0.forma(m, MODGPYF1.FmtObjeto(initObject.Frm_Pln_VisExp.Tx_Plnv[Index]));
                }
                else
                {
                    if (Index != 39)
                    {
                        initObject.Frm_Pln_VisExp.Tx_Plnv[Index].Text = "";
                    }
                }

            }
            else
            {
                if (Index != 39)
                {
                    initObject.Frm_Pln_VisExp.Tx_Plnv[Index].Text = "";
                }
            }

            //Verifica si el Campo es Fecha.
            if (VB6Helpers.Instr(Txt_Fecha, VB6Helpers.Format(VB6Helpers.CStr(Index), "00")) != 0)
            {
                if(!string.IsNullOrEmpty(initObject.Frm_Pln_VisExp.Tx_Plnv[Index].Text))
                {
                    if (~MODGPYF1.EsFecha2000(initObject.Frm_Pln_VisExp.Tx_Plnv[Index].Text, initObject, T_MODXPLN1.MsgxPlv) != 0)
                    {
                        return;
                    }
                    fech = MODGPYF1.ValidaFecha(initObject.Frm_Pln_VisExp.Tx_Plnv[Index].Text);
                    if (fech == 0)
                    {
                        //VB6Helpers.MsgBox("La Fecha ingresada es incorrecta.", MsgBoxStyle.Information, T_MODXPLN1.MsgxPlv);
                        //  initObject.Frm_Pln_VisExp.Tx_Plnv[Index].SetFocus();
                        initObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = " La Fecha ingresada es incorrecta.",
                            Title = T_MODXPLN1.MsgxPlv,
                            Type = TipoMensaje.Informacion
                        });
                        return;
                    }

                    initObject.Frm_Pln_VisExp.Tx_Plnv[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(fech), "dd/MM/yyyy");
                }
            }
        }





    }
}
