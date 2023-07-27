using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public static class Frm_Rem_PVI
    {

        private static short IndiceInt;
        private static short IndiceFin;
        private static short FlgYaMostro;
        private static double MtoInteres;
        private static short IngPlan;
        private static short Flag_TipCam;

        public static void Form_Load(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short[] Tabs = null;
            short a;
            short e;


            //Lista de Interes.-
            Tabs = new short[5];
            Tabs[0] = (short)15.5;
            Tabs[1] = (short)77.5;
            Tabs[2] = (short)139.5;
            Tabs[3] = (short)162.75;
            Tabs[4] = (short)178.25;

            //Lista Final.-
            Tabs = new short[3];
            Tabs[0] = 10;
            Tabs[1] = 45;
            a = MODGPYF0.seteatabulador(initObj.Frm_Rem_PVI.Lt_Final, Tabs);

            Pr_Inicializa(initObj);

            //Carga Plaza Banco Central.-
            //---------------------------
            e = MODGTAB1.SyGetn_Pbc(initObj.MODGTAB1, uow);
            if(e != 0)
            {
                initObj.Frm_Rem_PVI.Cb_Pbc.Items.Clear();
                MODGTAB1.CargaEnListaPbc(initObj.Frm_Rem_PVI.Cb_Pbc, initObj);
            }

            Pr_GetMoneda(initObj, uow);

            //Paridad y Tipo Cambio.-
            //------------------------
            Pr_ParyTipCa(initObj, uow);

            //Cargamos los datos de la declaracion y del idi
            //------------------------
            Pr_CargaDecId(initObj);

        }

        public static void Bot_Acepta_Click(InitializationObject initObj)
        {
            short TotPlanilla = 0;
            short TotReg = 0;
            short i = 0;
            TotReg = (short)VB6Helpers.UBound(initObj.MODPREEM.Vx_PReem);
            if(IngPlan == -1)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.YesNo,
                    Text = "Hay datos que no han sido ingresados a la lista. Desea Ingresarlos ?"
                });
                if(initObj.Frm_Rem_PVI.PopUps.Count > 0)
                {
                    return;
                }
            }

            initObj.MODANUVI.Vx_AnuReem.HayRee = (short)(true ? -1 : 0);
            for(i = 1; i <= (short)TotReg; i++)
            {
                if(initObj.MODPREEM.Vx_PReem[i].Estado == 1)
                {
                    //total de planillas
                    TotPlanilla = (short)(TotPlanilla + 1);
                }
            }

            MODPREEM.Pr_LlenaMtoRee(initObj.MODPREEM, initObj.MODANUVI);

            //Refunde
            //------------------
            MODPREEM.Pr_RefundeGrDO(initObj.MODCVDIMMM);
            //------------------

            initObj.MODANUVI.Vx_AnuReem.TotPln = TotPlanilla;
            initObj.MODANUVI.Vx_AnuReem.AcepRee = (short)(true ? -1 : 0);

        }

        public static void Bot_Cancel_Click(InitializationObject initObj)
        {
            initObj.MODPREEM.Vx_PReem = new Plan_Reemp[1];
            initObj.MODVPLE.IntPla = new T_IntPla[1];

            initObj.MODGFYS.RIdiFin = new R_Idi[1];
            initObj.MODCVDIMMM.RDecFin = new r_dec[1];

            VB6Helpers.Erase(ref initObj.MODGFYS.RIdiIni);
            VB6Helpers.Erase(ref initObj. MODCVDIMMM.RDecIni);
        }

        public static void Bot_NoFinal_Click(InitializationObject initObj)
        {
            short IndLis = 0;
            if(initObj.Frm_Rem_PVI.Lt_Final.ListIndex > -1)
            {
                IndLis = (short)initObj.Frm_Rem_PVI.Lt_Final.get_ItemData((short)initObj.Frm_Rem_PVI.Lt_Final.ListIndex);
                initObj.MODPREEM.Vx_PReem[IndLis].Estado = 9;
                initObj.Frm_Rem_PVI.Lt_Final.Items.RemoveAt((short)initObj.Frm_Rem_PVI.Lt_Final.ListIndex);
                Pr_LimIdiDec(initObj);
                Pr_LimOtros(initObj);
            }

        }

        public static void Bot_OkDec_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short x = 0;
            int NumIdi = 0;
            string FecIdi = string.Empty;
            string numdec = string.Empty;
            string FecDec = string.Empty;
            short CodPag = 0;
            string PrtImp = string.Empty;

            if(~Fn_ValIdiDec(initObj) != 0)
            {
                return;
            }

            numdec = CambiaGuion(VB6Helpers.Trim(initObj.Frm_Rem_PVI.Tx_NumDec.Text));
            FecDec = DateTime.Parse(initObj.Frm_Rem_PVI.Tx_FecDec.Text).ToString("dd/MM/yyyy");
            CodPag = VB6Helpers.CShort(MODGPYF1.ValTexto(initObj.Frm_Rem_PVI.Tx_CodPag.Text, initObj));

            if(numdec != string.Empty && numdec != "________________-_")
            {
                if(~MODPREEM.SyGet_IdiDec(initObj, uow, numdec, FecDec, CodPag, initObj.MODANUVI.Vx_AnuReem.PrtCli) != 0)
                {
                    return;
                }

                if(initObj.MODPREEM.VxIdiDec.flag == 1)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "Esta Declaración de Importación No Existe.",
                        Title = T_MODANUVI.MsgRemPv,
                        Type = TipoMensaje.Informacion
                    });
                    
                    Pr_LimIdiDec(initObj);
                    Pr_Habilita(initObj, 0);
                    return;
                }

                //El primer num. de conocim.
                x = MODPREEM.Getn_Conoc(initObj,uow, numdec, FecDec);
                initObj.Frm_Rem_PVI.Tx_NroCon.Text = MODGFYS.FtoNumCon(initObj.MODPREEM.Vx_ConEmb[1].NumBlw);
                initObj.Frm_Rem_PVI.Tx_FecCon.Text = initObj.MODPREEM.VxIdiDec.FecEmb;
            }

            Pr_Habilita(initObj, -1);
            Pr_CargaPln(initObj,uow);
            MODPREEM.Pr_PlAcceso(initObj, uow, FecDec, numdec);
            MODPREEM.Pr_VeEnPlan(initObj, numdec, FecDec, CodPag);

            //Tx_CodPlz.SetFocus
            MtoInteres = Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_IntPla.Text));

            //Flag para determinar si hay datos de planilla que no han sido
            //ingresados a la lista.-
            IngPlan = (short)(true ? -1 : 0);
        }

        public static string CambiaGuion(string Texto)
        {
            short cont = 0;
            string nuevo = string.Empty;
            for(cont = 1; cont <= (short)VB6Helpers.Len(Texto); cont++)
            {
                if(VB6Helpers.Mid(Texto, cont, 1) == "_")
                {
                    nuevo += "0";
                }
            }

            for(cont = 1; cont <= (short)VB6Helpers.Len(Texto); cont++)
            {
                if(VB6Helpers.Mid(Texto, cont, 1) != "_")
                {
                    nuevo += VB6Helpers.Mid(Texto, cont, 1);
                }
            }
            return nuevo;
        }

        public static void Bot_OkFinal_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {

            int Plani = VB6Helpers.CInt(MODGPYF1.ValTexto(initObj.Frm_Rem_PVI.Pn_NroPre.Text, initObj));
            double MontoTot = 0;
            double Detalle_Interes = 0;
            double Interes = 0;
            short Indice_Ree = 0;
            //Numero de Planilla
            //----------------------------
            if(Plani == 0)
            {
                //=> si aun no se ingresa a la lista
                Plani = 999999;
            }

            //Validacion de campos
            //----------------------------
            if(~Fn_ValIdiDec(initObj) != 0)
            {
                return;
            }
            if (~Fn_ValiCampos(initObj) != 0)
            {
                return;
            }

            //Validamos el total de Montos de Planilla.-
            //------------------------------------------

            MontoTot = Format.StringToDouble(VB6Helpers.Format(MODGPYF1.ValTexto(initObj.Frm_Rem_PVI.Tx_MtoFob.Text, initObj) + MODGPYF1.ValTexto(initObj.Frm_Rem_PVI.Tx_MtoFle.Text, initObj) + MODGPYF1.ValTexto(initObj.Frm_Rem_PVI.Tx_MtoSeg.Text, initObj), initObj.MODANUVI.FtoSal));

            if(MontoTot != Format.StringToDouble((initObj.Frm_Rem_PVI.Pn_ValCif.Text)))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "El Valor Cif de la Planilla no es igual a la suma de valor Fob, Flete y Seguro.",
                    Title = T_MODANUVI.MsgRemPv,
                    Type = TipoMensaje.Informacion
                });
                return;
            }

            //Obtenemos la suma de Interes
            //-----------------------------
            Detalle_Interes = Fn_ValIntFin(initObj, Plani);
            Detalle_Interes = Format.StringToDouble(Format.FormatCurrency((Detalle_Interes), "0.00"));
            Interes = Format.StringToDouble(Format.FormatCurrency((MODGPYF1.ValTexto(initObj.Frm_Rem_PVI.Tx_IntPla.Text, initObj)), "0.00"));

            //Llenamos la estructura
            //----------------------
            Indice_Ree = Fn_LlenaRee(initObj, uow);

            //Relacion Declaracion
            //----------------------
            MODPREEM.Pr_RelDec(initObj, Indice_Ree, initObj.MODPREEM.NuevaPlan);

            //Relacion Rebajes
            //----------------------
            MODPREEM.Pr_RelReb(initObj, Indice_Ree, initObj.MODPREEM.NuevaPlan);

            //Cargamos la lista
            //----------------------
            Pr_CargaLis(initObj);

            //Limpiamos los campos
            //----------------------
            Pr_LimIdiDec(initObj);
            Pr_LimOtros(initObj);

            initObj.Frm_Rem_PVI.Bot_Acepta.Enabled = true;
            IngPlan = (short)(false ? -1 : 0);
        }

        public static void Ch_Acuerdo_Click(InitializationObject initObj, ref short Value)
        {

            switch(Value)
            {
                case 0:
                    initObj.Frm_Rem_PVI.Tx_CantAc.Text = string.Empty;
                    initObj.Frm_Rem_PVI.Tx_NumAc1.Text = string.Empty;
                    initObj.Frm_Rem_PVI.Tx_NumAc2.Text = string.Empty;
                    Enabled_Acuerdos(initObj, false);

                    break;
                case -1:
                    Enabled_Acuerdos(initObj, true);

                    break;
            }

        }

        public static void Ch_ConvCre_Click(InitializationObject initObj, ref short Value)
        {

            switch(Value)
            {
                case 0:
                    initObj.Frm_Rem_PVI.Tx_FecDeb.Text = string.Empty;
                    initObj.Frm_Rem_PVI.Tx_DocChi.Text = string.Empty;
                    initObj.Frm_Rem_PVI.Tx_DocExt.Text = string.Empty;
                    Enabled_ConvCre(initObj, false);
                    break;
                case -1:
                    Enabled_ConvCre(initObj, true);
                    

                    break;
            }

        }

        public static void Ch_CPagos_Click(InitializationObject initObj, ref short Value)
        {

            switch(Value)
            {
                case 0:
                    initObj.Frm_Rem_PVI.Tx_NCpago.Text = string.Empty;
                    initObj.Frm_Rem_PVI.Tx_NCuota.Text = string.Empty;
                    Enabled_Cpagos(initObj, false);

                    break;
                case -1:
                    Enabled_Cpagos(initObj, true);

                    break;
            }

        }

        //Llenamos la estructura de Planilla .-
        private static short Fn_LlenaRee(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short largo_reem = 0;
            short Largo_Int = 0;
            double NroPres = 0;
            short PosPbc = 0;
            short i = 0;
            string x = string.Empty;
            string ru = string.Empty;
            string RutSin = string.Empty;
            string IdRee = string.Empty;
            string observ = string.Empty;
            
            largo_reem = (short)VB6Helpers.UBound(initObj.MODPREEM.Vx_PReem);
            Largo_Int = (short)VB6Helpers.UBound(initObj.MODVPLE.IntPla);

            if(IndiceFin == 0)
            {
                //Ingreso Nuevo.-
                largo_reem = (short)(largo_reem + 1);
                VB6Helpers.RedimPreserve(ref initObj.MODPREEM.Vx_PReem, 0, largo_reem);
                initObj.MODPREEM.NuevaPlan = (short)(true ? -1 : 0);
            }
            else
            {
                largo_reem = IndiceFin;  //Modifico.-
                initObj.MODPREEM.NuevaPlan = (short)(false ? -1 : 0);
            }

            //Número Presentación.
            //--------------------
            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'MODGPYF1.ValTexto(Pn_NroPre)'. Consider using the GetDefaultMember6 helper method.
            if(Format.StringToDouble((initObj.Frm_Rem_PVI.Pn_NroPre.Text)) == 0)
            {
                NroPres = MODGRNG.LeeSceRng(initObj.MODGRNG, initObj.MODGUSR,initObj.Mdi_Principal, uow, T_MODGRNG.Rng_PlaVis);  //Nuevo
            }
            else
            {
                NroPres = Format.StringToDouble((initObj.Frm_Rem_PVI.Pn_NroPre.Text));
            }

            initObj.MODPREEM.Vx_PReem[largo_reem].NumPla = (int)NroPres;
            //--------------------
            //Fecha de Venta
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].FecVen = DateTime.Now.ToString("dd/MM/yyyy");
            //Nombre Importador
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].NomImp = initObj.Frm_Rem_PVI.Pn_Import.Text;

            //Rut
            //----------------------
            x = initObj.Frm_Rem_PVI.Pn_RutImp.Text;
            if(x != string.Empty)
            {
                ru = VB6Helpers.Trim(VB6Helpers.Mid(x, 1, VB6Helpers.Len(x) - 2)) + VB6Helpers.Right(x, 1);
            }

            RutSin = MODGPYF0.Componer(ru, ".", string.Empty);
            initObj.MODPREEM.Vx_PReem[largo_reem].RutImp = VB6Helpers.Right("000000000" + RutSin, 10);

            //''Vx_PReem(largo_reem%).CodPlz = MODPREEM.VxIdiDec.PlzApr
            initObj.MODPREEM.Vx_PReem[largo_reem].CodPlz = (short)initObj.Frm_Rem_PVI.Cb_Pbc.get_ItemData_((short)initObj.Frm_Rem_PVI.Cb_Pbc.ListIndex);
            //Declaracion
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].numdec = initObj.Frm_Rem_PVI.Tx_NumDec.Text;
            initObj.MODPREEM.Vx_PReem[largo_reem].FecDec = VB6Helpers.Format(initObj.Frm_Rem_PVI.Tx_FecDec.Text, "dd/mm/yyyy");
            //Conocimiento
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].NumCon = initObj.Frm_Rem_PVI.Tx_NroCon.Text;
            initObj.MODPREEM.Vx_PReem[largo_reem].FecCon = VB6Helpers.Format(initObj.Frm_Rem_PVI.Tx_FecCon.Text, "dd/mm/yyyy");
            //Codigo y Codigo Banco Chile
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].Codigo = "205000";
            //Vx_PReem(largo_reem%).CodBch = ValTexto(Tx_CodPlz)
            //Vx_PReem(largo_reem%).NomPlz = Pn_NomPlz.Text
            initObj.MODPREEM.Vx_PReem[largo_reem].CodBch = (short)initObj.Frm_Rem_PVI.Cb_Pbc.get_ItemData_((short)initObj.Frm_Rem_PVI.Cb_Pbc.ListIndex);
            PosPbc = MODGTAB1.Get_VPbc(initObj, uow, (short)(initObj.Frm_Rem_PVI.Cb_Pbc.get_ItemData_((short)initObj.Frm_Rem_PVI.Cb_Pbc.ListIndex)));
            if (PosPbc >= 0)
                initObj.MODPREEM.Vx_PReem[largo_reem].NomPlz = initObj.MODGTAB1.VPbc[PosPbc].Pbc_PbcDes;
            else
                initObj.MODPREEM.Vx_PReem[largo_reem].NomPlz = string.Empty;

            //Forma Pago
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].CodPag = VB6Helpers.CShort(MODGPYF1.ValTexto(initObj.Frm_Rem_PVI.Tx_CodPag.Text, initObj));
            //Pais de Pago
            //'--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].CodPPa = VB6Helpers.CShort(MODGPYF1.ValTexto(initObj.Frm_Rem_PVI.Tx_CodPai.Text, initObj));
            initObj.MODPREEM.Vx_PReem[largo_reem].NomPai = initObj.Frm_Rem_PVI.Pn_PPago.Text;
            //Moneda
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].CodMPa = VB6Helpers.CShort(MODGPYF1.ValTexto(initObj.Frm_Rem_PVI.Pn_CodMon.Text, initObj));  //Moneda Bco.Central
            initObj.MODPREEM.Vx_PReem[largo_reem].CodMon = initObj.MODANUVI.Var_CodMon;  //Moneda BCh.
            initObj.MODPREEM.Vx_PReem[largo_reem].NomMon = initObj.Frm_Rem_PVI.Pn_Moneda.Text;
            initObj.MODPREEM.Vx_PReem[largo_reem].NemMon = initObj.MODPREEM.NemMoneda;
            //Paridad y Tipo de Cambio
            //--------------------
           initObj.MODPREEM.Vx_PReem[largo_reem].ParPag = Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_Paridad.Text));
           initObj.MODPREEM.Vx_PReem[largo_reem].TipCamo = Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_TipCam.Text));  //Tipo de cambio original
            initObj.MODPREEM.Vx_PReem[largo_reem].TipCam = Format.StringToDouble((initObj.Frm_Rem_PVI.Pn_TCDol.Text));  //Tip.Cambio equivalente.-
            //Montos
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].MtoFob = Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_MtoFob.Text));
            initObj.MODPREEM.Vx_PReem[largo_reem].MtoFle = Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_MtoFle.Text));
            initObj.MODPREEM.Vx_PReem[largo_reem].MtoSeg = Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_MtoSeg.Text));
            initObj.MODPREEM.Vx_PReem[largo_reem].MtoCif = Format.StringToDouble((initObj.Frm_Rem_PVI.Pn_ValCif.Text));
            initObj.MODPREEM.Vx_PReem[largo_reem].MtoInt = Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_IntPla.Text));
            initObj.MODPREEM.Vx_PReem[largo_reem].GasBan = Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_GasBan.Text));
            initObj.MODPREEM.Vx_PReem[largo_reem].TotOri = Format.StringToDouble((initObj.Frm_Rem_PVI.Pn_MtoTot.Text));
            initObj.MODPREEM.Vx_PReem[largo_reem].CifDol = Format.StringToDouble((initObj.Frm_Rem_PVI.Pn_CifDol.Text));
            initObj.MODPREEM.Vx_PReem[largo_reem].TotDol = Format.StringToDouble((initObj.Frm_Rem_PVI.Pn_TotDol.Text));
            //Fecha de Vencimiento
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].FecVto = DateTime.Parse(initObj.Frm_Rem_PVI.Tx_FecVen.Text).ToString("dd/MM/yyyy");
            //Cuadro de Pagos.-
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].HayCua = (short)initObj.Frm_Rem_PVI.Ch_CPagos.Value;
            initObj.MODPREEM.Vx_PReem[largo_reem].NumCua = VB6Helpers.CInt(MODGPYF1.ValTexto(initObj.Frm_Rem_PVI.Tx_NCpago.Text, initObj));
            initObj.MODPREEM.Vx_PReem[largo_reem].numcuo = VB6Helpers.CShort(MODGPYF1.ValTexto(initObj.Frm_Rem_PVI.Tx_NCuota.Text, initObj));
            //Acuerdo
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].HayAcu = (short)initObj.Frm_Rem_PVI.Ch_Acuerdo.Value;
            initObj.MODPREEM.Vx_PReem[largo_reem].NumAcu = VB6Helpers.CShort(MODGPYF1.ValTexto(initObj.Frm_Rem_PVI.Tx_CantAc.Text, initObj));
            initObj.MODPREEM.Vx_PReem[largo_reem].Acuer1 = initObj.Frm_Rem_PVI.Tx_NumAc1.Text;
            initObj.MODPREEM.Vx_PReem[largo_reem].Acuer2 = initObj.Frm_Rem_PVI.Tx_NumAc2.Text;
            //Datos de Anulacion
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].HayAnu = 0;
            initObj.MODPREEM.Vx_PReem[largo_reem].VenAnu = string.Empty;
            initObj.MODPREEM.Vx_PReem[largo_reem].IndAnu = 2;
            initObj.MODPREEM.Vx_PReem[largo_reem].NumReg = 0;
            initObj.MODPREEM.Vx_PReem[largo_reem].ParAnu = 0;
            initObj.MODPREEM.Vx_PReem[largo_reem].TotAnu = 0;
            initObj.MODPREEM.Vx_PReem[largo_reem].FecAnu = string.Empty;
            //Planilla Reemplazada.-
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].NumPln_R = VB6Helpers.CInt(MODGPYF1.ValTexto(initObj.Frm_Rem_PVI.Tx_NroPre.Text, initObj));
            initObj.MODPREEM.Vx_PReem[largo_reem].FecPln_R = DateTime.Parse(initObj.Frm_Rem_PVI.Tx_FecRee.Text).ToString("dd/MM/yyyy");
            initObj.MODPREEM.Vx_PReem[largo_reem].CodPlz_R = initObj.MODPREEM.Vx_PReem[largo_reem].CodPlz;
            MODPREEM.Pr_DatosPl_R(initObj, initObj.MODPREEM.Vx_PReem[largo_reem].NumPln_R, initObj.MODPREEM.Vx_PReem[largo_reem].FecPln_R, largo_reem);
            initObj.MODPREEM.Vx_PReem[largo_reem].HayRpl = (short)(true ? -1 : 0);  //PACP.-

            //Convenio Credito
            //--------------------
            initObj.MODPREEM.Vx_PReem[largo_reem].FecDeb = DateTime.Parse(initObj.Frm_Rem_PVI.Tx_FecDeb.Text).ToString("dd/MM/yyyy");
            initObj.MODPREEM.Vx_PReem[largo_reem].DocChi = initObj.Frm_Rem_PVI.Tx_DocChi.Text;
            initObj.MODPREEM.Vx_PReem[largo_reem].DocExt = initObj.Frm_Rem_PVI.Tx_DocExt.Text;

            //Observaciones
            //--------------------
            IdRee = initObj.Module1.Codop.Cent_Costo + "-" + initObj.Module1.Codop.Id_Product + "-" + initObj.Module1.Codop.Id_Especia + "-" + initObj.Module1.Codop.Id_Empresa + "-" + initObj.Module1.Codop.Id_Operacion;
            observ = "OPERACION  " + IdRee + "  ESP  " + initObj.Module1.Codop.Id_Especia;
            if(VB6Helpers.Trim(initObj.Module1.PartysOpe[T_MODGCVD.ICli].rut) != VB6Helpers.Trim(initObj.MODPREEM.Vx_PReem[largo_reem].RutImp))
            {
                observ = observ + " PARTY " + MODGFYS.Rut_Formateado(initObj.MODPREEM.Vx_PReem[largo_reem].RutImp);
            }

            initObj.MODPREEM.Vx_PReem[largo_reem].observ = initObj.Frm_Rem_PVI.Tx_Observ.Text;
            initObj.MODPREEM.Vx_PReem[largo_reem].ObsDec = " D.I: " + " " + initObj.Frm_Rem_PVI.Tx_NumDec.Text;
            initObj.MODPREEM.Vx_PReem[largo_reem].ObsMer = string.Empty;
            initObj.MODPREEM.Vx_PReem[largo_reem].ObsPar = string.Empty;
            initObj.MODPREEM.Vx_PReem[largo_reem].ObsCob = observ;
            initObj.MODPREEM.Vx_PReem[largo_reem].Estado = 1;

            //Cambiamos el numero de planilla de los intereses.-
            //--------------------------------------------------
            for(i = 1; i <= (short)Largo_Int; i++)
            {
                if(Format.StringToDouble(VB6Helpers.Format(VB6Helpers.CStr(initObj.MODVPLE.IntPla[i].NroPln), "000000")) == 999999)
                {
                    initObj.MODVPLE.IntPla[i].NroPln = initObj.MODPREEM.Vx_PReem[largo_reem].NumPla;
                }

            }

            IndiceFin = 0;

            return largo_reem;
        }

        //Valida el Ingreso de todos los campos.
        private static short Fn_ValiCampos(InitializationObject initObj)
        {
            short _retValue = 0;
            int NroPre = 0;
            string FecRee = string.Empty;
            string s = string.Empty;
            _retValue = (short)(false ? -1 : 0);

            if(initObj.Frm_Rem_PVI.Cb_Pbc.ListIndex == -1)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar el Código de la Plaza.",
                    Title = T_MODANUVI.MsgRemPv,
                    Type = TipoMensaje.Informacion
                });
                return _retValue;
            }

            if(Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_CodPai.Text)) == 0)
            {

                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar el Código del Pais.",
                    Title = T_MODANUVI.MsgRemPv,
                    Type = TipoMensaje.Informacion
                });
                return _retValue;
            }

            if(Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_CodPag.Text)) != 32)
            {
                if(Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_NroCon.Text)) == 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "Debe Ingresar el Número de Conocimiento.",
                        Title = T_MODANUVI.MsgRemPv,
                        Type = TipoMensaje.Informacion
                    });
                    return _retValue;
                }

                if(initObj.Frm_Rem_PVI.Tx_FecCon.Text == string.Empty)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "Debe Ingresar la Fecha para el Número de Conocimiento.",
                        Title = T_MODANUVI.MsgRemPv,
                        Type = TipoMensaje.Informacion
                    });
                    return _retValue;
                }

            }

            if(VB6Helpers.Val(initObj.Frm_Rem_PVI.Tx_CodPag.Text) == 11)
            {
                if(initObj.Frm_Rem_PVI.Tx_FecVen.Text == string.Empty)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "Debe Ingresar la Fecha de Vencimiento de la Operación.",
                        Title = T_MODANUVI.MsgRemPv,
                        Type = TipoMensaje.Informacion
                    });
                    return _retValue;
                }

                if((int)initObj.Frm_Rem_PVI.Ch_CPagos.Value == 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "La forma de pago utilizada requiere que se seleccione el campo Cuadro de Pagos.",
                        Title = T_MODANUVI.MsgRemPv,
                        Type = TipoMensaje.Informacion
                    });
                    return _retValue;
                }

                if(initObj.Frm_Rem_PVI.Tx_NCpago.Text == string.Empty)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "La forma de pago utilizada requiere que se ingrese el número de Cuadro de Pago.",
                        Title = T_MODANUVI.MsgRemPv,
                        Type = TipoMensaje.Informacion
                    });
                    return _retValue;
                }

                if(initObj.Frm_Rem_PVI.Tx_NCuota.Text == string.Empty)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "La forma de pago utilizada requiere que se ingrese el número de la cuota del Cuadro de Pago.",
                        Title = T_MODANUVI.MsgRemPv,
                        Type = TipoMensaje.Informacion
                    });
                    return _retValue;
                }

            }

            //Check de Acuerdo.-
            //------------------
            if((int)initObj.Frm_Rem_PVI.Ch_Acuerdo.Value == -1 && Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_CantAc.Text)) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar la Cantidad de Acuerdos.",
                    Title = T_MODANUVI.MsgRemPv,
                    Type = TipoMensaje.Informacion
                });
                return _retValue;
            }

            if((int)initObj.Frm_Rem_PVI.Ch_Acuerdo.Value == -1 && Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_NumAc1.Text)) == 0 && Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_NumAc2.Text)) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar el Número de Acuerdos.",
                    Title = T_MODANUVI.MsgRemPv,
                    Type = TipoMensaje.Informacion
                });
                return _retValue;
            }

            //Check de Convenio de Crédito.-
            //------------------------------
            if((int)initObj.Frm_Rem_PVI.Ch_ConvCre.Value == -1 && Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_FecDeb.Text)) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar la Fecha de Débito para el Convenio.",
                    Title = T_MODANUVI.MsgRemPv,
                    Type = TipoMensaje.Informacion
                });
                return _retValue;
            }

            if((int)initObj.Frm_Rem_PVI.Ch_ConvCre.Value == -1 && Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_DocChi.Text)) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar el Número de Documento en Chile para el Convenio.",
                    Title = T_MODANUVI.MsgRemPv,
                    Type = TipoMensaje.Informacion
                });
                return _retValue;
            }

            if((int)initObj.Frm_Rem_PVI.Ch_ConvCre.Value == -1 && Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_DocExt.Text)) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar el Número de Documento en el Extranjero para el Convenio.",
                    Title = T_MODANUVI.MsgRemPv,
                    Type = TipoMensaje.Informacion
                });
                return _retValue;
            }

            //Chek de Pagos
            //-------------------------
            if((int)initObj.Frm_Rem_PVI.Ch_CPagos.Value == -1 && Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_NCpago.Text)) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar Número de Cuadro de Pago.",
                    Title = T_MODANUVI.MsgRemPv,
                    Type = TipoMensaje.Informacion
                });
                return _retValue;
            }

            if((int)initObj.Frm_Rem_PVI.Ch_CPagos.Value == -1 && Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_NCuota.Text)) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar Número de Cuotas del Cuadro de Pago.",
                    Title = T_MODANUVI.MsgRemPv,
                    Type = TipoMensaje.Informacion
                });
                return _retValue;
            }

            //Validamos los datos de antecedentes de Planilla reemplazada.-
            //-------------------------------------------------------------
            if(Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_NroPre.Text)) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar el Número de Presentación de la Planilla Reemplazada.",
                    Title = T_MODANUVI.MsgRemPv,
                    Type = TipoMensaje.Informacion
                });
                return _retValue;
            }

            if(Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_FecRee.Text)) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar la Fecha de la Planilla Reemplazada.",
                    Title = T_MODANUVI.MsgRemPv,
                    Type = TipoMensaje.Informacion
                });
                return _retValue;
            }

            NroPre = VB6Helpers.CInt(MODGPYF1.ValTexto(initObj.Frm_Rem_PVI.Tx_NroPre.Text, initObj));
            FecRee = VB6Helpers.Format(initObj.Frm_Rem_PVI.Tx_FecRee.Text, "dd/mm/yyyy");

            if(~MODPREEM.Fn_LeePlAnu(initObj, NroPre, FecRee) != 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Los Antecedentes de Planilla Reemplazada están Incorrectos.",
                    Title = T_MODANUVI.MsgRemPv,
                    Type = TipoMensaje.Informacion
                });
                return _retValue;
            }

            s = initObj.Frm_Rem_PVI.Tx_Observ.Text;
            if(VB6Helpers.Instr(1, s, "@") != 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar los parámetros de las Observaciones.",
                    Title = T_MODANUVI.MsgRemPv,
                    Type = TipoMensaje.Informacion
                });
                return _retValue;
            }

            //Valores de Tipo Cambio y Paridad.-
            //-----------------------------------
            if(Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_TipCam.Text)) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar el Tipo de Cambio.",
                    Title = T_MODANUVI.MsgRemPv,
                    Type = TipoMensaje.Informacion
                });
                return _retValue;
            }

            if(Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_Paridad.Text)) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar la Paridad.",
                    Title = T_MODANUVI.MsgRemPv,
                    Type = TipoMensaje.Informacion
                });
                return _retValue;
            }

            //Validamos los montos ingresados con respecto al disponible.-
            //---------------------------------------------
            if(Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_MtoFob.Text)) > initObj.MODPREEM.VxIdiDec.DisFob_M)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "El Monto Ingresado Sobrepasa el FOB Disponible de la Declaración que es " + initObj.MODPREEM.NemMoneda + " " + Format.FormatCurrency((initObj.MODPREEM.VxIdiDec.DisFob_M), initObj.MODANUVI.FtoSal),
                    Title = T_MODANUVI.MsgRemPv,
                    Type = TipoMensaje.Informacion
                });
                return _retValue;
            }

            initObj.Frm_Rem_PVI.Tx_MtoFle.Text = VB6Helpers.Format(initObj.Frm_Rem_PVI.Tx_MtoFle.Text, initObj.MODANUVI.FtoSal);
            if(Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_MtoFle.Text)) > initObj.MODPREEM.VxIdiDec.DisFle_M)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "El Monto Ingresado Sobrepasa el Flete Disponible de la Declaracion que es " + initObj.MODPREEM.NemMoneda + " " + Format.FormatCurrency((initObj.MODPREEM.VxIdiDec.DisFle_M), initObj.MODANUVI.FtoSal),
                    Title = T_MODANUVI.MsgRemPv,
                    Type = TipoMensaje.Informacion
                });
                initObj.Frm_Rem_PVI.Tx_MtoFle.Text = string.Empty;
                return _retValue;
            }

            initObj.Frm_Rem_PVI.Tx_MtoSeg.Text = VB6Helpers.Format(initObj.Frm_Rem_PVI.Tx_MtoSeg.Text, initObj.MODANUVI.FtoSal);
            if(Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_MtoSeg.Text)) > initObj.MODPREEM.VxIdiDec.DisSeg_M)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "El Monto Ingresado Sobrepasa el Seguro Disponible de la Declaración que es " + initObj.MODPREEM.NemMoneda + " " + Format.FormatCurrency((initObj.MODPREEM.VxIdiDec.DisSeg_M), initObj.MODANUVI.FtoSal),
                    Title = T_MODANUVI.MsgRemPv,
                    Type = TipoMensaje.Informacion
                });
                initObj.Frm_Rem_PVI.Tx_MtoSeg.Text = string.Empty;
                return _retValue;
            }

            //---------------------------------------------

            return (short)(true ? -1 : 0);
        }

        private static short Fn_ValIdiDec(InitializationObject initObj)
        {
            short _retValue = 0;

            _retValue = (short)(false ? -1 : 0);

            if(initObj.Frm_Rem_PVI.Tx_CodPag.Text == string.Empty)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar el Código de Forma de Pago.",
                    Title = T_MODANUVI.MsgRemPv,
                    Type = TipoMensaje.Informacion
                });
                return _retValue;
            }

            return (short)(true ? -1 : 0);
        }

        //Suma el monto ingresado en la lista
        private static double Fn_ValIntFin(InitializationObject initObj, int Plani)
        {
            short largo = 0;
            short i = 0;
            double Interes = 0;
            
            largo = (short)VB6Helpers.UBound(initObj.MODVPLE.IntPla);

            Interes = 0;
            for(i = 1; i <= (short)largo; i++)
            {
                if(initObj.MODVPLE.IntPla[i].FlgEli != -1 && VB6Helpers.Format(VB6Helpers.CStr(initObj.MODVPLE.IntPla[i].NroPln), "000000") == VB6Helpers.Format(VB6Helpers.CStr(Plani), "000000"))
                {
                    Interes += initObj.MODVPLE.IntPla[i].MtoInt;
                }

            }

            return Interes;
        }

        //Limpia frame que contiene datos del Idi y la declaracion.-
        private static void Pr_LimIdiDec(InitializationObject initObj)
        {
            initObj.Frm_Rem_PVI.Tx_CodPag.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_NumDec.Text = "________________-_";
            initObj.Frm_Rem_PVI.Tx_FecDec.Text = string.Empty;
        }

        private static void Pr_LimOtros(InitializationObject initObj)
        {

            //Limpia Textos
            //--------------
            initObj.Frm_Rem_PVI.Tx_CodPai.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_NroCon.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_FecCon.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_FecVen.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_NumAc1.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_NumAc2.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_FecDeb.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_DocChi.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_DocExt.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_CantAc.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_NumAc1.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_NumAc2.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_NCpago.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_NCuota.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_NroPre.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_FecRee.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_MtoFob.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_MtoFle.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_MtoSeg.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_IntPla.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_GasBan.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_Observ.Text = string.Empty;

            //Limpia las Check.-
            //------------------
            initObj.Frm_Rem_PVI.Ch_Acuerdo.Value = 0;
            initObj.Frm_Rem_PVI.Ch_ConvCre.Value = 0;
            initObj.Frm_Rem_PVI.Ch_ConvCre.Value = 0;
            initObj.Frm_Rem_PVI.Ch_ConvCre.Value = 0;
            initObj.Frm_Rem_PVI.Ch_CPagos.Value = 0;
            initObj.Frm_Rem_PVI.Ch_CPagos.Value = 0;

            //Limpiamos los paneles.-
            //------------------------
            initObj.Frm_Rem_PVI.Pn_NroPre.Text = string.Empty;
            initObj.Frm_Rem_PVI.Pn_Import.Text = string.Empty;
            initObj.Frm_Rem_PVI.Pn_RutImp.Text = string.Empty;
            initObj.Frm_Rem_PVI.Pn_PPago.Text = string.Empty;
            initObj.Frm_Rem_PVI.Pn_ValCif.Text = string.Empty;
            initObj.Frm_Rem_PVI.Pn_MtoTot.Text = string.Empty;
            initObj.Frm_Rem_PVI.Pn_CifDol.Text = string.Empty;
            initObj.Frm_Rem_PVI.Pn_TotDol.Text = string.Empty;

            //Combos.-
            //--------------------
            initObj.Frm_Rem_PVI.Cb_Pbc.ListIndex = -1;

            Flag_TipCam = 0;
        }

        private static void Pr_LimParTCMon(InitializationObject initObj)
        {

            initObj.Frm_Rem_PVI.Tx_TipCam.Text = string.Empty;
            initObj.Frm_Rem_PVI.Tx_Paridad.Text = string.Empty;
            initObj.Frm_Rem_PVI.Pn_TCDol.Text = string.Empty;
            initObj.Frm_Rem_PVI.Lb_NemTC.Text = string.Empty;
            initObj.Frm_Rem_PVI.Pn_CodMon.Text = string.Empty;
            initObj.Frm_Rem_PVI.Pn_Moneda.Text = string.Empty;
        }

        private static void Enabled_Acuerdos(InitializationObject initObj, bool valor)
        {
            initObj.Frm_Rem_PVI.Tx_CantAc.Enabled = valor;
            initObj.Frm_Rem_PVI.Tx_CantAc.Enabled = valor;
            initObj.Frm_Rem_PVI.Tx_NumAc1.Enabled = valor;
            initObj.Frm_Rem_PVI.Tx_NumAc2.Enabled = valor;
        }
        
        private static void Enabled_ConvCre(InitializationObject initObj, bool valor)
        {
            initObj.Frm_Rem_PVI.Tx_FecDeb.Enabled = valor;
            initObj.Frm_Rem_PVI.Tx_DocChi.Enabled = valor;
            initObj.Frm_Rem_PVI.Tx_DocExt.Enabled = valor;
        }

        private static void Enabled_Cpagos(InitializationObject initObj, bool valor)
        {
            initObj.Frm_Rem_PVI.Tx_NCpago.Enabled = valor;
            initObj.Frm_Rem_PVI.Tx_NCuota.Enabled = valor;
        }

        //Muestra los datos en caso de haber seleccionado una sola planilla en la
        //ventana de anulacion.-
        public static void Pr_CargaPln(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short n = 0;
            short x = 0;
            short CodPais = 0;
            short PosPais = 0;
            string p = string.Empty;
            string s = string.Empty;
            double Paridad = 0;
            
            n = (short)VB6Helpers.UBound(initObj.MODANUVI.V_PlAnu);
            x = (short)VB6Helpers.UBound(initObj.MODANUVI.V_Plani);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            //Nombre Importador
            //------------------
            initObj.Frm_Rem_PVI.Pn_Import.Text = MODGPYF1.Minuscula(initObj.MODANUVI.V_PlAnu[n].NomImp);

            //Rut Importador
            //------------------
            initObj.Frm_Rem_PVI.Pn_RutImp.Text = MODGFYS.Rut_Formateado(initObj.MODANUVI.V_PlAnu[n].RutImp);

            //Pais de Pago.-
            //------------------
            initObj.Frm_Rem_PVI.Tx_CodPai.Text = VB6Helpers.CStr(initObj.MODPREEM.VxIdiDec.PaiAdq);
            CodPais = VB6Helpers.CShort(MODGPYF1.ValTexto(initObj.Frm_Rem_PVI.Tx_CodPai.Text, initObj));
            PosPais = MODGTAB0.Get_VPai(initObj, uow, CodPais);
            initObj.Frm_Rem_PVI.Pn_PPago.Text = initObj.MODGTAB0.VPai[PosPais].Pai_PaiNom;

            if(n == 0 && FlgYaMostro == 0)
            {
                //Numero de Planilla
                //------------------
                initObj.Frm_Rem_PVI.Tx_NroPre.Text = VB6Helpers.Format(VB6Helpers.CStr(initObj.MODANUVI.V_PlAnu[n].NumPre), "000000");
                //Fecha de Vencimiento de planilla reemplazada
                //------------------
                initObj.Frm_Rem_PVI.Tx_FecRee.Text = VB6Helpers.Format(initObj.MODANUVI.V_PlAnu[n].FecVen, "dd/MM/yyyy");

                //If ValTexto(Tx_NroPre) <> 0 And Trim$(Tx_FecRee.Text) <> string.Empty Then
                //    Tx_Observ.Text = "Se complementa con planilla anulada " + Format(Tx_NroPre, "000000") + " con fecha " + Format(Tx_FecRee, "dd/mm/yyyy")
                //End If

                //Conocimiento de embarque
                //------------------
                initObj.Frm_Rem_PVI.Tx_NroCon.Text = initObj.MODANUVI.V_PlAnu[n].NumCon;
                initObj.Frm_Rem_PVI.Tx_FecCon.Text = VB6Helpers.Format(initObj.MODANUVI.V_PlAnu[n].FecCon, "dd/MM/yyyy");
                //Plaza Banco Central
                //------------------
                //Tx_CodPlz.Text = V_PlAnu(n%).CodPla
                //Tx_CodPlz.Text = VGen.CodPbc
                initObj.Frm_Rem_PVI.Cb_Pbc.ListIndex = MODGPYF0.PosLista(initObj.Frm_Rem_PVI.Cb_Pbc, initObj.MODGSCE.VGen.CodPbc);
                //Pn_NomPlz.Text = V_PlAnu(n%).NomPla
                //Pais de Pago
                //------------------
                initObj.Frm_Rem_PVI.Tx_CodPai.Text = VB6Helpers.CStr(initObj.MODANUVI.V_PlAnu[n].CodPPa);
                initObj.Frm_Rem_PVI.Pn_PPago.Text = initObj.MODANUVI.V_PlAnu[n].NomPai;
                //Moneda
                //------------------
                initObj.Frm_Rem_PVI.Pn_Moneda.Text = VB6Helpers.UCase(initObj.MODANUVI.V_PlAnu[n].NomMon);

                //Montos
                //------------------
                initObj.Frm_Rem_PVI.Tx_MtoFob.Text = Format.FormatCurrency((initObj.MODANUVI.V_PlAnu[n].MtoFob), initObj.MODANUVI.FtoSal);
                initObj.Frm_Rem_PVI.Tx_MtoFle.Text = Format.FormatCurrency((initObj.MODANUVI.V_PlAnu[n].MtoFle), initObj.MODANUVI.FtoSal);
                initObj.Frm_Rem_PVI.Tx_MtoSeg.Text = Format.FormatCurrency((initObj.MODANUVI.V_PlAnu[n].MtoSeg), initObj.MODANUVI.FtoSal);
                initObj.Frm_Rem_PVI.Pn_ValCif.Text = Format.FormatCurrency((initObj.MODANUVI.V_PlAnu[n].MtoCif), initObj.MODANUVI.FtoSal);
                initObj.Frm_Rem_PVI.Tx_IntPla.Text = Format.FormatCurrency((initObj.MODANUVI.V_PlAnu[n].MtoInt), initObj.MODANUVI.FtoSal);
                initObj.Frm_Rem_PVI.Tx_GasBan.Text = Format.FormatCurrency((initObj.MODANUVI.V_PlAnu[n].MtoGas), initObj.MODANUVI.FtoSal);
                initObj.Frm_Rem_PVI.Pn_MtoTot.Text = Format.FormatCurrency((initObj.MODANUVI.V_PlAnu[n].MtoTot), initObj.MODANUVI.FtoSal);

                //Paridad
                //------------------
                if(DateTime.Parse(initObj.MODANUVI.V_PlAnu[n].FecVen).Month != DateTime.Now.Month)
                {
                    Paridad = MODGTAB1.SyGet_Vmc(initObj.MODGTAB0, uow, initObj.MODANUVI.Var_CodMon, DateTime.Now.ToString("dd/MM/yyyy"), "P");
                    initObj.Frm_Rem_PVI.Pn_CifDol.Text = Format.FormatCurrency((Format.StringToDouble((initObj.Frm_Rem_PVI.Pn_ValCif.Text)) / Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_Paridad.Text))), T_MODGCON0.FormatoConDec);  //PACP
                    initObj.Frm_Rem_PVI.Pn_TotDol.Text = Format.FormatCurrency(Format.StringToDouble((initObj.Frm_Rem_PVI.Pn_MtoTot.Text)) / Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_Paridad.Text)), T_MODGCON0.FormatoConDec);  //PACP
                    initObj.Frm_Rem_PVI.Tx_Paridad.Text = MODGSYB.dbPardSyForRead(Paridad);
                }
                else
                {
                    initObj.Frm_Rem_PVI.Tx_Paridad.Text = MODGSYB.dbPardSyForRead(initObj.MODANUVI.V_PlAnu[n].ParAnu);
                    initObj.Frm_Rem_PVI.Pn_CifDol.Text = Format.FormatCurrency((initObj.MODANUVI.V_PlAnu[n].CifDol), T_MODGCON0.FormatoConDec);  //PACP
                    initObj.Frm_Rem_PVI.Pn_TotDol.Text = Format.FormatCurrency((initObj.MODANUVI.V_PlAnu[n].TotDol), T_MODGCON0.FormatoConDec);  //PACP
                }

                //Tx_Paridad.Text = V_PlAnu(n%).ParAnu
                //Tx_Paridad.Text = Paridad#
                if(initObj.MODANUVI.V_PlAnu[n].ParAnu != 0)
                {
                    initObj.Frm_Rem_PVI.Tx_Paridad.Enabled = false;
                }

                //Fecha de Vencimiento
                //------------------
                initObj.Frm_Rem_PVI.Tx_FecVen.Text = DateTime.Parse(initObj.MODANUVI.V_PlAnu[n].FecVto).ToString("dd/MM/yyyy");
                //Cuadro de pagos
                //------------------
                if(initObj.MODANUVI.V_PlAnu[n].HayCua == -1)
                {
                    initObj.Frm_Rem_PVI.Ch_CPagos.Value = (short)(true ? -1 : 0);
                    initObj.Frm_Rem_PVI.Tx_NCpago.Text = VB6Helpers.CStr(initObj.MODANUVI.V_PlAnu[n].NumCua);
                    initObj.Frm_Rem_PVI.Tx_NCuota.Text = VB6Helpers.CStr(initObj.MODANUVI.V_PlAnu[n].numcuo);
                }

                //Acuerdo
                //------------------
                if(initObj.MODANUVI.V_PlAnu[n].HayAcu == -1)
                {
                    initObj.Frm_Rem_PVI.Ch_Acuerdo.Value = (short)(true ? -1 : 0);
                    initObj.Frm_Rem_PVI.Tx_CantAc.Text = VB6Helpers.CStr(initObj.MODANUVI.V_PlAnu[n].NumAcu);
                    initObj.Frm_Rem_PVI.Tx_NumAc1.Text = initObj.MODANUVI.V_PlAnu[n].Acuer1;
                    initObj.Frm_Rem_PVI.Tx_NumAc2.Text = initObj.MODANUVI.V_PlAnu[n].Acuer2;
                }

                //Convenio Credito
                //------------------
                if(initObj.MODANUVI.V_PlAnu[n].FecDeb != string.Empty && initObj.MODANUVI.V_PlAnu[n].DocChi != string.Empty && initObj.MODANUVI.V_PlAnu[n].DocExt != string.Empty)
                {
                    initObj.Frm_Rem_PVI.Ch_ConvCre.Value = (short)(true ? -1 : 0);
                    initObj.Frm_Rem_PVI.Tx_FecDeb.Text = DateTime.Parse(initObj.MODANUVI.V_PlAnu[n].FecDeb).ToString("dd/MM/yyyy");
                    initObj.Frm_Rem_PVI.Tx_DocChi.Text = initObj.MODANUVI.V_PlAnu[n].DocChi;
                    initObj.Frm_Rem_PVI.Tx_DocExt.Text = initObj.MODANUVI.V_PlAnu[n].DocExt;
                }

                //Cargamos los intereses.-
                //------------------
                Pr_TraeIntPln(initObj);
                //Flag
                //------------------
                FlgYaMostro = (short)(true ? -1 : 0);
            }

            if(x == 0)
            {
                //Numero de Planilla
                //------------------
                initObj.Frm_Rem_PVI.Tx_NroPre.Text = VB6Helpers.Format(VB6Helpers.CStr(initObj.MODANUVI.V_PlAnu[n].NumPre), "000000");
                //Fecha de Vencimiento de planilla reemplazada
                //------------------
                initObj.Frm_Rem_PVI.Tx_FecRee.Text = DateTime.Parse(initObj.MODANUVI.V_PlAnu[n].FecVen).ToString("dd/MM/yyyy");
            }

            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'MODGPYF1.ValTexto(Tx_NroPre)'. Consider using the GetDefaultMember6 helper method.
            if(Format.StringToDouble((initObj.Frm_Rem_PVI.Tx_NroPre.Text)) != 0 && VB6Helpers.Trim(initObj.Frm_Rem_PVI.Tx_FecRee.Text) != string.Empty)
            {
                p = VB6Helpers.Format(initObj.Frm_Rem_PVI.Tx_NroPre.Text, "000000") + "~" + DateTime.Parse(initObj.Frm_Rem_PVI.Tx_FecRee.Text).ToString("dd/MM/yyyy");
                s = Mdl_Funciones.SyGet_Fra(initObj, uow, 92, "E", p);
                initObj.Frm_Rem_PVI.Tx_Observ.Text = s;
            }

        }

        private static void Pr_CargaLis(InitializationObject initObj)
        {
            short largo_reem = 0;
            short i = 0;
            string ListaRee = string.Empty;
            largo_reem = (short)VB6Helpers.UBound(initObj.MODPREEM.Vx_PReem);

            //Cargamos en la lista numero de planilla y monto.-
            //-------------------------------------------------
            initObj.Frm_Rem_PVI.Lt_Final.Items.Clear();
            for(i = 1; i <= (short)largo_reem; i++)
            {
                if(initObj.MODPREEM.Vx_PReem[i].Estado == 1)
                {
                    ListaRee = string.Empty;
                    ListaRee = ListaRee + MODGPYF0.forma(initObj.MODPREEM.Vx_PReem[i].NumPla, "000000") + VB6Helpers.Chr(9);
                    ListaRee = ListaRee + initObj.MODPREEM.NemMoneda + " " + MODGPYF0.forma(VB6Helpers.Str(initObj.MODPREEM.Vx_PReem[i].TotOri), T_MODGCON0.FormatoConDec);
                    initObj.Frm_Rem_PVI.Lt_Final.Items.Add(new UI_ListBoxItem() { Value = ListaRee });

                }

            }

        }

        private static void Pr_TraeIntPln(InitializationObject initObj)
        {
            short Int_Anu = 0;
            short k = 0;
            short ind = 0;

            Int_Anu = (short)VB6Helpers.UBound(initObj.MODVPLE.IntAnu);
            initObj.MODVPLE.IntPla = new T_IntPla[1];

            //Transferimos los valores de intereses de anulacion a los de reemplazo.-
            for(k = 1; k <= (short)Int_Anu; k++)
            {
                if(VB6Helpers.Format(VB6Helpers.CStr(initObj.MODVPLE.IntAnu[k].MtoInt), T_MODGPYF1.Fto_Comparar) != VB6Helpers.Format("0", T_MODGPYF1.Fto_Comparar))
                {
                    
                    ind = (short)VB6Helpers.UBound(initObj.MODVPLE.IntPla);
                    VB6Helpers.RedimPreserve(ref initObj.MODVPLE.IntPla, 0, ind + 1);
                    initObj.MODVPLE.IntPla[ind + 1] = initObj.MODVPLE.IntAnu[k];
                }

            }

        }

        //Deshabilita o Habilita Frames,Botones,Textos y Check de acuerdo al valor
        //pasado por parametro.-
        private static void Pr_Habilita(InitializationObject initObj, short Valor)
        {

            initObj.Frm_Rem_PVI.Ch_ConvCre.Enabled = Valor != 0;
            initObj.Frm_Rem_PVI.Ch_Acuerdo.Enabled = Valor != 0;
            initObj.Frm_Rem_PVI.Ch_CPagos.Enabled = Valor != 0;


            //Fr_Presen.Enabled = Valor != 0;
            //Fr_Montos.Enabled = Valor != 0;
            //Fr_PlanRee.Enabled = Valor != 0;
            //Fr_Conoc.Enabled = Valor != 0;

            //Fr_Final.Enabled = Valor != 0;
            //Fr_Montos.Enabled = Valor != 0;

            initObj.Frm_Rem_PVI.Tx_Observ.Enabled = Valor != 0;

        }

        private static void Pr_Inicializa(InitializationObject initObj)
        {

            //Limpia el frame de ingreso de Idi y Dec.-
            //-----------------------------------------
            Pr_LimIdiDec(initObj);

            //Limpiamos los campos.-
            //----------------------------
            Pr_LimOtros(initObj);
            Pr_LimParTCMon(initObj);

            //Deshabilita Frames, menos el de ingreso de informe
            //--------------------------------------------------
            Pr_Habilita(initObj, 0);

            //Limpiamos variables y estructuras.-
            //------------------------------------
            //Vx_DatReem = Vx_DatReemNull
            initObj.MODPREEM.VxIdiDec = initObj.MODPREEM.VxIdiDecNull;

            VB6Helpers.Erase(ref initObj.MODGFYS.RIdiIni);
            VB6Helpers.Erase(ref initObj.MODCVDIMMM.RDecIni);

            initObj.MODPREEM.Vx_PReem = new Plan_Reemp[1] { new Plan_Reemp() };
            initObj.MODGFYS.RIdiFin = new R_Idi[1] { new R_Idi() };
            initObj.MODCVDIMMM.RDecFin = new r_dec[1] { new r_dec() };
            initObj.MODCVDIMMM.AnuCob = new ParaAnuCob[1] { new ParaAnuCob() };

            IndiceFin = 0;
            IndiceInt = 0;
            MtoInteres = 0;
            FlgYaMostro = (short)(false ? -1 : 0);
            IngPlan = (short)(false ? -1 : 0);
            initObj.MODPREEM.NuevaPlan = (short)(false ? -1 : 0);
        }

        private static void Pr_GetMoneda(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short Pos_Cod = MODGTAB0.Get_VMnd(initObj.MODGTAB0, uow, initObj.MODANUVI.Var_CodMon);
            string FtoEnt = "";
            string Moneda = "";
            //Obtenemos la posicion para ese codigo.-
            //---------------------------------------

            //Asignamos el tag correspondiente segun la moneda.-
            //---------------------------------------
            if(initObj.MODGTAB0.VMnd[Pos_Cod].Mnd_MndSin != 0)
            {
                initObj.Frm_Rem_PVI.Tx_MtoFob.Tag = "_____________";
                initObj.Frm_Rem_PVI.Tx_MtoFle.Tag = "_____________";
                initObj.Frm_Rem_PVI.Tx_MtoSeg.Tag = "_____________";
                initObj.Frm_Rem_PVI.Tx_IntPla.Tag = "_____________";
                initObj.Frm_Rem_PVI.Tx_GasBan.Tag = "_____________";
                FtoEnt = "0";
                initObj.MODANUVI.FtoSal = T_MODGCON0.FormatoSinDec;
            }
            else
            {
                initObj.Frm_Rem_PVI.Tx_MtoFob.Tag = "_____________.__";
                initObj.Frm_Rem_PVI.Tx_MtoFle.Tag = "_____________.__";
                initObj.Frm_Rem_PVI.Tx_MtoSeg.Tag = "_____________.__";
                initObj.Frm_Rem_PVI.Tx_IntPla.Tag = "_____________.__";
                initObj.Frm_Rem_PVI.Tx_GasBan.Tag = "_____________.__";
                FtoEnt = "0.00";
                initObj.MODANUVI.FtoSal = T_MODGCON0.FormatoConDec;
            }

            //Desplegamos el codigo de la Moneda Banco Central.-
            //---------------------------------------
            if(Pos_Cod != 0)
            {
                initObj.Frm_Rem_PVI.Pn_CodMon.Text = VB6Helpers.CStr(initObj.MODGTAB0.VMnd[Pos_Cod].Mnd_MndCbc);
            }

            //Desplegamos la Moneda.-
            //---------------------------------------
            Moneda = MODGTAB0.Get_NomMnd(initObj, initObj.MODANUVI.Var_CodMon);
            initObj.Frm_Rem_PVI.Pn_Moneda.Text = Moneda;
            initObj.MODPREEM.NemMoneda = MODGTAB0.Get_NemMnd(initObj.MODGTAB0, uow, initObj.MODANUVI.Var_CodMon);
        }

        private static void Pr_ParyTipCa(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            double Paridad = MODGTAB1.SyGet_Vmc(initObj.MODGTAB0, uow, initObj.MODANUVI.Var_CodMon, DateTime.Now.ToString("dd/MM/yyyy"), "P");
            short x = 0;

            //Paridad.-
            //-----------------
            initObj.Frm_Rem_PVI.Tx_Paridad.Text = Paridad.ToString("##0.000#");
            if(Paridad == 0D)
            {
                //Si no hay paridad, se habilita
                initObj.Frm_Rem_PVI.Tx_Paridad.Enabled = true;  //el campo para su ingreso.-
            }

            //Tipo de cambio.-
            //-----------------
            x = MODGMTA.Get_VPar(initObj.MODGMTA, initObj.MODGTAB0, initObj.Frm_Ingreso_Valores, uow, DateTime.Now.ToString("yyyy-MM-dd"), initObj.MODANUVI.Var_CodMon);
            initObj.Frm_Rem_PVI.Tx_TipCam.Text = Format.FormatCurrency((initObj.MODANUVI.Var_TipCam), "##0.000#");  //PACP
            initObj.Frm_Rem_PVI.Lb_NemTC.Text = initObj.MODPREEM.NemMoneda;

            //Tipo de cambio equivalente en dolar.-
            //-----------------
            if(initObj.MODANUVI.Var_TipCam != 0 && Paridad != 0)
            {
                initObj.Frm_Rem_PVI.Pn_TCDol.Text = Format.FormatCurrency(initObj.MODANUVI.Var_TipCam * Paridad, "###,##0.0000");
            }
            else
            {
                initObj.Frm_Rem_PVI.Pn_TCDol.Text = Format.FormatCurrency(initObj.MODGTAB0.VVmd.VmdMbv * Paridad, "###,##0.0000");
            }

        }

        //Carga la declaracion y el Idi en caso de que se haya seleccionado una.-
        //planilla en la pantalla de anulacion.-
        private static void Pr_CargaDecId(InitializationObject initObj)
        {
            short n = 0;

            n = (short)VB6Helpers.UBound(initObj.MODANUVI.V_PlAnu);

            if(n == 0)
            {
                if(initObj.MODANUVI.V_PlAnu[n].NumIdi != 0 && !string.IsNullOrEmpty(initObj.MODANUVI.V_PlAnu[n].numdec))
                {
                    if(VB6Helpers.Len(initObj.MODANUVI.V_PlAnu[n].numdec) < 18 || VB6Helpers.Mid(initObj.MODANUVI.V_PlAnu[n].numdec, 17, 1) != "-")
                    {

                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "El formato del Número de Declaración es incorrecto. Deberá ingresarla en forma manual.",
                            Title = T_MODANUVI.MsgRemPv,
                            Type = TipoMensaje.Informacion
                        });
                    }
                    else
                    {
                        initObj.Frm_Rem_PVI.Tx_NumDec.Text = initObj.MODANUVI.V_PlAnu[n].numdec;
                    }

                    initObj.Frm_Rem_PVI.Tx_FecDec.Text = DateTime.Parse(initObj.MODANUVI.V_PlAnu[n].FecDec).ToString("dd/MM/yyyy");
                }

                initObj.Frm_Rem_PVI.Tx_CodPag.Text = VB6Helpers.Format(VB6Helpers.CStr(initObj.MODANUVI.V_PlAnu[n].CodPag), "00");
            }

        }


    }
}
