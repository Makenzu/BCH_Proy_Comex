using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public class Frm_VisE_Logic
    {
        public static void Form_Load(T_MODXPLN0 MODXPLN0, T_MODGPYF0 MODGPYF0, T_MODGTAB0 MODGTAB0, T_MODGCVD MODGCVD, 
            UI_Frm_VisE Frm_VisE, T_Mdl_Funciones_Varias Mdl_Funciones_Varias, UnitOfWorkCext01 unit)
        {
            short b;
            short i = 0;
            
            //Lee las Monedas.
            b = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.SyGetn_Mnd(MODGTAB0,unit);
            BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.CargaEnLista_Mnd(MODGTAB0,Frm_VisE.Cb_Mnd);
            Frm_VisE.Cb_Mnd.ListIndex = Frm_VisE.Cb_Mnd.Items.FindIndex(x => x.ID.Equals(MODGCVD.CodMonDolar.ToString()));
            Cb_Mnd_Blur(MODGCVD, MODGTAB0, Mdl_Funciones_Varias, Frm_VisE, unit);

            //' Se deja comentariado Problemas al inicio al cargar moneda
            //'For i% = 0 To 3
            //'    Tx_MtoVisE(i%) = Format$(ValTexto(Tx_MtoVisE(i%)), DecObjeto(Tx_MtoVisE(i%)))
            //'Next

            Frm_VisE.Tx_MtoVisE[0].Text = MODGTAB0.VVmd.VmdMbc.ToString("0.0000");
            for (i = 0; i <= 3; i++)
            {
                Frm_VisE.Tx_MtoVisE[i].Tag = "0.00";
                Frm_VisE.Tx_MtoVisE[i].Text = MODGPYF1.ValTexto(MODGPYF0, Frm_VisE.Tx_MtoVisE[i].Text).ToString(MODGPYF1.DecObjeto(Frm_VisE.Tx_MtoVisE[i]));
                //Frm_VisE.Tx_MtoVisE[i].Text = VB6Helpers.Format(VB6Helpers.CStr(BCH.Comex.Core.BL.XCFT.Modulos.MODGPYF1.ValTexto(MODGPYF0,Frm_VisE.Tx_MtoVisE[i].Text)), BCH.Comex.Core.BL.XCFT.Modulos.MODGPYF1.DecObjeto(Frm_VisE.Tx_MtoVisE[i]));
            }

            MODXPLN0.VxDatP.Acepto = (short)(false ? -1 : 0);
        }

        public static void Cb_Mnd_Blur(T_MODGCVD MODGCVD, T_MODGTAB0 MODGTAB0, T_Mdl_Funciones_Varias Mdl_Funciones_Varias, 
            UI_Frm_VisE Frm_VisE,UnitOfWorkCext01 unit)
        {
            short a = short.Parse(Frm_VisE.Cb_Mnd.Items.ElementAt(Frm_VisE.Cb_Mnd.ListIndex).ID);
            double b = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.SyGet_Vmd(MODGTAB0,unit, DateTime.Now.ToString("yyyy-MM-dd"), a);
            if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
            {
                Mdl_Funciones_Varias.LC_NOM_MDA = Frm_VisE.Cb_Mnd.Items.ElementAt(Frm_VisE.Cb_Mnd.ListIndex).Value;
            }

            //Cuando hay Reemplazo => NO precargar TC (y es Compra).-
            if (MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_RyR)
            {
                Frm_VisE.Caption = "Comercio Visible - Compra";
                return;
            }

            if (MODGTAB0.VVmd.VmdObs > 0)
            {
                Frm_VisE.Tx_MtoVisE[0].Text = MODGTAB0.VVmd.VmdMbc.ToString("0.0000");
            }
            else
            {
                Frm_VisE.Tx_MtoVisE[0].Text = MODGTAB0.VVmd.VmdObs.ToString("0.0000");
            }

        }

        public static void Tx_MtoVisE_LostFocus(UI_Frm_VisE Frm_VisE, short Index)
        {
            switch (Index)
            {
                case 0:
                    Frm_VisE.Tx_MtoVisE[Index].Text = Format.FormatCurrency(Format.StringToDouble(Frm_VisE.Tx_MtoVisE[Index].Text),"##,###0.0000");
                    break;
                case 1:
                case 2:
                case 3:
                    Frm_VisE.Tx_MtoVisE[Index].Text = Format.FormatCurrency(Format.StringToDouble(Frm_VisE.Tx_MtoVisE[Index].Text),"##,###0.00");
                    break;
            }
        }

        public static bool Co_Boton_Click(T_MODGTAB0 MODGTAB0, T_MODXPLN0 MODXPLN0, T_MODGCVD MODGCVD, UI_Frm_VisE Frm_VisE,UnitOfWorkCext01 unit, short Index)
        {
            double TC = 0;
            double ML = 0;
            short n = 0;
            switch (Index)
            {
                case 0:  //Aceptar
                    if (Frm_VisE.Cb_Mnd.ListIndex == -1)
                    {
                        Frm_VisE.Messages.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Debe ingresar el tipo de moneda.",
                            ControlName = "idCbMnd"
                        });
                        return false;
                    }

                    if (Format.StringToDouble(Frm_VisE.Tx_MtoVisE[1].Text) == 0 && Format.StringToDouble(Frm_VisE.Tx_MtoVisE[2].Text) == 0 && Format.StringToDouble(Frm_VisE.Tx_MtoVisE[3].Text) == 0)
                    {
                        Frm_VisE.Messages.Add(new UI_Message()
                        {
                            Type=TipoMensaje.Error,
                            Text= "Debe ingresar algún monto antes de Aceptar.",
                            ControlName = "Tx_MtoVisE_001"
                        });
                        return false;
                    }

                    TC = Format.StringToDouble(Frm_VisE.Tx_MtoVisE[0].Text);  //Tipo de Cambio.
                    ML = Format.StringToDouble(Frm_VisE.Tx_MtoVisE[1].Text);  //Monto a Liquidar.
                    if (ML > 0 && TC == 0)
                    {
                        Frm_VisE.Messages.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Debe ingresar el Tipo de Cambio correspondiente al Monto a Liquidar. Corrija los valores y reintente.",
                            ControlName = "Tx_MtoVisE_000"
                        });
                        return false;
                    }

                    MODXPLN0.VxDatP.NumOpe = MODGCVD.VgCvd.OpeSin;
                    MODXPLN0.VxDatP.IndPrt = T_MODGCVD.ICli;
                    MODXPLN0.VxDatP.CodMnd = short.Parse(Frm_VisE.Cb_Mnd.Items.ElementAt(Frm_VisE.Cb_Mnd.ListIndex).ID);
                    n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0,unit, MODXPLN0.VxDatP.CodMnd);
                    MODXPLN0.VxDatP.NemMnd = MODGTAB0.VMnd[n].Mnd_MndNmc;
                    MODXPLN0.VxDatP.CodMBC = MODGTAB0.VMnd[n].Mnd_MndCbc;
                    MODXPLN0.VxDatP.TipCam = Format.StringToDouble(Frm_VisE.Tx_MtoVisE[0].Text);
                    MODXPLN0.VxDatP.MtoLiq = Format.StringToDouble(Frm_VisE.Tx_MtoVisE[1].Text);
                    MODXPLN0.VxDatP.MtoInf = Format.StringToDouble(Frm_VisE.Tx_MtoVisE[2].Text);
                    MODXPLN0.VxDatP.mtotran = Format.StringToDouble(Frm_VisE.Tx_MtoVisE[3].Text);
                    MODXPLN0.VxDatP.MtoLiqs = MODXPLN0.VxDatP.MtoLiq;
                    MODXPLN0.VxDatP.MtoInfs = MODXPLN0.VxDatP.MtoInf;
                    MODXPLN0.VxDatP.MtoTrans = MODXPLN0.VxDatP.mtotran;
                    MODXPLN0.VxDatP.Acepto = (short)(true ? -1 : 0);
                    if (MODXPLN0.VxDatP.MtoLiqs > 0)
                    {
                        MODGCVD.VgCvd.Etapa = "VIA";
                    }
                    break;
                case 1:  //Cancelar
                    MODXPLN0.VxDatP.Acepto = (short)(false ? -1 : 0);
                    break;
            }
            return true;
        }
    }
}
