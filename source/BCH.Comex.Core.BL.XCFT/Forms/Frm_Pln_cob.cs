using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public static class Frm_Pln_cob
    {
        public static void Form_Load(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            // UPGRADE_INFO (#05B1): The 'Tabs' variable wasn't declared explicitly.
            short[] Tabs = null;
            short ProxIndice = 0;
            Tabs = new short[8];
            Tabs[0] = (short)7.75;
            Tabs[1] = (short)23.25;
            Tabs[2] = (short)100.75;
            Tabs[3] = (short)178.5;
            Tabs[4] = (short)224.75;
            Tabs[5] = 279;
            Tabs[6] = (short)294.5;

            //Deshabilita botones en caso de haber solo una planilla.-
            //---------------------------------------------------------
            //if (VB6Helpers.UBound(initObj.MODPREEM.Vx_PReem) == 1)
           if(initObj.MODPREEM.Vx_PReem.Length <= 1)
            {
                initObj.Frm_Pln_cob.Bot_Sig.Enabled = false;
                initObj.Frm_Pln_cob.Bot_Ant.Enabled = false;
            }

            //Limpiamos los campos.-
            //------------------------
            Pr_LimpiaPlan(initObj);

            //Obtenemos la primera planilla no eliminada.-
            //--------------------------------------------
            initObj.Frm_Pln_cob.Bot_Ant.Tag = Fn_GetPlanProx(0, 1, initObj);
            initObj.MODGCHQ.Indice = (short)VB6Helpers.Val(initObj.Frm_Pln_cob.Bot_Ant.Tag);

            //Mostramos los datos en pantalla.-
            //---------------------------------
            Pr_GetDatosPl(initObj.MODGCHQ.Indice, initObj,uow);

            //Vemos si hay mas planillas para habilitar o deshab. los botones.-
            //-----------------------------------------------------------------
            ProxIndice = Fn_GetPlanProx(initObj.MODGCHQ.Indice, 1, initObj);
            if (ProxIndice == initObj.MODGCHQ.Indice)
            {
                initObj.Frm_Pln_cob.Bot_Sig.Enabled = false;
            }
            else
            {
                initObj.Frm_Pln_cob.Bot_Sig.Enabled = true;
            }

            //Porque es la primera planilla =>
            //---------------------------------
            initObj.Frm_Pln_cob.Bot_Ant.Enabled = false;
        }

        //Muestra los datos de la planilla en los label.-
        public static void Pr_GetDatosPl(short Indice, InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short CodMon = 0;
            if (Indice >= 0 && initObj.MODPREEM.Vx_PReem[Indice].Estado == 1)
            {
                //Numero de Planilla
                //------------------------
                initObj.Frm_Pln_cob.Lb_NumPre.Text = VB6Helpers.Format(VB6Helpers.CStr(initObj.MODPREEM.Vx_PReem[Indice].NumPla), "000000");
                //Fecha de Venta
                //------------------------
                initObj.Frm_Pln_cob.Tx_FecVen.Text = initObj.MODPREEM.Vx_PReem[Indice].FecVen;

                //Habilita o deshabilita campo fecha venta
                //----------------------------------------------
                if (initObj.MODPREEM.Vx_PReem[Indice].IndAnu == 3)
                {
                    initObj.Frm_Pln_cob.Tx_FecVen.Enabled = true;
                }
                else
                {
                    initObj.Frm_Pln_cob.Tx_FecVen.Enabled = false;
                }

                //Plaza Banco Central
                //------------------------
                initObj.Frm_Pln_cob.Lb_NomPla.Text = initObj.MODPREEM.Vx_PReem[Indice].NomPlz;

                initObj.Frm_Pln_cob.Lb_CodPla.Text = VB6Helpers.CStr(initObj.MODPREEM.Vx_PReem[Indice].CodBch);
                //Importador
                //------------------------
                initObj.Frm_Pln_cob.Lb_NomImp.Text = initObj.MODPREEM.Vx_PReem[Indice].NomImp;
                initObj.Frm_Pln_cob.Lb_RutImp.Text = MODGFYS.Rut_Formateado(initObj.MODPREEM.Vx_PReem[Indice].RutImp);
                //Informe de Importacion
                //------------------------

                initObj.Frm_Pln_cob.Lb_NumIdi.Text = initObj.MODPREEM.Vx_PReem[Indice].numdec;
                initObj.Frm_Pln_cob.Lb_FecIdi.Text = VB6Helpers.Format(initObj.MODPREEM.Vx_PReem[Indice].FecDec, "dd/MM/yyyy");

                initObj.Frm_Pln_cob.Lb_PagIdi.Text = VB6Helpers.Format(VB6Helpers.CStr(initObj.MODPREEM.Vx_PReem[Indice].CodPag), "00");
                //Conocimiento de Embarque
                //------------------------
                initObj.Frm_Pln_cob.Lb_NumCon.Text = initObj.MODPREEM.Vx_PReem[Indice].NumCon;
                initObj.Frm_Pln_cob.Lb_FecCon.Text = VB6Helpers.Format(initObj.MODPREEM.Vx_PReem[Indice].FecCon, "dd/MM/yyyy");
                //Fecha vencimiento
                //------------------------
                //------------------------
                if (initObj.MODPREEM.Vx_PReem[Indice].NumCua != 0)
                {
                    initObj.Frm_Pln_cob.Lb_NumCua.Text = VB6Helpers.CStr(initObj.MODPREEM.Vx_PReem[Indice].NumCua);
                }

                if (initObj.MODPREEM.Vx_PReem[Indice].numcuo != 0)
                {
                    initObj.Frm_Pln_cob.Lb_NumCuo.Text = VB6Helpers.CStr(initObj.MODPREEM.Vx_PReem[Indice].numcuo);
                }

                //Acuerdo
                //------------------------
                if (initObj.MODPREEM.Vx_PReem[Indice].NumAcu != 0)
                {
                    initObj.Frm_Pln_cob.Lb_NumAcu.Text = VB6Helpers.CStr(initObj.MODPREEM.Vx_PReem[Indice].NumAcu);
                }

                if(!string.IsNullOrEmpty(initObj.MODPREEM.Vx_PReem[Indice].Acuer1))
                {
                    initObj.Frm_Pln_cob.Lb_Acuer1.Text = initObj.MODPREEM.Vx_PReem[Indice].Acuer1;
                }

                if(!string.IsNullOrEmpty(initObj.MODPREEM.Vx_PReem[Indice].Acuer2))
                {
                    initObj.Frm_Pln_cob.Lb_Acuer2.Text = initObj.MODPREEM.Vx_PReem[Indice].Acuer2;
                }

                //Pais de Pago
                //------------------------
                initObj.Frm_Pln_cob.Lb_NomPai.Text = initObj.MODPREEM.Vx_PReem[Indice].NomPai;
                initObj.Frm_Pln_cob.Lb_CodPai.Text = VB6Helpers.CStr(initObj.MODPREEM.Vx_PReem[Indice].CodPPa);
                //Moneda
                //------------------------
                initObj.Frm_Pln_cob.Lb_NomMon.Text = initObj.MODPREEM.Vx_PReem[Indice].NomMon;
                initObj.Frm_Pln_cob.Lb_CodMon.Text = VB6Helpers.CStr(initObj.MODPREEM.Vx_PReem[Indice].CodMPa);
                CodMon = initObj.MODPREEM.Vx_PReem[Indice].CodMon;
                initObj.Frm_Pln_cob.Nemonico = MODGTAB0.Get_NemMnd(initObj.MODGTAB0,uow, CodMon);
                //Montos
                //------------------------
                initObj.Frm_Pln_cob.Lb_MtoFob.Text = Utils.Format.FormatCurrency(initObj.MODPREEM.Vx_PReem[Indice].MtoFob, initObj.MODANUVI.FtoSal);
                initObj.Frm_Pln_cob.Lb_MtoFle.Text = Utils.Format.FormatCurrency(initObj.MODPREEM.Vx_PReem[Indice].MtoFle, initObj.MODANUVI.FtoSal);
                initObj.Frm_Pln_cob.Lb_MtoSeg.Text = Utils.Format.FormatCurrency(initObj.MODPREEM.Vx_PReem[Indice].MtoSeg, initObj.MODANUVI.FtoSal);
                initObj.Frm_Pln_cob.Lb_MtoCif.Text = Utils.Format.FormatCurrency(initObj.MODPREEM.Vx_PReem[Indice].MtoCif, initObj.MODANUVI.FtoSal);
                initObj.Frm_Pln_cob.Lb_ValTot.Text = Utils.Format.FormatCurrency(initObj.MODPREEM.Vx_PReem[Indice].TotOri, initObj.MODANUVI.FtoSal);
                initObj.Frm_Pln_cob.Lb_CifDol.Text = Utils.Format.FormatCurrency(initObj.MODPREEM.Vx_PReem[Indice].CifDol, T_MODGCON0.FormatoConDec);
                initObj.Frm_Pln_cob.Lb_TotDol.Text = Utils.Format.FormatCurrency(initObj.MODPREEM.Vx_PReem[Indice].TotDol, T_MODGCON0.FormatoConDec);
                //Tipo Cambio y Paridad
                //------------------------
                initObj.Frm_Pln_cob.Lb_TipCam.Text = VB6Helpers.CStr(initObj.MODPREEM.Vx_PReem[Indice].TipCam);
                initObj.Frm_Pln_cob.Lb_ParPag.Text = VB6Helpers.CStr(initObj.MODPREEM.Vx_PReem[Indice].ParPag);

                //--------------------------------------------
                //Planilla Reemplazada.-
                initObj.Frm_Pln_cob.Lb_NumPlnR.Text = VB6Helpers.Format(VB6Helpers.CStr(initObj.MODPREEM.Vx_PReem[Indice].NumPln_R), "000000");
                initObj.Frm_Pln_cob.Lb_FecPlnR.Text = VB6Helpers.Format(initObj.MODPREEM.Vx_PReem[Indice].FecPln_R, "dd/MM/yyyy");

                initObj.Frm_Pln_cob.Lb_CodPlzR.Text = VB6Helpers.Format(VB6Helpers.CStr(initObj.MODPREEM.Vx_PReem[Indice].CodPlz_R), "00");
                initObj.Frm_Pln_cob.Lb_CodEntR.Text = VB6Helpers.CStr(initObj.MODPREEM.Vx_PReem[Indice].CodEnt_R);
                initObj.Frm_Pln_cob.Lb_NumConR.Text = initObj.MODPREEM.Vx_PReem[Indice].NumCon_R;
                initObj.Frm_Pln_cob.Lb_FecConR.Text = initObj.MODPREEM.Vx_PReem[Indice].FecCon_R;
                //Numero declaracion.-
                //------------------------
                if(!string.IsNullOrEmpty(initObj.MODPREEM.Vx_PReem[Indice].FecDeb))
                {
                    initObj.Frm_Pln_cob.Lb_FecDeb.Text = VB6Helpers.Format(initObj.MODPREEM.Vx_PReem[Indice].FecDeb, "dd/MM/yyyy");
                }

                if(!string.IsNullOrEmpty(initObj.MODPREEM.Vx_PReem[Indice].DocChi))
                {
                    initObj.Frm_Pln_cob.Lb_DocChi.Text = initObj.MODPREEM.Vx_PReem[Indice].DocChi;
                }

                if(!string.IsNullOrEmpty(initObj.MODPREEM.Vx_PReem[Indice].DocExt))
                {
                    initObj.Frm_Pln_cob.Lb_DocExt.Text = initObj.MODPREEM.Vx_PReem[Indice].DocExt;
                }

                //Observaciones
                //------------------------
                initObj.Frm_Pln_cob.Tx_Observ.Text = initObj.MODPREEM.Vx_PReem[Indice].observ;

                if (initObj.MODPREEM.Vx_PReem[Indice].ZonFra != 0)
                {
                    initObj.Frm_Pln_cob.Ch_ZonFra.Value = (short)1;
                }
                else
                {
                    initObj.Frm_Pln_cob.Ch_ZonFra.Value = 0;
                }

            }

        }

        //Busca la posicion de una planilla no eliminada dentro del arreglo
        //dependiendo de la accion : -1 => Retroceder y 1 => Avanzar)
        private static short Fn_GetPlanProx(short IndActual, short Accion, InitializationObject initObj)
        {
            short IndPlan = (short)(IndActual + Accion);
            while (true)
            {

                //if (IndPlan == 0 || IndPlan > VB6Helpers.UBound(initObj.MODPREEM.Vx_PReem))
                if (IndPlan == 0 || IndPlan > (initObj.MODPREEM.Vx_PReem.Length-1)) //se resta uno ya que la funcion Ubound entrega el mayor indice
                {
                    return IndActual;
                }
                else
                {
                    if (initObj.MODPREEM.Vx_PReem[IndPlan].Estado == 1)
                    {
                        //Si no esta eliminada.-
                        return IndPlan;
                    }
                    else
                    {
                        IndPlan = (short)(IndPlan + Accion);
                    }

                }

            }

            //return 0;
        }


        private static void Pr_LimpiaPlan(InitializationObject initObj)
        {

            initObj.Frm_Pln_cob.Lb_NumPre.Text = "";
            initObj.Frm_Pln_cob.Lb_NomPla.Text = "";
            initObj.Frm_Pln_cob.Lb_CodPla.Text = "";
            initObj.Frm_Pln_cob.Tx_FecVen.Text = "";
            initObj.Frm_Pln_cob.Lb_NomImp.Text = "";
            initObj.Frm_Pln_cob.Lb_RutImp.Text = "";
            initObj.Frm_Pln_cob.Lb_NumIdi.Text = "";
            initObj.Frm_Pln_cob.Lb_FecIdi.Text = "";

            initObj.Frm_Pln_cob.Lb_PagIdi.Text = "";
            initObj.Frm_Pln_cob.Lb_NumCon.Text = "";
            initObj.Frm_Pln_cob.Lb_FecCon.Text = "";
            
            initObj.Frm_Pln_cob.Lb_NumCua.Text = "";
            initObj.Frm_Pln_cob.Lb_NumCuo.Text = "";
            initObj.Frm_Pln_cob.Lb_NumAcu.Text = "";
            initObj.Frm_Pln_cob.Lb_Acuer1.Text = "";
            initObj.Frm_Pln_cob.Lb_Acuer2.Text = "";
            initObj.Frm_Pln_cob.Lb_NomPai.Text = "";
            initObj.Frm_Pln_cob.Lb_CodPai.Text = "";
            initObj.Frm_Pln_cob.Lb_NomMon.Text = "";
            initObj.Frm_Pln_cob.Lb_CodMon.Text = "";
            
            initObj.Frm_Pln_cob.Lb_MtoFob.Text = "";
            initObj.Frm_Pln_cob.Lb_MtoSeg.Text = "";
            initObj.Frm_Pln_cob.Lb_MtoCif.Text = "";
            initObj.Frm_Pln_cob.Lb_ValTot.Text = "";
            initObj.Frm_Pln_cob.Lb_CifDol.Text = "";
            initObj.Frm_Pln_cob.Lb_TipCam.Text = "";
            initObj.Frm_Pln_cob.Lb_ParPag.Text = "";

            //Planilla Reemplazada.-
            initObj.Frm_Pln_cob.Lb_NumPlnR.Text = "";
            initObj.Frm_Pln_cob.Lb_FecPlnR.Text = "";
            initObj.Frm_Pln_cob.Lb_CodPlzR.Text = "";
            initObj.Frm_Pln_cob.Lb_CodEntR.Text = "";
            initObj.Frm_Pln_cob.Lb_NumConR.Text = "";
            initObj.Frm_Pln_cob.Lb_FecConR.Text = "";
            
            //Convenio credito reciproco.-
            initObj.Frm_Pln_cob.Lb_FecDeb.Text = "";
            initObj.Frm_Pln_cob.Lb_DocChi.Text = "";
            initObj.Frm_Pln_cob.Lb_DocExt.Text = "";
            initObj.Frm_Pln_cob.Tx_Observ.Text = "";
            initObj.Frm_Pln_cob.Ch_ZonFra.Value = (short)(false ? -1 : 0);
        }

        public static void Bot_Acepta_Click(InitializationObject initObj)
        {
            //initObj.
            //VB6Helpers.Unload(this);
        }

        //Retrocedemos una planilla.-
        public static void Bot_Ant_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short IndActual;
            short Indice;
            //Limpiamos los campos.-
            Pr_LimpiaPlan(initObj);

            //Vemos si hay planillas anteriores
            IndActual = (short)VB6Helpers.Val(initObj.Frm_Pln_cob.Bot_Ant.Tag);
            Indice = Fn_GetPlanProx(IndActual, -1,initObj);

            //Habilita o deshabilita botones
            if (IndActual == Indice)
            {
                //<= Si no hay mas que la actual.-
                initObj.Frm_Pln_cob.Bot_Ant.Enabled = false;
            }
            else
            {
                initObj.Frm_Pln_cob.Bot_Ant.Enabled = true;
            }

            //Traemos los datos.-
            initObj.Frm_Pln_cob.Bot_Ant.Tag = Indice;
            Pr_GetDatosPl(Indice, initObj,uow);

            //Activamos o desactivamos el boton para ver planillas anteriores.-
            if (Fn_GetPlanProx(Indice, -1, initObj) == Indice)
            {
                initObj.Frm_Pln_cob.Bot_Ant.Enabled = false;
            }

            //Activamos o desactivamos el boton para ver planillas siguientes.-
            if (Fn_GetPlanProx(Indice, 1,initObj) == Indice)
            {
                initObj.Frm_Pln_cob.Bot_Sig.Enabled = false;
            }
            else
            {
                initObj.Frm_Pln_cob.Bot_Sig.Enabled = true;
            }

            //Si no hay mas planillas anteriores a la actual.-
            if (initObj.Frm_Pln_cob.Bot_Ant.Enabled == false && initObj.Frm_Pln_cob.Bot_Sig.Enabled == true)
            {
                //initObj.Frm_Pln_cob.Bot_Sig.SetFocus();
            }

        }

        public static void Bot_Cancel_Click(InitializationObject initObj)
        {
            //initObj.
            //VB6Helpers.Unload(this);
        }

        //Para modificar las observaciones.-
        public static void Bot_Okey_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short Fecha_Paso = 0;
            short i = 0;
            short a = 0;
            if (string.IsNullOrEmpty(initObj.Frm_Pln_cob.Tx_FecVen.Text))
            {
                //VB6Helpers.MsgBox("Debe ingresar un valor para la fecha de venta.", MsgBoxStyle.Critical, MODANUVI.MsgPlaSO);
                //initObj.Frm_Pln_cob.Tx_FecVen.SetFocus();
                //return;

                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Debe ingresar un valor para la fecha de venta.",
                    Title = T_MODANUVI.MsgPlaSO, // "Planilla Cobertura Visible de Importacion",
                    Type = TipoMensaje.Critical
                });

                return;
            }

            //La fecha no puede ser sábado ni domingo
            Fecha_Paso = VB6Helpers.Weekday(VB6Helpers.CDate(initObj.Frm_Pln_cob.Tx_FecVen.Text));
            if (Fecha_Paso == 1 || Fecha_Paso == 7)
            {
                //Sólo si es fin de semana
                //VB6Helpers.MsgBox("Atención: La Fecha no puede ser fin de semana.", MsgBoxStyle.Critical, MODANUVI.MsgPlaSO);
                //initObj.Frm_Pln_cob.Tx_FecVen.SetFocus();
                //return;

                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Atención: La Fecha no puede ser fin de semana.",
                    Title = T_MODANUVI.MsgPlaSO, //"Planilla Cobertura Visible de Importacion",
                    Type = TipoMensaje.Critical
                });

                return;
            }

            //La fecha no puede ser un feriado de este año
            a = MODGTAB0.Fn_Buscar_Fecha_Fer(initObj.MODGTAB0,uow,VB6Helpers.Trim(initObj.Frm_Pln_cob.Tx_FecVen.Text));
            if (a == 0)
            {
                //VB6Helpers.MsgBox("Atención: La Fecha no corresponde, porque existe como fecha feriada de este año.", MsgBoxStyle.Critical, MODANUVI.MsgPlaSO);
                //initObj.Frm_Pln_cob.Tx_FecVen.SetFocus();
                //return;

                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Atención: La Fecha no corresponde, porque existe como fecha feriada de este año.",
                    Title = T_MODANUVI.MsgPlaSO, //"Planilla Cobertura Visible de Importacion",
                    Type = TipoMensaje.Critical
                });

                return;

            }

            i = (short)VB6Helpers.Val(initObj.Frm_Pln_cob.Bot_Ant.Tag);
            initObj.MODPREEM.Vx_PReem[i].observ = initObj.Frm_Pln_cob.Tx_Observ.Text;

            if ((int)initObj.Frm_Pln_cob.Ch_ZonFra.Value == 1)
            {
                initObj.MODPREEM.Vx_PReem[i].ZonFra = (short)(true ? -1 : 0);
            }
            else
            {
                initObj.MODPREEM.Vx_PReem[i].ZonFra = (short)(false ? -1 : 0);
            }

            initObj.MODPREEM.Vx_PReem[i].FecVen = VB6Helpers.Format(initObj.Frm_Pln_cob.Tx_FecVen.Text, "dd/MM/yyyy");

        }

        //Avanzamos una planilla.-
        public static void Bot_Sig_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short IndActual;
            short Indice;
            Pr_LimpiaPlan(initObj);

            IndActual = (short)VB6Helpers.Val(initObj.Frm_Pln_cob.Bot_Ant.Tag);
            Indice = Fn_GetPlanProx(IndActual, 1, initObj);

            //Habilita o deshabilita botones
            if (IndActual == Indice)
            {
                //<= Si estamos en la ultima planilla
                initObj.Frm_Pln_cob.Bot_Sig.Enabled = false;
            }
            else
            {
                initObj.Frm_Pln_cob.Bot_Ant.Enabled = true;
            }

            //Traemos los datos.-
            initObj.Frm_Pln_cob.Bot_Ant.Tag = Indice;
            Pr_GetDatosPl(Indice,initObj, uow);

            //Activamos o desactivamos el boton para ver planillas siguientes.-
            if (Fn_GetPlanProx(Indice, 1, initObj) == Indice)
            {
                initObj.Frm_Pln_cob.Bot_Sig.Enabled = false;
            }

            //Si no hay mas planillas siguientes a la actual.-
            if (initObj.Frm_Pln_cob.Bot_Sig.Enabled == false && initObj.Frm_Pln_cob.Bot_Ant.Enabled == true)
            {
                //Bot_Ant.SetFocus();
            }

        }
    }
}
