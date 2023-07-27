using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public class Frm_Comisiones
    {
        public static void Form_Load(InitializationObject initObject, UnitOfWorkCext01 uow)
        {
            short[] Tabs = null;
            short a = MODGTAB0.SyGet_Vmd(initObject.MODGTAB0, uow, DateTime.Now.ToString("yyyy-MM-dd"), 11);
            short i = 0;
            initObject.Frm_Comisiones.Ch_com.Enabled = false;
            initObject.Frm_Comisiones.Tx_Com[3].Enabled = false;
            //Extrae tipo de cambio del día
            initObject.Mdl_Funciones_Varias.WgCom.TipCam = initObject.MODGTAB0.VVmd.VmdObs;
            initObject.Mdl_Funciones_Varias.WgCom.Acepto = (short)(false ? -1 : 0);

            //Tabuladores.-
            Tabs = new short[3];
            Tabs[0] = 80;
            Tabs[1] = 85;
            a = MODGPYF0.seteatabulador(initObject.Frm_Comisiones.Ls_com, Tabs);

            //Valores Líquidos.-
            //Cargar las Comisiones en la lista.-
            for (i = 0; i <= (short)VB6Helpers.UBound(initObject.Mdl_Funciones_Varias.V_gCom); i++)
            {
                initObject.Frm_Comisiones.Ls_com.Items.Add(new UI_ListBoxItem
              {
                  Value = Linea_gCom(initObject, i),
                  Data = i
              });
            }

            //Se agrega al ultimo una linea vacia para poder agregar nuevas comisiones
            initObject.Frm_Comisiones.Ls_com.Items.Add(new UI_ListBoxItem
            {
                Value = "",
                Data = -1
            });

            initObject.Frm_Comisiones.Ls_com.ListIndex = 0;
            Ls_com_Click(initObject, uow);
        }
        public static void Cm_com_Click(InitializationObject initObject, UnitOfWorkCext01 uow)
        {
            if (~ValCom(initObject) != 0)
            {
                return;
            }
            initObject.Mdl_Funciones_Varias.WgCom.Acepto = (short)(true ? -1 : 0);
        }
        public static void NO_com_Click(InitializationObject initObject, UnitOfWorkCext01 uow)
        {
            initObject.Frm_Comisiones.Tx_Com[1].Text = "0";
            OK_Com_Click(initObject, uow);
        }
        public static void OK_Com_Click(InitializationObject initObject, UnitOfWorkCext01 uow)
        {
            int cta = 0;
            string GlsCom = "";
            double MtoCom = 0;
            short chcom = 0;
            double TipCam = 0;
            string NemCta = "";
            short x = 0;

            if (initObject.Frm_Comisiones.Tx_Com[0].Text == null)
            {
                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Debe ingresar la Descripción de la Comisión a cobrar.",
                    Title = T_Mdl_Funciones_Varias.MsggCom,
                    Type = TipoMensaje.Informacion,
                    ControlName = "Tx_Com_0_Text"
                });
                return;
            }

            if (initObject.Frm_Comisiones.Tx_Com[1].Text == null)
            {
                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Debe ingresar el Monto en US$ de la Comisión.",
                    Title = T_Mdl_Funciones_Varias.MsggCom,
                    Type = TipoMensaje.Informacion,
                    ControlName = "Tx_Com_1_Text"
                });
                return;
            }

            if (Format.StringToDouble((initObject.Frm_Comisiones.Tx_Com[2].Text)) == 0D && Format.StringToDouble((initObject.Frm_Comisiones.Tx_Com[1].Text)) > 0D)
            {
                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " El tipo de Cambio debe ser mayor que cero.",
                    Title = T_Mdl_Funciones_Varias.MsggCom,
                    Type = TipoMensaje.Informacion,
                    ControlName = "Tx_Com_2_Text"
                });
                return;
            }

            if (initObject.Frm_Comisiones.Tx_Com[4].Text == null)
            {
                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Debe ingresar el Nemónico de la Cuenta Contable.",
                    Title = T_Mdl_Funciones_Varias.MsggCom,
                    Type = TipoMensaje.Informacion,
                    ControlName = "Tx_Com_4_Text"
                });
                return;
            }
            
            cta = MODGNCTA.Get_Cta(initObject.Frm_Comisiones.Tx_Com[4].Text.ToUpper(), initObject, uow);
            if (cta == -1 || cta == 0)
            {
                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " El Nemónico que Usted acaba de ingresar no se encuentra dentro de las Cuentas Contables.",
                    Title = T_Mdl_Funciones_Varias.MsggCom,
                    Type = TipoMensaje.Informacion,
                    ControlName = "Tx_Com_4_Text"
                });
                return;
            }

            if (initObject.MODGNCTA.VCta[cta].Cta_Mon == T_MODGNCTA.CtaMonExt)
            {
                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Debe ingresar una Cuenta Contable en Moneda Nacional.",
                    Title = T_Mdl_Funciones_Varias.MsggCom,
                    Type = TipoMensaje.Informacion,
                    ControlName = "Tx_Com_4_Text"
                });
                return;
            }

            initObject.Frm_Comisiones.Tx_Com[5].Text = initObject.MODGNCTA.VCta[cta].Cta_Nom.Value.Trim();
            initObject.MODGCHQ.Indice = (short)initObject.Frm_Comisiones.Ls_com.get_ItemData((short)initObject.Frm_Comisiones.Ls_com.ListIndex);
            GlsCom = initObject.Frm_Comisiones.Tx_Com[0].Text.Trim();
            MtoCom = double.Parse(initObject.Frm_Comisiones.Tx_Com[1].Text);
            TipCam =  double.Parse(initObject.Frm_Comisiones.Tx_Com[2].Text);
            NemCta = initObject.Frm_Comisiones.Tx_Com[4].Text.ToUpper().Trim();

            // Incoporación de la validación para el IVA
            chcom = SyGet_Iva(uow, initObject.MODGNCTA.VCta[cta].Cta_Nem);
         
            x = Mdl_Funciones_Varias.Put_Gcom_2(initObject.Mdl_Funciones_Varias, initObject.MODGSCE, initObject.Mdi_Principal,
                initObject.MODGCHQ.Indice == (initObject.Frm_Comisiones.Ls_com.Items.Count - 1) ? (short)0 : initObject.MODGCHQ.Indice, GlsCom, "US$", MtoCom, chcom, TipCam, NemCta);

            if (initObject.MODGCHQ.Indice != -1)
            {
                int nuevoSelect = initObject.MODGCHQ.Indice + 1;
                if (nuevoSelect >= initObject.Frm_Comisiones.Ls_com.Items.Count())
                {
                    nuevoSelect = initObject.Frm_Comisiones.Ls_com.Items.Count() - 1;
                }
                //Aqui es necesario reemplazar en el UI_Combo con los nuevos valores de Mdl_Funciones_Varias.V_gCom para el item
                initObject.Frm_Comisiones.Ls_com.Items[initObject.MODGCHQ.Indice].Value = Linea_gCom(initObject, initObject.MODGCHQ.Indice);
                initObject.Frm_Comisiones.Ls_com.ListIndex = nuevoSelect;
            }
            else 
            {
                initObject.Frm_Comisiones.Ls_com.Items[initObject.Frm_Comisiones.Ls_com.Items.Count() - 1].Value = Linea_gCom(initObject, initObject.Frm_Comisiones.Ls_com.Items.Count() - 1);
                initObject.Frm_Comisiones.Ls_com.Items[initObject.Frm_Comisiones.Ls_com.Items.Count() - 1].Data = initObject.Frm_Comisiones.Ls_com.Items.Count() - 1;
                initObject.Frm_Comisiones.Ls_com.ListIndex = initObject.Frm_Comisiones.Ls_com.Items.Count();

                initObject.Frm_Comisiones.Ls_com.Items.Add(new UI_ListBoxItem
                {
                    Value = "",
                    Data = -1
                });
            }
            //se ejecuta el evento de cuando se selecciona una nueva comision
            Ls_com_Click(initObject, uow);
        }
        public static void Ls_com_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short i = (short)initObj.Frm_Comisiones.Ls_com.get_ItemData((short)initObj.Frm_Comisiones.Ls_com.ListIndex);
            int cta = 0;
            if (i == -1) {
                initObj.Frm_Comisiones.Tx_Com[5].Text = "";
                initObj.Frm_Comisiones.NO_com.Enabled = false;
                initObj.Frm_Comisiones.Tx_Com[2].Text = i != -1 ? Format.FormatCurrency(Convert.ToDouble(initObj.Mdl_Funciones_Varias.V_gCom[i].TipCam), "###,##0.0000") : Format.FormatCurrency(Convert.ToDouble(initObj.Mdl_Funciones_Varias.WgCom.TipCam), "###,##0.0000");
            }
            else
            {
                initObj.Frm_Comisiones.NO_com.Enabled = true;
                initObj.Frm_Comisiones.Tx_Com[2].Text = Format.FormatCurrency(Convert.ToDouble(initObj.Mdl_Funciones_Varias.V_gCom[i].TipCam), "###,##0.0000");
            }

            if (initObj.Mdl_Funciones_Varias.V_gCom.Length > 0 && i != -1)
            {
                initObj.Frm_Comisiones.Tx_Com[0].MaxLength = 25;
                initObj.Mdl_Funciones_Varias.V_gCom[i].GlsCom =
                    initObj.Mdl_Funciones_Varias.V_gCom[i].GlsCom.Length > initObj.Frm_Comisiones.Tx_Com[0].MaxLength ? initObj.Mdl_Funciones_Varias.V_gCom[i].GlsCom.Substring(0, 25) : initObj.Mdl_Funciones_Varias.V_gCom[i].GlsCom;
                
                initObj.Frm_Comisiones.Tx_Com[0].Text = initObj.Mdl_Funciones_Varias.V_gCom[i].GlsCom;
                initObj.Frm_Comisiones.Tx_Com[1].Text = Format.FormatCurrency(initObj.Mdl_Funciones_Varias.V_gCom[i].MtoCom, "#,###,###,###,##0.00");
                initObj.Frm_Comisiones.Tx_Com[3].Text = Format.FormatCurrency(initObj.Mdl_Funciones_Varias.V_gCom[i].MtoTotp, "#,###,###,###,##0");
                
                if (initObj.Mdl_Funciones_Varias.V_gCom[i].NemCta != null)
                    initObj.Frm_Comisiones.Tx_Com[4].Text = initObj.Mdl_Funciones_Varias.V_gCom[i].NemCta.ToUpper().Trim();
                else
                    initObj.Frm_Comisiones.Tx_Com[4].Text = initObj.Mdl_Funciones_Varias.V_gCom[i].NemCta.Trim();
                
                // Incoporación de la validación para el IVA
                initObj.Frm_Comisiones.Ch_com.Value = SyGet_Iva(uow, initObj.Mdl_Funciones_Varias.V_gCom[i].NemCta);
                Ch_com_Click(initObj, uow);

                if (i >= 0)
                {
                    cta = MODGNCTA.Get_Cta(initObj.Frm_Comisiones.Tx_Com[4].Text, initObj, uow);
                    if (cta != -1)
                        initObj.Frm_Comisiones.Tx_Com[5].Text = initObj.MODGNCTA.VCta[cta].Cta_Nom.Value.Trim();
                    else
                        initObj.Frm_Comisiones.Tx_Com[5].Text = string.Empty;
                }
            }
            else
            {
                initObj.Frm_Comisiones.Tx_Com[0].Text = string.Empty;
                initObj.Frm_Comisiones.Tx_Com[1].Text = Format.FormatCurrency(0, "###,##0.00");
                initObj.Frm_Comisiones.Tx_Com[3].Text = Format.FormatCurrency(0, "###,##0.00");
                initObj.Frm_Comisiones.Tx_Com[4].Text = string.Empty;
                
                // Incoporación de la validación para el IVA
                initObj.Frm_Comisiones.Ch_com.Value = (short)0;
                // Fin de la Incorporacion
                if (i >= 0)
                {
                    cta = MODGNCTA.Get_Cta(initObj.Frm_Comisiones.Tx_Com[4].Text, initObj, uow);
                    initObj.Frm_Comisiones.Tx_Com[5].Text = initObj.MODGNCTA.VCta[cta].Cta_Nom.Value;
                }
            }
        }
        public static void Tx_Com_LostFocus(InitializationObject initObject, UnitOfWorkCext01 uow, short Index)
        {
            double MtoTotp = 0;
            double tasaiva = 0;
            switch (Index)
            {
                case 1:
                    if (initObject.Frm_Comisiones.Tx_Com[1].Text != null || initObject.Frm_Comisiones.Tx_Com[2].Text != null)
                    {
                        MtoTotp = Format.StringToDouble(initObject.Frm_Comisiones.Tx_Com[1].Text) * Format.StringToDouble(initObject.Frm_Comisiones.Tx_Com[2].Text);
                        if (initObject.Frm_Comisiones.Ch_com.Value != 0)
                        {
                            tasaiva = 1 + (initObject.MODGSCE.VGen.MtoIva / 100);
                            MtoTotp = MtoTotp * (tasaiva);
                        }
                        initObject.Frm_Comisiones.Tx_Com[3].Text = Format.FormatCurrency(MtoTotp, "##,###0");  //forma(MtoTotp#, "#,###,###,###,##0")
                    }
                    break;
                case 2:
                    if (initObject.Frm_Comisiones.Tx_Com[1].Text != null || initObject.Frm_Comisiones.Tx_Com[2].Text != null)
                    {
                        MtoTotp = Format.StringToDouble(initObject.Frm_Comisiones.Tx_Com[1].Text) * Format.StringToDouble(initObject.Frm_Comisiones.Tx_Com[2].Text);
                        if (initObject.Frm_Comisiones.Ch_com.Value != 0)
                        {
                            tasaiva = 1 + (initObject.MODGSCE.VGen.MtoIva / 100);
                            MtoTotp = MtoTotp * (tasaiva);
                        }
                        initObject.Frm_Comisiones.Tx_Com[3].Text = Format.FormatCurrency(MtoTotp, "##,###0");  //forma(MtoTotp#, "#,###,###,###,##0")
                    }
                    if (initObject.Frm_Comisiones.Tx_Com[4].Text != null)
                    {
                        initObject.Frm_Comisiones.Ch_com.Value = SyGet_Iva(uow, initObject.Frm_Comisiones.Tx_Com[4].Text.ToUpper());

                        if (initObject.Frm_Comisiones.Tx_Com[1].Text != null || initObject.Frm_Comisiones.Tx_Com[2].Text != null)
                        {
                            //Calculo del Monto en $.-
                            MtoTotp = Format.StringToDouble(initObject.Frm_Comisiones.Tx_Com[1].Text) * Format.StringToDouble(initObject.Frm_Comisiones.Tx_Com[2].Text);
                            if (initObject.Frm_Comisiones.Ch_com.Value != 0)
                            {
                                tasaiva = 1 + (initObject.MODGSCE.VGen.MtoIva / 100);
                                MtoTotp = MtoTotp * (tasaiva);
                            }
                            initObject.Frm_Comisiones.Tx_Com[3].Text = Format.FormatCurrency((MtoTotp), "##,###0");  //forma(MtoTotp#, "#,###,###,###,##0")
                        }

                        if(initObject.MODGCVD.COMISION == true && !string.IsNullOrWhiteSpace(initObject.Frm_Comisiones.Tx_Com[4].Text))
                        {
                            initObject.Frm_Comisiones.Tx_Com[5].Text = "";
                            initObject.Frm_Comisiones.Tx_Com[5].Text = Mdl_Funciones_Varias.BuscaCta(VB6Helpers.Trim(initObject.Frm_Comisiones.Tx_Com[4].Text), initObject, uow);
                        }
                    }
                    break;
                case 4:
                    initObject.Frm_Comisiones.Ch_com.Value = SyGet_Iva(uow, initObject.Frm_Comisiones.Tx_Com[4].Text.ToUpper());
                    if (initObject.MODGCVD.COMISION == true && VB6Helpers.Trim(initObject.Frm_Comisiones.Tx_Com[4].Text) != "")
                    {
                        initObject.Frm_Comisiones.Tx_Com[5].Text =
                        initObject.Frm_Comisiones.Tx_Com[5].Text = Mdl_Funciones_Varias.BuscaCta(VB6Helpers.Trim(initObject.Frm_Comisiones.Tx_Com[4].Text), initObject, uow);
                    }
                    break;
                default:
                    break;
            }
        }
        private static void Ch_com_Click(InitializationObject initObject, UnitOfWorkCext01 uow)
        {
            Tx_Com_LostFocus(initObject, uow, 1);
        }
        private static short ValCom(InitializationObject initObject)
        {
            short _retValue = 0;
            short n = 0;
            short i = 0;
            _retValue = (short)(false ? -1 : 0);
            
            n = (short)VB6Helpers.UBound(initObject.Mdl_Funciones_Varias.V_gCom);

            for (i = 1; i <= (short)n; i++)
            {
                if (initObject.Mdl_Funciones_Varias.V_gCom[i].TipCam == 0D && initObject.Mdl_Funciones_Varias.V_gCom[i].MtoCom > 0D)
                {
                    initObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = " El tipo de Cambio debe ser mayor que cero.",
                        Title = T_Mdl_Funciones_Varias.MsggCom,
                        Type = TipoMensaje.Informacion
                    });
                    initObject.Frm_Comisiones.Ls_com.ListIndex = MODGPYF0.PosLista(initObject.Frm_Comisiones.Ls_com, i);
                    return _retValue;
                }
            }
            return (short)(true ? -1 : 0);
        }
        private static string Linea_gCom(InitializationObject initObject, int Indice)
        {
            string s = "";
            s = s + Mdl_Funciones_Varias.llena_blancos_der(initObject.Mdl_Funciones_Varias.V_gCom[Indice].GlsCom, (short)25) + VB6Helpers.Chr(9);//initObject.Mdl_Funciones_Varias.V_gCom[Indice].GlsCom.Length);
            s = s + initObject.Mdl_Funciones_Varias.V_gCom[Indice].NemMnd + VB6Helpers.Chr(9);
            s = s + Format.FormatCurrency(initObject.Mdl_Funciones_Varias.V_gCom[Indice].MtoCom, "#,###,###,###,##0.00") + VB6Helpers.Chr(9);

            return s;
        }
        private static short SyGet_Iva(UnitOfWorkCext01 uow, string strNemo)
        {
            short _retValue;
            string Que = "";
            string R = "";
            try
            {
                R = uow.SceRepository.pro_sce_cta_s01_MS(strNemo);
                //_retValue = -1;
                _retValue = 1;

                if (R == null)
                {
                    _retValue = 0;//(short)(false ? -1 : 0);

                }

            }
            catch (Exception _ex)
            {
                _retValue = 0;//(short)(false ? -1 : 0);
            }

            return _retValue;
        }
    }
}
