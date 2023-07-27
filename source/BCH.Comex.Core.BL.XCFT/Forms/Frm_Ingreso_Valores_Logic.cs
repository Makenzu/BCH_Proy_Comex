using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public class Frm_Ingreso_Valores_Logic
    {
        public static void FormLoad(InitializationObject Modulos,UnitOfWorkCext01 unit)
        {
            short n;
            n = MODGTAB0.Get_VMnd(Modulos.MODGTAB0,unit,Modulos.MODGTAB0.VVmd.VmdCod);
            Modulos.Frm_Ingreso_Valores.Moneda.Text = Modulos.MODGTAB0.VMnd[n].Mnd_MndNom;  //Carga Moneda.-
            Modulos.Frm_Ingreso_Valores.Fecha.Text = VB6Helpers.Format(Modulos.MODGTAB0.VVmd.VmdFec, "dd/MM/yyyy");  //Carga Fecha.-
            
            //por las dudas le asigno el string vacio a los tags
            Modulos.Frm_Ingreso_Valores.Paridad.Tag = String.Empty;
            Modulos.Frm_Ingreso_Valores.TC_Obs.Tag = String.Empty;

            Modulos.Frm_Ingreso_Valores.Paridad.Text = Format.FormatCurrency(0, MODGPYF1.DecObjeto(Modulos.Frm_Ingreso_Valores.Paridad));
            Modulos.Frm_Ingreso_Valores.TC_Obs.Text = Format.FormatCurrency(0, MODGPYF1.DecObjeto(Modulos.Frm_Ingreso_Valores.TC_Obs));
        }

        public static void IngresarValores(InitializationObject Modulos, UnitOfWorkCext01 unit)
        {
            if ((VB6Helpers.Val(MODGPYF0.unformat(Modulos.MODGPYF0, Modulos.Frm_Ingreso_Valores.Paridad.Text)) != 0) && (VB6Helpers.Val(MODGPYF0.unformat(Modulos.MODGPYF0, Modulos.Frm_Ingreso_Valores.TC_Obs.Text)) != 0))
            {
                Modulos.Frm_Ingreso_Valores.Paridad.Text = Format.FormatCurrency(Format.StringToDouble(Modulos.Frm_Ingreso_Valores.Paridad.Text), "##,###0.000000000");
                Modulos.Frm_Ingreso_Valores.TC_Obs.Text = Format.FormatCurrency(Format.StringToDouble(Modulos.Frm_Ingreso_Valores.TC_Obs.Text), "##,###0.0000");
                Modulos.MODGTAB0.VVmd.VmdPrd = Format.StringToDouble(Modulos.Frm_Ingreso_Valores.Paridad.Text);
                Modulos.MODGTAB0.VVmd.VmdObs = Format.StringToDouble(Modulos.Frm_Ingreso_Valores.TC_Obs.Text);
                string vieneDe = Modulos.Frm_Ingreso_Valores.VieneDe;
                Modulos.FormularioQueAbrir = String.IsNullOrEmpty(vieneDe) ? "Index" : vieneDe;
                Modulos.Frm_Ingreso_Valores.VieneDe = String.Empty;
                Modulos.Frm_Ingreso_Valores = null;
            }
            else
            {
                Modulos.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type= TipoMensaje.Error,
                    Text = "Los valores deben ser distintos de cero."
                });
                Modulos.FormularioQueAbrir = "Ingreso_Valores";
            }
        }

        public static void Cancelar_Click(InitializationObject initObject)
        {
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;

            MODGTAB0.VVmd.VmdPrd = 0;
            MODGTAB0.VVmd.VmdObs = 0;
            initObject.FormularioQueAbrir =  "Index";
            initObject.Frm_Ingreso_Valores.VieneDe = String.Empty;
            if (!String.IsNullOrEmpty(initObject.Frm_Ingreso_Valores.MensageCancelacion))
            {
                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type= TipoMensaje.Error,
                    Text= initObject.Frm_Ingreso_Valores.MensageCancelacion
                });
            }
            //initObject.Frm_Ingreso_Valores = null;
        }
    }
}
