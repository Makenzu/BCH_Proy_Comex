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
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public static class FrmxPln0
    {
        
        public static void Form_Load(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short[] Tabs = null;
            short a = 0;
            short x = 0;
            short i = 0;
            short b = 0;
            short e = 0;

            initObj.FrmxPln0.Titulo = "Planillas                      # Dec.           IDE        Mnd                           Monto";

            //valores iniciales de los controles
            initObj.FrmxPln0.Tx_MtoPln[0].Enabled = false;
            initObj.FrmxPln0.Tx_MtoPln[1].Enabled = false;
            initObj.FrmxPln0.Tx_MtoPln[2].Enabled = false;
            initObj.FrmxPln0.Tx_MtoPln[3].Enabled = false;
            initObj.FrmxPln0.Tx_MtoPln[4].Enabled = false;
            initObj.FrmxPln0.Tx_MtoDec[5].Enabled = false;
            initObj.FrmxPln0.Tx_MtoDec[6].Enabled = false;
            initObj.FrmxPln0.Tx_MtoDec[8].Enabled = false;
            initObj.FrmxPln0.Tx_FecVen.Enabled = false;
            initObj.FrmxPln0.Boton[0].Enabled = false;
            initObj.FrmxPln0.Boton[1].Enabled = false;

            Tabs = new short[2];
            Tabs[0] = 20;
            Tabs[1] = 40;
            a = MODGPYF0.seteatabulador(initObj.FrmxPln0.LtMto, Tabs);

            Tabs = new short[6];
            Tabs[0] = 30;
            Tabs[1] = 60;
            Tabs[2] = 95;
            Tabs[3] = 140;
            Tabs[4] = 159;
            a = MODGPYF0.seteatabulador(initObj.FrmxPln0.LtPln, Tabs);

            //Carga los sectores económicos del beneficiarios
            x = VB6Helpers.CShort(MODGTAB0.SyGetSecEc(initObj.MODGTAB0, uow));
            for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODGTAB0.SecEc); i++)
            {
                initObj.FrmxPln0.Cb_SecEcBen.Items.Add(new UI_ComboItem{ 
                    Value = initObj.MODGTAB0.SecEc[i].NomSec, 
                    Data = initObj.MODGTAB0.SecEc[i].CodSec
                });
            }

            //Carga los sectores económicos del Inversionista
            for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODGTAB0.SecEc); i++)
            {
                initObj.FrmxPln0.Cb_SecEcIn.Items.Add(new UI_ComboItem
                {
                    Value = initObj.MODGTAB0.SecEc[i].NomSec,
                    Data = initObj.MODGTAB0.SecEc[i].CodSec
                });
            }

            //Carga las Aduanas.
            a = MODGTAB1.SyGetn_Adn(initObj.MODGTAB1, uow);

            //Carga los Bancos.
            b = MODGTAB0.SyGetn_Bco(initObj, uow);
            if (b != 0)
            {
                Pr_Cargar_Bancos(initObj);
            }

            //Carga las Plazas de Banco Central.
            e = MODGTAB1.SyGetn_Pbc(initObj.MODGTAB1, uow);
            if (e != 0)
            {
                Pr_Cargar_Pbc(initObj);
            }

            Pr_Cargar_LtMto(initObj);
            Pr_Cargar_Tipo_Planilla(initObj);

            //Carga Lista de Paises
            MODGTAB0.CargaEnLista_Pai(initObj.MODGTAB0, initObj.FrmxPln0.Cb_Pais, uow);

            //initObj.MODXPLN1.VxPlvs = new T_xPlv[1] { new T_xPlv() };  //Arreglo de Plns.
            //initObj.MODXPLN0.VxDecP = new T_xDecP[1] { new T_xDecP() } ;  //Arreglo de Decs.

            //Siempre se posiciona en el 1º monto.
            if (initObj.FrmxPln0.LtMto.Items.Count > 0)
            {
                if (initObj.FrmxPln0.LtMto.ListIndex != 0)
                {
                    initObj.FrmxPln0.LtMto.ListIndex = 0;
                    FrmxPln0.LtMto_Click(initObj);
                }
            }

            initObj.MODXPLN0.VxDatP.Acepto = (short)(false ? -1 : 0);

            if (initObj.FrmxPln0.LtTPln.Items.Count > 0 &&
                VB6Helpers.Instr(T_MODXPLN1.PLNCDEC, initObj.FrmxPln0.LtTPln.get_List((short)initObj.FrmxPln0.LtTPln.ListIndex)) != 0)
            {
                initObj.FrmxPln0.Tx_NumDec.Enabled = true;
                initObj.FrmxPln0.Tx_FecDec.Enabled = true;
                initObj.FrmxPln0.Tx_CodAdn.Enabled = true;
                initObj.FrmxPln0.Ok_Dec.Enabled = true;
            }

            if (initObj.FrmxPln0.LtTPln.Items.Count > 0 && 
                VB6Helpers.Instr(T_MODXPLN1.PLNSDEC, initObj.FrmxPln0.LtTPln.get_List((short)initObj.FrmxPln0.LtTPln.ListIndex)) != 0)
            {
                initObj.FrmxPln0.Tx_NumDec.Enabled = false;
                initObj.FrmxPln0.Tx_FecDec.Enabled = false;
                initObj.FrmxPln0.Tx_CodAdn.Enabled = false;
                initObj.FrmxPln0.Ok_Dec.Enabled = false;
            }

            initObj.FrmxPln0.Tx_Obs.Text = "Ope. " + initObj.MODGCVD.VgCvd.OpeSin;
            if(!string.IsNullOrEmpty(initObj.MODGASO.VgAso.OpeSin))
            {
                initObj.FrmxPln0.Tx_Obs.Text = initObj.FrmxPln0.Tx_Obs.Text + " Ope. Rel. " + initObj.MODGASO.VgAso.OpeSin;
            }

            initObj.FrmxPln0.guardaobs = initObj.FrmxPln0.Tx_Obs.Text;
        }

        public static bool Boton_Click(int Index, InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            double bTots = 0;
            double Tots = 0;
            short a = 0;
            short k = 0;

            initObj.Mdl_Funciones_Varias.VaxPlv.fecins = initObj.FrmxPln0.Tx_Fecha.Text ?? string.Empty;
            initObj.Mdl_Funciones_Varias.VaxPlv.NumCre = VB6Helpers.Val(initObj.FrmxPln0.Tx_NumIns.Text);
            if (initObj.FrmxPln0.Cb_Pais.ListIndex != -1)
            {
                initObj.Mdl_Funciones_Varias.VaxPlv.codpai = (short)initObj.FrmxPln0.Cb_Pais.get_ItemData_(initObj.FrmxPln0.Cb_Pais.ListIndex);
            }

            switch (Index)
            {
                case 0:  //Aceptar.

                    //Verifica Planillas OK.-
                    if (~PlanillasOK(initObj) != 0)
                    {
                        return false;
                    }

                    bTots = initObj.MODXPLN0.VxDatP.MtoLiqs + initObj.MODXPLN0.VxDatP.MtoInfs + initObj.MODXPLN0.VxDatP.MtoTrans;
                    if (Tots > 0)
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "Los montos NO han sido correctamente justificados.",
                            Title = T_MODXPLN1.MsgxPlv,
                            Type = TipoMensaje.Informacion
                        });
                        
                        //LtPln.SetFocus();
                        return false;
                    }

                    if (~MODXPLN0.RPut_xDec(initObj, uow, initObj.MODXPLN0.VxDatP.CodMnd) != 0)
                    {
                        return false;
                    }

                    if (Mdl_Funciones_Varias.vaAIngresoValores(initObj, uow, "PLANVIS_Finish"))
                    {
                        return false;
                    }

                    initObj.MODXPLN0.VxDatP.Acepto = (short)(true ? -1 : 0);
                    Pr_Comis_xPlv(initObj, uow);

                    //VB6Helpers.Unload(this);
                    initObj.FormularioQueAbrir = "Index";

                    break;
                case 1:  //Visualizar.
                    a = (short)initObj.FrmxPln0.LtPln.get_ItemData(initObj.FrmxPln0.LtPln.ListIndex);
                    k = initObj.MODXPLN1.VPln[a].Indice;

                    if (a >= 0)
                    {
                        //Visializa las Planillas Visibles.
                        if ((VB6Helpers.Trim(initObj.MODXPLN1.VPln[a].TipPln) == "V") &&
                            (initObj.MODXPLN1.VPln[a].Status != T_MODGCVD.EstadoEli))
                        {
                            initObj.MODXPLN1.IndPlv = k;
                           
                            initObj.FormularioQueAbrir = "PlanillaIngresoVisibleExport";
                            initObj.VieneDe = "PlanillaVisibleExport";
                        }

                        //Visualiza las Planillas Invisibles.
                        if ((VB6Helpers.Trim(initObj.MODXPLN1.VPln[a].TipPln) == "I") && 
                            (initObj.MODXPLN1.VPln[a].Status != T_MODGCVD.EstadoEli))
                        {
                            initObj.MODGPLI1.IndPli = k;
                            initObj.FormularioQueAbrir = "PlanillaInvisibleEditar";
                            initObj.VieneDe = "PlanillaVisibleExport";
                        }

                    }

                    break;
                case 2:  //Cancelar.
                    initObj.MODXPLN1.VxPlvs = new T_xPlv[0];
                    initObj.Mdl_Funciones_Varias.VxDec = initObj.Mdl_Funciones_Varias.VxDecNul;

                    initObj.FormularioQueAbrir = "Index";
                    break;
            }
            return true;
        }

        public static void Ch_Deduc_Click(short Index, InitializationObject initObj)
        {
            short _tempVar1 = 0;
            Tx_MtoDec_LostFocus(_tempVar1, initObj);
            Pr_Copiar_Montos(initObj);
        }

        public static void LtMto_Click(InitializationObject initObj)
        {

            if (initObj.FrmxPln0.LtMto.ListIndex == -1)
            {
                return;
            }

            //Se cargan las Planillas Visibles e Invisibles.
            Pr_Llena_LtPln(initObj);
        }

        public static void LtPln_Click(InitializationObject initObj)
        {

            //Error si lista no tiene items.
            if (initObj.FrmxPln0.LtPln.Items.Count == 0)
            {
                return;
            }

            //Carga los montos de la planilla seleccionada.
            Pr_Cargar_Campos(initObj);
        }

        public static void LtTPln_Click(InitializationObject initObj)
        {
            short PLA = 0;
            string Obs = "";
            string OBT = "";

            //Verifica que la planilla nesecite o no declaración
            //---------------------------------------------------

            if (VB6Helpers.Instr(T_MODXPLN1.PLNCDEC, initObj.FrmxPln0.LtTPln.get_List(initObj.FrmxPln0.LtTPln.ListIndex)) != 0)
            {
                initObj.FrmxPln0.Tx_NumDec.Enabled = true;
                initObj.FrmxPln0.Tx_FecDec.Enabled = true;
                initObj.FrmxPln0.Tx_CodAdn.Enabled = true;
                initObj.FrmxPln0.Ok_Dec.Enabled = true;
            }

            if (VB6Helpers.Instr(T_MODXPLN1.PLNSDEC, initObj.FrmxPln0.LtTPln.get_List(initObj.FrmxPln0.LtTPln.ListIndex)) != 0)
            {
                initObj.FrmxPln0.Tx_NumDec.Text = "";
                initObj.FrmxPln0.Tx_FecDec.Text = "";
                initObj.FrmxPln0.Tx_CodAdn.Text = "";
                initObj.FrmxPln0.Tx_NumDec.Enabled = false;
                initObj.FrmxPln0.Tx_FecDec.Enabled = false;
                initObj.FrmxPln0.Tx_CodAdn.Enabled = false;
                initObj.FrmxPln0.Ok_Dec.Enabled = false;
            }

            //Fr_Monto.Enabled = true;
            HabilitarFr_Monto(initObj.FrmxPln0, true);

            if (VB6Helpers.Mid(initObj.FrmxPln0.LtTPln.get_List(initObj.FrmxPln0.LtTPln.ListIndex).Trim(), 1, 1) == "4")
            {
                initObj.FrmxPln0.Tx_PlzFin.Enabled = true;
                initObj.FrmxPln0.Tx_PlzFin.Text = "300";
            }
            else
            {
                initObj.FrmxPln0.Tx_PlzFin.Enabled = false;
                initObj.FrmxPln0.Tx_PlzFin.Text = "";
            }

            if (VB6Helpers.Mid(initObj.FrmxPln0.LtTPln.get_List(initObj.FrmxPln0.LtTPln.ListIndex).Trim(), 1, 1) == "6")
            {

                initObj.FrmxPln0.Tx_NumPre.Enabled = true;
                initObj.FrmxPln0.Tx_FecPre.Enabled = true;
                initObj.FrmxPln0.Cb_Bco.Enabled = true;
                initObj.FrmxPln0.Cb_Pbc.Enabled = true;
                initObj.FrmxPln0.Cb_Tippln.Enabled = true;
                initObj.FrmxPln0.Tx_Fecha.Enabled = true;
                initObj.FrmxPln0.Tx_NumIns.Enabled = true;
                initObj.FrmxPln0.Cb_Pais.Enabled = true;

                initObj.FrmxPln0.Cb_Bco.ListIndex = MODGPYF0.PosLista(initObj.FrmxPln0.Cb_Bco, 15);
                
                string tipoPlanilla = initObj.FrmxPln0.LtTPln.get_List(initObj.FrmxPln0.LtTPln.ListIndex);
                if (VB6Helpers.Mid(VB6Helpers.Trim(tipoPlanilla), 1, 3) == "601")
                {
                    initObj.FrmxPln0.Cb_Tippln.ListIndex = MODGPYF0.PosLista(initObj.FrmxPln0.Cb_Tippln, 401);
                }
                else if (VB6Helpers.Mid(VB6Helpers.Trim(tipoPlanilla), 1, 3) == "602")
                {
                    initObj.FrmxPln0.Cb_Tippln.ListIndex = MODGPYF0.PosLista(initObj.FrmxPln0.Cb_Tippln, 402);
                }
                else if (VB6Helpers.Mid(VB6Helpers.Trim(tipoPlanilla), 1, 3) == "603")
                {
                    initObj.FrmxPln0.Cb_Tippln.ListIndex = MODGPYF0.PosLista(initObj.FrmxPln0.Cb_Tippln, 403);
                }

                initObj.FrmxPln0.Cb_Tippln.Enabled = true;
            }
            else
            {

                initObj.FrmxPln0.Tx_NumPre.Enabled = false;
                initObj.FrmxPln0.Tx_FecPre.Enabled = false;
                initObj.FrmxPln0.Cb_Bco.Enabled = false;
                initObj.FrmxPln0.Cb_Pbc.Enabled = false;
                initObj.FrmxPln0.Cb_Tippln.Enabled = false;
                initObj.FrmxPln0.Tx_Fecha.Enabled = false;
                initObj.FrmxPln0.Tx_NumIns.Enabled = false;
            }

            PLA = (short)VB6Helpers.Val(initObj.FrmxPln0.LtTPln.get_List(initObj.FrmxPln0.LtTPln.ListIndex));
            Obs = "Solo efectos estadisticos ";

            if (VB6Helpers.Instr(T_MODXPLN1.PlnEst, VB6Helpers.CStr(PLA)) > 0 || 
                VB6Helpers.Instr(T_MODXPLN1.PLNINF, VB6Helpers.CStr(PLA)) > 0)
            {
                if (VB6Helpers.Left(initObj.FrmxPln0.LtMto.get_List(initObj.FrmxPln0.LtMto.ListIndex), 3) == "Liq")
                {
                    if (VB6Helpers.Instr(initObj.FrmxPln0.Tx_Obs.Text, Obs) == 0)
                    {
                        initObj.FrmxPln0.Tx_Obs.Text = initObj.FrmxPln0.guardaobs;
                    }

                }
                else if (VB6Helpers.Left(initObj.FrmxPln0.LtMto.get_List(initObj.FrmxPln0.LtMto.ListIndex), 3) == "Tra")
                {
                    if (VB6Helpers.Instr(initObj.FrmxPln0.Tx_Obs.Text, "Planilla de transferencia") == 0)
                    {
                        initObj.FrmxPln0.Tx_Obs.Text += " Planilla de transferencia.";
                    }

                }
                else
                {
                    initObj.FrmxPln0.Tx_Obs.Text = initObj.FrmxPln0.guardaobs;
                }

            }
            else
            {
                initObj.FrmxPln0.Tx_Obs.Text = initObj.FrmxPln0.guardaobs;
            }

            if (!string.IsNullOrEmpty(initObj.Mdl_Funciones_Varias.VxDec.numdec) && 
                !string.IsNullOrEmpty(initObj.Mdl_Funciones_Varias.VxDec.FecDec) &&
                initObj.Mdl_Funciones_Varias.VxDec.CodAdn > 0)
            {
                if (string.IsNullOrEmpty(initObj.Mdl_Funciones_Varias.VxDec.NumInf) && 
                    string.IsNullOrEmpty(initObj.Mdl_Funciones_Varias.VxDec.FecInf))
                {
                    OBT = "CNA/CAP. IX-II ";
                    if (VB6Helpers.Instr(initObj.FrmxPln0.Tx_Obs.Text, OBT) == 0)
                    {
                        initObj.FrmxPln0.Tx_Obs.Text = initObj.FrmxPln0.Tx_Obs.Text + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                        initObj.FrmxPln0.Tx_Obs.Text += OBT;
                    }

                }

            }

            LtTPln_LostFocus(initObj);
        }

        public static bool ok_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short i = 0;
            short errores = 0;
            double Mtopar = 0;
            short p = 0;
            short d = 0;
            int n = 0;
            short z = 0;
            short x = 0;
            short largo = 0;
            short q = 0;

            const string CONDEC = "501;502;570;601;603;607";

            //Validaciones.

            if (initObj.FrmxPln0.LtTPln.ListIndex == -1)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = T_MODXPLN1.MsgxPlv,
                    Text = "Debe seleccionar el tipo de planilla",
                    Type = TipoMensaje.Informacion,
                    ControlName = "CmbTipoPlanilla_SelectedValue"
                });

                return false;
            }

            if (VB6Helpers.Instr(T_MODXPLN1.PLNSDEC, initObj.FrmxPln0.LtTPln.get_List(initObj.FrmxPln0.LtTPln.ListIndex)) != 0)
            {
                // capturamos en orden el nombre del control que esta generando el error, mejora la UX.
                string controlName = string.IsNullOrEmpty(initObj.FrmxPln0.Tx_NumDec.Text) ? "TxtNumeroDeclaracion_Text"
                                        : string.IsNullOrEmpty(initObj.FrmxPln0.Tx_FecDec.Text) ? "TxtFechaDeclaracion_Text"
                                        : string.IsNullOrEmpty(initObj.FrmxPln0.Tx_CodAdn.Text) ? "TxtCodigoDeclaracion_Text"
                                        : string.Empty;

                if (!string.IsNullOrEmpty(controlName))
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Title = T_MODXPLN1.MsgxPlv,
                        Text = "Para generar esta planilla no debe ingresar los datos de la declaración.",
                        Type = TipoMensaje.Informacion,
                        ControlName = controlName
                    });

                    return false;
                }
            }

            if (Format.StringToDouble((initObj.FrmxPln0.Tx_MtoDec[6].Text)) <= 0)
            {
                //Valor Liquido Dec > 0.-
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = T_MODXPLN1.MsgxPlv,
                    Text = "El valor correspondiente al Valor Líquido de la Declaración debe ser mayor que cero.",
                    Type = TipoMensaje.Informacion,
                    ControlName = "TxtValBrutoDeclaracion_Text"
                });

                return false;
            }

            if (Format.StringToDouble((initObj.FrmxPln0.Tx_MtoPln[4].Text)) <= 0)
            {
                //Valor Liquido Pln > 0.-
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = T_MODXPLN1.MsgxPlv,
                    Text = "El valor correspondiente al Valor Líquido de la Planilla debe ser mayor que cero.",
                    Type = TipoMensaje.Informacion,
                    ControlName = "TxtValBrutoDeclaracion_Text"
                });

                return false;
            }

            if (MODGPYF1.ValTexto(initObj.MODGPYF0, initObj.FrmxPln0.Tx_MtoDec[6].Text) >
                MODGPYF1.ValTexto(initObj.MODGPYF0, initObj.FrmxPln0.Tx_MtoPln[0].Text))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = T_MODXPLN1.MsgxPlv,
                    Text = "El Valor Líquido de la Declaración excede el valor que requiere la Planilla. Debe ajustar los valores de la Declaración.",
                    Type = TipoMensaje.Informacion,
                    ControlName = "TxtValBrutoDeclaracion_Text"
                });

                return false;
            }

            //TC > 0.
            if ("401500501502".Contains(
                initObj.FrmxPln0.LtTPln.get_List(initObj.FrmxPln0.LtTPln.ListIndex).Trim()) &&
                MODGPYF1.ValTexto(initObj.MODGPYF0, initObj.FrmxPln0.Tx_MtoPln[5].Text) <= 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = T_MODXPLN1.MsgxPlv,
                    Text = "Para generar esta Planilla debe ingresar el monto correspondiente al Tipo de Cambio.",
                    Type = TipoMensaje.Informacion,
                    ControlName = "TxtTipoCambioPlanilla_Text"
                });

                return false;
            }

            if ("401402".Contains(initObj.FrmxPln0.LtTPln.get_List(initObj.FrmxPln0.LtTPln.ListIndex).Trim()) &&
                string.IsNullOrWhiteSpace(initObj.FrmxPln0.Tx_NomCom.Text))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = T_MODXPLN1.MsgxPlv,
                    Text = "Para generar esta Planilla debe ingresar el nombre del comprador.",
                    Type = TipoMensaje.Informacion,
                    ControlName = "TxtNombreComprador_Text"
                });

                return false;
            }

            if (initObj.FrmxPln0.Cb_Pais.ListIndex == -1)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = T_MODXPLN1.MsgxPlv,
                    Text = "Debe seleccionar el País de la Operación",
                    Type = TipoMensaje.Informacion,
                    ControlName = "CmbPaisOperacion_SelectedValue"
                });

                return false;
            }

            //Verifica que el reabje de saldos esté OK.
            i = (short)initObj.FrmxPln0.LtMto.get_ItemData(initObj.FrmxPln0.LtMto.ListIndex);
            switch (i)
            {
                case 1:
                    if (Format.StringToDouble(initObj.FrmxPln0.Tx_MtoPln[0].Text) < (Format.StringToDouble(initObj.FrmxPln0.Tx_MtoPln[4].Text)))
                    {
                        errores = (short)(true ? -1 : 0);
                    }
                    break;
                case 2:
                    if (Format.StringToDouble(initObj.FrmxPln0.Tx_MtoPln[0].Text) < (Format.StringToDouble(initObj.FrmxPln0.Tx_MtoPln[4].Text)))
                    {
                        errores = (short)(true ? -1 : 0);
                    }
                    break;
                case 3:
                    if (Format.StringToDouble(initObj.FrmxPln0.Tx_MtoPln[0].Text) < (Format.StringToDouble(initObj.FrmxPln0.Tx_MtoPln[4].Text)))
                    {
                        errores = (short)(true ? -1 : 0);
                    }
                    break;
            }

            if (errores != 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = T_MODXPLN1.MsgxPlv,
                    Text = "El Valor Líquido de la Planilla (" +
                        Format.FormatCurrency(Format.StringToDouble(initObj.FrmxPln0.Tx_MtoPln[4].Text),"#,###,###,###,##0.00") +
                        ") excede el monto máximo disponible (" +
                        Format.FormatCurrency(Format.StringToDouble(initObj.FrmxPln0.Tx_MtoPln[0].Text),"#,###,###,###,##0.00") + ").",
                    Type = TipoMensaje.Informacion,
                    ControlName = "TxtValBrutoDeclaracion_Text"
                });

                return false;
            }

            //Sólo se validarán los campos que se requieren para el caso de
            //seleccionar una Planilla de Tipo 6..  4..
            bool validar = Fn_Validar_Campos(1, 5, initObj);
            if (!validar)
            {
                return false;
            }

            //Paridad mensual (Sgt_Vmc).-
            Mtopar = MODGTAB1.SyGet_Vmc(initObj.MODGTAB0, uow, initObj.MODXPLN0.VxDatP.CodMnd,
                DateTime.Now.ToString("dd/MM/yyyy"), "P");
            if (Mtopar == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = T_MODXPLN1.MsgxPlv,
                    Text = "No se ha podido establecer la Paridad del último día hábil del mes anterior. Reporte este problema.",
                    Type = TipoMensaje.Informacion
                });

                return false;
            }

            //************************************************
            //Valida que para ingresar una 500 no hayan 400's.-
            //************************************************
            if (VB6Helpers.Left(initObj.FrmxPln0.LtTPln.get_List(initObj.FrmxPln0.LtTPln.ListIndex), 2) == "50" &&
                ExistePln400(initObj) &&
                initObj.FrmxPln0.LtMto.get_ItemData(initObj.FrmxPln0.LtMto.ListIndex) == 1)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = T_MODXPLN1.MsgxPlv,
                    Text = "No puede ingresar una Planilla 500 existiendo planillas del tipo 400 ya generadas.",
                    Type = TipoMensaje.Informacion,
                    ControlName = "CmbTipoPlanilla_SelectedValue"
                });

                return false;
            }

            //se valida que queden numeros para las PVX
            n = (int)MODGRNG.LeeSceRng(initObj.MODGRNG, initObj.MODGUSR, initObj.Mdi_Principal, uow, "PVX");
            //Si retorna -1 no se logro conseguir numero
            if (n == -1)
            {
                return false;
            }

            //************************************************
            //Se crea el espacio para la planilla nueva Visible.
            p = (short)(VB6Helpers.UBound(initObj.MODXPLN1.VxPlvs) + 1);
            VB6Helpers.RedimPreserve(ref initObj.MODXPLN1.VxPlvs, 0, p);

            d = ObtenerIDTag(initObj);
            //Cuando se genera una planilla sin Dec. => d%=-1.
            //------------------------------------------------
            //Carga los datos de la Planilla Visible.
            //------------------------------------------------
            
            //Algoritmo para Dígito Verificador.-
            initObj.MODXPLN1.VxPlvs[p].NumPre = MODXPLN1.Fn_DigVer_xPlv(initObj.MODGSCE.VGen.CodPbc, initObj.MODGSCE.VGen.CodBch, n,
                VB6Helpers.Year(DateTime.Now));

            if (string.IsNullOrEmpty(initObj.MODXPLN1.VxPlvs[p].NumPre))
            {
                return false;
            }

            initObj.MODXPLN1.VxPlvs[p].fecpre = DateTime.Now.ToString("dd/MM/yyyy");

            if (initObj.FrmxPln0.Cb_SecEcBen.ListIndex != -1)
            {
                initObj.MODXPLN1.VxPlvs[p].SecBen = (short)initObj.FrmxPln0.Cb_SecEcBen.get_ItemData_(initObj.FrmxPln0.Cb_SecEcBen.ListIndex);
            }

            if (initObj.FrmxPln0.Cb_SecEcIn.ListIndex != -1)
            {
                initObj.MODXPLN1.VxPlvs[p].SecInv = (short)initObj.FrmxPln0.Cb_SecEcIn.get_ItemData_(initObj.FrmxPln0.Cb_SecEcIn.ListIndex);
            }

            if (!string.IsNullOrEmpty(initObj.FrmxPln0.Tx_PrcPar.Text))
            {
                initObj.MODXPLN1.VxPlvs[p].PrcPar = Format.StringToDouble((initObj.FrmxPln0.Tx_PrcPar.Text));
            }

            initObj.MODXPLN1.VxPlvs[p].TipPln = (short)VB6Helpers.Val(initObj.FrmxPln0.LtTPln.get_List(initObj.FrmxPln0.LtTPln.ListIndex));
            //------------------------------------------------
            initObj.MODXPLN1.VxPlvs[p].cencos = initObj.MODGUSR.UsrEsp.CentroCosto;
            initObj.MODXPLN1.VxPlvs[p].codusr = initObj.MODGUSR.UsrEsp.Especialista;
            initObj.MODXPLN1.VxPlvs[p].Fecing = DateTime.Now.ToString("dd/MM/yyyy");
            initObj.MODXPLN1.VxPlvs[p].FecAct = DateTime.Now.ToString("dd/MM/yyyy");
            //------------------------------------------------
            initObj.MODXPLN1.VxPlvs[p].codcct = VB6Helpers.Mid(initObj.MODXPLN0.VxDatP.NumOpe, 1, 3);
            initObj.MODXPLN1.VxPlvs[p].codpro = VB6Helpers.Mid(initObj.MODXPLN0.VxDatP.NumOpe, 4, 2);
            initObj.MODXPLN1.VxPlvs[p].codesp = VB6Helpers.Mid(initObj.MODXPLN0.VxDatP.NumOpe, 6, 2);
            initObj.MODXPLN1.VxPlvs[p].codofi = VB6Helpers.Mid(initObj.MODXPLN0.VxDatP.NumOpe, 8, 3);
            initObj.MODXPLN1.VxPlvs[p].codope = VB6Helpers.Mid(initObj.MODXPLN0.VxDatP.NumOpe, 11, 5);
            //------------------------------------------------
            initObj.MODXPLN1.VxPlvs[p].Estado = 1;
            initObj.MODXPLN1.VxPlvs[p].numdec = VB6Helpers.Trim(initObj.FrmxPln0.Tx_NumDec.Text);
            initObj.MODXPLN1.VxPlvs[p].FecDec = VB6Helpers.Trim(initObj.FrmxPln0.Tx_FecDec.Text);
            initObj.MODXPLN1.VxPlvs[p].CodAdn = (short)VB6Helpers.Val(initObj.FrmxPln0.Tx_CodAdn.Text);
            initObj.MODXPLN1.VxPlvs[p].FecVen = VB6Helpers.Trim(initObj.FrmxPln0.Tx_FecVen.Text);

            //------------------------------------------------
            d = ObtenerIDTag(initObj);

            if (VB6Helpers.Instr(CONDEC, initObj.MODXPLN1.VxPlvs[p].TipPln.ToString("000")) != 0 && d >= 0)
            {
                if (MODGPYF0.Componer(initObj.Module1.PartysOpe[initObj.MODXPLN0.VxDatP.IndPrt].LlaveArchivo, "~", "") ==
                    initObj.MODXPLN0.VxDecP[d].PrtExp1)
                {
                    initObj.MODXPLN1.VxPlvs[p].RutExp = MODGPYF0.Componer(initObj.MODXPLN0.VxDecP[d].PrtExp1, "~", "");
                    initObj.MODXPLN1.VxPlvs[p].PrtExp = VB6Helpers.Left(initObj.MODXPLN0.VxDecP[d].PrtExp1 + "||||||||||||", 12);
                    initObj.MODXPLN1.VxPlvs[p].IndNom = initObj.MODXPLN0.VxDecP[d].IndNom1;
                    initObj.MODXPLN1.VxPlvs[p].IndDir = initObj.MODXPLN0.VxDecP[d].IndDir1;
                }
                else if (MODGPYF0.Componer(initObj.Module1.PartysOpe[initObj.MODXPLN0.VxDatP.IndPrt].LlaveArchivo, "~", "") ==
                    initObj.MODXPLN0.VxDecP[d].PrtExp2)
                {
                    initObj.MODXPLN1.VxPlvs[p].RutExp = MODGPYF0.Componer(initObj.MODXPLN0.VxDecP[d].PrtExp2, "~", "");
                    initObj.MODXPLN1.VxPlvs[p].PrtExp = VB6Helpers.Left(initObj.MODXPLN0.VxDecP[d].PrtExp2 + "||||||||||||", 12);
                    initObj.MODXPLN1.VxPlvs[p].IndNom = initObj.MODXPLN0.VxDecP[d].IndNom2;
                    initObj.MODXPLN1.VxPlvs[p].IndDir = initObj.MODXPLN0.VxDecP[d].IndDir2;
                }
                else
                {
                    initObj.MODXPLN1.VxPlvs[p].RutExp = MODGPYF0.Componer(initObj.MODXPLN0.VxDecP[d].PrtExp1, "~", "");
                    initObj.MODXPLN1.VxPlvs[p].PrtExp = VB6Helpers.Left(initObj.MODXPLN0.VxDecP[d].PrtExp1 + "||||||||||||", 12);
                    initObj.MODXPLN1.VxPlvs[p].IndNom = initObj.MODXPLN0.VxDecP[d].IndNom1;
                    initObj.MODXPLN1.VxPlvs[p].IndDir = initObj.MODXPLN0.VxDecP[d].IndDir1;
                }

            }
            else
            {
                initObj.MODXPLN1.VxPlvs[p].RutExp = initObj.Module1.PartysOpe[initObj.MODXPLN0.VxDatP.IndPrt].rut;
                initObj.MODXPLN1.VxPlvs[p].PrtExp = initObj.Module1.PartysOpe[initObj.MODXPLN0.VxDatP.IndPrt].LlaveArchivo;
                initObj.MODXPLN1.VxPlvs[p].IndNom = initObj.Module1.PartysOpe[initObj.MODXPLN0.VxDatP.IndPrt].IndNombre;
                initObj.MODXPLN1.VxPlvs[p].IndDir = initObj.Module1.PartysOpe[initObj.MODXPLN0.VxDatP.IndPrt].IndDireccion;
            }

            //Advierte que la Planilla NO pasará sin RUT.-
            if (string.IsNullOrEmpty(initObj.MODXPLN1.VxPlvs[p].RutExp))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = T_MODXPLN1.MsgxPlv,
                    Text = "Esta Planilla se generó por algún motivo sin el Rut del Exportador. Reporte este problema a Sistemas.",
                    Type = TipoMensaje.Informacion
                });

                return false;
            }

            initObj.MODXPLN1.VxPlvs[p].RutExp = VB6Helpers.Right("0000000000" + initObj.MODXPLN1.VxPlvs[p].RutExp, 10);
            //------------------------------------------------
            initObj.MODXPLN1.VxPlvs[p].CodMnd = initObj.MODXPLN0.VxDatP.CodMnd;
            initObj.MODXPLN1.VxPlvs[p].MtoBru = Format.StringToDouble(initObj.FrmxPln0.Tx_MtoPln[1].Text);
            initObj.MODXPLN1.VxPlvs[p].MtoCom = Format.StringToDouble(initObj.FrmxPln0.Tx_MtoPln[2].Text);
            initObj.MODXPLN1.VxPlvs[p].MtoOtg = Format.StringToDouble(initObj.FrmxPln0.Tx_MtoPln[3].Text);
            initObj.MODXPLN1.VxPlvs[p].MtoLiq = Format.StringToDouble(initObj.FrmxPln0.Tx_MtoPln[4].Text);
            initObj.MODXPLN1.VxPlvs[p].Mtopar = Mtopar;
            if (initObj.MODXPLN1.VxPlvs[p].Mtopar != 0)
            {
                initObj.MODXPLN1.VxPlvs[p].MtoDol = (
                    initObj.MODXPLN1.VxPlvs[p].MtoLiq / initObj.MODXPLN1.VxPlvs[p].Mtopar);
            }

            if (!(initObj.MODXPLN1.VxPlvs[p].TipPln == 601 || initObj.MODXPLN1.VxPlvs[p].TipPln == 603))
            {
                if (initObj.MODXPLN1.VxPlvs[p].MtoDol != 0)
                {
                    initObj.MODXPLN1.VxPlvs[p].TipCam = (
                        (initObj.MODXPLN0.VxDatP.TipCam * initObj.MODXPLN1.VxPlvs[p].MtoLiq) / initObj.MODXPLN1.VxPlvs[p].MtoDol);
                }

            }

            initObj.MODXPLN1.VxPlvs[p].TipCamo = initObj.MODXPLN0.VxDatP.TipCam;
            initObj.MODXPLN1.VxPlvs[p].PlzBcc = short.Parse(initObj.Usuario.CodPBC);//short.Parse(initObj.Usuario.MODGUSR_UsrEsp_CentroCosto_CodPBC);

            //------------------------------------------------
            //Datos Financiamiento Original.-
            //------------------------------------------------
            initObj.MODXPLN1.VxPlvs[p].DfoNpr = VB6Helpers.Trim(initObj.FrmxPln0.Tx_NumPre.Text);
            initObj.MODXPLN1.VxPlvs[p].DfoFpr = VB6Helpers.Trim(initObj.FrmxPln0.Tx_FecPre.Text);
            if (initObj.FrmxPln0.Cb_Bco.ListIndex != -1)
            {
                initObj.MODXPLN1.VxPlvs[p].DfoCea = (short)initObj.FrmxPln0.Cb_Bco.get_ItemData_(initObj.FrmxPln0.Cb_Bco.ListIndex);
            }

            if (initObj.FrmxPln0.Cb_Pbc.ListIndex != -1)
            {
                z = (short)initObj.FrmxPln0.Cb_Pbc.get_ItemData_(initObj.FrmxPln0.Cb_Pbc.ListIndex);
                if (z != 0)
                {
                    initObj.MODXPLN1.VxPlvs[p].DfoCbc = z;
                }
            }

            if (initObj.FrmxPln0.Cb_Tippln.ListIndex != -1)
            {
                z = (short)initObj.FrmxPln0.Cb_Tippln.get_ItemData_(initObj.FrmxPln0.Cb_Tippln.ListIndex);
                if (z != 0)
                {
                    initObj.MODXPLN1.VxPlvs[p].DfoCtf = z;
                }
            }

            if (T_MODXPLN1.PLN400.Contains(initObj.FrmxPln0.LtTPln.get_List(initObj.FrmxPln0.LtTPln.ListIndex)))
            {
                initObj.MODXPLN1.VxPlvs[p].AfiMnd = initObj.MODXPLN0.VxDatP.CodMnd;
                initObj.MODXPLN1.VxPlvs[p].AfiPar = Mtopar;
                initObj.MODXPLN1.VxPlvs[p].AfiMto = Format.StringToDouble(initObj.FrmxPln0.Tx_MtoPln[4].Text);
                if (initObj.MODXPLN1.VxPlvs[p].AfiPar > 0)
                {
                    initObj.MODXPLN1.VxPlvs[p].AfiMtoD = (
                        initObj.MODXPLN1.VxPlvs[p].AfiMto / initObj.MODXPLN1.VxPlvs[p].AfiPar);
                }

                initObj.MODXPLN1.VxPlvs[p].AfiVen = (short)VB6Helpers.Val(initObj.FrmxPln0.Tx_PlzFin.Text);
            }

            if (d >= 0)
            {
                //------------------------------------------------
                initObj.MODXPLN1.VxPlvs[p].DiePbc = initObj.MODXPLN0.VxDecP[d].CodPbc;
                initObj.MODXPLN1.VxPlvs[p].DieNum = initObj.MODXPLN0.VxDecP[d].NumInf;
                initObj.MODXPLN1.VxPlvs[p].DieFec = initObj.MODXPLN0.VxDecP[d].FecInf;
                initObj.MODXPLN1.VxPlvs[p].ObsPln = initObj.FrmxPln0.Tx_Obs.Text.Trim();
                //------------------------------------------------
            }
            initObj.MODXPLN1.VxPlvs[p].Status = T_MODGCVD.EstadoIng;
            initObj.MODXPLN1.VxPlvs[p].IndPrt = initObj.MODXPLN0.VxDatP.IndPrt;
            initObj.MODXPLN1.VxPlvs[p].TipMto = (short)initObj.FrmxPln0.LtMto.get_ItemData(initObj.FrmxPln0.LtMto.ListIndex);
            initObj.MODXPLN1.VxPlvs[p].ValRet = Format.StringToDouble((initObj.FrmxPln0.Tx_MtoDec[0].Text));
            initObj.MODXPLN1.VxPlvs[p].ValCom = Format.StringToDouble((initObj.FrmxPln0.Tx_MtoDec[1].Text));
            initObj.MODXPLN1.VxPlvs[p].ValGas = 0;
            initObj.MODXPLN1.VxPlvs[p].ValFle = Format.StringToDouble((initObj.FrmxPln0.Tx_MtoDec[2].Text));
            initObj.MODXPLN1.VxPlvs[p].ValSeg = Format.StringToDouble((initObj.FrmxPln0.Tx_MtoDec[3].Text));
            initObj.MODXPLN1.VxPlvs[p].ValLiq = Format.StringToDouble(initObj.FrmxPln0.Tx_MtoDec[6].Text);
            initObj.MODXPLN1.VxPlvs[p].ObsPln = initObj.FrmxPln0.Tx_Obs.Text.Trim();
            if (initObj.FrmxPln0.LtMto.Items.Count > 0)
            {
                int montoVal = initObj.FrmxPln0.LtMto.get_ItemData(initObj.FrmxPln0.LtMto.ListIndex);
                if (montoVal == 1 || montoVal == 2)
                {
                    initObj.MODXPLN1.VxPlvs[p].PlnEst = 0;
                }
                else if (montoVal == 3)
                {
                    initObj.MODXPLN1.VxPlvs[p].PlnEst = 1;
                }

            }

            //---Se activan campos deducibles de la planilla--
            if (initObj.FrmxPln0.Ch_Deduc[0].Value != 0)
            {
                initObj.MODXPLN1.VxPlvs[p].DedCom = (short)(true ? -1 : 0);
            }
            else
            {
                initObj.MODXPLN1.VxPlvs[p].DedCom = (short)(false ? -1 : 0);
            }

            if ((initObj.FrmxPln0.Ch_Deduc[1].Value | initObj.FrmxPln0.Ch_Deduc[1].Value) != 0)
            {
                initObj.MODXPLN1.VxPlvs[p].DedFle = (short)(true ? -1 : 0);
            }
            else
            {
                initObj.MODXPLN1.VxPlvs[p].DedFle = (short)(false ? -1 : 0);
            }

            if (initObj.FrmxPln0.Cb_Pais.ListIndex != -1)
            {
                initObj.Mdl_Funciones_Varias.VaxPlv.codpai = (short)initObj.FrmxPln0.Cb_Pais.get_ItemData_(initObj.FrmxPln0.Cb_Pais.ListIndex);
            }

            //Se agregan datos de CER   21/12/99
            initObj.MODXPLN1.VxPlvs[p].fecins = initObj.Mdl_Funciones_Varias.VaxPlv.fecins ?? string.Empty;
            initObj.MODXPLN1.VxPlvs[p].NumCre = initObj.Mdl_Funciones_Varias.VaxPlv.NumCre;
            initObj.MODXPLN1.VxPlvs[p].codpai = initObj.Mdl_Funciones_Varias.VaxPlv.codpai;
            initObj.Mdl_Funciones_Varias.VaxPlv = initObj.Mdl_Funciones_Varias.VaPliNul;
            string _tempVar1 = initObj.FrmxPln0.Tx_NomCom.Text;
            initObj.MODXPLN1.VxPlvs[p].nomcom = MODGFYS.Escribe_Nombre(ref _tempVar1);

            //---------------------------------------------------
            //Rebaja los saldos cuando existe declaración (aumenta el monto cancelado).

            d = ObtenerIDTag(initObj);
            RebajaSaldos(d, p, 1, initObj);
            MODXPLN0.GetDis_xDec(initObj.MODXPLN0, d);

            //*****************************************************
            //Se crea el espacio para la planilla nueva Invisible.*
            //*****************************************************
            if (VB6Helpers.Val(initObj.FrmxPln0.Tx_MtoDec[7].Text) != 0)
            {
                largo = (short)VB6Helpers.UBound(initObj.MODGPLI1.Vplis);

                q = (short)(largo + 1);
                VB6Helpers.RedimPreserve(ref initObj.MODGPLI1.Vplis, 0, q);
                //Cuando se genera una planilla sin Dec. => d%=0.
                d = ObtenerIDTag(initObj);

                //Carga los datos de la Planilla Invisible.
                //------------------------------------------------
                initObj.MODGPLI1.Vplis[q].NumPli = "152528-12K";
                if (string.IsNullOrWhiteSpace(initObj.MODGPLI1.Vplis[q].NumPli))
                {
                    return false;
                }
                initObj.MODGPLI1.Vplis[q].FecPli = DateTime.Now.ToString("dd/MM/yyyy");
                initObj.MODGPLI1.Vplis[q].TipPln = (short)VB6Helpers.Val(initObj.FrmxPln0.LtTPln.get_List(initObj.FrmxPln0.LtPln.ListIndex));
                //------------------------------------------------
                initObj.MODGPLI1.Vplis[q].cencos = initObj.MODGUSR.UsrEsp.CentroCosto;
                initObj.MODGPLI1.Vplis[q].codusr = initObj.MODGUSR.UsrEsp.Especialista;
                initObj.MODGPLI1.Vplis[q].Fecing = DateTime.Now.ToString("dd/MM/yyyy");
                initObj.MODGPLI1.Vplis[q].FecAct = DateTime.Now.ToString("dd/MM/yyyy");
                //------------------------------------------------
                initObj.MODGPLI1.Vplis[q].codcct = VB6Helpers.Mid(initObj.MODXPLN0.VxDatP.NumOpe, 1, 3);
                initObj.MODGPLI1.Vplis[q].codpro = VB6Helpers.Mid(initObj.MODXPLN0.VxDatP.NumOpe, 4, 2);
                initObj.MODGPLI1.Vplis[q].codesp = VB6Helpers.Mid(initObj.MODXPLN0.VxDatP.NumOpe, 6, 2);
                initObj.MODGPLI1.Vplis[q].codofi = VB6Helpers.Mid(initObj.MODXPLN0.VxDatP.NumOpe, 8, 3);
                initObj.MODGPLI1.Vplis[q].codope = VB6Helpers.Mid(initObj.MODXPLN0.VxDatP.NumOpe, 11, 5);
                //------------------------------------------------
                initObj.MODGPLI1.Vplis[q].Estado = 1;
                initObj.MODGPLI1.Vplis[q].numdec = VB6Helpers.Trim(initObj.FrmxPln0.Tx_NumDec.Text);
                initObj.MODGPLI1.Vplis[q].FecDec = VB6Helpers.Trim(initObj.FrmxPln0.Tx_FecDec.Text);
                initObj.MODGPLI1.Vplis[q].CodAdn = (short)VB6Helpers.Val(initObj.FrmxPln0.Tx_CodAdn.Text);
                //------------------------------------------------
                initObj.MODGPLI1.Vplis[q].rutcli = initObj.Module1.PartysOpe[initObj.MODXPLN0.VxDatP.IndPrt].rut;
                initObj.MODGPLI1.Vplis[q].rutcli = VB6Helpers.Right("0000000000" + initObj.MODGPLI1.Vplis[q].rutcli, 10);
                initObj.MODGPLI1.Vplis[q].PrtCli = initObj.Module1.PartysOpe[initObj.MODXPLN0.VxDatP.IndPrt].LlaveArchivo;
                initObj.MODGPLI1.Vplis[q].IndNom = initObj.Module1.PartysOpe[initObj.MODXPLN0.VxDatP.IndPrt].IndNombre;
                initObj.MODGPLI1.Vplis[q].IndDir = initObj.Module1.PartysOpe[initObj.MODXPLN0.VxDatP.IndPrt].IndDireccion;
                //------------------------------------------------
                initObj.MODGPLI1.Vplis[q].CodMnd = initObj.MODXPLN0.VxDatP.CodMBC;
                initObj.MODGPLI1.Vplis[q].MtoOpe = Format.StringToDouble(initObj.FrmxPln0.Tx_MtoDec[7].Text);
                initObj.MODGPLI1.Vplis[q].MtoNac = Format.StringToDouble(initObj.FrmxPln0.Tx_MtoPln[4].Text);
                initObj.MODGPLI1.Vplis[q].Mtopar = Mtopar;
                initObj.MODGPLI1.Vplis[q].MtoDol = (
                initObj.MODGPLI1.Vplis[q].MtoNac * initObj.MODGPLI1.Vplis[q].Mtopar);
                initObj.MODGPLI1.Vplis[q].TipCam = Format.StringToDouble(initObj.FrmxPln0.Tx_MtoPln[5].Text);
                initObj.MODGPLI1.Vplis[q].PlzBcc = short.Parse(initObj.Usuario.CodPBC); //short.Parse(initObj.Usuario.MODGUSR_UsrEsp_CentroCosto_CodPBC);

                

                //------------------------------------------------
                initObj.MODGPLI1.Vplis[q].DiePbc = initObj.MODXPLN0.VxDecP[d].CodPbc;
                initObj.MODGPLI1.Vplis[q].DieNum = initObj.MODXPLN0.VxDecP[d].NumInf;
                initObj.MODGPLI1.Vplis[q].DieFec = initObj.MODXPLN0.VxDecP[d].FecInf;
                initObj.MODGPLI1.Vplis[q].ObsPli = VB6Helpers.Trim(initObj.FrmxPln0.Tx_Obs.Text);
                //------------------------------------------------
                initObj.MODGPLI1.Vplis[q].Status = T_MODGCVD.EstadoIng;
                initObj.MODGPLI1.Vplis[q].IndPrt = initObj.MODXPLN0.VxDatP.IndPrt;
                initObj.MODGPLI1.Vplis[q].TipMto = (short)initObj.FrmxPln0.LtMto.get_ItemData(initObj.FrmxPln0.LtMto.ListIndex);
                //------------------------------------------------
                //Se actualiza arreglo que respalda la lista de planillas.
            }

            //************************************************

            //Se ingresan los nuevos datos en la lista de Planillas.
            Pr_Llena_LtPln(initObj);

            //Si todo está justificado => se posiciona en botón Aceptar.
            if ((initObj.MODXPLN0.VxDatP.MtoLiqs +
                initObj.MODXPLN0.VxDatP.MtoInfs + initObj.MODXPLN0.VxDatP.MtoTrans) == 0)
            {
                initObj.FrmxPln0.Boton[0].Enabled = true;
                initObj.FrmxPln0.Boton[1].Enabled = initObj.FrmxPln0.Boton[0].Enabled;
            }
            else
            {
                initObj.FrmxPln0.Boton[0].Enabled = false;
                initObj.FrmxPln0.Boton[1].Enabled = initObj.FrmxPln0.Boton[0].Enabled;
            }

            initObj.Mdl_Funciones_Varias.VaxPlv.fecins = initObj.FrmxPln0.Tx_Fecha.Text;
            initObj.Mdl_Funciones_Varias.VaxPlv.NumCre = VB6Helpers.Val(initObj.FrmxPln0.Tx_NumIns.Text);

            //SE LIMPIAN LOS DATOS DE LA DECLARACION SI EXISTEN
            //--------------------------------------------------
            initObj.Mdl_Funciones_Varias.VxDec = initObj.Mdl_Funciones_Varias.VxDecNul;

            //Limpio las cajas del tipo deducir
            //---------------------------------
            initObj.FrmxPln0.Ch_Deduc[0].Value = 0;
            //FrmxPln0.Ch_Deduc_Click(0, initObj);

            initObj.FrmxPln0.Ch_Deduc[1].Value = 0;
            //FrmxPln0.Ch_Deduc_Click(1, initObj);

            initObj.FrmxPln0.Ch_Deduc[2].Value = 0;
            //FrmxPln0.Ch_Deduc_Click(2, initObj);
            

            initObj.FrmxPln0.Cb_SecEcBen.ListIndex = -1;
            initObj.FrmxPln0.Cb_SecEcIn.ListIndex = -1;
            initObj.FrmxPln0.Tx_PrcPar.Text = "";
            initObj.FrmxPln0.Tx_NomCom.Text = "";

            //Fr_Sec.Enabled = false;
            HabilitarFr_Sec(initObj.FrmxPln0, false);

            initObj.FrmxPln0.Cb_Pais.ListIndex = -1;

            return true;
        }

        private static short ObtenerIDTag(InitializationObject initObj)
        {
            short d;

            if (initObj.FrmxPln0.Ok_Dec.Tag != null && string.IsNullOrEmpty(initObj.FrmxPln0.Ok_Dec.Tag.ToString()))
            {
                d = -1;
            }
            else
            {
                d = (short)VB6Helpers.Val(initObj.FrmxPln0.Ok_Dec.Tag);
            }

            return d;
        }

        public static bool Ok_Dec_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short i = 0;
            string OBT = "";
            short n = 0;
            short x = 0;
            short m = 0;
            short PrtyOK = 0;
            string p = "";
            string tr = "";

            //Valida Número de Declaración.
            if (string.IsNullOrEmpty(initObj.FrmxPln0.Tx_NumDec.Text) && (!string.IsNullOrEmpty(initObj.FrmxPln0.Tx_FecDec.Text) ||
                !string.IsNullOrEmpty(initObj.FrmxPln0.Tx_CodAdn.Text)))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar el Número de la Declaración de Exportación.",
                    Title = "Planillas Visibles de Exportaciones",
                    Type = TipoMensaje.Informacion,
                    ControlName = "TxtNumeroDeclaracion_Text"
                });

                return false;
            }

            //Valida Fecha de Declaración.
            if (string.IsNullOrEmpty(initObj.FrmxPln0.Tx_FecDec.Text) && 
                (!string.IsNullOrEmpty(initObj.FrmxPln0.Tx_NumDec.Text) || !string.IsNullOrEmpty(initObj.FrmxPln0.Tx_CodAdn.Text)))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar la Fecha de la Declaración de Exportación.",
                    Title = "Planillas Visibles de Exportaciones",
                    Type = TipoMensaje.Informacion,
                    ControlName = "TxtFechaDeclaracion_Text"
                });

                return false;
            }

            //Valida Aduana.
            if (string.IsNullOrEmpty(initObj.FrmxPln0.Tx_CodAdn.Text) && 
                (!string.IsNullOrEmpty(initObj.FrmxPln0.Tx_NumDec.Text) || !string.IsNullOrEmpty(initObj.FrmxPln0.Tx_FecDec.Text)))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe Ingresar la Aduana correspondiente a la Declaración de Exportación.",
                    Title = "Planillas Visibles de Exportaciones",
                    Type = TipoMensaje.Informacion,
                    ControlName = "TxtCodigoDeclaracion_Text"
                });

                return false;
            }

            //Limpia los datos de la Declaración.
            for (i = 0; i <= 8; i++)
            {
                initObj.FrmxPln0.Tx_MtoDec[0].Text = 0.ToString("0.00");
            }

            //Sólo si la declaración existe.

            OBT = "";
            if(!string.IsNullOrEmpty(initObj.FrmxPln0.Tx_NumDec.Text))
            {
                //Busca la Dec 1º en memoria, 2º en disco.
                n = MODXPLN0.Get_xDec(initObj, VB6Helpers.Trim(initObj.FrmxPln0.Tx_NumDec.Text),
                    VB6Helpers.Trim(initObj.FrmxPln0.Tx_FecDec.Text), (short)VB6Helpers.Val(initObj.FrmxPln0.Tx_CodAdn.Text));
                if (n < 0)
                {
                    x = Mdl_Funciones.SyGet_xDec(initObj.Mdl_Funciones_Varias, uow, VB6Helpers.Trim(initObj.FrmxPln0.Tx_NumDec.Text),
                        VB6Helpers.Trim(initObj.FrmxPln0.Tx_FecDec.Text), (short)VB6Helpers.Val(initObj.FrmxPln0.Tx_CodAdn.Text));
                    n = MODXPLN0.Put_xDec(initObj, uow, initObj.MODXPLN0.VxDatP.CodMnd, -1);
                    if (n == 0)
                    {
                        return false;
                    }
                    MODXPLN0.GetDis_xDec(initObj.MODXPLN0, n);
                }
                else
                {
                    m = MODXPLN0.Releer_xDec(initObj.MODXPLN0, VB6Helpers.Trim(initObj.FrmxPln0.Tx_NumDec.Text),
                        VB6Helpers.Trim(initObj.FrmxPln0.Tx_FecDec.Text), (short)VB6Helpers.Val(initObj.FrmxPln0.Tx_CodAdn.Text));
                    if (m >= 0)
                    {
                        x = Mdl_Funciones.SyGet_xDec(initObj.Mdl_Funciones_Varias, uow,
                            VB6Helpers.Trim(initObj.FrmxPln0.Tx_NumDec.Text),
                            VB6Helpers.Trim(initObj.FrmxPln0.Tx_FecDec.Text),
                            (short)VB6Helpers.Val(initObj.FrmxPln0.Tx_CodAdn.Text));
                        n = MODXPLN0.Put_xDec(initObj, uow, initObj.MODXPLN0.VxDatP.CodMnd, m);
                        if (n < 0)
                        {
                            return false;
                        }
                        MODXPLN0.GetDis_xDec(initObj.MODXPLN0, n);
                    }

                }

                if (string.IsNullOrEmpty(initObj.Mdl_Funciones_Varias.VxDec.NumInf) &&
                    string.IsNullOrEmpty(initObj.Mdl_Funciones_Varias.VxDec.FecInf))
                {
                    OBT = "CNA/CAP. IX-II ";
                }

                //Valida que los Partys concuerden.
                PrtyOK = 0;
                p = initObj.Module1.PartysOpe[initObj.MODXPLN0.VxDatP.IndPrt].LlaveArchivo;
                p = MODGPYF0.Componer(p, "~", "");
                if (p == initObj.MODXPLN0.VxDecP[n].PrtExp1 && initObj.Module1.PartysOpe[initObj.MODXPLN0.VxDatP.IndPrt].IndNombre == initObj.MODXPLN0.VxDecP[n].IndNom1)
                {
                    PrtyOK = 1;
                }

                if (p == initObj.MODXPLN0.VxDecP[n].PrtExp2 && initObj.Module1.PartysOpe[initObj.MODXPLN0.VxDatP.IndPrt].IndNombre == initObj.MODXPLN0.VxDecP[n].IndNom2)
                {
                    PrtyOK = 2;
                }

                if (PrtyOK == 0)
                {
                    //MsgBoxResult _switchVar1 = VB6Helpers.MsgBox("La Declaración existe pero está asociada a otro Exportador. ¿ Aún así desea utilizarla ?", (MsgBoxStyle)MODGPYF0.pito(292), MODXPLN1.MsgxPlv);
                    //if ((int)_switchVar1 == 2 || (int)_switchVar1 == 7)
                    //{
                    //    //CANCELAR
                    //    //initObj.FrmxPln0.Tx_NumDec.SetFocus();
                    //    return;
                    //}

                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "La Declaración existe pero está asociada a otro Exportador. Modifique los datos e intente nuevamente.",
                        Title = "Planillas Visibles de Exportaciones",
                        Type = TipoMensaje.Informacion,
                        ControlName = "TxtNumeroDeclaracion_Text"
                    });
                }

                //Se verifica si la Declaración tiene disponible para Party1.
                if (PrtyOK == 1 && initObj.MODXPLN0.VxDecP[n].ValRet1d <= 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "No existe monto disponible asociado al Participante de la Declaración ingresada. Corrija los datos y reintente con otra Declaración.",
                        Title = "Planillas Visibles de Exportaciones",
                        Type = TipoMensaje.Informacion,
                        ControlName = "TxtNumeroDeclaracion_Text"
                    });

                    return false;
                }

                //Se verifica si la Declaración tiene disponible para Party2.
                if (PrtyOK == 2 && initObj.MODXPLN0.VxDecP[n].ValRet2d <= 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "No existe monto disponible asociado al Participante de la Declaración ingresada. Corrija los datos y reintente con otra Declaración.",
                        Title = "Planillas Visibles de Exportaciones",
                        Type = TipoMensaje.Informacion,
                        ControlName = "TxtNumeroDeclaracion_Text"
                    });

                    return false;
                }

                //Se guarda el indice de la Dec.
                initObj.FrmxPln0.Ok_Dec.Tag = n;

                //Carga los valores a los objetos.
                if (VB6Helpers.Trim(initObj.MODXPLN0.VxDecP[n].PrtExp1) == p)
                {
                    initObj.FrmxPln0.Tx_MtoDec[0].Text = initObj.MODXPLN0.VxDecP[n].ValRet1d.ToString("0.00");
                    initObj.FrmxPln0.Tx_MtoDec[1].Text = initObj.MODXPLN0.VxDecP[n].ValCom1d.ToString("0.00");
                    initObj.FrmxPln0.Tx_MtoDec[2].Text = initObj.MODXPLN0.VxDecP[n].ValFle1d.ToString("0.00");
                    initObj.FrmxPln0.Tx_MtoDec[3].Text = initObj.MODXPLN0.VxDecP[n].ValSeg1d.ToString("0.00");
                }
                else
                {
                    if (VB6Helpers.Trim(initObj.MODXPLN0.VxDecP[n].PrtExp2) == p)
                    {
                        initObj.FrmxPln0.Tx_MtoDec[0].Text = initObj.MODXPLN0.VxDecP[n].ValRet2d.ToString("0.00");
                        initObj.FrmxPln0.Tx_MtoDec[1].Text = initObj.MODXPLN0.VxDecP[n].ValCom2d.ToString("0.00");
                        initObj.FrmxPln0.Tx_MtoDec[2].Text = initObj.MODXPLN0.VxDecP[n].ValFle2d.ToString("0.00");
                        initObj.FrmxPln0.Tx_MtoDec[3].Text = initObj.MODXPLN0.VxDecP[n].ValSeg2d.ToString("0.00");
                    }
                    else
                    {
                        initObj.FrmxPln0.Tx_MtoDec[0].Text = initObj.MODXPLN0.VxDecP[n].ValRet1d.ToString("0.00");
                        initObj.FrmxPln0.Tx_MtoDec[1].Text = initObj.MODXPLN0.VxDecP[n].ValCom1d.ToString("0.00");
                        initObj.FrmxPln0.Tx_MtoDec[2].Text = initObj.MODXPLN0.VxDecP[n].ValFle1d.ToString("0.00");
                        initObj.FrmxPln0.Tx_MtoDec[3].Text = initObj.MODXPLN0.VxDecP[n].ValSeg1d.ToString("0.00");
                    }

                }

                initObj.FrmxPln0.Tx_FecVen.Text = VB6Helpers.Format(initObj.MODXPLN0.VxDecP[n].FecRet, "dd/MM/yyyy");

                //Despliega info de Observaciones.-
                if (VB6Helpers.Instr(initObj.FrmxPln0.Tx_Obs.Text, OBT) == 0 && string.IsNullOrEmpty(initObj.MODXPLN0.VxDecP[n].NumInf) &&
                    string.IsNullOrEmpty(initObj.MODXPLN0.VxDecP[n].FecInf))
                {
                    initObj.FrmxPln0.Tx_Obs.Text = initObj.FrmxPln0.Tx_Obs.Text + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                    initObj.FrmxPln0.Tx_Obs.Text = initObj.FrmxPln0.Tx_Obs.Text + "CNA/CAP. IX-II ";
                }

            }
            else
            {
                //No existe la Dec.
                tr = initObj.FrmxPln0.Tx_Obs.Text;
                if(!string.IsNullOrEmpty(OBT))
                {
                    initObj.FrmxPln0.Tx_Obs.Text = MODGPYF0.Componer(tr, OBT, "");
                }

                initObj.FrmxPln0.Ok_Dec.Tag = -1;
            }

            //Copia los montos en los campos de las Planillas.
            Pr_Copiar_Montos(initObj);

            short _tempVar1 = 0;
            FrmxPln0.Tx_MtoDec_LostFocus(_tempVar1, initObj);

            return true;
        }

        public static void Tx_MtoDec_LostFocus(short Index, InitializationObject initObj)
        {
            short n = 0;
            double VBAR = 0;
            double VCAR = 0;
            double VFAR = 0;
            double VSAR = 0;
            double VGAR = 0;
            double VAAR = 0;
            double VTAR = 0;
            string s1 = "";
            string s2 = "";
            double Saldo = 0;
            double VLAR = 0;
            double VO = 0;

            //Indice de la Declaración.
            n = ObtenerIDTag(initObj);
            VBAR = Format.StringToDouble(initObj.FrmxPln0.Tx_MtoDec[0].Text);
            VCAR = Format.StringToDouble(initObj.FrmxPln0.Tx_MtoDec[1].Text);
            VFAR = Format.StringToDouble(initObj.FrmxPln0.Tx_MtoDec[2].Text);
            VSAR = Format.StringToDouble(initObj.FrmxPln0.Tx_MtoDec[3].Text);
            VGAR = Format.StringToDouble(initObj.FrmxPln0.Tx_MtoDec[4].Text);
            VAAR = Format.StringToDouble(initObj.FrmxPln0.Tx_MtoDec[5].Text);
            VTAR = Format.StringToDouble(initObj.FrmxPln0.Tx_MtoDec[7].Text);

            //Se valida sólo si existe Declaración.
            if(!string.IsNullOrEmpty(initObj.FrmxPln0.Tx_NumDec.Text))
            {
                //Validaciones según disponible.
                s1 = "El monto correspondiente a @ no puede ser mayor que el disponible de la Declaración de Exportación.";
                string _switchVar1 = MODGPYF0.Componer(initObj.MODXPLN0.VxDatP.PrtExp, "~", "");
                if (_switchVar1 == initObj.MODXPLN0.VxDecP[n].PrtExp1)
                {
                    string controlName = string.Empty;
                    switch (Index)
                    {
                        case 0:  //Valor Bruto.
                            if (VBAR > initObj.MODXPLN0.VxDecP[n].ValRet1d)
                            {
                                s2 = "Valor Bruto";
                                controlName = "TxtValBrutoDeclaracion_Text";
                            }
                            break;
                        case 1:  //Comisiones.
                            if (VCAR > initObj.MODXPLN0.VxDecP[n].ValCom1d)
                            {
                                s2 = "Comisiones";
                                controlName = "TxtValComisionDeclaracion_Text";
                            }
                            break;
                    }

                    //Hay Errores en los montos ingresados.
                    if(!string.IsNullOrEmpty(s2))
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = MODGPYF0.Componer(s1, "@", s2),
                            Title = T_MODXPLN1.MsgxPlv,
                            Type = TipoMensaje.Informacion,
                            ControlName = controlName
                        });
                        initObj.FrmxPln0.Tx_MtoDec[Index].Text = initObj.FrmxPln0.ValAnt.ToString("0.00");
                        //initObj.FrmxPln0.Tx_MtoDec[Index].SetFocus();
                        return;
                    }

                    //Flete + Seguro debe ser menor que Gastos de la Declaración.-
                    if ((VFAR + VSAR) > initObj.MODXPLN0.VxDecP[n].ValGas1d && (Index == 2 || Index == 3) &&
                        Format.StringToDouble((initObj.FrmxPln0.Tx_MtoDec[Index].Text)) > 0)
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "El Valor de Flete + Seguro NO debe exceder el monto disponible del Item Otros Gastos de la Declaración. (" +
                            Format.FormatCurrency(initObj.MODXPLN0.VxDecP[n].ValGas1d,"#,###,###,###,##0.00") + ").",
                            Title = T_MODXPLN1.MsgxPlv,
                            Type = TipoMensaje.Informacion,
                            ControlName = "TxtValFleteDeclaracion_Text"
                        });

                        //Tx_MtoDec[Index].SetFocus();
                        return;
                    }

                }
                else if (_switchVar1 == initObj.MODXPLN0.VxDecP[n].PrtExp2)
                {
                    string controlName = string.Empty;
                    switch (Index)
                    {
                        case 0:  //Valor Bruto.
                            if (VBAR > initObj.MODXPLN0.VxDecP[n].ValRet2d)
                            {
                                s2 = "Valor Bruto";
                                controlName = "TxtValBrutoDeclaracion_Text";
                            }
                            break;
                        case 1:  //Comisiones.
                            if (VCAR > initObj.MODXPLN0.VxDecP[n].ValCom2d)
                            {
                                s2 = "Comisiones";
                                controlName = "TxtValComisionDeclaracion_Text";
                            }
                            break;
                    }

                    //Hay Errores en los montos ingresados.
                    if(!string.IsNullOrEmpty(s2))
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = MODGPYF0.Componer(s1, "@", s2),
                            Title = T_MODXPLN1.MsgxPlv,
                            Type = TipoMensaje.Informacion,
                            ControlName = controlName
                        });
                        initObj.FrmxPln0.Tx_MtoDec[Index].Text = initObj.FrmxPln0.ValAnt.ToString("0.00");
                        //Tx_MtoDec[Index].SetFocus();
                        return;
                    }

                    //Flete + Seguro debe ser menor que Gastos de la Declaración.-
                    if ((VFAR + VSAR) > initObj.MODXPLN0.VxDecP[n].ValGas2d && (Index == 2 || Index == 3) &&
                        Format.StringToDouble((initObj.FrmxPln0.Tx_MtoDec[Index].Text)) > 0)
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "El Valor de Flete + Seguro NO debe exceder el monto disponible del Item Otros Gastos de la Declaración. (" +
                            Format.FormatCurrency(initObj.MODXPLN0.VxDecP[n].ValGas1d,"#,###,###,###,##0.00") + ").",
                            Title = T_MODXPLN1.MsgxPlv,
                            Type = TipoMensaje.Informacion,
                            ControlName = "TxtValFleteDeclaracion_Text"
                        });
                        //Tx_MtoDec[Index].SetFocus();
                        return;
                    }

                }

            }

            //Valida que no se sobrepase del saldo.
            Saldo = GetSaldo(initObj);

            //Se calculan montos.
            VLAR = VBAR - VGAR;
            if ((int)initObj.FrmxPln0.Ch_Deduc[0].Value == 0)
            {
                //Comisiones.-
                VLAR -= VCAR;
            }

            if ((int)initObj.FrmxPln0.Ch_Deduc[1].Value == 0)
            {
                //Flete.-
                VLAR -= VFAR;
            }

            if ((int)initObj.FrmxPln0.Ch_Deduc[2].Value == 0)
            {
                //Seguro.-
                VLAR -= VSAR;
            }

            VO = (VLAR + VTAR);

            initObj.FrmxPln0.Tx_MtoDec[0].Text = Format.FormatCurrency(VBAR,"##,###0.00");
            initObj.FrmxPln0.Tx_MtoDec[1].Text = Format.FormatCurrency(VCAR,"##,###0.00");
            initObj.FrmxPln0.Tx_MtoDec[2].Text = Format.FormatCurrency(VFAR,"##,###0.00");
            initObj.FrmxPln0.Tx_MtoDec[3].Text = Format.FormatCurrency(VSAR,"##,###0.00");
            initObj.FrmxPln0.Tx_MtoDec[4].Text = Format.FormatCurrency(VGAR,"##,###0.00");
            initObj.FrmxPln0.Tx_MtoDec[5].Text = Format.FormatCurrency(VAAR,"##,###0.00");
            initObj.FrmxPln0.Tx_MtoDec[6].Text = Format.FormatCurrency(VLAR,"##,###0.00");
            initObj.FrmxPln0.Tx_MtoDec[7].Text = Format.FormatCurrency(VTAR,"##,###0.00");
            initObj.FrmxPln0.Tx_MtoDec[8].Text = Format.FormatCurrency(VO,"##,###0.00");

            //Copia los montos el los campos de las Planillas.
            Pr_Copiar_Montos(initObj);
        }

        public static void Tx_NumDec_LostFocus(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short n = 0;
            string z = "";
            string s = "";
            string fecha = "";
            short CodAdn = 0;

            //Verifica que si quiere abrir una declaración, esta exista.
            if(!string.IsNullOrWhiteSpace(initObj.FrmxPln0.Tx_NumDec.Text))
            {
                n = (short)(7 - VB6Helpers.Len(initObj.FrmxPln0.Tx_NumDec.Text));
                z = MODGPYF1.Zeros(n) + initObj.FrmxPln0.Tx_NumDec.Text;
                fecha = VB6Helpers.Format(initObj.FrmxPln0.Tx_FecDec.Text, "dd/MM/yyyy");
                CodAdn = (short)VB6Helpers.Val(initObj.FrmxPln0.Tx_CodAdn.Text);
                s = Mdl_Funciones.SyExis_xDec(uow, z, fecha, CodAdn);
                if (string.IsNullOrWhiteSpace(s))
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "La Declaración ingresada no se encuentra registrada. Debe ingresar una Declaración existente.",
                        Title = T_MODXPLN1.MsgxPlv,
                        Type = TipoMensaje.Informacion,
                        ControlName = "TxtNumeroDeclaracion_Text"
                    });

                    initObj.FrmxPln0.Tx_NumDec.Text = "";
                    initObj.FrmxPln0.Tx_FecDec.Text = "";
                    initObj.FrmxPln0.Tx_CodAdn.Text = "";
                }
                else
                {
                    initObj.FrmxPln0.Tx_NumDec.Text = z;
                    initObj.FrmxPln0.Tx_FecDec.Text = VB6Helpers.Format(MODGPYF0.copiardestring(s, ";", 2), "dd/MM/yyyy");
                    initObj.FrmxPln0.Tx_CodAdn.Text = MODGPYF0.copiardestring(s, ";", 3);
                }
            }
        }

        /// <summary>
        /// Indica si existe una planilla válida con el código dado.
        /// </summary>
        /// <returns></returns>
        private static bool ExistePln400(InitializationObject initObj)
        {
            short i = 0;

            for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODXPLN1.VxPlvs); i++)
            {
                if (T_MODXPLN1.PLN400.Contains(initObj.MODXPLN1.VxPlvs[i].TipPln.ToString()) && (
                    initObj.MODXPLN1.VxPlvs[i].Status != T_MODGCVD.EstadoEli))
                {
                    return true;
                }
            }

            return false;
        }

        //****************************************************************************
        //   1.  Retorna una linea para la lista de Planillas.
        //****************************************************************************
        private static string Fn_Linea_LtPln(InitializationObject initObj, string TipPln, short Indice, short TipMto)
        {
            short Correlativo = 0;
            string dec = "";
            string Ide = "";
            string s = "";

            switch (TipMto)
            {
                case 1:
                    initObj.FrmxPln0.ConMtoLiq = (short)(initObj.FrmxPln0.ConMtoLiq + 1);
                    Correlativo = initObj.FrmxPln0.ConMtoLiq;
                    break;
                case 2:
                    initObj.FrmxPln0.ConMtoInf = (short)(initObj.FrmxPln0.ConMtoInf + 1);
                    Correlativo = initObj.FrmxPln0.ConMtoInf;
                    break;
                case 3:
                    initObj.FrmxPln0.ConMtoEst = (short)(initObj.FrmxPln0.ConMtoEst + 1);
                    Correlativo = initObj.FrmxPln0.ConMtoEst;
                    break;
            }

            string _switchVar1 = VB6Helpers.Trim(VB6Helpers.UCase(TipPln));
            if (_switchVar1 == "V")
            {
                if (string.IsNullOrEmpty(initObj.MODXPLN1.VxPlvs[Indice].numdec))
                {
                    dec = "Sin Dec";
                    Ide = "Sin Ide";
                }
                else
                {
                    dec = MODGPYF1.Zeros((short)(VB6Helpers.Len(initObj.MODXPLN1.VxPlvs[Indice].numdec) - 7)) + initObj.MODXPLN1.VxPlvs[Indice].numdec;
                    dec = VB6Helpers.Left(dec, 6) + "-" + VB6Helpers.Right(dec, 1);
                    Ide = MODGPYF1.Zeros((short)(VB6Helpers.Len(initObj.MODXPLN1.VxPlvs[Indice].DieNum) - 7)) + initObj.MODXPLN1.VxPlvs[Indice].DieNum;
                    Ide = VB6Helpers.Left(Ide, 6) + "-" + VB6Helpers.Right(Ide, 1);
                }

                s = "";
                s = s + Correlativo.ToString("00") + " " + "VIS" + VB6Helpers.Chr(9);
                s = s + VB6Helpers.Trim(VB6Helpers.Str(initObj.MODXPLN1.VxPlvs[Indice].TipPln)) + VB6Helpers.Chr(9);
                s = s + dec + VB6Helpers.Chr(9);
                s = s + Ide + VB6Helpers.Chr(9);
                s = s + initObj.MODXPLN0.VxDatP.NemMnd + VB6Helpers.Chr(9);
                s += Format.FormatCurrency(initObj.MODXPLN1.VxPlvs[Indice].MtoLiq, UI_FrmxPln0.FormatoMtoPln).PadLeft(17);  //MODGPYF0.forma(initObj.MODXPLN1.VxPlvs[Indice].MtoLiq, UI_FrmxPln0.FormatoMtoPln);
                return s;
            }
            else if (_switchVar1 == "I")
            {
                if (string.IsNullOrEmpty(initObj.FrmxPln0.Tx_NumDec.Text))
                {
                    dec = "Sin Dec";
                    Ide = "Sin Ide";
                }
                else
                {
                    dec = MODGPYF1.Zeros((short)(VB6Helpers.Len(initObj.MODGPLI1.Vplis[Indice].numdec) - 7)) + initObj.MODGPLI1.Vplis[Indice].numdec;
                    dec = VB6Helpers.Left(dec, 6) + "-" + VB6Helpers.Right(dec, 1);
                    Ide = MODGPYF1.Zeros((short)(VB6Helpers.Len(initObj.MODGPLI1.Vplis[Indice].DieNum) - 7)) + initObj.MODGPLI1.Vplis[Indice].DieNum;
                    Ide = VB6Helpers.Left(Ide, 6) + "-" + VB6Helpers.Right(Ide, 1);
                }

                s = "";
                s = s + Correlativo.ToString("00") + " " + "INV" + VB6Helpers.Chr(9);
                s = s + VB6Helpers.Trim(VB6Helpers.Str(initObj.MODGPLI1.Vplis[Indice].TipPln)) + VB6Helpers.Chr(9);
                s = s + dec + VB6Helpers.Chr(9);
                s = s + Ide + VB6Helpers.Chr(9);
                s = s + initObj.MODXPLN0.VxDatP.NemMnd + VB6Helpers.Chr(9);
                s += MODGPYF0.forma(initObj.MODGPLI1.Vplis[Indice].MtoOpe, UI_FrmxPln0.FormatoMtoPln);
                return s;
            }

            return "";
        }

        //****************************************************************************
        //   1.  Valida los Campos relacionados con la Lista de Tipo de Planillas.
        //****************************************************************************
        private static bool Fn_Validar_Campos(short CampoInicial, short CampoFinal, InitializationObject initObj)
        {
            short i = 0;

            for (i = (short)CampoInicial; i <= (short)CampoFinal; i++)
            {
                string tipoPlanilla = initObj.FrmxPln0.LtTPln.get_List(initObj.FrmxPln0.LtTPln.ListIndex).Trim();
                if ((VB6Helpers.Mid(tipoPlanilla, 1, 1) == "4") ||
                    (VB6Helpers.Mid(tipoPlanilla, 1, 1) == "6"))
                {
                    switch (i)
                    {
                        case 1:  //Tx_PlzFin
                            if (VB6Helpers.Mid(tipoPlanilla, 1, 1) == "4")
                            {
                                if (string.IsNullOrWhiteSpace(initObj.FrmxPln0.Tx_PlzFin.Text))
                                {
                                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                                    {
                                        Text = "Atención: Es necesario que ingrese un Plazo para el Financiamiento.",
                                        Title = T_MODXPLN1.MsgxPlv,
                                        Type = TipoMensaje.Informacion,
                                        ControlName = "TxtPlazoFinanciamiento_Text"
                                    });

                                    return false;
                                }
                            }
                            break;
                        case 4:  //Cb_Bco
                            if (VB6Helpers.Mid(VB6Helpers.Trim(tipoPlanilla), 1, 1) == "6")
                            {
                                if (initObj.FrmxPln0.Cb_Bco.ListIndex == -1)
                                {
                                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                                    {
                                        Text = "Atención: Es necesario que seleccione un Banco Comercial.",
                                        Title = T_MODXPLN1.MsgxPlv,
                                        Type = TipoMensaje.Informacion,
                                        ControlName = "CmbBanco_SelectedValue"
                                    });

                                    return false;
                                }
                            }
                            break;
                        case 5:  //Cb_Pbc
                            if (VB6Helpers.Mid(VB6Helpers.Trim(tipoPlanilla), 1, 1) == "6")
                            {
                                if (initObj.FrmxPln0.Cb_Pbc.ListIndex == -1)
                                {
                                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                                    {
                                        Text = "Atención: Es necesario que seleccione una Plaza de Banco Central.",
                                        Title = T_MODXPLN1.MsgxPlv,
                                        Type = TipoMensaje.Informacion,
                                        ControlName = "CmbPlazaBancoCentral_SelectedValue"
                                    });

                                    return false;
                                }

                            }

                            //Cb_TipPln
                            if (VB6Helpers.Mid(VB6Helpers.Trim(tipoPlanilla), 1, 1) == "6")
                            {
                                if (initObj.FrmxPln0.Cb_Tippln.ListIndex == -1)
                                {
                                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                                    {
                                        Text = "Atención: Es necesario que seleccione un Tipo de Planilla.",
                                        Title = T_MODXPLN1.MsgxPlv,
                                        Type = TipoMensaje.Informacion,
                                        ControlName = "CmbTipoPlanillaInformar_SelectedValue"
                                    });

                                    return false;
                                }

                            }

                            break;
                    }

                }

            }

            return true;
        }

        /// <summary>
        /// Retorna el saldo del monto seleccionado.
        /// </summary>
        /// <param name="initObj"></param>
        /// <returns></returns>
        private static double GetSaldo(InitializationObject initObj)
        {
            short i = 0;

            i = (short)initObj.FrmxPln0.LtMto.get_ItemData(initObj.FrmxPln0.LtMto.ListIndex);
            switch (i)
            {
                case 1:
                    return initObj.MODXPLN0.VxDatP.MtoLiqs;
                case 2:
                    return initObj.MODXPLN0.VxDatP.MtoInfs;
                case 3:
                    return initObj.MODXPLN0.VxDatP.MtoTrans;
            }

            return 0;
        }

        public static void LtTPln_LostFocus(InitializationObject initObj)
        {
            string tipoPlanilla = initObj.FrmxPln0.LtTPln.get_List(initObj.FrmxPln0.LtTPln.ListIndex);
            if (tipoPlanilla == "401" || tipoPlanilla == "402")
            {
                //Fr_Sec.Enabled = true;
                HabilitarFr_Sec(initObj.FrmxPln0, true);
                //initObj.FrmxPln0.Cb_SecEcBen.SetFocus();
            }
            else
            {
                initObj.FrmxPln0.Cb_SecEcBen.ListIndex = -1;
                initObj.FrmxPln0.Cb_SecEcIn.ListIndex = -1;
                initObj.FrmxPln0.Tx_PrcPar.Text = "";
                
                //Fr_Sec.Enabled = false;
                HabilitarFr_Sec(initObj.FrmxPln0, false);
            }

        }

        public static void NO_Click(InitializationObject initObj)
        {
            short i = 0;
            short j = 0;
            short k = 0;
            short d = 0;
            short p = 0;
            short PLA = 0;
            string Obs = "";

            i = (short)initObj.FrmxPln0.LtPln.ListIndex;
            j = (short)initObj.FrmxPln0.LtPln.get_ItemData(i);

            if (j == -1)
            {
                return;
            }

            //Verifica si esta eliminando una planilla Visible.
            k = initObj.MODXPLN1.VPln[j].Indice;

            if (VB6Helpers.Trim(initObj.MODXPLN1.VPln[j].TipPln) == "V")
            {
                //TODO:@estanislao mover a .js
                //MsgBoxResult _switchVar1 = VB6Helpers.MsgBox("¿Está seguro que desea eliminar esta Planilla Visible?", (MsgBoxStyle)MODGPYF0.pito((short)(4 + 32 + 256)), MODXPLN1.MsgxPlv);
                //if ((int)_switchVar1 == 2 || (int)_switchVar1 == 7)
                //{
                //    //2 => Cancelar; 7 =>No.
                //    return;
                //}

                //Aumenta el saldo cuando existe la Dec. (disminuye el monto cancelado).
                d = ObtenerIDTag(initObj);
                p = initObj.MODXPLN1.VPln[initObj.FrmxPln0.LtPln.get_ItemData(initObj.FrmxPln0.LtPln.ListIndex)].IndPlv;
                RebajaSaldos(d, p, -1, initObj);
                MODXPLN0.GetDis_xDec(initObj.MODXPLN0, d);

                //Se le asigna un estado eliminada.
                initObj.MODXPLN1.VxPlvs[k].Status = T_MODGCVD.EstadoEli;
                initObj.MODXPLN1.VPln[j].Status = T_MODGCVD.EstadoEli;
                initObj.FrmxPln0.Tx_Obs.Text = "";

                if (T_MODXPLN1.PlnEst.Contains(PLA.ToString()) || T_MODXPLN1.PLNINF.Contains(PLA.ToString()))
                {
                    if (VB6Helpers.Left(initObj.FrmxPln0.LtMto.get_List(initObj.FrmxPln0.LtMto.ListIndex), 3) == "Liq")
                    {
                        if (VB6Helpers.Instr(initObj.FrmxPln0.Tx_Obs.Text, Obs) == 0)
                        {
                            initObj.FrmxPln0.Tx_Obs.Text = initObj.FrmxPln0.guardaobs;
                        }
                    }
                    else if (VB6Helpers.Left(initObj.FrmxPln0.LtMto.get_List(initObj.FrmxPln0.LtMto.ListIndex), 3) == "Tra")
                    {
                        if (VB6Helpers.Instr(initObj.FrmxPln0.Tx_Obs.Text, "Planilla de transferencia") == 0)
                        {
                            initObj.FrmxPln0.Tx_Obs.Text += " Planilla de transferencia.";
                        }
                    }
                    else
                    {
                        initObj.FrmxPln0.Tx_Obs.Text = initObj.FrmxPln0.guardaobs;
                    }
                }
                else
                {
                    initObj.FrmxPln0.Tx_Obs.Text = initObj.FrmxPln0.guardaobs;
                }

                if (i == initObj.FrmxPln0.LtPln.Items.Count - 1)
                {
                    p = 0;
                }
                else
                {
                    p = i;
                }

                initObj.FrmxPln0.LtPln.Items.RemoveAt(i);
                if (initObj.FrmxPln0.LtPln.Items.Count >= 0)
                {
                    initObj.FrmxPln0.LtPln.Items.Add(new UI_ListBoxItem
                    {
                        Value = "",
                        Data = 0
                    });
                }

                if (initObj.FrmxPln0.LtPln.ListIndex != p)
                {
                    initObj.FrmxPln0.LtPln.ListIndex = p;
                    FrmxPln0.LtPln_Click(initObj);
                }

                initObj.FrmxPln0.Boton[0].Enabled = false;
                initObj.FrmxPln0.Boton[1].Enabled = false;

                initObj.FrmxPln0.Cb_SecEcBen.ListIndex = -1;
                initObj.FrmxPln0.Cb_SecEcIn.ListIndex = -1;
                initObj.FrmxPln0.Tx_PrcPar.Text = "";

                //Fr_Sec.Enabled = false;
                HabilitarFr_Sec(initObj.FrmxPln0, false);

                return;
            }

            //Verifica si esta eliminando una planilla Invisible.
            if ((VB6Helpers.Trim(initObj.MODXPLN1.VPln[j].TipPln) == "I"))
            {
                //mover a .js
                //MsgBoxResult _switchVar2 = VB6Helpers.MsgBox("¿Está seguro que desea eliminar esta planilla Invisible?", (MsgBoxStyle)MODGPYF0.pito(291), MODXPLN1.MsgxPlv);
                //if ((int)_switchVar2 == 2 || (int)_switchVar2 == 7)
                //{
                //    //2 => Cancelar; 7 =>No.
                //    return;
                //}

                //Se le asigna un estado eliminada.
                initObj.MODGPLI1.Vplis[k].Status = T_MODGCVD.EstadoEli;
                initObj.MODXPLN1.VPln[j].Status = T_MODGCVD.EstadoEli;

                if (i == initObj.FrmxPln0.LtPln.Items.Count - 1)
                {
                    p = 0;
                }
                else
                {
                    p = i;
                }

                initObj.FrmxPln0.LtPln.Items.RemoveAt(i);
                if (initObj.FrmxPln0.LtPln.Items.Count == 0)
                {
                    initObj.FrmxPln0.LtPln.Items.Add(new UI_ListBoxItem
                    {
                        Value = "",
                        Data = 0
                    });
                }

                if (initObj.FrmxPln0.LtPln.ListIndex != p)
                {
                    initObj.FrmxPln0.LtPln.ListIndex = p;
                    FrmxPln0.LtPln_Click(initObj);
                }
            }

        }

        /// <summary>
        /// Habilita o deshabilita los controles del panel
        /// </summary>
        /// <param name="initObj"></param>
        /// <param name="habilitado"></param>
        private static void HabilitarFr_Sec(UI_FrmxPln0 frm, bool habilitado)
        {
            frm.Cb_SecEcIn.Enabled = habilitado;
            frm.Cb_SecEcBen.Enabled = habilitado;
            frm.Tx_PrcPar.Enabled = habilitado;
            frm.Tx_NomCom.Enabled = habilitado;
        }

        //Verifica que las Planillas estén bien generadas.-
        private static short PlanillasOK(InitializationObject initObj)
        {
            short i = 0;
            double suma = 0;
            double dif = 0;

            //Valores Líquidos.-
            for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODXPLN1.VxPlvs); i++)
            {
                if (initObj.MODXPLN1.VxPlvs[i].Status != T_MODGCVD.EstadoEli)
                {
                    suma += initObj.MODXPLN1.VxPlvs[i].MtoLiq;
                }

            }

            dif = (suma - (initObj.MODXPLN0.VxDatP.MtoLiq + initObj.MODXPLN0.VxDatP.MtoInf + initObj.MODXPLN0.VxDatP.mtotran));
            if (dif != 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Existe diferencias entre los montos de la Operación y los montos de las Planillas Generadas. Reporte este problema a la Unidad de Sistemas.",
                    Title = "Planillas Visibles de Exportaciones",
                    Type = TipoMensaje.Informacion
                });

                return 0;
            }

            //Información acerca del Cliente.-
            for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODXPLN1.VxPlvs); i++)
            {
                if (initObj.MODXPLN1.VxPlvs[i].Status != T_MODGCVD.EstadoEli)
                {
                    if (initObj.MODXPLN1.VxPlvs[i].PrtExp == "||||||||||||" || initObj.MODXPLN1.VxPlvs[i].RutExp == "0000000000")
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "Se ha generado una (o más) planilla(s) que NO tiene(n) información acerca del RUT del Exportador. Reporte este problema a la Unidad de Sistemas.",
                            Title = "Planillas Visibles de Exportaciones",
                            Type = TipoMensaje.Informacion
                        });

                        return 0;
                    }

                }

            }

            return (short)(true ? -1 : 0);
        }

        //****************************************************************************
        //   1.  Carga los Bancos en una determinada Combox.
        //****************************************************************************
        private static void Pr_Cargar_Bancos(InitializationObject initObj)
        {
            initObj.FrmxPln0.Cb_Bco.Items = initObj.MODGTAB0.VBco.Skip(1).Select(x => new UI_ComboItem
            {
                Value = x.NomBco,
                Data = x.CodBco
            }).ToList();

        }

        //****************************************************************************
        //   1.  Limpia los campos de la Planilla de esta Pantalla.
        //****************************************************************************
        private static void Pr_Cargar_Campos(InitializationObject initObj)
        {
            short j = 0;
            short n = 0;
            double Saldo = 0;
            string s = "";
            short m = 0;
            short i = 0;
            string Obs = "";
            short PLA = 0;
            short a = 0;
            short k = 0;

            //Indice en VxPlv().
            j = (short)initObj.FrmxPln0.LtPln.get_ItemData(initObj.FrmxPln0.LtPln.ListIndex);

            //Saldo.
            n = (short)initObj.FrmxPln0.LtMto.get_ItemData(initObj.FrmxPln0.LtMto.ListIndex);
            switch (n)
            {
                case 1: Saldo = initObj.MODXPLN0.VxDatP.MtoLiqs;
                    break;
                case 2: Saldo = initObj.MODXPLN0.VxDatP.MtoInfs;
                    break;
                case 3: Saldo = initObj.MODXPLN0.VxDatP.MtoTrans;
                    break;
            }

            initObj.FrmxPln0.Tx_MtoPln[0].Text = Saldo.ToString("0.00");

            //Tipos de Planillas.
            initObj.FrmxPln0.LtTPln.Items.Clear();
            initObj.FrmxPln0.LtTPln.ListIndex = -1;

            int _switchVar1 = initObj.FrmxPln0.LtMto.get_ItemData(initObj.FrmxPln0.LtMto.ListIndex);
            if (_switchVar1 == 1)
            {
                //Liquidar.-
                s = T_MODXPLN1.PLNLIQ;
                m = (short)VB6Helpers.Val(MODGPYF0.copiardestring(s, ";", 4));
            }
            else if (_switchVar1 == 2)
            {
                //Informar.-
                s = T_MODXPLN1.PLNINF;
                m = (short)VB6Helpers.Val(MODGPYF0.copiardestring(s, ";", 1));
            }
            else if (_switchVar1 == 3)
            {
                //Estadísticas.-
                s = T_MODXPLN1.PlnEst;
                m = (short)VB6Helpers.Val(MODGPYF0.copiardestring(s, ";", 4));
            }

            n = MODGPYF0.cuentadestring(s, ";");
            for (i = 1; i <= (short)n; i++)
            {
                initObj.FrmxPln0.LtTPln.Items.Add(new UI_ComboItem
                {
                    Value = MODGPYF0.copiardestring(s, ";", i),
                    Data = i
                });
            }

            for (i = 0; i <= (short)(initObj.FrmxPln0.LtTPln.Items.Count - 1); i++)
            {
                initObj.FrmxPln0.LtTPln.Items[i].Data = (int)VB6Helpers.Val(initObj.FrmxPln0.LtTPln.get_List(i));
            }

            if (initObj.FrmxPln0.LtTPln.ListIndex != MODGPYF0.PosLista(initObj.FrmxPln0.LtTPln, m))
            {
                initObj.FrmxPln0.LtTPln.ListIndex = MODGPYF0.PosLista(initObj.FrmxPln0.LtTPln, m);
                FrmxPln0.LtTPln_Click(initObj);
            }

            //Planilla NO existente.
            if (j < 0)
            {
                initObj.FrmxPln0.Boton[1].Enabled = false;
                initObj.FrmxPln0.Ok.Enabled = true;
                initObj.FrmxPln0.NO.Enabled = false;

                //if (!initObj.FrmxPln0.Tx_Obs.Enabled) 
                    HabilitarFr_Planillas(initObj.FrmxPln0, true);
                //if(!initObj.FrmxPln0.Tx_NumDec.Enabled) 
                    HabilitarFr_Declaracion(initObj.FrmxPln0, true);

                initObj.FrmxPln0.Tx_CodAdn.Text = "";
                initObj.FrmxPln0.Tx_NumDec.Text = "";
                initObj.FrmxPln0.Tx_FecDec.Text = "";
                for (i = 0; i <= 8; i++)
                {
                    initObj.FrmxPln0.Tx_MtoDec[i].Text = 0.ToString("0.00");
                }

                for (i = 1; i <= 4; i++)
                {
                    initObj.FrmxPln0.Tx_MtoPln[i].Text = 0.ToString("0.00");
                }

                initObj.FrmxPln0.Tx_MtoPln[5].Text = initObj.MODXPLN0.VxDatP.TipCam.ToString("0.0000");
                initObj.FrmxPln0.Tx_FecVen.Text = "";
                Obs = "Solo efectos estadisticos ";
                PLA = (short)VB6Helpers.Val(initObj.FrmxPln0.LtTPln.get_List(initObj.FrmxPln0.LtTPln.ListIndex));
                if (VB6Helpers.Instr(T_MODXPLN1.PlnEst, VB6Helpers.CStr(PLA)) > 0 || VB6Helpers.Instr(T_MODXPLN1.PLNINF, VB6Helpers.CStr(PLA)) > 0)
                {
                    if (VB6Helpers.Left(initObj.FrmxPln0.LtMto.get_List(initObj.FrmxPln0.LtMto.ListIndex), 3) == "Liq")
                    {
                        if (VB6Helpers.Instr(initObj.FrmxPln0.Tx_Obs.Text, Obs) == 0)
                        {
                            initObj.FrmxPln0.Tx_Obs.Text = initObj.FrmxPln0.guardaobs;
                        }

                    }
                    else if (VB6Helpers.Left(initObj.FrmxPln0.LtMto.get_List(initObj.FrmxPln0.LtMto.ListIndex), 3) == "Tra")
                    {
                        if (VB6Helpers.Instr(initObj.FrmxPln0.Tx_Obs.Text, "Planilla de transferencia") == 0)
                        {
                            initObj.FrmxPln0.Tx_Obs.Text += " Planilla de transferencia.";
                        }

                    }
                    else
                    {
                        initObj.FrmxPln0.Tx_Obs.Text = initObj.FrmxPln0.guardaobs;
                    }

                }
                else
                {
                    initObj.FrmxPln0.Tx_Obs.Text = initObj.FrmxPln0.guardaobs;
                }

                return;
            }
            else
            {
                initObj.FrmxPln0.NO.Enabled = true;
            }

            //Planilla existente.
            if (j >= 0)
            {
                initObj.FrmxPln0.Boton[1].Enabled = true;
                a = (short)initObj.FrmxPln0.LtPln.get_ItemData(initObj.FrmxPln0.LtPln.ListIndex);
                k = initObj.MODXPLN1.VPln[a].Indice;
                if (a >= 0)
                {
                    //Rescata datos de la Planilla Visible.
                    if ((VB6Helpers.Trim(initObj.MODXPLN1.VPln[a].TipPln) == "V"))
                    {
                        //Fr_Monto.Enabled = false;
                        HabilitarFr_Monto(initObj.FrmxPln0, false);


                        HabilitarFr_Planillas(initObj.FrmxPln0, false);
                        HabilitarFr_Declaracion(initObj.FrmxPln0, false);

                        if (initObj.MODXPLN1.VxPlvs[k].SecBen != 0 || initObj.MODXPLN1.VxPlvs[k].SecInv != 0 || !string.IsNullOrEmpty(initObj.FrmxPln0.Tx_PrcPar.Text))
                        {
                            HabilitarFr_Sec(initObj.FrmxPln0, true);

                            if (initObj.MODXPLN1.VxPlvs[k].SecBen != 0)
                            {
                                initObj.FrmxPln0.Cb_SecEcBen.ListIndex = MODGPYF0.PosLista(initObj.FrmxPln0.Cb_SecEcBen, initObj.MODXPLN1.VxPlvs[k].SecBen);
                            }
                            else
                            {
                                initObj.FrmxPln0.Cb_SecEcBen.ListIndex = -1;
                            }

                            if (initObj.MODXPLN1.VxPlvs[k].SecInv != 0)
                            {
                                initObj.FrmxPln0.Cb_SecEcIn.ListIndex = MODGPYF0.PosLista(initObj.FrmxPln0.Cb_SecEcIn, initObj.MODXPLN1.VxPlvs[k].SecInv);
                            }
                            else
                            {
                                initObj.FrmxPln0.Cb_SecEcIn.ListIndex = -1;
                            }

                            if (initObj.MODXPLN1.VxPlvs[k].PrcPar != 0)
                            {
                                initObj.FrmxPln0.Tx_PrcPar.Text = VB6Helpers.CStr(initObj.MODXPLN1.VxPlvs[k].PrcPar);
                            }
                            else
                            {
                                initObj.FrmxPln0.Tx_PrcPar.Text = "";
                            }

                        }

                        for (i = 0; i <= 8; i++)
                        {
                            initObj.FrmxPln0.Tx_MtoDec[i].Text = "";
                        }

                        initObj.FrmxPln0.Tx_CodAdn.Text = "";
                        initObj.FrmxPln0.Tx_NumDec.Text = "";
                        initObj.FrmxPln0.Tx_FecDec.Text = "";
                        for (i = 0; i <= 2; i++)
                        {
                            initObj.FrmxPln0.Ch_Deduc[i].Value = 0;
                            //FrmxPln0.Ch_Deduc_Click(i, initObj);
                        }

                        initObj.FrmxPln0.Tx_MtoPln[1].Text = initObj.MODXPLN1.VxPlvs[k].MtoBru.ToString("0.00");
                        initObj.FrmxPln0.Tx_MtoPln[2].Text = initObj.MODXPLN1.VxPlvs[k].MtoCom.ToString("0.00");
                        initObj.FrmxPln0.Tx_MtoPln[3].Text = initObj.MODXPLN1.VxPlvs[k].MtoOtg.ToString("0.00");
                        initObj.FrmxPln0.Tx_MtoPln[4].Text = initObj.MODXPLN1.VxPlvs[k].MtoLiq.ToString("0.00");
                        initObj.FrmxPln0.Tx_MtoPln[5].Text = initObj.MODXPLN1.VxPlvs[k].TipCam.ToString("0.00");
                        initObj.FrmxPln0.Tx_FecVen.Text = VB6Helpers.Trim(initObj.MODXPLN1.VxPlvs[k].FecVen);

                        if (initObj.MODXPLN1.VxPlvs[k].DfoCea != 0)
                        {
                            initObj.FrmxPln0.Cb_Bco.ListIndex = MODGPYF0.PosLista(initObj.FrmxPln0.Cb_Bco, initObj.MODXPLN1.VxPlvs[k].DfoCea);
                        }
                        else
                        {
                            initObj.FrmxPln0.Cb_Bco.ListIndex = -1;
                        }

                        if (initObj.MODXPLN1.VxPlvs[k].DfoCbc != 0)
                        {
                            initObj.FrmxPln0.Cb_Pbc.ListIndex = MODGPYF0.PosLista(initObj.FrmxPln0.Cb_Pbc, initObj.MODXPLN1.VxPlvs[k].DfoCbc);
                        }
                        else
                        {
                            initObj.FrmxPln0.Cb_Pbc.ListIndex = -1;
                        }

                        initObj.FrmxPln0.Cb_Pais.ListIndex = MODGPYF0.PosLista(initObj.FrmxPln0.Cb_Pais, initObj.MODXPLN1.VxPlvs[k].codpai);

                        if (initObj.MODXPLN1.VxPlvs[k].AfiVen != 0)
                        {
                            initObj.FrmxPln0.Tx_PlzFin.Text = VB6Helpers.Trim(VB6Helpers.Str(initObj.MODXPLN1.VxPlvs[k].AfiVen));
                        }
                        else
                        {
                            initObj.FrmxPln0.Tx_PlzFin.Text = "";
                        }

                        initObj.FrmxPln0.Tx_Obs.Text = VB6Helpers.Trim(initObj.MODXPLN1.VxPlvs[k].ObsPln);
                        if (initObj.FrmxPln0.LtTPln.ListIndex != 
                            MODGPYF0.PosLista(initObj.FrmxPln0.LtTPln, initObj.MODXPLN1.VxPlvs[k].TipPln))
                        {
                            initObj.FrmxPln0.LtTPln.ListIndex = MODGPYF0.PosLista(initObj.FrmxPln0.LtTPln, initObj.MODXPLN1.VxPlvs[k].TipPln);
                            LtTPln_Click(initObj);
                        }
                        initObj.FrmxPln0.Ok.Enabled = false;
                        return;
                    }

                    //Rescata datos de la Planilla Invisible.
                    if ((VB6Helpers.Trim(initObj.MODXPLN1.VPln[a].TipPln) == "I"))
                    {
                        HabilitarFr_Planillas(initObj.FrmxPln0, false);
                        HabilitarFr_Declaracion(initObj.FrmxPln0, false);

                        initObj.FrmxPln0.Tx_CodAdn.Text = VB6Helpers.Trim(VB6Helpers.Str(initObj.MODGPLI1.Vplis[k].CodAdn));
                        initObj.FrmxPln0.Tx_NumDec.Text = VB6Helpers.Trim(initObj.MODGPLI1.Vplis[k].numdec);
                        initObj.FrmxPln0.Tx_FecDec.Text = VB6Helpers.Trim(initObj.MODGPLI1.Vplis[k].FecDec);
                        for (i = 0; i <= 8; i++)
                        {
                            initObj.FrmxPln0.Tx_MtoDec[i].Text = "";
                        }

                        for (i = 0; i <= 2; i++)
                        {
                            initObj.FrmxPln0.Ch_Deduc[i].Value = 0;
                            //FrmxPln0.Ch_Deduc_Click(i, initObj);
                        }

                        initObj.FrmxPln0.Tx_MtoPln[1].Text = initObj.MODGPLI1.Vplis[k].MtoOpe.ToString("0.00");
                        initObj.FrmxPln0.Tx_MtoPln[4].Text = initObj.MODGPLI1.Vplis[k].MtoNac.ToString("0.00");
                        initObj.FrmxPln0.Tx_MtoPln[5].Text = initObj.MODGPLI1.Vplis[k].TipCam.ToString("0.00");
                        initObj.FrmxPln0.Tx_Obs.Text = VB6Helpers.Trim(initObj.MODGPLI1.Vplis[k].ObsPli);
                        if (initObj.FrmxPln0.LtTPln.ListIndex != MODGPYF0.PosLista(initObj.FrmxPln0.LtTPln, initObj.MODGPLI1.Vplis[k].TipPln))
                        {
                            initObj.FrmxPln0.LtTPln.ListIndex = MODGPYF0.PosLista(initObj.FrmxPln0.LtTPln, initObj.MODGPLI1.Vplis[k].TipPln);
                            FrmxPln0.LtTPln_Click(initObj);
                        }

                        initObj.FrmxPln0.Ok.Enabled = false;

                        return;
                    }

                }

            }

        }

        private static void HabilitarFr_Monto(UI_FrmxPln0 frm, bool habilitar)
        {
            frm.Tx_PlzFin.Enabled = habilitar;
            frm.Tx_NumPre.Enabled = habilitar;
            frm.Tx_FecPre.Enabled = habilitar;
            frm.Cb_Bco.Enabled = habilitar;
            frm.Cb_Pbc.Enabled = habilitar;
            frm.Cb_Tippln.Enabled = habilitar;
            frm.Tx_Fecha.Enabled = habilitar;
            frm.Tx_NumIns.Enabled = habilitar;
            frm.Cb_Pais.Enabled = habilitar;
        }

        /// <summary>
        /// Carga los montos de Liquidación, Informar y Estadística en una Lista
        /// de Montos (LtMto).
        /// </summary>
        /// <param name="initObj"></param>
        private static void Pr_Cargar_LtMto(InitializationObject initObj)
        {
            string s = "";

            if (initObj.MODXPLN0.VxDatP.MtoLiq > 0)
            {
                s = "Liq" + VB6Helpers.Chr(9) + initObj.MODXPLN0.VxDatP.NemMnd + VB6Helpers.Chr(9) +
                    MODGPYF0.forma(initObj.MODXPLN0.VxDatP.MtoLiq, UI_FrmxPln0.FormatoMtoPln);

                initObj.FrmxPln0.LtMto.Items.Add(new UI_ListBoxItem
                {
                    Value = s,
                    Data = 1
                });
            }

            if (initObj.MODXPLN0.VxDatP.MtoInf > 0)
            {
                s = "Inf" + VB6Helpers.Chr(9) + initObj.MODXPLN0.VxDatP.NemMnd + VB6Helpers.Chr(9) + 
                    MODGPYF0.forma(initObj.MODXPLN0.VxDatP.MtoInf, UI_FrmxPln0.FormatoMtoPln);
                initObj.FrmxPln0.LtMto.Items.Add(new UI_ListBoxItem
                {
                    Value = s,
                    Data = 2
                });
            }

            if (initObj.MODXPLN0.VxDatP.mtotran > 0)
            {
                s = "Tran" + VB6Helpers.Chr(9) + initObj.MODXPLN0.VxDatP.NemMnd + VB6Helpers.Chr(9) +
                    MODGPYF0.forma(initObj.MODXPLN0.VxDatP.mtotran, UI_FrmxPln0.FormatoMtoPln);

                initObj.FrmxPln0.LtMto.Items.Add(new UI_ListBoxItem
                {
                    Value = s,
                    Data = 3
                });
            }

        }

        /// <summary>
        /// Carga las Plazas de Banco Central.
        /// </summary>
        /// <param name="initObj"></param>
        private static void Pr_Cargar_Pbc(InitializationObject initObj)
        {
            initObj.FrmxPln0.Cb_Pbc.Items = initObj.MODGTAB1.VPbc.Skip(1).Select(x => new UI_ComboItem
            {
                Value = x.Pbc_PbcDes,
                Data = x.Pbc_PbcCod
            }).ToList();


            if (initObj.FrmxPln0.Cb_Pbc.Items.Count > 0)
            {
                initObj.FrmxPln0.Cb_Pbc.ListIndex = -1;
            }
        }

        /// <summary>
        /// Carga los tipos de Planillas que pueden existir en base al tipo 601.-
        /// </summary>
        /// <param name="initObj"></param>
        private static void Pr_Cargar_Tipo_Planilla(InitializationObject initObj)
        {
            initObj.FrmxPln0.Cb_Tippln.Items.Clear();

            initObj.FrmxPln0.Cb_Tippln.Items.AddRange(new UI_ComboItem[]{ 
                new UI_ComboItem
                {
                    Value = "401",
                    Data = 401
                }, new UI_ComboItem
                {
                    Value = "402",
                    Data = 402,
                },
                new UI_ComboItem
                {
                    Value = "403",
                    Data = 403,
                },
                new UI_ComboItem
                {
                    Value = "407",
                    Data = 407
                },
            });
        }

        /// <summary>
        /// Determina el número de Planillas a Informar y Estadísticas y
        /// efectúa el calculo de Comisiones que deja en la variable VCom_xPlv.-
        /// </summary>
        /// <param name="initObj"></param>
        private static void Pr_Comis_xPlv(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            int Can_inf;
            int Can_Est;
            double Tot_inf;
            double Tot_iva;
            double Val_Inf;
            double Iva_Inf;
            double Val_Est;
            double Iva_Est;
            short c = 0;
            short i = 0;
            double Mon_cob = 0;

            Can_inf = 0;
            Can_Est = 0;
            Tot_inf = 0;
            Tot_iva = 0;
            Val_Inf = 0;
            Iva_Inf = 0;
            Val_Est = 0;
            Iva_Est = 0;

            
            for (int a = 0; a <= VB6Helpers.UBound(initObj.MODXPLN1.VxPlvs); a++)
            {
                if (initObj.MODXPLN1.VxPlvs[a].Status != T_MODGCVD.EstadoEli)
                {
                    if (T_MODXPLN1.PLNINF.Contains(initObj.MODXPLN1.VxPlvs[a].TipPln.ToString()))
                    { 
                        Can_inf = Can_inf + 1;
                    }

                    if (initObj.MODXPLN1.VxPlvs[a].PlnEst != 0)
                    {
                        Can_Est = Can_Est + 1;
                    }
                }
            }

            if (Can_inf > 0)
            {
                MODGMTA.LlenaDatCob(initObj.MODGMTA, "", "SER", "PLN", "PPI", VB6Helpers.Format(DateTime.Now, "dd/MM/yyyy"), 
                    initObj.MODXPLN0.VxDatP.CodMnd, initObj.MODGSCE.VGen.MndNac, initObj.MODXPLN0.VxDatP.CodMnd, 0, 0, 0);
                c = MODGMTA.Cobrar(initObj.MODGMTA, initObj.MODGPYF0, initObj.MODGTAB0, initObj.MODGCHQ, initObj.Mdi_Principal,
                    initObj.Frm_Ingreso_Valores, uow);
                i = (short)VB6Helpers.UBound(initObj.MODGMTA.VCon);
                Tot_inf = initObj.MODGMTA.VCon[i].MtoCob;
                Tot_iva = initObj.MODGMTA.VCon[i].ivacon;
                Mon_cob = initObj.MODGMTA.VCon[i].MonCon;
            }

            if (Can_inf > 3)
            {
                MODGMTA.LlenaDatCob(initObj.MODGMTA, "", "SER", "PLN", "UPI", VB6Helpers.Format(DateTime.Now, "dd/MM/yyyy"), 
                    initObj.MODXPLN0.VxDatP.CodMnd, initObj.MODGSCE.VGen.MndNac, initObj.MODXPLN0.VxDatP.CodMnd, 0, 0, 0);
                c = MODGMTA.Cobrar(initObj.MODGMTA, initObj.MODGPYF0, initObj.MODGTAB0, initObj.MODGCHQ, initObj.Mdi_Principal,
                    initObj.Frm_Ingreso_Valores, uow);
                i = (short)VB6Helpers.UBound(initObj.MODGMTA.VCon);
                Val_Inf = initObj.MODGMTA.VCon[i].MtoCob * ((Can_inf) - 3);
                Iva_Inf = initObj.MODGMTA.VCon[i].ivacon * ((Can_inf) - 3);
                Mon_cob = initObj.MODGMTA.VCon[i].MonCon;
            }

            if ((Can_Est) > 0)
            {

                MODGMTA.LlenaDatCob(initObj.MODGMTA, "", "SER", "PLN", "CPE", VB6Helpers.Format(DateTime.Now, "dd/MM/yyyy"), 
                                    T_MODGTAB0.MndDol, initObj.MODGSCE.VGen.MndNac, initObj.MODXPLN0.VxDatP.CodMnd, 0, 0, 0);
                c = MODGMTA.Cobrar(initObj.MODGMTA, initObj.MODGPYF0, initObj.MODGTAB0, initObj.MODGCHQ, initObj.Mdi_Principal,
                    initObj.Frm_Ingreso_Valores, uow);
                i = (short)VB6Helpers.UBound(initObj.MODGMTA.VCon);
                Val_Est = initObj.MODGMTA.VCon[i].MtoCob * (Can_Est);
                Iva_Est = initObj.MODGMTA.VCon[i].ivacon * (Can_Est);
                Mon_cob = initObj.MODGMTA.VCon[i].MonCon;
            }
            //initObj.MODXPLN1.VCom_xPlv.MtoCom = Format.StringToDouble(VB6Helpers.Format(Tot_inf + Val_Inf + Val_Est, "0"));
            //initObj.MODXPLN1.VCom_xPlv.MtoIva = Format.StringToDouble(VB6Helpers.Format(Tot_iva + Iva_Inf + Iva_Est, "0"));
            initObj.MODXPLN1.VCom_xPlv.MtoCom = Format.StringToDouble(Math.Round(Tot_inf + Val_Inf + Val_Est));
            initObj.MODXPLN1.VCom_xPlv.MtoIva = Format.StringToDouble(Math.Round(Tot_iva + Iva_Inf + Iva_Est));
            initObj.MODXPLN1.VCom_xPlv.MtoTot = initObj.MODXPLN1.VCom_xPlv.MtoCom + initObj.MODXPLN1.VCom_xPlv.MtoIva;
            initObj.MODXPLN1.VCom_xPlv.MonCon = VB6Helpers.CShort(Mon_cob);
        }

        private static void Pr_Copiar_Montos(InitializationObject initObj)
        {
            double VBAR = 0;
            double VCAR = 0;
            double VFAR = 0;
            double VSAR = 0;
            double VGAR = 0;
            double VAAR = 0;
            double VLAR = 0;
            double VTAR = 0;
            double VOAR = 0;
            double VBP = 0;
            double VCP = 0;
            double VGP = 0;
            double VLP = 0;
            string Texto = "";
            string z = "";

            VBAR = (MODGPYF0.FDouble(Format.StringToDouble(initObj.FrmxPln0.Tx_MtoDec[0].Text), 2, initObj.MODGPYF0));
            VCAR = (MODGPYF0.FDouble(Format.StringToDouble(initObj.FrmxPln0.Tx_MtoDec[1].Text), 2, initObj.MODGPYF0));
            VFAR = (MODGPYF0.FDouble(Format.StringToDouble(initObj.FrmxPln0.Tx_MtoDec[2].Text), 2, initObj.MODGPYF0));
            VSAR = (MODGPYF0.FDouble(Format.StringToDouble(initObj.FrmxPln0.Tx_MtoDec[3].Text), 2, initObj.MODGPYF0));
            VGAR = (MODGPYF0.FDouble(Format.StringToDouble(initObj.FrmxPln0.Tx_MtoDec[4].Text), 2, initObj.MODGPYF0));
            VAAR = (MODGPYF0.FDouble(Format.StringToDouble(initObj.FrmxPln0.Tx_MtoDec[5].Text), 2, initObj.MODGPYF0));
            VLAR = (MODGPYF0.FDouble(Format.StringToDouble(initObj.FrmxPln0.Tx_MtoDec[6].Text), 2, initObj.MODGPYF0));
            VTAR = (MODGPYF0.FDouble(Format.StringToDouble(initObj.FrmxPln0.Tx_MtoDec[7].Text), 2, initObj.MODGPYF0));
            VOAR = (MODGPYF0.FDouble(Format.StringToDouble(initObj.FrmxPln0.Tx_MtoDec[8].Text), 2, initObj.MODGPYF0));

            //0 : BrutoPln = BrutoDec + ValorAjuste
            //1 : ComisPln = ComisDec
            //2 : GastoPln = FleteDec + SeguroDec + GastosDec
            //3 : LiquiPln = LiquiDec
            VBP = VBAR + VAAR;
            VCP = VCAR;
            VGP = 0;

            if ((int)initObj.FrmxPln0.Ch_Deduc[1].Value == 0)
            {
                //Flete.-
                VGP += VFAR;
            }

            if ((int)initObj.FrmxPln0.Ch_Deduc[2].Value == 0)
            {
                //Seguro.-
                VGP += VSAR;
            }
            VGP += VGAR;

            initObj.FrmxPln0.Tx_MtoPln[1].Text = Format.FormatCurrency(VBP,"##,###0.00");
            initObj.FrmxPln0.Tx_MtoPln[2].Text = Format.FormatCurrency(VCP,"##,###0.00");
            initObj.FrmxPln0.Tx_MtoPln[3].Text = Format.FormatCurrency(VGP,"##,###0.00");

            //Se calcula el Valor Líquido Planilla.
            VLP = VBP;

            //Comisiones.
            if (VCP != 0 && (int)initObj.FrmxPln0.Ch_Deduc[0].Value == 1)
            {
                Texto = "Liquidado en exceso por recuperación de Comisión.";
                if (VB6Helpers.Instr(initObj.FrmxPln0.Tx_Obs.Text, Texto) == 0)
                {
                    initObj.FrmxPln0.Tx_Obs.Text = initObj.FrmxPln0.Tx_Obs.Text + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                    initObj.FrmxPln0.Tx_Obs.Text = initObj.FrmxPln0.Tx_Obs.Text + initObj.MODXPLN0.VxDatP.NemMnd + " " + 
                        VB6Helpers.Trim(MODGPYF0.forma(VCP, UI_FrmxPln0.FormatoMtoPln)) + 
                        " " + Texto;
                }

            }
            else
            {
                VLP -= (VCP);
            }

            //Flete.
            if (VFAR != 0 && (int)initObj.FrmxPln0.Ch_Deduc[1].Value == 1)
            {
                Texto = "Liquidado en exceso por recuperación de Flete.";
                if (VB6Helpers.Instr(initObj.FrmxPln0.Tx_Obs.Text, Texto) == 0)
                {
                    initObj.FrmxPln0.Tx_Obs.Text = initObj.FrmxPln0.Tx_Obs.Text + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                    initObj.FrmxPln0.Tx_Obs.Text = initObj.FrmxPln0.Tx_Obs.Text + initObj.MODXPLN0.VxDatP.NemMnd + " " + 
                        VB6Helpers.Trim(MODGPYF0.forma(VFAR, UI_FrmxPln0.FormatoMtoPln)) + " " + Texto;
                }
            }
            else
            {
                VLP -= (VFAR);
            }

            //Seguro.
            if (VSAR != 0 && (int)initObj.FrmxPln0.Ch_Deduc[2].Value == 1)
            {
                Texto = "Liquidado en exceso por recuperación de Seguro.";
                if (VB6Helpers.Instr(initObj.FrmxPln0.Tx_Obs.Text, Texto) == 0)
                {
                    initObj.FrmxPln0.Tx_Obs.Text = initObj.FrmxPln0.Tx_Obs.Text + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                    initObj.FrmxPln0.Tx_Obs.Text = initObj.FrmxPln0.Tx_Obs.Text + initObj.MODXPLN0.VxDatP.NemMnd + " " +
                        VB6Helpers.Trim(MODGPYF0.forma(VSAR, UI_FrmxPln0.FormatoMtoPln)) + " " + Texto;
                }
            }
            else
            {
                VLP -= (VSAR);
            }

            //Gastos Corresponsal.
            VLP -= (VGAR);

            initObj.FrmxPln0.Tx_MtoPln[4].Text = VLP.ToString("0.00");

            //Observaciones.
            z = "Gastos Corresponsal ";
            if (VGAR > 0)
            {
                if (VB6Helpers.Instr(initObj.FrmxPln0.Tx_Obs.Text, z) == 0)
                {
                    if(!string.IsNullOrEmpty(initObj.FrmxPln0.Tx_Obs.Text))
                    {
                        initObj.FrmxPln0.Tx_Obs.Text = initObj.FrmxPln0.Tx_Obs.Text + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                    }
                    initObj.FrmxPln0.Tx_Obs.Text = initObj.FrmxPln0.Tx_Obs.Text + z + initObj.MODXPLN0.VxDatP.NemMnd + " " + 
                        VB6Helpers.Trim(MODGPYF0.forma(VGAR, UI_FrmxPln0.FormatoMtoPln)) + ".";
                }
            }

        }

        //****************************************************************************
        //   1.  Llena la lista LtPln con respecto al Tipo de Monto seleccionado antes.
        //****************************************************************************
        private static void Pr_Llena_LtPln(InitializationObject initObj)
        {
            short n = 0;
            short m = 0;
            short T = 0;
            short i = 0;
            short z = 0;
            short LNul = 0;

            //Cuantas Planillas generadas.
            n = (short)VB6Helpers.UBound(initObj.MODXPLN1.VxPlvs);
            m = (short)VB6Helpers.UBound(initObj.MODGPLI1.Vplis);

            initObj.MODXPLN1.VPln = new T_Pln[0];

            //Se obtienen las Planillas Visibles para el monto.
            T = (short)initObj.FrmxPln0.LtMto.get_ItemData(initObj.FrmxPln0.LtMto.ListIndex);

            initObj.FrmxPln0.LtPln.Items.Clear();
            initObj.FrmxPln0.LtPln.ListIndex = -1;

            initObj.FrmxPln0.ConMtoLiq = 0;
            initObj.FrmxPln0.ConMtoInf = 0; 
            initObj.FrmxPln0.ConMtoEst = 0;

            for (i = 0; i <= (short)n; i++)
            {
                if ((initObj.MODXPLN1.VxPlvs[i].TipMto == T) && (initObj.MODXPLN1.VxPlvs[i].Status != T_MODGCVD.EstadoEli))
                {
                    z = (short)(VB6Helpers.UBound(initObj.MODXPLN1.VPln) + 1);
                    VB6Helpers.RedimPreserve(ref initObj.MODXPLN1.VPln, 0, z);
                    initObj.MODXPLN1.VPln[z].TipPln = "V";
                    initObj.MODXPLN1.VPln[z].Indice = i;
                    initObj.MODXPLN1.VPln[z].Status = T_MODGCVD.EstadoIng;
                    initObj.MODXPLN1.VPln[z].IndPlv = i;
                    initObj.FrmxPln0.LtPln.Items.Add(new UI_ListBoxItem
                    {
                        Value = Fn_Linea_LtPln(initObj,"V", i, T),
                        Data = z
                    });
                }

            }

            //Se obtienen las Planillas Invisibles para el monto.
            T = (short)initObj.FrmxPln0.LtMto.get_ItemData(initObj.FrmxPln0.LtMto.ListIndex);
            for (i = 0; i <= (short)m; i++)
            {
                if ((initObj.MODGPLI1.Vplis[i].TipMto == T) && (initObj.MODGPLI1.Vplis[i].Status != T_MODGCVD.EstadoEli))
                {
                    z = (short)(VB6Helpers.UBound(initObj.MODXPLN1.VPln) + 1);
                    VB6Helpers.RedimPreserve(ref initObj.MODXPLN1.VPln, 0, z);
                    initObj.MODXPLN1.VPln[z].TipPln = "I";
                    initObj.MODXPLN1.VPln[z].Indice = i;
                    initObj.MODXPLN1.VPln[z].Status = T_MODGCVD.EstadoIng;
                    initObj.FrmxPln0.LtPln.Items.Add(new UI_ListBoxItem
                    {
                        Value = Fn_Linea_LtPln(initObj, "I", i, T),
                        Data = z
                    });
                }

            }

            //Se genera planilla nula.
            LNul = (short)(false ? -1 : 0);
            int _switchVar1 = initObj.FrmxPln0.LtMto.get_ItemData(initObj.FrmxPln0.LtMto.ListIndex);
            if (_switchVar1 == 1)
            {
                if (initObj.MODXPLN0.VxDatP.MtoLiqs > 0)
                {
                    LNul = (short)(true ? -1 : 0);
                }
            }
            else if (_switchVar1 == 2)
            {
                if (initObj.MODXPLN0.VxDatP.MtoInfs > 0)
                {
                    LNul = (short)(true ? -1 : 0);
                }
            }
            else if (_switchVar1 == 3)
            {
                if (initObj.MODXPLN0.VxDatP.MtoTrans > 0)
                {
                    LNul = (short)(true ? -1 : 0);
                }
            }

            if (LNul != 0)
            {
                initObj.FrmxPln0.LtPln.Items.Add(new UI_ListBoxItem
                {
                    Value="",
                    Data = -1
                });
            }

            if (initObj.FrmxPln0.LtPln.Items.Count > 0)
            {
                initObj.FrmxPln0.LtPln.Enabled = true;
                if (initObj.FrmxPln0.LtPln.ListIndex != initObj.FrmxPln0.LtPln.Items.Count - 1)
                {
                    initObj.FrmxPln0.LtPln.ListIndex = initObj.FrmxPln0.LtPln.Items.Count - 1;
                    FrmxPln0.LtPln_Click(initObj);
                }
            }
            else
            {
                initObj.FrmxPln0.LtPln.Enabled = false;
                //initObj.FrmxPln0.Fr_Declaracion.Enabled = false;
                HabilitarFr_Declaracion(initObj.FrmxPln0, false);

                //initObj.FrmxPln0.Fr_Planillas.Enabled = false;
                HabilitarFr_Planillas(initObj.FrmxPln0, false);
            }

        }

        private static void HabilitarFr_Declaracion(UI_FrmxPln0 frm, bool habilitar)
        {
            frm.Tx_NumDec.Enabled = habilitar;
            frm.Ok_Dec.Enabled = habilitar;
            frm.Tx_FecDec.Enabled = habilitar;
            frm.Tx_CodAdn.Enabled = habilitar;
            //frm.Tx_MtoDec.ForEach(x => x.Enabled = habilitar);
            frm.Tx_MtoDec[0].Enabled = habilitar;
            frm.Tx_MtoDec[1].Enabled = habilitar;
            frm.Tx_MtoDec[2].Enabled = habilitar;
            frm.Tx_MtoDec[3].Enabled = habilitar;
            frm.Tx_MtoDec[4].Enabled = habilitar;
            frm.Tx_MtoDec[7].Enabled = habilitar;
            frm.Ch_Deduc.ForEach(x => x.Enabled = habilitar);
        }

        private static void HabilitarFr_Planillas(UI_FrmxPln0 frm, bool habilitar)
        {
            //frm.Tx_MtoPln.ForEach(x => x.Enabled = habilitar);
            frm.LtTPln.Enabled = habilitar;
            //frm.Tx_FecDec.Enabled = habilitar;
            frm.Tx_Obs.Enabled = habilitar;
        }

        /// <summary>
        ///    1.  Rebaja los saldos de las Declaraciones y del Monto Disponible.
        ///    2.  Signo :    Indica si se rebaja  el saldo (+1:aumenta   lo cancelado)
        ///                   Indica si se aumenta el saldo (-1:disminuye lo cancelado)
        /// </summary>
        /// <param name="IDec"></param>
        /// <param name="IPln"></param>
        /// <param name="signo"></param>
        /// <param name="initObj"></param>
        private static void RebajaSaldos(short IDec, short IPln, short signo, InitializationObject initObj)
        {
            short n = 0;
            short m = 0;
            string p = "";
            int i = 0;
            n = IDec;
            m = IPln;

            //Sólo se rebaja cuando existe la Declaración.
            if ((IDec) >= 0)
            {
                //Rebaja Saldo Dec. Exportador 1.
                p = initObj.Module1.PartysOpe[initObj.MODXPLN0.VxDatP.IndPrt].LlaveArchivo;
                p = MODGPYF0.Componer(p, "~", "");
                if (p == initObj.MODXPLN0.VxDecP[n].PrtExp1 &&
                    initObj.Module1.PartysOpe[initObj.MODXPLN0.VxDatP.IndPrt].IndNombre == initObj.MODXPLN0.VxDecP[n].IndNom1)
                {
                    initObj.MODXPLN0.VxDecP[n].ValRet1c = initObj.MODXPLN0.VxDecP[n].ValRet1c + 
                        (signo * initObj.MODXPLN1.VxPlvs[m].ValRet);
                    initObj.MODXPLN0.VxDecP[n].ValCom1c = 
                        initObj.MODXPLN0.VxDecP[n].ValCom1c + (signo * initObj.MODXPLN1.VxPlvs[m].ValCom);
                    initObj.MODXPLN0.VxDecP[n].ValGas1c = 
                        initObj.MODXPLN0.VxDecP[n].ValGas1c + (signo * initObj.MODXPLN1.VxPlvs[m].ValFle + signo *
                        initObj.MODXPLN1.VxPlvs[m].ValSeg);
                    initObj.MODXPLN0.VxDecP[n].ValLiq1c = initObj.MODXPLN0.VxDecP[n].ValRet1c -
                        (initObj.MODXPLN0.VxDecP[n].ValCom1c + initObj.MODXPLN0.VxDecP[n].ValGas1c);
                }
                else
                {
                    //Rebaja Saldo Dec. Exportador 2.
                    if (p == initObj.MODXPLN0.VxDecP[n].PrtExp2 &&
                        initObj.Module1.PartysOpe[initObj.MODXPLN0.VxDatP.IndPrt].IndNombre == initObj.MODXPLN0.VxDecP[n].IndNom2)
                    {
                        initObj.MODXPLN0.VxDecP[n].ValRet2c = 
                            initObj.MODXPLN0.VxDecP[n].ValRet2c + (signo * initObj.MODXPLN1.VxPlvs[m].ValRet);
                        initObj.MODXPLN0.VxDecP[n].ValCom2c = 
                            initObj.MODXPLN0.VxDecP[n].ValCom2c + (signo * initObj.MODXPLN1.VxPlvs[m].ValCom);
                        initObj.MODXPLN0.VxDecP[n].ValGas2c = 
                            initObj.MODXPLN0.VxDecP[n].ValGas2c + (signo * initObj.MODXPLN1.VxPlvs[m].ValFle +
                            signo * initObj.MODXPLN1.VxPlvs[m].ValSeg);
                        initObj.MODXPLN0.VxDecP[n].ValLiq2c = initObj.MODXPLN0.VxDecP[n].ValRet2c - (initObj.MODXPLN0.VxDecP[n].ValCom2c +
                            initObj.MODXPLN0.VxDecP[n].ValGas2c);
                    }
                    else
                    {
                        initObj.MODXPLN0.VxDecP[n].ValRet1c = 
                            initObj.MODXPLN0.VxDecP[n].ValRet1c + (signo * initObj.MODXPLN1.VxPlvs[m].ValRet);
                        initObj.MODXPLN0.VxDecP[n].ValCom1c = 
                            initObj.MODXPLN0.VxDecP[n].ValCom1c + (signo * initObj.MODXPLN1.VxPlvs[m].ValCom);
                        initObj.MODXPLN0.VxDecP[n].ValGas1c = (
                            initObj.MODXPLN0.VxDecP[n].ValGas1c) + (signo * initObj.MODXPLN1.VxPlvs[m].ValFle +
                            signo * initObj.MODXPLN1.VxPlvs[m].ValSeg);
                        initObj.MODXPLN0.VxDecP[n].ValLiq2c = initObj.MODXPLN0.VxDecP[n].ValRet2c - (initObj.MODXPLN0.VxDecP[n].ValCom2c +
                            initObj.MODXPLN0.VxDecP[n].ValGas2c);
                    }

                }

            }

            //Rebaja Saldo de Planillas.
            i = initObj.FrmxPln0.LtMto.get_ItemData(initObj.FrmxPln0.LtMto.ListIndex);
            switch (i)
            {
                case 1:
                    initObj.MODXPLN0.VxDatP.MtoLiqs = Format.StringToDouble(initObj.FrmxPln0.Tx_MtoPln[0].Text) -
                        (signo * Format.StringToDouble(initObj.FrmxPln0.Tx_MtoPln[4].Text));
                    break;
                case 2:
                    initObj.MODXPLN0.VxDatP.MtoInfs = Format.StringToDouble(initObj.FrmxPln0.Tx_MtoPln[0].Text) -
                        (signo * Format.StringToDouble(initObj.FrmxPln0.Tx_MtoPln[4].Text));
                    break;
                case 3:
                    initObj.MODXPLN0.VxDatP.MtoTrans = Format.StringToDouble(initObj.FrmxPln0.Tx_MtoPln[0].Text) -
                        (signo * Format.StringToDouble(initObj.FrmxPln0.Tx_MtoPln[4].Text));
                    break;
            }

        }

    }
}
