using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Utils;
using System;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public static class FrmGrev_Declaracion_Logic
    {
        public static void Form_Load(InitializationObject initObj)
        {
            short i = 0;
            i = (short)initObj.frmgrev.Lt_Pln.get_ItemData_((short)initObj.frmgrev.Lt_Pln.ListIndex);

            initObj.Frm_Declaracion.CAM_Clausula.Text = Utils.Format.FormatCurrency(initObj.MODGANU.VAnuPl[i].ValCla, MODGPYF1.DecObjeto(initObj.Frm_Declaracion.CAM_Clausula));

            initObj.Frm_Declaracion.CAM_Comisiones.Text = Utils.Format.FormatCurrency(initObj.MODGANU.VAnuPl[i].ValCom, MODGPYF1.DecObjeto(initObj.Frm_Declaracion.CAM_Comisiones));

            initObj.Frm_Declaracion.CAM_Otros.Text = Utils.Format.FormatCurrency(initObj.MODGANU.VAnuPl[i].OtrGas, MODGPYF1.DecObjeto(initObj.Frm_Declaracion.CAM_Otros));

            initObj.Frm_Declaracion.CAM_Liquido.Text = Utils.Format.FormatCurrency(initObj.MODGANU.VAnuPl[i].ValLiq, MODGPYF1.DecObjeto(initObj.Frm_Declaracion.CAM_Liquido));

            initObj.Frm_Declaracion.CAM_Liquido.Enabled = false;
        }
        public static void CAM_Acepta_Click(InitializationObject initObj)
        {
            short i = 0;
            i = (short)initObj.frmgrev.Lt_Pln.get_ItemData_((short)initObj.frmgrev.Lt_Pln.ListIndex);
            initObj.MODGANU.VAnuPl[i].ValCla = Format.StringToDouble(initObj.Frm_Declaracion.CAM_Clausula.Text);
            initObj.MODGANU.VAnuPl[i].ValCom = Format.StringToDouble(initObj.Frm_Declaracion.CAM_Comisiones.Text);
            initObj.MODGANU.VAnuPl[i].OtrGas = Format.StringToDouble(initObj.Frm_Declaracion.CAM_Otros.Text);
            initObj.MODGANU.VAnuPl[i].ValLiq = Format.StringToDouble(initObj.Frm_Declaracion.CAM_Liquido.Text);

            if (initObj.MODGANU.VAnuPl[i].ValLiq != initObj.MODGANU.VAnuPl[i].MtoPln)
            {
                initObj.frmgrev.Errores.Add(new UI_Message
                {
                    Text = "El Valor Líquido a devolver a la Declaración debe corresponder al Valor Líquido de la Planilla.",
                    Type = TipoMensaje.Error,
                    ControlName = "txt_VClausula"
                });
                return;
            }
        }
        public static void CAM_Clausula_GotFocus(InitializationObject initObj)
        {
            initObj.Frm_Declaracion.CAM_Clausula.Text = Utils.Format.FormatCurrency(Format.StringToDouble(initObj.Frm_Declaracion.CAM_Clausula.Text), MODGPYF1.DecObjeto(initObj.Frm_Declaracion.CAM_Clausula));

        }
        public static void CAM_Clausula_KeyPress(ref short KeyAscii,InitializationObject initObj)
        {

            if (KeyAscii == 13)
            {
                KeyAscii = 0;
                //Co_observaciones.SetFocus
                //VB6Helpers.SendKeys("{Tab}");
            }
            else
            {
                KeyAscii = MODGPYF0.Mascara(KeyAscii, initObj.Frm_Declaracion.CAM_Clausula);
            }

        }
        public static void CAM_Clausula_LostFocus(InitializationObject initObj)
        {
            short a = 0;
            short x = 0;
            a = MODGPYF0.MascaraLost(initObj.Frm_Declaracion.CAM_Clausula, initObj.MODGPYF0);
            initObj.Frm_Declaracion.CAM_Clausula.Text = Utils.Format.FormatCurrency(Format.StringToDouble(initObj.Frm_Declaracion.CAM_Clausula.Text), MODGPYF1.DecObjeto(initObj.Frm_Declaracion.CAM_Clausula));
            x = Suma_Mto(initObj);  
        }
        public static void CAM_Comisiones_GotFocus(InitializationObject initObj)
        {
            initObj.Frm_Declaracion.CAM_Comisiones.Text = Utils.Format.FormatCurrency(Format.StringToDouble(initObj.Frm_Declaracion.CAM_Comisiones.Text), MODGPYF1.DecObjeto(initObj.Frm_Declaracion.CAM_Comisiones));
        }
        public static void CAM_Comisiones_KeyPress(ref short KeyAscii, InitializationObject initObj)
        {
            if (KeyAscii == 13)
            {
                KeyAscii = 0;
                //Co_observaciones.SetFocus
                //VB6Helpers.SendKeys("{Tab}");
            }
            else
            {
                KeyAscii = MODGPYF0.Mascara(KeyAscii, initObj.Frm_Declaracion.CAM_Comisiones);
            }

        }
        public static void CAM_Comisiones_LostFocus(InitializationObject initObj)
        {
            short x = 0;
            initObj.Frm_Declaracion.CAM_Comisiones.Text = Utils.Format.FormatCurrency(Format.StringToDouble(initObj.Frm_Declaracion.CAM_Comisiones.Text), MODGPYF1.DecObjeto(initObj.Frm_Declaracion.CAM_Comisiones));
            x = Suma_Mto(initObj);
        }
        public static void CAM_Liquido_GotFocus(InitializationObject initObj)
        {
            initObj.Frm_Declaracion.CAM_Liquido.Text = Utils.Format.FormatCurrency(Format.StringToDouble(initObj.Frm_Declaracion.CAM_Liquido.Text), MODGPYF1.DecObjeto(initObj.Frm_Declaracion.CAM_Liquido));
        }
        public static void CAM_Liquido_KeyPress(ref short KeyAscii, InitializationObject initObj)
        {
            if (KeyAscii == 13)
            {
                KeyAscii = 0;
                //Co_observaciones.SetFocus
                //VB6Helpers.SendKeys("{Tab}");
            }
            else
            {
                KeyAscii = MODGPYF0.Mascara(KeyAscii, initObj.Frm_Declaracion.CAM_Liquido);
            }

        }
        public static void CAM_Liquido_LostFocus(InitializationObject initObj)
        {
            initObj.Frm_Declaracion.CAM_Liquido.Text = Utils.Format.FormatCurrency(Format.StringToDouble(initObj.Frm_Declaracion.CAM_Liquido.Text), MODGPYF1.DecObjeto(initObj.Frm_Declaracion.CAM_Liquido));
        }
        public static void CAM_Otros_GotFocus(InitializationObject initObj)
        {
            initObj.Frm_Declaracion.CAM_Otros.Text = Utils.Format.FormatCurrency(Format.StringToDouble(initObj.Frm_Declaracion.CAM_Otros.Text), MODGPYF1.DecObjeto(initObj.Frm_Declaracion.CAM_Otros));
        }
        public static void CAM_Otros_KeyPress(ref short KeyAscii, InitializationObject initObj)
        {
            if (KeyAscii == 13)
            {
                KeyAscii = 0;
                //Co_observaciones.SetFocus
               // VB6Helpers.SendKeys("{Tab}");
            }
            else
            {
                KeyAscii = MODGPYF0.Mascara(KeyAscii, initObj.Frm_Declaracion.CAM_Otros);
            }

        }
        public static void CAM_Otros_LostFocus(InitializationObject initObj)
        {
            short x = 0;
            initObj.Frm_Declaracion.CAM_Otros.Text = Utils.Format.FormatCurrency(Format.StringToDouble(initObj.Frm_Declaracion.CAM_Otros.Text), MODGPYF1.DecObjeto(initObj.Frm_Declaracion.CAM_Otros));
            x = Suma_Mto(initObj);
        }
        public static short Suma_Mto(InitializationObject initObj)
        {
            using (var tracer = new Tracer())
            {
            try
            {
                double valor = Format.StringToDouble(initObj.Frm_Declaracion.CAM_Clausula.Text) - Format.StringToDouble(initObj.Frm_Declaracion.CAM_Comisiones.Text) - Format.StringToDouble(initObj.Frm_Declaracion.CAM_Otros.Text);

                initObj.Frm_Declaracion.CAM_Liquido.Text = Utils.Format.FormatCurrency(valor, "###,##0.00");
                return 0;
            }
                catch (Exception e)
            {
                    tracer.TraceException("Alerta", e);
            }
            return 0;
            }
        }
    }
}
