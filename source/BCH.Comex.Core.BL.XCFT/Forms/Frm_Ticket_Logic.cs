using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public static class Frm_Ticket_Logic
    {
        #region METODOS PRIVADOS

        #endregion

        #region METODOS PUBLICOS
        public static void Form_Load(InitializationObject initObject,UnitOfWorkCext01 unit)
        {
            UI_Frm_Ticket Frm_Ticket = initObject.Frm_Ticket;
            T_MODXVIA MODXVIA = initObject.MODXVIA;

            short x;
            short i = 0;
            Frm_Ticket.CBO_DeHa.Items.Clear();
            Frm_Ticket.CBO_DeHa.Items.Add(new UI_ComboItem()
            {
                Value= "Débito",
                Data=1
            });
            Frm_Ticket.CBO_DeHa.Items.Add(new UI_ComboItem()
            {
                Value = "Crédito",
                Data = 2
            });

            Frm_Ticket.CAM_Nombre.Text = MODXVIA.Strtic.Nomtic;
            Frm_Ticket.CAM_Nemonico.Text = MODXVIA.Strtic.Nemtic;
            Frm_Ticket.CAM_Monto.Text = MODXVIA.Strtic.Montic;
            Frm_Ticket.CAM_Cuenta.Text = MODXVIA.Strtic.Cuetic;
            Frm_Ticket.CBO_DeHa.ListIndex = MODXVIA.Strtic.Dehtic;
            

            //Carga datos a combo Cb_ticket de tabla Sce_Tdme      .-
            //Datos Sce_Tdme: VTDme(i%).CodDme ... VTDme(i%).DesDme.-

            x = BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.SyGetn_Tdme(initObject,unit);

            for (i = 0; i <= (short)VB6Helpers.UBound(MODXVIA.VTDme); i++)
            {
                Frm_Ticket.Cb_ticket.Items.Add(new UI_ComboItem()
                {
                    Value= MODGPYF1.Minuscula(MODXVIA.VTDme[i].DesDme),
                    Data=(i+1)
                });
            }

            if (!String.IsNullOrEmpty(MODXVIA.Strtic.Glosa))
            {
                Frm_Ticket.CAM_Concepto.Text = MODXVIA.Strtic.Glosa;
            }
            else
            {
                Frm_Ticket.Cb_ticket.ListIndex = 0;
                Cb_ticket_Click(initObject);
            }

        }
        public static void Aceptar_Click(InitializationObject initObject,UnitOfWorkCext01 unit)
        {
            UI_Frm_Ticket Frm_Ticket = initObject.Frm_Ticket;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_Module1 Module1 = initObject.Module1;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_MODGSCE MODGSCE = initObject.MODGSCE;
            int i;
            short x = 0;
            double[] MtoDC;

            if (!String.IsNullOrEmpty(Frm_Ticket.CAM_Cuenta.Text) && String.IsNullOrEmpty(Frm_Ticket.CAM_Concepto.Text.Trim()))
            {
                Frm_Ticket.Errors.Add(new UI_Message()
                {
                    Type=TipoMensaje.Error,
                    Text= "Debe ingresar concepto de la cuenta corriente",
                    Title= "Ticket"
                });
                return;
            }
            MODXVIA.Strtic.Contic = Frm_Ticket.CAM_Concepto.Text;
            Frm_Ticket.aceptar.Tag = "A";
            initObject.Frm_Ticket = null;
            initObject.FormularioQueAbrir = "Grabar2";
            
            //*********************************************************************************
            //ESTO SE CAMBIO PARA SABER PARA CADA CORRELATIVO DE QUE TIPO ES. Credito o Debito
            //*********************************************************************************
            if (Frm_Ticket.esOri)
            {
                i = initObject.oriLoop - 1;
                
                if (i != -1)
                {
                    if (((MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteMN ? -1 : 0) & Frm_Ticket.ImpDeb & (T_MODGMTA.impflag == 1 ? -1 : 0)) != 0)
                    {
                        MtoDC = new double[2];
                        MtoDC[0] = MODXORI.VxOri[i].MtoTot;
                        MtoDC[1] = MODGSCE.VGen.MtoDeb;
                    }
                    else
                    {
                        MtoDC = new double[1];
                        MtoDC[0] = MODXORI.VxOri[i].MtoTot;
                    }
                    
                    x = BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Doc_gAdc(initObject, unit, Frm_Ticket.NumOpe, ref Module1.PartysOpe[MODXORI.VxOri[i].PosPrty], "D", MODXORI.VxOri[i].CtaCte_t, MODXVIA.Strtic.Contic, MODXORI.VxOri[i].NemMon, MtoDC, Frm_Ticket.Referencia, Frm_Ticket.Usuario);
                    if (MODXORI.VxOri[i].CodMon == T_MODGTAB0.MndNac)
                    {
                        initObject.SyPutn_Adc_Str += VB6Helpers.Str(x) + " N D" + ";";
                    }
                    else
                    {
                        initObject.SyPutn_Adc_Str += VB6Helpers.Str(x) + " E D" + ";";
                    }
                }
            }
            else
            {
                i = initObject.viaLoop - 1;
                if (i != -1)
                {
                    MtoDC = new double[1];
                    MtoDC[0] = MODXVIA.VxVia[i].MtoTot;
                    x = BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Doc_gAdc(initObject, unit, Frm_Ticket.NumOpe, ref Module1.PartysOpe[MODXVIA.VxVia[i].PosPrty], "C", MODXVIA.VxVia[i].CtaCte_t, MODXVIA.Strtic.Contic, MODXVIA.VxVia[i].NemMon, MtoDC, Frm_Ticket.Referencia, Frm_Ticket.Usuario);
                    if (MODXVIA.VxVia[i].CodMon == T_MODGTAB0.MndNac)
                    {
                        initObject.SyPutn_Adc_Str += VB6Helpers.Str(x) + " N C" + ";";
                    }
                    else
                    {
                        initObject.SyPutn_Adc_Str += VB6Helpers.Str(x) + " E C" + ";";
                    }
                }
            }
        }

        public static bool ValidarCheckTicker(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            UI_Frm_Ticket Frm_Ticket = initObject.Frm_Ticket;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_Module1 Module1 = initObject.Module1;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_MODGSCE MODGSCE = initObject.MODGSCE;
            int i;
            short x = 0;
            double[] MtoDC;
            
            if (Frm_Ticket.esOri)
            {
                i = initObject.oriLoop - 1;

                if (i != -1)
                {
                    if (((MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteMN ? -1 : 0) & Frm_Ticket.ImpDeb & (T_MODGMTA.impflag == 1 ? -1 : 0)) != 0)
                    {
                        MtoDC = new double[2];
                        MtoDC[0] = MODXORI.VxOri[i].MtoTot;
                        MtoDC[1] = MODGSCE.VGen.MtoDeb;
                    }
                    else
                    {
                        MtoDC = new double[1];
                        MtoDC[0] = MODXORI.VxOri[i].MtoTot;
                    }

                    x = BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Doc_gAdc(initObject, unit, Frm_Ticket.NumOpe, ref Module1.PartysOpe[MODXORI.VxOri[i].PosPrty], "D", MODXORI.VxOri[i].CtaCte_t, MODXVIA.Strtic.Contic, MODXORI.VxOri[i].NemMon, MtoDC, Frm_Ticket.Referencia, Frm_Ticket.Usuario);
                    if (MODXORI.VxOri[i].CodMon == T_MODGTAB0.MndNac)
                    {
                        initObject.SyPutn_Adc_Str += VB6Helpers.Str(x) + " N D" + ";";
                    }
                    else
                    {
                        initObject.SyPutn_Adc_Str += VB6Helpers.Str(x) + " E D" + ";";
                    }
                    return true;
                }                
            }
            else
            {
                i = initObject.viaLoop - 1;
                if (i != -1)
                {
                    MtoDC = new double[1];
                    MtoDC[0] = MODXVIA.VxVia[i].MtoTot;
                    x = BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Doc_gAdc(initObject, unit, Frm_Ticket.NumOpe, ref Module1.PartysOpe[MODXVIA.VxVia[i].PosPrty], "C", MODXVIA.VxVia[i].CtaCte_t, MODXVIA.Strtic.Contic, MODXVIA.VxVia[i].NemMon, MtoDC, Frm_Ticket.Referencia, Frm_Ticket.Usuario);
                    if (MODXVIA.VxVia[i].CodMon == T_MODGTAB0.MndNac)
                    {
                        initObject.SyPutn_Adc_Str += VB6Helpers.Str(x) + " N C" + ";";
                    }
                    else
                    {
                        initObject.SyPutn_Adc_Str += VB6Helpers.Str(x) + " E C" + ";";
                    }
                    return true;
                }                
            }
            return false;
        }

        public static void Cancelar_Click(InitializationObject initObject)
        {
            UI_Frm_Ticket Frm_Ticket = initObject.Frm_Ticket;
            T_MODXVIA MODXVIA = initObject.MODXVIA;

            if (!String.IsNullOrEmpty(Frm_Ticket.CAM_Cuenta.Text) && String.IsNullOrEmpty(Frm_Ticket.CAM_Concepto.Text.Trim()))
            {
                Frm_Ticket.Errors.Add(new UI_Message()
                {
                    Type=TipoMensaje.Error,
                    Text= "Debe ingresar concepto de la cuenta corriente",
                    Title= "Ticket"
                });
                return;
            }

            //If CAM_Concepto.Text = "" Then
            //    CAM_Concepto.Text = "Ajuste Contable"
            //End If

            MODXVIA.Strtic.Contic = Frm_Ticket.CAM_Concepto.Text;
            // UPGRADE_INFO (#0181): Reference to default form instance 'Frm_Ticket' was converted to Me/this keyword.
            // UPGRADE_WARNING (#0364): Unable to assign default member of symbol 'this.Tag'. Consider using the SetDefaultMember6 helper method.
            Frm_Ticket.cancelar.Tag = "C";
            initObject.Frm_Ticket = null;
            initObject.FormularioQueAbrir = "Grabar2";
        }
        public static void Cb_ticket_Click(InitializationObject initObject)
        {
            UI_Frm_Ticket Frm_Ticket = initObject.Frm_Ticket;
            //Cb_ticket.List(Cb_ticket.ListIndex) = S$
            //Cb_ticket.ItemData(Cb_ticket.ListIndex) = k%
            //VTDme(i%).CodDme
            Frm_Ticket.CAM_Concepto.Text = Frm_Ticket.Cb_ticket.get_List((short)Frm_Ticket.Cb_ticket.ListIndex);
        }
        public static void otro_Click(InitializationObject initObject)
        {
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            UI_Frm_Ticket Frm_Ticket = initObject.Frm_Ticket;

            if (Frm_Ticket.otro.Value != 0)
            {
                MODXVIA.Strtic.Demtci = true;
            }
            else
            {
                MODXVIA.Strtic.Demtci = false;
            }

        }
        #endregion

    }
}
