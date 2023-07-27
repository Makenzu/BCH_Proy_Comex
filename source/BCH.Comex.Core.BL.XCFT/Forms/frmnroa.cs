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
    public class frmnroa
    {
        public static void Form_Load(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            MODGTAB0.SyGetn_Mnd(initObj.MODGTAB0, uow);
            MODGTAB0.CargaEnLista_Mnd(initObj.MODGTAB0, initObj.Frmnroa.Cb_Moneda);
            initObj.Frmnroa.Cb_Moneda.ListIndex = MODGPYF0.PosLista(initObj.Frmnroa.Cb_Moneda, initObj.MODGCVD.CodMonDolar);
            initObj.Frmnroa.CAM_TipCam.Text = Format.FormatCurrency(0, "###,##0.0000");
            initObj.MODXANU.VgAnu.AnuSinOK = (short)(false ? -1 : 0);
            Cb_Moneda_Click(initObj, uow, 11);
        }

        public static string Cb_Moneda_Click(InitializationObject initObj, UnitOfWorkCext01 uow, short monedaItemData)
        {
            short a = 0;
            double b = 0;
            a = monedaItemData;
            b = MODGTAB0.SyGet_Vmd(initObj.MODGTAB0, uow, DateTime.Now.ToString("dd/MM/yyyy"), a);

            if (initObj.MODGTAB0.VVmd.VmdObs > 0)
            {
                initObj.Frmnroa.CAM_TipCam.Text = Format.FormatCurrency((initObj.MODGTAB0.VVmd.VmdMbc), "0.0000");
                return Format.FormatCurrency((initObj.MODGTAB0.VVmd.VmdMbc), "0.0000");
            }
            else
            {
                initObj.Frmnroa.CAM_TipCam.Text = Format.FormatCurrency((initObj.MODGTAB0.VVmd.VmdObs), "0.0000");
                return Format.FormatCurrency((initObj.MODGTAB0.VVmd.VmdObs), "0.0000");
            }
        }

        public static void bot_acep_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short nr = 0;
            short m = 0;
            short i = 0;
            int n = 0;

            if(string.IsNullOrEmpty(initObj.Frmnroa.CAM_TipCam.Text) || double.Parse(initObj.Frmnroa.CAM_TipCam.Text) == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Ingrese Tipo de Cambio",
                    Title = T_MODXANU.MsgxAnu,
                    ControlName = "TipoCambio"
                });
                return;
            }

            nr = VB6Helpers.CShort(initObj.Frmnroa.Cam_NroPln.Text);
            if(string.IsNullOrEmpty(initObj.Frmnroa.Cam_NroPln.Text) ||  nr == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Ingrese nro. de planillas",
                    Title = T_MODXANU.MsgxAnu,
                    ControlName = "NroPlanillas"
                });
                return;
            }

            
            initObj.MODXANU.VgAnu.AnuSin = (short)(true ? -1 : 0);
            initObj.MODXANU.VxAnus = new T_xAnu[nr];
            //Pedir 3 números de Planillas y cargarlos en numpre,fecpre.-
            //----------------------------------------
            // Cargo los valores en la estructura
            //----------------------------------------
            initObj.MODGCVD.VxPlaAnu.Moneda = (short)initObj.Frmnroa.Cb_Moneda.get_ItemData_((short)initObj.Frmnroa.Cb_Moneda.ListIndex);
            initObj.MODGCVD.VxPlaAnu.TipCam = Format.StringToDouble(initObj.Frmnroa.CAM_TipCam.Text);
            initObj.MODGCVD.VxPlaAnu.Nropla = nr;
            m = MODGTAB0.Get_VMnd(initObj.MODGTAB0, uow, initObj.MODGCVD.VxPlaAnu.Moneda);
            initObj.MODGCVD.VxPlaAnu.Moneda = initObj.MODGTAB0.VMnd[m].Mnd_MndCbc;

            //-----------------------------------------
            //Se genera una nueva planilla.-
            //-----------------------------------------
            for (i = 0; i < nr; i++)
            {
                //se valida si quedan numeros para las PVX
                n = (int)MODGRNG.LeeSceRng(initObj.MODGRNG, initObj.MODGUSR, initObj.Mdi_Principal, uow, "PVX");
                //Si retorna -1 no se logro conseguir numero
                if (n == -1)
                {
                    return;
                }
                initObj.MODXANU.VxAnus[i] = new T_xAnu();
                initObj.MODXANU.VxAnus[i].NumPre = MODXPLN1.Fn_DigVer_xPlv(initObj.MODGSCE.VGen.CodPbc, initObj.MODGSCE.VGen.CodBch, n, VB6Helpers.Year(DateTime.Now));
                if (string.IsNullOrEmpty(initObj.MODXANU.VxAnus[i].NumPre))
                {
                    return;
                }
                initObj.MODXANU.VxAnus[i].fecpre = DateTime.Now.ToString("dd/MM/yyyy");
                initObj.MODXANU.VxAnus[i].CodMnd = initObj.MODGCVD.VxPlaAnu.Moneda;
            }

            //-----------------------------------------
            initObj.MODXANU.VgAnu.AnuSinOK = (short)(true ? -1 : 0);
        }
    }
}
