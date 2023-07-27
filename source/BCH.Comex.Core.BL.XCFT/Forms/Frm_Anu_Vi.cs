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
    public static class Frm_Anu_Vi
    {
        private static short Contador;
        private static short CodMon;
        private static string FormatoMonto = string.Empty;

        public static void Form_Load(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            Pr_Inicializa(initObj, uow);

        }

        private static void Pr_Inicializa(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short[] Tabs = null;
            short a = 0;
            //Limpiamos el Numero de Operacion
            //--------------------------------
            initObj.Frm_Anu_Vi.Tx_NroOpe[0].Text = string.Empty;
            initObj.Frm_Anu_Vi.Tx_NroOpe[1].Text = "30";
            initObj.Frm_Anu_Vi.Tx_NroOpe[2].Text = string.Empty;
            initObj.Frm_Anu_Vi.Tx_NroOpe[3].Text = "000";
            initObj.Frm_Anu_Vi.Tx_NroOpe[4].Text = "000000";

            //Limpiamos los otros campos
            //--------------------------
            initObj.Frm_Anu_Vi.Tx_FecPre.Text = string.Empty;
            initObj.Frm_Anu_Vi.Lt_PlAnul.Items.Clear();

            initObj.Frm_Anu_Vi.Ch_Reemp.Value = 0;
            initObj.Frm_Anu_Vi.Tx_ObsAnu.Text = "Motivo del reparo :";
            
            //Limpiamos variables.-
            //---------------------
            Contador = 0;
            CodMon = 0;
            initObj.MODANUVI.Var_TipCam = 0;
            initObj.MODANUVI.V_Plani = new T_AnuVi[1] { new T_AnuVi() };
            initObj.MODANUVI.V_PlAnu = new T_AnuVi[1]{new T_AnuVi()};

            initObj.MODANUVI.Vx_AnuReem = initObj.MODANUVI.Vx_AnuReemNull;
            initObj.MODPREEM.VxIdiDec = initObj.MODPREEM.VxIdiDecNull;

            initObj.Frm_Anu_Vi.Tx_NroOpe[0].Text = initObj.MODGUSR.UsrEsp.CentroCosto;
            initObj.Frm_Anu_Vi.Tx_NroOpe[2].Text = initObj.MODGUSR.UsrEsp.Especialista;

            //Tabulaciones de la lista.-
            //----------------------------
            Tabs = new short[6];
            Tabs[0] = (short)7.75;
            Tabs[1] = 62;
            Tabs[2] = (short)108.5;
            Tabs[3] = 186;
            Tabs[4] = (short)193.8;
            a = MODGPYF0.seteatabulador(initObj.Frm_Anu_Vi.Lt_PlAnul, Tabs);
            
            //Carga Tipo Autorizacion.-
            //--------------------------
            Pr_Cargar_Autorizacion(initObj, uow);
            
        }

        private static void Pr_Cargar_Autorizacion(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short b = 0;
            initObj.Frm_Anu_Vi.Cb_TipAut.Items.Clear();
            b = Mdl_Funciones_Varias.SyGet_TipAut(initObj.Mdl_Funciones_Varias, uow);
            MODPREEM.CargaEnLista_TipAut(initObj.Mdl_Funciones_Varias, initObj.Frm_Anu_Vi.Cb_TipAut);
        }

        private static bool Fn_ValDatPla(InitializationObject initObj)
        {
            bool _retValue = false;

            if(Format.StringToDouble((initObj.Frm_Anu_Vi.Tx_MtoAnu.Text)) == 0)
            {

                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Seleccionar una Planilla de la Lista.",
                    Title = T_MODANUVI.MsgAnuVi,
                    Type = TipoMensaje.Informacion,
                    ControlName = "Lt_PlAnul"
                });
               
                return _retValue;
            }

            //Datos de autorizacion.-
            if(initObj.Frm_Anu_Vi.Cb_TipAut.ListIndex == -1)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar el Tipo de Autorización.",
                    Title = T_MODANUVI.MsgAnuVi,
                    Type = TipoMensaje.Informacion,
                    ControlName = "Cb_TipAut_SelectedValue"
                });
                return _retValue;
            }

            if(Convert.ToInt16(initObj.Frm_Anu_Vi.Cb_TipAut.ListIndex) != 6)
            {
                if(initObj.Frm_Anu_Vi.Tx_ObsAnu.Text.Trim().Length < 20)
                {

                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "Debe Indicar el Motivo del reparo.",
                        Title = T_MODANUVI.MsgAnuVi,
                        Type = TipoMensaje.Informacion,
                        ControlName = "Tx_ObsAnu"
                    });
                    return _retValue;
                }

                if(Format.StringToDouble((initObj.Frm_Anu_Vi.Tx_NumAut.Text)) == 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "El Número de Autorización no ha sido Ingresado.",
                        Title = T_MODANUVI.MsgAnuVi,
                        Type = TipoMensaje.Informacion,
                        ControlName = "Tx_NumAut_Text"
                    });
                    return _retValue;
                }

                if(!VB6Helpers.IsDate(initObj.Frm_Anu_Vi.Tx_FecAut.Text))
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "La Fecha de Autorización Ingresada es Incorrecta.",
                        Title = T_MODANUVI.MsgAnuVi,
                        Type = TipoMensaje.Informacion,
                        ControlName = "Tx_FecAut_Text"
                    });
                    return _retValue;
                }
            }

            return true;
        }

        private static bool Fn_ValOpeFec(InitializationObject initObj)
        {
            bool _retValue = false;
            string controlName = string.IsNullOrEmpty(initObj.Frm_Anu_Vi.Tx_NroOpe[0].Text) ? "Tx_NroOpe_000_Text" :
                                 string.IsNullOrEmpty(initObj.Frm_Anu_Vi.Tx_NroOpe[1].Text) ? "Tx_NroOpe_001_Text" :
                                 string.IsNullOrEmpty(initObj.Frm_Anu_Vi.Tx_NroOpe[2].Text) ? "Tx_NroOpe_002_Text" :
                                 string.IsNullOrEmpty(initObj.Frm_Anu_Vi.Tx_NroOpe[3].Text) ? "Tx_NroOpe_003_Text" :
                                 string.IsNullOrEmpty(initObj.Frm_Anu_Vi.Tx_NroOpe[4].Text) ? "Tx_NroOpe_004_Text" : 
                                 string.Empty;

            if (!string.IsNullOrEmpty(controlName))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Indicar el Número de Operación Requerido.",
                    Title = T_MODANUVI.MsgAnuVi,
                    Type = TipoMensaje.Informacion,
                    ControlName = controlName
                });

                return _retValue;

            }

            if(!VB6Helpers.IsDate(initObj.Frm_Anu_Vi.Tx_FecPre.Text))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "La Fecha Ingresada es Incorrecta.",
                    Title = T_MODANUVI.MsgAnuVi,
                    Type = TipoMensaje.Informacion,
                    ControlName = "Tx_FecPre_Text"
                });

                return _retValue;
            }

            return true;
        }

        public static void Bot_Aceptar_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short largo_anuvi = 0;
            short largo_planu = 0;
            short i = 0;
            short x = 0;
            short Y = 0;
            string TipAut = string.Empty;

            if(!Fn_ValDatPla(initObj))
            {
                return;
            }
            if(!Fn_ValOpeFec(initObj))
            {
                return;
            }
            MODXORI.SyGetn_Codtran(initObj.MODXORI, uow);
            largo_anuvi = -1;
            largo_planu = -1;
            largo_anuvi = (short)VB6Helpers.UBound(initObj.MODANUVI.V_Plani);
            largo_planu = (short)VB6Helpers.UBound(initObj.MODANUVI.V_PlAnu);

            if(largo_anuvi >= 0)
            {
                for(i = 0; i <= (short)largo_anuvi; i++)
                {

                    if(initObj.Frm_Anu_Vi.Lt_PlAnul.get_ItemData(i) == initObj.Frm_Anu_Vi.Lt_PlAnul.ListIndex && i == initObj.Frm_Anu_Vi.Lt_PlAnul.get_ItemData((short)(i)))
                    {
                        
                        initObj.MODANUVI.V_PlAnu[largo_planu].codcct = initObj.Frm_Anu_Vi.Tx_NroOpe[0].Text;
                        initObj.MODANUVI.V_PlAnu[largo_planu].codpro = initObj.Frm_Anu_Vi.Tx_NroOpe[1].Text;
                        initObj.MODANUVI.V_PlAnu[largo_planu].codesp = initObj.Frm_Anu_Vi.Tx_NroOpe[2].Text;
                        initObj.MODANUVI.V_PlAnu[largo_planu].codofi = initObj.Frm_Anu_Vi.Tx_NroOpe[3].Text;
                        initObj.MODANUVI.V_PlAnu[largo_planu].codope = initObj.Frm_Anu_Vi.Tx_NroOpe[4].Text;
                        initObj.MODANUVI.V_PlAnu[largo_planu].NumPre = initObj.MODANUVI.V_Plani[i].NumPre;
                        initObj.MODANUVI.V_PlAnu[largo_planu].FecVen = initObj.MODANUVI.V_Plani[i].FecVen;
                        initObj.MODANUVI.V_PlAnu[largo_planu].NumIdi = initObj.MODANUVI.V_Plani[i].NumIdi;
                        initObj.MODANUVI.V_PlAnu[largo_planu].FecIdi = initObj.MODANUVI.V_Plani[i].FecIdi;
                        initObj.MODANUVI.V_PlAnu[largo_planu].numdec = initObj.MODANUVI.V_Plani[i].numdec;
                        initObj.MODANUVI.V_PlAnu[largo_planu].FecDec = initObj.MODANUVI.V_Plani[i].FecDec;
                        initObj.MODANUVI.V_PlAnu[largo_planu].CodMon = initObj.MODANUVI.V_Plani[i].CodMon;
                        initObj.MODANUVI.V_PlAnu[largo_planu].MtoFob = initObj.MODANUVI.V_Plani[i].MtoFob;
                        initObj.MODANUVI.V_PlAnu[largo_planu].MtoFle = initObj.MODANUVI.V_Plani[i].MtoFle;
                        initObj.MODANUVI.V_PlAnu[largo_planu].MtoSeg = initObj.MODANUVI.V_Plani[i].MtoSeg;
                        initObj.MODANUVI.V_PlAnu[largo_planu].MtoCif = initObj.MODANUVI.V_Plani[i].MtoCif;
                        initObj.MODANUVI.V_PlAnu[largo_planu].MtoInt = initObj.MODANUVI.V_Plani[i].MtoInt;
                        initObj.MODANUVI.V_PlAnu[largo_planu].MtoGas = initObj.MODANUVI.V_Plani[i].MtoGas;
                        initObj.MODANUVI.V_PlAnu[largo_planu].MtoTot = initObj.MODANUVI.V_Plani[i].MtoTot;
                        initObj.MODANUVI.V_PlAnu[largo_planu].TotDol = initObj.MODANUVI.V_Plani[i].TotDol;
                        initObj.MODANUVI.V_PlAnu[largo_planu].TipCam = Format.StringToDouble(initObj.Frm_Anu_Vi.Tx_TipCam.Text);
                        x = MODANUVI.Fn_LeePlnSy(initObj, uow, largo_planu);  //Asignamos los datos que faltan a la estruct.
                        Y = MODANUVI.Fn_LeeIntPln(initObj, uow, largo_planu);

                        if(initObj.MODANUVI.V_Plani[i].IndAnu == 0 || initObj.MODANUVI.V_Plani[i].IndAnu == 1 || initObj.MODANUVI.V_Plani[i].IndAnu == 2)
                        {
                            initObj.MODANUVI.V_PlAnu[largo_planu].IndAnu = 1;
                        }
                        else
                        {
                            initObj.MODANUVI.V_PlAnu[largo_planu].IndAnu = 4;
                        }

                        initObj.MODANUVI.V_PlAnu[largo_planu].FecAnu =DateTime.Now.ToString("dd/MM/yyyy");
                        initObj.MODANUVI.V_PlAnu[largo_planu].ParAnu = MODGTAB1.SyGet_Vmc(initObj.MODGTAB0, uow, initObj.MODANUVI.V_PlAnu[largo_planu].CodMon, DateTime.Now.ToString("dd/MM/yyyy"), "P");

                        if (initObj.MODANUVI.V_PlAnu[largo_planu].ParAnu == 0)
                        {
                            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                            {
                                Text = "No se ha podido establecer la Paridad del último día hábil del mes anterior. Reporte este problema.",
                                Title = T_MODANUVI.MsgAnuVi,
                                Type = TipoMensaje.Error
                            });
                            return;
                        }
                        else
                        {
                            initObj.MODANUVI.V_PlAnu[largo_planu].TotAnu = (initObj.MODANUVI.V_PlAnu[largo_planu].MtoTot / initObj.MODANUVI.V_PlAnu[largo_planu].ParAnu);
                        }

                        initObj.MODANUVI.V_PlAnu[largo_planu].observ = initObj.Frm_Anu_Vi.Tx_ObsAnu.Text;
                        int _switchVar1 = Convert.ToInt16(initObj.Frm_Anu_Vi.Cb_TipAut.ListIndex);
                        if(_switchVar1 == 1)
                        {
                            TipAut = "AC";
                        }
                        else if(_switchVar1 == 2)
                        {
                            TipAut = "AE";
                        }
                        else if(_switchVar1 == 3)
                        {
                            TipAut = "CE";
                        }
                        else if(_switchVar1 == 4)
                        {
                            TipAut = "EE";
                        }
                        else if(_switchVar1 == 5)
                        {
                            TipAut = "IN";
                        }
                        else if(_switchVar1 == 6)
                        {
                            TipAut = "RP";
                        }
                        else if(_switchVar1 == 7)
                        {
                            TipAut = string.Empty;
                        }

                        if(Convert.ToInt16(initObj.Frm_Anu_Vi.Cb_TipAut.ListIndex) != 7)
                        {
                            initObj.MODANUVI.V_PlAnu[largo_planu].TipAut = TipAut;
                            initObj.MODANUVI.V_PlAnu[largo_planu].NumAut = VB6Helpers.CInt(initObj.Frm_Anu_Vi.Tx_NumAut.Text);
                            string fec = (initObj.Frm_Anu_Vi.Tx_FecAut.Text == null) ? "01-01-1900" : initObj.Frm_Anu_Vi.Tx_FecAut.Text;
                            initObj.MODANUVI.V_PlAnu[largo_planu].FecAut = Convert.ToDateTime(fec).ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            initObj.MODANUVI.V_PlAnu[largo_planu].TipAut = string.Empty;
                            initObj.MODANUVI.V_PlAnu[largo_planu].NumAut = 0;
                            initObj.MODANUVI.V_PlAnu[largo_planu].FecAut = string.Empty;
                        }

                        //PCP

                        Llena_ParaReb(initObj, uow, i);
                        largo_planu ++;
                    }
                }
            }

            //Llenamos montos para conversion y cambio.-
            MODANUVI.Pr_LlenaMtoAnu(initObj);

            //Si se elige reemplazar, se muestra la pantalla de reemplazo.-
            if((int)initObj.Frm_Anu_Vi.Ch_Reemp.Value != 0)
            {
                initObj.MODANUVI.Var_CodMon = initObj.MODANUVI.V_PlAnu[largo_planu -1].CodMon;
                initObj.MODANUVI.Var_TipCam = Format.StringToDouble((initObj.Frm_Anu_Vi.Tx_TipCam.Text));  //PACP
                initObj.MODANUVI.Vx_AnuReem.HayRee = (short)(true ? -1 : 0);
            }            

            initObj.MODANUVI.Vx_AnuReem.AcepAnu = (short)(true ? -1 : 0);
        }

        public static short Llena_ParaReb(InitializationObject initObj, UnitOfWorkCext01 uow, short ind_vplani)
        {
            short Largo_Anucob = 0;
            short Ind_Reb = 0;
            // Agregar nueva relación de rebajes
            if(initObj.MODCVDIMMM.AnuCob == null)
            {
                initObj.MODCVDIMMM.AnuCob = new ParaAnuCob[0];
            }
            Largo_Anucob = 0;
            Largo_Anucob = (short)VB6Helpers.UBound(initObj.MODCVDIMMM.AnuCob);
            Ind_Reb = (short)(Largo_Anucob + 1);
            VB6Helpers.RedimPreserve(ref initObj.MODCVDIMMM.AnuCob, 0, Ind_Reb);

            

            initObj.MODCVDIMMM.AnuCob[Ind_Reb].NumPla = VB6Helpers.Str(initObj.MODANUVI.V_Plani[ind_vplani].NumPre);
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].NumIdi = VB6Helpers.Str(initObj.MODANUVI.V_Plani[ind_vplani].NumIdi);
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].FecIdi = initObj.MODANUVI.V_Plani[ind_vplani].FecIdi;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].numdec = initObj.MODANUVI.V_Plani[ind_vplani].numdec;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].FecDec = initObj.MODANUVI.V_Plani[ind_vplani].FecDec;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].NumPri = 0;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].PagChi = 0;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].Moneda = initObj.MODANUVI.V_Plani[ind_vplani].CodMon;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].MtoFob = initObj.MODANUVI.V_Plani[ind_vplani].MtoFob;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].MtoFle = initObj.MODANUVI.V_Plani[ind_vplani].MtoFle;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].MtoSeg = initObj.MODANUVI.V_Plani[ind_vplani].MtoSeg;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].FecDec = initObj.MODANUVI.V_Plani[ind_vplani].FecDec;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].FobMer = 0;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].FleMer = 0;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].SegMer = 0;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].MtoCif = initObj.MODANUVI.V_Plani[ind_vplani].MtoCif;
            initObj.MODCVDIMMM.AnuCob[Ind_Reb].CifUsd = initObj.MODANUVI.V_Plani[ind_vplani].CifDol;

            if(initObj.MODANUVI.V_Plani[ind_vplani].FecDec != string.Empty)
            {
                initObj.MODPREEM.ParDec = MODPREEM.Par_Dec(initObj, uow, initObj.MODANUVI.V_Plani[ind_vplani].FecDec, T_MODGTAB0.MndDol);
                initObj.MODCVDIMMM.AnuCob[Ind_Reb].ParDec = initObj.MODPREEM.ParDec;
            }
            else
            {
                initObj.MODPREEM.ParDec = 0;
            }

            initObj.MODPREEM.ParIdi = 0;

            initObj.MODCVDIMMM.AnuCob[Ind_Reb].TCVDia = initObj.MODANUVI.V_Plani[ind_vplani].TipCamo;

            return 0;
        }

        public static void Pr_ListaPlan(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short Max_Anuvi = 0;
            short i = 0;
            string Lista = string.Empty;
            string Nemonico = string.Empty;
            short Pos_Cod = 0;

            Max_Anuvi = (short)VB6Helpers.UBound(initObj.MODANUVI.V_Plani);

            initObj.Frm_Anu_Vi.Lt_PlAnul.Items.Clear();

            for(i = 0; i <= (short)Max_Anuvi; i++)
            {
                Lista = string.Empty;
                Lista = Lista + initObj.MODANUVI.V_Plani[i].NumPre + VB6Helpers.Chr(9);
                Lista = Lista + Format.FormatCurrency((initObj.MODANUVI.V_Plani[i].NumIdi), "000000") + VB6Helpers.Chr(9);
                Lista = Lista + initObj.MODANUVI.V_Plani[i].numdec + VB6Helpers.Chr(9);
                Nemonico = MODGTAB0.Get_NemMnd(initObj.MODGTAB0, uow, initObj.MODANUVI.V_Plani[i].CodMon);  //Nemonico.-
                Pos_Cod = MODGTAB0.Get_VMnd(initObj.MODGTAB0, uow, initObj.MODANUVI.V_Plani[i].CodMon);  //Formato de moneda.-
                if(initObj.MODGTAB0.VMnd[Pos_Cod].Mnd_MndSin != 0)
                {
                    FormatoMonto = T_MODGCON0.FormatoSinDec;
                }
                else
                {
                    FormatoMonto = T_MODGCON0.FormatoConDec;
                }

                Lista = Lista + Nemonico + VB6Helpers.Chr(9);
                Lista += MODGPYF0.forma(initObj.MODANUVI.V_Plani[i].MtoTot, FormatoMonto);
                initObj.Frm_Anu_Vi.Lt_PlAnul.Items.Add(new UI_ListBoxItem() { Value = Lista, Data = i });
                initObj.Frm_Anu_Vi.Lt_PlAnul.Enabled = true;
            }

        }

        public static void Bot_Ok_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            string FecVen = string.Empty;
            string Oper = string.Empty;
            if(Fn_ValOpeFec(initObj) == false)
            {
                return;
            }

            FecVen = Convert.ToDateTime(initObj.Frm_Anu_Vi.Tx_FecPre.Text).ToString("dd/MM/yyyy");
            if(FecVen == DateTime.Now.ToString("dd/MM/yyyy"))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "No es posible anular Planillas generadas el día de hoy.",
                    Title = T_MODANUVI.MsgAnuVi,
                    Type = TipoMensaje.Informacion,
                    ControlName = "Tx_FecPre_Text"
                });
                return;
            }

            //Obtenemos las Planillas para esa operacion y las listamos
            //---------------------------------------------------------
            Oper = initObj.Frm_Anu_Vi.Tx_NroOpe[0].Text + "-" + initObj.Frm_Anu_Vi.Tx_NroOpe[1].Text + "-" + initObj.Frm_Anu_Vi.Tx_NroOpe[2].Text + "-" + initObj.Frm_Anu_Vi.Tx_NroOpe[3].Text + "-" + initObj.Frm_Anu_Vi.Tx_NroOpe[4].Text;

            if (~MODANUVI.SyGet_PlPrt(initObj, uow, Oper, FecVen) != 0)
            {
                return;
            }

            if (~MODANUVI.SyGet_PlAnu(initObj, uow, Oper, FecVen) != 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "No hay Planillas disponibles para esta operación en esta fecha, o bien, las que habían han sido anuladas previamente.",
                    Title = T_MODANUVI.MsgAnuVi,
                    Type = TipoMensaje.Informacion,
                    ControlName = "Tx_FecPre_Text"
                });
                return;
            }

            //Revisa si operacion es Cosmos
            initObj.MODXORI.gb_esCosmos = false;
            //if(MODXORI.SyGet_CtaCte(initObj.MODANUVI.Vx_AnuReem.PrtCli,initObj, uow))
            //{
                //string token = MODXORI.SrvGetCtaCos(initObj.MODXORI.gs_ctacte_party);
            MODXORI.Inet1_StateChanged(int.Parse(initObj.Frm_Anu_Vi.Tx_NroOpe[1].Text), initObj);
            //}           

            Pr_ListaPlan(initObj, uow);
        }

        public static void Bot_Cancel_Click(InitializationObject initObj)
        {

            initObj.MODANUVI.V_Plani = new T_AnuVi[1];
            initObj.MODANUVI.V_PlAnu = new T_AnuVi[1];
            initObj.MODVPLE.IntAnu = new T_IntPla[1];
            initObj.MODANUVI.Vx_AnuReem = initObj.MODANUVI.Vx_AnuReemNull;
        }

        public static void Lt_PlAnul_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short k = -1;
            short j = 0;
            short i = 0;
            short indice = 0;
            double TC = 0;
            double Interes = 0;

            if(initObj.Frm_Anu_Vi.Lt_PlAnul.ListIndex < 0)
            {
                Contador = 0;
                CodMon = 0;
            }

            //Almacenamos la Moneda de la primera seleccion y la comparamos con
            //las otras selecciones
            indice = (short)initObj.Frm_Anu_Vi.Lt_PlAnul.get_ItemData((short)initObj.Frm_Anu_Vi.Lt_PlAnul.ListIndex);

            for(i = 0; i <= indice; i++)
            {
                if(initObj.MODANUVI.V_Plani[i].MtoInt > 0 || initObj.MODANUVI.V_Plani[i].MtoGas > 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "El sistema no puede anular esta planilla, ya que el monto del interés o el monto de los gastos del cedente es mayor que cero.",
                        Title = T_MODANUVI.MsgAnuVi,
                        Type = TipoMensaje.Informacion
                    });
                    return;
                }
                else
                {
                    if(initObj.Frm_Anu_Vi.Lt_PlAnul.ListIndex >= 0)
                    {
                        if(k == -1)
                        {
                            k = (short)(i);
                        }
                        if(Contador == 0)
                        {
                            //primera seleccion.-
                            Contador = (short)(Contador + 1);
                            CodMon = initObj.MODANUVI.V_Plani[i].CodMon;
                        }

                        if(CodMon != initObj.MODANUVI.V_Plani[i].CodMon)
                        {
                            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                            {
                                Text = "Todas las Planillas Seleccionadas deben corresponder a la misma Moneda.",
                                Title = T_MODANUVI.MsgAnuVi,
                                Type = TipoMensaje.Informacion
                            });

                            initObj.Frm_Anu_Vi.Lt_PlAnul.ListIndex = -1;
                            
                            return;
                        }

                        Interes = initObj.MODANUVI.V_Plani[i].MtoTot;
                    }
                }
            }

            //Monto de Interes.-
           initObj.Frm_Anu_Vi.Tx_MtoAnu.Text = MODGPYF0.forma(Interes, T_MODGCON0.FormatoConDec).Trim();

            //Tipo de Cambio
            if(k != -1)
            {
                if(initObj.MODANUVI.V_Plani[k].ParPag > 0)
                {
                    TC = Math.Round(initObj.MODANUVI.V_Plani[k].TipCam / initObj.MODANUVI.V_Plani[k].ParPag, 2);
                }
            }

            initObj.Frm_Anu_Vi.Tx_TipCam.Text = TC.ToString("##0.000#");
            if(initObj.MODANUVI.V_Plani[initObj.Frm_Anu_Vi.Lt_PlAnul.get_ItemData((short)initObj.Frm_Anu_Vi.Lt_PlAnul.ListIndex)].IndAnu == 3)
            {
                initObj.Frm_Anu_Vi.Ch_Reemp.Value = 0;
                initObj.Frm_Anu_Vi.Ch_Reemp.Enabled = false;
            }


            initObj.Frm_Anu_Vi.Tx_ObsAnu.Text = "Motivo del reparo :\r\n";

        }

        public static void Tx_FecPre_LostFocus(UI_Mdi_Principal Mdi_Principal, UI_Frm_Anu_Vi Frm_Anu_Vi,  InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            double Fecha = 0;
           T_MODGANU MODGANU = initObj.MODGANU;


            if (initObj.Frm_Anu_Vi.Tx_FecPre.Text != "")
            {
                if (~MODGPYF1.EsFecha2000(Frm_Anu_Vi.Tx_FecPre.Text,initObj, T_MODGCVD.MsgCVD) != 0)
                {
                    return;
                }
                Fecha = MODGPYF1.ValidaFecha((initObj.Frm_Anu_Vi.Tx_FecPre.Text));
                if (Fecha == 0)
                {
                    Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "La Fecha Ingresada es Incorrecta.",
                        Type = TipoMensaje.Informacion,
                        Title ="Anulación de Planillas Visibles",
                        ControlName = "Tx_FecPre_Text"
                    });
                    return;
                }
                initObj.Frm_Anu_Vi.Tx_FecPre.Text = VB6Helpers.Format(VB6Helpers.CStr(Fecha), "dd/mm/yyyy");
            }
        }

        public static void Tx_FecPre_KeyPress(ref short KeyAscii, UI_Frm_Anu_Vi Frm_Anu_Vi, InitializationObject initObj, UnitOfWorkCext01 uow, string text)
        {
            MODGPYF1.formatfecha(Frm_Anu_Vi.Tx_FecPre, ref KeyAscii);
          
            /*if (KeyAscii == 13)
            {
                KeyAscii = 0;
                VB6Helpers.SendKeys("{Tab}");
            }
            else
            {
                MODGPYF1.formatfecha(Frm_Anu_Vi.Tx_FecPre, ref KeyAscii);
            }*/
        }

        public static void Tx_NroOpe_LostFocus(ref short Index, UI_Frm_Anu_Vi Frm_Anu_Vi, InitializationObject initObj)
        {

            T_MODGPYF0 MODGPYF0 = initObj.MODGPYF0;


            int n = 0;
            if (string.IsNullOrEmpty(Frm_Anu_Vi.Tx_NroOpe[Index].Text))
            {
                n = 0;
            }
            else
            {
                n = (int)Format.StringToDouble(Frm_Anu_Vi.Tx_NroOpe[Index].Text);
            }

            switch (Index)
            {
                case 0:
                    Frm_Anu_Vi.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "000");
                    break;
                case 1:
                    Frm_Anu_Vi.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "00");
                    break;
                case 2:
                    Frm_Anu_Vi.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "00");
                    break;
                case 3:
                    Frm_Anu_Vi.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "000");
                    break;
                case 4:
                    Frm_Anu_Vi.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "00000");
                    break;
            }

        }
    
    }
}
